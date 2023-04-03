using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000272 RID: 626
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the max distance value of the AudioSource. Returns Success.")]
	public class SetMaxDistance : Action
	{
		// Token: 0x06000CDA RID: 3290 RVA: 0x0002677C File Offset: 0x0002497C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000267BC File Offset: 0x000249BC
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.maxDistance = this.maxDistance.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000267EF File Offset: 0x000249EF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxDistance = 1f;
		}

		// Token: 0x04000893 RID: 2195
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000894 RID: 2196
		[Tooltip("The max distance value of the AudioSource")]
		public SharedFloat maxDistance;

		// Token: 0x04000895 RID: 2197
		private AudioSource audioSource;

		// Token: 0x04000896 RID: 2198
		private GameObject prevGameObject;
	}
}
