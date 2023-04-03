using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E4C RID: 3660
	[PacketId(ClientPacketId.kNKMPacket_STAGE_UNLOCK_ACK)]
	public sealed class NKMPacket_STAGE_UNLOCK_ACK : ISerializable
	{
		// Token: 0x06009788 RID: 38792 RVA: 0x0032D13C File Offset: 0x0032B33C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x040089AD RID: 35245
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089AE RID: 35246
		public int stageId;

		// Token: 0x040089AF RID: 35247
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
