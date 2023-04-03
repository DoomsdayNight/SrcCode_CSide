using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EBF RID: 3775
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_CHANGE_INDEX_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_CHANGE_INDEX_ACK : ISerializable
	{
		// Token: 0x0600986A RID: 39018 RVA: 0x0032E616 File Offset: 0x0032C816
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipPresetData>(ref this.presetDatas);
		}

		// Token: 0x04008ACB RID: 35531
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008ACC RID: 35532
		public List<NKMEquipPresetData> presetDatas = new List<NKMEquipPresetData>();
	}
}
