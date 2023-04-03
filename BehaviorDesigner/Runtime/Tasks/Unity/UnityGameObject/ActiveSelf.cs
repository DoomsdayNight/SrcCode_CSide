using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000224 RID: 548
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveSelf : Conditional
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x00023E43 File Offset: 0x00022043
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).activeSelf)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00023E60 File Offset: 0x00022060
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000783 RID: 1923
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
