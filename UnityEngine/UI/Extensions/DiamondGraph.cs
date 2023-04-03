using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200031D RID: 797
	[AddComponentMenu("UI/Extensions/Primitives/Diamond Graph")]
	public class DiamondGraph : UIPrimitiveBase
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00041D66 File Offset: 0x0003FF66
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x00041D6E File Offset: 0x0003FF6E
		public float A
		{
			get
			{
				return this.m_a;
			}
			set
			{
				this.m_a = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00041D77 File Offset: 0x0003FF77
		// (set) Token: 0x0600124C RID: 4684 RVA: 0x00041D7F File Offset: 0x0003FF7F
		public float B
		{
			get
			{
				return this.m_b;
			}
			set
			{
				this.m_b = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00041D88 File Offset: 0x0003FF88
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x00041D90 File Offset: 0x0003FF90
		public float C
		{
			get
			{
				return this.m_c;
			}
			set
			{
				this.m_c = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00041D99 File Offset: 0x0003FF99
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x00041DA1 File Offset: 0x0003FFA1
		public float D
		{
			get
			{
				return this.m_d;
			}
			set
			{
				this.m_d = value;
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00041DAC File Offset: 0x0003FFAC
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			float num = base.rectTransform.rect.width / 2f;
			this.m_a = Math.Min(1f, Math.Max(0f, this.m_a));
			this.m_b = Math.Min(1f, Math.Max(0f, this.m_b));
			this.m_c = Math.Min(1f, Math.Max(0f, this.m_c));
			this.m_d = Math.Min(1f, Math.Max(0f, this.m_d));
			Color32 color = this.color;
			vh.AddVert(new Vector3(-num * this.m_a, 0f), color, new Vector2(0f, 0f));
			vh.AddVert(new Vector3(0f, num * this.m_b), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(num * this.m_c, 0f), color, new Vector2(1f, 1f));
			vh.AddVert(new Vector3(0f, -num * this.m_d), color, new Vector2(1f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x04000CA0 RID: 3232
		[SerializeField]
		private float m_a = 1f;

		// Token: 0x04000CA1 RID: 3233
		[SerializeField]
		private float m_b = 1f;

		// Token: 0x04000CA2 RID: 3234
		[SerializeField]
		private float m_c = 1f;

		// Token: 0x04000CA3 RID: 3235
		[SerializeField]
		private float m_d = 1f;
	}
}
