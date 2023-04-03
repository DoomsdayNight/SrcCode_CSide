using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200079B RID: 1947
	public class NKCUILobbyMenuPanel : MonoBehaviour
	{
		// Token: 0x06004C60 RID: 19552 RVA: 0x0016DE40 File Offset: 0x0016C040
		private void Awake()
		{
			if (this.m_CanvasGroup == null)
			{
				this.m_CanvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this.m_Animator == null)
			{
				this.m_Animator = base.GetComponent<Animator>();
			}
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0016DE76 File Offset: 0x0016C076
		public void PlayIntroAnimation()
		{
			if (this.m_Animator != null)
			{
				this.m_Animator.SetTrigger("Intro");
			}
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0016DE96 File Offset: 0x0016C096
		public void PlaySelectAnimation(bool bSelected)
		{
			if (this.m_Animator != null)
			{
				this.m_Animator.SetBool("Active", bSelected);
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06004C63 RID: 19555 RVA: 0x0016DEB7 File Offset: 0x0016C0B7
		// (set) Token: 0x06004C64 RID: 19556 RVA: 0x0016DED8 File Offset: 0x0016C0D8
		public float alpha
		{
			get
			{
				if (!(this.m_CanvasGroup != null))
				{
					return 0f;
				}
				return this.m_CanvasGroup.alpha;
			}
			set
			{
				if (this.m_CanvasGroup != null)
				{
					this.m_CanvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x04003C1E RID: 15390
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04003C1F RID: 15391
		public Animator m_Animator;
	}
}
