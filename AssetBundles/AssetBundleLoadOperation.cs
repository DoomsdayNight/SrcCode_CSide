using System;
using System.Collections;

namespace AssetBundles
{
	// Token: 0x0200004C RID: 76
	public abstract class AssetBundleLoadOperation : IEnumerator
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A5DA File Offset: 0x000087DA
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A5DD File Offset: 0x000087DD
		public bool MoveNext()
		{
			return !this.IsDone();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public void Reset()
		{
		}

		// Token: 0x06000253 RID: 595
		public abstract bool Update();

		// Token: 0x06000254 RID: 596
		public abstract bool IsDone();
	}
}
