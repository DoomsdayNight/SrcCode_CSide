using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.UI.Result;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C2D RID: 3117
	public class NKCUICollectionTeamUp : MonoBehaviour
	{
		// Token: 0x0600907B RID: 36987 RVA: 0x003132D8 File Offset: 0x003114D8
		public void Init(NKCUICollection.OnSyncCollectingData callback, NKCUICollection.OnNotify notify)
		{
			if (null != this.m_LoopVerticalScrollFlexibleRect)
			{
				this.m_LoopVerticalScrollFlexibleRect.dOnGetObject += this.MakeTeamUpSlot;
				this.m_LoopVerticalScrollFlexibleRect.dOnReturnObject += this.ReturnTeamUpSlot;
				this.m_LoopVerticalScrollFlexibleRect.dOnProvideData += this.ProvideTeamUpSlotData;
				NKCUtil.SetScrollHotKey(this.m_LoopVerticalScrollFlexibleRect, null);
			}
			this.dOnSyncCollectingData = callback;
			this.dOnNotify = notify;
		}

		// Token: 0x0600907C RID: 36988 RVA: 0x00313354 File Offset: 0x00311554
		public void Open()
		{
			bool bNotify = this.OpenTeamUpData();
			if (!NKCUnitMissionManager.GetOpenTagCollectionTeamUp())
			{
				bNotify = false;
			}
			NKCUICollection.OnNotify onNotify = this.dOnNotify;
			if (onNotify == null)
			{
				return;
			}
			onNotify(bNotify);
		}

		// Token: 0x0600907D RID: 36989 RVA: 0x00313382 File Offset: 0x00311582
		public void Clear()
		{
			NKCUICollectionUnitInfo.CheckInstanceAndClose();
		}

		// Token: 0x0600907E RID: 36990 RVA: 0x0031338C File Offset: 0x0031158C
		private RectTransform MakeTeamUpSlot(int index)
		{
			if (this.m_stkTeamUpPool.Count > 0)
			{
				RectTransform rectTransform = this.m_stkTeamUpPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUICollectionTeamUpSlot nkcuicollectionTeamUpSlot = UnityEngine.Object.Instantiate<NKCUICollectionTeamUpSlot>(this.m_pfTeamUpSlot);
			nkcuicollectionTeamUpSlot.Init();
			nkcuicollectionTeamUpSlot.transform.localPosition = Vector3.zero;
			nkcuicollectionTeamUpSlot.transform.localScale = Vector3.one;
			return nkcuicollectionTeamUpSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600907F RID: 36991 RVA: 0x003133F0 File Offset: 0x003115F0
		private void ReturnTeamUpSlot(Transform go)
		{
			NKCUICollectionTeamUpSlot component = go.GetComponent<NKCUICollectionTeamUpSlot>();
			List<RectTransform> rentalSlot = component.GetRentalSlot();
			for (int i = 0; i < rentalSlot.Count; i++)
			{
				rentalSlot[i].SetParent(this.m_rtTeamUpSlotIconPool);
				this.m_stkTeamUpIconPool.Push(rentalSlot[i]);
			}
			component.ClearRentalList();
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rtTeamUpSlotPool);
			this.m_stkTeamUpPool.Push(go.GetComponent<RectTransform>());
		}

		// Token: 0x06009080 RID: 36992 RVA: 0x0031346C File Offset: 0x0031166C
		private List<RectTransform> GetUISlot(int iCnt)
		{
			List<RectTransform> list = new List<RectTransform>();
			for (int i = 0; i < iCnt; i++)
			{
				if (this.m_stkTeamUpIconPool.Count > 0)
				{
					RectTransform item = this.m_stkTeamUpIconPool.Pop();
					list.Add(item);
				}
				else
				{
					NKCUISlot nkcuislot = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfUISlot);
					nkcuislot.Init();
					nkcuislot.transform.localPosition = Vector3.zero;
					nkcuislot.transform.localScale = Vector3.one;
					RectTransform component = nkcuislot.GetComponent<RectTransform>();
					list.Add(component);
				}
			}
			return list;
		}

		// Token: 0x06009081 RID: 36993 RVA: 0x003134F0 File Offset: 0x003116F0
		private void ProvideTeamUpSlotData(Transform tr, int idx)
		{
			NKCUICollectionTeamUpSlot component = tr.GetComponent<NKCUICollectionTeamUpSlot>();
			if (component == null)
			{
				return;
			}
			List<RectTransform> uislot = this.GetUISlot(this.m_list_TeamUp[idx].m_lstUnit.Count);
			component.SetData(this.m_list_TeamUp[idx], new NKCUICollectionTeamUpSlot.OnClicked(this.TryGetReward), true, new NKCUISlot.OnClick(this.TryUnitInfoOpen), uislot);
			this.m_lstTeamUpSlot.Add(component);
		}

		// Token: 0x06009082 RID: 36994 RVA: 0x00313563 File Offset: 0x00311763
		public void TryGetReward(int teamID)
		{
			if (!NKCUnitMissionManager.GetOpenTagCollectionTeamUp())
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_TEAM_COLLECTION_REWARD_REQ(teamID);
		}

		// Token: 0x06009083 RID: 36995 RVA: 0x00313574 File Offset: 0x00311774
		public static List<NKCUICollectionTeamUp.TeamUpSlotData> UpdateTeamUpList(ref int hasTeamUpCount, ref int totalTeamUpCount, NKMArmyData armyData, bool getTeamUpList, out bool bNotify)
		{
			List<NKCUICollectionTeamUp.TeamUpSlotData> list = null;
			if (getTeamUpList)
			{
				list = new List<NKCUICollectionTeamUp.TeamUpSlotData>();
			}
			bNotify = false;
			if (armyData == null)
			{
				return null;
			}
			foreach (NKMCollectionTeamUpGroupTemplet nkmcollectionTeamUpGroupTemplet in NKMTempletContainer<NKMCollectionTeamUpGroupTemplet>.Values)
			{
				if (nkmcollectionTeamUpGroupTemplet != null)
				{
					List<int> list2 = new List<int>();
					if (nkmcollectionTeamUpGroupTemplet.UnitIDList != null)
					{
						int count = nkmcollectionTeamUpGroupTemplet.UnitIDList.Count;
						for (int i = 0; i < count; i++)
						{
							if (NKMUnitManager.GetUnitTempletBase(nkmcollectionTeamUpGroupTemplet.UnitIDList[i]).PickupEnableByTag)
							{
								list2.Add(nkmcollectionTeamUpGroupTemplet.UnitIDList[i]);
							}
						}
					}
					if (list2.Count > 0)
					{
						int unitCollectCount = armyData.GetUnitCollectCount(list2);
						if (list2.Count <= unitCollectCount)
						{
							hasTeamUpCount++;
						}
						totalTeamUpCount++;
						if (getTeamUpList)
						{
							NKCUICollectionTeamUp.eTeamUpRewardState eTeamUpRewardState = NKCUICollectionTeamUp.eTeamUpRewardState.RS_NONE;
							NKMTeamCollectionData teamCollectionData = armyData.GetTeamCollectionData(nkmcollectionTeamUpGroupTemplet.TeamID);
							if (teamCollectionData != null && teamCollectionData.IsRewardComplete())
							{
								eTeamUpRewardState = NKCUICollectionTeamUp.eTeamUpRewardState.RS_COMPLETE;
							}
							if (eTeamUpRewardState == NKCUICollectionTeamUp.eTeamUpRewardState.RS_NONE)
							{
								if (nkmcollectionTeamUpGroupTemplet.RewardCriteria <= unitCollectCount)
								{
									bNotify = true;
									eTeamUpRewardState = NKCUICollectionTeamUp.eTeamUpRewardState.RS_READY;
								}
								else
								{
									eTeamUpRewardState = NKCUICollectionTeamUp.eTeamUpRewardState.RS_NOT_READY;
								}
							}
							NKCUICollectionTeamUp.TeamUpSlotData item = new NKCUICollectionTeamUp.TeamUpSlotData(nkmcollectionTeamUpGroupTemplet.TeamID, NKCStringTable.GetString(nkmcollectionTeamUpGroupTemplet.TeamName, false), unitCollectCount, list2.Count, nkmcollectionTeamUpGroupTemplet.RewardID, nkmcollectionTeamUpGroupTemplet.RewardValue, nkmcollectionTeamUpGroupTemplet.RewardType, list2, eTeamUpRewardState);
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06009084 RID: 36996 RVA: 0x003136E8 File Offset: 0x003118E8
		private bool OpenTeamUpData()
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData == null)
			{
				return false;
			}
			this.m_list_TeamUp.Clear();
			this.m_iHasTeamUpCount = 0;
			this.m_iTotalTeamUpCount = 0;
			bool result;
			this.m_list_TeamUp = NKCUICollectionTeamUp.UpdateTeamUpList(ref this.m_iHasTeamUpCount, ref this.m_iTotalTeamUpCount, armyData, true, out result);
			if (!this.m_bPrepareTeamUpSlot)
			{
				this.m_lstTeamUpSlot.Clear();
				this.m_bPrepareTeamUpSlot = true;
				this.m_LoopVerticalScrollFlexibleRect.TotalCount = this.m_list_TeamUp.Count;
				this.m_LoopVerticalScrollFlexibleRect.PrepareCells(0);
				this.m_LoopVerticalScrollFlexibleRect.velocity = new Vector2(0f, 0f);
				this.m_LoopVerticalScrollFlexibleRect.SetIndexPosition(0);
			}
			this.SyncCollectingUnitData();
			return result;
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x003137A4 File Offset: 0x003119A4
		private void UpdateTeamUpData()
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData == null)
			{
				return;
			}
			bool flag = false;
			foreach (NKMCollectionTeamUpGroupTemplet nkmcollectionTeamUpGroupTemplet in NKMTempletContainer<NKMCollectionTeamUpGroupTemplet>.Values)
			{
				if (nkmcollectionTeamUpGroupTemplet != null)
				{
					NKMTeamCollectionData collectionData = armyData.GetTeamCollectionData(nkmcollectionTeamUpGroupTemplet.TeamID);
					if (collectionData != null)
					{
						NKCUICollectionTeamUpSlot nkcuicollectionTeamUpSlot = this.m_lstTeamUpSlot.Find((NKCUICollectionTeamUpSlot x) => x.GetTeamID() == collectionData.TeamID);
						NKCUICollectionTeamUp.TeamUpSlotData slotData = this.m_list_TeamUp.Find((NKCUICollectionTeamUp.TeamUpSlotData x) => x.m_TeamID == collectionData.TeamID);
						if (nkcuicollectionTeamUpSlot != null)
						{
							nkcuicollectionTeamUpSlot.SetData(slotData, null, false, null, null);
						}
					}
					else if (!flag)
					{
						int unitCollectCount = armyData.GetUnitCollectCount(nkmcollectionTeamUpGroupTemplet.UnitIDList);
						if (nkmcollectionTeamUpGroupTemplet.RewardCriteria <= unitCollectCount)
						{
							flag = true;
						}
					}
				}
			}
			if (!NKCUnitMissionManager.GetOpenTagCollectionTeamUp())
			{
				flag = false;
			}
			NKCUICollection.OnNotify onNotify = this.dOnNotify;
			if (onNotify == null)
			{
				return;
			}
			onNotify(flag);
		}

		// Token: 0x06009086 RID: 36998 RVA: 0x003138A8 File Offset: 0x00311AA8
		private void SyncCollectingUnitData()
		{
			if (this.dOnSyncCollectingData != null)
			{
				this.dOnSyncCollectingData(NKCUICollection.CollectionType.CT_TEAM_UP, this.m_iHasTeamUpCount, this.m_iTotalTeamUpCount);
			}
		}

		// Token: 0x06009087 RID: 36999 RVA: 0x003138CC File Offset: 0x00311ACC
		public void OnRecvTeamCollectionRewardAck(NKMPacket_TEAM_COLLECTION_REWARD_ACK sPacket)
		{
			if (sPacket.rewardData.MiscItemDataList.Count > 0)
			{
				NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.AddItemMisc(sPacket.rewardData.MiscItemDataList);
				NKCUIResult.Instance.OpenItemGain(sPacket.rewardData.MiscItemDataList, NKCUtilString.GET_STRING_ITEM_GAIN, NKCUtilString.GET_STRING_CONGRATULATION, null);
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData == null)
			{
				return;
			}
			NKCUICollectionTeamUp.eTeamUpRewardState state = NKCUICollectionTeamUp.eTeamUpRewardState.RS_READY;
			if (sPacket.teamCollectionData.IsRewardComplete())
			{
				state = NKCUICollectionTeamUp.eTeamUpRewardState.RS_COMPLETE;
			}
			this.SetSlotDataState(sPacket.teamCollectionData.TeamID, state);
			armyData.AddTeamCollectionData(sPacket.teamCollectionData);
			this.UpdateTeamUpData();
		}

		// Token: 0x06009088 RID: 37000 RVA: 0x00313970 File Offset: 0x00311B70
		public void TryUnitInfoOpen(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.m_lstCurUnitData.Clear();
			NKMCollectionTeamUpGroupTemplet nkmcollectionTeamUpGroupTemplet = NKMCollectionTeamUpGroupTemplet.Find(slotData.GroupID);
			if (nkmcollectionTeamUpGroupTemplet != null)
			{
				int num = -1;
				for (int i = 0; i < nkmcollectionTeamUpGroupTemplet.UnitIDList.Count; i++)
				{
					if (NKMUnitTempletBase.Find(nkmcollectionTeamUpGroupTemplet.UnitIDList[i]).PickupEnableByTag)
					{
						if (nkmcollectionTeamUpGroupTemplet.UnitIDList[i] == slotData.ID)
						{
							num = this.m_lstCurUnitData.Count;
						}
						this.m_lstCurUnitData.Add(NKCUtil.MakeDummyUnit(nkmcollectionTeamUpGroupTemplet.UnitIDList[i], true));
					}
				}
				if (num >= 0 && this.m_lstCurUnitData.Count > 0)
				{
					NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(this.m_lstCurUnitData, num);
					NKCUICollectionUnitInfo.CheckInstanceAndOpen(this.m_lstCurUnitData[num], openOption, null, NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, false, NKCUIUpsideMenu.eMode.Normal);
				}
			}
		}

		// Token: 0x06009089 RID: 37001 RVA: 0x00313A3C File Offset: 0x00311C3C
		private void SetSlotDataState(int teamID, NKCUICollectionTeamUp.eTeamUpRewardState state)
		{
			NKCUICollectionTeamUp.TeamUpSlotData teamUpSlotData = this.m_list_TeamUp.Find((NKCUICollectionTeamUp.TeamUpSlotData x) => x.m_TeamID == teamID);
			if (teamUpSlotData != null)
			{
				teamUpSlotData.m_RewardState = state;
			}
		}

		// Token: 0x04007DA2 RID: 32162
		public NKCUICollectionTeamUpSlot m_pfTeamUpSlot;

		// Token: 0x04007DA3 RID: 32163
		public LoopVerticalScrollFlexibleRect m_LoopVerticalScrollFlexibleRect;

		// Token: 0x04007DA4 RID: 32164
		public RectTransform m_rtTeamUpSlotPool;

		// Token: 0x04007DA5 RID: 32165
		private Stack<RectTransform> m_stkTeamUpPool = new Stack<RectTransform>();

		// Token: 0x04007DA6 RID: 32166
		private NKCUICollection.OnSyncCollectingData dOnSyncCollectingData;

		// Token: 0x04007DA7 RID: 32167
		public NKCUISlot m_pfUISlot;

		// Token: 0x04007DA8 RID: 32168
		public RectTransform m_rtTeamUpSlotIconPool;

		// Token: 0x04007DA9 RID: 32169
		private Stack<RectTransform> m_stkTeamUpIconPool = new Stack<RectTransform>();

		// Token: 0x04007DAA RID: 32170
		private NKCUICollection.OnNotify dOnNotify;

		// Token: 0x04007DAB RID: 32171
		private List<NKCUICollectionTeamUpSlot> m_lstTeamUpSlot = new List<NKCUICollectionTeamUpSlot>();

		// Token: 0x04007DAC RID: 32172
		private List<NKCUICollectionTeamUp.TeamUpSlotData> m_list_TeamUp = new List<NKCUICollectionTeamUp.TeamUpSlotData>();

		// Token: 0x04007DAD RID: 32173
		private int m_iTotalTeamUpCount;

		// Token: 0x04007DAE RID: 32174
		private int m_iHasTeamUpCount;

		// Token: 0x04007DAF RID: 32175
		private bool m_bPrepareTeamUpSlot;

		// Token: 0x04007DB0 RID: 32176
		private List<NKMUnitData> m_lstCurUnitData = new List<NKMUnitData>();

		// Token: 0x04007DB1 RID: 32177
		private List<long> m_listSelectedUnit = new List<long>();

		// Token: 0x020019F0 RID: 6640
		public enum eTeamUpRewardState
		{
			// Token: 0x0400AD51 RID: 44369
			RS_NONE = -1,
			// Token: 0x0400AD52 RID: 44370
			RS_NOT_READY,
			// Token: 0x0400AD53 RID: 44371
			RS_READY,
			// Token: 0x0400AD54 RID: 44372
			RS_COMPLETE
		}

		// Token: 0x020019F1 RID: 6641
		public class TeamUpSlotData
		{
			// Token: 0x0600BA9E RID: 47774 RVA: 0x0036E1F8 File Offset: 0x0036C3F8
			public TeamUpSlotData(int TeamID, string Name, int unitCnt, int RewardCriteria, int RewardID, int RewardVal, NKM_REWARD_TYPE RewardType, List<int> lstUnit, NKCUICollectionTeamUp.eTeamUpRewardState RewardState)
			{
				this.m_TeamID = TeamID;
				this.m_TeamName = Name;
				this.m_RewardCriteria = RewardCriteria;
				this.m_RewardID = RewardID;
				this.m_RewardValue = RewardVal;
				this.m_RewardType = RewardType;
				this.m_lstUnit = lstUnit;
				this.m_HasUnitCount = unitCnt;
				this.m_RewardState = RewardState;
			}

			// Token: 0x0400AD55 RID: 44373
			public readonly int m_TeamID;

			// Token: 0x0400AD56 RID: 44374
			public readonly string m_TeamName;

			// Token: 0x0400AD57 RID: 44375
			public readonly int m_RewardCriteria;

			// Token: 0x0400AD58 RID: 44376
			public readonly int m_RewardID;

			// Token: 0x0400AD59 RID: 44377
			public readonly int m_RewardValue;

			// Token: 0x0400AD5A RID: 44378
			public readonly NKM_REWARD_TYPE m_RewardType;

			// Token: 0x0400AD5B RID: 44379
			public NKCUICollectionTeamUp.eTeamUpRewardState m_RewardState = NKCUICollectionTeamUp.eTeamUpRewardState.RS_NONE;

			// Token: 0x0400AD5C RID: 44380
			public int m_HasUnitCount;

			// Token: 0x0400AD5D RID: 44381
			public List<int> m_lstUnit;
		}
	}
}
