using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200047C RID: 1148
	public class NKMTeamCollectionData : ISerializable
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x00094743 File Offset: 0x00092943
		public int TeamID
		{
			get
			{
				return this.m_TeamID;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0009474B File Offset: 0x0009294B
		public bool Reward
		{
			get
			{
				return this.m_bReward;
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00094753 File Offset: 0x00092953
		public NKMTeamCollectionData()
		{
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0009475B File Offset: 0x0009295B
		public NKMTeamCollectionData(int teamID, bool reward)
		{
			this.m_TeamID = teamID;
			this.m_bReward = reward;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00094771 File Offset: 0x00092971
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_TeamID);
			stream.PutOrGet(ref this.m_bReward);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0009478B File Offset: 0x0009298B
		public void GiveReward()
		{
			this.m_bReward = true;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00094794 File Offset: 0x00092994
		public bool IsRewardComplete()
		{
			return this.m_bReward;
		}

		// Token: 0x04001FC0 RID: 8128
		private int m_TeamID;

		// Token: 0x04001FC1 RID: 8129
		private bool m_bReward;
	}
}
