using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000010 RID: 16
public class NKCUICharacterViewEffectPinup : NKCUICharacterViewEffectBase
{
	// Token: 0x06000095 RID: 149 RVA: 0x000033C8 File Offset: 0x000015C8
	private void Awake()
	{
		this.m_rRectMask = base.GetComponent<Mask>();
		Transform transform = base.transform.Find("PinupBackGround");
		if (transform != null)
		{
			this.m_PipupBackGround = transform.GetComponent<Image>();
		}
		this.m_PipupMask = base.transform.GetComponent<Image>();
		if (this.m_rRectMask != null)
		{
			this.m_rRectTransform = this.m_rRectMask.rectTransform;
		}
		this.SetActivePinupObject(false);
		this.m_StartSequence = DOTween.Sequence();
		this.m_EndSequence = DOTween.Sequence();
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00003454 File Offset: 0x00001654
	public override void SetEffect(NKCASUIUnitIllust unitIllust, Transform trOriginalRoot)
	{
		this.m_rRectMask == null;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00003463 File Offset: 0x00001663
	public override void CleanUp(NKCASUIUnitIllust unitIllust, Transform trOriginalRoot)
	{
		if (this.m_rRectMask == null)
		{
			return;
		}
		this.SetActivePinupObject(false);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000347B File Offset: 0x0000167B
	public override void SetColor(Color color)
	{
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000347D File Offset: 0x0000167D
	public override void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f)
	{
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003480 File Offset: 0x00001680
	public void StartPinupEffect(float fEasingTime)
	{
		if (this.m_rRectTransform == null)
		{
			return;
		}
		if (this.m_rRectTransform == null)
		{
			return;
		}
		if (this.m_StartSequence.IsPlaying())
		{
			return;
		}
		this.SetActivePinupObject(true);
		this.m_StartSequence.Kill(false);
		this.m_StartSequence.Append(this.m_rRectTransform.DOSizeDelta(new Vector3(300f, 3000f), fEasingTime, false).SetEase(Ease.InOutQuart));
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003500 File Offset: 0x00001700
	public void ClosePinupEffect(float fEasingTime)
	{
		if (this.m_rRectTransform == null)
		{
			return;
		}
		if (this.m_rRectTransform == null)
		{
			return;
		}
		this.SetActivePinupObject(true);
		this.m_EndSequence.Kill(false);
		this.m_EndSequence.Append(this.m_rRectTransform.DOSizeDelta(new Vector3(0f, 3000f), fEasingTime, false).SetEase(Ease.InOutQuart));
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00003572 File Offset: 0x00001772
	public void SetDeActive()
	{
		this.SetActivePinupObject(false);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000357B File Offset: 0x0000177B
	public RectTransform GetRectTransform()
	{
		return this.m_rRectTransform;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00003584 File Offset: 0x00001784
	private void SetActivePinupObject(bool bActive)
	{
		if (this.m_PipupBackGround != null)
		{
			this.m_PipupBackGround.gameObject.SetActive(bActive);
		}
		if (this.m_PipupMask != null)
		{
			this.m_PipupMask.enabled = bActive;
		}
		if (this.m_rRectMask != null)
		{
			this.m_rRectMask.enabled = bActive;
		}
		if (!bActive && this.m_rRectTransform != null)
		{
			this.m_rRectTransform.DOKill(false);
			this.m_rRectTransform.SetSize(new Vector2(0f, 3000f));
		}
	}

	// Token: 0x04000039 RID: 57
	private Mask m_rRectMask;

	// Token: 0x0400003A RID: 58
	private RectTransform m_rRectTransform;

	// Token: 0x0400003B RID: 59
	private Image m_PipupMask;

	// Token: 0x0400003C RID: 60
	private Image m_PipupBackGround;

	// Token: 0x0400003D RID: 61
	private const float m_fPipupActiveValueX = 300f;

	// Token: 0x0400003E RID: 62
	private const float m_fPipupActiveValueY = 3000f;

	// Token: 0x0400003F RID: 63
	private Sequence m_StartSequence;

	// Token: 0x04000040 RID: 64
	private Sequence m_EndSequence;
}
