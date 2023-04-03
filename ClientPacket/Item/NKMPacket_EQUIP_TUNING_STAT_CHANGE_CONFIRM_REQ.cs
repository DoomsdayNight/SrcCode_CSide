using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E9B RID: 3739
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ)]
	public sealed class NKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ : ISerializable
	{
		// Token: 0x06009822 RID: 38946 RVA: 0x0032E039 File Offset: 0x0032C239
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUID);
			stream.PutOrGet(ref this.equipOptionID);
		}

		// Token: 0x04008A7D RID: 35453
		public long equipUID;

		// Token: 0x04008A7E RID: 35454
		public int equipOptionID = -1;
	}
}
