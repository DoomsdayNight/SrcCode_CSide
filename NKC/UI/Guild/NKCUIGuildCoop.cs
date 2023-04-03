using System;
using System.Collections;
using ClientPacket.Guild;
using ClientPacket.Warfare;
using Cs.Core.Util;
using Cs.Logging;
using NKC.PacketHandler;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B36 RID: 2870
	public class NKCUIGuildCoop : NKCUIBase
	{
		// Token: 0x0600829D RID: 33437 RVA: 0x002C1240 File Offset: 0x002BF440
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIGuildCoop.s_LoadedUIData))
			{
				NKCUIGuildCoop.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIGuildCoop>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_CONSORTIUM_COOP_FRONT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGuildCoop.CleanupInstance));
			}
			return NKCUIGuildCoop.s_LoadedUIData;
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x0600829E RID: 33438 RVA: 0x002C1274 File Offset: 0x002BF474
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGuildCoop.s_LoadedUIData != null && NKCUIGuildCoop.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x0600829F RID: 33439 RVA: 0x002C1289 File Offset: 0x002BF489
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIGuildCoop.s_LoadedUIData != null && NKCUIGuildCoop.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x060082A0 RID: 33440 RVA: 0x002C129E File Offset: 0x002BF49E
		public static NKCUIGuildCoop GetInstance()
		{
			if (NKCUIGuildCoop.s_LoadedUIData != null && NKCUIGuildCoop.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIGuildCoop.s_LoadedUIData.GetInstance<NKCUIGuildCoop>();
			}
			return null;
		}

		// Token: 0x060082A1 RID: 33441 RVA: 0x002C12BF File Offset: 0x002BF4BF
		public static void CleanupInstance()
		{
			NKCUIGuildCoop.s_LoadedUIData = null;
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x060082A2 RID: 33442 RVA: 0x002C12C7 File Offset: 0x002BF4C7
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x060082A3 RID: 33443 RVA: 0x002C12CA File Offset: 0x002BF4CA
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_BATTLE", false);
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x060082A4 RID: 33444 RVA: 0x002C12D7 File Offset: 0x002BF4D7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x060082A5 RID: 33445 RVA: 0x002C12DA File Offset: 0x002BF4DA
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_GUILD_DUNGEON";
			}
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x002C12E4 File Offset: 0x002BF4E4
		public void InitUI()
		{
			this.m_btnChat.PointerClick.RemoveAllListeners();
			this.m_btnChat.PointerClick.AddListener(new UnityAction(this.OnClickChat));
			this.m_btnStatus.PointerClick.RemoveAllListeners();
			this.m_btnStatus.PointerClick.AddListener(new UnityAction(this.OnClickStatus));
			this.m_btnSeasonReward.PointerClick.RemoveAllListeners();
			this.m_btnSeasonReward.PointerClick.AddListener(new UnityAction(this.OnClickSeasonReward));
			this.m_btnViewArtifact.PointerClick.RemoveAllListeners();
			this.m_btnViewArtifact.PointerClick.AddListener(new UnityAction(this.OnClickArtifact));
			if (this.m_ArtifactPopup != null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_ArtifactPopup.InitUI();
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			this.m_SeasonTemplet = GuildDungeonTempletManager.GetCurrentSeasonTemplet(ServiceTime.Recent);
			if (this.m_SeasonTemplet == null)
			{
				Log.Error(string.Format("시즌 진행중이 아님 - current : {0}", ServiceTime.Recent), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCUIGuildCoop.cs", 123);
				return;
			}
			this.m_CurSessionData = this.m_SeasonTemplet.GetCurrentSession(ServiceTime.Recent);
			this.m_CurSessionData.templet.GetDungeonList();
			GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>(this.m_SeasonTemplet.GetSeasonBgPrefabName(), this.m_SeasonTemplet.GetSeasonBgPrefabName(), false);
			if (orLoadAssetResource != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource);
				this.m_NKCUIGuildCoopBack = gameObject.GetComponent<NKCUIGuildCoopBack>();
				if (this.m_NKCUIGuildCoopBack != null)
				{
					this.m_NKCUIGuildCoopBack.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas), false);
					this.m_NKCUIGuildCoopBack.transform.position = Vector3.zero;
					this.m_NKCUIGuildCoopBack.Init(this.m_SeasonTemplet.Key, new NKCUIGuildCoopBack.OnClickArena(this.OnClickArena), new NKCUIGuildCoopBack.OnClickBoss(this.OnClickBoss));
				}
			}
			this.m_Animator.Play("NKM_UI_CONSORTIUM_COOP_FRONT_IMTRO");
			this.m_GuildBadge.InitUI();
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x002C14EE File Offset: 0x002BF6EE
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x002C14FC File Offset: 0x002BF6FC
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			UnityEngine.Object.Destroy(this.m_NKCUIGuildCoopBack.gameObject);
		}

		// Token: 0x060082A9 RID: 33449 RVA: 0x002C1514 File Offset: 0x002BF714
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x002C1524 File Offset: 0x002BF724
		public void Open()
		{
			this.m_GuildBadge.SetData(NKCGuildManager.MyGuildData.badgeId);
			NKCUtil.SetLabelText(this.m_lbGuildLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, NKCGuildManager.MyGuildData.guildLevel));
			NKCUtil.SetLabelText(this.m_lbGuildName, NKCGuildManager.MyGuildData.name);
			if (NKCGuildCoopManager.m_cGuildRaidTemplet == null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageId());
			if (dungeonTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbBossName, dungeonTempletBase.GetDungeonName());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbBossName, "");
			}
			NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_SESSION_PROGRESS_OF_ROUND_INFO, this.m_CurSessionData.templet.GetSessionIndex()));
			NKCUtil.SetLabelText(this.m_lbTaskNum, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_SESSION_PROGRESS_OF_ROUND_TASK_INFO, this.m_CurSessionData.templet.GetSessionIndex()));
			NKCUtil.SetLabelText(this.m_lbSeasonName, NKCStringTable.GetString(this.m_SeasonTemplet.GetSeasonNameID(), false));
			this.bSessionChanged = false;
			this.m_fDeltaTime = 0f;
			this.UpdateRemainTime();
			this.SetNewChatCount(NKCChatManager.GetUncheckedMessageCount(NKCGuildManager.MyData.guildUid));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_NKCUIGuildCoopBack != null)
			{
				this.m_NKCUIGuildCoopBack.SetData();
				this.m_NKCUIGuildCoopBack.SetEnableDrag(true);
			}
			NKCUtil.SetGameobjectActive(this.m_btnStatus, NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON_LEADERBOARD, 0, 0));
			NKCUtil.SetGameobjectActive(this.m_btnSeasonReward, NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON_REWARD_SEASON, 0, 0));
			GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.GetGuildMemberInfo().Find((GuildDungeonMemberInfo x) => x.profile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
			NKCUtil.SetGameobjectActive(this.m_objDisabled, guildDungeonMemberInfo == null);
			if (guildDungeonMemberInfo == null)
			{
				NKCUtil.SetLabelText(this.m_lbRemainArenaCount, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_COOP_FRONT_COUNT_ARENA, 0));
				NKCUtil.SetLabelText(this.m_lbRemainBossCount, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_COOP_FRONT_COUNT_RAID, 0));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbRemainArenaCount, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_COOP_FRONT_COUNT_ARENA, NKCGuildCoopManager.m_ArenaPlayableCount));
				NKCUtil.SetLabelText(this.m_lbRemainBossCount, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_COOP_FRONT_COUNT_RAID, NKCGuildCoopManager.m_BossPlayableCount));
			}
			if (!NKCUIGuildCoop.IsInstanceOpen)
			{
				base.UIOpened(true);
			}
			NKCUtil.SetGameobjectActive(this.m_objSeasonRewardRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable());
			if ((NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.SessionOut || NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.SeasonOut) && !NKCUIGuildCoopEnd.IsInstanceOpen)
			{
				NKCUIGuildCoopEnd.Instance.Open();
			}
			if (NKCGuildCoopManager.m_GuildDungeonState != GuildDungeonState.Adjust && NKCGuildCoopManager.m_bCanReward)
			{
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_SESSION_REWARD_REQ();
			}
		}

		// Token: 0x060082AB RID: 33451 RVA: 0x002C17C5 File Offset: 0x002BF9C5
		private void UpdateRemainTime()
		{
			NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeStringEx(NKCGuildCoopManager.m_NextSessionStartDateUTC));
		}

		// Token: 0x060082AC RID: 33452 RVA: 0x002C17DC File Offset: 0x002BF9DC
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				if (NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.SeasonOut && NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC) && !NKCGuildCoopManager.HasNextSessionData(NKCGuildCoopManager.m_NextSessionStartDateUTC))
				{
					NKCUtil.SetLabelText(this.m_lbRemainTime, "");
					return;
				}
				if (NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC))
				{
					if (!this.bSessionChanged)
					{
						this.bSessionChanged = true;
						NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_INFO_REQ(NKCGuildManager.MyData.guildUid);
						return;
					}
				}
				else
				{
					this.UpdateRemainTime();
				}
			}
		}

		// Token: 0x060082AD RID: 33453 RVA: 0x002C187C File Offset: 0x002BFA7C
		public void SetNewChatCount(int count)
		{
			if (count > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNewCount, true);
				NKCUtil.SetLabelText(this.m_lbNewCount, count.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNewCount, false);
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x002C18AD File Offset: 0x002BFAAD
		public void OnCloseInfoPopup()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.ProcessStartCamera(this.m_NKCUIGuildCoopBack.GetTargetPosition(0, true), false));
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x002C18D0 File Offset: 0x002BFAD0
		public void OnClickArena(GuildDungeonInfoTemplet templet)
		{
			if (templet != null)
			{
				if (NKCPopupGuildCoopBossInfo.IsInstanceOpen)
				{
					NKCPopupGuildCoopBossInfo.Instance.Close();
				}
				base.StopAllCoroutines();
				base.StartCoroutine(this.ProcessStartCamera(this.m_NKCUIGuildCoopBack.GetTargetPosition(templet.GetArenaIndex(), true), true));
				NKCPopupGuildCoopArenaInfo.Instance.Open(templet, new NKCPopupGuildCoopArenaInfo.OnClickStart(this.OnStartArena));
			}
		}

		// Token: 0x060082B0 RID: 33456 RVA: 0x002C192E File Offset: 0x002BFB2E
		private IEnumerator ProcessStartCamera(Vector3 pinPos, bool bZoomin)
		{
			yield return null;
			if (bZoomin)
			{
				this.m_Animator.Play("NKM_UI_CONSORTIUM_COOP_FRONT_OUTRO");
				NKCCamera.TrackingPos(0.4f, pinPos.x + this.m_NKCUIGuildCoopBack.m_fCameraXPosAddValue, pinPos.y, this.m_NKCUIGuildCoopBack.m_fCameraZPosZoomIn);
				this.m_NKCUIGuildCoopBack.SetEnableDrag(false);
			}
			else
			{
				this.m_Animator.Play("NKM_UI_CONSORTIUM_COOP_FRONT_INTRO");
				NKCCamera.TrackingPos(0.4f, pinPos.x, pinPos.y, this.m_NKCUIGuildCoopBack.m_fCameraZPosZoomOut);
				this.m_NKCUIGuildCoopBack.SetEnableDrag(true);
			}
			yield return new WaitForSecondsWithCancel(1.6f, new WaitForSecondsWithCancel.CancelWait(this.CanSkipSDCamera), null);
			yield break;
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x002C194B File Offset: 0x002BFB4B
		private bool CanSkipSDCamera()
		{
			return Input.anyKeyDown;
		}

		// Token: 0x060082B2 RID: 33458 RVA: 0x002C1954 File Offset: 0x002BFB54
		private void OnStartArena(NKMDungeonTempletBase templet, int arenaIdx)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(NKCGuildCoopManager.CanStartArena(arenaIdx), true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().WarfareGameData != null && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP && NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID > 0)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EPISODE_GIVE_UP_WARFARE, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGWarfare), null, false);
				return;
			}
			if (NKCPopupGuildCoopArenaInfo.IsInstanceOpen)
			{
				NKCPopupGuildCoopArenaInfo.Instance.Close();
			}
			NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(templet, DeckContents.GUILD_COOP);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
		}

		// Token: 0x060082B3 RID: 33459 RVA: 0x002C19F4 File Offset: 0x002BFBF4
		private void OnClickOkGiveUpINGWarfare()
		{
			NKMPacket_WARFARE_GAME_GIVE_UP_REQ packet = new NKMPacket_WARFARE_GAME_GIVE_UP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060082B4 RID: 33460 RVA: 0x002C1A1C File Offset: 0x002BFC1C
		public void OnClickBoss(int stageID)
		{
			GuildSeasonTemplet currentSeasonTemplet = GuildDungeonTempletManager.GetCurrentSeasonTemplet(ServiceTime.Recent);
			if (currentSeasonTemplet == null)
			{
				return;
			}
			if (NKCPopupGuildCoopArenaInfo.IsInstanceOpen)
			{
				NKCPopupGuildCoopArenaInfo.Instance.Close();
			}
			base.StopAllCoroutines();
			base.StartCoroutine(this.ProcessStartCamera(this.m_NKCUIGuildCoopBack.GetTargetPosition(0, false), true));
			int seasonRaidGroup = currentSeasonTemplet.GetSeasonRaidGroup();
			NKCPopupGuildCoopBossInfo.Instance.Open(GuildDungeonTempletManager.GetGuildRaidTemplet(seasonRaidGroup, NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageId()), new NKCPopupGuildCoopBossInfo.OnStartBoss(this.OnStartBoss));
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x002C1A98 File Offset: 0x002BFC98
		private void OnStartBoss()
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(NKCGuildCoopManager.CanStartBoss(), true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetRaidUID((long)NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageId());
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetGuildRaid(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID_READY, true);
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x002C1AF0 File Offset: 0x002BFCF0
		public void OnClickChat()
		{
			NKCUtil.SetGameobjectActive(this.m_objNewCount, false);
			NKCPopupGuildChat.Instance.Open(NKCGuildManager.MyData.guildUid);
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x002C1B12 File Offset: 0x002BFD12
		public void OnClickStatus()
		{
			NKCPopupGuildCoopStatus.Instance.Open();
		}

		// Token: 0x060082B8 RID: 33464 RVA: 0x002C1B1E File Offset: 0x002BFD1E
		public void OnClickSeasonReward()
		{
			NKCPopupGuildCoopSeasonReward.Instance.Open(new NKCPopupGuildCoopSeasonReward.OnClose(this.RefreshSeasonReward));
		}

		// Token: 0x060082B9 RID: 33465 RVA: 0x002C1B36 File Offset: 0x002BFD36
		public void OnClickArtifact()
		{
			if (!this.m_ArtifactPopup.gameObject.activeSelf)
			{
				this.m_ArtifactPopup.Open();
				return;
			}
			this.m_ArtifactPopup.Close();
		}

		// Token: 0x060082BA RID: 33466 RVA: 0x002C1B61 File Offset: 0x002BFD61
		public void RefreshSeasonReward()
		{
			NKCUtil.SetGameobjectActive(this.m_objSeasonRewardRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable());
		}

		// Token: 0x060082BB RID: 33467 RVA: 0x002C1B73 File Offset: 0x002BFD73
		public void RefreshArenaSlot(int arenaIdx)
		{
			NKCUIGuildCoopBack nkcuiguildCoopBack = this.m_NKCUIGuildCoopBack;
			if (nkcuiguildCoopBack == null)
			{
				return;
			}
			nkcuiguildCoopBack.RefreshArenaSlot(arenaIdx);
		}

		// Token: 0x060082BC RID: 33468 RVA: 0x002C1B86 File Offset: 0x002BFD86
		public void RefreshBossSlot()
		{
			NKCUIGuildCoopBack nkcuiguildCoopBack = this.m_NKCUIGuildCoopBack;
			if (nkcuiguildCoopBack == null)
			{
				return;
			}
			nkcuiguildCoopBack.RefreshBossSlot();
		}

		// Token: 0x04006ED9 RID: 28377
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006EDA RID: 28378
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_COOP_FRONT";

		// Token: 0x04006EDB RID: 28379
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04006EDC RID: 28380
		public NKCPopupGuildCoopArtifactStorage m_ArtifactPopup;

		// Token: 0x04006EDD RID: 28381
		[Header("좌측 UI")]
		public Animator m_Animator;

		// Token: 0x04006EDE RID: 28382
		public Text m_lbTaskNum;

		// Token: 0x04006EDF RID: 28383
		public Text m_lbTitle;

		// Token: 0x04006EE0 RID: 28384
		public Text m_lbSeasonName;

		// Token: 0x04006EE1 RID: 28385
		public Text m_lbRemainTime;

		// Token: 0x04006EE2 RID: 28386
		public Text m_lbRemainArenaCount;

		// Token: 0x04006EE3 RID: 28387
		public Text m_lbRemainBossCount;

		// Token: 0x04006EE4 RID: 28388
		public NKCUIComStateButton m_btnStatus;

		// Token: 0x04006EE5 RID: 28389
		public NKCUIComStateButton m_btnSeasonReward;

		// Token: 0x04006EE6 RID: 28390
		public GameObject m_objSeasonRewardRedDot;

		// Token: 0x04006EE7 RID: 28391
		public NKCUIComStateButton m_btnViewArtifact;

		// Token: 0x04006EE8 RID: 28392
		public GameObject m_objDisabled;

		// Token: 0x04006EE9 RID: 28393
		[Header("길드 정보")]
		public NKCUIGuildBadge m_GuildBadge;

		// Token: 0x04006EEA RID: 28394
		public Text m_lbGuildName;

		// Token: 0x04006EEB RID: 28395
		public Text m_lbGuildLevel;

		// Token: 0x04006EEC RID: 28396
		[Header("중앙 하단 보스정보")]
		public Text m_lbBossName;

		// Token: 0x04006EED RID: 28397
		[Header("채팅창 호출")]
		public NKCUIComStateButton m_btnChat;

		// Token: 0x04006EEE RID: 28398
		public GameObject m_objNewCount;

		// Token: 0x04006EEF RID: 28399
		public Text m_lbNewCount;

		// Token: 0x04006EF0 RID: 28400
		private NKCUIGuildCoopBack m_NKCUIGuildCoopBack;

		// Token: 0x04006EF1 RID: 28401
		private GuildSeasonTemplet m_SeasonTemplet;

		// Token: 0x04006EF2 RID: 28402
		private GuildSeasonTemplet.SessionData m_CurSessionData;

		// Token: 0x04006EF3 RID: 28403
		private float m_fDeltaTime;

		// Token: 0x04006EF4 RID: 28404
		private bool bSessionChanged;
	}
}
