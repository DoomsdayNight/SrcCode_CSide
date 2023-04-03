using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F4 RID: 756
	[AddComponentMenu("Layout/Extensions/Curved Layout")]
	public class CurvedLayout : LayoutGroup
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0003B1B4 File Offset: 0x000393B4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CalculateRadial();
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003B1C2 File Offset: 0x000393C2
		public override void SetLayoutHorizontal()
		{
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003B1C4 File Offset: 0x000393C4
		public override void SetLayoutVertical()
		{
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003B1C6 File Offset: 0x000393C6
		public override void CalculateLayoutInputVertical()
		{
			this.CalculateRadial();
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003B1CE File Offset: 0x000393CE
		public override void CalculateLayoutInputHorizontal()
		{
			this.CalculateRadial();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003B1D8 File Offset: 0x000393D8
		private void CalculateRadial()
		{
			this.m_Tracker.Clear();
			if (base.transform.childCount == 0)
			{
				return;
			}
			Vector2 pivot = new Vector2((float)(base.childAlignment % TextAnchor.MiddleLeft) * 0.5f, (float)(base.childAlignment / TextAnchor.MiddleLeft) * 0.5f);
			Vector3 a = new Vector3(base.GetStartOffset(0, base.GetTotalPreferredSize(0)), base.GetStartOffset(1, base.GetTotalPreferredSize(1)), 0f);
			float num = 0f;
			float num2 = 1f / (float)base.transform.childCount;
			Vector3 b = this.itemAxis.normalized * this.itemSize;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				RectTransform rectTransform = (RectTransform)base.transform.GetChild(i);
				if (rectTransform != null)
				{
					this.m_Tracker.Add(this, rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
					Vector3 a2 = a + b;
					a = (rectTransform.localPosition = a2 + (num - this.centerpoint) * this.CurveOffset);
					rectTransform.pivot = pivot;
					RectTransform rectTransform2 = rectTransform;
					RectTransform rectTransform3 = rectTransform;
					Vector2 vector = new Vector2(0.5f, 0.5f);
					rectTransform3.anchorMax = vector;
					rectTransform2.anchorMin = vector;
					num += num2;
				}
			}
		}

		// Token: 0x04000BC4 RID: 3012
		public Vector3 CurveOffset;

		// Token: 0x04000BC5 RID: 3013
		[Tooltip("axis along which to place the items, Normalized before use")]
		public Vector3 itemAxis;

		// Token: 0x04000BC6 RID: 3014
		[Tooltip("size of each item along the Normalized axis")]
		public float itemSize;

		// Token: 0x04000BC7 RID: 3015
		public float centerpoint = 0.5f;
	}
}
