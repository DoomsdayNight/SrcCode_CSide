using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Math;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D2 RID: 2514
	public class NKCUIRaid : NKCUIBase
	{
		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06006B8D RID: 27533 RVA: 0x00230DDC File Offset: 0x0022EFDC
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_RAID;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06006B8E RID: 27534 RVA: 0x00230DE3 File Offset: 0x0022EFE3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06006B8F RID: 27535 RVA: 0x00230DE6 File Offset: 0x0022EFE6
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06006B90 RID: 27536 RVA: 0x00230DE9 File Offset: 0x0022EFE9
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_lstUpsideMenuResource == null)
				{
					return base.UpsideMenuShowResourceList;
				}
				return this.m_lstUpsideMenuResource;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06006B91 RID: 27537 RVA: 0x00230E00 File Offset: 0x0022F000
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_RAID_INFO";
			}
		}

		// Token: 0x06006B92 RID: 27538 RVA: 0x00230E07 File Offset: 0x0022F007
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIBaseSceneMenu>("AB_UI_NKM_UI_WORLD_MAP_RAID", "NKM_UI_WORLD_MAP_RAID");
		}

		// Token: 0x06006B93 RID: 27539 RVA: 0x00230E18 File Offset: 0x0022F018
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIRaid retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIRaid>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x00230E27 File Offset: 0x0022F027
		public void CloseInstance()
		{
			NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_WORLD_MAP_RAID", "NKM_UI_WORLD_MAP_RAID");
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06006B95 RID: 27541 RVA: 0x00230E44 File Offset: 0x0022F044
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_csbtnSupportREQ.PointerClick.RemoveAllListeners();
			this.m_csbtnSupportREQ.PointerClick.AddListener(new UnityAction(this.OnClickSupportREQ));
			this.m_lvsrSupportUser.dOnGetObject += this.GetSlot;
			this.m_lvsrSupportUser.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrSupportUser.dOnProvideData += this.ProvideData;
			this.m_NKCUICharacterViewBoss.Init(null, null);
			this.m_NKCUIRaidRightSide.Init(null);
			this.m_bInit = true;
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x00230EF8 File Offset: 0x0022F0F8
		public RectTransform GetSlot(int index)
		{
			NKCUIRaidSupportUserSlot newInstance = NKCUIRaidSupportUserSlot.GetNewInstance(this.m_trSupportUserListRoot);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06006B97 RID: 27543 RVA: 0x00230F24 File Offset: 0x0022F124
		public void ReturnSlot(Transform tr)
		{
			NKCUIRaidSupportUserSlot component = tr.GetComponent<NKCUIRaidSupportUserSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06006B98 RID: 27544 RVA: 0x00230F60 File Offset: 0x0022F160
		public void ProvideData(Transform tr, int index)
		{
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (nkmraidDetailData == null)
			{
				return;
			}
			NKCUIRaidSupportUserSlot component = tr.GetComponent<NKCUIRaidSupportUserSlot>();
			if (component != null && nkmraidDetailData.raidJoinDataList.Count > index && index >= 0)
			{
				component.SetUI(nkmraidDetailData, nkmraidDetailData.raidJoinDataList[index], index + 1);
			}
		}

		// Token: 0x06006B99 RID: 27545 RVA: 0x00230FC0 File Offset: 0x0022F1C0
		public bool SetUI()
		{
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (nkmraidDetailData == null)
			{
				return false;
			}
			NKMWorldMapData worldmapData = NKCScenManager.CurrentUserData().m_WorldmapData;
			if (worldmapData == null)
			{
				return false;
			}
			NKMWorldMapCityData cityData = worldmapData.GetCityData(nkmraidDetailData.cityID);
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(nkmraidDetailData.cityID);
			if (cityTemplet == null)
			{
				return false;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
			if (nkmraidTemplet == null)
			{
				return false;
			}
			NKCUtil.SetGameobjectActive(this.m_objSupportParent, nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_RAID);
			NKCUtil.SetGameobjectActive(this.m_objRaidFail, NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate) && nkmraidDetailData.curHP > 0f);
			if (cityData == null)
			{
				this.m_imgCityExp.fillAmount = 0f;
				this.m_lbCityLevel.text = "0";
			}
			else
			{
				NKMWorldMapCityExpTemplet cityExpTable = NKMWorldMapManager.GetCityExpTable(cityData.level);
				if (cityExpTable != null && cityExpTable.m_ExpRequired != 0)
				{
					this.m_imgCityExp.fillAmount = (float)cityData.exp / (float)cityExpTable.m_ExpRequired;
				}
				else
				{
					this.m_imgCityExp.fillAmount = 1f;
				}
				this.m_lbCityLevel.text = cityData.level.ToString();
			}
			this.m_lbCityName.text = cityTemplet.GetName();
			NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(nkmraidTemplet.DungeonTempletBase.m_DungeonID);
			if (dungeonTemplet != null)
			{
				this.m_NKCUICharacterViewBoss.SetCharacterIllust(dungeonTemplet.m_BossUnitStrID, true, true, false, 0);
			}
			this.UpdateUI(false);
			NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE eNKC_RAID_SUB_BUTTON_TYPE = NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_READY;
			if (nkmraidDetailData.curHP.IsNearlyZero(1E-05f) || NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
			{
				eNKC_RAID_SUB_BUTTON_TYPE = NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_EXIT;
			}
			NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
			if (nkmraidJoinData != null)
			{
				bool tryAssist = nkmraidJoinData.tryAssist;
			}
			this.m_NKCUIRaidRightSide.SetUI(this.m_RaidUID, NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_REMAIN_TIME, eNKC_RAID_SUB_BUTTON_TYPE);
			if (nkmraidTemplet.StageReqItemID == 1)
			{
				this.m_lstUpsideMenuResource = new List<int>
				{
					1,
					101
				};
			}
			else
			{
				this.m_lstUpsideMenuResource = new List<int>
				{
					1,
					nkmraidTemplet.StageReqItemID,
					101
				};
			}
			return true;
		}

		// Token: 0x06006B9A RID: 27546 RVA: 0x002311D8 File Offset: 0x0022F3D8
		private void OnClickSupportREQ()
		{
			string get_STRING_WARNING = NKCUtilString.GET_STRING_WARNING;
			string get_STRING_RAID_COOP_REQ_WARNING = NKCUtilString.GET_STRING_RAID_COOP_REQ_WARNING;
			NKCPopupOKCancel.OpenOKCancelBox(get_STRING_WARNING, get_STRING_RAID_COOP_REQ_WARNING, delegate()
			{
				if (!NKCScenManager.GetScenManager().GetNKCRaidDataMgr().CheckRaidCoopOn(this.m_RaidUID))
				{
					NKCPacketSender.Send_NKMPacket_RAID_SET_COOP_REQ(this.m_RaidUID);
				}
			}, null, false);
		}

		// Token: 0x06006B9B RID: 27547 RVA: 0x00231204 File Offset: 0x0022F404
		public void Open(long raidUID)
		{
			this.m_RaidUID = raidUID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.SetUI())
			{
				base.UIOpened(true);
				this.CheckTutorial();
				return;
			}
			Log.Error("SetUI Failed!!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIRaid.cs", 260);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006B9C RID: 27548 RVA: 0x0023125A File Offset: 0x0022F45A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006B9D RID: 27549 RVA: 0x00231268 File Offset: 0x0022F468
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
		}

		// Token: 0x06006B9E RID: 27550 RVA: 0x00231277 File Offset: 0x0022F477
		private void Update()
		{
			if (this.m_fNextUpdateTime + 1f > Time.time)
			{
				return;
			}
			this.UpdateUI(true);
			this.m_fNextUpdateTime = Time.time;
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x002312A0 File Offset: 0x0022F4A0
		private void UpdateUI(bool bFrameMoveUpdate = false)
		{
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (nkmraidDetailData == null)
			{
				return;
			}
			if (NKMRaidTemplet.Find(nkmraidDetailData.stageID) == null)
			{
				return;
			}
			bool isCoop = nkmraidDetailData.isCoop;
			bool flag = nkmraidDetailData.curHP <= 0f || NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate);
			NKCUtil.SetGameobjectActive(this.m_objSupportREQNotYet, !isCoop && !flag);
			NKCUtil.SetGameobjectActive(this.m_objSupportREQOnGoing, isCoop);
			if (!bFrameMoveUpdate && isCoop)
			{
				nkmraidDetailData.SortJoinDataByDamage();
				if (this.m_bFirstOpenLoopScroll)
				{
					this.m_lvsrSupportUser.PrepareCells(0);
					this.m_bFirstOpenLoopScroll = false;
				}
				this.m_lvsrSupportUser.TotalCount = nkmraidDetailData.raidJoinDataList.Count;
				this.m_lvsrSupportUser.velocity = new Vector2(0f, 0f);
				this.m_lvsrSupportUser.SetIndexPosition(0);
				NKCUtil.SetGameobjectActive(this.m_objNone, this.m_lvsrSupportUser.TotalCount == 0);
				if (!flag)
				{
					NKCUtil.SetGameobjectActive(this.m_objSupport, true);
					NKCUtil.SetLabelText(this.m_lbSupport, NKCUtilString.GET_STRING_RAID_REQ_SUPPORT);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objSupport, false);
				}
			}
			if (flag)
			{
				this.m_NKCUICharacterViewBoss.PlayEffect(NKCUICharacterView.EffectType.Gray);
				this.m_NKCUICharacterViewBoss.SetVFX(false);
			}
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x002313E3 File Offset: 0x0022F5E3
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.RaidReady, true);
		}

		// Token: 0x0400572F RID: 22319
		private List<int> m_lstUpsideMenuResource;

		// Token: 0x04005730 RID: 22320
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_RAID";

		// Token: 0x04005731 RID: 22321
		private const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RAID";

		// Token: 0x04005732 RID: 22322
		[Header("왼쪽 UI")]
		public Image m_imgCityExp;

		// Token: 0x04005733 RID: 22323
		public Text m_lbCityLevel;

		// Token: 0x04005734 RID: 22324
		public Text m_lbCityName;

		// Token: 0x04005735 RID: 22325
		public GameObject m_objSupportParent;

		// Token: 0x04005736 RID: 22326
		public GameObject m_objSupportREQNotYet;

		// Token: 0x04005737 RID: 22327
		public NKCUIComStateButton m_csbtnSupportREQ;

		// Token: 0x04005738 RID: 22328
		public GameObject m_objSupportREQOnGoing;

		// Token: 0x04005739 RID: 22329
		public LoopScrollRect m_lvsrSupportUser;

		// Token: 0x0400573A RID: 22330
		public Transform m_trSupportUserListRoot;

		// Token: 0x0400573B RID: 22331
		public GameObject m_objSupport;

		// Token: 0x0400573C RID: 22332
		public Text m_lbSupport;

		// Token: 0x0400573D RID: 22333
		public GameObject m_objNone;

		// Token: 0x0400573E RID: 22334
		[Header("가운데 UI")]
		public NKCUICharacterView m_NKCUICharacterViewBoss;

		// Token: 0x0400573F RID: 22335
		public GameObject m_objRaidFail;

		// Token: 0x04005740 RID: 22336
		[Header("오른쪽 UI")]
		public NKCUIRaidRightSide m_NKCUIRaidRightSide;

		// Token: 0x04005741 RID: 22337
		private bool m_bInit;

		// Token: 0x04005742 RID: 22338
		private bool m_bFirstOpenLoopScroll = true;

		// Token: 0x04005743 RID: 22339
		private long m_RaidUID;

		// Token: 0x04005744 RID: 22340
		private float m_fNextUpdateTime;
	}
}
