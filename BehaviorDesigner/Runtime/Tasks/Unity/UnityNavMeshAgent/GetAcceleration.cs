using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001DC RID: 476
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the maximum acceleration of an agent as it follows a path, given in units / sec^2.. Returns Success.")]
	public class GetAcceleration : Action
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x00021B5C File Offset: 0x0001FD5C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00021B9C File Offset: 0x0001FD9C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.acceleration;
			return TaskStatus.Success;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00021BCF File Offset: 0x0001FDCF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006AC RID: 1708
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006AD RID: 1709
		[SharedRequired]
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat storeValue;

		// Token: 0x040006AE RID: 1710
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006AF RID: 1711
		private GameObject prevGameObject;
	}
}
