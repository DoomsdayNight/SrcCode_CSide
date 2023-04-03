using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000750 RID: 1872
	public class NKCUICharacterViewEffectHologram : NKCUICharacterViewEffectBase
	{
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x00166FD4 File Offset: 0x001651D4
		public override Color EffectColor { get; } = new Color(0.1058f, 0.6f, 1f, 1f);

		// Token: 0x06004AE9 RID: 19177 RVA: 0x00166FDC File Offset: 0x001651DC
		public override void SetEffect(NKCASUIUnitIllust unitIllust, Transform trOriginalRoot)
		{
			if (this.m_objRectMask == null)
			{
				this.m_objRectMask = new GameObject("Hologram RectMask", new Type[]
				{
					typeof(RectTransform),
					typeof(RectMask2D)
				});
				this.m_RectMask = this.m_objRectMask.GetComponent<RectMask2D>();
			}
			if (this.m_asHologramEffect == null)
			{
				this.m_asHologramEffect = NKCAssetResourceManager.OpenInstance<GameObject>("ab_fx_ui_hologram", "AB_FX_UI_HOLOGRAM", false, null);
			}
			RectTransform component = this.m_objRectMask.GetComponent<RectTransform>();
			component.SetParent(trOriginalRoot);
			component.anchoredPosition = Vector2.zero;
			unitIllust.SetParent(component, true);
			unitIllust.SetColor(this.EffectColor);
			unitIllust.SetEffectMaterial(NKCUICharacterView.EffectType.Hologram);
			if (this.m_asHologramEffect != null && this.m_asHologramEffect.m_Instant != null)
			{
				this.m_asHologramEffect.m_Instant.transform.SetParent(trOriginalRoot, false);
				this.canvasGroup = this.m_asHologramEffect.m_Instant.GetComponent<CanvasGroup>();
			}
			this.HologramIn();
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x001670E0 File Offset: 0x001652E0
		public void HologramIn()
		{
			RectTransform component = this.m_objRectMask.GetComponent<RectTransform>();
			component.SetSize(new Vector2(0f, 3000f));
			this.m_RectMask.enabled = true;
			if (this.m_asHologramEffect != null && this.m_asHologramEffect.m_Instant != null)
			{
				this.m_asHologramEffect.m_Instant.SetActive(true);
				Animator component2 = this.m_asHologramEffect.m_Instant.GetComponent<Animator>();
				if (component2 != null)
				{
					component2.SetTrigger("Start");
					component2.ResetTrigger("Finish");
				}
				this.m_asHologramEffect.m_Instant.transform.localPosition = Vector3.zero;
			}
			component.DOKill(false);
			component.DOSizeDelta(new Vector2(750f, 3000f), 0.33f, false).OnComplete(delegate
			{
				this.m_RectMask.enabled = false;
			});
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x001671C4 File Offset: 0x001653C4
		public void HologramOut()
		{
			if (this.m_asHologramEffect != null && this.m_asHologramEffect != null && this.m_asHologramEffect.m_Instant != null)
			{
				Animator component = this.m_asHologramEffect.m_Instant.GetComponent<Animator>();
				if (component != null)
				{
					component.ResetTrigger("Start");
					component.SetTrigger("Finish");
				}
			}
			RectTransform component2 = this.m_objRectMask.GetComponent<RectTransform>();
			this.m_RectMask.enabled = true;
			component2.DOKill(false);
			component2.DOSizeDelta(new Vector2(0f, 3000f), 0.33f, false).OnComplete(delegate
			{
				if (this.m_asHologramEffect != null && this.m_asHologramEffect.m_Instant != null)
				{
					this.m_asHologramEffect.m_Instant.SetActive(false);
				}
			});
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x00167270 File Offset: 0x00165470
		public override void CleanUp(NKCASUIUnitIllust unitIllust, Transform trOriginalRoot)
		{
			if (unitIllust != null)
			{
				unitIllust.SetColor(Color.white);
				unitIllust.SetParent(trOriginalRoot, true);
				unitIllust.SetDefaultMaterial();
			}
			if (this.m_asHologramEffect != null)
			{
				this.m_asHologramEffect.Close();
				this.m_asHologramEffect.Unload();
			}
			this.m_objRectMask.GetComponent<RectTransform>().DOKill(false);
			UnityEngine.Object.Destroy(this.m_objRectMask);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001672D4 File Offset: 0x001654D4
		public override void SetColor(Color color)
		{
			if (this.canvasGroup != null)
			{
				this.canvasGroup.alpha = color.a;
			}
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x001672F5 File Offset: 0x001654F5
		public override void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f)
		{
			if (this.canvasGroup != null && fA >= 0f)
			{
				this.canvasGroup.alpha = fA;
			}
		}

		// Token: 0x04003995 RID: 14741
		private NKCAssetInstanceData m_asHologramEffect;

		// Token: 0x04003996 RID: 14742
		private GameObject m_objRectMask;

		// Token: 0x04003997 RID: 14743
		private RectMask2D m_RectMask;

		// Token: 0x04003998 RID: 14744
		private CanvasGroup canvasGroup;

		// Token: 0x0400399A RID: 14746
		private const float HOLOGRAM_MASK_SIZE_X = 750f;

		// Token: 0x0400399B RID: 14747
		private const float HOLOGRAM_MASK_SIZE_Y = 3000f;

		// Token: 0x0400399C RID: 14748
		private const float HOLOGRAM_OPEN_CLOSE_TIME = 0.33f;
	}
}
