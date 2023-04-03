using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006B5 RID: 1717
	public class NKCOperatorCollection : NKCOperatorSortSystem
	{
		// Token: 0x06003915 RID: 14613 RVA: 0x00127F4A File Offset: 0x0012614A
		public NKCOperatorCollection(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x00127F54 File Offset: 0x00126154
		protected override IEnumerable<NKMOperator> GetTargetOperatorList(NKMUserData userData)
		{
			if (this.m_lstCollection == null)
			{
				this.m_lstCollection = this.BuildUnitCollection(userData);
			}
			return this.m_lstCollection;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x00127F74 File Offset: 0x00126174
		private List<NKMOperator> BuildUnitCollection(NKMUserData userData)
		{
			List<NKMOperator> list = new List<NKMOperator>();
			foreach (int unitID in userData.m_ArmyData.m_illustrateUnit)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					list.Add(NKCOperatorUtil.GetDummyOperator(unitTempletBase, false));
				}
			}
			return list;
		}

		// Token: 0x040034FF RID: 13567
		public List<NKMOperator> m_lstCollection;
	}
}
