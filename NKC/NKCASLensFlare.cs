using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200062D RID: 1581
	public class NKCASLensFlare : NKMObjectPoolData
	{
		// Token: 0x060030E5 RID: 12517 RVA: 0x000F27D5 File Offset: 0x000F09D5
		public float GetLensFlareBrightOrg()
		{
			return this.m_LensFlareBrightOrg;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000F27DD File Offset: 0x000F09DD
		public NKCASLensFlare(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASLensFlare;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000F2809 File Offset: 0x000F0A09
		public override void Load(bool bAsync)
		{
			this.m_LensFlareInstant = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000F2824 File Offset: 0x000F0A24
		public override bool LoadComplete()
		{
			if (this.m_LensFlareInstant == null || this.m_LensFlareInstant.m_Instant == null)
			{
				return false;
			}
			this.m_LensFlareInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKM_LENS_FLARE_LIST().transform, false);
			this.m_LensFlare = this.m_LensFlareInstant.m_Instant.GetComponent<LensFlare>();
			this.m_LensFlareBrightOrg = this.m_LensFlare.brightness;
			return true;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000F28A0 File Offset: 0x000F0AA0
		public override void Open()
		{
			if (!this.m_LensFlareInstant.m_Instant.activeSelf)
			{
				this.m_LensFlareInstant.m_Instant.SetActive(true);
			}
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000F28C5 File Offset: 0x000F0AC5
		public override void Close()
		{
			if (this.m_LensFlareInstant.m_Instant.activeSelf)
			{
				this.m_LensFlareInstant.m_Instant.SetActive(false);
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000F28EA File Offset: 0x000F0AEA
		public override void Unload()
		{
			NKCAssetResourceManager.CloseInstance(this.m_LensFlareInstant);
			this.m_LensFlareInstant = null;
			this.m_LensFlare = null;
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000F2908 File Offset: 0x000F0B08
		public void SetPos(float fX = -1f, float fY = -1f, float fZ = -1f)
		{
			Vector3 position = this.m_LensFlareInstant.m_Instant.transform.position;
			if (fX != -1f)
			{
				position.x = fX;
			}
			if (fY != -1f)
			{
				position.y = fY;
			}
			if (fZ != -1f)
			{
				position.z = fZ;
			}
			this.m_LensFlareInstant.m_Instant.transform.position = position;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000F2971 File Offset: 0x000F0B71
		public void SetLensFlareBright(float fBright)
		{
			this.m_LensFlareBrightNow = fBright;
			this.m_LensFlare.brightness = this.m_LensFlareBrightNow;
		}

		// Token: 0x04003049 RID: 12361
		public NKCAssetInstanceData m_LensFlareInstant;

		// Token: 0x0400304A RID: 12362
		public LensFlare m_LensFlare;

		// Token: 0x0400304B RID: 12363
		public float m_LensFlareBrightOrg;

		// Token: 0x0400304C RID: 12364
		public float m_LensFlareBrightNow;
	}
}
