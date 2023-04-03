using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000971 RID: 2417
	public class NKCUIAttendanceSubUI : MonoBehaviour
	{
		// Token: 0x0600617F RID: 24959 RVA: 0x001E8F14 File Offset: 0x001E7114
		private void InitUI()
		{
			if (this.m_lstSlot == null || this.m_lstSlot.Count == 0)
			{
				Debug.LogError(base.gameObject.name + " - m_lstSlot is null");
			}
			if (this.m_NKM_UI_EVENT_DAILYCHECK_MONTH_IMG == null)
			{
				Debug.LogError(base.gameObject.name + " - m_NKM_UI_EVENT_DAILYCHECK_MONTH_IMG is null");
			}
			if (this.m_AB_FX_UI_EVENT_ICON_SLOT_RECIEVE_FX == null)
			{
				Debug.LogError(base.gameObject.name + " - AB_FX_UI_EVENT_ICON_SLOT_RECIEVE_FX is null");
			}
			NKCUtil.SetGameobjectActive(this.m_AB_FX_UI_EVENT_ICON_SLOT_RECIEVE_FX.gameObject, false);
			this.m_bInitComplete = true;
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x001E8FB8 File Offset: 0x001E71B8
		public void SetData(NKMAttendanceTabTemplet tabTemplet)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			this.m_slotToday = null;
			this.m_slotNext = null;
			if (tabTemplet == null)
			{
				Debug.LogError("tabTemplet is null");
				return;
			}
			NKMAttendance nkmattendance = NKCScenManager.CurrentUserData().m_AttendanceData.AttList.Find((NKMAttendance x) => x.IDX == tabTemplet.IDX);
			if (nkmattendance == null)
			{
				Debug.LogError(string.Format("attendance is null - key : {0}", tabTemplet.IDX));
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				if (i < tabTemplet.RewardTemplets.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[i], true);
					int num = i + 1;
					bool bShowCheckMark = num <= nkmattendance.Count;
					bool bShowCheckNext = num == nkmattendance.Count + 1;
					this.m_lstSlot[i].SetData(tabTemplet.RewardTemplets[num], bShowCheckMark, bShowCheckNext);
					if (num == nkmattendance.Count)
					{
						this.m_slotToday = this.m_lstSlot[i];
					}
					else if (num == nkmattendance.Count + 1)
					{
						this.m_slotNext = this.m_lstSlot[i];
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
				}
			}
			this.m_NKM_UI_EVENT_DAILYCHECK_MONTH_IMG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_attendance_texture", tabTemplet.BackgroundImage, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x001E9155 File Offset: 0x001E7355
		public IEnumerator ProcessSubUI(bool bShowSlotAnimation)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			yield return new WaitForSeconds(0.2f);
			if (this.m_slotToday != null)
			{
				this.m_slotToday.ShowInduceAnimation(true);
			}
			if (bShowSlotAnimation)
			{
				yield return this.ShowSlotCheckAnimation();
			}
			if (this.m_slotNext != null)
			{
				yield return this.m_slotNext.ShowNextMark(bShowSlotAnimation);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x001E916B File Offset: 0x001E736B
		private IEnumerator ShowSlotCheckAnimation()
		{
			if (this.m_slotToday == null)
			{
				Debug.LogWarning("아이템 획득 연출이 필요한데 대상 슬롯이 없음 - SetData 체크");
				yield break;
			}
			yield return this.m_slotToday.ShowSlotCheckMarkAnimation(this.m_AB_FX_UI_EVENT_ICON_SLOT_RECIEVE_FX);
			NKCUtil.SetGameobjectActive(this.m_AB_FX_UI_EVENT_ICON_SLOT_RECIEVE_FX, false);
			yield break;
		}

		// Token: 0x04004DB3 RID: 19891
		private const string BG_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_attendance_texture";

		// Token: 0x04004DB4 RID: 19892
		public const float UI_OPEN_DELAY = 0.2f;

		// Token: 0x04004DB5 RID: 19893
		[Header("프리팹 신규 생성 시 연결 필요")]
		public List<NKCUIAttendanceSlot> m_lstSlot = new List<NKCUIAttendanceSlot>();

		// Token: 0x04004DB6 RID: 19894
		public Image m_NKM_UI_EVENT_DAILYCHECK_MONTH_IMG;

		// Token: 0x04004DB7 RID: 19895
		public GameObject m_AB_FX_UI_EVENT_ICON_SLOT_RECIEVE_FX;

		// Token: 0x04004DB8 RID: 19896
		private NKCUIAttendanceSlot m_slotToday;

		// Token: 0x04004DB9 RID: 19897
		private NKCUIAttendanceSlot m_slotNext;

		// Token: 0x04004DBA RID: 19898
		private bool m_bInitComplete;
	}
}
