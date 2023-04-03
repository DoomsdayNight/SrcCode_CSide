using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BE9 RID: 3049
	public class NKCUIContractBanner : MonoBehaviour
	{
		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06008D69 RID: 36201 RVA: 0x003017FA File Offset: 0x002FF9FA
		// (set) Token: 0x06008D6A RID: 36202 RVA: 0x00301802 File Offset: 0x002FFA02
		public int ContractID { get; set; }

		// Token: 0x06008D6B RID: 36203 RVA: 0x0030180B File Offset: 0x002FFA0B
		public void SetRemainCount(int count)
		{
			NKCUtil.SetLabelText(this.m_txtRemainCount, string.Format(NKCUtilString.GET_STRING_CONTRACT_COUNT_ONE_PARAM, count.ToString()));
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x00301829 File Offset: 0x002FFA29
		public void SetActiveEventTag(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objEventTag, bValue);
		}

		// Token: 0x06008D6D RID: 36205 RVA: 0x00301837 File Offset: 0x002FFA37
		public void SetEnableAnimator(bool bValue)
		{
			if (this.m_Ani != null)
			{
				this.m_Ani.enabled = bValue;
			}
		}

		// Token: 0x04007A3C RID: 31292
		[Header("n회 Text")]
		public Text m_txtRemainCount;

		// Token: 0x04007A3D RID: 31293
		[Header("기밀채용 전용 애니메이터")]
		public Animator m_Ani;

		// Token: 0x04007A3E RID: 31294
		[Header("이벤트용 태그")]
		public GameObject m_objEventTag;
	}
}
