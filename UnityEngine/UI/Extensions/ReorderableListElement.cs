using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C3 RID: 707
	[RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
	public class ReorderableListElement : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0002E17C File Offset: 0x0002C37C
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0002E184 File Offset: 0x0002C384
		public bool IsTransferable
		{
			get
			{
				return this._isTransferable;
			}
			set
			{
				this._canvasGroup = base.gameObject.GetOrAddComponent<CanvasGroup>();
				this._canvasGroup.blocksRaycasts = value;
				this._isTransferable = value;
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002E1AC File Offset: 0x0002C3AC
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!this._canvasGroup)
			{
				this._canvasGroup = base.gameObject.GetOrAddComponent<CanvasGroup>();
			}
			this._canvasGroup.blocksRaycasts = false;
			this.isValid = true;
			if (this._reorderableList == null)
			{
				return;
			}
			if (!this._reorderableList.IsDraggable || !this.IsGrabbable)
			{
				this._draggingObject = null;
				return;
			}
			if (!this._reorderableList.CloneDraggedObject)
			{
				this._draggingObject = this._rect;
				this._fromIndex = this._rect.GetSiblingIndex();
				this._displacedFromIndex = -1;
				if (this._reorderableList.OnElementRemoved != null)
				{
					UnityEvent<ReorderableList.ReorderableListEventStruct> onElementRemoved = this._reorderableList.OnElementRemoved;
					ReorderableList.ReorderableListEventStruct arg = new ReorderableList.ReorderableListEventStruct
					{
						DroppedObject = this._draggingObject.gameObject,
						IsAClone = this._reorderableList.CloneDraggedObject,
						SourceObject = (this._reorderableList.CloneDraggedObject ? base.gameObject : this._draggingObject.gameObject),
						FromList = this._reorderableList,
						FromIndex = this._fromIndex
					};
					onElementRemoved.Invoke(arg);
				}
				if (!this.isValid)
				{
					this._draggingObject = null;
					return;
				}
			}
			else
			{
				GameObject gameObject = Object.Instantiate<GameObject>(base.gameObject);
				this._draggingObject = gameObject.GetComponent<RectTransform>();
			}
			this._draggingObjectOriginalSize = base.gameObject.GetComponent<RectTransform>().rect.size;
			this._draggingObjectLE = this._draggingObject.GetComponent<LayoutElement>();
			this._draggingObject.SetParent(this._reorderableList.DraggableArea, true);
			this._draggingObject.SetAsLastSibling();
			this._reorderableList.Refresh();
			this._fakeElement = new GameObject("Fake").AddComponent<RectTransform>();
			this._fakeElementLE = this._fakeElement.gameObject.AddComponent<LayoutElement>();
			this.RefreshSizes();
			if (this._reorderableList.OnElementGrabbed != null)
			{
				UnityEvent<ReorderableList.ReorderableListEventStruct> onElementGrabbed = this._reorderableList.OnElementGrabbed;
				ReorderableList.ReorderableListEventStruct arg = new ReorderableList.ReorderableListEventStruct
				{
					DroppedObject = this._draggingObject.gameObject,
					IsAClone = this._reorderableList.CloneDraggedObject,
					SourceObject = (this._reorderableList.CloneDraggedObject ? base.gameObject : this._draggingObject.gameObject),
					FromList = this._reorderableList,
					FromIndex = this._fromIndex
				};
				onElementGrabbed.Invoke(arg);
				if (!this.isValid)
				{
					this.CancelDrag();
					return;
				}
			}
			this._isDragging = true;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002E430 File Offset: 0x0002C630
		public void OnDrag(PointerEventData eventData)
		{
			if (!this._isDragging)
			{
				return;
			}
			if (!this.isValid)
			{
				this.CancelDrag();
				return;
			}
			Canvas componentInParent = this._draggingObject.GetComponentInParent<Canvas>();
			Vector3 vector;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(componentInParent.GetComponent<RectTransform>(), eventData.position, (componentInParent.renderMode != RenderMode.ScreenSpaceOverlay) ? componentInParent.worldCamera : null, out vector);
			this._draggingObject.position = vector;
			ReorderableList currentReorderableListRaycasted = this._currentReorderableListRaycasted;
			EventSystem.current.RaycastAll(eventData, this._raycastResults);
			for (int i = 0; i < this._raycastResults.Count; i++)
			{
				this._currentReorderableListRaycasted = this._raycastResults[i].gameObject.GetComponent<ReorderableList>();
				if (this._currentReorderableListRaycasted != null)
				{
					break;
				}
			}
			if (this._currentReorderableListRaycasted == null || !this._currentReorderableListRaycasted.IsDropable || (((this._fakeElement.parent == this._currentReorderableListRaycasted.Content) ? (this._currentReorderableListRaycasted.Content.childCount - 1) : this._currentReorderableListRaycasted.Content.childCount) >= this._currentReorderableListRaycasted.maxItems && !this._currentReorderableListRaycasted.IsDisplacable) || this._currentReorderableListRaycasted.maxItems <= 0)
			{
				this.RefreshSizes();
				this._fakeElement.transform.SetParent(this._reorderableList.DraggableArea, false);
				if (this._displacedObject != null)
				{
					this.revertDisplacedElement();
					return;
				}
			}
			else
			{
				if (this._currentReorderableListRaycasted.Content.childCount < this._currentReorderableListRaycasted.maxItems && this._fakeElement.parent != this._currentReorderableListRaycasted.Content)
				{
					this._fakeElement.SetParent(this._currentReorderableListRaycasted.Content, false);
				}
				float num = float.PositiveInfinity;
				int num2 = 0;
				float num3 = 0f;
				for (int j = 0; j < this._currentReorderableListRaycasted.Content.childCount; j++)
				{
					RectTransform component = this._currentReorderableListRaycasted.Content.GetChild(j).GetComponent<RectTransform>();
					if (this._currentReorderableListRaycasted.ContentLayout is VerticalLayoutGroup)
					{
						num3 = Mathf.Abs(component.position.y - vector.y);
					}
					else if (this._currentReorderableListRaycasted.ContentLayout is HorizontalLayoutGroup)
					{
						num3 = Mathf.Abs(component.position.x - vector.x);
					}
					else if (this._currentReorderableListRaycasted.ContentLayout is GridLayoutGroup)
					{
						num3 = Mathf.Abs(component.position.x - vector.x) + Mathf.Abs(component.position.y - vector.y);
					}
					if (num3 < num)
					{
						num = num3;
						num2 = j;
					}
				}
				if ((this._currentReorderableListRaycasted != currentReorderableListRaycasted || num2 != this._displacedFromIndex) && this._currentReorderableListRaycasted.Content.childCount == this._currentReorderableListRaycasted.maxItems)
				{
					Transform child = this._currentReorderableListRaycasted.Content.GetChild(num2);
					if (this._displacedObject != null)
					{
						this.revertDisplacedElement();
						if (this._currentReorderableListRaycasted.Content.childCount > this._currentReorderableListRaycasted.maxItems)
						{
							this.displaceElement(num2, child);
						}
					}
					else if (this._fakeElement.parent != this._currentReorderableListRaycasted.Content)
					{
						this._fakeElement.SetParent(this._currentReorderableListRaycasted.Content, false);
						this.displaceElement(num2, child);
					}
				}
				this.RefreshSizes();
				this._fakeElement.SetSiblingIndex(num2);
				this._fakeElement.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002E7EC File Offset: 0x0002C9EC
		private void displaceElement(int targetIndex, Transform displaced)
		{
			this._displacedFromIndex = targetIndex;
			this._displacedObjectOriginList = this._currentReorderableListRaycasted;
			this._displacedObject = displaced.GetComponent<RectTransform>();
			this._displacedObjectLE = this._displacedObject.GetComponent<LayoutElement>();
			this._displacedObjectOriginalSize = this._displacedObject.rect.size;
			ReorderableList.ReorderableListEventStruct arg = new ReorderableList.ReorderableListEventStruct
			{
				DroppedObject = this._displacedObject.gameObject,
				FromList = this._currentReorderableListRaycasted,
				FromIndex = targetIndex
			};
			int num = (this._fakeElement.parent == this._reorderableList.Content) ? (this._reorderableList.Content.childCount - 1) : this._reorderableList.Content.childCount;
			if (this._reorderableList.IsDropable && num < this._reorderableList.maxItems && this._displacedObject.GetComponent<ReorderableListElement>().IsTransferable)
			{
				this._displacedObjectLE.preferredWidth = this._draggingObjectOriginalSize.x;
				this._displacedObjectLE.preferredHeight = this._draggingObjectOriginalSize.y;
				this._displacedObject.SetParent(this._reorderableList.Content, false);
				this._displacedObject.rotation = this._reorderableList.transform.rotation;
				this._displacedObject.SetSiblingIndex(this._fromIndex);
				this._reorderableList.Refresh();
				this._currentReorderableListRaycasted.Refresh();
				arg.ToList = this._reorderableList;
				arg.ToIndex = this._fromIndex;
				this._reorderableList.OnElementDisplacedTo.Invoke(arg);
				this._reorderableList.OnElementAdded.Invoke(arg);
			}
			else if (this._displacedObject.GetComponent<ReorderableListElement>().isDroppableInSpace)
			{
				this._displacedObject.SetParent(this._currentReorderableListRaycasted.DraggableArea, true);
				this._currentReorderableListRaycasted.Refresh();
				this._displacedObject.position += new Vector3(this._draggingObjectOriginalSize.x / 2f, this._draggingObjectOriginalSize.y / 2f, 0f);
			}
			else
			{
				this._displacedObject.SetParent(null, true);
				this._displacedObjectOriginList.Refresh();
				this._displacedObject.gameObject.SetActive(false);
			}
			this._displacedObjectOriginList.OnElementDisplacedFrom.Invoke(arg);
			this._reorderableList.OnElementRemoved.Invoke(arg);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002EA74 File Offset: 0x0002CC74
		private void revertDisplacedElement()
		{
			ReorderableList.ReorderableListEventStruct reorderableListEventStruct = new ReorderableList.ReorderableListEventStruct
			{
				DroppedObject = this._displacedObject.gameObject,
				FromList = this._displacedObjectOriginList,
				FromIndex = this._displacedFromIndex
			};
			if (this._displacedObject.parent != null)
			{
				reorderableListEventStruct.ToList = this._reorderableList;
				reorderableListEventStruct.ToIndex = this._fromIndex;
			}
			this._displacedObjectLE.preferredWidth = this._displacedObjectOriginalSize.x;
			this._displacedObjectLE.preferredHeight = this._displacedObjectOriginalSize.y;
			this._displacedObject.SetParent(this._displacedObjectOriginList.Content, false);
			this._displacedObject.rotation = this._displacedObjectOriginList.transform.rotation;
			this._displacedObject.SetSiblingIndex(this._displacedFromIndex);
			this._displacedObject.gameObject.SetActive(true);
			this._reorderableList.Refresh();
			this._displacedObjectOriginList.Refresh();
			if (reorderableListEventStruct.ToList != null)
			{
				this._reorderableList.OnElementDisplacedToReturned.Invoke(reorderableListEventStruct);
				this._reorderableList.OnElementRemoved.Invoke(reorderableListEventStruct);
			}
			this._displacedObjectOriginList.OnElementDisplacedFromReturned.Invoke(reorderableListEventStruct);
			this._displacedObjectOriginList.OnElementAdded.Invoke(reorderableListEventStruct);
			this._displacedFromIndex = -1;
			this._displacedObjectOriginList = null;
			this._displacedObject = null;
			this._displacedObjectLE = null;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		public void finishDisplacingElement()
		{
			if (this._displacedObject.parent == null)
			{
				Object.Destroy(this._displacedObject.gameObject);
			}
			this._displacedFromIndex = -1;
			this._displacedObjectOriginList = null;
			this._displacedObject = null;
			this._displacedObjectLE = null;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002EC34 File Offset: 0x0002CE34
		public void OnEndDrag(PointerEventData eventData)
		{
			this._isDragging = false;
			if (this._draggingObject != null)
			{
				if (this._currentReorderableListRaycasted != null && this._fakeElement.parent == this._currentReorderableListRaycasted.Content)
				{
					ReorderableList.ReorderableListEventStruct reorderableListEventStruct = new ReorderableList.ReorderableListEventStruct
					{
						DroppedObject = this._draggingObject.gameObject,
						IsAClone = this._reorderableList.CloneDraggedObject,
						SourceObject = (this._reorderableList.CloneDraggedObject ? base.gameObject : this._draggingObject.gameObject),
						FromList = this._reorderableList,
						FromIndex = this._fromIndex,
						ToList = this._currentReorderableListRaycasted,
						ToIndex = this._fakeElement.GetSiblingIndex()
					};
					ReorderableList.ReorderableListEventStruct arg = reorderableListEventStruct;
					if (this._reorderableList && this._reorderableList.OnElementDropped != null)
					{
						this._reorderableList.OnElementDropped.Invoke(arg);
					}
					if (!this.isValid)
					{
						this.CancelDrag();
						return;
					}
					this.RefreshSizes();
					this._draggingObject.SetParent(this._currentReorderableListRaycasted.Content, false);
					this._draggingObject.rotation = this._currentReorderableListRaycasted.transform.rotation;
					this._draggingObject.SetSiblingIndex(this._fakeElement.GetSiblingIndex());
					if (this.IsTransferable)
					{
						this._draggingObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
					}
					this._reorderableList.Refresh();
					this._currentReorderableListRaycasted.Refresh();
					this._reorderableList.OnElementAdded.Invoke(arg);
					if (this._displacedObject != null)
					{
						this.finishDisplacingElement();
					}
					if (!this.isValid)
					{
						throw new Exception("It's too late to cancel the Transfer! Do so in OnElementDropped!");
					}
				}
				else
				{
					if (this.isDroppableInSpace)
					{
						UnityEvent<ReorderableList.ReorderableListEventStruct> onElementDropped = this._reorderableList.OnElementDropped;
						ReorderableList.ReorderableListEventStruct reorderableListEventStruct = new ReorderableList.ReorderableListEventStruct
						{
							DroppedObject = this._draggingObject.gameObject,
							IsAClone = this._reorderableList.CloneDraggedObject,
							SourceObject = (this._reorderableList.CloneDraggedObject ? base.gameObject : this._draggingObject.gameObject),
							FromList = this._reorderableList,
							FromIndex = this._fromIndex
						};
						onElementDropped.Invoke(reorderableListEventStruct);
					}
					else
					{
						this.CancelDrag();
					}
					if (this._currentReorderableListRaycasted != null && ((this._currentReorderableListRaycasted.Content.childCount >= this._currentReorderableListRaycasted.maxItems && !this._currentReorderableListRaycasted.IsDisplacable) || this._currentReorderableListRaycasted.maxItems <= 0))
					{
						GameObject gameObject = this._draggingObject.gameObject;
						UnityEvent<ReorderableList.ReorderableListEventStruct> onElementDroppedWithMaxItems = this._reorderableList.OnElementDroppedWithMaxItems;
						ReorderableList.ReorderableListEventStruct reorderableListEventStruct = new ReorderableList.ReorderableListEventStruct
						{
							DroppedObject = gameObject,
							IsAClone = this._reorderableList.CloneDraggedObject,
							SourceObject = (this._reorderableList.CloneDraggedObject ? base.gameObject : gameObject),
							FromList = this._reorderableList,
							ToList = this._currentReorderableListRaycasted,
							FromIndex = this._fromIndex
						};
						onElementDroppedWithMaxItems.Invoke(reorderableListEventStruct);
					}
				}
			}
			if (this._fakeElement != null)
			{
				Object.Destroy(this._fakeElement.gameObject);
				this._fakeElement = null;
			}
			this._canvasGroup.blocksRaycasts = true;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002EF9C File Offset: 0x0002D19C
		private void CancelDrag()
		{
			this._isDragging = false;
			if (this._reorderableList.CloneDraggedObject)
			{
				Object.Destroy(this._draggingObject.gameObject);
			}
			else
			{
				this.RefreshSizes();
				this._draggingObject.SetParent(this._reorderableList.Content, false);
				this._draggingObject.rotation = this._reorderableList.Content.transform.rotation;
				this._draggingObject.SetSiblingIndex(this._fromIndex);
				ReorderableList.ReorderableListEventStruct arg = new ReorderableList.ReorderableListEventStruct
				{
					DroppedObject = this._draggingObject.gameObject,
					IsAClone = this._reorderableList.CloneDraggedObject,
					SourceObject = (this._reorderableList.CloneDraggedObject ? base.gameObject : this._draggingObject.gameObject),
					FromList = this._reorderableList,
					FromIndex = this._fromIndex,
					ToList = this._reorderableList,
					ToIndex = this._fromIndex
				};
				this._reorderableList.Refresh();
				this._reorderableList.OnElementAdded.Invoke(arg);
				if (!this.isValid)
				{
					throw new Exception("Transfer is already Canceled.");
				}
			}
			if (this._fakeElement != null)
			{
				Object.Destroy(this._fakeElement.gameObject);
				this._fakeElement = null;
			}
			if (this._displacedObject != null)
			{
				this.revertDisplacedElement();
			}
			this._canvasGroup.blocksRaycasts = true;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002F120 File Offset: 0x0002D320
		private void RefreshSizes()
		{
			Vector2 vector = this._draggingObjectOriginalSize;
			if (this._currentReorderableListRaycasted != null && this._currentReorderableListRaycasted.IsDropable && this._currentReorderableListRaycasted.Content.childCount > 0 && this._currentReorderableListRaycasted.EqualizeSizesOnDrag)
			{
				Transform child = this._currentReorderableListRaycasted.Content.GetChild(0);
				if (child != null)
				{
					vector = child.GetComponent<RectTransform>().rect.size;
				}
			}
			this._draggingObject.sizeDelta = vector;
			this._fakeElementLE.preferredHeight = (this._draggingObjectLE.preferredHeight = vector.y);
			this._fakeElementLE.preferredWidth = (this._draggingObjectLE.preferredWidth = vector.x);
			this._fakeElement.GetComponent<RectTransform>().sizeDelta = vector;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002F1F9 File Offset: 0x0002D3F9
		public void Init(ReorderableList reorderableList)
		{
			this._reorderableList = reorderableList;
			this._rect = base.GetComponent<RectTransform>();
			this._canvasGroup = base.gameObject.GetOrAddComponent<CanvasGroup>();
		}

		// Token: 0x04000A80 RID: 2688
		[Tooltip("Can this element be dragged?")]
		[SerializeField]
		private bool IsGrabbable = true;

		// Token: 0x04000A81 RID: 2689
		[Tooltip("Can this element be transfered to another list")]
		[SerializeField]
		private bool _isTransferable = true;

		// Token: 0x04000A82 RID: 2690
		[Tooltip("Can this element be dropped in space?")]
		[SerializeField]
		private bool isDroppableInSpace;

		// Token: 0x04000A83 RID: 2691
		private readonly List<RaycastResult> _raycastResults = new List<RaycastResult>();

		// Token: 0x04000A84 RID: 2692
		private ReorderableList _currentReorderableListRaycasted;

		// Token: 0x04000A85 RID: 2693
		private int _fromIndex;

		// Token: 0x04000A86 RID: 2694
		private RectTransform _draggingObject;

		// Token: 0x04000A87 RID: 2695
		private LayoutElement _draggingObjectLE;

		// Token: 0x04000A88 RID: 2696
		private Vector2 _draggingObjectOriginalSize;

		// Token: 0x04000A89 RID: 2697
		private RectTransform _fakeElement;

		// Token: 0x04000A8A RID: 2698
		private LayoutElement _fakeElementLE;

		// Token: 0x04000A8B RID: 2699
		private int _displacedFromIndex;

		// Token: 0x04000A8C RID: 2700
		private RectTransform _displacedObject;

		// Token: 0x04000A8D RID: 2701
		private LayoutElement _displacedObjectLE;

		// Token: 0x04000A8E RID: 2702
		private Vector2 _displacedObjectOriginalSize;

		// Token: 0x04000A8F RID: 2703
		private ReorderableList _displacedObjectOriginList;

		// Token: 0x04000A90 RID: 2704
		private bool _isDragging;

		// Token: 0x04000A91 RID: 2705
		private RectTransform _rect;

		// Token: 0x04000A92 RID: 2706
		private ReorderableList _reorderableList;

		// Token: 0x04000A93 RID: 2707
		private CanvasGroup _canvasGroup;

		// Token: 0x04000A94 RID: 2708
		internal bool isValid;
	}
}
