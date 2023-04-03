using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E1F RID: 3615
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_ADD_ACK)]
	public sealed class NKMPacket_OFFICE_PRESET_ADD_ACK : ISerializable
	{
		// Token: 0x06009732 RID: 38706 RVA: 0x0032CA81 File Offset: 0x0032AC81
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.totalPresetCount);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDatas);
		}

		// Token: 0x0400894B RID: 35147
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400894C RID: 35148
		public int totalPresetCount;

		// Token: 0x0400894D RID: 35149
		public List<NKMItemMiscData> costItemDatas = new List<NKMItemMiscData>();
	}
}
