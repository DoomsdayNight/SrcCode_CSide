using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E99 RID: 3737
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_STAT_CHANGE_REQ)]
	public sealed class NKMPacket_EQUIP_TUNING_STAT_CHANGE_REQ : ISerializable
	{
		// Token: 0x0600981E RID: 38942 RVA: 0x0032DFB4 File Offset: 0x0032C1B4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUID);
			stream.PutOrGet(ref this.equipOptionID);
		}

		// Token: 0x04008A76 RID: 35446
		public long equipUID;

		// Token: 0x04008A77 RID: 35447
		public int equipOptionID = -1;
	}
}
