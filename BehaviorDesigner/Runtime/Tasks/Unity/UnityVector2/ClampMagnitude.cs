using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020000FA RID: 250
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Clamps the magnitude of the Vector2.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0001A567 File Offset: 0x00018767
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.ClampMagnitude(this.vector2Variable.Value, this.maxLength.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001A590 File Offset: 0x00018790
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = Vector2.zero;
			this.maxLength = 0f;
		}

		// Token: 0x040003A4 RID: 932
		[Tooltip("The Vector2 to clamp the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003A5 RID: 933
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x040003A6 RID: 934
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
