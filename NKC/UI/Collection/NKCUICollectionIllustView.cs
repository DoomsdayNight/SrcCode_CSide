using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C23 RID: 3107
	public class NKCUICollectionIllustView : NKCUIBase
	{
		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06008FD1 RID: 36817 RVA: 0x0030E33B File Offset: 0x0030C53B
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06008FD2 RID: 36818 RVA: 0x0030E342 File Offset: 0x0030C542
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06008FD3 RID: 36819 RVA: 0x0030E345 File Offset: 0x0030C545
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.BackButtonOnly;
			}
		}

		// Token: 0x06008FD4 RID: 36820 RVA: 0x0030E348 File Offset: 0x0030C548
		public override void OnBackButton()
		{
			base.OnBackButton();
		}

		// Token: 0x06008FD5 RID: 36821 RVA: 0x0030E350 File Offset: 0x0030C550
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			for (int i = 0; i < this.m_ListAssetInstance.Count; i++)
			{
				this.m_ListAssetInstance[i].m_Instant.transform.SetParent(null);
				this.m_ListAssetInstance[i].Unload();
			}
			this.m_ListAssetInstance.Clear();
		}

		// Token: 0x06008FD6 RID: 36822 RVA: 0x0030E3B8 File Offset: 0x0030C5B8
		public void Init()
		{
			if (null != this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS)
			{
				this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS.PointerClick.AddListener(new UnityAction(this.MovePrev));
			}
			if (null != this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT)
			{
				this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT.PointerClick.AddListener(new UnityAction(this.MoveNext));
			}
			if (null != this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_CLICK_AREA)
			{
				this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_CLICK_AREA.PointerClick.AddListener(new UnityAction(this.OnSubUI));
			}
		}

		// Token: 0x06008FD7 RID: 36823 RVA: 0x0030E443 File Offset: 0x0030C643
		public void Open(int CategoryID, int BGGroupID)
		{
			this.SetData(CategoryID, BGGroupID);
			base.UIOpened(true);
		}

		// Token: 0x06008FD8 RID: 36824 RVA: 0x0030E454 File Offset: 0x0030C654
		private void SetData(int CategoryID, int BGGroupID)
		{
			this.m_iSelectIndex = -1;
			NKCCollectionIllustTemplet illustTemplet = NKCCollectionManager.GetIllustTemplet(CategoryID);
			if (illustTemplet == null)
			{
				return;
			}
			if (illustTemplet.m_dicIllustData.ContainsKey(BGGroupID))
			{
				this.m_ListAssetInstance.Clear();
				NKCCollectionIllustData nkccollectionIllustData = illustTemplet.m_dicIllustData[BGGroupID];
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_ALBUM_VIEW_TEXT_01, nkccollectionIllustData.m_BGGroupTitle);
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_ALBUM_VIEW_TEXT_02, nkccollectionIllustData.m_BGGroupText);
				this.m_lstIllust.Clear();
				for (int i = 0; i < nkccollectionIllustData.m_FileData.Count; i++)
				{
					this.SetIllustData(nkccollectionIllustData.m_FileData[i].m_BGFileName, nkccollectionIllustData.m_FileData[i].m_BGFileName, nkccollectionIllustData.m_FileData[i].m_GameObjectBGAniName);
				}
				this.UpdateBG(0);
			}
		}

		// Token: 0x06008FD9 RID: 36825 RVA: 0x0030E520 File Offset: 0x0030C720
		private void SetIllustData(string bundleName, string assName, string BGAniName)
		{
			if (assName.Contains("AB_UI_NKM_UI_CUTSCEN"))
			{
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assName, false, null);
				if (nkcassetInstanceData.m_Instant != null)
				{
					nkcassetInstanceData.m_Instant.transform.SetParent(this.m_rt_NKM_UI_COLLECTION_ALBUM_VIEW_THUMBNAIL);
					NKCUtil.SetGameobjectActive(nkcassetInstanceData.m_Instant, false);
					this.PrepareBackground(nkcassetInstanceData.m_Instant);
					this.m_ListAssetInstance.Add(nkcassetInstanceData);
				}
				else
				{
					Debug.Log("Create 실패! 썸네일!");
				}
				this.m_lstIllust.Add(new NKCUICollectionIllustView.IllustData(true, this.m_ListAssetInstance.Count - 1, "", BGAniName));
				return;
			}
			this.m_lstIllust.Add(new NKCUICollectionIllustView.IllustData(false, -1, assName, ""));
		}

		// Token: 0x06008FDA RID: 36826 RVA: 0x0030E5D8 File Offset: 0x0030C7D8
		public static Sprite GetThumbnail(string AssetName)
		{
			string text = "AB_UI_NKM_UI_CUTSCEN_BG_" + AssetName;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(text, text, false);
			if (orLoadAssetResource == null)
			{
				Debug.LogError("can not found sprite " + AssetName);
			}
			return orLoadAssetResource;
		}

		// Token: 0x06008FDB RID: 36827 RVA: 0x0030E608 File Offset: 0x0030C808
		private void PrepareBackground(GameObject objBG)
		{
			RectTransform component = objBG.GetComponent<RectTransform>();
			if (component != null)
			{
				this.m_rtGoBG = component;
				float a = NKCUIManager.UIFrontCanvasSafeRectTransform.GetWidth() / 1920f;
				float b = NKCUIManager.UIFrontCanvasSafeRectTransform.GetHeight() / 1080f;
				float num = Mathf.Max(a, b);
				this.m_OrgGOScale = new Vector3(num, num, 1f);
				component.localScale = new Vector3(this.m_OrgGOScale.x * this.m_OffsetScale.x, this.m_OrgGOScale.y * this.m_OffsetScale.y, this.m_OrgGOScale.z * this.m_OffsetScale.z);
				component.offsetMin = new Vector2(0f, 0f);
				component.offsetMax = new Vector2(0f, 0f);
				component.anchoredPosition = this.m_OffsetPos;
				this.m_orgPos = component.anchoredPosition;
				return;
			}
			this.m_rtGoBG = null;
			objBG.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x06008FDC RID: 36828 RVA: 0x0030E724 File Offset: 0x0030C924
		private void UpdateBG(int selIdx)
		{
			if (selIdx == -1 || this.m_lstIllust.Count <= selIdx || this.m_iSelectIndex == selIdx)
			{
				return;
			}
			if (this.m_iSelectIndex != -1)
			{
				if (this.m_lstIllust[this.m_iSelectIndex].IsSpine)
				{
					NKCUtil.SetGameobjectActive(this.m_ListAssetInstance[this.m_lstIllust[this.m_iSelectIndex].InstanceIdx].m_Instant, false);
				}
				this.m_iSelectIndex = selIdx;
				if (this.m_lstIllust[this.m_iSelectIndex].IsSpine)
				{
					NKCUtil.SetGameobjectActive(this.m_ListAssetInstance[this.m_lstIllust[this.m_iSelectIndex].InstanceIdx].m_Instant, true);
					SkeletonGraphic componentInChildren = this.m_ListAssetInstance[this.m_lstIllust[this.m_iSelectIndex].InstanceIdx].m_Instant.GetComponentInChildren<SkeletonGraphic>();
					if (componentInChildren != null && this.m_lstIllust[this.m_iSelectIndex].AniName != "")
					{
						componentInChildren.AnimationState.SetAnimation(0, this.m_lstIllust[this.m_iSelectIndex].AniName, true);
					}
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_NKM_UI_COLLECTION_ILLUST_VIEW_THUMBNAIL, NKCUICollectionIllustView.GetThumbnail(this.m_lstIllust[this.m_iSelectIndex].AssetName), false);
				}
			}
			this.m_bCanMovePrev = false;
			this.m_bCanMoveNext = false;
			if (this.m_iSelectIndex - 1 >= 0)
			{
				this.m_bCanMovePrev = true;
			}
			if (this.m_iSelectIndex + 1 < this.m_lstIllust.Count)
			{
				this.m_bCanMoveNext = true;
			}
			this.UpdateMoveButtonColor();
		}

		// Token: 0x06008FDD RID: 36829 RVA: 0x0030E8D4 File Offset: 0x0030CAD4
		private void UpdateMoveButtonColor()
		{
			if (null != this.m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS)
			{
				if (this.m_bCanMovePrev)
				{
					this.m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS.color = Color.white;
				}
				else
				{
					this.m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS.color = NKCUtil.GetColor("#5D5D64");
				}
			}
			if (null != this.m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT)
			{
				if (this.m_bCanMoveNext)
				{
					this.m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT.color = Color.white;
					return;
				}
				this.m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT.color = NKCUtil.GetColor("#5D5D64");
			}
		}

		// Token: 0x06008FDE RID: 36830 RVA: 0x0030E95A File Offset: 0x0030CB5A
		public void MoveNext()
		{
			if (!this.m_bCanMoveNext)
			{
				return;
			}
			this.UpdateBG(this.m_iSelectIndex + 1);
		}

		// Token: 0x06008FDF RID: 36831 RVA: 0x0030E973 File Offset: 0x0030CB73
		public void MovePrev()
		{
			if (!this.m_bCanMovePrev)
			{
				return;
			}
			this.UpdateBG(this.m_iSelectIndex - 1);
		}

		// Token: 0x06008FE0 RID: 36832 RVA: 0x0030E98C File Offset: 0x0030CB8C
		public void OnSubUI()
		{
			if (!this.m_bShowSubUI)
			{
				this.m_bShowSubUI = true;
			}
			else
			{
				this.m_bShowSubUI = false;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_ALBUM_VIEW_TEXT_01.gameObject, this.m_bShowSubUI);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_ALBUM_VIEW_TEXT_02.gameObject, this.m_bShowSubUI);
			NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS.gameObject, this.m_bShowSubUI);
			NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT.gameObject, this.m_bShowSubUI);
		}

		// Token: 0x04007CC7 RID: 31943
		public RectTransform m_rt_NKM_UI_COLLECTION_ALBUM_VIEW_THUMBNAIL;

		// Token: 0x04007CC8 RID: 31944
		public Text m_NKM_UI_COLLECTION_ALBUM_VIEW_TEXT_01;

		// Token: 0x04007CC9 RID: 31945
		public Text m_NKM_UI_COLLECTION_ALBUM_VIEW_TEXT_02;

		// Token: 0x04007CCA RID: 31946
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS;

		// Token: 0x04007CCB RID: 31947
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT;

		// Token: 0x04007CCC RID: 31948
		public Image m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_PREVIOUS;

		// Token: 0x04007CCD RID: 31949
		public Image m_Img_NKM_UI_COLLECTION_ALBUM_VIEW_BUTTON_NEXT;

		// Token: 0x04007CCE RID: 31950
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_ALBUM_VIEW_CLICK_AREA;

		// Token: 0x04007CCF RID: 31951
		public Image m_NKM_UI_COLLECTION_ILLUST_VIEW_THUMBNAIL;

		// Token: 0x04007CD0 RID: 31952
		private int m_iSelectIndex = -1;

		// Token: 0x04007CD1 RID: 31953
		private List<NKCUICollectionIllustView.IllustData> m_lstIllust = new List<NKCUICollectionIllustView.IllustData>();

		// Token: 0x04007CD2 RID: 31954
		private List<NKCAssetInstanceData> m_ListAssetInstance = new List<NKCAssetInstanceData>();

		// Token: 0x04007CD3 RID: 31955
		private RectTransform m_rtGoBG;

		// Token: 0x04007CD4 RID: 31956
		private Vector2 m_orgPos = Vector2.zero;

		// Token: 0x04007CD5 RID: 31957
		private Vector2 m_OffsetPos = Vector2.zero;

		// Token: 0x04007CD6 RID: 31958
		private Vector3 m_OrgGOScale = Vector3.one;

		// Token: 0x04007CD7 RID: 31959
		private Vector3 m_OffsetScale = Vector3.one;

		// Token: 0x04007CD8 RID: 31960
		private bool m_bCanMovePrev;

		// Token: 0x04007CD9 RID: 31961
		private bool m_bCanMoveNext;

		// Token: 0x04007CDA RID: 31962
		private bool m_bShowSubUI = true;

		// Token: 0x020019E4 RID: 6628
		public struct IllustData
		{
			// Token: 0x0600BA7C RID: 47740 RVA: 0x0036DF85 File Offset: 0x0036C185
			public IllustData(bool spine, int idx, string name = "", string bgName = "")
			{
				this.IsSpine = spine;
				this.InstanceIdx = idx;
				this.AssetName = name;
				this.AniName = bgName;
			}

			// Token: 0x0400AD2A RID: 44330
			public bool IsSpine;

			// Token: 0x0400AD2B RID: 44331
			public int InstanceIdx;

			// Token: 0x0400AD2C RID: 44332
			public string AssetName;

			// Token: 0x0400AD2D RID: 44333
			public string AniName;
		}
	}
}
