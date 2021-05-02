#include "stdafx.h"

#include "CriticalSection.h"
#include "MultiThreadSync.h"
#include "NetworkSession.h"
#include "NmsPacketSession.h"
#include "Stream.h"
#include "IniFile.h"
#include "Logger.h"
#include "Log.h"
#include "GenericAsyncQueue.h"
#include "CelotSession.h"
#include "CelotSessionManager.h"
#include "DeviceManager.h"
#include "Logger.h"
#include "BaseIocp.h"
#include "CelotIocp.h"

using namespace std;
extern TCHAR  SERVICE_NAME[MAX_PATH];
extern TCHAR  SERVICE_DISPLAY_NAME[1024];
extern TCHAR  SERVICE_FILE_PATH[MAX_PATH];
extern USHORT SERVICE_PORT;
extern DWORD MAX_SESSION_COUNT;
extern DWORD CUR_PROTOCOL;
extern DWORD RUN_MODE;
extern DWORD LOW_BATTERY_LIMIT;

DWORD WINAPI ProcessThreadCallback(LPVOID parameter)
{
	CelotIocp *Owner = (CelotIocp*)parameter;
	Owner->ProcessThreadCallback();
	return 0;
}

DWORD WINAPI PacketSendThreadCallback(LPVOID parameter)
{
	CelotIocp *Owner = (CelotIocp*)parameter;
	Owner->PacketSendThreadCallback();
	return 0;
}


CelotIocp::CelotIocp(VOID)
{
}

CelotIocp::~CelotIocp(VOID)
{
}

VOID CelotIocp::ProcessThreadCallback()
{
	nms_reprot_t report;
	BOOL reportDevice = FALSE; 
	HANDLE events[2] = { mProcessThreadDestroyEvent, mPacketReadReadyEvent };
	
	while (TRUE){
		DWORD dwResult = ::WaitForMultipleObjects(2, events, FALSE, INFINITE);
		switch (dwResult){
		case WAIT_OBJECT_0:
			return;
		case WAIT_OBJECT_0 + 1:
			while (TRUE)
			{
				if (mReadReportPacketQueue.Pop(report)){
					if (!mDeviceManager.IsReportedDevice(report.header.session_id)){
						reportDevice = TRUE;
					}
					if (RUN_MODE == 1){
						DisplayNmsReport(&report);
					}
					mDeviceManager.ProcessDevice(report);
					if (reportDevice){
						mDeviceManager.AddPhoneNumber(report.header.session_id);
						reportDevice = FALSE;
					}
				}
				else {
					ResetEvent(mPacketReadReadyEvent);
					break;
				}
			}
			break;
		}
	}
}


//패킷 전송 스레드 
VOID CelotIocp::PacketSendThreadCallback()
{
	
}

BOOL CelotIocp::Begin(VOID)
{

	IniFile	 IniFile;
	if (IniFile.Open(CONFIG_FILENAME))
	{
		IniFile.GetValue(_T("SERVICE"), _T("SERVICE_PORT"), &SERVICE_PORT);
		IniFile.GetValue(_T("SERVICE"), _T("MAX_SESSION_COUNT"), &MAX_SESSION_COUNT);
		IniFile.GetValue(_T("PROTOCOL"), _T("USING_PROTOCOL"), &CUR_PROTOCOL);
		IniFile.GetValue(_T("CLIENT"), _T("LOW_BATTERY_LIMIT"), &LOW_BATTERY_LIMIT);
		IniFile.Close();
	
		CPlusPlusLogging::Logger::getInstance()->info("Config file loading");
		std::ostringstream ss;
		ss << "Low battery limit    : " << LOW_BATTERY_LIMIT << endl;
		CPlusPlusLogging::Logger::getInstance()->info(ss);
	}
	else
	{
		CPlusPlusLogging::Logger::getInstance()->error("Config file can`t load");
	}

	mNmsAlertCheck.Init();

	//Job Quey init
	if (!mReadReportPacketQueue.Begin()){
		End();
		return FALSE;
	}

	//Job Quey init
	if (!mSendReportPacketQueue.Begin()){
		End();
		return FALSE;
	}

	if (!BaseIocp::Begin())
	{	
		End();
		return FALSE;
	}

	CPlusPlusLogging::Logger::getInstance()->info("CELOT Service initializing");
	if (!mListenSession.Begin())
	{
		End();
		return FALSE;
	}

	CPlusPlusLogging::Logger::getInstance()->info("Connecting to CELOT Database");
	if (!mDeviceManager.Begin())
	{
		CPlusPlusLogging::Logger::getInstance()->info("Error  in receiving database data");
		End();
		return FALSE;
	}

	CPlusPlusLogging::Logger::getInstance()->info("receiving initial Devices data and Reported Devices");
	if (!mDeviceManager.LoadDevices() || !mDeviceManager.LoadRecentReportedPhoneNumbers())
	{
		End();
		return FALSE;
	}

	CPlusPlusLogging::Logger::getInstance()->info("CELOT Service trying to bind & listen");
	if (!mListenSession.TcpBind())
	{
		End();
		return FALSE;
	}

	if (!mListenSession.Listen(SERVICE_PORT, MAX_SESSION_COUNT))
	{
		End();
		return FALSE;
	}

	char temp[400] = { 0, };
	_snprintf(temp, sizeof(temp), "CELOT Service is listening at port  %d by %d", SERVICE_PORT, MAX_SESSION_COUNT);
	CPlusPlusLogging::Logger::getInstance()->info(temp);
	if (!BaseIocp::RegisterSocketToIocp(mListenSession.GetSocket(), (ULONG_PTR)&mListenSession))
	{
		End();
		return FALSE;
	}

	if (!mCelotSessionManager.Begin(MAX_SESSION_COUNT, mListenSession.GetSocket()))
	{
		End();
		return FALSE;
	}

	CPlusPlusLogging::Logger::getInstance()->info("CELOT Session initialized and Session Begin to Aceept All");
	if (!mCelotSessionManager.AcceptAll())
	{
		End();
		return FALSE;
	}

	mProcessThreadDestroyEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
	if (!mProcessThreadDestroyEvent)
	{
		End();
		return FALSE;
	}

	mPacketReadReadyEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
	if (!mPacketReadReadyEvent)
	{
		End();
		return FALSE;
	}

	mProcessThreadHandle = CreateThread(NULL, 0, ::ProcessThreadCallback, this, 0, NULL);
	if (!mProcessThreadHandle)
	{
		End();
		return FALSE;
	}

	mPacketSendThreadDestroyEvent= CreateEvent(NULL, FALSE, FALSE, NULL);
	if (!mPacketSendThreadDestroyEvent)
	{
		End();
		return FALSE;
	}

	mPacketSendThreadHandle = CreateThread(NULL, 0, ::PacketSendThreadCallback, this, 0, NULL);
	if (!mPacketSendThreadHandle)
	{
		End();
		return FALSE;
	}

	CPlusPlusLogging::Logger::getInstance()->info((CUR_PROTOCOL == PROTOCOL_CELOT ? "PROTOCOL :  CELOT NMS Protocol" : "PROTOCOL :  SNMP Protocol"));
	CPlusPlusLogging::Logger::getInstance()->info("CELOT Sever Start now");
	CPlusPlusLogging::Logger::getInstance()->info("Come on !!");
	return TRUE;
}

BOOL CelotIocp::End(VOID)
{
	if (mProcessThreadDestroyEvent && mProcessThreadHandle)
	{
		SetEvent(mProcessThreadDestroyEvent);
		WaitForSingleObject(mProcessThreadHandle, INFINITE);
		CloseHandle(mProcessThreadDestroyEvent);
		CloseHandle(mProcessThreadHandle);
	}

	if (mPacketSendThreadDestroyEvent && mPacketSendThreadHandle)
	{
		SetEvent(mPacketSendThreadDestroyEvent);
		WaitForSingleObject(mPacketSendThreadHandle, INFINITE);
		CloseHandle(mPacketSendThreadDestroyEvent);
		CloseHandle(mPacketSendThreadHandle);
	}

	BaseIocp::End();
	mReadReportPacketQueue.End();
	mSendReportPacketQueue.End();
	mCelotSessionManager.End();
	mDeviceManager.End();
	mListenSession.End();
	return TRUE;
}

VOID CelotIocp::OnIoConnected(VOID *object)
{
	CelotSession *session= (CelotSession*) object;
	TCHAR	RemoteAddress[32]	= {0,};
	USHORT	RemotePort			= 0;

	if (!BaseIocp::RegisterSocketToIocp(session->GetSocket(), (ULONG_PTR)session))
	{
		End();
		return;
	}

	if (!session->InitializeReadForIocp())
	{
		if (!session->Reload(mListenSession.GetSocket()))
		{
			End();
			return;
		}
	}

	mCelotSessionManager.IncreaseConnectedSessionCount();
	std::ostringstream ss;
	//CLog::WriteLog(_T("# New client connected : 0x%x(0x%x)\n"), session, session->GetSocket());
}

VOID CelotIocp::OnIoDisconnected(VOID *object)
{
	CelotSession *celotSession= (CelotSession*) object;
	if (!celotSession->Reload(mListenSession.GetSocket()))
	{
		End();
		return;
	}
	mCelotSessionManager.DecreaseConnectedSessionCount();
	//CLog::WriteLog(_T("# Client disconnected : 0x%x(0x%x)\n"), celotSession, celotSession->GetSocket());
}

VOID CelotIocp::OnIoRead(VOID *object, DWORD dataLength)
{
	CelotSession* celotSession = (CelotSession*)object;
	nms_reprot_t report;
	if (celotSession->ReadPacketForIocp(dataLength))
	{
		while (celotSession->GetPacket(report))
		{
			mReadReportPacketQueue.Push(report);
			SetEvent(mPacketReadReadyEvent);
		}
	}
	if (!celotSession->InitializeReadForIocp()) celotSession->End();
}

VOID CelotIocp::OnIoWrote(VOID *object, DWORD dataLength)
{
	CelotSession *celotSession = (CelotSession*)object;
	celotSession->WriteComplete();

#ifdef _FULL_DEBUG
	CLog::WriteLog(_T("# Client data wrote : 0x%x(0x%x)(%d)\n"), ConnectedUser, ConnectedUser->GetSocket(), dataLength);
#endif
}

VOID CelotIocp::DisplayNmsReport(nms_reprot_t* cmd)
{
	time_t   current_time;
	char temp[1024] = { 0, };

	struct tm * timeinfo;
	time_t dest;

	//dest = cmd->data_new.current_time;
	timeinfo = localtime(&dest);
	memcpy(temp, cmd, sizeof(temp));
	time(&current_time);
	printf(ctime(&current_time));

	printf("\n========= Display Function===========\n\n");
	printf("# 현재시간:%s\n", cmd->data.current_time);
	printf("# 전화번호:%d\n", cmd->header.session_id);
	printf("# 프로토콜버전:%d, ", cmd->header.pro_ver);
	printf("메시지타입:%d, ", cmd->header.message_type);
	printf("데이터길이:%d\n", cmd->header.data_len);
	printf("# IP Address:%s\n", cmd->data.current_ip_address);
	printf("# 수신데이터:%llu, ", cmd->data.use_rx_amount);
	printf("송신데이터:%llu\n", cmd->data.use_tx_amount);
	printf("# LAN1상태:%d, ", cmd->data.ethernet1_state);
	printf("LAN2상태:%d, ", cmd->data.ethernet2_state);
	printf("WAN상태:%d, ", cmd->data.network_state);
	printf("전원상태:%d\n", cmd->data.external_power);
	printf("# 외부장치상태:%d\n", cmd->data.devicestatus);
	printf("# BAND:%d(2:WCDMA,3:LTE), ", cmd->data.moduleband);
	printf("서비스상태:%d(1:service, else:no service), ", cmd->data.moduleservice);
	printf("RSSI:%d\n", cmd->data.modulesignal);
	printf("# Wifi 상태:%d, ", cmd->data.wifistatus);
	printf("VPN 상태:%d, ", cmd->data.vpnstatus);
	printf("NEW SMS:%d\n", cmd->data.newsms);

	printf("# SW_Ver:%s\n", cmd->data.sw_version);
	printf("# rpt_time:%d\n", cmd->data.rpt_time);
	printf("# RSRQsignal:%d\n", cmd->data.rsrqsignal);
	printf("# RSRPsignal:%d\n", cmd->data.rsrpsignal);
	printf("# HW_version:%s\n", cmd->data.hw_version);
	printf("# ext_device1[0]:%d\n", cmd->data.ext_device1[0]);
	//printf("# ext_device1[1]:%d(%s)\n", cmd->data.ext_device1[1], inet_ntoa(ClientSockInfo2.sin_addr));
	printf("# report_port:%d\n", cmd->data.rpt_port);
	printf("# remote_port:%d\n", cmd->data.rmt_port);

	//printf("\n[TCP 서버] 클라이언트 접속: IP주소=%s, 포트번호=%d\n", inet_ntoa(ClientSockInfo2.sin_addr), port);
	printf("\n========= Display Function END =======\n\n\n");
}

VOID CelotIocp::LoadDevice()
{
	mDeviceManager.LoadDevices();
}


