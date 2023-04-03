using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D6F RID: 3439
	public sealed class RevengePvpTarget : ISerializable
	{
		// Token: 0x060095D9 RID: 38361 RVA: 0x0032ADBC File Offset: 0x00328FBC
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
			stream.PutOrGet(ref this.revengeAble);
			stream.PutOrGetEnum<PVP_RESULT>(ref this.result);
			stream.PutOrGet<NKMAsyncDeckData>(ref this.asyncDeck);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x040087C8 RID: 34760
		public int userLevel;

		// Token: 0x040087C9 RID: 34761
		public string userNickName;

		// Token: 0x040087CA RID: 34762
		public long userFriendCode;

		// Token: 0x040087CB RID: 34763
		public int rank;

		// Token: 0x040087CC RID: 34764
		public int score;

		// Token: 0x040087CD RID: 34765
		public int tier;

		// Token: 0x040087CE RID: 34766
		public int mainUnitId;

		// Token: 0x040087CF RID: 34767
		public int mainUnitSkinId;

		// Token: 0x040087D0 RID: 34768
		public int selfieFrameId;

		// Token: 0x040087D1 RID: 34769
		public bool revengeAble;

		// Token: 0x040087D2 RID: 34770
		public PVP_RESULT result;

		// Token: 0x040087D3 RID: 34771
		public NKMAsyncDeckData asyncDeck = new NKMAsyncDeckData();

		// Token: 0x040087D4 RID: 34772
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
