using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x02000761 RID: 1889
	public class NKCUIComStateButton : NKCUIComStateButtonBase
	{
		// Token: 0x06004B66 RID: 19302 RVA: 0x001692C7 File Offset: 0x001674C7
		protected override void OnPointerDownEvent(PointerEventData eventData)
		{
			if (this.PointerDown != null)
			{
				this.PointerDown.Invoke(eventData);
			}
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x001692DD File Offset: 0x001674DD
		protected override void OnPointerUpEvent(PointerEventData eventData)
		{
			if (this.PointerUp != null)
			{
				this.PointerUp.Invoke();
			}
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x001692F2 File Offset: 0x001674F2
		protected override void OnPointerClickEvent(PointerEventData eventData)
		{
			if (this.PointerClick != null)
			{
				this.PointerClick.Invoke();
			}
			if (this.PointerClickWithData != null)
			{
				this.PointerClickWithData.Invoke(this.m_DataInt);
			}
		}

		// Token: 0x04003A03 RID: 14851
		public NKCUnityEvent PointerDown = new NKCUnityEvent();

		// Token: 0x04003A04 RID: 14852
		public UnityEvent PointerUp;

		// Token: 0x04003A05 RID: 14853
		public UnityEvent PointerClick;

		// Token: 0x04003A06 RID: 14854
		public NKCUnityEventInt PointerClickWithData;
	}
}
