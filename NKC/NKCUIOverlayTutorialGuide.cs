using System;
using NKC.UI.HUD;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200091B RID: 2331
	public class NKCUIOverlayTutorialGuide : NKCUIBase
	{
		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x001CC52E File Offset: 0x001CA72E
		public static NKCUIOverlayTutorialGuide Instance
		{
			get
			{
				if (NKCUIOverlayTutorialGuide.m_Instance == null)
				{
					NKCUIOverlayTutorialGuide.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOverlayTutorialGuide>("ab_ui_nkm_ui_tutorial", "NKM_UI_TUTORIAL_GUIDE", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOverlayTutorialGuide.CleanupInstance)).GetInstance<NKCUIOverlayTutorialGuide>();
				}
				return NKCUIOverlayTutorialGuide.m_Instance;
			}
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x001CC568 File Offset: 0x001CA768
		private static void CleanupInstance()
		{
			NKCUIOverlayTutorialGuide.m_Instance = null;
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x001CC570 File Offset: 0x001CA770
		public static bool HasInstance
		{
			get
			{
				return NKCUIOverlayTutorialGuide.m_Instance != null;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06005D4F RID: 23887 RVA: 0x001CC57D File Offset: 0x001CA77D
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOverlayTutorialGuide.m_Instance != null && NKCUIOverlayTutorialGuide.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x001CC598 File Offset: 0x001CA798
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOverlayTutorialGuide.m_Instance != null && NKCUIOverlayTutorialGuide.m_Instance.IsOpen)
			{
				NKCUIOverlayTutorialGuide.m_Instance.Close();
			}
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x001CC5BD File Offset: 0x001CA7BD
		private void OnDestroy()
		{
			NKCUIOverlayTutorialGuide.m_Instance = null;
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06005D52 RID: 23890 RVA: 0x001CC5C5 File Offset: 0x001CA7C5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06005D53 RID: 23891 RVA: 0x001CC5C8 File Offset: 0x001CA7C8
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_TUTORIAL_GUIDE;
			}
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x001CC5CF File Offset: 0x001CA7CF
		public override void CloseInternal()
		{
			this.Cleanup();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x001CC5E3 File Offset: 0x001CA7E3
		public override void OnBackButton()
		{
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x001CC5E5 File Offset: 0x001CA7E5
		public override bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06005D57 RID: 23895 RVA: 0x001CC5E8 File Offset: 0x001CA7E8
		// (set) Token: 0x06005D58 RID: 23896 RVA: 0x001CC5F0 File Offset: 0x001CA7F0
		public bool IsShowingInvalidMap { get; set; }

		// Token: 0x06005D59 RID: 23897 RVA: 0x001CC5FC File Offset: 0x001CA7FC
		private void Cleanup()
		{
			NKCUIComStateButton sbtnClickTarget = this.m_sbtnClickTarget;
			if (sbtnClickTarget != null)
			{
				sbtnClickTarget.PointerClick.RemoveListener(new UnityAction(this.OnPressButton));
			}
			NKCUIComButton btnClickTarget = this.m_btnClickTarget;
			if (btnClickTarget != null)
			{
				btnClickTarget.PointerClick.RemoveListener(new UnityAction(this.OnPressButton));
			}
			this.m_sbtnClickTarget = null;
			this.m_btnClickTarget = null;
			if (this.m_UIScreen != null)
			{
				this.m_UIScreen.CleanUp();
			}
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x001CC674 File Offset: 0x001CA874
		public void Open(Renderer targetRenderer, string text, UnityAction onComplete, NKCUIComRectScreen.ScreenExpand expandFlag = NKCUIComRectScreen.ScreenExpand.None)
		{
			this.dOnComplete = onComplete;
			this.m_fOpenTime = 0f;
			this.m_fTargetOpenTime = 0f;
			NKCUtil.SetGameobjectActive(this.m_rtDragDeck, false);
			NKCUtil.SetGameobjectActive(this.m_rtDragShipSkill, false);
			NKCUtil.SetGameobjectActive(this.m_rtTouch, false);
			this.m_CurrentType = NKCUIOverlayTutorialGuide.ClickGuideType.None;
			if (string.IsNullOrEmpty(text))
			{
				NKCUtil.SetGameobjectActive(this.m_objMessageRoot, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objMessageRoot, true);
				NKCUtil.SetLabelText(this.m_lbMessage, text);
			}
			this.m_UIScreen.SetScreen(targetRenderer, true, expandFlag);
			this.IsShowingInvalidMap = false;
			base.UIOpened(true);
			Canvas component = NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIOverlay).GetComponent<Canvas>();
			if (component != null)
			{
				component.overrideSorting = !component.overrideSorting;
				component.overrideSorting = false;
			}
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x001CC740 File Offset: 0x001CA940
		public void Open(RectTransform rtClickableArea, NKCUIOverlayTutorialGuide.ClickGuideType type, string text, UnityAction onComplete, bool bIsFromMidCanvas = false, NKCUIComRectScreen.ScreenExpand expandFlag = NKCUIComRectScreen.ScreenExpand.None, float fOpenTime = 0f)
		{
			this.dOnComplete = onComplete;
			this.m_fOpenTime = 0f;
			this.m_fTargetOpenTime = fOpenTime;
			NKCUtil.SetGameobjectActive(this.m_rtDragDeck, type == NKCUIOverlayTutorialGuide.ClickGuideType.DeckDrag);
			NKCUtil.SetGameobjectActive(this.m_rtDragShipSkill, type == NKCUIOverlayTutorialGuide.ClickGuideType.ShipSkill);
			NKCUtil.SetGameobjectActive(this.m_rtTouch, type == NKCUIOverlayTutorialGuide.ClickGuideType.Touch);
			RectTransform rectTransform = null;
			RectTransform target = null;
			switch (type)
			{
			case NKCUIOverlayTutorialGuide.ClickGuideType.None:
				rectTransform = null;
				target = rtClickableArea;
				break;
			case NKCUIOverlayTutorialGuide.ClickGuideType.DeckDrag:
			{
				NKCGameHud gameHud = NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
				gameHud.dOnUseDeck = (NKCGameHud.OnUseDeck)Delegate.Combine(gameHud.dOnUseDeck, new NKCGameHud.OnUseDeck(this.OnHudUseDeck));
				target = this.m_rtDragDeckHighlightArea;
				rectTransform = this.m_rtDragDeck;
				break;
			}
			case NKCUIOverlayTutorialGuide.ClickGuideType.ShipSkill:
			{
				NKCGameHud gameHud2 = NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
				gameHud2.dOnUseSkill = (NKCGameHud.OnUseSkill)Delegate.Combine(gameHud2.dOnUseSkill, new NKCGameHud.OnUseSkill(this.OnHudUseSkill));
				rectTransform = this.m_rtDragShipSkill;
				target = this.m_rtDragShipSkillHightlightArea;
				break;
			}
			case NKCUIOverlayTutorialGuide.ClickGuideType.Touch:
				if (rtClickableArea == null)
				{
					Debug.LogError("Target rect not exist!");
					this.OnPressButton();
					return;
				}
				this.m_btnClickTarget = rtClickableArea.GetComponent<NKCUIComButton>();
				if (this.m_btnClickTarget != null)
				{
					this.m_btnClickTarget.PointerClick.AddListener(new UnityAction(this.OnPressButton));
				}
				this.m_sbtnClickTarget = rtClickableArea.GetComponent<NKCUIComStateButton>();
				if (this.m_sbtnClickTarget != null)
				{
					this.m_sbtnClickTarget.PointerClick.AddListener(new UnityAction(this.OnPressButton));
				}
				rectTransform = this.m_rtTouch;
				target = rtClickableArea;
				break;
			}
			this.m_CurrentType = type;
			if (rectTransform != null && rtClickableArea != null)
			{
				rectTransform.pivot = rtClickableArea.pivot;
				rectTransform.position = rtClickableArea.position;
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				NKCUtil.SetGameobjectActive(this.m_objMessageRoot, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objMessageRoot, true);
				NKCUtil.SetLabelText(this.m_lbMessage, text);
			}
			NKCUtil.SetGameobjectActive(this.m_UIScreen, true);
			this.m_UIScreen.SetScreen(target, bIsFromMidCanvas, true);
			if (type == NKCUIOverlayTutorialGuide.ClickGuideType.None)
			{
				this.m_UIScreen.SetTouchSteal(null);
			}
			this.IsShowingInvalidMap = false;
			base.UIOpened(true);
			Canvas component = NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIOverlay).GetComponent<Canvas>();
			if (component != null)
			{
				component.overrideSorting = !component.overrideSorting;
				component.overrideSorting = false;
			}
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x001CC991 File Offset: 0x001CAB91
		private void SetGuidePositionByScreen(RectTransform guideRect)
		{
			if (guideRect == null)
			{
				return;
			}
			guideRect.pivot = this.m_UIScreen.m_rtCenter.pivot;
			guideRect.position = this.m_UIScreen.m_rtCenter.position;
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x001CC9C9 File Offset: 0x001CABC9
		public void SetStealInput(UnityAction<BaseEventData> onInput)
		{
			this.m_UIScreen.SetTouchSteal(onInput);
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x001CC9D7 File Offset: 0x001CABD7
		public void SetScreenActive(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_UIScreen, value);
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x001CC9E8 File Offset: 0x001CABE8
		private void Update()
		{
			if (this.m_fTargetOpenTime > 0f)
			{
				this.m_fOpenTime += Time.deltaTime;
				if (this.m_fOpenTime >= this.m_fTargetOpenTime)
				{
					UnityAction unityAction = this.dOnComplete;
					if (unityAction == null)
					{
						return;
					}
					unityAction();
				}
				return;
			}
			switch (this.m_CurrentType)
			{
			case NKCUIOverlayTutorialGuide.ClickGuideType.None:
				if (Input.anyKeyDown)
				{
					UnityAction unityAction2 = this.dOnComplete;
					if (unityAction2 == null)
					{
						return;
					}
					unityAction2();
					return;
				}
				break;
			case NKCUIOverlayTutorialGuide.ClickGuideType.DeckDrag:
			case NKCUIOverlayTutorialGuide.ClickGuideType.ShipSkill:
				break;
			case NKCUIOverlayTutorialGuide.ClickGuideType.Touch:
				this.SetGuidePositionByScreen(this.m_rtTouch);
				break;
			default:
				return;
			}
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x001CCA75 File Offset: 0x001CAC75
		private void OnPressButton()
		{
			this.Cleanup();
			UnityAction unityAction = this.dOnComplete;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x001CCA90 File Offset: 0x001CAC90
		private void OnHudUseDeck(int deckIndex)
		{
			NKCGameHud gameHud = NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
			gameHud.dOnUseDeck = (NKCGameHud.OnUseDeck)Delegate.Remove(gameHud.dOnUseDeck, new NKCGameHud.OnUseDeck(this.OnHudUseDeck));
			UnityAction unityAction = this.dOnComplete;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x001CCAE0 File Offset: 0x001CACE0
		private void OnHudUseSkill(int deckIndex)
		{
			NKCGameHud gameHud = NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
			gameHud.dOnUseSkill = (NKCGameHud.OnUseSkill)Delegate.Remove(gameHud.dOnUseSkill, new NKCGameHud.OnUseSkill(this.OnHudUseSkill));
			UnityAction unityAction = this.dOnComplete;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x001CCB2D File Offset: 0x001CAD2D
		public void SetBGScreenAlpha(float alpha)
		{
			this.m_UIScreen.SetAlpha(alpha);
		}

		// Token: 0x04004964 RID: 18788
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_tutorial";

		// Token: 0x04004965 RID: 18789
		private const string UI_ASSET_NAME = "NKM_UI_TUTORIAL_GUIDE";

		// Token: 0x04004966 RID: 18790
		private static NKCUIOverlayTutorialGuide m_Instance;

		// Token: 0x04004967 RID: 18791
		public GameObject m_objMessageRoot;

		// Token: 0x04004968 RID: 18792
		public Text m_lbMessage;

		// Token: 0x04004969 RID: 18793
		public RectTransform m_rtDragDeck;

		// Token: 0x0400496A RID: 18794
		public RectTransform m_rtDragDeckHighlightArea;

		// Token: 0x0400496B RID: 18795
		public RectTransform m_rtDragShipSkill;

		// Token: 0x0400496C RID: 18796
		public RectTransform m_rtDragShipSkillHightlightArea;

		// Token: 0x0400496D RID: 18797
		public RectTransform m_rtTouch;

		// Token: 0x0400496E RID: 18798
		public NKCUIComRectScreen m_UIScreen;

		// Token: 0x04004970 RID: 18800
		private UnityAction dOnComplete;

		// Token: 0x04004971 RID: 18801
		private NKCUIOverlayTutorialGuide.ClickGuideType m_CurrentType;

		// Token: 0x04004972 RID: 18802
		private float m_fOpenTime;

		// Token: 0x04004973 RID: 18803
		private float m_fTargetOpenTime;

		// Token: 0x04004974 RID: 18804
		private NKCUIComButton m_btnClickTarget;

		// Token: 0x04004975 RID: 18805
		private NKCUIComStateButton m_sbtnClickTarget;

		// Token: 0x020015AA RID: 5546
		public enum ClickGuideType
		{
			// Token: 0x0400A248 RID: 41544
			None,
			// Token: 0x0400A249 RID: 41545
			DeckDrag,
			// Token: 0x0400A24A RID: 41546
			ShipSkill,
			// Token: 0x0400A24B RID: 41547
			Touch
		}
	}
}
