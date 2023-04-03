using System;
using UnityEngine;

namespace NKC.UI.Component
{
	// Token: 0x02000C5D RID: 3165
	public class NKCUIComScreenPosConstraint : MonoBehaviour
	{
		// Token: 0x0600936A RID: 37738 RVA: 0x00325182 File Offset: 0x00323382
		private void LateUpdate()
		{
			this.SetPosition();
		}

		// Token: 0x0600936B RID: 37739 RVA: 0x0032518C File Offset: 0x0032338C
		private void SetPosition()
		{
			Vector2 screenPoint;
			screenPoint.x = (float)Screen.width * this.relativePosition.x + this.offset.x;
			screenPoint.y = (float)Screen.height * this.relativePosition.y + this.offset.y;
			Vector3 position;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCanvas), screenPoint, NKCCamera.GetSubUICamera(), out position);
			base.transform.position = position;
		}

		// Token: 0x0400805D RID: 32861
		public Vector2 relativePosition;

		// Token: 0x0400805E RID: 32862
		public Vector2 offset;
	}
}
