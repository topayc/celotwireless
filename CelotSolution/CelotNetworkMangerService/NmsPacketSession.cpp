#include "stdafx.h"
#include "CriticalSection.h"
#include "MultiThreadSync.h"
#include "CircularQueue.h"
#include "NetworkSession.h"
#include "NmsPacketSession.h"

NmsPacketSession::NmsPacketSession(VOID)
{
	memset(mPacketBuffer, 0, sizeof(mPacketBuffer));
	mRemainLength			= 0;
	mCurrentPacketNumber	= 0;
	mLastReadPacketNumber	= 0;
}

NmsPacketSession::~NmsPacketSession(VOID){}

BOOL NmsPacketSession::Begin(VOID)
{
	CThreadSync Sync;
	memset(mPacketBuffer, 0, sizeof(mPacketBuffer));
	mRemainLength			= 0;
	mCurrentPacketNumber	= 0;
	mLastReadPacketNumber	= 0;

	if (!mWriteQueue.Begin()){
		return FALSE;
	}
	return CNetworkSession::Begin();
}

BOOL NmsPacketSession::End(VOID)
{
	CThreadSync Sync;
	mLastReadPacketInfoVectorForUdp.clear();

	if (!mWriteQueue.End())
		return FALSE;

	return CNetworkSession::End();
}


BOOL NmsPacketSession::GetPacket(DWORD &protocol, BYTE *packet, DWORD &packetLength)
{
	CThreadSync Sync;

	if (!packet)
		return FALSE;

	if (mRemainLength < sizeof(DWORD))
		return FALSE;

	INT PacketLength = 0;
	memcpy(&PacketLength, mPacketBuffer, sizeof(INT));

	if (PacketLength > MAX_BUFFER_LENGTH || PacketLength <= 0) // Invalid Packet
	{
		mRemainLength = 0;
		return FALSE;
	}
	return FALSE;
}

BOOL NmsPacketSession::GetPacket(LPSTR remoteAddress, USHORT remotePort, DWORD &protocol, BYTE *packet, DWORD &packetLength)
{
	CThreadSync Sync;

	if (!packet)
		return FALSE;

	if (mRemainLength < sizeof(DWORD))
		return FALSE;

	INT PacketLength = 0;
	memcpy(&PacketLength, mPacketBuffer, sizeof(INT));

	if (PacketLength > MAX_BUFFER_LENGTH || PacketLength <= 0) // Invalid Packet
	{
		mRemainLength = 0;

		return FALSE;
	}
	return FALSE;
}

BOOL NmsPacketSession::GetPacket(nms_reprot_t& report)
{
	CThreadSync Sync;

	DWORD reportLength = sizeof(nms_reprot_t);
	if (mRemainLength < reportLength)  return FALSE;

	memcpy(&report, mPacketBuffer, reportLength);
	if (mRemainLength - reportLength> 0)
		memmove(mPacketBuffer, mPacketBuffer + reportLength, mRemainLength - reportLength);
	
	mRemainLength -= reportLength;
	if (mRemainLength <= 0)
	{
		mRemainLength = 0;
		memset(mPacketBuffer, 0, sizeof(mPacketBuffer));
	}
	return  TRUE;
}


// ReadPacketForIocp는 FALSE가 떨어질때 까지 while문을 돌린다.
BOOL NmsPacketSession::ReadPacketForIocp(DWORD readLength)
{
	CThreadSync Sync;

	if (!CNetworkSession::ReadForIocp(mPacketBuffer + mRemainLength, readLength))
		return FALSE;

	mRemainLength	+= readLength;
	//return getPacket(protocol, packet, packetLength);
	return TRUE;
}

// ReadPacketForEventSelect는 FALSE가 떨어질때 까지 while문을 돌린다.
BOOL NmsPacketSession::ReadPacketForEventSelect(VOID)
{
	CThreadSync Sync;

	DWORD ReadLength = 0;

	if (!CNetworkSession::ReadForEventSelect(mPacketBuffer + mRemainLength, ReadLength))
		return FALSE;

	mRemainLength	+= ReadLength;
	//return getPacket(protocol, packet, packetLength);
	return TRUE;
}

// ReadPacketForIocp는 FALSE가 떨어질때 까지 while문을 돌린다.
BOOL NmsPacketSession::ReadFromPacketForIocp(LPSTR remoteAddress, USHORT &remotePort, DWORD readLength)
{
	CThreadSync Sync;

	if (!CNetworkSession::ReadFromForIocp(remoteAddress, remotePort, mPacketBuffer + mRemainLength, readLength))
		return FALSE;

	mRemainLength	+= readLength;
	//return getPacket(remoteAddress, remotePort, protocol, packet, packetLength);
	return TRUE;
}

// ReadPacketForEventSelect는 FALSE가 떨어질때 까지 while문을 돌린다.
BOOL NmsPacketSession::ReadFromPacketForEventSelect(LPSTR remoteAddress, USHORT &remotePort)
{
	CThreadSync Sync;

	DWORD ReadLength = 0;
	if (!CNetworkSession::ReadFromForEventSelect(remoteAddress, remotePort, mPacketBuffer + mRemainLength, ReadLength))
		return FALSE;

	mRemainLength	+= ReadLength;
	//return getPacket(remoteAddress, remotePort, protocol, packet, packetLength);
	return TRUE;
}

BOOL NmsPacketSession::WritePacket(DWORD protocol, const BYTE *packet, DWORD packetLength)
{
	CThreadSync Sync;
	if (!packet)
		return FALSE;
	return TRUE;
}

BOOL NmsPacketSession::WriteComplete(VOID)
{
	CThreadSync Sync;
	// WriteQueue에서 Pop을 해 주면 된다.
	return mWriteQueue.Pop();
}

BOOL NmsPacketSession::ResetUdp(VOID)
{
	CThreadSync	Sync;
	mLastReadPacketInfoVectorForUdp.clear();
	return TRUE;
}