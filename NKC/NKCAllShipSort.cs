using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006EB RID: 1771
	public class NKCAllShipSort : NKCUnitSortSystem
	{
		// Token: 0x06003E49 RID: 15945 RVA: 0x00142CDE File Offset: 0x00140EDE
		public NKCAllShipSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x00142CE8 File Offset: 0x00140EE8
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			if (this.m_lstCollection == null)
			{
				this.m_lstCollection = this.BuildUnitCollection(userData);
			}
			return this.m_lstCollection;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x00142D08 File Offset: 0x00140F08
		private List<NKMUnitData> BuildUnitCollection(NKMUserData userData)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (NKMUnitTempletBase nkmunitTempletBase in NKMTempletContainer<NKMUnitTempletBase>.Values)
			{
				if (nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					list.Add(NKCUnitSortSystem.MakeTempUnitData(nkmunitTempletBase.m_UnitID, 1, 0));
				}
			}
			return list;
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x00142D70 File Offset: 0x00140F70
		protected override void BuildUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
		}

		// Token: 0x04003719 RID: 14105
		public List<NKMUnitData> m_lstCollection;
	}
}
