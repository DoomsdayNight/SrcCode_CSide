using System;

namespace NKM
{
	// Token: 0x020004FB RID: 1275
	public readonly struct EpisodeCompleteKey
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x000BB804 File Offset: 0x000B9A04
		public EpisodeCompleteKey(int episodeID, int episodeDifficulty)
		{
			long num = (long)episodeID;
			num <<= 32;
			num |= (long)((ulong)episodeDifficulty);
			this.m_EpisodeKey = num;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x000BB825 File Offset: 0x000B9A25
		public int EpisodeID
		{
			get
			{
				return (int)(this.m_EpisodeKey >> 32);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x000BB831 File Offset: 0x000B9A31
		public int EpisodeDifficulty
		{
			get
			{
				return (int)(this.m_EpisodeKey & (long)((ulong)-1));
			}
		}

		// Token: 0x040025D3 RID: 9683
		public readonly long m_EpisodeKey;
	}
}
