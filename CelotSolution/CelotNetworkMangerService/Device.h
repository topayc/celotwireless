#pragma once
class Device
{
private:
	INT			mDeviceNo;
	CHAR		mGroupName[100];
	CHAR		mName[100];
	INT			mSerialNo;
	CHAR		mSecuCode[10];
	CHAR		mRouterIp[45];
	FLOAT		mlatitude;
	FLOAT		mLongitude;
	CHAR		mDes[255];
	INT			mPhoneNumber;
	INT			mSmsSupport;
	INT			mBatterySupport;
	INT			mWifiSupport;
	INT			mVpnSupport;
	INT			mResetType;
	INT			mResetTme;
	INT			mDeviceRegDate;
	INT		    mAlertStatus;
	CHAR        mAlertOccurentTime[23];

public:
	Device();
	virtual ~Device();

public :
	VOID Init();
	inline INT GetDeviceNo(){ return mDeviceNo; }
	inline void SetDeviceNo(int deviceNo){ mDeviceNo = deviceNo; }

	inline CHAR* GetGroupName(){ return mGroupName; }
	inline void SetGroupName(char* groupName){ strcpy(mGroupName, groupName);}

	inline CHAR* GetDes(){ return mDes; }
	inline void SetDes(char* des){ strcpy(mDes, des); }

	inline CHAR* GetName(){ return mName; }
	inline void SetName(char* name){ strcpy(mName, name); }

	inline INT GetSerialNo(){ return mSerialNo; }
	inline void SetSerialNo(int serialNo){ mSerialNo = serialNo; }

	inline CHAR* GetSecuCode(){ return mSecuCode; }
	inline void SetSecuCode(char* secuCode){ strcpy(mSecuCode, secuCode); }

	inline CHAR* GetRouterIp(){ return mRouterIp; }
	inline void SetRouterIp(char* routerIp){ strcpy(mRouterIp, routerIp); }

	inline float GetLatitude(){ return mlatitude; }
	inline void SetLatitude(int latitude){ mlatitude= mlatitude; }

	inline float GetLongitude(){ return mLongitude; }
	inline void SetLongitude(int longitude){ mLongitude= longitude; }

	inline int GetPhoneNumber(){ return mPhoneNumber; }
	inline void SetPhoneNumber(int phoneNumber){ mPhoneNumber= phoneNumber; }

	inline int GetSmsSupport(){ return mSmsSupport; }
	inline void SetSmsSupport(int smsSupport){ mSmsSupport = smsSupport; }

	inline int GetBatterySupport(){ return mBatterySupport; }
	inline void SetBatterySupport(int batterySupport){ mBatterySupport= batterySupport; }

	inline int GetWifiSupport(){ return mWifiSupport; }
	inline void SetWifiSupport(int wifiSupport){ mWifiSupport= wifiSupport; }

	inline int GetVpnSupport(){ return mVpnSupport; }
	inline void SetVpnSupport(int vpnSupport){ mVpnSupport= vpnSupport; }

	inline int GetResetType(){ return mResetType; }
	inline void SetResetType(int type){ mResetType= type; }

	inline int GetResetTime(){ return mResetTme; }
	inline void SetResetTime(int time){ mResetTme = time; }

	inline int GetAlertStatus(){ return mAlertStatus; }
	inline void SetAlertStatus(int status){ mAlertStatus = status; }

	inline char* GetAlertOccurentTime(){ return mAlertOccurentTime; }
	inline void SetAlertOccurentTime(char* time){ strcpy(mAlertOccurentTime, time); }
};

