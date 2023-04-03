using System;
using NKM;
using NKM.Templet;
using Spine.Unity;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000950 RID: 2384
	public class NKCUIInGameCharacterViewer : MonoBehaviour
	{
		// Token: 0x06005F14 RID: 24340 RVA: 0x001D89B9 File Offset: 0x001D6BB9
		public void Prepare(Material mat = null)
		{
			this.m_TextureRenderer.PrepareTexture(mat);
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x001D89C7 File Offset: 0x001D6BC7
		public void CleanUp()
		{
			this.CloseCurrentPreviewModel();
			this.m_TextureRenderer.CleanUp();
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x001D89DC File Offset: 0x001D6BDC
		private void CloseCurrentPreviewModel()
		{
			if (this.m_UnitPreview != null && this.m_UnitPreview.m_UnitSpineSpriteInstant != null && this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant != null)
			{
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.localScale = Vector3.one;
				NKCUtil.SetLayer(this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform, this.m_UnitPreviewOrigLayer);
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_UnitPreview);
			this.m_UnitPreview = null;
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x001D8A74 File Offset: 0x001D6C74
		public void SetPreviewBattleUnit(int unitID, int skinID)
		{
			this.CloseCurrentPreviewModel();
			if (skinID == 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				this.m_UnitPreview = NKCUnitViewer.OpenUnitViewerSpineSprite(unitTempletBase, false, true);
				this.bWaitingForLoading = true;
			}
			else
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				this.m_UnitPreview = NKCUnitViewer.OpenUnitViewerSpineSprite(skinTemplet, false, true);
				this.bWaitingForLoading = true;
			}
			NKCUtil.SetGameobjectActive(this.m_TextureRenderer, false);
			NKCUtil.SetGameobjectActive(this.m_objLoading, this.bWaitingForLoading);
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x001D8AE1 File Offset: 0x001D6CE1
		private void Update()
		{
			if (this.bWaitingForLoading && this.m_UnitPreview != null && this.m_UnitPreview.m_bIsLoaded)
			{
				this.AfterUnitLoadComplete();
			}
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001D8B08 File Offset: 0x001D6D08
		private void AfterUnitLoadComplete()
		{
			this.bWaitingForLoading = false;
			if (this.m_UnitPreview != null && this.m_UnitPreview.m_UnitSpineSpriteInstant != null && this.m_UnitPreview.m_cSkeletonAnimation != null)
			{
				this.m_UnitPreviewOrigLayer = this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.layer;
				NKCUtil.SetLayer(this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform, 31);
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.SetParent(this.m_TextureRenderer.transform, false);
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.localPosition = new Vector3(0f, 30f, 0f);
				float d = this.m_TextureRenderer.m_rtImage.GetHeight() / 600f;
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.localScale = Vector3.one * d;
				Transform transform = this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.Find("SPINE_SkeletonAnimation");
				if (transform != null)
				{
					SkeletonAnimation component = transform.GetComponent<SkeletonAnimation>();
					if (component != null)
					{
						component.AnimationState.SetAnimation(0, "ASTAND", true);
					}
					NKCUtil.SetGameobjectActive(this.m_TextureRenderer, true);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_TextureRenderer, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objLoading, false);
		}

		// Token: 0x04004B2F RID: 19247
		[Header("스킨 인게임유닛 미리보기")]
		public NKCUIComModelTextureRenderer m_TextureRenderer;

		// Token: 0x04004B30 RID: 19248
		public GameObject m_objLoading;

		// Token: 0x04004B31 RID: 19249
		private NKCASUnitSpineSprite m_UnitPreview;

		// Token: 0x04004B32 RID: 19250
		private int m_UnitPreviewOrigLayer;

		// Token: 0x04004B33 RID: 19251
		private bool bWaitingForLoading;
	}
}
