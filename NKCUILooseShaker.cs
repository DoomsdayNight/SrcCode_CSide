using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class NKCUILooseShaker : MonoBehaviour
{
	// Token: 0x060000AF RID: 175 RVA: 0x00003730 File Offset: 0x00001930
	private void Awake()
	{
		this._myRectTransform = base.GetComponent<RectTransform>();
		if (this._myRectTransform != null)
		{
			this.originAnchorPos = this._myRectTransform.anchoredPosition;
		}
		if (this.EasingType.Length == 5)
		{
			this.EasingType[0] = Ease.InBounce;
			this.EasingType[1] = Ease.OutElastic;
			this.EasingType[2] = Ease.InOutBounce;
			this.EasingType[3] = Ease.InOutBack;
			this.EasingType[4] = Ease.OutCirc;
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000037A5 File Offset: 0x000019A5
	private void OnDisable()
	{
		this.CleanUp();
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000037AD File Offset: 0x000019AD
	private float GetRandValue()
	{
		return UnityEngine.Random.Range(this.min, this.max);
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000037C0 File Offset: 0x000019C0
	public void StartShake()
	{
		if (this._myRectTransform == null)
		{
			return;
		}
		float randValue = this.GetRandValue();
		float randValue2 = this.GetRandValue();
		this.StopShake();
		this._myRectTransform.DOAnchorPos(new Vector2(this.originAnchorPos.x + randValue, this.originAnchorPos.y + randValue2), this.durationTime, false).SetEase(this.GetRandEaseType()).OnComplete(new TweenCallback(this.StartShake));
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000383E File Offset: 0x00001A3E
	public void StopShake()
	{
		if (this._myRectTransform == null)
		{
			return;
		}
		if (DOTween.IsTweening(this._myRectTransform, false))
		{
			this._myRectTransform.DOKill(false);
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000386A File Offset: 0x00001A6A
	public void CleanUp()
	{
		if (this._myRectTransform == null)
		{
			return;
		}
		this.StopShake();
		this._myRectTransform.anchoredPosition = this.originAnchorPos;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00003894 File Offset: 0x00001A94
	private Ease GetRandEaseType()
	{
		switch (UnityEngine.Random.Range(0, this.EasingType.Length))
		{
		case 0:
			return this.EasingType[0];
		case 1:
			return this.EasingType[1];
		case 2:
			return this.EasingType[2];
		case 3:
			return this.EasingType[3];
		case 4:
			return this.EasingType[4];
		default:
			return Ease.InBounce;
		}
	}

	// Token: 0x04000046 RID: 70
	[SerializeField]
	private float min = -30f;

	// Token: 0x04000047 RID: 71
	[SerializeField]
	private float max = 30f;

	// Token: 0x04000048 RID: 72
	[SerializeField]
	private float durationTime = 2f;

	// Token: 0x04000049 RID: 73
	[SerializeField]
	private Ease[] EasingType = new Ease[5];

	// Token: 0x0400004A RID: 74
	private Vector2 originAnchorPos;

	// Token: 0x0400004B RID: 75
	private RectTransform _myRectTransform;
}
