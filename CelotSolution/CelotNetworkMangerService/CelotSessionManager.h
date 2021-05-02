#pragma once

class CelotSession;
class CelotSessionManager : public CMultiThreadSync<CelotSessionManager>
{
public:
	CelotSessionManager(VOID);
	virtual ~CelotSessionManager(VOID);

private:
	std::vector<CelotSession*>	mCelotSessionVector;
	DWORD							    mMaxSessionCount;

	DWORD							    mCurrentSessionCount;
	
	SOCKET							    mListenSocket;

public:
	BOOL	Begin(DWORD maxUserCount, SOCKET listenSocket);
	BOOL	End(VOID);

	BOOL	AcceptAll(VOID);
	inline VOID	IncreaseConnectedSessionCount(VOID){ InterlockedIncrement((LONG*)&mCurrentSessionCount); }
	inline VOID	DecreaseConnectedSessionCount(VOID){ InterlockedDecrement((LONG*)&mCurrentSessionCount); }
};
