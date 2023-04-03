using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DA RID: 218
	[TaskDescription("Compares the property value to the value specified. Returns success if the values are the same.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class ComparePropertyValue : Conditional
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00019248 File Offset: 0x00017448
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
				Debug.LogWarning("Unable to compare property - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to compare the property with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			object value = component.GetType().GetProperty(this.propertyName.Value).GetValue(component, null);
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

		// Token: 0x06000706 RID: 1798 RVA: 0x00019312 File Offset: 0x00017512
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.compareValue = null;
		}

		// Token: 0x0400034E RID: 846
		[Tooltip("The GameObject to compare the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400034F RID: 847
		[Tooltip("The component to compare the property of")]
		public SharedString componentName;

		// Token: 0x04000350 RID: 848
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04000351 RID: 849
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
