using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000266 RID: 614
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the time samples value of the AudioSource. Returns Success.")]
	public class GetTimeSamples : Action
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x0002608C File Offset: 0x0002428C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000260CC File Offset: 0x000242CC
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = (float)this.audioSource.timeSamples;
			return TaskStatus.Success;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00026100 File Offset: 0x00024300
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000865 RID: 2149
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000866 RID: 2150
		[Tooltip("The time samples value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000867 RID: 2151
		private AudioSource audioSource;

		// Token: 0x04000868 RID: 2152
		private GameObject prevGameObject;
	}
}
