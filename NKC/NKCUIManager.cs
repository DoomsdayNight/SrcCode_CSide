using System;
using System.Collections.Generic;
using ClientPacket.Office;
using Cs.Logging;
using DG.Tweening;
using NKC.Loading;
using NKC.UI.Gauntlet;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B9 RID: 2489
	public class NKCUIManager
	{
		// Token: 0x06006927 RID: 26919 RVA: 0x0021FE9C File Offset: 0x0021E09C
		public static void RegisterInventoryObserver(NKCUIManager.IInventoryChangeObserver observer)
		{
			observer.Handle = NKCUIManager.m_sHandle;
			NKCUIManager.m_dicInventoryObserver.Add(NKCUIManager.m_sHandle, observer);
			NKCUIManager.m_sHandle++;
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x0021FEC5 File Offset: 0x0021E0C5
		public static void UnregisterInventoryObserver(NKCUIManager.IInventoryChangeObserver observer)
		{
			NKCUIManager.m_dicInventoryObserver.Remove(observer.Handle);
			if (NKCUIManager.m_dicInventoryObserver.Count == 0)
			{
				NKCUIManager.m_sHandle = 0;
			}
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x0021FEEA File Offset: 0x0021E0EA
		public static void SetUseFrontLowCanvas(bool bUse)
		{
			NKCUtil.SetGameobjectActive(NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas, bUse);
			if (NKCCamera.GetSubUILowCamera() != null)
			{
				NKCCamera.GetSubUILowCamera().enabled = bUse;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x0600692A RID: 26922 RVA: 0x0021FF0F File Offset: 0x0021E10F
		// (set) Token: 0x0600692B RID: 26923 RVA: 0x0021FF16 File Offset: 0x0021E116
		public static CanvasGroup FrontCanvasGroup { get; private set; }

		// Token: 0x0600692C RID: 26924 RVA: 0x0021FF1E File Offset: 0x0021E11E
		public static CanvasScaler GetUIFrontCanvasScaler()
		{
			return NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler;
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x0600692D RID: 26925 RVA: 0x0021FF25 File Offset: 0x0021E125
		public static RectTransform UIFrontCanvasSafeRectTransform
		{
			get
			{
				return NKCUIManager.m_NKM_SCEN_UI_FRONT_RectTransform;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x0600692E RID: 26926 RVA: 0x0021FF2C File Offset: 0x0021E12C
		public static RectTransform UIFrontLowCanvasSafeRectTransform
		{
			get
			{
				return NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_RectTransform;
			}
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x0021FF33 File Offset: 0x0021E133
		public static RectTransform Get_NUF_DRAG()
		{
			return NKCUIManager.m_NUF_DRAG;
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06006930 RID: 26928 RVA: 0x0021FF3A File Offset: 0x0021E13A
		// (set) Token: 0x06006931 RID: 26929 RVA: 0x0021FF41 File Offset: 0x0021E141
		public static NKCUIUpsideMenu NKCUIUpsideMenu { get; private set; }

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06006932 RID: 26930 RVA: 0x0021FF49 File Offset: 0x0021E149
		// (set) Token: 0x06006933 RID: 26931 RVA: 0x0021FF50 File Offset: 0x0021E150
		public static NKCPopupMessage NKCPopupMessage { get; private set; }

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06006934 RID: 26932 RVA: 0x0021FF58 File Offset: 0x0021E158
		// (set) Token: 0x06006935 RID: 26933 RVA: 0x0021FF5F File Offset: 0x0021E15F
		public static NKCUIOverlayCaption NKCUIOverlayCaption { get; private set; }

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06006936 RID: 26934 RVA: 0x0021FF67 File Offset: 0x0021E167
		// (set) Token: 0x06006937 RID: 26935 RVA: 0x0021FF6E File Offset: 0x0021E16E
		public static NKCUIGauntletResult NKCUIGauntletResult { get; set; }

		// Token: 0x06006938 RID: 26936 RVA: 0x0021FF76 File Offset: 0x0021E176
		public static NKCUIPowerSaveMode GetNKCUIPowerSaveMode()
		{
			return NKCUIManager.m_NKCUIPowerSaveMode;
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06006939 RID: 26937 RVA: 0x0021FF7D File Offset: 0x0021E17D
		// (set) Token: 0x0600693A RID: 26938 RVA: 0x0021FF84 File Offset: 0x0021E184
		public static NKCUILoadingScreen LoadingUI { get; private set; }

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600693B RID: 26939 RVA: 0x0021FF8C File Offset: 0x0021E18C
		// (set) Token: 0x0600693C RID: 26940 RVA: 0x0021FF93 File Offset: 0x0021E193
		public static RectTransform rectMidCanvas { get; private set; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x0600693D RID: 26941 RVA: 0x0021FF9B File Offset: 0x0021E19B
		// (set) Token: 0x0600693E RID: 26942 RVA: 0x0021FFA2 File Offset: 0x0021E1A2
		public static RectTransform rectFrontCanvas { get; private set; }

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x0600693F RID: 26943 RVA: 0x0021FFAA File Offset: 0x0021E1AA
		// (set) Token: 0x06006940 RID: 26944 RVA: 0x0021FFB1 File Offset: 0x0021E1B1
		public static Canvas FrontCanvas { get; private set; }

		// Token: 0x06006941 RID: 26945 RVA: 0x0021FFBC File Offset: 0x0021E1BC
		public static RectTransform GetUIBaseRect(NKCUIManager.eUIBaseRect type)
		{
			switch (type)
			{
			case NKCUIManager.eUIBaseRect.UIFrontLow:
				return NKCUIManager.m_rtFrontLowRoot;
			case NKCUIManager.eUIBaseRect.UIFrontCommonLow:
				return NKCUIManager.m_rtCommonLowRoot;
			case NKCUIManager.eUIBaseRect.UIFrontCommon:
				return NKCUIManager.m_rtCommonRoot;
			case NKCUIManager.eUIBaseRect.UIFrontPopup:
				return NKCUIManager.m_rtPopupRoot;
			case NKCUIManager.eUIBaseRect.UIMidCanvas:
				return NKCUIManager.rectMidCanvas;
			case NKCUIManager.eUIBaseRect.UIFrontCanvas:
				return NKCUIManager.rectFrontCanvas;
			case NKCUIManager.eUIBaseRect.UIOverlay:
				return NKCUIManager.m_rtOverlayRoot;
			default:
				return null;
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x00220018 File Offset: 0x0021E218
		public static void SetScreenInputBlock(bool bSet)
		{
			NKCUtil.SetGameobjectActive(NKCUIManager.m_NUF_BLOCK_SCREEN_INPUT, bSet);
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x00220025 File Offset: 0x0021E225
		public static bool CheckScreenInputBlock()
		{
			return NKCUIManager.m_NUF_BLOCK_SCREEN_INPUT.activeSelf;
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x00220031 File Offset: 0x0021E231
		private NKCUIManager()
		{
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x0022003C File Offset: 0x0021E23C
		public static void Init()
		{
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				DOTween.Init(new bool?(true), new bool?(true), new LogBehaviour?(LogBehaviour.Verbose));
			}
			else
			{
				DOTween.Init(new bool?(true), new bool?(true), new LogBehaviour?(LogBehaviour.ErrorsOnly));
			}
			DOTween.useSafeMode = true;
			NKCUIManager.m_NKM_SCEN_UI = GameObject.Find("NKM_SCEN_UI");
			NKCUIManager.m_TR_NKM_WAIT_INSTANT = NKCUIManager.m_NKM_SCEN_UI.transform.Find("NKM_WAIT_INSTANT");
			NKCUIManager.m_NUF_GAME_TOUCH_OBJECT = NKCUIManager.m_NKM_SCEN_UI.transform.Find("NKM_SCEN_UI_FRONT_Canvas/NKM_SCEN_UI_FRONT/NUF_GAME_TOUCH_OBJECT").gameObject;
			NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas = GameObject.Find("NKM_SCEN_UI_BACK_Canvas");
			NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler = NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas.GetComponent<CanvasScaler>();
			NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler.enabled = true;
			NKCUIManager.m_NKM_SCEN_UI_MID_Canvas = GameObject.Find("NKM_SCEN_UI_MID_Canvas");
			NKCUIManager.m_NKM_SCEN_UI_MID_Canvas_CanvasScaler = NKCUIManager.m_NKM_SCEN_UI_MID_Canvas.GetComponent<CanvasScaler>();
			NKCUIManager.rectMidCanvas = NKCUIManager.m_NKM_SCEN_UI_MID_Canvas.GetComponent<RectTransform>();
			NKCUIManager.m_NKM_SCEN_UI_MID_Canvas_CanvasScaler.enabled = true;
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas = GameObject.Find("NKM_SCEN_UI_FRONT_LOW_Canvas");
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler = NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas.GetComponent<CanvasScaler>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler.enabled = true;
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW = NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas.transform.Find("NKM_SCEN_UI_FRONT_LOW").gameObject;
			NKCUIManager.m_rtFrontLowRoot = NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW.GetComponent<RectTransform>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Image = NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW.GetComponent<Image>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Image.enabled = false;
			NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_RectTransform = NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW.GetComponent<RectTransform>();
			NKCUIManager.m_NKCUILooseShaker = NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW.AddComponent<NKCUILooseShaker>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas = GameObject.Find("NKM_SCEN_UI_FRONT_Canvas");
			NKCUIManager.rectFrontCanvas = NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas.GetComponent<RectTransform>();
			NKCUIManager.FrontCanvas = NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas.GetComponent<Canvas>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler = NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas.GetComponent<CanvasScaler>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler.enabled = true;
			NKCUIManager.FrontCanvasGroup = NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas.GetComponent<CanvasGroup>();
			NKCUIManager.FrontCanvasGroup.alpha = 1f;
			NKCUIManager.m_NKM_SCEN_UI_FRONT = NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas.transform.Find("NKM_SCEN_UI_FRONT").gameObject;
			NKCUIManager.m_NKM_SCEN_UI_FRONT_Image = NKCUIManager.m_NKM_SCEN_UI_FRONT.GetComponent<Image>();
			NKCUIManager.m_NKM_SCEN_UI_FRONT_Image.enabled = false;
			NKCUIManager.m_NKM_SCEN_UI_FRONT_RectTransform = NKCUIManager.m_NKM_SCEN_UI_FRONT.GetComponent<RectTransform>();
			NKCUIManager.m_rectBackground = NKCUIManager.m_NKM_SCEN_UI_MID_Canvas.transform.Find("NUM_Background").GetComponent<RectTransform>();
			if (NKCUIManager.m_rectBackground != null)
			{
				NKCUIManager.m_imgBackground = NKCUIManager.m_rectBackground.GetComponent<Image>();
				EventTrigger component = NKCUIManager.m_rectBackground.GetComponent<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.Drag;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCUIManager.OnDragBackground(eventData);
				});
				component.triggers.Add(entry);
			}
			else
			{
				Debug.LogError("Background Object not found!");
			}
			GameObject gameObject = GameObject.Find("NUF_POPUP_Panel");
			if (gameObject != null)
			{
				NKCUIManager.m_rtPopupRoot = gameObject.GetComponent<RectTransform>();
			}
			else
			{
				Debug.LogError("UIManager : Popup Root not found!");
			}
			GameObject gameObject2 = GameObject.Find("NUF_OVERLAY_Panel");
			if (gameObject2 != null)
			{
				NKCUIManager.m_rtOverlayRoot = gameObject2.GetComponent<RectTransform>();
			}
			else
			{
				Debug.LogError("UIManager : Overlay Root not found!");
			}
			NKCUIManager.SetAspect();
			NKCUIManager.m_NUF_DRAG = GameObject.Find("NUF_DRAG").GetComponent<RectTransform>();
			NKCUIManager.m_rtCommonRoot = NKCUIManager.OpenUI("NUF_COMMON_Panel").GetComponent<RectTransform>();
			NKCUIManager.m_rtCommonLowRoot = NKCUIManager.OpenUI("NUF_COMMON_LOW_Panel").GetComponent<RectTransform>();
			NKCUIManager.NKCUIUpsideMenu = NKCUIManager.OpenUI<NKCUIUpsideMenu>("NKM_UI_UPSIDE_MENU");
			NKCUIManager.NKCUIUpsideMenu.InitUI();
			NKCUIManager.LoadingUI = NKCUIManager.OpenUI<NKCUILoadingScreen>("NUF_LOADING_Panel");
			NKCUIManager.LoadingUI.Init();
			NKCUtil.SetGameobjectActive(NKCUIManager.LoadingUI, false);
			NKCPopupOKCancel.InitUI();
			NKCUIManager.NKCPopupMessage = NKCUIManager.OpenUI<NKCPopupMessage>("NKM_UI_POPUP_MESSAGE");
			NKCUIManager.NKCPopupMessage.gameObject.SetActive(false);
			NKCUIManager.NKCUIOverlayCaption = NKCUIManager.OpenUI<NKCUIOverlayCaption>("NKM_UI_OVERLAY_CAPTION");
			NKCUIManager.NKCUIOverlayCaption.gameObject.SetActive(false);
			NKCUIManager.m_NKCUIPowerSaveMode = NKCUIManager.OpenUI<NKCUIPowerSaveMode>("NKM_UI_SLEEP_MODE");
			NKCUtil.SetGameobjectActive(NKCUIManager.m_NKCUIPowerSaveMode, false);
			NKCUIFadeInOut.InitUI();
			NKCCutScenManager.Init();
			NKCDescMgr.Init();
			NKCUIManager.m_NUF_BLOCK_SCREEN_INPUT = NKCUIManager.OpenUI("NUF_BLOCK_SCREEN_INPUT");
			NKCUtil.SetGameobjectActive(NKCUIManager.m_NUF_BLOCK_SCREEN_INPUT, false);
			NKCUIManager.SetUseFrontLowCanvas(false);
			NKCUIManager.m_ScreenWidth = Screen.width;
			NKCUIManager.m_ScreenHeight = Screen.height;
			Log.Info(string.Format("Screen:{0}, {1}, {2}, {3}", new object[]
			{
				Screen.currentResolution.width,
				Screen.currentResolution.height,
				Screen.width,
				Screen.height
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIManager.cs", 334);
			Log.Info(string.Format("Screen.safeArea:{0}, {1}, {2}, {3}", new object[]
			{
				Screen.safeArea.x,
				Screen.safeArea.y,
				Screen.safeArea.width,
				Screen.safeArea.height
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIManager.cs", 335);
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x0022054C File Offset: 0x0021E74C
		public static void SetAspect()
		{
			if (NKCCamera.GetCamera() == null)
			{
				return;
			}
			NKCCamera.GetCamera().ResetAspect();
			if (NKCCamera.GetCamera().aspect >= 1.777f)
			{
				if (NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler.matchWidthOrHeight = 1f;
				}
				if (NKCUIManager.m_NKM_SCEN_UI_MID_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_MID_Canvas_CanvasScaler.matchWidthOrHeight = 1f;
				}
				if (NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler.matchWidthOrHeight = 1f;
				}
				if (NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler.matchWidthOrHeight = 1f;
					return;
				}
			}
			else
			{
				if (NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler.matchWidthOrHeight = 0f;
				}
				if (NKCUIManager.m_NKM_SCEN_UI_MID_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_MID_Canvas_CanvasScaler.matchWidthOrHeight = 0f;
				}
				if (NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler.matchWidthOrHeight = 0f;
				}
				if (NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler != null)
				{
					NKCUIManager.m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler.matchWidthOrHeight = 0f;
				}
			}
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x00220668 File Offset: 0x0021E868
		private static Rect SetSafeArea_(RectTransform rt)
		{
			if (rt != null)
			{
				Vector2 vector = rt.localScale;
				vector.x = Screen.safeArea.width / (float)Screen.currentResolution.width;
				vector.y = Screen.safeArea.height / (float)Screen.currentResolution.height;
				if (vector.x > vector.y)
				{
					vector.x = vector.y;
				}
				else
				{
					vector.y = vector.x;
				}
				rt.localScale = vector;
				Rect rect = NKCUIManager.m_NKM_SCEN_UI_FRONT_RectTransform.rect;
				rect.x = (1f - vector.x) * (float)Screen.currentResolution.width * 0.5f / (float)Screen.currentResolution.width;
				rect.y = (1f - vector.y) * (float)Screen.currentResolution.height * 0.5f / (float)Screen.currentResolution.height;
				rect.width = vector.x;
				rect.height = vector.y;
				return rect;
			}
			return default(Rect);
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x002207A7 File Offset: 0x0021E9A7
		public static void SetSafeArea()
		{
			NKCCamera.SetCameraRect(NKCUIManager.SetSafeArea_(NKCUIManager.m_NKM_SCEN_UI_FRONT_RectTransform));
			NKCUIManager.SetSafeArea_(NKCUIManager.m_NKM_SCEN_UI_FRONT_LOW_RectTransform);
		}

		// Token: 0x06006949 RID: 26953 RVA: 0x002207C3 File Offset: 0x0021E9C3
		public static GameObject OpenUI(string uiName)
		{
			GameObject gameObject = GameObject.Find(uiName);
			NKCUIManager.OpenUI(gameObject);
			return gameObject;
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x002207D4 File Offset: 0x0021E9D4
		public static T OpenUI<T>(string uiName) where T : MonoBehaviour
		{
			GameObject gameObject = GameObject.Find(uiName);
			if (gameObject != null)
			{
				T component = gameObject.GetComponent<T>();
				NKCUIManager.OpenUI(gameObject);
				return component;
			}
			return default(T);
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x0022080C File Offset: 0x0021EA0C
		public static GameObject OpenUI(GameObject ui)
		{
			if (ui != null)
			{
				Transform component = ui.GetComponent<Transform>();
				if (component != null)
				{
					Vector3 position = component.position;
					position.Set(0f, 0f, 0f);
					component.position = position;
				}
				RectTransform component2 = ui.GetComponent<RectTransform>();
				if (component2 != null)
				{
					component2.anchoredPosition3D = new Vector3(0f, 0f, 0f);
					component2.anchoredPosition = Vector2.zero;
				}
				NKCUIComSafeArea component3 = ui.GetComponent<NKCUIComSafeArea>();
				if (component3 != null)
				{
					component3.SetSafeAreaUI();
				}
			}
			return ui;
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x002208A4 File Offset: 0x0021EAA4
		public static void Update(float deltaTime)
		{
			NKCUIFadeInOut.Update(deltaTime);
			NKMPopUpBox.Update();
			if (NKCUIManager.m_eUITransitionState == NKCUIManager.eUITransitionProcess.FinishedAndWaiting && NKCUIFadeInOut.IsFinshed())
			{
				NKCUIManager.OnTransitionComplete();
			}
			if (NKCUIManager.s_bUseCameraFunctions && NKCUIManager.s_currentFullScreenUIBase != null)
			{
				NKCUIManager.s_currentFullScreenUIBase.UpdateCamera();
			}
			if (NKCUIManager.m_ScreenWidth != Screen.width || NKCUIManager.m_ScreenHeight != Screen.height)
			{
				NKCUIManager.m_ScreenWidth = Screen.width;
				NKCUIManager.m_ScreenHeight = Screen.height;
				NKCUIManager.OnScreenResolutionChanged();
			}
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x00220920 File Offset: 0x0021EB20
		private static void SetBackground(NKCUIBase currentFullscreenUI)
		{
			Sprite backgroundSprite = currentFullscreenUI.GetBackgroundSprite();
			if (backgroundSprite != null)
			{
				NKCUIManager.s_bUseCameraFunctions = true;
				Vector3 backgroundDimension = currentFullscreenUI.GetBackgroundDimension();
				NKCUIManager.m_rectBackground.SetWidth(backgroundDimension.x);
				NKCUIManager.m_rectBackground.SetHeight(backgroundDimension.y);
				NKCUIManager.m_rectBackground.localPosition = new Vector3(0f, 0f, backgroundDimension.z);
				NKCUtil.SetImageSprite(NKCUIManager.m_imgBackground, backgroundSprite, true);
				NKCUtil.SetImageColor(NKCUIManager.m_imgBackground, currentFullscreenUI.GetBackgroundColor());
				return;
			}
			NKCUIManager.s_bUseCameraFunctions = false;
			NKCUtil.SetImageSprite(NKCUIManager.m_imgBackground, null, true);
		}

		// Token: 0x0600694E RID: 26958 RVA: 0x002209B8 File Offset: 0x0021EBB8
		private static void OnDragBackground(BaseEventData cBaseEventData)
		{
			if (NKCUIManager.s_currentFullScreenUIBase != null)
			{
				NKCUIManager.s_currentFullScreenUIBase.OnDragBackground(cBaseEventData);
			}
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x002209D2 File Offset: 0x0021EBD2
		public static void StartLooseShake()
		{
			if (NKCUIManager.m_NKCUILooseShaker != null)
			{
				NKCUIManager.m_NKCUILooseShaker.StartShake();
			}
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x002209EB File Offset: 0x0021EBEB
		public static void StopLooseShake()
		{
			if (NKCUIManager.m_NKCUILooseShaker != null)
			{
				NKCUIManager.m_NKCUILooseShaker.StopShake();
			}
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x00220A04 File Offset: 0x0021EC04
		public static void UIPrepare(NKCUIBase openedUI)
		{
			NKCUIManager.m_PreparingUI = openedUI;
			NKCUIManager.m_eUITransitionState = NKCUIManager.eUITransitionProcess.Preparing;
			switch (openedUI.eTransitionEffect)
			{
			case NKCUIBase.eTransitionEffectType.SmallLoading:
				NKMPopUpBox.OpenSmallWaitBox(0f, "");
				break;
			case NKCUIBase.eTransitionEffectType.FullScreenLoading:
				if (NKCScenManager.GetScenManager() != null)
				{
					NKCScenManager.GetScenManager().SetActiveLoadingUI(NKCLoadingScreenManager.eGameContentsType.DEFAULT, 0);
					return;
				}
				break;
			case NKCUIBase.eTransitionEffectType.FadeInOut:
				NKCUIFadeInOut.FadeOut(0.1f, null, false, 7f);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x00220A78 File Offset: 0x0021EC78
		public static void UIReady(NKCUIBase openedUI)
		{
			NKCUIManager.m_eUITransitionState = NKCUIManager.eUITransitionProcess.FinishedAndWaiting;
			NKCUIBase.eTransitionEffectType eTransitionEffect = openedUI.eTransitionEffect;
			if (eTransitionEffect == NKCUIBase.eTransitionEffectType.None || eTransitionEffect != NKCUIBase.eTransitionEffectType.FadeInOut)
			{
				NKCUIManager.OnTransitionComplete();
				return;
			}
			if (NKCUIFadeInOut.IsFinshed())
			{
				NKCUIManager.OnTransitionComplete();
			}
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x00220AAC File Offset: 0x0021ECAC
		private static void OnTransitionComplete()
		{
			switch (NKCUIManager.m_PreparingUI.eTransitionEffect)
			{
			case NKCUIBase.eTransitionEffectType.SmallLoading:
				NKMPopUpBox.CloseWaitBox();
				break;
			case NKCUIBase.eTransitionEffectType.FullScreenLoading:
				if (NKCScenManager.GetScenManager() != null)
				{
					NKCScenManager.GetScenManager().CloseLoadingUI();
				}
				break;
			case NKCUIBase.eTransitionEffectType.FadeInOut:
				NKCUIFadeInOut.FadeIn(0.1f, null, false);
				break;
			}
			NKCUIManager.m_eUITransitionState = NKCUIManager.eUITransitionProcess.Idle;
			NKCUIManager.UIOpened(NKCUIManager.m_PreparingUI);
			NKCUIManager.m_PreparingUI = null;
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x00220B1C File Offset: 0x0021ED1C
		public static void UIOpened(NKCUIBase openedUI)
		{
			if (NKCUIManager.m_eUITransitionState == NKCUIManager.eUITransitionProcess.Preparing)
			{
				Debug.LogWarning("Trying UIOpen while another is processing. ignoring open");
				return;
			}
			if (NKCUIManager.m_stkUI.Contains(openedUI))
			{
				if (openedUI.IsResetUpsideMenuWhenOpenAgain())
				{
					NKCUIManager.SetUpsideMenuState(openedUI, false);
				}
				Debug.LogError("[NKCUIManager]Undefined behaivor : 이미 열려서 UI 스택에 등록되어있는 UI를 다시 열려고 시도");
				return;
			}
			if (openedUI.IsFullScreenUI)
			{
				NKCUIManager.s_currentFullScreenUIBase = openedUI;
				NKCUIManager.SetBackground(openedUI);
				if (openedUI.WillCloseUnderPopupOnOpen)
				{
					while (NKCUIManager.m_stkUI.Count > 0)
					{
						if (NKCUIManager.m_stkUI.Peek().IsFullScreenUI)
						{
							NKCUIManager.m_stkUI.Peek().Hide();
							break;
						}
						NKCUIManager.m_stkUI.Pop()._ForceCloseInternal();
					}
				}
				else
				{
					foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
					{
						nkcuibase.Hide();
						if (nkcuibase.IsFullScreenUI)
						{
							break;
						}
					}
				}
			}
			switch (openedUI.eUIType)
			{
			case NKCUIBase.eMenutype.FullScreen:
				if (openedUI.transform.parent == NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon))
				{
					openedUI.transform.SetAsLastSibling();
				}
				break;
			case NKCUIBase.eMenutype.Popup:
				if (NKCUIManager.m_rtPopupRoot != null)
				{
					openedUI.transform.SetParent(NKCUIManager.m_rtPopupRoot, false);
					openedUI.transform.SetAsLastSibling();
				}
				break;
			case NKCUIBase.eMenutype.Overlay:
				if (NKCUIManager.m_rtOverlayRoot != null)
				{
					openedUI.transform.SetParent(NKCUIManager.m_rtOverlayRoot, false);
					openedUI.transform.SetAsLastSibling();
				}
				NKCUIManager.m_setOverlayUI.Add(openedUI);
				openedUI.Activate();
				return;
			}
			NKCUIManager.SetUpsideMenuState(openedUI, true);
			NKCUIManager.m_stkUI.Push(openedUI);
			openedUI.Activate();
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x00220CD4 File Offset: 0x0021EED4
		public static void RegisterUICallback(NKMUserData userData)
		{
			NKCUIManager.NKCUIUpsideMenu.RegisterUserdataCallback(userData);
			if (userData != null)
			{
				userData.m_InventoryData.dOnMiscInventoryUpdate += NKCUIManager.OnInventoryChange;
				userData.m_InventoryData.dOnEquipUpdate += NKCUIManager.OnEquipChange;
				NKMArmyData armyData = userData.m_ArmyData;
				armyData.dOnUnitUpdate = (NKMArmyData.OnUnitUpdate)Delegate.Combine(armyData.dOnUnitUpdate, new NKMArmyData.OnUnitUpdate(NKCUIManager.OnUnitUpdate));
				NKMArmyData armyData2 = userData.m_ArmyData;
				armyData2.dOnOperatorUpdate = (NKMArmyData.OnOperatorUpdate)Delegate.Combine(armyData2.dOnOperatorUpdate, new NKMArmyData.OnOperatorUpdate(NKCUIManager.OnOperatorUpdate));
				NKMArmyData armyData3 = userData.m_ArmyData;
				armyData3.dOnDeckUpdate = (NKMArmyData.OnDeckUpdate)Delegate.Combine(armyData3.dOnDeckUpdate, new NKMArmyData.OnDeckUpdate(NKCUIManager.OnDeckUpdate));
				userData.dOnUserLevelUpdate = (NKMUserData.OnUserLevelUpdate)Delegate.Combine(userData.dOnUserLevelUpdate, new NKMUserData.OnUserLevelUpdate(NKCUIManager.OnUserLevelChanged));
				userData.dOnCompanyBuffUpdate = (NKMUserData.OnCompanyBuffUpdate)Delegate.Combine(userData.dOnCompanyBuffUpdate, new NKMUserData.OnCompanyBuffUpdate(NKCUIManager.OnCompanyBuffUpdate));
				userData.OfficeData.dOnInteriorInventoryUpdate += NKCUIManager.OnInteriorInventoryUpdate;
			}
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x00220DF0 File Offset: 0x0021EFF0
		private static void OnUserLevelChanged(NKMUserData userData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnUserLevelChanged(userData);
			}
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x00220E40 File Offset: 0x0021F040
		private static void OnInventoryChange(NKMItemMiscData itemData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnInventoryChange(itemData);
			}
			foreach (NKCUIManager.IInventoryChangeObserver inventoryChangeObserver in NKCUIManager.m_dicInventoryObserver.Values)
			{
				if (inventoryChangeObserver != null)
				{
					inventoryChangeObserver.OnInventoryChange(itemData);
				}
			}
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x00220EDC File Offset: 0x0021F0DC
		private static void OnInteriorInventoryUpdate(NKMInteriorData interiorData, bool bAdded)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnInteriorInventoryUpdate(interiorData, bAdded);
			}
			foreach (NKCUIManager.IInventoryChangeObserver inventoryChangeObserver in NKCUIManager.m_dicInventoryObserver.Values)
			{
				if (inventoryChangeObserver != null)
				{
					inventoryChangeObserver.OnInteriorInventoryUpdate(interiorData, bAdded);
				}
			}
		}

		// Token: 0x06006959 RID: 26969 RVA: 0x00220F78 File Offset: 0x0021F178
		private static void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnEquipChange(eType, equipUID, equipData);
			}
			foreach (NKCUIManager.IInventoryChangeObserver inventoryChangeObserver in NKCUIManager.m_dicInventoryObserver.Values)
			{
				if (inventoryChangeObserver != null)
				{
					inventoryChangeObserver.OnEquipChange(eType, equipUID, equipData);
				}
			}
		}

		// Token: 0x0600695A RID: 26970 RVA: 0x00221018 File Offset: 0x0021F218
		private static void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnUnitUpdate(eEventType, eUnitType, uid, unitData);
			}
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x0022106C File Offset: 0x0021F26C
		private static void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnOperatorUpdate(eEventType, uid, operatorData);
			}
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x002210C0 File Offset: 0x0021F2C0
		private static void OnDeckUpdate(NKMDeckIndex deckIndex, NKMDeckData deckData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnDeckUpdate(deckIndex, deckData);
			}
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x00221114 File Offset: 0x0021F314
		private static void OnCompanyBuffUpdate(NKMUserData userData)
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnCompanyBuffUpdate(userData);
			}
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x00221164 File Offset: 0x0021F364
		public static bool OnHotkey(HotkeyEventType hotkey)
		{
			return NKCUIManager.m_stkUI.Count > 0 && NKCUIManager.m_stkUI.Peek().OnHotkey(hotkey) && hotkey != HotkeyEventType.ShowHotkey;
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x00221191 File Offset: 0x0021F391
		public static void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (NKCUIManager.m_stkUI.Count > 0)
			{
				NKCUIManager.m_stkUI.Peek().OnHotkeyHold(hotkey);
			}
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x002211B0 File Offset: 0x0021F3B0
		public static void OnHotKeyRelease(HotkeyEventType hotkey)
		{
			if (NKCUIManager.m_stkUI.Count > 0)
			{
				NKCUIManager.m_stkUI.Peek().OnHotkeyRelease(hotkey);
			}
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x002211D0 File Offset: 0x0021F3D0
		public static NKCUIBase FindRootUIBase(Transform tr)
		{
			while (tr.parent != null)
			{
				tr = tr.parent;
				NKCUIBase component = tr.GetComponent<NKCUIBase>();
				if (component != null)
				{
					return component;
				}
			}
			return null;
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x00221208 File Offset: 0x0021F408
		public static void OnScreenResolutionChanged()
		{
			NKCUIManager.SetAspect();
			NKCUIComSafeArea.RevertCalculatedSafeArea();
			NKCUIComSafeArea.InitSafeArea();
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				NKCUIComSafeArea[] componentsInChildren = rootGameObjects[i].GetComponentsInChildren<NKCUIComSafeArea>(true);
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					if (!(componentsInChildren[j] == null) && componentsInChildren[j].enabled)
					{
						if (componentsInChildren[j].CheckInit())
						{
							componentsInChildren[j].Rollback();
						}
						componentsInChildren[j].SetSafeAreaBase();
					}
				}
			}
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnScreenResolutionChanged();
			}
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x002212D4 File Offset: 0x0021F4D4
		public static void OnGuildDataChanged()
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnGuildDataChanged();
			}
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x00221324 File Offset: 0x0021F524
		public static void OnMissionUpdated()
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				nkcuibase.OnMissionUpdated();
			}
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x00221374 File Offset: 0x0021F574
		public static void UIClosed(NKCUIBase closedUI)
		{
			if (closedUI.eUIType == NKCUIBase.eMenutype.Overlay)
			{
				if (!NKCUIManager.m_setOverlayUI.Contains(closedUI))
				{
					Debug.LogWarning("Closed UI asked to closed again");
					return;
				}
				NKCUIManager.m_setOverlayUI.Remove(closedUI);
				closedUI.CloseInternal();
				return;
			}
			else
			{
				if (!NKCUIManager.m_stkUI.Contains(closedUI))
				{
					Debug.LogError(string.Concat(new string[]
					{
						"FATAL : closing UI(",
						closedUI.gameObject.name,
						" : ",
						closedUI.MenuName,
						") not registered in UIManager."
					}));
					closedUI.CloseInternal();
					return;
				}
				while (NKCUIManager.m_stkUI.Count > 0)
				{
					NKCUIBase nkcuibase = NKCUIManager.m_stkUI.Pop();
					nkcuibase._ForceCloseInternal();
					if (nkcuibase == closedUI)
					{
						break;
					}
				}
				if (closedUI.IsFullScreenUI)
				{
					foreach (NKCUIBase nkcuibase2 in NKCUIManager.m_stkUI)
					{
						nkcuibase2.UnHide();
						if (nkcuibase2.IsFullScreenUI)
						{
							NKCUIManager.s_currentFullScreenUIBase = nkcuibase2;
							NKCUIManager.SetBackground(nkcuibase2);
							break;
						}
					}
				}
				NKCUIManager.UpdateUpsideMenu();
				return;
			}
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x00221494 File Offset: 0x0021F694
		public static void UpdateUpsideMenu()
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				if (nkcuibase.eUpsideMenuMode != NKCUIUpsideMenu.eMode.Invalid)
				{
					NKCUIManager.SetUpsideMenuState(nkcuibase, false);
					break;
				}
			}
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x002214F4 File Offset: 0x0021F6F4
		private static void SetUpsideMenuState(NKCUIBase targetUI, bool bOpen = false)
		{
			if (targetUI.eUpsideMenuMode == NKCUIUpsideMenu.eMode.Invalid)
			{
				return;
			}
			if (targetUI.eUpsideMenuMode == NKCUIUpsideMenu.eMode.Disable)
			{
				NKCUIManager.NKCUIUpsideMenu.Close();
				return;
			}
			string name;
			switch (targetUI.eUpsideMenuMode)
			{
			case NKCUIUpsideMenu.eMode.Disable:
			case NKCUIUpsideMenu.eMode.ResourceOnly:
			case NKCUIUpsideMenu.eMode.Invalid:
				name = ((NKCUIManager.s_currentFullScreenUIBase != null) ? NKCUIManager.s_currentFullScreenUIBase.MenuName : targetUI.MenuName);
				goto IL_71;
			}
			name = targetUI.MenuName;
			IL_71:
			NKCUIManager.NKCUIUpsideMenu.Open(targetUI.UpsideMenuShowResourceList, targetUI.eUpsideMenuMode, name, targetUI.GuideTempletID, targetUI.DisableSubMenu);
		}

		// Token: 0x06006968 RID: 26984 RVA: 0x00221598 File Offset: 0x0021F798
		public static void OnHomeButton()
		{
			using (Stack<NKCUIBase>.Enumerator enumerator = NKCUIManager.m_stkUI.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.OnHomeButton())
					{
						return;
					}
				}
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x002215F8 File Offset: 0x0021F7F8
		public static void OnBackButton()
		{
			if (NKCUIManager.m_eUITransitionState != NKCUIManager.eUITransitionProcess.Idle)
			{
				return;
			}
			if (!NKCUIFadeInOut.IsFinshed())
			{
				return;
			}
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				if (!nkcuibase.IsHidden && nkcuibase.IgnoreBackButtonWhenOpen)
				{
					return;
				}
			}
			foreach (NKCUIBase nkcuibase2 in NKCUIManager.m_setOverlayUI)
			{
				if (!nkcuibase2.IsHidden && nkcuibase2.IgnoreBackButtonWhenOpen)
				{
					return;
				}
			}
			Log.Debug("[OnBackButton] back button pressed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIManager.cs", 1169);
			if (NKCUIManager.m_stkUI.Count > 0)
			{
				NKCUIManager.m_stkUI.Peek().OnBackButton();
				return;
			}
			if (NKCScenManager.GetScenManager() != null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
			}
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x00221700 File Offset: 0x0021F900
		private static int GetUIKey()
		{
			return ++NKCUIManager.UIKeySeed;
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x00221710 File Offset: 0x0021F910
		public static NKCUIManager.LoadedUIData GetInstance(string bundleName, string assetName, bool OpenedUIOnly)
		{
			foreach (NKCUIManager.LoadedUIData loadedUIData in NKCUIManager.s_dicLoadedUI.Values)
			{
				if (loadedUIData.bundleName == bundleName && loadedUIData.assetName == assetName)
				{
					if (OpenedUIOnly)
					{
						bool isUIOpen = loadedUIData.IsUIOpen;
						return loadedUIData;
					}
					return loadedUIData;
				}
			}
			return null;
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x00221794 File Offset: 0x0021F994
		public static bool IsValid(NKCUIManager.LoadedUIData uiData)
		{
			return uiData != null && NKCUIManager.s_dicLoadedUI.ContainsKey(uiData.key) && uiData.HasAssetResourceData;
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x002217BA File Offset: 0x0021F9BA
		public static NKCUIManager.LoadedUIData OpenNewInstance<T>(NKMAssetName assetName, Transform parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(assetName.m_BundleName, assetName.m_AssetName, parent, onCleanupInstance, false);
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x002217D0 File Offset: 0x0021F9D0
		public static NKCUIManager.LoadedUIData OpenNewInstance<T>(NKMAssetName assetName, NKCUIManager.eUIBaseRect parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(assetName.m_BundleName, assetName.m_AssetName, NKCUIManager.GetUIBaseRect(parent), onCleanupInstance, false);
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x002217EB File Offset: 0x0021F9EB
		public static NKCUIManager.LoadedUIData OpenNewInstance<T>(string BundleName, string AssetName, Transform parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(BundleName, AssetName, parent, onCleanupInstance, false);
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x002217F7 File Offset: 0x0021F9F7
		public static NKCUIManager.LoadedUIData OpenNewInstance<T>(string BundleName, string AssetName, NKCUIManager.eUIBaseRect parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(BundleName, AssetName, NKCUIManager.GetUIBaseRect(parent), onCleanupInstance, false);
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x00221808 File Offset: 0x0021FA08
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync<T>(string BundleName, string AssetName, Transform parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(BundleName, AssetName, parent, onCleanupInstance, true);
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x00221814 File Offset: 0x0021FA14
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync<T>(string BundleName, string AssetName, NKCUIManager.eUIBaseRect parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(BundleName, AssetName, NKCUIManager.GetUIBaseRect(parent), onCleanupInstance, true);
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x00221828 File Offset: 0x0021FA28
		private static NKCUIManager.LoadedUIData OpenNewInstance<T>(string BundleName, string AssetName, Transform parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance, bool bAsync) where T : NKCUIBase
		{
			NKCUIManager.LoadedUIData reuseableUIData = NKCUIManager.GetReuseableUIData<T>(BundleName, AssetName);
			if (reuseableUIData != null)
			{
				return reuseableUIData;
			}
			NKCAssetResourceData assetData = NKCAssetResourceManager.OpenResource<GameObject>(BundleName, AssetName, bAsync, null);
			NKCUIManager.LoadedUIData loadedUIData = new NKCUIManager.LoadedUIData(NKCUIManager.GetUIKey(), assetData, parent, onCleanupInstance);
			NKCUIManager.s_dicLoadedUI.Add(loadedUIData.key, loadedUIData);
			loadedUIData.IsOpenReserved = bAsync;
			return loadedUIData;
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x00221878 File Offset: 0x0021FA78
		private static NKCUIManager.LoadedUIData GetReuseableUIData<T>(string BundleName, string AssetName) where T : NKCUIBase
		{
			foreach (NKCUIManager.LoadedUIData loadedUIData in NKCUIManager.s_dicLoadedUI.Values)
			{
				if (string.Equals(loadedUIData.bundleName, BundleName, StringComparison.InvariantCultureIgnoreCase) && loadedUIData.assetName == AssetName && NKCUIManager.IsValid(loadedUIData) && (!loadedUIData.HasInstance || (loadedUIData.GetInstance<T>() != null && !loadedUIData.GetInstance<T>().IsOpen)))
				{
					return loadedUIData;
				}
			}
			return null;
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x00221924 File Offset: 0x0021FB24
		public static NKCUIManager.LoadedUIData GetUIData(int key)
		{
			NKCUIManager.LoadedUIData result;
			if (NKCUIManager.s_dicLoadedUI.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x00221943 File Offset: 0x0021FB43
		private static void InstanceClosed(NKCUIManager.LoadedUIData uiData)
		{
			if (uiData == null)
			{
				return;
			}
			if (!NKCUIManager.s_dicLoadedUI.ContainsKey(uiData.key))
			{
				Debug.LogWarning("bad UI Key. UI Already unloaded maybe?");
				return;
			}
			NKCUIManager.s_dicLoadedUI.Remove(uiData.key);
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x00221978 File Offset: 0x0021FB78
		public static void OnSceneOpenComplete()
		{
			foreach (KeyValuePair<int, NKCUIManager.LoadedUIData> keyValuePair in NKCUIManager.s_dicLoadedUI)
			{
				NKCUIManager.LoadedUIData value = keyValuePair.Value;
				if (value.IsLoadComplete)
				{
					value.IsOpenReserved = false;
				}
			}
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x002219DC File Offset: 0x0021FBDC
		public static bool IsUnloadUIOnScenChange()
		{
			return NKCScenManager.GetScenManager().GetSystemMemorySize() < 4000;
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x002219EF File Offset: 0x0021FBEF
		public static void OnScenEnd(NKCUIManager.eUIUnloadFlag unloadFlag)
		{
			NKCPopupMessageToastSimple.CheckInstanceAndClose();
			NKCUIManager.UnloadAllUI(unloadFlag, false);
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x00221A00 File Offset: 0x0021FC00
		public static void UnloadAllUI(NKCUIManager.eUIUnloadFlag unloadFlag, bool dontCloseOpenedUI = false)
		{
			List<int> list = new List<int>();
			bool flag = unloadFlag == NKCUIManager.eUIUnloadFlag.DEFAULT && !NKCUIManager.IsUnloadUIOnScenChange();
			foreach (KeyValuePair<int, NKCUIManager.LoadedUIData> keyValuePair in NKCUIManager.s_dicLoadedUI)
			{
				NKCUIManager.LoadedUIData value = keyValuePair.Value;
				if (value.HasInstance)
				{
					NKCUIBase instance = value.GetInstance<NKCUIBase>();
					if (instance != null)
					{
						if ((dontCloseOpenedUI && (value.IsOpenReserved || instance.IsOpen)) || (instance.eUIType == NKCUIBase.eMenutype.Overlay && instance.IsOpen))
						{
							continue;
						}
						if (instance.IsOpen)
						{
							instance.Close();
						}
						if (flag)
						{
							continue;
						}
						if (value.eUnloadFlag <= unloadFlag)
						{
							instance.OnCloseInstance();
							UnityEngine.Object.Destroy(instance.gameObject);
							NKCUIManager.LoadedUIData.OnCleanupInstance dOnCleanupInstance = value.dOnCleanupInstance;
							if (dOnCleanupInstance != null)
							{
								dOnCleanupInstance();
							}
							value.dOnCleanupInstance = null;
						}
					}
				}
				if (value.eUnloadFlag <= unloadFlag)
				{
					list.Add(keyValuePair.Key);
					NKCAssetResourceManager.CloseResource(value.bundleName, value.assetName);
				}
			}
			foreach (int key in list)
			{
				NKCUIManager.s_dicLoadedUI.Remove(key);
			}
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x00221B78 File Offset: 0x0021FD78
		public static bool CheckUIOpenError()
		{
			bool result = false;
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, NKCUIManager.LoadedUIData> keyValuePair in NKCUIManager.s_dicLoadedUI)
			{
				NKCUIManager.LoadedUIData value = keyValuePair.Value;
				if (value.HasInstance)
				{
					NKCUIBase instance = value.GetInstance<NKCUIBase>();
					if (instance != null && instance.gameObject.activeInHierarchy && !instance.IsOpen)
					{
						result = true;
						instance.OnCloseInstance();
						UnityEngine.Object.Destroy(instance.gameObject);
						NKCUIManager.LoadedUIData.OnCleanupInstance dOnCleanupInstance = value.dOnCleanupInstance;
						if (dOnCleanupInstance != null)
						{
							dOnCleanupInstance();
						}
						value.dOnCleanupInstance = null;
						list.Add(keyValuePair.Key);
						NKCAssetResourceManager.CloseResource(value.bundleName, value.assetName);
					}
				}
			}
			foreach (int key in list)
			{
				NKCUIManager.s_dicLoadedUI.Remove(key);
			}
			return result;
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x00221CA4 File Offset: 0x0021FEA4
		public static T GetOpenedUIByType<T>() where T : NKCUIBase
		{
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				if (nkcuibase is T)
				{
					return nkcuibase as T;
				}
			}
			return default(T);
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x00221D10 File Offset: 0x0021FF10
		public static List<T> GetOpenedUIsByType<T>() where T : NKCUIBase
		{
			List<T> list = new List<T>();
			foreach (NKCUIBase nkcuibase in NKCUIManager.m_stkUI)
			{
				T t = nkcuibase as T;
				if (t != null)
				{
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x00221D84 File Offset: 0x0021FF84
		public static bool IsAnyPopupOpened()
		{
			using (Stack<NKCUIBase>.Enumerator enumerator = NKCUIManager.m_stkUI.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.eUIType == NKCUIBase.eMenutype.Popup)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x00221DE0 File Offset: 0x0021FFE0
		public static void SetAsTopmost(NKCUIBase ui, bool bForce = false)
		{
			if (!ui.IsOpen)
			{
				return;
			}
			if (!ui.IsFullScreenUI)
			{
				return;
			}
			if (!bForce && NKCUIManager.s_currentFullScreenUIBase == ui)
			{
				return;
			}
			while (NKCUIManager.m_stkUI.Count > 0)
			{
				if (!(NKCUIManager.m_stkUI.Peek() != ui))
				{
					ui.UnHide();
					NKCUIManager.s_currentFullScreenUIBase = ui;
					NKCUIManager.SetBackground(ui);
					break;
				}
				NKCUIManager.m_stkUI.Pop()._ForceCloseInternal();
			}
			NKCUIManager.UpdateUpsideMenu();
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x00221E59 File Offset: 0x00220059
		public static NKCUIBase.eMenutype GetTopmostUIType()
		{
			return NKCUIManager.m_stkUI.Peek().eUIType;
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x00221E6A File Offset: 0x0022006A
		public static NKCUIUpsideMenu.eMode GetTopmoseUIUpsidemenuMode()
		{
			if (NKCUIManager.m_stkUI.Count > 0)
			{
				return NKCUIManager.m_stkUI.Peek().eUpsideMenuMode;
			}
			return NKCUIUpsideMenu.eMode.Invalid;
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x00221E8A File Offset: 0x0022008A
		public static bool IsTopmostUI(NKCUIBase ui)
		{
			return !(ui == null) && NKCUIManager.m_stkUI.Count != 0 && NKCUIManager.m_stkUI.Peek() == ui;
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x00221EB8 File Offset: 0x002200B8
		public static void CloseAllOverlay()
		{
			foreach (NKCUIBase nkcuibase in new List<NKCUIBase>(NKCUIManager.m_setOverlayUI))
			{
				nkcuibase.Close();
			}
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x00221F0C File Offset: 0x0022010C
		public static void CloseAllPopup()
		{
			while (NKCUIManager.m_stkUI.Count > 0)
			{
				if (!NKCUIManager.m_stkUI.Peek().IsPopupUI)
				{
					return;
				}
				NKCUIManager.m_stkUI.Pop()._ForceCloseInternal();
			}
		}

		// Token: 0x040054FC RID: 21756
		public static int m_sHandle = 0;

		// Token: 0x040054FD RID: 21757
		public static Dictionary<int, NKCUIManager.IInventoryChangeObserver> m_dicInventoryObserver = new Dictionary<int, NKCUIManager.IInventoryChangeObserver>();

		// Token: 0x040054FE RID: 21758
		private static GameObject m_NKM_SCEN_UI = null;

		// Token: 0x040054FF RID: 21759
		public static Transform m_TR_NKM_WAIT_INSTANT;

		// Token: 0x04005500 RID: 21760
		public static GameObject m_NUF_GAME_TOUCH_OBJECT;

		// Token: 0x04005501 RID: 21761
		private static GameObject m_NKM_SCEN_UI_BACK_Canvas = null;

		// Token: 0x04005502 RID: 21762
		private static CanvasScaler m_NKM_SCEN_UI_BACK_Canvas_CanvasScaler = null;

		// Token: 0x04005503 RID: 21763
		private static GameObject m_NKM_SCEN_UI_MID_Canvas = null;

		// Token: 0x04005504 RID: 21764
		private static CanvasScaler m_NKM_SCEN_UI_MID_Canvas_CanvasScaler = null;

		// Token: 0x04005505 RID: 21765
		private static GameObject m_NKM_SCEN_UI_FRONT_LOW_Canvas = null;

		// Token: 0x04005506 RID: 21766
		private static CanvasScaler m_NKM_SCEN_UI_FRONT_LOW_Canvas_CanvasScaler = null;

		// Token: 0x04005507 RID: 21767
		private static GameObject m_NKM_SCEN_UI_FRONT_Canvas = null;

		// Token: 0x04005508 RID: 21768
		private static CanvasScaler m_NKM_SCEN_UI_FRONT_Canvas_CanvasScaler = null;

		// Token: 0x0400550A RID: 21770
		private static GameObject m_NKM_SCEN_UI_FRONT_LOW = null;

		// Token: 0x0400550B RID: 21771
		private static Image m_NKM_SCEN_UI_FRONT_LOW_Image = null;

		// Token: 0x0400550C RID: 21772
		private static GameObject m_NKM_SCEN_UI_FRONT = null;

		// Token: 0x0400550D RID: 21773
		private static Image m_NKM_SCEN_UI_FRONT_Image = null;

		// Token: 0x0400550E RID: 21774
		private static RectTransform m_rectBackground;

		// Token: 0x0400550F RID: 21775
		private static Image m_imgBackground;

		// Token: 0x04005510 RID: 21776
		private static RectTransform m_NKM_SCEN_UI_FRONT_RectTransform = null;

		// Token: 0x04005511 RID: 21777
		private static RectTransform m_NKM_SCEN_UI_FRONT_LOW_RectTransform = null;

		// Token: 0x04005512 RID: 21778
		private static NKCUILooseShaker m_NKCUILooseShaker;

		// Token: 0x04005513 RID: 21779
		private static RectTransform m_NUF_DRAG = null;

		// Token: 0x04005514 RID: 21780
		private static RectTransform m_rtCommonLowRoot = null;

		// Token: 0x04005515 RID: 21781
		private static RectTransform m_rtCommonRoot = null;

		// Token: 0x04005516 RID: 21782
		private static RectTransform m_rtFrontLowRoot = null;

		// Token: 0x04005517 RID: 21783
		private static NKCUIBase s_currentFullScreenUIBase = null;

		// Token: 0x04005518 RID: 21784
		private static bool s_bUseCameraFunctions = false;

		// Token: 0x0400551D RID: 21789
		private static NKCUIPowerSaveMode m_NKCUIPowerSaveMode = null;

		// Token: 0x0400551F RID: 21791
		private static int m_ScreenWidth;

		// Token: 0x04005520 RID: 21792
		private static int m_ScreenHeight;

		// Token: 0x04005521 RID: 21793
		private static RectTransform m_rtPopupRoot;

		// Token: 0x04005522 RID: 21794
		private static RectTransform m_rtOverlayRoot;

		// Token: 0x04005526 RID: 21798
		private static GameObject m_NUF_BLOCK_SCREEN_INPUT;

		// Token: 0x04005527 RID: 21799
		private static Stack<NKCUIBase> m_stkUI = new Stack<NKCUIBase>();

		// Token: 0x04005528 RID: 21800
		private static HashSet<NKCUIBase> m_setOverlayUI = new HashSet<NKCUIBase>();

		// Token: 0x04005529 RID: 21801
		private static NKCUIBase m_PreparingUI;

		// Token: 0x0400552A RID: 21802
		private static NKCUIManager.eUITransitionProcess m_eUITransitionState = NKCUIManager.eUITransitionProcess.Idle;

		// Token: 0x0400552B RID: 21803
		private static int UIKeySeed = 0;

		// Token: 0x0400552C RID: 21804
		private static Dictionary<int, NKCUIManager.LoadedUIData> s_dicLoadedUI = new Dictionary<int, NKCUIManager.LoadedUIData>();

		// Token: 0x020016B2 RID: 5810
		public interface IInventoryChangeObserver
		{
			// Token: 0x17001918 RID: 6424
			// (get) Token: 0x0600B107 RID: 45319
			// (set) Token: 0x0600B108 RID: 45320
			int Handle { get; set; }

			// Token: 0x0600B109 RID: 45321
			void OnInventoryChange(NKMItemMiscData itemData);

			// Token: 0x0600B10A RID: 45322
			void OnInteriorInventoryUpdate(NKMInteriorData interiorData, bool bAdded);

			// Token: 0x0600B10B RID: 45323
			void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipData);
		}

		// Token: 0x020016B3 RID: 5811
		public enum eUIBaseRect
		{
			// Token: 0x0400A508 RID: 42248
			UIFrontLow,
			// Token: 0x0400A509 RID: 42249
			UIFrontCommonLow,
			// Token: 0x0400A50A RID: 42250
			UIFrontCommon,
			// Token: 0x0400A50B RID: 42251
			UIFrontPopup,
			// Token: 0x0400A50C RID: 42252
			UIMidCanvas,
			// Token: 0x0400A50D RID: 42253
			UIFrontCanvas,
			// Token: 0x0400A50E RID: 42254
			UIOverlay
		}

		// Token: 0x020016B4 RID: 5812
		private enum eUITransitionProcess : short
		{
			// Token: 0x0400A510 RID: 42256
			Idle,
			// Token: 0x0400A511 RID: 42257
			Preparing,
			// Token: 0x0400A512 RID: 42258
			FinishedAndWaiting
		}

		// Token: 0x020016B5 RID: 5813
		public enum eUIUnloadFlag
		{
			// Token: 0x0400A514 RID: 42260
			DEFAULT,
			// Token: 0x0400A515 RID: 42261
			ON_PLAY_GAME,
			// Token: 0x0400A516 RID: 42262
			ONLY_MEMORY_SHORTAGE,
			// Token: 0x0400A517 RID: 42263
			NEVER_UNLOAD
		}

		// Token: 0x020016B6 RID: 5814
		public class LoadedUIData
		{
			// Token: 0x0600B10C RID: 45324 RVA: 0x0035FC6C File Offset: 0x0035DE6C
			public LoadedUIData(int _key, NKCAssetResourceData _assetData, Transform _parent, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance)
			{
				this.key = _key;
				this.assetData = _assetData;
				this.parent = _parent;
				this.instance = null;
				this.eUnloadFlag = NKCUIManager.eUIUnloadFlag.DEFAULT;
				this.dOnCleanupInstance = onCleanupInstance;
				if (this.assetData == null)
				{
					Debug.LogError("assetData is Null!");
					this.bundleName = "";
					this.assetName = "";
					return;
				}
				this.bundleName = this.assetData.m_NKMAssetName.m_BundleName;
				this.assetName = this.assetData.m_NKMAssetName.m_AssetName;
			}

			// Token: 0x17001919 RID: 6425
			// (get) Token: 0x0600B10D RID: 45325 RVA: 0x0035FCFF File Offset: 0x0035DEFF
			// (set) Token: 0x0600B10E RID: 45326 RVA: 0x0035FD07 File Offset: 0x0035DF07
			public bool IsOpenReserved { get; set; }

			// Token: 0x1700191A RID: 6426
			// (get) Token: 0x0600B10F RID: 45327 RVA: 0x0035FD10 File Offset: 0x0035DF10
			public bool IsLoadComplete
			{
				get
				{
					return this.assetData != null && (this.instance != null || this.assetData.IsDone());
				}
			}

			// Token: 0x1700191B RID: 6427
			// (get) Token: 0x0600B110 RID: 45328 RVA: 0x0035FD37 File Offset: 0x0035DF37
			public bool HasInstance
			{
				get
				{
					return this.instance != null;
				}
			}

			// Token: 0x1700191C RID: 6428
			// (get) Token: 0x0600B111 RID: 45329 RVA: 0x0035FD45 File Offset: 0x0035DF45
			public bool HasAssetResourceData
			{
				get
				{
					return this.assetData != null;
				}
			}

			// Token: 0x1700191D RID: 6429
			// (get) Token: 0x0600B112 RID: 45330 RVA: 0x0035FD50 File Offset: 0x0035DF50
			public bool IsUIOpen
			{
				get
				{
					return this.instance != null && this.instance.IsOpen;
				}
			}

			// Token: 0x0600B113 RID: 45331 RVA: 0x0035FD70 File Offset: 0x0035DF70
			public NKCUIBase GetInstance()
			{
				if (this.instance != null)
				{
					return this.instance;
				}
				if (this.assetData == null)
				{
					return null;
				}
				if (!this.assetData.IsDone())
				{
					return null;
				}
				GameObject asset = this.assetData.GetAsset<GameObject>();
				if (asset != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(asset, this.parent);
					this.instance = gameObject.GetComponent<NKCUIBase>();
					if (this.instance != null)
					{
						if (this.instance.GetComponent<Canvas>() == null)
						{
							this.instance.gameObject.AddComponent<Canvas>().additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
							GraphicRaycaster graphicRaycaster = this.instance.gameObject.AddComponent<GraphicRaycaster>();
							graphicRaycaster.ignoreReversedGraphics = true;
							graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
						}
						NKCUIManager.OpenUI(this.instance.gameObject);
						this.eUnloadFlag = this.instance.UnloadFlag;
					}
				}
				else
				{
					this.instance = null;
				}
				return this.instance;
			}

			// Token: 0x0600B114 RID: 45332 RVA: 0x0035FE64 File Offset: 0x0035E064
			public T GetInstance<T>() where T : NKCUIBase
			{
				if (this.instance != null)
				{
					return this.instance as T;
				}
				if (this.assetData == null)
				{
					return default(T);
				}
				if (!this.assetData.IsDone())
				{
					return default(T);
				}
				GameObject asset = this.assetData.GetAsset<GameObject>();
				if (asset != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(asset, this.parent);
					this.instance = gameObject.GetComponent<T>();
					if (this.instance != null)
					{
						if (this.instance.GetComponent<Canvas>() == null)
						{
							this.instance.gameObject.AddComponent<Canvas>().additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
							GraphicRaycaster graphicRaycaster = this.instance.gameObject.AddComponent<GraphicRaycaster>();
							graphicRaycaster.ignoreReversedGraphics = true;
							graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
						}
						NKCUIManager.OpenUI(this.instance.gameObject);
						this.eUnloadFlag = this.instance.UnloadFlag;
					}
				}
				else
				{
					this.instance = null;
				}
				return this.instance as T;
			}

			// Token: 0x0600B115 RID: 45333 RVA: 0x0035FF80 File Offset: 0x0035E180
			public void CloseInstance()
			{
				NKCAssetResourceManager.CloseResource(this.assetData);
				this.assetData = null;
				if (this.instance != null)
				{
					if (this.instance.IsOpen)
					{
						this.instance.Close();
					}
					this.instance.OnCloseInstance();
					UnityEngine.Object.Destroy(this.instance.gameObject);
				}
				NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance = this.dOnCleanupInstance;
				if (onCleanupInstance != null)
				{
					onCleanupInstance();
				}
				this.dOnCleanupInstance = null;
				this.instance = null;
				this.parent = null;
				NKCUIManager.InstanceClosed(this);
			}

			// Token: 0x0600B116 RID: 45334 RVA: 0x0036000D File Offset: 0x0035E20D
			public bool CheckLoadAndGetInstance<T>(out T ins) where T : NKCUIBase
			{
				if (this.IsLoadComplete)
				{
					ins = this.GetInstance<T>();
				}
				else
				{
					ins = default(T);
				}
				return ins != null;
			}

			// Token: 0x0600B117 RID: 45335 RVA: 0x00360040 File Offset: 0x0035E240
			public override string ToString()
			{
				if (this.IsLoadComplete)
				{
					return string.Format("{0} : {1}.{2} ({3})", new object[]
					{
						this.key,
						this.bundleName,
						this.assetName,
						this.eUnloadFlag
					});
				}
				return string.Format("{0} : {1}.{2} Loading..", this.key, this.bundleName, this.assetName);
			}

			// Token: 0x0400A518 RID: 42264
			public readonly int key;

			// Token: 0x0400A519 RID: 42265
			public readonly string bundleName;

			// Token: 0x0400A51A RID: 42266
			public readonly string assetName;

			// Token: 0x0400A51B RID: 42267
			public NKCUIManager.eUIUnloadFlag eUnloadFlag;

			// Token: 0x0400A51C RID: 42268
			public NKCUIManager.LoadedUIData.OnCleanupInstance dOnCleanupInstance;

			// Token: 0x0400A51D RID: 42269
			private NKCAssetResourceData assetData;

			// Token: 0x0400A51E RID: 42270
			private NKCUIBase instance;

			// Token: 0x0400A51F RID: 42271
			private Transform parent;

			// Token: 0x02001A86 RID: 6790
			// (Invoke) Token: 0x0600BC3A RID: 48186
			public delegate void OnCleanupInstance();
		}
	}
}
