using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E16 RID: 3606
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CHAT_LIST_REQ)]
	public sealed class NKMPacket_OFFICE_CHAT_LIST_REQ : ISerializable
	{
		// Token: 0x06009720 RID: 38688 RVA: 0x0032C8D2 File Offset: 0x0032AAD2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008935 RID: 35125
		public long userUid;
	}
}
