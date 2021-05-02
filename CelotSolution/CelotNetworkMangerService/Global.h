#pragma once

#include <iostream>
#include <tchar.h>
#include <stdio.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <mswsock.h>
#include <windows.h>
#include <dbghelp.h>
#include <assert.h>
#include <time.h>

#include <vector>
#include <queue>
#include <list>
#include <map>
#include <string>
#include <string.h>
#include <utility>

enum IO_TYPE
{
	IO_ACCEPT,
	IO_READ,
	IO_WRITE
};

typedef struct _OVERLAPPED_EX
{
	OVERLAPPED	Overlapped;
	IO_TYPE			IoType;
	VOID				*Object;
} OVERLAPPED_EX;

#define MAX_BUFFER_LENGTH	1000
#define MAX_QUEUE_LENGTH	150
