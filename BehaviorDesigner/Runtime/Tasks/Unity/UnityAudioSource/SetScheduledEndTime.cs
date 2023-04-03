using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000278 RID: 632
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will end. Notice that depending on the timing not all rescheduling requests can be fulfilled. Returns Success.")]
	public class SetScheduledEndTime : Action
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00026AE0 File Offset: 0x00024CE0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00026B20 File Offset: 0x00024D20
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.SetScheduledEndTime((double)this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00026B54 File Offset: 0x00024D54
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040008AB RID: 2219
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008AC RID: 2220
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x040008AD RID: 2221
		private AudioSource audioSource;

		// Token: 0x040008AE RID: 2222
		private GameObject prevGameObject;
	}
}
