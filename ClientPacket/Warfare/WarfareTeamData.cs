using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C95 RID: 3221
	public sealed class WarfareTeamData : ISerializable
	{
		// Token: 0x06009427 RID: 37927 RVA: 0x00328365 File Offset: 0x00326565
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.flagShipWarfareUnitUID);
			stream.PutOrGet<WarfareUnitData>(ref this.warfareUnitDataByUIDMap);
		}

		// Token: 0x04008562 RID: 34146
		public int flagShipWarfareUnitUID;

		// Token: 0x04008563 RID: 34147
		public Dictionary<int, WarfareUnitData> warfareUnitDataByUIDMap = new Dictionary<int, WarfareUnitData>();
	}
}
