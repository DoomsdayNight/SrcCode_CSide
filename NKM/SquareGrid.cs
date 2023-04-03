using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x02000394 RID: 916
	public class SquareGrid
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x0005EF1D File Offset: 0x0005D11D
		public SquareGrid(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0005EF42 File Offset: 0x0005D142
		public bool InBounds(NKMAStarSearchLocation id)
		{
			return this.x <= id.x && id.x < this.width && this.y <= id.y && id.y < this.height;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0005EF7E File Offset: 0x0005D17E
		public bool Passable(NKMAStarSearchLocation id)
		{
			return this.tiles[id.x, id.y] < NKM_ASTAR_SEARCH_TILE_TYPE.NASTT_WALL;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005EF9E File Offset: 0x0005D19E
		public float Cost(NKMAStarSearchLocation a, NKMAStarSearchLocation b)
		{
			return (float)this.tiles[b.x, b.y];
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0005EFB8 File Offset: 0x0005D1B8
		public IEnumerable<NKMAStarSearchLocation> Neighbors(NKMAStarSearchLocation id)
		{
			if (!this.bCrossMoveType)
			{
				foreach (NKMAStarSearchLocation nkmastarSearchLocation in SquareGrid.DIRS)
				{
					NKMAStarSearchLocation nkmastarSearchLocation2 = new NKMAStarSearchLocation(id.x + nkmastarSearchLocation.x, id.y + nkmastarSearchLocation.y);
					if (this.InBounds(nkmastarSearchLocation2) && this.Passable(nkmastarSearchLocation2))
					{
						yield return nkmastarSearchLocation2;
					}
				}
				NKMAStarSearchLocation[] array = null;
			}
			else
			{
				foreach (NKMAStarSearchLocation nkmastarSearchLocation3 in SquareGrid.DIRS_Cross)
				{
					NKMAStarSearchLocation nkmastarSearchLocation4 = new NKMAStarSearchLocation(id.x + nkmastarSearchLocation3.x, id.y + nkmastarSearchLocation3.y);
					if (this.InBounds(nkmastarSearchLocation4) && this.Passable(nkmastarSearchLocation4))
					{
						yield return nkmastarSearchLocation4;
					}
				}
				NKMAStarSearchLocation[] array = null;
			}
			yield break;
		}

		// Token: 0x04000FEC RID: 4076
		public static readonly NKMAStarSearchLocation[] DIRS = new NKMAStarSearchLocation[]
		{
			new NKMAStarSearchLocation(1, 0),
			new NKMAStarSearchLocation(0, -1),
			new NKMAStarSearchLocation(-1, 0),
			new NKMAStarSearchLocation(0, 1),
			new NKMAStarSearchLocation(1, 1),
			new NKMAStarSearchLocation(-1, 1),
			new NKMAStarSearchLocation(1, -1),
			new NKMAStarSearchLocation(-1, -1)
		};

		// Token: 0x04000FED RID: 4077
		public static readonly NKMAStarSearchLocation[] DIRS_Cross = new NKMAStarSearchLocation[]
		{
			new NKMAStarSearchLocation(1, 0),
			new NKMAStarSearchLocation(0, -1),
			new NKMAStarSearchLocation(-1, 0),
			new NKMAStarSearchLocation(0, 1)
		};

		// Token: 0x04000FEE RID: 4078
		public bool bCrossMoveType;

		// Token: 0x04000FEF RID: 4079
		public int x;

		// Token: 0x04000FF0 RID: 4080
		public int y;

		// Token: 0x04000FF1 RID: 4081
		public int width;

		// Token: 0x04000FF2 RID: 4082
		public int height;

		// Token: 0x04000FF3 RID: 4083
		public NKM_ASTAR_SEARCH_TILE_TYPE[,] tiles;
	}
}
