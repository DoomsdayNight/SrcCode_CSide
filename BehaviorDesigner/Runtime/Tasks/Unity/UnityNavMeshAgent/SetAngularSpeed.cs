using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E7 RID: 487
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the maximum turning speed in (deg/s) while following a path. Returns Success.")]
	public class SetAngularSpeed : Action
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002216C File Offset: 0x0002036C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000221AC File Offset: 0x000203AC
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.angularSpeed = this.angularSpeed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000221DF File Offset: 0x000203DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularSpeed = 0f;
		}

		// Token: 0x040006D5 RID: 1749
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006D6 RID: 1750
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat angularSpeed;

		// Token: 0x040006D7 RID: 1751
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006D8 RID: 1752
		private GameObject prevGameObject;
	}
}
