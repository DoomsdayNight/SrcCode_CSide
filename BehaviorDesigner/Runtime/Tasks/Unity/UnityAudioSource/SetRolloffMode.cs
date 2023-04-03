using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000277 RID: 631
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetRolloffMode : Action
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00026A58 File Offset: 0x00024C58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00026A98 File Offset: 0x00024C98
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.rolloffMode = this.rolloffMode;
			return TaskStatus.Success;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00026AC6 File Offset: 0x00024CC6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rolloffMode = AudioRolloffMode.Logarithmic;
		}

		// Token: 0x040008A7 RID: 2215
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008A8 RID: 2216
		[Tooltip("The rolloff mode of the AudioSource")]
		public AudioRolloffMode rolloffMode;

		// Token: 0x040008A9 RID: 2217
		private AudioSource audioSource;

		// Token: 0x040008AA RID: 2218
		private GameObject prevGameObject;
	}
}
