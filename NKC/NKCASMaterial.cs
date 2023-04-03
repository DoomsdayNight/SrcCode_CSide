using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200062E RID: 1582
	public class NKCASMaterial : NKMObjectPoolData
	{
		// Token: 0x060030EE RID: 12526 RVA: 0x000F298B File Offset: 0x000F0B8B
		public NKCASMaterial(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.Load(bAsync);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000F29AF File Offset: 0x000F0BAF
		public override void Load(bool bAsync)
		{
			this.m_MaterialOrg = NKCAssetResourceManager.OpenResource<Material>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000F29CA File Offset: 0x000F0BCA
		public override bool LoadComplete()
		{
			if (this.m_MaterialOrg == null || this.m_MaterialOrg.GetAsset<Material>() == null)
			{
				return false;
			}
			this.m_Material = new Material(this.m_MaterialOrg.GetAsset<Material>());
			return true;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000F2A00 File Offset: 0x000F0C00
		public override void Unload()
		{
			this.m_Material = null;
			NKCAssetResourceManager.CloseResource(this.m_MaterialOrg);
			this.m_MaterialOrg = null;
		}

		// Token: 0x0400304D RID: 12365
		public NKCAssetResourceData m_MaterialOrg;

		// Token: 0x0400304E RID: 12366
		public Material m_Material;
	}
}
