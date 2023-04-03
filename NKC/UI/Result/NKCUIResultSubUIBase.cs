using System;
using System.Collections;
using UnityEngine;

namespace NKC.UI.Result
{
	// Token: 0x02000B9E RID: 2974
	public abstract class NKCUIResultSubUIBase : MonoBehaviour
	{
		// Token: 0x06008990 RID: 35216
		protected abstract IEnumerator InnerProcess(bool bAutoSkip);

		// Token: 0x06008991 RID: 35217
		public abstract bool IsProcessFinished();

		// Token: 0x06008992 RID: 35218
		public abstract void FinishProcess();

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06008993 RID: 35219 RVA: 0x002EA7CF File Offset: 0x002E89CF
		// (set) Token: 0x06008994 RID: 35220 RVA: 0x002EA7D7 File Offset: 0x002E89D7
		public bool ProcessRequired { get; set; }

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06008995 RID: 35221 RVA: 0x002EA7E0 File Offset: 0x002E89E0
		public RectTransform RectTransform
		{
			get
			{
				if (this.m_RectTransform == null)
				{
					this.m_RectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_RectTransform;
			}
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x002EA802 File Offset: 0x002E8A02
		public virtual void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x002EA810 File Offset: 0x002E8A10
		public IEnumerator Process(bool bAutoSkip = false)
		{
			if (!base.gameObject.activeSelf)
			{
				yield break;
			}
			base.StartCoroutine(this.InnerProcess(bAutoSkip));
			this.m_bPause = false;
			this.m_bHadUserInput = false;
			while (!this.IsProcessFinished())
			{
				if (this.m_bHadUserInput)
				{
					this.FinishProcess();
					break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06008998 RID: 35224 RVA: 0x002EA826 File Offset: 0x002E8A26
		public virtual void OnUserInput()
		{
			this.m_bHadUserInput = true;
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x002EA82F File Offset: 0x002E8A2F
		public void SetPause(bool bValue)
		{
			this.m_bPause = bValue;
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x002EA838 File Offset: 0x002E8A38
		public void SetReserveCountdown(bool bValue)
		{
			this.m_bWillPlayCountdown = bValue;
		}

		// Token: 0x0600899B RID: 35227 RVA: 0x002EA841 File Offset: 0x002E8A41
		public IEnumerator PlayCloseAnimation(Animator animator)
		{
			if (animator == null)
			{
				yield break;
			}
			NKCSoundManager.PlaySound("FX_UI_TITLE_OUT_TEST", 1f, base.transform.position.x, 0f, false, 0f, false, 0f);
			animator.enabled = true;
			animator.Play("OUTRO");
			yield return this.WaitAniOrInput(animator);
			yield break;
		}

		// Token: 0x0600899C RID: 35228 RVA: 0x002EA857 File Offset: 0x002E8A57
		public IEnumerator WaitAniOrInput(Animator animator)
		{
			this.m_bHadUserInput = false;
			float deltaTime = 0f;
			if (!(animator == null))
			{
				for (;;)
				{
					yield return null;
					if (this.m_bHadUserInput)
					{
						break;
					}
					if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
					{
						goto Block_6;
					}
					if (!this.m_bIgnoreAutoClose)
					{
						deltaTime += Time.deltaTime;
						if (deltaTime > 5f)
						{
							goto IL_11E;
						}
					}
				}
				yield break;
				Block_6:
				yield break;
			}
			yield return new WaitForSeconds(0.1f);
			while (!this.m_bHadUserInput)
			{
				yield return null;
				if (!this.m_bIgnoreAutoClose)
				{
					deltaTime += Time.deltaTime;
					if (deltaTime > 5f)
					{
						yield break;
					}
				}
			}
			IL_11E:
			yield break;
		}

		// Token: 0x0600899D RID: 35229 RVA: 0x002EA86D File Offset: 0x002E8A6D
		protected IEnumerator WaitTimeOrUserInput(float waitTime = 5f)
		{
			float currentTime = 0f;
			this.m_bHadUserInput = false;
			if (waitTime == 0f)
			{
				yield break;
			}
			if (waitTime < 0f)
			{
				while (!this.m_bHadUserInput)
				{
					yield return null;
				}
			}
			else
			{
				while (currentTime < waitTime)
				{
					currentTime += Time.deltaTime;
					if (this.m_bHadUserInput)
					{
						break;
					}
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x040075FD RID: 30205
		private const string LIST_OUT_SOUND_BUNDLE_NAME = "ab_sound";

		// Token: 0x040075FE RID: 30206
		private const string LIST_OUT_SOUND_ASSET_NAME = "FX_UI_TITLE_OUT_TEST";

		// Token: 0x040075FF RID: 30207
		private const float SKIP_DELAY = 0.1f;

		// Token: 0x04007600 RID: 30208
		private const float MAX_ANI_WAIT_TIME = 5f;

		// Token: 0x04007602 RID: 30210
		protected bool m_bIgnoreAutoClose;

		// Token: 0x04007603 RID: 30211
		protected bool m_bPause;

		// Token: 0x04007604 RID: 30212
		protected bool m_bWillPlayCountdown;

		// Token: 0x04007605 RID: 30213
		private RectTransform m_RectTransform;

		// Token: 0x04007606 RID: 30214
		protected bool m_bHadUserInput;
	}
}
