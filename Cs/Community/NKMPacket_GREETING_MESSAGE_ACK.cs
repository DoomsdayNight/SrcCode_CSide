using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001004 RID: 4100
	[PacketId(ClientPacketId.kNKMPacket_GREETING_MESSAGE_ACK)]
	public sealed class NKMPacket_GREETING_MESSAGE_ACK : ISerializable
	{
		// Token: 0x06009AD8 RID: 39640 RVA: 0x00331F42 File Offset: 0x00330142
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.message);
		}

		// Token: 0x04008E38 RID: 36408
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E39 RID: 36409
		public string message;
	}
}
