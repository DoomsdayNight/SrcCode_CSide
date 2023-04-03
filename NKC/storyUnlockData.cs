using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000653 RID: 1619
	public struct storyUnlockData
	{
		// Token: 0x0600329E RID: 12958 RVA: 0x000FBDAC File Offset: 0x000F9FAC
		public storyUnlockData(List<UnlockInfo> unlockReqList, EPISODE_CATEGORY episodeCategory, int episodeID, int actID, int stageID)
		{
			this.m_UnlockReqList = unlockReqList;
			this.m_EpisodeCategory = episodeCategory;
			this.m_EpisodeID = episodeID;
			this.m_ActID = actID;
			this.m_StageID = stageID;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000FBDD3 File Offset: 0x000F9FD3
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x000FBDDB File Offset: 0x000F9FDB
		public EPISODE_CATEGORY m_EpisodeCategory { readonly get; private set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000FBDE4 File Offset: 0x000F9FE4
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000FBDEC File Offset: 0x000F9FEC
		public int m_EpisodeID { readonly get; private set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000FBDF5 File Offset: 0x000F9FF5
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x000FBDFD File Offset: 0x000F9FFD
		public int m_ActID { readonly get; private set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000FBE06 File Offset: 0x000FA006
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x000FBE0E File Offset: 0x000FA00E
		public int m_StageID { readonly get; private set; }

		// Token: 0x04003187 RID: 12679
		public List<UnlockInfo> m_UnlockReqList;
	}
}
