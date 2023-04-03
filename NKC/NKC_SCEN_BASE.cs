using System;
using System.Collections.Generic;
using ClientPacket.Unit;
using NKC.UI;
using NKC.UI.NPC;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x020006FB RID: 1787
	public class NKC_SCEN_BASE : NKC_SCEN_BASIC
	{
		// Token: 0x06004602 RID: 17922 RVA: 0x001547B6 File Offset: 0x001529B6
		public void SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve UIToOpen, long unitUID = 0L, bool bForce = false)
		{
			if (!bForce && this.CheckIgnoreReservedUI(UIToOpen))
			{
				return;
			}
			this.m_eUIOpenReserve = UIToOpen;
			this.m_reserveUnitUID = unitUID;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x001547D4 File Offset: 0x001529D4
		private bool CheckIgnoreReservedUI(NKC_SCEN_BASE.eUIOpenReserve UIToOpen)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_BASE)
			{
				return false;
			}
			switch (UIToOpen)
			{
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Train:
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Enchant:
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Transcendence:
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Negotiate:
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime:
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout:
				return false;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft:
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant:
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning:
				if (NKCUIForgeCraft.IsInstanceOpen)
				{
					return true;
				}
				if (NKCUIForge.IsInstanceOpen)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00154853 File Offset: 0x00152A53
		public NKC_SCEN_BASE()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_BASE;
			this.eLoadingUIType = NKC_SCEN_BASE.LoadingUIType.Base;
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00154874 File Offset: 0x00152A74
		private void BeginUILoading(NKC_SCEN_BASE.LoadingUIType type)
		{
			if (type == NKC_SCEN_BASE.LoadingUIType.Nothing)
			{
				return;
			}
			this.eLoadingUIType = type;
			switch (this.eLoadingUIType)
			{
			case NKC_SCEN_BASE.LoadingUIType.Base:
				if (!NKCUIManager.IsValid(this.m_UIDataBaseSceneMenu))
				{
					this.m_UIDataBaseSceneMenu = NKCUIManager.OpenNewInstanceAsync<NKCUIBaseSceneMenu>("ab_ui_nuf_base", "NKM_UI_BASE", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null);
				}
				break;
			case NKC_SCEN_BASE.LoadingUIType.Lab:
				if (!NKCUIManager.IsValid(this.m_UIDataLab))
				{
					this.m_UIDataLab = NKCUIManager.OpenNewInstanceAsync<NKCUILab>("ab_ui_nkm_ui_lab", "NKM_UI_LAB", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null);
				}
				break;
			}
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x0015490C File Offset: 0x00152B0C
		private NKCUIManager.LoadedUIData GetLoadedUIData(NKC_SCEN_BASE.LoadingUIType uiType)
		{
			if (uiType == NKC_SCEN_BASE.LoadingUIType.Base)
			{
				return this.m_UIDataBaseSceneMenu;
			}
			if (uiType != NKC_SCEN_BASE.LoadingUIType.Lab)
			{
				return null;
			}
			return this.m_UIDataLab;
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x00154928 File Offset: 0x00152B28
		private void ProcessUILoading(bool bOpenUIOnComplete)
		{
			if (this.eLoadingUIType == NKC_SCEN_BASE.LoadingUIType.Nothing && this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.Nothing)
			{
				return;
			}
			if (this.eLoadingUIType != NKC_SCEN_BASE.LoadingUIType.Nothing && this.GetLoadedUIData(this.eLoadingUIType) == null)
			{
				this.eLoadingUIType = NKC_SCEN_BASE.LoadingUIType.Nothing;
				Debug.LogError("Logic Error : UILoadResourceData not loaded");
			}
			switch (this.eLoadingUIType)
			{
			case NKC_SCEN_BASE.LoadingUIType.Base:
				if (this.m_UIDataBaseSceneMenu.CheckLoadAndGetInstance<NKCUIBaseSceneMenu>(out this.m_NKCUIBaseSceneMenu))
				{
					this.m_NKCUIBaseSceneMenu.Init(new UnityAction<NKC_SCEN_BASE.eUIOpenReserve>(this.BeginUILoading), new UnityAction(this.OpenSubMenu));
					this.eLoadingUIType = NKC_SCEN_BASE.LoadingUIType.Nothing;
					NKMPopUpBox.CloseWaitBox();
					if (bOpenUIOnComplete)
					{
						this.m_NKCUIBaseSceneMenu.Open(false);
					}
					this.m_bBaseSceneMenuInitComplete = true;
				}
				break;
			case NKC_SCEN_BASE.LoadingUIType.Lab:
				if (this.m_UIDataLab.CheckLoadAndGetInstance<NKCUILab>(out this.m_NKCUILab))
				{
					NKCUILab nkcuilab = this.m_NKCUILab;
					NKCUILabLimitBreak.OnTryLimitBreak onTryLimitBreak = new NKCUILabLimitBreak.OnTryLimitBreak(NKCPacketSender.Send_Packet_NKMPacket_LIMIT_BREAK_UNIT_REQ);
					NKCUILabSkillTrain.OnTrySkillTrain onTrySkillTrain = new NKCUILabSkillTrain.OnTrySkillTrain(NKCPacketSender.Send_Packet_NKMPacket_UNIT_SKILL_UPGRADE_REQ);
					GameObject objNPCLab_Professor_TouchArea = this.m_NKCUIBaseSceneMenu.m_objNPCLab_Professor_TouchArea;
					nkcuilab.InitUI(onTryLimitBreak, onTrySkillTrain, (objNPCLab_Professor_TouchArea != null) ? objNPCLab_Professor_TouchArea.GetComponent<NKCUINPCProfessorOlivia>() : null);
					this.eLoadingUIType = NKC_SCEN_BASE.LoadingUIType.Nothing;
					NKMPopUpBox.CloseWaitBox();
					if (bOpenUIOnComplete)
					{
						this.OpenLab();
					}
				}
				break;
			}
			if (bOpenUIOnComplete && this.m_bBaseSceneMenuInitComplete && this.m_eUIOpenReserve != NKC_SCEN_BASE.eUIOpenReserve.Nothing)
			{
				this.OpenSubMenu();
			}
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x00154A69 File Offset: 0x00152C69
		private void BeginUILoading(NKC_SCEN_BASE.eUIOpenReserve openBtnType)
		{
			this.m_eUIOpenReserve = openBtnType;
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x00154A74 File Offset: 0x00152C74
		private void OpenSubMenu()
		{
			if (this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.Nothing)
			{
				return;
			}
			this.CloseOpenedUI(this.m_eUIOpenReserve);
			switch (this.m_eUIOpenReserve)
			{
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Train:
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Enchant:
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Transcendence:
				this.OpenLab();
				return;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft:
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant:
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning:
				this.OpenFactory();
				return;
			case NKC_SCEN_BASE.eUIOpenReserve.Hangar_Build:
			case NKC_SCEN_BASE.eUIOpenReserve.Hangar_Shipyard:
				this.OpenHanger(this.m_eUIOpenReserve);
				return;
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Negotiate:
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime:
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout:
				this.OpenHR();
				return;
			case NKC_SCEN_BASE.eUIOpenReserve.Base_Main:
				this.m_NKCUIBaseSceneMenu.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Base, true);
				this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
				return;
			}
			Debug.LogError(string.Format("NKC_SCEN_BASE::OpenSubMenu - type not matched! : {0}", this.m_eUIOpenReserve));
			this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00154B4C File Offset: 0x00152D4C
		private void OpenLab()
		{
			if (!(this.m_NKCUILab != null))
			{
				this.BeginUILoading(NKC_SCEN_BASE.LoadingUIType.Lab);
				return;
			}
			if (this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.LAB_Transcendence)
			{
				this.m_SelectLabDetailState = NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK;
			}
			else if (this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.LAB_Enchant)
			{
				this.m_SelectLabDetailState = NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE;
			}
			else if (this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.LAB_Train)
			{
				this.m_SelectLabDetailState = NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN;
			}
			this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
			if (this.m_SelectLabDetailState == NKCUILab.LAB_DETAIL_STATE.LDS_INVALID)
			{
				return;
			}
			this.m_NKCUIBaseSceneMenu.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Lab, true);
			this.m_NKCUILab.Open(this.m_SelectLabDetailState, this.m_reserveUnitUID);
			this.m_reserveUnitUID = 0L;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x00154BE0 File Offset: 0x00152DE0
		private void OpenHR()
		{
			if (this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout)
			{
				this.m_NKCUIBaseSceneMenu.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel, true);
				NKCUIScout.Instance.Open();
				this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
				return;
			}
			if (this.m_eUIOpenReserve == NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime)
			{
				this.m_NKCUIBaseSceneMenu.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel, true);
				NKCUIPersonnel.Instance.Open();
				NKMUnitData unitData;
				if (this.FindReserveUnit(out unitData))
				{
					NKCUIPersonnel.Instance.ReserveUnitData(unitData);
				}
				this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
				return;
			}
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00154C54 File Offset: 0x00152E54
		public void OpenFactory()
		{
			this.m_NKCUIBaseSceneMenu.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Factory, true);
			switch (this.m_eUIOpenReserve)
			{
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft:
				NKCUIForgeCraft.Instance.Open();
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant:
				NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, 0L, null);
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning:
				NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_TUNING, 0L, null);
				break;
			}
			this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00154CBC File Offset: 0x00152EBC
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.eLoadingUIType == NKC_SCEN_BASE.LoadingUIType.Nothing && !NKCUIManager.IsValid(this.m_UIDataBaseSceneMenu))
			{
				this.eLoadingUIType = NKC_SCEN_BASE.LoadingUIType.Base;
			}
			this.BeginUILoading(this.eLoadingUIType);
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00154CEC File Offset: 0x00152EEC
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			this.ProcessUILoading(false);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00154CFB File Offset: 0x00152EFB
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00154D03 File Offset: 0x00152F03
		public override void ScenStart()
		{
			base.ScenStart();
			this.Open();
			NKCCamera.EnableBloom(false);
			NKCCamera.GetCamera().orthographic = false;
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, -1000f);
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x00154D3C File Offset: 0x00152F3C
		public override void ScenEnd()
		{
			NKCUIGameResultGetUnit.CheckInstanceAndClose();
			if (this.m_NKCUILab != null && this.m_NKCUILab.IsOpen)
			{
				this.m_NKCUILab.Close();
			}
			this.m_NKCUILab = null;
			if (this.m_NKCUIBaseSceneMenu != null && this.m_NKCUIBaseSceneMenu.IsOpen)
			{
				this.m_NKCUIBaseSceneMenu.Close();
			}
			this.m_NKCUIBaseSceneMenu = null;
			if (this.m_UIDataBaseSceneMenu != null)
			{
				this.m_UIDataBaseSceneMenu.CloseInstance();
			}
			this.m_UIDataBaseSceneMenu = null;
			if (this.m_UIDataLab != null)
			{
				this.m_UIDataLab.CloseInstance();
			}
			this.m_UIDataLab = null;
			base.ScenEnd();
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00154DE2 File Offset: 0x00152FE2
		public void Open()
		{
			if (this.m_NKCUIBaseSceneMenu != null)
			{
				this.m_NKCUIBaseSceneMenu.Open(this.m_eUIOpenReserve > NKC_SCEN_BASE.eUIOpenReserve.Nothing);
			}
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x00154E08 File Offset: 0x00153008
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			this.ProcessUILoading(true);
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-30f, 30f), NKMRandom.Range(-30f, 30f), NKMRandom.Range(-1000f, -950f));
			}
			this.m_BloomIntensity.Update(Time.deltaTime);
			if (!this.m_BloomIntensity.IsTracking())
			{
				this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x00154EAD File Offset: 0x001530AD
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00154EB0 File Offset: 0x001530B0
		public void OnRecv(NKMPacket_LIMIT_BREAK_UNIT_ACK sPacket)
		{
			if (this.m_NKCUILab != null && this.m_NKCUILab.IsOpen)
			{
				NKCUIGameResultGetUnit.ShowUnitTranscendence(sPacket.unitData, delegate
				{
					if (NKCGameEventManager.IsEventPlaying())
					{
						NKCGameEventManager.WaitFinished();
						return;
					}
					this.m_NKCUILab.TutorialCheckUnit();
				});
			}
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x00154EE4 File Offset: 0x001530E4
		public void OnRecv(NKMPacket_ENHANCE_UNIT_ACK sPacket)
		{
			if (this.m_NKCUILab != null && this.m_NKCUILab.IsOpen)
			{
				this.m_NKCUILab.OnRecv(sPacket);
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x00154F0D File Offset: 0x0015310D
		public void OnRecv(NKMPacket_UNIT_SKILL_UPGRADE_ACK sPacket)
		{
			if (this.m_NKCUILab != null && this.m_NKCUILab.IsOpen)
			{
				this.m_NKCUILab.OnRecv(sPacket);
			}
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x00154F38 File Offset: 0x00153138
		private void OnFinishMultiSelectionToRemoveEquip(List<long> listEquipSlot)
		{
			if (listEquipSlot == null || listEquipSlot.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_SELECTED_EQUIP, null, "");
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			NKCPopupOKCancel.OnButton <>9__0;
			for (int i = 0; i < listEquipSlot.Count; i++)
			{
				NKMEquipItemData itemEquip = inventoryData.GetItemEquip(listEquipSlot[i]);
				if (itemEquip != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
					if (equipTemplet != null && equipTemplet.m_NKM_ITEM_GRADE >= NKM_ITEM_GRADE.NIG_SR)
					{
						string get_STRING_NOTICE = NKCUtilString.GET_STRING_NOTICE;
						string get_STRING_EQUIP_BREAK_UP_WARNING = NKCUtilString.GET_STRING_EQUIP_BREAK_UP_WARNING;
						NKCPopupOKCancel.OnButton onOkButton;
						if ((onOkButton = <>9__0) == null)
						{
							onOkButton = (<>9__0 = delegate()
							{
								NKCPacketSender.Send_NKMPacket_REMOVE_EQUIP_ITEM_REQ(listEquipSlot);
							});
						}
						NKCPopupOKCancel.OpenOKCancelBox(get_STRING_NOTICE, get_STRING_EQUIP_BREAK_UP_WARNING, onOkButton, null, false);
						return;
					}
				}
			}
			NKCPacketSender.Send_NKMPacket_REMOVE_EQUIP_ITEM_REQ(listEquipSlot);
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x00155013 File Offset: 0x00153213
		private NKMUserData UserData
		{
			get
			{
				return NKCScenManager.CurrentUserData();
			}
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x0015501A File Offset: 0x0015321A
		private void InitAnchor()
		{
			if (this.m_NUF_COMMON_Panel == null)
			{
				this.m_NUF_COMMON_Panel = NKCUIManager.OpenUI("NUF_COMMON_Panel").GetComponent<RectTransform>();
			}
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x00155040 File Offset: 0x00153240
		private void OpenHanger(NKC_SCEN_BASE.eUIOpenReserve newState)
		{
			this.InitAnchor();
			this.m_NKCUIBaseSceneMenu.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar, true);
			NKC_SCEN_BASE.eUIOpenReserve eUIOpenReserve = this.m_eUIOpenReserve;
			if (eUIOpenReserve - NKC_SCEN_BASE.eUIOpenReserve.Hangar_Build <= 1)
			{
				this.m_reserveUnitUID = 0L;
			}
			this.m_eUIOpenReserve = NKC_SCEN_BASE.eUIOpenReserve.Nothing;
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x0015507D File Offset: 0x0015327D
		private bool FindReserveUnit(out NKMUnitData result)
		{
			result = null;
			if (this.m_reserveUnitUID > 0L)
			{
				result = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_reserveUnitUID);
				this.m_reserveUnitUID = 0L;
			}
			return result != null;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x001550B0 File Offset: 0x001532B0
		public void CloseOpenedUI(NKC_SCEN_BASE.eUIOpenReserve reservedUI)
		{
			if (reservedUI == NKC_SCEN_BASE.eUIOpenReserve.Nothing)
			{
				return;
			}
			if (reservedUI != NKC_SCEN_BASE.eUIOpenReserve.LAB_Enchant && reservedUI != NKC_SCEN_BASE.eUIOpenReserve.LAB_Train && reservedUI != NKC_SCEN_BASE.eUIOpenReserve.LAB_Transcendence && this.m_NKCUILab != null && this.m_NKCUILab.IsOpen)
			{
				this.m_NKCUILab.Close();
			}
			if (reservedUI != NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft && reservedUI != NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant && reservedUI != NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning)
			{
				NKCUIForge.CheckInstanceAndClose();
				NKCUIForgeCraft.CheckInstanceAndClose();
				NKCUIInventory.CheckInstanceAndClose();
			}
			if (reservedUI != NKC_SCEN_BASE.eUIOpenReserve.Personnel_Negotiate && NKCUIUnitSelectList.Instance != null && NKCUIUnitSelectList.Instance.isActiveAndEnabled)
			{
				NKCUIUnitSelectList.Instance.Close();
			}
			if (reservedUI != NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout && NKCUIScout.IsInstanceOpen)
			{
				NKCUIScout.Instance.Close();
			}
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x0015514D File Offset: 0x0015334D
		public void TutorialCheck(NKC_SCEN_BASE.eUIOpenReserve openUi)
		{
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0015514F File Offset: 0x0015334F
		public void SetBaseMenuType(NKCUIBaseSceneMenu.BaseSceneMenuType menu)
		{
			this.m_NKCUIBaseSceneMenu.ChangeMenu(menu, false);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x0015515E File Offset: 0x0015335E
		public NKCUILab GetUILab()
		{
			return this.m_NKCUILab;
		}

		// Token: 0x0400374A RID: 14154
		private NKCUIManager.LoadedUIData m_UIDataBaseSceneMenu;

		// Token: 0x0400374B RID: 14155
		private NKCUIBaseSceneMenu m_NKCUIBaseSceneMenu;

		// Token: 0x0400374C RID: 14156
		private NKCUIManager.LoadedUIData m_UIDataLab;

		// Token: 0x0400374D RID: 14157
		private NKCUILab m_NKCUILab;

		// Token: 0x0400374E RID: 14158
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x0400374F RID: 14159
		public GameObject m_objNUF_BASE_Panel;

		// Token: 0x04003750 RID: 14160
		private bool m_bBaseSceneMenuInitComplete;

		// Token: 0x04003751 RID: 14161
		private NKC_SCEN_BASE.eUIOpenReserve m_eUIOpenReserve;

		// Token: 0x04003752 RID: 14162
		private long m_reserveUnitUID;

		// Token: 0x04003753 RID: 14163
		private NKC_SCEN_BASE.LoadingUIType eLoadingUIType;

		// Token: 0x04003754 RID: 14164
		private NKCUILab.LAB_DETAIL_STATE m_SelectLabDetailState;

		// Token: 0x04003755 RID: 14165
		private RectTransform m_NUF_COMMON_Panel;

		// Token: 0x020013DB RID: 5083
		public enum eUIOpenReserve
		{
			// Token: 0x04009C4D RID: 40013
			Nothing,
			// Token: 0x04009C4E RID: 40014
			LAB_Begin,
			// Token: 0x04009C4F RID: 40015
			LAB_Train,
			// Token: 0x04009C50 RID: 40016
			LAB_Enchant,
			// Token: 0x04009C51 RID: 40017
			LAB_Transcendence,
			// Token: 0x04009C52 RID: 40018
			LAB_End,
			// Token: 0x04009C53 RID: 40019
			Factory_Begin,
			// Token: 0x04009C54 RID: 40020
			Factory_Craft,
			// Token: 0x04009C55 RID: 40021
			Factory_Enchant,
			// Token: 0x04009C56 RID: 40022
			Factory_Tunning,
			// Token: 0x04009C57 RID: 40023
			Factory_End,
			// Token: 0x04009C58 RID: 40024
			Hangar_Begin,
			// Token: 0x04009C59 RID: 40025
			Hangar_Build,
			// Token: 0x04009C5A RID: 40026
			Hangar_Shipyard,
			// Token: 0x04009C5B RID: 40027
			Hangar_End,
			// Token: 0x04009C5C RID: 40028
			Personnel_Begin,
			// Token: 0x04009C5D RID: 40029
			Personnel_Negotiate,
			// Token: 0x04009C5E RID: 40030
			Personnel_Lifetime,
			// Token: 0x04009C5F RID: 40031
			Personnel_Scout,
			// Token: 0x04009C60 RID: 40032
			Personnel_End,
			// Token: 0x04009C61 RID: 40033
			Base_Main,
			// Token: 0x04009C62 RID: 40034
			FORGE_Enchant
		}

		// Token: 0x020013DC RID: 5084
		private enum LoadingUIType
		{
			// Token: 0x04009C64 RID: 40036
			Nothing,
			// Token: 0x04009C65 RID: 40037
			Base,
			// Token: 0x04009C66 RID: 40038
			Lab,
			// Token: 0x04009C67 RID: 40039
			Hangar,
			// Token: 0x04009C68 RID: 40040
			Personnel
		}
	}
}
