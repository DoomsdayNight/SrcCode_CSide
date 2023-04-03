using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001095 RID: 4245
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_ACK)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009BE7 RID: 39911 RVA: 0x00333FD4 File Offset: 0x003321D4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04009029 RID: 36905
		public NKM_ERROR_CODE errorCode;
	}
}
