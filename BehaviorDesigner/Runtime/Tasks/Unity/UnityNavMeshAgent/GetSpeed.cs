using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E1 RID: 481
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the maximum movement speed when following a path. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00021E38 File Offset: 0x00020038
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00021E78 File Offset: 0x00020078
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.speed;
			return TaskStatus.Success;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00021EAB File Offset: 0x000200AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006C0 RID: 1728
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006C1 RID: 1729
		[SharedRequired]
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat storeValue;

		// Token: 0x040006C2 RID: 1730
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006C3 RID: 1731
		private GameObject prevGameObject;
	}
}
