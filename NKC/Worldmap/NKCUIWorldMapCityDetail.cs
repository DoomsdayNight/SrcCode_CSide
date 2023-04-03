using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC1 RID: 2753
	public class NKCUIWorldMapCityDetail : MonoBehaviour
	{
		// Token: 0x06007AF9 RID: 31481 RVA: 0x0028FBAC File Offset: 0x0028DDAC
		public void Init(NKCUIWorldMapCityDetail.OnSelectNextCity onSelectNextCity, NKCUIWorldMapCityDetail.OnExit onExit)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.CitySwipe));
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = EventTriggerType.BeginDrag;
			entry2.callback.AddListener(new UnityAction<BaseEventData>(this.BeginDrag));
			EventTrigger.Entry entry3 = new EventTrigger.Entry();
			entry3.eventID = EventTriggerType.PointerClick;
			entry3.callback.AddListener(new UnityAction<BaseEventData>(this.OnClick));
			this.m_evtSwipe.triggers.Clear();
			this.m_evtSwipe.triggers.Add(entry);
			this.m_evtSwipe.triggers.Add(entry2);
			this.m_evtSwipe.triggers.Add(entry3);
			this.dOnSelectNextCity = onSelectNextCity;
			this.dOnExit = onExit;
			this.m_csbtnSetLeader.PointerClick.AddListener(new UnityAction(this.OnBtnLeaderSelect));
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x0028FC93 File Offset: 0x0028DE93
		public void CleanUp()
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			this.ResetSDPosition();
			this.m_currentSDUnitID = -1;
			this.m_currentSDSkinID = -1;
			this.m_spineSD = null;
			this.m_CityData = null;
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x0028FCCC File Offset: 0x0028DECC
		public void SetData(NKMWorldMapCityData cityData)
		{
			if (cityData == null)
			{
				Debug.Log("CityData Null!!");
				return;
			}
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(cityData.cityID);
			if (cityTemplet == null)
			{
				Debug.LogError(string.Format("Fatal : CityTemplet not exist(id : {0}). server/client off sync", cityData.cityID));
				return;
			}
			this.m_CityID = cityTemplet.m_ID;
			this.m_CityData = cityData;
			NKCUtil.SetLabelText(this.m_lbCityName, cityTemplet.GetName());
			NKCUtil.SetLabelText(this.m_lbCityTitle, cityTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_lbCityLevel, cityData.level.ToString());
			this.SetExpBar(cityData);
			this.SetLeaderData(cityData);
			this.SetOffice(cityData);
			this.ShowEmotion(NKCUIWorldMapCityDetail.EmotionType.None, 0f);
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x0028FD7D File Offset: 0x0028DF7D
		private void SetExpBar(NKMWorldMapCityData cityData)
		{
			if (this.m_imgCityExp != null)
			{
				this.m_imgCityExp.fillAmount = NKMWorldMapManager.GetCityExpPercent(cityData);
			}
		}

		// Token: 0x06007AFD RID: 31485 RVA: 0x0028FDA0 File Offset: 0x0028DFA0
		private void SetOffice(NKMWorldMapCityData cityData)
		{
			string bundleName = "AB_UI_NKM_UI_WORLD_MAP_RENEWAL_OFFICE";
			string assetName = string.Empty;
			foreach (KeyValuePair<int, NKMWorldmapCityBuildingData> keyValuePair in cityData.worldMapCityBuildingDataMap)
			{
				NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(keyValuePair.Value.id);
				Dictionary<int, NKMWorldmapCityBuildingData>.Enumerator enumerator;
				keyValuePair = enumerator.Current;
				NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(keyValuePair.Value.level);
				if (levelTemplet == null)
				{
					string format = "buildingTemplet.GetLevelTemplet({0}) is null";
					keyValuePair = enumerator.Current;
					Debug.Log(string.Format(format, keyValuePair.Value.level));
				}
				else if (!string.IsNullOrEmpty(levelTemplet.ManagerRoomPath))
				{
					assetName = levelTemplet.ManagerRoomPath;
					break;
				}
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(bundleName, assetName, false);
			if (orLoadAssetResource != null)
			{
				this.m_imgOffice.sprite = orLoadAssetResource;
			}
		}

		// Token: 0x06007AFE RID: 31486 RVA: 0x0028FE70 File Offset: 0x0028E070
		public void SetLeaderData(NKMWorldMapCityData cityData)
		{
			if (cityData != null)
			{
				if (cityData.leaderUnitUID == 0L)
				{
					NKCUtil.SetLabelText(this.m_lbSetLeader, NKCUtilString.GET_STRING_WORLDMAP_CITY_SET_LEADER);
					NKCUtil.SetImageSprite(this.m_imgSetLeader, this.m_spAddLeader, false);
					NKCUtil.SetLabelText(this.m_lbLeaderLevel, "");
					NKCUtil.SetLabelText(this.m_lbLeaderName, "");
					this.OpenSDIllust(null);
					NKCUtil.SetGameobjectActive(this.m_objLeaderSeized, false);
				}
				else
				{
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData == null)
					{
						return;
					}
					NKCUtil.SetLabelText(this.m_lbSetLeader, NKCUtilString.GET_STRING_WORLDMAP_CITY_CHANGE_LEADER);
					NKCUtil.SetImageSprite(this.m_imgSetLeader, this.m_spChangeLeader, false);
					NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(cityData.leaderUnitUID);
					if (unitFromUID != null)
					{
						this.OpenSDIllust(unitFromUID);
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID);
						if (unitTempletBase != null)
						{
							NKCUIComTextUnitLevel lbLeaderLevel = this.m_lbLeaderLevel;
							if (lbLeaderLevel != null)
							{
								lbLeaderLevel.SetLevel(unitFromUID, 0, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, Array.Empty<Text>());
							}
							NKCUtil.SetLabelText(this.m_lbLeaderName, unitTempletBase.GetUnitName());
						}
						else
						{
							NKCUtil.SetLabelText(this.m_lbLeaderLevel, "");
							NKCUtil.SetLabelText(this.m_lbLeaderName, "");
						}
						NKCUtil.SetGameobjectActive(this.m_objLeaderSeized, unitFromUID.IsSeized);
					}
					else
					{
						Debug.LogError("leader unit not exist! uid : " + cityData.leaderUnitUID.ToString());
						this.OpenSDIllust(null);
						NKCUtil.SetLabelText(this.m_lbLeaderLevel, "");
						NKCUtil.SetLabelText(this.m_lbLeaderName, "");
						NKCUtil.SetGameobjectActive(this.m_objLeaderSeized, false);
					}
				}
				bool flag = cityData.worldMapMission != null && cityData.worldMapMission.currentMissionID != 0;
				NKCUtil.SetGameobjectActive(this.m_csbtnSetLeader, !flag);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnSetLeader, false);
			this.OpenSDIllust(null);
			NKCUtil.SetLabelText(this.m_lbLeaderLevel, "");
			NKCUtil.SetLabelText(this.m_lbLeaderName, "");
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x00290044 File Offset: 0x0028E244
		public void BeginDrag(BaseEventData data)
		{
			this.bSwipePossible = true;
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x00290050 File Offset: 0x0028E250
		public void CitySwipe(BaseEventData cBaseEventData)
		{
			if (!this.bSwipePossible)
			{
				return;
			}
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData.delta.x < -50f)
			{
				this.SelectNextCity();
				this.bSwipePossible = false;
				return;
			}
			if (pointerEventData.delta.x > 50f)
			{
				this.SelectPrevCity();
				this.bSwipePossible = false;
			}
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x002900AC File Offset: 0x0028E2AC
		private void SelectNextCity()
		{
			if (this.dOnSelectNextCity != null)
			{
				this.dOnSelectNextCity(this.m_CityID, true);
			}
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x002900C8 File Offset: 0x0028E2C8
		private void SelectPrevCity()
		{
			if (this.dOnSelectNextCity != null)
			{
				this.dOnSelectNextCity(this.m_CityID, false);
			}
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x002900E4 File Offset: 0x0028E2E4
		private void OnClick(BaseEventData data)
		{
			if (this.bSwipePossible && this.dOnExit != null)
			{
				this.dOnExit();
			}
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x00290104 File Offset: 0x0028E304
		private void OnBtnLeaderSelect()
		{
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(this.m_CityID);
			if (cityData == null)
			{
				return;
			}
			if (cityData.worldMapMission != null && cityData.worldMapMission.currentMissionID != 0)
			{
				return;
			}
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.bDescending = true;
			options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			options.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			options.bShowRemoveSlot = (cityData.leaderUnitUID != 0L);
			options.bShowHideDeckedUnitMenu = false;
			options.bHideDeckedUnit = false;
			options.strUpsideMenuName = NKCUtilString.GET_STRING_WORLDMAP_CITY_SELECT_LEADER;
			options.strEmptyMessage = NKCUtilString.GET_STRING_WORLDMAP_CITY_NO_EXIST_LEADER;
			options.bIncludeUndeckableUnit = false;
			options.setExcludeUnitUID = new HashSet<long>
			{
				cityData.leaderUnitUID
			};
			options.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			options.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			options.m_bUseFavorite = true;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnLeaderUnitSelected), null, null, null, null);
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x0029020C File Offset: 0x0028E40C
		private void OnLeaderUnitSelected(List<long> lstUnitUID)
		{
			if (lstUnitUID.Count != 1)
			{
				Debug.LogError("Fatal Error : UnitSelectList returned wrong list");
				return;
			}
			long leaderUID = lstUnitUID[0];
			NKCUIUnitSelectList.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().Send_NKMPacket_WORLDMAP_SET_LEADER_REQ(this.m_CityID, leaderUID);
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x00290250 File Offset: 0x0028E450
		private void OpenSDIllust(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
				this.m_currentSDUnitID = 0;
				this.m_currentSDSkinID = 0;
				return;
			}
			if (unitData.m_UnitID == this.m_currentSDUnitID && unitData.m_SkinID == this.m_currentSDSkinID)
			{
				this.PlaySdAnimation();
				return;
			}
			this.m_currentSDUnitID = unitData.m_UnitID;
			this.m_currentSDSkinID = unitData.m_SkinID;
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(unitData, false);
			if (this.m_spineSD != null)
			{
				this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localScale = Vector3.one * this.m_fSDScale;
				}
				this.ResetSDPosition();
				this.PlaySdAnimation();
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x00290380 File Offset: 0x0028E580
		private void PlaySdAnimation()
		{
			if (this.m_CityData == null)
			{
				return;
			}
			if (NKMWorldMapManager.IsMissionRunning(this.m_CityData))
			{
				this.SDWork();
				return;
			}
			this.SDIdle();
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x002903A8 File Offset: 0x0028E5A8
		private void ResetSDPosition()
		{
			this.m_rtSDRoot.DOKill(false);
			this.m_rtSDRoot.localRotation = Quaternion.identity;
			this.m_rtSDRoot.anchoredPosition = new Vector2(this.m_rtSDMoveRange.GetWidth() * 0.5f, this.m_rtSDMoveRange.GetHeight());
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x00290400 File Offset: 0x0028E600
		private void SDRandomWalk()
		{
			this.m_rtSDRoot.DOKill(false);
			Vector2 vector = new Vector2(UnityEngine.Random.Range(0f, this.m_rtSDMoveRange.GetWidth()), UnityEngine.Random.Range(0f, this.m_rtSDMoveRange.GetHeight()));
			bool flag = vector.x - this.m_rtSDRoot.anchoredPosition.x < 0f;
			this.m_rtSDRoot.localRotation = (flag ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity);
			this.m_rtSDRoot.DOAnchorPos(vector, this.MoveSpeed, false).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(new TweenCallback(this.SDIdle));
			this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_WALK, true, 0, true, 0f, true);
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x002904E0 File Offset: 0x0028E6E0
		private void SDIdle()
		{
			this.m_rtSDRoot.DOKill(false);
			float duration = (float)UnityEngine.Random.Range(1, 4);
			this.m_rtSDRoot.DOAnchorPos(this.m_rtSDRoot.anchoredPosition, duration, false).OnComplete(new TweenCallback(this.SDRandomWalk));
			this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x00290546 File Offset: 0x0028E746
		private void SDWork()
		{
			this.ResetSDPosition();
			this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_WORKING, true, 0, true, 0f, true);
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x00290567 File Offset: 0x0028E767
		private void ShowEmotion(NKCUIWorldMapCityDetail.EmotionType type, float time)
		{
			NKCUtil.SetGameobjectActive(this.m_imgSDEmotion, type > NKCUIWorldMapCityDetail.EmotionType.None);
			if (type == NKCUIWorldMapCityDetail.EmotionType.None)
			{
				return;
			}
			if (type != NKCUIWorldMapCityDetail.EmotionType.Heart)
			{
				return;
			}
			base.StartCoroutine(this.WaitAndPlay(time, delegate
			{
				this.ShowEmotion(NKCUIWorldMapCityDetail.EmotionType.None, 0f);
			}));
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x0029059B File Offset: 0x0028E79B
		private IEnumerator WaitAndPlay(float time, UnityAction action)
		{
			yield return new WaitForSeconds(time);
			if (action != null)
			{
				action();
			}
			yield break;
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x002905B4 File Offset: 0x0028E7B4
		private void SDWin()
		{
			this.m_rtSDRoot.DOKill(false);
			float animationTime = this.m_spineSD.GetAnimationTime(NKCASUIUnitIllust.eAnimation.SD_WIN);
			this.m_rtSDRoot.DOAnchorPos(this.m_rtSDRoot.anchoredPosition, animationTime, false).OnComplete(new TweenCallback(this.PlaySdAnimation));
			this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_WIN, false, 0, true, 0f, true);
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x00290622 File Offset: 0x0028E822
		public void OnDonate()
		{
			this.ShowEmotion(NKCUIWorldMapCityDetail.EmotionType.Heart, 2f);
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x00290630 File Offset: 0x0028E830
		public void OnMissionStart()
		{
			this.SDWork();
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x00290638 File Offset: 0x0028E838
		public void OnMissionCancel()
		{
			this.SDRandomWalk();
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x00290640 File Offset: 0x0028E840
		public void OnMissionComplete()
		{
			this.SDWin();
		}

		// Token: 0x06007B13 RID: 31507 RVA: 0x00290648 File Offset: 0x0028E848
		public void OnCityLevelup()
		{
			this.SDWin();
		}

		// Token: 0x040067B2 RID: 26546
		[Header("상단부")]
		public Text m_lbCityLevel;

		// Token: 0x040067B3 RID: 26547
		public Image m_imgCityExp;

		// Token: 0x040067B4 RID: 26548
		public Text m_lbCityName;

		// Token: 0x040067B5 RID: 26549
		public Text m_lbCityTitle;

		// Token: 0x040067B6 RID: 26550
		[Header("중단부")]
		public RectTransform m_rtSDRoot;

		// Token: 0x040067B7 RID: 26551
		public RectTransform m_rtSDMoveRange;

		// Token: 0x040067B8 RID: 26552
		public float m_fSDScale = 1.2f;

		// Token: 0x040067B9 RID: 26553
		public Image m_imgSDEmotion;

		// Token: 0x040067BA RID: 26554
		public NKCUIComTextUnitLevel m_lbLeaderLevel;

		// Token: 0x040067BB RID: 26555
		public Text m_lbLeaderName;

		// Token: 0x040067BC RID: 26556
		private int m_currentSDUnitID = -1;

		// Token: 0x040067BD RID: 26557
		private int m_currentSDSkinID = -1;

		// Token: 0x040067BE RID: 26558
		private NKCASUIUnitIllust m_spineSD;

		// Token: 0x040067BF RID: 26559
		public Image m_imgOffice;

		// Token: 0x040067C0 RID: 26560
		public GameObject m_objLeaderSeized;

		// Token: 0x040067C1 RID: 26561
		[Header("하단부")]
		public NKCUIComStateButton m_csbtnSetLeader;

		// Token: 0x040067C2 RID: 26562
		public Text m_lbSetLeader;

		// Token: 0x040067C3 RID: 26563
		public Image m_imgSetLeader;

		// Token: 0x040067C4 RID: 26564
		public Sprite m_spAddLeader;

		// Token: 0x040067C5 RID: 26565
		public Sprite m_spChangeLeader;

		// Token: 0x040067C6 RID: 26566
		[Header("이벤트")]
		public EventTrigger m_evtSwipe;

		// Token: 0x040067C7 RID: 26567
		private int m_CityID;

		// Token: 0x040067C8 RID: 26568
		private bool bSwipePossible = true;

		// Token: 0x040067C9 RID: 26569
		private NKMWorldMapCityData m_CityData;

		// Token: 0x040067CA RID: 26570
		private NKCUIWorldMapCityDetail.OnSelectNextCity dOnSelectNextCity;

		// Token: 0x040067CB RID: 26571
		private NKCUIWorldMapCityDetail.OnExit dOnExit;

		// Token: 0x040067CC RID: 26572
		public float MoveSpeed = 50f;

		// Token: 0x02001830 RID: 6192
		private enum EmotionType
		{
			// Token: 0x0400A849 RID: 43081
			None,
			// Token: 0x0400A84A RID: 43082
			Heart
		}

		// Token: 0x02001831 RID: 6193
		// (Invoke) Token: 0x0600B552 RID: 46418
		public delegate void OnSelectNextCity(int currentCityID, bool bForward);

		// Token: 0x02001832 RID: 6194
		// (Invoke) Token: 0x0600B556 RID: 46422
		public delegate void OnExit();
	}
}
