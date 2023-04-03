using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000387 RID: 903
	public class NKMItemMoldTemplet : INKMTemplet, INKMTempletEx
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0005BB3C File Offset: 0x00059D3C
		public int Key
		{
			get
			{
				return this.m_MoldID;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0005BB44 File Offset: 0x00059D44
		public static IEnumerable<NKMItemMoldTemplet> Values
		{
			get
			{
				return NKMTempletContainer<NKMItemMoldTemplet>.Values;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005BB4B File Offset: 0x00059D4B
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0005BB58 File Offset: 0x00059D58
		public bool HasDateLimit
		{
			get
			{
				return this.IntervalTemplet.IsValid;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0005BB65 File Offset: 0x00059D65
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x0005BB6D File Offset: 0x00059D6D
		public NKMIntervalTemplet IntervalTemplet { get; private set; } = NKMIntervalTemplet.Invalid;

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005BB78 File Offset: 0x00059D78
		public bool IsEquipMold
		{
			get
			{
				NKM_CRAFT_TAB_TYPE tabType = this.m_TabType;
				return tabType == NKM_CRAFT_TAB_TYPE.MT_EQUIP || tabType - NKM_CRAFT_TAB_TYPE.MT_EQUIP_PRIVATE <= 2;
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0005BB98 File Offset: 0x00059D98
		public static NKMItemMoldTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 250))
			{
				return null;
			}
			NKMItemMoldTemplet nkmitemMoldTemplet = new NKMItemMoldTemplet();
			bool flag = true;
			flag &= lua.GetData("m_MoldID", ref nkmitemMoldTemplet.m_MoldID);
			flag &= lua.GetData("m_MoldStrID", ref nkmitemMoldTemplet.m_MoldStrID);
			flag &= lua.GetData("m_RewardGroupID", ref nkmitemMoldTemplet.m_RewardGroupID);
			flag &= lua.GetData<NKM_CRAFT_TAB_TYPE>("m_MoldTabID", ref nkmitemMoldTemplet.m_TabType);
			flag &= lua.GetData<NKM_ITEM_DROP_POSITION>("m_ContentType", ref nkmitemMoldTemplet.m_ContentType);
			flag &= lua.GetData("m_Tier", ref nkmitemMoldTemplet.m_Tier);
			flag = lua.GetData<NKM_ITEM_GRADE>("m_Grade", ref nkmitemMoldTemplet.m_Grade);
			flag = lua.GetData<NKM_UNIT_STYLE_TYPE>("m_RewardEquipUnitType", ref nkmitemMoldTemplet.m_RewardEquipUnitType);
			flag = lua.GetData<ITEM_EQUIP_POSITION>("m_RewardEquipPosition", ref nkmitemMoldTemplet.m_RewardEquipPosition);
			flag &= lua.GetData("m_bPermanent", ref nkmitemMoldTemplet.m_bPermanent);
			flag &= lua.GetData("m_MoldName", ref nkmitemMoldTemplet.m_MoldName);
			flag &= lua.GetData("m_MoldDesc", ref nkmitemMoldTemplet.m_MoldDesc);
			flag &= lua.GetData("m_Time", ref nkmitemMoldTemplet.m_Time);
			lua.GetData("m_OpenTag", ref nkmitemMoldTemplet.m_OpenTag);
			for (int i = 1; i <= 3; i++)
			{
				NKMItemMoldMaterialData nkmitemMoldMaterialData = default(NKMItemMoldMaterialData);
				lua.GetData<NKM_REWARD_TYPE>(string.Format("m_MaterialType{0}", i), ref nkmitemMoldMaterialData.m_MaterialType);
				lua.GetData(string.Format("m_MaterialID{0}", i), ref nkmitemMoldMaterialData.m_MaterialID);
				lua.GetData(string.Format("m_MaterialValue{0}", i), ref nkmitemMoldMaterialData.m_MaterialValue);
				if (nkmitemMoldMaterialData.m_MaterialID > 0)
				{
					nkmitemMoldTemplet.m_MaterialList.Add(nkmitemMoldMaterialData);
				}
			}
			flag &= lua.GetData("m_MoldIconName", ref nkmitemMoldTemplet.m_MoldIconName);
			lua.GetData("m_DateStrID", ref nkmitemMoldTemplet.intervalId);
			nkmitemMoldTemplet.CheckValidation();
			if (!flag)
			{
				Log.Error(string.Format("NKMItemMoldTemplet Load fail - {0}", nkmitemMoldTemplet.m_MoldID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 290);
				return null;
			}
			if (NKMItemManager.m_dicMoldReward.ContainsKey(nkmitemMoldTemplet.m_RewardGroupID))
			{
				NKMItemManager.m_dicMoldReward[nkmitemMoldTemplet.m_RewardGroupID].Add(nkmitemMoldTemplet.m_RewardGroupID);
			}
			return nkmitemMoldTemplet;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005BDD7 File Offset: 0x00059FD7
		public static NKMItemMoldTemplet Find(int key)
		{
			return NKMTempletContainer<NKMItemMoldTemplet>.Find(key);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0005BDDF File Offset: 0x00059FDF
		public void Join()
		{
			if (NKMUtil.IsServer)
			{
				this.JoinIntervalTemplet();
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0005BDF0 File Offset: 0x00059FF0
		public void JoinIntervalTemplet()
		{
			if (!string.IsNullOrEmpty(this.intervalId))
			{
				this.IntervalTemplet = NKMIntervalTemplet.Find(this.intervalId);
				if (this.IntervalTemplet == null)
				{
					this.IntervalTemplet = NKMIntervalTemplet.Unuseable;
					Log.ErrorAndExit(string.Format("[Mold:{0}]잘못된 interval id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 321);
					return;
				}
				if (this.IntervalTemplet.IsRepeatDate)
				{
					Log.ErrorAndExit(string.Format("[Mold:{0}] 반복 기간설정 사용 불가. id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 327);
					return;
				}
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005BE94 File Offset: 0x0005A094
		public void Validate()
		{
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0005BE96 File Offset: 0x0005A096
		public void PostJoin()
		{
			this.JoinIntervalTemplet();
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005BEA0 File Offset: 0x0005A0A0
		private void CheckValidation()
		{
			foreach (NKMItemMoldMaterialData nkmitemMoldMaterialData in this.m_MaterialList)
			{
				if (nkmitemMoldMaterialData.m_MaterialType != NKM_REWARD_TYPE.RT_MISC)
				{
					Log.ErrorAndExit(string.Format("[ItemMoldTemplet] 재료 아이템은 MISC만 넣을 수 있음 m_MoldID : {0}, m_MaterialType1 : {1}, m_MaterialID1 : {2}", this.m_MoldID, nkmitemMoldMaterialData.m_MaterialType, nkmitemMoldMaterialData.m_MaterialID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 38);
				}
				if (nkmitemMoldMaterialData.m_MaterialID > 0 && !NKMRewardTemplet.IsValidReward(nkmitemMoldMaterialData.m_MaterialType, nkmitemMoldMaterialData.m_MaterialID))
				{
					Log.ErrorAndExit(string.Format("[ItemMoldTemplet] 재료 아이템이 존재하지 않음 m_MoldID : {0}, m_MaterialType1 : {1}, m_MaterialID1 : {2}", this.m_MoldID, nkmitemMoldMaterialData.m_MaterialType, nkmitemMoldMaterialData.m_MaterialID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 45);
				}
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005BF88 File Offset: 0x0005A188
		public string GetItemName()
		{
			return NKCStringTable.GetString(this.m_MoldName, false);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005BF96 File Offset: 0x0005A196
		public string GetItemDesc()
		{
			return NKCStringTable.GetString(this.m_MoldDesc, false);
		}

		// Token: 0x04000F73 RID: 3955
		public const int MAX_MATERIAL_COUNT = 3;

		// Token: 0x04000F74 RID: 3956
		private string intervalId;

		// Token: 0x04000F75 RID: 3957
		public int m_MoldID;

		// Token: 0x04000F76 RID: 3958
		public string m_MoldStrID;

		// Token: 0x04000F77 RID: 3959
		public int m_RewardGroupID;

		// Token: 0x04000F78 RID: 3960
		public NKM_CRAFT_TAB_TYPE m_TabType;

		// Token: 0x04000F79 RID: 3961
		public int m_Tier;

		// Token: 0x04000F7A RID: 3962
		public NKM_ITEM_DROP_POSITION m_ContentType;

		// Token: 0x04000F7B RID: 3963
		public NKM_ITEM_GRADE m_Grade;

		// Token: 0x04000F7C RID: 3964
		public ITEM_EQUIP_POSITION m_RewardEquipPosition = ITEM_EQUIP_POSITION.IEP_NONE;

		// Token: 0x04000F7D RID: 3965
		public NKM_UNIT_STYLE_TYPE m_RewardEquipUnitType = NKM_UNIT_STYLE_TYPE.NUST_ETC;

		// Token: 0x04000F7E RID: 3966
		private string m_OpenTag;

		// Token: 0x04000F7F RID: 3967
		public bool m_bPermanent;

		// Token: 0x04000F80 RID: 3968
		public string m_MoldName;

		// Token: 0x04000F81 RID: 3969
		public string m_MoldDesc;

		// Token: 0x04000F82 RID: 3970
		public int m_Time;

		// Token: 0x04000F83 RID: 3971
		public List<NKMItemMoldMaterialData> m_MaterialList = new List<NKMItemMoldMaterialData>(3);

		// Token: 0x04000F84 RID: 3972
		public Dictionary<int, int> m_dicRewardGroup = new Dictionary<int, int>();

		// Token: 0x04000F85 RID: 3973
		public string m_MoldIconName;
	}
}
