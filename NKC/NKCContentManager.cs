using System;
using System.Collections.Generic;
using System.Text;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Collection;
using NKC.UI.Event;
using NKC.UI.Friend;
using NKC.UI.Gauntlet;
using NKC.UI.Guide;
using NKC.UI.Guild;
using NKC.UI.Module;
using NKC.UI.Office;
using NKC.UI.Shop;
using NKC.UI.Trim;
using NKM;
using NKM.Event;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x0200065A RID: 1626
	public static class NKCContentManager
	{
		// Token: 0x06003305 RID: 13061 RVA: 0x000FD780 File Offset: 0x000FB980
		public static void AddUnlockableContents()
		{
			NKCContentManager.m_dicUnlockableContents.Clear();
			foreach (NKMContentUnlockTemplet templet in NKMTempletContainer<NKMContentUnlockTemplet>.Values)
			{
				NKCContentManager.NKCUnlockableContent nkcunlockableContent = new NKCContentManager.NKCUnlockableContent(templet);
				if (!NKCContentManager.m_dicUnlockableContents.ContainsKey(nkcunlockableContent.m_Code))
				{
					if (nkcunlockableContent.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED)
					{
						NKCContentManager.m_dicUnlockableContents.Add(nkcunlockableContent.m_Code, nkcunlockableContent);
					}
					else if (nkcunlockableContent.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_UNLOCK_STAGE)
					{
						NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(nkcunlockableContent.m_ContentsValue);
						if (nkmstageTempletV != null)
						{
							if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV.m_UnlockInfo, false))
							{
								NKCContentManager.m_dicUnlockableContents.Add(nkcunlockableContent.m_Code, nkcunlockableContent);
							}
							else
							{
								NKCContentManager.m_hsUnlockCompletedContents.Add(nkcunlockableContent.m_Code);
							}
						}
					}
					else if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkcunlockableContent.m_UnlockInfo, false))
					{
						NKCContentManager.m_dicUnlockableContents.Add(nkcunlockableContent.m_Code, nkcunlockableContent);
					}
					else
					{
						NKCContentManager.m_hsUnlockCompletedContents.Add(nkcunlockableContent.m_Code);
					}
				}
				else
				{
					Debug.LogWarning(string.Format("{0} 중복", nkcunlockableContent.m_eContentsType));
				}
			}
			foreach (ShopItemTemplet shopItemTemplet in NKCShopManager.GetLockedProductList())
			{
				if (shopItemTemplet.m_bUnlockBanner || shopItemTemplet.IsInstantProduct)
				{
					NKCContentManager.NKCUnlockableContent nkcunlockableContent2 = new NKCContentManager.NKCUnlockableContent(shopItemTemplet);
					if (!NKCContentManager.m_dicUnlockableContents.ContainsKey(nkcunlockableContent2.m_Code))
					{
						NKCContentManager.m_dicUnlockableContents.Add(nkcunlockableContent2.m_Code, nkcunlockableContent2);
					}
					else
					{
						Debug.LogWarning(string.Format("{0} 중복", nkcunlockableContent2.m_eContentsType));
					}
				}
			}
			NKCContentManager.NKCUnlockableContent nkcunlockableContent3 = new NKCContentManager.NKCUnlockableContent(ContentsType.ALARM_ONLY, NKMPvpCommonConst.Instance.RankUnlockInfo.reqValue, NKMPvpCommonConst.Instance.RankUnlockInfo, NKMPvpCommonConst.Instance.RankUnlockPopupTitle, NKMPvpCommonConst.Instance.RankUnlockPopupDesc, NKMPvpCommonConst.Instance.RankUnlockPopupImageName);
			if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkcunlockableContent3.m_UnlockInfo, false))
			{
				if (!NKCContentManager.m_dicUnlockableContents.ContainsKey(nkcunlockableContent3.m_Code))
				{
					NKCContentManager.m_dicUnlockableContents.Add(nkcunlockableContent3.m_Code, nkcunlockableContent3);
				}
				else
				{
					Debug.LogWarning("랭크전 언락정보 중복");
				}
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
			{
				NKCContentManager.NKCUnlockableContent nkcunlockableContent4 = new NKCContentManager.NKCUnlockableContent(ContentsType.ALARM_ONLY, NKMPvpCommonConst.Instance.LeagueUnlockInfo.reqValue, NKMPvpCommonConst.Instance.LeagueUnlockInfo, NKMPvpCommonConst.Instance.LeagueUnlockPopupTitle, NKMPvpCommonConst.Instance.LeagueUnlockPopupDesc, NKMPvpCommonConst.Instance.LeagueUnlockPopupImageName);
				if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkcunlockableContent4.m_UnlockInfo, false))
				{
					if (!NKCContentManager.m_dicUnlockableContents.ContainsKey(nkcunlockableContent4.m_Code))
					{
						NKCContentManager.m_dicUnlockableContents.Add(nkcunlockableContent4.m_Code, nkcunlockableContent4);
						return;
					}
					Debug.LogWarning("리그전 언락정보 중복");
				}
			}
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000FDA88 File Offset: 0x000FBC88
		public static bool IsUnlockableContents(ContentsType contentsType, int contentsValue)
		{
			int key = NKCContentManager.NKCUnlockableContent.Encode(contentsType, contentsValue);
			return NKCContentManager.m_dicUnlockableContents.ContainsKey(key);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000FDAB0 File Offset: 0x000FBCB0
		public static bool IsContentAlwaysLocked(ContentsType contentsType, int contentsValue)
		{
			int key = NKCContentManager.NKCUnlockableContent.Encode(contentsType, contentsValue);
			NKCContentManager.NKCUnlockableContent nkcunlockableContent;
			return NKCContentManager.m_dicUnlockableContents.TryGetValue(key, out nkcunlockableContent) && nkcunlockableContent.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000FDAE8 File Offset: 0x000FBCE8
		public static NKCContentManager.eContentStatus CheckContentStatus(ContentsType contentsType, out bool bAdmin, int contentsValue = 0, int contentsValue2 = 0)
		{
			bAdmin = false;
			if (NKCScenManager.CurrentUserData() == null)
			{
				if (contentsType - ContentsType.BATTLE_AUTO_RESPAWN <= 2)
				{
					return NKCContentManager.eContentStatus.Open;
				}
				return NKCContentManager.eContentStatus.Hide;
			}
			else
			{
				if (contentsType == ContentsType.None)
				{
					return NKCContentManager.eContentStatus.Open;
				}
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int num = NKCContentManager.NKCUnlockableContent.Encode(contentsType, contentsValue);
				NKCContentManager.eContentStatus eContentStatus = NKCContentManager.eContentStatus.Hide;
				if (NKCContentManager.m_dicUnlockableContents.ContainsKey(num))
				{
					NKCContentManager.NKCUnlockableContent nkcunlockableContent = NKCContentManager.m_dicUnlockableContents[num];
					STAGE_UNLOCK_REQ_TYPE eReqType = nkcunlockableContent.m_UnlockInfo.eReqType;
					if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED)
					{
						if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_HIDDEN)
						{
							eContentStatus = (NKMContentUnlockManager.IsContentUnlocked(nkmuserData, nkcunlockableContent.m_UnlockInfo, out bAdmin) ? NKCContentManager.eContentStatus.Open : NKCContentManager.eContentStatus.Lock);
						}
						else
						{
							eContentStatus = NKCContentManager.eContentStatus.Hide;
						}
					}
					else
					{
						eContentStatus = NKCContentManager.eContentStatus.Lock;
					}
				}
				else if (NKCContentManager.m_hsUnlockCompletedContents.Contains(num))
				{
					eContentStatus = NKCContentManager.eContentStatus.Open;
				}
				if (eContentStatus != NKCContentManager.eContentStatus.Open && nkmuserData != null && nkmuserData.IsSuperUser())
				{
					bAdmin = true;
					return NKCContentManager.eContentStatus.Open;
				}
				return eContentStatus;
			}
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000FDB98 File Offset: 0x000FBD98
		public static bool IsContentsUnlocked(ContentsType contentsType, int contentsValue = 0, int contentsValue2 = 0)
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return contentsType - ContentsType.BATTLE_AUTO_RESPAWN <= 2;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.IsSuperUser())
			{
				return true;
			}
			if (contentsType == ContentsType.None)
			{
				return true;
			}
			int key = NKCContentManager.NKCUnlockableContent.Encode(contentsType, contentsValue);
			if (NKCContentManager.m_dicUnlockableContents.ContainsKey(key))
			{
				NKCContentManager.NKCUnlockableContent nkcunlockableContent = NKCContentManager.m_dicUnlockableContents[key];
				return nkcunlockableContent.m_UnlockInfo.eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkcunlockableContent.m_UnlockInfo, false);
			}
			return true;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000FDC18 File Offset: 0x000FBE18
		public static NKMStageTempletV2 GetFirstStageTemplet(NKMEpisodeTempletV2 episodeTemplet, int actID, EPISODE_DIFFICULTY difficulty)
		{
			if (episodeTemplet != null && episodeTemplet.m_DicStage.Count > actID && episodeTemplet.m_DicStage[actID].Count > 0)
			{
				NKMStageTempletV2 nkmstageTempletV = episodeTemplet.m_DicStage[actID][0];
				if (nkmstageTempletV != null)
				{
					return nkmstageTempletV;
				}
			}
			return null;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000FDC64 File Offset: 0x000FBE64
		public static int GetFirstStageID(NKMEpisodeTempletV2 episodeTemplet, int actID, EPISODE_DIFFICULTY difficulty)
		{
			NKMStageTempletV2 firstStageTemplet = NKCContentManager.GetFirstStageTemplet(episodeTemplet, actID, difficulty);
			if (firstStageTemplet != null)
			{
				return firstStageTemplet.Key;
			}
			return -1;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000FDC88 File Offset: 0x000FBE88
		public static void RemoveUnlockedContent(ContentsType contentsType, int contentsValue = 0, bool bRemoveKey = true)
		{
			int key = NKCContentManager.NKCUnlockableContent.Encode(contentsType, contentsValue);
			if (NKCContentManager.m_dicUnlockableContents.ContainsKey(key))
			{
				NKCContentManager.m_dicUnlockableContents.Remove(key);
			}
			if (bRemoveKey)
			{
				string preferenceString = NKCContentManager.GetPreferenceString(contentsType, contentsValue);
				if (PlayerPrefs.HasKey(preferenceString))
				{
					PlayerPrefs.DeleteKey(preferenceString);
				}
			}
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000FDCD0 File Offset: 0x000FBED0
		public static bool IsStageUnlocked(ContentsType contentsType, int contentsValue)
		{
			if (contentsType != ContentsType.EPISODE && contentsType != ContentsType.ACT)
			{
				return false;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(contentsValue);
			return nkmstageTempletV != null && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV.m_UnlockInfo, false);
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000FDD04 File Offset: 0x000FBF04
		public static string GetLockedMessage(ContentsType contentsType, int contentsValue = 0)
		{
			int key = NKCContentManager.NKCUnlockableContent.Encode(contentsType, contentsValue);
			if (!NKCContentManager.m_dicUnlockableContents.ContainsKey(key))
			{
				return string.Empty;
			}
			NKCContentManager.NKCUnlockableContent nkcunlockableContent = NKCContentManager.m_dicUnlockableContents[key];
			if (string.IsNullOrEmpty(nkcunlockableContent.m_LockedText))
			{
				return NKCContentManager.MakeUnlockConditionString(nkcunlockableContent.m_UnlockInfo, false);
			}
			return NKCStringTable.GetString(NKCContentManager.m_dicUnlockableContents[key].m_LockedText, false);
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000FDD68 File Offset: 0x000FBF68
		public static bool ShowLockedMessagePopup(ContentsType contentsType, int contentsValue = 0)
		{
			string lockedMessage = NKCContentManager.GetLockedMessage(contentsType, contentsValue);
			if (!string.IsNullOrEmpty(lockedMessage))
			{
				NKCPopupMessageManager.AddPopupMessage(lockedMessage, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000FDD98 File Offset: 0x000FBF98
		public static void AddUnlockableCounterCase()
		{
			NKCContentManager.m_dicLockedCounterCaseStageTemplet.Clear();
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(50, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in nkmepisodeTempletV.m_DicStage)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						NKMStageTempletV2 nkmstageTempletV = keyValuePair.Value[i];
						if (!NKCContentManager.IsStageUnlocked(ContentsType.EPISODE, nkmstageTempletV.Key) && !NKMEpisodeMgr.CheckClear(NKCScenManager.CurrentUserData(), nkmstageTempletV) && !NKCContentManager.m_dicLockedCounterCaseStageTemplet.ContainsKey(nkmstageTempletV.Key))
						{
							NKCContentManager.m_dicLockedCounterCaseStageTemplet.Add(nkmstageTempletV.Key, nkmstageTempletV);
						}
					}
				}
			}
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000FDE68 File Offset: 0x000FC068
		public static bool SetUnlockedCounterCaseKey()
		{
			bool result = false;
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(50, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in nkmepisodeTempletV.m_DicStage)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						NKMStageTempletV2 nkmstageTempletV = keyValuePair.Value[i];
						if (!NKCContentManager.IsStageUnlocked(ContentsType.EPISODE, nkmstageTempletV.Key))
						{
							break;
						}
						if (!NKMEpisodeMgr.CheckClear(NKCScenManager.CurrentUserData(), nkmstageTempletV) && NKCContentManager.m_dicLockedCounterCaseStageTemplet.ContainsKey(nkmstageTempletV.Key) && !PlayerPrefs.HasKey(NKCContentManager.GetCounterCaseNormalKey(nkmstageTempletV.ActId)))
						{
							PlayerPrefs.SetInt(NKCContentManager.GetCounterCaseNormalKey(nkmstageTempletV.ActId), nkmstageTempletV.ActId);
							NKCContentManager.m_dicLockedCounterCaseStageTemplet.Remove(nkmstageTempletV.Key);
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000FDF6C File Offset: 0x000FC16C
		public static bool CheckNewCounterCase(NKMEpisodeTempletV2 episodeTemplet)
		{
			if (episodeTemplet == null)
			{
				return false;
			}
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in episodeTemplet.m_DicStage)
			{
				if (PlayerPrefs.HasKey(NKCContentManager.GetCounterCaseNormalKey(keyValuePair.Key)))
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						if (keyValuePair.Value[i].EnableByTag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000FE004 File Offset: 0x000FC204
		public static void RemoveUnlockedCounterCaseKey(int actID)
		{
			if (PlayerPrefs.HasKey(NKCContentManager.GetCounterCaseNormalKey(actID)))
			{
				PlayerPrefs.DeleteKey(NKCContentManager.GetCounterCaseNormalKey(actID));
			}
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000FE01E File Offset: 0x000FC21E
		public static string GetCounterCaseNormalKey(int actID)
		{
			return string.Format("NewCC_{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, actID);
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000FE040 File Offset: 0x000FC240
		public static bool HasUnlockedContent(params STAGE_UNLOCK_REQ_TYPE[] aUnlockReq)
		{
			if (aUnlockReq == null || aUnlockReq.Length == 0)
			{
				return NKCContentManager.m_dicUnlockedContent != null && NKCContentManager.m_dicUnlockedContent.Count > 0;
			}
			HashSet<STAGE_UNLOCK_REQ_TYPE> hashSet = new HashSet<STAGE_UNLOCK_REQ_TYPE>(aUnlockReq);
			foreach (KeyValuePair<int, NKCContentManager.NKCUnlockableContent> keyValuePair in NKCContentManager.m_dicUnlockedContent)
			{
				if (hashSet.Contains(keyValuePair.Value.m_UnlockInfo.eReqType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000FE0D0 File Offset: 0x000FC2D0
		public static bool UnlockEffectRequired(ContentsType contentsType, int contentsValue = 0)
		{
			return PlayerPrefs.HasKey(NKCContentManager.GetPreferenceString(contentsType, contentsValue));
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000FE0DE File Offset: 0x000FC2DE
		public static string GetPreferenceString(ContentsType contentsType, int contentsValue = 0)
		{
			if (NKCScenManager.CurrentUserData() != null)
			{
				return string.Format("{0}_{1}_{2}", NKCScenManager.CurrentUserData().m_UserUID, contentsType, contentsValue);
			}
			return string.Empty;
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000FE112 File Offset: 0x000FC312
		public static void SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE eReqType = STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED, int reqValue = -1)
		{
			if (eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE)
			{
				NKCContentManager.OnWarfareClear(reqValue);
				return;
			}
			if (eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON)
			{
				NKCContentManager.OnDungeonClear(reqValue);
				return;
			}
			if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE)
			{
				NKCContentManager.OnContentUnlock(eReqType);
				return;
			}
			NKCContentManager.OnPhaseClear(reqValue);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000FE140 File Offset: 0x000FC340
		private static bool UnlockPopupProcessRequired(NKCContentManager.NKCUnlockableContent content)
		{
			if (content == null)
			{
				return false;
			}
			ContentsType eContentsType = content.m_eContentsType;
			if (eContentsType == ContentsType.MARKET_REVIEW_REQUEST)
			{
				return true;
			}
			if (eContentsType == ContentsType.SHOP_ITEM_POPUP)
			{
				NKCShopManager.SetReserveRefreshShop();
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(content.m_ContentsValue);
				bool flag;
				long num;
				return shopItemTemplet != null && shopItemTemplet.m_bUnlockBanner && NKCShopManager.CanBuyFixShop(NKCScenManager.CurrentUserData(), shopItemTemplet, out flag, out num, true) == NKM_ERROR_CODE.NEC_OK;
			}
			return !string.IsNullOrEmpty(content.m_PopupImageName);
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000FE1AC File Offset: 0x000FC3AC
		private static void OnContentUnlock(STAGE_UNLOCK_REQ_TYPE eReqType)
		{
			new List<int>();
			foreach (KeyValuePair<int, NKCContentManager.NKCUnlockableContent> keyValuePair in NKCContentManager.m_dicUnlockableContents)
			{
				if ((eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED || keyValuePair.Value.m_UnlockInfo.eReqType == eReqType) && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), keyValuePair.Value.m_UnlockInfo, false))
				{
					NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000FE23C File Offset: 0x000FC43C
		private static void OnDungeonClear(int warfareID)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(warfareID);
			if (dungeonTempletBase != null)
			{
				foreach (KeyValuePair<int, NKCContentManager.NKCUnlockableContent> keyValuePair in NKCContentManager.m_dicUnlockableContents)
				{
					if (keyValuePair.Value.m_UnlockInfo.eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED)
					{
						if (keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON && keyValuePair.Value.m_UnlockInfo.reqValue == warfareID)
						{
							NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
						}
						else if (keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE)
						{
							NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(keyValuePair.Value.m_UnlockInfo.reqValue);
							if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_DUNGEON && nkmstageTempletV.m_StageBattleStrID == dungeonTempletBase.m_DungeonStrID)
							{
								NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
							}
						}
						else if ((keyValuePair.Value.m_eContentsType == ContentsType.EPISODE || keyValuePair.Value.m_eContentsType == ContentsType.ACT || keyValuePair.Value.m_eContentsType == ContentsType.DUNGEON) && keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_UNLOCK_STAGE)
						{
							NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(keyValuePair.Value.m_ContentsValue);
							if (nkmstageTempletV2 != null && nkmstageTempletV2.m_UnlockInfo.reqValue == warfareID && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV2.m_UnlockInfo, false))
							{
								NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000FE3E0 File Offset: 0x000FC5E0
		private static void OnWarfareClear(int warfareID)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareID);
			if (nkmwarfareTemplet != null)
			{
				foreach (KeyValuePair<int, NKCContentManager.NKCUnlockableContent> keyValuePair in NKCContentManager.m_dicUnlockableContents)
				{
					if (keyValuePair.Value.m_UnlockInfo.eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED)
					{
						if (keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE && keyValuePair.Value.m_UnlockInfo.reqValue == warfareID)
						{
							NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
						}
						else if (keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE)
						{
							NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(keyValuePair.Value.m_UnlockInfo.reqValue);
							if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_WARFARE && nkmstageTempletV.m_StageBattleStrID == nkmwarfareTemplet.m_WarfareStrID)
							{
								NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
							}
						}
						else if ((keyValuePair.Value.m_eContentsType == ContentsType.EPISODE || keyValuePair.Value.m_eContentsType == ContentsType.ACT || keyValuePair.Value.m_eContentsType == ContentsType.DUNGEON) && keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_UNLOCK_STAGE)
						{
							NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(keyValuePair.Value.m_ContentsValue);
							if (nkmstageTempletV2 != null && nkmstageTempletV2.m_UnlockInfo.reqValue == warfareID && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV2.m_UnlockInfo, false))
							{
								NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000FE584 File Offset: 0x000FC784
		private static void OnPhaseClear(int phaseID)
		{
			NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(phaseID);
			if (nkmphaseTemplet != null)
			{
				foreach (KeyValuePair<int, NKCContentManager.NKCUnlockableContent> keyValuePair in NKCContentManager.m_dicUnlockableContents)
				{
					if (keyValuePair.Value.m_UnlockInfo.eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED)
					{
						if (keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE && keyValuePair.Value.m_UnlockInfo.reqValue == phaseID)
						{
							NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
						}
						else if (keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE)
						{
							NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(keyValuePair.Value.m_UnlockInfo.reqValue);
							if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE && nkmstageTempletV.m_StageBattleStrID == nkmphaseTemplet.StrId)
							{
								NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
							}
						}
						else if ((keyValuePair.Value.m_eContentsType == ContentsType.EPISODE || keyValuePair.Value.m_eContentsType == ContentsType.ACT || keyValuePair.Value.m_eContentsType == ContentsType.DUNGEON) && keyValuePair.Value.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_UNLOCK_STAGE)
						{
							NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(keyValuePair.Value.m_ContentsValue);
							if (nkmstageTempletV2 != null && nkmstageTempletV2.m_UnlockInfo.reqValue == phaseID && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV2.m_UnlockInfo, false))
							{
								NKCContentManager.MarkAsUnlockedContent(keyValuePair.Value);
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000FE728 File Offset: 0x000FC928
		public static void AddUnlockedContentCC(NKCContentManager.NKCUnlockableContent content)
		{
			if (content.m_eContentsType == ContentsType.COUNTERCASE_NEW_CHARACTER)
			{
				NKCContentManager.m_dicUnlockedContent.Add(content.m_Code, content);
			}
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000FE748 File Offset: 0x000FC948
		private static void MarkAsUnlockedContent(NKCContentManager.NKCUnlockableContent content)
		{
			if (NKCContentManager.UnlockPopupProcessRequired(content) && !NKCContentManager.m_dicUnlockedContent.ContainsKey(content.m_Code))
			{
				NKCContentManager.m_dicUnlockedContent.Add(content.m_Code, content);
			}
			NKCContentManager.m_hsUnlockCompletedContents.Add(content.m_Code);
			if (NKCContentManager.SET_NEED_UNLOCK_EFFECTS.Contains(content.m_eContentsType))
			{
				PlayerPrefs.SetInt(NKCContentManager.GetPreferenceString(content.m_eContentsType, content.m_ContentsValue), 1);
			}
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000FE7BC File Offset: 0x000FC9BC
		public static void ShowContentUnlockPopup(NKCContentManager.OnClose onClose = null, params STAGE_UNLOCK_REQ_TYPE[] aReqType)
		{
			if (NKCContentManager.m_bPopupOpened)
			{
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			if (NKCContentManager.m_dicUnlockedContent.Count <= 0)
			{
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			List<NKCContentManager.NKCUnlockableContent> list = new List<NKCContentManager.NKCUnlockableContent>();
			NKCContentManager.dOnClose = onClose;
			NKCContentManager.m_bPopupOpened = true;
			if (aReqType == null || aReqType.Length == 0)
			{
				list.AddRange(NKCContentManager.m_dicUnlockedContent.Values);
				NKCContentManager.m_dicUnlockedContent.Clear();
			}
			else
			{
				HashSet<STAGE_UNLOCK_REQ_TYPE> hashSet = new HashSet<STAGE_UNLOCK_REQ_TYPE>(aReqType);
				foreach (KeyValuePair<int, NKCContentManager.NKCUnlockableContent> keyValuePair in NKCContentManager.m_dicUnlockedContent)
				{
					if (hashSet.Contains(keyValuePair.Value.m_UnlockInfo.eReqType))
					{
						list.Add(keyValuePair.Value);
					}
				}
				foreach (NKCContentManager.NKCUnlockableContent nkcunlockableContent in list)
				{
					if (NKCContentManager.m_dicUnlockedContent.ContainsKey(nkcunlockableContent.m_Code))
					{
						NKCContentManager.m_dicUnlockedContent.Remove(nkcunlockableContent.m_Code);
					}
				}
			}
			list.Sort(new Comparison<NKCContentManager.NKCUnlockableContent>(NKCContentManager.Compare));
			foreach (NKCContentManager.NKCUnlockableContent item in list)
			{
				NKCContentManager.m_qUnlockedContent.Enqueue(item);
			}
			if (NKCContentManager.m_qUnlockedContent.Count > 0)
			{
				NKCContentManager.ContentUnlockPopupProcess();
				return;
			}
			NKCContentManager.m_bPopupOpened = false;
			NKCContentManager.OnClose onClose2 = NKCContentManager.dOnClose;
			if (onClose2 == null)
			{
				return;
			}
			onClose2();
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000FE970 File Offset: 0x000FCB70
		private static int Compare(NKCContentManager.NKCUnlockableContent lhs, NKCContentManager.NKCUnlockableContent rhs)
		{
			if (lhs.m_eContentsType == rhs.m_eContentsType)
			{
				return lhs.m_ContentsValue.CompareTo(rhs.m_ContentsValue);
			}
			return lhs.m_eContentsType.CompareTo(rhs.m_eContentsType);
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000FE9B0 File Offset: 0x000FCBB0
		private static void ContentUnlockPopupProcess()
		{
			NKCContentManager.NKCUnlockableContent nkcunlockableContent = NKCContentManager.m_qUnlockedContent.Dequeue();
			if (nkcunlockableContent == null)
			{
				Debug.LogError("unlockContent null!");
				NKCContentManager.OnCloseContentUnlockPopup();
				return;
			}
			NKCContentManager.RemoveUnlockedContent(nkcunlockableContent.m_eContentsType, nkcunlockableContent.m_ContentsValue, false);
			ContentsType eContentsType = nkcunlockableContent.m_eContentsType;
			if (eContentsType == ContentsType.COUNTERCASE_NEW_CHARACTER)
			{
				NKCPopupContentUnlock.instance.Open(nkcunlockableContent, new NKCPopupContentUnlock.OnClose(NKCContentManager.OnCloseContentUnlockPopup));
				return;
			}
			if (eContentsType != ContentsType.MARKET_REVIEW_REQUEST)
			{
				if (eContentsType == ContentsType.SHOP_ITEM_POPUP)
				{
					NKCPopupShopBannerNotice.Open(nkcunlockableContent.m_ContentsValue, new Action(NKCContentManager.OnCloseContentUnlockPopup));
					return;
				}
				NKCPopupContentUnlock.instance.Open(nkcunlockableContent, new NKCPopupContentUnlock.OnClose(NKCContentManager.OnCloseContentUnlockPopup));
				return;
			}
			else
			{
				if (NKCPublisherModule.Marketing.MarketReviewEnabled)
				{
					Debug.Log("MarketReview Enabled. Try review popup...");
					NKCPublisherModule.Marketing.OpenMarketReviewPopup(NKCContentManager.MakeReviewDescription(nkcunlockableContent), new UnityAction(NKCContentManager.OnCloseContentUnlockPopup));
					return;
				}
				Debug.Log("MarketReview Disabled!");
				NKCContentManager.OnCloseContentUnlockPopup();
				return;
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000FEA8D File Offset: 0x000FCC8D
		private static void OnCloseContentUnlockPopup()
		{
			if (NKCContentManager.m_qUnlockedContent.Count > 0)
			{
				NKCContentManager.ContentUnlockPopupProcess();
				return;
			}
			NKCContentManager.m_bPopupOpened = false;
			NKCContentManager.OnClose onClose = NKCContentManager.dOnClose;
			if (onClose == null)
			{
				return;
			}
			onClose();
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000FEAB8 File Offset: 0x000FCCB8
		public static GameObject AddUnlockedEffect(Transform parent)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(NKCResourceUtility.GetOrLoadAssetResource<GameObject>("ab_fx_ui_deck_open", "AB_FX_UI_CONTENT_UNLOCK_LOOP_NOPARTICLE", false));
			gameObject.transform.SetParent(parent);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
			gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
			return gameObject;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000FEB26 File Offset: 0x000FCD26
		public static string MakeUnlockConditionString(in UnlockInfo info, bool bSimple)
		{
			return NKCUtilString.GetUnlockConditionRequireDesc(info, bSimple);
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000FEB34 File Offset: 0x000FCD34
		public static NKM_SCEN_ID GetShortCutTargetSceneID(NKM_SHORTCUT_TYPE shortCutType)
		{
			switch (shortCutType)
			{
			case NKM_SHORTCUT_TYPE.SHORTCUT_MAINSTREAM:
			case NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON:
			case NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION:
				return NKM_SCEN_ID.NSI_OPERATION;
			case NKM_SHORTCUT_TYPE.SHORTCUT_DIVE:
			case NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION:
				return NKM_SCEN_ID.NSI_WORLDMAP;
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_MAIN:
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_RANK:
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_ASYNC:
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_LEAGUE:
				return NKM_SCEN_ID.NSI_GAUNTLET_LOBBY;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_CONTRACT:
				return NKM_SCEN_ID.NSI_CONTRACT;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_NEGOTIATE:
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_SCOUT:
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_MAKE:
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_ENCHANT:
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_TUNING:
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_MAKE:
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_UPGRADE:
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_LEVELUP:
			case NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE:
				return NKM_SCEN_ID.NSI_OFFICE;
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHOP:
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHOP_SCENE:
				return NKM_SCEN_ID.NSI_SHOP;
			case NKM_SHORTCUT_TYPE.SHORTCUT_FRIEND_ADD:
			case NKM_SHORTCUT_TYPE.SHORTCUT_FRIEND_MYPROFILE:
				return NKM_SCEN_ID.NSI_FRIEND;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION:
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_SHIP:
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_UNIT:
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_ILLUST:
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_STORY:
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_TEAMUP:
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_OPERATOR:
				return NKM_SCEN_ID.NSI_COLLECTION;
			case NKM_SHORTCUT_TYPE.SHORTCUT_MISSION:
			case NKM_SHORTCUT_TYPE.SHORTCUT_RANKING:
				return NKM_SCEN_ID.NSI_HOME;
			case NKM_SHORTCUT_TYPE.SHORTCUT_INVENTORY:
				return NKM_SCEN_ID.NSI_INVENTORY;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNITLIST:
				return NKM_SCEN_ID.NSI_UNIT_LIST;
			case NKM_SHORTCUT_TYPE.SHORTCUT_DECKSETUP:
				return NKM_SCEN_ID.NSI_TEAM;
			case NKM_SHORTCUT_TYPE.SHORTCUT_BASEMAIN:
				return NKM_SCEN_ID.NSI_BASE;
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHADOW_PALACE:
				return NKM_SCEN_ID.NSI_SHADOW_PALACE;
			case NKM_SHORTCUT_TYPE.SHORTCUT_EVENT:
				return NKM_SCEN_ID.NSI_INVALID;
			case NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE:
				return NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT;
			case NKM_SHORTCUT_TYPE.SHORTCUT_TRIM:
				return NKM_SCEN_ID.NSI_TRIM;
			case NKM_SHORTCUT_TYPE.SHORTCUT_EVENT_COLLECTION:
				return NKM_SCEN_ID.NSI_HOME;
			}
			return NKM_SCEN_ID.NSI_HOME;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000FEC4C File Offset: 0x000FCE4C
		public static void MoveToShortCut(NKM_SHORTCUT_TYPE shortCutType, string shortCutParam, bool bForce = false)
		{
			if (NKCUIMail.IsInstanceOpen)
			{
				NKCUIMail.Instance.Close();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_BASE)
			{
				if (NKCUIInventory.IsInstanceOpen)
				{
					NKCUIInventory.Instance.Close();
				}
				if (NKCUIUnitSelectList.IsInstanceOpen)
				{
					NKCUIUnitSelectList.Instance.Close();
				}
				if (NKCUIPersonnel.IsInstanceOpen)
				{
					NKCUIPersonnel.Instance.Close();
				}
			}
			switch (shortCutType)
			{
			case NKM_SHORTCUT_TYPE.SHORTCUT_MAINSTREAM:
				if (shortCutParam != "")
				{
					NKC_SCEN_OPERATION_V2 scen_OPERATION = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
					if (scen_OPERATION != null)
					{
						NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(int.Parse(shortCutParam));
						if (nkmwarfareTemplet != null)
						{
							NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
							if (nkmstageTempletV != null)
							{
								if (NKMEpisodeMgr.CheckEpisodeMission(NKCScenManager.CurrentUserData(), nkmstageTempletV))
								{
									scen_OPERATION.SetReservedStage(nkmstageTempletV);
									NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
									return;
								}
								NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, nkmstageTempletV.EpisodeCategory.ToString(), false);
								return;
							}
						}
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON:
				if (shortCutParam != "")
				{
					NKC_SCEN_OPERATION_V2 scen_OPERATION2 = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
					if (scen_OPERATION2 != null)
					{
						NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(int.Parse(shortCutParam));
						if (nkmstageTempletV2 != null)
						{
							ContentsType contentsType = NKCContentManager.GetContentsType(nkmstageTempletV2.EpisodeCategory);
							if (!NKCContentManager.IsContentsUnlocked(contentsType, 0, 0))
							{
								NKCContentManager.ShowLockedMessagePopup(contentsType, 0);
								return;
							}
							if (!nkmstageTempletV2.EpisodeTemplet.IsOpenedDayOfWeek())
							{
								NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_DAILY_CHECK_DAY, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
								return;
							}
							EPISODE_DIFFICULTY difficulty = nkmstageTempletV2.m_Difficulty;
							bool flag = NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), nkmstageTempletV2.EpisodeTemplet.m_EpisodeID, difficulty);
							if (!flag && nkmstageTempletV2.m_Difficulty == EPISODE_DIFFICULTY.HARD)
							{
								difficulty = EPISODE_DIFFICULTY.NORMAL;
								flag = NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), nkmstageTempletV2.EpisodeTemplet.m_EpisodeID, difficulty);
							}
							if (!flag)
							{
								if (NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(nkmstageTempletV2.EpisodeCategory, true, nkmstageTempletV2.m_Difficulty).Count <= 0)
								{
									NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
									return;
								}
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.Append(nkmstageTempletV2.EpisodeCategory);
								stringBuilder.Append("@");
								stringBuilder.Append(nkmstageTempletV2.EpisodeTemplet.m_EpisodeID);
								NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, stringBuilder.ToString(), false);
								return;
							}
							else
							{
								bool flag2 = NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION;
								bool flag3 = scen_OPERATION2.GetReservedEpisodeTemplet() != nkmstageTempletV2.EpisodeTemplet;
								NKC_SCEN_OPERATION_V2 scen_OPERATION3 = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
								if (scen_OPERATION3 != null)
								{
									if (NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV2.m_UnlockInfo, false))
									{
										scen_OPERATION3.SetReservedStage(nkmstageTempletV2);
									}
									else
									{
										NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
										UnlockInfo unlockInfo = nkmstageTempletV2.EpisodeTemplet.GetUnlockInfo();
										if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
										{
											scen_OPERATION3.SetReservedEpisodeTemplet(nkmstageTempletV2.EpisodeTemplet);
										}
									}
								}
								if (nkmstageTempletV2.EpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_COUNTERCASE)
								{
									NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
									return;
								}
								if (!NKMEpisodeMgr.CheckEpisodeMission(NKCScenManager.CurrentUserData(), nkmstageTempletV2))
								{
									NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
								}
								if (flag2 || flag3)
								{
									NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
									return;
								}
								scen_OPERATION2.ReopenEpisodeView();
								return;
							}
						}
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION:
				if (shortCutParam != "")
				{
					string[] array = shortCutParam.Split(new char[]
					{
						'@'
					});
					if (array.Length != 0)
					{
						NKC_SCEN_OPERATION_V2 scen_OPERATION4 = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
						EPISODE_CATEGORY episode_CATEGORY;
						if (scen_OPERATION4 != null && Enum.TryParse<EPISODE_CATEGORY>(array[0], out episode_CATEGORY))
						{
							ContentsType contentsType2 = NKCContentManager.GetContentsType(episode_CATEGORY);
							if (!NKCContentManager.IsContentsUnlocked(contentsType2, 0, 0))
							{
								NKCContentManager.ShowLockedMessagePopup(contentsType2, 0);
								return;
							}
							if (episode_CATEGORY == EPISODE_CATEGORY.EC_TRIM)
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TRIM, true);
								return;
							}
							if (episode_CATEGORY == EPISODE_CATEGORY.EC_SHADOW)
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_PALACE, true);
								return;
							}
							if (episode_CATEGORY == EPISODE_CATEGORY.EC_FIERCE)
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT, true);
								return;
							}
							if (array.Length > 1)
							{
								int num;
								if (int.TryParse(array[1], out num))
								{
									NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(num, EPISODE_DIFFICULTY.NORMAL);
									if (nkmepisodeTempletV != null)
									{
										if (!NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), nkmepisodeTempletV))
										{
											NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GetUnlockConditionRequireDesc(nkmepisodeTempletV.GetFirstStage(1), false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
											return;
										}
										if (num == 50)
										{
											scen_OPERATION4.SetReservedEpisodeTemplet(nkmepisodeTempletV);
										}
										else
										{
											NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(1);
											if (firstStage != null && firstStage.IsOpenedDayOfWeek())
											{
												scen_OPERATION4.SetReservedEpisodeTemplet(nkmepisodeTempletV);
											}
										}
									}
								}
							}
							else
							{
								scen_OPERATION4.SetReservedEpisodeCategory(episode_CATEGORY);
							}
						}
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, bForce);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_DIVE:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.DIVE, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().SetTargetEventID(0, 0);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE_READY, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.WORLDMAP, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.WORLDMAP, 0);
					return;
				}
				if (shortCutParam == "POPUP_RAID")
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReserveOpenEventList();
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetShowIntro();
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_MAIN:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.PVP, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.PVP, 0);
					return;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_INTRO, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_RANK:
				if (!NKCPVPManager.IsPvpRankUnlocked())
				{
					NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_RANK_MODE, NKMPvpCommonConst.Instance.RANK_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_CONTRACT:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.CONTRACT, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.CONTRACT, 0);
					return;
				}
				if (!string.IsNullOrEmpty(shortCutParam))
				{
					NKC_SCEN_CONTRACT scen_CONTRACT = NKCScenManager.GetScenManager().GET_SCEN_CONTRACT();
					if (scen_CONTRACT != null)
					{
						scen_CONTRACT.SetReserveContractID(shortCutParam);
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CONTRACT, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_TRAINING:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LAB_TRAINING, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LAB_TRAINING, 0);
					return;
				}
				long num2 = 0L;
				long.TryParse(shortCutParam, out num2);
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(num2) == null)
				{
					return;
				}
				NKCScenManager.GetScenManager().GET_NKC_SCEN_UNIT_LIST().SetOpenReserve(NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitSkillTraining, num2, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_LIMITBREAK:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LAB_LIMITBREAK, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LAB_LIMITBREAK, 0);
					return;
				}
				long num3 = 0L;
				long.TryParse(shortCutParam, out num3);
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(num3) == null)
				{
					return;
				}
				NKCScenManager.GetScenManager().GET_NKC_SCEN_UNIT_LIST().SetOpenReserve(NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitLimitbreak, num3, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_ENCHANT:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_LAB, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_LAB, 0);
					return;
				}
				long unitUID = 0L;
				long.TryParse(shortCutParam, out unitUID);
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.LAB_Enchant, unitUID, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_BASE, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_NEGOTIATE:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_NEGO, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_NEGO, 0);
					return;
				}
				long num4 = 0L;
				long.TryParse(shortCutParam, out num4);
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(num4) == null)
				{
					return;
				}
				NKCScenManager.GetScenManager().GET_NKC_SCEN_UNIT_LIST().SetOpenReserve(NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitNegotiate, num4, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_SCOUT:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_PERSONNAL, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(shortCutType, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_LIFETIME:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_PERSONNAL, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(shortCutType, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_DISMISS:
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNITLIST:
			{
				NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB reservedTab;
				if (!string.IsNullOrEmpty(shortCutParam) && Enum.TryParse<NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB>(shortCutParam, out reservedTab))
				{
					NKC_SCEN_UNIT_LIST nkc_SCEN_UNIT_LIST = NKCScenManager.GetScenManager().GET_NKC_SCEN_UNIT_LIST();
					if (nkc_SCEN_UNIT_LIST != null)
					{
						nkc_SCEN_UNIT_LIST.SetReservedTab(reservedTab);
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_MAKE:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(shortCutType, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_ENCHANT:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
					return;
				}
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant, 0L, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_BASE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_TUNING:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
					return;
				}
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_TUNING, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_TUNING, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning, 0L, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_BASE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_MAKE:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_HANGAR, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_HANGAR, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(shortCutType, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_UPGRADE:
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_LEVELUP:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPYARD, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPYARD, 0);
					return;
				}
				long num5 = 0L;
				long.TryParse(shortCutParam, out num5);
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(num5) == null)
				{
					return;
				}
				NKCScenManager.GetScenManager().GET_NKC_SCEN_UNIT_LIST().SetOpenReserve(NKC_SCEN_UNIT_LIST.eUIOpenReserve.ShipRepair, num5, bForce);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHOP:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
					return;
				}
				if (string.IsNullOrEmpty(shortCutParam))
				{
					return;
				}
				string[] array2 = shortCutParam.Split(new char[]
				{
					',',
					' ',
					'@'
				});
				if (array2.Length != 0)
				{
					int subIndex = 0;
					int reservedProductID = 0;
					string tabType = array2[0];
					if (array2.Length > 1)
					{
						int.TryParse(array2[1], out subIndex);
					}
					if (array2.Length > 2)
					{
						int.TryParse(array2[2], out reservedProductID);
					}
					NKCUIShop.ShopShortcut(tabType, subIndex, reservedProductID);
					return;
				}
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_FRIEND_ADD:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FRIENDS, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FRIENDS, 0);
					return;
				}
				if (!string.IsNullOrEmpty(shortCutParam))
				{
					NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE friend_TOP_MENU_TYPE = (NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE)Enum.Parse(typeof(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE), shortCutParam);
					if (friend_TOP_MENU_TYPE == NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_REGISTER)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().SetReservedTab(friend_TOP_MENU_TYPE);
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FRIEND, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION:
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_SHIP:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().SetOpenReserve(NKCUICollection.CollectionType.CT_SHIP, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_UNIT:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().SetOpenReserve(NKCUICollection.CollectionType.CT_UNIT, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_ILLUST:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().SetOpenReserve(NKCUICollection.CollectionType.CT_ILLUST, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_STORY:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().SetOpenReserve(NKCUICollection.CollectionType.CT_STORY, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_MISSION:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
					return;
				}
				int num6 = 0;
				int.TryParse(shortCutParam, out num6);
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_HOME)
				{
					NKCScenManager.GetScenManager().Get_SCEN_HOME().SetReservedOpenUI(NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_MISSION, num6);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
					return;
				}
				if (NKCUIMissionAchievement.IsInstanceOpen)
				{
					NKCUIManager.SetAsTopmost(NKCUIMissionAchievement.Instance, false);
					return;
				}
				NKCUIMissionAchievement.Instance.Open(num6);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_INVENTORY:
			{
				NKCUIInventory.NKC_INVENTORY_TAB reservedOpenTyp;
				if (!string.IsNullOrEmpty(shortCutParam) && Enum.TryParse<NKCUIInventory.NKC_INVENTORY_TAB>(shortCutParam, out reservedOpenTyp))
				{
					NKC_SCEN_INVENTORY scen_INVENTORY = NKCScenManager.GetScenManager().Get_SCEN_INVENTORY();
					if (scen_INVENTORY != null)
					{
						scen_INVENTORY.SetReservedOpenTyp(reservedOpenTyp);
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_INVENTORY, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_DECKSETUP:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.DECKVIEW, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.DECKVIEW, 0);
					return;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TEAM, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_BASEMAIN:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.Base_Main, 0L, true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_BASE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_ASYNC:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.PVP, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.PVP, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHADOW_PALACE:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.SHADOW_PALACE, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.SHADOW_PALACE, 0);
					return;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_PALACE, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_RANKING:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LEADERBOARD, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LEADERBOARD, 0);
					return;
				}
				NKMLeaderBoardTemplet reservedTemplet = null;
				int num7;
				if (int.TryParse(shortCutParam, out num7))
				{
					reservedTemplet = NKMTempletContainer<NKMLeaderBoardTemplet>.Find(num7);
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_HOME)
				{
					NKCScenManager.GetScenManager().Get_SCEN_HOME().SetReservedOpenUI(NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_RANKING_BOARD, num7);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
					return;
				}
				if (NKCUILeaderBoard.IsInstanceOpen)
				{
					NKCUIManager.SetAsTopmost(NKCUILeaderBoard.Instance, false);
					return;
				}
				NKCUILeaderBoard.Instance.Open(reservedTemplet, true);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_RANKING_POPUP:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LEADERBOARD, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LEADERBOARD, 0);
					return;
				}
				NKMLeaderBoardTemplet nkmleaderBoardTemplet = null;
				int key;
				if (int.TryParse(shortCutParam, out key))
				{
					nkmleaderBoardTemplet = NKMTempletContainer<NKMLeaderBoardTemplet>.Find(key);
				}
				if (nkmleaderBoardTemplet == null)
				{
					return;
				}
				NKCPopupLeaderBoardSingle.Instance.OpenSingle(nkmleaderBoardTemplet);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_GUILD:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.GUILD, 0);
					return;
				}
				if (NKCGuildManager.MyData.guildUid <= 0L)
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_INTRO, true);
					return;
				}
				if (shortCutParam != null)
				{
					if (!(shortCutParam == "TAB_POINT"))
					{
						if (shortCutParam == "TAB_SHOP")
						{
							NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().SetReserveMoveToShop(true);
						}
					}
					else
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().SetReserveLobbyTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point);
					}
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_EVENT:
			{
				NKMEventTabTemplet reservedTabTemplet = null;
				int eventId;
				if (int.TryParse(shortCutParam, out eventId))
				{
					reservedTabTemplet = NKMEventTabTemplet.Find(eventId);
				}
				NKCUIEvent.Instance.Open(reservedTabTemplet);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FIERCE, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FIERCE, 0);
					return;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE:
			{
				bool flag4 = false;
				int key2;
				NKMOfficeRoomTemplet.RoomType roomType;
				NKCUIOfficeMapFront.SectionType sectionType;
				if (int.TryParse(shortCutParam, out key2))
				{
					NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(key2);
					if (nkmofficeRoomTemplet == null)
					{
						return;
					}
					if (!nkmofficeRoomTemplet.IsFacility)
					{
						flag4 = true;
					}
				}
				else if (Enum.TryParse<NKMOfficeRoomTemplet.RoomType>(shortCutParam, out roomType))
				{
					if (roomType == NKMOfficeRoomTemplet.RoomType.Dorm)
					{
						flag4 = true;
					}
				}
				else if (Enum.TryParse<NKCUIOfficeMapFront.SectionType>(shortCutParam, out sectionType) && sectionType == NKCUIOfficeMapFront.SectionType.Room)
				{
					flag4 = true;
				}
				if (flag4 && !NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.OFFICE, 0);
					return;
				}
				if (!string.IsNullOrEmpty(shortCutParam))
				{
					if (string.Equals(shortCutParam, "Hangar") && !NKCContentManager.IsContentsUnlocked(ContentsType.BASE_HANGAR, 0, 0))
					{
						NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_HANGAR, 0);
						return;
					}
					if (string.Equals(shortCutParam, "Forge") && !NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
					{
						NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
						return;
					}
				}
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null)
				{
					return;
				}
				nkmuserData.OfficeData.ResetFriendUId();
				NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_URL:
				if (!string.IsNullOrEmpty(shortCutParam))
				{
					Application.OpenURL(shortCutParam);
					return;
				}
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_TEAMUP:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().SetOpenReserve(NKCUICollection.CollectionType.CT_TEAM_UP, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_OPERATOR:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().SetOpenReserve(NKCUICollection.CollectionType.CT_OPERATOR, shortCutParam);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHOP_SCENE:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
					return;
				}
				if (string.IsNullOrEmpty(shortCutParam))
				{
					return;
				}
				string[] array3 = shortCutParam.Split(new char[]
				{
					',',
					' ',
					'@'
				});
				if (array3.Length == 0)
				{
					return;
				}
				int tabIndex = 0;
				int productID = 0;
				string shopType = array3[0];
				if (array3.Length > 1)
				{
					int.TryParse(array3[1], out tabIndex);
				}
				if (array3.Length > 2)
				{
					int.TryParse(array3[2], out productID);
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_SHOP().SetReservedOpenTab(shopType, tabIndex, productID);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_SHOP)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_SHOP().MoveToReservedTab();
					return;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHOP, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_PVP_LEAGUE:
				if (!NKCPVPManager.IsPvpLeagueUnlocked())
				{
					NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_LEAGUE_MODE, NKMPvpCommonConst.Instance.LEAGUE_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_GUIDE:
				if (!string.IsNullOrEmpty(shortCutParam))
				{
					NKCUIPopUpGuide.Instance.Open(shortCutParam, 0);
					return;
				}
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_PT_EXCHANGE:
				NKCUIPointExchangeLobby.OpenPtExchangePopup();
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_GUIDE_MISSION:
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
					return;
				}
				int num8;
				int.TryParse(shortCutParam, out num8);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
				{
					NKCUIMissionGuide.Instance.Open(num8);
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_HOME().SetReservedOpenUI(NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_GUIDE_MISSION, num8);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_TRIM:
			{
				NKMTrimIntervalTemplet nkmtrimIntervalTemplet = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
				if (!NKCUITrimUtility.OpenTagEnabled || nkmtrimIntervalTemplet == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_NOT_INTERVAL_TIME, null, "");
					return;
				}
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIMENSION_TRIM, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.DIMENSION_TRIM, 0);
					return;
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_TRIM)
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TRIM, false);
					return;
				}
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_EVENT_COLLECTION:
			{
				int num9;
				NKMEventCollectionIndexTemplet nkmeventCollectionIndexTemplet;
				if (int.TryParse(shortCutParam, out num9) && num9 > 0)
				{
					nkmeventCollectionIndexTemplet = NKMTempletContainer<NKMEventCollectionIndexTemplet>.Find(num9);
				}
				else
				{
					nkmeventCollectionIndexTemplet = NKCUIModuleLobby.GetEventCollectionIndexTemplet();
				}
				if (nkmeventCollectionIndexTemplet == null || !nkmeventCollectionIndexTemplet.IsOpen)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_MENU_EXCEPTION_EVENT_EXPIRED_POPUP", false), null, "");
					return;
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
				{
					NKCUIModuleHome.OpenEventModule(nkmeventCollectionIndexTemplet);
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_HOME().SetReservedOpenUI(NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_EVENT_COLLECTION, nkmeventCollectionIndexTemplet.Key);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_FRIEND_MYPROFILE:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FRIENDS, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FRIENDS, 0);
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().SetReservedTab(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_MY_PROFILE);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FRIEND, false);
				return;
			case NKM_SHORTCUT_TYPE.SHORTCUT_UI_PREFAB:
			{
				Dictionary<string, string> dictionary = NKCUtil.ParseStringTable(shortCutParam);
				string text;
				if (!dictionary.TryGetValue("Prefab", out text))
				{
					Debug.LogError("SHORTCUT_UI_PREFAB : 패러미터에 Prefab 형식이 존재하지 않음.");
					return;
				}
				NKMAssetName nkmassetName = NKMAssetName.ParseBundleName("", text);
				if (string.IsNullOrEmpty(nkmassetName.m_BundleName))
				{
					Debug.LogError("SHORTCUT_UI_PREFAB : Prefab 항목 " + text + "의 Parse 실패");
					return;
				}
				NKCUIManager.eUIBaseRect parent = NKCUIManager.eUIBaseRect.UIFrontPopup;
				string text2;
				if (dictionary.TryGetValue("BaseRect", out text2))
				{
					NKCUIManager.eUIBaseRect eUIBaseRect;
					if (Enum.TryParse<NKCUIManager.eUIBaseRect>(text2, true, out eUIBaseRect))
					{
						parent = eUIBaseRect;
					}
					else
					{
						Debug.LogError("SHORTCUT_UI_PREFAB : BaseRect 항목 " + text2 + "의 파싱 실패");
					}
				}
				NKCUIBase instance = NKCUIManager.OpenNewInstance<NKCUIBase>(nkmassetName, parent, null).GetInstance();
				instance.Initialize();
				instance.OpenByShortcut(dictionary);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_HOME_EVENT_BANNER:
			{
				int reservedID;
				int.TryParse(shortCutParam, out reservedID);
				NKCScenManager.GetScenManager().Get_SCEN_HOME().SetReservedOpenUI(NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_EVENT_BANNER, reservedID);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				return;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_LOBBY_CHANGE:
				NKCScenManager.GetScenManager().SetActionAfterScenChange(new NKCScenManager.DoAfterScenChange(NKCScenManager.GetScenManager().Get_SCEN_HOME().ForceOpenLobbyChange));
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				return;
			}
			Debug.LogWarning(string.Format("정의되지 않은 숏컷 타입 - {0}", shortCutType));
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000FFF20 File Offset: 0x000FE120
		public static ContentsType GetContentsType(EPISODE_CATEGORY category)
		{
			switch (category)
			{
			case EPISODE_CATEGORY.EC_DAILY:
				return ContentsType.DAILY;
			case EPISODE_CATEGORY.EC_COUNTERCASE:
				return ContentsType.COUNTERCASE;
			case EPISODE_CATEGORY.EC_SIDESTORY:
				return ContentsType.SIDESTORY;
			case EPISODE_CATEGORY.EC_FIELD:
				return ContentsType.FIELD;
			case EPISODE_CATEGORY.EC_EVENT:
			case EPISODE_CATEGORY.EC_TIMEATTACK:
				return ContentsType.EVENT;
			case EPISODE_CATEGORY.EC_SUPPLY:
				return ContentsType.SUPPLY_MISSION;
			case EPISODE_CATEGORY.EC_CHALLENGE:
				return ContentsType.CHALLENGE;
			case EPISODE_CATEGORY.EC_TRIM:
				return ContentsType.DIMENSION_TRIM;
			case EPISODE_CATEGORY.EC_FIERCE:
				return ContentsType.FIERCE;
			case EPISODE_CATEGORY.EC_SHADOW:
				return ContentsType.SHADOW_PALACE;
			default:
				return ContentsType.None;
			}
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000FFF82 File Offset: 0x000FE182
		public static ContentsType GetContentsType(NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE dailyMissionType)
		{
			switch (dailyMissionType)
			{
			case NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE.NDMT_ATTACK:
				return ContentsType.DAILY_ATTACK;
			case NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE.NDMT_DEFENSE:
				return ContentsType.DAILY_DEFENCE;
			case NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE.NDMT_SEARCH:
				return ContentsType.DAILY_SEARCH;
			default:
				return ContentsType.None;
			}
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000FFFA2 File Offset: 0x000FE1A2
		public static bool CheckLevelChanged()
		{
			return NKCContentManager.m_bLevelChanged;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000FFFA9 File Offset: 0x000FE1A9
		public static void SetLevelChanged(bool bValue)
		{
			NKCContentManager.m_bLevelChanged = bValue;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000FFFB1 File Offset: 0x000FE1B1
		public static void RegisterCallback(NKMUserData userData)
		{
			if (userData != null)
			{
				userData.dOnUserLevelUpdate = (NKMUserData.OnUserLevelUpdate)Delegate.Combine(userData.dOnUserLevelUpdate, new NKMUserData.OnUserLevelUpdate(NKCContentManager.OnUserLevelChanged));
			}
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000FFFD8 File Offset: 0x000FE1D8
		private static void OnUserLevelChanged(NKMUserData userData)
		{
			NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_PLAYER_LEVEL, userData.UserLevel);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000FFFE8 File Offset: 0x000FE1E8
		private static string MakeReviewDescription(NKCContentManager.NKCUnlockableContent content)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RuntimePlatform platform = Application.platform;
			if (platform <= RuntimePlatform.IPhonePlayer)
			{
				if (platform > RuntimePlatform.OSXPlayer && platform != RuntimePlatform.IPhonePlayer)
				{
					goto IL_40;
				}
			}
			else
			{
				if (platform == RuntimePlatform.Android)
				{
					stringBuilder.Append("A");
					goto IL_40;
				}
				if (platform != RuntimePlatform.tvOS)
				{
					goto IL_40;
				}
			}
			stringBuilder.Append("I");
			IL_40:
			stringBuilder.Append((int)content.m_UnlockInfo.eReqType);
			if (!string.IsNullOrEmpty(content.m_UnlockInfo.reqValueStr))
			{
				stringBuilder.Append("@");
				stringBuilder.Append(content.m_UnlockInfo.reqValueStr);
			}
			if (content.m_UnlockInfo.reqValue != 0)
			{
				stringBuilder.Append("@");
				stringBuilder.Append(content.m_UnlockInfo.reqValue);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040031AD RID: 12717
		private static Dictionary<int, NKCContentManager.NKCUnlockableContent> m_dicUnlockableContents = new Dictionary<int, NKCContentManager.NKCUnlockableContent>();

		// Token: 0x040031AE RID: 12718
		private static Dictionary<int, NKCContentManager.NKCUnlockableContent> m_dicUnlockedContent = new Dictionary<int, NKCContentManager.NKCUnlockableContent>();

		// Token: 0x040031AF RID: 12719
		private static HashSet<int> m_hsUnlockCompletedContents = new HashSet<int>();

		// Token: 0x040031B0 RID: 12720
		private static Queue<NKCContentManager.NKCUnlockableContent> m_qUnlockedContent = new Queue<NKCContentManager.NKCUnlockableContent>();

		// Token: 0x040031B1 RID: 12721
		private static Dictionary<int, NKMStageTempletV2> m_dicLockedCounterCaseStageTemplet = new Dictionary<int, NKMStageTempletV2>();

		// Token: 0x040031B2 RID: 12722
		private static NKCContentManager.OnClose dOnClose;

		// Token: 0x040031B3 RID: 12723
		private static bool m_bPopupOpened = false;

		// Token: 0x040031B4 RID: 12724
		private static readonly HashSet<ContentsType> SET_NEED_UNLOCK_EFFECTS = new HashSet<ContentsType>
		{
			ContentsType.EPISODE,
			ContentsType.ACT,
			ContentsType.DUNGEON,
			ContentsType.FIELD,
			ContentsType.DAILY,
			ContentsType.SIDESTORY,
			ContentsType.COUNTERCASE
		};

		// Token: 0x040031B5 RID: 12725
		private static bool m_bLevelChanged = false;

		// Token: 0x02001305 RID: 4869
		public enum eContentStatus
		{
			// Token: 0x040097C6 RID: 38854
			Open,
			// Token: 0x040097C7 RID: 38855
			Lock,
			// Token: 0x040097C8 RID: 38856
			Hide
		}

		// Token: 0x02001306 RID: 4870
		public class NKCUnlockableContent
		{
			// Token: 0x0600A4F6 RID: 42230 RVA: 0x00344BBF File Offset: 0x00342DBF
			public NKCUnlockableContent()
			{
			}

			// Token: 0x0600A4F7 RID: 42231 RVA: 0x00344BC8 File Offset: 0x00342DC8
			public NKCUnlockableContent(NKMContentUnlockTemplet templet)
			{
				this.m_eContentsType = templet.m_eContentsType;
				this.m_ContentsValue = templet.m_ContentsValue;
				this.m_UnlockInfo = templet.m_UnlockInfo;
				this.m_LockedText = templet.m_LockedText;
				this.m_PopupTitle = templet.m_strPopupTitle;
				this.m_PopupDesc = templet.m_strPopupDesc;
				this.m_PopupImageName = templet.m_strPopupImageName;
				this.m_PopupIconAssetBundleName = templet.m_PopupIconAssetBundleName;
				this.m_PopupIconName = templet.m_PopupIconName;
				this.m_Code = NKCContentManager.NKCUnlockableContent.Encode(this.m_eContentsType, this.m_ContentsValue);
			}

			// Token: 0x0600A4F8 RID: 42232 RVA: 0x00344C60 File Offset: 0x00342E60
			public NKCUnlockableContent(ShopItemTemplet shopTemplet)
			{
				this.m_eContentsType = ContentsType.SHOP_ITEM_POPUP;
				this.m_ContentsValue = shopTemplet.m_ProductID;
				this.m_UnlockInfo = shopTemplet.m_UnlockInfo;
				this.m_LockedText = ((shopTemplet.m_UnlockReqStrID != "AUTO") ? shopTemplet.m_UnlockReqStrID : "");
				this.m_Code = NKCContentManager.NKCUnlockableContent.Encode(this.m_eContentsType, this.m_ContentsValue);
			}

			// Token: 0x0600A4F9 RID: 42233 RVA: 0x00344CD0 File Offset: 0x00342ED0
			public NKCUnlockableContent(ContentsType contentsType, int contentsValue, UnlockInfo unlockInfo, string title, string desc, string imgName)
			{
				this.m_eContentsType = contentsType;
				this.m_ContentsValue = contentsValue;
				this.m_UnlockInfo = unlockInfo;
				this.m_LockedText = "";
				this.m_PopupTitle = title;
				this.m_PopupDesc = desc;
				this.m_PopupImageName = imgName;
				this.m_Code = NKCContentManager.NKCUnlockableContent.Encode(this.m_eContentsType, this.m_ContentsValue);
			}

			// Token: 0x0600A4FA RID: 42234 RVA: 0x00344D32 File Offset: 0x00342F32
			public static int Encode(ContentsType contentsType, int contentsValue)
			{
				return (int)(87 * contentsValue + contentsType);
			}

			// Token: 0x0600A4FB RID: 42235 RVA: 0x00344D3A File Offset: 0x00342F3A
			public static bool Decode(int code, out ContentsType contentsType, out int contentsValue)
			{
				contentsType = (ContentsType)(code % 87);
				contentsValue = code / 87;
				return false;
			}

			// Token: 0x040097C9 RID: 38857
			public int m_Code;

			// Token: 0x040097CA RID: 38858
			public ContentsType m_eContentsType;

			// Token: 0x040097CB RID: 38859
			public int m_ContentsValue;

			// Token: 0x040097CC RID: 38860
			public UnlockInfo m_UnlockInfo;

			// Token: 0x040097CD RID: 38861
			public string m_LockedText;

			// Token: 0x040097CE RID: 38862
			public string m_PopupTitle;

			// Token: 0x040097CF RID: 38863
			public string m_PopupDesc;

			// Token: 0x040097D0 RID: 38864
			public string m_PopupImageName;

			// Token: 0x040097D1 RID: 38865
			public string m_PopupIconAssetBundleName;

			// Token: 0x040097D2 RID: 38866
			public string m_PopupIconName;
		}

		// Token: 0x02001307 RID: 4871
		// (Invoke) Token: 0x0600A4FD RID: 42237
		public delegate void OnClose();
	}
}
