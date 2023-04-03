using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D1 RID: 721
	[AddComponentMenu("UI/Effects/Extensions/BestFit Outline")]
	public class BestFitOutline : Shadow
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00032C19 File Offset: 0x00030E19
		protected BestFitOutline()
		{
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00032C24 File Offset: 0x00030E24
		public override void ModifyMesh(Mesh mesh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			using (VertexHelper vertexHelper = new VertexHelper(mesh))
			{
				vertexHelper.GetUIVertexStream(list);
			}
			Text component = base.GetComponent<Text>();
			float num = 1f;
			if (component && component.resizeTextForBestFit)
			{
				num = (float)component.cachedTextGenerator.fontSizeUsedForBestFit / (float)(component.resizeTextMaxSize - 1);
			}
			int start = 0;
			int count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x * num, base.effectDistance.y * num);
			start = count;
			int count2 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x * num, -base.effectDistance.y * num);
			start = count2;
			int count3 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, -base.effectDistance.x * num, base.effectDistance.y * num);
			start = count3;
			int count4 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, -base.effectDistance.x * num, -base.effectDistance.y * num);
			using (VertexHelper vertexHelper2 = new VertexHelper())
			{
				vertexHelper2.AddUIVertexTriangleStream(list);
				vertexHelper2.FillMesh(mesh);
			}
		}
	}
}
