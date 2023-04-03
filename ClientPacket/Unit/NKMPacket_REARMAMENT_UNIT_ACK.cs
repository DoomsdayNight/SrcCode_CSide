using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D07 RID: 3335
	[PacketId(ClientPacketId.kNKMPacket_REARMAMENT_UNIT_ACK)]
	public sealed class NKMPacket_REARMAMENT_UNIT_ACK : ISerializable
	{
		// Token: 0x0600950B RID: 38155 RVA: 0x00329958 File Offset: 0x00327B58
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.rearmamentUnitData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
		}

		// Token: 0x0400869D RID: 34461
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400869E RID: 34462
		public NKMUnitData rearmamentUnitData;

		// Token: 0x0400869F RID: 34463
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();
	}
}
