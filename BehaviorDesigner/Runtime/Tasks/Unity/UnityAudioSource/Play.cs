using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200026A RID: 618
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays the audio clip. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x000262B4 File Offset: 0x000244B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000262F4 File Offset: 0x000244F4
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.Play();
			return TaskStatus.Success;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002631C File Offset: 0x0002451C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000873 RID: 2163
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000874 RID: 2164
		private AudioSource audioSource;

		// Token: 0x04000875 RID: 2165
		private GameObject prevGameObject;
	}
}
