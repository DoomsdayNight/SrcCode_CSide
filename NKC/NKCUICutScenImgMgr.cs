using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B6 RID: 1974
	public class NKCUICutScenImgMgr : MonoBehaviour
	{
		// Token: 0x06004E34 RID: 20020 RVA: 0x001792B6 File Offset: 0x001774B6
		public static NKCUICutScenImgMgr GetCutScenImgMgr()
		{
			return NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr;
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x001792BD File Offset: 0x001774BD
		public void SetPause(bool bPause)
		{
			this.m_bPause = bPause;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x001792C8 File Offset: 0x001774C8
		public static void InitUI(GameObject goNKM_UI_CUTSCEN_PLAYER)
		{
			if (NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr != null)
			{
				return;
			}
			NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_IMG_MGR").gameObject.GetComponent<NKCUICutScenImgMgr>();
			NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr.m_goRectTransform = NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr.GetComponent<RectTransform>();
			NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr.m_OrgPos = NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr.m_goRectTransform.anchoredPosition;
			NKCUICutScenImgMgr.m_scNKCUICutScenImgMgr.Close();
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00179339 File Offset: 0x00177539
		public void Reset()
		{
			this.SetPause(false);
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00179344 File Offset: 0x00177544
		private void Update()
		{
			if (this.m_bPause)
			{
				return;
			}
			if (!this.m_bFinished && this.m_CanvasGroup.alpha < 1f)
			{
				this.m_CanvasGroup.alpha += Time.deltaTime * 3f;
				if (this.m_CanvasGroup.alpha >= 1f)
				{
					this.m_CanvasGroup.alpha = 1f;
					this.m_bFinished = true;
				}
			}
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x001793BC File Offset: 0x001775BC
		public void Open(string imgFileName, Vector2 offsetPos, float scale)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (this.m_imgFileName != imgFileName)
			{
				this.m_CanvasGroup.alpha = 0f;
				this.m_bFinished = false;
			}
			else
			{
				this.m_CanvasGroup.alpha = 1f;
				this.m_bFinished = true;
			}
			this.m_imgFileName = imgFileName;
			Vector2 anchoredPosition = new Vector2(this.m_OrgPos.x + offsetPos.x, this.m_OrgPos.y + offsetPos.y);
			this.m_goRectTransform.anchoredPosition = anchoredPosition;
			this.m_goRectTransform.localScale = new Vector2(scale, scale);
			NKCAssetResourceData nkcassetResourceData = NKCResourceUtility.GetAssetResource("AB_UI_NKM_UI_CUTSCEN_IMG", "AB_UI_NKM_UI_CUTSCEN_IMG_" + imgFileName);
			if (nkcassetResourceData != null)
			{
				this.m_imgImg.sprite = nkcassetResourceData.GetAsset<Sprite>();
				if (this.m_imgImg.sprite != null)
				{
					this.m_rtImg.sizeDelta = this.m_imgImg.sprite.rect.size;
					return;
				}
			}
			else
			{
				nkcassetResourceData = NKCAssetResourceManager.OpenResource<Sprite>("AB_UI_NKM_UI_CUTSCEN_IMG", "AB_UI_NKM_UI_CUTSCEN_IMG_" + imgFileName, false, null);
				if (nkcassetResourceData != null)
				{
					this.m_imgImg.sprite = nkcassetResourceData.GetAsset<Sprite>();
					this.m_rtImg.sizeDelta = this.m_imgImg.sprite.rect.size;
					NKCAssetResourceManager.CloseResource(nkcassetResourceData);
				}
			}
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x0017952E File Offset: 0x0017772E
		public bool IsFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00179536 File Offset: 0x00177736
		public void Finish()
		{
			if (this.m_bFinished)
			{
				return;
			}
			this.m_bFinished = true;
			this.m_CanvasGroup.alpha = 1f;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00179558 File Offset: 0x00177758
		public void Close()
		{
			this.Finish();
			this.m_imgFileName = "";
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x04003DCF RID: 15823
		private static NKCUICutScenImgMgr m_scNKCUICutScenImgMgr;

		// Token: 0x04003DD0 RID: 15824
		private Vector2 m_OrgPos = new Vector2(0f, 0f);

		// Token: 0x04003DD1 RID: 15825
		private RectTransform m_goRectTransform;

		// Token: 0x04003DD2 RID: 15826
		public RectTransform m_rtImg;

		// Token: 0x04003DD3 RID: 15827
		public Image m_imgImg;

		// Token: 0x04003DD4 RID: 15828
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04003DD5 RID: 15829
		private bool m_bFinished = true;

		// Token: 0x04003DD6 RID: 15830
		private string m_imgFileName = "";

		// Token: 0x04003DD7 RID: 15831
		private bool m_bPause;
	}
}
