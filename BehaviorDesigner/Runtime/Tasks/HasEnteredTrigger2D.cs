using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D3 RID: 211
	[TaskDescription("Returns success when an object enters the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredTrigger2D : Conditional
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00018DF8 File Offset: 0x00016FF8
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredTrigger)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00018E05 File Offset: 0x00017005
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00018E10 File Offset: 0x00017010
		public override void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || other.gameObject.CompareTag(this.tag.Value))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00018E5F File Offset: 0x0001705F
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04000338 RID: 824
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04000339 RID: 825
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x0400033A RID: 826
		private bool enteredTrigger;
	}
}
