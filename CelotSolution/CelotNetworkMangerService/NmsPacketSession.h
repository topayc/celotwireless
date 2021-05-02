#pragma once

typedef struct _READ_PACKET_INFO
{
	CHAR	RemoteAddress[14];
	USHORT	RemotePort;
	DWORD	PacketNumber;
} READ_PACKET_INFO;

class NmsPacketSession : public CNetworkSession
{
public:
	NmsPacketSession(VOID);
	virtual ~NmsPacketSession(VOID);

private:
	BYTE						mPacketBuffer[MAX_BUFFER_LENGTH * 3];
	INT						mRemainLength;
	DWORD					mCurrentPacketNumber;
	DWORD					mLastReadPacketNumber;
	CCircularQueue		mWriteQueue;

	//std::map<std::string, DWORD>		mLastReadPacketInfoMapForUdp;
	std::vector<READ_PACKET_INFO>		mLastReadPacketInfoVectorForUdp;

public:
	BOOL	Begin(VOID);
	BOOL	End(VOID);

	BOOL	ReadPacketForIocp(DWORD readLength);
	BOOL	ReadPacketForEventSelect(VOID);

	BOOL	ReadFromPacketForIocp(LPSTR remoteAddress, USHORT &remotePort, DWORD readLength);
	BOOL	ReadFromPacketForEventSelect(LPSTR remoteAddress, USHORT &remotePort);

	BOOL	WritePacket(DWORD protocol, const BYTE *packet, DWORD packetLength);
	BOOL	WriteToPacket(LPCSTR remoteAddress, USHORT remotePort, DWORD protocol, const BYTE *packet, DWORD packetLength);
	BOOL	WriteComplete(VOID);

	BOOL	ResetUdp(VOID);

	BOOL	GetPacket(DWORD &protocol, BYTE *packet, DWORD &packetLength);
	BOOL	GetPacket(LPSTR remoteAddress, USHORT remotePort, DWORD &protocol, BYTE *packet, DWORD &packetLength);

	//Get Celot Nms Packet 
	BOOL GetPacket(nms_reprot_t& nms);
};
