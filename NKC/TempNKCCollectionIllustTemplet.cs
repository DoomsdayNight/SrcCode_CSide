using System;
using NKC.Templet.Base;
using NKM;

namespace NKC
{
	// Token: 0x0200064C RID: 1612
	public class TempNKCCollectionIllustTemplet : INKCTemplet
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000FBACB File Offset: 0x000F9CCB
		public int Key
		{
			get
			{
				return this.m_CategoryID;
			}
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000FBAD4 File Offset: 0x000F9CD4
		public static TempNKCCollectionIllustTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			TempNKCCollectionIllustTemplet tempNKCCollectionIllustTemplet = new TempNKCCollectionIllustTemplet();
			if (!(true & cNKMLua.GetData("m_CategoryID", ref tempNKCCollectionIllustTemplet.m_CategoryID) & cNKMLua.GetData("m_CategoryTitle", ref tempNKCCollectionIllustTemplet.m_CategoryTitle) & cNKMLua.GetData("m_CategorySubtitle", ref tempNKCCollectionIllustTemplet.m_CategorySubTitle) & cNKMLua.GetData("m_BGGroupID", ref tempNKCCollectionIllustTemplet.m_BGGroupID) & cNKMLua.GetData("m_BGGroupTitle", ref tempNKCCollectionIllustTemplet.m_BGGroupTitle) & cNKMLua.GetData("m_BGGroupText", ref tempNKCCollectionIllustTemplet.m_BGGroupText) & cNKMLua.GetData("m_BGThumbnailFileName", ref tempNKCCollectionIllustTemplet.m_BGThumbnailFileName) & cNKMLua.GetData("m_BGFileName", ref tempNKCCollectionIllustTemplet.m_BGFileName) & cNKMLua.GetData("m_GameObjectBGAniName", ref tempNKCCollectionIllustTemplet.m_GameObjectBGAniName) & cNKMLua.GetData<STAGE_UNLOCK_REQ_TYPE>("m_UnlockReqType", ref tempNKCCollectionIllustTemplet.m_UnlockReqType) & cNKMLua.GetData("m_UnlockReqValue", ref tempNKCCollectionIllustTemplet.m_UnlockReqValue)))
			{
				return null;
			}
			return tempNKCCollectionIllustTemplet;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000FBBB3 File Offset: 0x000F9DB3
		public string GetBGGroupTitle()
		{
			return NKCStringTable.GetString(this.m_BGGroupTitle, false);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000FBBC1 File Offset: 0x000F9DC1
		public string GetBGGroupText()
		{
			return NKCStringTable.GetString(this.m_BGGroupText, false);
		}

		// Token: 0x0400315D RID: 12637
		public int m_CategoryID;

		// Token: 0x0400315E RID: 12638
		public string m_CategoryTitle = "";

		// Token: 0x0400315F RID: 12639
		public string m_CategorySubTitle = "";

		// Token: 0x04003160 RID: 12640
		public int m_BGGroupID;

		// Token: 0x04003161 RID: 12641
		private string m_BGGroupTitle = "";

		// Token: 0x04003162 RID: 12642
		private string m_BGGroupText = "";

		// Token: 0x04003163 RID: 12643
		public string m_BGThumbnailFileName = "";

		// Token: 0x04003164 RID: 12644
		public string m_BGFileName = "";

		// Token: 0x04003165 RID: 12645
		public string m_GameObjectBGAniName = "";

		// Token: 0x04003166 RID: 12646
		public STAGE_UNLOCK_REQ_TYPE m_UnlockReqType;

		// Token: 0x04003167 RID: 12647
		public int m_UnlockReqValue;
	}
}
