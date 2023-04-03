using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DA RID: 474
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Simulate the Particle System.")]
	public class Simulate : Action
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00021A4C File Offset: 0x0001FC4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00021A8C File Offset: 0x0001FC8C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.Simulate(this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00021ABF File Offset: 0x0001FCBF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040006A5 RID: 1701
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006A6 RID: 1702
		[Tooltip("Time to fastfoward the Particle System to")]
		public SharedFloat time;

		// Token: 0x040006A7 RID: 1703
		private ParticleSystem particleSystem;

		// Token: 0x040006A8 RID: 1704
		private GameObject prevGameObject;
	}
}
