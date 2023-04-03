using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CAF RID: 3247
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_SET_LEADER_ACK)]
	public sealed class NKMPacket_DECK_UNIT_SET_LEADER_ACK : ISerializable
	{
		// Token: 0x0600945B RID: 37979 RVA: 0x00328972 File Offset: 0x00326B72
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.leaderSlotIndex);
		}

		// Token: 0x040085BA RID: 34234
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085BB RID: 34235
		public NKMDeckIndex deckIndex;

		// Token: 0x040085BC RID: 34236
		public sbyte leaderSlotIndex;
	}
}
