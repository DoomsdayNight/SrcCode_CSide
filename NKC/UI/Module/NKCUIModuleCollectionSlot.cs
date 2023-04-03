using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B1D RID: 2845
	public class NKCUIModuleCollectionSlot : MonoBehaviour
	{
		// Token: 0x06008191 RID: 33169 RVA: 0x002BAD39 File Offset: 0x002B8F39
		public void Init()
		{
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x002BAD3C File Offset: 0x002B8F3C
		public void SetData(NKCUISlot.SlotData slotData, bool awaken)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(slotData.ID);
			NKM_UNIT_GRADE unitBackground = NKM_UNIT_GRADE.NUG_N;
			if (unitTempletBase != null)
			{
				unitBackground = unitTempletBase.m_NKM_UNIT_GRADE;
			}
			this.SetUnitBackground(unitBackground);
			NKCUtil.SetGameobjectActive(this.m_objAwaken, awaken);
			if (this.m_imgUnit != null)
			{
				this.m_imgUnit.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
			}
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x002BAD94 File Offset: 0x002B8F94
		public void SetOwnState(bool owned)
		{
			NKCUtil.SetGameobjectActive(this.m_objNotOwned, !owned);
		}

		// Token: 0x06008194 RID: 33172 RVA: 0x002BADA8 File Offset: 0x002B8FA8
		private void SetUnitBackground(NKM_UNIT_GRADE grade)
		{
			if (this.m_imgRank == null)
			{
				return;
			}
			switch (grade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				this.m_imgRank.sprite = this.m_spRarityN;
				return;
			case NKM_UNIT_GRADE.NUG_R:
				this.m_imgRank.sprite = this.m_spRarityR;
				return;
			case NKM_UNIT_GRADE.NUG_SR:
				this.m_imgRank.sprite = this.m_spRaritySR;
				return;
			case NKM_UNIT_GRADE.NUG_SSR:
				this.m_imgRank.sprite = this.m_spRaritySSR;
				return;
			default:
				Debug.LogError("Unit BG undefined");
				this.m_imgRank.sprite = this.m_spRarityN;
				return;
			}
		}

		// Token: 0x06008195 RID: 33173 RVA: 0x002BAE3F File Offset: 0x002B903F
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
		}

		// Token: 0x04006DC0 RID: 28096
		public Image m_imgBG;

		// Token: 0x04006DC1 RID: 28097
		public Image m_imgRank;

		// Token: 0x04006DC2 RID: 28098
		public Image m_imgUnit;

		// Token: 0x04006DC3 RID: 28099
		public GameObject m_objNotOwned;

		// Token: 0x04006DC4 RID: 28100
		public GameObject m_objAwaken;

		// Token: 0x04006DC5 RID: 28101
		[Header("��� �̹���")]
		public Sprite m_spRarityN;

		// Token: 0x04006DC6 RID: 28102
		public Sprite m_spRarityR;

		// Token: 0x04006DC7 RID: 28103
		public Sprite m_spRaritySR;

		// Token: 0x04006DC8 RID: 28104
		public Sprite m_spRaritySSR;

		// Token: 0x04006DC9 RID: 28105
		private NKCAssetInstanceData m_NKCAssetInstanceData;
	}
}
