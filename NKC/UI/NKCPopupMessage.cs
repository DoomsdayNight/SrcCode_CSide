using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A27 RID: 2599
	public class NKCPopupMessage : NKCUIBase
	{
		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x060071CC RID: 29132 RVA: 0x0025D4CC File Offset: 0x0025B6CC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x060071CD RID: 29133 RVA: 0x0025D4CF File Offset: 0x0025B6CF
		public override string MenuName
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x0025D4D8 File Offset: 0x0025B6D8
		public void Open(PopupMessage msg)
		{
			base.gameObject.SetActive(true);
			if (!msg.m_bPreemptive)
			{
				this.m_queue.Enqueue(msg);
			}
			else
			{
				this.m_preemptiveMessage = msg;
			}
			if (msg.m_bPreemptive)
			{
				if (!this.m_bPlaying)
				{
					this.m_bPlaying = true;
					base.UIOpened(true);
				}
				base.StopAllCoroutines();
				base.StartCoroutine(this.ProcessPreemptiveMessage());
				return;
			}
			if (!this.m_bPlaying)
			{
				this.m_bPlaying = true;
				base.UIOpened(true);
				NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
				base.StopAllCoroutines();
				base.StartCoroutine(this.ProcessNormalMessage());
			}
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x0025D575 File Offset: 0x0025B775
		private IEnumerator ProcessPreemptiveMessage()
		{
			PopupMessage preemptiveMessage = this.m_preemptiveMessage;
			if (preemptiveMessage == null)
			{
				yield break;
			}
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objFX, preemptiveMessage.m_bShowFX);
			if (preemptiveMessage.m_bShowFX)
			{
				NKCSoundManager.PlaySound("FX_UI_DECK_SLOT_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
			}
			this.SetPosition(preemptiveMessage.m_messagePosition);
			this.m_lbMessage.text = preemptiveMessage.m_message;
			yield return new WaitForSeconds(preemptiveMessage.m_delayTime);
			yield return base.StartCoroutine(this.ProcessShowMessage());
			while (this.m_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			this.m_preemptiveMessage = null;
			if (this.m_queue.Count > 0)
			{
				base.StartCoroutine(this.ProcessNormalMessage());
			}
			else
			{
				base.Close();
			}
			yield break;
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x0025D584 File Offset: 0x0025B784
		private IEnumerator ProcessNormalMessage()
		{
			while (this.m_preemptiveMessage != null)
			{
				yield return null;
			}
			while (this.m_queue.Count > 0)
			{
				PopupMessage popupMessage = this.m_queue.Dequeue();
				if (popupMessage == null)
				{
					break;
				}
				NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
				this.SetPosition(popupMessage.m_messagePosition);
				this.m_lbMessage.text = popupMessage.m_message;
				yield return new WaitForSeconds(popupMessage.m_delayTime);
				yield return base.StartCoroutine(this.ProcessShowMessage());
				while (this.m_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
			}
			base.Close();
			yield break;
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x0025D594 File Offset: 0x0025B794
		private void SetPosition(NKCPopupMessage.eMessagePosition position)
		{
			switch (position)
			{
			case NKCPopupMessage.eMessagePosition.Top:
				this.m_rtMessageRoot.anchorMin = new Vector2(0f, 1f);
				this.m_rtMessageRoot.anchorMax = new Vector2(1f, 1f);
				this.m_rtMessageRoot.anchoredPosition = new Vector2(0f, -this.m_fAnchoredPosY);
				return;
			case NKCPopupMessage.eMessagePosition.Middle:
				this.m_rtMessageRoot.anchorMin = new Vector2(0f, 0.5f);
				this.m_rtMessageRoot.anchorMax = new Vector2(1f, 0.5f);
				this.m_rtMessageRoot.anchoredPosition = new Vector2(0f, 0f);
				return;
			case NKCPopupMessage.eMessagePosition.Bottom:
				this.m_rtMessageRoot.anchorMin = new Vector2(0f, 0f);
				this.m_rtMessageRoot.anchorMax = new Vector2(1f, 0f);
				this.m_rtMessageRoot.anchoredPosition = new Vector2(0f, this.m_fAnchoredPosY);
				return;
			case NKCPopupMessage.eMessagePosition.BottomIngame:
				this.m_rtMessageRoot.anchorMin = new Vector2(0f, 0f);
				this.m_rtMessageRoot.anchorMax = new Vector2(1f, 0f);
				this.m_rtMessageRoot.anchoredPosition = new Vector2(0f, 354f);
				return;
			case NKCPopupMessage.eMessagePosition.TopIngame:
				this.m_rtMessageRoot.anchorMin = new Vector2(0f, 1f);
				this.m_rtMessageRoot.anchorMax = new Vector2(1f, 1f);
				this.m_rtMessageRoot.anchoredPosition = new Vector2(0f, -317f);
				return;
			default:
				return;
			}
		}

		// Token: 0x060071D2 RID: 29138 RVA: 0x0025D749 File Offset: 0x0025B949
		private IEnumerator ProcessShowMessage()
		{
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, true);
			this.m_Ani.Play("NKM_UI_POPUP_MESSAGE", 0, 0f);
			yield return base.StartCoroutine(this.ProcessAlpha(0f, 1f, 0.5f));
			yield return new WaitForSeconds(2f);
			yield return base.StartCoroutine(this.ProcessAlpha(1f, 0f, 0.5f));
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
			yield break;
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x0025D758 File Offset: 0x0025B958
		private IEnumerator ProcessCloseOnly(float timeFadeOut)
		{
			if (timeFadeOut > 0f)
			{
				yield return base.StartCoroutine(this.ProcessAlpha(this.m_CanvasGroup.alpha, 0f, timeFadeOut));
			}
			else
			{
				this.m_CanvasGroup.alpha = 0f;
			}
			yield return null;
			base.Close();
			yield break;
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x0025D76E File Offset: 0x0025B96E
		private IEnumerator ProcessAlpha(float startValue, float endValue, float time)
		{
			float fDeltaTime = 0f;
			this.m_CanvasGroup.alpha = startValue;
			yield return null;
			for (fDeltaTime += Time.deltaTime; fDeltaTime < time; fDeltaTime += Time.deltaTime)
			{
				float alpha = NKCUtil.TrackValue(this.m_eFadeType, startValue, endValue, fDeltaTime, time);
				this.m_CanvasGroup.alpha = alpha;
				yield return null;
			}
			this.m_CanvasGroup.alpha = endValue;
			yield break;
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x0025D792 File Offset: 0x0025B992
		public override void CloseInternal()
		{
			this.m_CanvasGroup.alpha = 0f;
			this.m_bPlaying = false;
			this.m_queue.Clear();
			base.gameObject.SetActive(false);
		}

		// Token: 0x04005DBD RID: 23997
		public const float MESSAGE_STAY_TIME = 2f;

		// Token: 0x04005DBE RID: 23998
		public const float MESSAGE_FADE_TIME = 0.5f;

		// Token: 0x04005DBF RID: 23999
		public Animator m_Ani;

		// Token: 0x04005DC0 RID: 24000
		public Text m_lbMessage;

		// Token: 0x04005DC1 RID: 24001
		public RectTransform m_rtMessageRoot;

		// Token: 0x04005DC2 RID: 24002
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04005DC3 RID: 24003
		public GameObject m_objFX;

		// Token: 0x04005DC4 RID: 24004
		public TRACKING_DATA_TYPE m_eFadeType;

		// Token: 0x04005DC5 RID: 24005
		private PopupMessage m_preemptiveMessage;

		// Token: 0x04005DC6 RID: 24006
		private Queue<PopupMessage> m_queue = new Queue<PopupMessage>();

		// Token: 0x04005DC7 RID: 24007
		public float m_fAnchoredPosY = 127f;

		// Token: 0x04005DC8 RID: 24008
		private const float m_fAnchoredPosY_For_Ingame = 354f;

		// Token: 0x04005DC9 RID: 24009
		private const float m_fAnchoredPosY_For_IngameTop = 317f;

		// Token: 0x04005DCA RID: 24010
		private bool m_bPlaying;

		// Token: 0x02001764 RID: 5988
		public enum eMessagePosition
		{
			// Token: 0x0400A6B4 RID: 42676
			Top,
			// Token: 0x0400A6B5 RID: 42677
			Middle,
			// Token: 0x0400A6B6 RID: 42678
			Bottom,
			// Token: 0x0400A6B7 RID: 42679
			BottomIngame,
			// Token: 0x0400A6B8 RID: 42680
			TopIngame
		}
	}
}
