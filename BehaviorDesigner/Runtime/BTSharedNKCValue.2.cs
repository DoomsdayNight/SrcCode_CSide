using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000096 RID: 150
	public abstract class BTSharedNKCValue<T> : BTSharedNKCValue
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00016698 File Offset: 0x00014898
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000166B4 File Offset: 0x000148B4
		public T Value
		{
			get
			{
				if (this.mGetter != null)
				{
					return this.mGetter();
				}
				return this.mValue;
			}
			set
			{
				if (this.mSetter != null)
				{
					this.mSetter(value);
					return;
				}
				this.mValue = value;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000166D4 File Offset: 0x000148D4
		public override void InitializePropertyMapping(BehaviorSource behaviorSource)
		{
			if (!BehaviorManager.IsPlaying || !(behaviorSource.Owner.GetObject() is Behavior) || string.IsNullOrEmpty(base.PropertyMapping))
			{
				return;
			}
			string[] array = base.PropertyMapping.Split(new char[]
			{
				'/'
			});
			GameObject gameObject = null;
			try
			{
				gameObject = (object.Equals(base.PropertyMappingOwner, null) ? (behaviorSource.Owner.GetObject() as Behavior).gameObject : base.PropertyMappingOwner);
			}
			catch (Exception)
			{
				Behavior behavior = behaviorSource.Owner.GetObject() as Behavior;
				if (behavior != null && behavior.AsynchronousLoad)
				{
					Debug.LogError("Error: Unable to retrieve GameObject. Properties cannot be mapped while using asynchronous load.");
					return;
				}
			}
			if (gameObject == null)
			{
				Debug.LogError("Error: Unable to find GameObject on " + behaviorSource.behaviorName + " for property mapping with variable " + base.Name);
				return;
			}
			Component component = gameObject.GetComponent(TaskUtility.GetTypeWithinAssembly(array[0]));
			if (component == null)
			{
				Debug.LogError("Error: Unable to find component on " + behaviorSource.behaviorName + " for property mapping with variable " + base.Name);
				return;
			}
			PropertyInfo property = component.GetType().GetProperty(array[1]);
			if (property != null)
			{
				MethodInfo methodInfo = property.GetGetMethod();
				if (methodInfo != null)
				{
					this.mGetter = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), component, methodInfo);
				}
				methodInfo = property.GetSetMethod();
				if (methodInfo != null)
				{
					this.mSetter = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), component, methodInfo);
				}
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00016860 File Offset: 0x00014A60
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001686D File Offset: 0x00014A6D
		public override void SetValue(object value)
		{
			if (this.mSetter != null)
			{
				this.mSetter((T)((object)value));
				return;
			}
			this.mValue = (T)((object)value);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00016898 File Offset: 0x00014A98
		public override string ToString()
		{
			if (this.Value == null)
			{
				return "(null)";
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x040002C9 RID: 713
		private Func<T> mGetter;

		// Token: 0x040002CA RID: 714
		private Action<T> mSetter;

		// Token: 0x040002CB RID: 715
		[SerializeField]
		protected T mValue;
	}
}
