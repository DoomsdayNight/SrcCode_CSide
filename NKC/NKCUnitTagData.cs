using System;

namespace NKC
{
	// Token: 0x02000652 RID: 1618
	public class NKCUnitTagData
	{
		// Token: 0x0600329D RID: 12957 RVA: 0x000FBD87 File Offset: 0x000F9F87
		public NKCUnitTagData(short type, bool vote, int count, bool top)
		{
			this.TagType = type;
			this.Voted = vote;
			this.VoteCount = count;
			this.IsTop = top;
		}

		// Token: 0x04003183 RID: 12675
		public readonly short TagType;

		// Token: 0x04003184 RID: 12676
		public bool Voted;

		// Token: 0x04003185 RID: 12677
		public bool IsTop;

		// Token: 0x04003186 RID: 12678
		public int VoteCount;
	}
}
