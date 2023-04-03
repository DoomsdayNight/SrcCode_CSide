using System;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C59 RID: 3161
	public class NKCUIComKillCountPrivate : MonoBehaviour
	{
		// Token: 0x06009353 RID: 37715 RVA: 0x00324604 File Offset: 0x00322804
		public void Init()
		{
			if (this.m_privateRewardSlot != null)
			{
				int num = this.m_privateRewardSlot.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUISlot nkcuislot = this.m_privateRewardSlot[i];
					if (nkcuislot != null)
					{
						nkcuislot.Init();
					}
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPrivateRewardProgress, new UnityAction(this.OnClickProgress));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPrivateRewardComplete, new UnityAction(this.OnClickComplete));
			NKCUtil.SetGameobjectActive(this.m_objComplete, false);
		}

		// Token: 0x06009354 RID: 37716 RVA: 0x0032467C File Offset: 0x0032287C
		public void SetData(int eventId)
		{
			this.m_iEventId = eventId;
			this.m_iMaxUserStep = 0;
			long num = 0L;
			int userCompleteStep = 0;
			NKMKillCountData killCountData = NKCKillCountManager.GetKillCountData(eventId);
			if (killCountData != null)
			{
				num = killCountData.killCount;
				userCompleteStep = killCountData.userCompleteStep;
			}
			NKCUtil.SetLabelText(this.m_lbPrivateCurrentKillCnt, string.Format("{0:#,0}", num));
			NKMKillCountTemplet nkmkillCountTemplet = NKMKillCountTemplet.Find(eventId);
			if (nkmkillCountTemplet != null)
			{
				this.m_iMaxUserStep = nkmkillCountTemplet.GetMaxUserStep();
				this.SetRewardKillCount(nkmkillCountTemplet);
				this.SetRewardSlot(nkmkillCountTemplet, userCompleteStep, num);
				this.SetRewardGetButtonState(nkmkillCountTemplet, userCompleteStep, num);
				this.SetKillCountGauge(nkmkillCountTemplet, num, userCompleteStep);
			}
		}

		// Token: 0x06009355 RID: 37717 RVA: 0x00324708 File Offset: 0x00322908
		private void SetRewardKillCount(NKMKillCountTemplet killCountTemplet)
		{
			if (this.m_lbPrivateRewardKillCnt == null)
			{
				return;
			}
			int num = this.m_lbPrivateRewardKillCnt.Length;
			for (int i = 0; i < num; i++)
			{
				NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
				killCountTemplet.TryGetUserStep(i + 1, out nkmkillCountStepTemplet);
				if (nkmkillCountStepTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbPrivateRewardKillCnt[i], string.Format("{0:#,0}", nkmkillCountStepTemplet.KillCount));
				}
			}
		}

		// Token: 0x06009356 RID: 37718 RVA: 0x00324768 File Offset: 0x00322968
		private void SetRewardSlot(NKMKillCountTemplet killCountTemplet, int userCompleteStep, long killCount)
		{
			if (this.m_privateRewardSlot == null)
			{
				return;
			}
			int num = this.m_privateRewardSlot.Length;
			for (int i = 0; i < num; i++)
			{
				NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
				killCountTemplet.TryGetUserStep(i + 1, out nkmkillCountStepTemplet);
				if (nkmkillCountStepTemplet != null && !(this.m_privateRewardSlot[i] == null))
				{
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmkillCountStepTemplet.RewardInfo, 0);
					this.m_privateRewardSlot[i].SetData(data, true, null);
					bool flag = false;
					bool bValue = false;
					if (i + 1 <= userCompleteStep)
					{
						flag = true;
					}
					else if (killCount >= (long)nkmkillCountStepTemplet.KillCount)
					{
						bValue = true;
					}
					if (this.m_objSlotRewardOn != null && this.m_objSlotRewardOn[i] != null)
					{
						NKCUtil.SetGameobjectActive(this.m_objSlotRewardOn[i], bValue);
					}
					this.m_privateRewardSlot[i].SetDisable(flag, "");
					this.m_privateRewardSlot[i].SetEventGet(flag);
				}
			}
		}

		// Token: 0x06009357 RID: 37719 RVA: 0x00324848 File Offset: 0x00322A48
		private void SetRewardGetButtonState(NKMKillCountTemplet killCountTemplet, int userCompleteStep, long killCount)
		{
			if (this.m_iMaxUserStep > userCompleteStep)
			{
				NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
				killCountTemplet.TryGetUserStep(userCompleteStep + 1, out nkmkillCountStepTemplet);
				if (nkmkillCountStepTemplet != null)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnPrivateRewardProgress.gameObject, killCount < (long)nkmkillCountStepTemplet.KillCount);
					NKCUtil.SetGameobjectActive(this.m_csbtnPrivateRewardComplete.gameObject, killCount >= (long)nkmkillCountStepTemplet.KillCount);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnPrivateRewardProgress.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnPrivateRewardComplete.gameObject, false);
			}
		}

		// Token: 0x06009358 RID: 37720 RVA: 0x003248CC File Offset: 0x00322ACC
		private void SetKillCountGauge(NKMKillCountTemplet killCountTemplet, long killCount, int userCompleteStep)
		{
			if (this.m_KillCountGauge == null)
			{
				return;
			}
			int num = this.m_KillCountGauge.Length;
			for (int i = 0; i < num; i++)
			{
				if (!(this.m_KillCountGauge[i] == null))
				{
					int num2 = 0;
					NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
					killCountTemplet.TryGetUserStep(i + 1, out nkmkillCountStepTemplet);
					if (nkmkillCountStepTemplet != null)
					{
						num2 = nkmkillCountStepTemplet.KillCount;
					}
					int num3 = 0;
					NKMKillCountStepTemplet nkmkillCountStepTemplet2 = null;
					killCountTemplet.TryGetUserStep(i, out nkmkillCountStepTemplet2);
					if (nkmkillCountStepTemplet2 != null)
					{
						num3 = nkmkillCountStepTemplet2.KillCount;
					}
					float value;
					if (killCount >= (long)num2)
					{
						value = 1f;
					}
					else if (killCount < (long)num3)
					{
						value = 0f;
					}
					else
					{
						value = (float)(killCount - (long)num3) / (float)(num2 - num3);
					}
					NKCUtil.SetImageFillAmount(this.m_KillCountGauge[i], value);
				}
			}
		}

		// Token: 0x06009359 RID: 37721 RVA: 0x00324985 File Offset: 0x00322B85
		private void OnClickProgress()
		{
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, "EC_EVENT", false);
		}

		// Token: 0x0600935A RID: 37722 RVA: 0x00324994 File Offset: 0x00322B94
		private void OnClickComplete()
		{
			NKMKillCountData killCountData = NKCKillCountManager.GetKillCountData(this.m_iEventId);
			long num = 0L;
			int num2 = 0;
			if (killCountData != null)
			{
				num = killCountData.killCount;
				num2 = killCountData.userCompleteStep;
			}
			if (this.m_iMaxUserStep <= num2)
			{
				return;
			}
			NKMKillCountTemplet nkmkillCountTemplet = NKMKillCountTemplet.Find(this.m_iEventId);
			if (nkmkillCountTemplet != null)
			{
				NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
				nkmkillCountTemplet.TryGetUserStep(num2 + 1, out nkmkillCountStepTemplet);
				if (nkmkillCountStepTemplet != null && (long)nkmkillCountStepTemplet.KillCount <= num)
				{
					NKCPacketSender.Send_NKMPacket_KILL_COUNT_USER_REWARD_REQ(this.m_iEventId, num2 + 1);
				}
			}
		}

		// Token: 0x0400803C RID: 32828
		public Text m_lbPrivateCurrentKillCnt;

		// Token: 0x0400803D RID: 32829
		public Text[] m_lbPrivateRewardKillCnt;

		// Token: 0x0400803E RID: 32830
		public NKCUISlot[] m_privateRewardSlot;

		// Token: 0x0400803F RID: 32831
		public GameObject m_objComplete;

		// Token: 0x04008040 RID: 32832
		public GameObject[] m_objSlotRewardOn;

		// Token: 0x04008041 RID: 32833
		public Image[] m_KillCountGauge;

		// Token: 0x04008042 RID: 32834
		public NKCUIComStateButton m_csbtnPrivateRewardComplete;

		// Token: 0x04008043 RID: 32835
		public NKCUIComStateButton m_csbtnPrivateRewardProgress;

		// Token: 0x04008044 RID: 32836
		private int m_iEventId;

		// Token: 0x04008045 RID: 32837
		private int m_iMaxUserStep;
	}
}
