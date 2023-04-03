using System;
using NKC.UI.Tooltip;
using NKM;
using NKM.Shop;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A4B RID: 2635
	public class NKCPopupFavoriteSlot : MonoBehaviour
	{
		// Token: 0x060073AB RID: 29611 RVA: 0x00267928 File Offset: 0x00265B28
		public void InitUI()
		{
			this.m_btnSlot.PointerClick.RemoveAllListeners();
			this.m_btnSlot.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			this.m_btnSlot.m_bGetCallbackWhileLocked = true;
			this.m_btnDelete.PointerClick.RemoveAllListeners();
			this.m_btnDelete.PointerClick.AddListener(new UnityAction(this.OnClickDelete));
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x0026799C File Offset: 0x00265B9C
		public void SetData(NKMStageTempletV2 stageTemplet)
		{
			this.m_StageTemplet = stageTemplet;
			NKCUtil.SetLabelText(this.m_lbStageNum, NKCUtilString.GetEpisodeNumber(stageTemplet.EpisodeTemplet, stageTemplet));
			NKCUtil.SetLabelText(this.m_lbStageName, stageTemplet.GetDungeonName());
			if (stageTemplet.MainRewardData != null && this.m_slotMainReward != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objMainReward, true);
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID, stageTemplet.MainRewardData.MinValue, 0);
				this.m_slotMainReward.SetData(slotData, true, null);
				this.m_slotMainReward.DisableItemCount();
				NKCUIComButton component = this.m_slotMainReward.gameObject.GetComponent<NKCUIComButton>();
				if (component != null)
				{
					component.PointerDown.RemoveAllListeners();
					component.PointerDown.AddListener(delegate(PointerEventData x)
					{
						NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
					});
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objMainReward, false);
			}
			bool flag;
			if (stageTemplet.EnterLimit > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_lbStageEnterLimit, true);
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int num = 0;
				if (nkmuserData != null)
				{
					num = nkmuserData.GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false);
				}
				NKCUtil.SetLabelText(this.m_lbStageEnterLimit, string.Format("{0}/{1}", stageTemplet.EnterLimit - num, stageTemplet.EnterLimit));
				flag = (!nkmuserData.IsHaveStatePlayData(this.m_StageTemplet.Key) || num < this.m_StageTemplet.EnterLimit || nkmuserData.GetStageRestoreCnt(this.m_StageTemplet.Key) < this.m_StageTemplet.RestoreLimit);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbStageEnterLimit, false);
				flag = true;
			}
			if (this.m_StageTemplet.EpisodeTemplet.IsOpen && this.m_StageTemplet.IsOpenedDayOfWeek() && flag)
			{
				this.m_btnSlot.UnLock(false);
			}
			else
			{
				this.m_btnSlot.Lock(false);
			}
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(this.m_StageTemplet.EpisodeTemplet.m_GroupID);
			NKCUtil.SetImageSprite(this.m_imgCategory, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeGroupTemplet.m_EPGroupIcon, false), false);
			if (NKMEpisodeMgr.HasHardDifficulty(stageTemplet.EpisodeId))
			{
				NKCUtil.SetGameobjectActive(this.m_imgNormalDot, stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL);
				NKCUtil.SetGameobjectActive(this.m_imgHardDot, stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.HARD);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgNormalDot, false);
				NKCUtil.SetGameobjectActive(this.m_imgHardDot, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckStageHasEventDrop(stageTemplet));
		}

		// Token: 0x060073AD RID: 29613 RVA: 0x00267C38 File Offset: 0x00265E38
		private void OnClickSlot()
		{
			if (this.m_btnSlot.m_bLock)
			{
				if (!this.m_StageTemplet.EpisodeTemplet.IsOpen || !this.m_StageTemplet.IsOpenedDayOfWeek())
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_FAVORITES_NO_ENTRY, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			else
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (this.m_StageTemplet.EnterLimit > 0 && nkmuserData.IsHaveStatePlayData(this.m_StageTemplet.Key) && nkmuserData.GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false) >= this.m_StageTemplet.EnterLimit)
				{
					if (nkmuserData.GetStageRestoreCnt(this.m_StageTemplet.Key) >= this.m_StageTemplet.RestoreLimit)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
						return;
					}
					NKCPopupResourceWithdraw.Instance.OpenForRestoreEnterLimit(this.m_StageTemplet, delegate
					{
						NKCPacketSender.Send_NKMPacket_RESET_STAGE_PLAY_COUNT_REQ(this.m_StageTemplet.Key);
					}, nkmuserData.GetStageRestoreCnt(this.m_StageTemplet.Key));
					return;
				}
				else if (this.m_StageTemplet.EpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY && (long)this.m_StageTemplet.m_StageReqItemCount - nkmuserData.m_InventoryData.GetCountMiscItem(this.m_StageTemplet.m_StageReqItemID) > 0L)
				{
					int dailyMissionTicketShopID = NKCShopManager.GetDailyMissionTicketShopID(this.m_StageTemplet.EpisodeTemplet.m_EpisodeID);
					if (NKCShopManager.GetBuyCountLeft(dailyMissionTicketShopID) > 0)
					{
						NKCShopManager.OnBtnProductBuy(ShopItemTemplet.Find(dailyMissionTicketShopID).Key, false);
						return;
					}
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				else
				{
					if (!NKMEpisodeMgr.HasEnoughResource(this.m_StageTemplet, 1))
					{
						return;
					}
					NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite = true;
					if (this.m_StageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE)
					{
						NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(this.m_StageTemplet, DeckContents.PHASE);
					}
					else
					{
						NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(this.m_StageTemplet, DeckContents.NORMAL);
					}
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
					return;
				}
			}
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x00267E48 File Offset: 0x00266048
		private void OnClickDelete()
		{
			NKCPacketSender.Send_NKMPacket_FAVORITES_STAGE_DELETE_REQ(this.m_StageTemplet.Key);
		}

		// Token: 0x04005FA4 RID: 24484
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x04005FA5 RID: 24485
		public Image m_imgCategory;

		// Token: 0x04005FA6 RID: 24486
		public Image m_imgNormalDot;

		// Token: 0x04005FA7 RID: 24487
		public Image m_imgHardDot;

		// Token: 0x04005FA8 RID: 24488
		public TMP_Text m_lbStageNum;

		// Token: 0x04005FA9 RID: 24489
		public TMP_Text m_lbStageName;

		// Token: 0x04005FAA RID: 24490
		public GameObject m_objMainReward;

		// Token: 0x04005FAB RID: 24491
		public NKCUISlot m_slotMainReward;

		// Token: 0x04005FAC RID: 24492
		public NKCUIComStateButton m_btnDelete;

		// Token: 0x04005FAD RID: 24493
		public TMP_Text m_lbStageEnterLimit;

		// Token: 0x04005FAE RID: 24494
		public GameObject m_objEventDrop;

		// Token: 0x04005FAF RID: 24495
		private NKMStageTempletV2 m_StageTemplet;
	}
}
