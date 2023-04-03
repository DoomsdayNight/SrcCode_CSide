using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C9E RID: 3230
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_NEXT_ORDER_ACK : ISerializable
	{
		// Token: 0x06009439 RID: 37945 RVA: 0x00328602 File Offset: 0x00326802
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<WarfareSyncData>(ref this.warfareSyncData);
			stream.PutOrGet<NKMWarfareClearData>(ref this.warfareClearData);
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.episodeCompleteData);
		}

		// Token: 0x04008588 RID: 34184
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008589 RID: 34185
		public WarfareSyncData warfareSyncData = new WarfareSyncData();

		// Token: 0x0400858A RID: 34186
		public NKMWarfareClearData warfareClearData;

		// Token: 0x0400858B RID: 34187
		public NKMEpisodeCompleteData episodeCompleteData;
	}
}
