using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C6 RID: 454
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the playback speed of the Particle System.")]
	public class GetPlaybackSpeed : Action
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00020E50 File Offset: 0x0001F050
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00020E90 File Offset: 0x0001F090
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			ParticleSystem.MainModule main = this.particleSystem.main;
			this.storeResult.Value = main.simulationSpeed;
			return TaskStatus.Success;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00020ED6 File Offset: 0x0001F0D6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x0400065B RID: 1627
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400065C RID: 1628
		[Tooltip("The playback speed of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x0400065D RID: 1629
		private ParticleSystem particleSystem;

		// Token: 0x0400065E RID: 1630
		private GameObject prevGameObject;
	}
}
