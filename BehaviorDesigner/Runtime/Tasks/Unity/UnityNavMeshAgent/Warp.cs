using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001EC RID: 492
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Warps agent to the provided position. Returns Success.")]
	public class Warp : Action
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x0002242C File Offset: 0x0002062C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002246C File Offset: 0x0002066C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.Warp(this.newPosition.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000224A0 File Offset: 0x000206A0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.newPosition = Vector3.zero;
		}

		// Token: 0x040006E8 RID: 1768
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006E9 RID: 1769
		[Tooltip("The position to warp to")]
		public SharedVector3 newPosition;

		// Token: 0x040006EA RID: 1770
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006EB RID: 1771
		private GameObject prevGameObject;
	}
}
