using System;
using ClientPacket.WorldMap;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000ABF RID: 2751
	public class NKCUIWorldMapCity : MonoBehaviour
	{
		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06007ADA RID: 31450 RVA: 0x0028EE21 File Offset: 0x0028D021
		public RectTransform Rect
		{
			get
			{
				if (this._Rect == null)
				{
					this._Rect = base.GetComponent<RectTransform>();
				}
				return this._Rect;
			}
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x0028EE44 File Offset: 0x0028D044
		public void Init(NKCUIWorldMapCity.OnClickCity onClickCity, NKCUIWorldMapCityEventPin.OnClickEvent onClickEvent)
		{
			this.dOnClickCity = onClickCity;
			if (this.m_csbtnButton != null)
			{
				this.m_csbtnButton.PointerClick.RemoveAllListeners();
				this.m_csbtnButton.PointerClick.AddListener(new UnityAction(this.OnSlotClicked));
			}
			if (this.m_UIEventPin != null)
			{
				this.m_UIEventPin.Init(onClickEvent);
			}
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x0028EEAC File Offset: 0x0028D0AC
		public void OnSlotClicked()
		{
			NKCUIWorldMapCity.OnClickCity onClickCity = this.dOnClickCity;
			if (onClickCity == null)
			{
				return;
			}
			onClickCity(this.m_CityID);
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x0028EEC4 File Offset: 0x0028D0C4
		public void CleanUp()
		{
			if (this.m_spineSD != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			}
			this.m_spineSD = null;
		}

		// Token: 0x06007ADE RID: 31454 RVA: 0x0028EEEA File Offset: 0x0028D0EA
		public void CleanUpEventPinSpineSD()
		{
			if (this.m_UIEventPin != null)
			{
				this.m_UIEventPin.CleanUpSpineSD();
			}
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x0028EF05 File Offset: 0x0028D105
		public void PlaySDAnim(NKCASUIUnitIllust.eAnimation eAnim, bool bLoop = false)
		{
			if (this.m_UIEventPin != null)
			{
				this.m_UIEventPin.PlaySDAnim(eAnim, bLoop);
			}
		}

		// Token: 0x06007AE0 RID: 31456 RVA: 0x0028EF22 File Offset: 0x0028D122
		public Vector3 GetPinSDPos()
		{
			if (this.m_UIEventPin != null)
			{
				return this.m_UIEventPin.GetPinSDPos() + base.transform.localPosition;
			}
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x0028EF64 File Offset: 0x0028D164
		public bool SetData(NKMWorldMapCityData cityData)
		{
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(this.m_CityID);
			if (cityTemplet == null)
			{
				Debug.LogError(string.Format("CityTemplet Not Found. ID {0}", this.m_CityID));
				return false;
			}
			NKCUtil.SetLabelText(this.m_lbName, cityTemplet.GetName());
			bool bValue = false;
			if (cityData != null)
			{
				this.m_CityData = cityData;
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(this.m_CityData.leaderUnitUID);
				this.m_CityLeaderUnitData = unitFromUID;
				NKCUtil.SetLabelText(this.m_lbLevel, cityData.level.ToString());
				this.SetExpBar(cityData);
				if (cityData.HasMission())
				{
					if (NKCSynchronizedTime.IsFinished(cityData.worldMapMission.completeTime))
					{
						this.SetState(NKCUIWorldMapCity.LocationStatus.Complete);
					}
					else
					{
						this.SetState(NKCUIWorldMapCity.LocationStatus.Progress);
					}
					this.UpdateClock();
				}
				else
				{
					this.SetState(NKCUIWorldMapCity.LocationStatus.Active);
				}
				this.SetEventData(cityData.worldMapEventGroup);
			}
			else
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKMWorldMapData worldmapData = nkmuserData.m_WorldmapData;
					if (worldmapData != null && worldmapData.GetUnlockedCityCount() < NKMWorldMapManager.GetPossibleCityCount(nkmuserData.UserLevel))
					{
						bValue = true;
					}
				}
				this.m_CityData = null;
				this.m_CityLeaderUnitData = null;
				NKCUtil.SetLabelText(this.m_lbLevel, "-");
				this.SetState(NKCUIWorldMapCity.LocationStatus.Deactive);
				this.SetEventData(null);
			}
			NKCUtil.SetGameobjectActive(this.m_BRANCH_OPEN, bValue);
			return true;
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x0028F0AB File Offset: 0x0028D2AB
		public void UpdateCityRaidData()
		{
			this.m_UIEventPin.UpdateRaidData();
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x0028F0B8 File Offset: 0x0028D2B8
		private void SetExpBar(NKMWorldMapCityData cityData)
		{
			if (this.m_imgLevelGuage != null)
			{
				this.m_imgLevelGuage.fillAmount = NKMWorldMapManager.GetCityExpPercent(cityData);
			}
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x0028F0DC File Offset: 0x0028D2DC
		private void SetEventData(NKMWorldMapEventGroup eventGroupData)
		{
			if (this.m_UIEventPin == null || eventGroupData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_UIEventPin, false);
				return;
			}
			if (eventGroupData.worldmapEventID == 0)
			{
				NKCUtil.SetGameobjectActive(this.m_UIEventPin, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_UIEventPin, true);
			this.m_UIEventPin.SetData(this.m_CityID, eventGroupData);
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x0028F13C File Offset: 0x0028D33C
		private void SetState(NKCUIWorldMapCity.LocationStatus status)
		{
			this.m_CurrentStatus = status;
			if (this.m_Animator != null)
			{
				this.m_Animator.SetBool("Locked", status == NKCUIWorldMapCity.LocationStatus.Deactive);
				this.m_Animator.SetBool("Mission", status == NKCUIWorldMapCity.LocationStatus.Progress);
				this.m_Animator.SetBool("Complete", status == NKCUIWorldMapCity.LocationStatus.Complete);
			}
			switch (this.m_CurrentStatus)
			{
			case NKCUIWorldMapCity.LocationStatus.Deactive:
				NKCUtil.SetGameobjectActive(this.m_objProgressRoot, false);
				break;
			case NKCUIWorldMapCity.LocationStatus.Active:
				NKCUtil.SetGameobjectActive(this.m_objProgressRoot, false);
				break;
			case NKCUIWorldMapCity.LocationStatus.Progress:
				NKCUtil.SetGameobjectActive(this.m_objProgressRoot, true);
				NKCUtil.SetGameobjectActive(this.m_lbProgressTimeLeft, true);
				break;
			case NKCUIWorldMapCity.LocationStatus.Complete:
				NKCUtil.SetGameobjectActive(this.m_objProgressRoot, false);
				NKCUtil.SetGameobjectActive(this.m_lbProgressTimeLeft, false);
				break;
			}
			this.SetSD(status);
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x0028F210 File Offset: 0x0028D410
		private void SetSD(NKCUIWorldMapCity.LocationStatus status)
		{
			switch (status)
			{
			case NKCUIWorldMapCity.LocationStatus.Deactive:
				this.OpenSDIllust(null);
				break;
			case NKCUIWorldMapCity.LocationStatus.Active:
				this.OpenSDIllust(this.m_CityLeaderUnitData);
				if (this.m_spineSD != null)
				{
					float fStartTime = NKMRandom.Range(0f, this.m_spineSD.GetAnimationTime(NKCASUIUnitIllust.eAnimation.SD_IDLE));
					this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, fStartTime, true);
					return;
				}
				break;
			case NKCUIWorldMapCity.LocationStatus.Progress:
			{
				NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(this.m_CityData.worldMapMission.currentMissionID);
				this.OpenSDIllust(this.m_CityLeaderUnitData);
				NKCASUIUnitIllust.eAnimation eAnim;
				if (missionTemplet != null)
				{
					eAnim = NKCUIWorldMap.GetMissionProgressAnimationType(missionTemplet.m_eMissionType);
				}
				else
				{
					eAnim = NKCASUIUnitIllust.eAnimation.SD_WORKING;
				}
				if (this.m_spineSD != null)
				{
					float fStartTime2 = NKMRandom.Range(0f, this.m_spineSD.GetAnimationTime(eAnim));
					this.m_spineSD.SetAnimation(eAnim, true, 0, true, fStartTime2, true);
					return;
				}
				break;
			}
			case NKCUIWorldMapCity.LocationStatus.Complete:
				NKMWorldMapManager.GetMissionTemplet(this.m_CityData.worldMapMission.currentMissionID);
				this.OpenSDIllust(this.m_CityLeaderUnitData);
				if (this.m_spineSD != null)
				{
					float fStartTime3 = NKMRandom.Range(0f, this.m_spineSD.GetAnimationTime(NKCASUIUnitIllust.eAnimation.SD_WIN));
					this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_WIN, true, 0, true, fStartTime3, true);
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x0028F350 File Offset: 0x0028D550
		private void OpenSDIllust(NKMUnitData unitData)
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			if (unitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
				this.m_spineSD = null;
				return;
			}
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(unitData, false);
			if (this.m_spineSD != null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector2.zero;
					rectTransform.localScale = Vector3.one;
					rectTransform.localRotation = Quaternion.identity;
					return;
				}
			}
			else
			{
				Debug.LogError("spine SD data not found from unitID : " + unitData.m_UnitID.ToString());
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
			}
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x0028F420 File Offset: 0x0028D620
		private void UpdateClock()
		{
			if (this.m_CityData != null)
			{
				this.m_lbProgressTimeLeft.text = NKCSynchronizedTime.GetTimeLeftString(this.m_CityData.worldMapMission.completeTime);
				if (NKCSynchronizedTime.IsFinished(this.m_CityData.worldMapMission.completeTime))
				{
					this.SetState(NKCUIWorldMapCity.LocationStatus.Complete);
				}
			}
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x0028F473 File Offset: 0x0028D673
		private void Update()
		{
			if (this.m_CurrentStatus == NKCUIWorldMapCity.LocationStatus.Progress)
			{
				this.UpdateClock();
			}
			this.m_rtRoot.rotation = Quaternion.identity;
		}

		// Token: 0x0400678B RID: 26507
		[Header("기본")]
		public int m_CityID;

		// Token: 0x0400678C RID: 26508
		public RectTransform m_rtRoot;

		// Token: 0x0400678D RID: 26509
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x0400678E RID: 26510
		public Animator m_Animator;

		// Token: 0x0400678F RID: 26511
		public GameObject m_BRANCH_OPEN;

		// Token: 0x04006790 RID: 26512
		public Text m_lbName;

		// Token: 0x04006791 RID: 26513
		public Image m_imgLevelGuage;

		// Token: 0x04006792 RID: 26514
		public Text m_lbLevel;

		// Token: 0x04006793 RID: 26515
		[Header("진행중 표시")]
		public GameObject m_objProgressRoot;

		// Token: 0x04006794 RID: 26516
		public Text m_lbProgressTimeLeft;

		// Token: 0x04006795 RID: 26517
		[Header("SD 루트")]
		public RectTransform m_rtSDRoot;

		// Token: 0x04006796 RID: 26518
		[Header("이벤트")]
		public NKCUIWorldMapCityEventPin m_UIEventPin;

		// Token: 0x04006797 RID: 26519
		private NKCUIWorldMapCity.LocationStatus m_CurrentStatus;

		// Token: 0x04006798 RID: 26520
		private NKMWorldMapCityData m_CityData;

		// Token: 0x04006799 RID: 26521
		private NKMUnitData m_CityLeaderUnitData;

		// Token: 0x0400679A RID: 26522
		private NKCASUIUnitIllust m_spineSD;

		// Token: 0x0400679B RID: 26523
		private NKCUIWorldMapCity.OnClickCity dOnClickCity;

		// Token: 0x0400679C RID: 26524
		private RectTransform _Rect;

		// Token: 0x0200182D RID: 6189
		public enum LocationStatus
		{
			// Token: 0x0400A844 RID: 43076
			Deactive,
			// Token: 0x0400A845 RID: 43077
			Active,
			// Token: 0x0400A846 RID: 43078
			Progress,
			// Token: 0x0400A847 RID: 43079
			Complete
		}

		// Token: 0x0200182E RID: 6190
		// (Invoke) Token: 0x0600B54A RID: 46410
		public delegate void OnClickCity(int CityID);
	}
}
