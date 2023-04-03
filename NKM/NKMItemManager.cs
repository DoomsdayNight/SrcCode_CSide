using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKC;
using NKC.UI;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000389 RID: 905
	public static class NKMItemManager
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x0005C187 File Offset: 0x0005A387
		public static IReadOnlyDictionary<int, NKMItemMiscTemplet> ResourceTemplets
		{
			get
			{
				return NKMItemManager.resourceTemplets;
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0005C18E File Offset: 0x0005A38E
		public static bool LoadFromLUA_Item_Equip(string filename)
		{
			NKMItemManager.m_dicItemEquipTempletByID = NKMTempletLoader.LoadDictionary<NKMEquipTemplet>("AB_SCRIPT_ITEM_TEMPLET", filename, "ITEM_EQUIP_TEMPLET", new Func<NKMLua, NKMEquipTemplet>(NKMEquipTemplet.LoadFromLUA));
			NKMTempletContainer<NKMEquipTemplet>.Load(NKMItemManager.m_dicItemEquipTempletByID.Values, null);
			return NKMItemManager.m_dicItemEquipTempletByID != null;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0005C1C9 File Offset: 0x0005A3C9
		public static bool LoadFromLUA_EquipEnchantExp(string filename)
		{
			NKMItemManager.m_dicEquipEnchantExpTemplet = NKMTempletLoader.LoadDictionary<NKMEquipEnchantExpTemplet>("AB_SCRIPT_ITEM_TEMPLET", filename, "EQUIP_ENCHANT_EXP_TABLE", new Func<NKMLua, NKMEquipEnchantExpTemplet>(NKMEquipEnchantExpTemplet.LoadFromLUA));
			return NKMItemManager.m_dicEquipEnchantExpTemplet != null;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0005C1F4 File Offset: 0x0005A3F4
		public static void LoadFromLUA_ITEM_MISC(string fileName)
		{
			NKMTempletContainer<NKMItemMiscTemplet>.Load("AB_SCRIPT_ITEM_TEMPLET", fileName, "ITEM_MISC_TEMPLET", new Func<NKMLua, NKMItemMiscTemplet>(NKMItemMiscTemplet.LoadFromLUA), (NKMItemMiscTemplet e) => e.m_ItemMiscStrID);
			NKMItemManager.CheckValidation();
			NKMItemManager.resourceTemplets = (from e in NKMItemMiscTemplet.Values
			where e.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_RESOURCE
			select e).ToDictionary((NKMItemMiscTemplet e) => e.Key);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0005C293 File Offset: 0x0005A493
		public static void LoadFromLua_Item_Mold(string filename)
		{
			NKMTempletContainer<NKMItemMoldTemplet>.Load("AB_SCRIPT_ITEM_TEMPLET", filename, "ITEM_MOLD_TEMPLET", new Func<NKMLua, NKMItemMoldTemplet>(NKMItemMoldTemplet.LoadFromLUA));
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0005C2B4 File Offset: 0x0005A4B4
		public static bool LoadFromLUA_EquipSetOption(string filename)
		{
			NKMItemManager.m_dicItemEquipSetOptionTempletByID = NKMTempletLoader.LoadDictionary<NKMItemEquipSetOptionTemplet>("AB_SCRIPT_ITEM_TEMPLET", filename, "ITEM_EQUIP_SET_OPTION", new Func<NKMLua, NKMItemEquipSetOptionTemplet>(NKMItemEquipSetOptionTemplet.LoadFromLUA));
			bool flag = NKMItemManager.m_dicItemEquipSetOptionTempletByID != null;
			if (flag)
			{
				NKMItemManager.m_lstItemEquipSetOptionTemplet = new List<NKMItemEquipSetOptionTemplet>(NKMItemManager.m_dicItemEquipSetOptionTempletByID.Values);
			}
			return flag;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0005C301 File Offset: 0x0005A501
		public static NKMItemMiscTemplet GetItemMiscTempletByID(int itemMiscID)
		{
			return NKMTempletContainer<NKMItemMiscTemplet>.Find(itemMiscID);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0005C30C File Offset: 0x0005A50C
		public static NKMEquipTemplet GetEquipTemplet(int equipID)
		{
			NKMEquipTemplet result;
			if (NKMItemManager.m_dicItemEquipTempletByID.TryGetValue(equipID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0005C32B File Offset: 0x0005A52B
		public static NKMItemMoldTemplet GetItemMoldTempletByID(int moldID)
		{
			return NKMItemMoldTemplet.Find(moldID);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0005C334 File Offset: 0x0005A534
		public static NKMEquipEnchantExpTemplet GetEquipEnchantExpTemplet(int tierId, int equipEnchantLevel)
		{
			int hashCode = new ValueTuple<int, int>(tierId, equipEnchantLevel).GetHashCode();
			NKMEquipEnchantExpTemplet result;
			NKMItemManager.m_dicEquipEnchantExpTemplet.TryGetValue(hashCode, out result);
			return result;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0005C368 File Offset: 0x0005A568
		public static int GetEnchantRequireExp(int target_tier, int target_level, NKM_ITEM_GRADE target_grade)
		{
			NKMEquipEnchantExpTemplet equipEnchantExpTemplet = NKMItemManager.GetEquipEnchantExpTemplet(target_tier, target_level);
			if (equipEnchantExpTemplet == null)
			{
				return -1;
			}
			switch (target_grade)
			{
			case NKM_ITEM_GRADE.NIG_N:
				return equipEnchantExpTemplet.m_ReqLevelupExp_N;
			case NKM_ITEM_GRADE.NIG_R:
				return equipEnchantExpTemplet.m_ReqLevelupExp_R;
			case NKM_ITEM_GRADE.NIG_SR:
				return equipEnchantExpTemplet.m_ReqLevelupExp_SR;
			case NKM_ITEM_GRADE.NIG_SSR:
				return equipEnchantExpTemplet.m_ReqLevelupExp_SSR;
			default:
				return -1;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
		public static int GetEquipEnchantFeedExp(NKMEquipItemData equipItemData)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return -1;
			}
			NKMEquipEnchantExpTemplet equipEnchantExpTemplet = NKMItemManager.GetEquipEnchantExpTemplet(equipTemplet.m_NKM_ITEM_TIER, equipItemData.m_EnchantLevel);
			if (equipEnchantExpTemplet == null)
			{
				return -1;
			}
			float reqEnchantFeedEXPBonusRate = equipEnchantExpTemplet.m_ReqEnchantFeedEXPBonusRate;
			return (int)((float)equipTemplet.m_FeedEXP * reqEnchantFeedEXPBonusRate);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0005C400 File Offset: 0x0005A600
		public static NKM_ERROR_CODE CanEnchantItem(NKMUserData userdata, NKMEquipItemData target_equip_item)
		{
			NKMDeckData deckDataByUnitUID = userdata.m_ArmyData.GetDeckDataByUnitUID(target_equip_item.m_OwnerUnitUID);
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

		// Token: 0x0600170B RID: 5899 RVA: 0x0005C444 File Offset: 0x0005A644
		public static NKM_ERROR_CODE CanEnchantItem(NKMUserData userdata, NKMEquipItemData target_equip_item, List<long> material_item_list)
		{
			if (material_item_list == null || material_item_list.Count <= 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_UID;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(userdata, target_equip_item);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			int num = 0;
			foreach (long num2 in material_item_list)
			{
				if (num2 == target_equip_item.m_ItemUid)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_UID;
				}
				NKMEquipItemData itemEquip = userdata.m_InventoryData.GetItemEquip(num2);
				if (itemEquip == null)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_UID;
				}
				nkm_ERROR_CODE = NKMItemManager.CanRemoveItem(itemEquip);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
				int equipEnchantFeedExp = NKMItemManager.GetEquipEnchantFeedExp(itemEquip);
				if (equipEnchantFeedExp == -1)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_EQUIP_ITEM;
				}
				num += equipEnchantFeedExp;
			}
			int num3 = num * 8;
			if (userdata.GetCredit() < (long)num3)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0005C520 File Offset: 0x0005A720
		public static NKM_ERROR_CODE CanRemoveItem(NKMEquipItemData equip_item_data)
		{
			if (equip_item_data.m_bLock)
			{
				return NKM_ERROR_CODE.NEC_FAIL_ITEM_LOCKED;
			}
			if (equip_item_data.m_OwnerUnitUID > 0L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_EQUIP_ITEM;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0005C544 File Offset: 0x0005A744
		public static bool IsRedudantItemProhibited(NKM_REWARD_TYPE rewardType, int itemID)
		{
			if (rewardType != NKM_REWARD_TYPE.RT_MISC)
			{
				return rewardType == NKM_REWARD_TYPE.RT_SKIN || rewardType == NKM_REWARD_TYPE.RT_EMOTICON;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			return itemMiscTempletByID != null && NKMItemManager.IsRedudantItemProhibited(itemMiscTempletByID.m_ItemMiscType, itemMiscTempletByID.m_ItemMiscSubType);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0005C581 File Offset: 0x0005A781
		public static bool IsRedudantItemProhibited(NKM_ITEM_MISC_TYPE itemType, NKM_ITEM_MISC_SUBTYPE subType)
		{
			return itemType - NKM_ITEM_MISC_TYPE.IMT_EMBLEM <= 1 || itemType - NKM_ITEM_MISC_TYPE.IMT_BACKGROUND <= 1 || (itemType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR && subType == NKM_ITEM_MISC_SUBTYPE.IMST_INTERIOR_DECO);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0005C5A0 File Offset: 0x0005A7A0
		public static NKMItemEquipSetOptionTemplet GetEquipSetOptionTemplet(int optionID)
		{
			if (NKMItemManager.m_lstItemEquipSetOptionTemplet != null && NKMItemManager.m_lstItemEquipSetOptionTemplet.Count > 0)
			{
				return NKMItemManager.m_lstItemEquipSetOptionTemplet.Find((NKMItemEquipSetOptionTemplet e) => e.m_EquipSetID == optionID);
			}
			return null;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0005C5E6 File Offset: 0x0005A7E6
		public static List<NKMItemEquipSetOptionTemplet> GetActivatedSetItem(NKMUnitData unitData, NKMInventoryData inventoryData)
		{
			return NKMItemManager.GetActivatedSetItem(unitData.GetEquipmentSet(inventoryData));
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005C5F4 File Offset: 0x0005A7F4
		public static List<NKMItemEquipSetOptionTemplet> GetActivatedSetItem(NKMEquipmentSet equipmentSet)
		{
			List<NKMItemEquipSetOptionTemplet> list = new List<NKMItemEquipSetOptionTemplet>();
			if (equipmentSet == null)
			{
				return list;
			}
			List<NKMEquipItemData> list2 = new List<NKMEquipItemData>();
			if (equipmentSet.Weapon != null && equipmentSet.Weapon.m_SetOptionId != 0)
			{
				list2.Add(equipmentSet.Weapon);
			}
			if (equipmentSet.Defence != null && equipmentSet.Defence.m_SetOptionId != 0)
			{
				list2.Add(equipmentSet.Defence);
			}
			if (equipmentSet.Accessory != null && equipmentSet.Accessory.m_SetOptionId != 0)
			{
				list2.Add(equipmentSet.Accessory);
			}
			if (equipmentSet.Accessory2 != null && equipmentSet.Accessory2.m_SetOptionId != 0)
			{
				list2.Add(equipmentSet.Accessory2);
			}
			using (List<NKMEquipItemData>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMEquipItemData equipItem = enumerator.Current;
					if (list.Find((NKMItemEquipSetOptionTemplet e) => e.m_EquipSetID == equipItem.m_SetOptionId) == null)
					{
						List<NKMEquipItemData> list3 = list2.FindAll((NKMEquipItemData e) => e.m_SetOptionId == equipItem.m_SetOptionId);
						NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(equipItem.m_SetOptionId);
						if (equipSetOptionTemplet == null)
						{
							Log.Error("SetOptionTemplet not exist! SetOptionID : " + equipItem.m_SetOptionId.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 701);
						}
						else
						{
							int num = list3.Count / equipSetOptionTemplet.m_EquipSetPart;
							for (int i = 0; i < num; i++)
							{
								list.Add(equipSetOptionTemplet);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005C778 File Offset: 0x0005A978
		public static HashSet<long> GetSetItemActivatedMark(NKMEquipmentSet equipmentSet)
		{
			HashSet<long> hashSet = new HashSet<long>();
			if (equipmentSet == null)
			{
				return hashSet;
			}
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			if (equipmentSet.Weapon != null && equipmentSet.Weapon.m_SetOptionId != 0)
			{
				list.Add(equipmentSet.Weapon);
			}
			if (equipmentSet.Defence != null && equipmentSet.Defence.m_SetOptionId != 0)
			{
				list.Add(equipmentSet.Defence);
			}
			if (equipmentSet.Accessory != null && equipmentSet.Accessory.m_SetOptionId != 0)
			{
				list.Add(equipmentSet.Accessory);
			}
			if (equipmentSet.Accessory2 != null && equipmentSet.Accessory2.m_SetOptionId != 0)
			{
				list.Add(equipmentSet.Accessory2);
			}
			HashSet<long> hashSet2 = new HashSet<long>();
			using (List<NKMEquipItemData>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMEquipItemData equipItem = enumerator.Current;
					if (!hashSet2.Contains(equipItem.m_ItemUid))
					{
						List<NKMEquipItemData> list2 = list.FindAll((NKMEquipItemData e) => e.m_SetOptionId == equipItem.m_SetOptionId);
						NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(equipItem.m_SetOptionId);
						if (equipSetOptionTemplet == null)
						{
							Log.Error("SetOptionTemplet not exist! SetOptionID : " + equipItem.m_SetOptionId.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 750);
						}
						else
						{
							int num = list2.Count / equipSetOptionTemplet.m_EquipSetPart;
							for (int i = 0; i < list2.Count; i++)
							{
								hashSet2.Add(list2[i].m_ItemUid);
								if (i < num * equipSetOptionTemplet.m_EquipSetPart)
								{
									hashSet.Add(list2[i].m_ItemUid);
								}
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0005C93C File Offset: 0x0005AB3C
		private static void CheckValidation()
		{
			if (NKMItemManager.GetItemMiscTempletByID(1) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 크레딧 자원이 존재하지 않음 m_ItemMiscID : {0}", 1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 774);
			}
			if (NKMItemManager.GetItemMiscTempletByID(2) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 이터니움 자원이 존재하지 않음 m_ItemMiscID : {0}", 2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 779);
			}
			if (NKMItemManager.GetItemMiscTempletByID(3) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 정보 자원이 존재하지 않음 m_ItemMiscID : {0}", 3), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 784);
			}
			if (NKMItemManager.GetItemMiscTempletByID(4) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 연수티켓 자원이 존재하지 않음 m_ItemMiscID : {0}", 4), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 789);
			}
			if (NKMItemManager.GetItemMiscTempletByID(15) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 공격 모의작전 연수티켓 자원이 존재하지 않음 m_ItemMiscID : {0}", 15), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 794);
			}
			if (NKMItemManager.GetItemMiscTempletByID(16) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 방어 모의작전 연수티켓 자원이 존재하지 않음 m_ItemMiscID : {0}", 16), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 799);
			}
			if (NKMItemManager.GetItemMiscTempletByID(17) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 대공 모의작전 연수티켓 자원이 존재하지 않음 m_ItemMiscID : {0}", 17), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 804);
			}
			if (NKMItemManager.GetItemMiscTempletByID(11) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 허수코어 자원이 존재하지 않음 m_ItemMiscID : {0}", 11), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 809);
			}
			if (NKMItemManager.GetItemMiscTempletByID(101) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 캐시 자원이 존재하지 않음 m_ItemMiscID : {0}", 101), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 814);
			}
			if (NKMItemManager.GetItemMiscTempletByID(202) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 업적 점수 자원이 존재하지 않음 m_ItemMiscID : {0}", 202), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 819);
			}
			if (NKMItemManager.GetItemMiscTempletByID(501) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 사장 경험치 자원이 존재하지 않음 m_ItemMiscID : {0}", 501), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 824);
			}
			if (NKMItemManager.GetItemMiscTempletByID(1012) == null)
			{
				Log.ErrorAndExit(string.Format("[ItemMiscTemplet] 즉시 제작권 아이템이 존재하지 않음 m_ItemMiscID : {0}", 1012), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 829);
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005CB40 File Offset: 0x0005AD40
		public static int GetEnchantRequireExp(NKMEquipItemData equipItemData)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				Log.ErrorAndExit(string.Format("[NKMEquipTemplet] Fail - Item Equip ID : {0}", equipItemData.m_ItemEquipID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 394);
				return -1;
			}
			return NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, equipItemData.m_EnchantLevel, equipTemplet.m_NKM_ITEM_GRADE);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0005CB9C File Offset: 0x0005AD9C
		public static int GetMaxEquipEnchantLevel(int equipTier)
		{
			int num = 0;
			while (NKMItemManager.GetEquipEnchantExpTemplet(equipTier, num) != null)
			{
				num++;
			}
			return num - 1;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005CBC0 File Offset: 0x0005ADC0
		public static int GetMaxEquipEnchantExp(int equipID)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
			if (equipTemplet == null)
			{
				return 0;
			}
			int num = 0;
			int maxEquipEnchantLevel = NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER);
			int num2 = 0;
			while (num <= maxEquipEnchantLevel && NKMItemManager.GetEquipEnchantExpTemplet(equipTemplet.m_NKM_ITEM_TIER, num) != null)
			{
				int enchantRequireExp = NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, num, equipTemplet.m_NKM_ITEM_GRADE);
				if (enchantRequireExp > 0)
				{
					num2 += enchantRequireExp;
				}
				num++;
			}
			return num2;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0005CC20 File Offset: 0x0005AE20
		public static int GetNeedExpToMaxLevel(NKMEquipItemData equipData)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return 0;
			}
			int maxEquipEnchantLevel = NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER);
			int num = NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, equipData.m_EnchantLevel, equipTemplet.m_NKM_ITEM_GRADE) - equipData.m_EnchantExp;
			for (int i = equipData.m_EnchantLevel + 1; i < maxEquipEnchantLevel; i++)
			{
				num += NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, i, equipTemplet.m_NKM_ITEM_GRADE);
			}
			return num;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005CC94 File Offset: 0x0005AE94
		public static List<NKMItemMiscTemplet> GetItemMiscTempletListByType(NKM_ITEM_MISC_TYPE type)
		{
			return (from e in NKMItemMiscTemplet.Values
			where e.m_ItemMiscType == type
			select e).ToList<NKMItemMiscTemplet>();
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0005CCCC File Offset: 0x0005AECC
		public static List<NKMMoldItemData> GetMoldItemData(NKM_CRAFT_TAB_TYPE type)
		{
			List<NKMMoldItemData> list = new List<NKMMoldItemData>();
			foreach (NKMItemMoldTemplet nkmitemMoldTemplet in NKMTempletContainer<NKMItemMoldTemplet>.Values)
			{
				if (nkmitemMoldTemplet.EnableByTag && nkmitemMoldTemplet.m_TabType == type)
				{
					long count = 0L;
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData != null && nkmuserData.m_CraftData != null)
					{
						count = nkmuserData.m_CraftData.GetMoldCount(nkmitemMoldTemplet.m_MoldID);
					}
					list.Add(new NKMMoldItemData(nkmitemMoldTemplet.m_MoldID, count));
				}
			}
			return list;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0005CD68 File Offset: 0x0005AF68
		public static NKMItemMiscTemplet GetItemMiscTempletByRewardType(NKM_REWARD_TYPE type)
		{
			if (type == NKM_REWARD_TYPE.RT_USER_EXP)
			{
				return NKMItemMiscTemplet.Find(501);
			}
			return null;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0005CD7C File Offset: 0x0005AF7C
		public static int GetMoldCount(NKM_CRAFT_TAB_TYPE type = NKM_CRAFT_TAB_TYPE.MT_EQUIP)
		{
			int num = 0;
			foreach (NKMItemMoldTemplet nkmitemMoldTemplet in NKMTempletContainer<NKMItemMoldTemplet>.Values)
			{
				if (nkmitemMoldTemplet.EnableByTag && nkmitemMoldTemplet.m_TabType == type)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0005CDDC File Offset: 0x0005AFDC
		public static NKCRandomMoldBoxTemplet GetRandomMoldBoxTemplet(int rewardID)
		{
			foreach (KeyValuePair<int, NKCRandomMoldBoxTemplet> keyValuePair in NKMItemManager.m_dicRandomMoldBoxTemplet)
			{
				if (keyValuePair.Value.m_RewardID == rewardID)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0005CE44 File Offset: 0x0005B044
		public static Dictionary<int, NKCRandomMoldBoxTemplet> RandomMoldBoxTemplet
		{
			get
			{
				return NKMItemManager.m_dicRandomMoldBoxTemplet;
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005CE4C File Offset: 0x0005B04C
		public static bool LoadFromLua_Random_Mold_Box(string filename)
		{
			NKMItemManager.m_dicRandomMoldBoxTemplet = NKMTempletLoader.LoadDictionary<NKCRandomMoldBoxTemplet>("AB_SCRIPT_ITEM_TEMPLET", filename, "RANDOM_MOLD_BOX", new Func<NKMLua, NKCRandomMoldBoxTemplet>(NKCRandomMoldBoxTemplet.LoadFromLUA));
			if (NKMItemManager.m_dicRandomMoldBoxTemplet != null)
			{
				NKMItemManager.m_dicRandomMoldBox = new Dictionary<int, List<int>>();
				foreach (KeyValuePair<int, NKCRandomMoldBoxTemplet> keyValuePair in NKMItemManager.m_dicRandomMoldBoxTemplet)
				{
					if (keyValuePair.Value != null)
					{
						if (!NKMItemManager.m_dicRandomMoldBox.ContainsKey(keyValuePair.Value.m_RewardGroupID))
						{
							NKMItemManager.m_dicRandomMoldBox.Add(keyValuePair.Value.m_RewardGroupID, new List<int>
							{
								keyValuePair.Value.m_RewardID
							});
						}
						else
						{
							NKMItemManager.m_dicRandomMoldBox[keyValuePair.Value.m_RewardGroupID].Add(keyValuePair.Value.m_RewardID);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<int>> keyValuePair2 in NKMItemManager.m_dicRandomMoldBox)
			{
				if (keyValuePair2.Value != null)
				{
					keyValuePair2.Value.Sort(new CompTemplet.CompNET());
				}
			}
			Debug.Log(string.Format("LoadFromLua_Random_Mold_Box : result - {0}", NKMItemManager.m_dicRandomMoldBox.Count));
			return true;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0005CFBC File Offset: 0x0005B1BC
		public static Dictionary<int, NKCRandomMoldTabTemplet> MoldTapTemplet
		{
			get
			{
				return NKMItemManager.m_dicMoldTabTemplet;
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0005CFC4 File Offset: 0x0005B1C4
		public static bool LoadFromLua_Item_Mold_Tab(string filename)
		{
			NKMItemManager.m_dicMoldTabTemplet = NKMTempletLoader.LoadDictionary<NKCRandomMoldTabTemplet>("AB_SCRIPT_ITEM_TEMPLET", filename, "ITEM_MOLD_TAB", new Func<NKMLua, NKCRandomMoldTabTemplet>(NKCRandomMoldTabTemplet.LoadFromLUA));
			Dictionary<int, NKCRandomMoldBoxTemplet> dicRandomMoldBoxTemplet = NKMItemManager.m_dicRandomMoldBoxTemplet;
			Debug.Log(string.Format("LoadFromLua_Random_Mold_Box : result - {0}", NKMItemManager.m_dicRandomMoldBox.Count));
			return true;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0005D017 File Offset: 0x0005B217
		public static Dictionary<int, NKCEquipAutoWeightTemplet> AutoWeightTemplet
		{
			get
			{
				return NKMItemManager.m_dicAutoWeightTemplet;
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0005D020 File Offset: 0x0005B220
		public static bool LoadFromLua_Item_AutoWeight(string filename)
		{
			NKMItemManager.m_dicAutoWeightTemplet = NKMTempletLoader.LoadDictionary<NKCEquipAutoWeightTemplet>("AB_SCRIPT_ITEM_TEMPLET", filename, "EQUIP_AUTO_WEIGHT", new Func<NKMLua, NKCEquipAutoWeightTemplet>(NKCEquipAutoWeightTemplet.LoadFromLUA));
			Dictionary<int, NKCEquipAutoWeightTemplet> dicAutoWeightTemplet = NKMItemManager.m_dicAutoWeightTemplet;
			Debug.Log(string.Format("LoadFromLua_Item_AutoWeight : result - {0}", NKMItemManager.m_dicAutoWeightTemplet.Count));
			return true;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005D074 File Offset: 0x0005B274
		public static float GetOptionWeight(NKM_STAT_TYPE targetOption, NKM_UNIT_ROLE_TYPE unitRole)
		{
			if (!NKMItemManager.m_dicAutoWeightTemplet.ContainsKey((int)targetOption))
			{
				return 0f;
			}
			NKCEquipAutoWeightTemplet nkcequipAutoWeightTemplet = NKMItemManager.m_dicAutoWeightTemplet[(int)targetOption];
			switch (unitRole)
			{
			case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
				return nkcequipAutoWeightTemplet.NURT_INVALID;
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				return nkcequipAutoWeightTemplet.NURT_STRIKER;
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				return nkcequipAutoWeightTemplet.NURT_RANGER;
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				return nkcequipAutoWeightTemplet.NURT_DEFENDER;
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				return nkcequipAutoWeightTemplet.NURT_SNIPER;
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
				return nkcequipAutoWeightTemplet.NURT_SUPPORTER;
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
				return nkcequipAutoWeightTemplet.NURT_SIEGE;
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				return nkcequipAutoWeightTemplet.NURT_TOWER;
			default:
				return 0f;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005D108 File Offset: 0x0005B308
		public static string GetSetOptionDescription(NKM_STAT_TYPE type, float fRateValue, float fValue)
		{
			if (type <= NKM_STAT_TYPE.NST_EVADE)
			{
				return string.Format(NKCUtilString.GetStatShortName(type, fRateValue) + " {0:P1}", fRateValue);
			}
			if (NKCUtilString.IsNameReversedIfNegative(type) && fValue < 0f)
			{
				return string.Format(NKCUtilString.GetStatShortName(type, fValue) + " {0:P1}", -fValue);
			}
			return string.Format(NKCUtilString.GetStatShortName(type) + " {0:P1}", fValue);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005D17F File Offset: 0x0005B37F
		public static bool IsActiveSetOptionItem(long itemUID)
		{
			return NKMItemManager.IsActiveSetOptionItem(NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(itemUID));
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0005D198 File Offset: 0x0005B398
		public static bool IsActiveSetOptionItem(NKMEquipItemData itemData)
		{
			if (itemData == null)
			{
				return false;
			}
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(itemData.m_SetOptionId);
			if (equipSetOptionTemplet == null)
			{
				return false;
			}
			if (equipSetOptionTemplet.m_EquipSetPart == 1)
			{
				return true;
			}
			List<long> matchedSetOptionItemList = NKMItemManager.GetMatchedSetOptionItemList(itemData.m_ItemUid);
			if (matchedSetOptionItemList.Count < equipSetOptionTemplet.m_EquipSetPart)
			{
				return false;
			}
			if (matchedSetOptionItemList.Count == equipSetOptionTemplet.m_EquipSetPart)
			{
				return true;
			}
			Predicate<long> <>9__0;
			while (matchedSetOptionItemList.Count >= equipSetOptionTemplet.m_EquipSetPart)
			{
				List<long> range = matchedSetOptionItemList.GetRange(0, equipSetOptionTemplet.m_EquipSetPart);
				Predicate<long> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((long e) => e == itemData.m_ItemUid));
				}
				if (range.FindIndex(match) >= 0)
				{
					return true;
				}
				matchedSetOptionItemList.RemoveRange(0, equipSetOptionTemplet.m_EquipSetPart);
			}
			return false;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005D260 File Offset: 0x0005B460
		public static int GetMatchingSetOptionItem(NKMEquipItemData itemData)
		{
			if (itemData == null)
			{
				return 0;
			}
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(itemData.m_SetOptionId);
			if (equipSetOptionTemplet == null)
			{
				return 0;
			}
			if (equipSetOptionTemplet.m_EquipSetPart == 1)
			{
				return 1;
			}
			List<long> matchedSetOptionItemList = NKMItemManager.GetMatchedSetOptionItemList(itemData.m_ItemUid);
			if (matchedSetOptionItemList.Count < equipSetOptionTemplet.m_EquipSetPart)
			{
				return matchedSetOptionItemList.Count;
			}
			if (matchedSetOptionItemList.Count == equipSetOptionTemplet.m_EquipSetPart)
			{
				return equipSetOptionTemplet.m_EquipSetPart;
			}
			Predicate<long> <>9__0;
			while (matchedSetOptionItemList.Count >= equipSetOptionTemplet.m_EquipSetPart)
			{
				List<long> range = matchedSetOptionItemList.GetRange(0, equipSetOptionTemplet.m_EquipSetPart);
				List<long> list = range;
				Predicate<long> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((long e) => e == itemData.m_ItemUid));
				}
				if (list.FindIndex(match) >= 0)
				{
					return range.Count;
				}
				matchedSetOptionItemList.RemoveRange(0, equipSetOptionTemplet.m_EquipSetPart);
			}
			return matchedSetOptionItemList.Count;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005D340 File Offset: 0x0005B540
		public static int GetExpactSetOptionMatchingCnt(NKMEquipItemData itemData, int newSetOptionID)
		{
			if (itemData == null)
			{
				return 1;
			}
			if (itemData.m_OwnerUnitUID <= 0L)
			{
				return 1;
			}
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(newSetOptionID);
			if (equipSetOptionTemplet == null)
			{
				return 1;
			}
			List<long> matchedSetOptionItemList = NKMItemManager.GetMatchedSetOptionItemList(itemData, newSetOptionID, true);
			if (matchedSetOptionItemList.Count == equipSetOptionTemplet.m_EquipSetPart)
			{
				return equipSetOptionTemplet.m_EquipSetPart;
			}
			Predicate<long> <>9__0;
			while (matchedSetOptionItemList.Count >= equipSetOptionTemplet.m_EquipSetPart)
			{
				List<long> range = matchedSetOptionItemList.GetRange(0, equipSetOptionTemplet.m_EquipSetPart);
				List<long> list = range;
				Predicate<long> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((long e) => e == itemData.m_ItemUid));
				}
				if (list.FindIndex(match) >= 0)
				{
					return range.Count;
				}
				matchedSetOptionItemList.RemoveRange(0, equipSetOptionTemplet.m_EquipSetPart);
			}
			return matchedSetOptionItemList.Count;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005D404 File Offset: 0x0005B604
		private static List<long> GetMatchedSetOptionItemList(long itemUID)
		{
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(itemUID);
			if (itemEquip == null)
			{
				return new List<long>();
			}
			return NKMItemManager.GetMatchingItemList(itemEquip);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005D434 File Offset: 0x0005B634
		private static List<long> GetMatchingItemList(NKMEquipItemData itemData)
		{
			List<long> list = new List<long>();
			if (NKMItemManager.GetEquipSetOptionTemplet(itemData.m_SetOptionId) == null)
			{
				return list;
			}
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(itemData.m_OwnerUnitUID);
			if (unitFromUID == null)
			{
				return list;
			}
			NKMEquipmentSet equipmentSet = unitFromUID.GetEquipmentSet(NKCScenManager.CurrentUserData().m_InventoryData);
			if (equipmentSet == null)
			{
				return list;
			}
			if (equipmentSet.Weapon != null && equipmentSet.Weapon.m_SetOptionId == itemData.m_SetOptionId)
			{
				list.Add(equipmentSet.Weapon.m_ItemUid);
			}
			if (equipmentSet.Defence != null && equipmentSet.Defence.m_SetOptionId == itemData.m_SetOptionId)
			{
				list.Add(equipmentSet.Defence.m_ItemUid);
			}
			if (equipmentSet.Accessory != null && equipmentSet.Accessory.m_SetOptionId == itemData.m_SetOptionId)
			{
				list.Add(equipmentSet.Accessory.m_ItemUid);
			}
			if (equipmentSet.Accessory2 != null && equipmentSet.Accessory2.m_SetOptionId == itemData.m_SetOptionId)
			{
				list.Add(equipmentSet.Accessory2.m_ItemUid);
			}
			return list;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005D540 File Offset: 0x0005B740
		private static List<long> GetMatchedSetOptionItemList(NKMEquipItemData itemData, int SetOptionID, bool IgnoreEquipType = false)
		{
			List<long> list = new List<long>();
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(itemData.m_OwnerUnitUID);
			if (unitFromUID != null)
			{
				NKMEquipmentSet equipmentSet = unitFromUID.GetEquipmentSet(NKCScenManager.CurrentUserData().m_InventoryData);
				if (equipmentSet != null)
				{
					if (equipmentSet.Weapon != null && (equipmentSet.Weapon.m_SetOptionId == SetOptionID || (IgnoreEquipType && itemData.m_ItemUid == equipmentSet.Weapon.m_ItemUid)))
					{
						list.Add(equipmentSet.Weapon.m_ItemUid);
					}
					if (equipmentSet.Defence != null && (equipmentSet.Defence.m_SetOptionId == SetOptionID || (IgnoreEquipType && itemData.m_ItemUid == equipmentSet.Defence.m_ItemUid)))
					{
						list.Add(equipmentSet.Defence.m_ItemUid);
					}
					if (equipmentSet.Accessory != null && (equipmentSet.Accessory.m_SetOptionId == SetOptionID || (IgnoreEquipType && itemData.m_ItemUid == equipmentSet.Accessory.m_ItemUid)))
					{
						list.Add(equipmentSet.Accessory.m_ItemUid);
					}
					if (equipmentSet.Accessory2 != null && (equipmentSet.Accessory2.m_SetOptionId == SetOptionID || (IgnoreEquipType && itemData.m_ItemUid == equipmentSet.Accessory2.m_ItemUid)))
					{
						list.Add(equipmentSet.Accessory2.m_ItemUid);
					}
				}
			}
			return list;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005D680 File Offset: 0x0005B880
		public static ITEM_EQUIP_POSITION GetItemEquipPosition(long itemUID)
		{
			return NKMItemManager.GetItemEquipPosition(NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(itemUID));
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0005D698 File Offset: 0x0005B898
		public static ITEM_EQUIP_POSITION GetItemEquipPosition(NKMEquipItemData eqipItem)
		{
			ITEM_EQUIP_POSITION result = ITEM_EQUIP_POSITION.IEP_NONE;
			if (eqipItem != null && eqipItem.m_OwnerUnitUID > 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(eqipItem.m_OwnerUnitUID);
				if (unitFromUID != null)
				{
					if (unitFromUID.GetEquipItemWeaponUid() == eqipItem.m_ItemUid)
					{
						result = ITEM_EQUIP_POSITION.IEP_WEAPON;
					}
					else if (unitFromUID.GetEquipItemDefenceUid() == eqipItem.m_ItemUid)
					{
						result = ITEM_EQUIP_POSITION.IEP_DEFENCE;
					}
					else if (unitFromUID.GetEquipItemAccessoryUid() == eqipItem.m_ItemUid)
					{
						result = ITEM_EQUIP_POSITION.IEP_ACC;
					}
					else if (unitFromUID.GetEquipItemAccessory2Uid() == eqipItem.m_ItemUid)
					{
						result = ITEM_EQUIP_POSITION.IEP_ACC2;
					}
				}
			}
			return result;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0005D71C File Offset: 0x0005B91C
		public static NKM_ERROR_CODE CanEnchantItem(NKMUserData userdata, long itemUID)
		{
			NKMEquipItemData itemEquip = userdata.m_InventoryData.GetItemEquip(itemUID);
			return NKMItemManager.CanEnchantItem(userdata, itemEquip);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0005D740 File Offset: 0x0005B940
		public static void UnEquip(long itemUID)
		{
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(itemUID);
			if (itemEquip == null)
			{
				return;
			}
			if (itemEquip.m_OwnerUnitUID > 0L)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
				if (equipTemplet == null)
				{
					return;
				}
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
				if (unitFromUID == null)
				{
					return;
				}
				NKM_ERROR_CODE nkm_ERROR_CODE = equipTemplet.CanUnEquipByUnit(NKCScenManager.GetScenManager().GetMyUserData(), unitFromUID);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				ITEM_EQUIP_POSITION itemEquipPosition = NKMItemManager.GetItemEquipPosition(itemEquip.m_ItemUid);
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, itemEquip.m_OwnerUnitUID, itemEquip.m_ItemUid, itemEquipPosition);
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0005D7E8 File Offset: 0x0005B9E8
		public static NKC_EQUIP_UPGRADE_STATE CanUpgradeEquipByCoreID(NKMEquipItemData coreEquipData)
		{
			if (coreEquipData == null)
			{
				return NKC_EQUIP_UPGRADE_STATE.NOT_HAVE;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(coreEquipData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return NKC_EQUIP_UPGRADE_STATE.NONE;
			}
			if (coreEquipData.m_EnchantLevel < equipTemplet.m_MaxEnchantLevel)
			{
				return NKC_EQUIP_UPGRADE_STATE.NEED_ENHANCE;
			}
			if (coreEquipData.m_Precision < 100 || coreEquipData.m_Precision2 < 100)
			{
				return NKC_EQUIP_UPGRADE_STATE.NEED_PRECISION;
			}
			return NKC_EQUIP_UPGRADE_STATE.UPGRADABLE;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005D834 File Offset: 0x0005BA34
		public static NKC_EQUIP_UPGRADE_STATE GetSetUpgradeSlotState(NKMItemEquipUpgradeTemplet upgradeTemplet, ref List<NKMEquipItemData> lstCoreEquipData)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(upgradeTemplet.CoreEquipTemplet.m_ItemEquipID);
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			lstCoreEquipData = new List<NKMEquipItemData>();
			bool flag = false;
			foreach (KeyValuePair<long, NKMEquipItemData> keyValuePair in inventoryData.EquipItems)
			{
				bool flag2 = false;
				bool flag3 = false;
				if (keyValuePair.Value.m_ItemEquipID == equipTemplet.m_ItemEquipID)
				{
					if (keyValuePair.Value.m_EnchantLevel == NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_MaxEnchantLevel))
					{
						flag2 = true;
					}
					if (keyValuePair.Value.m_Precision >= 100 && keyValuePair.Value.m_Precision2 >= 100)
					{
						flag3 = true;
					}
					if (flag2 && flag3)
					{
						flag = true;
					}
					lstCoreEquipData.Add(keyValuePair.Value);
				}
			}
			if (lstCoreEquipData.Count == 0)
			{
				return NKC_EQUIP_UPGRADE_STATE.NOT_HAVE;
			}
			if (flag)
			{
				return NKC_EQUIP_UPGRADE_STATE.UPGRADABLE;
			}
			return NKC_EQUIP_UPGRADE_STATE.NEED_ENHANCE;
		}

		// Token: 0x04000F95 RID: 3989
		public const int IMI_ITEM_MISC_MAKE_WARFARE_REPAIR = 1016;

		// Token: 0x04000F96 RID: 3990
		public const int IMI_ITEM_MISC_MAKE_WARFARE_SUPPLY = 1017;

		// Token: 0x04000F97 RID: 3991
		public const int NKM_ENCHANT_CREDIT_MULTI_VALUE = 8;

		// Token: 0x04000F98 RID: 3992
		private static Dictionary<int, NKMItemMiscTemplet> resourceTemplets;

		// Token: 0x04000F99 RID: 3993
		private static Dictionary<int, NKMEquipEnchantExpTemplet> m_dicEquipEnchantExpTemplet = null;

		// Token: 0x04000F9A RID: 3994
		public static Dictionary<int, NKMEquipTemplet> m_dicItemEquipTempletByID = null;

		// Token: 0x04000F9B RID: 3995
		public static Dictionary<int, List<int>> m_dicMoldReward = new Dictionary<int, List<int>>();

		// Token: 0x04000F9C RID: 3996
		public static Dictionary<int, NKMItemEquipSetOptionTemplet> m_dicItemEquipSetOptionTempletByID = null;

		// Token: 0x04000F9D RID: 3997
		public static List<NKMItemEquipSetOptionTemplet> m_lstItemEquipSetOptionTemplet = null;

		// Token: 0x04000F9E RID: 3998
		private static Dictionary<int, NKCRandomMoldBoxTemplet> m_dicRandomMoldBoxTemplet = null;

		// Token: 0x04000F9F RID: 3999
		public static Dictionary<int, List<int>> m_dicRandomMoldBox = null;

		// Token: 0x04000FA0 RID: 4000
		private static Dictionary<int, NKCRandomMoldTabTemplet> m_dicMoldTabTemplet = null;

		// Token: 0x04000FA1 RID: 4001
		private static Dictionary<int, NKCEquipAutoWeightTemplet> m_dicAutoWeightTemplet = null;
	}
}
