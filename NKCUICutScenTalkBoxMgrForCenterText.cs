using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000019 RID: 25
public class NKCUICutScenTalkBoxMgrForCenterText : MonoBehaviour, INKCUICutScenTalkBoxMgr
{
	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003FA3 File Offset: 0x000021A3
	public bool WaitForFadOut
	{
		get
		{
			return this.m_fFadeOutTime > 0f;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003FB2 File Offset: 0x000021B2
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

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003FCA File Offset: 0x000021CA
	public bool IsFinished
	{
		get
		{
			return this.m_bFinished;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003FD2 File Offset: 0x000021D2
	public NKCUICutScenTalkBoxMgr.TalkBoxType MyBoxType
	{
		get
		{
			return NKCUICutScenTalkBoxMgr.TalkBoxType.CenterText;
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00003FD8 File Offset: 0x000021D8
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
		if (!this.m_bFinishedFade)
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

	// Token: 0x060000D6 RID: 214 RVA: 0x000040A4 File Offset: 0x000022A4
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

	// Token: 0x060000D7 RID: 215 RVA: 0x000040FA File Offset: 0x000022FA
	public void ResetTalkBox()
	{
		this.SetPause(false);
		this.m_bOpenStateBeforePause = false;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000410C File Offset: 0x0000230C
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

	// Token: 0x060000D9 RID: 217 RVA: 0x0000415C File Offset: 0x0000235C
	public void Open(string _TalkerName, string _Talk, float fCoolTime, bool bWaitClick, bool _bTalkAppend)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (!base.gameObject.activeSelf)
		{
			this.m_CanvasGroup.alpha = 1f;
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
		if (fCoolTime <= 0f)
		{
			this.m_lbTalk.text = _Talk;
			this.m_bFinished = true;
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
				this.m_lbTalk.text = "";
			}
			this.m_NKCUITypeWriter.Start(this.m_lbTalk, this.m_goalTalk, fCoolTime, _bTalkAppend);
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00004250 File Offset: 0x00002450
	public void StartFadeIn(float time)
	{
		if (this.m_bFinishedFade)
		{
			return;
		}
		this.m_bFinishedFade = false;
		if (DOTween.IsTweening(this.m_NKM_UI_CUTSCEN_PLAYER_TALK_BOX, false))
		{
			this.m_NKM_UI_CUTSCEN_PLAYER_TALK_BOX.DOKill(false);
		}
		Sequence s = DOTween.Sequence();
		s.Append(this.m_NKM_UI_CUTSCEN_PLAYER_TALK_BOX.DOColor(new Color(1f, 1f, 1f, 0f), 0f));
		s.AppendInterval(0.1f);
		s.Append(this.m_NKM_UI_CUTSCEN_PLAYER_TALK_BOX.DOColor(new Color(1f, 1f, 1f, 0.7f), time).SetEase(Ease.OutSine).OnComplete(delegate
		{
			this.m_bFinishedFade = true;
		}));
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000430B File Offset: 0x0000250B
	public void FadeOutBooking(float time)
	{
		this.m_fFadeOutTime = time;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00004314 File Offset: 0x00002514
	public void StartFadeOut(Action onFadeComplete)
	{
		this.ClearTalk();
		this.m_NKM_UI_CUTSCEN_PLAYER_TALK_BOX.DOFade(0f, this.m_fFadeOutTime).SetEase(Ease.OutSine).OnComplete(delegate
		{
			Action onFadeComplete2 = onFadeComplete;
			if (onFadeComplete2 != null)
			{
				onFadeComplete2();
			}
			this.m_fFadeOutTime = 0f;
		});
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00004369 File Offset: 0x00002569
	public void ClearTalk()
	{
		NKCUtil.SetLabelText(this.m_lbTalk, string.Empty);
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000437B File Offset: 0x0000257B
	public void Close()
	{
		NKCUtil.SetGameobjectActive(this, false);
		this.m_bFinishedFade = false;
		this.m_fFadeOutTime = 0f;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00004396 File Offset: 0x00002596
	public void OnChange()
	{
		this.Close();
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000439E File Offset: 0x0000259E
	public bool UsingTMPText()
	{
		return false;
	}

	// Token: 0x04000060 RID: 96
	public Text m_lbTalk;

	// Token: 0x04000061 RID: 97
	public Image m_NKM_UI_CUTSCEN_PLAYER_TALK_BOX;

	// Token: 0x04000062 RID: 98
	public GameObject m_goTalkNext;

	// Token: 0x04000063 RID: 99
	public CanvasGroup m_CanvasGroup;

	// Token: 0x04000064 RID: 100
	private readonly NKCUITypeWriter m_NKCUITypeWriter = new NKCUITypeWriter();

	// Token: 0x04000065 RID: 101
	private string m_goalTalk;

	// Token: 0x04000066 RID: 102
	private bool m_bFinished = true;

	// Token: 0x04000067 RID: 103
	private bool m_bWaitClick;

	// Token: 0x04000068 RID: 104
	private bool m_bOpenStateBeforePause;

	// Token: 0x04000069 RID: 105
	private bool m_bPause;

	// Token: 0x0400006A RID: 106
	private bool m_bFinishedFade;

	// Token: 0x0400006B RID: 107
	private float m_fFadeOutTime;
}
