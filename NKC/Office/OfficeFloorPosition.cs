using System;

namespace NKC.Office
{
	// Token: 0x02000835 RID: 2101
	public struct OfficeFloorPosition : IEquatable<OfficeFloorPosition>
	{
		// Token: 0x06005371 RID: 21361 RVA: 0x00196EEC File Offset: 0x001950EC
		public OfficeFloorPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00196EFC File Offset: 0x001950FC
		public OfficeFloorPosition(ValueTuple<int, int> pos)
		{
			this.x = pos.Item1;
			this.y = pos.Item2;
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x00196F16 File Offset: 0x00195116
		public ValueTuple<int, int> ToPair
		{
			get
			{
				return new ValueTuple<int, int>(this.x, this.y);
			}
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x00196F29 File Offset: 0x00195129
		public static OfficeFloorPosition operator +(OfficeFloorPosition a)
		{
			return a;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00196F2C File Offset: 0x0019512C
		public static OfficeFloorPosition operator -(OfficeFloorPosition a)
		{
			return new OfficeFloorPosition(-a.x, -a.y);
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x00196F41 File Offset: 0x00195141
		public static OfficeFloorPosition operator +(OfficeFloorPosition a, OfficeFloorPosition b)
		{
			return new OfficeFloorPosition(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x00196F62 File Offset: 0x00195162
		public static OfficeFloorPosition operator -(OfficeFloorPosition a, OfficeFloorPosition b)
		{
			return new OfficeFloorPosition(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x00196F83 File Offset: 0x00195183
		bool IEquatable<OfficeFloorPosition>.Equals(OfficeFloorPosition other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x00196FA3 File Offset: 0x001951A3
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.x, this.y);
		}

		// Token: 0x040042D9 RID: 17113
		public int x;

		// Token: 0x040042DA RID: 17114
		public int y;
	}
}
