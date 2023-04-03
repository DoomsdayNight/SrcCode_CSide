using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007CE RID: 1998
	public class NKCUIOPCounterCaseSlot : MonoBehaviour
	{
		// Token: 0x06004EE9 RID: 20201 RVA: 0x0017D068 File Offset: 0x0017B268
		private void InitUI()
		{
			if (this.m_NKCUIOPCounterCaseSlot != null)
			{
				this.m_NKCUIOPCounterCaseSlot.PointerClick.RemoveAllListeners();
				this.m_NKCUIOPCounterCaseSlot.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			}
			this.m_bInitComplete = true;
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x0017D0B8 File Offset: 0x0017B2B8
		public void SetData(NKMEpisodeTempletV2 episodeTemplet, ContentsType contentsType, NKCUIOPCounterCaseSlot.OnClick onClick)
		{
			if (episodeTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_SLOT_GAUGE.gameObject, false);
				this.m_NKM_UI_COUNTER_CASE_SLOT_GAUGE_TEXT_NUMBER.text = "";
				return;
			}
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			this.dOnClick = onClick;
			bool flag = NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.GetScenManager().GetMyUserData(), episodeTemplet);
			NKCUtil.SetGameobjectActive(this.m_objLock, !flag);
			NKCUtil.SetGameobjectActive(this.m_lbLockText, !NKCContentManager.IsContentsUnlocked(contentsType, 0, 0));
			NKCUtil.SetLabelText(this.m_lbLockText, NKCContentManager.GetLockedMessage(contentsType, 0));
			NKCUtil.SetLabelText(this.m_NKM_UI_COUNTER_CASE_SLOT_TITLE, episodeTemplet.GetEpisodeName());
			NKCUtil.SetGameobjectActive(this.m_objRedDot, NKCContentManager.CheckNewCounterCase(episodeTemplet));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_SLOT_GAUGE.gameObject, false);
			this.m_NKM_UI_COUNTER_CASE_SLOT_GAUGE_TEXT_NUMBER.text = "";
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0017D18C File Offset: 0x0017B38C
		private void OnClickBtn()
		{
			NKCUIOPCounterCaseSlot.OnClick onClick = this.dOnClick;
			if (onClick == null)
			{
				return;
			}
			onClick();
		}

		// Token: 0x04003EC6 RID: 16070
		public NKCUIComButton m_NKCUIOPCounterCaseSlot;

		// Token: 0x04003EC7 RID: 16071
		public Slider m_NKM_UI_COUNTER_CASE_SLOT_GAUGE;

		// Token: 0x04003EC8 RID: 16072
		public Text m_NKM_UI_COUNTER_CASE_SLOT_GAUGE_TEXT_NUMBER;

		// Token: 0x04003EC9 RID: 16073
		public Text m_NKM_UI_COUNTER_CASE_SLOT_TITLE;

		// Token: 0x04003ECA RID: 16074
		public GameObject m_objLock;

		// Token: 0x04003ECB RID: 16075
		public Text m_lbLockText;

		// Token: 0x04003ECC RID: 16076
		public GameObject m_objRedDot;

		// Token: 0x04003ECD RID: 16077
		private NKCUIOPCounterCaseSlot.OnClick dOnClick;

		// Token: 0x04003ECE RID: 16078
		private bool m_bInitComplete;

		// Token: 0x0200148A RID: 5258
		// (Invoke) Token: 0x0600A926 RID: 43302
		public delegate void OnClick();
	}
}
