using System;
using System.Collections.Generic;
using ClientPacket.Item;
using DG.Tweening;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000994 RID: 2452
	public class NKCUIForgeCraftSlot : MonoBehaviour
	{
		// Token: 0x060065B8 RID: 26040 RVA: 0x002058CC File Offset: 0x00203ACC
		public void SetIndex(int index)
		{
			this.m_Index = index;
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x002058D5 File Offset: 0x00203AD5
		public int GetIndex()
		{
			return this.m_Index;
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x002058DD File Offset: 0x00203ADD
		private void OnClickBG()
		{
			if (this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_LOCK.activeSelf)
			{
				this.OnClickUnLock();
				return;
			}
			if (!this.m_BUTTON_INSTANT_CRAFT.gameObject.activeSelf)
			{
				this.OnClickGetOrSelect();
				return;
			}
			this.OnClickInstantCraft();
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x00205914 File Offset: 0x00203B14
		public void Init(int index, NKCUIForgeCraftSlot.OnClickSelect dOnClickSelect = null, NKCUIForgeCraftSlot.OnClickSelect dOnClickGet = null, NKCUIForgeCraftSlot.OnClickSelect dOnClickInstanceGet = null)
		{
			this.SetIndex(index);
			this.m_dOnClickSelect = dOnClickSelect;
			this.m_dOnClickGet = dOnClickGet;
			this.m_dOnClickInstanceGet = dOnClickInstanceGet;
			if (this.m_AB_ICON_SLOT != null)
			{
				this.m_AB_ICON_SLOT.Init();
			}
			this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG.PointerClick.AddListener(new UnityAction(this.OnClickBG));
			this.m_BUTTON.PointerClick.RemoveAllListeners();
			this.m_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickGetOrSelect));
			this.m_BUTTON_SELECT.PointerClick.RemoveAllListeners();
			this.m_BUTTON_SELECT.PointerClick.AddListener(new UnityAction(this.OnClickGetOrSelect));
			if (this.m_NKM_UI_FACTORY_CRAFT_SLOT_BUTTONS_INSTANT != null)
			{
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_BUTTONS_INSTANT.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_BUTTONS_INSTANT.PointerClick.AddListener(new UnityAction(this.OnClickInstantCraft));
			}
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x00205A18 File Offset: 0x00203C18
		public void OnClickUnLock()
		{
			if (this.m_Index < 1 || this.m_Index > NKMCraftData.MAX_CRAFT_SLOT_DATA)
			{
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_CRAFT_SLOT_ADD, 101, 300, new NKCPopupResourceConfirmBox.OnButton(this.UnLockConfirm), null, false);
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x00205A68 File Offset: 0x00203C68
		public void UnLockConfirm()
		{
			NKMPacket_CRAFT_UNLOCK_SLOT_REQ packet = new NKMPacket_CRAFT_UNLOCK_SLOT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x00205A8E File Offset: 0x00203C8E
		private void SetLockUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_LOCK, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_NO_LOCK, false);
			this.m_NKM_UI_FACTORY_CRAFT_SLOT_TEXT_NUM_FOR_LOCK.text = this.m_Index.ToString();
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x00205AC0 File Offset: 0x00203CC0
		private void PlayAni()
		{
			if (this.m_bPlaying != null)
			{
				bool? bPlaying = this.m_bPlaying;
				bool flag = true;
				if (bPlaying.GetValueOrDefault() == flag & bPlaying != null)
				{
					return;
				}
			}
			this.m_bPlaying = new bool?(true);
			for (int i = 0; i < this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PARTS.Count; i++)
			{
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PARTS[i].DOPlay();
			}
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x00205B2C File Offset: 0x00203D2C
		private void StopAni()
		{
			if (this.m_bPlaying != null)
			{
				bool? bPlaying = this.m_bPlaying;
				bool flag = false;
				if (bPlaying.GetValueOrDefault() == flag & bPlaying != null)
				{
					return;
				}
			}
			this.m_bPlaying = new bool?(false);
			for (int i = 0; i < this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PARTS.Count; i++)
			{
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PARTS[i].DOPause();
			}
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x00205B98 File Offset: 0x00203D98
		public void ResetUI(bool bUpdateIconSlot = true)
		{
			if (this.m_Index < 1 || this.m_Index > NKMCraftData.MAX_CRAFT_SLOT_DATA)
			{
				this.SetLockUI();
				return;
			}
			NKMCraftData craftData = NKCScenManager.GetScenManager().GetMyUserData().m_CraftData;
			if (craftData == null)
			{
				this.SetLockUI();
				return;
			}
			NKMCraftSlotData slotData = craftData.GetSlotData((byte)this.m_Index);
			this.m_NKMEquipCreationSlotData = slotData;
			if (slotData == null)
			{
				this.SetLockUI();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_LOCK, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_NO_LOCK, true);
			if (slotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_EMPTY)
			{
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT.gameObject, false);
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_NAME.text = NKCUtilString.GET_STRING_FORGE_CRAFT_WAIT_NAME;
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_TEXT.text = NKCUtilString.GET_STRING_FORGE_CRAFT_WAIT_TEXT;
				NKCUtil.SetGameobjectActive(this.m_BUTTON_GET, false);
				NKCUtil.SetGameobjectActive(this.m_BUTTON_INSTANT_CRAFT, false);
				NKCUtil.SetGameobjectActive(this.m_BUTTON_SELECT, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_ANIM, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_COMPLETE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_TIME, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_COMPLETE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_00, false);
				this.StopAni();
				this.m_NKM_UI_FACTORY_CRAFT_SLOT_NUMBER.text = this.m_Index.ToString();
				return;
			}
			if (slotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(slotData.MoldID);
				if (itemMoldTempletByID != null)
				{
					NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT.gameObject, true);
					if (bUpdateIconSlot)
					{
						this.m_AB_ICON_SLOT.SetData(NKCUISlot.SlotData.MakeMoldItemData(itemMoldTempletByID.m_MoldID, (long)slotData.Count), false, true, true, null);
					}
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_NAME.text = itemMoldTempletByID.GetItemName();
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_TEXT.text = NKCUtilString.GET_STRING_FORGE_CRAFT_ING_TEXT;
					NKCUtil.SetGameobjectActive(this.m_BUTTON_GET, false);
					NKCUtil.SetGameobjectActive(this.m_BUTTON_INSTANT_CRAFT, true);
					NKCUtil.SetGameobjectActive(this.m_BUTTON_SELECT, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_ANIM, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_COMPLETE, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_TIME, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_COMPLETE, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_00, true);
					this.PlayAni();
					TimeSpan timeSpan = new TimeSpan(slotData.CompleteDate - NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks);
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_TIME_Text.text = NKCUtilString.GetTimeSpanString(timeSpan);
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_NUMBER.text = this.m_Index.ToString();
					return;
				}
				this.SetLockUI();
				return;
			}
			else
			{
				if (slotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) != NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
				{
					this.SetLockUI();
					return;
				}
				NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(slotData.MoldID);
				if (itemMoldTempletByID2 != null)
				{
					NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT.gameObject, true);
					if (bUpdateIconSlot)
					{
						this.m_AB_ICON_SLOT.SetData(NKCUISlot.SlotData.MakeMoldItemData(itemMoldTempletByID2.m_MoldID, (long)slotData.Count), true, null);
					}
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_NAME.text = itemMoldTempletByID2.GetItemName();
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_TEXT.text = NKCUtilString.GET_STRING_FORGE_CRAFT_COMPLETED_TEXT;
					NKCUtil.SetGameobjectActive(this.m_BUTTON_GET, true);
					NKCUtil.SetGameobjectActive(this.m_BUTTON_INSTANT_CRAFT, false);
					NKCUtil.SetGameobjectActive(this.m_BUTTON_SELECT, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_ANIM, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_COMPLETE, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_TIME, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_BG_COMPLETE, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_00, true);
					this.StopAni();
					this.m_NKM_UI_FACTORY_CRAFT_SLOT_NUMBER.text = this.m_Index.ToString();
					return;
				}
				this.SetLockUI();
				return;
			}
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x00205F24 File Offset: 0x00204124
		public void OnClickGetOrSelect()
		{
			if (this.m_NKMEquipCreationSlotData == null)
			{
				return;
			}
			if (this.m_NKMEquipCreationSlotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
			{
				if (this.m_dOnClickGet != null)
				{
					this.m_dOnClickGet(this.m_Index);
					return;
				}
			}
			else if (this.m_NKMEquipCreationSlotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_EMPTY && this.m_dOnClickSelect != null)
			{
				this.m_dOnClickSelect(this.m_Index);
			}
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x00205FA4 File Offset: 0x002041A4
		public void OnClickInstantCraft()
		{
			if (this.m_NKMEquipCreationSlotData == null)
			{
				return;
			}
			if (this.m_NKMEquipCreationSlotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW && this.m_dOnClickInstanceGet != null)
			{
				this.m_dOnClickInstanceGet(this.m_Index);
			}
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x00205FE4 File Offset: 0x002041E4
		public RectTransform GetButtonRect()
		{
			if (this.m_NKMEquipCreationSlotData == null)
			{
				return null;
			}
			switch (this.m_NKMEquipCreationSlotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)))
			{
			case NKM_CRAFT_SLOT_STATE.NECSS_EMPTY:
				return this.m_BUTTON_SELECT.GetComponent<RectTransform>();
			case NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW:
				return this.m_BUTTON_INSTANT_CRAFT.GetComponent<RectTransform>();
			case NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED:
				return this.m_BUTTON_GET.GetComponent<RectTransform>();
			default:
				return null;
			}
		}

		// Token: 0x0400512F RID: 20783
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_BG_LOCK;

		// Token: 0x04005130 RID: 20784
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_BG_NO_LOCK;

		// Token: 0x04005131 RID: 20785
		public Text m_NKM_UI_FACTORY_CRAFT_SLOT_NUMBER;

		// Token: 0x04005132 RID: 20786
		public Text m_NKM_UI_FACTORY_CRAFT_SLOT_TEXT_NUM_FOR_LOCK;

		// Token: 0x04005133 RID: 20787
		public NKCUISlot m_AB_ICON_SLOT;

		// Token: 0x04005134 RID: 20788
		public Text m_NKM_UI_FACTORY_CRAFT_SLOT_NAME;

		// Token: 0x04005135 RID: 20789
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_TIME;

		// Token: 0x04005136 RID: 20790
		public Text m_NKM_UI_FACTORY_CRAFT_SLOT_TEXT;

		// Token: 0x04005137 RID: 20791
		public Text m_NKM_UI_FACTORY_CRAFT_SLOT_TIME_Text;

		// Token: 0x04005138 RID: 20792
		public GameObject m_BUTTON_GET;

		// Token: 0x04005139 RID: 20793
		public GameObject m_BUTTON_INSTANT_CRAFT;

		// Token: 0x0400513A RID: 20794
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_ANIM;

		// Token: 0x0400513B RID: 20795
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_COMPLETE;

		// Token: 0x0400513C RID: 20796
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PART_00;

		// Token: 0x0400513D RID: 20797
		public GameObject m_NKM_UI_FACTORY_CRAFT_SLOT_BG_COMPLETE;

		// Token: 0x0400513E RID: 20798
		public NKCUIComButton m_NKM_UI_FACTORY_CRAFT_SLOT_BG;

		// Token: 0x0400513F RID: 20799
		public NKCUIComButton m_BUTTON;

		// Token: 0x04005140 RID: 20800
		public NKCUIComButton m_BUTTON_SELECT;

		// Token: 0x04005141 RID: 20801
		public NKCUIComButton m_NKM_UI_FACTORY_CRAFT_SLOT_BUTTONS_INSTANT;

		// Token: 0x04005142 RID: 20802
		public List<DOTweenAnimation> m_NKM_UI_FACTORY_CRAFT_SLOT_PROGRESS_PARTS;

		// Token: 0x04005143 RID: 20803
		private bool? m_bPlaying;

		// Token: 0x04005144 RID: 20804
		private int m_Index = -1;

		// Token: 0x04005145 RID: 20805
		private NKCUIForgeCraftSlot.OnClickSelect m_dOnClickSelect;

		// Token: 0x04005146 RID: 20806
		private NKCUIForgeCraftSlot.OnClickSelect m_dOnClickGet;

		// Token: 0x04005147 RID: 20807
		private NKCUIForgeCraftSlot.OnClickSelect m_dOnClickInstanceGet;

		// Token: 0x04005148 RID: 20808
		private NKMCraftSlotData m_NKMEquipCreationSlotData;

		// Token: 0x02001662 RID: 5730
		// (Invoke) Token: 0x0600B01A RID: 45082
		public delegate void OnClickSelect(int index);
	}
}
