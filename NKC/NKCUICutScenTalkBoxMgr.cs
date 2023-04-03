using System;
using MyNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000808 RID: 2056
	public class NKCUICutScenTalkBoxMgr : MonoBehaviour, INKCUICutScenTalkBoxMgr
	{
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x0018C1AC File Offset: 0x0018A3AC
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

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x0600518E RID: 20878 RVA: 0x0018C1C4 File Offset: 0x0018A3C4
		public bool IsFinished
		{
			get
			{
				return this.m_bFinished;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x0018C1CC File Offset: 0x0018A3CC
		public NKCUICutScenTalkBoxMgr.TalkBoxType MyBoxType
		{
			get
			{
				return NKCUICutScenTalkBoxMgr.TalkBoxType.JapanNeeds;
			}
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x0018C1D0 File Offset: 0x0018A3D0
		public static INKCUICutScenTalkBoxMgr GetCutScenTalkBoxMgr(NKCUICutScenTalkBoxMgr.TalkBoxType talkBoxType)
		{
			if (NKCUICutScenTalkBoxMgr._curCutScenTalkBox != null && NKCUICutScenTalkBoxMgr._curCutScenTalkBox.MyBoxType == talkBoxType)
			{
				return NKCUICutScenTalkBoxMgr._curCutScenTalkBox;
			}
			if (talkBoxType > NKCUICutScenTalkBoxMgr.TalkBoxType.JapanNeeds)
			{
				if (talkBoxType != NKCUICutScenTalkBoxMgr.TalkBoxType.CenterText)
				{
					return NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr;
				}
				INKCUICutScenTalkBoxMgr curCutScenTalkBox = NKCUICutScenTalkBoxMgr._curCutScenTalkBox;
				if (curCutScenTalkBox != null)
				{
					curCutScenTalkBox.OnChange();
				}
				if (NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan == null)
				{
					NKCUICutScenTalkBoxMgr._curCutScenTalkBox = NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr;
				}
				else
				{
					NKCUICutScenTalkBoxMgr._curCutScenTalkBox = NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForCenterText;
				}
			}
			else
			{
				INKCUICutScenTalkBoxMgr curCutScenTalkBox2 = NKCUICutScenTalkBoxMgr._curCutScenTalkBox;
				if (curCutScenTalkBox2 != null)
				{
					curCutScenTalkBox2.OnChange();
				}
				if (NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan == null)
				{
					NKCUICutScenTalkBoxMgr._curCutScenTalkBox = NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr;
				}
				else
				{
					NKCUICutScenTalkBoxMgr._curCutScenTalkBox = NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan;
				}
			}
			return NKCUICutScenTalkBoxMgr._curCutScenTalkBox;
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x0018C278 File Offset: 0x0018A478
		public static void InitUI(GameObject goNKM_UI_CUTSCEN_PLAYER)
		{
			NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_PLAYER_TALK_BOX_MGR").gameObject.GetComponent<NKCUICutScenTalkBoxMgr>();
			Transform transform = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_PLAYER_TALK_BOX_MGR_FOR_JAPAN");
			NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan = ((transform != null) ? transform.gameObject.GetComponent<NKCUICutScenTalkBoxMgrForJapan>() : null);
			Transform transform2 = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_PLAYER_TALK_BOX_MGR_FOR_CENTER_TEXT");
			NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForCenterText = ((transform2 != null) ? transform2.gameObject.GetComponent<NKCUICutScenTalkBoxMgrForCenterText>() : null);
			if (NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr != null)
			{
				NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr.Close();
			}
			if (NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan != null)
			{
				NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan.Close();
			}
			if (NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForCenterText != null)
			{
				NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForCenterText.Close();
			}
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0018C335 File Offset: 0x0018A535
		public static void OnCleanUp()
		{
			NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgr = null;
			NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForJapan = null;
			NKCUICutScenTalkBoxMgr.m_NKCUICutScenTalkBoxMgrForCenterText = null;
			NKCUICutScenTalkBoxMgr._curCutScenTalkBox = null;
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0018C34F File Offset: 0x0018A54F
		public void ResetTalkBox()
		{
			this.SetPause(false);
			this.m_bOpenStateBeforePause = false;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0018C360 File Offset: 0x0018A560
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

		// Token: 0x06005195 RID: 20885 RVA: 0x0018C3B8 File Offset: 0x0018A5B8
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

		// Token: 0x06005196 RID: 20886 RVA: 0x0018C47C File Offset: 0x0018A67C
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
			NKCUtil.SetGameobjectActive(this.m_lbTalk, !this.UsingTMPText());
			NKCUtil.SetGameobjectActive(this.m_lbTmpTalk, this.UsingTMPText());
			if (this.UsingTMPText())
			{
				this.m_lbTmpTalkerName.SetText(NKCUITypeWriter.ReplaceNameString(_TalkerName, false), true);
			}
			else
			{
				this.m_lbTalkerName.text = NKCUITypeWriter.ReplaceNameString(_TalkerName, false);
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
					this.m_NKCUITypeWriter.Start(this.m_lbTmpTalk, _TalkerName, this.m_goalTalk, fCoolTime, _bTalkAppend);
					return;
				}
				this.m_NKCUITypeWriter.Start(this.m_lbTalk, _TalkerName, this.m_goalTalk, fCoolTime, _bTalkAppend);
			}
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x0018C630 File Offset: 0x0018A830
		public void StartFadeIn(float time)
		{
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0018C632 File Offset: 0x0018A832
		public void FadeOutBooking(float time)
		{
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0018C634 File Offset: 0x0018A834
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

		// Token: 0x0600519A RID: 20890 RVA: 0x0018C684 File Offset: 0x0018A884
		public void ClearTalk()
		{
			this.m_lbTalk.text = "";
			this.m_lbTalkerName.text = "";
			this.m_lbTmpTalk.SetText("", true);
			this.m_lbTmpTalkerName.SetText("", true);
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x0018C6D3 File Offset: 0x0018A8D3
		public void Close()
		{
			if (base.gameObject != null && base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0018C6FC File Offset: 0x0018A8FC
		public void OnChange()
		{
			this.Close();
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0018C704 File Offset: 0x0018A904
		public bool UsingTMPText()
		{
			return this.m_useTMPro && this.m_lbTmpTalk != null;
		}

		// Token: 0x040041F7 RID: 16887
		public Text m_lbTalkerName;

		// Token: 0x040041F8 RID: 16888
		public Text m_lbTalk;

		// Token: 0x040041F9 RID: 16889
		public TextMeshProUGUI m_lbTmpTalkerName;

		// Token: 0x040041FA RID: 16890
		public TextMeshProUGUI m_lbTmpTalk;

		// Token: 0x040041FB RID: 16891
		private string m_goalTalk;

		// Token: 0x040041FC RID: 16892
		public GameObject m_goTalkNext;

		// Token: 0x040041FD RID: 16893
		public CanvasGroup m_CanvasGroup;

		// Token: 0x040041FE RID: 16894
		public bool m_useTMPro;

		// Token: 0x040041FF RID: 16895
		private NKCUITypeWriter m_NKCUITypeWriter = new NKCUITypeWriter();

		// Token: 0x04004200 RID: 16896
		private static NKCUICutScenTalkBoxMgr m_NKCUICutScenTalkBoxMgr;

		// Token: 0x04004201 RID: 16897
		private static NKCUICutScenTalkBoxMgrForJapan m_NKCUICutScenTalkBoxMgrForJapan;

		// Token: 0x04004202 RID: 16898
		private static NKCUICutScenTalkBoxMgrForCenterText m_NKCUICutScenTalkBoxMgrForCenterText;

		// Token: 0x04004203 RID: 16899
		private static INKCUICutScenTalkBoxMgr _curCutScenTalkBox;

		// Token: 0x04004204 RID: 16900
		private bool m_bFinished = true;

		// Token: 0x04004205 RID: 16901
		private bool m_bWaitClick;

		// Token: 0x04004206 RID: 16902
		private bool m_bOpenStateBeforePause;

		// Token: 0x04004207 RID: 16903
		private bool m_bPause;

		// Token: 0x020014C4 RID: 5316
		public enum TalkBoxType
		{
			// Token: 0x04009F02 RID: 40706
			Default,
			// Token: 0x04009F03 RID: 40707
			JapanNeeds,
			// Token: 0x04009F04 RID: 40708
			CenterText
		}
	}
}
