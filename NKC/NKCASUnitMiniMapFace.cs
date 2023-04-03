using System;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000631 RID: 1585
	public class NKCASUnitMiniMapFace : NKMObjectPoolData
	{
		// Token: 0x06003145 RID: 12613 RVA: 0x000F3D54 File Offset: 0x000F1F54
		public NKCASUnitMiniMapFace(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitMiniMapFace;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000F3D80 File Offset: 0x000F1F80
		public override void Load(bool bAsync)
		{
			this.m_MiniMapFaceInstant = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UNIT_MINI_MAP_FACE", "AB_UNIT_MINI_MAP_FACE", bAsync, null);
			this.m_MiniMapFaceSprite = NKCAssetResourceManager.OpenResource<Sprite>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000F3DB4 File Offset: 0x000F1FB4
		public override bool LoadComplete()
		{
			if (this.m_MiniMapFaceInstant == null || this.m_MiniMapFaceInstant.m_Instant == null)
			{
				Debug.LogError("NKCASUnitMiniMapFace:LoadComplete null " + this.m_ObjectPoolBundleName + " " + this.m_ObjectPoolName);
				return false;
			}
			this.ObjectParentRestore();
			this.m_RectTransform = this.m_MiniMapFaceInstant.m_Instant.GetComponentInChildren<RectTransform>();
			this.m_MiniMapFaceImage = this.m_MiniMapFaceInstant.m_Instant.GetComponentInChildren<Image>();
			this.m_MiniMapFaceImage.sprite = this.m_MiniMapFaceSprite.GetAsset<Sprite>();
			this.m_MarkerGreen = this.m_MiniMapFaceInstant.m_Instant.transform.Find("MINI_MAP_FACE_MARKER_GREEN_Panel").gameObject;
			this.m_MarkerRed = this.m_MiniMapFaceInstant.m_Instant.transform.Find("MINI_MAP_FACE_MARKER_RED_Panel").gameObject;
			this.m_UNIT_MINI_MAP_FACE_FX = this.m_MiniMapFaceInstant.m_Instant.transform.Find("UNIT_MINI_MAP_FACE_FX").gameObject;
			if (this.m_UNIT_MINI_MAP_FACE_FX != null)
			{
				this.m_UNIT_MINI_MAP_FACE_FX_Animator = this.m_UNIT_MINI_MAP_FACE_FX.GetComponentInChildren<Animator>();
			}
			return true;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000F3ED5 File Offset: 0x000F20D5
		public override void Open()
		{
			this.ObjectParentRestore();
			if (!this.m_MiniMapFaceInstant.m_Instant.activeSelf)
			{
				this.m_MiniMapFaceInstant.m_Instant.SetActive(true);
			}
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000F3F00 File Offset: 0x000F2100
		public override void Close()
		{
			this.ObjectParentWait();
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000F3F08 File Offset: 0x000F2108
		public override void Unload()
		{
			NKCAssetResourceManager.CloseInstance(this.m_MiniMapFaceInstant);
			NKCAssetResourceManager.CloseResource(this.m_MiniMapFaceSprite);
			this.m_MiniMapFaceInstant = null;
			this.m_RectTransform = null;
			this.m_MiniMapFaceImage = null;
			this.m_MiniMapFaceSprite = null;
			this.m_MarkerGreen = null;
			this.m_MarkerRed = null;
			this.m_UNIT_MINI_MAP_FACE_FX = null;
			this.m_UNIT_MINI_MAP_FACE_FX_Animator = null;
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000F3F64 File Offset: 0x000F2164
		public void ObjectParentWait()
		{
			if (this.m_MiniMapFaceInstant != null && this.m_MiniMapFaceInstant.m_Instant != null && this.m_MiniMapFaceInstant.m_Instant.transform.parent != NKCUIManager.m_TR_NKM_WAIT_INSTANT)
			{
				this.m_MiniMapFaceInstant.m_Instant.transform.SetParent(NKCUIManager.m_TR_NKM_WAIT_INSTANT, false);
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000F3FC8 File Offset: 0x000F21C8
		public void ObjectParentRestore()
		{
			if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA() == null || NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_GAME_HUD_MINI_MAP() == null)
			{
				return;
			}
			if (this.m_MiniMapFaceInstant == null || this.m_MiniMapFaceInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_MiniMapFaceInstant.m_Instant.transform.parent != NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_GAME_HUD_MINI_MAP().transform)
			{
				this.m_MiniMapFaceInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_GAME_HUD_MINI_MAP().transform, false);
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000F4084 File Offset: 0x000F2284
		public void SetColor(float fR, float fG, float fB)
		{
			if (this.m_MiniMapFaceImage == null)
			{
				return;
			}
			Color color = this.m_MiniMapFaceImage.color;
			color.r = fR;
			color.g = fG;
			color.b = fB;
			color.a = 1f;
			this.m_MiniMapFaceImage.color = color;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000F40DC File Offset: 0x000F22DC
		public void Warnning()
		{
			if (this.m_UNIT_MINI_MAP_FACE_FX == null || this.m_UNIT_MINI_MAP_FACE_FX_Animator == null)
			{
				return;
			}
			if (this.m_UNIT_MINI_MAP_FACE_FX.activeInHierarchy)
			{
				this.m_UNIT_MINI_MAP_FACE_FX_Animator.Play("IDLE", -1);
			}
		}

		// Token: 0x0400306A RID: 12394
		public NKCAssetInstanceData m_MiniMapFaceInstant;

		// Token: 0x0400306B RID: 12395
		public RectTransform m_RectTransform;

		// Token: 0x0400306C RID: 12396
		public Image m_MiniMapFaceImage;

		// Token: 0x0400306D RID: 12397
		public NKCAssetResourceData m_MiniMapFaceSprite;

		// Token: 0x0400306E RID: 12398
		public GameObject m_MarkerGreen;

		// Token: 0x0400306F RID: 12399
		public GameObject m_MarkerRed;

		// Token: 0x04003070 RID: 12400
		public GameObject m_UNIT_MINI_MAP_FACE_FX;

		// Token: 0x04003071 RID: 12401
		public Animator m_UNIT_MINI_MAP_FACE_FX_Animator;
	}
}
