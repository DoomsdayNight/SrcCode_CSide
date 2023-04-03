using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200108F RID: 4239
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK : ISerializable
	{
		// Token: 0x06009BDB RID: 39899 RVA: 0x00333F2F File Offset: 0x0033212F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04009023 RID: 36899
		public NKM_ERROR_CODE errorCode;
	}
}
