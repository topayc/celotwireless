#include "stdafx.h"
#include "CriticalSection.h"
#include "MultiThreadSync.h"
#include "NetworkSession.h"
#include "NmsPacketSession.h"
#include "CelotSession.h"

CelotSession::CelotSession(VOID)
{
}

CelotSession::~CelotSession(VOID)
{
}

BOOL CelotSession::Begin(VOID)
{
	CThreadSync Sync;
	return NmsPacketSession::Begin();
}

BOOL CelotSession::End(VOID)
{
	CThreadSync Sync;
	return NmsPacketSession::End();
}

BOOL CelotSession::Reload(SOCKET listenSocket)
{
	CThreadSync Sync;
	End();

	if (!Begin()) return FALSE;
	if (!CNetworkSession::Accept(listenSocket)) return FALSE;
	return TRUE;
}