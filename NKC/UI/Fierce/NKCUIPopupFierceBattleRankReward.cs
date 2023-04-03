using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKM;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BBC RID: 3004
	public class NKCUIPopupFierceBattleRankReward : NKCUIBase
	{
		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06008AB4 RID: 35508 RVA: 0x002F2778 File Offset: 0x002F0978
		public static NKCUIPopupFierceBattleRankReward Instance
		{
			get
			{
				if (NKCUIPopupFierceBattleRankReward.m_Instance == null)
				{
					NKCUIPopupFierceBattleRankReward.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupFierceBattleRankReward>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_FINAL_REWARD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupFierceBattleRankReward.CleanupInstance)).GetInstance<NKCUIPopupFierceBattleRankReward>();
					NKCUIPopupFierceBattleRankReward.m_Instance.Init();
				}
				return NKCUIPopupFierceBattleRankReward.m_Instance;
			}
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x002F27C7 File Offset: 0x002F09C7
		private static void CleanupInstance()
		{
			NKCUIPopupFierceBattleRankReward.m_Instance = null;
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06008AB6 RID: 35510 RVA: 0x002F27CF File Offset: 0x002F09CF
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupFierceBattleRankReward.m_Instance != null && NKCUIPopupFierceBattleRankReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008AB7 RID: 35511 RVA: 0x002F27EA File Offset: 0x002F09EA
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupFierceBattleRankReward.m_Instance != null && NKCUIPopupFierceBattleRankReward.m_Instance.IsOpen)
			{
				NKCUIPopupFierceBattleRankReward.m_Instance.Close();
			}
		}

		// Token: 0x06008AB8 RID: 35512 RVA: 0x002F280F File Offset: 0x002F0A0F
		private void OnDestroy()
		{
			NKCUIPopupFierceBattleRankReward.m_Instance = null;
		}

		// Token: 0x06008AB9 RID: 35513 RVA: 0x002F2817 File Offset: 0x002F0A17
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06008ABA RID: 35514 RVA: 0x002F2825 File Offset: 0x002F0A25
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06008ABB RID: 35515 RVA: 0x002F2828 File Offset: 0x002F0A28
		public override string MenuName
		{
			get
			{
				return "NKM_UI_POPUP_FIERCE_BATTLE_FINAL_REWARD";
			}
		}

		// Token: 0x06008ABC RID: 35516 RVA: 0x002F2830 File Offset: 0x002F0A30
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_BUTTON, new UnityAction(NKCUIPopupFierceBattleRankReward.CheckInstanceAndClose));
			NKCUtil.SetHotkey(this.m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_BUTTON, HotkeyEventType.Confirm, null, false);
			if (this.m_Bg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCUIPopupFierceBattleRankReward.CheckInstanceAndClose();
				});
				this.m_Bg.triggers.Add(entry);
			}
		}

		// Token: 0x06008ABD RID: 35517 RVA: 0x002F28B8 File Offset: 0x002F0AB8
		public void Open(NKMRewardData reward)
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				NKCUtil.SetLabelText(this.m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_RANK1, nkcfierceBattleSupportDataMgr.GetRankingTotalDesc());
				NKCUtil.SetLabelText(this.m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_RANK2, nkcfierceBattleSupportDataMgr.GetTotalPoint().ToString());
			}
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (reward != null)
			{
				foreach (NKMEquipItemData nkmequipItemData in reward.EquipItemDataList)
				{
					if (nkmequipItemData != null)
					{
						NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeEquipData(nkmequipItemData.m_ItemEquipID, nkmequipItemData.m_EnchantLevel, 0);
						list.Add(item);
					}
				}
				foreach (NKMItemMiscData nkmitemMiscData in reward.MiscItemDataList)
				{
					if (nkmitemMiscData != null)
					{
						NKCUISlot.SlotData item2 = NKCUISlot.SlotData.MakeMiscItemData(nkmitemMiscData.ItemID, nkmitemMiscData.TotalCount, 0);
						list.Add(item2);
					}
				}
				foreach (NKMMoldItemData nkmmoldItemData in reward.MoldItemDataList)
				{
					if (nkmmoldItemData != null)
					{
						NKCUISlot.SlotData item3 = NKCUISlot.SlotData.MakeMoldItemData(nkmmoldItemData.m_MoldID, nkmmoldItemData.m_Count);
						list.Add(item3);
					}
				}
				foreach (NKMInteriorData nkminteriorData in reward.Interiors)
				{
					if (nkminteriorData != null)
					{
						NKCUISlot.SlotData item4 = NKCUISlot.SlotData.MakeMiscItemData(nkminteriorData.itemId, nkminteriorData.count, 0);
						list.Add(item4);
					}
				}
				for (int i = 0; i < this.m_RewardSlots.Count; i++)
				{
					if (!(this.m_RewardSlots[i] == null))
					{
						if (list.Count <= i || list[i] == null)
						{
							NKCUtil.SetGameobjectActive(this.m_RewardSlots[i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_RewardSlots[i], true);
							this.m_RewardSlots[i].SetData(list[i], true, null);
						}
					}
				}
				base.UIOpened(true);
			}
		}

		// Token: 0x04007773 RID: 30579
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x04007774 RID: 30580
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_FINAL_REWARD";

		// Token: 0x04007775 RID: 30581
		private static NKCUIPopupFierceBattleRankReward m_Instance;

		// Token: 0x04007776 RID: 30582
		public Text m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_RANK1;

		// Token: 0x04007777 RID: 30583
		public Text m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_RANK2;

		// Token: 0x04007778 RID: 30584
		public List<NKCUISlot> m_RewardSlots;

		// Token: 0x04007779 RID: 30585
		public NKCUIComStateButton m_POPUP_FIERCE_BATTLE_FINAL_RANK_REWARD_BUTTON;

		// Token: 0x0400777A RID: 30586
		public EventTrigger m_Bg;
	}
}
