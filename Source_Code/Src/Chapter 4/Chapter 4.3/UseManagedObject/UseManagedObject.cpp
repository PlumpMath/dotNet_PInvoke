// UseManagedObject.cpp : main project file.

#include "stdafx.h"
#include <locale.h>
#include <conio.h>
#include "NativeStopwatchClient.h"

int main(array<System::String ^> ^args)
{
	_wsetlocale(LC_ALL, L"chs");
	
	// �����й���ʵ��
	AdvancedStopwatch^ watch = gcnew AdvancedStopwatch();
	// ����Wrapper��ʾ��
	CStopwatchWrapper wrapper(watch);

	// �������й���ʵ��
	CNativeStopwatchClient program;
	program.SetStopwatcher(wrapper);
	program.GetUserInputRecord();
	program.PrintRecord();

	wprintf(L"\n��������˳�...\n");
	_getch();

	return 0;
}
