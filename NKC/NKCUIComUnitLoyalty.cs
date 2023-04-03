using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200094E RID: 2382
	public class NKCUIComUnitLoyalty : MonoBehaviour
	{
		// Token: 0x06005EF4 RID: 24308 RVA: 0x001D7A74 File Offset: 0x001D5C74
		public void SetLoyalty(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				NKCUtil.SetLabelText(this.m_lbLoyalty, "0");
				NKCUtil.SetGameobjectActive(this.m_objLoyaltyMax, false);
				NKCUtil.SetGameobjectActive(this.m_objLoyaltyLifetime, false);
				NKCUtil.SetLabelText(this.m_lbLoyaltyTitle, NKCUtilString.GET_STRING_LOYALTY);
				NKCUtil.SetLabelText(this.m_lbLoyaltyMax, "/{0}", new object[]
				{
					100
				});
				return;
			}
			NKCUtil.SetLabelText(this.m_lbLoyalty, (unitData.loyalty / 100).ToString());
			NKCUtil.SetGameobjectActive(this.m_objLoyaltyMax, unitData.loyalty >= 10000);
			NKCUtil.SetGameobjectActive(this.m_objLoyaltyLifetime, unitData.IsPermanentContract);
			if (unitData.IsPermanentContract)
			{
				NKCUtil.SetLabelText(this.m_lbLoyaltyTitle, NKCUtilString.GET_STRING_LOYALTY_LIFETIME);
				NKCUtil.SetLabelText(this.m_lbLoyaltyMax, "/{0}", new object[]
				{
					100
				});
				return;
			}
			NKCUtil.SetLabelText(this.m_lbLoyaltyTitle, NKCUtilString.GET_STRING_LOYALTY);
			NKCUtil.SetLabelText(this.m_lbLoyaltyMax, "/{0}", new object[]
			{
				100
			});
		}

		// Token: 0x04004B15 RID: 19221
		public Text m_lbLoyalty;

		// Token: 0x04004B16 RID: 19222
		public Text m_lbLoyaltyMax;

		// Token: 0x04004B17 RID: 19223
		public Text m_lbLoyaltyTitle;

		// Token: 0x04004B18 RID: 19224
		public GameObject m_objLoyaltyMax;

		// Token: 0x04004B19 RID: 19225
		public GameObject m_objLoyaltyLifetime;
	}
}
