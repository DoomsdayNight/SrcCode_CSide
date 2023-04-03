using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BBE RID: 3006
	public class NKCUIPopupFierceBattleScoreReward : NKCUIBase
	{
		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06008ACE RID: 35534 RVA: 0x002F2F58 File Offset: 0x002F1158
		public static NKCUIPopupFierceBattleScoreReward Instance
		{
			get
			{
				if (NKCUIPopupFierceBattleScoreReward.m_Instance == null)
				{
					NKCUIPopupFierceBattleScoreReward.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupFierceBattleScoreReward>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupFierceBattleScoreReward.CleanupInstance)).GetInstance<NKCUIPopupFierceBattleScoreReward>();
					NKCUIPopupFierceBattleScoreReward.m_Instance.Init();
				}
				return NKCUIPopupFierceBattleScoreReward.m_Instance;
			}
		}

		// Token: 0x06008ACF RID: 35535 RVA: 0x002F2FA7 File Offset: 0x002F11A7
		private static void CleanupInstance()
		{
			NKCUIPopupFierceBattleScoreReward.m_Instance.Clear();
			NKCUIPopupFierceBattleScoreReward.m_Instance = null;
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06008AD0 RID: 35536 RVA: 0x002F2FB9 File Offset: 0x002F11B9
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupFierceBattleScoreReward.m_Instance != null && NKCUIPopupFierceBattleScoreReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008AD1 RID: 35537 RVA: 0x002F2FD4 File Offset: 0x002F11D4
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupFierceBattleScoreReward.m_Instance != null && NKCUIPopupFierceBattleScoreReward.m_Instance.IsOpen)
			{
				NKCUIPopupFierceBattleScoreReward.m_Instance.Close();
			}
		}

		// Token: 0x06008AD2 RID: 35538 RVA: 0x002F2FF9 File Offset: 0x002F11F9
		private void OnDestroy()
		{
			NKCUIPopupFierceBattleScoreReward.m_Instance = null;
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x002F3001 File Offset: 0x002F1201
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06008AD4 RID: 35540 RVA: 0x002F300F File Offset: 0x002F120F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06008AD5 RID: 35541 RVA: 0x002F3012 File Offset: 0x002F1212
		public override string MenuName
		{
			get
			{
				return "FIERCE_BATTLE_TOTAL_SCORE_REWARD";
			}
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x002F301C File Offset: 0x002F121C
		private void Init()
		{
			if (this.m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_Bg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCUIPopupFierceBattleScoreReward.CheckInstanceAndClose();
				});
				this.m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_Bg.triggers.Add(entry);
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CLOSE_BUTTON, new UnityAction(NKCUIPopupFierceBattleScoreReward.CheckInstanceAndClose));
			if (this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect != null)
			{
				this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.dOnGetObject += this.GetSlot;
				this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.dOnProvideData += this.ProvideData;
				this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect, null);
			}
			if (this.m_lbAllReceiveReward != null)
			{
				this.m_AllReceiveRewardOriginalColor = this.m_lbAllReceiveReward.color;
			}
			NKCUtil.SetBindFunction(this.m_csbtnAllReceiveReward, new UnityAction(this.OnClickRecevieAllReward));
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x002F3138 File Offset: 0x002F1338
		public void Open()
		{
			this.m_FierceMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			this.m_iTotalPoint = this.m_FierceMgr.GetTotalPoint();
			int pointRewardGroupID = this.m_FierceMgr.FierceTemplet.PointRewardGroupID;
			if (NKMFiercePointRewardTemplet.Groups.ContainsKey(pointRewardGroupID))
			{
				this.m_lstPointReward = NKMFiercePointRewardTemplet.Groups[pointRewardGroupID].ToList<NKMFiercePointRewardTemplet>();
				this.m_lstPointReward.Sort(new NKCUIPopupFierceBattleScoreReward.PointRewardCompare());
			}
			this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.TotalCount = this.m_lstPointReward.Count;
			this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.SetIndexPosition(0);
			this.m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect.RefreshCells(true);
			NKCUtil.SetLabelText(this.m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_MYSCORE_TEXT_1, this.m_FierceMgr.GetTotalPoint().ToString());
			NKCUtil.SetLabelText(this.m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_MYSCORE_TEXT_2, this.m_FierceMgr.GetRankingTotalDesc());
			if (!this.m_FierceMgr.IsCanReceivePointReward())
			{
				NKCUtil.SetImageSprite(this.m_imgAllReceiveReward, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.m_lbAllReceiveReward, NKCUtil.GetColor("#212122"));
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgAllReceiveReward, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
				NKCUtil.SetLabelTextColor(this.m_lbAllReceiveReward, this.m_AllReceiveRewardOriginalColor);
			}
			base.UIOpened(true);
		}

		// Token: 0x06008AD8 RID: 35544 RVA: 0x002F326C File Offset: 0x002F146C
		private void OnClickRecevieAllReward()
		{
			if (!this.m_FierceMgr.IsCanReceivePointReward())
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_REQ();
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x002F3284 File Offset: 0x002F1484
		private void Clear()
		{
			for (int i = 0; i < this.m_lstScoreRewardSlot.Count; i++)
			{
				this.m_lstScoreRewardSlot[i].DestoryInstance();
			}
			this.m_lstScoreRewardSlot.Clear();
			while (this.m_stkScoreRewardSlot.Count > 0)
			{
				NKCUIPopupFierceBattleScoreRewardSlot nkcuipopupFierceBattleScoreRewardSlot = this.m_stkScoreRewardSlot.Pop();
				if (nkcuipopupFierceBattleScoreRewardSlot != null)
				{
					nkcuipopupFierceBattleScoreRewardSlot.DestoryInstance();
				}
			}
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x002F32F0 File Offset: 0x002F14F0
		public RectTransform GetSlot(int index)
		{
			if (this.m_stkScoreRewardSlot.Count > 0)
			{
				NKCUIPopupFierceBattleScoreRewardSlot nkcuipopupFierceBattleScoreRewardSlot = this.m_stkScoreRewardSlot.Pop();
				if (nkcuipopupFierceBattleScoreRewardSlot != null)
				{
					return nkcuipopupFierceBattleScoreRewardSlot.GetComponent<RectTransform>();
				}
			}
			NKCUIPopupFierceBattleScoreRewardSlot newInstance = NKCUIPopupFierceBattleScoreRewardSlot.GetNewInstance(this.m_Content);
			if (newInstance != null)
			{
				this.m_lstScoreRewardSlot.Add(newInstance);
				return newInstance.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06008ADB RID: 35547 RVA: 0x002F3350 File Offset: 0x002F1550
		public void ReturnSlot(Transform tr)
		{
			NKCUIPopupFierceBattleScoreRewardSlot component = tr.GetComponent<NKCUIPopupFierceBattleScoreRewardSlot>();
			tr.SetParent(base.transform);
			this.m_stkScoreRewardSlot.Push(component);
		}

		// Token: 0x06008ADC RID: 35548 RVA: 0x002F337C File Offset: 0x002F157C
		public void ProvideData(Transform tr, int index)
		{
			if (index > this.m_lstPointReward.Count)
			{
				return;
			}
			NKCUIPopupFierceBattleScoreRewardSlot component = tr.GetComponent<NKCUIPopupFierceBattleScoreRewardSlot>();
			if (component != null)
			{
				NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE type;
				if (this.m_FierceMgr.IsReceivedPointReward(this.m_lstPointReward[index].FiercePointRewardID))
				{
					type = NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.COMPLETE;
				}
				else if (this.m_lstPointReward[index].Point <= this.m_iTotalPoint)
				{
					type = NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.CAN_RECEVIE;
				}
				else
				{
					type = NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.DISABLE;
				}
				component.SetData(this.m_lstPointReward[index], type);
			}
		}

		// Token: 0x04007786 RID: 30598
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x04007787 RID: 30599
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD";

		// Token: 0x04007788 RID: 30600
		private static NKCUIPopupFierceBattleScoreReward m_Instance;

		// Token: 0x04007789 RID: 30601
		public EventTrigger m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_Bg;

		// Token: 0x0400778A RID: 30602
		public LoopScrollRect m_POPUP_FIERCE_BATTLE_RANK_REWARD_SLOT_ScrollRect;

		// Token: 0x0400778B RID: 30603
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSE_BUTTON;

		// Token: 0x0400778C RID: 30604
		public Text m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_MYSCORE_TEXT_1;

		// Token: 0x0400778D RID: 30605
		public Text m_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_MYSCORE_TEXT_2;

		// Token: 0x0400778E RID: 30606
		private Color m_AllReceiveRewardOriginalColor;

		// Token: 0x0400778F RID: 30607
		public Image m_imgAllReceiveReward;

		// Token: 0x04007790 RID: 30608
		public Text m_lbAllReceiveReward;

		// Token: 0x04007791 RID: 30609
		public NKCUIComStateButton m_csbtnAllReceiveReward;

		// Token: 0x04007792 RID: 30610
		private List<NKMFiercePointRewardTemplet> m_lstPointReward;

		// Token: 0x04007793 RID: 30611
		private NKCFierceBattleSupportDataMgr m_FierceMgr;

		// Token: 0x04007794 RID: 30612
		private int m_iTotalPoint;

		// Token: 0x04007795 RID: 30613
		public RectTransform m_Content;

		// Token: 0x04007796 RID: 30614
		private Stack<NKCUIPopupFierceBattleScoreRewardSlot> m_stkScoreRewardSlot = new Stack<NKCUIPopupFierceBattleScoreRewardSlot>();

		// Token: 0x04007797 RID: 30615
		private List<NKCUIPopupFierceBattleScoreRewardSlot> m_lstScoreRewardSlot = new List<NKCUIPopupFierceBattleScoreRewardSlot>();

		// Token: 0x02001986 RID: 6534
		private class PointRewardCompare : IComparer<NKMFiercePointRewardTemplet>
		{
			// Token: 0x0600B92D RID: 47405 RVA: 0x0036C6F6 File Offset: 0x0036A8F6
			public int Compare(NKMFiercePointRewardTemplet x, NKMFiercePointRewardTemplet y)
			{
				return x.Step.CompareTo(y.Step);
			}
		}
	}
}
