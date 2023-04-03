using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E5E RID: 3678
	[PacketId(ClientPacketId.kNKMPacket_TRIM_INTERVAL_INFO_NOT)]
	public sealed class NKMPacket_TRIM_INTERVAL_INFO_NOT : ISerializable
	{
		// Token: 0x060097AC RID: 38828 RVA: 0x0032D4B2 File Offset: 0x0032B6B2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimIntervalId);
			stream.PutOrGet<NKMTrimIntervalData>(ref this.trimIntervalData);
			stream.PutOrGet<NKMTrimClearData>(ref this.trimClearList);
		}

		// Token: 0x040089DB RID: 35291
		public int trimIntervalId;

		// Token: 0x040089DC RID: 35292
		public NKMTrimIntervalData trimIntervalData = new NKMTrimIntervalData();

		// Token: 0x040089DD RID: 35293
		public List<NKMTrimClearData> trimClearList = new List<NKMTrimClearData>();
	}
}
