using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FDB RID: 4059
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_BLOCK_ACK)]
	public sealed class NKMPacket_FRIEND_BLOCK_ACK : ISerializable
	{
		// Token: 0x06009A86 RID: 39558 RVA: 0x003318BD File Offset: 0x0032FABD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.isCancel);
		}

		// Token: 0x04008DDE RID: 36318
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DDF RID: 36319
		public long friendCode;

		// Token: 0x04008DE0 RID: 36320
		public bool isCancel;
	}
}
