using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001051 RID: 4177
	public sealed class NKMDungeonRewardSet : ISerializable
	{
		// Token: 0x06009B62 RID: 39778 RVA: 0x00332DF3 File Offset: 0x00330FF3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.episodeCompleteData);
			stream.PutOrGet<NKMDungeonClearData>(ref this.dungeonClearData);
		}

		// Token: 0x04008F25 RID: 36645
		public NKMEpisodeCompleteData episodeCompleteData;

		// Token: 0x04008F26 RID: 36646
		public NKMDungeonClearData dungeonClearData = new NKMDungeonClearData();
	}
}
