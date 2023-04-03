using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000BC RID: 188
	[TaskDescription("Invokes the specified method with the specified parameters. Can optionally store the return value. Returns success if the method was invoked.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class InvokeMethod : Action
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00016F3C File Offset: 0x0001513C
		public override TaskStatus OnUpdate()
		{
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to invoke - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to invoke method with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			List<object> list = new List<object>();
			List<Type> list2 = new List<Type>();
			int num = 0;
			SharedVariable sharedVariable;
			while (num < 4 && (sharedVariable = (base.GetType().GetField("parameter" + (num + 1).ToString()).GetValue(this) as SharedVariable)) != null)
			{
				list.Add(sharedVariable.GetValue());
				list2.Add(sharedVariable.GetType().GetProperty("Value").PropertyType);
				num++;
			}
			MethodInfo method = component.GetType().GetMethod(this.methodName.Value, list2.ToArray());
			if (method == null)
			{
				Debug.LogWarning("Unable to invoke method " + this.methodName.Value + " on component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			object value = method.Invoke(component, list.ToArray());
			if (this.storeResult != null)
			{
				this.storeResult.SetValue(value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000170A0 File Offset: 0x000152A0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.methodName = null;
			this.parameter1 = null;
			this.parameter2 = null;
			this.parameter3 = null;
			this.parameter4 = null;
			this.storeResult = null;
		}

		// Token: 0x040002DB RID: 731
		[Tooltip("The GameObject to invoke the method on")]
		public SharedGameObject targetGameObject;

		// Token: 0x040002DC RID: 732
		[Tooltip("The component to invoke the method on")]
		public SharedString componentName;

		// Token: 0x040002DD RID: 733
		[Tooltip("The name of the method")]
		public SharedString methodName;

		// Token: 0x040002DE RID: 734
		[Tooltip("The first parameter of the method")]
		public SharedVariable parameter1;

		// Token: 0x040002DF RID: 735
		[Tooltip("The second parameter of the method")]
		public SharedVariable parameter2;

		// Token: 0x040002E0 RID: 736
		[Tooltip("The third parameter of the method")]
		public SharedVariable parameter3;

		// Token: 0x040002E1 RID: 737
		[Tooltip("The fourth parameter of the method")]
		public SharedVariable parameter4;

		// Token: 0x040002E2 RID: 738
		[Tooltip("Store the result of the invoke call")]
		public SharedVariable storeResult;
	}
}
