using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DB RID: 475
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stop the Particle System.")]
	public class Stop : Action
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00021B20 File Offset: 0x0001FD20
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.Stop();
			return TaskStatus.Success;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00021B48 File Offset: 0x0001FD48
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006A9 RID: 1705
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006AA RID: 1706
		private ParticleSystem particleSystem;

		// Token: 0x040006AB RID: 1707
		private GameObject prevGameObject;
	}
}
