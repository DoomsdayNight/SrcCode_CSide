using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CFF RID: 3327
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_LOCK_ACK)]
	public sealed class NKMPacket_OPERATOR_LOCK_ACK : ISerializable
	{
		// Token: 0x060094FB RID: 38139 RVA: 0x003297CA File Offset: 0x003279CA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.locked);
		}

		// Token: 0x04008688 RID: 34440
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008689 RID: 34441
		public long unitUID;

		// Token: 0x0400868A RID: 34442
		public bool locked;
	}
}
