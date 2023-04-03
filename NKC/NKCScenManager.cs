using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using AssetBundles;
using ClientPacket.Warfare;
using Cs.Logging;
using Cs.Protocol;
using NKC.Loading;
using NKC.Localization;
using NKC.PacketHandler;
using NKC.Patcher;
using NKC.Publisher;
using NKC.Trim;
using NKC.UI;
using NKC.UI.Event;
using NKC.UI.Option;
using NKC.Util;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020006C6 RID: 1734
	public class NKCScenManager : MonoBehaviour
	{
		// Token: 0x06003B9A RID: 15258 RVA: 0x00131E66 File Offset: 0x00130066
		public static NKCScenManager GetScenManager()
		{
			return NKCScenManager.m_ScenManager;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x00131E6D File Offset: 0x0013006D
		public int GetSystemMemorySize()
		{
			return this.m_SystemMemorySize;
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x00131E7E File Offset: 0x0013007E
		// (set) Token: 0x06003B9C RID: 15260 RVA: 0x00131E75 File Offset: 0x00130075
		private float m_fAppEnableConnectCheckTime
		{
			get
			{
				return this.m_enableConnectCheckTime;
			}
			set
			{
				this.m_enableConnectCheckTime = value;
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x00131E86 File Offset: 0x00130086
		public void SetAppEnableConnectCheckTime(float fTime, bool bForce = false)
		{
			if (fTime == -1f || this.m_fAppEnableConnectCheckTime < 0f || this.m_fAppEnableConnectCheckTime > fTime || bForce)
			{
				this.m_fAppEnableConnectCheckTime = fTime;
			}
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x00131EB0 File Offset: 0x001300B0
		public NKCConnectLogin GetConnectLogin()
		{
			return this.m_NKCConnectLogin;
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x00131EB8 File Offset: 0x001300B8
		public NKCConnectGame GetConnectGame()
		{
			return this.m_NKCConnectGame;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00131EC0 File Offset: 0x001300C0
		public NKCGameClient GetGameClient()
		{
			return this.m_NKCGameClient;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x00131EC8 File Offset: 0x001300C8
		public float GetFixedFrameTime()
		{
			return this.m_FixedFrameTime;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x00131ED0 File Offset: 0x001300D0
		public NKCScenChangeOrder PeekNextScenChangeOrder()
		{
			if (this.m_qScenChange.Count <= 0)
			{
				return null;
			}
			return this.m_qScenChange.Peek();
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00131EED File Offset: 0x001300ED
		public NKM_SCEN_ID GetNowScenID()
		{
			if (this.m_NKM_SCEN_NOW != null)
			{
				return this.m_NKM_SCEN_NOW.Get_NKM_SCEN_ID();
			}
			return NKM_SCEN_ID.NSI_INVALID;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00131F04 File Offset: 0x00130104
		public NKC_SCEN_STATE GetNowScenState()
		{
			if (this.m_NKM_SCEN_NOW != null)
			{
				return this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE();
			}
			return NKC_SCEN_STATE.NSS_INVALID;
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x00131F1B File Offset: 0x0013011B
		public NKC_SCEN_LOGIN Get_SCEN_LOGIN()
		{
			return this.m_NKC_SCEN_LOGIN;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x00131F23 File Offset: 0x00130123
		public NKC_SCEN_HOME Get_SCEN_HOME()
		{
			return this.m_NKC_SCEN_HOME;
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x00131F2B File Offset: 0x0013012B
		public NKC_SCEN_GAME Get_SCEN_GAME()
		{
			return this.m_NKC_SCEN_GAME;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x00131F33 File Offset: 0x00130133
		public NKC_SCEN_TEAM Get_SCEN_TEAM()
		{
			return this.m_NKC_SCEN_TEAM;
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x00131F3B File Offset: 0x0013013B
		public NKC_SCEN_BASE Get_SCEN_BASE()
		{
			return this.m_NKC_SCEN_BASE;
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x00131F43 File Offset: 0x00130143
		public NKC_SCEN_CONTRACT GET_SCEN_CONTRACT()
		{
			return this.m_NKC_SCEN_CONTRACT;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x00131F4B File Offset: 0x0013014B
		public NKC_SCEN_INVENTORY Get_SCEN_INVENTORY()
		{
			return this.m_NKC_SCEN_INVENTORY;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x00131F53 File Offset: 0x00130153
		public NKC_SCEN_CUTSCEN_SIM Get_SCEN_CUTSCEN_SIM()
		{
			return this.m_NKC_SCEN_CUTSCEN_SIM;
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x00131F5B File Offset: 0x0013015B
		public NKC_SCEN_OPERATION_V2 Get_SCEN_OPERATION()
		{
			return this.m_NKC_SCEN_OPERATION_V2;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x00131F63 File Offset: 0x00130163
		public NKC_SCEN_EPISODE Get_SCEN_EPISODE()
		{
			return this.m_NKC_SCEN_EPISODE;
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x00131F6B File Offset: 0x0013016B
		public NKC_SCEN_DUNGEON_ATK_READY Get_SCEN_DUNGEON_ATK_READY()
		{
			return this.m_NKC_SCEN_DUNGEON_ATK_READY;
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x00131F73 File Offset: 0x00130173
		public NKC_SCEN_UNIT_LIST GET_NKC_SCEN_UNIT_LIST()
		{
			return this.m_NKC_SCEN_UNIT_LIST;
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x00131F7B File Offset: 0x0013017B
		public NKC_SCEN_COLLECTION Get_NKC_SCEN_COLLECTION()
		{
			return this.m_NKC_SCEN_COLLECTION;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x00131F83 File Offset: 0x00130183
		public NKC_SCEN_WARFARE_GAME Get_NKC_SCEN_WARFARE_GAME()
		{
			return this.m_NKC_SCEN_WARFARE_GAME;
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x00131F8B File Offset: 0x0013018B
		public NKC_SCEN_SHOP Get_NKC_SCEN_SHOP()
		{
			return this.m_NKC_SCEN_SHOP;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x00131F93 File Offset: 0x00130193
		public NKC_SCEN_FRIEND Get_NKC_SCEN_FRIEND()
		{
			return this.m_NKC_SCEN_FRIEND;
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x00131F9B File Offset: 0x0013019B
		public NKC_SCEN_WORLDMAP Get_NKC_SCEN_WORLDMAP()
		{
			return this.m_NKC_SCEN_WORLDMAP;
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x00131FA3 File Offset: 0x001301A3
		public NKC_SCEN_CUTSCEN_DUNGEON Get_NKC_SCEN_CUTSCEN_DUNGEON()
		{
			return this.m_NKC_SCEN_CUTSCEN_DUNGEON;
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x00131FAB File Offset: 0x001301AB
		public NKC_SCEN_GAME_RESULT Get_NKC_SCEN_GAME_RESULT()
		{
			return this.m_NKC_SCEN_GAME_RESULT;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x00131FB3 File Offset: 0x001301B3
		public NKC_SCEN_DIVE_READY Get_NKC_SCEN_DIVE_READY()
		{
			return this.m_NKC_SCEN_DIVE_READY;
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x00131FBB File Offset: 0x001301BB
		public NKC_SCEN_DIVE Get_NKC_SCEN_DIVE()
		{
			return this.m_NKC_SCEN_DIVE;
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x00131FC3 File Offset: 0x001301C3
		public NKC_SCEN_DIVE_RESULT Get_NKC_SCEN_DIVE_RESULT()
		{
			return this.m_NKC_SCEN_DIVE_RESULT;
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x00131FCB File Offset: 0x001301CB
		public NKC_SCEN_GAUNTLET_INTRO Get_NKC_SCEN_GAUNTLET_INTRO()
		{
			return this.m_NKC_SCEN_GAUNTLET_INTRO;
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00131FD3 File Offset: 0x001301D3
		public NKC_SCEN_GAUNTLET_LOBBY Get_NKC_SCEN_GAUNTLET_LOBBY()
		{
			return this.m_NKC_SCEN_GAUNTLET_LOBBY;
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x00131FDB File Offset: 0x001301DB
		public NKC_SCEN_GAUNTLET_MATCH_READY Get_NKC_SCEN_GAUNTLET_MATCH_READY()
		{
			return this.m_NKC_SCEN_GAUNTLET_MATCH_READY;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00131FE3 File Offset: 0x001301E3
		public NKC_SCEN_GAUNTLET_MATCH Get_NKC_SCEN_GAUNTLET_MATCH()
		{
			return this.m_NKC_SCEN_GAUNTLET_MATCH;
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x00131FEB File Offset: 0x001301EB
		public NKC_SCEN_GAUNTLET_ASYNC_READY Get_NKC_SCEN_GAUNTLET_ASYNC_READY()
		{
			return this.m_NKC_SCEN_GAUNTLET_ASYNC_READY;
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x00131FF3 File Offset: 0x001301F3
		public NKC_SCEN_GAUNTLET_PRIVATE_READY Get_NKC_SCEN_GAUNTLET_PRIVATE_READY()
		{
			return this.m_NKC_SCEN_GAUNTLET_PRIVATE_READY;
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x00131FFB File Offset: 0x001301FB
		public NKC_SCEN_GAUNTLET_LEAGUE_ROOM Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM()
		{
			return this.m_NKC_SCEN_GAUNTLET_LEAGUE_ROOM;
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x00132003 File Offset: 0x00130203
		public NKC_SCEN_GAUNTLET_PRIVATE_ROOM Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM()
		{
			return this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM;
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x0013200B File Offset: 0x0013020B
		public NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT()
		{
			return this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT;
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x00132013 File Offset: 0x00130213
		public NKC_SCEN_RAID Get_NKC_SCEN_RAID()
		{
			return this.m_NKC_SCEN_RAID;
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x0013201B File Offset: 0x0013021B
		public NKC_SCEN_RAID_READY Get_NKC_SCEN_RAID_READY()
		{
			return this.m_NKC_SCEN_RAID_READY;
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00132023 File Offset: 0x00130223
		public NKC_SCEN_SHADOW_PALACE Get_NKC_SCEN_SHADOW_PALACE()
		{
			return this.m_NKC_SCEN_SHADOW_PALACE;
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x0013202B File Offset: 0x0013022B
		public NKC_SCEN_SHADOW_BATTLE Get_NKC_SCEN_SHADOW_BATTLE()
		{
			return this.m_NKC_SCEN_SHADOW_BATTLE;
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00132033 File Offset: 0x00130233
		public NKC_SCEN_SHADOW_RESULT Get_NKC_SCEN_SHADOW_RESULT()
		{
			return this.m_NKC_SCEN_SHADOW_RESULT;
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x0013203B File Offset: 0x0013023B
		public NKC_SCEN_GUILD_INTRO Get_NKC_SCEN_GUILD_INTRO()
		{
			return this.m_NKC_SCEN_GUILD_INTRO;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x00132043 File Offset: 0x00130243
		public NKC_SCEN_GUILD_LOBBY Get_NKC_SCEN_GUILD_LOBBY()
		{
			return this.m_NKC_SCEN_GUILD_LOBBY;
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x0013204B File Offset: 0x0013024B
		public NKC_SCEN_GUILD_COOP Get_NKC_SCEN_GUILD_COOP()
		{
			return this.m_NKC_SCEN_GUILD_COOP;
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00132053 File Offset: 0x00130253
		public NKC_SCEN_OFFICE Get_NKC_SCEN_OFFICE()
		{
			return this.m_NKC_SCEN_OFFICE;
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x0013205B File Offset: 0x0013025B
		public NKC_SCEN_TRIM Get_NKC_SCEN_TRIM()
		{
			return this.m_NKC_SCEN_TRIM;
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x00132063 File Offset: 0x00130263
		public NKC_SCEN_TRIM_RESULT Get_NKC_SCEN_TRIM_RESULT()
		{
			return this.m_NKC_SCEN_TRIM_RESULT;
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x0013206B File Offset: 0x0013026B
		public NKC_SCEN_DUNGEON_RESULT GET_NKC_SCEN_DUNGEON_RESULT()
		{
			return this.m_NKC_SCEN_DUNGEON_RESULT;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x00132073 File Offset: 0x00130273
		public NKC_SCEN_FIERCE_BATTLE_SUPPORT Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT()
		{
			return this.m_NKC_SCEN_FIERCE_BATTLE_SUPPORT;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x0013207B File Offset: 0x0013027B
		public NKMUserData GetMyUserData()
		{
			return this.m_MyUserData;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00132084 File Offset: 0x00130284
		public void SetMyUserData(NKMUserData cNKMUserData)
		{
			this.m_MyUserData = cNKMUserData;
			this.m_MyUserData.m_ArmyData.SetOwner(cNKMUserData);
			NKCUIManager.RegisterUICallback(this.m_MyUserData);
			NKCContentManager.RegisterCallback(this.m_MyUserData);
			this.m_MyUserData.m_LastDiveHistoryData = new HashSet<int>(this.m_MyUserData.m_DiveHistoryData);
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x001320DA File Offset: 0x001302DA
		public static NKMUserData CurrentUserData()
		{
			if (NKCScenManager.GetScenManager() != null)
			{
				return NKCScenManager.GetScenManager().GetMyUserData();
			}
			return null;
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x001320F8 File Offset: 0x001302F8
		public static NKMArmyData CurrentArmyData()
		{
			if (NKCScenManager.GetScenManager() == null)
			{
				return null;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return null;
			}
			return myUserData.m_ArmyData;
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x0013212A File Offset: 0x0013032A
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x00132132 File Offset: 0x00130332
		public WarfareGameData WarfareGameData { get; private set; }

		// Token: 0x06003BD8 RID: 15320 RVA: 0x0013213B File Offset: 0x0013033B
		public void SetWarfareGameData(WarfareGameData warfare)
		{
			this.WarfareGameData = warfare;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x00132144 File Offset: 0x00130344
		public NKCReplayMgr GetNKCReplayMgr()
		{
			return this.m_NKCReplayMgr;
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x0013214C File Offset: 0x0013034C
		public NKCContractDataMgr GetNKCContractDataMgr()
		{
			return this.m_NKCContractDataMgr;
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x00132154 File Offset: 0x00130354
		public NKCFierceBattleSupportDataMgr GetNKCFierceBattleSupportDataMgr()
		{
			return this.m_NKCFierceBattleSupportDataMgr;
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x0013215C File Offset: 0x0013035C
		public NKCRaidDataMgr GetNKCRaidDataMgr()
		{
			return this.m_NKCRaidDataMgr;
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00132164 File Offset: 0x00130364
		public NKCRepeatOperaion GetNKCRepeatOperaion()
		{
			return this.m_NKCRepeatOperaion;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x0013216C File Offset: 0x0013036C
		public NKCPowerSaveMode GetNKCPowerSaveMode()
		{
			return this.m_NKCPowerSaveMode;
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x00132174 File Offset: 0x00130374
		public NKCSurveyMgr GetNKCSurveyMgr()
		{
			return this.m_NKCSurveyMgr;
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x0013217C File Offset: 0x0013037C
		public NKCGameOptionData GetGameOptionData()
		{
			return this.m_GameOptionData;
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00132184 File Offset: 0x00130384
		public NKCEventPassDataManager GetEventPassDataManager()
		{
			return this.m_NKCEventPassDataManager;
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x0013218C File Offset: 0x0013038C
		// (set) Token: 0x06003BE3 RID: 15331 RVA: 0x00132194 File Offset: 0x00130394
		public Camera TextureCamera { get; private set; }

		// Token: 0x06003BE4 RID: 15332 RVA: 0x0013219D File Offset: 0x0013039D
		public bool GetHasTouch(int index)
		{
			return this.m_bTouch[index];
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x001321A7 File Offset: 0x001303A7
		public Vector2 GetTouchPos2D(int index)
		{
			return this.m_TouchPos2D[index];
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x001321B5 File Offset: 0x001303B5
		public bool GetHasPinch()
		{
			return this.m_bHasPinch;
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x001321BD File Offset: 0x001303BD
		public float GetPinchDeltaMagnitude()
		{
			return this.m_fPinchDeltaMagnitude;
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x001321C5 File Offset: 0x001303C5
		public void ResetPinchDeltaMagnitude()
		{
			this.m_fPinchDeltaMagnitude = 0f;
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x001321D2 File Offset: 0x001303D2
		public Vector2 GetPinchCenter()
		{
			return this.m_vPinchCenter;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x001321DA File Offset: 0x001303DA
		public GameObject Get_NKM_NEW_INSTANT()
		{
			return this.m_NKM_NEW_INSTANT;
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x001321E2 File Offset: 0x001303E2
		public NKCEffectManager GetEffectManager()
		{
			return this.m_NKCEffectManager;
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x001321EA File Offset: 0x001303EA
		public RectTransform Get_NUF_AFTER_UI_EFFECT()
		{
			return this.m_rtNUF_AFTER_UI_EFFECT;
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x001321F2 File Offset: 0x001303F2
		public NKMObjectPool GetObjectPool()
		{
			return this.GetGameClient().GetObjectPool();
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x001321FF File Offset: 0x001303FF
		public void SetSkipScenChangeFadeOutEffect(bool bSet)
		{
			this.m_bSkipScenChangeFadeOutEffect = bSet;
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x00132208 File Offset: 0x00130408
		public void SetLanguage()
		{
			if (!this.m_bSetLanguage)
			{
				this.m_bSetLanguage = true;
				NKM_NATIONAL_CODE nkm_NATIONAL_CODE = NKM_NATIONAL_CODE.NNC_KOREA;
				NKCGameOptionData gameOptionData = this.GetGameOptionData();
				if (gameOptionData != null)
				{
					nkm_NATIONAL_CODE = gameOptionData.NKM_NATIONAL_CODE;
				}
				NKCStringTable.LoadFromLUA(nkm_NATIONAL_CODE);
				NKC_VOICE_CODE nkc_VOICE_CODE = NKCUIVoiceManager.LoadLocalVoiceCode();
				NKCUIVoiceManager.SetVoiceCode(nkc_VOICE_CODE);
				AssetBundleManager.ActiveVariants = NKCLocalization.GetVariants(nkm_NATIONAL_CODE, nkc_VOICE_CODE);
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x00132258 File Offset: 0x00130458
		private void Awake()
		{
			if (NKCPublisherModule.ApplyCultureInfo())
			{
				CultureInfo currentCulture = new CultureInfo("en-US");
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			Debug.unityLogger.logEnabled = NKCDefineManager.DEFINE_UNITY_DEBUG_LOG();
			if (NKCScenManager.m_ScenManager == null)
			{
				AssetBundleManager.ActiveVariants = new string[]
				{
					"asset"
				};
				NKCAssetResourceManager.Init();
				this.m_GameOptionData.LoadLocal();
				this.SetLanguage();
				NKCScenManager.m_ScenManager = this;
				if (!NKCScenManager.m_bApplicationDelegateRegistered)
				{
					Application.lowMemory += this.OnLowMemory;
					Application.logMessageReceived += NKCScenManager.OnLogReceived;
					NKCScenManager.m_bApplicationDelegateRegistered = true;
				}
				this.m_SystemMemorySize = SystemInfo.systemMemorySize;
				Log.Info(string.Format("SystemInfo.systemMemorySize: {0}", this.m_SystemMemorySize), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCScenManager.cs", 380);
				Screen.sleepTimeout = -1;
				Application.targetFrameRate = 30;
				Shader.SetGlobalFloat("_FxGlobalTransparency", 0f);
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00132358 File Offset: 0x00130558
		private void Start()
		{
			if (!this.m_UIInit)
			{
				NKMProfiler.SetProvider(new ProfilerProvider());
				PacketController.Instance.Initialize();
				this.m_NKM_NEW_INSTANT = GameObject.Find("NKM_NEW_INSTANT");
				this.m_NKM_NEW_INSTANT.SetActive(false);
				GameObject gameObject = GameObject.Find("NUF_AFTER_UI_EFFECT");
				this.m_rtNUF_AFTER_UI_EFFECT = ((gameObject != null) ? gameObject.GetComponent<RectTransform>() : null);
				NKCPacketObjectPool.Init();
				NKCMessage.Init();
				NKCMain.NKCInit();
				this.m_NKCEffectManager.Init();
				this.m_NKCEffectManager.LoadFromLUA("LUA_EFFECT_POOL");
				NKCSoundManager.Init();
				NKCSoundManager.LoadFromLUA("LUA_SCEN_MUSIC");
				NKCSoundManager.SetAllVolume(this.m_GameOptionData.GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP.ALL));
				NKCSoundManager.SetMusicVolume(this.m_GameOptionData.GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP.BGM));
				NKCSoundManager.SetSoundVolume(this.m_GameOptionData.GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP.SE));
				NKCSoundManager.SetVoiceVolume(this.m_GameOptionData.GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP.VOICE));
				NKCUIVoiceManager.Init();
				NKCVoiceTimingManager.LoadFromLua("LUA_UNIT_VOICE_TEMPLET");
				NKCCamera.Init();
				this.InitTextureCamera();
				NKCCamera.SetBloomEnableUI(this.m_GameOptionData.UseCommonEffect);
				if (QualitySettings.GetQualityLevel() != (int)this.m_GameOptionData.QualityLevel)
				{
					QualitySettings.SetQualityLevel((int)this.m_GameOptionData.QualityLevel, true);
				}
				this.m_NKCGameClient = new NKCGameClient();
				NKMAttendanceManager.Init(NKCSynchronizedTime.GetServerUTCTime(0.0));
				Debug.Log("UIManager Init");
				NKCUIManager.Init();
				Debug.Log("Loading UI Open");
				this.m_UIInit = true;
				Debug.Log("Scen Build");
				this.m_NKC_SCEN_LOGIN = new NKC_SCEN_LOGIN();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_LOGIN.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_LOGIN);
				this.m_NKC_SCEN_HOME = new NKC_SCEN_HOME();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_HOME.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_HOME);
				this.m_NKC_SCEN_GAME = new NKC_SCEN_GAME();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAME.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAME);
				this.m_NKC_SCEN_TEAM = new NKC_SCEN_TEAM();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_TEAM.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_TEAM);
				this.m_NKC_SCEN_BASE = new NKC_SCEN_BASE();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_BASE.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_BASE);
				this.m_NKC_SCEN_CONTRACT = new NKC_SCEN_CONTRACT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_CONTRACT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_CONTRACT);
				this.m_NKC_SCEN_INVENTORY = new NKC_SCEN_INVENTORY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_INVENTORY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_INVENTORY);
				this.m_NKC_SCEN_CUTSCEN_SIM = new NKC_SCEN_CUTSCEN_SIM();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_CUTSCEN_SIM.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_CUTSCEN_SIM);
				this.m_NKC_SCEN_OPERATION_V2 = new NKC_SCEN_OPERATION_V2();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_OPERATION_V2.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_OPERATION_V2);
				this.m_NKC_SCEN_EPISODE = new NKC_SCEN_EPISODE();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_EPISODE.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_EPISODE);
				this.m_NKC_SCEN_DUNGEON_ATK_READY = new NKC_SCEN_DUNGEON_ATK_READY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_DUNGEON_ATK_READY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_DUNGEON_ATK_READY);
				this.m_NKC_SCEN_UNIT_LIST = new NKC_SCEN_UNIT_LIST();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_UNIT_LIST.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_UNIT_LIST);
				this.m_NKC_SCEN_COLLECTION = new NKC_SCEN_COLLECTION();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_COLLECTION.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_COLLECTION);
				this.m_NKC_SCEN_WARFARE_GAME = new NKC_SCEN_WARFARE_GAME();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_WARFARE_GAME.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_WARFARE_GAME);
				this.m_NKC_SCEN_SHOP = new NKC_SCEN_SHOP();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_SHOP.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_SHOP);
				this.m_NKC_SCEN_FRIEND = new NKC_SCEN_FRIEND();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_FRIEND.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_FRIEND);
				this.m_NKC_SCEN_WORLDMAP = new NKC_SCEN_WORLDMAP();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_WORLDMAP.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_WORLDMAP);
				this.m_NKC_SCEN_CUTSCEN_DUNGEON = new NKC_SCEN_CUTSCEN_DUNGEON();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_CUTSCEN_DUNGEON.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_CUTSCEN_DUNGEON);
				this.m_NKC_SCEN_GAME_RESULT = new NKC_SCEN_GAME_RESULT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAME_RESULT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAME_RESULT);
				this.m_NKC_SCEN_DIVE_READY = new NKC_SCEN_DIVE_READY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_DIVE_READY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_DIVE_READY);
				this.m_NKC_SCEN_DIVE = new NKC_SCEN_DIVE();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_DIVE.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_DIVE);
				this.m_NKC_SCEN_DIVE_RESULT = new NKC_SCEN_DIVE_RESULT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_DIVE_RESULT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_DIVE_RESULT);
				this.m_NKC_SCEN_GAUNTLET_INTRO = new NKC_SCEN_GAUNTLET_INTRO();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_INTRO.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_INTRO);
				this.m_NKC_SCEN_GAUNTLET_LOBBY = new NKC_SCEN_GAUNTLET_LOBBY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_LOBBY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_LOBBY);
				this.m_NKC_SCEN_GAUNTLET_MATCH_READY = new NKC_SCEN_GAUNTLET_MATCH_READY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_MATCH_READY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_MATCH_READY);
				this.m_NKC_SCEN_GAUNTLET_MATCH = new NKC_SCEN_GAUNTLET_MATCH();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_MATCH.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_MATCH);
				this.m_NKC_SCEN_GAUNTLET_ASYNC_READY = new NKC_SCEN_GAUNTLET_ASYNC_READY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_ASYNC_READY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_ASYNC_READY);
				this.m_NKC_SCEN_GAUNTLET_PRIVATE_READY = new NKC_SCEN_GAUNTLET_PRIVATE_READY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_PRIVATE_READY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_PRIVATE_READY);
				this.m_NKC_SCEN_GAUNTLET_LEAGUE_ROOM = new NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_LEAGUE_ROOM.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_LEAGUE_ROOM);
				this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM = new NKC_SCEN_GAUNTLET_PRIVATE_ROOM();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM);
				this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT = new NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT);
				this.m_NKC_SCEN_RAID = new NKC_SCEN_RAID();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_RAID.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_RAID);
				this.m_NKC_SCEN_RAID_READY = new NKC_SCEN_RAID_READY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_RAID_READY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_RAID_READY);
				this.m_NKC_SCEN_VOICE_LIST = new NKC_SCEN_VOICE_LIST();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_VOICE_LIST.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_VOICE_LIST);
				this.m_NKC_SCEN_SHADOW_PALACE = new NKC_SCEN_SHADOW_PALACE();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_SHADOW_PALACE.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_SHADOW_PALACE);
				this.m_NKC_SCEN_SHADOW_BATTLE = new NKC_SCEN_SHADOW_BATTLE();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_SHADOW_BATTLE.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_SHADOW_BATTLE);
				this.m_NKC_SCEN_SHADOW_RESULT = new NKC_SCEN_SHADOW_RESULT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_SHADOW_RESULT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_SHADOW_RESULT);
				this.m_NKC_SCEN_GUILD_INTRO = new NKC_SCEN_GUILD_INTRO();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GUILD_INTRO.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GUILD_INTRO);
				this.m_NKC_SCEN_GUILD_LOBBY = new NKC_SCEN_GUILD_LOBBY();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GUILD_LOBBY.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GUILD_LOBBY);
				this.m_NKC_SCEN_FIERCE_BATTLE_SUPPORT = new NKC_SCEN_FIERCE_BATTLE_SUPPORT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_FIERCE_BATTLE_SUPPORT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_FIERCE_BATTLE_SUPPORT);
				this.m_NKC_SCEN_GUILD_COOP = new NKC_SCEN_GUILD_COOP();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_GUILD_COOP.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_GUILD_COOP);
				this.m_NKC_SCEN_OFFICE = new NKC_SCEN_OFFICE();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_OFFICE.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_OFFICE);
				this.m_NKC_SCEN_TRIM = new NKC_SCEN_TRIM();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_TRIM.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_TRIM);
				this.m_NKC_SCEN_TRIM_RESULT = new NKC_SCEN_TRIM_RESULT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_TRIM_RESULT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_TRIM_RESULT);
				this.m_NKC_SCEN_DUNGEON_RESULT = new NKC_SCEN_DUNGEON_RESULT();
				this.m_dicSCEN.Add(this.m_NKC_SCEN_DUNGEON_RESULT.Get_NKM_SCEN_ID(), this.m_NKC_SCEN_DUNGEON_RESULT);
				Debug.Log("Scen Build Complete");
				this.m_NKM_UI_FPS = GameObject.Find("NKM_UI_FPS");
				if (this.m_NKM_UI_FPS != null)
				{
					UnityEngine.Object.Destroy(this.m_NKM_UI_FPS);
					this.m_NKM_UI_FPS = null;
				}
				NKCLogManager.Init();
				Debug.Log("[Game] Logmanager Init");
				Debug.Log("[Game] Aplication Version [" + Application.version + "]");
				Debug.Log("[Game] Aplication ProductName [" + Application.productName + "]");
				Debug.Log(string.Format("[Game] Aplication Platform [{0}]", Application.platform));
				Debug.Log("[Game] Aplication DataPath [" + Application.dataPath + "]");
				Debug.Log("[Game] Aplication persistentDataPath [" + Application.persistentDataPath + "]");
				NKCAdjustManager.Init();
				NKMPopUpBox.Init();
				Debug.Log("Obj Pool Init");
				this.GetObjectPool().Init();
				Debug.Log("Power Save Mode Init");
				this.InitPowerSaveMode();
				Debug.Log("Move to Login Scene");
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				Debug.Log("GameOptionData load");
				NKCGameOptionData gameOptionData = this.GetGameOptionData();
				if (gameOptionData != null)
				{
					Application.targetFrameRate = gameOptionData.GetFrameLimit();
				}
				if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.NexonPC)
				{
					this.m_NKCHealthyGame = new NKCHealthyGame();
					this.m_NKCHealthyGame.Start();
				}
				Debug.Log("Init Done!");
			}
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x00132D00 File Offset: 0x00130F00
		private void InitPowerSaveMode()
		{
			this.GetNKCPowerSaveMode().SetTurnOnEvent(delegate
			{
				NKCUIManager.GetNKCUIPowerSaveMode().Open();
				NKCUIManager.GetNKCUIPowerSaveMode().SetBlackScreen(false);
				NKCCamera.GetCamera().enabled = true;
				NKCCamera.GetSubUICamera().enabled = true;
				NKCCamera.SetBlackCameraEnable(false);
				NKCCamera.GetSubUICameraVideoPlayer().SetCamera(null);
			});
			this.GetNKCPowerSaveMode().SetTurnOffEvent(delegate
			{
				NKCUIManager.GetNKCUIPowerSaveMode().Close();
				NKCCamera.GetSubUICameraVideoPlayer().SetCamera(NKCCamera.GetSubUICamera());
			});
			this.GetNKCPowerSaveMode().SetMaxModeEvent(delegate
			{
				NKCUIManager.GetNKCUIPowerSaveMode().SetBlackScreen(true);
			});
			this.GetNKCPowerSaveMode().SetMaxModeNextFrameEvent(delegate
			{
				NKCCamera.GetCamera().enabled = false;
				NKCCamera.GetSubUICamera().enabled = false;
				NKCCamera.SetBlackCameraEnable(true);
			});
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00132DB8 File Offset: 0x00130FB8
		private void Update()
		{
			try
			{
				this.m_fFPSTime += (Time.deltaTime - this.m_fFPSTime) * 0.1f;
				NKCLogManager.Update();
				NKCReportManager.Update();
				if (this.m_UIInit)
				{
					NKCSynchronizedTime.Update(Time.unscaledDeltaTime);
					NKCCamera.Update(Time.deltaTime);
					NKCAssetResourceManager.Update();
					NKCMailManager.Update(Time.deltaTime);
					NKCCompanyBuffManager.Update(Time.deltaTime);
					if (this.m_NKCHealthyGame != null)
					{
						this.m_NKCHealthyGame.Update();
					}
					if (this.m_NKM_SCEN_NOW != null)
					{
						try
						{
							switch (this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE())
							{
							case NKC_SCEN_STATE.NSS_DATA_REQ_WAIT:
								this.ScenIsDataReqWait();
								break;
							case NKC_SCEN_STATE.NSS_LOADING_UI:
								this.ScenIsLoadingUI();
								break;
							case NKC_SCEN_STATE.NSS_LOADING_UI_COMPLETE_WAIT:
								this.ScenChangeUICompleteWait();
								break;
							case NKC_SCEN_STATE.NSS_LOADING:
								this.ScenIsLoading();
								break;
							case NKC_SCEN_STATE.NSS_LOADING_LAST:
								this.ScenIsLoadingLast();
								break;
							case NKC_SCEN_STATE.NSS_LOADING_COMPLETE_WAIT:
								this.ScenChangeCompleteWait();
								break;
							case NKC_SCEN_STATE.NSS_LOADING_COMPLETE:
								this.ScenChangeComplete();
								this.m_bScenChanging = false;
								break;
							case NKC_SCEN_STATE.NSS_START:
								this.m_NKM_SCEN_NOW.ScenUpdate();
								break;
							case NKC_SCEN_STATE.NSS_FAIL:
								this.ScenLoadFailed();
								break;
							}
						}
						catch (Exception ex) when (this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() != NKC_SCEN_STATE.NSS_START)
						{
							Debug.LogError("LoadGame Failed : Exception " + ex.Message + "\n" + ex.StackTrace);
							this.m_NKM_SCEN_NOW.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_INVALID);
							NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_FAILED, delegate()
							{
								Application.Quit();
							}, "");
							NKCPopupOKCancel.SetOnTop(false);
						}
					}
					this.ScenChangeFadeUpdate();
					this.GetObjectPool().Update();
					NKCUIManager.Update(Time.deltaTime);
					NKCPopupMessageManager.Update(Time.deltaTime);
					NKCSoundManager.Update(Time.deltaTime);
					this.m_NKCEffectManager.Update(Time.deltaTime);
					NKCMessage.Update();
					NKCLocalServerManager.Update(Time.deltaTime);
					this.m_NKCConnectLogin.Update();
					this.m_NKCConnectGame.Update();
					this.AppEnableConnectCheck();
					NKCGameEventManager.Update(Time.deltaTime);
					NKCLoginCutSceneManager.Update();
					NKCLoadingScreenManager.Update();
					this.GetTouch();
					if (this.GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
					{
						for (int i = 0; i < this.m_bTouch.Length; i++)
						{
							if (this.m_bTouch[i] && this.m_rtNUF_AFTER_UI_EFFECT != null && this.m_rtNUF_AFTER_UI_EFFECT.gameObject.activeInHierarchy)
							{
								Vector3 vector;
								RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_rtNUF_AFTER_UI_EFFECT, this.m_TouchPos2D[i], NKCCamera.GetSubUICamera(), out vector);
								vector.x /= this.m_rtNUF_AFTER_UI_EFFECT.lossyScale.x;
								vector.y /= this.m_rtNUF_AFTER_UI_EFFECT.lossyScale.y;
								this.m_NKCEffectManager.UseEffect(0, "AB_fx_ui_touch", "AB_fx_ui_touch", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_AFTER_UI_EFFECT, vector.x, vector.y, this.m_TouchPos3D[i].z + 10f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "BASE", 1f, false, false, 0f, -1f, false);
							}
						}
					}
					NKCAlarmManager.Update(Time.deltaTime);
					this.UpdatePowerSaveMode();
				}
			}
			catch (Exception ex2)
			{
				Debug.LogErrorFormat("NKCScenManager Update {0}", new object[]
				{
					ex2.Message
				});
				Debug.LogErrorFormat("NKCScenManager Update {0}", new object[]
				{
					ex2.StackTrace
				});
				int num = 1;
				Exception innerException = ex2.InnerException;
				while (innerException != null)
				{
					Debug.LogErrorFormat("NKCScenManager Update InnerException depth[{0}] {1}", new object[]
					{
						num,
						innerException
					});
					Debug.LogErrorFormat("NKCScenManager Update InnerException depth[{0}] {1}", new object[]
					{
						num,
						innerException.StackTrace
					});
					innerException = innerException.InnerException;
					num++;
				}
				this.ScenLoadFailed();
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x001331F8 File Offset: 0x001313F8
		private void InitTextureCamera()
		{
			if (this.TextureCamera == null)
			{
				this.m_objTextureCamera = new GameObject("TextureImageCamera");
				if (NKCScenManager.GetScenManager() != null)
				{
					this.m_objTextureCamera.transform.SetParent(base.transform, false);
				}
				this.m_objTextureCamera.SetActive(false);
				this.TextureCamera = this.m_objTextureCamera.AddComponent<Camera>();
				this.TextureCamera.enabled = false;
				this.TextureCamera.orthographic = true;
				this.TextureCamera.cullingMask = int.MinValue;
				this.TextureCamera.clearFlags = CameraClearFlags.Color;
				this.TextureCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x001332C0 File Offset: 0x001314C0
		public void TextureCapture(Renderer targetRenderer, Bounds bound, ref RenderTexture Texture)
		{
			if (bound.size.x == 0f || bound.size.y == 0f)
			{
				return;
			}
			Vector3 vector = bound.center - bound.extents;
			Vector3 vector2 = bound.center + bound.extents;
			Vector3[] worldCornerPosArray = new Vector3[]
			{
				vector,
				new Vector3(vector.x, vector2.y, vector.z),
				vector2,
				new Vector3(vector2.x, vector.y, vector.z)
			};
			NKCCamera.FitCameraToWorldRect(this.TextureCamera, worldCornerPosArray);
			int layer = targetRenderer.gameObject.layer;
			targetRenderer.gameObject.layer = 31;
			this.TextureCamera.targetTexture = Texture;
			this.TextureCamera.Render();
			targetRenderer.gameObject.layer = layer;
			this.TextureCamera.targetTexture = null;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x001333C4 File Offset: 0x001315C4
		public void ForceRender(Renderer targetRenderer)
		{
			if (targetRenderer == null)
			{
				return;
			}
			Bounds bounds = targetRenderer.bounds;
			if (this.m_tempTexture == null)
			{
				this.m_tempTexture = new RenderTexture(128, 128, 0, RenderTextureFormat.ARGB32);
				this.m_tempTexture.wrapMode = TextureWrapMode.Clamp;
				this.m_tempTexture.antiAliasing = 1;
				this.m_tempTexture.filterMode = FilterMode.Bilinear;
				this.m_tempTexture.anisoLevel = 0;
				this.m_tempTexture.Create();
			}
			this.TextureCapture(targetRenderer, bounds, ref this.m_tempTexture);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x00133454 File Offset: 0x00131654
		public void ForceRender(Texture targetTexture)
		{
			if (targetTexture == null)
			{
				return;
			}
			if (this.tempMeshRenderer == null)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
				gameObject.name = "LoadingTempMeshRenderer";
				gameObject.transform.localScale = new Vector3(256f, 256f, 1f);
				this.tempMeshRenderer = gameObject.GetComponent<MeshRenderer>();
				NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<Material>("AB_MATERIAL", "MAT_NKC_AFTERIMAGE", false, null);
				this.tempMeshRenderer.material = nkcassetResourceData.GetAsset<Material>();
			}
			this.tempMeshRenderer.gameObject.SetActive(true);
			this.tempMeshRenderer.material.mainTexture = targetTexture;
			this.ForceRender(this.tempMeshRenderer);
			this.tempMeshRenderer.gameObject.SetActive(false);
			this.tempMeshRenderer.material.mainTexture = null;
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x0013352C File Offset: 0x0013172C
		public void PreloadSprite(string bundleName, string assetName)
		{
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(bundleName, assetName, false);
			if (orLoadAssetResource == null)
			{
				return;
			}
			this.ForceRender(orLoadAssetResource.texture);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00133558 File Offset: 0x00131758
		public void PreloadTexture(string bundleName, string assetName)
		{
			Texture orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Texture>(bundleName, assetName, false);
			if (orLoadAssetResource == null)
			{
				return;
			}
			this.ForceRender(orLoadAssetResource);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x0013357F File Offset: 0x0013177F
		private void UpdatePowerSaveMode()
		{
			if (Input.anyKey)
			{
				this.m_NKCPowerSaveMode.SetLastKeyInputTime(Time.time);
			}
			this.m_NKCPowerSaveMode.Update(Time.time);
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x001335A8 File Offset: 0x001317A8
		private void FPSView()
		{
			if (this.m_NKM_UI_FPS.activeSelf)
			{
				this.m_NKM_UI_FPS.SetActive(false);
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x001335CE File Offset: 0x001317CE
		public void SetActionAfterScenChange(NKCScenManager.DoAfterScenChange action)
		{
			this.dDoAfterScenChange = action;
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x001335D7 File Offset: 0x001317D7
		public void SetActiveLoadingUI(NKCLoadingScreenManager.eGameContentsType contentType, int contentValue = 0)
		{
			NKCUIManager.LoadingUI.ShowMainLoadingUI(contentType, contentValue, 0);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x001335E6 File Offset: 0x001317E6
		public void CloseLoadingUI()
		{
			NKCUIManager.LoadingUI.CloseLoadingUI();
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x001335F2 File Offset: 0x001317F2
		private IEnumerator CoroutineScenChangeFade()
		{
			while (!NKCUIFadeInOut.IsFinshed())
			{
				yield return null;
			}
			this.ScenChangeImpl(this.m_eNextScen);
			this.m_eNextScen = NKM_SCEN_ID.NSI_INVALID;
			yield break;
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x00133601 File Offset: 0x00131801
		public void ScenReload()
		{
			this.ScenChangeImpl(this.m_NKM_SCEN_NOW.Get_NKM_SCEN_ID());
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x00133614 File Offset: 0x00131814
		private void ScenChangeImpl(NKM_SCEN_ID eScenID)
		{
			if (!this.m_dicSCEN.ContainsKey(eScenID))
			{
				Debug.LogErrorFormat("m_dicSCEN has NO eScenID: {0}", new object[]
				{
					eScenID.ToString()
				});
				return;
			}
			Debug.Log("ScenChangeImpl Begin");
			if (this.m_NKM_SCEN_NOW != null)
			{
				this.m_NKM_SCEN_NOW.ScenEnd();
				this.m_BeforeScenID = this.m_NKM_SCEN_NOW.Get_NKM_SCEN_ID();
			}
			Debug.Log("ScenChangeImpl ScenEnd Complete");
			this.m_NKM_SCEN_NOW = this.m_dicSCEN[eScenID];
			if (eScenID == NKM_SCEN_ID.NSI_GAME)
			{
				NKCUIManager.OnScenEnd(NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME);
			}
			else
			{
				NKCUIManager.OnScenEnd(NKCUIManager.eUIUnloadFlag.DEFAULT);
				if (this.tempMeshRenderer != null)
				{
					UnityEngine.Object.Destroy(this.tempMeshRenderer.gameObject);
					this.tempMeshRenderer = null;
				}
			}
			Debug.Log("ScenChangeImpl : Unload All UI Complete");
			this.m_NKM_SCEN_NOW.ScenChangeStart();
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x001336E6 File Offset: 0x001318E6
		public bool CheckSceneChangeEnabled(NKM_SCEN_ID eScenID)
		{
			if ((eScenID == NKM_SCEN_ID.NSI_HOME || eScenID == NKM_SCEN_ID.NSI_OPERATION) && NKCPatchUtility.GetDownloadType() == NKCPatchDownloader.DownType.TutorialWithBackground)
			{
				NKCPatchUtility.RemoveDownloadType();
				this.ShowNeedUpdateAfterTutorial();
				return false;
			}
			return true;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x00133708 File Offset: 0x00131908
		public void ScenChangeFade(NKM_SCEN_ID eScenID, bool bForce = true)
		{
			if (!this.CheckSceneChangeEnabled(eScenID))
			{
				return;
			}
			if (this.GetNowScenID() == eScenID && !bForce)
			{
				NKCScenManager.DoAfterScenChange doAfterScenChange = this.dDoAfterScenChange;
				if (doAfterScenChange != null)
				{
					doAfterScenChange();
				}
				this.dDoAfterScenChange = null;
				return;
			}
			NKCScenChangeOrder nkcscenChangeOrder = new NKCScenChangeOrder();
			nkcscenChangeOrder.m_NextScen = eScenID;
			nkcscenChangeOrder.m_bForce = bForce;
			this.m_qScenChange.Enqueue(nkcscenChangeOrder);
			if (!this.m_bSkipScenChangeFadeOutEffect)
			{
				NKCUIFadeInOut.FadeOut(0.1f, null, false, 7f);
			}
			this.m_bSkipScenChangeFadeOutEffect = false;
			NKCUIVoiceManager.StopVoice();
			NKCUIManager.NKCUIOverlayCaption.CloseAllCaption();
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x00133794 File Offset: 0x00131994
		private void ScenChangeFadeUpdate()
		{
			if (this.m_bScenChanging)
			{
				return;
			}
			if (this.m_qScenChange.Count <= 0)
			{
				return;
			}
			if (this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() != NKC_SCEN_STATE.NSS_START)
			{
				return;
			}
			Debug.Log("ScenChangeFadeUpdate Begin");
			NKCScenChangeOrder nkcscenChangeOrder = this.m_qScenChange.Dequeue();
			if (this.GetNowScenID() == nkcscenChangeOrder.m_NextScen && !nkcscenChangeOrder.m_bForce)
			{
				return;
			}
			Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.High;
			if (nkcscenChangeOrder.m_NextScen == NKM_SCEN_ID.NSI_GAME)
			{
				this.Get_SCEN_GAME().ScenClear();
			}
			if (nkcscenChangeOrder.m_NextScen == NKM_SCEN_ID.NSI_GAME || this.GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKMGameData nkmgameData = this.GetGameClient().GetGameDataDummy();
				if (nkmgameData == null)
				{
					nkmgameData = this.GetGameClient().GetGameData();
				}
				if (nkmgameData != null)
				{
					NKM_GAME_TYPE gameType = nkmgameData.GetGameType();
					if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
					{
						if (gameType != NKM_GAME_TYPE.NGT_DIVE)
						{
							NKCUIManager.LoadingUI.ShowMainLoadingUI(nkmgameData.GetGameType(), nkmgameData.m_DungeonID);
						}
						else
						{
							NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
							NKMDiveGameData nkmdiveGameData = (nkmuserData != null) ? nkmuserData.m_DiveGameData : null;
							if (nkmdiveGameData != null && nkmdiveGameData.Floor != null && nkmdiveGameData.Floor.Templet != null)
							{
								NKCUIManager.LoadingUI.ShowMainLoadingUI(NKM_GAME_TYPE.NGT_DIVE, nkmdiveGameData.Floor.Templet.StageID);
							}
							else
							{
								NKCUIManager.LoadingUI.ShowMainLoadingUI(NKM_GAME_TYPE.NGT_DIVE, nkmgameData.m_DungeonID);
							}
						}
					}
					else
					{
						NKCUIManager.LoadingUI.ShowMainLoadingUI(NKM_GAME_TYPE.NGT_WARFARE, nkmgameData.m_WarfareID);
					}
				}
				else
				{
					NKCUIManager.LoadingUI.ShowMainLoadingUI(NKCLoadingScreenManager.eGameContentsType.DEFAULT, 0, 0);
				}
				this.ScenChangeImpl(nkcscenChangeOrder.m_NextScen);
				NKCUIFadeInOut.Finish();
			}
			else if (this.GetNowScenID() == NKM_SCEN_ID.NSI_LOGIN && nkcscenChangeOrder.m_NextScen == NKM_SCEN_ID.NSI_HOME)
			{
				NKCUIManager.LoadingUI.ShowMainLoadingUI(NKCLoadingScreenManager.eGameContentsType.DEFAULT, 0, 0);
				this.ScenChangeImpl(nkcscenChangeOrder.m_NextScen);
				NKCUIFadeInOut.Finish();
			}
			else
			{
				this.m_eNextScen = nkcscenChangeOrder.m_NextScen;
				base.StartCoroutine(this.CoroutineScenChangeFade());
				NKCUIManager.LoadingUI.CloseLoadingUI();
			}
			this.m_bScenChanging = true;
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x00133965 File Offset: 0x00131B65
		public void ScenIsDataReqWait()
		{
			if (this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_DATA_REQ_WAIT)
			{
				this.m_NKM_SCEN_NOW.ScenDataReqWaitUpdate();
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x00133988 File Offset: 0x00131B88
		public void ScenIsLoadingUI()
		{
			if (this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_LOADING_UI)
			{
				this.m_NKM_SCEN_NOW.ScenLoadUIUpdate();
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x001339AB File Offset: 0x00131BAB
		public void ScenChangeUICompleteWait()
		{
			if (this.m_NKM_SCEN_NOW != null)
			{
				this.m_NKM_SCEN_NOW.ScenLoadUICompleteWait();
			}
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x001339C0 File Offset: 0x00131BC0
		public void ScenIsLoading()
		{
			if (this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_LOADING)
			{
				this.m_NKM_SCEN_NOW.ScenLoadUpdate();
			}
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x001339E3 File Offset: 0x00131BE3
		public void ScenIsLoadingLast()
		{
			if (this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_LOADING_LAST)
			{
				this.m_NKM_SCEN_NOW.ScenLoadLastUpdate();
			}
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x00133A06 File Offset: 0x00131C06
		public void ScenChangeCompleteWait()
		{
			if (this.m_NKM_SCEN_NOW != null)
			{
				this.m_NKM_SCEN_NOW.ScenLoadCompleteWait();
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x00133A1C File Offset: 0x00131C1C
		public void ScenLoadFailed()
		{
			if (this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() != NKC_SCEN_STATE.NSS_START)
			{
				this.m_bScenChanging = false;
				this.m_BeforeScenID = this.m_NKM_SCEN_NOW.Get_NKM_SCEN_ID();
				this.m_NKM_SCEN_NOW.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_START);
				if (this.m_BeforeScenID == NKM_SCEN_ID.NSI_SHOP)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, delegate()
					{
						this.ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
					}, "");
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString("SI_ERROR_DEFAULT_MESSAGE", false), delegate()
				{
					this.ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
			}
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00133ABC File Offset: 0x00131CBC
		public void ScenChangeComplete()
		{
			if (this.m_NKCMemoryCleaner != null)
			{
				this.m_NKCMemoryCleaner.DoUnloadUnusedAssetsAndGC();
			}
			NKCUIManager.LoadingUI.CloseLoadingUI();
			this.m_NKM_SCEN_NOW.ScenStart();
			if (this.m_CurScenID != this.m_NKM_SCEN_NOW.Get_NKM_SCEN_ID() && this.m_BeforeScenID != NKM_SCEN_ID.NSI_INVALID && this.IsReconnectScen())
			{
				this.m_CurScenID = this.m_NKM_SCEN_NOW.Get_NKM_SCEN_ID();
				if (this.GetConnectGame().IsConnected)
				{
					NKCPacketSender.Send_NKMPacket_UI_SCEN_CHANGED_REQ(this.m_CurScenID);
				}
				Log.Info(string.Concat(new string[]
				{
					"<color=#FFFF00FF>ScenChangeInfo: BeforeScenID : ",
					this.m_BeforeScenID.ToString(),
					" > AfterScenID : ",
					this.m_CurScenID.ToString(),
					"</color>"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCScenManager.cs", 1357);
			}
			NKCUIOverlayPatchProgress.OpenWhenDownloading();
			NKCScenManager.DoAfterScenChange doAfterScenChange = this.dDoAfterScenChange;
			if (doAfterScenChange != null)
			{
				doAfterScenChange();
			}
			this.dDoAfterScenChange = null;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x00133BC4 File Offset: 0x00131DC4
		private void GetTouch()
		{
			for (int i = 0; i < this.m_bTouch.Length; i++)
			{
				this.m_bTouch[i] = false;
				if (Input.touchCount > i && Input.GetTouch(i).phase == TouchPhase.Began)
				{
					this.m_TouchPos2D[i] = Input.GetTouch(i).position;
					NKCCamera.GetScreenPosToWorldPos(out this.m_TouchPos3D[i], this.m_TouchPos2D[i].x, this.m_TouchPos2D[i].y);
					this.m_bTouch[i] = true;
				}
				else if (Input.GetMouseButtonDown(i))
				{
					this.m_TouchPos2D[i] = Input.mousePosition;
					NKCCamera.GetScreenPosToWorldPos(out this.m_TouchPos3D[i], this.m_TouchPos2D[i].x, this.m_TouchPos2D[i].y);
					this.m_bTouch[i] = true;
				}
			}
			if (Input.touchCount == 2)
			{
				this.m_bHasPinch = true;
				Touch touch = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				Vector2 a = touch.position - touch.deltaPosition;
				Vector2 b = touch2.position - touch2.deltaPosition;
				float sqrMagnitude = (a - b).sqrMagnitude;
				float sqrMagnitude2 = (touch.position - touch2.position).sqrMagnitude;
				float num = 1f / (float)(Screen.width * Screen.width);
				this.m_fPinchDeltaMagnitude = (sqrMagnitude2 - sqrMagnitude) * num;
				this.m_vPinchCenter = (touch.position + touch2.position) * 0.5f;
				return;
			}
			this.m_bHasPinch = false;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x00133D84 File Offset: 0x00131F84
		public bool MsgProc(NKCMessageData cNKCMessageData)
		{
			switch (cNKCMessageData.m_NKC_EVENT_MESSAGE)
			{
			case NKC_EVENT_MESSAGE.NEM_NKCPACKET_SEND_TO_SERVER:
				if (NKCLocalServerManager.ScenMsgProc(cNKCMessageData))
				{
					return true;
				}
				break;
			case NKC_EVENT_MESSAGE.NEM_NKCPACKET_SEND_TO_CLIENT:
				if (NKCLocalPacketHandler.ScenMsgProc(cNKCMessageData))
				{
					return true;
				}
				break;
			case NKC_EVENT_MESSAGE.NEM_UI_GAME_QUIT:
				if (Application.platform == RuntimePlatform.Android)
				{
					Application.Quit();
				}
				break;
			}
			return this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.ScenMsgProc(cNKCMessageData);
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x00133DEB File Offset: 0x00131FEB
		private void OnHideUnity(bool isGameShown)
		{
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x00133DF0 File Offset: 0x00131FF0
		private void OnLowMemory()
		{
			Debug.LogError("OnLowMemory!!");
			if (this.m_NKM_SCEN_NOW == null)
			{
				return;
			}
			if (this.GetNowScenID() != NKM_SCEN_ID.NSI_GAME && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START)
			{
				this.GetGameClient().Init();
				if (this.m_NKCMemoryCleaner != null)
				{
					this.m_NKCMemoryCleaner.Clean(null, NKCUIManager.eUIUnloadFlag.ONLY_MEMORY_SHORTAGE, true);
				}
			}
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00133E4F File Offset: 0x0013204F
		private static void OnLogReceived(string msg, string stackTrace, LogType type)
		{
			if (type == LogType.Exception && NKCUIManager.CheckUIOpenError())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_UI_LOADING_ERROR, null, "");
			}
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00133E74 File Offset: 0x00132074
		private void AppEnableConnectCheck()
		{
			if (NKCDefineManager.DEFINE_NX_PC() && this.GetNowScenID() == NKM_SCEN_ID.NSI_LOGIN)
			{
				return;
			}
			if (this.m_fAppEnableConnectCheckTime == -1f)
			{
				return;
			}
			if (this.m_fAppEnableConnectCheckTime > 0f)
			{
				float deltaTime = Time.deltaTime;
				this.m_fAppEnableConnectCheckTime -= deltaTime;
				if (this.m_fAppEnableConnectCheckTime <= 0f)
				{
					Log.Info("[AppEnableConnectCheck] VersionCheck", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCScenManager.cs", 1561);
					this.m_fAppEnableConnectCheckTime = -1f;
					this.VersionCheck(new UnityAction(this.m_NKCConnectGame.Reconnect));
				}
			}
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00133F04 File Offset: 0x00132104
		public void SetApplicaitonPause(bool bPause)
		{
			this.OnApplicationPause(bPause);
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x00133F10 File Offset: 0x00132110
		private void OnApplicationPause(bool pauseStatus)
		{
			Debug.Log("OnApplicationPause " + pauseStatus.ToString());
			if (!pauseStatus)
			{
				if (this.IsReconnectScen() && (this.GetNowScenID() != NKM_SCEN_ID.NSI_GAME || (this.GetGameClient().GetGameData() != null && this.GetGameClient().GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_DEV)))
				{
					NKCPacketSender.Send_NKMPacket_NKMPacket_CONNECT_CHECK_REQ();
					this.m_fAppEnableConnectCheckTime = 2f;
				}
				if (this.GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
				{
					this.GetGameClient().GetGameHud().UseCompleteDeck();
					if (this.GetGameClient().GetGameData() != null && this.GetGameClient().GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
					{
						this.GetGameClient().Send_Packet_GAME_CHECK_DIE_UNIT_REQ();
					}
				}
				NKCUIFadeInOut.Close(true);
				return;
			}
			if (this.GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				if (this.GetGameClient().GetGameData() != null && this.GetGameClient().GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
				{
					this.Get_SCEN_GAME().TrySendGamePauseEnableREQ();
					return;
				}
			}
			else
			{
				if (this.GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					this.Get_NKC_SCEN_WARFARE_GAME().TryPause();
					return;
				}
				if (this.GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
				{
					this.Get_SCEN_HOME().TryPause();
				}
			}
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x00134024 File Offset: 0x00132224
		private void OnApplicationFocus(bool focus)
		{
			if (!NKCUIJukeBox.IsHasInstance || !NKCUIJukeBox.IsInstanceOpen)
			{
				NKCSoundManager.SetMute(this.GetGameOptionData().SoundMute, false);
			}
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00134045 File Offset: 0x00132245
		private void CheckForCameraPermission()
		{
			if (this.CameraPermissionCoroutine != null)
			{
				base.StopCoroutine(this.CameraPermissionCoroutine);
				this.CameraPermissionCoroutine = null;
			}
			this.CameraPermissionCoroutine = base.StartCoroutine(this.CameraPermission());
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x00134074 File Offset: 0x00132274
		private IEnumerator CameraPermission()
		{
			yield return null;
			yield return null;
			yield return null;
			yield return null;
			yield return null;
			if (NKCPublisherModule.Permission != null)
			{
				NKCPublisherModule.Permission.CheckCameraPermission();
			}
			yield break;
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x0013407C File Offset: 0x0013227C
		private void OnApplicationQuit()
		{
			Debug.Log("[OnApplicationQuit] NKCScenManager - Quit Start");
			NKCMain.InvalidateSafeMode();
			Debug.Log("[OnApplicationQuit] InvalidSafeMode");
			this.GetConnectLogin().ResetConnection();
			Debug.Log("[OnApplicationQuit] ResetConnection");
			this.GetConnectGame().ResetConnection();
			Debug.Log("[OnApplicationQuit] ResetConnection");
			if (NKCPatchDownloader.Instance != null)
			{
				NKCPatchDownloader.Instance.Unload();
				Debug.Log("[OnApplicationQuit] NKCPatchDownloader Unload");
			}
			Debug.Log("[OnApplicationQuit] NKCScenManager - Quit Complete");
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x001340F8 File Offset: 0x001322F8
		public void DoAfterLogout()
		{
			if (this.m_NKCRepeatOperaion != null)
			{
				this.m_NKCRepeatOperaion.Init();
				this.m_NKCRepeatOperaion.SetAlarmRepeatOperationQuitByDefeat(false);
				this.m_NKCRepeatOperaion.SetAlarmRepeatOperationSuccess(false);
			}
			NKCUIUserInfo.SetComment("");
			this.GetNKCSurveyMgr().Clear();
			this.Get_SCEN_HOME().DoAfterLogout();
			this.Get_NKC_SCEN_WORLDMAP().DoAfterLogout();
			this.Get_NKC_SCEN_DIVE().DoAfterLogout();
			this.Get_SCEN_EPISODE().DoAfterLogout();
			this.Get_NKC_SCEN_WARFARE_GAME().DoAfterLogout();
			this.Get_NKC_SCEN_GAUNTLET_LOBBY().DoAfterLogout();
			this.Get_SCEN_DUNGEON_ATK_READY().DoAfterLogout();
			this.Get_NKC_SCEN_DIVE_READY().DoAfterLogout();
			this.Get_NKC_SCEN_RAID_READY().DoAfterLogout();
			this.Get_SCEN_OPERATION().DoAfterLogOut();
			NKCChatManager.Initialize();
			NKCGuildManager.Initialize();
			NKCGuildCoopManager.Initialize();
			NKMEpisodeMgr.DoAfterLogOut();
			NKCFriendManager.Initialize();
			NKCPhaseManager.Reset();
			NKCTrimManager.Reset();
			NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(-1f, true);
			NKCUnitReviewManager.m_bReceivedUnitReviewBanList = false;
			NKCEmoticonManager.m_bReceivedEmoticonData = false;
			this.GetConnectGame().SetReconnectKey("");
			NKCPMNexonNGS.SetNpaCode("");
			NKCUIGameOption.InvalidShowHiddenOption();
			NKCUIEventSubUIWechatFollow.DoAfterLogout();
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00134218 File Offset: 0x00132418
		public bool IsReconnectScen()
		{
			NKM_SCEN_ID nowScenID = this.GetNowScenID();
			return nowScenID > NKM_SCEN_ID.NSI_LOGIN && nowScenID != NKM_SCEN_ID.NSI_CUTSCENE_SIM && nowScenID != NKM_SCEN_ID.NSI_VOICE_LIST;
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x0013423C File Offset: 0x0013243C
		public void VersionCheck(UnityAction onSuccess)
		{
			if (this.m_bVersionCheckRunning)
			{
				return;
			}
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCUtilString.GET_STRING_DECONNECT_INTERNET, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				}, "");
			}
			if (NKCPatchDownloader.Instance.ProloguePlay)
			{
				if (onSuccess != null)
				{
					onSuccess();
				}
				return;
			}
			base.StartCoroutine(this._VersionCheckAndReconnect(onSuccess));
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x001342B0 File Offset: 0x001324B0
		private IEnumerator _VersionCheckAndReconnect(UnityAction onSuccess)
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			this.m_bVersionCheckRunning = true;
			Debug.Log("Checking Assetbundle Version");
			NKCPatchDownloader.Instance.CheckVersion(new List<string>(AssetBundleManager.ActiveVariants), false);
			while (NKCPatchDownloader.Instance.BuildCheckStatus == NKCPatchDownloader.BuildStatus.Unchecked)
			{
				yield return null;
			}
			NKCPatchDownloader.BuildStatus buildCheckStatus = NKCPatchDownloader.Instance.BuildCheckStatus;
			if (buildCheckStatus == NKCPatchDownloader.BuildStatus.UpdateAvailable)
			{
				NKMPopUpBox.CloseWaitBox();
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_PATCHER_CAN_UPDATE, delegate()
				{
					this.m_bVersionCheckRunning = false;
					this.MoveToMarket();
				}, delegate()
				{
					this.m_bVersionCheckRunning = false;
					onSuccess();
				}, false);
				yield break;
			}
			if (buildCheckStatus == NKCPatchDownloader.BuildStatus.RequireAppUpdate)
			{
				this.m_bVersionCheckRunning = false;
				NKMPopUpBox.CloseWaitBox();
				this.ShowAppUpdate();
				yield break;
			}
			while (NKCPatchDownloader.Instance.VersionCheckStatus == NKCPatchDownloader.VersionStatus.Unchecked)
			{
				yield return null;
			}
			if (NKCPatchDownloader.Instance.VersionCheckStatus != NKCPatchDownloader.VersionStatus.UpToDate)
			{
				this.m_bVersionCheckRunning = false;
				NKMPopUpBox.CloseWaitBox();
				this.ShowBundleUpdate(false);
				yield break;
			}
			NKMPopUpBox.CloseWaitBox();
			yield return NKCPatcherManager.GetPatcherManager().UpdateServerMaintenanceData();
			if (NKCConnectionInfo.IsServerUnderMaintenance())
			{
				bool bWait = true;
				NKCPublisherModule.OnComplete <>9__3;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_PATCHER_NOTICE, NKCStringTable.GetString("SI_SYSTEM_NOTICE_MAINTENANCE_DESC", false), delegate()
				{
					NKCPopupNoticeWeb instance = NKCPopupNoticeWeb.Instance;
					string url = NKCPublisherModule.Notice.NoticeUrl(true);
					NKCPublisherModule.OnComplete onWindowClosed;
					if ((onWindowClosed = <>9__3) == null)
					{
						onWindowClosed = (<>9__3 = delegate(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
						{
							bWait = false;
						});
					}
					instance.Open(url, onWindowClosed, true);
				}, "");
				while (bWait)
				{
					yield return null;
				}
				this.m_bVersionCheckRunning = false;
				yield break;
			}
			UnityAction onSuccess2 = onSuccess;
			if (onSuccess2 != null)
			{
				onSuccess2();
			}
			this.m_bVersionCheckRunning = false;
			yield break;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x001342C8 File Offset: 0x001324C8
		public void MoveToPatchScene()
		{
			NKCMain.InvalidateSafeMode();
			if (this.m_NKM_SCEN_NOW != null)
			{
				this.m_NKM_SCEN_NOW.ScenEnd();
			}
			NKCSoundManager.Unload();
			NKCUIOverlayPatchProgress.CheckInstanceAndClose();
			NKCTempletUtility.CleanupAllTemplets();
			NKCAssetResourceManager.UnloadAllResources();
			AssetBundleManager.UnloadAllAndCleanup();
			if (this.m_NKCMemoryCleaner != null)
			{
				this.m_NKCMemoryCleaner.Clean(delegate
				{
					SceneManager.LoadSceneAsync("NKM_SCEN_PATCHER", LoadSceneMode.Single);
				}, NKCUIManager.eUIUnloadFlag.NEVER_UNLOAD, false);
				return;
			}
			SceneManager.LoadSceneAsync("NKM_SCEN_PATCHER", LoadSceneMode.Single);
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x00134350 File Offset: 0x00132550
		public void ShowBundleUpdate(bool bCallFromTutorial)
		{
			if (NKCPublisherModule.IsSteamPC())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REFRESH_DATA, delegate()
				{
					Application.Quit();
				}, "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REFRESH_DATA, new NKCPopupOKCancel.OnButton(this.MoveToPatchScene), "");
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x001343B8 File Offset: 0x001325B8
		public void ShowNeedUpdateAfterTutorial()
		{
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_PATCHER_NEED_UPDATE_AFTER_TUTORIAL", false), new NKCPopupOKCancel.OnButton(this.MoveToPatchScene), "");
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x001343E0 File Offset: 0x001325E0
		private void MoveToMarket()
		{
			if (NKCPatchDownloader.Instance != null)
			{
				NKCPatchDownloader.Instance.MoveToMarket();
				return;
			}
			Application.Quit();
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x001343FF File Offset: 0x001325FF
		private void ShowAppUpdate()
		{
			this.m_bVersionCheckRunning = false;
			base.StopAllCoroutines();
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_PATCHER_NEED_UPDATE, new NKCPopupOKCancel.OnButton(this.MoveToMarket), "");
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x0013442E File Offset: 0x0013262E
		public void OnLoginReady()
		{
			if (this.GetConnectLogin() != null)
			{
				this.GetConnectLogin().AuthToLoginServer();
				NKMPopUpBox.OpenWaitBox(0f, "");
			}
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x00134452 File Offset: 0x00132652
		public void SetAspectRatio(ValueTuple<int, int> ratio)
		{
			this.SetAspectRatio(ratio.Item1, ratio.Item2);
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x00134466 File Offset: 0x00132666
		public void SetAspectRatio(int x, int y)
		{
			Debug.Log(string.Format("SetAspectRatio {0}:{1}", x, y));
			AspectRatioController.SetAspectRatio(x, y);
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x0013448C File Offset: 0x0013268C
		public bool ProcessGlobalHotkey(HotkeyEventType eventtype)
		{
			if (eventtype == HotkeyEventType.Cancel)
			{
				if (!NKMPopUpBox.IsOpenedWaitBox() && !this.GetNKCPowerSaveMode().GetEnable() && this.m_NKM_SCEN_NOW != null && this.m_NKM_SCEN_NOW.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START)
				{
					NKCUIManager.OnBackButton();
				}
				return true;
			}
			if (eventtype != HotkeyEventType.ScreenCapture)
			{
				if (eventtype == HotkeyEventType.Mute)
				{
					this.m_GameOptionData.SoundMute = !this.m_GameOptionData.SoundMute;
					this.m_GameOptionData.Save();
					if (NKCUIGameOption.IsInstanceOpen)
					{
						NKCUIGameOption.Instance.UpdateOptionContent(NKCUIGameOption.GameOptionGroup.Sound);
					}
				}
				return false;
			}
			if (NKCScreenCaptureUtility.CaptureScreen())
			{
				NKCSoundManager.PlaySound("FX_CUTSCEN_NOISE_CAMERA", 1f, 0f, 0f, false, 0f, false, 0f);
			}
			return true;
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x00134544 File Offset: 0x00132744
		public void ProcessGlobalHotkeyHold(HotkeyEventType eventType)
		{
			if (eventType != HotkeyEventType.MasterVolumeUp)
			{
				if (eventType != HotkeyEventType.MasterVolumeDown)
				{
					return;
				}
				this.m_GameOptionData.ChangeSoundVolume(NKC_GAME_OPTION_SOUND_GROUP.ALL, -2);
				this.m_GameOptionData.Save();
				NKCSoundManager.SetAllVolume(this.m_GameOptionData.GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP.ALL));
				if (NKCUIGameOption.IsInstanceOpen)
				{
					NKCUIGameOption.Instance.UpdateOptionContent(NKCUIGameOption.GameOptionGroup.Sound);
				}
			}
			else
			{
				this.m_GameOptionData.ChangeSoundVolume(NKC_GAME_OPTION_SOUND_GROUP.ALL, 2);
				this.m_GameOptionData.Save();
				NKCSoundManager.SetAllVolume(this.m_GameOptionData.GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP.ALL));
				if (NKCUIGameOption.IsInstanceOpen)
				{
					NKCUIGameOption.Instance.UpdateOptionContent(NKCUIGameOption.GameOptionGroup.Sound);
					return;
				}
			}
		}

		// Token: 0x0400356A RID: 13674
		private static bool m_bApplicationDelegateRegistered;

		// Token: 0x0400356B RID: 13675
		private static NKCScenManager m_ScenManager;

		// Token: 0x0400356C RID: 13676
		private int m_SystemMemorySize;

		// Token: 0x0400356D RID: 13677
		public NKCSystemEvent m_NKCSystemEvent;

		// Token: 0x0400356E RID: 13678
		public NKCMemoryCleaner m_NKCMemoryCleaner;

		// Token: 0x0400356F RID: 13679
		private bool m_UIInit;

		// Token: 0x04003570 RID: 13680
		private GameObject m_NKM_UI_FPS;

		// Token: 0x04003571 RID: 13681
		private Text m_NKM_UI_FPS_Text;

		// Token: 0x04003572 RID: 13682
		private NKM_SCEN_ID m_eNextScen;

		// Token: 0x04003573 RID: 13683
		private Dictionary<NKM_SCEN_ID, NKC_SCEN_BASIC> m_dicSCEN = new Dictionary<NKM_SCEN_ID, NKC_SCEN_BASIC>();

		// Token: 0x04003574 RID: 13684
		private NKC_SCEN_BASIC m_NKM_SCEN_NOW;

		// Token: 0x04003575 RID: 13685
		private NKC_SCEN_LOGIN m_NKC_SCEN_LOGIN;

		// Token: 0x04003576 RID: 13686
		private NKC_SCEN_HOME m_NKC_SCEN_HOME;

		// Token: 0x04003577 RID: 13687
		private NKC_SCEN_GAME m_NKC_SCEN_GAME;

		// Token: 0x04003578 RID: 13688
		private NKC_SCEN_TEAM m_NKC_SCEN_TEAM;

		// Token: 0x04003579 RID: 13689
		private NKC_SCEN_BASE m_NKC_SCEN_BASE;

		// Token: 0x0400357A RID: 13690
		private NKC_SCEN_CONTRACT m_NKC_SCEN_CONTRACT;

		// Token: 0x0400357B RID: 13691
		private NKC_SCEN_INVENTORY m_NKC_SCEN_INVENTORY;

		// Token: 0x0400357C RID: 13692
		private NKC_SCEN_CUTSCEN_SIM m_NKC_SCEN_CUTSCEN_SIM;

		// Token: 0x0400357D RID: 13693
		private NKC_SCEN_OPERATION_V2 m_NKC_SCEN_OPERATION_V2;

		// Token: 0x0400357E RID: 13694
		private NKC_SCEN_EPISODE m_NKC_SCEN_EPISODE;

		// Token: 0x0400357F RID: 13695
		private NKC_SCEN_DUNGEON_ATK_READY m_NKC_SCEN_DUNGEON_ATK_READY;

		// Token: 0x04003580 RID: 13696
		private NKC_SCEN_UNIT_LIST m_NKC_SCEN_UNIT_LIST;

		// Token: 0x04003581 RID: 13697
		private NKC_SCEN_COLLECTION m_NKC_SCEN_COLLECTION;

		// Token: 0x04003582 RID: 13698
		private NKC_SCEN_WARFARE_GAME m_NKC_SCEN_WARFARE_GAME;

		// Token: 0x04003583 RID: 13699
		private NKC_SCEN_SHOP m_NKC_SCEN_SHOP;

		// Token: 0x04003584 RID: 13700
		private NKC_SCEN_FRIEND m_NKC_SCEN_FRIEND;

		// Token: 0x04003585 RID: 13701
		private NKC_SCEN_WORLDMAP m_NKC_SCEN_WORLDMAP;

		// Token: 0x04003586 RID: 13702
		private NKC_SCEN_CUTSCEN_DUNGEON m_NKC_SCEN_CUTSCEN_DUNGEON;

		// Token: 0x04003587 RID: 13703
		private NKC_SCEN_GAME_RESULT m_NKC_SCEN_GAME_RESULT;

		// Token: 0x04003588 RID: 13704
		private NKC_SCEN_DIVE_READY m_NKC_SCEN_DIVE_READY;

		// Token: 0x04003589 RID: 13705
		private NKC_SCEN_DIVE m_NKC_SCEN_DIVE;

		// Token: 0x0400358A RID: 13706
		private NKC_SCEN_DIVE_RESULT m_NKC_SCEN_DIVE_RESULT;

		// Token: 0x0400358B RID: 13707
		private NKC_SCEN_GAUNTLET_INTRO m_NKC_SCEN_GAUNTLET_INTRO;

		// Token: 0x0400358C RID: 13708
		private NKC_SCEN_GAUNTLET_LOBBY m_NKC_SCEN_GAUNTLET_LOBBY;

		// Token: 0x0400358D RID: 13709
		private NKC_SCEN_GAUNTLET_MATCH_READY m_NKC_SCEN_GAUNTLET_MATCH_READY;

		// Token: 0x0400358E RID: 13710
		private NKC_SCEN_GAUNTLET_MATCH m_NKC_SCEN_GAUNTLET_MATCH;

		// Token: 0x0400358F RID: 13711
		private NKC_SCEN_GAUNTLET_ASYNC_READY m_NKC_SCEN_GAUNTLET_ASYNC_READY;

		// Token: 0x04003590 RID: 13712
		private NKC_SCEN_GAUNTLET_PRIVATE_READY m_NKC_SCEN_GAUNTLET_PRIVATE_READY;

		// Token: 0x04003591 RID: 13713
		private NKC_SCEN_GAUNTLET_LEAGUE_ROOM m_NKC_SCEN_GAUNTLET_LEAGUE_ROOM;

		// Token: 0x04003592 RID: 13714
		private NKC_SCEN_GAUNTLET_PRIVATE_ROOM m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM;

		// Token: 0x04003593 RID: 13715
		private NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT m_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT;

		// Token: 0x04003594 RID: 13716
		private NKC_SCEN_RAID m_NKC_SCEN_RAID;

		// Token: 0x04003595 RID: 13717
		private NKC_SCEN_RAID_READY m_NKC_SCEN_RAID_READY;

		// Token: 0x04003596 RID: 13718
		private NKC_SCEN_VOICE_LIST m_NKC_SCEN_VOICE_LIST;

		// Token: 0x04003597 RID: 13719
		private NKC_SCEN_SHADOW_PALACE m_NKC_SCEN_SHADOW_PALACE;

		// Token: 0x04003598 RID: 13720
		private NKC_SCEN_SHADOW_BATTLE m_NKC_SCEN_SHADOW_BATTLE;

		// Token: 0x04003599 RID: 13721
		private NKC_SCEN_SHADOW_RESULT m_NKC_SCEN_SHADOW_RESULT;

		// Token: 0x0400359A RID: 13722
		private NKC_SCEN_GUILD_INTRO m_NKC_SCEN_GUILD_INTRO;

		// Token: 0x0400359B RID: 13723
		private NKC_SCEN_GUILD_LOBBY m_NKC_SCEN_GUILD_LOBBY;

		// Token: 0x0400359C RID: 13724
		private NKC_SCEN_FIERCE_BATTLE_SUPPORT m_NKC_SCEN_FIERCE_BATTLE_SUPPORT;

		// Token: 0x0400359D RID: 13725
		private NKC_SCEN_GUILD_COOP m_NKC_SCEN_GUILD_COOP;

		// Token: 0x0400359E RID: 13726
		private NKC_SCEN_OFFICE m_NKC_SCEN_OFFICE;

		// Token: 0x0400359F RID: 13727
		private NKC_SCEN_TRIM m_NKC_SCEN_TRIM;

		// Token: 0x040035A0 RID: 13728
		private NKC_SCEN_TRIM_RESULT m_NKC_SCEN_TRIM_RESULT;

		// Token: 0x040035A1 RID: 13729
		private NKC_SCEN_DUNGEON_RESULT m_NKC_SCEN_DUNGEON_RESULT;

		// Token: 0x040035A2 RID: 13730
		private NKCScenManager.DoAfterScenChange dDoAfterScenChange;

		// Token: 0x040035A3 RID: 13731
		private float m_enableConnectCheckTime = -1f;

		// Token: 0x040035A4 RID: 13732
		private NKCConnectLogin m_NKCConnectLogin = new NKCConnectLogin();

		// Token: 0x040035A5 RID: 13733
		private NKCConnectGame m_NKCConnectGame = new NKCConnectGame();

		// Token: 0x040035A6 RID: 13734
		private NKCGameClient m_NKCGameClient;

		// Token: 0x040035A7 RID: 13735
		private float m_FixedFrameTime = 0.016666668f;

		// Token: 0x040035A8 RID: 13736
		private float m_fFPSTime;

		// Token: 0x040035A9 RID: 13737
		private StringBuilder m_FPSText = new StringBuilder();

		// Token: 0x040035AA RID: 13738
		private bool m_bScenChanging;

		// Token: 0x040035AB RID: 13739
		private Queue<NKCScenChangeOrder> m_qScenChange = new Queue<NKCScenChangeOrder>();

		// Token: 0x040035AC RID: 13740
		private NKMUserData m_MyUserData;

		// Token: 0x040035AE RID: 13742
		private NKCReplayMgr m_NKCReplayMgr = new NKCReplayMgr();

		// Token: 0x040035AF RID: 13743
		private NKCContractDataMgr m_NKCContractDataMgr = new NKCContractDataMgr();

		// Token: 0x040035B0 RID: 13744
		private NKCFierceBattleSupportDataMgr m_NKCFierceBattleSupportDataMgr = new NKCFierceBattleSupportDataMgr();

		// Token: 0x040035B1 RID: 13745
		private NKCRaidDataMgr m_NKCRaidDataMgr = new NKCRaidDataMgr();

		// Token: 0x040035B2 RID: 13746
		private NKCRepeatOperaion m_NKCRepeatOperaion = new NKCRepeatOperaion();

		// Token: 0x040035B3 RID: 13747
		private NKCPowerSaveMode m_NKCPowerSaveMode = new NKCPowerSaveMode();

		// Token: 0x040035B4 RID: 13748
		private NKCSurveyMgr m_NKCSurveyMgr = new NKCSurveyMgr();

		// Token: 0x040035B5 RID: 13749
		private NKCGameOptionData m_GameOptionData = new NKCGameOptionData();

		// Token: 0x040035B6 RID: 13750
		private NKCEventPassDataManager m_NKCEventPassDataManager = new NKCEventPassDataManager();

		// Token: 0x040035B8 RID: 13752
		private GameObject m_objTextureCamera;

		// Token: 0x040035B9 RID: 13753
		public const int TextureCaptureLayer = 31;

		// Token: 0x040035BA RID: 13754
		private bool[] m_bTouch = new bool[3];

		// Token: 0x040035BB RID: 13755
		private Vector3[] m_TouchPos3D = new Vector3[3];

		// Token: 0x040035BC RID: 13756
		private Vector2[] m_TouchPos2D = new Vector2[3];

		// Token: 0x040035BD RID: 13757
		private bool m_bHasPinch;

		// Token: 0x040035BE RID: 13758
		private float m_fPinchDeltaMagnitude;

		// Token: 0x040035BF RID: 13759
		private Vector2 m_vPinchCenter;

		// Token: 0x040035C0 RID: 13760
		private float m_fScreenWidthInvSquare;

		// Token: 0x040035C1 RID: 13761
		private GameObject m_NKM_NEW_INSTANT;

		// Token: 0x040035C2 RID: 13762
		protected NKCEffectManager m_NKCEffectManager = new NKCEffectManager();

		// Token: 0x040035C3 RID: 13763
		private RectTransform m_rtNUF_AFTER_UI_EFFECT;

		// Token: 0x040035C4 RID: 13764
		private Rect m_RectToDrawFPS;

		// Token: 0x040035C5 RID: 13765
		private bool m_bSkipScenChangeFadeOutEffect;

		// Token: 0x040035C6 RID: 13766
		private NKCHealthyGame m_NKCHealthyGame;

		// Token: 0x040035C7 RID: 13767
		private bool m_bSetLanguage;

		// Token: 0x040035C8 RID: 13768
		private RenderTexture m_tempTexture;

		// Token: 0x040035C9 RID: 13769
		private MeshRenderer tempMeshRenderer;

		// Token: 0x040035CA RID: 13770
		private NKM_SCEN_ID m_CurScenID;

		// Token: 0x040035CB RID: 13771
		private NKM_SCEN_ID m_BeforeScenID;

		// Token: 0x040035CC RID: 13772
		private Coroutine CameraPermissionCoroutine;

		// Token: 0x040035CD RID: 13773
		private bool m_bVersionCheckRunning;

		// Token: 0x02001390 RID: 5008
		// (Invoke) Token: 0x0600A630 RID: 42544
		public delegate void DoAfterScenChange();
	}
}
