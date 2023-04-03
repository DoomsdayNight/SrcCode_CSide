using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using Cs.Math;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000ABA RID: 2746
	public class NKCPopupWorldMapEventListSlot : MonoBehaviour
	{
		// Token: 0x06007A54 RID: 31316 RVA: 0x0028C0C8 File Offset: 0x0028A2C8
		public void SetDataForMyEventList(NKMWorldMapEventGroup eventGroupData, NKMWorldMapCityData cityData, NKCPopupWorldMapEventListSlot.OnMove onMove)
		{
			this.dOnMove = onMove;
			this.m_TabState = NKCPopupWorldMapEventList.eState.EventList;
			this.SetCityData(cityData);
			this.SetDataCommon(eventGroupData);
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x0028C0E8 File Offset: 0x0028A2E8
		public void SetDataForJoinList(NKMRaidResultData cNKMRaidResultData)
		{
			if (cNKMRaidResultData == null)
			{
				return;
			}
			this.m_NKM_WORLDMAP_EVENT_TYPE = NKM_WORLDMAP_EVENT_TYPE.WET_RAID;
			this.m_TabState = NKCPopupWorldMapEventList.eState.JoinList;
			this.m_eventUID = cNKMRaidResultData.raidUID;
			this.SetSupportReqUser(cNKMRaidResultData.userUID, cNKMRaidResultData.nickname, cNKMRaidResultData.mainUnitID, cNKMRaidResultData.mainUnitSkinID, cNKMRaidResultData.friendCode);
			this.m_dtEndTime = new DateTime(cNKMRaidResultData.expireDate);
			this.SetRaidInfo(cNKMRaidResultData);
			this.SetRaidThumbnail(cNKMRaidResultData.stageID);
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x0028C15C File Offset: 0x0028A35C
		private void SetRaidThumbnail(int stageID)
		{
			NKMWorldMapEventTemplet worldMapEventTempletByStageID = NKMWorldMapManager.GetWorldMapEventTempletByStageID(stageID);
			Sprite thumbnail = this.GetThumbnail(worldMapEventTempletByStageID);
			NKCUtil.SetImageSprite(this.m_imgThumbnail, thumbnail, true);
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x0028C188 File Offset: 0x0028A388
		public void SetDataForHelpList(NKMCoopRaidData cNKMCoopRaidData)
		{
			if (cNKMCoopRaidData == null)
			{
				return;
			}
			this.m_TabState = NKCPopupWorldMapEventList.eState.HelpList;
			this.m_NKM_WORLDMAP_EVENT_TYPE = NKM_WORLDMAP_EVENT_TYPE.WET_RAID;
			this.m_eventUID = cNKMCoopRaidData.raidUID;
			this.SetSupportReqUser(cNKMCoopRaidData.userUID, cNKMCoopRaidData.nickname, cNKMCoopRaidData.mainUnitID, cNKMCoopRaidData.mainUnitSkinID, cNKMCoopRaidData.friendCode);
			this.m_dtEndTime = new DateTime(cNKMCoopRaidData.expireDate);
			this.SetRaidInfo(cNKMCoopRaidData);
			this.SetRaidThumbnail(cNKMCoopRaidData.stageID);
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x0028C1FC File Offset: 0x0028A3FC
		private void SetSupportReqUser(long userUID, string nickName, int mainUnitID, int mainUnitSkinID, long friendCode)
		{
			NKCUtil.SetGameobjectActive(this.m_objRootCityInfo, false);
			NKCUtil.SetGameobjectActive(this.m_objRootHelp, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(mainUnitID);
			this.m_imgHelpUserIcon.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase, mainUnitSkinID);
			this.m_lbHelpUserName.text = nickName;
			this.m_lbHelpUserUID.text = NKCUtilString.GetFriendCode(friendCode);
			if (NKCScenManager.CurrentUserData().m_UserUID == userUID)
			{
				this.m_lbHelpUserName.color = this.m_colUserNameWhenMe;
			}
			else
			{
				this.m_lbHelpUserName.color = this.m_colUserName;
			}
			NKCUtil.SetGameobjectActive(this.m_objMyHelp, NKCScenManager.CurrentUserData().m_UserUID == userUID);
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x0028C2A4 File Offset: 0x0028A4A4
		private void SetCityData(NKMWorldMapCityData cityData)
		{
			NKCUtil.SetGameobjectActive(this.m_objRootHelp, false);
			if (cityData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRootCityInfo, false);
				this.m_cityID = 0;
				return;
			}
			this.m_cityID = cityData.cityID;
			NKCUtil.SetGameobjectActive(this.m_objRootCityInfo, true);
			NKCUtil.SetLabelText(this.m_lbCityLevel, cityData.level.ToString());
			if (this.m_imgCityExp != null)
			{
				this.m_imgCityExp.fillAmount = NKMWorldMapManager.GetCityExpPercent(cityData);
			}
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(cityData.cityID);
			NKCUtil.SetLabelText(this.m_lbCityName, (cityTemplet != null) ? cityTemplet.GetName() : "");
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x0028C348 File Offset: 0x0028A548
		private void SetDataCommon(NKMWorldMapEventGroup eventGroupData)
		{
			if (eventGroupData == null)
			{
				Debug.LogError("EventData Null!");
				this.m_eventID = 0;
				this.m_eventUID = 0L;
				return;
			}
			this.m_eventID = eventGroupData.worldmapEventID;
			this.m_eventUID = eventGroupData.eventUid;
			this.m_dtEndTime = eventGroupData.eventGroupEndDate;
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(eventGroupData.worldmapEventID);
			if (nkmworldMapEventTemplet == null)
			{
				Debug.LogError(string.Format("Worldmap eventtemplet null! ID : {0}", eventGroupData.worldmapEventID));
			}
			NKM_WORLDMAP_EVENT_TYPE eventType = nkmworldMapEventTemplet.eventType;
			if (eventType != NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				if (eventType != NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					return;
				}
				NKMDiveTemplet diveInfo = NKMDiveTemplet.Find(nkmworldMapEventTemplet.stageID);
				this.SetDiveInfo(diveInfo);
			}
			else
			{
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(eventGroupData.eventUid);
				nkmraidDetailData.SortJoinDataByDamage();
				string mvpName = "-";
				if (nkmraidDetailData.raidJoinDataList.Count > 0 && nkmraidDetailData.raidJoinDataList[0].tryCount > 0)
				{
					mvpName = nkmraidDetailData.raidJoinDataList[0].nickName;
				}
				int myTryCount = 0;
				NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
				if (nkmraidJoinData != null)
				{
					myTryCount = (int)nkmraidJoinData.tryCount;
				}
				NKMRaidSeasonTemplet nowSeasonTemplet = NKCRaidSeasonManager.GetNowSeasonTemplet();
				bool bCurrentSeason = nowSeasonTemplet != null && nowSeasonTemplet.RaidSeasonId == nkmraidDetailData.seasonID;
				this.SetRaidInfo(nkmraidDetailData.stageID, nkmraidDetailData.curHP, nkmraidDetailData.maxHP, nkmraidDetailData.raidJoinDataList.Count, mvpName, myTryCount, bCurrentSeason);
			}
			Sprite thumbnail = this.GetThumbnail(nkmworldMapEventTemplet);
			NKCUtil.SetImageSprite(this.m_imgThumbnail, thumbnail, true);
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x0028C4C8 File Offset: 0x0028A6C8
		private Sprite GetThumbnail(NKMWorldMapEventTemplet eventTemplet)
		{
			if (eventTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_EVENT_THUMBNAIL", eventTemplet.thumbnail, false);
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x0028C4E0 File Offset: 0x0028A6E0
		private void SetDiveInfo(NKMDiveTemplet diveTemplet)
		{
			this.m_NKM_WORLDMAP_EVENT_TYPE = NKM_WORLDMAP_EVENT_TYPE.WET_DIVE;
			this.m_eMiddleState = NKCPopupWorldMapEventListSlot.MiddleState.Normal;
			NKCUtil.SetGameobjectActive(this.m_objEventTypeRaid, false);
			NKCUtil.SetGameobjectActive(this.m_objEventTypeRaidExpried, false);
			NKCUtil.SetGameobjectActive(this.m_objEventTypeDive, true);
			NKCUtil.SetGameobjectActive(this.m_objRootEventNormal, true);
			NKCUtil.SetGameobjectActive(this.m_objRootEventRaid, false);
			NKCUtil.SetGameobjectActive(this.m_objAttendLimit, false);
			NKCUtil.SetGameobjectActive(this.m_objEntryCheck, false);
			NKCUtil.SetGameobjectActive(this.m_objTryCount, false);
			if (diveTemplet != null)
			{
				this.m_level_N = diveTemplet.StageLevel;
				NKCUtil.SetLabelText(this.m_lbLevel_N, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, diveTemplet.StageLevel));
				NKCUtil.SetLabelText(this.m_lbName_N, diveTemplet.Get_STAGE_NAME());
				NKCUtil.SetLabelText(this.m_lbDiveLevel, diveTemplet.Get_STAGE_NAME_SUB());
				NKCUtil.SetLabelText(this.m_lbDiveSlotCount, diveTemplet.SlotCount.ToString());
				NKCUtil.SetGameobjectActive(this.m_imgEventPointColor_N, true);
				NKCUtil.SetDiveEventPoint(this.m_imgEventPointColor_N, diveTemplet.GetCommonDifficultyData() == EPISODE_DIFFICULTY.HARD);
			}
			else
			{
				this.m_level_N = 0;
				NKCUtil.SetLabelText(this.m_lbLevel_N, "");
				NKCUtil.SetLabelText(this.m_lbName_N, "");
				NKCUtil.SetLabelText(this.m_lbDiveLevel, "");
				NKCUtil.SetLabelText(this.m_lbDiveSlotCount, "");
				NKCUtil.SetGameobjectActive(this.m_imgEventPointColor_N, false);
			}
			this.m_bNeedTimeUpdate = true;
			NKCUtil.SetLabelText(this.m_lbTimeLeft, NKCSynchronizedTime.GetTimeLeftString(this.m_dtEndTime));
			this.SetButtonNEventStateByCurr();
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x0028C660 File Offset: 0x0028A860
		private void SetRaidInfo(int stageID, float curHP, float maxHP, int attendCount, string mvpName, int myTryCount, bool bCurrentSeason)
		{
			this.m_NKM_WORLDMAP_EVENT_TYPE = NKM_WORLDMAP_EVENT_TYPE.WET_RAID;
			this.m_eMiddleState = NKCPopupWorldMapEventListSlot.MiddleState.Raid;
			NKCUtil.SetGameobjectActive(this.m_objEventTypeRaid, bCurrentSeason);
			NKCUtil.SetGameobjectActive(this.m_objEventTypeRaidExpried, !bCurrentSeason);
			NKCUtil.SetGameobjectActive(this.m_objEventTypeDive, false);
			NKCUtil.SetGameobjectActive(this.m_objRootEventNormal, false);
			NKCUtil.SetGameobjectActive(this.m_objRootEventRaid, true);
			NKCUtil.SetGameobjectActive(this.m_objEntryCheck, myTryCount > 0);
			NKCUtil.SetGameobjectActive(this.m_objTryCount, true);
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(stageID);
			if (nkmraidTemplet != null)
			{
				this.m_level_R = nkmraidTemplet.RaidLevel;
				NKCUtil.SetLabelText(this.m_lbLevel_R, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkmraidTemplet.RaidLevel));
				this.m_lbName_R.text = nkmraidTemplet.DungeonTempletBase.GetDungeonName();
				NKCUtil.SetGameobjectActive(this.m_objAttendLimit, nkmraidTemplet.AttendLimit > 0);
				if (nkmraidTemplet.AttendLimit > 0)
				{
					NKCUtil.SetLabelText(this.m_lbAttendLimit, string.Format("{0}/{1}", attendCount, nkmraidTemplet.AttendLimit));
				}
				NKCUtil.SetGameobjectActive(this.m_imgEventPointColor_R, true);
				NKCUtil.SetRaidEventPoint(this.m_imgEventPointColor_R, nkmraidTemplet);
				NKCUtil.SetLabelText(this.m_lbTryCount, string.Format(NKCUtilString.GET_STRING_RAID_REMAIN_COUNT_ONE_PARAM, nkmraidTemplet.RaidTryCount - myTryCount));
			}
			else
			{
				this.m_level_R = 0;
				NKCUtil.SetGameobjectActive(this.m_objAttendLimit, false);
				NKCUtil.SetGameobjectActive(this.m_imgEventPointColor_R, false);
				NKCUtil.SetLabelText(this.m_lbTryCount, "");
			}
			NKCUtil.SetLabelText(this.m_lbMVPName_R, (attendCount > 0) ? mvpName : "-");
			NKCUtil.SetLabelText(this.m_lbTargetHP_R, string.Format("{0}/{1}", (int)curHP, (int)maxHP));
			int num = (int)(curHP / maxHP * 100f);
			NKCUtil.SetLabelText(this.m_lbHPLeft_R, string.Format("{0}%", num));
			if (maxHP.IsNearlyZero(1E-05f))
			{
				this.m_imgTargetHP_R.fillAmount = 0f;
			}
			else
			{
				this.m_imgTargetHP_R.fillAmount = curHP / maxHP;
			}
			this.SetButtonNEventStateByCurr();
			this.m_bNeedTimeUpdate = (curHP > 0f);
			NKCUtil.SetGameobjectActive(this.m_objTimeLeft, curHP > 0f);
			NKCUtil.SetLabelText(this.m_lbTimeLeft, NKCSynchronizedTime.GetTimeLeftString(this.m_dtEndTime));
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x0028C8A4 File Offset: 0x0028AAA4
		private void SetRaidInfo(NKMRaidResultData cNKMRaidResultData)
		{
			this.m_cNKMRaidResultData = cNKMRaidResultData;
			this.m_cityID = cNKMRaidResultData.cityID;
			this.m_cNKMRaidResultData.SortJoinDataByDamage();
			string mvpName = "-";
			if (cNKMRaidResultData.raidJoinDataList.Count > 0 && cNKMRaidResultData.raidJoinDataList[0].tryCount > 0)
			{
				mvpName = cNKMRaidResultData.raidJoinDataList[0].nickName;
			}
			int myTryCount = 0;
			NKMRaidJoinData nkmraidJoinData = cNKMRaidResultData.raidJoinDataList.Find((NKMRaidJoinData x) => x.userUID == NKCScenManager.CurrentUserData().m_UserUID);
			if (nkmraidJoinData != null)
			{
				myTryCount = (int)nkmraidJoinData.tryCount;
			}
			NKMRaidSeasonTemplet nowSeasonTemplet = NKCRaidSeasonManager.GetNowSeasonTemplet();
			bool bCurrentSeason = nowSeasonTemplet != null && nowSeasonTemplet.RaidSeasonId == cNKMRaidResultData.seasonID;
			this.SetRaidInfo(cNKMRaidResultData.stageID, cNKMRaidResultData.curHP, cNKMRaidResultData.maxHP, cNKMRaidResultData.raidJoinDataList.Count, mvpName, myTryCount, bCurrentSeason);
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x0028C984 File Offset: 0x0028AB84
		private void SetRaidInfo(NKMCoopRaidData cNKMCoopRaidData)
		{
			cNKMCoopRaidData.SortJoinDataByDamage();
			string mvpName = "-";
			if (cNKMCoopRaidData.raidJoinDataList.Count > 0 && cNKMCoopRaidData.raidJoinDataList[0].tryCount > 0)
			{
				mvpName = cNKMCoopRaidData.raidJoinDataList[0].nickName;
			}
			int myTryCount = 0;
			NKMRaidJoinData nkmraidJoinData = cNKMCoopRaidData.raidJoinDataList.Find((NKMRaidJoinData x) => x.userUID == NKCScenManager.CurrentUserData().m_UserUID);
			if (nkmraidJoinData != null)
			{
				myTryCount = (int)nkmraidJoinData.tryCount;
			}
			NKMRaidSeasonTemplet nowSeasonTemplet = NKCRaidSeasonManager.GetNowSeasonTemplet();
			bool bCurrentSeason = nowSeasonTemplet != null && nowSeasonTemplet.RaidSeasonId == cNKMCoopRaidData.seasonID;
			this.SetRaidInfo(cNKMCoopRaidData.stageID, cNKMCoopRaidData.curHP, cNKMCoopRaidData.maxHP, cNKMCoopRaidData.raidJoinDataList.Count, mvpName, myTryCount, bCurrentSeason);
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x0028CA4C File Offset: 0x0028AC4C
		private void SetEventState(List<NKCPopupWorldMapEventListSlot.eEventState> listEventState)
		{
			NKCUtil.SetGameobjectActive(this.m_objProgress_R, false);
			NKCUtil.SetGameobjectActive(this.m_objFail_R, false);
			NKCUtil.SetGameobjectActive(this.m_objMVP_R, this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_RAID);
			NKCUtil.SetGameobjectActive(this.m_objMyMVP, false);
			NKCUtil.SetGameobjectActive(this.m_objComplete_R, false);
			for (int i = 0; i < listEventState.Count; i++)
			{
				if (listEventState[i] == NKCPopupWorldMapEventListSlot.eEventState.Progress)
				{
					NKCUtil.SetGameobjectActive(this.m_objProgress_R, true);
				}
				else if (listEventState[i] == NKCPopupWorldMapEventListSlot.eEventState.Fail)
				{
					NKCUtil.SetGameobjectActive(this.m_objFail_R, true);
					NKCUtil.SetLabelText(this.m_lbFail_R, NKCUtilString.GET_STRING_WORLDMAP_EVENT_STATE_FAIL);
				}
				else if (listEventState[i] == NKCPopupWorldMapEventListSlot.eEventState.MVP)
				{
					NKCUtil.SetGameobjectActive(this.m_objMyMVP, true);
				}
				else if (listEventState[i] == NKCPopupWorldMapEventListSlot.eEventState.Complete)
				{
					NKCUtil.SetGameobjectActive(this.m_objComplete_R, true);
				}
				else if (listEventState[i] == NKCPopupWorldMapEventListSlot.eEventState.Expired)
				{
					NKCUtil.SetGameobjectActive(this.m_objFail_R, true);
					NKCUtil.SetLabelText(this.m_lbFail_R, NKCUtilString.GET_STRING_WORLDMAP_EVENT_STATE_TIME_EXPIRED);
				}
			}
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x0028CB4A File Offset: 0x0028AD4A
		private void _OnDelete()
		{
			NKCPacketSender.Send_NKMPacket_WORLDMAP_EVENT_CANCEL_REQ(this.m_cityID);
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x0028CB58 File Offset: 0x0028AD58
		private void OnDeleteNewRaid()
		{
			NKC_SCEN_WORLDMAP cNKC_SCEN_WORLDMAP = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP();
			cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Open(NKCUtilString.GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_NEW_RAID_DELETE_WARN, int.Parse(this.m_lbCityLevel.text), this.m_imgCityExp.fillAmount, this.m_lbCityName.text, NKM_WORLDMAP_EVENT_TYPE.WET_RAID, this.m_level_R, new NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel(this._OnDelete), delegate
			{
				cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Close();
			});
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x0028CBD8 File Offset: 0x0028ADD8
		private void OnDeleteNewDive()
		{
			NKC_SCEN_WORLDMAP cNKC_SCEN_WORLDMAP = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP();
			cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Open(NKCUtilString.GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_NEW_DIVE_DELETE_WARN, int.Parse(this.m_lbCityLevel.text), this.m_imgCityExp.fillAmount, this.m_lbCityName.text, NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, this.m_level_N, new NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel(this._OnDelete), delegate
			{
				cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Close();
			});
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x0028CC58 File Offset: 0x0028AE58
		private void OnDeleteOnGoingRaid()
		{
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(this.m_cNKMRaidResultData.cityID);
			if (cityData == null)
			{
				return;
			}
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(cityData.cityID);
			if (cityTemplet == null)
			{
				return;
			}
			NKC_SCEN_WORLDMAP cNKC_SCEN_WORLDMAP = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP();
			cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Open(NKCUtilString.GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_ON_GOING_RAID_DELETE_WARN, cityData.level, NKMWorldMapManager.GetCityExpPercent(cityData), cityTemplet.GetName(), NKM_WORLDMAP_EVENT_TYPE.WET_RAID, this.m_level_R, new NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel(this._OnDelete), delegate
			{
				cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Close();
			});
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x0028CCF8 File Offset: 0x0028AEF8
		private void OnDeleteOnGoingDive()
		{
			NKC_SCEN_WORLDMAP cNKC_SCEN_WORLDMAP = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP();
			cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Open(NKCUtilString.GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_ON_GOING_DIVE_DELETE_WARN, int.Parse(this.m_lbCityLevel.text), this.m_imgCityExp.fillAmount, this.m_lbCityName.text, NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, this.m_level_N, new NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel(this._OnDelete), delegate
			{
				cNKC_SCEN_WORLDMAP.NKCPopupWorldmapEventOKCancel.Close();
			});
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x0028CD78 File Offset: 0x0028AF78
		private void SetButtonNEventStateByCurr()
		{
			NKCPopupWorldMapEventListSlot.eButtonState_Top eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.None;
			NKCPopupWorldMapEventListSlot.eButtonState_Bottom eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.None;
			List<NKCPopupWorldMapEventListSlot.eEventState> list = new List<NKCPopupWorldMapEventListSlot.eEventState>();
			if (this.m_TabState == NKCPopupWorldMapEventList.eState.EventList)
			{
				if (NKCSynchronizedTime.IsFinished(this.m_dtEndTime))
				{
					eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.None;
					eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK;
					list.Add(NKCPopupWorldMapEventListSlot.eEventState.Expired);
				}
				else
				{
					eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Delete;
					eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Move;
				}
				if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					bool flag = NKCScenManager.CurrentUserData().m_DiveGameData != null && NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == this.m_eventUID;
					this.m_csbtnDelete.PointerClick.RemoveAllListeners();
					if (flag)
					{
						this.m_csbtnDelete.PointerClick.AddListener(new UnityAction(this.OnDeleteOnGoingDive));
					}
					else
					{
						this.m_csbtnDelete.PointerClick.AddListener(new UnityAction(this.OnDeleteNewDive));
					}
				}
				else
				{
					NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_eventUID);
					if (nkmraidDetailData != null)
					{
						NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
						if (nkmraidTemplet != null && nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
						{
							NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
							if (nkmraidJoinData != null && (int)nkmraidJoinData.tryCount >= nkmraidTemplet.RaidTryCount && nkmraidDetailData.curHP > 0f)
							{
								eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.None;
								eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK;
								if (list.Contains(NKCPopupWorldMapEventListSlot.eEventState.Expired))
								{
									list.Remove(NKCPopupWorldMapEventListSlot.eEventState.Expired);
								}
								list.Add(NKCPopupWorldMapEventListSlot.eEventState.Fail);
							}
						}
					}
					this.m_csbtnDelete.PointerClick.RemoveAllListeners();
					this.m_csbtnDelete.PointerClick.AddListener(new UnityAction(this.OnDeleteNewRaid));
				}
			}
			else if (this.m_TabState == NKCPopupWorldMapEventList.eState.HelpList)
			{
				if (NKCSynchronizedTime.IsFinished(this.m_dtEndTime))
				{
					eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.None;
					eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK;
					list.Add(NKCPopupWorldMapEventListSlot.eEventState.Expired);
				}
				else
				{
					eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Help;
					eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Help;
				}
				if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
				{
					NKCPopupWorldMapEventListSlot.eEventState item = NKCPopupWorldMapEventListSlot.eEventState.None;
					if (this.m_cNKMCoopRaidData != null)
					{
						if (this.m_cNKMCoopRaidData.curHP <= 0f)
						{
							item = NKCPopupWorldMapEventListSlot.eEventState.Complete;
						}
						else if (!this.m_cNKMCoopRaidData.IsOnGoing())
						{
							item = NKCPopupWorldMapEventListSlot.eEventState.Fail;
						}
						else
						{
							NKMRaidDetailData nkmraidDetailData2 = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_eventUID);
							if (nkmraidDetailData2 != null)
							{
								NKMRaidTemplet nkmraidTemplet2 = NKMRaidTemplet.Find(nkmraidDetailData2.stageID);
								if (nkmraidTemplet2 != null && nkmraidTemplet2.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
								{
									Debug.LogWarning(string.Format("솔로레이드는 지원요청을 못함 - stageID : {0}, category : {1}", nkmraidTemplet2.Key, nkmraidTemplet2.DungeonTempletBase.m_DungeonType));
									NKCUtil.SetGameobjectActive(base.gameObject, false);
								}
								else
								{
									item = NKCPopupWorldMapEventListSlot.eEventState.Progress;
								}
							}
						}
					}
					list.Add(item);
				}
			}
			else if (this.m_TabState == NKCPopupWorldMapEventList.eState.JoinList)
			{
				if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					bool flag2 = NKCScenManager.CurrentUserData().m_DiveGameData != null && NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == this.m_eventUID;
					if (NKCSynchronizedTime.IsFinished(this.m_dtEndTime) || (this.m_eventUID > 0L && !flag2))
					{
						eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.None;
						eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK;
						list.Add(NKCPopupWorldMapEventListSlot.eEventState.Fail);
					}
					else
					{
						eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Delete;
						eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Move;
					}
					this.m_csbtnDelete.PointerClick.RemoveAllListeners();
					if (flag2)
					{
						this.m_csbtnDelete.PointerClick.AddListener(new UnityAction(this.OnDeleteOnGoingDive));
					}
					else
					{
						this.m_csbtnDelete.PointerClick.AddListener(new UnityAction(this.OnDeleteNewDive));
					}
				}
				else if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
				{
					if (NKCSynchronizedTime.IsFinished(this.m_dtEndTime))
					{
						eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK;
						eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Result;
						if (this.m_cNKMRaidResultData != null)
						{
							if (this.m_cNKMRaidResultData.curHP > 0f)
							{
								list.Add(NKCPopupWorldMapEventListSlot.eEventState.Fail);
							}
							else
							{
								list.Add(NKCPopupWorldMapEventListSlot.eEventState.Complete);
							}
						}
					}
					else if (this.m_cNKMRaidResultData != null)
					{
						if (this.m_cNKMRaidResultData.raidJoinDataList.Count > 0 && this.m_cNKMRaidResultData.raidJoinDataList[0].userUID == NKCScenManager.CurrentUserData().m_UserUID)
						{
							list.Add(NKCPopupWorldMapEventListSlot.eEventState.MVP);
						}
						if (this.m_cNKMRaidResultData.curHP > 0f)
						{
							if (!this.m_cNKMRaidResultData.isCoop)
							{
								eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Delete;
								eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Move;
								this.m_csbtnDelete.PointerClick.RemoveAllListeners();
								this.m_csbtnDelete.PointerClick.AddListener(new UnityAction(this.OnDeleteOnGoingRaid));
							}
							else
							{
								eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Process;
								eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Move;
							}
							list.Add(NKCPopupWorldMapEventListSlot.eEventState.Progress);
						}
						else
						{
							eButtonState_Top = NKCPopupWorldMapEventListSlot.eButtonState_Top.Result;
							eButtonState_Bottom = NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK;
							list.Add(NKCPopupWorldMapEventListSlot.eEventState.Complete);
						}
					}
				}
			}
			this.SetEventState(list);
			NKCUtil.SetGameobjectActive(this.m_objBtnOnProgress, eButtonState_Top == NKCPopupWorldMapEventListSlot.eButtonState_Top.Process);
			NKCUtil.SetGameobjectActive(this.m_objCoop, eButtonState_Top == NKCPopupWorldMapEventListSlot.eButtonState_Top.Help);
			NKCUtil.SetGameobjectActive(this.m_csbtnResult, eButtonState_Top == NKCPopupWorldMapEventListSlot.eButtonState_Top.Result);
			NKCUtil.SetGameobjectActive(this.m_csbtnDelete, eButtonState_Top == NKCPopupWorldMapEventListSlot.eButtonState_Top.Delete);
			NKCUtil.SetGameobjectActive(this.m_objBtnBlue, eButtonState_Bottom == NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Move || eButtonState_Bottom == NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Help);
			NKCUtil.SetGameobjectActive(this.m_objBtnOK, eButtonState_Bottom == NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK);
			if (eButtonState_Bottom == NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Move)
			{
				NKCUtil.SetLabelText(this.m_lbBtnBlue, NKCUtilString.GET_STRING_WORLDMAP_GO_BUTTON);
				this.m_csbtnPlay.PointerClick.RemoveAllListeners();
				this.m_csbtnPlay.PointerClick.AddListener(new UnityAction(this.OnClickBtn_Move));
				return;
			}
			if (eButtonState_Bottom == NKCPopupWorldMapEventListSlot.eButtonState_Bottom.Help)
			{
				NKCUtil.SetLabelText(this.m_lbBtnBlue, NKCUtilString.GET_STRING_WORLDMAP_HELP_BUTTON);
				this.m_csbtnPlay.PointerClick.RemoveAllListeners();
				this.m_csbtnPlay.PointerClick.AddListener(new UnityAction(this.OnClickBtn_Help));
				return;
			}
			if (eButtonState_Bottom == NKCPopupWorldMapEventListSlot.eButtonState_Bottom.OK)
			{
				this.m_csbtnPlay.PointerClick.RemoveAllListeners();
				this.m_csbtnPlay.PointerClick.AddListener(new UnityAction(this.OnClickBtn_OK));
			}
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x0028D2C8 File Offset: 0x0028B4C8
		private void OnClickBtn_Move()
		{
			if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(this.m_eventUID);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
				return;
			}
			NKCPopupWorldMapEventListSlot.OnMove onMove = this.dOnMove;
			if (onMove == null)
			{
				return;
			}
			onMove(this.m_cityID, this.m_eventID, this.m_eventUID);
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x0028D322 File Offset: 0x0028B522
		private void OnClickBtn_Help()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(this.m_eventUID);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x0028D346 File Offset: 0x0028B546
		private void OnClickBtn_Progress()
		{
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WORLDMAP_PROGRESS_BUTTON_POPUP, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x0028D35C File Offset: 0x0028B55C
		private void OnClickBtn_OK()
		{
			if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
			{
				NKCPacketSender.Send_NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ(this.m_cityID);
				return;
			}
			if (this.m_NKM_WORLDMAP_EVENT_TYPE == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				if (this.m_TabState == NKCPopupWorldMapEventList.eState.EventList)
				{
					NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_eventUID);
					if (nkmraidDetailData != null && nkmraidDetailData.curHP > 0f)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(this.m_eventUID);
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
						return;
					}
				}
				else if (this.m_TabState != NKCPopupWorldMapEventList.eState.HelpList && this.m_TabState == NKCPopupWorldMapEventList.eState.JoinList)
				{
					if (this.m_cNKMRaidResultData == null)
					{
						return;
					}
					NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_cNKMRaidResultData.stageID);
					if (nkmraidTemplet == null)
					{
						return;
					}
					if (this.m_cNKMRaidResultData.curHP <= 0f)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OpenTopPlayerPopup(nkmraidTemplet, this.m_cNKMRaidResultData.raidJoinDataList, this.m_cNKMRaidResultData.raidUID);
						return;
					}
					NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(this.m_eventUID);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
				}
			}
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x0028D46E File Offset: 0x0028B66E
		private void Update()
		{
			this.m_fTimer += Time.deltaTime;
			if (this.m_fTimer >= 1f)
			{
				this.m_fTimer = 0f;
				if (this.m_bNeedTimeUpdate)
				{
					this.UpdateClock();
				}
				this.SetButtonNEventStateByCurr();
			}
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x0028D4B0 File Offset: 0x0028B6B0
		private void UpdateClock()
		{
			NKCPopupWorldMapEventListSlot.MiddleState eMiddleState = this.m_eMiddleState;
			Text lbTimeLeft;
			if (eMiddleState != NKCPopupWorldMapEventListSlot.MiddleState.Normal)
			{
				if (eMiddleState != NKCPopupWorldMapEventListSlot.MiddleState.Raid)
				{
					return;
				}
				lbTimeLeft = this.m_lbTimeLeft;
			}
			else
			{
				lbTimeLeft = this.m_lbTimeLeft;
			}
			NKCUtil.SetGameobjectActive(this.m_objTimeLeft, true);
			NKCUtil.SetLabelText(lbTimeLeft, NKCSynchronizedTime.GetTimeLeftString(this.m_dtEndTime));
		}

		// Token: 0x04006714 RID: 26388
		private NKCPopupWorldMapEventListSlot.MiddleState m_eMiddleState;

		// Token: 0x04006715 RID: 26389
		private DateTime m_dtEndTime;

		// Token: 0x04006716 RID: 26390
		private NKCPopupWorldMapEventList.eState m_TabState;

		// Token: 0x04006717 RID: 26391
		private NKM_WORLDMAP_EVENT_TYPE m_NKM_WORLDMAP_EVENT_TYPE = NKM_WORLDMAP_EVENT_TYPE.WET_NONE;

		// Token: 0x04006718 RID: 26392
		private NKMCoopRaidData m_cNKMCoopRaidData;

		// Token: 0x04006719 RID: 26393
		private NKMRaidResultData m_cNKMRaidResultData;

		// Token: 0x0400671A RID: 26394
		public Image m_imgThumbnail;

		// Token: 0x0400671B RID: 26395
		public GameObject m_objEventTypeRaid;

		// Token: 0x0400671C RID: 26396
		public GameObject m_objEventTypeRaidExpried;

		// Token: 0x0400671D RID: 26397
		public GameObject m_objEventTypeDive;

		// Token: 0x0400671E RID: 26398
		[Header("일반")]
		public GameObject m_objRootEventNormal;

		// Token: 0x0400671F RID: 26399
		public Text m_lbLevel_N;

		// Token: 0x04006720 RID: 26400
		private int m_level_N;

		// Token: 0x04006721 RID: 26401
		public Text m_lbName_N;

		// Token: 0x04006722 RID: 26402
		public Text m_lbDiveLevel;

		// Token: 0x04006723 RID: 26403
		public GameObject m_objTimeLeft;

		// Token: 0x04006724 RID: 26404
		public Text m_lbTimeLeft;

		// Token: 0x04006725 RID: 26405
		public Text m_lbDiveSlotCount;

		// Token: 0x04006726 RID: 26406
		public Image m_imgEventPointColor_N;

		// Token: 0x04006727 RID: 26407
		[Header("레이드 뷰")]
		public GameObject m_objRootEventRaid;

		// Token: 0x04006728 RID: 26408
		public Text m_lbLevel_R;

		// Token: 0x04006729 RID: 26409
		private int m_level_R;

		// Token: 0x0400672A RID: 26410
		public Text m_lbName_R;

		// Token: 0x0400672B RID: 26411
		public Text m_lbHPLeft_R;

		// Token: 0x0400672C RID: 26412
		public Text m_lbTargetHP_R;

		// Token: 0x0400672D RID: 26413
		public Image m_imgTargetHP_R;

		// Token: 0x0400672E RID: 26414
		public Image m_imgEventPointColor_R;

		// Token: 0x0400672F RID: 26415
		public GameObject m_objEntryCheck;

		// Token: 0x04006730 RID: 26416
		public GameObject m_objTryCount;

		// Token: 0x04006731 RID: 26417
		public Text m_lbTryCount;

		// Token: 0x04006732 RID: 26418
		[Header("최고 피해 표시")]
		public GameObject m_objMVP_R;

		// Token: 0x04006733 RID: 26419
		public GameObject m_objMyMVP;

		// Token: 0x04006734 RID: 26420
		public Text m_lbMVPName_R;

		// Token: 0x04006735 RID: 26421
		[Header("이벤트 상태표시")]
		public GameObject m_objProgress_R;

		// Token: 0x04006736 RID: 26422
		public GameObject m_objFail_R;

		// Token: 0x04006737 RID: 26423
		public Text m_lbFail_R;

		// Token: 0x04006738 RID: 26424
		public GameObject m_objComplete_R;

		// Token: 0x04006739 RID: 26425
		[Header("참가인원 표시")]
		public GameObject m_objAttendLimit;

		// Token: 0x0400673A RID: 26426
		public Text m_lbAttendLimit;

		// Token: 0x0400673B RID: 26427
		[Header("도시정보")]
		public GameObject m_objRootCityInfo;

		// Token: 0x0400673C RID: 26428
		public Text m_lbCityLevel;

		// Token: 0x0400673D RID: 26429
		public Image m_imgCityExp;

		// Token: 0x0400673E RID: 26430
		public Text m_lbCityName;

		// Token: 0x0400673F RID: 26431
		[Header("지원요청")]
		public GameObject m_objRootHelp;

		// Token: 0x04006740 RID: 26432
		public Image m_imgHelpUserIcon;

		// Token: 0x04006741 RID: 26433
		public Text m_lbHelpUserName;

		// Token: 0x04006742 RID: 26434
		public Text m_lbHelpUserUID;

		// Token: 0x04006743 RID: 26435
		public Color m_colUserName;

		// Token: 0x04006744 RID: 26436
		public Color m_colUserNameWhenMe;

		// Token: 0x04006745 RID: 26437
		public GameObject m_objMyHelp;

		// Token: 0x04006746 RID: 26438
		[Header("버튼 상단")]
		public GameObject m_objBtnOnProgress;

		// Token: 0x04006747 RID: 26439
		public GameObject m_objCoop;

		// Token: 0x04006748 RID: 26440
		public GameObject m_csbtnResult;

		// Token: 0x04006749 RID: 26441
		public NKCUIComStateButton m_csbtnDelete;

		// Token: 0x0400674A RID: 26442
		[Header("버튼 하단")]
		public NKCUIComStateButton m_csbtnPlay;

		// Token: 0x0400674B RID: 26443
		public GameObject m_objBtnBlue;

		// Token: 0x0400674C RID: 26444
		public Text m_lbBtnBlue;

		// Token: 0x0400674D RID: 26445
		public GameObject m_objBtnOK;

		// Token: 0x0400674E RID: 26446
		private int m_cityID;

		// Token: 0x0400674F RID: 26447
		private int m_eventID;

		// Token: 0x04006750 RID: 26448
		private long m_eventUID;

		// Token: 0x04006751 RID: 26449
		private bool m_bNeedTimeUpdate = true;

		// Token: 0x04006752 RID: 26450
		private NKCPopupWorldMapEventListSlot.OnMove dOnMove;

		// Token: 0x04006753 RID: 26451
		private float m_fTimer;

		// Token: 0x0200181D RID: 6173
		private enum MiddleState
		{
			// Token: 0x0400A814 RID: 43028
			Normal,
			// Token: 0x0400A815 RID: 43029
			Raid
		}

		// Token: 0x0200181E RID: 6174
		private enum eButtonState_Top
		{
			// Token: 0x0400A817 RID: 43031
			None,
			// Token: 0x0400A818 RID: 43032
			Process,
			// Token: 0x0400A819 RID: 43033
			Help,
			// Token: 0x0400A81A RID: 43034
			Result,
			// Token: 0x0400A81B RID: 43035
			Delete
		}

		// Token: 0x0200181F RID: 6175
		private enum eButtonState_Bottom
		{
			// Token: 0x0400A81D RID: 43037
			None,
			// Token: 0x0400A81E RID: 43038
			Help,
			// Token: 0x0400A81F RID: 43039
			Move,
			// Token: 0x0400A820 RID: 43040
			OK
		}

		// Token: 0x02001820 RID: 6176
		private enum eEventState
		{
			// Token: 0x0400A822 RID: 43042
			None,
			// Token: 0x0400A823 RID: 43043
			Fail,
			// Token: 0x0400A824 RID: 43044
			MVP,
			// Token: 0x0400A825 RID: 43045
			Progress,
			// Token: 0x0400A826 RID: 43046
			Complete,
			// Token: 0x0400A827 RID: 43047
			Expired
		}

		// Token: 0x02001821 RID: 6177
		// (Invoke) Token: 0x0600B522 RID: 46370
		public delegate void OnMove(int cityID, int eventID, long eventUID);
	}
}
