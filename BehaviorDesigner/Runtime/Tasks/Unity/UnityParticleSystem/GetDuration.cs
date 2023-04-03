using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C0 RID: 448
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the duration of the Particle System.")]
	public class GetDuration : Action
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x00020A98 File Offset: 0x0001EC98
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.main.duration;
			return TaskStatus.Success;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00020B1E File Offset: 0x0001ED1E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04000643 RID: 1603
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000644 RID: 1604
		[Tooltip("The duration of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04000645 RID: 1605
		private ParticleSystem particleSystem;

		// Token: 0x04000646 RID: 1606
		private GameObject prevGameObject;
	}
}
