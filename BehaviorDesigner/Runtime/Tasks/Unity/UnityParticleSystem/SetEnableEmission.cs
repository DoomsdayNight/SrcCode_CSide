using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CF RID: 463
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Enables or disables the Particle System emission.")]
	public class SetEnableEmission : Action
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0002130C File Offset: 0x0001F50C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002134C File Offset: 0x0001F54C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.emission.enabled = this.enable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00021392 File Offset: 0x0001F592
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.enable = false;
		}

		// Token: 0x04000679 RID: 1657
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400067A RID: 1658
		[Tooltip("Enable the ParticleSystem emissions?")]
		public SharedBool enable;

		// Token: 0x0400067B RID: 1659
		private ParticleSystem particleSystem;

		// Token: 0x0400067C RID: 1660
		private GameObject prevGameObject;
	}
}
