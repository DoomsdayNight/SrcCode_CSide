using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200022D RID: 557
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Stores the GameObject tag. Returns Success.")]
	public class GetTag : Action
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002412F File Offset: 0x0002232F
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).tag;
			return TaskStatus.Success;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00024153 File Offset: 0x00022353
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = "";
		}

		// Token: 0x04000795 RID: 1941
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000796 RID: 1942
		[Tooltip("Active state of the GameObject")]
		[RequiredField]
		public SharedString storeValue;
	}
}
