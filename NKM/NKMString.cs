using System;
using System.Text;
using System.Threading;

namespace NKM
{
	// Token: 0x02000504 RID: 1284
	public static class NKMString
	{
		// Token: 0x060024C8 RID: 9416 RVA: 0x000BE6A7 File Offset: 0x000BC8A7
		public static StringBuilder GetBuilder()
		{
			StringBuilder value = NKMString.perThreadBuilder.Value;
			value.Clear();
			return value;
		}

		// Token: 0x04002650 RID: 9808
		private static readonly ThreadLocal<StringBuilder> perThreadBuilder = new ThreadLocal<StringBuilder>(() => new StringBuilder());
	}
}
