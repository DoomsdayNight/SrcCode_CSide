using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D9 RID: 473
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the time of the Particle System.")]
	public class SetTime : Action
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x000219B8 File Offset: 0x0001FBB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000219F8 File Offset: 0x0001FBF8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.time = this.time.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00021A2B File Offset: 0x0001FC2B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040006A1 RID: 1697
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006A2 RID: 1698
		[Tooltip("The time of the ParticleSystem")]
		public SharedFloat time;

		// Token: 0x040006A3 RID: 1699
		private ParticleSystem particleSystem;

		// Token: 0x040006A4 RID: 1700
		private GameObject prevGameObject;
	}
}
