using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AB7 RID: 2743
	public class NKCPopupWorldMapBuildingInfo : NKCUIBase
	{
		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06007A17 RID: 31255 RVA: 0x0028A3ED File Offset: 0x002885ED
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06007A18 RID: 31256 RVA: 0x0028A3F0 File Offset: 0x002885F0
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_WORLDMAP_BUILDING;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06007A19 RID: 31257 RVA: 0x0028A3F7 File Offset: 0x002885F7
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x06007A1A RID: 31258 RVA: 0x0028A3FA File Offset: 0x002885FA
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007A1B RID: 31259 RVA: 0x0028A408 File Offset: 0x00288608
		public void Init()
		{
			if (this.m_csbtnClose != null)
			{
				this.m_csbtnClose.PointerClick.RemoveAllListeners();
				this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_csbtnBuild != null)
			{
				this.m_csbtnBuild.PointerClick.RemoveAllListeners();
				this.m_csbtnBuild.PointerClick.AddListener(new UnityAction(this.OnBtnBuild));
				NKCUtil.SetHotkey(this.m_csbtnBuild, HotkeyEventType.Confirm, null, false);
			}
			if (this.m_csbtnDestory != null)
			{
				this.m_csbtnDestory.PointerClick.RemoveAllListeners();
				this.m_csbtnDestory.PointerClick.AddListener(new UnityAction(this.OnBtnRemove));
			}
		}

		// Token: 0x06007A1C RID: 31260 RVA: 0x0028A4D4 File Offset: 0x002886D4
		public void OpenForBuild(int cityID, NKMWorldMapBuildingTemplet buildingTemplet, int currentBuildPoint, int cityBuildingCount, NKCPopupWorldMapBuildingInfo.OnButton onBuild)
		{
			this.dOnBuild = onBuild;
			this.dOnDestory = null;
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = buildingTemplet.GetLevelTemplet(1);
			this.SetDataCommon(buildingTemplet, levelTemplet, currentBuildPoint, cityBuildingCount);
			NKCUtil.SetGameobjectActive(this.m_csbtnDestory, false);
			NKCUtil.SetLabelText(this.m_lbBuild, NKCUtilString.GET_STRING_WORLDMAP_BUILDING_BUILD);
			NKCUtil.SetLabelText(this.m_lbBuildLocked, NKCUtilString.GET_STRING_WORLDMAP_BUILDING_BUILD);
			this.SetCost(levelTemplet, currentBuildPoint);
			this.SetBuildButtonAndAlert(cityID, levelTemplet, false);
			base.UIOpened(true);
		}

		// Token: 0x06007A1D RID: 31261 RVA: 0x0028A548 File Offset: 0x00288748
		public void OpenForManagement(int cityID, NKMWorldMapBuildingTemplet buildingTemplet, NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, int currentBuildPoint, int cityBuildingCount, NKCPopupWorldMapBuildingInfo.OnButton onLevelUp, NKCPopupWorldMapBuildingInfo.OnButton onRemove)
		{
			this.dOnBuild = onLevelUp;
			this.dOnDestory = onRemove;
			this.SetDataCommon(buildingTemplet, levelTemplet, currentBuildPoint, cityBuildingCount);
			NKCUtil.SetGameobjectActive(this.m_csbtnDestory, buildingTemplet.Key != 1);
			NKCUtil.SetLabelText(this.m_lbBuild, NKCUtilString.GET_STRING_LEVEL_UP);
			NKCUtil.SetLabelText(this.m_lbBuildLocked, NKCUtilString.GET_STRING_LEVEL_UP);
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet2 = buildingTemplet.GetLevelTemplet(levelTemplet.level + 1);
			this.SetCost(levelTemplet2, currentBuildPoint);
			this.SetBuildButtonAndAlert(cityID, levelTemplet2, true);
			base.UIOpened(true);
		}

		// Token: 0x06007A1E RID: 31262 RVA: 0x0028A5D0 File Offset: 0x002887D0
		private void SetCost(NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, int currentBuildPoint)
		{
			if (levelTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBuildCost, true);
				bool flag = levelTemplet.reqBuildingPoint > 0;
				NKCUtil.SetGameobjectActive(this.m_tagBuildPoint, flag);
				if (flag)
				{
					this.m_tagBuildPoint.SetData(911, levelTemplet.reqBuildingPoint, (long)currentBuildPoint, true, true, false);
				}
				using (IEnumerator<NKMWorldMapBuildingTemplet.LevelTemplet.CostItem> enumerator = levelTemplet.BuildCostItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMWorldMapBuildingTemplet.LevelTemplet.CostItem costItem = enumerator.Current;
						if (costItem.ItemID == 1)
						{
							this.m_tagCredit.SetData(costItem.ItemID, costItem.Count, NKCScenManager.CurrentUserData().GetCredit(), true, true, false);
						}
						else
						{
							Debug.LogError(string.Format("Unexpected Build Cost! building id {0}, level {1}", levelTemplet.id, levelTemplet.level));
						}
					}
					return;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objBuildCost, false);
			NKCUtil.SetGameobjectActive(this.m_tagBuildPoint, false);
		}

		// Token: 0x06007A1F RID: 31263 RVA: 0x0028A6C8 File Offset: 0x002888C8
		private void SetDataCommon(NKMWorldMapBuildingTemplet buildingTemplet, NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, int currentBuildPoint, int cityBuildingCount)
		{
			this.m_BuildingID = buildingTemplet.Key;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_world_map_renewal_building_icon", levelTemplet.iconPath, false);
			NKCUtil.SetImageSprite(this.m_imgBuilding, orLoadAssetResource, true);
			NKCUtil.SetLabelText(this.m_lbName, levelTemplet.GetName());
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, levelTemplet.level));
			NKCUtil.SetLabelText(this.m_lbMaxLevel, string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_MAX_LEVEL_ONE_PARAM, buildingTemplet.FindMaxLevel()));
			NKCUtil.SetLabelText(this.m_lbBuildingPoint, currentBuildPoint.ToString());
			NKCUtil.SetLabelText(this.m_lbDescription, levelTemplet.GetDesc());
			NKCUtil.SetLabelText(this.m_lbInformation, levelTemplet.GetInfo());
			NKCUtil.SetLabelText(this.m_lbSlotCount, string.Empty);
		}

		// Token: 0x06007A20 RID: 31264 RVA: 0x0028A798 File Offset: 0x00288998
		private void SetBuildButtonAndAlert(int cityID, NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, bool bUpgrade)
		{
			if (levelTemplet != null)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE;
				if (bUpgrade)
				{
					nkm_ERROR_CODE = NKMWorldMapManager.CanLevelUpBuilding(NKCScenManager.CurrentUserData(), cityID, levelTemplet.id);
				}
				else
				{
					nkm_ERROR_CODE = NKMWorldMapManager.CanBuild(NKCScenManager.CurrentUserData(), cityID, levelTemplet.id);
				}
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
				{
					this.m_csbtnBuild.UnLock(false);
				}
				else
				{
					this.m_csbtnBuild.Lock(false);
				}
				StringBuilder stringBuilder = new StringBuilder();
				NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(cityID);
				if (levelTemplet.reqCityLevel > 0)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					string text = "- " + string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REQ_CITY_LEVEL_ONE_PARAM, levelTemplet.reqCityLevel);
					if (cityData.level < levelTemplet.reqCityLevel)
					{
						text = this.GetRedString(text);
					}
					stringBuilder.Append(text);
				}
				if (levelTemplet.reqBuildingID != 0)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					NKMWorldMapBuildingTemplet.LevelTemplet cityBuildingTemplet = NKMWorldMapManager.GetCityBuildingTemplet(levelTemplet.reqBuildingID, levelTemplet.reqBuildingLevel);
					string text2 = "- " + string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REQ_BUILD_TWO_PARAM, cityBuildingTemplet.GetName(), cityBuildingTemplet.level);
					NKMWorldmapCityBuildingData buildingData = cityData.GetBuildingData(levelTemplet.reqBuildingID);
					if (buildingData == null || buildingData.level < levelTemplet.reqBuildingLevel)
					{
						text2 = this.GetRedString(text2);
					}
					stringBuilder.Append(text2);
				}
				if (levelTemplet.reqClearDiveId != 0)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(levelTemplet.reqClearDiveId);
					string text3 = "- " + string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_DIVE_CLEAR_ONE_PARAM, nkmdiveTemplet.IndexID);
					if (!NKCScenManager.CurrentUserData().CheckDiveHistory(levelTemplet.reqClearDiveId))
					{
						text3 = this.GetRedString(text3);
					}
					stringBuilder.Append(text3);
				}
				if (levelTemplet.notBuildingTogether != 0)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					NKMWorldMapBuildingTemplet.LevelTemplet cityBuildingTemplet2 = NKMWorldMapManager.GetCityBuildingTemplet(levelTemplet.notBuildingTogether, 1);
					string text4 = "- " + string.Format(NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_NOT_BUILDING_ONE_PARAM", false), cityBuildingTemplet2.GetName());
					if (cityData.GetBuildingData(levelTemplet.notBuildingTogether) != null)
					{
						text4 = this.GetRedString(text4);
					}
					stringBuilder.Append(text4);
				}
				NKCUtil.SetLabelText(this.m_lbAlert, stringBuilder.ToString());
				NKCUtil.SetGameobjectActive(this.m_objRootAlert, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRootAlert, false);
			this.m_csbtnBuild.Lock(false);
		}

		// Token: 0x06007A21 RID: 31265 RVA: 0x0028A9F6 File Offset: 0x00288BF6
		private string GetRedString(string str)
		{
			return "<color=#FF0000>" + str + "</color>";
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x0028AA08 File Offset: 0x00288C08
		private void OnBtnBuild()
		{
			NKCPopupWorldMapBuildingInfo.OnButton onButton = this.dOnBuild;
			if (onButton == null)
			{
				return;
			}
			onButton(this.m_BuildingID);
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x0028AA20 File Offset: 0x00288C20
		private void OnBtnRemove()
		{
			NKCPopupWorldMapBuildingInfo.OnButton onButton = this.dOnDestory;
			if (onButton == null)
			{
				return;
			}
			onButton(this.m_BuildingID);
		}

		// Token: 0x040066CE RID: 26318
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x040066CF RID: 26319
		public const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RENEWAL_BUILDING_INFO_POPUP";

		// Token: 0x040066D0 RID: 26320
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040066D1 RID: 26321
		[Header("빌딩 정보")]
		public Image m_imgBuilding;

		// Token: 0x040066D2 RID: 26322
		public Text m_lbBuildingPoint;

		// Token: 0x040066D3 RID: 26323
		public Text m_lbLevel;

		// Token: 0x040066D4 RID: 26324
		public Text m_lbName;

		// Token: 0x040066D5 RID: 26325
		public Text m_lbMaxLevel;

		// Token: 0x040066D6 RID: 26326
		public Text m_lbDescription;

		// Token: 0x040066D7 RID: 26327
		public Text m_lbInformation;

		// Token: 0x040066D8 RID: 26328
		[Header("아래쪽")]
		public NKCUIComStateButton m_csbtnBuild;

		// Token: 0x040066D9 RID: 26329
		public Text m_lbBuild;

		// Token: 0x040066DA RID: 26330
		public Text m_lbBuildLocked;

		// Token: 0x040066DB RID: 26331
		public NKCUIComStateButton m_csbtnDestory;

		// Token: 0x040066DC RID: 26332
		public GameObject m_objBuildCost;

		// Token: 0x040066DD RID: 26333
		public NKCUIItemCostSlot m_tagBuildPoint;

		// Token: 0x040066DE RID: 26334
		public NKCUIItemCostSlot m_tagCredit;

		// Token: 0x040066DF RID: 26335
		public Text m_lbSlotCount;

		// Token: 0x040066E0 RID: 26336
		[Header("건설 불가 경고")]
		public GameObject m_objRootAlert;

		// Token: 0x040066E1 RID: 26337
		public Text m_lbAlert;

		// Token: 0x040066E2 RID: 26338
		private NKCPopupWorldMapBuildingInfo.OnButton dOnBuild;

		// Token: 0x040066E3 RID: 26339
		private NKCPopupWorldMapBuildingInfo.OnButton dOnDestory;

		// Token: 0x040066E4 RID: 26340
		private int m_BuildingID;

		// Token: 0x02001814 RID: 6164
		// (Invoke) Token: 0x0600B50D RID: 46349
		public delegate void OnButton(int BuildingID);
	}
}
