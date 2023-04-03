using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x02000650 RID: 1616
	public class CollectionStoryData
	{
		// Token: 0x06003299 RID: 12953 RVA: 0x000FBD3E File Offset: 0x000F9F3E
		public CollectionStoryData(STAGE_UNLOCK_REQ_TYPE type, int ReqVal)
		{
			this.m_UnlockReqType = type;
			this.m_UnlockReqValue = ReqVal;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000FBD5F File Offset: 0x000F9F5F
		public void SetClearState(bool active)
		{
			this.m_bActive = active;
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000FBD68 File Offset: 0x000F9F68
		public bool IsClearMission()
		{
			return this.m_bActive;
		}

		// Token: 0x04003177 RID: 12663
		private bool m_bActive;

		// Token: 0x04003178 RID: 12664
		public readonly string m_BGGroupTitle;

		// Token: 0x04003179 RID: 12665
		public readonly string m_BGGroupText;

		// Token: 0x0400317A RID: 12666
		public STAGE_UNLOCK_REQ_TYPE m_UnlockReqType;

		// Token: 0x0400317B RID: 12667
		public readonly int m_UnlockReqValue;

		// Token: 0x0400317C RID: 12668
		public List<NKCIllustFileData> m_FileData = new List<NKCIllustFileData>();

		// Token: 0x0400317D RID: 12669
		public int m_ActID;

		// Token: 0x0400317E RID: 12670
		public int m_MissionIndex;

		// Token: 0x0400317F RID: 12671
		public string m_StageName;
	}
}
