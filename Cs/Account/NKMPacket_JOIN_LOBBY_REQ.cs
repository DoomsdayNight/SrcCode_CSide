using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200106E RID: 4206
	[PacketId(ClientPacketId.kNKMPacket_JOIN_LOBBY_REQ)]
	public sealed class NKMPacket_JOIN_LOBBY_REQ : ISerializable
	{
		// Token: 0x06009B99 RID: 39833 RVA: 0x003335A3 File Offset: 0x003317A3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet(ref this.accessToken);
		}

		// Token: 0x04008F9F RID: 36767
		public int protocolVersion;

		// Token: 0x04008FA0 RID: 36768
		public string accessToken;
	}
}
