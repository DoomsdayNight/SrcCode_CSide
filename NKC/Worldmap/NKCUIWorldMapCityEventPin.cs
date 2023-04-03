using System;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC2 RID: 2754
	public class NKCUIWorldMapCityEventPin : MonoBehaviour
	{
		// Token: 0x06007B16 RID: 31510 RVA: 0x00290691 File Offset: 0x0028E891
		public void Init(NKCUIWorldMapCityEventPin.OnClickEvent onClickEvent)
		{
			this.dOnClickEvent = onClickEvent;
			this.m_sbtnPin.PointerClick.RemoveAllListeners();
			this.m_sbtnPin.PointerClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x002906C6 File Offset: 0x0028E8C6
		public Vector3 GetPinSDPos()
		{
			return this.m_rtSDRoot.transform.localPosition + base.transform.localPosition;
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x002906E8 File Offset: 0x0028E8E8
		public void PlaySDAnim(NKCASUIUnitIllust.eAnimation eAnim, bool bLoop = false)
		{
			if (this.m_spineSD == null && this.m_aniSoloRaid == null)
			{
				this.OpenSDIllust(eAnim, bLoop);
				return;
			}
			if (this.m_spineSD != null)
			{
				this.m_spineSD.SetAnimation(eAnim, bLoop, 0, true, 0f, true);
			}
			if (this.m_aniSoloRaid != null)
			{
				if (eAnim == NKCASUIUnitIllust.eAnimation.SD_START)
				{
					this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_OPEN");
					return;
				}
				this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_IDLE");
			}
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x00290769 File Offset: 0x0028E969
		public void CleanUpSpineSD()
		{
			if (this.m_spineSD != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
				this.m_spineSD = null;
			}
			if (this.m_aniSoloRaid != null)
			{
				this.m_aniSoloRaid = null;
			}
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x002907A4 File Offset: 0x0028E9A4
		public void CleanUp()
		{
			this.CleanUpSpineSD();
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x002907AC File Offset: 0x0028E9AC
		private bool OpenSDIllust(NKCASUIUnitIllust.eAnimation eStartAnim = NKCASUIUnitIllust.eAnimation.NONE, bool bLoopStartAnim = false)
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(this.m_EventID);
			if (nkmworldMapEventTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
				this.m_spineSD = null;
				return false;
			}
			GameObject objSoloRaid = this.m_objSoloRaid;
			NKMRaidTemplet raidTemplet = nkmworldMapEventTemplet.raidTemplet;
			NKCUtil.SetGameobjectActive(objSoloRaid, raidTemplet != null && raidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID);
			if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID && nkmworldMapEventTemplet.raidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
			{
				if (this.m_aniSoloRaid == null)
				{
					NKCUtil.SetImageSprite(this.m_imgSoloRaid, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", nkmworldMapEventTemplet.spineSDName, false), false);
					this.m_aniSoloRaid = this.m_objSoloRaid.GetComponentInChildren<Animator>();
				}
				if (this.m_aniSoloRaid != null)
				{
					if (eStartAnim == NKCASUIUnitIllust.eAnimation.SD_START)
					{
						this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_OPEN");
					}
					else
					{
						this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_IDLE");
					}
				}
				return true;
			}
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(nkmworldMapEventTemplet, false);
			if (this.m_spineSD != null && (this.m_spineSD.m_SpineIllustInstant == null || this.m_spineSD.m_SpineIllustInstant_SkeletonGraphic == null))
			{
				this.m_spineSD = null;
			}
			if (this.m_spineSD != null)
			{
				if (eStartAnim == NKCASUIUnitIllust.eAnimation.NONE)
				{
					this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				}
				else
				{
					this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, false, false, 0f);
					this.m_spineSD.SetAnimation(eStartAnim, bLoopStartAnim, 0, true, 0f, true);
				}
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector2.zero;
					if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
					{
						if (NKMRandom.Range(0, 3) == 1)
						{
							rectTransform.localScale = Vector3.one;
						}
						else
						{
							rectTransform.localScale = new Vector3(-1f, 1f, 1f);
						}
					}
					else
					{
						rectTransform.localScale = Vector3.one;
					}
					rectTransform.localRotation = Quaternion.identity;
				}
				return true;
			}
			Debug.Log("spine SD data not found from worldmapEventID : " + nkmworldMapEventTemplet.worldmapEventID.ToString());
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
			return false;
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x002909F8 File Offset: 0x0028EBF8
		private void OnClick()
		{
			NKCUIWorldMapCityEventPin.OnClickEvent onClickEvent = this.dOnClickEvent;
			if (onClickEvent == null)
			{
				return;
			}
			onClickEvent(this.m_CityID, this.m_EventID, this.m_EventUID);
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x00290A1C File Offset: 0x0028EC1C
		private void ProcessSDAnim()
		{
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(this.m_EventID);
			if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID && !NKCUIManager.CheckScreenInputBlock())
			{
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_EventUID);
				if (nkmraidDetailData != null && nkmraidDetailData.curHP > 0f)
				{
					NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
					if (nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
					{
						NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
						if ((nkmraidJoinData == null || (int)nkmraidJoinData.tryCount < nkmraidTemplet.RaidTryCount) && this.m_aniSoloRaid != null)
						{
							this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_IDLE");
							return;
						}
					}
					else if (this.m_spineSD != null && NKMRandom.Range(0, 35) <= 1)
					{
						this.PlaySDAnim(NKCASUIUnitIllust.eAnimation.SD_ATTACK, false);
					}
				}
			}
		}

		// Token: 0x06007B1E RID: 31518 RVA: 0x00290AF3 File Offset: 0x0028ECF3
		private void Update()
		{
			if (this.m_fUpdateTimeForUI + 1f < Time.time)
			{
				this.m_fUpdateTimeForUI = Time.time;
				this.SetMarkAndStateUI();
				this.SetActiveSDLV();
				this.SetTimeUI();
				this.ProcessSDAnim();
			}
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x00290B2C File Offset: 0x0028ED2C
		private void SetTimeUI()
		{
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(this.m_EventID);
			if (nkmworldMapEventTemplet == null)
			{
				return;
			}
			NKM_WORLDMAP_EVENT_TYPE eventType = nkmworldMapEventTemplet.eventType;
			if (eventType != NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(this.m_CityID);
					if (cityData != null)
					{
						bool flag = NKCScenManager.CurrentUserData().m_DiveGameData != null && NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == cityData.worldMapEventGroup.eventUid;
						if (NKCSynchronizedTime.IsFinished(cityData.worldMapEventGroup.eventGroupEndDate) || (cityData.worldMapEventGroup.eventUid > 0L && !flag))
						{
							NKCUtil.SetGameobjectActive(this.m_objTime, false);
							return;
						}
						NKCUtil.SetGameobjectActive(this.m_objTime, true);
						DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
						TimeSpan timeSpan = cityData.worldMapEventGroup.eventGroupEndDate - serverUTCTime;
						this.m_lbTime.text = NKCUtilString.GetTimeSpanString(timeSpan);
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objTime, false);
				}
			}
			else
			{
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_EventUID);
				if (nkmraidDetailData != null)
				{
					if (nkmraidDetailData.curHP <= 0f || NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
					{
						NKCUtil.SetGameobjectActive(this.m_objTime, false);
						return;
					}
					NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
					if (nkmraidTemplet.DungeonTempletBase.m_DungeonType != NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
					{
						NKCUtil.SetGameobjectActive(this.m_objTime, true);
						DateTime d = new DateTime(nkmraidDetailData.expireDate);
						DateTime serverUTCTime2 = NKCSynchronizedTime.GetServerUTCTime(0.0);
						TimeSpan timeSpan2 = d - serverUTCTime2;
						NKCUtil.SetLabelText(this.m_lbTime, NKCUtilString.GetTimeSpanString(timeSpan2));
						return;
					}
					NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
					if (nkmraidJoinData != null && (int)nkmraidJoinData.tryCount >= nkmraidTemplet.RaidTryCount)
					{
						NKCUtil.SetGameobjectActive(this.m_objTime, false);
						return;
					}
					NKCUtil.SetGameobjectActive(this.m_objTime, true);
					DateTime d2 = new DateTime(nkmraidDetailData.expireDate);
					DateTime serverUTCTime3 = NKCSynchronizedTime.GetServerUTCTime(0.0);
					TimeSpan timeSpan3 = d2 - serverUTCTime3;
					NKCUtil.SetLabelText(this.m_lbTime, NKCUtilString.GetTimeSpanString(timeSpan3));
					return;
				}
			}
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x00290D4C File Offset: 0x0028EF4C
		private bool CheckDiveExpired(NKMWorldMapCityData cNKMWorldMapCityData)
		{
			if (cNKMWorldMapCityData == null)
			{
				return false;
			}
			bool flag = NKCScenManager.CurrentUserData().m_DiveGameData != null && NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == this.m_EventUID;
			return NKCSynchronizedTime.IsFinished(cNKMWorldMapCityData.worldMapEventGroup.eventGroupEndDate) || (!flag && cNKMWorldMapCityData.worldMapEventGroup.eventUid > 0L);
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x00290DAC File Offset: 0x0028EFAC
		private void SetMarkAndStateUI()
		{
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(this.m_EventID);
			if (nkmworldMapEventTemplet == null)
			{
				return;
			}
			NKM_WORLDMAP_EVENT_TYPE eventType = nkmworldMapEventTemplet.eventType;
			if (eventType != NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					NKCUIWorldMapCityEventPin.eMark mark = NKCUIWorldMapCityEventPin.eMark.DiveReady;
					NKCUIWorldMapCityEventPin.eState state = NKCUIWorldMapCityEventPin.eState.None;
					NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(this.m_CityID);
					if (cityData != null)
					{
						bool flag = NKCScenManager.CurrentUserData().m_DiveGameData != null && NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == this.m_EventUID;
						if (this.CheckDiveExpired(cityData))
						{
							state = NKCUIWorldMapCityEventPin.eState.None;
							mark = NKCUIWorldMapCityEventPin.eMark.DiveExpired;
						}
						else if (flag)
						{
							state = NKCUIWorldMapCityEventPin.eState.OnProgress;
						}
						else
						{
							state = NKCUIWorldMapCityEventPin.eState.New;
						}
					}
					this.SetState(state);
					this.SetMark(mark);
					return;
				}
				this.SetMark(NKCUIWorldMapCityEventPin.eMark.None);
				this.SetState(NKCUIWorldMapCityEventPin.eState.None);
			}
			else
			{
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_EventUID);
				if (nkmraidDetailData != null)
				{
					NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
					if (nkmraidTemplet != null)
					{
						if (nkmraidDetailData.curHP <= 0f)
						{
							this.SetMark(NKCUIWorldMapCityEventPin.eMark.RaidComplete);
						}
						else if (NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
						{
							this.SetMark(NKCUIWorldMapCityEventPin.eMark.RaidFailed);
						}
						else if (nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
						{
							NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
							short? num = (nkmraidJoinData != null) ? new short?(nkmraidJoinData.tryCount) : null;
							int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
							int raidTryCount = nkmraidTemplet.RaidTryCount;
							if (num2.GetValueOrDefault() >= raidTryCount & num2 != null)
							{
								this.SetMark(NKCUIWorldMapCityEventPin.eMark.RaidFailed);
							}
							else
							{
								this.SetMark(NKCUIWorldMapCityEventPin.eMark.RaidReady);
							}
						}
						else
						{
							this.SetMark(NKCUIWorldMapCityEventPin.eMark.RaidReady);
						}
						if (nkmraidDetailData.curHP <= 0f)
						{
							this.SetState(NKCUIWorldMapCityEventPin.eState.Complete);
							return;
						}
						if (NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
						{
							this.SetState(NKCUIWorldMapCityEventPin.eState.None);
							return;
						}
						if (nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
						{
							NKMRaidJoinData nkmraidJoinData2 = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
							if (nkmraidJoinData2 == null)
							{
								this.SetState(NKCUIWorldMapCityEventPin.eState.None);
								return;
							}
							if (nkmraidJoinData2.tryCount == 0)
							{
								this.SetState(NKCUIWorldMapCityEventPin.eState.New);
								return;
							}
							if ((int)nkmraidJoinData2.tryCount >= nkmraidTemplet.RaidTryCount)
							{
								this.SetState(NKCUIWorldMapCityEventPin.eState.None);
								return;
							}
							this.SetState(NKCUIWorldMapCityEventPin.eState.OnProgress);
							return;
						}
						else
						{
							if (nkmraidDetailData.isCoop)
							{
								this.SetState(NKCUIWorldMapCityEventPin.eState.Help);
								return;
							}
							if (!nkmraidDetailData.isNew)
							{
								this.SetState(NKCUIWorldMapCityEventPin.eState.OnProgress);
								return;
							}
							this.SetState(NKCUIWorldMapCityEventPin.eState.New);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x00291018 File Offset: 0x0028F218
		public void SetData(int cityID, NKMWorldMapEventGroup eventGroupData)
		{
			this.m_CityID = cityID;
			if (eventGroupData == null)
			{
				this.m_EventID = 0;
				this.m_EventUID = 0L;
				this.SetMark(NKCUIWorldMapCityEventPin.eMark.None);
				return;
			}
			this.m_EventID = eventGroupData.worldmapEventID;
			this.m_EventUID = eventGroupData.eventUid;
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(eventGroupData.worldmapEventID);
			if (nkmworldMapEventTemplet == null)
			{
				Debug.LogError(string.Format("NKMWorldMapEventTemplet Null! id : {0}", eventGroupData.worldmapEventID));
				this.SetMark(NKCUIWorldMapCityEventPin.eMark.None);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objDiveArrow, nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE);
			NKCUtil.SetGameobjectActive(this.m_objRaidArrow, nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID);
			NKM_WORLDMAP_EVENT_TYPE eventType = nkmworldMapEventTemplet.eventType;
			if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				this.SetRaid(eventGroupData, nkmworldMapEventTemplet);
				return;
			}
			if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
			{
				this.SetDive(eventGroupData, nkmworldMapEventTemplet);
				return;
			}
			this.SetMark(NKCUIWorldMapCityEventPin.eMark.None);
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x002910DE File Offset: 0x0028F2DE
		public void SetNew(bool value)
		{
			this.SetState(NKCUIWorldMapCityEventPin.eState.New);
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x002910E7 File Offset: 0x0028F2E7
		public void UpdateRaidData()
		{
			this.SetMarkAndStateUI();
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x002910F0 File Offset: 0x0028F2F0
		private void SetDive(NKMWorldMapEventGroup eventGroupData, NKMWorldMapEventTemplet eventTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_objDiveSpecial, eventTemplet.eventGrade == NKM_WORLDMAP_EVENT_GRADE.WEG_SPECIAL);
			NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(eventTemplet.stageID);
			if (nkmdiveTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_lbLevel, true);
				NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkmdiveTemplet.StageLevel));
			}
			else
			{
				Debug.LogError(string.Format("DiveTemplet Not Found! stageID : {0}", eventTemplet.stageID));
				NKCUtil.SetGameobjectActive(this.m_lbLevel, false);
			}
			this.SetMarkAndStateUI();
			this.SetTimeUI();
			bool flag = false;
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(this.m_CityID);
			if (cityData != null && this.CheckDiveExpired(cityData))
			{
				flag = true;
			}
			if (!flag && this.OpenSDIllust(NKCASUIUnitIllust.eAnimation.NONE, false))
			{
				if (this.m_spineSD != null)
				{
					this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				}
			}
			else
			{
				this.CleanUp();
			}
			this.SetActiveSDLV();
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x002911E8 File Offset: 0x0028F3E8
		private void SetRaid(NKMWorldMapEventGroup eventGroupData, NKMWorldMapEventTemplet eventTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_objDiveSpecial, false);
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(eventGroupData.eventUid);
			NKCUtil.SetGameobjectActive(this.m_lbLevel, true);
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, "??"));
			if (nkmraidDetailData != null)
			{
				NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
				this.SetMarkAndStateUI();
				this.SetTimeUI();
				if (nkmraidTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkmraidTemplet.RaidLevel));
					bool flag = NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate);
					bool flag2 = nkmraidDetailData.curHP <= 0f;
					NKCUtil.SetGameobjectActive(this.m_objSoloRaid, !flag || flag2);
					NKCUtil.SetGameobjectActive(this.m_rtSDRoot, !flag || flag2);
					NKCUtil.SetGameobjectActive(this.m_objLevel, !flag || flag2);
					if (!flag)
					{
						NKCUtil.SetGameobjectActive(this.m_objLevel, true);
						if (flag2)
						{
							this.ProcessWinRaid(nkmraidDetailData, nkmraidTemplet);
							return;
						}
						if (nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
						{
							if (!this.OpenSDIllust(NKCASUIUnitIllust.eAnimation.NONE, false) || !(this.m_aniSoloRaid != null))
							{
								this.CleanUp();
								return;
							}
							NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
							short? num = (nkmraidJoinData != null) ? new short?(nkmraidJoinData.tryCount) : null;
							int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
							int raidTryCount = nkmraidTemplet.RaidTryCount;
							if (num2.GetValueOrDefault() >= raidTryCount & num2 != null)
							{
								this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_COMPLETE");
								return;
							}
							this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_IDLE");
							return;
						}
						else
						{
							if (this.OpenSDIllust(NKCASUIUnitIllust.eAnimation.NONE, false) && this.m_spineSD != null)
							{
								this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
								return;
							}
							this.CleanUp();
							return;
						}
					}
					else
					{
						if (flag2)
						{
							this.ProcessWinRaid(nkmraidDetailData, nkmraidTemplet);
							return;
						}
						this.CleanUp();
					}
				}
			}
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x00291400 File Offset: 0x0028F600
		private void ProcessWinRaid(NKMRaidDetailData cNKMRaidDetailData, NKMRaidTemplet cNKMRaidTemplet)
		{
			if (cNKMRaidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
			{
				if (this.OpenSDIllust(NKCASUIUnitIllust.eAnimation.NONE, false) && this.m_aniSoloRaid != null)
				{
					this.m_aniSoloRaid.Play("NKM_UI_WORLD_HOLOGRAM_COMPLETE");
					return;
				}
				this.CleanUp();
				return;
			}
			else
			{
				if (this.OpenSDIllust(NKCASUIUnitIllust.eAnimation.NONE, false) && this.m_spineSD != null)
				{
					this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_DOWN, true, 0, true, 0f, true);
					return;
				}
				this.CleanUp();
				return;
			}
		}

		// Token: 0x06007B28 RID: 31528 RVA: 0x00291480 File Offset: 0x0028F680
		private void SetActiveSDLV()
		{
			NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(this.m_EventID);
			if (nkmworldMapEventTemplet == null)
			{
				return;
			}
			bool flag = false;
			bool flag2;
			if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_EventUID);
				if (nkmraidDetailData == null)
				{
					return;
				}
				if (nkmraidDetailData.curHP <= 0f)
				{
					flag = true;
				}
				NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
				if (nkmraidTemplet == null)
				{
					return;
				}
				if (nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
				{
					flag2 = NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate);
					if (!flag2)
					{
						NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
						if (nkmraidJoinData != null)
						{
							flag2 = ((int)nkmraidJoinData.tryCount >= nkmraidTemplet.RaidTryCount && nkmraidDetailData.curHP > 0f);
						}
					}
				}
				else
				{
					flag2 = NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate);
				}
			}
			else
			{
				if (nkmworldMapEventTemplet.eventType != NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
				{
					return;
				}
				NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(this.m_CityID);
				if (cityData == null)
				{
					return;
				}
				if (NKCScenManager.CurrentUserData().m_DiveGameData != null)
				{
					bool flag3 = NKCScenManager.CurrentUserData().m_DiveGameData.DiveUid == this.m_EventUID;
				}
				flag2 = this.CheckDiveExpired(cityData);
			}
			NKCUtil.SetGameobjectActive(this.m_objSoloRaid, (!flag2 || flag) && nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID && nkmworldMapEventTemplet.raidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID);
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, !flag2 || flag);
			NKCUtil.SetGameobjectActive(this.m_objLevel, !flag2 || flag);
		}

		// Token: 0x06007B29 RID: 31529 RVA: 0x00291604 File Offset: 0x0028F804
		private void SetMark(NKCUIWorldMapCityEventPin.eMark mark)
		{
			NKCUtil.SetGameobjectActive(this.m_objRaid, mark == NKCUIWorldMapCityEventPin.eMark.RaidReady || mark == NKCUIWorldMapCityEventPin.eMark.RaidComplete);
			NKCUtil.SetGameobjectActive(this.m_objRaidComplete, mark == NKCUIWorldMapCityEventPin.eMark.RaidComplete);
			NKCUtil.SetGameobjectActive(this.m_objRaidFailed, mark == NKCUIWorldMapCityEventPin.eMark.RaidFailed);
			NKCUtil.SetGameobjectActive(this.m_objDive, mark == NKCUIWorldMapCityEventPin.eMark.DiveReady);
			NKCUtil.SetGameobjectActive(this.m_objDiveExpired, mark == NKCUIWorldMapCityEventPin.eMark.DiveExpired);
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x00291663 File Offset: 0x0028F863
		private void SetState(NKCUIWorldMapCityEventPin.eState state)
		{
			NKCUtil.SetGameobjectActive(this.m_objNew, state == NKCUIWorldMapCityEventPin.eState.New);
			NKCUtil.SetGameobjectActive(this.m_objComplete, state == NKCUIWorldMapCityEventPin.eState.Complete);
			NKCUtil.SetGameobjectActive(this.m_objHelp, state == NKCUIWorldMapCityEventPin.eState.Help);
			NKCUtil.SetGameobjectActive(this.m_objProgress, state == NKCUIWorldMapCityEventPin.eState.OnProgress);
		}

		// Token: 0x040067CD RID: 26573
		public NKCUIComStateButton m_sbtnPin;

		// Token: 0x040067CE RID: 26574
		public RectTransform m_rtSDRoot;

		// Token: 0x040067CF RID: 26575
		[Header("Marker")]
		public GameObject m_objRaid;

		// Token: 0x040067D0 RID: 26576
		public GameObject m_objSoloRaid;

		// Token: 0x040067D1 RID: 26577
		public Image m_imgSoloRaid;

		// Token: 0x040067D2 RID: 26578
		public GameObject m_objRaidComplete;

		// Token: 0x040067D3 RID: 26579
		public GameObject m_objRaidFailed;

		// Token: 0x040067D4 RID: 26580
		public GameObject m_objDive;

		// Token: 0x040067D5 RID: 26581
		public GameObject m_objDiveSpecial;

		// Token: 0x040067D6 RID: 26582
		public GameObject m_objDiveExpired;

		// Token: 0x040067D7 RID: 26583
		[Header("Line")]
		public GameObject m_objRaidArrow;

		// Token: 0x040067D8 RID: 26584
		public GameObject m_objDiveArrow;

		// Token: 0x040067D9 RID: 26585
		[Header("State Object")]
		public GameObject m_objComplete;

		// Token: 0x040067DA RID: 26586
		public GameObject m_objNew;

		// Token: 0x040067DB RID: 26587
		public GameObject m_objHelp;

		// Token: 0x040067DC RID: 26588
		public GameObject m_objProgress;

		// Token: 0x040067DD RID: 26589
		[Header("Etc")]
		public GameObject m_objLevel;

		// Token: 0x040067DE RID: 26590
		public Text m_lbLevel;

		// Token: 0x040067DF RID: 26591
		public GameObject m_objTime;

		// Token: 0x040067E0 RID: 26592
		public Text m_lbTime;

		// Token: 0x040067E1 RID: 26593
		private int m_CityID;

		// Token: 0x040067E2 RID: 26594
		private int m_EventID;

		// Token: 0x040067E3 RID: 26595
		private long m_EventUID;

		// Token: 0x040067E4 RID: 26596
		private NKCUIWorldMapCityEventPin.OnClickEvent dOnClickEvent;

		// Token: 0x040067E5 RID: 26597
		private float m_fUpdateTimeForUI;

		// Token: 0x040067E6 RID: 26598
		private NKCASUISpineIllust m_spineSD;

		// Token: 0x040067E7 RID: 26599
		private Animator m_aniSoloRaid;

		// Token: 0x02001834 RID: 6196
		public enum BossStatus
		{
			// Token: 0x0400A850 RID: 43088
			Start,
			// Token: 0x0400A851 RID: 43089
			Idle
		}

		// Token: 0x02001835 RID: 6197
		private enum eMark
		{
			// Token: 0x0400A853 RID: 43091
			None,
			// Token: 0x0400A854 RID: 43092
			RaidReady,
			// Token: 0x0400A855 RID: 43093
			RaidComplete,
			// Token: 0x0400A856 RID: 43094
			RaidFailed,
			// Token: 0x0400A857 RID: 43095
			DiveReady,
			// Token: 0x0400A858 RID: 43096
			DiveExpired
		}

		// Token: 0x02001836 RID: 6198
		public enum eState
		{
			// Token: 0x0400A85A RID: 43098
			None,
			// Token: 0x0400A85B RID: 43099
			Complete,
			// Token: 0x0400A85C RID: 43100
			New,
			// Token: 0x0400A85D RID: 43101
			Help,
			// Token: 0x0400A85E RID: 43102
			OnProgress
		}

		// Token: 0x02001837 RID: 6199
		// (Invoke) Token: 0x0600B560 RID: 46432
		public delegate void OnClickEvent(int cityID, int eventID, long eventUID);
	}
}
