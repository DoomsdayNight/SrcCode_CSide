using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200012A RID: 298
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Is the timeline currently playing?")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x0001C000 File Offset: 0x0001A200
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001C040 File Offset: 0x0001A240
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				Debug.LogWarning("PlayableDirector is null");
				return TaskStatus.Failure;
			}
			if (this.playableDirector.state != PlayState.Playing)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001C06D File Offset: 0x0001A26D
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000458 RID: 1112
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000459 RID: 1113
		private PlayableDirector playableDirector;

		// Token: 0x0400045A RID: 1114
		private GameObject prevGameObject;
	}
}
