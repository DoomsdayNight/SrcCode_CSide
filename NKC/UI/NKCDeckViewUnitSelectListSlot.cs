using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000968 RID: 2408
	public class NKCDeckViewUnitSelectListSlot : NKCUIUnitSelectListSlotBase
	{
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x0600608E RID: 24718 RVA: 0x001E2A00 File Offset: 0x001E0C00
		protected override NKCResourceUtility.eUnitResourceType UseResourceType
		{
			get
			{
				return NKCResourceUtility.eUnitResourceType.INVEN_ICON;
			}
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x001E2A04 File Offset: 0x001E0C04
		public override void SetData(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(cNKMUnitData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			base.ProcessBanUIForUnit();
			if (cNKMUnitData != null)
			{
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(cNKMUnitData.m_UnitID);
					if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(cNKMUnitData.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
						this.m_lbSummonCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_BAN_COST, respawnCost.ToString());
					}
					else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(cNKMUnitData.m_UnitID))
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
						this.m_lbSummonCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_UP_COST, respawnCost.ToString());
					}
					else
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(false, false, null, null);
						this.m_lbSummonCost.text = respawnCost.ToString();
					}
				}
				if (this.m_sliderExp != null)
				{
					if (NKCExpManager.GetUnitMaxLevel(cNKMUnitData) == cNKMUnitData.m_UnitLevel)
					{
						this.m_sliderExp.value = 1f;
					}
					else
					{
						this.m_sliderExp.value = NKCExpManager.GetUnitNextLevelExpProgress(cNKMUnitData);
					}
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(cNKMUnitData);
				}
			}
			this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x001E2B44 File Offset: 0x001E0D44
		public override void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(templetBase, levelToDisplay, skinID, bEnableLayoutElement, onSelectThisSlot);
			if (templetBase != null)
			{
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(templetBase.m_UnitID);
					this.m_lbSummonCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
				}
				if (this.m_sliderExp != null)
				{
					this.m_sliderExp.value = 0f;
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank == null)
				{
					return;
				}
				comStarRank.SetStarRank(templetBase.m_StarGradeMax, templetBase.m_StarGradeMax, false);
			}
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x001E2BD4 File Offset: 0x001E0DD4
		public override void SetDataForBan(NKMUnitTempletBase templetBase, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bUp = false, bool bSetOriginalCost = false)
		{
			base.SetData(templetBase, 0, bEnableLayoutElement, onSelectThisSlot);
			base.ProcessBanUIForUnit();
			if (templetBase != null)
			{
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(templetBase.m_UnitID);
					this.m_lbSummonCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
				}
				if (this.m_sliderExp != null)
				{
					this.m_sliderExp.value = 0f;
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(templetBase.m_StarGradeMax, templetBase.m_StarGradeMax, false);
				}
			}
			this.SetCityLeaderMark(false);
			this.SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState.None);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x001E2C7C File Offset: 0x001E0E7C
		public override void SetSlotState(NKCUnitSortSystem.eUnitState eUnitSlotState)
		{
			this.m_eUnitSlotState = eUnitSlotState;
			NKCUtil.SetGameobjectActive(this.m_objInCityMission, this.m_eUnitSlotState > NKCUnitSortSystem.eUnitState.NONE);
			NKCUtil.SetGameobjectActive(this.m_objSeized, false);
			if (this.m_cgCard != null)
			{
				this.m_cgCard.alpha = ((eUnitSlotState == NKCUnitSortSystem.eUnitState.NONE) ? 1f : 0.5f);
			}
			switch (this.m_eUnitSlotState)
			{
			case NKCUnitSortSystem.eUnitState.DUPLICATE:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DUPLICATE);
				return;
			case NKCUnitSortSystem.eUnitState.CITY_SET:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_WORLDMAP_CITY_LEADER);
				return;
			case NKCUnitSortSystem.eUnitState.CITY_MISSION:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_MISSION);
				return;
			case NKCUnitSortSystem.eUnitState.WARFARE_BATCH:
			case NKCUnitSortSystem.eUnitState.DIVE_BATCH:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DOING);
				return;
			case NKCUnitSortSystem.eUnitState.DECKED:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DECKED);
				return;
			case NKCUnitSortSystem.eUnitState.MAINUNIT:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_MAINUNIT);
				return;
			case NKCUnitSortSystem.eUnitState.LOCKED:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_LOCKED);
				return;
			case NKCUnitSortSystem.eUnitState.SEIZURE:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_SEIZURE);
				return;
			case NKCUnitSortSystem.eUnitState.LOBBY_UNIT:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_LOBBY_UNIT_CAPTAIN);
				return;
			case NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED:
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_CANNOT_USE", false));
				return;
			case NKCUnitSortSystem.eUnitState.CHECKED:
				break;
			case NKCUnitSortSystem.eUnitState.LEAGUE_BANNED:
				NKCUtil.SetCanvasGroupAlpha(this.m_cgCard, 1f);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetGameobjectActive(this.m_objLeagueBanned, true);
				NKCUtil.SetGameobjectActive(this.m_objLeaguePicked, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, "");
				NKCUtil.SetLabelText(this.m_lbMissionStatus, "");
				return;
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT:
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_RIGHT:
			{
				NKCUtil.SetCanvasGroupAlpha(this.m_cgCard, 1f);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetGameobjectActive(this.m_objLeagueBanned, false);
				NKCUtil.SetGameobjectActive(this.m_objLeaguePicked, true);
				Color color = (this.m_eUnitSlotState == NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT) ? this.m_colorLeaguePickedLeft : this.m_colorLeaguePickedRight;
				NKCUtil.SetImageColor(this.m_imgLeaguePicked, color);
				NKCUtil.SetLabelText(this.m_lbMissionStatus, "");
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x001E2E94 File Offset: 0x001E1094
		protected override void OnClick()
		{
			NKCUnitSortSystem.eUnitState eUnitSlotState = this.m_eUnitSlotState;
			if (eUnitSlotState - NKCUnitSortSystem.eUnitState.LEAGUE_BANNED <= 2)
			{
				return;
			}
			base.OnClick();
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x001E2EB8 File Offset: 0x001E10B8
		protected override void SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode mode)
		{
			base.SetMode(mode);
			if (mode - NKCUIUnitSelectListSlotBase.eUnitSlotMode.ClearAll <= 1)
			{
				NKCUtil.SetGameobjectActive(this.m_objSlotStatus, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objAutoSelect, mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.AutoComplete);
			NKCUtil.SetGameobjectActive(this.m_objClearDeck, mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.ClearAll);
			NKCUtil.SetGameobjectActive(this.m_lbName, mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character);
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001E2F0C File Offset: 0x001E110C
		public void SetCityLeaderTag(NKMWorldMapManager.WorldMapLeaderState eWorldmapState)
		{
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

		// Token: 0x04004CD3 RID: 19667
		[Header("일반 유닛 전용 정보")]
		public Slider m_sliderExp;

		// Token: 0x04004CD4 RID: 19668
		public Text m_lbSummonCost;

		// Token: 0x04004CD5 RID: 19669
		public GameObject m_objAutoSelect;

		// Token: 0x04004CD6 RID: 19670
		public GameObject m_objClearDeck;

		// Token: 0x04004CD7 RID: 19671
		public CanvasGroup m_cgCard;

		// Token: 0x04004CD8 RID: 19672
		[Header("지부 소속 정보")]
		public GameObject m_objCityLeaderRoot;

		// Token: 0x04004CD9 RID: 19673
		public Image m_imgCityLeaderBG;

		// Token: 0x04004CDA RID: 19674
		public Text m_lbCityLeader;

		// Token: 0x04004CDB RID: 19675
		public Sprite m_spCityLeaderBG;

		// Token: 0x04004CDC RID: 19676
		public Sprite m_spOtherCityBG;
	}
}
