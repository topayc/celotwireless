#pragma once
class CelotSession : public NmsPacketSession
{
public:
	CelotSession(VOID);
	virtual ~CelotSession(VOID);

private:
public:
	BOOL				Begin(VOID);
	BOOL				End(VOID);
	BOOL				Reload(SOCKET listenSocket);
};
