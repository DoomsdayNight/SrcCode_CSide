using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001085 RID: 4229
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_BIRTHDAY_ACK)]
	public sealed class NKMPacket_ACCOUNT_BIRTHDAY_ACK : ISerializable
	{
		// Token: 0x06009BC7 RID: 39879 RVA: 0x00333D0F File Offset: 0x00331F0F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.birthday);
		}

		// Token: 0x04009004 RID: 36868
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04009005 RID: 36869
		public DateTime birthday;
	}
}
