using System;
using System.Collections;
using UnityEngine;

namespace NKC.AppTracking
{
	// Token: 0x02000C6C RID: 3180
	public class iOSAppTrackingChecker : MonoBehaviour
	{
		// Token: 0x060093D2 RID: 37842 RVA: 0x00327A87 File Offset: 0x00325C87
		public void StartIDFA()
		{
			base.StartCoroutine(this.StartIDFACoroutine());
		}

		// Token: 0x060093D3 RID: 37843 RVA: 0x00327A96 File Offset: 0x00325C96
		public IEnumerator StartIDFACoroutine()
		{
			yield return null;
			yield break;
		}
	}
}
