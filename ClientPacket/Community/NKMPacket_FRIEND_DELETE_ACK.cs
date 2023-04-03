using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD8 RID: 4056
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_DELETE_ACK)]
	public sealed class NKMPacket_FRIEND_DELETE_ACK : ISerializable
	{
		// Token: 0x06009A80 RID: 39552 RVA: 0x00331857 File Offset: 0x0032FA57
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DD8 RID: 36312
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DD9 RID: 36313
		public long friendCode;
	}
}
