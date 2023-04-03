using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EAA RID: 3754
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_ADD_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_ADD_ACK : ISerializable
	{
		// Token: 0x06009840 RID: 38976 RVA: 0x0032E2A4 File Offset: 0x0032C4A4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.totalPresetCount);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008A9E RID: 35486
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A9F RID: 35487
		public int totalPresetCount;

		// Token: 0x04008AA0 RID: 35488
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
