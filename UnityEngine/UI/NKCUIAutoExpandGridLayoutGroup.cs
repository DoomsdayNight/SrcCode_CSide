using System;

namespace UnityEngine.UI
{
	// Token: 0x020002AC RID: 684
	[AddComponentMenu("Layout/Auto Expand Grid Layout Group", 152)]
	public class NKCUIAutoExpandGridLayoutGroup : LayoutGroup
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00028D76 File Offset: 0x00026F76
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00028D7E File Offset: 0x00026F7E
		public NKCUIAutoExpandGridLayoutGroup.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<NKCUIAutoExpandGridLayoutGroup.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00028D8D File Offset: 0x00026F8D
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00028D95 File Offset: 0x00026F95
		public NKCUIAutoExpandGridLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<NKCUIAutoExpandGridLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00028DA4 File Offset: 0x00026FA4
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00028DAC File Offset: 0x00026FAC
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00028DBB File Offset: 0x00026FBB
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00028DC3 File Offset: 0x00026FC3
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00028DD2 File Offset: 0x00026FD2
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00028DDA File Offset: 0x00026FDA
		public NKCUIAutoExpandGridLayoutGroup.Constraint constraint
		{
			get
			{
				return this.m_Constraint;
			}
			set
			{
				base.SetProperty<NKCUIAutoExpandGridLayoutGroup.Constraint>(ref this.m_Constraint, value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00028DE9 File Offset: 0x00026FE9
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00028DF1 File Offset: 0x00026FF1
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, Mathf.Max(1, value));
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00028E06 File Offset: 0x00027006
		protected NKCUIAutoExpandGridLayoutGroup()
		{
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00028E38 File Offset: 0x00027038
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int num2;
			int num;
			if (this.m_Constraint == NKCUIAutoExpandGridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = (num2 = this.m_ConstraintCount);
			}
			else if (this.m_Constraint == NKCUIAutoExpandGridLayoutGroup.Constraint.FixedRowCount)
			{
				num = (num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f));
			}
			else
			{
				num2 = 1;
				num = Mathf.CeilToInt(Mathf.Sqrt((float)base.rectChildren.Count));
			}
			base.SetLayoutInputForAxis((float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num2 - this.spacing.x, (float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num - this.spacing.x, -1f, 0);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00028F1C File Offset: 0x0002711C
		public override void CalculateLayoutInputVertical()
		{
			int num;
			if (this.m_Constraint == NKCUIAutoExpandGridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f);
			}
			else if (this.m_Constraint == NKCUIAutoExpandGridLayoutGroup.Constraint.FixedRowCount)
			{
				num = this.m_ConstraintCount;
			}
			else
			{
				float x = base.rectTransform.rect.size.x;
				int num2 = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2);
			}
			float num3 = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)num - this.spacing.y;
			base.SetLayoutInputForAxis(num3, num3, -1f, 1);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00029020 File Offset: 0x00027220
		public override void SetLayoutHorizontal()
		{
			this.SetCellsAlongAxis(0);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00029029 File Offset: 0x00027229
		public override void SetLayoutVertical()
		{
			this.SetCellsAlongAxis(1);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00029034 File Offset: 0x00027234
		private void SetCellsAlongAxis(int axis)
		{
			if (axis == 0)
			{
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rectTransform = base.rectChildren[i];
					this.m_Tracker.Add(this, rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rectTransform.anchorMin = Vector2.up;
					rectTransform.anchorMax = Vector2.up;
					rectTransform.sizeDelta = this.cellSize;
				}
				return;
			}
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			int num;
			int num2;
			if (this.m_Constraint == NKCUIAutoExpandGridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = this.m_ConstraintCount;
				num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num - 0.001f);
			}
			else if (this.m_Constraint == NKCUIAutoExpandGridLayoutGroup.Constraint.FixedRowCount)
			{
				num2 = this.m_ConstraintCount;
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2 - 0.001f);
			}
			else
			{
				if (this.cellSize.x + this.spacing.x <= 0f)
				{
					num = int.MaxValue;
				}
				else
				{
					num = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				}
				if (this.cellSize.y + this.spacing.y <= 0f)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = Mathf.Max(1, Mathf.FloorToInt((y - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
				}
			}
			int num3 = (int)(this.startCorner % NKCUIAutoExpandGridLayoutGroup.Corner.LowerLeft);
			int num4 = (int)(this.startCorner / NKCUIAutoExpandGridLayoutGroup.Corner.LowerLeft);
			int num5;
			int num6;
			int num7;
			if (this.startAxis == NKCUIAutoExpandGridLayoutGroup.Axis.Horizontal)
			{
				num5 = num;
				num6 = Mathf.Clamp(num, 1, base.rectChildren.Count);
				num7 = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			else
			{
				num5 = num2;
				num7 = Mathf.Clamp(num2, 1, base.rectChildren.Count);
				num6 = Mathf.Clamp(num, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			Vector2 vector = new Vector2((float)num6 * this.cellSize.x + (float)(num6 - 1) * this.spacing.x, (float)num7 * this.cellSize.y + (float)(num7 - 1) * this.spacing.y);
			Vector2 vector2 = new Vector2(base.GetStartOffset(0, vector.x), base.GetStartOffset(1, vector.y));
			for (int j = 0; j < base.rectChildren.Count; j++)
			{
				int num8;
				int num9;
				if (this.startAxis == NKCUIAutoExpandGridLayoutGroup.Axis.Horizontal)
				{
					num8 = j % num5;
					num9 = j / num5;
				}
				else
				{
					num8 = j / num5;
					num9 = j % num5;
				}
				if (num3 == 1)
				{
					num8 = num6 - 1 - num8;
				}
				if (num4 == 1)
				{
					num9 = num7 - 1 - num9;
				}
				float num10 = (x - this.spacing[0] * (float)(num6 - 1)) / (float)num6;
				base.SetChildAlongAxis(base.rectChildren[j], 0, vector2.x + (num10 + this.spacing[0]) * (float)num8, num10);
				base.SetChildAlongAxis(base.rectChildren[j], 1, vector2.y + (this.cellSize[1] + this.spacing[1]) * (float)num9, this.cellSize[1]);
			}
		}

		// Token: 0x04000999 RID: 2457
		[SerializeField]
		protected NKCUIAutoExpandGridLayoutGroup.Corner m_StartCorner;

		// Token: 0x0400099A RID: 2458
		[SerializeField]
		protected NKCUIAutoExpandGridLayoutGroup.Axis m_StartAxis;

		// Token: 0x0400099B RID: 2459
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x0400099C RID: 2460
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x0400099D RID: 2461
		[SerializeField]
		protected NKCUIAutoExpandGridLayoutGroup.Constraint m_Constraint;

		// Token: 0x0400099E RID: 2462
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x02001119 RID: 4377
		public enum Corner
		{
			// Token: 0x0400918F RID: 37263
			UpperLeft,
			// Token: 0x04009190 RID: 37264
			UpperRight,
			// Token: 0x04009191 RID: 37265
			LowerLeft,
			// Token: 0x04009192 RID: 37266
			LowerRight
		}

		// Token: 0x0200111A RID: 4378
		public enum Axis
		{
			// Token: 0x04009194 RID: 37268
			Horizontal,
			// Token: 0x04009195 RID: 37269
			Vertical
		}

		// Token: 0x0200111B RID: 4379
		public enum Constraint
		{
			// Token: 0x04009197 RID: 37271
			Flexible,
			// Token: 0x04009198 RID: 37272
			FixedColumnCount,
			// Token: 0x04009199 RID: 37273
			FixedRowCount
		}
	}
}
