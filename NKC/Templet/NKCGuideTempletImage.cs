using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x0200084D RID: 2125
	public class NKCGuideTempletImage : INKMTemplet
	{
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x0019C611 File Offset: 0x0019A811
		public int Key
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0019C61C File Offset: 0x0019A81C
		public static NKCGuideTempletImage LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCGuideTemplet.cs", 78))
			{
				return null;
			}
			NKCGuideTempletImage nkcguideTempletImage = new NKCGuideTempletImage();
			bool flag = true & cNKMLua.GetData("ID", ref nkcguideTempletImage.ID) & cNKMLua.GetData("ID_STRING", ref nkcguideTempletImage.ID_STRING) & cNKMLua.GetData("GUIDE_BUNDLE_PATH", ref nkcguideTempletImage.GUIDE_BUNDLE_PATH) & cNKMLua.GetData("GUIDE_IMAGE_PATH", ref nkcguideTempletImage.GUIDE_IMAGE_PATH);
			cNKMLua.GetData("IsSprite", ref nkcguideTempletImage.IsSprite);
			if (!flag)
			{
				return null;
			}
			return nkcguideTempletImage;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0019C6A0 File Offset: 0x0019A8A0
		public void Join()
		{
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0019C6A2 File Offset: 0x0019A8A2
		public void Validate()
		{
		}

		// Token: 0x0400437D RID: 17277
		public int ID;

		// Token: 0x0400437E RID: 17278
		public string ID_STRING;

		// Token: 0x0400437F RID: 17279
		public string GUIDE_BUNDLE_PATH;

		// Token: 0x04004380 RID: 17280
		public string GUIDE_IMAGE_PATH;

		// Token: 0x04004381 RID: 17281
		public bool IsSprite = true;
	}
}
