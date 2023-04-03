using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002EA RID: 746
	[RequireComponent(typeof(RectTransform), typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Effects/Extensions/Flippable")]
	public class UIFlippable : BaseMeshEffect
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00037AE4 File Offset: 0x00035CE4
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00037AEC File Offset: 0x00035CEC
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00037AF5 File Offset: 0x00035CF5
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00037AFD File Offset: 0x00035CFD
		public bool vertical
		{
			get
			{
				return this.m_Veritical;
			}
			set
			{
				this.m_Veritical = value;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00037B08 File Offset: 0x00035D08
		public override void ModifyMesh(VertexHelper verts)
		{
			RectTransform rectTransform = base.transform as RectTransform;
			for (int i = 0; i < verts.currentVertCount; i++)
			{
				UIVertex uivertex = default(UIVertex);
				verts.PopulateUIVertex(ref uivertex, i);
				uivertex.position = new Vector3(this.m_Horizontal ? (uivertex.position.x + (rectTransform.rect.center.x - uivertex.position.x) * 2f) : uivertex.position.x, this.m_Veritical ? (uivertex.position.y + (rectTransform.rect.center.y - uivertex.position.y) * 2f) : uivertex.position.y, uivertex.position.z);
				verts.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x04000B58 RID: 2904
		[SerializeField]
		private bool m_Horizontal;

		// Token: 0x04000B59 RID: 2905
		[SerializeField]
		private bool m_Veritical;
	}
}
