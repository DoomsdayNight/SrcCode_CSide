using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004BC RID: 1212
	public class NKMGameStatRateTemplet : INKMTemplet
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x000ADE1A File Offset: 0x000AC01A
		public int Key
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000ADE22 File Offset: 0x000AC022
		public static NKMGameStatRateTemplet Find(int key)
		{
			return NKMTempletContainer<NKMGameStatRateTemplet>.Find(key);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000ADE2A File Offset: 0x000AC02A
		public static NKMGameStatRateTemplet Find(string strKey)
		{
			return NKMTempletContainer<NKMGameStatRateTemplet>.Find(strKey);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000ADE32 File Offset: 0x000AC032
		public void Join()
		{
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000ADE34 File Offset: 0x000AC034
		public void Validate()
		{
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000ADE38 File Offset: 0x000AC038
		public static NKMGameStatRateTemplet LoadFromLua(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 1431))
			{
				return null;
			}
			NKMGameStatRateTemplet nkmgameStatRateTemplet = new NKMGameStatRateTemplet();
			bool flag = true & cNKMLua.GetData("m_ID", ref nkmgameStatRateTemplet.m_ID) & cNKMLua.GetData("m_strID", ref nkmgameStatRateTemplet.m_strID) & cNKMLua.GetData("m_MainStatFactorRate", ref nkmgameStatRateTemplet.m_StatRate.m_MainStatFactorRate) & cNKMLua.GetData("m_SubStatRate", ref nkmgameStatRateTemplet.m_StatRate.m_SubStatValueRate);
			cNKMLua.GetData("m_EquipStatValueRate", ref nkmgameStatRateTemplet.m_StatRate.m_EquipStatRate);
			if (!(flag & cNKMLua.GetData("NST_HP", ref nkmgameStatRateTemplet.m_StatRate.m_HPRate) & cNKMLua.GetData("NST_ATK", ref nkmgameStatRateTemplet.m_StatRate.m_ATKRate) & cNKMLua.GetData("NST_DEF", ref nkmgameStatRateTemplet.m_StatRate.m_DEFRate) & cNKMLua.GetData("NST_CRITICAL", ref nkmgameStatRateTemplet.m_StatRate.m_CRITRate) & cNKMLua.GetData("NST_HIT", ref nkmgameStatRateTemplet.m_StatRate.m_HITRate) & cNKMLua.GetData("NST_EVADE", ref nkmgameStatRateTemplet.m_StatRate.m_EvadeRate)))
			{
				return null;
			}
			return nkmgameStatRateTemplet;
		}

		// Token: 0x040022F2 RID: 8946
		public int m_ID;

		// Token: 0x040022F3 RID: 8947
		public string m_strID;

		// Token: 0x040022F4 RID: 8948
		public NKMGameStatRate m_StatRate = new NKMGameStatRate();
	}
}
