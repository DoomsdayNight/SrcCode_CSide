using System;
using NKM;
using UnityEngine;
using UnityEngine.Rendering;

namespace NKC
{
	// Token: 0x02000634 RID: 1588
	public class NKCASUnitSprite : NKMObjectPoolData
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x000F50F2 File Offset: 0x000F32F2
		public NKCASUnitSprite(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSprite;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000F5120 File Offset: 0x000F3320
		public override void Load(bool bAsync)
		{
			this.m_UnitSpriteInstant = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
			this.m_MainMaterial = (NKCASMaterial)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial, "AB_MATERIAL", "MAT_NKC_MAIN", bAsync);
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000F516C File Offset: 0x000F336C
		public override bool LoadComplete()
		{
			if (this.m_UnitSpriteInstant == null || this.m_UnitSpriteInstant.m_Instant == null || this.m_MainMaterial == null || this.m_MainMaterial.m_Material == null)
			{
				return false;
			}
			GameObject asset = this.m_UnitSpriteInstant.m_InstantOrg.GetAsset<GameObject>();
			this.m_OrgSpriteRenderer = asset.GetComponentsInChildren<SpriteRenderer>(true);
			this.m_MainSpriteRenderer = this.m_UnitSpriteInstant.m_Instant.GetComponentsInChildren<SpriteRenderer>(true);
			this.m_OrgColor = new Color[this.m_OrgSpriteRenderer.Length];
			for (int i = 0; i < this.m_OrgSpriteRenderer.Length; i++)
			{
				this.m_OrgColor[i] = this.m_OrgSpriteRenderer[i].color;
			}
			this.m_MainParticleSystemRenderer = this.m_UnitSpriteInstant.m_Instant.GetComponentsInChildren<ParticleSystemRenderer>(true);
			NKCUtil.SetParticleSystemRendererSortOrder(this.m_MainParticleSystemRenderer, true);
			this.m_SortingGroup = this.m_UnitSpriteInstant.m_Instant.GetComponentInChildren<SortingGroup>();
			if (this.m_SortingGroup != null)
			{
				this.m_OrgSortingLayerName = this.m_SortingGroup.sortingLayerName;
			}
			return true;
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000F527E File Offset: 0x000F347E
		public override void Open()
		{
			if (!this.m_UnitSpriteInstant.m_Instant.activeSelf)
			{
				this.m_UnitSpriteInstant.m_Instant.SetActive(true);
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000F52A3 File Offset: 0x000F34A3
		public override void Close()
		{
			if (this.m_SortingGroup != null)
			{
				this.m_SortingGroup.sortingLayerName = this.m_OrgSortingLayerName;
			}
			this.m_UnitSpriteInstant.Close();
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000F52CF File Offset: 0x000F34CF
		public void ChangeSortingLayerName(string layerName)
		{
			if (this.m_SortingGroup != null)
			{
				this.m_SortingGroup.sortingLayerName = layerName;
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000F52EC File Offset: 0x000F34EC
		public override void Unload()
		{
			this.m_MainParticleSystemRenderer = null;
			this.m_MainSpriteRenderer = null;
			this.m_MainMaterial.Unload();
			this.m_MainMaterial = null;
			NKCAssetResourceManager.CloseInstance(this.m_UnitSpriteInstant);
			this.m_UnitSpriteInstant = null;
			this.m_SortingGroup = null;
			this.m_MainSpriteRenderer = null;
			this.m_MainParticleSystemRenderer = null;
			this.m_OrgSpriteRenderer = null;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000F5348 File Offset: 0x000F3548
		public void SetColor(float fR, float fG, float fB, float fA = -1f)
		{
			bool flag = false;
			if (this.m_fColorR != fR)
			{
				this.m_fColorR = fR;
				flag = true;
			}
			if (this.m_fColorG != fG)
			{
				this.m_fColorG = fG;
				flag = true;
			}
			if (this.m_fColorB != fB)
			{
				this.m_fColorB = fB;
				flag = true;
			}
			if (this.m_fColorA != fA)
			{
				this.m_fColorA = fA;
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			for (int i = 0; i < this.m_MainSpriteRenderer.Length; i++)
			{
				Color color = this.m_OrgColor[i];
				color.r *= fR;
				color.g *= fG;
				color.b *= fB;
				if (fA != -1f)
				{
					color.a *= fA;
				}
				this.m_MainSpriteRenderer[i].color = color;
			}
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000F541C File Offset: 0x000F361C
		public void SetMaterial(bool bOrg = false)
		{
			for (int i = 0; i < this.m_MainSpriteRenderer.Length; i++)
			{
				if (bOrg)
				{
					this.m_MainSpriteRenderer[i].sharedMaterial = this.m_OrgSpriteRenderer[i].sharedMaterial;
				}
				else
				{
					this.m_MainSpriteRenderer[i].sharedMaterial = this.m_MainMaterial.m_Material;
				}
			}
		}

		// Token: 0x04003092 RID: 12434
		public NKCAssetInstanceData m_UnitSpriteInstant;

		// Token: 0x04003093 RID: 12435
		public NKCASMaterial m_MainMaterial;

		// Token: 0x04003094 RID: 12436
		public SortingGroup m_SortingGroup;

		// Token: 0x04003095 RID: 12437
		public SpriteRenderer[] m_MainSpriteRenderer;

		// Token: 0x04003096 RID: 12438
		public ParticleSystemRenderer[] m_MainParticleSystemRenderer;

		// Token: 0x04003097 RID: 12439
		public SpriteRenderer[] m_OrgSpriteRenderer;

		// Token: 0x04003098 RID: 12440
		public Color[] m_OrgColor;

		// Token: 0x04003099 RID: 12441
		public float m_fColorR;

		// Token: 0x0400309A RID: 12442
		public float m_fColorG;

		// Token: 0x0400309B RID: 12443
		public float m_fColorB;

		// Token: 0x0400309C RID: 12444
		public float m_fColorA;

		// Token: 0x0400309D RID: 12445
		private string m_OrgSortingLayerName;
	}
}
