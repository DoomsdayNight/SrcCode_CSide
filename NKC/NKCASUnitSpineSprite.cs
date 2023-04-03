using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000633 RID: 1587
	public class NKCASUnitSpineSprite : NKMObjectPoolData
	{
		// Token: 0x06003158 RID: 12632 RVA: 0x000F4564 File Offset: 0x000F2764
		public NKCASUnitSpineSprite(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSpineSprite;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000F45D0 File Offset: 0x000F27D0
		public override void Load(bool bAsync)
		{
			this.m_UnitSpineSpriteInstant = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000F45EB File Offset: 0x000F27EB
		public void SetReplaceMatResource(string bundleName, string assetName, bool bAsync = false)
		{
			if (this.m_ReplaceMatResource != null)
			{
				NKCAssetResourceManager.CloseResource(this.m_ReplaceMatResource);
				this.m_ReplaceMatResource = null;
			}
			this.m_ReplaceMaterial = null;
			if (assetName.Length > 1)
			{
				this.m_ReplaceMatResource = NKCAssetResourceManager.OpenResource<Material>(bundleName, assetName, bAsync, null);
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000F4628 File Offset: 0x000F2828
		public void SetOverrideMaterial()
		{
			if (!this.m_bIsLoaded)
			{
				return;
			}
			if (this.m_cSkeletonAnimation == null)
			{
				this.ClearReplaceMat();
				return;
			}
			if (this.m_ReplaceMatResource == null)
			{
				this.ClearReplaceMat();
				return;
			}
			if (!this.m_ReplaceMatResource.IsDone())
			{
				this.ClearReplaceMat();
				return;
			}
			Material asset = this.m_ReplaceMatResource.GetAsset<Material>();
			if (asset == null)
			{
				this.ClearReplaceMat();
				return;
			}
			this.m_ReplaceMaterial = new Material(asset);
			this.m_ReplaceDissolveMaterial = new Material(asset);
			this.m_ReplaceDissolveMaterial.EnableKeyword("DISSOLVE_ON");
			this.m_cSkeletonAnimation.CustomMaterialOverride.Clear();
			if (!this.m_cSkeletonAnimation.CustomMaterialOverride.ContainsKey(this.m_Material))
			{
				this.m_cSkeletonAnimation.CustomMaterialOverride.Add(this.m_Material, this.m_ReplaceMaterial);
			}
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000F46FE File Offset: 0x000F28FE
		protected void ClearReplaceMat()
		{
			SkeletonAnimation cSkeletonAnimation = this.m_cSkeletonAnimation;
			if (cSkeletonAnimation != null)
			{
				cSkeletonAnimation.CustomMaterialOverride.Clear();
			}
			this.m_ReplaceMaterial = null;
			this.m_ReplaceDissolveMaterial = null;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000F4724 File Offset: 0x000F2924
		public override bool LoadComplete()
		{
			if (this.m_UnitSpineSpriteInstant == null || this.m_UnitSpineSpriteInstant.m_Instant == null)
			{
				return false;
			}
			NKCComSpineAnimControl component = this.m_UnitSpineSpriteInstant.m_Instant.GetComponent<NKCComSpineAnimControl>();
			if (component != null)
			{
				component.enabled = false;
			}
			Transform transform = this.m_UnitSpineSpriteInstant.m_InstantOrg.GetAsset<GameObject>().transform.Find("SPINE_SkeletonAnimation");
			if (transform == null)
			{
				Log.Error(this.m_ObjectPoolName + " has no sub prefab name SPINE_SkeletonAnimation", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCASUnitSpineSprite.cs", 137);
			}
			this.m_SPINE_SkeletonAnimationOrg = transform.gameObject;
			this.m_MeshRendererOrg = this.m_SPINE_SkeletonAnimationOrg.GetComponentInChildren<MeshRenderer>(true);
			this.m_SortingLayerNameOrg = this.m_MeshRendererOrg.sortingLayerName;
			if (this.m_MeshRendererOrg.sharedMaterial == null)
			{
				Debug.LogErrorFormat("m_MeshRendererOrg.sharedMaterial is null : {0} / {1}", new object[]
				{
					this.m_ObjectPoolBundleName,
					this.m_ObjectPoolName
				});
				this.m_DissolveMaterial = null;
			}
			else
			{
				this.m_DissolveMaterial = new Material(this.m_MeshRendererOrg.sharedMaterial);
			}
			if (this.m_DissolveMaterial != null)
			{
				this.m_DissolveMaterial.EnableKeyword("DISSOLVE_ON");
			}
			this.m_SPINE_SkeletonAnimation = this.m_UnitSpineSpriteInstant.m_Instant.transform.Find("SPINE_SkeletonAnimation").gameObject;
			this.m_MeshRenderer = this.m_SPINE_SkeletonAnimation.GetComponentInChildren<MeshRenderer>(true);
			if (this.m_MeshRenderer == null)
			{
				Debug.LogErrorFormat("m_MeshRenderer is null : {0} / {1}", new object[]
				{
					this.m_ObjectPoolBundleName,
					this.m_ObjectPoolName
				});
			}
			else
			{
				this.m_Material = this.m_MeshRenderer.sharedMaterial;
			}
			this.m_MainParticleSystemRenderer = this.m_UnitSpineSpriteInstant.m_Instant.GetComponentsInChildren<ParticleSystemRenderer>(true);
			NKCUtil.SetParticleSystemRendererSortOrder(this.m_MainParticleSystemRenderer, true);
			this.m_Color.r = 1f;
			this.m_Color.g = 1f;
			this.m_Color.b = 1f;
			this.m_Color.a = 1f;
			if (this.m_DissolveMaterial != null)
			{
				this.m_DissolveColorOrg = this.m_DissolveMaterial.GetColor("_DissolveGlowColor");
			}
			this.m_cSkeletonAnimation = this.m_SPINE_SkeletonAnimation.GetComponent<SkeletonAnimation>();
			if (this.m_cSkeletonAnimation != null)
			{
				this.m_Bone_Move = this.m_cSkeletonAnimation.skeleton.FindBone("MOVE");
				this.m_Bone_Head = this.m_cSkeletonAnimation.skeleton.FindBone("BIP01_HEAD");
			}
			this.SetOverrideMaterial();
			if (this.m_cSkeletonAnimation.maskMaterials != null)
			{
				for (int i = 0; i < this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled.Length; i++)
				{
					this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled[i] = new Material(this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled[i]);
				}
			}
			return true;
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000F4A08 File Offset: 0x000F2C08
		public override void Open()
		{
			if (this.m_UnitSpineSpriteInstant != null && this.m_UnitSpineSpriteInstant.m_Instant != null && !this.m_UnitSpineSpriteInstant.m_Instant.activeSelf)
			{
				this.m_UnitSpineSpriteInstant.m_Instant.SetActive(true);
			}
			this.SetColor(1f, 1f, 1f, 1f, true);
			this.SetOverrideMaterial();
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000F4A74 File Offset: 0x000F2C74
		public override void Close()
		{
			this.SetSortingLayerName(this.m_SortingLayerNameOrg);
			NKCAssetInstanceData unitSpineSpriteInstant = this.m_UnitSpineSpriteInstant;
			if (unitSpineSpriteInstant != null)
			{
				unitSpineSpriteInstant.Close();
			}
			if (this.m_MeshRenderer != null)
			{
				this.m_MeshRenderer.sharedMaterial = this.m_Material;
				this.m_MeshRenderer.SetPropertyBlock(null);
			}
			this.ClearReplaceMat();
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000F4AD0 File Offset: 0x000F2CD0
		public override void Unload()
		{
			this.ClearReplaceMat();
			this.m_SPINE_SkeletonAnimationOrg = null;
			this.m_MeshRendererOrg = null;
			this.m_cSkeletonAnimation = null;
			this.m_SPINE_SkeletonAnimation = null;
			this.m_MeshRenderer = null;
			this.m_Material = null;
			this.m_DissolveMaterial = null;
			this.m_MPB = null;
			this.m_MainParticleSystemRenderer = null;
			this.m_Bone_Move = null;
			this.m_Bone_Head = null;
			this.m_ReplaceMaterial = null;
			this.m_ReplaceDissolveMaterial = null;
			this.m_MainParticleSystemRenderer = null;
			this.m_Bone_Move = null;
			this.m_Bone_Head = null;
			this.m_dicBone.Clear();
			if (this.m_ReplaceMatResource != null)
			{
				NKCAssetResourceManager.CloseResource(this.m_ReplaceMatResource);
			}
			this.m_ReplaceMatResource = null;
			NKCAssetResourceManager.CloseInstance(this.m_UnitSpineSpriteInstant);
			this.m_UnitSpineSpriteInstant = null;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000F4B8C File Offset: 0x000F2D8C
		public void SetColor(float fR, float fG, float fB, float fA = -1f, bool bForce = false)
		{
			if (!bForce)
			{
				bool flag = false;
				if (this.m_Color.r != fR)
				{
					this.m_Color.r = fR;
					flag = true;
				}
				if (this.m_Color.g != fG)
				{
					this.m_Color.g = fG;
					flag = true;
				}
				if (this.m_Color.b != fB)
				{
					this.m_Color.b = fB;
					flag = true;
				}
				if (fA != -1f && this.m_Color.a != fA)
				{
					this.m_Color.a = fA;
					flag = true;
				}
				if (!flag)
				{
					return;
				}
			}
			if (this.m_Color.r == 1f && this.m_Color.g == 1f && this.m_Color.b == 1f && this.m_Color.a == 1f)
			{
				if (this.m_MeshRenderer != null)
				{
					this.m_MeshRenderer.SetPropertyBlock(null);
					return;
				}
			}
			else
			{
				this.m_MPB.SetColor(Shader.PropertyToID("_Color"), this.m_Color);
				if (this.m_MeshRenderer != null)
				{
					this.m_MeshRenderer.SetPropertyBlock(this.m_MPB);
				}
			}
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000F4CBE File Offset: 0x000F2EBE
		public void SetSortingLayerName(string sortingLayerName)
		{
			if (this.m_MeshRenderer != null)
			{
				this.m_MeshRenderer.sortingLayerName = sortingLayerName;
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000F4CDC File Offset: 0x000F2EDC
		public void SetDissolveOn(bool bOn)
		{
			if (this.m_DissolveMaterial == null)
			{
				return;
			}
			if (this.m_Material == null)
			{
				return;
			}
			if (this.m_cSkeletonAnimation == null)
			{
				return;
			}
			if (bOn)
			{
				if (this.m_cSkeletonAnimation.CustomMaterialOverride.ContainsKey(this.m_Material))
				{
					this.m_cSkeletonAnimation.CustomMaterialOverride.Remove(this.m_Material);
				}
				if (this.m_ReplaceDissolveMaterial == null)
				{
					this.m_cSkeletonAnimation.CustomMaterialOverride.Add(this.m_Material, this.m_DissolveMaterial);
				}
				else
				{
					this.m_cSkeletonAnimation.CustomMaterialOverride.Add(this.m_Material, this.m_ReplaceDissolveMaterial);
				}
				if (this.m_cSkeletonAnimation.maskMaterials != null)
				{
					for (int i = 0; i < this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled.Length; i++)
					{
						this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled[i].EnableKeyword("DISSOLVE_ON");
					}
					return;
				}
			}
			else
			{
				if (this.m_cSkeletonAnimation.CustomMaterialOverride.ContainsKey(this.m_Material))
				{
					this.m_cSkeletonAnimation.CustomMaterialOverride.Remove(this.m_Material);
				}
				if (this.m_ReplaceMaterial != null)
				{
					this.m_cSkeletonAnimation.CustomMaterialOverride.Add(this.m_Material, this.m_ReplaceMaterial);
				}
				if (this.m_cSkeletonAnimation.maskMaterials != null)
				{
					for (int j = 0; j < this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled.Length; j++)
					{
						this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled[j].DisableKeyword("DISSOLVE_ON");
					}
				}
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000F4E7C File Offset: 0x000F307C
		public void SetDissolveBlend(float fBlend)
		{
			if (this.m_ReplaceDissolveMaterial != null)
			{
				this.m_ReplaceDissolveMaterial.SetFloat("_DissolveBlend", fBlend);
			}
			else if (this.m_DissolveMaterial != null)
			{
				this.m_DissolveMaterial.SetFloat("_DissolveBlend", fBlend);
			}
			if (this.m_cSkeletonAnimation.maskMaterials != null)
			{
				for (int i = 0; i < this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled.Length; i++)
				{
					this.m_cSkeletonAnimation.maskMaterials.materialsMaskDisabled[i].SetFloat("_DissolveBlend", fBlend);
				}
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000F4F10 File Offset: 0x000F3110
		public void SetDissolveColor(Color color)
		{
			if (color.r == -1f)
			{
				color.r = this.m_DissolveColorOrg.r;
			}
			if (color.g == -1f)
			{
				color.g = this.m_DissolveColorOrg.g;
			}
			if (color.b == -1f)
			{
				color.b = this.m_DissolveColorOrg.b;
			}
			if (color.a == -1f)
			{
				color.a = this.m_DissolveColorOrg.a;
			}
			if (this.m_ReplaceDissolveMaterial != null)
			{
				this.m_ReplaceDissolveMaterial.SetColor("_DissolveGlowColor", color);
				return;
			}
			if (this.m_DissolveMaterial != null)
			{
				this.m_DissolveMaterial.SetColor("_DissolveGlowColor", color);
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000F4FD8 File Offset: 0x000F31D8
		public void CalcBoneMoveWorldPos()
		{
			if (this.m_Bone_Move != null)
			{
				this.m_Bone_MovePos = this.m_SPINE_SkeletonAnimation.transform.TransformPoint(this.m_Bone_Move.WorldX, this.m_Bone_Move.WorldY, 0f);
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000F5013 File Offset: 0x000F3213
		public void CalcBoneHeadWorldPos()
		{
			if (this.m_Bone_Head != null)
			{
				this.m_Bone_HeadPos = this.m_SPINE_SkeletonAnimation.transform.TransformPoint(this.m_Bone_Head.WorldX, this.m_Bone_Head.WorldY, 0f);
			}
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000F5050 File Offset: 0x000F3250
		public Bone GetBone(string boneName)
		{
			if (this.m_cSkeletonAnimation == null)
			{
				return null;
			}
			if (!this.m_dicBone.ContainsKey(boneName))
			{
				Bone bone = this.m_cSkeletonAnimation.skeleton.FindBone(boneName);
				if (bone == null)
				{
					return null;
				}
				this.m_dicBone.Add(boneName, bone);
			}
			return this.m_dicBone[boneName];
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000F50B0 File Offset: 0x000F32B0
		public bool GetBoneWorldPos(string boneName, ref Vector3 worldPos)
		{
			Bone bone = this.GetBone(boneName);
			if (bone == null)
			{
				return false;
			}
			worldPos = this.m_SPINE_SkeletonAnimation.transform.TransformPoint(bone.WorldX, bone.WorldY, 0f);
			return true;
		}

		// Token: 0x0400307D RID: 12413
		public NKCAssetInstanceData m_UnitSpineSpriteInstant;

		// Token: 0x0400307E RID: 12414
		public GameObject m_SPINE_SkeletonAnimationOrg;

		// Token: 0x0400307F RID: 12415
		public MeshRenderer m_MeshRendererOrg;

		// Token: 0x04003080 RID: 12416
		public SkeletonAnimation m_cSkeletonAnimation;

		// Token: 0x04003081 RID: 12417
		public string m_SortingLayerNameOrg;

		// Token: 0x04003082 RID: 12418
		public GameObject m_SPINE_SkeletonAnimation;

		// Token: 0x04003083 RID: 12419
		public MeshRenderer m_MeshRenderer;

		// Token: 0x04003084 RID: 12420
		protected Material m_Material;

		// Token: 0x04003085 RID: 12421
		protected Material m_DissolveMaterial;

		// Token: 0x04003086 RID: 12422
		protected Color m_DissolveColorOrg;

		// Token: 0x04003087 RID: 12423
		protected NKCAssetResourceData m_ReplaceMatResource;

		// Token: 0x04003088 RID: 12424
		protected Material m_ReplaceMaterial;

		// Token: 0x04003089 RID: 12425
		protected Material m_ReplaceDissolveMaterial;

		// Token: 0x0400308A RID: 12426
		protected MaterialPropertyBlock m_MPB = new MaterialPropertyBlock();

		// Token: 0x0400308B RID: 12427
		public ParticleSystemRenderer[] m_MainParticleSystemRenderer;

		// Token: 0x0400308C RID: 12428
		protected Color m_Color = new Color(1f, 1f, 1f, 1f);

		// Token: 0x0400308D RID: 12429
		public Bone m_Bone_Move;

		// Token: 0x0400308E RID: 12430
		public Vector3 m_Bone_MovePos;

		// Token: 0x0400308F RID: 12431
		public Bone m_Bone_Head;

		// Token: 0x04003090 RID: 12432
		public Vector3 m_Bone_HeadPos;

		// Token: 0x04003091 RID: 12433
		private Dictionary<string, Bone> m_dicBone = new Dictionary<string, Bone>();
	}
}
