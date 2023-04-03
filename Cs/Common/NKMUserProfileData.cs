using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001042 RID: 4162
	public sealed class NKMUserProfileData : ISerializable
	{
		// Token: 0x06009B44 RID: 39748 RVA: 0x0033295C File Offset: 0x00330B5C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.friendIntro);
			stream.PutOrGet<NKMUserProfileData.PvpProfileData>(ref this.rankPvpData);
			stream.PutOrGet<NKMUserProfileData.PvpProfileData>(ref this.asyncPvpData);
			stream.PutOrGet<NKMUserProfileData.PvpProfileData>(ref this.leaguePvpData);
			stream.PutOrGet<NKMDummyDeckData>(ref this.profileDeck);
			stream.PutOrGet<NKMDummyDeckData>(ref this.leagueDeck);
			stream.PutOrGet<NKMAsyncDeckData>(ref this.defenceDeck);
			stream.PutOrGet<NKMEmblemData>(ref this.emblems);
			stream.PutOrGet(ref this.selfiFrameId);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
			stream.PutOrGet(ref this.hasOffice);
			stream.PutOrGetEnum<PrivatePvpInvitation>(ref this.privatePvpInvitation);
		}

		// Token: 0x04008EDF RID: 36575
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008EE0 RID: 36576
		public string friendIntro;

		// Token: 0x04008EE1 RID: 36577
		public NKMUserProfileData.PvpProfileData rankPvpData = new NKMUserProfileData.PvpProfileData();

		// Token: 0x04008EE2 RID: 36578
		public NKMUserProfileData.PvpProfileData asyncPvpData = new NKMUserProfileData.PvpProfileData();

		// Token: 0x04008EE3 RID: 36579
		public NKMUserProfileData.PvpProfileData leaguePvpData = new NKMUserProfileData.PvpProfileData();

		// Token: 0x04008EE4 RID: 36580
		public NKMDummyDeckData profileDeck;

		// Token: 0x04008EE5 RID: 36581
		public NKMDummyDeckData leagueDeck;

		// Token: 0x04008EE6 RID: 36582
		public NKMAsyncDeckData defenceDeck = new NKMAsyncDeckData();

		// Token: 0x04008EE7 RID: 36583
		public List<NKMEmblemData> emblems = new List<NKMEmblemData>();

		// Token: 0x04008EE8 RID: 36584
		public int selfiFrameId;

		// Token: 0x04008EE9 RID: 36585
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();

		// Token: 0x04008EEA RID: 36586
		public bool hasOffice;

		// Token: 0x04008EEB RID: 36587
		public PrivatePvpInvitation privatePvpInvitation;

		// Token: 0x02001A2D RID: 6701
		public sealed class PvpProfileData : ISerializable
		{
			// Token: 0x0600BB45 RID: 47941 RVA: 0x0036EA04 File Offset: 0x0036CC04
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.seasonId);
				stream.PutOrGet(ref this.leagueTierId);
				stream.PutOrGet(ref this.score);
			}

			// Token: 0x0400ADE2 RID: 44514
			public int seasonId;

			// Token: 0x0400ADE3 RID: 44515
			public int leagueTierId;

			// Token: 0x0400ADE4 RID: 44516
			public int score;
		}
	}
}
