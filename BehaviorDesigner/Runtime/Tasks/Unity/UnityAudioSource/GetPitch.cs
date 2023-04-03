using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000262 RID: 610
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the pitch value of the AudioSource. Returns Success.")]
	public class GetPitch : Action
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x00025E40 File Offset: 0x00024040
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00025E80 File Offset: 0x00024080
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.pitch;
			return TaskStatus.Success;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00025EB3 File Offset: 0x000240B3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000855 RID: 2133
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000856 RID: 2134
		[Tooltip("The pitch value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000857 RID: 2135
		private AudioSource audioSource;

		// Token: 0x04000858 RID: 2136
		private GameObject prevGameObject;
	}
}
