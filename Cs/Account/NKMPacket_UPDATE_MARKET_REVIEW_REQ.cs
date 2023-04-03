using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200107D RID: 4221
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_MARKET_REVIEW_REQ)]
	public sealed class NKMPacket_UPDATE_MARKET_REVIEW_REQ : ISerializable
	{
		// Token: 0x06009BB7 RID: 39863 RVA: 0x00333C2F File Offset: 0x00331E2F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.description);
		}

		// Token: 0x04008FF8 RID: 36856
		public string description;
	}
}
