using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D94 RID: 3476
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_CANCEL_NOT)]
	public sealed class NKMPacket_PRIVATE_PVP_CANCEL_NOT : ISerializable
	{
		// Token: 0x06009621 RID: 38433 RVA: 0x0032B448 File Offset: 0x00329648
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGetEnum<PrivatePvpCancelType>(ref this.cancelType);
		}

		// Token: 0x0400882F RID: 34863
		public long targetUserUid;

		// Token: 0x04008830 RID: 34864
		public PrivatePvpCancelType cancelType;
	}
}
