using System;
using System.Collections.Generic;
using DG.Tweening;
using NKC.Templet;
using NKC.UI.Component;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C33 RID: 3123
	public class NKCUIPopupTutorialImagePanel : NKCUIBase
	{
		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06009110 RID: 37136 RVA: 0x00317108 File Offset: 0x00315308
		public static NKCUIPopupTutorialImagePanel Instance
		{
			get
			{
				if (NKCUIPopupTutorialImagePanel.m_Instance == null)
				{
					NKCUIPopupTutorialImagePanel.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupTutorialImagePanel>("ab_ui_nkm_ui_tutorial", "NKM_UI_POPUP_IMAGE_TUTORIAL", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupTutorialImagePanel.CleanupInstance)).GetInstance<NKCUIPopupTutorialImagePanel>();
					NKCUIPopupTutorialImagePanel.m_Instance.InitUI();
				}
				return NKCUIPopupTutorialImagePanel.m_Instance;
			}
		}

		// Token: 0x06009111 RID: 37137 RVA: 0x00317157 File Offset: 0x00315357
		private static void CleanupInstance()
		{
			NKCUIPopupTutorialImagePanel.m_Instance = null;
		}

		// Token: 0x06009112 RID: 37138 RVA: 0x0031715F File Offset: 0x0031535F
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupTutorialImagePanel.m_Instance != null && NKCUIPopupTutorialImagePanel.m_Instance.IsOpen)
			{
				NKCUIPopupTutorialImagePanel.m_Instance.Close();
			}
		}

		// Token: 0x06009113 RID: 37139 RVA: 0x00317184 File Offset: 0x00315384
		private void OnDestroy()
		{
			NKCUIPopupTutorialImagePanel.m_Instance = null;
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06009114 RID: 37140 RVA: 0x0031718C File Offset: 0x0031538C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06009115 RID: 37141 RVA: 0x0031718F File Offset: 0x0031538F
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_TUTORIAL_IMAGE;
			}
		}

		// Token: 0x06009116 RID: 37142 RVA: 0x00317196 File Offset: 0x00315396
		public override void CloseInternal()
		{
			this.ClearBannerData();
			base.gameObject.SetActive(false);
			NKCUIPopupTutorialImagePanel.OnClose onClose = this.dOnClose;
			if (onClose == null)
			{
				return;
			}
			onClose();
		}

		// Token: 0x06009117 RID: 37143 RVA: 0x003171BC File Offset: 0x003153BC
		public void InitUI()
		{
			this.m_csbtnClose.PointerClick.RemoveAllListeners();
			this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_UIComDragPanel.Init(false, true);
			this.m_UIComDragPanel.dOnGetObject += this.MakeImageObject;
			this.m_UIComDragPanel.dOnReturnObject += this.ReturnObject;
			this.m_UIComDragPanel.dOnProvideData += this.ProvideData;
			this.m_UIComDragPanel.dOnFocus += this.Focus;
		}

		// Token: 0x06009118 RID: 37144 RVA: 0x0031725E File Offset: 0x0031545E
		public void Open(int guideID, NKCUIPopupTutorialImagePanel.OnClose onClose)
		{
			this.Open(NKCGuideTemplet.Find(guideID), onClose);
		}

		// Token: 0x06009119 RID: 37145 RVA: 0x00317270 File Offset: 0x00315470
		public void Open(string guideStrID, NKCUIPopupTutorialImagePanel.OnClose onClose)
		{
			NKCGuideTemplet nkcguideTemplet = NKCGuideTemplet.Find(guideStrID);
			if (nkcguideTemplet == null)
			{
				Debug.LogError("GuideTemplet " + guideStrID + " not found");
				NKCUIBase.SetGameObjectActive(base.gameObject, false);
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			this.Open(nkcguideTemplet, onClose);
		}

		// Token: 0x0600911A RID: 37146 RVA: 0x003172BC File Offset: 0x003154BC
		private void Open(NKCGuideTemplet templet, NKCUIPopupTutorialImagePanel.OnClose onClose)
		{
			if (templet == null)
			{
				Debug.LogError("GuideTemplet not found");
				if (onClose != null)
				{
					onClose();
				}
				NKCUIBase.SetGameObjectActive(base.gameObject, false);
				return;
			}
			this.dOnClose = onClose;
			this.m_Templet = templet;
			this.m_UIComDragPanel.TotalCount = this.m_Templet.lstImages.Count;
			this.m_UIComDragPanel.SetIndex(0);
			base.UIOpened(true);
		}

		// Token: 0x0600911B RID: 37147 RVA: 0x00317328 File Offset: 0x00315528
		private RectTransform MakeImageObject()
		{
			if (this.m_stkImageObjects.Count > 0)
			{
				return this.m_stkImageObjects.Pop();
			}
			return new GameObject("ImagePanel", new Type[]
			{
				typeof(RectTransform),
				typeof(Image)
			}).GetComponent<RectTransform>();
		}

		// Token: 0x0600911C RID: 37148 RVA: 0x0031737E File Offset: 0x0031557E
		private void ReturnObject(RectTransform rect)
		{
			this.m_stkImageObjects.Push(rect);
			rect.gameObject.SetActive(false);
			rect.parent = base.transform;
		}

		// Token: 0x0600911D RID: 37149 RVA: 0x003173A4 File Offset: 0x003155A4
		private void ProvideData(RectTransform rect, int idx)
		{
			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;
			rect.sizeDelta = Vector2.zero;
			int childCount = this.m_UIComDragPanel.m_rtContentRect.childCount;
			int num = idx;
			if (childCount <= idx)
			{
				num = idx % childCount;
				using (List<NKCUIPopupTutorialImagePanel.BannerObj>.Enumerator enumerator = this.m_lstBanner.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKCUIPopupTutorialImagePanel.BannerObj bannerObj = enumerator.Current;
						if (bannerObj.bannerIdx == num)
						{
							NKCUtil.SetGameobjectActive(bannerObj.bannerObj, false);
						}
					}
					goto IL_FC;
				}
			}
			foreach (NKCUIPopupTutorialImagePanel.BannerObj bannerObj2 in this.m_lstBanner)
			{
				if (bannerObj2.bannerIdx != idx && bannerObj2.bannerIdx % childCount == idx)
				{
					NKCUtil.SetGameobjectActive(bannerObj2.bannerObj, false);
				}
			}
			IL_FC:
			if (this.m_Templet.lstImages[idx].IsSprite)
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(this.m_Templet.lstImages[idx].GUIDE_BUNDLE_PATH, this.m_Templet.lstImages[idx].GUIDE_IMAGE_PATH, false);
				if (orLoadAssetResource != null)
				{
					Image component = rect.GetComponent<Image>();
					NKCUtil.SetImageSprite(component, orLoadAssetResource, false);
					component.enabled = true;
					return;
				}
			}
			else
			{
				Image component2 = rect.GetComponent<Image>();
				if (component2 != null)
				{
					component2.enabled = false;
				}
				NKCUIPopupTutorialImagePanel.BannerObj bannerObj3 = this.m_lstBanner.Find((NKCUIPopupTutorialImagePanel.BannerObj e) => e.dataIdx == idx);
				if (bannerObj3.bannerObj != null)
				{
					NKCUtil.SetGameobjectActive(bannerObj3.bannerObj, true);
					bannerObj3.bannerIdx = idx;
					bannerObj3.bannerObj.transform.SetParent(rect);
					return;
				}
				GameObject banner = this.GetBanner(this.m_Templet.lstImages[idx].GUIDE_BUNDLE_PATH, this.m_Templet.lstImages[idx].GUIDE_IMAGE_PATH);
				if (banner != null)
				{
					banner.transform.SetParent(rect, false);
					NKCUIPopupTutorialImagePanel.BannerObj item = default(NKCUIPopupTutorialImagePanel.BannerObj);
					item.bannerIdx = idx;
					item.bannerObj = banner;
					item.dataIdx = idx;
					this.m_lstBanner.Add(item);
				}
			}
		}

		// Token: 0x0600911E RID: 37150 RVA: 0x00317644 File Offset: 0x00315844
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

		// Token: 0x0600911F RID: 37151 RVA: 0x003176A8 File Offset: 0x003158A8
		private Sprite OpenSprite(int index)
		{
			if (this.m_Templet == null)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			if (index >= this.m_Templet.lstImages.Count)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>(this.m_Templet.lstImages[index].GUIDE_BUNDLE_PATH, this.m_Templet.lstImages[index].GUIDE_IMAGE_PATH, false);
		}

		// Token: 0x06009120 RID: 37152 RVA: 0x0031770C File Offset: 0x0031590C
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

		// Token: 0x06009121 RID: 37153 RVA: 0x00317750 File Offset: 0x00315950
		private void ClearBannerData()
		{
			foreach (NKCAssetInstanceData cInstantData in this.m_lstAssetInstanceData)
			{
				NKCAssetResourceManager.CloseInstance(cInstantData);
			}
			this.m_lstAssetInstanceData.Clear();
			this.m_lstBanner.Clear();
		}

		// Token: 0x04007E56 RID: 32342
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_tutorial";

		// Token: 0x04007E57 RID: 32343
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_IMAGE_TUTORIAL";

		// Token: 0x04007E58 RID: 32344
		private static NKCUIPopupTutorialImagePanel m_Instance;

		// Token: 0x04007E59 RID: 32345
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04007E5A RID: 32346
		public NKCUIComDragSelectablePanel m_UIComDragPanel;

		// Token: 0x04007E5B RID: 32347
		private NKCUIPopupTutorialImagePanel.OnClose dOnClose;

		// Token: 0x04007E5C RID: 32348
		private NKCGuideTemplet m_Templet;

		// Token: 0x04007E5D RID: 32349
		private Stack<RectTransform> m_stkImageObjects = new Stack<RectTransform>();

		// Token: 0x04007E5E RID: 32350
		private List<NKCUIPopupTutorialImagePanel.BannerObj> m_lstBanner = new List<NKCUIPopupTutorialImagePanel.BannerObj>();

		// Token: 0x04007E5F RID: 32351
		private List<NKCAssetInstanceData> m_lstAssetInstanceData = new List<NKCAssetInstanceData>();

		// Token: 0x020019FB RID: 6651
		// (Invoke) Token: 0x0600BAB3 RID: 47795
		public delegate void OnClose();

		// Token: 0x020019FC RID: 6652
		private struct BannerObj
		{
			// Token: 0x0400AD73 RID: 44403
			public int bannerIdx;

			// Token: 0x0400AD74 RID: 44404
			public int dataIdx;

			// Token: 0x0400AD75 RID: 44405
			public GameObject bannerObj;
		}
	}
}
