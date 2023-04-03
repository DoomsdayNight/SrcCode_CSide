using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D2 RID: 210
	[TaskDescription("Returns success when an object enters the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredTrigger : Conditional
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x00018D60 File Offset: 0x00016F60
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredTrigger)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00018D6D File Offset: 0x00016F6D
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00018D78 File Offset: 0x00016F78
		public override void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || other.gameObject.CompareTag(this.tag.Value))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00018DC7 File Offset: 0x00016FC7
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04000335 RID: 821
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04000336 RID: 822
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000337 RID: 823
		private bool enteredTrigger;
	}
}
