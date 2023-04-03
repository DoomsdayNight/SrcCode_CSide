using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cs.Logging;
using NKC;
using NKM.Contract2;
using NKM.Item;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;

namespace NKM
{
	// Token: 0x0200038A RID: 906
	public class NKMItemMiscTemplet : INKMTemplet
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0005D95C File Offset: 0x0005BB5C
		public static IEnumerable<NKMItemMiscTemplet> Values
		{
			get
			{
				return NKMTempletContainer<NKMItemMiscTemplet>.Values;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005D963 File Offset: 0x0005BB63
		public static IEnumerable<NKMOfficeInteriorTemplet> InteriorValues
		{
			get
			{
				return NKMTempletContainer<NKMOfficeInteriorTemplet>.Values;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0005D96A File Offset: 0x0005BB6A
		public int Key
		{
			get
			{
				return this.m_ItemMiscID;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0005D972 File Offset: 0x0005BB72
		public string DebugName
		{
			get
			{
				return string.Format("[{0}]{1}", this.m_ItemMiscID, this.m_ItemMiscStrID);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0005D98F File Offset: 0x0005BB8F
		public IReadOnlyList<NKMCustomPackageGroupTemplet> CustomPackageTemplets
		{
			get
			{
				return this.customRewardTemplets;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0005D997 File Offset: 0x0005BB97
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0005D9A4 File Offset: 0x0005BBA4
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x0005D9AC File Offset: 0x0005BBAC
		public MiscContractTemplet MiscContractTemplet { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0005D9B5 File Offset: 0x0005BBB5
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x0005D9BD File Offset: 0x0005BBBD
		public NKMIntervalTemplet IntervalTemplet { get; private set; }

		// Token: 0x0600173D RID: 5949 RVA: 0x0005D9C6 File Offset: 0x0005BBC6
		public static NKMItemMiscTemplet Find(int key)
		{
			return NKMTempletContainer<NKMItemMiscTemplet>.Find(key);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005D9CE File Offset: 0x0005BBCE
		public static NKMItemMiscTemplet Find(string key)
		{
			return NKMTempletContainer<NKMItemMiscTemplet>.Find(key);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0005D9D6 File Offset: 0x0005BBD6
		public static NKMOfficeInteriorTemplet FindInterior(int key)
		{
			return NKMTempletContainer<NKMOfficeInteriorTemplet>.Find(key);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0005D9E0 File Offset: 0x0005BBE0
		public static NKMItemMiscTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 54))
			{
				return null;
			}
			NKMItemMiscTemplet nkmitemMiscTemplet = new NKMItemMiscTemplet();
			if (!nkmitemMiscTemplet.Load(lua))
			{
				return null;
			}
			return nkmitemMiscTemplet;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005DA10 File Offset: 0x0005BC10
		public bool IsUsable()
		{
			NKM_ITEM_MISC_TYPE itemMiscType = this.m_ItemMiscType;
			return itemMiscType - NKM_ITEM_MISC_TYPE.IMT_PACKAGE <= 1 || itemMiscType == NKM_ITEM_MISC_TYPE.IMT_CONTRACT || this.IsChoiceItem();
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0005DA37 File Offset: 0x0005BC37
		public bool IsRatioOpened()
		{
			return this.m_RandomBoxRatioBool;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0005DA40 File Offset: 0x0005BC40
		public bool IsHideInInven()
		{
			NKM_ITEM_MISC_TYPE itemMiscType = this.m_ItemMiscType;
			return itemMiscType - NKM_ITEM_MISC_TYPE.IMT_RESOURCE <= 3 || itemMiscType - NKM_ITEM_MISC_TYPE.IMT_PIECE <= 2 || itemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0005DA69 File Offset: 0x0005BC69
		public bool IsEmblem()
		{
			return this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_EMBLEM || this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_EMBLEM_RANK;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005DA80 File Offset: 0x0005BC80
		public bool IsChoiceItem()
		{
			NKM_ITEM_MISC_TYPE itemMiscType = this.m_ItemMiscType;
			return itemMiscType - NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT <= 3 || itemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_OPERATOR || itemMiscType - NKM_ITEM_MISC_TYPE.IMT_CHOICE_FURNITURE <= 1;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0005DAAC File Offset: 0x0005BCAC
		public virtual void Join()
		{
			foreach (int num in this.customRewardGroupId)
			{
				NKMCustomPackageGroupTemplet nkmcustomPackageGroupTemplet = NKMCustomPackageGroupTemplet.Find(num);
				if (nkmcustomPackageGroupTemplet == null)
				{
					NKMTempletError.Add(string.Format("[MiscItem] CustomRewardGroupId가 올바르지 않음. miscId:{0} groupId:{1}", this.DebugName, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 127);
				}
				else
				{
					this.customRewardTemplets.Add(nkmcustomPackageGroupTemplet);
				}
			}
			if (this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CONTRACT)
			{
				this.MiscContractTemplet = MiscContractTemplet.Find(this.m_typeValue);
				if (this.MiscContractTemplet == null)
				{
					NKMTempletError.Add(string.Format("[MiscItem] 채용 아이디가 올바르지 않음. miscId:{0} typeValue:{1}", this.DebugName, this.m_typeValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 139);
				}
			}
			if (!string.IsNullOrEmpty(this.dateStrId) && NKMUtil.IsServer)
			{
				this.IntervalTemplet = NKMIntervalTemplet.Find(this.dateStrId);
				if (this.IntervalTemplet == null)
				{
					NKMTempletError.Add("[MiscItem] 잘못된 인터벌 아이디. miscId:" + this.DebugName + " dateStrId:" + this.dateStrId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 148);
				}
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0005DBD8 File Offset: 0x0005BDD8
		public virtual void Validate()
		{
			if (!this.EnableByTag)
			{
				return;
			}
			if (this.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE && this.customRewardTemplets.Any<NKMCustomPackageGroupTemplet>())
			{
				NKMTempletError.Add("[MiscItem] 커스텀패키지가 아닌데 선택보상 정보를 갖고있음. miscId:" + this.DebugName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 163);
			}
			if (this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CONTRACT)
			{
				this.MiscContractTemplet = MiscContractTemplet.Find(this.m_typeValue);
				if (this.MiscContractTemplet != null)
				{
					this.MiscContractTemplet.ValidateMiscContract();
				}
			}
			if (this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE)
			{
				if (this.customRewardTemplets.Count == 0)
				{
					NKMTempletError.Add("[MiscItem] 커스텀패키지 선택보상 정보가 비어있음. miscId:" + this.DebugName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 179);
				}
				if (this.customRewardTemplets.Count > 10)
				{
					NKMTempletError.Add(string.Format("[MiscItem] 커스텀패키지 선택보상 최대개수 초과. miscId:{0} count:{1}", this.DebugName, this.customRewardTemplets.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 184);
				}
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0005DCC8 File Offset: 0x0005BEC8
		public bool ValidateCustompackageSelection(IReadOnlyList<int> selectIndices)
		{
			if (this.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE || selectIndices == null || selectIndices.Count != this.customRewardTemplets.Count)
			{
				return false;
			}
			for (int i = 0; i < this.customRewardTemplets.Count; i++)
			{
				NKMCustomPackageGroupTemplet nkmcustomPackageGroupTemplet = this.customRewardTemplets[i];
				int num = selectIndices[i];
				if (num < 0 || num >= nkmcustomPackageGroupTemplet.Elements.Count)
				{
					return false;
				}
				if (!nkmcustomPackageGroupTemplet.Elements[num].EnableByTag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0005DD4C File Offset: 0x0005BF4C
		public bool SelectCustomPackageElement(IReadOnlyList<int> selectIndices, out List<NKMCustomPackageElement> elements)
		{
			if (this.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE || selectIndices == null || selectIndices.Count != this.customRewardTemplets.Count)
			{
				elements = null;
				return false;
			}
			elements = new List<NKMCustomPackageElement>(this.customRewardTemplets.Count);
			for (int i = 0; i < this.customRewardTemplets.Count; i++)
			{
				NKMCustomPackageGroupTemplet nkmcustomPackageGroupTemplet = this.customRewardTemplets[i];
				int num = selectIndices[i];
				if (num < 0 || num >= nkmcustomPackageGroupTemplet.Elements.Count)
				{
					return false;
				}
				if (!nkmcustomPackageGroupTemplet.Elements[num].EnableByTag)
				{
					return false;
				}
				elements.Add(nkmcustomPackageGroupTemplet.Elements[num]);
			}
			return true;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0005DDF8 File Offset: 0x0005BFF8
		protected virtual bool Load(NKMLua lua)
		{
			lua.GetData("m_OpenTag", ref this.m_OpenTag);
			lua.GetData("m_ItemMiscID", ref this.m_ItemMiscID);
			lua.GetData("m_ItemMiscStrID", ref this.m_ItemMiscStrID);
			lua.GetData("m_ItemMiscName", ref this.m_ItemMiscName);
			lua.GetData("m_ItemMiscIconName", ref this.m_ItemMiscIconName);
			lua.GetData("m_ItemMiscDesc", ref this.m_ItemMiscDesc);
			bool data = lua.GetData<NKM_ITEM_MISC_TYPE>("m_ItemMiscType", ref this.m_ItemMiscType);
			lua.GetData<NKM_ITEM_MISC_SUBTYPE>("m_ItemMiscSubType", ref this.m_ItemMiscSubType);
			if (!(data & lua.GetData<NKM_ITEM_GRADE>("m_NKM_ITEM_GRADE", ref this.m_NKM_ITEM_GRADE)))
			{
				Log.Error("NKMItemMiscTemplet.LoadFromLUA fail - " + this.m_ItemMiscStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemMiscTemplet.cs", 264);
				return false;
			}
			lua.GetData("m_RewardGroupID", ref this.m_RewardGroupID);
			lua.GetData("m_BannerImage", ref this.m_BannerImage);
			lua.GetData("m_RandomBoxRatioBool", ref this.m_RandomBoxRatioBool);
			lua.GetData("m_ShortCutShopTabID", ref this.m_ShortCutShopTabID);
			lua.GetData("m_ShortCutShopIndex", ref this.m_ShortCutShopIndex);
			lua.GetData("m_NexonLogSubType", ref this.m_NexonLogSubType);
			if (lua.OpenTable("m_PopupDisplayProductID"))
			{
				this.m_lstRecommandProductItemIfNotEnough = new List<int>();
				int num = 1;
				int item = 0;
				while (lua.GetData(num, ref item))
				{
					this.m_lstRecommandProductItemIfNotEnough.Add(item);
					num++;
				}
				lua.CloseTable();
			}
			if (lua.OpenTable("m_CustomRewardGroupID"))
			{
				int num2 = 1;
				int item2 = 0;
				while (lua.GetData(num2, ref item2))
				{
					this.customRewardGroupId.Add(item2);
					num2++;
				}
				lua.CloseTable();
			}
			lua.GetData("m_typeValue", ref this.m_typeValue);
			lua.GetData("m_ItemDropInfo", ref this.m_ItemDropInfo);
			lua.GetData("m_DateStrID", ref this.dateStrId);
			return true;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0005DFE1 File Offset: 0x0005C1E1
		public bool IsPackageItem
		{
			get
			{
				return this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_PACKAGE;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0005DFEC File Offset: 0x0005C1EC
		public bool IsCustomPackageItem
		{
			get
			{
				return this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0005DFF8 File Offset: 0x0005C1F8
		public bool IsContractItem
		{
			get
			{
				return this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CONTRACT;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0005E004 File Offset: 0x0005C204
		public bool IsTimeIntervalItem
		{
			get
			{
				return !string.IsNullOrEmpty(this.dateStrId);
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0005E014 File Offset: 0x0005C214
		public string GetItemName()
		{
			return NKCStringTable.GetString(this.m_ItemMiscName, false);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0005E024 File Offset: 0x0005C224
		public string GetItemDesc()
		{
			if (this.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(this.Key);
				if (nkmofficeInteriorTemplet != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_INTERIOR_SCORE_ONE_PARAM", new object[]
					{
						nkmofficeInteriorTemplet.InteriorScore
					}));
					if (nkmofficeInteriorTemplet.InteriorCategory == InteriorCategory.FURNITURE)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(NKCStringTable.GetString("SI_DP_INTERIOR_SIZE_TWO_PARAM", new object[]
						{
							nkmofficeInteriorTemplet.CellX,
							nkmofficeInteriorTemplet.CellY
						}));
					}
					stringBuilder.AppendLine();
					stringBuilder.Append(NKCStringTable.GetString(this.m_ItemMiscDesc, false));
					return stringBuilder.ToString();
				}
			}
			return NKCStringTable.GetString(this.m_ItemMiscDesc, false);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0005E0F0 File Offset: 0x0005C2F0
		public TimeSpan GetIntervalTimeSpanLeft()
		{
			if (!this.IsTimeIntervalItem)
			{
				return new TimeSpan(99999, 0, 0, 0);
			}
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(this.dateStrId);
			if (nkmintervalTemplet == null)
			{
				Log.Error(string.Format("MiscItemId: {0} DateStrId: {1} IntervalTemplet is not found", this.m_ItemMiscID, this.dateStrId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 137);
				return new TimeSpan(99999, 0, 0, 0);
			}
			return NKCSynchronizedTime.GetTimeLeft(nkmintervalTemplet.GetEndDateUtc());
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005E168 File Offset: 0x0005C368
		public bool WillBeDeletedSoon()
		{
			return this.GetIntervalTimeSpanLeft().Days < 2;
		}

		// Token: 0x04000FA2 RID: 4002
		private readonly List<int> customRewardGroupId = new List<int>();

		// Token: 0x04000FA3 RID: 4003
		private readonly List<NKMCustomPackageGroupTemplet> customRewardTemplets = new List<NKMCustomPackageGroupTemplet>();

		// Token: 0x04000FA4 RID: 4004
		private string dateStrId;

		// Token: 0x04000FA5 RID: 4005
		public int m_ItemMiscID;

		// Token: 0x04000FA6 RID: 4006
		public string m_ItemMiscStrID = "";

		// Token: 0x04000FA7 RID: 4007
		public string m_ItemMiscName = "";

		// Token: 0x04000FA8 RID: 4008
		public string m_ItemMiscIconName = "";

		// Token: 0x04000FA9 RID: 4009
		public string m_ItemMiscDesc = "";

		// Token: 0x04000FAA RID: 4010
		public NKM_ITEM_MISC_TYPE m_ItemMiscType;

		// Token: 0x04000FAB RID: 4011
		public NKM_ITEM_MISC_SUBTYPE m_ItemMiscSubType;

		// Token: 0x04000FAC RID: 4012
		public NKM_ITEM_GRADE m_NKM_ITEM_GRADE;

		// Token: 0x04000FAD RID: 4013
		public int m_RewardGroupID;

		// Token: 0x04000FAE RID: 4014
		public string m_ShortCutShopTabID = "TAB_NONE";

		// Token: 0x04000FAF RID: 4015
		public int m_ShortCutShopIndex;

		// Token: 0x04000FB0 RID: 4016
		public string m_BannerImage = "";

		// Token: 0x04000FB1 RID: 4017
		public bool m_RandomBoxRatioBool;

		// Token: 0x04000FB2 RID: 4018
		public string m_NexonLogSubType = "";

		// Token: 0x04000FB3 RID: 4019
		private string m_OpenTag;

		// Token: 0x04000FB4 RID: 4020
		public int m_typeValue;

		// Token: 0x04000FB5 RID: 4021
		public bool m_ItemDropInfo;

		// Token: 0x04000FB6 RID: 4022
		public List<int> m_lstRecommandProductItemIfNotEnough;
	}
}
