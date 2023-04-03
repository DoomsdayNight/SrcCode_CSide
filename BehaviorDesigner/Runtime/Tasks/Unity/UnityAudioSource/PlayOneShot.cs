using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200026C RID: 620
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays an AudioClip, and scales the AudioSource volume by volumeScale. Returns Success.")]
	public class PlayOneShot : Action
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x000263D4 File Offset: 0x000245D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00026414 File Offset: 0x00024614
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.PlayOneShot((AudioClip)this.clip.Value, this.volumeScale.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00026462 File Offset: 0x00024662
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.clip = null;
			this.volumeScale = 1f;
		}

		// Token: 0x0400087A RID: 2170
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400087B RID: 2171
		[Tooltip("The clip being played")]
		public SharedObject clip;

		// Token: 0x0400087C RID: 2172
		[Tooltip("The scale of the volume (0-1)")]
		public SharedFloat volumeScale = 1f;

		// Token: 0x0400087D RID: 2173
		private AudioSource audioSource;

		// Token: 0x0400087E RID: 2174
		private GameObject prevGameObject;
	}
}
