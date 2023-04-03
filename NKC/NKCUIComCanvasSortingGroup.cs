using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000752 RID: 1874
	[RequireComponent(typeof(Canvas))]
	public class NKCUIComCanvasSortingGroup : MonoBehaviour
	{
		// Token: 0x06004B07 RID: 19207 RVA: 0x00167D0C File Offset: 0x00165F0C
		private void Awake()
		{
			Canvas component = base.GetComponent<Canvas>();
			if (component != null)
			{
				component.sortingLayerID = this.SortingLayer;
				component.sortingOrder = this.SortingOrder;
			}
		}

		// Token: 0x040039B9 RID: 14777
		public int SortingLayer;

		// Token: 0x040039BA RID: 14778
		public int SortingOrder;
	}
}
