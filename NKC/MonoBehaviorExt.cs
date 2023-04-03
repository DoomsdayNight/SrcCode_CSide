using System;
using System.Collections;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006F2 RID: 1778
	public static class MonoBehaviorExt
	{
		// Token: 0x06003F22 RID: 16162 RVA: 0x0014808C File Offset: 0x0014628C
		public static Coroutine<T> StartCoroutine<T>(this MonoBehaviour obj, IEnumerator coroutine)
		{
			Coroutine<T> coroutine2 = new Coroutine<T>();
			coroutine2.coroutine = obj.StartCoroutine(coroutine2.InternalRoutine(coroutine));
			return coroutine2;
		}
	}
}
