using System;
using UnityEngine;

namespace NKC.DebugUtil
{
	// Token: 0x0200090A RID: 2314
	public class NKCObjectUnityFunctionLog : MonoBehaviour
	{
		// Token: 0x06005C78 RID: 23672 RVA: 0x001C9A08 File Offset: 0x001C7C08
		private void Awake()
		{
			if (this.m_bAwake)
			{
				Debug.Log(base.gameObject.name + " Awake");
			}
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x001C9A2C File Offset: 0x001C7C2C
		private void Start()
		{
			if (this.m_bStart)
			{
				Debug.Log(base.gameObject.name + " Start");
			}
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x001C9A50 File Offset: 0x001C7C50
		private void Update()
		{
			if (this.m_bUpdate)
			{
				Debug.Log(base.gameObject.name + " Update");
			}
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x001C9A74 File Offset: 0x001C7C74
		private void OnEnable()
		{
			if (this.m_bOnEnable)
			{
				Debug.Log(base.gameObject.name + " OnEnable");
			}
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x001C9A98 File Offset: 0x001C7C98
		private void OnDisable()
		{
			if (this.m_bOnDisable)
			{
				Debug.Log(base.gameObject.name + " OnDisable");
			}
		}

		// Token: 0x040048E0 RID: 18656
		public bool m_bAwake;

		// Token: 0x040048E1 RID: 18657
		public bool m_bStart;

		// Token: 0x040048E2 RID: 18658
		public bool m_bUpdate;

		// Token: 0x040048E3 RID: 18659
		public bool m_bOnEnable;

		// Token: 0x040048E4 RID: 18660
		public bool m_bOnDisable;
	}
}
