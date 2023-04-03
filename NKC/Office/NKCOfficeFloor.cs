using System;

namespace NKC.Office
{
	// Token: 0x02000834 RID: 2100
	public class NKCOfficeFloor : NKCOfficeFloorBase
	{
		// Token: 0x0600536E RID: 21358 RVA: 0x00196ED9 File Offset: 0x001950D9
		public override bool GetFunitureInvert(NKCOfficeFunitureData funitureData)
		{
			return funitureData.bInvert;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00196EE1 File Offset: 0x001950E1
		protected override bool GetFunitureInvert()
		{
			return false;
		}
	}
}
