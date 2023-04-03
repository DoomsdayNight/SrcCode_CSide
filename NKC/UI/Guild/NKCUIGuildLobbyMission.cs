using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B55 RID: 2901
	public class NKCUIGuildLobbyMission : MonoBehaviour
	{
		// Token: 0x06008451 RID: 33873 RVA: 0x002C9AB8 File Offset: 0x002C7CB8
		public void InitUI()
		{
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loop, null);
			this.m_btnCompleteAll.PointerClick.RemoveAllListeners();
			this.m_btnCompleteAll.PointerClick.AddListener(new UnityAction(this.OnClickCompleteAll));
			this.m_tglUser.OnValueChanged.RemoveAllListeners();
			this.m_tglUser.OnValueChanged.AddListener(new UnityAction<bool>(this.OnUserToggle));
			this.m_tglUnion.OnValueChanged.RemoveAllListeners();
			this.m_tglUnion.OnValueChanged.AddListener(new UnityAction<bool>(this.OnUnionToggle));
		}

		// Token: 0x06008452 RID: 33874 RVA: 0x002C9BA8 File Offset: 0x002C7DA8
		private RectTransform GetObject(int idx)
		{
			NKCUIMissionAchieveSlot nkcuimissionAchieveSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuimissionAchieveSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuimissionAchieveSlot = UnityEngine.Object.Instantiate<NKCUIMissionAchieveSlot>(this.m_pfbSlot, this.m_trSlotParent);
			}
			nkcuimissionAchieveSlot.Init();
			return nkcuimissionAchieveSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008453 RID: 33875 RVA: 0x002C9BF4 File Offset: 0x002C7DF4
		private void ReturnObject(Transform tr)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06008454 RID: 33876 RVA: 0x002C9C27 File Offset: 0x002C7E27
		private void ProvideData(Transform tr, int idx)
		{
			tr.GetComponent<NKCUIMissionAchieveSlot>().SetData(this.m_lstUserMissions[idx], new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickRefresh), null);
		}

		// Token: 0x06008455 RID: 33877 RVA: 0x002C9C68 File Offset: 0x002C7E68
		public void SetData()
		{
			this.BuildMissionTemplets();
			if (this.m_CurrentMissionUIType == NKCUIGuildLobbyMission.MissionUIType.User)
			{
				this.m_tglUser.Select(true, true, true);
				this.OnUserToggle(true);
			}
			else
			{
				this.m_tglUnion.Select(true, true, true);
				this.OnUnionToggle(true);
			}
			this.SetNextResetTime();
		}

		// Token: 0x06008456 RID: 33878 RVA: 0x002C9CB7 File Offset: 0x002C7EB7
		private void BuildMissionTemplets()
		{
			if (this.m_lstUserMissions.Count == 0 || !NKCScenManager.CurrentUserData().m_MissionData.WaitingForRandomMissionRefresh())
			{
				this.m_lstUserMissions = NKMMissionManager.GetGuildUserMissionTemplets();
			}
			this.m_lstUnionMissions = new List<NKMMissionTemplet>();
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x002C9CF0 File Offset: 0x002C7EF0
		private void SetRefreshCount()
		{
			if (this.m_CurrentMissionUIType != NKCUIGuildLobbyMission.MissionUIType.User)
			{
				NKCUtil.SetGameobjectActive(this.m_objRefreshCount, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRefreshCount, true);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.GUILD);
			if (missionTabTemplet == null)
			{
				Log.Error("GuildTabTemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildLobbyMission.cs", 137);
				return;
			}
			int randomMissionRefreshCount = nkmuserData.m_MissionData.GetRandomMissionRefreshCount(missionTabTemplet.m_tabID);
			if (randomMissionRefreshCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_imgRefreshCost, false);
				NKCUtil.SetLabelText(this.m_lbRefreshCost, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_DONATION_REFRESH_FREE_TEXT, randomMissionRefreshCount));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgRefreshCost, true);
			NKCUtil.SetImageSprite(this.m_imgRefreshCost, NKCResourceUtility.GetOrLoadMiscItemIcon(missionTabTemplet.m_MissionRefreshReqItemID), false);
			NKCUtil.SetLabelText(this.m_lbRefreshCost, missionTabTemplet.m_MissionRefreshReqItemValue.ToString());
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x002C9DC4 File Offset: 0x002C7FC4
		private void SetButtonState()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (this.m_CurrentMissionUIType != NKCUIGuildLobbyMission.MissionUIType.User)
			{
				this.m_btnCompleteAll.Lock(false);
				NKCUtil.SetLabelTextColor(this.m_lbCompleteAll, NKCUtil.GetColor("#212122"));
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.GUILD);
			if (missionTabTemplet == null)
			{
				Log.Error("GuildTabTemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildLobbyMission.cs", 173);
				return;
			}
			if (!nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, missionTabTemplet.m_tabID, false))
			{
				this.m_btnCompleteAll.Lock(false);
				NKCUtil.SetLabelTextColor(this.m_lbCompleteAll, NKCUtil.GetColor("#212122"));
				return;
			}
			this.m_btnCompleteAll.UnLock(false);
			NKCUtil.SetLabelTextColor(this.m_lbCompleteAll, NKCUtil.GetColor("#582817"));
		}

		// Token: 0x06008459 RID: 33881 RVA: 0x002C9E80 File Offset: 0x002C8080
		private void SetNextResetTime()
		{
			DateTime resetTime = NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Week);
			if ((NKCSynchronizedTime.GetServerUTCTime(0.0) - resetTime).TotalMinutes < (double)this.WEEKLY_REFRESH_WAITING_MINUTE)
			{
				NKCUtil.SetGameobjectActive(this.m_objResetWaiting, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objResetWaiting, false);
			}
			NKCUtil.SetLabelText(this.m_lbNextResetTime, NKCUtilString.GetResetTimeString(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Week, 3));
		}

		// Token: 0x0600845A RID: 33882 RVA: 0x002C9F01 File Offset: 0x002C8101
		private bool WaitingForWeeklyRefresh()
		{
			return NKCScenManager.CurrentUserData().m_MissionData.WaitingForRandomMissionRefresh();
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x002C9F12 File Offset: 0x002C8112
		private void OnClickCompleteAll()
		{
			if (this.WaitingForWeeklyRefresh())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_TITLE_TEXT, NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_DESC_TEXT, new NKCPopupOKCancel.OnButton(this.SetData), "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.GUILD).m_tabID);
		}

		// Token: 0x0600845C RID: 33884 RVA: 0x002C9F50 File Offset: 0x002C8150
		public void OnClickMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (this.WaitingForWeeklyRefresh())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_TITLE_TEXT, NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_DESC_TEXT, new NKCPopupOKCancel.OnButton(this.SetData), "");
				return;
			}
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
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, nkmuserData))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.SetData), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x0600845D RID: 33885 RVA: 0x002C9FF4 File Offset: 0x002C81F4
		public void OnClickComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (this.WaitingForWeeklyRefresh())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_TITLE_TEXT, NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_DESC_TEXT, new NKCPopupOKCancel.OnButton(this.SetData), "");
				return;
			}
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId) == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x0600845E RID: 33886 RVA: 0x002CA074 File Offset: 0x002C8274
		private void OnClickRefresh(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (this.WaitingForWeeklyRefresh())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_TITLE_TEXT, NKCUtilString.GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_DESC_TEXT, new NKCPopupOKCancel.OnButton(this.SetData), "");
				return;
			}
			NKMMissionTabTemplet tabTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_TabTemplet;
			if (tabTemplet == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (nkmuserData.m_MissionData.GetRandomMissionRefreshCount(tabTemplet.m_tabID) > 0)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_TITLE, NKCUtilString.GET_STRING_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_BODY, delegate()
				{
					this.OnConfirmRefresh(cNKCUIMissionAchieveSlot);
				}, null, false);
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_TITLE, NKCUtilString.GET_STRING_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_BODY, tabTemplet.m_MissionRefreshReqItemID, tabTemplet.m_MissionRefreshReqItemValue, delegate()
			{
				this.OnConfirmRefresh(cNKCUIMissionAchieveSlot);
			}, null, false);
		}

		// Token: 0x0600845F RID: 33887 RVA: 0x002CA140 File Offset: 0x002C8340
		private void OnConfirmRefresh(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_RANDOM_MISSION_CHANGE_REQ(nkmmissionTemplet.m_MissionTabId, nkmmissionTemplet.m_MissionID);
		}

		// Token: 0x06008460 RID: 33888 RVA: 0x002CA16C File Offset: 0x002C836C
		private void OnUserToggle(bool bValue)
		{
			if (bValue)
			{
				this.m_CurrentMissionUIType = NKCUIGuildLobbyMission.MissionUIType.User;
				this.m_loop.TotalCount = this.m_lstUserMissions.Count;
				this.m_loop.SetIndexPosition(0);
				this.SetRefreshCount();
				this.SetButtonState();
				NKCUtil.SetGameobjectActive(this.m_objDisabled, this.m_loop.TotalCount == 0);
			}
		}

		// Token: 0x06008461 RID: 33889 RVA: 0x002CA1CC File Offset: 0x002C83CC
		private void OnUnionToggle(bool bValue)
		{
			if (bValue)
			{
				this.m_CurrentMissionUIType = NKCUIGuildLobbyMission.MissionUIType.Union;
				this.m_loop.TotalCount = 0;
				this.m_loop.SetIndexPosition(0);
				this.SetRefreshCount();
				this.SetButtonState();
				NKCUtil.SetGameobjectActive(this.m_objDisabled, this.m_loop.TotalCount == 0);
			}
		}

		// Token: 0x06008462 RID: 33890 RVA: 0x002CA220 File Offset: 0x002C8420
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				this.SetNextResetTime();
			}
		}

		// Token: 0x0400707A RID: 28794
		public float WEEKLY_REFRESH_WAITING_MINUTE = 5f;

		// Token: 0x0400707B RID: 28795
		public NKCUIMissionAchieveSlot m_pfbSlot;

		// Token: 0x0400707C RID: 28796
		public NKCUIComToggle m_tglUser;

		// Token: 0x0400707D RID: 28797
		public NKCUIComToggle m_tglUnion;

		// Token: 0x0400707E RID: 28798
		public GameObject m_objRefreshCount;

		// Token: 0x0400707F RID: 28799
		public Image m_imgRefreshCost;

		// Token: 0x04007080 RID: 28800
		public Text m_lbRefreshCost;

		// Token: 0x04007081 RID: 28801
		public LoopScrollRect m_loop;

		// Token: 0x04007082 RID: 28802
		public Transform m_trSlotParent;

		// Token: 0x04007083 RID: 28803
		public NKCUIComStateButton m_btnCompleteAll;

		// Token: 0x04007084 RID: 28804
		public Text m_lbCompleteAll;

		// Token: 0x04007085 RID: 28805
		public Text m_lbNextResetTime;

		// Token: 0x04007086 RID: 28806
		public GameObject m_objDisabled;

		// Token: 0x04007087 RID: 28807
		public GameObject m_objResetWaiting;

		// Token: 0x04007088 RID: 28808
		private Stack<NKCUIMissionAchieveSlot> m_stkSlot = new Stack<NKCUIMissionAchieveSlot>();

		// Token: 0x04007089 RID: 28809
		private List<NKMMissionTemplet> m_lstUserMissions = new List<NKMMissionTemplet>();

		// Token: 0x0400708A RID: 28810
		private List<NKMMissionTemplet> m_lstUnionMissions = new List<NKMMissionTemplet>();

		// Token: 0x0400708B RID: 28811
		private NKCUIGuildLobbyMission.MissionUIType m_CurrentMissionUIType;

		// Token: 0x0400708C RID: 28812
		private float m_fDeltaTime;

		// Token: 0x020018EE RID: 6382
		public enum MissionUIType
		{
			// Token: 0x0400AA31 RID: 43569
			User,
			// Token: 0x0400AA32 RID: 43570
			Union
		}
	}
}
