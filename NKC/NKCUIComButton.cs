using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x02000751 RID: 1873
	public class NKCUIComButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler, IPointerExitHandler
	{
		// Token: 0x06004AF2 RID: 19186 RVA: 0x0016737E File Offset: 0x0016557E
		private void Start()
		{
			this.m_uiRoot = NKCUIManager.FindRootUIBase(base.transform);
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x00167391 File Offset: 0x00165591
		public void SetLinkButton(NKCUIComButton cNKCUIComButton)
		{
			this.m_LinkButton = cNKCUIComButton;
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x0016739C File Offset: 0x0016559C
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
			this.m_touchPos = eventData.position;
			if (this.PointerDown != null)
			{
				this.PointerDown.Invoke(eventData);
			}
			if (this.m_LinkButton != null)
			{
				this.m_LinkButton.OnPointerDown(eventData);
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00167430 File Offset: 0x00165630
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
			if (this.PointerUp != null)
			{
				this.PointerUp.Invoke();
			}
			if (this.m_LinkButton != null)
			{
				this.m_LinkButton.OnPointerUp(eventData);
			}
			this.ClearTempRayCaster();
			this.ClearHolding();
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x001674B0 File Offset: 0x001656B0
		public void OnPointerClick(PointerEventData eventData)
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
				if (this.PointerClick != null)
				{
					this.PointerClick.Invoke();
				}
				if (this.m_LinkButton != null)
				{
					this.m_LinkButton.OnPointerClick(eventData);
					return;
				}
			}
			else if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Locked)
			{
				if (this.PointerClick != null)
				{
					this.PointerClick.Invoke();
				}
				if (this.m_LinkButton != null)
				{
					this.m_LinkButton.OnPointerClick(eventData);
				}
			}
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00167586 File Offset: 0x00165786
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				this.SetButtonState(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
			}
			this.ClearHolding();
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x001675A9 File Offset: 0x001657A9
		private void StartHolding(Vector2 touchPos)
		{
			if (this.dOnPointerHolding == null)
			{
				return;
			}
			NKCUIHoldLoading.Instance.Open(touchPos, -1f);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x001675C4 File Offset: 0x001657C4
		private void OnHoldEvent()
		{
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				this.SetButtonState(this.m_bSelect ? NKCUIComStateButtonBase.ButtonState.Selected : NKCUIComStateButtonBase.ButtonState.Normal);
			}
			NKCUIComButton.OnPointerHolding onPointerHolding = this.dOnPointerHolding;
			if (onPointerHolding != null)
			{
				onPointerHolding();
			}
			this.ClearHolding();
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x001675F8 File Offset: 0x001657F8
		private void ClearHolding()
		{
			if (NKCUIHoldLoading.IsOpen)
			{
				NKCUIHoldLoading.Instance.Close();
			}
			this.m_fHoldingTime = 0f;
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00167618 File Offset: 0x00165818
		public void UpdateOrgSize()
		{
			this.m_RectTransform = base.gameObject.GetComponentInChildren<RectTransform>();
			this.m_fOrgSizeX = this.m_RectTransform.localScale.x;
			this.m_fOrgSizeY = this.m_RectTransform.localScale.y;
			this.m_Scale.SetNowValue(1f);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00167672 File Offset: 0x00165872
		private void Awake()
		{
			this.Select(this.m_bSelect, true, false);
			this.UpdateOrgSize();
			this.UpdateScale();
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0016768E File Offset: 0x0016588E
		private void OnDisable()
		{
			this.m_Scale.StopTracking();
			if (this.m_RectTransform != null)
			{
				this.m_RectTransform.localScale = new Vector3(this.m_fOrgSizeX, this.m_fOrgSizeY, 1f);
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x001676CC File Offset: 0x001658CC
		public void Update()
		{
			this.m_Scale.Update(Time.deltaTime);
			this.UpdateScale();
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				if (this.dOnPointerHolding == null)
				{
					return;
				}
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
			}
			if (!this.m_bLock && this.m_HotkeyEventType != HotkeyEventType.None)
			{
				if (!NKCUIManager.IsTopmostUI(this.m_uiRoot))
				{
					return;
				}
				if (NKCInputManager.CheckHotKeyEvent(this.m_HotkeyEventType))
				{
					Vector3 vector = NKCCamera.GetSubUICamera().WorldToScreenPoint(base.transform.GetComponent<RectTransform>().GetCenterWorldPos());
					PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
					pointerEventData.position = new Vector2(vector.x, vector.y);
					List<RaycastResult> list = new List<RaycastResult>();
					EventSystem.current.RaycastAll(pointerEventData, list);
					if (list.Count > 0 && list[0].gameObject.transform.IsChildOf(base.transform))
					{
						NKCInputManager.ConsumeHotKeyEvent(this.m_HotkeyEventType);
						if (this.PointerClick != null)
						{
							this.PointerClick.Invoke();
						}
						if (this.m_LinkButton != null)
						{
							this.m_LinkButton.OnPointerClick(pointerEventData);
						}
					}
				}
				if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
				{
					NKCUIComHotkeyDisplay.OpenInstance(base.transform, this.m_HotkeyEventType);
					return;
				}
			}
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00167858 File Offset: 0x00165A58
		private void UpdateScale()
		{
			if (this.m_Scale.IsTracking() && this.m_RectTransform != null)
			{
				Vector3 localScale = this.m_RectTransform.localScale;
				localScale.Set(this.m_fOrgSizeX * this.m_Scale.GetNowValue(), this.m_fOrgSizeY * this.m_Scale.GetNowValue(), 1f);
				this.m_RectTransform.localScale = localScale;
			}
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x001678C8 File Offset: 0x00165AC8
		public void Select(bool bSelect, bool bForce = false, bool bImmediate = false)
		{
			if (this.m_bLock)
			{
				return;
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
					if (this.m_ButtonBG_On != null && this.m_ButtonBG_On.activeSelf)
					{
						this.m_ButtonBG_On.SetActive(false);
					}
					if (this.m_ButtonBG_Off != null && !this.m_ButtonBG_Off.activeSelf)
					{
						this.m_ButtonBG_Off.SetActive(true);
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
					if (this.m_ButtonBG_On != null && !this.m_ButtonBG_On.activeSelf)
					{
						this.m_ButtonBG_On.SetActive(true);
					}
					if (this.m_ButtonBG_Off != null && this.m_ButtonBG_Off.activeSelf)
					{
						this.m_ButtonBG_Off.SetActive(false);
					}
					this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Selected);
				}
				this.m_bSelect = bSelect;
			}
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x00167A64 File Offset: 0x00165C64
		public void Lock()
		{
			if (this.m_bLock)
			{
				return;
			}
			this.Select(false, false, false);
			if (this.m_ButtonBG_On != null && this.m_ButtonBG_On.activeSelf)
			{
				this.m_ButtonBG_On.SetActive(false);
			}
			if (this.m_ButtonBG_Off != null && this.m_ButtonBG_Off.activeSelf)
			{
				this.m_ButtonBG_Off.SetActive(false);
			}
			if (this.m_ButtonBG_UnLock != null && this.m_ButtonBG_UnLock.activeSelf)
			{
				this.m_ButtonBG_UnLock.SetActive(false);
			}
			if (this.m_ButtonBG_Lock != null && !this.m_ButtonBG_Lock.activeSelf)
			{
				this.m_ButtonBG_Lock.SetActive(true);
			}
			this.m_bLock = true;
			this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Locked);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00167B30 File Offset: 0x00165D30
		public void UnLock()
		{
			if (!this.m_bLock)
			{
				return;
			}
			if (this.m_ButtonBG_UnLock != null && !this.m_ButtonBG_UnLock.activeSelf)
			{
				this.m_ButtonBG_UnLock.SetActive(true);
			}
			if (this.m_ButtonBG_Lock != null && this.m_ButtonBG_Lock.activeSelf)
			{
				this.m_ButtonBG_Lock.SetActive(false);
			}
			this.m_bLock = false;
			this.SetButtonState(NKCUIComStateButtonBase.ButtonState.Normal);
			this.Select(this.m_bSelect, true, false);
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00167BB0 File Offset: 0x00165DB0
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

		// Token: 0x06004B04 RID: 19204 RVA: 0x00167C58 File Offset: 0x00165E58
		private void ClearTempRayCaster()
		{
			if (this.m_objTempRaycaster != null)
			{
				UnityEngine.Object.Destroy(this.m_objTempRaycaster);
				this.m_objTempRaycaster = null;
			}
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00167C7A File Offset: 0x00165E7A
		private void SetButtonState(NKCUIComStateButtonBase.ButtonState state)
		{
			this.m_eCurrentState = state;
		}

		// Token: 0x0400399D RID: 14749
		public HotkeyEventType m_HotkeyEventType;

		// Token: 0x0400399E RID: 14750
		public NKCUnityEvent PointerDown = new NKCUnityEvent();

		// Token: 0x0400399F RID: 14751
		public UnityEvent PointerUp;

		// Token: 0x040039A0 RID: 14752
		public UnityEvent PointerClick;

		// Token: 0x040039A1 RID: 14753
		private float m_fOrgSizeX = 1f;

		// Token: 0x040039A2 RID: 14754
		private float m_fOrgSizeY = 1f;

		// Token: 0x040039A3 RID: 14755
		public float m_fTouchSize = 0.9f;

		// Token: 0x040039A4 RID: 14756
		public float m_fSelectSize = 1.05f;

		// Token: 0x040039A5 RID: 14757
		public float m_fTrackingTime = 0.1f;

		// Token: 0x040039A6 RID: 14758
		public bool m_bLock;

		// Token: 0x040039A7 RID: 14759
		public bool m_bSelect;

		// Token: 0x040039A8 RID: 14760
		public bool m_bSelectByClick = true;

		// Token: 0x040039A9 RID: 14761
		public GameObject m_ButtonBG_On;

		// Token: 0x040039AA RID: 14762
		public GameObject m_ButtonBG_Off;

		// Token: 0x040039AB RID: 14763
		public GameObject m_ButtonBG_Lock;

		// Token: 0x040039AC RID: 14764
		public GameObject m_ButtonBG_UnLock;

		// Token: 0x040039AD RID: 14765
		private GameObject m_objTempRaycaster;

		// Token: 0x040039AE RID: 14766
		public int m_DataInt;

		// Token: 0x040039AF RID: 14767
		private RectTransform m_RectTransform;

		// Token: 0x040039B0 RID: 14768
		private NKMTrackingFloat m_Scale = new NKMTrackingFloat();

		// Token: 0x040039B1 RID: 14769
		public string m_SoundForPointClick = "FX_UI_BUTTON_SELECT";

		// Token: 0x040039B2 RID: 14770
		private NKCUIComButton m_LinkButton;

		// Token: 0x040039B3 RID: 14771
		protected NKCUIComStateButtonBase.ButtonState m_eCurrentState;

		// Token: 0x040039B4 RID: 14772
		public float m_fDelayHold = 0.3f;

		// Token: 0x040039B5 RID: 14773
		public NKCUIComButton.OnPointerHolding dOnPointerHolding;

		// Token: 0x040039B6 RID: 14774
		private float m_fHoldingTime;

		// Token: 0x040039B7 RID: 14775
		private Vector2 m_touchPos = Vector2.zero;

		// Token: 0x040039B8 RID: 14776
		private NKCUIBase m_uiRoot;

		// Token: 0x02001428 RID: 5160
		// (Invoke) Token: 0x0600A801 RID: 43009
		public delegate void OnPointerHolding();
	}
}
