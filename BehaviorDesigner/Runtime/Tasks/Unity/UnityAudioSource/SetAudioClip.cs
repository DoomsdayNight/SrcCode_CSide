using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200026E RID: 622
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the clip value of the AudioSource. Returns Success.")]
	public class SetAudioClip : Action
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x00026544 File Offset: 0x00024744
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00026584 File Offset: 0x00024784
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.clip = this.audioClip;
			return TaskStatus.Success;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000265B2 File Offset: 0x000247B2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.audioClip = null;
		}

		// Token: 0x04000883 RID: 2179
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000884 RID: 2180
		[Tooltip("The AudioSource clip")]
		public AudioClip audioClip;

		// Token: 0x04000885 RID: 2181
		private AudioSource audioSource;

		// Token: 0x04000886 RID: 2182
		private GameObject prevGameObject;
	}
}
