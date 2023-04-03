using System;
using ClientPacket.Common;
using DG.Tweening;
using NKC.UI.Event;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C5B RID: 3163
	public class NKCUIComKillCountTotal : MonoBehaviour
	{
		// Token: 0x06009360 RID: 37728 RVA: 0x00324B94 File Offset: 0x00322D94
		public void Init()
		{
			NKCUISlot serverRewardSlot = this.m_serverRewardSlot;
			if (serverRewardSlot != null)
			{
				serverRewardSlot.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnServerReward, new UnityAction(this.OnClickViewServerReward));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnServerRewardProgress, new UnityAction(this.OnClickProgress));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnServerRewardComplete, new UnityAction(this.OnClickComplete));
		}

		// Token: 0x06009361 RID: 37729 RVA: 0x00324BF8 File Offset: 0x00322DF8
		public void SetData(int eventId)
		{
			this.m_iEventId = eventId;
			this.m_iMaxServerStep = 0;
			long num = 0L;
			int num2 = 0;
			NKMServerKillCountData killCountServerData = NKCKillCountManager.GetKillCountServerData(eventId);
			if (killCountServerData != null)
			{
				num = killCountServerData.killCount;
			}
			NKMKillCountData killCountData = NKCKillCountManager.GetKillCountData(eventId);
			if (killCountData != null)
			{
				num2 = killCountData.serverCompleteStep;
			}
			NKCUtil.SetLabelText(this.m_lbServerCurrentKillCnt, string.Format("{0:#,0}", num));
			NKMKillCountTemplet nkmkillCountTemplet = NKMKillCountTemplet.Find(eventId);
			if (nkmkillCountTemplet != null)
			{
				this.m_iMaxServerStep = nkmkillCountTemplet.GetMaxServerStep();
				NKCUtil.SetLabelText(this.m_lbCurrentRewardStep, string.Format(NKCUtilString.GET_STRING_KILLCOUNT_SERVER_REWARD_CURRENT_STEP, Mathf.Min(num2 + 1, this.m_iMaxServerStep)));
				NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
				nkmkillCountTemplet.TryGetServerStep(num2 + 1, out nkmkillCountStepTemplet);
				NKMKillCountStepTemplet nkmkillCountStepTemplet2 = null;
				nkmkillCountTemplet.TryGetServerStep(num2, out nkmkillCountStepTemplet2);
				float num4;
				if (nkmkillCountStepTemplet != null)
				{
					int killCount = nkmkillCountStepTemplet.KillCount;
					int num3 = (nkmkillCountStepTemplet2 != null) ? nkmkillCountStepTemplet2.KillCount : 0;
					NKCUtil.SetLabelText(this.m_lbServerRewardKillCnt, string.Format("{0:#,0}", killCount));
					num4 = (float)(num - (long)num3) / (float)(killCount - num3);
					NKCUtil.SetImageFillAmount(this.m_KillCountGauge, num4);
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmkillCountStepTemplet.RewardInfo, 0);
					this.m_serverRewardSlot.SetData(data, true, null);
				}
				else
				{
					NKMKillCountStepTemplet nkmkillCountStepTemplet3 = null;
					nkmkillCountTemplet.TryGetServerStep(num2 - 1, out nkmkillCountStepTemplet3);
					int num5 = (nkmkillCountStepTemplet3 != null) ? nkmkillCountStepTemplet3.KillCount : 0;
					int num6 = (nkmkillCountStepTemplet2 != null) ? nkmkillCountStepTemplet2.KillCount : 0;
					NKCUtil.SetLabelText(this.m_lbServerRewardKillCnt, string.Format("{0:#,0}", num6));
					num4 = (float)(num - (long)num5) / (float)(num6 - num5);
					NKCUtil.SetImageFillAmount(this.m_KillCountGauge, num4);
					NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(nkmkillCountStepTemplet2.RewardInfo, 0);
					this.m_serverRewardSlot.SetData(data2, true, null);
				}
				NKCUtil.SetGameobjectActive(this.m_csbtnServerRewardProgress.gameObject, num4 < 1f);
				NKCUtil.SetGameobjectActive(this.m_csbtnServerRewardComplete.gameObject, num4 >= 1f);
				if (num2 >= this.m_iMaxServerStep)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnServerRewardProgress.gameObject, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnServerRewardComplete.gameObject, false);
					NKCUtil.SetGameobjectActive(this.m_objSlotRewardOn, false);
					NKCUtil.SetGameobjectActive(this.m_objComplet, true);
					this.m_serverRewardSlot.SetDisable(true, "");
					this.m_serverRewardSlot.SetEventGet(true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objSlotRewardOn, num4 >= 1f);
					NKCUtil.SetGameobjectActive(this.m_objComplet, false);
					this.m_serverRewardSlot.SetDisable(false, "");
					this.m_serverRewardSlot.SetEventGet(false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objRefreshEffect, false);
			if (NKCUIEventSubUIHorizon.RewardGet)
			{
				NKCUtil.SetGameobjectActive(this.m_objRefreshEffect, true);
				DOTweenAnimation componentInChildren = this.m_objRefreshEffect.GetComponentInChildren<DOTweenAnimation>();
				if (componentInChildren != null)
				{
					componentInChildren.DORestart();
				}
				NKCUIEventSubUIHorizon.RewardGet = false;
			}
		}

		// Token: 0x06009362 RID: 37730 RVA: 0x00324ECE File Offset: 0x003230CE
		private void OnClickProgress()
		{
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, "EC_EVENT", false);
		}

		// Token: 0x06009363 RID: 37731 RVA: 0x00324EDC File Offset: 0x003230DC
		private void OnClickComplete()
		{
			NKMKillCountData killCountData = NKCKillCountManager.GetKillCountData(this.m_iEventId);
			int num = 0;
			if (killCountData != null)
			{
				num = killCountData.serverCompleteStep;
			}
			if (this.m_iMaxServerStep <= num)
			{
				return;
			}
			NKMKillCountTemplet nkmkillCountTemplet = NKMKillCountTemplet.Find(this.m_iEventId);
			if (nkmkillCountTemplet != null)
			{
				NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
				UnlockInfo unlockInfo = nkmkillCountTemplet.UnlockInfo;
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
				{
					NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(nkmkillCountTemplet.UnlockInfo.reqValue);
					string message;
					if (nkmkillCountTemplet.UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE && nkmstageTempletV != null)
					{
						message = nkmstageTempletV.GetDungeonName() + " " + NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_STAGE", false);
					}
					else
					{
						unlockInfo = nkmkillCountTemplet.UnlockInfo;
						message = NKCContentManager.MakeUnlockConditionString(unlockInfo, false);
					}
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(message, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
				NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
				nkmkillCountTemplet.TryGetServerStep(num + 1, out nkmkillCountStepTemplet);
				long num2 = 0L;
				NKMServerKillCountData killCountServerData = NKCKillCountManager.GetKillCountServerData(this.m_iEventId);
				if (killCountServerData != null)
				{
					num2 = killCountServerData.killCount;
				}
				if (nkmkillCountStepTemplet != null && (long)nkmkillCountStepTemplet.KillCount <= num2)
				{
					NKCPacketSender.Send_NKMPacket_KILL_COUNT_SERVER_REWARD_REQ(this.m_iEventId, num + 1);
				}
			}
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x00324FF5 File Offset: 0x003231F5
		private void OnClickViewServerReward()
		{
			NKCPopupEventKillCountReward.Instance.Open(this.m_iEventId);
		}

		// Token: 0x0400804C RID: 32844
		public Text m_lbCurrentRewardStep;

		// Token: 0x0400804D RID: 32845
		public Text m_lbServerCurrentKillCnt;

		// Token: 0x0400804E RID: 32846
		public Text m_lbServerRewardKillCnt;

		// Token: 0x0400804F RID: 32847
		public NKCUISlot m_serverRewardSlot;

		// Token: 0x04008050 RID: 32848
		public Image m_KillCountGauge;

		// Token: 0x04008051 RID: 32849
		public GameObject m_objComplet;

		// Token: 0x04008052 RID: 32850
		public GameObject m_objRefreshEffect;

		// Token: 0x04008053 RID: 32851
		public GameObject m_objSlotRewardOn;

		// Token: 0x04008054 RID: 32852
		public NKCUIComStateButton m_csbtnServerReward;

		// Token: 0x04008055 RID: 32853
		public NKCUIComStateButton m_csbtnServerRewardComplete;

		// Token: 0x04008056 RID: 32854
		public NKCUIComStateButton m_csbtnServerRewardProgress;

		// Token: 0x04008057 RID: 32855
		private int m_iEventId;

		// Token: 0x04008058 RID: 32856
		private int m_iMaxServerStep;

		// Token: 0x04008059 RID: 32857
		private const int StepMin = 1;
	}
}
