using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x02000234 RID: 564
	[TaskDescription("LogFormat is analgous to Debug.LogFormat().\nIt takes format string, substitutes arguments supplied a '{0-4}' and returns success.\nAny fields or arguments not supplied are ignored.It can be used for debugging.")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class LogFormat : Action
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x00024438 File Offset: 0x00022638
		public override TaskStatus OnUpdate()
		{
			object[] args = this.buildParamsArray();
			if (this.logError.Value)
			{
				Debug.LogErrorFormat(this.textFormat.Value, args);
			}
			else
			{
				Debug.LogFormat(this.textFormat.Value, args);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00024480 File Offset: 0x00022680
		private object[] buildParamsArray()
		{
			object[] array;
			if (this.isValid(this.arg3))
			{
				array = new object[]
				{
					null,
					null,
					null,
					this.arg3.GetValue()
				};
				array[2] = this.arg2.GetValue();
				array[1] = this.arg1.GetValue();
				array[0] = this.arg0.GetValue();
			}
			else if (this.isValid(this.arg2))
			{
				array = new object[]
				{
					null,
					null,
					this.arg2.GetValue()
				};
				array[1] = this.arg1.GetValue();
				array[0] = this.arg0.GetValue();
			}
			else if (this.isValid(this.arg1))
			{
				array = new object[]
				{
					null,
					this.arg1.GetValue()
				};
				array[0] = this.arg0.GetValue();
			}
			else
			{
				if (!this.isValid(this.arg0))
				{
					return null;
				}
				array = new object[]
				{
					this.arg0.GetValue()
				};
			}
			return array;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002457B File Offset: 0x0002277B
		private bool isValid(SharedVariable sv)
		{
			return sv != null && !sv.IsNone;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002458B File Offset: 0x0002278B
		public override void OnReset()
		{
			this.textFormat = string.Empty;
			this.logError = false;
			this.arg0 = null;
			this.arg1 = null;
			this.arg2 = null;
			this.arg3 = null;
		}

		// Token: 0x040007AA RID: 1962
		[Tooltip("Text format with {0}, {1}, etc")]
		public SharedString textFormat;

		// Token: 0x040007AB RID: 1963
		[Tooltip("Is this text an error?")]
		public SharedBool logError;

		// Token: 0x040007AC RID: 1964
		public SharedVariable arg0;

		// Token: 0x040007AD RID: 1965
		public SharedVariable arg1;

		// Token: 0x040007AE RID: 1966
		public SharedVariable arg2;

		// Token: 0x040007AF RID: 1967
		public SharedVariable arg3;
	}
}
