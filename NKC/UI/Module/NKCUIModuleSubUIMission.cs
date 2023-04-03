using System;
using System.Collections.Generic;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B26 RID: 2854
	public class NKCUIModuleSubUIMission : NKCUIModuleSubUIBase
	{
		// Token: 0x06008200 RID: 33280 RVA: 0x002BDA5B File Offset: 0x002BBC5B
		public override void Init()
		{
			base.Init();
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAll, new UnityAction(this.OnCompleteAll));
			NKCUIMissionAchieveSlot specialMissionSlot = this.m_SpecialMissionSlot;
			if (specialMissionSlot == null)
			{
				return;
			}
			specialMissionSlot.Init();
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x002BDA8C File Offset: 0x002BBC8C
		public override void OnOpen(NKMEventCollectionIndexTemplet templet)
		{
			base.OnOpen(templet);
			this.m_collectionIndexTemplet = templet;
			if (templet == null)
			{
				return;
			}
			this.m_missionSlotBundleName = templet.EventPrefabId;
			this.m_missionSlotAssetName = templet.EventMissionSlotPrefabId;
			if (!this.m_bScrollRectInit && this.m_scrollRect != null)
			{
				this.m_scrollRect.dOnGetObject += this.GetSlot;
				this.m_scrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_scrollRect.dOnProvideData += this.ProvideData;
				this.m_scrollRect.ContentConstraintCount = 1;
				this.m_scrollRect.TotalCount = 0;
				this.m_scrollRect.PrepareCells(0);
				this.m_bScrollRectInit = true;
			}
			this.m_missionTempletList.Clear();
			foreach (int missionTabId in templet.MissionTabIds)
			{
				List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(missionTabId);
				if (missionTempletListByType != null)
				{
					this.m_missionTempletList.AddRange(missionTempletListByType);
				}
			}
			this.Refresh();
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x002BDBB0 File Offset: 0x002BBDB0
		public override void Refresh()
		{
			base.Refresh();
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			this.m_missionTempletShowList.Clear();
			foreach (NKMMissionTemplet nkmmissionTemplet in this.m_missionTempletList)
			{
				if (nkmmissionTemplet != null)
				{
					if (this.m_hideCompletedMission)
					{
						NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(nkmmissionTemplet);
						if (missionStateData.state == NKMMissionManager.MissionState.COMPLETED || missionStateData.state == NKMMissionManager.MissionState.REPEAT_COMPLETED)
						{
							continue;
						}
					}
					if (this.m_hideLockedMission && nkmmissionTemplet.m_MissionRequire != 0)
					{
						NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
						NKMMissionData nkmmissionData = (myUserData != null) ? myUserData.m_MissionData.GetMissionData(nkmmissionTemplet) : null;
						if (nkmmissionData == null)
						{
							continue;
						}
						if (nkmmissionData.mission_id == nkmmissionTemplet.m_MissionID)
						{
							this.m_missionTempletShowList.Add(nkmmissionTemplet);
							continue;
						}
						nkmmissionData = ((myUserData != null) ? myUserData.m_MissionData.GetMissionDataByMissionId(nkmmissionTemplet.m_MissionRequire) : null);
						if (nkmmissionData == null)
						{
							continue;
						}
						if (nkmmissionData.isComplete && nkmmissionData.mission_id == nkmmissionTemplet.m_MissionRequire)
						{
							this.m_missionTempletShowList.Add(nkmmissionTemplet);
							continue;
						}
						if (nkmmissionData.mission_id <= nkmmissionTemplet.m_MissionRequire)
						{
							continue;
						}
					}
					this.m_missionTempletShowList.Add(nkmmissionTemplet);
				}
			}
			this.m_missionTempletShowList.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
			NKCUtil.SetGameobjectActive(this.m_objAllMissionCompleted, this.m_missionTempletShowList.Count <= 0);
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.TotalCount = this.m_missionTempletShowList.Count;
				this.m_scrollRect.SetIndexPosition(0);
			}
			this.UpdateCompleteAllState();
			int specialMissionData = (this.m_collectionIndexTemplet != null) ? this.m_collectionIndexTemplet.EventMissionAllClearTabId : 0;
			this.SetSpecialMissionData(specialMissionData);
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x002BDD9C File Offset: 0x002BBF9C
		public override void OnClose()
		{
			base.OnClose();
			this.m_missionSlotBundleName = null;
			this.m_missionSlotAssetName = null;
			this.m_collectionIndexTemplet = null;
			this.m_missionTempletList.Clear();
			this.m_missionTempletShowList.Clear();
			this.m_completeEnableTabIdList.Clear();
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x002BDDDC File Offset: 0x002BBFDC
		private void UpdateCompleteAllState()
		{
			this.m_completeEnableTabIdList.Clear();
			NKCUIComStateButton csbtnCompleteAll = this.m_csbtnCompleteAll;
			if (csbtnCompleteAll != null)
			{
				csbtnCompleteAll.Lock(false);
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserMissionData missionData = NKCScenManager.GetScenManager().GetMyUserData().m_MissionData;
			if (missionData == null)
			{
				return;
			}
			if (this.m_collectionIndexTemplet == null)
			{
				return;
			}
			foreach (int num in this.m_collectionIndexTemplet.MissionTabIds)
			{
				if (missionData.CheckCompletableMission(myUserData, num, false))
				{
					NKCUIComStateButton csbtnCompleteAll2 = this.m_csbtnCompleteAll;
					if (csbtnCompleteAll2 != null)
					{
						csbtnCompleteAll2.UnLock(false);
					}
					this.m_completeEnableTabIdList.Add(num);
				}
			}
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x002BDEA0 File Offset: 0x002BC0A0
		private void SetSpecialMissionData(int specialMissionTabID)
		{
			if (this.m_SpecialMissionSlot == null)
			{
				return;
			}
			if (specialMissionTabID <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_SpecialMissionSlot, false);
				return;
			}
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(specialMissionTabID);
			NKMMissionTemplet nkmmissionTemplet = null;
			NKMMissionTemplet nkmmissionTemplet2 = null;
			NKMMissionTemplet nkmmissionTemplet3 = null;
			bool flag = false;
			foreach (NKMMissionTemplet nkmmissionTemplet4 in missionTempletListByType)
			{
				switch (NKMMissionManager.GetMissionStateData(nkmmissionTemplet4).state)
				{
				case NKMMissionManager.MissionState.CAN_COMPLETE:
				case NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE:
				case NKMMissionManager.MissionState.ONGOING:
					nkmmissionTemplet = nkmmissionTemplet4;
					flag = true;
					break;
				case NKMMissionManager.MissionState.REPEAT_COMPLETED:
				case NKMMissionManager.MissionState.COMPLETED:
					nkmmissionTemplet2 = nkmmissionTemplet4;
					break;
				case NKMMissionManager.MissionState.LOCKED:
					if (nkmmissionTemplet3 == null)
					{
						nkmmissionTemplet3 = nkmmissionTemplet4;
					}
					break;
				}
				if (flag)
				{
					break;
				}
			}
			bool bValue = false;
			if (nkmmissionTemplet == null)
			{
				if (nkmmissionTemplet2 != null)
				{
					nkmmissionTemplet = nkmmissionTemplet2;
					bValue = true;
				}
				else
				{
					if (nkmmissionTemplet3 == null)
					{
						NKCUtil.SetGameobjectActive(this.m_SpecialMissionSlot, false);
						return;
					}
					nkmmissionTemplet = nkmmissionTemplet3;
				}
			}
			NKCUIMissionAchieveSlot specialMissionSlot = this.m_SpecialMissionSlot;
			if (specialMissionSlot != null)
			{
				specialMissionSlot.SetData(nkmmissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
			}
			NKCUtil.SetGameobjectActive(this.m_SpecialMissionSlot, true);
			NKCUtil.SetGameobjectActive(this.m_objSpecialMissionCompleted, bValue);
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x002BDFC8 File Offset: 0x002BC1C8
		private void OnClickMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.Refresh), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x002BE03C File Offset: 0x002BC23C
		private void OnClickComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.Refresh), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x002BE0C4 File Offset: 0x002BC2C4
		private RectTransform GetSlot(int index)
		{
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(this.m_missionSlotBundleName, this.m_missionSlotAssetName);
			NKCUIMissionAchieveSlot newInstance = NKCUIMissionAchieveSlot.GetNewInstance(null, nkmassetName.m_BundleName, nkmassetName.m_AssetName);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008209 RID: 33289 RVA: 0x002BE100 File Offset: 0x002BC300
		private void ReturnSlot(Transform tr)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x0600820A RID: 33290 RVA: 0x002BE138 File Offset: 0x002BC338
		private void ProvideData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_missionTempletShowList.Count > index)
			{
				component.SetData(this.m_missionTempletShowList[index], new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
			}
		}

		// Token: 0x0600820B RID: 33291 RVA: 0x002BE190 File Offset: 0x002BC390
		private void OnCompleteAll()
		{
			foreach (int missionTabID in this.m_completeEnableTabIdList)
			{
				NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(missionTabID);
			}
		}

		// Token: 0x04006E21 RID: 28193
		[Header("�̼� ��ũ�ѷ�Ʈ")]
		public LoopScrollRect m_scrollRect;

		// Token: 0x04006E22 RID: 28194
		[Header("�ϰ� �Ϸ� ��ư")]
		public NKCUIComStateButton m_csbtnCompleteAll;

		// Token: 0x04006E23 RID: 28195
		[Header("����� �̼� ����")]
		public NKCUIMissionAchieveSlot m_SpecialMissionSlot;

		// Token: 0x04006E24 RID: 28196
		[Space]
		public GameObject m_objAllMissionCompleted;

		// Token: 0x04006E25 RID: 28197
		public GameObject m_objSpecialMissionCompleted;

		// Token: 0x04006E26 RID: 28198
		public bool m_hideCompletedMission;

		// Token: 0x04006E27 RID: 28199
		public bool m_hideLockedMission;

		// Token: 0x04006E28 RID: 28200
		private string m_missionSlotBundleName;

		// Token: 0x04006E29 RID: 28201
		private string m_missionSlotAssetName;

		// Token: 0x04006E2A RID: 28202
		private NKMEventCollectionIndexTemplet m_collectionIndexTemplet;

		// Token: 0x04006E2B RID: 28203
		private List<NKMMissionTemplet> m_missionTempletList = new List<NKMMissionTemplet>();

		// Token: 0x04006E2C RID: 28204
		private List<NKMMissionTemplet> m_missionTempletShowList = new List<NKMMissionTemplet>();

		// Token: 0x04006E2D RID: 28205
		private List<int> m_completeEnableTabIdList = new List<int>();

		// Token: 0x04006E2E RID: 28206
		private bool m_bScrollRectInit;
	}
}
