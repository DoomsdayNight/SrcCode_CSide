using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006E8 RID: 1768
	public class NKCUnitCollection : NKCUnitSortSystem
	{
		// Token: 0x06003E3F RID: 15935 RVA: 0x00142B13 File Offset: 0x00140D13
		public NKCUnitCollection(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00142B1D File Offset: 0x00140D1D
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			if (this.m_lstCollection == null)
			{
				this.m_lstCollection = this.BuildUnitCollection(userData);
			}
			return this.m_lstCollection;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00142B3C File Offset: 0x00140D3C
		private List<NKMUnitData> BuildUnitCollection(NKMUserData userData)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (int unitID in userData.m_ArmyData.m_illustrateUnit)
			{
				if (NKMUnitManager.GetUnitTempletBase(unitID).m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					list.Add(NKCUnitSortSystem.MakeTempUnitData(unitID, 1, 0));
				}
			}
			return list;
		}

		// Token: 0x04003716 RID: 14102
		public List<NKMUnitData> m_lstCollection;
	}
}
