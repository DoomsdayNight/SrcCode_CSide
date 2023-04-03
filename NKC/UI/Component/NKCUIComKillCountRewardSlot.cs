using System;
using ClientPacket.Common;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C5A RID: 3162
	public class NKCUIComKillCountRewardSlot : MonoBehaviour
	{
		// Token: 0x0600935C RID: 37724 RVA: 0x00324A10 File Offset: 0x00322C10
		public void Init()
		{
			NKCUISlot slot = this.m_Slot;
			if (slot == null)
			{
				return;
			}
			slot.Init();
		}

		// Token: 0x0600935D RID: 37725 RVA: 0x00324A24 File Offset: 0x00322C24
		public void SetData(int eventId, NKMKillCountStepTemplet stepTemplet, NKMKillCountData killCountData, long currentServerKillCount)
		{
			if (stepTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbStep, string.Format(NKCUtilString.GET_STRING_KILLCOUNT_SERVER_REWARD_STEP, stepTemplet.StepId));
			NKCUtil.SetLabelText(this.m_lbDesc, string.Format(NKCUtilString.GET_STRING_KILLCOUNT_SERVER_REWARD_DESC, stepTemplet.KillCount));
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(stepTemplet.RewardInfo, 0);
			this.m_iEventId = eventId;
			this.m_iStep = stepTemplet.StepId;
			this.m_iServerCompleteStep = 0;
			if (killCountData != null)
			{
				this.m_iServerCompleteStep = killCountData.serverCompleteStep;
			}
			if (this.m_iServerCompleteStep >= stepTemplet.StepId)
			{
				this.m_Slot.SetData(data, true, null);
				this.m_Slot.SetDisable(true, "");
				this.m_Slot.SetEventGet(true);
				this.m_Slot.SetFirstGetMark(false);
				return;
			}
			if ((long)stepTemplet.KillCount <= currentServerKillCount)
			{
				this.m_Slot.SetData(data, true, new NKCUISlot.OnClick(this.OnClickSlot));
				this.m_Slot.SetFirstGetMark(true);
				this.m_Slot.SetArrowBGText(NKCUtilString.GET_STRING_KILLCOUNT_SERVER_REWARD_GET, NKCUtil.GetColor("#4E4F52"));
			}
			else
			{
				this.m_Slot.SetData(data, true, null);
				this.m_Slot.SetFirstGetMark(false);
			}
			this.m_Slot.SetDisable(false, "");
			this.m_Slot.SetEventGet(false);
		}

		// Token: 0x0600935E RID: 37726 RVA: 0x00324B74 File Offset: 0x00322D74
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPacketSender.Send_NKMPacket_KILL_COUNT_SERVER_REWARD_REQ(this.m_iEventId, this.m_iServerCompleteStep + 1);
		}

		// Token: 0x04008046 RID: 32838
		public Text m_lbStep;

		// Token: 0x04008047 RID: 32839
		public Text m_lbDesc;

		// Token: 0x04008048 RID: 32840
		public NKCUISlot m_Slot;

		// Token: 0x04008049 RID: 32841
		private int m_iEventId;

		// Token: 0x0400804A RID: 32842
		private int m_iStep;

		// Token: 0x0400804B RID: 32843
		private int m_iServerCompleteStep;
	}
}
