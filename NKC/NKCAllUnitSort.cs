using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006EA RID: 1770
	public class NKCAllUnitSort : NKCUnitSortSystem
	{
		// Token: 0x06003E45 RID: 15941 RVA: 0x00142C4C File Offset: 0x00140E4C
		public NKCAllUnitSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x00142C56 File Offset: 0x00140E56
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			if (this.m_lstCollection == null)
			{
				this.m_lstCollection = this.BuildUnitCollection(userData);
			}
			return this.m_lstCollection;
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00142C74 File Offset: 0x00140E74
		private List<NKMUnitData> BuildUnitCollection(NKMUserData userData)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (NKMUnitTempletBase nkmunitTempletBase in NKMTempletContainer<NKMUnitTempletBase>.Values)
			{
				if (nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					list.Add(NKCUnitSortSystem.MakeTempUnitData(nkmunitTempletBase.m_UnitID, 1, 0));
				}
			}
			return list;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00142CDC File Offset: 0x00140EDC
		protected override void BuildUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
		}

		// Token: 0x04003718 RID: 14104
		public List<NKMUnitData> m_lstCollection;
	}
}
