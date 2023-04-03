using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ClientPacket.Game;
using NKC;
using NKC.UI;
using NKC.UI.HUD;
using NKM.Templet;
using Spine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKM
{
	// Token: 0x0200036C RID: 876
	public class NKCUnitClient : NKMUnit
	{
		// Token: 0x0600152C RID: 5420 RVA: 0x0004FD31 File Offset: 0x0004DF31
		public Dictionary<short, NKCASEffect> GetBuffEffectDic()
		{
			return this.m_dicBuffEffect;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004FD39 File Offset: 0x0004DF39
		public NKCASUnitSpineSprite GetNKCASUnitSpineSprite()
		{
			return this.m_NKCASUnitSpineSprite;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0004FD41 File Offset: 0x0004DF41
		public NKCUnitViewer GetUnitViewer()
		{
			return this.m_NKCUnitViewer;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0004FD4C File Offset: 0x0004DF4C
		public NKCUnitClient()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCUnitClient;
			this.m_NKM_UNIT_CLASS_TYPE = NKM_UNIT_CLASS_TYPE.NCT_UNIT_CLIENT;
			this.InitUnitObject();
			this.InitSpriteObject();
			this.InitGage();
			this.InitShadow();
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0004FECC File Offset: 0x0004E0CC
		private void InitUnitObject()
		{
			this.m_UnitObject = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UNIT_GAME_NKM_UNIT", "NKM_GAME_UNIT", false, null);
			if (this.m_UnitObject == null)
			{
				return;
			}
			this.m_UnitObject.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT().transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_UnitObject.m_Instant, 0f, 0f, 0f);
			this.m_NKCUnitTouchObject = new NKCUnitTouchObject();
			this.m_NKCUnitTouchObject.Init();
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0004FF60 File Offset: 0x0004E160
		private void InitSpriteObject()
		{
			Transform transform = this.m_UnitObject.m_Instant.transform.Find("SPRITE");
			this.m_SpriteObject = transform.gameObject;
			transform = this.m_SpriteObject.transform.Find("MAIN_SPRITE");
			this.m_MainSpriteObject = transform.gameObject;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0004FFB8 File Offset: 0x0004E1B8
		private void InitGage()
		{
			Transform transform = this.m_UnitObject.m_Instant.transform.Find("UNIT_GAGE");
			this.m_UNIT_GAGE = transform.gameObject;
			this.m_UNIT_GAGE_RectTransform = this.m_UNIT_GAGE.GetComponent<RectTransform>();
			this.m_UNIT_SKILL = this.m_UNIT_GAGE.transform.Find("UNIT_SKILL").gameObject;
			this.m_SKILL_BUTTON = this.m_UNIT_GAGE.transform.Find("UNIT_SKILL/SKILL_BUTTON").gameObject;
			this.m_SKILL_BUTTON_Btn = this.m_SKILL_BUTTON.GetComponent<NKCUIComButton>();
			this.m_NKCUnitTouchObject.GetButton().PointerClick.RemoveAllListeners();
			this.m_NKCUnitTouchObject.GetButton().PointerClick.AddListener(new UnityAction(this.UseManualSkill));
			this.m_UNIT_GAGE_PANEL = this.m_UNIT_GAGE.transform.Find("UNIT_GAGE_PANEL").gameObject;
			this.m_UNIT_HP_GAGE = this.m_UNIT_GAGE.transform.Find("UNIT_GAGE_PANEL/UNIT_HP_GAGE").gameObject;
			Transform transform2 = this.m_UNIT_HP_GAGE.transform.Find("GAGE_BAR_SHADED");
			if (transform2 != null)
			{
				this.m_UnitHealthBar = transform2.gameObject.GetComponent<NKCUIComHealthBar>();
				if (this.m_UnitHealthBar == null)
				{
					this.m_UnitHealthBar = transform2.gameObject.AddComponent<NKCUIComHealthBar>();
				}
				NKCUtil.SetGameobjectActive(this.m_UnitHealthBar, true);
				this.m_UnitHealthBar.Init();
			}
			this.m_SKILL_GAUGE = this.m_UNIT_GAGE.transform.Find("UNIT_GAGE_PANEL").GetComponent<NKCUIComSkillGauge>();
			this.m_UNIT_LEVEL_BG = this.m_UNIT_GAGE.transform.Find("UNIT_LEVEL/UNIT_LEVEL_BG").gameObject;
			this.m_UNIT_LEVEL_BG_Image = this.m_UNIT_LEVEL_BG.GetComponent<Image>();
			this.m_UNIT_LEVEL = this.m_UNIT_GAGE.transform.Find("UNIT_LEVEL").gameObject;
			this.m_UNIT_LEVEL_RectTransform = this.m_UNIT_LEVEL.GetComponent<RectTransform>();
			this.m_UNIT_LEVEL_TEXT = this.m_UNIT_GAGE.transform.Find("UNIT_LEVEL/UNIT_LEVEL_TEXT").gameObject;
			this.m_UNIT_LEVEL_TEXT_Text = this.m_UNIT_LEVEL_TEXT.GetComponent<NKCUIComTextUnitLevel>();
			this.m_UNIT_ARMOR_TYPE = this.m_UNIT_GAGE.transform.Find("UNIT_LEVEL/UNIT_ARMOR_TYPE").gameObject;
			this.m_UNIT_ARMOR_TYPE_Image = this.m_UNIT_ARMOR_TYPE.GetComponent<Image>();
			this.m_UNIT_ASSIST = this.m_UNIT_GAGE.transform.Find("UNIT_LEVEL/UNIT_ASSIST").gameObject;
			this.m_fOrgGageSize = this.m_UNIT_GAGE_RectTransform.sizeDelta.x;
			this.m_listNKCUnitBuffIcon.Clear();
			for (int i = 0; i < 6; i++)
			{
				NKCUnitBuffIcon nkcunitBuffIcon = new NKCUnitBuffIcon();
				nkcunitBuffIcon.InitObject(this.m_UNIT_GAGE, i);
				this.m_listNKCUnitBuffIcon.Add(nkcunitBuffIcon);
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0005027C File Offset: 0x0004E47C
		private void InitShadow()
		{
			this.m_NKCASUnitShadow = (NKCASUnitShadow)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitShadow, "", "", false);
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_SHADOW().transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant, 0f, 0f, 0f);
			if (this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(false);
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00050363 File Offset: 0x0004E563
		public override void Open()
		{
			base.Open();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005036C File Offset: 0x0004E56C
		public override void Close()
		{
			if (this.m_SpriteObject != null)
			{
				Vector3 localScale = this.m_SpriteObject.transform.localScale;
				localScale.x = 1f;
				localScale.y = 1f;
				this.m_SpriteObject.transform.localScale = localScale;
			}
			if (this.m_UnitObject.m_Instant.activeSelf)
			{
				this.m_UnitObject.m_Instant.SetActive(false);
			}
			this.m_NKCUnitTouchObject.Close();
			if (this.m_NKCAnimSpine != null)
			{
				this.m_NKCAnimSpine.ResetParticleSimulSpeedOrg();
			}
			this.m_NKCAnimSpine.Init();
			this.m_NKCMotionAfterImage.Clear();
			this.ActiveObject(false);
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUnitSprite);
			this.m_NKCASUnitSprite = null;
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUnitSpineSprite);
			this.m_NKCASUnitSpineSprite = null;
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUnitMiniMapFace);
			this.m_NKCASUnitMiniMapFace = null;
			if (this.m_NKCUnitViewer == null)
			{
				this.m_NKCUnitViewer = new NKCUnitViewer();
			}
			this.m_NKCUnitViewer.Init();
			this.m_UNIT_ARMOR_TYPE_Image.sprite = null;
			foreach (NKCASEffect nkcaseffect in this.m_llEffect)
			{
				nkcaseffect.Stop(false);
				nkcaseffect.m_bAutoDie = true;
			}
			this.m_llEffect.Clear();
			foreach (KeyValuePair<short, NKCASEffect> keyValuePair in this.m_dicBuffEffect)
			{
				NKCASEffect value = keyValuePair.Value;
				value.Stop(false);
				value.m_bAutoDie = true;
			}
			this.m_dicBuffEffect.Clear();
			foreach (NKCASEffect nkcaseffect2 in this.m_llBuffTextEffect)
			{
				nkcaseffect2.Stop(false);
				nkcaseffect2.m_bAutoDie = true;
			}
			this.m_llBuffTextEffect.Clear();
			foreach (KeyValuePair<short, NKCASEffect> keyValuePair2 in this.m_dicBuffEffectRange)
			{
				NKCASEffect value2 = keyValuePair2.Value;
				value2.Stop(false);
				value2.m_bAutoDie = true;
			}
			this.m_dicBuffEffectRange.Clear();
			foreach (KeyValuePair<NKM_UNIT_STATUS_EFFECT, NKCASEffect> keyValuePair3 in this.m_dicStatusEffect)
			{
				NKCASEffect value3 = keyValuePair3.Value;
				value3.Stop(false);
				value3.m_bAutoDie = true;
			}
			this.m_dicStatusEffect.Clear();
			this.m_listBuffDelete.Clear();
			this.m_listBuffDieEvent.Clear();
			this.m_listNKMStaticBuffDataRuntime.Clear();
			this.m_listDamageResistUnit.Clear();
			if (this.m_NKCASDangerChargeUI != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASDangerChargeUI);
				this.m_NKCASDangerChargeUI = null;
			}
			base.Close();
			this.m_bLoadComplete = false;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000506B4 File Offset: 0x0004E8B4
		public override void Unload()
		{
			this.m_NKCASUnitShadow.Unload();
			this.m_NKCASUnitShadow = null;
			if (this.m_NKCUnitViewer != null)
			{
				this.m_NKCUnitViewer.Unload();
				this.m_NKCUnitViewer = null;
			}
			if (this.m_NKCASDangerChargeUI != null)
			{
				this.m_NKCASDangerChargeUI.Unload();
				this.m_NKCASDangerChargeUI = null;
			}
			this.m_NKCUnitTouchObject.Unload();
			this.m_NKCUnitTouchObject = null;
			NKCAssetResourceManager.CloseInstance(this.m_UnitObject);
			this.m_UnitObject = null;
			this.m_NKCGameClient = null;
			this.m_SpriteObject = null;
			this.m_MainSpriteObject = null;
			this.m_UNIT_GAGE = null;
			this.m_UNIT_GAGE_RectTransform = null;
			this.m_UNIT_GAGE_PANEL = null;
			this.m_UNIT_HP_GAGE = null;
			this.m_UnitHealthBar = null;
			this.m_SKILL_GAUGE = null;
			this.m_UNIT_LEVEL_BG = null;
			this.m_UNIT_LEVEL_BG_Image = null;
			this.m_UNIT_LEVEL = null;
			this.m_UNIT_LEVEL_RectTransform = null;
			this.m_UNIT_LEVEL_TEXT = null;
			this.m_UNIT_LEVEL_TEXT_Text = null;
			this.m_UNIT_ARMOR_TYPE = null;
			this.m_UNIT_ARMOR_TYPE_Image = null;
			this.m_UNIT_ASSIST = null;
			for (int i = 0; i < this.m_listNKCUnitBuffIcon.Count; i++)
			{
				this.m_listNKCUnitBuffIcon[i].Unload();
			}
			this.m_listNKCUnitBuffIcon.Clear();
			this.m_NKCAnimSpine.Init();
			this.m_NKCAnimSpine = null;
			this.m_llEffect.Clear();
			this.m_dicLoadEffectTemp.Clear();
			this.m_dicLoadSoundTemp.Clear();
			this.m_dicBuffEffectRange.Clear();
			this.m_dicBuffEffect.Clear();
			this.m_llBuffTextEffect.Clear();
			this.m_dicStatusEffect.Clear();
			this.m_NKCASUnitSprite = null;
			this.m_NKCASUnitSpineSprite = null;
			this.m_NKCASUnitShadow = null;
			this.m_NKCASUnitMiniMapFace = null;
			this.m_NKCASDangerChargeUI = null;
			this.m_NKCMotionAfterImage.Clear();
			this.m_NKCMotionAfterImage = null;
			base.Unload();
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00050875 File Offset: 0x0004EA75
		public void ObjectParentWait()
		{
			if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_bIsLoaded)
			{
				this.m_NKCASUnitMiniMapFace.ObjectParentWait();
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00050897 File Offset: 0x0004EA97
		public void ObjectParentRestore()
		{
			if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_bIsLoaded)
			{
				this.m_NKCASUnitMiniMapFace.ObjectParentRestore();
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000508BC File Offset: 0x0004EABC
		public override bool LoadUnit(NKMGame cNKMGame, NKMUnitData cNKMUnitData, short masterGameUnitUID, short gameUnitUID, float fNearTargetRange, NKM_TEAM_TYPE eNKM_TEAM_TYPE, bool bSub, bool bAsync)
		{
			if (!base.LoadUnit(cNKMGame, cNKMUnitData, masterGameUnitUID, gameUnitUID, fNearTargetRange, eNKM_TEAM_TYPE, bSub, bAsync))
			{
				return false;
			}
			this.m_NKCGameClient = (NKCGameClient)cNKMGame;
			this.LoadResInst(bAsync);
			this.m_NKCASUnitSpineSprite = NKCUnitClient.OpenUnitSpineSprite(cNKMUnitData, bSub, bAsync);
			this.m_NKCASUnitMiniMapFace = this.LoadUnitMiniMapFace(this.m_UnitTemplet.m_UnitTempletBase, bSub, bAsync);
			this.m_SKILL_GAUGE.SetSkillType(this.m_UnitTemplet.m_UnitTempletBase.StopDefaultCoolTime);
			if (this.m_NKCUnitViewer == null)
			{
				this.m_NKCUnitViewer = new NKCUnitViewer();
			}
			if (!cNKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, eNKM_TEAM_TYPE) && !cNKMGame.IsBoss(base.GetUnitDataGame().m_GameUnitUID))
			{
				this.m_NKCUnitViewer.LoadUnit(cNKMUnitData, bSub, NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_VIEWER(), bAsync);
			}
			else
			{
				this.m_NKCUnitViewer.Unload();
				this.m_NKCUnitViewer = null;
			}
			foreach (KeyValuePair<string, NKMUnitState> keyValuePair in this.m_UnitTemplet.m_dicNKMUnitState)
			{
				if (keyValuePair.Value.m_DangerCharge.m_fChargeTime > 0f)
				{
					this.m_NKCASDangerChargeUI = (NKCASDangerChargeUI)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASDangerChargeUI, "", "", bAsync);
					break;
				}
			}
			for (int i = 0; i < base.GetUnitTemplet().m_UnitTempletBase.GetSkillCount(); i++)
			{
				NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(base.GetUnitTemplet().m_UnitTempletBase.GetSkillStrID(i), base.GetUnitData());
				if (unitSkillTemplet != null && unitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER)
				{
					this.m_NKCUnitTouchObject.SetLinkButton(this.m_SKILL_BUTTON_Btn);
					break;
				}
			}
			return true;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00050A8C File Offset: 0x0004EC8C
		private NKCASUnitMiniMapFace LoadUnitMiniMapFace(NKMUnitTempletBase cTempletBase, bool bSub, bool bAsync)
		{
			NKCASUnitMiniMapFace result;
			if (bSub && cTempletBase.m_MiniMapFaceNameSub.Length > 1)
			{
				result = (NKCASUnitMiniMapFace)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitMiniMapFace, "AB_UNIT_MINI_MAP_FACE", cTempletBase.m_MiniMapFaceNameSub, bAsync);
			}
			else
			{
				result = (NKCASUnitMiniMapFace)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitMiniMapFace, "AB_UNIT_MINI_MAP_FACE", cTempletBase.m_MiniMapFaceName, bAsync);
			}
			return result;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		public override void LoadUnitComplete()
		{
			if (this.m_bLoadComplete)
			{
				return;
			}
			this.LoadUnitCompleteRes();
			this.LoadUnitCompleteUnitSprite();
			this.LoadUnitCompleteUnitMiniMapFace();
			this.LoadUnitCompleteGage();
			this.LoadUnitCompleteColor(this.m_UnitTemplet.m_ColorR, this.m_UnitTemplet.m_ColorG, this.m_UnitTemplet.m_ColorB);
			this.LoadUnitCompleteShadow();
			this.LoadUnitCompleteUnitViewer();
			NKCUtil.SetGameObjectLocalPos(this.m_SpriteObject, this.m_UnitTemplet.m_SpriteOffsetX, this.m_UnitTemplet.m_SpriteOffsetY, 0f);
			this.m_Vector3Temp.Set(this.m_UnitTemplet.m_SpriteScale, this.m_UnitTemplet.m_SpriteScale, this.m_SpriteObject.transform.localScale.z);
			this.m_SpriteObject.transform.localScale = this.m_Vector3Temp;
			this.m_Vector3Temp.Set(this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_PosZ + this.m_UnitDataGame.m_RespawnJumpYPos, this.m_UnitSyncData.m_PosZ);
			this.m_UnitObject.m_Instant.transform.localPosition = this.m_Vector3Temp;
			this.m_NKCAnimSpine.SetAnimObj(this.m_MainSpriteObject, null, true);
			if (this.m_UnitTemplet.m_bUseMotionBlur)
			{
				this.m_NKCMotionAfterImage.Init(10, this.m_NKCASUnitSpineSprite.m_MeshRenderer);
			}
			this.m_ObjPosX.SetNowValue(this.m_UnitSyncData.m_PosX);
			this.m_ObjPosZ.SetNowValue(this.m_UnitSyncData.m_PosZ + this.m_UnitDataGame.m_RespawnJumpYPos);
			this.m_Vector3Temp = this.m_SpriteObject.transform.localEulerAngles;
			this.m_Vector3Temp.y = 0f;
			this.m_SpriteObject.transform.localEulerAngles = this.m_Vector3Temp;
			if (this.m_NKCUnitTouchObject != null)
			{
				this.m_NKCUnitTouchObject.SetSize(base.GetUnitTemplet());
			}
			this.ActiveObject(false);
			if (this.m_UnitHealthBar != null)
			{
				NKCGameClient nkcgameClient = this.m_NKCGameClient;
				bool flag;
				if (nkcgameClient == null)
				{
					flag = (null != null);
				}
				else
				{
					NKMGameData gameData = nkcgameClient.GetGameData();
					flag = (((gameData != null) ? gameData.GameStatRate : null) != null);
				}
				float num = flag ? this.m_NKCGameClient.GetGameData().GameStatRate.GetStatValueRate(NKM_STAT_TYPE.NST_HP) : 1f;
				this.m_UnitHealthBar.SetStepRatio((num > 0.25f) ? 1f : 0.1f);
			}
			this.m_bLoadComplete = true;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00050D58 File Offset: 0x0004EF58
		public override void RespawnUnit(float fPosX, float fPosZ, float fJumpYPos, bool bUseRight = false, bool bRight = true, float fInitHP = 0f, bool bInitHPRate = false, float rollbackTime = 0f)
		{
			base.RespawnUnit(fPosX, fPosZ, fJumpYPos, bUseRight, bRight, fInitHP, bInitHPRate, 0f);
			this.m_ObjPosX.SetNowValue(this.m_UnitSyncData.m_PosX);
			this.m_ObjPosZ.SetNowValue(this.m_UnitSyncData.m_PosZ + this.m_UnitDataGame.m_RespawnJumpYPos);
			this.m_DissolveFactor.SetNowValue(0f);
			this.m_bDissolveEnable = false;
			this.m_NKCASUnitSpineSprite.SetDissolveBlend(0f);
			this.m_NKCASUnitSpineSprite.SetDissolveOn(false);
			this.m_NKCASUnitSpineSprite.SetColor(1f, 1f, 1f, 1f, true);
			this.ActiveObject(false);
			if (this.m_NKCUnitViewer != null)
			{
				this.m_NKCUnitViewer.SetActiveSprite(false);
				this.m_NKCUnitViewer.SetActiveShadow(false);
				this.m_NKCUnitViewer.StopTimer();
			}
			float zscaleFactor = this.m_NKCGameClient.GetZScaleFactor(this.m_ObjPosZ.GetNowValue());
			this.InitGagePos(zscaleFactor);
			this.GageInit();
			if (this.m_NKCASUnitShadow != null && this.m_NKCASUnitShadow.m_NKCComGroupColor != null)
			{
				this.m_NKCASUnitShadow.m_NKCComGroupColor.SetColor(-1f, -1f, -1f, 0f, 0f);
				this.m_NKCASUnitShadow.m_NKCComGroupColor.SetColor(-1f, -1f, -1f, 0.9f, 2f);
			}
			this.m_NKCASUnitSpineSprite.SetOverrideMaterial();
			this.m_fManualSkillUseAck = 0f;
			this.m_bManualSkillUseStart = false;
			this.m_bManualSkillUseStateID = 0;
			this.m_BuffUnitLevelLastUpdate = 0;
			this.m_BuffDescTextPosYIndex = 0;
			this.SetShowUI();
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00050F04 File Offset: 0x0004F104
		private void LoadUnitCompleteUnitSprite()
		{
			this.m_NKCASUnitSpineSprite.m_UnitSpineSpriteInstant.m_Instant.transform.SetParent(this.m_MainSpriteObject.transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_NKCASUnitSpineSprite.m_UnitSpineSpriteInstant.m_Instant, 0f, 0f, 0f);
			NKCScenManager.GetScenManager().ForceRender(this.m_NKCASUnitSpineSprite.m_MeshRenderer);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00050F70 File Offset: 0x0004F170
		private void LoadUnitCompleteUnitMiniMapFace()
		{
			if (this.m_NKCASUnitMiniMapFace == null || this.m_NKCASUnitMiniMapFace.m_MarkerGreen == null || this.m_NKCASUnitMiniMapFace.m_MarkerRed == null)
			{
				Debug.LogError("LoadUnitCompleteUnitMiniMapFace null" + base.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID);
				return;
			}
			if (!this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG))
			{
				this.m_NKCASUnitMiniMapFace.m_MarkerGreen.SetActive(true);
				this.m_NKCASUnitMiniMapFace.m_MarkerRed.SetActive(false);
				return;
			}
			this.m_NKCASUnitMiniMapFace.m_MarkerGreen.SetActive(false);
			this.m_NKCASUnitMiniMapFace.m_MarkerRed.SetActive(true);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00051034 File Offset: 0x0004F234
		private void LoadUnitCompleteGage()
		{
			this.SetShowUI();
			if (this.m_UnitTemplet.m_listSkillStateData.Count > 0 && this.m_UnitTemplet.m_listSkillStateData[0] != null && base.IsStateUnlocked(this.m_UnitTemplet.m_listSkillStateData[0]))
			{
				this.m_SKILL_GAUGE.SetActiveSkillGauge(true);
			}
			else
			{
				this.m_SKILL_GAUGE.SetActiveSkillGauge(false);
			}
			if (this.m_UnitTemplet.m_listHyperSkillStateData.Count > 0 && this.m_UnitTemplet.m_listHyperSkillStateData[0] != null && base.IsStateUnlocked(this.m_UnitTemplet.m_listHyperSkillStateData[0]))
			{
				this.m_SKILL_GAUGE.SetActiveHyperGauge(true);
			}
			else
			{
				this.m_SKILL_GAUGE.SetActiveHyperGauge(false);
			}
			this.ChangeLevelGageBG();
			if (this.m_UnitHealthBar != null)
			{
				this.m_UnitHealthBar.SetColor(this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG));
			}
			this.m_UNIT_LEVEL_TEXT_Text.SetLevel(this.m_UnitData, base.GetUnitFrameData().m_BuffUnitLevel, Array.Empty<Text>());
			float zscaleFactor = this.m_NKCGameClient.GetZScaleFactor(this.m_ObjPosZ.GetNowValue());
			this.InitGagePos(zscaleFactor);
			this.GageInit();
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00051180 File Offset: 0x0004F380
		private void GageInit()
		{
			this.m_GageWide.SetNowValue(0f);
			Vector2 vector = this.m_UNIT_GAGE_RectTransform.sizeDelta;
			vector.x = 0f;
			this.m_UNIT_GAGE_RectTransform.sizeDelta = vector;
			vector = this.m_UNIT_LEVEL_RectTransform.anchoredPosition;
			vector.x = this.m_UNIT_LEVEL_RectTransform.sizeDelta.x * 0.5f;
			this.m_UNIT_LEVEL_RectTransform.anchoredPosition = vector;
			this.m_UNIT_GAGE_PANEL.SetActive(false);
			this.m_SKILL_GAUGE.SetSkillCoolTime(0f);
			this.m_SKILL_GAUGE.SetHyperCoolTime(0f);
			for (int i = 0; i < this.m_listNKCUnitBuffIcon.Count; i++)
			{
				this.m_listNKCUnitBuffIcon[i].Init();
			}
			this.m_UNIT_ARMOR_TYPE_Image.sprite = NKCResourceUtility.GetOrLoadUnitRoleIconInGame(base.GetUnitTemplet().m_UnitTempletBase);
			if (!this.m_NKCGameClient.GetGameData().GetTeamData(base.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG).IsAssistUnit(base.GetUnitDataGame().m_UnitUID))
			{
				if (this.m_UNIT_ASSIST.activeSelf)
				{
					this.m_UNIT_ASSIST.SetActive(false);
					return;
				}
			}
			else if (!this.m_UNIT_ASSIST.activeSelf)
			{
				this.m_UNIT_ASSIST.SetActive(true);
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000512C4 File Offset: 0x0004F4C4
		private void GageSetBuffIconActive(int index, bool bActive, NKMBuffData cNKMBuffData = null, float fLifeTimeRate = 1f)
		{
			if (index >= this.m_listNKCUnitBuffIcon.Count)
			{
				return;
			}
			NKCUnitBuffIcon nkcunitBuffIcon = this.m_listNKCUnitBuffIcon[index];
			if (nkcunitBuffIcon == null)
			{
				return;
			}
			nkcunitBuffIcon.GageSetBuffIconActive(bActive, cNKMBuffData, fLifeTimeRate);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000512FB File Offset: 0x0004F4FB
		private void InitGagePos(float fZScaleFactor)
		{
			if (this.m_NKCASUnitSpineSprite.m_Bone_Move != null)
			{
				this.InitGagePosSpine(fZScaleFactor);
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00051314 File Offset: 0x0004F514
		private void InitGagePosSpine(float fZScaleFactor)
		{
			this.m_GageOffsetPosX.Init();
			this.m_GageOffsetPosY.Init();
			if (this.m_UnitSyncData.m_bRight)
			{
				this.m_fGagePosX = this.m_UnitTemplet.m_fGageOffsetX + this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldX * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.x;
				this.m_fGagePosY = this.m_UnitTemplet.m_fGageOffsetY + this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldY * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.y;
				this.m_fGagePosY *= fZScaleFactor;
			}
			else
			{
				this.m_fGagePosX = -this.m_UnitTemplet.m_fGageOffsetX - this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldX * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.x;
				this.m_fGagePosY = this.m_UnitTemplet.m_fGageOffsetY - this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldY * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.y;
				this.m_fGagePosY *= fZScaleFactor;
			}
			Vector3 anchoredPosition3D = this.m_UNIT_GAGE_RectTransform.anchoredPosition3D;
			anchoredPosition3D.Set(this.m_fGagePosX, this.m_fGagePosY, 0f);
			this.m_UNIT_GAGE_RectTransform.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00051490 File Offset: 0x0004F690
		private void UpdateGagePos(float fZScaleFactor)
		{
			if (this.m_NKCASUnitSpineSprite.m_Bone_Move == null)
			{
				return;
			}
			this.m_GageOffsetPosX.Update(this.m_DeltaTime);
			this.m_GageOffsetPosY.Update(this.m_DeltaTime);
			if (this.m_UnitSyncData.m_bRight)
			{
				this.m_fGagePosX = this.m_UnitTemplet.m_fGageOffsetX + this.m_GageOffsetPosX.GetNowValue() + this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldX * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.x;
				this.m_fGagePosY = this.m_UnitTemplet.m_fGageOffsetY + this.m_GageOffsetPosY.GetNowValue() + this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldY * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.y;
				this.m_fGagePosY *= fZScaleFactor;
			}
			else
			{
				this.m_fGagePosX = -this.m_UnitTemplet.m_fGageOffsetX - this.m_GageOffsetPosX.GetNowValue() - this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldX * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.x;
				this.m_fGagePosY = this.m_UnitTemplet.m_fGageOffsetY - this.m_GageOffsetPosY.GetNowValue() - this.m_NKCASUnitSpineSprite.m_Bone_Move.WorldY * this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.localScale.y;
				this.m_fGagePosY *= fZScaleFactor;
			}
			Vector3 anchoredPosition3D = this.m_UNIT_GAGE_RectTransform.anchoredPosition3D;
			anchoredPosition3D.Set(this.m_fGagePosX, this.m_fGagePosY, 0f);
			this.m_UNIT_GAGE_RectTransform.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00051654 File Offset: 0x0004F854
		private void ChangeLevelGageBG()
		{
			if (!this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG))
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_GAME_NKM_UNIT_SPRITE", "AB_UNIT_GAME_NKM_UNIT_LEVEL_BG", false);
				if (orLoadAssetResource != null)
				{
					this.m_UNIT_LEVEL_BG_Image.sprite = orLoadAssetResource;
					return;
				}
			}
			else
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_GAME_NKM_UNIT_SPRITE", "AB_UNIT_GAME_NKM_UNIT_LEVEL_BG_ENEMY", false);
				if (orLoadAssetResource != null)
				{
					this.m_UNIT_LEVEL_BG_Image.sprite = orLoadAssetResource;
				}
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000516D4 File Offset: 0x0004F8D4
		private void LoadUnitCompleteColor(float fR, float fG, float fB)
		{
			this.m_UnitFrameData.m_ColorR.SetNowValue(fR);
			this.m_UnitFrameData.m_ColorG.SetNowValue(fG);
			this.m_UnitFrameData.m_ColorB.SetNowValue(fB);
			this.m_NKCASUnitSpineSprite.SetColor(this.m_UnitFrameData.m_ColorR.GetNowValue(), this.m_UnitFrameData.m_ColorG.GetNowValue(), this.m_UnitFrameData.m_ColorB.GetNowValue(), -1f, false);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00051758 File Offset: 0x0004F958
		private void LoadUnitCompleteShadow()
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			if (!this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(true);
			}
			this.m_Vector3Temp.Set(this.m_UnitTemplet.m_fShadowScaleX, this.m_UnitTemplet.m_fShadowScaleY, 1f);
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localScale = this.m_Vector3Temp;
			this.m_Vector3Temp.Set(this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_PosZ, this.m_UnitSyncData.m_PosZ);
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localPosition = this.m_Vector3Temp;
			bool bTeamA = !this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, base.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG);
			this.m_NKCASUnitShadow.SetShadowType(this.m_UnitTemplet.m_NKC_TEAM_COLOR_TYPE, bTeamA, this.m_UnitTemplet.m_UnitTempletBase.IsRearmUnit);
			this.m_NKCASUnitShadow.m_NKCComGroupColor.SetColor(-1f, -1f, -1f, 0f, 0f);
			this.m_NKCASUnitShadow.m_NKCComGroupColor.SetColor(-1f, -1f, -1f, 0.9f, 2f);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000518F8 File Offset: 0x0004FAF8
		private void LoadUnitCompleteUnitViewer()
		{
			bool shadowType = !this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, base.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG);
			if (this.m_NKCUnitViewer != null)
			{
				try
				{
					this.m_NKCUnitViewer.LoadUnitComplete();
					this.m_NKCUnitViewer.SetShadowType(shadowType);
					this.m_NKCUnitViewer.SetActiveSprite(false);
					this.m_NKCUnitViewer.SetActiveShadow(false);
					this.m_NKCUnitViewer.StopTimer();
					this.m_NKCUnitViewer.SetUnitUID(base.GetUnitData().m_UnitUID);
				}
				catch (Exception ex)
				{
					if (base.GetUnitTemplet() != null && base.GetUnitTemplet().m_UnitTempletBase != null)
					{
						Debug.LogError(base.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID + ": LoadUnitCompleteUnitViewer Failed with exception : " + ex.Message);
					}
					else
					{
						Debug.LogError("Unknown unit : LoadUnitCompleteUnitViewer Failed with exception : " + ex.Message);
					}
				}
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000519F4 File Offset: 0x0004FBF4
		private void LoadResInst(bool bAsync)
		{
			foreach (KeyValuePair<string, NKMUnitState> keyValuePair in this.m_UnitTemplet.m_dicNKMUnitState)
			{
				NKMUnitState value = keyValuePair.Value;
				if (value != null)
				{
					for (int i = 0; i < value.m_listNKMEventSound.Count; i++)
					{
						NKMEventSound nkmeventSound = value.m_listNKMEventSound[i];
						if (nkmeventSound != null && (this.m_UnitData == null || nkmeventSound.IsRightSkin(this.m_UnitData.m_SkinID)))
						{
							List<string> targetSoundList = nkmeventSound.GetTargetSoundList(this.m_UnitData);
							for (int j = 0; j < targetSoundList.Count; j++)
							{
								this.LoadSound(targetSoundList[j], bAsync);
							}
						}
					}
					for (int k = 0; k < value.m_listNKMEventAttack.Count; k++)
					{
						NKMEventAttack nkmeventAttack = value.m_listNKMEventAttack[k];
						if (nkmeventAttack != null)
						{
							this.LoadAttackResInst(nkmeventAttack, bAsync);
						}
					}
					for (int l = 0; l < value.m_listNKMEventEffect.Count; l++)
					{
						NKMEventEffect nkmeventEffect = value.m_listNKMEventEffect[l];
						if (nkmeventEffect != null)
						{
							this.LoadEffectInst(nkmeventEffect, bAsync);
						}
					}
					NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
					if (gameOptionData == null || gameOptionData.ViewSkillCutIn)
					{
						this.m_bHyperCutinLoaded = true;
						for (int m = 0; m < value.m_listNKMEventHyperSkillCutIn.Count; m++)
						{
							NKMEventHyperSkillCutIn nkmeventHyperSkillCutIn = value.m_listNKMEventHyperSkillCutIn[m];
							if (nkmeventHyperSkillCutIn != null)
							{
								string skillCutin = NKMSkinManager.GetSkillCutin(base.GetUnitData(), nkmeventHyperSkillCutIn.m_CutInEffectName);
								this.LoadEffectInst(skillCutin, bAsync);
							}
						}
					}
					else
					{
						this.m_bHyperCutinLoaded = false;
					}
					for (int n = 0; n < value.m_listNKMEventDamageEffect.Count; n++)
					{
						NKMEventDamageEffect nkmeventDamageEffect = value.m_listNKMEventDamageEffect[n];
						if (nkmeventDamageEffect != null)
						{
							this.LoadDEEffectInst(nkmeventDamageEffect, bAsync);
						}
					}
					for (int num = 0; num < value.m_listNKMEventBuff.Count; num++)
					{
						NKMEventBuff nkmeventBuff = value.m_listNKMEventBuff[num];
						if (nkmeventBuff != null)
						{
							NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(nkmeventBuff.m_BuffStrID);
							if (buffTempletByStrID == null)
							{
								Debug.LogError("Bufftemplet " + nkmeventBuff.m_BuffStrID + " does not exist!!");
							}
							this.LoadBuff(buffTempletByStrID, bAsync);
						}
					}
				}
			}
			this.LoadSound("FX_UI_DUNGEON_RESPONE", bAsync);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00051C38 File Offset: 0x0004FE38
		private void LoadBuff(NKMBuffTemplet cNKMBuffTemplet, bool bAsync)
		{
			if (cNKMBuffTemplet == null)
			{
				return;
			}
			this.LoadEffectInst(cNKMBuffTemplet.m_RangeEffectName, bAsync);
			this.LoadEffectInst(cNKMBuffTemplet.GetMasterEffectName(this.m_UnitData.m_SkinID), bAsync);
			this.LoadEffectInst(cNKMBuffTemplet.GetSlaveEffectName(this.m_UnitData.m_SkinID), bAsync);
			this.LoadEffectInst(cNKMBuffTemplet.m_BarrierDamageEffectName, bAsync);
			this.LoadDamageTemplet(cNKMBuffTemplet.m_DTStart, bAsync);
			this.LoadDamageTemplet(cNKMBuffTemplet.m_DTEnd, bAsync);
			this.LoadDamageTemplet(cNKMBuffTemplet.m_DTDispel, bAsync);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00051CBA File Offset: 0x0004FEBA
		private void LoadDamageTemplet(NKMDamageTemplet cNKMDamageTemplet, bool bAsync)
		{
			if (cNKMDamageTemplet != null)
			{
				if (cNKMDamageTemplet.m_HitSoundName != null)
				{
					this.LoadSound(cNKMDamageTemplet.m_HitSoundName, bAsync);
				}
				this.LoadEffectInst(cNKMDamageTemplet.m_HitEffect, bAsync);
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00051CE1 File Offset: 0x0004FEE1
		private void LoadSound(string assetName, bool bAsync)
		{
			if (assetName.Length <= 1)
			{
				return;
			}
			if (this.m_dicLoadSoundTemp.ContainsKey(assetName))
			{
				return;
			}
			this.m_dicLoadSoundTemp.Add(assetName, NKCAssetResourceManager.OpenResource<AudioClip>(assetName, bAsync));
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00051D10 File Offset: 0x0004FF10
		private void LoadAttackResInst(NKMEventAttack cNKMEventAttack, bool bAsync)
		{
			if (cNKMEventAttack.m_SoundName != null)
			{
				this.LoadSound(cNKMEventAttack.m_SoundName, bAsync);
			}
			this.LoadEffectInst(cNKMEventAttack.m_EffectName, bAsync);
			NKMDamageTemplet templetByStrID = NKMDamageManager.GetTempletByStrID(cNKMEventAttack.m_DamageTempletName);
			if (templetByStrID != null)
			{
				if (templetByStrID.m_HitSoundName != null)
				{
					this.LoadSound(templetByStrID.m_HitSoundName, bAsync);
				}
				this.LoadEffectInst(templetByStrID.m_HitEffect, bAsync);
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00051D70 File Offset: 0x0004FF70
		private void LoadEffectInst(string effectName, bool bAsync)
		{
			if (effectName.Length <= 1)
			{
				return;
			}
			if (this.m_dicLoadEffectTemp.ContainsKey(effectName))
			{
				return;
			}
			NKCASEffect value = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, effectName, effectName, bAsync);
			this.m_dicLoadEffectTemp.Add(effectName, value);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00051DBD File Offset: 0x0004FFBD
		private void LoadEffectInst(NKMEventEffect cNKMEventEffect, bool bAsync)
		{
			if (cNKMEventEffect != null && cNKMEventEffect.IsRightSkin(this.m_UnitData.m_SkinID))
			{
				this.LoadEffectInst(cNKMEventEffect.GetEffectName(this.m_UnitData), bAsync);
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00051DE8 File Offset: 0x0004FFE8
		private void LoadDEEffectInst(NKMEventDamageEffect cNKMEventDamageEffect, bool bAsync)
		{
			this.LoadDEEffectInst(cNKMEventDamageEffect.m_DEName, bAsync);
			if (cNKMEventDamageEffect.m_DENamePVP.Length > 1)
			{
				this.LoadDEEffectInst(cNKMEventDamageEffect.m_DENamePVP, bAsync);
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00051E14 File Offset: 0x00050014
		private void LoadDEEffectInst(string DEName, bool bAsync)
		{
			NKMDamageEffectTemplet detemplet = NKMDETempletManager.GetDETemplet(DEName);
			if (detemplet != null)
			{
				this.LoadEffectInst(detemplet.GetMainEffectName(this.m_UnitData.m_SkinID), bAsync);
				foreach (KeyValuePair<string, NKMDamageEffectState> keyValuePair in detemplet.m_dicNKMState)
				{
					NKMDamageEffectState value = keyValuePair.Value;
					this.LoadDEStateEffectInst(value, bAsync);
				}
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00051E74 File Offset: 0x00050074
		private bool LoadDEStateEffectInst(NKMDamageEffectState cNKMDamageEffectState, bool bAsync)
		{
			for (int i = 0; i < cNKMDamageEffectState.m_listNKMEventSound.Count; i++)
			{
				NKMEventSound nkmeventSound = cNKMDamageEffectState.m_listNKMEventSound[i];
				if (nkmeventSound != null && nkmeventSound.IsRightSkin(this.m_UnitData.m_SkinID))
				{
					List<string> targetSoundList = nkmeventSound.GetTargetSoundList(this.m_UnitData);
					for (int j = 0; j < targetSoundList.Count; j++)
					{
						this.LoadSound(targetSoundList[j], bAsync);
					}
				}
			}
			for (int k = 0; k < cNKMDamageEffectState.m_listNKMEventAttack.Count; k++)
			{
				NKMEventAttack cNKMEventAttack = cNKMDamageEffectState.m_listNKMEventAttack[k];
				this.LoadAttackResInst(cNKMEventAttack, bAsync);
			}
			for (int l = 0; l < cNKMDamageEffectState.m_listNKMEventEffect.Count; l++)
			{
				NKMEventEffect cNKMEventEffect = cNKMDamageEffectState.m_listNKMEventEffect[l];
				this.LoadEffectInst(cNKMEventEffect, bAsync);
			}
			for (int m = 0; m < cNKMDamageEffectState.m_listNKMEventBuff.Count; m++)
			{
				NKMEventBuff nkmeventBuff = cNKMDamageEffectState.m_listNKMEventBuff[m];
				if (nkmeventBuff != null)
				{
					NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(nkmeventBuff.m_BuffStrID);
					if (buffTempletByStrID == null)
					{
						Debug.LogError("Bufftemplet " + nkmeventBuff.m_BuffStrID + " does not exist!!");
					}
					this.LoadBuff(buffTempletByStrID, bAsync);
				}
			}
			return true;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00051FB0 File Offset: 0x000501B0
		public void LoadUnitCompleteRes()
		{
			foreach (KeyValuePair<string, NKCASEffect> keyValuePair in this.m_dicLoadEffectTemp)
			{
				NKCASEffect value = keyValuePair.Value;
				if (value != null)
				{
					NKCScenManager.GetScenManager().GetObjectPool().CloseObj(value);
				}
			}
			this.m_dicLoadEffectTemp.Clear();
			foreach (KeyValuePair<string, NKCAssetResourceData> keyValuePair2 in this.m_dicLoadSoundTemp)
			{
				NKCAssetResourceData value2 = keyValuePair2.Value;
				if (value2 != null)
				{
					NKCAssetResourceManager.CloseResource(value2);
				}
			}
			this.m_dicLoadSoundTemp.Clear();
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00052080 File Offset: 0x00050280
		public override void SetDying(bool bForce, bool bUnitChange = false)
		{
			base.SetDying(bForce, bUnitChange);
			if (this.m_UNIT_GAGE.activeSelf)
			{
				this.m_UNIT_GAGE.SetActive(false);
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000520A3 File Offset: 0x000502A3
		public override bool SetDie(bool bCheckAllDie = true)
		{
			bool result = base.SetDie(bCheckAllDie);
			if (this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_DEV || this.m_UnitTemplet.m_bDieDeActive)
			{
				this.ActiveObject(false);
			}
			return result;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000520D4 File Offset: 0x000502D4
		public void SetShowUI()
		{
			if (!this.m_NKCGameClient.IsShowUI())
			{
				NKCUtil.SetGameobjectActive(this.m_UNIT_GAGE, false);
				return;
			}
			if (this.m_UnitStateNow != null && !this.m_UnitStateNow.m_bShowGage)
			{
				NKCUtil.SetGameobjectActive(this.m_UNIT_GAGE, false);
				return;
			}
			if (this.m_UnitData.m_DungeonRespawnUnitTemplet != null && this.m_UnitData.m_DungeonRespawnUnitTemplet.m_eShowGage != NKMDungeonRespawnUnitTemplet.ShowGageOverride.Default)
			{
				NKCUtil.SetGameobjectActive(this.m_UNIT_GAGE, this.m_UnitData.m_DungeonRespawnUnitTemplet.m_eShowGage == NKMDungeonRespawnUnitTemplet.ShowGageOverride.Show);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_UNIT_GAGE, this.m_UnitTemplet.m_bShowGage);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00052174 File Offset: 0x00050374
		public void ActiveObject(bool bActive)
		{
			if (this.m_UnitObject.m_Instant.activeSelf == !bActive)
			{
				this.m_UnitObject.m_Instant.SetActive(bActive);
			}
			if (bActive)
			{
				if (this.m_NKCASUnitSpineSprite != null && !this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.activeSelf)
				{
					this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.SetActive(true);
				}
			}
			else
			{
				this.m_NKCMotionAfterImage.StopMotionImage();
				this.m_NKCUnitTouchObject.ActiveObject(false);
			}
			if (this.m_NKCASUnitShadow != null && this.m_NKCASUnitShadow.m_ShadowSpriteInstant != null && this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant != null && this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf == !bActive)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(bActive);
			}
			if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant != null && this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant.m_Instant != null && this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant.m_Instant.activeSelf == !bActive)
			{
				this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant.m_Instant.SetActive(bActive);
			}
			foreach (KeyValuePair<short, NKCASEffect> keyValuePair in this.m_dicBuffEffect)
			{
				NKCASEffect value = keyValuePair.Value;
				if (value != null)
				{
					if (!bActive)
					{
						if (value.m_bEndAnim)
						{
							value.PlayAnim("END", false, 1f);
						}
						else if (value.m_EffectInstant.m_Instant.activeSelf == !bActive)
						{
							value.m_EffectInstant.m_Instant.SetActive(bActive);
						}
					}
					else if (value.m_EffectInstant.m_Instant.activeSelf == !bActive)
					{
						value.m_EffectInstant.m_Instant.SetActive(bActive);
						value.PlayAnim("BASE", false, 1f);
					}
				}
			}
			foreach (NKCASEffect nkcaseffect in this.m_llBuffTextEffect)
			{
				if (nkcaseffect != null && !bActive)
				{
					nkcaseffect.SetDie();
				}
			}
			foreach (KeyValuePair<short, NKCASEffect> keyValuePair2 in this.m_dicBuffEffectRange)
			{
				NKCASEffect value2 = keyValuePair2.Value;
				if (value2 != null && value2.m_EffectInstant.m_Instant.activeSelf == !bActive)
				{
					value2.m_EffectInstant.m_Instant.SetActive(bActive);
				}
			}
			if (!bActive)
			{
				foreach (KeyValuePair<NKM_UNIT_STATUS_EFFECT, NKCASEffect> keyValuePair3 in this.m_dicStatusEffect)
				{
					NKCASEffect value3 = keyValuePair3.Value;
					if (value3 != null)
					{
						if (value3.m_bEndAnim)
						{
							value3.m_bAutoDie = true;
							value3.PlayAnim("END", false, 1f);
						}
						else
						{
							this.m_NKCGameClient.GetNKCEffectManager().DeleteEffect(value3.m_EffectUID);
						}
					}
				}
				this.m_dicStatusEffect.Clear();
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000524D0 File Offset: 0x000506D0
		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000524D9 File Offset: 0x000506D9
		protected override void StateEnd()
		{
			base.StateEnd();
			this.m_NKCGameClient.SetCameraFocusUnitOut(0);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000524F0 File Offset: 0x000506F0
		protected override void StateStart()
		{
			for (int i = 0; i < this.m_listSoundUID.Count; i++)
			{
				NKCSoundManager.StopSound(this.m_listSoundUID[i]);
			}
			this.m_listSoundUID.Clear();
			base.StateStart();
			this.m_GageOffsetPosX.SetTracking(this.m_UnitStateNow.m_fGageOffsetX, 0.4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_GageOffsetPosY.SetTracking(this.m_UnitStateNow.m_fGageOffsetY, 0.4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitObject.m_Instant.activeSelf && this.m_NKCAnimSpine != null)
			{
				this.m_NKCAnimSpine.SetPlaySpeed(this.m_UnitFrameData.m_fAnimSpeed);
				if (this.m_UnitStateNow.m_AnimName.Length > 1)
				{
					this.m_NKCAnimSpine.Play(this.m_UnitStateNow.m_AnimName, false, this.m_UnitStateNow.m_fAnimStartTime);
				}
			}
			this.SetShowUI();
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DIE && (this.m_NKMGame.GetGameData().m_NKMGameTeamDataA.m_MainShip == null || base.GetUnitData().m_UnitUID != this.m_NKMGame.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_UnitUID) && (this.m_NKMGame.GetGameData().m_NKMGameTeamDataB.m_MainShip == null || base.GetUnitData().m_UnitUID != this.m_NKMGame.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_UnitUID) && this.m_NKCASUnitShadow != null && this.m_NKCASUnitShadow.m_NKCComGroupColor != null)
			{
				this.m_NKCASUnitShadow.m_NKCComGroupColor.SetColor(-1f, -1f, -1f, 0f, 3f);
			}
			if (this.m_UnitStateNow.m_bSkillCutIn || this.m_UnitStateNow.m_bHyperSkillCutIn)
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData == null || gameOptionData.ViewSkillCutIn)
				{
					Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, this.m_UnitData);
					if (sprite != null)
					{
						string skillName = "";
						NKMUnitSkillTemplet skillTempletNowState = base.GetSkillTempletNowState();
						if (skillTempletNowState != null)
						{
							skillName = skillTempletNowState.GetSkillName();
						}
						else if (this.m_UnitStateNow.m_SkillCutInName.Length > 1)
						{
							skillName = NKCStringTable.GetString(this.m_UnitStateNow.m_SkillCutInName, false);
						}
						if (this.m_UnitStateNow.m_bSkillCutIn)
						{
							if (!this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG))
							{
								this.m_NKCGameClient.PlaySkillCutIn(this, false, true, sprite, base.GetUnitTemplet().m_UnitTempletBase.GetUnitName(), skillName);
							}
							else
							{
								this.m_NKCGameClient.PlaySkillCutIn(this, false, false, sprite, base.GetUnitTemplet().m_UnitTempletBase.GetUnitName(), skillName);
							}
						}
						if (this.m_UnitStateNow.m_bHyperSkillCutIn)
						{
							if (!this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG))
							{
								this.m_NKCGameClient.PlaySkillCutIn(this, true, true, sprite, base.GetUnitTemplet().m_UnitTempletBase.GetUnitName(), skillName);
							}
							else
							{
								this.m_NKCGameClient.PlaySkillCutIn(this, true, false, sprite, base.GetUnitTemplet().m_UnitTempletBase.GetUnitName(), skillName);
							}
						}
					}
				}
			}
			if (this.m_NKCASDangerChargeUI != null)
			{
				if (base.GetUnitFrameData().m_fDangerChargeTime > 0f)
				{
					this.m_NKCASDangerChargeUI.OpenDangerCharge(base.GetUnitFrameData().m_fDangerChargeTime, base.GetMaxHP(this.m_UnitStateNow.m_DangerCharge.m_fCancelDamageRate), (float)this.m_UnitStateNow.m_DangerCharge.m_CancelHitCount);
				}
				else
				{
					this.m_NKCASDangerChargeUI.CloseDangerCharge();
				}
			}
			if (this.m_UnitStateNow.m_StateID == this.m_bManualSkillUseStateID)
			{
				this.m_bManualSkillUseStart = true;
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000528A7 File Offset: 0x00050AA7
		protected override void ChangeAnimSpeed(float fAnimSpeed)
		{
			base.ChangeAnimSpeed(fAnimSpeed);
			if (this.m_UnitObject.m_Instant.activeSelf && this.m_NKCAnimSpine != null)
			{
				this.m_NKCAnimSpine.SetPlaySpeed(this.m_UnitFrameData.m_fAnimSpeed);
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000528E0 File Offset: 0x00050AE0
		protected override void StateUpdate()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			base.StateUpdate();
			if (this.m_UnitObject.m_Instant.activeSelf && !NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				if (this.m_UnitFrameData.m_bAnimPlayCountAddThisFrame)
				{
					this.m_NKCAnimSpine.Play(this.m_NKCAnimSpine.GetAnimName(), false, 0f);
				}
				this.m_NKCAnimSpine.Update(this.m_DeltaTime);
				if (this.m_UnitTemplet.m_bUseMotionBlur)
				{
					this.m_NKCMotionAfterImage.Update(this.m_DeltaTime);
				}
				this.SetDataObject();
			}
			float num = 0f;
			bool flag = false;
			if (this.m_NKCGameClient.GetShipSkillDrag() && this.m_NKCGameClient.GetSelectShipSkillID() != 0)
			{
				flag = this.m_NKCGameClient.GetShipSkillDrag();
				NKMShipSkillTemplet shipSkillTempletByID = NKMShipSkillManager.GetShipSkillTempletByID(this.m_NKCGameClient.GetSelectShipSkillID());
				if (shipSkillTempletByID != null)
				{
					if (shipSkillTempletByID.m_bFullMap)
					{
						num = this.m_NKCGameClient.GetMapTemplet().m_fMaxX - this.m_NKCGameClient.GetMapTemplet().m_fMinX;
					}
					else
					{
						num = shipSkillTempletByID.m_fRange;
					}
					if (shipSkillTempletByID.m_bEnemy && !this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG))
					{
						flag = false;
					}
					else if (!shipSkillTempletByID.m_bAir && base.IsAirUnit())
					{
						flag = false;
					}
				}
			}
			if (this.m_UnitFrameData.m_fHitLightTime > 0f)
			{
				this.m_NKCASUnitSpineSprite.SetColor(1f, 0.5f, 0.5f, -1f, false);
				if (this.m_NKCASUnitMiniMapFace != null)
				{
					this.m_NKCASUnitMiniMapFace.SetColor(1f, 0.5f, 0.5f);
				}
			}
			else if (flag && this.m_UnitSyncData.m_PosX >= this.m_NKCGameClient.GetShipSkillDragPosX() - num * 0.5f && this.m_UnitSyncData.m_PosX <= this.m_NKCGameClient.GetShipSkillDragPosX() + num * 0.5f)
			{
				this.m_NKCASUnitSpineSprite.SetColor(1f, 0.2f, 0.2f, -1f, false);
				if (this.m_NKCASUnitMiniMapFace != null)
				{
					this.m_NKCASUnitMiniMapFace.SetColor(1f, 0.2f, 0.2f);
				}
			}
			else
			{
				float fR = this.m_UnitFrameData.m_ColorR.GetNowValue();
				float fG = this.m_UnitFrameData.m_ColorG.GetNowValue();
				float fB = this.m_UnitFrameData.m_ColorB.GetNowValue();
				foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
				{
					NKMBuffData value = keyValuePair.Value;
					if (value != null && value.m_NKMBuffTemplet != null)
					{
						if (value.m_BuffSyncData.m_MasterGameUnitUID == base.GetUnitSyncData().m_GameUnitUID)
						{
							if (value.m_NKMBuffTemplet.m_MasterColorR != -1f)
							{
								fR = value.m_NKMBuffTemplet.m_MasterColorR;
							}
							if (value.m_NKMBuffTemplet.m_MasterColorG != -1f)
							{
								fG = value.m_NKMBuffTemplet.m_MasterColorG;
							}
							if (value.m_NKMBuffTemplet.m_MasterColorB != -1f)
							{
								fB = value.m_NKMBuffTemplet.m_MasterColorB;
							}
						}
						else
						{
							if (value.m_NKMBuffTemplet.m_ColorR != -1f)
							{
								fR = value.m_NKMBuffTemplet.m_ColorR;
							}
							if (value.m_NKMBuffTemplet.m_ColorG != -1f)
							{
								fG = value.m_NKMBuffTemplet.m_ColorG;
							}
							if (value.m_NKMBuffTemplet.m_ColorB != -1f)
							{
								fB = value.m_NKMBuffTemplet.m_ColorB;
							}
						}
					}
				}
				this.m_NKCASUnitSpineSprite.SetColor(fR, fG, fB, -1f, false);
				if (this.m_NKCASUnitMiniMapFace != null)
				{
					this.m_NKCASUnitMiniMapFace.SetColor(fR, fG, fB);
				}
			}
			if (this.m_fManualSkillUseAck > 0f)
			{
				this.m_fManualSkillUseAck -= this.m_DeltaTime;
				if (this.m_fManualSkillUseAck < 0f)
				{
					this.m_fManualSkillUseAck = 0f;
				}
			}
			if (this.m_bManualSkillUseStart && this.m_UnitStateNow.m_NKM_SKILL_TYPE < NKM_SKILL_TYPE.NST_HYPER)
			{
				this.m_fManualSkillUseAck = 0f;
			}
			if (!base.GetUnitTemplet().m_UnitTempletBase.IsEnv)
			{
				this.ProcessStatusEffect();
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00052D4C File Offset: 0x00050F4C
		private void SetDataObject()
		{
			if (Mathf.Abs(this.m_ObjPosX.GetNowValue() - this.m_UnitSyncData.m_PosX) > 300f)
			{
				this.m_ObjPosX.SetNowValue(this.m_UnitSyncData.m_PosX);
				this.m_ObjPosZ.SetNowValue(this.m_UnitSyncData.m_PosZ);
			}
			this.m_ObjPosX.SetTracking(this.m_UnitSyncData.m_PosX, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_ObjPosZ.SetTracking(this.m_UnitSyncData.m_PosZ, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_ObjPosX.Update(this.m_DeltaTime);
			this.m_ObjPosZ.Update(this.m_DeltaTime);
			this.m_Vector3Temp.Set(this.m_ObjPosX.GetNowValue(), this.m_ObjPosZ.GetNowValue() + this.m_UnitSyncData.m_JumpYPos, this.m_ObjPosZ.GetNowValue());
			this.m_UnitObject.m_Instant.transform.localPosition = this.m_Vector3Temp;
			float zscaleFactor = this.m_NKCGameClient.GetZScaleFactor(this.m_ObjPosZ.GetNowValue());
			this.m_Vector3Temp = this.m_SpriteObject.transform.localScale;
			this.m_Vector3Temp.x = this.m_UnitTemplet.m_SpriteScale * zscaleFactor;
			this.m_Vector3Temp.y = this.m_UnitTemplet.m_SpriteScale * zscaleFactor;
			this.m_SpriteObject.transform.localScale = this.m_Vector3Temp;
			if (this.m_UnitSyncData.m_bRight)
			{
				this.m_Vector3Temp = this.m_SpriteObject.transform.localEulerAngles;
				this.m_Vector3Temp.y = 0f;
				this.m_SpriteObject.transform.localEulerAngles = this.m_Vector3Temp;
				NKCUtil.SetGameObjectLocalPos(this.m_SpriteObject, this.m_UnitTemplet.m_SpriteOffsetX, this.m_UnitTemplet.m_SpriteOffsetY, 0f);
			}
			else
			{
				this.m_Vector3Temp = this.m_SpriteObject.transform.localEulerAngles;
				this.m_Vector3Temp.y = 180f;
				this.m_SpriteObject.transform.localEulerAngles = this.m_Vector3Temp;
				NKCUtil.SetGameObjectLocalPos(this.m_SpriteObject, -this.m_UnitTemplet.m_SpriteOffsetX, this.m_UnitTemplet.m_SpriteOffsetY, 0f);
			}
			this.SetDataObject_DangerChargeUI();
			this.SetDataObject_Shadow(zscaleFactor);
			this.SetDataObject_HPGage(zscaleFactor);
			this.SetDataObject_SkillGage();
			this.SetDataObject_MiniMapFace();
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00052FBC File Offset: 0x000511BC
		private void SetDataObject_DangerChargeUI()
		{
			if (this.m_NKCASDangerChargeUI == null)
			{
				return;
			}
			this.m_Vector3Temp.Set(this.m_ObjPosX.GetNowValue(), this.m_ObjPosZ.GetNowValue() + this.m_UnitSyncData.m_JumpYPos + base.GetUnitTemplet().m_UnitSizeY * 0.5f, this.m_ObjPosZ.GetNowValue());
			this.m_NKCASDangerChargeUI.SetPos(ref this.m_Vector3Temp);
			this.m_NKCASDangerChargeUI.Update(base.GetUnitFrameData().m_fDangerChargeTime, base.GetUnitFrameData().m_fDangerChargeDamage, (float)base.GetUnitFrameData().m_DangerChargeHitCount);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0005305C File Offset: 0x0005125C
		private void SetDataObject_Shadow(float fZScaleFactor)
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_UnitSyncData.m_bRight)
			{
				this.m_Vector3Temp.Set(this.m_UnitTemplet.m_fShadowRotateX, 180f, this.m_UnitTemplet.m_fShadowRotateZ);
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localEulerAngles = this.m_Vector3Temp;
			}
			else
			{
				this.m_Vector3Temp.Set(this.m_UnitTemplet.m_fShadowRotateX, 0f, this.m_UnitTemplet.m_fShadowRotateZ);
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localEulerAngles = this.m_Vector3Temp;
			}
			if (this.m_NKCASUnitSpineSprite.m_Bone_Move != null)
			{
				this.m_NKCASUnitSpineSprite.CalcBoneMoveWorldPos();
				float num = this.m_NKCASUnitSpineSprite.m_Bone_MovePos.x;
				if (this.m_UnitSyncData.m_bRight)
				{
					num -= this.m_UnitTemplet.m_SpriteOffsetX;
				}
				else
				{
					num += this.m_UnitTemplet.m_SpriteOffsetX;
				}
				this.m_Vector3Temp.Set(num, this.m_ObjPosZ.GetNowValue(), this.m_ObjPosZ.GetNowValue());
			}
			else
			{
				this.m_Vector3Temp.Set(this.m_ObjPosX.GetNowValue(), this.m_ObjPosZ.GetNowValue(), this.m_ObjPosZ.GetNowValue());
			}
			if (this.m_UnitSyncData.m_bRight)
			{
				this.m_Vector3Temp.x = this.m_Vector3Temp.x + this.m_UnitTemplet.m_fShadowOffsetX;
			}
			else
			{
				this.m_Vector3Temp.x = this.m_Vector3Temp.x - this.m_UnitTemplet.m_fShadowOffsetX;
			}
			this.m_Vector3Temp.y = this.m_Vector3Temp.y + this.m_UnitTemplet.m_fShadowOffsetY;
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localPosition = this.m_Vector3Temp;
			float num2 = 1f - 0.2f * this.m_UnitSyncData.m_JumpYPos * 0.01f;
			if (num2 < 0.3f)
			{
				num2 = 0.3f;
			}
			NKCUtil.SetGameObjectLocalScale(this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant, this.m_UnitTemplet.m_fShadowScaleX * num2 * fZScaleFactor, this.m_UnitTemplet.m_fShadowScaleY * num2 * fZScaleFactor, 1f);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000532B8 File Offset: 0x000514B8
		private void SetDataObject_HPGage(float fZScaleFactor)
		{
			float totalBarrierHP = this.GetTotalBarrierHP();
			if (this.m_UnitHealthBar != null)
			{
				this.m_UnitHealthBar.SetData(this.m_UnitSyncData.GetHP(), totalBarrierHP, this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP));
			}
			this.UpdateGagePos(fZScaleFactor);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0005330C File Offset: 0x0005150C
		private float GetTotalBarrierHP()
		{
			float num = 0f;
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value != null && value.m_NKMBuffTemplet.m_fBarrierHP != -1f && value.m_BuffSyncData.m_bAffect)
				{
					num += value.m_fBarrierHP;
				}
			}
			return num;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00053398 File Offset: 0x00051598
		public NKMAttackStateData GetFastestCoolTimeSkillData()
		{
			return this.GetFastestCoolTimeSkillData(this.m_UnitTemplet.m_listSkillStateData);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000533AB File Offset: 0x000515AB
		public NKMAttackStateData GetFastestCoolTimeHyperSkillData()
		{
			return this.GetFastestCoolTimeSkillData(this.m_UnitTemplet.m_listHyperSkillStateData);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000533C0 File Offset: 0x000515C0
		private NKMAttackStateData GetFastestCoolTimeSkillData(List<NKMAttackStateData> listNKMAttackStateData)
		{
			if (listNKMAttackStateData.Count < 1)
			{
				return null;
			}
			int num = 0;
			float num2 = 999999f;
			for (int i = 0; i < listNKMAttackStateData.Count; i++)
			{
				NKMAttackStateData nkmattackStateData = listNKMAttackStateData[i];
				if (nkmattackStateData != null && base.IsStateUnlocked(nkmattackStateData))
				{
					float stateCoolTime = base.GetStateCoolTime(nkmattackStateData.m_StateName);
					if (num2 > stateCoolTime)
					{
						num = i;
						num2 = stateCoolTime;
					}
				}
			}
			if (num < 0 || num >= listNKMAttackStateData.Count)
			{
				num = 0;
			}
			return listNKMAttackStateData[num];
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00053434 File Offset: 0x00051634
		private void SetDataObject_SkillGage()
		{
			if (this.m_UNIT_GAGE_PANEL.activeSelf)
			{
				NKMAttackStateData nkmattackStateData = this.GetFastestCoolTimeSkillData();
				if (nkmattackStateData != null && this.m_SKILL_GAUGE.GetSkillGauge().activeSelf)
				{
					float skillCoolTime = 1f - base.GetStateCoolTime(nkmattackStateData.m_StateName) / base.GetStateMaxCoolTime(nkmattackStateData.m_StateName);
					NKCUIComSkillGauge skill_GAUGE = this.m_SKILL_GAUGE;
					if (skill_GAUGE != null)
					{
						skill_GAUGE.SetSkillCoolTime(skillCoolTime);
					}
				}
				nkmattackStateData = this.GetFastestCoolTimeHyperSkillData();
				if (nkmattackStateData != null && this.m_SKILL_GAUGE.GetHyperGauge().activeSelf)
				{
					float hyperCoolTime = 1f - base.GetStateCoolTime(nkmattackStateData.m_StateName) / base.GetStateMaxCoolTime(nkmattackStateData.m_StateName);
					NKCUIComSkillGauge skill_GAUGE2 = this.m_SKILL_GAUGE;
					if (skill_GAUGE2 != null)
					{
						skill_GAUGE2.SetHyperCoolTime(hyperCoolTime);
					}
				}
			}
			if (!this.m_UNIT_HP_GAGE.activeSelf && !this.m_SKILL_GAUGE.GetSkillGauge().activeSelf && !this.m_SKILL_GAUGE.GetHyperGauge().activeSelf)
			{
				if (this.m_UNIT_GAGE_PANEL.activeSelf)
				{
					this.m_UNIT_GAGE_PANEL.SetActive(false);
					this.m_GageWide.SetNowValue(0f);
					Vector2 vector = this.m_UNIT_GAGE_RectTransform.sizeDelta;
					vector.x = this.m_GageWide.GetNowValue();
					this.m_UNIT_GAGE_RectTransform.sizeDelta = vector;
					vector = this.m_UNIT_LEVEL_RectTransform.anchoredPosition;
					vector.x = this.m_UNIT_LEVEL_RectTransform.sizeDelta.x * 0.5f;
					this.m_UNIT_LEVEL_RectTransform.anchoredPosition = vector;
				}
			}
			else if (!this.m_UNIT_GAGE_PANEL.activeSelf)
			{
				this.m_UNIT_GAGE_PANEL.SetActive(true);
				this.m_GageWide.SetTracking(this.m_fOrgGageSize, 1f, TRACKING_DATA_TYPE.TDT_SLOWER);
				Vector2 anchoredPosition = this.m_UNIT_LEVEL_RectTransform.anchoredPosition;
				anchoredPosition.x = 0f;
				this.m_UNIT_LEVEL_RectTransform.anchoredPosition = anchoredPosition;
			}
			this.m_GageWide.Update(this.m_DeltaTime);
			if (this.m_GageWide.IsTracking())
			{
				Vector2 sizeDelta = this.m_UNIT_GAGE_RectTransform.sizeDelta;
				sizeDelta.x = this.m_GageWide.GetNowValue();
				this.m_UNIT_GAGE_RectTransform.sizeDelta = sizeDelta;
			}
			bool flag = false;
			byte b = 0;
			if (this.m_NKCGameClient.GetMyRunTimeTeamData().m_NKM_GAME_AUTO_SKILL_TYPE != NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO && base.CanUseManualSkill(false, out flag, out b) && this.m_fManualSkillUseAck <= 0f && !base.IsDyingOrDie() && this.IsMyTeam() && !base.IsBoss())
			{
				this.m_NKCUnitTouchObject.ActiveObject(true);
				if (!this.m_UNIT_SKILL.activeSelf)
				{
					this.m_UNIT_SKILL.SetActive(true);
				}
				Vector3 vector2 = default(Vector3);
				if (!this.m_NKCASUnitSpineSprite.GetBoneWorldPos("MOVE", ref vector2))
				{
					vector2 = this.m_UnitObject.m_Instant.transform.position;
				}
				NKCCamera.GetWorldPosToScreenPos(out this.m_Vector3Temp, vector2.x, vector2.y, vector2.z);
				this.m_NKCUnitTouchObject.GetRectTransform().position = this.m_Vector3Temp;
				return;
			}
			this.m_NKCUnitTouchObject.ActiveObject(false);
			if (this.m_UNIT_SKILL.activeSelf)
			{
				this.m_UNIT_SKILL.SetActive(false);
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00053768 File Offset: 0x00051968
		public void MoveToLastTouchObject()
		{
			this.m_NKCUnitTouchObject.MoveToLastTouchObject();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00053778 File Offset: 0x00051978
		private void SetDataObject_MiniMapFace()
		{
			Vector2 vector = new Vector2(0f, 0f);
			if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_RectTransform != null)
			{
				vector = this.m_NKCASUnitMiniMapFace.m_RectTransform.anchoredPosition;
			}
			NKMMapTemplet mapTemplet = this.m_NKMGame.GetMapTemplet();
			if (mapTemplet != null)
			{
				float newX = (this.m_ObjPosX.GetNowValue() - mapTemplet.m_fMinX) / (mapTemplet.m_fMaxX - mapTemplet.m_fMinX) * this.m_NKCGameClient.GetGameHud().GetMiniMapRectWidth();
				vector.Set(newX, 0f);
			}
			if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_RectTransform != null)
			{
				this.m_NKCASUnitMiniMapFace.m_RectTransform.anchoredPosition = vector;
			}
			if (this.m_UnitSyncData.m_bRight)
			{
				if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_RectTransform != null)
				{
					vector = this.m_NKCASUnitMiniMapFace.m_RectTransform.localScale;
					vector.Set(1f, 1f);
				}
			}
			else if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_RectTransform != null)
			{
				vector = this.m_NKCASUnitMiniMapFace.m_RectTransform.localScale;
				vector.Set(-1f, 1f);
			}
			if (this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_RectTransform != null)
			{
				this.m_NKCASUnitMiniMapFace.m_RectTransform.localScale = vector;
			}
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY && this.m_NKCASUnitMiniMapFace != null && this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant != null && this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant.m_Instant != null && this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitMiniMapFace.m_MiniMapFaceInstant.m_Instant.SetActive(false);
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00053968 File Offset: 0x00051B68
		private void StateCamera()
		{
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0005396C File Offset: 0x00051B6C
		public float GetSkillCoolRate()
		{
			NKMAttackStateData fastestCoolTimeSkillData = this.GetFastestCoolTimeSkillData();
			if (fastestCoolTimeSkillData == null)
			{
				return 0f;
			}
			float stateCoolTime = base.GetStateCoolTime(fastestCoolTimeSkillData.m_StateName);
			float stateMaxCoolTime = base.GetStateMaxCoolTime(fastestCoolTimeSkillData.m_StateName);
			return stateCoolTime / stateMaxCoolTime;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000539A4 File Offset: 0x00051BA4
		public float GetHyperSkillCoolRate()
		{
			NKMAttackStateData fastestCoolTimeHyperSkillData = this.GetFastestCoolTimeHyperSkillData();
			if (fastestCoolTimeHyperSkillData == null)
			{
				return 0f;
			}
			float stateCoolTime = base.GetStateCoolTime(fastestCoolTimeHyperSkillData.m_StateName);
			float stateMaxCoolTime = base.GetStateMaxCoolTime(fastestCoolTimeHyperSkillData.m_StateName);
			return stateCoolTime / stateMaxCoolTime;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000539DC File Offset: 0x00051BDC
		protected override void PhysicProcess()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			base.PhysicProcess();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000539F0 File Offset: 0x00051BF0
		public override bool ProcessCamera()
		{
			if (this.m_UnitStateNow == null)
			{
				return false;
			}
			if (this.m_NKCGameClient.GetCameraMode() == NKM_GAME_CAMERA_MODE.NGCM_DRAG)
			{
				return false;
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.ActionCamera)
			{
				return false;
			}
			if (this.m_NKCGameClient.GetDeckDrag() || this.m_NKCGameClient.GetShipSkillDrag() || this.m_NKCGameClient.GetDeckTouchDown() || this.m_NKCGameClient.GetShipSkillDeckTouchDown() || this.m_NKCGameClient.GetCameraTouchDown())
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventCameraMove.Count; i++)
			{
				NKMEventCameraMove nkmeventCameraMove = this.m_UnitStateNow.m_listNKMEventCameraMove[i];
				if (nkmeventCameraMove != null && base.CheckEventCondition(nkmeventCameraMove.m_Condition))
				{
					bool flag = false;
					if (base.EventTimer(nkmeventCameraMove.m_bAnimTime, nkmeventCameraMove.m_fEventTimeMin, nkmeventCameraMove.m_fEventTimeMax))
					{
						flag = true;
					}
					if (flag)
					{
						result = true;
						if (nkmeventCameraMove.m_fCameraRadius <= 0f || NKCCamera.GetDist(this) <= nkmeventCameraMove.m_fCameraRadius)
						{
							float num = -1f;
							if (nkmeventCameraMove.m_fPosXOffset != -1f)
							{
								num = this.m_UnitSyncData.m_PosX;
								if (this.m_UnitSyncData.m_bRight)
								{
									num += nkmeventCameraMove.m_fPosXOffset;
									num += this.m_UnitTemplet.m_SpriteOffsetX;
								}
								else
								{
									num -= nkmeventCameraMove.m_fPosXOffset;
									num -= this.m_UnitTemplet.m_SpriteOffsetX;
								}
							}
							float num2 = -1f;
							if (nkmeventCameraMove.m_fPosYOffset != -1f)
							{
								num2 = this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos;
								num2 += nkmeventCameraMove.m_fPosYOffset;
								num2 += this.m_UnitTemplet.m_SpriteOffsetY;
							}
							NKCCamera.TrackingPos(nkmeventCameraMove.m_fMoveTrackingTime, num, num2, -1f);
							if (nkmeventCameraMove.m_fZoom != -1f)
							{
								NKCCamera.TrackingZoom(nkmeventCameraMove.m_fZoomTrackingTime, NKCCamera.GetCameraSizeOrg() + nkmeventCameraMove.m_fZoom);
							}
							if (nkmeventCameraMove.m_fFocusBlur > 0f)
							{
								float num3 = this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos;
								num3 += nkmeventCameraMove.m_fPosYOffset;
								num3 += this.m_UnitTemplet.m_SpriteOffsetY;
								num3 += this.m_UnitTemplet.m_UnitSizeY * 0.7f;
								NKCCamera.SetFocusBlur(nkmeventCameraMove.m_fFocusBlur, num, num3, this.m_UnitSyncData.m_PosZ);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00053C60 File Offset: 0x00051E60
		protected override void ProcessEventText(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventText.Count; i++)
			{
				NKMEventText nkmeventText = this.m_UnitStateNow.m_listNKMEventText[i];
				if (nkmeventText != null && base.CheckEventCondition(nkmeventText.m_Condition))
				{
					bool flag = false;
					if (nkmeventText.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventText.m_bAnimTime, nkmeventText.m_fEventTime, true) && !nkmeventText.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag && NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
					{
						NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkmeventText.m_Text, false), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
					}
				}
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00053D18 File Offset: 0x00051F18
		protected override void ProcessEventAttack()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			base.ProcessEventAttack();
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventAttack.Count; i++)
			{
				NKMEventAttack nkmeventAttack = this.m_UnitStateNow.m_listNKMEventAttack[i];
				if (nkmeventAttack != null && nkmeventAttack.m_SoundName.Length > 1 && base.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, true))
				{
					NKCSoundManager.PlaySound(nkmeventAttack.m_SoundName, nkmeventAttack.m_fLocalVol, this.m_SpriteObject.transform.position.x, 1200f, false, 0f, false, 0f);
				}
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00053DC4 File Offset: 0x00051FC4
		protected override void ProcessAttackHitEffect(NKMEventAttack cNKMEventAttack)
		{
			if (cNKMEventAttack == null)
			{
				return;
			}
			this.m_Vector3Temp.Set(this.m_ObjPosX.GetNowValue(), this.m_ObjPosZ.GetNowValue() + this.m_UnitSyncData.m_JumpYPos, this.m_ObjPosZ.GetNowValue());
			this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, cNKMEventAttack.m_EffectName, cNKMEventAttack.m_EffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, this.m_UnitSyncData.m_bRight, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "", 1f, false, false, 0f, -1f, false);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00053E9C File Offset: 0x0005209C
		protected override void ProcessEventSound(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventSound.Count; i++)
			{
				NKMEventSound nkmeventSound = this.m_UnitStateNow.m_listNKMEventSound[i];
				if (nkmeventSound != null && base.CheckEventCondition(nkmeventSound.m_Condition) && nkmeventSound.IsRightSkin(this.m_UnitData.m_SkinID))
				{
					bool bOneTime = true;
					if (this.m_UnitStateNow.m_bAnimLoop)
					{
						bOneTime = false;
					}
					bool flag = false;
					if (nkmeventSound.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventSound.m_bAnimTime, nkmeventSound.m_fEventTime, bOneTime) && !nkmeventSound.m_bStateEndTime)
					{
						flag = true;
					}
					string audioClipName;
					if (flag && NKMRandom.Range(0f, 1f) <= nkmeventSound.m_PlayRate && nkmeventSound.GetRandomSound(this.m_UnitData, out audioClipName))
					{
						int item;
						if (!nkmeventSound.m_bVoice)
						{
							item = NKCSoundManager.PlaySound(audioClipName, nkmeventSound.m_fLocalVol, this.m_SpriteObject.transform.position.x, nkmeventSound.m_fFocusRange, nkmeventSound.m_bLoop, 0f, false, 0f);
						}
						else
						{
							item = NKCSoundManager.PlayVoice(audioClipName, base.GetUnitDataGame().m_GameUnitUID, true, true, nkmeventSound.m_fLocalVol, this.m_SpriteObject.transform.position.x, nkmeventSound.m_fFocusRange, nkmeventSound.m_bLoop, 0f, false);
						}
						if (nkmeventSound.m_bStopSound)
						{
							this.m_listSoundUID.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00054028 File Offset: 0x00052228
		protected override void ProcessEventColor(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitFrameData.m_fColorEventTime > 0f)
			{
				this.m_UnitFrameData.m_fColorEventTime -= this.m_DeltaTime;
				this.m_UnitFrameData.m_ColorR.Update(this.m_DeltaTime);
				this.m_UnitFrameData.m_ColorG.Update(this.m_DeltaTime);
				this.m_UnitFrameData.m_ColorB.Update(this.m_DeltaTime);
				if (this.m_UnitFrameData.m_fColorEventTime <= 0f)
				{
					this.m_UnitFrameData.m_fColorEventTime = this.m_UnitFrameData.m_ColorR.GetTime() + 0.1f;
					this.m_UnitFrameData.m_ColorR.SetTracking(this.m_UnitTemplet.m_ColorR, this.m_UnitFrameData.m_ColorR.GetTime(), TRACKING_DATA_TYPE.TDT_SLOWER);
					this.m_UnitFrameData.m_ColorG.SetTracking(this.m_UnitTemplet.m_ColorG, this.m_UnitFrameData.m_ColorG.GetTime(), TRACKING_DATA_TYPE.TDT_SLOWER);
					this.m_UnitFrameData.m_ColorB.SetTracking(this.m_UnitTemplet.m_ColorB, this.m_UnitFrameData.m_ColorB.GetTime(), TRACKING_DATA_TYPE.TDT_SLOWER);
				}
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventColor.Count; i++)
			{
				NKMEventColor nkmeventColor = this.m_UnitStateNow.m_listNKMEventColor[i];
				if (nkmeventColor != null && base.CheckEventCondition(nkmeventColor.m_Condition))
				{
					bool flag = false;
					if (nkmeventColor.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventColor.m_bAnimTime, nkmeventColor.m_fEventTime, true) && !nkmeventColor.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.m_UnitFrameData.m_fColorEventTime = nkmeventColor.m_fColorTime;
						this.m_UnitFrameData.m_ColorR.SetTracking(nkmeventColor.m_fColorR, nkmeventColor.m_fTrackTime, TRACKING_DATA_TYPE.TDT_SLOWER);
						this.m_UnitFrameData.m_ColorG.SetTracking(nkmeventColor.m_fColorG, nkmeventColor.m_fTrackTime, TRACKING_DATA_TYPE.TDT_SLOWER);
						this.m_UnitFrameData.m_ColorB.SetTracking(nkmeventColor.m_fColorB, nkmeventColor.m_fTrackTime, TRACKING_DATA_TYPE.TDT_SLOWER);
					}
				}
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0005424C File Offset: 0x0005244C
		protected override void ProcessEventCameraCrash(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventCameraCrash.Count; i++)
			{
				NKMEventCameraCrash nkmeventCameraCrash = this.m_UnitStateNow.m_listNKMEventCameraCrash[i];
				if (nkmeventCameraCrash != null && base.CheckEventCondition(nkmeventCameraCrash.m_Condition))
				{
					bool flag = false;
					if (nkmeventCameraCrash.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventCameraCrash.m_bAnimTime, nkmeventCameraCrash.m_fEventTime, true) && !nkmeventCameraCrash.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag && (nkmeventCameraCrash.m_fCrashRadius <= 0f || NKCCamera.GetDist(this) <= nkmeventCameraCrash.m_fCrashRadius))
					{
						switch (nkmeventCameraCrash.m_CameraCrashType)
						{
						case NKM_CAMERA_CRASH_TYPE.NCCT_UP:
							NKCCamera.UpCrashCamera(nkmeventCameraCrash.m_fCameraCrashSpeed, nkmeventCameraCrash.m_fCameraCrashAccel);
							break;
						case NKM_CAMERA_CRASH_TYPE.NCCT_DOWN:
							NKCCamera.DownCrashCamera(nkmeventCameraCrash.m_fCameraCrashSpeed, nkmeventCameraCrash.m_fCameraCrashAccel);
							break;
						case NKM_CAMERA_CRASH_TYPE.NCCT_UP_DOWN:
							NKCCamera.UpDownCrashCamera(nkmeventCameraCrash.m_fCameraCrashGap, nkmeventCameraCrash.m_fCameraCrashTime, 0.05f);
							break;
						case NKM_CAMERA_CRASH_TYPE.NCCT_UP_DOWN_NO_RESET:
							NKCCamera.UpDownCrashCameraNoReset(nkmeventCameraCrash.m_fCameraCrashGap, nkmeventCameraCrash.m_fCameraCrashTime);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0005436C File Offset: 0x0005256C
		protected override void ProcessEventCameraMove(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_NKCGameClient.GetCameraMode() == NKM_GAME_CAMERA_MODE.NGCM_DRAG)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventCameraMove.Count; i++)
			{
				NKMEventCameraMove nkmeventCameraMove = this.m_UnitStateNow.m_listNKMEventCameraMove[i];
				if (nkmeventCameraMove != null && base.CheckEventCondition(nkmeventCameraMove.m_Condition))
				{
					bool flag = false;
					if (base.EventTimer(nkmeventCameraMove.m_bAnimTime, nkmeventCameraMove.m_fEventTimeMin, true))
					{
						flag = true;
					}
					if (flag)
					{
						this.m_NKCGameClient.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_FOCUS_UNIT);
						this.m_NKCGameClient.SetCameraFocusUnit(this.m_UnitDataGame.m_GameUnitUID);
						this.m_NKCGameClient.SetCameraNormalTackingWaitTime(0.1f);
					}
				}
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00054420 File Offset: 0x00052620
		protected override void ProcessEventFadeWorld()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventFadeWorld.Count; i++)
			{
				NKMEventFadeWorld nkmeventFadeWorld = this.m_UnitStateNow.m_listNKMEventFadeWorld[i];
				if (nkmeventFadeWorld != null && base.EventTimer(nkmeventFadeWorld.m_bAnimTime, nkmeventFadeWorld.m_fEventTimeMin, nkmeventFadeWorld.m_fEventTimeMax))
				{
					this.m_NKCGameClient.FadeColor(this, nkmeventFadeWorld.m_fColorR, nkmeventFadeWorld.m_fColorG, nkmeventFadeWorld.m_fColorB, nkmeventFadeWorld.m_fMapColorKeepTime, nkmeventFadeWorld.m_fMapColorReturnTime);
				}
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000544AC File Offset: 0x000526AC
		protected override void ProcessEventDissolve(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			this.m_DissolveFactor.Update(this.m_DeltaTime);
			if (this.m_DissolveFactor.IsTracking())
			{
				this.m_NKCASUnitSpineSprite.SetDissolveBlend(this.m_DissolveFactor.GetNowValue());
			}
			else if (this.m_DissolveFactor.GetNowValue() <= 0f && this.m_bDissolveEnable)
			{
				this.m_bDissolveEnable = false;
				this.m_NKCASUnitSpineSprite.SetDissolveBlend(0f);
				this.m_NKCASUnitSpineSprite.SetDissolveOn(this.m_bDissolveEnable);
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDissolve.Count; i++)
			{
				NKMEventDissolve nkmeventDissolve = this.m_UnitStateNow.m_listNKMEventDissolve[i];
				if (nkmeventDissolve != null && base.CheckEventCondition(nkmeventDissolve.m_Condition))
				{
					bool flag = false;
					if (nkmeventDissolve.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventDissolve.m_bAnimTime, nkmeventDissolve.m_fEventTime, true) && !nkmeventDissolve.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						if (!this.m_bDissolveEnable)
						{
							this.m_bDissolveEnable = true;
							this.m_NKCASUnitSpineSprite.SetDissolveOn(this.m_bDissolveEnable);
							this.m_ColorTemp.r = nkmeventDissolve.m_fColorR;
							this.m_ColorTemp.g = nkmeventDissolve.m_fColorG;
							this.m_ColorTemp.b = nkmeventDissolve.m_fColorB;
							this.m_ColorTemp.a = 1f;
							this.m_NKCASUnitSpineSprite.SetDissolveColor(this.m_ColorTemp);
						}
						this.m_DissolveFactor.SetTracking(nkmeventDissolve.m_fDissolve, nkmeventDissolve.m_fTrackTime, TRACKING_DATA_TYPE.TDT_NORMAL);
					}
				}
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00054648 File Offset: 0x00052848
		protected override void ProcessEventMotionBlur()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			this.m_NKCMotionAfterImage.SetEnable(false);
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventMotionBlur.Count; i++)
			{
				NKMEventMotionBlur nkmeventMotionBlur = this.m_UnitStateNow.m_listNKMEventMotionBlur[i];
				if (nkmeventMotionBlur != null && base.CheckEventCondition(nkmeventMotionBlur.m_Condition) && base.EventTimer(nkmeventMotionBlur.m_bAnimTime, nkmeventMotionBlur.m_fEventTimeMin, nkmeventMotionBlur.m_fEventTimeMax) && this.m_NKCMotionAfterImage != null)
				{
					this.m_NKCMotionAfterImage.SetColor(new Color(nkmeventMotionBlur.m_fColorR, nkmeventMotionBlur.m_fColorG, nkmeventMotionBlur.m_fColorB));
					this.m_NKCMotionAfterImage.SetEnable(true);
				}
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000546FC File Offset: 0x000528FC
		private void ProcessStatusEffect()
		{
			if (base.IsDyingOrDie())
			{
				foreach (NKCASEffect nkcaseffect in this.m_dicStatusEffect.Values)
				{
					if (nkcaseffect != null)
					{
						nkcaseffect.Stop(true);
						nkcaseffect.m_bAutoDie = true;
					}
				}
				this.m_dicStatusEffect.Clear();
				return;
			}
			if (this.m_UnitStateNow != null)
			{
				foreach (NKM_UNIT_STATUS_EFFECT status in this.m_UnitStateNow.m_listFixedStatusEffect)
				{
					this.AddStatusEffect(status, false);
				}
			}
			foreach (NKM_UNIT_STATUS_EFFECT nkm_UNIT_STATUS_EFFECT in this.m_UnitTemplet.m_listFixedStatusEffect)
			{
				if ((this.m_UnitStateNow == null || !this.m_UnitStateNow.m_listFixedStatusImmune.Contains(nkm_UNIT_STATUS_EFFECT)) && !this.m_UnitTemplet.m_listFixedStatusImmune.Contains(nkm_UNIT_STATUS_EFFECT))
				{
					this.AddStatusEffect(nkm_UNIT_STATUS_EFFECT, true);
				}
			}
			foreach (NKM_UNIT_STATUS_EFFECT nkm_UNIT_STATUS_EFFECT2 in base.GetUnitFrameData().m_hsStatus)
			{
				if (!this.m_UnitTemplet.m_listFixedStatusEffect.Contains(nkm_UNIT_STATUS_EFFECT2) && !this.m_UnitTemplet.m_listFixedStatusImmune.Contains(nkm_UNIT_STATUS_EFFECT2) && (this.m_UnitStateNow == null || !this.m_UnitStateNow.m_listFixedStatusImmune.Contains(nkm_UNIT_STATUS_EFFECT2)) && !base.GetUnitFrameData().m_hsImmuneStatus.Contains(nkm_UNIT_STATUS_EFFECT2))
				{
					this.AddStatusEffect(nkm_UNIT_STATUS_EFFECT2, false);
				}
			}
			this.m_hsEffectToRemove.Clear();
			foreach (KeyValuePair<NKM_UNIT_STATUS_EFFECT, NKCASEffect> keyValuePair in this.m_dicStatusEffect)
			{
				if (!base.HasStatus(keyValuePair.Key))
				{
					NKCASEffect value = keyValuePair.Value;
					if (value != null)
					{
						value.Stop(true);
						value.m_bAutoDie = true;
					}
					this.m_hsEffectToRemove.Add(keyValuePair.Key);
				}
			}
			foreach (NKM_UNIT_STATUS_EFFECT key in this.m_hsEffectToRemove)
			{
				this.m_dicStatusEffect.Remove(key);
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000549B0 File Offset: 0x00052BB0
		private void AddStatusEffect(NKM_UNIT_STATUS_EFFECT status, bool bFixedStatus)
		{
			NKCASEffect nkcaseffect;
			if (!this.m_dicStatusEffect.TryGetValue(status, out nkcaseffect))
			{
				nkcaseffect = null;
			}
			if (nkcaseffect != null && this.m_NKCGameClient.GetNKCEffectManager().IsLiveEffect(nkcaseffect.m_EffectUID))
			{
				return;
			}
			NKMUnitStatusTemplet nkmunitStatusTemplet = NKMUnitStatusTemplet.Find(status);
			if (nkmunitStatusTemplet == null)
			{
				return;
			}
			if (bFixedStatus && !nkmunitStatusTemplet.m_bShowFixedStatus)
			{
				return;
			}
			if (string.IsNullOrEmpty(nkmunitStatusTemplet.m_StatusEffectName))
			{
				return;
			}
			float fScaleFactor = 1f;
			float offsetY;
			string boneName;
			switch (nkmunitStatusTemplet.m_StatusEffectPosition)
			{
			case NKMUnitStatusTemplet.EffectPosition.OverGuageMark:
				offsetY = 80f;
				boneName = "";
				goto IL_EC;
			case NKMUnitStatusTemplet.EffectPosition.Head:
				offsetY = 0f;
				boneName = "BIP01_HEAD";
				fScaleFactor = base.GetUnitTemplet().m_fBuffEffectScaleFactor;
				goto IL_EC;
			case NKMUnitStatusTemplet.EffectPosition.Ground:
				offsetY = 0f;
				boneName = "";
				fScaleFactor = base.GetUnitTemplet().m_fBuffEffectScaleFactor;
				goto IL_EC;
			}
			boneName = "BIP01_PELVIS";
			if (this.m_NKCASUnitSpineSprite.GetBone(boneName) == null)
			{
				boneName = "BIP01_SPINE1";
			}
			fScaleFactor = base.GetUnitTemplet().m_fBuffEffectScaleFactor;
			offsetY = 0f;
			IL_EC:
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(nkmunitStatusTemplet.m_StatusEffectName, nkmunitStatusTemplet.m_StatusEffectName);
			nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, nkmassetName.m_BundleName, nkmassetName.m_AssetName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos, this.m_UnitSyncData.m_PosZ, true, fScaleFactor, 0f, offsetY, -1f, false, 0f, true, boneName, false, false, "BASE", 1f, false, false, 0f, -1f, false);
			this.m_dicStatusEffect[status] = nkcaseffect;
			if (nkcaseffect != null)
			{
				this.m_llEffect.AddLast(nkcaseffect);
				if (nkmunitStatusTemplet.m_StatusEffectPosition == NKMUnitStatusTemplet.EffectPosition.OverGuageMark)
				{
					nkcaseffect.SetGuageRoot(true);
				}
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00054B70 File Offset: 0x00052D70
		protected override void ProcessEventEffect(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (bStateEnd)
			{
				using (LinkedList<NKCASEffect>.Enumerator enumerator = this.m_llEffect.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKCASEffect nkcaseffect = enumerator.Current;
						if (nkcaseffect != null && nkcaseffect.m_bStateEndStop && this.m_NKCGameClient.GetNKCEffectManager().IsLiveEffect(nkcaseffect.m_EffectUID))
						{
							nkcaseffect.Stop(nkcaseffect.m_bStateEndStopForce);
						}
					}
					goto IL_238;
				}
			}
			LinkedListNode<NKCASEffect> next;
			for (LinkedListNode<NKCASEffect> linkedListNode = this.m_llEffect.First; linkedListNode != null; linkedListNode = next)
			{
				next = linkedListNode.Next;
				NKCASEffect value = linkedListNode.Value;
				if (value == null || !this.m_NKCGameClient.GetNKCEffectManager().IsLiveEffect(value.m_EffectUID))
				{
					this.m_llEffect.Remove(linkedListNode);
				}
				else if (value.m_bUseGuageAsRoot)
				{
					value.SetPos(this.m_ObjPosX.GetNowValue() + this.m_fGagePosX, this.m_ObjPosZ.GetNowValue() + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY, this.m_ObjPosZ.GetNowValue());
				}
				else if (value.m_BoneName.Length > 1)
				{
					Bone bone = this.m_NKCASUnitSpineSprite.GetBone(value.m_BoneName);
					if (bone != null)
					{
						this.m_Vector3Temp = this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.TransformPoint(bone.WorldX, bone.WorldY, 0f);
					}
					value.SetPos(this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z);
					if (value.m_bUseBoneRotate && bone != null)
					{
						Vector3 eulerAngles = this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.rotation.eulerAngles;
						value.m_EffectInstant.m_Instant.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + bone.WorldRotationX);
					}
				}
				else
				{
					value.SetRight(this.m_UnitSyncData.m_bRight);
					value.SetPos(this.m_ObjPosX.GetNowValue(), this.m_ObjPosZ.GetNowValue() + this.m_UnitSyncData.m_JumpYPos, this.m_ObjPosZ.GetNowValue());
				}
			}
			IL_238:
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventEffect.Count; i++)
			{
				NKMEventEffect nkmeventEffect = this.m_UnitStateNow.m_listNKMEventEffect[i];
				if (nkmeventEffect != null)
				{
					if (nkmeventEffect.m_bCutIn)
					{
						NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
						if (gameOptionData != null && !gameOptionData.ViewSkillCutIn)
						{
							goto IL_555;
						}
					}
					if (base.CheckEventCondition(nkmeventEffect.m_Condition) && nkmeventEffect.IsRightSkin(this.m_UnitData.m_SkinID))
					{
						bool flag = false;
						if (nkmeventEffect.m_bStateEndTime && bStateEnd)
						{
							flag = true;
						}
						else if (base.EventTimer(nkmeventEffect.m_bAnimTime, nkmeventEffect.m_fEventTime, true) && !nkmeventEffect.m_bStateEndTime)
						{
							flag = true;
						}
						if (flag)
						{
							this.m_Vector3Temp2.Set(nkmeventEffect.m_OffsetX, nkmeventEffect.m_OffsetY, nkmeventEffect.m_OffsetZ);
							if (nkmeventEffect.m_bFixedPos)
							{
								this.m_Vector3Temp.Set(nkmeventEffect.m_OffsetX, nkmeventEffect.m_OffsetY, nkmeventEffect.m_OffsetZ);
								this.m_Vector3Temp2.Set(0f, 0f, 0f);
							}
							else if (nkmeventEffect.m_BoneName.Length > 1)
							{
								Bone bone2 = this.m_NKCASUnitSpineSprite.GetBone(nkmeventEffect.m_BoneName);
								if (bone2 != null)
								{
									this.m_Vector3Temp = this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.TransformPoint(bone2.WorldX, bone2.WorldY, 0f);
								}
							}
							else
							{
								this.m_Vector3Temp.Set(this.m_ObjPosX.GetNowValue(), this.m_ObjPosZ.GetNowValue() + this.m_UnitSyncData.m_JumpYPos, this.m_ObjPosZ.GetNowValue());
								if (nkmeventEffect.m_bLandConnect)
								{
									this.m_Vector3Temp.y = this.m_ObjPosZ.GetNowValue();
								}
							}
							bool bRight = this.m_UnitSyncData.m_bRight;
							if (nkmeventEffect.m_bForceRight)
							{
								bRight = true;
							}
							if (nkmeventEffect.m_bCutIn)
							{
								this.m_NKCGameClient.GetNKCEffectManager().StopCutInEffect();
							}
							string effectName = nkmeventEffect.GetEffectName(this.m_UnitData);
							NKCASEffect nkcaseffect2 = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, effectName, effectName, nkmeventEffect.m_ParentType, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, bRight, nkmeventEffect.m_fScaleFactor, this.m_Vector3Temp2.x, this.m_Vector3Temp2.y, this.m_Vector3Temp2.z, nkmeventEffect.m_bUseOffsetZtoY, nkmeventEffect.m_fAddRotate, nkmeventEffect.m_bUseZScale, nkmeventEffect.m_BoneName, nkmeventEffect.m_bUseBoneRotate, true, nkmeventEffect.m_AnimName, 1f, false, nkmeventEffect.m_bCutIn, nkmeventEffect.m_fReserveTime, -1f, false);
							if (nkcaseffect2 != null)
							{
								if (nkmeventEffect.m_bHold || nkmeventEffect.m_bStateEndStop)
								{
									nkcaseffect2.m_bStateEndStop = nkmeventEffect.m_bStateEndStop;
									nkcaseffect2.m_bStateEndStopForce = nkmeventEffect.m_bStateEndStopForce;
									this.m_llEffect.AddLast(nkcaseffect2);
								}
								nkcaseffect2.SetUseMasterAnimSpeed(nkmeventEffect.m_UseMasterAnimSpeed);
								nkcaseffect2.SetApplyStopTime(nkmeventEffect.m_ApplyStopTime);
							}
						}
					}
				}
				IL_555:;
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00055100 File Offset: 0x00053300
		protected override void ProcessEventHyperSkillCutIn()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.ViewSkillCutIn)
			{
				return;
			}
			if (!this.m_bHyperCutinLoaded)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventHyperSkillCutIn.Count; i++)
			{
				NKMEventHyperSkillCutIn nkmeventHyperSkillCutIn = this.m_UnitStateNow.m_listNKMEventHyperSkillCutIn[i];
				if (nkmeventHyperSkillCutIn != null && base.CheckEventCondition(nkmeventHyperSkillCutIn.m_Condition))
				{
					bool flag = false;
					if (base.EventTimer(nkmeventHyperSkillCutIn.m_bAnimTime, nkmeventHyperSkillCutIn.m_fEventTime, true))
					{
						flag = true;
					}
					if (flag)
					{
						this.m_NKCGameClient.GetNKCEffectManager().StopCutInEffect();
						NKM_GAME_SPEED_TYPE nkm_GAME_SPEED_TYPE = this.m_NKCGameClient.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE;
						float fAnimSpeed;
						if (nkm_GAME_SPEED_TYPE != NKM_GAME_SPEED_TYPE.NGST_2)
						{
							if (nkm_GAME_SPEED_TYPE != NKM_GAME_SPEED_TYPE.NGST_05)
							{
								fAnimSpeed = 1.1f;
							}
							else
							{
								fAnimSpeed = 0.6f;
							}
						}
						else
						{
							fAnimSpeed = 1.5f;
						}
						this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, nkmeventHyperSkillCutIn.m_BGEffectName, nkmeventHyperSkillCutIn.m_BGEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, 0f, 0f, 0f, base.GetUnitSyncData().m_bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "BASE", fAnimSpeed, false, true, 0f, nkmeventHyperSkillCutIn.m_fDurationTime, false);
						string skillCutin = NKMSkinManager.GetSkillCutin(base.GetUnitData(), nkmeventHyperSkillCutIn.m_CutInEffectName);
						this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, skillCutin, skillCutin, NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_CONTROL_EFFECT, 0f, 0f, 0f, base.GetUnitSyncData().m_bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, nkmeventHyperSkillCutIn.m_CutInEffectAnimName, fAnimSpeed, false, true, 0f, -1f, false);
						NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_SKILL_CUTIN_COMMON_DESC", "AB_FX_SKILL_CUTIN_COMMON_DESC", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_CONTROL_EFFECT, 0f, 0f, 0f, base.GetUnitSyncData().m_bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "BASE", fAnimSpeed, false, true, 0f, nkmeventHyperSkillCutIn.m_fDurationTime, false);
						if (nkcaseffect != null)
						{
							nkcaseffect.Init_AB_FX_SKILL_CUTIN_COMMON_DESC();
							NKCUtil.SetLabelText(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME, base.GetUnitTemplet().m_UnitTempletBase.GetUnitName());
							if (base.GetUnitSyncData().m_bRight)
							{
								NKCUtil.SetRectTransformLocalRotate(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform, 0f, 0f, 0f);
							}
							else
							{
								NKCUtil.SetRectTransformLocalRotate(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform, 0f, 180f, 0f);
							}
							NKMUnitSkillTemplet skillTempletNowState = base.GetSkillTempletNowState();
							if (skillTempletNowState != null)
							{
								NKCUtil.SetLabelText(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME, skillTempletNowState.GetSkillName());
							}
							else
							{
								NKCUtil.SetLabelText(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME, "");
							}
							if (base.GetUnitSyncData().m_bRight)
							{
								NKCUtil.SetRectTransformLocalRotate(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform, 0f, 0f, 0f);
							}
							else
							{
								NKCUtil.SetRectTransformLocalRotate(nkcaseffect.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform, 0f, 180f, 0f);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0005544C File Offset: 0x0005364C
		protected override void ProcessBuff()
		{
			base.ProcessBuff();
			if (this.m_BuffUnitLevelLastUpdate != base.GetUnitFrameData().m_BuffUnitLevel)
			{
				this.m_BuffUnitLevelLastUpdate = base.GetUnitFrameData().m_BuffUnitLevel;
				this.m_UNIT_LEVEL_TEXT_Text.SetLevel(this.m_UnitData, base.GetUnitFrameData().m_BuffUnitLevel, Array.Empty<Text>());
				NKCGameHudDeckSlot hudDeckByUnitUID = this.m_NKCGameClient.GetGameHud().GetHudDeckByUnitUID(this.m_UnitData.m_UnitUID);
				if (hudDeckByUnitUID != null)
				{
					hudDeckByUnitUID.SetDeckUnitLevel(this.m_UnitData, base.GetUnitFrameData().m_BuffUnitLevel);
				}
			}
			int num = 0;
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value != null)
				{
					if ((value.m_BuffSyncData.m_MasterGameUnitUID == base.GetUnitDataGame().m_GameUnitUID && !value.m_NKMBuffTemplet.m_AffectMe) || !value.m_NKMBuffTemplet.m_bShowBuffIcon)
					{
						continue;
					}
					if (value.m_fLifeTime == -1f || value.m_NKMBuffTemplet.m_bInfinity || value.m_BuffSyncData.m_bRangeSon)
					{
						this.GageSetBuffIconActive(num, true, value, 1f);
					}
					else
					{
						float fLifeTimeRate = value.m_fLifeTime / value.GetLifeTimeMax();
						this.GageSetBuffIconActive(num, true, value, fLifeTimeRate);
					}
				}
				num++;
				if (num >= 6)
				{
					break;
				}
			}
			for (int i = num; i < 6; i++)
			{
				this.GageSetBuffIconActive(i, false, null, 1f);
			}
			foreach (KeyValuePair<short, NKCASEffect> keyValuePair2 in this.m_dicBuffEffect)
			{
				NKCASEffect value2 = keyValuePair2.Value;
				if (value2 != null)
				{
					float fX = 0f;
					float fY = base.GetUnitSyncData().m_PosZ + base.GetUnitSyncData().m_JumpYPos;
					float fZ = base.GetUnitSyncData().m_PosZ - 0.01f;
					NKMBuffData buff = base.GetBuff(keyValuePair2.Key, false);
					if (buff != null)
					{
						string text;
						if (buff.m_BuffSyncData.m_MasterGameUnitUID == base.GetUnitSyncData().m_GameUnitUID)
						{
							text = buff.m_NKMBuffTemplet.m_MasterEffectBoneName;
						}
						else
						{
							text = buff.m_NKMBuffTemplet.m_SlaveEffectBoneName;
						}
						if (!buff.m_NKMBuffTemplet.IsFixedPosBuff() || buff.m_BuffSyncData.m_bRangeSon)
						{
							if (text.Length > 1)
							{
								if (this.m_NKCASUnitSpineSprite.GetBoneWorldPos(text, ref this.m_Vector3Temp))
								{
									fX = this.m_Vector3Temp.x;
									fY = this.m_Vector3Temp.y;
									fZ = this.m_Vector3Temp.z - 1f;
								}
								else
								{
									fX = base.GetUnitSyncData().m_PosX + this.m_fGagePosX;
								}
							}
							else
							{
								fX = base.GetUnitSyncData().m_PosX + this.m_fGagePosX;
							}
						}
						else
						{
							fX = buff.m_fBuffPosX;
						}
					}
					value2.SetPos(fX, fY, fZ);
					if (!buff.m_NKMBuffTemplet.m_bIgnoreUnitScaleFactor)
					{
						value2.SetScaleFactor(base.GetUnitTemplet().m_fBuffEffectScaleFactor, base.GetUnitTemplet().m_fBuffEffectScaleFactor, base.GetUnitTemplet().m_fBuffEffectScaleFactor);
					}
				}
			}
			LinkedListNode<NKCASEffect> linkedListNode = this.m_llBuffTextEffect.First;
			while (linkedListNode != null)
			{
				LinkedListNode<NKCASEffect> next = linkedListNode.Next;
				NKCASEffect value3 = linkedListNode.Value;
				if (value3 != null)
				{
					value3.SetPos(this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY + 30f + 35f + 35f * (float)value3.m_BuffDescTextPosYIndex, this.m_UnitSyncData.m_PosZ);
					if ((value3.m_bPlayed && !this.m_NKCGameClient.GetNKCEffectManager().IsLiveEffect(value3.m_EffectUID)) || value3.m_MasterUnitGameUID != base.GetUnitDataGame().m_GameUnitUID)
					{
						this.m_llBuffTextEffect.Remove(linkedListNode);
						if (this.m_BuffDescTextPosYIndex > 1)
						{
							this.m_BuffDescTextPosYIndex = 0;
						}
					}
					linkedListNode = next;
				}
			}
			if (this.m_llBuffTextEffect.Count == 0)
			{
				this.m_BuffDescTextPosYIndex = 0;
			}
			foreach (KeyValuePair<short, NKCASEffect> keyValuePair3 in this.m_dicBuffEffectRange)
			{
				NKCASEffect value4 = keyValuePair3.Value;
				if (value4 != null)
				{
					float fX2 = 0f;
					NKMBuffData buff2 = base.GetBuff(keyValuePair3.Key, false);
					if (buff2 != null)
					{
						if (!buff2.m_NKMBuffTemplet.IsFixedPosBuff() || buff2.m_BuffSyncData.m_bRangeSon)
						{
							fX2 = base.GetUnitSyncData().m_PosX + this.m_fGagePosX;
						}
						else
						{
							fX2 = buff2.m_fBuffPosX;
						}
					}
					value4.SetPos(fX2, base.GetUnitSyncData().m_PosZ, base.GetUnitSyncData().m_PosZ);
				}
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00055990 File Offset: 0x00053B90
		protected override bool ProcessDangerCharge()
		{
			return base.ProcessDangerCharge();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0005599A File Offset: 0x00053B9A
		public override float GetEventMovePosX(NKMEventMove cNKMEventMove, bool isATeam)
		{
			if (this.m_NKCGameClient.IsReversePosTeam(this.m_NKCGameClient.m_MyTeam))
			{
				isATeam = !isATeam;
			}
			return base.GetEventMovePosX(cNKMEventMove, isATeam);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000559C4 File Offset: 0x00053BC4
		public override void DamageReact(NKMDamageInst cNKMDamageInst, bool bBuffDamage)
		{
			base.DamageReact(cNKMDamageInst, bBuffDamage);
			if (cNKMDamageInst == null)
			{
				return;
			}
			if (cNKMDamageInst.m_Templet == null)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				return;
			}
			if (cNKMDamageInst.m_ReActResult != NKM_REACT_TYPE.NRT_NO)
			{
				bool flag = false;
				if (cNKMDamageInst.m_ReAttackCount == 0)
				{
					if (cNKMDamageInst.m_EventAttack != null && cNKMDamageInst.m_EventAttack.m_AttackUnitCount + (int)cNKMDamageInst.m_AttackerAddAttackUnitCount <= cNKMDamageInst.m_AttackCount)
					{
						flag = true;
					}
				}
				else if (cNKMDamageInst.m_bReAttackCountOver)
				{
					flag = true;
				}
				if (!flag && cNKMDamageInst.m_Templet.m_HitSoundName.Length > 1)
				{
					NKCSoundManager.PlaySound(cNKMDamageInst.m_Templet.m_HitSoundName, cNKMDamageInst.m_Templet.m_fLocalVol, this.m_SpriteObject.transform.position.x, 800f, false, 0.1f * (float)cNKMDamageInst.m_listHitUnit.Count, false, 0f);
				}
				if (!flag && NKCCamera.GetDist(this) <= 800f)
				{
					NKCCamera.UpDownCrashCamera(cNKMDamageInst.m_Templet.m_fCameraCrashGap, cNKMDamageInst.m_Templet.m_fCameraCrashTime, 0.05f);
				}
				if (base.IsAirUnit() && cNKMDamageInst.m_Templet.m_HitEffectAir.Length > 1)
				{
					if (!flag)
					{
						this.m_NKCGameClient.HitEffect(this, cNKMDamageInst, cNKMDamageInst.m_Templet.m_HitEffectAir, cNKMDamageInst.m_Templet.m_HitEffectAir, cNKMDamageInst.m_Templet.m_HitEffectAirAnimName, cNKMDamageInst.m_Templet.m_fHitEffectAirRange, cNKMDamageInst.m_Templet.m_fHitEffectAirOffsetZ, false);
						return;
					}
					this.m_NKCGameClient.HitEffect(this, cNKMDamageInst, "AB_fx_hit_b_blue_small", "AB_fx_hit_b_blue_small", "BASE", 50f, 0f, false);
					return;
				}
				else
				{
					if (!flag)
					{
						this.m_NKCGameClient.HitEffect(this, cNKMDamageInst, cNKMDamageInst.m_Templet.m_HitEffect, cNKMDamageInst.m_Templet.m_HitEffect, cNKMDamageInst.m_Templet.m_HitEffectAnimName, cNKMDamageInst.m_Templet.m_fHitEffectRange, cNKMDamageInst.m_Templet.m_fHitEffectOffsetZ, cNKMDamageInst.m_Templet.m_bHitEffectLand);
						return;
					}
					this.m_NKCGameClient.HitEffect(this, cNKMDamageInst, "AB_fx_hit_b_blue_small", "AB_fx_hit_b_blue_small", "BASE", 50f, 0f, false);
				}
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00055BDC File Offset: 0x00053DDC
		public void OnRecv(NKMUnitSyncData cNKMUnitSyncData)
		{
			if (cNKMUnitSyncData.GetHP() > 0f)
			{
				this.ActiveObject(true);
			}
			this.SyncDamageData(cNKMUnitSyncData.m_listDamageData);
			this.SyncBuffData(cNKMUnitSyncData.m_dicBuffData);
			this.SyncStatusTimeData(cNKMUnitSyncData.m_listStatusTimeData);
			if (this.m_UnitSyncData.m_StateID != cNKMUnitSyncData.m_StateID || this.m_UnitSyncData.m_StateChangeCount != cNKMUnitSyncData.m_StateChangeCount)
			{
				base.StateChange((short)cNKMUnitSyncData.m_StateID, true, true);
			}
			byte stateID = this.m_UnitSyncData.m_StateID;
			NKM_UNIT_PLAY_STATE nkm_UNIT_PLAY_STATE = this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE;
			this.m_UnitSyncData.DeepCopyWithoutDamageAndMarkFrom(cNKMUnitSyncData);
			if (nkm_UNIT_PLAY_STATE != this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING)
			{
				this.m_EventMovePosX.StopTracking();
				this.m_EventMovePosZ.StopTracking();
				this.m_EventMovePosJumpY.StopTracking();
			}
			this.SyncUnitSyncHalfData(this.m_UnitSyncData);
			this.m_TargetUnit = base.GetTargetUnit(false);
			this.m_SubTargetUnit = base.GetTargetUnit(this.m_UnitSyncData.m_SubTargetUID, this.m_UnitTemplet.m_SubTargetFindData);
			this.m_UnitSyncData.m_StateID = stateID;
			this.SyncEventMark(cNKMUnitSyncData.m_listNKM_UNIT_EVENT_MARK);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00055D08 File Offset: 0x00053F08
		private void SyncEventMark(List<byte> listNKM_UNIT_EVENT_MARK)
		{
			for (int i = 0; i < listNKM_UNIT_EVENT_MARK.Count; i++)
			{
				switch (listNKM_UNIT_EVENT_MARK[i])
				{
				case 1:
					this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_EXCLAMATION_MARK", "AB_FX_EXCLAMATION_MARK", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY + 30f, this.m_UnitSyncData.m_PosZ, true, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "BASE", 1f, false, false, 0.2f * (float)i, -1f, false);
					break;
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
				{
					NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_COST", "AB_FX_COST", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY + 30f, this.m_UnitSyncData.m_PosZ, true, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "BASE", 1f, false, false, 0.2f * (float)i, -1f, false);
					if (nkcaseffect != null && nkcaseffect.m_EffectInstant != null && nkcaseffect.m_EffectInstant.m_Instant != null)
					{
						nkcaseffect.Init_AB_FX_COST();
						if (nkcaseffect.m_AB_FX_COST_COUNT_Text != null)
						{
							this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
							switch (listNKM_UNIT_EVENT_MARK[i])
							{
							case 2:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+";
								goto IL_639;
							case 3:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+1";
								goto IL_639;
							case 4:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+2";
								goto IL_639;
							case 5:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+3";
								goto IL_639;
							case 6:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+4";
								goto IL_639;
							case 7:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+5";
								goto IL_639;
							case 12:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+5";
								goto IL_639;
							case 13:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+4";
								goto IL_639;
							case 14:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+3";
								goto IL_639;
							case 15:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+2";
								goto IL_639;
							case 16:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+1";
								goto IL_639;
							case 17:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "-";
								goto IL_639;
							case 18:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "-1";
								goto IL_639;
							case 19:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "-2";
								goto IL_639;
							case 20:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "-3";
								goto IL_639;
							case 21:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "-4";
								goto IL_639;
							case 22:
								nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "-5";
								goto IL_639;
							}
							nkcaseffect.m_AB_FX_COST_COUNT_Text.text = "+";
						}
					}
					break;
				}
				case 9:
				case 10:
				case 11:
					this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_CMN_RECALL", "AB_FX_CMN_RECALL", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos, this.m_UnitSyncData.m_PosZ, true, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "BASE", 1f, false, false, 0.2f * (float)i, -1f, false);
					NKCSoundManager.PlaySound("FX_UI_DUNGEON_RESPONE", 1f, this.m_SpriteObject.transform.position.x, 1200f, false, 0f, false, 0f);
					break;
				case 24:
				{
					NKCASEffect value = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_CMN_DISPEL", "AB_FX_CMN_DISPEL", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, true, 1f, 0f, 0f, 0f, false, 0f, true, "BIP01_SPINE1", false, true, "BASE", 1f, false, false, 0f, -1f, false);
					this.m_llEffect.AddLast(value);
					break;
				}
				case 25:
				{
					Bone bone = this.m_NKCASUnitSpineSprite.GetBone("BIP01_SPINE1");
					if (bone != null)
					{
						this.m_Vector3Temp = this.m_NKCASUnitSpineSprite.m_SPINE_SkeletonAnimation.transform.TransformPoint(bone.WorldX, bone.WorldY, 0f);
					}
					this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_CMN_INSTANTKILL", "AB_FX_CMN_INSTANTKILL", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, true, 1f, 0f, 0f, 0f, false, 0f, true, "BIP01_SPINE1", false, true, "BASE", 1f, false, false, 0f, -1f, false);
					break;
				}
				}
				IL_639:;
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00056360 File Offset: 0x00054560
		private void SyncDamageData(List<NKMDamageData> listDamageData)
		{
			for (int i = 0; i < listDamageData.Count; i++)
			{
				NKMDamageData nkmdamageData = listDamageData[i];
				if (nkmdamageData != null)
				{
					NKM_DAMAGE_RESULT_TYPE nkm_DAMAGE_RESULT_TYPE = nkmdamageData.m_NKM_DAMAGE_RESULT_TYPE;
					if (nkm_DAMAGE_RESULT_TYPE > NKM_DAMAGE_RESULT_TYPE.NDRT_HEAL)
					{
						if (nkm_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME)
						{
							this.SyncDamageData_CoolTime(nkmdamageData, i);
						}
					}
					else
					{
						this.SyncDamageData_Damage(nkmdamageData, i);
					}
				}
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000563AC File Offset: 0x000545AC
		private void SyncStatusTimeData(List<NKMUnitStatusTimeSyncData> listStatusTimeData)
		{
			foreach (NKMUnitStatusTimeSyncData nkmunitStatusTimeSyncData in listStatusTimeData)
			{
				if (nkmunitStatusTimeSyncData != null)
				{
					this.m_UnitFrameData.m_dicStatusTime[nkmunitStatusTimeSyncData.m_eStatusType] = nkmunitStatusTimeSyncData.m_fTime;
					this.StatusTimeAffectEffect(nkmunitStatusTimeSyncData.m_eStatusType);
				}
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00056420 File Offset: 0x00054620
		private void SyncDamageData_Damage(NKMDamageData cNKMDamageData, int i)
		{
			float num = (float)cNKMDamageData.m_FinalDamage;
			if (num > 0f && num < 1f)
			{
				num = 1f;
			}
			if (num == 0f)
			{
				return;
			}
			NKM_DAMAGE_RESULT_TYPE nkm_DAMAGE_RESULT_TYPE = cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE;
			if (nkm_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL || nkm_DAMAGE_RESULT_TYPE - NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT <= 3)
			{
				if (base.GetUnitFrameData().m_BarrierBuffData != null)
				{
					base.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP -= num;
					if (base.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP < 0f)
					{
						base.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP = 0f;
					}
				}
				else
				{
					base.GetUnitFrameData().m_fDangerChargeDamage += num;
					base.GetUnitFrameData().m_DangerChargeHitCount++;
				}
			}
			if (!this.m_NKCGameClient.IsShowUI())
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				return;
			}
			if (cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_HEAL)
			{
				NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_BUFF_IN_HEAL", "AB_FX_BUFF_IN_HEAL", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos, this.m_UnitSyncData.m_PosZ, true, base.GetUnitTemplet().m_fBuffEffectScaleFactor, 0f, 0f, 0f, false, 0f, true, "", false, true, "BASE", 1f, false, false, 0.2f * (float)i, -1f, false);
				if (nkcaseffect != null)
				{
					this.m_llEffect.AddLast(nkcaseffect);
				}
			}
			this.ShowDamageNumber(cNKMDamageData, num, i);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000565BC File Offset: 0x000547BC
		private void ShowDamageNumber(NKMDamageData cNKMDamageData, float fDamage, int i)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			NKCGameOptionDataSt.GameOptionDamageNumber gameOptionDamageNumber = NKCGameOptionDataSt.GameOptionDamageNumber.Off;
			if (gameOptionData != null)
			{
				gameOptionDamageNumber = gameOptionData.UseDamageAndBuffNumberFx;
			}
			switch (cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE)
			{
			case NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL:
			case NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME:
				if (gameOptionDamageNumber != NKCGameOptionDataSt.GameOptionDamageNumber.On)
				{
					return;
				}
				break;
			case NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK:
				return;
			case NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT:
			case NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL:
			case NKM_DAMAGE_RESULT_TYPE.NDRT_MISS:
			case NKM_DAMAGE_RESULT_TYPE.NDRT_WEAK:
			case NKM_DAMAGE_RESULT_TYPE.NDRT_HEAL:
				if (gameOptionDamageNumber == NKCGameOptionDataSt.GameOptionDamageNumber.Off)
				{
					return;
				}
				break;
			}
			if (gameOptionDamageNumber != NKCGameOptionDataSt.GameOptionDamageNumber.Off && cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE != NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK)
			{
				string animName = "BASE";
				switch (cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE)
				{
				case NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT:
					animName = "PROTECT";
					break;
				case NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL:
					animName = "CRITICAL";
					break;
				case NKM_DAMAGE_RESULT_TYPE.NDRT_WEAK:
					animName = "WEAK";
					break;
				}
				if (cNKMDamageData.m_bAttackCountOver || cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_MISS)
				{
					if (cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT)
					{
						animName = "SMALL_PROTECT";
					}
					else
					{
						animName = "SMALL";
					}
				}
				NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_DAMAGE_TEXT", "AB_FX_DAMAGE_TEXT", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY + 30f, this.m_UnitSyncData.m_PosZ, true, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, animName, 1f, false, false, 0.2f * (float)i, -1f, false);
				if (nkcaseffect != null && nkcaseffect.m_EffectInstant != null && nkcaseffect.m_EffectInstant.m_Instant != null)
				{
					nkcaseffect.DamageTextInit();
					if (nkcaseffect.m_DamageText != null && nkcaseffect.m_DamageTextCritical != null)
					{
						switch (cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE)
						{
						case NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL:
						case NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT:
						case NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL:
						case NKM_DAMAGE_RESULT_TYPE.NDRT_WEAK:
							if (gameOptionDamageNumber == NKCGameOptionDataSt.GameOptionDamageNumber.On)
							{
								nkcaseffect.m_DamageText.text = string.Format("{0}", (int)fDamage);
							}
							else
							{
								nkcaseffect.m_DamageText.text = "";
							}
							break;
						case NKM_DAMAGE_RESULT_TYPE.NDRT_MISS:
							if (gameOptionDamageNumber == NKCGameOptionDataSt.GameOptionDamageNumber.On)
							{
								nkcaseffect.m_DamageText.text = string.Format("<color=FFFFFF>m</color>{0}", (int)fDamage);
							}
							else
							{
								nkcaseffect.m_DamageText.text = string.Format("<color=FFFFFF>m</color>", Array.Empty<object>());
							}
							break;
						case NKM_DAMAGE_RESULT_TYPE.NDRT_HEAL:
							nkcaseffect.m_DamageText.text = string.Format("+{0}", (int)fDamage);
							break;
						}
						Color color = nkcaseffect.m_DamageText.color;
						if (cNKMDamageData.m_NKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_HEAL)
						{
							color.r = 0f;
							color.g = 1f;
							color.b = 0f;
						}
						else if (!this.m_NKMGame.IsEnemy(this.m_NKCGameClient.m_MyTeam, base.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG))
						{
							color.r = 1f;
							color.g = 0.5f;
							color.b = 0f;
						}
						else
						{
							color.r = 1f;
							color.g = 0f;
							color.b = 0f;
						}
						nkcaseffect.m_DamageText.color = color;
						nkcaseffect.m_DamageTextCritical.color = color;
					}
				}
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00056910 File Offset: 0x00054B10
		private void SyncDamageData_CoolTime(NKMDamageData cNKMDamageData, int i)
		{
			NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_COOLTIME", "AB_FX_COOLTIME", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY + 30f, this.m_UnitSyncData.m_PosZ, true, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "BASE", 1f, false, false, 0.2f * (float)i, -1f, false);
			if (nkcaseffect != null && nkcaseffect.m_EffectInstant != null && nkcaseffect.m_EffectInstant.m_Instant != null)
			{
				nkcaseffect.Init_AB_FX_COOLTIME();
				if (nkcaseffect.m_NKM_UI_HUD_COOLTIME_COUNT_Text != null)
				{
					float num = (float)cNKMDamageData.m_FinalDamage;
					if (num > 0f)
					{
						nkcaseffect.m_NKM_UI_HUD_COOLTIME_COUNT_Text.text = string.Format(NKCUtilString.GET_STRING_SKILL_COOLTIME_INC, (int)num);
						return;
					}
					nkcaseffect.m_NKM_UI_HUD_COOLTIME_COUNT_Text.text = string.Format(NKCUtilString.GET_STRING_SKILL_COOLTIME_DEC, (int)num);
					nkcaseffect.PlayAnim("MINUS", false, 1f);
				}
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00056A54 File Offset: 0x00054C54
		private void SetBuffOverlap(short buffID, byte count)
		{
			NKMBuffData buff = base.GetBuff(buffID, false);
			if (buff != null && buff.m_BuffSyncData.m_OverlapCount != count)
			{
				buff.m_BuffSyncData.m_OverlapCount = count;
				this.m_bBuffChangedThisFrame = true;
				this.m_bPushSimpleSyncData = true;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00056A98 File Offset: 0x00054C98
		private void SyncBuffData(Dictionary<short, NKMBuffSyncData> dicBuffData)
		{
			foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in dicBuffData)
			{
				NKMBuffSyncData value = keyValuePair.Value;
				if (value != null)
				{
					if (!base.IsBuffLive(value.m_BuffID) || value.m_bNew)
					{
						if (this.AddBuffByID(keyValuePair.Key, value.m_BuffStatLevel, value.m_BuffTimeLevel, value.m_MasterGameUnitUID, value.m_bUseMasterStat, value.m_bRangeSon, false, 1) != 0)
						{
							this.SetBuffOverlap(keyValuePair.Key, value.m_OverlapCount);
							this.m_bBuffChangedThisFrame = true;
						}
						NKMBuffData buff = base.GetBuff(keyValuePair.Key, false);
						if (buff != null && buff.m_NKMBuffTemplet != null && buff.m_NKMBuffTemplet.m_UnitLevel != 0)
						{
							this.m_bBuffUnitLevelChangedThisFrame = true;
						}
					}
					else
					{
						NKMBuffData buff2 = base.GetBuff(value.m_BuffID, false);
						if (buff2 != null && buff2.m_BuffSyncData.m_MasterGameUnitUID == value.m_MasterGameUnitUID && (buff2.m_BuffSyncData.m_OverlapCount != value.m_OverlapCount || buff2.m_BuffSyncData.m_BuffStatLevel != value.m_BuffStatLevel || buff2.m_BuffSyncData.m_BuffTimeLevel != value.m_BuffTimeLevel))
						{
							buff2.m_BuffSyncData.m_OverlapCount = value.m_OverlapCount;
							buff2.m_BuffSyncData.m_BuffStatLevel = value.m_BuffStatLevel;
							buff2.m_BuffSyncData.m_BuffTimeLevel = value.m_BuffTimeLevel;
							this.m_bBuffChangedThisFrame = true;
							this.BuffAffectEffect(buff2);
						}
					}
				}
			}
			this.m_listBuffDelete.Clear();
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair2 in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value2 = keyValuePair2.Value;
				if (value2 != null && !dicBuffData.ContainsKey(value2.m_BuffSyncData.m_BuffID))
				{
					this.m_listBuffDelete.Add(value2.m_BuffSyncData.m_BuffID);
				}
			}
			foreach (short buffID in this.m_listBuffDelete)
			{
				this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.NoUse);
			}
			if (this.m_listBuffDelete.Count > 0)
			{
				this.m_bBuffChangedThisFrame = true;
			}
			this.m_listBuffDelete.Clear();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00056D50 File Offset: 0x00054F50
		public override short AddBuffByID(short buffID, byte buffLevel, byte buffTimeLevel, short masterGameUnitUID, bool bUseMasterStat, bool bRangeSon, bool bStateEndRemove, byte overlapCount)
		{
			short num = base.AddBuffByID(buffID, buffLevel, buffTimeLevel, masterGameUnitUID, bUseMasterStat, bRangeSon, bStateEndRemove, overlapCount);
			if (num != 0)
			{
				NKMBuffData buff = base.GetBuff(buffID, false);
				if (buff != null && buff.m_NKMBuffTemplet != null)
				{
					string text;
					string text2;
					if (buff.m_BuffSyncData.m_MasterGameUnitUID == base.GetUnitSyncData().m_GameUnitUID)
					{
						text = buff.m_NKMBuffTemplet.m_MasterEffectBoneName;
						text2 = buff.m_NKMBuffTemplet.GetMasterEffectName(this.m_UnitData.m_SkinID);
					}
					else
					{
						NKMUnit unit = this.m_NKCGameClient.GetUnit(masterGameUnitUID, true, true);
						int skinID = (unit != null) ? unit.GetUnitData().m_SkinID : 0;
						text = buff.m_NKMBuffTemplet.m_SlaveEffectBoneName;
						text2 = buff.m_NKMBuffTemplet.GetSlaveEffectName(skinID);
					}
					float posY = base.GetUnitSyncData().m_PosZ + base.GetUnitSyncData().m_JumpYPos;
					float posZ = base.GetUnitSyncData().m_PosZ - 0.01f;
					float posX;
					if (!buff.m_NKMBuffTemplet.IsFixedPosBuff() || bRangeSon)
					{
						if (text.Length > 1)
						{
							if (this.m_NKCASUnitSpineSprite.GetBoneWorldPos(text, ref this.m_Vector3Temp))
							{
								posX = this.m_Vector3Temp.x;
								posY = this.m_Vector3Temp.y;
								posZ = this.m_Vector3Temp.z - 1f;
							}
							else
							{
								posX = base.GetUnitSyncData().m_PosX + this.m_fGagePosX;
							}
						}
						else
						{
							posX = base.GetUnitSyncData().m_PosX + this.m_fGagePosX;
						}
					}
					else
					{
						posX = buff.m_fBuffPosX;
					}
					if (text2.Length > 1)
					{
						NKCScenManager scenManager = NKCScenManager.GetScenManager();
						NKCGameOptionData nkcgameOptionData = (scenManager != null) ? scenManager.GetGameOptionData() : null;
						if (nkcgameOptionData == null || nkcgameOptionData.UseBuffEffect)
						{
							NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, text2, text2, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, posX, posY, posZ, this.m_UnitSyncData.m_bRight, 1f, 0f, 0f, 0f, false, 0f, true, "", false, false, "BASE", 1f, false, false, 0f, -1f, false);
							if (nkcaseffect != null)
							{
								if (!this.m_dicBuffEffect.ContainsKey(buffID))
								{
									this.m_dicBuffEffect.Add(buffID, nkcaseffect);
								}
								if (!buff.m_NKMBuffTemplet.m_bIgnoreUnitScaleFactor)
								{
									nkcaseffect.SetScaleFactor(base.GetUnitTemplet().m_fBuffEffectScaleFactor, base.GetUnitTemplet().m_fBuffEffectScaleFactor, base.GetUnitTemplet().m_fBuffEffectScaleFactor);
								}
							}
						}
					}
					if (buff.m_BuffSyncData.m_MasterGameUnitUID == base.GetUnitSyncData().m_GameUnitUID && buff.m_NKMBuffTemplet.m_Range > 0f && buff.m_NKMBuffTemplet.m_RangeEffectName.Length > 1)
					{
						NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, buff.m_NKMBuffTemplet.m_RangeEffectName, buff.m_NKMBuffTemplet.m_RangeEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, posX, base.GetUnitSyncData().m_PosZ, base.GetUnitSyncData().m_PosZ, this.m_UnitSyncData.m_bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, false, "BASE", 1f, false, false, 0f, -1f, false);
						if (nkcaseffect != null)
						{
							if (!this.m_dicBuffEffectRange.ContainsKey(buffID))
							{
								this.m_dicBuffEffectRange.Add(buffID, nkcaseffect);
							}
							nkcaseffect.SetScaleFactor(buff.m_NKMBuffTemplet.m_Range, buff.m_NKMBuffTemplet.m_Range, buff.m_NKMBuffTemplet.m_Range);
						}
					}
					if (this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
					{
						NKCLeaguePVPMgr.CheckLeagueModeBuff(buff, this);
					}
				}
			}
			return num;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00057104 File Offset: 0x00055304
		protected override void BuffAffectEffect(NKMBuffData cNKMBuffData)
		{
			if (cNKMBuffData != null && cNKMBuffData.m_NKMBuffTemplet != null && !cNKMBuffData.m_NKMBuffTemplet.m_bShowBuffText)
			{
				return;
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && gameOptionData.UseDamageAndBuffNumberFx != NKCGameOptionDataSt.GameOptionDamageNumber.On)
			{
				return;
			}
			bool bDebuff = cNKMBuffData.m_NKMBuffTemplet.m_bDebuff;
			if (cNKMBuffData.m_BuffSyncData.m_bRangeSon)
			{
				bDebuff = cNKMBuffData.m_NKMBuffTemplet.m_bDebuffSon;
			}
			this.AddBuffDescString(this.GetBuffDescText(cNKMBuffData.m_NKMBuffTemplet, cNKMBuffData.m_BuffSyncData), bDebuff);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00057180 File Offset: 0x00055380
		private void StatusTimeAffectEffect(NKM_UNIT_STATUS_EFFECT status)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && gameOptionData.UseDamageAndBuffNumberFx != NKCGameOptionDataSt.GameOptionDamageNumber.On)
			{
				return;
			}
			this.AddBuffDescString(NKCUtilString.GetStatusName(status), NKMUnitStatusTemplet.IsDebuff(status));
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000571B8 File Offset: 0x000553B8
		private void AddBuffDescString(string text, bool bDebuff)
		{
			NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(base.GetUnitSyncData().m_GameUnitUID, "AB_FX_BUFF_TEXT", "AB_FX_BUFF_TEXT", NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_UnitSyncData.m_PosX + this.m_fGagePosX, this.m_UnitSyncData.m_PosZ + this.m_UnitSyncData.m_JumpYPos + this.m_fGagePosY + 30f + 35f + 35f * (float)this.m_BuffDescTextPosYIndex, this.m_UnitSyncData.m_PosZ, true, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "BASE", 1f, false, false, 0.2f * (float)this.m_BuffDescTextPosYIndex, -1f, false);
			if (nkcaseffect != null && nkcaseffect.m_EffectInstant != null && nkcaseffect.m_EffectInstant.m_Instant != null)
			{
				nkcaseffect.BuffTextInit(this.m_BuffDescTextPosYIndex);
				if (nkcaseffect.m_BuffText != null)
				{
					nkcaseffect.m_BuffText.text = text;
					if (!bDebuff)
					{
						Color color = nkcaseffect.m_BuffText.color;
						color.r = 1f;
						color.g = 1f;
						color.b = 1f;
						nkcaseffect.m_BuffText.color = color;
					}
					else
					{
						Color color2 = nkcaseffect.m_BuffText.color;
						color2.r = 1f;
						color2.g = 0f;
						color2.b = 0f;
						nkcaseffect.m_BuffText.color = color2;
					}
				}
				this.m_llBuffTextEffect.AddFirst(nkcaseffect);
			}
			this.m_BuffDescTextPosYIndex += 1;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0005736C File Offset: 0x0005556C
		public string GetBuffDescText(NKMBuffTemplet buffTemplet, NKMBuffSyncData buffSyncData)
		{
			if (!this.m_NKCGameClient.IsShowUI())
			{
				return "";
			}
			StringBuilder builder = NKMString.GetBuilder();
			bool flag = false;
			foreach (NKM_UNIT_STATUS_EFFECT status in buffTemplet.m_ApplyStatus)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCUtilString.GetStatusName(status));
				flag = true;
			}
			foreach (NKM_UNIT_STATUS_EFFECT status2 in buffTemplet.m_ImmuneStatus)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCUtilString.GetStatusImmuneName(status2));
				flag = true;
			}
			if (buffTemplet.m_AddAttackUnitCount > 0)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_ADD_ATTACK_UNIT_COUNT", false));
				flag = true;
			}
			if (buffTemplet.m_fAddAttackRange > 0f)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_ADD_ATTACK_RANGE", false));
				flag = true;
			}
			if (buffTemplet.m_bDispelBuff)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_DISPEL_BUFF", false));
				flag = true;
			}
			if (buffTemplet.m_bRangeSonDispelBuff && buffSyncData.m_bRangeSon)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_DISPEL_BUFF", false));
				flag = true;
			}
			if (buffTemplet.m_bNotCastSummon)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_NOT_CAST_SUMMON", false));
				flag = true;
			}
			if (buffTemplet.m_bRangeSonDispelDebuff && buffSyncData.m_bRangeSon)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_DISPEL_DEBUFF", false));
				flag = true;
			}
			if (buffTemplet.m_bDispelDebuff)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_DISPEL_DEBUFF", false));
				flag = true;
			}
			switch (buffTemplet.m_SuperArmorLevel)
			{
			case NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL:
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_SUPERARMOR_SKILL", false));
				flag = true;
				break;
			case NKM_SUPER_ARMOR_LEVEL.NSAL_HYPER:
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_SUPERARMOR_HYPER", false));
				flag = true;
				break;
			case NKM_SUPER_ARMOR_LEVEL.NSAL_SUPER:
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_SUPERARMOR_SUPER", false));
				flag = true;
				break;
			}
			if (buffTemplet.m_fDamageTransfer > 0f)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_DAMAGE_TRANSFER", new object[]
				{
					Mathf.FloorToInt(buffTemplet.m_fDamageTransfer * 100f)
				}));
				flag = true;
			}
			if (buffTemplet.m_bGuard)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_BUFF_GUARD", false));
				flag = true;
			}
			if (buffTemplet.m_fDamageReflection > 0f)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_DAMAGE_REFLECTION", new object[]
				{
					Mathf.FloorToInt(buffTemplet.m_fDamageReflection * 100f)
				}));
				flag = true;
			}
			if (buffTemplet.m_fHealFeedback > 0f)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				float num = buffTemplet.m_fHealFeedback;
				if (buffSyncData.m_BuffStatLevel > 0)
				{
					num += buffTemplet.m_fHealFeedbackPerLevel * (float)(buffSyncData.m_BuffStatLevel - 1);
				}
				builder.Append(NKCStringTable.GetString("SI_BATTLE_HEAL_FEEDBACK", new object[]
				{
					Mathf.FloorToInt(num * 100f)
				}));
				flag = true;
			}
			if (buffTemplet.m_UnitLevel != 0)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				if (buffTemplet.m_UnitLevel > 0)
				{
					builder.AppendFormat(NKCStringTable.GetString("SI_BATTLE_BUFF_UNIT_LEVEL_UP", false), buffTemplet.m_UnitLevel);
				}
				else
				{
					builder.AppendFormat(NKCStringTable.GetString("SI_BATTLE_BUFF_UNIT_LEVEL_DOWN", false), buffTemplet.m_UnitLevel);
				}
				flag = true;
			}
			this.GetBuffDescText(ref flag, builder, buffTemplet.m_StatType1, buffTemplet.m_StatValue1, buffTemplet.m_StatFactor1, buffTemplet.m_StatAddPerLevel1, buffSyncData.m_BuffStatLevel, buffSyncData.m_OverlapCount);
			this.GetBuffDescText(ref flag, builder, buffTemplet.m_StatType2, buffTemplet.m_StatValue2, buffTemplet.m_StatFactor2, buffTemplet.m_StatAddPerLevel2, buffSyncData.m_BuffStatLevel, buffSyncData.m_OverlapCount);
			this.GetBuffDescText(ref flag, builder, buffTemplet.m_StatType3, buffTemplet.m_StatValue3, buffTemplet.m_StatFactor3, buffTemplet.m_StatAddPerLevel3, buffSyncData.m_BuffStatLevel, buffSyncData.m_OverlapCount);
			return builder.ToString();
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00057840 File Offset: 0x00055A40
		public void GetBuffDescText(ref bool bAppend, StringBuilder cStringBuilder, NKM_STAT_TYPE statType, int statValue, int statFactor, int statAddPerLevel, byte buffLevel, byte overlapCount)
		{
			if (NKMUnitStatManager.IsPercentStat(statType))
			{
				statFactor = statValue;
				statValue = 0;
			}
			if (statType != NKM_STAT_TYPE.NST_END)
			{
				if (bAppend)
				{
					cStringBuilder.Append(", ");
				}
				if (statValue != 0)
				{
					int num = statValue + statAddPerLevel * (int)(buffLevel - 1);
					num *= (int)overlapCount;
					cStringBuilder.Append(NKCUtilString.GetBuffStatValueShortString(statType, num, true));
				}
				else if (statFactor != 0)
				{
					int num2 = statFactor + statAddPerLevel * (int)(buffLevel - 1);
					num2 *= (int)overlapCount;
					cStringBuilder.Append(NKCUtilString.GetBuffStatFactorShortString(statType, num2, true));
				}
				bAppend = true;
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000578BC File Offset: 0x00055ABC
		public override bool DeleteBuff(short buffID, NKMBuffTemplet.BuffEndDTType eEndDTType)
		{
			if (this.m_dicBuffEffect.ContainsKey(buffID))
			{
				NKCASEffect nkcaseffect = this.m_dicBuffEffect[buffID];
				if (nkcaseffect.m_bEndAnim)
				{
					nkcaseffect.m_bAutoDie = true;
					nkcaseffect.PlayAnim("END", false, 1f);
				}
				else
				{
					this.m_NKCGameClient.GetNKCEffectManager().DeleteEffect(nkcaseffect.m_EffectUID);
				}
				this.m_dicBuffEffect.Remove(buffID);
			}
			if (this.m_dicBuffEffectRange.ContainsKey(buffID))
			{
				NKCASEffect nkcaseffect2 = this.m_dicBuffEffectRange[buffID];
				this.m_NKCGameClient.GetNKCEffectManager().DeleteEffect(nkcaseffect2.m_EffectUID);
				this.m_dicBuffEffectRange.Remove(buffID);
			}
			return base.DeleteBuff(buffID, eEndDTType);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00057970 File Offset: 0x00055B70
		private float HalfToFloat(ushort usHalf)
		{
			if (usHalf == 0)
			{
				return 0f;
			}
			this.m_HalfTemp.value = usHalf;
			IConvertible convertible = this.m_HalfTemp;
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			return convertible.ToSingle(invariantCulture);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000579AC File Offset: 0x00055BAC
		private void SyncUnitSyncHalfData(NKMUnitSyncData cNKMUnitSyncData)
		{
			this.m_UnitFrameData.m_fSpeedX = this.HalfToFloat(cNKMUnitSyncData.m_usSpeedX);
			this.m_UnitFrameData.m_fSpeedY = this.HalfToFloat(cNKMUnitSyncData.m_usSpeedY);
			this.m_UnitFrameData.m_fSpeedZ = this.HalfToFloat(cNKMUnitSyncData.m_usSpeedZ);
			this.m_UnitFrameData.m_fDamageSpeedX = this.HalfToFloat(cNKMUnitSyncData.m_usDamageSpeedX);
			if (cNKMUnitSyncData.m_bDamageSpeedXNegative)
			{
				this.m_UnitFrameData.m_fDamageSpeedX *= -1f;
			}
			this.m_UnitFrameData.m_fDamageSpeedZ = this.HalfToFloat(cNKMUnitSyncData.m_usDamageSpeedZ);
			this.m_UnitFrameData.m_fDamageSpeedJumpY = this.HalfToFloat(cNKMUnitSyncData.m_usDamageSpeedJumpY);
			this.m_UnitFrameData.m_fDamageSpeedKeepTimeX = this.HalfToFloat(cNKMUnitSyncData.m_usDamageSpeedKeepTimeX);
			this.m_UnitFrameData.m_fDamageSpeedKeepTimeZ = this.HalfToFloat(cNKMUnitSyncData.m_usDamageSpeedKeepTimeZ);
			this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY = this.HalfToFloat(cNKMUnitSyncData.m_usDamageSpeedKeepTimeJumpY);
			for (int i = 0; i < this.m_UnitTemplet.m_listSkillStateData.Count; i++)
			{
				if (this.m_UnitTemplet.m_listSkillStateData[i] != null)
				{
					this.SetStateCoolTimeClient(this.m_UnitTemplet.m_listSkillStateData[i].m_StateName, this.HalfToFloat(cNKMUnitSyncData.m_usSkillCoolTime));
				}
			}
			for (int j = 0; j < this.m_UnitTemplet.m_listHyperSkillStateData.Count; j++)
			{
				if (this.m_UnitTemplet.m_listHyperSkillStateData[j] != null)
				{
					this.SetStateCoolTimeClient(this.m_UnitTemplet.m_listHyperSkillStateData[j].m_StateName, this.HalfToFloat(cNKMUnitSyncData.m_usHyperSkillCoolTime));
				}
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00057B54 File Offset: 0x00055D54
		protected void SetStateCoolTimeClient(string stateName, float fCoolTime)
		{
			if (stateName.Length <= 1)
			{
				return;
			}
			NKMUnitState unitState = base.GetUnitState(stateName, true);
			if (unitState != null)
			{
				if (!this.m_dicStateCoolTime.ContainsKey((int)unitState.m_StateID))
				{
					NKMStateCoolTime nkmstateCoolTime = (NKMStateCoolTime)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime, "", "", false);
					nkmstateCoolTime.m_CoolTime = fCoolTime;
					this.m_dicStateCoolTime.Add((int)unitState.m_StateID, nkmstateCoolTime);
					return;
				}
				this.m_dicStateCoolTime[(int)unitState.m_StateID].m_CoolTime = fCoolTime;
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00057BDD File Offset: 0x00055DDD
		public void MiniMapFaceWarrning()
		{
			if (this.m_NKCASUnitMiniMapFace != null)
			{
				this.m_NKCASUnitMiniMapFace.Warnning();
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00057BF2 File Offset: 0x00055DF2
		public GameObject GetObjectUnitSkillGuage()
		{
			NKCUIComSkillGauge skill_GAUGE = this.m_SKILL_GAUGE;
			if (skill_GAUGE == null)
			{
				return null;
			}
			return skill_GAUGE.GetSkillGauge();
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00057C05 File Offset: 0x00055E05
		public GameObject GetObjectUnitHyperGuage()
		{
			NKCUIComSkillGauge skill_GAUGE = this.m_SKILL_GAUGE;
			if (skill_GAUGE == null)
			{
				return null;
			}
			return skill_GAUGE.GetHyperGauge();
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00057C18 File Offset: 0x00055E18
		public GameObject GetObjectUnitHyper()
		{
			return this.m_UNIT_SKILL;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00057C20 File Offset: 0x00055E20
		public bool IsMyTeam()
		{
			return this.m_NKCGameClient.GetMyTeamData().m_eNKM_TEAM_TYPE == base.GetUnitDataGame().m_NKM_TEAM_TYPE;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00057C42 File Offset: 0x00055E42
		public void UseManualSkill()
		{
			this.m_NKCGameClient.Send_Packet_GAME_USE_UNIT_SKILL_REQ(base.GetUnitDataGame().m_GameUnitUID);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00057C5C File Offset: 0x00055E5C
		public void OnRecv(NKMGameSyncDataSimple_Unit cNKMGameSyncDataSimple_Unit)
		{
			this.m_UnitSyncData.m_TargetUID = cNKMGameSyncDataSimple_Unit.m_TargetUID;
			this.m_UnitSyncData.m_SubTargetUID = cNKMGameSyncDataSimple_Unit.m_SubTargetUID;
			this.m_UnitSyncData.m_bRight = cNKMGameSyncDataSimple_Unit.m_bRight;
			this.SyncBuffData(cNKMGameSyncDataSimple_Unit.m_dicBuffData);
			this.SyncEventMark(cNKMGameSyncDataSimple_Unit.m_listNKM_UNIT_EVENT_MARK);
			this.SyncStatusTimeData(cNKMGameSyncDataSimple_Unit.m_listStatusTimeData);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00057CC0 File Offset: 0x00055EC0
		public void OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_ACK cNKMPacket_GAME_USE_UNIT_SKILL_ACK)
		{
			if (cNKMPacket_GAME_USE_UNIT_SKILL_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				this.m_fManualSkillUseAck = 3f;
				this.m_bManualSkillUseStart = false;
				this.m_bManualSkillUseStateID = (byte)cNKMPacket_GAME_USE_UNIT_SKILL_ACK.skillStateID;
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00057CEC File Offset: 0x00055EEC
		public static NKCASUnitSpineSprite OpenUnitSpineSprite(NKMUnitData unitData, bool bSub, bool bAsync)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData);
			if (skinTemplet != null)
			{
				return NKCUnitClient.OpenUnitSpineSprite(skinTemplet, bSub, bAsync);
			}
			return NKCUnitClient.OpenUnitSpineSprite(NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID), bSub, bAsync);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00057D20 File Offset: 0x00055F20
		public static NKCASUnitSpineSprite OpenUnitSpineSprite(NKMUnitTempletBase unitTempletBase, bool bSub, bool bAsync)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			NKCASUnitSpineSprite nkcasunitSpineSprite;
			if (bSub && !string.IsNullOrEmpty(unitTempletBase.m_SpriteNameSub))
			{
				nkcasunitSpineSprite = (NKCASUnitSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSpineSprite, unitTempletBase.m_SpriteBundleNameSub, unitTempletBase.m_SpriteNameSub, bAsync);
				if (!string.IsNullOrEmpty(unitTempletBase.m_SpriteMaterialNameSub))
				{
					nkcasunitSpineSprite.SetReplaceMatResource(unitTempletBase.m_SpriteBundleNameSub, unitTempletBase.m_SpriteMaterialNameSub, bAsync);
				}
				else
				{
					nkcasunitSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			else
			{
				nkcasunitSpineSprite = (NKCASUnitSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSpineSprite, unitTempletBase.m_SpriteBundleName, unitTempletBase.m_SpriteName, bAsync);
				if (!string.IsNullOrEmpty(unitTempletBase.m_SpriteMaterialName))
				{
					nkcasunitSpineSprite.SetReplaceMatResource(unitTempletBase.m_SpriteBundleName, unitTempletBase.m_SpriteMaterialName, bAsync);
				}
				else
				{
					nkcasunitSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			return nkcasunitSpineSprite;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00057DF4 File Offset: 0x00055FF4
		public static NKCASUnitSpineSprite OpenUnitSpineSprite(NKMSkinTemplet skinTemplet, bool bSub, bool bAsync)
		{
			if (skinTemplet == null)
			{
				return null;
			}
			NKCASUnitSpineSprite nkcasunitSpineSprite;
			if (bSub && !string.IsNullOrEmpty(skinTemplet.m_SpriteNameSub))
			{
				nkcasunitSpineSprite = (NKCASUnitSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSpineSprite, skinTemplet.m_SpriteBundleNameSub, skinTemplet.m_SpriteNameSub, bAsync);
				if (!string.IsNullOrEmpty(skinTemplet.m_SpriteMaterialNameSub))
				{
					nkcasunitSpineSprite.SetReplaceMatResource(skinTemplet.m_SpriteBundleNameSub, skinTemplet.m_SpriteMaterialNameSub, bAsync);
				}
				else
				{
					nkcasunitSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			else
			{
				nkcasunitSpineSprite = (NKCASUnitSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSpineSprite, skinTemplet.m_SpriteBundleName, skinTemplet.m_SpriteName, bAsync);
				if (!string.IsNullOrEmpty(skinTemplet.m_SpriteMaterialName))
				{
					nkcasunitSpineSprite.SetReplaceMatResource(skinTemplet.m_SpriteBundleName, skinTemplet.m_SpriteMaterialName, bAsync);
				}
				else
				{
					nkcasunitSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			return nkcasunitSpineSprite;
		}

		// Token: 0x04000E85 RID: 3717
		private NKCGameClient m_NKCGameClient;

		// Token: 0x04000E86 RID: 3718
		protected bool m_bLoadComplete;

		// Token: 0x04000E87 RID: 3719
		private NKCAssetInstanceData m_UnitObject;

		// Token: 0x04000E88 RID: 3720
		private NKCUnitTouchObject m_NKCUnitTouchObject;

		// Token: 0x04000E89 RID: 3721
		private GameObject m_SpriteObject;

		// Token: 0x04000E8A RID: 3722
		private GameObject m_MainSpriteObject;

		// Token: 0x04000E8B RID: 3723
		private GameObject m_UNIT_GAGE;

		// Token: 0x04000E8C RID: 3724
		private RectTransform m_UNIT_GAGE_RectTransform;

		// Token: 0x04000E8D RID: 3725
		private GameObject m_UNIT_SKILL;

		// Token: 0x04000E8E RID: 3726
		private GameObject m_SKILL_BUTTON;

		// Token: 0x04000E8F RID: 3727
		private NKCUIComButton m_SKILL_BUTTON_Btn;

		// Token: 0x04000E90 RID: 3728
		private GameObject m_UNIT_GAGE_PANEL;

		// Token: 0x04000E91 RID: 3729
		private NKCUIComHealthBar m_UnitHealthBar;

		// Token: 0x04000E92 RID: 3730
		private GameObject m_UNIT_HP_GAGE;

		// Token: 0x04000E93 RID: 3731
		private float m_fOrgGageSize = 150f;

		// Token: 0x04000E94 RID: 3732
		private NKCUIComSkillGauge m_SKILL_GAUGE;

		// Token: 0x04000E95 RID: 3733
		private GameObject m_UNIT_LEVEL_BG;

		// Token: 0x04000E96 RID: 3734
		private Image m_UNIT_LEVEL_BG_Image;

		// Token: 0x04000E97 RID: 3735
		private GameObject m_UNIT_LEVEL;

		// Token: 0x04000E98 RID: 3736
		private RectTransform m_UNIT_LEVEL_RectTransform;

		// Token: 0x04000E99 RID: 3737
		private GameObject m_UNIT_LEVEL_TEXT;

		// Token: 0x04000E9A RID: 3738
		private NKCUIComTextUnitLevel m_UNIT_LEVEL_TEXT_Text;

		// Token: 0x04000E9B RID: 3739
		private GameObject m_UNIT_ARMOR_TYPE;

		// Token: 0x04000E9C RID: 3740
		private Image m_UNIT_ARMOR_TYPE_Image;

		// Token: 0x04000E9D RID: 3741
		private GameObject m_UNIT_ASSIST;

		// Token: 0x04000E9E RID: 3742
		private NKMTrackingFloat m_GageWide = new NKMTrackingFloat();

		// Token: 0x04000E9F RID: 3743
		private NKMTrackingFloat m_GageOffsetPosX = new NKMTrackingFloat();

		// Token: 0x04000EA0 RID: 3744
		private NKMTrackingFloat m_GageOffsetPosY = new NKMTrackingFloat();

		// Token: 0x04000EA1 RID: 3745
		private List<NKCUnitBuffIcon> m_listNKCUnitBuffIcon = new List<NKCUnitBuffIcon>();

		// Token: 0x04000EA2 RID: 3746
		private NKMTrackingFloat m_ObjPosX = new NKMTrackingFloat();

		// Token: 0x04000EA3 RID: 3747
		private NKMTrackingFloat m_ObjPosZ = new NKMTrackingFloat();

		// Token: 0x04000EA4 RID: 3748
		private NKCAnimSpine m_NKCAnimSpine = new NKCAnimSpine();

		// Token: 0x04000EA5 RID: 3749
		private bool m_bDissolveEnable;

		// Token: 0x04000EA6 RID: 3750
		private NKMTrackingFloat m_DissolveFactor = new NKMTrackingFloat();

		// Token: 0x04000EA7 RID: 3751
		private List<int> m_listSoundUID = new List<int>();

		// Token: 0x04000EA8 RID: 3752
		private Color m_ColorTemp;

		// Token: 0x04000EA9 RID: 3753
		private NKMMinMaxVec3 m_TempMinMaxVec3 = new NKMMinMaxVec3(0f, 0f, 0f, 0f, 0f, 0f);

		// Token: 0x04000EAA RID: 3754
		private LinkedList<NKCASEffect> m_llEffect = new LinkedList<NKCASEffect>();

		// Token: 0x04000EAB RID: 3755
		private Dictionary<NKM_UNIT_STATUS_EFFECT, NKCASEffect> m_dicStatusEffect = new Dictionary<NKM_UNIT_STATUS_EFFECT, NKCASEffect>();

		// Token: 0x04000EAC RID: 3756
		private Dictionary<string, NKCASEffect> m_dicLoadEffectTemp = new Dictionary<string, NKCASEffect>();

		// Token: 0x04000EAD RID: 3757
		private Dictionary<string, NKCAssetResourceData> m_dicLoadSoundTemp = new Dictionary<string, NKCAssetResourceData>();

		// Token: 0x04000EAE RID: 3758
		private Dictionary<short, NKCASEffect> m_dicBuffEffectRange = new Dictionary<short, NKCASEffect>();

		// Token: 0x04000EAF RID: 3759
		private Dictionary<short, NKCASEffect> m_dicBuffEffect = new Dictionary<short, NKCASEffect>();

		// Token: 0x04000EB0 RID: 3760
		private LinkedList<NKCASEffect> m_llBuffTextEffect = new LinkedList<NKCASEffect>();

		// Token: 0x04000EB1 RID: 3761
		private Vector3 m_Vector3Temp = new Vector3(0f, 0f, 0f);

		// Token: 0x04000EB2 RID: 3762
		private Vector3 m_Vector3Temp2 = new Vector3(0f, 0f, 0f);

		// Token: 0x04000EB3 RID: 3763
		private float m_fGagePosX;

		// Token: 0x04000EB4 RID: 3764
		private float m_fGagePosY;

		// Token: 0x04000EB5 RID: 3765
		private byte m_BuffDescTextPosYIndex;

		// Token: 0x04000EB6 RID: 3766
		private NKCASUnitSprite m_NKCASUnitSprite;

		// Token: 0x04000EB7 RID: 3767
		private NKCASUnitSpineSprite m_NKCASUnitSpineSprite;

		// Token: 0x04000EB8 RID: 3768
		private NKCASUnitShadow m_NKCASUnitShadow;

		// Token: 0x04000EB9 RID: 3769
		private NKCASUnitMiniMapFace m_NKCASUnitMiniMapFace;

		// Token: 0x04000EBA RID: 3770
		private NKCUnitViewer m_NKCUnitViewer = new NKCUnitViewer();

		// Token: 0x04000EBB RID: 3771
		private NKCASDangerChargeUI m_NKCASDangerChargeUI;

		// Token: 0x04000EBC RID: 3772
		private NKC2DMotionAfterImage m_NKCMotionAfterImage = new NKC2DMotionAfterImage();

		// Token: 0x04000EBD RID: 3773
		private Half m_HalfTemp;

		// Token: 0x04000EBE RID: 3774
		protected StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04000EBF RID: 3775
		private float m_fManualSkillUseAck;

		// Token: 0x04000EC0 RID: 3776
		private bool m_bManualSkillUseStart;

		// Token: 0x04000EC1 RID: 3777
		private byte m_bManualSkillUseStateID;

		// Token: 0x04000EC2 RID: 3778
		private int m_BuffUnitLevelLastUpdate;

		// Token: 0x04000EC3 RID: 3779
		private bool m_bHyperCutinLoaded;

		// Token: 0x04000EC4 RID: 3780
		private HashSet<NKM_UNIT_STATUS_EFFECT> m_hsEffectToRemove = new HashSet<NKM_UNIT_STATUS_EFFECT>();
	}
}
