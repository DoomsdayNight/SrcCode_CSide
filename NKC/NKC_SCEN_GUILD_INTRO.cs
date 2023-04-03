using System;
using NKC.UI.Guild;
using NKM;

namespace NKC
{
	// Token: 0x0200071C RID: 1820
	public class NKC_SCEN_GUILD_INTRO : NKC_SCEN_BASIC
	{
		// Token: 0x06004816 RID: 18454 RVA: 0x0015C7E3 File Offset: 0x0015A9E3
		public NKC_SCEN_GUILD_INTRO()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GUILD_INTRO;
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0015C7F3 File Offset: 0x0015A9F3
		public void ClearCacheData()
		{
			if (this.m_NKCUIGuildIntro != null)
			{
				this.m_NKCUIGuildIntro.CloseInstance();
				this.m_NKCUIGuildIntro = null;
			}
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0015C815 File Offset: 0x0015AA15
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.m_NKCUIGuildIntro == null)
			{
				this.m_UILoadResourceData = NKCUIGuildIntro.OpenInstanceAsync();
				return;
			}
			this.m_UILoadResourceData = null;
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0015C840 File Offset: 0x0015AA40
		public override void ScenLoadUpdate()
		{
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			if (this.m_NKCUIGuildIntro == null && this.m_UILoadResourceData != null)
			{
				if (!NKCUIGuildIntro.CheckInstanceLoaded(this.m_UILoadResourceData, out this.m_NKCUIGuildIntro))
				{
					return;
				}
				this.m_UILoadResourceData = null;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0015C88C File Offset: 0x0015AA8C
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_NKCUIGuildIntro != null)
			{
				this.m_NKCUIGuildIntro.InitUI();
			}
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0015C8AD File Offset: 0x0015AAAD
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			if (this.m_NKCUIGuildIntro != null)
			{
				if (NKCScenManager.CurrentUserData().UserProfileData == null)
				{
					NKCPacketSender.Send_NKMPacket_MY_USER_PROFILE_INFO_REQ();
				}
				this.m_NKCUIGuildIntro.Open();
				this.TutorialCheck();
			}
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0015C8EB File Offset: 0x0015AAEB
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIGuildIntro != null)
			{
				this.m_NKCUIGuildIntro.Close();
			}
			this.ClearCacheData();
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0015C912 File Offset: 0x0015AB12
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0015C91A File Offset: 0x0015AB1A
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0015C91D File Offset: 0x0015AB1D
		public void TutorialCheck()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_INTRO)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.ConsortiumMain, true);
			}
		}

		// Token: 0x04003817 RID: 14359
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x04003818 RID: 14360
		private NKCUIGuildIntro m_NKCUIGuildIntro;
	}
}
