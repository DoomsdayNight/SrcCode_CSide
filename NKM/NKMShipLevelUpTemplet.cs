using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000466 RID: 1126
	public sealed class NKMShipLevelUpTemplet : INKMTemplet
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00090AAA File Offset: 0x0008ECAA
		public NKM_UNIT_GRADE ShipUnitGrade
		{
			get
			{
				return this.m_ShipRareGrade;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00090AB2 File Offset: 0x0008ECB2
		public int ShipStarGrade
		{
			get
			{
				return this.m_ShipStarGrade;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00090ABA File Offset: 0x0008ECBA
		public int ShipLimitBreakGrade
		{
			get
			{
				return this.m_ShipLimitBreakGrade;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00090AC2 File Offset: 0x0008ECC2
		public int ShipMaxLevel
		{
			get
			{
				return this.m_ShipMaxLevel;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00090ACA File Offset: 0x0008ECCA
		public int ShipUpgradeCredit
		{
			get
			{
				return this.m_Credit;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00090AD2 File Offset: 0x0008ECD2
		public int ShipUpgradeEternium
		{
			get
			{
				return this.m_Eternium;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00090ADA File Offset: 0x0008ECDA
		public List<LevelupMaterial> ShipLevelupMaterialList
		{
			get
			{
				return this.m_LevelupMaterialList;
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00090AE4 File Offset: 0x0008ECE4
		public static NKMShipLevelUpTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMShipLevelUpTemplet nkmshipLevelUpTemplet = new NKMShipLevelUpTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("m_ShipStarGrade", ref nkmshipLevelUpTemplet.m_ShipStarGrade);
			cNKMLua.GetData("m_ShipLimitBreakGrade", ref nkmshipLevelUpTemplet.m_ShipLimitBreakGrade);
			flag &= cNKMLua.GetData<NKM_UNIT_GRADE>("m_ShipRareGrade", ref nkmshipLevelUpTemplet.m_ShipRareGrade);
			flag &= cNKMLua.GetData("m_ShipMaxLevel", ref nkmshipLevelUpTemplet.m_ShipMaxLevel);
			flag &= cNKMLua.GetData("m_Credit", ref nkmshipLevelUpTemplet.m_Credit);
			flag &= cNKMLua.GetData("m_Eternium", ref nkmshipLevelUpTemplet.m_Eternium);
			for (int i = 0; i < 3; i++)
			{
				int num = 0;
				int num2 = 0;
				string str = (i + 1).ToString("D");
				cNKMLua.GetData("m_LevelupMaterialItemID" + str, ref num);
				cNKMLua.GetData("m_LevelupMaterialCount" + str, ref num2);
				if (num != 0 && num2 > 0)
				{
					LevelupMaterial item;
					item.m_LevelupMaterialItemID = num;
					item.m_LevelupMaterialCount = num2;
					nkmshipLevelUpTemplet.m_LevelupMaterialList.Add(item);
				}
			}
			if (!flag)
			{
				return null;
			}
			return nkmshipLevelUpTemplet;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00090BEC File Offset: 0x0008EDEC
		public static bool LoadFromLua(string fileName, string tableName)
		{
			IEnumerable<NKMShipLevelUpTemplet> enumerable = NKMTempletLoader.LoadCommonPath<NKMShipLevelUpTemplet>("AB_SCRIPT", fileName, tableName, new Func<NKMLua, NKMShipLevelUpTemplet>(NKMShipLevelUpTemplet.LoadFromLUA));
			if (enumerable != null)
			{
				NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet = (from e in enumerable
				group e by e.m_ShipRareGrade).ToDictionary((IGrouping<NKM_UNIT_GRADE, NKMShipLevelUpTemplet> e) => e.Key, (IGrouping<NKM_UNIT_GRADE, NKMShipLevelUpTemplet> e) => e.ToList<NKMShipLevelUpTemplet>());
			}
			return NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet != null;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00090C8A File Offset: 0x0008EE8A
		public void Join()
		{
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00090C8C File Offset: 0x0008EE8C
		public void Validate()
		{
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00090C8E File Offset: 0x0008EE8E
		public int Key
		{
			get
			{
				return this.ShipStarGrade;
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00090C98 File Offset: 0x0008EE98
		public static NKMShipLevelUpTemplet Find(int starGrade, NKM_UNIT_GRADE key = NKM_UNIT_GRADE.NUG_N, int ShipLimitBreakGrade = 0)
		{
			if (NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet.ContainsKey(key))
			{
				foreach (NKMShipLevelUpTemplet nkmshipLevelUpTemplet in NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet[key])
				{
					if (nkmshipLevelUpTemplet.Key == starGrade && nkmshipLevelUpTemplet.m_ShipLimitBreakGrade == ShipLimitBreakGrade)
					{
						return nkmshipLevelUpTemplet;
					}
				}
			}
			return null;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00090D10 File Offset: 0x0008EF10
		public static int GetMaxLevel(int starGrade, NKM_UNIT_GRADE grade = NKM_UNIT_GRADE.NUG_N, int ShipLimitBreakGrade = 0)
		{
			if (NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet.ContainsKey(grade))
			{
				foreach (NKMShipLevelUpTemplet nkmshipLevelUpTemplet in NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet[grade])
				{
					if (nkmshipLevelUpTemplet.m_ShipStarGrade == starGrade && nkmshipLevelUpTemplet.m_ShipLimitBreakGrade == ShipLimitBreakGrade)
					{
						return nkmshipLevelUpTemplet.m_ShipMaxLevel;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00090D8C File Offset: 0x0008EF8C
		public static int GetLevelUpCredit(int starGrade, NKM_UNIT_GRADE grade = NKM_UNIT_GRADE.NUG_N, int ShipLimitBreakGrade = 0)
		{
			if (NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet.ContainsKey(grade))
			{
				foreach (NKMShipLevelUpTemplet nkmshipLevelUpTemplet in NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet[grade])
				{
					if (nkmshipLevelUpTemplet.m_ShipStarGrade == starGrade && nkmshipLevelUpTemplet.m_ShipLimitBreakGrade == ShipLimitBreakGrade)
					{
						return nkmshipLevelUpTemplet.m_Credit;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00090E08 File Offset: 0x0008F008
		public static NKMShipLevelUpTemplet GetShipLevelupTempletByLevel(int targetLevel, NKM_UNIT_GRADE grade = NKM_UNIT_GRADE.NUG_N, int ShipLimitBreakGrade = 0)
		{
			NKMShipLevelUpTemplet result = new NKMShipLevelUpTemplet();
			if (NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet.ContainsKey(grade))
			{
				foreach (NKMShipLevelUpTemplet nkmshipLevelUpTemplet in NKMShipLevelUpTemplet.m_dicShipLevelUpTemplet[grade])
				{
					if (nkmshipLevelUpTemplet.m_ShipMaxLevel > targetLevel && nkmshipLevelUpTemplet.m_ShipLimitBreakGrade == ShipLimitBreakGrade)
					{
						return nkmshipLevelUpTemplet;
					}
					result = nkmshipLevelUpTemplet;
				}
				return result;
			}
			return result;
		}

		// Token: 0x04001F14 RID: 7956
		private int m_ShipStarGrade;

		// Token: 0x04001F15 RID: 7957
		private int m_ShipLimitBreakGrade;

		// Token: 0x04001F16 RID: 7958
		private NKM_UNIT_GRADE m_ShipRareGrade;

		// Token: 0x04001F17 RID: 7959
		private int m_ShipMaxLevel;

		// Token: 0x04001F18 RID: 7960
		private int m_Credit;

		// Token: 0x04001F19 RID: 7961
		private int m_Eternium;

		// Token: 0x04001F1A RID: 7962
		private List<LevelupMaterial> m_LevelupMaterialList = new List<LevelupMaterial>(3);

		// Token: 0x04001F1B RID: 7963
		private static Dictionary<NKM_UNIT_GRADE, List<NKMShipLevelUpTemplet>> m_dicShipLevelUpTemplet;
	}
}
