using System;
using System.Text;
using ClientPacket.WorldMap;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC0 RID: 2752
	public class NKCUIWorldMapCityBuildingSlot : MonoBehaviour
	{
		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06007AEB RID: 31467 RVA: 0x0028F49C File Offset: 0x0028D69C
		public int BuildingID
		{
			get
			{
				return this.m_BuildingID;
			}
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x0028F4A4 File Offset: 0x0028D6A4
		public void Init()
		{
			if (this.m_csbtnSlot != null)
			{
				this.m_csbtnSlot.PointerClick.RemoveAllListeners();
				this.m_csbtnSlot.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x0028F4E0 File Offset: 0x0028D6E0
		public void SetEmpty(NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			NKCUtil.SetGameobjectActive(this.m_objRootManage, false);
			NKCUtil.SetGameobjectActive(this.m_objRootBuild, false);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
			NKCUtil.SetGameobjectActive(this.m_objCanLevelUp, false);
			NKCUtil.SetGameobjectActive(this.m_objReqCityLevel, false);
			NKCUtil.SetGameobjectActive(this.m_objRootPrice, false);
			NKCUtil.SetGameobjectActive(this.m_objLock, false);
			this.dOnSelectSlot = onSelectSlot;
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x0028F548 File Offset: 0x0028D748
		public void SetDataCommonOnly(int BuildingID, int Level, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(BuildingID);
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(Level);
			this.SetDataCommonOnly(nkmworldMapBuildingTemplet, levelTemplet, onSelectSlot);
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x0028F570 File Offset: 0x0028D770
		public void SetDataCommonOnly(NKMWorldMapBuildingTemplet buildingTemplet, NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			this.SetDataCommon(buildingTemplet, levelTemplet, onSelectSlot);
			NKCUtil.SetGameobjectActive(this.m_objRootManage, false);
			NKCUtil.SetGameobjectActive(this.m_objRootBuild, false);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_objCanLevelUp, false);
			NKCUtil.SetGameobjectActive(this.m_objReqCityLevel, false);
			NKCUtil.SetGameobjectActive(this.m_objRootPrice, false);
			NKCUtil.SetGameobjectActive(this.m_objLock, false);
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x0028F5DC File Offset: 0x0028D7DC
		public void SetDataForManage(NKMWorldMapCityData cityData, int cityBuildPointLeft, int BuildingID, int Level, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(BuildingID);
			if (nkmworldMapBuildingTemplet == null)
			{
				Log.Error(string.Format("Building ID: {0} is not exist in WORLDMAP_CITY_BUILDING templet", BuildingID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/WorldMap/NKCUIWorldMapCityBuildingSlot.cs", 95);
				return;
			}
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(Level);
			this.SetDataForManage(cityData, cityBuildPointLeft, nkmworldMapBuildingTemplet, levelTemplet, onSelectSlot);
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x0028F628 File Offset: 0x0028D828
		public void SetDataForManage(NKMWorldMapCityData cityData, int cityBuildPointLeft, NKMWorldMapBuildingTemplet buildingTemplet, NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			this.SetDataCommon(buildingTemplet, levelTemplet, onSelectSlot);
			NKCUtil.SetGameobjectActive(this.m_objRootManage, true);
			NKCUtil.SetGameobjectActive(this.m_objRootBuild, false);
			NKCUtil.SetGameobjectActive(this.m_objRootPrice, false);
			NKCUtil.SetGameobjectActive(this.m_objLock, false);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			bool bValue = NKMWorldMapManager.CanLevelUpBuilding(NKCScenManager.CurrentUserData(), cityData.cityID, buildingTemplet.Key) == NKM_ERROR_CODE.NEC_OK;
			NKCUtil.SetGameobjectActive(this.m_objCanLevelUp, bValue);
			NKCUtil.SetGameobjectActive(this.m_objReqCityLevel, false);
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x0028F6B0 File Offset: 0x0028D8B0
		public void SetDataForBuild(NKMWorldMapCityData cityData, int cityBuildPointLeft, int BuildingID, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			NKMWorldMapBuildingTemplet buildingTemplet = NKMWorldMapBuildingTemplet.Find(BuildingID);
			this.SetDataForBuild(cityData, cityBuildPointLeft, buildingTemplet, onSelectSlot);
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x0028F6D0 File Offset: 0x0028D8D0
		public void SetDataForBuild(NKMWorldMapCityData cityData, int cityBuildPointLeft, NKMWorldMapBuildingTemplet buildingTemplet, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = buildingTemplet.GetLevelTemplet(1);
			if (levelTemplet == null)
			{
				return;
			}
			this.SetDataCommon(buildingTemplet, levelTemplet, onSelectSlot);
			NKCUtil.SetGameobjectActive(this.m_objRootManage, false);
			NKCUtil.SetGameobjectActive(this.m_objRootBuild, true);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_objCanLevelUp, false);
			NKCUtil.SetGameobjectActive(this.m_objReqCityLevel, false);
			NKCUtil.SetGameobjectActive(this.m_objRootPrice, true);
			this.m_tagReqBuildPoint.SetDataByHaveCount(levelTemplet.reqBuildingPoint, cityBuildPointLeft, false, true);
			foreach (NKMWorldMapBuildingTemplet.LevelTemplet.CostItem costItem in levelTemplet.BuildCostItems)
			{
				if (costItem.ItemID == 1)
				{
					this.m_tagCreditPrice.SetData(costItem.ItemID, costItem.Count, false, true, false);
				}
				else
				{
					Debug.LogError(string.Format("Unexpected Build Cost! building id {0}, level {1}", levelTemplet.id, levelTemplet.level));
				}
			}
			string msg;
			if (this.CanBuild(cityData, levelTemplet, out msg))
			{
				NKCUtil.SetGameobjectActive(this.m_objLock, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objLock, true);
			NKCUtil.SetLabelText(this.m_lbLockReason, msg);
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x0028F808 File Offset: 0x0028DA08
		private bool CanBuild(NKMWorldMapCityData cityData, NKMWorldMapBuildingTemplet.LevelTemplet targetBuildingLevelTemplet, out string reason)
		{
			bool result = true;
			StringBuilder stringBuilder = new StringBuilder();
			if (cityData.GetBuildingData(targetBuildingLevelTemplet.id) != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_ALREADY_BUILD);
				result = false;
			}
			if (cityData.level < targetBuildingLevelTemplet.reqCityLevel)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REQ_CITY_LEVEL_ONE_PARAM, targetBuildingLevelTemplet.reqCityLevel));
				result = false;
			}
			if (targetBuildingLevelTemplet.reqBuildingID != 0)
			{
				NKMWorldmapCityBuildingData buildingData = cityData.GetBuildingData(targetBuildingLevelTemplet.reqBuildingID);
				if (buildingData == null || buildingData.level < targetBuildingLevelTemplet.reqBuildingLevel)
				{
					NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(targetBuildingLevelTemplet.reqBuildingID);
					if (nkmworldMapBuildingTemplet == null)
					{
						Debug.LogError("Required Building Not Found!!");
					}
					NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(targetBuildingLevelTemplet.reqBuildingLevel);
					if (levelTemplet == null)
					{
						Debug.LogError("Required Building Not Found!!");
					}
					if (targetBuildingLevelTemplet.reqBuildingLevel > 1)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.AppendLine();
						}
						stringBuilder.Append(string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REQ_BUILD_TWO_PARAM, levelTemplet.GetName(), targetBuildingLevelTemplet.reqBuildingLevel));
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.AppendLine();
						}
						stringBuilder.Append(string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REQ_BUILDING_ONE_PARAM, levelTemplet.GetName()));
					}
					result = false;
				}
			}
			if (targetBuildingLevelTemplet.reqClearDiveId != 0 && !NKCScenManager.CurrentUserData().CheckDiveHistory(targetBuildingLevelTemplet.reqClearDiveId))
			{
				NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(targetBuildingLevelTemplet.reqClearDiveId);
				if (nkmdiveTemplet != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_DIVE_CLEAR_ONE_PARAM, nkmdiveTemplet.IndexID));
				}
				result = false;
			}
			if (targetBuildingLevelTemplet.notBuildingTogether != 0 && cityData.GetBuildingData(targetBuildingLevelTemplet.notBuildingTogether) != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				NKMWorldMapBuildingTemplet.LevelTemplet cityBuildingTemplet = NKMWorldMapManager.GetCityBuildingTemplet(targetBuildingLevelTemplet.notBuildingTogether, 1);
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WORLDMAP_NOT_BUILDING_TOGETHER", false), cityBuildingTemplet.GetName()));
				result = false;
			}
			reason = stringBuilder.ToString();
			return result;
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x0028FA04 File Offset: 0x0028DC04
		private void SetDataCommon(NKMWorldMapBuildingTemplet buildingTemplet, NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet, NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot)
		{
			if (levelTemplet == null)
			{
				Debug.Log("NKMWorldMapBuildingTemplet.LevelTemplet is null");
				return;
			}
			this.m_BuildingID = levelTemplet.id;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_world_map_renewal_building_icon", levelTemplet.iconPath, false);
			NKCUtil.SetImageSprite(this.m_imgIcon, orLoadAssetResource, true);
			NKCUtil.SetLabelText(this.m_lbName, levelTemplet.GetName());
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, levelTemplet.level));
			NKCUtil.SetGameobjectActive(this.m_objMaxLevel, buildingTemplet.GetLevelTemplet(levelTemplet.level + 1) == null);
			NKCUtil.SetLabelText(this.m_lbInformation, levelTemplet.GetInfo());
			this.dOnSelectSlot = onSelectSlot;
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x0028FAB0 File Offset: 0x0028DCB0
		public void SetFx(bool bShow)
		{
			NKCUtil.SetGameobjectActive(this.m_trEffect, false);
			if (bShow)
			{
				if (this.m_buildFX == null)
				{
					this.m_buildFX = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_UI_BRANCH_BUILD", "AB_FX_UI_BRANCH_BUILD", false, null);
					this.m_buildFX.m_Instant.transform.SetParent(this.m_trEffect);
					this.m_buildFX.m_Instant.transform.localPosition = Vector3.zero;
					this.m_buildFX.m_Instant.transform.localScale = Vector3.one;
				}
				NKCUtil.SetGameobjectActive(this.m_trEffect, bShow);
				NKCSoundManager.PlaySound("FX_UI_CONTRACT_SLOT_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
				return;
			}
			if (this.m_buildFX != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_buildFX);
				this.m_buildFX = null;
			}
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x0028FB8A File Offset: 0x0028DD8A
		private void OnClick()
		{
			NKCUIWorldMapCityBuildingSlot.OnSelectSlot onSelectSlot = this.dOnSelectSlot;
			if (onSelectSlot == null)
			{
				return;
			}
			onSelectSlot(this.m_BuildingID);
		}

		// Token: 0x0400679D RID: 26525
		public NKCUIComStateButton m_csbtnSlot;

		// Token: 0x0400679E RID: 26526
		public Image m_imgIcon;

		// Token: 0x0400679F RID: 26527
		public Text m_lbName;

		// Token: 0x040067A0 RID: 26528
		public Text m_lbLevel;

		// Token: 0x040067A1 RID: 26529
		public GameObject m_objMaxLevel;

		// Token: 0x040067A2 RID: 26530
		public Text m_lbInformation;

		// Token: 0x040067A3 RID: 26531
		[Header("관리시")]
		public GameObject m_objRootManage;

		// Token: 0x040067A4 RID: 26532
		public GameObject m_objEmpty;

		// Token: 0x040067A5 RID: 26533
		public GameObject m_objCanLevelUp;

		// Token: 0x040067A6 RID: 26534
		public GameObject m_objReqCityLevel;

		// Token: 0x040067A7 RID: 26535
		public Text m_lbReqCityLevel;

		// Token: 0x040067A8 RID: 26536
		public Transform m_trEffect;

		// Token: 0x040067A9 RID: 26537
		[Header("건설시")]
		public GameObject m_objRootBuild;

		// Token: 0x040067AA RID: 26538
		public GameObject m_objRootPrice;

		// Token: 0x040067AB RID: 26539
		public NKCUIPriceTag m_tagReqBuildPoint;

		// Token: 0x040067AC RID: 26540
		public NKCUIPriceTag m_tagCreditPrice;

		// Token: 0x040067AD RID: 26541
		public GameObject m_objLock;

		// Token: 0x040067AE RID: 26542
		public Text m_lbLockReason;

		// Token: 0x040067AF RID: 26543
		private int m_BuildingID;

		// Token: 0x040067B0 RID: 26544
		private NKCUIWorldMapCityBuildingSlot.OnSelectSlot dOnSelectSlot;

		// Token: 0x040067B1 RID: 26545
		private NKCAssetInstanceData m_buildFX;

		// Token: 0x0200182F RID: 6191
		// (Invoke) Token: 0x0600B54E RID: 46414
		public delegate void OnSelectSlot(int m_BuildingID);
	}
}
