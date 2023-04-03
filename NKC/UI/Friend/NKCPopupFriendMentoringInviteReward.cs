using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B19 RID: 2841
	public class NKCPopupFriendMentoringInviteReward : NKCUIBase
	{
		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x0600815E RID: 33118 RVA: 0x002B9BD8 File Offset: 0x002B7DD8
		public static NKCPopupFriendMentoringInviteReward Instance
		{
			get
			{
				if (NKCPopupFriendMentoringInviteReward.m_Instance == null)
				{
					NKCPopupFriendMentoringInviteReward.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFriendMentoringInviteReward>("ab_ui_nkm_ui_friend", "NKM_UI_POPUP_FRIEND_MENTORING_INVITE_REWARD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFriendMentoringInviteReward.CleanupInstance)).GetInstance<NKCPopupFriendMentoringInviteReward>();
					NKCPopupFriendMentoringInviteReward.m_Instance.Init();
				}
				return NKCPopupFriendMentoringInviteReward.m_Instance;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x0600815F RID: 33119 RVA: 0x002B9C27 File Offset: 0x002B7E27
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFriendMentoringInviteReward.m_Instance != null && NKCPopupFriendMentoringInviteReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x002B9C42 File Offset: 0x002B7E42
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFriendMentoringInviteReward.m_Instance != null && NKCPopupFriendMentoringInviteReward.m_Instance.IsOpen)
			{
				NKCPopupFriendMentoringInviteReward.m_Instance.Close();
			}
		}

		// Token: 0x06008161 RID: 33121 RVA: 0x002B9C67 File Offset: 0x002B7E67
		private static void CleanupInstance()
		{
			NKCPopupFriendMentoringInviteReward.m_Instance = null;
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06008162 RID: 33122 RVA: 0x002B9C6F File Offset: 0x002B7E6F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06008163 RID: 33123 RVA: 0x002B9C72 File Offset: 0x002B7E72
		public override string MenuName
		{
			get
			{
				return "NKCPopupFriendMentoringSearch";
			}
		}

		// Token: 0x06008164 RID: 33124 RVA: 0x002B9C7C File Offset: 0x002B7E7C
		private void Init()
		{
			if (this.m_NKM_UI_POPUP_FRIEND_MENTORING_INVITE_REWARD_BG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupFriendMentoringInviteReward.CheckInstanceAndClose();
				});
				this.m_NKM_UI_POPUP_FRIEND_MENTORING_INVITE_REWARD_BG.triggers.Add(entry);
			}
			if (this.m_lstReward.Count > 5)
			{
				Debug.LogError(string.Format("{0}개 사용하기로 정함", 5));
			}
			if (this.m_BUTTON_ALL != null)
			{
				this.m_BUTTON_ALL.PointerClick.RemoveAllListeners();
				this.m_BUTTON_ALL.PointerClick.AddListener(new UnityAction(this.OnAllReceive));
				NKCUtil.SetHotkey(this.m_BUTTON_ALL, HotkeyEventType.Confirm, null, false);
			}
			if (this.m_NKM_UI_POPUP_CLOSE_BUTTON != null)
			{
				this.m_NKM_UI_POPUP_CLOSE_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_POPUP_CLOSE_BUTTON.PointerClick.AddListener(new UnityAction(NKCPopupFriendMentoringInviteReward.CheckInstanceAndClose));
			}
			foreach (NKCUISlot nkcuislot in this.m_lstReward)
			{
				if (nkcuislot != null)
				{
					nkcuislot.Init();
				}
			}
		}

		// Token: 0x06008165 RID: 33125 RVA: 0x002B9DD0 File Offset: 0x002B7FD0
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008166 RID: 33126 RVA: 0x002B9DDE File Offset: 0x002B7FDE
		private void OnAllReceive()
		{
			NKCPacketSender.Send_NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_REQ();
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x002B9DE8 File Offset: 0x002B7FE8
		public void Open(HashSet<int> rewardHistories, UnityAction callback = null)
		{
			int menteeMissionCompletCnt = NKCScenManager.CurrentUserData().GetMenteeMissionCompletCnt();
			NKCUtil.SetLabelText(this.m_MENTORING_INVITE_COMPLETE_COUNT_TEXT_02, menteeMissionCompletCnt.ToString());
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.ResetUI(rewardHistories.ToList<int>());
			this.m_callBack = callback;
			base.UIOpened(true);
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x002B9E38 File Offset: 0x002B8038
		public void ResetUI(List<int> rewardHistories)
		{
			NKMMentoringTemplet currentTempet = NKCMentoringUtil.GetCurrentTempet();
			if (currentTempet == null)
			{
				return;
			}
			IOrderedEnumerable<NKMMentoringRewardTemplet> orderedEnumerable = from x in NKMMentoringRewardTemplet.GetRewardGroupList(currentTempet.RewardGroupId)
			orderby x.InviteSuccessRequireCnt
			select x;
			if (orderedEnumerable.Count<NKMMentoringRewardTemplet>() > 5)
			{
				Debug.LogError(string.Format("{0}개 사용하기로 정함", 5));
			}
			int num = 0;
			bool flag = false;
			int menteeMissionCompletCnt = NKCScenManager.CurrentUserData().GetMenteeMissionCompletCnt();
			int num2 = 0;
			List<int> list = new List<int>();
			using (IEnumerator<NKMMentoringRewardTemplet> enumerator = orderedEnumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMMentoringRewardTemplet data = enumerator.Current;
					if (data != null)
					{
						if (data.InviteSuccessRequireCnt > num2)
						{
							num2 = data.InviteSuccessRequireCnt;
						}
						bool flag2 = rewardHistories.Contains(data.InviteSuccessRequireCnt);
						bool flag3 = false;
						bool flag4 = data.InviteSuccessRequireCnt <= menteeMissionCompletCnt;
						if (!flag2 && flag4)
						{
							flag3 = true;
							flag = true;
						}
						NKCUtil.SetGameobjectActive(this.m_lstCircleOff[num], !flag4);
						NKCUtil.SetGameobjectActive(this.m_lstCircleOn[num], flag4);
						NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(data.RewardType, data.RewardId, data.RewardCount, 0);
						NKCUISlot.OnClick onClick = null;
						if (!flag2 && flag3)
						{
							onClick = delegate(NKCUISlot.SlotData slotData, bool bLocked)
							{
								NKCPacketSender.Send_NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_REQ(data.InviteSuccessRequireCnt);
							};
						}
						this.m_lstReward[num].SetData(data2, false, onClick);
						this.m_lstReward[num].SetRewardFx(flag4 && flag3);
						this.m_lstReward[num].SetDisable(flag4 && !flag3, "");
						this.m_lstReward[num].SetEventGet(flag4 && !flag3);
						NKCUtil.SetLabelText(this.m_lstRewardNum[num], data.InviteSuccessRequireCnt.ToString());
						list.Add(data.InviteSuccessRequireCnt);
						num++;
					}
				}
			}
			if (this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER != null)
			{
				float num3 = 0f;
				if (menteeMissionCompletCnt > 0)
				{
					float num4 = 0f;
					if (list.Count > 0)
					{
						num4 = this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER.maxValue / (float)list.Count;
					}
					int i = 0;
					while (i < list.Count)
					{
						if (list[i] == menteeMissionCompletCnt)
						{
							num3 = (float)(i + 1) * num4;
							break;
						}
						if (list[i] > menteeMissionCompletCnt)
						{
							num3 = (float)i * num4;
							if (num3 >= this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER.maxValue)
							{
								num3 = this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER.maxValue;
								break;
							}
							int num5 = list[i] - list[i - 1];
							float num6 = num4 / (float)num5 * (float)(num5 - (list[i] - menteeMissionCompletCnt));
							num3 += num6;
							break;
						}
						else
						{
							i++;
						}
					}
				}
				this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER.value = num3;
			}
			if (!flag)
			{
				this.m_BUTTON_ALL.Lock(false);
				NKCScenManager.CurrentUserData().SetMentoringNotify(false);
				if (this.m_callBack != null)
				{
					this.m_callBack();
					return;
				}
			}
			else
			{
				this.m_BUTTON_ALL.UnLock(false);
			}
		}

		// Token: 0x04006D8E RID: 28046
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_friend";

		// Token: 0x04006D8F RID: 28047
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FRIEND_MENTORING_INVITE_REWARD";

		// Token: 0x04006D90 RID: 28048
		private static NKCPopupFriendMentoringInviteReward m_Instance;

		// Token: 0x04006D91 RID: 28049
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSE_BUTTON;

		// Token: 0x04006D92 RID: 28050
		public EventTrigger m_NKM_UI_POPUP_FRIEND_MENTORING_INVITE_REWARD_BG;

		// Token: 0x04006D93 RID: 28051
		public Text m_MENTORING_INVITE_COMPLETE_COUNT_TEXT_02;

		// Token: 0x04006D94 RID: 28052
		public NKCUIComStateButton m_BUTTON_ALL;

		// Token: 0x04006D95 RID: 28053
		public List<NKCUISlot> m_lstReward = new List<NKCUISlot>();

		// Token: 0x04006D96 RID: 28054
		public Slider m_MENTORING_MISSION_LIST_PROGRESS_SLIDER;

		// Token: 0x04006D97 RID: 28055
		public List<GameObject> m_lstCircleOff = new List<GameObject>();

		// Token: 0x04006D98 RID: 28056
		public List<GameObject> m_lstCircleOn = new List<GameObject>();

		// Token: 0x04006D99 RID: 28057
		public List<Text> m_lstRewardNum = new List<Text>();

		// Token: 0x04006D9A RID: 28058
		private const int FIXED_REWARD_COUNT = 5;

		// Token: 0x04006D9B RID: 28059
		private UnityAction m_callBack;
	}
}
