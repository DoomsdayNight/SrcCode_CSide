using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C2 RID: 450
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores if the Particle System is emitting particles.")]
	public class GetEnableEmission : Action
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00020C08 File Offset: 0x0001EE08
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.emission.enabled;
			return TaskStatus.Success;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00020C4E File Offset: 0x0001EE4E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x0400064B RID: 1611
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400064C RID: 1612
		[Tooltip("Is the Particle System emitting particles?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x0400064D RID: 1613
		private ParticleSystem particleSystem;

		// Token: 0x0400064E RID: 1614
		private GameObject prevGameObject;
	}
}
