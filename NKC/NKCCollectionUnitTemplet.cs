using System;
using NKC.Templet.Base;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x0200064A RID: 1610
	public class NKCCollectionUnitTemplet : INKCTemplet
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000FB94A File Offset: 0x000F9B4A
		public int Key
		{
			get
			{
				return this.m_UnitID;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000FB954 File Offset: 0x000F9B54
		public static NKCCollectionUnitTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 31))
			{
				return null;
			}
			NKCCollectionUnitTemplet nkccollectionUnitTemplet = new NKCCollectionUnitTemplet();
			cNKMLua.GetData("m_ContentsVersionStart", ref nkccollectionUnitTemplet.m_ContentsVersionStart);
			cNKMLua.GetData("m_ContentsVersionEnd", ref nkccollectionUnitTemplet.m_ContentsVersionEnd);
			bool flag = true & cNKMLua.GetData("Idx", ref nkccollectionUnitTemplet.Idx) & cNKMLua.GetData("m_UnitID", ref nkccollectionUnitTemplet.m_UnitID) & cNKMLua.GetData("m_UnitStrID", ref nkccollectionUnitTemplet.m_UnitStrID) & cNKMLua.GetData<NKM_UNIT_TYPE>("m_NKM_UNIT_TYPE", ref nkccollectionUnitTemplet.m_NKM_UNIT_TYPE) & cNKMLua.GetData("m_UnitIntro", ref nkccollectionUnitTemplet.m_UnitIntro);
			cNKMLua.GetData("m_CutsceneLifetime_Start", ref nkccollectionUnitTemplet.m_CutsceneLifetime_Start);
			cNKMLua.GetData("m_CutsceneLifetime_End", ref nkccollectionUnitTemplet.m_CutsceneLifetime_End);
			cNKMLua.GetData("m_CutsceneLifetime_BG", ref nkccollectionUnitTemplet.m_CutsceneLifetime_BG);
			cNKMLua.GetData("m_bExclude", ref nkccollectionUnitTemplet.m_bExclude);
			if (!flag)
			{
				return null;
			}
			return nkccollectionUnitTemplet;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000FBA44 File Offset: 0x000F9C44
		public string GetUnitIntro()
		{
			return NKCStringTable.GetString(this.m_UnitIntro, false);
		}

		// Token: 0x04003150 RID: 12624
		public int Idx;

		// Token: 0x04003151 RID: 12625
		public int m_UnitID;

		// Token: 0x04003152 RID: 12626
		public string m_UnitStrID;

		// Token: 0x04003153 RID: 12627
		private string m_UnitIntro;

		// Token: 0x04003154 RID: 12628
		public NKM_UNIT_TYPE m_NKM_UNIT_TYPE;

		// Token: 0x04003155 RID: 12629
		public string m_CutsceneLifetime_Start;

		// Token: 0x04003156 RID: 12630
		public string m_CutsceneLifetime_End;

		// Token: 0x04003157 RID: 12631
		public string m_CutsceneLifetime_BG;

		// Token: 0x04003158 RID: 12632
		public string m_ContentsVersionStart = "";

		// Token: 0x04003159 RID: 12633
		public string m_ContentsVersionEnd = "";

		// Token: 0x0400315A RID: 12634
		public bool m_bExclude;
	}
}
