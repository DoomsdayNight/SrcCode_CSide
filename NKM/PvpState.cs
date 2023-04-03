using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000364 RID: 868
	public class PvpState : ISerializable
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x0004E285 File Offset: 0x0004C485
		public static bool IsBanPossibleScore(int score)
		{
			return score >= NKMPvpCommonConst.Instance.PvpUnitBanExceptionScore;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0004E297 File Offset: 0x0004C497
		public bool IsBanPossibleScore()
		{
			return PvpState.IsBanPossibleScore(this.Score);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		public static void SetPrevScore(int score)
		{
			PvpState.m_sPrevScore = score;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0004E2AC File Offset: 0x0004C4AC
		public static int GetPrevScore()
		{
			return PvpState.m_sPrevScore;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.SeasonID);
			stream.PutOrGet(ref this.WeekID);
			stream.PutOrGet(ref this.WinCount);
			stream.PutOrGet(ref this.LoseCount);
			stream.PutOrGet(ref this.LeagueTierID);
			stream.PutOrGet(ref this.MaxLeagueTierID);
			stream.PutOrGet(ref this.Score);
			stream.PutOrGet(ref this.MaxScore);
			stream.PutOrGet(ref this.WinStreak);
			stream.PutOrGet(ref this.MaxWinStreak);
			stream.PutOrGet(ref this.Rank);
		}

		// Token: 0x04000E5F RID: 3679
		private static int m_sPrevScore;

		// Token: 0x04000E60 RID: 3680
		public int SeasonID;

		// Token: 0x04000E61 RID: 3681
		public int WeekID;

		// Token: 0x04000E62 RID: 3682
		public int WinCount;

		// Token: 0x04000E63 RID: 3683
		public int LoseCount;

		// Token: 0x04000E64 RID: 3684
		public int LeagueTierID;

		// Token: 0x04000E65 RID: 3685
		public int MaxLeagueTierID;

		// Token: 0x04000E66 RID: 3686
		public int Score;

		// Token: 0x04000E67 RID: 3687
		public int MaxScore;

		// Token: 0x04000E68 RID: 3688
		public int WinStreak;

		// Token: 0x04000E69 RID: 3689
		public int MaxWinStreak;

		// Token: 0x04000E6A RID: 3690
		public int Rank;
	}
}
