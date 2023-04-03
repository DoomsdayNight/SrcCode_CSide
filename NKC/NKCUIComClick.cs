using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x02000754 RID: 1876
	public class NKCUIComClick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler
	{
		// Token: 0x06004B16 RID: 19222 RVA: 0x00167F80 File Offset: 0x00166180
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.PointerDown != null)
			{
				this.PointerDown.Invoke();
			}
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x00167F95 File Offset: 0x00166195
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.PointerUp != null)
			{
				this.PointerUp.Invoke();
			}
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x00167FAA File Offset: 0x001661AA
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.PointerClick != null)
			{
				this.PointerClick.Invoke();
			}
		}

		// Token: 0x040039C2 RID: 14786
		public UnityEvent PointerDown;

		// Token: 0x040039C3 RID: 14787
		public UnityEvent PointerUp;

		// Token: 0x040039C4 RID: 14788
		public UnityEvent PointerClick;
	}
}
