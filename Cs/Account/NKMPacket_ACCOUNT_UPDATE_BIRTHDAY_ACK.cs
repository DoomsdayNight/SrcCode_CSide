using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001087 RID: 4231
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_UPDATE_BIRTHDAY_ACK)]
	public sealed class NKMPacket_ACCOUNT_UPDATE_BIRTHDAY_ACK : ISerializable
	{
		// Token: 0x06009BCB RID: 39883 RVA: 0x00333D47 File Offset: 0x00331F47
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.birthday);
		}

		// Token: 0x04009007 RID: 36871
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04009008 RID: 36872
		public DateTime birthday;
	}
}
