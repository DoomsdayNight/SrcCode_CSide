using System;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B3 RID: 1971
	public class NKCUIAttendanceTab : MonoBehaviour
	{
		// Token: 0x06004E00 RID: 19968 RVA: 0x00177B5C File Offset: 0x00175D5C
		private void InitUI()
		{
			NKCUIComStateButton btn = this.m_btn;
			if (btn != null)
			{
				btn.PointerDown.RemoveAllListeners();
			}
			NKCUIComStateButton btn2 = this.m_btn;
			if (btn2 != null)
			{
				btn2.PointerDown.AddListener(delegate(PointerEventData eventData)
				{
					this.OnBtnClick();
				});
			}
			this.bInitComplete = true;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00177BA8 File Offset: 0x00175DA8
		public void SetData(NKMAttendanceTabTemplet tabInfo, NKCUIComToggleGroup toggleGroup, bool bEnableRedDot, NKCUIAttendanceTab.OnClickEvent onClick)
		{
			if (!this.bInitComplete)
			{
				this.InitUI();
			}
			this.m_fDeltaTime = 0f;
			this.m_tabIDX = tabInfo.IDX;
			this.m_lbTitleOn.text = tabInfo.GetTabNameMain();
			this.m_lbSubTitleOn.text = tabInfo.GetTabNameSub();
			this.m_imgIconOn.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_attendance_sprite", tabInfo.TabIconName, false);
			this.m_lbTitleOff.text = tabInfo.GetTabNameMain();
			this.m_lbSubTitleOff.text = tabInfo.GetTabNameSub();
			this.m_imgIconOff.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_attendance_sprite", tabInfo.TabIconName, false);
			this.m_tgl.SetToggleGroup(toggleGroup);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, bEnableRedDot);
			this.SetRemainTime();
			if (onClick != null)
			{
				this.dOnClick = onClick;
			}
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x00177C80 File Offset: 0x00175E80
		private void SetRemainTime()
		{
			DateTime eventEndDate = NKCScenManager.CurrentUserData().m_AttendanceData.AttList.Find((NKMAttendance x) => x.IDX == this.m_tabIDX).EventEndDate;
			NKCUtil.SetLabelText(this.m_lbRemainDaysOn, string.Format(NKCUtilString.GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM, NKCUtilString.GetRemainTimeString(eventEndDate, 1)));
			NKCUtil.SetLabelText(this.m_lbRemainDaysOff, string.Format(NKCUtilString.GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM, NKCUtilString.GetRemainTimeString(eventEndDate, 1)));
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x00177CEB File Offset: 0x00175EEB
		public void Select(bool bSelect)
		{
			this.m_tgl.Select(bSelect, true, true);
			if (bSelect)
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			}
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x00177D0B File Offset: 0x00175F0B
		private void OnBtnClick()
		{
			if (this.dOnClick != null)
			{
				this.dOnClick(this.m_tabIDX);
			}
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x00177D26 File Offset: 0x00175F26
		public int GetTabIDX()
		{
			return this.m_tabIDX;
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00177D30 File Offset: 0x00175F30
		private void Update()
		{
			if (base.gameObject.activeSelf && this.m_tabIDX > 0)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRemainTime();
				}
			}
		}

		// Token: 0x04003D8B RID: 15755
		private const string ICON_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_attendance_sprite";

		// Token: 0x04003D8C RID: 15756
		public Text m_lbTitleOn;

		// Token: 0x04003D8D RID: 15757
		public Text m_lbSubTitleOn;

		// Token: 0x04003D8E RID: 15758
		public Image m_imgIconOn;

		// Token: 0x04003D8F RID: 15759
		public Text m_lbRemainDaysOn;

		// Token: 0x04003D90 RID: 15760
		public Text m_lbTitleOff;

		// Token: 0x04003D91 RID: 15761
		public Text m_lbSubTitleOff;

		// Token: 0x04003D92 RID: 15762
		public Image m_imgIconOff;

		// Token: 0x04003D93 RID: 15763
		public Text m_lbRemainDaysOff;

		// Token: 0x04003D94 RID: 15764
		public NKCUIComStateButton m_btn;

		// Token: 0x04003D95 RID: 15765
		public NKCUIComToggle m_tgl;

		// Token: 0x04003D96 RID: 15766
		public GameObject m_objRedDot;

		// Token: 0x04003D97 RID: 15767
		private NKCUIAttendanceTab.OnClickEvent dOnClick;

		// Token: 0x04003D98 RID: 15768
		private bool bInitComplete;

		// Token: 0x04003D99 RID: 15769
		private int m_tabIDX;

		// Token: 0x04003D9A RID: 15770
		private float m_fDeltaTime;

		// Token: 0x02001474 RID: 5236
		// (Invoke) Token: 0x0600A8D4 RID: 43220
		public delegate void OnClickEvent(int tabID);
	}
}
