using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000265 RID: 613
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the time value of the AudioSource. Returns Success.")]
	public class GetTime : Action
	{
		// Token: 0x06000CA6 RID: 3238 RVA: 0x00025FF8 File Offset: 0x000241F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00026038 File Offset: 0x00024238
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.time;
			return TaskStatus.Success;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002606B File Offset: 0x0002426B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000861 RID: 2145
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000862 RID: 2146
		[Tooltip("The time value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000863 RID: 2147
		private AudioSource audioSource;

		// Token: 0x04000864 RID: 2148
		private GameObject prevGameObject;
	}
}
