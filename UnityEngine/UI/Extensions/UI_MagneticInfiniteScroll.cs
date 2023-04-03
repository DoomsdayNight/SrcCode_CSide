using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200034B RID: 843
	[AddComponentMenu("UI/Extensions/UI Magnetic Infinite Scroll")]
	[RequireComponent(typeof(ScrollRect))]
	public class UI_MagneticInfiniteScroll : UI_InfiniteScroll, IDragHandler, IEventSystemHandler, IEndDragHandler, IScrollHandler
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060013E4 RID: 5092 RVA: 0x0004A908 File Offset: 0x00048B08
		// (remove) Token: 0x060013E5 RID: 5093 RVA: 0x0004A940 File Offset: 0x00048B40
		public event Action<GameObject> OnNewSelect;

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0004A975 File Offset: 0x00048B75
		public List<RectTransform> Items { get; }

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004A97D File Offset: 0x00048B7D
		protected override void Awake()
		{
			base.Awake();
			base.StartCoroutine(this.SetInitContent());
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004A994 File Offset: 0x00048B94
		private void Update()
		{
			if (this._scrollRect == null || !this._scrollRect.content || !this.pivot || !this._useMagnetic || !this._isMovement || this.items == null)
			{
				return;
			}
			float rightAxis = this.GetRightAxis(this._scrollRect.content.anchoredPosition);
			this._currentSpeed = Mathf.Abs(rightAxis - this._pastPosition);
			this._pastPosition = rightAxis;
			if (Mathf.Abs(this._currentSpeed) > this.maxSpeedForMagnetic)
			{
				return;
			}
			if (this._isStopping)
			{
				Vector2 anchoredPosition = this._scrollRect.content.anchoredPosition;
				this._currentTime += Time.deltaTime;
				float t = this._currentTime / this.timeForDeceleration;
				float num = Mathf.Lerp(this.GetRightAxis(anchoredPosition), this._stopValue, t);
				this._scrollRect.content.anchoredPosition = (this._isVertical ? new Vector2(anchoredPosition.x, num) : new Vector2(num, anchoredPosition.y));
				if (num == this.GetRightAxis(anchoredPosition) && this._nearestIndex > 0 && this._nearestIndex < this.items.Count)
				{
					this._isStopping = false;
					this._isMovement = false;
					RectTransform rectTransform = this.items[this._nearestIndex];
					if (rectTransform != null && this.OnNewSelect != null)
					{
						this.OnNewSelect(rectTransform.gameObject);
						return;
					}
				}
			}
			else
			{
				float num2 = float.PositiveInfinity * -this._initMovementDirection;
				for (int i = 0; i < this.items.Count; i++)
				{
					RectTransform rectTransform2 = this.items[i];
					if (!(rectTransform2 == null))
					{
						float num3 = this.GetRightAxis(rectTransform2.position) - this.GetRightAxis(this.pivot.position);
						if ((this._initMovementDirection <= 0f && num3 < num2 && num3 > 0f) || (this._initMovementDirection > 0f && num3 > num2 && num3 < 0f))
						{
							num2 = num3;
							this._nearestIndex = i;
						}
					}
				}
				this._isStopping = true;
				this._stopValue = this.GetAnchoredPositionForPivot(this._nearestIndex);
				this._scrollRect.StopMovement();
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004AC04 File Offset: 0x00048E04
		public override void SetNewItems(ref List<Transform> newItems)
		{
			foreach (Transform transform in newItems)
			{
				RectTransform component = transform.GetComponent<RectTransform>();
				if (component && this.pivot)
				{
					component.sizeDelta = this.pivot.sizeDelta;
				}
			}
			base.SetNewItems(ref newItems);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004AC80 File Offset: 0x00048E80
		public void SetContentInPivot(int index)
		{
			float anchoredPositionForPivot = this.GetAnchoredPositionForPivot(index);
			Vector2 anchoredPosition = this._scrollRect.content.anchoredPosition;
			if (this._scrollRect.content)
			{
				this._scrollRect.content.anchoredPosition = (this._isVertical ? new Vector2(anchoredPosition.x, anchoredPositionForPivot) : new Vector2(anchoredPositionForPivot, anchoredPosition.y));
				this._pastPosition = this.GetRightAxis(this._scrollRect.content.anchoredPosition);
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004AD06 File Offset: 0x00048F06
		private IEnumerator SetInitContent()
		{
			yield return new WaitForSeconds(this._waitForContentSet);
			this.SetContentInPivot(this.indexStart);
			yield break;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004AD18 File Offset: 0x00048F18
		private float GetAnchoredPositionForPivot(int index)
		{
			if (!this.pivot || this.items == null || this.items.Count < 0)
			{
				return 0f;
			}
			index = Mathf.Clamp(index, 0, this.items.Count - 1);
			float rightAxis = this.GetRightAxis(this.items[index].anchoredPosition);
			return this.GetRightAxis(this.pivot.anchoredPosition) - rightAxis;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004AD8F File Offset: 0x00048F8F
		private void FinishPrepareMovement()
		{
			this._isMovement = true;
			this._useMagnetic = true;
			this._isStopping = false;
			this._currentTime = 0f;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004ADB1 File Offset: 0x00048FB1
		private float GetRightAxis(Vector2 vector)
		{
			if (!this._isVertical)
			{
				return vector.x;
			}
			return vector.y;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004ADC8 File Offset: 0x00048FC8
		public void OnDrag(PointerEventData eventData)
		{
			float rightAxis = this.GetRightAxis(UIExtensionsInputManager.MousePosition);
			this._initMovementDirection = Mathf.Sign(rightAxis - this._pastPositionMouseSpeed);
			this._pastPositionMouseSpeed = rightAxis;
			this._useMagnetic = false;
			this._isStopping = false;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004AE0E File Offset: 0x0004900E
		public void OnEndDrag(PointerEventData eventData)
		{
			this.FinishPrepareMovement();
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004AE16 File Offset: 0x00049016
		public void OnScroll(PointerEventData eventData)
		{
			this._initMovementDirection = -UIExtensionsInputManager.MouseScrollDelta.y;
			this.FinishPrepareMovement();
		}

		// Token: 0x04000DC4 RID: 3524
		[Tooltip("The pointer to the pivot, the visual element for centering objects.")]
		[SerializeField]
		private RectTransform pivot;

		// Token: 0x04000DC5 RID: 3525
		[Tooltip("The maximum speed that allows you to activate the magnet to center on the pivot")]
		[SerializeField]
		private float maxSpeedForMagnetic = 10f;

		// Token: 0x04000DC6 RID: 3526
		[SerializeField]
		[Tooltip("The index of the object which must be initially centered")]
		private int indexStart;

		// Token: 0x04000DC7 RID: 3527
		[SerializeField]
		[Tooltip("The time to decelerate and aim to the pivot")]
		private float timeForDeceleration = 0.05f;

		// Token: 0x04000DC8 RID: 3528
		private float _pastPositionMouseSpeed;

		// Token: 0x04000DC9 RID: 3529
		private float _initMovementDirection;

		// Token: 0x04000DCA RID: 3530
		private float _pastPosition;

		// Token: 0x04000DCB RID: 3531
		private float _currentSpeed;

		// Token: 0x04000DCC RID: 3532
		private float _stopValue;

		// Token: 0x04000DCD RID: 3533
		private readonly float _waitForContentSet = 0.1f;

		// Token: 0x04000DCE RID: 3534
		private float _currentTime;

		// Token: 0x04000DCF RID: 3535
		private int _nearestIndex;

		// Token: 0x04000DD0 RID: 3536
		private bool _useMagnetic = true;

		// Token: 0x04000DD1 RID: 3537
		private bool _isStopping;

		// Token: 0x04000DD2 RID: 3538
		private bool _isMovement;
	}
}
