using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB0 RID: 3760
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_REGISTER_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_REGISTER_ACK : ISerializable
	{
		// Token: 0x0600984C RID: 38988 RVA: 0x0032E3AA File Offset: 0x0032C5AA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipPresetData>(ref this.presetData);
		}

		// Token: 0x04008AAD RID: 35501
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AAE RID: 35502
		public NKMEquipPresetData presetData = new NKMEquipPresetData();
	}
}
