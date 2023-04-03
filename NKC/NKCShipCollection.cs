using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006E9 RID: 1769
	public class NKCShipCollection : NKCUnitSortSystem
	{
		// Token: 0x06003E42 RID: 15938 RVA: 0x00142BB0 File Offset: 0x00140DB0
		public NKCShipCollection(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00142BBA File Offset: 0x00140DBA
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			if (this.m_lstCollection == null)
			{
				this.m_lstCollection = this.BuildUnitCollection(userData);
			}
			return this.m_lstCollection;
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x00142BD8 File Offset: 0x00140DD8
		private List<NKMUnitData> BuildUnitCollection(NKMUserData userData)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (int unitID in userData.m_ArmyData.m_illustrateUnit)
			{
				if (NKMUnitManager.GetUnitTempletBase(unitID).m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					list.Add(NKCUnitSortSystem.MakeTempUnitData(unitID, 1, 0));
				}
			}
			return list;
		}

		// Token: 0x04003717 RID: 14103
		public List<NKMUnitData> m_lstCollection;
	}
}
