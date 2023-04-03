using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D82 RID: 3458
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_START_GAME_REQ)]
	public sealed class NKMPacket_ASYNC_PVP_START_GAME_REQ : ISerializable
	{
		// Token: 0x060095FF RID: 38399 RVA: 0x0032B107 File Offset: 0x00329307
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetFriendCode);
			stream.PutOrGet(ref this.selectDeckIndex);
			stream.PutOrGetEnum<NKM_GAME_TYPE>(ref this.gameType);
		}

		// Token: 0x040087F7 RID: 34807
		public long targetFriendCode;

		// Token: 0x040087F8 RID: 34808
		public byte selectDeckIndex;

		// Token: 0x040087F9 RID: 34809
		public NKM_GAME_TYPE gameType;
	}
}
