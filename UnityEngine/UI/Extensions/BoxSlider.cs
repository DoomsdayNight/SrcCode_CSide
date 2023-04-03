using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B3 RID: 691
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/BoxSlider")]
	public class BoxSlider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0002A1DF File Offset: 0x000283DF
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0002A1E7 File Offset: 0x000283E7
		public RectTransform HandleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (BoxSlider.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0002A203 File Offset: 0x00028403
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x0002A20B File Offset: 0x0002840B
		public float MinValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (BoxSlider.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.SetX(this.m_ValueX);
					this.SetY(this.m_ValueY);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0002A239 File Offset: 0x00028439
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x0002A241 File Offset: 0x00028441
		public float MaxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (BoxSlider.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.SetX(this.m_ValueX);
					this.SetY(this.m_ValueY);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0002A26F File Offset: 0x0002846F
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0002A277 File Offset: 0x00028477
		public bool WholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (BoxSlider.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.SetX(this.m_ValueX);
					this.SetY(this.m_ValueY);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0002A2A5 File Offset: 0x000284A5
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0002A2C1 File Offset: 0x000284C1
		public float ValueX
		{
			get
			{
				if (this.WholeNumbers)
				{
					return Mathf.Round(this.m_ValueX);
				}
				return this.m_ValueX;
			}
			set
			{
				this.SetX(value);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0002A2CA File Offset: 0x000284CA
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x0002A2FC File Offset: 0x000284FC
		public float NormalizedValueX
		{
			get
			{
				if (Mathf.Approximately(this.MinValue, this.MaxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.MinValue, this.MaxValue, this.ValueX);
			}
			set
			{
				this.ValueX = Mathf.Lerp(this.MinValue, this.MaxValue, value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002A316 File Offset: 0x00028516
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x0002A332 File Offset: 0x00028532
		public float ValueY
		{
			get
			{
				if (this.WholeNumbers)
				{
					return Mathf.Round(this.m_ValueY);
				}
				return this.m_ValueY;
			}
			set
			{
				this.SetY(value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002A33B File Offset: 0x0002853B
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x0002A36D File Offset: 0x0002856D
		public float NormalizedValueY
		{
			get
			{
				if (Mathf.Approximately(this.MinValue, this.MaxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.MinValue, this.MaxValue, this.ValueY);
			}
			set
			{
				this.ValueY = Mathf.Lerp(this.MinValue, this.MaxValue, value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0002A387 File Offset: 0x00028587
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x0002A38F File Offset: 0x0002858F
		public BoxSlider.BoxSliderEvent OnValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0002A398 File Offset: 0x00028598
		private float StepSize
		{
			get
			{
				if (!this.WholeNumbers)
				{
					return (this.MaxValue - this.MinValue) * 0.1f;
				}
				return 1f;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002A3BB File Offset: 0x000285BB
		protected BoxSlider()
		{
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002A3FA File Offset: 0x000285FA
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002A3FC File Offset: 0x000285FC
		public void LayoutComplete()
		{
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002A3FE File Offset: 0x000285FE
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002A400 File Offset: 0x00028600
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002A44D File Offset: 0x0002864D
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002A46D File Offset: 0x0002866D
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.SetX(this.m_ValueX, false);
			this.SetY(this.m_ValueY, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002A49B File Offset: 0x0002869B
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002A4B0 File Offset: 0x000286B0
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
					return;
				}
			}
			else
			{
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0002A50C File Offset: 0x0002870C
		private void SetX(float input)
		{
			this.SetX(input, true);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0002A518 File Offset: 0x00028718
		private void SetX(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.MinValue, this.MaxValue);
			if (this.WholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_ValueX == num)
			{
				return;
			}
			this.m_ValueX = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(num, this.ValueY);
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002A573 File Offset: 0x00028773
		private void SetY(float input)
		{
			this.SetY(input, true);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002A580 File Offset: 0x00028780
		private void SetY(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.MinValue, this.MaxValue);
			if (this.WholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_ValueY == num)
			{
				return;
			}
			this.m_ValueY = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(this.ValueX, num);
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0002A5DB File Offset: 0x000287DB
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this.UpdateVisuals();
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002A5EC File Offset: 0x000287EC
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				zero[0] = (one[0] = this.NormalizedValueX);
				zero[1] = (one[1] = this.NormalizedValueY);
				if (Application.isPlaying)
				{
					this.m_HandleRect.anchorMin = zero;
					this.m_HandleRect.anchorMax = one;
				}
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002A688 File Offset: 0x00028888
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform handleContainerRect = this.m_HandleContainerRect;
			if (handleContainerRect != null && handleContainerRect.rect.size[0] > 0f)
			{
				Vector2 a;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(handleContainerRect, eventData.position, cam, out a))
				{
					return;
				}
				a -= handleContainerRect.rect.position;
				float normalizedValueX = Mathf.Clamp01((a - this.m_Offset)[0] / handleContainerRect.rect.size[0]);
				this.NormalizedValueX = normalizedValueX;
				float normalizedValueY = Mathf.Clamp01((a - this.m_Offset)[1] / handleContainerRect.rect.size[1]);
				this.NormalizedValueY = normalizedValueY;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0002A768 File Offset: 0x00028968
		private bool CanDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0002A788 File Offset: 0x00028988
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.CanDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 offset;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out offset))
				{
					this.m_Offset = offset;
				}
				this.m_Offset.y = -this.m_Offset.y;
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0002A81F File Offset: 0x00028A1F
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.CanDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002A838 File Offset: 0x00028A38
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002A841 File Offset: 0x00028A41
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040009BB RID: 2491
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x040009BC RID: 2492
		[Space(6f)]
		[SerializeField]
		private float m_MinValue;

		// Token: 0x040009BD RID: 2493
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x040009BE RID: 2494
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x040009BF RID: 2495
		[SerializeField]
		private float m_ValueX = 1f;

		// Token: 0x040009C0 RID: 2496
		[SerializeField]
		private float m_ValueY = 1f;

		// Token: 0x040009C1 RID: 2497
		[Space(6f)]
		[SerializeField]
		private BoxSlider.BoxSliderEvent m_OnValueChanged = new BoxSlider.BoxSliderEvent();

		// Token: 0x040009C2 RID: 2498
		private Transform m_HandleTransform;

		// Token: 0x040009C3 RID: 2499
		private RectTransform m_HandleContainerRect;

		// Token: 0x040009C4 RID: 2500
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x040009C5 RID: 2501
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02001122 RID: 4386
		public enum Direction
		{
			// Token: 0x040091A4 RID: 37284
			LeftToRight,
			// Token: 0x040091A5 RID: 37285
			RightToLeft,
			// Token: 0x040091A6 RID: 37286
			BottomToTop,
			// Token: 0x040091A7 RID: 37287
			TopToBottom
		}

		// Token: 0x02001123 RID: 4387
		[Serializable]
		public class BoxSliderEvent : UnityEvent<float, float>
		{
		}

		// Token: 0x02001124 RID: 4388
		private enum Axis
		{
			// Token: 0x040091A9 RID: 37289
			Horizontal,
			// Token: 0x040091AA RID: 37290
			Vertical
		}
	}
}
