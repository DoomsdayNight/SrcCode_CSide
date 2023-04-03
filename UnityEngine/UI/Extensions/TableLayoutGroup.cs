using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000314 RID: 788
	[AddComponentMenu("Layout/Extensions/Table Layout Group")]
	public class TableLayoutGroup : LayoutGroup
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x000402EF File Offset: 0x0003E4EF
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x000402F7 File Offset: 0x0003E4F7
		public TableLayoutGroup.Corner StartCorner
		{
			get
			{
				return this.startCorner;
			}
			set
			{
				base.SetProperty<TableLayoutGroup.Corner>(ref this.startCorner, value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00040306 File Offset: 0x0003E506
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0004030E File Offset: 0x0003E50E
		public float[] ColumnWidths
		{
			get
			{
				return this.columnWidths;
			}
			set
			{
				base.SetProperty<float[]>(ref this.columnWidths, value);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004031D File Offset: 0x0003E51D
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00040325 File Offset: 0x0003E525
		public float MinimumRowHeight
		{
			get
			{
				return this.minimumRowHeight;
			}
			set
			{
				base.SetProperty<float>(ref this.minimumRowHeight, value);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00040334 File Offset: 0x0003E534
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0004033C File Offset: 0x0003E53C
		public bool FlexibleRowHeight
		{
			get
			{
				return this.flexibleRowHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.flexibleRowHeight, value);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0004034B File Offset: 0x0003E54B
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x00040353 File Offset: 0x0003E553
		public float ColumnSpacing
		{
			get
			{
				return this.columnSpacing;
			}
			set
			{
				base.SetProperty<float>(ref this.columnSpacing, value);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00040362 File Offset: 0x0003E562
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x0004036A File Offset: 0x0003E56A
		public float RowSpacing
		{
			get
			{
				return this.rowSpacing;
			}
			set
			{
				base.SetProperty<float>(ref this.rowSpacing, value);
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004037C File Offset: 0x0003E57C
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			float num = (float)base.padding.horizontal;
			int num2 = Mathf.Min(base.rectChildren.Count, this.columnWidths.Length);
			for (int i = 0; i < num2; i++)
			{
				num += this.columnWidths[i];
				num += this.columnSpacing;
			}
			num -= this.columnSpacing;
			base.SetLayoutInputForAxis(num, num, 0f, 0);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000403EC File Offset: 0x0003E5EC
		public override void CalculateLayoutInputVertical()
		{
			int num = this.columnWidths.Length;
			int num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num);
			this.preferredRowHeights = new float[num2];
			float num3 = (float)base.padding.vertical;
			float num4 = (float)base.padding.vertical;
			if (num2 > 1)
			{
				float num5 = (float)(num2 - 1) * this.rowSpacing;
				num3 += num5;
				num4 += num5;
			}
			if (this.flexibleRowHeight)
			{
				for (int i = 0; i < num2; i++)
				{
					float num6 = this.minimumRowHeight;
					float num7 = this.minimumRowHeight;
					for (int j = 0; j < num; j++)
					{
						int num8 = i * num + j;
						if (num8 == base.rectChildren.Count)
						{
							break;
						}
						num7 = Mathf.Max(LayoutUtility.GetPreferredHeight(base.rectChildren[num8]), num7);
						num6 = Mathf.Max(LayoutUtility.GetMinHeight(base.rectChildren[num8]), num6);
					}
					num3 += num6;
					num4 += num7;
					this.preferredRowHeights[i] = num7;
				}
			}
			else
			{
				for (int k = 0; k < num2; k++)
				{
					this.preferredRowHeights[k] = this.minimumRowHeight;
				}
				num3 += (float)num2 * this.minimumRowHeight;
				num4 = num3;
			}
			num4 = Mathf.Max(num3, num4);
			base.SetLayoutInputForAxis(num3, num4, 1f, 1);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00040550 File Offset: 0x0003E750
		public override void SetLayoutHorizontal()
		{
			if (this.columnWidths.Length == 0)
			{
				this.columnWidths = new float[1];
			}
			int num = this.columnWidths.Length;
			int num2 = (int)(this.startCorner % TableLayoutGroup.Corner.LowerLeft);
			float num3 = 0f;
			int num4 = Mathf.Min(base.rectChildren.Count, this.columnWidths.Length);
			for (int i = 0; i < num4; i++)
			{
				num3 += this.columnWidths[i];
				num3 += this.columnSpacing;
			}
			num3 -= this.columnSpacing;
			float num5 = base.GetStartOffset(0, num3);
			if (num2 == 1)
			{
				num5 += num3;
			}
			float num6 = num5;
			for (int j = 0; j < base.rectChildren.Count; j++)
			{
				int num7 = j % num;
				if (num7 == 0)
				{
					num6 = num5;
				}
				if (num2 == 1)
				{
					num6 -= this.columnWidths[num7];
				}
				base.SetChildAlongAxis(base.rectChildren[j], 0, num6, this.columnWidths[num7]);
				if (num2 == 1)
				{
					num6 -= this.columnSpacing;
				}
				else
				{
					num6 += this.columnWidths[num7] + this.columnSpacing;
				}
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004066C File Offset: 0x0003E86C
		public override void SetLayoutVertical()
		{
			int num = this.columnWidths.Length;
			int num2 = this.preferredRowHeights.Length;
			int num3 = (int)(this.startCorner / TableLayoutGroup.Corner.LowerLeft);
			float num4 = 0f;
			for (int i = 0; i < num2; i++)
			{
				num4 += this.preferredRowHeights[i];
			}
			if (num2 > 1)
			{
				num4 += (float)(num2 - 1) * this.rowSpacing;
			}
			float num5 = base.GetStartOffset(1, num4);
			if (num3 == 1)
			{
				num5 += num4;
			}
			float num6 = num5;
			for (int j = 0; j < num2; j++)
			{
				if (num3 == 1)
				{
					num6 -= this.preferredRowHeights[j];
				}
				for (int k = 0; k < num; k++)
				{
					int num7 = j * num + k;
					if (num7 == base.rectChildren.Count)
					{
						break;
					}
					base.SetChildAlongAxis(base.rectChildren[num7], 1, num6, this.preferredRowHeights[j]);
				}
				if (num3 == 1)
				{
					num6 -= this.rowSpacing;
				}
				else
				{
					num6 += this.preferredRowHeights[j] + this.rowSpacing;
				}
			}
			this.preferredRowHeights = null;
		}

		// Token: 0x04000C7C RID: 3196
		[SerializeField]
		protected TableLayoutGroup.Corner startCorner;

		// Token: 0x04000C7D RID: 3197
		[SerializeField]
		protected float[] columnWidths = new float[]
		{
			96f
		};

		// Token: 0x04000C7E RID: 3198
		[SerializeField]
		protected float minimumRowHeight = 32f;

		// Token: 0x04000C7F RID: 3199
		[SerializeField]
		protected bool flexibleRowHeight = true;

		// Token: 0x04000C80 RID: 3200
		[SerializeField]
		protected float columnSpacing;

		// Token: 0x04000C81 RID: 3201
		[SerializeField]
		protected float rowSpacing;

		// Token: 0x04000C82 RID: 3202
		private float[] preferredRowHeights;

		// Token: 0x0200115B RID: 4443
		public enum Corner
		{
			// Token: 0x0400921A RID: 37402
			UpperLeft,
			// Token: 0x0400921B RID: 37403
			UpperRight,
			// Token: 0x0400921C RID: 37404
			LowerLeft,
			// Token: 0x0400921D RID: 37405
			LowerRight
		}
	}
}
