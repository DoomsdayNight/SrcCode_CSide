using System;
using ClientPacket.Common;
using ClientPacket.Mode;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.Trim
{
	// Token: 0x02000898 RID: 2200
	public static class NKCTrimManager
	{
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x001A5108 File Offset: 0x001A3308
		// (set) Token: 0x060057C8 RID: 22472 RVA: 0x001A510F File Offset: 0x001A330F
		public static TrimModeState TrimModeState { get; private set; }

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x001A5117 File Offset: 0x001A3317
		// (set) Token: 0x060057CA RID: 22474 RVA: 0x001A511E File Offset: 0x001A331E
		public static bool GiveUpState { get; private set; }

		// Token: 0x060057CB RID: 22475 RVA: 0x001A5126 File Offset: 0x001A3326
		public static void Reset()
		{
			NKCTrimManager.ClearTrimModeState();
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x001A512D File Offset: 0x001A332D
		public static void SetGiveUpState(bool value)
		{
			NKCTrimManager.GiveUpState = value;
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x001A5135 File Offset: 0x001A3335
		public static void SetTrimModeState(TrimModeState state)
		{
			NKCTrimManager.TrimModeState = state;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x001A513D File Offset: 0x001A333D
		public static void ClearTrimModeState()
		{
			NKCTrimManager.TrimModeState = null;
			NKCTrimManager.GiveUpState = false;
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x001A514B File Offset: 0x001A334B
		public static NKMTrimTemplet GetTrimTemplet()
		{
			if (NKCTrimManager.TrimModeState == null)
			{
				return null;
			}
			return NKMTrimTemplet.Find(NKCTrimManager.TrimModeState.trimId);
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x001A5168 File Offset: 0x001A3368
		public static NKMTrimStageData GetFinishedTrimStageData(int index)
		{
			if (NKCTrimManager.TrimModeState == null)
			{
				return null;
			}
			if (NKCTrimManager.TrimModeState.lastClearStage.index == index)
			{
				return NKCTrimManager.TrimModeState.lastClearStage;
			}
			if (NKCTrimManager.TrimModeState.stageList == null)
			{
				return null;
			}
			return NKCTrimManager.TrimModeState.stageList.Find((NKMTrimStageData x) => x.index == index);
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x001A51D8 File Offset: 0x001A33D8
		public static int GetLastClearedTrimIndex(this TrimModeState trimState)
		{
			if (trimState.lastClearStage != null)
			{
				return trimState.lastClearStage.index;
			}
			if (trimState.stageList != null)
			{
				int num = -1;
				foreach (NKMTrimStageData nkmtrimStageData in trimState.stageList)
				{
					num = Mathf.Max(num, nkmtrimStageData.index);
				}
				return num;
			}
			return -1;
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x001A5254 File Offset: 0x001A3454
		public static bool ProcessTrim()
		{
			if (NKCTrimManager.TrimModeState == null)
			{
				return false;
			}
			if (NKCTrimManager.ShouldPlayNextTrim())
			{
				NKCTrimManager.PlayNextTrim();
				return true;
			}
			NKCPacketSender.Send_NKMPacket_TRIM_END_REQ(NKCTrimManager.TrimModeState.trimId);
			return true;
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x001A5280 File Offset: 0x001A3480
		private static bool ShouldPlayNextTrim()
		{
			if (NKCTrimManager.TrimModeState.trimId == 0)
			{
				Log.Error("TrimModeState trimid 0?", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCTrimManager.cs", 102);
				return false;
			}
			if (NKCTrimManager.TrimModeState.lastClearStage == null)
			{
				return true;
			}
			int num = NKCTrimManager.TrimModeState.lastClearStage.index + 1;
			if (num > 3)
			{
				return false;
			}
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(NKCTrimManager.TrimModeState.trimId);
			return (nkmtrimTemplet == null || num < nkmtrimTemplet.TrimDungeonIds.Length) && NKCTrimManager.TrimModeState.nextDungeonId > 0;
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x001A5304 File Offset: 0x001A3504
		public static bool WillPlayTrimDungeonCutscene(int trimID, int dungeonID, int level)
		{
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimID);
			if (nkmtrimTemplet == null)
			{
				return false;
			}
			NKMTrimDungeonTemplet trimDungeonTempletByDungeonID = nkmtrimTemplet.GetTrimDungeonTempletByDungeonID(NKCTrimManager.TrimModeState.nextDungeonId, NKCTrimManager.TrimModeState.trimLevel);
			if (trimDungeonTempletByDungeonID == null)
			{
				return false;
			}
			if (!trimDungeonTempletByDungeonID.m_bShowCutScene)
			{
				return false;
			}
			if (trimDungeonTempletByDungeonID.TrimLevelLow != level)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return (NKCScenManager.CurrentUserData() != null && NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene) || nkmuserData.TrimData.GetTrimClearData(NKCTrimManager.TrimModeState.trimId, NKCTrimManager.TrimModeState.trimLevel) == null;
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x001A5394 File Offset: 0x001A3594
		private static bool PlayNextTrim()
		{
			NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(0, 0, 0, NKCTrimManager.TrimModeState.nextDungeonId, 0, false, 1, 0);
			return true;
		}
	}
}
