using System;
using NKC.Patcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A30 RID: 2608
	public class NKCPopupDownloadTypeSlot : MonoBehaviour
	{
		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06007234 RID: 29236 RVA: 0x0025F50A File Offset: 0x0025D70A
		// (set) Token: 0x06007233 RID: 29235 RVA: 0x0025F501 File Offset: 0x0025D701
		public NKCPatchDownloader.DownType m_downloadType { get; private set; }

		// Token: 0x06007235 RID: 29237 RVA: 0x0025F512 File Offset: 0x0025D712
		private void Awake()
		{
			if (this.m_slotButton != null)
			{
				this.m_slotButton.PointerClick.RemoveAllListeners();
				this.m_slotButton.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x0025F550 File Offset: 0x0025D750
		public void SetData(NKCPatchDownloader.DownType downloadType, Action<NKCPatchDownloader.DownType> onClickButton)
		{
			this.m_downloadType = downloadType;
			this.m_onClick = onClickButton;
			if (this.NKM_UI_POPUP_DOWN_TITLE_TEXT != null)
			{
				this.NKM_UI_POPUP_DOWN_TITLE_TEXT.text = NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOADTYPE_TITLE_" + downloadType.ToString(), false);
			}
			if (this.NKM_UI_POPUP_DOWN_INFO_TEXT != null)
			{
				this.NKM_UI_POPUP_DOWN_INFO_TEXT.text = NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOADTYPE_DESC_" + downloadType.ToString(), false);
			}
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x0025F5D7 File Offset: 0x0025D7D7
		public void SetDownloadSizeText(string downloadSizeString)
		{
			if (this.NKM_UI_POPUP_DOWNLOAD_SIZE_TEXT != null)
			{
				this.NKM_UI_POPUP_DOWNLOAD_SIZE_TEXT.text = NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOAD_SIZE", false) + " : " + downloadSizeString;
			}
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x0025F608 File Offset: 0x0025D808
		private void OnClick()
		{
			Action<NKCPatchDownloader.DownType> onClick = this.m_onClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this.m_downloadType);
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x0025F620 File Offset: 0x0025D820
		public void SetSelect(bool active)
		{
			this.m_slotButton.Select(active, false, false);
		}

		// Token: 0x04005E1A RID: 24090
		public NKCUIComStateButton m_slotButton;

		// Token: 0x04005E1B RID: 24091
		public Text NKM_UI_POPUP_DOWN_TITLE_TEXT;

		// Token: 0x04005E1C RID: 24092
		public Text NKM_UI_POPUP_DOWNLOAD_SIZE_TEXT;

		// Token: 0x04005E1D RID: 24093
		public Text NKM_UI_POPUP_DOWN_INFO_TEXT;

		// Token: 0x04005E1E RID: 24094
		private Action<NKCPatchDownloader.DownType> m_onClick;
	}
}
