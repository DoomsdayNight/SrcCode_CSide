using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000358 RID: 856
	[RequireComponent(typeof(Slider))]
	public class ColorSlider : MonoBehaviour
	{
		// Token: 0x06001463 RID: 5219 RVA: 0x0004C8E0 File Offset: 0x0004AAE0
		private void Awake()
		{
			this.slider = base.GetComponent<Slider>();
			this.ColorPicker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
			this.ColorPicker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
			this.slider.onValueChanged.AddListener(new UnityAction<float>(this.SliderChanged));
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004C950 File Offset: 0x0004AB50
		private void OnDestroy()
		{
			this.ColorPicker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
			this.ColorPicker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
			this.slider.onValueChanged.RemoveListener(new UnityAction<float>(this.SliderChanged));
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004C9B4 File Offset: 0x0004ABB4
		private void ColorChanged(Color newColor)
		{
			this.listen = false;
			switch (this.type)
			{
			case ColorValues.R:
				this.slider.normalizedValue = newColor.r;
				return;
			case ColorValues.G:
				this.slider.normalizedValue = newColor.g;
				return;
			case ColorValues.B:
				this.slider.normalizedValue = newColor.b;
				return;
			case ColorValues.A:
				this.slider.normalizedValue = newColor.a;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004CA30 File Offset: 0x0004AC30
		private void HSVChanged(float hue, float saturation, float value)
		{
			this.listen = false;
			switch (this.type)
			{
			case ColorValues.Hue:
				this.slider.normalizedValue = hue;
				return;
			case ColorValues.Saturation:
				this.slider.normalizedValue = saturation;
				return;
			case ColorValues.Value:
				this.slider.normalizedValue = value;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004CA86 File Offset: 0x0004AC86
		private void SliderChanged(float newValue)
		{
			if (this.listen)
			{
				newValue = this.slider.normalizedValue;
				this.ColorPicker.AssignColor(this.type, newValue);
			}
			this.listen = true;
		}

		// Token: 0x04000E34 RID: 3636
		public ColorPickerControl ColorPicker;

		// Token: 0x04000E35 RID: 3637
		public ColorValues type;

		// Token: 0x04000E36 RID: 3638
		private Slider slider;

		// Token: 0x04000E37 RID: 3639
		private bool listen = true;
	}
}
