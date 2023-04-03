using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.User;
using NKC.UI.Component;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C27 RID: 3111
	public class NKCUICollectionShipInfo : NKCUIBase
	{
		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x0600901C RID: 36892 RVA: 0x00310020 File Offset: 0x0030E220
		public static NKCUICollectionShipInfo Instance
		{
			get
			{
				if (NKCUICollectionShipInfo.m_Instance == null)
				{
					NKCUICollectionShipInfo.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCUICollectionShipInfo>("ab_ui_nkm_ui_ship_info", "NKM_UI_SHIP_INFO_COLLECTION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUICollectionShipInfo.CleanupInstance));
					NKCUICollectionShipInfo.m_Instance = NKCUICollectionShipInfo.m_loadedUIData.GetInstance<NKCUICollectionShipInfo>();
					NKCUICollectionShipInfo.m_Instance.Init();
				}
				return NKCUICollectionShipInfo.m_Instance;
			}
		}

		// Token: 0x0600901D RID: 36893 RVA: 0x00310079 File Offset: 0x0030E279
		private static void CleanupInstance()
		{
			NKCUICollectionShipInfo.m_Instance = null;
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x0600901E RID: 36894 RVA: 0x00310081 File Offset: 0x0030E281
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUICollectionShipInfo.m_Instance != null && NKCUICollectionShipInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600901F RID: 36895 RVA: 0x0031009C File Offset: 0x0030E29C
		public static void CheckInstanceAndClose()
		{
			if (NKCUICollectionShipInfo.m_loadedUIData != null)
			{
				NKCUICollectionShipInfo.m_loadedUIData.CloseInstance();
			}
		}

		// Token: 0x06009020 RID: 36896 RVA: 0x003100AF File Offset: 0x0030E2AF
		private void OnDestroy()
		{
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06009021 RID: 36897 RVA: 0x003100B1 File Offset: 0x0030E2B1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06009022 RID: 36898 RVA: 0x003100B4 File Offset: 0x0030E2B4
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_SHIP_INFO;
			}
		}

		// Token: 0x06009023 RID: 36899 RVA: 0x003100BB File Offset: 0x0030E2BB
		public override void CloseInternal()
		{
			this.BannerCleanUp();
			base.gameObject.SetActive(false);
			NKCUIPopupIllustView.CheckInstanceAndClose();
		}

		// Token: 0x06009024 RID: 36900 RVA: 0x003100D4 File Offset: 0x0030E2D4
		public override void OnBackButton()
		{
			if (this.m_bIllustView)
			{
				this.OnClickChangeIllust();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06009025 RID: 36901 RVA: 0x003100EC File Offset: 0x0030E2EC
		private void Init()
		{
			if (this.m_cbtnChangeIllust != null)
			{
				this.m_cbtnChangeIllust.PointerClick.RemoveAllListeners();
				this.m_cbtnChangeIllust.PointerClick.AddListener(new UnityAction(this.OnClickChangeIllust));
			}
			if (this.m_cbtnPractice != null)
			{
				this.m_cbtnPractice.PointerClick.RemoveAllListeners();
				this.m_cbtnPractice.PointerClick.AddListener(new UnityAction(this.OnClickPractice));
			}
			if (null != this.m_GuideBtn)
			{
				this.m_GuideBtn.PointerClick.RemoveAllListeners();
				this.m_GuideBtn.PointerClick.AddListener(delegate()
				{
					NKCUIPopUpGuide.Instance.Open(this.m_GuideStrID, 0);
				});
			}
			if (this.m_ToolTipHP != null)
			{
				this.m_ToolTipHP.SetType(NKM_STAT_TYPE.NST_HP, false);
			}
			if (this.m_ToolTipATK != null)
			{
				this.m_ToolTipATK.SetType(NKM_STAT_TYPE.NST_ATK, false);
			}
			if (this.m_ToolTipDEF != null)
			{
				this.m_ToolTipDEF.SetType(NKM_STAT_TYPE.NST_DEF, false);
			}
			if (this.m_ToolTipCritical != null)
			{
				this.m_ToolTipCritical.SetType(NKM_STAT_TYPE.NST_CRITICAL, false);
			}
			if (this.m_ToolTipHit != null)
			{
				this.m_ToolTipHit.SetType(NKM_STAT_TYPE.NST_HIT, false);
			}
			if (this.m_ToolTipEvade != null)
			{
				this.m_ToolTipEvade.SetType(NKM_STAT_TYPE.NST_EVADE, false);
			}
			if (this.m_tglSocket_01 != null)
			{
				this.m_tglSocket_01.OnValueChanged.RemoveAllListeners();
				this.m_tglSocket_01.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSocket_01));
			}
			if (this.m_tglSocket_02 != null)
			{
				this.m_tglSocket_02.OnValueChanged.RemoveAllListeners();
				this.m_tglSocket_02.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSocket_02));
			}
			if (this.m_tglMoveRange)
			{
				this.m_tglMoveRange.OnValueChanged.RemoveAllListeners();
				this.m_tglMoveRange.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangeMoveRange));
			}
			this.InitDragSelectablePanel();
			this.m_SkillPanel.Init(new UnityAction(this.OpenPopupSkillFullInfoForShip));
			base.gameObject.SetActive(false);
		}

		// Token: 0x06009026 RID: 36902 RVA: 0x00310320 File Offset: 0x0030E520
		public void Open(NKMUnitData shipData, NKMDeckIndex deckIndex, NKCUIUnitInfo.OpenOption openOption = null, List<NKMEquipItemData> listNKMEquipItemData = null, bool isGauntlet = false)
		{
			this.m_bNonePlatoon = (deckIndex == NKMDeckIndex.None);
			this.m_listNKMEquipItemData = listNKMEquipItemData;
			this.m_isGauntlet = isGauntlet;
			this.m_tglMoveRange.Select(false, false, false);
			NKCUtil.SetGameobjectActive(this.m_ShipInfoMoveType, false);
			if (this.m_listNKMEquipItemData != null || this.m_isGauntlet)
			{
				NKCUtil.SetGameobjectActive(this.m_cbtnPractice, false);
			}
			this.SetShipData(shipData, deckIndex);
			base.gameObject.SetActive(true);
			if (openOption == null)
			{
				openOption = new NKCUIUnitInfo.OpenOption(new List<long>(), 0);
				openOption.m_lstUnitData.Add(shipData);
			}
			if (openOption.m_lstUnitData.Count <= 0 && openOption.m_UnitUIDList.Count <= 0)
			{
				Debug.Log("Can not found ship list info");
			}
			this.m_OpenOption = openOption;
			this.m_iBannerSlotCnt = 0;
			this.SetBannerUnit(shipData.m_UnitUID);
			base.UIOpened(true);
		}

		// Token: 0x06009027 RID: 36903 RVA: 0x003103FC File Offset: 0x0030E5FC
		private void OnClickPractice()
		{
			NKM_SHORTCUT_TYPE shortcutType;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				shortcutType = NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().GetCurrentShortcutType();
			}
			else
			{
				shortcutType = NKM_SHORTCUT_TYPE.SHORTCUT_NONE;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMUnitData);
			string shortcutParam = (unitTempletBase != null) ? unitTempletBase.m_UnitStrID : "";
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COLLECTION_TRAINING_MODE_CHANGE_REQ, delegate()
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(this.m_NKMUnitData, shortcutType, shortcutParam);
			}, null, false);
		}

		// Token: 0x06009028 RID: 36904 RVA: 0x00310484 File Offset: 0x0030E684
		private void SetShipData(NKMUnitData shipData, NKMDeckIndex deckIndex)
		{
			this.m_NKMUnitData = shipData;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipData.m_UnitID);
			this.m_deckIndex = deckIndex;
			this.m_uiSummary.SetShipData(shipData, unitTempletBase, deckIndex, false);
			if (this.m_listNKMEquipItemData != null || this.m_isGauntlet)
			{
				this.m_uiSummary.HideDeckNumber();
			}
			NKCUtil.SetGameobjectActive(this.m_tglMoveRange, unitTempletBase != null);
			if (unitTempletBase != null)
			{
				this.m_ShipInfoMoveType.SetData(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
			}
			bool flag = false;
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(shipData.m_UnitID);
			if (unitStatTemplet != null)
			{
				nkmstatData.MakeBaseStat(null, flag, shipData, unitStatTemplet.m_StatData, false, 0, null);
			}
			if (this.m_listNKMEquipItemData != null)
			{
				nkmstatData.MakeBaseBonusFactor(shipData, NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.EquipItems, null, null, flag);
			}
			else
			{
				nkmstatData.MakeBaseBonusFactor(shipData, null, null, null, flag);
			}
			this.m_lbHP.text = string.Format("{0:#;-#;0}", nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HP));
			this.m_lbAttack.text = string.Format("{0:#;-#;0}", nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_ATK));
			this.m_lbDefence.text = string.Format("{0:#;-#;0}", nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_DEF));
			this.m_lbCritical.text = string.Format("{0:#;-#;0}", nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_CRITICAL));
			this.m_lbHit.text = string.Format("{0:#;-#;0}", nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HIT));
			this.m_lbEvade.text = string.Format("{0:#;-#;0}", nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_EVADE));
			this.m_lbPower.text = shipData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null).ToString();
			NKCUtil.SetGameobjectActive(this.m_objEnabledModule, shipData.m_LimitBreakLevel > 0);
			NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(NKMShipManager.GetMaxLevelShipID(shipData.m_UnitID), 1);
			NKCUtil.SetGameobjectActive(this.m_objNoModule, shipLimitBreakTemplet == null);
			NKCUtil.SetGameobjectActive(this.m_objLockedModule, shipData.m_LimitBreakLevel == 0 && shipLimitBreakTemplet != null);
			if (this.m_objEnabledModule != null && this.m_objEnabledModule.activeSelf)
			{
				for (int i = 0; i < this.m_lstModuleStep.Count; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStep[i], i < (int)shipData.m_LimitBreakLevel);
				}
				NKCUtil.SetLabelText(this.m_lbModuleStep, string.Format(NKCUtilString.GET_STRING_SHIP_INFO_MODULE_STEP_TEXT, shipData.ShipCommandModule.Count));
				this.m_tglSocket_01.Select(true, true, true);
				this.ShowTotalSocketOptions(0);
			}
			this.m_SkillPanel.SetData(unitTempletBase);
			this.m_CurShipID = shipData.m_UnitID;
			if (this.m_listNKMEquipItemData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIP_INFO_NOTICE_NOTGET, !NKCUICollectionUnitList.IsHasUnit(NKM_UNIT_TYPE.NUT_SHIP, shipData.m_UnitID));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIP_INFO_NOTICE_NOTGET, false);
		}

		// Token: 0x06009029 RID: 36905 RVA: 0x0031076C File Offset: 0x0030E96C
		private void SetDeckNumber(NKMDeckIndex deckIndex)
		{
			this.m_deckIndex = deckIndex;
			if (this.m_listNKMEquipItemData != null)
			{
				this.m_uiSummary.HideDeckNumber();
				return;
			}
			this.m_uiSummary.SetDeckNumber(deckIndex);
		}

		// Token: 0x0600902A RID: 36906 RVA: 0x00310795 File Offset: 0x0030E995
		public void OnRecv(NKMPacket_DECK_SHIP_SET_ACK sPacket)
		{
			if (sPacket.shipUID == this.m_NKMUnitData.m_UnitUID)
			{
				this.SetDeckNumber(sPacket.deckIndex);
			}
		}

		// Token: 0x0600902B RID: 36907 RVA: 0x003107B6 File Offset: 0x0030E9B6
		private void OnClickChangeIllust()
		{
			NKCUIPopupIllustView.Instance.Open(this.m_NKMUnitData);
		}

		// Token: 0x0600902C RID: 36908 RVA: 0x003107C8 File Offset: 0x0030E9C8
		private void Update()
		{
			if (this.m_bIllustView)
			{
				if (NKCScenManager.GetScenManager().GetHasPinch())
				{
					this.m_srScrollRect.enabled = false;
					this.OnPinchZoom(NKCScenManager.GetScenManager().GetPinchCenter(), NKCScenManager.GetScenManager().GetPinchDeltaMagnitude());
				}
				else
				{
					this.m_srScrollRect.enabled = true;
				}
				float y = Input.mouseScrollDelta.y;
				if (y != 0f)
				{
					this.OnPinchZoom(Input.mousePosition, y);
				}
			}
		}

		// Token: 0x0600902D RID: 36909 RVA: 0x00310844 File Offset: 0x0030EA44
		public void OnPinchZoom(Vector2 PinchCenter, float pinchMagnitude)
		{
			float num = this.m_rectSpineIllustPanel.localScale.x * Mathf.Pow(4f, pinchMagnitude);
			if (num < 0.5f)
			{
				num = 0.5f;
			}
			if (num > 2f)
			{
				num = 2f;
			}
			this.m_rectSpineIllustPanel.localScale = new Vector3(num, num, 1f);
		}

		// Token: 0x0600902E RID: 36910 RVA: 0x003108A1 File Offset: 0x0030EAA1
		private void OpenPopupSkillFullInfoForShip()
		{
			if (this.m_CurShipID == 0)
			{
				return;
			}
			NKCPopupSkillFullInfo.ShipInstance.OpenForShip(this.m_CurShipID, -1L);
		}

		// Token: 0x0600902F RID: 36911 RVA: 0x003108BE File Offset: 0x0030EABE
		private void OnValueChangedSocket_01(bool bValue)
		{
			if (bValue)
			{
				this.ShowTotalSocketOptions(0);
			}
		}

		// Token: 0x06009030 RID: 36912 RVA: 0x003108CA File Offset: 0x0030EACA
		private void OnValueChangedSocket_02(bool bValue)
		{
			if (bValue)
			{
				this.ShowTotalSocketOptions(1);
			}
		}

		// Token: 0x06009031 RID: 36913 RVA: 0x003108D8 File Offset: 0x0030EAD8
		private void ShowTotalSocketOptions(int socketIndex)
		{
			List<NKMShipCmdSlot> list = new List<NKMShipCmdSlot>();
			for (int i = 0; i < this.m_NKMUnitData.ShipCommandModule.Count; i++)
			{
				if (this.m_NKMUnitData.ShipCommandModule[i] != null && this.m_NKMUnitData.ShipCommandModule[i].slots != null)
				{
					for (int j = 0; j < this.m_NKMUnitData.ShipCommandModule[i].slots.Length; j++)
					{
						if (socketIndex == j)
						{
							NKMShipCmdSlot nkmshipCmdSlot = this.m_NKMUnitData.ShipCommandModule[i].slots[j];
							if (nkmshipCmdSlot != null && nkmshipCmdSlot.statType != NKM_STAT_TYPE.NST_RANDOM)
							{
								this.AddSameBuff(ref list, nkmshipCmdSlot);
							}
						}
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int k = 0; k < list.Count; k++)
			{
				stringBuilder.AppendLine(NKCUtilString.GetSlotOptionString(list[k], "[{0}] {1}"));
			}
			NKCUtil.SetLabelText(this.m_lbSocketOptions, stringBuilder.ToString());
			this.m_srSocketOptions.normalizedPosition = Vector2.zero;
		}

		// Token: 0x06009032 RID: 36914 RVA: 0x003109E8 File Offset: 0x0030EBE8
		private void AddSameBuff(ref List<NKMShipCmdSlot> lstSocket, NKMShipCmdSlot targetSocket)
		{
			bool flag = false;
			for (int i = 0; i < lstSocket.Count; i++)
			{
				NKMShipCmdSlot nkmshipCmdSlot = lstSocket[i];
				if (nkmshipCmdSlot.statType == targetSocket.statType && nkmshipCmdSlot.targetStyleType.SetEquals(targetSocket.targetStyleType) && nkmshipCmdSlot.targetRoleType.SetEquals(targetSocket.targetRoleType))
				{
					flag = true;
					nkmshipCmdSlot.statValue += targetSocket.statValue;
					nkmshipCmdSlot.statFactor += targetSocket.statFactor;
					break;
				}
			}
			if (!flag)
			{
				NKMShipCmdSlot item = new NKMShipCmdSlot(targetSocket.targetStyleType, targetSocket.targetRoleType, targetSocket.statType, targetSocket.statValue, targetSocket.statFactor, targetSocket.isLock);
				lstSocket.Add(item);
			}
		}

		// Token: 0x06009033 RID: 36915 RVA: 0x00310AA8 File Offset: 0x0030ECA8
		private void InitDragSelectablePanel()
		{
			if (this.m_DragUnitView != null)
			{
				this.m_DragUnitView.Init(true, true);
				this.m_DragUnitView.dOnGetObject += this.MakeMainBannerListSlot;
				this.m_DragUnitView.dOnReturnObject += new NKCUIComDragSelectablePanel.OnReturnObject(this.ReturnMainBannerListSlot);
				this.m_DragUnitView.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideMainBannerListSlotData);
				this.m_DragUnitView.dOnIndexChangeListener += this.SelectCharacter;
				this.m_DragUnitView.dOnFocus += this.Focus;
				this.m_iBannerSlotCnt = 0;
			}
		}

		// Token: 0x06009034 RID: 36916 RVA: 0x00310B50 File Offset: 0x0030ED50
		private void SetBannerUnit(long unitUID)
		{
			if (this.m_DragUnitView != null)
			{
				if (this.m_OpenOption.m_lstUnitData.Count <= 0)
				{
					if (this.m_OpenOption.m_UnitUIDList.Count <= 0 && unitUID != 0L)
					{
						this.m_OpenOption.m_UnitUIDList.Add(unitUID);
					}
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData != null)
					{
						NKMArmyData armyData = nkmuserData.m_ArmyData;
						if (armyData != null)
						{
							for (int i = 0; i < this.m_OpenOption.m_UnitUIDList.Count; i++)
							{
								NKMUnitData shipFromUID = armyData.GetShipFromUID(this.m_OpenOption.m_UnitUIDList[i]);
								if (shipFromUID != null)
								{
									this.m_OpenOption.m_lstUnitData.Add(shipFromUID);
								}
							}
						}
					}
				}
				for (int j = 0; j < this.m_OpenOption.m_lstUnitData.Count; j++)
				{
					if (this.m_OpenOption.m_lstUnitData[j].m_UnitUID == unitUID)
					{
						this.m_DragUnitView.TotalCount = this.m_OpenOption.m_lstUnitData.Count;
						this.m_DragUnitView.SetIndex(j);
						return;
					}
				}
			}
		}

		// Token: 0x06009035 RID: 36917 RVA: 0x00310C68 File Offset: 0x0030EE68
		private RectTransform MakeMainBannerListSlot()
		{
			GameObject gameObject = new GameObject(string.Format("Banner{0}", this.m_iBannerSlotCnt), new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			LayoutElement component = gameObject.GetComponent<LayoutElement>();
			component.ignoreLayout = false;
			component.preferredWidth = this.m_DragUnitView.m_rtContentRect.GetWidth();
			component.preferredHeight = this.m_DragUnitView.m_rtContentRect.GetHeight();
			component.flexibleWidth = 2f;
			component.flexibleHeight = 2f;
			this.m_iBannerSlotCnt++;
			return gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06009036 RID: 36918 RVA: 0x00310D10 File Offset: 0x0030EF10
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_OpenOption != null && this.m_OpenOption.m_lstUnitData != null)
			{
				NKMUnitData nkmunitData = this.m_OpenOption.m_lstUnitData[idx];
				if (nkmunitData != null && tr != null)
				{
					string name = tr.gameObject.name;
					string s = name.Substring(name.Length - 1);
					int key = 0;
					int.TryParse(s, out key);
					if (!this.m_dicUnitIllust.ContainsKey(key))
					{
						NKCASUIUnitIllust nkcasuiunitIllust = NKCResourceUtility.OpenSpineIllust(nkmunitData, false);
						if (nkcasuiunitIllust != null)
						{
							RectTransform rectTransform = nkcasuiunitIllust.GetRectTransform();
							if (rectTransform != null)
							{
								rectTransform.localScale = new Vector3(-1f, rectTransform.localScale.y, rectTransform.localScale.z);
							}
							nkcasuiunitIllust.SetParent(tr.transform, false);
							nkcasuiunitIllust.SetAnchoredPosition(Vector2.zero);
							nkcasuiunitIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
						}
						this.m_dicUnitIllust.Add(key, nkcasuiunitIllust);
						return;
					}
					if (this.m_dicUnitIllust[key] != null)
					{
						this.m_dicUnitIllust[key].Unload();
						this.m_dicUnitIllust[key] = null;
						this.m_dicUnitIllust[key] = NKCResourceUtility.OpenSpineIllust(nkmunitData, false);
						if (this.m_dicUnitIllust[key] != null)
						{
							RectTransform rectTransform2 = this.m_dicUnitIllust[key].GetRectTransform();
							if (rectTransform2 != null)
							{
								rectTransform2.localScale = new Vector3(-1f, rectTransform2.localScale.y, rectTransform2.localScale.z);
							}
							this.m_dicUnitIllust[key].SetParent(tr.transform, false);
							this.m_dicUnitIllust[key].SetAnchoredPosition(Vector2.zero);
							this.m_dicUnitIllust[key].SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06009037 RID: 36919 RVA: 0x00310EE9 File Offset: 0x0030F0E9
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06009038 RID: 36920 RVA: 0x00310EFD File Offset: 0x0030F0FD
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x06009039 RID: 36921 RVA: 0x00310F0C File Offset: 0x0030F10C
		public void SelectCharacter(int idx)
		{
			if (this.m_OpenOption.m_lstUnitData.Count < idx || idx < 0)
			{
				Debug.LogWarning(string.Format("Error - Count : {0}, Index : {1}", this.m_OpenOption.m_lstUnitData.Count, idx));
				return;
			}
			NKMUnitData nkmunitData = this.m_OpenOption.m_lstUnitData[idx];
			if (nkmunitData != null)
			{
				this.ChangeUnit(nkmunitData);
			}
		}

		// Token: 0x0600903A RID: 36922 RVA: 0x00310F78 File Offset: 0x0030F178
		private void BannerCleanUp()
		{
			foreach (KeyValuePair<int, NKCASUIUnitIllust> keyValuePair in this.m_dicUnitIllust)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.Unload();
				}
			}
			this.m_dicUnitIllust.Clear();
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x0600903B RID: 36923 RVA: 0x00310FE4 File Offset: 0x0030F1E4
		private NKMArmyData NKMArmyData
		{
			get
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null)
				{
					return null;
				}
				return nkmuserData.m_ArmyData;
			}
		}

		// Token: 0x0600903C RID: 36924 RVA: 0x00310FF8 File Offset: 0x0030F1F8
		private void ChangeUnit(NKMUnitData cNKMUnitData)
		{
			NKMDeckIndex shipDeckIndex = this.NKMArmyData.GetShipDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, cNKMUnitData.m_UnitUID);
			this.SetShipData(cNKMUnitData, this.m_bNonePlatoon ? NKMDeckIndex.None : shipDeckIndex);
		}

		// Token: 0x0600903D RID: 36925 RVA: 0x0031102F File Offset: 0x0030F22F
		private void OnChangeMoveRange(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_ShipInfoMoveType, bValue);
		}

		// Token: 0x0600903E RID: 36926 RVA: 0x00311040 File Offset: 0x0030F240
		private Sprite GetSpriteMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string stringMoveType = this.GetStringMoveType(type);
			if (string.IsNullOrEmpty(stringMoveType))
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_SHIP_INFO_TEXTURE", stringMoveType, false);
		}

		// Token: 0x0600903F RID: 36927 RVA: 0x0031106C File Offset: 0x0030F26C
		private string GetStringMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string result = string.Empty;
			switch (type)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_1";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_4";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_2";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_3";
				break;
			}
			return result;
		}

		// Token: 0x04007D1F RID: 32031
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_ship_info";

		// Token: 0x04007D20 RID: 32032
		private const string UI_ASSET_NAME = "NKM_UI_SHIP_INFO_COLLECTION";

		// Token: 0x04007D21 RID: 32033
		private static NKCUICollectionShipInfo m_Instance;

		// Token: 0x04007D22 RID: 32034
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04007D23 RID: 32035
		[Header("왼쪽 위 함선 기본정보")]
		public NKCUIShipInfoSummary m_uiSummary;

		// Token: 0x04007D24 RID: 32036
		[Header("오른쪽 함선 정보")]
		public Text m_lbPower;

		// Token: 0x04007D25 RID: 32037
		public Text m_lbHP;

		// Token: 0x04007D26 RID: 32038
		public Text m_lbAttack;

		// Token: 0x04007D27 RID: 32039
		public Text m_lbDefence;

		// Token: 0x04007D28 RID: 32040
		public Text m_lbCritical;

		// Token: 0x04007D29 RID: 32041
		public Text m_lbHit;

		// Token: 0x04007D2A RID: 32042
		public Text m_lbEvade;

		// Token: 0x04007D2B RID: 32043
		public NKCUIShipInfoSkillPanel m_SkillPanel;

		// Token: 0x04007D2C RID: 32044
		public GameObject m_objNoModule;

		// Token: 0x04007D2D RID: 32045
		public GameObject m_objLockedModule;

		// Token: 0x04007D2E RID: 32046
		public GameObject m_objEnabledModule;

		// Token: 0x04007D2F RID: 32047
		public List<GameObject> m_lstModuleStep = new List<GameObject>();

		// Token: 0x04007D30 RID: 32048
		public Text m_lbModuleStep;

		// Token: 0x04007D31 RID: 32049
		public NKCUIComToggle m_tglSocket_01;

		// Token: 0x04007D32 RID: 32050
		public NKCUIComToggle m_tglSocket_02;

		// Token: 0x04007D33 RID: 32051
		public ScrollRect m_srSocketOptions;

		// Token: 0x04007D34 RID: 32052
		public Text m_lbSocketOptions;

		// Token: 0x04007D35 RID: 32053
		[Header("UI State 관련")]
		public RectTransform m_rtLeftRect;

		// Token: 0x04007D36 RID: 32054
		public RectTransform m_rtRightRect;

		// Token: 0x04007D37 RID: 32055
		public NKCUIRectMove m_rmLock;

		// Token: 0x04007D38 RID: 32056
		public Animator m_Ani_NKM_UI_SHIP_INFO_COLLECTION;

		// Token: 0x04007D39 RID: 32057
		[Header("스파인 일러스트")]
		public ScrollRect m_srScrollRect;

		// Token: 0x04007D3A RID: 32058
		public RectTransform m_rectSpineIllustPanel;

		// Token: 0x04007D3B RID: 32059
		public RectTransform m_rectIllustRoot;

		// Token: 0x04007D3C RID: 32060
		public Vector2 m_vIllustRootAnchorMinNormal;

		// Token: 0x04007D3D RID: 32061
		public Vector2 m_vIllustRootAnchorMaxNormal;

		// Token: 0x04007D3E RID: 32062
		public Vector2 m_vIllustRootAnchorMinIllustView;

		// Token: 0x04007D3F RID: 32063
		public Vector2 m_vIllustRootAnchorMaxIllustView;

		// Token: 0x04007D40 RID: 32064
		[Header("기타 버튼")]
		public NKCUIComStateButton m_cbtnChangeIllust;

		// Token: 0x04007D41 RID: 32065
		public NKCUIComStateButton m_cbtnPractice;

		// Token: 0x04007D42 RID: 32066
		public NKCUIComToggle m_tglMoveRange;

		// Token: 0x04007D43 RID: 32067
		public NKCUIShipInfoMoveType m_ShipInfoMoveType;

		// Token: 0x04007D44 RID: 32068
		public NKCUIComStateButton m_GuideBtn;

		// Token: 0x04007D45 RID: 32069
		public string m_GuideStrID;

		// Token: 0x04007D46 RID: 32070
		public NKCComStatInfoToolTip m_ToolTipHP;

		// Token: 0x04007D47 RID: 32071
		public NKCComStatInfoToolTip m_ToolTipATK;

		// Token: 0x04007D48 RID: 32072
		public NKCComStatInfoToolTip m_ToolTipDEF;

		// Token: 0x04007D49 RID: 32073
		public NKCComStatInfoToolTip m_ToolTipCritical;

		// Token: 0x04007D4A RID: 32074
		public NKCComStatInfoToolTip m_ToolTipHit;

		// Token: 0x04007D4B RID: 32075
		public NKCComStatInfoToolTip m_ToolTipEvade;

		// Token: 0x04007D4C RID: 32076
		[Header("유닛 획득 표시")]
		public GameObject m_NKM_UI_SHIP_INFO_NOTICE_NOTGET;

		// Token: 0x04007D4D RID: 32077
		private NKMDeckIndex m_deckIndex;

		// Token: 0x04007D4E RID: 32078
		private NKMUnitData m_NKMUnitData;

		// Token: 0x04007D4F RID: 32079
		private List<NKMEquipItemData> m_listNKMEquipItemData;

		// Token: 0x04007D50 RID: 32080
		private bool m_isGauntlet;

		// Token: 0x04007D51 RID: 32081
		private bool m_bNonePlatoon;

		// Token: 0x04007D52 RID: 32082
		private bool m_bIllustView;

		// Token: 0x04007D53 RID: 32083
		private const float MIN_ZOOM_SCALE = 0.5f;

		// Token: 0x04007D54 RID: 32084
		private const float MAX_ZOOM_SCALE = 2f;

		// Token: 0x04007D55 RID: 32085
		private int m_CurShipID;

		// Token: 0x04007D56 RID: 32086
		[Header("함선 변경")]
		public NKCUIComDragSelectablePanel m_DragUnitView;

		// Token: 0x04007D57 RID: 32087
		private Dictionary<int, NKCASUIUnitIllust> m_dicUnitIllust = new Dictionary<int, NKCASUIUnitIllust>();

		// Token: 0x04007D58 RID: 32088
		private int m_iBannerSlotCnt;

		// Token: 0x04007D59 RID: 32089
		private NKCUIUnitInfo.OpenOption m_OpenOption;
	}
}
