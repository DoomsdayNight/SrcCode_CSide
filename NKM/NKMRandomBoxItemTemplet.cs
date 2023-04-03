using System;
using Cs.Math;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200045E RID: 1118
	public sealed class NKMRandomBoxItemTemplet : INKMTemplet
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0008FB92 File Offset: 0x0008DD92
		public int TotalQuantity_Min
		{
			get
			{
				return this.FreeQuantity_Min + this.PaidQuantity_Min;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0008FBA1 File Offset: 0x0008DDA1
		public int TotalQuantity_Max
		{
			get
			{
				return this.FreeQuantity_Max + this.PaidQuantity_Max;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x0008FBB0 File Offset: 0x0008DDB0
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0008FBBD File Offset: 0x0008DDBD
		public int Key
		{
			get
			{
				return this.m_RewardGroupID;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x0008FBC5 File Offset: 0x0008DDC5
		public int GetRandomEquipExp
		{
			get
			{
				return RandomGenerator.Range(this.EquipExp_Min, this.EquipExp_Max);
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0008FBD8 File Offset: 0x0008DDD8
		public static NKMRandomBoxItemTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomBoxItemTemplet.cs", 31))
			{
				return null;
			}
			NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = new NKMRandomBoxItemTemplet();
			bool flag = true & cNKMLua.GetData("m_RewardGroupID", ref nkmrandomBoxItemTemplet.m_RewardGroupID) & cNKMLua.GetData<NKM_REWARD_TYPE>("m_RewardType", ref nkmrandomBoxItemTemplet.m_reward_type) & cNKMLua.GetData("m_RewardID", ref nkmrandomBoxItemTemplet.m_RewardID) & cNKMLua.GetData("m_Ratio", ref nkmrandomBoxItemTemplet.m_Ratio);
			cNKMLua.GetData("m_FreeQuantity_Min", ref nkmrandomBoxItemTemplet.FreeQuantity_Min);
			cNKMLua.GetData("m_FreeQuantity_Max", ref nkmrandomBoxItemTemplet.FreeQuantity_Max);
			cNKMLua.GetData("m_PaidQuantity_Min", ref nkmrandomBoxItemTemplet.PaidQuantity_Min);
			cNKMLua.GetData("m_PaidQuantity_Max", ref nkmrandomBoxItemTemplet.PaidQuantity_Max);
			cNKMLua.GetData("m_OrderList", ref nkmrandomBoxItemTemplet.m_OrderList);
			cNKMLua.GetData("m_OpenTag", ref nkmrandomBoxItemTemplet.m_OpenTag);
			cNKMLua.GetData("m_EquipExp_Min", ref nkmrandomBoxItemTemplet.EquipExp_Min);
			cNKMLua.GetData("m_EquipExp_Max", ref nkmrandomBoxItemTemplet.EquipExp_Max);
			if (!flag)
			{
				return null;
			}
			return nkmrandomBoxItemTemplet;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0008FCDC File Offset: 0x0008DEDC
		public override string ToString()
		{
			return string.Format("rewardType:{0} rewardId:{1} freeRewardCount:{2}~{3} paidRewardCount:{4}~{5}", new object[]
			{
				this.m_reward_type,
				this.m_RewardID,
				this.FreeQuantity_Min,
				this.FreeQuantity_Max,
				this.PaidQuantity_Min,
				this.PaidQuantity_Max
			});
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0008FD4D File Offset: 0x0008DF4D
		public void Join()
		{
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0008FD50 File Offset: 0x0008DF50
		public void Validate()
		{
			if (!NKMRewardTemplet.IsValidReward(this.m_reward_type, this.m_RewardID))
			{
				NKMTempletError.Add(string.Format("[RandomBoxItem] 보상 정보가 존재하지 않음 m_RewardGroupID:{0} m_reward_type:{1} m_RewardID:{2}", this.m_RewardGroupID, this.m_reward_type, this.m_RewardID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomBoxItemTemplet.cs", 73);
			}
			if (this.m_Ratio <= 0)
			{
				NKMTempletError.Add(string.Format("[RandomBoxItem] m_Ratio 는 0보다 커야 함. groupId:{0} rewardId:{1}", this.m_RewardGroupID, this.m_RewardID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomBoxItemTemplet.cs", 78);
			}
			if (this.m_reward_type == NKM_REWARD_TYPE.RT_MISC)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(this.m_RewardID);
				if (nkmitemMiscTemplet != null && nkmitemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE)
				{
					NKMTempletError.Add(string.Format("[RandomBoxItem] 아이템 그룹에 커스텀패키지 유형은 담을 수 없음. groupId:{0} rewardType:{1} rewardId:{2}", this.m_RewardGroupID, this.m_reward_type, this.m_RewardID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomBoxItemTemplet.cs", 86);
				}
				if (this.FreeQuantity_Min == 0 && this.FreeQuantity_Max == 0 && this.PaidQuantity_Min == 0 && this.PaidQuantity_Max == 0)
				{
					NKMTempletError.Add(string.Format("[RandomBoxItem] 보상 범위 설정 오류. groupId:{0} FreeQuantity_Min:{1} FreeQuantity_Max:{2} PaidQuantity_Min:{3} PaidQuantity_Max:{4}", new object[]
					{
						this.m_RewardGroupID,
						this.FreeQuantity_Min,
						this.FreeQuantity_Max,
						this.PaidQuantity_Min,
						this.PaidQuantity_Max
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomBoxItemTemplet.cs", 91);
				}
			}
			if (this.EquipExp_Max != 0 && this.EquipExp_Min != 0 && (this.EquipExp_Max < this.EquipExp_Min || this.EquipExp_Max < 0 || this.EquipExp_Min < 0))
			{
				NKMTempletError.Add(string.Format("[RandomBoxItem] 장비 획득 랜덤 경험치 이상. groupId:{0} EquipExpMin:{1} EquipExpMax{2}", this.m_RewardGroupID, this.EquipExp_Min, this.EquipExp_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomBoxItemTemplet.cs", 106);
			}
		}

		// Token: 0x04001EED RID: 7917
		private int EquipExp_Min;

		// Token: 0x04001EEE RID: 7918
		private int EquipExp_Max;

		// Token: 0x04001EEF RID: 7919
		public int m_RewardGroupID;

		// Token: 0x04001EF0 RID: 7920
		public NKM_REWARD_TYPE m_reward_type;

		// Token: 0x04001EF1 RID: 7921
		public int m_RewardID;

		// Token: 0x04001EF2 RID: 7922
		public int m_Ratio;

		// Token: 0x04001EF3 RID: 7923
		public int FreeQuantity_Min;

		// Token: 0x04001EF4 RID: 7924
		public int FreeQuantity_Max;

		// Token: 0x04001EF5 RID: 7925
		public int PaidQuantity_Min;

		// Token: 0x04001EF6 RID: 7926
		public int PaidQuantity_Max;

		// Token: 0x04001EF7 RID: 7927
		public int m_OrderList;

		// Token: 0x04001EF8 RID: 7928
		public string m_OpenTag;
	}
}
