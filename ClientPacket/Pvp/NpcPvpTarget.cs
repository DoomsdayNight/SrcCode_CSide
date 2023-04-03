using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D70 RID: 3440
	public sealed class NpcPvpTarget : ISerializable
	{
		// Token: 0x060095DB RID: 38363 RVA: 0x0032AE84 File Offset: 0x00329084
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userLevel);
			stream.PutOrGet(ref this.userNickName);
			stream.PutOrGet(ref this.userFriendCode);
			stream.PutOrGet(ref this.score);
			stream.PutOrGet(ref this.tier);
			stream.PutOrGet<NKMAsyncDeckData>(ref this.asyncDeck);
			stream.PutOrGet(ref this.isOpened);
		}

		// Token: 0x040087D5 RID: 34773
		public int userLevel;

		// Token: 0x040087D6 RID: 34774
		public string userNickName;

		// Token: 0x040087D7 RID: 34775
		public long userFriendCode;

		// Token: 0x040087D8 RID: 34776
		public int score;

		// Token: 0x040087D9 RID: 34777
		public int tier;

		// Token: 0x040087DA RID: 34778
		public NKMAsyncDeckData asyncDeck = new NKMAsyncDeckData();

		// Token: 0x040087DB RID: 34779
		public bool isOpened;
	}
}
