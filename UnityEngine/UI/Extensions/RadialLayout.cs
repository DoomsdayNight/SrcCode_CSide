using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200030F RID: 783
	[AddComponentMenu("Layout/Extensions/Radial Layout")]
	public class RadialLayout : LayoutGroup
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x0003DEE2 File Offset: 0x0003C0E2
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CalculateRadial();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003DEF0 File Offset: 0x0003C0F0
		public override void SetLayoutHorizontal()
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0003DEF2 File Offset: 0x0003C0F2
		public override void SetLayoutVertical()
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
		public override void CalculateLayoutInputVertical()
		{
			this.CalculateRadial();
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		public override void CalculateLayoutInputHorizontal()
		{
			this.CalculateRadial();
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003DF04 File Offset: 0x0003C104
		private void CalculateRadial()
		{
			this.m_Tracker.Clear();
			if (base.transform.childCount == 0)
			{
				return;
			}
			int num = 0;
			if (this.OnlyLayoutVisible)
			{
				for (int i = 0; i < base.transform.childCount; i++)
				{
					RectTransform rectTransform = (RectTransform)base.transform.GetChild(i);
					if (rectTransform != null && rectTransform.gameObject.activeSelf)
					{
						num++;
					}
				}
			}
			else
			{
				num = base.transform.childCount;
			}
			float num2 = (this.MaxAngle - this.MinAngle) / (float)num;
			float num3 = this.StartAngle;
			for (int j = 0; j < base.transform.childCount; j++)
			{
				RectTransform rectTransform2 = (RectTransform)base.transform.GetChild(j);
				if (rectTransform2 != null && (!this.OnlyLayoutVisible || rectTransform2.gameObject.activeSelf))
				{
					this.m_Tracker.Add(this, rectTransform2, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
					Vector3 a = new Vector3(Mathf.Cos(num3 * 0.017453292f), Mathf.Sin(num3 * 0.017453292f), 0f);
					rectTransform2.localPosition = a * this.fDistance;
					RectTransform rectTransform3 = rectTransform2;
					RectTransform rectTransform4 = rectTransform2;
					RectTransform rectTransform5 = rectTransform2;
					Vector2 vector = new Vector2(0.5f, 0.5f);
					rectTransform5.pivot = vector;
					rectTransform3.anchorMin = (rectTransform4.anchorMax = vector);
					num3 += num2;
				}
			}
		}

		// Token: 0x04000C14 RID: 3092
		public float fDistance;

		// Token: 0x04000C15 RID: 3093
		[Range(0f, 360f)]
		public float MinAngle;

		// Token: 0x04000C16 RID: 3094
		[Range(0f, 360f)]
		public float MaxAngle;

		// Token: 0x04000C17 RID: 3095
		[Range(0f, 360f)]
		public float StartAngle;

		// Token: 0x04000C18 RID: 3096
		public bool OnlyLayoutVisible;
	}
}
