using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C4 RID: 708
	[AddComponentMenu("UI/Extensions/RescalePanels/RescaleDragPanel")]
	public class RescaleDragPanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x0002F240 File Offset: 0x0002D440
		private void Awake()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			if (componentInParent != null)
			{
				this.canvasRectTransform = (componentInParent.transform as RectTransform);
				this.panelRectTransform = (base.transform.parent as RectTransform);
				this.goTransform = base.transform.parent;
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002F295 File Offset: 0x0002D495
		public void OnPointerDown(PointerEventData data)
		{
			this.panelRectTransform.SetAsLastSibling();
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.panelRectTransform, data.position, data.pressEventCamera, out this.pointerOffset);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
		public void OnDrag(PointerEventData data)
		{
			if (this.panelRectTransform == null)
			{
				return;
			}
			Vector2 screenPoint = this.ClampToWindow(data);
			Vector2 a;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRectTransform, screenPoint, data.pressEventCamera, out a))
			{
				this.panelRectTransform.localPosition = a - new Vector2(this.pointerOffset.x * this.goTransform.localScale.x, this.pointerOffset.y * this.goTransform.localScale.y);
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002F350 File Offset: 0x0002D550
		private Vector2 ClampToWindow(PointerEventData data)
		{
			Vector2 position = data.position;
			Vector3[] array = new Vector3[4];
			this.canvasRectTransform.GetWorldCorners(array);
			float x = Mathf.Clamp(position.x, array[0].x, array[2].x);
			float y = Mathf.Clamp(position.y, array[0].y, array[2].y);
			return new Vector2(x, y);
		}

		// Token: 0x04000A95 RID: 2709
		private Vector2 pointerOffset;

		// Token: 0x04000A96 RID: 2710
		private RectTransform canvasRectTransform;

		// Token: 0x04000A97 RID: 2711
		private RectTransform panelRectTransform;

		// Token: 0x04000A98 RID: 2712
		private Transform goTransform;
	}
}
