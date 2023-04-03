using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A5 RID: 2469
	public class NKCUIHangarShipyardPopup : NKCUIBase
	{
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060066E2 RID: 26338 RVA: 0x0020F6E8 File Offset: 0x0020D8E8
		public static NKCUIHangarShipyardPopup Instance
		{
			get
			{
				if (NKCUIHangarShipyardPopup.m_Instance == null)
				{
					NKCUIHangarShipyardPopup.m_Instance = NKCUIManager.OpenNewInstance<NKCUIHangarShipyardPopup>("AB_UI_NKM_UI_HANGAR", "NKM_UI_POPUP_HANGAR_SHIPYARD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIHangarShipyardPopup.CleanupInstance)).GetInstance<NKCUIHangarShipyardPopup>();
					NKCUIHangarShipyardPopup.m_Instance.Init();
				}
				return NKCUIHangarShipyardPopup.m_Instance;
			}
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x0020F737 File Offset: 0x0020D937
		private static void CleanupInstance()
		{
			NKCUIHangarShipyardPopup.m_Instance = null;
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060066E4 RID: 26340 RVA: 0x0020F73F File Offset: 0x0020D93F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIHangarShipyardPopup.m_Instance != null && NKCUIHangarShipyardPopup.m_Instance.IsOpen;
			}
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0020F75A File Offset: 0x0020D95A
		public static void CheckInstanceAndClose()
		{
			if (NKCUIHangarShipyardPopup.m_Instance != null && NKCUIHangarShipyardPopup.m_Instance.IsOpen)
			{
				NKCUIHangarShipyardPopup.m_Instance.Close();
			}
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x0020F77F File Offset: 0x0020D97F
		private void OnDestroy()
		{
			NKCUIHangarShipyardPopup.m_Instance = null;
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060066E7 RID: 26343 RVA: 0x0020F787 File Offset: 0x0020D987
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060066E8 RID: 26344 RVA: 0x0020F78A File Offset: 0x0020D98A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_MENU_NAME_SHIPYARD_CONFIRM;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x0020F791 File Offset: 0x0020D991
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060066EA RID: 26346 RVA: 0x0020F794 File Offset: 0x0020D994
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x0020F79C File Offset: 0x0020D99C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x0020F7AC File Offset: 0x0020D9AC
		public void Init()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUISlot costShipSlot = this.m_costShipSlot;
			if (costShipSlot != null)
			{
				costShipSlot.Init();
			}
			NKCUIItemCostSlot[] costSlot = this.m_costSlot;
			for (int i = 0; i < costSlot.Length; i++)
			{
				NKCUtil.SetGameobjectActive(costSlot[i], false);
			}
			if (this.NKM_UI_POPUP_CANCEL != null)
			{
				this.NKM_UI_POPUP_CANCEL.PointerClick.RemoveAllListeners();
				this.NKM_UI_POPUP_CANCEL.PointerClick.AddListener(new UnityAction(this.ButtonCancel));
			}
			if (this.NKM_UI_POPUP_OK != null)
			{
				this.NKM_UI_POPUP_OK.PointerClick.RemoveAllListeners();
				this.NKM_UI_POPUP_OK.PointerClick.AddListener(new UnityAction(this.ButtonOk));
				NKCUtil.SetHotkey(this.NKM_UI_POPUP_OK, HotkeyEventType.Confirm);
			}
			this.m_openAni = new NKCUIOpenAnimator(base.gameObject);
			for (int j = 0; j < this.m_PrevSkillSlot.Length; j++)
			{
				this.m_PrevSkillSlot[j].Init(null, true);
			}
			for (int k = 0; k < this.m_NextSkillSlot.Length; k++)
			{
				this.m_NextSkillSlot[k].Init(null, true);
			}
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x0020F8CC File Offset: 0x0020DACC
		public void Open(NKCUIShipInfoRepair.ShipRepairInfo targetShipData, UnityAction TryShipLevelUp, UnityAction TryShipUpgrade, UnityAction TryShipLimitBreak, NKCUISlot.SlotData costShipSlotData = null)
		{
			if (targetShipData == null || targetShipData.ShipData == null)
			{
				return;
			}
			this.dOnConfirmLevelUp = TryShipLevelUp;
			this.dOnConfirmUpgrade = TryShipUpgrade;
			this.dOnConfirmLimitBreak = TryShipLimitBreak;
			this.m_costShipSlotData = costShipSlotData;
			if (this.m_openAni != null)
			{
				this.m_openAni.PlayOpenAni();
			}
			if (targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
			{
				this.dOnConfirm = this.dOnConfirmLevelUp;
			}
			else if (targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
			{
				this.dOnConfirm = this.dOnConfirmUpgrade;
			}
			else if (targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak)
			{
				this.dOnConfirm = this.dOnConfirmLimitBreak;
			}
			this.UpdateData(targetShipData);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_LEVELUP, targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT.gameObject, targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade);
			NKCUtil.SetGameobjectActive(this.NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_UPGRADE, targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade);
			NKCUtil.SetGameobjectActive(this.m_objLimitBreak, targetShipData.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak);
			base.UIOpened(true);
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x0020F9C8 File Offset: 0x0020DBC8
		private void UpdateData(NKCUIShipInfoRepair.ShipRepairInfo curShipData)
		{
			if (NKCUtil.IsNullObject<NKMUnitData>(curShipData.ShipData, ""))
			{
				return;
			}
			this.UpdateMaterialSlot(curShipData);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(curShipData.ShipData.m_UnitID);
			if (curShipData.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
			{
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_LevelUp_Perv, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, curShipData.ShipData.m_UnitLevel));
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_LevelUp_Next, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, curShipData.iTargetLevel));
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null)
				{
					return;
				}
				if (nkmuserData.m_InventoryData == null)
				{
					return;
				}
				NKCUtil.SetLabelText(this.m_PREV_SHIP_ABILITY_POWER_COUNT, curShipData.ShipData.CalculateOperationPower(nkmuserData.m_InventoryData, curShipData.iCurShipLevel, null, null).ToString());
				NKCUtil.SetLabelText(this.m_PREV_SHIP_ABILITY_ATK_COUNT, Mathf.RoundToInt(curShipData.fCurShipAtk).ToString());
				NKCUtil.SetLabelText(this.m_PREV_SHIP_ABILITY_HP_COUNT, Mathf.RoundToInt(curShipData.fCurShipHP).ToString());
				NKCUtil.SetLabelText(this.m_PREV_SHIP_ABILITY_DEF_COUNT, Mathf.RoundToInt(curShipData.fCurShipDef).ToString());
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_POWER_COUNT, curShipData.ShipData.CalculateOperationPower(nkmuserData.m_InventoryData, curShipData.iTargetLevel, null, null).ToString());
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_ATK_COUNT, Mathf.RoundToInt(curShipData.fNextShipAtk).ToString());
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_ATK_COUNT_ADD, string.Format("(+{0})", Mathf.RoundToInt(curShipData.fNextShipAtk) - Mathf.RoundToInt(curShipData.fCurShipAtk)));
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_HP_COUNT, Mathf.RoundToInt(curShipData.fNextShipHP).ToString());
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_HP_COUNT_ADD, string.Format("(+{0})", Mathf.RoundToInt(curShipData.fNextShipHP) - Mathf.RoundToInt(curShipData.fCurShipHP)));
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_DEF_COUNT, Mathf.RoundToInt(curShipData.fNextShipDef).ToString());
				NKCUtil.SetLabelText(this.m_NEXT_SHIP_ABILITY_DEF_COUNT_ADD, string.Format("(+{0})", Mathf.RoundToInt(curShipData.fNextShipDef) - Mathf.RoundToInt(curShipData.fCurShipDef)));
			}
			else if (curShipData.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
			{
				NKCUtil.SetStarRank(this.m_lstCurStar, curShipData.iCurStar, 6);
				NKCUtil.SetStarRank(this.m_lstNextStar, curShipData.iNextStar, 6);
				if (this.m_NKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT != null)
				{
					this.m_NKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT.localPosition = this.m_lstNextStar[curShipData.iNextStar - 1].GetComponent<RectTransform>().localPosition;
				}
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(curShipData.ShipData.m_UnitID);
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(curShipData.iNextShipID);
				if (unitTempletBase2 == null || unitTempletBase3 == null)
				{
					Debug.LogError(string.Format("curShipTemplet is null : {0} // ship id info : {1}, next : {2}", unitTempletBase2 == null, curShipData.ShipData.m_UnitID, curShipData.iNextShipID));
					return;
				}
				for (int i = 0; i < 3; i++)
				{
					if (this.m_PrevSkillSlot[i] == null || this.m_NextSkillSlot[i] == null)
					{
						Debug.LogError(string.Format("m_PrevSkillSlot is null : cnt {0}", i));
						break;
					}
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase2, i);
					NKMShipSkillTemplet shipSkillTempletByIndex2 = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase3, i);
					if (shipSkillTempletByIndex != null && shipSkillTempletByIndex2 != null)
					{
						this.m_PrevSkillSlot[i].SetData(shipSkillTempletByIndex, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
						if (string.Equals(shipSkillTempletByIndex.m_ShipSkillStrID, shipSkillTempletByIndex2.m_ShipSkillStrID))
						{
							this.m_NextSkillSlot[i].SetData(shipSkillTempletByIndex2, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
						}
						else
						{
							this.m_NextSkillSlot[i].SetData(shipSkillTempletByIndex2, NKCUIShipSkillSlot.eShipSkillSlotStatus.ENHANCE);
						}
						NKCUtil.SetGameobjectActive(this.m_PrevSkillSlot[i].gameObject, true);
						NKCUtil.SetGameobjectActive(this.m_NextSkillSlot[i].gameObject, true);
					}
					else if (shipSkillTempletByIndex == null && shipSkillTempletByIndex2 == null)
					{
						NKCUtil.SetGameobjectActive(this.m_PrevSkillSlot[i].gameObject, false);
						NKCUtil.SetGameobjectActive(this.m_NextSkillSlot[i].gameObject, false);
					}
					else if (shipSkillTempletByIndex == null && shipSkillTempletByIndex2 != null)
					{
						NKCUtil.SetGameobjectActive(this.m_PrevSkillSlot[i].gameObject, false);
						NKCUtil.SetGameobjectActive(this.m_NextSkillSlot[i].gameObject, true);
						this.m_NextSkillSlot[i].SetData(shipSkillTempletByIndex2, NKCUIShipSkillSlot.eShipSkillSlotStatus.NEW);
					}
				}
			}
			else if (curShipData.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak)
			{
				NKCUtil.SetLabelText(this.m_lbPrevLimitBreakLevel, string.Format(NKCUtilString.GET_STRING_SHIP_LIMITBREAK_GRADE, curShipData.ShipData.m_LimitBreakLevel));
				NKCUtil.SetLabelText(this.m_lbNextLimitBreakLevel, string.Format(NKCUtilString.GET_STRING_SHIP_LIMITBREAK_GRADE, (int)(curShipData.ShipData.m_LimitBreakLevel + 1)));
				NKCUtil.SetLabelText(this.m_lbNewModule, string.Format(NKCUtilString.GET_STRING_SHIP_LIMITBREAK_GRADE_COMMANDMODULE_UNLOCK, (int)(curShipData.ShipData.m_LimitBreakLevel + 1)));
				for (int j = 0; j < this.m_lstModuleBefore.Count; j++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleBefore[j], j < (int)curShipData.ShipData.m_LimitBreakLevel);
					NKCUtil.SetGameobjectActive(this.m_lstModuleAfter[j], j < (int)(curShipData.ShipData.m_LimitBreakLevel + 1));
				}
			}
			this.SetTitleText(curShipData.eRepairState, unitTempletBase.GetUnitName());
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x0020FF14 File Offset: 0x0020E114
		private void UpdateMaterialSlot(NKCUIShipInfoRepair.ShipRepairInfo curShipInfo)
		{
			NKCUtil.SetGameobjectActive(this.m_costShipSlot, curShipInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak);
			if (curShipInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak)
			{
				this.m_costShipSlot.SetData(this.m_costShipSlotData, true, null);
				this.m_costShipSlot.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(this.m_costSlot[0], curShipInfo.iNeedCredit > 0);
			if (curShipInfo.iNeedCredit > 0)
			{
				this.m_costSlot[0].SetData(1, curShipInfo.iNeedCredit, nkmuserData.GetCredit(), true, true, false);
			}
			Dictionary<int, int>.Enumerator enumerator = curShipInfo.dicMaterialList.GetEnumerator();
			for (int i = 1; i < this.m_costSlot.Length; i++)
			{
				NKCUIItemCostSlot nkcuiitemCostSlot = this.m_costSlot[i];
				bool flag = enumerator.MoveNext();
				NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, flag);
				if (flag)
				{
					NKMInventoryData inventoryData = nkmuserData.m_InventoryData;
					KeyValuePair<int, int> keyValuePair = enumerator.Current;
					long countMiscItem = inventoryData.GetCountMiscItem(keyValuePair.Key);
					NKCUIItemCostSlot nkcuiitemCostSlot2 = nkcuiitemCostSlot;
					keyValuePair = enumerator.Current;
					int key = keyValuePair.Key;
					keyValuePair = enumerator.Current;
					nkcuiitemCostSlot2.SetData(key, keyValuePair.Value, countMiscItem, true, true, false);
				}
				else
				{
					nkcuiitemCostSlot.SetData(0, 0, 0L, true, true, false);
				}
			}
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x0021003E File Offset: 0x0020E23E
		private void ButtonCancel()
		{
			base.Close();
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x00210046 File Offset: 0x0020E246
		private void ButtonOk()
		{
			if (!this.NKM_UI_POPUP_OK.m_bLock)
			{
				if (this.dOnConfirm != null)
				{
					this.dOnConfirm();
				}
				base.Close();
			}
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x00210070 File Offset: 0x0020E270
		private void SetTitleText(NKCUIShipInfoRepair.RepairState state, string shipName = "")
		{
			if (state == NKCUIShipInfoRepair.RepairState.LevelUp)
			{
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TITLE, NKCUtilString.GET_STRING_HANGAR_LVUP);
				if (string.IsNullOrEmpty(shipName))
				{
					NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TEXT, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_LEVEL_UP_TEXT);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TEXT, shipName + NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_DESC_LEVEL_UP);
				}
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_HANGAR_SHIPYARD_ITEM_TITLE_TEXT, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_LEVEL_UP_MISC_TEXT);
				return;
			}
			if (state == NKCUIShipInfoRepair.RepairState.Upgrade)
			{
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TITLE, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_TITLE);
				if (string.IsNullOrEmpty(shipName))
				{
					NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TEXT, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_TEXT);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TEXT, shipName + NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_DESC_UPGRADE);
				}
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_HANGAR_SHIPYARD_ITEM_TITLE_TEXT, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_MISC_TEXT);
				return;
			}
			if (state == NKCUIShipInfoRepair.RepairState.LimitBreak)
			{
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TITLE, NKCUtilString.GET_STRING_SHIP_LIMITBREAK);
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TEXT, NKCUtilString.GET_STRING_SHIP_LIMITBREAK_POPUP_DESC);
				NKCUtil.SetLabelText(this.m_txt_NKM_UI_HANGAR_SHIPYARD_ITEM_TITLE_TEXT, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_MISC_TEXT);
			}
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x0021015B File Offset: 0x0020E35B
		private void Update()
		{
			if (this.m_openAni != null)
			{
				this.m_openAni.Update();
			}
		}

		// Token: 0x040052D0 RID: 21200
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_HANGAR";

		// Token: 0x040052D1 RID: 21201
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_HANGAR_SHIPYARD";

		// Token: 0x040052D2 RID: 21202
		private static NKCUIHangarShipyardPopup m_Instance;

		// Token: 0x040052D3 RID: 21203
		[Header("공용")]
		public NKCUIItemCostSlot[] m_costSlot;

		// Token: 0x040052D4 RID: 21204
		public NKCUIComButton NKM_UI_POPUP_CANCEL;

		// Token: 0x040052D5 RID: 21205
		public NKCUIComButton NKM_UI_POPUP_OK;

		// Token: 0x040052D6 RID: 21206
		public Text m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TITLE;

		// Token: 0x040052D7 RID: 21207
		public Text m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_TEXT;

		// Token: 0x040052D8 RID: 21208
		public Text m_txt_NKM_UI_HANGAR_SHIPYARD_ITEM_TITLE_TEXT;

		// Token: 0x040052D9 RID: 21209
		public GameObject NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_LEVELUP;

		// Token: 0x040052DA RID: 21210
		public GameObject NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_UPGRADE;

		// Token: 0x040052DB RID: 21211
		public GameObject m_objLimitBreak;

		// Token: 0x040052DC RID: 21212
		[Header("함선 레벨업 관련")]
		public Text m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_LevelUp_Perv;

		// Token: 0x040052DD RID: 21213
		public Text m_txt_NKM_UI_POPUP_HANGAR_SHIPYARD_CONTENTS_LevelUp_Next;

		// Token: 0x040052DE RID: 21214
		[Header("레벨업 어빌리티")]
		public Text m_PREV_SHIP_ABILITY_POWER_COUNT;

		// Token: 0x040052DF RID: 21215
		public Text m_PREV_SHIP_ABILITY_ATK_COUNT;

		// Token: 0x040052E0 RID: 21216
		public Text m_PREV_SHIP_ABILITY_HP_COUNT;

		// Token: 0x040052E1 RID: 21217
		public Text m_PREV_SHIP_ABILITY_DEF_COUNT;

		// Token: 0x040052E2 RID: 21218
		public Text m_NEXT_SHIP_ABILITY_POWER_COUNT;

		// Token: 0x040052E3 RID: 21219
		public Text m_NEXT_SHIP_ABILITY_ATK_COUNT;

		// Token: 0x040052E4 RID: 21220
		public Text m_NEXT_SHIP_ABILITY_ATK_COUNT_ADD;

		// Token: 0x040052E5 RID: 21221
		public Text m_NEXT_SHIP_ABILITY_HP_COUNT;

		// Token: 0x040052E6 RID: 21222
		public Text m_NEXT_SHIP_ABILITY_HP_COUNT_ADD;

		// Token: 0x040052E7 RID: 21223
		public Text m_NEXT_SHIP_ABILITY_DEF_COUNT;

		// Token: 0x040052E8 RID: 21224
		public Text m_NEXT_SHIP_ABILITY_DEF_COUNT_ADD;

		// Token: 0x040052E9 RID: 21225
		[Header("함선 개장 관련")]
		public List<GameObject> m_lstCurStar;

		// Token: 0x040052EA RID: 21226
		public List<GameObject> m_lstNextStar;

		// Token: 0x040052EB RID: 21227
		public RectTransform m_NKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT;

		// Token: 0x040052EC RID: 21228
		[Header("개장 스킬 아이콘")]
		public NKCUIShipSkillSlot[] m_PrevSkillSlot;

		// Token: 0x040052ED RID: 21229
		public NKCUIShipSkillSlot[] m_NextSkillSlot;

		// Token: 0x040052EE RID: 21230
		[Header("함선 초월 관련")]
		public Text m_lbPrevLimitBreakLevel;

		// Token: 0x040052EF RID: 21231
		public Text m_lbNextLimitBreakLevel;

		// Token: 0x040052F0 RID: 21232
		public NKCUISlot m_costShipSlot;

		// Token: 0x040052F1 RID: 21233
		public List<GameObject> m_lstModuleBefore = new List<GameObject>();

		// Token: 0x040052F2 RID: 21234
		public List<GameObject> m_lstModuleAfter = new List<GameObject>();

		// Token: 0x040052F3 RID: 21235
		public Text m_lbNewModule;

		// Token: 0x040052F4 RID: 21236
		private NKCUIOpenAnimator m_openAni;

		// Token: 0x040052F5 RID: 21237
		private NKCUISlot.SlotData m_costShipSlotData;

		// Token: 0x040052F6 RID: 21238
		private UnityAction dOnConfirm;

		// Token: 0x040052F7 RID: 21239
		private UnityAction dOnConfirmLevelUp;

		// Token: 0x040052F8 RID: 21240
		private UnityAction dOnConfirmUpgrade;

		// Token: 0x040052F9 RID: 21241
		private UnityAction dOnConfirmLimitBreak;
	}
}
