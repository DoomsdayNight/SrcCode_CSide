using System;
using System.Collections.Generic;
using System.Text;
using Cs.Logging;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C3C RID: 3132
	public class NKCGameHud : MonoBehaviour
	{
		// Token: 0x0600916A RID: 37226 RVA: 0x00318F20 File Offset: 0x00317120
		public NKCGameClient GetGameClient()
		{
			return this.m_GameClient;
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x0600916B RID: 37227 RVA: 0x00318F28 File Offset: 0x00317128
		// (set) Token: 0x0600916C RID: 37228 RVA: 0x00318F5A File Offset: 0x0031715A
		public virtual NKM_TEAM_TYPE CurrentViewTeamType
		{
			get
			{
				if (this.m_eMode != NKCGameHud.HUDMode.Observer)
				{
					return this.m_GameClient.m_MyTeam;
				}
				if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_OBSERVE_MODE))
				{
					return this.m_GameClient.m_MyTeam;
				}
				return this.m_CurrentViewTeamType;
			}
			set
			{
				this.m_CurrentViewTeamType = value;
			}
		}

		// Token: 0x0600916D RID: 37229 RVA: 0x00318F63 File Offset: 0x00317163
		public int GetWaveCount()
		{
			return this.m_WaveCount;
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x0600916E RID: 37230 RVA: 0x00318F6B File Offset: 0x0031716B
		public int DeadlineBuffLevel
		{
			get
			{
				return this.m_DeadlineBuffLevel;
			}
		}

		// Token: 0x0600916F RID: 37231 RVA: 0x00318F73 File Offset: 0x00317173
		public NKCGameHUDRepeatOperation GetNKCGameHUDRepeatOperation()
		{
			return this.m_NKCGameHUDRepeatOperation;
		}

		// Token: 0x06009170 RID: 37232 RVA: 0x00318F7B File Offset: 0x0031717B
		public int GetSelectUnitDeckIndex()
		{
			return this.m_SelectUnitDeckIndex;
		}

		// Token: 0x06009171 RID: 37233 RVA: 0x00318F83 File Offset: 0x00317183
		public void SetSelectUnitDeckIndex(int index)
		{
			this.m_SelectUnitDeckIndex = index;
		}

		// Token: 0x06009172 RID: 37234 RVA: 0x00318F8C File Offset: 0x0031718C
		public int GetSelectShipSkillDeckIndex()
		{
			return this.m_SelectShipSkillDeckIndex;
		}

		// Token: 0x06009173 RID: 37235 RVA: 0x00318F94 File Offset: 0x00317194
		public void SetSelectShipSkillDeckIndex(int index)
		{
			this.m_SelectShipSkillDeckIndex = index;
		}

		// Token: 0x06009174 RID: 37236 RVA: 0x00318F9D File Offset: 0x0031719D
		public NKCGameHudEmoticon GetNKCGameHudEmoticon()
		{
			return this.m_NKCGameHudEmoticon;
		}

		// Token: 0x06009175 RID: 37237 RVA: 0x00318FA5 File Offset: 0x003171A5
		public NKCGameHudPause GetNKCGameHudPause()
		{
			return this.m_NKCGameHudPause;
		}

		// Token: 0x06009176 RID: 37238 RVA: 0x00318FB0 File Offset: 0x003171B0
		public virtual void InitUI(NKCGameHud.HUDMode mode)
		{
			Log.Info("[GAME_HUD] InitUI", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Hud/NKCGameHud.cs", 311);
			this.m_eMode = mode;
			this.m_HudObjects = NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().GetHudObjects();
			RectTransform component = base.GetComponent<RectTransform>();
			component.anchoredPosition3D = Vector3.zero;
			component.anchoredPosition = Vector2.zero;
			NKCUtil.SetGameobjectActive(this.m_lbUnitMaxCountSameTime, false);
			this.InitUI_NetworkStrength(base.gameObject);
			this.InitUI_GameSpeed();
			this.InitUI_HideUI(base.gameObject);
			this.InitUI_Practice();
			this.InitUI_TeamDeck();
			this.InitUI_AutoRespawn();
			this.InitUI_AutoSkill();
			this.InitUI_MessageAlertTimeover();
			this.InitUI_Pause(base.gameObject);
			this.InitUI_HudTop();
			this.InitUI_Repeat();
			this.InitUI_Multiply();
			this.InitUI_Operator();
			this.InitUI_Replay();
			this.InitUI_Observer();
			NKCUtil.SetGameobjectActive(this.m_killCount, false);
			this.m_bCountDownVoicePlayed = false;
			this.m_bShipSkillReadyVoicePlayed = false;
			this.m_bShipSkillReady = false;
			this.m_fShipSkillFullTime = 0f;
			this.m_bCostFullVoicePlayed = false;
			this.m_bCostFull = false;
			this.m_fMaxCostTime = 0f;
			this.InitUI_Finalize();
		}

		// Token: 0x06009177 RID: 37239 RVA: 0x003190D0 File Offset: 0x003172D0
		protected virtual void InitUI_Finalize()
		{
		}

		// Token: 0x06009178 RID: 37240 RVA: 0x003190D2 File Offset: 0x003172D2
		protected void InitUI_Replay()
		{
			if (this.m_eMode == NKCGameHud.HUDMode.Replay)
			{
				NKCUtil.SetGameobjectActive(this.m_HudReplay, true);
				this.m_HudReplay.InitUI(this);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_HudReplay, false);
		}

		// Token: 0x06009179 RID: 37241 RVA: 0x00319102 File Offset: 0x00317302
		protected virtual void InitUI_Observer()
		{
			if (this.m_eMode == NKCGameHud.HUDMode.Observer)
			{
				NKCUtil.SetGameobjectActive(this.m_HudObserver, true);
				this.m_HudObserver.InitUI(this);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_HudObserver, false);
		}

		// Token: 0x0600917A RID: 37242 RVA: 0x00319134 File Offset: 0x00317334
		public void ChangeTeamDeck()
		{
			NKM_TEAM_TYPE currentViewTeamType = this.CurrentViewTeamType;
			if (currentViewTeamType - NKM_TEAM_TYPE.NTT_A1 > 1)
			{
				if (currentViewTeamType - NKM_TEAM_TYPE.NTT_B1 <= 1)
				{
					this.CurrentViewTeamType = NKM_TEAM_TYPE.NTT_A1;
				}
			}
			else
			{
				this.CurrentViewTeamType = NKM_TEAM_TYPE.NTT_B1;
			}
			NKMGameTeamData currentViewTeamData = this.GetCurrentViewTeamData();
			if (currentViewTeamData != null)
			{
				float respawnCostClient = this.GetGameClient().GetRespawnCostClient(this.CurrentViewTeamType);
				this.m_NKCUIHudRespawnGage.SetRespawnCostNowValue(respawnCostClient);
				this.SetRespawnCost();
				this.SetRespawnCostAssist();
				this.SetDeck(currentViewTeamData);
				this.SetAssistDeck(currentViewTeamData);
				this.SetShipSkillDeck(currentViewTeamData.m_MainShip);
				if (currentViewTeamData.GetTC_Combo() == null)
				{
					NKCUtil.SetGameobjectActive(this.m_NKCGameHudCombo, false);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_NKCGameHudCombo, true);
				this.m_NKCGameHudCombo.SetUI(this.GetGameClient().GetGameData(), this.CurrentViewTeamType);
			}
		}

		// Token: 0x0600917B RID: 37243 RVA: 0x003191F4 File Offset: 0x003173F4
		protected virtual void InitUI_QuickCam()
		{
		}

		// Token: 0x0600917C RID: 37244 RVA: 0x003191F8 File Offset: 0x003173F8
		protected virtual void InitUI_NetworkStrength(GameObject cNUF_GAME_PREFAB)
		{
			NKCUtil.SetGameobjectActive(this.m_objNetworkWeak, false);
			if (this.m_csbtnNetworkLevel != null)
			{
				this.m_csbtnNetworkLevel.PointerDown.RemoveAllListeners();
				this.m_csbtnNetworkLevel.PointerDown.AddListener(delegate(PointerEventData eventData)
				{
					this.OnClickedNetworkLevel(eventData);
				});
			}
			if (this.m_eMode == NKCGameHud.HUDMode.Replay)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnNetworkLevel, false);
			}
		}

		// Token: 0x0600917D RID: 37245 RVA: 0x00319260 File Offset: 0x00317460
		protected virtual void InitUI_GameSpeed()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnGameSpeed, new UnityAction(this.SendGameSpeed2X));
		}

		// Token: 0x0600917E RID: 37246 RVA: 0x0031927A File Offset: 0x0031747A
		protected virtual void InitUI_HideUI(GameObject cNUF_GAME_PREFAB)
		{
			NKCUtil.SetButtonClickDelegate(this.m_btnUnhide, new UnityAction(this.HUD_UNHIDE));
			NKCUtil.SetGameobjectActive(this.m_objUnhide, false);
			NKCUtil.SetButtonClickDelegate(this.m_btnHide, new UnityAction(this.OnBtnHideHud));
		}

		// Token: 0x0600917F RID: 37247 RVA: 0x003192B8 File Offset: 0x003174B8
		protected virtual void InitUI_TeamDeck()
		{
			this.m_NKCUIMainHPGageAlly.InitUI();
			this.m_NKCUIMainHPGageEnemy.InitUI();
			this.m_NKCUIMainHPGageAllyLong.InitUI();
			this.m_NKCUIMainHPGageEnemyLong.InitUI();
			NKCUtil.SetGameobjectActive(this.m_csbtnDeadlineBuff, false);
			NKCUtil.SetButtonPointerDownDelegate(this.m_csbtnDeadlineBuff, new UnityAction<PointerEventData>(this.OnClickedDeadlineBuff));
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				this.m_NKCUIHudDeck[i].InitUI(this, i);
			}
			this.m_NKCUIHudDeck[5].SetActive(false, false);
			NKCUtil.SetLabelText(this.m_lbRemainUnitCount, "");
			for (int j = 0; j < this.m_NKCUIHudShipSkillDeck.Length; j++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[j];
				if (!(nkcuihudShipSkillDeck == null))
				{
					nkcuihudShipSkillDeck.InitUI(j);
				}
			}
			Transform transform = base.transform.Find("NUF_GAME_HUD_TACTICAL_COMMAND");
			if (transform != null)
			{
				for (int k = 0; k < this.m_NKCUIHudTacticalCommandDeck.Length; k++)
				{
					NKCUIHudTacticalCommandDeck nkcuihudTacticalCommandDeck = new NKCUIHudTacticalCommandDeck();
					this.m_NKCUIHudTacticalCommandDeck[k] = nkcuihudTacticalCommandDeck;
					nkcuihudTacticalCommandDeck.InitUI(this, transform.gameObject, k);
				}
			}
		}

		// Token: 0x06009180 RID: 37248 RVA: 0x003193D4 File Offset: 0x003175D4
		protected virtual void InitUI_Practice()
		{
			if (this.m_GameHudPractice != null)
			{
				this.m_GameHudPractice.Init(this);
			}
		}

		// Token: 0x06009181 RID: 37249 RVA: 0x003193F0 File Offset: 0x003175F0
		protected virtual void InitUI_AutoSkill()
		{
			NKCUtil.SetButtonClickDelegate(this.m_btnAutoHyper, new UnityAction(this.SendGameAutoSkillChange));
		}

		// Token: 0x06009182 RID: 37250 RVA: 0x00319409 File Offset: 0x00317609
		protected virtual void InitUI_AutoRespawn()
		{
			NKCUtil.SetButtonClickDelegate(this.m_btnAutoRespawn, new UnityAction(NKCScenManager.GetScenManager().m_NKCSystemEvent.UI_HUD_AUTO_RESPAWN));
			this.ToggleAutoRespawn(false);
			this.m_imgAutoRespawnOff.color = NKCUtil.GetColor("#FFFFFF");
		}

		// Token: 0x06009183 RID: 37251 RVA: 0x00319447 File Offset: 0x00317647
		protected virtual void InitUI_MessageAlertTimeover()
		{
			NKCUtil.SetGameobjectActive(this.m_objHUDMessage, false);
			NKCUtil.SetGameobjectActive(this.m_AlertEnv, false);
			NKCUtil.SetGameobjectActive(this.m_AlertPhase, false);
			NKCUtil.SetGameobjectActive(this.m_objTimeOver, false);
		}

		// Token: 0x06009184 RID: 37252 RVA: 0x00319479 File Offset: 0x00317679
		protected virtual void InitUI_Pause(GameObject cNUF_GAME_PREFAB)
		{
			NKCUtil.SetButtonClickDelegate(this.m_btnPause, new UnityAction(NKCScenManager.GetScenManager().m_NKCSystemEvent.UI_GAME_PAUSE));
		}

		// Token: 0x06009185 RID: 37253 RVA: 0x0031949C File Offset: 0x0031769C
		protected virtual void InitUI_HudTop()
		{
			NKCUtil.SetGameobjectActive(this.m_objHUDBgFx, false);
			NKCUtil.SetButtonClickDelegate(this.m_btnLeftUser, new UnityAction(NKCScenManager.GetScenManager().m_NKCSystemEvent.UI_GAME_NO_HP_DMG_MODE_TEAM_A));
			NKCUtil.SetButtonClickDelegate(this.m_btnRightUser, new UnityAction(NKCScenManager.GetScenManager().m_NKCSystemEvent.UI_GAME_NO_HP_DMG_MODE_TEAM_B));
		}

		// Token: 0x06009186 RID: 37254 RVA: 0x003194F5 File Offset: 0x003176F5
		protected virtual void InitUI_Repeat()
		{
			this.m_NKCGameHUDRepeatOperation.InitUI();
			NKCUtil.SetGameobjectActive(this.m_NKCGameHUDRepeatOperation, false);
		}

		// Token: 0x06009187 RID: 37255 RVA: 0x0031950E File Offset: 0x0031770E
		protected virtual void InitUI_Multiply()
		{
			NKCUtil.SetGameobjectActive(this.m_objMultiplyReward, false);
		}

		// Token: 0x06009188 RID: 37256 RVA: 0x0031951C File Offset: 0x0031771C
		protected virtual void InitUI_Operator()
		{
			NKCUtil.SetGameobjectActive(this.m_objOperatorPanelRoot, false);
		}

		// Token: 0x06009189 RID: 37257 RVA: 0x0031952C File Offset: 0x0031772C
		public void SetGameClient(NKCGameClient cGameClient)
		{
			this.m_GameClient = cGameClient;
			if (this.m_NKCUIHudRespawnGage != null)
			{
				this.m_NKCUIHudRespawnGage.SetData();
			}
			if (this.m_NKCUIMainHPGageAlly != null)
			{
				this.m_NKCUIMainHPGageAlly.InitData();
			}
			if (this.m_NKCUIMainHPGageEnemy != null)
			{
				this.m_NKCUIMainHPGageEnemy.InitData();
			}
			if (this.m_NKCUIMainHPGageAllyLong != null)
			{
				this.m_NKCUIMainHPGageAllyLong.InitData();
			}
			if (this.m_NKCUIMainHPGageEnemyLong != null)
			{
				this.m_NKCUIMainHPGageEnemyLong.InitData();
			}
			NKCUtil.SetGameobjectActive(this.m_objLeftUserRage, false);
			NKCUtil.SetGameobjectActive(this.m_objRightUserRage, false);
			if (this.m_NKCUIMainSkillCoolLeft != null)
			{
				this.m_NKCUIMainSkillCoolLeft.SetSkillCoolVisible(false);
				this.m_NKCUIMainSkillCoolLeft.SetHyperCoolVisible(false);
			}
			if (this.m_NKCUIMainSkillCoolRight != null)
			{
				this.m_NKCUIMainSkillCoolRight.SetSkillCoolVisible(false);
				this.m_NKCUIMainSkillCoolRight.SetHyperCoolVisible(false);
			}
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null)
				{
					nkcgameHudDeckSlot.Init();
				}
			}
			for (int j = 0; j < this.m_NKCUIHudShipSkillDeck.Length; j++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[j];
				if (nkcuihudShipSkillDeck != null)
				{
					nkcuihudShipSkillDeck.Init();
				}
			}
			for (int k = 0; k < this.m_NKCUIHudTacticalCommandDeck.Length; k++)
			{
				NKCUIHudTacticalCommandDeck nkcuihudTacticalCommandDeck = this.m_NKCUIHudTacticalCommandDeck[k];
				if (nkcuihudTacticalCommandDeck != null)
				{
					nkcuihudTacticalCommandDeck.Init();
				}
			}
			this.m_SelectUnitDeckIndex = -1;
			this.m_SelectShipSkillDeckIndex = -1;
			this.m_RemainGameTimeInt = 0;
		}

		// Token: 0x0600918A RID: 37258 RVA: 0x003196BC File Offset: 0x003178BC
		public void LoadUI(NKMGameData cNKMGameData)
		{
			if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				if (this.m_GameHudPractice != null)
				{
					this.m_GameHudPractice.SetEnable(true);
				}
				this.m_objTop.SetActive(false);
			}
			else
			{
				if (this.m_GameHudPractice != null)
				{
					this.m_GameHudPractice.SetEnable(false);
				}
				this.m_objTop.SetActive(true);
			}
			this.LoadDeck(cNKMGameData);
			this.LoadUserIcon(cNKMGameData);
			this.LoadArtifact(cNKMGameData);
			this.LoadRepeatOperation(cNKMGameData);
			this.LoadEmoticon(cNKMGameData);
			this.LoadPause(cNKMGameData);
			this.LoadDangerMessage(cNKMGameData);
			this.LoadMultiplyReward(cNKMGameData);
			this.LoadFierceBattleScore(cNKMGameData);
			this.LoadTrimBattleScore(cNKMGameData);
			this.LoadCombo();
			if (cNKMGameData.IsPVP())
			{
				this.MakeSummonIndicator();
				this.MakeSummonIndicator();
			}
			this.PrepareAlerts(cNKMGameData);
		}

		// Token: 0x0600918B RID: 37259 RVA: 0x00319789 File Offset: 0x00317989
		private void LoadRepeatOperation(NKMGameData cNKMGameData)
		{
			this.m_NKCGameHUDRepeatOperation.SetUI(cNKMGameData);
		}

		// Token: 0x0600918C RID: 37260 RVA: 0x00319798 File Offset: 0x00317998
		private void LoadArtifact(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (NKCGameHUDArtifact.GetActive(cNKMGameData))
			{
				if (this.m_NKCGameHUDArtifact == null)
				{
					if (this.m_NKCAssetInstanceDataArtifact != null)
					{
						NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataArtifact);
					}
					this.m_NKCAssetInstanceDataArtifact = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_ARTIFACT", false, null);
					NKCGameHUDArtifact componentInChildren = this.m_NKCAssetInstanceDataArtifact.m_Instant.GetComponentInChildren<NKCGameHUDArtifact>();
					this.m_NKCGameHUDArtifact = componentInChildren;
					this.m_NKCAssetInstanceDataArtifact.m_Instant.transform.SetParent(base.transform, false);
					this.m_NKCAssetInstanceDataArtifact.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataArtifact.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataArtifact.m_Instant.transform.localPosition.y, 0f);
					componentInChildren.gameObject.SetActive(false);
					return;
				}
			}
			else
			{
				this.m_NKCGameHUDArtifact = null;
			}
		}

		// Token: 0x0600918D RID: 37261 RVA: 0x0031988C File Offset: 0x00317A8C
		private void LoadFierceBattleScore(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (cNKMGameData.GetGameType() != NKM_GAME_TYPE.NGT_FIERCE)
			{
				this.m_NKCGameHUDFierce = null;
				return;
			}
			if (this.m_NKCGameHUDFierce == null)
			{
				if (this.m_NKCAssetInstanceDataFierceScore != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataFierceScore);
				}
				this.m_NKCAssetInstanceDataFierceScore = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_FIERCE_BATTLE", false, null);
				this.m_NKCGameHUDFierce = this.m_NKCAssetInstanceDataFierceScore.m_Instant.GetComponentInChildren<NKCGameHUDFierceScore>();
				this.m_NKCAssetInstanceDataFierceScore.m_Instant.transform.SetParent(base.transform, false);
				this.m_NKCAssetInstanceDataFierceScore.m_Instant.transform.SetAsFirstSibling();
				this.m_NKCAssetInstanceDataFierceScore.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataFierceScore.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataFierceScore.m_Instant.transform.localPosition.y, 0f);
				this.m_NKCGameHUDFierce.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600918E RID: 37262 RVA: 0x00319995 File Offset: 0x00317B95
		public void SetFierceBattleScore(int fierceScore)
		{
			if (this.m_NKCGameHUDFierce != null)
			{
				this.m_NKCGameHUDFierce.SetData(fierceScore, NKCGameHUDFierceScore.SCORE_TYPE.FIERCE);
			}
		}

		// Token: 0x0600918F RID: 37263 RVA: 0x003199B4 File Offset: 0x00317BB4
		private void LoadTrimBattleScore(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (cNKMGameData.GetGameType() != NKM_GAME_TYPE.NGT_TRIM)
			{
				this.m_NKCGameHUDTrim = null;
				return;
			}
			if (this.m_NKCGameHUDTrim == null)
			{
				if (this.m_NKCAssetInstanceDataTrimScore != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataTrimScore);
				}
				this.m_NKCAssetInstanceDataTrimScore = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_FIERCE_BATTLE", false, null);
				this.m_NKCGameHUDTrim = this.m_NKCAssetInstanceDataTrimScore.m_Instant.GetComponentInChildren<NKCGameHUDFierceScore>();
				this.m_NKCAssetInstanceDataTrimScore.m_Instant.transform.SetParent(base.transform, false);
				this.m_NKCAssetInstanceDataTrimScore.m_Instant.transform.SetAsFirstSibling();
				this.m_NKCAssetInstanceDataTrimScore.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataTrimScore.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataTrimScore.m_Instant.transform.localPosition.y, 0f);
				this.m_NKCGameHUDTrim.gameObject.SetActive(false);
			}
		}

		// Token: 0x06009190 RID: 37264 RVA: 0x00319ABD File Offset: 0x00317CBD
		public void SetTrimBattleScore(int trimScore)
		{
			if (this.m_NKCGameHUDTrim != null)
			{
				this.m_NKCGameHUDTrim.SetData(trimScore, NKCGameHUDFierceScore.SCORE_TYPE.TRIM);
			}
		}

		// Token: 0x06009191 RID: 37265 RVA: 0x00319ADC File Offset: 0x00317CDC
		private void LoadEmoticon(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (cNKMGameData.IsPVP())
			{
				if (this.m_NKCGameHudEmoticon == null)
				{
					if (this.m_NKCAssetInstanceDataEmoticon != null)
					{
						NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataEmoticon);
					}
					this.m_NKCAssetInstanceDataEmoticon = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_EMOTICON", false, null);
					NKCGameHudEmoticon componentInChildren = this.m_NKCAssetInstanceDataEmoticon.m_Instant.GetComponentInChildren<NKCGameHudEmoticon>();
					this.m_NKCGameHudEmoticon = componentInChildren;
					this.m_NKCAssetInstanceDataEmoticon.m_Instant.transform.SetParent(this.m_HudObjects.m_NUF_GAME_HUD_UI_EMOTICON.transform, false);
					this.m_NKCAssetInstanceDataEmoticon.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataEmoticon.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataEmoticon.m_Instant.transform.localPosition.y, 0f);
					NKCUtil.SetGameobjectActive(componentInChildren.gameObject, true);
					return;
				}
			}
			else
			{
				this.m_NKCGameHudEmoticon = null;
			}
		}

		// Token: 0x06009192 RID: 37266 RVA: 0x00319BD8 File Offset: 0x00317DD8
		public bool IsOpenPause()
		{
			return this.m_NKCGameHudPause != null && this.m_NKCGameHudPause.IsOpen();
		}

		// Token: 0x06009193 RID: 37267 RVA: 0x00319BF5 File Offset: 0x00317DF5
		public void OpenPause(NKCGameHudPause.dOnClickContinue _dOnClickContinue = null)
		{
			if (this.m_NKCGameHudPause != null)
			{
				this.m_NKCGameHudPause.Open(_dOnClickContinue);
			}
		}

		// Token: 0x06009194 RID: 37268 RVA: 0x00319C11 File Offset: 0x00317E11
		public void OnClickContinueOnPause()
		{
			if (this.m_NKCGameHudPause != null && this.m_NKCGameHudPause.IsOpen())
			{
				this.m_NKCGameHudPause.OnClickContinue();
			}
		}

		// Token: 0x06009195 RID: 37269 RVA: 0x00319C39 File Offset: 0x00317E39
		public void ClosePause()
		{
			if (this.m_NKCGameHudPause != null)
			{
				this.m_NKCGameHudPause.Close();
			}
		}

		// Token: 0x06009196 RID: 37270 RVA: 0x00319C54 File Offset: 0x00317E54
		protected virtual bool CheckLoadPause(NKMGameData cNKMGameData)
		{
			NKCGameHud.HUDMode eMode = this.m_eMode;
			if (eMode != NKCGameHud.HUDMode.Normal)
			{
				if (eMode != NKCGameHud.HUDMode.Replay)
				{
				}
			}
			else if (NKMGame.IsPVP(cNKMGameData.GetGameType()))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06009197 RID: 37271 RVA: 0x00319C84 File Offset: 0x00317E84
		private void LoadPause(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (!this.CheckLoadPause(cNKMGameData))
			{
				this.m_NKCGameHudPause = null;
				return;
			}
			if (this.m_NKCGameHudPause == null)
			{
				if (this.m_NKCAssetInstanceDataPause != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataPause);
				}
				this.m_NKCAssetInstanceDataPause = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_PAUSE", false, null);
				NKCGameHudPause componentInChildren = this.m_NKCAssetInstanceDataPause.m_Instant.GetComponentInChildren<NKCGameHudPause>();
				this.m_NKCGameHudPause = componentInChildren;
				this.m_NKCAssetInstanceDataPause.m_Instant.transform.SetParent(this.m_HudObjects.m_NUF_GAME_HUD_UI_PAUSE.transform, false);
				this.m_NKCAssetInstanceDataPause.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataPause.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataPause.m_Instant.transform.localPosition.y, 0f);
				NKCUtil.SetGameobjectActive(componentInChildren.gameObject, false);
			}
		}

		// Token: 0x06009198 RID: 37272 RVA: 0x00319D80 File Offset: 0x00317F80
		public void Clear()
		{
			if (this.m_NKCAssetInstanceDataArtifact != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataArtifact);
			}
			this.m_NKCAssetInstanceDataArtifact = null;
			this.m_NKCGameHUDArtifact = null;
			if (this.m_NKCGameHudEmoticon != null)
			{
				this.m_NKCGameHudEmoticon.Clear();
			}
			if (this.m_NKCAssetInstanceDataEmoticon != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataEmoticon);
			}
			this.m_NKCAssetInstanceDataEmoticon = null;
			this.m_NKCGameHudEmoticon = null;
			if (this.m_NKCAssetInstanceDataPause != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataPause);
			}
			this.m_NKCAssetInstanceDataPause = null;
			this.m_NKCGameHudPause = null;
			if (this.m_NKCUIDangerMessage != null)
			{
				this.m_NKCUIDangerMessage.Clear();
			}
			if (this.m_NKCAssetInstanceDataDangerMessage != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataDangerMessage);
			}
			this.m_NKCAssetInstanceDataDangerMessage = null;
			this.m_NKCUIDangerMessage = null;
			if (this.m_NKCAssetInstanceDataMultiplyReward != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMultiplyReward);
			}
			this.m_NKCAssetInstanceDataMultiplyReward = null;
			this.m_NKCUIGameHUDMultiplyReward = null;
			if (this.m_NKCAssetInstanceDataFierceScore != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataFierceScore);
			}
			this.m_NKCAssetInstanceDataFierceScore = null;
			this.m_NKCGameHUDFierce = null;
			if (this.m_NKCAssetInstanceDataTrimScore != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataTrimScore);
			}
			this.m_NKCAssetInstanceDataTrimScore = null;
			this.m_NKCGameHUDTrim = null;
			if (this.m_NKCAssetInstanceDataCombo != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataCombo);
			}
			this.m_NKCAssetInstanceDataCombo = null;
			this.m_NKCGameHudCombo = null;
			this.CleanupAllHudAlert();
		}

		// Token: 0x06009199 RID: 37273 RVA: 0x00319ECD File Offset: 0x003180CD
		public void SetUserIconColor(bool bTeamA, Color _color)
		{
			if (bTeamA)
			{
				this.m_imgLeftUser.color = _color;
				return;
			}
			this.m_imgRightUser.color = _color;
		}

		// Token: 0x0600919A RID: 37274 RVA: 0x00319EEB File Offset: 0x003180EB
		public void SetAutoEnable()
		{
			NKCUtil.SetGameobjectActive(this.m_objAutoRespawn, this.IsAutoRespawnVisible());
			NKCUtil.SetImageColor(this.m_imgAutoRespawnOff, NKCUtil.GetColor(this.IsAutoRespawnUsable() ? "#FFFFFF" : "#7B7B7B"));
		}

		// Token: 0x0600919B RID: 37275 RVA: 0x00319F24 File Offset: 0x00318124
		public void SetMultiply(int multiply)
		{
			if (multiply > 1)
			{
				NKCGameHUDRepeatOperation nkcgameHUDRepeatOperation = this.m_NKCGameHUDRepeatOperation;
				if (nkcgameHUDRepeatOperation != null)
				{
					nkcgameHUDRepeatOperation.SetDisable();
				}
				if (this.m_NKCUIGameHUDMultiplyReward != null)
				{
					this.m_NKCUIGameHUDMultiplyReward.SetMultiply(multiply);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objMultiplyReward, multiply > 1);
			NKCUtil.SetLabelText(this.m_lbRewardMultiply, NKCUtilString.GET_MULTIPLY_REWARD_ONE_PARAM, new object[]
			{
				multiply
			});
			this.bReservedMultiply = (multiply > 1);
		}

		// Token: 0x0600919C RID: 37276 RVA: 0x00319F98 File Offset: 0x00318198
		public NKCUIComButton GetAutoButton()
		{
			return this.m_btnAutoRespawn;
		}

		// Token: 0x0600919D RID: 37277 RVA: 0x00319FA0 File Offset: 0x003181A0
		public void TogglePause(bool bSet, bool bLocalServer)
		{
			if (this.m_imgPause != null)
			{
				if (bSet)
				{
					this.m_imgPause.color = NKCUtil.GetColor("#FFD428");
				}
				else
				{
					this.m_imgPause.color = Color.white;
				}
			}
			NKCUtil.SetGameobjectActive(NKCUIManager.m_NUF_GAME_TOUCH_OBJECT, !bSet);
		}

		// Token: 0x0600919E RID: 37278 RVA: 0x00319FF4 File Offset: 0x003181F4
		public void SetAttackPointUI(NKMGameData cNKMGameData)
		{
			this.m_fAttackPointLeftCurrValue = 0f;
			this.m_AttackPointLeftTargetValue = 0;
			this.m_fElapsedTimeForAP = 0f;
			if (cNKMGameData == null)
			{
				return;
			}
			bool bValue = false;
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
				if (dungeonTempletBase != null && (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE || dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_RAID || dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID))
				{
					bValue = true;
					this.m_lbAttackPointNow.text = "";
					this.m_lbAttackPointMax.text = "";
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objAttackPoint, bValue);
		}

		// Token: 0x0600919F RID: 37279 RVA: 0x0031A084 File Offset: 0x00318284
		public void SetCurrentWaveUI(int waveID)
		{
			if (this.m_lbWave != null)
			{
				this.m_lbWave.text = waveID.ToString() + "/" + this.m_WaveCount.ToString();
			}
		}

		// Token: 0x060091A0 RID: 37280 RVA: 0x0031A0BC File Offset: 0x003182BC
		public void SetWaveUI(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			bool bValue = false;
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMGameData.m_DungeonID);
				if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase != null && dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					bValue = true;
					if (dungeonTemplet.m_listDungeonWave != null)
					{
						this.m_WaveCount = dungeonTemplet.m_listDungeonWave.Count;
						int num = 0;
						this.m_lbWave.text = num.ToString() + "/" + this.m_WaveCount.ToString();
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objWave, bValue);
		}

		// Token: 0x060091A1 RID: 37281 RVA: 0x0031A14C File Offset: 0x0031834C
		public void SetGageUI(NKMGameData cNKMGameData)
		{
			bool flag = false;
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
				if (dungeonTempletBase != null)
				{
					if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE)
					{
						this.m_NKCUIMainHPGageEnemyLong.SetMainGageVisible(true);
						this.m_NKCUIMainHPGageAllyLong.SetMainGageVisible(false);
						this.m_NKCUIMainHPGageEnemy.SetMainGageVisible(false);
						this.m_NKCUIMainHPGageAlly.SetMainGageVisible(false);
						flag = true;
					}
					else if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
					{
						this.m_NKCUIMainHPGageEnemyLong.SetMainGageVisible(false);
						this.m_NKCUIMainHPGageAllyLong.SetMainGageVisible(true);
						this.m_NKCUIMainHPGageEnemy.SetMainGageVisible(false);
						this.m_NKCUIMainHPGageAlly.SetMainGageVisible(false);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.m_NKCUIMainHPGageEnemy.SetMainGageVisible(true);
				this.m_NKCUIMainHPGageAlly.SetMainGageVisible(true);
				this.m_NKCUIMainHPGageEnemyLong.SetMainGageVisible(false);
				this.m_NKCUIMainHPGageAllyLong.SetMainGageVisible(false);
			}
		}

		// Token: 0x060091A2 RID: 37282 RVA: 0x0031A220 File Offset: 0x00318420
		public void SetAttackPoint(bool bLeft, int value)
		{
			if (bLeft)
			{
				if (value == 0)
				{
					this.m_fAttackPointLeftPivotValue = (float)value;
					this.m_fAttackPointLeftCurrValue = (float)value;
					this.m_lbAttackPointNow.text = value.ToString();
				}
				else if (value != this.m_AttackPointLeftTargetValue)
				{
					this.m_fElapsedTimeForAP = 0f;
					this.m_fAttackPointLeftPivotValue = this.m_fAttackPointLeftCurrValue;
				}
				this.m_AttackPointLeftTargetValue = value;
				return;
			}
			this.m_lbAttackPointMax.text = "/" + value.ToString("N0");
		}

		// Token: 0x060091A3 RID: 37283 RVA: 0x0031A2A0 File Offset: 0x003184A0
		public void SetShowUI(bool bShowUI, bool bDev)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bShowUI);
		}

		// Token: 0x060091A4 RID: 37284 RVA: 0x0031A2B0 File Offset: 0x003184B0
		private void LoadUserIcon(NKMGameData cNKMGameData)
		{
			NKMUnitData nkmunitData = cNKMGameData.m_NKMGameTeamDataA.GetUnitDataByUnitUID(cNKMGameData.m_NKMGameTeamDataA.m_LeaderUnitUID);
			NKMUnitData nkmunitData2 = cNKMGameData.m_NKMGameTeamDataB.GetUnitDataByUnitUID(cNKMGameData.m_NKMGameTeamDataB.m_LeaderUnitUID);
			if (nkmunitData != null)
			{
				this.LoadUnitFaceDeckAsset(nkmunitData);
			}
			if (nkmunitData2 != null)
			{
				this.LoadUnitFaceDeckAsset(nkmunitData2);
			}
			if (this.IsFristUnitMain(cNKMGameData.GetGameType()))
			{
				nkmunitData = cNKMGameData.m_NKMGameTeamDataA.GetFirstUnitData();
				nkmunitData2 = cNKMGameData.m_NKMGameTeamDataB.GetFirstUnitData();
				if (nkmunitData != null)
				{
					this.LoadUnitFaceDeckAsset(nkmunitData);
				}
				if (nkmunitData2 != null)
				{
					this.LoadUnitFaceDeckAsset(nkmunitData2);
				}
			}
			NKCResourceUtility.PreloadUnitInvenIconEmpty();
		}

		// Token: 0x060091A5 RID: 37285 RVA: 0x0031A33E File Offset: 0x0031853E
		private void LoadUnitFaceDeckAsset(NKMUnitData unitData)
		{
			NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitData, true);
		}

		// Token: 0x060091A6 RID: 37286 RVA: 0x0031A348 File Offset: 0x00318548
		protected virtual void LoadDeck(NKMGameData cNKMGameData)
		{
			NKMGameTeamData teamData = cNKMGameData.GetTeamData(this.CurrentViewTeamType);
			if (teamData != null)
			{
				this.LoadDeck(teamData);
			}
			if (this.m_eMode == NKCGameHud.HUDMode.Observer)
			{
				NKM_TEAM_TYPE myTeamType = NKM_TEAM_TYPE.NTT_INVALID;
				NKM_TEAM_TYPE myTeam = this.GetGameClient().m_MyTeam;
				if (myTeam - NKM_TEAM_TYPE.NTT_A1 > 1)
				{
					if (myTeam - NKM_TEAM_TYPE.NTT_B1 <= 1)
					{
						myTeamType = NKM_TEAM_TYPE.NTT_A1;
					}
				}
				else
				{
					myTeamType = NKM_TEAM_TYPE.NTT_B1;
				}
				NKMGameTeamData teamData2 = cNKMGameData.GetTeamData(myTeamType);
				if (teamData != null)
				{
					this.LoadDeck(teamData2);
				}
			}
		}

		// Token: 0x060091A7 RID: 37287 RVA: 0x0031A3AC File Offset: 0x003185AC
		protected void LoadDeck(NKMGameTeamData cNKMGameTeamData)
		{
			NKMUnitTemplet nkmunitTemplet = null;
			if (cNKMGameTeamData.m_MainShip != null)
			{
				nkmunitTemplet = NKMUnitManager.GetUnitTemplet(cNKMGameTeamData.m_MainShip.m_UnitID);
				this.LoadTacticalCommandDeck(cNKMGameTeamData);
			}
			if (nkmunitTemplet != null)
			{
				this.m_objRootShipSkill.SetActive(true);
				this.LoadShipSkillDeck(nkmunitTemplet);
			}
			else
			{
				this.m_objRootShipSkill.SetActive(false);
			}
			for (int i = 0; i < cNKMGameTeamData.m_listUnitData.Count; i++)
			{
				NKMUnitData nkmunitData = cNKMGameTeamData.m_listUnitData[i];
				if (nkmunitData != null)
				{
					this.LoadUnitDeck(nkmunitData, true);
				}
			}
			for (int j = 0; j < cNKMGameTeamData.m_listAssistUnitData.Count; j++)
			{
				NKMUnitData nkmunitData2 = cNKMGameTeamData.m_listAssistUnitData[j];
				if (nkmunitData2 != null)
				{
					this.LoadUnitDeck(nkmunitData2, true);
				}
			}
		}

		// Token: 0x060091A8 RID: 37288 RVA: 0x0031A45E File Offset: 0x0031865E
		public void LoadUnitDeck(NKMUnitData cNKMUnitData, bool bAsync)
		{
			NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitData, true);
		}

		// Token: 0x060091A9 RID: 37289 RVA: 0x0031A468 File Offset: 0x00318668
		public void LoadUnitDeck(NKMUnitTemplet cNKMUnitTemplet, bool bAsync)
		{
			NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitTemplet.m_UnitTempletBase, true);
		}

		// Token: 0x060091AA RID: 37290 RVA: 0x0031A478 File Offset: 0x00318678
		private void LoadShipSkillDeck(NKMUnitTemplet cNKMUnitTemplet)
		{
			if (cNKMUnitTemplet != null)
			{
				for (int i = 0; i < cNKMUnitTemplet.m_UnitTempletBase.m_lstSkillStrID.Count; i++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(cNKMUnitTemplet.m_UnitTempletBase, i);
					if (shipSkillTempletByIndex != null)
					{
						NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_SHIP_SKILL_ICON", shipSkillTempletByIndex.m_ShipSkillIcon, true);
					}
				}
			}
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_SHIP_SKILL_ICON", "SS_NO_SKILL_ICON", true);
		}

		// Token: 0x060091AB RID: 37291 RVA: 0x0031A4D4 File Offset: 0x003186D4
		private void LoadTacticalCommandDeck(NKMGameTeamData cNKMGameTeamData)
		{
			if (cNKMGameTeamData != null)
			{
				for (int i = 0; i < cNKMGameTeamData.m_listTacticalCommandData.Count; i++)
				{
					NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)cNKMGameTeamData.m_listTacticalCommandData[i].m_TCID);
					if (tacticalCommandTempletByID != null)
					{
						NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_TACTICAL_COMMAND_ICON", tacticalCommandTempletByID.m_TCIconName, true);
					}
				}
			}
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_TACTICAL_COMMAND_ICON", "ICON_TC_NO_SKILL_ICON", true);
		}

		// Token: 0x060091AC RID: 37292 RVA: 0x0031A538 File Offset: 0x00318738
		private void LoadDangerMessage(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_2X, 0, 0))
			{
				return;
			}
			if (NKCTutorialManager.IsTutorialDungeon(cNKMGameData.m_DungeonID))
			{
				return;
			}
			if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (this.m_GameClient.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
			{
				if (this.m_NKCUIDangerMessage == null)
				{
					if (this.m_NKCAssetInstanceDataDangerMessage != null)
					{
						NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataDangerMessage);
					}
					this.m_NKCAssetInstanceDataDangerMessage = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_UI_DANGER", "AB_FX_UI_DANGER_MESSAGE", false, null);
					NKCUIDangerMessage component = this.m_NKCAssetInstanceDataDangerMessage.m_Instant.GetComponent<NKCUIDangerMessage>();
					this.m_NKCUIDangerMessage = component;
					this.m_NKCAssetInstanceDataDangerMessage.m_Instant.transform.SetParent(base.transform, false);
					this.m_NKCAssetInstanceDataDangerMessage.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataDangerMessage.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataDangerMessage.m_Instant.transform.localPosition.y, 0f);
					NKCUtil.SetGameobjectActive(component, false);
					return;
				}
			}
			else
			{
				this.m_NKCUIDangerMessage = null;
			}
		}

		// Token: 0x060091AD RID: 37293 RVA: 0x0031A650 File Offset: 0x00318850
		private void LoadMultiplyReward(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0))
			{
				return;
			}
			if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (this.m_NKCUIGameHUDMultiplyReward == null)
			{
				if (this.m_NKCAssetInstanceDataMultiplyReward != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMultiplyReward);
				}
				this.m_NKCAssetInstanceDataMultiplyReward = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_MULTIPLY_REWARD", false, null);
				NKCUIGameHUDMultiplyReward component = this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.GetComponent<NKCUIGameHUDMultiplyReward>();
				this.m_NKCUIGameHUDMultiplyReward = component;
				this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.SetParent(base.transform, false);
				this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.localPosition.y, 0f);
				NKCUtil.SetGameobjectActive(component, false);
			}
		}

		// Token: 0x060091AE RID: 37294 RVA: 0x0031A740 File Offset: 0x00318940
		private void LoadCombo()
		{
			if (this.m_GameClient == null)
			{
				return;
			}
			NKMGameTeamData myTeamData = this.m_GameClient.GetMyTeamData();
			if (myTeamData == null)
			{
				return;
			}
			if (myTeamData.GetTC_Combo() == null)
			{
				return;
			}
			if (this.m_NKCAssetInstanceDataCombo == null && this.m_objOperatorPanelRoot != null)
			{
				if (this.m_NKCAssetInstanceDataCombo != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataCombo);
				}
				this.m_NKCAssetInstanceDataCombo = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_OPERATOR", false, null);
				NKCGameHudCombo component = this.m_NKCAssetInstanceDataCombo.m_Instant.GetComponent<NKCGameHudCombo>();
				this.m_NKCGameHudCombo = component;
				this.m_NKCAssetInstanceDataCombo.m_Instant.transform.SetParent(this.m_objOperatorPanelRoot.transform, false);
				this.m_NKCAssetInstanceDataCombo.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataCombo.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataCombo.m_Instant.transform.localPosition.y, 0f);
				NKCUtil.SetGameobjectActive(this.m_objOperatorPanelRoot, true);
				NKCUtil.SetGameobjectActive(component, false);
			}
		}

		// Token: 0x060091AF RID: 37295 RVA: 0x0031A854 File Offset: 0x00318A54
		public void LoadComplete(NKMGameData cNKMGameData)
		{
			this.SetDeck(cNKMGameData);
			this.SetUserInfoUI(cNKMGameData);
			NKMGameTeamData teamData = cNKMGameData.GetTeamData(this.CurrentViewTeamType);
			if (teamData != null)
			{
				this.SetShipSkillDeck(teamData.m_MainShip);
			}
			NKCUtil.SetGameobjectActive(this.m_objHUDMessage, false);
			NKCUtil.SetGameobjectActive(this.m_AlertEnv, false);
			NKCUtil.SetGameobjectActive(this.m_AlertPhase, false);
			this.m_objHUDBg.SetActive(false);
			this.m_objHUDBg.SetActive(true);
			this.m_NUF_GAME_HUD_MINI_MAP_RectTransform_width = this.m_rtMiniMap.rect.width;
			this.SetUserIconColor(true, new Color(1f, 1f, 1f));
			this.SetUserIconColor(false, new Color(1f, 1f, 1f));
			this.UnSelectUnitDeckAll();
			this.UnSelectShipSkillDeckAll();
			this.SetAttackPointUI(cNKMGameData);
			this.SetWaveUI(cNKMGameData);
			this.SetGageUI(cNKMGameData);
			this.SetMinimapUI(cNKMGameData);
			this.SetMainGageSkillCool(cNKMGameData);
			this.SetEnableRemainUnitCountUI(cNKMGameData);
			int remainSupplyOfTeamA = this.m_GameClient.GetRemainSupplyOfTeamA();
			this.m_NKCUIHudRespawnGage.SetSupply(remainSupplyOfTeamA);
			this.SetNetworkLatencyLevel(1);
			if (NKCTutorialManager.IsTutorialDungeon(cNKMGameData.m_DungeonID))
			{
				this.HideHud(true);
				NKCUtil.SetGameobjectActive(this.m_objHide, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objHide, true);
			}
			if (this.m_GameHudPractice != null)
			{
				this.m_GameHudPractice.LoadComplete(cNKMGameData);
			}
			if (this.m_NKCGameHUDArtifact != null)
			{
				this.m_NKCGameHUDArtifact.SetUI(cNKMGameData);
			}
			if (this.m_NKCGameHudEmoticon != null)
			{
				this.m_NKCGameHudEmoticon.SetUI(cNKMGameData);
			}
			if (this.m_NKCGameHudCombo != null)
			{
				this.m_NKCGameHudCombo.SetUI(cNKMGameData, this.m_GameClient.m_MyTeam);
			}
		}

		// Token: 0x060091B0 RID: 37296 RVA: 0x0031AA0C File Offset: 0x00318C0C
		public void SetNetworkLatencyLevel(int level)
		{
			if (this.m_lbNetworkLevel == null)
			{
				return;
			}
			float num = 1f - 0.1f * ((float)level - 1f);
			Color color = this.m_lbNetworkLevel.color;
			color.g = num;
			color.b = num;
			this.m_lbNetworkLevel.color = color;
			this.m_lbNetworkLevel.text = string.Format("{0}", level);
		}

		// Token: 0x060091B1 RID: 37297 RVA: 0x0031AA80 File Offset: 0x00318C80
		public void SetUIByRuntimeData(NKMGameData cNKMGameData, NKMGameRuntimeData cNKMGameRuntimeData)
		{
			if (cNKMGameData == null || cNKMGameRuntimeData == null)
			{
				return;
			}
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMGameData.m_DungeonID);
				if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase != null && dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					this.SetCurrentWaveUI(cNKMGameRuntimeData.m_WaveID);
				}
			}
		}

		// Token: 0x060091B2 RID: 37298 RVA: 0x0031AACD File Offset: 0x00318CCD
		public void UpdateRemainUnitCount(int count)
		{
			NKCUtil.SetLabelText(this.m_lbRemainUnitCount, NKCUtilString.GET_STRING_REMAIN_UNIT_COUNT_ONE_PARAM, new object[]
			{
				count
			});
		}

		// Token: 0x060091B3 RID: 37299 RVA: 0x0031AAF0 File Offset: 0x00318CF0
		private void SetEnableRemainUnitCountUI(NKMGameData cNKMGameData)
		{
			bool bValue = false;
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMGameData.m_DungeonID);
				if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase != null && (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_RAID || dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID))
				{
					bValue = true;
					NKMGameTeamData teamData = cNKMGameData.GetTeamData(NKM_TEAM_TYPE.NTT_A1);
					if (teamData != null)
					{
						this.UpdateRemainUnitCount(teamData.m_listUnitData.Count - teamData.m_DeckData.GetListUnitDeckTombCount());
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objRemainUnitCount, bValue);
		}

		// Token: 0x060091B4 RID: 37300 RVA: 0x0031AB70 File Offset: 0x00318D70
		private void SetMainGageSkillCool(NKMGameData cNKMGameData)
		{
			NKMUnitData unitDataByUnitUID = cNKMGameData.m_NKMGameTeamDataB.GetUnitDataByUnitUID(cNKMGameData.m_NKMGameTeamDataB.m_LeaderUnitUID);
			NKMUnitTemplet nkmunitTemplet = null;
			if (unitDataByUnitUID != null)
			{
				nkmunitTemplet = NKMUnitManager.GetUnitTemplet(unitDataByUnitUID.m_UnitID);
			}
			if (nkmunitTemplet != null && !NKMGame.IsPVP(cNKMGameData.m_NKM_GAME_TYPE))
			{
				this.m_NKCUIMainSkillCoolRight.SetUnit(nkmunitTemplet, unitDataByUnitUID);
			}
		}

		// Token: 0x060091B5 RID: 37301 RVA: 0x0031ABC4 File Offset: 0x00318DC4
		private void SetMinimapUI(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			bool bValue = true;
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMGameData.m_DungeonID);
				if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase != null)
				{
					if (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE || dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
					{
						if (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE)
						{
							bValue = false;
						}
					}
					else if (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_RAID || dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
					{
						bValue = false;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_rtMiniMap, bValue);
		}

		// Token: 0x060091B6 RID: 37302 RVA: 0x0031AC50 File Offset: 0x00318E50
		private void SetUserInfoUI(NKMGameData cNKMGameData)
		{
			this.m_lbLeftUserName.text = "";
			this.m_lbRightUserName.text = "";
			NKMUnitData nkmunitData = null;
			NKMUnitData nkmunitData2 = null;
			if (cNKMGameData.IsPVE())
			{
				nkmunitData = cNKMGameData.m_NKMGameTeamDataA.GetLeaderUnitData();
				nkmunitData2 = cNKMGameData.m_NKMGameTeamDataB.GetLeaderUnitData();
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
				if (dungeonTempletBase != null && (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_RAID || dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID) && nkmunitData == null)
				{
					nkmunitData = cNKMGameData.m_NKMGameTeamDataA.GetFirstUnitData();
				}
				int num = 0;
				int num2 = 0;
				if (nkmunitData != null)
				{
					num = nkmunitData.m_UnitLevel;
				}
				if (nkmunitData2 != null)
				{
					num2 = nkmunitData2.m_UnitLevel;
				}
				if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_DIVE)
					{
						NKMUnitData firstUnitData = cNKMGameData.m_NKMGameTeamDataB.GetFirstUnitData();
						if (firstUnitData != null)
						{
							num2 = firstUnitData.m_UnitLevel;
						}
						else
						{
							num2 = dungeonTempletBase.m_DungeonLevel;
						}
					}
					else
					{
						num2 = dungeonTempletBase.m_DungeonLevel;
					}
				}
				if (nkmunitData != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID);
					if (unitTempletBase != null)
					{
						this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
						this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_USER_A_NAME_TWO_PARAM, num, unitTempletBase.GetUnitName());
						this.m_lbLeftUserName.text = this.m_StringBuilder.ToString();
					}
				}
				if (nkmunitData2 != null)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID);
					if (unitTempletBase2 != null)
					{
						NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMGameData.m_DungeonID);
						string arg = (!string.IsNullOrEmpty(dungeonTemplet.m_BossUnitChangeName)) ? NKCStringTable.GetString(dungeonTemplet.m_BossUnitChangeName, false) : unitTempletBase2.GetUnitName();
						this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
						this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_USER_B_NAME_TWO_PARAM, num2, arg);
						this.m_lbRightUserName.text = this.m_StringBuilder.ToString();
					}
				}
				else
				{
					this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
					this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_USER_B_LEVEL_ONE_PARAM, num2);
					this.m_lbRightUserName.text = this.m_StringBuilder.ToString();
				}
			}
			else
			{
				NKM_TEAM_TYPE myTeam = this.m_GameClient.m_MyTeam;
				if (myTeam == NKM_TEAM_TYPE.NTT_A1)
				{
					nkmunitData = cNKMGameData.m_NKMGameTeamDataA.GetLeaderUnitData();
					if (this.IsFristUnitMain(this.m_GameClient.GetGameData().GetGameType()))
					{
						nkmunitData2 = cNKMGameData.m_NKMGameTeamDataB.GetFirstUnitData();
					}
					else
					{
						nkmunitData2 = cNKMGameData.m_NKMGameTeamDataB.GetLeaderUnitData();
					}
					if (nkmunitData != null && NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID) != null)
					{
						this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
						this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_TEAM_A_NAME_TWO_PARAM, cNKMGameData.m_NKMGameTeamDataA.m_UserLevel, NKCUtilString.GetUserNickname(cNKMGameData.m_NKMGameTeamDataA.m_UserNickname, false), cNKMGameData.m_NKMGameTeamDataA.m_Score);
						this.m_lbLeftUserName.text = this.m_StringBuilder.ToString();
					}
					if (nkmunitData2 != null && NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID) != null)
					{
						this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
						this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_TEAM_B_NAME_TWO_PARAM, cNKMGameData.m_NKMGameTeamDataB.m_UserLevel, NKCUtilString.GetUserNickname(cNKMGameData.m_NKMGameTeamDataB.m_UserNickname, true), cNKMGameData.m_NKMGameTeamDataB.m_Score);
						this.m_lbRightUserName.text = this.m_StringBuilder.ToString();
					}
				}
				else if (myTeam == NKM_TEAM_TYPE.NTT_B1)
				{
					nkmunitData = cNKMGameData.m_NKMGameTeamDataB.GetLeaderUnitData();
					if (this.IsFristUnitMain(this.m_GameClient.GetGameData().GetGameType()))
					{
						nkmunitData2 = cNKMGameData.m_NKMGameTeamDataA.GetFirstUnitData();
					}
					else
					{
						nkmunitData2 = cNKMGameData.m_NKMGameTeamDataA.GetLeaderUnitData();
					}
					if (nkmunitData != null && NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID) != null)
					{
						this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
						this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_TEAM_A_NAME_TWO_PARAM, cNKMGameData.m_NKMGameTeamDataA.m_UserLevel, NKCUtilString.GetUserNickname(cNKMGameData.m_NKMGameTeamDataA.m_UserNickname, true), cNKMGameData.m_NKMGameTeamDataA.m_Score);
						this.m_lbRightUserName.text = this.m_StringBuilder.ToString();
					}
					if (nkmunitData2 != null && NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID) != null)
					{
						this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
						this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_TEAM_B_NAME_TWO_PARAM, cNKMGameData.m_NKMGameTeamDataB.m_UserLevel, NKCUtilString.GetUserNickname(cNKMGameData.m_NKMGameTeamDataB.m_UserNickname, false), cNKMGameData.m_NKMGameTeamDataB.m_Score);
						this.m_lbLeftUserName.text = this.m_StringBuilder.ToString();
					}
				}
			}
			Image image = this.m_imgLeftUser;
			NKCAssetResourceData nkcassetResourceData = this.GetUnitFaceDeckAssetResourceData(nkmunitData);
			if (nkcassetResourceData != null)
			{
				image.sprite = nkcassetResourceData.GetAsset<Sprite>();
			}
			else
			{
				image.sprite = null;
			}
			image = this.m_imgRightUser;
			image.enabled = true;
			if (cNKMGameData.IsPVE())
			{
				NKMDungeonTempletBase dungeonTempletBase2 = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
				if (dungeonTempletBase2 != null && dungeonTempletBase2.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					image.enabled = false;
				}
				else
				{
					nkcassetResourceData = this.GetUnitFaceDeckAssetResourceData(nkmunitData2);
					if (nkcassetResourceData != null)
					{
						image.sprite = nkcassetResourceData.GetAsset<Sprite>();
					}
					else
					{
						image.sprite = null;
					}
				}
			}
			else
			{
				nkcassetResourceData = this.GetUnitFaceDeckAssetResourceData(nkmunitData2);
				if (nkcassetResourceData != null)
				{
					image.sprite = nkcassetResourceData.GetAsset<Sprite>();
				}
				else
				{
					nkcassetResourceData = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
					image.sprite = ((nkcassetResourceData != null) ? nkcassetResourceData.GetAsset<Sprite>() : null);
				}
			}
			if (nkmunitData != null)
			{
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID);
				if (unitTempletBase3 != null)
				{
					this.m_imgLeftUserRole.enabled = true;
					this.m_imgLeftUserRole.sprite = NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase3, true);
					this.m_imgLeftUserAttackType.enabled = true;
					this.m_imgLeftUserAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase3, true);
				}
				else
				{
					this.m_imgLeftUserRole.enabled = false;
					this.m_imgLeftUserAttackType.enabled = true;
				}
			}
			else
			{
				this.m_imgLeftUserRole.enabled = false;
				this.m_imgLeftUserAttackType.enabled = true;
			}
			if (nkmunitData2 == null)
			{
				this.m_imgRightUserRole.enabled = false;
				this.m_imgRightUserAttackType.enabled = false;
				return;
			}
			NKMUnitTempletBase unitTempletBase4 = NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID);
			if (unitTempletBase4 != null)
			{
				this.m_imgRightUserRole.enabled = true;
				this.m_imgRightUserRole.sprite = NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase4, true);
				this.m_imgRightUserAttackType.enabled = true;
				this.m_imgRightUserAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase4, true);
				return;
			}
			this.m_imgRightUserRole.enabled = false;
			this.m_imgRightUserAttackType.enabled = false;
		}

		// Token: 0x060091B7 RID: 37303 RVA: 0x0031B2E4 File Offset: 0x003194E4
		private NKCAssetResourceData GetUnitFaceDeckAssetResourceData(NKMUnitData unitData)
		{
			NKCAssetResourceData unitResource = NKCResourceUtility.GetUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitData);
			if (unitResource == null)
			{
				return NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
			}
			return unitResource;
		}

		// Token: 0x060091B8 RID: 37304 RVA: 0x0031B304 File Offset: 0x00319504
		private NKCAssetResourceData GetUnitFaceDeckAssetResourceData(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
			}
			return NKCResourceUtility.GetUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
		}

		// Token: 0x060091B9 RID: 37305 RVA: 0x0031B328 File Offset: 0x00319528
		public void StartGame(NKMGameRuntimeData cNKMGameRuntimeData)
		{
			this.SetAutoEnable();
			this.ChangeGameSpeedTypeUI(this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE);
			NKMGameRuntimeTeamData myRunTimeTeamData = this.m_GameClient.GetMyRunTimeTeamData();
			if (myRunTimeTeamData != null)
			{
				this.ChangeGameAutoSkillTypeUI(myRunTimeTeamData.m_NKM_GAME_AUTO_SKILL_TYPE);
			}
			this.RefreshClassGuide();
			this.SetTimeWarningFX(false);
			int multiply = (this.m_GameClient.GetGameData() != null && this.m_GameClient.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE) ? 1 : this.m_GameClient.MultiplyReward;
			this.SetMultiply(multiply);
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_AUTO_SKILL, 0, 0) && this.m_GameClient.GetMyRunTimeTeamData().m_NKM_GAME_AUTO_SKILL_TYPE != NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER)
			{
				this.m_GameClient.Send_Packet_GAME_AUTO_SKILL_CHANGE_REQ(NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER, false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_2X, 0, 0) && this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE != NKM_GAME_SPEED_TYPE.NGST_1)
			{
				this.m_GameClient.Send_Packet_GAME_SPEED_2X_REQ(NKM_GAME_SPEED_TYPE.NGST_1);
			}
			NKCUtil.SetGameobjectActive(this.m_btnPause, this.m_GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE);
			this.SetLeftMenu(cNKMGameRuntimeData);
			this.TogglePause(cNKMGameRuntimeData.m_bPause, this.m_GameClient.GetGameData().m_bLocal);
			if (this.m_NKCGameHUDArtifact != null)
			{
				this.m_NKCGameHUDArtifact.PlayEffectNoticeAni();
			}
			if (this.m_killCount != null)
			{
				if (NKCKillCountManager.IsKillCountDungeon(this.m_GameClient.GetGameData()))
				{
					NKCUtil.SetGameobjectActive(this.m_killCount, true);
					if (this.m_GameClient.GetIntrude())
					{
						this.m_killCount.SetKillCount("-");
						return;
					}
					if (NKCPhaseManager.IsPhaseOnGoing() && !NKCPhaseManager.IsFirstStage(this.m_GameClient.GetGameData().m_DungeonID))
					{
						this.m_killCount.SetKillCount(NKCKillCountManager.CurrentStageKillCount);
						return;
					}
					this.m_killCount.SetKillCount(0L);
					return;
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_killCount, false);
					this.m_killCount.SetKillCount("-");
				}
			}
		}

		// Token: 0x060091BA RID: 37306 RVA: 0x0031B508 File Offset: 0x00319708
		public void SetUnitCountMaxSameTime()
		{
			if (this.m_GameClient.GetDungeonTemplet() != null && this.m_GameClient.GetDungeonTemplet().m_DungeonTempletBase.m_RespawnCountMaxSameTime > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_lbUnitMaxCountSameTime, true);
				this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
				this.m_StringBuilder.AppendFormat(NKCUtilString.GET_STRING_INGAME_UNIT_COUNT_MAX_SAME_TIME, this.m_GameClient.GetLiveUnitCountTeamA(), this.m_GameClient.GetDungeonTemplet().m_DungeonTempletBase.m_RespawnCountMaxSameTime);
				this.m_lbUnitMaxCountSameTime.text = this.m_StringBuilder.ToString();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbUnitMaxCountSameTime, false);
		}

		// Token: 0x060091BB RID: 37307 RVA: 0x0031B5BF File Offset: 0x003197BF
		public void EndGame()
		{
		}

		// Token: 0x060091BC RID: 37308 RVA: 0x0031B5C4 File Offset: 0x003197C4
		public void SetLeftMenu(NKMGameRuntimeData cNKMGameRuntimeData)
		{
			NKMGameRuntimeTeamData myRuntimeTeamData = cNKMGameRuntimeData.GetMyRuntimeTeamData(NKCScenManager.GetScenManager().GetGameClient().m_MyTeam);
			if (myRuntimeTeamData != null)
			{
				this.ToggleAutoRespawn(myRuntimeTeamData.m_bAutoRespawn);
			}
		}

		// Token: 0x060091BD RID: 37309 RVA: 0x0031B5F8 File Offset: 0x003197F8
		public void SetDeck(NKMGameData cNKMGameData)
		{
			NKMGameTeamData teamData = cNKMGameData.GetTeamData(this.CurrentViewTeamType);
			if (teamData != null)
			{
				this.SetDeck(teamData);
				this.SetAssistDeck(teamData);
			}
		}

		// Token: 0x060091BE RID: 37310 RVA: 0x0031B624 File Offset: 0x00319824
		public void SetDeck(NKMGameTeamData cNKMGameTeamData)
		{
			for (int i = 0; i < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount(); i++)
			{
				this.SetDeck(i, cNKMGameTeamData);
			}
			this.SetNextDeck(cNKMGameTeamData);
			this.SetAutoRespawnIndex(cNKMGameTeamData.m_DeckData.GetAutoRespawnIndex());
		}

		// Token: 0x060091BF RID: 37311 RVA: 0x0031B668 File Offset: 0x00319868
		public void SetAssistDeck(NKMGameTeamData cNKMGameTeamData)
		{
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[5];
			if (cNKMGameTeamData.m_listAssistUnitData.Count <= 0 || cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist() < 0)
			{
				nkcgameHudDeckSlot.SetActive(false, false);
				return;
			}
			nkcgameHudDeckSlot.SetActive(true, false);
			NKMUnitData unitData = cNKMGameTeamData.m_listAssistUnitData[cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist()];
			nkcgameHudDeckSlot.SetDeckSprite(unitData, false, true, false, 0.1f * (float)(cNKMGameTeamData.m_DeckData.GetListUnitDeckCount() - 5));
			this.ReturnDeck(5);
			nkcgameHudDeckSlot.SetEnemy(false);
		}

		// Token: 0x060091C0 RID: 37312 RVA: 0x0031B6F0 File Offset: 0x003198F0
		public void SetDeck(int index, NKMGameTeamData cNKMGameTeamData)
		{
			long listUnitDeck = cNKMGameTeamData.m_DeckData.GetListUnitDeck(index);
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[index];
			nkcgameHudDeckSlot.SetActive(true, false);
			bool bAutoRespawn = false;
			if (this.IsAutoRespawnUsable() && this.m_AutoRespawnIndex == index && cNKMGameTeamData.GetUnitDataByUnitUID(listUnitDeck) != null)
			{
				bAutoRespawn = true;
			}
			nkcgameHudDeckSlot.SetDeckSprite(cNKMGameTeamData.GetUnitDataByUnitUID(listUnitDeck), cNKMGameTeamData.m_LeaderUnitUID == listUnitDeck, false, bAutoRespawn, 0.1f * (float)(cNKMGameTeamData.m_DeckData.GetListUnitDeckCount() - index));
			this.ReturnDeck(index);
			nkcgameHudDeckSlot.SetEnemy(false);
		}

		// Token: 0x060091C1 RID: 37313 RVA: 0x0031B771 File Offset: 0x00319971
		private void SetNextDeck(NKMGameTeamData cNKMGameTeamData)
		{
			this.m_NKCUIHudDeck[4].SetDeckSprite(cNKMGameTeamData.GetUnitDataByUnitUID(cNKMGameTeamData.m_DeckData.GetNextDeck()), cNKMGameTeamData.m_LeaderUnitUID == cNKMGameTeamData.m_DeckData.GetNextDeck(), false, false, 0f);
		}

		// Token: 0x060091C2 RID: 37314 RVA: 0x0031B7AC File Offset: 0x003199AC
		public void SetShipSkillDeck(NKMUnitData unitData)
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				this.m_NKCUIHudShipSkillDeck[i].SetDeckSprite(unitData, null);
			}
			NKMUnitTempletBase nkmunitTempletBase = null;
			if (unitData != null)
			{
				nkmunitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			}
			if (nkmunitTempletBase != null)
			{
				int num = 0;
				for (int j = 0; j < nkmunitTempletBase.GetSkillCount(); j++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(nkmunitTempletBase, j);
					if (num < this.m_NKCUIHudShipSkillDeck.Length && (shipSkillTempletByIndex == null || shipSkillTempletByIndex.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_PASSIVE))
					{
						this.m_NKCUIHudShipSkillDeck[num].SetDeckSprite(unitData, shipSkillTempletByIndex);
						num++;
					}
				}
			}
			for (int k = 0; k < this.m_NKCUIHudShipSkillDeck.Length; k++)
			{
				if (nkmunitTempletBase != null && nkmunitTempletBase.GetSkillCount() == 0)
				{
					this.m_NKCUIHudShipSkillDeck[k].SetActive(false, false);
				}
				else
				{
					this.ReturnShipSkillDeck(k);
				}
			}
		}

		// Token: 0x060091C3 RID: 37315 RVA: 0x0031B87C File Offset: 0x00319A7C
		public void SetTacticalCommandDeck(NKMGameTeamData cNKMGameTeamData)
		{
			for (int i = 0; i < this.m_NKCUIHudTacticalCommandDeck.Length; i++)
			{
				this.m_NKCUIHudTacticalCommandDeck[i].SetDeckSprite(null);
			}
			int num = 0;
			while (num < cNKMGameTeamData.m_listTacticalCommandData.Count && num < this.m_NKCUIHudTacticalCommandDeck.Length)
			{
				NKMTacticalCommandData nkmtacticalCommandData = cNKMGameTeamData.m_listTacticalCommandData[num];
				if (nkmtacticalCommandData != null)
				{
					NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)nkmtacticalCommandData.m_TCID);
					if (tacticalCommandTempletByID == null || tacticalCommandTempletByID.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_ACTIVE)
					{
						this.m_NKCUIHudTacticalCommandDeck[num].SetDeckSprite(tacticalCommandTempletByID);
					}
				}
				num++;
			}
		}

		// Token: 0x060091C4 RID: 37316 RVA: 0x0031B904 File Offset: 0x00319B04
		private void UpdateGameTimeUI()
		{
			int num = (int)this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime;
			if (this.m_RemainGameTimeInt == num)
			{
				return;
			}
			this.m_RemainGameTimeInt = num;
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			bool flag;
			if (this.m_GameClient.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE)
			{
				if (this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime > 10f)
				{
					flag = false;
					this.m_StringBuilder.AppendFormat("{0}:{1}", ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime / 60f)).ToString(), ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime % 60f)).ToString("D2"));
				}
				else if (this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime >= 0f)
				{
					flag = true;
					this.m_StringBuilder.AppendFormat("{0}:{1}", ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime / 60f)).ToString(), ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime % 60f)).ToString("D2"));
				}
				else
				{
					flag = true;
					this.m_StringBuilder.AppendFormat("-{0}:{1}", ((int)(-this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime / 60f)).ToString(), ((int)(-this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime % 60f)).ToString("D2"));
				}
			}
			else if (this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime > this.m_GameClient.GetGameData().m_fDoubleCostTime)
			{
				flag = false;
				this.m_StringBuilder.AppendFormat("{0}:{1}", ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime / 60f)).ToString(), ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime % 60f)).ToString("D2"));
			}
			else if (this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime >= 0f)
			{
				flag = true;
				this.m_StringBuilder.AppendFormat("{0}:{1}", ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime / 60f)).ToString(), ((int)(this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime % 60f)).ToString("D2"));
			}
			else
			{
				flag = true;
				this.m_StringBuilder.AppendFormat("-{0}:{1}", ((int)(-this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime / 60f)).ToString(), ((int)(-this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime % 60f)).ToString("D2"));
			}
			if (this.m_GameClient.GetGameRuntimeData().m_fRemainGameTime <= 10f && !this.m_bCountDownVoicePlayed)
			{
				this.m_bCountDownVoicePlayed = true;
				if (this.m_GameClient.GetMyTeamData() != null && this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
				{
					NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_COUNT_DOWN, this.m_GameClient.GetMyTeamData().m_Operator, false);
				}
			}
			this.m_lbTimeLeft.text = this.m_StringBuilder.ToString();
			if (!flag)
			{
				this.m_lbTimeLeft.color = Color.white;
				return;
			}
			this.m_lbTimeLeft.color = Color.red;
		}

		// Token: 0x060091C5 RID: 37317 RVA: 0x0031BC7D File Offset: 0x00319E7D
		private void UpdateComboUI()
		{
			if (this.m_NKCGameHudCombo == null)
			{
				return;
			}
			this.m_NKCGameHudCombo.UpdatePerFrame(this.GetCurrentViewTeamData().GetTC_Combo());
		}

		// Token: 0x060091C6 RID: 37318 RVA: 0x0031BCA4 File Offset: 0x00319EA4
		private void UpdateAttackPointUI(float fDeltaTime)
		{
			if (this.m_GameClient.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE || this.m_GameClient.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_RAID || this.m_GameClient.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
			{
				if (this.m_fElapsedTimeForAP >= 1f)
				{
					this.m_fElapsedTimeForAP = 0f;
					this.m_fAttackPointLeftCurrValue = (float)this.m_AttackPointLeftTargetValue;
					this.m_lbAttackPointNow.text = ((int)this.m_fAttackPointLeftCurrValue).ToString("N0");
				}
				if ((int)this.m_fAttackPointLeftCurrValue != this.m_AttackPointLeftTargetValue)
				{
					this.m_fAttackPointLeftCurrValue = Mathf.Lerp(this.m_fAttackPointLeftPivotValue, (float)this.m_AttackPointLeftTargetValue, this.m_fElapsedTimeForAP);
					this.m_fElapsedTimeForAP += fDeltaTime * 2f;
					this.m_lbAttackPointNow.text = ((int)this.m_fAttackPointLeftCurrValue).ToString("N0");
				}
			}
		}

		// Token: 0x060091C7 RID: 37319 RVA: 0x0031BD84 File Offset: 0x00319F84
		private void UpdateMainGage(float fDeltaTime)
		{
			if (this.m_NKCUIMainHPGageAlly.IsVisibleMainGage())
			{
				this.m_NKCUIMainHPGageAlly.UpdateGage(fDeltaTime);
			}
			if (this.m_NKCUIMainHPGageEnemy.IsVisibleMainGage())
			{
				this.m_NKCUIMainHPGageEnemy.UpdateGage(fDeltaTime);
			}
			if (this.m_NKCUIMainHPGageAllyLong.IsVisibleMainGage())
			{
				this.m_NKCUIMainHPGageAllyLong.UpdateGage(fDeltaTime);
			}
			if (this.m_NKCUIMainHPGageEnemyLong.IsVisibleMainGage())
			{
				this.m_NKCUIMainHPGageEnemyLong.UpdateGage(fDeltaTime);
			}
		}

		// Token: 0x060091C8 RID: 37320 RVA: 0x0031BDF5 File Offset: 0x00319FF5
		protected NKMGameTeamData GetCurrentViewTeamData()
		{
			return this.m_GameClient.GetGameData().GetTeamData(this.CurrentViewTeamType);
		}

		// Token: 0x060091C9 RID: 37321 RVA: 0x0031BE10 File Offset: 0x0031A010
		private void UpdateDeck(float fDeltaTime)
		{
			NKMGameTeamData currentViewTeamData = this.GetCurrentViewTeamData();
			if (currentViewTeamData != null)
			{
				NKMTacticalCombo cNKMTacticalComboGoal = null;
				NKMTacticalCommandData tc_Combo = currentViewTeamData.GetTC_Combo();
				if (tc_Combo != null && tc_Combo.m_bCoolTimeOn)
				{
					cNKMTacticalComboGoal = tc_Combo.GetNKMTacticalComboGoal();
				}
				for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
				{
					NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
					if (nkcgameHudDeckSlot != null)
					{
						nkcgameHudDeckSlot.UpdateDeck(fDeltaTime);
						if (nkcgameHudDeckSlot.m_UnitData != null)
						{
							NKCUnitClient nkcunitClient = null;
							if (nkcgameHudDeckSlot.m_UnitData.m_listGameUnitUID.Count > 0)
							{
								short gameUnitUID = nkcgameHudDeckSlot.m_UnitData.m_listGameUnitUID[0];
								nkcunitClient = (NKCUnitClient)this.m_GameClient.GetUnit(gameUnitUID, true, true);
							}
							if (nkcunitClient != null)
							{
								float fSkillCoolNow = 0f;
								float fSkillCoolMax = 0f;
								float fHyperSkillCoolNow = 0f;
								float fHyperSkillMax = 0f;
								NKMAttackStateData nkmattackStateData = nkcunitClient.GetFastestCoolTimeSkillData();
								if (nkmattackStateData != null)
								{
									fSkillCoolNow = nkcunitClient.GetStateCoolTime(nkmattackStateData.m_StateName);
									fSkillCoolMax = nkcunitClient.GetStateMaxCoolTime(nkmattackStateData.m_StateName);
								}
								nkmattackStateData = nkcunitClient.GetFastestCoolTimeHyperSkillData();
								if (nkmattackStateData != null)
								{
									fHyperSkillCoolNow = nkcunitClient.GetStateCoolTime(nkmattackStateData.m_StateName);
									fHyperSkillMax = nkcunitClient.GetStateMaxCoolTime(nkmattackStateData.m_StateName);
								}
								float fRespawnCostNow = this.m_NKCUIHudRespawnGage.GetRespawnCostGage();
								if (i == 5)
								{
									fRespawnCostNow = this.m_NKCUIHudRespawnGage.GetRespawnCostGageAssist();
								}
								nkcgameHudDeckSlot.SetDeckData(fRespawnCostNow, this.m_GameClient.IsGameUnitAllDie(nkcgameHudDeckSlot.m_UnitData.m_UnitUID), currentViewTeamData.m_LeaderUnitUID == nkcgameHudDeckSlot.m_UnitData.m_UnitUID, fSkillCoolNow, fSkillCoolMax, fHyperSkillCoolNow, fHyperSkillMax, cNKMTacticalComboGoal);
							}
						}
					}
				}
			}
			if (this.m_NKCUIHudRespawnGage.GetRespawnCostGage() >= 10f)
			{
				this.m_bCostFull = true;
			}
			else
			{
				this.m_bCostFull = false;
				this.m_fMaxCostTime = 0f;
			}
			if (this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY && this.m_bCostFull)
			{
				this.m_fMaxCostTime += Time.deltaTime;
				if (!this.m_bCostFullVoicePlayed)
				{
					if (this.m_fMaxCostTime >= 1f)
					{
						this.m_bCostFullVoicePlayed = true;
						NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_COST_FULL, currentViewTeamData.m_Operator, false);
						this.m_fMaxCostTime -= 1f;
					}
				}
				else if (this.m_fMaxCostTime >= 13f)
				{
					NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_COST_FULL, currentViewTeamData.m_Operator, false);
					this.m_fMaxCostTime -= 13f;
				}
			}
			bool flag = false;
			for (int j = 0; j < this.m_NKCUIHudShipSkillDeck.Length; j++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[j];
				if (nkcuihudShipSkillDeck != null && nkcuihudShipSkillDeck.m_NKMShipSkillTemplet != null)
				{
					NKMUnit unit = this.m_GameClient.GetUnit(nkcuihudShipSkillDeck.m_GameUnitUID, true, false);
					if (unit != null)
					{
						if (nkcuihudShipSkillDeck.m_NKMShipSkillTemplet.m_UnitStateName.Length > 1)
						{
							float stateCoolTime = unit.GetStateCoolTime(nkcuihudShipSkillDeck.m_NKMShipSkillTemplet.m_UnitStateName);
							nkcuihudShipSkillDeck.UpdateShipSkillDeck(fDeltaTime);
							nkcuihudShipSkillDeck.SetDeckData(stateCoolTime);
							if (stateCoolTime == 0f)
							{
								flag = true;
							}
						}
						else
						{
							nkcuihudShipSkillDeck.UpdateShipSkillDeck(fDeltaTime);
							nkcuihudShipSkillDeck.SetDeckData(0f);
						}
					}
				}
			}
			if (this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
			{
				if (flag)
				{
					if (!this.m_bShipSkillReady)
					{
						this.m_bShipSkillReady = true;
						this.m_fShipSkillFullTime = 0f;
					}
				}
				else
				{
					this.m_bShipSkillReady = false;
				}
				if (this.m_bShipSkillReady)
				{
					if (!this.m_bShipSkillReadyVoicePlayed)
					{
						this.m_bShipSkillReadyVoicePlayed = true;
						NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_SHIP_SKILL, currentViewTeamData.m_Operator, false);
					}
					else
					{
						this.m_fShipSkillFullTime += Time.deltaTime;
						if (this.m_fShipSkillFullTime >= 17f)
						{
							NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_SHIP_SKILL, currentViewTeamData.m_Operator, false);
							this.m_fShipSkillFullTime -= 17f;
						}
					}
				}
				else
				{
					this.m_bShipSkillReadyVoicePlayed = false;
				}
			}
			for (int k = 0; k < this.m_NKCUIHudTacticalCommandDeck.Length; k++)
			{
				NKCUIHudTacticalCommandDeck nkcuihudTacticalCommandDeck = this.m_NKCUIHudTacticalCommandDeck[k];
				if (nkcuihudTacticalCommandDeck != null && nkcuihudTacticalCommandDeck.m_NKMTacticalCommandTemplet != null)
				{
					for (int l = 0; l < this.m_GameClient.GetMyTeamData().m_listTacticalCommandData.Count; l++)
					{
						if (nkcuihudTacticalCommandDeck.m_NKMTacticalCommandTemplet.m_TCID == this.m_GameClient.GetMyTeamData().m_listTacticalCommandData[l].m_TCID)
						{
							nkcuihudTacticalCommandDeck.SetDeckData(this.m_NKCUIHudRespawnGage.GetRespawnCostGage(), this.m_GameClient.GetMyTeamData().m_listTacticalCommandData[l]);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060091CA RID: 37322 RVA: 0x0031C284 File Offset: 0x0031A484
		public void UpdateHud(float fDeltaTime)
		{
			this.UpdateGameTimeUI();
			this.UpdateAttackPointUI(fDeltaTime);
			this.UpdateComboUI();
			this.m_NKCUIHudRespawnGage.UpdateGage(fDeltaTime);
			this.UpdateMainGage(fDeltaTime);
			this.ProcessHotkey();
			this.UpdateDeck(fDeltaTime);
			NKCUnitClient nkcunitClient = (NKCUnitClient)this.m_GameClient.GetLiveBossUnit(this.m_GameClient.m_MyTeam);
			if (nkcunitClient != null)
			{
				if (this.m_NKCUIMainHPGageAlly.IsVisibleMainGage())
				{
					this.m_NKCUIMainHPGageAlly.GetMainGageBuff().SetUnit(nkcunitClient);
				}
				if (this.m_NKCUIMainHPGageAllyLong.IsVisibleMainGage())
				{
					this.m_NKCUIMainHPGageAllyLong.GetMainGageBuff().SetUnit(nkcunitClient);
				}
			}
			nkcunitClient = (NKCUnitClient)this.m_GameClient.GetLiveBossUnit(this.m_GameClient.GetGameData().GetEnemyTeamType(this.m_GameClient.m_MyTeam));
			if (nkcunitClient != null)
			{
				if (this.m_NKCUIMainHPGageEnemy.IsVisibleMainGage())
				{
					this.m_NKCUIMainHPGageEnemy.GetMainGageBuff().SetUnit(nkcunitClient);
				}
				if (this.m_NKCUIMainHPGageEnemyLong.IsVisibleMainGage())
				{
					this.m_NKCUIMainHPGageEnemyLong.GetMainGageBuff().SetUnit(nkcunitClient);
				}
				this.m_NKCUIMainSkillCoolRight.SetCooltime(nkcunitClient.GetSkillCoolRate(), nkcunitClient.GetHyperSkillCoolRate());
			}
			NKMMapTemplet mapTemplet = this.m_GameClient.GetMapTemplet();
			Vector2 vector = this.m_rtMinimapCamPanel.sizeDelta;
			if (mapTemplet != null)
			{
				float num = NKCCamera.GetCameraSizeNow() * 2f * NKCCamera.GetCameraAspect();
				float newX = this.m_NUF_GAME_HUD_MINI_MAP_RectTransform_width * (num / (mapTemplet.m_fMaxX - mapTemplet.m_fMinX));
				vector.Set(newX, vector.y);
			}
			this.m_rtMinimapCamPanel.sizeDelta = vector;
			vector = this.m_rtMinimapCamPanel.anchoredPosition;
			if (mapTemplet != null)
			{
				float newX2 = (NKCCamera.GetPosNowX(true) - mapTemplet.m_fMinX) / (mapTemplet.m_fMaxX - mapTemplet.m_fMinX) * this.m_NUF_GAME_HUD_MINI_MAP_RectTransform_width;
				vector.Set(newX2, vector.y);
			}
			this.m_rtMinimapCamPanel.anchoredPosition = vector;
			this.SetUnitCountMaxSameTime();
			this.UpdateHudAlert();
		}

		// Token: 0x060091CB RID: 37323 RVA: 0x0031C45B File Offset: 0x0031A65B
		public float GetMiniMapRectWidth()
		{
			return this.m_NUF_GAME_HUD_MINI_MAP_RectTransform_width;
		}

		// Token: 0x060091CC RID: 37324 RVA: 0x0031C464 File Offset: 0x0031A664
		public void SetRespawnCost()
		{
			float respawnCostClient = this.m_GameClient.GetRespawnCostClient(this.CurrentViewTeamType);
			this.m_NKCUIHudRespawnGage.SetRespawnCost(respawnCostClient);
		}

		// Token: 0x060091CD RID: 37325 RVA: 0x0031C490 File Offset: 0x0031A690
		public void SetRespawnCostAssist()
		{
			float respawnCostAssistClient = this.m_GameClient.GetRespawnCostAssistClient(this.CurrentViewTeamType);
			this.m_NKCUIHudRespawnGage.SetRespawnCostAssist(respawnCostAssistClient);
		}

		// Token: 0x060091CE RID: 37326 RVA: 0x0031C4BB File Offset: 0x0031A6BB
		public void PlayRespawnAddEvent(float value)
		{
			this.m_NKCUIHudRespawnGage.PlayRespawnAddEvent(value);
		}

		// Token: 0x060091CF RID: 37327 RVA: 0x0031C4CC File Offset: 0x0031A6CC
		public int GetDeckIndex(GameObject go)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null && nkcgameHudDeckSlot.gameObject == go)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060091D0 RID: 37328 RVA: 0x0031C510 File Offset: 0x0031A710
		public int GetShipSkillDeckIndex(GameObject go)
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[i];
				if (nkcuihudShipSkillDeck != null && nkcuihudShipSkillDeck.gameObject == go)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060091D1 RID: 37329 RVA: 0x0031C554 File Offset: 0x0031A754
		public long GetDeckUnitUID(int index)
		{
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[index];
			if (nkcgameHudDeckSlot.m_UnitData != null)
			{
				return nkcgameHudDeckSlot.m_UnitData.m_UnitUID;
			}
			return 0L;
		}

		// Token: 0x060091D2 RID: 37330 RVA: 0x0031C580 File Offset: 0x0031A780
		public void MoveDeck(int index, float fPosX, float fPosY)
		{
			if (index < 0 || index >= this.m_NKCUIHudDeck.Length)
			{
				return;
			}
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[index];
			if (nkcgameHudDeckSlot != null)
			{
				nkcgameHudDeckSlot.MoveDeck(fPosX, fPosY);
			}
		}

		// Token: 0x060091D3 RID: 37331 RVA: 0x0031C5B8 File Offset: 0x0031A7B8
		public void MoveShipSkillDeck(int index, float fPosX, float fPosY)
		{
			if (index < 0 || index >= this.m_NKCUIHudShipSkillDeck.Length)
			{
				return;
			}
			NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[index];
			if (nkcuihudShipSkillDeck != null)
			{
				nkcuihudShipSkillDeck.MoveShipSkillDeck(fPosX, fPosY);
			}
		}

		// Token: 0x060091D4 RID: 37332 RVA: 0x0031C5F0 File Offset: 0x0031A7F0
		public void ReturnDeck(int index)
		{
			if (index < 0 || index >= this.m_NKCUIHudDeck.Length)
			{
				return;
			}
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[index];
			if (nkcgameHudDeckSlot != null)
			{
				nkcgameHudDeckSlot.ReturnDeck(true);
			}
		}

		// Token: 0x060091D5 RID: 37333 RVA: 0x0031C628 File Offset: 0x0031A828
		public void ReturnShipSkillDeck(int index)
		{
			if (index < 0 || index >= this.m_NKCUIHudShipSkillDeck.Length)
			{
				return;
			}
			NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[index];
			if (nkcuihudShipSkillDeck != null)
			{
				nkcuihudShipSkillDeck.ReturnShipSkillDeck();
			}
		}

		// Token: 0x060091D6 RID: 37334 RVA: 0x0031C660 File Offset: 0x0031A860
		public void ReturnTacticalCommandDeck(int index)
		{
			if (index < 0 || index >= this.m_NKCUIHudTacticalCommandDeck.Length)
			{
				return;
			}
			NKCUIHudTacticalCommandDeck nkcuihudTacticalCommandDeck = this.m_NKCUIHudTacticalCommandDeck[index];
			if (nkcuihudTacticalCommandDeck != null)
			{
				nkcuihudTacticalCommandDeck.ReturnTacticalCommandDeck();
			}
		}

		// Token: 0x060091D7 RID: 37335 RVA: 0x0031C690 File Offset: 0x0031A890
		public void UseCompleteDeckByUnitUID(long unitUID, bool bReturnDeckActive = true)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null && nkcgameHudDeckSlot.m_UnitData != null && nkcgameHudDeckSlot.m_UnitData.m_UnitUID == unitUID)
				{
					nkcgameHudDeckSlot.UseCompleteDeck(bReturnDeckActive);
				}
			}
		}

		// Token: 0x060091D8 RID: 37336 RVA: 0x0031C6E0 File Offset: 0x0031A8E0
		public void UseCompleteDeckAssist(bool bReturnDeckActive = true)
		{
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[5];
			if (nkcgameHudDeckSlot != null && nkcgameHudDeckSlot.m_UnitData != null)
			{
				nkcgameHudDeckSlot.UseCompleteDeck(bReturnDeckActive);
			}
		}

		// Token: 0x060091D9 RID: 37337 RVA: 0x0031C710 File Offset: 0x0031A910
		public void UseCompleteDeck()
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null && nkcgameHudDeckSlot.m_UnitData != null)
				{
					nkcgameHudDeckSlot.UseCompleteDeck(true);
				}
			}
		}

		// Token: 0x060091DA RID: 37338 RVA: 0x0031C754 File Offset: 0x0031A954
		public void ReturnDeckByShipSkillID(int shipSkill)
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[i];
				if (nkcuihudShipSkillDeck != null && nkcuihudShipSkillDeck.m_NKMShipSkillTemplet != null && nkcuihudShipSkillDeck.m_NKMShipSkillTemplet.m_ShipSkillID == shipSkill)
				{
					this.ReturnShipSkillDeck(i);
				}
			}
		}

		// Token: 0x060091DB RID: 37339 RVA: 0x0031C7A4 File Offset: 0x0031A9A4
		public void ReturnDeckByTacticalCommandID(int TCID)
		{
			for (int i = 0; i < this.m_NKCUIHudTacticalCommandDeck.Length; i++)
			{
				NKCUIHudTacticalCommandDeck nkcuihudTacticalCommandDeck = this.m_NKCUIHudTacticalCommandDeck[i];
				if (nkcuihudTacticalCommandDeck != null && nkcuihudTacticalCommandDeck.m_NKMTacticalCommandTemplet != null && (int)nkcuihudTacticalCommandDeck.m_NKMTacticalCommandTemplet.m_TCID == TCID)
				{
					this.ReturnTacticalCommandDeck(i);
				}
			}
		}

		// Token: 0x060091DC RID: 37340 RVA: 0x0031C7F0 File Offset: 0x0031A9F0
		public void SetDeck(NKMGameTeamData cNKMGameTeamData, int unitDeckIndex, long unitDeckUID)
		{
			if (this.m_SelectUnitDeckIndex == unitDeckIndex)
			{
				this.UnSelectUnitDeck();
			}
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[unitDeckIndex];
			NKCUnitClient nkcunitClient = null;
			if (nkcgameHudDeckSlot.m_UnitData != null)
			{
				if (nkcgameHudDeckSlot.m_UnitData.m_UnitUID != unitDeckUID)
				{
					nkcgameHudDeckSlot.RespawnReady = false;
					nkcgameHudDeckSlot.RetreatReady = false;
				}
				nkcgameHudDeckSlot.SetActive(true, false);
				bool bAutoRespawn = false;
				if (this.IsAutoRespawnUsable() && this.m_AutoRespawnIndex == unitDeckIndex && cNKMGameTeamData.GetUnitDataByUnitUID(unitDeckUID) != null)
				{
					bAutoRespawn = true;
				}
				nkcgameHudDeckSlot.SetDeckSprite(cNKMGameTeamData.GetUnitDataByUnitUID(unitDeckUID), cNKMGameTeamData.m_LeaderUnitUID == unitDeckUID, false, bAutoRespawn, 0.3f);
			}
			if (nkcgameHudDeckSlot.m_UnitData != null && nkcgameHudDeckSlot.m_UnitData.m_listGameUnitUID.Count > 0)
			{
				short gameUnitUID = nkcgameHudDeckSlot.m_UnitData.m_listGameUnitUID[0];
				nkcunitClient = (NKCUnitClient)this.m_GameClient.GetUnit(gameUnitUID, true, true);
			}
			if (nkcunitClient != null)
			{
				float fSkillCoolNow = 0f;
				float fSkillCoolMax = 0f;
				float fHyperSkillCoolNow = 0f;
				float fHyperSkillMax = 0f;
				NKMAttackStateData nkmattackStateData = nkcunitClient.GetFastestCoolTimeSkillData();
				if (nkmattackStateData != null)
				{
					fSkillCoolNow = nkcunitClient.GetStateCoolTime(nkmattackStateData.m_StateName);
					fSkillCoolMax = nkcunitClient.GetStateMaxCoolTime(nkmattackStateData.m_StateName);
				}
				nkmattackStateData = nkcunitClient.GetFastestCoolTimeHyperSkillData();
				if (nkmattackStateData != null)
				{
					fHyperSkillCoolNow = nkcunitClient.GetStateCoolTime(nkmattackStateData.m_StateName);
					fHyperSkillMax = nkcunitClient.GetStateMaxCoolTime(nkmattackStateData.m_StateName);
				}
				NKMTacticalCombo cNKMTacticalComboGoal = null;
				NKMTacticalCommandData tc_Combo = cNKMGameTeamData.GetTC_Combo();
				if (tc_Combo != null && tc_Combo.m_bCoolTimeOn)
				{
					cNKMTacticalComboGoal = tc_Combo.GetNKMTacticalComboGoal();
				}
				nkcgameHudDeckSlot.SetDeckData(this.m_NKCUIHudRespawnGage.GetRespawnCostGage(), this.m_GameClient.IsGameUnitAllDie(unitDeckUID), cNKMGameTeamData.m_LeaderUnitUID == unitDeckUID, fSkillCoolNow, fSkillCoolMax, fHyperSkillCoolNow, fHyperSkillMax, cNKMTacticalComboGoal);
			}
			this.SetNextDeck(cNKMGameTeamData);
		}

		// Token: 0x060091DD RID: 37341 RVA: 0x0031C98D File Offset: 0x0031AB8D
		public void UseDeck(int unitDeckIndex, bool bRetreat)
		{
			this.UnSelectShipSkillDeck();
			if (this.m_SelectUnitDeckIndex != unitDeckIndex)
			{
				this.UnSelectUnitDeck();
			}
			this.m_NKCUIHudDeck[unitDeckIndex].UseDeck(bRetreat);
			NKCGameHud.OnUseDeck onUseDeck = this.dOnUseDeck;
			if (onUseDeck == null)
			{
				return;
			}
			onUseDeck(unitDeckIndex);
		}

		// Token: 0x060091DE RID: 37342 RVA: 0x0031C9C3 File Offset: 0x0031ABC3
		public void UseShipSkillDeck(int shipSkillDeckIndex)
		{
			this.UnSelectUnitDeck();
			if (this.m_SelectShipSkillDeckIndex != shipSkillDeckIndex)
			{
				this.UnSelectShipSkillDeck();
			}
			this.m_NKCUIHudShipSkillDeck[shipSkillDeckIndex].SetActive(false, false);
			NKCGameHud.OnUseSkill onUseSkill = this.dOnUseSkill;
			if (onUseSkill == null)
			{
				return;
			}
			onUseSkill(shipSkillDeckIndex);
		}

		// Token: 0x060091DF RID: 37343 RVA: 0x0031C9FA File Offset: 0x0031ABFA
		public NKCUIHudShipSkillDeck GetShipSkillDeck(int index)
		{
			return this.m_NKCUIHudShipSkillDeck[index];
		}

		// Token: 0x060091E0 RID: 37344 RVA: 0x0031CA04 File Offset: 0x0031AC04
		public void SetSyncDeck(NKMGameSyncData_Deck deckSyncDeckData)
		{
			if (this.CurrentViewTeamType != deckSyncDeckData.m_NKM_TEAM_TYPE)
			{
				return;
			}
			NKMGameTeamData currentViewTeamData = this.GetCurrentViewTeamData();
			this.SetAutoRespawnIndex(currentViewTeamData.m_DeckData.GetAutoRespawnIndex());
			this.SetDeck(currentViewTeamData, (int)deckSyncDeckData.m_UnitDeckIndex, deckSyncDeckData.m_UnitDeckUID);
		}

		// Token: 0x060091E1 RID: 37345 RVA: 0x0031CA4C File Offset: 0x0031AC4C
		public void SetAutoRespawnIndex(int index)
		{
			this.m_AutoRespawnIndex = index;
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null)
				{
					if (this.IsAutoRespawnUsable() && this.m_AutoRespawnIndex == i && nkcgameHudDeckSlot.m_UnitData != null)
					{
						nkcgameHudDeckSlot.SetAuto(true);
					}
					else
					{
						nkcgameHudDeckSlot.SetAuto(false);
					}
				}
			}
		}

		// Token: 0x060091E2 RID: 37346 RVA: 0x0031CAAE File Offset: 0x0031ACAE
		public void SetMainGage(bool bGageA, float fHP, bool bLong = false)
		{
			if (bLong)
			{
				if (bGageA)
				{
					this.m_NKCUIMainHPGageAllyLong.SetMainGage(fHP);
					return;
				}
				this.m_NKCUIMainHPGageEnemyLong.SetMainGage(fHP);
				return;
			}
			else
			{
				if (bGageA)
				{
					this.m_NKCUIMainHPGageAlly.SetMainGage(fHP);
					return;
				}
				this.m_NKCUIMainHPGageEnemy.SetMainGage(fHP);
				return;
			}
		}

		// Token: 0x060091E3 RID: 37347 RVA: 0x0031CAEC File Offset: 0x0031ACEC
		public void SetRageMode(bool bRageMode, bool isMyTeam)
		{
			if (isMyTeam)
			{
				if (this.m_NKCUIMainHPGageAlly != null)
				{
					this.m_NKCUIMainHPGageAlly.SetRageMode(bRageMode);
				}
				NKCUtil.SetGameobjectActive(this.m_objLeftUserRage, bRageMode);
				return;
			}
			if (this.m_NKCUIMainHPGageEnemy != null)
			{
				this.m_NKCUIMainHPGageEnemy.SetRageMode(bRageMode);
			}
			NKCUtil.SetGameobjectActive(this.m_objRightUserRage, bRageMode);
		}

		// Token: 0x060091E4 RID: 37348 RVA: 0x0031CB49 File Offset: 0x0031AD49
		public void SetDeadlineMode(int buffLevel, string updateBuffDesc)
		{
			this.m_DeadlineBuffLevel = buffLevel;
			NKCUtil.SetGameobjectActive(this.m_csbtnDeadlineBuff, true);
			NKCUtil.SetLabelText(this.m_lbDeadLineBuffLevel, buffLevel.ToString());
			this.m_strDeadLineBuffString = updateBuffDesc;
		}

		// Token: 0x060091E5 RID: 37349 RVA: 0x0031CB78 File Offset: 0x0031AD78
		public void SetMessage(string str, float time = -1f)
		{
			NKCUtil.SetGameobjectActive(this.m_objHUDMessage, true);
			this.m_lbHUDMessage.text = str;
			this.m_animatorHUDMessage.Play("BASE", -1, 0f);
			if (time <= 0f)
			{
				this.m_animatorHUDMessage.speed = 1f;
				return;
			}
			AnimationClip animationClip = this.m_animatorHUDMessage.runtimeAnimatorController.animationClips[0];
			if (animationClip != null)
			{
				this.m_animatorHUDMessage.speed = animationClip.length / time;
				return;
			}
			this.m_animatorHUDMessage.speed = 1f;
		}

		// Token: 0x060091E6 RID: 37350 RVA: 0x0031CC0C File Offset: 0x0031AE0C
		public void SetTimeOver(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objTimeOver, bSet);
		}

		// Token: 0x060091E7 RID: 37351 RVA: 0x0031CC1C File Offset: 0x0031AE1C
		public void ToggleAutoRespawn(bool bOn)
		{
			if (this.m_animatorAutoRespawn == null)
			{
				return;
			}
			if (bOn)
			{
				this.m_animatorAutoRespawn.Play("ON_START", -1, 0f);
				return;
			}
			this.m_animatorAutoRespawn.Play("OFF_START", -1, 0f);
		}

		// Token: 0x060091E8 RID: 37352 RVA: 0x0031CC68 File Offset: 0x0031AE68
		public void TouchDownDeck(int index)
		{
			NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[index];
			if (nkcgameHudDeckSlot != null)
			{
				nkcgameHudDeckSlot.TouchDown();
			}
			this.UnSelectShipSkillDeck();
			if (this.m_SelectUnitDeckIndex != index)
			{
				this.UnSelectUnitDeck();
			}
		}

		// Token: 0x060091E9 RID: 37353 RVA: 0x0031CCA4 File Offset: 0x0031AEA4
		public void TouchUpDeck(int index, bool bUseTouchScale)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null && nkcgameHudDeckSlot.m_UnitData != null)
				{
					if (i == index)
					{
						if (this.m_SelectUnitDeckIndex == index)
						{
							this.UnSelectUnitDeck();
						}
						else if (this.m_GameClient.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
						{
							this.m_SelectUnitDeckIndex = index;
							nkcgameHudDeckSlot.TouchSelectUnitDeck(bUseTouchScale);
						}
					}
					else
					{
						if (i == 5 && this.m_GameClient.GetMyTeamData().m_DeckData.GetAutoRespawnIndexAssist() == -1)
						{
							nkcgameHudDeckSlot.ReturnDeck(false);
						}
						else
						{
							nkcgameHudDeckSlot.ReturnDeck(true);
						}
						nkcgameHudDeckSlot.TouchUnSelectUnitDeck();
					}
				}
			}
		}

		// Token: 0x060091EA RID: 37354 RVA: 0x0031CD4C File Offset: 0x0031AF4C
		public void TouchDownShipSkillDeck(int index)
		{
			NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[index];
			if (nkcuihudShipSkillDeck != null)
			{
				nkcuihudShipSkillDeck.TouchDown();
			}
			this.UnSelectUnitDeck();
			if (this.m_SelectShipSkillDeckIndex != index)
			{
				this.UnSelectShipSkillDeck();
			}
		}

		// Token: 0x060091EB RID: 37355 RVA: 0x0031CD88 File Offset: 0x0031AF88
		public void TouchUpShipSkillDeck(int index)
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[i];
				if (nkcuihudShipSkillDeck != null)
				{
					if (i == index)
					{
						nkcuihudShipSkillDeck.TouchUp();
						if (this.m_SelectShipSkillDeckIndex == index)
						{
							this.UnSelectShipSkillDeck();
						}
						else if (nkcuihudShipSkillDeck.m_NKMShipSkillTemplet != null && nkcuihudShipSkillDeck.m_NKMShipSkillTemplet.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_PASSIVE && this.m_GameClient.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
						{
							this.m_SelectShipSkillDeckIndex = index;
							nkcuihudShipSkillDeck.TouchSelectShipSkillDeck();
						}
					}
					else
					{
						nkcuihudShipSkillDeck.TouchUnSelectShipSkillDeck();
					}
				}
			}
		}

		// Token: 0x060091EC RID: 37356 RVA: 0x0031CE10 File Offset: 0x0031B010
		public void UnSelectUnitDeck()
		{
			if (this.m_SelectUnitDeckIndex >= 0 && this.m_SelectUnitDeckIndex < this.m_NKCUIHudDeck.Length)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[this.m_SelectUnitDeckIndex];
				if (nkcgameHudDeckSlot != null)
				{
					nkcgameHudDeckSlot.TouchUnSelectUnitDeck();
				}
				this.m_SelectUnitDeckIndex = -1;
			}
		}

		// Token: 0x060091ED RID: 37357 RVA: 0x0031CE5C File Offset: 0x0031B05C
		public void UnSelectShipSkillDeck()
		{
			if (this.m_SelectShipSkillDeckIndex >= 0 && this.m_SelectShipSkillDeckIndex < this.m_NKCUIHudDeck.Length)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[this.m_SelectShipSkillDeckIndex];
				if (nkcuihudShipSkillDeck != null)
				{
					nkcuihudShipSkillDeck.TouchUnSelectShipSkillDeck();
				}
				this.m_SelectShipSkillDeckIndex = -1;
			}
		}

		// Token: 0x060091EE RID: 37358 RVA: 0x0031CEA8 File Offset: 0x0031B0A8
		public void UnSelectUnitDeckAll()
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot != null)
				{
					nkcgameHudDeckSlot.TouchUnSelectUnitDeck();
				}
			}
			this.m_SelectUnitDeckIndex = -1;
		}

		// Token: 0x060091EF RID: 37359 RVA: 0x0031CEE8 File Offset: 0x0031B0E8
		public void UnSelectShipSkillDeckAll()
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[i];
				if (nkcuihudShipSkillDeck != null)
				{
					nkcuihudShipSkillDeck.TouchUnSelectShipSkillDeck();
				}
			}
			this.m_SelectShipSkillDeckIndex = -1;
		}

		// Token: 0x060091F0 RID: 37360 RVA: 0x0031CF28 File Offset: 0x0031B128
		public void ShowTooltip(int index, Vector2 touchPos)
		{
			NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[index];
			if (nkcuihudShipSkillDeck != null)
			{
				NKCUITooltip.Instance.Open(nkcuihudShipSkillDeck.m_NKMShipSkillTemplet, new Vector2?(touchPos));
			}
		}

		// Token: 0x060091F1 RID: 37361 RVA: 0x0031CF5D File Offset: 0x0031B15D
		public void CloseTooltip()
		{
			if (NKCUITooltip.IsInstanceOpen)
			{
				NKCUITooltip.Instance.Close();
			}
		}

		// Token: 0x060091F2 RID: 37362 RVA: 0x0031CF70 File Offset: 0x0031B170
		public void PracticeGoBack()
		{
			if (this.m_GameHudPractice != null)
			{
				this.m_GameHudPractice.PracticeGoBack();
			}
		}

		// Token: 0x060091F3 RID: 37363 RVA: 0x0031CF8C File Offset: 0x0031B18C
		public virtual void SendGameSpeed2X()
		{
			if (this.m_eMode != NKCGameHud.HUDMode.Replay && NKMGame.IsPVPSync(this.m_GameClient.GetGameData().GetGameType()))
			{
				return;
			}
			NKM_GAME_SPEED_TYPE nkm_GAME_SPEED_TYPE = this.m_NKM_GAME_SPEED_TYPE;
			switch (this.m_NKM_GAME_SPEED_TYPE)
			{
			case NKM_GAME_SPEED_TYPE.NGST_1:
				nkm_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_2;
				break;
			case NKM_GAME_SPEED_TYPE.NGST_2:
				nkm_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
				break;
			case NKM_GAME_SPEED_TYPE.NGST_3:
				nkm_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_05;
				break;
			case NKM_GAME_SPEED_TYPE.NGST_05:
				nkm_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
				break;
			default:
				nkm_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
				break;
			}
			NKCGameHud.HUDMode eMode = this.m_eMode;
			if (eMode == NKCGameHud.HUDMode.Normal || eMode != NKCGameHud.HUDMode.Replay)
			{
				this.m_GameClient.Send_Packet_GAME_SPEED_2X_REQ(nkm_GAME_SPEED_TYPE);
				return;
			}
			NKCReplayMgr.GetNKCReplaMgr().SetPlayingGameSpeedType(nkm_GAME_SPEED_TYPE);
			this.ChangeGameSpeedTypeUI(nkm_GAME_SPEED_TYPE);
		}

		// Token: 0x060091F4 RID: 37364 RVA: 0x0031D020 File Offset: 0x0031B220
		public void SendGameAutoSkillChange()
		{
			NKM_GAME_AUTO_SKILL_TYPE eAutoSkillType = this.m_eAutoSkillType;
			if (eAutoSkillType == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO)
			{
				this.m_GameClient.Send_Packet_GAME_AUTO_SKILL_CHANGE_REQ(NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER, true);
				return;
			}
			if (eAutoSkillType != NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER)
			{
				this.m_GameClient.Send_Packet_GAME_AUTO_SKILL_CHANGE_REQ(NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO, true);
				return;
			}
			this.m_GameClient.Send_Packet_GAME_AUTO_SKILL_CHANGE_REQ(NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO, true);
		}

		// Token: 0x060091F5 RID: 37365 RVA: 0x0031D068 File Offset: 0x0031B268
		protected virtual bool IsChangeGameSpeedVisible()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_2X, 0, 0))
			{
				return false;
			}
			NKCGameHud.HUDMode eMode = this.m_eMode;
			if (eMode != NKCGameHud.HUDMode.Normal)
			{
				if (eMode != NKCGameHud.HUDMode.Replay)
				{
				}
			}
			else
			{
				if (this.m_GameClient.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
				{
					return false;
				}
				if (this.m_GameClient.GetGameData().IsPVP() && NKMGame.IsPVPSync(this.m_GameClient.GetGameData().GetGameType()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060091F6 RID: 37366 RVA: 0x0031D0D4 File Offset: 0x0031B2D4
		public void ChangeGameSpeedTypeUI(NKM_GAME_SPEED_TYPE eNKM_GAME_SPEED_TYPE)
		{
			this.m_NKM_GAME_SPEED_TYPE = eNKM_GAME_SPEED_TYPE;
			bool flag = this.IsChangeGameSpeedVisible();
			NKCUtil.SetGameobjectActive(this.m_csbtnGameSpeed, flag);
			if (!flag)
			{
				return;
			}
			this.m_imgGameSpeed1.color = NKCUtil.GetColor("#FFFFFF");
			switch (this.m_NKM_GAME_SPEED_TYPE)
			{
			case NKM_GAME_SPEED_TYPE.NGST_1:
				this.m_imgGameSpeed1.gameObject.SetActive(true);
				this.m_imgGameSpeed2.gameObject.SetActive(false);
				this.m_imgGameSpeed3.gameObject.SetActive(false);
				this.m_imgGameSpeed05.gameObject.SetActive(false);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_2:
				this.m_imgGameSpeed1.gameObject.SetActive(false);
				this.m_imgGameSpeed2.gameObject.SetActive(true);
				this.m_imgGameSpeed3.gameObject.SetActive(false);
				this.m_imgGameSpeed05.gameObject.SetActive(false);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_3:
				this.m_imgGameSpeed1.gameObject.SetActive(false);
				this.m_imgGameSpeed2.gameObject.SetActive(false);
				this.m_imgGameSpeed3.gameObject.SetActive(true);
				this.m_imgGameSpeed05.gameObject.SetActive(false);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_05:
				this.m_imgGameSpeed1.gameObject.SetActive(false);
				this.m_imgGameSpeed2.gameObject.SetActive(false);
				this.m_imgGameSpeed3.gameObject.SetActive(false);
				this.m_imgGameSpeed05.gameObject.SetActive(true);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_10:
				this.m_imgGameSpeed1.gameObject.SetActive(false);
				this.m_imgGameSpeed2.gameObject.SetActive(false);
				this.m_imgGameSpeed3.gameObject.SetActive(true);
				this.m_imgGameSpeed05.gameObject.SetActive(false);
				return;
			default:
				this.m_imgGameSpeed1.gameObject.SetActive(true);
				this.m_imgGameSpeed2.gameObject.SetActive(false);
				this.m_imgGameSpeed3.gameObject.SetActive(false);
				this.m_imgGameSpeed05.gameObject.SetActive(false);
				return;
			}
		}

		// Token: 0x060091F7 RID: 37367 RVA: 0x0031D2D8 File Offset: 0x0031B4D8
		protected virtual bool IsAutoSkillVisible()
		{
			NKCGameHud.HUDMode eMode = this.m_eMode;
			if (eMode == NKCGameHud.HUDMode.Normal || eMode - NKCGameHud.HUDMode.Replay > 1)
			{
				bool result = true;
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_AUTO_SKILL, 0, 0))
				{
					result = false;
				}
				if (this.m_GameClient.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
				{
					result = false;
				}
				return result;
			}
			return false;
		}

		// Token: 0x060091F8 RID: 37368 RVA: 0x0031D31C File Offset: 0x0031B51C
		public void ChangeGameAutoSkillTypeUI(NKM_GAME_AUTO_SKILL_TYPE eNKM_GAME_AUTO_SKILL_TYPE)
		{
			this.m_eAutoSkillType = eNKM_GAME_AUTO_SKILL_TYPE;
			bool flag = this.IsAutoSkillVisible();
			NKCUtil.SetGameobjectActive(this.m_btnAutoHyper, flag);
			if (!flag)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objAutoHyperOn, this.m_eAutoSkillType == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO);
			NKCUtil.SetGameobjectActive(this.m_objAutoHyperOff, this.m_eAutoSkillType == NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER);
		}

		// Token: 0x060091F9 RID: 37369 RVA: 0x0031D36F File Offset: 0x0031B56F
		public void DevHideHud(bool bHide)
		{
			if (bHide)
			{
				this.m_GameClient.SetShowUI(false, true);
				NKCUtil.SetGameobjectActive(this.m_objHide, false);
				NKCUtil.SetGameobjectActive(this.m_objUnhide, false);
				return;
			}
			this.HUD_UNHIDE();
		}

		// Token: 0x060091FA RID: 37370 RVA: 0x0031D3A0 File Offset: 0x0031B5A0
		private void OnBtnHideHud()
		{
			this.HUD_HIDE(false);
		}

		// Token: 0x060091FB RID: 37371 RVA: 0x0031D3AC File Offset: 0x0031B5AC
		public void HUD_HIDE(bool bHideCompletly = false)
		{
			this.m_GameClient.SetShowUI(false, true);
			NKCUtil.SetGameobjectActive(this.m_objUnhide, !bHideCompletly);
			NKCUtil.SetGameobjectActive(this.m_objHide, false);
			if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT() != null && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT() != null)
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT().transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT().transform, false);
			}
			if (bHideCompletly)
			{
				NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT(), false);
				NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_EFFECT(), false);
				NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR(), false);
				NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT(), false);
			}
		}

		// Token: 0x060091FC RID: 37372 RVA: 0x0031D4BC File Offset: 0x0031B6BC
		public void HUD_UNHIDE()
		{
			this.m_GameClient.SetShowUI(true, true);
			NKCUtil.SetGameobjectActive(this.m_objUnhide, false);
			NKCUtil.SetGameobjectActive(this.m_objHide, true);
			if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT() != null && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR() != null)
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT().transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR().transform, false);
			}
			NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT(), true);
			NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_EFFECT(), true);
			NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR(), true);
			NKCUtil.SetGameobjectActive(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT(), true);
		}

		// Token: 0x060091FD RID: 37373 RVA: 0x0031D5C6 File Offset: 0x0031B7C6
		public virtual void SetNetworkWeak(bool bOn)
		{
			NKCUtil.SetGameobjectActive(this.m_objNetworkWeak, bOn);
		}

		// Token: 0x060091FE RID: 37374 RVA: 0x0031D5D4 File Offset: 0x0031B7D4
		private bool IsAutoRespawnUsable()
		{
			return this.IsAutoRespawnVisible() && this.m_GameClient.CanUseAutoRespawn(NKCScenManager.CurrentUserData());
		}

		// Token: 0x060091FF RID: 37375 RVA: 0x0031D5F8 File Offset: 0x0031B7F8
		protected virtual bool IsAutoRespawnVisible()
		{
			NKCGameHud.HUDMode eMode = this.m_eMode;
			return (eMode == NKCGameHud.HUDMode.Normal || eMode - NKCGameHud.HUDMode.Replay > 1) && NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_AUTO_RESPAWN, 0, 0) && (this.m_GameClient.GetDungeonTemplet() == null || this.m_GameClient.GetDungeonTemplet().m_bCanUseAuto) && this.m_GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_TUTORIAL && this.m_GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE;
		}

		// Token: 0x06009200 RID: 37376 RVA: 0x0031D670 File Offset: 0x0031B870
		public void RefreshClassGuide()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			NKCUtil.SetGameobjectActive(this.m_objClassGuide, gameOptionData == null || gameOptionData.UseClassGuide);
		}

		// Token: 0x06009201 RID: 37377 RVA: 0x0031D69F File Offset: 0x0031B89F
		public void OnClickedNetworkLevel(PointerEventData eventData)
		{
			NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, NKCUtilString.GET_STRING_TOOLTIP_ETC_NETWORK_TITLE, NKCUtilString.GET_STRING_TOOLTIP_ETC_NETWORK_DESC, new Vector2?(eventData.position));
		}

		// Token: 0x06009202 RID: 37378 RVA: 0x0031D6C2 File Offset: 0x0031B8C2
		public void SetTimeWarningFX(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_objHUDBgFx, bActive);
		}

		// Token: 0x06009203 RID: 37379 RVA: 0x0031D6D0 File Offset: 0x0031B8D0
		public int GetRespawnCost(NKMUnitStatTemplet cNKMUnitStatTemplet, bool bLeader)
		{
			return this.m_GameClient.GetRespawnCost(cNKMUnitStatTemplet, bLeader, this.CurrentViewTeamType);
		}

		// Token: 0x06009204 RID: 37380 RVA: 0x0031D6E5 File Offset: 0x0031B8E5
		public bool IsBanUnit(int unitID)
		{
			return !this.IsNotAllowUpBan() && this.m_GameClient.IsBanUnit(unitID);
		}

		// Token: 0x06009205 RID: 37381 RVA: 0x0031D6FD File Offset: 0x0031B8FD
		public bool IsUpUnit(int unitID)
		{
			return !this.IsNotAllowUpBan() && this.m_GameClient.IsUpUnit(unitID);
		}

		// Token: 0x06009206 RID: 37382 RVA: 0x0031D715 File Offset: 0x0031B915
		private bool IsFristUnitMain(NKM_GAME_TYPE game_type)
		{
			return game_type == NKM_GAME_TYPE.NGT_ASYNC_PVP || game_type - NKM_GAME_TYPE.NGT_PVP_STRATEGY <= 2;
		}

		// Token: 0x06009207 RID: 37383 RVA: 0x0031D728 File Offset: 0x0031B928
		private bool IsNotAllowUpBan()
		{
			if (this.m_GameClient != null)
			{
				NKM_GAME_TYPE nkm_GAME_TYPE = this.m_GameClient.GetGameData().m_NKM_GAME_TYPE;
				return nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PVP_STRATEGY <= 2;
			}
			return false;
		}

		// Token: 0x06009208 RID: 37384 RVA: 0x0031D75A File Offset: 0x0031B95A
		public void PlayDangerMsg(string msg)
		{
			if (this.m_NKCUIDangerMessage != null)
			{
				this.m_NKCUIDangerMessage.Play(msg);
			}
		}

		// Token: 0x06009209 RID: 37385 RVA: 0x0031D776 File Offset: 0x0031B976
		public void StopDangerMsg()
		{
			if (this.m_NKCUIDangerMessage != null)
			{
				this.m_NKCUIDangerMessage.Stop();
			}
		}

		// Token: 0x0600920A RID: 37386 RVA: 0x0031D791 File Offset: 0x0031B991
		public bool IsShowDangerMsg()
		{
			return !(this.m_NKCUIDangerMessage == null) && this.m_NKCUIDangerMessage.gameObject.activeInHierarchy;
		}

		// Token: 0x0600920B RID: 37387 RVA: 0x0031D7B4 File Offset: 0x0031B9B4
		private void UpdateMultiplyAlert()
		{
			if (this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (this.bReservedMultiply)
			{
				this.bReservedMultiply = false;
				if (this.m_GameClient.MultiplyReward > 1 && this.m_NKCUIGameHUDMultiplyReward != null)
				{
					NKCUtil.SetGameobjectActive(this.m_NKCUIGameHUDMultiplyReward, true);
				}
			}
		}

		// Token: 0x0600920C RID: 37388 RVA: 0x0031D80C File Offset: 0x0031BA0C
		public void OnClickedDeadlineBuff(PointerEventData eventData)
		{
			NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, NKCUtilString.GET_STRING_TOOLTIP_DEADLINE_BUFF_TITLE, this.m_strDeadLineBuffString, new Vector2?(eventData.position));
		}

		// Token: 0x0600920D RID: 37389 RVA: 0x0031D830 File Offset: 0x0031BA30
		public void PrepareAlerts(NKMGameData gameData)
		{
			if (gameData.GetGameType() == NKM_GAME_TYPE.NGT_PHASE && this.m_AlertPhase != null)
			{
				string @string = NKCStringTable.GetString("SI_DP_GAME_PHASE_COUNT", new object[]
				{
					NKCPhaseManager.PhaseModeState.phaseIndex + 1
				});
				this.m_AlertPhase.AddAlert(@string, "", "");
				this.EnqueueHudAlert(this.m_AlertPhase);
			}
			bool flag = false;
			foreach (int bCondID in gameData.m_BattleConditionIDs.Keys)
			{
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
				if (templetByID != null && templetByID.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_BATTLE_CONDITION)
				{
					this.m_AlertEnv.AddAlert(templetByID.BattleCondName_Translated, templetByID.BattleCondDesc_Translated, templetByID.BattleCondIngameIcon);
					flag = true;
				}
			}
			if (flag)
			{
				this.EnqueueHudAlert(this.m_AlertEnv);
			}
		}

		// Token: 0x0600920E RID: 37390 RVA: 0x0031D920 File Offset: 0x0031BB20
		private void EnqueueHudAlert(IGameHudAlert alert)
		{
			this.m_qHudAlert.Enqueue(alert);
		}

		// Token: 0x0600920F RID: 37391 RVA: 0x0031D930 File Offset: 0x0031BB30
		private void UpdateHudAlert()
		{
			if (this.m_GameClient.GetGameRuntimeData().m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (this.m_currentHudAlert != null && this.m_currentHudAlert.IsFinished())
			{
				this.m_currentHudAlert.OnCleanup();
				this.m_currentHudAlert.SetActive(false);
				this.m_currentHudAlert = null;
			}
			if (this.m_currentHudAlert == null && this.m_qHudAlert.Count > 0)
			{
				this.m_currentHudAlert = this.m_qHudAlert.Dequeue();
				this.m_currentHudAlert.SetActive(true);
				this.m_currentHudAlert.OnStart();
			}
			if (this.m_currentHudAlert != null)
			{
				this.m_currentHudAlert.OnUpdate();
				return;
			}
			this.UpdateMultiplyAlert();
		}

		// Token: 0x06009210 RID: 37392 RVA: 0x0031D9DC File Offset: 0x0031BBDC
		private void CleanupAllHudAlert()
		{
			if (this.m_currentHudAlert != null)
			{
				this.m_currentHudAlert.OnCleanup();
				this.m_currentHudAlert.SetActive(false);
				this.m_currentHudAlert = null;
			}
			this.m_qHudAlert.Clear();
		}

		// Token: 0x06009211 RID: 37393 RVA: 0x0031DA0F File Offset: 0x0031BC0F
		private bool IsAlertProcessFinished()
		{
			return this.m_currentHudAlert == null && this.m_qHudAlert.Count == 0;
		}

		// Token: 0x06009212 RID: 37394 RVA: 0x0031DA2C File Offset: 0x0031BC2C
		private void ProcessHotkey()
		{
			if (this.IsOpenPause())
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				return;
			}
			if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.Unit0))
			{
				this.OnDeckHotkey(0);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.Unit0);
			}
			else if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.Unit1))
			{
				this.OnDeckHotkey(1);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.Unit1);
			}
			else if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.Unit2))
			{
				this.OnDeckHotkey(2);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.Unit2);
			}
			else if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.Unit3))
			{
				this.OnDeckHotkey(3);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.Unit3);
			}
			else if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.UnitAssist))
			{
				if (this.m_GameClient.GetMyTeamData().m_DeckData.GetAutoRespawnIndexAssist() == -1)
				{
					return;
				}
				this.OnDeckHotkey(5);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.UnitAssist);
			}
			else if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.ShipSkill0))
			{
				this.OnShipSkillDeck(0);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.ShipSkill0);
			}
			else if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.ShipSkill1))
			{
				this.OnShipSkillDeck(1);
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.ShipSkill1);
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
			{
				NKCUIComHotkeyDisplay.OpenInstance(this.m_trHotkeyPosLeft, HotkeyEventType.Left);
				NKCUIComHotkeyDisplay.OpenInstance(this.m_trHotkeyPosRight, HotkeyEventType.Right);
				NKCUIComHotkeyDisplay.OpenInstance(this.m_NKCUIHudDeck[0].m_rtSubRoot, InGamehotkeyEventType.Unit0);
				NKCUIComHotkeyDisplay.OpenInstance(this.m_NKCUIHudDeck[1].m_rtSubRoot, InGamehotkeyEventType.Unit1);
				NKCUIComHotkeyDisplay.OpenInstance(this.m_NKCUIHudDeck[2].m_rtSubRoot, InGamehotkeyEventType.Unit2);
				NKCUIComHotkeyDisplay.OpenInstance(this.m_NKCUIHudDeck[3].m_rtSubRoot, InGamehotkeyEventType.Unit3);
				NKCUIComHotkeyDisplay.OpenInstance(this.m_NKCUIHudDeck[5].m_rtSubRoot, InGamehotkeyEventType.UnitAssist);
				if (this.GetShipSkillDeck(0) != null)
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.GetShipSkillDeck(0).transform, InGamehotkeyEventType.ShipSkill0);
				}
				if (this.GetShipSkillDeck(1) != null)
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.GetShipSkillDeck(1).transform, InGamehotkeyEventType.ShipSkill1);
				}
			}
		}

		// Token: 0x06009213 RID: 37395 RVA: 0x0031DBD7 File Offset: 0x0031BDD7
		private void OnDeckHotkey(int index)
		{
			if (this.m_SelectUnitDeckIndex == index)
			{
				return;
			}
			this.TouchUpDeck(index, false);
			this.GetGameClient().OnUnitDeckHotkey(index);
		}

		// Token: 0x06009214 RID: 37396 RVA: 0x0031DBF7 File Offset: 0x0031BDF7
		private void OnShipSkillDeck(int index)
		{
			if (this.m_SelectShipSkillDeckIndex == index)
			{
				return;
			}
			this.TouchUpShipSkillDeck(index);
			this.GetGameClient().OnShipSkillhotkey(index);
		}

		// Token: 0x06009215 RID: 37397 RVA: 0x0031DC18 File Offset: 0x0031BE18
		public void HideHud(bool bEventControl = false)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				this.m_NKCUIHudDeck[i].SetActive(false, bEventControl);
			}
			for (int j = 0; j < this.m_NKCUIHudShipSkillDeck.Length; j++)
			{
				this.m_NKCUIHudShipSkillDeck[j].SetActive(false, bEventControl);
			}
		}

		// Token: 0x06009216 RID: 37398 RVA: 0x0031DC6C File Offset: 0x0031BE6C
		public void ShowHud(bool bEventControl = false)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				if (i != 5)
				{
					this.m_NKCUIHudDeck[i].SetActive(true, bEventControl);
				}
			}
			for (int j = 0; j < this.m_NKCUIHudShipSkillDeck.Length; j++)
			{
				this.m_NKCUIHudShipSkillDeck[j].SetActive(true, bEventControl);
			}
		}

		// Token: 0x06009217 RID: 37399 RVA: 0x0031DCC4 File Offset: 0x0031BEC4
		public void ShowHudDeck(int targetUnitID)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot.m_UnitData != null && nkcgameHudDeckSlot.m_UnitData.m_UnitID == targetUnitID)
				{
					NKCUtil.SetGameobjectActive(nkcgameHudDeckSlot, true);
					return;
				}
			}
		}

		// Token: 0x06009218 RID: 37400 RVA: 0x0031DD0C File Offset: 0x0031BF0C
		public NKCGameHudDeckSlot GetHudDeckByUnitID(int targetUnitID)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot.m_UnitData != null && nkcgameHudDeckSlot.m_UnitData.m_UnitID == targetUnitID)
				{
					return nkcgameHudDeckSlot;
				}
			}
			return null;
		}

		// Token: 0x06009219 RID: 37401 RVA: 0x0031DD50 File Offset: 0x0031BF50
		public NKCGameHudDeckSlot GetHudDeckByUnitUID(long targetUnitUID)
		{
			for (int i = 0; i < this.m_NKCUIHudDeck.Length; i++)
			{
				NKCGameHudDeckSlot nkcgameHudDeckSlot = this.m_NKCUIHudDeck[i];
				if (nkcgameHudDeckSlot.m_UnitData != null && nkcgameHudDeckSlot.m_UnitData.m_UnitUID == targetUnitUID)
				{
					return nkcgameHudDeckSlot;
				}
			}
			return null;
		}

		// Token: 0x0600921A RID: 37402 RVA: 0x0031DD94 File Offset: 0x0031BF94
		public void ShowHudSkill(int skillID)
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				this.m_NKCUIHudShipSkillDeck[i].SetActive(false, false);
			}
		}

		// Token: 0x0600921B RID: 37403 RVA: 0x0031DDC4 File Offset: 0x0031BFC4
		public NKCUIHudShipSkillDeck GetHudSkillBySkillID(int skillID)
		{
			for (int i = 0; i < this.m_NKCUIHudShipSkillDeck.Length; i++)
			{
				NKCUIHudShipSkillDeck nkcuihudShipSkillDeck = this.m_NKCUIHudShipSkillDeck[i];
				if (nkcuihudShipSkillDeck.m_NKMShipSkillTemplet != null && nkcuihudShipSkillDeck.m_NKMShipSkillTemplet.m_ShipSkillID == skillID)
				{
					return nkcuihudShipSkillDeck;
				}
			}
			return null;
		}

		// Token: 0x0600921C RID: 37404 RVA: 0x0031DE06 File Offset: 0x0031C006
		public void SetKillCount(long killCount)
		{
			if (this.m_killCount != null)
			{
				this.m_killCount.SetKillCount(killCount);
			}
		}

		// Token: 0x0600921D RID: 37405 RVA: 0x0031DE24 File Offset: 0x0031C024
		private NKCGameHudSummonIndicator GetSummonIndicator()
		{
			foreach (NKCGameHudSummonIndicator nkcgameHudSummonIndicator in this.m_lstSummonIndicator)
			{
				if (nkcgameHudSummonIndicator != null && nkcgameHudSummonIndicator.Idle)
				{
					return nkcgameHudSummonIndicator;
				}
			}
			return this.MakeSummonIndicator();
		}

		// Token: 0x0600921E RID: 37406 RVA: 0x0031DE90 File Offset: 0x0031C090
		private NKCGameHudSummonIndicator MakeSummonIndicator()
		{
			GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_WARNING", false);
			if (orLoadAssetResource == null)
			{
				return null;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource, this.m_trIndicatorRootLeft);
			if (gameObject == null)
			{
				return null;
			}
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			NKCGameHudSummonIndicator nkcgameHudSummonIndicator;
			if (gameObject.TryGetComponent<NKCGameHudSummonIndicator>(out nkcgameHudSummonIndicator))
			{
				this.m_lstSummonIndicator.Add(nkcgameHudSummonIndicator);
				NKCUtil.SetGameobjectActive(nkcgameHudSummonIndicator, false);
				return nkcgameHudSummonIndicator;
			}
			return null;
		}

		// Token: 0x0600921F RID: 37407 RVA: 0x0031DF14 File Offset: 0x0031C114
		public void SetUnitIndicator(NKCUnitClient cUnit)
		{
			NKCGameHudSummonIndicator summonIndicator = this.GetSummonIndicator();
			if (summonIndicator == null)
			{
				return;
			}
			if (!summonIndicator.SetData(cUnit, this.m_GameClient, this.m_trIndicatorRootLeft, this.m_trIndicatorRootRight))
			{
				NKCUtil.SetGameobjectActive(summonIndicator, false);
			}
		}

		// Token: 0x04007E92 RID: 32402
		private NKCGameClient m_GameClient;

		// Token: 0x04007E93 RID: 32403
		private NKM_TEAM_TYPE m_CurrentViewTeamType;

		// Token: 0x04007E94 RID: 32404
		[Header("서브모드 관련")]
		public NKCGameHudReplay m_HudReplay;

		// Token: 0x04007E95 RID: 32405
		public NKCGameHudObserver m_HudObserver;

		// Token: 0x04007E96 RID: 32406
		[Header("덱")]
		public const int NEXT_DECK_INDEX = 4;

		// Token: 0x04007E97 RID: 32407
		public const int ASSIST_DECK_INDEX = 5;

		// Token: 0x04007E98 RID: 32408
		public NKCGameHudDeckSlot[] m_NKCUIHudDeck;

		// Token: 0x04007E99 RID: 32409
		public GameObject m_objRemainUnitCount;

		// Token: 0x04007E9A RID: 32410
		public Text m_lbRemainUnitCount;

		// Token: 0x04007E9B RID: 32411
		[Header("함선 스킬")]
		public NKCUIHudShipSkillDeck[] m_NKCUIHudShipSkillDeck;

		// Token: 0x04007E9C RID: 32412
		private const int TACTICAL_COMMAND_DECK_COUNT = 1;

		// Token: 0x04007E9D RID: 32413
		private NKCUIHudTacticalCommandDeck[] m_NKCUIHudTacticalCommandDeck = new NKCUIHudTacticalCommandDeck[1];

		// Token: 0x04007E9E RID: 32414
		[Header("오브젝트")]
		public GameObject m_objTop;

		// Token: 0x04007E9F RID: 32415
		public GameObject m_objRootShipSkill;

		// Token: 0x04007EA0 RID: 32416
		public GameObject m_objHUDBg;

		// Token: 0x04007EA1 RID: 32417
		public GameObject m_objHUDBgFx;

		// Token: 0x04007EA2 RID: 32418
		[Header("상부 게이지들")]
		public NKCUIHudRespawnGage m_NKCUIHudRespawnGage;

		// Token: 0x04007EA3 RID: 32419
		public NKCUIMainHPGage m_NKCUIMainHPGageAlly;

		// Token: 0x04007EA4 RID: 32420
		public NKCUIMainHPGage m_NKCUIMainHPGageEnemy;

		// Token: 0x04007EA5 RID: 32421
		public NKCUIMainHPGage m_NKCUIMainHPGageAllyLong;

		// Token: 0x04007EA6 RID: 32422
		public NKCUIMainHPGage m_NKCUIMainHPGageEnemyLong;

		// Token: 0x04007EA7 RID: 32423
		public NKCUIGameUnitSkillCooltime m_NKCUIMainSkillCoolLeft;

		// Token: 0x04007EA8 RID: 32424
		public NKCUIGameUnitSkillCooltime m_NKCUIMainSkillCoolRight;

		// Token: 0x04007EA9 RID: 32425
		[Header("리더/보스 얼굴(좌)")]
		public NKCUIComButton m_btnLeftUser;

		// Token: 0x04007EAA RID: 32426
		public Image m_imgLeftUser;

		// Token: 0x04007EAB RID: 32427
		public Image m_imgLeftUserRole;

		// Token: 0x04007EAC RID: 32428
		public Image m_imgLeftUserAttackType;

		// Token: 0x04007EAD RID: 32429
		public Text m_lbLeftUserName;

		// Token: 0x04007EAE RID: 32430
		public GameObject m_objLeftUserRage;

		// Token: 0x04007EAF RID: 32431
		[Header("리더/보스 얼굴(우)")]
		public NKCUIComButton m_btnRightUser;

		// Token: 0x04007EB0 RID: 32432
		public Image m_imgRightUser;

		// Token: 0x04007EB1 RID: 32433
		public Image m_imgRightUserRole;

		// Token: 0x04007EB2 RID: 32434
		public Image m_imgRightUserAttackType;

		// Token: 0x04007EB3 RID: 32435
		public Text m_lbRightUserName;

		// Token: 0x04007EB4 RID: 32436
		public GameObject m_objRightUserRage;

		// Token: 0x04007EB5 RID: 32437
		[Header("웨이브 넘버")]
		public GameObject m_objWave;

		// Token: 0x04007EB6 RID: 32438
		public Text m_lbWave;

		// Token: 0x04007EB7 RID: 32439
		private int m_WaveCount;

		// Token: 0x04007EB8 RID: 32440
		[Header("남은 시간")]
		public Text m_lbTimeLeft;

		// Token: 0x04007EB9 RID: 32441
		[Header("미니맵")]
		public RectTransform m_rtMiniMap;

		// Token: 0x04007EBA RID: 32442
		public RectTransform m_rtMinimapCamPanel;

		// Token: 0x04007EBB RID: 32443
		private float m_NUF_GAME_HUD_MINI_MAP_RectTransform_width;

		// Token: 0x04007EBC RID: 32444
		[Header("일시정지")]
		public NKCUIComButton m_btnPause;

		// Token: 0x04007EBD RID: 32445
		public Image m_imgPause;

		// Token: 0x04007EBE RID: 32446
		[Header("UI 숨기기")]
		public GameObject m_objUnhide;

		// Token: 0x04007EBF RID: 32447
		public NKCUIComButton m_btnUnhide;

		// Token: 0x04007EC0 RID: 32448
		public GameObject m_objHide;

		// Token: 0x04007EC1 RID: 32449
		public NKCUIComButton m_btnHide;

		// Token: 0x04007EC2 RID: 32450
		[Header("리그전 버프 버튼")]
		public NKCUIComStateButton m_csbtnDeadlineBuff;

		// Token: 0x04007EC3 RID: 32451
		public Text m_lbDeadLineBuffLevel;

		// Token: 0x04007EC4 RID: 32452
		private string m_strDeadLineBuffString;

		// Token: 0x04007EC5 RID: 32453
		private int m_DeadlineBuffLevel;

		// Token: 0x04007EC6 RID: 32454
		[Header("공격 포인트(레이드 등)")]
		public GameObject m_objAttackPoint;

		// Token: 0x04007EC7 RID: 32455
		public Text m_lbAttackPointNow;

		// Token: 0x04007EC8 RID: 32456
		public Text m_lbAttackPointMax;

		// Token: 0x04007EC9 RID: 32457
		private float m_fAttackPointLeftPivotValue;

		// Token: 0x04007ECA RID: 32458
		private float m_fAttackPointLeftCurrValue;

		// Token: 0x04007ECB RID: 32459
		private int m_AttackPointLeftTargetValue;

		// Token: 0x04007ECC RID: 32460
		private float m_fElapsedTimeForAP;

		// Token: 0x04007ECD RID: 32461
		[Header("반복작전")]
		public GameObject m_objMultiplyReward;

		// Token: 0x04007ECE RID: 32462
		public Text m_lbRewardMultiply;

		// Token: 0x04007ECF RID: 32463
		public NKCGameHUDRepeatOperation m_NKCGameHUDRepeatOperation;

		// Token: 0x04007ED0 RID: 32464
		[Header("배속 버튼")]
		public NKCUIComStateButton m_csbtnGameSpeed;

		// Token: 0x04007ED1 RID: 32465
		public Image m_imgGameSpeed1;

		// Token: 0x04007ED2 RID: 32466
		public Image m_imgGameSpeed2;

		// Token: 0x04007ED3 RID: 32467
		public Image m_imgGameSpeed3;

		// Token: 0x04007ED4 RID: 32468
		public Image m_imgGameSpeed05;

		// Token: 0x04007ED5 RID: 32469
		protected NKM_GAME_SPEED_TYPE m_NKM_GAME_SPEED_TYPE;

		// Token: 0x04007ED6 RID: 32470
		[Header("자동 출격")]
		public GameObject m_objAutoRespawn;

		// Token: 0x04007ED7 RID: 32471
		public NKCUIComButton m_btnAutoRespawn;

		// Token: 0x04007ED8 RID: 32472
		public Animator m_animatorAutoRespawn;

		// Token: 0x04007ED9 RID: 32473
		public Image m_imgAutoRespawnOff;

		// Token: 0x04007EDA RID: 32474
		[Header("오토 스킬")]
		public NKCUIComButton m_btnAutoHyper;

		// Token: 0x04007EDB RID: 32475
		public GameObject m_objAutoHyperOn;

		// Token: 0x04007EDC RID: 32476
		public GameObject m_objAutoHyperOff;

		// Token: 0x04007EDD RID: 32477
		private NKM_GAME_AUTO_SKILL_TYPE m_eAutoSkillType;

		// Token: 0x04007EDE RID: 32478
		[Header("네트워크 현황")]
		public GameObject m_objNetworkWeak;

		// Token: 0x04007EDF RID: 32479
		public NKCUIComStateButton m_csbtnNetworkLevel;

		// Token: 0x04007EE0 RID: 32480
		public Text m_lbNetworkLevel;

		// Token: 0x04007EE1 RID: 32481
		[Header("알람 메시지")]
		public GameObject m_objHUDMessage;

		// Token: 0x04007EE2 RID: 32482
		public Animator m_animatorHUDMessage;

		// Token: 0x04007EE3 RID: 32483
		public Text m_lbHUDMessage;

		// Token: 0x04007EE4 RID: 32484
		public GameObject m_objTimeOver;

		// Token: 0x04007EE5 RID: 32485
		public NKCGameHudAlertCommon m_AlertEnv;

		// Token: 0x04007EE6 RID: 32486
		public NKCGameHudAlertCommon m_AlertPhase;

		// Token: 0x04007EE7 RID: 32487
		[Header("소규모 서브 메뉴들")]
		public NKCGameHudPractice m_GameHudPractice;

		// Token: 0x04007EE8 RID: 32488
		public NKCGameHudKillCount m_killCount;

		// Token: 0x04007EE9 RID: 32489
		public GameObject m_objClassGuide;

		// Token: 0x04007EEA RID: 32490
		public Text m_lbUnitMaxCountSameTime;

		// Token: 0x04007EEB RID: 32491
		[Header("기타 서브메뉴")]
		public GameObject m_objOperatorPanelRoot;

		// Token: 0x04007EEC RID: 32492
		[Header("핫키/인디케이터 위치잡이")]
		public Transform m_trHotkeyPosLeft;

		// Token: 0x04007EED RID: 32493
		public Transform m_trHotkeyPosRight;

		// Token: 0x04007EEE RID: 32494
		public Transform m_trIndicatorRootLeft;

		// Token: 0x04007EEF RID: 32495
		public Transform m_trIndicatorRootRight;

		// Token: 0x04007EF0 RID: 32496
		private int m_SelectUnitDeckIndex = -1;

		// Token: 0x04007EF1 RID: 32497
		private int m_SelectShipSkillDeckIndex = -1;

		// Token: 0x04007EF2 RID: 32498
		private int m_AutoRespawnIndex;

		// Token: 0x04007EF3 RID: 32499
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04007EF4 RID: 32500
		private int m_RemainGameTimeInt;

		// Token: 0x04007EF5 RID: 32501
		private bool m_bCountDownVoicePlayed;

		// Token: 0x04007EF6 RID: 32502
		private bool m_bShipSkillReadyVoicePlayed;

		// Token: 0x04007EF7 RID: 32503
		private bool m_bShipSkillReady;

		// Token: 0x04007EF8 RID: 32504
		private float m_fShipSkillFullTime;

		// Token: 0x04007EF9 RID: 32505
		private bool m_bCostFullVoicePlayed;

		// Token: 0x04007EFA RID: 32506
		private bool m_bCostFull;

		// Token: 0x04007EFB RID: 32507
		private float m_fMaxCostTime;

		// Token: 0x04007EFC RID: 32508
		private const string ICON_ENABLE_COLOR = "#FFFFFF";

		// Token: 0x04007EFD RID: 32509
		private const string ICON_DISABLE_COLOR = "#7B7B7B";

		// Token: 0x04007EFE RID: 32510
		public NKCGameHud.OnUseDeck dOnUseDeck;

		// Token: 0x04007EFF RID: 32511
		public NKCGameHud.OnUseSkill dOnUseSkill;

		// Token: 0x04007F00 RID: 32512
		private NKCGameHUDArtifact m_NKCGameHUDArtifact;

		// Token: 0x04007F01 RID: 32513
		private NKCAssetInstanceData m_NKCAssetInstanceDataArtifact;

		// Token: 0x04007F02 RID: 32514
		private NKCGameHudEmoticon m_NKCGameHudEmoticon;

		// Token: 0x04007F03 RID: 32515
		private NKCAssetInstanceData m_NKCAssetInstanceDataEmoticon;

		// Token: 0x04007F04 RID: 32516
		private NKCGameHudPause m_NKCGameHudPause;

		// Token: 0x04007F05 RID: 32517
		private NKCAssetInstanceData m_NKCAssetInstanceDataPause;

		// Token: 0x04007F06 RID: 32518
		private NKCUIDangerMessage m_NKCUIDangerMessage;

		// Token: 0x04007F07 RID: 32519
		private NKCAssetInstanceData m_NKCAssetInstanceDataDangerMessage;

		// Token: 0x04007F08 RID: 32520
		private NKCUIGameHUDMultiplyReward m_NKCUIGameHUDMultiplyReward;

		// Token: 0x04007F09 RID: 32521
		private NKCAssetInstanceData m_NKCAssetInstanceDataMultiplyReward;

		// Token: 0x04007F0A RID: 32522
		protected NKCGameHudCombo m_NKCGameHudCombo;

		// Token: 0x04007F0B RID: 32523
		private NKCAssetInstanceData m_NKCAssetInstanceDataCombo;

		// Token: 0x04007F0C RID: 32524
		private bool bReservedMultiply;

		// Token: 0x04007F0D RID: 32525
		private NKCGameHUDFierceScore m_NKCGameHUDTrim;

		// Token: 0x04007F0E RID: 32526
		private NKCAssetInstanceData m_NKCAssetInstanceDataTrimScore;

		// Token: 0x04007F0F RID: 32527
		private NKCGameHud.HUDMode m_eMode;

		// Token: 0x04007F10 RID: 32528
		private NKCGameHudObjects m_HudObjects;

		// Token: 0x04007F11 RID: 32529
		private NKCGameHUDFierceScore m_NKCGameHUDFierce;

		// Token: 0x04007F12 RID: 32530
		private NKCAssetInstanceData m_NKCAssetInstanceDataFierceScore;

		// Token: 0x04007F13 RID: 32531
		private IGameHudAlert m_currentHudAlert;

		// Token: 0x04007F14 RID: 32532
		private Queue<IGameHudAlert> m_qHudAlert = new Queue<IGameHudAlert>();

		// Token: 0x04007F15 RID: 32533
		private List<NKCGameHudSummonIndicator> m_lstSummonIndicator = new List<NKCGameHudSummonIndicator>();

		// Token: 0x02001A07 RID: 6663
		public enum HUDMode
		{
			// Token: 0x0400AD90 RID: 44432
			Normal,
			// Token: 0x0400AD91 RID: 44433
			Replay,
			// Token: 0x0400AD92 RID: 44434
			Observer
		}

		// Token: 0x02001A08 RID: 6664
		// (Invoke) Token: 0x0600BAD4 RID: 47828
		public delegate void OnUseDeck(int deckIndex);

		// Token: 0x02001A09 RID: 6665
		// (Invoke) Token: 0x0600BAD8 RID: 47832
		public delegate void OnUseSkill(int skillIndex);
	}
}
