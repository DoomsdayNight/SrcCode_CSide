using System;
using System.Collections.Generic;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC3 RID: 3011
	public class NKCPopupEventBingoReward : NKCUIBase
	{
		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06008B58 RID: 35672 RVA: 0x002F6308 File Offset: 0x002F4508
		public static NKCPopupEventBingoReward Instance
		{
			get
			{
				if (NKCPopupEventBingoReward.m_Instance == null)
				{
					NKCPopupEventBingoReward.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventBingoReward>("AB_UI_NKM_UI_EVENT", "NKM_UI_POPUP_EVENT_BINGO_REWARD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventBingoReward.CleanupInstance)).GetInstance<NKCPopupEventBingoReward>();
					NKCPopupEventBingoReward.m_Instance.Init();
				}
				return NKCPopupEventBingoReward.m_Instance;
			}
		}

		// Token: 0x06008B59 RID: 35673 RVA: 0x002F6357 File Offset: 0x002F4557
		private static void CleanupInstance()
		{
			NKCPopupEventBingoReward.m_Instance = null;
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06008B5A RID: 35674 RVA: 0x002F635F File Offset: 0x002F455F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventBingoReward.m_Instance != null && NKCPopupEventBingoReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x002F637A File Offset: 0x002F457A
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventBingoReward.m_Instance != null && NKCPopupEventBingoReward.m_Instance.IsOpen)
			{
				NKCPopupEventBingoReward.m_Instance.Close();
			}
		}

		// Token: 0x06008B5C RID: 35676 RVA: 0x002F639F File Offset: 0x002F459F
		private void OnDestroy()
		{
			NKCPopupEventBingoReward.m_Instance = null;
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06008B5D RID: 35677 RVA: 0x002F63A7 File Offset: 0x002F45A7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06008B5E RID: 35678 RVA: 0x002F63AA File Offset: 0x002F45AA
		public override string MenuName
		{
			get
			{
				return "Bingo Reward";
			}
		}

		// Token: 0x06008B5F RID: 35679 RVA: 0x002F63B1 File Offset: 0x002F45B1
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008B60 RID: 35680 RVA: 0x002F63C0 File Offset: 0x002F45C0
		private void Init()
		{
			if (this.m_close != null)
			{
				this.m_close.PointerClick.RemoveAllListeners();
				this.m_close.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_back != null)
			{
				this.m_back.PointerClick.RemoveAllListeners();
				this.m_back.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					this.m_listSlot[i].Init(new NKCPopupEventBingoRewardSlot.OnTouchComplete(this.OnTouchComplete));
				}
			}
			if (this.m_rewardSlot != null)
			{
				this.m_rewardSlot.Init();
			}
		}

		// Token: 0x06008B61 RID: 35681 RVA: 0x002F6495 File Offset: 0x002F4695
		public void Open(int eventID, NKCPopupEventBingoReward.OnComplete onComplete)
		{
			this.m_eventID = eventID;
			this.dOnComplete = onComplete;
			this.SetData(this.m_eventID);
			base.UIOpened(true);
		}

		// Token: 0x06008B62 RID: 35682 RVA: 0x002F64B8 File Offset: 0x002F46B8
		public void Refresh()
		{
			this.SetData(this.m_eventID);
		}

		// Token: 0x06008B63 RID: 35683 RVA: 0x002F64C8 File Offset: 0x002F46C8
		private void SetData(int eventID)
		{
			List<NKMEventBingoRewardTemplet> list = NKMEventManager.GetBingoRewardTempletList(eventID);
			if (list == null)
			{
				return;
			}
			EventBingo bingoData = NKMEventManager.GetBingoData(eventID);
			if (bingoData == null)
			{
				return;
			}
			list = list.FindAll((NKMEventBingoRewardTemplet v) => v.m_BingoCompletType == BingoCompleteType.LINE_SET);
			int count = bingoData.GetBingoLine().Count;
			if (this.m_listSlot != null)
			{
				for (int i = 0; i < this.m_listSlot.Count; i++)
				{
					NKCPopupEventBingoRewardSlot nkcpopupEventBingoRewardSlot = this.m_listSlot[i];
					if (i < list.Count)
					{
						NKMEventBingoRewardTemplet nkmeventBingoRewardTemplet = list[i];
						nkcpopupEventBingoRewardSlot.SetData(nkmeventBingoRewardTemplet, count, bingoData.m_bingoInfo.rewardList.Contains(nkmeventBingoRewardTemplet.ZeroBaseTileIndex));
						NKCUtil.SetGameobjectActive(nkcpopupEventBingoRewardSlot, true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcpopupEventBingoRewardSlot, false);
					}
				}
			}
			if (this.m_rewardSlot != null)
			{
				NKMEventBingoRewardTemplet bingoLastRewardTemplet = NKMEventManager.GetBingoLastRewardTemplet(eventID);
				if (bingoLastRewardTemplet != null)
				{
					NKMRewardInfo nkmrewardInfo = bingoLastRewardTemplet.rewards[0];
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count, 0);
					this.m_rewardSlot.SetData(data, true, null);
					bool flag = bingoData.m_bingoInfo.rewardList.Contains(bingoLastRewardTemplet.ZeroBaseTileIndex);
					this.m_rewardSlot.SetEventGet(flag);
					this.m_rewardSlot.SetDisable(flag, "");
					NKCUtil.SetLabelText(this.m_txtRewardName, this.m_rewardSlot.GetName());
					NKCUtil.SetGameobjectActive(this.m_rewardSlot, true);
					return;
				}
				NKCUtil.SetLabelText(this.m_txtRewardName, "");
				NKCUtil.SetGameobjectActive(this.m_rewardSlot, false);
			}
		}

		// Token: 0x06008B64 RID: 35684 RVA: 0x002F6660 File Offset: 0x002F4860
		private void OnTouchComplete(NKMEventBingoRewardTemplet rewardTemplet)
		{
			if (rewardTemplet == null)
			{
				return;
			}
			NKCPopupEventBingoReward.OnComplete onComplete = this.dOnComplete;
			if (onComplete == null)
			{
				return;
			}
			onComplete(rewardTemplet.ZeroBaseTileIndex);
		}

		// Token: 0x04007830 RID: 30768
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT";

		// Token: 0x04007831 RID: 30769
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_BINGO_REWARD";

		// Token: 0x04007832 RID: 30770
		private static NKCPopupEventBingoReward m_Instance;

		// Token: 0x04007833 RID: 30771
		public NKCUIComStateButton m_close;

		// Token: 0x04007834 RID: 30772
		public NKCUIComButton m_back;

		// Token: 0x04007835 RID: 30773
		[Header("최종보상")]
		public NKCUISlot m_rewardSlot;

		// Token: 0x04007836 RID: 30774
		public Text m_txtRewardName;

		// Token: 0x04007837 RID: 30775
		[Header("리스트")]
		public List<NKCPopupEventBingoRewardSlot> m_listSlot;

		// Token: 0x04007838 RID: 30776
		private int m_eventID;

		// Token: 0x04007839 RID: 30777
		private NKCPopupEventBingoReward.OnComplete dOnComplete;

		// Token: 0x02001992 RID: 6546
		// (Invoke) Token: 0x0600B951 RID: 47441
		public delegate void OnComplete(int reward_index);
	}
}
