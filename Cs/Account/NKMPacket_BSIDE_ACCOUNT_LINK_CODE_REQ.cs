using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200108E RID: 4238
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ : ISerializable
	{
		// Token: 0x06009BD9 RID: 39897 RVA: 0x00333F19 File Offset: 0x00332119
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.linkCode);
		}

		// Token: 0x04009022 RID: 36898
		public string linkCode;
	}
}
