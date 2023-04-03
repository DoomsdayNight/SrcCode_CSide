using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001092 RID: 4242
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_ACK)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_ACK : ISerializable
	{
		// Token: 0x06009BE1 RID: 39905 RVA: 0x00333F93 File Offset: 0x00332193
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04009027 RID: 36903
		public NKM_ERROR_CODE errorCode;
	}
}
