using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000230 RID: 560
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Activates/Deactivates the GameObject. Returns Success.")]
	public class SetActive : Action
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x00024281 File Offset: 0x00022481
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).SetActive(this.active.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000242A5 File Offset: 0x000224A5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.active = false;
		}

		// Token: 0x0400079E RID: 1950
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400079F RID: 1951
		[Tooltip("Active state of the GameObject")]
		public SharedBool active;
	}
}
