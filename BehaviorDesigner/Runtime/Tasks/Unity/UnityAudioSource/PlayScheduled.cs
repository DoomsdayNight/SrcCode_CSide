using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200026D RID: 621
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayScheduled : Action
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002649C File Offset: 0x0002469C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000264DC File Offset: 0x000246DC
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.PlayScheduled((double)this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00026510 File Offset: 0x00024710
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x0400087F RID: 2175
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000880 RID: 2176
		[Tooltip("Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing")]
		public SharedFloat time = 0f;

		// Token: 0x04000881 RID: 2177
		private AudioSource audioSource;

		// Token: 0x04000882 RID: 2178
		private GameObject prevGameObject;
	}
}
