using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FDD RID: 4061
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_CANCEL_REQUEST_ACK)]
	public sealed class NKMPacket_FRIEND_CANCEL_REQUEST_ACK : ISerializable
	{
		// Token: 0x06009A8A RID: 39562 RVA: 0x00331901 File Offset: 0x0032FB01
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DE2 RID: 36322
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DE3 RID: 36323
		public long friendCode;
	}
}
