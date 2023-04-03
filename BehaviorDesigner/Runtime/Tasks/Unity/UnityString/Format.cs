using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000136 RID: 310
	[TaskCategory("Unity/String")]
	[TaskDescription("Stores a string with the specified format.")]
	public class Format : Action
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x0001C4F2 File Offset: 0x0001A6F2
		public override void OnAwake()
		{
			this.variableValues = new object[this.variables.Length];
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001C508 File Offset: 0x0001A708
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.variableValues.Length; i++)
			{
				this.variableValues[i] = this.variables[i].Value.value.GetValue();
			}
			try
			{
				this.storeResult.Value = string.Format(this.format.Value, this.variableValues);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001C58C File Offset: 0x0001A78C
		public override void OnReset()
		{
			this.format = "";
			this.variables = null;
			this.storeResult = null;
		}

		// Token: 0x04000476 RID: 1142
		[Tooltip("The format of the string")]
		public SharedString format;

		// Token: 0x04000477 RID: 1143
		[Tooltip("Any variables to appear in the string")]
		public SharedGenericVariable[] variables;

		// Token: 0x04000478 RID: 1144
		[Tooltip("The result of the format")]
		[RequiredField]
		public SharedString storeResult;

		// Token: 0x04000479 RID: 1145
		private object[] variableValues;
	}
}
