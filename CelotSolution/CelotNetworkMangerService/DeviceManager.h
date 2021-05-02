#pragma once

#include "Mysql.h"
#include "Device.h"
#include "NMSAlertCheck.h"
class DeviceManager :public CMultiThreadSync<DeviceManager>
{
private:
	Mysql mMySql;
	std::vector<Device *>		    mDeviceVector;
	std::map<int, Device *>		mDeviceMap;
	std::vector<int>			    	mReportedDevicePhoneNumberVector;
	NMSAlertCheck					mNmsAlertCheck;
	
public:
	DeviceManager();
	virtual ~DeviceManager();
public :
	BOOL LoadDevices();
	BOOL LoadRecentReportedPhoneNumbers();
	BOOL AddPhoneNumber(int phoneNumber);
	BOOL ProcessDevice(nms_reprot_t& report);
	BOOL IsRegisteredDevice(int phoneNumber);
	BOOL IsReportedDevice(int phoneNumber);
	BOOL GetDeviceNo(int phoneNumber);
	BOOL Begin();
	BOOL End();
	Device* GetDeivceByPhoneNumber(int phoneNumber);
	VOID DisplayNmsReport(nms_reprot_t* cmd);

	VOID InsertPacket(nms_reprot_t &report);
	VOID UpdatePacket(nms_reprot_t &report);
	VOID InsertPacketLog(nms_reprot_t &report);
	VOID UpdateDeviceAlertStatus(nms_reprot_t& report);
};
