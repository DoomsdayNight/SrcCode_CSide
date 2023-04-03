using System;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003B6 RID: 950
	public class NKMCompanyBuffManager
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x000664E0 File Offset: 0x000646E0
		public static NKMCompanyBuffTemplet GetCompanyBuffTemplet(int companyBuffId)
		{
			return NKMTempletContainer<NKMCompanyBuffTemplet>.Find(companyBuffId);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000664E8 File Offset: 0x000646E8
		public static DateTime GetExpireTime(int companyBuffId, DateTime current)
		{
			NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(companyBuffId);
			if (companyBuffTemplet == null)
			{
				return DateTime.MinValue;
			}
			return current.AddMinutes((double)companyBuffTemplet.m_CompanyBuffTime);
		}
	}
}
