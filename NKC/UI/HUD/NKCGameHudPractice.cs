using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.HUD
{
	// Token: 0x02000C44 RID: 3140
	public class NKCGameHudPractice : MonoBehaviour
	{
		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x0600926C RID: 37484 RVA: 0x0031FBE3 File Offset: 0x0031DDE3
		private NKCGameClient GameClient
		{
			get
			{
				return this.m_GameHud.GetGameClient();
			}
		}

		// Token: 0x0600926D RID: 37485 RVA: 0x0031FBF0 File Offset: 0x0031DDF0
		public void Init(NKCGameHud gameHud)
		{
			this.m_GameHud = gameHud;
			NKCUtil.SetButtonClickDelegate(this.m_btnUpsideBack, new UnityAction(this.PracticeGoBack));
			NKCUtil.SetButtonClickDelegate(this.m_tglAI, new UnityAction<bool>(this.PracticeAIEnable));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnResetPlayer, new UnityAction(this.PracticeResetMy));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnResetEnemy, new UnityAction(this.PracticeResetEnemy));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSummonAir, new UnityAction(this.PracticeRespawnAir));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSummonGround, new UnityAction(this.PracticeRespawnLand));
			this.m_NKCUISkillSlot_Practice_1.Init(null);
			this.m_NKCUISkillSlot_Practice_2.Init(null);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSkill01, new UnityAction(this.PracticeSkillReset));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSkill02, new UnityAction(this.PracticeHyperSkillReset));
			NKCUtil.SetButtonClickDelegate(this.m_tglHeal, new UnityAction<bool>(this.PracticeHealEnable));
			NKCUtil.SetButtonClickDelegate(this.m_tglFixedDamage, new UnityAction<bool>(this.PracticeFixedDamageEnable));
		}

		// Token: 0x0600926E RID: 37486 RVA: 0x0031FD04 File Offset: 0x0031DF04
		public void SetEnable(bool value)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, value);
			if (value)
			{
				if (this.m_tglAI != null)
				{
					this.m_tglAI.Select(false, false, false);
				}
				if (this.m_tglHeal != null)
				{
					this.m_tglHeal.Select(true, false, false);
				}
				if (this.m_tglFixedDamage != null)
				{
					this.m_tglFixedDamage.Select(false, false, false);
				}
			}
		}

		// Token: 0x0600926F RID: 37487 RVA: 0x0031FD78 File Offset: 0x0031DF78
		public void LoadComplete(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (cNKMGameData.GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (cNKMGameData.m_NKMGameTeamDataA.m_listUnitData.Count <= 0)
			{
				return;
			}
			NKMUnitData nkmunitData = cNKMGameData.m_NKMGameTeamDataA.m_listUnitData[0];
			if (nkmunitData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKMUnitSkillTemplet nkmunitSkillTemplet = null;
			NKMUnitSkillTemplet nkmunitSkillTemplet2 = null;
			for (int i = 0; i < unitTempletBase.GetSkillCount(); i++)
			{
				NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(unitTempletBase.GetSkillStrID(i), nkmunitData);
				if (unitSkillTemplet != null)
				{
					if (unitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL)
					{
						nkmunitSkillTemplet = unitSkillTemplet;
					}
					else if (unitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER)
					{
						nkmunitSkillTemplet2 = unitSkillTemplet;
					}
				}
			}
			if (nkmunitSkillTemplet != null && !NKMUnitSkillManager.IsLockedSkill(nkmunitSkillTemplet.m_ID, (int)nkmunitData.m_LimitBreakLevel))
			{
				this.m_NKCUISkillSlot_Practice_1.SetData(nkmunitSkillTemplet, false);
			}
			else
			{
				this.m_NKCUISkillSlot_Practice_1.LockSkill(true);
			}
			if (nkmunitSkillTemplet2 != null && !NKMUnitSkillManager.IsLockedSkill(nkmunitSkillTemplet2.m_ID, (int)nkmunitData.m_LimitBreakLevel))
			{
				this.m_NKCUISkillSlot_Practice_2.SetData(nkmunitSkillTemplet2, true);
				return;
			}
			this.m_NKCUISkillSlot_Practice_2.LockSkill(true);
		}

		// Token: 0x06009270 RID: 37488 RVA: 0x0031FE77 File Offset: 0x0031E077
		public void PracticeGoBack()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().EndPracticeGame();
		}

		// Token: 0x06009271 RID: 37489 RVA: 0x0031FE9C File Offset: 0x0031E09C
		private void PracticeAIEnable(bool bOn)
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (bOn)
			{
				NKCLocalServerManager.GetGameServerLocal().GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable = false;
				return;
			}
			NKCLocalServerManager.GetGameServerLocal().GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable = true;
		}

		// Token: 0x06009272 RID: 37490 RVA: 0x0031FEEB File Offset: 0x0031E0EB
		public void PracticeResetMy()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCLocalServerManager.LocalGameUnitAllKill(false);
		}

		// Token: 0x06009273 RID: 37491 RVA: 0x0031FF07 File Offset: 0x0031E107
		public void PracticeResetEnemy()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCLocalServerManager.LocalGameUnitAllKill(true);
		}

		// Token: 0x06009274 RID: 37492 RVA: 0x0031FF23 File Offset: 0x0031E123
		public void PracticeRespawnAir()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCLocalServerManager.GetGameServerLocal().PracticeBossStateChange("USN_RESPAWN_AIR");
		}

		// Token: 0x06009275 RID: 37493 RVA: 0x0031FF48 File Offset: 0x0031E148
		public void PracticeRespawnLand()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCLocalServerManager.GetGameServerLocal().PracticeBossStateChange("USN_RESPAWN_LAND");
		}

		// Token: 0x06009276 RID: 37494 RVA: 0x0031FF6D File Offset: 0x0031E16D
		public void PracticeSkillReset()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetGameClient() == null)
			{
				return;
			}
			NKCScenManager.GetScenManager().GetGameClient().UI_GAME_SKILL_NORMAL_COOL_RESET(false);
		}

		// Token: 0x06009277 RID: 37495 RVA: 0x0031FFA0 File Offset: 0x0031E1A0
		public void PracticeHyperSkillReset()
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetGameClient() == null)
			{
				return;
			}
			NKCScenManager.GetScenManager().GetGameClient().UI_GAME_SKILL_HYPER_COOL_RESET(false);
		}

		// Token: 0x06009278 RID: 37496 RVA: 0x0031FFD3 File Offset: 0x0031E1D3
		public void PracticeHealEnable(bool value)
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCLocalServerManager.GetGameServerLocal().PracticeHealEnable(value);
		}

		// Token: 0x06009279 RID: 37497 RVA: 0x0031FFF4 File Offset: 0x0031E1F4
		public void PracticeFixedDamageEnable(bool value)
		{
			if (this.GameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			NKCLocalServerManager.GetGameServerLocal().PracticeFixedDamageEnable(value);
		}

		// Token: 0x04007F68 RID: 32616
		private NKCGameHud m_GameHud;

		// Token: 0x04007F69 RID: 32617
		public NKCUIComButton m_btnUpsideBack;

		// Token: 0x04007F6A RID: 32618
		public NKCUIComToggle m_tglAI;

		// Token: 0x04007F6B RID: 32619
		public NKCUIComStateButton m_csbtnResetPlayer;

		// Token: 0x04007F6C RID: 32620
		public NKCUIComStateButton m_csbtnResetEnemy;

		// Token: 0x04007F6D RID: 32621
		public NKCUIComStateButton m_csbtnSummonAir;

		// Token: 0x04007F6E RID: 32622
		public NKCUIComStateButton m_csbtnSummonGround;

		// Token: 0x04007F6F RID: 32623
		public NKCUIComStateButton m_csbtnSkill01;

		// Token: 0x04007F70 RID: 32624
		public NKCUIComStateButton m_csbtnSkill02;

		// Token: 0x04007F71 RID: 32625
		public NKCUIComToggle m_tglHeal;

		// Token: 0x04007F72 RID: 32626
		public NKCUIComToggle m_tglFixedDamage;

		// Token: 0x04007F73 RID: 32627
		public NKCUISkillSlot m_NKCUISkillSlot_Practice_1;

		// Token: 0x04007F74 RID: 32628
		public NKCUISkillSlot m_NKCUISkillSlot_Practice_2;
	}
}
