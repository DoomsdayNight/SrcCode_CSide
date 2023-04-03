using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C4C RID: 3148
	public class NKCUIMainGageBuffCell : MonoBehaviour
	{
		// Token: 0x060092B7 RID: 37559 RVA: 0x00321525 File Offset: 0x0031F725
		public Image Get_UNIT_BUFF_Image()
		{
			return this.m_imgBuff;
		}

		// Token: 0x060092B8 RID: 37560 RVA: 0x0032152D File Offset: 0x0031F72D
		public Image Get_UNIT_BUFF_COOL_Image()
		{
			return this.m_imgCooldown;
		}

		// Token: 0x060092B9 RID: 37561 RVA: 0x00321535 File Offset: 0x0031F735
		public void InitUI()
		{
			NKCUtil.SetImageFillAmount(this.m_imgCooldown, 0f);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060092BA RID: 37562 RVA: 0x00321554 File Offset: 0x0031F754
		private void SetOverlapCount(int overlapCount)
		{
			if (this.m_lbOverlap == null)
			{
				return;
			}
			if (overlapCount <= 1)
			{
				NKCUtil.SetGameobjectActive(this.m_lbOverlap, false);
				this.m_lbOverlap.text = string.Empty;
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbOverlap, true);
			this.m_lbOverlap.text = overlapCount.ToString();
		}

		// Token: 0x060092BB RID: 37563 RVA: 0x003215B0 File Offset: 0x0031F7B0
		public void SetData(NKMBuffTemplet cNKMBuffTemplet, float fLifeTimeRate, int overlapCount)
		{
			this.m_imgCooldown.fillAmount = 1f - fLifeTimeRate;
			if (cNKMBuffTemplet != null && this.m_GageBuffID != (int)cNKMBuffTemplet.m_BuffID && cNKMBuffTemplet.m_IconName.Length > 1)
			{
				this.m_imgBuff.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_GAME_NKM_UNIT_SPRITE", cNKMBuffTemplet.m_IconName, false);
			}
			this.m_GageBuffID = (int)cNKMBuffTemplet.m_BuffID;
			this.SetOverlapCount(overlapCount);
		}

		// Token: 0x04007FB0 RID: 32688
		public Image m_imgBuff;

		// Token: 0x04007FB1 RID: 32689
		public Image m_imgCooldown;

		// Token: 0x04007FB2 RID: 32690
		public Text m_lbOverlap;

		// Token: 0x04007FB3 RID: 32691
		private int m_GageBuffID;
	}
}
