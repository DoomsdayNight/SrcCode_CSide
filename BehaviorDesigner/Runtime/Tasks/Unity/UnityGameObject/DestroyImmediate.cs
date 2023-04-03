using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000228 RID: 552
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Destorys the specified GameObject immediately. Returns Success.")]
	public class DestroyImmediate : Action
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x00023F59 File Offset: 0x00022159
		public override TaskStatus OnUpdate()
		{
			UnityEngine.Object.DestroyImmediate(base.GetDefaultGameObject(this.targetGameObject.Value));
			return TaskStatus.Success;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00023F72 File Offset: 0x00022172
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400078A RID: 1930
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
