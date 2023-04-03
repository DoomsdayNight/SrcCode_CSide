using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x020006E6 RID: 1766
	public class NKCUnitSort : NKCUnitSortSystem
	{
		// Token: 0x06003E39 RID: 15929 RVA: 0x00142AC5 File Offset: 0x00140CC5
		public NKCUnitSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x00142ACF File Offset: 0x00140CCF
		public NKCUnitSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options, bool useLocal) : base(userData, options, useLocal)
		{
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x00142ADA File Offset: 0x00140CDA
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			return userData.m_ArmyData.m_dicMyUnit.Values;
		}
	}
}
