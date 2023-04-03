using System;
using ClientPacket.Common;
using NKM.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B30 RID: 2864
	public class NKCPopupGuildCoopSeasonRewardSlot : MonoBehaviour
	{
		// Token: 0x06008269 RID: 33385 RVA: 0x002BFEA2 File Offset: 0x002BE0A2
		public void InitUI()
		{
			this.m_slot.Init();
		}

		// Token: 0x0600826A RID: 33386 RVA: 0x002BFEB0 File Offset: 0x002BE0B0
		public void SetData(GuildSeasonRewardTemplet rewardTemplet, bool bCanReward, bool bIsRewarded, bool bIsFinalSlot, NKCPopupGuildCoopSeasonRewardSlot.OnClickSlot onClickSlot)
		{
			this.m_dOnClickSlot = onClickSlot;
			if (rewardTemplet == null)
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
			this.m_slot.SetData(NKCUISlot.SlotData.MakeRewardTypeData(rewardTemplet.GetRewardItemType(), rewardTemplet.GetRewardItemId(), rewardTemplet.GetRewardItemValue(), 0), true, new NKCUISlot.OnClick(this.OnClickBtn));
			this.m_slot.SetEventGet(bIsRewarded);
			this.m_slot.SetDisable(!bCanReward || bIsRewarded, "");
			this.m_slot.SetRewardFx(bCanReward);
			NKCUtil.SetGameobjectActive(this.m_lbScore, true);
			int rewardCountValue = rewardTemplet.GetRewardCountValue();
			NKCUtil.SetLabelText(this.m_lbScore, rewardCountValue.ToString("N0"));
			NKCUtil.SetGameobjectActive(this.m_objGauge, !bIsFinalSlot);
			GuildDungeonRewardCategory rewardCategory = rewardTemplet.GetRewardCategory();
			if (rewardCategory != GuildDungeonRewardCategory.RANK)
			{
				if (rewardCategory == GuildDungeonRewardCategory.DUNGEON_TRY)
				{
					NKCUtil.SetGameobjectActive(this.m_objNormal, !bCanReward);
					NKCUtil.SetGameobjectActive(this.m_objClear, NKCGuildCoopManager.m_TryCount >= rewardCountValue && !bIsRewarded);
					NKCUtil.SetGameobjectActive(this.m_objComplete, bIsRewarded);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objNormal, NKCGuildCoopManager.m_KillPoint < (long)rewardCountValue);
				NKCUtil.SetGameobjectActive(this.m_objClear, NKCGuildCoopManager.m_KillPoint >= (long)rewardCountValue && !bIsRewarded);
				NKCUtil.SetGameobjectActive(this.m_objComplete, bIsRewarded);
			}
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x002C0043 File Offset: 0x002BE243
		public void SetGaugeProgress(float progressPercent)
		{
			if (this.m_imgGauge != null)
			{
				this.m_imgGauge.fillAmount = progressPercent;
			}
		}

		// Token: 0x0600826C RID: 33388 RVA: 0x002C005F File Offset: 0x002BE25F
		public void OnClickBtn(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (!this.m_objClear.activeSelf || this.m_bBtnLocked)
			{
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, true);
				return;
			}
			this.m_bBtnLocked = true;
			NKCPopupGuildCoopSeasonRewardSlot.OnClickSlot dOnClickSlot = this.m_dOnClickSlot;
			if (dOnClickSlot == null)
			{
				return;
			}
			dOnClickSlot();
		}

		// Token: 0x04006E99 RID: 28313
		public NKCUISlot m_slot;

		// Token: 0x04006E9A RID: 28314
		public GameObject m_objNormal;

		// Token: 0x04006E9B RID: 28315
		public GameObject m_objClear;

		// Token: 0x04006E9C RID: 28316
		public GameObject m_objComplete;

		// Token: 0x04006E9D RID: 28317
		public Text m_lbScore;

		// Token: 0x04006E9E RID: 28318
		public GameObject m_objGauge;

		// Token: 0x04006E9F RID: 28319
		public Image m_imgGauge;

		// Token: 0x04006EA0 RID: 28320
		private bool m_bBtnLocked;

		// Token: 0x04006EA1 RID: 28321
		private NKCPopupGuildCoopSeasonRewardSlot.OnClickSlot m_dOnClickSlot;

		// Token: 0x020018CA RID: 6346
		// (Invoke) Token: 0x0600B6BE RID: 46782
		public delegate void OnClickSlot();
	}
}
