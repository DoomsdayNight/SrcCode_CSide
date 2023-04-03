using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x020002AE RID: 686
	[AddComponentMenu("UI/Extensions/Extensions Toggle", 31)]
	[RequireComponent(typeof(RectTransform))]
	public class ExtensionsToggle : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0002987C File Offset: 0x00027A7C
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x00029884 File Offset: 0x00027A84
		public ExtensionsToggleGroup Group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.m_Group = value;
				this.SetToggleGroup(this.m_Group, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000298A1 File Offset: 0x00027AA1
		protected ExtensionsToggle()
		{
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000298C6 File Offset: 0x00027AC6
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000298C8 File Offset: 0x00027AC8
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000298CA File Offset: 0x00027ACA
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000298CC File Offset: 0x00027ACC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000298E8 File Offset: 0x00027AE8
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000298F8 File Offset: 0x00027AF8
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				bool flag = !Mathf.Approximately(this.graphic.canvasRenderer.GetColor().a, 0f);
				if (this.m_IsOn != flag)
				{
					this.m_IsOn = flag;
					this.Set(!flag);
				}
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00029958 File Offset: 0x00027B58
		private void SetToggleGroup(ExtensionsToggleGroup newGroup, bool setMemberValue)
		{
			ExtensionsToggleGroup group = this.m_Group;
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (this.m_Group != null && this.IsActive())
			{
				this.m_Group.RegisterToggle(this);
			}
			if (newGroup != null && newGroup != group && this.IsOn && this.IsActive())
			{
				this.m_Group.NotifyToggleOn(this);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000299E0 File Offset: 0x00027BE0
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x000299E8 File Offset: 0x00027BE8
		public bool IsOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000299F1 File Offset: 0x00027BF1
		private void Set(bool value)
		{
			this.Set(value, true);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000299FC File Offset: 0x00027BFC
		private void Set(bool value, bool sendCallback)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.AllowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this);
			}
			this.PlayEffect(this.toggleTransition == ExtensionsToggle.ToggleTransition.None);
			if (sendCallback)
			{
				this.onValueChanged.Invoke(this.m_IsOn);
				this.onToggleChanged.Invoke(this);
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00029A94 File Offset: 0x00027C94
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha(this.m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00029AD4 File Offset: 0x00027CD4
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00029ADD File Offset: 0x00027CDD
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.IsOn = !this.IsOn;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00029AFF File Offset: 0x00027CFF
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00029B10 File Offset: 0x00027D10
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00029B18 File Offset: 0x00027D18
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040009A2 RID: 2466
		public string UniqueID;

		// Token: 0x040009A3 RID: 2467
		public ExtensionsToggle.ToggleTransition toggleTransition = ExtensionsToggle.ToggleTransition.Fade;

		// Token: 0x040009A4 RID: 2468
		public Graphic graphic;

		// Token: 0x040009A5 RID: 2469
		[SerializeField]
		private ExtensionsToggleGroup m_Group;

		// Token: 0x040009A6 RID: 2470
		[Tooltip("Use this event if you only need the bool state of the toggle that was changed")]
		public ExtensionsToggle.ToggleEvent onValueChanged = new ExtensionsToggle.ToggleEvent();

		// Token: 0x040009A7 RID: 2471
		[Tooltip("Use this event if you need access to the toggle that was changed")]
		public ExtensionsToggle.ToggleEventObject onToggleChanged = new ExtensionsToggle.ToggleEventObject();

		// Token: 0x040009A8 RID: 2472
		[FormerlySerializedAs("m_IsActive")]
		[Tooltip("Is the toggle currently on or off?")]
		[SerializeField]
		private bool m_IsOn;

		// Token: 0x0200111C RID: 4380
		public enum ToggleTransition
		{
			// Token: 0x0400919B RID: 37275
			None,
			// Token: 0x0400919C RID: 37276
			Fade
		}

		// Token: 0x0200111D RID: 4381
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}

		// Token: 0x0200111E RID: 4382
		[Serializable]
		public class ToggleEventObject : UnityEvent<ExtensionsToggle>
		{
		}
	}
}
