using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000276 RID: 630
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the priority value of the AudioSource. Returns Success.")]
	public class SetPriority : Action
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x000269C8 File Offset: 0x00024BC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00026A08 File Offset: 0x00024C08
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.priority = this.priority.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00026A3B File Offset: 0x00024C3B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.priority = 1;
		}

		// Token: 0x040008A3 RID: 2211
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008A4 RID: 2212
		[Tooltip("The priority value of the AudioSource")]
		public SharedInt priority;

		// Token: 0x040008A5 RID: 2213
		private AudioSource audioSource;

		// Token: 0x040008A6 RID: 2214
		private GameObject prevGameObject;
	}
}
