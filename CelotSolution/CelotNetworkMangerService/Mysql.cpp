// CMysql.cpp
#include "stdafx.h"
#include "Logger.h"
#include "Mysql.h"

Mysql::Mysql()
{
	iRowNum = 0;
	iFieldNum = 0;
	iCurrentPos = 0;
	bIsBof = false;
	bIsEof = false;
	bIsConnect = false;
	bHaveResultQuery = false;
}


Mysql::~Mysql()
{
	if (&conn != NULL)
	{
		mysql_close(&conn);
	}
}

void Mysql::ShowMySqlError()
{
	CPlusPlusLogging::Logger::getInstance()->error("[Mysql init Error]");
	CPlusPlusLogging::Logger::getInstance()->error(mysql_error(&conn));
	std::ostringstream ss;
	ss << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
	CPlusPlusLogging::Logger::getInstance()->error(ss);
}

bool Mysql::Begin(const char *host,
	const char *username,
	const char *password,
	const char *database,
	const unsigned int port,
	const char *unix_socket,
	const unsigned int client_flag)
{
	mysql_init(&conn);
	if (&conn == NULL)
	{
		ShowMySqlError();
		return FALSE;
	}
	my_bool reconnect = true;
	mysql_options(&conn, MYSQL_OPT_RECONNECT, &reconnect);
	CPlusPlusLogging::Logger::getInstance()->info("MySql option :  reconnect mode");
	if (mysql_real_connect(&conn, host, username, password, database, port, unix_socket, client_flag) == NULL)
	{
		ShowMySqlError();
		IsConnect(false);
		return false;
	}
	CPlusPlusLogging::Logger::getInstance()->info("Database Connected");
	IsConnect(true);
	return true;
}

bool Mysql::Connect(const char *host,
	const char *username,
	const char *password,
	const char *database,
	const unsigned int port,
	const char *unix_socket,
	const unsigned int client_flag)
{
	if (&conn == NULL)
	{
		ShowMySqlError();
		exit(1);
	}

	if (mysql_real_connect(&conn, host, username, password, database, port, unix_socket, client_flag) == NULL)
	{
		ShowMySqlError();
		IsConnect(false);
		return false;
	}
	IsConnect(true);
	return true;
}
void Mysql::Disconnect()
{
	if (&conn != NULL)
	{
		mysql_close(&conn);
		iRowNum = 0;
		iFieldNum = 0;
		iCurrentPos = 0;
		bIsBof = false;
		bIsEof = false;
		bIsConnect = false;
		bHaveResultQuery = false;

	}
}

bool Mysql::SelectDB(const char *szString)
{
	return !mysql_select_db(&conn, szString);
}

void Mysql::EscapeString(char * chunk, char* data, int size)
{
	mysql_real_escape_string(&conn, chunk, data, size);
}

bool Mysql::Query(const char *szString)
{
	if (&conn == NULL || !IsConnect())
	{
		ShowMySqlError();
		exit(1);
	}

	if (mysql_query(&conn, szString)) //raise error    
	{
		ShowMySqlError();
		return false;
	}
	else
	{
		pres = mysql_store_result(&conn); //����� �����ϰ�        
		if (pres)
		{            //OK,������ ������ ���� ����            
			iRowNum = mysql_num_rows(pres);
			iFieldNum = mysql_num_fields(pres);
			row = mysql_fetch_row(pres);
			pfield = mysql_fetch_fields(pres); //pfield[iFieldNum].name;            
			IsBOF(true);
			IsEOF(false);
			bHaveResultQuery = true;
			return true;
		}
		else
		{            //Update, Delete, insert query            
			if (mysql_field_count(&conn) == 0)
			{
				iRowNum = mysql_affected_rows(&conn);
				bHaveResultQuery = false;
				return true;
			}
			else
			{    //raise error                
				return false;
			}
		}
	}
	return true;
}
char * Mysql::Field(const char *szFieldName)
{    //����� �ִ� �������� ����    
	if (!bHaveResultQuery)
	{
		ShowMySqlError();
		//cout << "Exception Field() or [], ����� ���� ������ �����߽��ϴ�" << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	if (iRowNum == 0)
	{

		//cout << "Exception Field() or [], Query �� �ش��ϴ� ���ڵ���� �����ϴ�" << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}    //�ʵ��̸��� �ִ��� ���� �ִٸ� �ε����� ��������!    

	for (unsigned int i = 0; i < iFieldNum; i++)
	{
		if (strcmp(pfield[i].name, szFieldName) == 0)
		{
			return row[(int)i];
		}
	}
	return row[-1];
}
char * Mysql::Field(const my_ulonglong iFieldIndex)
{    //����� �ִ� �������� ����    
	if (!bHaveResultQuery)
	{
		ShowMySqlError();
		cout << "Exception Field() or [], ����� ���� ������ �����߽��ϴ�" << endl;
		cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	if (iRowNum == 0)
	{
		ShowMySqlError();
		cout << "Exception Field() or [], Query �� �ش��ϴ� ���ڵ���� �����ϴ�" << endl;
		cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	if (iFieldIndex > iFieldNum)
	{
		ShowMySqlError();
		cout << "Exception field() or [], �ʵ��� �ε����� �����̰ų� ������ �ѽ��ϴ�" << endl;
		cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	return row[iFieldIndex];
}

bool Mysql::Commit()
{
	return mysql_commit(&conn);
}

bool Mysql::mysqlClose()
{
	mysql_close(&conn);
	return TRUE;
}

void Mysql::First()
{
	IsBOF(true);
	IsEOF(false);
	iCurrentPos = 0;
	mysql_data_seek(pres, iCurrentPos);
	row = mysql_fetch_row(pres);
}
void Mysql::Last()
{
	IsBOF(false);
	IsEOF(true);
	iCurrentPos = iRowNum - 1;
	//row ��index �� 0 ���ʹ�
	mysql_data_seek(pres, iCurrentPos);
	row = mysql_fetch_row(pres);
}
void Mysql::Next()
{
	if (!IsEOF())
	{
		Move(1);
		if (IsBOF())
		{
			IsBOF(false); //Next() �� �ϸ� ó���� �ƴϴ�        
		}
	}
	else
	{
		ShowMySqlError();
		//cout << "[EXCEPTION] Exception Next(), ���� ���ڵ���� �����ϴ�. ";
		exit(1);
	}
}
void Mysql::Prev()
{
	if (!IsBOF())
	{
		Move(-1);
		if (IsEOF())
		{
			IsEOF(false); //Prev() �� �ϸ� ���� �ƴϴ�        
		}
	}
	else
	{
		ShowMySqlError();
		//cout << "Exception Prev(), ���� ���ڵ���� �����ϴ�." << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
}
void Mysql::Move(int n)
{    //������ �Ѵ��� ����.    
	my_ulonglong pos = iCurrentPos + n;
	if (pos == -1)
	{
		IsBOF(true);
		IsEOF(false);
	}
	else if (pos == iRowNum)
	{
		IsBOF(false);
		IsEOF(true);
	}
	else if ((pos < 0) || (pos > iRowNum))
	{
		ShowMySqlError();
		//cout << "Exception Prev() or Next() or Move(), ���ڵ�¹����� �ѽ��ϴ�" << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}

	iCurrentPos = pos;
	mysql_data_seek(pres, iCurrentPos);
	row = mysql_fetch_row(pres);
}

	void Mysql::SetCharacterSet(char* szCharSet)
	{    
		mysql_set_character_set(&conn, szCharSet);
	}

	void  Mysql::FreeResult()
	{
		if (pres != NULL) mysql_free_result(pres);
	}

	void  Mysql::Reconnect()
	{
		
	}

	bool  Mysql::Ping()
	{
		return false;
	}

	int  Mysql::Num_rows()
	{
		return mysql_num_rows(pres);
	}