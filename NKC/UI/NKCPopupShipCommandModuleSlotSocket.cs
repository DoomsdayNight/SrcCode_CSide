using System;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200096E RID: 2414
	public class NKCPopupShipCommandModuleSlotSocket : MonoBehaviour
	{
		// Token: 0x06006153 RID: 24915 RVA: 0x001E7E5A File Offset: 0x001E605A
		public void Init()
		{
			this.m_btnLock.PointerClick.RemoveAllListeners();
			this.m_btnLock.PointerClick.AddListener(new UnityAction(this.OnClickLock));
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x001E7E88 File Offset: 0x001E6088
		public void CloseInternal()
		{
			this.m_moduleIndex = -1;
			this.m_socketIndex = -1;
			this.m_bLockChanged = false;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x001E7EA0 File Offset: 0x001E60A0
		public void SetData(int moduleIndex, int socketIndex, NKMShipCmdSlot curSlotData, NKMShipCmdSlot nextSlotData = null)
		{
			this.m_moduleIndex = moduleIndex;
			this.m_socketIndex = socketIndex;
			this.m_curSlotData = curSlotData;
			this.m_nextSlotData = nextSlotData;
			NKCUtil.SetLabelText(this.m_lbSocketNum, (socketIndex + 1).ToString());
			NKCUtil.SetGameobjectActive(this.m_objOpen, curSlotData == null);
			if (curSlotData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objFocusLine, !curSlotData.isLock);
				string format = "[{0}] {1}";
				NKMUnitTempletBase unitTempletBase = NKCPopupShipCommandModule.Instance.GetShipData().GetUnitTemplet().m_UnitTempletBase;
				if (unitTempletBase != null)
				{
					NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = NKMShipManager.GetNKMShipCommandModuleTemplet(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, moduleIndex + 1);
					if (nkmshipCommandModuleTemplet != null)
					{
						if (socketIndex == 0)
						{
							this.m_StatGroupId = nkmshipCommandModuleTemplet.Slot1Id;
						}
						else
						{
							this.m_StatGroupId = nkmshipCommandModuleTemplet.Slot2Id;
						}
					}
				}
				bool flag = NKMShipManager.IsMaxStat(this.m_StatGroupId, this.m_curSlotData);
				NKCUtil.SetGameobjectActive(this.m_objCurrent, !flag);
				NKCUtil.SetGameobjectActive(this.m_objCurrentMax, flag);
				NKCUtil.SetLabelText(this.m_lbOptionCurrent, NKCUtilString.GetSlotOptionString(curSlotData, format));
				NKCUtil.SetLabelText(this.m_lbOptionCurrentMax, NKCUtilString.GetSlotOptionString(curSlotData, format));
				bool flag2 = NKMShipManager.IsMaxStat(this.m_StatGroupId, nextSlotData);
				if (nextSlotData != null)
				{
					NKCUtil.SetGameobjectActive(this.m_objAfter, !flag2);
					NKCUtil.SetGameobjectActive(this.m_objAfterMax, flag2);
					NKCUtil.SetLabelText(this.m_lbOptionAfter, NKCUtilString.GetSlotOptionString(nextSlotData, format));
					NKCUtil.SetLabelText(this.m_lbOptionAfterMax, NKCUtilString.GetSlotOptionString(nextSlotData, format));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objAfter, true);
					NKCUtil.SetGameobjectActive(this.m_objAfterMax, false);
					NKCUtil.SetLabelText(this.m_lbOptionAfter, "-");
					NKCUtil.SetLabelText(this.m_lbOptionAfterMax, "-");
				}
				this.SetLockState(curSlotData.isLock, this.m_bLockChanged);
				this.m_bLockChanged = false;
				this.m_btnLock.Select(curSlotData.isLock, true, true);
				NKCUtil.SetGameobjectActive(this.m_objMaxFx, flag || flag2);
				return;
			}
			this.m_btnLock.Select(false, true, true);
			NKCUtil.SetGameobjectActive(this.m_objFocusLine, false);
			NKCUtil.SetGameobjectActive(this.m_objCurrent, true);
			NKCUtil.SetGameobjectActive(this.m_objCurrentMax, false);
			NKCUtil.SetGameobjectActive(this.m_objAfter, false);
			NKCUtil.SetGameobjectActive(this.m_objAfterMax, false);
			NKCUtil.SetLabelText(this.m_lbOptionCurrent, string.Empty);
			NKCUtil.SetLabelText(this.m_lbOptionCurrentMax, string.Empty);
			NKCUtil.SetLabelText(this.m_lbOptionAfter, string.Empty);
			NKCUtil.SetLabelText(this.m_lbOptionAfterMax, string.Empty);
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x001E810C File Offset: 0x001E630C
		private void OnClickLock()
		{
			NKMUnitData shipData = NKCPopupShipCommandModule.Instance.GetShipData();
			if (shipData == null)
			{
				this.SetLockState(this.m_btnLock.m_bSelect, false);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipData.m_UnitID);
			if (unitTempletBase == null)
			{
				this.SetLockState(this.m_btnLock.m_bSelect, false);
				return;
			}
			NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = NKMShipManager.GetNKMShipCommandModuleTemplet(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, this.m_moduleIndex + 1);
			if (nkmshipCommandModuleTemplet == null)
			{
				Log.Error(string.Format("NKMShipCommandModuleTemplet is null - NKM_UNIT_STYLE_TYPE : {0}, NKM_UNIT_GRADE : {1}", unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCPopupShipCommandModuleSlotSocket.cs", 155);
				this.SetLockState(this.m_btnLock.m_bSelect, false);
				return;
			}
			if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid > 0L)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NEC_FAIL_SHIP_COMMAND_MODULE_LOCK_NO_CONFIRM, null, "");
				return;
			}
			if (!this.m_btnLock.m_bSelect)
			{
				NKCPopupResourceConfirmBox.Instance.Open(string.Format("{0} {1}", NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE, this.m_moduleIndex + 1), string.Format(NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_SLOT_LOCK, this.m_socketIndex + 1), nkmshipCommandModuleTemplet.ModuleLockItems[this.m_socketIndex].ItemId, nkmshipCommandModuleTemplet.ModuleLockItems[this.m_socketIndex].Count32, delegate()
				{
					this.OnConfirmLock(!this.m_btnLock.m_bSelect);
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(string.Format("{0} {1}", NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE, this.m_moduleIndex + 1), string.Format(NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_SLOT_UNLOCK, this.m_socketIndex + 1) + "\n" + NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE_SLOT_LOCK_COST_NOT_RECOVERED, delegate()
			{
				this.OnConfirmLock(!this.m_btnLock.m_bSelect);
			}, null, false);
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x001E82BE File Offset: 0x001E64BE
		private void OnConfirmLock(bool bValue)
		{
			this.m_bLockChanged = true;
			this.m_btnLock.m_bSelect = bValue;
			NKCUtil.SetGameobjectActive(this.m_objFocusLine, bValue);
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_LOCK_REQ(NKCPopupShipCommandModule.Instance.GetShipUID(), this.m_moduleIndex, this.m_socketIndex, bValue);
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x001E82FC File Offset: 0x001E64FC
		public void SetLockState(bool bLocked, bool bChanged)
		{
			if (bLocked)
			{
				if (bChanged)
				{
					this.SetLockState(NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.UNLOCK_TO_LOCK);
				}
				else
				{
					this.SetLockState(NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.LOCK);
				}
			}
			else if (bChanged)
			{
				this.SetLockState(NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.LOCK_TO_UNLOCK);
			}
			else
			{
				this.SetLockState(NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.UNLOCK);
			}
			NKCUtil.SetGameobjectActive(this.m_objLocked, bLocked);
			NKCUtil.SetGameobjectActive(this.m_objAfter, !bLocked && !NKMShipManager.IsMaxStat(this.m_StatGroupId, this.m_nextSlotData));
			NKCUtil.SetGameobjectActive(this.m_objAfterMax, !bLocked && NKMShipManager.IsMaxStat(this.m_StatGroupId, this.m_nextSlotData));
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x001E8388 File Offset: 0x001E6588
		public void SetLockState(NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE state)
		{
			switch (state)
			{
			case NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.UNLOCK:
				this.m_Ani.SetTrigger("UNLOCK_IDLE");
				return;
			case NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.UNLOCK_TO_LOCK:
				this.m_Ani.SetTrigger("UNLOCK_TO_LOCK");
				return;
			case NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.LOCK:
				this.m_Ani.SetTrigger("LOCK_IDLE");
				return;
			case NKCPopupShipCommandModuleSlotSocket.SHIP_MODULE_SOCKET_STATE.LOCK_TO_UNLOCK:
				this.m_Ani.SetTrigger("LOCK_TO_UNLOCK");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x001E83EF File Offset: 0x001E65EF
		public void ShowRerollFx()
		{
			if (!this.m_curSlotData.isLock)
			{
				NKCUtil.SetGameobjectActive(this.m_objRerollFx, false);
				NKCUtil.SetGameobjectActive(this.m_objRerollFx, true);
			}
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x001E8416 File Offset: 0x001E6616
		public void DisableAllFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objRerollFx, false);
			NKCUtil.SetGameobjectActive(this.m_objMaxFx, false);
		}

		// Token: 0x04004D7B RID: 19835
		public Text m_lbSocketNum;

		// Token: 0x04004D7C RID: 19836
		public GameObject m_objCurrent;

		// Token: 0x04004D7D RID: 19837
		public Text m_lbOptionCurrent;

		// Token: 0x04004D7E RID: 19838
		public GameObject m_objCurrentMax;

		// Token: 0x04004D7F RID: 19839
		public Text m_lbOptionCurrentMax;

		// Token: 0x04004D80 RID: 19840
		public GameObject m_objAfter;

		// Token: 0x04004D81 RID: 19841
		public Text m_lbOptionAfter;

		// Token: 0x04004D82 RID: 19842
		public GameObject m_objAfterMax;

		// Token: 0x04004D83 RID: 19843
		public Text m_lbOptionAfterMax;

		// Token: 0x04004D84 RID: 19844
		public GameObject m_objFocusLine;

		// Token: 0x04004D85 RID: 19845
		public Animator m_Ani;

		// Token: 0x04004D86 RID: 19846
		public NKCUIComStateButton m_btnLock;

		// Token: 0x04004D87 RID: 19847
		public GameObject m_objLocked;

		// Token: 0x04004D88 RID: 19848
		public GameObject m_objOpen;

		// Token: 0x04004D89 RID: 19849
		public GameObject m_objRerollFx;

		// Token: 0x04004D8A RID: 19850
		public GameObject m_objMaxFx;

		// Token: 0x04004D8B RID: 19851
		private int m_moduleIndex;

		// Token: 0x04004D8C RID: 19852
		private int m_socketIndex;

		// Token: 0x04004D8D RID: 19853
		private bool m_bLockChanged;

		// Token: 0x04004D8E RID: 19854
		private NKMShipCmdSlot m_curSlotData;

		// Token: 0x04004D8F RID: 19855
		private NKMShipCmdSlot m_nextSlotData;

		// Token: 0x04004D90 RID: 19856
		private int m_StatGroupId;

		// Token: 0x02001609 RID: 5641
		public enum SHIP_MODULE_SOCKET_STATE
		{
			// Token: 0x0400A2F5 RID: 41717
			UNLOCK,
			// Token: 0x0400A2F6 RID: 41718
			UNLOCK_TO_LOCK,
			// Token: 0x0400A2F7 RID: 41719
			LOCK,
			// Token: 0x0400A2F8 RID: 41720
			LOCK_TO_UNLOCK
		}
	}
}
