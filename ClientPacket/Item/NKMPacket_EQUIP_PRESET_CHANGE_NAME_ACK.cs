using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EAC RID: 3756
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_CHANGE_NAME_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_CHANGE_NAME_ACK : ISerializable
	{
		// Token: 0x06009844 RID: 38980 RVA: 0x0032E2FF File Offset: 0x0032C4FF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.newPresetName);
		}

		// Token: 0x04008AA3 RID: 35491
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AA4 RID: 35492
		public int presetIndex;

		// Token: 0x04008AA5 RID: 35493
		public string newPresetName;
	}
}
