using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200033B RID: 827
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/Scrollrect Conflict Manager")]
	public class ScrollConflictManager : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x0600136A RID: 4970 RVA: 0x00048B77 File Offset: 0x00046D77
		private void Awake()
		{
			if (this.ParentScrollRect)
			{
				this.InitialiseConflictManager();
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00048B8C File Offset: 0x00046D8C
		private void InitialiseConflictManager()
		{
			this._myScrollRect = base.GetComponent<ScrollRect>();
			this.scrollOtherHorizontally = this._myScrollRect.vertical;
			if (this.scrollOtherHorizontally)
			{
				if (this._myScrollRect.horizontal)
				{
					Debug.LogError("You have added the SecondScrollRect to a scroll view that already has both directions selected");
				}
				if (!this.ParentScrollRect.horizontal)
				{
					Debug.LogError("The other scroll rect does not support scrolling horizontally");
				}
			}
			else if (!this.ParentScrollRect.vertical)
			{
				Debug.LogError("The other scroll rect does not support scrolling vertically");
			}
			if (this.ParentScrollRect && !this.ParentScrollSnap)
			{
				this.ParentScrollSnap = this.ParentScrollRect.GetComponent<ScrollSnapBase>();
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00048C30 File Offset: 0x00046E30
		private void Start()
		{
			if (this.ParentScrollRect)
			{
				this.AssignScrollRectHandlers();
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00048C45 File Offset: 0x00046E45
		private void AssignScrollRectHandlers()
		{
			this._beginDragHandlers = this.ParentScrollRect.GetComponents<IBeginDragHandler>();
			this._dragHandlers = this.ParentScrollRect.GetComponents<IDragHandler>();
			this._endDragHandlers = this.ParentScrollRect.GetComponents<IEndDragHandler>();
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00048C7A File Offset: 0x00046E7A
		public void SetParentScrollRect(ScrollRect parentScrollRect)
		{
			this.ParentScrollRect = parentScrollRect;
			this.InitialiseConflictManager();
			this.AssignScrollRectHandlers();
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00048C90 File Offset: 0x00046E90
		public void OnBeginDrag(PointerEventData eventData)
		{
			float num = Mathf.Abs(eventData.position.x - eventData.pressPosition.x);
			float num2 = Mathf.Abs(eventData.position.y - eventData.pressPosition.y);
			if (this.scrollOtherHorizontally)
			{
				if (num > num2)
				{
					this.scrollOther = true;
					this._myScrollRect.enabled = false;
					int i = 0;
					int num3 = this._beginDragHandlers.Length;
					while (i < num3)
					{
						this._beginDragHandlers[i].OnBeginDrag(eventData);
						if (this.ParentScrollSnap)
						{
							this.ParentScrollSnap.OnBeginDrag(eventData);
						}
						i++;
					}
					return;
				}
			}
			else if (num2 > num)
			{
				this.scrollOther = true;
				this._myScrollRect.enabled = false;
				int j = 0;
				int num4 = this._beginDragHandlers.Length;
				while (j < num4)
				{
					this._beginDragHandlers[j].OnBeginDrag(eventData);
					if (this.ParentScrollSnap)
					{
						this.ParentScrollSnap.OnBeginDrag(eventData);
					}
					j++;
				}
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00048D90 File Offset: 0x00046F90
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.scrollOther)
			{
				this._myScrollRect.enabled = true;
				this.scrollOther = false;
				int i = 0;
				int num = this._endDragHandlers.Length;
				while (i < num)
				{
					this._endDragHandlers[i].OnEndDrag(eventData);
					if (this.ParentScrollSnap)
					{
						this.ParentScrollSnap.OnEndDrag(eventData);
					}
					i++;
				}
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00048DF4 File Offset: 0x00046FF4
		public void OnDrag(PointerEventData eventData)
		{
			if (this.scrollOther)
			{
				int i = 0;
				int num = this._endDragHandlers.Length;
				while (i < num)
				{
					this._dragHandlers[i].OnDrag(eventData);
					if (this.ParentScrollSnap)
					{
						this.ParentScrollSnap.OnDrag(eventData);
					}
					i++;
				}
			}
		}

		// Token: 0x04000D70 RID: 3440
		[Tooltip("The parent ScrollRect control hosting this ScrollSnap")]
		public ScrollRect ParentScrollRect;

		// Token: 0x04000D71 RID: 3441
		[Tooltip("The parent ScrollSnap control hosting this Scroll Snap.\nIf left empty, it will use the ScrollSnap of the ParentScrollRect")]
		private ScrollSnapBase ParentScrollSnap;

		// Token: 0x04000D72 RID: 3442
		private ScrollRect _myScrollRect;

		// Token: 0x04000D73 RID: 3443
		private IBeginDragHandler[] _beginDragHandlers;

		// Token: 0x04000D74 RID: 3444
		private IEndDragHandler[] _endDragHandlers;

		// Token: 0x04000D75 RID: 3445
		private IDragHandler[] _dragHandlers;

		// Token: 0x04000D76 RID: 3446
		private bool scrollOther;

		// Token: 0x04000D77 RID: 3447
		private bool scrollOtherHorizontally;
	}
}
