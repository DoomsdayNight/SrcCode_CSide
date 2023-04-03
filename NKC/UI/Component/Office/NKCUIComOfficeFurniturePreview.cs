using System;
using NKC.Office;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C64 RID: 3172
	public class NKCUIComOfficeFurniturePreview : MonoBehaviour
	{
		// Token: 0x060093A8 RID: 37800 RVA: 0x00326BAC File Offset: 0x00324DAC
		public void SetData(NKMOfficeInteriorTemplet templet)
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(this.m_lbName, templet != null);
			NKCUtil.SetGameobjectActive(this.m_lbDescription, templet != null);
			NKCUtil.SetGameobjectActive(this.m_objEnvPoint, templet != null);
			if (templet != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, templet.GetItemName());
				NKCUtil.SetLabelText(this.m_lbDescription, templet.GetItemDesc());
				NKCUtil.SetLabelText(this.m_lbEnvPoint, templet.InteriorScore.ToString());
				NKCUtil.SetGameobjectActive(this.m_rtFurnitureRoot, templet.Target != InteriorTarget.Background);
				NKCUtil.SetGameobjectActive(this.m_imgBackgroundIcon, templet.Target == InteriorTarget.Background);
				if (templet.Target == InteriorTarget.Background)
				{
					NKCUtil.SetImageSprite(this.m_imgBackgroundIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(templet), false);
					return;
				}
				this.m_rtFurnitureRoot.localScale = Vector3.one;
				this.m_furniturePreview = NKCOfficeFuniture.GetInstance(-1L, templet, 100f, this.m_rtFurnitureRoot);
				if (this.m_furniturePreview == null)
				{
					Debug.LogError("Furniture prefab not exist! itemID : " + templet.m_ItemMiscID.ToString());
					return;
				}
				this.m_furniturePreview.SetShowTile(this.bShowTile);
				if (this.m_furniturePreview != null)
				{
					this.m_furniturePreview.SetInvert(false, true);
					this.m_furniturePreview.transform.localPosition = Vector3.zero;
				}
				this.FitFurnitureToRect(this.m_furniturePreview);
			}
		}

		// Token: 0x060093A9 RID: 37801 RVA: 0x00326D14 File Offset: 0x00324F14
		public void SetData(NKMOfficeThemePresetTemplet templet)
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(this.m_lbName, templet != null);
			NKCUtil.SetGameobjectActive(this.m_lbDescription, true);
			NKCUtil.SetGameobjectActive(this.m_objEnvPoint, false);
			if (templet != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, NKCStringTable.GetString(templet.ThemaPresetStringID, false));
				NKCUtil.SetLabelText(this.m_lbDescription, NKCStringTable.GetString(templet.ThemaPresetDescID, false));
				NKCUtil.SetGameobjectActive(this.m_rtFurnitureRoot, false);
				NKCUtil.SetGameobjectActive(this.m_imgBackgroundIcon, true);
				NKCUtil.SetImageSprite(this.m_imgBackgroundIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_INVEN_ICON_FNC_THEME", templet.ThemaPresetIMG)), false);
			}
		}

		// Token: 0x060093AA RID: 37802 RVA: 0x00326DB8 File Offset: 0x00324FB8
		private void FitFurnitureToRect(NKCOfficeFuniture furniture)
		{
			Rect worldRect = furniture.GetWorldRect(!this.bShowTile);
			Vector2 a;
			a.x = furniture.transform.position.x - worldRect.center.x;
			a.y = furniture.transform.position.y - worldRect.center.y;
			Vector3 vector = default(Vector3);
			if (worldRect.width / this.m_rtFurniturePreviewViewport.GetWidth() > worldRect.height / this.m_rtFurniturePreviewViewport.GetHeight())
			{
				float width = this.m_rtFurniturePreviewViewport.GetWidth();
				vector.x = Mathf.Min(width / worldRect.width, 1f);
				vector.y = vector.x;
			}
			else
			{
				float height = this.m_rtFurniturePreviewViewport.GetHeight();
				vector.y = Mathf.Min(height / worldRect.height, 1f);
				vector.x = vector.y;
			}
			vector.z = 0f;
			this.m_rtFurnitureRoot.localScale = vector;
			this.m_rtFurnitureRoot.anchoredPosition = a * vector;
		}

		// Token: 0x060093AB RID: 37803 RVA: 0x00326EE6 File Offset: 0x003250E6
		public void Clear()
		{
			if (this.m_furniturePreview != null)
			{
				this.m_furniturePreview.CleanUp();
				this.m_furniturePreview = null;
			}
		}

		// Token: 0x0400809D RID: 32925
		public Text m_lbName;

		// Token: 0x0400809E RID: 32926
		public Text m_lbDescription;

		// Token: 0x0400809F RID: 32927
		public GameObject m_objEnvPoint;

		// Token: 0x040080A0 RID: 32928
		public Text m_lbEnvPoint;

		// Token: 0x040080A1 RID: 32929
		public RectTransform m_rtFurniturePreviewViewport;

		// Token: 0x040080A2 RID: 32930
		public RectTransform m_rtFurnitureRoot;

		// Token: 0x040080A3 RID: 32931
		public Image m_imgBackgroundIcon;

		// Token: 0x040080A4 RID: 32932
		public bool bShowTile;

		// Token: 0x040080A5 RID: 32933
		private NKCOfficeFuniture m_furniturePreview;
	}
}
