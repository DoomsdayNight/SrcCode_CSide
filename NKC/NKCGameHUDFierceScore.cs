using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000796 RID: 1942
	public class NKCGameHUDFierceScore : MonoBehaviour
	{
		// Token: 0x06004C4D RID: 19533 RVA: 0x0016D7D8 File Offset: 0x0016B9D8
		public void SetData(int totalScore, NKCGameHUDFierceScore.SCORE_TYPE scoreType)
		{
			if (!this.m_bFirstOpen)
			{
				this.m_bFirstOpen = true;
				this.m_iTotalScore = totalScore;
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_SCORE_ADD_ANI, false);
				NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_SCORE, this.m_iTotalScore.ToString());
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_SCORE_TEXT, scoreType == NKCGameHUDFierceScore.SCORE_TYPE.FIERCE);
				NKCUtil.SetGameobjectActive(this.m_TRIM_BATTLE_SCORE_TEXT, scoreType == NKCGameHUDFierceScore.SCORE_TYPE.TRIM);
				this.OnActive();
				return;
			}
			if (totalScore > this.m_iTotalScore)
			{
				this.m_iAddScore = totalScore - this.m_iTotalScore;
			}
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0016D859 File Offset: 0x0016BA59
		private void OnActive()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			Animator nuf_GAME_HUD_FIERCE_BATTLE = this.m_NUF_GAME_HUD_FIERCE_BATTLE;
			if (nuf_GAME_HUD_FIERCE_BATTLE != null)
			{
				nuf_GAME_HUD_FIERCE_BATTLE.SetTrigger("PLAY");
			}
			this.bDisplayScore = true;
			this.SetDelayTimer();
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0016D88A File Offset: 0x0016BA8A
		private void SetDelayTimer()
		{
			this.m_fCurTime = Time.deltaTime;
			this.m_fNextTime = this.m_fCurTime + this.CONST_ANI_DELAY_TIME;
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0016D8AC File Offset: 0x0016BAAC
		private void Update()
		{
			if (this.bDisplayScore)
			{
				this.m_fCurTime += Time.deltaTime;
				if (this.m_iAddScore > 0 && this.m_fCurTime > this.m_fNextTime)
				{
					if (!this.bAddAni)
					{
						this.bAddAni = true;
						NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_SCORE_ADD_ANI, true);
					}
					this.m_iTotalScore += this.m_iAddScore;
					NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_SCORE_ADD, "+ " + this.m_iAddScore.ToString());
					NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_SCORE, this.m_iTotalScore.ToString());
					this.m_iAddScore = 0;
					Animator aniFIERCE_BATTLE_SCORE_ADD_ANI = this.m_aniFIERCE_BATTLE_SCORE_ADD_ANI;
					if (aniFIERCE_BATTLE_SCORE_ADD_ANI != null)
					{
						aniFIERCE_BATTLE_SCORE_ADD_ANI.SetTrigger("PLAY");
					}
					this.SetDelayTimer();
				}
			}
		}

		// Token: 0x04003BFD RID: 15357
		public Animator m_NUF_GAME_HUD_FIERCE_BATTLE;

		// Token: 0x04003BFE RID: 15358
		public Text m_FIERCE_BATTLE_SCORE;

		// Token: 0x04003BFF RID: 15359
		public GameObject m_FIERCE_BATTLE_SCORE_ADD_ANI;

		// Token: 0x04003C00 RID: 15360
		public Animator m_aniFIERCE_BATTLE_SCORE_ADD_ANI;

		// Token: 0x04003C01 RID: 15361
		public Text m_FIERCE_BATTLE_SCORE_ADD;

		// Token: 0x04003C02 RID: 15362
		public GameObject m_FIERCE_BATTLE_SCORE_TEXT;

		// Token: 0x04003C03 RID: 15363
		public GameObject m_TRIM_BATTLE_SCORE_TEXT;

		// Token: 0x04003C04 RID: 15364
		private bool bDisplayScore;

		// Token: 0x04003C05 RID: 15365
		private bool m_bFirstOpen;

		// Token: 0x04003C06 RID: 15366
		private int m_iTotalScore;

		// Token: 0x04003C07 RID: 15367
		private int m_iAddScore;

		// Token: 0x04003C08 RID: 15368
		private bool bAddAni;

		// Token: 0x04003C09 RID: 15369
		private float CONST_ANI_DELAY_TIME = 0.3f;

		// Token: 0x04003C0A RID: 15370
		private float m_fCurTime;

		// Token: 0x04003C0B RID: 15371
		private float m_fNextTime;

		// Token: 0x04003C0C RID: 15372
		private const string ANI_PLAY = "PLAY";

		// Token: 0x0200145A RID: 5210
		public enum SCORE_TYPE
		{
			// Token: 0x04009E21 RID: 40481
			FIERCE,
			// Token: 0x04009E22 RID: 40482
			TRIM
		}
	}
}
