using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E0 RID: 480
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the distance between the agent's position and the destination on the current path. Returns Success.")]
	public class GetRemainingDistance : Action
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00021DE4 File Offset: 0x0001FFE4
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.remainingDistance;
			return TaskStatus.Success;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00021E17 File Offset: 0x00020017
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006BC RID: 1724
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006BD RID: 1725
		[SharedRequired]
		[Tooltip("The remaining distance")]
		public SharedFloat storeValue;

		// Token: 0x040006BE RID: 1726
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006BF RID: 1727
		private GameObject prevGameObject;
	}
}
