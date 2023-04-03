using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A31 RID: 2609
	public class NKCPopupAchieveRateReward : NKCUIBase
	{
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x0600723B RID: 29243 RVA: 0x0025F639 File Offset: 0x0025D839
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x0600723C RID: 29244 RVA: 0x0025F63C File Offset: 0x0025D83C
		public override string MenuName
		{
			get
			{
				return "달성도 보상 팝업";
			}
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x0025F644 File Offset: 0x0025D844
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			for (int i = 0; i < this.m_EpisodePopupProgress.Length; i++)
			{
				for (int j = 0; j < this.m_EpisodePopupProgress[i].AB_ICON_SLOT.Length; j++)
				{
					if (this.m_EpisodePopupProgress[i].AB_ICON_SLOT[j] != null)
					{
						this.m_EpisodePopupProgress[i].AB_ICON_SLOT[j].Init();
					}
				}
			}
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(this.OnClickGetAllReward));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			this.m_eventTriggerBG.triggers.Add(entry);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x0025F76B File Offset: 0x0025D96B
		public void Open(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			base.gameObject.SetActive(true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.SetData(cNKMEpisodeTemplet);
			base.UIOpened(true);
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x0025F798 File Offset: 0x0025D998
		public void OnClickGetAllReward()
		{
			if (this.m_NKMEpisodeTemplet == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < 3; i++)
			{
				foreach (object obj in Enum.GetValues(typeof(EPISODE_DIFFICULTY)))
				{
					EPISODE_DIFFICULTY episodeDifficulty = (EPISODE_DIFFICULTY)obj;
					if (NKMEpisodeMgr.CanGetEpisodeCompleteReward(myUserData, this.m_NKMEpisodeTemplet.m_EpisodeID, episodeDifficulty, i) == NKM_ERROR_CODE.NEC_OK)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKMPacket_EPISODE_COMPLETE_REWARD_ALL_REQ nkmpacket_EPISODE_COMPLETE_REWARD_ALL_REQ = new NKMPacket_EPISODE_COMPLETE_REWARD_ALL_REQ();
			nkmpacket_EPISODE_COMPLETE_REWARD_ALL_REQ.episodeID = this.m_NKMEpisodeTemplet.m_EpisodeID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EPISODE_COMPLETE_REWARD_ALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x0025F878 File Offset: 0x0025DA78
		public void ResetUI()
		{
			if (this.m_NKMEpisodeTemplet == null)
			{
				return;
			}
			this.SetData(this.m_NKMEpisodeTemplet);
		}

		// Token: 0x06007241 RID: 29249 RVA: 0x0025F890 File Offset: 0x0025DA90
		private void SetData(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			this.m_NKMEpisodeTemplet = cNKMEpisodeTemplet;
			this.m_NKM_UI_OPERATION_POPUP_EP.text = this.m_NKMEpisodeTemplet.GetEpisodeTitle();
			this.m_NKM_UI_OPERATION_POPUP_TITLE.text = this.m_NKMEpisodeTemplet.GetEpisodeName();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			float width = this.m_rtREWARD.GetWidth();
			bool flag = false;
			int i = 0;
			while (i < this.m_EpisodePopupProgress.Length)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(cNKMEpisodeTemplet.m_EpisodeID, (EPISODE_DIFFICULTY)i);
				if (i > 1)
				{
					Debug.LogError("$추가 난이도가 있는 경우, 추가 작업 해주세요.");
					break;
				}
				if (i != 1)
				{
					goto IL_A2;
				}
				NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].ObjProgress, nkmepisodeTempletV != null);
				if (nkmepisodeTempletV != null)
				{
					goto IL_A2;
				}
				IL_5C7:
				i++;
				continue;
				IL_A2:
				EPISODE_DIFFICULTY episode_DIFFICULTY = (EPISODE_DIFFICULTY)i;
				NKCUtil.SetLabelText(this.m_EpisodePopupProgress[i].NKM_UI_OPERATION_POPUP_MEDAL_TEXT, NKMEpisodeMgr.GetEPProgressClearCount(myUserData, nkmepisodeTempletV).ToString());
				List<int> list = new List<int>();
				for (int j = 0; j < nkmepisodeTempletV.m_CompletionReward.Length; j++)
				{
					if (nkmepisodeTempletV.m_CompletionReward[j] != null)
					{
						list.Add(nkmepisodeTempletV.m_CompletionReward[j].m_CompleteRate);
					}
				}
				if (list.Count == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].ObjProgress, false);
					goto IL_5C7;
				}
				int epprogressTotalCount = NKMEpisodeMgr.GetEPProgressTotalCount(nkmepisodeTempletV);
				int num = 0;
				while (num < this.m_EpisodePopupProgress[i].TEXT.Length && list.Count > num)
				{
					float num2 = (float)(epprogressTotalCount * list[num]) * 0.01f;
					NKCUtil.SetLabelText(this.m_EpisodePopupProgress[i].TEXT[num], ((int)num2).ToString());
					num++;
				}
				float epprogressPercent = NKMEpisodeMgr.GetEPProgressPercent(myUserData, nkmepisodeTempletV);
				this.m_EpisodePopupProgress[i].NKM_UI_OPERATION_EPISODE_PROGRESSBAR.value = epprogressPercent;
				NKC_EP_ACHIEVE_RATE nkc_EP_ACHIEVE_RATE;
				if (list.Count > 2 && epprogressPercent >= (float)list[2] * 0.01f)
				{
					nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_CLEAR_STEP_3;
				}
				else if (list.Count > 1 && epprogressPercent >= (float)list[1] * 0.01f)
				{
					nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_CLEAR_STEP_2;
				}
				else if (list.Count > 0 && epprogressPercent >= (float)list[0] * 0.01f)
				{
					nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_CLEAR_STEP_1;
				}
				else
				{
					nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_NONE;
				}
				int num3 = 0;
				while (num3 < 3 && num3 < list.Count)
				{
					if (num3 <= (int)nkc_EP_ACHIEVE_RATE)
					{
						if (episode_DIFFICULTY == EPISODE_DIFFICULTY.NORMAL)
						{
							NKCUtil.SetImageColor(this.m_EpisodePopupProgress[i].METAL[num3], NKCUtil.GetColor("#00D8FF"));
						}
						else if (episode_DIFFICULTY == EPISODE_DIFFICULTY.HARD)
						{
							NKCUtil.SetImageColor(this.m_EpisodePopupProgress[i].METAL[num3], NKCUtil.GetColor("#FFDE00"));
						}
						NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].FX[num3], true);
					}
					else
					{
						NKCUtil.SetImageColor(this.m_EpisodePopupProgress[i].METAL[num3], NKCUtil.GetColor("#FFFFFF"));
						NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].FX[num3], false);
					}
					num3++;
				}
				NKMEpisodeCompleteData episodeCompleteData = myUserData.GetEpisodeCompleteData(nkmepisodeTempletV.m_EpisodeID, episode_DIFFICULTY);
				for (int k = 0; k < 3; k++)
				{
					if (k >= list.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].rtMETAL[k].gameObject, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].rtMETAL[k].gameObject, true);
						float x = width * (float)list[k] * 0.01f;
						this.m_EpisodePopupProgress[i].rtMETAL[k].anchoredPosition = new Vector2(x, this.m_EpisodePopupProgress[i].rtMETAL[k].anchoredPosition.y);
						NKCUISlot.OnClick onClick = null;
						NKMRewardInfo rewardInfo = nkmepisodeTempletV.m_CompletionReward[k].m_RewardInfo;
						if (NKMEpisodeMgr.CanGetEpisodeCompleteReward(myUserData, nkmepisodeTempletV.m_EpisodeID, episode_DIFFICULTY, k) == NKM_ERROR_CODE.NEC_OK)
						{
							if (k == 0)
							{
								if (i == 0)
								{
									onClick = new NKCUISlot.OnClick(this.OnClickIndex0);
								}
								else if (i == 1)
								{
									onClick = new NKCUISlot.OnClick(this.OnClickIndex3);
								}
							}
							else if (k == 1)
							{
								if (i == 0)
								{
									onClick = new NKCUISlot.OnClick(this.OnClickIndex1);
								}
								else if (i == 1)
								{
									onClick = new NKCUISlot.OnClick(this.OnClickIndex4);
								}
							}
							else if (k == 2)
							{
								if (i == 0)
								{
									onClick = new NKCUISlot.OnClick(this.OnClickIndex2);
								}
								else if (i == 1)
								{
									onClick = new NKCUISlot.OnClick(this.OnClickIndex5);
								}
							}
						}
						if (episodeCompleteData != null)
						{
							NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].CHECK[k], episodeCompleteData.m_bRewards[k]);
							if (NKMEpisodeMgr.CanGetEpisodeCompleteReward(myUserData, nkmepisodeTempletV.m_EpisodeID, episode_DIFFICULTY, k) == NKM_ERROR_CODE.NEC_OK)
							{
								NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].AB_ICON_SLOT_REWARD_FX[k], !episodeCompleteData.m_bRewards[k]);
								this.m_EpisodePopupProgress[i].AB_ICON_SLOT[k].SetDisable(false, "");
								if (!flag)
								{
									flag = true;
								}
							}
							else
							{
								this.m_EpisodePopupProgress[i].AB_ICON_SLOT[k].SetDisable(true, "");
								NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].AB_ICON_SLOT_REWARD_FX[k], false);
							}
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].CHECK[k], false);
							NKCUtil.SetGameobjectActive(this.m_EpisodePopupProgress[i].AB_ICON_SLOT_REWARD_FX[k], false);
							this.m_EpisodePopupProgress[i].AB_ICON_SLOT[k].SetDisable(true, "");
						}
						this.m_EpisodePopupProgress[i].AB_ICON_SLOT[k].SetData(NKCUISlot.SlotData.MakeRewardTypeData(rewardInfo, 0), true, onClick);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_REWARD[k], true);
					}
				}
				goto IL_5C7;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK_DISABLE, !flag);
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x0025FE88 File Offset: 0x0025E088
		private void Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(int episodeDifficulty, int index)
		{
			if (this.m_NKMEpisodeTemplet == null)
			{
				return;
			}
			NKMPacket_EPISODE_COMPLETE_REWARD_REQ nkmpacket_EPISODE_COMPLETE_REWARD_REQ = new NKMPacket_EPISODE_COMPLETE_REWARD_REQ();
			nkmpacket_EPISODE_COMPLETE_REWARD_REQ.episodeID = this.m_NKMEpisodeTemplet.m_EpisodeID;
			nkmpacket_EPISODE_COMPLETE_REWARD_REQ.episodeDifficulty = episodeDifficulty;
			nkmpacket_EPISODE_COMPLETE_REWARD_REQ.rewardIndex = (sbyte)index;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EPISODE_COMPLETE_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x0025FED7 File Offset: 0x0025E0D7
		public void OnClickIndex0(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(0, 0);
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x0025FEE1 File Offset: 0x0025E0E1
		public void OnClickIndex1(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(0, 1);
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x0025FEEB File Offset: 0x0025E0EB
		public void OnClickIndex2(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(0, 2);
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x0025FEF5 File Offset: 0x0025E0F5
		public void OnClickIndex3(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(1, 0);
		}

		// Token: 0x06007247 RID: 29255 RVA: 0x0025FEFF File Offset: 0x0025E0FF
		public void OnClickIndex4(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(1, 1);
		}

		// Token: 0x06007248 RID: 29256 RVA: 0x0025FF09 File Offset: 0x0025E109
		public void OnClickIndex5(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.Send_NKMPacket_EPISODE_COMPLETE_REWARD_REQ(1, 2);
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x0025FF13 File Offset: 0x0025E113
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x0600724A RID: 29258 RVA: 0x0025FF28 File Offset: 0x0025E128
		public void CloseAchieveRateRewardPopup()
		{
			base.Close();
		}

		// Token: 0x0600724B RID: 29259 RVA: 0x0025FF30 File Offset: 0x0025E130
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x0600724C RID: 29260 RVA: 0x0025FF38 File Offset: 0x0025E138
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04005E20 RID: 24096
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_OPERATION";

		// Token: 0x04005E21 RID: 24097
		public const string UI_ASSET_NAME = "NKM_UI_OPERATION_POPUP_MEDAL";

		// Token: 0x04005E22 RID: 24098
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005E23 RID: 24099
		public Text m_NKM_UI_OPERATION_POPUP_EP;

		// Token: 0x04005E24 RID: 24100
		public Text m_NKM_UI_OPERATION_POPUP_TITLE;

		// Token: 0x04005E25 RID: 24101
		public Text m_NKM_UI_OPERATION_POPUP_MEDAL_TEXT;

		// Token: 0x04005E26 RID: 24102
		public List<GameObject> m_NKM_UI_OPERATION_EPISODE_REWARD;

		// Token: 0x04005E27 RID: 24103
		public List<NKCUISlot> m_lstRewardSlot;

		// Token: 0x04005E28 RID: 24104
		public List<GameObject> m_lstRewardFX;

		// Token: 0x04005E29 RID: 24105
		public List<GameObject> m_lstRewardCheck;

		// Token: 0x04005E2A RID: 24106
		public List<Text> m_NKM_UI_OPERATION_EPISODE_REWARD_Goal_Count;

		// Token: 0x04005E2B RID: 24107
		public List<Image> m_lstSmallMedal;

		// Token: 0x04005E2C RID: 24108
		public List<GameObject> m_lstSmallMedalFX;

		// Token: 0x04005E2D RID: 24109
		public Slider m_NKM_UI_OPERATION_EPISODE_PROGRESSBAR;

		// Token: 0x04005E2E RID: 24110
		public NKCUIComButton m_btnOK;

		// Token: 0x04005E2F RID: 24111
		public NKCUIComButton m_btnCancel;

		// Token: 0x04005E30 RID: 24112
		public EventTrigger m_eventTriggerBG;

		// Token: 0x04005E31 RID: 24113
		public GameObject m_NKM_UI_POPUP_OK_CANCEL_BOX_OK_DISABLE;

		// Token: 0x04005E32 RID: 24114
		private NKMEpisodeTempletV2 m_NKMEpisodeTemplet;

		// Token: 0x04005E33 RID: 24115
		[Header("클리어 정보")]
		public NKCPopupAchieveRateReward.EP_POPUP_PROGRESS[] m_EpisodePopupProgress;

		// Token: 0x04005E34 RID: 24116
		public RectTransform m_rtREWARD;

		// Token: 0x02001775 RID: 6005
		[Serializable]
		public struct EP_POPUP_PROGRESS
		{
			// Token: 0x0400A6F0 RID: 42736
			public GameObject ObjProgress;

			// Token: 0x0400A6F1 RID: 42737
			public Text NKM_UI_OPERATION_POPUP_MEDAL_TEXT;

			// Token: 0x0400A6F2 RID: 42738
			public Slider NKM_UI_OPERATION_EPISODE_PROGRESSBAR;

			// Token: 0x0400A6F3 RID: 42739
			public NKCUISlot[] AB_ICON_SLOT;

			// Token: 0x0400A6F4 RID: 42740
			public GameObject[] AB_ICON_SLOT_REWARD_FX;

			// Token: 0x0400A6F5 RID: 42741
			public GameObject[] CHECK;

			// Token: 0x0400A6F6 RID: 42742
			public RectTransform[] rtMETAL;

			// Token: 0x0400A6F7 RID: 42743
			public Image[] METAL;

			// Token: 0x0400A6F8 RID: 42744
			public GameObject[] FX;

			// Token: 0x0400A6F9 RID: 42745
			public Text[] TEXT;
		}
	}
}
