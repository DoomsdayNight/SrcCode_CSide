using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002BE RID: 702
	[AddComponentMenu("UI/Extensions/Radial Slider")]
	[RequireComponent(typeof(Image))]
	public class RadialSlider : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0002CE2F File Offset: 0x0002B02F
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0002CE42 File Offset: 0x0002B042
		public float Angle
		{
			get
			{
				return this.RadialImage.fillAmount * 360f;
			}
			set
			{
				if (this.LerpToTarget)
				{
					this.StartLerp(value / 360f);
					return;
				}
				this.UpdateRadialImage(value / 360f);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0002CE67 File Offset: 0x0002B067
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0002CE74 File Offset: 0x0002B074
		public float Value
		{
			get
			{
				return this.RadialImage.fillAmount;
			}
			set
			{
				if (this.LerpToTarget)
				{
					this.StartLerp(value);
					return;
				}
				this.UpdateRadialImage(value);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0002CE8D File Offset: 0x0002B08D
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0002CE95 File Offset: 0x0002B095
		public Color EndColor
		{
			get
			{
				return this.m_endColor;
			}
			set
			{
				this.m_endColor = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0002CE9E File Offset: 0x0002B09E
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x0002CEA6 File Offset: 0x0002B0A6
		public Color StartColor
		{
			get
			{
				return this.m_startColor;
			}
			set
			{
				this.m_startColor = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0002CEAF File Offset: 0x0002B0AF
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x0002CEB7 File Offset: 0x0002B0B7
		public bool LerpToTarget
		{
			get
			{
				return this.m_lerpToTarget;
			}
			set
			{
				this.m_lerpToTarget = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0002CEC0 File Offset: 0x0002B0C0
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
		public AnimationCurve LerpCurve
		{
			get
			{
				return this.m_lerpCurve;
			}
			set
			{
				this.m_lerpCurve = value;
				this.m_lerpTime = this.LerpCurve[this.LerpCurve.length - 1].time;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0002CF02 File Offset: 0x0002B102
		public bool LerpInProgress
		{
			get
			{
				return this.lerpInProgress;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0002CF0C File Offset: 0x0002B10C
		public Image RadialImage
		{
			get
			{
				if (this.m_image == null)
				{
					this.m_image = base.GetComponent<Image>();
					this.m_image.type = Image.Type.Filled;
					this.m_image.fillMethod = Image.FillMethod.Radial360;
					this.m_image.fillAmount = 0f;
				}
				return this.m_image;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002CF61 File Offset: 0x0002B161
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x0002CF69 File Offset: 0x0002B169
		public RadialSlider.RadialSliderValueChangedEvent onValueChanged
		{
			get
			{
				return this._onValueChanged;
			}
			set
			{
				this._onValueChanged = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0002CF72 File Offset: 0x0002B172
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0002CF7A File Offset: 0x0002B17A
		public RadialSlider.RadialSliderTextValueChangedEvent onTextValueChanged
		{
			get
			{
				return this._onTextValueChanged;
			}
			set
			{
				this._onTextValueChanged = value;
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002CF84 File Offset: 0x0002B184
		private void Awake()
		{
			if (this.LerpCurve != null && this.LerpCurve.length > 0)
			{
				this.m_lerpTime = this.LerpCurve[this.LerpCurve.length - 1].time;
				return;
			}
			this.m_lerpTime = 1f;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		private void Update()
		{
			if (this.isPointerDown)
			{
				this.m_targetAngle = this.GetAngleFromMousePoint();
				if (!this.lerpInProgress)
				{
					if (!this.LerpToTarget)
					{
						this.UpdateRadialImage(this.m_targetAngle);
						this.NotifyValueChanged();
					}
					else
					{
						if (this.isPointerReleased)
						{
							this.StartLerp(this.m_targetAngle);
						}
						this.isPointerReleased = false;
					}
				}
			}
			if (this.lerpInProgress && this.Value != this.m_lerpTargetAngle)
			{
				this.m_currentLerpTime += Time.deltaTime;
				float num = this.m_currentLerpTime / this.m_lerpTime;
				if (this.LerpCurve != null && this.LerpCurve.length > 0)
				{
					this.UpdateRadialImage(Mathf.Lerp(this.m_startAngle, this.m_lerpTargetAngle, this.LerpCurve.Evaluate(num)));
				}
				else
				{
					this.UpdateRadialImage(Mathf.Lerp(this.m_startAngle, this.m_lerpTargetAngle, num));
				}
			}
			if (this.m_currentLerpTime >= this.m_lerpTime || this.Value == this.m_lerpTargetAngle)
			{
				this.lerpInProgress = false;
				this.UpdateRadialImage(this.m_lerpTargetAngle);
				this.NotifyValueChanged();
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002D0FD File Offset: 0x0002B2FD
		private void StartLerp(float targetAngle)
		{
			if (!this.lerpInProgress)
			{
				this.m_startAngle = this.Value;
				this.m_lerpTargetAngle = targetAngle;
				this.m_currentLerpTime = 0f;
				this.lerpInProgress = true;
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002D12C File Offset: 0x0002B32C
		private float GetAngleFromMousePoint()
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, this.m_screenPos, this.m_eventCamera, out this.m_localPos);
			return (Mathf.Atan2(-this.m_localPos.y, this.m_localPos.x) * 180f / 3.1415927f + 180f) / 360f;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002D190 File Offset: 0x0002B390
		private void UpdateRadialImage(float targetAngle)
		{
			this.RadialImage.fillAmount = targetAngle;
			this.RadialImage.color = Color.Lerp(this.m_startColor, this.m_endColor, targetAngle);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002D1BC File Offset: 0x0002B3BC
		private void NotifyValueChanged()
		{
			this._onValueChanged.Invoke((int)(this.m_targetAngle * 360f));
			this._onTextValueChanged.Invoke(((int)(this.m_targetAngle * 360f)).ToString());
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002D201 File Offset: 0x0002B401
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.m_screenPos = eventData.position;
			this.m_eventCamera = eventData.enterEventCamera;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002D21B File Offset: 0x0002B41B
		public void OnPointerDown(PointerEventData eventData)
		{
			this.m_screenPos = eventData.position;
			this.m_eventCamera = eventData.enterEventCamera;
			this.isPointerDown = true;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002D23C File Offset: 0x0002B43C
		public void OnPointerUp(PointerEventData eventData)
		{
			this.m_screenPos = Vector2.zero;
			this.isPointerDown = false;
			this.isPointerReleased = true;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002D257 File Offset: 0x0002B457
		public void OnDrag(PointerEventData eventData)
		{
			this.m_screenPos = eventData.position;
		}

		// Token: 0x04000A3F RID: 2623
		private bool isPointerDown;

		// Token: 0x04000A40 RID: 2624
		private bool isPointerReleased;

		// Token: 0x04000A41 RID: 2625
		private bool lerpInProgress;

		// Token: 0x04000A42 RID: 2626
		private Vector2 m_localPos;

		// Token: 0x04000A43 RID: 2627
		private Vector2 m_screenPos;

		// Token: 0x04000A44 RID: 2628
		private float m_targetAngle;

		// Token: 0x04000A45 RID: 2629
		private float m_lerpTargetAngle;

		// Token: 0x04000A46 RID: 2630
		private float m_startAngle;

		// Token: 0x04000A47 RID: 2631
		private float m_currentLerpTime;

		// Token: 0x04000A48 RID: 2632
		private float m_lerpTime;

		// Token: 0x04000A49 RID: 2633
		private Camera m_eventCamera;

		// Token: 0x04000A4A RID: 2634
		private Image m_image;

		// Token: 0x04000A4B RID: 2635
		[SerializeField]
		[Tooltip("Radial Gradient Start Color")]
		private Color m_startColor = Color.green;

		// Token: 0x04000A4C RID: 2636
		[SerializeField]
		[Tooltip("Radial Gradient End Color")]
		private Color m_endColor = Color.red;

		// Token: 0x04000A4D RID: 2637
		[Tooltip("Move slider absolute or use Lerping?\nDragging only supported with absolute")]
		[SerializeField]
		private bool m_lerpToTarget;

		// Token: 0x04000A4E RID: 2638
		[Tooltip("Curve to apply to the Lerp\nMust be set to enable Lerp")]
		[SerializeField]
		private AnimationCurve m_lerpCurve;

		// Token: 0x04000A4F RID: 2639
		[Tooltip("Event fired when value of control changes, outputs an INT angle value")]
		[SerializeField]
		private RadialSlider.RadialSliderValueChangedEvent _onValueChanged = new RadialSlider.RadialSliderValueChangedEvent();

		// Token: 0x04000A50 RID: 2640
		[Tooltip("Event fired when value of control changes, outputs a TEXT angle value")]
		[SerializeField]
		private RadialSlider.RadialSliderTextValueChangedEvent _onTextValueChanged = new RadialSlider.RadialSliderTextValueChangedEvent();

		// Token: 0x0200112F RID: 4399
		[Serializable]
		public class RadialSliderValueChangedEvent : UnityEvent<int>
		{
		}

		// Token: 0x02001130 RID: 4400
		[Serializable]
		public class RadialSliderTextValueChangedEvent : UnityEvent<string>
		{
		}
	}
}
