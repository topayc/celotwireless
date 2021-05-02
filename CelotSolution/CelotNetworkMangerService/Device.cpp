#include "stdafx.h"
#include "Device.h"


Device::Device()
{
}


Device::~Device()
{
}

VOID Device::Init()
{
	mDeviceNo = 0;
	::ZeroMemory(mGroupName, sizeof(mGroupName));
	::ZeroMemory(mName, sizeof(mName));
	mSerialNo = 0;
	::ZeroMemory(mSecuCode, sizeof(mSecuCode));
	::ZeroMemory(mRouterIp, sizeof(mRouterIp));
	mlatitude = 0;
	mLongitude = 0;
	::ZeroMemory(mDes, sizeof(mDes));
	mPhoneNumber = 0;
	mSmsSupport = 0;
	mBatterySupport = 0;
	mWifiSupport = 0;
	mVpnSupport = 0;
	mResetType = 0;
	mResetTme = 0;
	mDeviceRegDate = 0;
	mAlertStatus == 0;
	::ZeroMemory(mAlertOccurentTime, sizeof(mAlertOccurentTime));
}
