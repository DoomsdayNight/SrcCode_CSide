using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using NKC.PacketHandler;
using NKC.UI;
using NKC.UI.Worldmap;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000734 RID: 1844
	public class NKC_SCEN_WORLDMAP : NKC_SCEN_BASIC
	{
		// Token: 0x06004982 RID: 18818 RVA: 0x0016179D File Offset: 0x0015F99D
		public void SetShowIntro()
		{
			this.m_bShowIntro = true;
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x001617A6 File Offset: 0x0015F9A6
		public void SetReserveOpenEventList()
		{
			this.m_bReserveOpenEventList = true;
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x001617AF File Offset: 0x0015F9AF
		public void SetReservedDiveReverseAni(bool bSet)
		{
			this.m_bReservedDiveReverseAni = bSet;
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x001617B8 File Offset: 0x0015F9B8
		public NKCPopupWorldMapBuildingInfo PopupWorldMapBuildingInfo
		{
			get
			{
				if (this.m_PopupWorldMapBuildingInfo == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWorldMapBuildingInfo>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_WORLD_MAP_RENEWAL_BUILDING_INFO_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_PopupWorldMapBuildingInfo = null;
					});
					this.m_PopupWorldMapBuildingInfo = loadedUIData.GetInstance<NKCPopupWorldMapBuildingInfo>();
					NKCPopupWorldMapBuildingInfo popupWorldMapBuildingInfo = this.m_PopupWorldMapBuildingInfo;
					if (popupWorldMapBuildingInfo != null)
					{
						popupWorldMapBuildingInfo.Init();
					}
				}
				return this.m_PopupWorldMapBuildingInfo;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x00161818 File Offset: 0x0015FA18
		public bool IsOpenPopupWorldMapBuildingInfo
		{
			get
			{
				return !(this.m_PopupWorldMapBuildingInfo == null) && this.m_PopupWorldMapBuildingInfo.IsOpen;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x00161838 File Offset: 0x0015FA38
		public NKCPopupWorldMapNewBuildingList PopupWorldMapNewBuildingList
		{
			get
			{
				if (this.m_PopupWorldMapNewBuildingList == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWorldMapNewBuildingList>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_WORLD_MAP_RENEWAL_BUILDING_LIST_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_PopupWorldMapNewBuildingList = null;
					});
					this.m_PopupWorldMapNewBuildingList = loadedUIData.GetInstance<NKCPopupWorldMapNewBuildingList>();
					NKCPopupWorldMapNewBuildingList popupWorldMapNewBuildingList = this.m_PopupWorldMapNewBuildingList;
					if (popupWorldMapNewBuildingList != null)
					{
						popupWorldMapNewBuildingList.Init();
					}
				}
				return this.m_PopupWorldMapNewBuildingList;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x00161898 File Offset: 0x0015FA98
		public bool IsOpenPopupWorldMapNewBuildingList
		{
			get
			{
				return !(this.m_PopupWorldMapNewBuildingList == null) && this.m_PopupWorldMapNewBuildingList.IsOpen;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x001618B8 File Offset: 0x0015FAB8
		public NKCPopupWorldmapEventOKCancel NKCPopupWorldmapEventOKCancel
		{
			get
			{
				if (this.m_NKCPopupWorldmapEventOKCancel == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWorldmapEventOKCancel>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL", "NKM_UI_WORLD_MAP_POPUP_EventOKCancel", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_NKCPopupWorldmapEventOKCancel = null;
					});
					this.m_NKCPopupWorldmapEventOKCancel = loadedUIData.GetInstance<NKCPopupWorldmapEventOKCancel>();
					NKCPopupWorldmapEventOKCancel nkcpopupWorldmapEventOKCancel = this.m_NKCPopupWorldmapEventOKCancel;
					if (nkcpopupWorldmapEventOKCancel != null)
					{
						nkcpopupWorldmapEventOKCancel.InitUI();
					}
				}
				return this.m_NKCPopupWorldmapEventOKCancel;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x00161918 File Offset: 0x0015FB18
		public NKC_SCEN_WORLDMAP()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_WORLDMAP;
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x00161928 File Offset: 0x0015FB28
		public void SetReservedWarning(NKC_WORLD_MAP_WARNING_TYPE type, int cityID)
		{
			NKCUIWorldMap.SetReservedWarning(type, cityID);
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00161931 File Offset: 0x0015FB31
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			Debug.Log("Worldmap Video Callback : " + message.ToString());
			switch (message)
			{
			case NKCUIComVideoPlayer.eVideoMessage.PlayFailed:
			case NKCUIComVideoPlayer.eVideoMessage.PlayComplete:
				this.m_bWaitingMovie = false;
				break;
			case NKCUIComVideoPlayer.eVideoMessage.PlayBegin:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x00161969 File Offset: 0x0015FB69
		public void DoAfterLogout()
		{
			this.SetReservedWarning(NKC_WORLD_MAP_WARNING_TYPE.NWMWT_NONE, -1);
			NKCUIWorldMap.SetReservedPinIntroCityID(-1);
			this.SetReservedDiveReverseAni(false);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x00161980 File Offset: 0x0015FB80
		public override void ScenDataReq()
		{
			base.ScenDataReq();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.m_WorldmapData.CheckIfHaveSpecificEvent(NKM_WORLDMAP_EVENT_TYPE.WET_RAID))
			{
				NKCPacketSender.Send_NKMPacket_MY_RAID_LIST_REQ();
				return;
			}
			base.ScenDataReqWaitUpdate();
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x001619B6 File Offset: 0x0015FBB6
		public override void ScenDataReqWaitUpdate()
		{
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x001619B8 File Offset: 0x0015FBB8
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.m_bShowIntro)
			{
				NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
				if (subUICameraVideoPlayer != null)
				{
					subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
					subUICameraVideoPlayer.m_fMoviePlaySpeed = 1.5f;
					this.m_bWaitingMovie = true;
					subUICameraVideoPlayer.Play("Worldmap_Intro.mp4", false, false, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), false);
					this.m_introSoundUID = NKCSoundManager.PlaySound("FX_UI_WORLDMAP_INTRO", 1f, 0f, 0f, false, 0f, false, 0f);
				}
				else
				{
					this.m_bWaitingMovie = false;
				}
			}
			if (!NKCUIManager.IsValid(this.m_NKCUIWorldMapUIData))
			{
				this.m_NKCUIWorldMapUIData = NKCUIWorldMap.OpenNewInstanceAsync();
			}
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00161A64 File Offset: 0x0015FC64
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!(this.m_NKCUIWorldMap == null))
			{
				return;
			}
			if (this.m_NKCUIWorldMapUIData != null && this.m_NKCUIWorldMapUIData.CheckLoadAndGetInstance<NKCUIWorldMap>(out this.m_NKCUIWorldMap))
			{
				this.m_NKCUIWorldMap.Init();
				return;
			}
			Debug.LogError("Error - NKC_SCEN_WORLDMAP.ScenLoadComplete() : UI Load Failed!");
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00161AB8 File Offset: 0x0015FCB8
		public override void ScenLoadUpdate()
		{
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			if (this.m_bWaitingMovie)
			{
				if (Input.anyKeyDown)
				{
					Debug.Log("TrySkip");
					NKCSoundManager.StopSound(this.m_introSoundUID);
					if (PlayerPrefs.GetInt("WORLDMAP_LOADING_SKIP", 0) == 1)
					{
						this.m_bWaitingMovie = false;
					}
				}
				return;
			}
			if (PlayerPrefs.GetInt("WORLDMAP_LOADING_SKIP", 0) == 0)
			{
				PlayerPrefs.SetInt("WORLDMAP_LOADING_SKIP", 1);
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00161B28 File Offset: 0x0015FD28
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_bShowIntro)
			{
				NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
				if (subUICameraVideoPlayer != null)
				{
					subUICameraVideoPlayer.Stop();
				}
			}
			this.m_bShowIntro = false;
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00161B5F File Offset: 0x0015FD5F
		public override void ScenStart()
		{
			base.ScenStart();
			this.OpenWorldMap();
			NKCCamera.GetCamera().orthographic = false;
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00161B78 File Offset: 0x0015FD78
		public void OnRecv(NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK)
		{
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.OnRecv(cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK);
			}
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00161B94 File Offset: 0x0015FD94
		public void OnRecv(NKMPacket_WORLDMAP_EVENT_CANCEL_ACK cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK)
		{
			NKCPopupWorldmapEventOKCancel nkcpopupWorldmapEventOKCancel = this.NKCPopupWorldmapEventOKCancel;
			if (nkcpopupWorldmapEventOKCancel != null)
			{
				nkcpopupWorldmapEventOKCancel.Close();
			}
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.OnRecv(cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK);
			}
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x00161BC4 File Offset: 0x0015FDC4
		public void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ACK cNKMPacket_RAID_RESULT_ACCEPT_ACK, int cityID)
		{
			if (cityID >= 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null)
				{
					return;
				}
				NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(cityID);
				if (cityData != null)
				{
					this.CityDataUpdated(cityData);
				}
			}
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.OnRecv(cNKMPacket_RAID_RESULT_ACCEPT_ACK, cityID);
			}
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00161C14 File Offset: 0x0015FE14
		public void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ALL_ACK sPacket, List<int> lstCity)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				foreach (int num in lstCity)
				{
					if (num >= 0)
					{
						NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(num);
						if (cityData != null)
						{
							this.CityDataUpdated(cityData);
						}
					}
				}
			}
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.OnRecv(sPacket, lstCity);
			}
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00161C9C File Offset: 0x0015FE9C
		public void OnRecv(NKMPacket_RAID_RESULT_LIST_ACK cNKMPacket_RAID_RESULT_LIST_ACK)
		{
			if (this.m_NKCUIWorldMap == null)
			{
				return;
			}
			this.m_NKCUIWorldMap.OnRecv(cNKMPacket_RAID_RESULT_LIST_ACK);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00161CB9 File Offset: 0x0015FEB9
		public void OnRecv(NKMPacket_RAID_COOP_LIST_ACK cNKMPacket_RAID_COOP_LIST_ACK)
		{
			if (this.m_NKCUIWorldMap == null)
			{
				return;
			}
			this.m_NKCUIWorldMap.OnRecv(cNKMPacket_RAID_COOP_LIST_ACK);
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00161CD8 File Offset: 0x0015FED8
		public void OnRecv(NKMPacket_MY_RAID_LIST_ACK cNKMPacket_MY_RAID_LIST_ACK)
		{
			if (this.m_NKC_SCEN_STATE == NKC_SCEN_STATE.NSS_DATA_REQ_WAIT)
			{
				base.ScenDataReqWaitUpdate();
				return;
			}
			if (this.m_NKCUIWorldMap == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null || nkmuserData.m_WorldmapData == null)
			{
				return;
			}
			if (this.m_NKCUIWorldMap.IsOpen && cNKMPacket_MY_RAID_LIST_ACK != null && cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList != null)
			{
				for (int i = 0; i < cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList.Count; i++)
				{
					NKMMyRaidData nkmmyRaidData = cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList[i];
					if (nkmmyRaidData != null)
					{
						NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(nkmmyRaidData.cityID);
						if (cityData != null)
						{
							this.CityDataUpdated(cityData);
						}
					}
				}
				return;
			}
			this.OpenWorldMap();
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x00161D77 File Offset: 0x0015FF77
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			if (this.m_NKCUIWorldMap == null)
			{
				return;
			}
			this.m_NKCUIWorldMap.OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x00161D94 File Offset: 0x0015FF94
		public void OnRecv(NKMPacket_RAID_DETAIL_INFO_ACK cNKMPacket_RAID_DETAIL_INFO_ACK)
		{
			if (this.m_NKCUIWorldMap == null)
			{
				return;
			}
			this.m_NKCUIWorldMap.OnRecv(cNKMPacket_RAID_DETAIL_INFO_ACK.raidDetailData);
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00161DB6 File Offset: 0x0015FFB6
		public void OnRecv(NKMPacket_RAID_SEASON_NOT sPacket)
		{
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.OnRecv(sPacket);
			}
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x00161DD4 File Offset: 0x0015FFD4
		private void OpenWorldMap()
		{
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.Open(this.m_bReservedDiveReverseAni);
				if (this.m_bReserveOpenEventList)
				{
					this.m_NKCUIWorldMap.OpenEventList();
				}
			}
			this.m_bReserveOpenEventList = false;
			this.m_bReservedDiveReverseAni = false;
			this.m_bWaitingCompleteAck = false;
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x00161E28 File Offset: 0x00160028
		public void CloseCityManageUI()
		{
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.CloseCityManagementUI();
			}
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x00161E43 File Offset: 0x00160043
		public void ShowReservedWarningUI()
		{
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.ShowReservedWarning();
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x00161E60 File Offset: 0x00160060
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.CloseDiveSearch();
			if (this.m_NKCUIWorldMap != null)
			{
				this.m_NKCUIWorldMap.Close();
			}
			NKCUIManager.LoadedUIData nkcuiworldMapUIData = this.m_NKCUIWorldMapUIData;
			if (nkcuiworldMapUIData != null)
			{
				nkcuiworldMapUIData.CloseInstance();
			}
			this.m_NKCUIWorldMapUIData = null;
			this.m_NKCUIWorldMap = null;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x00161EB1 File Offset: 0x001600B1
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00161EB9 File Offset: 0x001600B9
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00161EBC File Offset: 0x001600BC
		public void OpenDiveSearch()
		{
			this.OpenDiveSearch(0, 0);
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00161EC6 File Offset: 0x001600C6
		public void OpenDiveSearch(int cityID, int eventDiveID)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().SetTargetEventID(cityID, eventDiveID);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE_READY, true);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00161EE6 File Offset: 0x001600E6
		private void CloseDiveSearch()
		{
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x00161EE8 File Offset: 0x001600E8
		public void OpenShadowPalace()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_PALACE, true);
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x00161EF7 File Offset: 0x001600F7
		public void ProcessReLogin()
		{
			if (NKCGameEventManager.IsEventPlaying())
			{
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKCScenManager.GetScenManager().GetNowScenID(), true);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x00161F16 File Offset: 0x00160116
		public void RefreshEventList()
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.RefreshEventList();
			}
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x00161F30 File Offset: 0x00160130
		public void OpenTopPlayerPopup(NKMRaidTemplet raidTemplet, List<NKMRaidJoinData> raidJoinDataList, long raidUID)
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.OpenTopPlayerPopup(raidTemplet, raidJoinDataList, raidUID);
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x00161F50 File Offset: 0x00160150
		public void Send_NKMPacket_WORLDMAP_SET_CITY_REQ(int cityID, bool bCash)
		{
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(cityID);
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.CanOpenCity(cityTemplet, NKCScenManager.GetScenManager().GetMyUserData(), bCash);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_WORLDMAP_SET_CITY_REQ nkmpacket_WORLDMAP_SET_CITY_REQ = new NKMPacket_WORLDMAP_SET_CITY_REQ();
			nkmpacket_WORLDMAP_SET_CITY_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_SET_CITY_REQ.isCash = bCash;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_SET_CITY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00161FC6 File Offset: 0x001601C6
		public void OnWorldManCitySet(int cityID, NKMWorldMapCityData cityData)
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.OnWorldManCitySet(cityID, cityData);
			}
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x00161FE4 File Offset: 0x001601E4
		public void Send_NKMPacket_WORLDMAP_SET_LEADER_REQ(int cityID, long leaderUID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMWorldMapManager.CanSetLeader(NKCScenManager.GetScenManager().GetMyUserData(), leaderUID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_WORLDMAP_SET_LEADER_REQ nkmpacket_WORLDMAP_SET_LEADER_REQ = new NKMPacket_WORLDMAP_SET_LEADER_REQ();
			nkmpacket_WORLDMAP_SET_LEADER_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_SET_LEADER_REQ.leaderUID = leaderUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_SET_LEADER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x00162043 File Offset: 0x00160243
		public void OnCityLeaderChanged(NKMWorldMapCityData cityData)
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.CityLeaderChanged(cityData);
			}
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x00162060 File Offset: 0x00160260
		public void Send_NKMPacket_WORLDMAP_CITY_MISSION_REQ(int cityID, int missionID, NKMDeckIndex deckIndex)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				Debug.LogError(string.Format("city data is null - {0}", cityID));
				return;
			}
			NKMWorldMapManager.GetCityTemplet(cityID);
			NKM_ERROR_CODE nkm_ERROR_CODE = cityData.CanStartMission(myUserData, missionID, deckIndex);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_WORLDMAP_CITY_MISSION_REQ nkmpacket_WORLDMAP_CITY_MISSION_REQ = new NKMPacket_WORLDMAP_CITY_MISSION_REQ();
			nkmpacket_WORLDMAP_CITY_MISSION_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_CITY_MISSION_REQ.missionID = missionID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_CITY_MISSION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x001620F0 File Offset: 0x001602F0
		public void Send_NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_REQ(int cityID, int missionID)
		{
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				Debug.LogError(string.Format("city data is null - {0}", cityID));
				return;
			}
			NKMWorldMapManager.GetCityTemplet(cityID);
			NKM_ERROR_CODE nkm_ERROR_CODE = cityData.CanCancelMission(missionID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_REQ nkmpacket_WORLDMAP_CITY_MISSION_CANCEL_REQ = new NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_REQ();
			nkmpacket_WORLDMAP_CITY_MISSION_CANCEL_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_CITY_MISSION_CANCEL_REQ.missionID = missionID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_CITY_MISSION_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0016217C File Offset: 0x0016037C
		public void CityDataUpdated(NKMWorldMapCityData cityData)
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.CityDataUpdated(cityData);
			}
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x00162197 File Offset: 0x00160397
		public void CityEventSpawned(int cityID)
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.CityEventSpawned(cityID);
			}
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x001621B4 File Offset: 0x001603B4
		public void CityBuildingChanged(NKMWorldMapCityData cityData, int changedBuildingID = 0)
		{
			this.CityBuildingFX(changedBuildingID);
			this.CityDataUpdated(cityData);
			if (this.m_PopupWorldMapBuildingInfo != null && this.m_PopupWorldMapBuildingInfo.IsOpen)
			{
				this.m_PopupWorldMapBuildingInfo.Close();
			}
			if (this.m_PopupWorldMapNewBuildingList != null && this.m_PopupWorldMapNewBuildingList.IsOpen)
			{
				this.m_PopupWorldMapNewBuildingList.Close();
			}
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0016221B File Offset: 0x0016041B
		public void CityBuildingFX(int buildingID)
		{
			if (this.m_NKCUIWorldMap.IsOpen)
			{
				this.m_NKCUIWorldMap.SetFXBuildingID(buildingID);
			}
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x00162238 File Offset: 0x00160438
		public void Send_NKMPacket_WORLDMAP_MISSION_REFRESH_REQ(int cityID)
		{
			if (!NKCScenManager.GetScenManager().GetMyUserData().CheckPrice(50, 3))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CASH), null, "");
				return;
			}
			NKMPacket_WORLDMAP_MISSION_REFRESH_REQ nkmpacket_WORLDMAP_MISSION_REFRESH_REQ = new NKMPacket_WORLDMAP_MISSION_REFRESH_REQ();
			nkmpacket_WORLDMAP_MISSION_REFRESH_REQ.cityID = cityID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_MISSION_REFRESH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x00162294 File Offset: 0x00160494
		public void Send_NKMPacket_WORLDMAP_MISSION_COMPLETE_REQ(int cityID)
		{
			if (this.m_bWaitingCompleteAck)
			{
				return;
			}
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				Debug.LogError(string.Format("city data is null - {0}", cityID));
				return;
			}
			if (cityData.worldMapMission.currentMissionID == 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_NOT_DOING), null, "");
				return;
			}
			if (!NKCSynchronizedTime.IsFinished(cityData.worldMapMission.completeTime))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DOING), null, "");
				return;
			}
			this.m_bWaitingCompleteAck = true;
			NKMPacket_WORLDMAP_MISSION_COMPLETE_REQ nkmpacket_WORLDMAP_MISSION_COMPLETE_REQ = new NKMPacket_WORLDMAP_MISSION_COMPLETE_REQ();
			nkmpacket_WORLDMAP_MISSION_COMPLETE_REQ.cityID = cityID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_MISSION_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00162355 File Offset: 0x00160555
		public void OnWorldmapMissionCompleteAck()
		{
			this.m_bWaitingCompleteAck = false;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0016235E File Offset: 0x0016055E
		public void Send_NKMPacket_WORLDMAP_COLLECT_REQ(int cityID)
		{
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00162360 File Offset: 0x00160560
		public void Send_NKMPacket_WORLDMAP_BUILD_REQ(int cityID, int buildingID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMWorldMapManager.CanBuild(NKCScenManager.CurrentUserData(), cityID, buildingID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKMPacket_WORLDMAP_BUILD_REQ nkmpacket_WORLDMAP_BUILD_REQ = new NKMPacket_WORLDMAP_BUILD_REQ();
			nkmpacket_WORLDMAP_BUILD_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_BUILD_REQ.buildID = buildingID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_BUILD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x001623B4 File Offset: 0x001605B4
		public void Send_NKMPacket_WORLDMAP_BUILD_LEVELUP_REQ(int cityID, int buildingID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMWorldMapManager.CanLevelUpBuilding(NKCScenManager.CurrentUserData(), cityID, buildingID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKMPacket_WORLDMAP_BUILD_LEVELUP_REQ nkmpacket_WORLDMAP_BUILD_LEVELUP_REQ = new NKMPacket_WORLDMAP_BUILD_LEVELUP_REQ();
			nkmpacket_WORLDMAP_BUILD_LEVELUP_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_BUILD_LEVELUP_REQ.buildID = buildingID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_BUILD_LEVELUP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00162408 File Offset: 0x00160608
		public void Send_NKMPacket_WORLDMAP_BUILD_EXPIRE_REQ(int cityID, int buildingID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMWorldMapManager.CanExpireBuilding(NKCScenManager.CurrentUserData(), cityID, buildingID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKMPacket_WORLDMAP_BUILD_EXPIRE_REQ nkmpacket_WORLDMAP_BUILD_EXPIRE_REQ = new NKMPacket_WORLDMAP_BUILD_EXPIRE_REQ();
			nkmpacket_WORLDMAP_BUILD_EXPIRE_REQ.cityID = cityID;
			nkmpacket_WORLDMAP_BUILD_EXPIRE_REQ.buildID = buildingID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_BUILD_EXPIRE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00162459 File Offset: 0x00160659
		public RectTransform GetCityRect(int cityID)
		{
			if (this.m_NKCUIWorldMap != null)
			{
				return this.m_NKCUIWorldMap.GetPinRect(cityID);
			}
			return null;
		}

		// Token: 0x040038A4 RID: 14500
		private NKCUIWorldMap m_NKCUIWorldMap;

		// Token: 0x040038A5 RID: 14501
		private NKCUIManager.LoadedUIData m_NKCUIWorldMapUIData;

		// Token: 0x040038A6 RID: 14502
		private bool m_bWaitingCompleteAck;

		// Token: 0x040038A7 RID: 14503
		private bool m_bShowIntro;

		// Token: 0x040038A8 RID: 14504
		private bool m_bWaitingMovie;

		// Token: 0x040038A9 RID: 14505
		private bool m_bReserveOpenEventList;

		// Token: 0x040038AA RID: 14506
		private const float MOVIE_PLAY_SPEED = 1.5f;

		// Token: 0x040038AB RID: 14507
		private bool m_bReservedDiveReverseAni;

		// Token: 0x040038AC RID: 14508
		private NKCPopupWorldMapBuildingInfo m_PopupWorldMapBuildingInfo;

		// Token: 0x040038AD RID: 14509
		private NKCPopupWorldMapNewBuildingList m_PopupWorldMapNewBuildingList;

		// Token: 0x040038AE RID: 14510
		private NKCPopupWorldmapEventOKCancel m_NKCPopupWorldmapEventOKCancel;

		// Token: 0x040038AF RID: 14511
		private int m_introSoundUID;
	}
}
