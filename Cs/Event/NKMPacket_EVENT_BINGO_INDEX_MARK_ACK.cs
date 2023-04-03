using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F76 RID: 3958
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BINGO_INDEX_MARK_ACK)]
	public sealed class NKMPacket_EVENT_BINGO_INDEX_MARK_ACK : ISerializable
	{
		// Token: 0x060099C8 RID: 39368 RVA: 0x00330833 File Offset: 0x0032EA33
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.mileage);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008CE1 RID: 36065
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CE2 RID: 36066
		public int eventId;

		// Token: 0x04008CE3 RID: 36067
		public int mileage;

		// Token: 0x04008CE4 RID: 36068
		public NKMRewardData rewardData;
	}
}
