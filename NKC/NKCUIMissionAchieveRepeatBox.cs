using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007CA RID: 1994
	public class NKCUIMissionAchieveRepeatBox : MonoBehaviour
	{
		// Token: 0x06004ED1 RID: 20177 RVA: 0x0017C6F8 File Offset: 0x0017A8F8
		public void SetData(NKMMissionTemplet missionTemplet, NKCUIMissionAchieveRepeatBox.OnButton onButton)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			this.m_dOnButton = onButton;
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(delegate()
			{
				this.OnClick(missionTemplet);
			});
			NKCUtil.SetLabelText(this.m_lbRequirePoint, missionTemplet.m_Times.ToString());
			string text = string.Empty;
			NKM_MISSION_TYPE missionType = missionTabTemplet.m_MissionType;
			if (missionType != NKM_MISSION_TYPE.REPEAT_DAILY)
			{
				if (missionType == NKM_MISSION_TYPE.REPEAT_WEEKLY)
				{
					text = "AB_UI_NKM_UI_MISSION_REPEAT_TOP_ICON_WEEKLY";
				}
			}
			else
			{
				text = "AB_UI_NKM_UI_MISSION_REPEAT_TOP_ICON_DAILY";
			}
			if (!string.IsNullOrEmpty(text))
			{
				NKCUtil.SetImageSprite(this.m_imgRequirePoint, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_MISSION_SPRITE", text, false), false);
			}
			NKMMissionData missionData = nkmuserData.m_MissionData.GetMissionData(missionTemplet);
			if (missionData == null || NKMMissionManager.CheckCanReset(missionTemplet.m_ResetInterval, missionData) || missionData.times < missionTemplet.m_Times)
			{
				this.PlayAni(NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.BASE);
				return;
			}
			if (missionData.isComplete || missionData.mission_id > missionTemplet.m_MissionID)
			{
				this.PlayAni(NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.COMPLETE);
				return;
			}
			if (missionData.times >= missionTemplet.m_Times)
			{
				this.PlayAni(NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.INTRO);
				return;
			}
			this.PlayAni(NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.BASE);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0017C860 File Offset: 0x0017AA60
		private void PlayAni(NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE state)
		{
			switch (state)
			{
			case NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.BASE:
				this.m_Ani.Play("NKM_UI_MISSION_REPEAT_REWARD_BOX_BASE");
				return;
			case NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.INTRO:
			case NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.IDLE:
				this.m_Ani.Play("NKM_UI_MISSION_REPEAT_REWARD_BOX_TOUCH_INTRO");
				return;
			case NKCUIMissionAchieveRepeatBox.REPEAT_BOX_STATE.COMPLETE:
				this.m_Ani.Play("NKM_UI_MISSION_REPEAT_REWARD_BOX_COMPLETE");
				return;
			default:
				return;
			}
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0017C8B6 File Offset: 0x0017AAB6
		public void OnClick(NKMMissionTemplet missionTemplet)
		{
			NKCUIMissionAchieveRepeatBox.OnButton dOnButton = this.m_dOnButton;
			if (dOnButton == null)
			{
				return;
			}
			dOnButton(missionTemplet);
		}

		// Token: 0x04003E99 RID: 16025
		public Animator m_Ani;

		// Token: 0x04003E9A RID: 16026
		public Image m_imgRequirePoint;

		// Token: 0x04003E9B RID: 16027
		public Text m_lbRequirePoint;

		// Token: 0x04003E9C RID: 16028
		public NKCUIComStateButton m_btn;

		// Token: 0x04003E9D RID: 16029
		public NKCUIMissionAchieveRepeatBox.OnButton m_dOnButton;

		// Token: 0x02001484 RID: 5252
		public enum REPEAT_BOX_STATE
		{
			// Token: 0x04009E5C RID: 40540
			BASE,
			// Token: 0x04009E5D RID: 40541
			INTRO,
			// Token: 0x04009E5E RID: 40542
			IDLE,
			// Token: 0x04009E5F RID: 40543
			COMPLETE
		}

		// Token: 0x02001485 RID: 5253
		// (Invoke) Token: 0x0600A918 RID: 43288
		public delegate void OnButton(NKMMissionTemplet missionTemplet);
	}
}
