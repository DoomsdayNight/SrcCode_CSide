using System;
using TMPro;
using UnityEngine;

namespace NKC.UI.HUD
{
	// Token: 0x02000C48 RID: 3144
	public class NKCUIHudRespawnCostAddEvent : MonoBehaviour
	{
		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x0600928F RID: 37519 RVA: 0x003206D9 File Offset: 0x0031E8D9
		public bool Idle
		{
			get
			{
				return !base.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x06009290 RID: 37520 RVA: 0x003206EC File Offset: 0x0031E8EC
		public void Play(float value)
		{
			base.gameObject.SetActive(true);
			base.transform.localPosition = Vector3.zero;
			NKCUtil.SetLabelText(this.lbCost, "{0:+0.##;-0.##;0}", new object[]
			{
				value
			});
			if (this.animator != null)
			{
				this.animator.Rebind();
				this.animator.Update(0f);
			}
		}

		// Token: 0x06009291 RID: 37521 RVA: 0x00320760 File Offset: 0x0031E960
		private void Update()
		{
			if (this.animator == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				base.gameObject.SetActive(false);
				return;
			}
		}

		// Token: 0x04007F8E RID: 32654
		public TextMeshProUGUI lbCost;

		// Token: 0x04007F8F RID: 32655
		public Animator animator;
	}
}
