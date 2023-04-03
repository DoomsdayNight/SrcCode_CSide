using System;
using NKC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyNamespace
{
	// Token: 0x02000094 RID: 148
	public class NKCUICutScenTalkBoxMgrForJapan : MonoBehaviour, INKCUICutScenTalkBoxMgr
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00016158 File Offset: 0x00014358
		public GameObject MyGameObject
		{
			get
			{
				if (!(base.gameObject == null))
				{
					return base.gameObject;
				}
				return null;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00016170 File Offset: 0x00014370
		public bool IsFinished
		{
			get
			{
				return this.m_bFinished;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00016178 File Offset: 0x00014378
		public NKCUICutScenTalkBoxMgr.TalkBoxType MyBoxType
		{
			get
			{
				return NKCUICutScenTalkBoxMgr.TalkBoxType.JapanNeeds;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001617C File Offset: 0x0001437C
		private void Update()
		{
			if (this.m_bPause)
			{
				return;
			}
			if (this.m_bFinished)
			{
				return;
			}
			if (this.m_CanvasGroup.alpha < 1f)
			{
				this.m_CanvasGroup.alpha += Time.deltaTime * 3f;
				if (this.m_CanvasGroup.alpha >= 1f)
				{
					this.m_CanvasGroup.alpha = 1f;
				}
			}
			this.m_NKCUITypeWriter.Update();
			if (!this.m_NKCUITypeWriter.IsTyping())
			{
				if (!this.m_goTalkNext.activeSelf && this.m_bWaitClick)
				{
					this.m_goTalkNext.SetActive(true);
				}
				if (this.m_CanvasGroup.alpha >= 1f)
				{
					this.m_bFinished = true;
				}
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00016240 File Offset: 0x00014440
		public void SetPause(bool bPause)
		{
			if (base.gameObject == null)
			{
				return;
			}
			if (bPause)
			{
				this.m_bOpenStateBeforePause = base.gameObject.activeSelf;
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(base.gameObject, this.m_bOpenStateBeforePause);
			}
			this.m_bPause = bPause;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00016296 File Offset: 0x00014496
		public void ResetTalkBox()
		{
			this.SetPause(false);
			this.m_bOpenStateBeforePause = false;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000162A8 File Offset: 0x000144A8
		public void Finish()
		{
			this.m_NKCUITypeWriter.Finish();
			this.m_bFinished = true;
			if (!this.m_goTalkNext.activeSelf && this.m_bWaitClick)
			{
				this.m_goTalkNext.SetActive(true);
			}
			this.m_CanvasGroup.alpha = 1f;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000162F8 File Offset: 0x000144F8
		public void Open(string _TalkerName, string _Talk, float fCoolTime, bool bWaitClick, bool _bTalkAppend)
		{
			if (base.gameObject == null)
			{
				return;
			}
			if (!base.gameObject.activeSelf)
			{
				this.m_CanvasGroup.alpha = 0f;
				base.gameObject.SetActive(true);
			}
			this.m_bWaitClick = bWaitClick;
			if (_bTalkAppend)
			{
				this.m_goalTalk += _Talk;
			}
			else
			{
				this.m_goalTalk = _Talk;
			}
			NKCUtil.SetGameobjectActive(this.m_gTalkerNameLine, !string.IsNullOrEmpty(_TalkerName));
			NKCUtil.SetGameobjectActive(this.m_lbTmpTalkerName, this.UsingTMPText());
			NKCUtil.SetGameobjectActive(this.m_lbTmpTalk, this.UsingTMPText());
			NKCUtil.SetGameobjectActive(this.m_lbTalkerName, !this.UsingTMPText());
			NKCUtil.SetGameobjectActive(this.m_lbTalk, !this.UsingTMPText());
			if (this.UsingTMPText())
			{
				if (this.m_bTempFlag)
				{
					this.m_lbTmpTalkerName.SetText(NKCUITypeWriter.ReplaceNameString(_TalkerName, false), true);
				}
				else
				{
					this.m_lbTmpTalkerName.SetText(NKCUITypeWriter.ReplaceNameString(_TalkerName, false) + " ", true);
				}
				this.m_bTempFlag = !this.m_bTempFlag;
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTalkerName, NKCUITypeWriter.ReplaceNameString(_TalkerName, false));
			}
			if (fCoolTime <= 0f)
			{
				if (this.UsingTMPText())
				{
					this.m_lbTmpTalk.SetText(_Talk, true);
				}
				else
				{
					this.m_lbTalk.text = _Talk;
				}
				this.m_bFinished = true;
				this.m_CanvasGroup.alpha = 1f;
				if (!this.m_goTalkNext.activeSelf && bWaitClick)
				{
					this.m_goTalkNext.SetActive(true);
					return;
				}
			}
			else
			{
				if (this.m_goTalkNext.activeSelf)
				{
					this.m_goTalkNext.SetActive(false);
				}
				this.m_bFinished = false;
				if (!_bTalkAppend)
				{
					if (this.UsingTMPText())
					{
						this.m_lbTmpTalk.SetText("", true);
					}
					else
					{
						this.m_lbTalk.text = "";
					}
				}
				if (this.UsingTMPText())
				{
					this.m_NKCUITypeWriter.Start(this.m_lbTmpTalk, this.m_goalTalk, fCoolTime, _bTalkAppend);
					return;
				}
				this.m_NKCUITypeWriter.Start(this.m_lbTalk, this.m_goalTalk, fCoolTime, _bTalkAppend);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00016519 File Offset: 0x00014719
		public void StartFadeIn(float Time)
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001651B File Offset: 0x0001471B
		public void FadeOutBooking(float time)
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00016520 File Offset: 0x00014720
		public void ClearTalk()
		{
			NKCUtil.SetLabelText(this.m_lbTalk, string.Empty);
			NKCUtil.SetLabelText(this.m_lbTalkerName, string.Empty);
			TextMeshProUGUI lbTmpTalk = this.m_lbTmpTalk;
			if (lbTmpTalk != null)
			{
				lbTmpTalk.SetText(string.Empty, true);
			}
			TextMeshProUGUI lbTmpTalkerName = this.m_lbTmpTalkerName;
			if (lbTmpTalkerName != null)
			{
				lbTmpTalkerName.SetText(string.Empty, true);
			}
			NKCUtil.SetGameobjectActive(this.m_gTalkerNameLine, false);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00016587 File Offset: 0x00014787
		public void Close()
		{
			NKCUtil.SetGameobjectActive(this, false);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00016590 File Offset: 0x00014790
		public void OnChange()
		{
			this.Close();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00016598 File Offset: 0x00014798
		public bool UsingTMPText()
		{
			return this.useTMPro && this.m_lbTmpTalk != null && this.m_lbTmpTalkerName != null;
		}

		// Token: 0x040002BA RID: 698
		public GameObject m_gTalkerNameLine;

		// Token: 0x040002BB RID: 699
		public Text m_lbTalkerName;

		// Token: 0x040002BC RID: 700
		public Text m_lbTalk;

		// Token: 0x040002BD RID: 701
		public TextMeshProUGUI m_lbTmpTalkerName;

		// Token: 0x040002BE RID: 702
		public TextMeshProUGUI m_lbTmpTalk;

		// Token: 0x040002BF RID: 703
		private string m_goalTalk;

		// Token: 0x040002C0 RID: 704
		public GameObject m_goTalkNext;

		// Token: 0x040002C1 RID: 705
		public CanvasGroup m_CanvasGroup;

		// Token: 0x040002C2 RID: 706
		public bool useTMPro;

		// Token: 0x040002C3 RID: 707
		private NKCUITypeWriter m_NKCUITypeWriter = new NKCUITypeWriter();

		// Token: 0x040002C4 RID: 708
		private bool m_bFinished = true;

		// Token: 0x040002C5 RID: 709
		private bool m_bWaitClick;

		// Token: 0x040002C6 RID: 710
		private bool m_bOpenStateBeforePause;

		// Token: 0x040002C7 RID: 711
		private bool m_bPause;

		// Token: 0x040002C8 RID: 712
		private bool m_bTempFlag;
	}
}
