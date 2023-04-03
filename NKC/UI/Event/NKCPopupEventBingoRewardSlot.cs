using System;
using System.Collections.Generic;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC4 RID: 3012
	public class NKCPopupEventBingoRewardSlot : MonoBehaviour
	{
		// Token: 0x06008B66 RID: 35686 RVA: 0x002F6684 File Offset: 0x002F4884
		public void Init(NKCPopupEventBingoRewardSlot.OnTouchComplete onTouchComplete)
		{
			this.dOnTouchComplete = onTouchComplete;
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					this.m_listSlot[i].Init();
				}
			}
			if (this.m_btnComplete != null)
			{
				this.m_btnComplete.PointerClick.RemoveAllListeners();
				this.m_btnComplete.PointerClick.AddListener(new UnityAction(this.OnTouchCompleteBtn));
			}
		}

		// Token: 0x06008B67 RID: 35687 RVA: 0x002F6704 File Offset: 0x002F4904
		public void SetData(NKMEventBingoRewardTemplet rewardTemplet, int currentBingoCount, bool bComplete)
		{
			this.m_rewardTemplet = rewardTemplet;
			NKCUtil.SetLabelText(this.m_txtTitle, NKCUtilString.GET_STRING_EVENT_BINGO_REWARD_TITLE, new object[]
			{
				rewardTemplet.m_BingoCompletTypeValue
			});
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					NKCUISlot nkcuislot = this.m_listSlot[i];
					if (i < rewardTemplet.rewards.Count)
					{
						NKMRewardInfo nkmrewardInfo = rewardTemplet.rewards[i];
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count, 0);
						nkcuislot.SetData(data, true, null);
						NKCUtil.SetGameobjectActive(nkcuislot, true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcuislot, false);
					}
				}
			}
			bool flag = !bComplete && currentBingoCount >= rewardTemplet.m_BingoCompletTypeValue;
			NKCUtil.SetGameobjectActive(this.m_objClear, flag);
			NKCUtil.SetGameobjectActive(this.m_objComplete, bComplete);
			NKCUtil.SetGameobjectActive(this.m_btnComplete, flag);
			NKCUtil.SetGameobjectActive(this.m_btnDisable, !flag);
			NKCUtil.SetLabelText(this.m_txtButton, bComplete ? NKCUtilString.GET_STRING_EVENT_BINGO_REWARD_SLOT_COMPLETE : NKCUtilString.GET_STRING_EVENT_BINGO_REWARD_SLOT_PROGRESS);
		}

		// Token: 0x06008B68 RID: 35688 RVA: 0x002F6816 File Offset: 0x002F4A16
		private void OnTouchCompleteBtn()
		{
			NKCPopupEventBingoRewardSlot.OnTouchComplete onTouchComplete = this.dOnTouchComplete;
			if (onTouchComplete == null)
			{
				return;
			}
			onTouchComplete(this.m_rewardTemplet);
		}

		// Token: 0x0400783A RID: 30778
		public Text m_txtTitle;

		// Token: 0x0400783B RID: 30779
		public List<NKCUISlot> m_listSlot;

		// Token: 0x0400783C RID: 30780
		public GameObject m_objClear;

		// Token: 0x0400783D RID: 30781
		public GameObject m_objComplete;

		// Token: 0x0400783E RID: 30782
		public NKCUIComButton m_btnComplete;

		// Token: 0x0400783F RID: 30783
		public NKCUIComButton m_btnDisable;

		// Token: 0x04007840 RID: 30784
		public Text m_txtButton;

		// Token: 0x04007841 RID: 30785
		private NKCPopupEventBingoRewardSlot.OnTouchComplete dOnTouchComplete;

		// Token: 0x04007842 RID: 30786
		private NKMEventBingoRewardTemplet m_rewardTemplet;

		// Token: 0x02001994 RID: 6548
		// (Invoke) Token: 0x0600B958 RID: 47448
		public delegate void OnTouchComplete(NKMEventBingoRewardTemplet rewardTemplet);
	}
}
