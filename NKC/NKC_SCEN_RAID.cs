using System;
using ClientPacket.Raid;
using NKC.UI;
using NKM;

namespace NKC
{
	// Token: 0x02000727 RID: 1831
	public class NKC_SCEN_RAID : NKC_SCEN_BASIC
	{
		// Token: 0x060048E6 RID: 18662 RVA: 0x0015FA4F File Offset: 0x0015DC4F
		public NKC_SCEN_RAID()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_RAID;
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x0015FA5F File Offset: 0x0015DC5F
		public void ResetUI()
		{
			if (this.m_NKCUIRaid != null)
			{
				this.m_NKCUIRaid.SetUI();
			}
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x0015FA7B File Offset: 0x0015DC7B
		public void SetRaidUID(long raidUID)
		{
			this.m_RaidUID = raidUID;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x0015FA84 File Offset: 0x0015DC84
		public void ClearCacheData()
		{
			if (this.m_NKCUIRaid != null)
			{
				this.m_NKCUIRaid.CloseInstance();
				this.m_NKCUIRaid = null;
			}
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x0015FAA6 File Offset: 0x0015DCA6
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.m_NKCUIRaid == null)
			{
				this.m_UILoadResourceData = NKCUIRaid.OpenInstanceAsync();
				return;
			}
			this.m_UILoadResourceData = null;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x0015FAD0 File Offset: 0x0015DCD0
		public override void ScenLoadUpdate()
		{
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			if (this.m_NKCUIRaid == null && this.m_UILoadResourceData != null)
			{
				if (!NKCUIRaid.CheckInstanceLoaded(this.m_UILoadResourceData, out this.m_NKCUIRaid))
				{
					return;
				}
				this.m_UILoadResourceData = null;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x0015FB1C File Offset: 0x0015DD1C
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_NKCUIRaid != null)
			{
				this.m_NKCUIRaid.InitUI();
			}
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x0015FB40 File Offset: 0x0015DD40
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (nkmraidDetailData != null && NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
			{
				if (this.m_NKCUIRaid != null)
				{
					this.m_NKCUIRaid.Open(this.m_RaidUID);
					return;
				}
			}
			else
			{
				NKCPacketSender.Send_NKMPacket_RAID_DETAIL_INFO_REQ(this.m_RaidUID);
			}
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x0015FBAA File Offset: 0x0015DDAA
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIRaid != null)
			{
				this.m_NKCUIRaid.Close();
			}
			this.ClearCacheData();
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x0015FBD1 File Offset: 0x0015DDD1
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x0015FBD9 File Offset: 0x0015DDD9
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0015FBDC File Offset: 0x0015DDDC
		public void OnRecv(NKMPacket_RAID_DETAIL_INFO_ACK cNKMPacket_RAID_DETAIL_INFO_ACK)
		{
			this.m_RaidUID = cNKMPacket_RAID_DETAIL_INFO_ACK.raidDetailData.raidUID;
			if (this.m_NKCUIRaid != null)
			{
				this.m_NKCUIRaid.Open(this.m_RaidUID);
			}
		}

		// Token: 0x0400386F RID: 14447
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x04003870 RID: 14448
		private NKCUIRaid m_NKCUIRaid;

		// Token: 0x04003871 RID: 14449
		private long m_RaidUID;
	}
}
