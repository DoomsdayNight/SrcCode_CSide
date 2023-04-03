using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB3 RID: 3763
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_NOT)]
	public sealed class NKMPacket_EQUIP_PRESET_NOT : ISerializable
	{
		// Token: 0x06009852 RID: 38994 RVA: 0x0032E432 File Offset: 0x0032C632
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMEquipPresetData>(ref this.presetDatas);
		}

		// Token: 0x04008AB4 RID: 35508
		public List<NKMEquipPresetData> presetDatas = new List<NKMEquipPresetData>();
	}
}
