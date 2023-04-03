using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D3 RID: 467
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start color of the Particle System.")]
	public class SetStartColor : Action
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x000215B0 File Offset: 0x0001F7B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000215F0 File Offset: 0x0001F7F0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startColor = this.startColor.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002163B File Offset: 0x0001F83B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startColor = Color.white;
		}

		// Token: 0x04000689 RID: 1673
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400068A RID: 1674
		[Tooltip("The start color of the ParticleSystem")]
		public SharedColor startColor;

		// Token: 0x0400068B RID: 1675
		private ParticleSystem particleSystem;

		// Token: 0x0400068C RID: 1676
		private GameObject prevGameObject;
	}
}
