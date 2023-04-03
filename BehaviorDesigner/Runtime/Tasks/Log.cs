using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000B8 RID: 184
	[TaskDescription("Log is a simple task which will output the specified text and return success. It can be used for debugging.")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class Log : Action
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x00016C70 File Offset: 0x00014E70
		public override TaskStatus OnUpdate()
		{
			if (this.logError.Value)
			{
				Debug.LogError(this.logTime.Value ? string.Format("{0}: {1}", Time.time, this.text) : this.text);
			}
			else
			{
				Debug.Log(this.logTime.Value ? string.Format("{0}: {1}", Time.time, this.text) : this.text);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00016CFF File Offset: 0x00014EFF
		public override void OnReset()
		{
			this.text = "";
			this.logError = false;
			this.logTime = false;
		}

		// Token: 0x040002CE RID: 718
		[Tooltip("Text to output to the log")]
		public SharedString text;

		// Token: 0x040002CF RID: 719
		[Tooltip("Is this text an error?")]
		public SharedBool logError;

		// Token: 0x040002D0 RID: 720
		[Tooltip("Should the time be included in the log message?")]
		public SharedBool logTime;
	}
}
