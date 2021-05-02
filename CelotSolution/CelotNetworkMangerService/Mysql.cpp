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
		pres = mysql_store_result(&conn); //결과를 저장하고        
		if (pres)
		{            //OK,현재의 오프셋 등을 저장            
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
{    //결과가 있는 쿼리인지 조사    
	if (!bHaveResultQuery)
	{
		ShowMySqlError();
		//cout << "Exception Field() or [], 결과가 없는 쿼리를 실행했습니다" << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	if (iRowNum == 0)
	{

		//cout << "Exception Field() or [], Query 에 해당하는 레코드셋이 없습니다" << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}    //필드이름이 있는지 조사 있다면 인덱스를 리턴하자!    

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
{    //결과가 있는 쿼리인지 조사    
	if (!bHaveResultQuery)
	{
		ShowMySqlError();
		cout << "Exception Field() or [], 결과가 없는 쿼리를 실행했습니다" << endl;
		cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	if (iRowNum == 0)
	{
		ShowMySqlError();
		cout << "Exception Field() or [], Query 에 해당하는 레코드셋이 없습니다" << endl;
		cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
	if (iFieldIndex > iFieldNum)
	{
		ShowMySqlError();
		cout << "Exception field() or [], 필드의 인덱스가 음수이거나 범위를 넘습니다" << endl;
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
	//row 의index 는 0 부터다
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
			IsBOF(false); //Next() 를 하면 처음은 아니다        
		}
	}
	else
	{
		ShowMySqlError();
		//cout << "[EXCEPTION] Exception Next(), 다음 레코드셋이 없습니다. ";
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
			IsEOF(false); //Prev() 를 하면 끝은 아니다        
		}
	}
	else
	{
		ShowMySqlError();
		//cout << "Exception Prev(), 이전 레코드셋이 없습니다." << endl;
		//cout << "[Error:" << mysql_errno(&conn) << "] " << mysql_error(&conn) << endl;
		exit(1);
	}
}
void Mysql::Move(int n)
{    //범위가 넘는지 본다.    
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
		//cout << "Exception Prev() or Next() or Move(), 레코드셋범위를 넘습니다" << endl;
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