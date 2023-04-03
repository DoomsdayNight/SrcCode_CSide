using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.Office
{
	// Token: 0x0200083E RID: 2110
	[RequireComponent(typeof(RectTransform), typeof(Image))]
	public class NKCOfficeWall : NKCOfficeFloorBase
	{
		// Token: 0x06005422 RID: 21538 RVA: 0x0019AD80 File Offset: 0x00198F80
		public override bool GetFunitureInvert(NKCOfficeFunitureData funitureData)
		{
			return this.bInvertRequired;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0019AD88 File Offset: 0x00198F88
		protected override bool GetFunitureInvert()
		{
			return this.bInvertRequired;
		}

		// Token: 0x0400432B RID: 17195
		public bool bInvertRequired;
	}
}
