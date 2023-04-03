using System;
using System.Collections;
using System.Collections.Generic;
using NKC.Patcher;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A2F RID: 2607
	public class NKCPopupDownloadTypeSelection : NKCUIBase
	{
		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06007227 RID: 29223 RVA: 0x0025F2A1 File Offset: 0x0025D4A1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06007228 RID: 29224 RVA: 0x0025F2A4 File Offset: 0x0025D4A4
		public override string MenuName
		{
			get
			{
				return "NKCPopupDownloadTypeSelection";
			}
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x0025F2AB File Offset: 0x0025D4AB
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x0025F2B9 File Offset: 0x0025D4B9
		public void Awake()
		{
			this.Init();
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x0025F2C4 File Offset: 0x0025D4C4
		public void Init()
		{
			if (this.m_downloadTypeSlot == null)
			{
				return;
			}
			for (int i = 0; i < this.m_downloadTypeSlot.Length; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_downloadTypeSlot[i], false);
			}
			this.m_activatedDownloadTypeSelection.Clear();
			this.m_activatedDownloadTypeSelection.Add(NKCPatchDownloader.DownType.FullDownload);
			if (!NKCPatchUtility.GetTutorialClearedStatus())
			{
				this.m_activatedDownloadTypeSelection.Add(NKCPatchDownloader.DownType.TutorialWithBackground);
			}
			this.m_currentDownloadType = NKCPatchDownloader.DownType.FullDownload;
			this.RefreshUI();
			this.OkButton.PointerClick.RemoveAllListeners();
			this.OkButton.PointerClick.AddListener(new UnityAction(this.OnClickOk));
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x0025F360 File Offset: 0x0025D560
		public void RefreshUI()
		{
			if (this.m_downloadTypeSlot == null)
			{
				return;
			}
			foreach (NKCPatchDownloader.DownType downType in this.m_activatedDownloadTypeSelection)
			{
				int num = (int)downType;
				this.m_downloadTypeSlot[num].SetSelect(false);
				this.m_downloadTypeSlot[num].SetData(downType, new Action<NKCPatchDownloader.DownType>(this.ChangeToggleActive));
				NKCUtil.SetGameobjectActive(this.m_downloadTypeSlot[num], true);
				if (this.m_currentDownloadType == downType)
				{
					this.m_downloadTypeSlot[num].SetSelect(true);
				}
			}
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x0025F404 File Offset: 0x0025D604
		public void Open(Action<NKCPatchDownloader.DownType> onClickOK, List<NKCPopupDownloadTypeData> downloadTypeDataList)
		{
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x0025F408 File Offset: 0x0025D608
		public void Open(Action<NKCPatchDownloader.DownType> okPopup, float totalDownloadSize, float essentialDownloadSize, float nonEssentialDownloadSize, float tutorialDownloadSize)
		{
			this._waitForClick = true;
			NKCUIManager.OpenUI(base.gameObject);
			base.gameObject.SetActive(true);
			this.Init();
			for (int i = 0; i < 2; i++)
			{
				NKCPatchDownloader.DownType downType = (NKCPatchDownloader.DownType)i;
				string text;
				if (downType != NKCPatchDownloader.DownType.FullDownload)
				{
					if (downType != NKCPatchDownloader.DownType.TutorialWithBackground)
					{
						text = "";
					}
					else
					{
						text = string.Format("{0:F}mb + ({1:F}mb)", tutorialDownloadSize, essentialDownloadSize);
					}
				}
				else
				{
					text = string.Format("{0:F}mb", totalDownloadSize);
				}
				string downloadSizeText = text;
				this.m_downloadTypeSlot[i].SetDownloadSizeText(downloadSizeText);
			}
			if (tutorialDownloadSize <= 0f)
			{
				this.m_activatedDownloadTypeSelection.Remove(NKCPatchDownloader.DownType.TutorialWithBackground);
			}
			this._onClickOk = okPopup;
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x0025F4B1 File Offset: 0x0025D6B1
		private void ChangeToggleActive(NKCPatchDownloader.DownType downloadType)
		{
			this.m_currentDownloadType = downloadType;
			this.RefreshUI();
		}

		// Token: 0x06007230 RID: 29232 RVA: 0x0025F4C0 File Offset: 0x0025D6C0
		public void OnClickOk()
		{
			this._waitForClick = false;
			Action<NKCPatchDownloader.DownType> onClickOk = this._onClickOk;
			if (onClickOk == null)
			{
				return;
			}
			onClickOk(this.m_currentDownloadType);
		}

		// Token: 0x06007231 RID: 29233 RVA: 0x0025F4DF File Offset: 0x0025D6DF
		public IEnumerator WaitForClick()
		{
			while (this._waitForClick)
			{
				yield return null;
			}
			this.CloseInternal();
			yield break;
		}

		// Token: 0x04005E14 RID: 24084
		public NKCPopupDownloadTypeSlot[] m_downloadTypeSlot;

		// Token: 0x04005E15 RID: 24085
		private NKCPatchDownloader.DownType m_currentDownloadType;

		// Token: 0x04005E16 RID: 24086
		private Action<NKCPatchDownloader.DownType> _onClickOk;

		// Token: 0x04005E17 RID: 24087
		public NKCUIComButton OkButton;

		// Token: 0x04005E18 RID: 24088
		private bool _waitForClick;

		// Token: 0x04005E19 RID: 24089
		private List<NKCPatchDownloader.DownType> m_activatedDownloadTypeSelection = new List<NKCPatchDownloader.DownType>();
	}
}
