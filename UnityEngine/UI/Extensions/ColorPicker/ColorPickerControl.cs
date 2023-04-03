using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000354 RID: 852
	[ExecuteInEditMode]
	public class ColorPickerControl : MonoBehaviour
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x0004BEC8 File Offset: 0x0004A0C8
		public void SetHSVSlidersOn(bool value)
		{
			this.hsvSlidersOn = value;
			foreach (GameObject gameObject in this.hsvSliders)
			{
				gameObject.SetActive(value);
			}
			if (this.alphaSlider)
			{
				this.alphaSlider.SetActive(this.hsvSlidersOn || this.rgbSlidersOn);
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004BF4C File Offset: 0x0004A14C
		public void SetRGBSlidersOn(bool value)
		{
			this.rgbSlidersOn = value;
			foreach (GameObject gameObject in this.rgbSliders)
			{
				gameObject.SetActive(value);
			}
			if (this.alphaSlider)
			{
				this.alphaSlider.SetActive(this.hsvSlidersOn || this.rgbSlidersOn);
			}
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004BFD0 File Offset: 0x0004A1D0
		private void Update()
		{
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0004BFD2 File Offset: 0x0004A1D2
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x0004BFF4 File Offset: 0x0004A1F4
		public Color CurrentColor
		{
			get
			{
				return new Color(this._red, this._green, this._blue, this._alpha);
			}
			set
			{
				if (this.CurrentColor == value)
				{
					return;
				}
				this._red = value.r;
				this._green = value.g;
				this._blue = value.b;
				this._alpha = value.a;
				this.RGBChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004C04C File Offset: 0x0004A24C
		private void Start()
		{
			this.SendChangedEvent();
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0004C054 File Offset: 0x0004A254
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x0004C05C File Offset: 0x0004A25C
		public float H
		{
			get
			{
				return this._hue;
			}
			set
			{
				if (this._hue == value)
				{
					return;
				}
				this._hue = value;
				this.HSVChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004C07B File Offset: 0x0004A27B
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0004C083 File Offset: 0x0004A283
		public float S
		{
			get
			{
				return this._saturation;
			}
			set
			{
				if (this._saturation == value)
				{
					return;
				}
				this._saturation = value;
				this.HSVChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0004C0A2 File Offset: 0x0004A2A2
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0004C0AA File Offset: 0x0004A2AA
		public float V
		{
			get
			{
				return this._brightness;
			}
			set
			{
				if (this._brightness == value)
				{
					return;
				}
				this._brightness = value;
				this.HSVChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0004C0C9 File Offset: 0x0004A2C9
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0004C0D1 File Offset: 0x0004A2D1
		public float R
		{
			get
			{
				return this._red;
			}
			set
			{
				if (this._red == value)
				{
					return;
				}
				this._red = value;
				this.RGBChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0004C0F8 File Offset: 0x0004A2F8
		public float G
		{
			get
			{
				return this._green;
			}
			set
			{
				if (this._green == value)
				{
					return;
				}
				this._green = value;
				this.RGBChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0004C117 File Offset: 0x0004A317
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x0004C11F File Offset: 0x0004A31F
		public float B
		{
			get
			{
				return this._blue;
			}
			set
			{
				if (this._blue == value)
				{
					return;
				}
				this._blue = value;
				this.RGBChanged();
				this.SendChangedEvent();
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0004C13E File Offset: 0x0004A33E
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x0004C146 File Offset: 0x0004A346
		private float A
		{
			get
			{
				return this._alpha;
			}
			set
			{
				if (this._alpha == value)
				{
					return;
				}
				this._alpha = value;
				this.SendChangedEvent();
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004C160 File Offset: 0x0004A360
		private void RGBChanged()
		{
			HsvColor hsvColor = HSVUtil.ConvertRgbToHsv(this.CurrentColor);
			this._hue = hsvColor.NormalizedH;
			this._saturation = hsvColor.NormalizedS;
			this._brightness = hsvColor.NormalizedV;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004C1A0 File Offset: 0x0004A3A0
		private void HSVChanged()
		{
			Color color = HSVUtil.ConvertHsvToRgb((double)(this._hue * 360f), (double)this._saturation, (double)this._brightness, this._alpha);
			this._red = color.r;
			this._green = color.g;
			this._blue = color.b;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004C1F8 File Offset: 0x0004A3F8
		private void SendChangedEvent()
		{
			this.onValueChanged.Invoke(this.CurrentColor);
			this.onHSVChanged.Invoke(this._hue, this._saturation, this._brightness);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004C228 File Offset: 0x0004A428
		public void AssignColor(ColorValues type, float value)
		{
			switch (type)
			{
			case ColorValues.R:
				this.R = value;
				return;
			case ColorValues.G:
				this.G = value;
				return;
			case ColorValues.B:
				this.B = value;
				return;
			case ColorValues.A:
				this.A = value;
				return;
			case ColorValues.Hue:
				this.H = value;
				return;
			case ColorValues.Saturation:
				this.S = value;
				return;
			case ColorValues.Value:
				this.V = value;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0004C290 File Offset: 0x0004A490
		public float GetValue(ColorValues type)
		{
			switch (type)
			{
			case ColorValues.R:
				return this.R;
			case ColorValues.G:
				return this.G;
			case ColorValues.B:
				return this.B;
			case ColorValues.A:
				return this.A;
			case ColorValues.Hue:
				return this.H;
			case ColorValues.Saturation:
				return this.S;
			case ColorValues.Value:
				return this.V;
			default:
				throw new NotImplementedException("");
			}
		}

		// Token: 0x04000E14 RID: 3604
		private float _hue;

		// Token: 0x04000E15 RID: 3605
		private float _saturation;

		// Token: 0x04000E16 RID: 3606
		private float _brightness;

		// Token: 0x04000E17 RID: 3607
		private float _red;

		// Token: 0x04000E18 RID: 3608
		private float _green;

		// Token: 0x04000E19 RID: 3609
		private float _blue;

		// Token: 0x04000E1A RID: 3610
		private float _alpha = 1f;

		// Token: 0x04000E1B RID: 3611
		public ColorChangedEvent onValueChanged = new ColorChangedEvent();

		// Token: 0x04000E1C RID: 3612
		public HSVChangedEvent onHSVChanged = new HSVChangedEvent();

		// Token: 0x04000E1D RID: 3613
		[SerializeField]
		private bool hsvSlidersOn = true;

		// Token: 0x04000E1E RID: 3614
		[SerializeField]
		private List<GameObject> hsvSliders = new List<GameObject>();

		// Token: 0x04000E1F RID: 3615
		[SerializeField]
		private bool rgbSlidersOn = true;

		// Token: 0x04000E20 RID: 3616
		[SerializeField]
		private List<GameObject> rgbSliders = new List<GameObject>();

		// Token: 0x04000E21 RID: 3617
		[SerializeField]
		private GameObject alphaSlider;
	}
}
