using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E31 RID: 3633
	[PacketId(ClientPacketId.kNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK)]
	public sealed class NKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK : ISerializable
	{
		// Token: 0x06009752 RID: 38738 RVA: 0x0032CD05 File Offset: 0x0032AF05
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDungeonClearData>(ref this.dungeonClearData);
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.episodeCompleteData);
		}

		// Token: 0x04008973 RID: 35187
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008974 RID: 35188
		public NKMDungeonClearData dungeonClearData = new NKMDungeonClearData();

		// Token: 0x04008975 RID: 35189
		public NKMEpisodeCompleteData episodeCompleteData;
	}
}
