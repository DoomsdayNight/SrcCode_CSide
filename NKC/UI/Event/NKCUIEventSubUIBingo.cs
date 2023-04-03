using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Event;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BDC RID: 3036
	public class NKCUIEventSubUIBingo : NKCUIEventSubUIBase
	{
		// Token: 0x06008CCA RID: 36042 RVA: 0x002FDCE8 File Offset: 0x002FBEE8
		public override void Init()
		{
			base.Init();
			if (this.m_btnGuidePopup != null)
			{
				this.m_btnGuidePopup.PointerClick.RemoveAllListeners();
				this.m_btnGuidePopup.PointerClick.AddListener(new UnityAction(this.OnTouchGuidePopup));
			}
			if (this.m_btnMissionPopup != null)
			{
				this.m_btnMissionPopup.PointerClick.RemoveAllListeners();
				this.m_btnMissionPopup.PointerClick.AddListener(new UnityAction(this.OnTouchMissionPopup));
			}
			if (this.m_btnRewardPopup != null)
			{
				this.m_btnRewardPopup.PointerClick.RemoveAllListeners();
				this.m_btnRewardPopup.PointerClick.AddListener(new UnityAction(this.OnTouchRewardPopup));
			}
			if (this.m_btnTry != null)
			{
				this.m_btnTry.PointerClick.RemoveAllListeners();
				this.m_btnTry.PointerClick.AddListener(new UnityAction(this.OnTouchTry));
			}
			if (this.m_btnSpecialTry != null)
			{
				this.m_btnSpecialTry.PointerClick.RemoveAllListeners();
				this.m_btnSpecialTry.PointerClick.AddListener(new UnityAction(this.OnTouchSpecialTry));
			}
			if (this.m_listLineReward != null)
			{
				for (int i = 0; i < this.m_listLineReward.Count; i++)
				{
					this.m_listLineReward[i].Init();
				}
			}
			if (this.m_lastReward != null)
			{
				this.m_lastReward.Init();
			}
		}

		// Token: 0x06008CCB RID: 36043 RVA: 0x002FDE68 File Offset: 0x002FC068
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			NKMEventBingoTemplet bingoTemplet = NKMEventManager.GetBingoTemplet(tabTemplet.m_EventID);
			if (bingoTemplet == null)
			{
				Debug.LogError(string.Format("BingoEvent - 잘못된 EventID : {0}", tabTemplet.m_EventID));
				return;
			}
			EventBingo bingoData = NKMEventManager.GetBingoData(tabTemplet.m_EventID);
			if (bingoData == null)
			{
				Debug.LogError(string.Format("BingoEvent - 빙고데이터가 없음 : {0}", tabTemplet.m_EventID));
				return;
			}
			this.m_tabTemplet = tabTemplet;
			this.m_bingoTemplet = bingoTemplet;
			this.m_specialMode = false;
			this.m_specialIndex = -1;
			this.SetData(bingoData);
		}

		// Token: 0x06008CCC RID: 36044 RVA: 0x002FDEEC File Offset: 0x002FC0EC
		public override void Close()
		{
			if (this.m_coroutineGetNum != null)
			{
				base.StopCoroutine(this.m_coroutineGetNum);
			}
			this.m_coroutineGetNum = null;
			this.m_bPrecessGetNum = false;
			this.m_specialMode = false;
			this.m_specialIndex = -1;
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					this.m_listSlot[i].SetSelectFx(false);
					this.m_listSlot[i].SetGetFx(false);
				}
			}
			NKCUIManager.SetScreenInputBlock(false);
		}

		// Token: 0x06008CCD RID: 36045 RVA: 0x002FDF74 File Offset: 0x002FC174
		public override void Hide()
		{
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					this.m_listSlot[i].SetSelectFx(false);
					this.m_listSlot[i].SetGetFx(false);
				}
			}
		}

		// Token: 0x06008CCE RID: 36046 RVA: 0x002FDFC4 File Offset: 0x002FC1C4
		private void OnDisable()
		{
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					this.m_listSlot[i].SetSelectFx(false);
					this.m_listSlot[i].SetGetFx(false);
				}
			}
		}

		// Token: 0x06008CCF RID: 36047 RVA: 0x002FE013 File Offset: 0x002FC213
		public override bool OnBackButton()
		{
			if (this.m_bPrecessGetNum)
			{
				this.m_bTouch = true;
				return true;
			}
			return false;
		}

		// Token: 0x06008CD0 RID: 36048 RVA: 0x002FE028 File Offset: 0x002FC228
		private void SetData(EventBingo bingoData)
		{
			int bingoSize = this.m_bingoTemplet.m_BingoSize;
			this.InitBingoSlot(bingoSize);
			BingoInfo bingoInfo = bingoData.m_bingoInfo;
			if (this.m_listLineReward != null)
			{
				for (int i = 0; i < this.m_listLineReward.Count; i++)
				{
					NKCUISlot nkcuislot = this.m_listLineReward[i];
					NKMEventBingoRewardTemplet bingoRewardTemplet = NKMEventManager.GetBingoRewardTemplet(this.m_bingoTemplet.m_BingoCompletRewardGroupID, BingoCompleteType.LINE_SINGLE, i + 1);
					if (bingoRewardTemplet == null)
					{
						NKCUtil.SetGameobjectActive(nkcuislot, false);
					}
					else if (bingoRewardTemplet.rewards == null || bingoRewardTemplet.rewards.Count == 0)
					{
						NKCUtil.SetGameobjectActive(nkcuislot, false);
					}
					else
					{
						NKMRewardInfo nkmrewardInfo = bingoRewardTemplet.rewards[0];
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count, 0);
						nkcuislot.SetData(data, true, null);
					}
				}
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_bingoTemplet.m_BingoTryItemID);
			if (itemMiscTempletByID != null)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
				NKCUtil.SetImageSprite(this.m_imgTryItemIcon, orLoadMiscItemSmallIcon, false);
			}
			NKCUtil.SetLabelText(this.m_txtTryItemCount, this.m_bingoTemplet.m_BingoTryItemValue.ToString());
			if (this.m_lastReward != null)
			{
				NKMEventBingoRewardTemplet bingoLastRewardTemplet = NKMEventManager.GetBingoLastRewardTemplet(this.m_bingoTemplet.m_EventID);
				if (bingoLastRewardTemplet != null)
				{
					NKMRewardInfo nkmrewardInfo2 = bingoLastRewardTemplet.rewards[0];
					NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo2.rewardType, nkmrewardInfo2.ID, nkmrewardInfo2.Count, 0);
					this.m_lastReward.SetData(data2, true, null);
				}
			}
			base.SetDateLimit();
			this.Refresh();
		}

		// Token: 0x06008CD1 RID: 36049 RVA: 0x002FE1AC File Offset: 0x002FC3AC
		public override void Refresh()
		{
			EventBingo bingoData = NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
			if (bingoData == null)
			{
				this.Close();
				return;
			}
			this.UpdateBingoSlot(bingoData);
			this.UpdateRewardSlot(bingoData);
			this.UpdateLine(bingoData);
			this.UpdateMileage(bingoData);
			this.UpdateSpecialButton(bingoData);
			this.UpdateTryButton(bingoData);
			this.UpdateLastReward(bingoData);
			this.UpdateRedDot();
			NKCUtil.SetGameobjectActive(this.m_objSpacialMode, this.m_specialMode);
			this.SetSlotSpecialMode(this.m_specialMode);
			this.SetSlotSelectFx(this.m_specialIndex);
			NKCUtil.SetGameobjectActive(this.m_objAlreadyGetNum, false);
			NKCUtil.SetGameobjectActive(this.m_objGetNum, false);
		}

		// Token: 0x06008CD2 RID: 36050 RVA: 0x002FE24C File Offset: 0x002FC44C
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			EventBingo bingoData = NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
			this.UpdateTryButton(bingoData);
			this.UpdateRedDot();
		}

		// Token: 0x06008CD3 RID: 36051 RVA: 0x002FE278 File Offset: 0x002FC478
		private void UpdateBingoSlot(EventBingo bingoData)
		{
			BingoInfo bingoInfo = bingoData.m_bingoInfo;
			int bingoSize = this.m_bingoTemplet.m_BingoSize;
			int num = 0;
			for (int i = 0; i < this.m_listSlot.Count; i++)
			{
				NKCUIBingoSlot nkcuibingoSlot = this.m_listSlot[i];
				if ((double)i < Math.Pow((double)bingoSize, 2.0) && i < bingoInfo.tileValueList.Count)
				{
					if (this.m_bingoTemplet.MissionTiles.Contains(i))
					{
						nkcuibingoSlot.SetData(i, ++num, bingoInfo.markTileIndexList.Contains(i), true);
					}
					else
					{
						nkcuibingoSlot.SetData(i, bingoInfo.tileValueList[i], bingoInfo.markTileIndexList.Contains(i), false);
					}
					NKCUtil.SetGameobjectActive(nkcuibingoSlot, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuibingoSlot, false);
				}
			}
		}

		// Token: 0x06008CD4 RID: 36052 RVA: 0x002FE348 File Offset: 0x002FC548
		private void UpdateRewardSlot(EventBingo bingoData)
		{
			if (this.m_listLineReward == null || bingoData == null)
			{
				return;
			}
			List<int> bingoLine = bingoData.GetBingoLine();
			for (int i = 0; i < this.m_listLineReward.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_listLineReward[i];
				bool flag = bingoLine.Contains(i);
				NKMEventBingoRewardTemplet rewardTemplet = NKMEventManager.GetBingoRewardTemplet(this.m_bingoTemplet.m_BingoCompletRewardGroupID, BingoCompleteType.LINE_SINGLE, i + 1);
				bool flag2 = false;
				if (rewardTemplet != null)
				{
					flag2 = NKMEventManager.IsReceiveableBingoReward(this.m_bingoTemplet.m_EventID, rewardTemplet.ZeroBaseTileIndex);
				}
				nkcuislot.SetDisable(flag && !flag2, "");
				nkcuislot.SetEventGet(flag && !flag2);
				nkcuislot.SetRewardFx(flag && flag2);
				if (this.m_specialMode || flag2)
				{
					nkcuislot.SetOnClick(delegate(NKCUISlot.SlotData slotData, bool bLock)
					{
						this.OnTouchBingoReward(rewardTemplet.ZeroBaseTileIndex);
					});
				}
				else
				{
					nkcuislot.SetOpenItemBoxOnClick();
				}
			}
		}

		// Token: 0x06008CD5 RID: 36053 RVA: 0x002FE444 File Offset: 0x002FC644
		private void UpdateLine(EventBingo bingoData)
		{
			if (this.m_listCompleteLine == null)
			{
				return;
			}
			List<int> bingoLine = bingoData.GetBingoLine();
			for (int i = 0; i < this.m_listCompleteLine.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_listCompleteLine[i], bingoLine.Contains(i));
			}
		}

		// Token: 0x06008CD6 RID: 36054 RVA: 0x002FE490 File Offset: 0x002FC690
		private void UpdateMileage(EventBingo bingoData)
		{
			if (this.m_txtMileage == null)
			{
				return;
			}
			int mileage = bingoData.m_bingoInfo.mileage;
			int bingoSpecialTryRequireCnt = this.m_bingoTemplet.m_BingoSpecialTryRequireCnt;
			string arg;
			if (mileage >= bingoSpecialTryRequireCnt)
			{
				arg = mileage.ToString();
			}
			else
			{
				arg = string.Format("<color=#cd2121>{0}</color>", mileage);
			}
			this.m_txtMileage.text = string.Format(NKCUtilString.GET_STRING_EVENT_BINGO_MILEAGE, arg, bingoSpecialTryRequireCnt);
		}

		// Token: 0x06008CD7 RID: 36055 RVA: 0x002FE500 File Offset: 0x002FC700
		private void UpdateSpecialButton(EventBingo bingoData)
		{
			int mileage = bingoData.m_bingoInfo.mileage;
			int bingoSpecialTryRequireCnt = this.m_bingoTemplet.m_BingoSpecialTryRequireCnt;
			bool flag = mileage >= bingoSpecialTryRequireCnt;
			bool flag2 = !this.m_specialMode && flag && bingoData.IsRemainNum();
			string msg;
			Color col;
			if (this.m_specialMode)
			{
				msg = NKCUtilString.GET_STRING_EVENT_BINGO_SPECIAL_CANCEL;
				col = this.m_colorSpecialCancel;
			}
			else if (flag2)
			{
				msg = NKCUtilString.GET_STRING_EVENT_BINGO_SPECIAL;
				col = this.m_colorSpecialEnable;
			}
			else
			{
				msg = NKCUtilString.GET_STRING_EVENT_BINGO_SPECIAL;
				col = this.m_colorSpecialDisable;
			}
			NKCUtil.SetGameobjectActive(this.m_objSpecialBtnEnable, !this.m_specialMode && flag2);
			NKCUtil.SetGameobjectActive(this.m_objSpecialBtnDisable, !this.m_specialMode && !flag2);
			NKCUtil.SetGameobjectActive(this.m_objSpecialBtnCancel, this.m_specialMode);
			NKCUtil.SetLabelTextColor(this.m_txtSpecialBtn, col);
			NKCUtil.SetLabelText(this.m_txtSpecialBtn, msg);
		}

		// Token: 0x06008CD8 RID: 36056 RVA: 0x002FE5D8 File Offset: 0x002FC7D8
		private void UpdateTryButton(EventBingo bingoData)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && bingoData != null)
			{
				long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(this.m_bingoTemplet.m_BingoTryItemID);
				NKCUtil.SetLabelText(this.m_lbTryItemTotalCount, NKCUtilString.GET_STRING_HAVE_COUNT_ONE_PARAM, new object[]
				{
					countMiscItem
				});
				if (bingoData.IsRemainNum())
				{
					bool flag = nkmuserData.CheckPrice(this.m_bingoTemplet.m_BingoTryItemValue, this.m_bingoTemplet.m_BingoTryItemID);
					bool flag2 = !this.m_specialMode && flag;
					NKCUtil.SetGameobjectActive(this.m_objTryBtnEnable, flag2);
					NKCUtil.SetGameobjectActive(this.m_objTryBtnDisable, !flag2);
					NKCUtil.SetLabelText(this.m_txtTryBtn, NKCStringTable.GetString("SI_BINGO_BUTTON_GACHA_TEXT", false));
					NKCUtil.SetLabelTextColor(this.m_txtTryBtn, flag2 ? this.m_colorTryEnable : this.m_colorTryDisable);
					NKCUtil.SetLabelTextColor(this.m_txtTryItemCount, flag2 ? this.m_colorTryEnable : this.m_colorTryDisable);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objTryBtnEnable, true);
				NKCUtil.SetGameobjectActive(this.m_objTryBtnDisable, false);
				NKCUtil.SetLabelText(this.m_txtTryBtn, NKCStringTable.GetString("SI_BINGO_BUTTON_SHOP", false));
				NKCUtil.SetLabelTextColor(this.m_txtTryBtn, this.m_colorTryEnable);
				NKCUtil.SetLabelTextColor(this.m_txtTryItemCount, this.m_colorTryEnable);
			}
		}

		// Token: 0x06008CD9 RID: 36057 RVA: 0x002FE71C File Offset: 0x002FC91C
		private void UpdateLastReward(EventBingo bingoData)
		{
			if (this.m_lastReward != null)
			{
				NKMEventBingoRewardTemplet bingoLastRewardTemplet = NKMEventManager.GetBingoLastRewardTemplet(this.m_bingoTemplet.m_EventID);
				if (bingoLastRewardTemplet == null)
				{
					return;
				}
				bool flag = bingoData.m_bingoInfo.rewardList.Contains(bingoLastRewardTemplet.ZeroBaseTileIndex);
				this.m_lastReward.SetEventGet(flag);
				this.m_lastReward.SetDisable(flag, "");
			}
		}

		// Token: 0x06008CDA RID: 36058 RVA: 0x002FE780 File Offset: 0x002FC980
		private void UpdateRedDot()
		{
			NKCUtil.SetGameobjectActive(this.m_objMissionRedDot, NKMEventManager.CheckRedDotBingoMission(this.m_bingoTemplet.m_EventID));
			NKCUtil.SetGameobjectActive(this.m_objRewardRedDot, NKMEventManager.CheckRedDotBingoSet(this.m_bingoTemplet.m_EventID));
		}

		// Token: 0x06008CDB RID: 36059 RVA: 0x002FE7B8 File Offset: 0x002FC9B8
		private void InitBingoSlot(int bingoSize)
		{
			if (this.m_listSlot.Count > 0)
			{
				return;
			}
			int num = (int)Math.Pow((double)bingoSize, 2.0);
			for (int i = 0; i < num; i++)
			{
				NKCUIBingoSlot nkcuibingoSlot = UnityEngine.Object.Instantiate<NKCUIBingoSlot>(this.m_prefabSlot, this.m_gridSlotParent.transform);
				nkcuibingoSlot.transform.localScale = Vector3.one;
				nkcuibingoSlot.gameObject.SetActive(true);
				nkcuibingoSlot.Init(new NKCUIBingoSlot.OnClick(this.OnMarkTile));
				this.m_listSlot.Add(nkcuibingoSlot);
			}
			this.m_gridSlotParent.constraintCount = bingoSize;
		}

		// Token: 0x06008CDC RID: 36060 RVA: 0x002FE850 File Offset: 0x002FCA50
		private void SetSlotSpecialMode(bool bMode)
		{
			NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
			for (int i = 0; i < this.m_listSlot.Count; i++)
			{
				this.m_listSlot[i].SetSpecialMode(bMode);
			}
		}

		// Token: 0x06008CDD RID: 36061 RVA: 0x002FE898 File Offset: 0x002FCA98
		private void SetSlotSelectFx(int index)
		{
			for (int i = 0; i < this.m_listSlot.Count; i++)
			{
				this.m_listSlot[i].SetSelectFx(i == index);
			}
		}

		// Token: 0x06008CDE RID: 36062 RVA: 0x002FE8D0 File Offset: 0x002FCAD0
		private void SetSlotGetFx(int index)
		{
			if (index < this.m_listSlot.Count)
			{
				this.m_listSlot[index].SetGetFx(false);
				this.m_listSlot[index].SetGetFx(true);
			}
		}

		// Token: 0x06008CDF RID: 36063 RVA: 0x002FE904 File Offset: 0x002FCB04
		private void OnTouchGuidePopup()
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			if (this.m_specialMode)
			{
				return;
			}
			NKCPopupEventHelp.Instance.Open(this.m_bingoTemplet.m_EventID);
		}

		// Token: 0x06008CE0 RID: 36064 RVA: 0x002FE92E File Offset: 0x002FCB2E
		private void OnTouchMissionPopup()
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			if (this.m_specialMode)
			{
				return;
			}
			NKCPopupEventMission.Instance.Open(this.m_bingoTemplet, new NKCPopupEventMission.CheckTime(base.CheckEventTime), new NKCPopupEventMission.OnComplete(this.OnCompleteMission));
		}

		// Token: 0x06008CE1 RID: 36065 RVA: 0x002FE96B File Offset: 0x002FCB6B
		private void OnTouchRewardPopup()
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			if (this.m_specialMode)
			{
				return;
			}
			NKCPopupEventBingoReward.Instance.Open(this.m_bingoTemplet.m_EventID, new NKCPopupEventBingoReward.OnComplete(this.OnTouchBingoReward));
		}

		// Token: 0x06008CE2 RID: 36066 RVA: 0x002FE9A1 File Offset: 0x002FCBA1
		private void MoveToShop()
		{
			if (string.IsNullOrEmpty(this.m_strMoveShopTabWhenComplete))
			{
				this.m_strMoveShopTabWhenComplete = "TAB_HR,7";
			}
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHOP, this.m_strMoveShopTabWhenComplete, false);
		}

		// Token: 0x06008CE3 RID: 36067 RVA: 0x002FE9CC File Offset: 0x002FCBCC
		private void OnTouchTry()
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			if (this.m_specialMode)
			{
				return;
			}
			EventBingo bingoData = NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
			if (bingoData != null && !bingoData.IsRemainNum())
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_bingoTemplet.m_BingoTryItemID);
				if (itemMiscTempletByID != null && !string.IsNullOrEmpty(this.m_strMoveShopTabWhenComplete))
				{
					string @string = NKCStringTable.GetString("SI_DP_BINGO_MOVETO_SHOP_REMAIN_EXCHANGE", new object[]
					{
						itemMiscTempletByID.GetItemName()
					});
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, @string, new NKCPopupOKCancel.OnButton(this.MoveToShop), null, false);
					return;
				}
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_EVENT_BINGO_COMPLETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			else
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null || !nkmuserData.CheckPrice(this.m_bingoTemplet.m_BingoTryItemValue, this.m_bingoTemplet.m_BingoTryItemID))
				{
					NKCShopManager.OpenItemLackPopup(this.m_bingoTemplet.m_BingoTryItemID, this.m_bingoTemplet.m_BingoTryItemValue);
					return;
				}
				if (NKMItemManager.GetItemMiscTempletByID(this.m_bingoTemplet.m_BingoTryItemID) == null)
				{
					Debug.LogError(string.Format("item templet is null - {0}", this.m_bingoTemplet.m_BingoTryItemID));
					return;
				}
				NKCPacketSender.Send_NKMPacket_EVENT_BINGO_RANDOM_MARK_REQ(this.m_bingoTemplet.m_EventID);
				return;
			}
		}

		// Token: 0x06008CE4 RID: 36068 RVA: 0x002FEAF8 File Offset: 0x002FCCF8
		private void OnTouchSpecialTry()
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			EventBingo bingoData = NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
			int mileage = bingoData.m_bingoInfo.mileage;
			int bingoSpecialTryRequireCnt = this.m_bingoTemplet.m_BingoSpecialTryRequireCnt;
			if (mileage < bingoSpecialTryRequireCnt)
			{
				return;
			}
			if (!bingoData.IsRemainNum())
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_EVENT_BINGO_COMPLETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (this.m_specialMode)
			{
				this.m_specialMode = false;
			}
			else
			{
				this.m_specialMode = true;
			}
			this.UpdateSpecialButton(bingoData);
			this.UpdateTryButton(bingoData);
			this.UpdateLine(bingoData);
			this.UpdateRewardSlot(bingoData);
			this.m_specialIndex = -1;
			this.SetSlotSpecialMode(this.m_specialMode);
			this.SetSlotSelectFx(this.m_specialIndex);
			NKCUtil.SetGameobjectActive(this.m_objSpacialMode, this.m_specialMode);
		}

		// Token: 0x06008CE5 RID: 36069 RVA: 0x002FEBBC File Offset: 0x002FCDBC
		private void OnMarkTile(int index)
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			if (!this.m_specialMode)
			{
				Debug.LogError("bingo - 확정뽑기 모드가 아닌데 어케함?");
				return;
			}
			if (index == this.m_specialIndex)
			{
				EventBingo bingoData = NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
				if (bingoData != null)
				{
					int tileValue = bingoData.GetTileValue(index);
					string content = string.Format(NKCUtilString.GET_STRING_EVENT_BINGO_USE_MILEAGE, this.m_bingoTemplet.m_BingoSpecialTryRequireCnt, tileValue);
					string strPoint = string.Format(NKCUtilString.GET_STRING_EVENT_BINGO_REMAIN_MILEAGE, bingoData.m_bingoInfo.mileage);
					NKCPopupResourceTextConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, content, strPoint, delegate
					{
						NKCPacketSender.Send_NKMPacket_EVENT_BINGO_INDEX_MARK_REQ(this.m_bingoTemplet.m_EventID, index);
					}, null);
					return;
				}
			}
			else
			{
				this.m_specialIndex = index;
				this.SetSlotSelectFx(index);
			}
		}

		// Token: 0x06008CE6 RID: 36070 RVA: 0x002FECA4 File Offset: 0x002FCEA4
		private void OnTouchBingoReward(int rewardIndex)
		{
			if (!base.CheckEventTime(true))
			{
				return;
			}
			if (this.m_specialMode)
			{
				return;
			}
			if (NKMEventManager.IsReceiveableBingoReward(this.m_bingoTemplet.m_EventID, rewardIndex))
			{
				Debug.Log(string.Format("bingo rewardIndex : {0}", rewardIndex));
				NKCPacketSender.Send_NKMPacket_EVENT_BINGO_REWARD_REQ(this.m_bingoTemplet.m_EventID, rewardIndex);
			}
		}

		// Token: 0x06008CE7 RID: 36071 RVA: 0x002FECFD File Offset: 0x002FCEFD
		private void OnCompleteMission(int eventID, int tileIndex)
		{
			if (eventID != this.m_bingoTemplet.m_EventID)
			{
				return;
			}
			this.GetTile(tileIndex, false);
		}

		// Token: 0x06008CE8 RID: 36072 RVA: 0x002FED16 File Offset: 0x002FCF16
		public void GetTile(int tileIndex, bool bRandom)
		{
			if (!bRandom)
			{
				this.SetSlotGetFx(tileIndex);
				this.m_specialMode = false;
				this.m_specialIndex = -1;
				this.Refresh();
				return;
			}
			this.SetRandomFX(tileIndex);
		}

		// Token: 0x06008CE9 RID: 36073 RVA: 0x002FED40 File Offset: 0x002FCF40
		public void SetRandomFX(int tileIndex)
		{
			EventBingo bingoData = NKMEventManager.GetBingoData(this.m_bingoTemplet.m_EventID);
			if (bingoData != null)
			{
				int tileValue = bingoData.GetTileValue(tileIndex);
				if (tileValue <= 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objGetNum, false);
					return;
				}
				bool flag = false;
				if (tileIndex < this.m_listSlot.Count)
				{
					flag = this.m_listSlot[tileIndex].IsHas;
				}
				NKCUtil.SetGameobjectActive(this.m_objAlreadyGetNum, flag);
				NKCUtil.SetLabelText(this.m_txtGetNum, tileValue.ToString());
				NKCUtil.SetGameobjectActive(this.m_objGetNum, true);
				if (this.m_coroutineGetNum != null)
				{
					base.StopCoroutine(this.m_coroutineGetNum);
				}
				this.m_coroutineGetNum = base.StartCoroutine(this.ProcessGetNum(tileIndex, flag));
			}
		}

		// Token: 0x06008CEA RID: 36074 RVA: 0x002FEDF1 File Offset: 0x002FCFF1
		private IEnumerator ProcessGetNum(int tileIndex, bool bAlreadyGet)
		{
			this.m_bTouch = false;
			this.m_bPrecessGetNum = true;
			this.m_waitSeconds = 0f;
			NKCUIManager.SetScreenInputBlock(true);
			if (this.m_aniGetNum != null)
			{
				yield return this.PlayGetNumIntro();
				while (!this.m_bTouch && this.m_waitSeconds < 2f)
				{
					this.m_waitSeconds += Time.deltaTime;
					yield return null;
				}
				yield return this.PlayGetNumOutro();
			}
			if (!bAlreadyGet)
			{
				this.SetSlotGetFx(tileIndex);
			}
			this.Refresh();
			NKCUIManager.SetScreenInputBlock(false);
			this.m_bPrecessGetNum = false;
			yield break;
		}

		// Token: 0x06008CEB RID: 36075 RVA: 0x002FEE0E File Offset: 0x002FD00E
		private IEnumerator PlayGetNumIntro()
		{
			if (this.m_aniGetNum != null)
			{
				this.m_aniGetNum.Play("NKM_UI_EVENT_BINGO_GACHA_INTRO");
				yield return null;
				while (!this.m_bTouch && this.m_aniGetNum.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
				this.m_aniGetNum.Play("NKM_UI_EVENT_BINGO_GACHA_ROOP");
			}
			yield break;
		}

		// Token: 0x06008CEC RID: 36076 RVA: 0x002FEE1D File Offset: 0x002FD01D
		private IEnumerator PlayGetNumOutro()
		{
			if (this.m_aniGetNum != null)
			{
				this.m_aniGetNum.Play("NKM_UI_EVENT_BINGO_GACHA_OUTRO");
				yield return null;
				while (!this.m_bTouch && this.m_aniGetNum.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06008CED RID: 36077 RVA: 0x002FEE2C File Offset: 0x002FD02C
		private void Update()
		{
			if (Input.anyKeyDown)
			{
				this.m_bTouch = true;
			}
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x002FEE3C File Offset: 0x002FD03C
		public void MarkBingo(List<NKMBingoTile> bingoList, bool bRandom)
		{
			if (bingoList == null)
			{
				return;
			}
			for (int i = 0; i < bingoList.Count; i++)
			{
				if (bingoList[i].eventId == this.m_bingoTemplet.m_EventID)
				{
					this.GetTile(bingoList[i].tileIndex, bRandom);
					return;
				}
			}
		}

		// Token: 0x040079A4 RID: 31140
		[Header("버튼")]
		public NKCUIComStateButton m_btnGuidePopup;

		// Token: 0x040079A5 RID: 31141
		public NKCUIComStateButton m_btnMissionPopup;

		// Token: 0x040079A6 RID: 31142
		public NKCUIComStateButton m_btnRewardPopup;

		// Token: 0x040079A7 RID: 31143
		public NKCUIComStateButton m_btnTry;

		// Token: 0x040079A8 RID: 31144
		public NKCUIComStateButton m_btnSpecialTry;

		// Token: 0x040079A9 RID: 31145
		[Header("빙고판(가로,세로,↘,↙)")]
		public GridLayoutGroup m_gridSlotParent;

		// Token: 0x040079AA RID: 31146
		public NKCUIBingoSlot m_prefabSlot;

		// Token: 0x040079AB RID: 31147
		public List<NKCUISlot> m_listLineReward;

		// Token: 0x040079AC RID: 31148
		public List<GameObject> m_listCompleteLine;

		// Token: 0x040079AD RID: 31149
		public GameObject m_objSpacialMode;

		// Token: 0x040079AE RID: 31150
		[Header("뽑기 버튼")]
		public Text m_txtTryBtn;

		// Token: 0x040079AF RID: 31151
		public GameObject m_objTryBtnEnable;

		// Token: 0x040079B0 RID: 31152
		public GameObject m_objTryBtnDisable;

		// Token: 0x040079B1 RID: 31153
		public Color m_colorTryEnable;

		// Token: 0x040079B2 RID: 31154
		public Color m_colorTryDisable;

		// Token: 0x040079B3 RID: 31155
		public Image m_imgTryItemIcon;

		// Token: 0x040079B4 RID: 31156
		public Text m_txtTryItemCount;

		// Token: 0x040079B5 RID: 31157
		public Text m_lbTryItemTotalCount;

		// Token: 0x040079B6 RID: 31158
		public string m_strMoveShopTabWhenComplete = "TAB_HR,7";

		// Token: 0x040079B7 RID: 31159
		private const string DEFAULT_SHOP_TAB = "TAB_HR,7";

		// Token: 0x040079B8 RID: 31160
		[Header("확정 뽑기 버튼")]
		public Text m_txtSpecialBtn;

		// Token: 0x040079B9 RID: 31161
		public GameObject m_objSpecialBtnEnable;

		// Token: 0x040079BA RID: 31162
		public GameObject m_objSpecialBtnDisable;

		// Token: 0x040079BB RID: 31163
		public GameObject m_objSpecialBtnCancel;

		// Token: 0x040079BC RID: 31164
		public Color m_colorSpecialEnable;

		// Token: 0x040079BD RID: 31165
		public Color m_colorSpecialDisable;

		// Token: 0x040079BE RID: 31166
		public Color m_colorSpecialCancel;

		// Token: 0x040079BF RID: 31167
		[Header("마일리지")]
		public Text m_txtMileage;

		// Token: 0x040079C0 RID: 31168
		[Header("숫자 획득 연출")]
		public GameObject m_objGetNum;

		// Token: 0x040079C1 RID: 31169
		public Animator m_aniGetNum;

		// Token: 0x040079C2 RID: 31170
		public Text m_txtGetNum;

		// Token: 0x040079C3 RID: 31171
		public GameObject m_objAlreadyGetNum;

		// Token: 0x040079C4 RID: 31172
		[Header("최종 보상")]
		public NKCUISlot m_lastReward;

		// Token: 0x040079C5 RID: 31173
		[Header("빨콩")]
		public GameObject m_objMissionRedDot;

		// Token: 0x040079C6 RID: 31174
		public GameObject m_objRewardRedDot;

		// Token: 0x040079C7 RID: 31175
		private List<NKCUIBingoSlot> m_listSlot = new List<NKCUIBingoSlot>();

		// Token: 0x040079C8 RID: 31176
		private NKMEventBingoTemplet m_bingoTemplet;

		// Token: 0x040079C9 RID: 31177
		private bool m_specialMode;

		// Token: 0x040079CA RID: 31178
		private int m_specialIndex = -1;

		// Token: 0x040079CB RID: 31179
		private bool m_bTouch;

		// Token: 0x040079CC RID: 31180
		private bool m_bPrecessGetNum;

		// Token: 0x040079CD RID: 31181
		private float m_waitSeconds;

		// Token: 0x040079CE RID: 31182
		private Coroutine m_coroutineGetNum;

		// Token: 0x040079CF RID: 31183
		private const float WAIT_TIME = 2f;
	}
}
