using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200109D RID: 4253
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ)]
	public sealed class NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ : ISerializable
	{
		// Token: 0x06009BF7 RID: 39927 RVA: 0x0033406C File Offset: 0x0033226C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.code);
		}

		// Token: 0x0400902F RID: 36911
		public string code;
	}
}
