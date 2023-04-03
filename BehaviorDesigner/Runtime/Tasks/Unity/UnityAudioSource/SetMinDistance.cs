using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000273 RID: 627
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the min distance value of the AudioSource. Returns Success.")]
	public class SetMinDistance : Action
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x00026810 File Offset: 0x00024A10
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00026850 File Offset: 0x00024A50
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.minDistance = this.minDistance.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00026883 File Offset: 0x00024A83
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.minDistance = 1f;
		}

		// Token: 0x04000897 RID: 2199
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000898 RID: 2200
		[Tooltip("The min distance value of the AudioSource")]
		public SharedFloat minDistance;

		// Token: 0x04000899 RID: 2201
		private AudioSource audioSource;

		// Token: 0x0400089A RID: 2202
		private GameObject prevGameObject;
	}
}
