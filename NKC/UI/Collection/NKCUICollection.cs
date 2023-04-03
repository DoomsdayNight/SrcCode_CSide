using System;
using System.Collections.Generic;
using ClientPacket.Community;
using ClientPacket.User;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Collection
{
	// Token: 0x02000C1F RID: 3103
	public class NKCUICollection : NKCUIBase
	{
		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06008F99 RID: 36761 RVA: 0x0030CEE3 File Offset: 0x0030B0E3
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_COLLECTION;
			}
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06008F9A RID: 36762 RVA: 0x0030CEEA File Offset: 0x0030B0EA
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06008F9B RID: 36763 RVA: 0x0030CEED File Offset: 0x0030B0ED
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SYSTEM_COLLETION";
			}
		}

		// Token: 0x06008F9C RID: 36764 RVA: 0x0030CEF4 File Offset: 0x0030B0F4
		public override void OnBackButton()
		{
			if (NKCUICutScenPlayer.IsInstanceOpen && NKCUICutScenPlayer.Instance.IsPlaying())
			{
				NKCUICutScenPlayer.Instance.StopWithCallBack();
				return;
			}
			base.OnBackButton();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06008F9D RID: 36765 RVA: 0x0030CF26 File Offset: 0x0030B126
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x06008F9E RID: 36766 RVA: 0x0030CF30 File Offset: 0x0030B130
		public override void UnHide()
		{
			if (!this.m_bDataValid)
			{
				this.m_bDataValid = true;
			}
			base.UnHide();
			if (this.m_lstUpdateUnitMissionUnitId.Count > 0)
			{
				if (this.m_NKCUICollectionUnit != null && this.m_NKCUICollectionUnit.gameObject.activeSelf)
				{
					this.m_NKCUICollectionUnit.UpdateCollectionMissionRate(this.m_lstUpdateUnitMissionUnitId);
				}
				this.m_lstUpdateUnitMissionUnitId.Clear();
			}
			NKCUtil.SetGameobjectActive(this.NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE_Reddot, NKCUnitMissionManager.HasRewardEnableMission());
			if (this.m_NKCUICollectionUnit != null && this.m_NKCUICollectionUnit.gameObject.activeSelf)
			{
				this.m_NKCUICollectionUnit.CheckRewardToggle();
			}
		}

		// Token: 0x06008F9F RID: 36767 RVA: 0x0030CFD7 File Offset: 0x0030B1D7
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.CloseAllInstance();
		}

		// Token: 0x06008FA0 RID: 36768 RVA: 0x0030CFEC File Offset: 0x0030B1EC
		public NKM_SHORTCUT_TYPE GetShortcutType()
		{
			switch (this.m_eCollectionType)
			{
			default:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION;
			case NKCUICollection.CollectionType.CT_TEAM_UP:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_TEAMUP;
			case NKCUICollection.CollectionType.CT_UNIT:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_UNIT;
			case NKCUICollection.CollectionType.CT_SHIP:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_SHIP;
			case NKCUICollection.CollectionType.CT_OPERATOR:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_OPERATOR;
			case NKCUICollection.CollectionType.CT_ILLUST:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_ILLUST;
			case NKCUICollection.CollectionType.CT_STORY:
				return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_STORY;
			}
		}

		// Token: 0x06008FA1 RID: 36769 RVA: 0x0030D038 File Offset: 0x0030B238
		public void Init()
		{
			this.InitButton();
			this.m_NKCUICollectionRate.Init();
			NKCUtil.SetGameobjectActive(this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_OPERATOR, !NKCOperatorUtil.IsHide());
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x0030D060 File Offset: 0x0030B260
		private void InitButton()
		{
			if (null != this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_TEAN_UP)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_TEAN_UP.OnValueChanged.RemoveAllListeners();
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_TEAN_UP.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeState(NKCUICollection.CollectionType.CT_TEAM_UP);
				});
			}
			if (null != this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE.OnValueChanged.RemoveAllListeners();
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeState(NKCUICollection.CollectionType.CT_UNIT);
				});
			}
			if (null != this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_OPERATOR)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_OPERATOR.OnValueChanged.RemoveAllListeners();
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_OPERATOR.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeState(NKCUICollection.CollectionType.CT_OPERATOR);
				});
			}
			if (null != this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_SHIP)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_SHIP.OnValueChanged.RemoveAllListeners();
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_SHIP.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeState(NKCUICollection.CollectionType.CT_SHIP);
				});
			}
			if (null != this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_ALBUM)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_ALBUM.OnValueChanged.RemoveAllListeners();
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_ALBUM.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeState(NKCUICollection.CollectionType.CT_ILLUST);
				});
			}
			if (null != this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_STORY)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_STORY.OnValueChanged.RemoveAllListeners();
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_STORY.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeState(NKCUICollection.CollectionType.CT_STORY);
				});
			}
		}

		// Token: 0x06008FA3 RID: 36771 RVA: 0x0030D1CC File Offset: 0x0030B3CC
		public void Open(NKCUICollection.CollectionType reserveType = NKCUICollection.CollectionType.CT_NONE, string reserveUnitStrID = "")
		{
			this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_TEAN_UP.Select(true, false, false);
			base.UIOpened(true);
			if (!NKCOperatorUtil.IsActive() && reserveType == NKCUICollection.CollectionType.CT_OPERATOR)
			{
				reserveType = NKCUICollection.CollectionType.CT_NONE;
			}
			if (reserveType - NKCUICollection.CollectionType.CT_NONE > 1 && reserveType - NKCUICollection.CollectionType.CT_UNIT <= 4)
			{
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE.Select(reserveType == NKCUICollection.CollectionType.CT_UNIT, false, false);
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_OPERATOR.Select(reserveType == NKCUICollection.CollectionType.CT_OPERATOR, false, false);
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_SHIP.Select(reserveType == NKCUICollection.CollectionType.CT_SHIP, false, false);
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_ALBUM.Select(reserveType == NKCUICollection.CollectionType.CT_ILLUST, false, false);
				this.m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_STORY.Select(reserveType == NKCUICollection.CollectionType.CT_STORY, false, false);
				this.ChangeState(reserveType);
			}
			if (!string.IsNullOrEmpty(reserveUnitStrID))
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(reserveUnitStrID);
				if (unitTempletBase != null)
				{
					switch (unitTempletBase.m_NKM_UNIT_TYPE)
					{
					default:
						NKCUICollectionUnitInfo.CheckInstanceAndOpen(NKCUtil.MakeDummyUnit(unitTempletBase.m_UnitID, true), null, null, NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, false, NKCUIUpsideMenu.eMode.Normal);
						break;
					case NKM_UNIT_TYPE.NUT_SHIP:
					{
						NKMUnitData shipData = NKCUtil.MakeDummyUnit(unitTempletBase.m_UnitID, true);
						NKCUICollectionShipInfo.Instance.Open(shipData, NKMDeckIndex.None, null, null, false);
						break;
					}
					case NKM_UNIT_TYPE.NUT_OPERATOR:
						NKCUICollectionOperatorInfo.Instance.Open(NKCOperatorUtil.GetDummyOperator(unitTempletBase, true), null, NKCUICollectionOperatorInfo.eCollectionState.CS_PROFILE, NKCUIUpsideMenu.eMode.Normal, false, false);
						break;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE_Reddot, NKCUnitMissionManager.HasRewardEnableMission());
		}

		// Token: 0x06008FA4 RID: 36772 RVA: 0x0030D2F8 File Offset: 0x0030B4F8
		private void CloseAllInstance()
		{
			if (null != this.m_NKCUICollectionUnit)
			{
				this.m_NKCUICollectionUnit.Clear();
				this.m_NKCUICollectionUnit = null;
			}
			if (null != this.m_NKCUICollectionShip)
			{
				this.m_NKCUICollectionShip.Clear();
				this.m_NKCUICollectionShip = null;
			}
			if (null != this.m_NKCUICollectionOperator)
			{
				this.m_NKCUICollectionOperator.Clear();
				this.m_NKCUICollectionOperator = null;
			}
			if (null != this.m_NKCUICollectionTeamUp)
			{
				this.m_NKCUICollectionTeamUp.Clear();
				this.m_NKCUICollectionTeamUp = null;
			}
			if (null != this.m_NKCUICollectionIllust)
			{
				this.m_NKCUICollectionIllust.Clear();
				this.m_NKCUICollectionIllust = null;
			}
			if (null != this.m_NKCUICollectionStory)
			{
				this.m_NKCUICollectionStory.Clear();
				this.m_NKCUICollectionStory = null;
			}
			if (this.m_lstUpdateUnitMissionUnitId != null)
			{
				this.m_lstUpdateUnitMissionUnitId.Clear();
				this.m_lstUpdateUnitMissionUnitId = null;
			}
			for (int i = 0; i < this.m_lstAssetInstance.Count; i++)
			{
				this.m_lstAssetInstance[i].Unload();
			}
			this.m_eCollectionType = NKCUICollection.CollectionType.CT_NONE;
			UnityEngine.Object.Destroy(base.gameObject);
			NKCAssetResourceManager.CloseResource("ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION");
		}

		// Token: 0x06008FA5 RID: 36773 RVA: 0x0030D428 File Offset: 0x0030B628
		private void ChangeState(NKCUICollection.CollectionType type)
		{
			if (this.m_eCollectionType == type)
			{
				Debug.Log("already loaded type " + type.ToString());
				return;
			}
			this.PreUIUnHide();
			this.OpenUI(type);
			this.m_eCollectionType = type;
		}

		// Token: 0x06008FA6 RID: 36774 RVA: 0x0030D464 File Offset: 0x0030B664
		private void OpenUI(NKCUICollection.CollectionType type)
		{
			switch (type)
			{
			case NKCUICollection.CollectionType.CT_TEAM_UP:
				if (null == this.m_NKCUICollectionTeamUp)
				{
					NKCAssetInstanceData item = null;
					this.LoadInstance<NKCUICollectionTeamUp>(ref item, ref this.m_NKCUICollectionTeamUp, "ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_TEAM_UP");
					this.m_NKCUICollectionTeamUp.Init(new NKCUICollection.OnSyncCollectingData(this.SyncCollectingData), new NKCUICollection.OnNotify(this.NotifyTeamup));
					this.m_NKCUICollectionTeamUp.gameObject.transform.SetParent(this.m_rtNKM_UI_COLLECTION_CONTENT, false);
					this.m_lstAssetInstance.Add(item);
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionTeamUp.gameObject, true);
				this.m_NKCUICollectionTeamUp.Open();
				return;
			case NKCUICollection.CollectionType.CT_UNIT:
				if (null == this.m_NKCUICollectionUnit)
				{
					NKCAssetInstanceData item2 = null;
					this.LoadInstance<NKCUICollectionUnitList>(ref item2, ref this.m_NKCUICollectionUnit, "ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_UNIT");
					this.m_NKCUICollectionUnit.Init(new NKCUICollection.OnSyncCollectingData(this.SyncCollectingData));
					this.m_NKCUICollectionUnit.gameObject.transform.SetParent(this.m_rtNKM_UI_COLLECTION_CONTENT, false);
					this.m_lstAssetInstance.Add(item2);
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionUnit.gameObject, true);
				this.m_NKCUICollectionUnit.Open();
				return;
			case NKCUICollection.CollectionType.CT_SHIP:
				if (null == this.m_NKCUICollectionShip)
				{
					NKCAssetInstanceData item3 = null;
					this.LoadInstance<NKCUICollectionUnitList>(ref item3, ref this.m_NKCUICollectionShip, "ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_SHIP");
					this.m_NKCUICollectionShip.Init(new NKCUICollection.OnSyncCollectingData(this.SyncCollectingData));
					this.m_NKCUICollectionShip.gameObject.transform.SetParent(this.m_rtNKM_UI_COLLECTION_CONTENT, false);
					this.m_lstAssetInstance.Add(item3);
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionShip.gameObject, true);
				this.m_NKCUICollectionShip.Open();
				return;
			case NKCUICollection.CollectionType.CT_OPERATOR:
				if (null == this.m_NKCUICollectionOperator)
				{
					NKCAssetInstanceData item4 = null;
					this.LoadInstance<NKCUICollectionOperatorList>(ref item4, ref this.m_NKCUICollectionOperator, "ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_OPERATOR");
					this.m_NKCUICollectionOperator.Init(new NKCUICollection.OnSyncCollectingData(this.SyncCollectingData));
					this.m_NKCUICollectionOperator.gameObject.transform.SetParent(this.m_rtNKM_UI_COLLECTION_CONTENT, false);
					this.m_lstAssetInstance.Add(item4);
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionOperator.gameObject, true);
				this.m_NKCUICollectionOperator.Open();
				return;
			case NKCUICollection.CollectionType.CT_ILLUST:
				if (null == this.m_NKCUICollectionIllust)
				{
					NKCAssetInstanceData item5 = null;
					this.LoadInstance<NKCUICollectionIllust>(ref item5, ref this.m_NKCUICollectionIllust, "ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_ILLUST");
					this.m_NKCUICollectionIllust.Init(new NKCUICollection.OnSyncCollectingData(this.SyncCollectingData));
					this.m_NKCUICollectionIllust.gameObject.transform.SetParent(this.m_rtNKM_UI_COLLECTION_CONTENT, false);
					this.m_lstAssetInstance.Add(item5);
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionIllust.gameObject, true);
				this.m_NKCUICollectionIllust.Open();
				return;
			case NKCUICollection.CollectionType.CT_STORY:
				if (null == this.m_NKCUICollectionStory)
				{
					NKCAssetInstanceData item6 = null;
					this.LoadInstance<NKCUICollectionStory>(ref item6, ref this.m_NKCUICollectionStory, "ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_STORY");
					this.m_NKCUICollectionStory.Init(new NKCUICollection.OnSyncCollectingData(this.SyncCollectingData), new NKCUICollection.OnStoryCutscen(this.StoryCutscen));
					this.m_NKCUICollectionStory.gameObject.transform.SetParent(this.m_rtNKM_UI_COLLECTION_CONTENT, false);
					base.SetupScrollRects(this.m_NKCUICollectionStory.gameObject);
					this.m_lstAssetInstance.Add(item6);
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionStory.gameObject, true);
				this.m_NKCUICollectionStory.Open();
				return;
			default:
				return;
			}
		}

		// Token: 0x06008FA7 RID: 36775 RVA: 0x0030D7D4 File Offset: 0x0030B9D4
		public void NotifyTeamup(bool bNotify)
		{
			if (this.NKM_UI_COLLECTION_PANEL_MENU_TEAM_UP_Reddot != null)
			{
				NKCUtil.SetGameobjectActive(this.NKM_UI_COLLECTION_PANEL_MENU_TEAM_UP_Reddot, bNotify);
			}
		}

		// Token: 0x06008FA8 RID: 36776 RVA: 0x0030D7F0 File Offset: 0x0030B9F0
		public void SyncCollectingData(NKCUICollection.CollectionType type, int iCur, int iTotal)
		{
			this.m_NKCUICollectionRate.SetData(type, iCur, iTotal);
		}

		// Token: 0x06008FA9 RID: 36777 RVA: 0x0030D800 File Offset: 0x0030BA00
		private void LoadInstance<T>(ref NKCAssetInstanceData AssetInstance, ref T script, string AssetBundleName, string AssetName)
		{
			AssetInstance = NKCAssetResourceManager.OpenInstance<GameObject>(AssetBundleName, AssetName, false, null);
			if (AssetInstance.m_Instant != null)
			{
				AssetInstance.m_Instant.transform.SetParent(base.gameObject.transform, false);
				script = AssetInstance.m_Instant.GetComponent<T>();
				return;
			}
			Debug.LogError(string.Concat(new string[]
			{
				"Load Faile ",
				typeof(T).ToString(),
				", path",
				AssetBundleName.ToString(),
				", name",
				AssetName.ToString()
			}));
		}

		// Token: 0x06008FAA RID: 36778 RVA: 0x0030D8A8 File Offset: 0x0030BAA8
		private void PreUIUnHide()
		{
			if (this.m_eCollectionType == NKCUICollection.CollectionType.CT_NONE)
			{
				return;
			}
			switch (this.m_eCollectionType)
			{
			case NKCUICollection.CollectionType.CT_TEAM_UP:
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionTeamUp.gameObject, false);
				return;
			case NKCUICollection.CollectionType.CT_UNIT:
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionUnit.gameObject, false);
				return;
			case NKCUICollection.CollectionType.CT_SHIP:
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionShip.gameObject, false);
				return;
			case NKCUICollection.CollectionType.CT_OPERATOR:
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionOperator.gameObject, false);
				return;
			case NKCUICollection.CollectionType.CT_ILLUST:
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionIllust.gameObject, false);
				return;
			case NKCUICollection.CollectionType.CT_STORY:
				NKCUtil.SetGameobjectActive(this.m_NKCUICollectionStory.gameObject, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06008FAB RID: 36779 RVA: 0x0030D950 File Offset: 0x0030BB50
		public void OnRecvReviewTagVoteCancelAck(NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_ACK sPacket)
		{
			NKCUICollectionUnitInfo.Instance.OnRecvReviewTagVoteCancelAck(sPacket);
		}

		// Token: 0x06008FAC RID: 36780 RVA: 0x0030D95D File Offset: 0x0030BB5D
		public void OnRecvReviewTagVoteAck(NKMPacket_UNIT_REVIEW_TAG_VOTE_ACK sPacket)
		{
			NKCUICollectionUnitInfo.Instance.OnRecvReviewTagVoteAck(sPacket);
		}

		// Token: 0x06008FAD RID: 36781 RVA: 0x0030D96A File Offset: 0x0030BB6A
		public void OnRecvReviewTagListAck(NKMPacket_UNIT_REVIEW_TAG_LIST_ACK sPacket)
		{
			NKCUICollectionUnitInfo.Instance.OnRecvReviewTagListAck(sPacket);
		}

		// Token: 0x06008FAE RID: 36782 RVA: 0x0030D977 File Offset: 0x0030BB77
		public void OnRecvTeamCollectionRewardAck(NKMPacket_TEAM_COLLECTION_REWARD_ACK sPacket)
		{
			this.m_NKCUICollectionTeamUp.OnRecvTeamCollectionRewardAck(sPacket);
		}

		// Token: 0x06008FAF RID: 36783 RVA: 0x0030D985 File Offset: 0x0030BB85
		public void OnRecvUnitMissionReward(int unitId)
		{
			this.m_lstUpdateUnitMissionUnitId.Add(unitId);
		}

		// Token: 0x06008FB0 RID: 36784 RVA: 0x0030D993 File Offset: 0x0030BB93
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Add)
			{
				if (this.m_bHide)
				{
					this.m_bDataValid = false;
					return;
				}
				this.UpdateCollection();
			}
		}

		// Token: 0x06008FB1 RID: 36785 RVA: 0x0030D9B0 File Offset: 0x0030BBB0
		private void UpdateCollection()
		{
			switch (this.m_eCollectionType)
			{
			case NKCUICollection.CollectionType.CT_TEAM_UP:
				this.m_NKCUICollectionTeamUp.Open();
				return;
			case NKCUICollection.CollectionType.CT_UNIT:
				this.m_NKCUICollectionUnit.Open();
				return;
			case NKCUICollection.CollectionType.CT_SHIP:
				this.m_NKCUICollectionShip.Open();
				return;
			case NKCUICollection.CollectionType.CT_OPERATOR:
				this.m_NKCUICollectionOperator.Open();
				return;
			case NKCUICollection.CollectionType.CT_STORY:
				this.m_NKCUICollectionStory.Open();
				return;
			}
			Debug.Log("Can not fount collection type : " + this.m_eCollectionType.ToString());
		}

		// Token: 0x06008FB2 RID: 36786 RVA: 0x0030DA40 File Offset: 0x0030BC40
		public void StoryCutscen(bool bPlay)
		{
		}

		// Token: 0x04007C8B RID: 31883
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_collection";

		// Token: 0x04007C8C RID: 31884
		public const string UI_ASSET_NAME = "NKM_UI_COLLECTION";

		// Token: 0x04007C8D RID: 31885
		private NKCUICollection.CollectionType m_eCollectionType = NKCUICollection.CollectionType.CT_NONE;

		// Token: 0x04007C8E RID: 31886
		[Header("생성 위치")]
		public RectTransform m_rtNKM_UI_COLLECTION_CONTENT;

		// Token: 0x04007C8F RID: 31887
		[Header("획득율")]
		public NKCUICollectionRate m_NKCUICollectionRate;

		// Token: 0x04007C90 RID: 31888
		[Header("알림")]
		public GameObject NKM_UI_COLLECTION_PANEL_MENU_TEAM_UP_Reddot;

		// Token: 0x04007C91 RID: 31889
		public GameObject NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE_Reddot;

		// Token: 0x04007C92 RID: 31890
		[Header("메뉴 버튼")]
		public NKCUIComToggle m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_TEAN_UP;

		// Token: 0x04007C93 RID: 31891
		public NKCUIComToggle m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_EMPLOYEE;

		// Token: 0x04007C94 RID: 31892
		public NKCUIComToggle m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_OPERATOR;

		// Token: 0x04007C95 RID: 31893
		public NKCUIComToggle m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_SHIP;

		// Token: 0x04007C96 RID: 31894
		public NKCUIComToggle m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_ALBUM;

		// Token: 0x04007C97 RID: 31895
		public NKCUIComToggle m_ctbtn_NKM_UI_COLLECTION_PANEL_MENU_STORY;

		// Token: 0x04007C98 RID: 31896
		private const string ASSET_BUNDLE_NAME_COLLECTION = "ab_ui_nkm_ui_collection";

		// Token: 0x04007C99 RID: 31897
		private const string UI_ASSET_NAME_TEAMUP = "NKM_UI_COLLECTION_TEAM_UP";

		// Token: 0x04007C9A RID: 31898
		private NKCUICollectionTeamUp m_NKCUICollectionTeamUp;

		// Token: 0x04007C9B RID: 31899
		private const string UI_ASSET_NAME_UNIT = "NKM_UI_COLLECTION_UNIT";

		// Token: 0x04007C9C RID: 31900
		private NKCUICollectionUnitList m_NKCUICollectionUnit;

		// Token: 0x04007C9D RID: 31901
		private const string UI_ASSET_NAME_OPERATOR = "NKM_UI_COLLECTION_OPERATOR";

		// Token: 0x04007C9E RID: 31902
		private NKCUICollectionOperatorList m_NKCUICollectionOperator;

		// Token: 0x04007C9F RID: 31903
		private const string UI_ASSET_NAME_SHIP = "NKM_UI_COLLECTION_SHIP";

		// Token: 0x04007CA0 RID: 31904
		private NKCUICollectionUnitList m_NKCUICollectionShip;

		// Token: 0x04007CA1 RID: 31905
		private const string UI_ASSET_NAME_ILLUST = "NKM_UI_COLLECTION_ILLUST";

		// Token: 0x04007CA2 RID: 31906
		private NKCUICollectionIllust m_NKCUICollectionIllust;

		// Token: 0x04007CA3 RID: 31907
		private const string UI_ASSET_NAME_STORY = "NKM_UI_COLLECTION_STORY";

		// Token: 0x04007CA4 RID: 31908
		private NKCUICollectionStory m_NKCUICollectionStory;

		// Token: 0x04007CA5 RID: 31909
		private List<NKCAssetInstanceData> m_lstAssetInstance = new List<NKCAssetInstanceData>();

		// Token: 0x04007CA6 RID: 31910
		private List<int> m_lstUpdateUnitMissionUnitId = new List<int>();

		// Token: 0x04007CA7 RID: 31911
		private bool m_bDataValid;

		// Token: 0x020019DE RID: 6622
		public enum CollectionType
		{
			// Token: 0x0400AD23 RID: 44323
			CT_NONE = -1,
			// Token: 0x0400AD24 RID: 44324
			CT_TEAM_UP,
			// Token: 0x0400AD25 RID: 44325
			CT_UNIT,
			// Token: 0x0400AD26 RID: 44326
			CT_SHIP,
			// Token: 0x0400AD27 RID: 44327
			CT_OPERATOR,
			// Token: 0x0400AD28 RID: 44328
			CT_ILLUST,
			// Token: 0x0400AD29 RID: 44329
			CT_STORY
		}

		// Token: 0x020019DF RID: 6623
		// (Invoke) Token: 0x0600BA69 RID: 47721
		public delegate void OnNotify(bool bNotify);

		// Token: 0x020019E0 RID: 6624
		// (Invoke) Token: 0x0600BA6D RID: 47725
		public delegate void OnSyncCollectingData(NKCUICollection.CollectionType type, int iCur, int iTotal);

		// Token: 0x020019E1 RID: 6625
		// (Invoke) Token: 0x0600BA71 RID: 47729
		public delegate void OnStoryCutscen(bool bPlay);
	}
}
