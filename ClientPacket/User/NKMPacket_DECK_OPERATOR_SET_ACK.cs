using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB9 RID: 3257
	[PacketId(ClientPacketId.kNKMPacket_DECK_OPERATOR_SET_ACK)]
	public sealed class NKMPacket_DECK_OPERATOR_SET_ACK : ISerializable
	{
		// Token: 0x0600946F RID: 37999 RVA: 0x00328B86 File Offset: 0x00326D86
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.operatorUid);
			stream.PutOrGet<NKMDeckIndex>(ref this.oldDeckIndex);
		}

		// Token: 0x040085DC RID: 34268
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085DD RID: 34269
		public NKMDeckIndex deckIndex;

		// Token: 0x040085DE RID: 34270
		public long operatorUid;

		// Token: 0x040085DF RID: 34271
		public NKMDeckIndex oldDeckIndex;
	}
}
