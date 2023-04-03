using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE3 RID: 3299
	[PacketId(ClientPacketId.kNKMPacket_ENHANCE_UNIT_ACK)]
	public sealed class NKMPacket_ENHANCE_UNIT_ACK : ISerializable
	{
		// Token: 0x060094C3 RID: 38083 RVA: 0x003292A2 File Offset: 0x003274A2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.statExpList);
			stream.PutOrGet(ref this.consumeUnitUIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008640 RID: 34368
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008641 RID: 34369
		public long unitUID;

		// Token: 0x04008642 RID: 34370
		public List<int> statExpList = new List<int>();

		// Token: 0x04008643 RID: 34371
		public List<long> consumeUnitUIDList = new List<long>();

		// Token: 0x04008644 RID: 34372
		public NKMItemMiscData costItemData;
	}
}
