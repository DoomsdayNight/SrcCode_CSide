using System;
using System.Collections.Generic;
using ClientPacket.Community;
using ClientPacket.Warfare;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Warfare
{
	// Token: 0x02000B01 RID: 2817
	public class NKCPopupWarfareSelectShip : NKCUIBase
	{
		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06008014 RID: 32788 RVA: 0x002B2151 File Offset: 0x002B0351
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06008015 RID: 32789 RVA: 0x002B2154 File Offset: 0x002B0354
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_WARFARE_SELECT_SHIP_POPUP;
			}
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x002B215C File Offset: 0x002B035C
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstNKCDeckViewUnitSlot[i];
				if (nkcdeckViewUnitSlot != null)
				{
					nkcdeckViewUnitSlot.Init(i, true);
				}
			}
			this.m_NKM_UI_POPUP_MENU1_ON_Btn.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_POPUP_MENU1_ON_Btn.PointerClick.AddListener(new UnityAction(this.OnSetFlagShipButton_));
			this.m_NKM_UI_POPUP_MENU2_Btn.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_POPUP_MENU2_Btn.PointerClick.AddListener(new UnityAction(this.OnCancelBatchButton_));
			this.m_NKM_UI_POPUP_MENU3_Btn.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_POPUP_MENU3_Btn.PointerClick.AddListener(new UnityAction(this.OnCloseBtn));
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_MENU3_Btn, HotkeyEventType.Confirm);
			this.m_NKM_UI_POPUP_MENU4_Btn.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_POPUP_MENU4_Btn.PointerClick.AddListener(new UnityAction(this.OnDeckViewBtn_));
			this.m_NKM_UI_POPUP_MENU1_OFF.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_POPUP_MENU1_OFF.PointerClick.AddListener(new UnityAction(this.OnSetFlagShipButton_));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				this.OnCloseBtn();
			});
			this.m_NKM_UI_POPUP_BG.triggers.Add(entry);
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = EventTriggerType.PointerClick;
			entry2.callback.AddListener(delegate(BaseEventData e)
			{
				this.OnCloseBtn();
			});
			this.m_NKM_UI_POPUP_CANCEL.triggers.Add(entry2);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x002B230C File Offset: 0x002B050C
		public void OpenForMyShipInDive(NKMDeckIndex sNKMDeckIndex)
		{
			this.m_sNKMDeckIndex = sNKMDeckIndex;
			this.m_dOnSetFlagShipButton = null;
			this.m_dOnCancelBatchButton = null;
			this.m_dOnDeckViewBtn = null;
			base.gameObject.SetActive(true);
			this.SetUIByDataInDive(true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.SetBattleEnvironment("");
			base.UIOpened(true);
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x002B2368 File Offset: 0x002B0568
		public void OpenForMyShipInWarfare(NKMDeckIndex sNKMDeckIndex, int gameUnitUID, int shipUnitID)
		{
			this.m_sNKMDeckIndex = sNKMDeckIndex;
			this.m_WarfareGameUnitUID = gameUnitUID;
			this.m_ShipUnitID = shipUnitID;
			this.m_dOnSetFlagShipButton = null;
			this.m_dOnCancelBatchButton = null;
			this.m_dOnDeckViewBtn = null;
			base.gameObject.SetActive(true);
			this.SetUIByDataInWarfare(NKCPopupWarfareSelectShip.ShipType.player, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.SetBattleEnvironment("");
			base.UIOpened(true);
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x002B23D0 File Offset: 0x002B05D0
		public void OpenForMyShipInWarfare(NKMDeckIndex sNKMDeckIndex, int gameUnitUID, int shipUnitID, NKCPopupWarfareSelectShip.OnSetFlagShipButton _dOnSetFlagShipButton, NKCPopupWarfareSelectShip.OnCancelBatchButton _dOnCancelBatchButton, NKCPopupWarfareSelectShip.OnDeckViewBtn _dOnDeckViewBtn)
		{
			this.m_sNKMDeckIndex = sNKMDeckIndex;
			this.m_WarfareGameUnitUID = gameUnitUID;
			this.m_ShipUnitID = shipUnitID;
			this.m_dOnSetFlagShipButton = _dOnSetFlagShipButton;
			this.m_dOnCancelBatchButton = _dOnCancelBatchButton;
			this.m_dOnDeckViewBtn = _dOnDeckViewBtn;
			base.gameObject.SetActive(true);
			this.SetUIByDataInWarfare(NKCPopupWarfareSelectShip.ShipType.player, false);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.SetBattleEnvironment("");
			base.UIOpened(true);
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x002B243C File Offset: 0x002B063C
		public void OpenForSupporterInWarfare(WarfareSupporterListData friendData, int gameUnitUID, NKCPopupWarfareSelectShip.OnCancelBatchButton _dOnCancelBatchButton = null)
		{
			this.m_friendData = friendData;
			this.m_WarfareGameUnitUID = gameUnitUID;
			this.m_dOnSetFlagShipButton = null;
			this.m_dOnCancelBatchButton = _dOnCancelBatchButton;
			this.m_dOnDeckViewBtn = null;
			base.gameObject.SetActive(true);
			this.SetUIByDataInWarfare(NKCPopupWarfareSelectShip.ShipType.supporter, _dOnCancelBatchButton == null);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.SetBattleEnvironment("");
			base.UIOpened(true);
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x002B24A0 File Offset: 0x002B06A0
		public void OpenForEnemy(int dungeonID, string battleConditionStrID = "")
		{
			this.m_DungeonID = dungeonID;
			base.gameObject.SetActive(true);
			this.SetUIByDataInWarfare(NKCPopupWarfareSelectShip.ShipType.dungeon, false);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.m_dOnSetFlagShipButton = null;
			this.m_dOnCancelBatchButton = null;
			this.m_dOnDeckViewBtn = null;
			this.SetBattleEnvironment(battleConditionStrID);
			base.UIOpened(true);
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x002B24F8 File Offset: 0x002B06F8
		private void SetBattleEnvironment(string battleConditionStrID = "")
		{
			if (!string.IsNullOrEmpty(battleConditionStrID))
			{
				NKMBattleConditionTemplet cNKMBattleConditionTemplet = NKMBattleConditionManager.GetTempletByStrID(battleConditionStrID);
				if (cNKMBattleConditionTemplet != null)
				{
					NKCUtil.SetImageSprite(this.NKM_UI_OPERATION_POPUP_SCENARIO_BC_ICON, NKCUtil.GetSpriteBattleConditionICon(cNKMBattleConditionTemplet), false);
					NKCUIComStateButton component = this.NKM_UI_OPERATION_POPUP_SCENARIO_BC_ICON.gameObject.GetComponent<NKCUIComStateButton>();
					if (component != null)
					{
						component.PointerDown.RemoveAllListeners();
						component.PointerDown.AddListener(delegate(PointerEventData e)
						{
							NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, cNKMBattleConditionTemplet.BattleCondName_Translated, cNKMBattleConditionTemplet.BattleCondDesc_Translated, new Vector2?(e.position));
						});
					}
				}
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_BC, cNKMBattleConditionTemplet.BattleCondID != 0);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_BC, false);
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x002B25A3 File Offset: 0x002B07A3
		private void SetShipNameText(string name)
		{
			if (this.m_NKM_UI_POPUP_INFO_TEXT != null)
			{
				this.m_NKM_UI_POPUP_INFO_TEXT.text = name;
			}
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x002B25BF File Offset: 0x002B07BF
		private void SetShipTypeText(string name)
		{
			if (this.m_NKM_UI_POPUP_INFO_TEXT_2 != null)
			{
				this.m_NKM_UI_POPUP_INFO_TEXT_2.text = name;
			}
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x002B25DC File Offset: 0x002B07DC
		private void SetDeckViewUnitSlotCount(int count)
		{
			while (this.m_lstDeckViewUnitSlot.Count < count)
			{
				NKCDeckViewUnitSlot newInstance = NKCDeckViewUnitSlot.GetNewInstance(this.m_ENEMY_DECK_AREA_Content.transform);
				if (!(newInstance == null))
				{
					newInstance.Init(this.m_lstDeckViewUnitSlot.Count, false);
					newInstance.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
					this.m_lstDeckViewUnitSlot.Add(newInstance);
				}
			}
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x002B2658 File Offset: 0x002B0858
		public void SetUIByDataInDive(bool bMyShip)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_TOP_INFO, bMyShip);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_TOP_INFO_ENEMY, !bMyShip);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHIP_INFO, bMyShip);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHIP_INFO_ENEMY, !bMyShip);
			NKCUtil.SetGameobjectActive(this.m_UNIT_DECK_AREA, bMyShip);
			NKCUtil.SetGameobjectActive(this.m_ENEMY_DECK_AREA_ScrollView, !bMyShip);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_MENU1_ON, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_MENU2, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_MENU4_Btn.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objSquadPower, bMyShip);
			if (bMyShip)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(this.m_sNKMDeckIndex);
				if (deckData != null)
				{
					this.m_NKM_UI_POPUP_TOP_TEXT.text = string.Format(NKCUtilString.GET_STRING_SQUAD_ONE_PARAM, NKCUtilString.GetDeckNumberString(this.m_sNKMDeckIndex));
					int num = (int)(this.m_sNKMDeckIndex.m_iIndex + 1);
					this.m_NKM_UI_POPUP_TOP_TEXT2.text = string.Format(NKCUtilString.GET_STRING_SQUAD_TWO_PARAM, num, NKCUtilString.GetRankNumber(num, false).ToUpper());
					for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
					{
						if (i < 8)
						{
							this.m_lstNKCDeckViewUnitSlot[i].SetData(myUserData.m_ArmyData.GetUnitFromUID(deckData.m_listDeckUnitUID[i]), false);
							if (i == (int)deckData.m_LeaderIndex)
							{
								this.m_lstNKCDeckViewUnitSlot[i].SetLeader(true, false);
							}
						}
					}
					NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
					NKMUnitTempletBase nkmunitTempletBase = null;
					if (shipFromUID != null)
					{
						nkmunitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
					}
					NKCUtil.SetGameobjectActive(this.m_objOperator, !NKCOperatorUtil.IsHide());
					if (deckData.m_OperatorUID != 0L)
					{
						this.m_Operator.SetData(NKCOperatorUtil.GetOperatorData(deckData.m_OperatorUID), false);
					}
					else if (NKCOperatorUtil.IsHide())
					{
						this.m_Operator.SetHide();
					}
					else
					{
						this.m_Operator.SetEmpty();
					}
					if (nkmunitTempletBase != null)
					{
						this.SetShipNameText(nkmunitTempletBase.GetUnitName());
						this.SetShipTypeText(nkmunitTempletBase.GetUnitTitle());
						Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, nkmunitTempletBase);
						Sprite sprite2 = null;
						if (sprite != null)
						{
							sprite2 = sprite;
						}
						if (sprite2 == null)
						{
							NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
							if (assetResourceUnitInvenIconEmpty != null)
							{
								this.m_ImgUnit.sprite = assetResourceUnitInvenIconEmpty.GetAsset<Sprite>();
							}
							else
							{
								this.m_ImgUnit.sprite = null;
							}
						}
						else
						{
							this.m_ImgUnit.sprite = sprite2;
						}
					}
					int armyAvarageOperationPower = myUserData.m_ArmyData.GetArmyAvarageOperationPower(this.m_sNKMDeckIndex, false, null, null);
					NKCUtil.SetLabelText(this.m_lbAvgPower, armyAvarageOperationPower.ToString());
					NKCUtil.SetLabelText(this.m_lbCost, string.Format("{0:0.00}", myUserData.m_ArmyData.CalculateDeckAvgSummonCost(this.m_sNKMDeckIndex, null, null)));
					this.m_UIShipInfo.SetShipData(shipFromUID, nkmunitTempletBase, this.m_sNKMDeckIndex, false);
					NKCUtil.SetLabelText(this.m_txtShipPower, (shipFromUID != null) ? shipFromUID.CalculateOperationPower(myUserData.m_InventoryData, 0, null, null).ToString() : "");
				}
				else
				{
					this.m_NKM_UI_POPUP_TOP_TEXT.text = "";
					for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
					{
						if (i < 8)
						{
							this.m_lstNKCDeckViewUnitSlot[i].SetData(null, false);
						}
					}
					this.m_UIShipInfo.SetShipData(null, null, NKMDeckIndex.None, false);
					NKCUtil.SetLabelText(this.m_txtShipPower, "");
				}
				NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
				if (diveGameData != null)
				{
					NKMDiveSquad squad = diveGameData.Player.GetSquad((int)this.m_sNKMDeckIndex.m_iIndex);
					if (squad != null)
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_1, squad.Supply >= 1);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_2, squad.Supply >= 2);
						float num2 = squad.CurHp / squad.MaxHp;
						num2 = this.GetProperRatioValue(num2);
						if (num2 > 0.6f)
						{
							this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
							this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(1f, 1f, 1f);
							this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(this.GetProperRatioValue((num2 - 0.6f) / 0.4f), 1f, 1f);
							return;
						}
						if (num2 > 0.3f)
						{
							this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
							this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(this.GetProperRatioValue((num2 - 0.3f) / 0.3f), 1f, 1f);
							this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(0f, 1f, 1f);
							return;
						}
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(this.GetProperRatioValue(num2 / 0.3f), 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(0f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x002B2BC0 File Offset: 0x002B0DC0
		private float GetProperRatioValue(float fRatio)
		{
			if (fRatio < 0f)
			{
				fRatio = 0f;
			}
			if (fRatio > 1f)
			{
				fRatio = 1f;
			}
			return fRatio;
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x002B2BE4 File Offset: 0x002B0DE4
		private void SetUIByDataInWarfare(NKCPopupWarfareSelectShip.ShipType shipType, bool bPlaying = false)
		{
			bool flag = shipType == NKCPopupWarfareSelectShip.ShipType.player;
			bool flag2 = shipType != NKCPopupWarfareSelectShip.ShipType.dungeon;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_TOP_INFO, flag2);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_TOP_INFO_ENEMY, !flag2);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHIP_INFO, flag2);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHIP_INFO_ENEMY, !flag2);
			NKCUtil.SetGameobjectActive(this.m_UNIT_DECK_AREA, flag2);
			NKCUtil.SetGameobjectActive(this.m_ENEMY_DECK_AREA_ScrollView, !flag2);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_MENU1_ON, flag && !bPlaying);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_MENU2, flag2 && !bPlaying);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_MENU4_Btn.gameObject, flag && !bPlaying);
			NKCUtil.SetGameobjectActive(this.m_objSquadPower, flag2);
			NKCUtil.SetGameobjectActive(this.m_objFriendRoot, shipType == NKCPopupWarfareSelectShip.ShipType.supporter);
			NKCUtil.SetGameobjectActive(this.m_objOperator, !NKCOperatorUtil.IsHide() && shipType == NKCPopupWarfareSelectShip.ShipType.player);
			if (shipType == NKCPopupWarfareSelectShip.ShipType.player)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_ShipUnitID);
				if (unitTempletBase != null)
				{
					this.SetShipNameText(unitTempletBase.GetUnitName());
					this.SetShipTypeText(unitTempletBase.GetUnitTitle());
					Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
					Sprite sprite2 = null;
					if (sprite != null)
					{
						sprite2 = sprite;
					}
					if (sprite2 == null)
					{
						NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
						if (assetResourceUnitInvenIconEmpty != null)
						{
							this.m_ImgUnit.sprite = assetResourceUnitInvenIconEmpty.GetAsset<Sprite>();
						}
						else
						{
							this.m_ImgUnit.sprite = null;
						}
					}
					else
					{
						this.m_ImgUnit.sprite = sprite2;
					}
				}
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(this.m_sNKMDeckIndex);
				if (deckData != null)
				{
					this.m_NKM_UI_POPUP_TOP_TEXT.text = string.Format(NKCUtilString.GET_STRING_SQUAD_ONE_PARAM, NKCUtilString.GetDeckNumberString(this.m_sNKMDeckIndex));
					int num = (int)(this.m_sNKMDeckIndex.m_iIndex + 1);
					this.m_NKM_UI_POPUP_TOP_TEXT2.text = string.Format(NKCUtilString.GET_STRING_SQUAD_TWO_PARAM, num, NKCUtilString.GetRankNumber(num, false).ToUpper());
					for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
					{
						if (i < 8)
						{
							this.m_lstNKCDeckViewUnitSlot[i].SetData(myUserData.m_ArmyData.GetUnitFromUID(deckData.m_listDeckUnitUID[i]), false);
							if (i == (int)deckData.m_LeaderIndex)
							{
								this.m_lstNKCDeckViewUnitSlot[i].SetLeader(true, false);
							}
						}
					}
					NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
					NKMUnitTempletBase shipTempletBase = null;
					if (shipFromUID != null)
					{
						shipTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
					}
					NKCUtil.SetGameobjectActive(this.m_objOperator, !NKCOperatorUtil.IsHide());
					if (deckData.m_OperatorUID != 0L)
					{
						this.m_Operator.SetData(NKCOperatorUtil.GetOperatorData(deckData.m_OperatorUID), false);
					}
					else if (NKCOperatorUtil.IsHide())
					{
						this.m_Operator.SetHide();
					}
					else
					{
						this.m_Operator.SetEmpty();
					}
					int armyAvarageOperationPower = myUserData.m_ArmyData.GetArmyAvarageOperationPower(this.m_sNKMDeckIndex, false, null, null);
					NKCUtil.SetLabelText(this.m_lbAvgPower, armyAvarageOperationPower.ToString());
					NKCUtil.SetLabelText(this.m_lbCost, string.Format("{0:0.00}", myUserData.m_ArmyData.CalculateDeckAvgSummonCost(this.m_sNKMDeckIndex, null, null)));
					this.m_UIShipInfo.SetShipData(shipFromUID, shipTempletBase, this.m_sNKMDeckIndex, false);
					NKCUtil.SetLabelText(this.m_txtShipPower, (shipFromUID != null) ? shipFromUID.CalculateOperationPower(myUserData.m_InventoryData, 0, null, null).ToString() : "");
				}
				else
				{
					this.m_NKM_UI_POPUP_TOP_TEXT.text = "";
					for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
					{
						if (i < 8)
						{
							this.m_lstNKCDeckViewUnitSlot[i].SetData(null, false);
						}
					}
					this.m_UIShipInfo.SetShipData(null, null, NKMDeckIndex.None, false);
					NKCUtil.SetLabelText(this.m_txtShipPower, "");
				}
				if (!bPlaying)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_1, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_2, true);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(1f, 1f, 1f);
					return;
				}
				WarfareUnitData unitData = NKCScenManager.GetScenManager().WarfareGameData.GetUnitData(this.m_WarfareGameUnitUID);
				if (unitData != null)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_1, unitData.supply >= 1);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_2, unitData.supply >= 2);
					float num2 = unitData.hp / unitData.hpMax;
					num2 = this.GetProperRatioValue(num2);
					if (num2 > 0.6f)
					{
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(1f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(this.GetProperRatioValue((num2 - 0.6f) / 0.4f), 1f, 1f);
						return;
					}
					if (num2 > 0.3f)
					{
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(this.GetProperRatioValue((num2 - 0.3f) / 0.3f), 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(0f, 1f, 1f);
						return;
					}
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(this.GetProperRatioValue(num2 / 0.3f), 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(0f, 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(0f, 1f, 1f);
					return;
				}
			}
			else if (shipType == NKCPopupWarfareSelectShip.ShipType.supporter)
			{
				this.m_NKM_UI_POPUP_TOP_TEXT.text = this.m_friendData.commonProfile.nickname;
				this.m_NKM_UI_POPUP_TOP_TEXT2.text = "";
				this.m_txtFriendCode.text = NKCUtilString.GetFriendCode(this.m_friendData.commonProfile.friendCode);
				this.m_txtFriendDesc.text = this.m_friendData.message;
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.m_friendData.deckData.GetShipUnitId());
				if (unitTempletBase2 != null)
				{
					this.SetShipNameText(unitTempletBase2.GetUnitName());
					this.SetShipTypeText(unitTempletBase2.GetUnitTitle());
					Sprite sprite3 = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase2);
					if (sprite3 == null)
					{
						NKCAssetResourceData assetResourceUnitInvenIconEmpty2 = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
						if (assetResourceUnitInvenIconEmpty2 != null)
						{
							this.m_ImgUnit.sprite = assetResourceUnitInvenIconEmpty2.GetAsset<Sprite>();
						}
						else
						{
							this.m_ImgUnit.sprite = null;
						}
					}
					else
					{
						this.m_ImgUnit.sprite = sprite3;
					}
				}
				NKMUnitData nkmunitData = new NKMUnitData();
				nkmunitData.FillDataFromDummy(this.m_friendData.deckData.Ship);
				nkmunitData.m_UnitID = this.m_friendData.deckData.GetShipUnitId();
				this.m_UIShipInfo.SetShipData(nkmunitData, unitTempletBase2, false);
				NKCUtil.SetLabelText(this.m_txtShipPower, nkmunitData.CalculateOperationPower(null, 0, null, null).ToString());
				NKCUtil.SetLabelText(this.m_lbAvgPower, this.m_friendData.deckData.CalculateOperationPower().ToString());
				NKCUtil.SetLabelText(this.m_lbCost, string.Format("{0:0.00}", this.m_friendData.deckData.CalculateSummonCost()));
				for (int j = 0; j < this.m_lstNKCDeckViewUnitSlot.Count; j++)
				{
					if (j < 8 && j < this.m_friendData.deckData.List.Length)
					{
						NKMDummyUnitData nkmdummyUnitData = this.m_friendData.deckData.List[j];
						if (nkmdummyUnitData == null)
						{
							this.m_lstNKCDeckViewUnitSlot[j].SetData(null, false);
						}
						else
						{
							NKMUnitData nkmunitData2 = new NKMUnitData();
							nkmunitData2.FillDataFromDummy(nkmdummyUnitData);
							this.m_lstNKCDeckViewUnitSlot[j].SetData(nkmunitData2, false);
							if ((int)this.m_friendData.deckData.LeaderIndex == j)
							{
								this.m_lstNKCDeckViewUnitSlot[j].SetLeader(true, false);
							}
						}
					}
				}
				if (!bPlaying)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_1, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_2, true);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(1f, 1f, 1f);
					return;
				}
				WarfareUnitData unitData2 = NKCScenManager.GetScenManager().WarfareGameData.GetUnitData(this.m_WarfareGameUnitUID);
				if (unitData2 != null)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_1, unitData2.supply >= 1);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_INFO_BATTLEPOINT_2, unitData2.supply >= 2);
					float num3 = unitData2.hp / unitData2.hpMax;
					num3 = this.GetProperRatioValue(num3);
					if (num3 > 0.6f)
					{
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(1f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(this.GetProperRatioValue((num3 - 0.6f) / 0.4f), 1f, 1f);
						return;
					}
					if (num3 > 0.3f)
					{
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(1f, 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(this.GetProperRatioValue((num3 - 0.3f) / 0.3f), 1f, 1f);
						this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(0f, 1f, 1f);
						return;
					}
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1.transform.localScale = new Vector3(this.GetProperRatioValue(num3 / 0.3f), 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2.transform.localScale = new Vector3(0f, 1f, 1f);
					this.m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3.transform.localScale = new Vector3(0f, 1f, 1f);
					return;
				}
			}
			else if (shipType == NKCPopupWarfareSelectShip.ShipType.dungeon)
			{
				this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT2.text = NKCUtilString.GET_STRING_WARFARE_POPUP_ENEMY_INFO_KILL;
				this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT3.text = "";
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_DungeonID);
				if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase != null)
				{
					Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(dungeonTemplet.m_DungeonTempletBase);
					this.SetDeckViewUnitSlotCount(enemyUnits.Count);
					List<NKCEnemyData> list = new List<NKCEnemyData>(enemyUnits.Values);
					list.Sort(new NKCEnemyData.CompNED());
					int k;
					for (k = 0; k < list.Count; k++)
					{
						NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstDeckViewUnitSlot[k];
						nkcdeckViewUnitSlot.SetEnemyData(NKMUnitManager.GetUnitTempletBase(list[k].m_UnitStrID), list[k]);
						NKCUtil.SetGameobjectActive(nkcdeckViewUnitSlot.gameObject, true);
					}
					while (k < this.m_lstDeckViewUnitSlot.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstDeckViewUnitSlot[k], false);
						k++;
					}
					if (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
					{
						this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT2.text = NKCUtilString.GET_STRING_WARFARE_POPUP_ENEMY_INFO_WAVE;
						this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT3.text = string.Format(NKCUtilString.GET_STRING_WARFARE_POPUP_ENEMY_INFO_WAVE_ONE_PARAM, dungeonTemplet.m_listDungeonWave.Count);
					}
					this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT8.text = NKCUtilString.GetDGMissionText(DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR, 0);
					DUNGEON_GAME_MISSION_TYPE dgmissionType_ = dungeonTemplet.m_DungeonTempletBase.m_DGMissionType_1;
					DUNGEON_GAME_MISSION_TYPE dgmissionType_2 = dungeonTemplet.m_DungeonTempletBase.m_DGMissionType_2;
					int dgmissionValue_ = dungeonTemplet.m_DungeonTempletBase.m_DGMissionValue_1;
					int dgmissionValue_2 = dungeonTemplet.m_DungeonTempletBase.m_DGMissionValue_2;
					this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT7.text = NKCUtilString.GetDGMissionText(dgmissionType_, dgmissionValue_);
					this.m_NKM_UI_POPUP_ENEMY_INFO_TEXT6.text = NKCUtilString.GetDGMissionText(dgmissionType_2, dgmissionValue_2);
				}
			}
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x002B38A2 File Offset: 0x002B1AA2
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x002B38B7 File Offset: 0x002B1AB7
		public void CloseWarfareSelectShipPopup()
		{
			base.Close();
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x002B38BF File Offset: 0x002B1ABF
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x002B38C7 File Offset: 0x002B1AC7
		public void OnSetFlagShipButton_()
		{
			base.Close();
			if (this.m_dOnSetFlagShipButton != null)
			{
				this.m_dOnSetFlagShipButton(this.m_WarfareGameUnitUID);
			}
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x002B38E8 File Offset: 0x002B1AE8
		public void OnCancelBatchButton_()
		{
			base.Close();
			if (this.m_dOnCancelBatchButton != null)
			{
				this.m_dOnCancelBatchButton(this.m_WarfareGameUnitUID);
			}
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x002B3909 File Offset: 0x002B1B09
		public void OnDeckViewBtn_()
		{
			base.Close();
			if (this.m_dOnDeckViewBtn != null)
			{
				this.m_dOnDeckViewBtn(this.m_WarfareGameUnitUID);
			}
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x002B392A File Offset: 0x002B1B2A
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x002B3938 File Offset: 0x002B1B38
		private void OnDestroy()
		{
			for (int i = 0; i < this.m_lstDeckViewUnitSlot.Count; i++)
			{
				this.m_lstDeckViewUnitSlot[i].CloseInstance();
			}
			this.m_lstDeckViewUnitSlot.Clear();
		}

		// Token: 0x04006C39 RID: 27705
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_WARFARE_SELECT";

		// Token: 0x04006C3A RID: 27706
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_WARFARE_SELECT_SHIP";

		// Token: 0x04006C3B RID: 27707
		public GameObject m_NKM_UI_POPUP_TOP_INFO;

		// Token: 0x04006C3C RID: 27708
		public GameObject m_NKM_UI_POPUP_TOP_INFO_ENEMY;

		// Token: 0x04006C3D RID: 27709
		public GameObject m_NKM_UI_POPUP_SHIP_INFO;

		// Token: 0x04006C3E RID: 27710
		public GameObject m_NKM_UI_POPUP_SHIP_INFO_ENEMY;

		// Token: 0x04006C3F RID: 27711
		public GameObject m_UNIT_DECK_AREA;

		// Token: 0x04006C40 RID: 27712
		public GameObject m_ENEMY_DECK_AREA_ScrollView;

		// Token: 0x04006C41 RID: 27713
		public GameObject m_NKM_UI_POPUP_MENU1_ON;

		// Token: 0x04006C42 RID: 27714
		public GameObject m_NKM_UI_POPUP_MENU2;

		// Token: 0x04006C43 RID: 27715
		public Image m_ImgUnit;

		// Token: 0x04006C44 RID: 27716
		public Text m_NKM_UI_POPUP_INFO_TEXT;

		// Token: 0x04006C45 RID: 27717
		public Text m_NKM_UI_POPUP_INFO_TEXT_2;

		// Token: 0x04006C46 RID: 27718
		public Text m_NKM_UI_POPUP_TOP_TEXT;

		// Token: 0x04006C47 RID: 27719
		public Text m_NKM_UI_POPUP_TOP_TEXT2;

		// Token: 0x04006C48 RID: 27720
		public Text m_NKM_UI_POPUP_ENEMY_INFO_TEXT2;

		// Token: 0x04006C49 RID: 27721
		public Text m_NKM_UI_POPUP_ENEMY_INFO_TEXT3;

		// Token: 0x04006C4A RID: 27722
		public Text m_NKM_UI_POPUP_ENEMY_INFO_TEXT6;

		// Token: 0x04006C4B RID: 27723
		public Text m_NKM_UI_POPUP_ENEMY_INFO_TEXT7;

		// Token: 0x04006C4C RID: 27724
		public Text m_NKM_UI_POPUP_ENEMY_INFO_TEXT8;

		// Token: 0x04006C4D RID: 27725
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlot;

		// Token: 0x04006C4E RID: 27726
		public NKCUIShipInfoSummary m_UIShipInfo;

		// Token: 0x04006C4F RID: 27727
		public Text m_txtShipPower;

		// Token: 0x04006C50 RID: 27728
		public GameObject m_ENEMY_DECK_AREA_Content;

		// Token: 0x04006C51 RID: 27729
		public GameObject m_NKM_UI_POPUP_INFO_BATTLEPOINT_1;

		// Token: 0x04006C52 RID: 27730
		public GameObject m_NKM_UI_POPUP_INFO_BATTLEPOINT_2;

		// Token: 0x04006C53 RID: 27731
		public Image m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_1;

		// Token: 0x04006C54 RID: 27732
		public Image m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_2;

		// Token: 0x04006C55 RID: 27733
		public Image m_NKM_UI_POPUP_INFO_ICON_HP_GAUGE_3;

		// Token: 0x04006C56 RID: 27734
		public GameObject m_objSquadPower;

		// Token: 0x04006C57 RID: 27735
		public Text m_lbAvgPower;

		// Token: 0x04006C58 RID: 27736
		public Text m_lbCost;

		// Token: 0x04006C59 RID: 27737
		[Header("Button")]
		public NKCUIComButton m_NKM_UI_POPUP_MENU1_ON_Btn;

		// Token: 0x04006C5A RID: 27738
		public NKCUIComButton m_NKM_UI_POPUP_MENU2_Btn;

		// Token: 0x04006C5B RID: 27739
		public NKCUIComButton m_NKM_UI_POPUP_MENU3_Btn;

		// Token: 0x04006C5C RID: 27740
		public NKCUIComButton m_NKM_UI_POPUP_MENU4_Btn;

		// Token: 0x04006C5D RID: 27741
		public NKCUIComButton m_NKM_UI_POPUP_MENU1_OFF;

		// Token: 0x04006C5E RID: 27742
		public EventTrigger m_NKM_UI_POPUP_BG;

		// Token: 0x04006C5F RID: 27743
		public EventTrigger m_NKM_UI_POPUP_CANCEL;

		// Token: 0x04006C60 RID: 27744
		[Header("Friend")]
		public GameObject m_objFriendRoot;

		// Token: 0x04006C61 RID: 27745
		public Text m_txtFriendCode;

		// Token: 0x04006C62 RID: 27746
		public Text m_txtFriendDesc;

		// Token: 0x04006C63 RID: 27747
		[Header("Cheat")]
		public EventTrigger m_etDungeonClearRewardCheat;

		// Token: 0x04006C64 RID: 27748
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006C65 RID: 27749
		private NKCPopupWarfareSelectShip.OnSetFlagShipButton m_dOnSetFlagShipButton;

		// Token: 0x04006C66 RID: 27750
		private NKCPopupWarfareSelectShip.OnCancelBatchButton m_dOnCancelBatchButton;

		// Token: 0x04006C67 RID: 27751
		private NKCPopupWarfareSelectShip.OnDeckViewBtn m_dOnDeckViewBtn;

		// Token: 0x04006C68 RID: 27752
		private NKMDeckIndex m_sNKMDeckIndex;

		// Token: 0x04006C69 RID: 27753
		private int m_WarfareGameUnitUID;

		// Token: 0x04006C6A RID: 27754
		private int m_ShipUnitID;

		// Token: 0x04006C6B RID: 27755
		private WarfareSupporterListData m_friendData;

		// Token: 0x04006C6C RID: 27756
		private int m_DungeonID;

		// Token: 0x04006C6D RID: 27757
		private List<NKCDeckViewUnitSlot> m_lstDeckViewUnitSlot = new List<NKCDeckViewUnitSlot>();

		// Token: 0x04006C6E RID: 27758
		[Header("전투 환경")]
		public GameObject m_NKM_UI_OPERATION_BC;

		// Token: 0x04006C6F RID: 27759
		public Image NKM_UI_OPERATION_POPUP_SCENARIO_BC_ICON;

		// Token: 0x04006C70 RID: 27760
		[Header("오퍼레이터")]
		public GameObject m_objOperator;

		// Token: 0x04006C71 RID: 27761
		public NKCUIOperatorDeckSlot m_Operator;

		// Token: 0x02001897 RID: 6295
		private enum ShipType
		{
			// Token: 0x0400A94B RID: 43339
			player,
			// Token: 0x0400A94C RID: 43340
			dungeon,
			// Token: 0x0400A94D RID: 43341
			supporter
		}

		// Token: 0x02001898 RID: 6296
		// (Invoke) Token: 0x0600B64C RID: 46668
		public delegate void OnSetFlagShipButton(int gameUnitUID);

		// Token: 0x02001899 RID: 6297
		// (Invoke) Token: 0x0600B650 RID: 46672
		public delegate void OnCancelBatchButton(int gameUnitUID);

		// Token: 0x0200189A RID: 6298
		// (Invoke) Token: 0x0600B654 RID: 46676
		public delegate void OnDeckViewBtn(int gameUnitUID);
	}
}
