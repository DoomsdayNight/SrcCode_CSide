using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000AA5 RID: 2725
	public class NKCUITacticUpdateLevelSlot : MonoBehaviour
	{
		// Token: 0x060078DE RID: 30942 RVA: 0x00282238 File Offset: 0x00280438
		public void SetLevel(int setLevel, bool bShowActive = false)
		{
			int num = 0;
			foreach (NKCUITacticUpdateLevelSlot.LEVELUP_OBJECT levelup_OBJECT in this.m_lstLevelSlot)
			{
				if (!bShowActive)
				{
					NKCUtil.SetGameobjectActive(levelup_OBJECT.objActive, false);
					NKCUtil.SetGameobjectActive(levelup_OBJECT.objOFF, num >= setLevel);
					NKCUtil.SetGameobjectActive(levelup_OBJECT.objON, num < setLevel);
				}
				else
				{
					NKCUtil.SetGameobjectActive(levelup_OBJECT.objActive, num == setLevel);
					NKCUtil.SetGameobjectActive(levelup_OBJECT.objON, num < setLevel);
					NKCUtil.SetGameobjectActive(levelup_OBJECT.objOFF, num > setLevel);
				}
				num++;
			}
			NKCUtil.SetGameobjectActive(this.m_objFinishFX, bShowActive && setLevel == 6);
		}

		// Token: 0x0400655F RID: 25951
		public GameObject m_objFinishFX;

		// Token: 0x04006560 RID: 25952
		public List<NKCUITacticUpdateLevelSlot.LEVELUP_OBJECT> m_lstLevelSlot;

		// Token: 0x020017FB RID: 6139
		[Serializable]
		public struct LEVELUP_OBJECT
		{
			// Token: 0x0400A7CF RID: 42959
			public GameObject objOFF;

			// Token: 0x0400A7D0 RID: 42960
			public GameObject objON;

			// Token: 0x0400A7D1 RID: 42961
			public GameObject objActive;
		}
	}
}
