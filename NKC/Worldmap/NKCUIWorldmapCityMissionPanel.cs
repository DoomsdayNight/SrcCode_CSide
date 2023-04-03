using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using NKC.Publisher;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC6 RID: 2758
	public class NKCUIWorldmapCityMissionPanel : MonoBehaviour
	{
		// Token: 0x06007B58 RID: 31576 RVA: 0x002922A8 File Offset: 0x002904A8
		public void Init()
		{
			foreach (NKCUIWorldMapMissionSlot nkcuiworldMapMissionSlot in this.m_lstSlot)
			{
				nkcuiworldMapMissionSlot.Init(new NKCUIWorldMapMissionSlot.OnClickSlot(this.OnSelectMission));
			}
			this.m_SlotProgress.Init(null);
			if (this.m_csbtnMissionRefresh != null)
			{
				this.m_csbtnMissionRefresh.PointerClick.RemoveAllListeners();
				this.m_csbtnMissionRefresh.PointerClick.AddListener(new UnityAction(this.OnMissionRefresh));
			}
			if (this.m_csbtnCancelMission != null)
			{
				this.m_csbtnCancelMission.PointerClick.RemoveAllListeners();
				this.m_csbtnCancelMission.PointerClick.AddListener(new UnityAction(this.OnMissionCancel));
			}
			NKCUtil.SetGameobjectActive(this.m_INFO_TWN, NKMContentsVersionManager.HasCountryTag(CountryTagType.TWN));
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x00292398 File Offset: 0x00290598
		public void SetData(NKMWorldMapCityData cityData)
		{
			if (cityData == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_WORLDMAP_CITY_DATA_IS_NULL, null, "");
				NKCUtil.SetGameobjectActive(this.m_objMissionListRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objMissionProgressRoot, false);
				return;
			}
			this.m_bCompleteRequestSent = false;
			this.m_CityData = cityData;
			if (this.IsMissionRunning())
			{
				this.SetDataMissionProgress(cityData);
				return;
			}
			this.SetDataMissionList(cityData);
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x002923FC File Offset: 0x002905FC
		private void SetDataMissionList(NKMWorldMapCityData cityData)
		{
			NKCUtil.SetGameobjectActive(this.m_objMissionListRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objMissionProgressRoot, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnMissionRefresh, NKCScenManager.CurrentUserData().IsSuperUser());
			NKCUtil.SetGameobjectActive(this.m_objManagerRequired, !this.HasLeader());
			int leaderLevel = 0;
			if (cityData != null)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(cityData.leaderUnitUID);
				if (unitFromUID != null)
				{
					leaderLevel = unitFromUID.m_UnitLevel;
				}
				for (int i = 0; i < this.m_lstSlot.Count; i++)
				{
					if (i < cityData.worldMapMission.stMissionIDList.Count)
					{
						int id = cityData.worldMapMission.stMissionIDList[i];
						NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(id);
						if (missionTemplet != null)
						{
							this.m_lstSlot[i].SetData(missionTemplet, leaderLevel);
							NKCUtil.SetGameobjectActive(this.m_lstSlot[i], true);
						}
						else
						{
							Debug.LogError("Mission Templet Null! ID : " + id.ToString());
							NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
					}
				}
			}
			this.UpdateRefreshButton();
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x0029252C File Offset: 0x0029072C
		private void SetDataMissionProgress(NKMWorldMapCityData cityData)
		{
			NKCUtil.SetGameobjectActive(this.m_objMissionListRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objMissionProgressRoot, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnMissionRefresh, false);
			NKCUtil.SetGameobjectActive(this.m_objManagerRequired, !this.HasLeader());
			NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(cityData.worldMapMission.currentMissionID);
			if (missionTemplet == null)
			{
				Debug.LogError(string.Format("MissionTemplet Not Found! ID : {0}", cityData.worldMapMission.currentMissionID));
				return;
			}
			this.m_SlotProgress.SetData(missionTemplet, -1);
			NKCUtil.SetLabelText(this.m_lbMisionDeckIndex, NKCUtilString.GET_STRING_WORLDMAP_CITY_LEADER);
			NKCUtil.SetLabelText(this.m_lbMissionTimeLeft, NKCSynchronizedTime.GetTimeLeftString(this.m_CityData.worldMapMission.completeTime));
			NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.WORLD_MAP_MISSION_COMPLETE, false);
			this.SetMissionProgressSD(missionTemplet);
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x002925F8 File Offset: 0x002907F8
		private void SetMissionProgressSD(NKMWorldMapMissionTemplet missionTemplet)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(this.m_CityData.leaderUnitUID);
			NKCASUIUnitIllust.eAnimation missionProgressAnimationType = NKCUIWorldMap.GetMissionProgressAnimationType(missionTemplet.m_eMissionType);
			bool bValue = this.OpenSDIllust(unitFromUID, ref this.m_spineSDAlly, this.m_rtMissionSDSoloPos);
			NKCUtil.SetGameobjectActive(this.m_rtMissionSDSoloPos, bValue);
			this.m_spineSDAlly.SetAnimation(missionProgressAnimationType, true, 0, true, 0f, true);
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x00292668 File Offset: 0x00290868
		private bool OpenSDIllust(NKMUnitData unitData, ref NKCASUIUnitIllust SpineIllust, RectTransform parent)
		{
			if (unitData == null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(SpineIllust);
				SpineIllust = null;
				return false;
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(SpineIllust);
			SpineIllust = NKCResourceUtility.OpenSpineSD(unitData, false);
			if (SpineIllust != null)
			{
				SpineIllust.SetParent(parent, false);
				RectTransform rectTransform = SpineIllust.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector2.zero;
					rectTransform.localScale = Vector2.one;
					rectTransform.localRotation = Quaternion.identity;
				}
				return true;
			}
			Debug.LogError("spine data not found from unitID : " + unitData.m_UnitID.ToString());
			return false;
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x00292710 File Offset: 0x00290910
		private bool OpenSDIllust(int targetUnitID, ref NKCASUIUnitIllust SpineIllust, RectTransform parent)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnitID);
			if (unitTempletBase == null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(SpineIllust);
				SpineIllust = null;
				return false;
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(SpineIllust);
			SpineIllust = NKCResourceUtility.OpenSpineSD(unitTempletBase, false);
			if (SpineIllust != null)
			{
				SpineIllust.SetParent(parent, false);
				RectTransform rectTransform = SpineIllust.GetRectTransform();
				rectTransform.localPosition = Vector2.zero;
				rectTransform.localScale = Vector2.one;
				rectTransform.localRotation = Quaternion.identity;
				return true;
			}
			Debug.LogError("spine data not found from unitID : " + unitTempletBase.m_UnitID.ToString());
			return false;
		}

		// Token: 0x06007B5F RID: 31583 RVA: 0x002927B2 File Offset: 0x002909B2
		private void UpdateRefreshButton()
		{
			this.m_tagRefreshPrice.SetData(3, 50, false, true, false);
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x002927C6 File Offset: 0x002909C6
		private bool IsMissionRunning()
		{
			return NKMWorldMapManager.IsMissionRunning(this.m_CityData);
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x002927D4 File Offset: 0x002909D4
		private void Update()
		{
			if (this.m_objMissionProgressRoot.activeSelf && this.IsMissionRunning())
			{
				NKCUtil.SetLabelText(this.m_lbMissionTimeLeft, NKCSynchronizedTime.GetTimeLeftString(this.m_CityData.worldMapMission.completeTime));
				if (NKCSynchronizedTime.IsFinished(this.m_CityData.worldMapMission.completeTime))
				{
					this.SendCompleteReq();
				}
			}
		}

		// Token: 0x06007B62 RID: 31586 RVA: 0x00292833 File Offset: 0x00290A33
		private void SendCompleteReq()
		{
			if (this.m_CityData != null && !this.m_bCompleteRequestSent)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_MISSION_COMPLETE_REQ(this.m_CityData.cityID);
				this.m_bCompleteRequestSent = true;
			}
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x00292866 File Offset: 0x00290A66
		public void CleanUp()
		{
			this.m_CityData = null;
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x0029286F File Offset: 0x00290A6F
		private bool HasLeader()
		{
			return this.m_CityData != null && this.m_CityData.leaderUnitUID != 0L;
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x0029288C File Offset: 0x00290A8C
		private void OnSelectMission(int missionID)
		{
			this.m_SelectedMissionID = missionID;
			if (this.IsMissionRunning())
			{
				Debug.LogError("Logic Error : Accssing to Mission ");
				return;
			}
			if (this.m_CityData.leaderUnitUID == 0L)
			{
				Debug.LogWarning("지부장 없이 여기로 들어오면 안 됨");
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(this.m_CityData.leaderUnitUID);
			NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(missionID);
			if (unitFromUID.m_UnitLevel < missionTemplet.m_ReqManagerLevel)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_LEADER_LEVEL_LOW, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (NKMWorldMapManager.IsMissionLeaderOnly(missionTemplet.m_eMissionType))
			{
				int missionSuccessRate = NKMWorldMapManager.GetMissionSuccessRate(missionTemplet, nkmuserData.m_ArmyData, this.m_CityData);
				NKCCompanyBuff.IncreaseMissioRateInWorldMap(nkmuserData.m_companyBuffDataList, ref missionSuccessRate);
				string content = string.Format(NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_CONFIRM_TWO_PARAM, missionTemplet.GetMissionName(), missionSuccessRate);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, content, delegate()
				{
					this.OnMissionDeckSelected(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				}, null, false);
				return;
			}
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_SELECT_SQUAD;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.WorldMapMissionDeckSelect;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnMissionDeckSelected);
			options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, 0);
			options.dOnBackButton = null;
			options.SelectLeaderUnitOnOpen = true;
			options.bEnableDefaultBackground = true;
			options.bUpsideMenuHomeButton = false;
			options.WorldMapMissionID = missionID;
			options.WorldMapMissionCityID = this.m_CityData.cityID;
			options.StageBattleStrID = string.Empty;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x06007B66 RID: 31590 RVA: 0x00292A01 File Offset: 0x00290C01
		private void OnMissionRefresh()
		{
			if (!NKCScenManager.CurrentUserData().IsSuperUser())
			{
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_REFRESH, 3, 50, delegate()
			{
				this.SendRefreshRequest(true);
			}, null, true);
		}

		// Token: 0x06007B67 RID: 31591 RVA: 0x00292A35 File Offset: 0x00290C35
		private void SendRefreshRequest(bool bCash)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_MISSION_REFRESH_REQ(this.m_CityData.cityID);
		}

		// Token: 0x06007B68 RID: 31592 RVA: 0x00292A51 File Offset: 0x00290C51
		private void OnMissionCancel()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_CANCEL, new NKCPopupOKCancel.OnButton(this.SendDispatchCancel), null, false);
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x00292A70 File Offset: 0x00290C70
		private void SendDispatchCancel()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_REQ(this.m_CityData.cityID, this.m_CityData.worldMapMission.currentMissionID);
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x00292A9C File Offset: 0x00290C9C
		public void OnMissionDeckSelected(NKMDeckIndex deckIndex)
		{
			NKCUIDeckViewer.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_CITY_MISSION_REQ(this.m_CityData.cityID, this.m_SelectedMissionID, deckIndex);
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x00292AC4 File Offset: 0x00290CC4
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == 3 && !this.IsMissionRunning())
			{
				this.UpdateRefreshButton();
			}
		}

		// Token: 0x0400680F RID: 26639
		[Header("List")]
		public GameObject m_objMissionListRoot;

		// Token: 0x04006810 RID: 26640
		public List<NKCUIWorldMapMissionSlot> m_lstSlot;

		// Token: 0x04006811 RID: 26641
		[Header("Progress")]
		public GameObject m_objMissionProgressRoot;

		// Token: 0x04006812 RID: 26642
		public NKCUIWorldMapMissionSlot m_SlotProgress;

		// Token: 0x04006813 RID: 26643
		public Text m_lbMisionDeckIndex;

		// Token: 0x04006814 RID: 26644
		public Text m_lbMissionTimeLeft;

		// Token: 0x04006815 RID: 26645
		public NKCUIComStateButton m_csbtnCancelMission;

		// Token: 0x04006816 RID: 26646
		public RectTransform m_rtMissionSDSoloPos;

		// Token: 0x04006817 RID: 26647
		private NKCASUIUnitIllust m_spineSDAlly;

		// Token: 0x04006818 RID: 26648
		[Header("Etc")]
		public GameObject m_objManagerRequired;

		// Token: 0x04006819 RID: 26649
		public NKCUIComStateButton m_csbtnMissionRefresh;

		// Token: 0x0400681A RID: 26650
		public NKCUIPriceTag m_tagRefreshPrice;

		// Token: 0x0400681B RID: 26651
		public GameObject m_INFO_TWN;

		// Token: 0x0400681C RID: 26652
		private NKMWorldMapCityData m_CityData;

		// Token: 0x0400681D RID: 26653
		private bool m_bCompleteRequestSent;

		// Token: 0x0400681E RID: 26654
		private readonly int[] m_aEnemyID = new int[]
		{
			511003,
			511004,
			511006
		};

		// Token: 0x0400681F RID: 26655
		private int m_SelectedMissionID;
	}
}
