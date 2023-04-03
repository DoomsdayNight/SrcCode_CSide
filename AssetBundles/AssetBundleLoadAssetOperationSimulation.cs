using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000050 RID: 80
	public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000A75F File Offset: 0x0000895F
		public AssetBundleLoadAssetOperationSimulation(UnityEngine.Object simulatedObject)
		{
			this.m_SimulatedObject = simulatedObject;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A76E File Offset: 0x0000896E
		public override T GetAsset<T>()
		{
			return this.m_SimulatedObject as T;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A780 File Offset: 0x00008980
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A783 File Offset: 0x00008983
		public override bool IsDone()
		{
			return true;
		}

		// Token: 0x040001C3 RID: 451
		private UnityEngine.Object m_SimulatedObject;
	}
}
