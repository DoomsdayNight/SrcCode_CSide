using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CFC RID: 3324
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_ENHANCE_REQ)]
	public sealed class NKMPacket_OPERATOR_ENHANCE_REQ : ISerializable
	{
		// Token: 0x060094F5 RID: 38133 RVA: 0x00329734 File Offset: 0x00327934
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUnitUid);
			stream.PutOrGet(ref this.sourceUnitUid);
			stream.PutOrGet(ref this.transSkill);
		}

		// Token: 0x0400867E RID: 34430
		public long targetUnitUid;

		// Token: 0x0400867F RID: 34431
		public long sourceUnitUid;

		// Token: 0x04008680 RID: 34432
		public bool transSkill;
	}
}
