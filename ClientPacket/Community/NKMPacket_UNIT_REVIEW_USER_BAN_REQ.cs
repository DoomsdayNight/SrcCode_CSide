using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200100B RID: 4107
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_USER_BAN_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_USER_BAN_REQ : ISerializable
	{
		// Token: 0x06009AE6 RID: 39654 RVA: 0x00332052 File Offset: 0x00330252
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008E47 RID: 36423
		public long targetUserUid;
	}
}
