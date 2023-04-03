using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E9 RID: 489
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the stop status. Returns Success.")]
	public class SetIsStopped : Action
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x00022298 File Offset: 0x00020498
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000222D8 File Offset: 0x000204D8
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.isStopped = this.isStopped.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002230B File Offset: 0x0002050B
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006DD RID: 1757
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006DE RID: 1758
		[Tooltip("The stop status")]
		public SharedBool isStopped;

		// Token: 0x040006DF RID: 1759
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006E0 RID: 1760
		private GameObject prevGameObject;
	}
}
