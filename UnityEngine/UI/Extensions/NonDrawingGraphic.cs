using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000337 RID: 823
	[AddComponentMenu("Layout/Extensions/NonDrawingGraphic")]
	public class NonDrawingGraphic : MaskableGraphic
	{
		// Token: 0x06001358 RID: 4952 RVA: 0x00048901 File Offset: 0x00046B01
		public override void SetMaterialDirty()
		{
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00048903 File Offset: 0x00046B03
		public override void SetVerticesDirty()
		{
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00048905 File Offset: 0x00046B05
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}
	}
}
