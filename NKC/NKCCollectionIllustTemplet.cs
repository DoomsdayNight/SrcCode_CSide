using System;
using System.Collections.Generic;

namespace NKC
{
	// Token: 0x0200064D RID: 1613
	public class NKCCollectionIllustTemplet
	{
		// Token: 0x06003290 RID: 12944 RVA: 0x000FBC30 File Offset: 0x000F9E30
		public NKCCollectionIllustTemplet(int CategoryID, string CategoryTitle, string CategorySubTitle, int BGGroupID, NKCCollectionIllustData IllustData)
		{
			this.m_CategoryID = CategoryID;
			this.m_CategoryTitle = CategoryTitle;
			this.m_CategorySubTitle = CategorySubTitle;
			this.m_dicIllustData.Add(BGGroupID, IllustData);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000FBC67 File Offset: 0x000F9E67
		public string GetCategoryTitle()
		{
			return NKCStringTable.GetString(this.m_CategoryTitle, false);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000FBC75 File Offset: 0x000F9E75
		public string GetCategorySubTitle()
		{
			return NKCStringTable.GetString(this.m_CategorySubTitle, false);
		}

		// Token: 0x04003168 RID: 12648
		public int m_CategoryID;

		// Token: 0x04003169 RID: 12649
		private string m_CategoryTitle;

		// Token: 0x0400316A RID: 12650
		private string m_CategorySubTitle;

		// Token: 0x0400316B RID: 12651
		public Dictionary<int, NKCCollectionIllustData> m_dicIllustData = new Dictionary<int, NKCCollectionIllustData>();
	}
}
