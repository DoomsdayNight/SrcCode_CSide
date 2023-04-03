using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Core.Util;
using Cs.Logging;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B6E RID: 2926
	public class NKCUIGauntletLeagueGlobalBan : NKCUIBase
	{
		// Token: 0x060085C6 RID: 34246 RVA: 0x002D4ADC File Offset: 0x002D2CDC
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			return NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletLeagueGlobalBan>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LEAGUE_POPUP_GLOBAL_BAN", NKCUIManager.eUIBaseRect.UIFrontCommon, null);
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x060085C7 RID: 34247 RVA: 0x002D4AEF File Offset: 0x002D2CEF
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x060085C8 RID: 34248 RVA: 0x002D4AF2 File Offset: 0x002D2CF2
		public override bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x060085C9 RID: 34249 RVA: 0x002D4AF5 File Offset: 0x002D2CF5
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x060085CA RID: 34250 RVA: 0x002D4AF8 File Offset: 0x002D2CF8
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x060085CB RID: 34251 RVA: 0x002D4AFF File Offset: 0x002D2CFF
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					101
				};
			}
		}

		// Token: 0x060085CC RID: 34252 RVA: 0x002D4B0E File Offset: 0x002D2D0E
		public override void CloseInternal()
		{
			this.m_NKCLeaguePvpUnitSelectList.Close(false);
		}

		// Token: 0x060085CD RID: 34253 RVA: 0x002D4B1C File Offset: 0x002D2D1C
		public void Init()
		{
			this.m_prevRemainingSeconds = 0;
			NKCUtil.SetGameobjectActive(this.m_SelectedObject, false);
			NKCUtil.SetGameobjectActive(this.m_CandidateObject, false);
			NKCUtil.SetGameobjectActive(this.m_FinalResultObject, false);
			NKCUtil.SetGameobjectActive(this.m_BanSelectConfirm, false);
			NKCUtil.SetGameobjectActive(this.m_objWatingNotice, false);
			NKCUtil.SetGameobjectActive(this.m_LeaveRoom, false);
			NKCUtil.SetGameobjectActive(this.m_BanCandidateSearchObject, false);
			if (this.m_BanSelectConfirm != null)
			{
				this.m_BanSelectConfirm.PointerClick.RemoveAllListeners();
				this.m_BanSelectConfirm.PointerClick.AddListener(new UnityAction(this.OnClickSelectConfirm));
			}
			if (this.m_LeaveRoom != null)
			{
				this.m_LeaveRoom.PointerClick.RemoveAllListeners();
				this.m_LeaveRoom.PointerClick.AddListener(new UnityAction(this.OnClickGiveup));
			}
			if (this.m_BanCandidateSearchInputField != null)
			{
				Log.Info("[League] BanCandidateSearch", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueGlobalBan.cs", 190);
				this.m_BanCandidateSearchInputField.onEndEdit.RemoveAllListeners();
				this.m_BanCandidateSearchInputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSearch));
			}
			if (this.m_BanCandidateSearchButton != null)
			{
				this.m_BanCandidateSearchButton.PointerClick.RemoveAllListeners();
				this.m_BanCandidateSearchButton.PointerClick.AddListener(new UnityAction(this.OnClickSearch));
			}
			this.m_ViewerOptions = default(NKCUIDeckViewer.DeckViewerOption);
			this.m_ViewerOptions.MenuName = "";
			this.m_ViewerOptions.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.LeaguePvPGlobalBan;
			this.m_ViewerOptions.dOnSideMenuButtonConfirm = null;
			this.m_NKCLeaguePvpUnitSelectList.Init(null, null, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectCandidateSlot), null);
			NKCUtil.SetLabelText(this.m_SelectedCount, string.Format("{0}/{1}", 0, 2));
			NKCDeckViewUnitSelectListSlot[] selectedUnitList = this.m_SelectedUnitList;
			for (int i = 0; i < selectedUnitList.Length; i++)
			{
				NKCUtil.SetGameobjectActive(selectedUnitList[i], false);
			}
			base.UIOpened(true);
		}

		// Token: 0x060085CE RID: 34254 RVA: 0x002D4D18 File Offset: 0x002D2F18
		public void SetEndTime(DateTime endTime)
		{
			this.m_endTime = endTime;
			this.m_prevRemainingSeconds = Math.Max(0, Convert.ToInt32((this.m_endTime - ServiceTime.Now).TotalSeconds));
		}

		// Token: 0x060085CF RID: 34255 RVA: 0x002D4D58 File Offset: 0x002D2F58
		public void OpenCandidateList()
		{
			if (!NKCLeaguePVPMgr.IsObserver())
			{
				this.m_NKCLeaguePvpUnitSelectList.Open(true, NKM_UNIT_TYPE.NUT_NORMAL, this.MakeSortOptions(), this.m_ViewerOptions);
			}
			if (NKCLeaguePVPMgr.GetMyDraftTeamData() != null)
			{
				NKCUtil.SetGameobjectActive(this.m_BanSelectConfirm, true);
				NKCUtil.SetGameobjectActive(this.m_LeaveRoom, true);
				NKCUtil.SetGameobjectActive(this.m_SelectedObject, true);
				NKCUtil.SetGameobjectActive(this.m_CandidateObject, true);
				NKCUtil.SetGameobjectActive(this.m_BanCandidateSearchObject, true);
			}
			this.RefreshBanProgress();
		}

		// Token: 0x060085D0 RID: 34256 RVA: 0x002D4DD0 File Offset: 0x002D2FD0
		private NKCUnitSortSystem.UnitListOptions MakeSortOptions()
		{
			return new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = NKM_DECK_TYPE.NDT_NONE,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = null,
				setExcludeUnitUID = null,
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				bIgnoreCityState = true,
				bIgnoreWorldMapLeader = true,
				setFilterOption = this.m_NKCLeaguePvpUnitSelectList.SortOptions.setFilterOption,
				lstSortOption = this.m_NKCLeaguePvpUnitSelectList.SortOptions.lstSortOption,
				bDescending = this.m_NKCLeaguePvpUnitSelectList.SortOptions.bDescending,
				bIncludeUndeckableUnit = true,
				bHideDeckedUnit = false,
				bPushBackUnselectable = true
			};
		}

		// Token: 0x060085D1 RID: 34257 RVA: 0x002D4E8C File Offset: 0x002D308C
		public void ShowFinalResult()
		{
			this.RefreshBanProgress();
			NKCUtil.SetGameobjectActive(this.m_SelectedObject, !NKCLeaguePVPMgr.IsObserver());
			NKCUtil.SetGameobjectActive(this.m_CandidateObject, false);
			NKCUtil.SetGameobjectActive(this.m_BanCandidateSearchObject, false);
			NKCUtil.SetGameobjectActive(this.m_BanSelectConfirm, false);
			NKCUtil.SetGameobjectActive(this.m_objWatingNotice, false);
			NKCUtil.SetGameobjectActive(this.m_LeaveRoom, false);
			this.HideSequenceGuidePopup();
			for (int i = 0; i < NKCLeaguePVPMgr.GetLeftDraftTeamData().globalBanUnitIdList.Count; i++)
			{
				NKMUnitTempletBase templetBase = NKMUnitTempletBase.Find(NKCLeaguePVPMgr.GetLeftDraftTeamData().globalBanUnitIdList[i]);
				this.m_FinalResultUnitList[i].SetDataForBan(templetBase, true, null, false, false);
			}
			for (int j = 0; j < NKCLeaguePVPMgr.GetRightDraftTeamData().globalBanUnitIdList.Count; j++)
			{
				NKMUnitTempletBase templetBase2 = NKMUnitTempletBase.Find(NKCLeaguePVPMgr.GetRightDraftTeamData().globalBanUnitIdList[j]);
				this.m_FinalResultUnitList[j + 2].SetDataForBan(templetBase2, true, null, false, false);
			}
			NKCUtil.SetGameobjectActive(this.m_FinalResultObject, true);
			if (this.m_FinalResultAnimator != null)
			{
				this.m_FinalResultAnimator.Play("BAN_LIST_INTRO");
			}
			NKCSoundManager.PlaySound("FX_UI_TITLE_IN_TEST", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x060085D2 RID: 34258 RVA: 0x002D4FCA File Offset: 0x002D31CA
		public void ProcessBackButton()
		{
		}

		// Token: 0x060085D3 RID: 34259 RVA: 0x002D4FCC File Offset: 0x002D31CC
		private void UpdateUserInfo()
		{
			bool flag = NKCLeaguePVPMgr.GetLeftDraftTeamData().teamType == NKM_TEAM_TYPE.NTT_A1;
			this.m_userSlotLeft.SetData(NKCLeaguePVPMgr.GetLeftDraftTeamData().userProfileData, flag, true, NKCLeaguePVPMgr.IsPrivate());
			this.m_userSlotRight.SetData(NKCLeaguePVPMgr.GetRightDraftTeamData().userProfileData, !flag, false, NKCLeaguePVPMgr.IsPrivate());
		}

		// Token: 0x060085D4 RID: 34260 RVA: 0x002D5022 File Offset: 0x002D3222
		private void RefreshBanCandidates()
		{
			if (this.m_NKCLeaguePvpUnitSelectList != null && this.m_NKCLeaguePvpUnitSelectList.m_LoopScrollRect != null)
			{
				this.m_NKCLeaguePvpUnitSelectList.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x060085D5 RID: 34261 RVA: 0x002D5056 File Offset: 0x002D3256
		private void OnSelectCandidateSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			Debug.Log("[League] OnSelectCandidateSlot : " + unitTempletBase.m_UnitStrID);
			this.SelectUnit(unitTempletBase);
		}

		// Token: 0x060085D6 RID: 34262 RVA: 0x002D5074 File Offset: 0x002D3274
		public void OnClickSelectConfirm()
		{
			if (this.m_currentSelectedUnitTemplet == null)
			{
				return;
			}
			NKCLeaguePVPMgr.SelectGlobalBanUnit(this.m_currentSelectedUnitTemplet);
		}

		// Token: 0x060085D7 RID: 34263 RVA: 0x002D508C File Offset: 0x002D328C
		public void OnClickGiveup()
		{
			if (!NKCLeaguePVPMgr.CanLeaveRoom())
			{
				return;
			}
			if (!NKCLeaguePVPMgr.IsPrivate())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_GIVEUP_POPUP", false), delegate()
				{
					NKCLeaguePVPMgr.Send_NKMPacket_LEAGUE_PVP_GIVEUP_REQ();
				}, null, false);
				return;
			}
			if (NKCLeaguePVPMgr.IsObserver())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, "관전에서 나가시겠습니까? 텍스트", delegate()
				{
					NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ();
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL_TITLE, NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL, delegate()
			{
				NKCLeaguePVPMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ();
			}, null, false);
		}

		// Token: 0x060085D8 RID: 34264 RVA: 0x002D5147 File Offset: 0x002D3347
		public void SetLeaveRoomState()
		{
			NKCUtil.SetGameobjectActive(this.m_LeaveRoom, NKCLeaguePVPMgr.CanLeaveRoom());
		}

		// Token: 0x060085D9 RID: 34265 RVA: 0x002D5159 File Offset: 0x002D3359
		public void BanSelectedUnit()
		{
			if (this.m_currentSelectedUnitTemplet != null)
			{
				this.DeSelectUnit(this.m_currentSelectedUnitTemplet.m_UnitID);
			}
			this.m_currentSelectedUnitTemplet = null;
		}

		// Token: 0x060085DA RID: 34266 RVA: 0x002D517C File Offset: 0x002D337C
		public void RefreshMyBanUnitList(List<int> banUnitList, int oponentTeamBanCount)
		{
			int count = banUnitList.Count;
			NKCUtil.SetLabelText(this.m_SelectedCount, string.Format("{0}/{1}", count, 2));
			if (count == 2 && oponentTeamBanCount < 2)
			{
				this.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_OPPONENT", false));
			}
			for (int i = 0; i < banUnitList.Count; i++)
			{
				if (this.m_SelectedUnitList[i].NKMUnitTempletBase == null)
				{
					NKMUnitTempletBase templetBase = NKMUnitTempletBase.Find(banUnitList[i]);
					this.m_SelectedUnitList[i].SetData(templetBase, 0, false, null);
					NKCUtil.SetGameobjectActive(this.m_SelectedUnitList[i], true);
				}
			}
		}

		// Token: 0x060085DB RID: 34267 RVA: 0x002D5218 File Offset: 0x002D3418
		public void RefreshBanProgress()
		{
			this.SetEndTime(NKCLeaguePVPMgr.DraftRoomData.stateEndTime);
			this.UpdateUserInfo();
			if (NKCLeaguePVPMgr.IsObserver())
			{
				this.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_BAN_OBSERVER", false));
			}
			this.m_userSlotLeft.SetBanProgressFinished(NKCLeaguePVPMgr.GetLeftDraftTeamData().globalBanUnitIdList.Count >= 2);
			this.m_userSlotRight.SetBanProgressFinished(NKCLeaguePVPMgr.GetRightDraftTeamData().globalBanUnitIdList.Count >= 2);
			if (NKCLeaguePVPMgr.GetMyDraftTeamData() != null)
			{
				bool flag = NKCLeaguePVPMgr.GetMyDraftTeamData().globalBanUnitIdList.Count < 2;
				NKCUtil.SetGameobjectActive(this.m_BanSelectConfirm, flag);
				NKCUtil.SetGameobjectActive(this.m_LeaveRoom, flag);
				NKCUtil.SetGameobjectActive(this.m_objWatingNotice, !flag);
			}
			this.RefreshBanCandidates();
		}

		// Token: 0x060085DC RID: 34268 RVA: 0x002D52DC File Offset: 0x002D34DC
		private void Update()
		{
			if (this.m_prevRemainingSeconds > 0)
			{
				int num = Math.Max(0, Convert.ToInt32((this.m_endTime - ServiceTime.Now).TotalSeconds));
				if (num != this.m_prevRemainingSeconds)
				{
					NKCUtil.SetLabelText(this.m_timeText, num.ToString());
					this.m_prevRemainingSeconds = num;
				}
			}
		}

		// Token: 0x060085DD RID: 34269 RVA: 0x002D5338 File Offset: 0x002D3538
		private void SelectUnit(NKMUnitTempletBase unitTempletBase)
		{
			if (this.m_currentSelectedUnitTemplet != null)
			{
				this.DeSelectUnit(this.m_currentSelectedUnitTemplet.m_UnitID);
			}
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.m_NKCLeaguePvpUnitSelectList.FindSlotFromCurrentList(unitTempletBase.m_UnitID);
			if (nkcuiunitSelectListSlotBase != null)
			{
				Debug.Log(string.Format("[League] FindSlotFromCurrentList : {0}", unitTempletBase.m_UnitID));
				nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
				this.m_currentSelectedUnitTemplet = unitTempletBase;
			}
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x002D53A4 File Offset: 0x002D35A4
		private void DeSelectUnit(int unitID)
		{
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.m_NKCLeaguePvpUnitSelectList.FindSlotFromCurrentList(unitID);
			if (nkcuiunitSelectListSlotBase != null)
			{
				nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
			}
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x002D53CE File Offset: 0x002D35CE
		public void OnEndEditSearch(string inputText)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				Log.Info("[League] SearchEnter", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueGlobalBan.cs", 475);
				if (!this.m_BanCandidateSearchButton.m_bLock)
				{
					this.OnClickSearch();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x002D540C File Offset: 0x002D360C
		private void OnClickSearch()
		{
			if (this.m_BanCandidateSearchInputField != null)
			{
				this.m_banCandidateSearchString = this.m_BanCandidateSearchInputField.text;
			}
			Log.Info("[League] UpdateSearchString", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueGlobalBan.cs", 492);
			this.m_NKCLeaguePvpUnitSelectList.UpdateSearchString(this.m_banCandidateSearchString);
			this.RefreshBanCandidates();
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x002D5463 File Offset: 0x002D3663
		public void ShowSequenceGuidePopup(string text)
		{
			NKCUtil.SetGameobjectActive(this.m_PopupSequenceGuide, true);
			NKCUtil.SetLabelText(this.m_sequenceGuideText, text);
			if (this.m_animatorsequenceGuide != null)
			{
				this.m_animatorsequenceGuide.Play("NKM_UI_GAUNTLET_LEAGUE_SEQUENCE_GUIDE_INTRO");
			}
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x002D549B File Offset: 0x002D369B
		public void HideSequenceGuidePopup()
		{
			if (this.m_animatorsequenceGuide != null)
			{
				this.m_animatorsequenceGuide.Play("NKM_UI_GAUNTLET_LEAGUE_SEQUENCE_GUIDE_OUTRO");
			}
		}

		// Token: 0x04007276 RID: 29302
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04007277 RID: 29303
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_LEAGUE_POPUP_GLOBAL_BAN";

		// Token: 0x04007278 RID: 29304
		[SerializeField]
		public NKCUIGauntletLeagueGlobalBan.UserInfo m_userSlotLeft;

		// Token: 0x04007279 RID: 29305
		public NKCUIGauntletLeagueGlobalBan.UserInfo m_userSlotRight;

		// Token: 0x0400727A RID: 29306
		public GameObject m_SelectedObject;

		// Token: 0x0400727B RID: 29307
		public Text m_SelectedCount;

		// Token: 0x0400727C RID: 29308
		public NKCDeckViewUnitSelectListSlot[] m_SelectedUnitList;

		// Token: 0x0400727D RID: 29309
		public GameObject m_CandidateObject;

		// Token: 0x0400727E RID: 29310
		public NKCLeaguePvpUnitSelectList m_NKCLeaguePvpUnitSelectList;

		// Token: 0x0400727F RID: 29311
		public Animator m_FinalResultAnimator;

		// Token: 0x04007280 RID: 29312
		public GameObject m_FinalResultObject;

		// Token: 0x04007281 RID: 29313
		public NKCUIUnitSelectListSlot[] m_FinalResultUnitList;

		// Token: 0x04007282 RID: 29314
		public Text m_timeText;

		// Token: 0x04007283 RID: 29315
		public GameObject m_BanCandidateSearchObject;

		// Token: 0x04007284 RID: 29316
		public InputField m_BanCandidateSearchInputField;

		// Token: 0x04007285 RID: 29317
		public NKCUIComButton m_BanCandidateSearchButton;

		// Token: 0x04007286 RID: 29318
		public NKCUIComButton m_BanSelectConfirm;

		// Token: 0x04007287 RID: 29319
		public NKCUIComButton m_LeaveRoom;

		// Token: 0x04007288 RID: 29320
		public GameObject m_objWatingNotice;

		// Token: 0x04007289 RID: 29321
		[Header("팝업 가이드")]
		public GameObject m_PopupSequenceGuide;

		// Token: 0x0400728A RID: 29322
		public Text m_sequenceGuideText;

		// Token: 0x0400728B RID: 29323
		public Animator m_animatorsequenceGuide;

		// Token: 0x0400728C RID: 29324
		private NKCUIDeckViewer.DeckViewerOption m_ViewerOptions;

		// Token: 0x0400728D RID: 29325
		private NKMUnitTempletBase m_currentSelectedUnitTemplet;

		// Token: 0x0400728E RID: 29326
		private string m_banCandidateSearchString = "";

		// Token: 0x0400728F RID: 29327
		private DateTime m_endTime;

		// Token: 0x04007290 RID: 29328
		private int m_prevRemainingSeconds;

		// Token: 0x02001912 RID: 6418
		[Serializable]
		public class UserInfo
		{
			// Token: 0x0600B790 RID: 46992 RVA: 0x00367B50 File Offset: 0x00365D50
			public void SetData(NKMUserProfileData userProfileData, bool bFirstPick, bool isMyTeam, bool isPrivatePVP)
			{
				int seasonID = NKCPVPManager.FindPvPSeasonID(NKM_GAME_TYPE.NGT_PVP_LEAGUE, NKCSynchronizedTime.GetServerUTCTime(0.0));
				LEAGUE_TIER_ICON tierIconByScore = NKCPVPManager.GetTierIconByScore(NKM_GAME_TYPE.NGT_PVP_LEAGUE, seasonID, userProfileData.leaguePvpData.score);
				int tierNumberByScore = NKCPVPManager.GetTierNumberByScore(NKM_GAME_TYPE.NGT_PVP_LEAGUE, seasonID, userProfileData.leaguePvpData.score);
				if (this.TierIcon != null)
				{
					this.TierIcon.SetUI(tierIconByScore, tierNumberByScore);
				}
				NKCUtil.SetLabelText(this.TierScore, userProfileData.leaguePvpData.score.ToString());
				if (isPrivatePVP)
				{
					NKCUILeagueTier tierIcon = this.TierIcon;
					NKCUtil.SetGameobjectActive((tierIcon != null) ? tierIcon.gameObject : null, false);
					Text tierScore = this.TierScore;
					NKCUtil.SetGameobjectActive((tierScore != null) ? tierScore.gameObject : null, false);
					Text tierScore2 = this.TierScore;
					GameObject targetObj;
					if (tierScore2 == null)
					{
						targetObj = null;
					}
					else
					{
						Transform parent = tierScore2.transform.parent;
						targetObj = ((parent != null) ? parent.gameObject : null);
					}
					NKCUtil.SetGameobjectActive(targetObj, false);
				}
				NKCUtil.SetLabelText(this.Level, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
				{
					userProfileData.commonProfile.level
				});
				NKCUtil.SetLabelText(this.Name, NKCUtilString.GetUserNickname(userProfileData.commonProfile.nickname, !isMyTeam));
				bool bValue = true;
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD, 0, 0) || userProfileData.guildData == null || userProfileData.guildData.guildUid == 0L)
				{
					bValue = false;
				}
				NKCUtil.SetGameobjectActive(this.Guild, bValue);
				if (this.Guild != null && this.Guild.activeSelf)
				{
					this.GuildBadge.SetData(userProfileData.guildData.badgeId, !isMyTeam);
					NKCUtil.SetLabelText(this.GuildName, NKCUtilString.GetUserGuildName(userProfileData.guildData.guildName, !isMyTeam));
				}
				NKCUtil.SetGameobjectActive(this.FirstPick, bFirstPick);
				this.SetBanProgressFinished(false);
			}

			// Token: 0x0600B791 RID: 46993 RVA: 0x00367D0D File Offset: 0x00365F0D
			public void SetBanProgressFinished(bool bFinished)
			{
				NKCUtil.SetGameobjectActive(this.BanProgress, !bFinished);
				NKCUtil.SetGameobjectActive(this.BanFinished, bFinished);
			}

			// Token: 0x0400AA6C RID: 43628
			public NKCUILeagueTier TierIcon;

			// Token: 0x0400AA6D RID: 43629
			public Text TierScore;

			// Token: 0x0400AA6E RID: 43630
			public Text Level;

			// Token: 0x0400AA6F RID: 43631
			public Text Name;

			// Token: 0x0400AA70 RID: 43632
			public GameObject Guild;

			// Token: 0x0400AA71 RID: 43633
			public NKCUIGuildBadge GuildBadge;

			// Token: 0x0400AA72 RID: 43634
			public Text GuildName;

			// Token: 0x0400AA73 RID: 43635
			public GameObject FirstPick;

			// Token: 0x0400AA74 RID: 43636
			public GameObject BanProgress;

			// Token: 0x0400AA75 RID: 43637
			public GameObject BanFinished;
		}
	}
}
