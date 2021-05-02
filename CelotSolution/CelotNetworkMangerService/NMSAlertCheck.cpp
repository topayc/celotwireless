#include "stdafx.h"
#include  "Device.h"
#include "NMSAlertCheck.h"

extern DWORD LOW_BATTERY_LIMIT;

NMSAlertCheck::NMSAlertCheck()
{
	Init();
}

NMSAlertCheck::NMSAlertCheck(Device& device, nms_reprot_t& report)
{
	Init();
}


NMSAlertCheck::~NMSAlertCheck()
{
}

VOID NMSAlertCheck::Init()
{
	mRouterErrCnt = 0;
	mUnknownDeviceErrCnt = 0;
	mLan1ErrCnt = 0;
	mLan2ErrCnt = 0;
	mNetworkErrCnt = 0;
	mExtPowerErrCnt = 0;
	mRssiLevelErrCnt = 0;
	mLowBatteryErrCnt = 0;
	mDeviceIdErrCnt = 0;
	mIsAlert = FALSE;
}

VOID NMSAlertCheck::CheckAlert(Device* device,  nms_reprot_t& report)
{
	CheckLan1Alert(device,report);
	CheckLan2Alert(device, report);
	CheckExtPowerAlert(device, report);
	CheckRssiAlert(device, report);
	CheckLowBatteryAlert(device, report);
	CheckDeviceIDAlert(device, report);
}

/*
사용하지 않음, 구현 보류
*/
BOOL NMSAlertCheck::CheckRouterAlert(Device* device,  nms_reprot_t& report)
{
}

/*
사용하지 않음, 구현 보류
*/
BOOL NMSAlertCheck::CheckUnknownDeviceAlert(Device* device,  nms_reprot_t& report)
{
}

VOID NMSAlertCheck::CheckLan1Alert(Device* device,  nms_reprot_t& report)
{
	ethernet_status status = (ethernet_status)report.data.ethernet1_state;
	if (status == ethernet_status::ABNORMAL){
		mLan1ErrCnt++;
		mIsAlert = TRUE;
	}
}

VOID NMSAlertCheck::CheckLan2Alert(Device* device,  nms_reprot_t& report)
{
	ethernet_status status = (ethernet_status)report.data.ethernet2_state;
	if (status == ethernet_status::ABNORMAL){
		mLan2ErrCnt++;
		mIsAlert = TRUE;
	}
}

/*
내용 불분명으로 인하여 구현 보류 
*/
VOID NMSAlertCheck::CheckNetworkAlert(Device* device,  nms_reprot_t& report)
{
}

VOID NMSAlertCheck::CheckExtPowerAlert(Device* device, nms_reprot_t& report)
{
	external_power_status status = (external_power_status)report.data.external_power;
	if (status == external_power_status::ABNORMAL){
		mExtPowerErrCnt++;
		mIsAlert = TRUE;
	}
}

VOID NMSAlertCheck::CheckRssiAlert(Device* device, nms_reprot_t& report)
{
	int rssiLevel = GetRssiLevel(report.data.modulesignal);
	if (rssiLevel <= (int)rssi_level::LEVEL_1)
	{
		mRssiLevelErrCnt++;
		mIsAlert = TRUE;
	}
}

VOID NMSAlertCheck::CheckLowBatteryAlert(Device* device, nms_reprot_t& report)
{
	int battery = report.data.ext_device1[0];
	if (battery <= LOW_BATTERY_LIMIT)
	{
		mLowBatteryErrCnt++;
		mIsAlert = TRUE;
	}
}

VOID NMSAlertCheck::CheckDeviceIDAlert(Device* device, nms_reprot_t& report)
{
	BOOL deviceIdError = FALSE;
	char lanIp[128] = { 0, };
	char *sp, *dp;
	sp = strchr(report.data.current_ip_address, '(');
	dp = strrchr(report.data.current_ip_address, ')');
	strncpy(lanIp, sp + 1, dp - sp - 1);

	/*아이피 불일치 조사*/ 
	if (strcmp(device->GetRouterIp(), lanIp) != 0)
	{
		mDeviceIdErrCnt++;
		mIsAlert = TRUE;
	}

	//SMS 상태 불일지 조사
	if (device->GetSmsSupport() == 0)
	{
		if (report.data.newsms < 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
		}
	}
	else
	{
		if (report.data.newsms > 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
		}
	}

	//와이파이 상태 불일치 조사
	if (device->GetWifiSupport() == 0)
	{
		if (report.data.wifistatus != 1 && report.data.wifistatus != 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
			return;
		}
	}
	else
	{
		if (report.data.wifistatus == 1 || report.data.wifistatus == 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
			return;
		}
	}

	//VPN 상태 불일치 조사
	if (device->GetVpnSupport() == 0)
	{
		if (report.data.vpnstatus != 1 && report.data.vpnstatus != 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
			return;
		}
	}
	else
	{
		if (report.data.vpnstatus == 1 || report.data.vpnstatus == 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
			return;
		}
	}

	//배터리 상태 불일치 조사
	/*
	if ((battery_support)device.GetBatterySupport() == battery_support::SUPPORTED)
	{
		if (report.data.ext_device1[0] < 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
			return;
		}
	}
	else
	{
		if (report.data.ext_device1[0] > 0)
		{
			mDeviceIdErrCnt++;
			mIsAlert = TRUE;
			return;
		}
	}
	*/
}

INT NMSAlertCheck::GetRssiLevel(int moduleSignal)
{
	int level = 0;
	if (moduleSignal <= -105) level = 1;
	if (moduleSignal >= -105 && moduleSignal <= -95) level = 2;
	if (moduleSignal >= -94 && moduleSignal <= -90) level = 3;
	if (moduleSignal >= -89) level = 4;
	return level;
}

BOOL NMSAlertCheck::IsHaveAlert()
{
	return mIsAlert;
}