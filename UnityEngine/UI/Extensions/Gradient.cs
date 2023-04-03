using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D9 RID: 729
	[AddComponentMenu("UI/Effects/Extensions/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000348E0 File Offset: 0x00032AE0
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x000348E8 File Offset: 0x00032AE8
		public GradientMode GradientMode
		{
			get
			{
				return this._gradientMode;
			}
			set
			{
				this._gradientMode = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x000348FC File Offset: 0x00032AFC
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x00034904 File Offset: 0x00032B04
		public GradientDir GradientDir
		{
			get
			{
				return this._gradientDir;
			}
			set
			{
				this._gradientDir = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00034918 File Offset: 0x00032B18
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x00034920 File Offset: 0x00032B20
		public bool OverwriteAllColor
		{
			get
			{
				return this._overwriteAllColor;
			}
			set
			{
				this._overwriteAllColor = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00034934 File Offset: 0x00032B34
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0003493C File Offset: 0x00032B3C
		public Color Vertex1
		{
			get
			{
				return this._vertex1;
			}
			set
			{
				this._vertex1 = value;
				base.graphic.SetAllDirty();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00034950 File Offset: 0x00032B50
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x00034958 File Offset: 0x00032B58
		public Color Vertex2
		{
			get
			{
				return this._vertex2;
			}
			set
			{
				this._vertex2 = value;
				base.graphic.SetAllDirty();
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003496C File Offset: 0x00032B6C
		protected override void Awake()
		{
			this.targetGraphic = base.GetComponent<Graphic>();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003497C File Offset: 0x00032B7C
		public override void ModifyMesh(VertexHelper vh)
		{
			int currentVertCount = vh.currentVertCount;
			if (!this.IsActive() || currentVertCount == 0)
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			UIVertex uivertex = default(UIVertex);
			if (this._gradientMode == GradientMode.Global)
			{
				if (this._gradientDir == GradientDir.DiagonalLeftToRight || this._gradientDir == GradientDir.DiagonalRightToLeft)
				{
					this._gradientDir = GradientDir.Vertical;
				}
				float num = (this._gradientDir == GradientDir.Vertical) ? list[list.Count - 1].position.y : list[list.Count - 1].position.x;
				float num2 = ((this._gradientDir == GradientDir.Vertical) ? list[0].position.y : list[0].position.x) - num;
				for (int i = 0; i < currentVertCount; i++)
				{
					vh.PopulateUIVertex(ref uivertex, i);
					if (this._overwriteAllColor || !(uivertex.color != this.targetGraphic.color))
					{
						uivertex.color *= Color.Lerp(this._vertex2, this._vertex1, (((this._gradientDir == GradientDir.Vertical) ? uivertex.position.y : uivertex.position.x) - num) / num2);
						vh.SetUIVertex(uivertex, i);
					}
				}
				return;
			}
			for (int j = 0; j < currentVertCount; j++)
			{
				vh.PopulateUIVertex(ref uivertex, j);
				if (this._overwriteAllColor || this.CompareCarefully(uivertex.color, this.targetGraphic.color))
				{
					switch (this._gradientDir)
					{
					case GradientDir.Vertical:
						uivertex.color *= ((j % 4 == 0 || (j - 1) % 4 == 0) ? this._vertex1 : this._vertex2);
						break;
					case GradientDir.Horizontal:
						uivertex.color *= ((j % 4 == 0 || (j - 3) % 4 == 0) ? this._vertex1 : this._vertex2);
						break;
					case GradientDir.DiagonalLeftToRight:
						uivertex.color *= ((j % 4 == 0) ? this._vertex1 : (((j - 2) % 4 == 0) ? this._vertex2 : Color.Lerp(this._vertex2, this._vertex1, 0.5f)));
						break;
					case GradientDir.DiagonalRightToLeft:
						uivertex.color *= (((j - 1) % 4 == 0) ? this._vertex1 : (((j - 3) % 4 == 0) ? this._vertex2 : Color.Lerp(this._vertex2, this._vertex1, 0.5f)));
						break;
					}
					vh.SetUIVertex(uivertex, j);
				}
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00034C90 File Offset: 0x00032E90
		private bool CompareCarefully(Color col1, Color col2)
		{
			return Mathf.Abs(col1.r - col2.r) < 0.003f && Mathf.Abs(col1.g - col2.g) < 0.003f && Mathf.Abs(col1.b - col2.b) < 0.003f && Mathf.Abs(col1.a - col2.a) < 0.003f;
		}

		// Token: 0x04000B19 RID: 2841
		[SerializeField]
		private GradientMode _gradientMode;

		// Token: 0x04000B1A RID: 2842
		[SerializeField]
		private GradientDir _gradientDir;

		// Token: 0x04000B1B RID: 2843
		[SerializeField]
		private bool _overwriteAllColor;

		// Token: 0x04000B1C RID: 2844
		[SerializeField]
		private Color _vertex1 = Color.white;

		// Token: 0x04000B1D RID: 2845
		[SerializeField]
		private Color _vertex2 = Color.black;

		// Token: 0x04000B1E RID: 2846
		private Graphic targetGraphic;
	}
}
