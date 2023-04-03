using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C1 RID: 449
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the emission rate of the Particle System.")]
	public class GetEmissionRate : Action
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00020B40 File Offset: 0x0001ED40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00020B80 File Offset: 0x0001ED80
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			Debug.Log("Warning: GetEmissionRate is not used in Unity 5.3 or later.");
			return TaskStatus.Success;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00020BA7 File Offset: 0x0001EDA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04000647 RID: 1607
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000648 RID: 1608
		[Tooltip("The emission rate of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04000649 RID: 1609
		private ParticleSystem particleSystem;

		// Token: 0x0400064A RID: 1610
		private GameObject prevGameObject;
	}
}
