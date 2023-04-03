using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001074 RID: 4212
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_UNLINK_ACK)]
	public sealed class NKMPacket_ACCOUNT_UNLINK_ACK : ISerializable
	{
		// Token: 0x06009BA5 RID: 39845 RVA: 0x00333A43 File Offset: 0x00331C43
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008FDD RID: 36829
		public NKM_ERROR_CODE errorCode;
	}
}
