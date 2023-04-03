using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x020006B3 RID: 1715
	public class NKCGenericOperatorSort : NKCOperatorSortSystem
	{
		// Token: 0x06003910 RID: 14608 RVA: 0x00127F10 File Offset: 0x00126110
		public NKCGenericOperatorSort(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options, IEnumerable<NKMOperator> lstOperatorData) : base(userData, options, lstOperatorData)
		{
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x00127F1B File Offset: 0x0012611B
		protected override IEnumerable<NKMOperator> GetTargetOperatorList(NKMUserData userData)
		{
			return null;
		}
	}
}
