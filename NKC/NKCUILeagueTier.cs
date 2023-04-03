using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007C6 RID: 1990
	public class NKCUILeagueTier : MonoBehaviour
	{
		// Token: 0x06004EB5 RID: 20149 RVA: 0x0017BD6C File Offset: 0x00179F6C
		private void SetDisableNormalTier()
		{
			NKCUtil.SetGameobjectActive(this.m_objBronze, false);
			NKCUtil.SetGameobjectActive(this.m_objSilver, false);
			NKCUtil.SetGameobjectActive(this.m_objGold, false);
			NKCUtil.SetGameobjectActive(this.m_objPlatinum, false);
			NKCUtil.SetGameobjectActive(this.m_objDiamond, false);
			NKCUtil.SetGameobjectActive(this.m_objMaster, false);
			NKCUtil.SetGameobjectActive(this.m_objGrandMaster, false);
			NKCUtil.SetGameobjectActive(this.m_objChallenger, false);
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0017BDDC File Offset: 0x00179FDC
		public void SetUI(LEAGUE_TIER_ICON leagueTierIcon, int leagueTierNum)
		{
			NKCUtil.SetGameobjectActive(this.m_objBronze, leagueTierIcon == LEAGUE_TIER_ICON.LTI_BRONZE);
			NKCUtil.SetGameobjectActive(this.m_objSilver, leagueTierIcon == LEAGUE_TIER_ICON.LTI_SILVER);
			NKCUtil.SetGameobjectActive(this.m_objGold, leagueTierIcon == LEAGUE_TIER_ICON.LTI_GOLD);
			NKCUtil.SetGameobjectActive(this.m_objPlatinum, leagueTierIcon == LEAGUE_TIER_ICON.LTI_PLATINUM);
			NKCUtil.SetGameobjectActive(this.m_objDiamond, leagueTierIcon == LEAGUE_TIER_ICON.LTI_DIAMOND);
			NKCUtil.SetGameobjectActive(this.m_objMaster, leagueTierIcon == LEAGUE_TIER_ICON.LTI_MASTER);
			NKCUtil.SetGameobjectActive(this.m_objGrandMaster, leagueTierIcon == LEAGUE_TIER_ICON.LTI_GRANDMASTER);
			NKCUtil.SetGameobjectActive(this.m_objChallenger, leagueTierIcon == LEAGUE_TIER_ICON.LTI_CHALLENGER);
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_BRONZE)
			{
				this.m_lbBronzeNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_SILVER)
			{
				this.m_lbSilverNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_GOLD)
			{
				this.m_lbGoldNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_PLATINUM)
			{
				this.m_lbPlatinumNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_DIAMOND)
			{
				this.m_lbDiamondNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_MASTER)
			{
				this.m_lbMasterNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_GRANDMASTER)
			{
				this.m_lbGrandMasterNumber.text = leagueTierNum.ToString();
				return;
			}
			if (leagueTierIcon == LEAGUE_TIER_ICON.LTI_CHALLENGER)
			{
				this.m_lbChallengerNumber.text = leagueTierNum.ToString();
			}
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0017BF18 File Offset: 0x0017A118
		public void SetUI(NKMPvpRankTemplet cNKMPvpRankTemplet)
		{
			if (cNKMPvpRankTemplet == null)
			{
				return;
			}
			this.SetUI(cNKMPvpRankTemplet.LeagueTierIcon, cNKMPvpRankTemplet.LeagueTierIconNumber);
		}

		// Token: 0x04003E65 RID: 15973
		public GameObject m_objBronze;

		// Token: 0x04003E66 RID: 15974
		public GameObject m_objSilver;

		// Token: 0x04003E67 RID: 15975
		public GameObject m_objGold;

		// Token: 0x04003E68 RID: 15976
		public GameObject m_objPlatinum;

		// Token: 0x04003E69 RID: 15977
		public GameObject m_objDiamond;

		// Token: 0x04003E6A RID: 15978
		public GameObject m_objMaster;

		// Token: 0x04003E6B RID: 15979
		public GameObject m_objGrandMaster;

		// Token: 0x04003E6C RID: 15980
		public GameObject m_objChallenger;

		// Token: 0x04003E6D RID: 15981
		public Text m_lbBronzeNumber;

		// Token: 0x04003E6E RID: 15982
		public Text m_lbSilverNumber;

		// Token: 0x04003E6F RID: 15983
		public Text m_lbGoldNumber;

		// Token: 0x04003E70 RID: 15984
		public Text m_lbPlatinumNumber;

		// Token: 0x04003E71 RID: 15985
		public Text m_lbDiamondNumber;

		// Token: 0x04003E72 RID: 15986
		public Text m_lbMasterNumber;

		// Token: 0x04003E73 RID: 15987
		public Text m_lbGrandMasterNumber;

		// Token: 0x04003E74 RID: 15988
		public Text m_lbChallengerNumber;
	}
}
