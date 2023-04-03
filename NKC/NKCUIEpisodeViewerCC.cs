using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000988 RID: 2440
	public class NKCUIEpisodeViewerCC : NKCUIBase
	{
		// Token: 0x06006435 RID: 25653 RVA: 0x001FCA17 File Offset: 0x001FAC17
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIEpisodeViewerCC.s_LoadedUIData))
			{
				NKCUIEpisodeViewerCC.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIEpisodeViewerCC>("AB_UI_NKM_UI_COUNTER_CASE", "NKM_EPISODE_CC_Panel", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIEpisodeViewerCC.CleanupInstance));
			}
			return NKCUIEpisodeViewerCC.s_LoadedUIData;
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x001FCA4B File Offset: 0x001FAC4B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIEpisodeViewerCC.s_LoadedUIData != null && NKCUIEpisodeViewerCC.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06006437 RID: 25655 RVA: 0x001FCA60 File Offset: 0x001FAC60
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIEpisodeViewerCC.s_LoadedUIData != null && NKCUIEpisodeViewerCC.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x001FCA75 File Offset: 0x001FAC75
		public static NKCUIEpisodeViewerCC GetInstance()
		{
			if (NKCUIEpisodeViewerCC.s_LoadedUIData != null && NKCUIEpisodeViewerCC.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIEpisodeViewerCC.s_LoadedUIData.GetInstance<NKCUIEpisodeViewerCC>();
			}
			return null;
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x001FCA96 File Offset: 0x001FAC96
		public static void CleanupInstance()
		{
			NKCUIEpisodeViewerCC.s_LoadedUIData = null;
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600643A RID: 25658 RVA: 0x001FCA9E File Offset: 0x001FAC9E
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_CC;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x001FCAA5 File Offset: 0x001FACA5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x0600643C RID: 25660 RVA: 0x001FCAA8 File Offset: 0x001FACA8
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x0600643D RID: 25661 RVA: 0x001FCAAB File Offset: 0x001FACAB
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					3
				};
			}
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x001FCABC File Offset: 0x001FACBC
		public void InitUI()
		{
			this.m_LoopScrollRect.dOnGetObject += this.OnGetObject;
			this.m_LoopScrollRect.dOnReturnObject += this.OnReturnObject;
			this.m_LoopScrollRect.dOnProvideData += this.OnProvideData;
			this.m_LoopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
			this.CalculateContentRectSize();
			this.m_LoopScrollRect.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x001FCB5C File Offset: 0x001FAD5C
		private RectTransform OnGetObject(int index)
		{
			if (this.m_stkItemSlot.Count != 0)
			{
				NKCUIEpisodeActSlotCC nkcuiepisodeActSlotCC = this.m_stkItemSlot.Pop();
				this.m_listItemSlot.Add(nkcuiepisodeActSlotCC);
				return nkcuiepisodeActSlotCC.GetComponent<RectTransform>();
			}
			NKCUIEpisodeActSlotCC newInstance = NKCUIEpisodeActSlotCC.GetNewInstance(this.m_rectContentRect.transform, new NKCUIEpisodeActSlotCC.OnSelectedItemSlot(this.OnSelectedActSlot));
			if (newInstance == null)
			{
				return null;
			}
			newInstance.gameObject.GetComponent<RectTransform>().localScale = Vector2.one;
			this.m_listItemSlot.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x001FCBEC File Offset: 0x001FADEC
		private void OnReturnObject(Transform go)
		{
			NKCUIEpisodeActSlotCC component = go.GetComponent<NKCUIEpisodeActSlotCC>();
			this.m_listItemSlot.Remove(component);
			this.m_stkItemSlot.Push(component);
			go.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x001FCC2C File Offset: 0x001FAE2C
		private void OnProvideData(Transform transform, int idx)
		{
			transform.GetComponent<NKCUIEpisodeActSlotCC>().SetData(this.cNKMEpisodeTemplet, this.m_lstActIDs[idx]);
			NKCUtil.SetGameobjectActive(transform, true);
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x001FCC52 File Offset: 0x001FAE52
		public void SetActID(int id)
		{
			NKCUIEpisodeViewerCC.m_ActID = id;
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x001FCC5A File Offset: 0x001FAE5A
		private void OnSelectedActSlot(int actID)
		{
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OpenCounterCaseNormalAct(actID);
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x001FCC6C File Offset: 0x001FAE6C
		public void UpdateUI()
		{
			this.cNKMEpisodeTemplet = NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCC.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (this.cNKMEpisodeTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_CollectionRateSlider.gameObject, false);
			this.m_lbCollectionRate.text = "";
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x001FCCC0 File Offset: 0x001FAEC0
		public void Open()
		{
			NKCUIFadeInOut.FadeIn(0.1f, null, false);
			this.cNKMEpisodeTemplet = NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCC.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (this.cNKMEpisodeTemplet == null)
			{
				return;
			}
			this.m_lstActIDs.Clear();
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in this.cNKMEpisodeTemplet.m_DicStage)
			{
				bool flag = true;
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					NKMStageTempletV2 nkmstageTempletV = keyValuePair.Value[i];
					if (!nkmstageTempletV.EnableByTag)
					{
						flag = false;
						break;
					}
					if (nkmstageTempletV.m_StageIndex == 1 && nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_OPEN && !NKCContentManager.IsStageUnlocked(ContentsType.EPISODE, nkmstageTempletV.Key))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(NKMEpisodeMgr.GetUnitID(this.cNKMEpisodeTemplet, keyValuePair.Key));
					if (unitTempletBase != null && unitTempletBase.CollectionEnableByTag)
					{
						this.m_lstActIDs.Add(keyValuePair.Key);
					}
				}
			}
			base.UIOpened(true);
			this.m_LoopScrollRect.TotalCount = this.m_lstActIDs.Count;
			this.m_LoopScrollRect.RefreshCells(false);
			this.UpdateUI();
			if (NKCUIEpisodeViewerCC.m_ActID > 0)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OpenCounterCaseNormalAct(NKCUIEpisodeViewerCC.m_ActID);
				NKCUIEpisodeViewerCC.m_ActID = 0;
				return;
			}
			this.CheckTutorial();
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x001FCE38 File Offset: 0x001FB038
		private void CalculateContentRectSize()
		{
			int minColumn = 5;
			Vector2 vUnitSlotSize = this.m_vUnitSlotSize;
			Vector2 vUnitSlotSpacing = this.m_vUnitSlotSpacing;
			if (this.m_safeArea != null)
			{
				this.m_safeArea.SetSafeAreaBase();
			}
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, minColumn, vUnitSlotSize, vUnitSlotSpacing, false);
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x001FCE84 File Offset: 0x001FB084
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x001FCE9F File Offset: 0x001FB09F
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeTemplet(null);
			base.Close();
		}

		// Token: 0x06006449 RID: 25673 RVA: 0x001FCEB7 File Offset: 0x001FB0B7
		public override void UnHide()
		{
			base.UnHide();
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x001FCECB File Offset: 0x001FB0CB
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.CounterCase, true);
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x001FCED8 File Offset: 0x001FB0D8
		public RectTransform GetSlotByUnitID(int unitID)
		{
			int num = -1;
			for (int i = 0; i < this.m_lstActIDs.Count; i++)
			{
				if (NKMEpisodeMgr.GetUnitID(this.cNKMEpisodeTemplet, this.m_lstActIDs[i]) == unitID)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				this.m_LoopScrollRect.SetIndexPosition(num);
				NKCUIEpisodeActSlotCC nkcuiepisodeActSlotCC = this.m_listItemSlot.Find((NKCUIEpisodeActSlotCC v) => v.UnitID == unitID);
				if (nkcuiepisodeActSlotCC != null)
				{
					return nkcuiepisodeActSlotCC.GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x04004FD3 RID: 20435
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_COUNTER_CASE";

		// Token: 0x04004FD4 RID: 20436
		private const string UI_ASSET_NAME = "NKM_EPISODE_CC_Panel";

		// Token: 0x04004FD5 RID: 20437
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04004FD6 RID: 20438
		public Slider m_CollectionRateSlider;

		// Token: 0x04004FD7 RID: 20439
		public Text m_lbCollectionRate;

		// Token: 0x04004FD8 RID: 20440
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04004FD9 RID: 20441
		public RectTransform m_rectContentRect;

		// Token: 0x04004FDA RID: 20442
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04004FDB RID: 20443
		public Vector2 m_vUnitSlotSize;

		// Token: 0x04004FDC RID: 20444
		public Vector2 m_vUnitSlotSpacing;

		// Token: 0x04004FDD RID: 20445
		public NKCUIComSafeArea m_safeArea;

		// Token: 0x04004FDE RID: 20446
		private static int m_EpisodeID = 50;

		// Token: 0x04004FDF RID: 20447
		private static int m_ActID = 0;

		// Token: 0x04004FE0 RID: 20448
		private List<NKCUIEpisodeActSlotCC> m_listItemSlot = new List<NKCUIEpisodeActSlotCC>();

		// Token: 0x04004FE1 RID: 20449
		private Stack<NKCUIEpisodeActSlotCC> m_stkItemSlot = new Stack<NKCUIEpisodeActSlotCC>();

		// Token: 0x04004FE2 RID: 20450
		private List<int> m_lstActIDs = new List<int>();

		// Token: 0x04004FE3 RID: 20451
		private NKMEpisodeTempletV2 cNKMEpisodeTemplet;
	}
}
