using System;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003A1 RID: 929
	public class NKMAttendanceRewardTemplet
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x000621DC File Offset: 0x000603DC
		public int RewardGroup
		{
			get
			{
				return this.rewardGroup;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x000621E4 File Offset: 0x000603E4
		public int LoginDate
		{
			get
			{
				return this.loginDate;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x000621EC File Offset: 0x000603EC
		public NKM_REWARD_TYPE RewardType
		{
			get
			{
				return this.rewardType;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x000621F4 File Offset: 0x000603F4
		public int RewardID
		{
			get
			{
				return this.rewardID;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000621FC File Offset: 0x000603FC
		public string RewardStrID
		{
			get
			{
				return this.rewardStrID;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x00062204 File Offset: 0x00060404
		public int RewardValue
		{
			get
			{
				return this.rewardValue;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0006220C File Offset: 0x0006040C
		public string MailTitle
		{
			get
			{
				return this.mailTitle;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x00062214 File Offset: 0x00060414
		public string MailDesc
		{
			get
			{
				return this.mailDesc;
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0006221C File Offset: 0x0006041C
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			return true & cNKMLua.GetData("m_RewardGroup", ref this.rewardGroup) & cNKMLua.GetData("m_LoginDate", ref this.loginDate) & cNKMLua.GetData<NKM_REWARD_TYPE>("m_RewardType", ref this.rewardType) & cNKMLua.GetData("m_RewardID", ref this.rewardID) & cNKMLua.GetData("m_RewardStrID", ref this.rewardStrID) & cNKMLua.GetData("m_RewardValue", ref this.rewardValue) & cNKMLua.GetData("m_MailTitle", ref this.mailTitle) & cNKMLua.GetData("m_MailDesc", ref this.mailDesc);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000622BC File Offset: 0x000604BC
		public void Validate()
		{
			if (!NKMRewardTemplet.IsValidReward(this.rewardType, this.rewardID))
			{
				Log.ErrorAndExit(string.Format("Invalid reward data. type:{0} id:{1}", this.rewardType, this.rewardID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 203);
			}
		}

		// Token: 0x0400102A RID: 4138
		private int rewardGroup;

		// Token: 0x0400102B RID: 4139
		private int loginDate;

		// Token: 0x0400102C RID: 4140
		private NKM_REWARD_TYPE rewardType;

		// Token: 0x0400102D RID: 4141
		private int rewardID;

		// Token: 0x0400102E RID: 4142
		private string rewardStrID;

		// Token: 0x0400102F RID: 4143
		private int rewardValue;

		// Token: 0x04001030 RID: 4144
		private string mailTitle;

		// Token: 0x04001031 RID: 4145
		private string mailDesc;
	}
}
