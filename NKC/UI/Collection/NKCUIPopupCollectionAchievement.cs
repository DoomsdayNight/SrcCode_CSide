using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C32 RID: 3122
	public class NKCUIPopupCollectionAchievement : NKCUIBase
	{
		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060090FE RID: 37118 RVA: 0x00316D5C File Offset: 0x00314F5C
		public static NKCUIPopupCollectionAchievement Instance
		{
			get
			{
				if (NKCUIPopupCollectionAchievement.m_Instance == null)
				{
					NKCUIPopupCollectionAchievement.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupCollectionAchievement>("AB_UI_NKM_UI_COLLECTION", "NKM_UI_POPUP_COLLECTION_ACHIEVEMENT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupCollectionAchievement.CleanupInstance)).GetInstance<NKCUIPopupCollectionAchievement>();
					NKCUIPopupCollectionAchievement instance = NKCUIPopupCollectionAchievement.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCUIPopupCollectionAchievement.m_Instance;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060090FF RID: 37119 RVA: 0x00316DB1 File Offset: 0x00314FB1
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupCollectionAchievement.m_Instance != null && NKCUIPopupCollectionAchievement.m_Instance.IsOpen;
			}
		}

		// Token: 0x06009100 RID: 37120 RVA: 0x00316DCC File Offset: 0x00314FCC
		private static void CleanupInstance()
		{
			NKCUIPopupCollectionAchievement.m_Instance.Release();
			NKCUIPopupCollectionAchievement.m_Instance = null;
		}

		// Token: 0x06009101 RID: 37121 RVA: 0x00316DDE File Offset: 0x00314FDE
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupCollectionAchievement.m_Instance != null && NKCUIPopupCollectionAchievement.m_Instance.IsOpen)
			{
				NKCUIPopupCollectionAchievement.m_Instance.Close();
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06009102 RID: 37122 RVA: 0x00316E03 File Offset: 0x00315003
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06009103 RID: 37123 RVA: 0x00316E06 File Offset: 0x00315006
		public override string MenuName
		{
			get
			{
				return "유닛 도감 미션";
			}
		}

		// Token: 0x06009104 RID: 37124 RVA: 0x00316E10 File Offset: 0x00315010
		private void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAll, new UnityAction(this.OnClickCompleteAll));
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetMissionSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnMissionSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideMissionData;
				this.m_LoopScrollRect.ContentConstraintCount = 1;
				this.m_LoopScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			}
			if (this.m_eventBG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData e)
				{
					base.Close();
				});
				this.m_eventBG.triggers.Clear();
				this.m_eventBG.triggers.Add(entry);
			}
		}

		// Token: 0x06009105 RID: 37125 RVA: 0x00316F15 File Offset: 0x00315115
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06009106 RID: 37126 RVA: 0x00316F23 File Offset: 0x00315123
		public void Open(int unitId)
		{
			this.m_iUnitId = unitId;
			this.m_MissionStepTempletList = NKCUnitMissionManager.GetUnitMissionStepTempletList(unitId);
			base.gameObject.SetActive(true);
			this.Refresh();
			if (base.IsOpen)
			{
				return;
			}
			base.UIOpened(true);
		}

		// Token: 0x06009107 RID: 37127 RVA: 0x00316F5C File Offset: 0x0031515C
		public void Refresh()
		{
			if (this.m_MissionStepTempletList == null)
			{
				return;
			}
			this.m_missionStateDataList.Clear();
			bool flag = false;
			int count = this.m_MissionStepTempletList.Count;
			for (int i = 0; i < count; i++)
			{
				NKMMissionManager.MissionStateData missionState = NKCUnitMissionManager.GetMissionState(this.m_iUnitId, this.m_MissionStepTempletList[i]);
				this.m_missionStateDataList.Add(missionState);
				if (missionState.state == NKMMissionManager.MissionState.CAN_COMPLETE)
				{
					flag = true;
				}
			}
			NKCUIComStateButton csbtnCompleteAll = this.m_csbtnCompleteAll;
			if (csbtnCompleteAll != null)
			{
				csbtnCompleteAll.SetLock(!flag, false);
			}
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.TotalCount = count;
				this.m_LoopScrollRect.SetIndexPosition(0);
			}
		}

		// Token: 0x06009108 RID: 37128 RVA: 0x00317001 File Offset: 0x00315201
		private RectTransform GetMissionSlot(int index)
		{
			NKCUICollectionAchievementSlot newInstance = NKCUICollectionAchievementSlot.GetNewInstance(null, false);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06009109 RID: 37129 RVA: 0x00317018 File Offset: 0x00315218
		private void ReturnMissionSlot(Transform tr)
		{
			NKCUICollectionAchievementSlot component = tr.GetComponent<NKCUICollectionAchievementSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x0600910A RID: 37130 RVA: 0x00317050 File Offset: 0x00315250
		private void ProvideMissionData(Transform tr, int index)
		{
			NKCUICollectionAchievementSlot component = tr.GetComponent<NKCUICollectionAchievementSlot>();
			if (component == null || this.m_MissionStepTempletList == null || this.m_MissionStepTempletList.Count <= index)
			{
				return;
			}
			component.SetData(this.m_iUnitId, this.m_MissionStepTempletList[index], this.m_missionStateDataList[index], new NKCUICollectionAchievementSlot.OnComplete(this.OnMissionComplete));
		}

		// Token: 0x0600910B RID: 37131 RVA: 0x003170B4 File Offset: 0x003152B4
		private void OnMissionComplete(int unitId, int missionId, int stepId)
		{
			NKCPacketSender.Send_NKMPacket_UNIT_MISSION_REWARD_REQ(unitId, missionId, stepId);
		}

		// Token: 0x0600910C RID: 37132 RVA: 0x003170BE File Offset: 0x003152BE
		private void OnClickCompleteAll()
		{
			NKCPacketSender.Send_NKMPacket_UNIT_MISSION_REWARD_ALL_REQ(this.m_iUnitId);
		}

		// Token: 0x0600910D RID: 37133 RVA: 0x003170CB File Offset: 0x003152CB
		private void Release()
		{
			this.m_MissionStepTempletList = null;
			List<NKMMissionManager.MissionStateData> missionStateDataList = this.m_missionStateDataList;
			if (missionStateDataList != null)
			{
				missionStateDataList.Clear();
			}
			this.m_missionStateDataList = null;
		}

		// Token: 0x04007E4C RID: 32332
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_COLLECTION";

		// Token: 0x04007E4D RID: 32333
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_COLLECTION_ACHIEVEMENT";

		// Token: 0x04007E4E RID: 32334
		private static NKCUIPopupCollectionAchievement m_Instance;

		// Token: 0x04007E4F RID: 32335
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04007E50 RID: 32336
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04007E51 RID: 32337
		public NKCUIComStateButton m_csbtnCompleteAll;

		// Token: 0x04007E52 RID: 32338
		public EventTrigger m_eventBG;

		// Token: 0x04007E53 RID: 32339
		private int m_iUnitId;

		// Token: 0x04007E54 RID: 32340
		private List<NKMUnitMissionStepTemplet> m_MissionStepTempletList;

		// Token: 0x04007E55 RID: 32341
		private List<NKMMissionManager.MissionStateData> m_missionStateDataList = new List<NKMMissionManager.MissionStateData>();
	}
}
