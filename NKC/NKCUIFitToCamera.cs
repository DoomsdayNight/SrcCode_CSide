using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200076B RID: 1899
	[RequireComponent(typeof(RectTransform))]
	public class NKCUIFitToCamera : MonoBehaviour
	{
		// Token: 0x06004BD6 RID: 19414 RVA: 0x0016B0B8 File Offset: 0x001692B8
		private void Awake()
		{
			this.m_rect = base.GetComponent<RectTransform>();
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0016B0C6 File Offset: 0x001692C6
		private void Update()
		{
			NKCCamera.FitRectToCamera(this.m_rect);
		}

		// Token: 0x04003A51 RID: 14929
		private RectTransform m_rect;
	}
}
