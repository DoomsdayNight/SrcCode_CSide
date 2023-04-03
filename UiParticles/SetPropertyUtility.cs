using System;
using UnityEngine;

namespace UiParticles
{
	// Token: 0x02000031 RID: 49
	internal static class SetPropertyUtility
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00007504 File Offset: 0x00005704
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007553 File Offset: 0x00005753
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007574 File Offset: 0x00005774
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
