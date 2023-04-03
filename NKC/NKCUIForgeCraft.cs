using System;
using System.Collections.Generic;
using ClientPacket.Item;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000991 RID: 2449
	public class NKCUIForgeCraft : NKCUIBase
	{
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x00204164 File Offset: 0x00202364
		public static NKCUIForgeCraft Instance
		{
			get
			{
				if (NKCUIForgeCraft.m_Instance == null)
				{
					NKCUIForgeCraft.m_Instance = NKCUIManager.OpenNewInstance<NKCUIForgeCraft>("ab_ui_nkm_ui_factory", "NKM_UI_FACTORY_CRAFT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIForgeCraft.CleanupInstance)).GetInstance<NKCUIForgeCraft>();
					NKCUIForgeCraft.m_Instance.InitUI();
				}
				return NKCUIForgeCraft.m_Instance;
			}
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x002041B3 File Offset: 0x002023B3
		private static void CleanupInstance()
		{
			NKCUIForgeCraft.m_Instance = null;
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x0600656E RID: 25966 RVA: 0x002041BB File Offset: 0x002023BB
		public static bool HasInstance
		{
			get
			{
				return NKCUIForgeCraft.m_Instance != null;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x002041C8 File Offset: 0x002023C8
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIForgeCraft.m_Instance != null && NKCUIForgeCraft.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x002041E3 File Offset: 0x002023E3
		public static void CheckInstanceAndClose()
		{
			if (NKCUIForgeCraft.m_Instance != null && NKCUIForgeCraft.m_Instance.IsOpen)
			{
				NKCUIForgeCraft.m_Instance.Close();
			}
		}

		// Token: 0x06006571 RID: 25969 RVA: 0x00204208 File Offset: 0x00202408
		private void OnDestroy()
		{
			NKCUIForgeCraft.m_Instance = null;
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x00204210 File Offset: 0x00202410
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FORGE_CRAFT;
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06006573 RID: 25971 RVA: 0x00204217 File Offset: 0x00202417
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x0020421F File Offset: 0x0020241F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x00204222 File Offset: 0x00202422
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_EQUIP_MAKE";
			}
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x0020422C File Offset: 0x0020242C
		public void InitUI()
		{
			base.gameObject.SetActive(false);
			for (int i = 0; i < this.m_lstNKCUIForgeCraftSlot.Count; i++)
			{
				this.m_lstNKCUIForgeCraftSlot[i].Init(i + 1, new NKCUIForgeCraftSlot.OnClickSelect(this.OnClickSlotBySelectBtn), new NKCUIForgeCraftSlot.OnClickSelect(this.OnClickSlotByGetBtn), new NKCUIForgeCraftSlot.OnClickSelect(this.OnClickSlotByInstanceGetBtn));
			}
			NKCUtil.SetScrollHotKey(this.m_srForgeCraftSlot, null);
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x002042A0 File Offset: 0x002024A0
		private void Update()
		{
			if (base.IsOpen && this.m_fRefreshTime < Time.time)
			{
				this.m_fRefreshTime = Time.time + 0.5f;
				for (int i = 0; i < this.m_lstNKCUIForgeCraftSlot.Count; i++)
				{
					this.m_lstNKCUIForgeCraftSlot[i].ResetUI(false);
				}
			}
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x002042FB File Offset: 0x002024FB
		public void OnClickSlotBySelectBtn(int index)
		{
			NKCUIForgeCraftMold.Instance.Open(index);
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x00204308 File Offset: 0x00202508
		public void OnClickSlotByGetBtn(int index)
		{
			NKMPacket_CRAFT_COMPLETE_REQ nkmpacket_CRAFT_COMPLETE_REQ = new NKMPacket_CRAFT_COMPLETE_REQ();
			nkmpacket_CRAFT_COMPLETE_REQ.index = (byte)index;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CRAFT_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x00204338 File Offset: 0x00202538
		private void SendInstantCompletePacket()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetCountMiscItem(1012) < 1L)
			{
				NKCShopManager.OpenItemLackPopup(1012, 1);
				return;
			}
			NKMPacket_CRAFT_INSTANT_COMPLETE_REQ nkmpacket_CRAFT_INSTANT_COMPLETE_REQ = new NKMPacket_CRAFT_INSTANT_COMPLETE_REQ();
			nkmpacket_CRAFT_INSTANT_COMPLETE_REQ.index = (byte)this.m_ReservedInstanceGetIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CRAFT_INSTANT_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x00204394 File Offset: 0x00202594
		public void OnClickSlotByInstanceGetBtn(int index)
		{
			this.m_ReservedInstanceGetIndex = index;
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(1012);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_FORGE_CRAFT_USE_MISC_ONE_PARAM, itemMiscTempletByID.GetItemName()), itemMiscTempletByID.m_ItemMiscID, 1, new NKCPopupResourceConfirmBox.OnButton(this.SendInstantCompletePacket), null, false);
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x002043EB File Offset: 0x002025EB
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.AddNPC();
			this.ResetUI();
			base.UIOpened(true);
			this.TutorialCheck();
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x00204414 File Offset: 0x00202614
		public void ResetUI()
		{
			for (int i = 0; i < this.m_lstNKCUIForgeCraftSlot.Count; i++)
			{
				this.m_lstNKCUIForgeCraftSlot[i].ResetUI(true);
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(1012);
			if (itemMiscTempletByID != null)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				this.m_NKM_UI_FACTORY_CRAFT_INSTANT_TEXT.text = itemMiscTempletByID.GetItemName();
				NKMItemMiscData itemMisc = myUserData.m_InventoryData.GetItemMisc(1012);
				if (itemMisc != null)
				{
					Text nkm_UI_FACTORY_CRAFT_INSTANT_TEXT = this.m_NKM_UI_FACTORY_CRAFT_INSTANT_TEXT;
					nkm_UI_FACTORY_CRAFT_INSTANT_TEXT.text = nkm_UI_FACTORY_CRAFT_INSTANT_TEXT.text + " :  " + itemMisc.TotalCount.ToString();
				}
				else
				{
					Text nkm_UI_FACTORY_CRAFT_INSTANT_TEXT2 = this.m_NKM_UI_FACTORY_CRAFT_INSTANT_TEXT;
					nkm_UI_FACTORY_CRAFT_INSTANT_TEXT2.text += " : 0";
				}
			}
			else
			{
				this.m_NKM_UI_FACTORY_CRAFT_INSTANT_TEXT.text = NKCUtilString.GET_STRING_FORGE_CRAFT_ITEM_NO_FOUND;
			}
			this.m_NKM_UI_FACTORY_SHORTCUT_MENU.SetData(ContentsType.FACTORY_CRAFT);
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x002044EC File Offset: 0x002026EC
		public void OnRecvSlotOpen(int unlockedSlotNum)
		{
			if (unlockedSlotNum - 1 < this.m_lstNKCUIForgeCraftSlot.Count)
			{
				NKCUIForgeCraftSlot nkcuiforgeCraftSlot = this.m_lstNKCUIForgeCraftSlot[unlockedSlotNum - 1];
				if (this.m_objUnlockEffect != null && nkcuiforgeCraftSlot != null)
				{
					Transform transform = nkcuiforgeCraftSlot.transform;
					this.m_objUnlockEffect.transform.SetParent(transform);
					this.m_objUnlockEffect.transform.localPosition = Vector3.zero;
					this.m_objUnlockEffect.transform.localScale = Vector3.one;
					NKCUtil.SetGameobjectActive(this.m_objUnlockEffect, false);
					NKCUtil.SetGameobjectActive(this.m_objUnlockEffect, true);
					NKCSoundManager.PlaySound("FX_UI_CONTRACT_SLOT_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
				}
			}
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x002045B8 File Offset: 0x002027B8
		public override void CloseInternal()
		{
			this.m_objUnlockEffect.transform.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(this.m_objUnlockEffect, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.RemoveNPC();
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x002045EE File Offset: 0x002027EE
		public override void Hide()
		{
			this.m_objUnlockEffect.transform.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(this.m_objUnlockEffect, false);
			base.Hide();
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x00204618 File Offset: 0x00202818
		public override void UnHide()
		{
			base.UnHide();
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x00204620 File Offset: 0x00202820
		private void AddNPC()
		{
			if (this.m_UINPC_Factory == null)
			{
				this.m_UINPC_Factory = this.m_objNPCFactory_TouchArea.GetComponent<NKCUINPCFactoryAnastasia>();
				this.m_UINPC_Factory.Init(true);
				return;
			}
			this.m_UINPC_Factory.PlayAni(NPC_ACTION_TYPE.START, false);
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x0020465B File Offset: 0x0020285B
		private void RemoveNPC()
		{
			if (this.m_UINPC_Factory != null)
			{
				this.m_UINPC_Factory = null;
			}
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x00204672 File Offset: 0x00202872
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.FactoryCraft, true);
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x00204680 File Offset: 0x00202880
		public NKCUIForgeCraftSlot GetSlot(int index)
		{
			if (index < 1 || index > NKMCraftData.MAX_CRAFT_SLOT_DATA)
			{
				return null;
			}
			return this.m_lstNKCUIForgeCraftSlot.Find((NKCUIForgeCraftSlot v) => v.GetIndex() == index);
		}

		// Token: 0x040050F1 RID: 20721
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_factory";

		// Token: 0x040050F2 RID: 20722
		private const string UI_ASSET_NAME = "NKM_UI_FACTORY_CRAFT";

		// Token: 0x040050F3 RID: 20723
		private static NKCUIForgeCraft m_Instance;

		// Token: 0x040050F4 RID: 20724
		private readonly List<int> RESOURCE_LIST = new List<int>
		{
			1012,
			1,
			2,
			101
		};

		// Token: 0x040050F5 RID: 20725
		public GameObject m_objUnlockEffect;

		// Token: 0x040050F6 RID: 20726
		public GameObject m_objBackGround;

		// Token: 0x040050F7 RID: 20727
		public ScrollRect m_srForgeCraftSlot;

		// Token: 0x040050F8 RID: 20728
		public List<NKCUIForgeCraftSlot> m_lstNKCUIForgeCraftSlot;

		// Token: 0x040050F9 RID: 20729
		public Text m_NKM_UI_FACTORY_CRAFT_INSTANT_TEXT;

		// Token: 0x040050FA RID: 20730
		private float m_fRefreshTime;

		// Token: 0x040050FB RID: 20731
		private int m_ReservedInstanceGetIndex = -1;

		// Token: 0x040050FC RID: 20732
		public GameObject NKM_UI_FACTORY_CRAFT_NPC;

		// Token: 0x040050FD RID: 20733
		public NKCUIFactoryShortCutMenu m_NKM_UI_FACTORY_SHORTCUT_MENU;

		// Token: 0x040050FE RID: 20734
		[Header("npc")]
		private NKCUINPCFactoryAnastasia m_UINPC_Factory;

		// Token: 0x040050FF RID: 20735
		public GameObject m_objNPCFactory_TouchArea;
	}
}
