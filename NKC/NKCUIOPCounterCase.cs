using System;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007CD RID: 1997
	public class NKCUIOPCounterCase : MonoBehaviour
	{
		// Token: 0x06004EE0 RID: 20192 RVA: 0x0017CF36 File Offset: 0x0017B136
		public static NKCUIOPCounterCase GetInstance()
		{
			return NKCUIOPCounterCase.m_scNKCUIOPCounterCase;
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0017CF3D File Offset: 0x0017B13D
		public void InitUI()
		{
			NKCUIOPCounterCase.m_scNKCUIOPCounterCase = base.gameObject.GetComponent<NKCUIOPCounterCase>();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0017CF5C File Offset: 0x0017B15C
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKMEpisodeTempletV2 episodeTemplet = NKMEpisodeTempletV2.Find(50, EPISODE_DIFFICULTY.NORMAL);
			this.m_COUNTERCASE_NORMAL.SetData(episodeTemplet, ContentsType.COUNTERCASE, new NKCUIOPCounterCaseSlot.OnClick(this.OnClickCounterCaseNormal));
			episodeTemplet = NKMEpisodeTempletV2.Find(51, EPISODE_DIFFICULTY.NORMAL);
			this.m_COUNTERCASE_SECRET.SetData(episodeTemplet, ContentsType.COUNTERCASE_SECRET, new NKCUIOPCounterCaseSlot.OnClick(this.OnClickCounterCaseSecret));
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0017CFBD File Offset: 0x0017B1BD
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0017CFCB File Offset: 0x0017B1CB
		private void SelectEP(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			if (!NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.GetScenManager().GetMyUserData(), cNKMEpisodeTemplet))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.COUNTERCASE_SECRET, 0);
			}
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0017CFEC File Offset: 0x0017B1EC
		public void OnClickCounterCaseNormal()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.COUNTERCASE, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.COUNTERCASE, 0);
				return;
			}
			this.SelectEP(NKMEpisodeTempletV2.Find(50, EPISODE_DIFFICULTY.NORMAL));
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0017D011 File Offset: 0x0017B211
		public void OnClickCounterCaseSecret()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.COUNTERCASE_SECRET, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.COUNTERCASE_SECRET, 0);
				return;
			}
			this.SelectEP(NKMEpisodeTempletV2.Find(51, EPISODE_DIFFICULTY.NORMAL));
		}

		// Token: 0x04003EC1 RID: 16065
		private readonly Color DISABLED_COLOR = new Color(1f, 0.8117647f, 0.23137255f);

		// Token: 0x04003EC2 RID: 16066
		private readonly Color ENABLED_COLOR = Color.white;

		// Token: 0x04003EC3 RID: 16067
		private static NKCUIOPCounterCase m_scNKCUIOPCounterCase;

		// Token: 0x04003EC4 RID: 16068
		public NKCUIOPCounterCaseSlot m_COUNTERCASE_NORMAL;

		// Token: 0x04003EC5 RID: 16069
		public NKCUIOPCounterCaseSlot m_COUNTERCASE_SECRET;

		// Token: 0x02001489 RID: 5257
		public enum NKC_COUNTER_CASE_TYPE
		{
			// Token: 0x04009E63 RID: 40547
			NDMT_NORMAL,
			// Token: 0x04009E64 RID: 40548
			NDMT_SECRET,
			// Token: 0x04009E65 RID: 40549
			NDMT_COUNT
		}
	}
}
