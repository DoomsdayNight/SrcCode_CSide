using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200096C RID: 2412
	public class NKCPopupShipCommandModule : NKCUIBase
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x001E6B98 File Offset: 0x001E4D98
		public static NKCPopupShipCommandModule Instance
		{
			get
			{
				if (NKCPopupShipCommandModule.m_Instance == null)
				{
					NKCPopupShipCommandModule.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShipCommandModule>("ab_ui_nkm_ui_ship_info", "NKM_UI_POPUP_SHIP_MODULE", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShipCommandModule.CleanupInstance)).GetInstance<NKCPopupShipCommandModule>();
					NKCPopupShipCommandModule.m_Instance.Init();
				}
				return NKCPopupShipCommandModule.m_Instance;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06006123 RID: 24867 RVA: 0x001E6BE7 File Offset: 0x001E4DE7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupShipCommandModule.m_Instance != null && NKCPopupShipCommandModule.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x001E6C02 File Offset: 0x001E4E02
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShipCommandModule.m_Instance != null && NKCPopupShipCommandModule.m_Instance.IsOpen)
			{
				NKCPopupShipCommandModule.m_Instance.Close();
			}
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x001E6C27 File Offset: 0x001E4E27
		private static void CleanupInstance()
		{
			NKCPopupShipCommandModule.m_Instance = null;
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x001E6C2F File Offset: 0x001E4E2F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x001E6C32 File Offset: 0x001E4E32
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06006128 RID: 24872 RVA: 0x001E6C39 File Offset: 0x001E4E39
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SHIP_COMMANDMODULE";
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x001E6C40 File Offset: 0x001E4E40
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x001E6C44 File Offset: 0x001E4E44
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_curShipData == null)
				{
					return base.UpsideMenuShowResourceList;
				}
				List<int> list = new List<int>();
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_curShipData.m_UnitID);
				if (unitTempletBase == null)
				{
					return base.UpsideMenuShowResourceList;
				}
				NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = NKMShipManager.GetNKMShipCommandModuleTemplet(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, 1);
				for (int i = 0; i < nkmshipCommandModuleTemplet.ModuleReqItems.Count; i++)
				{
					if (!list.Contains(nkmshipCommandModuleTemplet.ModuleReqItems[i].ItemId))
					{
						list.Add(nkmshipCommandModuleTemplet.ModuleReqItems[i].ItemId);
					}
				}
				for (int j = 0; j < nkmshipCommandModuleTemplet.ModuleLockItems.Count; j++)
				{
					if (!list.Contains(nkmshipCommandModuleTemplet.ModuleLockItems[j].ItemId))
					{
						list.Add(nkmshipCommandModuleTemplet.ModuleLockItems[j].ItemId);
					}
				}
				if (list.Count > 0)
				{
					return list;
				}
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x001E6D3A File Offset: 0x001E4F3A
		public NKMUnitData GetShipData()
		{
			return this.m_curShipData;
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x001E6D42 File Offset: 0x001E4F42
		public long GetShipUID()
		{
			if (this.m_curShipData == null)
			{
				return 0L;
			}
			return this.m_curShipData.m_UnitUID;
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x001E6D5C File Offset: 0x001E4F5C
		public override void CloseInternal()
		{
			this.m_curShipData = null;
			this.m_curSelectedModuleIndex = -1;
			this.m_curOpenModuleIndex = -1;
			for (int i = 0; i < this.m_lstModuleSlot.Count; i++)
			{
				this.m_lstModuleSlot[i].CloseInternal();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x001E6DB4 File Offset: 0x001E4FB4
		private void Init()
		{
			for (int i = 0; i < this.m_lstModuleSlot.Count; i++)
			{
				this.m_lstModuleSlot[i].Init(new NKCPopupShipCommandModuleSlot.OnSetting(this.OnClickOpen), new NKCPopupShipCommandModuleSlot.OnSetting(this.OnClickSetting));
			}
			for (int j = 0; j < this.m_lstCostSlot.Count; j++)
			{
				this.m_lstCostSlot[j].SetData(0, 0, 0L, true, true, false);
			}
			this.m_btnReroll.PointerClick.RemoveAllListeners();
			this.m_btnReroll.PointerClick.AddListener(new UnityAction(this.OnClickReroll));
			this.m_btnReroll.m_bGetCallbackWhileLocked = true;
			this.m_btnConfirm.PointerClick.RemoveAllListeners();
			this.m_btnConfirm.PointerClick.AddListener(new UnityAction(this.OnClickConfirm));
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x001E6E94 File Offset: 0x001E5094
		public override void Hide()
		{
			base.Hide();
			for (int i = 0; i < this.m_lstModuleSlot.Count; i++)
			{
				this.m_lstModuleSlot[i].DisableAllFx();
			}
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x001E6ECE File Offset: 0x001E50CE
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_curSelectedModuleIndex < 0)
			{
				this.m_Ani.SetTrigger("01_IDLE");
			}
			else
			{
				this.m_Ani.SetTrigger("02_IDLE");
			}
			this.m_btnReroll.enabled = true;
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x001E6F10 File Offset: 0x001E5110
		public void Open(NKMUnitData shipData, int reservedModuleIndex = -1)
		{
			if (!NKMShipManager.HasNKMShipCommandModuleTemplet(shipData))
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_curShipData = shipData;
			this.m_curSelectedModuleIndex = reservedModuleIndex;
			this.m_curOpenModuleIndex = -1;
			this.m_btnReroll.enabled = true;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_curShipData.m_UnitID);
			NKCUtil.SetImageSprite(this.m_imgShip, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase), false);
			NKCUtil.SetImageSprite(this.m_imgShip_02, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase), false);
			NKCUtil.SetLabelText(this.m_lbShipName, unitTempletBase.GetUnitName());
			NKCUtil.SetLabelText(this.m_lbShipName_02, unitTempletBase.GetUnitName());
			base.UpdateUpsideMenu();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_curSelectedModuleIndex < 0)
			{
				this.m_Ani.SetTrigger("INTRO");
			}
			else
			{
				this.m_Ani.SetTrigger("02_IDLE");
			}
			this.SetData();
			this.SetModuleState();
			base.UIOpened(true);
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x001E6FFC File Offset: 0x001E51FC
		private void SetData()
		{
			NKMShipModuleCandidate shipCandidateData = NKCScenManager.CurrentUserData().GetShipCandidateData();
			for (int i = 0; i < this.m_lstModuleSlot.Count; i++)
			{
				if (i < this.m_curShipData.ShipCommandModule.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStep[i], true);
					if (shipCandidateData.shipUid == this.m_curShipData.m_UnitUID && shipCandidateData.moduleId == i)
					{
						this.m_lstModuleSlot[i].SetData(i, this.m_curShipData.ShipCommandModule[i], shipCandidateData.slotCandidate);
					}
					else
					{
						this.m_lstModuleSlot[i].SetData(i, this.m_curShipData.ShipCommandModule[i], null);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStep[i], false);
					this.m_lstModuleSlot[i].SetData(i, null, null);
				}
				if (this.m_curSelectedModuleIndex < 0)
				{
					this.m_lstModuleSlot[i].SetDisable(false);
				}
				else
				{
					this.m_lstModuleSlot[i].SetDisable(this.m_curSelectedModuleIndex != i);
				}
			}
			this.SetBottomInfo();
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x001E7128 File Offset: 0x001E5328
		private void SetModuleState()
		{
			for (int i = 0; i < this.m_lstModuleSlot.Count; i++)
			{
				if (this.m_curSelectedModuleIndex == i)
				{
					this.m_lstModuleSlot[i].SetState(NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.IDLE_02);
				}
				else
				{
					this.m_lstModuleSlot[i].SetState(NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.IDLE_01);
				}
			}
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x001E717C File Offset: 0x001E537C
		private void OnClickOpen(int slotIndex)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMShipManager.CanModuleOptionChange(this.m_curShipData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.m_curOpenModuleIndex = slotIndex;
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_FIRST_OPTION_REQ(this.m_curShipData.m_UnitUID, slotIndex);
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x001E71C0 File Offset: 0x001E53C0
		private void OnClickSetting(int moduleIndex)
		{
			if (moduleIndex == this.m_curSelectedModuleIndex)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid > 0L)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_EXIT_CONFIRM, delegate()
				{
					this.OnSlotChange(moduleIndex);
				}, null, false);
				return;
			}
			if (this.m_curSelectedModuleIndex < 0)
			{
				this.m_Ani.SetTrigger("01_TO_02");
			}
			else
			{
				this.m_lstModuleSlot[this.m_curSelectedModuleIndex].SetState(NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.SET_02_TO_01);
				this.m_Ani.SetTrigger("02_IDLE");
			}
			for (int i = 0; i < this.m_lstModuleSlot.Count; i++)
			{
				this.m_lstModuleSlot[i].SetDisable(i != moduleIndex);
			}
			this.m_curSelectedModuleIndex = moduleIndex;
			this.m_lstModuleSlot[moduleIndex].SetState(NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.SET_01_TO_02);
			this.SetBottomInfo();
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x001E72BF File Offset: 0x001E54BF
		private void OnSlotChange(int targetModuleIndex)
		{
			this.m_targetSlotIndex = targetModuleIndex;
			this.bSlotChange = true;
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x001E72D4 File Offset: 0x001E54D4
		private void SetBottomInfo()
		{
			if (this.m_curSelectedModuleIndex < 0 || this.m_curSelectedModuleIndex >= this.m_curShipData.ShipCommandModule.Count)
			{
				this.SetTargetSocket(true, true);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_curShipData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = NKMShipManager.GetNKMShipCommandModuleTemplet(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, this.m_curSelectedModuleIndex + 1);
			if (nkmshipCommandModuleTemplet == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lstCostSlot.Count; i++)
			{
				if (i < nkmshipCommandModuleTemplet.ModuleReqItems.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstCostSlot[i], true);
					this.m_lstCostSlot[i].SetData(nkmshipCommandModuleTemplet.ModuleReqItems[i].ItemId, nkmshipCommandModuleTemplet.ModuleReqItems[i].Count32, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(nkmshipCommandModuleTemplet.ModuleReqItems[i].ItemId), true, true, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstCostSlot[i], false);
				}
			}
			if (this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots != null && this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots.Length > 1)
			{
				this.SetTargetSocket(this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots[0].isLock, this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots[1].isLock);
			}
			bool flag = false;
			for (int j = 0; j < this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots.Length; j++)
			{
				if (!this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots[j].isLock)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.m_btnReroll.Lock(false);
			}
			else
			{
				this.m_btnReroll.UnLock(false);
			}
			if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid == 0L)
			{
				this.m_btnConfirm.Lock(false);
				return;
			}
			this.m_btnConfirm.UnLock(false);
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x001E7507 File Offset: 0x001E5707
		private void SetTargetSocket(bool bSocket_01_locked, bool bSocket_02_locked)
		{
			NKCUtil.SetGameobjectActive(this.m_objTargetSocket_01_ON, !bSocket_01_locked);
			NKCUtil.SetGameobjectActive(this.m_objTargetSocket_01_OFF, bSocket_01_locked);
			NKCUtil.SetGameobjectActive(this.m_objTargetSocket_02_ON, !bSocket_02_locked);
			NKCUtil.SetGameobjectActive(this.m_objTargetSocket_02_OFF, bSocket_02_locked);
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x001E7540 File Offset: 0x001E5740
		private void OnClickReroll()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_curShipData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = NKMShipManager.GetNKMShipCommandModuleTemplet(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, this.m_curSelectedModuleIndex + 1);
			if (nkmshipCommandModuleTemplet == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots.Length; i++)
			{
				NKMShipCmdSlot nkmshipCmdSlot = this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex].slots[i];
				if (nkmshipCmdSlot != null && !nkmshipCmdSlot.isLock)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE_SLOT_ALL_LOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag2 = true;
			MiscItemUnit miscItemUnit = null;
			for (int j = 0; j < nkmshipCommandModuleTemplet.ModuleReqItems.Count; j++)
			{
				if (nkmuserData.m_InventoryData.GetCountMiscItem(nkmshipCommandModuleTemplet.ModuleReqItems[j].ItemId) < (long)nkmshipCommandModuleTemplet.ModuleReqItems[j].Count32)
				{
					flag2 = false;
					miscItemUnit = nkmshipCommandModuleTemplet.ModuleReqItems[j];
					break;
				}
			}
			if (!flag2)
			{
				NKCPopupItemLack.Instance.OpenItemMiscLackPopup(miscItemUnit.ItemId, miscItemUnit.Count32);
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMShipManager.CanModuleOptionChange(this.m_curShipData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.m_btnReroll.enabled = false;
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CHANGE_REQ(this.m_curShipData.m_UnitUID, this.m_curSelectedModuleIndex);
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x001E76C3 File Offset: 0x001E58C3
		private void OnClickConfirm()
		{
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CONFIRM_REQ(this.m_curShipData.m_UnitUID, this.m_curSelectedModuleIndex);
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x001E76DC File Offset: 0x001E58DC
		public void CandidateChanged()
		{
			this.m_btnReroll.enabled = true;
			if (this.m_curSelectedModuleIndex < this.m_lstModuleSlot.Count)
			{
				if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid > 0L)
				{
					this.m_lstModuleSlot[this.m_curSelectedModuleIndex].ShowRerollFx();
				}
				else
				{
					this.m_lstModuleSlot[this.m_curSelectedModuleIndex].ShowConfirmFx();
				}
			}
			this.SetBottomInfo();
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x001E774F File Offset: 0x001E594F
		public void ShowModuleOpenFx()
		{
			if (this.m_curOpenModuleIndex >= 0)
			{
				this.m_lstModuleSlot[this.m_curOpenModuleIndex].ShowOpenFx();
				this.m_curOpenModuleIndex = -1;
			}
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x001E7778 File Offset: 0x001E5978
		public override bool OnHomeButton()
		{
			if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid > 0L && this.m_curSelectedModuleIndex >= 0)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_EXIT_CONFIRM, new NKCPopupOKCancel.OnButton(this.OnHomeConfirm), null, false);
				return false;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
			return true;
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x001E77CD File Offset: 0x001E59CD
		private void OnHomeConfirm()
		{
			this.bHomeButton = true;
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x001E77DC File Offset: 0x001E59DC
		public override void OnBackButton()
		{
			if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid > 0L && this.m_curSelectedModuleIndex >= 0)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_EXIT_CONFIRM, new NKCPopupOKCancel.OnButton(this.OnBackConfirm), null, false);
				return;
			}
			if (this.m_curSelectedModuleIndex < 0)
			{
				base.OnBackButton();
				return;
			}
			this.m_Ani.SetTrigger("02_TO_01");
			this.m_lstModuleSlot[this.m_curSelectedModuleIndex].SetState(NKCPopupShipCommandModuleSlot.SHIP_MODULE_STATE.SET_02_TO_01);
			this.m_curSelectedModuleIndex = -1;
			this.SetData();
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x001E7867 File Offset: 0x001E5A67
		private void OnBackConfirm()
		{
			this.bBackButton = true;
			NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x001E7878 File Offset: 0x001E5A78
		public void OnCandidateRemoved()
		{
			if (this.bBackButton)
			{
				this.bBackButton = false;
				this.m_lstModuleSlot[this.m_curSelectedModuleIndex].SetData(this.m_curSelectedModuleIndex, this.m_curShipData.ShipCommandModule[this.m_curSelectedModuleIndex], null);
				this.OnBackButton();
				return;
			}
			if (this.bHomeButton)
			{
				this.bHomeButton = false;
				this.m_curSelectedModuleIndex = -1;
				this.OnHomeButton();
				return;
			}
			if (this.bSlotChange)
			{
				this.bSlotChange = false;
				this.SetData();
				this.OnClickSetting(this.m_targetSlotIndex);
				this.m_targetSlotIndex = -1;
				return;
			}
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x001E7915 File Offset: 0x001E5B15
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Update)
			{
				base.OnUnitUpdate(eEventType, eUnitType, uid, unitData);
				if (this.m_curShipData.m_UnitUID == uid)
				{
					this.m_curShipData = unitData;
				}
				this.SetData();
				this.SetModuleState();
			}
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x001E7948 File Offset: 0x001E5B48
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
			if (this.UpsideMenuShowResourceList.Contains(itemData.ItemID))
			{
				this.SetBottomInfo();
			}
		}

		// Token: 0x04004D55 RID: 19797
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_ship_info";

		// Token: 0x04004D56 RID: 19798
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHIP_MODULE";

		// Token: 0x04004D57 RID: 19799
		private static NKCPopupShipCommandModule m_Instance;

		// Token: 0x04004D58 RID: 19800
		public Animator m_Ani;

		// Token: 0x04004D59 RID: 19801
		public Image m_imgShip;

		// Token: 0x04004D5A RID: 19802
		public Image m_imgShip_02;

		// Token: 0x04004D5B RID: 19803
		public Text m_lbShipName;

		// Token: 0x04004D5C RID: 19804
		public Text m_lbShipName_02;

		// Token: 0x04004D5D RID: 19805
		public List<GameObject> m_lstModuleStep = new List<GameObject>();

		// Token: 0x04004D5E RID: 19806
		public List<NKCPopupShipCommandModuleSlot> m_lstModuleSlot = new List<NKCPopupShipCommandModuleSlot>();

		// Token: 0x04004D5F RID: 19807
		public GameObject m_objTargetSocket_01_ON;

		// Token: 0x04004D60 RID: 19808
		public GameObject m_objTargetSocket_01_OFF;

		// Token: 0x04004D61 RID: 19809
		public GameObject m_objTargetSocket_02_ON;

		// Token: 0x04004D62 RID: 19810
		public GameObject m_objTargetSocket_02_OFF;

		// Token: 0x04004D63 RID: 19811
		public List<NKCUIItemCostSlot> m_lstCostSlot = new List<NKCUIItemCostSlot>();

		// Token: 0x04004D64 RID: 19812
		public NKCUIComStateButton m_btnReroll;

		// Token: 0x04004D65 RID: 19813
		public NKCUIComStateButton m_btnConfirm;

		// Token: 0x04004D66 RID: 19814
		private NKMUnitData m_curShipData;

		// Token: 0x04004D67 RID: 19815
		private int m_curSelectedModuleIndex;

		// Token: 0x04004D68 RID: 19816
		private int m_curOpenModuleIndex;

		// Token: 0x04004D69 RID: 19817
		private bool bBackButton;

		// Token: 0x04004D6A RID: 19818
		private bool bHomeButton;

		// Token: 0x04004D6B RID: 19819
		private bool bSlotChange;

		// Token: 0x04004D6C RID: 19820
		private int m_targetSlotIndex = -1;
	}
}
