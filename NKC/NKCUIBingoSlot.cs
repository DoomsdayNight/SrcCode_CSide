using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000795 RID: 1941
	public class NKCUIBingoSlot : MonoBehaviour
	{
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06004C44 RID: 19524 RVA: 0x0016D561 File Offset: 0x0016B761
		public bool IsHas
		{
			get
			{
				return this.m_bHave;
			}
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0016D56C File Offset: 0x0016B76C
		public void Init(NKCUIBingoSlot.OnClick onClick)
		{
			this.dOnClick = onClick;
			if (this.m_btn != null)
			{
				this.m_btn.PointerClick.RemoveAllListeners();
				this.m_btn.PointerClick.AddListener(new UnityAction(this.OnTouch));
			}
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0016D5BC File Offset: 0x0016B7BC
		public void SetData(int index, int num, bool bHave, bool isMission)
		{
			this.m_index = index;
			this.m_isMission = isMission;
			this.m_bHave = bHave;
			NKCUtil.SetGameobjectActive(this.m_objNumber, !this.m_isMission);
			NKCUtil.SetGameobjectActive(this.m_txtNumber, !this.m_isMission);
			NKCUtil.SetGameobjectActive(this.m_objMission, this.m_isMission);
			NKCUtil.SetGameobjectActive(this.m_txtMission, this.m_isMission);
			if (this.m_isMission)
			{
				NKCUtil.SetLabelText(this.m_txtMissionTagOn, num.ToString());
				NKCUtil.SetLabelText(this.m_txtMissionTagOff, num.ToString());
				NKCUtil.SetGameobjectActive(this.m_objMissionOn, bHave);
				NKCUtil.SetGameobjectActive(this.m_objMissionOff, !bHave);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_txtNumber, num.ToString());
				NKCUtil.SetGameobjectActive(this.m_objOn, bHave);
				NKCUtil.SetGameobjectActive(this.m_objOff, !bHave);
			}
			this.SetTextColor(false);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0016D6A8 File Offset: 0x0016B8A8
		public void SetSpecialMode(bool bSpecialMode)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelect, bSpecialMode && !this.m_isMission && !this.m_bHave);
			NKCUtil.SetGameobjectActive(this.m_objDisable, bSpecialMode && (this.m_isMission || this.m_bHave));
			this.SetTextColor(bSpecialMode);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0016D703 File Offset: 0x0016B903
		public void SetSelectFx(bool active)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelectFx, active);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0016D711 File Offset: 0x0016B911
		public void SetGetFx(bool active)
		{
			NKCUtil.SetGameobjectActive(this.m_objGetFx, active);
			if (active)
			{
				NKCSoundManager.PlaySound("FX_UI_ATTENDANCE_CHECK", 1f, 0f, 0f, false, 0f, false, 0f);
			}
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0016D748 File Offset: 0x0016B948
		private void SetTextColor(bool bSpecialMode)
		{
			if (this.m_bHave)
			{
				NKCUtil.SetLabelTextColor(this.m_txtNumber, this.m_colorOn);
				NKCUtil.SetLabelTextColor(this.m_txtMission, this.m_colorMissionOn);
				return;
			}
			if (bSpecialMode)
			{
				NKCUtil.SetLabelTextColor(this.m_txtNumber, this.m_colorSelect);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_txtNumber, this.m_colorOff);
			NKCUtil.SetLabelTextColor(this.m_txtMission, this.m_colorMissionOff);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0016D7B7 File Offset: 0x0016B9B7
		private void OnTouch()
		{
			NKCUIBingoSlot.OnClick onClick = this.dOnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this.m_index);
		}

		// Token: 0x04003BE5 RID: 15333
		public NKCUIComStateButton m_btn;

		// Token: 0x04003BE6 RID: 15334
		[Header("숫자")]
		public GameObject m_objNumber;

		// Token: 0x04003BE7 RID: 15335
		public Text m_txtNumber;

		// Token: 0x04003BE8 RID: 15336
		public GameObject m_objOn;

		// Token: 0x04003BE9 RID: 15337
		public GameObject m_objOff;

		// Token: 0x04003BEA RID: 15338
		public Color m_colorOn;

		// Token: 0x04003BEB RID: 15339
		public Color m_colorOff;

		// Token: 0x04003BEC RID: 15340
		[Header("미션")]
		public GameObject m_objMission;

		// Token: 0x04003BED RID: 15341
		public Text m_txtMission;

		// Token: 0x04003BEE RID: 15342
		public Text m_txtMissionTagOn;

		// Token: 0x04003BEF RID: 15343
		public Text m_txtMissionTagOff;

		// Token: 0x04003BF0 RID: 15344
		public GameObject m_objMissionOn;

		// Token: 0x04003BF1 RID: 15345
		public GameObject m_objMissionOff;

		// Token: 0x04003BF2 RID: 15346
		public Color m_colorMissionOn;

		// Token: 0x04003BF3 RID: 15347
		public Color m_colorMissionOff;

		// Token: 0x04003BF4 RID: 15348
		[Header("선택")]
		public GameObject m_objSelect;

		// Token: 0x04003BF5 RID: 15349
		public GameObject m_objSelectFx;

		// Token: 0x04003BF6 RID: 15350
		public GameObject m_objDisable;

		// Token: 0x04003BF7 RID: 15351
		public Color m_colorSelect;

		// Token: 0x04003BF8 RID: 15352
		[Header("획득")]
		public GameObject m_objGetFx;

		// Token: 0x04003BF9 RID: 15353
		private bool m_isMission;

		// Token: 0x04003BFA RID: 15354
		private int m_index;

		// Token: 0x04003BFB RID: 15355
		private bool m_bHave;

		// Token: 0x04003BFC RID: 15356
		private NKCUIBingoSlot.OnClick dOnClick;

		// Token: 0x02001459 RID: 5209
		// (Invoke) Token: 0x0600A874 RID: 43124
		public delegate void OnClick(int index);
	}
}
