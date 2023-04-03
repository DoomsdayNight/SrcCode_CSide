using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000BF RID: 191
	[TaskDescription("Restarts a behavior tree, returns success after it has been restarted.")]
	[TaskIcon("{SkinColor}RestartBehaviorTreeIcon.png")]
	public class RestartBehaviorTree : Action
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x0001728C File Offset: 0x0001548C
		public override void OnAwake()
		{
			Behavior[] components = base.GetDefaultGameObject(this.behaviorGameObject.Value).GetComponents<Behavior>();
			if (components.Length == 1)
			{
				this.behavior = components[0];
				return;
			}
			if (components.Length > 1)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].Group == this.group.Value)
					{
						this.behavior = components[i];
						break;
					}
				}
				if (this.behavior == null)
				{
					this.behavior = components[0];
				}
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001730B File Offset: 0x0001550B
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return TaskStatus.Failure;
			}
			this.behavior.DisableBehavior();
			this.behavior.EnableBehavior();
			return TaskStatus.Success;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00017334 File Offset: 0x00015534
		public override void OnReset()
		{
			this.behavior = null;
		}

		// Token: 0x040002EB RID: 747
		[Tooltip("The GameObject of the behavior tree that should be restarted. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x040002EC RID: 748
		[Tooltip("The group of the behavior tree that should be restarted")]
		public SharedInt group;

		// Token: 0x040002ED RID: 749
		private Behavior behavior;
	}
}
