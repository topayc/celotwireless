#include "stdafx.h"
#include "CriticalSection.h"
#include "MultiThreadSync.h"
#include "CircularQueue.h"
#include "NetworkSession.h"
#include "NmsPacketSession.h"
#include "CelotSession.h"
#include "CelotSessionManager.h"

CelotSessionManager::CelotSessionManager(VOID)
{
	mMaxSessionCount = 0;
	mCurrentSessionCount = 0;
}

CelotSessionManager::~CelotSessionManager(VOID)
{
}

BOOL CelotSessionManager::Begin(DWORD maxSessionCount, SOCKET listenSocket)
{
	CThreadSync Sync;
	if (maxSessionCount <= 0 || !listenSocket)
		return FALSE;

	mMaxSessionCount = maxSessionCount;
	mCurrentSessionCount	= 0;
	mListenSocket		= listenSocket;

	for (DWORD i = 0; i<mMaxSessionCount; i++)
	{
		CelotSession *ConnectedCelotSession = new CelotSession();
		if (ConnectedCelotSession->Begin())
			mCelotSessionVector.push_back(ConnectedCelotSession);
		else
		{
			End();
			return FALSE;
		}
	}
	return TRUE;
}

BOOL CelotSessionManager::End(VOID)
{
	CThreadSync Sync;
	for (DWORD i=0;i<mCelotSessionVector.size();i++)
	{
		CelotSession *celotSession= mCelotSessionVector[i];
		celotSession->End();
		delete celotSession;
	}
	mCelotSessionVector.clear();
	return TRUE;
}

BOOL CelotSessionManager::AcceptAll(VOID)
{
	CThreadSync Sync;
	for (DWORD i=0;i<mCelotSessionVector.size();i++)
	{
		CelotSession *celotSession= mCelotSessionVector[i];
		if (!celotSession->Accept(mListenSocket))
		{
			End();
			return FALSE;
		}
	}
	return TRUE;
}

