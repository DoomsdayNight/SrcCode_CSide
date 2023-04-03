using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EAD RID: 3757
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_REGISTER_ALL_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_REGISTER_ALL_REQ : ISerializable
	{
		// Token: 0x06009846 RID: 38982 RVA: 0x0032E32D File Offset: 0x0032C52D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.presetIndex);
		}

		// Token: 0x04008AA6 RID: 35494
		public long unitUid;

		// Token: 0x04008AA7 RID: 35495
		public int presetIndex;
	}
}
