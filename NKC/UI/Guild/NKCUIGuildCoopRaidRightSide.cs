using System;
using System.Collections.Generic;
using Cs.Math;
using NKC.PacketHandler;
using NKC.UI.Guide;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B3B RID: 2875
	public class NKCUIGuildCoopRaidRightSide : NKCUIInstantiatable
	{
		// Token: 0x060082EB RID: 33515 RVA: 0x002C28E0 File Offset: 0x002C0AE0
		public GuildRaidTemplet GetRaidTemplet()
		{
			return GuildDungeonTempletManager.GetGuildRaidTemplet(this.m_RaidGroupId, this.m_BossStageId);
		}

		// Token: 0x060082EC RID: 33516 RVA: 0x002C28F4 File Offset: 0x002C0AF4
		public static NKCUIGuildCoopRaidRightSide OpenInstance(Transform trParent, NKCUIGuildCoopRaidRightSide.onClickAttackBtn _onClickAttackBtn = null)
		{
			NKCUIGuildCoopRaidRightSide nkcuiguildCoopRaidRightSide = NKCUIInstantiatable.OpenInstance<NKCUIGuildCoopRaidRightSide>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_CONSORTIUM_COOP_RAID_RIGHT", trParent);
			if (nkcuiguildCoopRaidRightSide != null)
			{
				nkcuiguildCoopRaidRightSide.Init(_onClickAttackBtn);
			}
			return nkcuiguildCoopRaidRightSide;
		}

		// Token: 0x060082ED RID: 33517 RVA: 0x002C2923 File Offset: 0x002C0B23
		public void CloseInstance()
		{
			base.CloseInstance("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_CONSORTIUM_COOP_RAID_RIGHT");
		}

		// Token: 0x060082EE RID: 33518 RVA: 0x002C2938 File Offset: 0x002C0B38
		public void Init(NKCUIGuildCoopRaidRightSide.onClickAttackBtn _onClickAttackBtn = null)
		{
			this.m_dOnClickAttackBtn = _onClickAttackBtn;
			this.m_csbtnClear.PointerClick.RemoveAllListeners();
			this.m_csbtnClear.PointerClick.AddListener(new UnityAction(this.OnClickAttackBtn));
			this.m_csbtnClear.m_bGetCallbackWhileLocked = true;
			this.m_csbtnInfo.PointerClick.RemoveAllListeners();
			this.m_csbtnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfoBtn));
			if (this.m_btnGuide != null)
			{
				this.m_btnGuide.PointerClick.RemoveAllListeners();
				this.m_btnGuide.PointerClick.AddListener(new UnityAction(this.OnClickGuide));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_Artifact.Init();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060082EF RID: 33519 RVA: 0x002C2A0D File Offset: 0x002C0C0D
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060082F0 RID: 33520 RVA: 0x002C2A1B File Offset: 0x002C0C1B
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060082F1 RID: 33521 RVA: 0x002C2A2C File Offset: 0x002C0C2C
		public void SetUI(int raidGroupId, int bossStageId)
		{
			this.m_RaidGroupId = raidGroupId;
			this.m_BossStageId = bossStageId;
			GuildRaidTemplet raidTemplet = this.GetRaidTemplet();
			if (raidTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_LEVEL_INFO, NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageIndex()));
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(raidTemplet.GetStageId());
			if (dungeonTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, dungeonTempletBase.GetDungeonName());
			}
			float num = NKCGuildCoopManager.m_BossRemainHp / NKCGuildCoopManager.m_BossMaxHp;
			if (num.IsNearlyZero(1E-05f))
			{
				this.m_imgBossHP.fillAmount = 0f;
			}
			else
			{
				this.m_imgBossHP.fillAmount = num;
			}
			NKCUtil.SetLabelText(this.m_lbRemainHP, string.Format("{0} ({1:0.##}%)", ((int)NKCGuildCoopManager.m_BossRemainHp).ToString("N0"), num * 100f));
			NKCUtil.SetGameobjectActive(this.m_btnGuide, !string.IsNullOrEmpty(raidTemplet.GetGuideShortCut()));
			this.m_Artifact.SetData(NKCGuildCoopManager.GetMyArtifactDictionary());
			if (this.m_csbtnClear != null)
			{
				if (NKCGuildCoopManager.m_BossPlayableCount > 0)
				{
					this.m_csbtnClear.UnLock(false);
				}
				else
				{
					this.m_csbtnClear.Lock(false);
				}
				NKCUtil.SetGameobjectActive(this.m_objRemainCount, NKCGuildCoopManager.m_BossPlayableCount > 0);
				NKCUtil.SetLabelText(this.m_lbRemainCount, string.Format(NKCUtilString.GET_STRING_RAID_REMAIN_COUNT_ONE_PARAM, NKCGuildCoopManager.m_BossPlayableCount));
			}
		}

		// Token: 0x060082F2 RID: 33522 RVA: 0x002C2B8C File Offset: 0x002C0D8C
		private void OnClickAttackBtn()
		{
			if (this.m_csbtnClear.m_bLock)
			{
				NKCPacketHandlers.Check_NKM_ERROR_CODE(NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_BOSS_PLAYABLE, true, null, int.MinValue);
				return;
			}
			NKCUIGuildCoopRaidRightSide.onClickAttackBtn dOnClickAttackBtn = this.m_dOnClickAttackBtn;
			if (dOnClickAttackBtn == null)
			{
				return;
			}
			dOnClickAttackBtn(0L, null, 0, 0, false);
		}

		// Token: 0x060082F3 RID: 33523 RVA: 0x002C2BC4 File Offset: 0x002C0DC4
		private void OnClickGuide()
		{
			if (this.GetRaidTemplet() == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.GetRaidTemplet().GetGuideShortCut()))
			{
				return;
			}
			NKCUIPopupTutorialImagePanel.Instance.Open(this.GetRaidTemplet().GetGuideShortCut(), null);
		}

		// Token: 0x060082F4 RID: 33524 RVA: 0x002C2BF8 File Offset: 0x002C0DF8
		private void OnClickInfoBtn()
		{
			NKCPopupGuildCoopBossInfoDetail.Instance.Open();
		}

		// Token: 0x04006F1F RID: 28447
		private NKCUIGuildCoopRaidRightSide.onClickAttackBtn m_dOnClickAttackBtn;

		// Token: 0x04006F20 RID: 28448
		[Header("기본 정보")]
		public Text m_lbLevel;

		// Token: 0x04006F21 RID: 28449
		public Text m_lbName;

		// Token: 0x04006F22 RID: 28450
		public NKCUIComStateButton m_btnGuide;

		// Token: 0x04006F23 RID: 28451
		public NKCUIComStateButton m_csbtnInfo;

		// Token: 0x04006F24 RID: 28452
		public Image m_imgBossHP;

		// Token: 0x04006F25 RID: 28453
		public Text m_lbRemainHP;

		// Token: 0x04006F26 RID: 28454
		[Header("아티팩트 세팅")]
		public NKCUIComGuildArtifactContent m_Artifact;

		// Token: 0x04006F27 RID: 28455
		[Header("남은 횟수")]
		public GameObject m_objRemainCount;

		// Token: 0x04006F28 RID: 28456
		public Text m_lbRemainCount;

		// Token: 0x04006F29 RID: 28457
		[Header("맨 아래 버튼 모음")]
		public NKCUIComStateButton m_csbtnClear;

		// Token: 0x04006F2A RID: 28458
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006F2B RID: 28459
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_COOP_RAID_RIGHT";

		// Token: 0x04006F2C RID: 28460
		private int m_RaidGroupId;

		// Token: 0x04006F2D RID: 28461
		private int m_BossStageId;

		// Token: 0x020018D8 RID: 6360
		// (Invoke) Token: 0x0600B6E7 RID: 46823
		public delegate void onClickAttackBtn(long raidUID, List<int> _buffs, int reqItemID, int reqItemCount, bool bIsTryAssist);
	}
}
