using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200096D RID: 2413
	public class NKCPopupShipCommandModuleSlot : MonoBehaviour
	{
		// Token: 0x06006145 RID: 24901 RVA: 0x001E799C File Offset: 0x001E5B9C
		public void Init(NKCPopupShipCommandModuleSlot.OnSetting dOnOpen, NKCPopupShipCommandModuleSlot.OnSetting dOnSetting)
		{
			for (int i = 0; i < this.m_lstSocket.Count; i++)
			{
				this.m_lstSocket[i].Init();
			}
			this.m_btnInfo.PointerClick.RemoveAllListeners();
			this.m_btnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfo));
			this.m_btnOpen.PointerClick.RemoveAllListeners();
			this.m_btnOpen.PointerClick.AddListener(new UnityAction(this.OnClickOpen));
			this.m_btnOpen.m_bGetCallbackWhileLocked = true;
			this.m_btnSetting.PointerClick.RemoveAllListeners();
			this.m_btnSetting.PointerClick.AddListener(new UnityAction(this.OnClickSetting));
			this.m_dOnOpen = dOnOpen;
			this.m_dOnClickSetting = dOnSetting;
			NKCUtil.SetGameobjectActive(this.m_objOpenFx, false);
			NKCUtil.SetGameobjectActive(this.m_objConfirmFx, false);
			NKCUtil.SetGameobjectActive(this.m_objRerollFx, false);
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x001E7A94 File Offset: 0x001E5C94
		public void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(this.m_objOpenFx, false);
			NKCUtil.SetGameobjectActive(this.m_objConfirmFx, false);
			NKCUtil.SetGameobjectActive(this.m_objRerollFx, false);
			NKCUtil.SetGameobjectActive(this.m_objDisable, false);
			this.m_moduleIndex = -1;
			for (int i = 0; i < this.m_lstSocket.Count; i++)
			{
				this.m_lstSocket[i].CloseInternal();
			}
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x001E7B00 File Offset: 0x001E5D00
		public void SetData(int moduleIndex, NKMShipCmdModule curModuleData, NKMShipCmdModule nextModuleData = null)
		{
			NKCUtil.SetGameobjectActive(this.m_objDisable, false);
			NKCUtil.SetGameobjectActive(this.m_objLock, curModuleData == null);
			NKCUtil.SetGameobjectActive(this.m_btnOpen, curModuleData == null || this.IsEmptySlot(curModuleData));
			NKCUtil.SetGameobjectActive(this.m_btnSetting, curModuleData != null && !this.IsEmptySlot(curModuleData));
			NKCUtil.SetImageSprite(this.m_imgSlotNum, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_SHIP_INFO_SPRITE", string.Format("NKM_UI_SHIP_MODULE_FONT_SLOT_0{0}", moduleIndex + 1), false), false);
			if (curModuleData == null)
			{
				this.m_btnOpen.Lock(false);
			}
			else
			{
				this.m_btnOpen.UnLock(false);
			}
			this.m_moduleIndex = moduleIndex;
			for (int i = 0; i < this.m_lstSocket.Count; i++)
			{
				if (curModuleData != null)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSocket[i], true);
					this.m_lstSocket[i].SetData(moduleIndex, i, curModuleData.slots[i], (nextModuleData == null) ? null : nextModuleData.slots[i]);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSocket[i], false);
					this.m_lstSocket[i].SetData(moduleIndex, i, null, null);
				}
			}
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x001E7C28 File Offset: 0x001E5E28
		public void SetState(NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE state)
		{
			switch (state)
			{
			case NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.LOCKED:
			case NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.IDLE_01:
				this.m_Ani.SetTrigger("01_IDLE");
				return;
			case NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.SET_01_TO_02:
				this.m_Ani.SetTrigger("01_TO_02");
				return;
			case NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.IDLE_02:
				this.m_Ani.SetTrigger("02_IDLE");
				return;
			case NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.SET_02_TO_01:
				this.m_Ani.SetTrigger("02_TO_01");
				return;
			default:
				return;
			}
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x001E7C93 File Offset: 0x001E5E93
		public void SetDisable(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objDisable, bValue);
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x001E7CA4 File Offset: 0x001E5EA4
		private bool IsEmptySlot(NKMShipCmdModule slotData)
		{
			if (slotData == null || slotData.slots == null)
			{
				return true;
			}
			for (int i = 0; i < slotData.slots.Length; i++)
			{
				if (slotData.slots[i] == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x001E7CE0 File Offset: 0x001E5EE0
		private void OnClickInfo()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(NKCPopupShipCommandModule.Instance.GetShipData().m_UnitID);
			NKCPopupShipModuleOption.Instance.Open(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, this.m_moduleIndex);
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x001E7D1E File Offset: 0x001E5F1E
		private void OnClickOpen()
		{
			if (this.m_btnOpen.m_bLock)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE_SLOT_NOT_OPEN, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.m_dOnOpen(this.m_moduleIndex);
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x001E7D52 File Offset: 0x001E5F52
		private void OnClickSetting()
		{
			this.m_dOnClickSetting(this.m_moduleIndex);
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x001E7D65 File Offset: 0x001E5F65
		public void ShowOpenFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objOpenFx, false);
			NKCUtil.SetGameobjectActive(this.m_objOpenFx, true);
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x001E7D80 File Offset: 0x001E5F80
		public void ShowRerollFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objRerollFx, false);
			NKCUtil.SetGameobjectActive(this.m_objRerollFx, true);
			for (int i = 0; i < this.m_lstSocket.Count; i++)
			{
				this.m_lstSocket[i].ShowRerollFx();
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x001E7DCC File Offset: 0x001E5FCC
		public void ShowConfirmFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objConfirmFx, false);
			NKCUtil.SetGameobjectActive(this.m_objConfirmFx, true);
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x001E7DE8 File Offset: 0x001E5FE8
		public void DisableAllFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objOpenFx, false);
			NKCUtil.SetGameobjectActive(this.m_objRerollFx, false);
			NKCUtil.SetGameobjectActive(this.m_objConfirmFx, false);
			for (int i = 0; i < this.m_lstSocket.Count; i++)
			{
				this.m_lstSocket[i].DisableAllFx();
			}
		}

		// Token: 0x04004D6D RID: 19821
		public Animator m_Ani;

		// Token: 0x04004D6E RID: 19822
		public List<NKCPopupShipCommandModuleSlotSocket> m_lstSocket = new List<NKCPopupShipCommandModuleSlotSocket>();

		// Token: 0x04004D6F RID: 19823
		public Image m_imgSlotNum;

		// Token: 0x04004D70 RID: 19824
		public NKCUIComStateButton m_btnInfo;

		// Token: 0x04004D71 RID: 19825
		public NKCUIComStateButton m_btnOpen;

		// Token: 0x04004D72 RID: 19826
		public NKCUIComStateButton m_btnSetting;

		// Token: 0x04004D73 RID: 19827
		public GameObject m_objLock;

		// Token: 0x04004D74 RID: 19828
		public GameObject m_objDisable;

		// Token: 0x04004D75 RID: 19829
		public GameObject m_objOpenFx;

		// Token: 0x04004D76 RID: 19830
		public GameObject m_objRerollFx;

		// Token: 0x04004D77 RID: 19831
		public GameObject m_objConfirmFx;

		// Token: 0x04004D78 RID: 19832
		private NKCPopupShipCommandModuleSlot.OnSetting m_dOnOpen;

		// Token: 0x04004D79 RID: 19833
		private NKCPopupShipCommandModuleSlot.OnSetting m_dOnClickSetting;

		// Token: 0x04004D7A RID: 19834
		private int m_moduleIndex = -1;

		// Token: 0x02001607 RID: 5639
		public enum SHIP_MODULE_STATE
		{
			// Token: 0x0400A2EF RID: 41711
			LOCKED,
			// Token: 0x0400A2F0 RID: 41712
			IDLE_01,
			// Token: 0x0400A2F1 RID: 41713
			SET_01_TO_02,
			// Token: 0x0400A2F2 RID: 41714
			IDLE_02,
			// Token: 0x0400A2F3 RID: 41715
			SET_02_TO_01
		}

		// Token: 0x02001608 RID: 5640
		// (Invoke) Token: 0x0600AEFC RID: 44796
		public delegate void OnSetting(int slotIndex);
	}
}
