using System;
using NKM;
using Spine.Unity;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000940 RID: 2368
	public class NKCUIComPrefabLoader : MonoBehaviour, INKCUIValidator
	{
		// Token: 0x06005E9F RID: 24223 RVA: 0x001D5DCE File Offset: 0x001D3FCE
		private void Awake()
		{
			this.Validate();
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x001D5DD8 File Offset: 0x001D3FD8
		public void Validate()
		{
			if (this.m_bValidated)
			{
				return;
			}
			this.m_bValidated = true;
			if (this.m_targetObj == null || this.m_targetObj.gameObject.name.Contains("Missing Prefab"))
			{
				this.ReplaceObject();
			}
			else
			{
				Transform transform = this.m_targetObj.transform.Find("SPINE_SkeletonGraphic");
				if (transform != null)
				{
					SkeletonGraphic component = transform.GetComponent<SkeletonGraphic>();
					if (component == null || component.SkeletonDataAsset == null)
					{
						this.RebuildSpine(component);
					}
				}
			}
			if (this.m_bUseTwoPassTransparency && this.m_targetObj != null)
			{
				SkeletonGraphic[] componentsInChildren = this.m_targetObj.GetComponentsInChildren<SkeletonGraphic>(true);
				NKCASMaterial nkcasmaterial = (NKCASMaterial)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial, "shaders", "SkeletonGraphic2Pass", false);
				foreach (SkeletonGraphic skeletonGraphic in componentsInChildren)
				{
					skeletonGraphic.material = nkcasmaterial.m_Material;
					skeletonGraphic.MeshGenerator.settings.zSpacing = -0.0001f;
				}
				nkcasmaterial.Close();
				nkcasmaterial.Unload();
			}
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x001D5EF4 File Offset: 0x001D40F4
		private void RebuildSpine(SkeletonGraphic graphic)
		{
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>(this.m_BundleName, this.m_AssetName, false, null);
			if (nkcassetResourceData == null)
			{
				return;
			}
			GameObject asset = nkcassetResourceData.GetAsset<GameObject>();
			if (asset == null)
			{
				return;
			}
			Transform transform = asset.transform.Find("SPINE_SkeletonGraphic");
			if (transform == null)
			{
				return;
			}
			graphic.transform.localPosition = transform.localPosition;
			graphic.transform.localRotation = transform.localRotation;
			graphic.transform.localScale = transform.localScale;
			SkeletonGraphic component = transform.GetComponent<SkeletonGraphic>();
			if (component == null)
			{
				return;
			}
			graphic.skeletonDataAsset = component.skeletonDataAsset;
			graphic.Initialize(true);
			NKCAssetResourceManager.CloseResource(nkcassetResourceData);
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x001D5FA4 File Offset: 0x001D41A4
		private void ReplaceObject()
		{
			if (this.m_targetObj != null)
			{
				UnityEngine.Object.Destroy(this.m_targetObj);
			}
			if (this.m_InstanceData == null)
			{
				this.m_InstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_BundleName, this.m_AssetName, false, null);
			}
			this.m_targetObj = this.m_InstanceData.m_Instant;
			if (this.m_targetObj != null)
			{
				this.m_targetObj.transform.parent = base.transform;
				this.m_targetObj.transform.localPosition = this.Position;
				this.m_targetObj.transform.localScale = this.Scale;
			}
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x001D604C File Offset: 0x001D424C
		private void OnDestroy()
		{
			if (this.m_InstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			}
		}

		// Token: 0x04004AC0 RID: 19136
		public string m_BundleName;

		// Token: 0x04004AC1 RID: 19137
		public string m_AssetName;

		// Token: 0x04004AC2 RID: 19138
		public Vector3 Position = Vector3.zero;

		// Token: 0x04004AC3 RID: 19139
		public Vector3 Scale = Vector3.one;

		// Token: 0x04004AC4 RID: 19140
		public GameObject m_targetObj;

		// Token: 0x04004AC5 RID: 19141
		public bool m_bUseTwoPassTransparency;

		// Token: 0x04004AC6 RID: 19142
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04004AC7 RID: 19143
		private bool m_bValidated;
	}
}
