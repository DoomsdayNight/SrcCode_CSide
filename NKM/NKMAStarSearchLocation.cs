using System;

namespace NKM
{
	// Token: 0x02000393 RID: 915
	public class NKMAStarSearchLocation
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x0005EEA1 File Offset: 0x0005D0A1
		public NKMAStarSearchLocation(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0005EEB7 File Offset: 0x0005D0B7
		public NKMAStarSearchLocation(float x, float y)
		{
			this.x = (int)x;
			this.y = (int)y;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0005EED0 File Offset: 0x0005D0D0
		public override bool Equals(object obj)
		{
			NKMAStarSearchLocation nkmastarSearchLocation = obj as NKMAStarSearchLocation;
			return this.x == nkmastarSearchLocation.x && this.y == nkmastarSearchLocation.y;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0005EF02 File Offset: 0x0005D102
		public override int GetHashCode()
		{
			return this.x * 597 ^ this.y * 1173;
		}

		// Token: 0x04000FEA RID: 4074
		public readonly int x;

		// Token: 0x04000FEB RID: 4075
		public readonly int y;
	}
}
