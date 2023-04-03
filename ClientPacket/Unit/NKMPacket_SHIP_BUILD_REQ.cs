using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CEC RID: 3308
	[PacketId(ClientPacketId.kNKMPacket_SHIP_BUILD_REQ)]
	public sealed class NKMPacket_SHIP_BUILD_REQ : ISerializable
	{
		// Token: 0x060094D5 RID: 38101 RVA: 0x00329475 File Offset: 0x00327675
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipID);
		}

		// Token: 0x04008659 RID: 34393
		public int shipID;
	}
}
