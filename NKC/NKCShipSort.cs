using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x020006E7 RID: 1767
	public class NKCShipSort : NKCUnitSortSystem
	{
		// Token: 0x06003E3C RID: 15932 RVA: 0x00142AEC File Offset: 0x00140CEC
		public NKCShipSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options) : base(userData, options)
		{
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x00142AF6 File Offset: 0x00140CF6
		public NKCShipSort(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options, bool useLocal) : base(userData, options, useLocal)
		{
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x00142B01 File Offset: 0x00140D01
		protected override IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData)
		{
			return userData.m_ArmyData.m_dicMyShip.Values;
		}
	}
}
