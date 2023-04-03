using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C3 RID: 195
	[TaskDescription("Pause or disable a behavior tree and return success after it has been stopped.")]
	[TaskIcon("{SkinColor}StopBehaviorTreeIcon.png")]
	public class StopBehaviorTree : Action
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x00017B5C File Offset: 0x00015D5C
		public override void OnStart()
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

		// Token: 0x06000666 RID: 1638 RVA: 0x00017BDB File Offset: 0x00015DDB
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return TaskStatus.Failure;
			}
			this.behavior.DisableBehavior(this.pauseBehavior.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00017C04 File Offset: 0x00015E04
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.pauseBehavior = false;
		}

		// Token: 0x040002FE RID: 766
		[Tooltip("The GameObject of the behavior tree that should be stopped. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x040002FF RID: 767
		[Tooltip("The group of the behavior tree that should be stopped")]
		public SharedInt group;

		// Token: 0x04000300 RID: 768
		[Tooltip("Should the behavior be paused or completely disabled")]
		public SharedBool pauseBehavior = false;

		// Token: 0x04000301 RID: 769
		private Behavior behavior;
	}
}
