using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D06 RID: 3334
	[PacketId(ClientPacketId.kNKMPacket_REARMAMENT_UNIT_REQ)]
	public sealed class NKMPacket_REARMAMENT_UNIT_REQ : ISerializable
	{
		// Token: 0x06009509 RID: 38153 RVA: 0x00329936 File Offset: 0x00327B36
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.rearmamentId);
		}

		// Token: 0x0400869B RID: 34459
		public long unitUid;

		// Token: 0x0400869C RID: 34460
		public int rearmamentId;
	}
}
