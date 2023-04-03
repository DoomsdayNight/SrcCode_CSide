using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003EB RID: 1003
	public class NKMDungeonRespawnUnitTemplet
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00071934 File Offset: 0x0006FB34
		public string StrKey
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_ChangeUnitName))
				{
					return this.m_UnitStrID;
				}
				return string.Format("{0}@{1}", this.m_UnitStrID, this.m_ChangeUnitName);
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00071960 File Offset: 0x0006FB60
		public void Validate(string dungeonStrID)
		{
			if (!string.IsNullOrEmpty(this.m_UnitStrID))
			{
				if (NKMUnitManager.GetUnitTemplet(this.m_UnitStrID) == null)
				{
					Log.ErrorAndExit(string.Concat(new string[]
					{
						"[NKMDungeonRespawnUnitTemplet] 유닛 정보가 존재하지 않음 DungeonStrID [",
						dungeonStrID,
						"], m_UnitStrID [",
						this.m_UnitStrID,
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 400);
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitStrID);
				if (unitTempletBase == null)
				{
					Log.ErrorAndExit(string.Concat(new string[]
					{
						"[NKMDungeonRespawnUnitTemplet] 유닛 템플릿이 존재하지 않음 DungeonStrID [",
						dungeonStrID,
						"], m_UnitStrID [",
						this.m_UnitStrID,
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 406);
				}
				if (this.m_SkinID != 0)
				{
					NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_SkinID);
					if (skinTemplet == null)
					{
						Log.ErrorAndExit(string.Format("[NKMDungeonRespawnUnitTemplet]스킨 템플릿이 존재하지 않음 DungeonStrID [{0}], m_UnitStrID [{1}], m_SkinID [{2}]", dungeonStrID, this.m_UnitStrID, this.m_SkinID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 414);
					}
					if (!NKMSkinManager.IsSkinForCharacter(unitTempletBase.m_UnitID, skinTemplet))
					{
						Log.ErrorAndExit(string.Format("[NKMDungeonRespawnUnitTemplet]지정된 스킨이 목표 유닛을 위한 스킨이 아님 DungeonStrID [{0}], m_UnitStrID [{1}], m_SkinID [{2}]", dungeonStrID, this.m_UnitStrID, this.m_SkinID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 419);
					}
				}
			}
			if (this.m_listStaticBuffData != null)
			{
				foreach (NKMStaticBuffData nkmstaticBuffData in this.m_listStaticBuffData)
				{
					if (!nkmstaticBuffData.Validate())
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[NKMDungeonRespawnUnitTemplet] m_listStaticBuffData is invalid. DungeonStrID [",
							dungeonStrID,
							"], StaticBuffStrID [",
							nkmstaticBuffData.m_BuffStrID,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 430);
					}
				}
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00071B20 File Offset: 0x0006FD20
		public bool LoadFromLUA(NKMLua cNKMLua, NKMDungeonTempletBase cNKMDungeonTempletBase, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE dungeonRespawnUnitTempletType, int waveID, int respawnUnitCount)
		{
			this.m_TempletUID = NKMDungeonManager.AddNKMDungeonRespawnUnitTemplet(this, cNKMDungeonTempletBase.m_DungeonStrID, cNKMDungeonTempletBase.m_DungeonID, dungeonRespawnUnitTempletType, waveID, respawnUnitCount);
			if (cNKMLua.OpenTable("m_NKMDungeonEventTiming"))
			{
				this.m_NKMDungeonEventTiming.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_EventRespawnUnitTag", ref this.m_EventRespawnUnitTag);
			cNKMLua.GetData("m_UnitStrID", ref this.m_UnitStrID);
			cNKMLua.GetData("m_SkinID", ref this.m_SkinID);
			cNKMLua.GetData("m_UnitLevel", ref this.m_UnitLevel);
			cNKMLua.GetData("m_UnitLimitBreakLevel", ref this.m_UnitLimitBreakLevel);
			if (this.m_UnitLevel == 0)
			{
				this.m_UnitLevel = cNKMDungeonTempletBase.m_DungeonLevel;
			}
			bool flag = false;
			if (cNKMLua.GetData("m_bShowGage", ref flag))
			{
				this.m_eShowGage = (flag ? NKMDungeonRespawnUnitTemplet.ShowGageOverride.Show : NKMDungeonRespawnUnitTemplet.ShowGageOverride.Hide);
			}
			else
			{
				this.m_eShowGage = NKMDungeonRespawnUnitTemplet.ShowGageOverride.Default;
			}
			if (cNKMLua.OpenTable("m_listStaticBuffData"))
			{
				this.m_listStaticBuffData.Clear();
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					NKMStaticBuffData nkmstaticBuffData = new NKMStaticBuffData();
					nkmstaticBuffData.LoadFromLUA(cNKMLua);
					this.m_listStaticBuffData.Add(nkmstaticBuffData);
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_AddStatData"))
			{
				this.m_AddStatData.LoadFromLUA(cNKMLua, true);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_fRespawnCoolTime", ref this.m_fRespawnCoolTime);
			cNKMLua.GetData("m_ChangeUnitName", ref this.m_ChangeUnitName);
			return true;
		}

		// Token: 0x0400138A RID: 5002
		public long m_TempletUID;

		// Token: 0x0400138B RID: 5003
		public NKMDungeonEventTiming m_NKMDungeonEventTiming = new NKMDungeonEventTiming();

		// Token: 0x0400138C RID: 5004
		public string m_EventRespawnUnitTag = "";

		// Token: 0x0400138D RID: 5005
		public string m_UnitStrID = "";

		// Token: 0x0400138E RID: 5006
		public int m_UnitLevel;

		// Token: 0x0400138F RID: 5007
		public int m_SkinID;

		// Token: 0x04001390 RID: 5008
		public short m_UnitLimitBreakLevel;

		// Token: 0x04001391 RID: 5009
		public List<NKMStaticBuffData> m_listStaticBuffData = new List<NKMStaticBuffData>();

		// Token: 0x04001392 RID: 5010
		public NKMDungeonRespawnUnitTemplet.ShowGageOverride m_eShowGage;

		// Token: 0x04001393 RID: 5011
		public NKMStatData m_AddStatData = new NKMStatData();

		// Token: 0x04001394 RID: 5012
		public float m_fRespawnCoolTime;

		// Token: 0x04001395 RID: 5013
		public int m_WaveID;

		// Token: 0x04001396 RID: 5014
		public string m_ChangeUnitName;

		// Token: 0x020011DD RID: 4573
		public enum ShowGageOverride
		{
			// Token: 0x040093AA RID: 37802
			Default,
			// Token: 0x040093AB RID: 37803
			Show,
			// Token: 0x040093AC RID: 37804
			Hide
		}
	}
}
