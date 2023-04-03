using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200027D RID: 637
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the volume value of the AudioSource. Returns Success.")]
	public class SetVolume : Action
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00026E20 File Offset: 0x00025020
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.volume = this.volume.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00026E53 File Offset: 0x00025053
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.volume = 1f;
		}

		// Token: 0x040008BF RID: 2239
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008C0 RID: 2240
		[Tooltip("The volume value of the AudioSource")]
		public SharedFloat volume;

		// Token: 0x040008C1 RID: 2241
		private AudioSource audioSource;

		// Token: 0x040008C2 RID: 2242
		private GameObject prevGameObject;
	}
}
