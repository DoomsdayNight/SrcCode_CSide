using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NKC.Templet;
using NKC.UI.Component;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C36 RID: 3126
	public class NKCUIPopUpGuide : NKCUIBase
	{
		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06009129 RID: 37161 RVA: 0x003178A4 File Offset: 0x00315AA4
		public static NKCUIPopUpGuide Instance
		{
			get
			{
				if (NKCUIPopUpGuide.m_Instance == null)
				{
					NKCUIPopUpGuide.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopUpGuide>("ab_ui_nkm_ui_tutorial", "NKM_UI_TOTAL_GUIDE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopUpGuide.CleanupInstance)).GetInstance<NKCUIPopUpGuide>();
					NKCUIPopUpGuide.m_Instance.Init();
				}
				return NKCUIPopUpGuide.m_Instance;
			}
		}

		// Token: 0x0600912A RID: 37162 RVA: 0x003178F3 File Offset: 0x00315AF3
		private static void CleanupInstance()
		{
			NKCUIPopUpGuide.m_Instance = null;
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x0600912B RID: 37163 RVA: 0x003178FB File Offset: 0x00315AFB
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x0600912C RID: 37164 RVA: 0x003178FE File Offset: 0x00315AFE
		public static bool HasInstance
		{
			get
			{
				return NKCUIPopUpGuide.m_Instance != null;
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x0600912D RID: 37165 RVA: 0x0031790B File Offset: 0x00315B0B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopUpGuide.m_Instance != null && NKCUIPopUpGuide.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600912E RID: 37166 RVA: 0x00317926 File Offset: 0x00315B26
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopUpGuide.m_Instance != null && NKCUIPopUpGuide.m_Instance.IsOpen)
			{
				NKCUIPopUpGuide.m_Instance.Close();
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x0600912F RID: 37167 RVA: 0x0031794B File Offset: 0x00315B4B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06009130 RID: 37168 RVA: 0x0031794E File Offset: 0x00315B4E
		public override string MenuName
		{
			get
			{
				return "Guide";
			}
		}

		// Token: 0x06009131 RID: 37169 RVA: 0x00317955 File Offset: 0x00315B55
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.ClearSlot();
		}

		// Token: 0x06009132 RID: 37170 RVA: 0x0031796C File Offset: 0x00315B6C
		private void Init()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetBindFunction(this.m_NKM_UI_TOTAL_GUIDE_CLOSE, new UnityAction(base.Close));
			if (this.m_NKM_UI_TOTAL_GUIDE_MANUAL != null)
			{
				this.m_NKM_UI_TOTAL_GUIDE_MANUAL.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_TOTAL_GUIDE_MANUAL.OnValueChanged.AddListener(new UnityAction<bool>(this.OnGuideManual));
			}
			if (this.NKM_UI_TOTAL_GUIDE_GLOSSARY != null)
			{
				this.NKM_UI_TOTAL_GUIDE_GLOSSARY.OnValueChanged.RemoveAllListeners();
				this.NKM_UI_TOTAL_GUIDE_GLOSSARY.OnValueChanged.AddListener(new UnityAction<bool>(this.OnGuideGlossary));
			}
			this.InitUI();
		}

		// Token: 0x06009133 RID: 37171 RVA: 0x00317A1C File Offset: 0x00315C1C
		private void InitUI()
		{
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.Init(false, true);
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.dOnGetObject += this.MakeBannerObject;
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.dOnReturnObject += this.ReturnObject;
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.dOnProvideData += this.ProvideData;
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.dOnFocus += this.Focus;
		}

		// Token: 0x06009134 RID: 37172 RVA: 0x00317A94 File Offset: 0x00315C94
		private void InitData()
		{
			for (int i = 0; i < 1000; i++)
			{
				NKCGuideManualTemplet nkcguideManualTemplet = NKCGuideManualTemplet.Find(i);
				if (nkcguideManualTemplet != null && !this.m_dicCategory.ContainsKey(nkcguideManualTemplet.ID))
				{
					this.m_dicCategory.Add(nkcguideManualTemplet.ID, this.GetSlot(nkcguideManualTemplet.GetTitle()));
					foreach (NKCGuideManualTempletData nkcguideManualTempletData in nkcguideManualTemplet.lstManualTemplets)
					{
						this.m_dicCategory[nkcguideManualTemplet.ID].AddSubSlot(this.GetSubSlot(NKCStringTable.GetString(nkcguideManualTempletData.ARTICLE_STRING_ID, false), nkcguideManualTempletData.ARTICLE_ID));
						NKCGuideTemplet nkcguideTemplet = NKCGuideTemplet.Find(nkcguideManualTempletData.GUIDE_ID_STRING);
						if (nkcguideTemplet != null)
						{
							if (!this.m_dicBundleData.ContainsKey(nkcguideManualTempletData.ARTICLE_ID))
							{
								List<NKCUIPopUpGuide.GuideInfo> list = new List<NKCUIPopUpGuide.GuideInfo>();
								foreach (NKCGuideTempletImage nkcguideTempletImage in nkcguideTemplet.lstImages)
								{
									list.Add(new NKCUIPopUpGuide.GuideInfo(nkcguideTempletImage.GUIDE_BUNDLE_PATH, nkcguideTempletImage.GUIDE_IMAGE_PATH, nkcguideTempletImage.IsSprite));
								}
								if (list.Count <= 0)
								{
									Debug.Log(string.Format("가이드 데이터가 존재하지 않습니다 : id {0}({1})", nkcguideManualTemplet.ID, nkcguideManualTempletData.GUIDE_ID_STRING));
								}
								this.m_dicBundleData.Add(nkcguideManualTempletData.ARTICLE_ID, list);
							}
							else
							{
								Debug.LogWarning(string.Format("중복 타이틀이 있습니다. : id {0}({1})", nkcguideManualTemplet.ID, nkcguideManualTempletData.ARTICLE_ID));
							}
						}
					}
				}
			}
		}

		// Token: 0x06009135 RID: 37173 RVA: 0x00317C74 File Offset: 0x00315E74
		private NKCUIPopupGuideSlot GetSlot(string title)
		{
			NKCUIPopupGuideSlot nkcuipopupGuideSlot = UnityEngine.Object.Instantiate<NKCUIPopupGuideSlot>(this.m_pfbGuideSlot);
			NKCUtil.SetGameobjectActive(nkcuipopupGuideSlot, true);
			nkcuipopupGuideSlot.Init(title, this.m_CONTENT_TOGGLE_GROUP);
			nkcuipopupGuideSlot.GetComponent<RectTransform>().SetParent(this.m_rtNKM_UI_TOTAL_GUIDE_CONTENT);
			if (this.m_fGuideSlotHeight == 0f)
			{
				this.m_fGuideSlotHeight = nkcuipopupGuideSlot.GetComponent<RectTransform>().GetHeight();
			}
			nkcuipopupGuideSlot.transform.localScale = Vector3.one;
			return nkcuipopupGuideSlot;
		}

		// Token: 0x06009136 RID: 37174 RVA: 0x00317CE4 File Offset: 0x00315EE4
		private NKCUIPopupGuideSubSlot GetSubSlot(string title, string articleID)
		{
			NKCUIPopupGuideSubSlot nkcuipopupGuideSubSlot = UnityEngine.Object.Instantiate<NKCUIPopupGuideSubSlot>(this.m_pbfGuideSubSlot);
			NKCUtil.SetGameobjectActive(nkcuipopupGuideSubSlot, true);
			nkcuipopupGuideSubSlot.Init(title, articleID, new NKCUIPopupGuideSubSlot.OnClicked(this.OnClicked));
			nkcuipopupGuideSubSlot.GetComponent<RectTransform>().SetParent(this.m_rtNKM_UI_TOTAL_GUIDE_CONTENT);
			nkcuipopupGuideSubSlot.transform.localScale = Vector3.one;
			return nkcuipopupGuideSubSlot;
		}

		// Token: 0x06009137 RID: 37175 RVA: 0x00317D38 File Offset: 0x00315F38
		public void ClearSlot()
		{
			foreach (KeyValuePair<int, NKCUIPopupGuideSlot> keyValuePair in this.m_dicCategory)
			{
				keyValuePair.Value.Clear();
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
			this.ClearBannerData();
			this.m_dicCategory.Clear();
			this.m_dicBundleData.Clear();
		}

		// Token: 0x06009138 RID: 37176 RVA: 0x00317DC0 File Offset: 0x00315FC0
		private void ClearBannerData()
		{
			foreach (NKCAssetInstanceData nkcassetInstanceData in this.m_lstAssetInstanceData)
			{
				IGuideSubPage component = nkcassetInstanceData.m_Instant.GetComponent<IGuideSubPage>();
				if (component != null)
				{
					component.Clear();
				}
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
			}
			this.m_lstAssetInstanceData.Clear();
			this.m_lstBanner.Clear();
		}

		// Token: 0x06009139 RID: 37177 RVA: 0x00317E3C File Offset: 0x0031603C
		private void Update()
		{
			if (base.IsOpen && this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x0600913A RID: 37178 RVA: 0x00317E5C File Offset: 0x0031605C
		public void Open(string ArticleID = "", int idx = 0)
		{
			this.InitData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (string.IsNullOrEmpty(ArticleID))
			{
				this.m_NKM_UI_TOTAL_GUIDE_IMAGE.TotalCount = 1;
				this.m_NKM_UI_TOTAL_GUIDE_IMAGE.SetIndex(0);
				this.m_rtNKM_UI_TOTAL_GUIDE_CONTENT.DOAnchorPosY(0f, 0f, false);
				this.m_strPreOpendArticleID = "";
			}
			else
			{
				this.OnClicked(ArticleID, idx);
				if (!string.Equals(this.m_strPreOpendArticleID, ArticleID))
				{
					this.m_NKM_UI_TOTAL_GUIDE_SCROLL.vertical = false;
					int num = 0;
					foreach (KeyValuePair<int, NKCUIPopupGuideSlot> keyValuePair in this.m_dicCategory)
					{
						if (keyValuePair.Value.HasChild(ArticleID))
						{
							this.m_rtNKM_UI_TOTAL_GUIDE_CONTENT.DOAnchorPosY(this.m_fGuideSlotHeight * (float)num, 0f, false);
							break;
						}
						num++;
					}
					base.StartCoroutine(this.ChangeScrollVertical());
					this.m_strPreOpendArticleID = ArticleID;
				}
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x0600913B RID: 37179 RVA: 0x00317F80 File Offset: 0x00316180
		private IEnumerator ChangeScrollVertical()
		{
			yield return new WaitForSeconds(0.33f);
			this.m_NKM_UI_TOTAL_GUIDE_SCROLL.vertical = true;
			yield break;
		}

		// Token: 0x0600913C RID: 37180 RVA: 0x00317F8F File Offset: 0x0031618F
		private void OnGuideManual(bool bSel)
		{
			if (bSel)
			{
				this.OnSelectTab(true);
			}
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x00317F9B File Offset: 0x0031619B
		private void OnGuideGlossary(bool bSel)
		{
			if (bSel)
			{
				this.OnSelectTab(false);
			}
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x00317FA7 File Offset: 0x003161A7
		public void OnSelectTab(bool bSelectManual)
		{
		}

		// Token: 0x0600913F RID: 37183 RVA: 0x00317FAC File Offset: 0x003161AC
		public void OnClicked(string ARTICLE_ID, int idx = 0)
		{
			this.ClearBannerData();
			if (!this.m_dicBundleData.ContainsKey(ARTICLE_ID))
			{
				Debug.Log("식별할 수 없는 가이드 id : " + ARTICLE_ID);
				this.Open("", 0);
				return;
			}
			this.m_selectedArticleID = ARTICLE_ID;
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.TotalCount = this.m_dicBundleData[this.m_selectedArticleID].Count;
			this.m_NKM_UI_TOTAL_GUIDE_IMAGE.SetIndex(idx);
			foreach (KeyValuePair<int, NKCUIPopupGuideSlot> keyValuePair in this.m_dicCategory)
			{
				keyValuePair.Value.SelectSubSlot(ARTICLE_ID);
			}
		}

		// Token: 0x06009140 RID: 37184 RVA: 0x0031806C File Offset: 0x0031626C
		private RectTransform MakeBannerObject()
		{
			if (this.m_stkBannerObjects.Count > 0)
			{
				return this.m_stkBannerObjects.Pop();
			}
			return new GameObject("Banner", new Type[]
			{
				typeof(RectTransform),
				typeof(Image)
			}).GetComponent<RectTransform>();
		}

		// Token: 0x06009141 RID: 37185 RVA: 0x003180C2 File Offset: 0x003162C2
		private void ReturnObject(RectTransform rect)
		{
			this.m_stkBannerObjects.Push(rect);
			rect.gameObject.SetActive(false);
			rect.parent = base.transform;
		}

		// Token: 0x06009142 RID: 37186 RVA: 0x003180E8 File Offset: 0x003162E8
		private void ProvideData(RectTransform rect, int idx)
		{
			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;
			rect.sizeDelta = Vector2.zero;
			if (string.IsNullOrEmpty(this.m_selectedArticleID))
			{
				Image component = rect.GetComponent<Image>();
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_tutorial_texutre", "AB_UI_NKM_UI_TUTORIAL_TEXUTRE_TITLE", false);
				if (orLoadAssetResource != null)
				{
					NKCUtil.SetImageSprite(component, orLoadAssetResource, false);
				}
				return;
			}
			if (!this.m_dicBundleData.ContainsKey(this.m_selectedArticleID))
			{
				return;
			}
			if (idx < 0)
			{
				return;
			}
			List<NKCUIPopUpGuide.GuideInfo> list = this.m_dicBundleData[this.m_selectedArticleID];
			if (idx >= list.Count)
			{
				return;
			}
			int childCount = this.m_NKM_UI_TOTAL_GUIDE_IMAGE.m_rtContentRect.childCount;
			int num = idx;
			if (childCount <= idx)
			{
				num = idx % childCount;
			}
			foreach (NKCUIPopUpGuide.BannerObj bannerObj in this.m_lstBanner)
			{
				if (bannerObj.bannerIdx == num)
				{
					NKCUtil.SetGameobjectActive(bannerObj.bannerObj, false);
				}
			}
			if (list[idx].IsSprite)
			{
				Sprite orLoadAssetResource2 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(list[idx].GUIDE_BUNDLE_PATH, list[idx].GUIDE_BUNDLE_NAME, false);
				if (orLoadAssetResource2 != null)
				{
					Image component2 = rect.GetComponent<Image>();
					NKCUtil.SetImageSprite(component2, orLoadAssetResource2, false);
					component2.enabled = true;
					return;
				}
			}
			else
			{
				rect.GetComponent<Image>().enabled = false;
				NKCUIPopUpGuide.BannerObj bannerObj2 = this.m_lstBanner.Find((NKCUIPopUpGuide.BannerObj e) => e.dataIdx == idx);
				if (bannerObj2.bannerObj != null)
				{
					NKCUtil.SetGameobjectActive(bannerObj2.bannerObj, true);
					bannerObj2.bannerIdx = num;
					bannerObj2.bannerObj.transform.SetParent(rect);
					return;
				}
				GameObject banner = this.GetBanner(list[idx].GUIDE_BUNDLE_PATH, list[idx].GUIDE_BUNDLE_NAME);
				if (banner != null)
				{
					IGuideSubPage guideSubPage;
					if (banner.TryGetComponent<IGuideSubPage>(out guideSubPage))
					{
						guideSubPage.SetData();
					}
					banner.transform.SetParent(rect, false);
					NKCUIPopUpGuide.BannerObj item = default(NKCUIPopUpGuide.BannerObj);
					item.bannerIdx = num;
					item.bannerObj = banner;
					item.dataIdx = idx;
					this.m_lstBanner.Add(item);
				}
			}
		}

		// Token: 0x06009143 RID: 37187 RVA: 0x00318364 File Offset: 0x00316564
		private void Focus(RectTransform rect, bool bFocus)
		{
			Image component = rect.GetComponent<Image>();
			if (bFocus)
			{
				component.DOColor(new Color(1f, 1f, 1f, 1f), 0.4f);
				return;
			}
			component.DOColor(new Color(1f, 1f, 1f, 0.5f), 0.4f);
		}

		// Token: 0x06009144 RID: 37188 RVA: 0x003183C8 File Offset: 0x003165C8
		private GameObject GetBanner(string path, string name)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(path, name, false, null);
			if (nkcassetInstanceData != null && nkcassetInstanceData.m_Instant != null)
			{
				this.m_lstAssetInstanceData.Add(nkcassetInstanceData);
				return nkcassetInstanceData.m_Instant.gameObject;
			}
			return null;
		}

		// Token: 0x04007E63 RID: 32355
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_tutorial";

		// Token: 0x04007E64 RID: 32356
		private const string UI_ASSET_NAME = "NKM_UI_TOTAL_GUIDE";

		// Token: 0x04007E65 RID: 32357
		private static NKCUIPopUpGuide m_Instance;

		// Token: 0x04007E66 RID: 32358
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04007E67 RID: 32359
		public ScrollRect m_NKM_UI_TOTAL_GUIDE_SCROLL;

		// Token: 0x04007E68 RID: 32360
		public NKCUIPopupGuideSlot m_pfbGuideSlot;

		// Token: 0x04007E69 RID: 32361
		public NKCUIPopupGuideSubSlot m_pbfGuideSubSlot;

		// Token: 0x04007E6A RID: 32362
		private Dictionary<int, NKCUIPopupGuideSlot> m_dicCategory = new Dictionary<int, NKCUIPopupGuideSlot>();

		// Token: 0x04007E6B RID: 32363
		private float m_fGuideSlotHeight;

		// Token: 0x04007E6C RID: 32364
		private string m_strPreOpendArticleID = "";

		// Token: 0x04007E6D RID: 32365
		[Header("상단 탭")]
		public NKCUIComToggle m_NKM_UI_TOTAL_GUIDE_MANUAL;

		// Token: 0x04007E6E RID: 32366
		public NKCUIComToggle NKM_UI_TOTAL_GUIDE_GLOSSARY;

		// Token: 0x04007E6F RID: 32367
		public RectTransform m_rtNKM_UI_TOTAL_GUIDE_CONTENT;

		// Token: 0x04007E70 RID: 32368
		public NKCUIComStateButton m_NKM_UI_TOTAL_GUIDE_CLOSE;

		// Token: 0x04007E71 RID: 32369
		public NKCUIComToggleGroup m_CONTENT_TOGGLE_GROUP;

		// Token: 0x04007E72 RID: 32370
		public NKCUIComDragSelectablePanel m_NKM_UI_TOTAL_GUIDE_IMAGE;

		// Token: 0x04007E73 RID: 32371
		private Stack<RectTransform> m_stkBannerObjects = new Stack<RectTransform>();

		// Token: 0x04007E74 RID: 32372
		private string m_selectedArticleID = "";

		// Token: 0x04007E75 RID: 32373
		private Dictionary<string, List<NKCUIPopUpGuide.GuideInfo>> m_dicBundleData = new Dictionary<string, List<NKCUIPopUpGuide.GuideInfo>>();

		// Token: 0x04007E76 RID: 32374
		private List<NKCUIPopUpGuide.BannerObj> m_lstBanner = new List<NKCUIPopUpGuide.BannerObj>();

		// Token: 0x04007E77 RID: 32375
		private List<NKCAssetInstanceData> m_lstAssetInstanceData = new List<NKCAssetInstanceData>();

		// Token: 0x020019FE RID: 6654
		private struct GuideInfo
		{
			// Token: 0x0600BAB8 RID: 47800 RVA: 0x0036E3C0 File Offset: 0x0036C5C0
			public GuideInfo(string path, string name, bool bSprite)
			{
				this.GUIDE_BUNDLE_PATH = path;
				this.GUIDE_BUNDLE_NAME = name;
				this.IsSprite = bSprite;
			}

			// Token: 0x0400AD77 RID: 44407
			public string GUIDE_BUNDLE_PATH;

			// Token: 0x0400AD78 RID: 44408
			public string GUIDE_BUNDLE_NAME;

			// Token: 0x0400AD79 RID: 44409
			public bool IsSprite;
		}

		// Token: 0x020019FF RID: 6655
		private struct BannerObj
		{
			// Token: 0x0400AD7A RID: 44410
			public int bannerIdx;

			// Token: 0x0400AD7B RID: 44411
			public int dataIdx;

			// Token: 0x0400AD7C RID: 44412
			public GameObject bannerObj;
		}
	}
}
