using System;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B3D RID: 2877
	public class NKCPopupGuildAttendanceSlot : MonoBehaviour
	{
		// Token: 0x06008300 RID: 33536 RVA: 0x002C2E7C File Offset: 0x002C107C
		public void SetData(int needMemberCount, RewardUnit reward, bool bComplete)
		{
			if (needMemberCount > 0)
			{
				NKCUtil.SetLabelText(this.m_lbNeedMemberCount, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_ATTENDANCE_REWARD_CONDITION, needMemberCount));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbNeedMemberCount, NKCUtilString.GET_STRING_CONSORTIUM_POPUP_ATTENDANCE_REWARD_BASIC);
			}
			NKCUtil.SetImageSprite(this.m_imgRewardIcon, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(reward.ItemID), false);
			NKCUtil.SetLabelText(this.m_lbReward, string.Format("{0} {1}", this.GetRewardName(reward), reward.Count));
			NKCUtil.SetGameobjectActive(this.m_objComplete, bComplete);
		}

		// Token: 0x06008301 RID: 33537 RVA: 0x002C2F04 File Offset: 0x002C1104
		private string GetRewardName(RewardUnit reward)
		{
			if (reward.RewardType == NKM_REWARD_TYPE.RT_MISC)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(reward.ItemID);
				if (itemMiscTempletByID != null)
				{
					return itemMiscTempletByID.GetItemName();
				}
			}
			return "";
		}

		// Token: 0x04006F38 RID: 28472
		public Text m_lbNeedMemberCount;

		// Token: 0x04006F39 RID: 28473
		public Image m_imgRewardIcon;

		// Token: 0x04006F3A RID: 28474
		public Text m_lbReward;

		// Token: 0x04006F3B RID: 28475
		public GameObject m_objComplete;
	}
}
