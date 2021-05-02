#define NMSS_SECU_LEN 8
#define MAX_PHONE_NUM_LEN 16 

typedef unsigned int uint_32;

#pragma pack(1)
typedef struct _cg_header_t
{
	uint_32			session_id;						// phone_number(010,012���� 8�ڸ�)
	uint_32 			message_type;					// �̺�Ʈ : 1 , Report : 0
	uint_32 			data_len;							// body ����
	uint_32 			pro_ver;							//  protocol version
} cg_header_t;

typedef struct  _config_t
{
	uint_32			ethernet1_state;					// LAN ��Ʈ1 ���� ,  ��� : 1, ���� : 0
	uint_32 			ethernet2_state;					// LAN ��Ʈ 2 ���� ,  ��� : 1, ���� : 0
	uint_32 			network_state;						// Wan ���� ����,  ���� : 1, ���� : 0
	uint_32 			external_power;					// �ܺ� ���� ���� : 1, ���� :0
	uint_32 			use_rx_amount;					// ������ ��뷮(rx)
	uint_32 			use_tx_amount;					// ������ ��뷮(tx)
	char 				current_ip_address[128];		// ���� ip �ּ�		
	char				current_time[23]; 				// timestemp 	
	uint_32			moduleband;      					// band : LTE or Not
	uint_32	 		moduleservice; 					// Service : Connected or Not
	int					modulesignal;   					// RSSI Level 0 ~ -120)
	uint_32	 		devicestatus; 						// �ܺ� device ���� ����, ���:1, ����: 0, unknown:2
	uint_32	 		wifistatus; 							// WiFi ����,  On:1, Off : 0, else: not support
	uint_32	 		vpnstatus;   						// VPN ���� ���:1, ������: 0, else : not support
	int	 				newsms;								// ���ο� SMS ����, -1 : SMS not support
	char				sw_version[128];    				// firmware version
	uint_32 			rpt_time;								// report time(�ֱ⼳��,��)
	int 	   			rsrqsignal;   						// RSRQ (0 ~ 97)
	int 	   			rsrpsignal;   						// RSRP (-3 ~ -20	
	char	 			hw_version[128];					// hw version
	uint_32			rpt_port;								//  report port	
	uint_32			rmt_port;							//  remort port	
	int 				ext_device1[64];					// external device  int ��
	char				ext_device2[64];					// reserved
} config_t;

typedef struct _nms_reprot_t
{
	cg_header_t		header;
	config_t			data;
}nms_reprot_t;

#pragma pack(4)

typedef enum
{
	DSAT_NET_REG_NONE = 0,      /* Not registered, not searching */
	DSAT_NET_REG_HOME = 1,      /* Registered on home network, Connect*/
	DSAT_NET_REG_SEARCHING = 2,      /* Not registered, searching */
	DSAT_NET_REG_DENIED = 3,      /* Registration denied */
	DSAT_NET_REG_UNKNOWN = 4,      /* Unknown registration state */
	DSAT_NET_REG_ROAMING = 5,      /* Registered on roaming network */
	DSAT_REG_REGISTERED_MAX	,		         /* Internal use only! */
} data_service_type;

typedef enum
{
	MODEM_BAND_NONE = 0,
	MODEM_BNAD_CDMA = 1,
	MODEM_BAND_WCDMA = 2,
	MODEM_BNAD_LTE = 3,
} data_band_type;

typedef enum 
{
	DEVICE_STATUS_CONNECT = 0,
	DEVICE_STATUS_NO_CONNECT = 1,
} device_status_type;

enum class ethernet_status
{
	NORMAL = 0,
	ABNORMAL = 1,
} ;

enum class network_status
{
	WIRE = 0,
	WIRELESS = 1,
} ;

enum class external_power_status
{
	NORMAL = 0,
	ABNORMAL = 1,
} ;

enum class device_status
{
	WIRE = 0,
	WIRELESS = 1,
} ;

enum class wify_status
{
	OFF = 0,
	ON = 1,
};

enum class vpn_status
{
	NOT_USE = 0,
	USE = 1,
};

enum class sms_status
{
	NORMAL = 0,
	ERR = 1,
};

enum class battery_support
{
	SUPPORTED = 0,
	UNSUPPORTED = 1,
};

enum class sms_support
{
	SUPPORTED = 0,
	UNSUPPORTED = 1,
};

enum class wify_support
{
	SUPPORTED = 0,
	UNSUPPORTED = 1,
};

enum class vpn_support
{
	SUPPORTED = 0,
	UNSUPPORTED = 1,
};

enum class rssi_level
{
	LEVEL_0,  // No service 
	LEVEL_1,  // - 105 ����  
	LEVEL_2,  // -95 ~ 104 
	LEVEL_3,  // -90 ~ 94
	LEVEL_4, // -89 �̻� 
};









