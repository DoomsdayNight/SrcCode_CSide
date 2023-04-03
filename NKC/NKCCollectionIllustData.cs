using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x0200064E RID: 1614
	public class NKCCollectionIllustData
	{
		// Token: 0x06003293 RID: 12947 RVA: 0x000FBC84 File Offset: 0x000F9E84
		public NKCCollectionIllustData(string BGGroupTitle, string BGGroupText, string BGThumbnailFileName, string BGFileName, string BGAniName, STAGE_UNLOCK_REQ_TYPE UnlockReqType, int UnlockReqValue)
		{
			this.m_BGGroupTitle = BGGroupTitle;
			this.m_BGGroupText = BGGroupText;
			this.m_UnlockReqType = UnlockReqType;
			this.m_UnlockReqValue = UnlockReqValue;
			this.m_FileData.Add(new NKCIllustFileData(BGThumbnailFileName, BGFileName, BGAniName));
			this.m_bActive = false;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000FBCDC File Offset: 0x000F9EDC
		public void SetClearState(bool active)
		{
			this.m_bActive = active;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000FBCE5 File Offset: 0x000F9EE5
		public bool IsClearMission()
		{
			return this.m_bActive;
		}

		// Token: 0x0400316C RID: 12652
		private bool m_bActive;

		// Token: 0x0400316D RID: 12653
		public readonly string m_BGGroupTitle;

		// Token: 0x0400316E RID: 12654
		public readonly string m_BGGroupText;

		// Token: 0x0400316F RID: 12655
		public STAGE_UNLOCK_REQ_TYPE m_UnlockReqType;

		// Token: 0x04003170 RID: 12656
		public readonly int m_UnlockReqValue;

		// Token: 0x04003171 RID: 12657
		public List<NKCIllustFileData> m_FileData = new List<NKCIllustFileData>();
	}
}
