using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D2 RID: 466
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the playback speed of the Particle System.")]
	public class SetPlaybackSpeed : Action
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x000214F8 File Offset: 0x0001F6F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00021538 File Offset: 0x0001F738
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.simulationSpeed = this.playbackSpeed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002157E File Offset: 0x0001F77E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.playbackSpeed = 1f;
		}

		// Token: 0x04000685 RID: 1669
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000686 RID: 1670
		[Tooltip("The playback speed of the ParticleSystem")]
		public SharedFloat playbackSpeed = 1f;

		// Token: 0x04000687 RID: 1671
		private ParticleSystem particleSystem;

		// Token: 0x04000688 RID: 1672
		private GameObject prevGameObject;
	}
}
