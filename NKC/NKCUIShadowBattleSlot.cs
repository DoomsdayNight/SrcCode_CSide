using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Mode;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009DB RID: 2523
	public class NKCUIShadowBattleSlot : MonoBehaviour
	{
		// Token: 0x06006C4B RID: 27723 RVA: 0x002359BF File Offset: 0x00233BBF
		public void Init()
		{
			NKCUIComStateButton btnBattle = this.m_btnBattle;
			if (btnBattle != null)
			{
				btnBattle.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnBattle2 = this.m_btnBattle;
			if (btnBattle2 == null)
			{
				return;
			}
			btnBattle2.PointerClick.AddListener(new UnityAction(this.TouchBattle));
		}

		// Token: 0x06006C4C RID: 27724 RVA: 0x002359F8 File Offset: 0x00233BF8
		public void SetData(NKMShadowBattleTemplet battleTemplet, NKMPalaceDungeonData dungeonData, int current_order, NKCUIShadowBattleSlot.OnTouchBattle onTouchBattle)
		{
			this.m_templet = battleTemplet;
			this.dOnTouchBattle = onTouchBattle;
			NKCUtil.SetLabelText(this.m_txtNumber, battleTemplet.BATTLE_ORDER.ToString("D2"));
			string msg = "-:--:--";
			string hexRGB = "#FFFFFF";
			if (dungeonData != null && dungeonData.bestTime > 0)
			{
				msg = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)dungeonData.bestTime));
				hexRGB = "#FCCE3E";
			}
			NKCUtil.SetLabelTextColor(this.m_txtBestTime, NKCUtil.GetColor(hexRGB));
			NKCUtil.SetLabelText(this.m_txtBestTime, msg);
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(battleTemplet.DUNGEON_ID);
			if (dungeonTempletBase == null)
			{
				Debug.LogError(string.Format("dungeonTempletBase  is null - battleTemplet.DUNGEON_ID : {0}", battleTemplet.DUNGEON_ID));
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (dungeonTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_txtEnemyName, dungeonTempletBase.GetDungeonName());
				string text = (battleTemplet.PALACE_BATTLE_TYPE == PALACE_BATTLE_TYPE.PBT_BOSS) ? "DA1515" : "FCCE3E";
				NKCUtil.SetLabelText(this.m_txtEnemyLv, NKCUtilString.GET_SHADOW_BATTLE_ENEMY_LEVEL, new object[]
				{
					text,
					dungeonTempletBase.m_DungeonLevel
				});
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_FACE_CARD", battleTemplet.PALACE_BATTLE_IMG, false);
			NKCUtil.SetImageSprite(this.m_imgEnemy, orLoadAssetResource, true);
			NKCUtil.SetGameobjectActive(this.m_objBoss, battleTemplet.PALACE_BATTLE_TYPE == PALACE_BATTLE_TYPE.PBT_BOSS);
			NKCUtil.SetGameobjectActive(this.m_objLock, battleTemplet.BATTLE_ORDER > current_order);
			NKCUtil.SetGameobjectActive(this.m_objCurrent, battleTemplet.BATTLE_ORDER == current_order);
			bool flag = battleTemplet.BATTLE_ORDER < current_order;
			NKCUtil.SetGameobjectActive(this.m_objClear, flag);
			NKCUtil.SetGameobjectActive(this.m_objButtonEnable, !flag);
			NKCUtil.SetGameobjectActive(this.m_objButtonDisable, flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_txtClearName, NKCUtilString.GET_SHADOW_BATTLE_CLEAR_NUM, new object[]
				{
					battleTemplet.BATTLE_ORDER
				});
				msg = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)dungeonData.recentTime));
				NKCUtil.SetLabelText(this.m_txtClearTime, msg);
			}
			List<int> lstRewardGroup = (from v in dungeonTempletBase.m_listDungeonReward
			select v.m_RewardGroupID).ToList<int>();
			List<NKMRewardTemplet> list = new List<NKMRewardTemplet>();
			this.m_lstRewardGroupID.Clear();
			this.DivisionReward(lstRewardGroup, ref list, ref this.m_lstRewardGroupID);
			bool flag2 = NKCUtil.CheckExistRewardType(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_UNIT) || NKCUtil.CheckExistRewardType(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_OPERATOR);
			bool flag3 = NKCUtil.CheckExistRewardType(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_EQUIP);
			bool flag4 = NKCUtil.CheckExistRewardType(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_MOLD);
			bool flag5 = NKCUtil.CheckExistRewardType(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_MISC);
			int count = this.m_lstRewardSlot.Count;
			int num = list.Count;
			if (dungeonTempletBase.m_RewardUserEXP > 0)
			{
				num++;
			}
			if (dungeonTempletBase.m_RewardCredit_Min > 0)
			{
				num++;
			}
			if (dungeonTempletBase.m_RewardEternium_Min > 0)
			{
				num++;
			}
			if (dungeonTempletBase.m_RewardInformation_Min > 0)
			{
				num++;
			}
			if (flag2)
			{
				num++;
			}
			if (flag3)
			{
				num++;
			}
			if (flag4)
			{
				num++;
			}
			if (flag5)
			{
				num++;
			}
			for (int i = count; i < num; i++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_trReward);
				newInstance.Init();
				this.m_lstRewardSlot.Add(newInstance);
			}
			int j = 0;
			if (dungeonTempletBase.m_RewardUserEXP > 0)
			{
				this.SetMiscSlot(this.m_lstRewardSlot[j++], 501, dungeonTempletBase.m_RewardUserEXP);
			}
			if (dungeonTempletBase.m_RewardCredit_Min > 0)
			{
				this.SetMiscSlot(this.m_lstRewardSlot[j++], 1, dungeonTempletBase.m_RewardCredit_Min);
			}
			if (dungeonTempletBase.m_RewardEternium_Min > 0)
			{
				this.SetMiscSlot(this.m_lstRewardSlot[j++], 2, dungeonTempletBase.m_RewardEternium_Min);
			}
			if (dungeonTempletBase.m_RewardInformation_Min > 0)
			{
				this.SetMiscSlot(this.m_lstRewardSlot[j++], 3, dungeonTempletBase.m_RewardInformation_Min);
			}
			for (int k = 0; k < list.Count; k++)
			{
				NKCUISlot nkcuislot = this.m_lstRewardSlot[j];
				NKMRewardTemplet nkmrewardTemplet = list[k];
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(new NKMRewardInfo
				{
					rewardType = nkmrewardTemplet.m_eRewardType,
					ID = nkmrewardTemplet.m_RewardID,
					Count = nkmrewardTemplet.m_Quantity_Min
				}, 0);
				nkcuislot.SetData(data, true, null);
				NKCUtil.SetGameobjectActive(nkcuislot, true);
				j++;
			}
			if (flag2)
			{
				int maxGradeInRewardGroups = NKCUtil.GetMaxGradeInRewardGroups(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_UNIT);
				int maxGradeInRewardGroups2 = NKCUtil.GetMaxGradeInRewardGroups(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_OPERATOR);
				this.SetRandomSlot(this.m_lstRewardSlot[j++], 901, new NKCUISlot.OnClick(this.OnClickRandomUnitSlot), Mathf.Max(maxGradeInRewardGroups, maxGradeInRewardGroups2));
			}
			if (flag3)
			{
				int maxGradeInRewardGroups3 = NKCUtil.GetMaxGradeInRewardGroups(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_EQUIP);
				this.SetRandomSlot(this.m_lstRewardSlot[j++], 902, new NKCUISlot.OnClick(this.OnClickRandomEquipSlot), maxGradeInRewardGroups3);
			}
			if (flag4)
			{
				int maxGradeInRewardGroups4 = NKCUtil.GetMaxGradeInRewardGroups(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_MOLD);
				this.SetRandomSlot(this.m_lstRewardSlot[j++], 904, new NKCUISlot.OnClick(this.OnClickRandomMoldSlot), maxGradeInRewardGroups4);
			}
			if (flag5)
			{
				int maxGradeInRewardGroups5 = NKCUtil.GetMaxGradeInRewardGroups(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_MISC);
				this.SetRandomSlot(this.m_lstRewardSlot[j++], 903, new NKCUISlot.OnClick(this.OnClickRandomMiscSlot), maxGradeInRewardGroups5);
			}
			while (j < this.m_lstRewardSlot.Count)
			{
				NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[j], false);
				j++;
			}
		}

		// Token: 0x06006C4D RID: 27725 RVA: 0x00235F79 File Offset: 0x00234179
		private void TouchBattle()
		{
			NKCUIShadowBattleSlot.OnTouchBattle onTouchBattle = this.dOnTouchBattle;
			if (onTouchBattle == null)
			{
				return;
			}
			onTouchBattle(this.m_templet);
		}

		// Token: 0x06006C4E RID: 27726 RVA: 0x00235F94 File Offset: 0x00234194
		private void DivisionReward(List<int> lstRewardGroup, ref List<NKMRewardTemplet> lstOneRewardTemplet, ref List<int> lstRewardGroupID)
		{
			for (int i = 0; i < lstRewardGroup.Count; i++)
			{
				NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(lstRewardGroup[i]);
				if (rewardGroup != null)
				{
					if (rewardGroup.List.Count == 1)
					{
						lstOneRewardTemplet.Add(rewardGroup.List[0]);
					}
					else if (!lstRewardGroupID.Contains(lstRewardGroup[i]))
					{
						lstRewardGroupID.Add(lstRewardGroup[i]);
					}
				}
			}
		}

		// Token: 0x06006C4F RID: 27727 RVA: 0x00236004 File Offset: 0x00234204
		private void SetMiscSlot(NKCUISlot slot, int miscItemID, int count)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(miscItemID, (long)count, 0L, 0), 0);
			slot.SetData(data, false, false, true, null);
			slot.SetOpenItemBoxOnClick();
			NKCUtil.SetGameobjectActive(slot, true);
		}

		// Token: 0x06006C50 RID: 27728 RVA: 0x0023603C File Offset: 0x0023423C
		private void SetRandomSlot(NKCUISlot slot, int miscItemID, NKCUISlot.OnClick onClick, int maxGrade)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(miscItemID, 1L, 0L, 0), 0);
			slot.SetData(data, false, false, true, onClick);
			slot.SetBackGround(maxGrade);
			NKCUtil.SetGameobjectActive(slot, true);
		}

		// Token: 0x06006C51 RID: 27729 RVA: 0x00236078 File Offset: 0x00234278
		private void OnClickRandomUnitSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_UNIT);
			HashSet<int> rewardIDs2 = NKCUtil.GetRewardIDs(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_OPERATOR);
			List<NKMUnitTempletBase> list = new List<NKMUnitTempletBase>();
			foreach (int unitID in rewardIDs)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase != null)
				{
					list.Add(unitTempletBase);
				}
			}
			foreach (int unitID2 in rewardIDs2)
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitID2);
				if (unitTempletBase2 != null)
				{
					list.Add(unitTempletBase2);
				}
			}
			list.Sort(new CompTemplet.CompNUTB());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(list[i].m_UnitID);
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_UNIT, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x00236188 File Offset: 0x00234388
		private void OnClickRandomEquipSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_EQUIP);
			List<NKMEquipTemplet> list = new List<NKMEquipTemplet>();
			foreach (int equipID in rewardIDs)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
				if (equipTemplet != null)
				{
					list.Add(equipTemplet);
				}
			}
			list.Sort(new CompTemplet.CompNET());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(list[i].m_ItemEquipID);
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_EQUIP, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x00236240 File Offset: 0x00234440
		private void OnClickRandomMoldSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_MOLD);
			List<NKMItemMoldTemplet> list = new List<NKMItemMoldTemplet>();
			foreach (int moldID in rewardIDs)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(moldID);
				if (itemMoldTempletByID != null)
				{
					list.Add(itemMoldTempletByID);
				}
			}
			list.Sort(new CompTemplet.CompNMT());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(list[i].m_MoldID);
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_MOLD, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x002362F8 File Offset: 0x002344F8
		private void OnClickRandomMiscSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_lstRewardGroupID, NKM_REWARD_TYPE.RT_MISC);
			List<NKMItemMiscTemplet> list = new List<NKMItemMiscTemplet>();
			foreach (int itemMiscID in rewardIDs)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemMiscID);
				if (itemMiscTempletByID != null)
				{
					list.Add(itemMiscTempletByID);
				}
			}
			list.Sort(new CompTemplet.CompNIMT());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(list[i].m_ItemMiscID);
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_MISC, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x04005806 RID: 22534
		[Header("battle Info")]
		public Text m_txtNumber;

		// Token: 0x04005807 RID: 22535
		public Text m_txtBestTime;

		// Token: 0x04005808 RID: 22536
		[Header("enemy info")]
		public Text m_txtEnemyName;

		// Token: 0x04005809 RID: 22537
		public Text m_txtEnemyLv;

		// Token: 0x0400580A RID: 22538
		public Image m_imgEnemy;

		// Token: 0x0400580B RID: 22539
		public GameObject m_objBoss;

		// Token: 0x0400580C RID: 22540
		[Header("rewards")]
		public Transform m_trReward;

		// Token: 0x0400580D RID: 22541
		[Header("clear data")]
		public GameObject m_objClear;

		// Token: 0x0400580E RID: 22542
		public Text m_txtClearName;

		// Token: 0x0400580F RID: 22543
		public Text m_txtClearTime;

		// Token: 0x04005810 RID: 22544
		[Header("state")]
		public GameObject m_objCurrent;

		// Token: 0x04005811 RID: 22545
		public GameObject m_objLock;

		// Token: 0x04005812 RID: 22546
		[Header("button")]
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x04005813 RID: 22547
		public GameObject m_objButtonEnable;

		// Token: 0x04005814 RID: 22548
		public GameObject m_objButtonDisable;

		// Token: 0x04005815 RID: 22549
		private NKCUIShadowBattleSlot.OnTouchBattle dOnTouchBattle;

		// Token: 0x04005816 RID: 22550
		private NKMShadowBattleTemplet m_templet;

		// Token: 0x04005817 RID: 22551
		private List<NKCUISlot> m_lstRewardSlot = new List<NKCUISlot>();

		// Token: 0x04005818 RID: 22552
		private List<int> m_lstRewardGroupID = new List<int>();

		// Token: 0x020016E0 RID: 5856
		// (Invoke) Token: 0x0600B193 RID: 45459
		public delegate void OnTouchBattle(NKMShadowBattleTemplet bttleTemplet);
	}
}
