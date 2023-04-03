using System;
using System.Collections.Generic;
using NKC.UI.Event;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009BA RID: 2490
	public class NKCUIMissionAchieveSlot : MonoBehaviour
	{
		// Token: 0x06006986 RID: 27014 RVA: 0x0022200B File Offset: 0x0022020B
		public NKMMissionTemplet GetNKMMissionTemplet()
		{
			return this.m_MissionTemplet;
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x00222013 File Offset: 0x00220213
		public NKMMissionData GetNKMMissionData()
		{
			return this.m_MissionData;
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x0022201B File Offset: 0x0022021B
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x00222028 File Offset: 0x00220228
		public static NKCUIMissionAchieveSlot GetNewInstance(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIMissionAchieveSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIMissionAchieveSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIMissionAchieveSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			component.Init();
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x002220B4 File Offset: 0x002202B4
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x002220D4 File Offset: 0x002202D4
		public void Init()
		{
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				if (this.m_lstRewardSlot[i] != null)
				{
					this.m_lstRewardSlot[i].Init();
				}
			}
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x0022211C File Offset: 0x0022031C
		private void Update()
		{
			if (base.gameObject.activeSelf && base.gameObject.activeInHierarchy && this.m_fLastUIUpdateTime + 1f < Time.time)
			{
				this.m_fLastUIUpdateTime = Time.time;
				this.UpdateRemainCoolTimeTextUI();
			}
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x0022215C File Offset: 0x0022035C
		public void SetData(NKMMissionTemplet cNKMMissionTemplet, NKCUIMissionAchieveSlot.OnClickMASlot OnClickMASlotMove = null, NKCUIMissionAchieveSlot.OnClickMASlot OnClickMASlotComplete = null, NKCUIMissionAchieveSlot.OnClickMASlot OnClickMASlotRefresh = null, NKCUIMissionAchieveSlot.OnClickMASlot onClickMASlotLocked = null)
		{
			bool flag = false;
			if (this.m_MissionTemplet == cNKMMissionTemplet)
			{
				flag = true;
			}
			this.m_MissionTemplet = cNKMMissionTemplet;
			this.m_MissionData = NKMMissionManager.GetMissionData(cNKMMissionTemplet);
			this.m_MissionUIData = NKMMissionManager.GetMissionStateData(cNKMMissionTemplet);
			if (this.m_MissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_MissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			this.m_OnClickMASlotMove = OnClickMASlotMove;
			this.m_OnClickMASlotComplete = OnClickMASlotComplete;
			this.m_OnClickMASlotRefresh = OnClickMASlotRefresh;
			this.m_OnClickMASlotLocked = onClickMASlotLocked;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnProgress, new UnityAction(this.OnClickMove));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnComplete, new UnityAction(this.OnClickComplete));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRefresh, new UnityAction(this.OnClickRefresh));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDisable, new UnityAction(this.OnClickLocked));
			NKMMissionManager.MissionState state = this.m_MissionUIData.state;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_CLEAR, state == NKMMissionManager.MissionState.CAN_COMPLETE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_REPEAT_BADGE, this.m_MissionTemplet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.NONE);
			NKCUtil.SetImageSprite(this.m_ImgMissionIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_MISSION_SPRITE", "AB_UI_" + this.m_MissionTemplet.m_MissionIcon, false), false);
			if (this.m_MissionTemplet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.NONE)
			{
				if (!flag)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_DAILY, this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_WEEKLY, this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_MONTHLY, this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY);
					if (this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY)
					{
						NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_REPEAT_BADGE_Text, NKCUtilString.GET_STRING_MISSION_RESET_INTERVAL_DAILY);
					}
					else if (this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY)
					{
						NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_REPEAT_BADGE_Text, NKCUtilString.GET_STRING_MISSION_RESET_INTERVAL_WEEKLY);
					}
					else if (this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY)
					{
						NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_REPEAT_BADGE_Text, NKCUtilString.GET_STRING_MISSION_RESET_INTERVAL_MONTHLY);
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_DAILY, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_WEEKLY, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_MONTHLY, false);
			}
			NKM_MISSION_RESET_INTERVAL resetInterval = this.m_MissionTemplet.m_ResetInterval;
			bool flag2 = state == NKMMissionManager.MissionState.CAN_COMPLETE || state == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE;
			bool flag3 = state == NKMMissionManager.MissionState.REPEAT_COMPLETED || state == NKMMissionManager.MissionState.COMPLETED;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_REPEAT_CLEAR, state == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE || state == NKMMissionManager.MissionState.REPEAT_COMPLETED);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_CLEAR_IMG, state == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_GAUGE_COMPLETE, state == NKMMissionManager.MissionState.REPEAT_COMPLETED || state == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE || state == NKMMissionManager.MissionState.COMPLETED || state == NKMMissionManager.MissionState.CAN_COMPLETE);
			NKCUtil.SetGameobjectActive(this.m_csbtnComplete, flag2);
			if (state == NKMMissionManager.MissionState.ONGOING)
			{
				if (this.m_MissionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.DONATE_MISSION_ITEM)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnProgress, true);
					NKCUtil.SetGameobjectActive(this.m_csbtnDisable, false);
					NKCUIComStateButton csbtnProgress = this.m_csbtnProgress;
					if (csbtnProgress != null)
					{
						csbtnProgress.SetTitleText(NKCStringTable.GetString("SI_PF_MISSION_DONATE", false));
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnProgress, this.m_MissionTemplet.m_ShortCutType > NKM_SHORTCUT_TYPE.SHORTCUT_NONE);
					NKCUtil.SetGameobjectActive(this.m_csbtnDisable, this.m_MissionTemplet.m_ShortCutType == NKM_SHORTCUT_TYPE.SHORTCUT_NONE);
					NKCUIComStateButton csbtnProgress2 = this.m_csbtnProgress;
					if (csbtnProgress2 != null)
					{
						csbtnProgress2.SetTitleText(NKCStringTable.GetString("SI_PF_MISSION_MOVE", false));
					}
				}
				NKCUtil.SetGameobjectActive(this.m_csbtnRefresh, this.m_MissionTemplet.m_MissionPoolID > 0);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnProgress, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnDisable, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnRefresh, false);
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_MISSION_TITLE, this.m_MissionTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_MISSION_EXPLAIN, this.m_MissionTemplet.GetDesc());
			long progressCount = this.m_MissionUIData.progressCount;
			if (this.m_NKM_UI_MISSION_LIST_SLOT_SLIDER != null)
			{
				this.m_NKM_UI_MISSION_LIST_SLOT_SLIDER.value = (float)progressCount / (float)this.m_MissionTemplet.m_Times;
			}
			if (state == NKMMissionManager.MissionState.ONGOING || state == NKMMissionManager.MissionState.LOCKED)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_GAUGE_TEXT, string.Format("{0} / {1}", progressCount, this.m_MissionTemplet.m_Times));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_GAUGE_TEXT, "");
			}
			NKCScenManager.GetScenManager().GetMyUserData();
			this.m_bHasCooltime = false;
			if (missionTabTemplet.HasDateLimit && !NKCSynchronizedTime.IsStarted(missionTabTemplet.m_startTimeUtc))
			{
				this.m_bHasCooltime = true;
				this.m_NextActivateTime = missionTabTemplet.m_startTimeUtc;
			}
			if (!NKMContentUnlockManager.IsStarted(missionTabTemplet.m_UnlockInfo))
			{
				this.m_bHasCooltime = true;
				this.m_NextActivateTime = NKMContentUnlockManager.GetConditionStartTime(missionTabTemplet.m_UnlockInfo);
			}
			else if (this.m_MissionTemplet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.NONE && this.m_MissionUIData.IsMissionCompleted)
			{
				this.m_bHasCooltime = true;
				this.m_NextActivateTime = this.GetNextResetTime();
			}
			if (!this.m_bHasCooltime)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COOL_TIME, false);
				if (this.m_MissionUIData.IsMissionCompleted)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE_TEXT, missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.EMBLEM);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_EMBLEM_COMPLETE_TEXT, missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.EMBLEM);
					NKCUtil.SetGameobjectActive(this.m_csbtnComplete, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnProgress, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COOL_TIME, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnComplete, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnProgress, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE, false);
				this.UpdateRemainCoolTimeTextUI();
			}
			if (!flag)
			{
				for (int i = 0; i < this.m_MissionTemplet.m_MissionReward.Count; i++)
				{
					if (this.m_lstRewardSlot.Count > i)
					{
						MissionReward missionReward = this.m_MissionTemplet.m_MissionReward[i];
						NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(missionReward.reward_type, missionReward.reward_id, missionReward.reward_value, 0);
						if (this.m_bShowRewardName)
						{
							bool bShowNumber = false;
							NKCUISlot.eSlotMode eType = slotData.eType;
							if (eType == NKCUISlot.eSlotMode.ItemMisc)
							{
								goto IL_5BF;
							}
							if (eType != NKCUISlot.eSlotMode.Mold)
							{
								if (eType == NKCUISlot.eSlotMode.UnitCount)
								{
									goto IL_5BF;
								}
							}
							else
							{
								NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(slotData.ID);
								if (itemMoldTempletByID != null)
								{
									bShowNumber = !itemMoldTempletByID.m_bPermanent;
								}
							}
							IL_5E2:
							this.m_lstRewardSlot[i].SetData(slotData, true, bShowNumber, true, null);
							this.m_lstRewardSlot[i].SetOpenItemBoxOnClick();
							goto IL_625;
							IL_5BF:
							bShowNumber = true;
							goto IL_5E2;
						}
						this.m_lstRewardSlot[i].SetData(slotData, true, null);
						IL_625:
						this.m_lstRewardSlot[i].SetActive(true);
					}
					else
					{
						Debug.LogError(string.Format("보상슬롯 갯수가 부족함 - 미션아이디 : {0}, 슬롯 갯수 : {1}, 보상 갯수 : {2}", this.m_MissionTemplet.m_MissionID, this.m_lstRewardSlot.Count, this.m_MissionTemplet.m_MissionReward.Count));
					}
				}
				for (int j = this.m_MissionTemplet.m_MissionReward.Count; j < this.m_lstRewardSlot.Count; j++)
				{
					this.m_lstRewardSlot[j].SetActive(false);
				}
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.EVENT_PASS && (flag2 || flag3))
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnRefresh, false);
			}
			this.m_bNeedRefresh = false;
			bool flag4 = NKMMissionManager.IsMissionOpened(this.m_MissionTemplet);
			NKCUtil.SetGameobjectActive(this.m_objLock, !flag4 && !this.m_bHasCooltime);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x00222898 File Offset: 0x00220A98
		private DateTime GetNextResetTime()
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(-1.0);
			NKMTime.TimePeriod timePeriod = NKMTime.TimePeriod.Day;
			if (this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY)
			{
				timePeriod = NKMTime.TimePeriod.Day;
			}
			else if (this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY)
			{
				timePeriod = NKMTime.TimePeriod.Week;
			}
			else if (this.m_MissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY)
			{
				timePeriod = NKMTime.TimePeriod.Month;
			}
			return NKMTime.GetNextResetTime(serverUTCTime, timePeriod);
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x002228F0 File Offset: 0x00220AF0
		private void UpdateRemainCoolTimeTextUI()
		{
			if (this.m_bHasCooltime)
			{
				TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_NextActivateTime);
				if (timeLeft.TotalMilliseconds <= 0.0 && !this.m_bNeedRefresh)
				{
					this.m_bNeedRefresh = true;
					if (NKCUIMissionAchievement.IsInstanceOpen)
					{
						NKCUIMissionAchievement.Instance.RefreshMissionUI();
					}
					if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
					}
					if (NKCUIEvent.IsInstanceOpen)
					{
						NKCUIEvent.Instance.RefreshUI(0);
					}
				}
				string msg;
				if (timeLeft.TotalDays >= 1.0)
				{
					msg = string.Format(NKCUtilString.GET_STRING_MISSION_REMAIN_TWO_PARAM, NKCUtilString.GET_STRING_TIME_PERIOD, string.Format(NKCUtilString.GET_STRING_TIME_DAY_ONE_PARAM, (int)timeLeft.TotalDays));
				}
				else if (timeLeft.TotalHours >= 1.0)
				{
					msg = string.Format(NKCUtilString.GET_STRING_MISSION_REMAIN_TWO_PARAM, NKCUtilString.GET_STRING_TIME, string.Format(NKCUtilString.GET_STRING_TIME_HOUR_ONE_PARAM, (int)timeLeft.TotalHours));
				}
				else if (timeLeft.TotalMinutes >= 1.0)
				{
					msg = string.Format(NKCUtilString.GET_STRING_MISSION_REMAIN_TWO_PARAM, NKCUtilString.GET_STRING_TIME, string.Format(NKCUtilString.GET_STRING_TIME_MINUTE_ONE_PARAM, (int)timeLeft.TotalMinutes));
				}
				else if (timeLeft.TotalSeconds >= 1.0)
				{
					msg = string.Format(NKCUtilString.GET_STRING_MISSION_REMAIN_TWO_PARAM, NKCUtilString.GET_STRING_TIME, string.Format(NKCUtilString.GET_STRING_TIME_SECOND_ONE_PARAM, (int)timeLeft.TotalSeconds));
				}
				else
				{
					msg = string.Format(NKCUtilString.GET_STRING_MISSION_REMAIN_TWO_PARAM, NKCUtilString.GET_STRING_TIME, NKCUtilString.GET_STRING_TIME_A_SECOND_AGO);
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_MISSION_LIST_SLOT_BUTTONS_COMPLETE_TEXT, msg);
			}
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x00222A88 File Offset: 0x00220C88
		public void OnClickMove()
		{
			if (this.m_MissionTemplet != null && this.m_MissionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.DONATE_MISSION_ITEM)
			{
				this.OnDonate();
				return;
			}
			if (this.m_OnClickMASlotMove != null)
			{
				this.m_OnClickMASlotMove(this);
			}
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x00222AC1 File Offset: 0x00220CC1
		public void OnClickComplete()
		{
			if (this.m_OnClickMASlotComplete != null)
			{
				this.m_OnClickMASlotComplete(this);
			}
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x00222AD7 File Offset: 0x00220CD7
		public void OnClickRefresh()
		{
			if (this.m_OnClickMASlotRefresh != null)
			{
				this.m_OnClickMASlotRefresh(this);
			}
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x00222AED File Offset: 0x00220CED
		public void OnClickLocked()
		{
			if (this.m_OnClickMASlotLocked != null)
			{
				this.m_OnClickMASlotLocked(this);
			}
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x00222B04 File Offset: 0x00220D04
		private void OnDonate()
		{
			if (this.m_MissionTemplet == null)
			{
				return;
			}
			if (this.m_MissionTemplet.m_MissionCond.value1.Count == 0)
			{
				return;
			}
			int num = this.m_MissionTemplet.m_MissionCond.value1[0];
			long times = this.m_MissionTemplet.m_Times;
			long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(num);
			long progressCount = this.m_MissionUIData.progressCount;
			int num2 = (int)(times - progressCount);
			int num3 = (int)Math.Min((long)num2, countMiscItem);
			int minValue = (num3 > 0) ? 1 : 0;
			NKCUISlot.SlotData itemSlotData = NKCUISlot.SlotData.MakeMiscItemData(num, 1L, 0);
			string itemName = NKMItemManager.GetItemMiscTempletByID(num).GetItemName();
			string @string = NKCStringTable.GetString("SI_DP_POPUP_DELIVERY_MISSION_TITLE", false);
			string desc = string.Format(NKCStringTable.GetString("SI_PF_POPUP_DELIVERY_MISSION_DESC", false), itemName);
			NKCPopupItemSlider.Instance.Open(@string, desc, itemSlotData, minValue, num3, num2, true, new NKCPopupItemSlider.OnConfirm(this.OnDonateConfirm), num3);
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x00222BE8 File Offset: 0x00220DE8
		private void OnDonateConfirm(int count)
		{
			if (count <= 0)
			{
				return;
			}
			int num = this.m_MissionTemplet.m_MissionCond.value1[0];
			NKCPacketSender.Send_NKMPacket_MISSION_GIVE_ITEM_REQ(this.m_MissionTemplet.m_MissionID, count);
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x00222C17 File Offset: 0x00220E17
		public void SetActive(bool bSet)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bSet);
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x00222C25 File Offset: 0x00220E25
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x00222C34 File Offset: 0x00220E34
		public void SetForceActivateEventPassRefreshButton()
		{
			NKMMissionManager.MissionState state = this.m_MissionUIData.state;
			bool flag = state == NKMMissionManager.MissionState.CAN_COMPLETE || state == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE;
			bool flag2 = state == NKMMissionManager.MissionState.REPEAT_COMPLETED || state == NKMMissionManager.MissionState.COMPLETED;
			if (flag || flag2)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnRefresh, true);
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x00222C74 File Offset: 0x00220E74
		public void SetForceMissionDisabled()
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_MissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (!this.m_bHasCooltime)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COOL_TIME, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE_TEXT, missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.EMBLEM);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_EMBLEM_COMPLETE_TEXT, missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.EMBLEM);
				NKCUtil.SetGameobjectActive(this.m_csbtnComplete, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnProgress, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COOL_TIME, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnComplete, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnProgress, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_SLOT_COMPLETE, false);
			this.UpdateRemainCoolTimeTextUI();
		}

		// Token: 0x0400552D RID: 21805
		[Header("미션 아이콘")]
		public Image m_ImgMissionIcon;

		// Token: 0x0400552E RID: 21806
		[Header("미션 반복 표시")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_REPEAT_BADGE;

		// Token: 0x0400552F RID: 21807
		public Text m_NKM_UI_MISSION_LIST_SLOT_REPEAT_BADGE_Text;

		// Token: 0x04005530 RID: 21808
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_REPEAT_CLEAR;

		// Token: 0x04005531 RID: 21809
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_CLEAR_IMG;

		// Token: 0x04005532 RID: 21810
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_DAILY;

		// Token: 0x04005533 RID: 21811
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_WEEKLY;

		// Token: 0x04005534 RID: 21812
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_BADGE_BG_MONTHLY;

		// Token: 0x04005535 RID: 21813
		[Header("미션 완료 버튼")]
		public NKCUIComStateButton m_csbtnDisable;

		// Token: 0x04005536 RID: 21814
		public NKCUIComStateButton m_csbtnComplete;

		// Token: 0x04005537 RID: 21815
		public NKCUIComStateButton m_csbtnProgress;

		// Token: 0x04005538 RID: 21816
		public NKCUIComStateButton m_csbtnRefresh;

		// Token: 0x04005539 RID: 21817
		[Header("미션 완료 표시")]
		[Tooltip("완료도 하고 보상도 받았을때 나옴")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_COMPLETE;

		// Token: 0x0400553A RID: 21818
		[Tooltip("완료도 하고 보상도 받았을때 나옴")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_COMPLETE_TEXT;

		// Token: 0x0400553B RID: 21819
		[Tooltip("완료도 하고 보상도 받은 엠블럼 미션")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_EMBLEM_COMPLETE_TEXT;

		// Token: 0x0400553C RID: 21820
		[Tooltip("완료만 하고 보상 아직 안받음")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_CLEAR;

		// Token: 0x0400553D RID: 21821
		[Header("미션 제목")]
		public Text m_NKM_UI_MISSION_LIST_SLOT_MISSION_TITLE;

		// Token: 0x0400553E RID: 21822
		[Header("미션 설명")]
		public Text m_NKM_UI_MISSION_LIST_SLOT_MISSION_EXPLAIN;

		// Token: 0x0400553F RID: 21823
		[Header("미션 진행도")]
		[Tooltip("미션 진행도 슬라이더")]
		public Slider m_NKM_UI_MISSION_LIST_SLOT_SLIDER;

		// Token: 0x04005540 RID: 21824
		[Tooltip("미션 진행도 텍스트 : 0/1 이런거")]
		public Text m_NKM_UI_MISSION_LIST_SLOT_GAUGE_TEXT;

		// Token: 0x04005541 RID: 21825
		[Tooltip("미션 완료했을 때 나타나는 오브젝트")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_GAUGE_COMPLETE;

		// Token: 0x04005542 RID: 21826
		[Header("반복 미션 쿨타임")]
		[Tooltip("쿨타임 중인 미션")]
		public GameObject m_NKM_UI_MISSION_LIST_SLOT_COOL_TIME;

		// Token: 0x04005543 RID: 21827
		[Tooltip("다시 시도 가능할때까지 남은 시간")]
		public Text m_NKM_UI_MISSION_LIST_SLOT_BUTTONS_COMPLETE_TEXT;

		// Token: 0x04005544 RID: 21828
		[Header("보상 슬롯")]
		public List<NKCUISlot> m_lstRewardSlot;

		// Token: 0x04005545 RID: 21829
		public bool m_bShowRewardName;

		// Token: 0x04005546 RID: 21830
		[Header("진행 불가능 오브젝트")]
		public GameObject m_objLock;

		// Token: 0x04005547 RID: 21831
		private NKMMissionTemplet m_MissionTemplet;

		// Token: 0x04005548 RID: 21832
		private NKMMissionData m_MissionData;

		// Token: 0x04005549 RID: 21833
		private NKMMissionManager.MissionStateData m_MissionUIData;

		// Token: 0x0400554A RID: 21834
		private NKCUIMissionAchieveSlot.OnClickMASlot m_OnClickMASlotMove;

		// Token: 0x0400554B RID: 21835
		private NKCUIMissionAchieveSlot.OnClickMASlot m_OnClickMASlotComplete;

		// Token: 0x0400554C RID: 21836
		private NKCUIMissionAchieveSlot.OnClickMASlot m_OnClickMASlotRefresh;

		// Token: 0x0400554D RID: 21837
		private NKCUIMissionAchieveSlot.OnClickMASlot m_OnClickMASlotLocked;

		// Token: 0x0400554E RID: 21838
		private float m_fLastUIUpdateTime;

		// Token: 0x0400554F RID: 21839
		private bool m_bNeedRefresh;

		// Token: 0x04005550 RID: 21840
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04005551 RID: 21841
		private NKCUIComButton m_csbtnNKM_UI_MISSION_LIST_SLOT_BUTTONS_PROGRESS;

		// Token: 0x04005552 RID: 21842
		private NKCUIComButton m_csbtnm_NKM_UI_MISSION_LIST_SLOT_BUTTONS_DISABLE;

		// Token: 0x04005553 RID: 21843
		private bool m_bHasCooltime;

		// Token: 0x04005554 RID: 21844
		private DateTime m_NextActivateTime;

		// Token: 0x020016B8 RID: 5816
		// (Invoke) Token: 0x0600B11C RID: 45340
		public delegate void OnClickMASlot(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot);
	}
}
