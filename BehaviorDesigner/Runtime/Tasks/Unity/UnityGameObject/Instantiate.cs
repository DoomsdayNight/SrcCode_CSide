using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200022E RID: 558
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Instantiates a new GameObject. Returns Success.")]
	public class Instantiate : Action
	{
		// Token: 0x06000BD4 RID: 3028 RVA: 0x00024174 File Offset: 0x00022374
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = UnityEngine.Object.Instantiate<GameObject>(this.targetGameObject.Value, this.position.Value, this.rotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000241A8 File Offset: 0x000223A8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04000797 RID: 1943
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000798 RID: 1944
		[Tooltip("The position of the new GameObject")]
		public SharedVector3 position;

		// Token: 0x04000799 RID: 1945
		[Tooltip("The rotation of the new GameObject")]
		public SharedQuaternion rotation = Quaternion.identity;

		// Token: 0x0400079A RID: 1946
		[SharedRequired]
		[Tooltip("The instantiated GameObject")]
		public SharedGameObject storeResult;
	}
}
