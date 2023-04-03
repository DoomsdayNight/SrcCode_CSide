using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000226 RID: 550
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if tags match, otherwise Failure.")]
	public class CompareTag : Conditional
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00023EB0 File Offset: 0x000220B0
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).CompareTag(this.tag.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00023ED8 File Offset: 0x000220D8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
		}

		// Token: 0x04000786 RID: 1926
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000787 RID: 1927
		[Tooltip("The tag to compare against")]
		public SharedString tag;
	}
}
