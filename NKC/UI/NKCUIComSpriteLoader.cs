using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000949 RID: 2377
	[RequireComponent(typeof(Image))]
	public class NKCUIComSpriteLoader : MonoBehaviour, INKCUIValidator
	{
		// Token: 0x06005EDD RID: 24285 RVA: 0x001D735B File Offset: 0x001D555B
		private void Awake()
		{
			this.Validate();
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x001D7364 File Offset: 0x001D5564
		public void Validate()
		{
			if (this.m_bValidate)
			{
				return;
			}
			this.m_bValidate = true;
			if (this.m_image == null)
			{
				this.m_image = base.GetComponent<Image>();
			}
			if (NKCStringTable.GetNationalCode() != NKM_NATIONAL_CODE.NNC_KOREA && NKCAssetResourceManager.IsAssetInLocBundleCheckAll(this.m_BundleName, this.m_AssetName))
			{
				NKCUtil.SetImageSprite(this.m_image, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(this.m_BundleName, this.m_AssetName, false), false);
				return;
			}
			if (this.m_image != null && this.m_image.sprite == null)
			{
				this.m_image.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(this.m_BundleName, this.m_AssetName, false);
			}
		}

		// Token: 0x04004AFF RID: 19199
		public string m_BundleName;

		// Token: 0x04004B00 RID: 19200
		public string m_AssetName;

		// Token: 0x04004B01 RID: 19201
		private bool m_bValidate;

		// Token: 0x04004B02 RID: 19202
		private Image m_image;
	}
}
