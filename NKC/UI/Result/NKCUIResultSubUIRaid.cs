using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA2 RID: 2978
	public class NKCUIResultSubUIRaid : NKCUIResultSubUIBase
	{
		// Token: 0x060089B7 RID: 35255 RVA: 0x002EAE30 File Offset: 0x002E9030
		public void SetData(NKCUIResult.BattleResultData data, bool bIgnoreAutoClose = false)
		{
			if (data == null || data.m_RaidBossResultData == null)
			{
				base.ProcessRequired = false;
				return;
			}
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
			int num;
			if (data.m_RaidBossResultData.curHP > 0f && data.m_RaidBossResultData.curHP < 1f)
			{
				num = 1;
			}
			else
			{
				num = (int)data.m_RaidBossResultData.curHP;
			}
			int num2 = (int)data.m_RaidBossResultData.maxHp;
			float num3 = (float)num / (float)num2 * 100f;
			int num4 = (int)data.m_RaidBossResultData.damage;
			float num5 = 0f;
			if (num2 != 0)
			{
				num5 = (float)num4 / (float)num2 * 100f;
			}
			this.m_lbDamageAmount.text = string.Format(string.Format("{0} ({1}%)", num4, string.Format("{0:0.##}", num5)), Array.Empty<object>());
			this.m_lbRemainBossHP.text = string.Format(string.Format("{0} ({1}%)", num, string.Format("{0:0.##}", num3)), Array.Empty<object>());
			this.m_imgRemainBossHP.fillAmount = num3 / 100f;
		}

		// Token: 0x060089B8 RID: 35256 RVA: 0x002EAF4F File Offset: 0x002E914F
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089B9 RID: 35257 RVA: 0x002EAF57 File Offset: 0x002E9157
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			yield return new WaitForSeconds(this.RAID_DELAY_TIME);
			this.m_bFinished = true;
			yield break;
		}

		// Token: 0x060089BA RID: 35258 RVA: 0x002EAF66 File Offset: 0x002E9166
		public override void FinishProcess()
		{
			this.m_bFinished = true;
		}

		// Token: 0x0400761F RID: 30239
		public Text m_lbDamageAmount;

		// Token: 0x04007620 RID: 30240
		public Text m_lbRemainBossHP;

		// Token: 0x04007621 RID: 30241
		public Image m_imgRemainBossHP;

		// Token: 0x04007622 RID: 30242
		private bool m_bFinished;

		// Token: 0x04007623 RID: 30243
		private float RAID_DELAY_TIME = 1f;
	}
}
