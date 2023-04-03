using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD5 RID: 4053
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_REQUEST_ACK)]
	public sealed class NKMPacket_FRIEND_REQUEST_ACK : ISerializable
	{
		// Token: 0x06009A7A RID: 39546 RVA: 0x003317FE File Offset: 0x0032F9FE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DD4 RID: 36308
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DD5 RID: 36309
		public long friendCode;
	}
}
