using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB8 RID: 3768
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_IMPRINT_REQ)]
	public sealed class NKMPacket_EQUIP_IMPRINT_REQ : ISerializable
	{
		// Token: 0x0600985C RID: 39004 RVA: 0x0032E4D9 File Offset: 0x0032C6D9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUid);
			stream.PutOrGet(ref this.unitId);
		}

		// Token: 0x04008ABB RID: 35515
		public long equipUid;

		// Token: 0x04008ABC RID: 35516
		public int unitId;
	}
}
