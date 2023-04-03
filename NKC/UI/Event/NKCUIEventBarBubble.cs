using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD0 RID: 3024
	public class NKCUIEventBarBubble : MonoBehaviour
	{
		// Token: 0x06008C2D RID: 35885 RVA: 0x002FACE9 File Offset: 0x002F8EE9
		public void Init()
		{
			this.m_position = base.transform.position;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnButton, new UnityAction(this.OnClickBubble));
		}

		// Token: 0x06008C2E RID: 35886 RVA: 0x002FAD14 File Offset: 0x002F8F14
		public void Show(int cocktailID, bool hideStart, NKCUIEventBarBubble.OnTouchBubble onTouchBubble)
		{
			this.m_timer = 0f;
			if (this.m_tween != null)
			{
				this.m_tween.Kill(false);
				this.m_tween = null;
			}
			base.transform.position = this.m_position;
			float endValue = this.m_position.y + this.m_moveYValue;
			this.m_tween = base.transform.DOMoveY(endValue, this.m_moveYDuration, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
			Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(cocktailID);
			NKCUtil.SetImageSprite(this.m_imgCocktail, orLoadMiscItemIcon, true);
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(cocktailID);
			int num = (nkmeventBarTemplet != null) ? nkmeventBarTemplet.DeliveryValue : 0;
			NKCUtil.SetLabelText(this.m_lbDeliveryCount, string.Format("x{0}", num));
			base.gameObject.SetActive(true);
			NKCUIComStateButton csbtnButton = this.m_csbtnButton;
			if (csbtnButton != null)
			{
				csbtnButton.SetLock(hideStart, false);
			}
			this.m_onTouchBubble = onTouchBubble;
			this.m_hideStart = hideStart;
			CanvasGroup canvasGroup = this.m_canvasGroup;
			NKCUtil.SetGameobjectActive((canvasGroup != null) ? canvasGroup.gameObject : null, !this.m_hideStart);
		}

		// Token: 0x06008C2F RID: 35887 RVA: 0x002FAE21 File Offset: 0x002F9021
		public void Hide()
		{
			if (this.m_tween != null)
			{
				this.m_tween.Kill(false);
				this.m_tween = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008C30 RID: 35888 RVA: 0x002FAE4C File Offset: 0x002F904C
		public void ResetAnimation()
		{
			if (!base.gameObject.activeSelf || this.m_aniBubble == null || this.m_csbtnButton == null || !this.m_csbtnButton.m_bLock)
			{
				return;
			}
			this.m_aniBubble.Rebind();
			this.m_aniBubble.Update(0f);
			this.m_timer = 0f;
			this.m_csbtnButton.SetLock(false, false);
			this.m_hideStart = false;
			CanvasGroup canvasGroup = this.m_canvasGroup;
			NKCUtil.SetGameobjectActive((canvasGroup != null) ? canvasGroup.gameObject : null, true);
		}

		// Token: 0x06008C31 RID: 35889 RVA: 0x002FAEE4 File Offset: 0x002F90E4
		private void Update()
		{
			if (this.m_aniBubble == null)
			{
				return;
			}
			if (!this.m_hideStart)
			{
				if (this.m_aniBubble.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
				{
					NKCUIComStateButton csbtnButton = this.m_csbtnButton;
					if (csbtnButton != null)
					{
						csbtnButton.SetLock(true, false);
					}
					this.m_timer += Time.deltaTime;
					if (this.m_timer >= this.m_hideTime)
					{
						this.ResetAnimation();
					}
				}
				return;
			}
			this.m_timer += Time.deltaTime;
			if (this.m_timer >= this.m_hideTime)
			{
				CanvasGroup canvasGroup = this.m_canvasGroup;
				NKCUtil.SetGameobjectActive((canvasGroup != null) ? canvasGroup.gameObject : null, true);
				this.ResetAnimation();
			}
		}

		// Token: 0x06008C32 RID: 35890 RVA: 0x002FAF9C File Offset: 0x002F919C
		private void OnClickBubble()
		{
			if (this.m_onTouchBubble != null)
			{
				this.m_onTouchBubble();
			}
			if (this.m_aniBubble == null)
			{
				return;
			}
			this.m_aniBubble.Rebind();
			this.m_aniBubble.Play("EVENT_GREMORY_BAR_BUBBLE_INTRO", 0, this.m_rewindAniTime);
			this.m_timer = 0f;
		}

		// Token: 0x040078F2 RID: 30962
		public Animator m_aniBubble;

		// Token: 0x040078F3 RID: 30963
		public CanvasGroup m_canvasGroup;

		// Token: 0x040078F4 RID: 30964
		public Image m_imgCocktail;

		// Token: 0x040078F5 RID: 30965
		public Text m_lbDeliveryCount;

		// Token: 0x040078F6 RID: 30966
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x040078F7 RID: 30967
		public float m_hideTime;

		// Token: 0x040078F8 RID: 30968
		public float m_moveYValue;

		// Token: 0x040078F9 RID: 30969
		public float m_moveYDuration;

		// Token: 0x040078FA RID: 30970
		public float m_rewindAniTime;

		// Token: 0x040078FB RID: 30971
		private Vector3 m_position;

		// Token: 0x040078FC RID: 30972
		private float m_timer;

		// Token: 0x040078FD RID: 30973
		private Tween m_tween;

		// Token: 0x040078FE RID: 30974
		private bool m_hideStart;

		// Token: 0x040078FF RID: 30975
		private NKCUIEventBarBubble.OnTouchBubble m_onTouchBubble;

		// Token: 0x020019A6 RID: 6566
		// (Invoke) Token: 0x0600B996 RID: 47510
		public delegate void OnTouchBubble();
	}
}
