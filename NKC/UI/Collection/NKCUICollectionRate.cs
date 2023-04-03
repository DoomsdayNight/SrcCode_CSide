using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C26 RID: 3110
	public class NKCUICollectionRate : MonoBehaviour
	{
		// Token: 0x06009019 RID: 36889 RVA: 0x0030FEF6 File Offset: 0x0030E0F6
		public void Init()
		{
			if (null != this.m_Img_NKM_UI_COLLECTION_RATE_GAUGE_BAR)
			{
				this.m_Img_NKM_UI_COLLECTION_RATE_GAUGE_BAR.fillAmount = 0f;
			}
			NKCUtil.SetLabelText(this.m_txt_NKM_UI_COLLECTION_RATE_TEXT_PERCENT, "0");
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_RATE_TEXT_COUNT, "0/0");
		}

		// Token: 0x0600901A RID: 36890 RVA: 0x0030FF38 File Offset: 0x0030E138
		public void SetData(NKCUICollection.CollectionType type, int iCur, int iTotal)
		{
			NKCUtil.SetLabelText(this.m_txt_NKM_UI_COLLECTION_RATE_TEXT_COLLECTION_RATE, NKCUtilString.GetCollectionRateStrByType(type));
			float num = Mathf.Floor((float)iCur) / Mathf.Floor((float)iTotal);
			num = Mathf.Clamp(num, 0f, 1f);
			if (null != this.m_Img_NKM_UI_COLLECTION_RATE_GAUGE_BAR)
			{
				this.m_Img_NKM_UI_COLLECTION_RATE_GAUGE_BAR.fillAmount = num;
			}
			NKCUtil.SetLabelText(this.m_txt_NKM_UI_COLLECTION_RATE_TEXT_PERCENT, Mathf.FloorToInt(num * 100f).ToString() + "%");
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_RATE_TEXT_COUNT, string.Format("{0}/{1}", iCur, iTotal));
			int num2 = Mathf.FloorToInt(num * 360f);
			int num3 = num2 - this.m_preAngle;
			this.m_preAngle = num2;
			this.m_NKM_UI_COLLECTION_RATE_GAUGE_HANDLE.RotateAround(this.m_NKM_UI_COLLECTION_RATE_GAUGE_BG_01.position, Vector3.back, (float)num3);
		}

		// Token: 0x04007D18 RID: 32024
		[Header("사원 수집률")]
		public Image m_Img_NKM_UI_COLLECTION_RATE_GAUGE_BAR;

		// Token: 0x04007D19 RID: 32025
		public Text m_txt_NKM_UI_COLLECTION_RATE_TEXT_COLLECTION_RATE;

		// Token: 0x04007D1A RID: 32026
		public Text m_txt_NKM_UI_COLLECTION_RATE_TEXT_PERCENT;

		// Token: 0x04007D1B RID: 32027
		public Text m_NKM_UI_COLLECTION_RATE_TEXT_COUNT;

		// Token: 0x04007D1C RID: 32028
		public Transform m_NKM_UI_COLLECTION_RATE_GAUGE_BG_01;

		// Token: 0x04007D1D RID: 32029
		public Transform m_NKM_UI_COLLECTION_RATE_GAUGE_HANDLE;

		// Token: 0x04007D1E RID: 32030
		private int m_preAngle;
	}
}
