using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CB RID: 459
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System stopped?")]
	public class IsStopped : Conditional
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0002110C File Offset: 0x0001F30C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002114C File Offset: 0x0001F34C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			if (!this.particleSystem.isStopped)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00021178 File Offset: 0x0001F378
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400066C RID: 1644
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400066D RID: 1645
		private ParticleSystem particleSystem;

		// Token: 0x0400066E RID: 1646
		private GameObject prevGameObject;
	}
}
