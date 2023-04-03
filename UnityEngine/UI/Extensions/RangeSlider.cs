using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002BF RID: 703
	[AddComponentMenu("UI/Extensions/Range Slider", 34)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class RangeSlider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0002D299 File Offset: 0x0002B499
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0002D2A1 File Offset: 0x0002B4A1
		public RectTransform FillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (RangeSlider.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002D2BD File Offset: 0x0002B4BD
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0002D2C5 File Offset: 0x0002B4C5
		public RectTransform LowHandleRect
		{
			get
			{
				return this.m_LowHandleRect;
			}
			set
			{
				if (RangeSlider.SetClass<RectTransform>(ref this.m_LowHandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002D2E1 File Offset: 0x0002B4E1
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0002D2E9 File Offset: 0x0002B4E9
		public RectTransform HighHandleRect
		{
			get
			{
				return this.m_HighHandleRect;
			}
			set
			{
				if (RangeSlider.SetClass<RectTransform>(ref this.m_HighHandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002D305 File Offset: 0x0002B505
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x0002D30D File Offset: 0x0002B50D
		public float MinValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (RangeSlider.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.SetLow(this.m_LowValue);
					this.SetHigh(this.m_HighValue);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0002D33B File Offset: 0x0002B53B
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x0002D343 File Offset: 0x0002B543
		public float MaxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (RangeSlider.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.SetLow(this.m_LowValue);
					this.SetHigh(this.m_HighValue);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0002D371 File Offset: 0x0002B571
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0002D379 File Offset: 0x0002B579
		public bool WholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (RangeSlider.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.SetLow(this.m_LowValue);
					this.SetHigh(this.m_HighValue);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0002D3A7 File Offset: 0x0002B5A7
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0002D3C3 File Offset: 0x0002B5C3
		public virtual float LowValue
		{
			get
			{
				if (this.WholeNumbers)
				{
					return Mathf.Round(this.m_LowValue);
				}
				return this.m_LowValue;
			}
			set
			{
				this.SetLow(value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0002D3CC File Offset: 0x0002B5CC
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0002D3FE File Offset: 0x0002B5FE
		public float NormalizedLowValue
		{
			get
			{
				if (Mathf.Approximately(this.MinValue, this.MaxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.MinValue, this.MaxValue, this.LowValue);
			}
			set
			{
				this.LowValue = Mathf.Lerp(this.MinValue, this.MaxValue, value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0002D418 File Offset: 0x0002B618
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x0002D434 File Offset: 0x0002B634
		public virtual float HighValue
		{
			get
			{
				if (this.WholeNumbers)
				{
					return Mathf.Round(this.m_HighValue);
				}
				return this.m_HighValue;
			}
			set
			{
				this.SetHigh(value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0002D43D File Offset: 0x0002B63D
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0002D46F File Offset: 0x0002B66F
		public float NormalizedHighValue
		{
			get
			{
				if (Mathf.Approximately(this.MinValue, this.MaxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.MinValue, this.MaxValue, this.HighValue);
			}
			set
			{
				this.HighValue = Mathf.Lerp(this.MinValue, this.MaxValue, value);
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002D489 File Offset: 0x0002B689
		public virtual void SetValueWithoutNotify(float low, float high)
		{
			this.SetLow(low, false);
			this.SetHigh(high, false);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0002D49B File Offset: 0x0002B69B
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0002D4A3 File Offset: 0x0002B6A3
		public RangeSlider.RangeSliderEvent OnValueChanged
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0002D4AC File Offset: 0x0002B6AC
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

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002D4CF File Offset: 0x0002B6CF
		protected RangeSlider()
		{
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002D50A File Offset: 0x0002B70A
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002D50C File Offset: 0x0002B70C
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002D50E File Offset: 0x0002B70E
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002D510 File Offset: 0x0002B710
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002D55D File Offset: 0x0002B75D
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002D57D File Offset: 0x0002B77D
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.SetLow(this.LowValue, false);
			this.SetHigh(this.HighValue, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002D5AB File Offset: 0x0002B7AB
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002D5BE File Offset: 0x0002B7BE
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.UpdateVisuals();
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002D5D5 File Offset: 0x0002B7D5
		protected override void OnDidApplyAnimationProperties()
		{
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect && this.m_FillRect != (RectTransform)base.transform)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillRect = null;
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HighHandleRect && this.m_HighHandleRect != (RectTransform)base.transform)
			{
				this.m_HighHandleTransform = this.m_HighHandleRect.transform;
				if (this.m_HighHandleTransform.parent != null)
				{
					this.m_HighHandleContainerRect = this.m_HighHandleTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_HighHandleRect = null;
				this.m_HighHandleContainerRect = null;
			}
			if (this.m_LowHandleRect && this.m_LowHandleRect != (RectTransform)base.transform)
			{
				this.m_LowHandleTransform = this.m_LowHandleRect.transform;
				if (this.m_LowHandleTransform.parent != null)
				{
					this.m_LowHandleContainerRect = this.m_LowHandleTransform.parent.GetComponent<RectTransform>();
					return;
				}
			}
			else
			{
				this.m_LowHandleRect = null;
				this.m_LowHandleContainerRect = null;
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002D751 File Offset: 0x0002B951
		private void SetLow(float input)
		{
			this.SetLow(input, true);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002D75C File Offset: 0x0002B95C
		protected virtual void SetLow(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.MinValue, this.HighValue);
			if (this.WholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_LowValue == num)
			{
				return;
			}
			this.m_LowValue = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("RangeSlider.lowValue", this);
				this.m_OnValueChanged.Invoke(num, this.HighValue);
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002D7C2 File Offset: 0x0002B9C2
		private void SetHigh(float input)
		{
			this.SetHigh(input, true);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002D7CC File Offset: 0x0002B9CC
		protected virtual void SetHigh(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.LowValue, this.MaxValue);
			if (this.WholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_HighValue == num)
			{
				return;
			}
			this.m_HighValue = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("RangeSlider.highValue", this);
				this.m_OnValueChanged.Invoke(this.LowValue, num);
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002D832 File Offset: 0x0002BA32
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002D84C File Offset: 0x0002BA4C
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				zero[0] = this.NormalizedLowValue;
				one[0] = this.NormalizedHighValue;
				this.m_FillRect.anchorMin = zero;
				this.m_FillRect.anchorMax = one;
			}
			if (this.m_LowHandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_LowHandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero2 = Vector2.zero;
				Vector2 one2 = Vector2.one;
				zero2[0] = (one2[0] = this.NormalizedLowValue);
				this.m_LowHandleRect.anchorMin = zero2;
				this.m_LowHandleRect.anchorMax = one2;
			}
			if (this.m_HighHandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HighHandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero3 = Vector2.zero;
				Vector2 one3 = Vector2.one;
				zero3[0] = (one3[0] = this.NormalizedHighValue);
				this.m_HighHandleRect.anchorMin = zero3;
				this.m_HighHandleRect.anchorMax = one3;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002D998 File Offset: 0x0002BB98
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			switch (this.interactionState)
			{
			case RangeSlider.InteractionState.Low:
				this.NormalizedLowValue = this.CalculateDrag(eventData, cam, this.m_LowHandleContainerRect, this.m_LowOffset);
				return;
			case RangeSlider.InteractionState.High:
				this.NormalizedHighValue = this.CalculateDrag(eventData, cam, this.m_HighHandleContainerRect, this.m_HighOffset);
				return;
			case RangeSlider.InteractionState.Bar:
				this.CalculateBarDrag(eventData, cam);
				break;
			case RangeSlider.InteractionState.None:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002DA04 File Offset: 0x0002BC04
		private float CalculateDrag(PointerEventData eventData, Camera cam, RectTransform containerRect, Vector2 offset)
		{
			RectTransform rectTransform = containerRect ?? this.m_FillContainerRect;
			if (!(rectTransform != null) || rectTransform.rect.size[0] <= 0f)
			{
				return 0f;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, cam, out a))
			{
				return 0f;
			}
			a -= rectTransform.rect.position;
			return Mathf.Clamp01((a - offset)[0] / rectTransform.rect.size[0]);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002DAA4 File Offset: 0x0002BCA4
		private void CalculateBarDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform fillContainerRect = this.m_FillContainerRect;
			if (fillContainerRect != null && fillContainerRect.rect.size[0] > 0f)
			{
				Vector2 a;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(fillContainerRect, eventData.position, cam, out a))
				{
					return;
				}
				a -= fillContainerRect.rect.position;
				if (this.NormalizedLowValue >= 0f && this.NormalizedHighValue <= 1f)
				{
					float num = (this.NormalizedHighValue + this.NormalizedLowValue) / 2f;
					float num2 = Mathf.Clamp01(a[0] / fillContainerRect.rect.size[0]) - num;
					if (this.NormalizedLowValue + num2 < 0f)
					{
						num2 = -this.NormalizedLowValue;
					}
					else if (this.NormalizedHighValue + num2 > 1f)
					{
						num2 = 1f - this.NormalizedHighValue;
					}
					this.NormalizedLowValue += num2;
					this.NormalizedHighValue += num2;
				}
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002DBC2 File Offset: 0x0002BDC2
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_LowOffset = (this.m_HighOffset = Vector2.zero);
			if (this.m_HighHandleRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HighHandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HighHandleRect, eventData.position, eventData.pressEventCamera, out vector))
				{
					this.m_HighOffset = vector;
				}
				this.interactionState = RangeSlider.InteractionState.High;
				if (base.transition == Selectable.Transition.ColorTint)
				{
					base.targetGraphic = this.m_HighHandleRect.GetComponent<Graphic>();
				}
			}
			else if (this.m_LowHandleRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_LowHandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_LowHandleRect, eventData.position, eventData.pressEventCamera, out vector))
				{
					this.m_LowOffset = vector;
				}
				this.interactionState = RangeSlider.InteractionState.Low;
				if (base.transition == Selectable.Transition.ColorTint)
				{
					base.targetGraphic = this.m_LowHandleRect.GetComponent<Graphic>();
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
				if (eventData.pointerCurrentRaycast.gameObject == this.m_FillRect.gameObject)
				{
					this.interactionState = RangeSlider.InteractionState.Bar;
				}
				if (base.transition == Selectable.Transition.ColorTint)
				{
					base.targetGraphic = this.m_FillImage;
				}
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002DD39 File Offset: 0x0002BF39
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002DD52 File Offset: 0x0002BF52
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.interactionState = RangeSlider.InteractionState.None;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002DD62 File Offset: 0x0002BF62
		public override void OnMove(AxisEventData eventData)
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002DD64 File Offset: 0x0002BF64
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002DD6D File Offset: 0x0002BF6D
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000A51 RID: 2641
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x04000A52 RID: 2642
		[SerializeField]
		private RectTransform m_LowHandleRect;

		// Token: 0x04000A53 RID: 2643
		[SerializeField]
		private RectTransform m_HighHandleRect;

		// Token: 0x04000A54 RID: 2644
		[Space]
		[SerializeField]
		private float m_MinValue;

		// Token: 0x04000A55 RID: 2645
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x04000A56 RID: 2646
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x04000A57 RID: 2647
		[SerializeField]
		private float m_LowValue;

		// Token: 0x04000A58 RID: 2648
		[SerializeField]
		private float m_HighValue;

		// Token: 0x04000A59 RID: 2649
		[Space]
		[SerializeField]
		private RangeSlider.RangeSliderEvent m_OnValueChanged = new RangeSlider.RangeSliderEvent();

		// Token: 0x04000A5A RID: 2650
		private RangeSlider.InteractionState interactionState = RangeSlider.InteractionState.None;

		// Token: 0x04000A5B RID: 2651
		private Image m_FillImage;

		// Token: 0x04000A5C RID: 2652
		private Transform m_FillTransform;

		// Token: 0x04000A5D RID: 2653
		private RectTransform m_FillContainerRect;

		// Token: 0x04000A5E RID: 2654
		private Transform m_HighHandleTransform;

		// Token: 0x04000A5F RID: 2655
		private RectTransform m_HighHandleContainerRect;

		// Token: 0x04000A60 RID: 2656
		private Transform m_LowHandleTransform;

		// Token: 0x04000A61 RID: 2657
		private RectTransform m_LowHandleContainerRect;

		// Token: 0x04000A62 RID: 2658
		private Vector2 m_LowOffset = Vector2.zero;

		// Token: 0x04000A63 RID: 2659
		private Vector2 m_HighOffset = Vector2.zero;

		// Token: 0x04000A64 RID: 2660
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000A65 RID: 2661
		private bool m_DelayedUpdateVisuals;

		// Token: 0x02001131 RID: 4401
		[Serializable]
		public class RangeSliderEvent : UnityEvent<float, float>
		{
		}

		// Token: 0x02001132 RID: 4402
		private enum InteractionState
		{
			// Token: 0x040091B4 RID: 37300
			Low,
			// Token: 0x040091B5 RID: 37301
			High,
			// Token: 0x040091B6 RID: 37302
			Bar,
			// Token: 0x040091B7 RID: 37303
			None
		}
	}
}
