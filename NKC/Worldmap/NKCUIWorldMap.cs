using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Mode;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using NKC.Trim;
using NKC.UI.Shop;
using NKC.UI.Trim;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000ABD RID: 2749
	public class NKCUIWorldMap : NKCUIBase
	{
		// Token: 0x06007A7B RID: 31355 RVA: 0x0028D883 File Offset: 0x0028BA83
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIWorldMap.s_LoadedUIData))
			{
				NKCUIWorldMap.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIWorldMap>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_WORLD_MAP_RENEWAL_FRONT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIWorldMap.CleanupInstance));
			}
			return NKCUIWorldMap.s_LoadedUIData;
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06007A7C RID: 31356 RVA: 0x0028D8B7 File Offset: 0x0028BAB7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIWorldMap.s_LoadedUIData != null && NKCUIWorldMap.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06007A7D RID: 31357 RVA: 0x0028D8CC File Offset: 0x0028BACC
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIWorldMap.s_LoadedUIData != null && NKCUIWorldMap.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x0028D8E1 File Offset: 0x0028BAE1
		public static NKCUIWorldMap GetInstance()
		{
			if (NKCUIWorldMap.s_LoadedUIData != null && NKCUIWorldMap.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIWorldMap.s_LoadedUIData.GetInstance<NKCUIWorldMap>();
			}
			return null;
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x0028D902 File Offset: 0x0028BB02
		public static void CleanupInstance()
		{
			NKCUIWorldMap.s_LoadedUIData = null;
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06007A80 RID: 31360 RVA: 0x0028D90A File Offset: 0x0028BB0A
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06007A81 RID: 31361 RVA: 0x0028D90D File Offset: 0x0028BB0D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06007A82 RID: 31362 RVA: 0x0028D910 File Offset: 0x0028BB10
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06007A83 RID: 31363 RVA: 0x0028D913 File Offset: 0x0028BB13
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_WORLDMAP;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06007A84 RID: 31364 RVA: 0x0028D91A File Offset: 0x0028BB1A
		public override NKCUIBase.eTransitionEffectType eTransitionEffect
		{
			get
			{
				if (!this.m_bShowIntro)
				{
					return NKCUIBase.eTransitionEffectType.None;
				}
				return NKCUIBase.eTransitionEffectType.FadeInOut;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06007A85 RID: 31365 RVA: 0x0028D927 File Offset: 0x0028BB27
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_WORLDMAP_INFO";
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06007A86 RID: 31366 RVA: 0x0028D92E File Offset: 0x0028BB2E
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.lstResources;
			}
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x0028D936 File Offset: 0x0028BB36
		public static void SetReservedWarning(NKC_WORLD_MAP_WARNING_TYPE type, int cityID)
		{
			NKCUIWorldMap.m_Reserved_NKC_WORLD_MAP_WARNING_TYPE = type;
			NKCUIWorldMap.m_ReservedWarningCityID = cityID;
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x0028D944 File Offset: 0x0028BB44
		public static void SetReservedPinIntroCityID(int cityID)
		{
			NKCUIWorldMap.m_ReservedPinIntroCityID = cityID;
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x0028D94C File Offset: 0x0028BB4C
		private NKCPopupWorldMapCityUnlock UICityUnlock
		{
			get
			{
				if (this._UICityUnlock == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWorldMapCityUnlock>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_WORLD_MAP_RENEWAL_BRANCH_OPEN_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null);
					this._UICityUnlock = loadedUIData.GetInstance<NKCPopupWorldMapCityUnlock>();
					this._UICityUnlock.InitUI();
				}
				return this._UICityUnlock;
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06007A8A RID: 31370 RVA: 0x0028D99C File Offset: 0x0028BB9C
		private NKCPopupWorldMapEventList UIEventList
		{
			get
			{
				if (this._UIEventList == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWorldMapEventList>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_WORLD_MAP_RENEWAL_EVENT_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this._UIEventList = loadedUIData.GetInstance<NKCPopupWorldMapEventList>();
					this._UIEventList.Init();
					NKCUtil.SetGameobjectActive(this._UIEventList, false);
				}
				return this._UIEventList;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x0028D9F8 File Offset: 0x0028BBF8
		private NKCPopupTopPlayer UITopPlayer
		{
			get
			{
				if (this._UITopPlayer == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupTopPlayer>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL", "NKM_UI_POPUP_WORLD_MAP_RENEWAL_RANKING", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this._UITopPlayer = loadedUIData.GetInstance<NKCPopupTopPlayer>();
					this._UITopPlayer.Init();
					NKCUtil.SetGameobjectActive(this._UITopPlayer, false);
				}
				return this._UITopPlayer;
			}
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x0028DA53 File Offset: 0x0028BC53
		private bool IsOpenUITopPlayer()
		{
			return !(this._UITopPlayer == null) && this.UITopPlayer.IsOpen;
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x0028DA70 File Offset: 0x0028BC70
		public NKCPopupTopPlayer GetUITopPlayer()
		{
			return this.UITopPlayer;
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06007A8E RID: 31374 RVA: 0x0028DA78 File Offset: 0x0028BC78
		private NKMWorldMapData WorldmapData
		{
			get
			{
				return NKCScenManager.CurrentUserData().m_WorldmapData;
			}
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x0028DA84 File Offset: 0x0028BC84
		public void CloseCityManagementUI()
		{
			if (this.m_UICityManagement != null)
			{
				this.m_UICityManagement.Close();
			}
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x0028DAA0 File Offset: 0x0028BCA0
		public override void CloseInternal()
		{
			if (this.m_UICityManagement != null)
			{
				this.m_UICityManagement.Close();
			}
			base.gameObject.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_UIWorldmapBack, false);
			if (NKCPopupWarning.IsInstanceOpen)
			{
				NKCPopupWarning.Instance.Close();
			}
			if (this.m_crtSDStartCameraMove != null)
			{
				base.StopCoroutine(this.m_crtSDStartCameraMove);
				this.m_crtSDStartCameraMove = null;
			}
			this.ClearCoroutineChangeSceneAfterFadeout();
			NKCCamera.DisableFocusBlur();
			NKCCamera.TurnOffCrashUpDown();
			NKCUIManager.SetScreenInputBlock(false);
		}

		// Token: 0x06007A91 RID: 31377 RVA: 0x0028DB20 File Offset: 0x0028BD20
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			UnityEngine.Object.Destroy(this.m_UIWorldmapBack.gameObject);
		}

		// Token: 0x06007A92 RID: 31378 RVA: 0x0028DB38 File Offset: 0x0028BD38
		public override void OnBackButton()
		{
			if (this.m_UICityManagement.IsOpen)
			{
				this.m_UICityManagement.Close();
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x0028DB5F File Offset: 0x0028BD5F
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_UIWorldmapBack, false);
		}

		// Token: 0x06007A94 RID: 31380 RVA: 0x0028DB74 File Offset: 0x0028BD74
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_UIWorldmapBack, true);
			NKCUtil.SetGameobjectActive(this.m_objDiveLock, !NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0));
			this.m_UIWorldmapBack.SetData(NKCScenManager.CurrentUserData().m_WorldmapData);
			if (this.m_UICityManagement != null && this.m_UICityManagement.IsOpen)
			{
				this.m_UICityManagement.Unhide();
			}
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x0028DBE8 File Offset: 0x0028BDE8
		public void Init()
		{
			if (this.m_UIWorldmapBack != null)
			{
				this.m_UIWorldmapBack.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas), false);
				this.m_UIWorldmapBack.transform.position = Vector3.zero;
				this.m_UIWorldmapBack.Init(new NKCUIWorldMapCity.OnClickCity(this.SelectCity), new NKCUIWorldMapCityEventPin.OnClickEvent(this.OnSelectEvent));
			}
			else
			{
				Debug.LogError("Worldmap Back Not Found!!!");
			}
			if (this.m_UICityManagement != null)
			{
				this.m_UICityManagement.Init(new NKCUIWorldMapCityDetail.OnSelectNextCity(this.SelectNextCity), new NKCUIWorldMapCityDetail.OnExit(this.UnselectCity));
			}
			if (this.m_csbtnEventList != null)
			{
				this.m_csbtnEventList.PointerClick.RemoveAllListeners();
				this.m_csbtnEventList.PointerClick.AddListener(new UnityAction(this.OnClickEventList));
			}
			else
			{
				Debug.LogError("WorldMap : EventListBtn Not Connected");
			}
			if (this.m_csbtnRaidDeckSetup != null)
			{
				this.m_csbtnRaidDeckSetup.PointerClick.RemoveAllListeners();
				this.m_csbtnRaidDeckSetup.PointerClick.AddListener(new UnityAction(this.OnClickRaidDeckSetup));
			}
			else
			{
				Debug.LogError("WorldMap : RaidDeckSetupBtn Not Connected");
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnShopShortcut, new UnityAction(this.OnClickShopShortcut));
			if (this.m_csbtnDive != null)
			{
				this.m_csbtnDive.PointerClick.RemoveAllListeners();
				this.m_csbtnDive.PointerClick.AddListener(new UnityAction(this.OnClickDive));
				return;
			}
			Debug.LogError("WorldMap : DiveBtn Not Connected");
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x0028DD78 File Offset: 0x0028BF78
		public void Open(bool bDiveReverseAni)
		{
			this.m_bWaitingRaidTopPlayer = false;
			this.m_coopRaidDataList = new List<NKMCoopRaidData>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(this.m_UIWorldmapBack, true);
			NKCUtil.SetGameobjectActive(this.m_objDiveLock, !NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0));
			this.m_UIWorldmapBack.SetData(nkmuserData.m_WorldmapData);
			this.m_UIWorldmapBack.SetEnableDrag(true);
			NKCUtil.SetGameobjectActive(this.m_objEventListReddot, false);
			bool flag = NKMTutorialManager.IsTutorialCompleted(TutorialStep.RaidEvent, NKCScenManager.CurrentUserData());
			NKCUtil.SetGameobjectActive(this.m_csbtnEventList, flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnRaidDeckSetup, flag);
			if (flag)
			{
				NKCPacketSender.Send_NKMPacket_RAID_COOP_LIST_REQ();
			}
			this.SetDiveButtonProgress();
			this.UnselectCity();
			base.UIOpened(true);
			this.ShowReservedWarning();
			if (bDiveReverseAni)
			{
				this.m_UIWorldmapBack.m_amtorWorldmapBack.Play("NKM_UI_WORLD_MAP_RENEWAL_DIVEINTRO_REVERSE");
			}
			this.TutorialCheck();
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x0028DE50 File Offset: 0x0028C050
		private void CleanUpEventPinSpineSD(int cityID)
		{
			this.m_UIWorldmapBack.CleanUpEventPinSpineSD(cityID);
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x0028DE60 File Offset: 0x0028C060
		private void DoAfterCloseWarning(bool bCanceled)
		{
			Debug.Log("DoAfterCloseWarning " + bCanceled.ToString());
			this.m_UIWorldmapBack.PlayPinSDAniByCityID(NKCUIWorldMap.m_ReservedPinIntroCityID, NKCASUIUnitIllust.eAnimation.SD_START, false);
			if (!bCanceled)
			{
				this.m_crtSDStartCameraMove = base.StartCoroutine(this.ProcessSDStartCamera(NKCUIWorldMap.m_ReservedPinIntroCityID));
			}
			else
			{
				this.m_UIWorldmapBack.SetEnableDrag(true);
			}
			NKCUIWorldMap.m_ReservedPinIntroCityID = -1;
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x0028DEC7 File Offset: 0x0028C0C7
		private IEnumerator ProcessSDStartCamera(int cityID)
		{
			NKCUIManager.SetScreenInputBlock(true);
			NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(cityID);
			if (cityData != null)
			{
				NKMWorldMapEventTemplet cNKMWorldMapEventTemplet = NKMWorldMapEventTemplet.Find(cityData.worldMapEventGroup.worldmapEventID);
				if (cNKMWorldMapEventTemplet != null)
				{
					this.m_eCurrentSDCameraEventType = cNKMWorldMapEventTemplet.eventType;
					if (cNKMWorldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
					{
						if (cNKMWorldMapEventTemplet.raidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
						{
							NKCSoundManager.PlaySound("FX_UI_WORLDMAP_BOSS_RAID_HAPPEN", 1f, 0f, 0f, false, 0f, false, 0f);
							Vector3 cameraOrgPos = new Vector3(NKCCamera.GetPosNowX(false), NKCCamera.GetPosNowY(false), NKCCamera.GetPosNowZ(false));
							Vector3 pinSDPos = this.m_UIWorldmapBack.GetPinSDPos(cityID);
							NKCCamera.TrackingPos(0.6f, pinSDPos.x, pinSDPos.y, -300f);
							NKCCamera.SetFocusBlur(1.7f, 0.5f, 0.5f, 0f);
							yield return new WaitForSecondsWithCancel(1.6f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
							NKCCamera.TrackingPos(0.6f, cameraOrgPos.x, cameraOrgPos.y, cameraOrgPos.z);
							yield return new WaitForSecondsWithCancel(0.6f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
							this.m_UIWorldmapBack.SetEnableDrag(true);
							NKCUIManager.SetScreenInputBlock(false);
							this.CheckTutorialEvent(cNKMWorldMapEventTemplet.eventType);
							cameraOrgPos = default(Vector3);
						}
						else
						{
							NKCSoundManager.PlaySound("FX_UI_WORLDMAP_BOSS_RAID_HAPPEN", 1f, 0f, 0f, false, 0f, false, 0f);
							Vector3 cameraOrgPos = new Vector3(NKCCamera.GetPosNowX(false), NKCCamera.GetPosNowY(false), NKCCamera.GetPosNowZ(false));
							Vector3 pinSDPos2 = this.m_UIWorldmapBack.GetPinSDPos(cityID);
							NKCCamera.TrackingPos(0.6f, pinSDPos2.x, pinSDPos2.y, -300f);
							NKCCamera.SetFocusBlur(3.7f, 0.5f, 0.5f, 0f);
							yield return new WaitForSecondsWithCancel(2.2f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
							NKCCamera.UpDownCrashCamera(5f, 1.5f, 0.025f);
							yield return new WaitForSecondsWithCancel(1.6f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
							NKCCamera.TrackingPos(0.6f, cameraOrgPos.x, cameraOrgPos.y, cameraOrgPos.z);
							yield return new WaitForSecondsWithCancel(0.6f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
							this.m_UIWorldmapBack.SetEnableDrag(true);
							NKCUIManager.SetScreenInputBlock(false);
							this.CheckTutorialEvent(cNKMWorldMapEventTemplet.eventType);
							cameraOrgPos = default(Vector3);
						}
					}
					else if (cNKMWorldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
					{
						NKCSoundManager.PlaySound("FX_UI_WORLDMAP_DIVE_HAPPEN", 1f, 0f, 0f, false, 0f, false, 0f);
						Vector3 cameraOrgPos = new Vector3(NKCCamera.GetPosNowX(false), NKCCamera.GetPosNowY(false), NKCCamera.GetPosNowZ(false));
						Vector3 pinSDPos3 = this.m_UIWorldmapBack.GetPinSDPos(cityID);
						NKCCamera.TrackingPos(0.6f, pinSDPos3.x, pinSDPos3.y, -400f);
						yield return new WaitForSecondsWithCancel(3.3f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
						NKCCamera.TrackingPos(0.6f, cameraOrgPos.x, cameraOrgPos.y, cameraOrgPos.z);
						yield return new WaitForSecondsWithCancel(0.6f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
						this.m_UIWorldmapBack.SetEnableDrag(true);
						NKCUIManager.SetScreenInputBlock(false);
						this.CheckTutorialEvent(cNKMWorldMapEventTemplet.eventType);
						cameraOrgPos = default(Vector3);
					}
				}
				cNKMWorldMapEventTemplet = null;
			}
			yield break;
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x0028DEDD File Offset: 0x0028C0DD
		private bool CanSkipSDCamera()
		{
			return Input.anyKeyDown && this.IsTutorialForEventCompleted(this.m_eCurrentSDCameraEventType);
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x0028DEF4 File Offset: 0x0028C0F4
		public void ShowReservedWarning()
		{
			if (NKCUIWorldMap.m_Reserved_NKC_WORLD_MAP_WARNING_TYPE == NKC_WORLD_MAP_WARNING_TYPE.NWMWT_RAID)
			{
				NKCPopupWarning.Instance.Open(NKCUtilString.GET_STRING_WORLDMAP_RAID_WARNING, new NKCPopupWarning.OnClose(this.DoAfterCloseWarning));
				this.CleanUpEventPinSpineSD(NKCUIWorldMap.m_ReservedWarningCityID);
				NKCUIWorldMap.m_ReservedPinIntroCityID = NKCUIWorldMap.m_ReservedWarningCityID;
				this.m_UIWorldmapBack.SetEnableDrag(false);
			}
			else if (NKCUIWorldMap.m_Reserved_NKC_WORLD_MAP_WARNING_TYPE == NKC_WORLD_MAP_WARNING_TYPE.NWMWT_DIVE)
			{
				NKCPopupWarning.Instance.Open(NKCUtilString.GET_STRING_WORLDMAP_DIVE_WARNING, new NKCPopupWarning.OnClose(this.DoAfterCloseWarning));
				this.CleanUpEventPinSpineSD(NKCUIWorldMap.m_ReservedWarningCityID);
				NKCUIWorldMap.m_ReservedPinIntroCityID = NKCUIWorldMap.m_ReservedWarningCityID;
				this.m_UIWorldmapBack.SetEnableDrag(false);
			}
			NKCUIWorldMap.m_Reserved_NKC_WORLD_MAP_WARNING_TYPE = NKC_WORLD_MAP_WARNING_TYPE.NWMWT_NONE;
			NKCUIWorldMap.m_ReservedWarningCityID = -1;
		}

		// Token: 0x06007A9C RID: 31388 RVA: 0x0028DF98 File Offset: 0x0028C198
		private void SetDiveButtonProgress()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool bValue = false;
			if (nkmuserData.m_DiveGameData != null && !nkmuserData.m_DiveGameData.Floor.Templet.IsEventDive)
			{
				bValue = true;
			}
			NKCUtil.SetGameobjectActive(this.m_objDiveOnProgress, bValue);
			NKCUtil.SetGameobjectActive(this.m_objDiveLock, !NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0));
			int num;
			NKMDiveTemplet currNormalDiveTemplet = NKCDiveManager.GetCurrNormalDiveTemplet(out num);
			if (currNormalDiveTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbDiveDepth, currNormalDiveTemplet.Get_STAGE_NAME_SUB());
				return;
			}
			NKCUtil.SetLabelText(this.m_lbDiveDepth, string.Empty);
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x0028E01E File Offset: 0x0028C21E
		private void SelectCity(int cityID)
		{
			if (cityID == 0)
			{
				this.m_UICityManagement.Close();
				return;
			}
			if (this.IsCityUnlocked(cityID))
			{
				this._OnSelectCity(cityID);
				return;
			}
			this.UICityUnlock.Open(cityID);
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x0028E04C File Offset: 0x0028C24C
		private void _OnSelectCity(int CityID)
		{
			NKMWorldMapCityData cityData = this.WorldmapData.GetCityData(CityID);
			if (cityData != null && cityData.worldMapMission.completeTime != 0L && NKCSynchronizedTime.IsFinished(cityData.worldMapMission.completeTime))
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_MISSION_COMPLETE_REQ(CityID);
				return;
			}
			this.m_UICityManagement.Open(cityData, delegate
			{
				NKCUtil.SetGameobjectActive(this.m_UIWorldmapBack, false);
			}, delegate
			{
				NKCUtil.SetGameobjectActive(this.m_UIWorldmapBack, true);
				this.m_UIWorldmapBack.SetData(NKCScenManager.CurrentUserData().m_WorldmapData);
			});
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x0028E0C0 File Offset: 0x0028C2C0
		private void SelectNextCity(int currentCityID, bool bForward)
		{
			List<int> list = new List<int>(this.WorldmapData.worldMapCityDataMap.Keys);
			list.Sort();
			int num = list.IndexOf(currentCityID);
			if (num < 0)
			{
				Debug.LogError("Selected city not found");
				return;
			}
			if (list.Count <= 1)
			{
				return;
			}
			int num2 = bForward ? 1 : -1;
			for (int i = 1; i < list.Count; i++)
			{
				int index = num + num2 * i;
				index = NKCUtil.CalculateNormalizedIndex(index, list.Count);
				int cityID = list[index];
				if (this.IsCityUnlocked(cityID))
				{
					this.SelectCity(cityID);
					return;
				}
			}
		}

		// Token: 0x06007AA0 RID: 31392 RVA: 0x0028E156 File Offset: 0x0028C356
		public void UnselectCity()
		{
			this.SelectCity(0);
		}

		// Token: 0x06007AA1 RID: 31393 RVA: 0x0028E15F File Offset: 0x0028C35F
		private bool IsCityUnlocked(int cityID)
		{
			return NKCScenManager.CurrentUserData().m_WorldmapData.IsCityUnlocked(cityID);
		}

		// Token: 0x06007AA2 RID: 31394 RVA: 0x0028E174 File Offset: 0x0028C374
		private void OnSelectEvent(int cityID, int eventID, long eventUID)
		{
			NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				Debug.LogError(string.Format("City not found. ID : {0}", cityID));
				return;
			}
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(eventID);
			if (nkmworldMapEventTemplet == null)
			{
				Debug.LogError(string.Format("EventTemplet Null! ID : {0}", eventID));
				return;
			}
			NKM_WORLDMAP_EVENT_TYPE eventType = nkmworldMapEventTemplet.eventType;
			if (eventType != NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					bool flag = NKCScenManager.CurrentUserData().m_DiveGameData != null && NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == cityData.worldMapEventGroup.eventUid;
					if (NKCSynchronizedTime.IsFinished(cityData.worldMapEventGroup.eventGroupEndDate) || (cityData.worldMapEventGroup.eventUid > 0L && !flag))
					{
						NKCPacketSender.Send_NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ(cityData.cityID);
						return;
					}
					this.UIEventList.Close();
					this.ShowDivingAndGotoDiveScen(cityID, nkmworldMapEventTemplet.stageID);
					return;
				}
			}
			else if (NKCScenManager.GetScenManager().GetNKCRaidDataMgr().CheckCompletableRaid(cityData.worldMapEventGroup.eventUid))
			{
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(cityData.worldMapEventGroup.eventUid);
				if (nkmraidDetailData == null || nkmraidDetailData.curHP > 0f)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(eventUID);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
					return;
				}
				if (!this.m_bWaitingRaidTopPlayer)
				{
					this.m_bWaitingRaidTopPlayer = true;
					NKCPacketSender.Send_NKMPacket_RAID_DETAIL_INFO_REQ(cityData.worldMapEventGroup.eventUid);
					return;
				}
			}
			else
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(eventUID);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
			}
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x0028E2F6 File Offset: 0x0028C4F6
		private void ShowDivingAndGotoDiveScen(int cityID = 0, int eventDiveID = 0)
		{
			this.ChangeSceneAfterDiveEffect(ContentsType.DIVE, cityID, eventDiveID);
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x0028E302 File Offset: 0x0028C502
		private void OnClickDive()
		{
			if (NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0))
			{
				this.ShowDivingAndGotoDiveScen(0, 0);
				return;
			}
			NKCContentManager.ShowLockedMessagePopup(ContentsType.DIVE, 0);
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x0028E321 File Offset: 0x0028C521
		private void OnClickShadow()
		{
			if (NKCContentManager.IsContentsUnlocked(ContentsType.SHADOW_PALACE, 0, 0))
			{
				this.ChangeSceneAfterDiveEffect(ContentsType.SHADOW_PALACE, 0, 0);
				return;
			}
			NKCContentManager.ShowLockedMessagePopup(ContentsType.SHADOW_PALACE, 0);
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x0028E344 File Offset: 0x0028C544
		private void OnClickTrim()
		{
			if (!NKCUITrimUtility.OpenTagEnabled)
			{
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIMENSION_TRIM, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.DIMENSION_TRIM, 0);
				return;
			}
			if (NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime) == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_NOT_INTERVAL_TIME, null, "");
				return;
			}
			if (NKCTrimManager.TrimModeState != null)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_EXIST_TRIM_COMBAT_DATA, delegate()
				{
					NKCTrimManager.ProcessTrim();
				}, null, false);
				return;
			}
			this.ChangeSceneAfterDiveEffect(ContentsType.DIMENSION_TRIM, 0, 0);
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x0028E3D2 File Offset: 0x0028C5D2
		private void ChangeSceneAfterDiveEffect(ContentsType type, int cityID = 0, int eventDiveID = 0)
		{
			this.ClearCoroutineChangeSceneAfterFadeout();
			this.m_UIWorldmapBack.m_amtorWorldmapBack.Play("NKM_UI_WORLD_MAP_RENEWAL_DIVEINTRO");
			this.m_crtChangeSceneAfterFadeout = base.StartCoroutine(this.ChangeSceneAfterFadeout(type, cityID, eventDiveID));
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x0028E404 File Offset: 0x0028C604
		private void ClearCoroutineChangeSceneAfterFadeout()
		{
			if (this.m_crtChangeSceneAfterFadeout != null)
			{
				base.StopCoroutine(this.m_crtChangeSceneAfterFadeout);
				this.m_crtChangeSceneAfterFadeout = null;
			}
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x0028E421 File Offset: 0x0028C621
		private IEnumerator ChangeSceneAfterFadeout(ContentsType type, int cityID = 0, int eventDiveID = 0)
		{
			yield return new WaitForSeconds(0.25f);
			NKCUIFadeInOut.FadeOut(0.25f, delegate
			{
				NKCScenManager.GetScenManager().SetSkipScenChangeFadeOutEffect(true);
				ContentsType type2 = type;
				if (type2 <= ContentsType.SHADOW_PALACE)
				{
					if (type2 == ContentsType.DIVE)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OpenDiveSearch(cityID, eventDiveID);
						return;
					}
					if (type2 != ContentsType.SHADOW_PALACE)
					{
						return;
					}
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OpenShadowPalace();
					return;
				}
				else
				{
					if (type2 == ContentsType.FIERCE)
					{
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT, true);
						return;
					}
					if (type2 != ContentsType.DIMENSION_TRIM)
					{
						return;
					}
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TRIM, true);
					return;
				}
			}, false, -1f);
			this.m_crtChangeSceneAfterFadeout = null;
			yield break;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x0028E445 File Offset: 0x0028C645
		private bool IsOpenUIEventList()
		{
			return !(this._UIEventList == null) && this.UIEventList.IsOpen;
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x0028E462 File Offset: 0x0028C662
		private void OnClickEventList()
		{
			this.OpenEventList();
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x0028E46A File Offset: 0x0028C66A
		public void OpenEventList()
		{
			this.UIEventList.Open(this.WorldmapData, new NKCPopupWorldMapEventList.OnSelectEvent(this.OnSelectEvent), this.m_coopRaidDataList.Count > 0);
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x0028E497 File Offset: 0x0028C697
		private void OnClickRaidDeckSetup()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetRaidUID(0L);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetGuildRaid(false);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID_READY, true);
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x0028E4C7 File Offset: 0x0028C6C7
		private void OnClickShopShortcut()
		{
			NKCUIShop.ShopShortcut("TAB_EXCHANGE_RAID_COIN", 0, 0);
		}

		// Token: 0x06007AAF RID: 31407 RVA: 0x0028E4D5 File Offset: 0x0028C6D5
		public void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ACK cNKMPacket_RAID_RESULT_ACCEPT_ACK, int cityID)
		{
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(cNKMPacket_RAID_RESULT_ACCEPT_ACK, cityID);
			}
		}

		// Token: 0x06007AB0 RID: 31408 RVA: 0x0028E4EC File Offset: 0x0028C6EC
		public void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ALL_ACK sPacket, List<int> lstCity)
		{
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(sPacket, lstCity);
			}
		}

		// Token: 0x06007AB1 RID: 31409 RVA: 0x0028E504 File Offset: 0x0028C704
		public void OnRecv(NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK)
		{
			if (base.IsOpen)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK.cityID);
					if (cityData != null)
					{
						this.CityDataUpdated(cityData);
					}
				}
			}
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK);
			}
		}

		// Token: 0x06007AB2 RID: 31410 RVA: 0x0028E554 File Offset: 0x0028C754
		public void OnRecv(NKMPacket_WORLDMAP_EVENT_CANCEL_ACK cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK)
		{
			if (base.IsOpen)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK.cityID);
					if (cityData != null)
					{
						this.CityDataUpdated(cityData);
					}
				}
			}
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK);
			}
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x0028E5A2 File Offset: 0x0028C7A2
		public void OnRecv(NKMPacket_RAID_RESULT_LIST_ACK cNKMPacket_RAID_RESULT_LIST_ACK)
		{
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(cNKMPacket_RAID_RESULT_LIST_ACK);
			}
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x0028E5B8 File Offset: 0x0028C7B8
		public void OnRecv(NKMPacket_RAID_COOP_LIST_ACK cNKMPacket_RAID_COOP_LIST_ACK)
		{
			if (cNKMPacket_RAID_COOP_LIST_ACK != null && cNKMPacket_RAID_COOP_LIST_ACK.coopRaidDataList != null)
			{
				this.m_coopRaidDataList = cNKMPacket_RAID_COOP_LIST_ACK.coopRaidDataList;
			}
			NKCUtil.SetGameobjectActive(this.m_objEventListReddot, this.m_coopRaidDataList.Count > 0 || NKCAlarmManager.CheckRaidSeasonRewardNotify(NKCScenManager.CurrentUserData()));
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(cNKMPacket_RAID_COOP_LIST_ACK);
			}
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x0028E616 File Offset: 0x0028C816
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			this.SetDiveButtonProgress();
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
			}
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x0028E634 File Offset: 0x0028C834
		public void OnRecv(NKMRaidDetailData raidDetailData)
		{
			this.m_UIWorldmapBack.UpdateCityRaidData(raidDetailData);
			if (this.m_bWaitingRaidTopPlayer)
			{
				this.m_bWaitingRaidTopPlayer = false;
				NKMRaidTemplet raidTemplet = NKMRaidTemplet.Find(raidDetailData.stageID);
				this.OpenTopPlayerPopup(raidTemplet, raidDetailData.raidJoinDataList, raidDetailData.raidUID);
			}
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x0028E67B File Offset: 0x0028C87B
		public void OnRecv(NKMPacket_RAID_SEASON_NOT sPacket)
		{
			NKCUtil.SetGameobjectActive(this.m_objEventListReddot, this.m_coopRaidDataList.Count > 0 || NKCAlarmManager.CheckRaidSeasonRewardNotify(NKCScenManager.CurrentUserData()));
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.OnRecv(sPacket);
			}
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x0028E6B7 File Offset: 0x0028C8B7
		public void RefreshEventList()
		{
			if (this.IsOpenUIEventList())
			{
				this.UIEventList.RefreshUI();
			}
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x0028E6CC File Offset: 0x0028C8CC
		public void OpenTopPlayerPopup(NKMRaidTemplet raidTemplet, List<NKMRaidJoinData> raidJoinDataList, long raidUID)
		{
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(raidUID);
			if (nkmraidDetailData == null)
			{
				return;
			}
			if (NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate) && nkmraidDetailData.curHP > 0f)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(raidUID);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
				return;
			}
			string title_ = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, raidTemplet.RaidLevel);
			string dungeonName = raidTemplet.DungeonTempletBase.GetDungeonName();
			string @string = NKCStringTable.GetString("SI_PF_WORLD_MAP_RENEWAL_RANKING_TITLE", false);
			this.UITopPlayer.Open(title_, dungeonName, @string, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_FACE_CARD", raidTemplet.FaceCardName, false), LeaderBoardSlotData.MakeSlotDataList(raidJoinDataList, raidTemplet.RaidTryCount), new List<NKMEmblemData>(), raidUID, delegate(long uid)
			{
				NKCPacketSender.Send_NKMPacket_RAID_RESULT_ACCEPT_REQ(uid);
			});
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x0028E7A4 File Offset: 0x0028C9A4
		public void OnWorldManCitySet(int cityID, NKMWorldMapCityData cityData)
		{
			if (this._UICityUnlock != null && this._UICityUnlock.IsOpen)
			{
				this._UICityUnlock.Close();
			}
			this.m_UIWorldmapBack.UpdateCity(cityID, cityData);
			this.SelectCity(cityID);
			this.ShowAreaUnlockedEffect();
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x0028E7F1 File Offset: 0x0028C9F1
		public void CityLeaderChanged(NKMWorldMapCityData cityData)
		{
			if (cityData == null)
			{
				return;
			}
			this.m_UIWorldmapBack.UpdateCity(cityData.cityID, cityData);
			if (this.m_UICityManagement != null && this.m_UICityManagement.IsOpen)
			{
				this.m_UICityManagement.CityDataUpdated(cityData);
			}
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x0028E830 File Offset: 0x0028CA30
		public void CityDataUpdated(NKMWorldMapCityData cityData)
		{
			if (cityData == null)
			{
				return;
			}
			this.m_UIWorldmapBack.UpdateCity(cityData.cityID, cityData);
			if (this.m_UICityManagement != null && this.m_UICityManagement.IsOpen)
			{
				this.m_UICityManagement.CityDataUpdated(cityData);
			}
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x0028E86F File Offset: 0x0028CA6F
		public void CityEventSpawned(int cityID)
		{
			this.m_UIWorldmapBack.CityEventSpawned(cityID);
		}

		// Token: 0x06007ABE RID: 31422 RVA: 0x0028E87D File Offset: 0x0028CA7D
		private void ShowAreaUnlockedEffect()
		{
			NKCUICompleteEffect.Instance.OpenCityOpened(4f);
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x0028E88E File Offset: 0x0028CA8E
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (this.m_UICityManagement.IsOpen)
			{
				this.m_UICityManagement.OnInventoryChange(itemData);
			}
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x0028E8A9 File Offset: 0x0028CAA9
		public void SetFXBuildingID(int buildingID)
		{
			if (this.m_UICityManagement != null && this.m_UICityManagement.IsOpen)
			{
				this.m_UICityManagement.SetFXBuildingID(buildingID);
			}
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x0028E8D2 File Offset: 0x0028CAD2
		public RectTransform GetPinRect(int cityID)
		{
			return this.m_UIWorldmapBack.GetPinRect(cityID);
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x0028E8E0 File Offset: 0x0028CAE0
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.WorldMap, true);
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x0028E8EB File Offset: 0x0028CAEB
		private bool IsTutorialForEventCompleted(NKM_WORLDMAP_EVENT_TYPE type)
		{
			if (type != NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				return type != NKM_WORLDMAP_EVENT_TYPE.WET_DIVE || NKCTutorialManager.TutorialRequired(TutorialPoint.DiveWarning, true) == TutorialStep.None;
			}
			return NKCTutorialManager.TutorialRequired(TutorialPoint.RaidWarning, false) == TutorialStep.None;
		}

		// Token: 0x06007AC4 RID: 31428 RVA: 0x0028E90F File Offset: 0x0028CB0F
		private void CheckTutorialEvent(NKM_WORLDMAP_EVENT_TYPE type)
		{
			if (type == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.RaidWarning, true);
				return;
			}
			if (type == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.DiveWarning, true);
			}
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x0028E92C File Offset: 0x0028CB2C
		public static NKCASUIUnitIllust.eAnimation GetMissionProgressAnimationType(NKMWorldMapMissionTemplet.WorldMapMissionType missionType)
		{
			switch (missionType)
			{
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_EXPLORE:
				return NKCASUIUnitIllust.eAnimation.SD_RUN;
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_DEFENCE:
				return NKCASUIUnitIllust.eAnimation.SD_ATTACK;
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_MINING:
				return NKCASUIUnitIllust.eAnimation.SD_MINING;
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_OFFICE:
				return NKCASUIUnitIllust.eAnimation.SD_WORKING;
			}
			Debug.LogError("Mission templet missiontype undefined");
			return NKCASUIUnitIllust.eAnimation.SD_WALK;
		}

		// Token: 0x04006763 RID: 26467
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x04006764 RID: 26468
		private const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RENEWAL_FRONT";

		// Token: 0x04006765 RID: 26469
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04006766 RID: 26470
		private readonly List<int> lstResources = new List<int>
		{
			3,
			1,
			2,
			101
		};

		// Token: 0x04006767 RID: 26471
		public NKCUIWorldMapBack m_UIWorldmapBack;

		// Token: 0x04006768 RID: 26472
		public NKCUIWorldMapCityManagement m_UICityManagement;

		// Token: 0x04006769 RID: 26473
		public NKCUIComStateButton m_csbtnEventList;

		// Token: 0x0400676A RID: 26474
		public NKCUIComStateButton m_csbtnRaidDeckSetup;

		// Token: 0x0400676B RID: 26475
		public GameObject m_objEventListReddot;

		// Token: 0x0400676C RID: 26476
		public NKCUIComStateButton m_csbtnShopShortcut;

		// Token: 0x0400676D RID: 26477
		public NKCUIComStateButton m_csbtnDive;

		// Token: 0x0400676E RID: 26478
		public GameObject m_objDiveOnProgress;

		// Token: 0x0400676F RID: 26479
		public GameObject m_objDiveLock;

		// Token: 0x04006770 RID: 26480
		public Text m_lbDiveDepth;

		// Token: 0x04006771 RID: 26481
		private static NKC_WORLD_MAP_WARNING_TYPE m_Reserved_NKC_WORLD_MAP_WARNING_TYPE = NKC_WORLD_MAP_WARNING_TYPE.NWMWT_NONE;

		// Token: 0x04006772 RID: 26482
		private static int m_ReservedWarningCityID = -1;

		// Token: 0x04006773 RID: 26483
		private static int m_ReservedPinIntroCityID = -1;

		// Token: 0x04006774 RID: 26484
		private List<NKMCoopRaidData> m_coopRaidDataList = new List<NKMCoopRaidData>();

		// Token: 0x04006775 RID: 26485
		private bool m_bWaitingRaidTopPlayer;

		// Token: 0x04006776 RID: 26486
		[Header("SUB UIs")]
		private NKCPopupWorldMapCityUnlock _UICityUnlock;

		// Token: 0x04006777 RID: 26487
		private NKCPopupWorldMapEventList _UIEventList;

		// Token: 0x04006778 RID: 26488
		private NKCPopupTopPlayer _UITopPlayer;

		// Token: 0x04006779 RID: 26489
		private bool m_bShowIntro;

		// Token: 0x0400677A RID: 26490
		private Coroutine m_crtSDStartCameraMove;

		// Token: 0x0400677B RID: 26491
		private NKM_WORLDMAP_EVENT_TYPE m_eCurrentSDCameraEventType = NKM_WORLDMAP_EVENT_TYPE.WET_NONE;

		// Token: 0x0400677C RID: 26492
		private Coroutine m_crtChangeSceneAfterFadeout;
	}
}
