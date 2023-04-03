using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000765 RID: 1893
	public class NKCUIComToggle : NKCUIComStateButtonBase
	{
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x00169F45 File Offset: 0x00168145
		public bool m_bChecked
		{
			get
			{
				return this.m_bSelect;
			}
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x00169F4D File Offset: 0x0016814D
		public void SetbReverseSeqCallbackCall(bool bSet)
		{
			this.m_bReverseSeqCallbackCall = bSet;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00169F56 File Offset: 0x00168156
		protected override void OnPointerDownEvent(PointerEventData eventData)
		{
			if (this.PointerDown != null)
			{
				this.PointerDown.Invoke(eventData);
			}
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x00169F6C File Offset: 0x0016816C
		protected override void OnPointerUpEvent(PointerEventData eventData)
		{
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x00169F6E File Offset: 0x0016816E
		protected override void OnPointerClickEvent(PointerEventData eventData)
		{
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00169F70 File Offset: 0x00168170
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (this.m_bSelect && this.m_ToggleGroup != null && !this.m_ToggleGroup.m_bAllowSwitchOff)
			{
				base.SetButtonState(NKCUIComStateButtonBase.ButtonState.Selected);
				return;
			}
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Pressed)
			{
				if (!string.IsNullOrEmpty(this.m_SoundForPointClick))
				{
					NKCSoundManager.PlaySound(this.m_SoundForPointClick, 1f, 0f, 0f, false, 0f, false, 0f);
				}
				this.Select(!this.m_bSelect, false, false);
				return;
			}
			if (this.m_eCurrentState == NKCUIComStateButtonBase.ButtonState.Locked && this.m_bGetCallbackWhileLocked)
			{
				Toggle.ToggleEvent onValueChanged = this.OnValueChanged;
				if (onValueChanged != null)
				{
					onValueChanged.Invoke(this.m_bSelect);
				}
				NKCUIComToggle.ValueChangedWithData onValueChangedWithData = this.OnValueChangedWithData;
				if (onValueChangedWithData == null)
				{
					return;
				}
				onValueChangedWithData(this.m_bSelect, this.m_DataInt);
			}
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0016A03C File Offset: 0x0016823C
		private void Awake()
		{
			if (this.m_ToggleGroup != null)
			{
				this.m_ToggleGroup.RegisterToggle(this);
			}
			this.Select(this.m_bSelect, true, false);
			base.UpdateOrgSize();
			base.UpdateScale();
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0016A073 File Offset: 0x00168273
		public void SetToggleGroup(NKCUIComToggleGroup group)
		{
			if (this.m_ToggleGroup != null)
			{
				this.m_ToggleGroup.DeregisterToggle(this);
			}
			this.m_ToggleGroup = group;
			if (this.m_ToggleGroup != null)
			{
				this.m_ToggleGroup.RegisterToggle(this);
			}
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0016A0B0 File Offset: 0x001682B0
		public override bool Select(bool bSelect, bool bForce = false, bool bImmediate = false)
		{
			if (base.Select(bSelect, bForce, bImmediate))
			{
				if (this.m_bReverseSeqCallbackCall)
				{
					if (bSelect && this.m_ToggleGroup != null)
					{
						this.m_ToggleGroup.OnCheckOneToggle(this);
					}
					if (!bForce)
					{
						Toggle.ToggleEvent onValueChanged = this.OnValueChanged;
						if (onValueChanged != null)
						{
							onValueChanged.Invoke(this.m_bSelect);
						}
						NKCUIComToggle.ValueChangedWithData onValueChangedWithData = this.OnValueChangedWithData;
						if (onValueChangedWithData != null)
						{
							onValueChangedWithData(this.m_bSelect, this.m_DataInt);
						}
					}
				}
				else
				{
					if (!bForce)
					{
						Toggle.ToggleEvent onValueChanged2 = this.OnValueChanged;
						if (onValueChanged2 != null)
						{
							onValueChanged2.Invoke(this.m_bSelect);
						}
						NKCUIComToggle.ValueChangedWithData onValueChangedWithData2 = this.OnValueChangedWithData;
						if (onValueChangedWithData2 != null)
						{
							onValueChangedWithData2(this.m_bSelect, this.m_DataInt);
						}
					}
					if (bSelect && this.m_ToggleGroup != null)
					{
						this.m_ToggleGroup.OnCheckOneToggle(this);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04003A2F RID: 14895
		public NKCUnityEvent PointerDown = new NKCUnityEvent();

		// Token: 0x04003A30 RID: 14896
		public Toggle.ToggleEvent OnValueChanged;

		// Token: 0x04003A31 RID: 14897
		public NKCUIComToggle.ValueChangedWithData OnValueChangedWithData;

		// Token: 0x04003A32 RID: 14898
		public NKCUIComToggleGroup m_ToggleGroup;

		// Token: 0x04003A33 RID: 14899
		private bool m_bReverseSeqCallbackCall;

		// Token: 0x0200142F RID: 5167
		// (Invoke) Token: 0x0600A810 RID: 43024
		public delegate void ValueChangedWithData(bool value, int data);
	}
}
