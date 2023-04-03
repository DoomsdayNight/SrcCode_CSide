using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002BB RID: 699
	[AddComponentMenu("UI/Extensions/Cooldown Button")]
	public class CooldownButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0002CA9E File Offset: 0x0002AC9E
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0002CAA6 File Offset: 0x0002ACA6
		public float CooldownTimeout
		{
			get
			{
				return this.cooldownTimeout;
			}
			set
			{
				this.cooldownTimeout = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0002CAAF File Offset: 0x0002ACAF
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x0002CAB7 File Offset: 0x0002ACB7
		public float CooldownSpeed
		{
			get
			{
				return this.cooldownSpeed;
			}
			set
			{
				this.cooldownSpeed = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002CAC0 File Offset: 0x0002ACC0
		public bool CooldownInEffect
		{
			get
			{
				return this.cooldownInEffect;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
		public bool CooldownActive
		{
			get
			{
				return this.cooldownActive;
			}
			set
			{
				this.cooldownActive = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0002CAD9 File Offset: 0x0002ACD9
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x0002CAE1 File Offset: 0x0002ACE1
		public float CooldownTimeElapsed
		{
			get
			{
				return this.cooldownTimeElapsed;
			}
			set
			{
				this.cooldownTimeElapsed = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002CAEA File Offset: 0x0002ACEA
		public float CooldownTimeRemaining
		{
			get
			{
				return this.cooldownTimeRemaining;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0002CAF2 File Offset: 0x0002ACF2
		public int CooldownPercentRemaining
		{
			get
			{
				return this.cooldownPercentRemaining;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0002CAFA File Offset: 0x0002ACFA
		public int CooldownPercentComplete
		{
			get
			{
				return this.cooldownPercentComplete;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002CB04 File Offset: 0x0002AD04
		private void Update()
		{
			if (this.CooldownActive)
			{
				this.cooldownTimeRemaining -= Time.deltaTime * this.cooldownSpeed;
				this.cooldownTimeElapsed = this.CooldownTimeout - this.CooldownTimeRemaining;
				if (this.cooldownTimeRemaining < 0f)
				{
					this.StopCooldown();
					return;
				}
				this.cooldownPercentRemaining = (int)(100f * this.cooldownTimeRemaining * this.CooldownTimeout / 100f);
				this.cooldownPercentComplete = (int)((this.CooldownTimeout - this.cooldownTimeRemaining) / this.CooldownTimeout * 100f);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002CB9D File Offset: 0x0002AD9D
		public void PauseCooldown()
		{
			if (this.CooldownInEffect)
			{
				this.CooldownActive = false;
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002CBAE File Offset: 0x0002ADAE
		public void RestartCooldown()
		{
			if (this.CooldownInEffect)
			{
				this.CooldownActive = true;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002CBC0 File Offset: 0x0002ADC0
		public void StartCooldown()
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			this.buttonSource = pointerEventData;
			this.OnCooldownStart.Invoke(pointerEventData.button);
			this.cooldownTimeRemaining = this.cooldownTimeout;
			this.CooldownActive = (this.cooldownInEffect = true);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		public void StopCooldown()
		{
			this.cooldownTimeElapsed = this.CooldownTimeout;
			this.cooldownTimeRemaining = 0f;
			this.cooldownPercentRemaining = 0;
			this.cooldownPercentComplete = 100;
			this.cooldownActive = (this.cooldownInEffect = false);
			if (this.OnCoolDownFinish != null)
			{
				this.OnCoolDownFinish.Invoke(this.buttonSource.button);
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002CC70 File Offset: 0x0002AE70
		public void CancelCooldown()
		{
			this.cooldownActive = (this.cooldownInEffect = false);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002CC90 File Offset: 0x0002AE90
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			this.buttonSource = eventData;
			if (this.CooldownInEffect && this.OnButtonClickDuringCooldown != null)
			{
				this.OnButtonClickDuringCooldown.Invoke(eventData.button);
			}
			if (!this.CooldownInEffect)
			{
				if (this.OnCooldownStart != null)
				{
					this.OnCooldownStart.Invoke(eventData.button);
				}
				this.cooldownTimeRemaining = this.cooldownTimeout;
				this.cooldownActive = (this.cooldownInEffect = true);
			}
		}

		// Token: 0x04000A30 RID: 2608
		[SerializeField]
		private float cooldownTimeout;

		// Token: 0x04000A31 RID: 2609
		[SerializeField]
		private float cooldownSpeed = 1f;

		// Token: 0x04000A32 RID: 2610
		[SerializeField]
		[ReadOnly]
		private bool cooldownActive;

		// Token: 0x04000A33 RID: 2611
		[SerializeField]
		[ReadOnly]
		private bool cooldownInEffect;

		// Token: 0x04000A34 RID: 2612
		[SerializeField]
		[ReadOnly]
		private float cooldownTimeElapsed;

		// Token: 0x04000A35 RID: 2613
		[SerializeField]
		[ReadOnly]
		private float cooldownTimeRemaining;

		// Token: 0x04000A36 RID: 2614
		[SerializeField]
		[ReadOnly]
		private int cooldownPercentRemaining;

		// Token: 0x04000A37 RID: 2615
		[SerializeField]
		[ReadOnly]
		private int cooldownPercentComplete;

		// Token: 0x04000A38 RID: 2616
		private PointerEventData buttonSource;

		// Token: 0x04000A39 RID: 2617
		[Tooltip("Event that fires when a button is initially pressed down")]
		public CooldownButton.CooldownButtonEvent OnCooldownStart;

		// Token: 0x04000A3A RID: 2618
		[Tooltip("Event that fires when a button is released")]
		public CooldownButton.CooldownButtonEvent OnButtonClickDuringCooldown;

		// Token: 0x04000A3B RID: 2619
		[Tooltip("Event that continually fires while a button is held down")]
		public CooldownButton.CooldownButtonEvent OnCoolDownFinish;

		// Token: 0x0200112E RID: 4398
		[Serializable]
		public class CooldownButtonEvent : UnityEvent<PointerEventData.InputButton>
		{
		}
	}
}
