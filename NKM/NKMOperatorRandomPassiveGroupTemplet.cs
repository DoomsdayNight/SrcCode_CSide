using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math.Lottery;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200051D RID: 1309
	public sealed class NKMOperatorRandomPassiveGroupTemplet : INKMTemplet
	{
		// Token: 0x06002563 RID: 9571 RVA: 0x000C0C34 File Offset: 0x000BEE34
		public NKMOperatorRandomPassiveGroupTemplet(int groupId, List<NKMOperatorRandomPassiveTemplet> datas)
		{
			if (datas == null || datas.Count == 0)
			{
				Log.ErrorAndExit(string.Format("Invalid OperatorRandomPassiveGroup's groupId:{0}", groupId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorRandomPassiveGroupTemplet.cs", 21);
				return;
			}
			this.groupId = groupId;
			foreach (NKMOperatorRandomPassiveTemplet nkmoperatorRandomPassiveTemplet in datas)
			{
				this.lottery.AddCase(nkmoperatorRandomPassiveTemplet.ratio, nkmoperatorRandomPassiveTemplet);
				this.Groups.Add(nkmoperatorRandomPassiveTemplet);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x000C0CE4 File Offset: 0x000BEEE4
		public int Key
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000C0CEC File Offset: 0x000BEEEC
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000C0CF4 File Offset: 0x000BEEF4
		public static NKMOperatorRandomPassiveGroupTemplet Find(int key)
		{
			return NKMTempletContainer<NKMOperatorRandomPassiveGroupTemplet>.Find(key);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000C0CFC File Offset: 0x000BEEFC
		public NKMOperatorRandomPassiveTemplet Decide()
		{
			return this.lottery.Decide();
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000C0D09 File Offset: 0x000BEF09
		public void Join()
		{
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000C0D0B File Offset: 0x000BEF0B
		public void Validate()
		{
		}

		// Token: 0x040026B8 RID: 9912
		private readonly int groupId;

		// Token: 0x040026B9 RID: 9913
		private readonly RatioLottery<NKMOperatorRandomPassiveTemplet> lottery = new RatioLottery<NKMOperatorRandomPassiveTemplet>();

		// Token: 0x040026BA RID: 9914
		public List<NKMOperatorRandomPassiveTemplet> Groups = new List<NKMOperatorRandomPassiveTemplet>();
	}
}
