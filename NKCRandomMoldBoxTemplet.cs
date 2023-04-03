using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000529 RID: 1321
	public class NKCRandomMoldBoxTemplet : INKMTemplet
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x000C2188 File Offset: 0x000C0388
		public int Key
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000C2190 File Offset: 0x000C0390
		public static NKCRandomMoldBoxTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 175))
			{
				return null;
			}
			NKCRandomMoldBoxTemplet nkcrandomMoldBoxTemplet = new NKCRandomMoldBoxTemplet();
			if (!(true & cNKMLua.GetData("IDX", ref nkcrandomMoldBoxTemplet.index) & cNKMLua.GetData("m_RewardGroupID", ref nkcrandomMoldBoxTemplet.m_RewardGroupID) & cNKMLua.GetData<NKM_REWARD_TYPE>("m_RewardType", ref nkcrandomMoldBoxTemplet.m_reward_type) & cNKMLua.GetData("m_RewardID", ref nkcrandomMoldBoxTemplet.m_RewardID)))
			{
				Log.ErrorAndExit(string.Format("NKCRandomMoldBoxTemplet 정보를 읽어오지 못하였습니다. index : {0}", nkcrandomMoldBoxTemplet.index), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 187);
			}
			nkcrandomMoldBoxTemplet.CheckValidation();
			return nkcrandomMoldBoxTemplet;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000C222D File Offset: 0x000C042D
		public void Join()
		{
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000C222F File Offset: 0x000C042F
		public void Validate()
		{
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000C2234 File Offset: 0x000C0434
		private void CheckValidation()
		{
			if (this.m_reward_type == NKM_REWARD_TYPE.RT_EQUIP && NKMItemManager.GetEquipTemplet(this.m_RewardID) == null)
			{
				Log.ErrorAndExit(string.Format("[RandomMoldBox] 보상 정보가 존재하지 않음 m_RewardGroupID : {0}, m_reward_type : {1}, m_RewardID : {2}", this.m_RewardGroupID, this.m_reward_type, this.m_RewardID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 213);
			}
		}

		// Token: 0x0400272C RID: 10028
		public int index;

		// Token: 0x0400272D RID: 10029
		public int m_RewardGroupID;

		// Token: 0x0400272E RID: 10030
		public NKM_REWARD_TYPE m_reward_type;

		// Token: 0x0400272F RID: 10031
		public int m_RewardID;

		// Token: 0x04002730 RID: 10032
		public Dictionary<int, int> m_dicRewardGroup = new Dictionary<int, int>();
	}
}
