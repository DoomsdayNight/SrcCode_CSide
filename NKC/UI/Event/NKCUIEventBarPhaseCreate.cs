using System;
using System.Collections;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD4 RID: 3028
	public class NKCUIEventBarPhaseCreate : MonoBehaviour
	{
		// Token: 0x06008C5C RID: 35932 RVA: 0x002FBE74 File Offset: 0x002FA074
		public void Init()
		{
			NKCUIEventBarCreateMenu eventBarCreateMenu = this.m_eventBarCreateMenu;
			if (eventBarCreateMenu != null)
			{
				eventBarCreateMenu.Init();
			}
			if (this.m_csbtnCockTailResult != null)
			{
				this.m_csbtnCockTailResult.PointerDown.RemoveAllListeners();
				this.m_csbtnCockTailResult.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPressCocktailResult));
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnScriptPanel, new UnityAction(this.OnClickScriptPanel));
		}

		// Token: 0x06008C5D RID: 35933 RVA: 0x002FBEE4 File Offset: 0x002FA0E4
		public void SetData(int eventID)
		{
			this.m_iEventID = eventID;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_bartenderID);
			if (this.m_NKCASUISpineIllust == null)
			{
				this.m_NKCASUISpineIllust = NKCResourceUtility.OpenSpineIllust(unitTempletBase, false);
				NKCASUIUnitIllust nkcasuispineIllust = this.m_NKCASUISpineIllust;
				if (nkcasuispineIllust != null)
				{
					nkcasuispineIllust.SetParent(this.m_spineRoot, false);
				}
			}
			NKCASUIUnitIllust nkcasuispineIllust2 = this.m_NKCASUISpineIllust;
			if (nkcasuispineIllust2 != null)
			{
				nkcasuispineIllust2.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, false, 0, true, 0f, true);
			}
			NKCUtil.SetLabelText(this.m_lbUnitName, (unitTempletBase != null) ? unitTempletBase.GetUnitName() : "");
			this.m_eventBarCreateMenu.Open(eventID, this.m_bartenderID, new NKCUIEventBarCreateMenu.OnSelectMenu(this.OnSelectMenu), new NKCUIEventBarCreateMenu.OnChangeUnitAnimation(this.OnChangeAnimation), new NKCUIEventBarCreateMenu.OnCreate(this.OnCreateCocktail), new NKCUIEventBarCreateMenu.OnCreateRefuse(this.OnCreateRefuse));
			NKCUtil.SetGameobjectActive(this.m_objCocktailFx, false);
			NKCUtil.SetGameobjectActive(this.m_objCreateFx, false);
			NKCUtil.SetImageSprite(this.m_imgCocktailResult, this.m_spriteCocktailNone, false);
			this.m_iResultCocktailID = 0;
			if (this.m_cocktailFxCoroutine != null)
			{
				base.StopCoroutine(this.m_cocktailFxCoroutine);
				this.m_cocktailFxCoroutine = null;
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
			this.m_showScript = true;
		}

		// Token: 0x06008C5E RID: 35934 RVA: 0x002FC020 File Offset: 0x002FA220
		public void Close()
		{
			if (this.m_cocktailFxCoroutine != null)
			{
				base.StopCoroutine(this.m_cocktailFxCoroutine);
				this.m_cocktailFxCoroutine = null;
			}
			this.m_eventBarCreateMenu.Close();
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
				this.m_NKCASUISpineIllust = null;
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
		}

		// Token: 0x06008C5F RID: 35935 RVA: 0x002FC094 File Offset: 0x002FA294
		public void Refresh()
		{
			NKCUIEventBarCreateMenu.Step currentStep = this.m_eventBarCreateMenu.CurrentStep;
			if (currentStep == NKCUIEventBarCreateMenu.Step.Step1)
			{
				this.SetData(this.m_iEventID);
				return;
			}
			if (currentStep != NKCUIEventBarCreateMenu.Step.Step2)
			{
				return;
			}
			this.m_eventBarCreateMenu.Refresh();
		}

		// Token: 0x06008C60 RID: 35936 RVA: 0x002FC0CD File Offset: 0x002FA2CD
		public void ActivateCreateFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objCreateFx, false);
			NKCUtil.SetGameobjectActive(this.m_objCreateFx, true);
			NKCComSoundPlayer createFxSoundPlayer = this.m_CreateFxSoundPlayer;
			if (createFxSoundPlayer == null)
			{
				return;
			}
			createFxSoundPlayer.Play();
		}

		// Token: 0x06008C61 RID: 35937 RVA: 0x002FC0F8 File Offset: 0x002FA2F8
		private void Update()
		{
			if (this.m_showScript && base.gameObject.activeSelf)
			{
				this.ShowScript(NKCUtilString.GET_STRING_GREMORY_BARTENDER_HELLO);
				this.m_fScriptTimer = this.m_showScriptTime;
				if (this.m_scriptCoroutine == null)
				{
					this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
				}
				NKCUIComRandomVoicePlayer introVoice = this.m_introVoice;
				if (introVoice != null)
				{
					introVoice.PlayRandomVoice();
				}
				this.m_showScript = false;
			}
			this.m_typeWriter.Update();
		}

		// Token: 0x06008C62 RID: 35938 RVA: 0x002FC16F File Offset: 0x002FA36F
		private void ShowScript(string message)
		{
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, true);
			this.m_typeWriter.Start(this.m_lbScriptMsg, message, 0f, false);
		}

		// Token: 0x06008C63 RID: 35939 RVA: 0x002FC195 File Offset: 0x002FA395
		private void HideScript()
		{
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, false);
			NKCUtil.SetLabelText(this.m_lbScriptMsg, "");
		}

		// Token: 0x06008C64 RID: 35940 RVA: 0x002FC1B3 File Offset: 0x002FA3B3
		private IEnumerator IOnShowRequestScript()
		{
			while (this.m_fScriptTimer > 0f)
			{
				this.m_fScriptTimer -= Time.deltaTime;
				yield return null;
			}
			yield return new WaitWhile(() => this.m_typeWriter.IsTyping());
			Animator aniScript = this.m_aniScript;
			if (aniScript != null)
			{
				aniScript.SetTrigger("OUTRO");
			}
			yield return new WaitWhile(() => this.m_scriptCanvasGroup.alpha > 0f);
			this.HideScript();
			this.m_scriptCoroutine = null;
			yield break;
		}

		// Token: 0x06008C65 RID: 35941 RVA: 0x002FC1C2 File Offset: 0x002FA3C2
		private void OnCreateCocktail()
		{
			NKCUIComRandomVoicePlayer createVoice = this.m_createVoice;
			if (createVoice == null)
			{
				return;
			}
			createVoice.PlayRandomVoice();
		}

		// Token: 0x06008C66 RID: 35942 RVA: 0x002FC1D5 File Offset: 0x002FA3D5
		private void OnCreateRefuse()
		{
			this.ShowScript(NKCUtilString.GET_STRING_GREMORY_BARTENDER_REJECT);
			this.m_fScriptTimer = this.m_showScriptTime;
			if (this.m_scriptCoroutine == null)
			{
				this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
			}
		}

		// Token: 0x06008C67 RID: 35943 RVA: 0x002FC208 File Offset: 0x002FA408
		private void OnSelectMenu(int cocktailID)
		{
			if (this.m_imgCocktailResult == null || this.m_iResultCocktailID == cocktailID)
			{
				return;
			}
			if (cocktailID == 0)
			{
				if (this.m_cocktailFxCoroutine != null)
				{
					base.StopCoroutine(this.m_cocktailFxCoroutine);
					this.m_cocktailFxCoroutine = null;
				}
				NKCUtil.SetGameobjectActive(this.m_objCocktailFx, false);
				this.m_cocktailFxCoroutine = base.StartCoroutine(this.IChangeCocktailImage(this.m_spriteCocktailNone));
			}
			else if (cocktailID > 0)
			{
				Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(cocktailID);
				if (this.m_cocktailFxCoroutine != null)
				{
					base.StopCoroutine(this.m_cocktailFxCoroutine);
					this.m_cocktailFxCoroutine = null;
				}
				NKCUtil.SetGameobjectActive(this.m_objCocktailFx, false);
				this.m_cocktailFxCoroutine = base.StartCoroutine(this.IChangeCocktailImage(orLoadMiscItemIcon));
				if (cocktailID == NKCEventBarManager.DailyCocktailItemID)
				{
					NKCUIComRandomVoicePlayer todayCocktailVoice = this.m_todayCocktailVoice;
					if (todayCocktailVoice != null)
					{
						todayCocktailVoice.PlayRandomVoice();
					}
				}
			}
			this.m_iResultCocktailID = cocktailID;
		}

		// Token: 0x06008C68 RID: 35944 RVA: 0x002FC2D7 File Offset: 0x002FA4D7
		private void OnChangeAnimation(NKCASUIUnitIllust.eAnimation animation)
		{
			NKCASUIUnitIllust nkcasuispineIllust = this.m_NKCASUISpineIllust;
			if (nkcasuispineIllust == null)
			{
				return;
			}
			nkcasuispineIllust.SetAnimation(animation, false, 0, true, 0f, true);
		}

		// Token: 0x06008C69 RID: 35945 RVA: 0x002FC2F3 File Offset: 0x002FA4F3
		private IEnumerator IChangeCocktailImage(Sprite cocktailImage)
		{
			NKCUtil.SetGameobjectActive(this.m_objCocktailFx, true);
			while (this.m_aniCocktailFx.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
			{
				yield return null;
			}
			NKCUtil.SetImageSprite(this.m_imgCocktailResult, cocktailImage, false);
			while (this.m_aniCocktailFx.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			NKCUtil.SetGameobjectActive(this.m_objCocktailFx, false);
			yield break;
		}

		// Token: 0x06008C6A RID: 35946 RVA: 0x002FC30C File Offset: 0x002FA50C
		private void OnPressCocktailResult(PointerEventData eventData)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_iResultCocktailID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long count = 0L;
			if (nkmuserData != null)
			{
				count = nkmuserData.m_InventoryData.GetCountMiscItem(itemMiscTempletByID.m_ItemMiscID);
			}
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(this.m_iResultCocktailID, count, 0);
			NKCUITooltip.Instance.Open(slotData, new Vector2?(eventData.position));
		}

		// Token: 0x06008C6B RID: 35947 RVA: 0x002FC36B File Offset: 0x002FA56B
		private void OnClickScriptPanel()
		{
			if (this.m_typeWriter.IsTyping())
			{
				this.m_typeWriter.Finish();
				return;
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
		}

		// Token: 0x06008C6C RID: 35948 RVA: 0x002FC3A8 File Offset: 0x002FA5A8
		private void OnDestroy()
		{
			if (this.m_cocktailFxCoroutine != null)
			{
				base.StopCoroutine(this.m_cocktailFxCoroutine);
				this.m_cocktailFxCoroutine = null;
			}
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
				this.m_NKCASUISpineIllust = null;
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.m_typeWriter = null;
		}

		// Token: 0x04007933 RID: 31027
		public NKCUIEventBarCreateMenu m_eventBarCreateMenu;

		// Token: 0x04007934 RID: 31028
		public Sprite m_spriteCocktailNone;

		// Token: 0x04007935 RID: 31029
		public Image m_imgCocktailResult;

		// Token: 0x04007936 RID: 31030
		public NKCUIComStateButton m_csbtnCockTailResult;

		// Token: 0x04007937 RID: 31031
		[Header("이펙트")]
		public GameObject m_objCocktailFx;

		// Token: 0x04007938 RID: 31032
		public Animator m_aniCocktailFx;

		// Token: 0x04007939 RID: 31033
		public GameObject m_objCreateFx;

		// Token: 0x0400793A RID: 31034
		public NKCComSoundPlayer m_CreateFxSoundPlayer;

		// Token: 0x0400793B RID: 31035
		[Header("캐릭터")]
		public RectTransform m_spineRoot;

		// Token: 0x0400793C RID: 31036
		public int m_bartenderID;

		// Token: 0x0400793D RID: 31037
		[Header("인트로 음성")]
		public NKCUIComRandomVoicePlayer m_introVoice;

		// Token: 0x0400793E RID: 31038
		[Header("오늘의 칵테일 음성")]
		public NKCUIComRandomVoicePlayer m_todayCocktailVoice;

		// Token: 0x0400793F RID: 31039
		[Header("칵테일 제조 음성")]
		public NKCUIComRandomVoicePlayer m_createVoice;

		// Token: 0x04007940 RID: 31040
		[Header("대사 창")]
		public Animator m_aniScript;

		// Token: 0x04007941 RID: 31041
		public CanvasGroup m_scriptCanvasGroup;

		// Token: 0x04007942 RID: 31042
		public GameObject m_objScriptRoot;

		// Token: 0x04007943 RID: 31043
		public Text m_lbUnitName;

		// Token: 0x04007944 RID: 31044
		public Text m_lbScriptMsg;

		// Token: 0x04007945 RID: 31045
		public float m_showScriptTime;

		// Token: 0x04007946 RID: 31046
		public NKCUIComStateButton m_csbtnScriptPanel;

		// Token: 0x04007947 RID: 31047
		private NKCASUIUnitIllust m_NKCASUISpineIllust;

		// Token: 0x04007948 RID: 31048
		private NKCUITypeWriter m_typeWriter = new NKCUITypeWriter();

		// Token: 0x04007949 RID: 31049
		private Coroutine m_cocktailFxCoroutine;

		// Token: 0x0400794A RID: 31050
		private int m_iEventID;

		// Token: 0x0400794B RID: 31051
		private int m_iResultCocktailID;

		// Token: 0x0400794C RID: 31052
		private float m_fScriptTimer;

		// Token: 0x0400794D RID: 31053
		private Coroutine m_scriptCoroutine;

		// Token: 0x0400794E RID: 31054
		private bool m_showScript;
	}
}
