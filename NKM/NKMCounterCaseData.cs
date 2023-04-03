using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004FA RID: 1274
	public class NKMCounterCaseData : ISerializable
	{
		// Token: 0x0600240E RID: 9230 RVA: 0x000BB7C9 File Offset: 0x000B99C9
		public NKMCounterCaseData()
		{
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000BB7D1 File Offset: 0x000B99D1
		public NKMCounterCaseData(int dungeonID, bool unlocked)
		{
			this.m_DungeonID = dungeonID;
			this.m_Unlocked = unlocked;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000BB7E7 File Offset: 0x000B99E7
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_DungeonID);
			stream.PutOrGet(ref this.m_Unlocked);
		}

		// Token: 0x040025D1 RID: 9681
		public int m_DungeonID;

		// Token: 0x040025D2 RID: 9682
		public bool m_Unlocked;
	}
}
