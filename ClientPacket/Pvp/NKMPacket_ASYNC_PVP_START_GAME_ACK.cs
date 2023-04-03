using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D83 RID: 3459
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_START_GAME_ACK)]
	public sealed class NKMPacket_ASYNC_PVP_START_GAME_ACK : ISerializable
	{
		// Token: 0x06009601 RID: 38401 RVA: 0x0032B135 File Offset: 0x00329335
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMGameData>(ref this.gameData);
			stream.PutOrGet<AsyncPvpTarget>(ref this.refreshedTargetData);
			stream.PutOrGet<AsyncPvpTarget>(ref this.targetList);
		}

		// Token: 0x040087FA RID: 34810
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087FB RID: 34811
		public NKMGameData gameData;

		// Token: 0x040087FC RID: 34812
		public AsyncPvpTarget refreshedTargetData = new AsyncPvpTarget();

		// Token: 0x040087FD RID: 34813
		public List<AsyncPvpTarget> targetList = new List<AsyncPvpTarget>();
	}
}
