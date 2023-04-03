using System;
using Cs.Logging;

namespace NKM
{
	// Token: 0x02000503 RID: 1283
	public static class NKMUtil
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x000BE5D8 File Offset: 0x000BC7D8
		public static bool IsServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000BE5DC File Offset: 0x000BC7DC
		public static bool TryParse<T>(this string data, out T @enum, bool bSkipError = false) where T : Enum
		{
			Type typeFromHandle = typeof(T);
			try
			{
				object obj = Enum.Parse(typeFromHandle, data);
				@enum = (T)((object)obj);
				return true;
			}
			catch (Exception ex)
			{
				if (!bSkipError)
				{
					Log.Error(string.Format("GetStringToEnum Fail. enumType:{0} data:{1} message:{2}", typeFromHandle, data, ex.Message), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUtil.cs", 130);
				}
			}
			@enum = default(T);
			return false;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000BE650 File Offset: 0x000BC850
		public static float NKMToRadian(float fDegree)
		{
			return fDegree * 0.017453292f;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000BE659 File Offset: 0x000BC859
		public static float NKMToDegree(float fRadian)
		{
			return fRadian * 57.295776f;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000BE662 File Offset: 0x000BC862
		public static ushort FloatToHalf(float fValue)
		{
			if (fValue > 50000f)
			{
				fValue = 50000f;
			}
			if (fValue < -50000f)
			{
				fValue = -50000f;
			}
			return HalfHelper.SingleToHalf(fValue).value;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000BE68D File Offset: 0x000BC88D
		public static void SimpleEncrypt(byte encryptSeed, ref long target, long value)
		{
			target = value + (long)((ulong)encryptSeed);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000BE695 File Offset: 0x000BC895
		public static long SimpleDecrypt(byte encryptSeed, long target)
		{
			return target - (long)((ulong)encryptSeed);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000BE69B File Offset: 0x000BC89B
		public static void SimpleEncrypt(byte encryptSeed, ref int target, int value)
		{
			target = value + (int)encryptSeed;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000BE6A2 File Offset: 0x000BC8A2
		public static int SimpleDecrypt(byte encryptSeed, int target)
		{
			return target - (int)encryptSeed;
		}
	}
}
