#include "stdafx.h"
#include "Mysql.h"
#include "Device.h"
#include "CriticalSection.h"
#include "MultiThreadSync.h"
#include "Log.h"
#include "Logger.h"
#include "DeviceManager.h"

extern char host[];
extern char db_name[];
extern char user[];
extern char password[];
extern DWORD RUN_MODE;

DeviceManager::DeviceManager(){}
DeviceManager::~DeviceManager(){}

BOOL DeviceManager::Begin()
{
	CPlusPlusLogging::Logger::getInstance()->info(host);
	CPlusPlusLogging::Logger::getInstance()->info(db_name);
	CPlusPlusLogging::Logger::getInstance()->info(user);
	CPlusPlusLogging::Logger::getInstance()->info(password);
	
	if (!mMySql.Begin(host, user, password, db_name)){
		return FALSE;
	}
	if (mMySql.SelectDB("celot_db")){
		return TRUE;
	}
	else {
		return FALSE;
	}
	
	mNmsAlertCheck.Init();
}


BOOL DeviceManager::End()
{
	//mDeviceMap와 mDeviceVector 동일한 메모리 주소를 참조
	//only delete vector;
	for (int i = 0; i < mDeviceVector.size(); i++){
		delete  mDeviceVector[i];
	}

	mDeviceVector.clear();
	mDeviceMap.clear();
	mReportedDevicePhoneNumberVector.clear();

	//Mysql Disconnect
	mMySql.Disconnect();
	return TRUE;
}

BOOL DeviceManager::LoadDevices()
{
	CThreadSync Sync;
	//mDeviceMap와 mDeviceVector 동일한 메모리 주소를 참조
	//only delete vector;
	for (int i = 0; i < mDeviceVector.size(); i++){
		delete  mDeviceVector[i];
	}
	/*
	for (int i = 0; i <  mDeviceMap.size(); i++){
	delete  mDeviceMap[i];
	}
	*/

	mDeviceVector.clear();
	mDeviceMap.clear();

	std::ostringstream ss;
	mMySql.Query("SELECT *  FROM device");
	int row = mMySql.Num_rows();
	if ( row > 0){
		mMySql.First();
		while (!mMySql.IsEOF())
		{
			Device* pDevice = new Device();
			pDevice->Init();
			pDevice->SetDeviceNo(atoi(mMySql[(my_ulonglong)0]));
			pDevice->SetGroupName(mMySql[1]);
			pDevice->SetName(mMySql[2]);
			pDevice->SetSerialNo(atoi(mMySql[3]));
			pDevice->SetSecuCode(mMySql[4]);
			pDevice->SetRouterIp(mMySql[5]);
			pDevice->SetLatitude(atof(mMySql[6]));
			pDevice->SetLongitude(atof(mMySql[7]));
			pDevice->SetDes(mMySql[8]);
			pDevice->SetPhoneNumber(atoi(mMySql[9]));
			pDevice->SetSmsSupport(atoi(mMySql[10]));
			pDevice->SetBatterySupport(atoi(mMySql[11]));
			pDevice->SetWifiSupport(atoi(mMySql[12]));
			pDevice->SetVpnSupport(atoi(mMySql[13]));
			
			pDevice->SetResetTime(atoi(mMySql[14]));
			pDevice->SetAlertStatus(atoi(mMySql[15]));
			pDevice->SetAlertOccurentTime(mMySql[16]);

			mDeviceVector.push_back(pDevice);
			mDeviceMap.insert(map< int, Device* >::value_type(pDevice->GetPhoneNumber(), pDevice));
			mMySql.Next();
		}
		ss << "Registered Device count  =>  : " << mDeviceMap.size() << endl;
		CPlusPlusLogging::Logger::getInstance()->info(ss);
	}
	else {
		ss << "Registered Device count  : " << 0 << endl;
		CPlusPlusLogging::Logger::getInstance()->info(ss);
	}
	mMySql.FreeResult();
	return TRUE;
}

BOOL DeviceManager::LoadRecentReportedPhoneNumbers()
{
	CThreadSync Sync;
	mReportedDevicePhoneNumberVector.clear();
	std::ostringstream ss;

	CPlusPlusLogging::Logger::getInstance()->info("Reported Device List");
	mMySql.Query("select * from packet");
	int row = mMySql.Num_rows();
	if (row > 1){
		mMySql.First();
		while (!mMySql.IsEOF())
		{
			mReportedDevicePhoneNumberVector.push_back(atoi(mMySql[1]));
			//DisplayNmsReport((nms_reprot_t*)mMySql[3]);
			mMySql.Next();
		}
		ss << "Reported Device count   : " << mReportedDevicePhoneNumberVector.size();
		CPlusPlusLogging::Logger::getInstance()->info(ss);
	}
	else {
		ss << "Reported Device count   : " << 0 << endl;
		CPlusPlusLogging::Logger::getInstance()->info(ss);
	}
	mMySql.FreeResult();
	return TRUE;

}

BOOL DeviceManager::AddPhoneNumber(int phoneNumber)
{
	CThreadSync Sync;
	mReportedDevicePhoneNumberVector.push_back(phoneNumber);
	return TRUE;
}


BOOL DeviceManager::ProcessDevice(nms_reprot_t& report)
{
	CThreadSync Sync;
	if (IsReportedDevice(report.header.session_id))
	{
		UpdatePacket(report);
	}
	else 
	{
		InsertPacket(report);
	}

	InsertPacketLog(report);

	//check alerts and modify device info  if device registered
	if (IsRegisteredDevice(report.header.session_id))
		UpdateDeviceAlertStatus(report);
	
	mMySql.Commit();
	return TRUE;
}

VOID DeviceManager::InsertPacket(nms_reprot_t &report)
{
	char chunk[900 * 2 + 1];
	mMySql.EscapeString(chunk, (char*)&report, sizeof(nms_reprot_t));

	/*
	아래 주석처리된 부분 설명
	패킷의 필드가 테이블의 컬럼과 각각 매칭될 경우, int 배열를 콤마로 분리된 문자열로 저장하기 위한 코드 

	int bufferSize = 0;
	char ext1Buffer[500] = { 0, };
	for (int i = 0; i < sizeof(report.data.ext_device1) / sizeof(report.data.ext_device1[0]); i++){
		char strBuffer[100] = { 0, };
		itoa(report.data.ext_device1[i], strBuffer, 10);
		strcat(ext1Buffer, strBuffer);
		if (i != ((sizeof(report.data.ext_device1) / sizeof(report.data.ext_device1[0])) - 1)){
			strcat(ext1Buffer, ",");
		}
	}
	*/
	char queryStr[3000] = { 0, };
	sprintf(queryStr,
		"insert into packet( "
		"SessionId , "
		"Identifier , "
		"RawPacket, "
		"PacketRegDate) "
		"values(%d, %d, '%s',%d  )",
		report.header.session_id,
		report.header.session_id,
		chunk,
		(unsigned long)time(NULL));
	if (RUN_MODE == 1) CPlusPlusLogging::Logger::getInstance()->info(queryStr);
	mMySql.Query(queryStr);
}

VOID DeviceManager::UpdatePacket(nms_reprot_t &report)
{
	char chunk[900 * 2 + 1];
	mMySql.EscapeString(chunk, (char*)&report, sizeof(nms_reprot_t));
	
	char queryStr[3000] = { 0, };
	sprintf(queryStr,
		"update packet set "
		"SessionId = %d , "
		"Identifier = %d , "
		"RawPacket = '%s' "
		"where SessionId =%d",
		report.header.session_id,
		report.header.session_id,
		chunk,
		report.header.session_id
		);
	if (RUN_MODE == 1) CPlusPlusLogging::Logger::getInstance()->info(queryStr);
	mMySql.Query(queryStr);
}

VOID DeviceManager::InsertPacketLog(nms_reprot_t &report)
{
	char chunk[900 * 2 + 1];
	mMySql.EscapeString(chunk, (char*)&report, sizeof(nms_reprot_t));

	char queryStr[3000] = { 0, };
	sprintf(queryStr,
		"insert into packet_log( "
		"SessionId , "
		"Identifier , "
		"RawPacket, "
		"PacketLogRegDate) "
		"values(%d, %d,  '%s' , %d  )",
		report.header.session_id,
		report.header.session_id,
		chunk,
		(unsigned long)time(NULL));
	mMySql.Query(queryStr);
}

VOID DeviceManager::UpdateDeviceAlertStatus(nms_reprot_t& report)
{
	char queryStr[200] = { 0, };
	
	Device* device = GetDeivceByPhoneNumber(report.header.session_id);
	mNmsAlertCheck.Init();
	mNmsAlertCheck.CheckAlert(device, report);
	
	int updateAlertStatus = 0;
	int deviceAlertStatus = device->GetAlertStatus();
	if (mNmsAlertCheck.IsHaveAlert())
	{
		if (deviceAlertStatus == 2) return;
		updateAlertStatus = 2;
	}
	else
	{
		if (deviceAlertStatus == 1) return;
		updateAlertStatus = 1;
	}

	sprintf(queryStr,
		"update device set "
		"alertStatus = %d , "
		"AlertOccurentTime = '%s' "
		"where PhoneNumber = %d",
		updateAlertStatus,
		report.data.current_time,
		report.header.session_id
	);

	if (RUN_MODE == 1) CPlusPlusLogging::Logger::getInstance()->info(queryStr);
	mMySql.Query(queryStr);
}

BOOL DeviceManager::IsRegisteredDevice(int phoneNumber)
{
	CThreadSync Sync;
	std::map< int, Device* >::iterator FindIter = mDeviceMap.find(phoneNumber);
	if (FindIter != mDeviceMap.end())
	{
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}

BOOL DeviceManager::IsReportedDevice(int phoneNumber)
{
	CThreadSync Sync;
	for (int i = 0; i < mReportedDevicePhoneNumberVector.size(); i++){
		int rPhoneNumber = mReportedDevicePhoneNumberVector[i];
		if (phoneNumber == rPhoneNumber){
			return TRUE;
		}
	}
	return FALSE;
}

INT DeviceManager::GetDeviceNo(int phoneNumber)
{
	CThreadSync Sync;
	std::map< int, Device* >::iterator FindIter = mDeviceMap.find(phoneNumber);
	if (FindIter != mDeviceMap.end())
	{
		return FindIter->second->GetDeviceNo();
	}
	else
	{
		return 0;
	}
}

Device* DeviceManager::GetDeivceByPhoneNumber(int phoneNumber)
{
	CThreadSync Sync;
	return mDeviceMap[phoneNumber];
}

VOID DeviceManager::DisplayNmsReport(nms_reprot_t* cmd)
{
	CThreadSync Sync;
	printf("========= Reported  Device  ===========\n\n");
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
	printf("\n\n");
}



