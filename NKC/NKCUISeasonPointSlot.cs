using System;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AAB RID: 2731
	public class NKCUISeasonPointSlot : MonoBehaviour
	{
		// Token: 0x0600797A RID: 31098 RVA: 0x00286766 File Offset: 0x00284966
		public int GetSlotScore()
		{
			return this.m_slotScore;
		}

		// Token: 0x0600797B RID: 31099 RVA: 0x0028676E File Offset: 0x0028496E
		public void Init()
		{
			this.m_slot.Init();
			this.m_bInitComplete = true;
		}

		// Token: 0x0600797C RID: 31100 RVA: 0x00286784 File Offset: 0x00284984
		public void SetData(NKCUISeasonPointSlot.SeasonPointSlotData seasonPointSlotData, int myPoint, int receivedPoint, bool bIsFinalSlot, NKCUISeasonPointSlot.OnClickSlot dOnClickSlot)
		{
			this.m_SeasonSlotData = seasonPointSlotData;
			NKCUISlot.SlotData slotData = null;
			if (seasonPointSlotData.ID != 0)
			{
				slotData = NKCUISlot.SlotData.MakeRewardTypeData(seasonPointSlotData.RewardType, seasonPointSlotData.RewardID, seasonPointSlotData.RewardCount, 0);
			}
			this.SetData(slotData, seasonPointSlotData.SlotPoint, myPoint, receivedPoint, bIsFinalSlot, dOnClickSlot);
		}

		// Token: 0x0600797D RID: 31101 RVA: 0x002867D0 File Offset: 0x002849D0
		private void SetData(NKCUISlot.SlotData slotData, int slotScore, int myScore, int receivedScore, bool bIsFinalSlot, NKCUISeasonPointSlot.OnClickSlot dOnClickSlot)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.m_dOnClickSlot = dOnClickSlot;
			this.m_slotScore = slotScore;
			if (slotData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_slot, false);
				NKCUtil.SetGameobjectActive(this.m_lbScore, false);
				NKCUtil.SetGameobjectActive(this.m_objNormal, false);
				NKCUtil.SetGameobjectActive(this.m_objClear, false);
				NKCUtil.SetGameobjectActive(this.m_objComplete, false);
				NKCUtil.SetGameobjectActive(this.m_objGauge, true);
				this.m_bBtnLocked = true;
				return;
			}
			this.m_bBtnLocked = false;
			NKCUtil.SetGameobjectActive(this.m_slot, true);
			this.m_slot.SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickBtn));
			this.m_slot.SetEventGet(slotScore <= receivedScore);
			this.m_slot.SetDisable(slotScore <= receivedScore, "");
			this.m_slot.SetRewardFx(slotScore <= myScore && slotScore > receivedScore);
			NKCUtil.SetGameobjectActive(this.m_lbScore, true);
			NKCUtil.SetLabelText(this.m_lbScore, slotScore.ToString("N0"));
			NKCUtil.SetGameobjectActive(this.m_objGauge, !bIsFinalSlot);
			NKCUtil.SetGameobjectActive(this.m_objNormal, slotScore > myScore);
			NKCUtil.SetGameobjectActive(this.m_objClear, myScore >= slotScore && slotScore > receivedScore);
			NKCUtil.SetGameobjectActive(this.m_objComplete, slotScore <= receivedScore);
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x00286927 File Offset: 0x00284B27
		public void SetGaugeProgress(float progressPercent)
		{
			if (this.m_imgGauge != null)
			{
				this.m_imgGauge.fillAmount = progressPercent;
			}
		}

		// Token: 0x0600797F RID: 31103 RVA: 0x00286944 File Offset: 0x00284B44
		public void OnClickBtn(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (!this.m_objClear.activeSelf || this.m_bBtnLocked)
			{
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, true);
				return;
			}
			this.m_bBtnLocked = true;
			NKCUISeasonPointSlot.OnClickSlot dOnClickSlot = this.m_dOnClickSlot;
			if (dOnClickSlot == null)
			{
				return;
			}
			dOnClickSlot(this.m_SeasonSlotData);
		}

		// Token: 0x0400662E RID: 26158
		public NKCUISlot m_slot;

		// Token: 0x0400662F RID: 26159
		public GameObject m_objNormal;

		// Token: 0x04006630 RID: 26160
		public GameObject m_objClear;

		// Token: 0x04006631 RID: 26161
		public GameObject m_objComplete;

		// Token: 0x04006632 RID: 26162
		public Text m_lbScore;

		// Token: 0x04006633 RID: 26163
		public GameObject m_objGauge;

		// Token: 0x04006634 RID: 26164
		public Image m_imgGauge;

		// Token: 0x04006635 RID: 26165
		private NKCUISeasonPointSlot.OnClickSlot m_dOnClickSlot;

		// Token: 0x04006636 RID: 26166
		private int m_slotScore;

		// Token: 0x04006637 RID: 26167
		private bool m_bBtnLocked;

		// Token: 0x04006638 RID: 26168
		private NKCUISeasonPointSlot.SeasonPointSlotData m_SeasonSlotData;

		// Token: 0x04006639 RID: 26169
		private bool m_bInitComplete;

		// Token: 0x020017FE RID: 6142
		public class SeasonPointSlotData
		{
			// Token: 0x0600B4D4 RID: 46292 RVA: 0x00364466 File Offset: 0x00362666
			public static NKCUISeasonPointSlot.SeasonPointSlotData MakeEmptyData()
			{
				return new NKCUISeasonPointSlot.SeasonPointSlotData
				{
					ID = 0,
					SlotPoint = 0,
					RewardType = NKM_REWARD_TYPE.RT_NONE,
					RewardID = 0,
					RewardCount = 0
				};
			}

			// Token: 0x0600B4D5 RID: 46293 RVA: 0x00364490 File Offset: 0x00362690
			public static NKCUISeasonPointSlot.SeasonPointSlotData MakeSeasonPointSlotData(NKMRaidSeasonRewardTemplet cNKMRaidSeasonRewardTemplet)
			{
				return new NKCUISeasonPointSlot.SeasonPointSlotData
				{
					ID = cNKMRaidSeasonRewardTemplet.RewardBoardId,
					SlotPoint = cNKMRaidSeasonRewardTemplet.RaidPoint,
					RewardType = cNKMRaidSeasonRewardTemplet.RewardType,
					RewardID = cNKMRaidSeasonRewardTemplet.RewardId,
					RewardCount = cNKMRaidSeasonRewardTemplet.RewardValue
				};
			}

			// Token: 0x0600B4D6 RID: 46294 RVA: 0x003644E0 File Offset: 0x003626E0
			public static NKCUISeasonPointSlot.SeasonPointSlotData MakeSeasonPointSlotData(GuildSeasonRewardTemplet cGuildSeasonRewardTemplet)
			{
				return new NKCUISeasonPointSlot.SeasonPointSlotData
				{
					ID = cGuildSeasonRewardTemplet.Key,
					SlotPoint = cGuildSeasonRewardTemplet.GetRewardCountValue(),
					RewardType = cGuildSeasonRewardTemplet.GetRewardItemType(),
					RewardID = cGuildSeasonRewardTemplet.GetRewardItemId(),
					RewardCount = cGuildSeasonRewardTemplet.GetRewardItemValue()
				};
			}

			// Token: 0x0400A7D6 RID: 42966
			public int ID;

			// Token: 0x0400A7D7 RID: 42967
			public int SlotPoint;

			// Token: 0x0400A7D8 RID: 42968
			public NKM_REWARD_TYPE RewardType;

			// Token: 0x0400A7D9 RID: 42969
			public int RewardID;

			// Token: 0x0400A7DA RID: 42970
			public int RewardCount;
		}

		// Token: 0x020017FF RID: 6143
		// (Invoke) Token: 0x0600B4D9 RID: 46297
		public delegate void OnClickSlot(NKCUISeasonPointSlot.SeasonPointSlotData slotData);
	}
}
