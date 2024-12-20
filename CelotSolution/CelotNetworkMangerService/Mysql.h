#pragma once
#include <winsock2.h>
#include <iostream>
#include <mysql.h>
#pragma comment(lib, "libmysql.lib")
#pragma warning(disable:4996)

using namespace std;

class Mysql
{
	MYSQL       conn;
	MYSQL_RES   *pres;
	MYSQL_ROW   row;
	MYSQL_FIELD *pfield;
	my_ulonglong iRowNum;               //레코드셋의 개수  
	unsigned int iFieldNum;                 //필드의 개수   
	my_ulonglong iCurrentPos;             //현재 row 의 offset   
	bool bHaveResultQuery;                 //결과가 있는 쿼리인가요? 
	bool bIsEof;                                 //레코드셋의 처음인가요?  
	bool bIsBof;								//레코드셋의 마지막인가요? 
	bool bIsConnect;							//연결이 되어 있는가?

public:
	Mysql();
	virtual ~Mysql();
	bool Begin(const char *host = NULL,
		const char *username = NULL,
		const char *password = NULL,
		const char *database = NULL,
		const unsigned int port = 0,
		const char *unix_socket = NULL,
		const unsigned int client_flag = 0);

	bool Connect(const char *host = NULL,
		const char *username = NULL,
		const char *password = NULL,
		const char *database = NULL,
		const unsigned int port = 0,
		const char *unix_socket = NULL,
		const unsigned int client_flag = 0);
	void Disconnect();
	void EscapeString(char * chunk, char* data, int size);
	bool IsConnect() { return bIsConnect; }
	void IsConnect(bool b) { bIsConnect = b; }
	bool Query(const char *szString);
	bool SelectDB(const char *szString);    //result navigation 
	bool IsBOF() { return bIsBof; }
	void IsBOF(bool b) { bIsBof = b; }
	bool IsEOF() { return bIsEof; }
	void IsEOF(bool b) { bIsEof = b; }
	void First();
	void Last();
	void Next();
	void Prev();
	void Move(int n = 1);
	void SetCharacterSet(char* szCharSet);
	bool Commit();
	bool mysqlClose();
	void FreeResult();
	void Reconnect();
	bool Ping();
	char * Field(const char *szFieldName);
	char * Field(const my_ulonglong iFieldIndex);   //operator  
	char * operator[](const my_ulonglong iFieldIndex) { return Field(iFieldIndex); }
	char * operator[](const char *szFieldName) { return Field(szFieldName); }
	void ShowMySqlError();
	int  Num_rows();
};

