using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200026B RID: 619
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayDelayed : Action
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x00026330 File Offset: 0x00024530
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00026370 File Offset: 0x00024570
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.PlayDelayed(this.delay.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000263A3 File Offset: 0x000245A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.delay = 0f;
		}

		// Token: 0x04000876 RID: 2166
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000877 RID: 2167
		[Tooltip("Delay time specified in seconds")]
		public SharedFloat delay = 0f;

		// Token: 0x04000878 RID: 2168
		private AudioSource audioSource;

		// Token: 0x04000879 RID: 2169
		private GameObject prevGameObject;
	}
}
