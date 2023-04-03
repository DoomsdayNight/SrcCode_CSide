using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x02000129 RID: 297
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Is the timeline currently paused?")]
	public class IsPaused : Conditional
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x0001BF80 File Offset: 0x0001A180
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				Debug.LogWarning("PlayableDirector is null");
				return TaskStatus.Failure;
			}
			if (this.playableDirector.state != PlayState.Paused)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000455 RID: 1109
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000456 RID: 1110
		private PlayableDirector playableDirector;

		// Token: 0x04000457 RID: 1111
		private GameObject prevGameObject;
	}
}
