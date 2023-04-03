using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Core.Util;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B70 RID: 2928
	public class NKCUIGauntletLeagueMatch : NKCUIBase
	{
		// Token: 0x0600860C RID: 34316 RVA: 0x002D639A File Offset: 0x002D459A
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			return NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletLeagueMatch>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LEAGUE_MATCH", NKCUIManager.eUIBaseRect.UIFrontCommon, null);
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x0600860D RID: 34317 RVA: 0x002D63AD File Offset: 0x002D45AD
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x0600860E RID: 34318 RVA: 0x002D63B0 File Offset: 0x002D45B0
		public override bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x0600860F RID: 34319 RVA: 0x002D63B3 File Offset: 0x002D45B3
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06008610 RID: 34320 RVA: 0x002D63B6 File Offset: 0x002D45B6
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06008611 RID: 34321 RVA: 0x002D63BD File Offset: 0x002D45BD
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					101
				};
			}
		}

		// Token: 0x06008612 RID: 34322 RVA: 0x002D63CC File Offset: 0x002D45CC
		public override void CloseInternal()
		{
			if (this.m_CharacterViewLeft != null)
			{
				this.m_CharacterViewLeft.CleanUp();
				this.m_CharacterViewLeft = null;
			}
			if (this.m_CharacterViewRight != null)
			{
				this.m_CharacterViewRight.CleanUp();
				this.m_CharacterViewRight = null;
			}
		}

		// Token: 0x06008613 RID: 34323 RVA: 0x002D641C File Offset: 0x002D461C
		public void Init()
		{
			this.m_prevRemainingSeconds = 0;
			NKCUtil.SetGameobjectActive(this, false);
			if (this.m_CharacterViewLeft != null)
			{
				this.m_CharacterViewLeft.Init(null, delegate(PointerEventData cPointerEventData, NKCUICharacterView charView)
				{
				});
			}
			if (this.m_CharacterViewRight != null)
			{
				this.m_CharacterViewRight.Init(null, delegate(PointerEventData cPointerEventData, NKCUICharacterView charView)
				{
				});
			}
			this.m_UserLeft.Init(null, null);
			this.m_UserRight.Init(null, null);
		}

		// Token: 0x06008614 RID: 34324 RVA: 0x002D64C4 File Offset: 0x002D46C4
		public void Open(DateTime endTime)
		{
			NKCUtil.SetGameobjectActive(this, true);
			this.RefreshDraftData(endTime);
			base.UIOpened(true);
			if (this.m_animatorStart != null)
			{
				this.m_animatorStart.Play("GAUNTLET_LEAGUE_MATCH");
			}
			NKCSoundManager.PlaySound("FX_UI_PVP_MATCH_OK", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x06008615 RID: 34325 RVA: 0x002D652C File Offset: 0x002D472C
		private void Update()
		{
			if (this.m_prevRemainingSeconds > 0)
			{
				int num = Math.Max(0, Convert.ToInt32((this.m_endTime - ServiceTime.Now).TotalSeconds));
				if (num != this.m_prevRemainingSeconds)
				{
					this.m_prevRemainingSeconds = num;
				}
			}
		}

		// Token: 0x06008616 RID: 34326 RVA: 0x002D6576 File Offset: 0x002D4776
		public void RefreshDraftData(DateTime endTime)
		{
			this.SetEndTime(endTime);
			this.UpdateUserInfo();
		}

		// Token: 0x06008617 RID: 34327 RVA: 0x002D6588 File Offset: 0x002D4788
		public void SetEndTime(DateTime endTime)
		{
			this.m_endTime = endTime;
			this.m_prevRemainingSeconds = Math.Max(0, Convert.ToInt32((this.m_endTime - ServiceTime.Now).TotalSeconds));
		}

		// Token: 0x06008618 RID: 34328 RVA: 0x002D65C8 File Offset: 0x002D47C8
		public void UpdateUserInfo()
		{
			this.m_UserLeft.SetData(NKCLeaguePVPMgr.GetLeftDraftTeamData(), true, false, false, -1, -1, NKCLeaguePVPMgr.IsPrivate());
			this.m_UserRight.SetData(NKCLeaguePVPMgr.GetRightDraftTeamData(), false, false, false, -1, -1, NKCLeaguePVPMgr.IsPrivate());
			this.UpdateCharacterView(this.m_CharacterViewLeft, NKCLeaguePVPMgr.GetTeamLeaderUnit(true));
			this.UpdateCharacterView(this.m_CharacterViewRight, NKCLeaguePVPMgr.GetTeamLeaderUnit(false));
		}

		// Token: 0x06008619 RID: 34329 RVA: 0x002D6630 File Offset: 0x002D4830
		private void UpdateCharacterView(NKCUICharacterView characterView, NKMAsyncUnitData leaderUnitData)
		{
			if (leaderUnitData == null || characterView == null)
			{
				return;
			}
			if (leaderUnitData.skinId != 0)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(leaderUnitData.skinId);
				characterView.SetCharacterIllust(skinTemplet, false, false, 0);
				return;
			}
			if (leaderUnitData.unitId != 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(leaderUnitData.unitId);
				characterView.SetCharacterIllust(unitTempletBase, 0, false, false, 0);
			}
		}

		// Token: 0x040072AE RID: 29358
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040072AF RID: 29359
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_LEAGUE_MATCH";

		// Token: 0x040072B0 RID: 29360
		[SerializeField]
		[Header("유저 정보")]
		public NKCUIGauntletLeagueMain.LeagueUserInfoUI m_UserLeft;

		// Token: 0x040072B1 RID: 29361
		public NKCUIGauntletLeagueMain.LeagueUserInfoUI m_UserRight;

		// Token: 0x040072B2 RID: 29362
		[Header("애니메이터")]
		public Animator m_animatorStart;

		// Token: 0x040072B3 RID: 29363
		[Header("유닛 일러스트")]
		public NKCUICharacterView m_CharacterViewLeft;

		// Token: 0x040072B4 RID: 29364
		public NKCUICharacterView m_CharacterViewRight;

		// Token: 0x040072B5 RID: 29365
		private DateTime m_endTime;

		// Token: 0x040072B6 RID: 29366
		private int m_prevRemainingSeconds;
	}
}
