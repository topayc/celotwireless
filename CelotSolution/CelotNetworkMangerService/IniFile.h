#pragma once

class IniFile
{
public:
	IniFile(VOID);
	virtual ~IniFile(VOID);

private:
	TCHAR	mFileName[MAX_PATH];

public:
	BOOL	Open(LPCTSTR fileName);
	BOOL	Close(VOID);

	BOOL	SetValue(LPCTSTR keyName, LPCTSTR valueName, LPCTSTR value);
	BOOL	SetValue(LPCTSTR keyName, LPCTSTR valueName, DWORD value);
	BOOL	SetValue(LPCTSTR keyName, LPCTSTR valueName, FLOAT value);

	BOOL	GetValue(LPCTSTR keyName, LPCTSTR valueName, LPTSTR value, LPDWORD bufferLength);
	BOOL	GetValue(LPCTSTR keyName, LPCTSTR valueName, LPDWORD value);
	BOOL	GetValue(LPCTSTR keyName, LPCTSTR valueName, FLOAT *value);
	BOOL	GetValue(LPCTSTR keyName, LPCTSTR valueName, USHORT *value);
};
