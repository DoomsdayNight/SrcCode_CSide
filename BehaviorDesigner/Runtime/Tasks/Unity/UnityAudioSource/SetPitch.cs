using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000275 RID: 629
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the pitch value of the AudioSource. Returns Success.")]
	public class SetPitch : Action
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x00026934 File Offset: 0x00024B34
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00026974 File Offset: 0x00024B74
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.pitch = this.pitch.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000269A7 File Offset: 0x00024BA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.pitch = 1f;
		}

		// Token: 0x0400089F RID: 2207
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008A0 RID: 2208
		[Tooltip("The pitch value of the AudioSource")]
		public SharedFloat pitch;

		// Token: 0x040008A1 RID: 2209
		private AudioSource audioSource;

		// Token: 0x040008A2 RID: 2210
		private GameObject prevGameObject;
	}
}
