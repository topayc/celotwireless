// CelotNetworkMangerService.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//
#include "stdafx.h"
#include "Mysql.h"
#include "CriticalSection.h"
#include "MultiThreadSync.h"
#include  "GenericAsyncQueue.h"
#include  "Util.h"
#include  "Logger.h"
#include "IniFile.h"
#include "CelotIocp.h"

/*
/사용자 명령 Control Service 상수
*/
#define SERVICE_CONTROL_RELOAD_DEVICE 128

using namespace std;

void CelotServiceMain(DWORD argc, LPTSTR *argv);
void CelotServiceHandler(DWORD opCode);

BOOL SetConfigInfo();

/*
// Run Mode
// 1:  Debug
// 2:  Service
*/
DWORD  RUN_MODE = 1;

/*
/ default config
*/
USHORT SERVICE_PORT = 33333;
DWORD MAX_SESSION_COUNT = 1000;
TCHAR  SERVICE_NAME[MAX_PATH] = { 0, };
TCHAR  SERVICE_DISPLAY_NAME[1024] = { 0, };
TCHAR  SERVICE_FILE_PATH[MAX_PATH] = { 0, };
DWORD   LOW_BATTERY_LIMIT = 50;



/*
// Database
*/
char host[100] = { 0, };
char db_name[100] = { 0, };
char user[100] = { 0, };
char password[100] = { 0, };
char charset[100] = { 0, };

/*
// 현재 서버의 PROTOCOL
*/
DWORD CUR_PROTOCOL = PROTOCOL_CELOT;

/*
/ Service
*/
SERVICE_STATUS_HANDLE g_hSrv;
DWORD g_NowServiceState;
HANDLE g_ServiceExitEvent;

CelotIocp celotServer;

int _tmain(int argc, _TCHAR* argv[])
{
	WSADATA WsaData;
	WSAStartup(MAKEWORD(2, 2), &WsaData);
	
	RUN_MODE = 2;
	CPlusPlusLogging::Logger::getInstance()->enableFileLogging();

	char buffer[100];
	if (!SetConfigInfo()) {
		CPlusPlusLogging::Logger::getInstance()->error("Error occurd in loading configuration file");
		return 0;
	}
	CPlusPlusLogging::Logger::getInstance()->info(itoa(RUN_MODE, buffer, 10));
	CPlusPlusLogging::Logger::getInstance()->info("Succeed in loading configration file");

	SERVICE_TABLE_ENTRY ste[] = {
		{ SERVICE_NAME, (LPSERVICE_MAIN_FUNCTION)CelotServiceMain },
		{ NULL, NULL }
	};

	//서비스 실행, 서비스 실행이 실패하면 디버깅 모드로 동작 
	if (!StartServiceCtrlDispatcher(ste)){
		RUN_MODE = 1;   // 디버깅 모드
		CPlusPlusLogging::Logger::getInstance()->enableConsoleLogging();
		CPlusPlusLogging::Logger::getInstance()->info("## Debuging Concole mode");
		celotServer.Begin();
		getchar();
		celotServer.End();
		WSACleanup();
		return 0;
	}
	WSACleanup();
	return 0;
}

BOOL SetConfigInfo(){
	IniFile	 IniFile;
	if (IniFile.Open(CONFIG_FILENAME))
	{
		DWORD bufferLength = sizeof(SERVICE_NAME);
		IniFile.GetValue(_T("SERVICE"), _T("SERVICE_NAME"), SERVICE_NAME, &bufferLength);
		bufferLength = sizeof(SERVICE_DISPLAY_NAME);
		IniFile.GetValue(_T("SERVICE"), _T("SERVICE_DISPLAY_NAME"), SERVICE_DISPLAY_NAME, &bufferLength);
		bufferLength = sizeof(SERVICE_FILE_PATH);
		IniFile.GetValue(_T("SERVICE"), _T("SERVICE_FILE_PATH"), SERVICE_FILE_PATH, &bufferLength);

		TCHAR tmpHost[100] = { 0, };
		TCHAR tmpDb[100] = { 0, };
		TCHAR tmpUser[100] = { 0, };
		TCHAR tmpPass[100] = { 0, };
		TCHAR tmpCharset[100] = { 0, };

		bufferLength = sizeof(tmpHost);
		IniFile.GetValue(_T("DATABASE"), _T("DATABASE_HOST"), tmpHost, &bufferLength);
		bufferLength = sizeof(tmpDb);
		IniFile.GetValue(_T("DATABASE"), _T("DATABASE_NAME"), tmpDb, &bufferLength);
		bufferLength = sizeof(tmpUser);
		IniFile.GetValue(_T("DATABASE"), _T("DATABASE_ID"), tmpUser, &bufferLength);
		bufferLength = sizeof(tmpPass);
		IniFile.GetValue(_T("DATABASE"), _T("DATABASE_PASSWORD"), tmpPass, &bufferLength);
		bufferLength = sizeof(tmpCharset);
		IniFile.GetValue(_T("DATABASE"), _T("DATABASE_CHARSET"), tmpCharset, &bufferLength);

#ifdef _UNICODE
		WideCharToMultiByte(CP_ACP, 0, tmpHost, -1, host, sizeof(host), NULL, NULL);
		WideCharToMultiByte(CP_ACP, 0, tmpDb, -1, db_name, sizeof(db_name), NULL, NULL);
		WideCharToMultiByte(CP_ACP, 0, tmpUser, -1, user, sizeof(user), NULL, NULL);
		WideCharToMultiByte(CP_ACP, 0, tmpPass, -1, password, sizeof(password), NULL, NULL);
		WideCharToMultiByte(CP_ACP, 0, tmpCharset, -1, charset, sizeof(charset), NULL, NULL);
#else 
		strcpy(host, tmpHost);
		strcpy(db_name, tmpDb);
		strcpy(user, tmpUser);
		strcpy(password, tmpPass);
		strcpy(charset, tmpCharset);
#endif
		return TRUE;
	}
	else {
		CPlusPlusLogging::Logger::getInstance()->error("Config file can`t load");
		return FALSE;
	}
}

void SetStatus(DWORD dwState, DWORD dwAccept = SERVICE_ACCEPT_STOP)
{
	SERVICE_STATUS ss;
	ss.dwServiceType = SERVICE_WIN32_OWN_PROCESS;
	ss.dwCurrentState = dwState;
	ss.dwControlsAccepted = dwAccept;
	ss.dwWin32ExitCode = 0;
	ss.dwServiceSpecificExitCode = 0;
	ss.dwCheckPoint = 0;
	ss.dwWaitHint = 0;

	g_NowServiceState = dwState;
	SetServiceStatus(g_hSrv, &ss);
}

void CelotServiceMain(DWORD argc, LPTSTR *argv)
{
	g_hSrv = RegisterServiceCtrlHandler(SERVICE_NAME, (LPHANDLER_FUNCTION)CelotServiceHandler);
	if (g_hSrv == 0) {
		return;
	}

	SetStatus(SERVICE_START_PENDING);
	g_ServiceExitEvent = CreateEvent(NULL, TRUE, FALSE, _T("CelotServiceExitEevent"));
	celotServer.Begin();
	SetStatus(SERVICE_RUNNING);

	WaitForSingleObject(g_ServiceExitEvent, INFINITE);
	SetStatus(SERVICE_STOP_PENDING, 0);
	celotServer.End();
	SetStatus(SERVICE_STOPPED);
}

void CelotServiceHandler(DWORD fdwControl)
{
	if (fdwControl == g_NowServiceState)
		return;

	switch (fdwControl) {
	//case SERVICE_CONTROL_PAUSE:
		//SetStatus(SERVICE_PAUSE_PENDING, 0);
		//SetStatus(SERVICE_PAUSED);
		//break;
	//case SERVICE_CONTROL_CONTINUE:
		//SetStatus(SERVICE_CONTINUE_PENDING, 0);
		//SetStatus(SERVICE_RUNNING);
		//break;
	case SERVICE_CONTROL_STOP:
		SetStatus(SERVICE_STOP_PENDING, 0);
		SetEvent(g_ServiceExitEvent);
		break;
	case SERVICE_CONTROL_RELOAD_DEVICE:
		celotServer.LoadDevice(); 
		break;
	case SERVICE_CONTROL_INTERROGATE:
	default:
		SetStatus(g_NowServiceState);
		break;
	}
}


