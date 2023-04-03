using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EAE RID: 3758
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_REGISTER_ALL_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_REGISTER_ALL_ACK : ISerializable
	{
		// Token: 0x06009848 RID: 38984 RVA: 0x0032E34F File Offset: 0x0032C54F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipPresetData>(ref this.presetData);
		}

		// Token: 0x04008AA8 RID: 35496
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AA9 RID: 35497
		public NKMEquipPresetData presetData = new NKMEquipPresetData();
	}
}
