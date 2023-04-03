using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC5 RID: 2757
	public class NKCUIWorldmapCityBuildPanel : MonoBehaviour
	{
		// Token: 0x06007B46 RID: 31558 RVA: 0x00291D54 File Offset: 0x0028FF54
		internal void Init()
		{
			if (null != this.m_LoopScroll)
			{
				this.m_LoopScroll.dOnGetObject += this.GetSlot;
				this.m_LoopScroll.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScroll.dOnProvideData += this.ProvideSlotData;
				NKCUtil.SetScrollHotKey(this.m_LoopScroll, null);
			}
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x00291DC0 File Offset: 0x0028FFC0
		private RectTransform GetSlot(int index)
		{
			NKCUIWorldMapCityBuildingSlot nkcuiworldMapCityBuildingSlot = UnityEngine.Object.Instantiate<NKCUIWorldMapCityBuildingSlot>(this.m_pfbBuildingSlot);
			nkcuiworldMapCityBuildingSlot.Init();
			nkcuiworldMapCityBuildingSlot.transform.localPosition = Vector3.zero;
			nkcuiworldMapCityBuildingSlot.transform.localScale = Vector3.one;
			return nkcuiworldMapCityBuildingSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x00291DF8 File Offset: 0x0028FFF8
		private void ReturnSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go);
		}

		// Token: 0x06007B49 RID: 31561 RVA: 0x00291E0C File Offset: 0x0029000C
		private void ProvideSlotData(Transform transform, int idx)
		{
			NKCUIWorldMapCityBuildingSlot component = transform.GetComponent<NKCUIWorldMapCityBuildingSlot>();
			if (component != null)
			{
				if (idx < this.m_lstBuildingData.Count)
				{
					NKMWorldmapCityBuildingData nkmworldmapCityBuildingData = this.m_lstBuildingData[idx];
					component.SetDataForManage(this.m_cityData, this.m_cityBuildingPointCache, nkmworldmapCityBuildingData.id, nkmworldmapCityBuildingData.level, new NKCUIWorldMapCityBuildingSlot.OnSelectSlot(this.OnSelectCityBuilding));
					bool flag = this.m_fxBuildingID > 0 && this.m_fxBuildingID == nkmworldmapCityBuildingData.id;
					component.SetFx(flag);
					if (flag)
					{
						this.m_fxBuildingID = 0;
						return;
					}
				}
				else
				{
					component.SetEmpty(new NKCUIWorldMapCityBuildingSlot.OnSelectSlot(this.OnSelectBuildNewBuilding));
					component.SetFx(false);
				}
			}
		}

		// Token: 0x06007B4A RID: 31562 RVA: 0x00291EB8 File Offset: 0x002900B8
		internal void SetData(NKMWorldMapCityData cityData)
		{
			this.m_cityData = cityData;
			this.m_cityBuildingPointCache = NKMWorldMapManager.GetUsableBuildPoint(this.m_cityData);
			if (!this.m_bSlotReady)
			{
				this.m_bSlotReady = true;
				this.m_LoopScroll.PrepareCells(0);
			}
			this.m_lstBuildingData.Clear();
			this.m_lstBuildingData.AddRange(cityData.worldMapCityBuildingDataMap.Values);
			this.m_lstBuildingData.Sort(new Comparison<NKMWorldmapCityBuildingData>(this.SortBuildingData));
			this.m_LoopScroll.TotalCount = this.m_lstBuildingData.Count + 1;
			this.m_LoopScroll.RefreshCells(false);
			NKCUtil.SetLabelText(this.m_lbBuildPoint, this.m_cityBuildingPointCache.ToString());
			NKCUtil.SetLabelText(this.m_lbBuildSlot, string.Empty);
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x00291F7C File Offset: 0x0029017C
		private int SortBuildingData(NKMWorldmapCityBuildingData a, NKMWorldmapCityBuildingData b)
		{
			NKMWorldMapBuildingTemplet.LevelTemplet cityBuildingTemplet = NKMWorldMapManager.GetCityBuildingTemplet(a.id, a.level);
			NKMWorldMapBuildingTemplet.LevelTemplet cityBuildingTemplet2 = NKMWorldMapManager.GetCityBuildingTemplet(b.id, b.level);
			if (cityBuildingTemplet == null && cityBuildingTemplet2 == null)
			{
				return 0;
			}
			if (cityBuildingTemplet == null && cityBuildingTemplet2 != null)
			{
				return 1;
			}
			if (cityBuildingTemplet != null && cityBuildingTemplet2 == null)
			{
				return -1;
			}
			if (cityBuildingTemplet.sortIndex == cityBuildingTemplet2.sortIndex)
			{
				return a.id.CompareTo(b.id);
			}
			return cityBuildingTemplet.sortIndex.CompareTo(cityBuildingTemplet2.sortIndex);
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x00291FF6 File Offset: 0x002901F6
		internal void CleanUp()
		{
			this.m_cityData = null;
			this.m_cityBuildingPointCache = 0;
			this.m_fxBuildingID = 0;
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x00292010 File Offset: 0x00290210
		private bool GetBuildingTemplets(int buildingID, out NKMWorldMapBuildingTemplet buildingTemplet, out NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet)
		{
			if (this.m_cityData == null)
			{
				buildingTemplet = null;
				levelTemplet = null;
				return false;
			}
			NKMWorldmapCityBuildingData buildingData = this.m_cityData.GetBuildingData(buildingID);
			if (buildingData == null)
			{
				buildingTemplet = null;
				levelTemplet = null;
				return false;
			}
			buildingTemplet = NKMWorldMapBuildingTemplet.Find(buildingID);
			levelTemplet = buildingTemplet.GetLevelTemplet(buildingData.level);
			return true;
		}

		// Token: 0x06007B4E RID: 31566 RVA: 0x00292060 File Offset: 0x00290260
		private void OnSelectCityBuilding(int buildingID)
		{
			NKMWorldMapBuildingTemplet buildingTemplet;
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet;
			if (!this.GetBuildingTemplets(buildingID, out buildingTemplet, out levelTemplet))
			{
				Debug.LogError("Target Building Not Found!");
				return;
			}
			NKCPopupWorldMapBuildingInfo popupWorldMapBuildingInfo = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().PopupWorldMapBuildingInfo;
			if (popupWorldMapBuildingInfo != null)
			{
				popupWorldMapBuildingInfo.OpenForManagement(this.m_cityData.cityID, buildingTemplet, levelTemplet, this.m_cityBuildingPointCache, this.m_lstBuildingData.Count, new NKCPopupWorldMapBuildingInfo.OnButton(this.TryLevelupBuilding), new NKCPopupWorldMapBuildingInfo.OnButton(this.TryRemoveBuilding));
			}
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x002920DC File Offset: 0x002902DC
		private void OnSelectBuildNewBuilding(int buildingID)
		{
			NKCPopupWorldMapNewBuildingList popupWorldMapNewBuildingList = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().PopupWorldMapNewBuildingList;
			if (popupWorldMapNewBuildingList != null)
			{
				popupWorldMapNewBuildingList.Open(this.m_cityData, this.m_cityBuildingPointCache, new NKCPopupWorldMapNewBuildingList.OnBuildingSelected(this.OnBuildTargetSelected));
			}
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x00292120 File Offset: 0x00290320
		private void TryLevelupBuilding(int buildingID)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_BUILD_LEVELUP_REQ(this.m_cityData.cityID, buildingID);
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x00292140 File Offset: 0x00290340
		private void TryRemoveBuilding(int buildingID)
		{
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet;
			NKMWorldMapBuildingTemplet.LevelTemplet targetBuildingTemplet;
			if (!this.GetBuildingTemplets(buildingID, out nkmworldMapBuildingTemplet, out targetBuildingTemplet))
			{
				return;
			}
			NKCPopupResourceWithdraw.Instance.OpenForWorldmapBuildingRemove(targetBuildingTemplet, delegate
			{
				this.OnConfirmRemoveBuilding(buildingID);
			});
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x0029218B File Offset: 0x0029038B
		private void OnConfirmRemoveBuilding(int buildingID)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_BUILD_EXPIRE_REQ(this.m_cityData.cityID, buildingID);
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x002921A8 File Offset: 0x002903A8
		private void OnBuildTargetSelected(int buildingID)
		{
			NKMWorldMapBuildingTemplet buildingTemplet = NKMWorldMapBuildingTemplet.Find(buildingID);
			NKCPopupWorldMapBuildingInfo popupWorldMapBuildingInfo = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().PopupWorldMapBuildingInfo;
			if (popupWorldMapBuildingInfo != null)
			{
				popupWorldMapBuildingInfo.OpenForBuild(this.m_cityData.cityID, buildingTemplet, this.m_cityBuildingPointCache, this.m_lstBuildingData.Count, new NKCPopupWorldMapBuildingInfo.OnButton(this.TryBuildBuilding));
			}
		}

		// Token: 0x06007B54 RID: 31572 RVA: 0x00292204 File Offset: 0x00290404
		private void TryBuildBuilding(int buildingID)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_BUILD_REQ(this.m_cityData.cityID, buildingID);
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x00292221 File Offset: 0x00290421
		public void SetFXBuildingID(int buildingID)
		{
			this.m_fxBuildingID = buildingID;
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x0029222C File Offset: 0x0029042C
		public RectTransform GetEmptySlot()
		{
			this.m_LoopScroll.SetIndexPosition(this.m_lstBuildingData.Count);
			NKCUIWorldMapCityBuildingSlot[] componentsInChildren = this.m_LoopScroll.content.GetComponentsInChildren<NKCUIWorldMapCityBuildingSlot>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] != null && componentsInChildren[i].m_objEmpty.activeSelf)
				{
					return componentsInChildren[i].GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x04006806 RID: 26630
		public NKCUIWorldMapCityBuildingSlot m_pfbBuildingSlot;

		// Token: 0x04006807 RID: 26631
		public Text m_lbBuildPoint;

		// Token: 0x04006808 RID: 26632
		public LoopScrollRect m_LoopScroll;

		// Token: 0x04006809 RID: 26633
		public Text m_lbBuildSlot;

		// Token: 0x0400680A RID: 26634
		private NKMWorldMapCityData m_cityData;

		// Token: 0x0400680B RID: 26635
		private int m_cityBuildingPointCache;

		// Token: 0x0400680C RID: 26636
		private List<NKMWorldmapCityBuildingData> m_lstBuildingData = new List<NKMWorldmapCityBuildingData>();

		// Token: 0x0400680D RID: 26637
		private bool m_bSlotReady;

		// Token: 0x0400680E RID: 26638
		private int m_fxBuildingID;
	}
}
