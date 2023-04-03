using System;
using System.Collections.Generic;
using System.Text;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000623 RID: 1571
	public class NKCGameHUDArtifact : MonoBehaviour
	{
		// Token: 0x0600307B RID: 12411 RVA: 0x000EF69C File Offset: 0x000ED89C
		public void Awake()
		{
			this.m_csbtnOpen.PointerClick.RemoveAllListeners();
			this.m_csbtnOpen.PointerClick.AddListener(new UnityAction(this.OnClickOpen));
			this.m_csbtnClose.PointerClick.RemoveAllListeners();
			this.m_csbtnClose.PointerClick.AddListener(new UnityAction(this.OnClickClose));
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000EF704 File Offset: 0x000ED904
		public static bool GetActive(NKMGameData cNKMGameData)
		{
			NKM_GAME_TYPE gameType = cNKMGameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_DIVE)
			{
				if (gameType - NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA > 1)
				{
					return cNKMGameData.m_BattleConditionIDs != null && cNKMGameData.m_BattleConditionIDs.Count > 0;
				}
				return NKCGuildCoopManager.GetMyArtifactDictionary().Count > 0 || (cNKMGameData.m_BattleConditionIDs != null && cNKMGameData.m_BattleConditionIDs.Count > 0);
			}
			else
			{
				NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
				if (diveGameData == null)
				{
					return false;
				}
				for (int i = 0; i < diveGameData.Player.PlayerBase.Artifacts.Count; i++)
				{
					NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(diveGameData.Player.PlayerBase.Artifacts[i]);
					if (nkmdiveArtifactTemplet != null && nkmdiveArtifactTemplet.BattleConditionID > 0)
					{
						return true;
					}
				}
				return cNKMGameData.m_BattleConditionIDs != null && cNKMGameData.m_BattleConditionIDs.Count > 0;
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000EF7DC File Offset: 0x000ED9DC
		public void SetUI(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			bool flag;
			switch (cNKMGameData.GetGameType())
			{
			case NKM_GAME_TYPE.NGT_DIVE:
				flag = this.SetDiveArtifacts(cNKMGameData);
				goto IL_9D;
			case NKM_GAME_TYPE.NGT_PVP_RANK:
			case NKM_GAME_TYPE.NGT_ASYNC_PVP:
			case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
			case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
			case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
			case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
				flag = this.SetPvpBattleCond(cNKMGameData);
				goto IL_9D;
			case NKM_GAME_TYPE.NGT_FIERCE:
				flag = this.SetFierceArtifacts(cNKMGameData);
				goto IL_9D;
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA:
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS:
				flag = this.SetGuildCoopArtifacts(cNKMGameData);
				goto IL_9D;
			}
			flag = this.SetBattleCond(cNKMGameData, false, NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_BATTLE_CONDITION);
			IL_9D:
			NKCUtil.SetGameobjectActive(this.m_NUF_GAME_HUD_ARTIFACT_ANI_TEXT, cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_DIVE);
			NKCUtil.SetGameobjectActive(this.m_NUF_GAME_HUD_BATTLE_CONDITION_ANI_TEXT, cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_FIERCE);
			NKCUtil.SetGameobjectActive(base.gameObject, flag);
			if (flag)
			{
				this.m_AmtorOpenClose.Play("NUF_GAME_HUD_ARTIFACT_CLOSE_IDLE");
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000EF8D0 File Offset: 0x000EDAD0
		private bool SetDiveArtifacts(NKMGameData cNKMGameData)
		{
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData == null)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < diveGameData.Player.PlayerBase.Artifacts.Count; i++)
			{
				NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(diveGameData.Player.PlayerBase.Artifacts[i]);
				if (nkmdiveArtifactTemplet != null && nkmdiveArtifactTemplet.BattleConditionID > 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			NKCUtil.SetLabelText(this.m_lbTotalViewDesc, NKCUtilString.GetDiveArtifactTotalViewDesc(diveGameData.Player.PlayerBase.Artifacts));
			NKCUtil.SetImageSprite(this.m_NUF_GAME_HUD_ARTIFACT_ICON, this.m_spDiveArtifact, false);
			return true;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000EF974 File Offset: 0x000EDB74
		private bool SetBattleCond(NKMGameData cNKMGameData, bool bUseArtifactIcon, params NKMBattleConditionTemplet.USE_CONTENT_TYPE[] useTypes)
		{
			if (cNKMGameData.m_BattleConditionIDs == null || cNKMGameData.m_BattleConditionIDs.Count <= 0)
			{
				return false;
			}
			HashSet<NKMBattleConditionTemplet.USE_CONTENT_TYPE> hashSet = new HashSet<NKMBattleConditionTemplet.USE_CONTENT_TYPE>(useTypes);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int bCondID in cNKMGameData.m_BattleConditionIDs.Keys)
			{
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
				if (templetByID != null && hashSet.Contains(templetByID.UseContentsType))
				{
					stringBuilder.AppendLine(templetByID.BattleCondName_Translated ?? "");
					stringBuilder.AppendLine(templetByID.BattleCondDesc_Translated ?? "");
					stringBuilder.AppendLine();
				}
			}
			NKCUtil.SetLabelText(this.m_lbTotalViewDesc, stringBuilder.ToString());
			NKCUtil.SetImageSprite(this.m_NUF_GAME_HUD_ARTIFACT_ICON, bUseArtifactIcon ? this.m_spDiveArtifact : this.m_spBattleCondition, false);
			return true;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000EFA64 File Offset: 0x000EDC64
		private bool SetBattleCond(NKMGameData cNKMGameData, bool bUseArtifactIcon, NKMBattleConditionTemplet.USE_CONTENT_TYPE useType)
		{
			if (cNKMGameData.m_BattleConditionIDs == null || cNKMGameData.m_BattleConditionIDs.Count <= 0)
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int bCondID in cNKMGameData.m_BattleConditionIDs.Keys)
			{
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
				if (templetByID != null && templetByID.UseContentsType == useType)
				{
					stringBuilder.AppendLine(templetByID.BattleCondName_Translated ?? "");
					stringBuilder.AppendLine(templetByID.BattleCondDesc_Translated ?? "");
					stringBuilder.AppendLine();
				}
			}
			NKCUtil.SetLabelText(this.m_lbTotalViewDesc, stringBuilder.ToString());
			NKCUtil.SetImageSprite(this.m_NUF_GAME_HUD_ARTIFACT_ICON, bUseArtifactIcon ? this.m_spDiveArtifact : this.m_spBattleCondition, false);
			return true;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000EFB48 File Offset: 0x000EDD48
		private bool SetPvpBattleCond(NKMGameData cNKMGameData)
		{
			if (cNKMGameData.m_BattleConditionIDs != null && cNKMGameData.m_BattleConditionIDs.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int bCondID in cNKMGameData.m_BattleConditionIDs.Keys)
				{
					NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
					if (templetByID != null && templetByID.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_BATTLE_CONDITION)
					{
						stringBuilder.AppendLine(templetByID.BattleCondName_Translated ?? "");
						stringBuilder.AppendLine(templetByID.BattleCondDesc_Translated ?? "");
						stringBuilder.AppendLine();
					}
				}
				NKCUtil.SetLabelText(this.m_lbTotalViewDesc, stringBuilder.ToString());
				NKCUtil.SetImageSprite(this.m_NUF_GAME_HUD_ARTIFACT_ICON, this.m_spBattleCondition, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000EFC28 File Offset: 0x000EDE28
		private bool SetFierceArtifacts(NKMGameData cNKMGameData)
		{
			if (cNKMGameData.m_BattleConditionIDs != null && cNKMGameData.m_BattleConditionIDs.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int bCondID in cNKMGameData.m_BattleConditionIDs.Keys)
				{
					NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
					if (templetByID != null && (templetByID.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_BATTLE_CONDITION || templetByID.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_FIERCE_PENALTY))
					{
						stringBuilder.AppendLine(templetByID.BattleCondName_Translated ?? "");
						stringBuilder.AppendLine(templetByID.BattleCondDesc_Translated ?? "");
						stringBuilder.AppendLine();
					}
				}
				NKCUtil.SetLabelText(this.m_lbTotalViewDesc, stringBuilder.ToString());
				NKCUtil.SetImageSprite(this.m_NUF_GAME_HUD_ARTIFACT_ICON, this.m_spBattleCondition, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000EFD10 File Offset: 0x000EDF10
		private bool SetGuildCoopArtifacts(NKMGameData cNKMGameData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (cNKMGameData.m_BattleConditionIDs != null && cNKMGameData.m_BattleConditionIDs.Count > 0)
			{
				foreach (int bCondID in cNKMGameData.m_BattleConditionIDs.Keys)
				{
					NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
					if (templetByID != null && templetByID.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_BATTLE_CONDITION)
					{
						stringBuilder.AppendLine(templetByID.BattleCondName_Translated ?? "");
						stringBuilder.AppendLine(templetByID.BattleCondDesc_Translated ?? "");
						stringBuilder.AppendLine();
					}
				}
			}
			Dictionary<int, List<GuildDungeonArtifactTemplet>> myArtifactDictionary = NKCGuildCoopManager.GetMyArtifactDictionary();
			if (myArtifactDictionary.Count > 0)
			{
				List<int> list = new List<int>();
				foreach (KeyValuePair<int, List<GuildDungeonArtifactTemplet>> keyValuePair in myArtifactDictionary)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						list.Add(keyValuePair.Value[i].GetArtifactId());
					}
				}
				stringBuilder.Append(NKCUtilString.GetGuildArtifactTotalViewDesc(list));
			}
			if (stringBuilder.Length > 0)
			{
				NKCUtil.SetLabelText(this.m_lbTotalViewDesc, stringBuilder.ToString());
				NKCUtil.SetImageSprite(this.m_NUF_GAME_HUD_ARTIFACT_ICON, this.m_spBattleCondition, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000EFE88 File Offset: 0x000EE088
		public void PlayEffectNoticeAni()
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			this.m_AmtorArtifactEffectAlarm.Play("NUF_GAME_HUD_ARTIFACT");
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000EFEA8 File Offset: 0x000EE0A8
		private void OnClickOpen()
		{
			this.m_AmtorOpenClose.Play("NUF_GAME_HUD_ARTIFACT_OPEN");
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000EFEBA File Offset: 0x000EE0BA
		private void OnClickClose()
		{
			this.m_AmtorOpenClose.Play("NUF_GAME_HUD_ARTIFACT_CLOSE");
		}

		// Token: 0x04002FF0 RID: 12272
		public Animator m_AmtorOpenClose;

		// Token: 0x04002FF1 RID: 12273
		public Animator m_AmtorArtifactEffectAlarm;

		// Token: 0x04002FF2 RID: 12274
		public Text m_lbTotalViewDesc;

		// Token: 0x04002FF3 RID: 12275
		public NKCUIComStateButton m_csbtnOpen;

		// Token: 0x04002FF4 RID: 12276
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04002FF5 RID: 12277
		public Image m_NUF_GAME_HUD_ARTIFACT_ICON;

		// Token: 0x04002FF6 RID: 12278
		public Sprite m_spDiveArtifact;

		// Token: 0x04002FF7 RID: 12279
		public Sprite m_spBattleCondition;

		// Token: 0x04002FF8 RID: 12280
		public GameObject m_NUF_GAME_HUD_ARTIFACT_ANI_TEXT;

		// Token: 0x04002FF9 RID: 12281
		public GameObject m_NUF_GAME_HUD_BATTLE_CONDITION_ANI_TEXT;
	}
}
