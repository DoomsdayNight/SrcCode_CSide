using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BE1 RID: 3041
	public class NKCUIEventSubUIMission : NKCUIEventSubUIBase
	{
		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06008D11 RID: 36113 RVA: 0x002FF81C File Offset: 0x002FDA1C
		private RectTransform SlotPool
		{
			get
			{
				if (this.m_rtSlotPool == null)
				{
					GameObject gameObject = new GameObject("Slotpool", new Type[]
					{
						typeof(RectTransform)
					});
					gameObject.transform.SetParent(base.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					this.m_rtSlotPool = gameObject.GetComponent<RectTransform>();
					gameObject.SetActive(false);
				}
				return this.m_rtSlotPool;
			}
		}

		// Token: 0x06008D12 RID: 36114 RVA: 0x002FF8A0 File Offset: 0x002FDAA0
		public override void Init()
		{
			base.Init();
			RectTransform component = base.GetComponent<RectTransform>();
			if (component != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(component);
			}
			this.m_srMission.dOnGetObject += this.GetMissionSlot;
			this.m_srMission.dOnReturnObject += this.ReturnMissionSlot;
			this.m_srMission.dOnProvideData += this.ProvideData;
			this.m_srMission.ContentConstraintCount = 1;
			this.m_srMission.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_srMission, null);
			NKCUtil.SetGameobjectActive(this.m_csbtnCompleteAll, true);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventShop, new UnityAction(this.OnMovetoShop));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventShortcut, new UnityAction(base.OnMoveShortcut));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAll, new UnityAction(this.OnCompleteAll));
			NKCUIMissionAchieveSlot specialMissionSlot = this.m_SpecialMissionSlot;
			if (specialMissionSlot == null)
			{
				return;
			}
			specialMissionSlot.Init();
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x002FF993 File Offset: 0x002FDB93
		public override void UnHide()
		{
			base.UnHide();
			this.Refresh();
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x002FF9A4 File Offset: 0x002FDBA4
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_tabTemplet = tabTemplet;
			if (this.m_tabTemplet == null)
			{
				return;
			}
			this.m_EventMissionTemplet = NKCEventMissionTemplet.Find(this.m_tabTemplet.Key);
			if (this.m_EventMissionTemplet == null)
			{
				return;
			}
			base.SetDateLimit();
			NKCUtil.SetGameobjectActive(this.m_csbtnEventShop, this.m_EventMissionTemplet.m_ShortcutType > NKM_SHORTCUT_TYPE.SHORTCUT_NONE);
			this.PrepareMissionTab(this.m_EventMissionTemplet);
			this.Refresh();
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x002FFA11 File Offset: 0x002FDC11
		public override void Refresh()
		{
			this.RefreshMissionTab();
			this.SelectTab(this.m_SelectedTabID);
			if (this.m_EventMissionTemplet != null)
			{
				this.SetSpecialMissionData(this.m_EventMissionTemplet.m_SpecialMissionTab);
			}
		}

		// Token: 0x06008D16 RID: 36118 RVA: 0x002FFA40 File Offset: 0x002FDC40
		private void SelectTab(int tabID)
		{
			NKCUIEventSubUIMissionTab nkcuieventSubUIMissionTab;
			if (this.m_dicTab.TryGetValue(tabID, out nkcuieventSubUIMissionTab))
			{
				nkcuieventSubUIMissionTab.m_tglButton.Select(true, true, false);
			}
			this.m_SelectedTabID = tabID;
			this.BuildAllMissionTempletListByTab(tabID);
			this.m_srMission.TotalCount = this.m_lstCurrentList.Count;
			this.m_srMission.StopMovement();
			this.m_srMission.SetIndexPosition(0);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserMissionData missionData = NKCScenManager.GetScenManager().GetMyUserData().m_MissionData;
			if (missionData == null)
			{
				return;
			}
			if (!missionData.CheckCompletableMission(myUserData, tabID, false))
			{
				NKCUIComStateButton csbtnCompleteAll = this.m_csbtnCompleteAll;
				if (csbtnCompleteAll == null)
				{
					return;
				}
				csbtnCompleteAll.Lock(false);
				return;
			}
			else
			{
				NKCUIComStateButton csbtnCompleteAll2 = this.m_csbtnCompleteAll;
				if (csbtnCompleteAll2 == null)
				{
					return;
				}
				csbtnCompleteAll2.UnLock(false);
				return;
			}
		}

		// Token: 0x06008D17 RID: 36119 RVA: 0x002FFAF8 File Offset: 0x002FDCF8
		private void OnMovetoShop()
		{
			NKCContentManager.MoveToShortCut(this.m_EventMissionTemplet.m_ShortcutType, this.m_EventMissionTemplet.m_ShortcutParam, false);
		}

		// Token: 0x06008D18 RID: 36120 RVA: 0x002FFB16 File Offset: 0x002FDD16
		private void OnCompleteAll()
		{
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(this.m_SelectedTabID);
		}

		// Token: 0x06008D19 RID: 36121 RVA: 0x002FFB24 File Offset: 0x002FDD24
		public RectTransform GetMissionSlot(int index)
		{
			RectTransform parent = (this.m_srMission != null) ? this.m_srMission.content : null;
			NKCUIMissionAchieveSlot nkcuimissionAchieveSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuimissionAchieveSlot = this.m_stkSlot.Pop();
				nkcuimissionAchieveSlot.transform.SetParent(parent);
			}
			else
			{
				nkcuimissionAchieveSlot = UnityEngine.Object.Instantiate<NKCUIMissionAchieveSlot>(this.m_pfbMissionSlot, parent);
				nkcuimissionAchieveSlot.Init();
			}
			return nkcuimissionAchieveSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008D1A RID: 36122 RVA: 0x002FFB90 File Offset: 0x002FDD90
		public void ReturnMissionSlot(Transform tr)
		{
			tr.gameObject.SetActive(false);
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component != null)
			{
				this.m_stkSlot.Push(component);
			}
			tr.SetParent(this.SlotPool);
		}

		// Token: 0x06008D1B RID: 36123 RVA: 0x002FFBD4 File Offset: 0x002FDDD4
		public void ProvideData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component != null)
			{
				NKMMissionTemplet cNKMMissionTemplet = this.m_lstCurrentList[index];
				component.SetData(cNKMMissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
			}
		}

		// Token: 0x06008D1C RID: 36124 RVA: 0x002FFC20 File Offset: 0x002FDE20
		public void OnClickMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.Refresh), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x002FFC94 File Offset: 0x002FDE94
		public void OnClickComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (this.m_tabTemplet.m_EventType == NKM_EVENT_TYPE.ONTIME)
			{
				NKMMissionData missionData = NKMMissionManager.GetMissionData(nkmmissionTemplet);
				if (missionData != null && NKMMissionManager.CheckCanReset(nkmmissionTemplet.m_ResetInterval, missionData))
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.Refresh), NKCUtilString.GET_STRING_CONFIRM);
					return;
				}
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.Refresh), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x06008D1E RID: 36126 RVA: 0x002FFD64 File Offset: 0x002FDF64
		private void BuildAllMissionTempletListByTab(int tabID)
		{
			if (!this.m_dicNKMMissionTemplet.ContainsKey(tabID))
			{
				this.m_dicNKMMissionTemplet.Add(tabID, NKMMissionManager.GetMissionTempletListByType(tabID));
			}
			this.m_lstCurrentList = this.m_dicNKMMissionTemplet[tabID];
			if (this.m_tabTemplet.m_EventType == NKM_EVENT_TYPE.MISSION)
			{
				this.m_lstCurrentList.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
			}
		}

		// Token: 0x06008D1F RID: 36127 RVA: 0x002FFDC8 File Offset: 0x002FDFC8
		private void BuildMissionTempletListByTab(int tabID)
		{
			this.m_lstCurrentList.Clear();
			if (!this.m_dicNKMMissionTemplet.ContainsKey(tabID))
			{
				this.m_dicNKMMissionTemplet.Add(tabID, NKMMissionManager.GetMissionTempletListByType(tabID));
			}
			List<NKMMissionTemplet> list = this.m_dicNKMMissionTemplet[tabID];
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (NKMMissionManager.GetMissionTabTemplet(tabID) == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				NKMMissionTemplet nkmmissionTemplet = list[i];
				if (nkmmissionTemplet != null)
				{
					if (nkmmissionTemplet.m_MissionRequire != 0)
					{
						NKMMissionData nkmmissionData = myUserData.m_MissionData.GetMissionData(nkmmissionTemplet);
						if (nkmmissionData == null)
						{
							goto IL_F4;
						}
						if (nkmmissionData.mission_id == nkmmissionTemplet.m_MissionID)
						{
							this.m_lstCurrentList.Add(nkmmissionTemplet);
							goto IL_F4;
						}
						nkmmissionData = myUserData.m_MissionData.GetMissionDataByMissionId(nkmmissionTemplet.m_MissionRequire);
						if (nkmmissionData == null)
						{
							goto IL_F4;
						}
						if (nkmmissionData.isComplete && nkmmissionData.mission_id == nkmmissionTemplet.m_MissionRequire)
						{
							this.m_lstCurrentList.Add(nkmmissionTemplet);
							goto IL_F4;
						}
						if (nkmmissionData.mission_id <= nkmmissionTemplet.m_MissionRequire)
						{
							goto IL_F4;
						}
					}
					this.m_lstCurrentList.Add(nkmmissionTemplet);
				}
				IL_F4:;
			}
			this.m_lstCurrentList.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
		}

		// Token: 0x06008D20 RID: 36128 RVA: 0x002FFEF0 File Offset: 0x002FE0F0
		private void PrepareMissionTab(NKCEventMissionTemplet eventMissionTemplet)
		{
			if (eventMissionTemplet.m_lstMissionTab.Count == 1)
			{
				NKCUtil.SetGameobjectActive(this.m_srCategory, false);
				this.m_SelectedTabID = eventMissionTemplet.m_lstMissionTab[0];
				return;
			}
			if (this.m_srCategory == null || this.m_pfbCategory == null)
			{
				Debug.LogError(string.Format("Event {0} require Category, but prefab don't have it", eventMissionTemplet.m_EventID));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_srCategory, true);
			List<int> list = new List<int>(eventMissionTemplet.m_lstMissionTab);
			list.Sort();
			foreach (int num in list)
			{
				NKCUIEventSubUIMissionTab nkcuieventSubUIMissionTab;
				if (!this.m_dicTab.TryGetValue(num, out nkcuieventSubUIMissionTab))
				{
					nkcuieventSubUIMissionTab = UnityEngine.Object.Instantiate<NKCUIEventSubUIMissionTab>(this.m_pfbCategory, this.m_srCategory.content);
					this.m_dicTab.Add(num, nkcuieventSubUIMissionTab);
				}
				nkcuieventSubUIMissionTab.transform.SetAsLastSibling();
				NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(num);
				if (missionTabTemplet != null)
				{
					nkcuieventSubUIMissionTab.SetData(missionTabTemplet, this.m_tgCategory, new Action<int, bool>(this.OnSelectTab));
				}
			}
			if (this.m_SelectedTabID == 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					foreach (int num2 in list)
					{
						NKMMissionManager.GetMissionTabTemplet(num2);
						NKCUIEventSubUIMissionTab nkcuieventSubUIMissionTab2;
						if (this.m_dicTab.TryGetValue(num2, out nkcuieventSubUIMissionTab2) && !nkcuieventSubUIMissionTab2.Locked && !nkcuieventSubUIMissionTab2.Completed)
						{
							if (nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, num2, false))
							{
								this.m_SelectedTabID = num2;
								break;
							}
							if (this.m_SelectedTabID == 0)
							{
								this.m_SelectedTabID = num2;
							}
						}
					}
				}
				if (this.m_SelectedTabID == 0)
				{
					this.m_SelectedTabID = eventMissionTemplet.m_lstMissionTab[0];
				}
			}
		}

		// Token: 0x06008D21 RID: 36129 RVA: 0x003000E0 File Offset: 0x002FE2E0
		private void RefreshMissionTab()
		{
			foreach (KeyValuePair<int, NKCUIEventSubUIMissionTab> keyValuePair in this.m_dicTab)
			{
				NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(keyValuePair.Key);
				keyValuePair.Value.SetData(missionTabTemplet, this.m_tgCategory, new Action<int, bool>(this.OnSelectTab));
			}
		}

		// Token: 0x06008D22 RID: 36130 RVA: 0x00300158 File Offset: 0x002FE358
		private void SetSpecialMissionData(int specialMissionTabID)
		{
			if (this.m_SpecialMissionSlot == null)
			{
				return;
			}
			if (specialMissionTabID <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_SpecialMissionSlot, false);
				return;
			}
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(specialMissionTabID);
			NKMMissionTemplet nkmmissionTemplet = null;
			NKMMissionTemplet nkmmissionTemplet2 = null;
			NKMMissionTemplet nkmmissionTemplet3 = null;
			bool flag = false;
			foreach (NKMMissionTemplet nkmmissionTemplet4 in missionTempletListByType)
			{
				switch (NKMMissionManager.GetMissionStateData(nkmmissionTemplet4).state)
				{
				case NKMMissionManager.MissionState.CAN_COMPLETE:
				case NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE:
				case NKMMissionManager.MissionState.ONGOING:
					nkmmissionTemplet = nkmmissionTemplet4;
					flag = true;
					break;
				case NKMMissionManager.MissionState.REPEAT_COMPLETED:
				case NKMMissionManager.MissionState.COMPLETED:
					nkmmissionTemplet2 = nkmmissionTemplet4;
					break;
				case NKMMissionManager.MissionState.LOCKED:
					if (nkmmissionTemplet3 == null)
					{
						nkmmissionTemplet3 = nkmmissionTemplet4;
					}
					break;
				}
				if (flag)
				{
					break;
				}
			}
			if (nkmmissionTemplet == null)
			{
				if (nkmmissionTemplet2 != null)
				{
					nkmmissionTemplet = nkmmissionTemplet2;
				}
				else
				{
					if (nkmmissionTemplet3 == null)
					{
						NKCUtil.SetGameobjectActive(this.m_SpecialMissionSlot, false);
						return;
					}
					nkmmissionTemplet = nkmmissionTemplet3;
				}
			}
			this.m_NextResetTime = DateTime.MaxValue;
			if (this.m_objResetRemainTime != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objResetRemainTime, this.m_tabTemplet.m_EventType == NKM_EVENT_TYPE.ONTIME && nkmmissionTemplet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.NONE);
				if (this.m_objResetRemainTime.activeSelf)
				{
					this.m_NextResetTime = NKMTime.GetNextResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), nkmmissionTemplet.m_ResetInterval);
					this.SetRemainTime();
				}
			}
			this.m_SpecialMissionSlot.SetData(nkmmissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
			NKCUtil.SetGameobjectActive(this.m_SpecialMissionSlot, true);
			if (this.m_lbSpecialMissionRewardName != null && nkmmissionTemplet != null && nkmmissionTemplet.m_MissionReward.Count > 0)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(nkmmissionTemplet.m_MissionReward[0].reward_id);
				if (nkmitemMiscTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbSpecialMissionRewardName, nkmitemMiscTemplet.GetItemName());
				}
			}
		}

		// Token: 0x06008D23 RID: 36131 RVA: 0x00300324 File Offset: 0x002FE524
		private void SetRemainTime()
		{
			if (!NKCSynchronizedTime.IsFinished(this.m_NextResetTime))
			{
				NKCUtil.SetLabelText(this.m_lbResetRemainTime, NKCUtilString.GetRemainTimeStringEx(this.m_NextResetTime));
				return;
			}
			this.Refresh();
		}

		// Token: 0x06008D24 RID: 36132 RVA: 0x00300350 File Offset: 0x002FE550
		private void OnSelectTab(int tabID, bool bSet)
		{
			if (bSet)
			{
				this.SelectTab(tabID);
			}
		}

		// Token: 0x06008D25 RID: 36133 RVA: 0x0030035C File Offset: 0x002FE55C
		private void Update()
		{
			if (this.m_objResetRemainTime == null)
			{
				return;
			}
			if (!this.m_objResetRemainTime.activeSelf)
			{
				return;
			}
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				this.SetRemainTime();
			}
		}

		// Token: 0x040079E5 RID: 31205
		[Header("일괄 완료 버튼")]
		public NKCUIComStateButton m_csbtnCompleteAll;

		// Token: 0x040079E6 RID: 31206
		[Header("이벤트 상점 이동 버튼")]
		public NKCUIComStateButton m_csbtnEventShop;

		// Token: 0x040079E7 RID: 31207
		[Header("카데고리/탭 프리팹")]
		public NKCUIEventSubUIMissionTab m_pfbCategory;

		// Token: 0x040079E8 RID: 31208
		[Header("카데고리 버튼 스크롤렉트")]
		public ScrollRect m_srCategory;

		// Token: 0x040079E9 RID: 31209
		[Header("카데고리 버튼 토글 그룹")]
		public NKCUIComToggleGroup m_tgCategory;

		// Token: 0x040079EA RID: 31210
		[Header("미션 슬롯 프리팹")]
		public NKCUIMissionAchieveSlot m_pfbMissionSlot;

		// Token: 0x040079EB RID: 31211
		[Header("미션 스크롤렉트")]
		public LoopScrollRect m_srMission;

		// Token: 0x040079EC RID: 31212
		[Header("스페셜 미션 슬롯(프리팹 아님)")]
		public NKCUIMissionAchieveSlot m_SpecialMissionSlot;

		// Token: 0x040079ED RID: 31213
		[Header("스페셜 미션 첫번째 보상 이름(필요한 경우)")]
		public Text m_lbSpecialMissionRewardName;

		// Token: 0x040079EE RID: 31214
		[Header("초기화가 되는 경우 남은시간")]
		public GameObject m_objResetRemainTime;

		// Token: 0x040079EF RID: 31215
		public Text m_lbResetRemainTime;

		// Token: 0x040079F0 RID: 31216
		private NKCEventMissionTemplet m_EventMissionTemplet;

		// Token: 0x040079F1 RID: 31217
		private DateTime m_NextResetTime;

		// Token: 0x040079F2 RID: 31218
		private RectTransform m_rtSlotPool;

		// Token: 0x040079F3 RID: 31219
		private int m_SelectedTabID;

		// Token: 0x040079F4 RID: 31220
		private Dictionary<int, NKCUIEventSubUIMissionTab> m_dicTab = new Dictionary<int, NKCUIEventSubUIMissionTab>();

		// Token: 0x040079F5 RID: 31221
		private List<NKMMissionTemplet> m_lstCurrentList = new List<NKMMissionTemplet>();

		// Token: 0x040079F6 RID: 31222
		private Dictionary<int, List<NKMMissionTemplet>> m_dicNKMMissionTemplet = new Dictionary<int, List<NKMMissionTemplet>>();

		// Token: 0x040079F7 RID: 31223
		private Stack<NKCUIMissionAchieveSlot> m_stkSlot = new Stack<NKCUIMissionAchieveSlot>();

		// Token: 0x040079F8 RID: 31224
		private float m_fDeltaTime;
	}
}
