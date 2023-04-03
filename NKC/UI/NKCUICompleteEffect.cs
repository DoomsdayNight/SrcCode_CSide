using System;
using DG.Tweening;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000979 RID: 2425
	public class NKCUICompleteEffect : NKCUIBase
	{
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06006286 RID: 25222 RVA: 0x001EF0FC File Offset: 0x001ED2FC
		public static NKCUICompleteEffect Instance
		{
			get
			{
				if (NKCUICompleteEffect.m_Instance == null)
				{
					NKCUICompleteEffect.m_Instance = NKCUIManager.OpenNewInstance<NKCUICompleteEffect>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_COMPLETE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUICompleteEffect.CleanupInstance)).GetInstance<NKCUICompleteEffect>();
					NKCUICompleteEffect.m_Instance.Init();
				}
				return NKCUICompleteEffect.m_Instance;
			}
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x001EF14B File Offset: 0x001ED34B
		private static void CleanupInstance()
		{
			NKCUICompleteEffect.m_Instance = null;
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x001EF153 File Offset: 0x001ED353
		public override string MenuName
		{
			get
			{
				return "완료";
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x001EF15A File Offset: 0x001ED35A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x001EF15D File Offset: 0x001ED35D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			if (NKCGameEventManager.IsWaiting())
			{
				NKCGameEventManager.WaitFinished();
			}
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x001EF177 File Offset: 0x001ED377
		public override void OnBackButton()
		{
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x001EF179 File Offset: 0x001ED379
		public void Init()
		{
			base.transform.position = Vector3.zero;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x001EF198 File Offset: 0x001ED398
		public void OpenCityOpened(float time = 4f)
		{
			this.DefaultSetup(time);
			NKCUtil.SetGameobjectActive(this.m_objMarkCityOpen, true);
			this.m_lbText.text = NKCUtilString.GET_STRING_WORLDMAP_CITY_MAKE_COMPLETE;
			base.gameObject.SetActive(true);
			base.UIOpened(true);
			this.PlayOpenSound();
			this.PlayOpenAni();
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x001EF1E8 File Offset: 0x001ED3E8
		public void OpenSkillUpgrade(NKMUnitSkillTemplet oldSkillTemplet, NKMUnitSkillTemplet newSkillTemplet, float time = 3f)
		{
			this.DefaultSetup(time);
			NKCUtil.SetGameobjectActive(this.m_objMarkSkillUpgrade, true);
			this.m_lbText.text = NKCUtilString.GET_STRING_SKILL_TRAINING_COMPLETE;
			bool bIsHyper = false;
			if (oldSkillTemplet != null)
			{
				bIsHyper = (oldSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER);
			}
			this.m_slotSkillBefore.SetData(oldSkillTemplet, bIsHyper);
			bIsHyper = false;
			if (newSkillTemplet != null)
			{
				bIsHyper = (newSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER);
			}
			this.m_slotSkillAfter.SetData(newSkillTemplet, bIsHyper);
			base.gameObject.SetActive(true);
			base.UIOpened(true);
			this.PlayOpenSound();
			this.PlayOpenAni();
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x001EF26F File Offset: 0x001ED46F
		private void DefaultSetup(float openTime)
		{
			this.m_fOpenTime = openTime;
			this.m_fcurrentTime = 0f;
			NKCUtil.SetGameobjectActive(this.m_objMarkSkillUpgrade, false);
			NKCUtil.SetGameobjectActive(this.m_objMarkCityOpen, false);
			NKCUtil.SetGameobjectActive(this.m_objSkillSlotRoot, false);
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x001EF2A7 File Offset: 0x001ED4A7
		private void Update()
		{
			this.m_fcurrentTime += Time.deltaTime;
			if (this.m_fcurrentTime > this.m_fOpenTime)
			{
				this.m_fcurrentTime = 0f;
				base.Close();
			}
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x001EF2DA File Offset: 0x001ED4DA
		private void PlayOpenSound()
		{
			NKCSoundManager.PlaySound("FX_UI_TITLE_START", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x001EF304 File Offset: 0x001ED504
		private void PlayOpenAni()
		{
			DOTweenAnimation[] componentsInChildren = base.GetComponentsInChildren<DOTweenAnimation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].DORestart();
			}
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x001EF330 File Offset: 0x001ED530
		private void DoKill()
		{
			DOTweenAnimation[] componentsInChildren = base.GetComponentsInChildren<DOTweenAnimation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].DOKill();
			}
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x001EF35A File Offset: 0x001ED55A
		private void OnDestroy()
		{
			this.DoKill();
			NKCUICompleteEffect.m_Instance = null;
		}

		// Token: 0x04004E5E RID: 20062
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04004E5F RID: 20063
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_COMPLETE";

		// Token: 0x04004E60 RID: 20064
		private static NKCUICompleteEffect m_Instance;

		// Token: 0x04004E61 RID: 20065
		public GameObject m_objMarkSkillUpgrade;

		// Token: 0x04004E62 RID: 20066
		public GameObject m_objMarkCityOpen;

		// Token: 0x04004E63 RID: 20067
		public Text m_lbText;

		// Token: 0x04004E64 RID: 20068
		public GameObject m_objSkillSlotRoot;

		// Token: 0x04004E65 RID: 20069
		public NKCUISkillSlot m_slotSkillBefore;

		// Token: 0x04004E66 RID: 20070
		public NKCUISkillSlot m_slotSkillAfter;

		// Token: 0x04004E67 RID: 20071
		private float m_fOpenTime = 2f;

		// Token: 0x04004E68 RID: 20072
		private float m_fcurrentTime;
	}
}
