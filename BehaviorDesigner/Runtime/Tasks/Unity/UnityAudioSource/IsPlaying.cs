using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000268 RID: 616
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Returns Success if the AudioClip is playing, otherwise Failure.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x000261B8 File Offset: 0x000243B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000261F8 File Offset: 0x000243F8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			if (!this.audioSource.isPlaying)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00026224 File Offset: 0x00024424
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400086D RID: 2157
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400086E RID: 2158
		private AudioSource audioSource;

		// Token: 0x0400086F RID: 2159
		private GameObject prevGameObject;
	}
}
