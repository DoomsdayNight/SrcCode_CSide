using System;
using ClientPacket.WorldMap;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AB8 RID: 2744
	public class NKCPopupWorldMapCityUnlock : NKCUIBase
	{
		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x0028AA40 File Offset: 0x00288C40
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06007A26 RID: 31270 RVA: 0x0028AA43 File Offset: 0x00288C43
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_WORLDMAP_BUILDING;
			}
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x0028AA4A File Offset: 0x00288C4A
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x0028AA58 File Offset: 0x00288C58
		public void InitUI()
		{
			if (this.m_csbtnBuildCash != null)
			{
				this.m_csbtnBuildCash.PointerClick.RemoveAllListeners();
				this.m_csbtnBuildCash.PointerClick.AddListener(new UnityAction(this.OnBuildCash));
			}
			else
			{
				Debug.LogError("BuildCityCashBtn Not Connected!");
			}
			if (this.m_csbtnBuildCredit != null)
			{
				this.m_csbtnBuildCredit.PointerClick.RemoveAllListeners();
				this.m_csbtnBuildCredit.PointerClick.AddListener(new UnityAction(this.OnBuildCredit));
			}
			else
			{
				Debug.LogError("BuildCityCreditBtn Not Connected!");
			}
			if (this.m_csbtnClose != null)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnClose, false);
			}
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x0028AB0C File Offset: 0x00288D0C
		public void Open(int cityID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMWorldMapData worldmapData = nkmuserData.m_WorldmapData;
			if (worldmapData.IsCityUnlocked(cityID))
			{
				Debug.LogWarning("Logic Warning : Trying to unlock already unlocked city");
				return;
			}
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(cityID);
			if (cityTemplet == null)
			{
				Debug.LogError(string.Format("City Templet not found! (id : {0})", cityID));
				return;
			}
			this.m_CityID = cityID;
			NKCUtil.SetLabelText(this.m_lbName, cityTemplet.GetName());
			NKCUtil.SetLabelText(this.m_lbDescription, cityTemplet.GetDesc());
			this.SetButtonData(nkmuserData);
			int unlockedCityCount = worldmapData.GetUnlockedCityCount();
			NKCUtil.SetLabelText(this.m_lbCurrentCityCount, string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_CITY_COUNT_ONE_PARAM, unlockedCityCount));
			base.UIOpened(true);
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x0028ABB4 File Offset: 0x00288DB4
		private void SetButtonData(NKMUserData userData)
		{
			NKMWorldMapData worldmapData = userData.m_WorldmapData;
			int unlockedCityCount = worldmapData.GetUnlockedCityCount();
			if (unlockedCityCount < NKMWorldMapManager.GetPossibleCityCount(userData.UserLevel))
			{
				this.m_csbtnBuildCredit.UnLock(false);
				this.m_tagCreditCost.SetData(1, NKMWorldMapManager.GetCityOpenCost(worldmapData, false), false, false, false);
			}
			else
			{
				this.m_csbtnBuildCredit.Lock(false);
				int nextAreaUnlockLevel = NKMWorldMapManager.GetNextAreaUnlockLevel(unlockedCityCount);
				NKCUtil.SetLabelText(this.m_lbCreditReqLevel, string.Format(NKCUtilString.GET_STRING_WORLDMAP_BUILDING_CREDIT_REQ_LEVEL_ONE_PARAM, nextAreaUnlockLevel));
				this.m_tagCreditCostDisable.SetData(1, NKMWorldMapManager.GetCityOpenCost(worldmapData, false), false, false, false);
			}
			this.m_tagCashCost.SetData(101, NKMWorldMapManager.GetCityOpenCost(worldmapData, true), false, false, false);
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x0028AC60 File Offset: 0x00288E60
		private void OnBuildCash()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapData worldmapData = nkmuserData.m_WorldmapData;
			if (worldmapData == null)
			{
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WORLDMAP_CITY_UNLOCK_DESC, 101, NKMWorldMapManager.GetCityOpenCost(worldmapData, true), delegate()
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_SET_CITY_REQ(this.m_CityID, true);
			}, null, false);
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x0028ACB0 File Offset: 0x00288EB0
		private void OnBuildCredit()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapData worldmapData = nkmuserData.m_WorldmapData;
			if (worldmapData == null)
			{
				return;
			}
			if (worldmapData.GetUnlockedCityCount() < NKMWorldMapManager.GetPossibleCityCount(nkmuserData.UserLevel))
			{
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WORLDMAP_CITY_UNLOCK_DESC, 1, NKMWorldMapManager.GetCityOpenCost(worldmapData, false), delegate()
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_SET_CITY_REQ(this.m_CityID, false);
				}, null, false);
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_SET_CITY_REQ(this.m_CityID, false);
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x0028AD28 File Offset: 0x00288F28
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			int itemID = itemData.ItemID;
			if (itemID == 1 || itemID == 101)
			{
				this.SetButtonData(NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x040066E5 RID: 26341
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x040066E6 RID: 26342
		public const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RENEWAL_BRANCH_OPEN_POPUP";

		// Token: 0x040066E7 RID: 26343
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040066E8 RID: 26344
		public Text m_lbName;

		// Token: 0x040066E9 RID: 26345
		public Text m_lbDescription;

		// Token: 0x040066EA RID: 26346
		public Text m_lbCurrentCityCount;

		// Token: 0x040066EB RID: 26347
		public NKCUIPriceTag m_tagCashCost;

		// Token: 0x040066EC RID: 26348
		public NKCUIComStateButton m_csbtnBuildCash;

		// Token: 0x040066ED RID: 26349
		public NKCUIPriceTag m_tagCreditCost;

		// Token: 0x040066EE RID: 26350
		public NKCUIPriceTag m_tagCreditCostDisable;

		// Token: 0x040066EF RID: 26351
		public Text m_lbCreditReqLevel;

		// Token: 0x040066F0 RID: 26352
		public NKCUIComStateButton m_csbtnBuildCredit;

		// Token: 0x040066F1 RID: 26353
		private int m_CityID;
	}
}
