using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000334 RID: 820
	[RequireComponent(typeof(EventSystem))]
	[AddComponentMenu("UI/Extensions/DragCorrector")]
	public class DragCorrector : MonoBehaviour
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x000487A4 File Offset: 0x000469A4
		private void Start()
		{
			this.dragTH = this.baseTH * (int)Screen.dpi / this.basePPI;
			EventSystem component = base.GetComponent<EventSystem>();
			if (component)
			{
				component.pixelDragThreshold = this.dragTH;
			}
		}

		// Token: 0x04000D66 RID: 3430
		public int baseTH = 6;

		// Token: 0x04000D67 RID: 3431
		public int basePPI = 210;

		// Token: 0x04000D68 RID: 3432
		public int dragTH;
	}
}
