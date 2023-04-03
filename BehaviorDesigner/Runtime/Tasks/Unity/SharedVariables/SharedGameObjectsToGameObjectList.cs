using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000162 RID: 354
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedGameObjectList values from the GameObjects. Returns Success.")]
	public class SharedGameObjectsToGameObjectList : Action
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x0001D669 File Offset: 0x0001B869
		public override void OnAwake()
		{
			this.storedGameObjectList.Value = new List<GameObject>();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001D67C File Offset: 0x0001B87C
		public override TaskStatus OnUpdate()
		{
			if (this.gameObjects == null || this.gameObjects.Length == 0)
			{
				return TaskStatus.Failure;
			}
			this.storedGameObjectList.Value.Clear();
			for (int i = 0; i < this.gameObjects.Length; i++)
			{
				this.storedGameObjectList.Value.Add(this.gameObjects[i].Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001D6DD File Offset: 0x0001B8DD
		public override void OnReset()
		{
			this.gameObjects = null;
			this.storedGameObjectList = null;
		}

		// Token: 0x040004DC RID: 1244
		[Tooltip("The GameObjects value")]
		public SharedGameObject[] gameObjects;

		// Token: 0x040004DD RID: 1245
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedGameObjectList storedGameObjectList;
	}
}
