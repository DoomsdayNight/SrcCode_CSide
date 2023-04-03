using System;
using System.Diagnostics;
using System.Threading;

namespace Cs.Core.Core
{
	// Token: 0x020010D5 RID: 4309
	[DebuggerDisplay("value = {value}")]
	public sealed class AtomicFlag
	{
		// Token: 0x06009E24 RID: 40484 RVA: 0x0033A662 File Offset: 0x00338862
		public AtomicFlag(bool initialValue)
		{
			this.value = (initialValue ? 1 : 0);
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06009E25 RID: 40485 RVA: 0x0033A682 File Offset: 0x00338882
		public bool IsOn
		{
			get
			{
				return this.value == 1;
			}
		}

		// Token: 0x06009E26 RID: 40486 RVA: 0x0033A68F File Offset: 0x0033888F
		public bool On()
		{
			return Interlocked.CompareExchange(ref this.value, 1, 0) == 0;
		}

		// Token: 0x06009E27 RID: 40487 RVA: 0x0033A6A1 File Offset: 0x003388A1
		public bool Off()
		{
			return Interlocked.CompareExchange(ref this.value, 0, 1) == 1;
		}

		// Token: 0x0400909C RID: 37020
		private volatile int value = 1;
	}
}
