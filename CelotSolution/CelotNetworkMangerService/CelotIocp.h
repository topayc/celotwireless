#pragma once
#include "BaseIocp.h"
#include "NetworkSession.h"
#include "CelotSessionManager.h"
#include "DeviceManager.h"
#include "NMSAlertCheck.h"


typedef GenericAsyncQueue<nms_reprot_t> NMS_REPORT_QUEUE;

class CelotIocp : public BaseIocp
{
public:
	CelotIocp(VOID);
	virtual ~CelotIocp(VOID);

private:
	CNetworkSession					mListenSession;
	CelotSessionManager		    mCelotSessionManager;
	DeviceManager					mDeviceManager;
	NMS_REPORT_QUEUE		    mReadReportPacketQueue;  
	NMS_REPORT_QUEUE		    mSendReportPacketQueue;

	HANDLE								mProcessThreadHandle;
	HANDLE								mProcessThreadDestroyEvent;

	NMSAlertCheck					mNmsAlertCheck;

	HANDLE								mPacketReadReadyEvent;
	HANDLE								mPacketSendThreadHandle;
	HANDLE								mPacketSendThreadDestroyEvent;

public:
	BOOL	Begin(VOID);
	BOOL	End(VOID);

protected:
	VOID OnIoRead(VOID *object, DWORD dataLength);
	VOID OnIoWrote(VOID *object, DWORD dataLength);
	VOID OnIoConnected(VOID *object);
	VOID OnIoDisconnected(VOID *object);
public:
	VOID ProcessThreadCallback(VOID);
	VOID PacketSendThreadCallback(VOID);
public :
	VOID DisplayNmsReport(nms_reprot_t* cmd);
	VOID LoadDevice();
};
