using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClientPacket.User;
using Cs.Protocol;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Component;
using NKC.UI.Shop;
using NKC.Util;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000978 RID: 2424
	public class NKCUIChangeLobby : NKCUIBase, IDragHandler, IEventSystemHandler
	{
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06006236 RID: 25142 RVA: 0x001ECE40 File Offset: 0x001EB040
		public static NKCUIChangeLobby Instance
		{
			get
			{
				if (NKCUIChangeLobby.m_Instance == null)
				{
					NKCUIChangeLobby.m_Instance = NKCUIManager.OpenNewInstance<NKCUIChangeLobby>("ab_ui_nkm_ui_user_info", "NKM_UI_USER_INFO_LOBBY_CHANGE_RENEWAL", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIChangeLobby.CleanupInstance)).GetInstance<NKCUIChangeLobby>();
					NKCUIChangeLobby.m_Instance.Init();
				}
				return NKCUIChangeLobby.m_Instance;
			}
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x001ECE8F File Offset: 0x001EB08F
		private static void CleanupInstance()
		{
			NKCUIChangeLobby.m_Instance = null;
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x001ECE97 File Offset: 0x001EB097
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIChangeLobby.m_Instance != null && NKCUIChangeLobby.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x001ECEB2 File Offset: 0x001EB0B2
		public static void CheckInstanceAndClose()
		{
			if (NKCUIChangeLobby.m_Instance != null && NKCUIChangeLobby.m_Instance.IsOpen)
			{
				NKCUIChangeLobby.m_Instance.Close();
			}
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x001ECED7 File Offset: 0x001EB0D7
		private void OnDestroy()
		{
			NKCUIChangeLobby.m_Instance = null;
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x001ECEDF File Offset: 0x001EB0DF
		private NKCUICharacterView GetCharView(int index)
		{
			if (this.m_lstCvUnit == null)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			if (index >= this.m_lstCvUnit.Count)
			{
				return null;
			}
			return this.m_lstCvUnit[index];
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x0600623C RID: 25148 RVA: 0x001ECF0D File Offset: 0x001EB10D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x001ECF10 File Offset: 0x001EB110
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600623E RID: 25150 RVA: 0x001ECF13 File Offset: 0x001EB113
		public override string MenuName
		{
			get
			{
				return "change lobby";
			}
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x001ECF1C File Offset: 0x001EB11C
		public override void CloseInternal()
		{
			this.CleanUp();
			if (this.m_trRoot != null)
			{
				this.m_trRoot.SetParent(base.transform);
				this.m_trRoot.localPosition = Vector3.zero;
			}
			NKCUtil.SetGameobjectActive(this.m_trRoot, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x001ECF78 File Offset: 0x001EB178
		public override void OnBackButton()
		{
			if (this.IsPreviewMode)
			{
				this.SetPreview(false);
				return;
			}
			if (this.m_bOpenSelectBackground)
			{
				this.CloseSelectBackground();
				return;
			}
			if (this.m_LobbyFace.IsOpen)
			{
				this.m_LobbyFace.Close();
				return;
			}
			base.OnBackButton();
			NKCSoundManager.PlayScenMusic();
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x001ECFC8 File Offset: 0x001EB1C8
		public override void UnHide()
		{
			this.UpdateBackgroundMusic(this.m_currentBackgroundInfo, false);
			this.UpdateUnitIllust(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			base.UnHide();
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x001ECFF0 File Offset: 0x001EB1F0
		private void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_btnChangeUnit, new UnityAction(this.OnChangeUnit));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglUnitBG, new UnityAction<bool>(this.OnTglUnitBG));
			NKCUtil.SetButtonClickDelegate(this.m_BtnSkinOption, new UnityAction(this.OnSkinOption));
			NKCUtil.SetButtonClickDelegate(this.m_btnSkinChange, new UnityAction(this.OnSkinChange));
			for (int i = 0; i < this.m_lstTglUnit.Count; i++)
			{
				if (!(this.m_lstTglUnit[i] == null))
				{
					this.m_lstTglUnit[i].m_DataInt = i;
					this.m_lstTglUnit[i].OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnSelectUnit);
				}
			}
			for (int j = 0; j < this.m_lstTglPreset.Count; j++)
			{
				if (!(this.m_lstTglPreset[j] == null))
				{
					this.m_lstTglPreset[j].m_DataInt = j;
					this.m_lstTglPreset[j].OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnTglPreset);
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLoadPreset, new UnityAction(this.OnBtnLoadPreset));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSavePreset, new UnityAction(this.OnBtnSavePreset));
			NKCUtil.SetButtonClickDelegate(this.m_btnBackground, new UnityAction(this.OpenSelectBackground));
			NKCUtil.SetButtonClickDelegate(this.m_btnClose, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_btnApplyToLobby, new UnityAction(this.OnConfirm));
			NKCUtil.SetButtonClickDelegate(this.m_btnJukeBox, new UnityAction(this.OnOpenJukeBox));
			NKCUtil.SetButtonClickDelegate(this.m_btnBackClose, new UnityAction(this.CloseSelectBackground));
			NKCUtil.SetButtonClickDelegate(this.m_btnEmotion, new UnityAction(this.ToggleEmotionMenu));
			NKCUtil.SetButtonClickDelegate(this.m_btnResetPosition, new UnityAction(this.ResetPosition));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPreview, new UnityAction(this.OnBtnPreview));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglMusicContinue, new UnityAction<bool>(this.OnClickBGMContinue));
			for (int k = 0; k < this.m_lstCvUnit.Count; k++)
			{
				NKCUICharacterView nkcuicharacterView = this.m_lstCvUnit[k];
				if (nkcuicharacterView != null)
				{
					nkcuicharacterView.Init(new NKCUICharacterView.OnDragEvent(this.OnDragUnit), new NKCUICharacterView.OnTouchEvent(this.OnTouchUnit));
				}
			}
			NKCUIChangeLobbyFace lobbyFace = this.m_LobbyFace;
			if (lobbyFace != null)
			{
				lobbyFace.Init(new NKCUIChangeLobbyFace.OnSelectFace(this.OnSelectFace));
			}
			NKCUtil.SetScrollHotKey(this.m_ScrollRect, null);
			if (this.m_comDraggableList != null)
			{
				this.m_comDraggableList.dOnSlotSwapped = new NKCUIComDraggableList.OnSlotSwapped(this.OnSwap);
			}
		}

		// Token: 0x06006243 RID: 25155 RVA: 0x001ED29C File Offset: 0x001EB49C
		public void Open(NKMUserData userData)
		{
			NKMInventoryData inventoryData = userData.m_InventoryData;
			foreach (NKCBackgroundTemplet nkcbackgroundTemplet in NKMTempletContainer<NKCBackgroundTemplet>.Values)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(nkcbackgroundTemplet.m_ItemMiscID);
				if (nkmitemMiscTemplet != null)
				{
					NKM_ITEM_MISC_TYPE itemMiscType = nkmitemMiscTemplet.m_ItemMiscType;
					if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_BACKGROUND)
					{
						if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
						{
							continue;
						}
						NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(nkcbackgroundTemplet.m_ItemMiscID);
						if (nkmofficeInteriorTemplet == null || nkmofficeInteriorTemplet.Target != InteriorTarget.Background || userData.OfficeData.GetInteriorCount(nkcbackgroundTemplet.m_ItemMiscID) <= 0L)
						{
							continue;
						}
					}
					else if (nkcbackgroundTemplet.m_ItemMiscID != 9001 && inventoryData.GetItemMisc(nkcbackgroundTemplet.m_ItemMiscID) == null)
					{
						continue;
					}
					this.m_listBGTemplet.Add(nkcbackgroundTemplet);
				}
			}
			this.SetBackgroundInfo(this.MakeNKMBackgroundInfo(userData.backGroundInfo), false);
			this.SelectUnit(0, false);
			this.UpdatePresetButtons();
			this.m_currentPresetIndex = -1;
			for (int i = 0; i < this.m_lstTglPreset.Count; i++)
			{
				NKCUIComToggle nkcuicomToggle = this.m_lstTglPreset[i];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(false, true, false);
				}
			}
			NKCUIComStateButton csbtnSavePreset = this.m_csbtnSavePreset;
			if (csbtnSavePreset != null)
			{
				csbtnSavePreset.Lock(false);
			}
			NKCUIComStateButton csbtnLoadPreset = this.m_csbtnLoadPreset;
			if (csbtnLoadPreset != null)
			{
				csbtnLoadPreset.Lock(false);
			}
			base.UIOpened(true);
			if (this.m_comDraggableList != null)
			{
				this.m_comDraggableList.ResetPosition(false);
			}
			if (this.m_trRoot != null)
			{
				this.m_trRoot.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
				this.m_trRoot.localPosition = Vector3.zero;
			}
			NKCUtil.SetGameobjectActive(this.m_trRoot, true);
			this.SetPreview(false);
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x001ED454 File Offset: 0x001EB654
		private NKMBackgroundInfo MakeNKMBackgroundInfo(NKMBackgroundInfo source)
		{
			NKMBackgroundInfo nkmbackgroundInfo = new NKMBackgroundInfo();
			nkmbackgroundInfo.DeepCopyFrom(source);
			if (nkmbackgroundInfo.unitInfoList != null)
			{
				while (nkmbackgroundInfo.unitInfoList.Count < 6)
				{
					NKMBackgroundUnitInfo nkmbackgroundUnitInfo = new NKMBackgroundUnitInfo();
					nkmbackgroundUnitInfo.unitUid = 0L;
					nkmbackgroundUnitInfo.unitFace = 0;
					nkmbackgroundUnitInfo.unitPosX = 0f;
					nkmbackgroundUnitInfo.unitPosY = 0f;
					nkmbackgroundUnitInfo.unitSize = 1f;
					nkmbackgroundUnitInfo.backImage = true;
					nkmbackgroundUnitInfo.skinOption = 0;
					nkmbackgroundUnitInfo.unitType = NKM_UNIT_TYPE.NUT_NORMAL;
					nkmbackgroundInfo.unitInfoList.Add(nkmbackgroundUnitInfo);
				}
			}
			return nkmbackgroundInfo;
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x001ED4E0 File Offset: 0x001EB6E0
		private void UpdateUnitList()
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			for (int i = 0; i < this.m_lstTglUnit.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstTglUnit[i], i < 6);
				NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(i, this.m_currentBackgroundInfo);
				string titleText = "-";
				if (bgunitInfo != null && bgunitInfo.unitUid != 0L)
				{
					NKM_UNIT_TYPE unitType = bgunitInfo.unitType;
					if (unitType - NKM_UNIT_TYPE.NUT_NORMAL > 1)
					{
						if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(armyData.GetOperatorFromUId(bgunitInfo.unitUid));
							if (unitTempletBase != null)
							{
								titleText = NKCStringTable.GetString(unitTempletBase.Name, false);
							}
						}
					}
					else
					{
						NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(armyData.GetUnitOrShipFromUID(bgunitInfo.unitUid));
						if (unitTempletBase2 != null)
						{
							titleText = NKCStringTable.GetString(unitTempletBase2.Name, false);
						}
					}
				}
				this.m_lstTglUnit[i].SetTitleText(titleText);
				this.m_lstTglUnit[i].Select(this.m_currentUnitIndex == i, true, true);
			}
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x001ED5D8 File Offset: 0x001EB7D8
		private void SelectUnit(int index, bool bPlaySelectAnim)
		{
			if (index < 0)
			{
				index = 0;
			}
			this.m_currentUnitIndex = index;
			if (this.m_currentUnitIndex < this.m_lstTglUnit.Count)
			{
				NKCUIComToggle nkcuicomToggle = this.m_lstTglUnit[this.m_currentUnitIndex];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(true, true, true);
				}
			}
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(index, this.m_currentBackgroundInfo);
			NKCUICharacterView charView = this.GetCharView(index);
			if (bPlaySelectAnim && charView != null)
			{
				DOTween.CompleteAll(false);
				charView.SetColor(this.m_SelectStartColor, false);
				float scale = (bgunitInfo.unitSize > 1f) ? (bgunitInfo.unitSize * this.m_SelectScale) : (bgunitInfo.unitSize + (this.m_SelectScale - 1f));
				charView.SetScale(scale);
				DOTween.To(new DOGetter<Color>(charView.GetColor), delegate(Color col)
				{
					charView.SetColor(col, false);
				}, Color.white, this.m_SelectTime).SetEase(this.m_Ease);
				DOTween.To(new DOGetter<float>(charView.GetScale), delegate(float val)
				{
					charView.SetScale(val);
				}, bgunitInfo.unitSize, this.m_SelectTime).SetEase(this.m_Ease);
			}
			this.m_btnEmotion.SetLock(false, false);
			this.m_tglUnitBG.Select(this.GetUnitSkinBGEnabled(index), false, false);
			this.RefreshSkinOptionMenu();
			this.RefreshEmotionMenu();
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x001ED754 File Offset: 0x001EB954
		private void CleanUp()
		{
			DOTween.CompleteAll(false);
			for (int i = 0; i < this.m_lstCvUnit.Count; i++)
			{
				NKCUICharacterView nkcuicharacterView = this.m_lstCvUnit[i];
				if (nkcuicharacterView != null)
				{
					nkcuicharacterView.CleanUp();
				}
			}
			this.m_listBGTemplet.Clear();
			if (this.m_objBG != null)
			{
				UnityEngine.Object.Destroy(this.m_objBG);
			}
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x001ED7B9 File Offset: 0x001EB9B9
		public override void OnCloseInstance()
		{
			if (this.m_trRoot != null)
			{
				UnityEngine.Object.Destroy(this.m_trRoot.gameObject);
			}
			base.OnCloseInstance();
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x001ED7DF File Offset: 0x001EB9DF
		private void OnConfirm()
		{
			NKCPacketSender.Send_NKMPacket_BACKGROUND_CHANGE_REQ(this.m_currentBackgroundInfo);
			NKCScenManager.CurrentUserData().BackgroundBGMContinue = this.m_bBGMContinue;
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x001ED7FC File Offset: 0x001EB9FC
		private void OnOpenJukeBox()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_JUKEBOX_CONTENTS_UNLOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			int selectedMusicID = (this.m_currentBackgroundInfo != null) ? this.m_currentBackgroundInfo.backgroundBgmId : 0;
			NKCUIJukeBox.Instance.Open(true, selectedMusicID, new NKCUIJukeBox.OnMusicSelected(this.OnConfirmChangeBGMInfo));
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x001ED857 File Offset: 0x001EBA57
		public void OnConfirmChangeBGMInfo(int bgmID)
		{
			if (NKCUIJukeBox.IsInstanceOpen)
			{
				NKCUIJukeBox.Instance.Close();
			}
			this.m_currentBackgroundInfo.backgroundBgmId = bgmID;
			this.UpdateBackgroundMusic(this.m_currentBackgroundInfo, false);
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x001ED883 File Offset: 0x001EBA83
		private void OnChangeUnit()
		{
			this.OpenChangeUnit(this.m_currentUnitIndex);
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x001ED891 File Offset: 0x001EBA91
		private void OnSelectUnit(bool value, int index)
		{
			if (value)
			{
				this.SelectUnit(index, true);
			}
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x001ED8A0 File Offset: 0x001EBAA0
		private void OpenChangeUnit(int index)
		{
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.bDescending = true;
			options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			options.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
			{
				NKCUnitSortSystem.eSortOption.Rarity_High
			};
			options.bPushBackUnselectable = false;
			options.bShowRemoveSlot = true;
			options.bShowHideDeckedUnitMenu = false;
			options.bHideDeckedUnit = false;
			options.bCanSelectUnitInMission = true;
			options.m_SortOptions.bUseLobbyState = false;
			options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_TARGET_TO_SELECT;
			options.strUpsideMenuName = NKCUtilString.GET_STRING_LOBBY_BG_SELECT_CAPTAIN;
			options.m_SortOptions.AdditionalUnitStateFunc = new NKCUnitSortSystem.UnitListOptions.CustomUnitStateFunc(this.GiveLobbyState);
			options.bShowUnitShipChangeMenu = true;
			options.m_bUseFavorite = true;
			options.m_SortOptions.bIgnoreCityState = true;
			options.m_SortOptions.bIgnoreWorldMapLeader = true;
			options.m_SortOptions.bIgnoreMissionState = true;
			options.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			options.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			options.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			options.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			options.setOperatorFilterCategory = NKCPopupFilterOperator.MakeDefaultFilterCategory(NKCPopupFilterOperator.FILTER_OPEN_TYPE.NORMAL);
			options.setOperatorSortCategory = NKCOperatorSortSystem.setDefaultOperatorSortCategory;
			options.eUpsideMenuMode = NKCUIUpsideMenu.eMode.BackButtonOnly;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.ChangeUnit), null, null, null, null);
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x001ED9E8 File Offset: 0x001EBBE8
		private NKCUnitSortSystem.eUnitState GiveLobbyState(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return NKCUnitSortSystem.eUnitState.NONE;
			}
			for (int i = 0; i < 6; i++)
			{
				NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(i, this.m_currentBackgroundInfo);
				if (bgunitInfo != null && bgunitInfo.unitUid == unitData.m_UnitUID)
				{
					return NKCUnitSortSystem.eUnitState.LOBBY_UNIT;
				}
			}
			return NKCUnitSortSystem.eUnitState.NONE;
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x001EDA2C File Offset: 0x001EBC2C
		private void OnSwap(int oldIndex, int newIndex)
		{
			if (oldIndex < 0 || newIndex < 0)
			{
				return;
			}
			if (this.m_currentBackgroundInfo == null)
			{
				return;
			}
			if (this.m_currentBackgroundInfo.unitInfoList == null)
			{
				return;
			}
			if (this.m_currentBackgroundInfo.unitInfoList.Count <= oldIndex || this.m_currentBackgroundInfo.unitInfoList.Count <= newIndex)
			{
				return;
			}
			NKMBackgroundUnitInfo nkmbackgroundUnitInfo = this.m_currentBackgroundInfo.unitInfoList[oldIndex];
			NKMBackgroundUnitInfo nkmbackgroundUnitInfo2 = this.m_currentBackgroundInfo.unitInfoList[newIndex];
			float num;
			Vector2 vector = this.CalculateNewPosition(oldIndex, newIndex, nkmbackgroundUnitInfo.unitPosX, nkmbackgroundUnitInfo.unitPosY, out num);
			float num2;
			Vector2 vector2 = this.CalculateNewPosition(newIndex, oldIndex, nkmbackgroundUnitInfo2.unitPosX, nkmbackgroundUnitInfo2.unitPosY, out num2);
			nkmbackgroundUnitInfo.unitPosX = vector.x;
			nkmbackgroundUnitInfo.unitPosY = vector.y;
			nkmbackgroundUnitInfo.unitSize *= num;
			nkmbackgroundUnitInfo2.unitPosX = vector2.x;
			nkmbackgroundUnitInfo2.unitPosY = vector2.y;
			nkmbackgroundUnitInfo2.unitSize *= num2;
			this.m_currentBackgroundInfo.unitInfoList[oldIndex] = nkmbackgroundUnitInfo2;
			this.m_currentBackgroundInfo.unitInfoList[newIndex] = nkmbackgroundUnitInfo;
			this.UpdateUnitIllust(oldIndex, this.m_currentBackgroundInfo);
			this.UpdateUnitIllust(newIndex, this.m_currentBackgroundInfo);
			this.UpdateUnitList();
			this.RefreshSkinOptionMenu();
			this.RefreshEmotionMenu();
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x001EDB74 File Offset: 0x001EBD74
		private Vector2 CalculateNewPosition(int oldIndex, int newIndex, float posX, float posY, out float scaleRatio)
		{
			scaleRatio = 1f;
			Vector2 vector = new Vector2(posX, posY);
			if (oldIndex == newIndex)
			{
				return vector;
			}
			NKCUICharacterView charView = this.GetCharView(oldIndex);
			NKCUICharacterView charView2 = this.GetCharView(newIndex);
			if (charView == null || charView2 == null)
			{
				return vector;
			}
			Vector3 vector2 = charView.OffsetToWorldPos(vector);
			Vector3 vector3 = charView2.OffsetToWorldPos(Vector2.zero);
			float z = NKCCamera.GetCamera().transform.position.z;
			scaleRatio = (z - vector3.z) / (z - vector2.z);
			Vector3 v = vector2 * scaleRatio;
			return charView2.WorldPosToOffset(v);
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x001EDC18 File Offset: 0x001EBE18
		private void ChangeUnit(List<long> listUnitUID)
		{
			if (listUnitUID.Count != 1)
			{
				Debug.LogError("Fatal Error : UnitSelectList returned wrong list");
				return;
			}
			long num = listUnitUID[0];
			NKCUIUnitSelectList.CheckInstanceAndClose();
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			if (bgunitInfo != null && bgunitInfo.unitUid == num)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				if (i != this.m_currentUnitIndex)
				{
					NKMBackgroundUnitInfo bgunitInfo2 = this.GetBGUnitInfo(i, this.m_currentBackgroundInfo);
					if (bgunitInfo2 != null && bgunitInfo2.unitUid != 0L && bgunitInfo2.unitUid == num)
					{
						flag = true;
						bgunitInfo2.unitUid = bgunitInfo.unitUid;
						bgunitInfo2.unitType = bgunitInfo.unitType;
						bgunitInfo2.backImage = bgunitInfo.backImage;
						bgunitInfo2.skinOption = bgunitInfo.skinOption;
						bgunitInfo2.unitFace = bgunitInfo.unitFace;
						float unitSize = bgunitInfo2.unitSize;
						bgunitInfo2.unitSize = bgunitInfo.unitSize;
						bgunitInfo.unitSize = unitSize;
						this.UpdateUnitIllust(i, this.m_currentBackgroundInfo);
						break;
					}
				}
			}
			bgunitInfo.unitUid = num;
			bgunitInfo.unitType = this.GetUnitType(num);
			bgunitInfo.unitFace = 0;
			this.UpdateUnitIllust(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			if (!flag)
			{
				NKM_UNIT_TYPE unitType = bgunitInfo.unitType;
				if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
				{
					if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
					{
						NKMOperator operatorFromUId = NKCScenManager.CurrentUserData().m_ArmyData.GetOperatorFromUId(num);
						if (operatorFromUId != null)
						{
							NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_CAPTAIN, operatorFromUId, false, true);
						}
					}
				}
				else
				{
					NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(num);
					if (unitFromUID != null)
					{
						NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_CAPTAIN, unitFromUID, false, false);
					}
				}
			}
			this.m_btnEmotion.SetLock(false, false);
			this.UpdateUnitList();
			this.RefreshSkinOptionMenu();
			this.RefreshEmotionMenu();
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x001EDDC8 File Offset: 0x001EBFC8
		private NKM_UNIT_TYPE GetUnitType(long unitUID)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData.GetUnitFromUID(unitUID) != null)
			{
				return NKM_UNIT_TYPE.NUT_NORMAL;
			}
			if (armyData.GetShipFromUID(unitUID) != null)
			{
				return NKM_UNIT_TYPE.NUT_SHIP;
			}
			if (armyData.GetOperatorFromUId(unitUID) != null)
			{
				return NKM_UNIT_TYPE.NUT_OPERATOR;
			}
			return NKM_UNIT_TYPE.NUT_NORMAL;
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x001EDE02 File Offset: 0x001EC002
		private NKMBackgroundUnitInfo GetBGUnitInfo(int index, NKMBackgroundInfo bgInfo)
		{
			if (index < 0)
			{
				return null;
			}
			if (bgInfo == null)
			{
				return null;
			}
			if (bgInfo.unitInfoList == null)
			{
				return null;
			}
			if (index < bgInfo.unitInfoList.Count)
			{
				return bgInfo.unitInfoList[index];
			}
			return null;
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x001EDE38 File Offset: 0x001EC038
		public void UpdateUnitIllust(int index, NKMBackgroundInfo bgInfo)
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			NKCUICharacterView charView = this.GetCharView(index);
			if (charView == null)
			{
				return;
			}
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(index, bgInfo);
			charView.SetCharacterIllust(bgunitInfo, false, true);
			this.RefreshEmotionMenu();
			this.RefreshSkinOptionMenu();
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x001EDE80 File Offset: 0x001EC080
		private bool GetUnitSkinBGEnabled(int index)
		{
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(index, this.m_currentBackgroundInfo);
			return bgunitInfo == null || bgunitInfo.backImage;
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x001EDEA6 File Offset: 0x001EC0A6
		private void SetUnitSkinBGEnabled(int index, bool value)
		{
			this.GetBGUnitInfo(index, this.m_currentBackgroundInfo).backImage = value;
			this.UpdateUnitIllust(index, this.m_currentBackgroundInfo);
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x001EDEC8 File Offset: 0x001EC0C8
		private void OnTglUnitBG(bool value)
		{
			this.SetUnitSkinBGEnabled(this.m_currentUnitIndex, value);
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x001EDED8 File Offset: 0x001EC0D8
		private void CycleSkinOption(int index)
		{
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(index, this.m_currentBackgroundInfo);
			if (this.m_skinOptionCount == 0)
			{
				bgunitInfo.skinOption = 0;
				this.UpdateUnitIllust(index, this.m_currentBackgroundInfo);
				return;
			}
			int skinOption = (bgunitInfo.skinOption + 1) % this.m_skinOptionCount;
			bgunitInfo.skinOption = skinOption;
			this.UpdateUnitIllust(index, this.m_currentBackgroundInfo);
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x001EDF34 File Offset: 0x001EC134
		private void OnSkinOption()
		{
			this.CycleSkinOption(this.m_currentUnitIndex);
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x001EDF44 File Offset: 0x001EC144
		private void OnSkinChange()
		{
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			if (bgunitInfo == null)
			{
				return;
			}
			if (bgunitInfo.unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return;
			}
			NKMUnitData unitOrTrophyFromUID = NKCScenManager.CurrentArmyData().GetUnitOrTrophyFromUID(bgunitInfo.unitUid);
			if (unitOrTrophyFromUID == null)
			{
				return;
			}
			NKCUIShopSkinPopup.Instance.OpenForUnitInfo(unitOrTrophyFromUID, false);
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x001EDF94 File Offset: 0x001EC194
		private void ResetPosition()
		{
			NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
			if (charView != null)
			{
				charView.SetOffset(Vector2.zero);
				charView.SetScale(1f);
			}
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			bgunitInfo.unitPosX = 0f;
			bgunitInfo.unitPosY = 0f;
			bgunitInfo.unitSize = 1f;
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x001EDFFF File Offset: 0x001EC1FF
		private void ToggleEmotionMenu()
		{
			if (this.m_LobbyFace.IsOpen)
			{
				this.m_LobbyFace.Close();
				return;
			}
			this.OpenEmotionMenu();
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x001EE020 File Offset: 0x001EC220
		private void OpenEmotionMenu()
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
			NKCASUIUnitIllust targetIllust = (charView != null) ? charView.GetUnitIllust() : null;
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			int unitFace = bgunitInfo.unitFace;
			NKM_UNIT_TYPE unitType = bgunitInfo.unitType;
			if (unitType - NKM_UNIT_TYPE.NUT_NORMAL > 1)
			{
				if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					NKMOperator operatorFromUId = armyData.GetOperatorFromUId(bgunitInfo.unitUid);
					this.m_LobbyFace.Open(operatorFromUId, unitFace, targetIllust);
					return;
				}
			}
			else
			{
				NKMUnitData unitOrShipFromUID = armyData.GetUnitOrShipFromUID(bgunitInfo.unitUid);
				this.m_LobbyFace.Open(unitOrShipFromUID, unitFace, targetIllust);
			}
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x001EE0BC File Offset: 0x001EC2BC
		private void RefreshSkinOptionMenu()
		{
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
			this.m_skinOptionCount = ((bgunitInfo.unitUid > 0L && charView.GetUnitIllust() != null) ? charView.GetUnitIllust().GetSkinOptionCount() : 0);
			NKCUtil.SetGameobjectActive(this.m_BtnSkinOption, this.m_skinOptionCount > 0);
			NKCUtil.SetGameobjectActive(this.m_btnSkinChange, bgunitInfo.unitType == NKM_UNIT_TYPE.NUT_NORMAL && bgunitInfo.unitUid > 0L);
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x001EE144 File Offset: 0x001EC344
		private void RefreshEmotionMenu()
		{
			if (this.m_LobbyFace.IsOpen)
			{
				this.OpenEmotionMenu();
			}
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001EE15C File Offset: 0x001EC35C
		private void OnSelectFace(int selectedID)
		{
			this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo).unitFace = selectedID;
			NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
			NKMLobbyFaceTemplet defaultAnimation = NKMTempletContainer<NKMLobbyFaceTemplet>.Find(selectedID);
			charView.SetDefaultAnimation(defaultAnimation);
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x001EE19A File Offset: 0x001EC39A
		private void OpenSelectBackground()
		{
			this.m_bOpenSelectBackground = true;
			this.m_aniBGList.Play("AB_UI_NKM_UI_USER_INFO_BG_LIST_SCROLL_BASE");
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x001EE1B3 File Offset: 0x001EC3B3
		private void CloseSelectBackground()
		{
			this.m_bOpenSelectBackground = false;
			this.m_aniBGList.Play("AB_UI_NKM_UI_USER_INFO_BG_LIST_SCROLL_OUTRO");
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001EE1CC File Offset: 0x001EC3CC
		private void OnTouchSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			int num = slotData.ID;
			if (num == 9001)
			{
				num = 0;
			}
			this.UpdateBackground(num);
			this.UpdateBGSlotList(num);
			this.UpdateBackgroundMusic(this.m_currentBackgroundInfo, false);
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x001EE208 File Offset: 0x001EC408
		public void SetBackgroundInfo(NKMBackgroundInfo bgInfo, bool bMusic)
		{
			this.m_currentBackgroundInfo = bgInfo;
			for (int i = 0; i < 6; i++)
			{
				this.UpdateUnitIllust(i, this.m_currentBackgroundInfo);
			}
			this.UpdateBackground(this.m_currentBackgroundInfo.backgroundItemId);
			this.UpdateBackgroundMusic(this.m_currentBackgroundInfo, false);
			this.UpdateBGSlotList(this.m_currentBackgroundInfo.backgroundItemId);
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			this.m_btnEmotion.SetLock(false, false);
			this.m_tglUnitBG.Select(this.GetUnitSkinBGEnabled(this.m_currentUnitIndex), false, false);
			if (bgunitInfo.unitType == NKM_UNIT_TYPE.NUT_NORMAL || bgunitInfo.unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.RefreshEmotionMenu();
			}
			else
			{
				this.m_LobbyFace.Close();
			}
			this.UpdateUnitList();
			this.RefreshSkinOptionMenu();
			this.m_bBGMContinue = NKCScenManager.CurrentUserData().BackgroundBGMContinue;
			this.m_ctglMusicContinue.Select(this.m_bBGMContinue, true, false);
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x001EE2F4 File Offset: 0x001EC4F4
		public void UpdateBackground(int backgroundItemId)
		{
			NKCBackgroundTemplet nkcbackgroundTemplet = NKCBackgroundTemplet.Find(backgroundItemId);
			if (nkcbackgroundTemplet == null)
			{
				nkcbackgroundTemplet = NKCBackgroundTemplet.Find(9001);
			}
			if (nkcbackgroundTemplet == null)
			{
				this.m_currentBackgroundInfo.backgroundItemId = 0;
				this.SetBackground(NKMAssetName.ParseBundleName("AB_UI_BG_SPRITE_CITY_NIGHT", "AB_UI_BG_SPRITE_CITY_NIGHT"), true);
				return;
			}
			this.m_currentBackgroundInfo.backgroundItemId = nkcbackgroundTemplet.Key;
			this.SetBackground(NKMAssetName.ParseBundleName(nkcbackgroundTemplet.m_Background_Prefab, nkcbackgroundTemplet.m_Background_Prefab), nkcbackgroundTemplet.m_bBackground_CamMove);
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x001EE36C File Offset: 0x001EC56C
		private void SetBackground(NKMAssetName assetName, bool bCamMove)
		{
			if (this.m_objBG != null)
			{
				UnityEngine.Object.Destroy(this.m_objBG);
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>(assetName, false);
			if (nkcassetResourceData != null && nkcassetResourceData.GetAsset<GameObject>() != null)
			{
				this.m_objBG = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>());
				if (null != this.m_objBG)
				{
					this.m_objBG.transform.SetParent(this.m_trBGRoot);
					Transform transform = this.m_objBG.transform.Find("Stretch/Background");
					if (transform != null)
					{
						RectTransform component = transform.GetComponent<RectTransform>();
						if (null != component)
						{
							NKCCamera.SetPos(0f, 0f, -1000f, true, false);
							Vector2 cameraMoveRectSize = bCamMove ? new Vector2(200f, 200f) : Vector2.zero;
							NKCCamera.RescaleRectToCameraFrustrum(component, NKCCamera.GetCamera(), cameraMoveRectSize, -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
						}
						EventTrigger eventTrigger = transform.GetComponent<EventTrigger>();
						if (eventTrigger == null)
						{
							eventTrigger = transform.gameObject.AddComponent<EventTrigger>();
						}
						eventTrigger.triggers.Clear();
						EventTrigger.Entry entry = new EventTrigger.Entry();
						entry.eventID = EventTriggerType.Drag;
						entry.callback.AddListener(delegate(BaseEventData eventData)
						{
							PointerEventData eventData2 = eventData as PointerEventData;
							this.OnDrag(eventData2);
						});
						eventTrigger.triggers.Add(entry);
					}
				}
			}
			if (nkcassetResourceData != null)
			{
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
			}
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x001EE4C8 File Offset: 0x001EC6C8
		public void UpdateBGSlotList(int currentBGID)
		{
			if (currentBGID == 0)
			{
				currentBGID = 9001;
			}
			int count = this.m_listBGTemplet.Count;
			for (int i = this.m_listBGSlot.Count; i < count; i++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_ScrollRect.content);
				if (newInstance != null)
				{
					newInstance.transform.localScale = Vector3.one;
					this.m_listBGSlot.Add(newInstance);
				}
			}
			for (int j = 0; j < this.m_listBGSlot.Count; j++)
			{
				NKCUISlot nkcuislot = this.m_listBGSlot[j];
				NKCUtil.SetGameobjectActive(nkcuislot, j < count);
				if (j < count)
				{
					NKCBackgroundTemplet nkcbackgroundTemplet = this.m_listBGTemplet[j];
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					nkcuislot.SetMiscItemData(nkcbackgroundTemplet.m_ItemMiscID, 1L, false, false, true, new NKCUISlot.OnClick(this.OnTouchSlot));
					nkcuislot.SetSelected(nkcbackgroundTemplet.m_ItemMiscID == currentBGID);
				}
			}
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x001EE5B0 File Offset: 0x001EC7B0
		private void OnDragUnit(PointerEventData cPointerEventData)
		{
			NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
			if (charView == null)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetHasPinch())
			{
				this.OnPinch(charView, NKCScenManager.GetScenManager().GetPinchDeltaMagnitude());
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				this.OnPinch(charView, (cPointerEventData.delta.x + cPointerEventData.delta.y) / (float)Screen.width);
				return;
			}
			Vector2 screenPoint = cPointerEventData.position - cPointerEventData.delta;
			Vector2 position = cPointerEventData.position;
			RectTransform component = charView.GetComponent<RectTransform>();
			Vector2 b;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component, screenPoint, NKCCamera.GetCamera(), out b);
			Vector2 a;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component, position, NKCCamera.GetCamera(), out a);
			Vector2 b2 = (a - b) * charView.GetScale();
			Vector2 vector = charView.GetOffset() + b2;
			charView.SetOffset(vector);
			NKMBackgroundUnitInfo bgunitInfo = this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo);
			bgunitInfo.unitPosX = vector.x;
			bgunitInfo.unitPosY = vector.y;
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x001EE6C4 File Offset: 0x001EC8C4
		private void OnTouchUnit(PointerEventData cPointerEventData, NKCUICharacterView charView)
		{
			if (charView.GetUnitIllust() == null)
			{
				return;
			}
			int num = this.m_lstCvUnit.IndexOf(charView);
			if (num >= 0 && num < this.m_lstCvUnit.Count)
			{
				if (this.m_currentUnitIndex == num)
				{
					charView.TouchIllust();
					return;
				}
				this.SelectUnit(num, true);
			}
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x001EE714 File Offset: 0x001EC914
		private void OnPinch(NKCUICharacterView charView, float pinchMagnitude)
		{
			if (charView == null)
			{
				return;
			}
			float num = charView.GetScale() * Mathf.Pow(5f, pinchMagnitude);
			num = Mathf.Clamp(num, this.MIN_ZOOM_SCALE, this.MAX_ZOOM_SCALE);
			DOTween.CompleteAll(false);
			charView.SetScale(num);
			this.GetBGUnitInfo(this.m_currentUnitIndex, this.m_currentBackgroundInfo).unitSize = num;
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x001EE778 File Offset: 0x001EC978
		private void Update()
		{
			if (this.IsPreviewMode && Input.anyKey)
			{
				this.SetPreview(false);
			}
			if (!this.m_bOpenSelectBackground)
			{
				float y = Input.mouseScrollDelta.y;
				if (y != 0f)
				{
					NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
					this.OnPinch(charView, y * this.m_MouseWheelSensibility);
				}
			}
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x001EE7D2 File Offset: 0x001EC9D2
		public void OnDrag(PointerEventData eventData)
		{
			this.OnDragUnit(eventData);
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x001EE7DC File Offset: 0x001EC9DC
		private void UpdateBackgroundMusic(NKMBackgroundInfo bgInfo, bool bForce = false)
		{
			NKCBGMInfoTemplet nkcbgminfoTemplet = NKMTempletContainer<NKCBGMInfoTemplet>.Find(bgInfo.backgroundBgmId);
			if (nkcbgminfoTemplet != null)
			{
				NKCSoundManager.PlayMusic(nkcbgminfoTemplet.m_BgmAssetID, true, nkcbgminfoTemplet.BGMVolume, bForce, 0f, 0f);
				NKCUtil.SetLabelText(this.m_lbMusicTitle, NKCStringTable.GetString(nkcbgminfoTemplet.m_BgmNameStringID, false));
				return;
			}
			NKCBackgroundTemplet nkcbackgroundTemplet = NKCBackgroundTemplet.Find(bgInfo.backgroundItemId);
			float fLocalVol = 1f;
			foreach (NKCBGMInfoTemplet nkcbgminfoTemplet2 in NKMTempletContainer<NKCBGMInfoTemplet>.Values)
			{
				if (string.Equals(nkcbackgroundTemplet.m_Background_Music, nkcbgminfoTemplet2.m_BgmAssetID))
				{
					fLocalVol = nkcbgminfoTemplet2.BGMVolume;
					break;
				}
			}
			NKCSoundManager.PlayMusic(nkcbackgroundTemplet.m_Background_Music, true, fLocalVol, false, 0f, 0f);
			NKCUtil.SetLabelText(this.m_lbMusicTitle, NKCStringTable.GetString("SI_DP_LOBBY_MUSIC_DEFAULT", false));
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x001EE8C4 File Offset: 0x001ECAC4
		private void OnClickBGMContinue(bool bSet)
		{
			this.m_bBGMContinue = bSet;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x001EE8CD File Offset: 0x001ECACD
		private void UpdatePresetButtons()
		{
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x001EE8CF File Offset: 0x001ECACF
		private void TrySavePreset(NKMBackgroundInfo bgInfo)
		{
			if (this.m_currentPresetIndex >= 0)
			{
				this.SavePreset(this.m_currentPresetIndex, bgInfo);
			}
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x001EE8E7 File Offset: 0x001ECAE7
		private void OnTglPreset(bool value, int index)
		{
			if (value)
			{
				this.SelectPreset(index);
			}
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x001EE8F3 File Offset: 0x001ECAF3
		private void SelectPreset(int value)
		{
			this.m_currentPresetIndex = value;
			NKCUIComStateButton csbtnLoadPreset = this.m_csbtnLoadPreset;
			if (csbtnLoadPreset != null)
			{
				csbtnLoadPreset.SetLock(!this.HasPreset(value), false);
			}
			NKCUIComStateButton csbtnSavePreset = this.m_csbtnSavePreset;
			if (csbtnSavePreset == null)
			{
				return;
			}
			csbtnSavePreset.UnLock(false);
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x001EE92C File Offset: 0x001ECB2C
		private void OnBtnLoadPreset()
		{
			if (this.HasPreset(this.m_currentPresetIndex))
			{
				string @string = NKCStringTable.GetString("SI_DP_CHANGE_LOBBY_PRESET_LOAD_CONFIRM_TITLE", new object[]
				{
					this.m_currentPresetIndex + 1
				});
				string string2 = NKCStringTable.GetString("SI_PF_USER_INFO_LOBBY_CHANGE_PRESET_LOAD", false);
				NKCUIPopupChangeLobbyPreview.Instance.Open(this.m_currentPresetIndex, @string, this.GetPreviewThumbnailPath(this.m_currentPresetIndex), string2, new NKCUIPopupChangeLobbyPreview.OnConfirm(this.TryLoadPreset));
			}
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x001EE9A0 File Offset: 0x001ECBA0
		private void OnBtnSavePreset()
		{
			if (this.HasPreset(this.m_currentPresetIndex))
			{
				string @string = NKCStringTable.GetString("SI_DP_CHANGE_LOBBY_PRESET_LOAD_CONFIRM_TITLE", new object[]
				{
					this.m_currentPresetIndex + 1
				});
				string string2 = NKCStringTable.GetString("SI_PF_USER_INFO_LOBBY_CHANGE_PRESET_SAVE", false);
				NKCUIPopupChangeLobbyPreview.Instance.Open(this.m_currentPresetIndex, @string, this.GetPreviewThumbnailPath(this.m_currentPresetIndex), string2, new NKCUIPopupChangeLobbyPreview.OnConfirm(this.SavePreset));
				return;
			}
			this.SavePreset(this.m_currentPresetIndex);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x001EEA20 File Offset: 0x001ECC20
		private void SavePreset(int index)
		{
			if (index >= 0)
			{
				this.SavePreset(index, this.m_currentBackgroundInfo);
				this.UpdatePresetButtons();
				NKCUIComStateButton csbtnLoadPreset = this.m_csbtnLoadPreset;
				if (csbtnLoadPreset != null)
				{
					csbtnLoadPreset.UnLock(false);
				}
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_LOBBY_BACKGROUND_PRESET_SAVED", new object[]
				{
					this.m_currentPresetIndex + 1
				}), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x001EEA84 File Offset: 0x001ECC84
		private void TryLoadPreset(int index)
		{
			NKMBackgroundInfo nkmbackgroundInfo = this.LoadPreset(index);
			if (nkmbackgroundInfo != null)
			{
				this.SetBackgroundInfo(nkmbackgroundInfo, true);
			}
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x001EEAA4 File Offset: 0x001ECCA4
		private void SavePreset(int index, NKMBackgroundInfo bgInfo)
		{
			string value = this.EncodeBGInfo(bgInfo);
			PlayerPrefs.SetString(string.Format("BACKGROUND_PRESET_{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, index), value);
			string previewThumbnailPath = this.GetPreviewThumbnailPath(index);
			NKCScreenCaptureUtility.CaptureCamera(NKCCamera.GetCamera(), previewThumbnailPath, Screen.width / 4, Screen.height / 4);
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x001EEB00 File Offset: 0x001ECD00
		private string GetPreviewThumbnailPath(int index)
		{
			string str = string.Format("BACKGROUND_PRESET_{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, index);
			return Path.Combine(Application.persistentDataPath, str + ".png");
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x001EEB44 File Offset: 0x001ECD44
		private NKMBackgroundInfo LoadPreset(int index)
		{
			if (!this.HasPreset(index))
			{
				return null;
			}
			string @string = PlayerPrefs.GetString(string.Format("BACKGROUND_PRESET_{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, index));
			return this.DecodeBGInfo(@string);
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x001EEB88 File Offset: 0x001ECD88
		private NKMBackgroundInfo MakeEmptyBGInfo()
		{
			NKMBackgroundInfo nkmbackgroundInfo = new NKMBackgroundInfo();
			nkmbackgroundInfo.unitInfoList = new List<NKMBackgroundUnitInfo>(6);
			for (int i = 0; i < 6; i++)
			{
				nkmbackgroundInfo.unitInfoList.Add(new NKMBackgroundUnitInfo());
			}
			return nkmbackgroundInfo;
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x001EEBC4 File Offset: 0x001ECDC4
		private bool HasPreset(int index)
		{
			return PlayerPrefs.HasKey(string.Format("BACKGROUND_PRESET_{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, index));
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x001EEBEC File Offset: 0x001ECDEC
		private string EncodeBGInfo(NKMBackgroundInfo bgInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BgInfo={");
			stringBuilder.AppendFormat("bgID={0},", bgInfo.backgroundItemId);
			stringBuilder.AppendFormat("bgmID={0},", bgInfo.backgroundBgmId);
			for (int i = 0; i < bgInfo.unitInfoList.Count; i++)
			{
				stringBuilder.Append("{");
				NKMBackgroundUnitInfo nkmbackgroundUnitInfo = bgInfo.unitInfoList[i];
				if (nkmbackgroundUnitInfo != null)
				{
					stringBuilder.AppendFormat("uid=\"{0}\",", nkmbackgroundUnitInfo.unitUid);
					stringBuilder.AppendFormat("type={0},", (int)nkmbackgroundUnitInfo.unitType);
					stringBuilder.AppendFormat("size={0},", nkmbackgroundUnitInfo.unitSize);
					stringBuilder.AppendFormat("face={0},", nkmbackgroundUnitInfo.unitFace);
					stringBuilder.AppendFormat("X={0},", nkmbackgroundUnitInfo.unitPosX);
					stringBuilder.AppendFormat("Y={0},", nkmbackgroundUnitInfo.unitPosY);
					stringBuilder.AppendFormat("back={0},", nkmbackgroundUnitInfo.backImage.ToString().ToLower());
					stringBuilder.AppendFormat("skinOption={0},", nkmbackgroundUnitInfo.skinOption);
				}
				stringBuilder.Append("},");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x001EED50 File Offset: 0x001ECF50
		private NKMBackgroundInfo DecodeBGInfo(string encodedString)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKMBackgroundInfo nkmbackgroundInfo = new NKMBackgroundInfo();
			NKMBackgroundInfo result;
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.DoString(encodedString))
				{
					result = null;
				}
				else if (nkmlua.OpenTable("BgInfo"))
				{
					nkmlua.GetData("bgID", ref nkmbackgroundInfo.backgroundItemId);
					nkmlua.GetData("bgmID", ref nkmbackgroundInfo.backgroundBgmId);
					nkmbackgroundInfo.unitInfoList = new List<NKMBackgroundUnitInfo>();
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKMBackgroundUnitInfo nkmbackgroundUnitInfo = new NKMBackgroundUnitInfo();
						string @string = nkmlua.GetString("uid");
						nkmbackgroundUnitInfo.unitUid = long.Parse(@string);
						int num2 = 0;
						nkmlua.GetData("type", ref num2);
						nkmbackgroundUnitInfo.unitType = (NKM_UNIT_TYPE)num2;
						nkmlua.GetData("size", ref nkmbackgroundUnitInfo.unitSize);
						nkmlua.GetData("face", ref nkmbackgroundUnitInfo.unitFace);
						nkmlua.GetData("X", ref nkmbackgroundUnitInfo.unitPosX);
						nkmlua.GetData("Y", ref nkmbackgroundUnitInfo.unitPosY);
						nkmlua.GetData("back", ref nkmbackgroundUnitInfo.backImage);
						nkmlua.GetData("skinOption", ref nkmbackgroundUnitInfo.skinOption);
						NKM_UNIT_TYPE unitType = nkmbackgroundUnitInfo.unitType;
						if (unitType - NKM_UNIT_TYPE.NUT_NORMAL > 1)
						{
							if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR && armyData.GetOperatorFromUId(nkmbackgroundUnitInfo.unitUid) == null)
							{
								nkmbackgroundUnitInfo.unitUid = 0L;
								nkmbackgroundUnitInfo.unitType = NKM_UNIT_TYPE.NUT_INVALID;
								nkmbackgroundUnitInfo.unitFace = 0;
							}
						}
						else if (armyData.GetUnitOrShipFromUID(nkmbackgroundUnitInfo.unitUid) == null)
						{
							nkmbackgroundUnitInfo.unitUid = 0L;
							nkmbackgroundUnitInfo.unitType = NKM_UNIT_TYPE.NUT_INVALID;
							nkmbackgroundUnitInfo.unitFace = 0;
						}
						nkmbackgroundInfo.unitInfoList.Add(nkmbackgroundUnitInfo);
						num++;
						nkmlua.CloseTable();
					}
					nkmlua.CloseTable();
					while (nkmbackgroundInfo.unitInfoList.Count < 6)
					{
						NKMBackgroundUnitInfo nkmbackgroundUnitInfo2 = new NKMBackgroundUnitInfo();
						nkmbackgroundUnitInfo2.unitUid = 0L;
						nkmbackgroundUnitInfo2.unitFace = 0;
						nkmbackgroundUnitInfo2.unitPosX = 0f;
						nkmbackgroundUnitInfo2.unitPosY = 0f;
						nkmbackgroundUnitInfo2.unitSize = 1f;
						nkmbackgroundUnitInfo2.backImage = true;
						nkmbackgroundUnitInfo2.skinOption = 0;
						nkmbackgroundUnitInfo2.unitType = NKM_UNIT_TYPE.NUT_NORMAL;
						nkmbackgroundInfo.unitInfoList.Add(nkmbackgroundUnitInfo2);
					}
					result = nkmbackgroundInfo;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600627F RID: 25215 RVA: 0x001EEFB0 File Offset: 0x001ED1B0
		private bool IsPreviewMode
		{
			get
			{
				return this.m_objPreviewRoot != null && this.m_objPreviewRoot.activeSelf;
			}
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x001EEFCD File Offset: 0x001ED1CD
		private void SetPreview(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objPreviewRoot, value);
			NKCUtil.SetGameobjectActive(this.m_objUIRoot, !value);
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x001EEFEA File Offset: 0x001ED1EA
		private void OnBtnPreview()
		{
			this.SetPreview(true);
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x001EEFF4 File Offset: 0x001ED1F4
		public override void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Plus)
			{
				NKCUICharacterView charView = this.GetCharView(this.m_currentUnitIndex);
				this.OnPinch(charView, Time.deltaTime * 0.5f);
				return;
			}
			if (hotkey != HotkeyEventType.Minus)
			{
				return;
			}
			NKCUICharacterView charView2 = this.GetCharView(this.m_currentUnitIndex);
			this.OnPinch(charView2, -Time.deltaTime * 0.5f);
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x001EF04C File Offset: 0x001ED24C
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.ShowHotkey)
			{
				NKCUIComHotkeyDisplay.OpenInstance(base.transform, new HotkeyEventType[]
				{
					HotkeyEventType.Plus,
					HotkeyEventType.Minus
				});
			}
			return false;
		}

		// Token: 0x04004E2F RID: 20015
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_user_info";

		// Token: 0x04004E30 RID: 20016
		private const string UI_ASSET_NAME = "NKM_UI_USER_INFO_LOBBY_CHANGE_RENEWAL";

		// Token: 0x04004E31 RID: 20017
		private static NKCUIChangeLobby m_Instance;

		// Token: 0x04004E32 RID: 20018
		public Transform m_trRoot;

		// Token: 0x04004E33 RID: 20019
		public Transform m_trBGRoot;

		// Token: 0x04004E34 RID: 20020
		[Header("Button")]
		public NKCUIComDraggableList m_comDraggableList;

		// Token: 0x04004E35 RID: 20021
		public List<NKCUIComToggle> m_lstTglUnit;

		// Token: 0x04004E36 RID: 20022
		public NKCUIComToggle m_tglUnitBG;

		// Token: 0x04004E37 RID: 20023
		public NKCUIComStateButton m_btnChangeUnit;

		// Token: 0x04004E38 RID: 20024
		public NKCUIComStateButton m_btnEmotion;

		// Token: 0x04004E39 RID: 20025
		public NKCUIComStateButton m_btnResetPosition;

		// Token: 0x04004E3A RID: 20026
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x04004E3B RID: 20027
		public NKCUIComStateButton m_btnApplyToLobby;

		// Token: 0x04004E3C RID: 20028
		public NKCUIComStateButton m_btnJukeBox;

		// Token: 0x04004E3D RID: 20029
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04004E3E RID: 20030
		public NKCUIComStateButton m_BtnSkinOption;

		// Token: 0x04004E3F RID: 20031
		public NKCUIComStateButton m_btnSkinChange;

		// Token: 0x04004E40 RID: 20032
		[Header("Unit")]
		public List<NKCUICharacterView> m_lstCvUnit;

		// Token: 0x04004E41 RID: 20033
		[Header("Music")]
		public Text m_lbMusicTitle;

		// Token: 0x04004E42 RID: 20034
		public NKCUIComToggle m_ctglMusicContinue;

		// Token: 0x04004E43 RID: 20035
		[Header("Background")]
		public ScrollRect m_ScrollRect;

		// Token: 0x04004E44 RID: 20036
		public NKCUIComStateButton m_btnBackClose;

		// Token: 0x04004E45 RID: 20037
		public Animator m_aniBGList;

		// Token: 0x04004E46 RID: 20038
		[Header("Face")]
		public NKCUIChangeLobbyFace m_LobbyFace;

		// Token: 0x04004E47 RID: 20039
		[Header("Preset")]
		public List<NKCUIComToggle> m_lstTglPreset;

		// Token: 0x04004E48 RID: 20040
		public NKCUIComStateButton m_csbtnLoadPreset;

		// Token: 0x04004E49 RID: 20041
		public NKCUIComStateButton m_csbtnSavePreset;

		// Token: 0x04004E4A RID: 20042
		[Header("유닛 선택 관련")]
		public Color m_SelectStartColor;

		// Token: 0x04004E4B RID: 20043
		public float m_SelectScale = 1.1f;

		// Token: 0x04004E4C RID: 20044
		public float m_SelectTime = 0.4f;

		// Token: 0x04004E4D RID: 20045
		public Ease m_Ease = Ease.OutCubic;

		// Token: 0x04004E4E RID: 20046
		[Header("최대/최소 줌")]
		public float MIN_ZOOM_SCALE = 0.1f;

		// Token: 0x04004E4F RID: 20047
		public float MAX_ZOOM_SCALE = 3f;

		// Token: 0x04004E50 RID: 20048
		public float m_MouseWheelSensibility = 0.1f;

		// Token: 0x04004E51 RID: 20049
		[Header("미리보기")]
		public NKCUIComStateButton m_csbtnPreview;

		// Token: 0x04004E52 RID: 20050
		public GameObject m_objUIRoot;

		// Token: 0x04004E53 RID: 20051
		public GameObject m_objPreviewRoot;

		// Token: 0x04004E54 RID: 20052
		private NKMBackgroundInfo m_currentBackgroundInfo;

		// Token: 0x04004E55 RID: 20053
		private int m_currentPresetIndex = -1;

		// Token: 0x04004E56 RID: 20054
		private int m_currentUnitIndex;

		// Token: 0x04004E57 RID: 20055
		private int m_skinOptionCount;

		// Token: 0x04004E58 RID: 20056
		private List<NKCBackgroundTemplet> m_listBGTemplet = new List<NKCBackgroundTemplet>();

		// Token: 0x04004E59 RID: 20057
		private List<NKCUISlot> m_listBGSlot = new List<NKCUISlot>();

		// Token: 0x04004E5A RID: 20058
		private GameObject m_objBG;

		// Token: 0x04004E5B RID: 20059
		private bool m_bOpenSelectBackground;

		// Token: 0x04004E5C RID: 20060
		private bool m_bBGMContinue;

		// Token: 0x04004E5D RID: 20061
		private const string BG_PRESET_SAVE_KEY = "BACKGROUND_PRESET_{0}_{1}";
	}
}
