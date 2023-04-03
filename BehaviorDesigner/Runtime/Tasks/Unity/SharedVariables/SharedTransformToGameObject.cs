using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000163 RID: 355
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Gets the GameObject from the Transform component. Returns Success.")]
	public class SharedTransformToGameObject : Action
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x0001D6F5 File Offset: 0x0001B8F5
		public override TaskStatus OnUpdate()
		{
			if (this.sharedTransform.Value == null)
			{
				return TaskStatus.Failure;
			}
			this.sharedGameObject.Value = this.sharedTransform.Value.gameObject;
			return TaskStatus.Success;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001D728 File Offset: 0x0001B928
		public override void OnReset()
		{
			this.sharedTransform = null;
			this.sharedGameObject = null;
		}

		// Token: 0x040004DE RID: 1246
		[Tooltip("The Transform component")]
		public SharedTransform sharedTransform;

		// Token: 0x040004DF RID: 1247
		[RequiredField]
		[Tooltip("The GameObject to set")]
		public SharedGameObject sharedGameObject;
	}
}
