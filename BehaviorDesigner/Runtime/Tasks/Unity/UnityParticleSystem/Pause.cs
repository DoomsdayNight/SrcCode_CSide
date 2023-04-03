using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CC RID: 460
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Pause the Particle System.")]
	public class Pause : Action
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x0002118C File Offset: 0x0001F38C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000211CC File Offset: 0x0001F3CC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.Pause();
			return TaskStatus.Success;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000211F4 File Offset: 0x0001F3F4
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400066F RID: 1647
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000670 RID: 1648
		private ParticleSystem particleSystem;

		// Token: 0x04000671 RID: 1649
		private GameObject prevGameObject;
	}
}
