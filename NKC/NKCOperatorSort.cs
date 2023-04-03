using System;
using System.Collections.Generic;
using System.Linq;
using NKM;

namespace NKC
{
	// Token: 0x020006B4 RID: 1716
	public class NKCOperatorSort : NKCOperatorSortSystem
	{
		// Token: 0x06003912 RID: 14610 RVA: 0x00127F1E File Offset: 0x0012611E
		public NKCOperatorSort(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x00127F28 File Offset: 0x00126128
		public NKCOperatorSort(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options, bool local) : base(userData, options, local)
		{
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x00127F33 File Offset: 0x00126133
		protected override IEnumerable<NKMOperator> GetTargetOperatorList(NKMUserData userData)
		{
			return userData.m_ArmyData.m_dicMyOperator.Values.ToList<NKMOperator>();
		}
	}
}
