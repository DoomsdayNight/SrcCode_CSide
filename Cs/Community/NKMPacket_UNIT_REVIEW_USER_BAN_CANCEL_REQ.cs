using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200100D RID: 4109
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009AEA RID: 39658 RVA: 0x0033208A File Offset: 0x0033028A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008E4A RID: 36426
		public long targetUserUid;
	}
}
