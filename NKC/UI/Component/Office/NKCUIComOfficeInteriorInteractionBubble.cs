using System;
using System.Collections.Generic;
using NKC.Office;
using NKC.Templet.Office;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C6A RID: 3178
	public class NKCUIComOfficeInteriorInteractionBubble : MonoBehaviour
	{
		// Token: 0x060093C7 RID: 37831 RVA: 0x0032768C File Offset: 0x0032588C
		public void SetData(int itemID)
		{
			NKMOfficeInteriorTemplet data = NKMItemMiscTemplet.FindInterior(itemID);
			this.SetData(data);
		}

		// Token: 0x060093C8 RID: 37832 RVA: 0x003276A8 File Offset: 0x003258A8
		public void SetData(NKMItemMiscTemplet miscItemTemplet)
		{
			if (miscItemTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (miscItemTemplet.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKMOfficeInteriorTemplet data = NKMItemMiscTemplet.FindInterior(miscItemTemplet.m_ItemMiscID);
			this.SetData(data);
		}

		// Token: 0x060093C9 RID: 37833 RVA: 0x003276F0 File Offset: 0x003258F0
		public void SetData(NKMOfficeInteriorTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			List<NKCOfficeFurnitureInteractionTemplet> interactionTempletList = NKCOfficeFurnitureInteractionTemplet.GetInteractionTempletList(templet, NKCOfficeFurnitureInteractionTemplet.ActType.Common);
			if (interactionTempletList == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCOfficeFurnitureInteractionTemplet nkcofficeFurnitureInteractionTemplet = null;
			int num = 0;
			foreach (NKCOfficeFurnitureInteractionTemplet nkcofficeFurnitureInteractionTemplet2 in interactionTempletList)
			{
				num = nkcofficeFurnitureInteractionTemplet2.GetFirstExclusiveActTarget();
				if (num > 0)
				{
					nkcofficeFurnitureInteractionTemplet = nkcofficeFurnitureInteractionTemplet2;
					break;
				}
			}
			if (nkcofficeFurnitureInteractionTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			ActTargetType eActTargetType = nkcofficeFurnitureInteractionTemplet.eActTargetType;
			if (eActTargetType != ActTargetType.Unit)
			{
				if (eActTargetType == ActTargetType.Skin)
				{
					NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(num);
					if (skinTemplet == null)
					{
						Debug.LogError(string.Format("skin {0} does not exists!", num));
						NKCUtil.SetGameobjectActive(base.gameObject, false);
						return;
					}
					Sprite sp = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet);
					NKCUtil.SetImageSprite(this.m_ImgUnitIcon, sp, false);
					NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(skinTemplet.m_SkinEquipUnitID);
					NKCUtil.SetLabelText(this.m_lbUnitName, nkmunitTempletBase.GetUnitName());
				}
			}
			else
			{
				NKMUnitTempletBase nkmunitTempletBase2 = NKMUnitTempletBase.Find(num);
				Sprite sp2 = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, nkmunitTempletBase2);
				NKCUtil.SetImageSprite(this.m_ImgUnitIcon, sp2, false);
				NKCUtil.SetLabelText(this.m_lbUnitName, nkmunitTempletBase2.GetUnitName());
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x040080C2 RID: 32962
		public Image m_ImgUnitIcon;

		// Token: 0x040080C3 RID: 32963
		public Text m_lbUnitName;
	}
}
