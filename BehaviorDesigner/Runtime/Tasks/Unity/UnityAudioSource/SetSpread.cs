using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200027A RID: 634
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the spread value of the AudioSource. Returns Success.")]
	public class SetSpread : Action
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x00026C30 File Offset: 0x00024E30
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00026C70 File Offset: 0x00024E70
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.spread = this.spread.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00026CA3 File Offset: 0x00024EA3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spread = 1f;
		}

		// Token: 0x040008B3 RID: 2227
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008B4 RID: 2228
		[Tooltip("The spread value of the AudioSource")]
		public SharedFloat spread;

		// Token: 0x040008B5 RID: 2229
		private AudioSource audioSource;

		// Token: 0x040008B6 RID: 2230
		private GameObject prevGameObject;
	}
}
