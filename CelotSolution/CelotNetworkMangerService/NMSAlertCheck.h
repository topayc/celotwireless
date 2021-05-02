#pragma once
class NMSAlertCheck
{
private :
	INT32 mRouterErrCnt;
	INT32 mUnknownDeviceErrCnt;
	INT32 mLan1ErrCnt;
	INT32 mLan2ErrCnt;
	INT32 mNetworkErrCnt;
	INT32 mExtPowerErrCnt;
	INT32 mRssiLevelErrCnt;
	INT32 mLowBatteryErrCnt;
	INT32 mDeviceIdErrCnt;

	BOOL mIsAlert;
public:
	NMSAlertCheck();
	NMSAlertCheck(Device& device, nms_reprot_t& report);
	~NMSAlertCheck();

public :
	VOID Init();
	VOID CheckAlert(Device* device, nms_reprot_t& report);
	BOOL CheckRouterAlert(Device* device,  nms_reprot_t& report);
	BOOL CheckUnknownDeviceAlert(Device* device,  nms_reprot_t& report);
	VOID CheckLan1Alert(Device* device,  nms_reprot_t& report);
	VOID CheckLan2Alert(Device* device,  nms_reprot_t& report);
	VOID CheckNetworkAlert(Device* device,  nms_reprot_t& report);
	VOID CheckExtPowerAlert(Device* device,  nms_reprot_t& report);
	VOID CheckRssiAlert(Device* device, nms_reprot_t& report);
	VOID CheckLowBatteryAlert(Device* device,  nms_reprot_t& report);
	VOID CheckDeviceIDAlert(Device* device,  nms_reprot_t& report);
	INT GetRssiLevel(int moduleSignal);
	BOOL IsHaveAlert();
};

