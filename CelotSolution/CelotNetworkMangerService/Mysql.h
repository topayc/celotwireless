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
	my_ulonglong iRowNum;               //���ڵ���� ����  
	unsigned int iFieldNum;                 //�ʵ��� ����   
	my_ulonglong iCurrentPos;             //���� row �� offset   
	bool bHaveResultQuery;                 //����� �ִ� �����ΰ���? 
	bool bIsEof;                                 //���ڵ���� ó���ΰ���?  
	bool bIsBof;								//���ڵ���� �������ΰ���? 
	bool bIsConnect;							//������ �Ǿ� �ִ°�?

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

