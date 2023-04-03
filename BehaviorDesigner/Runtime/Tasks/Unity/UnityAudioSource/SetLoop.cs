using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000271 RID: 625
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the loop value of the AudioSource. Returns Success.")]
	public class SetLoop : Action
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x000266EC File Offset: 0x000248EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002672C File Offset: 0x0002492C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.loop = this.loop.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002675F File Offset: 0x0002495F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x0400088F RID: 2191
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000890 RID: 2192
		[Tooltip("The loop value of the AudioSource")]
		public SharedBool loop;

		// Token: 0x04000891 RID: 2193
		private AudioSource audioSource;

		// Token: 0x04000892 RID: 2194
		private GameObject prevGameObject;
	}
}
