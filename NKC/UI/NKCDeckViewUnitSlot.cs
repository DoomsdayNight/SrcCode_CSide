using System;
using System.Collections.Generic;
using ClientPacket.Common;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000969 RID: 2409
	public class NKCDeckViewUnitSlot : MonoBehaviour
	{
		// Token: 0x06006097 RID: 24727 RVA: 0x001E2F9A File Offset: 0x001E119A
		public bool GetInDrag()
		{
			return this.m_bInDrag;
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x001E2FA2 File Offset: 0x001E11A2
		public void SetEnableShowBan(bool bSet)
		{
			this.m_bEnableShowBan = bSet;
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x001E2FAB File Offset: 0x001E11AB
		public void SetEnableShowUpUnit(bool bSet)
		{
			this.m_bEnableShowUpUnit = bSet;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x001E2FB4 File Offset: 0x001E11B4
		public static NKCDeckViewUnitSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_UNIT_SLOT_DECK", "NKM_UI_DECK_VIEW_UNIT_SLOT", false, null);
			NKCDeckViewUnitSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCDeckViewUnitSlot>();
			if (component == null)
			{
				Debug.LogError("NKCDeckViewUnitSlot Prefab null!");
				return null;
			}
			component.m_instace = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x001E3054 File Offset: 0x001E1254
		public void Init(int index, bool bEnableDrag = true)
		{
			this.m_Index = index;
			this.m_RectTransform = base.GetComponent<RectTransform>();
			this.m_rectMain = this.m_objMain.GetComponent<RectTransform>();
			this.m_animatorMain = this.m_objMain.GetComponent<Animator>();
			this.m_PosXOrg = this.m_rectMain.position.x;
			this.m_PosYOrg = this.m_rectMain.position.y;
			this.m_bEnableDrag = bEnableDrag;
			if (!this.m_bEnableDrag)
			{
				this.m_NKCUIComDrag.enabled = false;
				this.m_NKCUIComButton.m_bSelect = false;
				this.m_NKCUIComButton.m_bSelectByClick = false;
			}
			this.SetExp(null);
			this.InitTransform();
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x001E3104 File Offset: 0x001E1304
		public void SetEnemyData(NKMUnitTempletBase cNKMUnitTempletBase, NKCEnemyData cNKMEnemyData)
		{
			if (cNKMEnemyData == null)
			{
				return;
			}
			this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
			NKCUtil.SetGameobjectActive(this.m_objLeader, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_COST_BG_Panel, false);
			NKCUtil.SetGameobjectActive(this.m_objArrow, false);
			NKCUtil.SetGameobjectActive(this.m_comStarRank, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_LEVEL_GAUGE_SLIDER, false);
			this.m_textCardCost.text = "";
			Sprite backPanelImage = this.GetBackPanelImage(NKM_UNIT_GRADE.NUG_N);
			this.m_imgBGPanel.sprite = backPanelImage;
			this.m_imgBgAddPanel.sprite = backPanelImage;
			if (backPanelImage == null)
			{
				Debug.LogError("SetEnemyData m_spPanelN: null");
			}
			if (this.m_imgBGPanel.sprite == null)
			{
				Debug.LogError("SetEnemyData m_imgBGPanel.sprite: null");
			}
			if (cNKMUnitTempletBase != null)
			{
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitTempletBase);
				this.m_imgUnitPanel.sprite = sprite;
				this.m_textLevel.SetText(cNKMEnemyData.m_Level.ToString(), false, Array.Empty<Text>());
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS, cNKMEnemyData.m_NKM_BOSS_TYPE >= NKM_BOSS_TYPE.NBT_DUNGEON_BOSS);
				if (cNKMEnemyData.m_NKM_BOSS_TYPE == NKM_BOSS_TYPE.NBT_DUNGEON_BOSS)
				{
					this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_POPUP_ENEMY_ICON", false);
				}
				else if (cNKMEnemyData.m_NKM_BOSS_TYPE == NKM_BOSS_TYPE.NBT_WARFARE_BOSS)
				{
					this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_POPUP_ENEMY_BOSS_ICON", false);
				}
				Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(cNKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgClassType, orLoadUnitRoleIcon, true);
				Sprite orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(cNKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgAttackType, orLoadUnitAttackTypeIcon, true);
				NKCUtil.SetGameobjectActive(this.m_objUnitMain, true);
				NKCUtil.SetGameobjectActive(this.m_imgUnitPanel, true);
				NKCUtil.SetGameobjectActive(this.m_imgUnitGrayPanel, false);
				NKCUtil.SetGameobjectActive(this.m_textLevel, true);
				NKCUtil.SetGameobjectActive(this.m_imgBgAddPanel, false);
				NKCUtil.SetGameobjectActive(this.m_imgUnitAddPanel, false);
				NKCUtil.SetGameobjectActive(this.m_imgSlotCardBlur, false);
				NKCUtil.SetGameobjectActive(this.m_objRearm, cNKMUnitTempletBase.IsRearmUnit);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitMain, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS, false);
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x001E32F8 File Offset: 0x001E14F8
		public void SetData(NKMUnitData cNKMUnitData, NKCUIDeckViewer.DeckViewerOption deckViewerOption, bool bEnableButton = true)
		{
			if (deckViewerOption.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.WorldMapMissionDeckSelect)
			{
				if (cNKMUnitData != null)
				{
					NKMWorldMapManager.WorldMapLeaderState unitWorldMapLeaderState = NKMWorldMapManager.GetUnitWorldMapLeaderState(NKCScenManager.CurrentUserData(), cNKMUnitData.m_UnitUID, deckViewerOption.WorldMapMissionCityID);
					this.SetCityLeaderTag(unitWorldMapLeaderState);
				}
				else
				{
					this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
				}
				this._SetData(cNKMUnitData, bEnableButton);
				return;
			}
			if (deckViewerOption.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
				this._SetData(cNKMUnitData, bEnableButton);
				this.SetLeader(this.m_bLeader, false);
				return;
			}
			this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
			this._SetData(cNKMUnitData, bEnableButton);
			bool bLeader = false;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData != null && cNKMUnitData != null)
			{
				NKMDeckData deckData = armyData.GetDeckData(deckViewerOption.DeckIndex);
				bLeader = (deckData != null && cNKMUnitData.m_UnitUID == deckData.GetLeaderUnitUID());
			}
			this.SetLeader(bLeader, false);
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x001E33B4 File Offset: 0x001E15B4
		public void SetData(NKMUnitData cNKMUnitData, bool bEnableButton = true)
		{
			this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
			this._SetData(cNKMUnitData, bEnableButton);
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x001E33C8 File Offset: 0x001E15C8
		private void _SetData(NKMUnitData cNKMUnitData, bool bEnableButton = true)
		{
			if (this.m_NKCUIComButton != null)
			{
				this.m_NKCUIComButton.enabled = bEnableButton;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_COST_BG_Panel, true);
			NKCUtil.SetGameobjectActive(this.m_comStarRank, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_LEVEL_GAUGE_SLIDER, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS, false);
			this.m_NKMUnitData = cNKMUnitData;
			if (cNKMUnitData != null && this.m_NKMUnitData.m_UnitID != 0)
			{
				this.m_NKMUnitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMUnitData.m_UnitID);
				Sprite backPanelImage = this.GetBackPanelImage(this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE);
				NKCUtil.SetImageSprite(this.m_imgBGPanel, backPanelImage, false);
				NKCUtil.SetImageSprite(this.m_imgBgAddPanel, backPanelImage, false);
				NKCUtil.SetAwakenFX(this.m_animAwakenFX, this.m_NKMUnitTempletBase);
				this.m_comStarRank.SetStarRank(cNKMUnitData);
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitData);
				this.m_imgUnitPanel.sprite = sprite;
				this.m_imgUnitAddPanel.sprite = sprite;
				this.m_imgUnitGrayPanel.sprite = sprite;
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(this.m_NKMUnitTempletBase.m_UnitID);
				if (unitStatTemplet != null)
				{
					bool flag = NKCBanManager.IsBanUnit(this.m_NKMUnitTempletBase.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL);
					bool flag2 = NKCBanManager.IsUpUnit(this.m_NKMUnitTempletBase.m_UnitID);
					if (flag && this.m_bEnableShowBan)
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
						this.m_textCardCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_BAN_COST, respawnCost.ToString());
						NKCUtil.SetGameobjectActive(this.m_objBan, true);
						int unitBanLevel = NKCBanManager.GetUnitBanLevel(this.m_NKMUnitTempletBase.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL);
						NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, unitBanLevel));
						this.m_lbBanLevel.color = Color.red;
					}
					else if (flag2 && this.m_bEnableShowUpUnit)
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
						this.m_textCardCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_UP_COST, respawnCost.ToString());
						NKCUtil.SetGameobjectActive(this.m_objBan, true);
						int unitUpLevel = NKCBanManager.GetUnitUpLevel(this.m_NKMUnitTempletBase.m_UnitID);
						NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_UP_LEVEL_ONE_PARAM, unitUpLevel));
						this.m_lbBanLevel.color = NKCBanManager.UP_COLOR;
					}
					else
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(false, false, null, null);
						this.m_textCardCost.text = respawnCost.ToString();
						NKCUtil.SetGameobjectActive(this.m_objBan, false);
					}
				}
				this.m_textLevel.SetLevel(cNKMUnitData, 0, Array.Empty<Text>());
				Sprite orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(cNKMUnitData, true);
				NKCUtil.SetImageSprite(this.m_imgAttackType, orLoadUnitAttackTypeIcon, true);
				Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(this.m_NKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgClassType, orLoadUnitRoleIcon, true);
				this.m_objUnitMain.SetActive(true);
				this.m_imgUnitPanel.gameObject.SetActive(true);
				this.m_imgUnitGrayPanel.gameObject.SetActive(false);
				this.m_objArrow.SetActive(false);
				this.m_objLeader.SetActive(false);
				this.m_textLevel.gameObject.SetActive(true);
				this.m_imgBgAddPanel.gameObject.SetActive(false);
				this.m_imgUnitAddPanel.gameObject.SetActive(false);
				this.m_imgSlotCardBlur.gameObject.SetActive(false);
				NKCUtil.SetGameobjectActive(this.m_objSeized, cNKMUnitData.IsSeized);
				bool bValue = true;
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
				if (unitTempletBase != null)
				{
					bValue = !unitTempletBase.m_bMonster;
				}
				NKCUtil.SetGameobjectActive(this.m_objRearm, unitTempletBase != null && unitTempletBase.IsRearmUnit);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_COST_BG_Panel, bValue);
				NKCUtil.SetGameobjectActive(this.m_textCardCost.gameObject, bValue);
				NKCUtil.SetGameobjectActive(this.m_comStarRank, bValue);
				NKCUtil.SetGameobjectActive(this.m_tacticUpdateLvSlot.gameObject, true);
				this.m_tacticUpdateLvSlot.SetLevel(this.m_NKMUnitData.tacticLevel, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
				Sprite emptyPanelImage = this.GetEmptyPanelImage();
				this.m_imgBGPanel.sprite = emptyPanelImage;
				this.m_imgBgAddPanel.sprite = emptyPanelImage;
				NKCUtil.SetGameobjectActive(this.m_comStarRank, false);
				this.m_objUnitMain.SetActive(false);
				this.m_imgBgAddPanel.gameObject.SetActive(false);
				this.m_imgUnitPanel.gameObject.SetActive(false);
				this.m_imgUnitAddPanel.gameObject.SetActive(false);
				this.m_imgUnitGrayPanel.gameObject.SetActive(false);
				this.m_objArrow.SetActive(false);
				this.m_objLeader.SetActive(false);
				this.m_textCardCost.gameObject.SetActive(false);
				this.m_textLevel.gameObject.SetActive(false);
				NKCUtil.SetGameobjectActive(this.m_imgClassType, false);
				NKCUtil.SetGameobjectActive(this.m_imgAttackType, false);
				NKCUtil.SetGameobjectActive(this.m_objSeized, false);
				NKCUtil.SetGameobjectActive(this.m_objRearm, false);
				NKCUtil.SetAwakenFX(this.m_animAwakenFX, null);
				NKCUtil.SetGameobjectActive(this.m_tacticUpdateLvSlot.gameObject, false);
			}
			this.ResetEffect();
			this.ResetPos(false);
			this.SetExp(cNKMUnitData);
			this.InitTransform();
			this.ButtonDeSelect(false, false);
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x001E38E4 File Offset: 0x001E1AE4
		public void SetUpBanData(NKMUnitData unitData, Dictionary<int, NKMBanData> dicBbanData, Dictionary<int, NKMUnitUpData> dicUpData, bool bLeader)
		{
			if (unitData == null || (dicBbanData == null && dicUpData == null))
			{
				return;
			}
			int num = 0;
			if (dicBbanData != null && dicBbanData.Count > 0 && dicBbanData.ContainsKey(unitData.m_UnitID))
			{
				num = (int)dicBbanData[unitData.m_UnitID].m_BanLevel;
			}
			int num2 = 0;
			if (dicUpData != null && dicUpData.Count > 0 && dicUpData.ContainsKey(unitData.m_UnitID))
			{
				num2 = (int)dicUpData[unitData.m_UnitID].upLevel;
			}
			if (num == 0 && num2 == 0)
			{
				return;
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet == null)
			{
				return;
			}
			if (num > 0)
			{
				int respawnCost = unitStatTemplet.GetRespawnCost(true, bLeader, dicBbanData, null);
				NKCUtil.SetLabelText(this.m_textCardCost, string.Format(NKCUtilString.GET_STRING_UNIT_BAN_COST, respawnCost.ToString()));
				NKCUtil.SetGameobjectActive(this.m_objBan, true);
				NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, num));
				NKCUtil.SetLabelTextColor(this.m_lbBanLevel, Color.red);
				return;
			}
			if (num2 > 0)
			{
				int respawnCost2 = unitStatTemplet.GetRespawnCost(true, bLeader, null, dicUpData);
				NKCUtil.SetLabelText(this.m_textCardCost, string.Format(NKCUtilString.GET_STRING_UNIT_UP_COST, respawnCost2.ToString()));
				NKCUtil.SetGameobjectActive(this.m_objBan, true);
				NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_UP_LEVEL_ONE_PARAM, num2));
				NKCUtil.SetLabelTextColor(this.m_lbBanLevel, NKCBanManager.UP_COLOR);
				return;
			}
			int respawnCost3 = unitStatTemplet.GetRespawnCost(false, false, null, null);
			NKCUtil.SetLabelText(this.m_textCardCost, respawnCost3.ToString());
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x001E3A61 File Offset: 0x001E1C61
		public void SetPrivate()
		{
			this.SetData(null, false);
			this.SetLeader(false, false);
			this.m_imgBGPanel.sprite = this.GetPrivatePanelImage();
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x001E3A84 File Offset: 0x001E1C84
		public void InitTransform()
		{
			this.m_PosX.SetNowValue(this.m_PosXOrg);
			this.m_PosY.SetNowValue(this.m_PosYOrg);
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x001E3AA8 File Offset: 0x001E1CA8
		private void SetExp(NKMUnitData unitData)
		{
			if (unitData != null)
			{
				bool bValue = false;
				float endValue;
				if (unitData.m_UnitID != 0)
				{
					if (NKCExpManager.GetUnitMaxLevel(unitData) == unitData.m_UnitLevel)
					{
						endValue = 1f;
						bValue = true;
					}
					else
					{
						endValue = NKCExpManager.GetUnitNextLevelExpProgress(unitData);
					}
				}
				else
				{
					endValue = 0f;
				}
				this.m_sliderExp.value = 0f;
				this.m_sliderExp.DOValue(endValue, 2f, false).SetEase(Ease.OutCubic);
				NKCUtil.SetGameobjectActive(this.m_objExpMax, bValue);
				return;
			}
			this.m_sliderExp.value = 0f;
			NKCUtil.SetGameobjectActive(this.m_objExpMax, false);
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x001E3B3C File Offset: 0x001E1D3C
		public void Update()
		{
			if (this.m_bEnableDrag)
			{
				this.m_PosX.Update(Time.deltaTime);
				this.m_PosY.Update(Time.deltaTime);
			}
			if (this.m_bEnableDrag)
			{
				this.UpdatePos();
			}
			this.fTimeTest -= Time.deltaTime;
			if (this.fTimeTest < 0f)
			{
				if (this.m_imgBGPanel.sprite == null)
				{
					Debug.LogError("SetEnemyData m_imgBGPanel.sprite: null");
				}
				this.fTimeTest = 3f;
			}
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x001E3BC8 File Offset: 0x001E1DC8
		private Sprite GetBackPanelImage(NKM_UNIT_GRADE unitGrade)
		{
			switch (unitGrade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_N", false);
			case NKM_UNIT_GRADE.NUG_R:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_R", false);
			case NKM_UNIT_GRADE.NUG_SR:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_SR", false);
			case NKM_UNIT_GRADE.NUG_SSR:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_SSR", false);
			default:
				return null;
			}
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x001E3C32 File Offset: 0x001E1E32
		private Sprite GetEmptyPanelImage()
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_EMPTY", false);
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x001E3C44 File Offset: 0x001E1E44
		private Sprite GetPrivatePanelImage()
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_ASYNC_PRIVATE", false);
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x001E3C58 File Offset: 0x001E1E58
		public void UpdatePos()
		{
			Vector3 position = this.m_rectMain.position;
			position.Set(this.m_PosX.GetNowValue(), this.m_PosY.GetNowValue(), position.z);
			this.m_rectMain.position = position;
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x001E3CA0 File Offset: 0x001E1EA0
		public void SetSelectable(bool bSelectable)
		{
			if (bSelectable)
			{
				this.ButtonSelect();
				return;
			}
			this.ButtonDeSelect(true, true);
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x001E3CB4 File Offset: 0x001E1EB4
		public void SetLeader(bool bLeader, bool bEffect)
		{
			if (bLeader)
			{
				this.m_objLeader.SetActive(true);
				if (bEffect)
				{
					this.PlayEffect();
				}
			}
			else
			{
				this.m_objLeader.SetActive(false);
			}
			this.m_bLeader = bLeader;
			if (this.m_NKMUnitTempletBase != null)
			{
				bool flag = NKCBanManager.IsBanUnit(this.m_NKMUnitTempletBase.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL);
				bool flag2 = NKCBanManager.IsUpUnit(this.m_NKMUnitTempletBase.m_UnitID);
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(this.m_NKMUnitTempletBase.m_UnitID);
				if (unitStatTemplet != null)
				{
					int respawnCost;
					if (flag && this.m_bEnableShowBan)
					{
						respawnCost = unitStatTemplet.GetRespawnCost(true, bLeader, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
						this.m_textCardCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_BAN_COST, respawnCost.ToString());
						return;
					}
					if (flag2 && this.m_bEnableShowUpUnit)
					{
						respawnCost = unitStatTemplet.GetRespawnCost(true, bLeader, null, NKCBanManager.m_dicNKMUpData);
						this.m_textCardCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_UP_COST, respawnCost.ToString());
						return;
					}
					if (bLeader)
					{
						respawnCost = unitStatTemplet.GetRespawnCost(false, bLeader, null, null);
						this.m_textCardCost.text = string.Format("<color=#FFCD07>{0}</color>", respawnCost.ToString());
						return;
					}
					respawnCost = unitStatTemplet.GetRespawnCost(false, bLeader, null, null);
					this.m_textCardCost.text = respawnCost.ToString();
				}
			}
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x001E3DEC File Offset: 0x001E1FEC
		public void SetLeagueBan(bool bBanUnit)
		{
			NKCUtil.SetGameobjectActive(this.m_objLeagueBan, bBanUnit);
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x001E3DFA File Offset: 0x001E1FFA
		public void SetEnableShowLevelText(bool bShow)
		{
			NKCUtil.SetGameobjectActive(this.m_lbTextLevelDesc, bShow);
			NKCUtil.SetGameobjectActive(this.m_textLevel, bShow);
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x001E3E14 File Offset: 0x001E2014
		public void SetLeaguePickEnable(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objLeaguePickEnable, bEnable);
			if (this.m_doTweenAnimation != null && bEnable)
			{
				DOTween.Sequence().Restart(true, -1f);
				this.m_doTweenAnimation.DOGoto(0f, true);
			}
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x001E3E60 File Offset: 0x001E2060
		public void PlayEffect()
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.m_animatorMain.Play("NKM_UI_GAME_DECK_CARD_READY", -1, 0f);
			}
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x001E3E85 File Offset: 0x001E2085
		private void ResetEffect()
		{
			this.m_imgBgAddPanel.gameObject.SetActive(false);
			this.m_imgUnitAddPanel.gameObject.SetActive(false);
			this.m_imgSlotCardBlur.gameObject.SetActive(false);
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x001E3EBA File Offset: 0x001E20BA
		public void ButtonSelect()
		{
			this.m_NKCUIComButton.Select(true, false, false);
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x001E3ECA File Offset: 0x001E20CA
		public void ButtonDeSelect(bool bForce = false, bool bImmediate = false)
		{
			this.m_NKCUIComButton.Select(false, bForce, bImmediate);
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x001E3EDA File Offset: 0x001E20DA
		public void BeginDrag()
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objCityLeaderRoot, false);
			this.m_bInDrag = true;
			this.m_objMain.transform.SetParent(NKCUIManager.Get_NUF_DRAG().transform);
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x001E3F14 File Offset: 0x001E2114
		public void Drag(PointerEventData eventData)
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			if (this.m_bInDrag)
			{
				Vector3 vector = NKCCamera.GetSubUICamera().ScreenToWorldPoint(eventData.position);
				this.m_PosX.SetNowValue(vector.x);
				this.m_PosY.SetNowValue(vector.y);
			}
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x001E3F6A File Offset: 0x001E216A
		public void EndDrag()
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			this.SetCityLeaderTag(this.m_eWorldmapState);
			this.m_bInDrag = false;
			this.ReturnToParent();
			this.ReturnToOrg();
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x001E3F94 File Offset: 0x001E2194
		public void EnableDrag(bool bEnalbe)
		{
			this.m_bEnableDrag = bEnalbe;
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x001E3F9D File Offset: 0x001E219D
		public void Swap(NKCDeckViewUnitSlot cNKCDeckViewUnitSlot)
		{
			this.m_PosX.SetTracking(cNKCDeckViewUnitSlot.m_PosXOrg, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_PosY.SetTracking(cNKCDeckViewUnitSlot.m_PosYOrg, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x001E3FCD File Offset: 0x001E21CD
		public void ReturnToParent()
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			this.m_objMain.transform.SetParent(base.transform);
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x001E3FEE File Offset: 0x001E21EE
		public void ReturnToOrg()
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			this.m_PosX.SetTracking(this.m_PosXOrg, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_PosY.SetTracking(this.m_PosYOrg, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x001E4028 File Offset: 0x001E2228
		public bool IsEnter(Vector3 incomingSlotPosition)
		{
			return incomingSlotPosition.x <= this.m_RectTransform.position.x + this.m_RectTransform.rect.width * 0.5f && incomingSlotPosition.x >= this.m_RectTransform.position.x - this.m_RectTransform.rect.width * 0.5f && incomingSlotPosition.y <= this.m_RectTransform.position.y + this.m_RectTransform.rect.height * 0.5f && incomingSlotPosition.y >= this.m_RectTransform.position.y - this.m_RectTransform.rect.height * 0.5f;
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x001E4108 File Offset: 0x001E2308
		public void ResetPos(bool bImmediate = false)
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			this.m_PosXOrg = this.m_RectTransform.position.x;
			this.m_PosYOrg = this.m_RectTransform.position.y;
			this.m_PosX.SetTracking(this.m_PosXOrg, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_PosY.SetTracking(this.m_PosYOrg, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			if (bImmediate)
			{
				this.m_PosX.SetNowValue(this.m_PosXOrg);
				this.m_PosY.SetNowValue(this.m_PosYOrg);
			}
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x001E419D File Offset: 0x001E239D
		public void OnDisable()
		{
			this.ResetEffect();
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x001E41A8 File Offset: 0x001E23A8
		public void SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState eWorldmapState)
		{
			this.m_eWorldmapState = eWorldmapState;
			bool flag = false;
			bool flag2;
			switch (eWorldmapState)
			{
			default:
				flag2 = false;
				break;
			case NKMWorldMapManager.WorldMapLeaderState.CityLeader:
				flag2 = true;
				flag = false;
				break;
			case NKMWorldMapManager.WorldMapLeaderState.CityLeaderOther:
				flag2 = true;
				flag = true;
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_objCityLeaderRoot, flag2);
			if (flag2)
			{
				if (!flag)
				{
					NKCUtil.SetImageSprite(this.m_imgCityLeaderBG, this.m_spCityLeaderBG, false);
					NKCUtil.SetLabelText(this.m_lbCityLeader, NKCUtilString.GET_STRING_WORLDMAP_CITY_LEADER);
					return;
				}
				NKCUtil.SetImageSprite(this.m_imgCityLeaderBG, this.m_spOtherCityBG, false);
				NKCUtil.SetLabelText(this.m_lbCityLeader, NKCUtilString.GET_STRING_WORLDMAP_ANOTHER_CITY);
			}
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x001E4235 File Offset: 0x001E2435
		private void OnDestroy()
		{
			this.CloseInstance();
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x001E423D File Offset: 0x001E243D
		public void CloseInstance()
		{
			if (this.m_instace != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_instace);
				this.m_instace = null;
			}
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x001E4259 File Offset: 0x001E2459
		public bool IsEmpty()
		{
			return this.m_NKMUnitData == null || this.m_NKMUnitData.m_UnitUID == 0L;
		}

		// Token: 0x04004CDD RID: 19677
		[NonSerialized]
		public int m_Index;

		// Token: 0x04004CDE RID: 19678
		[NonSerialized]
		public bool m_bLeader;

		// Token: 0x04004CDF RID: 19679
		[NonSerialized]
		public NKMUnitData m_NKMUnitData;

		// Token: 0x04004CE0 RID: 19680
		[NonSerialized]
		public NKMUnitTempletBase m_NKMUnitTempletBase;

		// Token: 0x04004CE1 RID: 19681
		private RectTransform m_RectTransform;

		// Token: 0x04004CE2 RID: 19682
		public GameObject m_objMain;

		// Token: 0x04004CE3 RID: 19683
		public RectTransform m_rectMain;

		// Token: 0x04004CE4 RID: 19684
		private Animator m_animatorMain;

		// Token: 0x04004CE5 RID: 19685
		public NKCUIComButton m_NKCUIComButton;

		// Token: 0x04004CE6 RID: 19686
		public NKCUIComDrag m_NKCUIComDrag;

		// Token: 0x04004CE7 RID: 19687
		public Image m_imgBGPanel;

		// Token: 0x04004CE8 RID: 19688
		public Image m_imgBgAddPanel;

		// Token: 0x04004CE9 RID: 19689
		public GameObject m_objUnitMain;

		// Token: 0x04004CEA RID: 19690
		public Image m_imgUnitPanel;

		// Token: 0x04004CEB RID: 19691
		public Image m_imgUnitAddPanel;

		// Token: 0x04004CEC RID: 19692
		public Image m_imgUnitGrayPanel;

		// Token: 0x04004CED RID: 19693
		public Image m_imgSlotCardBlur;

		// Token: 0x04004CEE RID: 19694
		public GameObject m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_COST_BG_Panel;

		// Token: 0x04004CEF RID: 19695
		public Text m_textCardCost;

		// Token: 0x04004CF0 RID: 19696
		public GameObject m_objArrow;

		// Token: 0x04004CF1 RID: 19697
		public GameObject m_objLeader;

		// Token: 0x04004CF2 RID: 19698
		public NKCUIComStarRank m_comStarRank;

		// Token: 0x04004CF3 RID: 19699
		public GameObject m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_LEVEL_GAUGE_SLIDER;

		// Token: 0x04004CF4 RID: 19700
		public Slider m_sliderExp;

		// Token: 0x04004CF5 RID: 19701
		public GameObject m_objExpMax;

		// Token: 0x04004CF6 RID: 19702
		public NKCUIComTextUnitLevel m_textLevel;

		// Token: 0x04004CF7 RID: 19703
		public Text m_lbTextLevelDesc;

		// Token: 0x04004CF8 RID: 19704
		public Image m_imgClassType;

		// Token: 0x04004CF9 RID: 19705
		public Image m_imgAttackType;

		// Token: 0x04004CFA RID: 19706
		public GameObject m_objRearm;

		// Token: 0x04004CFB RID: 19707
		private float m_PosXOrg;

		// Token: 0x04004CFC RID: 19708
		private float m_PosYOrg;

		// Token: 0x04004CFD RID: 19709
		public NKMTrackingFloat m_PosX = new NKMTrackingFloat();

		// Token: 0x04004CFE RID: 19710
		public NKMTrackingFloat m_PosY = new NKMTrackingFloat();

		// Token: 0x04004CFF RID: 19711
		public NKMTrackingFloat m_ScaleX = new NKMTrackingFloat();

		// Token: 0x04004D00 RID: 19712
		public NKMTrackingFloat m_ScaleY = new NKMTrackingFloat();

		// Token: 0x04004D01 RID: 19713
		[Header("Enemy")]
		public GameObject m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS;

		// Token: 0x04004D02 RID: 19714
		public Image m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS_img;

		// Token: 0x04004D03 RID: 19715
		[Header("지부 소속 정보")]
		public GameObject m_objCityLeaderRoot;

		// Token: 0x04004D04 RID: 19716
		public Image m_imgCityLeaderBG;

		// Token: 0x04004D05 RID: 19717
		public Text m_lbCityLeader;

		// Token: 0x04004D06 RID: 19718
		public Sprite m_spCityLeaderBG;

		// Token: 0x04004D07 RID: 19719
		public Sprite m_spOtherCityBG;

		// Token: 0x04004D08 RID: 19720
		private NKMWorldMapManager.WorldMapLeaderState m_eWorldmapState;

		// Token: 0x04004D09 RID: 19721
		[Header("밴 정보")]
		public GameObject m_objBan;

		// Token: 0x04004D0A RID: 19722
		public Text m_lbBanLevel;

		// Token: 0x04004D0B RID: 19723
		[Header("압류")]
		public GameObject m_objSeized;

		// Token: 0x04004D0C RID: 19724
		[Header("리그밴")]
		public GameObject m_objLeagueBan;

		// Token: 0x04004D0D RID: 19725
		public GameObject m_objLeaguePickEnable;

		// Token: 0x04004D0E RID: 19726
		public DOTweenAnimation m_doTweenAnimation;

		// Token: 0x04004D0F RID: 19727
		[Header("각성 애니메이션")]
		public Animator m_animAwakenFX;

		// Token: 0x04004D10 RID: 19728
		[Header("전술업데이트")]
		public NKCUITacticUpdateLevelSlot m_tacticUpdateLvSlot;

		// Token: 0x04004D11 RID: 19729
		private NKCAssetInstanceData m_instace;

		// Token: 0x04004D12 RID: 19730
		private bool m_bInDrag;

		// Token: 0x04004D13 RID: 19731
		private bool m_bEnableDrag = true;

		// Token: 0x04004D14 RID: 19732
		private bool m_bEnableShowBan;

		// Token: 0x04004D15 RID: 19733
		private bool m_bEnableShowUpUnit;

		// Token: 0x04004D16 RID: 19734
		private float fTimeTest = 3f;

		// Token: 0x04004D17 RID: 19735
		private const string DECK_SPRITE_BUNDLE_NAME = "ab_ui_unit_slot_deck_sprite";
	}
}
