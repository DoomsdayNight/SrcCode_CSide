using System;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BDB RID: 3035
	[RequireComponent(typeof(NKCUIEventSubUI))]
	public abstract class NKCUIEventSubUIBase : MonoBehaviour
	{
		// Token: 0x06008CBE RID: 36030 RVA: 0x002FDB8B File Offset: 0x002FBD8B
		public virtual void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventShortcut, new UnityAction(this.OnMoveShortcut));
		}

		// Token: 0x06008CBF RID: 36031
		public abstract void Open(NKMEventTabTemplet tabTemplet);

		// Token: 0x06008CC0 RID: 36032
		public abstract void Refresh();

		// Token: 0x06008CC1 RID: 36033 RVA: 0x002FDBA4 File Offset: 0x002FBDA4
		public virtual void Close()
		{
		}

		// Token: 0x06008CC2 RID: 36034 RVA: 0x002FDBA6 File Offset: 0x002FBDA6
		public virtual void Hide()
		{
		}

		// Token: 0x06008CC3 RID: 36035 RVA: 0x002FDBA8 File Offset: 0x002FBDA8
		public virtual void UnHide()
		{
		}

		// Token: 0x06008CC4 RID: 36036 RVA: 0x002FDBAA File Offset: 0x002FBDAA
		public virtual bool OnBackButton()
		{
			return false;
		}

		// Token: 0x06008CC5 RID: 36037 RVA: 0x002FDBAD File Offset: 0x002FBDAD
		public virtual void OnInventoryChange(NKMItemMiscData itemData)
		{
		}

		// Token: 0x06008CC6 RID: 36038 RVA: 0x002FDBAF File Offset: 0x002FBDAF
		protected void OnMoveShortcut()
		{
			if (this.m_tabTemplet.m_ShortCutType == NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				return;
			}
			if (!this.CheckEventTime(true))
			{
				return;
			}
			NKCContentManager.MoveToShortCut(this.m_tabTemplet.m_ShortCutType, this.m_tabTemplet.m_ShortCut, false);
		}

		// Token: 0x06008CC7 RID: 36039 RVA: 0x002FDBE8 File Offset: 0x002FBDE8
		protected bool CheckEventTime(bool bPopup = true)
		{
			if (this.m_tabTemplet == null)
			{
				return false;
			}
			if (this.m_tabTemplet.HasTimeLimit && !this.m_tabTemplet.IsAvailable)
			{
				if (bPopup)
				{
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_EVENT_END, delegate()
					{
						NKCUIEvent.Instance.Close();
					}, "");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06008CC8 RID: 36040 RVA: 0x002FDC50 File Offset: 0x002FBE50
		protected void SetDateLimit()
		{
			if (this.m_tabTemplet == null || !this.m_tabTemplet.HasDateLimit)
			{
				NKCUtil.SetLabelText(this.m_lbEventLimitDate, "");
				return;
			}
			if (NKCSynchronizedTime.GetTimeLeft(this.m_tabTemplet.EventDateEndUtc).TotalDays > (double)NKCSynchronizedTime.UNLIMITD_REMAIN_DAYS)
			{
				NKCUtil.SetLabelText(this.m_lbEventLimitDate, NKCUtilString.GET_STRING_EVENT_DATE_UNLIMITED_TEXT);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEventLimitDate, NKCUtilString.GetTimeIntervalString(this.m_tabTemplet.EventDateStart, this.m_tabTemplet.EventDateEnd, NKMTime.INTERVAL_FROM_UTC, false));
		}

		// Token: 0x040079A1 RID: 31137
		[Header("이벤트 기간")]
		[FormerlySerializedAs("m_lbTime")]
		[FormerlySerializedAs("m_txtTime")]
		[FormerlySerializedAs("m_lbEventDate")]
		[FormerlySerializedAs("m_lbEventTime")]
		public Text m_lbEventLimitDate;

		// Token: 0x040079A2 RID: 31138
		[Header("이벤트 숏컷 이동 버튼")]
		public NKCUIComStateButton m_csbtnEventShortcut;

		// Token: 0x040079A3 RID: 31139
		protected NKMEventTabTemplet m_tabTemplet;
	}
}
