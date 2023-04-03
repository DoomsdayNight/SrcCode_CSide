using System;
using NKC.Patcher;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x020007EE RID: 2030
	public class NKCPatcherIllustController : MonoBehaviour
	{
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005083 RID: 20611 RVA: 0x00185BBE File Offset: 0x00183DBE
		private AudioSource _audioSource
		{
			get
			{
				if (NKCPatchChecker.m_instance != null)
				{
					return NKCPatchChecker.m_instance.m_audioSource;
				}
				this.m_audioSource.enabled = true;
				return this.m_audioSource;
			}
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x00185BEC File Offset: 0x00183DEC
		private void Awake()
		{
			NKCUtil.SetButtonClickDelegate(this.m_buttonLeft, new UnityAction(this.OnClickLeft));
			NKCUtil.SetButtonClickDelegate(this.m_buttonRight, new UnityAction(this.OnClickRight));
			this.RefreshUI();
			if (this.m_patchIllustList != null)
			{
				this.m_maxIllustCount = this.m_patchIllustList.Length;
			}
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00185C44 File Offset: 0x00183E44
		public void RefreshUI()
		{
			if (this.m_patchIllustList != null)
			{
				for (int i = 0; i < this.m_patchIllustList.Length; i++)
				{
					if (i == this.m_currentIndex)
					{
						NKCUtil.SetGameobjectActive(this.m_patchIllustList[i], false);
						NKCUtil.SetGameobjectActive(this.m_patchIllustList[i], true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_patchIllustList[i], false);
					}
				}
			}
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00185CA1 File Offset: 0x00183EA1
		private void Start()
		{
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x00185CA3 File Offset: 0x00183EA3
		private void Update()
		{
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x00185CA5 File Offset: 0x00183EA5
		public void OnClickLeft()
		{
			this.m_currentIndex--;
			if (this.m_currentIndex < 0)
			{
				this.m_currentIndex = this.m_maxIllustCount - 1;
			}
			this.PlayClick();
			this.RefreshUI();
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x00185CD8 File Offset: 0x00183ED8
		public void OnClickRight()
		{
			this.m_currentIndex++;
			if (this.m_currentIndex >= this.m_maxIllustCount)
			{
				this.m_currentIndex = 0;
			}
			this.PlayClick();
			this.RefreshUI();
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x00185D09 File Offset: 0x00183F09
		public void PlayClick()
		{
			if (this._audioSource == null)
			{
				return;
			}
			if (this.m_clickSound == null)
			{
				return;
			}
			this._audioSource.PlayOneShot(this.m_clickSound);
		}

		// Token: 0x04004077 RID: 16503
		public NKCUIComStateButton m_buttonLeft;

		// Token: 0x04004078 RID: 16504
		public NKCUIComStateButton m_buttonRight;

		// Token: 0x04004079 RID: 16505
		public GameObject[] m_patchIllustList;

		// Token: 0x0400407A RID: 16506
		public int m_currentIndex;

		// Token: 0x0400407B RID: 16507
		public int m_maxIllustCount;

		// Token: 0x0400407C RID: 16508
		public AudioClip m_clickSound;

		// Token: 0x0400407D RID: 16509
		public AudioSource m_audioSource;

		// Token: 0x0400407E RID: 16510
		public AudioClip m_ambientBGM;
	}
}
