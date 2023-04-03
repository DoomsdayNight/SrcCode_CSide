using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001BF RID: 447
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Clear the Particle System.")]
	public class Clear : Action
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x00020A1C File Offset: 0x0001EC1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00020A5C File Offset: 0x0001EC5C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.Clear();
			return TaskStatus.Success;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00020A84 File Offset: 0x0001EC84
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000640 RID: 1600
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000641 RID: 1601
		private ParticleSystem particleSystem;

		// Token: 0x04000642 RID: 1602
		private GameObject prevGameObject;
	}
}
