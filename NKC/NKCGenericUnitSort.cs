using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x020006E5 RID: 1765
	public class NKCGenericUnitSort : NKCUnitSortSystem
	{
		// Token: 0x06003E37 RID: 15927 RVA: 0x00142AB7 File Offset: 0x00140CB7
		public NKCGenericUnitSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options, IEnumerable<NKMUnitData> lstUnitData) : base(userData, options, lstUnitData)
		{
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00142AC2 File Offset: 0x00140CC2
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			return null;
		}
	}
}
