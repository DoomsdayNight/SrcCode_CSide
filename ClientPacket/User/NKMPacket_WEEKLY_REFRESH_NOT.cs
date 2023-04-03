using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CDA RID: 3290
	[PacketId(ClientPacketId.kNKMPacket_WEEKLY_REFRESH_NOT)]
	public sealed class NKMPacket_WEEKLY_REFRESH_NOT : ISerializable
	{
		// Token: 0x060094B1 RID: 38065 RVA: 0x00329167 File Offset: 0x00327367
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMItemMiscData>(ref this.refreshItemDataList);
		}

		// Token: 0x04008630 RID: 34352
		public List<NKMItemMiscData> refreshItemDataList = new List<NKMItemMiscData>();
	}
}
