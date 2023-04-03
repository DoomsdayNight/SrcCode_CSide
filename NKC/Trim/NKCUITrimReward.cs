using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Trim
{
	// Token: 0x02000AB0 RID: 2736
	public class NKCUITrimReward : MonoBehaviour
	{
		// Token: 0x060079C8 RID: 31176 RVA: 0x00288750 File Offset: 0x00286950
		public void Init()
		{
			if (this.m_rewardParent != null)
			{
				NKCUISlot[] componentsInChildren = this.m_rewardParent.GetComponentsInChildren<NKCUISlot>(true);
				int num = (componentsInChildren != null) ? componentsInChildren.Length : 0;
				for (int i = 0; i < num; i++)
				{
					this.m_rewardSlotList.Add(componentsInChildren[i]);
				}
			}
		}

		// Token: 0x060079C9 RID: 31177 RVA: 0x0028879C File Offset: 0x0028699C
		public void SetData(int trimId, int trimLevel)
		{
			List<NKCUITrimReward.TrimRewardInfo> list = new List<NKCUITrimReward.TrimRewardInfo>();
			List<NKCUISlot.SlotData> equipRewardList = new List<NKCUISlot.SlotData>();
			NKCUISlot.SlotData slotData8 = null;
			NKCTrimRewardTemplet nkctrimRewardTemplet = NKCTrimRewardTemplet.Find(trimId, trimLevel);
			int num = 0;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				num = nkmuserData.TrimData.GetClearedTrimLevel(trimId);
			}
			bool flag = trimLevel <= num;
			if (nkctrimRewardTemplet != null)
			{
				if (nkctrimRewardTemplet.RewardUserExp > 0)
				{
					NKCUISlot.SlotData slotData2 = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_MISC, 501, 1, 0);
					list.Add(new NKCUITrimReward.TrimRewardInfo(slotData2, false, false));
				}
				if (nkctrimRewardTemplet.FirstClearRewardType != NKM_REWARD_TYPE.RT_NONE)
				{
					NKCUISlot.SlotData slotData3 = NKCUISlot.SlotData.MakeRewardTypeData(nkctrimRewardTemplet.FirstClearRewardType, nkctrimRewardTemplet.FirstClearRewardID, 1, 0);
					list.Add(new NKCUITrimReward.TrimRewardInfo(slotData3, true, false));
				}
				int count = nkctrimRewardTemplet.EventDropIndex.Count;
				for (int i = 0; i < count; i++)
				{
					NKMRewardGroupTemplet groupTemplet = NKMRewardManager.GetRewardGroup(nkctrimRewardTemplet.EventDropIndex[i]);
					if (groupTemplet != null)
					{
						int count2 = groupTemplet.List.Count;
						int j2;
						int j;
						Predicate<NKCUITrimReward.TrimRewardInfo> <>9__0;
						for (j = 0; j < count2; j = j2)
						{
							if (groupTemplet.List[j].intervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
							{
								List<NKCUITrimReward.TrimRewardInfo> list2 = list;
								Predicate<NKCUITrimReward.TrimRewardInfo> match;
								if ((match = <>9__0) == null)
								{
									match = (<>9__0 = ((NKCUITrimReward.TrimRewardInfo e) => e.slotData.ID == groupTemplet.List[j].m_RewardID));
								}
								if (list2.FindIndex(match) < 0)
								{
									NKCUISlot.SlotData slotData4 = NKCUISlot.SlotData.MakeRewardTypeData(groupTemplet.List[j].m_eRewardType, groupTemplet.List[j].m_RewardID, 1, 0);
									list.Add(new NKCUITrimReward.TrimRewardInfo(slotData4, false, true));
								}
							}
							j2 = j + 1;
						}
					}
				}
				if (nkctrimRewardTemplet.FixRewardType != NKM_REWARD_TYPE.RT_NONE)
				{
					NKCUISlot.SlotData slotData5 = NKCUISlot.SlotData.MakeRewardTypeData(nkctrimRewardTemplet.FixRewardType, nkctrimRewardTemplet.FixRewardID, 1, 0);
					list.Add(new NKCUITrimReward.TrimRewardInfo(slotData5, false, false));
				}
				int count3 = nkctrimRewardTemplet.RewardGroupID.Count;
				for (int k = 0; k < count3; k++)
				{
					NKMRewardGroupTemplet groupTemplet = NKMRewardManager.GetRewardGroup(nkctrimRewardTemplet.RewardGroupID[k]);
					if (groupTemplet != null)
					{
						int count4 = groupTemplet.List.Count;
						int j2;
						int j;
						Predicate<NKCUITrimReward.TrimRewardInfo> <>9__1;
						for (j = 0; j < count4; j = j2)
						{
							if (groupTemplet.List[j].intervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
							{
								if (groupTemplet.List[j].m_eRewardType == NKM_REWARD_TYPE.RT_EQUIP)
								{
									NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeRewardTypeData(groupTemplet.List[j].m_eRewardType, groupTemplet.List[j].m_RewardID, 1, 0);
									equipRewardList.Add(item);
								}
								else
								{
									List<NKCUITrimReward.TrimRewardInfo> list3 = list;
									Predicate<NKCUITrimReward.TrimRewardInfo> match2;
									if ((match2 = <>9__1) == null)
									{
										match2 = (<>9__1 = ((NKCUITrimReward.TrimRewardInfo e) => e.slotData.ID == groupTemplet.List[j].m_RewardID));
									}
									if (list3.FindIndex(match2) < 0)
									{
										NKCUISlot.SlotData slotData6 = NKCUISlot.SlotData.MakeRewardTypeData(groupTemplet.List[j].m_eRewardType, groupTemplet.List[j].m_RewardID, 1, 0);
										list.Add(new NKCUITrimReward.TrimRewardInfo(slotData6, false, false));
									}
								}
							}
							j2 = j + 1;
						}
					}
				}
				int count5 = nkctrimRewardTemplet.RewardUnitExp.Count;
				for (int l = 0; l < count5; l++)
				{
					if (nkctrimRewardTemplet.RewardUnitExp[l] > 0)
					{
						int expItemId = 0;
						switch (l)
						{
						case 0:
							expItemId = 1031;
							break;
						case 1:
							expItemId = 1032;
							break;
						case 2:
							expItemId = 1033;
							break;
						}
						if (expItemId != 0 && list.FindIndex((NKCUITrimReward.TrimRewardInfo e) => e.slotData.ID == expItemId) < 0)
						{
							NKCUISlot.SlotData slotData7 = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_MISC, expItemId, 1, 0);
							list.Add(new NKCUITrimReward.TrimRewardInfo(slotData7, false, false));
						}
					}
				}
				if (nkctrimRewardTemplet.RewardCreditMin > 0)
				{
					slotData8 = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_MISC, 1, 1, 0);
				}
			}
			int num2 = 0;
			int count6 = list.Count;
			int count7 = this.m_rewardSlotList.Count;
			for (int m = 0; m < count7; m++)
			{
				if (count6 <= m)
				{
					this.m_rewardSlotList[m].SetActive(false);
				}
				else
				{
					this.m_rewardSlotList[m].SetActive(true);
					this.m_rewardSlotList[m].Init();
					this.m_rewardSlotList[m].SetData(list[m].slotData, true, null);
					if (list[m].isFirstClearReward)
					{
						this.m_rewardSlotList[m].SetFirstGetMark(!flag);
						this.m_rewardSlotList[m].SetCompleteMark(flag);
					}
					else if (list[m].isEventDropReward)
					{
						this.m_rewardSlotList[m].SetEventDropMark(true);
					}
					num2++;
				}
			}
			for (int n = count7; n < count6; n++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_rewardParent);
				if (!(newInstance == null))
				{
					newInstance.transform.localScale = Vector3.one;
					newInstance.SetActive(true);
					newInstance.SetData(list[n].slotData, true, null);
					this.m_rewardSlotList.Add(newInstance);
					if (list[n].isFirstClearReward)
					{
						newInstance.SetFirstGetMark(!flag);
						newInstance.SetCompleteMark(flag);
					}
					else if (list[n].isEventDropReward)
					{
						newInstance.SetEventDropMark(true);
					}
				}
			}
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(902, 1L, 0L, 0), 0);
			if (equipRewardList.Count > 0)
			{
				NKCUISlot nkcuislot;
				if (num2 >= count7)
				{
					nkcuislot = NKCUISlot.GetNewInstance(this.m_rewardParent);
					if (nkcuislot != null)
					{
						nkcuislot.transform.localScale = Vector3.one;
						this.m_rewardSlotList.Add(nkcuislot);
					}
				}
				else
				{
					nkcuislot = this.m_rewardSlotList[num2];
					if (nkcuislot != null)
					{
						nkcuislot.Init();
					}
					num2++;
				}
				if (nkcuislot != null)
				{
					nkcuislot.SetActive(true);
					nkcuislot.SetData(data, false, false, true, delegate(NKCUISlot.SlotData slotData, bool locked)
					{
						List<int> lstID = new List<int>();
						equipRewardList.ForEach(delegate(NKCUISlot.SlotData e)
						{
							lstID.Add(e.ID);
						});
						NKCUISlotListViewer.Instance.OpenRewardList(lstID, NKM_REWARD_TYPE.RT_EQUIP, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
					});
					int maxGrade = 0;
					equipRewardList.ForEach(delegate(NKCUISlot.SlotData e)
					{
						int rewardGrade = NKCUtil.GetRewardGrade(e.ID, NKM_REWARD_TYPE.RT_EQUIP);
						if (maxGrade < rewardGrade)
						{
							maxGrade = rewardGrade;
						}
					});
					nkcuislot.SetBackGround(maxGrade);
				}
			}
			if (slotData8 != null)
			{
				NKCUISlot nkcuislot2;
				if (num2 >= count7)
				{
					nkcuislot2 = NKCUISlot.GetNewInstance(this.m_rewardParent);
					if (nkcuislot2 != null)
					{
						nkcuislot2.transform.localScale = Vector3.one;
						this.m_rewardSlotList.Add(nkcuislot2);
					}
				}
				else
				{
					nkcuislot2 = this.m_rewardSlotList[num2];
					if (nkcuislot2 != null)
					{
						nkcuislot2.Init();
					}
					num2++;
				}
				if (nkcuislot2 != null)
				{
					nkcuislot2.SetActive(true);
					nkcuislot2.SetData(slotData8, true, null);
				}
			}
		}

		// Token: 0x04006689 RID: 26249
		public Transform m_rewardParent;

		// Token: 0x0400668A RID: 26250
		private List<NKCUISlot> m_rewardSlotList = new List<NKCUISlot>();

		// Token: 0x02001805 RID: 6149
		public struct TrimRewardInfo
		{
			// Token: 0x0600B4EC RID: 46316 RVA: 0x00364624 File Offset: 0x00362824
			public TrimRewardInfo(NKCUISlot.SlotData _slotData, bool _isFirstClearReward, bool _isEventDropReward)
			{
				this.slotData = _slotData;
				this.isFirstClearReward = _isFirstClearReward;
				this.isEventDropReward = _isEventDropReward;
			}

			// Token: 0x0400A7E8 RID: 42984
			public NKCUISlot.SlotData slotData;

			// Token: 0x0400A7E9 RID: 42985
			public bool isFirstClearReward;

			// Token: 0x0400A7EA RID: 42986
			public bool isEventDropReward;
		}
	}
}
