using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D8E RID: 3470
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_INVITE_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_INVITE_REQ : ISerializable
	{
		// Token: 0x06009617 RID: 38423 RVA: 0x0032B389 File Offset: 0x00329589
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet<NKMPrivateGameConfig>(ref this.config);
		}

		// Token: 0x0400881D RID: 34845
		public long friendCode;

		// Token: 0x0400881E RID: 34846
		public NKMPrivateGameConfig config = new NKMPrivateGameConfig();
	}
}
