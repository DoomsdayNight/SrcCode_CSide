using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CEF RID: 3311
	[PacketId(ClientPacketId.kNKMPacket_SHIP_LEVELUP_ACK)]
	public sealed class NKMPacket_SHIP_LEVELUP_ACK : ISerializable
	{
		// Token: 0x060094DB RID: 38107 RVA: 0x003294E6 File Offset: 0x003276E6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipUnitData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x0400865F RID: 34399
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008660 RID: 34400
		public NKMUnitData shipUnitData;

		// Token: 0x04008661 RID: 34401
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
