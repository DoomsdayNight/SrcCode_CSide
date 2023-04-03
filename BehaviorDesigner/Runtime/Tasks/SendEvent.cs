using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C0 RID: 192
	[TaskDescription("Sends an event to the behavior tree, returns success after sending the event.")]
	[HelpURL("https://www.opsive.com/support/documentation/behavior-designer/events/")]
	[TaskIcon("{SkinColor}SendEventIcon.png")]
	public class SendEvent : Action
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x00017348 File Offset: 0x00015548
		public override void OnStart()
		{
			BehaviorTree[] components = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponents<BehaviorTree>();
			if (components.Length == 1)
			{
				this.behaviorTree = components[0];
				return;
			}
			if (components.Length > 1)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].Group == this.group.Value)
					{
						this.behaviorTree = components[i];
						break;
					}
				}
				if (this.behaviorTree == null)
				{
					this.behaviorTree = components[0];
				}
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000173C8 File Offset: 0x000155C8
		public override TaskStatus OnUpdate()
		{
			if (this.argument1 == null || this.argument1.IsNone)
			{
				this.behaviorTree.SendEvent(this.eventName.Value);
			}
			else if (this.argument2 == null || this.argument2.IsNone)
			{
				this.behaviorTree.SendEvent<object>(this.eventName.Value, this.argument1.GetValue());
			}
			else if (this.argument3 == null || this.argument3.IsNone)
			{
				this.behaviorTree.SendEvent<object, object>(this.eventName.Value, this.argument1.GetValue(), this.argument2.GetValue());
			}
			else
			{
				this.behaviorTree.SendEvent<object, object, object>(this.eventName.Value, this.argument1.GetValue(), this.argument2.GetValue(), this.argument3.GetValue());
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000174B8 File Offset: 0x000156B8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eventName = "";
		}

		// Token: 0x040002EE RID: 750
		[Tooltip("The GameObject of the behavior tree that should have the event sent to it. If null use the current behavior")]
		public SharedGameObject targetGameObject;

		// Token: 0x040002EF RID: 751
		[Tooltip("The event to send")]
		public SharedString eventName;

		// Token: 0x040002F0 RID: 752
		[Tooltip("The group of the behavior tree that the event should be sent to")]
		public SharedInt group;

		// Token: 0x040002F1 RID: 753
		[Tooltip("Optionally specify a first argument to send")]
		[SharedRequired]
		public SharedVariable argument1;

		// Token: 0x040002F2 RID: 754
		[Tooltip("Optionally specify a second argument to send")]
		[SharedRequired]
		public SharedVariable argument2;

		// Token: 0x040002F3 RID: 755
		[Tooltip("Optionally specify a third argument to send")]
		[SharedRequired]
		public SharedVariable argument3;

		// Token: 0x040002F4 RID: 756
		private BehaviorTree behaviorTree;
	}
}
