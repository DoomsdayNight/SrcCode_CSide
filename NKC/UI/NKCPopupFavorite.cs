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
	// Token: 0x02000A4A RID: 2634
	public class NKCPopupFavorite : NKCUIBase
	{
		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06007397 RID: 29591 RVA: 0x0026729C File Offset: 0x0026549C
		public static NKCPopupFavorite Instance
		{
			get
			{
				if (NKCPopupFavorite.m_Instance == null)
				{
					NKCPopupFavorite.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFavorite>("AB_UI_OPERATION", "AB_UI_OPERATION_FAV", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFavorite.CleanupInstance)).GetInstance<NKCPopupFavorite>();
					NKCPopupFavorite.m_Instance.Initialize();
				}
				return NKCPopupFavorite.m_Instance;
			}
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x002672EB File Offset: 0x002654EB
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFavorite.m_Instance != null && NKCPopupFavorite.m_Instance.IsOpen)
			{
				NKCPopupFavorite.m_Instance.Close();
			}
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x00267310 File Offset: 0x00265510
		private static void CleanupInstance()
		{
			NKCPopupFavorite.m_Instance = null;
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x00267318 File Offset: 0x00265518
		public static bool isOpen()
		{
			return NKCPopupFavorite.m_Instance != null && NKCPopupFavorite.m_Instance.IsOpen;
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x0600739B RID: 29595 RVA: 0x00267333 File Offset: 0x00265533
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x0600739C RID: 29596 RVA: 0x00267336 File Offset: 0x00265536
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x00267340 File Offset: 0x00265540
		public override void Initialize()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_Loop.dOnGetObject += this.GetObject;
			this.m_Loop.dOnReturnObject += this.ReturnObject;
			this.m_Loop.dOnProvideData += this.ProvideData;
			this.m_Loop.PrepareCells(0);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_CurGroupCategory = EPISODE_GROUP.EG_SUMMARY;
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.InitToggle(this.m_tglAll, EPISODE_GROUP.EG_SUMMARY);
			this.InitToggle(this.m_tglMainStream, EPISODE_GROUP.EG_MAINSTREAM);
			this.InitToggle(this.m_tglStory, EPISODE_GROUP.EG_SUBSTREAM);
			this.InitToggle(this.m_tglGrowth, EPISODE_GROUP.EG_GROWTH);
			this.InitToggle(this.m_tglChallenge, EPISODE_GROUP.EG_CHALLENGE);
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x0026742C File Offset: 0x0026562C
		private void InitToggle(NKCUIComToggle toggle, EPISODE_GROUP category)
		{
			this.m_dicToggle.Add(category, toggle);
			toggle.OnValueChanged.RemoveAllListeners();
			toggle.OnValueChanged.AddListener(delegate(bool bValue)
			{
				this.OnClickCategory(category);
			});
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x00267484 File Offset: 0x00265684
		private RectTransform GetObject(int idx)
		{
			NKCPopupFavoriteSlot nkcpopupFavoriteSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupFavoriteSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupFavoriteSlot = UnityEngine.Object.Instantiate<NKCPopupFavoriteSlot>(this.m_pfbSlot, this.m_Loop.content);
				nkcpopupFavoriteSlot.InitUI();
			}
			return nkcpopupFavoriteSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x002674D4 File Offset: 0x002656D4
		private void ReturnObject(Transform tr)
		{
			NKCPopupFavoriteSlot component = tr.GetComponent<NKCPopupFavoriteSlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, false);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x00267508 File Offset: 0x00265708
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupFavoriteSlot component = tr.GetComponent<NKCPopupFavoriteSlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			if (this.m_dicData.ContainsKey(this.m_CurGroupCategory))
			{
				component.SetData(this.m_dicData[this.m_CurGroupCategory][idx]);
				return;
			}
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x060073A2 RID: 29602 RVA: 0x00267565 File Offset: 0x00265765
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCPopupFavorite.OnClose dCallback = this.m_dCallback;
			if (dCallback == null)
			{
				return;
			}
			dCallback();
		}

		// Token: 0x060073A3 RID: 29603 RVA: 0x00267583 File Offset: 0x00265783
		public void Open(NKCPopupFavorite.OnClose callback)
		{
			this.m_dCallback = callback;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_CurGroupCategory = EPISODE_GROUP.EG_SUMMARY;
			this.RefreshList();
			base.UIOpened(true);
			this.TutorialCheck();
		}

		// Token: 0x060073A4 RID: 29604 RVA: 0x002675B4 File Offset: 0x002657B4
		public void RefreshList()
		{
			this.BuildFavoriteData();
			foreach (KeyValuePair<EPISODE_GROUP, NKCUIComToggle> keyValuePair in this.m_dicToggle)
			{
				keyValuePair.Value.Select(keyValuePair.Key == this.m_CurGroupCategory, true, false);
			}
			NKCUtil.SetLabelText(this.m_lbCategoryName, this.GetCategoryName(this.m_CurGroupCategory));
			this.m_Loop.TotalCount = (this.m_dicData.ContainsKey(this.m_CurGroupCategory) ? this.m_dicData[this.m_CurGroupCategory].Count : 0);
			if (this.m_Loop.TotalCount > 0)
			{
				this.m_Loop.SetIndexPosition(0);
			}
			else
			{
				this.m_Loop.RefreshCells(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_Loop.TotalCount == 0);
			NKCUtil.SetLabelText(this.m_lbFavoriteCount, string.Format("{0}/{1}", this.m_dicData[EPISODE_GROUP.EG_SUMMARY].Count, NKMCommonConst.MaxStageFavoriteCount));
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x002676E8 File Offset: 0x002658E8
		private string GetCategoryName(EPISODE_GROUP category)
		{
			if (category == EPISODE_GROUP.EG_SUMMARY || category - EPISODE_GROUP.EG_MAINSTREAM > 3)
			{
				return NKCStringTable.GetString("SI_PF_EPISODE_MENU_FAVORITE_ALL", false);
			}
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(category);
			if (nkmepisodeGroupTemplet != null)
			{
				return NKCStringTable.GetString(nkmepisodeGroupTemplet.m_EPGroupName, false);
			}
			return "";
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x00267728 File Offset: 0x00265928
		private void BuildFavoriteData()
		{
			this.m_dicData = new Dictionary<EPISODE_GROUP, List<NKMStageTempletV2>>();
			Dictionary<int, NKMStageTempletV2> favoriteStageList = NKMEpisodeMgr.GetFavoriteStageList();
			List<NKMStageTempletV2> list = new List<NKMStageTempletV2>();
			List<NKMStageTempletV2> list2 = new List<NKMStageTempletV2>();
			for (int i = 0; i < favoriteStageList.Count; i++)
			{
				if (favoriteStageList.ContainsKey(i))
				{
					if (favoriteStageList[i].EpisodeTemplet.IsOpen && favoriteStageList[i].IsOpenedDayOfWeek())
					{
						list.Add(favoriteStageList[i]);
					}
					else
					{
						list2.Add(favoriteStageList[i]);
					}
				}
			}
			this.m_dicData.Add(EPISODE_GROUP.EG_SUMMARY, new List<NKMStageTempletV2>());
			this.m_dicData[EPISODE_GROUP.EG_SUMMARY].AddRange(list);
			this.m_dicData[EPISODE_GROUP.EG_SUMMARY].AddRange(list2);
			for (int j = 0; j < list.Count; j++)
			{
				NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(list[j].EpisodeTemplet.m_GroupID);
				if (nkmepisodeGroupTemplet != null)
				{
					if (!this.m_dicData.ContainsKey(nkmepisodeGroupTemplet.GroupCategory))
					{
						this.m_dicData.Add(nkmepisodeGroupTemplet.GroupCategory, new List<NKMStageTempletV2>());
					}
					this.m_dicData[nkmepisodeGroupTemplet.GroupCategory].Add(list[j]);
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				NKMEpisodeGroupTemplet nkmepisodeGroupTemplet2 = NKMEpisodeGroupTemplet.Find(list2[k].EpisodeTemplet.m_GroupID);
				if (nkmepisodeGroupTemplet2 != null)
				{
					if (!this.m_dicData.ContainsKey(nkmepisodeGroupTemplet2.GroupCategory))
					{
						this.m_dicData.Add(nkmepisodeGroupTemplet2.GroupCategory, new List<NKMStageTempletV2>());
					}
					this.m_dicData[nkmepisodeGroupTemplet2.GroupCategory].Add(list2[k]);
				}
			}
		}

		// Token: 0x060073A7 RID: 29607 RVA: 0x002678D6 File Offset: 0x00265AD6
		private void OnClickCategory(EPISODE_GROUP category)
		{
			this.m_CurGroupCategory = category;
			this.RefreshList();
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x002678E5 File Offset: 0x00265AE5
		public void RefreshData()
		{
			this.m_Loop.RefreshCells(false);
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x002678F3 File Offset: 0x00265AF3
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_Favorite, true);
		}

		// Token: 0x04005F91 RID: 24465
		private const string ASSET_BUNDLE_NAME = "AB_UI_OPERATION";

		// Token: 0x04005F92 RID: 24466
		private const string UI_ASSET_NAME = "AB_UI_OPERATION_FAV";

		// Token: 0x04005F93 RID: 24467
		private static NKCPopupFavorite m_Instance;

		// Token: 0x04005F94 RID: 24468
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04005F95 RID: 24469
		public NKCPopupFavoriteSlot m_pfbSlot;

		// Token: 0x04005F96 RID: 24470
		public NKCUIComToggle m_tglAll;

		// Token: 0x04005F97 RID: 24471
		public NKCUIComToggle m_tglMainStream;

		// Token: 0x04005F98 RID: 24472
		public NKCUIComToggle m_tglStory;

		// Token: 0x04005F99 RID: 24473
		public NKCUIComToggle m_tglGrowth;

		// Token: 0x04005F9A RID: 24474
		public NKCUIComToggle m_tglChallenge;

		// Token: 0x04005F9B RID: 24475
		public TMP_Text m_lbCategoryName;

		// Token: 0x04005F9C RID: 24476
		public LoopScrollRect m_Loop;

		// Token: 0x04005F9D RID: 24477
		public TMP_Text m_lbFavoriteCount;

		// Token: 0x04005F9E RID: 24478
		public GameObject m_objNone;

		// Token: 0x04005F9F RID: 24479
		private Stack<NKCPopupFavoriteSlot> m_stkSlot = new Stack<NKCPopupFavoriteSlot>();

		// Token: 0x04005FA0 RID: 24480
		private Dictionary<EPISODE_GROUP, List<NKMStageTempletV2>> m_dicData = new Dictionary<EPISODE_GROUP, List<NKMStageTempletV2>>();

		// Token: 0x04005FA1 RID: 24481
		private Dictionary<EPISODE_GROUP, NKCUIComToggle> m_dicToggle = new Dictionary<EPISODE_GROUP, NKCUIComToggle>();

		// Token: 0x04005FA2 RID: 24482
		private EPISODE_GROUP m_CurGroupCategory;

		// Token: 0x04005FA3 RID: 24483
		private NKCPopupFavorite.OnClose m_dCallback;

		// Token: 0x02001794 RID: 6036
		// (Invoke) Token: 0x0600B3B7 RID: 46007
		public delegate void OnClose();
	}
}
