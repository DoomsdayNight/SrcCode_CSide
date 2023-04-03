using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007C1 RID: 1985
	public class NKCUIGameHUDMultiplyReward : MonoBehaviour
	{
		// Token: 0x06004EA0 RID: 20128 RVA: 0x0017B651 File Offset: 0x00179851
		public void SetMultiply(int multiply)
		{
			if (multiply <= 1)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_txt, NKCUtilString.GET_MULTIPLY_REWARD_ONE_PARAM, new object[]
			{
				multiply
			});
		}

		// Token: 0x04003E4C RID: 15948
		public Text m_txt;
	}
}
