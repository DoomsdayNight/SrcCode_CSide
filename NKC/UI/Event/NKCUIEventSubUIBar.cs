using System;
using System.Collections;
using NKC.UI.Guide;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Event
{
	// Token: 0x02000BDA RID: 3034
	public class NKCUIEventSubUIBar : NKCUIEventSubUIBase
	{
		// Token: 0x06008CB1 RID: 36017 RVA: 0x002FD814 File Offset: 0x002FBA14
		public override void Init()
		{
			base.Init();
			NKCUIEventBarPhaseEntry eventBarPhaseEntry = this.m_eventBarPhaseEntry;
			if (eventBarPhaseEntry != null)
			{
				eventBarPhaseEntry.Init();
			}
			NKCUIEventBarPhaseCreate eventBarPhaseCreate = this.m_eventBarPhaseCreate;
			if (eventBarPhaseCreate != null)
			{
				eventBarPhaseCreate.Init();
			}
			NKCUIEventBarResult eventBarResult = this.m_eventBarResult;
			if (eventBarResult != null)
			{
				eventBarResult.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCreatePhase, new UnityAction(this.OnClickCreatePhase));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnInitialPhase, new UnityAction(this.OnClickInitialPhase));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHelp, new UnityAction(this.OnClickHelp));
			NKCUtil.SetHotkey(this.m_csbtnCreatePhase, HotkeyEventType.NextTab, null, false);
			NKCUtil.SetHotkey(this.m_csbtnInitialPhase, HotkeyEventType.NextTab, null, false);
			NKCUtil.SetHotkey(this.m_csbtnHelp, HotkeyEventType.Help, null, false);
			this.m_AniEventGremoryBar.keepAnimatorControllerStateOnDisable = true;
		}

		// Token: 0x06008CB2 RID: 36018 RVA: 0x002FD8D8 File Offset: 0x002FBAD8
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			if (tabTemplet == null)
			{
				return;
			}
			NKCUIVoiceManager.StopVoice();
			this.m_tabTemplet = tabTemplet;
			NKCUIEventSubUIBar.EventID = tabTemplet.m_EventID;
			this.m_AniEventGremoryBar.Rebind();
			Animator aniEventGremoryBar = this.m_AniEventGremoryBar;
			if (aniEventGremoryBar != null)
			{
				aniEventGremoryBar.SetTrigger("INTRO");
			}
			NKCUIEventBarPhaseEntry eventBarPhaseEntry = this.m_eventBarPhaseEntry;
			if (eventBarPhaseEntry != null)
			{
				eventBarPhaseEntry.SetData(tabTemplet.m_EventID);
			}
			this.forceEntry = false;
			if (this.m_eventBarResult != null)
			{
				this.m_eventBarResult.Init();
				this.m_eventBarResult.Close();
			}
			this.m_phase = NKCUIEventSubUIBar.Phase.ENTRY;
			NKCUIEventSubUIBar.RefreshUI = false;
			base.SetDateLimit();
			this.m_introTimer = this.m_introDuration;
			base.StartCoroutine(this.IntroTimeElapse());
			if (PlayerPrefs.GetInt("EVENT_BAR_ALREADY_OPENED") == 0)
			{
				this.m_bBeginnerOpen = true;
				base.StartCoroutine(this.OpenHelp());
				return;
			}
			this.m_bBeginnerOpen = false;
		}

		// Token: 0x06008CB3 RID: 36019 RVA: 0x002FD9B8 File Offset: 0x002FBBB8
		public override void Refresh()
		{
			if (this.forceEntry)
			{
				this.Open(this.m_tabTemplet);
				return;
			}
			if (NKCUIEventSubUIBar.RefreshUI)
			{
				NKCUIEventSubUIBar.Phase phase = this.m_phase;
				if (phase != NKCUIEventSubUIBar.Phase.ENTRY)
				{
					if (phase == NKCUIEventSubUIBar.Phase.CREATE)
					{
						NKCUIEventBarPhaseCreate eventBarPhaseCreate = this.m_eventBarPhaseCreate;
						if (eventBarPhaseCreate != null)
						{
							eventBarPhaseCreate.Refresh();
						}
					}
				}
				else
				{
					NKCUIEventBarPhaseEntry eventBarPhaseEntry = this.m_eventBarPhaseEntry;
					if (eventBarPhaseEntry != null)
					{
						eventBarPhaseEntry.Refresh();
					}
				}
				NKCUIEventSubUIBar.RefreshUI = false;
			}
			if (NKCPopupEventBarMission.IsInstanceOpen)
			{
				NKCPopupEventBarMission.Instance.RefreshMission();
			}
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x002FDA2C File Offset: 0x002FBC2C
		public override void Close()
		{
			base.Close();
			NKCUIEventBarPhaseEntry eventBarPhaseEntry = this.m_eventBarPhaseEntry;
			if (eventBarPhaseEntry != null)
			{
				eventBarPhaseEntry.Close();
			}
			NKCUIEventBarPhaseCreate eventBarPhaseCreate = this.m_eventBarPhaseCreate;
			if (eventBarPhaseCreate != null)
			{
				eventBarPhaseCreate.Close();
			}
			NKCUIVoiceManager.StopVoice();
			this.forceEntry = true;
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public void ActivateCreateFx()
		{
			if (this.m_phase != NKCUIEventSubUIBar.Phase.CREATE)
			{
				return;
			}
			this.m_eventBarPhaseCreate.ActivateCreateFx();
		}

		// Token: 0x06008CB6 RID: 36022 RVA: 0x002FDA79 File Offset: 0x002FBC79
		public override void Hide()
		{
			base.Hide();
			if (this.m_phase == NKCUIEventSubUIBar.Phase.ENTRY)
			{
				this.m_eventBarPhaseEntry.Hide();
			}
		}

		// Token: 0x06008CB7 RID: 36023 RVA: 0x002FDA94 File Offset: 0x002FBC94
		private IEnumerator OpenHelp()
		{
			float delayTime = 1f;
			while (delayTime > 0f)
			{
				delayTime -= Time.deltaTime;
				yield return null;
			}
			PlayerPrefs.SetInt("EVENT_BAR_ALREADY_OPENED", 1);
			this.m_bBeginnerOpen = false;
			this.OnClickHelp();
			yield break;
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x002FDAA3 File Offset: 0x002FBCA3
		private IEnumerator IntroTimeElapse()
		{
			while (this.m_introTimer > 0f)
			{
				this.m_introTimer -= Time.deltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x06008CB9 RID: 36025 RVA: 0x002FDAB4 File Offset: 0x002FBCB4
		private void OnClickCreatePhase()
		{
			if (this.m_bBeginnerOpen)
			{
				return;
			}
			if (this.m_introTimer > 0f)
			{
				return;
			}
			NKCUIVoiceManager.StopVoice();
			NKCUIEventBarPhaseCreate eventBarPhaseCreate = this.m_eventBarPhaseCreate;
			if (eventBarPhaseCreate != null)
			{
				eventBarPhaseCreate.SetData(NKCUIEventSubUIBar.EventID);
			}
			Animator aniEventGremoryBar = this.m_AniEventGremoryBar;
			if (aniEventGremoryBar != null)
			{
				aniEventGremoryBar.SetTrigger("TRANS_TO_PH2");
			}
			this.m_phase = NKCUIEventSubUIBar.Phase.CREATE;
		}

		// Token: 0x06008CBA RID: 36026 RVA: 0x002FDB10 File Offset: 0x002FBD10
		private void OnClickInitialPhase()
		{
			if (NKCUIEventBarResult.IsInstanceOpen || this.m_bBeginnerOpen)
			{
				return;
			}
			NKCUIVoiceManager.StopVoice();
			NKCUIEventBarPhaseEntry eventBarPhaseEntry = this.m_eventBarPhaseEntry;
			if (eventBarPhaseEntry != null)
			{
				eventBarPhaseEntry.SetData(NKCUIEventSubUIBar.EventID);
			}
			Animator aniEventGremoryBar = this.m_AniEventGremoryBar;
			if (aniEventGremoryBar != null)
			{
				aniEventGremoryBar.SetTrigger("TRANS_TO_PH1");
			}
			this.m_phase = NKCUIEventSubUIBar.Phase.ENTRY;
		}

		// Token: 0x06008CBB RID: 36027 RVA: 0x002FDB65 File Offset: 0x002FBD65
		private void OnClickHelp()
		{
			if (this.m_bBeginnerOpen)
			{
				return;
			}
			NKCUIPopupTutorialImagePanel.Instance.Open(this.m_guideId, null);
		}

		// Token: 0x04007991 RID: 31121
		public Animator m_AniEventGremoryBar;

		// Token: 0x04007992 RID: 31122
		public NKCUIEventBarPhaseEntry m_eventBarPhaseEntry;

		// Token: 0x04007993 RID: 31123
		public NKCUIEventBarPhaseCreate m_eventBarPhaseCreate;

		// Token: 0x04007994 RID: 31124
		public NKCUIEventBarResult m_eventBarResult;

		// Token: 0x04007995 RID: 31125
		public NKCUIComStateButton m_csbtnCreatePhase;

		// Token: 0x04007996 RID: 31126
		public NKCUIComStateButton m_csbtnInitialPhase;

		// Token: 0x04007997 RID: 31127
		public NKCUIComStateButton m_csbtnHelp;

		// Token: 0x04007998 RID: 31128
		public float m_introDuration;

		// Token: 0x04007999 RID: 31129
		[Header("GUIDE ID(숫자)")]
		public int m_guideId;

		// Token: 0x0400799A RID: 31130
		private NKCUIEventSubUIBar.Phase m_phase;

		// Token: 0x0400799B RID: 31131
		private bool forceEntry;

		// Token: 0x0400799C RID: 31132
		private bool m_bBeginnerOpen;

		// Token: 0x0400799D RID: 31133
		private const string ALREADY_OPENED_KEY = "EVENT_BAR_ALREADY_OPENED";

		// Token: 0x0400799E RID: 31134
		private float m_introTimer;

		// Token: 0x0400799F RID: 31135
		public static int EventID;

		// Token: 0x040079A0 RID: 31136
		public static bool RefreshUI;

		// Token: 0x020019B5 RID: 6581
		private enum Phase
		{
			// Token: 0x0400AC9B RID: 44187
			ENTRY,
			// Token: 0x0400AC9C RID: 44188
			CREATE
		}
	}
}
