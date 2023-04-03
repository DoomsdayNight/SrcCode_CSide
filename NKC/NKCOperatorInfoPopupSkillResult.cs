using System;
using System.Collections;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007EA RID: 2026
	public class NKCOperatorInfoPopupSkillResult : NKCUIBase
	{
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x00184F14 File Offset: 0x00183114
		public static NKCOperatorInfoPopupSkillResult Instance
		{
			get
			{
				if (NKCOperatorInfoPopupSkillResult.m_Instance == null)
				{
					NKCOperatorInfoPopupSkillResult.m_Instance = NKCUIManager.OpenNewInstance<NKCOperatorInfoPopupSkillResult>("ab_ui_nkm_ui_operator_info", "NKM_UI_OPERATOR_INFO_POPUP_SKILL_RESULT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCOperatorInfoPopupSkillResult.CleanupInstance)).GetInstance<NKCOperatorInfoPopupSkillResult>();
					NKCOperatorInfoPopupSkillResult.m_Instance.Init();
				}
				return NKCOperatorInfoPopupSkillResult.m_Instance;
			}
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x00184F63 File Offset: 0x00183163
		private static void CleanupInstance()
		{
			NKCOperatorInfoPopupSkillResult.m_Instance = null;
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x00184F6B File Offset: 0x0018316B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCOperatorInfoPopupSkillResult.m_Instance != null && NKCOperatorInfoPopupSkillResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00184F86 File Offset: 0x00183186
		public static void CheckInstanceAndClose()
		{
			if (NKCOperatorInfoPopupSkillResult.m_Instance != null && NKCOperatorInfoPopupSkillResult.m_Instance.IsOpen)
			{
				NKCOperatorInfoPopupSkillResult.m_Instance.Close();
			}
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00184FAB File Offset: 0x001831AB
		private void OnDestroy()
		{
			NKCOperatorInfoPopupSkillResult.m_Instance = null;
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005062 RID: 20578 RVA: 0x00184FB3 File Offset: 0x001831B3
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_OPERATOR_SKILL_RESULT_TITLE;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x00184FBA File Offset: 0x001831BA
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00184FBD File Offset: 0x001831BD
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00184FD8 File Offset: 0x001831D8
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_Confirm, new UnityAction(this.OnStartResult));
			NKCUtil.SetHotkey(this.m_Confirm, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00185000 File Offset: 0x00183200
		public void Open(long targetUID, bool bTryMainSkill, bool bMainSkillLvUp, bool bTrySubskill, bool bSubskillLvUp, bool bTryImplantSubSkill, bool bImplantSubskill, int oldSubSkillID, int oldSubSkillLv)
		{
			this.m_lstEvent.Clear();
			this.m_Operator = NKCOperatorUtil.GetOperatorData(targetUID);
			if (this.m_Operator == null)
			{
				return;
			}
			if (bTryMainSkill)
			{
				if (bMainSkillLvUp)
				{
					this.m_lstEvent.Add(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_OK);
				}
				else
				{
					this.m_lstEvent.Add(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_FAIL);
				}
			}
			if (bTrySubskill)
			{
				if (bSubskillLvUp)
				{
					this.m_lstEvent.Add(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_OK);
				}
				else
				{
					this.m_lstEvent.Add(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_FAIL);
				}
			}
			if (bTryImplantSubSkill)
			{
				if (bImplantSubskill)
				{
					this.m_lstEvent.Add(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_OK);
				}
				else
				{
					this.m_lstEvent.Add(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_FAIL);
				}
			}
			this.m_preSubSkillID = oldSubSkillID;
			this.m_preSubSkillLevel = oldSubSkillLv;
			this.m_lastResultEventType = NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.NONE;
			this.m_bClickBlock = false;
			this.m_bPlayOutro = true;
			this.m_bPlaySkillUpVoice = false;
			base.UIOpened(true);
			this.OnStartResult();
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x001850CC File Offset: 0x001832CC
		private void OnStartResult()
		{
			if (this.m_bClickBlock)
			{
				return;
			}
			if (!this.m_bPlayOutro)
			{
				this.OnPlayOutro();
				return;
			}
			if (this.m_lstEvent.Count <= 0)
			{
				base.Close();
				return;
			}
			this.OnResult(this.m_lstEvent[0]);
			this.m_lstEvent.RemoveAt(0);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00185124 File Offset: 0x00183324
		private void OnPlayOutro()
		{
			switch (this.m_lastResultEventType)
			{
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_OK:
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_OK:
				this.m_Ani.SetTrigger(this.SuccessOutro);
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_FAIL:
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_FAIL:
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_FAIL:
				this.m_Ani.SetTrigger(this.FailOutro);
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_OK:
				this.m_Ani.SetTrigger(this.ImplantOutro);
				break;
			}
			this.m_bPlayOutro = true;
			base.StartCoroutine(this.StartDelayAni(this.m_Ani.GetCurrentAnimatorStateInfo(0).length - this.m_fDelayGap, this.m_lstEvent.Count > 0, this.m_lstEvent.Count <= 0));
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x001851DF File Offset: 0x001833DF
		private IEnumerator StartDelayAni(float delay, bool autoOpen = false, bool autoClose = false)
		{
			this.m_bClickBlock = true;
			yield return new WaitForSeconds(delay);
			this.m_bClickBlock = false;
			if (autoOpen)
			{
				this.OnStartResult();
			}
			if (autoClose)
			{
				base.Close();
			}
			yield break;
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x00185204 File Offset: 0x00183404
		private void OnResult(NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE type)
		{
			bool flag = false;
			string msg = "";
			this.m_lastResultEventType = type;
			switch (this.m_lastResultEventType)
			{
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_OK:
				this.m_OriginalSkill.SetData(this.m_Operator.mainSkill.id, (int)(this.m_Operator.mainSkill.level - 1), false);
				this.m_UpgradeSkill.SetData(this.m_Operator.mainSkill.id, (int)this.m_Operator.mainSkill.level, false);
				msg = NKCUtilString.GET_STRING_OPERATOR_MAIN_SKILL_SUCCESS;
				flag = true;
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_FAIL:
				this.m_FailedSkill.SetData(this.m_Operator.mainSkill.id, (int)this.m_Operator.mainSkill.level, false);
				msg = NKCUtilString.GET_STRING_OPERATOR_MAIN_SKILL_FAIL;
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_OK:
				this.m_OriginalSkill.SetData(this.m_preSubSkillID, this.m_preSubSkillLevel, false);
				this.m_UpgradeSkill.SetData(this.m_Operator.subSkill.id, (int)this.m_Operator.subSkill.level, false);
				msg = NKCUtilString.GET_STRING_OPERATOR_PASSIVE_SKILL_SUCCESS;
				flag = true;
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_FAIL:
				this.m_FailedSkill.SetData(this.m_Operator.subSkill.id, (int)this.m_Operator.subSkill.level, false);
				msg = NKCUtilString.GET_STRING_OPERATOR_PASSIVE_SKILL_FAIL;
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_OK:
				this.m_OriginalSkill.SetData(this.m_preSubSkillID, this.m_preSubSkillLevel, false);
				this.m_UpgradeSkill.SetData(this.m_Operator.subSkill.id, (int)this.m_Operator.subSkill.level, false);
				msg = NKCUtilString.GET_STRING_OPERATOR_PASSIVE_SKILL_IMPLANT_SUCCESS;
				flag = true;
				break;
			case NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_FAIL:
				this.m_FailedSkill.SetData(this.m_Operator.subSkill.id, (int)this.m_Operator.subSkill.level, false);
				msg = NKCUtilString.GET_STRING_OPERATOR_PASSIVE_SKILL_IMPLANT_FAIL;
				break;
			}
			if (!this.m_bPlaySkillUpVoice && (this.m_lastResultEventType == NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_OK || this.m_lastResultEventType == NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_TRANSFER_OK || this.m_lastResultEventType == NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_OK))
			{
				this.m_bPlaySkillUpVoice = true;
				base.StartCoroutine(this.PlaySkillGrowthVoice());
			}
			if (!flag)
			{
				this.m_Ani.SetTrigger(this.Fail);
			}
			else if (type == NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.MAIN_SKILL_UP_OK || type == NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE.SUB_SKILL_UP_OK)
			{
				this.m_Ani.SetTrigger(this.Success);
			}
			else
			{
				this.m_Ani.SetTrigger(this.Implant);
			}
			this.m_bPlayOutro = false;
			base.StartCoroutine(this.StartDelayAni(this.m_Ani.GetCurrentAnimatorStateInfo(0).length - this.m_fDelayGap, false, false));
			NKCUtil.SetGameobjectActive(this.m_Success, flag);
			NKCUtil.SetGameobjectActive(this.m_Fail, !flag);
			NKCUtil.SetLabelText(this.m_lbResult, msg);
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x001854C0 File Offset: 0x001836C0
		private IEnumerator PlaySkillGrowthVoice()
		{
			yield return new WaitForSeconds(this.m_fVoiceDelayTime);
			if (this.m_Operator != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_GROWTH_SKILL, this.m_Operator, false, true);
			}
			yield break;
		}

		// Token: 0x04004045 RID: 16453
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operator_info";

		// Token: 0x04004046 RID: 16454
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_INFO_POPUP_SKILL_RESULT";

		// Token: 0x04004047 RID: 16455
		private static NKCOperatorInfoPopupSkillResult m_Instance;

		// Token: 0x04004048 RID: 16456
		public NKCUIComStateButton m_Confirm;

		// Token: 0x04004049 RID: 16457
		public GameObject m_Success;

		// Token: 0x0400404A RID: 16458
		public GameObject m_Fail;

		// Token: 0x0400404B RID: 16459
		public NKCUIOperatorSkill m_OriginalSkill;

		// Token: 0x0400404C RID: 16460
		public NKCUIOperatorSkill m_UpgradeSkill;

		// Token: 0x0400404D RID: 16461
		public NKCUIOperatorSkill m_FailedSkill;

		// Token: 0x0400404E RID: 16462
		public Text m_lbResult;

		// Token: 0x0400404F RID: 16463
		public Animator m_Ani;

		// Token: 0x04004050 RID: 16464
		private bool m_bPlaySkillUpVoice;

		// Token: 0x04004051 RID: 16465
		private List<NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE> m_lstEvent = new List<NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE>();

		// Token: 0x04004052 RID: 16466
		private NKMOperator m_Operator;

		// Token: 0x04004053 RID: 16467
		private int m_preSubSkillID;

		// Token: 0x04004054 RID: 16468
		private int m_preSubSkillLevel;

		// Token: 0x04004055 RID: 16469
		private string Success = "Success";

		// Token: 0x04004056 RID: 16470
		private string Fail = "Fail";

		// Token: 0x04004057 RID: 16471
		private string Implant = "Implant";

		// Token: 0x04004058 RID: 16472
		private string SuccessOutro = "SuccessOutro";

		// Token: 0x04004059 RID: 16473
		private string FailOutro = "FailOutro";

		// Token: 0x0400405A RID: 16474
		private string ImplantOutro = "ImplantOutro";

		// Token: 0x0400405B RID: 16475
		private bool m_bClickBlock;

		// Token: 0x0400405C RID: 16476
		private bool m_bPlayOutro;

		// Token: 0x0400405D RID: 16477
		[Header("애니메이션 딜레이 갭")]
		public float m_fDelayGap;

		// Token: 0x0400405E RID: 16478
		private NKCOperatorInfoPopupSkillResult.RESULT_EVENT_TYPE m_lastResultEventType;

		// Token: 0x0400405F RID: 16479
		[Header("보이스 재생 딜레이시간")]
		public float m_fVoiceDelayTime = 0.3f;

		// Token: 0x020014A5 RID: 5285
		private enum RESULT_EVENT_TYPE
		{
			// Token: 0x04009EB4 RID: 40628
			NONE,
			// Token: 0x04009EB5 RID: 40629
			MAIN_SKILL_UP_OK,
			// Token: 0x04009EB6 RID: 40630
			MAIN_SKILL_UP_FAIL,
			// Token: 0x04009EB7 RID: 40631
			SUB_SKILL_UP_OK,
			// Token: 0x04009EB8 RID: 40632
			SUB_SKILL_UP_FAIL,
			// Token: 0x04009EB9 RID: 40633
			SUB_SKILL_TRANSFER_OK,
			// Token: 0x04009EBA RID: 40634
			SUB_SKILL_TRANSFER_FAIL
		}
	}
}
