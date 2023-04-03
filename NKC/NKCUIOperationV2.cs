using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A10 RID: 2576
	public class NKCUIOperationV2 : NKCUIBase
	{
		// Token: 0x0600706D RID: 28781 RVA: 0x002546F0 File Offset: 0x002528F0
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIOperationV2.s_LoadedUIData))
			{
				NKCUIOperationV2.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIOperationV2>("AB_UI_OPERATION", "AB_UI_OPERATION_UI", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperationV2.CleanupInstance));
			}
			return NKCUIOperationV2.s_LoadedUIData;
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x0600706E RID: 28782 RVA: 0x00254724 File Offset: 0x00252924
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperationV2.s_LoadedUIData != null && NKCUIOperationV2.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x0600706F RID: 28783 RVA: 0x00254739 File Offset: 0x00252939
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIOperationV2.s_LoadedUIData != null && NKCUIOperationV2.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06007070 RID: 28784 RVA: 0x0025474E File Offset: 0x0025294E
		public static NKCUIOperationV2 GetInstance()
		{
			if (NKCUIOperationV2.s_LoadedUIData != null && NKCUIOperationV2.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIOperationV2.s_LoadedUIData.GetInstance<NKCUIOperationV2>();
			}
			return null;
		}

		// Token: 0x06007071 RID: 28785 RVA: 0x0025476F File Offset: 0x0025296F
		public static void CleanupInstance()
		{
			NKCUIOperationV2.s_LoadedUIData = null;
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06007072 RID: 28786 RVA: 0x00254777 File Offset: 0x00252977
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06007073 RID: 28787 RVA: 0x0025477A File Offset: 0x0025297A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_OPERATION_VIEWER;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06007074 RID: 28788 RVA: 0x00254781 File Offset: 0x00252981
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.LeftsideWithHamburger;
			}
		}

		// Token: 0x06007075 RID: 28789 RVA: 0x00254784 File Offset: 0x00252984
		public override void UnHide()
		{
			base.UnHide();
			foreach (KeyValuePair<EPISODE_GROUP, NKCUIOperationCategorySlot> keyValuePair in this.m_dicCategory)
			{
				keyValuePair.Value.UpdateTglState();
				keyValuePair.Value.SetSelected(keyValuePair.Value.GetEpisodeGroup() == this.m_CurSelectedGroup);
			}
			this.OnSelectCategory(this.m_CurSelectedGroup);
			this.TutorialCheck();
		}

		// Token: 0x06007076 RID: 28790 RVA: 0x00254814 File Offset: 0x00252A14
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x06007077 RID: 28791 RVA: 0x00254822 File Offset: 0x00252A22
		public override void CloseInternal()
		{
			if (NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet() == null)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeCategory(EPISODE_CATEGORY.EC_COUNT);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007078 RID: 28792 RVA: 0x00254854 File Offset: 0x00252A54
		public void InitUI()
		{
			this.m_Summary.InitUI();
			this.m_MainStream.InitUI();
			this.m_Story.InitUI();
			this.m_Growth.InitUI();
			this.m_dicCategory = new Dictionary<EPISODE_GROUP, NKCUIOperationCategorySlot>();
			this.m_dicCategory.Add(EPISODE_GROUP.EG_SUMMARY, this.m_tglSummary);
			this.m_dicCategory.Add(EPISODE_GROUP.EG_MAINSTREAM, this.m_tglMainStream);
			this.m_dicCategory.Add(EPISODE_GROUP.EG_SUBSTREAM, this.m_tglStory);
			this.m_dicCategory.Add(EPISODE_GROUP.EG_GROWTH, this.m_tglGrowth);
			this.m_dicCategory.Add(EPISODE_GROUP.EG_CHALLENGE, this.m_tglChallenge);
			this.m_tglFavorite.OnValueChanged.RemoveAllListeners();
			this.m_tglFavorite.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickFavorite));
			this.m_tglSummary.InitUI(EPISODE_GROUP.EG_SUMMARY, new NKCUIOperationCategorySlot.OnSlotSelect(this.OnClickCategory));
			this.m_tglMainStream.InitUI(EPISODE_GROUP.EG_MAINSTREAM, new NKCUIOperationCategorySlot.OnSlotSelect(this.OnClickCategory));
			this.m_tglStory.InitUI(EPISODE_GROUP.EG_SUBSTREAM, new NKCUIOperationCategorySlot.OnSlotSelect(this.OnClickCategory));
			this.m_tglGrowth.InitUI(EPISODE_GROUP.EG_GROWTH, new NKCUIOperationCategorySlot.OnSlotSelect(this.OnClickCategory));
			this.m_tglChallenge.InitUI(EPISODE_GROUP.EG_CHALLENGE, new NKCUIOperationCategorySlot.OnSlotSelect(this.OnClickCategory));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007079 RID: 28793 RVA: 0x002549A4 File Offset: 0x00252BA4
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			foreach (KeyValuePair<EPISODE_GROUP, NKCUIOperationCategorySlot> keyValuePair in this.m_dicCategory)
			{
				keyValuePair.Value.UpdateTglState();
			}
			this.m_tglFavorite.Select(false, true, false);
			base.UIOpened(true);
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeCategory());
			if (nkmepisodeGroupTemplet != null)
			{
				this.OnClickCategory(nkmepisodeGroupTemplet.GroupCategory, false);
			}
			else if (NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_SUMMARY, 0, 0))
			{
				this.OnClickCategory(EPISODE_GROUP.EG_SUMMARY, false);
			}
			else
			{
				this.OnClickCategory(EPISODE_GROUP.EG_MAINSTREAM, false);
			}
			if (NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite = false;
				NKCPopupFavorite.Instance.Open(new NKCPopupFavorite.OnClose(this.OnCloseFavorite));
			}
			this.TutorialCheck();
		}

		// Token: 0x0600707A RID: 28794 RVA: 0x00254AA0 File Offset: 0x00252CA0
		public void PreLoad()
		{
		}

		// Token: 0x0600707B RID: 28795 RVA: 0x00254AA4 File Offset: 0x00252CA4
		public void OnSelectCategory(EPISODE_GROUP category)
		{
			this.m_CurSelectedGroup = category;
			NKCUtil.SetGameobjectActive(this.m_Summary, category == EPISODE_GROUP.EG_SUMMARY);
			NKCUtil.SetGameobjectActive(this.m_MainStream, category == EPISODE_GROUP.EG_MAINSTREAM);
			NKCUtil.SetGameobjectActive(this.m_Story, category == EPISODE_GROUP.EG_SUBSTREAM);
			NKCUtil.SetGameobjectActive(this.m_Growth, category == EPISODE_GROUP.EG_GROWTH);
			NKCUtil.SetGameobjectActive(this.m_Challenge, category == EPISODE_GROUP.EG_CHALLENGE);
			if (NKCPopupOperationSubStoryList.isOpen())
			{
				NKCPopupOperationSubStoryList.Instance.Close();
			}
			if (category != EPISODE_GROUP.EG_SUBSTREAM)
			{
				NKCSoundManager.PlayScenMusic(NKM_SCEN_ID.NSI_HOME, false);
			}
			switch (category)
			{
			case EPISODE_GROUP.EG_SUMMARY:
				this.m_Summary.Open();
				break;
			case EPISODE_GROUP.EG_MAINSTREAM:
				this.m_MainStream.Open();
				break;
			case EPISODE_GROUP.EG_SUBSTREAM:
				this.m_Story.Open();
				break;
			case EPISODE_GROUP.EG_GROWTH:
				this.m_Growth.Open();
				break;
			case EPISODE_GROUP.EG_CHALLENGE:
				this.m_Challenge.Open();
				break;
			}
			NKCUIFadeInOut.FadeIn(this.m_FadeTime, null, false);
		}

		// Token: 0x0600707C RID: 28796 RVA: 0x00254B88 File Offset: 0x00252D88
		public void OnClickCategory(EPISODE_GROUP category, bool bShowFade = true)
		{
			foreach (KeyValuePair<EPISODE_GROUP, NKCUIOperationCategorySlot> keyValuePair in this.m_dicCategory)
			{
				keyValuePair.Value.UpdateTglState();
				if (keyValuePair.Key == this.m_CurSelectedGroup || keyValuePair.Key == category)
				{
					keyValuePair.Value.ChangeSelected(keyValuePair.Key == category);
				}
			}
			if (bShowFade)
			{
				NKCUIFadeInOut.FadeOut(this.m_FadeTime, delegate
				{
					this.OnSelectCategory(category);
				}, false, -1f);
				return;
			}
			this.OnSelectCategory(category);
		}

		// Token: 0x0600707D RID: 28797 RVA: 0x00254C5C File Offset: 0x00252E5C
		public void OnClickMainStream(bool bSet = true)
		{
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_GROUP.EG_MAINSTREAM);
		}

		// Token: 0x0600707E RID: 28798 RVA: 0x00254C69 File Offset: 0x00252E69
		public void OnClickStory(bool bSet = true)
		{
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_GROUP.EG_SUBSTREAM);
		}

		// Token: 0x0600707F RID: 28799 RVA: 0x00254C76 File Offset: 0x00252E76
		public void OnClickGrowth(bool bSet = true)
		{
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_GROUP.EG_GROWTH);
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x00254C83 File Offset: 0x00252E83
		public void OnClickChallenge(bool bSet = true)
		{
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_GROUP.EG_CHALLENGE);
		}

		// Token: 0x06007081 RID: 28801 RVA: 0x00254C90 File Offset: 0x00252E90
		public void OnClickFavorite(bool bValue)
		{
			if (bValue)
			{
				NKCPopupFavorite.Instance.Open(new NKCPopupFavorite.OnClose(this.OnCloseFavorite));
				return;
			}
			NKCPopupFavorite.CheckInstanceAndClose();
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x00254CB1 File Offset: 0x00252EB1
		private void OnCloseFavorite()
		{
			this.m_tglFavorite.Select(false, true, false);
		}

		// Token: 0x06007083 RID: 28803 RVA: 0x00254CC2 File Offset: 0x00252EC2
		private void TutorialCheck()
		{
			if (base.gameObject.activeSelf)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.Operation, true);
			}
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x00254CDA File Offset: 0x00252EDA
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			this.OnSelectCategory(EPISODE_GROUP.EG_MAINSTREAM);
			this.m_MainStream.SetTutorialMainstreamGuide(eventTemplet, Complete);
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x00254CF0 File Offset: 0x00252EF0
		public RectTransform GetDailyRect()
		{
			return null;
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x00254CF3 File Offset: 0x00252EF3
		public RectTransform GetStageSlotRect(int stageIndex)
		{
			return null;
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x00254CF6 File Offset: 0x00252EF6
		public RectTransform GetActSlotRect(int actIndex)
		{
			return null;
		}

		// Token: 0x04005C2D RID: 23597
		private const string ASSET_BUNDLE_NAME = "AB_UI_OPERATION";

		// Token: 0x04005C2E RID: 23598
		private const string UI_ASSET_NAME = "AB_UI_OPERATION_UI";

		// Token: 0x04005C2F RID: 23599
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04005C30 RID: 23600
		public NKCUIOperationSubSummary m_Summary;

		// Token: 0x04005C31 RID: 23601
		public NKCUIOperationSubMainStream m_MainStream;

		// Token: 0x04005C32 RID: 23602
		public NKCUIOperationSubStory m_Story;

		// Token: 0x04005C33 RID: 23603
		public NKCUIOperationSubGrowth m_Growth;

		// Token: 0x04005C34 RID: 23604
		public NKCUIOperationSubChallenge m_Challenge;

		// Token: 0x04005C35 RID: 23605
		public NKCUIComToggle m_tglFavorite;

		// Token: 0x04005C36 RID: 23606
		public NKCUIOperationCategorySlot m_tglSummary;

		// Token: 0x04005C37 RID: 23607
		public NKCUIOperationCategorySlot m_tglMainStream;

		// Token: 0x04005C38 RID: 23608
		public NKCUIOperationCategorySlot m_tglStory;

		// Token: 0x04005C39 RID: 23609
		public NKCUIOperationCategorySlot m_tglGrowth;

		// Token: 0x04005C3A RID: 23610
		public NKCUIOperationCategorySlot m_tglChallenge;

		// Token: 0x04005C3B RID: 23611
		public float m_FadeTime = 0.3f;

		// Token: 0x04005C3C RID: 23612
		private Dictionary<EPISODE_GROUP, NKCUIOperationCategorySlot> m_dicCategory = new Dictionary<EPISODE_GROUP, NKCUIOperationCategorySlot>();

		// Token: 0x04005C3D RID: 23613
		private EPISODE_GROUP m_CurSelectedGroup;

		// Token: 0x0200174C RID: 5964
		public enum OPERATION_CATEGORY
		{
			// Token: 0x0400A678 RID: 42616
			Summary,
			// Token: 0x0400A679 RID: 42617
			MainStream,
			// Token: 0x0400A67A RID: 42618
			Story,
			// Token: 0x0400A67B RID: 42619
			Growth,
			// Token: 0x0400A67C RID: 42620
			Challenge
		}
	}
}
