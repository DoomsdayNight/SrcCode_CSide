using System;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;
using Cs.Logging;
using NKC.Publisher;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Video;

namespace NKC.UI
{
	// Token: 0x0200097D RID: 2429
	public class NKCUICutScenPlayer : NKCUIBase, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060062AE RID: 25262 RVA: 0x001EFC95 File Offset: 0x001EDE95
		public static NKCUICutScenPlayer Instance
		{
			get
			{
				NKCUICutScenPlayer.InitiateInstance();
				return NKCUICutScenPlayer.m_Instance;
			}
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x001EFCA1 File Offset: 0x001EDEA1
		public static void InitiateInstance()
		{
			if (NKCUICutScenPlayer.m_Instance == null)
			{
				NKCUICutScenPlayer.m_Instance = NKCUIManager.OpenNewInstance<NKCUICutScenPlayer>("ab_ui_nkm_ui_cutscen", "NKM_UI_CUTSCEN_PLAYER", NKCUIManager.eUIBaseRect.UIFrontCommonLow, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUICutScenPlayer.CleanupInstance)).GetInstance<NKCUICutScenPlayer>();
				NKCUICutScenPlayer.m_Instance.InitUI();
			}
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x001EFCE0 File Offset: 0x001EDEE0
		private void OnEnable()
		{
			NKCUICutScenTalkBoxMgr.InitUI(base.gameObject);
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x001EFCED File Offset: 0x001EDEED
		private static void CleanupInstance()
		{
			NKCUICutScenPlayer.m_Instance = null;
			NKCUICutScenTalkBoxMgr.OnCleanUp();
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060062B2 RID: 25266 RVA: 0x001EFCFA File Offset: 0x001EDEFA
		public static bool HasInstance
		{
			get
			{
				return NKCUICutScenPlayer.m_Instance != null;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060062B3 RID: 25267 RVA: 0x001EFD07 File Offset: 0x001EDF07
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUICutScenPlayer.m_Instance != null && NKCUICutScenPlayer.m_Instance.IsOpen;
			}
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x001EFD22 File Offset: 0x001EDF22
		public static void CheckInstanceAndClose()
		{
			if (NKCUICutScenPlayer.m_Instance != null && NKCUICutScenPlayer.m_Instance.IsOpen)
			{
				NKCUICutScenPlayer.m_Instance.Close();
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x001EFD47 File Offset: 0x001EDF47
		private INKCUICutScenTalkBoxMgr m_NKCUICutScenTalkBoxMgr
		{
			get
			{
				return NKCUICutScenTalkBoxMgr.GetCutScenTalkBoxMgr(NKCUICutScenPlayer.m_CutScenTalkBoxMgrType);
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060062B6 RID: 25270 RVA: 0x001EFD54 File Offset: 0x001EDF54
		private float ADD_WAIT_TIME_PER_ONE_WORD_BY_LONG_TALK_FOR_AUTO
		{
			get
			{
				if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameOptionData() != null)
				{
					NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO cutscen_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = NKCScenManager.GetScenManager().GetGameOptionData().CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO;
					float num = 1f;
					if (cutscen_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO == NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO.FAST)
					{
						num = NKCClientConst.NextTalkChangeSpeedWhenAuto_Fast;
					}
					else if (cutscen_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO == NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO.NORMAL)
					{
						num = NKCClientConst.NextTalkChangeSpeedWhenAuto_Normal;
					}
					else if (cutscen_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO == NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO.SLOW)
					{
						num = NKCClientConst.NextTalkChangeSpeedWhenAuto_Slow;
					}
					return num * 0.02f;
				}
				return 0.02f;
			}
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x001EFDC0 File Offset: 0x001EDFC0
		public override void CloseInternal()
		{
			foreach (KeyValuePair<string, List<int>> keyValuePair in this.m_dicLoopSounds)
			{
				List<int> value = keyValuePair.Value;
				for (int i = 0; i < value.Count; i++)
				{
					NKCSoundManager.StopSound(value[i]);
				}
			}
			this.m_dicLoopSounds.Clear();
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.Stop();
			}
			this.m_lstLog.Clear();
			this.UnLoad();
			this.AttachToCutscenPlayer();
			NKCUIManager.SetUseFrontLowCanvas(false);
			base.gameObject.SetActive(false);
			NKCCamera.SetEnableSepiaToneSubUILowCam(false);
			this.m_cNKCUICutState.InitPerCut();
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x001EFE90 File Offset: 0x001EE090
		public override void OnCloseInstance()
		{
			this.UnLoad();
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x001EFE98 File Offset: 0x001EE098
		public override void OnBackButton()
		{
			this.StopWithCallBack();
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x001EFEA0 File Offset: 0x001EE0A0
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060062BB RID: 25275 RVA: 0x001EFEA3 File Offset: 0x001EE0A3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060062BC RID: 25276 RVA: 0x001EFEA6 File Offset: 0x001EE0A6
		public override string MenuName
		{
			get
			{
				return "컷신";
			}
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x001EFEB0 File Offset: 0x001EE0B0
		public void InitUI()
		{
			NKCUICutScenTitleMgr.InitUI(base.gameObject);
			this.m_NKCUICutScenTitleMgr = NKCUICutScenTitleMgr.GetCutScenTitleMgr();
			NKCUICutScenBGMgr.InitUI(base.gameObject);
			this.m_NKCUICutScenBGMgr = NKCUICutScenBGMgr.GetCutScenBGMgr();
			NKCUICutScenUnitMgr.InitUI(base.gameObject);
			this.m_NKCUICutScenUnitMgr = NKCUICutScenUnitMgr.GetCutScenUnitMgr();
			NKCUICutScenImgMgr.InitUI(base.gameObject);
			this.m_NKCUICutScenImgMgr = NKCUICutScenImgMgr.GetCutScenImgMgr();
			NKCUtil.SetGameobjectActive(this.m_objTopMenuParent, true);
			NKCUtil.SetGameobjectActive(this.m_logViewer.gameObject, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_PauseToggle != null)
			{
				this.m_PauseToggle.OnValueChanged.RemoveAllListeners();
				this.m_PauseToggle.OnValueChanged.AddListener(new UnityAction<bool>(this.SetPause));
			}
			this.m_LogBtn.PointerClick.RemoveAllListeners();
			this.m_LogBtn.PointerClick.AddListener(new UnityAction(this.OnClickLogBtn));
			NKCUtil.SetHotkey(this.m_LogBtn, HotkeyEventType.Up);
			if (this.m_NKM_UI_CUTSCEN_PLAYER != null)
			{
				this.m_NKM_UI_CUTSCEN_PLAYER.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_CUTSCEN_PLAYER.PointerClick.AddListener(new UnityAction(this.OnClickedPlayer));
			}
			if (this.m_NKM_UI_CUTSCEN_PLAYER_SKIP != null)
			{
				this.m_NKM_UI_CUTSCEN_PLAYER_SKIP.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_CUTSCEN_PLAYER_SKIP.PointerClick.AddListener(new UnityAction(this.OnClickedSkip));
			}
			if (this.m_NKM_UI_CUTSCEN_PLAYER_TALK_SKIP_AUTO != null)
			{
				this.m_NKM_UI_CUTSCEN_PLAYER_TALK_SKIP_AUTO.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_CUTSCEN_PLAYER_TALK_SKIP_AUTO.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedAuto));
			}
			if (this.m_aSelectionButtons != null)
			{
				for (int i = 0; i < this.m_aSelectionButtons.Length; i++)
				{
					if (!(this.m_aSelectionButtons[i] == null))
					{
						this.m_aSelectionButtons[i].m_DataInt = i;
						NKCUtil.SetButtonClickDelegate(this.m_aSelectionButtons[i], new UnityAction<int>(this.OnSelectionRoute));
					}
				}
			}
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x001F00B0 File Offset: 0x001EE2B0
		private void OnClickLogBtn()
		{
			this.m_logViewer.OpenUI(this.m_lstLog, new NKCUICutScenLogViewer.OnButton(this.OnLogViewerClose), this.m_NKCUICutScenTitleMgr.gameObject.activeInHierarchy, this.m_NKCUICutScenTalkBoxMgr.MyGameObject.activeInHierarchy, this.m_AutoToggle.m_bChecked, NKMStageTempletV2.Find(this.m_stageID));
			this.m_AutoToggle.Select(false, false, false);
			NKCUtil.SetGameobjectActive(this.m_objTopMenuParent, false);
			NKCUtil.SetGameobjectActive(this.m_NKCUICutScenTitleMgr.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_NKCUICutScenTalkBoxMgr.MyGameObject, false);
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x001F0150 File Offset: 0x001EE350
		private void AddLog(NKCCutTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			string text = null;
			if (!string.IsNullOrEmpty(templet.m_Talk))
			{
				text = templet.m_Talk;
			}
			else if (!string.IsNullOrEmpty(templet.m_SubTitle))
			{
				text = templet.m_SubTitle;
			}
			else if (!string.IsNullOrEmpty(templet.m_Title))
			{
				text = templet.m_Title;
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (templet.m_bTalkAppend)
				{
					int index = this.m_lstLog.Count - 1;
					this.m_lstLog[index] = this.m_lstLog[index] + " " + text;
					return;
				}
				if (!string.IsNullOrEmpty(templet.m_CharStrID))
				{
					string text2 = "";
					NKCCutScenCharTemplet cutScenCharTempletByStrID = NKCCutScenManager.GetCutScenCharTempletByStrID(templet.m_CharStrID);
					if (cutScenCharTempletByStrID != null)
					{
						text2 = cutScenCharTempletByStrID.m_CharStr;
						if (NKCScenManager.CurrentUserData() != null)
						{
							text2 = text2.Replace("<usernickname>", NKCScenManager.CurrentUserData().m_UserNickName);
						}
					}
					this.m_lstLog.Add(text2 + " : " + text);
					return;
				}
				this.m_lstLog.Add(text ?? "");
			}
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x001F025B File Offset: 0x001EE45B
		private void OnLogViewerClose(bool bEnableTitle, bool bEnableTalkBox, bool bAutoEnabled)
		{
			NKCUtil.SetGameobjectActive(this.m_objTopMenuParent, true);
			NKCUtil.SetGameobjectActive(this.m_NKCUICutScenTitleMgr.gameObject, bEnableTitle);
			NKCUtil.SetGameobjectActive(this.m_NKCUICutScenTalkBoxMgr.MyGameObject, bEnableTalkBox);
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x001F028C File Offset: 0x001EE48C
		public void SetPause(bool bPause)
		{
			if (bPause)
			{
				this.m_AutoToggle.Select(false, false, false);
			}
			this.m_NKCUICutScenBGMgr.SetPause(bPause);
			this.m_NKCUICutScenTitleMgr.SetPause(bPause);
			this.m_NKCUICutScenUnitMgr.SetPause(bPause);
			this.m_NKCUICutScenImgMgr.SetPause(bPause);
			this.m_NKCUICutScenTalkBoxMgr.SetPause(bPause);
			NKCUtil.SetGameobjectActive(this.m_SkipBtn, !bPause);
			NKCUtil.SetGameobjectActive(this.m_AutoToggle, !bPause);
			NKCUtil.SetGameobjectActive(this.m_LogBtn, !bPause);
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null && subUICameraVideoPlayer.IsPlaying)
			{
				if (bPause)
				{
					subUICameraVideoPlayer.SetPlaybackSpeed(0f);
					return;
				}
				subUICameraVideoPlayer.SetPlaybackSpeed(1f);
			}
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x001F0345 File Offset: 0x001EE545
		public bool IsPlaying()
		{
			return this.m_bPlaying;
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x001F034D File Offset: 0x001EE54D
		public void OnValueChangedAuto(bool bSet)
		{
			if (bSet)
			{
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_CUTSCEN_AUTO", 1);
				return;
			}
			PlayerPrefs.SetInt("NKM_LOCAL_SAVE_CUTSCEN_AUTO", 0);
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x001F036C File Offset: 0x001EE56C
		public void Load(string strID, bool bPreLoad = true)
		{
			NKCCutScenManager.LoadFromLUA_CutScene(strID);
			if (bPreLoad)
			{
				Debug.Log("Cutscene " + strID + " Preloading");
			}
			else
			{
				Debug.Log("Cutscene " + strID + " loading");
			}
			HashSet<string> hashSet = new HashSet<string>();
			HashSet<NKMAssetName> hashSet2 = new HashSet<NKMAssetName>();
			HashSet<NKMAssetName> hashSet3 = new HashSet<NKMAssetName>();
			HashSet<string> hashSet4 = new HashSet<string>();
			NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(strID);
			if (cutScenTemple != null)
			{
				int count = cutScenTemple.m_listCutTemplet.Count;
				for (int i = 0; i < count; i++)
				{
					NKCCutTemplet nkccutTemplet = cutScenTemple.m_listCutTemplet[i];
					if (nkccutTemplet != null)
					{
						if (!string.IsNullOrWhiteSpace(nkccutTemplet.m_MovieName))
						{
							AssetBundleManager.GetRawFilePath("Movie/" + nkccutTemplet.m_MovieName);
						}
						if (nkccutTemplet.m_BGFileName.Length > 0 && nkccutTemplet.m_BGFileName != "CLOSE")
						{
							if (nkccutTemplet.m_bGameObjectBGType)
							{
								hashSet3.Add(new NKMAssetName(nkccutTemplet.m_BGFileName, nkccutTemplet.m_BGFileName));
							}
							else
							{
								string text = "AB_UI_NKM_UI_CUTSCEN_BG_" + nkccutTemplet.m_BGFileName;
								hashSet2.Add(new NKMAssetName(text, text));
							}
						}
						if (nkccutTemplet.m_CharStrID.Length > 0)
						{
							NKCCutScenCharTemplet cutScenCharTempletByStrID = NKCCutScenManager.GetCutScenCharTempletByStrID(nkccutTemplet.m_CharStrID);
							if (cutScenCharTempletByStrID != null && cutScenCharTempletByStrID.m_PrefabStr.Length > 0)
							{
								hashSet.Add(cutScenCharTempletByStrID.m_PrefabStr);
							}
						}
						if (nkccutTemplet.m_ImageName.Length > 0)
						{
							hashSet2.Add(new NKMAssetName("AB_UI_NKM_UI_CUTSCEN_IMG", "AB_UI_NKM_UI_CUTSCEN_IMG_" + nkccutTemplet.m_ImageName));
						}
						if (nkccutTemplet.m_StartBGMFileName.Length > 0)
						{
							hashSet4.Add(nkccutTemplet.m_StartBGMFileName);
						}
						if (nkccutTemplet.m_EndBGMFileName.Length > 0)
						{
							hashSet4.Add(nkccutTemplet.m_EndBGMFileName);
						}
						if (nkccutTemplet.m_StartFXSoundName.Length > 0)
						{
							hashSet4.Add(nkccutTemplet.m_StartFXSoundName);
						}
						if (nkccutTemplet.m_EndFXSoundName.Length > 0)
						{
							hashSet4.Add(nkccutTemplet.m_EndFXSoundName);
						}
						if (nkccutTemplet.m_Action == NKCCutTemplet.eCutsceneAction.PLAY_MUSIC)
						{
							string actionFirstToken = nkccutTemplet.GetActionFirstToken();
							if (!string.IsNullOrEmpty(actionFirstToken))
							{
								hashSet4.Add(actionFirstToken);
							}
						}
						if (!string.IsNullOrWhiteSpace(nkccutTemplet.m_VoiceFileName))
						{
							hashSet4.Add(nkccutTemplet.m_VoiceFileName);
						}
					}
				}
				foreach (string unitStrID in hashSet)
				{
					NKCASUIUnitIllust closeObj = NKCResourceUtility.OpenSpineIllustWithManualNaming(unitStrID, bPreLoad);
					NKCScenManager.GetScenManager().GetObjectPool().CloseObj(closeObj);
				}
				foreach (string text2 in hashSet4)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						this.m_listNKCAssetResourceData.Add(NKCAssetResourceManager.OpenResource<AudioClip>(text2, bPreLoad));
					}
				}
				if (bPreLoad)
				{
					foreach (NKMAssetName nkmassetName in hashSet3)
					{
						if (nkmassetName != null && !string.IsNullOrEmpty(nkmassetName.m_AssetName) && !string.IsNullOrEmpty(nkmassetName.m_BundleName))
						{
							NKCResourceUtility.LoadAssetResourceTemp<GameObject>(nkmassetName.m_BundleName, nkmassetName.m_AssetName, true);
						}
					}
					using (HashSet<NKMAssetName>.Enumerator enumerator2 = hashSet2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							NKMAssetName nkmassetName2 = enumerator2.Current;
							if (nkmassetName2 != null && !string.IsNullOrEmpty(nkmassetName2.m_AssetName) && !string.IsNullOrEmpty(nkmassetName2.m_BundleName))
							{
								NKCResourceUtility.LoadAssetResourceTemp<Sprite>(nkmassetName2.m_BundleName, nkmassetName2.m_AssetName, true);
							}
						}
						return;
					}
				}
				foreach (NKMAssetName nkmassetName3 in hashSet3)
				{
					if (nkmassetName3 != null && !string.IsNullOrEmpty(nkmassetName3.m_AssetName) && !string.IsNullOrEmpty(nkmassetName3.m_BundleName))
					{
						this.m_listNKCAssetResourceData.Add(NKCAssetResourceManager.OpenResource<GameObject>(nkmassetName3.m_BundleName, nkmassetName3.m_AssetName, false, null));
					}
				}
				foreach (NKMAssetName nkmassetName4 in hashSet2)
				{
					if (nkmassetName4 != null && !string.IsNullOrEmpty(nkmassetName4.m_AssetName) && !string.IsNullOrEmpty(nkmassetName4.m_BundleName))
					{
						this.m_listNKCAssetResourceData.Add(NKCAssetResourceManager.OpenResource<Sprite>(nkmassetName4.m_BundleName, nkmassetName4.m_AssetName, false, null));
					}
				}
			}
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x001F0840 File Offset: 0x001EEA40
		public void UnLoad()
		{
			Debug.Log("Cutscene unloading");
			for (int i = 0; i < this.m_listNKCAssetResourceData.Count; i++)
			{
				NKCAssetResourceManager.CloseResource(this.m_listNKCAssetResourceData[i]);
			}
			this.m_listNKCAssetResourceData.Clear();
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x001F088C File Offset: 0x001EEA8C
		private void DetachToFrontLow()
		{
			this.m_NKCUICutScenBGMgr.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontLow), false);
			this.m_NKCUICutScenUnitMgr.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontLow), false);
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x001F08BC File Offset: 0x001EEABC
		private void AttachToCutscenPlayer()
		{
			this.m_NKCUICutScenBGMgr.transform.SetParent(base.gameObject.transform, false);
			this.m_NKCUICutScenUnitMgr.transform.SetParent(base.gameObject.transform, false);
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x001F08F6 File Offset: 0x001EEAF6
		public override void Hide()
		{
			this.AttachToCutscenPlayer();
			base.Hide();
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x001F0904 File Offset: 0x001EEB04
		public override void UnHide()
		{
			base.UnHide();
			this.DetachToFrontLow();
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x001F0914 File Offset: 0x001EEB14
		public void Play(NKCCutScenTemplet cNKCCutScenTemplet, int stageID, NKCUICutScenPlayer.CutScenCallBack _callBack = null)
		{
			if (cNKCCutScenTemplet == null)
			{
				Log.Error("NKCUICutScenPlayer Cannot Play becuase templet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUICutScenPlayer.cs", 650);
				return;
			}
			this.m_NKCCutScenTemplet = cNKCCutScenTemplet;
			this.m_stageID = stageID;
			this.m_Callback = _callBack;
			if (this.m_logViewer.gameObject.activeSelf)
			{
				this.m_logViewer.gameObject.SetActive(false);
			}
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (!this.m_objTopMenuParent.activeSelf)
			{
				this.m_objTopMenuParent.SetActive(true);
			}
			this.m_bWasBloom = NKCCamera.GetEnableBloom();
			NKCCamera.EnableBloom(false);
			this.m_NextCutIndex = 0;
			this.m_NKCUICutScenBGMgr.Reset();
			this.m_NKCUICutScenTitleMgr.Reset();
			this.m_NKCUICutScenUnitMgr.Reset();
			this.m_NKCUICutScenImgMgr.Reset();
			this.m_NKCUICutScenTalkBoxMgr.ResetTalkBox();
			NKCUtil.SetGameobjectActive(this.m_NKCUICutScenBGMgr, true);
			NKCUtil.SetGameobjectActive(this.m_LogBtn, true);
			NKCUtil.SetGameobjectActive(this.m_PauseToggle, true);
			NKCUtil.SetGameobjectActive(this.m_SkipBtn, true);
			NKCUtil.SetGameobjectActive(this.m_AutoToggle, true);
			this.m_PauseToggle.Select(false, true, false);
			NKCUtil.SetGameobjectActive(this.m_objSelectionRoot, false);
			this.m_bPlaying = true;
			if (PlayerPrefs.HasKey("NKM_LOCAL_SAVE_CUTSCEN_AUTO"))
			{
				if (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_CUTSCEN_AUTO") == 1)
				{
					this.m_AutoToggle.Select(true, true, false);
				}
				else
				{
					this.m_AutoToggle.Select(false, true, false);
				}
			}
			else
			{
				this.m_AutoToggle.Select(false, true, false);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_CUTSCEN_AUTO", 0);
			}
			this.m_NKCUICutScenUnitMgr.Open();
			NKCUIFadeInOut.Close(false);
			NKCUIManager.SetUseFrontLowCanvas(true);
			this.DetachToFrontLow();
			NKCCamera.SetEnableSepiaToneSubUILowCam(false);
			this.m_lstLog.Clear();
			base.UIOpened(true);
			this.ApplyNextCut();
			NKCAdjustManager.OnPlayCutScene(this.m_NKCCutScenTemplet.m_CutScenID);
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x001F0AF0 File Offset: 0x001EECF0
		public void Play(string strID, int stageID, NKCUICutScenPlayer.CutScenCallBack _callBack = null)
		{
			NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(strID);
			if (cutScenTemple == null)
			{
				Log.Error("NKCUICutScenPlayer Cannot Play: " + strID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUICutScenPlayer.cs", 746);
				return;
			}
			this.Play(cutScenTemple, stageID, _callBack);
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x001F0B2B File Offset: 0x001EED2B
		public void Play(Queue<string> qStrID, int stageID, NKCUICutScenPlayer.CutScenCallBack _callBack = null)
		{
			this.m_qNextCutScen = qStrID;
			this.m_stageID = stageID;
			if (this.m_qNextCutScen.Count > 0)
			{
				this.Play(this.m_qNextCutScen.Dequeue(), stageID, _callBack);
				return;
			}
			this.m_Callback = _callBack;
			this.CallBack();
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x001F0B6A File Offset: 0x001EED6A
		public void LoadAndPlay(string strID, int stageID, NKCUICutScenPlayer.CutScenCallBack _callBack = null, bool bAsync = true)
		{
			NKMPopUpBox.OpenWaitBox(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, 0f, "", null);
			base.gameObject.SetActive(true);
			base.StartCoroutine(this._LoadAndPlay(strID, stageID, _callBack, bAsync));
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x001F0B9B File Offset: 0x001EED9B
		private IEnumerator _LoadAndPlay(string strID, int stageID, NKCUICutScenPlayer.CutScenCallBack _callBack = null, bool bAsync = true)
		{
			this.UnLoad();
			this.Load(strID, bAsync);
			while (!NKCAssetResourceManager.IsLoadEnd())
			{
				yield return new WaitForSeconds(0.02f);
			}
			if (bAsync)
			{
				NKCResourceUtility.SwapResource();
			}
			NKMPopUpBox.CloseWaitBox();
			this.Play(strID, stageID, delegate()
			{
				NKCSoundManager.StopAllSound();
				NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
				NKCUICutScenPlayer.CutScenCallBack callBack = _callBack;
				if (callBack == null)
				{
					return;
				}
				callBack();
			});
			yield break;
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001F0BC8 File Offset: 0x001EEDC8
		private bool IsCutFinished()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			return (!this.m_cNKCUICutState.m_bTitle || (this.m_cNKCUICutState.m_bTitle && this.m_NKCUICutScenTitleMgr.IsFinshed())) && (!this.m_cNKCUICutState.m_bFading || (this.m_cNKCUICutState.m_bFading && NKCUIFadeInOut.IsFinshed())) && (!this.m_cNKCUICutState.m_bTalk || (this.m_cNKCUICutState.m_bTalk && this.m_NKCUICutScenTalkBoxMgr.IsFinished)) && this.m_NKCUICutScenUnitMgr.IsFinished() && this.m_NKCUICutScenImgMgr.IsFinished() && this.m_NKCUICutScenBGMgr.IsFinished() && (!this.m_cNKCUICutState.m_bPlayVideo || (subUICameraVideoPlayer != null && !subUICameraVideoPlayer.IsPlayingOrPreparing() && !this.m_cNKCUICutState.m_bWaitSelection)) && (this.m_cNKCUICutState.m_VoiceUID < 0 || !this.m_AutoToggle.m_bChecked || !NKCSoundManager.IsPlayingVoice(this.m_cNKCUICutState.m_VoiceUID));
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x001F0CE0 File Offset: 0x001EEEE0
		private void InvalidVideoCallback()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.InvalidateCallBack();
			}
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x001F0D04 File Offset: 0x001EEF04
		public void OnClickedPlayer()
		{
			if (this.m_logViewer.gameObject.activeSelf)
			{
				return;
			}
			if (this.m_NKCCutScenTemplet == null)
			{
				return;
			}
			if (this.m_PauseToggle.m_bChecked)
			{
				this.m_PauseToggle.Select(false, false, false);
				return;
			}
			if (!this.m_cNKCUICutState.m_bWaitClick && (this.m_cNKCUICutState.m_bTitle || this.m_cNKCUICutState.m_bTalk))
			{
				return;
			}
			if (!this.IsCutFinished() || !this.m_cNKCUICutState.m_bWaitClick)
			{
				this.m_NKCUICutScenTitleMgr.Finish();
				this.m_NKCUICutScenTalkBoxMgr.Finish();
				NKCUIFadeInOut.Finish();
				this.m_NKCUICutScenUnitMgr.Finish();
				this.m_NKCUICutScenImgMgr.Finish();
				this.m_NKCUICutScenBGMgr.Finish();
				if (!this.m_cNKCUICutState.m_bWaitClick)
				{
					this.InvalidVideoCallback();
					this.ApplyNextCut();
				}
				return;
			}
			this.InvalidVideoCallback();
			NKCUICutScenTalkBoxMgrForCenterText nkcuicutScenTalkBoxMgrForCenterText = this.m_NKCUICutScenTalkBoxMgr as NKCUICutScenTalkBoxMgrForCenterText;
			if (nkcuicutScenTalkBoxMgrForCenterText != null && nkcuicutScenTalkBoxMgrForCenterText.WaitForFadOut && nkcuicutScenTalkBoxMgrForCenterText.WaitForFadOut)
			{
				nkcuicutScenTalkBoxMgrForCenterText.StartFadeOut(new Action(this.ApplyNextCut));
				return;
			}
			this.ApplyNextCut();
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x001F0E1C File Offset: 0x001EF01C
		public void OnClickedSkip()
		{
			if (this.m_cNKCUICutState.m_bPlayVideo)
			{
				this.SetMoviePause(true);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CUTSCENE_MOVIE_SKIP_TITLE, NKCUtilString.GET_STRING_CUTSCENE_MOVIE_SKIP_DESC, new NKCPopupOKCancel.OnButton(this.Skip), delegate()
				{
					this.SetMoviePause(false);
				}, false);
				return;
			}
			this.Skip();
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x001F0E6C File Offset: 0x001EF06C
		private void Skip()
		{
			this.StopPure();
			if (!this.PlayIfNextExist())
			{
				this.CallBack();
			}
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x001F0E84 File Offset: 0x001EF084
		private void SetMoviePause(bool bPause)
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				if (bPause)
				{
					subUICameraVideoPlayer.SetPlaybackSpeed(0f);
					return;
				}
				subUICameraVideoPlayer.SetPlaybackSpeed(1f);
			}
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x001F0EBA File Offset: 0x001EF0BA
		public bool PlayIfNextExist()
		{
			if (this.m_qNextCutScen.Count > 0)
			{
				this.Play(this.m_qNextCutScen.Dequeue(), this.m_stageID, null);
				return true;
			}
			return false;
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x001F0EE8 File Offset: 0x001EF0E8
		private void Update()
		{
			if (this.m_bPlaying)
			{
				if (this.IsCutFinished() && !this.m_PauseToggle.m_bChecked)
				{
					if (!this.m_cNKCUICutState.m_bWaitClick || this.m_AutoToggle.m_bChecked)
					{
						if (this.m_cNKCUICutState.m_fWaitTime > 0f)
						{
							this.m_cNKCUICutState.m_fElapsedTimeWithoutAutoCalc += Time.deltaTime;
							if (this.m_cNKCUICutState.m_fElapsedTimeWithoutAutoCalc >= this.m_cNKCUICutState.m_fWaitTime)
							{
								this.ApplyNextCut();
							}
							else if (this.m_AutoToggle.m_bChecked && this.m_cNKCUICutState.m_fElapsedTimeWithoutAutoCalc >= this.m_cNKCUICutState.m_fWaitTime - this.m_cNKCUICutState.m_fAddWaitTimeForAuto)
							{
								this.m_NKCUICutScenUnitMgr.StopCrash();
							}
						}
						else
						{
							this.ApplyNextCut();
						}
					}
					else
					{
						this.PlayReservedSoundNMusic();
						this.m_NKCUICutScenUnitMgr.StopCrash();
					}
				}
				if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Skip))
				{
					this.m_cNKCUICutState.m_bWaitClick = true;
					this.OnClickedPlayer();
				}
			}
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x001F0FF4 File Offset: 0x001EF1F4
		private void PlayReservedSoundNMusic()
		{
			if (this.m_cNKCUICutState.m_EndBGMFileName.Length > 0)
			{
				NKCSoundManager.PlayMusic(this.m_cNKCUICutState.m_EndBGMFileName, true, 1f, false, 0f, 0f);
				this.m_cNKCUICutState.m_EndBGMFileName = "";
			}
			if (this.m_cNKCUICutState.m_EndFXSoundName.Length > 0)
			{
				if (this.m_cNKCUICutState.m_EndFXSoundControl == NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY)
				{
					NKCSoundManager.PlaySound(this.m_cNKCUICutState.m_EndFXSoundName, 1f, 0f, 0f, false, 0f, false, 0f);
				}
				else if (this.m_cNKCUICutState.m_EndFXSoundControl == NKC_CUTSCEN_SOUND_CONTROL.NCSC_LOOP_PLAY)
				{
					int soundUID = NKCSoundManager.PlaySound(this.m_cNKCUICutState.m_EndFXSoundName, 1f, 0f, 0f, true, 0f, false, 0f);
					this.AddLoopSoundID(this.m_cNKCUICutState.m_EndFXSoundName, soundUID);
				}
				else if (this.m_cNKCUICutState.m_EndFXSoundControl == NKC_CUTSCEN_SOUND_CONTROL.NCSC_STOP)
				{
					this.StopSound(this.m_cNKCUICutState.m_EndFXSoundName);
				}
				this.m_cNKCUICutState.m_EndFXSoundName = "";
				this.m_cNKCUICutState.m_EndFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_STOP;
			}
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x001F1120 File Offset: 0x001EF320
		private void AddLoopSoundID(string name, int soundUID)
		{
			List<int> list = null;
			if (this.m_dicLoopSounds.TryGetValue(name, out list))
			{
				list.Add(soundUID);
				return;
			}
			list = new List<int>();
			list.Add(soundUID);
			this.m_dicLoopSounds.Add(name, list);
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x001F1164 File Offset: 0x001EF364
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			switch (message)
			{
			case NKCUIComVideoPlayer.eVideoMessage.PlayFailed:
			case NKCUIComVideoPlayer.eVideoMessage.PlayComplete:
				NKCUtil.SetGameobjectActive(this.m_AutoToggle, true);
				NKCUtil.SetGameobjectActive(this.m_LogBtn, true);
				NKCUtil.SetGameobjectActive(this.m_PauseToggle, true);
				NKCUtil.SetGameobjectActive(this.m_objTopMenuParent, true);
				this.ApplyNextCut();
				return;
			case NKCUIComVideoPlayer.eVideoMessage.PlayBegin:
				NKCUtil.SetGameobjectActive(this.m_AutoToggle, false);
				NKCUtil.SetGameobjectActive(this.m_LogBtn, false);
				NKCUtil.SetGameobjectActive(this.m_PauseToggle, false);
				NKCUtil.SetGameobjectActive(this.m_objTopMenuParent, this.m_cNKCUICutState.m_bMovieSkipEnable);
				return;
			default:
				return;
			}
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x001F11F8 File Offset: 0x001EF3F8
		private void ApplyNextCut()
		{
			this.PlayReservedSoundNMusic();
			this.m_NKCUICutScenUnitMgr.Finish();
			this.m_cNKCUICutState.InitPerCut();
			this.m_NKCUICutScenTitleMgr.Close();
			if (this.m_NKCCutScenTemplet.m_listCutTemplet.Count <= this.m_NextCutIndex)
			{
				this.StopPure();
				if (!this.PlayIfNextExist())
				{
					this.CallBack();
				}
				return;
			}
			NKCCutTemplet nkccutTemplet = this.m_NKCCutScenTemplet.m_listCutTemplet[this.m_NextCutIndex];
			if (nkccutTemplet != null)
			{
				if (nkccutTemplet.m_bLooseShake)
				{
					NKCUIManager.StartLooseShake();
				}
				else
				{
					NKCUIManager.StopLooseShake();
				}
				if (this.ProcessCutsceneAction(nkccutTemplet))
				{
					return;
				}
				if (nkccutTemplet.m_FilterType != NKC_CUTSCEN_FILTER_TYPE.NCFT_NONE)
				{
					NKCCamera.SetEnableSepiaToneSubUILowCam(nkccutTemplet.m_FilterType == NKC_CUTSCEN_FILTER_TYPE.NCFT_SEPIA);
				}
				if (!string.IsNullOrEmpty(nkccutTemplet.m_MovieName))
				{
					this.m_cNKCUICutState.m_bPlayVideo = true;
					this.m_cNKCUICutState.m_bMovieSkipEnable = nkccutTemplet.m_bMovieSkipEnable;
					NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
					if (subUICameraVideoPlayer != null)
					{
						subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
						subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
						subUICameraVideoPlayer.Play(nkccutTemplet.m_MovieName, false, true, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), true);
					}
				}
				else
				{
					NKCUIComVideoCamera subUICameraVideoPlayer2 = NKCCamera.GetSubUICameraVideoPlayer();
					if (subUICameraVideoPlayer2 != null)
					{
						subUICameraVideoPlayer2.Stop();
					}
				}
				if (nkccutTemplet.m_Title.Length > 0 || nkccutTemplet.m_SubTitle.Length > 0)
				{
					this.m_cNKCUICutState.m_bTitle = true;
					this.m_NKCUICutScenTitleMgr.Open(nkccutTemplet.m_bTitleFadeOut, nkccutTemplet.m_fTitleFadeOutTime, nkccutTemplet.m_SubTitle, nkccutTemplet.m_Title, nkccutTemplet.m_fTitleTalkTime, nkccutTemplet.m_fSubTitleTalkTime);
				}
				else if (nkccutTemplet.m_bTitleClear)
				{
					this.m_NKCUICutScenTitleMgr.ForceClear();
				}
				if (nkccutTemplet.m_CloseTalkBox)
				{
					INKCUICutScenTalkBoxMgr nkcuicutScenTalkBoxMgr = this.m_NKCUICutScenTalkBoxMgr;
					if (nkcuicutScenTalkBoxMgr != null)
					{
						nkcuicutScenTalkBoxMgr.Close();
					}
				}
				this.m_cNKCUICutState.m_bWaitClick = nkccutTemplet.m_bWaitClick;
				if (nkccutTemplet.m_fWaitTime > 0f)
				{
					this.m_cNKCUICutState.m_fWaitTime = nkccutTemplet.m_fWaitTime;
				}
				if (nkccutTemplet.m_BGFileName == "CLOSE")
				{
					this.m_NKCUICutScenBGMgr.CloseBG();
				}
				else if (nkccutTemplet.m_BGFileName.Length > 0 || (nkccutTemplet.m_bGameObjectBGType && nkccutTemplet.m_GameObjectBGAniName.Length > 0))
				{
					this.m_NKCUICutScenBGMgr.Open(nkccutTemplet.m_bGameObjectBGType, nkccutTemplet.m_BGFileName, nkccutTemplet.m_GameObjectBGAniName, nkccutTemplet.m_bGameObjectBGLoop, nkccutTemplet.m_fBGFadeInTime, nkccutTemplet.m_easeBGFadeIn, nkccutTemplet.m_colBGFadeInStart, nkccutTemplet.m_colBGFadeIn, nkccutTemplet.m_fBGFadeOutTime, nkccutTemplet.m_easeBGFadeOut, nkccutTemplet.m_colBGFadeOut);
				}
				if (nkccutTemplet.m_BGCrash > 0 && nkccutTemplet.m_fBGCrashTime > 0f)
				{
					this.m_NKCUICutScenBGMgr.SetCrash(nkccutTemplet.m_BGCrash, nkccutTemplet.m_fBGCrashTime);
				}
				else if (nkccutTemplet.m_fBGAnITime > 0f && (nkccutTemplet.m_bBGAniPos || nkccutTemplet.m_bBGAniScale))
				{
					this.m_NKCUICutScenBGMgr.SetAni(nkccutTemplet.m_bNoWaitBGAni, nkccutTemplet.m_fBGAnITime, nkccutTemplet.m_bBGAniPos, nkccutTemplet.m_BGOffsetPose, nkccutTemplet.m_tdtBGPos, nkccutTemplet.m_bBGAniScale, nkccutTemplet.m_BGOffsetScale, nkccutTemplet.m_tdtBGScale);
				}
				if (nkccutTemplet.m_fFadeTime > 0f)
				{
					this.m_cNKCUICutState.m_bFading = true;
					if (nkccutTemplet.m_bFadeIn)
					{
						NKCUIFadeInOut.FadeIn(nkccutTemplet.m_fFadeTime, null, nkccutTemplet.m_bFadeWhite);
					}
					else
					{
						NKCUIFadeInOut.FadeOut(nkccutTemplet.m_fFadeTime, null, nkccutTemplet.m_bFadeWhite, -1f);
					}
				}
				else if (nkccutTemplet.m_fFlashBangTime > 0f)
				{
					NKCUIFadeInOut.FadeIn(nkccutTemplet.m_fFlashBangTime, null, true);
				}
				if (nkccutTemplet.m_bClear)
				{
					this.m_NKCUICutScenUnitMgr.ClearUnit(nkccutTemplet);
				}
				else if (nkccutTemplet.m_CharStrID.Length > 0)
				{
					NKCCutScenCharTemplet cutScenCharTempletByStrID = NKCCutScenManager.GetCutScenCharTempletByStrID(nkccutTemplet.m_CharStrID);
					if (cutScenCharTempletByStrID != null)
					{
						this.m_NKCUICutScenUnitMgr.SetUnit(cutScenCharTempletByStrID, nkccutTemplet);
					}
				}
				if (nkccutTemplet.m_Talk.Length > 0)
				{
					string text = "";
					if (!string.IsNullOrEmpty(nkccutTemplet.m_CharStrID))
					{
						NKCCutScenCharTemplet cutScenCharTempletByStrID2 = NKCCutScenManager.GetCutScenCharTempletByStrID(nkccutTemplet.m_CharStrID);
						if (cutScenCharTempletByStrID2 != null)
						{
							text = cutScenCharTempletByStrID2.m_CharStr;
						}
					}
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					if (NKCScenManager.GetScenManager() != null && myUserData != null)
					{
						string newValue = NKCUITypeWriter.ReplaceNameString(myUserData.m_UserNickName, true);
						text = text.Replace("<usernickname>", newValue);
					}
					else
					{
						text = text.Replace("<usernickname>", "TESTCUT");
					}
					NKCUICutScenPlayer.ChangeTalkBoxMgrType(nkccutTemplet);
					this.m_NKCUICutScenTalkBoxMgr.Open(text, nkccutTemplet.m_Talk, nkccutTemplet.m_fTalkTime, nkccutTemplet.m_bWaitClick, nkccutTemplet.m_bTalkAppend);
					if (nkccutTemplet.m_bTalkCenterFadeIn)
					{
						this.m_NKCUICutScenTalkBoxMgr.StartFadeIn(nkccutTemplet.m_bTalkCenterFadeTime);
					}
					else if (nkccutTemplet.m_bTalkCenterFadeOut)
					{
						this.m_NKCUICutScenTalkBoxMgr.FadeOutBooking(nkccutTemplet.m_bTalkCenterFadeTime);
					}
					this.m_cNKCUICutState.m_bTalk = true;
					if (this.m_AutoToggle.m_bChecked)
					{
						this.m_cNKCUICutState.m_fAddWaitTimeForAuto = 1f;
						this.m_NKCTextChunk.TextAnalyze(nkccutTemplet.m_Talk);
						int pureTextCount = this.m_NKCTextChunk.GetPureTextCount();
						if (pureTextCount > 10)
						{
							float num = (float)(pureTextCount - 10) * this.ADD_WAIT_TIME_PER_ONE_WORD_BY_LONG_TALK_FOR_AUTO;
							if (num >= 3f)
							{
								num = 3f;
							}
							this.m_cNKCUICutState.m_fAddWaitTimeForAuto += num;
						}
						this.m_cNKCUICutState.m_fWaitTime += this.m_cNKCUICutState.m_fAddWaitTimeForAuto;
					}
				}
				else if (this.m_NKCUICutScenUnitMgr.IsExistUnitInScen())
				{
					this.m_NKCUICutScenTalkBoxMgr.ClearTalk();
				}
				else
				{
					this.m_NKCUICutScenTalkBoxMgr.Close();
				}
				if (nkccutTemplet.m_ImageName.Length > 0)
				{
					this.m_NKCUICutScenImgMgr.Open(nkccutTemplet.m_ImageName, nkccutTemplet.m_ImageOffsetPos, nkccutTemplet.m_fImageScale);
				}
				else
				{
					this.m_NKCUICutScenImgMgr.Close();
				}
				if (nkccutTemplet.m_StartBGMFileName.Length > 0)
				{
					NKCSoundManager.PlayMusic(nkccutTemplet.m_StartBGMFileName, true, 1f, false, 0f, 0f);
				}
				if (nkccutTemplet.m_StartFXSoundName.Length > 0)
				{
					if (nkccutTemplet.m_StartFXSoundControl == NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY)
					{
						NKCSoundManager.PlaySound(nkccutTemplet.m_StartFXSoundName, 1f, 0f, 0f, false, 0f, false, 0f);
					}
					else if (nkccutTemplet.m_StartFXSoundControl == NKC_CUTSCEN_SOUND_CONTROL.NCSC_LOOP_PLAY)
					{
						int soundUID = NKCSoundManager.PlaySound(nkccutTemplet.m_StartFXSoundName, 1f, 0f, 0f, true, 0f, false, 0f);
						this.AddLoopSoundID(nkccutTemplet.m_StartFXSoundName, soundUID);
					}
					else if (nkccutTemplet.m_StartFXSoundControl == NKC_CUTSCEN_SOUND_CONTROL.NCSC_STOP)
					{
						this.StopSound(nkccutTemplet.m_StartFXSoundName);
					}
				}
				this.m_cNKCUICutState.m_EndBGMFileName = nkccutTemplet.m_EndBGMFileName;
				this.m_cNKCUICutState.m_EndFXSoundControl = nkccutTemplet.m_EndFXSoundControl;
				this.m_cNKCUICutState.m_EndFXSoundName = nkccutTemplet.m_EndFXSoundName;
				if (!string.IsNullOrWhiteSpace(nkccutTemplet.m_VoiceFileName))
				{
					string bundleName = NKCAssetResourceManager.GetBundleName(nkccutTemplet.m_VoiceFileName);
					if (NKCAssetResourceManager.IsBundleExists(bundleName, nkccutTemplet.m_VoiceFileName))
					{
						this.m_cNKCUICutState.m_VoiceUID = NKCSoundManager.PlayVoice(nkccutTemplet.m_VoiceFileName, 0, false, false, 1f, 0f, 0f, false, 0f, false);
					}
					else
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"컷신 보이스 ",
							nkccutTemplet.m_VoiceFileName,
							"가 있지만 번들파일 ",
							bundleName,
							"이 존재하지 않음"
						}));
					}
				}
				this.AddLog(nkccutTemplet);
			}
			this.m_NextCutIndex++;
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x001F1919 File Offset: 0x001EFB19
		private static void ChangeTalkBoxMgrType(NKCCutTemplet cTemplet)
		{
			if (cTemplet.m_bTalkCenterFadeIn || cTemplet.m_bTalkCenterFadeOut)
			{
				NKCUICutScenPlayer.m_CutScenTalkBoxMgrType = NKCUICutScenTalkBoxMgr.TalkBoxType.CenterText;
				return;
			}
			NKCUICutScenPlayer.m_CutScenTalkBoxMgrType = NKCUICutScenTalkBoxMgr.TalkBoxType.JapanNeeds;
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x001F1938 File Offset: 0x001EFB38
		private bool ProcessCutsceneAction(NKCCutTemplet cTemplet)
		{
			if (cTemplet == null)
			{
				return false;
			}
			switch (cTemplet.m_Action)
			{
			case NKCCutTemplet.eCutsceneAction.NONE:
			case NKCCutTemplet.eCutsceneAction.MARK:
				return false;
			case NKCCutTemplet.eCutsceneAction.JUMP:
				this.JumpToMark(cTemplet.m_ActionStrKey);
				return true;
			case NKCCutTemplet.eCutsceneAction.SELECT:
				return this.ProcessSelection(this.m_NextCutIndex);
			case NKCCutTemplet.eCutsceneAction.PLAY_MUSIC:
			{
				string[] actionTokens = cTemplet.GetActionTokens();
				if (actionTokens != null && actionTokens.Length != 0)
				{
					string audioClipName = actionTokens[0];
					string s = actionTokens[1];
					float fStartTime = 0f;
					float num;
					if (float.TryParse(s, out num))
					{
						if (num < 0f)
						{
							fStartTime = NKCSoundManager.GetMusicTime();
						}
						else
						{
							fStartTime = num;
						}
					}
					NKCSoundManager.PlayMusic(audioClipName, true, 1f, true, fStartTime, 0f);
					return false;
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x001F19D4 File Offset: 0x001EFBD4
		private void JumpToMark(string mark)
		{
			int num = this.m_NKCCutScenTemplet.m_listCutTemplet.FindIndex((NKCCutTemplet x) => x.m_Action == NKCCutTemplet.eCutsceneAction.MARK && x.m_ActionStrKey == mark);
			if (num < 0)
			{
				Debug.LogError("Mark " + mark + " not found!!");
				return;
			}
			this.m_NextCutIndex = num;
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x001F1A34 File Offset: 0x001EFC34
		private bool ProcessSelection(int currentIndex)
		{
			List<Tuple<string, string>> selectionRoutes = NKCCutScenManager.GetSelectionRoutes(this.m_NKCCutScenTemplet.m_CutScenID, currentIndex);
			if (selectionRoutes == null)
			{
				return false;
			}
			this.m_cNKCUICutState.m_bWaitSelection = true;
			this.m_cNKCUICutState.m_lstSelectionMark.Clear();
			for (int i = 0; i < this.m_aSelectionButtons.Length; i++)
			{
				if (i < selectionRoutes.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_aSelectionButtons[i], true);
					this.m_cNKCUICutState.m_lstSelectionMark.Add(selectionRoutes[i]);
					NKCUIComStateButton nkcuicomStateButton = this.m_aSelectionButtons[i];
					if (nkcuicomStateButton != null)
					{
						nkcuicomStateButton.SetTitleText(selectionRoutes[i].Item2);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_aSelectionButtons[i], false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objSelectionRoot, true);
			return true;
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x001F1AF4 File Offset: 0x001EFCF4
		private void OnSelectionRoute(int routeIndex)
		{
			this.m_cNKCUICutState.m_bWaitSelection = false;
			NKCUtil.SetGameobjectActive(this.m_objSelectionRoot, false);
			this.m_lstLog.Add(NKCStringTable.GetString("SI_DP_CUTSCENE_LOG_SELECTION", new object[]
			{
				this.m_cNKCUICutState.m_lstSelectionMark[routeIndex].Item2
			}));
			this.JumpToMark(this.m_cNKCUICutState.m_lstSelectionMark[routeIndex].Item1);
			this.ApplyNextCut();
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x001F1B70 File Offset: 0x001EFD70
		private void StopSound(string name)
		{
			List<int> list = null;
			if (this.m_dicLoopSounds.TryGetValue(name, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					NKCSoundManager.StopSound(list[i]);
				}
				this.m_dicLoopSounds.Remove(name);
			}
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x001F1BBC File Offset: 0x001EFDBC
		private void PlayLastMusicNotPlayed()
		{
			string text = "";
			while (this.m_qNextCutScen.Count > 0)
			{
				text = this.m_qNextCutScen.Dequeue();
			}
			string text2 = "";
			if (text == "" && this.m_NKCCutScenTemplet != null)
			{
				if (this.m_cNKCUICutState.m_EndBGMFileName.Length > 0)
				{
					text2 = this.m_cNKCUICutState.m_EndBGMFileName;
				}
				this.m_NextCutIndex++;
				while (this.m_NKCCutScenTemplet.m_listCutTemplet.Count > this.m_NextCutIndex)
				{
					NKCCutTemplet nkccutTemplet = this.m_NKCCutScenTemplet.m_listCutTemplet[this.m_NextCutIndex];
					if (nkccutTemplet != null)
					{
						if (nkccutTemplet.m_StartBGMFileName.Length > 0)
						{
							text2 = nkccutTemplet.m_StartBGMFileName;
						}
						if (nkccutTemplet.m_EndBGMFileName.Length > 0)
						{
							text2 = nkccutTemplet.m_EndBGMFileName;
						}
						if (nkccutTemplet.m_Action == NKCCutTemplet.eCutsceneAction.PLAY_MUSIC)
						{
							string actionFirstToken = nkccutTemplet.GetActionFirstToken();
							if (!string.IsNullOrEmpty(actionFirstToken))
							{
								text2 = actionFirstToken;
							}
						}
					}
					this.m_NextCutIndex++;
				}
			}
			else if (text.Length > 0)
			{
				NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(text);
				if (cutScenTemple != null)
				{
					text2 = cutScenTemple.GetLastMusicAssetName();
				}
			}
			if (text2 != null && text2.Length > 0)
			{
				NKCSoundManager.PlayMusic(text2, true, 1f, false, 0f, 0f);
			}
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x001F1D04 File Offset: 0x001EFF04
		private void StopPure()
		{
			this.PlayLastMusicNotPlayed();
			if (base.IsOpen)
			{
				base.Close();
				NKCCamera.EnableBloom(this.m_bWasBloom);
				NKCUIFadeInOut.Close(false);
			}
			this.m_NKCUICutScenUnitMgr.Close();
			this.m_bPlaying = false;
			this.m_NKCUICutScenTitleMgr.Close();
			this.m_NKCUICutScenBGMgr.Close();
			this.m_NKCUICutScenTalkBoxMgr.Close();
			this.m_NKCUICutScenImgMgr.Close();
			this.m_qNextCutScen.Clear();
			NKCCamera.SetEnableSepiaToneSubUILowCam(false);
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x001F1D85 File Offset: 0x001EFF85
		private void CallBack()
		{
			if (this.m_Callback != null)
			{
				NKCUICutScenPlayer.CutScenCallBack callback = this.m_Callback;
				this.m_Callback = null;
				callback();
			}
			this.m_Callback = null;
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x001F1DA8 File Offset: 0x001EFFA8
		public void StopWithInvalidatingCallBack()
		{
			this.StopPure();
			if (this.m_NKCCutScenTemplet != null)
			{
				if (this.m_NKCCutScenTemplet.m_CutScenID == 60)
				{
					NKCPublisherModule.Notice.OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces.EP2Act2Clear, null);
				}
				else if (this.m_NKCCutScenTemplet.m_CutScenID == 81)
				{
					NKCPublisherModule.Notice.OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces.EP2Clear, null);
				}
			}
			this.m_Callback = null;
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x001F1E02 File Offset: 0x001F0002
		public void StopWithCallBack()
		{
			if (this.m_logViewer.gameObject.activeInHierarchy)
			{
				this.m_logViewer.OnClickClose();
				return;
			}
			this.StopPure();
			this.CallBack();
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x001F1E30 File Offset: 0x001F0030
		public void OnScroll(PointerEventData data)
		{
			if (NKCUIManager.IsTopmostUI(this))
			{
				if (data.scrollDelta.y > 0f)
				{
					if (this.m_LogBtn.gameObject.activeInHierarchy)
					{
						this.OnClickLogBtn();
						return;
					}
				}
				else if (data.scrollDelta.y < 0f)
				{
					this.OnClickedPlayer();
				}
			}
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x001F1E88 File Offset: 0x001F0088
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Confirm)
			{
				this.OnClickedPlayer();
				return true;
			}
			if (hotkey != HotkeyEventType.ShowHotkey)
			{
				return false;
			}
			NKCUIComHotkeyDisplay.OpenInstance(this.m_trHotkeyHelpPosConfirm, HotkeyEventType.Confirm);
			NKCUIComHotkeyDisplay.OpenInstance(this.m_trHotkeyHelpPosSkip, HotkeyEventType.Skip);
			return false;
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x001F1EBC File Offset: 0x001F00BC
		public void ValidateBySimulate()
		{
			while (this.m_bPlaying)
			{
				if (this.m_cNKCUICutState.m_bPlayVideo)
				{
					this.Skip();
					this.m_cNKCUICutState.m_bPlayVideo = false;
				}
				if (this.m_cNKCUICutState.m_bWaitSelection)
				{
					this.Skip();
					this.m_cNKCUICutState.m_bWaitSelection = false;
				}
				this.OnClickedPlayer();
			}
			NKCSoundManager.StopAllSound();
		}

		// Token: 0x04004E86 RID: 20102
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_cutscen";

		// Token: 0x04004E87 RID: 20103
		private const string UI_ASSET_NAME = "NKM_UI_CUTSCEN_PLAYER";

		// Token: 0x04004E88 RID: 20104
		private static NKCUICutScenPlayer m_Instance;

		// Token: 0x04004E89 RID: 20105
		private const string NKM_LOCAL_SAVE_CUTSCEN_AUTO = "NKM_LOCAL_SAVE_CUTSCEN_AUTO";

		// Token: 0x04004E8A RID: 20106
		private const float DEFAULT_ADD_WAIT_TIME_FOR_AUTO = 1f;

		// Token: 0x04004E8B RID: 20107
		public const float CUTSCEN_TITLE_TYPYING_COOL_TIME = 0.15f;

		// Token: 0x04004E8C RID: 20108
		public GameObject m_objTopMenuParent;

		// Token: 0x04004E8D RID: 20109
		public NKCUIComToggle m_AutoToggle;

		// Token: 0x04004E8E RID: 20110
		public NKCUIComToggle m_PauseToggle;

		// Token: 0x04004E8F RID: 20111
		public NKCUIComButton m_SkipBtn;

		// Token: 0x04004E90 RID: 20112
		public NKCUIComButton m_LogBtn;

		// Token: 0x04004E91 RID: 20113
		public NKCUICutScenLogViewer m_logViewer;

		// Token: 0x04004E92 RID: 20114
		public NKCUIComButton m_NKM_UI_CUTSCEN_PLAYER;

		// Token: 0x04004E93 RID: 20115
		public NKCUIComButton m_NKM_UI_CUTSCEN_PLAYER_SKIP;

		// Token: 0x04004E94 RID: 20116
		public NKCUIComToggle m_NKM_UI_CUTSCEN_PLAYER_TALK_SKIP_AUTO;

		// Token: 0x04004E95 RID: 20117
		public GameObject m_objSelectionRoot;

		// Token: 0x04004E96 RID: 20118
		public NKCUIComStateButton[] m_aSelectionButtons;

		// Token: 0x04004E97 RID: 20119
		[Header("단축키 도움말")]
		public Transform m_trHotkeyHelpPosConfirm;

		// Token: 0x04004E98 RID: 20120
		public Transform m_trHotkeyHelpPosSkip;

		// Token: 0x04004E99 RID: 20121
		private NKCUICutScenTitleMgr m_NKCUICutScenTitleMgr;

		// Token: 0x04004E9A RID: 20122
		private NKCUICutScenBGMgr m_NKCUICutScenBGMgr;

		// Token: 0x04004E9B RID: 20123
		private NKCUICutScenUnitMgr m_NKCUICutScenUnitMgr;

		// Token: 0x04004E9C RID: 20124
		private static NKCUICutScenTalkBoxMgr.TalkBoxType m_CutScenTalkBoxMgrType;

		// Token: 0x04004E9D RID: 20125
		private NKCUICutScenImgMgr m_NKCUICutScenImgMgr;

		// Token: 0x04004E9E RID: 20126
		private List<NKCAssetResourceData> m_listNKCAssetResourceData = new List<NKCAssetResourceData>();

		// Token: 0x04004E9F RID: 20127
		private NKCUICutState m_cNKCUICutState = new NKCUICutState();

		// Token: 0x04004EA0 RID: 20128
		private int m_NextCutIndex;

		// Token: 0x04004EA1 RID: 20129
		private NKCCutScenTemplet m_NKCCutScenTemplet;

		// Token: 0x04004EA2 RID: 20130
		private int m_stageID;

		// Token: 0x04004EA3 RID: 20131
		private bool m_bPlaying;

		// Token: 0x04004EA4 RID: 20132
		private NKCUICutScenPlayer.CutScenCallBack m_Callback;

		// Token: 0x04004EA5 RID: 20133
		private bool m_bWasBloom;

		// Token: 0x04004EA6 RID: 20134
		private Queue<string> m_qNextCutScen = new Queue<string>();

		// Token: 0x04004EA7 RID: 20135
		private NKCTextChunk m_NKCTextChunk = new NKCTextChunk();

		// Token: 0x04004EA8 RID: 20136
		private const float m_ADD_WAIT_TIME_PER_ONE_WORD_BY_LONG_TALK_FOR_AUTO = 0.02f;

		// Token: 0x04004EA9 RID: 20137
		private List<string> m_lstLog = new List<string>();

		// Token: 0x04004EAA RID: 20138
		private const int LONG_TALK_CHECK_WORD_COUNT = 10;

		// Token: 0x04004EAB RID: 20139
		private const float MAX_ADD_WAIT_TIME_FOR_LONG_TALK = 3f;

		// Token: 0x04004EAC RID: 20140
		private Dictionary<string, List<int>> m_dicLoopSounds = new Dictionary<string, List<int>>();

		// Token: 0x02001626 RID: 5670
		// (Invoke) Token: 0x0600AF55 RID: 44885
		public delegate void CutScenCallBack();
	}
}
