using System;
using System.Collections;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009CE RID: 2510
	public class NKCUIPopupIllustView : NKCUIBase
	{
		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06006B17 RID: 27415 RVA: 0x0022C724 File Offset: 0x0022A924
		public static NKCUIPopupIllustView Instance
		{
			get
			{
				if (NKCUIPopupIllustView.m_Instance == null)
				{
					if (NKCDefineManager.DEFINE_UNITY_STANDALONE())
					{
						NKCUIPopupIllustView.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupIllustView>("AB_UI_NKM_UI_UNIT_INFO", "NKM_UI_POPUP_ILLUST_VIEW_PC", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupIllustView.CleanupInstance)).GetInstance<NKCUIPopupIllustView>();
					}
					else
					{
						NKCUIPopupIllustView.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupIllustView>("AB_UI_NKM_UI_UNIT_INFO", "NKM_UI_POPUP_ILLUST_VIEW", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupIllustView.CleanupInstance)).GetInstance<NKCUIPopupIllustView>();
					}
					NKCUIPopupIllustView.m_Instance.Init();
				}
				return NKCUIPopupIllustView.m_Instance;
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06006B18 RID: 27416 RVA: 0x0022C7A2 File Offset: 0x0022A9A2
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupIllustView.m_Instance != null && NKCUIPopupIllustView.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x0022C7BD File Offset: 0x0022A9BD
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupIllustView.m_Instance != null && NKCUIPopupIllustView.m_Instance.IsOpen)
			{
				NKCUIPopupIllustView.m_Instance.Close();
			}
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x0022C7E2 File Offset: 0x0022A9E2
		private static void CleanupInstance()
		{
			NKCUIPopupIllustView.m_Instance = null;
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06006B1B RID: 27419 RVA: 0x0022C7EA File Offset: 0x0022A9EA
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06006B1C RID: 27420 RVA: 0x0022C7ED File Offset: 0x0022A9ED
		public override string MenuName
		{
			get
			{
				return "ILLUST VIEW";
			}
		}

		// Token: 0x06006B1D RID: 27421 RVA: 0x0022C7F4 File Offset: 0x0022A9F4
		private void Init()
		{
			this.m_characterView.Init(null, null);
			this.m_characterView.SetMode(NKCUICharacterView.eMode.CharacterView, false);
			if (NKCDefineManager.DEFINE_UNITY_STANDALONE_WIN())
			{
				this.m_characterView.scrollSensibility = this.m_fScaleVal;
			}
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnClose));
		}

		// Token: 0x06006B1E RID: 27422 RVA: 0x0022C860 File Offset: 0x0022AA60
		public void Open(NKMUnitData unit)
		{
			if (unit == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unit.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.SetRootPosition(unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP);
			this.m_characterView.SetCharacterIllust(unit, false, true, true, 0);
			this.Open();
		}

		// Token: 0x06006B1F RID: 27423 RVA: 0x0022C8A6 File Offset: 0x0022AAA6
		public void Open(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return;
			}
			if (NKMUnitManager.GetUnitTempletBase(operatorData.id) == null)
			{
				return;
			}
			this.SetRootPosition(false);
			this.m_characterView.SetCharacterIllust(operatorData, false, true, true, 0);
			this.Open();
		}

		// Token: 0x06006B20 RID: 27424 RVA: 0x0022C8D7 File Offset: 0x0022AAD7
		public void Open(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				return;
			}
			this.SetRootPosition(unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP);
			this.m_characterView.SetCharacterIllust(unitTempletBase, 0, false, true, 0);
			this.Open();
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x0022C902 File Offset: 0x0022AB02
		public void Open(NKMSkinTemplet skinTemplet)
		{
			this.SetRootPosition(false);
			this.m_characterView.SetCharacterIllust(skinTemplet, false, true, 0);
			this.Open();
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x0022C920 File Offset: 0x0022AB20
		private void Open()
		{
			this.m_scrollRect.normalizedPosition = new Vector2(0.5f, 0.5f);
			this.m_content.localScale = Vector3.one;
			base.UIOpened(true);
			this.PlayAni("NKM_UI_POPUP_ILLUST_VIEW_INTRO", null);
			NKCUICharacterView characterView = this.m_characterView;
			if (((characterView != null) ? characterView.m_srScrollRect : null) != null)
			{
				this.m_characterView.m_srScrollRect.scrollSensitivity = 0f;
			}
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x0022C99C File Offset: 0x0022AB9C
		private void SetRootPosition(bool isShip)
		{
			if (isShip)
			{
				if (!NKCDefineManager.DEFINE_UNITY_STANDALONE())
				{
					this.m_unitRoot.localRotation = Quaternion.Euler(0f, 0f, -90f);
				}
				this.m_unitRoot.localPosition = this.m_vecPositionShip;
				return;
			}
			this.m_unitRoot.localRotation = Quaternion.Euler(0f, 0f, 0f);
			this.m_unitRoot.localPosition = this.m_vecPositionUnit;
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x0022CA14 File Offset: 0x0022AC14
		public override void CloseInternal()
		{
			this.m_characterView.CleanUp();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x0022CA2D File Offset: 0x0022AC2D
		private void OnClose()
		{
			this.PlayAni("NKM_UI_POPUP_ILLUST_VIEW_OUTRO", new UnityAction(base.Close));
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x0022CA48 File Offset: 0x0022AC48
		public override void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Plus)
			{
				this.m_characterView.OnPinchZoom(Vector2.zero, Time.deltaTime * 0.5f);
				return;
			}
			if (hotkey != HotkeyEventType.Minus)
			{
				return;
			}
			this.m_characterView.OnPinchZoom(Vector2.zero, -Time.deltaTime * 0.5f);
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x0022CA98 File Offset: 0x0022AC98
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.ShowHotkey)
			{
				NKCUIComHotkeyDisplay.OpenInstance(base.transform, new HotkeyEventType[]
				{
					HotkeyEventType.Plus,
					HotkeyEventType.Minus
				});
			}
			return false;
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x0022CABB File Offset: 0x0022ACBB
		private void PlayAni(string ani_name, UnityAction onFinish = null)
		{
			if (this.m_mask != null)
			{
				this.m_mask.enabled = true;
			}
			this.m_animator.Play(ani_name);
			base.StartCoroutine(this.OnCompleteAni(ani_name, onFinish));
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x0022CAF2 File Offset: 0x0022ACF2
		private IEnumerator OnCompleteAni(string aniName, UnityAction onFinish)
		{
			if (!this.m_animator.GetCurrentAnimatorStateInfo(0).IsName(aniName))
			{
				yield return null;
			}
			while (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			if (this.m_mask != null)
			{
				this.m_mask.enabled = false;
			}
			if (onFinish != null)
			{
				onFinish();
			}
			yield break;
		}

		// Token: 0x040056BA RID: 22202
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_INFO";

		// Token: 0x040056BB RID: 22203
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_ILLUST_VIEW";

		// Token: 0x040056BC RID: 22204
		public const string UI_ASSET_NAME_PC = "NKM_UI_POPUP_ILLUST_VIEW_PC";

		// Token: 0x040056BD RID: 22205
		private static NKCUIPopupIllustView m_Instance;

		// Token: 0x040056BE RID: 22206
		public NKCUICharacterView m_characterView;

		// Token: 0x040056BF RID: 22207
		public Animator m_animator;

		// Token: 0x040056C0 RID: 22208
		public Transform m_unitRoot;

		// Token: 0x040056C1 RID: 22209
		public Mask m_mask;

		// Token: 0x040056C2 RID: 22210
		[Header("스크롤/확대/축소")]
		public ScrollRect m_scrollRect;

		// Token: 0x040056C3 RID: 22211
		public Transform m_content;

		// Token: 0x040056C4 RID: 22212
		[Header("BACKGROUND")]
		public GameObject m_objBG;

		// Token: 0x040056C5 RID: 22213
		[Header("BUTTONS")]
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040056C6 RID: 22214
		private const string ANIMATION_INTRO_NAME = "NKM_UI_POPUP_ILLUST_VIEW_INTRO";

		// Token: 0x040056C7 RID: 22215
		private const string ANIMATION_OUTRO_NAME = "NKM_UI_POPUP_ILLUST_VIEW_OUTRO";

		// Token: 0x040056C8 RID: 22216
		[Header("CharacterView Position")]
		public Vector3 m_vecPositionShip = Vector3.zero;

		// Token: 0x040056C9 RID: 22217
		public Vector3 m_vecPositionUnit = Vector3.zero;

		// Token: 0x040056CA RID: 22218
		[Header("PC Version - Whell Zoom")]
		public float m_fScaleVal = 0.1f;
	}
}
