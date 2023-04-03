using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200027C RID: 636
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetVelocityUpdateMode : Action
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x00026D58 File Offset: 0x00024F58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00026D98 File Offset: 0x00024F98
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.velocityUpdateMode = this.velocityUpdateMode;
			return TaskStatus.Success;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00026DC6 File Offset: 0x00024FC6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocityUpdateMode = AudioVelocityUpdateMode.Auto;
		}

		// Token: 0x040008BB RID: 2235
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008BC RID: 2236
		[Tooltip("The velocity update mode of the AudioSource")]
		public AudioVelocityUpdateMode velocityUpdateMode;

		// Token: 0x040008BD RID: 2237
		private AudioSource audioSource;

		// Token: 0x040008BE RID: 2238
		private GameObject prevGameObject;
	}
}
