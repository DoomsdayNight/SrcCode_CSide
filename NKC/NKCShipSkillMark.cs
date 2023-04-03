using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006C7 RID: 1735
	public class NKCShipSkillMark
	{
		// Token: 0x06003C2C RID: 15404 RVA: 0x001346E4 File Offset: 0x001328E4
		public void Init()
		{
			if (this.m_NKM_SHIP_SKILL_MARK == null && NKCScenManager.GetScenManager().Get_SCEN_GAME() != null && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_MAP() != null)
			{
				this.m_NKM_SHIP_SKILL_MARK = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_UI_SHIP_SKILL_TARGET", "AB_FX_UI_SHIP_SKILL_TARGET_CENTER", false, null);
				this.m_NKM_SHIP_SKILL_MARK_LAND = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_UI_SHIP_SKILL_TARGET", "AB_FX_UI_SHIP_SKILL_TARGET_LAND", false, null);
				this.m_NKM_SHIP_SKILL_MARK.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_MAP().transform);
				this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_MAP().transform);
			}
			this.SetShow(false);
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x001347BC File Offset: 0x001329BC
		public void SetShow(bool bShow)
		{
			if (this.m_NKM_SHIP_SKILL_MARK == null || this.m_NKM_SHIP_SKILL_MARK_LAND == null)
			{
				return;
			}
			if (bShow)
			{
				if (!this.m_NKM_SHIP_SKILL_MARK.m_Instant.activeSelf)
				{
					this.m_NKM_SHIP_SKILL_MARK.m_Instant.SetActive(true);
				}
				if (!this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.activeSelf)
				{
					this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.SetActive(true);
					return;
				}
			}
			else
			{
				if (this.m_NKM_SHIP_SKILL_MARK.m_Instant.activeSelf)
				{
					this.m_NKM_SHIP_SKILL_MARK.m_Instant.SetActive(false);
				}
				if (this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.activeSelf)
				{
					this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.SetActive(false);
				}
			}
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x0013486C File Offset: 0x00132A6C
		public void SetPos(float fX, float fZ)
		{
			this.m_Vec3Temp = this.m_NKM_SHIP_SKILL_MARK.m_Instant.transform.localPosition;
			this.m_Vec3Temp.x = fX;
			this.m_Vec3Temp.y = fZ;
			this.m_NKM_SHIP_SKILL_MARK.m_Instant.transform.localPosition = this.m_Vec3Temp;
			this.m_Vec3Temp = this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.transform.localPosition;
			this.m_Vec3Temp.x = fX;
			this.m_Vec3Temp.y = fZ;
			this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.transform.localPosition = this.m_Vec3Temp;
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x00134918 File Offset: 0x00132B18
		public void SetScale(float fX, float fY)
		{
			this.m_Vec3Temp.x = fX;
			this.m_Vec3Temp.y = fY;
			this.m_Vec3Temp.z = 1f;
			this.m_NKM_SHIP_SKILL_MARK_LAND.m_Instant.transform.localScale = this.m_Vec3Temp;
		}

		// Token: 0x040035CE RID: 13774
		private NKCAssetInstanceData m_NKM_SHIP_SKILL_MARK;

		// Token: 0x040035CF RID: 13775
		private NKCAssetInstanceData m_NKM_SHIP_SKILL_MARK_LAND;

		// Token: 0x040035D0 RID: 13776
		private Vector3 m_Vec3Temp;
	}
}
