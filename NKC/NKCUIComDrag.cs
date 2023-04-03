using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x02000758 RID: 1880
	public class NKCUIComDrag : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06004B1F RID: 19231 RVA: 0x00168109 File Offset: 0x00166309
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.BeginDrag != null)
			{
				this.BeginDrag.Invoke(eventData);
			}
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x0016811F File Offset: 0x0016631F
		public void OnDrag(PointerEventData eventData)
		{
			if (this.Drag != null)
			{
				this.Drag.Invoke(eventData);
			}
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x00168135 File Offset: 0x00166335
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.EndDrag != null)
			{
				this.EndDrag.Invoke(eventData);
			}
		}

		// Token: 0x040039C9 RID: 14793
		public NKCUnityEvent BeginDrag = new NKCUnityEvent();

		// Token: 0x040039CA RID: 14794
		public NKCUnityEvent Drag = new NKCUnityEvent();

		// Token: 0x040039CB RID: 14795
		public NKCUnityEvent EndDrag = new NKCUnityEvent();
	}
}
