using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E3 RID: 483
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Apply relative movement to the current position. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00021F4C File Offset: 0x0002014C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00021F8C File Offset: 0x0002018C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.Move(this.offset.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00021FBF File Offset: 0x000201BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.offset = Vector3.zero;
		}

		// Token: 0x040006C7 RID: 1735
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006C8 RID: 1736
		[Tooltip("The relative movement vector")]
		public SharedVector3 offset;

		// Token: 0x040006C9 RID: 1737
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006CA RID: 1738
		private GameObject prevGameObject;
	}
}
