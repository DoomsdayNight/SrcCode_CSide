using System;
using ClientPacket.Event;
using NKM.EventPass;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000674 RID: 1652
	public class NKCEventPassDataManager
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x00109B21 File Offset: 0x00107D21
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x00109B29 File Offset: 0x00107D29
		public int EventPassId
		{
			get
			{
				return this.m_iEventPassId;
			}
			set
			{
				this.m_iEventPassId = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x00109B32 File Offset: 0x00107D32
		// (set) Token: 0x06003498 RID: 13464 RVA: 0x00109B3A File Offset: 0x00107D3A
		public int TotalExp
		{
			get
			{
				return this.m_iTotalExp;
			}
			set
			{
				this.m_iTotalExp = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x00109B43 File Offset: 0x00107D43
		// (set) Token: 0x0600349A RID: 13466 RVA: 0x00109B4B File Offset: 0x00107D4B
		public int NormalRewardLevel
		{
			get
			{
				return this.m_iNormalRewardLevel;
			}
			set
			{
				this.m_iNormalRewardLevel = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x00109B54 File Offset: 0x00107D54
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x00109B5C File Offset: 0x00107D5C
		public int CoreRewardLevel
		{
			get
			{
				return this.m_iCoreRewardLevel;
			}
			set
			{
				this.m_iCoreRewardLevel = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x00109B65 File Offset: 0x00107D65
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x00109B6D File Offset: 0x00107D6D
		public bool CorePassPurchased
		{
			get
			{
				return this.m_bCorePassPurChased;
			}
			set
			{
				this.m_bCorePassPurChased = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x00109B76 File Offset: 0x00107D76
		// (set) Token: 0x060034A0 RID: 13472 RVA: 0x00109B7E File Offset: 0x00107D7E
		public bool EventPassDataReceived
		{
			get
			{
				return this.m_bEventPassDataReceived;
			}
			set
			{
				this.m_bEventPassDataReceived = value;
			}
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x00109B87 File Offset: 0x00107D87
		public NKCEventPassDataManager()
		{
			this.m_iEventPassId = 0;
			this.m_bEventPassDataReceived = false;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x00109B9D File Offset: 0x00107D9D
		public void SetEventPassData(NKMPacket_EVENT_PASS_ACK eventPassAck)
		{
			this.m_iTotalExp = eventPassAck.totalExp;
			this.m_iNormalRewardLevel = eventPassAck.rewardNormalLevel;
			this.m_iCoreRewardLevel = eventPassAck.rewardCoreLevel;
			this.m_bCorePassPurChased = eventPassAck.isCorePassPurchased;
			this.m_bEventPassDataReceived = true;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x00109BD8 File Offset: 0x00107DD8
		public int GetPassLevel()
		{
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
			if (!this.m_bEventPassDataReceived || nkmeventPassTemplet == null)
			{
				return 0;
			}
			return Mathf.Min(nkmeventPassTemplet.PassMaxExp, this.m_iTotalExp / nkmeventPassTemplet.PassLevelUpExp + 1);
		}

		// Token: 0x040032D7 RID: 13015
		private int m_iEventPassId;

		// Token: 0x040032D8 RID: 13016
		private int m_iTotalExp;

		// Token: 0x040032D9 RID: 13017
		private int m_iNormalRewardLevel;

		// Token: 0x040032DA RID: 13018
		private int m_iCoreRewardLevel;

		// Token: 0x040032DB RID: 13019
		private bool m_bCorePassPurChased;

		// Token: 0x040032DC RID: 13020
		private bool m_bEventPassDataReceived;
	}
}
