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
	// Token: 0x02000BBD RID: 3005
	public class NKCUIPopupFierceBattleRewardInfo : NKCUIBase
	{
		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06008ABF RID: 35519 RVA: 0x002F2B20 File Offset: 0x002F0D20
		public static NKCUIPopupFierceBattleRewardInfo Instance
		{
			get
			{
				if (NKCUIPopupFierceBattleRewardInfo.m_Instance == null)
				{
					NKCUIPopupFierceBattleRewardInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupFierceBattleRewardInfo>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_REWARD_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupFierceBattleRewardInfo.CleanupInstance)).GetInstance<NKCUIPopupFierceBattleRewardInfo>();
					NKCUIPopupFierceBattleRewardInfo.m_Instance.Init();
				}
				return NKCUIPopupFierceBattleRewardInfo.m_Instance;
			}
		}

		// Token: 0x06008AC0 RID: 35520 RVA: 0x002F2B6F File Offset: 0x002F0D6F
		private static void CleanupInstance()
		{
			NKCUIPopupFierceBattleRewardInfo.m_Instance.Clear();
			NKCUIPopupFierceBattleRewardInfo.m_Instance = null;
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06008AC1 RID: 35521 RVA: 0x002F2B81 File Offset: 0x002F0D81
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupFierceBattleRewardInfo.m_Instance != null && NKCUIPopupFierceBattleRewardInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008AC2 RID: 35522 RVA: 0x002F2B9C File Offset: 0x002F0D9C
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupFierceBattleRewardInfo.m_Instance != null && NKCUIPopupFierceBattleRewardInfo.m_Instance.IsOpen)
			{
				NKCUIPopupFierceBattleRewardInfo.m_Instance.Close();
			}
		}

		// Token: 0x06008AC3 RID: 35523 RVA: 0x002F2BC1 File Offset: 0x002F0DC1
		private void OnDestroy()
		{
			NKCUIPopupFierceBattleRewardInfo.m_Instance = null;
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06008AC4 RID: 35524 RVA: 0x002F2BC9 File Offset: 0x002F0DC9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06008AC5 RID: 35525 RVA: 0x002F2BCC File Offset: 0x002F0DCC
		public override string MenuName
		{
			get
			{
				return "POPUP_FIERCE_BATTLE_REWARD_INFO";
			}
		}

		// Token: 0x06008AC6 RID: 35526 RVA: 0x002F2BD3 File Offset: 0x002F0DD3
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008AC7 RID: 35527 RVA: 0x002F2BE4 File Offset: 0x002F0DE4
		private void Init()
		{
			if (this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_Bg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCUIPopupFierceBattleRewardInfo.CheckInstanceAndClose();
				});
				this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_Bg.triggers.Add(entry);
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CLOSE_BUTTON, new UnityAction(NKCUIPopupFierceBattleRewardInfo.CheckInstanceAndClose));
			if (this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect != null)
			{
				this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.dOnGetObject += this.GetSlot;
				this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.dOnProvideData += this.ProvideData;
				this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect, null);
			}
		}

		// Token: 0x06008AC8 RID: 35528 RVA: 0x002F2CCC File Offset: 0x002F0ECC
		public void Open()
		{
			int totalCount = 0;
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null && nkcfierceBattleSupportDataMgr.FierceTemplet != null)
			{
				if (NKMFierceRankRewardTemplet.NumericRankGroupMap.ContainsKey(nkcfierceBattleSupportDataMgr.FierceTemplet.RankRewardGroupID))
				{
					this.m_lstRankRewardTemplet = NKMFierceRankRewardTemplet.NumericRankGroupMap[nkcfierceBattleSupportDataMgr.FierceTemplet.RankRewardGroupID].ToList<NKMFierceRankRewardTemplet>();
					if (NKMFierceRankRewardTemplet.PercentRankGroupMap.ContainsKey(nkcfierceBattleSupportDataMgr.FierceTemplet.RankRewardGroupID))
					{
						this.m_lstRankRewardTemplet.AddRange(NKMFierceRankRewardTemplet.PercentRankGroupMap[nkcfierceBattleSupportDataMgr.FierceTemplet.RankRewardGroupID].ToList<NKMFierceRankRewardTemplet>());
					}
					from e in this.m_lstRankRewardTemplet
					orderby e.ShowIndex
					select e;
					if (this.m_lstRankRewardTemplet.Count > 0)
					{
						totalCount = this.m_lstRankRewardTemplet.Count;
					}
				}
				NKCUtil.SetLabelText(this.m_REWARD_INFO_MyRank_Text, nkcfierceBattleSupportDataMgr.GetRankingTotalDesc());
			}
			this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.TotalCount = totalCount;
			this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.SetIndexPosition(0);
			this.m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect.RefreshCells(true);
			base.UIOpened(true);
		}

		// Token: 0x06008AC9 RID: 35529 RVA: 0x002F2DF0 File Offset: 0x002F0FF0
		public void Clear()
		{
			for (int i = 0; i < this.m_slotList.Count; i++)
			{
				this.m_slotList[i].DestoryInstance();
			}
			this.m_slotList.Clear();
			while (this.m_slotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_slotPool.Pop();
				if (rectTransform != null)
				{
					NKCUIFierceBattleRewardInfoSlot component = rectTransform.GetComponent<NKCUIFierceBattleRewardInfoSlot>();
					if (component != null)
					{
						component.DestoryInstance();
					}
				}
			}
		}

		// Token: 0x06008ACA RID: 35530 RVA: 0x002F2E6C File Offset: 0x002F106C
		private RectTransform GetSlot(int index)
		{
			if (this.m_slotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_slotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUIFierceBattleRewardInfoSlot newInstance = NKCUIFierceBattleRewardInfoSlot.GetNewInstance(this.m_slotParent);
			if (newInstance == null)
			{
				return null;
			}
			newInstance.transform.localScale = Vector3.one;
			this.m_slotList.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008ACB RID: 35531 RVA: 0x002F2ED3 File Offset: 0x002F10D3
		public void ReturnSlot(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			this.m_slotPool.Push(tr.GetComponent<RectTransform>());
		}

		// Token: 0x06008ACC RID: 35532 RVA: 0x002F2EFC File Offset: 0x002F10FC
		public void ProvideData(Transform tr, int index)
		{
			NKCUIFierceBattleRewardInfoSlot component = tr.GetComponent<NKCUIFierceBattleRewardInfoSlot>();
			if (component != null && this.m_lstRankRewardTemplet.Count > index)
			{
				component.SetData(this.m_lstRankRewardTemplet[index]);
			}
		}

		// Token: 0x0400777B RID: 30587
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x0400777C RID: 30588
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_REWARD_INFO";

		// Token: 0x0400777D RID: 30589
		private static NKCUIPopupFierceBattleRewardInfo m_Instance;

		// Token: 0x0400777E RID: 30590
		public EventTrigger m_POPUP_FIERCE_BATTLE_REWARD_INFO_Bg;

		// Token: 0x0400777F RID: 30591
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSE_BUTTON;

		// Token: 0x04007780 RID: 30592
		public Text m_REWARD_INFO_MyRank_Text;

		// Token: 0x04007781 RID: 30593
		public LoopScrollRect m_POPUP_FIERCE_BATTLE_REWARD_INFO_ScrollRect;

		// Token: 0x04007782 RID: 30594
		private List<NKMFierceRankRewardTemplet> m_lstRankRewardTemplet;

		// Token: 0x04007783 RID: 30595
		public Transform m_slotParent;

		// Token: 0x04007784 RID: 30596
		private List<NKCUIFierceBattleRewardInfoSlot> m_slotList = new List<NKCUIFierceBattleRewardInfoSlot>();

		// Token: 0x04007785 RID: 30597
		private Stack<RectTransform> m_slotPool = new Stack<RectTransform>();
	}
}
