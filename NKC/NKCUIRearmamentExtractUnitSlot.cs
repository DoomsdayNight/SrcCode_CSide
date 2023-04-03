using System;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000805 RID: 2053
	public class NKCUIRearmamentExtractUnitSlot : MonoBehaviour
	{
		// Token: 0x06005159 RID: 20825 RVA: 0x0018B1B0 File Offset: 0x001893B0
		public void SetData(NKMUnitData unitData)
		{
			if (unitData != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
				NKM_UNIT_GRADE nkm_UNIT_GRADE = unitTempletBase.m_NKM_UNIT_GRADE;
				if (nkm_UNIT_GRADE != NKM_UNIT_GRADE.NUG_SR)
				{
					if (nkm_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
					{
						NKCUtil.SetImageSprite(this.m_imgBG, NKCUtil.GetGuildArtifactBgProbImage(GuildDungeonArtifactTemplet.ArtifactProbType.HIGH), false);
					}
					else
					{
						NKCUtil.SetImageSprite(this.m_imgBG, null, false);
					}
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_imgBG, NKCUtil.GetGuildArtifactBgProbImage(GuildDungeonArtifactTemplet.ArtifactProbType.MIDDLE), false);
				}
				NKCUtil.SetImageSprite(this.m_imgClass, NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, true), false);
				NKCUtil.SetImageSprite(this.m_imgFace, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitData), true);
				NKCUtil.SetGameobjectActive(this.m_objContractUnit, unitData.FromContract);
			}
			NKCUtil.SetGameobjectActive(this.m_ObjOff, unitData == null);
			NKCUtil.SetGameobjectActive(this.m_objOn, unitData != null);
		}

		// Token: 0x040041CE RID: 16846
		public GameObject m_objOn;

		// Token: 0x040041CF RID: 16847
		public GameObject m_ObjOff;

		// Token: 0x040041D0 RID: 16848
		public GameObject m_objContractUnit;

		// Token: 0x040041D1 RID: 16849
		public Image m_imgFace;

		// Token: 0x040041D2 RID: 16850
		public Image m_imgBG;

		// Token: 0x040041D3 RID: 16851
		public Image m_imgClass;
	}
}
