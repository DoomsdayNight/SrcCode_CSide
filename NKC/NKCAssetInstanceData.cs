using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200063D RID: 1597
	public class NKCAssetInstanceData
	{
		// Token: 0x060031B6 RID: 12726 RVA: 0x000F6E24 File Offset: 0x000F5024
		public NKCAssetInstanceData()
		{
			this.Init();
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000F6E3D File Offset: 0x000F503D
		public bool GetLoadFail()
		{
			return this.m_InstantOrg != null && ((this.m_InstantOrg.IsDone() && this.m_InstantOrg.GetAsset<UnityEngine.Object>() == null) || this.m_InstantOrg.GetLoadFail());
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000F6E78 File Offset: 0x000F5078
		public void Init()
		{
			this.m_BundleName = "";
			this.m_AssetName = "";
			this.m_InstantOrg = null;
			this.m_Instant = null;
			this.m_bLoad = false;
			this.m_Transforms = null;
			this.m_dicTransform.Clear();
			this.m_fTime = 0f;
			this.m_bLoadTypeAsync = false;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000F6ED4 File Offset: 0x000F50D4
		public void Unload()
		{
			if (this.m_Instant != null)
			{
				UnityEngine.Object.Destroy(this.m_Instant);
				NKCAssetResourceManager.CloseResource(this.m_InstantOrg);
			}
			this.Init();
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000F6F04 File Offset: 0x000F5104
		public void BuildTranformDic()
		{
			this.m_Transforms = this.m_Instant.GetComponentsInChildren<Transform>();
			this.m_dicTransform.Clear();
			for (int i = 0; i < this.m_Transforms.Length; i++)
			{
				if (!this.m_dicTransform.ContainsKey(this.m_Transforms[i].name))
				{
					this.m_dicTransform.Add(this.m_Transforms[i].name, this.m_Transforms[i]);
				}
			}
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000F6F7C File Offset: 0x000F517C
		public Transform GetTransform(string boneName)
		{
			if (this.m_dicTransform.Count == 0)
			{
				this.BuildTranformDic();
			}
			for (int i = 0; i < this.m_Transforms.Length; i++)
			{
				if (this.m_dicTransform.ContainsKey(boneName))
				{
					return this.m_dicTransform[boneName];
				}
			}
			return null;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000F6FCC File Offset: 0x000F51CC
		public void Close()
		{
			if (this.m_Instant == null)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().Get_NKM_NEW_INSTANT() != null)
			{
				this.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_NKM_NEW_INSTANT().transform);
			}
			this.ResetTransformToOrg();
			if (this.m_Instant.activeSelf)
			{
				this.m_Instant.SetActive(false);
			}
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000F7038 File Offset: 0x000F5238
		public void ResetTransformToOrg()
		{
			if (this.m_InstantOrg == null || this.m_Instant == null)
			{
				return;
			}
			GameObject asset = this.m_InstantOrg.GetAsset<GameObject>();
			if (asset == null)
			{
				return;
			}
			this.m_Instant.transform.localPosition = asset.transform.localPosition;
			this.m_Instant.transform.localRotation = asset.transform.localRotation;
			this.m_Instant.transform.localScale = asset.transform.localScale;
		}

		// Token: 0x040030DD RID: 12509
		public string m_BundleName;

		// Token: 0x040030DE RID: 12510
		public string m_AssetName;

		// Token: 0x040030DF RID: 12511
		public NKCAssetResourceData m_InstantOrg;

		// Token: 0x040030E0 RID: 12512
		public GameObject m_Instant;

		// Token: 0x040030E1 RID: 12513
		public bool m_bLoad;

		// Token: 0x040030E2 RID: 12514
		public Transform[] m_Transforms;

		// Token: 0x040030E3 RID: 12515
		public Dictionary<string, Transform> m_dicTransform = new Dictionary<string, Transform>();

		// Token: 0x040030E4 RID: 12516
		public float m_fTime;

		// Token: 0x040030E5 RID: 12517
		public bool m_bLoadTypeAsync;
	}
}
