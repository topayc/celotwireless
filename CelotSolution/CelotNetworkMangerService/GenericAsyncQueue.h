#pragma once

template<class TYPE>
class GenericAsyncQueue : public  CMultiThreadSync<GenericAsyncQueue<TYPE>>
{
private:
	TYPE mQueue[MAX_QUEUE_LENGTH];
	DWORD mQueueHead;
	DWORD mQueueTail;
	int mCount;
public:
	GenericAsyncQueue()
	{
		ZeroMemory(mQueue, sizeof(mQueue));
		mQueueHead = mQueueTail = 0;
	}
	~GenericAsyncQueue(){}
public :
	BOOL Begin(VOID)
	{
		ZeroMemory(mQueue, sizeof(mQueue));
		mQueueHead = mQueueTail = 0;
		return TRUE;
	}

	BOOL End(VOID)
	{
		return true;
	}

	BOOL Push(TYPE& data)
	{
		CThreadSync sync;
		DWORD TempTail = (mQueueTail + 1) % MAX_QUEUE_LENGTH;
		if (TempTail == mQueueHead) return FALSE;
		CopyMemory(&mQueue[TempTail], &data, sizeof(TYPE));
		mQueueTail = TempTail;
		mCount++;
		return TRUE;
	}

	BOOL Pop(TYPE& data)
	{
		CThreadSync sync;
		if (mQueueHead == mQueueTail) return FALSE;
		DWORD TempHead = (mQueueHead + 1) % MAX_QUEUE_LENGTH;
		CopyMemory(&data, &mQueue[TempHead], sizeof(TYPE));
		mQueueHead = TempHead;
		mCount--;
		return TRUE;
	}

	UINT Count(){ return mCount;}

	BOOL IsEmpty(VOID)
	{
		if (mQueueHead == mQueueTail) return TRUE;
		return FALSE;
	}
};

