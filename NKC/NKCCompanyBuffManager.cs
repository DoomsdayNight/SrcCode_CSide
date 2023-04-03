using System;

namespace NKC
{
	// Token: 0x02000656 RID: 1622
	public class NKCCompanyBuffManager
	{
		// Token: 0x060032D2 RID: 13010 RVA: 0x000FCF01 File Offset: 0x000FB101
		public static void Update(float deltaTime)
		{
			if (NKCCompanyBuffManager.s_fRefreshBuffTimer <= 1f)
			{
				NKCCompanyBuffManager.s_fRefreshBuffTimer += deltaTime;
				return;
			}
			NKCCompanyBuffManager.s_fRefreshBuffTimer = 0f;
			NKCCompanyBuffManager.RefreshBuffList();
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000FCF2B File Offset: 0x000FB12B
		private static void RefreshBuffList()
		{
			if (NKCScenManager.CurrentUserData() != null)
			{
				NKCCompanyBuff.RemoveExpiredBuffs(NKCScenManager.CurrentUserData().m_companyBuffDataList);
			}
		}

		// Token: 0x04003198 RID: 12696
		private const float BUFF_REFRESH_INTERVAL = 1f;

		// Token: 0x04003199 RID: 12697
		private static float s_fRefreshBuffTimer;
	}
}
