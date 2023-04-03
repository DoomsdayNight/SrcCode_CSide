using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F30 RID: 3888
	[PacketId(ClientPacketId.kNKMPacket_GAME_LOAD_ACK)]
	public sealed class NKMPacket_GAME_LOAD_ACK : ISerializable
	{
		// Token: 0x06009940 RID: 39232 RVA: 0x0032FB64 File Offset: 0x0032DD64
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMGameData>(ref this.gameData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008C27 RID: 35879
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C28 RID: 35880
		public NKMGameData gameData;

		// Token: 0x04008C29 RID: 35881
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
