using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CF RID: 207
	[TaskDescription("Returns success as soon as the event specified by eventName has been received.")]
	[TaskIcon("{SkinColor}HasReceivedEventIcon.png")]
	public class HasReceivedEvent : Conditional
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00018900 File Offset: 0x00016B00
		public override void OnStart()
		{
			if (!this.registered)
			{
				base.Owner.RegisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
				base.Owner.RegisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
				base.Owner.RegisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
				base.Owner.RegisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
				this.registered = true;
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000189A7 File Offset: 0x00016BA7
		public override TaskStatus OnUpdate()
		{
			if (!this.eventReceived)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000189B4 File Offset: 0x00016BB4
		public override void OnEnd()
		{
			if (this.eventReceived)
			{
				base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
				this.registered = false;
			}
			this.eventReceived = false;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00018A62 File Offset: 0x00016C62
		private void ReceivedEvent()
		{
			this.eventReceived = true;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00018A6B File Offset: 0x00016C6B
		private void ReceivedEvent(object arg1)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00018A94 File Offset: 0x00016C94
		private void ReceivedEvent(object arg1, object arg2)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
			if (this.storedValue2 != null && !this.storedValue2.IsNone)
			{
				this.storedValue2.SetValue(arg2);
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00018AEC File Offset: 0x00016CEC
		private void ReceivedEvent(object arg1, object arg2, object arg3)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
			if (this.storedValue2 != null && !this.storedValue2.IsNone)
			{
				this.storedValue2.SetValue(arg2);
			}
			if (this.storedValue3 != null && !this.storedValue3.IsNone)
			{
				this.storedValue3.SetValue(arg3);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00018B64 File Offset: 0x00016D64
		public override void OnBehaviorComplete()
		{
			base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
			this.eventReceived = false;
			this.registered = false;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00018C07 File Offset: 0x00016E07
		public override void OnReset()
		{
			this.eventName = "";
		}

		// Token: 0x04000329 RID: 809
		[Tooltip("The name of the event to receive")]
		public SharedString eventName = "";

		// Token: 0x0400032A RID: 810
		[Tooltip("Optionally store the first sent argument")]
		[SharedRequired]
		public SharedVariable storedValue1;

		// Token: 0x0400032B RID: 811
		[Tooltip("Optionally store the second sent argument")]
		[SharedRequired]
		public SharedVariable storedValue2;

		// Token: 0x0400032C RID: 812
		[Tooltip("Optionally store the third sent argument")]
		[SharedRequired]
		public SharedVariable storedValue3;

		// Token: 0x0400032D RID: 813
		private bool eventReceived;

		// Token: 0x0400032E RID: 814
		private bool registered;
	}
}
