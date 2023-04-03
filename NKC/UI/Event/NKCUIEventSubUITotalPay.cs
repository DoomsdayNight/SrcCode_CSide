using System;
using NKM.Event;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BE6 RID: 3046
	public class NKCUIEventSubUITotalPay : NKCUIEventSubUIBase
	{
		// Token: 0x06008D4E RID: 36174 RVA: 0x00301058 File Offset: 0x002FF258
		public override void Init()
		{
			base.Init();
			if (this.m_csbtnReturnGet != null)
			{
				this.m_csbtnReturnGet.PointerClick.RemoveAllListeners();
				this.m_csbtnReturnGet.PointerClick.AddListener(new UnityAction(this.OnClickReturnGet));
			}
		}

		// Token: 0x06008D4F RID: 36175 RVA: 0x003010A5 File Offset: 0x002FF2A5
		private void OnClickReturnGet()
		{
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, null, "");
		}

		// Token: 0x06008D50 RID: 36176 RVA: 0x003010BC File Offset: 0x002FF2BC
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_tabTemplet = tabTemplet;
			this.UpdateUI();
		}

		// Token: 0x06008D51 RID: 36177 RVA: 0x003010CB File Offset: 0x002FF2CB
		public override void Refresh()
		{
			this.UpdateUI();
		}

		// Token: 0x06008D52 RID: 36178 RVA: 0x003010D4 File Offset: 0x002FF2D4
		private void UpdateUI()
		{
			base.SetDateLimit();
			double totalPayment = NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.GetTotalPayment();
			double num;
			if (totalPayment <= 3000.0)
			{
				num = totalPayment * 10.0 * 1.5;
			}
			else
			{
				double num2 = totalPayment - 3000.0;
				num = 45000.0;
				num += num2 * 10.0;
			}
			NKCUtil.SetLabelText(this.m_lbTotalPay, NKCUtilString.GET_STRING_EVENT_TOTAL_PAY, new object[]
			{
				totalPayment
			});
			NKCUtil.SetLabelText(this.m_lbTotalPayReturn, NKCUtilString.GET_STRING_EVENT_TOTAL_PAY_RETURN, new object[]
			{
				num
			});
		}

		// Token: 0x04007A26 RID: 31270
		public Text m_lbTotalPay;

		// Token: 0x04007A27 RID: 31271
		public Text m_lbTotalPayReturn;

		// Token: 0x04007A28 RID: 31272
		public NKCUIComStateButton m_csbtnReturnGet;
	}
}
