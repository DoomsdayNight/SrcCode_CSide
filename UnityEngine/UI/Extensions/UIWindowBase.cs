using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200032F RID: 815
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/UI Window Base")]
	public class UIWindowBase : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06001319 RID: 4889 RVA: 0x00047350 File Offset: 0x00045550
		private void Start()
		{
			if (this.RootTransform == null)
			{
				this.RootTransform = base.GetComponent<RectTransform>();
			}
			this.m_originalCoods = this.RootTransform.position;
			this.m_canvas = base.GetComponentInParent<Canvas>();
			this.m_canvasRectTransform = this.m_canvas.GetComponent<RectTransform>();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000473A5 File Offset: 0x000455A5
		private void Update()
		{
			if (UIWindowBase.ResetCoords)
			{
				this.resetCoordinatePosition();
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000473B4 File Offset: 0x000455B4
		public void OnDrag(PointerEventData eventData)
		{
			if (this._isDragging)
			{
				Vector3 b = this.ScreenToCanvas(eventData.position) - this.ScreenToCanvas(eventData.position - eventData.delta);
				this.RootTransform.localPosition += b;
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00047414 File Offset: 0x00045614
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.pointerCurrentRaycast.gameObject == null)
			{
				return;
			}
			if (eventData.pointerCurrentRaycast.gameObject.name == base.name)
			{
				this._isDragging = true;
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004745F File Offset: 0x0004565F
		public void OnEndDrag(PointerEventData eventData)
		{
			this._isDragging = false;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00047468 File Offset: 0x00045668
		private void resetCoordinatePosition()
		{
			this.RootTransform.position = this.m_originalCoods;
			UIWindowBase.ResetCoords = false;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00047484 File Offset: 0x00045684
		private Vector3 ScreenToCanvas(Vector3 screenPosition)
		{
			Vector2 sizeDelta = this.m_canvasRectTransform.sizeDelta;
			Vector3 vector;
			Vector2 vector2;
			Vector2 vector3;
			if (this.m_canvas.renderMode == RenderMode.ScreenSpaceOverlay || (this.m_canvas.renderMode == RenderMode.ScreenSpaceCamera && this.m_canvas.worldCamera == null))
			{
				vector = screenPosition;
				vector2 = Vector2.zero;
				vector3 = sizeDelta;
			}
			else
			{
				Ray ray = this.m_canvas.worldCamera.ScreenPointToRay(screenPosition);
				Plane plane = new Plane(this.m_canvasRectTransform.forward, this.m_canvasRectTransform.position);
				float d;
				if (!plane.Raycast(ray, out d))
				{
					throw new Exception("Is it practically possible?");
				}
				Vector3 position = ray.origin + ray.direction * d;
				vector = this.m_canvasRectTransform.InverseTransformPoint(position);
				vector2 = -Vector2.Scale(sizeDelta, this.m_canvasRectTransform.pivot);
				vector3 = Vector2.Scale(sizeDelta, Vector2.one - this.m_canvasRectTransform.pivot);
			}
			vector.x = Mathf.Clamp(vector.x, vector2.x + (float)this.KeepWindowInCanvas, vector3.x - (float)this.KeepWindowInCanvas);
			vector.y = Mathf.Clamp(vector.y, vector2.y + (float)this.KeepWindowInCanvas, vector3.y - (float)this.KeepWindowInCanvas);
			return vector;
		}

		// Token: 0x04000D4A RID: 3402
		private bool _isDragging;

		// Token: 0x04000D4B RID: 3403
		public static bool ResetCoords;

		// Token: 0x04000D4C RID: 3404
		private Vector3 m_originalCoods = Vector3.zero;

		// Token: 0x04000D4D RID: 3405
		private Canvas m_canvas;

		// Token: 0x04000D4E RID: 3406
		private RectTransform m_canvasRectTransform;

		// Token: 0x04000D4F RID: 3407
		[Tooltip("Number of pixels of the window that must stay inside the canvas view.")]
		public int KeepWindowInCanvas = 5;

		// Token: 0x04000D50 RID: 3408
		[Tooltip("The transform that is moved when dragging, can be left empty in which case its own transform is used.")]
		public RectTransform RootTransform;
	}
}
