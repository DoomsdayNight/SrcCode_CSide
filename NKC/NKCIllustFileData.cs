using System;

namespace NKC
{
	// Token: 0x02000651 RID: 1617
	public struct NKCIllustFileData
	{
		// Token: 0x0600329C RID: 12956 RVA: 0x000FBD70 File Offset: 0x000F9F70
		public NKCIllustFileData(string BGThumbnailFileName, string BGFileName, string BGAniName)
		{
			this.m_BGThumbnailFileName = BGThumbnailFileName;
			this.m_BGFileName = BGFileName;
			this.m_GameObjectBGAniName = BGAniName;
		}

		// Token: 0x04003180 RID: 12672
		public readonly string m_BGThumbnailFileName;

		// Token: 0x04003181 RID: 12673
		public readonly string m_BGFileName;

		// Token: 0x04003182 RID: 12674
		public readonly string m_GameObjectBGAniName;
	}
}
