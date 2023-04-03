using System;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020004FC RID: 1276
	public class NKMEpisodeCompleteData : ISerializable
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x000BB840 File Offset: 0x000B9A40
		public void DeepCopyFromSource(NKMEpisodeCompleteData source)
		{
			this.m_EpisodeID = source.m_EpisodeID;
			this.m_EpisodeDifficulty = source.m_EpisodeDifficulty;
			this.m_EpisodeCompleteCount = source.m_EpisodeCompleteCount;
			for (int i = 0; i < 3; i++)
			{
				this.m_bRewards[i] = source.m_bRewards[i];
			}
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000BB88D File Offset: 0x000B9A8D
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_EpisodeID);
			stream.PutOrGetEnum<EPISODE_DIFFICULTY>(ref this.m_EpisodeDifficulty);
			stream.PutOrGet(ref this.m_EpisodeCompleteCount);
			stream.PutOrGet(ref this.m_bRewards);
		}

		// Token: 0x040025D4 RID: 9684
		public int m_EpisodeID;

		// Token: 0x040025D5 RID: 9685
		public EPISODE_DIFFICULTY m_EpisodeDifficulty;

		// Token: 0x040025D6 RID: 9686
		public int m_EpisodeCompleteCount;

		// Token: 0x040025D7 RID: 9687
		public bool[] m_bRewards = new bool[3];
	}
}
