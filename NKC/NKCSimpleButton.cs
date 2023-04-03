using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B1 RID: 1969
	public class NKCSimpleButton : MonoBehaviour
	{
		// Token: 0x06004DCA RID: 19914 RVA: 0x001776AC File Offset: 0x001758AC
		public void Init(UnityAction OnTouch)
		{
			this.m_button.PointerClick.RemoveAllListeners();
			this.m_button.PointerClick.AddListener(OnTouch);
			this.m_button.m_ButtonBG_Normal = this.m_objBtnBGNormal;
			this.m_button.m_ButtonBG_Selected = this.m_objBtnBGSelected;
			this.m_button.m_ButtonBG_Locked = this.m_objBtnBGLocked;
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0017770D File Offset: 0x0017590D
		public void On()
		{
			NKCUtil.SetImageColor(this.m_img, this.m_colOn);
			NKCUtil.SetLabelTextColor(this.m_txt, this.m_colOn);
			this.m_button.Select(true, false, false);
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x00177740 File Offset: 0x00175940
		public void Off()
		{
			NKCUtil.SetImageColor(this.m_img, this.m_colOff);
			NKCUtil.SetLabelTextColor(this.m_txt, this.m_colOff);
			this.m_button.Select(false, false, false);
		}

		// Token: 0x04003D83 RID: 15747
		public NKCUIComStateButton m_button;

		// Token: 0x04003D84 RID: 15748
		public GameObject m_objBtnBGNormal;

		// Token: 0x04003D85 RID: 15749
		public GameObject m_objBtnBGSelected;

		// Token: 0x04003D86 RID: 15750
		public GameObject m_objBtnBGLocked;

		// Token: 0x04003D87 RID: 15751
		public Image m_img;

		// Token: 0x04003D88 RID: 15752
		public Text m_txt;

		// Token: 0x04003D89 RID: 15753
		public Color m_colOff;

		// Token: 0x04003D8A RID: 15754
		public Color m_colOn;
	}
}
