using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A62 RID: 2658
	public class NKCPopupHiddenOptionPopup : NKCUIBase
	{
		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x0026DAEC File Offset: 0x0026BCEC
		public static NKCPopupHiddenOptionPopup Instance
		{
			get
			{
				if (NKCPopupHiddenOptionPopup.m_Instance == null)
				{
					NKCPopupHiddenOptionPopup.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupHiddenOptionPopup>("AB_UI_NKM_UI_FACTORY", "NKM_UI_FACTORY_POPUP_HIDDEN_OPTION", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupHiddenOptionPopup.CleanupInstance)).GetInstance<NKCPopupHiddenOptionPopup>();
					NKCPopupHiddenOptionPopup.m_Instance.InitUI();
				}
				return NKCPopupHiddenOptionPopup.m_Instance;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x060074E6 RID: 29926 RVA: 0x0026DB3B File Offset: 0x0026BD3B
		public static bool HasInstance
		{
			get
			{
				return NKCPopupHiddenOptionPopup.m_Instance != null;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x060074E7 RID: 29927 RVA: 0x0026DB48 File Offset: 0x0026BD48
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupHiddenOptionPopup.m_Instance != null && NKCPopupHiddenOptionPopup.m_Instance.IsOpen;
			}
		}

		// Token: 0x060074E8 RID: 29928 RVA: 0x0026DB63 File Offset: 0x0026BD63
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupHiddenOptionPopup.m_Instance != null && NKCPopupHiddenOptionPopup.m_Instance.IsOpen)
			{
				NKCPopupHiddenOptionPopup.m_Instance.Close();
			}
		}

		// Token: 0x060074E9 RID: 29929 RVA: 0x0026DB88 File Offset: 0x0026BD88
		private static void CleanupInstance()
		{
			NKCPopupHiddenOptionPopup instance = NKCPopupHiddenOptionPopup.m_Instance;
			if (instance != null)
			{
				instance.Release();
			}
			NKCPopupHiddenOptionPopup.m_Instance = null;
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x060074EA RID: 29930 RVA: 0x0026DBA0 File Offset: 0x0026BDA0
		public override string MenuName
		{
			get
			{
				return "Hidden Option";
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x060074EB RID: 29931 RVA: 0x0026DBA7 File Offset: 0x0026BDA7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x060074EC RID: 29932 RVA: 0x0026DBAC File Offset: 0x0026BDAC
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupHiddenOptionPopup.CheckInstanceAndClose();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
		}

		// Token: 0x060074ED RID: 29933 RVA: 0x0026DC3D File Offset: 0x0026BE3D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060074EE RID: 29934 RVA: 0x0026DC4C File Offset: 0x0026BE4C
		public void Open(int potentialOpGroupID)
		{
			NKMPotentialOptionGroupTemplet nkmpotentialOptionGroupTemplet = NKMTempletContainer<NKMPotentialOptionGroupTemplet>.Find(potentialOpGroupID);
			if (nkmpotentialOptionGroupTemplet == null)
			{
				return;
			}
			int num = nkmpotentialOptionGroupTemplet.OptionList.Count<NKMPotentialOptionTemplet>();
			this.m_lstOptionSlot.Clear();
			for (int i = 0; i < num; i++)
			{
				this.m_lstOptionSlot.Add(new List<NKCPopupEquipOptionListSlot>());
			}
			this.InstantiatOptionSlotPrefab(this.m_contentSocket1, num);
			this.InstantiatOptionSlotPrefab(this.m_contentSocket2, num);
			this.InstantiatOptionSlotPrefab(this.m_contentSocket3, num);
			int num2 = 0;
			foreach (NKMPotentialOptionTemplet nkmpotentialOptionTemplet in nkmpotentialOptionGroupTemplet.OptionList)
			{
				if (nkmpotentialOptionTemplet == null)
				{
					int count = this.m_lstOptionSlot[num2].Count;
					for (int j = 0; j < count; j++)
					{
						NKCUtil.SetLabelText(this.m_lstOptionSlot[num2][j].m_OPTION_NAME, "");
						NKCUtil.SetGameobjectActive(this.m_lstOptionSlot[num2][j], false);
					}
				}
				else
				{
					int num3 = Mathf.Min(nkmpotentialOptionTemplet.sockets.Length, this.m_lstOptionSlot[num2].Count);
					bool isPercentStat = NKMUnitStatManager.IsPercentStat(nkmpotentialOptionTemplet.StatType);
					for (int k = 0; k < num3; k++)
					{
						NKMPotentialSocketTemplet nkmpotentialSocketTemplet = nkmpotentialOptionTemplet.sockets[k];
						if (nkmpotentialSocketTemplet == null || (nkmpotentialSocketTemplet.MinStat == 0f && nkmpotentialSocketTemplet.MaxStat == 0f))
						{
							NKCUtil.SetLabelText(this.m_lstOptionSlot[num2][k].m_OPTION_NAME, "");
							NKCUtil.SetGameobjectActive(this.m_lstOptionSlot[num2][k], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_lstOptionSlot[num2][k], true);
							bool bPercent = this.IsPercentStatValue(isPercentStat, nkmpotentialSocketTemplet.ApplyType);
							string statShortNameForInvenEquip = NKCUtilString.GetStatShortNameForInvenEquip(nkmpotentialOptionTemplet.StatType, nkmpotentialSocketTemplet.MinStat, nkmpotentialSocketTemplet.MaxStat, bPercent);
							NKCUtil.SetLabelText(this.m_lstOptionSlot[num2][k].m_OPTION_NAME, statShortNameForInvenEquip);
						}
					}
					num2++;
				}
			}
			if (this.m_scrollView1 != null)
			{
				this.m_scrollView1.verticalNormalizedPosition = 1f;
			}
			if (this.m_scrollView2 != null)
			{
				this.m_scrollView2.verticalNormalizedPosition = 1f;
			}
			if (this.m_scrollView3 != null)
			{
				this.m_scrollView3.verticalNormalizedPosition = 1f;
			}
			base.UIOpened(true);
		}

		// Token: 0x060074EF RID: 29935 RVA: 0x0026DEF4 File Offset: 0x0026C0F4
		private void InstantiatOptionSlotPrefab(Transform scrollContent, int optionCount)
		{
			if (scrollContent == null)
			{
				return;
			}
			int childCount = scrollContent.childCount;
			if (childCount < optionCount)
			{
				for (int i = 0; i < optionCount - childCount; i++)
				{
					UnityEngine.Object.Instantiate<NKCPopupEquipOptionListSlot>(this.m_prfNKCPopupEquipOptionListSlot, scrollContent);
				}
			}
			childCount = scrollContent.childCount;
			for (int j = 0; j < childCount; j++)
			{
				if (j < optionCount)
				{
					this.m_lstOptionSlot[j].Add(scrollContent.GetChild(j).GetComponent<NKCPopupEquipOptionListSlot>());
					scrollContent.GetChild(j).gameObject.SetActive(true);
				}
				else
				{
					scrollContent.GetChild(j).gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x060074F0 RID: 29936 RVA: 0x0026DF8B File Offset: 0x0026C18B
		private bool IsPercentStatValue(bool isPercentStat, StatApplyType statApplyType)
		{
			return (isPercentStat && statApplyType == StatApplyType.Addable) || (!isPercentStat && statApplyType == StatApplyType.Multipliable);
		}

		// Token: 0x060074F1 RID: 29937 RVA: 0x0026DF9E File Offset: 0x0026C19E
		private void Release()
		{
			List<List<NKCPopupEquipOptionListSlot>> lstOptionSlot = this.m_lstOptionSlot;
			if (lstOptionSlot != null)
			{
				lstOptionSlot.Clear();
			}
			this.m_lstOptionSlot = null;
		}

		// Token: 0x04006134 RID: 24884
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FACTORY";

		// Token: 0x04006135 RID: 24885
		private const string UI_ASSET_NAME = "NKM_UI_FACTORY_POPUP_HIDDEN_OPTION";

		// Token: 0x04006136 RID: 24886
		private static NKCPopupHiddenOptionPopup m_Instance;

		// Token: 0x04006137 RID: 24887
		public ScrollRect m_scrollView1;

		// Token: 0x04006138 RID: 24888
		public ScrollRect m_scrollView2;

		// Token: 0x04006139 RID: 24889
		public ScrollRect m_scrollView3;

		// Token: 0x0400613A RID: 24890
		public Transform m_contentSocket1;

		// Token: 0x0400613B RID: 24891
		public Transform m_contentSocket2;

		// Token: 0x0400613C RID: 24892
		public Transform m_contentSocket3;

		// Token: 0x0400613D RID: 24893
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x0400613E RID: 24894
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400613F RID: 24895
		public EventTrigger m_eventTriggerBg;

		// Token: 0x04006140 RID: 24896
		public NKCPopupEquipOptionListSlot m_prfNKCPopupEquipOptionListSlot;

		// Token: 0x04006141 RID: 24897
		private List<List<NKCPopupEquipOptionListSlot>> m_lstOptionSlot = new List<List<NKCPopupEquipOptionListSlot>>();
	}
}
