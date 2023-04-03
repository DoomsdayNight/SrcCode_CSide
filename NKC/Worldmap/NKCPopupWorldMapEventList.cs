using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Mode;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AB9 RID: 2745
	public class NKCPopupWorldMapEventList : NKCUIBase
	{
		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06007A31 RID: 31281 RVA: 0x0028AD88 File Offset: 0x00288F88
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06007A32 RID: 31282 RVA: 0x0028AD8B File Offset: 0x00288F8B
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_WORLDMAP_EVENT;
			}
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x0028AD92 File Offset: 0x00288F92
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
			NKCUtil.SetGameobjectActive(this.m_objSeasonPoint, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x0028ADB8 File Offset: 0x00288FB8
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_eState == NKCPopupWorldMapEventList.eState.SeasonPoint)
			{
				this.m_NKCUIRaidSeasonPoint.Refresh(NKCRaidSeasonManager.RaidSeason.monthlyPoint, NKCRaidSeasonManager.RaidSeason.recvRewardRaidPoint);
				return;
			}
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x0028ADF8 File Offset: 0x00288FF8
		public void Init()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_csbtnClose != null)
			{
				this.m_csbtnClose.PointerClick.RemoveAllListeners();
				this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (null != this.m_LoopScrollRect)
			{
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
				this.m_LoopScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			}
			if (this.m_tglEventList != null)
			{
				this.m_tglEventList.OnValueChanged.RemoveAllListeners();
				this.m_tglEventList.OnValueChanged.AddListener(new UnityAction<bool>(this.OnEventList));
			}
			if (this.m_tglHelpList != null)
			{
				this.m_tglHelpList.OnValueChanged.RemoveAllListeners();
				this.m_tglHelpList.OnValueChanged.AddListener(new UnityAction<bool>(this.OnHelpList));
			}
			if (this.m_tglJoinList != null)
			{
				this.m_tglJoinList.OnValueChanged.RemoveAllListeners();
				this.m_tglJoinList.OnValueChanged.AddListener(new UnityAction<bool>(this.OnJoinList));
			}
			if (this.m_tglSeasonPoint != null)
			{
				this.m_tglSeasonPoint.OnValueChanged.RemoveAllListeners();
				this.m_tglSeasonPoint.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSeasonPoint));
			}
			if (this.m_evtBG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnClickBG));
				this.m_evtBG.triggers.Add(entry);
			}
			if (this.m_lbRaidAllReceive != null)
			{
				this.m_RaidAllReceiveOriginalColor = this.m_lbRaidAllReceive.color;
			}
			if (this.m_NKCUIRaidSeasonPoint != null)
			{
				this.m_NKCUIRaidSeasonPoint.Init(true);
			}
			NKCUtil.SetBindFunction(this.m_csbtnRaidAllReceive, new UnityAction(this.OnClickReceiveAllRaidResult));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x0028B049 File Offset: 0x00289249
		private void OnClickBG(BaseEventData cBaseEventData)
		{
			base.Close();
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x0028B054 File Offset: 0x00289254
		public void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ACK cNKMPacket_RAID_RESULT_ACCEPT_ACK, int cityID)
		{
			if (this.m_tglEventList.m_bChecked)
			{
				int num = this.m_lstCityHasEvent.FindIndex((NKMWorldMapCityData x) => x.cityID == cityID);
				if (num != -1)
				{
					this.m_lstCityHasEvent.RemoveAt(num);
				}
				int count = this.m_lstCityHasEvent.Count;
				this.m_LoopScrollRect.TotalCount = count;
				this.m_LoopScrollRect.SetIndexPosition(0);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_EVENT;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				return;
			}
			if (this.m_tglJoinList.m_bChecked)
			{
				NKMRaidResultData nkmraidResultData = this.m_RaidResultDataList.Find((NKMRaidResultData x) => x.raidUID == cNKMPacket_RAID_RESULT_ACCEPT_ACK.raidUID);
				if (nkmraidResultData != null)
				{
					this.m_RaidResultDataList.Remove(nkmraidResultData);
					this.m_LoopScrollRect.TotalCount = this.m_RaidResultDataList.Count;
					this.m_LoopScrollRect.SetIndexPosition(0);
					this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_JOIN;
					NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				}
				this.UpdateReceiveAllRewardBtnUI();
			}
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x0028B180 File Offset: 0x00289380
		public void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ALL_ACK sPacket, List<int> lstCity)
		{
			if (this.m_tglEventList.m_bChecked)
			{
				using (List<int>.Enumerator enumerator = lstCity.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int cityID = enumerator.Current;
						int num = this.m_lstCityHasEvent.FindIndex((NKMWorldMapCityData x) => x.cityID == cityID);
						if (num != -1)
						{
							this.m_lstCityHasEvent.RemoveAt(num);
						}
					}
				}
				int count = this.m_lstCityHasEvent.Count;
				this.m_LoopScrollRect.TotalCount = count;
				this.m_LoopScrollRect.SetIndexPosition(0);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_EVENT;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				return;
			}
			if (this.m_tglJoinList.m_bChecked)
			{
				bool flag = false;
				using (List<long>.Enumerator enumerator2 = sPacket.raidUids.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						long raidUID = enumerator2.Current;
						NKMRaidResultData nkmraidResultData = this.m_RaidResultDataList.Find((NKMRaidResultData x) => x.raidUID == raidUID);
						if (nkmraidResultData != null)
						{
							this.m_RaidResultDataList.Remove(nkmraidResultData);
							flag = true;
						}
					}
				}
				if (flag)
				{
					this.m_LoopScrollRect.TotalCount = this.m_RaidResultDataList.Count;
					this.m_LoopScrollRect.SetIndexPosition(0);
					this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_JOIN;
					NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				}
				this.UpdateReceiveAllRewardBtnUI();
			}
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x0028B338 File Offset: 0x00289538
		public void OnRecv(NKMPacket_RAID_COOP_LIST_ACK cNKMPacket_RAID_COOP_LIST_ACK)
		{
			if (cNKMPacket_RAID_COOP_LIST_ACK.coopRaidDataList == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objHelpListReddot, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objHelpListReddot, cNKMPacket_RAID_COOP_LIST_ACK.coopRaidDataList.Count > 0);
			}
			if (this.m_eState == NKCPopupWorldMapEventList.eState.HelpList)
			{
				this.m_CoopRaidDataList = cNKMPacket_RAID_COOP_LIST_ACK.coopRaidDataList;
				this.m_LoopScrollRect.TotalCount = this.m_CoopRaidDataList.Count;
				this.m_LoopScrollRect.SetIndexPosition(0);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_COOP;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
			}
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x0028B3D4 File Offset: 0x002895D4
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x0028B3E2 File Offset: 0x002895E2
		public void OnRecv(NKMPacket_RAID_SEASON_NOT sPacket)
		{
			NKCUtil.SetGameobjectActive(this.m_objSeasonPointReddot, NKCAlarmManager.CheckRaidSeasonRewardNotify(NKCScenManager.CurrentUserData()));
			this.RefreshUI();
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x0028B400 File Offset: 0x00289600
		private int GetCityIDHaveEventDiveOnGoing()
		{
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData != null)
			{
				return NKCScenManager.CurrentUserData().m_WorldmapData.GetCityIDByEventData(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, diveGameData.DiveUid);
			}
			return -1;
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x0028B433 File Offset: 0x00289633
		private bool CheckEventDiveOnGoing()
		{
			return this.GetCityIDHaveEventDiveOnGoing() != -1;
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x0028B444 File Offset: 0x00289644
		private int GetStartedEventDiveCount()
		{
			NKMWorldMapData worldmapData = NKCScenManager.CurrentUserData().m_WorldmapData;
			if (worldmapData == null)
			{
				return 0;
			}
			return worldmapData.GetStartedEventCount(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE);
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x0028B468 File Offset: 0x00289668
		private int GetStartedEventDiveCityID(int index)
		{
			NKMWorldMapData worldmapData = NKCScenManager.CurrentUserData().m_WorldmapData;
			if (worldmapData == null)
			{
				return -1;
			}
			return worldmapData.GetStartedEventCityID(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, index);
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x0028B490 File Offset: 0x00289690
		public void OnRecv(NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK)
		{
			int num = this.m_lstCityHasEvent.FindIndex((NKMWorldMapCityData x) => x.cityID == cNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK.cityID);
			if (num != -1)
			{
				this.m_lstCityHasEvent.RemoveAt(num);
			}
			int num2 = this.m_lstCityHasEvent.Count;
			if (this.CheckEventDiveOnGoing())
			{
				num2++;
			}
			this.m_LoopScrollRect.TotalCount = num2;
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_EVENT;
			NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
		}

		// Token: 0x06007A41 RID: 31297 RVA: 0x0028B52C File Offset: 0x0028972C
		public void OnRecv(NKMPacket_WORLDMAP_EVENT_CANCEL_ACK cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK)
		{
			int num = this.m_lstCityHasEvent.FindIndex((NKMWorldMapCityData x) => x.cityID == cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK.cityID);
			if (num != -1)
			{
				this.m_lstCityHasEvent.RemoveAt(num);
			}
			if (this.m_eState == NKCPopupWorldMapEventList.eState.EventList)
			{
				this.m_LoopScrollRect.TotalCount = this.m_lstCityHasEvent.Count;
				this.m_LoopScrollRect.RefreshCells(false);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_EVENT;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				return;
			}
			if (this.m_eState == NKCPopupWorldMapEventList.eState.JoinList)
			{
				for (int i = 0; i < this.m_RaidResultDataList.Count; i++)
				{
					if (this.m_RaidResultDataList[i].cityID == cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK.cityID)
					{
						this.m_RaidResultDataList.RemoveAt(i);
						break;
					}
				}
				int num2 = this.m_RaidResultDataList.Count;
				num2 += this.GetStartedEventDiveCount();
				this.m_LoopScrollRect.TotalCount = num2;
				this.m_LoopScrollRect.SetIndexPosition(0);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_JOIN;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
			}
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x0028B668 File Offset: 0x00289868
		public void OnRecv(NKMPacket_RAID_RESULT_LIST_ACK cNKMPacket_RAID_RESULT_LIST_ACK)
		{
			this.m_RaidResultDataList = cNKMPacket_RAID_RESULT_LIST_ACK.raidResultDataList;
			int num = this.m_RaidResultDataList.Count;
			num += this.GetStartedEventDiveCount();
			this.m_LoopScrollRect.TotalCount = num;
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_JOIN;
			NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
			this.UpdateReceiveAllRewardBtnUI();
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x0028B6DD File Offset: 0x002898DD
		private void OnClickReceiveAllRaidResult()
		{
			if (!this.m_bCanReceiveAllReward)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_RAID_RESULT_ACCEPT_ALL_REQ();
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x0028B6F0 File Offset: 0x002898F0
		private void UpdateReceiveAllRewardBtnUI()
		{
			this.m_bCanReceiveAllReward = false;
			using (List<NKMRaidResultData>.Enumerator enumerator = this.m_RaidResultDataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsOnGoing())
					{
						this.m_bCanReceiveAllReward = true;
						break;
					}
				}
			}
			if (!this.m_bCanReceiveAllReward)
			{
				NKCUtil.SetImageSprite(this.m_imgRaidAllReceive, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.m_lbRaidAllReceive, NKCUtil.GetColor("#212122"));
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgRaidAllReceive, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
			NKCUtil.SetLabelTextColor(this.m_lbRaidAllReceive, this.m_RaidAllReceiveOriginalColor);
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x0028B7A8 File Offset: 0x002899A8
		private RectTransform GetSlot(int index)
		{
			NKCPopupWorldMapEventListSlot nkcpopupWorldMapEventListSlot = null;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupWorldMapEventListSlot = this.m_stkSlot.Pop();
			}
			if (nkcpopupWorldMapEventListSlot == null)
			{
				nkcpopupWorldMapEventListSlot = UnityEngine.Object.Instantiate<NKCPopupWorldMapEventListSlot>(this.m_pfbSlot);
			}
			if (nkcpopupWorldMapEventListSlot != null)
			{
				nkcpopupWorldMapEventListSlot.transform.localPosition = Vector3.zero;
				nkcpopupWorldMapEventListSlot.transform.localScale = Vector3.one;
				nkcpopupWorldMapEventListSlot.transform.SetParent(this.m_LoopScrollRect.content);
				return nkcpopupWorldMapEventListSlot.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x0028B830 File Offset: 0x00289A30
		private void ReturnSlot(Transform tr)
		{
			NKCPopupWorldMapEventListSlot component = tr.GetComponent<NKCPopupWorldMapEventListSlot>();
			if (component != null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				this.m_stkSlot.Push(component);
			}
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x0028B860 File Offset: 0x00289A60
		private void ProvideSlotData(Transform transform, int idx)
		{
			NKCPopupWorldMapEventListSlot component = transform.GetComponent<NKCPopupWorldMapEventListSlot>();
			if (component == null)
			{
				return;
			}
			switch (this.m_eState)
			{
			case NKCPopupWorldMapEventList.eState.EventList:
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_NKCUIRaidSeasonPoint, false);
				NKCUtil.SetGameobjectActive(component, true);
				component.SetDataForMyEventList(this.m_lstCityHasEvent[idx].worldMapEventGroup, this.m_lstCityHasEvent[idx], new NKCPopupWorldMapEventListSlot.OnMove(this.OnSlotMove));
				return;
			case NKCPopupWorldMapEventList.eState.HelpList:
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_NKCUIRaidSeasonPoint, false);
				NKCUtil.SetGameobjectActive(component, true);
				component.SetDataForHelpList(this.m_CoopRaidDataList[idx]);
				return;
			case NKCPopupWorldMapEventList.eState.JoinList:
			{
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_NKCUIRaidSeasonPoint, false);
				NKCUtil.SetGameobjectActive(component, true);
				int startedEventDiveCount = this.GetStartedEventDiveCount();
				if (startedEventDiveCount > 0)
				{
					if (idx >= startedEventDiveCount)
					{
						component.SetDataForJoinList(this.m_RaidResultDataList[idx - startedEventDiveCount]);
						return;
					}
					int startedEventDiveCityID = this.GetStartedEventDiveCityID(idx);
					NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(startedEventDiveCityID);
					if (cityData != null)
					{
						component.SetDataForMyEventList(cityData.worldMapEventGroup, cityData, new NKCPopupWorldMapEventListSlot.OnMove(this.OnSlotMove));
						return;
					}
				}
				else
				{
					component.SetDataForJoinList(this.m_RaidResultDataList[idx]);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x0028B9A8 File Offset: 0x00289BA8
		public void Open(NKMWorldMapData worldmapData, NKCPopupWorldMapEventList.OnSelectEvent onSelectEvent, bool bHelpListReddot)
		{
			NKCUtil.SetGameobjectActive(this.m_objHelpListReddot, bHelpListReddot);
			NKCUtil.SetGameobjectActive(this.m_objSeasonPointReddot, NKCAlarmManager.CheckRaidSeasonRewardNotify(NKCScenManager.CurrentUserData()));
			base.gameObject.SetActive(true);
			this.dOnSelectEvent = onSelectEvent;
			this.RefreshEventDataList(worldmapData);
			this.SetState(NKCPopupWorldMapEventList.eState.EventList);
			NKCUtil.SetGameobjectActive(this.m_objRaidSeasonInfo, false);
			NKMRaidSeasonTemplet seasonTemplet = NKCRaidSeasonManager.GetNowSeasonTemplet();
			if (seasonTemplet != null)
			{
				NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMTempletContainer<NKMWorldMapEventTemplet>.Values.ToList<NKMWorldMapEventTemplet>().Find((NKMWorldMapEventTemplet x) => x.raidBossId == seasonTemplet.RaidBossId);
				if (nkmworldMapEventTemplet != null)
				{
					NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmworldMapEventTemplet.stageID);
					if (nkmraidTemplet != null)
					{
						NKCUtil.SetGameobjectActive(this.m_objRaidSeasonInfo, true);
						Text lbRaidBossName = this.m_lbRaidBossName;
						NKMDungeonTempletBase dungeonTempletBase = nkmraidTemplet.DungeonTempletBase;
						NKCUtil.SetLabelText(lbRaidBossName, (dungeonTempletBase != null) ? dungeonTempletBase.GetDungeonName() : null);
						NKCUtil.SetLabelText(this.m_lbRaidSeasonTimeLeft, NKCUtilString.GetRemainTimeStringOneParam(seasonTemplet.IntervalTemplet.GetEndDateUtc()));
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objRaidSeasonInfo, false);
			}
			this.m_bCanReceiveAllReward = false;
			base.UIOpened(true);
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x0028BAB0 File Offset: 0x00289CB0
		private void SetState(NKCPopupWorldMapEventList.eState state)
		{
			NKMRaidSeasonTemplet nowSeasonTemplet = NKCRaidSeasonManager.GetNowSeasonTemplet();
			if (nowSeasonTemplet == null)
			{
				if (this.m_eState == NKCPopupWorldMapEventList.eState.SeasonPoint)
				{
					this.m_tglEventList.Select(true, true, true);
					this.m_eState = NKCPopupWorldMapEventList.eState.EventList;
				}
				this.m_tglSeasonPoint.Lock(false);
			}
			else
			{
				this.m_tglSeasonPoint.UnLock(false);
			}
			this.m_eState = state;
			switch (this.m_eState)
			{
			case NKCPopupWorldMapEventList.eState.EventList:
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_objSeasonPoint, false);
				this.m_LoopScrollRect.TotalCount = this.m_lstCityHasEvent.Count;
				this.m_LoopScrollRect.SetIndexPosition(0);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_EVENT;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				this.m_tglEventList.Select(true, true, false);
				break;
			case NKCPopupWorldMapEventList.eState.HelpList:
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_objSeasonPoint, false);
				this.m_LoopScrollRect.TotalCount = 0;
				this.m_LoopScrollRect.RefreshCells(false);
				this.m_tglHelpList.Select(true, true, false);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_COOP;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				NKCPacketSender.Send_NKMPacket_RAID_COOP_LIST_REQ();
				break;
			case NKCPopupWorldMapEventList.eState.JoinList:
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_objSeasonPoint, false);
				this.m_LoopScrollRect.TotalCount = 0;
				this.m_LoopScrollRect.RefreshCells(false);
				this.m_tglJoinList.Select(true, true, false);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_WORLDMAP_NO_EXIST_JOIN;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				NKCPacketSender.Send_NKMPacket_RAID_RESULT_LIST_REQ();
				break;
			case NKCPopupWorldMapEventList.eState.SeasonPoint:
				NKCUtil.SetGameobjectActive(this.m_objSlotList, false);
				NKCUtil.SetGameobjectActive(this.m_objSeasonPoint, true);
				if (nowSeasonTemplet != null)
				{
					this.m_lstSeasonPointData = new List<NKCUISeasonPointSlot.SeasonPointSlotData>();
					this.m_lstSeasonPointData.Add(NKCUISeasonPointSlot.SeasonPointSlotData.MakeEmptyData());
					foreach (NKMRaidSeasonRewardTemplet nkmraidSeasonRewardTemplet in NKMRaidSeasonRewardTemplet.Values)
					{
						if (nkmraidSeasonRewardTemplet.RewardBoardId == nowSeasonTemplet.RaidBoardId)
						{
							this.m_lstSeasonPointData.Add(NKCUISeasonPointSlot.SeasonPointSlotData.MakeSeasonPointSlotData(nkmraidSeasonRewardTemplet));
						}
					}
					NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
					this.m_NKCUIRaidSeasonPoint.Open(this.m_lstSeasonPointData, NKCStringTable.GetString("SI_PF_WORLD_MAP_RENEWAL_EVENT_POPUP_REWARD_TITLE", false), NKCRaidSeasonManager.RaidSeason.monthlyPoint, NKCRaidSeasonManager.RaidSeason.recvRewardRaidPoint, nowSeasonTemplet.IntervalTemplet, new NKCUISeasonPointSlot.OnClickSlot(this.OnClickSeasonPointReward));
				}
				else
				{
					this.SetState(NKCPopupWorldMapEventList.eState.EventList);
				}
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_objRaidAllReceive, this.m_eState == NKCPopupWorldMapEventList.eState.JoinList);
			if (this.m_objRaidAllReceive != null && this.m_objRaidAllReceive.activeSelf)
			{
				RectTransform component = this.m_objRaidAllReceive.GetComponent<RectTransform>();
				RectTransform component2 = this.m_LoopScrollRect.gameObject.GetComponent<RectTransform>();
				component2.offsetMin = new Vector2(component2.offsetMin.x, component.GetHeight());
				return;
			}
			RectTransform component3 = this.m_LoopScrollRect.gameObject.GetComponent<RectTransform>();
			component3.offsetMin = new Vector2(component3.offsetMin.x, 0f);
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x0028BE00 File Offset: 0x0028A000
		private void RefreshEventDataList(NKMWorldMapData worldMapData)
		{
			this.m_lstCityHasEvent.Clear();
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in worldMapData.worldMapCityDataMap)
			{
				NKMWorldMapCityData value = keyValuePair.Value;
				if (value.worldMapEventGroup != null && value.worldMapEventGroup.worldmapEventID != 0)
				{
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(value.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
					{
						NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(value.worldMapEventGroup.eventUid);
						if (nkmraidDetailData != null && !nkmraidDetailData.isNew)
						{
							continue;
						}
					}
					else if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE && value.worldMapEventGroup.eventUid > 0L)
					{
						continue;
					}
					this.m_lstCityHasEvent.Add(value);
				}
			}
		}

		// Token: 0x06007A4B RID: 31307 RVA: 0x0028BEE4 File Offset: 0x0028A0E4
		private void OnEventList(bool value)
		{
			if (value)
			{
				this.SetState(NKCPopupWorldMapEventList.eState.EventList);
			}
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x0028BEF0 File Offset: 0x0028A0F0
		private void OnHelpList(bool value)
		{
			if (value)
			{
				this.SetState(NKCPopupWorldMapEventList.eState.HelpList);
			}
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x0028BEFC File Offset: 0x0028A0FC
		private void OnJoinList(bool value)
		{
			if (value)
			{
				this.SetState(NKCPopupWorldMapEventList.eState.JoinList);
			}
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x0028BF08 File Offset: 0x0028A108
		private void OnSeasonPoint(bool bValue)
		{
			if (bValue)
			{
				this.SetState(NKCPopupWorldMapEventList.eState.SeasonPoint);
			}
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x0028BF14 File Offset: 0x0028A114
		private void OnSlotMove(int cityID, int eventID, long eventUID)
		{
			NKCPopupWorldMapEventList.OnSelectEvent onSelectEvent = this.dOnSelectEvent;
			if (onSelectEvent == null)
			{
				return;
			}
			onSelectEvent(cityID, eventID, eventUID);
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x0028BF2C File Offset: 0x0028A12C
		private void OnClickSeasonPointReward(NKCUISeasonPointSlot.SeasonPointSlotData slotData)
		{
			if (slotData.SlotPoint <= NKCRaidSeasonManager.RaidSeason.recvRewardRaidPoint || slotData.SlotPoint > NKCRaidSeasonManager.RaidSeason.monthlyPoint)
			{
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, NKCUISlot.SlotData.MakeRewardTypeData(slotData.RewardType, slotData.RewardID, slotData.RewardCount, 0), null, false, false, true);
				return;
			}
			int num = 0;
			foreach (NKMRaidSeasonRewardTemplet nkmraidSeasonRewardTemplet in NKMRaidSeasonRewardTemplet.Values)
			{
				if (nkmraidSeasonRewardTemplet.RewardBoardId == slotData.ID && nkmraidSeasonRewardTemplet.RaidPoint >= num && nkmraidSeasonRewardTemplet.RaidPoint > NKCRaidSeasonManager.RaidSeason.recvRewardRaidPoint && nkmraidSeasonRewardTemplet.RaidPoint >= num && nkmraidSeasonRewardTemplet.RaidPoint <= NKCRaidSeasonManager.RaidSeason.monthlyPoint)
				{
					num = nkmraidSeasonRewardTemplet.RaidPoint;
				}
			}
			NKCPacketSender.Send_NKMPacket_RAID_POINT_REWARD_REQ(num);
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x0028C010 File Offset: 0x0028A210
		public void RefreshUI()
		{
			this.SetState(this.m_eState);
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x0028C020 File Offset: 0x0028A220
		private void Update()
		{
			if (this.m_eState == NKCPopupWorldMapEventList.eState.SeasonPoint)
			{
				this.bDeltaTime += Time.deltaTime;
				if (this.bDeltaTime > 1f)
				{
					this.bDeltaTime -= 1f;
					if (NKCRaidSeasonManager.GetNowSeasonTemplet() == null)
					{
						this.RefreshUI();
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WORLD_MAP_RAID_SEASON_END, null, "");
					}
				}
			}
		}

		// Token: 0x040066F2 RID: 26354
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x040066F3 RID: 26355
		public const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RENEWAL_EVENT_POPUP";

		// Token: 0x040066F4 RID: 26356
		public NKCPopupWorldMapEventListSlot m_pfbSlot;

		// Token: 0x040066F5 RID: 26357
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x040066F6 RID: 26358
		public EventTrigger m_evtBG;

		// Token: 0x040066F7 RID: 26359
		[Header("상단")]
		public GameObject m_objRaidSeasonInfo;

		// Token: 0x040066F8 RID: 26360
		public Text m_lbRaidBossName;

		// Token: 0x040066F9 RID: 26361
		public Text m_lbRaidSeasonTimeLeft;

		// Token: 0x040066FA RID: 26362
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040066FB RID: 26363
		[Header("좌측 탭")]
		public NKCUIComToggle m_tglEventList;

		// Token: 0x040066FC RID: 26364
		public NKCUIComToggle m_tglHelpList;

		// Token: 0x040066FD RID: 26365
		public GameObject m_objHelpListReddot;

		// Token: 0x040066FE RID: 26366
		public NKCUIComToggle m_tglJoinList;

		// Token: 0x040066FF RID: 26367
		public NKCUIComToggle m_tglSeasonPoint;

		// Token: 0x04006700 RID: 26368
		public GameObject m_objSeasonPointReddot;

		// Token: 0x04006701 RID: 26369
		[Header("우측 정보")]
		public Image m_imgRaidAllReceive;

		// Token: 0x04006702 RID: 26370
		public Text m_lbRaidAllReceive;

		// Token: 0x04006703 RID: 26371
		public GameObject m_objRaidAllReceive;

		// Token: 0x04006704 RID: 26372
		public NKCUIComStateButton m_csbtnRaidAllReceive;

		// Token: 0x04006705 RID: 26373
		public GameObject m_objEmpty;

		// Token: 0x04006706 RID: 26374
		public Text m_lbEmptyMessage;

		// Token: 0x04006707 RID: 26375
		public GameObject m_objSlotList;

		// Token: 0x04006708 RID: 26376
		public GameObject m_objSeasonPoint;

		// Token: 0x04006709 RID: 26377
		public NKCUISeasonPoint m_NKCUIRaidSeasonPoint;

		// Token: 0x0400670A RID: 26378
		private List<NKMWorldMapCityData> m_lstCityHasEvent = new List<NKMWorldMapCityData>();

		// Token: 0x0400670B RID: 26379
		private NKCPopupWorldMapEventList.eState m_eState;

		// Token: 0x0400670C RID: 26380
		private NKCPopupWorldMapEventList.OnSelectEvent dOnSelectEvent;

		// Token: 0x0400670D RID: 26381
		private List<NKMCoopRaidData> m_CoopRaidDataList = new List<NKMCoopRaidData>();

		// Token: 0x0400670E RID: 26382
		private List<NKMRaidResultData> m_RaidResultDataList = new List<NKMRaidResultData>();

		// Token: 0x0400670F RID: 26383
		private Stack<NKCPopupWorldMapEventListSlot> m_stkSlot = new Stack<NKCPopupWorldMapEventListSlot>();

		// Token: 0x04006710 RID: 26384
		private Color m_RaidAllReceiveOriginalColor;

		// Token: 0x04006711 RID: 26385
		private List<NKCUISeasonPointSlot.SeasonPointSlotData> m_lstSeasonPointData = new List<NKCUISeasonPointSlot.SeasonPointSlotData>();

		// Token: 0x04006712 RID: 26386
		private bool m_bCanReceiveAllReward;

		// Token: 0x04006713 RID: 26387
		private float bDeltaTime;

		// Token: 0x02001815 RID: 6165
		public enum eState
		{
			// Token: 0x0400A808 RID: 43016
			EventList,
			// Token: 0x0400A809 RID: 43017
			HelpList,
			// Token: 0x0400A80A RID: 43018
			JoinList,
			// Token: 0x0400A80B RID: 43019
			SeasonPoint
		}

		// Token: 0x02001816 RID: 6166
		// (Invoke) Token: 0x0600B511 RID: 46353
		public delegate void OnSelectEvent(int cityID, int eventID, long eventUID);
	}
}
