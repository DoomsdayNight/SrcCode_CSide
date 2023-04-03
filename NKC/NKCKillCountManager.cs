using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x0200068E RID: 1678
	public static class NKCKillCountManager
	{
		// Token: 0x060036CC RID: 14028 RVA: 0x0011A42C File Offset: 0x0011862C
		public static void SetServerKillCountData(List<NKMServerKillCountData> serverKillCountData)
		{
			NKCKillCountManager.serverKillCountDataList = serverKillCountData;
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x0011A434 File Offset: 0x00118634
		public static void SetKillCountData(List<NKMKillCountData> killCountData)
		{
			NKCKillCountManager.killCountDataList = killCountData;
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x0011A43C File Offset: 0x0011863C
		public static NKMServerKillCountData GetKillCountServerData(int eventId)
		{
			if (NKCKillCountManager.serverKillCountDataList == null)
			{
				return null;
			}
			return NKCKillCountManager.serverKillCountDataList.Find((NKMServerKillCountData e) => e.killCountId == eventId);
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x0011A478 File Offset: 0x00118678
		public static NKMKillCountData GetKillCountData(int eventId)
		{
			if (NKCKillCountManager.killCountDataList == null)
			{
				return null;
			}
			return NKCKillCountManager.killCountDataList.Find((NKMKillCountData e) => e.killCountId == eventId);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x0011A4B4 File Offset: 0x001186B4
		public static void UpdateKillCountData(NKMKillCountData killCountData)
		{
			if (NKCKillCountManager.killCountDataList == null || killCountData == null)
			{
				return;
			}
			int num = NKCKillCountManager.killCountDataList.FindIndex((NKMKillCountData e) => e.killCountId == killCountData.killCountId);
			if (num < 0 || num >= NKCKillCountManager.killCountDataList.Count)
			{
				NKCKillCountManager.killCountDataList.Add(killCountData);
				return;
			}
			NKCKillCountManager.killCountDataList[num] = killCountData;
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x0011A528 File Offset: 0x00118728
		// (set) Token: 0x060036D2 RID: 14034 RVA: 0x0011A52F File Offset: 0x0011872F
		public static long CurrentStageKillCount { get; set; }

		// Token: 0x060036D3 RID: 14035 RVA: 0x0011A538 File Offset: 0x00118738
		public static bool IsKillCountDungeon(NKMGameData gameData)
		{
			NKMStageTempletV2 nkmstageTempletV;
			if (gameData.GetGameType() == NKM_GAME_TYPE.NGT_PHASE)
			{
				nkmstageTempletV = NKCPhaseManager.GetStageTemplet();
			}
			else
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameData.m_DungeonID);
				nkmstageTempletV = ((dungeonTempletBase != null) ? dungeonTempletBase.StageTemplet : null);
			}
			return nkmstageTempletV != null && nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_KILLCOUNT;
		}

		// Token: 0x040033FC RID: 13308
		private static List<NKMServerKillCountData> serverKillCountDataList;

		// Token: 0x040033FD RID: 13309
		private static List<NKMKillCountData> killCountDataList;
	}
}
