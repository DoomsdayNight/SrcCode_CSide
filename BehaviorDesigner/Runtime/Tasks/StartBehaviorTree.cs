using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C2 RID: 194
	[TaskDescription("Start a new behavior tree and return success after it has been started.")]
	[TaskIcon("{SkinColor}StartBehaviorTreeIcon.png")]
	public class StartBehaviorTree : Action
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x000179A0 File Offset: 0x00015BA0
		public override void OnStart()
		{
			Behavior[] components = base.GetDefaultGameObject(this.behaviorGameObject.Value).GetComponents<Behavior>();
			if (components.Length == 1)
			{
				this.behavior = components[0];
			}
			else if (components.Length > 1)
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
			if (this.behavior != null)
			{
				List<SharedVariable> allVariables = base.Owner.GetAllVariables();
				if (allVariables != null && this.synchronizeVariables.Value)
				{
					for (int j = 0; j < allVariables.Count; j++)
					{
						this.behavior.SetVariableValue(allVariables[j].Name, allVariables[j]);
					}
				}
				this.behavior.EnableBehavior();
				if (this.waitForCompletion.Value)
				{
					this.behaviorComplete = false;
					this.behavior.OnBehaviorEnd += this.BehaviorEnded;
				}
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00017AB2 File Offset: 0x00015CB2
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return TaskStatus.Failure;
			}
			if (this.waitForCompletion.Value && !this.behaviorComplete)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00017ADC File Offset: 0x00015CDC
		private void BehaviorEnded(Behavior behavior)
		{
			this.behaviorComplete = true;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00017AE5 File Offset: 0x00015CE5
		public override void OnEnd()
		{
			if (this.behavior != null && this.waitForCompletion.Value)
			{
				this.behavior.OnBehaviorEnd -= this.BehaviorEnded;
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00017B19 File Offset: 0x00015D19
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.waitForCompletion = false;
			this.synchronizeVariables = false;
		}

		// Token: 0x040002F8 RID: 760
		[Tooltip("The GameObject of the behavior tree that should be started. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x040002F9 RID: 761
		[Tooltip("The group of the behavior tree that should be started")]
		public SharedInt group;

		// Token: 0x040002FA RID: 762
		[Tooltip("Should this task wait for the behavior tree to complete?")]
		public SharedBool waitForCompletion = false;

		// Token: 0x040002FB RID: 763
		[Tooltip("Should the variables be synchronized?")]
		public SharedBool synchronizeVariables;

		// Token: 0x040002FC RID: 764
		private bool behaviorComplete;

		// Token: 0x040002FD RID: 765
		private Behavior behavior;
	}
}
