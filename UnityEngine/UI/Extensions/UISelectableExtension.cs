using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000349 RID: 841
	[AddComponentMenu("UI/Extensions/UI Selectable Extension")]
	[RequireComponent(typeof(Selectable))]
	public class UISelectableExtension : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x060013D4 RID: 5076 RVA: 0x0004A145 File Offset: 0x00048345
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.OnButtonPress != null)
			{
				this.OnButtonPress.Invoke(eventData.button);
			}
			this._pressed = true;
			this._heldEventData = eventData;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004A16E File Offset: 0x0004836E
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (this.OnButtonRelease != null)
			{
				this.OnButtonRelease.Invoke(eventData.button);
			}
			this._pressed = false;
			this._heldEventData = null;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004A197 File Offset: 0x00048397
		private void Update()
		{
			if (!this._pressed)
			{
				return;
			}
			if (this.OnButtonHeld != null)
			{
				this.OnButtonHeld.Invoke(this._heldEventData.button);
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004A1C0 File Offset: 0x000483C0
		public void TestClicked()
		{
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004A1C2 File Offset: 0x000483C2
		public void TestPressed()
		{
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004A1C4 File Offset: 0x000483C4
		public void TestReleased()
		{
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004A1C6 File Offset: 0x000483C6
		public void TestHold()
		{
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004A1C8 File Offset: 0x000483C8
		private void OnDisable()
		{
			this._pressed = false;
		}

		// Token: 0x04000DAD RID: 3501
		[Tooltip("Event that fires when a button is initially pressed down")]
		public UISelectableExtension.UIButtonEvent OnButtonPress;

		// Token: 0x04000DAE RID: 3502
		[Tooltip("Event that fires when a button is released")]
		public UISelectableExtension.UIButtonEvent OnButtonRelease;

		// Token: 0x04000DAF RID: 3503
		[Tooltip("Event that continually fires while a button is held down")]
		public UISelectableExtension.UIButtonEvent OnButtonHeld;

		// Token: 0x04000DB0 RID: 3504
		private bool _pressed;

		// Token: 0x04000DB1 RID: 3505
		private PointerEventData _heldEventData;

		// Token: 0x02001176 RID: 4470
		[Serializable]
		public class UIButtonEvent : UnityEvent<PointerEventData.InputButton>
		{
		}
	}
}
