using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000274 RID: 628
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the mute value of the AudioSource. Returns Success.")]
	public class SetMute : Action
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x000268A4 File Offset: 0x00024AA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000268E4 File Offset: 0x00024AE4
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.mute = this.mute.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00026917 File Offset: 0x00024B17
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mute = false;
		}

		// Token: 0x0400089B RID: 2203
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400089C RID: 2204
		[Tooltip("The mute value of the AudioSource")]
		public SharedBool mute;

		// Token: 0x0400089D RID: 2205
		private AudioSource audioSource;

		// Token: 0x0400089E RID: 2206
		private GameObject prevGameObject;
	}
}
