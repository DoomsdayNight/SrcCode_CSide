using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000359 RID: 857
	[RequireComponent(typeof(RawImage))]
	[ExecuteInEditMode]
	public class ColorSliderImage : MonoBehaviour
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0004CAC5 File Offset: 0x0004ACC5
		private RectTransform RectTransform
		{
			get
			{
				return base.transform as RectTransform;
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0004CAD2 File Offset: 0x0004ACD2
		private void Awake()
		{
			this.image = base.GetComponent<RawImage>();
			if (this.image)
			{
				this.RegenerateTexture();
				return;
			}
			Debug.LogWarning("Missing RawImage on object [" + base.name + "]");
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0004CB10 File Offset: 0x0004AD10
		private void OnEnable()
		{
			if (this.picker != null && Application.isPlaying)
			{
				this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
				this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.ColorChanged));
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0004CB6C File Offset: 0x0004AD6C
		private void OnDisable()
		{
			if (this.picker != null)
			{
				this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
				this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.ColorChanged));
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0004CBBF File Offset: 0x0004ADBF
		private void OnDestroy()
		{
			if (this.image.texture != null)
			{
				Object.DestroyImmediate(this.image.texture);
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0004CBE4 File Offset: 0x0004ADE4
		private void ColorChanged(Color newColor)
		{
			switch (this.type)
			{
			case ColorValues.R:
			case ColorValues.G:
			case ColorValues.B:
			case ColorValues.Saturation:
			case ColorValues.Value:
				this.RegenerateTexture();
				break;
			case ColorValues.A:
			case ColorValues.Hue:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0004CC24 File Offset: 0x0004AE24
		private void ColorChanged(float hue, float saturation, float value)
		{
			switch (this.type)
			{
			case ColorValues.R:
			case ColorValues.G:
			case ColorValues.B:
			case ColorValues.Saturation:
			case ColorValues.Value:
				this.RegenerateTexture();
				break;
			case ColorValues.A:
			case ColorValues.Hue:
				break;
			default:
				return;
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0004CC64 File Offset: 0x0004AE64
		private void RegenerateTexture()
		{
			if (!this.picker)
			{
				Debug.LogWarning("Missing Picker on object [" + base.name + "]");
			}
			Color32 color = (this.picker != null) ? this.picker.CurrentColor : Color.black;
			float num = (this.picker != null) ? this.picker.H : 0f;
			float num2 = (this.picker != null) ? this.picker.S : 0f;
			float num3 = (this.picker != null) ? this.picker.V : 0f;
			bool flag = this.direction == Slider.Direction.BottomToTop || this.direction == Slider.Direction.TopToBottom;
			bool flag2 = this.direction == Slider.Direction.TopToBottom || this.direction == Slider.Direction.RightToLeft;
			int num4;
			switch (this.type)
			{
			case ColorValues.R:
			case ColorValues.G:
			case ColorValues.B:
			case ColorValues.A:
				num4 = 255;
				break;
			case ColorValues.Hue:
				num4 = 360;
				break;
			case ColorValues.Saturation:
			case ColorValues.Value:
				num4 = 100;
				break;
			default:
				throw new NotImplementedException("");
			}
			Texture2D texture2D;
			if (flag)
			{
				texture2D = new Texture2D(1, num4);
			}
			else
			{
				texture2D = new Texture2D(num4, 1);
			}
			texture2D.hideFlags = HideFlags.DontSave;
			Color32[] array = new Color32[num4];
			switch (this.type)
			{
			case ColorValues.R:
			{
				byte b = 0;
				while ((int)b < num4)
				{
					array[flag2 ? (num4 - 1 - (int)b) : ((int)b)] = new Color32(b, color.g, color.b, byte.MaxValue);
					b += 1;
				}
				break;
			}
			case ColorValues.G:
			{
				byte b2 = 0;
				while ((int)b2 < num4)
				{
					array[flag2 ? (num4 - 1 - (int)b2) : ((int)b2)] = new Color32(color.r, b2, color.b, byte.MaxValue);
					b2 += 1;
				}
				break;
			}
			case ColorValues.B:
			{
				byte b3 = 0;
				while ((int)b3 < num4)
				{
					array[flag2 ? (num4 - 1 - (int)b3) : ((int)b3)] = new Color32(color.r, color.g, b3, byte.MaxValue);
					b3 += 1;
				}
				break;
			}
			case ColorValues.A:
			{
				byte b4 = 0;
				while ((int)b4 < num4)
				{
					array[flag2 ? (num4 - 1 - (int)b4) : ((int)b4)] = new Color32(b4, b4, b4, byte.MaxValue);
					b4 += 1;
				}
				break;
			}
			case ColorValues.Hue:
				for (int i = 0; i < num4; i++)
				{
					array[flag2 ? (num4 - 1 - i) : i] = HSVUtil.ConvertHsvToRgb((double)i, 1.0, 1.0, 1f);
				}
				break;
			case ColorValues.Saturation:
				for (int j = 0; j < num4; j++)
				{
					array[flag2 ? (num4 - 1 - j) : j] = HSVUtil.ConvertHsvToRgb((double)(num * 360f), (double)((float)j / (float)num4), (double)num3, 1f);
				}
				break;
			case ColorValues.Value:
				for (int k = 0; k < num4; k++)
				{
					array[flag2 ? (num4 - 1 - k) : k] = HSVUtil.ConvertHsvToRgb((double)(num * 360f), (double)num2, (double)((float)k / (float)num4), 1f);
				}
				break;
			default:
				throw new NotImplementedException("");
			}
			texture2D.SetPixels32(array);
			texture2D.Apply();
			if (this.image.texture != null)
			{
				Object.DestroyImmediate(this.image.texture);
			}
			this.image.texture = texture2D;
			Slider.Direction direction = this.direction;
			if (direction > Slider.Direction.RightToLeft)
			{
				if (direction - Slider.Direction.BottomToTop <= 1)
				{
					this.image.uvRect = new Rect(0f, 0f, 2f, 1f);
					return;
				}
			}
			else
			{
				this.image.uvRect = new Rect(0f, 0f, 1f, 2f);
			}
		}

		// Token: 0x04000E38 RID: 3640
		public ColorPickerControl picker;

		// Token: 0x04000E39 RID: 3641
		public ColorValues type;

		// Token: 0x04000E3A RID: 3642
		public Slider.Direction direction;

		// Token: 0x04000E3B RID: 3643
		private RawImage image;
	}
}
