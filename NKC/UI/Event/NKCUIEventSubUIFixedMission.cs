using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Templet;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BDE RID: 3038
	public class NKCUIEventSubUIFixedMission : NKCUIEventSubUIBase
	{
		// Token: 0x06008CF7 RID: 36087 RVA: 0x002FF054 File Offset: 0x002FD254
		public override void Init()
		{
			base.Init();
			RectTransform component = base.GetComponent<RectTransform>();
			if (component != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(component);
			}
			NKCUtil.SetScrollHotKey(this.m_srMission, null);
			NKCUtil.SetGameobjectActive(this.m_csbtnCompleteAll, true);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventShop, new UnityAction(this.OnMovetoShop));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventShortcut, new UnityAction(base.OnMoveShortcut));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAll, new UnityAction(this.OnCompleteAll));
			for (int i = 0; i < this.m_lstMissionSlot.Count; i++)
			{
				this.m_lstMissionSlot[i].Init();
			}
		}

		// Token: 0x06008CF8 RID: 36088 RVA: 0x002FF104 File Offset: 0x002FD304
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
			this.SelectTab(this.m_tabTemplet.Key);
		}

		// Token: 0x06008CF9 RID: 36089 RVA: 0x002FF170 File Offset: 0x002FD370
		public override void Refresh()
		{
			this.SelectTab(this.m_tabTemplet.Key);
		}

		// Token: 0x06008CFA RID: 36090 RVA: 0x002FF184 File Offset: 0x002FD384
		private void SelectTab(int tabID)
		{
			this.BuildMissionTempletList(tabID);
			this.SetMissionData();
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

		// Token: 0x06008CFB RID: 36091 RVA: 0x002FF1EF File Offset: 0x002FD3EF
		private void OnMovetoShop()
		{
			NKCContentManager.MoveToShortCut(this.m_EventMissionTemplet.m_ShortcutType, this.m_EventMissionTemplet.m_ShortcutParam, false);
		}

		// Token: 0x06008CFC RID: 36092 RVA: 0x002FF20D File Offset: 0x002FD40D
		private void OnCompleteAll()
		{
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(this.m_tabTemplet.Key);
		}

		// Token: 0x06008CFD RID: 36093 RVA: 0x002FF220 File Offset: 0x002FD420
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

		// Token: 0x06008CFE RID: 36094 RVA: 0x002FF294 File Offset: 0x002FD494
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

		// Token: 0x06008CFF RID: 36095 RVA: 0x002FF364 File Offset: 0x002FD564
		private void BuildMissionTempletList(int tabID)
		{
			this.m_lstCurrentList.Clear();
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(tabID);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (NKMMissionManager.GetMissionTabTemplet(tabID) == null)
			{
				return;
			}
			for (int i = 0; i < missionTempletListByType.Count; i++)
			{
				NKMMissionTemplet nkmmissionTemplet = missionTempletListByType[i];
				if (nkmmissionTemplet != null)
				{
					if (nkmmissionTemplet.m_MissionRequire != 0)
					{
						NKMMissionData nkmmissionData = myUserData.m_MissionData.GetMissionData(nkmmissionTemplet);
						if (nkmmissionData == null)
						{
							goto IL_CE;
						}
						if (nkmmissionData.mission_id == nkmmissionTemplet.m_MissionID)
						{
							this.m_lstCurrentList.Add(nkmmissionTemplet);
							goto IL_CE;
						}
						nkmmissionData = myUserData.m_MissionData.GetMissionDataByMissionId(nkmmissionTemplet.m_MissionRequire);
						if (nkmmissionData == null)
						{
							goto IL_CE;
						}
						if (nkmmissionData.isComplete && nkmmissionData.mission_id == nkmmissionTemplet.m_MissionRequire)
						{
							this.m_lstCurrentList.Add(nkmmissionTemplet);
							goto IL_CE;
						}
						if (nkmmissionData.mission_id <= nkmmissionTemplet.m_MissionRequire)
						{
							goto IL_CE;
						}
					}
					this.m_lstCurrentList.Add(nkmmissionTemplet);
				}
				IL_CE:;
			}
			if (this.m_lstCurrentList.Count != this.m_lstMissionSlot.Count)
			{
				Log.Error(string.Format("MissionCount error! - TempletCount : {0}, SlotCount : {1}", this.m_lstCurrentList.Count, this.m_lstMissionSlot.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Event/NKCUIEventSubUIFixedMission.cs", 238);
			}
			this.m_lstCurrentList.Sort(new Comparison<NKMMissionTemplet>(this.Comparer));
		}

		// Token: 0x06008D00 RID: 36096 RVA: 0x002FF4B7 File Offset: 0x002FD6B7
		private int Comparer(NKMMissionTemplet x, NKMMissionTemplet y)
		{
			if (x.m_MissionPoolID != y.m_MissionPoolID)
			{
				return x.m_MissionPoolID.CompareTo(y.m_MissionPoolID);
			}
			return x.m_MissionID.CompareTo(y.m_MissionID);
		}

		// Token: 0x06008D01 RID: 36097 RVA: 0x002FF4EC File Offset: 0x002FD6EC
		private void SetMissionData()
		{
			for (int i = 0; i < this.m_lstMissionSlot.Count; i++)
			{
				if (i < this.m_lstCurrentList.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstMissionSlot[i], true);
					NKMMissionTemplet cNKMMissionTemplet = this.m_lstCurrentList[i];
					this.m_lstMissionSlot[i].m_bShowRewardName = true;
					this.m_lstMissionSlot[i].SetData(cNKMMissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstMissionSlot[i], false);
				}
			}
		}

		// Token: 0x06008D02 RID: 36098 RVA: 0x002FF594 File Offset: 0x002FD794
		private void SetRemainTime()
		{
			this.Refresh();
		}

		// Token: 0x06008D03 RID: 36099 RVA: 0x002FF59C File Offset: 0x002FD79C
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

		// Token: 0x040079D4 RID: 31188
		[Header("일괄 완료 버튼")]
		public NKCUIComStateButton m_csbtnCompleteAll;

		// Token: 0x040079D5 RID: 31189
		[Header("이벤트 상점 이동 버튼")]
		public NKCUIComStateButton m_csbtnEventShop;

		// Token: 0x040079D6 RID: 31190
		[Header("미션 슬롯 프리팹")]
		public List<NKCUIMissionAchieveSlot> m_lstMissionSlot;

		// Token: 0x040079D7 RID: 31191
		[Header("미션 스크롤렉트")]
		public ScrollRect m_srMission;

		// Token: 0x040079D8 RID: 31192
		[Header("초기화가 되는 경우 남은시간")]
		public GameObject m_objResetRemainTime;

		// Token: 0x040079D9 RID: 31193
		public Text m_lbResetRemainTime;

		// Token: 0x040079DA RID: 31194
		private NKCEventMissionTemplet m_EventMissionTemplet;

		// Token: 0x040079DB RID: 31195
		private List<NKMMissionTemplet> m_lstCurrentList = new List<NKMMissionTemplet>();

		// Token: 0x040079DC RID: 31196
		private float m_fDeltaTime;
	}
}
