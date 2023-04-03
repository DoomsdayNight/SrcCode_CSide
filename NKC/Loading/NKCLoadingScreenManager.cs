using System;
using System.Collections.Generic;
using System.Linq;
using NKC.UI;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.Loading
{
	// Token: 0x0200089A RID: 2202
	public static class NKCLoadingScreenManager
	{
		// Token: 0x060057DE RID: 22494 RVA: 0x001A550E File Offset: 0x001A370E
		private static int BuildKey(NKCLoadingScreenManager.eGameContentsType type, int value)
		{
			return (int)(value * 11 + type);
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x001A5518 File Offset: 0x001A3718
		public static NKCLoadingScreenManager.eGameContentsType GetGameContentsType(NKM_GAME_TYPE gameType)
		{
			switch (gameType)
			{
			case NKM_GAME_TYPE.NGT_INVALID:
			case NKM_GAME_TYPE.NGT_DEV:
			case NKM_GAME_TYPE.NGT_PRACTICE:
				return NKCLoadingScreenManager.eGameContentsType.DEFAULT;
			case NKM_GAME_TYPE.NGT_DUNGEON:
			case NKM_GAME_TYPE.NGT_TUTORIAL:
			case NKM_GAME_TYPE.NGT_PHASE:
				return NKCLoadingScreenManager.eGameContentsType.DUNGEON;
			case NKM_GAME_TYPE.NGT_WARFARE:
				return NKCLoadingScreenManager.eGameContentsType.WARFARE;
			case NKM_GAME_TYPE.NGT_DIVE:
				return NKCLoadingScreenManager.eGameContentsType.DIVE;
			case NKM_GAME_TYPE.NGT_PVP_RANK:
			case NKM_GAME_TYPE.NGT_ASYNC_PVP:
			case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
			case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
			case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
			case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
			case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
				return NKCLoadingScreenManager.eGameContentsType.GAUNTLET;
			case NKM_GAME_TYPE.NGT_RAID:
			case NKM_GAME_TYPE.NGT_RAID_SOLO:
				return NKCLoadingScreenManager.eGameContentsType.RAID;
			case NKM_GAME_TYPE.NGT_CUTSCENE:
			case NKM_GAME_TYPE.NGT_WORLDMAP:
				return NKCLoadingScreenManager.eGameContentsType.DEFAULT;
			case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
				return NKCLoadingScreenManager.eGameContentsType.SHADOW_PALACE;
			case NKM_GAME_TYPE.NGT_FIERCE:
				return NKCLoadingScreenManager.eGameContentsType.FIERCE;
			case NKM_GAME_TYPE.NGT_TRIM:
				return NKCLoadingScreenManager.eGameContentsType.TRIM;
			}
			return NKCLoadingScreenManager.eGameContentsType.DEFAULT;
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x001A55A8 File Offset: 0x001A37A8
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Load(from e in NKMTempletLoader<NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData>.LoadGroup("AB_SCRIPT", "LUA_LOADING_TEMPLET", "m_LoadingTemplet", new Func<NKMLua, NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData>(NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData.LoadFromLua))
			select new NKCLoadingScreenManager.NKCLoadingScreenTemplet(e.Key, e.Value), null);
			NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingImgTemplet>.Load("AB_SCRIPT", "LUA_LOADING_IMG_TEMPLET", "m_LoadingImg", new Func<NKMLua, NKCLoadingScreenManager.NKCLoadingImgTemplet>(NKCLoadingScreenManager.NKCLoadingImgTemplet.LoadFromLua));
			NKCLoadingScreenManager.LoadLoadingTipString();
			if (NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Find(9) == null)
			{
				Debug.LogError("Default Loading Screen Not Exist!!!");
			}
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x001A5638 File Offset: 0x001A3838
		private static bool LoadLoadingTipString()
		{
			NKMLua nkmlua = new NKMLua();
			bool flag = nkmlua.LoadCommonPath("AB_SCRIPT", "LUA_TOOLTIP_TEMPLET", true);
			if (flag)
			{
				flag = nkmlua.OpenTable("m_TooltipTemplet");
				if (flag)
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKCLoadingScreenManager.NKCLoadingDescTemplet nkcloadingDescTemplet = NKCLoadingScreenManager.NKCLoadingDescTemplet.LoadFromLUA(nkmlua);
						if (nkcloadingDescTemplet != null)
						{
							NKCLoadingScreenManager.s_lstLoadingDescTemplet.Add(nkcloadingDescTemplet);
						}
						num++;
						nkmlua.CloseTable();
					}
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return flag;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001A56AA File Offset: 0x001A38AA
		public static bool HasLoadingTemplet(NKCLoadingScreenManager.eGameContentsType contentType, int contentValue)
		{
			return NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Find(NKCLoadingScreenManager.BuildKey(contentType, contentValue)) != null;
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x001A56BC File Offset: 0x001A38BC
		private static NKCLoadingScreenManager.NKCLoadingScreenTemplet GetLoadingTemplet(NKCLoadingScreenManager.eGameContentsType contentType, int contentValue, int dungeonID = 0)
		{
			NKCLoadingScreenManager.NKCLoadingScreenTemplet nkcloadingScreenTemplet = null;
			if (contentValue != 0)
			{
				nkcloadingScreenTemplet = NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Find(NKCLoadingScreenManager.BuildKey(contentType, contentValue));
			}
			if (nkcloadingScreenTemplet == null && dungeonID != 0)
			{
				nkcloadingScreenTemplet = NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Find(NKCLoadingScreenManager.BuildKey(NKCLoadingScreenManager.eGameContentsType.DUNGEON, dungeonID));
			}
			if (nkcloadingScreenTemplet == null)
			{
				nkcloadingScreenTemplet = NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Find(NKCLoadingScreenManager.BuildKey(contentType, 0));
			}
			return nkcloadingScreenTemplet;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x001A5700 File Offset: 0x001A3900
		public static Tuple<NKCLoadingScreenManager.NKCLoadingImgTemplet, string> GetLoadingScreen(NKCLoadingScreenManager.eGameContentsType contentType, int contentValue, int dungeonID = 0)
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return new Tuple<NKCLoadingScreenManager.NKCLoadingImgTemplet, string>(null, "");
			}
			NKCLoadingScreenManager.NKCLoadingScreenTemplet nkcloadingScreenTemplet = NKCLoadingScreenManager.GetLoadingTemplet(contentType, contentValue, dungeonID);
			if (nkcloadingScreenTemplet == null)
			{
				nkcloadingScreenTemplet = NKCLoadingScreenManager.GetLoadingTempletFromEpisode(contentType, contentValue);
			}
			if (nkcloadingScreenTemplet == null)
			{
				nkcloadingScreenTemplet = NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Find(9);
			}
			if (nkcloadingScreenTemplet == null)
			{
				return new Tuple<NKCLoadingScreenManager.NKCLoadingImgTemplet, string>(null, "");
			}
			NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData randomData = nkcloadingScreenTemplet.GetRandomData();
			NKCLoadingScreenManager.NKCLoadingImgTemplet item = NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingImgTemplet>.Find(randomData.m_ImgID);
			string item2;
			if (string.IsNullOrEmpty(randomData.m_DescStrID))
			{
				item2 = NKCLoadingScreenManager.GetRandomLoadingTip();
			}
			else
			{
				item2 = randomData.m_DescStrID;
			}
			return new Tuple<NKCLoadingScreenManager.NKCLoadingImgTemplet, string>(item, item2);
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x001A5784 File Offset: 0x001A3984
		public static void ResetUnlockCache()
		{
			foreach (NKCLoadingScreenManager.NKCLoadingScreenTemplet nkcloadingScreenTemplet in NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingScreenTemplet>.Values)
			{
				nkcloadingScreenTemplet.ResetUnlockCache();
			}
			foreach (NKCLoadingScreenManager.NKCLoadingDescTemplet nkcloadingDescTemplet in NKCLoadingScreenManager.s_lstLoadingDescTemplet)
			{
				nkcloadingDescTemplet.m_bUnlockCache = false;
			}
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x001A580C File Offset: 0x001A3A0C
		private static string GetRandomLoadingTip()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < NKCLoadingScreenManager.s_lstLoadingDescTemplet.Count; i++)
			{
				NKCLoadingScreenManager.NKCLoadingDescTemplet nkcloadingDescTemplet = NKCLoadingScreenManager.s_lstLoadingDescTemplet[i];
				if (nkcloadingDescTemplet.m_bUnlockCache)
				{
					list.Add(i);
				}
				else if (nkcloadingDescTemplet.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED)
				{
					list.Add(i);
				}
				else if (NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkcloadingDescTemplet.m_UnlockInfo, false))
				{
					nkcloadingDescTemplet.m_bUnlockCache = true;
					list.Add(i);
				}
			}
			int index = list[NKMRandom.Range(0, list.Count)];
			return NKCLoadingScreenManager.s_lstLoadingDescTemplet[index].m_StrTooltipDesc;
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x001A58B0 File Offset: 0x001A3AB0
		private static NKCLoadingScreenManager.NKCLoadingScreenTemplet GetLoadingTempletFromEpisode(NKCLoadingScreenManager.eGameContentsType contentType, int contentValue)
		{
			if (contentType != NKCLoadingScreenManager.eGameContentsType.WARFARE)
			{
				if (contentType == NKCLoadingScreenManager.eGameContentsType.DUNGEON)
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(contentValue);
					if (dungeonTempletBase != null)
					{
						NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonTempletBase.m_DungeonStrID);
						if (nkmstageTempletV != null)
						{
							return NKCLoadingScreenManager.GetLoadingTemplet(NKCLoadingScreenManager.eGameContentsType.EPISODE, nkmstageTempletV.EpisodeId, 0);
						}
					}
				}
			}
			else
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(contentValue);
				if (nkmwarfareTemplet != null)
				{
					NKMStageTempletV2 nkmstageTempletV2 = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
					return NKCLoadingScreenManager.GetLoadingTemplet(NKCLoadingScreenManager.eGameContentsType.EPISODE, nkmstageTempletV2.EpisodeId, 0);
				}
			}
			return null;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x001A5911 File Offset: 0x001A3B11
		public static void SetLoadingProgress(float progress)
		{
			NKCUIManager.LoadingUI.SetLoadingProgress(progress);
			if (NKCLoadingScreenManager.s_UIPhaseTransition != null)
			{
				NKCLoadingScreenManager.s_UIPhaseTransition.SetLoadingProgress(progress);
			}
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x001A5936 File Offset: 0x001A3B36
		public static void Update()
		{
			if (NKCLoadingScreenManager.s_UIPhaseTransition != null && NKCLoadingScreenManager.s_UIPhaseTransition.IsAnimFinished())
			{
				NKCLoadingScreenManager.AdvanceIntroState();
			}
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x001A5958 File Offset: 0x001A3B58
		private static void AdvanceIntroState()
		{
			switch (NKCLoadingScreenManager.s_eState)
			{
			case NKCLoadingScreenManager.eIntroState.Outro:
				if (NKCLoadingScreenManager.s_UIPhaseTransition != null)
				{
					NKCLoadingScreenManager.s_UIPhaseTransition.PlayIdle();
				}
				NKCLoadingScreenManager.s_eState = NKCLoadingScreenManager.eIntroState.LoadingLoop;
				return;
			case NKCLoadingScreenManager.eIntroState.LoadingLoop:
				if (NKCUICutScenPlayer.IsInstanceOpen)
				{
					if (NKCLoadingScreenManager.s_UIPhaseTransition != null)
					{
						NKCLoadingScreenManager.s_UIPhaseTransition.PlayIntro();
					}
					NKCLoadingScreenManager.s_eState = NKCLoadingScreenManager.eIntroState.Intro_Prefired;
				}
				return;
			case NKCLoadingScreenManager.eIntroState.Intro_Prefired:
				NKCUtil.SetGameobjectActive(NKCLoadingScreenManager.s_instanceIntro.m_Instant, false);
				return;
			case NKCLoadingScreenManager.eIntroState.Intro:
				NKCLoadingScreenManager.CleanupIntroObject();
				return;
			default:
				return;
			}
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x001A59E0 File Offset: 0x001A3BE0
		public static void PlayDungeonIntro(NKMGameData gameData)
		{
			NKMAssetName introName = NKCLoadingScreenManager.GetIntroName(gameData);
			if (introName == null)
			{
				NKCLoadingScreenManager.CleanupIntroObject();
				return;
			}
			if (NKCLoadingScreenManager.s_eState == NKCLoadingScreenManager.eIntroState.Intro_Prefired)
			{
				NKCLoadingScreenManager.CleanupIntroObject();
				return;
			}
			NKCLoadingScreenManager.MakeIntroObject(introName);
			if (NKCLoadingScreenManager.s_UIPhaseTransition != null)
			{
				NKCLoadingScreenManager.s_UIPhaseTransition.PlayIntro();
			}
			NKCLoadingScreenManager.s_eState = NKCLoadingScreenManager.eIntroState.Intro;
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x001A5A30 File Offset: 0x001A3C30
		public static void PlayDungeonOutro(NKMGameData gameData)
		{
			NKMAssetName outroName = NKCLoadingScreenManager.GetOutroName(gameData);
			if (outroName == null)
			{
				NKCLoadingScreenManager.CleanupIntroObject();
				return;
			}
			NKCLoadingScreenManager.MakeIntroObject(outroName);
			if (NKCLoadingScreenManager.s_UIPhaseTransition != null)
			{
				NKCLoadingScreenManager.s_UIPhaseTransition.PlayOutro();
			}
			NKCLoadingScreenManager.s_eState = NKCLoadingScreenManager.eIntroState.Outro;
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x001A5A71 File Offset: 0x001A3C71
		public static void CleanupIntroObject()
		{
			if (NKCLoadingScreenManager.s_instanceIntro != null)
			{
				NKCLoadingScreenManager.s_instanceIntro.Close();
			}
			NKCLoadingScreenManager.s_instanceIntro = null;
			NKCLoadingScreenManager.s_UIPhaseTransition = null;
			NKCLoadingScreenManager.s_eState = NKCLoadingScreenManager.eIntroState.None;
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x001A5A98 File Offset: 0x001A3C98
		private static NKCUILoadingPhaseTransition MakeIntroObject(NKMAssetName assetName)
		{
			if (NKCLoadingScreenManager.s_instanceIntro != null && NKCLoadingScreenManager.s_instanceIntro.m_Instant != null && NKCLoadingScreenManager.s_instanceIntro.m_BundleName == assetName.m_BundleName && NKCLoadingScreenManager.s_instanceIntro.m_AssetName == assetName.m_AssetName)
			{
				return NKCLoadingScreenManager.s_UIPhaseTransition;
			}
			NKCLoadingScreenManager.CleanupIntroObject();
			NKCLoadingScreenManager.s_instanceIntro = NKCAssetResourceManager.OpenInstance<GameObject>(assetName, false, NKCScenManager.GetScenManager().Get_NUF_AFTER_UI_EFFECT());
			if (NKCLoadingScreenManager.s_instanceIntro != null && NKCLoadingScreenManager.s_instanceIntro.m_Instant)
			{
				NKCLoadingScreenManager.s_UIPhaseTransition = NKCLoadingScreenManager.s_instanceIntro.m_Instant.GetComponent<NKCUILoadingPhaseTransition>();
			}
			return NKCLoadingScreenManager.s_UIPhaseTransition;
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001A5B40 File Offset: 0x001A3D40
		public static NKMAssetName GetIntroName(NKMGameData gameData)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameData.m_DungeonID);
			if (dungeonTempletBase != null && !string.IsNullOrEmpty(dungeonTempletBase.m_Intro))
			{
				return NKMAssetName.ParseBundleName(dungeonTempletBase.m_Intro, dungeonTempletBase.m_Intro);
			}
			if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PHASE && !NKCPhaseManager.IsFirstStage(gameData.m_DungeonID))
			{
				NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
				if (phaseTemplet != null && !string.IsNullOrEmpty(phaseTemplet.m_Intro))
				{
					return NKMAssetName.ParseBundleName(phaseTemplet.m_Intro, phaseTemplet.m_Intro);
				}
			}
			return null;
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001A5BBC File Offset: 0x001A3DBC
		public static NKMAssetName GetOutroName(NKMGameData gameData)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameData.m_DungeonID);
			if (dungeonTempletBase != null && !string.IsNullOrEmpty(dungeonTempletBase.m_Outro))
			{
				return NKMAssetName.ParseBundleName(dungeonTempletBase.m_Outro, dungeonTempletBase.m_Outro);
			}
			if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PHASE && !NKCPhaseManager.IsLastStage(gameData.m_DungeonID))
			{
				NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
				if (phaseTemplet != null && !string.IsNullOrEmpty(phaseTemplet.m_Outro))
				{
					return NKMAssetName.ParseBundleName(phaseTemplet.m_Outro, phaseTemplet.m_Outro);
				}
			}
			return null;
		}

		// Token: 0x04004567 RID: 17767
		private const int DEFAULT_KEY = 9;

		// Token: 0x04004568 RID: 17768
		private static List<NKCLoadingScreenManager.NKCLoadingDescTemplet> s_lstLoadingDescTemplet = new List<NKCLoadingScreenManager.NKCLoadingDescTemplet>();

		// Token: 0x04004569 RID: 17769
		private static NKCAssetInstanceData s_instanceIntro;

		// Token: 0x0400456A RID: 17770
		private static NKCUILoadingPhaseTransition s_UIPhaseTransition;

		// Token: 0x0400456B RID: 17771
		private static NKCLoadingScreenManager.eIntroState s_eState = NKCLoadingScreenManager.eIntroState.None;

		// Token: 0x0200157C RID: 5500
		public enum eGameContentsType
		{
			// Token: 0x0400A13F RID: 41279
			NONE,
			// Token: 0x0400A140 RID: 41280
			WARFARE,
			// Token: 0x0400A141 RID: 41281
			DUNGEON,
			// Token: 0x0400A142 RID: 41282
			DIVE,
			// Token: 0x0400A143 RID: 41283
			FIERCE,
			// Token: 0x0400A144 RID: 41284
			GAUNTLET,
			// Token: 0x0400A145 RID: 41285
			RAID,
			// Token: 0x0400A146 RID: 41286
			SHADOW_PALACE,
			// Token: 0x0400A147 RID: 41287
			EPISODE,
			// Token: 0x0400A148 RID: 41288
			DEFAULT,
			// Token: 0x0400A149 RID: 41289
			TRIM,
			// Token: 0x0400A14A RID: 41290
			COUNT
		}

		// Token: 0x0200157D RID: 5501
		public class NKCLoadingScreenTemplet : INKMTemplet
		{
			// Token: 0x170018DE RID: 6366
			// (get) Token: 0x0600AD53 RID: 44371 RVA: 0x00358E45 File Offset: 0x00357045
			public int Key { get; }

			// Token: 0x0600AD54 RID: 44372 RVA: 0x00358E4D File Offset: 0x0035704D
			public NKCLoadingScreenTemplet(int id, IEnumerable<NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData> lstData)
			{
				this.Key = id;
				this.m_lstLoadingScreenData = new List<NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData>(lstData);
			}

			// Token: 0x0600AD55 RID: 44373 RVA: 0x00358E74 File Offset: 0x00357074
			public NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData GetRandomData()
			{
				List<int> list = new List<int>();
				for (int i = 0; i < this.m_lstLoadingScreenData.Count; i++)
				{
					NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData loadingScreenData = this.m_lstLoadingScreenData[i];
					if (loadingScreenData.EnableByTag)
					{
						if (loadingScreenData.m_bUnlockCache)
						{
							list.Add(i);
						}
						else if (loadingScreenData.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED)
						{
							list.Add(i);
						}
						else if (NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), loadingScreenData.m_UnlockInfo, false))
						{
							loadingScreenData.m_bUnlockCache = true;
							list.Add(i);
						}
					}
				}
				int index = list[NKMRandom.Range(0, list.Count)];
				return this.m_lstLoadingScreenData[index];
			}

			// Token: 0x0600AD56 RID: 44374 RVA: 0x00358F1C File Offset: 0x0035711C
			public void ResetUnlockCache()
			{
				foreach (NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData loadingScreenData in this.m_lstLoadingScreenData)
				{
					loadingScreenData.m_bUnlockCache = false;
				}
			}

			// Token: 0x0600AD57 RID: 44375 RVA: 0x00358F70 File Offset: 0x00357170
			public void Join()
			{
			}

			// Token: 0x0600AD58 RID: 44376 RVA: 0x00358F74 File Offset: 0x00357174
			public void Validate()
			{
				if (this.m_lstLoadingScreenData.Count == 0)
				{
					Debug.LogError(this.Key.ToString() + " : Loading Screen Data Length 0");
				}
				bool flag = false;
				foreach (NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData loadingScreenData in this.m_lstLoadingScreenData)
				{
					if (loadingScreenData.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED)
					{
						flag = true;
					}
					if (NKMTempletContainer<NKCLoadingScreenManager.NKCLoadingImgTemplet>.Find(loadingScreenData.m_ImgID) == null)
					{
						Debug.LogError(string.Format("{0} {1} : ImageID {2} Not exist!", loadingScreenData.m_eContentType, loadingScreenData.m_ContentValue, loadingScreenData.m_ImgID));
					}
				}
				if (!flag)
				{
					Debug.LogError(this.Key.ToString() + " : Every Loading Screen Data has unlock condition, potential error");
				}
			}

			// Token: 0x0400A14C RID: 41292
			private List<NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData> m_lstLoadingScreenData = new List<NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData>();

			// Token: 0x02001A6C RID: 6764
			public class LoadingScreenData : INKMTemplet
			{
				// Token: 0x17001A14 RID: 6676
				// (get) Token: 0x0600BBE2 RID: 48098 RVA: 0x0036FA88 File Offset: 0x0036DC88
				public int Key
				{
					get
					{
						return NKCLoadingScreenManager.BuildKey(this.m_eContentType, this.m_ContentValue);
					}
				}

				// Token: 0x17001A15 RID: 6677
				// (get) Token: 0x0600BBE3 RID: 48099 RVA: 0x0036FA9B File Offset: 0x0036DC9B
				public bool EnableByTag
				{
					get
					{
						return NKMOpenTagManager.IsOpened(this.m_OpenTag);
					}
				}

				// Token: 0x0600BBE4 RID: 48100 RVA: 0x0036FAA8 File Offset: 0x0036DCA8
				public static NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData LoadFromLua(NKMLua cNKMLua)
				{
					if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCLoadingScreenManager.cs", 71))
					{
						return null;
					}
					NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData loadingScreenData = new NKCLoadingScreenManager.NKCLoadingScreenTemplet.LoadingScreenData();
					bool flag = true & cNKMLua.GetData<NKCLoadingScreenManager.eGameContentsType>("m_eContentType", ref loadingScreenData.m_eContentType);
					cNKMLua.GetData("m_ContentValue", ref loadingScreenData.m_ContentValue);
					bool flag2 = flag & cNKMLua.GetData("m_ImgID", ref loadingScreenData.m_ImgID);
					cNKMLua.GetData("m_DescStrID", ref loadingScreenData.m_DescStrID);
					cNKMLua.GetData("m_OpenTag", ref loadingScreenData.m_OpenTag);
					loadingScreenData.m_UnlockInfo = UnlockInfo.LoadFromLua(cNKMLua, true);
					if (!flag2)
					{
						return null;
					}
					return loadingScreenData;
				}

				// Token: 0x0600BBE5 RID: 48101 RVA: 0x0036FB39 File Offset: 0x0036DD39
				public void Join()
				{
				}

				// Token: 0x0600BBE6 RID: 48102 RVA: 0x0036FB3B File Offset: 0x0036DD3B
				public void Validate()
				{
				}

				// Token: 0x0400AE79 RID: 44665
				public NKCLoadingScreenManager.eGameContentsType m_eContentType;

				// Token: 0x0400AE7A RID: 44666
				public int m_ContentValue;

				// Token: 0x0400AE7B RID: 44667
				public UnlockInfo m_UnlockInfo;

				// Token: 0x0400AE7C RID: 44668
				public int m_ImgID;

				// Token: 0x0400AE7D RID: 44669
				public string m_DescStrID;

				// Token: 0x0400AE7E RID: 44670
				public bool m_bUnlockCache;

				// Token: 0x0400AE7F RID: 44671
				private string m_OpenTag;
			}
		}

		// Token: 0x0200157E RID: 5502
		public class NKCLoadingDescTemplet
		{
			// Token: 0x0600AD59 RID: 44377 RVA: 0x0035905C File Offset: 0x0035725C
			public static NKCLoadingScreenManager.NKCLoadingDescTemplet LoadFromLUA(NKMLua cNKMLua)
			{
				if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCLoadingScreenManager.cs", 185))
				{
					return null;
				}
				NKCLoadingScreenManager.NKCLoadingDescTemplet nkcloadingDescTemplet = new NKCLoadingScreenManager.NKCLoadingDescTemplet();
				bool flag = true & cNKMLua.GetData("m_StrTooltipDesc", ref nkcloadingDescTemplet.m_StrTooltipDesc);
				nkcloadingDescTemplet.m_UnlockInfo = UnlockInfo.LoadFromLua(cNKMLua, true);
				if (!flag)
				{
					return null;
				}
				return nkcloadingDescTemplet;
			}

			// Token: 0x0400A14D RID: 41293
			public string m_StrTooltipDesc;

			// Token: 0x0400A14E RID: 41294
			public UnlockInfo m_UnlockInfo;

			// Token: 0x0400A14F RID: 41295
			public bool m_bUnlockCache;
		}

		// Token: 0x0200157F RID: 5503
		public class NKCLoadingImgTemplet : INKMTemplet
		{
			// Token: 0x170018DF RID: 6367
			// (get) Token: 0x0600AD5B RID: 44379 RVA: 0x003590B0 File Offset: 0x003572B0
			public int Key
			{
				get
				{
					return this.m_imgID;
				}
			}

			// Token: 0x0600AD5C RID: 44380 RVA: 0x003590B8 File Offset: 0x003572B8
			public static NKCLoadingScreenManager.NKCLoadingImgTemplet LoadFromLua(NKMLua cNKMLua)
			{
				if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCLoadingScreenManager.cs", 213))
				{
					return null;
				}
				NKCLoadingScreenManager.NKCLoadingImgTemplet nkcloadingImgTemplet = new NKCLoadingScreenManager.NKCLoadingImgTemplet();
				if (!(true & cNKMLua.GetData("m_imgID", ref nkcloadingImgTemplet.m_imgID) & cNKMLua.GetData<NKCLoadingScreenManager.NKCLoadingImgTemplet.eImgType>("m_eImgType", ref nkcloadingImgTemplet.m_eImgType) & cNKMLua.GetData("m_ImgAssetName", ref nkcloadingImgTemplet.m_ImgAssetName)))
				{
					return null;
				}
				return nkcloadingImgTemplet;
			}

			// Token: 0x0600AD5D RID: 44381 RVA: 0x0035911B File Offset: 0x0035731B
			public void Join()
			{
			}

			// Token: 0x0600AD5E RID: 44382 RVA: 0x0035911D File Offset: 0x0035731D
			public void Validate()
			{
				if (string.IsNullOrEmpty(this.m_ImgAssetName))
				{
					Debug.LogError(this.m_imgID.ToString() + " : m_ImgAssetName null!");
				}
			}

			// Token: 0x0400A150 RID: 41296
			public int m_imgID;

			// Token: 0x0400A151 RID: 41297
			public NKCLoadingScreenManager.NKCLoadingImgTemplet.eImgType m_eImgType;

			// Token: 0x0400A152 RID: 41298
			public string m_ImgAssetName;

			// Token: 0x02001A6D RID: 6765
			public enum eImgType
			{
				// Token: 0x0400AE81 RID: 44673
				FULL,
				// Token: 0x0400AE82 RID: 44674
				CARTOON
			}
		}

		// Token: 0x02001580 RID: 5504
		private enum eIntroState
		{
			// Token: 0x0400A154 RID: 41300
			None,
			// Token: 0x0400A155 RID: 41301
			Outro,
			// Token: 0x0400A156 RID: 41302
			LoadingLoop,
			// Token: 0x0400A157 RID: 41303
			Intro_Prefired,
			// Token: 0x0400A158 RID: 41304
			Intro
		}
	}
}
