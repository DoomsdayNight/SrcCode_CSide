using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A01 RID: 2561
	public class NKCPopupOperationSubStoryList : NKCUIBase
	{
		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x0024F064 File Offset: 0x0024D264
		public static NKCPopupOperationSubStoryList Instance
		{
			get
			{
				if (NKCPopupOperationSubStoryList.m_Instance == null)
				{
					NKCPopupOperationSubStoryList.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupOperationSubStoryList>("AB_UI_OPERATION", "AB_UI_POPUP_OPERATION_SUB_SHORTCUT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupOperationSubStoryList.CleanupInstance)).GetInstance<NKCPopupOperationSubStoryList>();
					NKCPopupOperationSubStoryList.m_Instance.Initialize();
				}
				return NKCPopupOperationSubStoryList.m_Instance;
			}
		}

		// Token: 0x06006FB8 RID: 28600 RVA: 0x0024F0B3 File Offset: 0x0024D2B3
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupOperationSubStoryList.m_Instance != null && NKCPopupOperationSubStoryList.m_Instance.IsOpen)
			{
				NKCPopupOperationSubStoryList.m_Instance.Close();
			}
		}

		// Token: 0x06006FB9 RID: 28601 RVA: 0x0024F0D8 File Offset: 0x0024D2D8
		private static void CleanupInstance()
		{
			NKCPopupOperationSubStoryList.m_Instance = null;
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x0024F0E0 File Offset: 0x0024D2E0
		public static bool isOpen()
		{
			return NKCPopupOperationSubStoryList.m_Instance != null && NKCPopupOperationSubStoryList.m_Instance.IsOpen;
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06006FBB RID: 28603 RVA: 0x0024F0FB File Offset: 0x0024D2FB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06006FBC RID: 28604 RVA: 0x0024F0FE File Offset: 0x0024D2FE
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x0024F105 File Offset: 0x0024D305
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x0024F114 File Offset: 0x0024D314
		public override void Initialize()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_tglSort.OnValueChanged.RemoveAllListeners();
			this.m_tglSort.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTgl));
		}

		// Token: 0x06006FBF RID: 28607 RVA: 0x0024F1E4 File Offset: 0x0024D3E4
		private RectTransform GetObject(int idx)
		{
			NKCUIOperationSubStorySlot nkcuioperationSubStorySlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuioperationSubStorySlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuioperationSubStorySlot = UnityEngine.Object.Instantiate<NKCUIOperationSubStorySlot>(this.m_pfbSlot, this.m_loop.content);
				nkcuioperationSubStorySlot.InitUI(new NKCUIOperationSubStorySlot.OnSelectSlot(this.OnClickSlot));
			}
			return nkcuioperationSubStorySlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x0024F240 File Offset: 0x0024D440
		private void ReturnObject(Transform tr)
		{
			NKCUIOperationSubStorySlot component = tr.GetComponent<NKCUIOperationSubStorySlot>();
			NKCUtil.SetGameobjectActive(component, false);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x0024F267 File Offset: 0x0024D467
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIOperationSubStorySlot component = tr.GetComponent<NKCUIOperationSubStorySlot>();
			NKCUtil.SetGameobjectActive(component, true);
			component.SetEpisodeID(this.m_dicData[this.m_bShowSupplement][idx].m_EpisodeID);
			component.SetData(!this.m_bShowSupplement);
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x0024F2A8 File Offset: 0x0024D4A8
		public void Open(NKCPopupOperationSubStoryList.OnSelectedSlot onSelected, bool bSupplement)
		{
			this.m_dOnSelectedSlot = onSelected;
			if (!NKCPopupOperationSubStoryList.isOpen())
			{
				this.m_tglSort.Select(true, true, false);
				this.BuildData();
				this.SetData(bSupplement);
				base.UIOpened(true);
				return;
			}
			if (this.m_bShowSupplement == bSupplement)
			{
				base.Close();
				return;
			}
			this.SetData(bSupplement);
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x0024F300 File Offset: 0x0024D500
		public void SetData(bool bSupplement)
		{
			this.m_bShowSupplement = bSupplement;
			NKCUtil.SetLabelText(this.m_Title, string.Format(NKCUtilString.GET_STRING_OPERATION_SUBSTREAM_SHORTCUT_TITLE, this.m_bShowSupplement ? NKCUtilString.GET_STRING_EPISODE_SUPPLEMENT : NKCUtilString.GET_STRING_EPISODE_CATEGORY_EC_SIDESTORY));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.TotalCount = this.m_dicData[this.m_bShowSupplement].Count;
			this.m_loop.SetIndexPosition(0);
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x0024F378 File Offset: 0x0024D578
		private void BuildData()
		{
			this.m_dicData.Clear();
			this.m_dicData.Add(true, new List<NKMEpisodeTempletV2>());
			this.m_dicData.Add(false, new List<NKMEpisodeTempletV2>());
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY.EC_SIDESTORY, false, EPISODE_DIFFICULTY.NORMAL);
			for (int i = 0; i < listNKMEpisodeTempletByCategory.Count; i++)
			{
				this.m_dicData[listNKMEpisodeTempletByCategory[i].m_bIsSupplement].Add(listNKMEpisodeTempletByCategory[i]);
			}
			this.m_dicData[true].Sort(new Comparison<NKMEpisodeTempletV2>(this.SortBySortIndex));
			this.m_dicData[false].Sort(new Comparison<NKMEpisodeTempletV2>(this.SortBySortIndex));
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x0024F429 File Offset: 0x0024D629
		private int SortBySortIndex(NKMEpisodeTempletV2 lItem, NKMEpisodeTempletV2 rItem)
		{
			return lItem.m_SortIndex.CompareTo(rItem.m_SortIndex);
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x0024F43C File Offset: 0x0024D63C
		private int SortBySortIndexRevert(NKMEpisodeTempletV2 lItem, NKMEpisodeTempletV2 rItem)
		{
			return rItem.m_SortIndex.CompareTo(lItem.m_SortIndex);
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x0024F44F File Offset: 0x0024D64F
		private void OnClickSlot(int episodeID)
		{
			this.m_dOnSelectedSlot(episodeID);
		}

		// Token: 0x06006FC8 RID: 28616 RVA: 0x0024F460 File Offset: 0x0024D660
		private void OnTgl(bool bValue)
		{
			this.m_tglSort.Select(bValue, true, false);
			if (bValue)
			{
				this.m_dicData[true].Sort(new Comparison<NKMEpisodeTempletV2>(this.SortBySortIndex));
				this.m_dicData[false].Sort(new Comparison<NKMEpisodeTempletV2>(this.SortBySortIndex));
			}
			else
			{
				this.m_dicData[true].Sort(new Comparison<NKMEpisodeTempletV2>(this.SortBySortIndexRevert));
				this.m_dicData[false].Sort(new Comparison<NKMEpisodeTempletV2>(this.SortBySortIndexRevert));
			}
			this.SetData(this.m_bShowSupplement);
		}

		// Token: 0x04005B59 RID: 23385
		private const string ASSET_BUNDLE_NAME = "AB_UI_OPERATION";

		// Token: 0x04005B5A RID: 23386
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OPERATION_SUB_SHORTCUT";

		// Token: 0x04005B5B RID: 23387
		private static NKCPopupOperationSubStoryList m_Instance;

		// Token: 0x04005B5C RID: 23388
		public TMP_Text m_Title;

		// Token: 0x04005B5D RID: 23389
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04005B5E RID: 23390
		[Space]
		public NKCUIComToggle m_tglSort;

		// Token: 0x04005B5F RID: 23391
		public NKCUIOperationSubStorySlot m_pfbSlot;

		// Token: 0x04005B60 RID: 23392
		public LoopScrollRect m_loop;

		// Token: 0x04005B61 RID: 23393
		private Stack<NKCUIOperationSubStorySlot> m_stkSlot = new Stack<NKCUIOperationSubStorySlot>();

		// Token: 0x04005B62 RID: 23394
		private Dictionary<bool, List<NKMEpisodeTempletV2>> m_dicData = new Dictionary<bool, List<NKMEpisodeTempletV2>>();

		// Token: 0x04005B63 RID: 23395
		private bool m_bShowSupplement;

		// Token: 0x04005B64 RID: 23396
		private NKCPopupOperationSubStoryList.OnSelectedSlot m_dOnSelectedSlot;

		// Token: 0x0200173E RID: 5950
		// (Invoke) Token: 0x0600B2B2 RID: 45746
		public delegate void OnSelectedSlot(int episodeID);
	}
}
