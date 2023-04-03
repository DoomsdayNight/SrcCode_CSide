using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D6 RID: 214
	[TaskDescription("Returns success when an object exits the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedTrigger : Conditional
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00018FB0 File Offset: 0x000171B0
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedTrigger)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00018FBD File Offset: 0x000171BD
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00018FC8 File Offset: 0x000171C8
		public override void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || other.gameObject.CompareTag(this.tag.Value))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00019017 File Offset: 0x00017217
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04000341 RID: 833
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04000342 RID: 834
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000343 RID: 835
		private bool exitedTrigger;
	}
}
