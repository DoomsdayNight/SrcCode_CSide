using System;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A17 RID: 2583
	public class NKCUIStagePrefabNode : MonoBehaviour
	{
		// Token: 0x060070C9 RID: 28873 RVA: 0x00256BF5 File Offset: 0x00254DF5
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x00256C02 File Offset: 0x00254E02
		public bool CheckLock()
		{
			return this.m_objLock.activeSelf;
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x00256C0F File Offset: 0x00254E0F
		public int GetStageIndex()
		{
			return this.m_StageIndex;
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x00256C18 File Offset: 0x00254E18
		public void SetData(NKMStageTempletV2 stageTemplet, IDungeonSlot.OnSelectedItemSlot selectedSlot = null)
		{
			if (stageTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_StageIndex = stageTemplet.m_StageIndex;
			this.m_StageBattleStrID = stageTemplet.m_StageBattleStrID;
			this.SetOnSelectedItemSlot(selectedSlot);
			NKCUtil.SetGameobjectActive(this.m_objSelected, false);
			NKCUtil.SetGameobjectActive(this.m_objNew, false);
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			NKCUtil.SetLabelText(this.m_lbStageNum, NKCUtilString.GetEpisodeNumber(stageTemplet.EpisodeTemplet, stageTemplet));
			NKCUtil.SetGameobjectActive(this.m_objLock, !NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, stageTemplet.m_UnlockInfo, false));
			this.SetMainReward(cNKMUserData, stageTemplet);
			this.SetFirstReward(cNKMUserData, stageTemplet);
			this.SetMedalReward(cNKMUserData, stageTemplet);
			this.SetMedalInfo(stageTemplet);
			this.SetPlayLimit(cNKMUserData, stageTemplet);
			if (this.m_imgBoss != null)
			{
				NKMDungeonTempletBase dungeonTempletBase = stageTemplet.DungeonTempletBase;
				if (dungeonTempletBase != null)
				{
					Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_" + dungeonTempletBase.m_DungeonIcon, false);
					if (orLoadAssetResource != null)
					{
						NKCUtil.SetImageSprite(this.m_imgBoss, orLoadAssetResource, false);
					}
					else
					{
						NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
						if (assetResourceUnitInvenIconEmpty != null)
						{
							NKCUtil.SetImageSprite(this.m_imgBoss, assetResourceUnitInvenIconEmpty.GetAsset<Sprite>(), false);
						}
					}
				}
			}
			if (this.m_lbLevel != null)
			{
				if (stageTemplet.DungeonTempletBase != null)
				{
					NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, stageTemplet.DungeonTempletBase.m_DungeonLevel));
				}
				else if (stageTemplet.PhaseTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, stageTemplet.PhaseTemplet.PhaseLevel));
				}
			}
			this.SetDungeonClear(cNKMUserData, stageTemplet);
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckStageHasEventDrop(stageTemplet));
		}

		// Token: 0x060070CD RID: 28877 RVA: 0x00256DBC File Offset: 0x00254FBC
		private void SetMainReward(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			if (this.m_objMainReward != null)
			{
				if (stageTemplet.MainRewardData != null && stageTemplet.MainRewardData.rewardType != NKM_REWARD_TYPE.RT_NONE)
				{
					NKCUtil.SetGameobjectActive(this.m_objMainReward, true);
					if (this.m_RewardSlot != null)
					{
						this.m_RewardSlot.Init();
						NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID, stageTemplet.MainRewardData.MinValue, 0);
						this.m_RewardSlot.SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickMainReward));
						this.m_RewardSlot.DisableItemCount();
						NKCUIComButton component = this.m_RewardSlot.gameObject.GetComponent<NKCUIComButton>();
						if (component != null)
						{
							component.PointerDown.RemoveAllListeners();
							component.PointerDown.AddListener(delegate(PointerEventData x)
							{
								NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
							});
							return;
						}
					}
					else if (this.m_imgRewardIcon != null && NKMItemMiscTemplet.Find(stageTemplet.MainRewardData.ID) != null)
					{
						NKCUtil.SetImageSprite(this.m_imgRewardIcon, NKCResourceUtility.GetRewardInvenIcon(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID, false), false);
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objMainReward, false);
				}
			}
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x00256F0C File Offset: 0x0025510C
		private void SetFirstReward(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			if (this.m_objFirstReward != null)
			{
				if (stageTemplet.GetFirstRewardData() != FirstRewardData.Empty)
				{
					NKCUtil.SetGameobjectActive(this.m_objFirstReward, true);
					FirstRewardData firstRewardData = stageTemplet.GetFirstRewardData();
					bool completeMark = NKMEpisodeMgr.CheckClear(cNKMUserData, stageTemplet);
					if (!(this.m_FirstRewardSlot != null) || firstRewardData == null || firstRewardData.Type == NKM_REWARD_TYPE.RT_NONE || firstRewardData.RewardId == 0)
					{
						NKCUtil.SetGameobjectActive(this.m_objFirstReward, false);
						return;
					}
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity, 0);
					this.m_FirstRewardSlot.SetData(slotData, true, null);
					this.m_FirstRewardSlot.SetCompleteMark(completeMark);
					this.m_FirstRewardSlot.SetFirstGetMark(true);
					NKCUIComButton component = this.m_FirstRewardSlot.gameObject.GetComponent<NKCUIComButton>();
					if (component != null)
					{
						component.PointerDown.RemoveAllListeners();
						component.PointerDown.AddListener(delegate(PointerEventData x)
						{
							NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
						});
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objFirstReward, false);
				}
			}
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x00257028 File Offset: 0x00255228
		private void SetMedalInfo(NKMStageTempletV2 stageTemplet)
		{
			if (this.m_Medal != null)
			{
				if (stageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE)
				{
					this.m_Medal.SetData(stageTemplet.PhaseTemplet, false);
					return;
				}
				if (stageTemplet.DungeonTempletBase != null)
				{
					this.m_Medal.SetData(stageTemplet.DungeonTempletBase, false);
				}
			}
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x0025707C File Offset: 0x0025527C
		private void SetPlayLimit(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet.EnterLimit > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objPlayLimit, true);
				int statePlayCnt = cNKMUserData.GetStatePlayCnt(stageTemplet.Key, false, false, false);
				NKCUtil.SetLabelText(this.m_lbPlayLimit, string.Format("{0}/{1}", stageTemplet.EnterLimit - statePlayCnt, stageTemplet.EnterLimit));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objPlayLimit, false);
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x002570E8 File Offset: 0x002552E8
		private void SetMedalReward(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			if (this.m_objMedalReward != null)
			{
				if (stageTemplet.MissionReward != null && stageTemplet.MissionReward.rewardType != NKM_REWARD_TYPE.RT_NONE && stageTemplet.MissionReward.ID != 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objMedalReward, true);
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(stageTemplet.MissionReward.rewardType, stageTemplet.MissionReward.ID, stageTemplet.MissionReward.Count, 0);
					this.m_MedalRewardSlot.SetData(slotData, true, null);
					this.m_MedalRewardSlot.SetFirstAllClearMark(true);
					bool medalAllClear = NKMEpisodeMgr.GetMedalAllClear(cNKMUserData, stageTemplet);
					this.m_MedalRewardSlot.SetCompleteMark(medalAllClear);
					NKCUIComButton component = this.m_MedalRewardSlot.gameObject.GetComponent<NKCUIComButton>();
					if (component != null)
					{
						component.PointerDown.RemoveAllListeners();
						component.PointerDown.AddListener(delegate(PointerEventData x)
						{
							NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
						});
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objMedalReward, false);
				}
			}
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x002571EC File Offset: 0x002553EC
		private void SetDungeonClear(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			if (this.m_objClear != null)
			{
				bool bValue = false;
				switch (stageTemplet.m_STAGE_TYPE)
				{
				case STAGE_TYPE.ST_DUNGEON:
				{
					NKMDungeonTempletBase dungeonTempletBase = stageTemplet.DungeonTempletBase;
					if (dungeonTempletBase != null && cNKMUserData.GetDungeonClearData(dungeonTempletBase.m_DungeonID) != null)
					{
						bValue = true;
					}
					break;
				}
				case STAGE_TYPE.ST_PHASE:
				{
					NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(this.m_StageBattleStrID);
					if (nkmphaseTemplet != null && NKCPhaseManager.GetPhaseClearData(nkmphaseTemplet) != null)
					{
						bValue = true;
					}
					break;
				}
				}
				NKCUtil.SetGameobjectActive(this.m_objClear, bValue);
			}
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x00257267 File Offset: 0x00255467
		public void SetOnSelectedItemSlot(IDungeonSlot.OnSelectedItemSlot selectedSlot)
		{
			if (selectedSlot != null)
			{
				this.m_btn.PointerClick.RemoveAllListeners();
				this.m_OnSelectedSlot = selectedSlot;
				this.m_btn.PointerClick.AddListener(new UnityAction(this.OnSelectedItemSlotImpl));
			}
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x0025729F File Offset: 0x0025549F
		private void OnSelectedItemSlotImpl()
		{
			if (this.m_OnSelectedSlot != null)
			{
				this.m_OnSelectedSlot(this.m_StageIndex, this.m_StageBattleStrID, false);
			}
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x002572C1 File Offset: 0x002554C1
		public void SetSelectNode(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelected, bValue);
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x002572CF File Offset: 0x002554CF
		public void RefreshSlot(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			this.SetDungeonClear(cNKMUserData, stageTemplet);
			this.SetFirstReward(cNKMUserData, stageTemplet);
			this.SetMainReward(cNKMUserData, stageTemplet);
			this.SetMedalInfo(stageTemplet);
			this.SetMedalReward(cNKMUserData, stageTemplet);
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x002572F8 File Offset: 0x002554F8
		public void SetEnableNewMark(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objNew, bValue);
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x00257306 File Offset: 0x00255506
		private void OnClickMainReward(NKCUISlot.SlotData slotData, bool bLocked)
		{
		}

		// Token: 0x04005C88 RID: 23688
		[Header("공용")]
		public Text m_lbStageNum;

		// Token: 0x04005C89 RID: 23689
		public NKCUIComStateButton m_btn;

		// Token: 0x04005C8A RID: 23690
		public GameObject m_objLock;

		// Token: 0x04005C8B RID: 23691
		public GameObject m_objNew;

		// Token: 0x04005C8C RID: 23692
		public GameObject m_objSelected;

		// Token: 0x04005C8D RID: 23693
		[Header("주요 보상")]
		[Header("사용할 오브젝트만 링크")]
		public GameObject m_objMainReward;

		// Token: 0x04005C8E RID: 23694
		public NKCUISlot m_RewardSlot;

		// Token: 0x04005C8F RID: 23695
		public Image m_imgRewardIcon;

		// Token: 0x04005C90 RID: 23696
		[Header("최초 클리어 보상")]
		public GameObject m_objFirstReward;

		// Token: 0x04005C91 RID: 23697
		public NKCUISlot m_FirstRewardSlot;

		// Token: 0x04005C92 RID: 23698
		[Header("3메달 보상")]
		public GameObject m_objMedalReward;

		// Token: 0x04005C93 RID: 23699
		public NKCUISlot m_MedalRewardSlot;

		// Token: 0x04005C94 RID: 23700
		[Header("메달")]
		public NKCUIComMedal m_Medal;

		// Token: 0x04005C95 RID: 23701
		[Header("보스초상화")]
		public Image m_imgBoss;

		// Token: 0x04005C96 RID: 23702
		[Header("레벨")]
		public Text m_lbLevel;

		// Token: 0x04005C97 RID: 23703
		[Header("클리어 체크표시")]
		public GameObject m_objClear;

		// Token: 0x04005C98 RID: 23704
		[Header("입장 횟수 제한")]
		public GameObject m_objPlayLimit;

		// Token: 0x04005C99 RID: 23705
		public Text m_lbPlayLimit;

		// Token: 0x04005C9A RID: 23706
		[Header("이벤트 드롭")]
		public GameObject m_objEventDrop;

		// Token: 0x04005C9B RID: 23707
		private IDungeonSlot.OnSelectedItemSlot m_OnSelectedSlot;

		// Token: 0x04005C9C RID: 23708
		private int m_StageIndex;

		// Token: 0x04005C9D RID: 23709
		private string m_StageBattleStrID;
	}
}
