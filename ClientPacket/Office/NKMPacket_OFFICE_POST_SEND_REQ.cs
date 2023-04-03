using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E0D RID: 3597
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_SEND_REQ)]
	public sealed class NKMPacket_OFFICE_POST_SEND_REQ : ISerializable
	{
		// Token: 0x0600970E RID: 38670 RVA: 0x0032C76A File Offset: 0x0032A96A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.receiverUserUid);
		}

		// Token: 0x04008924 RID: 35108
		public long receiverUserUid;
	}
}
