using System;
using ClientPacket.WorldMap;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC3 RID: 2755
	public class NKCUIWorldMapCityManagement : MonoBehaviour
	{
		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06007B2C RID: 31532 RVA: 0x002916A9 File Offset: 0x0028F8A9
		public bool IsOpen
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x002916B8 File Offset: 0x0028F8B8
		public void Init(NKCUIWorldMapCityDetail.OnSelectNextCity onSelectNextCity, NKCUIWorldMapCityDetail.OnExit onExit)
		{
			this.m_UICityDetail.Init(onSelectNextCity, onExit);
			this.m_UICityMission.Init();
			this.m_UICityBuilding.Init();
			if (this.m_tglMission != null)
			{
				this.m_tglMission.OnValueChanged.RemoveAllListeners();
				this.m_tglMission.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTglMission));
			}
			else
			{
				Debug.LogError("Mission Toggle Button Not Set");
			}
			if (this.m_tglBuilding != null)
			{
				this.m_tglBuilding.OnValueChanged.RemoveAllListeners();
				this.m_tglBuilding.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTglBuilding));
				return;
			}
			Debug.LogError("Building Toggle Button Not Set");
		}

		// Token: 0x06007B2E RID: 31534 RVA: 0x00291773 File Offset: 0x0028F973
		public void CleanUp()
		{
			this.m_eState = NKCUIWorldMapCityManagement.State.CityMission;
			this.m_CityData = null;
			this.m_UICityDetail.CleanUp();
			this.m_UICityMission.CleanUp();
			this.m_UICityBuilding.CleanUp();
		}

		// Token: 0x06007B2F RID: 31535 RVA: 0x002917A4 File Offset: 0x0028F9A4
		public void Open(NKMWorldMapCityData cityData, Action openCallback, Action closeCallback)
		{
			base.gameObject.SetActive(true);
			if (openCallback != null)
			{
				openCallback();
			}
			this.m_openCallback = openCallback;
			this.m_closeCallback = closeCallback;
			this.SetState(this.m_eState, cityData);
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x002917D8 File Offset: 0x0028F9D8
		private void SetState(NKCUIWorldMapCityManagement.State state, NKMWorldMapCityData cityData)
		{
			this.m_eState = state;
			NKCUtil.SetGameobjectActive(this.m_UICityMission, state == NKCUIWorldMapCityManagement.State.CityMission);
			NKCUtil.SetGameobjectActive(this.m_UICityBuilding, state == NKCUIWorldMapCityManagement.State.Building);
			NKCUIWorldMapCityManagement.State eState = this.m_eState;
			if (eState != NKCUIWorldMapCityManagement.State.CityMission)
			{
				if (eState == NKCUIWorldMapCityManagement.State.Building)
				{
					this.m_tglBuilding.Select(true, true, false);
				}
			}
			else
			{
				this.m_tglMission.Select(true, true, false);
			}
			this.SetData(cityData);
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x00291840 File Offset: 0x0028FA40
		public void SetData(NKMWorldMapCityData cityData)
		{
			this.m_CityData = cityData;
			this.m_UICityDetail.SetData(cityData);
			NKCUIWorldMapCityManagement.State eState = this.m_eState;
			if (eState != NKCUIWorldMapCityManagement.State.CityMission)
			{
				if (eState == NKCUIWorldMapCityManagement.State.Building)
				{
					this.m_UICityBuilding.SetData(cityData);
				}
			}
			else
			{
				this.m_UICityMission.SetData(cityData);
			}
			bool bValue = NKMWorldMapManager.GetUsableBuildPoint(cityData) > 0;
			for (int i = 0; i < this.m_objBuildingReddot.Length; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_objBuildingReddot[i], bValue);
			}
		}

		// Token: 0x06007B32 RID: 31538 RVA: 0x002918B3 File Offset: 0x0028FAB3
		public void Close()
		{
			this.CleanUp();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			Action closeCallback = this.m_closeCallback;
			if (closeCallback == null)
			{
				return;
			}
			closeCallback();
		}

		// Token: 0x06007B33 RID: 31539 RVA: 0x002918D7 File Offset: 0x0028FAD7
		public void Unhide()
		{
			Action openCallback = this.m_openCallback;
			if (openCallback == null)
			{
				return;
			}
			openCallback();
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x002918E9 File Offset: 0x0028FAE9
		private void OnTglMission(bool value)
		{
			if (value)
			{
				this.SetState(NKCUIWorldMapCityManagement.State.CityMission, this.m_CityData);
			}
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x002918FB File Offset: 0x0028FAFB
		private void OnTglBuilding(bool value)
		{
			if (value)
			{
				this.SetState(NKCUIWorldMapCityManagement.State.Building, this.m_CityData);
			}
		}

		// Token: 0x06007B36 RID: 31542 RVA: 0x0029190D File Offset: 0x0028FB0D
		public void CityDataUpdated(NKMWorldMapCityData cityData)
		{
			this.SetData(cityData);
		}

		// Token: 0x06007B37 RID: 31543 RVA: 0x00291916 File Offset: 0x0028FB16
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (this.m_eState == NKCUIWorldMapCityManagement.State.CityMission)
			{
				this.m_UICityMission.OnInventoryChange(itemData);
			}
		}

		// Token: 0x06007B38 RID: 31544 RVA: 0x0029192C File Offset: 0x0028FB2C
		public void SetFXBuildingID(int buildingID)
		{
			if (this.m_eState == NKCUIWorldMapCityManagement.State.Building)
			{
				this.m_UICityBuilding.SetFXBuildingID(buildingID);
			}
		}

		// Token: 0x040067E8 RID: 26600
		[Header("Left")]
		public NKCUIWorldMapCityDetail m_UICityDetail;

		// Token: 0x040067E9 RID: 26601
		[Header("Right")]
		public NKCUIComToggle m_tglMission;

		// Token: 0x040067EA RID: 26602
		public NKCUIComToggle m_tglBuilding;

		// Token: 0x040067EB RID: 26603
		public GameObject[] m_objBuildingReddot;

		// Token: 0x040067EC RID: 26604
		public NKCUIWorldmapCityMissionPanel m_UICityMission;

		// Token: 0x040067ED RID: 26605
		public NKCUIWorldmapCityBuildPanel m_UICityBuilding;

		// Token: 0x040067EE RID: 26606
		private NKCUIWorldMapCityManagement.State m_eState;

		// Token: 0x040067EF RID: 26607
		private NKMWorldMapCityData m_CityData;

		// Token: 0x040067F0 RID: 26608
		private Action m_openCallback;

		// Token: 0x040067F1 RID: 26609
		private Action m_closeCallback;

		// Token: 0x02001838 RID: 6200
		private enum State
		{
			// Token: 0x0400A860 RID: 43104
			CityMission,
			// Token: 0x0400A861 RID: 43105
			Building
		}
	}
}
