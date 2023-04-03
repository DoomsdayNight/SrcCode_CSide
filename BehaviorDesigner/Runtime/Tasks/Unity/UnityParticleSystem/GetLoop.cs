using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C3 RID: 451
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores if the Particle System should loop.")]
	public class GetLoop : Action
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00020C6C File Offset: 0x0001EE6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00020CAC File Offset: 0x0001EEAC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.main.loop;
			return TaskStatus.Success;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00020CF2 File Offset: 0x0001EEF2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x0400064F RID: 1615
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000650 RID: 1616
		[Tooltip("Should the ParticleSystem loop?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x04000651 RID: 1617
		private ParticleSystem particleSystem;

		// Token: 0x04000652 RID: 1618
		private GameObject prevGameObject;
	}
}
