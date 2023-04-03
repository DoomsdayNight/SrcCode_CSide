using System;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006AD RID: 1709
	public class NKCNewsTemplet : INKMTemplet
	{
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x00124AB2 File Offset: 0x00122CB2
		public int Key
		{
			get
			{
				return this.Idx;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x00124ABA File Offset: 0x00122CBA
		public DateTime m_DateStartUtc
		{
			get
			{
				return NKMTime.LocalToUTC(this.m_DateStart, 0);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x00124AC8 File Offset: 0x00122CC8
		public DateTime m_DateEndUtc
		{
			get
			{
				return NKMTime.LocalToUTC(this.m_DateEnd, 0);
			}
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x00124AD8 File Offset: 0x00122CD8
		public static NKCNewsTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCNewsManager.cs", 39))
			{
				return null;
			}
			NKCNewsTemplet nkcnewsTemplet = new NKCNewsTemplet();
			bool flag = true & cNKMLua.GetData("Idx", ref nkcnewsTemplet.Idx) & cNKMLua.GetData("m_Order", ref nkcnewsTemplet.m_Order);
			cNKMLua.GetData("m_DateStart", ref nkcnewsTemplet.m_DateStart);
			cNKMLua.GetData("m_DateEnd", ref nkcnewsTemplet.m_DateEnd);
			bool flag2 = flag & cNKMLua.GetData("m_bDateAlert", ref nkcnewsTemplet.m_bDateAlert) & cNKMLua.GetData<eNewsFilterType>("m_FliterType", ref nkcnewsTemplet.m_FilterType) & cNKMLua.GetData("m_TabImage", ref nkcnewsTemplet.m_TabImage);
			cNKMLua.GetData("m_Title", ref nkcnewsTemplet.m_Title);
			cNKMLua.GetData("m_Contents", ref nkcnewsTemplet.m_Contents);
			cNKMLua.GetData("m_BannerImage", ref nkcnewsTemplet.m_BannerImage);
			cNKMLua.GetData<NKM_SHORTCUT_TYPE>("m_ShortCutType", ref nkcnewsTemplet.m_ShortCutType);
			cNKMLua.GetData("m_ShortCut", ref nkcnewsTemplet.m_ShortCut);
			if (!flag2)
			{
				return null;
			}
			return nkcnewsTemplet;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x00124BDA File Offset: 0x00122DDA
		public void Join()
		{
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x00124BDC File Offset: 0x00122DDC
		public void Validate()
		{
		}

		// Token: 0x040034CD RID: 13517
		public int Idx;

		// Token: 0x040034CE RID: 13518
		public int m_Order;

		// Token: 0x040034CF RID: 13519
		public DateTime m_DateStart;

		// Token: 0x040034D0 RID: 13520
		public DateTime m_DateEnd;

		// Token: 0x040034D1 RID: 13521
		public bool m_bDateAlert;

		// Token: 0x040034D2 RID: 13522
		public eNewsFilterType m_FilterType;

		// Token: 0x040034D3 RID: 13523
		public string m_TabImage;

		// Token: 0x040034D4 RID: 13524
		public string m_Title;

		// Token: 0x040034D5 RID: 13525
		public string m_Contents;

		// Token: 0x040034D6 RID: 13526
		public string m_BannerImage;

		// Token: 0x040034D7 RID: 13527
		public NKM_SHORTCUT_TYPE m_ShortCutType;

		// Token: 0x040034D8 RID: 13528
		public string m_ShortCut;
	}
}
