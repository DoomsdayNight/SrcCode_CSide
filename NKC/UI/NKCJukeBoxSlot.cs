using System;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x0200096B RID: 2411
	public class NKCJukeBoxSlot : MonoBehaviour
	{
		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x0600611A RID: 24858 RVA: 0x001E69E2 File Offset: 0x001E4BE2
		public int Index
		{
			get
			{
				return this.m_idx;
			}
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x001E69EA File Offset: 0x001E4BEA
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnClick, new UnityAction(this.OnClickSlot));
			this.m_btnStat = NKCJukeBoxSlot.ButtonStat.NORMAL;
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x001E6A0C File Offset: 0x001E4C0C
		public void SetData(NKCBGMInfoTemplet bgmTemplet, NKCJukeBoxSlot.OnClick callback, bool bFavorite, bool bOpen, bool bNew)
		{
			if (!bOpen)
			{
				this.SetButtonStat(NKCJukeBoxSlot.ButtonStat.LOCK);
				NKCUtil.SetGameobjectActive(this.m_objNew, false);
				this.SetFavorite(false);
				return;
			}
			if (bgmTemplet == null)
			{
				return;
			}
			this.SetButtonStat(NKCJukeBoxSlot.ButtonStat.NORMAL);
			this.SetFavorite(bFavorite);
			NKCUtil.SetGameobjectActive(this.m_objNew, bNew);
			NKCComText[] lbTitle = this.m_lbTitle;
			for (int i = 0; i < lbTitle.Length; i++)
			{
				NKCUtil.SetLabelText(lbTitle[i], NKCStringTable.GetString(bgmTemplet.m_BgmNameStringID, false));
			}
			AudioClip audioClip = NKCUIJukeBox.GetAudioClip(bgmTemplet.m_BgmAssetID);
			if (null != audioClip)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)audioClip.length);
				NKCUtil.SetLabelText(this.m_lbTime, string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds));
			}
			this.SetStat(false);
			this.m_idx = bgmTemplet.Key;
			this.dClick = callback;
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x001E6AE9 File Offset: 0x001E4CE9
		public void SetStat(bool bPlay)
		{
			if (this.m_btnStat == NKCJukeBoxSlot.ButtonStat.LOCK)
			{
				return;
			}
			if (bPlay)
			{
				this.SetButtonStat(NKCJukeBoxSlot.ButtonStat.SELECTED);
				return;
			}
			this.SetButtonStat(NKCJukeBoxSlot.ButtonStat.NORMAL);
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x001E6B07 File Offset: 0x001E4D07
		public void SetFavorite(bool bSet)
		{
			if (bSet && this.m_btnStat == NKCJukeBoxSlot.ButtonStat.LOCK)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objFavorite, bSet);
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x001E6B22 File Offset: 0x001E4D22
		private void OnClickSlot()
		{
			if (this.m_btnStat == NKCJukeBoxSlot.ButtonStat.LOCK)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_JUKEBOX_BLOCK_SLOT_MSG, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCJukeBoxSlot.OnClick onClick = this.dClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this.m_idx);
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x001E6B57 File Offset: 0x001E4D57
		private void SetButtonStat(NKCJukeBoxSlot.ButtonStat stat)
		{
			NKCUtil.SetGameobjectActive(this.m_objBtnStatNormal, stat == NKCJukeBoxSlot.ButtonStat.NORMAL);
			NKCUtil.SetGameobjectActive(this.m_objBtnStatSelected, stat == NKCJukeBoxSlot.ButtonStat.SELECTED);
			NKCUtil.SetGameobjectActive(this.m_objBtnStatLock, stat == NKCJukeBoxSlot.ButtonStat.LOCK);
			this.m_btnStat = stat;
		}

		// Token: 0x04004D4A RID: 19786
		public NKCUIComStateButton m_csbtnClick;

		// Token: 0x04004D4B RID: 19787
		public NKCComText[] m_lbTitle;

		// Token: 0x04004D4C RID: 19788
		public NKCComText m_lbTime;

		// Token: 0x04004D4D RID: 19789
		public GameObject m_objFavorite;

		// Token: 0x04004D4E RID: 19790
		public GameObject m_objNew;

		// Token: 0x04004D4F RID: 19791
		[Header("버튼 상태")]
		public GameObject m_objBtnStatNormal;

		// Token: 0x04004D50 RID: 19792
		public GameObject m_objBtnStatSelected;

		// Token: 0x04004D51 RID: 19793
		public GameObject m_objBtnStatLock;

		// Token: 0x04004D52 RID: 19794
		private NKCJukeBoxSlot.ButtonStat m_btnStat;

		// Token: 0x04004D53 RID: 19795
		private NKCJukeBoxSlot.OnClick dClick;

		// Token: 0x04004D54 RID: 19796
		private int m_idx;

		// Token: 0x02001604 RID: 5636
		// (Invoke) Token: 0x0600AEF6 RID: 44790
		public delegate void OnClick(int idx);

		// Token: 0x02001605 RID: 5637
		private enum ButtonStat
		{
			// Token: 0x0400A2E9 RID: 41705
			NORMAL,
			// Token: 0x0400A2EA RID: 41706
			LOCK,
			// Token: 0x0400A2EB RID: 41707
			SELECTED
		}
	}
}
