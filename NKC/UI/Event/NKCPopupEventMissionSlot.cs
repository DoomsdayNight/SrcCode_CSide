using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC7 RID: 3015
	public class NKCPopupEventMissionSlot : MonoBehaviour
	{
		// Token: 0x06008B86 RID: 35718 RVA: 0x002F6CDC File Offset: 0x002F4EDC
		public void Init(NKCPopupEventMissionSlot.OnTouchProgress onTouchProgress, NKCPopupEventMissionSlot.OnTouchComplete onTouchComplete)
		{
			if (this.m_btnComplete != null)
			{
				this.m_btnComplete.PointerClick.RemoveAllListeners();
				this.m_btnComplete.PointerClick.AddListener(new UnityAction(this.OnTouchCompleteBtn));
			}
			if (this.m_btnProgress != null)
			{
				this.m_btnProgress.PointerClick.RemoveAllListeners();
				this.m_btnProgress.PointerClick.AddListener(new UnityAction(this.OnTouchProgressBtn));
			}
			if (this.m_btnDisable != null)
			{
				this.m_btnDisable.PointerClick.RemoveAllListeners();
				this.m_btnDisable.PointerClick.AddListener(new UnityAction(this.OnTouchDisableBtn));
			}
			this.dOnTouchProgress = onTouchProgress;
			this.dOnTouchComplete = onTouchComplete;
		}

		// Token: 0x06008B87 RID: 35719 RVA: 0x002F6DA8 File Offset: 0x002F4FA8
		public void SetData(NKMMissionTemplet missionTemplet, NKMMissionData missionData)
		{
			this.m_missionTemplet = missionTemplet;
			this.m_missionData = missionData;
			NKCUtil.SetLabelText(this.m_txtTitle, missionTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_txtExplain, missionTemplet.GetDesc());
			bool flag = missionData != null && NKMMissionManager.CanComplete(missionTemplet, NKCScenManager.CurrentUserData(), missionData) == NKM_ERROR_CODE.NEC_OK;
			bool flag2 = missionData != null && missionData.isComplete;
			NKCUtil.SetGameobjectActive(this.m_objClear, flag);
			NKCUtil.SetGameobjectActive(this.m_objComplete, flag2);
			long num = 0L;
			if (missionData != null)
			{
				if (flag || flag2)
				{
					num = missionTemplet.m_Times;
				}
				else
				{
					num = missionData.times;
				}
			}
			if (this.m_slider != null)
			{
				if (missionTemplet.m_Times > 0L)
				{
					this.m_slider.value = (float)num / (float)missionTemplet.m_Times;
				}
				else
				{
					this.m_slider.value = 0f;
				}
			}
			NKCUtil.SetLabelText(this.m_txtGauge, flag ? "" : string.Format("{0}/{1}", num, missionTemplet.m_Times));
			NKCUtil.SetGameobjectActive(this.m_btnProgress, !flag && !flag2);
			NKCUtil.SetGameobjectActive(this.m_btnComplete, flag);
			NKCUtil.SetGameobjectActive(this.m_btnDisable, flag2);
		}

		// Token: 0x06008B88 RID: 35720 RVA: 0x002F6ED7 File Offset: 0x002F50D7
		private void OnTouchCompleteBtn()
		{
			NKCPopupEventMissionSlot.OnTouchComplete onTouchComplete = this.dOnTouchComplete;
			if (onTouchComplete == null)
			{
				return;
			}
			onTouchComplete(this.m_missionTemplet, this.m_missionData);
		}

		// Token: 0x06008B89 RID: 35721 RVA: 0x002F6EF5 File Offset: 0x002F50F5
		private void OnTouchProgressBtn()
		{
			NKCPopupEventMissionSlot.OnTouchProgress onTouchProgress = this.dOnTouchProgress;
			if (onTouchProgress == null)
			{
				return;
			}
			onTouchProgress(this.m_missionTemplet, this.m_missionData);
		}

		// Token: 0x06008B8A RID: 35722 RVA: 0x002F6F13 File Offset: 0x002F5113
		private void OnTouchDisableBtn()
		{
		}

		// Token: 0x04007852 RID: 30802
		public Text m_txtTitle;

		// Token: 0x04007853 RID: 30803
		public Text m_txtExplain;

		// Token: 0x04007854 RID: 30804
		public GameObject m_objClear;

		// Token: 0x04007855 RID: 30805
		public GameObject m_objComplete;

		// Token: 0x04007856 RID: 30806
		[Header("진행도")]
		public Text m_txtGauge;

		// Token: 0x04007857 RID: 30807
		public Slider m_slider;

		// Token: 0x04007858 RID: 30808
		[Header("버튼")]
		public NKCUIComButton m_btnComplete;

		// Token: 0x04007859 RID: 30809
		public NKCUIComButton m_btnProgress;

		// Token: 0x0400785A RID: 30810
		public NKCUIComButton m_btnDisable;

		// Token: 0x0400785B RID: 30811
		private NKCPopupEventMissionSlot.OnTouchProgress dOnTouchProgress;

		// Token: 0x0400785C RID: 30812
		private NKCPopupEventMissionSlot.OnTouchComplete dOnTouchComplete;

		// Token: 0x0400785D RID: 30813
		private NKMMissionTemplet m_missionTemplet;

		// Token: 0x0400785E RID: 30814
		private NKMMissionData m_missionData;

		// Token: 0x02001997 RID: 6551
		// (Invoke) Token: 0x0600B964 RID: 47460
		public delegate void OnTouchProgress(NKMMissionTemplet templet, NKMMissionData data);

		// Token: 0x02001998 RID: 6552
		// (Invoke) Token: 0x0600B968 RID: 47464
		public delegate void OnTouchComplete(NKMMissionTemplet templet, NKMMissionData data);
	}
}
