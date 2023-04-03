using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C21 RID: 3105
	public class NKCUICollectionIllust : MonoBehaviour
	{
		// Token: 0x06008FC0 RID: 36800 RVA: 0x0030DCF0 File Offset: 0x0030BEF0
		public void Init(NKCUICollection.OnSyncCollectingData callBack)
		{
			if (null != this.m_LoopVerticalScrollFlexibleRect)
			{
				this.m_LoopVerticalScrollFlexibleRect.dOnGetObject += this.MakeIllustSlot;
				this.m_LoopVerticalScrollFlexibleRect.dOnReturnObject += this.ReturnIllustSlot;
				this.m_LoopVerticalScrollFlexibleRect.dOnProvideData += this.ProvideIllustSlotData;
			}
			if (callBack != null)
			{
				this.dOnSyncCollectingData = callBack;
			}
			this.m_bPrepareCollectionSlot = false;
			this.ReserveUISlot(40);
		}

		// Token: 0x06008FC1 RID: 36801 RVA: 0x0030DD69 File Offset: 0x0030BF69
		private void SyncCollectingUnitData()
		{
			if (this.dOnSyncCollectingData != null)
			{
				this.dOnSyncCollectingData(NKCUICollection.CollectionType.CT_ILLUST, this.m_iClearCount, this.m_iTotalCount);
			}
		}

		// Token: 0x06008FC2 RID: 36802 RVA: 0x0030DD8C File Offset: 0x0030BF8C
		private RectTransform MakeIllustSlot(int index)
		{
			if (this.m_stkCollectionSlotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_stkCollectionSlotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUICollectionIllustSlot nkcuicollectionIllustSlot = UnityEngine.Object.Instantiate<NKCUICollectionIllustSlot>(this.m_pfCollectionAlbumSlot);
			nkcuicollectionIllustSlot.Init();
			nkcuicollectionIllustSlot.transform.localPosition = Vector3.zero;
			nkcuicollectionIllustSlot.transform.localScale = Vector3.one;
			return nkcuicollectionIllustSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008FC3 RID: 36803 RVA: 0x0030DDF0 File Offset: 0x0030BFF0
		private void ReturnIllustSlot(Transform go)
		{
			NKCUICollectionIllustSlot component = go.GetComponent<NKCUICollectionIllustSlot>();
			List<RectTransform> rentalSlot = component.GetRentalSlot();
			for (int i = 0; i < rentalSlot.Count; i++)
			{
				rentalSlot[i].SetParent(this.m_rtIllustSlotPool);
				this.m_stkIllustSlotPool.Push(rentalSlot[i]);
			}
			component.ClearRentalList();
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rtNKM_UI_COLLECTION_ALBUM_Pool);
			this.m_stkCollectionSlotPool.Push(go.GetComponent<RectTransform>());
		}

		// Token: 0x06008FC4 RID: 36804 RVA: 0x0030DE6C File Offset: 0x0030C06C
		private void ProvideIllustSlotData(Transform tr, int idx)
		{
			NKCUICollectionIllustSlot component = tr.GetComponent<NKCUICollectionIllustSlot>();
			if (component == null)
			{
				return;
			}
			List<RectTransform> uislot = this.GetUISlot(this.m_lstIllustSlot[idx]);
			component.SetData(this.m_lstIllustSlot[idx], uislot, new NKCUICollectionIllust.OnIllustView(this.IllustView));
		}

		// Token: 0x06008FC5 RID: 36805 RVA: 0x0030DEBC File Offset: 0x0030C0BC
		private List<RectTransform> GetUISlot(int CategoryID)
		{
			List<RectTransform> list = new List<RectTransform>();
			NKCCollectionIllustTemplet illustTemplet = NKCCollectionManager.GetIllustTemplet(CategoryID);
			if (illustTemplet != null)
			{
				for (int i = 0; i < illustTemplet.m_dicIllustData.Count; i++)
				{
					if (this.m_stkIllustSlotPool.Count > 0)
					{
						RectTransform item = this.m_stkIllustSlotPool.Pop();
						list.Add(item);
					}
					else
					{
						NKCUIIllustSlot nkcuiillustSlot = UnityEngine.Object.Instantiate<NKCUIIllustSlot>(this.m_pfUISlot);
						nkcuiillustSlot.transform.localPosition = Vector3.zero;
						nkcuiillustSlot.transform.localScale = Vector3.one;
						RectTransform component = nkcuiillustSlot.GetComponent<RectTransform>();
						list.Add(component);
					}
				}
			}
			return list;
		}

		// Token: 0x06008FC6 RID: 36806 RVA: 0x0030DF50 File Offset: 0x0030C150
		private void ReserveUISlot(int size)
		{
			for (int i = 0; i < size; i++)
			{
				NKCUIIllustSlot nkcuiillustSlot = UnityEngine.Object.Instantiate<NKCUIIllustSlot>(this.m_pfUISlot);
				nkcuiillustSlot.transform.localPosition = Vector3.zero;
				nkcuiillustSlot.transform.localScale = Vector3.one;
				RectTransform component = nkcuiillustSlot.GetComponent<RectTransform>();
				this.m_stkIllustSlotPool.Push(component);
			}
		}

		// Token: 0x06008FC7 RID: 36807 RVA: 0x0030DFA8 File Offset: 0x0030C1A8
		public void IllustView(int CategoryID, int BGGroupID)
		{
			if (null == this.m_NKCUICollectionIllustView)
			{
				NKCUICollectionIllust.m_AssetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(this.ILLUST_VIEW_BUNDLE_NAME, this.ILLUST_VIEW_ASSET_NAME, false, null);
				if (NKCUICollectionIllust.m_AssetInstanceData.m_Instant != null)
				{
					RectTransform component = NKCUIManager.OpenUI("NUF_COMMON_Panel").GetComponent<RectTransform>();
					NKCUICollectionIllust.m_AssetInstanceData.m_Instant.transform.SetParent(component.transform, false);
					this.m_NKCUICollectionIllustView = NKCUICollectionIllust.m_AssetInstanceData.m_Instant.GetComponent<NKCUICollectionIllustView>();
					this.m_NKCUICollectionIllustView.Init();
					this.m_NKCUICollectionIllustView.Open(CategoryID, BGGroupID);
					return;
				}
			}
			else
			{
				this.m_NKCUICollectionIllustView.Open(CategoryID, BGGroupID);
			}
		}

		// Token: 0x06008FC8 RID: 36808 RVA: 0x0030E058 File Offset: 0x0030C258
		public void Open()
		{
			this.m_iClearCount = 0;
			this.m_iTotalCount = 0;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				Debug.Log("NKCUICollectionAlbum - Curret User Data is null");
				return;
			}
			this.m_lstIllustSlot.Clear();
			foreach (KeyValuePair<int, NKCCollectionIllustTemplet> keyValuePair in NKCCollectionManager.GetIllustData())
			{
				this.m_lstIllustSlot.Add(keyValuePair.Key);
				foreach (KeyValuePair<int, NKCCollectionIllustData> keyValuePair2 in keyValuePair.Value.m_dicIllustData)
				{
					NKMUserData cNKMUserData = nkmuserData;
					UnlockInfo unlockInfo = new UnlockInfo(keyValuePair2.Value.m_UnlockReqType, keyValuePair2.Value.m_UnlockReqValue);
					bool flag = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
					keyValuePair2.Value.SetClearState(flag);
					if (flag)
					{
						this.m_iClearCount++;
					}
					this.m_iTotalCount++;
				}
			}
			if (!this.m_bPrepareCollectionSlot)
			{
				this.m_bPrepareCollectionSlot = true;
				this.m_LoopVerticalScrollFlexibleRect.TotalCount = this.m_lstIllustSlot.Count;
				this.m_LoopVerticalScrollFlexibleRect.PrepareCells(0);
				this.m_LoopVerticalScrollFlexibleRect.velocity = new Vector2(0f, 0f);
				this.m_LoopVerticalScrollFlexibleRect.SetIndexPosition(0);
			}
			this.SyncCollectingUnitData();
		}

		// Token: 0x06008FC9 RID: 36809 RVA: 0x0030E1E0 File Offset: 0x0030C3E0
		public void Clear()
		{
			if (NKCUICollectionIllust.m_AssetInstanceData != null)
			{
				NKCUICollectionIllust.m_AssetInstanceData.Unload();
			}
		}

		// Token: 0x04007CB3 RID: 31923
		public NKCUICollectionIllustSlot m_pfCollectionAlbumSlot;

		// Token: 0x04007CB4 RID: 31924
		public LoopVerticalScrollFlexibleRect m_LoopVerticalScrollFlexibleRect;

		// Token: 0x04007CB5 RID: 31925
		public RectTransform m_rtNKM_UI_COLLECTION_ALBUM_Pool;

		// Token: 0x04007CB6 RID: 31926
		private Stack<RectTransform> m_stkCollectionSlotPool = new Stack<RectTransform>();

		// Token: 0x04007CB7 RID: 31927
		private NKCUICollection.OnSyncCollectingData dOnSyncCollectingData;

		// Token: 0x04007CB8 RID: 31928
		public NKCUIIllustSlot m_pfUISlot;

		// Token: 0x04007CB9 RID: 31929
		public RectTransform m_rtIllustSlotPool;

		// Token: 0x04007CBA RID: 31930
		private Stack<RectTransform> m_stkIllustSlotPool = new Stack<RectTransform>();

		// Token: 0x04007CBB RID: 31931
		private int m_iClearCount;

		// Token: 0x04007CBC RID: 31932
		private int m_iTotalCount;

		// Token: 0x04007CBD RID: 31933
		private string ILLUST_VIEW_ASSET_NAME = "NKM_UI_COLLECTION_ILLUST_VIEW";

		// Token: 0x04007CBE RID: 31934
		private string ILLUST_VIEW_BUNDLE_NAME = "ab_ui_nkm_ui_collection";

		// Token: 0x04007CBF RID: 31935
		private NKCUICollectionIllustView m_NKCUICollectionIllustView;

		// Token: 0x04007CC0 RID: 31936
		private static NKCAssetInstanceData m_AssetInstanceData;

		// Token: 0x04007CC1 RID: 31937
		private bool m_bPrepareCollectionSlot;

		// Token: 0x04007CC2 RID: 31938
		private List<int> m_lstIllustSlot = new List<int>();

		// Token: 0x020019E3 RID: 6627
		// (Invoke) Token: 0x0600BA79 RID: 47737
		public delegate void OnIllustView(int CatagoryID, int BGGroupID);
	}
}
