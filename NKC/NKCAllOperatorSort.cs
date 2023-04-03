using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006B6 RID: 1718
	public class NKCAllOperatorSort : NKCOperatorSortSystem
	{
		// Token: 0x06003918 RID: 14616 RVA: 0x00127FE8 File Offset: 0x001261E8
		public NKCAllOperatorSort(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x00127FF2 File Offset: 0x001261F2
		protected override IEnumerable<NKMOperator> GetTargetOperatorList(NKMUserData userData)
		{
			if (this.m_lstCollection == null)
			{
				this.m_lstCollection = this.BuildUnitCollection(userData);
			}
			return this.m_lstCollection;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x00128010 File Offset: 0x00126210
		private List<NKMOperator> BuildUnitCollection(NKMUserData userData)
		{
			List<NKMOperator> list = new List<NKMOperator>();
			foreach (NKMUnitTempletBase nkmunitTempletBase in NKMTempletContainer<NKMUnitTempletBase>.Values)
			{
				if (nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					list.Add(NKCOperatorUtil.GetDummyOperator(nkmunitTempletBase, false));
				}
			}
			return list;
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x00128074 File Offset: 0x00126274
		protected override void BuildUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
		}

		// Token: 0x04003500 RID: 13568
		public List<NKMOperator> m_lstCollection;
	}
}
