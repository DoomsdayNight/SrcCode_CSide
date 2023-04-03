using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000453 RID: 1107
	public class PvpSingleHistory : ISerializable, IComparable<PvpSingleHistory>
	{
		// Token: 0x06001E16 RID: 7702 RVA: 0x0008ED64 File Offset: 0x0008CF64
		public int CompareTo(PvpSingleHistory other)
		{
			return other.RegdateTick.CompareTo(this.RegdateTick);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0008ED78 File Offset: 0x0008CF78
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.gameUid);
			stream.PutOrGet(ref this.MyUserLevel);
			stream.PutOrGet(ref this.TargetUserLevel);
			stream.PutOrGet(ref this.TargetNickName);
			stream.PutOrGetEnum<PVP_RESULT>(ref this.Result);
			stream.PutOrGet(ref this.GainScore);
			stream.PutOrGet(ref this.MyTier);
			stream.PutOrGet(ref this.MyScore);
			stream.PutOrGet(ref this.TargetTier);
			stream.PutOrGet(ref this.TargetScore);
			stream.PutOrGet(ref this.RegdateTick);
			stream.PutOrGet<NKMAsyncDeckData>(ref this.MyDeckData);
			stream.PutOrGet<NKMAsyncDeckData>(ref this.TargetDeckData);
			stream.PutOrGetEnum<NKM_GAME_TYPE>(ref this.GameType);
			stream.PutOrGet(ref this.TargetFriendCode);
			stream.PutOrGet(ref this.SourceGuildUid);
			stream.PutOrGet(ref this.SourceGuildName);
			stream.PutOrGet(ref this.SourceGuildBadgeId);
			stream.PutOrGet(ref this.TargetGuildUid);
			stream.PutOrGet(ref this.TargetGuildName);
			stream.PutOrGet(ref this.TargetGuildBadgeId);
			stream.PutOrGet(ref this.myBanUnitIds);
			stream.PutOrGet(ref this.targetBanUnitIds);
			stream.PutOrGet(ref this.forfeitured);
		}

		// Token: 0x04001EAE RID: 7854
		public long gameUid;

		// Token: 0x04001EAF RID: 7855
		public long idx;

		// Token: 0x04001EB0 RID: 7856
		public int MyUserLevel;

		// Token: 0x04001EB1 RID: 7857
		public int TargetUserLevel;

		// Token: 0x04001EB2 RID: 7858
		public string TargetNickName;

		// Token: 0x04001EB3 RID: 7859
		public PVP_RESULT Result;

		// Token: 0x04001EB4 RID: 7860
		public int GainScore;

		// Token: 0x04001EB5 RID: 7861
		public int MyTier;

		// Token: 0x04001EB6 RID: 7862
		public int MyScore;

		// Token: 0x04001EB7 RID: 7863
		public int TargetTier;

		// Token: 0x04001EB8 RID: 7864
		public int TargetScore;

		// Token: 0x04001EB9 RID: 7865
		public long RegdateTick;

		// Token: 0x04001EBA RID: 7866
		public NKMAsyncDeckData MyDeckData;

		// Token: 0x04001EBB RID: 7867
		public NKMAsyncDeckData TargetDeckData;

		// Token: 0x04001EBC RID: 7868
		public NKM_GAME_TYPE GameType;

		// Token: 0x04001EBD RID: 7869
		public long TargetFriendCode;

		// Token: 0x04001EBE RID: 7870
		public long SourceGuildUid;

		// Token: 0x04001EBF RID: 7871
		public string SourceGuildName;

		// Token: 0x04001EC0 RID: 7872
		public long SourceGuildBadgeId;

		// Token: 0x04001EC1 RID: 7873
		public long TargetGuildUid;

		// Token: 0x04001EC2 RID: 7874
		public string TargetGuildName;

		// Token: 0x04001EC3 RID: 7875
		public long TargetGuildBadgeId;

		// Token: 0x04001EC4 RID: 7876
		public List<int> myBanUnitIds = new List<int>();

		// Token: 0x04001EC5 RID: 7877
		public List<int> targetBanUnitIds = new List<int>();

		// Token: 0x04001EC6 RID: 7878
		public bool forfeitured;
	}
}
