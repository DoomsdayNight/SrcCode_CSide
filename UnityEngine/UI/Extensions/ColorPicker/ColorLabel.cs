using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000353 RID: 851
	[RequireComponent(typeof(Text))]
	public class ColorLabel : MonoBehaviour
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x0004BD1B File Offset: 0x00049F1B
		private void Awake()
		{
			this.label = base.GetComponent<Text>();
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004BD2C File Offset: 0x00049F2C
		private void OnEnable()
		{
			if (Application.isPlaying && this.picker != null)
			{
				this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
				this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004BD88 File Offset: 0x00049F88
		private void OnDestroy()
		{
			if (this.picker != null)
			{
				this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
				this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004BDDB File Offset: 0x00049FDB
		private void ColorChanged(Color color)
		{
			this.UpdateValue();
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0004BDE3 File Offset: 0x00049FE3
		private void HSVChanged(float hue, float sateration, float value)
		{
			this.UpdateValue();
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004BDEC File Offset: 0x00049FEC
		private void UpdateValue()
		{
			if (this.picker == null)
			{
				this.label.text = this.prefix + "-";
				return;
			}
			float value = this.minValue + this.picker.GetValue(this.type) * (this.maxValue - this.minValue);
			this.label.text = this.prefix + this.ConvertToDisplayString(value);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004BE68 File Offset: 0x0004A068
		private string ConvertToDisplayString(float value)
		{
			if (this.precision > 0)
			{
				return value.ToString("f " + this.precision.ToString());
			}
			return Mathf.FloorToInt(value).ToString();
		}

		// Token: 0x04000E0D RID: 3597
		public ColorPickerControl picker;

		// Token: 0x04000E0E RID: 3598
		public ColorValues type;

		// Token: 0x04000E0F RID: 3599
		public string prefix = "R: ";

		// Token: 0x04000E10 RID: 3600
		public float minValue;

		// Token: 0x04000E11 RID: 3601
		public float maxValue = 255f;

		// Token: 0x04000E12 RID: 3602
		public int precision;

		// Token: 0x04000E13 RID: 3603
		private Text label;
	}
}
