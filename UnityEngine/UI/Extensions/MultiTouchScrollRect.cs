using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002BD RID: 701
	[AddComponentMenu("UI/Extensions/MultiTouchScrollRect")]
	public class MultiTouchScrollRect : ScrollRect
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002CDE2 File Offset: 0x0002AFE2
		public override void OnBeginDrag(PointerEventData eventData)
		{
			this.pid = eventData.pointerId;
			base.OnBeginDrag(eventData);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002CDF7 File Offset: 0x0002AFF7
		public override void OnDrag(PointerEventData eventData)
		{
			if (this.pid == eventData.pointerId)
			{
				base.OnDrag(eventData);
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002CE0E File Offset: 0x0002B00E
		public override void OnEndDrag(PointerEventData eventData)
		{
			this.pid = -100;
			base.OnEndDrag(eventData);
		}

		// Token: 0x04000A3E RID: 2622
		private int pid = -100;
	}
}
