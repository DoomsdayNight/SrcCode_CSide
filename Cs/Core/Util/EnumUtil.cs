using System;
using System.Collections.Generic;
using System.Linq;

namespace Cs.Core.Util
{
	// Token: 0x020010D9 RID: 4313
	public static class EnumUtil<T> where T : Enum
	{
		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06009E3A RID: 40506 RVA: 0x0033A8B8 File Offset: 0x00338AB8
		public static int Count
		{
			get
			{
				return Enum.GetNames(typeof(T)).Length;
			}
		}

		// Token: 0x06009E3B RID: 40507 RVA: 0x0033A8CB File Offset: 0x00338ACB
		public static IEnumerable<T> GetValues()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}
	}
}
