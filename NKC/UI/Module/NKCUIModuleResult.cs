using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Module
{
	// Token: 0x02000B21 RID: 2849
	public class NKCUIModuleResult : NKCUIBase, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x060081CB RID: 33227 RVA: 0x002BC15B File Offset: 0x002BA35B
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060081CC RID: 33228 RVA: 0x002BC15E File Offset: 0x002BA35E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060081CD RID: 33229 RVA: 0x002BC161 File Offset: 0x002BA361
		public override string MenuName
		{
			get
			{
				return "NKCUIModuleResult ��� ����";
			}
		}

		// Token: 0x060081CE RID: 33230 RVA: 0x002BC168 File Offset: 0x002BA368
		public static void CheckInstanceAndClose()
		{
			if (NKCUIModuleResult.m_loadedUIData != null)
			{
				NKCUIModuleResult.m_loadedUIData.CloseInstance();
				NKCUIModuleResult.m_loadedUIData = null;
			}
		}

		// Token: 0x060081CF RID: 33231 RVA: 0x002BC184 File Offset: 0x002BA384
		public static NKCUIModuleResult MakeInstance(string bundleName, string assetName)
		{
			if (NKCUIModuleResult.m_loadedUIData == null)
			{
				NKCUIModuleResult.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCUIModuleResult>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIOverlay, null);
			}
			if (NKCUIModuleResult.m_loadedUIData == null)
			{
				return null;
			}
			NKCUIModuleResult instance = NKCUIModuleResult.m_loadedUIData.GetInstance<NKCUIModuleResult>();
			if (null == instance)
			{
				return null;
			}
			return instance;
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x002BC1C6 File Offset: 0x002BA3C6
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x002BC1E1 File Offset: 0x002BA3E1
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x002BC1E9 File Offset: 0x002BA3E9
		public override void OnBackButton()
		{
			base.OnBackButton();
			if (!string.IsNullOrEmpty(this.m_strBGM))
			{
				NKCUIModuleHome.PlayBGMMusic();
			}
			UnityAction unityAction = this.dClose;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x002BC214 File Offset: 0x002BA414
		public void Open(UnityAction close = null)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.dClose = close;
			this.m_SkeletonGraphic.AnimationState.ClearTrack(0);
			this.m_SkeletonGraphic.AnimationState.SetAnimation(0, "BASE", false);
			this.m_SkeletonGraphic.AnimationState.Complete += this.SetNextAni;
			if (!string.IsNullOrEmpty(this.m_strBGM))
			{
				NKCSoundManager.PlayMusic(this.m_strBGM, true, 1f, false, 0f, 0f);
			}
			if (!string.IsNullOrEmpty(this.m_strSound))
			{
				NKCSoundManager.PlaySound(this.m_strSound, 1f, 0f, 0f, false, 0f, false, 0f);
			}
			if (this.m_bFixResolution)
			{
				NKCCamera.RescaleRectToCameraFrustrum(this.m_rtScaleTargetRect, NKCCamera.GetSubUICamera(), Vector2.zero, NKCCamera.GetSubUICamera().transform.position.z, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
			}
			base.UIOpened(true);
		}

		// Token: 0x060081D4 RID: 33236 RVA: 0x002BC310 File Offset: 0x002BA510
		private void SetNextAni(TrackEntry trackEntry)
		{
			this.OnBackButton();
		}

		// Token: 0x060081D5 RID: 33237 RVA: 0x002BC318 File Offset: 0x002BA518
		public void OnPointerDown(PointerEventData eventData)
		{
			this.OnBackButton();
		}

		// Token: 0x060081D6 RID: 33238 RVA: 0x002BC320 File Offset: 0x002BA520
		public override void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Skip)
			{
				if (!NKCUIManager.IsTopmostUI(this))
				{
					return;
				}
				if (null != this.m_SkeletonGraphic)
				{
					this.m_SkeletonGraphic.AnimationState.TimeScale = 2f;
				}
			}
		}

		// Token: 0x060081D7 RID: 33239 RVA: 0x002BC353 File Offset: 0x002BA553
		public override void OnHotkeyRelease(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Skip)
			{
				if (!NKCUIManager.IsTopmostUI(this))
				{
					return;
				}
				if (null != this.m_SkeletonGraphic)
				{
					this.m_SkeletonGraphic.AnimationState.TimeScale = 1f;
				}
			}
		}

		// Token: 0x04006DEE RID: 28142
		public SkeletonGraphic m_SkeletonGraphic;

		// Token: 0x04006DEF RID: 28143
		public RectTransform m_rtScaleTargetRect;

		// Token: 0x04006DF0 RID: 28144
		[Header("Sound(��� ���)")]
		public string m_strBGM;

		// Token: 0x04006DF1 RID: 28145
		public string m_strSound;

		// Token: 0x04006DF2 RID: 28146
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04006DF3 RID: 28147
		public bool m_bFixResolution = true;

		// Token: 0x04006DF4 RID: 28148
		private UnityAction dClose;
	}
}
