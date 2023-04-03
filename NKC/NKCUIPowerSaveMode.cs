using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D3 RID: 2003
	public class NKCUIPowerSaveMode : MonoBehaviour
	{
		// Token: 0x06004F02 RID: 20226 RVA: 0x0017DCB3 File Offset: 0x0017BEB3
		private void OnDestroy()
		{
			this.m_imgFadeIn.DOKill(false);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x0017DCC4 File Offset: 0x0017BEC4
		private void Start()
		{
			this.SetBlackScreen(false);
			NKCUtil.SetEventTriggerDelegate(this.m_etBG, delegate(BaseEventData data)
			{
				this.OnBeginDrag(data);
			}, EventTriggerType.BeginDrag, true);
			NKCUtil.SetEventTriggerDelegate(this.m_etBG, delegate(BaseEventData data)
			{
				this.OnDrag(data);
			}, EventTriggerType.Drag, false);
			NKCUtil.SetEventTriggerDelegate(this.m_etBG, delegate(BaseEventData data)
			{
				this.OnEndDrag(data);
			}, EventTriggerType.EndDrag, false);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x0017DD25 File Offset: 0x0017BF25
		private void ResetDragState()
		{
			this.m_fDragOffset = 0f;
			this.m_fDragMoveX = 0f;
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0017DD40 File Offset: 0x0017BF40
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.ResetDragState();
			this.m_bJukeBoxMode = NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode;
			NKCUtil.SetGameobjectActive(this.m_objJukeBoxBG, this.m_bJukeBoxMode);
			NKCUtil.SetGameobjectActive(this.m_amtorBG.gameObject, !this.m_bJukeBoxMode);
			NKCUtil.SetGameobjectActive(this.m_lbJukeBoxPlayTitle.gameObject, this.m_bJukeBoxMode);
			NKCUtil.SetGameobjectActive(this.m_objBattleText, !this.m_bJukeBoxMode);
			NKCUtil.SetGameobjectActive(this.m_objJukeBoxText, this.m_bJukeBoxMode);
			NKCUtil.SetGameobjectActive(this.m_lbBattleProgressCount, !this.m_bJukeBoxMode);
			this.m_amtorEffect.SetFloat("Length", 0f);
			this.m_bComplete = false;
			this.UpdateUI();
			NKCUtil.SetGameobjectActive(this.m_imgFadeIn, true);
			this.m_imgFadeIn.color = new Color(this.m_imgFadeIn.color.r, this.m_imgFadeIn.color.g, this.m_imgFadeIn.color.b, 1f);
			this.m_imgFadeIn.DOFade(0f, 1.3f);
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0017DE76 File Offset: 0x0017C076
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_bComplete = false;
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0017DE8B File Offset: 0x0017C08B
		public void SetBlackScreen(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objBlackScreen, bSet);
			this.ResetDragState();
			if (!bSet)
			{
				this.m_amtorEffect.SetFloat("Length", 0f);
				this.UpdateUI();
			}
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0017DEC0 File Offset: 0x0017C0C0
		private void UpdateUI()
		{
			NKCUtil.SetLabelText(this.m_lbTime, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
			bool isOnGoing = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
			if (!this.m_bComplete && !isOnGoing)
			{
				this.m_bComplete = true;
				NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetLastKeyInputTime(Time.time);
				this.m_amtorBG.Play("NKM_UI_SLEEP_MODE_EFFECT_COMPLETE", -1, 0f);
			}
			NKCUtil.SetGameobjectActive(this.m_objBattleProgress, !this.bFinishJukeBox && (isOnGoing || this.m_bJukeBoxMode));
			if (isOnGoing)
			{
				long currRepeatCount = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetCurrRepeatCount();
				long maxRepeatCount = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetMaxRepeatCount();
				NKCUtil.SetLabelText(this.m_lbBattleProgressCount, string.Format("({0}/{1})", currRepeatCount, maxRepeatCount));
			}
			float batteryLevel = SystemInfo.batteryLevel;
			if (batteryLevel == -1f)
			{
				NKCUtil.SetGameobjectActive(this.m_objBattery, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objBattery, true);
				this.m_ImgBattery.fillAmount = batteryLevel;
				NKCUtil.SetLabelText(this.m_lbBatteryPercent, ((int)(batteryLevel * 100f)).ToString() + "%");
				if (batteryLevel <= 0.2f)
				{
					this.m_ImgBattery.color = new Color(1f, 0f, 0f, 1f);
				}
				else
				{
					this.m_ImgBattery.color = new Color(1f, 1f, 1f, 1f);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objInternet, false);
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0017E086 File Offset: 0x0017C286
		private void Update()
		{
			if (this.m_fLastTimeUpdate + 1f < Time.time)
			{
				this.m_fLastTimeUpdate = Time.time;
				this.UpdateUI();
			}
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0017E0AC File Offset: 0x0017C2AC
		public void OnBeginDrag(BaseEventData eventData)
		{
			this.ResetDragState();
			this.m_amtorEffect.SetFloat("Length", 0f);
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x0017E0CC File Offset: 0x0017C2CC
		public void OnDrag(BaseEventData eventData)
		{
			Vector2 delta = (eventData as PointerEventData).delta;
			this.m_fDragOffset += Vector2.Dot(delta, Vector2.right);
			this.m_fDragMoveX = this.m_fDragOffset * 0.002f;
			this.m_amtorEffect.SetFloat("Length", Mathf.Clamp(this.m_fDragMoveX, 0f, 1f));
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0017E134 File Offset: 0x0017C334
		public void OnEndDrag(BaseEventData eventData)
		{
			float num = Mathf.Clamp(this.m_fDragMoveX, 0f, 1f);
			if (num >= 0.85f)
			{
				this.m_amtorEffect.SetFloat("Length", num);
				NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetEnable(false);
			}
			else
			{
				this.m_amtorEffect.SetFloat("Length", 0f);
			}
			this.ResetDragState();
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x0017E19D File Offset: 0x0017C39D
		public void SetJukeBoxTitle(string lbTitle)
		{
			NKCUtil.SetLabelText(this.m_lbJukeBoxPlayTitle, lbTitle);
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x0017E1AB File Offset: 0x0017C3AB
		public void SetFinishJukeBox(bool bFinish)
		{
			this.bFinishJukeBox = bFinish;
			this.SetJukeBoxTitle(NKCUtilString.GET_STRING_JUKEBOX_FINISH_SLEEP_MODE);
		}

		// Token: 0x04003EF1 RID: 16113
		private const float DRAG_SENSITIVITY = 0.002f;

		// Token: 0x04003EF2 RID: 16114
		public Text m_lbTime;

		// Token: 0x04003EF3 RID: 16115
		public GameObject m_objBlackScreen;

		// Token: 0x04003EF4 RID: 16116
		public Animator m_amtorEffect;

		// Token: 0x04003EF5 RID: 16117
		public EventTrigger m_etBG;

		// Token: 0x04003EF6 RID: 16118
		public Animator m_amtorBG;

		// Token: 0x04003EF7 RID: 16119
		public Image m_imgFadeIn;

		// Token: 0x04003EF8 RID: 16120
		public GameObject m_objBattleProgress;

		// Token: 0x04003EF9 RID: 16121
		public Text m_lbBattleProgressCount;

		// Token: 0x04003EFA RID: 16122
		public GameObject m_objBattery;

		// Token: 0x04003EFB RID: 16123
		public Image m_ImgBattery;

		// Token: 0x04003EFC RID: 16124
		public Text m_lbBatteryPercent;

		// Token: 0x04003EFD RID: 16125
		public GameObject m_objInternet;

		// Token: 0x04003EFE RID: 16126
		private float m_fLastTimeUpdate;

		// Token: 0x04003EFF RID: 16127
		private float m_fDragOffset;

		// Token: 0x04003F00 RID: 16128
		private float m_fDragMoveX;

		// Token: 0x04003F01 RID: 16129
		private bool m_bComplete;

		// Token: 0x04003F02 RID: 16130
		[Header("주크박스")]
		public GameObject m_objJukeBoxBG;

		// Token: 0x04003F03 RID: 16131
		public Text m_lbJukeBoxPlayTitle;

		// Token: 0x04003F04 RID: 16132
		public GameObject m_objBattleText;

		// Token: 0x04003F05 RID: 16133
		public GameObject m_objJukeBoxText;

		// Token: 0x04003F06 RID: 16134
		private bool m_bJukeBoxMode;

		// Token: 0x04003F07 RID: 16135
		private bool bFinishJukeBox;
	}
}
