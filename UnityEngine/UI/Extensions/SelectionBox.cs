using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002CB RID: 715
	[RequireComponent(typeof(Canvas))]
	[AddComponentMenu("UI/Extensions/Selection Box")]
	public class SelectionBox : MonoBehaviour
	{
		// Token: 0x06000F5C RID: 3932 RVA: 0x00030224 File Offset: 0x0002E424
		private void ValidateCanvas()
		{
			if (base.gameObject.GetComponent<Canvas>().renderMode != RenderMode.ScreenSpaceOverlay)
			{
				throw new Exception("SelectionBox component must be placed on a canvas in Screen Space Overlay mode.");
			}
			CanvasScaler component = base.gameObject.GetComponent<CanvasScaler>();
			if (component && component.enabled && (!Mathf.Approximately(component.scaleFactor, 1f) || component.uiScaleMode != CanvasScaler.ScaleMode.ConstantPixelSize))
			{
				Object.Destroy(component);
				Debug.LogWarning("SelectionBox component is on a gameObject with a Canvas Scaler component. As of now, Canvas Scalers without the default settings throw off the coordinates of the selection box. Canvas Scaler has been removed.");
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00030294 File Offset: 0x0002E494
		private void SetSelectableGroup(IEnumerable<MonoBehaviour> behaviourCollection)
		{
			if (behaviourCollection == null)
			{
				this.selectableGroup = null;
				return;
			}
			List<MonoBehaviour> list = new List<MonoBehaviour>();
			foreach (MonoBehaviour monoBehaviour in behaviourCollection)
			{
				if (monoBehaviour is IBoxSelectable)
				{
					list.Add(monoBehaviour);
				}
			}
			this.selectableGroup = list.ToArray();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00030304 File Offset: 0x0002E504
		private void CreateBoxRect()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "Selection Box";
			gameObject.transform.parent = base.transform;
			gameObject.AddComponent<Image>();
			this.boxRect = (gameObject.transform as RectTransform);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003034C File Offset: 0x0002E54C
		private void ResetBoxRect()
		{
			Image component = this.boxRect.GetComponent<Image>();
			component.color = this.color;
			component.sprite = this.art;
			this.origin = Vector2.zero;
			this.boxRect.anchoredPosition = Vector2.zero;
			this.boxRect.sizeDelta = Vector2.zero;
			this.boxRect.anchorMax = Vector2.zero;
			this.boxRect.anchorMin = Vector2.zero;
			this.boxRect.pivot = Vector2.zero;
			this.boxRect.gameObject.SetActive(false);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000303E8 File Offset: 0x0002E5E8
		private void BeginSelection()
		{
			if (!UIExtensionsInputManager.GetMouseButtonDown(0))
			{
				return;
			}
			this.boxRect.gameObject.SetActive(true);
			this.origin = new Vector2(UIExtensionsInputManager.MousePosition.x, UIExtensionsInputManager.MousePosition.y);
			if (!this.PointIsValidAgainstSelectionMask(this.origin))
			{
				this.ResetBoxRect();
				return;
			}
			this.boxRect.anchoredPosition = this.origin;
			MonoBehaviour[] array;
			if (this.selectableGroup == null)
			{
				array = Object.FindObjectsOfType<MonoBehaviour>();
			}
			else
			{
				array = this.selectableGroup;
			}
			List<IBoxSelectable> list = new List<IBoxSelectable>();
			MonoBehaviour[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				IBoxSelectable boxSelectable = array2[i] as IBoxSelectable;
				if (boxSelectable != null)
				{
					list.Add(boxSelectable);
					if (!UIExtensionsInputManager.GetKey(KeyCode.LeftShift))
					{
						boxSelectable.selected = false;
					}
				}
			}
			this.selectables = list.ToArray();
			this.clickedBeforeDrag = this.GetSelectableAtMousePosition();
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000304C4 File Offset: 0x0002E6C4
		private bool PointIsValidAgainstSelectionMask(Vector2 screenPoint)
		{
			if (!this.selectionMask)
			{
				return true;
			}
			Camera screenPointCamera = this.GetScreenPointCamera(this.selectionMask);
			return RectTransformUtility.RectangleContainsScreenPoint(this.selectionMask, screenPoint, screenPointCamera);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000304FC File Offset: 0x0002E6FC
		private IBoxSelectable GetSelectableAtMousePosition()
		{
			if (!this.PointIsValidAgainstSelectionMask(UIExtensionsInputManager.MousePosition))
			{
				return null;
			}
			foreach (IBoxSelectable boxSelectable in this.selectables)
			{
				RectTransform rectTransform = boxSelectable.transform as RectTransform;
				if (rectTransform)
				{
					Camera screenPointCamera = this.GetScreenPointCamera(rectTransform);
					if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, UIExtensionsInputManager.MousePosition, screenPointCamera))
					{
						return boxSelectable;
					}
				}
				else
				{
					float magnitude = boxSelectable.transform.GetComponent<Renderer>().bounds.extents.magnitude;
					if (Vector2.Distance(this.GetScreenPointOfSelectable(boxSelectable), UIExtensionsInputManager.MousePosition) <= magnitude)
					{
						return boxSelectable;
					}
				}
			}
			return null;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x000305B0 File Offset: 0x0002E7B0
		private void DragSelection()
		{
			if (!UIExtensionsInputManager.GetMouseButton(0) || !this.boxRect.gameObject.activeSelf)
			{
				return;
			}
			Vector2 vector = new Vector2(UIExtensionsInputManager.MousePosition.x, UIExtensionsInputManager.MousePosition.y);
			Vector2 vector2 = vector - this.origin;
			Vector2 anchoredPosition = this.origin;
			if (vector2.x < 0f)
			{
				anchoredPosition.x = vector.x;
				vector2.x = -vector2.x;
			}
			if (vector2.y < 0f)
			{
				anchoredPosition.y = vector.y;
				vector2.y = -vector2.y;
			}
			this.boxRect.anchoredPosition = anchoredPosition;
			this.boxRect.sizeDelta = vector2;
			foreach (IBoxSelectable boxSelectable in this.selectables)
			{
				Vector3 v = this.GetScreenPointOfSelectable(boxSelectable);
				boxSelectable.preSelected = (RectTransformUtility.RectangleContainsScreenPoint(this.boxRect, v, null) && this.PointIsValidAgainstSelectionMask(v));
			}
			if (this.clickedBeforeDrag != null)
			{
				this.clickedBeforeDrag.preSelected = true;
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000306E0 File Offset: 0x0002E8E0
		private void ApplySingleClickDeselection()
		{
			if (this.clickedBeforeDrag == null)
			{
				return;
			}
			if (this.clickedAfterDrag != null && this.clickedBeforeDrag.selected && this.clickedBeforeDrag.transform == this.clickedAfterDrag.transform)
			{
				this.clickedBeforeDrag.selected = false;
				this.clickedBeforeDrag.preSelected = false;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00030740 File Offset: 0x0002E940
		private void ApplyPreSelections()
		{
			foreach (IBoxSelectable boxSelectable in this.selectables)
			{
				if (boxSelectable.preSelected)
				{
					boxSelectable.selected = true;
					boxSelectable.preSelected = false;
				}
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003077C File Offset: 0x0002E97C
		private Vector2 GetScreenPointOfSelectable(IBoxSelectable selectable)
		{
			RectTransform rectTransform = selectable.transform as RectTransform;
			if (rectTransform)
			{
				return RectTransformUtility.WorldToScreenPoint(this.GetScreenPointCamera(rectTransform), selectable.transform.position);
			}
			return Camera.main.WorldToScreenPoint(selectable.transform.position);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000307D0 File Offset: 0x0002E9D0
		private Camera GetScreenPointCamera(RectTransform rectTransform)
		{
			RectTransform rectTransform2 = rectTransform;
			Canvas canvas;
			do
			{
				canvas = rectTransform2.GetComponent<Canvas>();
				if (canvas && !canvas.isRootCanvas)
				{
					canvas = null;
				}
				rectTransform2 = (RectTransform)rectTransform2.parent;
			}
			while (canvas == null);
			switch (canvas.renderMode)
			{
			case RenderMode.ScreenSpaceOverlay:
				return null;
			case RenderMode.ScreenSpaceCamera:
				if (!canvas.worldCamera)
				{
					return Camera.main;
				}
				return canvas.worldCamera;
			}
			return Camera.main;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003084C File Offset: 0x0002EA4C
		public IBoxSelectable[] GetAllSelected()
		{
			if (this.selectables == null)
			{
				return new IBoxSelectable[0];
			}
			List<IBoxSelectable> list = new List<IBoxSelectable>();
			foreach (IBoxSelectable boxSelectable in this.selectables)
			{
				if (boxSelectable.selected)
				{
					list.Add(boxSelectable);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003089C File Offset: 0x0002EA9C
		private void EndSelection()
		{
			if (!UIExtensionsInputManager.GetMouseButtonUp(0) || !this.boxRect.gameObject.activeSelf)
			{
				return;
			}
			this.clickedAfterDrag = this.GetSelectableAtMousePosition();
			this.ApplySingleClickDeselection();
			this.ApplyPreSelections();
			this.ResetBoxRect();
			this.onSelectionChange.Invoke(this.GetAllSelected());
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000308F3 File Offset: 0x0002EAF3
		private void Start()
		{
			this.ValidateCanvas();
			this.CreateBoxRect();
			this.ResetBoxRect();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00030907 File Offset: 0x0002EB07
		private void Update()
		{
			this.BeginSelection();
			this.DragSelection();
			this.EndSelection();
		}

		// Token: 0x04000AB6 RID: 2742
		public Color color;

		// Token: 0x04000AB7 RID: 2743
		public Sprite art;

		// Token: 0x04000AB8 RID: 2744
		private Vector2 origin;

		// Token: 0x04000AB9 RID: 2745
		public RectTransform selectionMask;

		// Token: 0x04000ABA RID: 2746
		private RectTransform boxRect;

		// Token: 0x04000ABB RID: 2747
		private IBoxSelectable[] selectables;

		// Token: 0x04000ABC RID: 2748
		private MonoBehaviour[] selectableGroup;

		// Token: 0x04000ABD RID: 2749
		private IBoxSelectable clickedBeforeDrag;

		// Token: 0x04000ABE RID: 2750
		private IBoxSelectable clickedAfterDrag;

		// Token: 0x04000ABF RID: 2751
		public SelectionBox.SelectionEvent onSelectionChange = new SelectionBox.SelectionEvent();

		// Token: 0x02001139 RID: 4409
		public class SelectionEvent : UnityEvent<IBoxSelectable[]>
		{
		}
	}
}
