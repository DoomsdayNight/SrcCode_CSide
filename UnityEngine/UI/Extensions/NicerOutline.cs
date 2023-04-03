using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002DF RID: 735
	[AddComponentMenu("UI/Effects/Extensions/Nicer Outline")]
	public class NicerOutline : BaseMeshEffect
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003654D File Offset: 0x0003474D
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x00036555 File Offset: 0x00034755
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00036577 File Offset: 0x00034777
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x00036580 File Offset: 0x00034780
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00036620 File Offset: 0x00034820
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00036628 File Offset: 0x00034828
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003664C File Offset: 0x0003484C
		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count * 2;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00036700 File Offset: 0x00034900
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count * 2;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00036738 File Offset: 0x00034938
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_Verts.Clear();
			vh.GetUIVertexStream(this.m_Verts);
			Text component = base.GetComponent<Text>();
			float num = 1f;
			if (component && component.resizeTextForBestFit)
			{
				num = (float)component.cachedTextGenerator.fontSizeUsedForBestFit / (float)(component.resizeTextMaxSize - 1);
			}
			float num2 = this.effectDistance.x * num;
			float num3 = this.effectDistance.y * num;
			int start = 0;
			int count = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, num2, num3);
			start = count;
			int count2 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, num2, -num3);
			start = count2;
			int count3 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, -num2, num3);
			start = count3;
			int count4 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, -num2, -num3);
			start = count4;
			int count5 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, num2, 0f);
			start = count5;
			int count6 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, -num2, 0f);
			start = count6;
			int count7 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, 0f, num3);
			start = count7;
			int count8 = this.m_Verts.Count;
			this.ApplyShadow(this.m_Verts, this.effectColor, start, this.m_Verts.Count, 0f, -num3);
			vh.Clear();
			vh.AddUIVertexTriangleStream(this.m_Verts);
		}

		// Token: 0x04000B33 RID: 2867
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000B34 RID: 2868
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000B35 RID: 2869
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04000B36 RID: 2870
		private List<UIVertex> m_Verts = new List<UIVertex>();
	}
}
