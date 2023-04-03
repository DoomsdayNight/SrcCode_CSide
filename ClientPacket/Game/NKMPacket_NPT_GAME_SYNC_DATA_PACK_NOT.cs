using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F43 RID: 3907
	[PacketId(ClientPacketId.kNKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT)]
	public sealed class NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT : ISerializable
	{
		// Token: 0x06009966 RID: 39270 RVA: 0x0032FFF0 File Offset: 0x0032E1F0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.gameTime);
			stream.PutOrGet(ref this.absoluteGameTime);
			stream.PutOrGet<NKMGameSyncDataPack>(ref this.gameSyncDataPack);
		}

		// Token: 0x04008C6C RID: 35948
		public float gameTime;

		// Token: 0x04008C6D RID: 35949
		public float absoluteGameTime;

		// Token: 0x04008C6E RID: 35950
		public NKMGameSyncDataPack gameSyncDataPack;
	}
}
