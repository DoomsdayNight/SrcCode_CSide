using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C98 RID: 3224
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_START_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_START_ACK : ISerializable
	{
		// Token: 0x0600942D RID: 37933 RVA: 0x0032852E File Offset: 0x0032672E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<WarfareGameData>(ref this.warfareGameData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x0400857F RID: 34175
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008580 RID: 34176
		public WarfareGameData warfareGameData = new WarfareGameData();

		// Token: 0x04008581 RID: 34177
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
