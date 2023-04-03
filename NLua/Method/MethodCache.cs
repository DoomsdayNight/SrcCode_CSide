using System;
using System.Reflection;

namespace NLua.Method
{
	// Token: 0x02000076 RID: 118
	internal class MethodCache
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x0001467C File Offset: 0x0001287C
		public MethodCache()
		{
			this.args = new object[0];
			this.argTypes = new MethodArgs[0];
			this.outList = new int[0];
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000146A8 File Offset: 0x000128A8
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x000146B0 File Offset: 0x000128B0
		public MethodBase cachedMethod
		{
			get
			{
				return this._cachedMethod;
			}
			set
			{
				this._cachedMethod = value;
				MethodInfo methodInfo = value as MethodInfo;
				if (methodInfo != null)
				{
					this.IsReturnVoid = (methodInfo.ReturnType == typeof(void));
				}
			}
		}

		// Token: 0x04000255 RID: 597
		private MethodBase _cachedMethod;

		// Token: 0x04000256 RID: 598
		public bool IsReturnVoid;

		// Token: 0x04000257 RID: 599
		public object[] args;

		// Token: 0x04000258 RID: 600
		public int[] outList;

		// Token: 0x04000259 RID: 601
		public MethodArgs[] argTypes;
	}
}
