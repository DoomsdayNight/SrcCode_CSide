using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C31 RID: 3121
	public class NKCUIIllustSlot : MonoBehaviour
	{
		// Token: 0x060090F8 RID: 37112 RVA: 0x00316C24 File Offset: 0x00314E24
		public void Init(int categoryID, int BGGroupID, NKCUICollectionIllust.OnIllustView callback)
		{
			if (null != this.m_btn_NKM_UI_COLLECTION_ALBUM_SLOT_OPEN)
			{
				this.m_btn_NKM_UI_COLLECTION_ALBUM_SLOT_OPEN.PointerClick.RemoveAllListeners();
				this.m_btn_NKM_UI_COLLECTION_ALBUM_SLOT_OPEN.PointerClick.AddListener(new UnityAction(this.ShowIllustView));
			}
			this.m_iCategoryID = categoryID;
			this.m_iBGGroupID = BGGroupID;
			this.OnIllustView = callback;
			base.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
		}

		// Token: 0x060090F9 RID: 37113 RVA: 0x00316C95 File Offset: 0x00314E95
		private void ShowIllustView()
		{
			if (this.OnIllustView != null)
			{
				this.OnIllustView(this.m_iCategoryID, this.m_iBGGroupID);
			}
		}

		// Token: 0x060090FA RID: 37114 RVA: 0x00316CB8 File Offset: 0x00314EB8
		public void SetData(NKCCollectionIllustData data = null)
		{
			if (data == null)
			{
				return;
			}
			if (!data.IsClearMission())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_ALBUM_SLOT_LOCK, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_ALBUM_SLOT_LOCK, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_ALBUM_SLOT_OPEN, true);
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_ALBUM_SLOT_ALBUM_TITLE_TEXT, data.m_BGGroupTitle);
			if (data.m_FileData.Count >= 1)
			{
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_COLLECTION_ALBUM_SLOT_BG_THUMBNAIL, this.GetThumbnail(data.m_FileData[0].m_BGThumbnailFileName), false);
			}
		}

		// Token: 0x060090FB RID: 37115 RVA: 0x00316D38 File Offset: 0x00314F38
		private Sprite GetThumbnail(string name)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_collection_thumbnail", name, false);
		}

		// Token: 0x060090FC RID: 37116 RVA: 0x00316D46 File Offset: 0x00314F46
		public void Clear()
		{
			this.m_AssetInstanceData.Unload();
		}

		// Token: 0x04007E43 RID: 32323
		public GameObject m_NKM_UI_COLLECTION_ALBUM_SLOT_LOCK;

		// Token: 0x04007E44 RID: 32324
		public GameObject m_NKM_UI_COLLECTION_ALBUM_SLOT_OPEN;

		// Token: 0x04007E45 RID: 32325
		public Text m_NKM_UI_COLLECTION_ALBUM_SLOT_ALBUM_TITLE_TEXT;

		// Token: 0x04007E46 RID: 32326
		public Image m_img_NKM_UI_COLLECTION_ALBUM_SLOT_BG_THUMBNAIL;

		// Token: 0x04007E47 RID: 32327
		public NKCUIComStateButton m_btn_NKM_UI_COLLECTION_ALBUM_SLOT_OPEN;

		// Token: 0x04007E48 RID: 32328
		private int m_iCategoryID;

		// Token: 0x04007E49 RID: 32329
		private int m_iBGGroupID;

		// Token: 0x04007E4A RID: 32330
		private NKCUICollectionIllust.OnIllustView OnIllustView;

		// Token: 0x04007E4B RID: 32331
		private NKCAssetInstanceData m_AssetInstanceData;
	}
}
