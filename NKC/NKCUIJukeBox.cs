using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKC.Office;
using NKC.UI.Office;
using NKM;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009AD RID: 2477
	public class NKCUIJukeBox : NKCUIBase
	{
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x002159D8 File Offset: 0x00213BD8
		public static NKCUIJukeBox Instance
		{
			get
			{
				if (NKCUIJukeBox.m_Instance == null)
				{
					NKCUIJukeBox.m_Instance = NKCUIManager.OpenNewInstance<NKCUIJukeBox>("ab_ui_bgm", "AB_UI_BGM", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIJukeBox.CleanupInstance)).GetInstance<NKCUIJukeBox>();
					NKCUIJukeBox.m_Instance.Init();
				}
				return NKCUIJukeBox.m_Instance;
			}
		}

		// Token: 0x0600677E RID: 26494 RVA: 0x00215A27 File Offset: 0x00213C27
		private static void CleanupInstance()
		{
			NKCUIJukeBox.m_Instance = null;
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x0600677F RID: 26495 RVA: 0x00215A2F File Offset: 0x00213C2F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x00215A32 File Offset: 0x00213C32
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_JUKEBOX_TITLE;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06006781 RID: 26497 RVA: 0x00215A39 File Offset: 0x00213C39
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.BackButtonOnly;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06006782 RID: 26498 RVA: 0x00215A3C File Offset: 0x00213C3C
		public static bool IsHasInstance
		{
			get
			{
				return NKCUIJukeBox.m_Instance != null;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06006783 RID: 26499 RVA: 0x00215A49 File Offset: 0x00213C49
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIJukeBox.m_Instance != null && NKCUIJukeBox.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x00215A64 File Offset: 0x00213C64
		public override void CloseInternal()
		{
			this.ClearSlots();
			this.AlreadyJukeBoxMode = NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode;
			NKCUIManager.GetNKCUIPowerSaveMode().SetFinishJukeBox(false);
			NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetJukeBoxMode(false);
			NKCSoundManager.StopAllSound();
			if (!this.m_bLobbyMusicSelectMode)
			{
				NKCSoundManager.StopMusic();
				NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
			}
			if (this.SaveAllSongList() && NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice.GetInstance().UpdateAlarm();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x00215AF0 File Offset: 0x00213CF0
		private void Init()
		{
			NKCUtil.SetHotkey(this.m_csbtnPrev, HotkeyEventType.Left, null, false);
			NKCUtil.SetHotkey(this.m_csbtnNext, HotkeyEventType.Right, null, false);
			NKCUtil.SetHotkey(this.m_ctglPlayOrPause, HotkeyEventType.Confirm, null, false);
			if (null != this.m_sdProgressBar)
			{
				if (this.m_sdProgressBar.minValue != 0f || this.m_sdProgressBar.maxValue != 1f)
				{
					Debug.LogError("[JukeBox:Init]Error - Not Matched Slider Value : 0 to 1");
				}
				this.m_sdProgressBar.value = 0f;
			}
			if (NKMTempletContainer<NKCBGMInfoTemplet>.Values.ToList<NKCBGMInfoTemplet>().Count <= 0)
			{
				Debug.LogError("[JukeBox:Init]Error - NKCBGMInfoTempletis empty");
			}
			NKCUtil.SetEventTriggerDelegate(this.m_sdEvTrigger, delegate(BaseEventData data)
			{
				this.OnBeginDrag(data);
			}, EventTriggerType.BeginDrag, true);
			NKCUtil.SetEventTriggerDelegate(this.m_sdEvTrigger, delegate(BaseEventData data)
			{
				this.OnDrag(data);
			}, EventTriggerType.Drag, false);
			NKCUtil.SetEventTriggerDelegate(this.m_sdEvTrigger, delegate(BaseEventData data)
			{
				this.EndDragDrag(data);
			}, EventTriggerType.EndDrag, false);
			NKCUtil.SetEventTriggerDelegate(this.m_sdEvTrigger, delegate(BaseEventData data)
			{
				this.OnSilderClick(data);
			}, EventTriggerType.PointerClick, false);
			if (null != this.m_LoopScroll)
			{
				this.m_LoopScroll.dOnGetObject += this.GetSlot;
				this.m_LoopScroll.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScroll.dOnProvideData += this.ProvideSlot;
				this.m_LoopScroll.ContentConstraintCount = 1;
				this.m_LoopScroll.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScroll, null);
			}
			NKCUtil.SetBindFunction(this.m_csbtnClear, new UnityAction(this.OnClickClearLobbyMusic));
			NKCUtil.SetBindFunction(this.m_csbtnSaveLobbyMusic, new UnityAction(this.OnClickSaveLobbyMusic));
			NKCUtil.SetBindFunction(this.m_csbtnSaveMode, new UnityAction(this.OnClickSaveMode));
			NKCUtil.SetBindFunction(this.m_csbtnRepeat, new UnityAction(this.OnClickRepeat));
			NKCUtil.SetBindFunction(this.m_csbtnPrev, new UnityAction(this.OnClickPrevMusic));
			NKCUtil.SetBindFunction(this.m_csbtnNext, new UnityAction(this.OnClickNextMusic));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglTotalList, new UnityAction<bool>(this.OnClickTotalListToggle));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglFavoriteList, new UnityAction<bool>(this.OnClickFavoriteListToggle));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglFavorite, new UnityAction<bool>(this.OnClickFavorite));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglPlayOrPause, new UnityAction<bool>(this.OnClickPlayToggle));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglRandom, new UnityAction<bool>(this.OnClickRandomToggle));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglSortDefault, new UnityAction<bool>(this.OnClickSortToggle));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglSortName, new UnityAction<bool>(this.OnClickSortToggleName));
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x00215D97 File Offset: 0x00213F97
		public void Open(bool bLobbyMusicSelectMode, int selectedMusicID = 0, NKCUIJukeBox.OnMusicSelected onMusicSelected = null)
		{
			this.m_bLobbyMusicSelectMode = bLobbyMusicSelectMode;
			this.dOnMusicSelected = onMusicSelected;
			this.AlreadyJukeBoxMode = false;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCSoundManager.StopAllSound();
			NKCSoundManager.StopMusic();
			this.OnceUpdate(selectedMusicID, bLobbyMusicSelectMode);
			base.UIOpened(true);
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06006787 RID: 26503 RVA: 0x00215DD3 File Offset: 0x00213FD3
		private List<NKCBGMInfoTemplet> lstBGMInfoTemplet
		{
			get
			{
				return NKMTempletContainer<NKCBGMInfoTemplet>.Values.ToList<NKCBGMInfoTemplet>();
			}
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x00215DE0 File Offset: 0x00213FE0
		private void OnceUpdate(int selectedMusicID, bool bInLobby)
		{
			this.m_lstBGMData = this.lstBGMInfoTemplet;
			this.m_lstBGMData.Sort((NKCBGMInfoTemplet x, NKCBGMInfoTemplet y) => x.m_OrderIdx.CompareTo(y.m_OrderIdx));
			this.m_ctglSortDefault.Select(true, true, false);
			this.RefreshSlots();
			this.UpdateNewBGMList();
			int num = selectedMusicID;
			if (num == 0)
			{
				List<NKCBGMInfoTemplet> list = NKMTempletContainer<NKCBGMInfoTemplet>.Values.ToList<NKCBGMInfoTemplet>();
				list.Sort((NKCBGMInfoTemplet x, NKCBGMInfoTemplet y) => x.m_OrderIdx.CompareTo(y.m_OrderIdx));
				foreach (NKCBGMInfoTemplet nkcbgminfoTemplet in list)
				{
					if (NKCUIJukeBox.CheckOpenCond(nkcbgminfoTemplet))
					{
						num = nkcbgminfoTemplet.Key;
						break;
					}
				}
			}
			this.m_fCurPlayTime = 0f;
			this.OnSelectedMusic(num);
			this.m_curListType = NKCUIJukeBox.LIST_TYPE.TOTAL;
			this.m_ctglRandom.Select(false, true, false);
			this.m_curRepeatStatus = NKCUIJukeBox.REPEAT_STATUS.NONE;
			this.UpdateRepeatUI();
			this.m_curMusicStatus = NKCUIJukeBox.MUSIC_STATUS.PAUSE;
			this.m_ctglPlayOrPause.Select(false, true, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnClear, bInLobby);
			NKCUtil.SetGameobjectActive(this.m_csbtnSaveLobbyMusic, bInLobby);
			NKCUtil.SetGameobjectActive(this.m_csbtnSaveMode, !bInLobby);
			NKCUtil.SetGameobjectActive(this.m_ctglRandom, !bInLobby);
			NKCUtil.SetGameobjectActive(this.m_csbtnRepeat, !bInLobby);
			this.m_ctglTotalList.Select(true, true, false);
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x00215F5C File Offset: 0x0021415C
		private bool SaveAllSongList()
		{
			List<int> list = this.LoadOpendSongList();
			List<int> list2 = new List<int>();
			foreach (NKCBGMInfoTemplet nkcbgminfoTemplet in NKMTempletContainer<NKCBGMInfoTemplet>.Values)
			{
				if (NKCUIJukeBox.CheckOpenCond(nkcbgminfoTemplet))
				{
					list2.Add(nkcbgminfoTemplet.Key);
				}
			}
			this.SaveOpendBGMList(list2);
			return list != null && list.Count < list2.Count;
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x00215FDC File Offset: 0x002141DC
		private List<int> LoadOpendSongList()
		{
			List<int> list = new List<int>();
			List<int> list2 = NKCUIJukeBox.LoadAlreadyOpendBGMList();
			foreach (NKCBGMInfoTemplet nkcbgminfoTemplet in this.m_lstBGMData)
			{
				if (NKCUIJukeBox.CheckOpenCond(nkcbgminfoTemplet) && !list2.Contains(nkcbgminfoTemplet.Key))
				{
					list.Add(nkcbgminfoTemplet.Key);
				}
			}
			return list;
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x00216058 File Offset: 0x00214258
		private void UpdateNewBGMList()
		{
			if (this.m_lstBGMData == null)
			{
				return;
			}
			this.m_lstNewOpendBGMKeys = this.LoadOpendSongList();
			NKCUtil.SetGameobjectActive(this.m_objNewRedDot, this.m_lstNewOpendBGMKeys.Count > 0);
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x00216088 File Offset: 0x00214288
		private static List<int> LoadAlreadyOpendBGMList()
		{
			List<int> list = new List<int>();
			string userKey = NKCUIJukeBox.GetUserKey(false);
			if (PlayerPrefs.HasKey(userKey))
			{
				string[] array = PlayerPrefs.GetString(userKey).Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					int num;
					int.TryParse(array[i], out num);
					NKCBGMInfoTemplet nkcbgminfoTemplet = NKMTempletContainer<NKCBGMInfoTemplet>.Find(num);
					if (nkcbgminfoTemplet != null)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x002160F0 File Offset: 0x002142F0
		public static bool HasNewMusic()
		{
			List<int> list = NKCUIJukeBox.LoadAlreadyOpendBGMList();
			foreach (NKCBGMInfoTemplet nkcbgminfoTemplet in NKMTempletContainer<NKCBGMInfoTemplet>.Values)
			{
				if (!list.Contains(nkcbgminfoTemplet.Key) && NKCUIJukeBox.CheckOpenCond(nkcbgminfoTemplet))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x00216158 File Offset: 0x00214358
		private void SaveOpendBGMList(List<int> lstMusic)
		{
			string userKey = NKCUIJukeBox.GetUserKey(false);
			if (lstMusic != null && lstMusic.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int num in lstMusic)
				{
					stringBuilder.Append(string.Format("{0},", num));
				}
				PlayerPrefs.SetString(userKey, stringBuilder.ToString());
				return;
			}
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x002161DC File Offset: 0x002143DC
		private static string GetUserKey(bool bFavorite)
		{
			long userUID = NKCScenManager.CurrentUserData().m_UserUID;
			if (bFavorite)
			{
				return string.Format("JukeF_" + userUID.ToString(), Array.Empty<object>());
			}
			return string.Format("Juke_" + userUID.ToString(), Array.Empty<object>());
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x00216230 File Offset: 0x00214430
		private void SaveFavoriteList(List<int> lstMusic)
		{
			string userKey = NKCUIJukeBox.GetUserKey(true);
			if (lstMusic != null && lstMusic.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int num in lstMusic)
				{
					stringBuilder.Append(string.Format("{0},", num));
				}
				PlayerPrefs.SetString(userKey, stringBuilder.ToString());
				return;
			}
			PlayerPrefs.DeleteKey(userKey);
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x002162BC File Offset: 0x002144BC
		private List<int> LoadFavoriteList()
		{
			List<int> list = new List<int>();
			string userKey = NKCUIJukeBox.GetUserKey(true);
			if (!string.IsNullOrEmpty(userKey) && PlayerPrefs.HasKey(userKey))
			{
				string[] array = PlayerPrefs.GetString(userKey).Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					int num;
					int.TryParse(array[i], out num);
					NKCBGMInfoTemplet nkcbgminfoTemplet = NKMTempletContainer<NKCBGMInfoTemplet>.Find(num);
					if (nkcbgminfoTemplet != null)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x0021632C File Offset: 0x0021452C
		private void RefreshSlots()
		{
			this.m_LoopScroll.TotalCount = this.m_lstBGMData.Count;
			this.m_LoopScroll.PrepareCells(0);
			this.m_LoopScroll.RefreshCells(true);
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_lstBGMData.Count <= 0);
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x00216384 File Offset: 0x00214584
		private RectTransform GetSlot(int index)
		{
			NKCJukeBoxSlot nkcjukeBoxSlot = UnityEngine.Object.Instantiate<NKCJukeBoxSlot>(this.m_pfbJukeBoxSlot);
			nkcjukeBoxSlot.transform.localScale = Vector3.one;
			this.m_lstJukeBoxSlots.Add(nkcjukeBoxSlot);
			return nkcjukeBoxSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x002163BF File Offset: 0x002145BF
		private void ReturnSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x002163C8 File Offset: 0x002145C8
		private void ProvideSlot(Transform tr, int idx)
		{
			NKCJukeBoxSlot component = tr.GetComponent<NKCJukeBoxSlot>();
			if (component != null)
			{
				List<int> list = this.LoadFavoriteList();
				tr.SetParent(this.m_LoopScroll.content);
				component.Init();
				component.SetData(this.m_lstBGMData[idx], new NKCJukeBoxSlot.OnClick(this.OnClickJukeBoxSlot), list.Contains(this.m_lstBGMData[idx].Key), NKCUIJukeBox.CheckOpenCond(this.m_lstBGMData[idx]), this.m_lstNewOpendBGMKeys.Contains(this.m_lstBGMData[idx].Key));
				if (this.m_CurBGMInfoTemplet != null && this.m_lstBGMData[idx].Key == this.m_CurBGMInfoTemplet.Key)
				{
					component.SetStat(true);
				}
				NKCUtil.SetGameobjectActive(tr.gameObject, true);
			}
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x002164A4 File Offset: 0x002146A4
		private void ClearSlots()
		{
			for (int i = 0; i < this.m_lstJukeBoxSlots.Count; i++)
			{
				if (this.m_lstJukeBoxSlots[i] != null)
				{
					UnityEngine.Object.DestroyImmediate(this.m_lstJukeBoxSlots[i].gameObject);
					this.m_lstJukeBoxSlots[i] = null;
				}
			}
			this.m_lstJukeBoxSlots.Clear();
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x0021650C File Offset: 0x0021470C
		private void OnSelectedMusic(int idx)
		{
			NKCBGMInfoTemplet nkcbgminfoTemplet = this.m_lstBGMData.Find((NKCBGMInfoTemplet e) => e.Key == idx);
			if (nkcbgminfoTemplet == null)
			{
				return;
			}
			this.m_CurBGMInfoTemplet = nkcbgminfoTemplet;
			this.UpdateAlbumUI();
			this.UpdateSlotUI();
			AudioClip audioClip = NKCUIJukeBox.GetAudioClip(this.m_CurBGMInfoTemplet.m_BgmAssetID);
			if (null != audioClip)
			{
				this.m_fCurTotalTime = audioClip.length;
				this.UpdatePlayTimeUI();
			}
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x00216581 File Offset: 0x00214781
		public void OnClickJukeBoxSlot(int idx)
		{
			if (this.m_CurBGMInfoTemplet != null && this.m_CurBGMInfoTemplet.Key == idx)
			{
				return;
			}
			this.OnSelectedMusic(idx);
			this.PlayMusic();
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x002165A7 File Offset: 0x002147A7
		private void OnBeginDrag(BaseEventData evtData)
		{
			this.PauseMusic();
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x002165AF File Offset: 0x002147AF
		public void OnDrag(BaseEventData evtData)
		{
			this.UpdateMusicTime(this.m_sdProgressBar.value);
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x002165C2 File Offset: 0x002147C2
		public void EndDragDrag(BaseEventData evtData)
		{
			if (this.m_ctglPlayOrPause.m_bSelect)
			{
				this.PlayMusic();
			}
			this.UpdateMusicTime(this.m_sdProgressBar.value);
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x002165E8 File Offset: 0x002147E8
		private void OnSilderClick(BaseEventData evtData)
		{
			this.UpdateMusicTime(this.m_sdProgressBar.value);
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x002165FB File Offset: 0x002147FB
		private void OnClickClearLobbyMusic()
		{
			base.Close();
			NKCUIJukeBox.OnMusicSelected onMusicSelected = this.dOnMusicSelected;
			if (onMusicSelected == null)
			{
				return;
			}
			onMusicSelected(0);
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x00216614 File Offset: 0x00214814
		private void OnClickSaveLobbyMusic()
		{
			if (this.m_CurBGMInfoTemplet == null)
			{
				return;
			}
			base.Close();
			NKCUIJukeBox.OnMusicSelected onMusicSelected = this.dOnMusicSelected;
			if (onMusicSelected == null)
			{
				return;
			}
			onMusicSelected(this.m_CurBGMInfoTemplet.Key);
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x00216640 File Offset: 0x00214840
		private void OnClickSaveMode()
		{
			if (this.m_curMusicStatus == NKCUIJukeBox.MUSIC_STATUS.PAUSE)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_JUKEBOX_BLOCK_SLEEP_MODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIManager.GetNKCUIPowerSaveMode().SetFinishJukeBox(false);
			NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetJukeBoxMode(true);
			this.UpdateJukeBoxTitle();
			NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetEnable(true);
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x0021669A File Offset: 0x0021489A
		private void UpdateJukeBoxTitle()
		{
			if (!NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode)
			{
				return;
			}
			if (this.m_CurBGMInfoTemplet != null)
			{
				NKCUIManager.GetNKCUIPowerSaveMode().SetJukeBoxTitle(NKCStringTable.GetString(this.m_CurBGMInfoTemplet.m_BgmNameStringID, false));
			}
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x002166D4 File Offset: 0x002148D4
		private void OnClickRepeat()
		{
			switch (this.m_curRepeatStatus)
			{
			case NKCUIJukeBox.REPEAT_STATUS.NONE:
				this.m_curRepeatStatus = NKCUIJukeBox.REPEAT_STATUS.TOTAL_REPEAT;
				break;
			case NKCUIJukeBox.REPEAT_STATUS.TOTAL_REPEAT:
				this.m_curRepeatStatus = NKCUIJukeBox.REPEAT_STATUS.SINGLE_REPEAT;
				break;
			case NKCUIJukeBox.REPEAT_STATUS.SINGLE_REPEAT:
				this.m_curRepeatStatus = NKCUIJukeBox.REPEAT_STATUS.NONE;
				break;
			}
			this.UpdateRepeatUI();
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x0021671C File Offset: 0x0021491C
		private void OnClickPrevMusic()
		{
			int playMusicID = this.GetPlayMusicID(this.m_CurBGMInfoTemplet.Key, false);
			if (playMusicID > 0)
			{
				this.OnSelectedMusic(playMusicID);
				if (this.m_ctglPlayOrPause.m_bSelect)
				{
					this.PlayMusic();
					return;
				}
			}
			else
			{
				this.m_ctglPlayOrPause.Select(false, true, false);
				this.PauseMusic();
			}
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x00216770 File Offset: 0x00214970
		private void OnClickNextMusic()
		{
			int playMusicID = this.GetPlayMusicID(this.m_CurBGMInfoTemplet.Key, true);
			if (playMusicID > 0)
			{
				this.OnSelectedMusic(playMusicID);
				if (this.m_ctglPlayOrPause.m_bSelect)
				{
					this.PlayMusic();
					return;
				}
			}
			else
			{
				this.m_ctglPlayOrPause.Select(false, true, false);
				this.PauseMusic();
			}
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x002167C4 File Offset: 0x002149C4
		private int GetPlayMusicID(int iStartKey, bool bNext = true)
		{
			bool flag = false;
			if (bNext)
			{
				int i = 0;
				while (i < this.m_lstBGMData.Count)
				{
					if (this.m_lstBGMData[i].Key == iStartKey)
					{
						if (this.m_lstBGMData.Count <= i + 1)
						{
							if (this.m_curRepeatStatus != NKCUIJukeBox.REPEAT_STATUS.TOTAL_REPEAT)
							{
								return -1;
							}
							if (NKCUIJukeBox.CheckOpenCond(this.m_lstBGMData[0]))
							{
								return this.m_lstBGMData[0].Key;
							}
							return this.GetPlayMusicID(this.m_lstBGMData[0].Key, bNext);
						}
						else
						{
							if (NKCUIJukeBox.CheckOpenCond(this.m_lstBGMData[i + 1]))
							{
								return this.m_lstBGMData[i + 1].Key;
							}
							return this.GetPlayMusicID(this.m_lstBGMData[i + 1].Key, bNext);
						}
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				int j = 0;
				while (j < this.m_lstBGMData.Count)
				{
					if (this.m_lstBGMData[j].Key == iStartKey)
					{
						if (j == 0)
						{
							if (NKCUIJukeBox.CheckOpenCond(this.m_lstBGMData[this.m_lstBGMData.Count - 1]))
							{
								return this.m_lstBGMData[this.m_lstBGMData.Count - 1].Key;
							}
							return this.GetPlayMusicID(this.m_lstBGMData[this.m_lstBGMData.Count - 1].Key, bNext);
						}
						else
						{
							if (NKCUIJukeBox.CheckOpenCond(this.m_lstBGMData[j - 1]))
							{
								return this.m_lstBGMData[j - 1].Key;
							}
							return this.GetPlayMusicID(this.m_lstBGMData[j - 1].Key, bNext);
						}
					}
					else
					{
						j++;
					}
				}
			}
			if (this.m_curListType == NKCUIJukeBox.LIST_TYPE.FAVORITE && !flag && this.m_lstBGMData.Count > 0)
			{
				return this.m_lstBGMData[0].Key;
			}
			return -1;
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x002169BC File Offset: 0x00214BBC
		private static bool CheckOpenCond(NKCBGMInfoTemplet bgmTemplet)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			if (nkmuserData.IsSuperUser())
			{
				return true;
			}
			switch (bgmTemplet.m_UnlockCond)
			{
			case NKC_BGM_COND.COLLECT_BACKGRUND:
			{
				NKMInventoryData inventoryData = nkmuserData.m_InventoryData;
				using (List<int>.Enumerator enumerator = bgmTemplet.m_UnlockCondValue1.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int itemID = enumerator.Current;
						if (bgmTemplet.bAllCollecte && inventoryData.GetCountMiscItem(itemID) <= 0L)
						{
							return false;
						}
						if (!bgmTemplet.bAllCollecte && inventoryData.GetCountMiscItem(itemID) > 0L)
						{
							return true;
						}
					}
					goto IL_31E;
				}
				break;
			}
			case NKC_BGM_COND.COLLECT_SKIN:
				goto IL_1A5;
			case NKC_BGM_COND.COLLECT_UNIT:
				break;
			case NKC_BGM_COND.STAGE_CLEAR_TOTAL_CNT:
				goto IL_122;
			case NKC_BGM_COND.COLLECT_ITEM_MISC:
				goto IL_216;
			case NKC_BGM_COND.COLLECT_ITEM_INTERIOR:
				goto IL_28B;
			default:
				goto IL_2F7;
			}
			NKMArmyData armyData = nkmuserData.m_ArmyData;
			using (List<int>.Enumerator enumerator = bgmTemplet.m_UnlockCondValue1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int unitID = enumerator.Current;
					if (bgmTemplet.bAllCollecte && armyData.IsFirstGetUnit(unitID))
					{
						return false;
					}
					if (!bgmTemplet.bAllCollecte && !armyData.IsFirstGetUnit(unitID))
					{
						return true;
					}
				}
				goto IL_31E;
			}
			IL_122:
			using (List<int>.Enumerator enumerator = bgmTemplet.m_UnlockCondValue1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int stageID = enumerator.Current;
					if (nkmuserData.CheckStageCleared(stageID))
					{
						if (bgmTemplet.bAllCollecte && nkmuserData.GetStatePlayCnt(stageID, false, true, true) < bgmTemplet.m_UnlockCondValue2)
						{
							return false;
						}
						if (!bgmTemplet.bAllCollecte && nkmuserData.GetStatePlayCnt(stageID, false, true, true) >= bgmTemplet.m_UnlockCondValue2)
						{
							return true;
						}
					}
				}
				goto IL_31E;
			}
			IL_1A5:
			using (List<int>.Enumerator enumerator = bgmTemplet.m_UnlockCondValue1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int skinID = enumerator.Current;
					if (bgmTemplet.bAllCollecte && !nkmuserData.m_InventoryData.HasItemSkin(skinID))
					{
						return false;
					}
					if (!bgmTemplet.bAllCollecte && nkmuserData.m_InventoryData.HasItemSkin(skinID))
					{
						return true;
					}
				}
				goto IL_31E;
			}
			IL_216:
			NKMInventoryData inventoryData2 = nkmuserData.m_InventoryData;
			using (List<int>.Enumerator enumerator = bgmTemplet.m_UnlockCondValue1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int itemID2 = enumerator.Current;
					if (bgmTemplet.bAllCollecte && inventoryData2.GetCountMiscItem(itemID2) < 0L)
					{
						return false;
					}
					if (!bgmTemplet.bAllCollecte && inventoryData2.GetCountMiscItem(itemID2) > 0L)
					{
						return true;
					}
				}
				goto IL_31E;
			}
			IL_28B:
			NKCOfficeData officeData = nkmuserData.OfficeData;
			using (List<int>.Enumerator enumerator = bgmTemplet.m_UnlockCondValue1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int itemID3 = enumerator.Current;
					if (bgmTemplet.bAllCollecte && officeData.GetInteriorCount(itemID3) < 0L)
					{
						return false;
					}
					if (!bgmTemplet.bAllCollecte && officeData.GetInteriorCount(itemID3) > 0L)
					{
						return true;
					}
				}
				goto IL_31E;
			}
			IL_2F7:
			Debug.Log(string.Format("<color=red>unknow unlock cond type : {0}, key : {1}</color>", bgmTemplet.m_UnlockCond, bgmTemplet.Key));
			return false;
			IL_31E:
			return bgmTemplet.bAllCollecte;
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x00216D44 File Offset: 0x00214F44
		private void OnClickRandomToggle(bool bSet)
		{
			this.UpdateMusicSlotList();
			this.OnSelectedMusic(this.m_CurBGMInfoTemplet.Key);
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x00216D60 File Offset: 0x00214F60
		private void OnClickSortToggle(bool bSet)
		{
			if (bSet)
			{
				this.m_lstBGMData = this.lstBGMInfoTemplet;
				this.m_lstBGMData.Sort((NKCBGMInfoTemplet x, NKCBGMInfoTemplet y) => x.m_OrderIdx.CompareTo(y.m_OrderIdx));
				this.RefreshSlots();
			}
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x00216DAC File Offset: 0x00214FAC
		private void OnClickSortToggleName(bool bSet)
		{
			if (bSet)
			{
				this.m_lstBGMData = this.lstBGMInfoTemplet;
				this.m_lstBGMData.Sort((NKCBGMInfoTemplet x, NKCBGMInfoTemplet y) => NKCStringTable.GetString(x.m_BgmNameStringID, false).CompareTo(NKCStringTable.GetString(y.m_BgmNameStringID, false)));
				this.RefreshSlots();
			}
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x00216DF8 File Offset: 0x00214FF8
		private void UpdateMusicTime(float fValue)
		{
			NKCSoundManager.SetChangeMusicTime(fValue);
			this.m_fCurPlayTime = this.m_fCurTotalTime * fValue;
			this.UpdatePlayTimeUI();
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x00216E14 File Offset: 0x00215014
		private void OnClickTotalListToggle(bool bSet)
		{
			if (bSet)
			{
				this.m_curListType = NKCUIJukeBox.LIST_TYPE.TOTAL;
				this.UpdateMusicSlotList();
			}
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x00216E26 File Offset: 0x00215026
		private void OnClickFavoriteListToggle(bool bSet)
		{
			if (bSet)
			{
				this.m_curListType = NKCUIJukeBox.LIST_TYPE.FAVORITE;
				this.UpdateMusicSlotList();
			}
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x00216E38 File Offset: 0x00215038
		private void UpdateMusicSlotList()
		{
			if (this.m_curListType == NKCUIJukeBox.LIST_TYPE.TOTAL)
			{
				if (this.m_ctglRandom.m_bSelect)
				{
					List<NKCBGMInfoTemplet> list = new List<NKCBGMInfoTemplet>();
					list.Add(this.m_CurBGMInfoTemplet);
					this.m_lstBGMData = this.lstBGMInfoTemplet;
					this.m_lstBGMData.Remove(this.m_CurBGMInfoTemplet);
					list.AddRange((from g in this.m_lstBGMData
					orderby Guid.NewGuid()
					select g).ToList<NKCBGMInfoTemplet>());
					this.m_lstBGMData = list;
				}
				else
				{
					this.m_lstBGMData = this.lstBGMInfoTemplet;
				}
			}
			if (this.m_curListType == NKCUIJukeBox.LIST_TYPE.FAVORITE)
			{
				List<NKCBGMInfoTemplet> list2 = new List<NKCBGMInfoTemplet>();
				foreach (int key in this.LoadFavoriteList())
				{
					NKCBGMInfoTemplet nkcbgminfoTemplet = NKMTempletContainer<NKCBGMInfoTemplet>.Find(key);
					if (nkcbgminfoTemplet != null)
					{
						list2.Add(nkcbgminfoTemplet);
					}
				}
				if (this.m_ctglRandom.m_bSelect)
				{
					this.m_lstBGMData = (from g in list2
					orderby Guid.NewGuid()
					select g).ToList<NKCBGMInfoTemplet>();
				}
				else
				{
					this.m_lstBGMData = list2;
				}
			}
			if (this.m_lstBGMData.Count <= 0)
			{
				Debug.Log(string.Format("<color=red>m_curListType : {0} 리스트가 0</color>", this.m_curListType));
			}
			this.RefreshSlots();
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x00216FAC File Offset: 0x002151AC
		private void OnClickFavorite(bool bSet)
		{
			List<int> list = this.LoadFavoriteList();
			if (bSet)
			{
				if (!list.Contains(this.m_CurBGMInfoTemplet.Key))
				{
					list.Add(this.m_CurBGMInfoTemplet.Key);
				}
			}
			else if (list.Contains(this.m_CurBGMInfoTemplet.Key))
			{
				list.Remove(this.m_CurBGMInfoTemplet.Key);
			}
			this.SaveFavoriteList(list);
			foreach (NKCJukeBoxSlot nkcjukeBoxSlot in this.m_lstJukeBoxSlots)
			{
				if (!(null == nkcjukeBoxSlot))
				{
					nkcjukeBoxSlot.SetFavorite(list.Contains(nkcjukeBoxSlot.Index));
				}
			}
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x00217070 File Offset: 0x00215270
		private void OnClickPlayToggle(bool bSet)
		{
			if (bSet)
			{
				this.PlayMusic();
				float value = this.m_sdProgressBar.value;
				this.UpdateMusicTime(value);
				return;
			}
			this.PauseMusic();
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x002170A0 File Offset: 0x002152A0
		private void PauseMusic()
		{
			NKCSoundManager.StopMusic();
			this.m_curMusicStatus = NKCUIJukeBox.MUSIC_STATUS.PAUSE;
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode)
			{
				NKCUIManager.GetNKCUIPowerSaveMode().SetFinishJukeBox(true);
			}
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x002170CC File Offset: 0x002152CC
		private void UpdateAlbumUI()
		{
			if (this.m_CurBGMInfoTemplet == null)
			{
				return;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_bgm_albumcover", this.m_CurBGMInfoTemplet.m_BgmCoverIMGID, false);
			NKCUtil.SetImageSprite(this.m_imgCover, orLoadAssetResource, false);
			List<int> list = this.LoadFavoriteList();
			this.m_ctglFavorite.Select(list.Contains(this.m_CurBGMInfoTemplet.Key), true, false);
			NKCUtil.SetLabelText(this.m_lbBGMTitle, NKCStringTable.GetString(this.m_CurBGMInfoTemplet.m_BgmNameStringID, false));
			AudioClip audioClip = NKCUIJukeBox.GetAudioClip(this.m_CurBGMInfoTemplet.m_BgmAssetID);
			if (audioClip == null)
			{
				return;
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)audioClip.length);
			NKCUtil.SetLabelText(this.m_lbPlayTime, string.Format("{0:00}:{1:00}", 0, 0));
			NKCUtil.SetLabelText(this.m_lbTotalTime, string.Format("-{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds));
			this.m_sdProgressBar.value = 0f;
			this.UpdateJukeBoxTitle();
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x002171D4 File Offset: 0x002153D4
		private void UpdateSlotUI()
		{
			if (this.m_CurBGMInfoTemplet == null)
			{
				return;
			}
			int num = this.m_lstBGMData.FindIndex((NKCBGMInfoTemplet e) => e.Key == this.m_CurBGMInfoTemplet.Key);
			if (num >= 0)
			{
				if (num == 0 || num == this.m_lstBGMData.Count - 1)
				{
					this.m_LoopScroll.SetIndexPosition(num);
				}
				else
				{
					LoopVerticalScrollRect loopScroll = this.m_LoopScroll;
					if (loopScroll != null)
					{
						loopScroll.ScrollToCell(num, 0.4f, LoopScrollRect.ScrollTarget.Center, null);
					}
				}
			}
			foreach (NKCJukeBoxSlot nkcjukeBoxSlot in this.m_lstJukeBoxSlots)
			{
				nkcjukeBoxSlot.SetStat(nkcjukeBoxSlot.Index == this.m_CurBGMInfoTemplet.Key);
			}
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x00217298 File Offset: 0x00215498
		private void UpdateRepeatUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objRepeatSingle, this.m_curRepeatStatus == NKCUIJukeBox.REPEAT_STATUS.SINGLE_REPEAT);
			NKCUtil.SetGameobjectActive(this.m_objRepeatTotal, this.m_curRepeatStatus == NKCUIJukeBox.REPEAT_STATUS.TOTAL_REPEAT);
			NKCUtil.SetGameobjectActive(this.m_objRepeatNormal, this.m_curRepeatStatus == NKCUIJukeBox.REPEAT_STATUS.NONE);
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x002172D8 File Offset: 0x002154D8
		private void UpdatePlayTimeUI()
		{
			if (this.m_curMusicStatus == NKCUIJukeBox.MUSIC_STATUS.PLAY)
			{
				this.m_fCurPlayTime = NKCSoundManager.GetMusicTime();
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)this.m_fCurPlayTime);
			TimeSpan timeSpan2 = TimeSpan.FromSeconds((double)(this.m_fCurTotalTime - this.m_fCurPlayTime));
			NKCUtil.SetLabelText(this.m_lbPlayTime, string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds));
			NKCUtil.SetLabelText(this.m_lbTotalTime, string.Format("-{0:00}:{1:00}", timeSpan2.Minutes, timeSpan2.Seconds));
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x00217374 File Offset: 0x00215574
		private void Update()
		{
			if (this.m_curMusicStatus == NKCUIJukeBox.MUSIC_STATUS.PLAY)
			{
				this.m_fCurPlayTime = NKCSoundManager.GetMusicTime();
				this.m_sdProgressBar.value = this.m_fCurPlayTime / this.m_fCurTotalTime;
				this.UpdatePlayTimeUI();
				if (this.m_fCurTotalTime - this.m_fTimeOffSet <= this.m_fCurPlayTime)
				{
					NKCUIJukeBox.REPEAT_STATUS curRepeatStatus = this.m_curRepeatStatus;
					if (curRepeatStatus > NKCUIJukeBox.REPEAT_STATUS.TOTAL_REPEAT)
					{
						if (curRepeatStatus == NKCUIJukeBox.REPEAT_STATUS.SINGLE_REPEAT)
						{
							this.PlayMusic();
							return;
						}
					}
					else
					{
						this.OnClickNextMusic();
					}
				}
			}
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x002173E4 File Offset: 0x002155E4
		private void ShowUnit()
		{
			this.m_InGameUnitViewer.CleanUp();
			this.m_InGameUnitViewer.Prepare(null);
			this.m_InGameUnitViewer.SetPreviewBattleUnit(1135, 113516);
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x00217414 File Offset: 0x00215614
		private void PlayMusic()
		{
			if (this.m_CurBGMInfoTemplet == null)
			{
				return;
			}
			NKCSoundManager.StopMusic();
			NKCSoundManager.PlayMusic(this.m_CurBGMInfoTemplet.m_BgmAssetID, false, this.m_CurBGMInfoTemplet.BGMVolume, true, 0f, 0f);
			AudioClip audioClip = NKCUIJukeBox.GetAudioClip(this.m_CurBGMInfoTemplet.m_BgmAssetID);
			if (null != audioClip)
			{
				this.m_fCurTotalTime = audioClip.length;
			}
			this.m_fCurPlayTime = 0f;
			this.m_curMusicStatus = NKCUIJukeBox.MUSIC_STATUS.PLAY;
			this.m_ctglPlayOrPause.Select(true, true, false);
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x002174A0 File Offset: 0x002156A0
		public static AudioClip GetAudioClip(string audioClipName)
		{
			string bundleName = NKCAssetResourceManager.GetBundleName(audioClipName);
			if (string.IsNullOrEmpty(bundleName))
			{
				return null;
			}
			if (!NKCAssetResourceManager.IsBundleExists(bundleName, audioClipName))
			{
				return null;
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<AudioClip>(bundleName, audioClipName, false, null);
			if (nkcassetResourceData == null)
			{
				return null;
			}
			return nkcassetResourceData.GetAsset<AudioClip>();
		}

		// Token: 0x040053AD RID: 21421
		public const string ASSET_BUNDLE_NAME = "ab_ui_bgm";

		// Token: 0x040053AE RID: 21422
		public const string UI_ASSET_NAME = "AB_UI_BGM";

		// Token: 0x040053AF RID: 21423
		private static NKCUIJukeBox m_Instance;

		// Token: 0x040053B0 RID: 21424
		public bool AlreadyJukeBoxMode;

		// Token: 0x040053B1 RID: 21425
		[Header("Top Menu")]
		public GameObject m_objNewRedDot;

		// Token: 0x040053B2 RID: 21426
		public NKCUIComToggle m_ctglTotalList;

		// Token: 0x040053B3 RID: 21427
		public NKCUIComToggle m_ctglFavoriteList;

		// Token: 0x040053B4 RID: 21428
		public NKCUIComToggle m_ctglSortDefault;

		// Token: 0x040053B5 RID: 21429
		public NKCUIComToggle m_ctglSortName;

		// Token: 0x040053B6 RID: 21430
		public LoopVerticalScrollRect m_LoopScroll;

		// Token: 0x040053B7 RID: 21431
		public GameObject m_objNone;

		// Token: 0x040053B8 RID: 21432
		[Header("Cover")]
		public GameObject m_ObjCover;

		// Token: 0x040053B9 RID: 21433
		public Image m_imgCover;

		// Token: 0x040053BA RID: 21434
		public NKCComText m_lbBGMTitle;

		// Token: 0x040053BB RID: 21435
		public NKCUIComToggle m_ctglFavorite;

		// Token: 0x040053BC RID: 21436
		[Header("Controller")]
		public Text m_lbPlayTime;

		// Token: 0x040053BD RID: 21437
		public Text m_lbTotalTime;

		// Token: 0x040053BE RID: 21438
		public Slider m_sdProgressBar;

		// Token: 0x040053BF RID: 21439
		public EventTrigger m_sdEvTrigger;

		// Token: 0x040053C0 RID: 21440
		public NKCUIComStateButton m_csbtnPrev;

		// Token: 0x040053C1 RID: 21441
		public NKCUIComStateButton m_csbtnNext;

		// Token: 0x040053C2 RID: 21442
		public NKCUIComToggle m_ctglPlayOrPause;

		// Token: 0x040053C3 RID: 21443
		public NKCUIComToggle m_ctglRandom;

		// Token: 0x040053C4 RID: 21444
		public NKCUIComStateButton m_csbtnRepeat;

		// Token: 0x040053C5 RID: 21445
		public GameObject m_objRepeatNormal;

		// Token: 0x040053C6 RID: 21446
		public GameObject m_objRepeatTotal;

		// Token: 0x040053C7 RID: 21447
		public GameObject m_objRepeatSingle;

		// Token: 0x040053C8 RID: 21448
		public NKCUIComStateButton m_csbtnClear;

		// Token: 0x040053C9 RID: 21449
		public NKCUIComStateButton m_csbtnSaveMode;

		// Token: 0x040053CA RID: 21450
		public NKCUIComStateButton m_csbtnSaveLobbyMusic;

		// Token: 0x040053CB RID: 21451
		[Header("주크박스 슬롯")]
		public NKCJukeBoxSlot m_pfbJukeBoxSlot;

		// Token: 0x040053CC RID: 21452
		private List<NKCJukeBoxSlot> m_lstJukeBoxSlots = new List<NKCJukeBoxSlot>();

		// Token: 0x040053CD RID: 21453
		private List<NKCBGMInfoTemplet> m_lstBGMData;

		// Token: 0x040053CE RID: 21454
		private NKCUIJukeBox.OnMusicSelected dOnMusicSelected;

		// Token: 0x040053CF RID: 21455
		private bool m_bLobbyMusicSelectMode;

		// Token: 0x040053D0 RID: 21456
		private List<int> m_lstNewOpendBGMKeys = new List<int>();

		// Token: 0x040053D1 RID: 21457
		private NKCBGMInfoTemplet m_CurBGMInfoTemplet;

		// Token: 0x040053D2 RID: 21458
		private NKCUIJukeBox.LIST_TYPE m_curListType;

		// Token: 0x040053D3 RID: 21459
		private NKCUIJukeBox.REPEAT_STATUS m_curRepeatStatus;

		// Token: 0x040053D4 RID: 21460
		private NKCUIJukeBox.MUSIC_STATUS m_curMusicStatus;

		// Token: 0x040053D5 RID: 21461
		public float m_fTimeOffSet = 0.02f;

		// Token: 0x040053D6 RID: 21462
		public NKCUIInGameCharacterViewer m_InGameUnitViewer;

		// Token: 0x040053D7 RID: 21463
		private float m_fCurPlayTime;

		// Token: 0x040053D8 RID: 21464
		private float m_fCurTotalTime;

		// Token: 0x02001696 RID: 5782
		// (Invoke) Token: 0x0600B0B7 RID: 45239
		public delegate void OnMusicSelected(int id);

		// Token: 0x02001697 RID: 5783
		private enum LIST_TYPE
		{
			// Token: 0x0400A4CD RID: 42189
			TOTAL,
			// Token: 0x0400A4CE RID: 42190
			FAVORITE
		}

		// Token: 0x02001698 RID: 5784
		private enum REPEAT_STATUS
		{
			// Token: 0x0400A4D0 RID: 42192
			NONE,
			// Token: 0x0400A4D1 RID: 42193
			TOTAL_REPEAT,
			// Token: 0x0400A4D2 RID: 42194
			SINGLE_REPEAT
		}

		// Token: 0x02001699 RID: 5785
		private enum MUSIC_STATUS
		{
			// Token: 0x0400A4D4 RID: 42196
			PAUSE,
			// Token: 0x0400A4D5 RID: 42197
			PLAY
		}
	}
}
