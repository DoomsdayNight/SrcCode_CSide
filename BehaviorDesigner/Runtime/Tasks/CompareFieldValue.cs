using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D9 RID: 217
	[TaskDescription("Compares the field value to the value specified. Returns success if the values are the same.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class CompareFieldValue : Conditional
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x00019158 File Offset: 0x00017358
		public override TaskStatus OnUpdate()
		{
			if (this.compareValue == null)
			{
				Debug.LogWarning("Unable to compare field - compare value is null");
				return TaskStatus.Failure;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to compare field - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to compare the field with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			object value = component.GetType().GetField(this.fieldName.Value).GetValue(component);
			if (value == null && this.compareValue.GetValue() == null)
			{
				return TaskStatus.Success;
			}
			if (!value.Equals(this.compareValue.GetValue()))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00019221 File Offset: 0x00017421
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.compareValue = null;
		}

		// Token: 0x0400034A RID: 842
		[Tooltip("The GameObject to compare the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400034B RID: 843
		[Tooltip("The component to compare the field on")]
		public SharedString componentName;

		// Token: 0x0400034C RID: 844
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x0400034D RID: 845
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
