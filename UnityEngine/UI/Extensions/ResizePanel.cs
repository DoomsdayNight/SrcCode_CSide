using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C6 RID: 710
	[AddComponentMenu("UI/Extensions/RescalePanels/ResizePanel")]
	public class ResizePanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0002F56C File Offset: 0x0002D76C
		private void Awake()
		{
			this.rectTransform = base.transform.parent.GetComponent<RectTransform>();
			float width = this.rectTransform.rect.width;
			float height = this.rectTransform.rect.height;
			this.ratio = height / width;
			this.minSize = new Vector2(0.1f * width, 0.1f * height);
			this.maxSize = new Vector2(10f * width, 10f * height);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002F5F2 File Offset: 0x0002D7F2
		public void OnPointerDown(PointerEventData data)
		{
			this.rectTransform.SetAsLastSibling();
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, out this.previousPointerPosition);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002F620 File Offset: 0x0002D820
		public void OnDrag(PointerEventData data)
		{
			if (this.rectTransform == null)
			{
				return;
			}
			Vector2 vector = this.rectTransform.sizeDelta;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, out this.currentPointerPosition);
			Vector2 vector2 = this.currentPointerPosition - this.previousPointerPosition;
			vector += new Vector2(vector2.x, this.ratio * vector2.x);
			vector = new Vector2(Mathf.Clamp(vector.x, this.minSize.x, this.maxSize.x), Mathf.Clamp(vector.y, this.minSize.y, this.maxSize.y));
			this.rectTransform.sizeDelta = vector;
			this.previousPointerPosition = this.currentPointerPosition;
		}

		// Token: 0x04000AA1 RID: 2721
		public Vector2 minSize;

		// Token: 0x04000AA2 RID: 2722
		public Vector2 maxSize;

		// Token: 0x04000AA3 RID: 2723
		private RectTransform rectTransform;

		// Token: 0x04000AA4 RID: 2724
		private Vector2 currentPointerPosition;

		// Token: 0x04000AA5 RID: 2725
		private Vector2 previousPointerPosition;

		// Token: 0x04000AA6 RID: 2726
		private float ratio;
	}
}
