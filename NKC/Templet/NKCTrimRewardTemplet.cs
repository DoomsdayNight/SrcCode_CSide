using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC.Templet
{
	// Token: 0x0200085C RID: 2140
	public class NKCTrimRewardTemplet
	{
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x0019D7B1 File Offset: 0x0019B9B1
		public NKM_REWARD_TYPE FirstClearRewardType
		{
			get
			{
				return this.firstClearRewardType;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x0019D7B9 File Offset: 0x0019B9B9
		public int FirstClearRewardID
		{
			get
			{
				return this.firstClearRewardID;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x0019D7C1 File Offset: 0x0019B9C1
		public int FirstClearValue
		{
			get
			{
				return this.firstClearValue;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060054F6 RID: 21750 RVA: 0x0019D7C9 File Offset: 0x0019B9C9
		public NKM_REWARD_TYPE FixRewardType
		{
			get
			{
				return this.fixRewardType;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060054F7 RID: 21751 RVA: 0x0019D7D1 File Offset: 0x0019B9D1
		public int FixRewardID
		{
			get
			{
				return this.fixRewardID;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x0019D7D9 File Offset: 0x0019B9D9
		public int RewardCreditMin
		{
			get
			{
				return this.rewardCreditMin;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x0019D7E1 File Offset: 0x0019B9E1
		public List<int> RewardGroupID
		{
			get
			{
				return this.rewardGroupID;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x0019D7E9 File Offset: 0x0019B9E9
		public List<int> RewardUnitExp
		{
			get
			{
				return this.rewardUnitExp;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060054FB RID: 21755 RVA: 0x0019D7F1 File Offset: 0x0019B9F1
		public int RewardUserExp
		{
			get
			{
				return this.rewardUserExp;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x060054FC RID: 21756 RVA: 0x0019D7F9 File Offset: 0x0019B9F9
		public List<int> EventDropIndex
		{
			get
			{
				return this.eventDropIndex;
			}
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0019D801 File Offset: 0x0019BA01
		public static NKCTrimRewardTemplet Find(int trimId, int trimLevel)
		{
			if (!NKCTrimRewardTemplet.TrimRewardList.ContainsKey(trimId))
			{
				return null;
			}
			if (!NKCTrimRewardTemplet.TrimRewardList[trimId].ContainsKey(trimLevel))
			{
				return null;
			}
			return NKCTrimRewardTemplet.TrimRewardList[trimId][trimLevel];
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0019D838 File Offset: 0x0019BA38
		public static bool Load(string assetName, string fileName)
		{
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath(assetName, fileName, true))
				{
					return false;
				}
				if (!nkmlua.OpenTable("TRIM_REWARD_TEMPLET"))
				{
					return false;
				}
				int num = 1;
				while (nkmlua.OpenTable(num++))
				{
					NKCTrimRewardTemplet nkctrimRewardTemplet = new NKCTrimRewardTemplet();
					if (!nkctrimRewardTemplet.LoadFromLua(nkmlua))
					{
						nkmlua.CloseTable();
					}
					else
					{
						if (!NKCTrimRewardTemplet.TrimRewardList.ContainsKey(nkctrimRewardTemplet.trimID))
						{
							NKCTrimRewardTemplet.TrimRewardList.Add(nkctrimRewardTemplet.trimID, new Dictionary<int, NKCTrimRewardTemplet>());
						}
						if (!NKCTrimRewardTemplet.TrimRewardList[nkctrimRewardTemplet.trimID].ContainsKey(nkctrimRewardTemplet.trimLevel))
						{
							NKCTrimRewardTemplet.TrimRewardList[nkctrimRewardTemplet.trimID].Add(nkctrimRewardTemplet.trimLevel, nkctrimRewardTemplet);
						}
						nkmlua.CloseTable();
					}
				}
				nkmlua.CloseTable();
			}
			return true;
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0019D92C File Offset: 0x0019BB2C
		private bool LoadFromLua(NKMLua cNKMLua)
		{
			bool flag = true;
			flag &= cNKMLua.GetData("TrimID", ref this.trimID);
			flag &= cNKMLua.GetData("TrimLevel", ref this.trimLevel);
			cNKMLua.GetData<NKM_REWARD_TYPE>("FirstClearRewardType", ref this.firstClearRewardType);
			cNKMLua.GetData("FirstClearRewardID", ref this.firstClearRewardID);
			cNKMLua.GetData("FirstClearRewardValue", ref this.firstClearValue);
			cNKMLua.GetData("RewardCountPoint", ref this.rewardCountPoint);
			cNKMLua.GetData<NKM_REWARD_TYPE>("FirstClearRewardType", ref this.fixRewardType);
			cNKMLua.GetData("FixRewardID", ref this.fixRewardID);
			cNKMLua.GetData("m_RewardCredit_Min", ref this.rewardCreditMin);
			int num = 1;
			int item = 0;
			while (cNKMLua.GetData(string.Format("m_RewardGroupID_{0}", num++), ref item))
			{
				this.rewardGroupID.Add(item);
			}
			int num2 = 1;
			int item2 = 0;
			while (cNKMLua.GetData(string.Format("m_RewardUnitExp{0}", num2++), ref item2))
			{
				this.rewardUnitExp.Add(item2);
			}
			cNKMLua.GetData("m_RewardUserEXP", ref this.rewardUserExp);
			int num3 = 1;
			int item3 = 0;
			while (cNKMLua.GetData(string.Format("m_EventDropIndex_{0}", num3++), ref item3))
			{
				this.eventDropIndex.Add(item3);
			}
			return flag;
		}

		// Token: 0x040043DB RID: 17371
		public static readonly Dictionary<int, Dictionary<int, NKCTrimRewardTemplet>> TrimRewardList = new Dictionary<int, Dictionary<int, NKCTrimRewardTemplet>>();

		// Token: 0x040043DC RID: 17372
		private int trimID;

		// Token: 0x040043DD RID: 17373
		private int trimLevel;

		// Token: 0x040043DE RID: 17374
		private NKM_REWARD_TYPE firstClearRewardType;

		// Token: 0x040043DF RID: 17375
		private int firstClearRewardID;

		// Token: 0x040043E0 RID: 17376
		private int firstClearValue;

		// Token: 0x040043E1 RID: 17377
		private int rewardCountPoint;

		// Token: 0x040043E2 RID: 17378
		private NKM_REWARD_TYPE fixRewardType;

		// Token: 0x040043E3 RID: 17379
		private int fixRewardID;

		// Token: 0x040043E4 RID: 17380
		private int rewardCreditMin;

		// Token: 0x040043E5 RID: 17381
		private List<int> rewardGroupID = new List<int>();

		// Token: 0x040043E6 RID: 17382
		private List<int> rewardUnitExp = new List<int>();

		// Token: 0x040043E7 RID: 17383
		private int rewardUserExp;

		// Token: 0x040043E8 RID: 17384
		private List<int> eventDropIndex = new List<int>();
	}
}
