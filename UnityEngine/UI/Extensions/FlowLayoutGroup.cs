using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200030C RID: 780
	[AddComponentMenu("Layout/Extensions/Flow Layout Group")]
	public class FlowLayoutGroup : LayoutGroup
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0003CD22 File Offset: 0x0003AF22
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0003CD2A File Offset: 0x0003AF2A
		public FlowLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<FlowLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003CD3C File Offset: 0x0003AF3C
		public override void CalculateLayoutInputHorizontal()
		{
			if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
			{
				base.CalculateLayoutInputHorizontal();
				float totalMin = this.GetGreatestMinimumChildWidth() + (float)base.padding.left + (float)base.padding.right;
				base.SetLayoutInputForAxis(totalMin, -1f, -1f, 0);
				return;
			}
			this._layoutWidth = this.SetLayout(0, true);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003CD99 File Offset: 0x0003AF99
		public override void SetLayoutHorizontal()
		{
			this.SetLayout(0, false);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		public override void SetLayoutVertical()
		{
			this.SetLayout(1, false);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		public override void CalculateLayoutInputVertical()
		{
			if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
			{
				this._layoutHeight = this.SetLayout(1, true);
				return;
			}
			base.CalculateLayoutInputHorizontal();
			float totalMin = this.GetGreatestMinimumChildHeigth() + (float)base.padding.bottom + (float)base.padding.top;
			base.SetLayoutInputForAxis(totalMin, -1f, -1f, 1);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0003CE0D File Offset: 0x0003B00D
		protected bool IsCenterAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.LowerCenter || base.childAlignment == TextAnchor.MiddleCenter || base.childAlignment == TextAnchor.UpperCenter;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0003CE2C File Offset: 0x0003B02C
		protected bool IsRightAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.LowerRight || base.childAlignment == TextAnchor.MiddleRight || base.childAlignment == TextAnchor.UpperRight;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0003CE4B File Offset: 0x0003B04B
		protected bool IsMiddleAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.MiddleLeft || base.childAlignment == TextAnchor.MiddleRight || base.childAlignment == TextAnchor.MiddleCenter;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0003CE6A File Offset: 0x0003B06A
		protected bool IsLowerAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.LowerLeft || base.childAlignment == TextAnchor.LowerRight || base.childAlignment == TextAnchor.LowerCenter;
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003CE8C File Offset: 0x0003B08C
		public float SetLayout(int axis, bool layoutInput)
		{
			float height = base.rectTransform.rect.height;
			float width = base.rectTransform.rect.width;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
			{
				num5 = height;
				num6 = width - (float)base.padding.left - (float)base.padding.right;
				if (this.IsLowerAlign)
				{
					num3 = (float)base.padding.bottom;
					num4 = (float)base.padding.top;
				}
				else
				{
					num3 = (float)base.padding.top;
					num4 = (float)base.padding.bottom;
				}
				num = this.SpacingY;
				num2 = this.SpacingX;
			}
			else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
			{
				num5 = width;
				num6 = height - (float)base.padding.top - (float)base.padding.bottom;
				if (this.IsRightAlign)
				{
					num3 = (float)base.padding.right;
					num4 = (float)base.padding.left;
				}
				else
				{
					num3 = (float)base.padding.left;
					num4 = (float)base.padding.right;
				}
				num = this.SpacingX;
				num2 = this.SpacingY;
			}
			float num7 = 0f;
			float num8 = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				int index = i;
				RectTransform rectTransform = base.rectChildren[index];
				float num9 = 0f;
				float num10 = 0f;
				if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
				{
					if (this.invertOrder)
					{
						index = (this.IsLowerAlign ? (base.rectChildren.Count - 1 - i) : i);
					}
					rectTransform = base.rectChildren[index];
					num9 = LayoutUtility.GetPreferredSize(rectTransform, 0);
					num9 = Mathf.Min(num9, num6);
					num10 = LayoutUtility.GetPreferredSize(rectTransform, 1);
					num10 = Mathf.Min(num10, num6);
				}
				else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
				{
					if (this.invertOrder)
					{
						index = (this.IsRightAlign ? (base.rectChildren.Count - 1 - i) : i);
					}
					rectTransform = base.rectChildren[index];
					num9 = LayoutUtility.GetPreferredSize(rectTransform, 1);
					num9 = Mathf.Min(num9, num6);
					num10 = LayoutUtility.GetPreferredSize(rectTransform, 0);
					num10 = Mathf.Min(num10, num6);
				}
				if (num7 + num9 > num6)
				{
					num7 -= num2;
					if (!layoutInput)
					{
						if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
						{
							float yOffset = this.CalculateRowVerticalOffset(num5, num3, num8);
							this.LayoutRow(this._itemList, num7, num8, num6, (float)base.padding.left, yOffset, axis);
						}
						else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
						{
							float xOffset = this.CalculateColHorizontalOffset(num5, num3, num8);
							this.LayoutCol(this._itemList, num8, num7, num6, xOffset, (float)base.padding.top, axis);
						}
					}
					this._itemList.Clear();
					num3 += num8;
					num3 += num;
					num8 = 0f;
					num7 = 0f;
				}
				num7 += num9;
				this._itemList.Add(rectTransform);
				if (num10 > num8)
				{
					num8 = num10;
				}
				if (i < base.rectChildren.Count - 1)
				{
					num7 += num2;
				}
			}
			if (!layoutInput)
			{
				if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
				{
					float yOffset2 = this.CalculateRowVerticalOffset(height, num3, num8);
					num7 -= num2;
					this.LayoutRow(this._itemList, num7, num8, num6 - (this.ChildForceExpandWidth ? 0f : num2), (float)base.padding.left, yOffset2, axis);
				}
				else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
				{
					float xOffset2 = this.CalculateColHorizontalOffset(width, num3, num8);
					num7 -= num2;
					this.LayoutCol(this._itemList, num8, num7, num6 - (this.ChildForceExpandHeight ? 0f : num2), xOffset2, (float)base.padding.top, axis);
				}
			}
			this._itemList.Clear();
			num3 += num8;
			num3 += num4;
			if (layoutInput)
			{
				base.SetLayoutInputForAxis(num3, num3, -1f, axis);
			}
			return num3;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		private float CalculateRowVerticalOffset(float groupHeight, float yOffset, float currentRowHeight)
		{
			if (this.IsLowerAlign)
			{
				return groupHeight - yOffset - currentRowHeight;
			}
			if (this.IsMiddleAlign)
			{
				return groupHeight * 0.5f - this._layoutHeight * 0.5f + yOffset;
			}
			return yOffset;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003D2E0 File Offset: 0x0003B4E0
		private float CalculateColHorizontalOffset(float groupWidth, float xOffset, float currentColWidth)
		{
			if (this.IsRightAlign)
			{
				return groupWidth - xOffset - currentColWidth;
			}
			if (this.IsCenterAlign)
			{
				return groupWidth * 0.5f - this._layoutWidth * 0.5f + xOffset;
			}
			return xOffset;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003D310 File Offset: 0x0003B510
		protected void LayoutRow(IList<RectTransform> contents, float rowWidth, float rowHeight, float maxWidth, float xOffset, float yOffset, int axis)
		{
			float num = xOffset;
			if (!this.ChildForceExpandWidth && this.IsCenterAlign)
			{
				num += (maxWidth - rowWidth) * 0.5f;
			}
			else if (!this.ChildForceExpandWidth && this.IsRightAlign)
			{
				num += maxWidth - rowWidth;
			}
			float num2 = 0f;
			float num3 = 0f;
			if (this.ChildForceExpandWidth)
			{
				num2 = (maxWidth - rowWidth) / (float)this._itemList.Count;
			}
			else if (this.ExpandHorizontalSpacing)
			{
				num3 = (maxWidth - rowWidth) / (float)(this._itemList.Count - 1);
				if (this._itemList.Count > 1)
				{
					if (this.IsCenterAlign)
					{
						num -= num3 * 0.5f * (float)(this._itemList.Count - 1);
					}
					else if (this.IsRightAlign)
					{
						num -= num3 * (float)(this._itemList.Count - 1);
					}
				}
			}
			for (int i = 0; i < this._itemList.Count; i++)
			{
				int index = this.IsLowerAlign ? (this._itemList.Count - 1 - i) : i;
				RectTransform rect = this._itemList[index];
				float num4 = LayoutUtility.GetPreferredSize(rect, 0) + num2;
				float num5 = LayoutUtility.GetPreferredSize(rect, 1);
				if (this.ChildForceExpandHeight)
				{
					num5 = rowHeight;
				}
				num4 = Mathf.Min(num4, maxWidth);
				float num6 = yOffset;
				if (this.IsMiddleAlign)
				{
					num6 += (rowHeight - num5) * 0.5f;
				}
				else if (this.IsLowerAlign)
				{
					num6 += rowHeight - num5;
				}
				if (this.ExpandHorizontalSpacing && i > 0)
				{
					num += num3;
				}
				if (axis == 0)
				{
					base.SetChildAlongAxis(rect, 0, num, num4);
				}
				else
				{
					base.SetChildAlongAxis(rect, 1, num6, num5);
				}
				if (i < this._itemList.Count - 1)
				{
					num += num4 + this.SpacingX;
				}
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0003D4D4 File Offset: 0x0003B6D4
		protected void LayoutCol(IList<RectTransform> contents, float colWidth, float colHeight, float maxHeight, float xOffset, float yOffset, int axis)
		{
			float num = yOffset;
			if (!this.ChildForceExpandHeight && this.IsMiddleAlign)
			{
				num += (maxHeight - colHeight) * 0.5f;
			}
			else if (!this.ChildForceExpandHeight && this.IsLowerAlign)
			{
				num += maxHeight - colHeight;
			}
			float num2 = 0f;
			float num3 = 0f;
			if (this.ChildForceExpandHeight)
			{
				num2 = (maxHeight - colHeight) / (float)this._itemList.Count;
			}
			else if (this.ExpandHorizontalSpacing)
			{
				num3 = (maxHeight - colHeight) / (float)(this._itemList.Count - 1);
				if (this._itemList.Count > 1)
				{
					if (this.IsMiddleAlign)
					{
						num -= num3 * 0.5f * (float)(this._itemList.Count - 1);
					}
					else if (this.IsLowerAlign)
					{
						num -= num3 * (float)(this._itemList.Count - 1);
					}
				}
			}
			for (int i = 0; i < this._itemList.Count; i++)
			{
				int index = this.IsRightAlign ? (this._itemList.Count - 1 - i) : i;
				RectTransform rect = this._itemList[index];
				float num4 = LayoutUtility.GetPreferredSize(rect, 0);
				float num5 = LayoutUtility.GetPreferredSize(rect, 1) + num2;
				if (this.ChildForceExpandWidth)
				{
					num4 = colWidth;
				}
				num5 = Mathf.Min(num5, maxHeight);
				float num6 = xOffset;
				if (this.IsCenterAlign)
				{
					num6 += (colWidth - num4) * 0.5f;
				}
				else if (this.IsRightAlign)
				{
					num6 += colWidth - num4;
				}
				if (this.ExpandHorizontalSpacing && i > 0)
				{
					num += num3;
				}
				if (axis == 0)
				{
					base.SetChildAlongAxis(rect, 0, num6, num4);
				}
				else
				{
					base.SetChildAlongAxis(rect, 1, num, num5);
				}
				if (i < this._itemList.Count - 1)
				{
					num += num5 + this.SpacingY;
				}
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0003D698 File Offset: 0x0003B898
		public float GetGreatestMinimumChildWidth()
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				num = Mathf.Max(LayoutUtility.GetMinWidth(base.rectChildren[i]), num);
			}
			return num;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		public float GetGreatestMinimumChildHeigth()
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				num = Mathf.Max(LayoutUtility.GetMinHeight(base.rectChildren[i]), num);
			}
			return num;
		}

		// Token: 0x04000C09 RID: 3081
		public float SpacingX;

		// Token: 0x04000C0A RID: 3082
		public float SpacingY;

		// Token: 0x04000C0B RID: 3083
		public bool ExpandHorizontalSpacing;

		// Token: 0x04000C0C RID: 3084
		public bool ChildForceExpandWidth;

		// Token: 0x04000C0D RID: 3085
		public bool ChildForceExpandHeight;

		// Token: 0x04000C0E RID: 3086
		public bool invertOrder;

		// Token: 0x04000C0F RID: 3087
		private float _layoutHeight;

		// Token: 0x04000C10 RID: 3088
		private float _layoutWidth;

		// Token: 0x04000C11 RID: 3089
		[SerializeField]
		protected FlowLayoutGroup.Axis m_StartAxis;

		// Token: 0x04000C12 RID: 3090
		private readonly IList<RectTransform> _itemList = new List<RectTransform>();

		// Token: 0x02001151 RID: 4433
		public enum Axis
		{
			// Token: 0x04009205 RID: 37381
			Horizontal,
			// Token: 0x04009206 RID: 37382
			Vertical
		}
	}
}
