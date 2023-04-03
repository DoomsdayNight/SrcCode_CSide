using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B95 RID: 2965
	public class NKCUIGameOptionSliderWithButton : MonoBehaviour
	{
		// Token: 0x060088E1 RID: 35041 RVA: 0x002E4D84 File Offset: 0x002E2F84
		public void Init(int min, int max, int value, string[] textTemplet = null, NKCUIGameOptionSliderWithButton.OnChanged onChanged = null)
		{
			this.m_Min = min;
			this.m_Max = max;
			this.m_TextTemplet = textTemplet;
			this.dOnChanged = onChanged;
			this.m_Value = value;
			this.m_WarningValue = max + 1;
			this.m_lastValue = value;
			this.m_Slider.maxValue = (float)max;
			this.m_Slider.minValue = (float)min;
			this.m_Slider.value = (float)value;
			this.m_Slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSlider));
			NKCUIComStateButton plusButton = this.m_PlusButton;
			if (plusButton != null)
			{
				plusButton.PointerClick.AddListener(delegate()
				{
					this.OnClickButton(NKCUIGameOptionSliderWithButton.ButtonType.Plus);
				});
			}
			NKCUIComStateButton minusButton = this.m_MinusButton;
			if (minusButton != null)
			{
				minusButton.PointerClick.AddListener(delegate()
				{
					this.OnClickButton(NKCUIGameOptionSliderWithButton.ButtonType.Minus);
				});
			}
			NKCUIComStateButton changeButton = this.m_ChangeButton;
			if (changeButton != null)
			{
				changeButton.PointerClick.AddListener(delegate()
				{
					this.OnClickButton(NKCUIGameOptionSliderWithButton.ButtonType.Change);
				});
			}
			NKCUtil.SetLabelText(this.m_Text, value.ToString());
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x002E4E84 File Offset: 0x002E3084
		public bool isDisabled()
		{
			return this.m_bDisabled;
		}

		// Token: 0x060088E3 RID: 35043 RVA: 0x002E4E8C File Offset: 0x002E308C
		public int GetValue()
		{
			return this.m_Value;
		}

		// Token: 0x060088E4 RID: 35044 RVA: 0x002E4E94 File Offset: 0x002E3094
		public void ChangeValue(int value)
		{
			if (value < this.m_Min)
			{
				this.m_Value = this.m_Min;
			}
			else if (value > this.m_Max)
			{
				this.m_Value = this.m_Max;
			}
			else
			{
				this.m_Value = value;
			}
			this.m_lastValue = this.m_Value;
			this.m_Slider.value = (float)this.m_Value;
			this.UpdateButtonText();
			if (this.dOnChanged != null)
			{
				this.dOnChanged();
			}
		}

		// Token: 0x060088E5 RID: 35045 RVA: 0x002E4F0D File Offset: 0x002E310D
		private void ChangeValueWithWarning(int value)
		{
			if (value != this.m_lastValue && value >= this.m_WarningValue)
			{
				this.ShowWarning(value);
				return;
			}
			this.ChangeValue(value);
		}

		// Token: 0x060088E6 RID: 35046 RVA: 0x002E4F30 File Offset: 0x002E3130
		public void SetMax(int value)
		{
			this.m_Max = value;
			if (this.m_Value > value)
			{
				this.m_Value = value;
			}
			this.m_Slider.maxValue = (float)value;
			this.ChangeValue(this.m_Value);
		}

		// Token: 0x060088E7 RID: 35047 RVA: 0x002E4F64 File Offset: 0x002E3164
		public void SetDisabled(bool disabled, string text = "")
		{
			this.m_bDisabled = disabled;
			this.m_Slider.interactable = !disabled;
			if (disabled)
			{
				this.m_Text.text = text;
				if (this.m_SliderHandle != null)
				{
					this.m_SliderHandle.color = this.m_DisabledHandleColor;
					return;
				}
			}
			else if (this.m_SliderHandle != null)
			{
				this.m_SliderHandle.color = this.m_OriginalHandleColor;
			}
		}

		// Token: 0x060088E8 RID: 35048 RVA: 0x002E4FD5 File Offset: 0x002E31D5
		public void UpdateButtonText()
		{
			if (this.m_TextTemplet != null)
			{
				this.m_Text.text = this.m_TextTemplet[this.m_Value];
				return;
			}
			this.m_Text.text = this.m_Value.ToString();
		}

		// Token: 0x060088E9 RID: 35049 RVA: 0x002E500E File Offset: 0x002E320E
		private void OnValueChangedSlider(float value)
		{
			if (this.m_bDisabled)
			{
				return;
			}
			this.ChangeValueWithWarning((int)value);
		}

		// Token: 0x060088EA RID: 35050 RVA: 0x002E5024 File Offset: 0x002E3224
		private void OnClickButton(NKCUIGameOptionSliderWithButton.ButtonType buttonType)
		{
			if (this.m_bDisabled)
			{
				this.m_bDisabled = false;
				this.m_Slider.interactable = true;
				this.m_SliderHandle.color = this.m_OriginalHandleColor;
			}
			switch (buttonType)
			{
			case NKCUIGameOptionSliderWithButton.ButtonType.Plus:
			{
				int num = this.m_Value + 1;
				this.m_Value = num;
				if (num > this.m_Max)
				{
					this.m_Value = this.m_Max;
				}
				this.ChangeValueWithWarning(this.m_Value);
				return;
			}
			case NKCUIGameOptionSliderWithButton.ButtonType.Minus:
			{
				int num = this.m_Value - 1;
				this.m_Value = num;
				this.ChangeValueWithWarning(num);
				return;
			}
			case NKCUIGameOptionSliderWithButton.ButtonType.Change:
			{
				int num2 = this.m_Value;
				num2++;
				if (num2 > this.m_Max)
				{
					num2 = this.m_Min;
				}
				this.ChangeValueWithWarning(num2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060088EB RID: 35051 RVA: 0x002E50DC File Offset: 0x002E32DC
		public void SetWarningPopup(int warningValue, string warningTitle, string warningDesc)
		{
			this.m_WarningValue = warningValue;
			this.m_WarningTilte = warningTitle;
			this.m_WarningDesc = warningDesc;
		}

		// Token: 0x060088EC RID: 35052 RVA: 0x002E50F4 File Offset: 0x002E32F4
		private void ShowWarning(int value)
		{
			NKCPopupOKCancel.OpenOKCancelBox(this.m_WarningTilte, this.m_WarningDesc, delegate()
			{
				this.ChangeValue(value);
			}, delegate()
			{
				this.m_Value = this.m_lastValue;
				this.ChangeValue(this.m_Value);
			}, false);
		}

		// Token: 0x04007559 RID: 30041
		private int m_Min;

		// Token: 0x0400755A RID: 30042
		private int m_Max;

		// Token: 0x0400755B RID: 30043
		private string[] m_TextTemplet;

		// Token: 0x0400755C RID: 30044
		private int m_Value;

		// Token: 0x0400755D RID: 30045
		private bool m_bDisabled;

		// Token: 0x0400755E RID: 30046
		public Slider m_Slider;

		// Token: 0x0400755F RID: 30047
		public Image m_SliderHandle;

		// Token: 0x04007560 RID: 30048
		public Color m_OriginalHandleColor;

		// Token: 0x04007561 RID: 30049
		public Color m_DisabledHandleColor;

		// Token: 0x04007562 RID: 30050
		public NKCUIComStateButton m_PlusButton;

		// Token: 0x04007563 RID: 30051
		public NKCUIComStateButton m_MinusButton;

		// Token: 0x04007564 RID: 30052
		public NKCUIComStateButton m_ChangeButton;

		// Token: 0x04007565 RID: 30053
		public Text m_Text;

		// Token: 0x04007566 RID: 30054
		private NKCUIGameOptionSliderWithButton.OnChanged dOnChanged;

		// Token: 0x04007567 RID: 30055
		private int m_WarningValue;

		// Token: 0x04007568 RID: 30056
		private string m_WarningTilte;

		// Token: 0x04007569 RID: 30057
		private string m_WarningDesc;

		// Token: 0x0400756A RID: 30058
		private int m_lastValue;

		// Token: 0x0200193A RID: 6458
		private enum ButtonType
		{
			// Token: 0x0400AAFE RID: 43774
			Plus,
			// Token: 0x0400AAFF RID: 43775
			Minus,
			// Token: 0x0400AB00 RID: 43776
			Change
		}

		// Token: 0x0200193B RID: 6459
		// (Invoke) Token: 0x0600B801 RID: 47105
		public delegate void OnChanged();
	}
}
