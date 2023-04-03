using System;
using ClientPacket.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AB1 RID: 2737
	public class NKCUITrimScoreSlot : MonoBehaviour
	{
		// Token: 0x060079CB RID: 31179 RVA: 0x00288F4C File Offset: 0x0028714C
		public void SetData(NKMTrimStageData trimStageData)
		{
			NKCUtil.SetLabelText(this.m_lbStageIndex, string.Format(NKCUtilString.GET_STRING_TRIM_STAGE_INDEX, (trimStageData.index + 1).ToString("D2")));
			NKCUtil.SetLabelText(this.m_lbScore, string.Format("{0:#,0}", trimStageData.score.ToString()));
			NKCUtil.SetGameobjectActive(this.m_objNone, !trimStageData.isWin);
			NKCUtil.SetGameobjectActive(this.m_objClear, trimStageData.isWin);
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x00288FC8 File Offset: 0x002871C8
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
		}

		// Token: 0x0400668B RID: 26251
		public Text m_lbStageIndex;

		// Token: 0x0400668C RID: 26252
		public Text m_lbScore;

		// Token: 0x0400668D RID: 26253
		public GameObject m_objNone;

		// Token: 0x0400668E RID: 26254
		public GameObject m_objClear;
	}
}
