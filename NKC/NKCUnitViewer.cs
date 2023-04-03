using System;
using NKC.Game;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006EC RID: 1772
	public class NKCUnitViewer : NKMObjectPoolData
	{
		// Token: 0x06003E4D RID: 15949 RVA: 0x00142D72 File Offset: 0x00140F72
		public NKMUnitTemplet GetUnitTemplet()
		{
			return this.m_UnitTemplet;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00142D7A File Offset: 0x00140F7A
		public void SetRespawnReady(bool bRespawnReady)
		{
			this.m_bRespawnReady = bRespawnReady;
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00142D83 File Offset: 0x00140F83
		public bool GetRespawnReady()
		{
			return this.m_bRespawnReady;
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00142D8B File Offset: 0x00140F8B
		public void SetUnitUID(long unitUID)
		{
			this.m_UnitUID = unitUID;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00142D94 File Offset: 0x00140F94
		public long GetUnitUID()
		{
			return this.m_UnitUID;
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00142D9C File Offset: 0x00140F9C
		public NKCUnitViewer()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCUnitViewer;
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00142DB8 File Offset: 0x00140FB8
		public override bool LoadComplete()
		{
			if (this.m_NKCASUnitViewerSpineSprite == null || this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant == null || this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return false;
			}
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_SHADOW().transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant, 0f, 0f, 0f);
			if (this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(false);
			}
			return true;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00142E8F File Offset: 0x0014108F
		public override void Open()
		{
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00142E91 File Offset: 0x00141091
		public override void Close()
		{
			if (this.m_AnimSpine != null)
			{
				this.m_AnimSpine.ResetParticleSimulSpeedOrg();
			}
			this.Init();
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x00142EAC File Offset: 0x001410AC
		public override void Unload()
		{
			if (this.m_NKCASUnitShadow != null)
			{
				this.m_NKCASUnitShadow.Unload();
				this.m_NKCASUnitShadow = null;
			}
			this.m_Parent = null;
			this.m_NKCASUnitViewerSpineSprite = null;
			this.m_UnitSpriteOrg = null;
			this.m_UnitTemplet = null;
			this.m_AnimSpine.Init();
			this.m_AnimSpine = null;
			UnityEngine.Object.Destroy(this.m_RespawnTimer);
			this.m_RespawnTimer = null;
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00142F14 File Offset: 0x00141114
		public void Init()
		{
			this.m_Parent = null;
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUnitViewerSpineSprite);
			this.m_NKCASUnitViewerSpineSprite = null;
			this.m_RespawnTimer = null;
			this.m_UnitSpriteOrg = null;
			this.m_UnitTemplet = null;
			this.m_AnimSpine.Init();
			this.m_bRight = true;
			this.m_fPosX = 0f;
			this.m_fPosY = 0f;
			this.m_fPosZ = 0f;
			this.m_fScaleX = 1f;
			this.m_fScaleY = 1f;
			this.m_bUseAirHigh = true;
			this.m_bRespawnReady = false;
			this.m_UnitUID = 0L;
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00142FB8 File Offset: 0x001411B8
		public void LoadUnit(NKMUnitData unitData, bool bSub, GameObject parent = null, bool bAsync = false)
		{
			if (unitData == null)
			{
				return;
			}
			this.m_UnitTemplet = NKMUnitManager.GetUnitTemplet(unitData.m_UnitID);
			if (this.m_UnitTemplet == null)
			{
				return;
			}
			this.m_Parent = parent;
			this.m_NKCASUnitViewerSpineSprite = NKCUnitViewer.OpenUnitViewerSpineSprite(unitData, bSub, bAsync);
			if (this.m_NKCASUnitViewerSpineSprite == null)
			{
				Debug.LogError("Unit Spine sprite null!!");
			}
			if (this.m_NKCASUnitShadow == null)
			{
				this.m_NKCASUnitShadow = (NKCASUnitShadow)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitShadow, "", "", bAsync);
			}
			this.m_RespawnTimer == null;
			if (!bAsync)
			{
				this.LoadUnitComplete();
			}
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00143054 File Offset: 0x00141254
		public static NKCASUnitViewerSpineSprite OpenUnitViewerSpineSprite(NKMUnitData unitData, bool bSub, bool bAsync)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData);
			if (skinTemplet != null)
			{
				return NKCUnitViewer.OpenUnitViewerSpineSprite(skinTemplet, bSub, bAsync);
			}
			return NKCUnitViewer.OpenUnitViewerSpineSprite(NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID), bSub, bAsync);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00143088 File Offset: 0x00141288
		public static NKCASUnitViewerSpineSprite OpenUnitViewerSpineSprite(NKMUnitTempletBase unitTempletBase, bool bSub, bool bAsync)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			NKCASUnitViewerSpineSprite nkcasunitViewerSpineSprite;
			if (bSub && !string.IsNullOrEmpty(unitTempletBase.m_SpriteNameSub))
			{
				nkcasunitViewerSpineSprite = (NKCASUnitViewerSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitViewerSpineSprite, unitTempletBase.m_SpriteBundleNameSub, unitTempletBase.m_SpriteNameSub, bAsync);
				if (!string.IsNullOrEmpty(unitTempletBase.m_SpriteMaterialNameSub))
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource(unitTempletBase.m_SpriteBundleNameSub, unitTempletBase.m_SpriteMaterialNameSub, bAsync);
				}
				else
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			else
			{
				nkcasunitViewerSpineSprite = (NKCASUnitViewerSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitViewerSpineSprite, unitTempletBase.m_SpriteBundleName, unitTempletBase.m_SpriteName, bAsync);
				if (!string.IsNullOrEmpty(unitTempletBase.m_SpriteMaterialName))
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource(unitTempletBase.m_SpriteBundleName, unitTempletBase.m_SpriteMaterialName, bAsync);
				}
				else
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			return nkcasunitViewerSpineSprite;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x0014315C File Offset: 0x0014135C
		public static NKCASUnitViewerSpineSprite OpenUnitViewerSpineSprite(NKMSkinTemplet skinTemplet, bool bSub, bool bAsync)
		{
			if (skinTemplet == null)
			{
				return null;
			}
			NKCASUnitViewerSpineSprite nkcasunitViewerSpineSprite;
			if (bSub && !string.IsNullOrEmpty(skinTemplet.m_SpriteNameSub))
			{
				nkcasunitViewerSpineSprite = (NKCASUnitViewerSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitViewerSpineSprite, skinTemplet.m_SpriteBundleNameSub, skinTemplet.m_SpriteNameSub, bAsync);
				if (!string.IsNullOrEmpty(skinTemplet.m_SpriteMaterialNameSub))
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource(skinTemplet.m_SpriteBundleNameSub, skinTemplet.m_SpriteMaterialNameSub, bAsync);
				}
				else
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			else
			{
				nkcasunitViewerSpineSprite = (NKCASUnitViewerSpineSprite)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitViewerSpineSprite, skinTemplet.m_SpriteBundleName, skinTemplet.m_SpriteName, bAsync);
				if (!string.IsNullOrEmpty(skinTemplet.m_SpriteMaterialName))
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource(skinTemplet.m_SpriteBundleName, skinTemplet.m_SpriteMaterialName, bAsync);
				}
				else
				{
					nkcasunitViewerSpineSprite.SetReplaceMatResource("", "", bAsync);
				}
			}
			return nkcasunitViewerSpineSprite;
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00143230 File Offset: 0x00141430
		public void LoadUnitComplete()
		{
			if (this.m_Parent != null)
			{
				this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant.transform.SetParent(this.m_Parent.transform, false);
				NKCUtil.SetGameObjectLocalPos(this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant, 0f, 0f, 0f);
			}
			this.m_UnitSpriteOrg = this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_InstantOrg.GetAsset<GameObject>();
			this.m_AnimSpine.SetAnimObj(this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant, null, true);
			if (this.m_RespawnTimer != null && this.m_UnitSpriteOrg != null)
			{
				this.m_RespawnTimer.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT().transform, false);
				this.m_RespawnTimer.transform.localPosition = new Vector3(0f, -75f, 0f);
			}
			NKCUtil.SetGameobjectActive(this.m_RespawnTimer, false);
			this.SetRight(true);
			this.SetPos(0f, 0f, 0f, true);
			this.SetScale(this.m_UnitTemplet.m_SpriteScale, this.m_UnitTemplet.m_SpriteScale);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0014337C File Offset: 0x0014157C
		public void Update(float fDeltaTime)
		{
			this.m_AnimSpine.Update(fDeltaTime);
			if (!this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant.activeSelf)
			{
				return;
			}
			this.UpdateObject();
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x001433A8 File Offset: 0x001415A8
		private void UpdateObject()
		{
			float num = this.m_fPosX;
			float num2 = this.m_fPosY + this.m_UnitTemplet.m_SpriteOffsetY + this.m_UnitSpriteOrg.transform.localPosition.y;
			if (this.m_bUseAirHigh)
			{
				num2 += this.m_UnitTemplet.m_fAirHigh;
			}
			if (this.m_bRight)
			{
				num += this.m_UnitTemplet.m_SpriteOffsetX + this.m_UnitSpriteOrg.transform.localPosition.x;
			}
			else
			{
				num -= this.m_UnitTemplet.m_SpriteOffsetX - this.m_UnitSpriteOrg.transform.localPosition.x;
			}
			NKCUtil.SetGameObjectLocalPos(this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant, num, this.m_fPosZ + num2, this.m_fPosZ);
			num = this.m_fScaleX * this.m_UnitSpriteOrg.transform.localScale.x;
			num2 = this.m_fScaleY * this.m_UnitSpriteOrg.transform.localScale.y;
			if (!this.m_bRight)
			{
				num = -num;
			}
			NKCUtil.SetGameObjectLocalScale(this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant, num, num2, 1f);
			this.SetDataObject_Shadow();
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x001434D8 File Offset: 0x001416D8
		private void SetDataObject_Shadow()
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_bRight)
			{
				this.m_Vec3Temp.Set(this.m_UnitTemplet.m_fShadowRotateX, 180f, this.m_UnitTemplet.m_fShadowRotateZ);
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localEulerAngles = this.m_Vec3Temp;
			}
			else
			{
				this.m_Vec3Temp.Set(this.m_UnitTemplet.m_fShadowRotateX, 0f, this.m_UnitTemplet.m_fShadowRotateZ);
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localEulerAngles = this.m_Vec3Temp;
			}
			this.m_Vec3Temp.Set(this.m_fPosX, this.m_fPosZ, this.m_fPosZ);
			if (this.m_bRight)
			{
				this.m_Vec3Temp.x = this.m_Vec3Temp.x + this.m_UnitTemplet.m_fShadowOffsetX;
			}
			else
			{
				this.m_Vec3Temp.x = this.m_Vec3Temp.x - this.m_UnitTemplet.m_fShadowOffsetX;
			}
			this.m_Vec3Temp.y = this.m_Vec3Temp.y + this.m_UnitTemplet.m_fShadowOffsetY;
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localPosition = this.m_Vec3Temp;
			float num = 1f - 0.2f * this.m_fPosY * 0.01f;
			if (num < 0.3f)
			{
				num = 0.3f;
			}
			NKCUtil.SetGameObjectLocalScale(this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant, this.m_UnitTemplet.m_fShadowScaleX * num, this.m_UnitTemplet.m_fShadowScaleY * num, 1f);
			if (this.m_RespawnTimer != null)
			{
				this.m_RespawnTimer.SetPosition(this.m_Vec3Temp);
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x001436B8 File Offset: 0x001418B8
		public void SetRight(bool bRight)
		{
			this.m_bRight = bRight;
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x001436C1 File Offset: 0x001418C1
		public void SetPos(float fPosX, float fPosY, float fPosZ, bool bUseAirHigh = true)
		{
			this.m_fPosX = fPosX;
			this.m_fPosY = fPosY;
			this.m_fPosZ = fPosZ;
			this.m_bUseAirHigh = bUseAirHigh;
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x001436E0 File Offset: 0x001418E0
		public void SetScale(float fScaleX, float fScaleY)
		{
			this.m_fScaleX = fScaleX;
			this.m_fScaleY = fScaleY;
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x001436F0 File Offset: 0x001418F0
		public void SetActiveSprite(bool bActive)
		{
			if (bActive != this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitViewerSpineSprite.m_UnitSpineSpriteInstant.m_Instant.SetActive(bActive);
			}
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00143720 File Offset: 0x00141920
		public void SetActiveShadow(bool bActive)
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			if (bActive != this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(bActive);
			}
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0014378C File Offset: 0x0014198C
		public void SetShadowType(bool bTeamA)
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			this.m_NKCASUnitShadow.SetShadowType(this.m_UnitTemplet.m_NKC_TEAM_COLOR_TYPE, bTeamA, this.m_UnitTemplet.m_UnitTempletBase.IsRearmUnit);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x001437EE File Offset: 0x001419EE
		public void SetShadowType(NKC_TEAM_COLOR_TYPE eNKC_TEAM_COLOR_TYPE, bool bTeamA, bool bRearm)
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			this.m_NKCASUnitShadow.SetShadowType(eNKC_TEAM_COLOR_TYPE, bTeamA, bRearm);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x0014382C File Offset: 0x00141A2C
		public void Play(string animName, bool bLoop, float fNormalTime = 0f)
		{
			this.m_AnimSpine.Play(animName, bLoop, fNormalTime);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x0014383C File Offset: 0x00141A3C
		public void SetColor(float fR, float fG, float fB, float fA)
		{
			this.m_NKCASUnitViewerSpineSprite.SetColor(fR, fG, fB, fA, false);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x0014384F File Offset: 0x00141A4F
		public void SetLayer(string layerName)
		{
			this.m_NKCASUnitViewerSpineSprite.SetSortingLayerName(layerName);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0014385D File Offset: 0x00141A5D
		public NKCASUnitShadow GetShadow()
		{
			return this.m_NKCASUnitShadow;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00143865 File Offset: 0x00141A65
		public void PlayTimer(float time = 1f)
		{
			if (this.m_RespawnTimer == null)
			{
				return;
			}
			this.m_RespawnTimer.Play(time);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00143882 File Offset: 0x00141A82
		public void StopTimer()
		{
			if (this.m_RespawnTimer == null)
			{
				return;
			}
			this.m_RespawnTimer.Stop();
		}

		// Token: 0x0400371A RID: 14106
		private GameObject m_Parent;

		// Token: 0x0400371B RID: 14107
		private NKCASUnitViewerSpineSprite m_NKCASUnitViewerSpineSprite;

		// Token: 0x0400371C RID: 14108
		private NKCASUnitRespawnTimer m_RespawnTimer;

		// Token: 0x0400371D RID: 14109
		private GameObject m_UnitSpriteOrg;

		// Token: 0x0400371E RID: 14110
		private NKCASUnitShadow m_NKCASUnitShadow;

		// Token: 0x0400371F RID: 14111
		private NKMUnitTemplet m_UnitTemplet;

		// Token: 0x04003720 RID: 14112
		private NKCAnimSpine m_AnimSpine = new NKCAnimSpine();

		// Token: 0x04003721 RID: 14113
		private bool m_bRight;

		// Token: 0x04003722 RID: 14114
		private float m_fPosX;

		// Token: 0x04003723 RID: 14115
		private float m_fPosY;

		// Token: 0x04003724 RID: 14116
		private float m_fPosZ;

		// Token: 0x04003725 RID: 14117
		private float m_fScaleX;

		// Token: 0x04003726 RID: 14118
		private float m_fScaleY;

		// Token: 0x04003727 RID: 14119
		private bool m_bUseAirHigh;

		// Token: 0x04003728 RID: 14120
		private bool m_bRespawnReady;

		// Token: 0x04003729 RID: 14121
		private long m_UnitUID;

		// Token: 0x0400372A RID: 14122
		private Vector3 m_Vec3Temp;
	}
}
