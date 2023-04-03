using System;
using System.Collections.Generic;
using Cs.Logging;

namespace NKM
{
	// Token: 0x020003EC RID: 1004
	public class NKMDungeonTemplet
	{
		// Token: 0x06001A58 RID: 6744 RVA: 0x00071CD8 File Offset: 0x0006FED8
		public bool LoadFromLUA(NKMLua cNKMLua, NKMDungeonTempletBase cNKMDungeonTempletBase)
		{
			this.m_DungeonTempletBase = cNKMDungeonTempletBase;
			cNKMLua.GetData("m_bCanUseAuto", ref this.m_bCanUseAuto);
			cNKMLua.GetData("m_fStartCost", ref this.m_fStartCost);
			cNKMLua.GetData("m_bRespawnFreePos", ref this.m_bRespawnFreePos);
			cNKMLua.GetData("m_fCostSpeedRateA", ref this.m_fCostSpeedRateA);
			cNKMLua.GetData("m_fCostSpeedRateB", ref this.m_fCostSpeedRateB);
			cNKMLua.GetData("m_BossUnitStrID", ref this.m_BossUnitStrID);
			cNKMLua.GetData("m_BossUnitChangeName", ref this.m_BossUnitChangeName);
			cNKMLua.GetData("m_BossUnitLevel", ref this.m_BossUnitLevel);
			if (this.m_BossUnitLevel == 0)
			{
				this.m_BossUnitLevel = cNKMDungeonTempletBase.m_DungeonLevel;
			}
			cNKMLua.GetData("m_fBossPosZ", ref this.m_fBossPosZ);
			if (cNKMLua.OpenTable("m_BossRespawnUnitTemplet"))
			{
				this.m_BossRespawnUnitTemplet.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE.BOSS_RESPAWN_UNIT, 0, 0);
				cNKMLua.CloseTable();
			}
			if (!string.IsNullOrEmpty(this.m_BossUnitChangeName))
			{
				this.m_BossRespawnUnitTemplet.m_ChangeUnitName = this.m_BossUnitChangeName;
			}
			cNKMLua.GetData("m_bNoTimeStop", ref this.m_bNoTimeStop);
			cNKMLua.GetData("m_bNoEnemyRespawnBeforeUserFirstRespawn", ref this.m_bNoEnemyRespawnBeforeUserFirstRespawn);
			cNKMLua.GetData("m_fAllyHyperCooltimeStartRatio", ref this.m_fAllyHyperCooltimeStartRatio);
			cNKMLua.GetData("m_fEnemyHyperCooltimeStartRatio", ref this.m_fEnemyHyperCooltimeStartRatio);
			cNKMLua.GetData("m_DungeonTag", ref this.m_DungeonTag);
			if (cNKMLua.GetDataList("m_listValidLand", out this.m_listValidLand))
			{
				if (this.m_listValidLand.Count != 3)
				{
					Log.Error(cNKMDungeonTempletBase.DebugName + " : m_listValidLand count must 3!!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 578);
					this.m_listValidLand = null;
				}
			}
			else
			{
				this.m_listValidLand = null;
			}
			if (cNKMLua.OpenTable("m_listDungeonDeck"))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet;
					if (this.m_listDungeonDeck.Count >= num)
					{
						nkmdungeonRespawnUnitTemplet = this.m_listDungeonDeck[num - 1];
					}
					else
					{
						nkmdungeonRespawnUnitTemplet = new NKMDungeonRespawnUnitTemplet();
						this.m_listDungeonDeck.Add(nkmdungeonRespawnUnitTemplet);
					}
					nkmdungeonRespawnUnitTemplet.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE.DUNGEON_DECK, 0, num);
					cNKMLua.CloseTable();
					num++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listDungeonWave"))
			{
				int num2 = 1;
				while (cNKMLua.OpenTable(num2))
				{
					NKMDungeonWaveTemplet nkmdungeonWaveTemplet;
					if (this.m_listDungeonWave.Count >= num2)
					{
						nkmdungeonWaveTemplet = this.m_listDungeonWave[num2 - 1];
					}
					else
					{
						nkmdungeonWaveTemplet = new NKMDungeonWaveTemplet();
						this.m_listDungeonWave.Add(nkmdungeonWaveTemplet);
					}
					nkmdungeonWaveTemplet.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase);
					cNKMLua.CloseTable();
					num2++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listDungeonUnitRespawnA"))
			{
				int num3 = 1;
				while (cNKMLua.OpenTable(num3))
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet2;
					if (this.m_listDungeonUnitRespawnA.Count >= num3)
					{
						nkmdungeonRespawnUnitTemplet2 = this.m_listDungeonUnitRespawnA[num3 - 1];
					}
					else
					{
						nkmdungeonRespawnUnitTemplet2 = new NKMDungeonRespawnUnitTemplet();
						this.m_listDungeonUnitRespawnA.Add(nkmdungeonRespawnUnitTemplet2);
					}
					nkmdungeonRespawnUnitTemplet2.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE.DUNGEON_UNIT_RESPAWN_A, 0, num3);
					cNKMLua.CloseTable();
					num3++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listDungeonUnitRespawnB"))
			{
				int num4 = 1;
				while (cNKMLua.OpenTable(num4))
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet3;
					if (this.m_listDungeonUnitRespawnB.Count >= num4)
					{
						nkmdungeonRespawnUnitTemplet3 = this.m_listDungeonUnitRespawnB[num4 - 1];
					}
					else
					{
						nkmdungeonRespawnUnitTemplet3 = new NKMDungeonRespawnUnitTemplet();
						this.m_listDungeonUnitRespawnB.Add(nkmdungeonRespawnUnitTemplet3);
					}
					nkmdungeonRespawnUnitTemplet3.LoadFromLUA(cNKMLua, cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE.DUNGEON_UNIT_RESPAWN_B, 0, num4);
					cNKMLua.CloseTable();
					num4++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listDungeonEventTempletTeamA"))
			{
				int num5 = 1;
				while (cNKMLua.OpenTable(num5))
				{
					NKMDungeonEventTemplet nkmdungeonEventTemplet;
					if (this.m_listDungeonEventTempletTeamA.Count >= num5)
					{
						nkmdungeonEventTemplet = this.m_listDungeonEventTempletTeamA[num5 - 1];
					}
					else
					{
						nkmdungeonEventTemplet = new NKMDungeonEventTemplet();
						this.m_listDungeonEventTempletTeamA.Add(nkmdungeonEventTemplet);
					}
					nkmdungeonEventTemplet.LoadFromLUA(cNKMLua);
					cNKMLua.CloseTable();
					num5++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listDungeonEventTempletTeamB"))
			{
				int num6 = 1;
				while (cNKMLua.OpenTable(num6))
				{
					NKMDungeonEventTemplet nkmdungeonEventTemplet2;
					if (this.m_listDungeonEventTempletTeamB.Count >= num6)
					{
						nkmdungeonEventTemplet2 = this.m_listDungeonEventTempletTeamB[num6 - 1];
					}
					else
					{
						nkmdungeonEventTemplet2 = new NKMDungeonEventTemplet();
						this.m_listDungeonEventTempletTeamB.Add(nkmdungeonEventTemplet2);
					}
					nkmdungeonEventTemplet2.LoadFromLUA(cNKMLua);
					cNKMLua.CloseTable();
					num6++;
				}
				cNKMLua.CloseTable();
			}
			this.m_bLoaded = true;
			return true;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0007213C File Offset: 0x0007033C
		public NKMDungeonWaveTemplet GetWaveTemplet(int waveID)
		{
			for (int i = 0; i < this.m_listDungeonWave.Count; i++)
			{
				NKMDungeonWaveTemplet nkmdungeonWaveTemplet = this.m_listDungeonWave[i];
				if (nkmdungeonWaveTemplet != null && nkmdungeonWaveTemplet.m_WaveID == waveID)
				{
					return nkmdungeonWaveTemplet;
				}
			}
			return null;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0007217C File Offset: 0x0007037C
		public int GetNextWave(int waveID)
		{
			for (int i = 0; i < this.m_listDungeonWave.Count; i++)
			{
				NKMDungeonWaveTemplet nkmdungeonWaveTemplet = this.m_listDungeonWave[i];
				if (nkmdungeonWaveTemplet != null && nkmdungeonWaveTemplet.m_WaveID == waveID)
				{
					return nkmdungeonWaveTemplet.m_NextWaveID;
				}
			}
			return 0;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000721C0 File Offset: 0x000703C0
		public bool CheckValidWave(int waveID)
		{
			for (int i = 0; i < this.m_listDungeonWave.Count; i++)
			{
				NKMDungeonWaveTemplet nkmdungeonWaveTemplet = this.m_listDungeonWave[i];
				if (nkmdungeonWaveTemplet != null && nkmdungeonWaveTemplet.m_WaveID == waveID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001397 RID: 5015
		public bool m_bLoaded;

		// Token: 0x04001398 RID: 5016
		public NKMDungeonTempletBase m_DungeonTempletBase;

		// Token: 0x04001399 RID: 5017
		public bool m_bCanUseAuto = true;

		// Token: 0x0400139A RID: 5018
		public bool m_bRespawnFreePos;

		// Token: 0x0400139B RID: 5019
		public float m_fStartCost;

		// Token: 0x0400139C RID: 5020
		public float m_fCostSpeedRateA = 1f;

		// Token: 0x0400139D RID: 5021
		public float m_fCostSpeedRateB = 1f;

		// Token: 0x0400139E RID: 5022
		public string m_BossUnitStrID = "";

		// Token: 0x0400139F RID: 5023
		public int m_BossUnitLevel;

		// Token: 0x040013A0 RID: 5024
		public string m_BossUnitChangeName = "";

		// Token: 0x040013A1 RID: 5025
		public float m_fBossPosZ = 1f;

		// Token: 0x040013A2 RID: 5026
		public NKMDungeonRespawnUnitTemplet m_BossRespawnUnitTemplet = new NKMDungeonRespawnUnitTemplet();

		// Token: 0x040013A3 RID: 5027
		public bool m_bNoTimeStop;

		// Token: 0x040013A4 RID: 5028
		public bool m_bNoEnemyRespawnBeforeUserFirstRespawn;

		// Token: 0x040013A5 RID: 5029
		public float m_fAllyHyperCooltimeStartRatio = -1f;

		// Token: 0x040013A6 RID: 5030
		public float m_fEnemyHyperCooltimeStartRatio = -1f;

		// Token: 0x040013A7 RID: 5031
		public List<NKMDungeonRespawnUnitTemplet> m_listDungeonDeck = new List<NKMDungeonRespawnUnitTemplet>();

		// Token: 0x040013A8 RID: 5032
		public List<NKMDungeonWaveTemplet> m_listDungeonWave = new List<NKMDungeonWaveTemplet>();

		// Token: 0x040013A9 RID: 5033
		public List<NKMDungeonRespawnUnitTemplet> m_listDungeonUnitRespawnA = new List<NKMDungeonRespawnUnitTemplet>();

		// Token: 0x040013AA RID: 5034
		public List<NKMDungeonRespawnUnitTemplet> m_listDungeonUnitRespawnB = new List<NKMDungeonRespawnUnitTemplet>();

		// Token: 0x040013AB RID: 5035
		public List<NKMDungeonEventTemplet> m_listDungeonEventTempletTeamA = new List<NKMDungeonEventTemplet>();

		// Token: 0x040013AC RID: 5036
		public List<NKMDungeonEventTemplet> m_listDungeonEventTempletTeamB = new List<NKMDungeonEventTemplet>();

		// Token: 0x040013AD RID: 5037
		public string m_DungeonTag;

		// Token: 0x040013AE RID: 5038
		public List<float> m_listValidLand;
	}
}
