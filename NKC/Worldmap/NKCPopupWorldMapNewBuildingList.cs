using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000ABB RID: 2747
	public class NKCPopupWorldMapNewBuildingList : NKCUIBase
	{
		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x0028D50F File Offset: 0x0028B70F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06007A6F RID: 31343 RVA: 0x0028D512 File Offset: 0x0028B712
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_WORLDMAP_NEW_BUILDING;
			}
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x0028D519 File Offset: 0x0028B719
		public override void CloseInternal()
		{
			this.m_cityData = null;
			base.gameObject.SetActive(false);
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06007A71 RID: 31345 RVA: 0x0028D52E File Offset: 0x0028B72E
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x0028D534 File Offset: 0x0028B734
		public void Init()
		{
			if (null != this.m_LoopScroll)
			{
				this.m_LoopScroll.dOnGetObject += this.GetSlot;
				this.m_LoopScroll.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScroll.dOnProvideData += this.ProvideSlotData;
				this.m_LoopScroll.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScroll, null);
			}
			this.m_lstBuildingTemplet.Clear();
			foreach (NKMWorldMapBuildingTemplet item in NKMTempletContainer<NKMWorldMapBuildingTemplet>.Values)
			{
				this.m_lstBuildingTemplet.Add(item);
			}
			this.m_lstBuildingTemplet.Sort(new Comparison<NKMWorldMapBuildingTemplet>(this.SortBuildingData));
			if (this.m_csbtnClose != null)
			{
				this.m_csbtnClose.PointerClick.RemoveAllListeners();
				this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x0028D64C File Offset: 0x0028B84C
		private int SortBuildingData(NKMWorldMapBuildingTemplet a, NKMWorldMapBuildingTemplet b)
		{
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = a.GetLevelTemplet(1);
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet2 = b.GetLevelTemplet(1);
			if (levelTemplet == null && levelTemplet2 == null)
			{
				return 0;
			}
			if (levelTemplet == null && levelTemplet2 != null)
			{
				return 1;
			}
			if (levelTemplet != null && levelTemplet2 == null)
			{
				return -1;
			}
			if (levelTemplet.sortIndex == levelTemplet2.sortIndex)
			{
				return a.Key.CompareTo(b.Key);
			}
			return levelTemplet.sortIndex.CompareTo(levelTemplet2.sortIndex);
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x0028D6B5 File Offset: 0x0028B8B5
		private RectTransform GetSlot(int index)
		{
			NKCUIWorldMapCityBuildingSlot nkcuiworldMapCityBuildingSlot = UnityEngine.Object.Instantiate<NKCUIWorldMapCityBuildingSlot>(this.m_pfbBuildingSlot);
			nkcuiworldMapCityBuildingSlot.Init();
			nkcuiworldMapCityBuildingSlot.transform.localPosition = Vector3.zero;
			nkcuiworldMapCityBuildingSlot.transform.localScale = Vector3.one;
			return nkcuiworldMapCityBuildingSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x0028D6ED File Offset: 0x0028B8ED
		private void ReturnSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go);
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x0028D704 File Offset: 0x0028B904
		private void ProvideSlotData(Transform transform, int idx)
		{
			NKCUIWorldMapCityBuildingSlot component = transform.GetComponent<NKCUIWorldMapCityBuildingSlot>();
			if (component != null && idx < this.m_lstBuildingTemplet.Count)
			{
				NKMWorldMapBuildingTemplet buildingTemplet = this.m_lstBuildingTemplet[idx];
				component.SetDataForBuild(this.m_cityData, this.m_cityBuildingPointLeft, buildingTemplet, new NKCUIWorldMapCityBuildingSlot.OnSelectSlot(this.SlotSelected));
			}
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x0028D75C File Offset: 0x0028B95C
		public void Open(NKMWorldMapCityData cityData, int cityBuildingPointLeft, NKCPopupWorldMapNewBuildingList.OnBuildingSelected onBuildingSelected)
		{
			this.m_cityData = cityData;
			this.m_cityBuildingPointLeft = cityBuildingPointLeft;
			this.dOnBuildingSelected = onBuildingSelected;
			base.gameObject.SetActive(true);
			this.m_LoopScroll.TotalCount = this.m_lstBuildingTemplet.Count;
			this.m_LoopScroll.RefreshCells(false);
			NKCUtil.SetLabelText(this.m_lbBuildingPointLeft, cityBuildingPointLeft.ToString());
			NKCUtil.SetLabelText(this.m_lbBuildingSlot, string.Empty);
			base.UIOpened(true);
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x0028D7D5 File Offset: 0x0028B9D5
		private void SlotSelected(int buildingID)
		{
			NKCPopupWorldMapNewBuildingList.OnBuildingSelected onBuildingSelected = this.dOnBuildingSelected;
			if (onBuildingSelected == null)
			{
				return;
			}
			onBuildingSelected(buildingID);
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x0028D7E8 File Offset: 0x0028B9E8
		public RectTransform GetBuildingSlot(int buildingID)
		{
			int num = this.m_lstBuildingTemplet.FindIndex((NKMWorldMapBuildingTemplet v) => v.GetLevelTemplet(1).id == buildingID);
			if (num < 0)
			{
				return null;
			}
			this.m_LoopScroll.SetIndexPosition(num);
			NKCUIWorldMapCityBuildingSlot[] componentsInChildren = this.m_LoopScroll.content.GetComponentsInChildren<NKCUIWorldMapCityBuildingSlot>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] != null && componentsInChildren[i].BuildingID == buildingID)
				{
					return componentsInChildren[i].GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x04006754 RID: 26452
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x04006755 RID: 26453
		public const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RENEWAL_BUILDING_LIST_POPUP";

		// Token: 0x04006756 RID: 26454
		public NKCUIWorldMapCityBuildingSlot m_pfbBuildingSlot;

		// Token: 0x04006757 RID: 26455
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006758 RID: 26456
		public Text m_lbBuildingPointLeft;

		// Token: 0x04006759 RID: 26457
		public LoopScrollRect m_LoopScroll;

		// Token: 0x0400675A RID: 26458
		public Text m_lbBuildingSlot;

		// Token: 0x0400675B RID: 26459
		private NKMWorldMapCityData m_cityData;

		// Token: 0x0400675C RID: 26460
		private int m_cityBuildingPointLeft;

		// Token: 0x0400675D RID: 26461
		private NKCPopupWorldMapNewBuildingList.OnBuildingSelected dOnBuildingSelected;

		// Token: 0x0400675E RID: 26462
		private List<NKMWorldMapBuildingTemplet> m_lstBuildingTemplet = new List<NKMWorldMapBuildingTemplet>();

		// Token: 0x02001827 RID: 6183
		// (Invoke) Token: 0x0600B532 RID: 46386
		public delegate void OnBuildingSelected(int buildingID);
	}
}
