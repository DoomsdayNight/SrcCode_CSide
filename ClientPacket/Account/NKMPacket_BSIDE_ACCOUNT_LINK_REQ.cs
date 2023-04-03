using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200108B RID: 4235
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_REQ)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_REQ : ISerializable
	{
		// Token: 0x06009BD3 RID: 39891 RVA: 0x00333E9D File Offset: 0x0033209D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x0400901C RID: 36892
		public long friendCode;
	}
}
