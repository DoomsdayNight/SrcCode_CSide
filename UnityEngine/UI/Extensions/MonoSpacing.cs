using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002DE RID: 734
	[AddComponentMenu("UI/Effects/Extensions/Mono Spacing")]
	[RequireComponent(typeof(Text))]
	[RequireComponent(typeof(RectTransform))]
	public class MonoSpacing : BaseMeshEffect
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x000360EB File Offset: 0x000342EB
		protected MonoSpacing()
		{
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000360FE File Offset: 0x000342FE
		protected override void Awake()
		{
			this.text = base.GetComponent<Text>();
			if (this.text == null)
			{
				Debug.LogWarning("MonoSpacing: Missing Text component");
				return;
			}
			this.rectTransform = this.text.GetComponent<RectTransform>();
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00036136 File Offset: 0x00034336
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0003613E File Offset: 0x0003433E
		public float Spacing
		{
			get
			{
				return this.m_spacing;
			}
			set
			{
				if (this.m_spacing == value)
				{
					return;
				}
				this.m_spacing = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003616C File Offset: 0x0003436C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			string[] array = this.text.text.Split(new char[]
			{
				'\n'
			});
			float num = this.Spacing * (float)this.text.fontSize / 100f;
			float num2 = 0f;
			int num3 = 0;
			switch (this.text.alignment)
			{
			case TextAnchor.UpperLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.LowerLeft:
				num2 = 0f;
				break;
			case TextAnchor.UpperCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.LowerCenter:
				num2 = 0.5f;
				break;
			case TextAnchor.UpperRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.LowerRight:
				num2 = 1f;
				break;
			}
			foreach (string text in array)
			{
				float num4 = -((float)(text.Length - 1) * num * num2 - (num2 - 0.5f) * this.rectTransform.rect.width) + num / 2f * (1f - num2 * 2f);
				for (int j = 0; j < text.Length; j++)
				{
					int index = num3 * 6;
					int index2 = num3 * 6 + 1;
					int index3 = num3 * 6 + 2;
					int index4 = num3 * 6 + 3;
					int index5 = num3 * 6 + 4;
					int num5 = num3 * 6 + 5;
					if (num5 > list.Count - 1)
					{
						return;
					}
					UIVertex uivertex = list[index];
					UIVertex uivertex2 = list[index2];
					UIVertex uivertex3 = list[index3];
					UIVertex uivertex4 = list[index4];
					UIVertex uivertex5 = list[index5];
					UIVertex uivertex6 = list[num5];
					float x = (uivertex2.position - uivertex.position).x;
					bool flag = this.UseHalfCharWidth && x < this.HalfCharWidth;
					float num6 = flag ? (-num / 4f) : 0f;
					uivertex.position += new Vector3(-uivertex.position.x + num4 + -0.5f * x + num6, 0f, 0f);
					uivertex2.position += new Vector3(-uivertex2.position.x + num4 + 0.5f * x + num6, 0f, 0f);
					uivertex3.position += new Vector3(-uivertex3.position.x + num4 + 0.5f * x + num6, 0f, 0f);
					uivertex4.position += new Vector3(-uivertex4.position.x + num4 + 0.5f * x + num6, 0f, 0f);
					uivertex5.position += new Vector3(-uivertex5.position.x + num4 + -0.5f * x + num6, 0f, 0f);
					uivertex6.position += new Vector3(-uivertex6.position.x + num4 + -0.5f * x + num6, 0f, 0f);
					if (flag)
					{
						num4 += num / 2f;
					}
					else
					{
						num4 += num;
					}
					list[index] = uivertex;
					list[index2] = uivertex2;
					list[index3] = uivertex3;
					list[index4] = uivertex4;
					list[index5] = uivertex5;
					list[num5] = uivertex6;
					num3++;
				}
				num3++;
			}
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}

		// Token: 0x04000B2E RID: 2862
		[SerializeField]
		private float m_spacing;

		// Token: 0x04000B2F RID: 2863
		public float HalfCharWidth = 1f;

		// Token: 0x04000B30 RID: 2864
		public bool UseHalfCharWidth;

		// Token: 0x04000B31 RID: 2865
		private RectTransform rectTransform;

		// Token: 0x04000B32 RID: 2866
		private Text text;
	}
}
