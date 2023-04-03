using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E4 RID: 484
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Clears the current path. Returns Success.")]
	public class ResetPath : Action
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x00021FE0 File Offset: 0x000201E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00022020 File Offset: 0x00020220
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.ResetPath();
			return TaskStatus.Success;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00022048 File Offset: 0x00020248
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006CB RID: 1739
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006CC RID: 1740
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006CD RID: 1741
		private GameObject prevGameObject;
	}
}
