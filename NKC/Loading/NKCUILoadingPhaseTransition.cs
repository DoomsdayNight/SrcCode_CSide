using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.Loading
{
	// Token: 0x0200089B RID: 2203
	public class NKCUILoadingPhaseTransition : MonoBehaviour
	{
		// Token: 0x060057F2 RID: 22514 RVA: 0x001A5C49 File Offset: 0x001A3E49
		public void PlayIntro()
		{
			this.PlayAnimation("INTRO");
			NKCUtil.SetGameobjectActive(this.m_objProgress, false);
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x001A5C62 File Offset: 0x001A3E62
		public void PlayIdle()
		{
			this.PlayAnimation("IDLE");
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x001A5C6F File Offset: 0x001A3E6F
		public void PlayOutro()
		{
			this.PlayAnimation("OUTRO");
			NKCUtil.SetGameobjectActive(this.m_objProgress, false);
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x001A5C88 File Offset: 0x001A3E88
		private void PlayAnimation(string name)
		{
			if (this.m_Animator != null)
			{
				this.m_Animator.Play(name, -1, 0f);
				this.m_Animator.Update(0.001f);
			}
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x001A5CBC File Offset: 0x001A3EBC
		public bool IsAnimFinished()
		{
			if (this.m_Animator == null)
			{
				return true;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
			return !currentAnimatorStateInfo.loop && currentAnimatorStateInfo.normalizedTime > 1f;
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x001A5D00 File Offset: 0x001A3F00
		public void SetLoadingProgress(float fProgress)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				NKCUtil.SetGameobjectActive(this.m_objProgress, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objProgress, true);
			NKCUtil.SetLabelText(this.m_lbProgress, NKCUtilString.GET_STRING_ATTACK_PREPARING);
			NKCUtil.SetLabelText(this.m_lbProgressCount, string.Format("{0}%", (int)(fProgress * 100f)));
			this.m_imgProgress.fillAmount = fProgress;
		}

		// Token: 0x0400456C RID: 17772
		private const string INTRO_ANIM_NAME = "INTRO";

		// Token: 0x0400456D RID: 17773
		private const string IDLE_ANIM_NAME = "IDLE";

		// Token: 0x0400456E RID: 17774
		private const string OUTRO_ANIM_NAME = "OUTRO";

		// Token: 0x0400456F RID: 17775
		public Animator m_Animator;

		// Token: 0x04004570 RID: 17776
		public GameObject m_objProgress;

		// Token: 0x04004571 RID: 17777
		public Text m_lbProgress;

		// Token: 0x04004572 RID: 17778
		public Text m_lbProgressCount;

		// Token: 0x04004573 RID: 17779
		public Image m_imgProgress;
	}
}
