using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000313 RID: 787
	[DisallowMultipleComponent]
	public class ScrollSnapScrollbarHelper : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x00040274 File Offset: 0x0003E474
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.OnScrollBarDown();
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004027C File Offset: 0x0003E47C
		public void OnDrag(PointerEventData eventData)
		{
			this.ss.CurrentPage();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004028A File Offset: 0x0003E48A
		public void OnEndDrag(PointerEventData eventData)
		{
			this.OnScrollBarUp();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00040292 File Offset: 0x0003E492
		public void OnPointerDown(PointerEventData eventData)
		{
			this.OnScrollBarDown();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004029A File Offset: 0x0003E49A
		public void OnPointerUp(PointerEventData eventData)
		{
			this.OnScrollBarUp();
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000402A2 File Offset: 0x0003E4A2
		private void OnScrollBarDown()
		{
			if (this.ss != null)
			{
				this.ss.SetLerp(false);
				this.ss.StartScreenChange();
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000402C3 File Offset: 0x0003E4C3
		private void OnScrollBarUp()
		{
			this.ss.SetLerp(true);
			this.ss.ChangePage(this.ss.CurrentPage());
		}

		// Token: 0x04000C7B RID: 3195
		internal IScrollSnap ss;
	}
}
