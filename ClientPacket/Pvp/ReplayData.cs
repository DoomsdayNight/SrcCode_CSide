using System;
using System.Collections.Generic;
using ClientPacket.Game;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D6D RID: 3437
	public sealed class ReplayData : ISerializable
	{
		// Token: 0x060095D5 RID: 38357 RVA: 0x0032AC98 File Offset: 0x00328E98
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.replayName);
			stream.PutOrGet(ref this.replayVersion);
			stream.PutOrGet<NKMGameData>(ref this.gameData);
			stream.PutOrGet<NKMGameRuntimeData>(ref this.gameRuntimeData);
			stream.PutOrGet<NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT>(ref this.syncList);
			stream.PutOrGetEnum<PVP_RESULT>(ref this.pvpResult);
			stream.PutOrGet(ref this.gameEndTime);
			stream.PutOrGet<NKMGameRecord>(ref this.gameRecord);
			stream.PutOrGet<ReplayData.EmoticonData>(ref this.emoticonList);
		}

		// Token: 0x040087B8 RID: 34744
		public string replayName;

		// Token: 0x040087B9 RID: 34745
		public string replayVersion;

		// Token: 0x040087BA RID: 34746
		public NKMGameData gameData;

		// Token: 0x040087BB RID: 34747
		public NKMGameRuntimeData gameRuntimeData;

		// Token: 0x040087BC RID: 34748
		public List<NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT> syncList = new List<NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT>();

		// Token: 0x040087BD RID: 34749
		public PVP_RESULT pvpResult;

		// Token: 0x040087BE RID: 34750
		public float gameEndTime;

		// Token: 0x040087BF RID: 34751
		public NKMGameRecord gameRecord;

		// Token: 0x040087C0 RID: 34752
		public List<ReplayData.EmoticonData> emoticonList = new List<ReplayData.EmoticonData>();

		// Token: 0x02001A29 RID: 6697
		public sealed class EmoticonData : ISerializable
		{
			// Token: 0x0600BB3D RID: 47933 RVA: 0x0036E8E7 File Offset: 0x0036CAE7
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.time);
				stream.PutOrGet<NKMPacket_GAME_EMOTICON_NOT>(ref this.not);
			}

			// Token: 0x0400ADD4 RID: 44500
			public float time;

			// Token: 0x0400ADD5 RID: 44501
			public NKMPacket_GAME_EMOTICON_NOT not = new NKMPacket_GAME_EMOTICON_NOT();
		}
	}
}
