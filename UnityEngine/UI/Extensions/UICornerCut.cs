using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200031F RID: 799
	[AddComponentMenu("UI/Extensions/Primitives/Cut Corners")]
	public class UICornerCut : UIPrimitiveBase
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00042519 File Offset: 0x00040719
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x00042521 File Offset: 0x00040721
		public bool CutUL
		{
			get
			{
				return this.m_cutUL;
			}
			set
			{
				this.m_cutUL = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00042530 File Offset: 0x00040730
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x00042538 File Offset: 0x00040738
		public bool CutUR
		{
			get
			{
				return this.m_cutUR;
			}
			set
			{
				this.m_cutUR = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00042547 File Offset: 0x00040747
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x0004254F File Offset: 0x0004074F
		public bool CutLL
		{
			get
			{
				return this.m_cutLL;
			}
			set
			{
				this.m_cutLL = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004255E File Offset: 0x0004075E
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00042566 File Offset: 0x00040766
		public bool CutLR
		{
			get
			{
				return this.m_cutLR;
			}
			set
			{
				this.m_cutLR = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00042575 File Offset: 0x00040775
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x0004257D File Offset: 0x0004077D
		public bool MakeColumns
		{
			get
			{
				return this.m_makeColumns;
			}
			set
			{
				this.m_makeColumns = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004258C File Offset: 0x0004078C
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00042594 File Offset: 0x00040794
		public bool UseColorUp
		{
			get
			{
				return this.m_useColorUp;
			}
			set
			{
				this.m_useColorUp = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004259D File Offset: 0x0004079D
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x000425A5 File Offset: 0x000407A5
		public Color32 ColorUp
		{
			get
			{
				return this.m_colorUp;
			}
			set
			{
				this.m_colorUp = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x000425AE File Offset: 0x000407AE
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x000425B6 File Offset: 0x000407B6
		public bool UseColorDown
		{
			get
			{
				return this.m_useColorDown;
			}
			set
			{
				this.m_useColorDown = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x000425BF File Offset: 0x000407BF
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x000425C7 File Offset: 0x000407C7
		public Color32 ColorDown
		{
			get
			{
				return this.m_colorDown;
			}
			set
			{
				this.m_colorDown = value;
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000425D0 File Offset: 0x000407D0
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			Rect rect = base.rectTransform.rect;
			Rect rect2 = rect;
			Color32 color = this.color;
			bool flag = this.m_cutUL | this.m_cutUR;
			bool flag2 = this.m_cutLL | this.m_cutLR;
			bool flag3 = this.m_cutLL | this.m_cutUL;
			bool flag4 = this.m_cutLR | this.m_cutUR;
			if ((flag || flag2) && this.cornerSize.sqrMagnitude > 0f)
			{
				vh.Clear();
				if (flag3)
				{
					rect2.xMin += this.cornerSize.x;
				}
				if (flag2)
				{
					rect2.yMin += this.cornerSize.y;
				}
				if (flag)
				{
					rect2.yMax -= this.cornerSize.y;
				}
				if (flag4)
				{
					rect2.xMax -= this.cornerSize.x;
				}
				if (this.m_makeColumns)
				{
					Vector2 vector = new Vector2(rect.xMin, this.m_cutUL ? rect2.yMax : rect.yMax);
					Vector2 vector2 = new Vector2(rect.xMax, this.m_cutUR ? rect2.yMax : rect.yMax);
					Vector2 vector3 = new Vector2(rect.xMin, this.m_cutLL ? rect2.yMin : rect.yMin);
					Vector2 vector4 = new Vector2(rect.xMax, this.m_cutLR ? rect2.yMin : rect.yMin);
					if (flag3)
					{
						UICornerCut.AddSquare(vector3, vector, new Vector2(rect2.xMin, rect.yMax), new Vector2(rect2.xMin, rect.yMin), rect, this.m_useColorUp ? this.m_colorUp : color, vh);
					}
					if (flag4)
					{
						UICornerCut.AddSquare(vector2, vector4, new Vector2(rect2.xMax, rect.yMin), new Vector2(rect2.xMax, rect.yMax), rect, this.m_useColorDown ? this.m_colorDown : color, vh);
					}
				}
				else
				{
					Vector2 vector = new Vector2(this.m_cutUL ? rect2.xMin : rect.xMin, rect.yMax);
					Vector2 vector2 = new Vector2(this.m_cutUR ? rect2.xMax : rect.xMax, rect.yMax);
					Vector2 vector3 = new Vector2(this.m_cutLL ? rect2.xMin : rect.xMin, rect.yMin);
					Vector2 vector4 = new Vector2(this.m_cutLR ? rect2.xMax : rect.xMax, rect.yMin);
					if (flag2)
					{
						UICornerCut.AddSquare(vector4, vector3, new Vector2(rect.xMin, rect2.yMin), new Vector2(rect.xMax, rect2.yMin), rect, this.m_useColorDown ? this.m_colorDown : color, vh);
					}
					if (flag)
					{
						UICornerCut.AddSquare(vector, vector2, new Vector2(rect.xMax, rect2.yMax), new Vector2(rect.xMin, rect2.yMax), rect, this.m_useColorUp ? this.m_colorUp : color, vh);
					}
				}
				if (this.m_makeColumns)
				{
					UICornerCut.AddSquare(new Rect(rect2.xMin, rect.yMin, rect2.width, rect.height), rect, color, vh);
					return;
				}
				UICornerCut.AddSquare(new Rect(rect.xMin, rect2.yMin, rect.width, rect2.height), rect, color, vh);
			}
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00042980 File Offset: 0x00040B80
		private static void AddSquare(Rect rect, Rect rectUV, Color32 color32, VertexHelper vh)
		{
			int num = UICornerCut.AddVert(rect.xMin, rect.yMin, rectUV, color32, vh);
			int idx = UICornerCut.AddVert(rect.xMin, rect.yMax, rectUV, color32, vh);
			int num2 = UICornerCut.AddVert(rect.xMax, rect.yMax, rectUV, color32, vh);
			int idx2 = UICornerCut.AddVert(rect.xMax, rect.yMin, rectUV, color32, vh);
			vh.AddTriangle(num, idx, num2);
			vh.AddTriangle(num2, idx2, num);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000429FC File Offset: 0x00040BFC
		private static void AddSquare(Vector2 a, Vector2 b, Vector2 c, Vector2 d, Rect rectUV, Color32 color32, VertexHelper vh)
		{
			int num = UICornerCut.AddVert(a.x, a.y, rectUV, color32, vh);
			int idx = UICornerCut.AddVert(b.x, b.y, rectUV, color32, vh);
			int num2 = UICornerCut.AddVert(c.x, c.y, rectUV, color32, vh);
			int idx2 = UICornerCut.AddVert(d.x, d.y, rectUV, color32, vh);
			vh.AddTriangle(num, idx, num2);
			vh.AddTriangle(num2, idx2, num);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00042A80 File Offset: 0x00040C80
		private static int AddVert(float x, float y, Rect area, Color32 color32, VertexHelper vh)
		{
			Vector2 v = new Vector2(Mathf.InverseLerp(area.xMin, area.xMax, x), Mathf.InverseLerp(area.yMin, area.yMax, y));
			vh.AddVert(new Vector3(x, y), color32, v);
			return vh.currentVertCount - 1;
		}

		// Token: 0x04000CB1 RID: 3249
		public Vector2 cornerSize = new Vector2(16f, 16f);

		// Token: 0x04000CB2 RID: 3250
		[Header("Corners to cut")]
		[SerializeField]
		private bool m_cutUL = true;

		// Token: 0x04000CB3 RID: 3251
		[SerializeField]
		private bool m_cutUR;

		// Token: 0x04000CB4 RID: 3252
		[SerializeField]
		private bool m_cutLL;

		// Token: 0x04000CB5 RID: 3253
		[SerializeField]
		private bool m_cutLR;

		// Token: 0x04000CB6 RID: 3254
		[Tooltip("Up-Down colors become Left-Right colors")]
		[SerializeField]
		private bool m_makeColumns;

		// Token: 0x04000CB7 RID: 3255
		[Header("Color the cut bars differently")]
		[SerializeField]
		private bool m_useColorUp;

		// Token: 0x04000CB8 RID: 3256
		[SerializeField]
		private Color32 m_colorUp;

		// Token: 0x04000CB9 RID: 3257
		[SerializeField]
		private bool m_useColorDown;

		// Token: 0x04000CBA RID: 3258
		[SerializeField]
		private Color32 m_colorDown;
	}
}
