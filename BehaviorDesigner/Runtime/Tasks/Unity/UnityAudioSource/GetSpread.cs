using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000264 RID: 612
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the spread value of the AudioSource. Returns Success.")]
	public class GetSpread : Action
	{
		// Token: 0x06000CA2 RID: 3234 RVA: 0x00025F64 File Offset: 0x00024164
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00025FA4 File Offset: 0x000241A4
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.spread;
			return TaskStatus.Success;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00025FD7 File Offset: 0x000241D7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x0400085D RID: 2141
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400085E RID: 2142
		[Tooltip("The spread value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400085F RID: 2143
		private AudioSource audioSource;

		// Token: 0x04000860 RID: 2144
		private GameObject prevGameObject;
	}
}
