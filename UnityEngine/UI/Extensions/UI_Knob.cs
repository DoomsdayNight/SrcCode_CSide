using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002CF RID: 719
	[RequireComponent(typeof(Image))]
	[AddComponentMenu("UI/Extensions/UI_Knob")]
	public class UI_Knob : Selectable, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IInitializePotentialDragHandler
	{
		// Token: 0x06000FAD RID: 4013 RVA: 0x000324E1 File Offset: 0x000306E1
		protected override void Awake()
		{
			this._screenSpaceOverlay = (base.GetComponentInParent<Canvas>().rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000324FC File Offset: 0x000306FC
		protected override void Start()
		{
			this.CheckForParentTouchMask();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00032504 File Offset: 0x00030704
		private void CheckForParentTouchMask()
		{
			if (this.ParentTouchMask)
			{
				this.ParentTouchMask.gameObject.GetOrAddComponent<Image>().color = this.MaskBackground;
				EventTrigger orAddComponent = this.ParentTouchMask.gameObject.GetOrAddComponent<EventTrigger>();
				orAddComponent.triggers.Clear();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener(delegate(BaseEventData data)
				{
					this.OnPointerDown((PointerEventData)data);
				});
				orAddComponent.triggers.Add(entry);
				EventTrigger.Entry entry2 = new EventTrigger.Entry();
				entry2.eventID = EventTriggerType.PointerUp;
				entry2.callback.AddListener(delegate(BaseEventData data)
				{
					this.OnPointerUp((PointerEventData)data);
				});
				orAddComponent.triggers.Add(entry2);
				EventTrigger.Entry entry3 = new EventTrigger.Entry();
				entry3.eventID = EventTriggerType.PointerEnter;
				entry3.callback.AddListener(delegate(BaseEventData data)
				{
					this.OnPointerEnter((PointerEventData)data);
				});
				orAddComponent.triggers.Add(entry3);
				EventTrigger.Entry entry4 = new EventTrigger.Entry();
				entry4.eventID = EventTriggerType.PointerExit;
				entry4.callback.AddListener(delegate(BaseEventData data)
				{
					this.OnPointerExit((PointerEventData)data);
				});
				orAddComponent.triggers.Add(entry4);
				EventTrigger.Entry entry5 = new EventTrigger.Entry();
				entry5.eventID = EventTriggerType.Drag;
				entry5.callback.AddListener(delegate(BaseEventData data)
				{
					this.OnDrag((PointerEventData)data);
				});
				orAddComponent.triggers.Add(entry5);
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003264A File Offset: 0x0003084A
		public override void OnPointerUp(PointerEventData eventData)
		{
			this._canDrag = false;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00032653 File Offset: 0x00030853
		public override void OnPointerEnter(PointerEventData eventData)
		{
			this._canDrag = true;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003265C File Offset: 0x0003085C
		public override void OnPointerExit(PointerEventData eventData)
		{
			this._canDrag = false;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00032668 File Offset: 0x00030868
		public override void OnPointerDown(PointerEventData eventData)
		{
			this._canDrag = true;
			base.OnPointerDown(eventData);
			this._initRotation = base.transform.rotation;
			if (this._screenSpaceOverlay)
			{
				this._currentVector = eventData.position - base.transform.position;
			}
			else
			{
				this._currentVector = eventData.position - Camera.main.WorldToScreenPoint(base.transform.position);
			}
			this._initAngle = Mathf.Atan2(this._currentVector.y, this._currentVector.x) * 57.29578f;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00032714 File Offset: 0x00030914
		public void OnDrag(PointerEventData eventData)
		{
			if (!this._canDrag)
			{
				return;
			}
			if (this._screenSpaceOverlay)
			{
				this._currentVector = eventData.position - base.transform.position;
			}
			else
			{
				this._currentVector = eventData.position - Camera.main.WorldToScreenPoint(base.transform.position);
			}
			this._currentAngle = Mathf.Atan2(this._currentVector.y, this._currentVector.x) * 57.29578f;
			Quaternion rhs = Quaternion.AngleAxis(this._currentAngle - this._initAngle, base.transform.forward);
			rhs.eulerAngles = new Vector3(0f, 0f, rhs.eulerAngles.z);
			Quaternion rotation = this._initRotation * rhs;
			if (this.direction == UI_Knob.Direction.CW)
			{
				this.KnobValue = 1f - rotation.eulerAngles.z / 360f;
				if (this.SnapToPosition)
				{
					this.SnapToPositionValue(ref this.KnobValue);
					rotation.eulerAngles = new Vector3(0f, 0f, 360f - 360f * this.KnobValue);
				}
			}
			else
			{
				this.KnobValue = rotation.eulerAngles.z / 360f;
				if (this.SnapToPosition)
				{
					this.SnapToPositionValue(ref this.KnobValue);
					rotation.eulerAngles = new Vector3(0f, 0f, 360f * this.KnobValue);
				}
			}
			this.UpdateKnobValue();
			base.transform.rotation = rotation;
			this.InvokeEvents(this.KnobValue + this._currentLoops);
			this._previousValue = this.KnobValue;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000328DC File Offset: 0x00030ADC
		private void UpdateKnobValue()
		{
			if (Mathf.Abs(this.KnobValue - this._previousValue) > 0.5f)
			{
				if (this.KnobValue < 0.5f && this.Loops > 1 && this._currentLoops < (float)(this.Loops - 1))
				{
					this._currentLoops += 1f;
				}
				else if (this.KnobValue > 0.5f && this._currentLoops >= 1f)
				{
					this._currentLoops -= 1f;
				}
				else
				{
					if (this.KnobValue > 0.5f && this._currentLoops == 0f)
					{
						this.KnobValue = 0f;
						base.transform.localEulerAngles = Vector3.zero;
						this.InvokeEvents(this.KnobValue + this._currentLoops);
						return;
					}
					if (this.KnobValue < 0.5f && this._currentLoops == (float)(this.Loops - 1))
					{
						this.KnobValue = 1f;
						base.transform.localEulerAngles = Vector3.zero;
						this.InvokeEvents(this.KnobValue + this._currentLoops);
						return;
					}
				}
			}
			if (this.MaxValue > 0f && this.KnobValue + this._currentLoops > this.MaxValue)
			{
				this.KnobValue = this.MaxValue;
				float z = (this.direction == UI_Knob.Direction.CW) ? (360f - 360f * this.MaxValue) : (360f * this.MaxValue);
				base.transform.localEulerAngles = new Vector3(0f, 0f, z);
				this.InvokeEvents(this.KnobValue);
				return;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00032A8C File Offset: 0x00030C8C
		public void SetKnobValue(float value, int loops = 0)
		{
			Quaternion identity = Quaternion.identity;
			this.KnobValue = value;
			this._currentLoops = (float)loops;
			if (this.SnapToPosition)
			{
				this.SnapToPositionValue(ref this.KnobValue);
			}
			if (this.direction == UI_Knob.Direction.CW)
			{
				identity.eulerAngles = new Vector3(0f, 0f, 360f - 360f * this.KnobValue);
			}
			else
			{
				identity.eulerAngles = new Vector3(0f, 0f, 360f * this.KnobValue);
			}
			this.UpdateKnobValue();
			base.transform.rotation = identity;
			this.InvokeEvents(this.KnobValue + this._currentLoops);
			this._previousValue = this.KnobValue;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00032B48 File Offset: 0x00030D48
		private void SnapToPositionValue(ref float knobValue)
		{
			float num = 1f / (float)this.SnapStepsPerLoop;
			float num2 = Mathf.Round(knobValue / num) * num;
			knobValue = num2;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00032B72 File Offset: 0x00030D72
		private void InvokeEvents(float value)
		{
			if (this.ClampOutput01)
			{
				value /= (float)this.Loops;
			}
			this.OnValueChanged.Invoke(value);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00032B93 File Offset: 0x00030D93
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x04000AF0 RID: 2800
		[Tooltip("Direction of rotation CW - clockwise, CCW - counterClockwise")]
		public UI_Knob.Direction direction;

		// Token: 0x04000AF1 RID: 2801
		[HideInInspector]
		public float KnobValue;

		// Token: 0x04000AF2 RID: 2802
		[Tooltip("Max value of the knob, maximum RAW output value knob can reach, overrides snap step, IF set to 0 or higher than loops, max value will be set by loops")]
		public float MaxValue;

		// Token: 0x04000AF3 RID: 2803
		[Tooltip("How many rotations knob can do, if higher than max value, the latter will limit max value")]
		public int Loops;

		// Token: 0x04000AF4 RID: 2804
		[Tooltip("Clamp output value between 0 and 1, useful with loops > 1")]
		public bool ClampOutput01;

		// Token: 0x04000AF5 RID: 2805
		[Tooltip("snap to position?")]
		public bool SnapToPosition;

		// Token: 0x04000AF6 RID: 2806
		[Tooltip("Number of positions to snap")]
		public int SnapStepsPerLoop = 10;

		// Token: 0x04000AF7 RID: 2807
		[Tooltip("Parent touch area to extend the touch radius")]
		public RectTransform ParentTouchMask;

		// Token: 0x04000AF8 RID: 2808
		[Tooltip("Default background color of the touch mask. Defaults as transparent")]
		public Color MaskBackground = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04000AF9 RID: 2809
		[Space(30f)]
		public KnobFloatValueEvent OnValueChanged;

		// Token: 0x04000AFA RID: 2810
		private float _currentLoops;

		// Token: 0x04000AFB RID: 2811
		private float _previousValue;

		// Token: 0x04000AFC RID: 2812
		private float _initAngle;

		// Token: 0x04000AFD RID: 2813
		private float _currentAngle;

		// Token: 0x04000AFE RID: 2814
		private Vector2 _currentVector;

		// Token: 0x04000AFF RID: 2815
		private Quaternion _initRotation;

		// Token: 0x04000B00 RID: 2816
		private bool _canDrag;

		// Token: 0x04000B01 RID: 2817
		private bool _screenSpaceOverlay;

		// Token: 0x02001140 RID: 4416
		public enum Direction
		{
			// Token: 0x040091D6 RID: 37334
			CW,
			// Token: 0x040091D7 RID: 37335
			CCW
		}
	}
}
