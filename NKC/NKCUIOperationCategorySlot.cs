using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A02 RID: 2562
	public class NKCUIOperationCategorySlot : MonoBehaviour
	{
		// Token: 0x06006FCA RID: 28618 RVA: 0x0024F51F File Offset: 0x0024D71F
		public EPISODE_GROUP GetEpisodeGroup()
		{
			return this.m_EpisodeGroup;
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x0024F528 File Offset: 0x0024D728
		public void InitUI(EPISODE_GROUP category, NKCUIOperationCategorySlot.OnSlotSelect onSlotSelect)
		{
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTgl));
			this.m_tgl.m_bGetCallbackWhileLocked = true;
			this.m_dOnSlotSelect = onSlotSelect;
			this.m_EpisodeGroup = category;
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x0024F57B File Offset: 0x0024D77B
		public void UpdateTglState()
		{
			if (NKCContentManager.IsContentsUnlocked(this.GetContentsType(this.m_EpisodeGroup), 0, 0))
			{
				this.m_tgl.UnLock(false);
				return;
			}
			this.m_tgl.Lock(false);
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x0024F5AB File Offset: 0x0024D7AB
		private ContentsType GetContentsType(EPISODE_GROUP group)
		{
			switch (group)
			{
			case EPISODE_GROUP.EG_SUMMARY:
				return ContentsType.OPERATION_SUMMARY;
			case EPISODE_GROUP.EG_MAINSTREAM:
				return ContentsType.EPISODE;
			case EPISODE_GROUP.EG_SUBSTREAM:
				return ContentsType.OPERATION_SUBSTREAM;
			case EPISODE_GROUP.EG_GROWTH:
				return ContentsType.OPERATION_GROWTH;
			case EPISODE_GROUP.EG_CHALLENGE:
				return ContentsType.OPERATION_CHALLENGE;
			default:
				return ContentsType.None;
			}
		}

		// Token: 0x06006FCE RID: 28622 RVA: 0x0024F5D8 File Offset: 0x0024D7D8
		public void SetSelected(bool bValue)
		{
			this.m_tgl.Select(bValue, true, true);
			if (bValue)
			{
				this.m_Ani.SetTrigger("ON");
				return;
			}
			this.m_Ani.SetTrigger("OFF");
		}

		// Token: 0x06006FCF RID: 28623 RVA: 0x0024F60D File Offset: 0x0024D80D
		public void ChangeSelected(bool bValue)
		{
			this.m_tgl.Select(bValue, true, true);
			if (bValue)
			{
				this.m_Ani.SetTrigger("OFF_TO_ON");
				return;
			}
			this.m_Ani.SetTrigger("ON_TO_OFF");
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x0024F642 File Offset: 0x0024D842
		public void OnTgl(bool bValue)
		{
			if (this.m_tgl.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(this.GetContentsType(this.m_EpisodeGroup), 0);
				return;
			}
			NKCUIOperationCategorySlot.OnSlotSelect dOnSlotSelect = this.m_dOnSlotSelect;
			if (dOnSlotSelect == null)
			{
				return;
			}
			dOnSlotSelect(this.m_EpisodeGroup, true);
		}

		// Token: 0x04005B65 RID: 23397
		public NKCUIComToggle m_tgl;

		// Token: 0x04005B66 RID: 23398
		public Animator m_Ani;

		// Token: 0x04005B67 RID: 23399
		private NKCUIOperationCategorySlot.OnSlotSelect m_dOnSlotSelect;

		// Token: 0x04005B68 RID: 23400
		private EPISODE_GROUP m_EpisodeGroup;

		// Token: 0x0200173F RID: 5951
		// (Invoke) Token: 0x0600B2B6 RID: 45750
		public delegate void OnSlotSelect(EPISODE_GROUP catgory, bool bShowFade);
	}
}
