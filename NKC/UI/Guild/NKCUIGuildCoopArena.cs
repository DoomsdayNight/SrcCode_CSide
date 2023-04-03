using System;
using ClientPacket.Guild;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B37 RID: 2871
	public class NKCUIGuildCoopArena : MonoBehaviour
	{
		// Token: 0x060082BE RID: 33470 RVA: 0x002C1BA0 File Offset: 0x002BFDA0
		public void InitUI(NKCUIGuildCoopBack.OnClickArena onClickArena = null)
		{
			this.m_dOnClickArena = onClickArena;
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickArena));
		}

		// Token: 0x060082BF RID: 33471 RVA: 0x002C1BD8 File Offset: 0x002BFDD8
		public void SetData(GuildDungeonInfoTemplet templet)
		{
			this.m_GuildDungeonInfoTemplet = templet;
			if (this.m_GuildDungeonInfoTemplet == null)
			{
				this.m_btn.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_imgClearPoint, false);
				NKCUtil.SetGameobjectActive(this.m_objArtifactCount, false);
				NKCUtil.SetGameobjectActive(this.m_objFight, false);
				return;
			}
			this.m_btn.UnLock(false);
			NKCUtil.SetGameobjectActive(this.m_imgClearPoint, true);
			NKCUtil.SetGameobjectActive(this.m_objArtifactCount, true);
			NKCUtil.SetLabelText(this.m_lbArenaNum, this.m_GuildDungeonInfoTemplet.GetArenaIndex().ToString());
			this.UpdateClearPoint();
			this.UpdatePlayUser();
		}

		// Token: 0x060082C0 RID: 33472 RVA: 0x002C1C74 File Offset: 0x002BFE74
		private void UpdateClearPoint()
		{
			if (NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == this.m_GuildDungeonInfoTemplet.GetArenaIndex()) == null)
			{
				NKCUtil.SetLabelText(this.m_lbArtifactHaveCount, "0");
				this.m_imgClearPoint.fillAmount = 0f;
				return;
			}
			int currentArtifactCountByArena = NKCGuildCoopManager.GetCurrentArtifactCountByArena(this.m_GuildDungeonInfoTemplet.GetArenaIndex());
			int count = GuildDungeonTempletManager.GetDungeonArtifactList(this.m_GuildDungeonInfoTemplet.GetStageRewardArtifactGroup()).Count;
			NKCUtil.SetLabelText(this.m_lbArtifactHaveCount, currentArtifactCountByArena.ToString());
			if (currentArtifactCountByArena == count)
			{
				this.m_imgClearPoint.fillAmount = 1f;
				return;
			}
			this.m_imgClearPoint.fillAmount = NKCGuildCoopManager.GetClearPointPercentage(this.m_GuildDungeonInfoTemplet.GetArenaIndex());
		}

		// Token: 0x060082C1 RID: 33473 RVA: 0x002C1D24 File Offset: 0x002BFF24
		private void UpdatePlayUser()
		{
			GuildDungeonArena arena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == this.m_GuildDungeonInfoTemplet.GetArenaIndex());
			if (arena == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objFight, false);
				return;
			}
			if (arena.playUserUid == 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objFight, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objFight, true);
			GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.GetGuildMemberInfo().Find((GuildDungeonMemberInfo x) => x.profile.userUid == arena.playUserUid);
			NKCUtil.SetLabelText(this.m_lbFightUserName, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_SESSION_ATTACK_ING_USER_INFO, guildDungeonMemberInfo.profile.nickname));
		}

		// Token: 0x060082C2 RID: 33474 RVA: 0x002C1DCC File Offset: 0x002BFFCC
		public void OnClickArena()
		{
			if (this.m_GuildDungeonInfoTemplet == null)
			{
				return;
			}
			NKCUIGuildCoopBack.OnClickArena dOnClickArena = this.m_dOnClickArena;
			if (dOnClickArena == null)
			{
				return;
			}
			dOnClickArena(this.m_GuildDungeonInfoTemplet);
		}

		// Token: 0x060082C3 RID: 33475 RVA: 0x002C1DED File Offset: 0x002BFFED
		public void Refresh()
		{
			this.UpdatePlayUser();
			this.UpdateClearPoint();
		}

		// Token: 0x04006EF5 RID: 28405
		public NKCUIComStateButton m_btn;

		// Token: 0x04006EF6 RID: 28406
		public Text m_lbArenaNum;

		// Token: 0x04006EF7 RID: 28407
		[Header("정화도 표시")]
		public Image m_imgClearPoint;

		// Token: 0x04006EF8 RID: 28408
		[Header("보유 아티팩트 숫자")]
		public GameObject m_objArtifactCount;

		// Token: 0x04006EF9 RID: 28409
		public Text m_lbArtifactHaveCount;

		// Token: 0x04006EFA RID: 28410
		[Header("다른 유저가 전투중")]
		public GameObject m_objFight;

		// Token: 0x04006EFB RID: 28411
		public Text m_lbFightUserName;

		// Token: 0x04006EFC RID: 28412
		private GuildDungeonInfoTemplet m_GuildDungeonInfoTemplet;

		// Token: 0x04006EFD RID: 28413
		private NKCUIGuildCoopBack.OnClickArena m_dOnClickArena;
	}
}
