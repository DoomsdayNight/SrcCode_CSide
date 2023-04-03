using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Item;
using Cs.Logging;
using Cs.Math;
using NKC;
using NKM.Contract2;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200037D RID: 893
	public class NKMEquipTemplet : INKMTemplet
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0005AF66 File Offset: 0x00059166
		public IReadOnlyList<NKMEquipStatGroupTemplet> StatGroups
		{
			get
			{
				return this.statGroups;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005AF6E File Offset: 0x0005916E
		public IReadOnlyList<int> SetGroupList
		{
			get
			{
				if (this.m_lstSetGroup != null)
				{
					return this.m_lstSetGroup.AsReadOnly();
				}
				return null;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005AF85 File Offset: 0x00059185
		public IReadOnlyList<int> PrivateUnitList
		{
			get
			{
				return this.m_lstPrivateUnitID;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005AF8D File Offset: 0x0005918D
		public int Key
		{
			get
			{
				return this.m_ItemEquipID;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005AF95 File Offset: 0x00059195
		public bool IsEquipEnable
		{
			get
			{
				return NKMUnitTempletBase.IsUnitStyleType(this.m_EquipUnitStyleType);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0005AFA2 File Offset: 0x000591A2
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x0005AFAA File Offset: 0x000591AA
		public NKM_EQUIP_PRESET_TYPE PresetType { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005AFB3 File Offset: 0x000591B3
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x0005AFBB File Offset: 0x000591BB
		public NKMPotentialOptionGroupTemplet PotentialOptionGroupTemplet { get; private set; }

		// Token: 0x060016CE RID: 5838 RVA: 0x0005AFC4 File Offset: 0x000591C4
		public bool IsPrivateEquip()
		{
			return this.m_lstPrivateUnitID.Count > 0;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0005AFD4 File Offset: 0x000591D4
		public int GetPrivateUnitID()
		{
			if (this.m_lstPrivateUnitID.Count == 0)
			{
				return 0;
			}
			return this.m_lstPrivateUnitID[0];
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0005AFF1 File Offset: 0x000591F1
		public bool IsPrivateEquipForUnit(int unitID)
		{
			return this.m_lstPrivateUnitID.Count > 0 && NKMUnitManager.CheckContainsBaseUnit(this.m_lstPrivateUnitID, unitID);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0005B00F File Offset: 0x0005920F
		public NKM_ERROR_CODE CanEquipByUnitID(int unitID)
		{
			if (NKMUnitManager.GetUnitTempletBase(unitID).m_NKM_UNIT_STYLE_TYPE != this.m_EquipUnitStyleType)
			{
				return NKM_ERROR_CODE.NEX_FAIL_CANNOT_EQUIP_ITEM;
			}
			if (this.m_lstPrivateUnitID.Count > 0 && !NKMUnitManager.CheckContainsBaseUnit(this.m_lstPrivateUnitID, unitID))
			{
				return NKM_ERROR_CODE.NEX_FAIL_CANNOT_EQUIP_ITEM;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0005B050 File Offset: 0x00059250
		public NKM_ERROR_CODE CanEquipByUnit(NKMUserData userdata, NKMUnitData unitData)
		{
			if (unitData.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
			}
			if (NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID).m_NKM_UNIT_STYLE_TYPE != this.m_EquipUnitStyleType)
			{
				return NKM_ERROR_CODE.NEX_FAIL_CANNOT_EQUIP_ITEM;
			}
			if (this.m_lstPrivateUnitID.Count > 0 && !NKMUnitManager.CheckContainsBaseUnit(this.m_lstPrivateUnitID, unitData.m_UnitID))
			{
				return NKM_ERROR_CODE.NEX_FAIL_CANNOT_EQUIP_ITEM;
			}
			NKMDeckData deckDataByUnitUID = userdata.m_ArmyData.GetDeckDataByUnitUID(unitData.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				if (deckDataByUnitUID.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (deckDataByUnitUID.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0005B0E4 File Offset: 0x000592E4
		public NKM_ERROR_CODE CanUnEquipByUnit(NKMUserData userdata, NKMUnitData unit)
		{
			NKMDeckData deckDataByUnitUID = userdata.m_ArmyData.GetDeckDataByUnitUID(unit.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				if (deckDataByUnitUID.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (deckDataByUnitUID.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005B128 File Offset: 0x00059328
		public int MakeSetOption(int currentSetOption = 0)
		{
			if (this.m_lstSetGroup == null || this.m_lstSetGroup.Count == 0)
			{
				return 0;
			}
			List<int> list = this.m_lstSetGroup;
			if (currentSetOption != 0)
			{
				list = (from e in this.m_lstSetGroup
				where e != currentSetOption
				select e).ToList<int>();
			}
			int index = RandomGenerator.Next(list.Count);
			return list[index];
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0005B198 File Offset: 0x00059398
		public bool ValidSetOption(int setOptionId)
		{
			return this.m_lstSetGroup != null && this.m_lstSetGroup.Count != 0 && this.m_lstSetGroup.Contains(setOptionId);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0005B1C4 File Offset: 0x000593C4
		public bool CheckEquipEnable(int unitId)
		{
			if (!this.IsPrivateEquip())
			{
				return true;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitTempletBase.Find(unitId);
			return unitTempletBase != null && this.m_lstPrivateUnitID.Any((int e) => unitTempletBase.IsSameBaseUnit(e));
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0005B210 File Offset: 0x00059410
		public static NKMEquipTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 230))
			{
				return null;
			}
			NKMEquipTemplet nkmequipTemplet = new NKMEquipTemplet();
			bool flag = true;
			flag &= lua.GetData("m_ItemEquipID", ref nkmequipTemplet.m_ItemEquipID);
			flag &= lua.GetData("m_ItemEquipStrID", ref nkmequipTemplet.m_ItemEquipStrID);
			flag &= lua.GetData("m_ItemEquipName", ref nkmequipTemplet.m_ItemEquipName);
			flag &= lua.GetData("m_ItemEquipDesc", ref nkmequipTemplet.m_ItemEquipDesc);
			flag &= lua.GetData<NKM_ITEM_GRADE>("m_NKM_ITEM_GRADE", ref nkmequipTemplet.m_NKM_ITEM_GRADE);
			flag &= lua.GetData("m_NKM_ITEM_TIER", ref nkmequipTemplet.m_NKM_ITEM_TIER);
			flag &= lua.GetData<ITEM_EQUIP_POSITION>("m_ItemEquipPosition", ref nkmequipTemplet.m_ItemEquipPosition);
			flag &= lua.GetData<NKM_UNIT_STYLE_TYPE>("m_EquipUnitStyleType", ref nkmequipTemplet.m_EquipUnitStyleType);
			flag &= lua.GetData("m_ItemEquipIconName", ref nkmequipTemplet.m_ItemEquipIconName);
			flag &= lua.GetData("m_MaxEnchantLevel", ref nkmequipTemplet.m_MaxEnchantLevel);
			int num = 0;
			int num2 = 0;
			lua.GetData("m_OnRemoveItemID_1", ref num);
			lua.GetData("m_OnRemoveItemCount_1", ref num2);
			if (num > 0 && num2 > 0)
			{
				nkmequipTemplet.m_OnRemoveItemList.Add(new NKMEquipTemplet.OnRemoveItemData(num, num2));
			}
			num = 0;
			num2 = 0;
			lua.GetData("m_OnRemoveItemID_2", ref num);
			lua.GetData("m_OnRemoveItemCount_2", ref num2);
			if (num > 0 && num2 > 0)
			{
				nkmequipTemplet.m_OnRemoveItemList.Add(new NKMEquipTemplet.OnRemoveItemData(num, num2));
			}
			if (lua.OpenTable("m_lstPrivateUnitID"))
			{
				nkmequipTemplet.m_lstPrivateUnitID.Clear();
				int num3 = 1;
				int item = 0;
				while (lua.GetData(num3, ref item))
				{
					nkmequipTemplet.m_lstPrivateUnitID.Add(item);
					num3++;
				}
				lua.CloseTable();
			}
			nkmequipTemplet.m_StatType = NKM_STAT_TYPE.NST_END;
			lua.GetData<NKM_STAT_TYPE>("STAT_TYPE_1", ref nkmequipTemplet.m_StatType);
			flag &= lua.GetData("STAT_VALUE_1", ref nkmequipTemplet.m_StatData.m_fBaseValue);
			flag &= lua.GetData("STAT_LEVELUP_VALUE_1", ref nkmequipTemplet.m_StatData.m_fLevelUpValue);
			lua.GetData("m_StatGroupID", ref nkmequipTemplet.m_StatGroupID);
			lua.GetData("m_StatGroupID_2", ref nkmequipTemplet.m_StatGroupID_2);
			lua.GetData("m_FeedEXP", ref nkmequipTemplet.m_FeedEXP);
			lua.GetData("m_PrecisionReqResource", ref nkmequipTemplet.m_PrecisionReqResource);
			lua.GetData("m_PrecisionReqItem", ref nkmequipTemplet.m_PrecisionReqItem);
			lua.GetData("m_RandomStatReqResource", ref nkmequipTemplet.m_RandomStatReqResource);
			lua.GetData("m_RandomStatReqItem", ref nkmequipTemplet.m_RandomStatReqItem);
			lua.GetData("m_RandomSetReqResource", ref nkmequipTemplet.m_RandomSetReqResource);
			lua.GetData("m_RandomSetReqItemID", ref nkmequipTemplet.m_RandomSetReqItemID);
			lua.GetData("m_RandomSetReqItemValue", ref nkmequipTemplet.m_RandomSetReqItemValue);
			lua.GetData("m_bItemFirstLock", ref nkmequipTemplet.m_bItemFirstLock);
			lua.GetData("m_bRelic", ref nkmequipTemplet.m_bRelic);
			lua.GetData("m_PotentialOptionGroupID", ref nkmequipTemplet.potentialOptionGroupId);
			lua.GetData("m_bEffect", ref nkmequipTemplet.m_bShowEffect);
			if (lua.OpenTable("m_SetGroup"))
			{
				nkmequipTemplet.m_lstSetGroup = new List<int>();
				int num4 = 1;
				int item2 = 0;
				while (lua.GetData(num4, ref item2))
				{
					nkmequipTemplet.m_lstSetGroup.Add(item2);
					num4++;
				}
				lua.CloseTable();
			}
			if (nkmequipTemplet.m_bRelic)
			{
				for (int i = 0; i < nkmequipTemplet.socketReqResource.Length; i++)
				{
					int num5 = i + 1;
					List<MiscItemUnit> list = new List<MiscItemUnit>();
					nkmequipTemplet.socketReqResource[i] = list;
					int num6 = 1;
					long num7;
					lua.GetData(string.Format("Socket{0}_ReqResource", num5), out num7, 0L);
					if (num7 > 0L)
					{
						list.Add(new MiscItemUnit(num6, num7));
					}
					num6 = 0;
					num7 = 0L;
					lua.GetData(string.Format("Socket{0}_OpenItemID", num5), ref num6);
					lua.GetData(string.Format("Socket{0}_OpenCount", num5), ref num7);
					if (num6 > 0)
					{
						list.Add(new MiscItemUnit(num6, num7));
					}
				}
			}
			if (!flag)
			{
				Log.Error(string.Format("NKMEquipTemplet Load - {0}", nkmequipTemplet.m_ItemEquipID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 345);
				return null;
			}
			return nkmequipTemplet;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0005B638 File Offset: 0x00059838
		public void Join()
		{
			if (NKMUnitTempletBase.IsUnitStyleType(this.m_EquipUnitStyleType))
			{
				this.PresetType = NKMEquipTemplet.UnitStyleToEquipPreset(this.m_EquipUnitStyleType);
			}
			if (this.m_StatGroupID > 0)
			{
				NKMEquipStatGroupTemplet nkmequipStatGroupTemplet;
				if (!NKMEquipTuningManager.TryGetStatGroupTemplet(this.m_StatGroupID, out nkmequipStatGroupTemplet))
				{
					NKMTempletError.Add(string.Format("[EquipTemplet:{0}] 랜덤스탯 그룹이 존재하지 않음 m_StatGroupID:{1}", this.m_ItemEquipID, this.m_StatGroupID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 363);
				}
				else
				{
					this.statGroups[0] = nkmequipStatGroupTemplet;
				}
			}
			if (this.m_StatGroupID_2 > 0)
			{
				NKMEquipStatGroupTemplet nkmequipStatGroupTemplet2;
				if (!NKMEquipTuningManager.TryGetStatGroupTemplet(this.m_StatGroupID_2, out nkmequipStatGroupTemplet2))
				{
					NKMTempletError.Add(string.Format("[EquipTemplet:{0}] 랜덤스탯 그룹이 존재하지 않음 m_StatGroupID:{1}", this.m_ItemEquipID, this.m_StatGroupID_2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 375);
				}
				else
				{
					this.statGroups[1] = nkmequipStatGroupTemplet2;
				}
			}
			if (this.m_bRelic)
			{
				foreach (MiscItemUnit miscItemUnit in this.socketReqResource.SelectMany((List<MiscItemUnit> e) => e))
				{
					miscItemUnit.Join("/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 387);
				}
				this.PotentialOptionGroupTemplet = NKMPotentialOptionGroupTemplet.Find(this.potentialOptionGroupId);
				if (this.PotentialOptionGroupTemplet == null)
				{
					NKMTempletError.Add(string.Format("[EquipTemplet:{0}] 잠재옵션 그룹아이디가 유효하지 않음:{1}", this.m_ItemEquipID, this.potentialOptionGroupId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 393);
				}
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0005B7C8 File Offset: 0x000599C8
		public void Validate()
		{
			if (NKMUnitTempletBase.IsUnitStyleType(this.m_EquipUnitStyleType) && (this.PresetType == NKM_EQUIP_PRESET_TYPE.NEPT_INVLID || this.PresetType == NKM_EQUIP_PRESET_TYPE.NEPT_NONE))
			{
				NKMTempletError.Add(string.Format("[Equip:{0}] 장비 프리셋 타입 계산 오류. styleType:{1}", this.Key, this.m_EquipUnitStyleType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 405);
				return;
			}
			foreach (int num in this.m_lstPrivateUnitID)
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(num);
				if (nkmunitTempletBase == null)
				{
					NKMTempletError.Add(string.Format("[Equip:{0}] 잘못된 유닛아이디(전용장비 대상):{1}", this.Key, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 415);
				}
				else if (nkmunitTempletBase.IsRearmUnit)
				{
					NKMTempletError.Add(string.Format("[Equip:{0}] 전용장비 대상 unitId에는 재무장 유닛id를 넣을 수 없음. unitId:{1} rearmGrade:{2}", this.Key, num, nkmunitTempletBase.m_RearmGrade), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 422);
				}
			}
			if (this.m_lstSetGroup != null)
			{
				foreach (int num2 in this.m_lstSetGroup)
				{
					if (num2 != 0 && !NKMItemManager.m_dicItemEquipSetOptionTempletByID.ContainsKey(num2))
					{
						Log.ErrorAndExit(string.Format("[EquipTemplet] 세트옵션이 존재하지 않음. m_ItemEquipID : {0}, SetOptionId:{1}", this.m_ItemEquipID, num2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMEquipTemplet.cs", 433);
					}
				}
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0005B958 File Offset: 0x00059B58
		public bool IsValidEquipPosition(ITEM_EQUIP_POSITION equipPosition)
		{
			if (equipPosition > ITEM_EQUIP_POSITION.IEP_DEFENCE)
			{
				return equipPosition - ITEM_EQUIP_POSITION.IEP_ACC <= 1 && this.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC;
			}
			return this.m_ItemEquipPosition == equipPosition;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0005B97B File Offset: 0x00059B7B
		public static NKM_EQUIP_PRESET_TYPE UnitStyleToEquipPreset(NKM_UNIT_STYLE_TYPE unitStyleType)
		{
			switch (unitStyleType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return NKM_EQUIP_PRESET_TYPE.NEPT_COUNTER;
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return NKM_EQUIP_PRESET_TYPE.NEPT_SOLDIER;
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return NKM_EQUIP_PRESET_TYPE.NEPT_MECHANIC;
			default:
				return NKM_EQUIP_PRESET_TYPE.NEPT_INVLID;
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0005B99A File Offset: 0x00059B9A
		public string GetItemName()
		{
			return NKCStringTable.GetString(this.m_ItemEquipName, false);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0005B9A8 File Offset: 0x00059BA8
		public string GetItemDesc()
		{
			return NKCStringTable.GetString(this.m_ItemEquipDesc, false);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0005B9B6 File Offset: 0x00059BB6
		public bool IsRelic()
		{
			return this.m_bRelic;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005B9BE File Offset: 0x00059BBE
		public int GetPotentialOptionGroupID()
		{
			return this.potentialOptionGroupId;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0005B9C6 File Offset: 0x00059BC6
		public List<MiscItemUnit> GetSocketOpenResource(int socketIndex)
		{
			if (this.socketReqResource.Length <= socketIndex)
			{
				return new List<MiscItemUnit>();
			}
			return this.socketReqResource[socketIndex];
		}

		// Token: 0x04000F0B RID: 3851
		private readonly NKMEquipStatGroupTemplet[] statGroups = new NKMEquipStatGroupTemplet[2];

		// Token: 0x04000F0C RID: 3852
		public int m_ItemEquipID;

		// Token: 0x04000F0D RID: 3853
		public string m_ItemEquipStrID = "";

		// Token: 0x04000F0E RID: 3854
		public string m_ItemEquipName = "";

		// Token: 0x04000F0F RID: 3855
		public string m_ItemEquipDesc = "";

		// Token: 0x04000F10 RID: 3856
		public NKM_ITEM_GRADE m_NKM_ITEM_GRADE;

		// Token: 0x04000F11 RID: 3857
		public int m_NKM_ITEM_TIER = 1;

		// Token: 0x04000F12 RID: 3858
		public ITEM_EQUIP_POSITION m_ItemEquipPosition;

		// Token: 0x04000F13 RID: 3859
		public NKM_UNIT_STYLE_TYPE m_EquipUnitStyleType;

		// Token: 0x04000F14 RID: 3860
		public string m_ItemEquipIconName = "";

		// Token: 0x04000F15 RID: 3861
		public int m_MaxEnchantLevel = 1;

		// Token: 0x04000F16 RID: 3862
		public List<NKMEquipTemplet.OnRemoveItemData> m_OnRemoveItemList = new List<NKMEquipTemplet.OnRemoveItemData>();

		// Token: 0x04000F17 RID: 3863
		public NKM_STAT_TYPE m_StatType = NKM_STAT_TYPE.NST_END;

		// Token: 0x04000F18 RID: 3864
		public NKMEquipTemplet.ItemStatData m_StatData;

		// Token: 0x04000F19 RID: 3865
		public int m_StatGroupID;

		// Token: 0x04000F1A RID: 3866
		public int m_StatGroupID_2;

		// Token: 0x04000F1B RID: 3867
		public int m_FeedEXP;

		// Token: 0x04000F1C RID: 3868
		public int m_PrecisionReqResource;

		// Token: 0x04000F1D RID: 3869
		public int m_PrecisionReqItem;

		// Token: 0x04000F1E RID: 3870
		public int m_RandomStatReqResource;

		// Token: 0x04000F1F RID: 3871
		public int m_RandomStatReqItem;

		// Token: 0x04000F20 RID: 3872
		public int m_RandomSetReqResource;

		// Token: 0x04000F21 RID: 3873
		public int m_RandomSetReqItemID;

		// Token: 0x04000F22 RID: 3874
		public int m_RandomSetReqItemValue;

		// Token: 0x04000F23 RID: 3875
		public List<int> m_lstSetGroup;

		// Token: 0x04000F24 RID: 3876
		public bool m_bItemFirstLock;

		// Token: 0x04000F25 RID: 3877
		public bool m_bRelic;

		// Token: 0x04000F26 RID: 3878
		public int potentialOptionGroupId;

		// Token: 0x04000F27 RID: 3879
		public readonly List<MiscItemUnit>[] socketReqResource = new List<MiscItemUnit>[3];

		// Token: 0x04000F28 RID: 3880
		public bool m_bShowEffect;

		// Token: 0x04000F29 RID: 3881
		private readonly List<int> m_lstPrivateUnitID = new List<int>();

		// Token: 0x0200118C RID: 4492
		public struct ItemStatData
		{
			// Token: 0x04009295 RID: 37525
			public float m_fBaseValue;

			// Token: 0x04009296 RID: 37526
			public float m_fLevelUpValue;
		}

		// Token: 0x0200118D RID: 4493
		public struct OnRemoveItemData
		{
			// Token: 0x06009FFE RID: 40958 RVA: 0x0033D811 File Offset: 0x0033BA11
			public OnRemoveItemData(int itemID, int itemCount)
			{
				this.m_ItemID = itemID;
				this.m_ItemCount = itemCount;
			}

			// Token: 0x04009297 RID: 37527
			public int m_ItemID;

			// Token: 0x04009298 RID: 37528
			public int m_ItemCount;
		}
	}
}
