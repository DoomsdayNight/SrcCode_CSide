using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C5 RID: 709
	[AddComponentMenu("UI/Extensions/RescalePanels/RescalePanel")]
	public class RescalePanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x0002F3CC File Offset: 0x0002D5CC
		private void Awake()
		{
			this.rectTransform = base.transform.parent.GetComponent<RectTransform>();
			this.goTransform = base.transform.parent;
			this.thisRectTransform = base.GetComponent<RectTransform>();
			this.sizeDelta = this.thisRectTransform.sizeDelta;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002F41D File Offset: 0x0002D61D
		public void OnPointerDown(PointerEventData data)
		{
			this.rectTransform.SetAsLastSibling();
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, out this.previousPointerPosition);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002F448 File Offset: 0x0002D648
		public void OnDrag(PointerEventData data)
		{
			if (this.rectTransform == null)
			{
				return;
			}
			Vector3 vector = this.goTransform.localScale;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, out this.currentPointerPosition);
			Vector2 vector2 = this.currentPointerPosition - this.previousPointerPosition;
			vector += new Vector3(-vector2.y * 0.001f, -vector2.y * 0.001f, 0f);
			vector = new Vector3(Mathf.Clamp(vector.x, this.minSize.x, this.maxSize.x), Mathf.Clamp(vector.y, this.minSize.y, this.maxSize.y), 1f);
			this.goTransform.localScale = vector;
			this.previousPointerPosition = this.currentPointerPosition;
			float num = this.sizeDelta.x / this.goTransform.localScale.x;
			Vector2 vector3 = new Vector2(num, num);
			this.thisRectTransform.sizeDelta = vector3;
		}

		// Token: 0x04000A99 RID: 2713
		public Vector2 minSize;

		// Token: 0x04000A9A RID: 2714
		public Vector2 maxSize;

		// Token: 0x04000A9B RID: 2715
		private RectTransform rectTransform;

		// Token: 0x04000A9C RID: 2716
		private Transform goTransform;

		// Token: 0x04000A9D RID: 2717
		private Vector2 currentPointerPosition;

		// Token: 0x04000A9E RID: 2718
		private Vector2 previousPointerPosition;

		// Token: 0x04000A9F RID: 2719
		private RectTransform thisRectTransform;

		// Token: 0x04000AA0 RID: 2720
		private Vector2 sizeDelta;
	}
}
