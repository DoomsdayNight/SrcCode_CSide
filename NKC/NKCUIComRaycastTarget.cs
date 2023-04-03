using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200075B RID: 1883
	[RequireComponent(typeof(CanvasRenderer), typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class NKCUIComRaycastTarget : Graphic
	{
		// Token: 0x06004B35 RID: 19253 RVA: 0x001686E0 File Offset: 0x001668E0
		public override void SetMaterialDirty()
		{
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x001686E2 File Offset: 0x001668E2
		public override void SetVerticesDirty()
		{
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x001686E4 File Offset: 0x001668E4
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}
	}
}
