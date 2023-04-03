using System;
using System.ComponentModel;

namespace NKC
{
	// Token: 0x020006EF RID: 1775
	public static class EnumsHelperExtension
	{
		// Token: 0x06003F1D RID: 16157 RVA: 0x00148008 File Offset: 0x00146208
		public static string ToDescription(this Enum value)
		{
			DescriptionAttribute[] array = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length == 0)
			{
				return value.ToString();
			}
			return array[0].Description;
		}
	}
}
