using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000931 RID: 2353
	public class EventDropReward : StageRewardData
	{
		// Token: 0x06005E00 RID: 24064 RVA: 0x001D0CF8 File Offset: 0x001CEEF8
		public EventDropReward(Transform slotParent, int eventDroprewardIndex) : base(slotParent)
		{
			this.m_iEventDropRewardIndex = eventDroprewardIndex;
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x001D0D08 File Offset: 0x001CEF08
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || !NKMEpisodeMgr.CheckStageHasEventDrop(stageTemplet))
			{
				return;
			}
			this.m_iRewardGroupId = stageTemplet.GetEventDropRewardGroupID(this.m_iEventDropRewardIndex);
			NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(this.m_iRewardGroupId);
			if (rewardGroup == null)
			{
				return;
			}
			int count = rewardGroup.List.Count;
			List<NKMRewardTemplet> list = new List<NKMRewardTemplet>();
			for (int i = 0; i < count; i++)
			{
				if (rewardGroup.List[i].intervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
				{
					list.Add(rewardGroup.List[i]);
				}
			}
			int count2 = list.Count;
			if (count2 <= 0)
			{
				return;
			}
			if (count2 > 1)
			{
				int num = -1;
				for (int j = 0; j < list.Count; j++)
				{
					int rewardGrade = NKCUtil.GetRewardGrade(list[j].m_RewardID, list[j].m_eRewardType);
					if (num < rewardGrade)
					{
						num = rewardGrade;
					}
				}
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(903, 1L, 0L, 0), 0);
				this.m_cSlot.SetData(data, false, false, true, new NKCUISlot.OnClick(this.OnClickEventDropSlot));
				this.m_cSlot.SetBackGround(num);
				this.m_cSlot.SetEventDropMark(true);
				listSlotToShow.Add(this.m_cSlot);
				return;
			}
			if (count2 == 1)
			{
				NKMRewardTemplet nkmrewardTemplet = list[0];
				NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardTemplet.m_eRewardType, nkmrewardTemplet.m_RewardID, 1, 0);
				this.m_cSlot.SetData(data2, false, false, true, null);
				this.m_cSlot.SetOpenItemBoxOnClick();
				this.m_cSlot.SetEventDropMark(true);
				listSlotToShow.Add(this.m_cSlot);
			}
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x001D0EA0 File Offset: 0x001CF0A0
		private void OnClickEventDropSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(this.m_iRewardGroupId);
			if (rewardGroup == null)
			{
				return;
			}
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<int> hashSet2 = new HashSet<int>();
			HashSet<int> hashSet3 = new HashSet<int>();
			HashSet<int> hashSet4 = new HashSet<int>();
			HashSet<int> hashSet5 = new HashSet<int>();
			int count = rewardGroup.List.Count;
			List<NKMRewardTemplet> list = new List<NKMRewardTemplet>();
			for (int i = 0; i < count; i++)
			{
				if (rewardGroup.List[i].intervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
				{
					list.Add(rewardGroup.List[i]);
				}
			}
			int count2 = list.Count;
			if (count2 <= 0)
			{
				return;
			}
			for (int j = 0; j < count2; j++)
			{
				NKM_REWARD_TYPE eRewardType = list[j].m_eRewardType;
				switch (eRewardType)
				{
				case NKM_REWARD_TYPE.RT_UNIT:
					hashSet.Add(list[j].m_RewardID);
					break;
				case NKM_REWARD_TYPE.RT_SHIP:
				case NKM_REWARD_TYPE.RT_USER_EXP:
					break;
				case NKM_REWARD_TYPE.RT_MISC:
					hashSet5.Add(list[j].m_RewardID);
					break;
				case NKM_REWARD_TYPE.RT_EQUIP:
					hashSet3.Add(list[j].m_RewardID);
					break;
				case NKM_REWARD_TYPE.RT_MOLD:
					hashSet4.Add(list[j].m_RewardID);
					break;
				default:
					if (eRewardType == NKM_REWARD_TYPE.RT_OPERATOR)
					{
						hashSet2.Add(list[j].m_RewardID);
					}
					break;
				}
			}
			Dictionary<NKM_REWARD_TYPE, List<int>> dictionary = new Dictionary<NKM_REWARD_TYPE, List<int>>();
			dictionary.Add(NKM_REWARD_TYPE.RT_UNIT, NKCUIComDungeonRewardList.GetUnitRewardIdList(hashSet, hashSet2));
			dictionary.Add(NKM_REWARD_TYPE.RT_EQUIP, NKCUIComDungeonRewardList.GetEquipRewardIdList(hashSet3));
			dictionary.Add(NKM_REWARD_TYPE.RT_MOLD, NKCUIComDungeonRewardList.GetMoldRewardIdList(hashSet4));
			dictionary.Add(NKM_REWARD_TYPE.RT_MISC, NKCUIComDungeonRewardList.GetMiscRewardIdList(hashSet5));
			NKCUISlotListViewer.Instance.OpenRewardList(dictionary, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x04004A37 RID: 18999
		private int m_iEventDropRewardIndex;

		// Token: 0x04004A38 RID: 19000
		private int m_iRewardGroupId;
	}
}
