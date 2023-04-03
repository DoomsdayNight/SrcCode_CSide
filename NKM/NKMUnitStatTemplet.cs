using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004A4 RID: 1188
	public class NKMUnitStatTemplet : INKMTemplet
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000A8F72 File Offset: 0x000A7172
		public int Key
		{
			get
			{
				return this.m_UnitID;
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000A8FA8 File Offset: 0x000A71A8
		public static NKMUnitStatTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 563))
			{
				return null;
			}
			NKMUnitStatTemplet nkmunitStatTemplet = new NKMUnitStatTemplet();
			cNKMLua.GetData("m_UnitStrID", ref nkmunitStatTemplet.m_UnitStrID);
			nkmunitStatTemplet.m_UnitID = NKMUnitManager.GetUnitID(nkmunitStatTemplet.m_UnitStrID);
			if (nkmunitStatTemplet.m_UnitID == 0)
			{
				Log.ErrorAndExit("GetUnitID failed. unitStrId:" + nkmunitStatTemplet.m_UnitStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 572);
				return null;
			}
			cNKMLua.GetData("m_RespawnCost", ref nkmunitStatTemplet.m_RespawnCost);
			cNKMLua.GetData("m_RespawnCount", ref nkmunitStatTemplet.m_RespawnCount);
			if (cNKMLua.OpenTable("m_StatData"))
			{
				nkmunitStatTemplet.m_StatData.LoadFromLUA(cNKMLua, false);
				cNKMLua.CloseTable();
			}
			return nkmunitStatTemplet;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000A9064 File Offset: 0x000A7264
		public void DeepCopyFromSource(NKMUnitStatTemplet source)
		{
			if (source == null)
			{
				return;
			}
			this.m_UnitID = source.m_UnitID;
			this.m_UnitStrID = source.m_UnitStrID;
			this.m_RespawnCost = source.m_RespawnCost;
			this.m_RespawnCount = source.m_RespawnCount;
			this.m_StatData.DeepCopyFromSource(source.m_StatData);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000A90B6 File Offset: 0x000A72B6
		public void Join()
		{
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000A90B8 File Offset: 0x000A72B8
		public void Validate()
		{
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000A90BC File Offset: 0x000A72BC
		public int GetRespawnCost(bool bLeader, Dictionary<int, NKMBanData> dicNKMBanData)
		{
			int num = this.m_RespawnCost;
			if (dicNKMBanData != null && dicNKMBanData.ContainsKey(this.m_UnitID))
			{
				NKMBanData nkmbanData = dicNKMBanData[this.m_UnitID];
				num = this.m_RespawnCost + (int)nkmbanData.m_BanLevel;
			}
			else if (bLeader)
			{
				num--;
			}
			if (num < 1)
			{
				num = 1;
			}
			if (num > 10)
			{
				num = 10;
			}
			return num;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000A9114 File Offset: 0x000A7314
		public int GetRespawnCost(bool bLeader, Dictionary<int, NKMBanData> dicNKMBanData, Dictionary<int, NKMUnitUpData> dicNKMUpData)
		{
			int num = this.m_RespawnCost;
			if (dicNKMBanData != null && dicNKMBanData.ContainsKey(this.m_UnitID))
			{
				NKMBanData nkmbanData = dicNKMBanData[this.m_UnitID];
				num = this.m_RespawnCost + (int)nkmbanData.m_BanLevel;
			}
			else if (bLeader)
			{
				num--;
			}
			if (dicNKMUpData != null && dicNKMUpData.ContainsKey(this.m_UnitID))
			{
				NKMUnitUpData nkmunitUpData = dicNKMUpData[this.m_UnitID];
				num -= (int)nkmunitUpData.upLevel;
			}
			if (num < 1)
			{
				num = 1;
			}
			if (num > 10)
			{
				num = 10;
			}
			return num;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000A9194 File Offset: 0x000A7394
		public int GetRespawnCost(bool bPVP, bool bLeader, Dictionary<int, NKMBanData> dicNKMBanData, Dictionary<int, NKMUnitUpData> dicNKMUpData)
		{
			if (bPVP)
			{
				return this.GetRespawnCost(bLeader, dicNKMBanData, dicNKMUpData);
			}
			int num = this.m_RespawnCost;
			if (bLeader)
			{
				num--;
			}
			if (num < 1)
			{
				num = 1;
			}
			if (num > 10)
			{
				num = 10;
			}
			return num;
		}

		// Token: 0x040021E3 RID: 8675
		public int m_UnitID;

		// Token: 0x040021E4 RID: 8676
		public string m_UnitStrID = "";

		// Token: 0x040021E5 RID: 8677
		private int m_RespawnCost = 1;

		// Token: 0x040021E6 RID: 8678
		public int m_RespawnCount = 1;

		// Token: 0x040021E7 RID: 8679
		public NKMStatData m_StatData = new NKMStatData();
	}
}
