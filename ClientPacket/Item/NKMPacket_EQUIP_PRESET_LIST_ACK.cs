using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA8 RID: 3752
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_LIST_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_LIST_ACK : ISerializable
	{
		// Token: 0x0600983C RID: 38972 RVA: 0x0032E261 File Offset: 0x0032C461
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipPresetData>(ref this.presetDatas);
		}

		// Token: 0x04008A9B RID: 35483
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A9C RID: 35484
		public List<NKMEquipPresetData> presetDatas = new List<NKMEquipPresetData>();
	}
}
