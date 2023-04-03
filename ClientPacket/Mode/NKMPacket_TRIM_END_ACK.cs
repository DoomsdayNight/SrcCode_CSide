using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E5D RID: 3677
	[PacketId(ClientPacketId.kNKMPacket_TRIM_END_ACK)]
	public sealed class NKMPacket_TRIM_END_ACK : ISerializable
	{
		// Token: 0x060097AA RID: 38826 RVA: 0x0032D434 File Offset: 0x0032B634
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isFirst);
			stream.PutOrGet(ref this.bestScore);
			stream.PutOrGet<TrimModeState>(ref this.trimModeState);
			stream.PutOrGet<NKMTrimClearData>(ref this.trimClearData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x040089D5 RID: 35285
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089D6 RID: 35286
		public bool isFirst;

		// Token: 0x040089D7 RID: 35287
		public int bestScore;

		// Token: 0x040089D8 RID: 35288
		public TrimModeState trimModeState = new TrimModeState();

		// Token: 0x040089D9 RID: 35289
		public NKMTrimClearData trimClearData = new NKMTrimClearData();

		// Token: 0x040089DA RID: 35290
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
