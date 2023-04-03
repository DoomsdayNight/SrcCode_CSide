using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE5 RID: 3301
	[PacketId(ClientPacketId.kNKMPacket_LOCK_UNIT_ACK)]
	public sealed class NKMPacket_LOCK_UNIT_ACK : ISerializable
	{
		// Token: 0x060094C7 RID: 38087 RVA: 0x00329320 File Offset: 0x00327520
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.isLock);
		}

		// Token: 0x04008647 RID: 34375
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008648 RID: 34376
		public long unitUID;

		// Token: 0x04008649 RID: 34377
		public bool isLock;
	}
}
