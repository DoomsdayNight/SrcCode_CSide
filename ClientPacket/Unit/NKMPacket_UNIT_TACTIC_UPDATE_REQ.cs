using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D1C RID: 3356
	[PacketId(ClientPacketId.kNKMPacket_UNIT_TACTIC_UPDATE_REQ)]
	public sealed class NKMPacket_UNIT_TACTIC_UPDATE_REQ : ISerializable
	{
		// Token: 0x06009535 RID: 38197 RVA: 0x00329CDA File Offset: 0x00327EDA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.consumeUnitUid);
		}

		// Token: 0x040086CF RID: 34511
		public long unitUid;

		// Token: 0x040086D0 RID: 34512
		public long consumeUnitUid;
	}
}
