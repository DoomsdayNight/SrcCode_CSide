using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200012E RID: 302
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Stops playback of the current Playable and destroys the corresponding graph.")]
	public class Stop : Action
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001C314 File Offset: 0x0001A514
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				Debug.LogWarning("PlayableDirector is null");
				return TaskStatus.Failure;
			}
			this.playableDirector.Stop();
			return TaskStatus.Success;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001C33C File Offset: 0x0001A53C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000469 RID: 1129
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400046A RID: 1130
		private PlayableDirector playableDirector;

		// Token: 0x0400046B RID: 1131
		private GameObject prevGameObject;
	}
}
