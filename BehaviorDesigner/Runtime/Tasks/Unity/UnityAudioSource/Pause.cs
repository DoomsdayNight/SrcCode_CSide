using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000269 RID: 617
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Pauses the audio clip. Returns Success.")]
	public class Pause : Action
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00026238 File Offset: 0x00024438
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00026278 File Offset: 0x00024478
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.Pause();
			return TaskStatus.Success;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000262A0 File Offset: 0x000244A0
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000870 RID: 2160
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000871 RID: 2161
		private AudioSource audioSource;

		// Token: 0x04000872 RID: 2162
		private GameObject prevGameObject;
	}
}
