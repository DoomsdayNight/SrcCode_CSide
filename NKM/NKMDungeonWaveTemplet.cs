using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x020003E9 RID: 1001
	public class NKMDungeonWaveTemplet
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x00071754 File Offset: 0x0006F954
		public void Validate(string dungeonStrID)
		{
			if (this.m_listDungeonUnitRespawnA != null)
			{
				foreach (NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet in this.m_listDungeonUnitRespawnA)
				{
					nkmdungeonRespawnUnitTemplet.Validate(dungeonStrID);
				}
			}
			if (this.m_listDungeonUnitRespawnB != null)
			{
				foreach (NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet2 in this.m_listDungeonUnitRespawnB)
				{
					nkmdungeonRespawnUnitTemplet2.Validate(dungeonStrID);
				}
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000717F8 File Offset: 0x0006F9F8
		public bool LoadFromLUA(NKMLua cNKMLua, NKMDungeonTempletBase cNKMDungeonTempletBase)
		{
			cNKMLua.GetData("m_WaveID", ref this.m_WaveID);
			cNKMLua.GetData("m_NextWaveID", ref this.m_NextWaveID);
			cNKMLua.GetData("m_fNextWavetime", ref this.m_fNextWavetime);
			if (cNKMLua.OpenTable("m_listDungeonUnitRespawnA"))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet;
					if (this.m_listDungeonUnitRespawnA.Count >= num)
					{
						nkmdungeonRespawnUnitTemplet = this.m_listDungeonUnitRespawnA[num - 1];
					}
					else
					{
						nkmdungeonRespawnUnitTemplet = new NKMDungeonRespawnUnitTemplet();
						this.m_listDungeonUnitRespawnA.Add(nkmdungeonRespawnUnitTemplet);
					}
					nkmdungeonRespawnUnitTemplet.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE.WAVE_DUNGEON_UNIT_RESPAWN_A, this.m_WaveID, num);
					nkmdungeonRespawnUnitTemplet.m_WaveID = this.m_WaveID;
					cNKMLua.CloseTable();
					num++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listDungeonUnitRespawnB"))
			{
				int num2 = 1;
				while (cNKMLua.OpenTable(num2))
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet2;
					if (this.m_listDungeonUnitRespawnB.Count >= num2)
					{
						nkmdungeonRespawnUnitTemplet2 = this.m_listDungeonUnitRespawnB[num2 - 1];
					}
					else
					{
						nkmdungeonRespawnUnitTemplet2 = new NKMDungeonRespawnUnitTemplet();
						this.m_listDungeonUnitRespawnB.Add(nkmdungeonRespawnUnitTemplet2);
					}
					nkmdungeonRespawnUnitTemplet2.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE.WAVE_DUNGEON_UNIT_RESPAWN_B, this.m_WaveID, num2);
					nkmdungeonRespawnUnitTemplet2.m_WaveID = this.m_WaveID;
					cNKMLua.CloseTable();
					num2++;
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x0400137E RID: 4990
		public int m_WaveID;

		// Token: 0x0400137F RID: 4991
		public int m_NextWaveID;

		// Token: 0x04001380 RID: 4992
		public float m_fNextWavetime;

		// Token: 0x04001381 RID: 4993
		public List<NKMDungeonRespawnUnitTemplet> m_listDungeonUnitRespawnA = new List<NKMDungeonRespawnUnitTemplet>();

		// Token: 0x04001382 RID: 4994
		public List<NKMDungeonRespawnUnitTemplet> m_listDungeonUnitRespawnB = new List<NKMDungeonRespawnUnitTemplet>();
	}
}
