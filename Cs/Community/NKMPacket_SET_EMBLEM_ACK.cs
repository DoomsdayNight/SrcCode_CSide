using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE9 RID: 4073
	[PacketId(ClientPacketId.kNKMPacket_SET_EMBLEM_ACK)]
	public sealed class NKMPacket_SET_EMBLEM_ACK : ISerializable
	{
		// Token: 0x06009AA2 RID: 39586 RVA: 0x00331AB0 File Offset: 0x0032FCB0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.index);
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008DFB RID: 36347
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DFC RID: 36348
		public sbyte index;

		// Token: 0x04008DFD RID: 36349
		public int itemId;

		// Token: 0x04008DFE RID: 36350
		public long count;
	}
}
