using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000B7 RID: 183
	[TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
	[TaskIcon("{SkinColor}IdleIcon.png")]
	public class Idle : Action
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x00016C62 File Offset: 0x00014E62
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}
	}
}
