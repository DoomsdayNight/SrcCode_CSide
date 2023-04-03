using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI.Result
{
	// Token: 0x02000BA4 RID: 2980
	public class NKCUIResultSubUIShadowLife : NKCUIResultSubUIBase
	{
		// Token: 0x060089D0 RID: 35280 RVA: 0x002EB9C8 File Offset: 0x002E9BC8
		public void SetData(BATTLE_RESULT_TYPE resultType, int prevLife, int currLife, bool bIgnoreAutoClose = false)
		{
			if (resultType != BATTLE_RESULT_TYPE.BRT_LOSE || prevLife <= currLife)
			{
				base.ProcessRequired = false;
				return;
			}
			this.m_prevLife = prevLife;
			this.m_updateLife = currLife;
			for (int i = 0; i < this.m_lstLife.Count; i++)
			{
				if (i < this.m_prevLife)
				{
					this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE");
				}
				else
				{
					this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE_OFF");
				}
			}
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x002EBA4F File Offset: 0x002E9C4F
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x002EBA6C File Offset: 0x002E9C6C
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x002EBA74 File Offset: 0x002E9C74
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bHadUserInput = false;
			float currentTime = 0f;
			int lifeIdx = -1;
			if (this.m_prevLife != this.m_updateLife)
			{
				for (int i = 0; i < this.m_lstLife.Count; i++)
				{
					if (i < this.m_updateLife)
					{
						this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE");
					}
					else if (i < this.m_prevLife)
					{
						this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE_DOWN");
						lifeIdx = i;
					}
					else
					{
						this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE_OFF");
					}
				}
				yield return null;
			}
			if (bAutoSkip)
			{
				while (this.UI_END_DELAY_TIME > currentTime)
				{
					if (this.m_bHadUserInput)
					{
						break;
					}
					if (!this.m_bPause)
					{
						currentTime += Time.deltaTime;
					}
					yield return null;
				}
			}
			else
			{
				while (this.m_bPause)
				{
					yield return null;
				}
				yield return base.WaitAniOrInput(this.m_lstLife[lifeIdx]);
			}
			this.FinishProcess();
			yield break;
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x002EBA8A File Offset: 0x002E9C8A
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04007646 RID: 30278
		public List<Animator> m_lstLife;

		// Token: 0x04007647 RID: 30279
		public float UI_END_DELAY_TIME = 2f;

		// Token: 0x04007648 RID: 30280
		private bool m_bFinished;

		// Token: 0x04007649 RID: 30281
		private int m_prevLife;

		// Token: 0x0400764A RID: 30282
		private int m_updateLife;
	}
}
