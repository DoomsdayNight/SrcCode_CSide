using System;
using System.Collections.Generic;
using NKC.Templet;
using NKC.UI.Component;
using NKM.Contract2;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BEA RID: 3050
	public class NKCUIContractListSlot : NKCUIComFoldableListSlot
	{
		// Token: 0x06008D6F RID: 36207 RVA: 0x0030185C File Offset: 0x002FFA5C
		protected override void SetData(NKCUIComFoldableList.Element element)
		{
			NKCContractCategoryTemplet nkccontractCategoryTemplet = NKCContractCategoryTemplet.Find(element.MajorKey);
			if (element.isMajor)
			{
				NKCUtil.SetImageColor(this.m_imgContract, this.m_colBasic);
				if (this.m_Toggle != null)
				{
					this.m_Toggle.SetTitleText(nkccontractCategoryTemplet.GetName());
				}
				this.SetImage("");
				return;
			}
			if (NKCScenManager.GetScenManager().GetNKCContractDataMgr() != null)
			{
				switch (nkccontractCategoryTemplet.m_Type)
				{
				case NKCContractCategoryTemplet.TabType.Basic:
					NKCUtil.SetImageColor(this.m_imgContract, this.m_colBasic);
					goto IL_133;
				case NKCContractCategoryTemplet.TabType.Awaken:
					NKCUtil.SetImageColor(this.m_imgContract, this.m_colClassified);
					goto IL_133;
				case NKCContractCategoryTemplet.TabType.FollowTarget:
					NKCUtil.SetImageColor(this.m_imgContract, this.m_colBasic);
					using (IEnumerator<RandomUnitTempletV2> enumerator = ContractTempletV2.Find(element.MinorKey).UnitPoolTemplet.UnitTemplets.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							RandomUnitTempletV2 randomUnitTempletV = enumerator.Current;
							if (randomUnitTempletV.PickUpTarget && randomUnitTempletV.UnitTemplet.m_bAwaken)
							{
								NKCUtil.SetImageColor(this.m_imgContract, this.m_colClassified);
								break;
							}
						}
						goto IL_133;
					}
					break;
				case NKCContractCategoryTemplet.TabType.Hidden:
					goto IL_133;
				case NKCContractCategoryTemplet.TabType.Confirm:
					break;
				default:
					goto IL_133;
				}
				NKCUtil.SetImageColor(this.m_imgContract, this.m_colConfirm);
				IL_133:
				if (this.m_Toggle != null)
				{
					ContractTempletBase contractTempletBase = ContractTempletBase.FindBase(element.MinorKey);
					this.m_Toggle.SetTitleText(contractTempletBase.GetContractName());
					return;
				}
			}
			else
			{
				NKCUtil.SetImageColor(this.m_imgContract, this.m_colBasic);
				if (this.m_Toggle != null)
				{
					this.m_Toggle.SetTitleText("");
				}
			}
		}

		// Token: 0x06008D70 RID: 36208 RVA: 0x00301A0C File Offset: 0x002FFC0C
		public void SetActiveRedDot(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objRedDot, bValue);
		}

		// Token: 0x06008D71 RID: 36209 RVA: 0x00301A1A File Offset: 0x002FFC1A
		public void SetImage(string imageName)
		{
			if (!string.IsNullOrEmpty(imageName))
			{
				NKCUtil.SetImageSprite(this.m_imgContract, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONTRACT_V2_Tab_Bg", imageName, false), false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgContract, null, false);
		}

		// Token: 0x04007A40 RID: 31296
		private const string TAB_IMAGE_ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONTRACT_V2_Tab_Bg";

		// Token: 0x04007A41 RID: 31297
		public Image m_imgContract;

		// Token: 0x04007A42 RID: 31298
		public Color m_colBasic;

		// Token: 0x04007A43 RID: 31299
		public Color m_colClassified;

		// Token: 0x04007A44 RID: 31300
		public Color m_colConfirm;

		// Token: 0x04007A45 RID: 31301
		public GameObject m_objRedDot;
	}
}
