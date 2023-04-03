using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000267 RID: 615
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the volume value of the AudioSource. Returns Success.")]
	public class GetVolume : Action
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x00026124 File Offset: 0x00024324
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00026164 File Offset: 0x00024364
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.volume;
			return TaskStatus.Success;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00026197 File Offset: 0x00024397
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000869 RID: 2153
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400086A RID: 2154
		[Tooltip("The volume value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400086B RID: 2155
		private AudioSource audioSource;

		// Token: 0x0400086C RID: 2156
		private GameObject prevGameObject;
	}
}
