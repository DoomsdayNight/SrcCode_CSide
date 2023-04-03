using System;
using System.Text;
using NKC.Loading;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007C8 RID: 1992
	public class NKCUILoadingScreen : MonoBehaviour
	{
		// Token: 0x06004EC2 RID: 20162 RVA: 0x0017C342 File Offset: 0x0017A542
		public void Init()
		{
			base.gameObject.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_objBigLoadingScreen, false);
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x0017C35C File Offset: 0x0017A55C
		public void ShowMainLoadingUI(NKM_GAME_TYPE gameType, int contentValue = 0)
		{
			this.ShowMainLoadingUI(NKCLoadingScreenManager.GetGameContentsType(gameType), contentValue, 0);
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x0017C36C File Offset: 0x0017A56C
		public void ShowMainLoadingUI(NKCLoadingScreenManager.eGameContentsType contentType = NKCLoadingScreenManager.eGameContentsType.DEFAULT, int contentValue = 0, int dungeonID = 0)
		{
			if (base.gameObject.activeSelf)
			{
				return;
			}
			Tuple<NKCLoadingScreenManager.NKCLoadingImgTemplet, string> loadingScreen = NKCLoadingScreenManager.GetLoadingScreen(contentType, contentValue, dungeonID);
			NKCLoadingScreenManager.NKCLoadingImgTemplet item = loadingScreen.Item1;
			if (item == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objDefaultLoadingScreen, true);
				NKCUtil.SetGameobjectActive(this.m_rootCartoon, false);
				NKCUtil.SetGameobjectActive(this.m_imgFull, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objDefaultLoadingScreen, false);
				Sprite sprite = this.LoadSprite(item.m_ImgAssetName);
				if (sprite != null)
				{
					NKCLoadingScreenManager.NKCLoadingImgTemplet.eImgType eImgType = item.m_eImgType;
					if (eImgType != NKCLoadingScreenManager.NKCLoadingImgTemplet.eImgType.FULL)
					{
						if (eImgType == NKCLoadingScreenManager.NKCLoadingImgTemplet.eImgType.CARTOON)
						{
							NKCUtil.SetGameobjectActive(this.m_imgFull, false);
							NKCUtil.SetGameobjectActive(this.m_rootCartoon, true);
							NKCUtil.SetImageSprite(this.m_imgCartoon, sprite, false);
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_imgFull, true);
						NKCUtil.SetGameobjectActive(this.m_rootCartoon, false);
						NKCUtil.SetImageSprite(this.m_imgFull, sprite, false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objDefaultLoadingScreen, true);
					NKCUtil.SetGameobjectActive(this.m_rootCartoon, false);
					NKCUtil.SetGameobjectActive(this.m_imgFull, false);
				}
			}
			if (string.IsNullOrEmpty(loadingScreen.Item2))
			{
				NKCUtil.SetLabelText(this.m_lbTip, "");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTip, NKCStringTable.GetString(loadingScreen.Item2, false));
			}
			NKCUtil.SetGameobjectActive(this.m_objBigLoadingScreen, true);
			base.gameObject.SetActive(true);
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x0017C4B5 File Offset: 0x0017A6B5
		public void CloseLoadingUI()
		{
			base.gameObject.SetActive(false);
			NKCUtil.SetImageSprite(this.m_imgFull, null, false);
			NKCUtil.SetImageSprite(this.m_imgCartoon, null, false);
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0017C4E0 File Offset: 0x0017A6E0
		public void SetLoadingProgress(float fProgress)
		{
			StringBuilder builder = NKMString.GetBuilder();
			builder.AppendFormat("{0}%", (int)(fProgress * 100f));
			NKCUtil.SetLabelText(this.m_lbProgress, NKCUtilString.GET_STRING_ATTACK_PREPARING);
			NKCUtil.SetLabelText(this.m_lbProgressCount, builder.ToString());
			this.m_imgProgress.fillAmount = fProgress;
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x0017C539 File Offset: 0x0017A739
		public void SetWaitOpponent()
		{
			NKCUtil.SetLabelText(this.m_lbProgress, NKCUtilString.GET_STRING_ATTACK_WAITING_OPPONENT);
			NKCUtil.SetLabelText(this.m_lbProgressCount, "100%");
			this.m_imgProgress.fillAmount = 1f;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x0017C56C File Offset: 0x0017A76C
		private Sprite LoadSprite(string assetName)
		{
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName("ab_loading", assetName);
			if (NKCAssetResourceManager.IsBundleExists(nkmassetName.m_BundleName, nkmassetName.m_AssetName))
			{
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>(nkmassetName);
			}
			return null;
		}

		// Token: 0x04003E8B RID: 16011
		public GameObject m_objBigLoadingScreen;

		// Token: 0x04003E8C RID: 16012
		public Text m_lbTip;

		// Token: 0x04003E8D RID: 16013
		public Text m_lbProgress;

		// Token: 0x04003E8E RID: 16014
		public Text m_lbProgressCount;

		// Token: 0x04003E8F RID: 16015
		public Image m_imgProgress;

		// Token: 0x04003E90 RID: 16016
		public GameObject m_objDefaultLoadingScreen;

		// Token: 0x04003E91 RID: 16017
		public Image m_imgFull;

		// Token: 0x04003E92 RID: 16018
		public Image m_imgCartoon;

		// Token: 0x04003E93 RID: 16019
		public GameObject m_rootCartoon;
	}
}
