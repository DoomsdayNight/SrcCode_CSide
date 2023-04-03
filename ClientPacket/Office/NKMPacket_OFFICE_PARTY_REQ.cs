using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E18 RID: 3608
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PARTY_REQ)]
	public sealed class NKMPacket_OFFICE_PARTY_REQ : ISerializable
	{
		// Token: 0x06009724 RID: 38692 RVA: 0x0032C921 File Offset: 0x0032AB21
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
		}

		// Token: 0x04008939 RID: 35129
		public int roomId;
	}
}
