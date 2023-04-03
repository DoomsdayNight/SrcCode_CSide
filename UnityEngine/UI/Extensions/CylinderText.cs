using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D8 RID: 728
	[RequireComponent(typeof(Text), typeof(RectTransform))]
	[AddComponentMenu("UI/Effects/Extensions/Cylinder Text")]
	public class CylinderText : BaseMeshEffect
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x00034819 File Offset: 0x00032A19
		protected override void Awake()
		{
			base.Awake();
			this.OnRectTransformDimensionsChange();
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00034827 File Offset: 0x00032A27
		protected override void OnEnable()
		{
			base.OnEnable();
			this.OnRectTransformDimensionsChange();
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00034838 File Offset: 0x00032A38
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			int currentVertCount = vh.currentVertCount;
			if (!this.IsActive() || currentVertCount == 0)
			{
				return;
			}
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				UIVertex uivertex = default(UIVertex);
				vh.PopulateUIVertex(ref uivertex, i);
				float x = uivertex.position.x;
				uivertex.position.z = -this.radius * Mathf.Cos(x / this.radius);
				uivertex.position.x = this.radius * Mathf.Sin(x / this.radius);
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x04000B18 RID: 2840
		public float radius;
	}
}
