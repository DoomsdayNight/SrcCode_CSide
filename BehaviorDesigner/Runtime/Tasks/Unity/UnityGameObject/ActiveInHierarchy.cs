using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000223 RID: 547
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveInHierarchy : Conditional
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x00023E15 File Offset: 0x00022015
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).activeInHierarchy)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00023E32 File Offset: 0x00022032
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000782 RID: 1922
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
