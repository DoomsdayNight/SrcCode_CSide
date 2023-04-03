using System;
using ClientPacket.Guild;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B39 RID: 2873
	public class NKCUIGuildCoopBoss : MonoBehaviour
	{
		// Token: 0x060082D0 RID: 33488 RVA: 0x002C22E4 File Offset: 0x002C04E4
		public void InitUI(NKCUIGuildCoopBack.OnClickBoss onClickBoss = null)
		{
			this.m_dOnClickBoss = onClickBoss;
			if (this.m_btn != null)
			{
				this.m_btn.PointerClick.RemoveAllListeners();
				this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickBoss));
			}
		}

		// Token: 0x060082D1 RID: 33489 RVA: 0x002C2332 File Offset: 0x002C0532
		public void OnDestroy()
		{
			this.CleanUpSpineSD();
		}

		// Token: 0x060082D2 RID: 33490 RVA: 0x002C233C File Offset: 0x002C053C
		public void SetData(GuildRaidTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_cGuildRaidTemplet = templet;
			this.m_BossStageId = templet.GetStageId();
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_BossStageId);
			if (dungeonTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetLabelText(this.m_lbBossName, dungeonTempletBase.GetDungeonName());
			this.UpdateBossState();
			this.UpdateBossHP();
			this.UpdateBossPlayUser();
		}

		// Token: 0x060082D3 RID: 33491 RVA: 0x002C23B7 File Offset: 0x002C05B7
		public void OnClickBoss()
		{
			if (this.m_BossStageId == 0)
			{
				return;
			}
			NKCUIGuildCoopBack.OnClickBoss dOnClickBoss = this.m_dOnClickBoss;
			if (dOnClickBoss == null)
			{
				return;
			}
			dOnClickBoss(this.m_BossStageId);
		}

		// Token: 0x060082D4 RID: 33492 RVA: 0x002C23D8 File Offset: 0x002C05D8
		public void PlaySDAnim(NKCASUIUnitIllust.eAnimation eAnim, bool bLoop = false)
		{
			if (this.m_spineSD == null)
			{
				this.OpenSDIllust(eAnim, bLoop);
				return;
			}
			if (this.m_spineSD != null)
			{
				this.m_spineSD.SetAnimation(eAnim, bLoop, 0, true, 0f, true);
			}
		}

		// Token: 0x060082D5 RID: 33493 RVA: 0x002C2409 File Offset: 0x002C0609
		public void CleanUpSpineSD()
		{
			if (this.m_spineSD != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
				this.m_spineSD = null;
			}
		}

		// Token: 0x060082D6 RID: 33494 RVA: 0x002C2430 File Offset: 0x002C0630
		private bool OpenSDIllust(NKCASUIUnitIllust.eAnimation eStartAnim = NKCASUIUnitIllust.eAnimation.NONE, bool bLoopStartAnim = false)
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(this.m_cGuildRaidTemplet.GetRaidBossSDName(), false);
			if (this.m_spineSD != null && (this.m_spineSD.m_SpineIllustInstant == null || this.m_spineSD.m_SpineIllustInstant_SkeletonGraphic == null))
			{
				this.m_spineSD = null;
			}
			if (this.m_spineSD != null)
			{
				if (eStartAnim == NKCASUIUnitIllust.eAnimation.NONE)
				{
					this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				}
				else
				{
					this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, false, false, 0f);
					this.m_spineSD.SetAnimation(eStartAnim, bLoopStartAnim, 0, true, 0f, true);
				}
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector2.zero;
					rectTransform.localScale = Vector3.one;
					rectTransform.localRotation = Quaternion.identity;
				}
				return true;
			}
			Debug.Log("spine SD data not found from m_cGuildRaidTemplet.GetRaidBossSDName() : " + this.m_cGuildRaidTemplet.GetRaidBossSDName());
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
			return false;
		}

		// Token: 0x060082D7 RID: 33495 RVA: 0x002C2570 File Offset: 0x002C0770
		private void UpdateBossHP()
		{
			float num = NKCGuildCoopManager.m_BossRemainHp / NKCGuildCoopManager.m_BossMaxHp;
			NKCUtil.SetLabelText(this.m_lbBossHP, string.Format("{0} ({1:0.##}%)", NKCGuildCoopManager.m_BossRemainHp.ToString("N0"), num * 100f));
			this.m_imgBossHP.fillAmount = num;
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x002C25C8 File Offset: 0x002C07C8
		private void UpdateBossPlayUser()
		{
			if (NKCGuildCoopManager.m_BossPlayUserUid == 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objFight, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objFight, true);
			GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.GetGuildMemberInfo().Find((GuildDungeonMemberInfo x) => x.profile.userUid == NKCGuildCoopManager.m_BossPlayUserUid);
			NKCUtil.SetLabelText(this.m_lbFightUserName, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_SESSION_ATTACK_ING_USER_INFO, guildDungeonMemberInfo.profile.nickname));
		}

		// Token: 0x060082D9 RID: 33497 RVA: 0x002C263F File Offset: 0x002C083F
		public void UpdateBossState()
		{
			if (NKCGuildCoopManager.m_BossRemainHp <= 0f)
			{
				this.PlaySDAnim(NKCASUIUnitIllust.eAnimation.SD_DOWN, true);
				return;
			}
			this.PlaySDAnim(NKCASUIUnitIllust.eAnimation.SD_IDLE, true);
		}

		// Token: 0x060082DA RID: 33498 RVA: 0x002C2666 File Offset: 0x002C0866
		public void Refresh()
		{
			this.UpdateBossState();
			this.UpdateBossHP();
			this.UpdateBossPlayUser();
		}

		// Token: 0x04006F09 RID: 28425
		public NKCUIComStateButton m_btn;

		// Token: 0x04006F0A RID: 28426
		public Text m_lbBossName;

		// Token: 0x04006F0B RID: 28427
		[Header("보스 체력 게이지")]
		public Transform m_rtSDRoot;

		// Token: 0x04006F0C RID: 28428
		public Image m_imgBossHP;

		// Token: 0x04006F0D RID: 28429
		public Text m_lbBossHP;

		// Token: 0x04006F0E RID: 28430
		[Header("다른 유저가 전투중")]
		public GameObject m_objFight;

		// Token: 0x04006F0F RID: 28431
		public Text m_lbFightUserName;

		// Token: 0x04006F10 RID: 28432
		private NKCUIGuildCoopBack.OnClickBoss m_dOnClickBoss;

		// Token: 0x04006F11 RID: 28433
		private int m_BossStageId;

		// Token: 0x04006F12 RID: 28434
		private GuildRaidTemplet m_cGuildRaidTemplet;

		// Token: 0x04006F13 RID: 28435
		private NKCASUISpineIllust m_spineSD;
	}
}
