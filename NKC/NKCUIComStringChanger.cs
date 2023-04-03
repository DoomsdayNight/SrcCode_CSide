using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000764 RID: 1892
	public class NKCUIComStringChanger : MonoBehaviour
	{
		// Token: 0x06004B8A RID: 19338 RVA: 0x00169E6C File Offset: 0x0016806C
		public void Translate()
		{
			for (int i = 0; i < this.m_lstTargetStringInfoToChange.Count; i++)
			{
				TargetStringInfoToChange targetStringInfoToChange = this.m_lstTargetStringInfoToChange[i];
				NKCUtil.SetLabelText(targetStringInfoToChange.m_lbText, NKCStringTable.GetString(targetStringInfoToChange.m_Key, false));
				if (targetStringInfoToChange.m_doTweenAni != null && targetStringInfoToChange.m_doTweenAni.targetType == DOTweenAnimation.TargetType.Text)
				{
					targetStringInfoToChange.m_doTweenAni.endValueString = NKCStringTable.GetString(targetStringInfoToChange.m_Key, false);
				}
				if (targetStringInfoToChange.m_inputField != null)
				{
					targetStringInfoToChange.m_inputField.text = NKCStringTable.GetString(targetStringInfoToChange.m_Key, false);
				}
			}
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x00169F12 File Offset: 0x00168112
		private void Awake()
		{
			if (!this.m_bTranslateAtStart)
			{
				this.Translate();
			}
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x00169F22 File Offset: 0x00168122
		private void Start()
		{
			if (this.m_bTranslateAtStart)
			{
				this.Translate();
			}
		}

		// Token: 0x04003A2D RID: 14893
		public bool m_bTranslateAtStart;

		// Token: 0x04003A2E RID: 14894
		public List<TargetStringInfoToChange> m_lstTargetStringInfoToChange = new List<TargetStringInfoToChange>();
	}
}
