using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000231 RID: 561
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Sets the GameObject tag. Returns Success.")]
	public class SetTag : Action
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x000242C2 File Offset: 0x000224C2
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).tag = this.tag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000242E6 File Offset: 0x000224E6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
		}

		// Token: 0x040007A0 RID: 1952
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007A1 RID: 1953
		[Tooltip("The GameObject tag")]
		public SharedString tag;
	}
}
