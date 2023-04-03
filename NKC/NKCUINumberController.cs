using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200076C RID: 1900
	public class NKCUINumberController : MonoBehaviour
	{
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x0016B0DB File Offset: 0x001692DB
		// (set) Token: 0x06004BDA RID: 19418 RVA: 0x0016B0E3 File Offset: 0x001692E3
		public int Number
		{
			get
			{
				return this._Number;
			}
			private set
			{
				this.SetNumber(value, true);
			}
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x0016B0F0 File Offset: 0x001692F0
		public void SetNumber(int value, bool bInvokeCallback = false)
		{
			if (value > 9)
			{
				value = 0;
			}
			if (value < 0)
			{
				value = 9;
			}
			if (value == this._Number)
			{
				return;
			}
			this._Number = value;
			if (this.m_imgNumber != null && this.m_lstNumberSprite != null)
			{
				this.m_imgNumber.sprite = this.m_lstNumberSprite[this._Number];
			}
			if (this.m_lbNumber != null)
			{
				this.m_lbNumber.text = value.ToString(CultureInfo.InvariantCulture);
			}
			if (bInvokeCallback && this.dOnChangeNumber != null)
			{
				this.dOnChangeNumber(value);
			}
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0016B18C File Offset: 0x0016938C
		public void Init(List<Sprite> lstNumberSprite, NKCUINumberController.OnChangeNumber onChangeNumber)
		{
			this.m_cbtnUp.PointerClick.RemoveAllListeners();
			this.m_cbtnUp.PointerClick.AddListener(new UnityAction(this.OnBtnUp));
			this.m_cbtnDown.PointerClick.RemoveAllListeners();
			this.m_cbtnDown.PointerClick.AddListener(new UnityAction(this.OnBtnDown));
			this.m_lstNumberSprite = lstNumberSprite;
			this.dOnChangeNumber = onChangeNumber;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0016B200 File Offset: 0x00169400
		public void OnBtnUp()
		{
			int number = this.Number;
			this.Number = number + 1;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0016B220 File Offset: 0x00169420
		public void OnBtnDown()
		{
			int number = this.Number;
			this.Number = number - 1;
		}

		// Token: 0x04003A52 RID: 14930
		public NKCUIComButton m_cbtnUp;

		// Token: 0x04003A53 RID: 14931
		public NKCUIComButton m_cbtnDown;

		// Token: 0x04003A54 RID: 14932
		public Image m_imgNumber;

		// Token: 0x04003A55 RID: 14933
		public Text m_lbNumber;

		// Token: 0x04003A56 RID: 14934
		private List<Sprite> m_lstNumberSprite;

		// Token: 0x04003A57 RID: 14935
		private NKCUINumberController.OnChangeNumber dOnChangeNumber;

		// Token: 0x04003A58 RID: 14936
		private int _Number;

		// Token: 0x0200143A RID: 5178
		// (Invoke) Token: 0x0600A82D RID: 43053
		public delegate void OnChangeNumber(int changedNumber);
	}
}
