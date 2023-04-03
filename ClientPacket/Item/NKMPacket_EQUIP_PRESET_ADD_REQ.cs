using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA9 RID: 3753
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_ADD_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_ADD_REQ : ISerializable
	{
		// Token: 0x0600983E RID: 38974 RVA: 0x0032E28E File Offset: 0x0032C48E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.addPresetCount);
		}

		// Token: 0x04008A9D RID: 35485
		public int addPresetCount;
	}
}
