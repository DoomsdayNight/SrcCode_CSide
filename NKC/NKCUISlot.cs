using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Contract;
using ClientPacket.Office;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Contract;
using NKC.UI.Result;
using NKC.UI.Tooltip;
using NKM;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E9 RID: 2537
	public class NKCUISlot : MonoBehaviour
	{
		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06006D22 RID: 27938 RVA: 0x0023C42C File Offset: 0x0023A62C
		// (set) Token: 0x06006D23 RID: 27939 RVA: 0x0023C434 File Offset: 0x0023A634
		public bool IsLocked { get; private set; }

		// Token: 0x06006D24 RID: 27940 RVA: 0x0023C43D File Offset: 0x0023A63D
		public void Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE type)
		{
			this.m_EQUIP_BOX_BOTTOM_MENU_TYPE = type;
		}

		// Token: 0x06006D25 RID: 27941 RVA: 0x0023C446 File Offset: 0x0023A646
		public void SetUseBigImg(bool bSet)
		{
			this.m_bUseBigImg = bSet;
		}

		// Token: 0x06006D26 RID: 27942 RVA: 0x0023C44F File Offset: 0x0023A64F
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
		}

		// Token: 0x06006D27 RID: 27943 RVA: 0x0023C45C File Offset: 0x0023A65C
		public int GetCount()
		{
			return int.Parse(this.m_lbItemCount.text);
		}

		// Token: 0x06006D28 RID: 27944 RVA: 0x0023C46E File Offset: 0x0023A66E
		public void SetActiveCount(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_lbItemCount, value);
			NKCUtil.SetGameobjectActive(this.m_objItemCount, value);
		}

		// Token: 0x06006D29 RID: 27945 RVA: 0x0023C488 File Offset: 0x0023A688
		public void SetNewCountAni(long newCount, float fDuration = 0.4f)
		{
			this.m_lbItemCount.DOText(newCount.ToString(), fDuration, true, ScrambleMode.Numerals, null);
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x0023C4A1 File Offset: 0x0023A6A1
		public void SetBGVisible(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_imgBG, bSet);
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x0023C4B0 File Offset: 0x0023A6B0
		public void SetAlphaColorDisableImg(float fVal)
		{
			Image component = this.m_AB_ICON_SLOT_DISABLE.GetComponent<Image>();
			if (null == component)
			{
				return;
			}
			Color color = component.color;
			color.a = fVal;
			component.color = color;
		}

		// Token: 0x06006D2C RID: 27948 RVA: 0x0023C4E9 File Offset: 0x0023A6E9
		public void SetBonusRate(int bonusRate)
		{
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, bonusRate > 0);
			if (bonusRate > 0)
			{
				NKCUtil.SetLabelText(this.m_lbItemAddCount, string.Format("(+{0}%)", bonusRate));
			}
		}

		// Token: 0x06006D2D RID: 27949 RVA: 0x0023C51C File Offset: 0x0023A71C
		public static NKCUISlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_INVEN_ICON", "AB_ICON_SLOT", false, null);
			NKCUISlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUISlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUISlot Prefab null!");
				return null;
			}
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			component.Init();
			return component;
		}

		// Token: 0x06006D2E RID: 27950 RVA: 0x0023C5C8 File Offset: 0x0023A7C8
		public void Init()
		{
			this.m_cbtnButton.PointerClick.RemoveAllListeners();
			this.m_cbtnButton.PointerClick.AddListener(new UnityAction(this.OnPress));
			this.m_cbtnButton.PointerDown.RemoveAllListeners();
			this.m_cbtnButton.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointerDown));
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, false);
			NKCUtil.SetGameobjectActive(this.m_objCompleteMark, false);
			NKCUtil.SetGameobjectActive(this.m_objFirstClear, false);
			NKCUtil.SetGameobjectActive(this.m_objHaveCount, false);
			NKCUtil.SetGameobjectActive(this.m_objTopNotice, false);
			this.SetUsable(false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_EQUIP_SET_ICON.gameObject, false);
			this.m_bEmpty = true;
		}

		// Token: 0x06006D2F RID: 27951 RVA: 0x0023C688 File Offset: 0x0023A888
		public NKCUISlot.SlotData GetSlotData()
		{
			return this.m_SlotData;
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x0023C690 File Offset: 0x0023A890
		public void CleanUp()
		{
			this.dOnClick = null;
			this.m_SlotData = null;
			this.m_bEmpty = true;
			this.dOnPointerDown = null;
		}

		// Token: 0x06006D31 RID: 27953 RVA: 0x0023C6B0 File Offset: 0x0023A8B0
		public virtual void TurnOffExtraUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objCompleteMark, false);
			NKCUtil.SetGameobjectActive(this.m_objFirstClear, false);
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, false);
			NKCUtil.SetGameobjectActive(this.m_lbAdditionalText, false);
			NKCUtil.SetGameobjectActive(this.m_objTier, false);
			NKCUtil.SetGameobjectActive(this.m_imgUpperRightIcon, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_DISABLE, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_CLEARED, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_DENIED, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_INDUCE_ARROW, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_INDUCE_ARROW_RED, false);
			NKCUtil.SetGameobjectActive(this.m_objItemDetail, false);
			NKCUtil.SetGameobjectActive(this.m_objSeized, false);
			NKCUtil.SetGameobjectActive(this.m_objRewardFx, false);
			NKCUtil.SetGameobjectActive(this.m_objEventGet, false);
			NKCUtil.SetGameobjectActive(this.m_objHaveCount, false);
			NKCUtil.SetGameobjectActive(this.m_objTopNotice, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_EQUIP_SET_ICON.gameObject, false);
			NKCUtil.SetAwakenFX(this.m_animAwakenFX, null);
			NKCUtil.SetGameobjectActive(this.m_objTier_7, false);
			NKCUtil.SetGameobjectActive(this.m_objRelic, false);
			NKCUtil.SetGameobjectActive(this.m_objSelected, false);
			NKCUtil.SetGameobjectActive(this.m_objTimeInterval, false);
		}

		// Token: 0x06006D32 RID: 27954 RVA: 0x0023C7E4 File Offset: 0x0023A9E4
		public static List<NKCUISlot.SlotData> MakeSlotDataListFromReward(NKMRewardData rewardData, bool bIncludeContractList = false, bool bStackEnchantType = false)
		{
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (rewardData == null)
			{
				return list;
			}
			List<NKMUnitData> list2 = new List<NKMUnitData>(rewardData.UnitDataList);
			List<NKMOperator> list3 = new List<NKMOperator>(rewardData.OperatorList);
			if (bIncludeContractList && rewardData.ContractList != null)
			{
				foreach (MiscContractResult miscContractResult in rewardData.ContractList)
				{
					if (miscContractResult != null)
					{
						list2.AddRange(miscContractResult.units);
					}
				}
			}
			list2.Sort(new Comparison<NKMUnitData>(NKCUISlot.RewardUnitSort));
			foreach (NKMUnitData unitData in list2)
			{
				NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeUnitData(unitData);
				list.Add(item);
			}
			list3.Sort(new Comparison<NKMOperator>(NKCUISlot.RewardOperatorSort));
			foreach (NKMOperator operatorData in list3)
			{
				NKCUISlot.SlotData item2 = NKCUISlot.SlotData.MakeUnitData(operatorData);
				list.Add(item2);
			}
			List<NKMEquipItemData> list4 = new List<NKMEquipItemData>(rewardData.EquipItemDataList);
			list4.Sort(new Comparison<NKMEquipItemData>(NKCUISlot.RewardEquipSort));
			Dictionary<int, List<NKMEquipItemData>> dictionary = new Dictionary<int, List<NKMEquipItemData>>();
			foreach (NKMEquipItemData nkmequipItemData in list4)
			{
				if (bStackEnchantType)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
					if (equipTemplet != null && equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
					{
						if (!dictionary.ContainsKey(nkmequipItemData.m_ItemEquipID))
						{
							dictionary.Add(nkmequipItemData.m_ItemEquipID, new List<NKMEquipItemData>());
						}
						dictionary[nkmequipItemData.m_ItemEquipID].Add(nkmequipItemData);
						continue;
					}
				}
				NKCUISlot.SlotData item3 = NKCUISlot.SlotData.MakeEquipData(nkmequipItemData);
				list.Add(item3);
			}
			foreach (KeyValuePair<int, List<NKMEquipItemData>> keyValuePair in dictionary)
			{
				int count = keyValuePair.Value.Count;
				if (count > 0)
				{
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeEquipData(keyValuePair.Value[0]);
					slotData.eType = NKCUISlot.eSlotMode.EquipCount;
					slotData.Count = (long)count;
					list.Add(slotData);
				}
			}
			if (rewardData.UserExp > 0)
			{
				NKCUISlot.SlotData item4 = NKCUISlot.SlotData.MakeMiscItemData(501, (long)rewardData.UserExp, rewardData.BonusRatioOfUserExp);
				list.Add(item4);
			}
			if (rewardData.DailyMissionPoint > 0)
			{
				NKCUISlot.SlotData item5 = NKCUISlot.SlotData.MakeMiscItemData(203, (long)rewardData.DailyMissionPoint, 0);
				list.Add(item5);
			}
			if (rewardData.WeeklyMissionPoint > 0)
			{
				NKCUISlot.SlotData item6 = NKCUISlot.SlotData.MakeMiscItemData(204, (long)rewardData.WeeklyMissionPoint, 0);
				list.Add(item6);
			}
			if (rewardData.AchievePoint > 0L)
			{
				NKCUISlot.SlotData item7 = NKCUISlot.SlotData.MakeMiscItemData(202, rewardData.AchievePoint, 0);
				list.Add(item7);
			}
			if (rewardData.SkinIdList != null)
			{
				foreach (int skinID in rewardData.SkinIdList)
				{
					NKCUISlot.SlotData item8 = NKCUISlot.SlotData.MakeSkinData(skinID);
					list.Add(item8);
				}
			}
			List<NKMMoldItemData> list5 = new List<NKMMoldItemData>(rewardData.MoldItemDataList);
			list5.Sort(new Comparison<NKMMoldItemData>(NKCUISlot.RewardMoldSort));
			foreach (NKMMoldItemData itemData in list5)
			{
				NKCUISlot.SlotData item9 = NKCUISlot.SlotData.MakeMoldItemData(itemData);
				list.Add(item9);
			}
			Dictionary<int, long> dictionary2 = new Dictionary<int, long>();
			foreach (NKMItemMiscData nkmitemMiscData in rewardData.MiscItemDataList)
			{
				if (nkmitemMiscData.ItemID != 0 && nkmitemMiscData.TotalCount > 0L)
				{
					if (dictionary2.ContainsKey(nkmitemMiscData.ItemID))
					{
						dictionary2[nkmitemMiscData.ItemID] = dictionary2[nkmitemMiscData.ItemID] + nkmitemMiscData.TotalCount;
					}
					else
					{
						dictionary2[nkmitemMiscData.ItemID] = nkmitemMiscData.TotalCount;
					}
				}
				else
				{
					Debug.LogError("ItemID 0 or count 0 itemdata. any error?, ItemMiscID : " + nkmitemMiscData.ItemID.ToString() + ", count : " + nkmitemMiscData.TotalCount.ToString());
				}
			}
			if (rewardData.EmoticonList != null)
			{
				foreach (int id in rewardData.EmoticonList)
				{
					NKCUISlot.SlotData item10 = NKCUISlot.SlotData.MakeEmoticonData(id, 1);
					list.Add(item10);
				}
			}
			if (rewardData.Interiors != null)
			{
				foreach (NKMInteriorData nkminteriorData in rewardData.Interiors)
				{
					NKCUISlot.SlotData item11 = NKCUISlot.SlotData.MakeMiscItemData(nkminteriorData.itemId, nkminteriorData.count, 0);
					list.Add(item11);
				}
			}
			using (Dictionary<int, long>.Enumerator enumerator10 = dictionary2.GetEnumerator())
			{
				while (enumerator10.MoveNext())
				{
					KeyValuePair<int, long> kvPair = enumerator10.Current;
					int bonusRate = 0;
					if (rewardData.MiscItemDataList.Find((NKMItemMiscData x) => x.ItemID == kvPair.Key) != null)
					{
						bonusRate = rewardData.MiscItemDataList.Find((NKMItemMiscData x) => x.ItemID == kvPair.Key).BonusRatio;
					}
					NKCUISlot.SlotData item12 = NKCUISlot.SlotData.MakeMiscItemData(kvPair.Key, kvPair.Value, bonusRate);
					list.Add(item12);
				}
			}
			return list;
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x0023CDEC File Offset: 0x0023AFEC
		public static List<NKCUISlot.SlotData> MakeSlotDataListFromReward(NKMAdditionalReward reward)
		{
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (reward != null)
			{
				if (reward.guildExpDelta > 0L)
				{
					NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeMiscItemData(503, reward.guildExpDelta, 0);
					list.Add(item);
				}
				if (reward.unionPointDelta > 0L)
				{
					NKCUISlot.SlotData item2 = NKCUISlot.SlotData.MakeMiscItemData(24, reward.unionPointDelta, 0);
					list.Add(item2);
				}
				if (reward.eventPassExpDelta > 0L)
				{
					NKCUISlot.SlotData item3 = NKCUISlot.SlotData.MakeMiscItemData(504, reward.eventPassExpDelta, 0);
					list.Add(item3);
				}
			}
			return list;
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x0023CE6C File Offset: 0x0023B06C
		public static int RewardEquipSort(NKMEquipItemData A, NKMEquipItemData B)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(A.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(B.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return 1;
			}
			if (equipTemplet2 == null)
			{
				return -1;
			}
			if (equipTemplet.m_NKM_ITEM_GRADE == equipTemplet2.m_NKM_ITEM_GRADE)
			{
				return A.m_ItemEquipID.CompareTo(B.m_ItemEquipID);
			}
			return equipTemplet2.m_NKM_ITEM_GRADE.CompareTo(equipTemplet.m_NKM_ITEM_GRADE);
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x0023CED8 File Offset: 0x0023B0D8
		public static int RewardUnitSort(NKMUnitData A, NKMUnitData B)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(A.m_UnitID);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(B.m_UnitID);
			if (unitTempletBase.m_bAwaken != unitTempletBase2.m_bAwaken)
			{
				return unitTempletBase2.m_bAwaken.CompareTo(unitTempletBase.m_bAwaken);
			}
			if (unitTempletBase.m_NKM_UNIT_GRADE != unitTempletBase2.m_NKM_UNIT_GRADE)
			{
				return unitTempletBase2.m_NKM_UNIT_GRADE.CompareTo(unitTempletBase.m_NKM_UNIT_GRADE);
			}
			return A.m_UnitID.CompareTo(B.m_UnitID);
		}

		// Token: 0x06006D36 RID: 27958 RVA: 0x0023CF5C File Offset: 0x0023B15C
		public static int RewardOperatorSort(NKMOperator A, NKMOperator B)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(A.id);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(B.id);
			if (unitTempletBase.m_NKM_UNIT_GRADE != unitTempletBase2.m_NKM_UNIT_GRADE)
			{
				return unitTempletBase2.m_NKM_UNIT_GRADE.CompareTo(unitTempletBase.m_NKM_UNIT_GRADE);
			}
			return A.id.CompareTo(B.id);
		}

		// Token: 0x06006D37 RID: 27959 RVA: 0x0023CFC0 File Offset: 0x0023B1C0
		public static int RewardMoldSort(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null)
			{
				return 1;
			}
			if (itemMoldTempletByID2 == null)
			{
				return -1;
			}
			if (itemMoldTempletByID.m_Grade != itemMoldTempletByID2.m_Grade)
			{
				return itemMoldTempletByID2.m_Grade.CompareTo(itemMoldTempletByID.m_Grade);
			}
			return itemMoldTempletByID.m_MoldID.CompareTo(itemMoldTempletByID2.m_MoldID);
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x0023D02C File Offset: 0x0023B22C
		public static void SetSlotListData(List<NKCUISlot> lstSlot, List<NKCUISlot.SlotData> lstSlotData, bool bShowName, bool bShowNumber, bool bEnableLayoutElement, NKCUISlot.OnClick onClick, params NKCUISlot.SlotClickType[] clickTypes)
		{
			for (int i = 0; i < lstSlot.Count; i++)
			{
				if (i < lstSlotData.Count)
				{
					NKCUtil.SetGameobjectActive(lstSlot[i], true);
					lstSlot[i].SetData(lstSlotData[i], bShowName, bShowNumber, bEnableLayoutElement, onClick);
					if (clickTypes != null && clickTypes.Length != 0)
					{
						lstSlot[i].SetOnClickAction(clickTypes);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(lstSlot[i], false);
				}
			}
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x0023D0A0 File Offset: 0x0023B2A0
		public void SetData(NKCUISlot.SlotData data, bool bEnableLayoutElement = true, NKCUISlot.OnClick onClick = null)
		{
			bool bShowNumber = NKCUISlot.WillShowCount(data);
			if (onClick == null)
			{
				this.SetData(data, false, bShowNumber, bEnableLayoutElement, new NKCUISlot.OnClick(this.OpenItemBox));
				return;
			}
			this.SetData(data, false, bShowNumber, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x0023D0DC File Offset: 0x0023B2DC
		public static bool WillShowCount(NKCUISlot.SlotData data)
		{
			NKCUISlot.eSlotMode eType = data.eType;
			switch (eType)
			{
			case NKCUISlot.eSlotMode.ItemMisc:
			case NKCUISlot.eSlotMode.EquipCount:
				break;
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.Skin:
				return false;
			case NKCUISlot.eSlotMode.Mold:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(data.ID);
				if (itemMoldTempletByID != null)
				{
					return !itemMoldTempletByID.m_bPermanent;
				}
				return false;
			}
			default:
				if (eType != NKCUISlot.eSlotMode.UnitCount)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x0023D12C File Offset: 0x0023B32C
		public void SetData(NKCUISlot.SlotData data, bool bShowName, bool bShowNumber, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			switch (data.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
				this.SetUnitData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.ItemMisc:
				this.SetMiscItemData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.Equip:
				this.SetEquipData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.EquipCount:
				this.SetEquipCountData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.Skin:
				this.SetSkinData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.Mold:
				this.SetMoldData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.DiveArtifact:
				this.SetDiveArtifactData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.Buff:
				this.SetBuffData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.UnitCount:
				this.SetUnitCountData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.Emoticon:
				this.SetEmoticonData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			case NKCUISlot.eSlotMode.GuildArtifact:
				this.SetGuildArtifactData(data, bShowName, bShowNumber, bEnableLayoutElement, onClick);
				break;
			}
			this.m_bEmpty = false;
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x0023D228 File Offset: 0x0023B428
		public void SetCountRange(long min, long max)
		{
			this.SetActiveCount(true);
			if (min == max)
			{
				NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}", max));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}-{1}", min, max));
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x0023D280 File Offset: 0x0023B480
		public void SetHaveCount(long count, bool bInfinite = false)
		{
			if (bInfinite)
			{
				NKCUtil.SetLabelText(this.m_lbHaveCount, NKCStringTable.GetString("SI_DP_ICON_SLOT_HAVE", new object[]
				{
					NKCUtilString.GET_STRING_FORGE_CRAFT_COUNT_INFINITE_SYMBOL
				}));
				NKCUtil.SetGameobjectActive(this.m_objHaveCount, true);
				return;
			}
			if (count <= 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objHaveCount, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objHaveCount, true);
			if (count > 999L)
			{
				NKCUtil.SetLabelText(this.m_lbHaveCount, NKCStringTable.GetString("SI_DP_ICON_SLOT_HAVE", new object[]
				{
					"999+"
				}));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbHaveCount, NKCStringTable.GetString("SI_DP_ICON_SLOT_HAVE", new object[]
			{
				count
			}));
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x0023D32F File Offset: 0x0023B52F
		public void SetHaveCountString(bool bShow, string str)
		{
			NKCUtil.SetGameobjectActive(this.m_objHaveCount, bShow);
			if (bShow)
			{
				NKCUtil.SetLabelText(this.m_lbHaveCount, str);
			}
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x0023D34C File Offset: 0x0023B54C
		public void SetSlotItemCount(long count)
		{
			NKCUtil.SetLabelText(this.m_lbItemCount, count.ToString());
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x0023D360 File Offset: 0x0023B560
		public void SetSlotItemCountString(bool bShow, string str)
		{
			this.SetActiveCount(bShow);
			if (bShow)
			{
				NKCUtil.SetLabelText(this.m_lbItemCount, str);
			}
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x0023D378 File Offset: 0x0023B578
		public void AddProbabilityToName(float probability)
		{
			string text = this.GetName();
			text += string.Format("\n{0:0.000%}", probability);
			this.m_lbName.verticalOverflow = VerticalWrapMode.Overflow;
			NKCUtil.SetLabelText(this.m_lbName, text);
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x0023D3BB File Offset: 0x0023B5BB
		public void OverrideName(string name, bool supportRichText, bool forceShow = false)
		{
			NKCUtil.SetLabelText(this.m_lbName, name);
			this.m_lbName.supportRichText = supportRichText;
			if (forceShow)
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, true);
			}
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x0023D3E4 File Offset: 0x0023B5E4
		private void SetName(string text, bool bShowFullname = true)
		{
			if (bShowFullname || text.Length <= 7)
			{
				NKCUtil.SetLabelText(this.m_lbName, text);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbName, text.Substring(0, 7) + "...");
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x0023D41C File Offset: 0x0023B61C
		public string GetName()
		{
			if (this.m_lbName != null)
			{
				return this.m_lbName.text;
			}
			return "";
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x0023D43D File Offset: 0x0023B63D
		public bool IsEmpty()
		{
			return this.m_bEmpty;
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x0023D448 File Offset: 0x0023B648
		public void SetEmpty(NKCUISlot.OnClick _dOnClick = null)
		{
			this.m_bEmpty = true;
			this.dOnClick = _dOnClick;
			this.dOnPointerDown = null;
			this.m_SlotData = null;
			this.TurnOffExtraUI();
			NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
			NKCUtil.SetGameobjectActive(this.m_lbName, false);
			this.SetActiveCount(false);
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_EQUIP_SET_ICON.gameObject, false);
			this.SetEmptyBackground();
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x0023D4C5 File Offset: 0x0023B6C5
		private void SetEmptyBackground()
		{
			if (this.m_bCustomizedEmptySP)
			{
				this.m_imgBG.sprite = this.m_spCustomizedEmpty;
				return;
			}
			this.m_imgBG.sprite = this.m_spEmpty;
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x0023D4F4 File Offset: 0x0023B6F4
		public void SetLock(NKCUISlot.OnClick _dOnClick = null)
		{
			this.IsLocked = true;
			this.CleanUp();
			this.dOnClick = _dOnClick;
			this.TurnOffExtraUI();
			NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
			NKCUtil.SetGameobjectActive(this.m_lbName, false);
			this.SetActiveCount(false);
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_EQUIP_SET_ICON.gameObject, false);
			NKCUtil.SetImageSprite(this.m_imgBG, this.m_spBGLock, false);
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x0023D578 File Offset: 0x0023B778
		public void SetEmptyMaterial(NKCUISlot.OnClick _dOnClick = null)
		{
			this.m_bEmpty = true;
			this.dOnClick = _dOnClick;
			this.m_SlotData = null;
			this.TurnOffExtraUI();
			NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
			NKCUtil.SetGameobjectActive(this.m_lbName, false);
			this.SetActiveCount(false);
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			if (this.m_sp_MatAdd != null)
			{
				this.m_imgBG.sprite = this.m_sp_MatAdd;
				return;
			}
			this.m_imgBG.sprite = this.m_spEmpty;
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x0023D608 File Offset: 0x0023B808
		public void SetCustomizedEmptySP(Sprite sp)
		{
			this.m_bCustomizedEmptySP = true;
			this.m_spCustomizedEmpty = sp;
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x0023D618 File Offset: 0x0023B818
		public void SetCompleteMark(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objCompleteMark, bValue);
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x0023D626 File Offset: 0x0023B826
		public void SetFirstGetMark(bool bValue)
		{
			if (!bValue && !this.m_bFirstReward)
			{
				return;
			}
			this.m_bFirstReward = bValue;
			this.SetShowArrowBGText(bValue);
			if (bValue)
			{
				this.SetArrowBGText(NKCStringTable.GetString("SI_DP_OPERATION_FIRST_CLEAR_REWARD", false), NKCUtil.GetColor("#4E4F52"));
			}
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x0023D660 File Offset: 0x0023B860
		public void SetMainRewardMark(bool bValue)
		{
			this.SetShowArrowBGText(bValue);
			if (bValue)
			{
				this.SetArrowBGText(NKCUtilString.GET_STRING_POPUP_DUNGEON_GET_MAIN_REWARD, NKCUtil.GetColor("#8A0D00"));
			}
			this.SetActiveCount(false);
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x0023D688 File Offset: 0x0023B888
		public void SetUsedMark(bool bVal)
		{
			NKCUtil.SetGameobjectActive(this.m_EQUIP_FIERCE_BATTLE, bVal);
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x0023D696 File Offset: 0x0023B896
		public void DisableItemCount()
		{
			this.SetActiveCount(false);
		}

		// Token: 0x06006D50 RID: 27984 RVA: 0x0023D69F File Offset: 0x0023B89F
		public void SetFirstAllClearMark(bool bValue)
		{
			if (!bValue && !this.m_bFirstAllClear)
			{
				return;
			}
			this.m_bFirstAllClear = bValue;
			this.SetShowArrowBGText(bValue);
			if (bValue)
			{
				this.SetArrowBGText(NKCUtilString.GET_STRING_WARFARE_FIRST_ALL_CLEAR, NKCUtil.GetColor("#4E4F52"));
			}
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x0023D6D3 File Offset: 0x0023B8D3
		public void SetOnetimeMark(bool bValue)
		{
			if (!bValue && !this.m_bOneTimeReward)
			{
				return;
			}
			this.m_bOneTimeReward = bValue;
			this.SetShowArrowBGText(bValue);
			if (bValue)
			{
				this.SetArrowBGText(NKCUtilString.GET_STRING_REWARD_CHANCE_UP, NKCUtil.GetColor("#4E4F52"));
			}
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x0023D707 File Offset: 0x0023B907
		public void SetEventDropMark(bool bValue)
		{
			this.SetShowArrowBGText(bValue);
			if (bValue)
			{
				this.SetArrowBGText(NKCStringTable.GetString("SI_PF_EVENT_BADGE_TEXT_EVENT", false), NKCUtil.GetColor("#8600DB"));
			}
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x0023D72E File Offset: 0x0023B92E
		public void SetShowArrowBGText(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objFirstClear, bSet);
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x0023D73C File Offset: 0x0023B93C
		public void SetArrowBGText(string desc, Color bgColor)
		{
			if (this.m_imgFirstClearBG != null)
			{
				this.m_imgFirstClearBG.color = bgColor;
			}
			NKCUtil.SetLabelText(this.m_lbFirstClear, desc);
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x0023D764 File Offset: 0x0023B964
		public void SetDiveArtifactData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetDiveArtifactData(null, slotData, bShowName, bShowCount, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x0023D774 File Offset: 0x0023B974
		public void SetDiveArtifactData(NKMUserData userData, NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			long count = slotData.Count;
			this.m_bEmpty = false;
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(id);
			if (nkmdiveArtifactTemplet == null)
			{
				this.SetEmpty(null);
				return;
			}
			if (bShowName)
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, true);
				this.SetName(nkmdiveArtifactTemplet.ArtifactName_Translated, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, false);
			}
			if (userData != null)
			{
				int num = 0;
				NKMDiveGameData diveGameData = userData.m_DiveGameData;
				if (diveGameData != null)
				{
					List<int> artifacts = diveGameData.Player.PlayerBase.Artifacts;
					for (int i = 0; i < artifacts.Count; i++)
					{
						if (artifacts[i] == id)
						{
							num = 1;
							break;
						}
					}
				}
				this.SetActiveCount(true);
				if (count <= (long)num)
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/{1}", count, num));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/<color=#ff0000ff>{1}</color>", count, num));
				}
			}
			else if (bShowCount && count > 1L)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, count.ToString());
			}
			else
			{
				this.SetActiveCount(false);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBG, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			Sprite sprite;
			if (this.m_bUseBigImg)
			{
				sprite = NKCResourceUtility.GetOrLoadDiveArtifactIconBig(nkmdiveArtifactTemplet);
			}
			else
			{
				sprite = NKCResourceUtility.GetOrLoadDiveArtifactIcon(nkmdiveArtifactTemplet);
			}
			this.m_imgIcon.sprite = sprite;
			if (sprite == null)
			{
				Debug.LogError("iconSprite not found. artifact ID : " + id.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_objTier, false);
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x0023D95C File Offset: 0x0023BB5C
		public void SetEmoticonData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetEmoticonData(null, slotData, bShowName, bShowCount, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x0023D96C File Offset: 0x0023BB6C
		public void SetEmoticonData(NKMUserData userData, NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			long count = slotData.Count;
			this.m_bEmpty = false;
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(id);
			if (nkmemoticonTemplet == null)
			{
				this.SetEmpty(null);
				return;
			}
			if (bShowName)
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, true);
				this.SetName(nkmemoticonTemplet.GetEmoticonName(), true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, false);
			}
			if (userData != null)
			{
				int num = 0;
				NKMDiveGameData diveGameData = userData.m_DiveGameData;
				if (diveGameData != null)
				{
					List<int> artifacts = diveGameData.Player.PlayerBase.Artifacts;
					for (int i = 0; i < artifacts.Count; i++)
					{
						if (artifacts[i] == id)
						{
							num = 1;
							break;
						}
					}
				}
				this.SetActiveCount(true);
				if (count <= (long)num)
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/{1}", count, num));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/<color=#ff0000ff>{1}</color>", count, num));
				}
			}
			else if (bShowCount && count > 1L)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, count.ToString());
			}
			else
			{
				this.SetActiveCount(false);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBG, true);
			this.SetItemBackground(nkmemoticonTemplet.m_EmoticonGrade);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			Sprite orLoadEmoticonIcon = NKCResourceUtility.GetOrLoadEmoticonIcon(nkmemoticonTemplet);
			this.m_imgIcon.sprite = orLoadEmoticonIcon;
			if (orLoadEmoticonIcon == null)
			{
				Debug.LogError("iconSprite not found. emoticon ID : " + id.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_objTier, false);
		}

		// Token: 0x06006D59 RID: 27993 RVA: 0x0023DB4F File Offset: 0x0023BD4F
		public void SetMoldData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetMoldData(null, slotData, bShowName, bShowCount, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D5A RID: 27994 RVA: 0x0023DB60 File Offset: 0x0023BD60
		public void SetMoldData(NKMUserData userData, NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			long count = slotData.Count;
			this.m_bEmpty = false;
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(id);
			if (itemMoldTempletByID == null)
			{
				this.SetEmpty(null);
				return;
			}
			if (bShowName)
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, true);
				this.SetName(itemMoldTempletByID.GetItemName(), true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, false);
			}
			if (userData != null)
			{
				this.SetActiveCount(true);
				long moldCount = userData.m_CraftData.GetMoldCount(id);
				if (count <= moldCount)
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/{1}", count, moldCount));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/<color=#ff0000ff>{1}</color>", count, moldCount));
				}
			}
			else if (bShowCount && count > 1L)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, count.ToString());
			}
			else
			{
				this.SetActiveCount(false);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBG, true);
			this.SetItemBackground(itemMoldTempletByID.m_Grade, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			Sprite orLoadMoldIcon = NKCResourceUtility.GetOrLoadMoldIcon(itemMoldTempletByID);
			this.m_imgIcon.sprite = orLoadMoldIcon;
			if (orLoadMoldIcon == null)
			{
				Debug.LogError("Item iconSprite not found. itemMoldID : " + id.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_objTier, itemMoldTempletByID.IsEquipMold);
			NKCUtil.SetLabelText(this.m_lbTier, NKCUtilString.GetItemEquipTier(itemMoldTempletByID.m_Tier));
		}

		// Token: 0x06006D5B RID: 27995 RVA: 0x0023DD1E File Offset: 0x0023BF1E
		public void SetMiscItemData(NKMItemMiscData data, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetMiscItemData(null, NKCUISlot.SlotData.MakeMiscItemData(data, 0), bShowName, bShowCount, bEnableLayoutElement, onClick, false);
		}

		// Token: 0x06006D5C RID: 27996 RVA: 0x0023DD35 File Offset: 0x0023BF35
		public void SetMiscItemData(int itemID, long itemCount, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetMiscItemData(null, NKCUISlot.SlotData.MakeMiscItemData(itemID, itemCount, 0), bShowName, bShowCount, bEnableLayoutElement, onClick, false);
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x0023DD4E File Offset: 0x0023BF4E
		public void SetMiscItemData(NKMUserData userData, NKMItemMiscData data, bool bShowName, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetMiscItemData(userData, NKCUISlot.SlotData.MakeMiscItemData(data, 0), bShowName, true, bEnableLayoutElement, onClick, false);
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x0023DD65 File Offset: 0x0023BF65
		public void SetMiscItemData(NKMUserData userData, int itemID, long itemCount, bool bShowName, bool bEnableLayoutElement, bool bHideUseCnt, NKCUISlot.OnClick onClick, bool bShowCount = true)
		{
			this.SetMiscItemData(userData, NKCUISlot.SlotData.MakeMiscItemData(itemID, itemCount, 0), bShowName, bShowCount, bEnableLayoutElement, onClick, bHideUseCnt);
		}

		// Token: 0x06006D5F RID: 27999 RVA: 0x0023DD80 File Offset: 0x0023BF80
		public void SetMiscItemData(NKMUserData userData, int itemID, long itemCount, bool bShowName, bool bEnableLayoutElement, NKCUISlot.OnClick onClick, bool bShowCount = true)
		{
			this.SetMiscItemData(userData, NKCUISlot.SlotData.MakeMiscItemData(itemID, itemCount, 0), bShowName, bShowCount, bEnableLayoutElement, onClick, false);
		}

		// Token: 0x06006D60 RID: 28000 RVA: 0x0023DD9A File Offset: 0x0023BF9A
		public void SetMiscItemData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetMiscItemData(null, slotData, bShowName, bShowCount, bEnableLayoutElement, onClick, false);
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x0023DDAC File Offset: 0x0023BFAC
		public void SetMiscItemData(NKMUserData userData, NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick, bool bHideUseCnt = false)
		{
			int id = slotData.ID;
			long count = slotData.Count;
			this.m_bEmpty = false;
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(id);
			if (itemMiscTempletByID == null)
			{
				this.SetEmpty(null);
				return;
			}
			if (bShowName)
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, true);
				this.SetName(itemMiscTempletByID.GetItemName(), true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, false);
			}
			if (userData != null)
			{
				if (bShowCount)
				{
					this.SetActiveCount(true);
					long countMiscItem = userData.m_InventoryData.GetCountMiscItem(id);
					NKMItemMiscTemplet itemMiscTempletByID2 = NKMItemManager.GetItemMiscTempletByID(id);
					if (itemMiscTempletByID2 != null)
					{
						bool flag = itemMiscTempletByID2.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_RESOURCE;
						if (bHideUseCnt)
						{
							if (countMiscItem >= count)
							{
								NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("<color=#ffffff>{0}</color>", count));
							}
							else
							{
								NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("<color=#ff0000ff>{0}</color>", count));
							}
						}
						else if (count <= countMiscItem)
						{
							if (flag)
							{
								NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}", count));
							}
							else
							{
								NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/{1}", count, countMiscItem));
							}
						}
						else if (flag)
						{
							NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("<color=#ff0000ff>{0}</color>", count));
						}
						else
						{
							NKCUtil.SetLabelText(this.m_lbItemCount, string.Format("{0}/<color=#ff0000ff>{1}</color>", count, countMiscItem));
						}
					}
				}
				else
				{
					this.SetActiveCount(false);
				}
			}
			else if (itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_EMBLEM_RANK)
			{
				this.SetActiveCount(false);
				if (bShowCount && count > 1L)
				{
					this.SetAdditionalText(string.Format(NKCUtilString.GET_STRING_RANK_ONE_PARAM, count), TextAnchor.MiddleCenter);
				}
			}
			else if (bShowCount && count > 1L)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, count.ToString());
			}
			else
			{
				this.SetActiveCount(false);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBG, true);
			this.SetItemBackground(itemMiscTempletByID);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID);
			this.m_imgIcon.sprite = orLoadMiscItemIcon;
			if (orLoadMiscItemIcon == null)
			{
				Debug.LogError("Item iconSprite not found. itemMiscID : " + id.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_objTimeInterval, itemMiscTempletByID.IsTimeIntervalItem);
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x0023E044 File Offset: 0x0023C244
		private bool ItemUsingWhiteBackground(NKMItemMiscTemplet itemTemplet)
		{
			NKM_ITEM_MISC_TYPE itemMiscType = itemTemplet.m_ItemMiscType;
			return itemMiscType - NKM_ITEM_MISC_TYPE.IMT_BACKGROUND <= 1 || itemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR;
		}

		// Token: 0x06006D63 RID: 28003 RVA: 0x0023E068 File Offset: 0x0023C268
		private void SetItemBackground(NKMItemMiscTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			if (templet.IsEmblem())
			{
				this.m_imgBG.sprite = this.m_spEmblem;
				return;
			}
			bool bUseWhiteBG = this.ItemUsingWhiteBackground(templet);
			this.SetItemBackground(templet.m_NKM_ITEM_GRADE, bUseWhiteBG);
		}

		// Token: 0x06006D64 RID: 28004 RVA: 0x0023E0A8 File Offset: 0x0023C2A8
		private void SetItemBackground(NKM_ITEM_GRADE grade, bool bUseWhiteBG)
		{
			if (bUseWhiteBG)
			{
				switch (grade)
				{
				case NKM_ITEM_GRADE.NIG_N:
					this.m_imgBG.sprite = this.m_spBGWhiteRarityN;
					return;
				case NKM_ITEM_GRADE.NIG_R:
					this.m_imgBG.sprite = this.m_spBGWhiteRarityR;
					return;
				case NKM_ITEM_GRADE.NIG_SR:
					this.m_imgBG.sprite = this.m_spBGWhiteRaritySR;
					return;
				case NKM_ITEM_GRADE.NIG_SSR:
					this.m_imgBG.sprite = this.m_spBGWhiteRaritySSR;
					return;
				default:
					Debug.LogError("Item BG undefined");
					this.m_imgBG.sprite = this.m_spBGWhiteRarityN;
					return;
				}
			}
			else
			{
				switch (grade)
				{
				case NKM_ITEM_GRADE.NIG_N:
					this.m_imgBG.sprite = this.m_spBGRarityN;
					return;
				case NKM_ITEM_GRADE.NIG_R:
					this.m_imgBG.sprite = this.m_spBGRarityR;
					return;
				case NKM_ITEM_GRADE.NIG_SR:
					this.m_imgBG.sprite = this.m_spBGRaritySR;
					return;
				case NKM_ITEM_GRADE.NIG_SSR:
					this.m_imgBG.sprite = this.m_spBGRaritySSR;
					return;
				default:
					Debug.LogError("Item BG undefined");
					this.m_imgBG.sprite = this.m_spBGRarityN;
					return;
				}
			}
		}

		// Token: 0x06006D65 RID: 28005 RVA: 0x0023E1B0 File Offset: 0x0023C3B0
		private void SetItemBackground(NKM_EMOTICON_GRADE grade)
		{
			switch (grade)
			{
			case NKM_EMOTICON_GRADE.NEG_N:
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			case NKM_EMOTICON_GRADE.NEG_R:
				this.m_imgBG.sprite = this.m_spBGRarityR;
				return;
			case NKM_EMOTICON_GRADE.NEG_SR:
				this.m_imgBG.sprite = this.m_spBGRaritySR;
				return;
			case NKM_EMOTICON_GRADE.NEG_SSR:
				this.m_imgBG.sprite = this.m_spBGRaritySSR;
				return;
			default:
				Debug.LogError("Item BG undefined");
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			}
		}

		// Token: 0x06006D66 RID: 28006 RVA: 0x0023E238 File Offset: 0x0023C438
		public void SetUnitData(NKMOperator data, bool bShowName, bool bShowLevel, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetUnitData(NKCUISlot.SlotData.MakeUnitData(data), bShowName, bShowLevel, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D67 RID: 28007 RVA: 0x0023E24C File Offset: 0x0023C44C
		public void SetUnitData(NKMUnitData data, bool bShowName, bool bShowLevel, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetUnitData(NKCUISlot.SlotData.MakeUnitData(data), bShowName, bShowLevel, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D68 RID: 28008 RVA: 0x0023E260 File Offset: 0x0023C460
		public void SetUnitData(int unitID, int unitLevel, int skinID, bool bShowName, bool bShowLevel, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetUnitData(NKCUISlot.SlotData.MakeUnitData(unitID, unitLevel, skinID, 0), bShowName, bShowLevel, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x0023E27C File Offset: 0x0023C47C
		public void SetUnitData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowLevel, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.m_bEmpty = false;
			this.m_SlotData = slotData;
			this.dOnClick = onClick;
			if (bShowLevel)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, slotData.Count));
			}
			else
			{
				this.SetActiveCount(false);
			}
			this.m_layoutElement.enabled = bEnableLayoutElement;
			this.SetUnitTempletData(this.m_SlotData.ID, this.m_SlotData.Data, bShowName, onClick);
		}

		// Token: 0x06006D6A RID: 28010 RVA: 0x0023E300 File Offset: 0x0023C500
		public void SetUnitCountData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.m_bEmpty = false;
			this.m_SlotData = slotData;
			this.dOnClick = onClick;
			if (bShowCount && slotData.Count > 1L)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, slotData.Count.ToString());
			}
			else
			{
				this.SetActiveCount(false);
			}
			this.m_layoutElement.enabled = bEnableLayoutElement;
			this.SetUnitTempletData(this.m_SlotData.ID, this.m_SlotData.Data, bShowName, onClick);
		}

		// Token: 0x06006D6B RID: 28011 RVA: 0x0023E384 File Offset: 0x0023C584
		private void SetUnitTempletData(int unitID, int skinID, bool bShowName, NKCUISlot.OnClick onClick)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			this.TurnOffExtraUI();
			if (unitTempletBase == null)
			{
				this.SetEmpty(onClick);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			if (skinTemplet != null && skinTemplet.m_SkinEquipUnitID == unitID)
			{
				this.m_imgIcon.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet);
			}
			else
			{
				this.m_imgIcon.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
			}
			NKCUtil.SetGameobjectActive(this.m_lbName, bShowName);
			NKCUtil.SetLabelText(this.m_lbName, unitTempletBase.GetUnitName());
			this.SetUnitBackground(unitTempletBase.m_NKM_UNIT_GRADE);
			NKCUtil.SetAwakenFX(this.m_animAwakenFX, unitTempletBase);
			NKCUtil.SetGameobjectActive(this.m_imgUpperRightIcon, true);
			Sprite orLoadUnitTypeIcon = NKCResourceUtility.GetOrLoadUnitTypeIcon(unitTempletBase, true);
			if (orLoadUnitTypeIcon != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgUpperRightIcon, true);
				if (this.m_imgUpperRightIcon != null)
				{
					this.m_imgUpperRightIcon.sprite = orLoadUnitTypeIcon;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgUpperRightIcon, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x0023E4A8 File Offset: 0x0023C6A8
		private void SetUnitBackground(NKM_UNIT_GRADE grade)
		{
			switch (grade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			case NKM_UNIT_GRADE.NUG_R:
				this.m_imgBG.sprite = this.m_spBGRarityR;
				return;
			case NKM_UNIT_GRADE.NUG_SR:
				this.m_imgBG.sprite = this.m_spBGRaritySR;
				return;
			case NKM_UNIT_GRADE.NUG_SSR:
				this.m_imgBG.sprite = this.m_spBGRaritySSR;
				return;
			default:
				Debug.LogError("Unit BG undefined");
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			}
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x0023E530 File Offset: 0x0023C730
		private void SetEquipData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowLevel, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			int num = (int)slotData.Count;
			int groupID = slotData.GroupID;
			this.m_bEmpty = false;
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			this.SetEquipTempletData(id, bShowName);
			this.SetEquipItemData(slotData.UID);
			bool flag = bShowLevel && num > 0;
			this.SetActiveCount(flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_lbItemCount, "+" + num.ToString());
			}
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(groupID);
			if (equipSetOptionTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_EQUIP_SET_ICON.gameObject, true);
				NKCUtil.SetImageSprite(this.m_AB_ICON_SLOT_EQUIP_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(equipSetOptionTemplet), false);
			}
		}

		// Token: 0x06006D6E RID: 28014 RVA: 0x0023E5E8 File Offset: 0x0023C7E8
		private void SetEquipCountData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			int groupID = slotData.GroupID;
			this.m_bEmpty = false;
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			this.SetEquipTempletData(id, bShowName);
			this.SetEquipItemData(slotData.UID);
			bShowCount = (bShowCount && slotData.Count > 1L);
			this.SetActiveCount(bShowCount);
			if (bShowCount)
			{
				if (slotData.Count > 999L)
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, "999+");
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbItemCount, slotData.Count.ToString());
				}
			}
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(groupID);
			if (equipSetOptionTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_EQUIP_SET_ICON.gameObject, true);
				NKCUtil.SetImageSprite(this.m_AB_ICON_SLOT_EQUIP_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(equipSetOptionTemplet), false);
			}
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x0023E6B8 File Offset: 0x0023C8B8
		private void SetEquipTempletData(int equipID, bool bShowName)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
			this.TurnOffExtraUI();
			if (equipTemplet == null)
			{
				this.SetEmpty(null);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			this.m_imgIcon.sprite = NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet);
			NKCUtil.SetGameobjectActive(this.m_lbName, bShowName);
			NKCUtil.SetLabelText(this.m_lbName, NKCUtilString.GetItemEquipNameWithTier(equipTemplet));
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_imgBG, true);
			this.SetItemBackground(equipTemplet.m_NKM_ITEM_GRADE, false);
			NKCUtil.SetGameobjectActive(this.m_objTier, true);
			NKCUtil.SetLabelText(this.m_lbTier, NKCUtilString.GetItemEquipTier(equipTemplet.m_NKM_ITEM_TIER));
			this.SetEquipEffect(equipTemplet.m_bShowEffect);
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x0023E790 File Offset: 0x0023C990
		private void SetEquipItemData(long equipUID)
		{
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRelic, equipTemplet.IsRelic());
			if (this.m_objRelic.activeSelf)
			{
				for (int i = 0; i < this.m_lstImgRelic.Count; i++)
				{
					if (itemEquip.potentialOption == null || itemEquip.potentialOption.sockets == null)
					{
						NKCUtil.SetGameobjectActive(this.m_lstImgRelic[i], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstImgRelic[i], i < itemEquip.potentialOption.sockets.Length && itemEquip.potentialOption.sockets[i] != null);
					}
				}
			}
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x0023E854 File Offset: 0x0023CA54
		private void SetSkinData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowNumber, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			this.m_bEmpty = false;
			this.m_SlotData = slotData;
			this.dOnClick = onClick;
			this.TurnOffExtraUI();
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(id);
			if (skinTemplet == null)
			{
				this.SetEmpty(null);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbName, bShowName);
			this.SetActiveCount(bShowNumber);
			this.m_layoutElement.enabled = bEnableLayoutElement;
			NKCUtil.SetLabelText(this.m_lbName, skinTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_lbItemCount, NKCUtilString.GET_STRING_SKIN);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			this.m_imgIcon.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet);
			NKCUtil.SetGameobjectActive(this.m_imgBG, true);
			this.SetBackGround(skinTemplet.m_SkinGrade);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x0023E93C File Offset: 0x0023CB3C
		public void SetBackGround(NKMSkinTemplet.SKIN_GRADE grade)
		{
			switch (grade)
			{
			case NKMSkinTemplet.SKIN_GRADE.SG_VARIATION:
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_NORMAL:
				this.m_imgBG.sprite = this.m_spBGRarityR;
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				this.m_imgBG.sprite = this.m_spBGRaritySR;
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
				this.m_imgBG.sprite = this.m_spBGRaritySSR;
				return;
			default:
				Debug.LogError("SKIN_GRADE BG undefined");
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			}
		}

		// Token: 0x06006D73 RID: 28019 RVA: 0x0023E9C8 File Offset: 0x0023CBC8
		private void SetBuffData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowNumber, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			this.m_bEmpty = false;
			this.m_SlotData = slotData;
			this.dOnClick = onClick;
			this.TurnOffExtraUI();
			NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(slotData.ID);
			if (companyBuffTemplet == null)
			{
				this.SetEmpty(null);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbName, bShowName);
			this.SetActiveCount(bShowNumber);
			this.m_layoutElement.enabled = bEnableLayoutElement;
			NKCUtil.SetLabelText(this.m_lbName, companyBuffTemplet.GetBuffName());
			NKCUtil.SetLabelText(this.m_lbItemCount, string.Empty);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			this.m_imgIcon.sprite = NKCResourceUtility.GetOrLoadBuffIconForItemPopup(companyBuffTemplet);
			NKCUtil.SetGameobjectActive(this.m_imgBG, true);
			this.SetItemBackground(NKM_ITEM_GRADE.NIG_SSR, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
		}

		// Token: 0x06006D74 RID: 28020 RVA: 0x0023EAB4 File Offset: 0x0023CCB4
		public void SetEtcData(NKCUISlot.SlotData slotData, Sprite iconSprite, string countText, string name, NKCUISlot.OnClick onClick)
		{
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			NKCUtil.SetImageSprite(this.m_imgIcon, iconSprite, false);
			NKCUtil.SetGameobjectActive(this.m_lbItemCount, !string.IsNullOrEmpty(countText));
			NKCUtil.SetLabelText(this.m_lbItemCount, countText);
			NKCUtil.SetGameobjectActive(this.m_lbName, !string.IsNullOrEmpty(name));
			NKCUtil.SetLabelText(this.m_lbName, name);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			NKCUtil.SetGameobjectActive(this.m_imgBG, false);
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x0023EB64 File Offset: 0x0023CD64
		public void SetEtcData(NKCUISlot.SlotData slotData, Sprite iconSprite, string countText, string name, NKM_ITEM_GRADE grade, NKCUISlot.OnClick onClick)
		{
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			NKCUtil.SetImageSprite(this.m_imgIcon, iconSprite, false);
			NKCUtil.SetLabelText(this.m_lbItemCount, countText);
			NKCUtil.SetLabelText(this.m_lbName, name);
			this.SetItemBackground(grade, false);
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x0023EBB5 File Offset: 0x0023CDB5
		public void SetGuildArtifactData(NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			this.SetGuildArtifactData(null, slotData, bShowName, bShowCount, bEnableLayoutElement, onClick);
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x0023EBC8 File Offset: 0x0023CDC8
		public void SetGuildArtifactData(NKMUserData userData, NKCUISlot.SlotData slotData, bool bShowName, bool bShowCount, bool bEnableLayoutElement, NKCUISlot.OnClick onClick)
		{
			int id = slotData.ID;
			long count = slotData.Count;
			this.m_bEmpty = false;
			this.TurnOffExtraUI();
			this.dOnClick = onClick;
			this.m_SlotData = slotData;
			this.m_layoutElement.enabled = bEnableLayoutElement;
			GuildDungeonArtifactTemplet artifactTemplet = GuildDungeonTempletManager.GetArtifactTemplet(id);
			if (artifactTemplet == null)
			{
				this.SetEmpty(null);
				return;
			}
			if (bShowName)
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, true);
				this.SetName(artifactTemplet.GetName(), true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbName, false);
			}
			if (bShowCount && count > 1L)
			{
				this.SetActiveCount(true);
				NKCUtil.SetLabelText(this.m_lbItemCount, count.ToString());
			}
			else
			{
				this.SetActiveCount(false);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBG, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
			this.m_imgIcon.color = new Color(1f, 1f, 1f, 1f);
			Sprite sprite;
			if (this.m_bUseBigImg)
			{
				sprite = NKCResourceUtility.GetOrLoadGuildArtifactIconBig(artifactTemplet);
			}
			else
			{
				sprite = NKCResourceUtility.GetOrLoadGuildArtifactIcon(artifactTemplet);
			}
			this.m_imgIcon.sprite = sprite;
			if (sprite == null)
			{
				Debug.LogError("iconSprite not found. artifact ID : " + id.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_objTier, false);
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x0023ED0C File Offset: 0x0023CF0C
		public void PlaySmallToOrgSize()
		{
			base.transform.DOComplete(false);
			base.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			base.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x0023ED62 File Offset: 0x0023CF62
		public void SetOriginalSize()
		{
			base.transform.DOComplete(false);
			base.transform.localScale = Vector3.one;
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x0023ED84 File Offset: 0x0023CF84
		public void SetOnClickAction(params NKCUISlot.SlotClickType[] clickTypes)
		{
			this.dOnPointerDown = null;
			this.dOnClick = null;
			if (this.m_SlotData == null)
			{
				return;
			}
			for (int i = 0; i < clickTypes.Length; i++)
			{
				switch (clickTypes[i])
				{
				case NKCUISlot.SlotClickType.Tooltip:
					this.SetOpenTooltipOnPress(true);
					return;
				case NKCUISlot.SlotClickType.ItemBox:
					if (this.m_SlotData.eType != NKCUISlot.eSlotMode.Skin)
					{
						this.SetOpenItemBoxOnClick();
						return;
					}
					break;
				case NKCUISlot.SlotClickType.RatioList:
					if (this.m_SlotData.eType == NKCUISlot.eSlotMode.ItemMisc)
					{
						NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_SlotData.ID);
						if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsRatioOpened())
						{
							if (itemMiscTempletByID.IsContractItem)
							{
								this.SetOpenContractRatioBoxOnPress();
								return;
							}
							this.SetOpenRatioBoxOnPress();
							return;
						}
					}
					break;
				case NKCUISlot.SlotClickType.BoxList:
					if (this.m_SlotData.eType == NKCUISlot.eSlotMode.ItemMisc)
					{
						NKMItemMiscTemplet itemMiscTempletByID2 = NKMItemManager.GetItemMiscTempletByID(this.m_SlotData.ID);
						if (itemMiscTempletByID2 != null && itemMiscTempletByID2.IsUsable() && itemMiscTempletByID2.IsPackageItem)
						{
							this.SetOpenPackageList();
							return;
						}
					}
					break;
				case NKCUISlot.SlotClickType.MoldList:
					if (this.m_SlotData.eType == NKCUISlot.eSlotMode.Mold)
					{
						NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_SlotData.ID);
						if (itemMoldTempletByID != null)
						{
							this.SetOpenItemBoxOnClick();
							this.SetItemDetailIcon(itemMoldTempletByID.IsEquipMold);
							return;
						}
					}
					break;
				case NKCUISlot.SlotClickType.ChoiceList:
					if (this.m_SlotData.eType == NKCUISlot.eSlotMode.ItemMisc)
					{
						NKMItemMiscTemplet itemMiscTempletByID3 = NKMItemManager.GetItemMiscTempletByID(this.m_SlotData.ID);
						if (itemMiscTempletByID3 != null && itemMiscTempletByID3.IsChoiceItem())
						{
							this.SetOpenChoiceList();
							return;
						}
					}
					break;
				}
			}
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x0023EF08 File Offset: 0x0023D108
		public void SetOnClick(NKCUISlot.OnClick onClick)
		{
			this.dOnClick = onClick;
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x0023EF11 File Offset: 0x0023D111
		public void ResetOnClick()
		{
			this.dOnClick = null;
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x0023EF1A File Offset: 0x0023D11A
		public void SetOpenItemBoxOnClick()
		{
			this.dOnClick = new NKCUISlot.OnClick(this.OpenItemBox);
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x0023EF30 File Offset: 0x0023D130
		private void OpenItemBox(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			switch (slotData.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
			case NKCUISlot.eSlotMode.Emoticon:
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, true);
				return;
			default:
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, true);
				return;
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
				this.OpenEquipBox(slotData);
				return;
			case NKCUISlot.eSlotMode.Skin:
				Debug.LogWarning("Skin Popup under construction");
				return;
			case NKCUISlot.eSlotMode.Mold:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_SlotData.ID);
				if (itemMoldTempletByID != null && itemMoldTempletByID.IsEquipMold && NKMItemManager.m_dicRandomMoldBox.ContainsKey(itemMoldTempletByID.m_RewardGroupID))
				{
					List<int> list = NKMItemManager.m_dicRandomMoldBox[itemMoldTempletByID.m_RewardGroupID];
					if (list != null && list.Count > 0)
					{
						NKCUISlotListViewer.GetNewInstance().OpenRewardList(list, NKM_REWARD_TYPE.RT_EQUIP, itemMoldTempletByID.GetItemName(), NKCUtilString.GET_STRING_FORGE_CRAFT_MOLD_DESC);
					}
				}
				return;
			}
			}
		}

		// Token: 0x06006D7F RID: 28031 RVA: 0x0023F00D File Offset: 0x0023D20D
		private void SetOpenTooltipOnPress(bool bBlockClick = false)
		{
			this.dOnPointerDown = new NKCUISlot.PointerDown(this.OpenTooltip);
			if (bBlockClick)
			{
				this.dOnClick = null;
			}
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x0023F02B File Offset: 0x0023D22B
		private void OpenTooltip(NKCUISlot.SlotData slotData, bool bLocked, PointerEventData eventData)
		{
			if (slotData == null)
			{
				return;
			}
			NKCUITooltip.Instance.Open(slotData, new Vector2?(eventData.position));
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x0023F048 File Offset: 0x0023D248
		private void OpenEquipBox(NKCUISlot.SlotData slotData)
		{
			NKMEquipItemData nkmequipItemData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(slotData.UID);
			if (nkmequipItemData == null)
			{
				nkmequipItemData = NKCEquipSortSystem.MakeTempEquipData(slotData.ID, slotData.GroupID, false);
				NKCPopupItemEquipBox.Open(nkmequipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
				return;
			}
			if (NKCUIWarfareResult.IsInstanceOpen)
			{
				NKCPopupItemEquipBox.Open(nkmequipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
				return;
			}
			if (nkmequipItemData.m_OwnerUnitUID > 0L)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
				if (equipTemplet == null)
				{
					return;
				}
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(nkmequipItemData.m_OwnerUnitUID);
				if (unitFromUID == null)
				{
					return;
				}
				NKM_ERROR_CODE nkm_ERROR_CODE = equipTemplet.CanUnEquipByUnit(NKCScenManager.GetScenManager().GetMyUserData(), unitFromUID);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE.ToString(), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCPopupItemEquipBox.Open(nkmequipItemData, this.m_EQUIP_BOX_BOTTOM_MENU_TYPE, null);
				return;
			}
			else
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY)
				{
					NKCPopupItemEquipBox.Open(nkmequipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
					return;
				}
				NKCPopupItemEquipBox.Open(nkmequipItemData, this.m_EQUIP_BOX_BOTTOM_MENU_TYPE, null);
				return;
			}
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x0023F13B File Offset: 0x0023D33B
		private void SetOpenRatioBoxOnPress()
		{
			this.SetItemDetailIcon(true);
			this.dOnClick = new NKCUISlot.OnClick(this.OpenRatioBox);
		}

		// Token: 0x06006D83 RID: 28035 RVA: 0x0023F156 File Offset: 0x0023D356
		private void SetOpenContractRatioBoxOnPress()
		{
			this.SetItemDetailIcon(true);
			this.dOnClick = new NKCUISlot.OnClick(this.OpenContractRatio);
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x0023F174 File Offset: 0x0023D374
		private void OpenRatioBox(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			if (slotData.eType != NKCUISlot.eSlotMode.ItemMisc)
			{
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
			if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsRatioOpened())
			{
				NKCUISlotListViewer newInstance = NKCUISlotListViewer.GetNewInstance();
				if (newInstance != null)
				{
					newInstance.OpenItemBoxRatio(slotData.ID);
				}
			}
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x0023F1CC File Offset: 0x0023D3CC
		private void OpenContractRatio(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			if (slotData.eType != NKCUISlot.eSlotMode.ItemMisc)
			{
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
			NKCUIContractPopupRateV2.Instance.Open(itemMiscTempletByID);
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x0023F1FE File Offset: 0x0023D3FE
		private void SetOpenPackageList()
		{
			this.SetItemDetailIcon(true);
			this.dOnClick = new NKCUISlot.OnClick(this.OpenPackageList);
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x0023F21C File Offset: 0x0023D41C
		private void OpenPackageList(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			if (slotData.eType != NKCUISlot.eSlotMode.ItemMisc)
			{
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
			if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsPackageItem)
			{
				NKCUISlotListViewer newInstance = NKCUISlotListViewer.GetNewInstance();
				if (newInstance != null)
				{
					newInstance.OpenPackageInfo(this.m_SlotData.ID);
				}
			}
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x0023F276 File Offset: 0x0023D476
		private void SetOpenChoiceList()
		{
			this.SetItemDetailIcon(true);
			this.dOnClick = new NKCUISlot.OnClick(this.OpenChoiceList);
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x0023F294 File Offset: 0x0023D494
		private void OpenChoiceList(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			if (slotData.eType != NKCUISlot.eSlotMode.ItemMisc)
			{
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
			if (itemMiscTempletByID != null && itemMiscTempletByID.IsChoiceItem())
			{
				NKCUISlotListViewer newInstance = NKCUISlotListViewer.GetNewInstance();
				if (newInstance != null)
				{
					newInstance.OpenChoiceInfo(this.m_SlotData.ID);
				}
			}
		}

		// Token: 0x06006D8A RID: 28042 RVA: 0x0023F2E8 File Offset: 0x0023D4E8
		public void SetBackGround(int grade)
		{
			switch (grade)
			{
			case 0:
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			case 1:
				this.m_imgBG.sprite = this.m_spBGRarityR;
				return;
			case 2:
				this.m_imgBG.sprite = this.m_spBGRaritySR;
				return;
			case 3:
				this.m_imgBG.sprite = this.m_spBGRaritySSR;
				return;
			case 4:
				this.m_imgBG.sprite = this.m_spBGRarityEPIC;
				return;
			default:
				Debug.LogError("Unit BG undefined");
				this.m_imgBG.sprite = this.m_spBGRarityN;
				return;
			}
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x0023F386 File Offset: 0x0023D586
		public void SetAdditionalText(string text, TextAnchor alignment = TextAnchor.MiddleCenter)
		{
			if (this.m_lbAdditionalText != null)
			{
				this.m_lbAdditionalText.alignment = alignment;
			}
			NKCUtil.SetLabelText(this.m_lbAdditionalText, text);
			NKCUtil.SetGameobjectActive(this.m_lbAdditionalText, true);
		}

		// Token: 0x06006D8C RID: 28044 RVA: 0x0023F3BA File Offset: 0x0023D5BA
		public void SetDenied(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_DISABLE, value);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_DENIED, value);
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x0023F3D4 File Offset: 0x0023D5D4
		public void SetDisable(bool disable, string text = "")
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_DISABLE, disable);
			if (disable && !string.IsNullOrEmpty(text))
			{
				NKCUtil.SetLabelText(this.m_lbDisable, text);
				NKCUtil.SetGameobjectActive(this.m_lbDisable, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbDisable, false);
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x0023F414 File Offset: 0x0023D614
		public void SetUsable(bool usable)
		{
			if (usable)
			{
				bool flag = false;
				NKMItemMiscTemplet nkmitemMiscTemplet = null;
				if (this.m_SlotData != null && this.m_SlotData.eType == NKCUISlot.eSlotMode.ItemMisc)
				{
					nkmitemMiscTemplet = NKMItemManager.GetItemMiscTempletByID(this.m_SlotData.ID);
				}
				if (nkmitemMiscTemplet != null)
				{
					flag = nkmitemMiscTemplet.WillBeDeletedSoon();
				}
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_INDUCE_ARROW, !flag);
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_INDUCE_ARROW_RED, flag);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_INDUCE_ARROW, usable);
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_INDUCE_ARROW_RED, usable);
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x0023F48D File Offset: 0x0023D68D
		public void SetClear(bool clear)
		{
			NKCUtil.SetGameobjectActive(this.m_objCompleteMark, clear);
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x0023F49B File Offset: 0x0023D69B
		public void SetSelected(bool bSelected)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelected, bSelected);
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x0023F4A9 File Offset: 0x0023D6A9
		public bool GetSelected()
		{
			return !(this.m_objSelected == null) && this.m_objSelected.activeSelf;
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x0023F4C6 File Offset: 0x0023D6C6
		public void SetItemDetailIcon(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objItemDetail, bValue);
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x0023F4D4 File Offset: 0x0023D6D4
		public void SetSeized(bool bSeized)
		{
			NKCUtil.SetGameobjectActive(this.m_objSeized, bSeized);
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x0023F4E2 File Offset: 0x0023D6E2
		public void SetRewardFx(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_objRewardFx, bActive);
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x0023F4F0 File Offset: 0x0023D6F0
		public void SetEventGet(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_objEventGet, bActive);
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x0023F4FE File Offset: 0x0023D6FE
		public void SetTopNotice(string notice, bool bActive)
		{
			NKCUtil.SetLabelText(this.m_lbTopNoticeText, notice);
			NKCUtil.SetGameobjectActive(this.m_objTopNotice, bActive);
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x0023F518 File Offset: 0x0023D718
		public void SetEquipEffect(bool bShowEffect)
		{
			NKCUtil.SetGameobjectActive(this.m_objTier_7, bShowEffect);
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x0023F528 File Offset: 0x0023D728
		public void SetRelic(NKCUISlot.SlotData slotData)
		{
			NKCUtil.SetGameobjectActive(this.m_objRelic, false);
			if (NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(slotData.UID) == null)
			{
				NKCEquipSortSystem.MakeTempEquipData(slotData.ID, slotData.GroupID, false);
			}
			if (NKMItemManager.GetEquipTemplet(slotData.ID) == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRelic, true);
			for (int i = 0; i < this.m_lstImgRelic.Count; i++)
			{
				if (i < 0)
				{
					NKCUtil.SetGameobjectActive(this.m_lstImgRelic[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstImgRelic[i], false);
				}
			}
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x0023F5CA File Offset: 0x0023D7CA
		public void OnPress()
		{
			if (this.dOnClick != null)
			{
				this.dOnClick(this.m_SlotData, this.IsLocked);
			}
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x0023F5EB File Offset: 0x0023D7EB
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x0023F60A File Offset: 0x0023D80A
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06006D9C RID: 28060 RVA: 0x0023F617 File Offset: 0x0023D817
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.dOnPointerDown != null)
			{
				this.dOnPointerDown(this.m_SlotData, this.IsLocked, eventData);
			}
		}

		// Token: 0x06006D9D RID: 28061 RVA: 0x0023F639 File Offset: 0x0023D839
		public void SetRedudantMark(bool value)
		{
			if (value)
			{
				this.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_ICON_SLOT_ALREADY_HAVE", false));
				return;
			}
			this.SetHaveCountString(false, null);
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x0023F659 File Offset: 0x0023D859
		public void SetDuplicateSelection(bool value)
		{
			if (value)
			{
				this.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_SHOP_CUSTOM_DUPLICATE", false));
				return;
			}
			this.SetHaveCountString(false, null);
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x0023F679 File Offset: 0x0023D879
		public static string GetName(NKCUISlot.SlotData slotData)
		{
			if (slotData == null)
			{
				return "";
			}
			return NKCUISlot.GetName(slotData.eType, slotData.ID);
		}

		// Token: 0x06006DA0 RID: 28064 RVA: 0x0023F698 File Offset: 0x0023D898
		public static string GetName(NKCUISlot.eSlotMode type, int ID)
		{
			switch (type)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(ID);
				if (unitTempletBase != null)
				{
					return unitTempletBase.GetUnitName();
				}
				break;
			}
			case NKCUISlot.eSlotMode.ItemMisc:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(ID);
				if (itemMiscTempletByID != null)
				{
					return itemMiscTempletByID.GetItemName();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(ID);
				if (equipTemplet != null)
				{
					return NKCUtilString.GetItemEquipNameWithTier(equipTemplet);
				}
				break;
			}
			case NKCUISlot.eSlotMode.Skin:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(ID);
				if (skinTemplet != null)
				{
					return skinTemplet.GetTitle();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Mold:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(ID);
				if (itemMoldTempletByID != null)
				{
					return itemMoldTempletByID.GetItemName();
				}
				break;
			}
			case NKCUISlot.eSlotMode.DiveArtifact:
			{
				NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(ID);
				if (nkmdiveArtifactTemplet != null)
				{
					return nkmdiveArtifactTemplet.ArtifactName_Translated;
				}
				break;
			}
			case NKCUISlot.eSlotMode.Buff:
			{
				NKMCompanyBuffTemplet nkmcompanyBuffTemplet = NKMCompanyBuffTemplet.Find(ID);
				if (nkmcompanyBuffTemplet != null)
				{
					return nkmcompanyBuffTemplet.GetBuffName();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Emoticon:
			{
				NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(ID);
				if (nkmemoticonTemplet != null)
				{
					return nkmemoticonTemplet.GetEmoticonName();
				}
				break;
			}
			case NKCUISlot.eSlotMode.GuildArtifact:
			{
				GuildDungeonArtifactTemplet artifactTemplet = GuildDungeonTempletManager.GetArtifactTemplet(ID);
				if (artifactTemplet != null)
				{
					return artifactTemplet.GetName();
				}
				break;
			}
			}
			return "";
		}

		// Token: 0x06006DA1 RID: 28065 RVA: 0x0023F78F File Offset: 0x0023D98F
		public static string GetDesc(NKCUISlot.SlotData slotData, bool bFull = false)
		{
			if (slotData == null)
			{
				return "";
			}
			return NKCUISlot.GetDesc(slotData.eType, slotData.ID, bFull);
		}

		// Token: 0x06006DA2 RID: 28066 RVA: 0x0023F7AC File Offset: 0x0023D9AC
		public static string GetDesc(NKCUISlot.eSlotMode type, int ID, bool bFull = false)
		{
			switch (type)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
				if (bFull)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(ID);
					if (unitTempletBase != null)
					{
						return unitTempletBase.GetUnitDesc();
					}
				}
				break;
			case NKCUISlot.eSlotMode.ItemMisc:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(ID);
				if (itemMiscTempletByID != null)
				{
					return itemMiscTempletByID.GetItemDesc();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(ID);
				if (equipTemplet != null)
				{
					return equipTemplet.GetItemDesc();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Skin:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(ID);
				if (skinTemplet != null)
				{
					return skinTemplet.GetSkinDesc();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Mold:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(ID);
				if (itemMoldTempletByID != null)
				{
					return itemMoldTempletByID.GetItemDesc();
				}
				break;
			}
			case NKCUISlot.eSlotMode.DiveArtifact:
			{
				NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(ID);
				if (nkmdiveArtifactTemplet != null)
				{
					return nkmdiveArtifactTemplet.ArtifactMiscDesc_1_Translated;
				}
				break;
			}
			case NKCUISlot.eSlotMode.Buff:
			{
				NKMCompanyBuffTemplet nkmcompanyBuffTemplet = NKMCompanyBuffTemplet.Find(ID);
				if (nkmcompanyBuffTemplet != null)
				{
					return nkmcompanyBuffTemplet.GetBuffDescForItemPopup();
				}
				break;
			}
			case NKCUISlot.eSlotMode.Emoticon:
			{
				NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(ID);
				if (nkmemoticonTemplet != null)
				{
					return nkmemoticonTemplet.GetEmoticonDesc();
				}
				break;
			}
			case NKCUISlot.eSlotMode.GuildArtifact:
			{
				GuildDungeonArtifactTemplet artifactTemplet = GuildDungeonTempletManager.GetArtifactTemplet(ID);
				if (artifactTemplet != null)
				{
					if (!bFull)
					{
						return artifactTemplet.GetDescShort();
					}
					return artifactTemplet.GetDescFull();
				}
				break;
			}
			}
			return "";
		}

		// Token: 0x06006DA3 RID: 28067 RVA: 0x0023F8B7 File Offset: 0x0023DAB7
		public void SetHotkey(HotkeyEventType eventType)
		{
			NKCUtil.SetHotkey(this.m_cbtnButton, eventType);
		}

		// Token: 0x040058FF RID: 22783
		[Header("Common")]
		public Image m_imgIcon;

		// Token: 0x04005900 RID: 22784
		public Text m_lbName;

		// Token: 0x04005901 RID: 22785
		public Image m_imgUpperRightIcon;

		// Token: 0x04005902 RID: 22786
		public LayoutElement m_layoutElement;

		// Token: 0x04005903 RID: 22787
		public NKCUIComButton m_cbtnButton;

		// Token: 0x04005904 RID: 22788
		public GameObject m_objItemCount;

		// Token: 0x04005905 RID: 22789
		public Text m_lbItemCount;

		// Token: 0x04005906 RID: 22790
		public Text m_lbItemAddCount;

		// Token: 0x04005907 RID: 22791
		public Text m_lbAdditionalText;

		// Token: 0x04005908 RID: 22792
		[Header("별")]
		public GameObject m_objStarRoot;

		// Token: 0x04005909 RID: 22793
		public List<GameObject> m_lstStar;

		// Token: 0x0400590A RID: 22794
		[Header("Background images")]
		public Image m_imgBG;

		// Token: 0x0400590B RID: 22795
		public Sprite m_spLocked;

		// Token: 0x0400590C RID: 22796
		public Sprite m_spEmpty;

		// Token: 0x0400590D RID: 22797
		public Sprite m_sp_MatAdd;

		// Token: 0x0400590E RID: 22798
		public Sprite m_spBGRarityN;

		// Token: 0x0400590F RID: 22799
		public Sprite m_spBGRarityR;

		// Token: 0x04005910 RID: 22800
		public Sprite m_spBGRaritySR;

		// Token: 0x04005911 RID: 22801
		public Sprite m_spBGRaritySSR;

		// Token: 0x04005912 RID: 22802
		public Sprite m_spBGRarityEPIC;

		// Token: 0x04005913 RID: 22803
		[FormerlySerializedAs("m_spBGInteriorRarityN")]
		public Sprite m_spBGWhiteRarityN;

		// Token: 0x04005914 RID: 22804
		[FormerlySerializedAs("m_spBGInteriorRarityR")]
		public Sprite m_spBGWhiteRarityR;

		// Token: 0x04005915 RID: 22805
		[FormerlySerializedAs("m_spBGInteriorRaritySR")]
		public Sprite m_spBGWhiteRaritySR;

		// Token: 0x04005916 RID: 22806
		[FormerlySerializedAs("m_spBGInteriorRaritySSR")]
		public Sprite m_spBGWhiteRaritySSR;

		// Token: 0x04005917 RID: 22807
		public Sprite m_spEmblem;

		// Token: 0x04005918 RID: 22808
		public Sprite m_spBGLock;

		// Token: 0x04005919 RID: 22809
		[Header("Extra")]
		public GameObject m_objCompleteMark;

		// Token: 0x0400591A RID: 22810
		public GameObject m_objFirstClear;

		// Token: 0x0400591B RID: 22811
		public Image m_imgFirstClearBG;

		// Token: 0x0400591C RID: 22812
		public Text m_lbFirstClear;

		// Token: 0x0400591D RID: 22813
		public GameObject m_objSelected;

		// Token: 0x0400591E RID: 22814
		public GameObject m_objTier;

		// Token: 0x0400591F RID: 22815
		public Text m_lbTier;

		// Token: 0x04005920 RID: 22816
		public GameObject m_AB_ICON_SLOT_DISABLE;

		// Token: 0x04005921 RID: 22817
		public Text m_lbDisable;

		// Token: 0x04005922 RID: 22818
		public GameObject m_AB_ICON_SLOT_CLEARED;

		// Token: 0x04005923 RID: 22819
		public GameObject m_AB_ICON_SLOT_DENIED;

		// Token: 0x04005924 RID: 22820
		public GameObject m_AB_ICON_SLOT_INDUCE_ARROW;

		// Token: 0x04005925 RID: 22821
		public GameObject m_AB_ICON_SLOT_INDUCE_ARROW_RED;

		// Token: 0x04005926 RID: 22822
		public GameObject m_objItemDetail;

		// Token: 0x04005927 RID: 22823
		public GameObject m_objSeized;

		// Token: 0x04005928 RID: 22824
		public GameObject m_objRewardFx;

		// Token: 0x04005929 RID: 22825
		public GameObject m_objEventGet;

		// Token: 0x0400592A RID: 22826
		public GameObject m_objTopNotice;

		// Token: 0x0400592B RID: 22827
		public GameObject m_objTimeInterval;

		// Token: 0x0400592C RID: 22828
		public Text m_lbTopNoticeText;

		// Token: 0x0400592D RID: 22829
		[Header("보유 갯수 표시")]
		public GameObject m_objHaveCount;

		// Token: 0x0400592E RID: 22830
		public Text m_lbHaveCount;

		// Token: 0x0400592F RID: 22831
		[Header("유닛 각성 애니")]
		public Animator m_animAwakenFX;

		// Token: 0x04005930 RID: 22832
		[Header("세트 장비 아이콘")]
		public Image m_AB_ICON_SLOT_EQUIP_SET_ICON;

		// Token: 0x04005931 RID: 22833
		[Header("격전지원 착용 표시")]
		public GameObject m_EQUIP_FIERCE_BATTLE;

		// Token: 0x04005932 RID: 22834
		[Header("잠재 개방")]
		public GameObject m_objRelic;

		// Token: 0x04005933 RID: 22835
		public List<Image> m_lstImgRelic;

		// Token: 0x04005934 RID: 22836
		[Header("티어7 표시")]
		public GameObject m_objTier_7;

		// Token: 0x04005935 RID: 22837
		private const int MAX_ITEM_NAME_COUNT = 7;

		// Token: 0x04005936 RID: 22838
		private NKCUISlot.SlotData m_SlotData;

		// Token: 0x04005938 RID: 22840
		private NKCUISlot.OnClick dOnClick;

		// Token: 0x04005939 RID: 22841
		private NKCUISlot.PointerDown dOnPointerDown;

		// Token: 0x0400593A RID: 22842
		private bool m_bEmpty = true;

		// Token: 0x0400593B RID: 22843
		private bool m_bCustomizedEmptySP;

		// Token: 0x0400593C RID: 22844
		private Sprite m_spCustomizedEmpty;

		// Token: 0x0400593D RID: 22845
		private NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE m_EQUIP_BOX_BOTTOM_MENU_TYPE = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_ENFORCE_AND_EQUIP;

		// Token: 0x0400593E RID: 22846
		private bool m_bUseBigImg;

		// Token: 0x0400593F RID: 22847
		private NKCAssetInstanceData m_NKCAssetInstanceData;

		// Token: 0x04005940 RID: 22848
		private bool m_bFirstReward;

		// Token: 0x04005941 RID: 22849
		private bool m_bOneTimeReward;

		// Token: 0x04005942 RID: 22850
		private bool m_bFirstAllClear;

		// Token: 0x020016F5 RID: 5877
		// (Invoke) Token: 0x0600B1CE RID: 45518
		public delegate void OnClick(NKCUISlot.SlotData slotData, bool bLocked);

		// Token: 0x020016F6 RID: 5878
		// (Invoke) Token: 0x0600B1D2 RID: 45522
		public delegate void PointerDown(NKCUISlot.SlotData slotData, bool bLocked, PointerEventData eventData);

		// Token: 0x020016F7 RID: 5879
		public enum eSlotMode
		{
			// Token: 0x0400A5A1 RID: 42401
			Unit,
			// Token: 0x0400A5A2 RID: 42402
			ItemMisc,
			// Token: 0x0400A5A3 RID: 42403
			Equip,
			// Token: 0x0400A5A4 RID: 42404
			EquipCount,
			// Token: 0x0400A5A5 RID: 42405
			Skin,
			// Token: 0x0400A5A6 RID: 42406
			Mold,
			// Token: 0x0400A5A7 RID: 42407
			DiveArtifact,
			// Token: 0x0400A5A8 RID: 42408
			Buff,
			// Token: 0x0400A5A9 RID: 42409
			UnitCount,
			// Token: 0x0400A5AA RID: 42410
			Emoticon,
			// Token: 0x0400A5AB RID: 42411
			GuildArtifact,
			// Token: 0x0400A5AC RID: 42412
			Etc
		}

		// Token: 0x020016F8 RID: 5880
		public class SlotData
		{
			// Token: 0x0600B1D5 RID: 45525 RVA: 0x00361627 File Offset: 0x0035F827
			public SlotData()
			{
			}

			// Token: 0x0600B1D6 RID: 45526 RVA: 0x00361630 File Offset: 0x0035F830
			public SlotData(NKCUISlot.SlotData slotData)
			{
				if (slotData != null)
				{
					this.eType = slotData.eType;
					this.ID = slotData.ID;
					this.Count = slotData.Count;
					this.UID = slotData.UID;
					this.Data = slotData.Data;
					this.GroupID = slotData.GroupID;
					this.BonusRate = slotData.BonusRate;
				}
			}

			// Token: 0x0600B1D7 RID: 45527 RVA: 0x0036169C File Offset: 0x0035F89C
			public static List<NKCUISlot.SlotData> MakeUnitData(List<NKMUnitData> lstUnitData, bool bSort = true)
			{
				if (lstUnitData == null)
				{
					return new List<NKCUISlot.SlotData>();
				}
				if (bSort)
				{
					List<NKMUnitData> list = new List<NKMUnitData>(lstUnitData);
					list.Sort(new Comparison<NKMUnitData>(NKCUISlot.RewardUnitSort));
					return (from x in list
					select NKCUISlot.SlotData.MakeUnitData(x)).ToList<NKCUISlot.SlotData>();
				}
				return (from x in lstUnitData
				select NKCUISlot.SlotData.MakeUnitData(x)).ToList<NKCUISlot.SlotData>();
			}

			// Token: 0x0600B1D8 RID: 45528 RVA: 0x00361721 File Offset: 0x0035F921
			public static NKCUISlot.SlotData MakeUnitData(int unitID, int level, int SkinID = 0, int GroupID = 0)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.Unit,
					ID = unitID,
					Count = (long)level,
					Data = SkinID,
					GroupID = GroupID
				};
			}

			// Token: 0x0600B1D9 RID: 45529 RVA: 0x0036174C File Offset: 0x0035F94C
			public static NKCUISlot.SlotData MakeUnitData(NKMUnitData unitData)
			{
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
				slotData.eType = NKCUISlot.eSlotMode.Unit;
				if (unitData != null)
				{
					slotData.ID = unitData.m_UnitID;
					slotData.Count = (long)unitData.m_UnitLevel;
					slotData.UID = unitData.m_UnitUID;
					slotData.Data = unitData.m_SkinID;
				}
				else
				{
					slotData.ID = 0;
					slotData.Count = 1L;
					slotData.UID = 0L;
					slotData.Data = 0;
				}
				return slotData;
			}

			// Token: 0x0600B1DA RID: 45530 RVA: 0x003617BC File Offset: 0x0035F9BC
			public static NKCUISlot.SlotData MakeUnitData(NKMOperator operatorData)
			{
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
				slotData.eType = NKCUISlot.eSlotMode.Unit;
				if (operatorData != null)
				{
					slotData.ID = operatorData.id;
					slotData.Count = (long)operatorData.level;
					slotData.UID = operatorData.uid;
					slotData.Data = 0;
				}
				else
				{
					slotData.ID = 0;
					slotData.Count = 1L;
					slotData.UID = 0L;
					slotData.Data = 0;
				}
				return slotData;
			}

			// Token: 0x0600B1DB RID: 45531 RVA: 0x00361826 File Offset: 0x0035FA26
			public static NKCUISlot.SlotData MakeUnitCountData(int unitID, int count, int SkinID = 0, int GroupID = 0)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.UnitCount,
					ID = unitID,
					Count = (long)count,
					Data = SkinID,
					GroupID = GroupID
				};
			}

			// Token: 0x0600B1DC RID: 45532 RVA: 0x00361851 File Offset: 0x0035FA51
			public static NKCUISlot.SlotData MakeMoldItemData(int itemID, long Count)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.Mold,
					ID = itemID,
					Count = Count
				};
			}

			// Token: 0x0600B1DD RID: 45533 RVA: 0x00361870 File Offset: 0x0035FA70
			public static NKCUISlot.SlotData MakeMoldItemData(NKMMoldItemData itemData)
			{
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
				slotData.eType = NKCUISlot.eSlotMode.Mold;
				if (itemData != null)
				{
					slotData.ID = itemData.m_MoldID;
					slotData.Count = itemData.m_Count;
				}
				else
				{
					slotData.ID = 0;
					slotData.Count = 0L;
				}
				return slotData;
			}

			// Token: 0x0600B1DE RID: 45534 RVA: 0x003618B7 File Offset: 0x0035FAB7
			public static NKCUISlot.SlotData MakeMiscItemData(int itemID, long Count, int BonusRate = 0)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.ItemMisc,
					ID = itemID,
					Count = Count,
					BonusRate = BonusRate
				};
			}

			// Token: 0x0600B1DF RID: 45535 RVA: 0x003618DC File Offset: 0x0035FADC
			public static NKCUISlot.SlotData MakeMiscItemData(NKMItemMiscData itemData, int BonusRate = 0)
			{
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
				slotData.eType = NKCUISlot.eSlotMode.ItemMisc;
				if (itemData != null)
				{
					slotData.ID = itemData.ItemID;
					slotData.Count = itemData.TotalCount;
					slotData.BonusRate = BonusRate;
				}
				else
				{
					slotData.ID = 0;
					slotData.Count = 0L;
					slotData.BonusRate = 0;
				}
				return slotData;
			}

			// Token: 0x0600B1E0 RID: 45536 RVA: 0x00361931 File Offset: 0x0035FB31
			public static NKCUISlot.SlotData MakeEquipData(int itemID, int Level, int setOptionID = 0)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.EquipCount,
					ID = itemID,
					Count = (long)Level,
					GroupID = setOptionID
				};
			}

			// Token: 0x0600B1E1 RID: 45537 RVA: 0x00361958 File Offset: 0x0035FB58
			public static NKCUISlot.SlotData MakeEquipData(NKMEquipItemData equipData)
			{
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
				slotData.eType = NKCUISlot.eSlotMode.Equip;
				if (equipData != null)
				{
					slotData.ID = equipData.m_ItemEquipID;
					slotData.Count = (long)equipData.m_EnchantLevel;
					slotData.UID = equipData.m_ItemUid;
					slotData.GroupID = equipData.m_SetOptionId;
				}
				else
				{
					slotData.ID = 0;
					slotData.Count = 1L;
					slotData.UID = 0L;
					slotData.GroupID = 0;
				}
				return slotData;
			}

			// Token: 0x0600B1E2 RID: 45538 RVA: 0x003619C7 File Offset: 0x0035FBC7
			public static NKCUISlot.SlotData MakePostItemData(NKMRewardInfo postItem)
			{
				return NKCUISlot.SlotData.MakeRewardTypeData(postItem.rewardType, postItem.ID, postItem.Count, 0);
			}

			// Token: 0x0600B1E3 RID: 45539 RVA: 0x003619E1 File Offset: 0x0035FBE1
			public static NKCUISlot.SlotData MakeBuffItemData(int itemID, int Count)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.Buff,
					ID = itemID,
					Count = (long)Count
				};
			}

			// Token: 0x0600B1E4 RID: 45540 RVA: 0x00361A00 File Offset: 0x0035FC00
			public static NKCUISlot.SlotData MakeShopItemData(ShopItemTemplet shopTemplet, bool bFirstBuy)
			{
				if (shopTemplet == null)
				{
					Debug.LogError("ShopTemplet Null!");
					return NKCUISlot.SlotData.MakeMiscItemData(1, 0L, 0);
				}
				if (bFirstBuy && shopTemplet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.FIRST_PURCHASE_CHANGE_REWARD_VALUE)
				{
					return NKCUISlot.SlotData.MakeRewardTypeData(shopTemplet.m_ItemType, shopTemplet.m_ItemID, shopTemplet.m_PurchaseEventValue, 0);
				}
				return NKCUISlot.SlotData.MakeRewardTypeData(shopTemplet.m_ItemType, shopTemplet.m_ItemID, shopTemplet.TotalValue, 0);
			}

			// Token: 0x0600B1E5 RID: 45541 RVA: 0x00361A60 File Offset: 0x0035FC60
			public static NKCUISlot.SlotData MakeShopItemData(NKMShopRandomListData shopRandomTemplet)
			{
				if (shopRandomTemplet == null)
				{
					Debug.LogError("NKMShopRandomListData Null!");
					return NKCUISlot.SlotData.MakeMiscItemData(1, 0L, 0);
				}
				return NKCUISlot.SlotData.MakeRewardTypeData(shopRandomTemplet.itemType, shopRandomTemplet.itemId, shopRandomTemplet.itemCount, 0);
			}

			// Token: 0x0600B1E6 RID: 45542 RVA: 0x00361A91 File Offset: 0x0035FC91
			public static NKCUISlot.SlotData MakeSkinData(int skinID)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.Skin,
					ID = skinID
				};
			}

			// Token: 0x0600B1E7 RID: 45543 RVA: 0x00361AA6 File Offset: 0x0035FCA6
			public static NKCUISlot.SlotData MakeRewardTypeData(NKMRewardInfo info, int bonusRate = 0)
			{
				if (info == null)
				{
					return null;
				}
				return NKCUISlot.SlotData.MakeRewardTypeData(info.rewardType, info.ID, info.Count, bonusRate);
			}

			// Token: 0x0600B1E8 RID: 45544 RVA: 0x00361AC5 File Offset: 0x0035FCC5
			public static NKCUISlot.SlotData MakeDiveArtifactData(int id, int count = 1)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.DiveArtifact,
					ID = id,
					Count = (long)count
				};
			}

			// Token: 0x0600B1E9 RID: 45545 RVA: 0x00361AE2 File Offset: 0x0035FCE2
			public static NKCUISlot.SlotData MakeEmoticonData(int id, int count = 1)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.Emoticon,
					ID = id,
					Count = (long)count
				};
			}

			// Token: 0x0600B1EA RID: 45546 RVA: 0x00361B00 File Offset: 0x0035FD00
			public static NKCUISlot.SlotData MakeGuildArtifactData(int id, int count = 1)
			{
				return new NKCUISlot.SlotData
				{
					eType = NKCUISlot.eSlotMode.GuildArtifact,
					ID = id,
					Count = (long)count
				};
			}

			// Token: 0x0600B1EB RID: 45547 RVA: 0x00361B20 File Offset: 0x0035FD20
			public static NKCUISlot.SlotData MakeRewardTypeData(NKM_REWARD_TYPE type, int ID, int count, int bonusRate = 0)
			{
				switch (type)
				{
				case NKM_REWARD_TYPE.RT_UNIT:
				case NKM_REWARD_TYPE.RT_SHIP:
				case NKM_REWARD_TYPE.RT_OPERATOR:
					return NKCUISlot.SlotData.MakeUnitCountData(ID, count, 0, 0);
				case NKM_REWARD_TYPE.RT_MISC:
				case NKM_REWARD_TYPE.RT_MISSION_POINT:
				case NKM_REWARD_TYPE.RT_PASS_EXP:
					return NKCUISlot.SlotData.MakeMiscItemData(ID, (long)count, bonusRate);
				case NKM_REWARD_TYPE.RT_USER_EXP:
					return NKCUISlot.SlotData.MakeMiscItemData(501, (long)count, bonusRate);
				case NKM_REWARD_TYPE.RT_EQUIP:
					return NKCUISlot.SlotData.MakeEquipData(ID, count, 0);
				case NKM_REWARD_TYPE.RT_MOLD:
					return NKCUISlot.SlotData.MakeMoldItemData(ID, (long)count);
				case NKM_REWARD_TYPE.RT_SKIN:
					return NKCUISlot.SlotData.MakeSkinData(ID);
				case NKM_REWARD_TYPE.RT_BUFF:
					return NKCUISlot.SlotData.MakeBuffItemData(ID, count);
				case NKM_REWARD_TYPE.RT_EMOTICON:
					return NKCUISlot.SlotData.MakeEmoticonData(ID, count);
				}
				Debug.LogError("Undefined type");
				return NKCUISlot.SlotData.MakeMiscItemData(1, 0L, 0);
			}

			// Token: 0x0400A5AD RID: 42413
			public NKCUISlot.eSlotMode eType;

			// Token: 0x0400A5AE RID: 42414
			public int ID;

			// Token: 0x0400A5AF RID: 42415
			public long Count;

			// Token: 0x0400A5B0 RID: 42416
			public long UID;

			// Token: 0x0400A5B1 RID: 42417
			public int Data;

			// Token: 0x0400A5B2 RID: 42418
			public int GroupID;

			// Token: 0x0400A5B3 RID: 42419
			public int BonusRate;
		}

		// Token: 0x020016F9 RID: 5881
		public enum SlotClickType
		{
			// Token: 0x0400A5B5 RID: 42421
			Tooltip,
			// Token: 0x0400A5B6 RID: 42422
			ItemBox,
			// Token: 0x0400A5B7 RID: 42423
			RatioList,
			// Token: 0x0400A5B8 RID: 42424
			BoxList,
			// Token: 0x0400A5B9 RID: 42425
			MoldList,
			// Token: 0x0400A5BA RID: 42426
			ChoiceList
		}
	}
}
