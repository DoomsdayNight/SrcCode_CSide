using System;
using NKC.FX;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007DB RID: 2011
	public class NKCWarfareGameAssist : MonoBehaviour
	{
		// Token: 0x06004F53 RID: 20307 RVA: 0x0017F5E8 File Offset: 0x0017D7E8
		public static NKCWarfareGameAssist GetNewInstance(Transform parent, Vector3 start, Vector3 target)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_PF_CMN", "AB_FX_PF_ARC_DISTANCE_WARFARE", false, null);
			NKCWarfareGameAssist component = nkcassetInstanceData.m_Instant.GetComponent<NKCWarfareGameAssist>();
			if (component == null)
			{
				Debug.LogError("NKCWarfareGameAssist Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.transform.localScale = Vector3.one;
			}
			component.Init(start, target);
			component.GetComponent<NKC_FXM_PLAYER>().Restart();
			return component;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x0017F668 File Offset: 0x0017D868
		private void Init(Vector3 start, Vector3 target)
		{
			this.m_Start.localPosition = start;
			this.m_Target.localPosition = target;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x0017F682 File Offset: 0x0017D882
		public void Close()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
		}

		// Token: 0x04003F49 RID: 16201
		public Transform m_Start;

		// Token: 0x04003F4A RID: 16202
		public Transform m_Target;

		// Token: 0x04003F4B RID: 16203
		private NKCAssetInstanceData m_instance;
	}
}
