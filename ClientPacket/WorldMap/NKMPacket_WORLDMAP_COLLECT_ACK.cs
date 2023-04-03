using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C86 RID: 3206
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_COLLECT_ACK)]
	public sealed class NKMPacket_WORLDMAP_COLLECT_ACK : ISerializable
	{
		// Token: 0x0600940B RID: 37899 RVA: 0x00327F41 File Offset: 0x00326141
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.collectItemDataList);
		}

		// Token: 0x0400851C RID: 34076
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400851D RID: 34077
		public List<NKMItemMiscData> collectItemDataList = new List<NKMItemMiscData>();
	}
}
