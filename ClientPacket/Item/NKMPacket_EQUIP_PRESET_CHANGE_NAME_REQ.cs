using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EAB RID: 3755
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_CHANGE_NAME_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_CHANGE_NAME_REQ : ISerializable
	{
		// Token: 0x06009842 RID: 38978 RVA: 0x0032E2DD File Offset: 0x0032C4DD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.newPresetName);
		}

		// Token: 0x04008AA1 RID: 35489
		public int presetIndex;

		// Token: 0x04008AA2 RID: 35490
		public string newPresetName;
	}
}
