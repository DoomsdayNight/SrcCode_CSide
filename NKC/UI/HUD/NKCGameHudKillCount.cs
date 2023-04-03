using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C41 RID: 3137
	public class NKCGameHudKillCount : MonoBehaviour
	{
		// Token: 0x06009265 RID: 37477 RVA: 0x0031FB4B File Offset: 0x0031DD4B
		public void SetKillCount(long count)
		{
			NKCUtil.SetLabelText(this.m_lbCount, NKCStringTable.GetString("SI_DP_GAME_KILLCOUNT", new object[]
			{
				count
			}));
		}

		// Token: 0x06009266 RID: 37478 RVA: 0x0031FB71 File Offset: 0x0031DD71
		public void SetKillCount(string count)
		{
			NKCUtil.SetLabelText(this.m_lbCount, NKCStringTable.GetString("SI_DP_GAME_KILLCOUNT", new object[]
			{
				count
			}));
		}

		// Token: 0x04007F5E RID: 32606
		public Text m_lbCount;
	}
}
