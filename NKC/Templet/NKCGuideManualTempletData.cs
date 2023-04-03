using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x0200084B RID: 2123
	public class NKCGuideManualTempletData : INKMTemplet
	{
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x0019C3EC File Offset: 0x0019A5EC
		public int Key
		{
			get
			{
				return this.CATEGORY_ID;
			}
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0019C3F4 File Offset: 0x0019A5F4
		public static NKCGuideManualTempletData LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCGuideManualTemplet.cs", 114))
			{
				return null;
			}
			NKCGuideManualTempletData nkcguideManualTempletData = new NKCGuideManualTempletData();
			cNKMLua.GetData("m_ContentsVersionStart", ref nkcguideManualTempletData.m_ContentsVersionStart);
			cNKMLua.GetData("m_ContentsVersionEnd", ref nkcguideManualTempletData.m_ContentsVersionEnd);
			if (!(true & cNKMLua.GetData("CATEGORY_ID", ref nkcguideManualTempletData.CATEGORY_ID) & cNKMLua.GetData("CATEGORY_STRING", ref nkcguideManualTempletData.CATEGORY_STRING) & cNKMLua.GetData("CATEGORY_TITLE", ref nkcguideManualTempletData.CATEGORY_TITLE) & cNKMLua.GetData("ARTICLE_ID", ref nkcguideManualTempletData.ARTICLE_ID) & cNKMLua.GetData("ARTICLE_STRING_ID", ref nkcguideManualTempletData.ARTICLE_STRING_ID) & cNKMLua.GetData("GUIDE_ID_STRING", ref nkcguideManualTempletData.GUIDE_ID_STRING)))
			{
				return null;
			}
			return nkcguideManualTempletData;
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0019C4AE File Offset: 0x0019A6AE
		public void Join()
		{
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0019C4B0 File Offset: 0x0019A6B0
		public void Validate()
		{
		}

		// Token: 0x04004371 RID: 17265
		public int CATEGORY_ID;

		// Token: 0x04004372 RID: 17266
		public string CATEGORY_STRING;

		// Token: 0x04004373 RID: 17267
		public string CATEGORY_TITLE;

		// Token: 0x04004374 RID: 17268
		public string ARTICLE_ID;

		// Token: 0x04004375 RID: 17269
		public string ARTICLE_STRING_ID;

		// Token: 0x04004376 RID: 17270
		public string GUIDE_ID_STRING;

		// Token: 0x04004377 RID: 17271
		public string m_ContentsVersionStart = "";

		// Token: 0x04004378 RID: 17272
		public string m_ContentsVersionEnd = "";
	}
}
