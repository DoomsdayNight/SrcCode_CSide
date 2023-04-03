using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x0200035D RID: 861
	[RequireComponent(typeof(InputField))]
	public class HexColorField : MonoBehaviour
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x0004D3B4 File Offset: 0x0004B5B4
		private void Awake()
		{
			this.hexInputField = base.GetComponent<InputField>();
			this.hexInputField.onEndEdit.AddListener(new UnityAction<string>(this.UpdateColor));
			this.ColorPicker.onValueChanged.AddListener(new UnityAction<Color>(this.UpdateHex));
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0004D405 File Offset: 0x0004B605
		private void OnDestroy()
		{
			this.hexInputField.onValueChanged.RemoveListener(new UnityAction<string>(this.UpdateColor));
			this.ColorPicker.onValueChanged.RemoveListener(new UnityAction<Color>(this.UpdateHex));
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0004D43F File Offset: 0x0004B63F
		private void UpdateHex(Color newColor)
		{
			this.hexInputField.text = this.ColorToHex(newColor);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004D458 File Offset: 0x0004B658
		private void UpdateColor(string newHex)
		{
			Color32 c;
			if (HexColorField.HexToColor(newHex, out c))
			{
				this.ColorPicker.CurrentColor = c;
				return;
			}
			Debug.Log("hex value is in the wrong format, valid formats are: #RGB, #RGBA, #RRGGBB and #RRGGBBAA (# is optional)");
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004D48C File Offset: 0x0004B68C
		private string ColorToHex(Color32 color)
		{
			if (this.displayAlpha)
			{
				return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
				{
					color.r,
					color.g,
					color.b,
					color.a
				});
			}
			return string.Format("#{0:X2}{1:X2}{2:X2}", color.r, color.g, color.b);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0004D518 File Offset: 0x0004B718
		public static bool HexToColor(string hex, out Color32 color)
		{
			if (Regex.IsMatch(hex, "^#?(?:[0-9a-fA-F]{3,4}){1,2}$"))
			{
				int num = hex.StartsWith("#") ? 1 : 0;
				if (hex.Length == num + 8)
				{
					color = new Color32(byte.Parse(hex.Substring(num, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 2, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 4, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 6, 2), NumberStyles.AllowHexSpecifier));
				}
				else if (hex.Length == num + 6)
				{
					color = new Color32(byte.Parse(hex.Substring(num, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 2, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 4, 2), NumberStyles.AllowHexSpecifier), byte.MaxValue);
				}
				else if (hex.Length == num + 4)
				{
					color = new Color32(byte.Parse(hex[num].ToString() + hex[num].ToString(), NumberStyles.AllowHexSpecifier), byte.Parse(hex[num + 1].ToString() + hex[num + 1].ToString(), NumberStyles.AllowHexSpecifier), byte.Parse(hex[num + 2].ToString() + hex[num + 2].ToString(), NumberStyles.AllowHexSpecifier), byte.Parse(hex[num + 3].ToString() + hex[num + 3].ToString(), NumberStyles.AllowHexSpecifier));
				}
				else
				{
					color = new Color32(byte.Parse(hex[num].ToString() + hex[num].ToString(), NumberStyles.AllowHexSpecifier), byte.Parse(hex[num + 1].ToString() + hex[num + 1].ToString(), NumberStyles.AllowHexSpecifier), byte.Parse(hex[num + 2].ToString() + hex[num + 2].ToString(), NumberStyles.AllowHexSpecifier), byte.MaxValue);
				}
				return true;
			}
			color = default(Color32);
			return false;
		}

		// Token: 0x04000E47 RID: 3655
		public ColorPickerControl ColorPicker;

		// Token: 0x04000E48 RID: 3656
		public bool displayAlpha;

		// Token: 0x04000E49 RID: 3657
		private InputField hexInputField;

		// Token: 0x04000E4A RID: 3658
		private const string hexRegex = "^#?(?:[0-9a-fA-F]{3,4}){1,2}$";
	}
}
