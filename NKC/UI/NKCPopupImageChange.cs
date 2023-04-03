using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A63 RID: 2659
	public class NKCPopupImageChange : NKCUIBase
	{
		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x060074F3 RID: 29939 RVA: 0x0026DFCC File Offset: 0x0026C1CC
		public static NKCPopupImageChange Instance
		{
			get
			{
				if (NKCPopupImageChange.m_Instance == null)
				{
					NKCPopupImageChange.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupImageChange>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_IMAGE_CHANGE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupImageChange.CleanupInstance)).GetInstance<NKCPopupImageChange>();
					NKCPopupImageChange.m_Instance.InitUI();
				}
				return NKCPopupImageChange.m_Instance;
			}
		}

		// Token: 0x060074F4 RID: 29940 RVA: 0x0026E01B File Offset: 0x0026C21B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupImageChange.m_Instance != null && NKCPopupImageChange.m_Instance.IsOpen)
			{
				NKCPopupImageChange.m_Instance.Close();
			}
		}

		// Token: 0x060074F5 RID: 29941 RVA: 0x0026E040 File Offset: 0x0026C240
		private static void CleanupInstance()
		{
			NKCPopupImageChange.m_Instance = null;
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x060074F6 RID: 29942 RVA: 0x0026E048 File Offset: 0x0026C248
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x060074F7 RID: 29943 RVA: 0x0026E04B File Offset: 0x0026C24B
		public override string MenuName
		{
			get
			{
				return "ImageChange";
			}
		}

		// Token: 0x060074F8 RID: 29944 RVA: 0x0026E054 File Offset: 0x0026C254
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.dOnGetObject += this.OnGetObject;
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.dOnProvideData += this.OnProvideData;
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.dOnReturnObject += this.OnReturnObject;
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_PROFILE_IMAGE_CHANGE_ScrollView, null);
			if (this.m_NKM_UI_POPUP_OK_BOX_OK != null)
			{
				this.m_NKM_UI_POPUP_OK_BOX_OK.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_POPUP_OK_BOX_OK.PointerClick.AddListener(new UnityAction(this.OK));
				NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_OK_BOX_OK, HotkeyEventType.Confirm);
			}
		}

		// Token: 0x060074F9 RID: 29945 RVA: 0x0026E124 File Offset: 0x0026C324
		private RectTransform OnGetObject(int index)
		{
			if (this.m_slotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_slotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_ParentOfSlots);
			if (newInstance == null)
			{
				return null;
			}
			newInstance.transform.localScale = Vector3.one;
			this.m_slotList.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060074FA RID: 29946 RVA: 0x0026E18C File Offset: 0x0026C38C
		private void OnProvideData(Transform tr, int index)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			if (component == null)
			{
				return;
			}
			component.SetSelected(false);
			switch (this.m_OpenType)
			{
			case NKCPopupImageChange.OPEN_TYPE.UNIT:
				component.SetUnitData(this.m_lstSlotData[index].unitID, 0, this.m_lstSlotData[index].skinID, false, false, true, new NKCUISlot.OnClick(this.OnClickSlot));
				if (this.m_lstSlotData[index].bOnlySkin)
				{
					component.SetShowArrowBGText(true);
					component.SetArrowBGText(NKCStringTable.GetString("SI_DP_PROFILE_REPRESENT_SKIN_ONLY_NO_UNIT", false), NKCUtil.GetColor("#A30000"));
					component.SetDisable(true, "");
					return;
				}
				component.SetShowArrowBGText(false);
				component.SetDisable(false, "");
				return;
			case NKCPopupImageChange.OPEN_TYPE.ITEN:
				if (this.m_lstItemMiscData[index] == null)
				{
					component.SetEmpty(new NKCUISlot.OnClick(this.OnClickSlot));
					return;
				}
				component.SetMiscItemData(this.m_lstItemMiscData[index], false, true, true, new NKCUISlot.OnClick(this.OnClickSlot));
				return;
			case NKCPopupImageChange.OPEN_TYPE.EMOTICON:
				component.SetEmoticonData(NKCUISlot.SlotData.MakeEmoticonData(this.m_lstEmoticon[index], 1), true, false, true, new NKCUISlot.OnClick(this.OnClickSlot));
				return;
			default:
				return;
			}
		}

		// Token: 0x060074FB RID: 29947 RVA: 0x0026E2C5 File Offset: 0x0026C4C5
		private void OnReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			this.m_slotPool.Push(tr.GetComponent<RectTransform>());
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x0026E2EC File Offset: 0x0026C4EC
		public void OpenForUnit(NKCPopupImageChange.OnClickOK _OnClickOK)
		{
			this.m_OpenType = NKCPopupImageChange.OPEN_TYPE.UNIT;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			HashSet<int> hashSet = new HashSet<int>(myUserData.m_ArmyData.m_illustrateUnit);
			IEnumerable<int> skinIds = myUserData.m_InventoryData.SkinIds;
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_IMAGE_CHANGE_TOP_TEXT, NKCUtilString.GET_STRING_FRIEND_CHANGE_IMAGE);
			if (hashSet == null)
			{
				return;
			}
			this.m_GridLayoutGroup.spacing = new Vector2(this.m_GridLayoutGroup.spacing.x, 0f);
			List<NKCPopupImageChange.UnitWithSkin> list = new List<NKCPopupImageChange.UnitWithSkin>();
			if (skinIds != null)
			{
				foreach (int skinID in skinIds)
				{
					NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
					if (skinTemplet != null)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID);
						if (unitTempletBase != null)
						{
							list.Add(new NKCPopupImageChange.UnitWithSkin
							{
								m_NKMSkinTemplet = skinTemplet,
								m_NKMUnitTempletBase = unitTempletBase
							});
						}
					}
				}
			}
			HashSet<int> hashSet2 = new HashSet<int>();
			foreach (int unitID in hashSet)
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase2 != null && unitTempletBase2.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					list.Add(new NKCPopupImageChange.UnitWithSkin
					{
						m_NKMSkinTemplet = null,
						m_NKMUnitTempletBase = unitTempletBase2
					});
					if (!hashSet.Contains(unitTempletBase2.m_BaseUnitID))
					{
						hashSet2.Add(unitTempletBase2.m_BaseUnitID);
					}
				}
			}
			hashSet.UnionWith(hashSet2);
			list.Sort(new NKCPopupImageChange.CompUnitWithSkin());
			if (list.Count <= 0)
			{
				return;
			}
			this.m_lstSlotData.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				int num = 0;
				bool hasNoUnit = false;
				int unitID2 = list[i].m_NKMUnitTempletBase.m_UnitID;
				if (list[i].m_NKMSkinTemplet != null)
				{
					num = list[i].m_NKMSkinTemplet.m_SkinID;
				}
				if (num != 0 && !hashSet.Contains(unitID2))
				{
					hasNoUnit = true;
				}
				if (num != 0 || list[i].m_NKMUnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL || list[i].m_NKMUnitTempletBase.IsTrophy || NKCCollectionManager.GetUnitTemplet(unitID2) != null)
				{
					this.m_lstSlotData.Add(new NKCPopupImageChange.slotData(unitID2, num, hasNoUnit));
				}
			}
			this.m_OnClickOK = _OnClickOK;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_IMAGE_CHANGE_HAVE_NO_TEXT_layoutgruop, false);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.TotalCount = this.m_lstSlotData.Count;
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.SetIndexPosition(0);
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.RefreshCells(true);
			base.UIOpened(true);
		}

		// Token: 0x060074FD RID: 29949 RVA: 0x0026E5B0 File Offset: 0x0026C7B0
		public void OpenForItem(string title, List<NKMItemMiscData> lstNKMItemMiscData, NKCPopupImageChange.OnClickOK _OnClickOK, bool bUseEmpty = true, string emptyNoticeText = "")
		{
			this.m_GridLayoutGroup.spacing = new Vector2(this.m_GridLayoutGroup.spacing.x, 0f);
			List<NKCPopupImageChange.ItemMiscDataAndTemplet> list = new List<NKCPopupImageChange.ItemMiscDataAndTemplet>();
			for (int i = 0; i < lstNKMItemMiscData.Count; i++)
			{
				list.Add(new NKCPopupImageChange.ItemMiscDataAndTemplet
				{
					m_NKMItemMiscData = lstNKMItemMiscData[i],
					m_NKMItemMiscTemplet = NKMItemManager.GetItemMiscTempletByID(lstNKMItemMiscData[i].ItemID)
				});
			}
			list.Sort(new NKCPopupImageChange.CompGradeAndID());
			if (bUseEmpty)
			{
				list.Insert(0, new NKCPopupImageChange.ItemMiscDataAndTemplet
				{
					m_NKMItemMiscData = null,
					m_NKMItemMiscTemplet = null
				});
			}
			this.m_OnClickOK = _OnClickOK;
			this.m_NKM_UI_POPUP_IMAGE_CHANGE_TOP_TEXT.text = title;
			this.m_lstItemMiscData.Clear();
			foreach (NKCPopupImageChange.ItemMiscDataAndTemplet itemMiscDataAndTemplet in list)
			{
				if (itemMiscDataAndTemplet.m_NKMItemMiscData == null)
				{
					this.m_lstItemMiscData.Add(null);
				}
				else
				{
					this.m_lstItemMiscData.Add(itemMiscDataAndTemplet.m_NKMItemMiscData);
				}
			}
			this.m_OpenType = NKCPopupImageChange.OPEN_TYPE.ITEN;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.TotalCount = this.m_lstItemMiscData.Count;
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.SetIndexPosition(0);
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.RefreshCells(true);
			if (emptyNoticeText.Length > 0 && this.m_lstItemMiscData.Count <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_IMAGE_CHANGE_HAVE_NO_TEXT_layoutgruop, true);
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_IMAGE_CHANGE_NO_TEXT, emptyNoticeText);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_IMAGE_CHANGE_HAVE_NO_TEXT_layoutgruop, false);
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060074FE RID: 29950 RVA: 0x0026E76C File Offset: 0x0026C96C
		public void OpenForEmoticon(List<int> lstEmoticonID, NKCPopupImageChange.OnClickOK _OnClickOK)
		{
			this.m_OpenType = NKCPopupImageChange.OPEN_TYPE.EMOTICON;
			this.m_GridLayoutGroup.spacing = new Vector2(this.m_GridLayoutGroup.spacing.x, 40f);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_IMAGE_CHANGE_TOP_TEXT, NKCUtilString.GET_DEV_CONSOLE_CHEAT_EMOTICON_CHEAT);
			this.m_OnClickOK = _OnClickOK;
			this.m_lstEmoticon.Clear();
			this.m_lstEmoticon = lstEmoticonID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_IMAGE_CHANGE_HAVE_NO_TEXT_layoutgruop, false);
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.TotalCount = this.m_lstEmoticon.Count;
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.SetIndexPosition(0);
			this.m_PROFILE_IMAGE_CHANGE_ScrollView.RefreshCells(true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x0026E828 File Offset: 0x0026CA28
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			foreach (NKCUISlot nkcuislot in this.m_slotList)
			{
				if (nkcuislot.GetSlotData() == slotData)
				{
					nkcuislot.SetSelected(true);
				}
				else
				{
					nkcuislot.SetSelected(false);
				}
			}
		}

		// Token: 0x06007500 RID: 29952 RVA: 0x0026E890 File Offset: 0x0026CA90
		public void OK()
		{
			if (this.m_OnClickOK == null)
			{
				return;
			}
			foreach (NKCUISlot nkcuislot in this.m_slotList)
			{
				if (nkcuislot.GetSelected())
				{
					this.m_OnClickOK(nkcuislot);
				}
			}
		}

		// Token: 0x06007501 RID: 29953 RVA: 0x0026E8FC File Offset: 0x0026CAFC
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007502 RID: 29954 RVA: 0x0026E90A File Offset: 0x0026CB0A
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x04006142 RID: 24898
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04006143 RID: 24899
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_IMAGE_CHANGE";

		// Token: 0x04006144 RID: 24900
		private static NKCPopupImageChange m_Instance;

		// Token: 0x04006145 RID: 24901
		public Transform m_ParentOfSlots;

		// Token: 0x04006146 RID: 24902
		public Text m_NKM_UI_POPUP_IMAGE_CHANGE_TOP_TEXT;

		// Token: 0x04006147 RID: 24903
		public GameObject m_NKM_UI_POPUP_IMAGE_CHANGE_HAVE_NO_TEXT_layoutgruop;

		// Token: 0x04006148 RID: 24904
		public Text m_NKM_UI_POPUP_IMAGE_CHANGE_NO_TEXT;

		// Token: 0x04006149 RID: 24905
		public LoopVerticalScrollRect m_PROFILE_IMAGE_CHANGE_ScrollView;

		// Token: 0x0400614A RID: 24906
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x0400614B RID: 24907
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x0400614C RID: 24908
		private NKCPopupImageChange.OnClickOK m_OnClickOK;

		// Token: 0x0400614D RID: 24909
		public NKCUIComButton m_NKM_UI_POPUP_OK_BOX_OK;

		// Token: 0x0400614E RID: 24910
		private List<NKCUISlot> m_slotList = new List<NKCUISlot>();

		// Token: 0x0400614F RID: 24911
		private Stack<RectTransform> m_slotPool = new Stack<RectTransform>();

		// Token: 0x04006150 RID: 24912
		private List<NKCPopupImageChange.slotData> m_lstSlotData = new List<NKCPopupImageChange.slotData>();

		// Token: 0x04006151 RID: 24913
		private NKCPopupImageChange.OPEN_TYPE m_OpenType;

		// Token: 0x04006152 RID: 24914
		private List<NKMItemMiscData> m_lstItemMiscData = new List<NKMItemMiscData>();

		// Token: 0x04006153 RID: 24915
		private List<int> m_lstEmoticon = new List<int>();

		// Token: 0x020017BB RID: 6075
		// (Invoke) Token: 0x0600B417 RID: 46103
		public delegate void OnClickOK(NKCUISlot slot);

		// Token: 0x020017BC RID: 6076
		private struct slotData
		{
			// Token: 0x0600B41A RID: 46106 RVA: 0x00363D88 File Offset: 0x00361F88
			public slotData(int uID, int sID, bool hasNoUnit)
			{
				this.unitID = uID;
				this.skinID = sID;
				this.bOnlySkin = hasNoUnit;
			}

			// Token: 0x0400A76E RID: 42862
			public int unitID;

			// Token: 0x0400A76F RID: 42863
			public int skinID;

			// Token: 0x0400A770 RID: 42864
			public bool bOnlySkin;
		}

		// Token: 0x020017BD RID: 6077
		public class UnitWithSkin
		{
			// Token: 0x0400A771 RID: 42865
			public NKMUnitTempletBase m_NKMUnitTempletBase;

			// Token: 0x0400A772 RID: 42866
			public NKMSkinTemplet m_NKMSkinTemplet;
		}

		// Token: 0x020017BE RID: 6078
		public class CompUnitWithSkin : IComparer<NKCPopupImageChange.UnitWithSkin>
		{
			// Token: 0x0600B41C RID: 46108 RVA: 0x00363DA8 File Offset: 0x00361FA8
			public int Compare(NKCPopupImageChange.UnitWithSkin x, NKCPopupImageChange.UnitWithSkin y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.m_NKMSkinTemplet != null && y.m_NKMSkinTemplet == null)
				{
					return -1;
				}
				if (x.m_NKMSkinTemplet == null && y.m_NKMSkinTemplet != null)
				{
					return 1;
				}
				if (x.m_NKMSkinTemplet != null && y.m_NKMSkinTemplet != null)
				{
					if (x.m_NKMSkinTemplet.m_SkinEquipUnitID < y.m_NKMSkinTemplet.m_SkinEquipUnitID)
					{
						return -1;
					}
					if (x.m_NKMSkinTemplet.m_SkinEquipUnitID > y.m_NKMSkinTemplet.m_SkinEquipUnitID)
					{
						return 1;
					}
					if (x.m_NKMSkinTemplet.m_SkinGrade > y.m_NKMSkinTemplet.m_SkinGrade)
					{
						return -1;
					}
					if (x.m_NKMSkinTemplet.m_SkinGrade < y.m_NKMSkinTemplet.m_SkinGrade)
					{
						return 1;
					}
					x.m_NKMSkinTemplet.m_SkinID.CompareTo(y.m_NKMSkinTemplet.m_SkinID);
				}
				return x.m_NKMUnitTempletBase.m_UnitID.CompareTo(y.m_NKMUnitTempletBase.m_UnitID);
			}
		}

		// Token: 0x020017BF RID: 6079
		private class CompGradeAndID : IComparer<NKCPopupImageChange.ItemMiscDataAndTemplet>
		{
			// Token: 0x0600B41E RID: 46110 RVA: 0x00363EA0 File Offset: 0x003620A0
			public int Compare(NKCPopupImageChange.ItemMiscDataAndTemplet x, NKCPopupImageChange.ItemMiscDataAndTemplet y)
			{
				if (x == null || x.m_NKMItemMiscTemplet == null || x.m_NKMItemMiscData == null)
				{
					return 1;
				}
				if (y == null || y.m_NKMItemMiscTemplet == null || y.m_NKMItemMiscData == null)
				{
					return -1;
				}
				if (x.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE > y.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE)
				{
					return -1;
				}
				if (x.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE < y.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE)
				{
					return 1;
				}
				return x.m_NKMItemMiscTemplet.m_ItemMiscID.CompareTo(y.m_NKMItemMiscTemplet.m_ItemMiscID);
			}
		}

		// Token: 0x020017C0 RID: 6080
		private class ItemMiscDataAndTemplet
		{
			// Token: 0x0400A773 RID: 42867
			public NKMItemMiscData m_NKMItemMiscData;

			// Token: 0x0400A774 RID: 42868
			public NKMItemMiscTemplet m_NKMItemMiscTemplet;
		}

		// Token: 0x020017C1 RID: 6081
		private enum OPEN_TYPE
		{
			// Token: 0x0400A776 RID: 42870
			NONE,
			// Token: 0x0400A777 RID: 42871
			UNIT,
			// Token: 0x0400A778 RID: 42872
			ITEN,
			// Token: 0x0400A779 RID: 42873
			EMOTICON
		}
	}
}
