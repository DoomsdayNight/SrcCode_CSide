using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A98 RID: 2712
	public class NKCUIPopupRearmamentExtractConfirm : NKCUIBase
	{
		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06007830 RID: 30768 RVA: 0x0027E76C File Offset: 0x0027C96C
		public static NKCUIPopupRearmamentExtractConfirm Instance
		{
			get
			{
				if (NKCUIPopupRearmamentExtractConfirm.m_Instance == null)
				{
					NKCUIPopupRearmamentExtractConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupRearmamentExtractConfirm>("ab_ui_rearm", "AB_UI_POPUP_REARM_RECORD_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupRearmamentExtractConfirm.CleanupInstance)).GetInstance<NKCUIPopupRearmamentExtractConfirm>();
					NKCUIPopupRearmamentExtractConfirm.m_Instance.InitUI();
				}
				return NKCUIPopupRearmamentExtractConfirm.m_Instance;
			}
		}

		// Token: 0x06007831 RID: 30769 RVA: 0x0027E7BB File Offset: 0x0027C9BB
		private static void CleanupInstance()
		{
			NKCUIPopupRearmamentExtractConfirm.m_Instance = null;
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06007832 RID: 30770 RVA: 0x0027E7C3 File Offset: 0x0027C9C3
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupRearmamentExtractConfirm.m_Instance != null && NKCUIPopupRearmamentExtractConfirm.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007833 RID: 30771 RVA: 0x0027E7DE File Offset: 0x0027C9DE
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupRearmamentExtractConfirm.m_Instance != null && NKCUIPopupRearmamentExtractConfirm.m_Instance.IsOpen)
			{
				NKCUIPopupRearmamentExtractConfirm.m_Instance.Close();
			}
		}

		// Token: 0x06007834 RID: 30772 RVA: 0x0027E803 File Offset: 0x0027CA03
		private void OnDestroy()
		{
			NKCUIPopupRearmamentExtractConfirm.m_Instance = null;
		}

		// Token: 0x06007835 RID: 30773 RVA: 0x0027E80B File Offset: 0x0027CA0B
		public override void CloseInternal()
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06007836 RID: 30774 RVA: 0x0027E81F File Offset: 0x0027CA1F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x0027E822 File Offset: 0x0027CA22
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_REARM_CONFIRM_POPUP_TITLE;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06007838 RID: 30776 RVA: 0x0027E829 File Offset: 0x0027CA29
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.BackButtonOnly;
			}
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x0027E82C File Offset: 0x0027CA2C
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm);
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x0027E868 File Offset: 0x0027CA68
		private void Clear()
		{
			for (int i = 0; i < this.m_lstExtractItem.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstExtractItem[i]);
			}
			this.m_lstExtractItem.Clear();
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x0027E8A8 File Offset: 0x0027CAA8
		public void Open(List<NKCUISlot.SlotData> lstExtractItemData, List<long> lstSelectedUnitsUID, bool bActiveSynergyBouns = false)
		{
			this.m_lstSelectedUnitsUID = lstSelectedUnitsUID;
			NKCUtil.SetLabelText(this.m_lbResultText, string.Format(NKCUtilString.GET_STRING_REARM_EXTRACT_CONFIRM_POPUP_DESC, lstSelectedUnitsUID.Count));
			if (bActiveSynergyBouns)
			{
				int synergyIncreasePercentage = NKCRearmamentUtil.GetSynergyIncreasePercentage(this.m_lstSelectedUnitsUID);
				NKCUtil.SetLabelText(this.m_lbSynergyBonus, string.Format(NKCUtilString.GET_STRING_REARM_EXTRACT_CONFIRM_POPUP_SYNERGY_BONUS, synergyIncreasePercentage));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbSynergyBonus, NKCUtilString.GET_STRING_REARM_EXTRACT_NOT_ACTIVE_SYNERGY_BOUNS);
			}
			foreach (GameObject targetObj in this.m_lstSynergyON)
			{
				NKCUtil.SetGameobjectActive(targetObj, bActiveSynergyBouns);
			}
			foreach (GameObject targetObj2 in this.m_lstSynergyOFF)
			{
				NKCUtil.SetGameobjectActive(targetObj2, !bActiveSynergyBouns);
			}
			foreach (NKCUISlot.SlotData slotData in lstExtractItemData)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
				if (itemMiscTempletByID != null)
				{
					if (itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_PIECE)
					{
						Debug.LogError("NKCUIPopupRearmamentExtractConfirm::Open() - Can not support imt_piece type");
					}
					else if (this.m_rtTacticsInfo != null)
					{
						NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_rtTacticsInfo);
						if (null != newInstance)
						{
							NKCUISlot.SlotData slotData2 = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_MISC, slotData.ID, (int)slotData.Count, 0);
							if (slotData2 != null)
							{
								newInstance.SetData(slotData2, true, null);
								NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
							}
							this.m_lstExtractItem.Add(newInstance);
						}
					}
				}
			}
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(bActiveSynergyBouns ? this.m_iMiscRewardItemCode : this.m_iDisableMiscRewardItemCode, 1L, 0);
			this.m_SynergySlot.SetData(data, true, null);
			base.UIOpened(true);
		}

		// Token: 0x0600783C RID: 30780 RVA: 0x0027EA9C File Offset: 0x0027CC9C
		private void OnClickOK()
		{
			NKCPacketSender.Send_NKMPacket_EXTRACT_UNIT_REQ(this.m_lstSelectedUnitsUID);
		}

		// Token: 0x040064B4 RID: 25780
		private const string ASSET_BUNDLE_NAME = "ab_ui_rearm";

		// Token: 0x040064B5 RID: 25781
		private const string UI_ASSET_NAME = "AB_UI_POPUP_REARM_RECORD_CONFIRM";

		// Token: 0x040064B6 RID: 25782
		private static NKCUIPopupRearmamentExtractConfirm m_Instance;

		// Token: 0x040064B7 RID: 25783
		public Text m_lbResultText;

		// Token: 0x040064B8 RID: 25784
		public RectTransform m_rtTacticsInfo;

		// Token: 0x040064B9 RID: 25785
		private List<NKCUISlot> m_lstExtractItem = new List<NKCUISlot>();

		// Token: 0x040064BA RID: 25786
		public NKCUISlot m_SynergySlot;

		// Token: 0x040064BB RID: 25787
		public Text m_lbSynergyBonus;

		// Token: 0x040064BC RID: 25788
		public NKCUIComButton m_csbtnOK;

		// Token: 0x040064BD RID: 25789
		public NKCUIComButton m_csbtnCancel;

		// Token: 0x040064BE RID: 25790
		public List<GameObject> m_lstSynergyON;

		// Token: 0x040064BF RID: 25791
		public List<GameObject> m_lstSynergyOFF;

		// Token: 0x040064C0 RID: 25792
		public int m_iMiscRewardItemCode = 913;

		// Token: 0x040064C1 RID: 25793
		public int m_iDisableMiscRewardItemCode = 913;

		// Token: 0x040064C2 RID: 25794
		private List<long> m_lstSelectedUnitsUID;
	}
}
