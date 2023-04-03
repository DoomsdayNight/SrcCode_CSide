using System;
using System.Collections.Generic;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A6C RID: 2668
	public class NKCPopupMergeConfirmBox : NKCUIBase
	{
		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x060075AD RID: 30125 RVA: 0x00272598 File Offset: 0x00270798
		public static NKCPopupMergeConfirmBox Instance
		{
			get
			{
				if (NKCPopupMergeConfirmBox.m_Instance == null)
				{
					NKCPopupMergeConfirmBox.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupMergeConfirmBox>("ab_ui_eventmerge", "EVENTMERGE_POPUP_MERGE_RESULT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupMergeConfirmBox.CleanupInstance)).GetInstance<NKCPopupMergeConfirmBox>();
					NKCPopupMergeConfirmBox instance = NKCPopupMergeConfirmBox.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupMergeConfirmBox.m_Instance;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x060075AE RID: 30126 RVA: 0x002725ED File Offset: 0x002707ED
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupMergeConfirmBox.m_Instance != null && NKCPopupMergeConfirmBox.m_Instance.IsOpen;
			}
		}

		// Token: 0x060075AF RID: 30127 RVA: 0x00272608 File Offset: 0x00270808
		private static void CleanupInstance()
		{
			NKCPopupMergeConfirmBox.m_Instance = null;
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x060075B0 RID: 30128 RVA: 0x00272610 File Offset: 0x00270810
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x060075B1 RID: 30129 RVA: 0x00272613 File Offset: 0x00270813
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x060075B2 RID: 30130 RVA: 0x0027261A File Offset: 0x0027081A
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return base.eUpsideMenuMode;
			}
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x00272624 File Offset: 0x00270824
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetBindFunction(this.m_csbtnCancel, new UnityAction(this.OnClickCancel));
			NKCUtil.SetBindFunction(this.m_csbtnResult, new UnityAction(this.OnClickResult));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x00272684 File Offset: 0x00270884
		public void Open(int mergeID, int groupID, List<long> lstConsumeUids, Action click)
		{
			this.m_mergeID = mergeID;
			this.m_groupID = groupID;
			this.m_TargetUids = lstConsumeUids;
			this.m_OnClick = click;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			for (int i = 0; i < this.m_lstUnitSlot.Count; i++)
			{
				if (i < this.m_TargetUids.Count)
				{
					NKMUnitData trophyFromUID = armyData.GetTrophyFromUID(this.m_TargetUids[i]);
					if (trophyFromUID != null)
					{
						NKMUnitManager.GetUnitTempletBase(trophyFromUID);
						this.m_lstUnitSlot[i].SetData(NKCUISlot.SlotData.MakeUnitData(trophyFromUID), true, null);
						NKCUtil.SetGameobjectActive(this.m_lstUnitSlot[i], true);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstUnitSlot[i], false);
				}
			}
			this.UpdateResultIcon();
			base.UIOpened(true);
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x00272746 File Offset: 0x00270946
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060075B6 RID: 30134 RVA: 0x00272754 File Offset: 0x00270954
		private void UpdateResultIcon()
		{
			NKMEventCollectionMergeTemplet nkmeventCollectionMergeTemplet = NKMTempletContainer<NKMEventCollectionMergeTemplet>.Find(this.m_mergeID);
			if (nkmeventCollectionMergeTemplet == null)
			{
				return;
			}
			NKMEventCollectionMergeRecipeTemplet recipeTemplet = nkmeventCollectionMergeTemplet.GetRecipeTemplet(this.m_groupID);
			if (recipeTemplet == null)
			{
				return;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(recipeTemplet.MergeOutputBundleName, recipeTemplet.MergeOutputAssetName, false);
			NKCUtil.SetImageSprite(this.m_imgResult, orLoadAssetResource, false);
		}

		// Token: 0x060075B7 RID: 30135 RVA: 0x002727A4 File Offset: 0x002709A4
		private void OnClickResult()
		{
			NKMEventCollectionMergeTemplet nkmeventCollectionMergeTemplet = NKMTempletContainer<NKMEventCollectionMergeTemplet>.Find(this.m_mergeID);
			if (nkmeventCollectionMergeTemplet == null)
			{
				return;
			}
			NKMEventCollectionMergeRecipeTemplet recipeTemplet = nkmeventCollectionMergeTemplet.GetRecipeTemplet(this.m_groupID);
			if (recipeTemplet == null)
			{
				return;
			}
			List<int> list = new List<int>();
			foreach (NKMEventCollectionTemplet nkmeventCollectionTemplet in NKMTempletContainer<NKMEventCollectionTemplet>.Values)
			{
				if (nkmeventCollectionMergeTemplet.Key == nkmeventCollectionTemplet.CollectionMergeId)
				{
					foreach (NKMEventCollectionDetailTemplet nkmeventCollectionDetailTemplet in nkmeventCollectionTemplet.Details)
					{
						if (recipeTemplet.MergeOutputGradeGroupId == nkmeventCollectionDetailTemplet.CollectionGradeGroupId)
						{
							list.Add(nkmeventCollectionDetailTemplet.Key);
						}
					}
				}
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list, NKM_REWARD_TYPE.RT_UNIT, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x00272890 File Offset: 0x00270A90
		private void OnClickOK()
		{
			Action onClick = this.m_OnClick;
			if (onClick == null)
			{
				return;
			}
			onClick();
		}

		// Token: 0x060075B9 RID: 30137 RVA: 0x002728A2 File Offset: 0x00270AA2
		private void OnClickCancel()
		{
			base.Close();
		}

		// Token: 0x04006215 RID: 25109
		private const string ASSET_BUNDLE_NAME = "ab_ui_eventmerge";

		// Token: 0x04006216 RID: 25110
		private const string UI_ASSET_NAME = "EVENTMERGE_POPUP_MERGE_RESULT";

		// Token: 0x04006217 RID: 25111
		private static NKCPopupMergeConfirmBox m_Instance;

		// Token: 0x04006218 RID: 25112
		public List<NKCUISlot> m_lstUnitSlot;

		// Token: 0x04006219 RID: 25113
		public Image m_imgResult;

		// Token: 0x0400621A RID: 25114
		public NKCUIComStateButton m_csbtnResult;

		// Token: 0x0400621B RID: 25115
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x0400621C RID: 25116
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x0400621D RID: 25117
		private List<long> m_TargetUids = new List<long>();

		// Token: 0x0400621E RID: 25118
		private int m_groupID;

		// Token: 0x0400621F RID: 25119
		private int m_mergeID;

		// Token: 0x04006220 RID: 25120
		private Action m_OnClick;
	}
}
