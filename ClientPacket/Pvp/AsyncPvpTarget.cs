using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D6C RID: 3436
	public sealed class AsyncPvpTarget : ISerializable
	{
		// Token: 0x060095D3 RID: 38355 RVA: 0x0032ABE8 File Offset: 0x00328DE8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userLevel);
			stream.PutOrGet(ref this.userNickName);
			stream.PutOrGet(ref this.userFriendCode);
			stream.PutOrGet(ref this.rank);
			stream.PutOrGet(ref this.score);
			stream.PutOrGet(ref this.tier);
			stream.PutOrGet(ref this.mainUnitId);
			stream.PutOrGet(ref this.mainUnitSkinId);
			stream.PutOrGet(ref this.selfieFrameId);
			stream.PutOrGet<NKMAsyncDeckData>(ref this.asyncDeck);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x040087AD RID: 34733
		public int userLevel;

		// Token: 0x040087AE RID: 34734
		public string userNickName;

		// Token: 0x040087AF RID: 34735
		public long userFriendCode;

		// Token: 0x040087B0 RID: 34736
		public int rank;

		// Token: 0x040087B1 RID: 34737
		public int score;

		// Token: 0x040087B2 RID: 34738
		public int tier;

		// Token: 0x040087B3 RID: 34739
		public int mainUnitId;

		// Token: 0x040087B4 RID: 34740
		public int mainUnitSkinId;

		// Token: 0x040087B5 RID: 34741
		public int selfieFrameId;

		// Token: 0x040087B6 RID: 34742
		public NKMAsyncDeckData asyncDeck = new NKMAsyncDeckData();

		// Token: 0x040087B7 RID: 34743
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
