using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D7 RID: 215
	[TaskDescription("Returns success when an object exits the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedTrigger2D : Conditional
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00019048 File Offset: 0x00017248
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedTrigger)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00019055 File Offset: 0x00017255
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00019060 File Offset: 0x00017260
		public override void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || other.gameObject.CompareTag(this.tag.Value))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000190AF File Offset: 0x000172AF
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04000344 RID: 836
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04000345 RID: 837
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000346 RID: 838
		private bool exitedTrigger;
	}
}
