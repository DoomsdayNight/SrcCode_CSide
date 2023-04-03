using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000632 RID: 1586
	public class NKCASUnitShadow : NKMObjectPoolData
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x000F4119 File Offset: 0x000F2319
		public NKCASUnitShadow(bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitShadow;
			this.Load(bAsync);
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000F4130 File Offset: 0x000F2330
		public override void Load(bool bAsync)
		{
			this.m_ShadowSpriteInstant = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UNIT_GAME_NKM_UNIT", "NKM_UNIT_SHADOW", bAsync, null);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000F414C File Offset: 0x000F234C
		public override bool LoadComplete()
		{
			if (this.m_ShadowSpriteInstant == null || this.m_ShadowSpriteInstant.m_Instant == null)
			{
				Debug.LogError("Shadow Sprite load failed!!");
				return false;
			}
			this.m_ShadowSpriteInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_SHADOW().transform, false);
			this.m_TeamColor_Green = this.m_ShadowSpriteInstant.m_Instant.transform.Find("fx_mark_green").gameObject;
			this.m_TeamColor_Green_fx_mark_green_big = this.m_TeamColor_Green.transform.Find("fx_mark_green_big").gameObject;
			this.m_TeamColor_Green_fx_mark_green_mini = this.m_TeamColor_Green.transform.Find("fx_mark_green_mini").gameObject;
			this.m_TeamColor_Red = this.m_ShadowSpriteInstant.m_Instant.transform.Find("fx_mark_red").gameObject;
			this.m_TeamColor_Red_fx_mark_red_big = this.m_TeamColor_Red.transform.Find("fx_mark_red_big").gameObject;
			this.m_TeamColor_Red_fx_mark_red_mini = this.m_TeamColor_Red.transform.Find("fx_mark_red_mini").gameObject;
			this.m_Rearmament_Green = this.m_ShadowSpriteInstant.m_Instant.transform.Find("FX_REARMAMENT_GREEN").gameObject;
			this.m_Rearmament_Red = this.m_ShadowSpriteInstant.m_Instant.transform.Find("FX_REARMAMENT_RED").gameObject;
			this.m_TeamColor_common_shadow = this.m_ShadowSpriteInstant.m_Instant.transform.Find("common_shadow").gameObject;
			this.m_NKCComGroupColor = this.m_ShadowSpriteInstant.m_Instant.GetComponent<NKCComGroupColor>();
			return true;
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000F42FF File Offset: 0x000F24FF
		public override void Open()
		{
			if (!this.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_ShadowSpriteInstant.m_Instant.SetActive(true);
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000F4324 File Offset: 0x000F2524
		public override void Close()
		{
			if (this.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_ShadowSpriteInstant.m_Instant.SetActive(false);
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000F434C File Offset: 0x000F254C
		public override void Unload()
		{
			this.m_TeamColor_Green = null;
			this.m_TeamColor_Green_fx_mark_green_big = null;
			this.m_TeamColor_Green_fx_mark_green_mini = null;
			this.m_TeamColor_Red = null;
			this.m_TeamColor_Red_fx_mark_red_big = null;
			this.m_TeamColor_Red_fx_mark_red_mini = null;
			this.m_TeamColor_common_shadow = null;
			this.m_NKCComGroupColor = null;
			NKCAssetResourceManager.CloseInstance(this.m_ShadowSpriteInstant);
			this.m_ShadowSpriteInstant = null;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000F43A3 File Offset: 0x000F25A3
		public void SetShadowType(NKC_TEAM_COLOR_TYPE eNKC_TEAM_COLOR_TYPE, bool bTeamA, bool bRearm)
		{
			this.SetShadowType(eNKC_TEAM_COLOR_TYPE);
			this.SetShadowTeam(bTeamA, bRearm);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000F43B4 File Offset: 0x000F25B4
		public void SetShadowType(NKC_TEAM_COLOR_TYPE eNKC_TEAM_COLOR_TYPE)
		{
			switch (eNKC_TEAM_COLOR_TYPE)
			{
			case NKC_TEAM_COLOR_TYPE.NTCT_NO:
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_big, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_big, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_common_shadow, false);
				return;
			case NKC_TEAM_COLOR_TYPE.NTCT_ONLY_SHADOW:
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_big, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_big, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_common_shadow, true);
				return;
			case NKC_TEAM_COLOR_TYPE.NTCT_SIMPLE:
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_big, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_big, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_common_shadow, true);
				return;
			case NKC_TEAM_COLOR_TYPE.NTCT_FULL:
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_big, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_mini, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_big, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_mini, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_common_shadow, true);
				return;
			case NKC_TEAM_COLOR_TYPE.NTCT_SIMPLE_NO_SHADOW:
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_big, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Green_fx_mark_green_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_big, true);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_Red_fx_mark_red_mini, false);
				NKCUtil.SetGameobjectActive(this.m_TeamColor_common_shadow, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000F450C File Offset: 0x000F270C
		public void SetShadowTeam(bool bTeamA, bool bRearm)
		{
			NKCUtil.SetGameobjectActive(this.m_Rearmament_Green, bTeamA && bRearm);
			NKCUtil.SetGameobjectActive(this.m_Rearmament_Red, !bTeamA && bRearm);
			NKCUtil.SetGameobjectActive(this.m_TeamColor_Green, bTeamA && !bRearm);
			NKCUtil.SetGameobjectActive(this.m_TeamColor_Red, !bTeamA && !bRearm);
		}

		// Token: 0x04003072 RID: 12402
		public NKCAssetInstanceData m_ShadowSpriteInstant;

		// Token: 0x04003073 RID: 12403
		public GameObject m_TeamColor_Green;

		// Token: 0x04003074 RID: 12404
		public GameObject m_TeamColor_Green_fx_mark_green_big;

		// Token: 0x04003075 RID: 12405
		public GameObject m_TeamColor_Green_fx_mark_green_mini;

		// Token: 0x04003076 RID: 12406
		public GameObject m_TeamColor_Red;

		// Token: 0x04003077 RID: 12407
		public GameObject m_TeamColor_Red_fx_mark_red_big;

		// Token: 0x04003078 RID: 12408
		public GameObject m_TeamColor_Red_fx_mark_red_mini;

		// Token: 0x04003079 RID: 12409
		public GameObject m_Rearmament_Green;

		// Token: 0x0400307A RID: 12410
		public GameObject m_Rearmament_Red;

		// Token: 0x0400307B RID: 12411
		public GameObject m_TeamColor_common_shadow;

		// Token: 0x0400307C RID: 12412
		public NKCComGroupColor m_NKCComGroupColor;
	}
}
