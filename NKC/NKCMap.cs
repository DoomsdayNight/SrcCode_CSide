using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006A3 RID: 1699
	public class NKCMap
	{
		// Token: 0x06003808 RID: 14344 RVA: 0x0012141F File Offset: 0x0011F61F
		public SpriteRenderer GetInvalidLandRenderer()
		{
			return this.m_MapInvalidLand_SpriteRenderer;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x00121428 File Offset: 0x0011F628
		public NKCMap()
		{
			this.Init();
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x00121498 File Offset: 0x0011F698
		public void Init()
		{
			this.m_NKMMapTemplet = null;
			if (this.m_MapObject_m_MeshRenderer != null)
			{
				for (int i = 0; i < this.m_MapObject_m_MeshRenderer.Length; i++)
				{
					if (this.m_MapObject_m_MeshRenderer[i] != null)
					{
						this.m_MapObject_m_MeshRenderer[i].SetPropertyBlock(null);
					}
				}
			}
			this.m_MapObject_m_MeshRenderer = null;
			NKCAssetResourceManager.CloseInstance(this.m_MapObject);
			this.m_MapObject = null;
			this.m_MapObject_SpriteRenderer = null;
			this.m_fMapColorROrg = 1f;
			this.m_fMapColorGOrg = 1f;
			this.m_fMapColorBOrg = 1f;
			this.m_fMapColorRNow = 1f;
			this.m_fMapColorGNow = 1f;
			this.m_fMapColorBNow = 1f;
			this.m_MapColorROrg = null;
			this.m_MapColorGOrg = null;
			this.m_MapColorBOrg = null;
			this.m_MapColorRNow = null;
			this.m_MapColorGNow = null;
			this.m_MapColorBNow = null;
			this.m_fMapColorKeepTime = 0f;
			this.m_fMapColorReturnTime = 0f;
			this.m_fDeltaTime = 0f;
			NKCAssetResourceManager.CloseInstance(this.m_MapInvalidLand);
			this.m_MapInvalidLand = null;
			this.m_MapInvalidLand_SpriteRenderer = null;
			this.m_MapInvalidLandWidth.SetNowValue(0f);
			this.m_MapInvalidLandAlpha.SetNowValue(0f);
			this.m_fCustomWidth = 0f;
			this.ClearLensFlares();
			this.m_dicMapLayer.Clear();
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x001215E8 File Offset: 0x0011F7E8
		public void Load(int mapID, bool bAsync = true)
		{
			this.Init();
			this.m_NKMMapTemplet = NKMMapManager.GetMapTempletByID(mapID);
			this.m_MapObject = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_NKMMapTemplet.m_MapAssetName, bAsync, null);
			for (int i = 0; i < this.m_NKMMapTemplet.m_listBloomPoint.Count; i++)
			{
				NKMBloomPoint nkmbloomPoint = this.m_NKMMapTemplet.m_listBloomPoint[i];
				if (nkmbloomPoint.m_LensFlareName.m_AssetName.Length > 1)
				{
					NKCASLensFlare value = (NKCASLensFlare)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASLensFlare, nkmbloomPoint.m_LensFlareName.m_BundleName, nkmbloomPoint.m_LensFlareName.m_AssetName, bAsync);
					this.m_dicLensFlare.Add(i, value);
				}
			}
			this.m_MapInvalidLand = NKCAssetResourceManager.OpenInstance<GameObject>("AB_MAP_RESPAWN_INVALID_LAND", "AB_MAP_RESPAWN_INVALID_LAND", bAsync, null);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x001216B4 File Offset: 0x0011F8B4
		public bool LoadComplete()
		{
			if (!this.m_MapObject.m_Instant.activeSelf)
			{
				this.m_MapObject.m_Instant.SetActive(true);
			}
			this.m_MapObject.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_MAP().transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_MapObject.m_Instant, this.m_NKMMapTemplet.m_fInitPosX, 0f, 0f);
			this.m_MapObject_SpriteRenderer = this.m_MapObject.m_Instant.GetComponentsInChildren<SpriteRenderer>();
			this.m_MapObject_m_MeshRenderer = this.m_MapObject.m_Instant.GetComponentsInChildren<MeshRenderer>(true);
			this.m_MapColorROrg = new float[this.m_MapObject_SpriteRenderer.Length];
			this.m_MapColorGOrg = new float[this.m_MapObject_SpriteRenderer.Length];
			this.m_MapColorBOrg = new float[this.m_MapObject_SpriteRenderer.Length];
			this.m_MapColorRNow = new float[this.m_MapObject_SpriteRenderer.Length];
			this.m_MapColorGNow = new float[this.m_MapObject_SpriteRenderer.Length];
			this.m_MapColorBNow = new float[this.m_MapObject_SpriteRenderer.Length];
			for (int i = 0; i < this.m_MapObject_SpriteRenderer.Length; i++)
			{
				this.m_MapColorROrg[i] = this.m_MapObject_SpriteRenderer[i].color.r;
				this.m_MapColorGOrg[i] = this.m_MapObject_SpriteRenderer[i].color.g;
				this.m_MapColorBOrg[i] = this.m_MapObject_SpriteRenderer[i].color.b;
				this.m_MapColorRNow[i] = this.m_MapObject_SpriteRenderer[i].color.r;
				this.m_MapColorGNow[i] = this.m_MapObject_SpriteRenderer[i].color.g;
				this.m_MapColorBNow[i] = this.m_MapObject_SpriteRenderer[i].color.b;
			}
			for (int j = 0; j < this.m_NKMMapTemplet.m_listBloomPoint.Count; j++)
			{
				NKMBloomPoint nkmbloomPoint = this.m_NKMMapTemplet.m_listBloomPoint[j];
				if (nkmbloomPoint.m_LensFlareName.m_AssetName.Length > 1)
				{
					this.m_dicLensFlare[j].SetPos(nkmbloomPoint.m_fBloomPointX, nkmbloomPoint.m_fBloomPointY, -1f);
				}
			}
			for (int k = 0; k < this.m_NKMMapTemplet.m_listMapLayer.Count; k++)
			{
				NKMMapLayer nkmmapLayer = this.m_NKMMapTemplet.m_listMapLayer[k];
				Transform transform = this.m_MapObject.m_Instant.transform.Find(nkmmapLayer.m_LayerName);
				if (!(transform == null))
				{
					this.m_Vec3Temp = transform.localPosition;
					this.m_Vec3Temp.Set(0f, 0f, 0f);
					transform.localPosition = this.m_Vec3Temp;
					this.m_dicMapLayer.Add(k, transform.gameObject);
				}
			}
			this.m_MapInvalidLandWidth.SetNowValue(0.6f);
			this.m_MapInvalidLand_SpriteRenderer = this.m_MapInvalidLand.m_Instant.GetComponentInChildren<SpriteRenderer>();
			this.m_MapInvalidLand.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_MAP().transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_MapInvalidLand.m_Instant, 0f, 0f, 0f);
			if (this.m_MapInvalidLand.m_Instant.activeSelf)
			{
				this.m_MapInvalidLand.m_Instant.SetActive(false);
			}
			return true;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x00121A2C File Offset: 0x0011FC2C
		public void ClearLensFlares()
		{
			foreach (KeyValuePair<int, NKCASLensFlare> keyValuePair in this.m_dicLensFlare)
			{
				NKCASLensFlare value = keyValuePair.Value;
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(value);
			}
			this.m_dicLensFlare.Clear();
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x00121A7C File Offset: 0x0011FC7C
		public void Update(float fDeltaTime)
		{
			if (this.m_MapObject == null || this.m_MapObject_SpriteRenderer == null)
			{
				return;
			}
			this.m_fDeltaTime = fDeltaTime;
			if (this.m_fMapColorKeepTime > 0f)
			{
				this.m_fMapColorKeepTime -= this.m_fDeltaTime;
				if (this.m_fMapColorKeepTime < 0f)
				{
					this.m_fMapColorKeepTime = 0f;
				}
			}
			else if (this.m_fMapColorReturnTime > 0f)
			{
				this.m_fMapColorReturnTime -= this.m_fDeltaTime;
				if (this.m_fMapColorReturnTime < 0f)
				{
					this.m_fMapColorReturnTime = 0f;
				}
				this.ColorProcess();
			}
			for (int i = 0; i < this.m_MapObject_SpriteRenderer.Length; i++)
			{
				Color color = this.m_MapObject_SpriteRenderer[i].color;
				color.r = this.m_MapColorRNow[i];
				color.g = this.m_MapColorGNow[i];
				color.b = this.m_MapColorBNow[i];
				this.m_MapObject_SpriteRenderer[i].color = color;
			}
			this.SetColorMeshRenderer(this.m_fMapColorRNow, this.m_fMapColorGNow, this.m_fMapColorBNow, -1f);
			this.UpdateLayer();
			this.UpdateInvalidLand();
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x00121BA0 File Offset: 0x0011FDA0
		public void UpdateLayer()
		{
			float posNowX = NKCCamera.GetPosNowX(false);
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.m_dicMapLayer)
			{
				NKMMapLayer nkmmapLayer = this.m_NKMMapTemplet.m_listMapLayer[keyValuePair.Key];
				GameObject value = keyValuePair.Value;
				this.m_Vec3Temp = value.transform.localPosition;
				this.m_Vec3Temp.x = posNowX * nkmmapLayer.m_fMoveFactor;
				value.transform.localPosition = this.m_Vec3Temp;
			}
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x00121C4C File Offset: 0x0011FE4C
		public void UpdateInvalidLand()
		{
			this.m_MapInvalidLandWidth.Update(this.m_fDeltaTime);
			if (this.m_MapInvalidLandWidth.IsTracking())
			{
				this.SetRespawnValidLandAlpha(2f, 0f);
			}
			this.m_MapInvalidLandAlpha.Update(this.m_fDeltaTime);
			if (this.m_MapInvalidLandAlpha.GetNowValue() > 0f)
			{
				float num = this.m_MapInvalidLandWidth.GetNowValue();
				if (this.m_fCustomWidth > 0f)
				{
					num = this.m_fCustomWidth;
				}
				if (!this.m_MapInvalidLand.m_Instant.activeSelf)
				{
					this.m_MapInvalidLand.m_Instant.SetActive(true);
				}
				this.m_Vec2Temp.x = (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * num;
				this.m_Vec2Temp.y = this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ;
				this.m_Vec2Temp.x = this.m_Vec2Temp.x * 0.01f;
				this.m_Vec2Temp.y = this.m_Vec2Temp.y * 0.01f;
				this.m_MapInvalidLand_SpriteRenderer.size = this.m_Vec2Temp;
				this.m_Vec3Temp = this.m_MapInvalidLand.m_Instant.transform.localPosition;
				this.m_Vec3Temp.y = this.m_NKMMapTemplet.m_fMinZ + (this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ) * 0.5f;
				this.m_Vec3Temp.x = this.m_NKMMapTemplet.m_fMaxX - (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * num * 0.5f;
				this.m_MapInvalidLand.m_Instant.transform.localPosition = this.m_Vec3Temp;
				this.m_ColorTemp = this.m_MapInvalidLand_SpriteRenderer.color;
				this.m_ColorTemp.a = this.m_MapInvalidLandAlpha.GetNowValue();
				this.m_MapInvalidLand_SpriteRenderer.color = this.m_ColorTemp;
				return;
			}
			if (this.m_MapInvalidLand.m_Instant.activeSelf)
			{
				this.m_MapInvalidLand.m_Instant.SetActive(false);
			}
			this.m_fCustomWidth = 0f;
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x00121E76 File Offset: 0x00120076
		public void SetRespawnValidLandFactor(float fFactor, bool bTracking)
		{
			if (bTracking)
			{
				this.m_MapInvalidLandWidth.SetTracking(1f - fFactor, 2f, TRACKING_DATA_TYPE.TDT_SLOWER);
				return;
			}
			this.m_MapInvalidLandWidth.SetNowValue(1f - fFactor);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x00121EA8 File Offset: 0x001200A8
		public void SetRespawnValidLandAlpha(float fTrackingTime, float fCustomWidth = 0f)
		{
			this.m_MapInvalidLandAlpha.SetNowValue(1f);
			this.m_MapInvalidLandAlpha.SetTracking(0f, fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			if (!this.m_MapInvalidLand.m_Instant.activeSelf)
			{
				this.m_MapInvalidLand.m_Instant.SetActive(true);
			}
			this.m_fCustomWidth = fCustomWidth;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x00121F04 File Offset: 0x00120104
		private void ColorProcess()
		{
			for (int i = 0; i < this.m_MapObject_SpriteRenderer.Length; i++)
			{
				this.ColorProcess(i);
			}
			this.ColorProcess(ref this.m_fMapColorROrg, ref this.m_fMapColorRNow);
			this.ColorProcess(ref this.m_fMapColorGOrg, ref this.m_fMapColorGNow);
			this.ColorProcess(ref this.m_fMapColorBOrg, ref this.m_fMapColorBNow);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00121F64 File Offset: 0x00120164
		private void ColorProcess(int index)
		{
			this.ColorProcess(ref this.m_MapColorROrg[index], ref this.m_MapColorRNow[index]);
			this.ColorProcess(ref this.m_MapColorGOrg[index], ref this.m_MapColorGNow[index]);
			this.ColorProcess(ref this.m_MapColorBOrg[index], ref this.m_MapColorBNow[index]);
			this.ColorProcess(ref this.m_fMapColorROrg, ref this.m_fMapColorRNow);
			this.ColorProcess(ref this.m_fMapColorGOrg, ref this.m_fMapColorGNow);
			this.ColorProcess(ref this.m_fMapColorBOrg, ref this.m_fMapColorBNow);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00122004 File Offset: 0x00120204
		private void ColorProcess(ref float colorOrg, ref float colorNow)
		{
			if (colorOrg != colorNow)
			{
				if (this.m_fMapColorReturnTime <= 0f)
				{
					colorNow = colorOrg;
					return;
				}
				float num = colorOrg - colorNow;
				num /= this.m_fMapColorReturnTime;
				colorNow += num * this.m_fDeltaTime;
			}
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x00122048 File Offset: 0x00120248
		public void FadeColor(float fR, float fG, float fB, float fMapColorKeepTime, float fMapColorReturnTime)
		{
			for (int i = 0; i < this.m_MapObject_SpriteRenderer.Length; i++)
			{
				this.m_MapColorRNow[i] = fR;
				this.m_MapColorGNow[i] = fG;
				this.m_MapColorBNow[i] = fB;
			}
			this.m_fMapColorRNow = fR;
			this.m_fMapColorGNow = fG;
			this.m_fMapColorBNow = fB;
			this.m_fMapColorKeepTime = fMapColorKeepTime;
			this.m_fMapColorReturnTime = fMapColorReturnTime;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x001220A8 File Offset: 0x001202A8
		public void SetColorMeshRenderer(float fR, float fG, float fB, float fA = -1f)
		{
			bool flag = false;
			if (this.m_ColorMeshRenderer.r != fR)
			{
				this.m_ColorMeshRenderer.r = fR;
				flag = true;
			}
			if (this.m_ColorMeshRenderer.g != fG)
			{
				this.m_ColorMeshRenderer.g = fG;
				flag = true;
			}
			if (this.m_ColorMeshRenderer.b != fB)
			{
				this.m_ColorMeshRenderer.b = fB;
				flag = true;
			}
			if (fA != -1f && this.m_ColorMeshRenderer.a != fA)
			{
				this.m_ColorMeshRenderer.a = fA;
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			if (this.m_ColorMeshRenderer.r == 1f && this.m_ColorMeshRenderer.g == 1f && this.m_ColorMeshRenderer.b == 1f && this.m_ColorMeshRenderer.a == 1f)
			{
				for (int i = 0; i < this.m_MapObject_m_MeshRenderer.Length; i++)
				{
					this.m_MapObject_m_MeshRenderer[i].SetPropertyBlock(null);
				}
				return;
			}
			this.m_MPB.SetColor(Shader.PropertyToID("_Color"), this.m_ColorMeshRenderer);
			for (int j = 0; j < this.m_MapObject_m_MeshRenderer.Length; j++)
			{
				this.m_MapObject_m_MeshRenderer[j].SetPropertyBlock(this.m_MPB);
			}
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x001221E4 File Offset: 0x001203E4
		public float GetMapBright()
		{
			float num = (this.m_fMapColorRNow + this.m_fMapColorGNow + this.m_fMapColorBNow) / 3f;
			float num2 = (this.m_fMapColorROrg + this.m_fMapColorGOrg + this.m_fMapColorBOrg) / 3f;
			return num / num2;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x00122228 File Offset: 0x00120428
		public NKCASLensFlare GetLensFlare(int index)
		{
			if (this.m_dicLensFlare.ContainsKey(index))
			{
				return this.m_dicLensFlare[index];
			}
			return null;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x00122246 File Offset: 0x00120446
		public void SetActiveMap(bool bActive)
		{
			if (this.m_MapObject == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_MapObject.m_Instant, bActive);
		}

		// Token: 0x0400347A RID: 13434
		private NKMMapTemplet m_NKMMapTemplet;

		// Token: 0x0400347B RID: 13435
		private NKCAssetInstanceData m_MapObject;

		// Token: 0x0400347C RID: 13436
		private SpriteRenderer[] m_MapObject_SpriteRenderer;

		// Token: 0x0400347D RID: 13437
		private MeshRenderer[] m_MapObject_m_MeshRenderer;

		// Token: 0x0400347E RID: 13438
		private MaterialPropertyBlock m_MPB = new MaterialPropertyBlock();

		// Token: 0x0400347F RID: 13439
		private Color m_ColorMeshRenderer = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04003480 RID: 13440
		private float m_fMapColorROrg;

		// Token: 0x04003481 RID: 13441
		private float m_fMapColorGOrg;

		// Token: 0x04003482 RID: 13442
		private float m_fMapColorBOrg;

		// Token: 0x04003483 RID: 13443
		private float m_fMapColorRNow;

		// Token: 0x04003484 RID: 13444
		private float m_fMapColorGNow;

		// Token: 0x04003485 RID: 13445
		private float m_fMapColorBNow;

		// Token: 0x04003486 RID: 13446
		private float[] m_MapColorROrg;

		// Token: 0x04003487 RID: 13447
		private float[] m_MapColorGOrg;

		// Token: 0x04003488 RID: 13448
		private float[] m_MapColorBOrg;

		// Token: 0x04003489 RID: 13449
		private float[] m_MapColorRNow;

		// Token: 0x0400348A RID: 13450
		private float[] m_MapColorGNow;

		// Token: 0x0400348B RID: 13451
		private float[] m_MapColorBNow;

		// Token: 0x0400348C RID: 13452
		private NKCAssetInstanceData m_MapInvalidLand;

		// Token: 0x0400348D RID: 13453
		private SpriteRenderer m_MapInvalidLand_SpriteRenderer;

		// Token: 0x0400348E RID: 13454
		private NKMTrackingFloat m_MapInvalidLandWidth = new NKMTrackingFloat();

		// Token: 0x0400348F RID: 13455
		private NKMTrackingFloat m_MapInvalidLandAlpha = new NKMTrackingFloat();

		// Token: 0x04003490 RID: 13456
		private float m_fCustomWidth;

		// Token: 0x04003491 RID: 13457
		private float m_fMapColorKeepTime;

		// Token: 0x04003492 RID: 13458
		private float m_fMapColorReturnTime;

		// Token: 0x04003493 RID: 13459
		private float m_fDeltaTime;

		// Token: 0x04003494 RID: 13460
		private Color m_ColorTemp;

		// Token: 0x04003495 RID: 13461
		private Vector3 m_Vec3Temp;

		// Token: 0x04003496 RID: 13462
		private Vector2 m_Vec2Temp;

		// Token: 0x04003497 RID: 13463
		private Dictionary<int, NKCASLensFlare> m_dicLensFlare = new Dictionary<int, NKCASLensFlare>();

		// Token: 0x04003498 RID: 13464
		private Dictionary<int, GameObject> m_dicMapLayer = new Dictionary<int, GameObject>();
	}
}
