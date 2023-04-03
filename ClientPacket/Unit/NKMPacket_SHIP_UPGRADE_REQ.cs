using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF0 RID: 3312
	[PacketId(ClientPacketId.kNKMPacket_SHIP_UPGRADE_REQ)]
	public sealed class NKMPacket_SHIP_UPGRADE_REQ : ISerializable
	{
		// Token: 0x060094DD RID: 38109 RVA: 0x0032951F File Offset: 0x0032771F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUID);
			stream.PutOrGet(ref this.nextShipID);
		}

		// Token: 0x04008662 RID: 34402
		public long shipUID;

		// Token: 0x04008663 RID: 34403
		public int nextShipID;
	}
}
