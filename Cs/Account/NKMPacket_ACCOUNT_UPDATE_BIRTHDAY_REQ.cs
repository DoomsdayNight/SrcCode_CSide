using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001086 RID: 4230
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_UPDATE_BIRTHDAY_REQ)]
	public sealed class NKMPacket_ACCOUNT_UPDATE_BIRTHDAY_REQ : ISerializable
	{
		// Token: 0x06009BC9 RID: 39881 RVA: 0x00333D31 File Offset: 0x00331F31
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.birthday);
		}

		// Token: 0x04009006 RID: 36870
		public DateTime birthday;
	}
}
