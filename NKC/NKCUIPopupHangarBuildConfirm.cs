using System;
using System.Collections.Generic;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A93 RID: 2707
	public class NKCUIPopupHangarBuildConfirm : NKCUIBase
	{
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060077D5 RID: 30677 RVA: 0x0027D1F0 File Offset: 0x0027B3F0
		public static NKCUIPopupHangarBuildConfirm Instance
		{
			get
			{
				if (NKCUIPopupHangarBuildConfirm.m_Instance == null)
				{
					NKCUIPopupHangarBuildConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupHangarBuildConfirm>("ab_ui_nkm_ui_hangar", "NKM_UI_POPUP_HANGAR_BUILD_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupHangarBuildConfirm.CleanupInstance)).GetInstance<NKCUIPopupHangarBuildConfirm>();
					NKCUIPopupHangarBuildConfirm.m_Instance.InitUI();
				}
				return NKCUIPopupHangarBuildConfirm.m_Instance;
			}
		}

		// Token: 0x060077D6 RID: 30678 RVA: 0x0027D23F File Offset: 0x0027B43F
		private static void CleanupInstance()
		{
			NKCUIPopupHangarBuildConfirm.m_Instance = null;
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060077D7 RID: 30679 RVA: 0x0027D247 File Offset: 0x0027B447
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060077D8 RID: 30680 RVA: 0x0027D24A File Offset: 0x0027B44A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_MENU_NAME_BUILD_CONFIRM;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060077D9 RID: 30681 RVA: 0x0027D251 File Offset: 0x0027B451
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060077DA RID: 30682 RVA: 0x0027D259 File Offset: 0x0027B459
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x060077DB RID: 30683 RVA: 0x0027D25C File Offset: 0x0027B45C
		public override void CloseInternal()
		{
			if (this.dCloseAction != null)
			{
				this.dCloseAction();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x0027D280 File Offset: 0x0027B480
		public void InitUI()
		{
			if (this.m_NKCUINPCHangarNaHeeRin == null)
			{
				this.m_NKCUINPCHangarNaHeeRin = this.m_AB_NPC_NA_HEE_RIN_TOUCH.GetComponent<NKCUINPCHangarNaHeeRin>();
				this.m_NKCUINPCHangarNaHeeRin.Init(true);
			}
			else
			{
				this.m_NKCUINPCHangarNaHeeRin.PlayAni(NPC_ACTION_TYPE.START, false);
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK, new UnityAction(this.OnTryBuild));
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_OK_CANCEL_BOX_CANCEL, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CLOSEBUTTON, new UnityAction(base.Close));
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x0027D31C File Offset: 0x0027B51C
		public void Open(int shipID, NKCUIPopupHangarBuildConfirm.OnTryBuildShip tryBuildShip, UnityAction closeAction = null)
		{
			NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(shipID);
			if (shipBuildTemplet != null)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				for (int i = 0; i < this.m_lstCostItems.Length; i++)
				{
					if (i + 1 > shipBuildTemplet.BuildMaterialList.Count)
					{
						this.m_lstCostItems[i].SetData(0, 0, 0L, true, true, false);
					}
					else
					{
						BuildMaterial buildMaterial = shipBuildTemplet.BuildMaterialList[i];
						if (nkmuserData != null)
						{
							this.m_lstCostItems[i].SetData(buildMaterial.m_ShipBuildMaterialID, buildMaterial.m_ShipBuildMaterialCount, nkmuserData.m_InventoryData.GetCountMiscItem(buildMaterial.m_ShipBuildMaterialID), true, true, false);
						}
					}
				}
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipID);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_HANGAR_BUILD_CONFIRM_TOP_TEXT, string.Format(NKCUtilString.GET_STRING_HANGAR_CONFIRM, unitTempletBase.GetUnitName()));
			}
			this.dOnTryBuildShip = tryBuildShip;
			this.m_targetBuildShipID = shipID;
			this.dCloseAction = closeAction;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x0027D403 File Offset: 0x0027B603
		private void OnTryBuild()
		{
			if (this.dOnTryBuildShip != null)
			{
				this.dOnTryBuildShip(this.m_targetBuildShipID);
				base.Close();
			}
		}

		// Token: 0x04006470 RID: 25712
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_hangar";

		// Token: 0x04006471 RID: 25713
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_HANGAR_BUILD_CONFIRM";

		// Token: 0x04006472 RID: 25714
		private static NKCUIPopupHangarBuildConfirm m_Instance;

		// Token: 0x04006473 RID: 25715
		[Header("아이템 슬롯 리스트")]
		public NKCUIItemCostSlot[] m_lstCostItems;

		// Token: 0x04006474 RID: 25716
		public Text m_NKM_UI_POPUP_HANGAR_BUILD_CONFIRM_TOP_TEXT;

		// Token: 0x04006475 RID: 25717
		[Header("npc")]
		public GameObject m_AB_NPC_NA_HEE_RIN_TOUCH;

		// Token: 0x04006476 RID: 25718
		private NKCUINPCHangarNaHeeRin m_NKCUINPCHangarNaHeeRin;

		// Token: 0x04006477 RID: 25719
		[Header("버튼")]
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_CANCEL_BOX_OK;

		// Token: 0x04006478 RID: 25720
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_CANCEL_BOX_CANCEL;

		// Token: 0x04006479 RID: 25721
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSEBUTTON;

		// Token: 0x0400647A RID: 25722
		private NKCUIPopupHangarBuildConfirm.OnTryBuildShip dOnTryBuildShip;

		// Token: 0x0400647B RID: 25723
		private int m_targetBuildShipID;

		// Token: 0x0400647C RID: 25724
		private UnityAction dCloseAction;

		// Token: 0x020017EF RID: 6127
		// (Invoke) Token: 0x0600B4A8 RID: 46248
		public delegate void OnTryBuildShip(int id);
	}
}
