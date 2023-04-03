using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B9 RID: 1977
	public class NKCUICutScenTitleMgr : MonoBehaviour
	{
		// Token: 0x06004E4A RID: 20042 RVA: 0x00179A60 File Offset: 0x00177C60
		public void SetPause(bool bPause)
		{
			this.m_bPause = bPause;
			if (this.UsingTMPro())
			{
				NKCUtil.SetGameobjectActive(this.m_lbTmpTitle, !this.m_bPause);
				NKCUtil.SetGameobjectActive(this.m_lbTmpSubTitle, !this.m_bPause);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbTitle, !this.m_bPause);
			NKCUtil.SetGameobjectActive(this.m_lbSubTitle, !this.m_bPause);
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x00179AD0 File Offset: 0x00177CD0
		public static void InitUI(GameObject goNKM_UI_CUTSCEN_PLAYER)
		{
			if (NKCUICutScenTitleMgr.m_scNKCUICutScenTitleMgr != null)
			{
				return;
			}
			NKCUICutScenTitleMgr.m_scNKCUICutScenTitleMgr = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_TITLE_MGR").gameObject.GetComponent<NKCUICutScenTitleMgr>();
			NKCUICutScenTitleMgr.m_scNKCUICutScenTitleMgr.Close();
			NKCUICutScenTitleMgr.m_scNKCUICutScenTitleMgr.m_NKCUITypeWriter.SetTypingSound(true);
			NKCUICutScenTitleMgr.m_scNKCUICutScenTitleMgr.m_NKCUITypeWriter.SetSpaceSound(false);
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x00179B34 File Offset: 0x00177D34
		public void Reset()
		{
			this.SetPause(false);
			this.m_CanvasGroup.alpha = 1f;
			this.m_bTitleFadeOut = false;
			this.m_fTitleFadeOutTime = 0f;
			this.m_bDidFadeOut = false;
			if (this.m_Seq != null && this.m_Seq.IsActive() && this.m_Seq.IsPlaying())
			{
				this.m_Seq.Kill(false);
			}
			this.m_Seq = null;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00179BA8 File Offset: 0x00177DA8
		public void ForceClear()
		{
			if (this.m_Seq != null && this.m_Seq.IsActive() && this.m_Seq.IsPlaying())
			{
				this.m_Seq.Kill(false);
			}
			this.m_Seq = null;
			this.m_CanvasGroup.alpha = 0f;
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00179BFA File Offset: 0x00177DFA
		public static NKCUICutScenTitleMgr GetCutScenTitleMgr()
		{
			return NKCUICutScenTitleMgr.m_scNKCUICutScenTitleMgr;
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00179C04 File Offset: 0x00177E04
		private void Update()
		{
			if (this.m_bPause)
			{
				return;
			}
			if (this.m_CUTSCEN_TITLE_PLAY_STATE == NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_SUB || this.m_CUTSCEN_TITLE_PLAY_STATE == NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_MAIN)
			{
				this.m_NKCUITypeWriter.Update();
				if (this.m_CUTSCEN_TITLE_PLAY_STATE == NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_SUB)
				{
					if (!this.m_NKCUITypeWriter.IsTyping())
					{
						if (this.m_titleStr.Length > 0)
						{
							if (this.UsingTMPro())
							{
								this.m_NKCUITypeWriter.Start(this.m_lbTmpTitle, "", this.m_titleStr, this.m_fTitleTypeCoolTime, false);
							}
							else
							{
								this.m_NKCUITypeWriter.Start(this.m_lbTitle, "", this.m_titleStr, this.m_fTitleTypeCoolTime, false);
							}
							this.m_CUTSCEN_TITLE_PLAY_STATE++;
							return;
						}
						this.m_CUTSCEN_TITLE_PLAY_STATE = NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_END;
						this.m_bFinished = true;
						return;
					}
				}
				else if (this.m_CUTSCEN_TITLE_PLAY_STATE == NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_MAIN && !this.m_NKCUITypeWriter.IsTyping())
				{
					this.m_bFinished = true;
					this.m_CUTSCEN_TITLE_PLAY_STATE++;
				}
			}
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00179CFB File Offset: 0x00177EFB
		private bool CheckFadeOutCond()
		{
			return this.m_bTitleFadeOut;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00179D04 File Offset: 0x00177F04
		private void DoFadeOut()
		{
			if (this.m_Seq != null && this.m_Seq.IsActive() && this.m_Seq.IsPlaying())
			{
				this.m_Seq.Kill(false);
			}
			this.m_Seq = DOTween.Sequence();
			this.m_Seq.AppendInterval(this.m_fTitleFadeOutTime / 2f);
			this.m_Seq.Append(this.m_CanvasGroup.DOFade(0f, this.m_fTitleFadeOutTime / 2f));
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x00179D8A File Offset: 0x00177F8A
		public void Finish()
		{
			this.m_bFinished = true;
			this.m_NKCUITypeWriter.Finish();
			this.m_CUTSCEN_TITLE_PLAY_STATE = NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_END;
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00179DA5 File Offset: 0x00177FA5
		public bool IsFinshed()
		{
			return this.m_bFinished;
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x00179DB0 File Offset: 0x00177FB0
		public void Open(bool bTitleFadeOut, float fTitleFadeOutTime, string subTitle, string title, float fSubTitleTypeCoolTime = 0.15f, float fTitleTypeCoolTime = 0.15f)
		{
			this.m_bTitleFadeOut = bTitleFadeOut;
			this.m_fTitleFadeOutTime = fTitleFadeOutTime;
			this.m_bDidFadeOut = false;
			this.m_CanvasGroup.alpha = 1f;
			this.m_titleStr = title;
			this.m_subTitleStr = subTitle;
			this.m_fSubTitleTypeCoolTime = fSubTitleTypeCoolTime;
			this.m_fTitleTypeCoolTime = fTitleTypeCoolTime;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			NKCUtil.SetGameobjectActive(this.m_lbTitle, !this.UsingTMPro());
			NKCUtil.SetGameobjectActive(this.m_lbSubTitle, !this.UsingTMPro());
			NKCUtil.SetGameobjectActive(this.m_lbTmpTitle, this.UsingTMPro());
			NKCUtil.SetGameobjectActive(this.m_lbTmpSubTitle, this.UsingTMPro());
			if (this.UsingTMPro())
			{
				this.m_lbTmpTitle.SetText("", true);
				this.m_lbTmpSubTitle.SetText("", true);
			}
			else
			{
				this.m_lbTitle.text = "";
				this.m_lbSubTitle.text = "";
			}
			this.m_bFinished = false;
			if (subTitle.Length > 0)
			{
				this.m_CUTSCEN_TITLE_PLAY_STATE = NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_SUB;
				if (this.UsingTMPro())
				{
					this.m_NKCUITypeWriter.Start(this.m_lbTmpSubTitle, "", subTitle, this.m_fSubTitleTypeCoolTime, false);
					return;
				}
				this.m_NKCUITypeWriter.Start(this.m_lbSubTitle, "", subTitle, this.m_fSubTitleTypeCoolTime, false);
				return;
			}
			else
			{
				if (title.Length <= 0)
				{
					this.m_CUTSCEN_TITLE_PLAY_STATE = NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_END;
					this.m_bFinished = true;
					return;
				}
				this.m_CUTSCEN_TITLE_PLAY_STATE = NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE.CTPS_MAIN;
				if (this.UsingTMPro())
				{
					this.m_NKCUITypeWriter.Start(this.m_lbTmpTitle, "", this.m_titleStr, this.m_fTitleTypeCoolTime, false);
					return;
				}
				this.m_NKCUITypeWriter.Start(this.m_lbTitle, "", this.m_titleStr, this.m_fTitleTypeCoolTime, false);
				return;
			}
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x00179F7C File Offset: 0x0017817C
		public void Close()
		{
			if (this.CheckFadeOutCond())
			{
				if (!this.m_bDidFadeOut)
				{
					this.DoFadeOut();
				}
				this.m_bDidFadeOut = true;
			}
			else
			{
				if (this.m_Seq != null && this.m_Seq.IsActive() && this.m_Seq.IsPlaying())
				{
					this.m_Seq.Kill(false);
				}
				this.m_Seq = null;
				this.m_CanvasGroup.alpha = 0f;
			}
			this.Finish();
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00179FF3 File Offset: 0x001781F3
		private bool UsingTMPro()
		{
			return this.m_useTMPro && this.m_lbTmpSubTitle != null && this.m_lbTmpTitle != null;
		}

		// Token: 0x04003DE7 RID: 15847
		private static NKCUICutScenTitleMgr m_scNKCUICutScenTitleMgr;

		// Token: 0x04003DE8 RID: 15848
		private NKCUITypeWriter m_NKCUITypeWriter = new NKCUITypeWriter();

		// Token: 0x04003DE9 RID: 15849
		public Text m_lbTitle;

		// Token: 0x04003DEA RID: 15850
		public Text m_lbSubTitle;

		// Token: 0x04003DEB RID: 15851
		public TextMeshProUGUI m_lbTmpTitle;

		// Token: 0x04003DEC RID: 15852
		public TextMeshProUGUI m_lbTmpSubTitle;

		// Token: 0x04003DED RID: 15853
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04003DEE RID: 15854
		public bool m_useTMPro;

		// Token: 0x04003DEF RID: 15855
		private Sequence m_Seq;

		// Token: 0x04003DF0 RID: 15856
		private string m_titleStr = "";

		// Token: 0x04003DF1 RID: 15857
		private string m_subTitleStr = "";

		// Token: 0x04003DF2 RID: 15858
		private float m_fTitleTypeCoolTime = 0.15f;

		// Token: 0x04003DF3 RID: 15859
		private float m_fSubTitleTypeCoolTime = 0.15f;

		// Token: 0x04003DF4 RID: 15860
		private bool m_bFinished = true;

		// Token: 0x04003DF5 RID: 15861
		private NKCUICutScenTitleMgr.CUTSCEN_TITLE_PLAY_STATE m_CUTSCEN_TITLE_PLAY_STATE;

		// Token: 0x04003DF6 RID: 15862
		private bool m_bPause;

		// Token: 0x04003DF7 RID: 15863
		private bool m_bTitleFadeOut;

		// Token: 0x04003DF8 RID: 15864
		private float m_fTitleFadeOutTime;

		// Token: 0x04003DF9 RID: 15865
		private bool m_bDidFadeOut;

		// Token: 0x02001477 RID: 5239
		private enum CUTSCEN_TITLE_PLAY_STATE
		{
			// Token: 0x04009E47 RID: 40519
			CTPS_SUB,
			// Token: 0x04009E48 RID: 40520
			CTPS_MAIN,
			// Token: 0x04009E49 RID: 40521
			CTPS_END
		}
	}
}
