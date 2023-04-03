using System;
using NKM;

namespace NKC
{
	// Token: 0x0200066A RID: 1642
	public class NKCDescTemplet
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x001044E8 File Offset: 0x001026E8
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_UnitID", ref this.m_UnitID);
			cNKMLua.GetData("m_SkinID", ref this.m_SkinID);
			this.SetData(cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE.NDTT_RESULT_WIN, "m_ResultWinDesc", "m_ResultWinAni");
			this.SetData(cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE.NDTT_RESULT_LOSE, "m_ResultLoseDesc", "m_ResultLoseAni");
			this.SetData(cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE.NDTT_GET_UNIT, "m_GetUnitDesc", "m_GetUnitAni");
			this.SetData(cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE.NDTT_SUPER, "m_SuperDesc", "m_SuperAni");
			this.SetData(cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE.NDTT_RESULT_WIN_LIFE, "m_ResultWinLifeDesc", "m_ResultWinLifeAni");
			this.SetData(cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE.NDTT_RESULT_LOSE_LIFE, "m_ResultLoseLifeDesc", "m_ResultLoseLifeAni");
			return true;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x00104588 File Offset: 0x00102788
		private void SetData(NKMLua cNKMLua, NKCDescTemplet.NKC_DESC_TEMPLET_TYPE type, string descPropName, string aniPropName)
		{
			string desc = "";
			string ani = "";
			cNKMLua.GetData(descPropName, ref desc);
			cNKMLua.GetData(aniPropName, ref ani);
			this.m_arrDescData[(int)type] = new NKCDescTemplet.NKCDescData(desc, ani);
		}

		// Token: 0x0400324C RID: 12876
		public int m_UnitID;

		// Token: 0x0400324D RID: 12877
		public int m_SkinID;

		// Token: 0x0400324E RID: 12878
		public NKCDescTemplet.NKCDescData[] m_arrDescData = new NKCDescTemplet.NKCDescData[6];

		// Token: 0x0200130F RID: 4879
		public enum NKC_DESC_TEMPLET_TYPE
		{
			// Token: 0x040097E2 RID: 38882
			NDTT_RESULT_WIN,
			// Token: 0x040097E3 RID: 38883
			NDTT_RESULT_LOSE,
			// Token: 0x040097E4 RID: 38884
			NDTT_GET_UNIT,
			// Token: 0x040097E5 RID: 38885
			NDTT_SUPER,
			// Token: 0x040097E6 RID: 38886
			NDTT_RESULT_WIN_LIFE,
			// Token: 0x040097E7 RID: 38887
			NDTT_RESULT_LOSE_LIFE,
			// Token: 0x040097E8 RID: 38888
			NDTT_COUNT
		}

		// Token: 0x02001310 RID: 4880
		public class NKCDescData
		{
			// Token: 0x0600A50C RID: 42252 RVA: 0x00344DD2 File Offset: 0x00342FD2
			public NKCDescData(string desc, string ani)
			{
				this.m_Desc = desc;
				this.m_Ani = ani;
			}

			// Token: 0x0600A50D RID: 42253 RVA: 0x00344DE8 File Offset: 0x00342FE8
			public string GetDesc()
			{
				return NKCStringTable.GetString(this.m_Desc, false);
			}

			// Token: 0x040097E9 RID: 38889
			private string m_Desc;

			// Token: 0x040097EA RID: 38890
			public string m_Ani;
		}
	}
}
