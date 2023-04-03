using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EAF RID: 3759
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_REGISTER_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_REGISTER_REQ : ISerializable
	{
		// Token: 0x0600984A RID: 38986 RVA: 0x0032E37C File Offset: 0x0032C57C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGetEnum<ITEM_EQUIP_POSITION>(ref this.equipPosition);
			stream.PutOrGet(ref this.equipUid);
		}

		// Token: 0x04008AAA RID: 35498
		public int presetIndex;

		// Token: 0x04008AAB RID: 35499
		public ITEM_EQUIP_POSITION equipPosition;

		// Token: 0x04008AAC RID: 35500
		public long equipUid;
	}
}
