using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E57 RID: 3671
	[PacketId(ClientPacketId.kNKMPacket_TRIM_START_ACK)]
	public sealed class NKMPacket_TRIM_START_ACK : ISerializable
	{
		// Token: 0x0600979E RID: 38814 RVA: 0x0032D376 File Offset: 0x0032B576
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<TrimModeState>(ref this.trimModeState);
		}

		// Token: 0x040089CC RID: 35276
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089CD RID: 35277
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x040089CE RID: 35278
		public TrimModeState trimModeState = new TrimModeState();
	}
}
