using System;
using System.Collections.Generic;

namespace NKC
{
	// Token: 0x0200064F RID: 1615
	public class NKCollectionStoryTemplet
	{
		// Token: 0x06003296 RID: 12950 RVA: 0x000FBCED File Offset: 0x000F9EED
		public NKCollectionStoryTemplet(int EpID, string EpTitle, string EpName, CollectionStoryData newData)
		{
			this.m_EpisodeID = EpID;
			this.m_EpisodeTitle = EpTitle;
			this.m_EpisodeName = EpName;
			this.m_lstData = new List<CollectionStoryData>();
			this.m_lstData.Add(newData);
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000FBD22 File Offset: 0x000F9F22
		public string GetEpisodeTitle()
		{
			return NKCStringTable.GetString(this.m_EpisodeTitle, false);
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000FBD30 File Offset: 0x000F9F30
		public string GetEpisodeName()
		{
			return NKCStringTable.GetString(this.m_EpisodeName, false);
		}

		// Token: 0x04003172 RID: 12658
		public int m_EpisodeID;

		// Token: 0x04003173 RID: 12659
		private string m_EpisodeTitle;

		// Token: 0x04003174 RID: 12660
		private string m_EpisodeName;

		// Token: 0x04003175 RID: 12661
		public int m_ActID;

		// Token: 0x04003176 RID: 12662
		public List<CollectionStoryData> m_lstData;
	}
}
