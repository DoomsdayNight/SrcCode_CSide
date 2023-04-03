using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200074D RID: 1869
	public class NKCUICamFaceBillboard : MonoBehaviour
	{
		// Token: 0x06004A9F RID: 19103 RVA: 0x00165C34 File Offset: 0x00163E34
		private void Update()
		{
			base.transform.LookAt(base.transform.position + NKCCamera.GetCamera().transform.rotation * Vector3.forward, NKCCamera.GetCamera().transform.rotation * Vector3.up);
		}
	}
}
