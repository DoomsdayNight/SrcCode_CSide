using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000762 RID: 1890
	public abstract class NKCUIComStateButtonBase : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler, IPointerExitHandler
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x00169333 File Offset: 0x00167533
		public bool IsSelected
		{
			get
			{
				return this.m_bSelect;
			}
		}

		// Token: 0x06004B6B RID: 19307
		protected abstract void OnPointerDownEvent(PointerEventData eventData);

		// Token: 0x06004B6C RID: 19308
		protected abstract void OnPointerUpEvent(PointerEventData eventData);

		// Token: 0x06004B6D RID: 19309
		protected abstract void OnPointerClickEvent(PointerEventData eventData);

		// Token: 0x06004B6E RID: 19310 RVA: 0x0016933B File Offset: 0x0016753B
		protected virtual void OnPointerExitEvent(PointerEventData eventData)
		{
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0016933D File Offset: 0x0016753D
		private void Start()
		{
			if (this.m_uiRoot == null)
			{
				this.m_uiRoot = NKCUIManager.FindRootUIBase(base.transform);
			}
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00169360 File Offset: 0x00167560
		public void OnPointerDown(PointerEventData eventData)
		{
			this.m_Scale.SetNowValue(1f);
			this.m_Scale.SetTracking(this.m_fTouchSize, this.m_fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Pressed);
			if (this.m_fTouchSize < 1f)
			{
				this.MakeTempRayCaster();
			}
			this.m_fHoldingTime = 0f;
			this.m_fPressGap = this.m_fPressGapMax;
			this.m_touchPos = eventData.position;
			this.OnPointerDownEvent(eventData);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x001693DC File Offset: 0x001675DC
		public void OnPointerUp(PointerEventData eventData)
		{
			if (!this.m_bSelect)
			{
				this.m_Scale.SetTracking(1f, this.m_fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			else
			{
				this.m_Scale.SetTracking(this.m_fSelectSize, this.m_fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			this.OnPointerUpEvent(eventData);
			this.ClearTempRayCaster();
			this.ClearHolding();
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00169438 File Offset: 0x00167638
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				if (this.m_bSelectByClick)
				{
					this.Select(true, false, false);
				}
				else
				{
					this.SetButtonState(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
				}
				if (!string.IsNullOrEmpty(this.m_SoundForPointClick))
				{
					NKCSoundManager.PlaySound(this.m_SoundForPointClick, 1f, 0f, 0f, false, 0f, false, 0f);
				}
				this.OnPointerClickEvent(eventData);
				return;
			}
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Locked && this.m_bGetCallbackWhileLocked)
			{
				this.OnPointerClickEvent(eventData);
			}
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x001694C8 File Offset: 0x001676C8
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				this.SetButtonState(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
			}
			this.ClearHolding();
			this.OnPointerExitEvent(eventData);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x001694F2 File Offset: 0x001676F2
		private void StartHolding(Vector2 touchPos)
		{
			if (this.dOnPointerHolding == null)
			{
				return;
			}
			NKCUIHoldLoading.Instance.Open(touchPos, -1f);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0016950D File Offset: 0x0016770D
		private void OnHoldEvent()
		{
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				this.SetButtonState(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
			}
			NKCUIComStateButtonBase.OnPointerHolding onPointerHolding = this.dOnPointerHolding;
			if (onPointerHolding != null)
			{
				onPointerHolding();
			}
			this.ClearHolding();
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00169541 File Offset: 0x00167741
		private void ClearHolding()
		{
			if (NKCUIHoldLoading.IsOpen)
			{
				NKCUIHoldLoading.Instance.Close();
			}
			this.m_fHoldingTime = 0f;
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00169560 File Offset: 0x00167760
		public void UpdateOrgSize()
		{
			this.m_RectTransform = base.gameObject.GetComponentInChildren<RectTransform>();
			this.m_fOrgSizeX = this.m_RectTransform.localScale.x;
			this.m_fOrgSizeY = this.m_RectTransform.localScale.y;
			this.m_Scale.SetNowValue(1f);
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x001695BA File Offset: 0x001677BA
		private void Awake()
		{
			this.Select(this.m_bSelect, true, false);
			this.UpdateOrgSize();
			this.UpdateScale();
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x001695D8 File Offset: 0x001677D8
		public void Update()
		{
			this.m_Scale.Update(Time.deltaTime);
			this.UpdateScale();
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				if (this.dOnPointerHolding != null)
				{
					if (Input.touchCount > 1)
					{
						this.ClearHolding();
						return;
					}
					if (this.m_fHoldingTime < this.m_fDelayHold)
					{
						this.m_fHoldingTime += Time.deltaTime;
						return;
					}
					if (!NKCUIHoldLoading.IsOpen)
					{
						this.StartHolding(this.m_touchPos);
						return;
					}
					if (!NKCUIHoldLoading.Instance.IsPlaying())
					{
						this.OnHoldEvent();
					}
					return;
				}
				else if (this.dOnPointerHoldPress != null)
				{
					if (Input.touchCount > 1)
					{
						this.m_fHoldingTime = 0f;
						return;
					}
					this.ProcessHold();
					return;
				}
			}
			if ((!this.m_bLock || this.m_bGetCallbackWhileLocked) && this.m_HotkeyEventType != HotkeyEventType.None)
			{
				if (!NKCUIManager.IsTopmostUI(this.m_uiRoot))
				{
					return;
				}
				PointerEventData eventData2;
				if (NKCInputManager.CheckHotKeyEvent(this.m_HotkeyEventType))
				{
					this.m_fHoldingTime = 0f;
					this.m_fPressGap = this.m_fPressGapMax;
					PointerEventData eventData;
					if (this.m_bUseHotkeyUpDownEvent && this.CanCastRaycast(out eventData))
					{
						this.OnPointerDownEvent(eventData);
					}
				}
				else if (NKCInputManager.CheckHotKeyUp(this.m_HotkeyEventType) && this.m_bUseHotkeyUpDownEvent && this.CanCastRaycast(out eventData2))
				{
					this.OnPointerUpEvent(eventData2);
				}
				if (NKCInputManager.CheckHotKeyEvent(this.m_HotkeyEventType))
				{
					PointerEventData eventData3;
					if (this.CanCastRaycast(out eventData3))
					{
						NKCInputManager.ConsumeHotKeyEvent(this.m_HotkeyEventType);
						this.OnPointerClickEvent(eventData3);
					}
				}
				else if (NKCInputManager.IsHotkeyPressed(this.m_HotkeyEventType) && this.dOnPointerHoldPress != null)
				{
					this.ProcessHold();
					return;
				}
				if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
				{
					NKCUIComHotkeyDisplay.OpenInstance(base.transform, this.m_HotkeyEventType);
					return;
				}
			}
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x0016977C File Offset: 0x0016797C
		private void ProcessHold()
		{
			this.m_fHoldingTime += Time.deltaTime;
			if (this.m_fHoldingTime > this.m_fPressGap)
			{
				this.m_fPressGap *= this.m_fDamping;
				if (this.m_iFastRepeatValue < 1)
				{
					this.m_iFastRepeatValue = 1;
				}
				int num = (this.m_fPressGap < this.m_fPressGapMin) ? this.m_iFastRepeatValue : 1;
				this.m_fPressGap = Mathf.Clamp(this.m_fPressGap, this.m_fPressGapMin, this.m_fPressGapMax);
				this.m_fHoldingTime = 0f;
				for (int i = 0; i < num; i++)
				{
					this.dOnPointerHoldPress();
				}
			}
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00169824 File Offset: 0x00167A24
		public bool CanCastRaycast(out PointerEventData raycastEvent)
		{
			Vector3 vector = NKCCamera.GetSubUICamera().WorldToScreenPoint(base.transform.GetComponent<RectTransform>().GetCenterWorldPos());
			raycastEvent = new PointerEventData(EventSystem.current);
			raycastEvent.position = new Vector2(vector.x, vector.y);
			List<RaycastResult> list = new List<RaycastResult>();
			EventSystem.current.RaycastAll(raycastEvent, list);
			return list.Count > 0 && list[0].gameObject.transform.IsChildOf(base.transform);
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x001698B0 File Offset: 0x00167AB0
		private void OnDisable()
		{
			this.ClearHolding();
			this.SetButtonState(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
			this.m_Scale.StopTracking();
			if (this.m_RectTransform != null)
			{
				this.m_RectTransform.localScale = new Vector3(this.m_fOrgSizeX, this.m_fOrgSizeY, 1f);
			}
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00169910 File Offset: 0x00167B10
		protected void UpdateScale()
		{
			if (this.m_Scale.IsTracking() && this.m_RectTransform != null)
			{
				Vector3 localScale = this.m_RectTransform.localScale;
				localScale.Set(this.m_fOrgSizeX * this.m_Scale.GetNowValue(), this.m_fOrgSizeY * this.m_Scale.GetNowValue(), 1f);
				this.m_RectTransform.localScale = localScale;
			}
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00169980 File Offset: 0x00167B80
		public virtual bool Select(bool bSelect, bool bForce = false, bool bImmediate = false)
		{
			if (this.m_bLock)
			{
				return false;
			}
			if (this.m_bSelect != bSelect || bForce)
			{
				if (!bSelect)
				{
					if (bImmediate)
					{
						this.m_Scale.StopTracking();
						if (this.m_RectTransform != null)
						{
							this.m_RectTransform.localScale = new Vector3(this.m_fOrgSizeX, this.m_fOrgSizeY, 1f);
						}
					}
					else
					{
						this.m_Scale.SetTracking(1f, this.m_fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
					}
					this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Normal);
				}
				else
				{
					if (bImmediate)
					{
						this.m_Scale.StopTracking();
						if (this.m_RectTransform != null)
						{
							this.m_RectTransform.localScale = new Vector3(this.m_fOrgSizeX * this.m_fSelectSize, this.m_fOrgSizeY * this.m_fSelectSize, 1f);
						}
					}
					else
					{
						this.m_Scale.SetTracking(this.m_fSelectSize, this.m_fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
					}
					this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Selected);
				}
				this.m_bSelect = bSelect;
				return true;
			}
			return false;
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00169A7E File Offset: 0x00167C7E
		public void SetLock(bool value, bool bForce = false)
		{
			if (value)
			{
				this.Lock(bForce);
				return;
			}
			this.UnLock(bForce);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00169A92 File Offset: 0x00167C92
		public void Lock(bool bForce = false)
		{
			if (this.m_bLock && !bForce)
			{
				return;
			}
			this.Select(false, false, false);
			this.m_bLock = true;
			this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Locked);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00169AB8 File Offset: 0x00167CB8
		public void UnLock(bool bForce = false)
		{
			if (!this.m_bLock && !bForce)
			{
				return;
			}
			this.m_bLock = false;
			this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Normal);
			this.Select(this.m_bSelect, true, false);
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00169AE3 File Offset: 0x00167CE3
		public NKCUIComStateButtonBase.ButtonState GetButtonState()
		{
			return this.m_eCurrentState;
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x00169AEC File Offset: 0x00167CEC
		protected void SetButtonState(NKCUIComStateButtonBase.ButtonState state)
		{
			this.m_eCurrentState = state;
			if (this.m_bLock)
			{
				this.m_eCurrentState = NKCUIComStateButtonBase.ButtonState.Locked;
			}
			switch (this.m_eCurrentState)
			{
			case NKCUIComStateButtonBase.ButtonState.Normal:
				this.SetObject(NKCUIComStateButtonBase.ButtonState.Normal);
				return;
			case NKCUIComStateButtonBase.ButtonState.Pressed:
				if (this.m_ButtonBG_Pressed == null)
				{
					this.SetObject(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
					return;
				}
				this.SetObject(NKCUIComStateButtonBase.ButtonState.Pressed);
				return;
			case NKCUIComStateButtonBase.ButtonState.Selected:
				if (this.m_ButtonBG_Selected == null)
				{
					this.SetObject(NKCUIComStateButtonBase.ButtonState.Normal);
					return;
				}
				this.SetObject(NKCUIComStateButtonBase.ButtonState.Selected);
				return;
			case NKCUIComStateButtonBase.ButtonState.Locked:
				if (this.m_ButtonBG_Locked == null)
				{
					this.SetObject(NKCUIComStateButtonBase.ButtonState.Normal);
					return;
				}
				this.SetObject(NKCUIComStateButtonBase.ButtonState.Locked);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x00169B9C File Offset: 0x00167D9C
		private void SetObject(NKCUIComStateButtonBase.ButtonState state)
		{
			switch (this.m_ePressedEffectIsAdditional)
			{
			case NKCUIComStateButtonBase.PressedEffectType.Single:
				NKCUtil.SetGameobjectActive(this.m_ButtonBG_Normal, state == NKCUIComStateButtonBase.ButtonState.Normal);
				NKCUtil.SetGameobjectActive(this.m_ButtonBG_Pressed, state == NKCUIComStateButtonBase.ButtonState.Pressed);
				NKCUtil.SetGameobjectActive(this.m_ButtonBG_Selected, state == NKCUIComStateButtonBase.ButtonState.Selected);
				NKCUtil.SetGameobjectActive(this.m_ButtonBG_Locked, state == NKCUIComStateButtonBase.ButtonState.Locked);
				return;
			case NKCUIComStateButtonBase.PressedEffectType.Additional:
				if (state != NKCUIComStateButtonBase.ButtonState.Pressed)
				{
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Normal, state == NKCUIComStateButtonBase.ButtonState.Normal);
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Pressed, false);
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Selected, state == NKCUIComStateButtonBase.ButtonState.Selected);
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Locked, state == NKCUIComStateButtonBase.ButtonState.Locked);
					return;
				}
				if (!this.m_bLock)
				{
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Pressed, true);
					return;
				}
				break;
			case NKCUIComStateButtonBase.PressedEffectType.Effect:
				if (state == NKCUIComStateButtonBase.ButtonState.Pressed)
				{
					if (!this.m_bLock)
					{
						NKCUtil.SetGameobjectActive(this.m_ButtonBG_Pressed, false);
						NKCUtil.SetGameobjectActive(this.m_ButtonBG_Pressed, true);
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Normal, state == NKCUIComStateButtonBase.ButtonState.Normal);
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Selected, state == NKCUIComStateButtonBase.ButtonState.Selected);
					NKCUtil.SetGameobjectActive(this.m_ButtonBG_Locked, state == NKCUIComStateButtonBase.ButtonState.Locked);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x00169CA8 File Offset: 0x00167EA8
		private void MakeTempRayCaster()
		{
			if (this.m_objTempRaycaster == null)
			{
				RectTransform component = base.GetComponent<RectTransform>();
				if (component != null)
				{
					this.m_objTempRaycaster = new GameObject("TempRaycaster");
					RectTransform rectTransform = this.m_objTempRaycaster.AddComponent<RectTransform>();
					rectTransform.SetParent(base.transform, false);
					rectTransform.anchorMin = Vector2.zero;
					rectTransform.anchorMax = Vector2.one;
					rectTransform.offsetMin = component.GetSize() * -0.2f;
					rectTransform.offsetMax = component.GetSize() * 0.2f;
					this.m_objTempRaycaster.AddComponent<NKCUIComRaycastTarget>().raycastTarget = true;
				}
			}
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x00169D50 File Offset: 0x00167F50
		private void ClearTempRayCaster()
		{
			if (this.m_objTempRaycaster != null)
			{
				UnityEngine.Object.Destroy(this.m_objTempRaycaster);
				this.m_objTempRaycaster = null;
			}
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00169D74 File Offset: 0x00167F74
		public void SetTitleText(string text)
		{
			if (this.m_aTitle == null)
			{
				return;
			}
			for (int i = 0; i < this.m_aTitle.Length; i++)
			{
				NKCUtil.SetLabelText(this.m_aTitle[i], text);
			}
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x00169DAB File Offset: 0x00167FAB
		public void SetHotkey(HotkeyEventType hotkey, NKCUIBase uiBase = null, bool bUpDownEvent = false)
		{
			this.m_HotkeyEventType = hotkey;
			if (uiBase != null)
			{
				this.m_uiRoot = uiBase;
			}
			this.m_bUseHotkeyUpDownEvent = bUpDownEvent;
		}

		// Token: 0x04003A07 RID: 14855
		protected NKCUIComStateButtonBase.ButtonState m_eCurrentState;

		// Token: 0x04003A08 RID: 14856
		private float m_fOrgSizeX = 1f;

		// Token: 0x04003A09 RID: 14857
		private float m_fOrgSizeY = 1f;

		// Token: 0x04003A0A RID: 14858
		public float m_fTouchSize = 0.9f;

		// Token: 0x04003A0B RID: 14859
		public float m_fSelectSize = 1f;

		// Token: 0x04003A0C RID: 14860
		public float m_fTrackingTime = 0.1f;

		// Token: 0x04003A0D RID: 14861
		public HotkeyEventType m_HotkeyEventType;

		// Token: 0x04003A0E RID: 14862
		public bool m_bUseHotkeyUpDownEvent;

		// Token: 0x04003A0F RID: 14863
		public bool m_bLock;

		// Token: 0x04003A10 RID: 14864
		public bool m_bGetCallbackWhileLocked;

		// Token: 0x04003A11 RID: 14865
		[FormerlySerializedAs("m_bChecked")]
		public bool m_bSelect;

		// Token: 0x04003A12 RID: 14866
		public bool m_bSelectByClick;

		// Token: 0x04003A13 RID: 14867
		[FormerlySerializedAs("m_ButtonBG_Off")]
		public GameObject m_ButtonBG_Normal;

		// Token: 0x04003A14 RID: 14868
		[FormerlySerializedAs("m_ButtonBG_On")]
		public GameObject m_ButtonBG_Selected;

		// Token: 0x04003A15 RID: 14869
		[FormerlySerializedAs("m_ButtonBG_Lock")]
		public GameObject m_ButtonBG_Locked;

		// Token: 0x04003A16 RID: 14870
		public GameObject m_ButtonBG_Pressed;

		// Token: 0x04003A17 RID: 14871
		public NKCUIComStateButtonBase.PressedEffectType m_ePressedEffectIsAdditional;

		// Token: 0x04003A18 RID: 14872
		public int m_DataInt;

		// Token: 0x04003A19 RID: 14873
		protected RectTransform m_RectTransform;

		// Token: 0x04003A1A RID: 14874
		private NKMTrackingFloat m_Scale = new NKMTrackingFloat();

		// Token: 0x04003A1B RID: 14875
		public string m_SoundForPointClick = "FX_UI_BUTTON_SELECT";

		// Token: 0x04003A1C RID: 14876
		public Text[] m_aTitle;

		// Token: 0x04003A1D RID: 14877
		private GameObject m_objTempRaycaster;

		// Token: 0x04003A1E RID: 14878
		public float m_fDelayHold = 0.3f;

		// Token: 0x04003A1F RID: 14879
		public NKCUIComStateButtonBase.OnPointerHolding dOnPointerHolding;

		// Token: 0x04003A20 RID: 14880
		[Header("버튼 홀드 이벤트 호출 속도 조절")]
		public float m_fPressGapMax = 0.4f;

		// Token: 0x04003A21 RID: 14881
		public float m_fPressGapMin = 0.01f;

		// Token: 0x04003A22 RID: 14882
		public float m_fDamping = 0.8f;

		// Token: 0x04003A23 RID: 14883
		public int m_iFastRepeatValue = 1;

		// Token: 0x04003A24 RID: 14884
		public NKCUIComStateButtonBase.OnPointerHoldPress dOnPointerHoldPress;

		// Token: 0x04003A25 RID: 14885
		private float m_fHoldingTime;

		// Token: 0x04003A26 RID: 14886
		private float m_fPressGap;

		// Token: 0x04003A27 RID: 14887
		private Vector2 m_touchPos = Vector2.zero;

		// Token: 0x04003A28 RID: 14888
		private NKCUIBase m_uiRoot;

		// Token: 0x0200142B RID: 5163
		public enum ButtonState
		{
			// Token: 0x04009DA1 RID: 40353
			Normal,
			// Token: 0x04009DA2 RID: 40354
			Pressed,
			// Token: 0x04009DA3 RID: 40355
			Selected,
			// Token: 0x04009DA4 RID: 40356
			Locked
		}

		// Token: 0x0200142C RID: 5164
		public enum PressedEffectType
		{
			// Token: 0x04009DA6 RID: 40358
			Single,
			// Token: 0x04009DA7 RID: 40359
			Additional,
			// Token: 0x04009DA8 RID: 40360
			Effect
		}

		// Token: 0x0200142D RID: 5165
		// (Invoke) Token: 0x0600A808 RID: 43016
		public delegate void OnPointerHolding();

		// Token: 0x0200142E RID: 5166
		// (Invoke) Token: 0x0600A80C RID: 43020
		public delegate void OnPointerHoldPress();
	}
}
