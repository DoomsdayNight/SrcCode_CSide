using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001071 RID: 4209
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_LINK_ACK)]
	public sealed class NKMPacket_ACCOUNT_LINK_ACK : ISerializable
	{
		// Token: 0x06009B9F RID: 39839 RVA: 0x00333A0D File Offset: 0x00331C0D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008FDB RID: 36827
		public NKM_ERROR_CODE errorCode;
	}
}
