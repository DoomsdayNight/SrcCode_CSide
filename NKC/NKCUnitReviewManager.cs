using System;
using System.Collections.Generic;

namespace NKC
{
	// Token: 0x020006E3 RID: 1763
	public class NKCUnitReviewManager
	{
		// Token: 0x06003D8C RID: 15756 RVA: 0x0013CEF4 File Offset: 0x0013B0F4
		public static void SetBanList(List<long> lstBanUser)
		{
			if (lstBanUser != null)
			{
				NKCUnitReviewManager.m_lstBannedUserUid = lstBanUser;
			}
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x0013CEFF File Offset: 0x0013B0FF
		public static void AddBanList(long userUid)
		{
			if (!NKCUnitReviewManager.m_lstBannedUserUid.Contains(userUid))
			{
				NKCUnitReviewManager.m_lstBannedUserUid.Add(userUid);
			}
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x0013CF19 File Offset: 0x0013B119
		public static void RemoveBanList(long userUid)
		{
			if (NKCUnitReviewManager.m_lstBannedUserUid.Contains(userUid))
			{
				NKCUnitReviewManager.m_lstBannedUserUid.Remove(userUid);
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0013CF34 File Offset: 0x0013B134
		public static bool IsBannedUser(long userUid)
		{
			return NKCUnitReviewManager.m_lstBannedUserUid.Contains(userUid);
		}

		// Token: 0x040036EB RID: 14059
		public static bool m_bReceivedUnitReviewBanList = false;

		// Token: 0x040036EC RID: 14060
		public static List<long> m_lstBannedUserUid = new List<long>();
	}
}
