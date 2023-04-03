using System;
using NKC.PacketHandler;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B27 RID: 2855
	public class NKCPopupGuildCoopArenaInfo : NKCUIBase
	{
		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x0600820D RID: 33293 RVA: 0x002BE20C File Offset: 0x002BC40C
		public static NKCPopupGuildCoopArenaInfo Instance
		{
			get
			{
				if (NKCPopupGuildCoopArenaInfo.m_Instance == null)
				{
					NKCPopupGuildCoopArenaInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopArenaInfo>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_ARENA_INFO", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildCoopArenaInfo.CleanupInstance)).GetInstance<NKCPopupGuildCoopArenaInfo>();
					if (NKCPopupGuildCoopArenaInfo.m_Instance != null)
					{
						NKCPopupGuildCoopArenaInfo.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopArenaInfo.m_Instance;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x0600820E RID: 33294 RVA: 0x002BE26D File Offset: 0x002BC46D
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopArenaInfo.m_Instance != null && NKCPopupGuildCoopArenaInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x002BE288 File Offset: 0x002BC488
		private static void CleanupInstance()
		{
			NKCPopupGuildCoopArenaInfo.m_Instance = null;
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06008210 RID: 33296 RVA: 0x002BE290 File Offset: 0x002BC490
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06008211 RID: 33297 RVA: 0x002BE297 File Offset: 0x002BC497
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008212 RID: 33298 RVA: 0x002BE29C File Offset: 0x002BC49C
		public void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_NKCUIComEnemyList.InitUI();
			this.m_btnStart.PointerClick.RemoveAllListeners();
			this.m_btnStart.PointerClick.AddListener(new UnityAction(this.OnClickBattle));
			this.m_btnStart.m_bGetCallbackWhileLocked = true;
			this.m_btnShowArtifact.PointerClick.RemoveAllListeners();
			this.m_btnShowArtifact.PointerClick.AddListener(new UnityAction(this.OnClickShowArtifact));
		}

		// Token: 0x06008213 RID: 33299 RVA: 0x002BE344 File Offset: 0x002BC544
		public override void CloseInternal()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().OnCloseInfoPopup();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_dOnClickStart = null;
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x002BE368 File Offset: 0x002BC568
		private void OnDestroy()
		{
			NKCPopupGuildCoopArenaInfo.m_Instance = null;
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x002BE370 File Offset: 0x002BC570
		public void Open(GuildDungeonInfoTemplet templet, NKCPopupGuildCoopArenaInfo.OnClickStart onClickStart)
		{
			this.m_GuildDungeonInfoTemplet = templet;
			this.m_dOnClickStart = onClickStart;
			if (templet == null)
			{
				return;
			}
			this.m_dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(templet.GetSeasonDungeonId());
			if (this.m_dungeonTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				Debug.LogError(string.Format("dungeonTempletBase is null - {0}", templet.GetSeasonDungeonId()));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbArenaNum, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_DUNGEON_UI_ARENA_INFO, templet.GetArenaIndex()));
			NKCUtil.SetLabelText(this.m_lbArenaName, this.m_dungeonTempletBase.GetDungeonName());
			this.m_NKCUIComDungeonMission.SetData(this.m_dungeonTempletBase, true);
			if (NKCUtil.GetSpriteBattleConditionICon(this.m_dungeonTempletBase.BattleCondition) == null)
			{
				NKCUtil.SetImageSprite(this.m_imgBattleCondition, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_BC", "AB_UI_NKM_UI_COMMON_BC_ICON_NONE", false), false);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgBattleCondition, NKCUtil.GetSpriteBattleConditionICon(this.m_dungeonTempletBase.BattleCondition), false);
			}
			if (this.m_dungeonTempletBase.BattleCondition != null)
			{
				NKCUtil.SetLabelText(this.m_lbBattleConditionName, this.m_dungeonTempletBase.BattleCondition.BattleCondName_Translated);
				NKCUtil.SetLabelText(this.m_lbBattleConditionDesc, this.m_dungeonTempletBase.BattleCondition.BattleCondDesc_Translated);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbBattleConditionName, NKCUtilString.GET_STRING_NO_EXIST);
				NKCUtil.SetLabelText(this.m_lbBattleConditionDesc, string.Empty);
			}
			this.UpdateClearPoint();
			this.UpdateButtonState();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIComEnemyList.SetData(this.m_dungeonTempletBase);
			base.UIOpened(true);
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x002BE500 File Offset: 0x002BC700
		private void UpdateClearPoint()
		{
			int currentArtifactCountByArena = NKCGuildCoopManager.GetCurrentArtifactCountByArena(this.m_GuildDungeonInfoTemplet.GetArenaIndex());
			int count = GuildDungeonTempletManager.GetDungeonArtifactList(this.m_GuildDungeonInfoTemplet.GetStageRewardArtifactGroup()).Count;
			if (currentArtifactCountByArena == count)
			{
				NKCUtil.SetGameobjectActive(this.m_objNextArtifact, false);
				float num = 1f;
				NKCUtil.SetLabelText(this.m_lbArenaClearPoint, string.Format("{0}%", (num * 100f).ToString("N0")));
				this.m_imgClearPoint.fillAmount = num;
				return;
			}
			float clearPointPercentage = NKCGuildCoopManager.GetClearPointPercentage(this.m_GuildDungeonInfoTemplet.GetArenaIndex());
			NKCUtil.SetLabelText(this.m_lbArenaClearPoint, string.Format("{0}%", (clearPointPercentage * 100f).ToString("N0")));
			this.m_imgClearPoint.fillAmount = clearPointPercentage;
			int nextArtifactID = NKCGuildCoopManager.GetNextArtifactID(this.m_GuildDungeonInfoTemplet.GetArenaIndex());
			GuildDungeonArtifactTemplet artifactTemplet = GuildDungeonTempletManager.GetArtifactTemplet(nextArtifactID);
			if (artifactTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objNextArtifact, true);
				this.m_slotArtifact.SetData(NKCUISlot.SlotData.MakeGuildArtifactData(nextArtifactID, 1), true, null);
				NKCUtil.SetLabelText(this.m_lbNextArtifaceDesc, artifactTemplet.GetDescFull());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNextArtifact, false);
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x002BE623 File Offset: 0x002BC823
		private void UpdateButtonState()
		{
			if (NKCGuildCoopManager.CanStartArena(this.m_GuildDungeonInfoTemplet.GetArenaIndex()) == NKM_ERROR_CODE.NEC_OK)
			{
				this.m_btnStart.UnLock(false);
				return;
			}
			this.m_btnStart.Lock(false);
		}

		// Token: 0x06008218 RID: 33304 RVA: 0x002BE650 File Offset: 0x002BC850
		public void OnClickBattle()
		{
			if (this.m_btnStart.m_bLock)
			{
				NKCPacketHandlers.Check_NKM_ERROR_CODE(NKCGuildCoopManager.CanStartArena(this.m_GuildDungeonInfoTemplet.GetArenaIndex()), true, null, int.MinValue);
				return;
			}
			NKCPopupGuildCoopArenaInfo.OnClickStart dOnClickStart = this.m_dOnClickStart;
			if (dOnClickStart == null)
			{
				return;
			}
			dOnClickStart(this.m_dungeonTempletBase, this.m_GuildDungeonInfoTemplet.GetArenaIndex());
		}

		// Token: 0x06008219 RID: 33305 RVA: 0x002BE6A9 File Offset: 0x002BC8A9
		public void OnClickShowArtifact()
		{
			NKCPopupGuildCoopArtifactList.Instance.Open(this.m_GuildDungeonInfoTemplet, NKCGuildCoopManager.GetCurrentArtifactCountByArena(this.m_GuildDungeonInfoTemplet.GetArenaIndex()));
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x002BE6CB File Offset: 0x002BC8CB
		public void Refresh()
		{
			this.UpdateClearPoint();
			this.UpdateButtonState();
		}

		// Token: 0x04006E2F RID: 28207
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006E30 RID: 28208
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_ARENA_INFO";

		// Token: 0x04006E31 RID: 28209
		private static NKCPopupGuildCoopArenaInfo m_Instance;

		// Token: 0x04006E32 RID: 28210
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006E33 RID: 28211
		[Header("타이틀")]
		public Text m_lbArenaNum;

		// Token: 0x04006E34 RID: 28212
		public Text m_lbArenaName;

		// Token: 0x04006E35 RID: 28213
		[Header("정화도")]
		public Text m_lbArenaClearPoint;

		// Token: 0x04006E36 RID: 28214
		public Image m_imgClearPoint;

		// Token: 0x04006E37 RID: 28215
		[Header("다음 아티팩트 숫자")]
		public GameObject m_objNextArtifact;

		// Token: 0x04006E38 RID: 28216
		public NKCUISlot m_slotArtifact;

		// Token: 0x04006E39 RID: 28217
		public Text m_lbNextArtifaceDesc;

		// Token: 0x04006E3A RID: 28218
		[Header("메달")]
		public NKCUIComDungeonMission m_NKCUIComDungeonMission;

		// Token: 0x04006E3B RID: 28219
		[Header("전투 환경")]
		public GameObject m_objBattleCondition;

		// Token: 0x04006E3C RID: 28220
		public Image m_imgBattleCondition;

		// Token: 0x04006E3D RID: 28221
		public Text m_lbBattleConditionName;

		// Token: 0x04006E3E RID: 28222
		public Text m_lbBattleConditionDesc;

		// Token: 0x04006E3F RID: 28223
		[Header("등장 적 리스트")]
		public NKCUIComEnemyList m_NKCUIComEnemyList;

		// Token: 0x04006E40 RID: 28224
		[Header("하단")]
		public NKCUIComStateButton m_btnStart;

		// Token: 0x04006E41 RID: 28225
		public NKCUIComStateButton m_btnShowArtifact;

		// Token: 0x04006E42 RID: 28226
		private NKCPopupGuildCoopArenaInfo.OnClickStart m_dOnClickStart;

		// Token: 0x04006E43 RID: 28227
		private GuildDungeonInfoTemplet m_GuildDungeonInfoTemplet;

		// Token: 0x04006E44 RID: 28228
		private NKMDungeonTempletBase m_dungeonTempletBase;

		// Token: 0x020018C6 RID: 6342
		// (Invoke) Token: 0x0600B6A9 RID: 46761
		public delegate void OnClickStart(NKMDungeonTempletBase templetBase, int arenaIdx);
	}
}
