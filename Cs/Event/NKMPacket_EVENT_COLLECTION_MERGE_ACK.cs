using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FB4 RID: 4020
	[PacketId(ClientPacketId.kNKMPacket_EVENT_COLLECTION_MERGE_ACK)]
	public sealed class NKMPacket_EVENT_COLLECTION_MERGE_ACK : ISerializable
	{
		// Token: 0x06009A3C RID: 39484 RVA: 0x0033116E File Offset: 0x0032F36E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.collectionMergeId);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.consumeTrophyUids);
		}

		// Token: 0x04008D75 RID: 36213
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D76 RID: 36214
		public int collectionMergeId;

		// Token: 0x04008D77 RID: 36215
		public NKMRewardData rewardData;

		// Token: 0x04008D78 RID: 36216
		public List<long> consumeTrophyUids = new List<long>();
	}
}
