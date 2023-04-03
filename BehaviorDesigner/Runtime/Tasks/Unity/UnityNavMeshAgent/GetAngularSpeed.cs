using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001DD RID: 477
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the maximum turning speed in (deg/s) while following a path.. Returns Success.")]
	public class GetAngularSpeed : Action
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x00021BF0 File Offset: 0x0001FDF0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00021C30 File Offset: 0x0001FE30
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.angularSpeed;
			return TaskStatus.Success;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00021C63 File Offset: 0x0001FE63
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006B0 RID: 1712
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006B1 RID: 1713
		[SharedRequired]
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat storeValue;

		// Token: 0x040006B2 RID: 1714
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006B3 RID: 1715
		private GameObject prevGameObject;
	}
}
