using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x0200035E RID: 862
	[RequireComponent(typeof(BoxSlider), typeof(RawImage))]
	[ExecuteInEditMode]
	public class SVBoxSlider : MonoBehaviour
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0004D799 File Offset: 0x0004B999
		public RectTransform RectTransform
		{
			get
			{
				return base.transform as RectTransform;
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0004D7A6 File Offset: 0x0004B9A6
		private void Awake()
		{
			this.slider = base.GetComponent<BoxSlider>();
			this.image = base.GetComponent<RawImage>();
			this.RegenerateSVTexture();
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0004D7C8 File Offset: 0x0004B9C8
		private void OnEnable()
		{
			if (Application.isPlaying && this.picker != null)
			{
				this.slider.OnValueChanged.AddListener(new UnityAction<float, float>(this.SliderChanged));
				this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0004D824 File Offset: 0x0004BA24
		private void OnDisable()
		{
			if (this.picker != null)
			{
				this.slider.OnValueChanged.RemoveListener(new UnityAction<float, float>(this.SliderChanged));
				this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0004D877 File Offset: 0x0004BA77
		private void OnDestroy()
		{
			if (this.image.texture != null)
			{
				Object.DestroyImmediate(this.image.texture);
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004D89C File Offset: 0x0004BA9C
		private void SliderChanged(float saturation, float value)
		{
			if (this.listen)
			{
				this.picker.AssignColor(ColorValues.Saturation, saturation);
				this.picker.AssignColor(ColorValues.Value, value);
			}
			this.listen = true;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004D8C8 File Offset: 0x0004BAC8
		private void HSVChanged(float h, float s, float v)
		{
			if (this.lastH != h)
			{
				this.lastH = h;
				this.RegenerateSVTexture();
			}
			if (s != this.slider.NormalizedValueX)
			{
				this.listen = false;
				this.slider.NormalizedValueX = s;
			}
			if (v != this.slider.NormalizedValueY)
			{
				this.listen = false;
				this.slider.NormalizedValueY = v;
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0004D930 File Offset: 0x0004BB30
		private void RegenerateSVTexture()
		{
			double h = (double)((this.picker != null) ? (this.picker.H * 360f) : 0f);
			if (this.image.texture != null)
			{
				Object.DestroyImmediate(this.image.texture);
			}
			Texture2D texture2D = new Texture2D(100, 100)
			{
				hideFlags = HideFlags.DontSave
			};
			for (int i = 0; i < 100; i++)
			{
				Color32[] array = new Color32[100];
				for (int j = 0; j < 100; j++)
				{
					array[j] = HSVUtil.ConvertHsvToRgb(h, (double)((float)i / 100f), (double)((float)j / 100f), 1f);
				}
				texture2D.SetPixels32(i, 0, 1, 100, array);
			}
			texture2D.Apply();
			this.image.texture = texture2D;
		}

		// Token: 0x04000E4B RID: 3659
		public ColorPickerControl picker;

		// Token: 0x04000E4C RID: 3660
		private BoxSlider slider;

		// Token: 0x04000E4D RID: 3661
		private RawImage image;

		// Token: 0x04000E4E RID: 3662
		private float lastH = -1f;

		// Token: 0x04000E4F RID: 3663
		private bool listen = true;
	}
}
