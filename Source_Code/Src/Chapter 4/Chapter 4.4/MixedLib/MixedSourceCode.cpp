#include "StdAfx.h"
#include "MixedSourceCode.h"

namespace MixedLib
{
	// ȥ��#pragma unmanaged����ѡ��󣬿�����Reflector��VS Debugger��������
	#pragma unmanaged
	int Max(int num0, int num1)
	{
		return num0 > num1 ? num0 : num1;
	}
	#pragma managed

	int MixedSourceCode::FindBiggestNumber(int num0, int num1, int num2)
	{
		return Max(Max(num0, num1), num2);
	}

}
