using System;
using ClientPacket.Community;
using ClientPacket.User;
using NKC.UI;
using NKC.UI.Collection;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006FE RID: 1790
	public class NKC_SCEN_COLLECTION : NKC_SCEN_BASIC
	{
		// Token: 0x06004639 RID: 17977 RVA: 0x0015553F File Offset: 0x0015373F
		public NKC_SCEN_COLLECTION()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_COLLECTION;
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x00155561 File Offset: 0x00153761
		public void SetOpenReserve(NKCUICollection.CollectionType reciveType, string targetUnitStrID)
		{
			this.m_eReserveState = reciveType;
			this.m_ReserveUnitStrID = targetUnitStrID;
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x00155574 File Offset: 0x00153774
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			NKCCollectionManager.Init();
			if (!NKCUIManager.IsValid(this.m_UICollectionLoadData))
			{
				this.m_NKCUICollection = null;
				this.m_UICollectionLoadData = NKCUIManager.OpenNewInstanceAsync<NKCUICollection>("ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null);
			}
			NKCUnitMissionManager.Init();
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x001555C1 File Offset: 0x001537C1
		public NKM_SHORTCUT_TYPE GetCurrentShortcutType()
		{
			if (this.m_NKCUICollection != null && this.m_NKCUICollection.IsOpen)
			{
				return this.m_NKCUICollection.GetShortcutType();
			}
			return NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION;
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x001555EC File Offset: 0x001537EC
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_NKCUICollection == null)
			{
				if (this.m_UICollectionLoadData != null)
				{
					if (!this.m_UICollectionLoadData.IsLoadComplete)
					{
						Debug.LogError("Error - NKC_SCEN_COLLECTION.ScenLoadComplete() : UICollectionLoadResourceData.IsDone() is false");
						return;
					}
					if (this.m_UICollectionLoadData.CheckLoadAndGetInstance<NKCUICollection>(out this.m_NKCUICollection))
					{
						this.m_NKCUICollection.Init();
						return;
					}
				}
				return;
			}
			Debug.LogError("Error - NKC_SCEN_COLLECTION.ScenLoadComplete() : UICollectionLoadResourceData is null");
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x00155657 File Offset: 0x00153857
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUICollection.Open(this.m_eReserveState, this.m_ReserveUnitStrID);
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x00155678 File Offset: 0x00153878
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUICollection != null)
			{
				this.m_NKCUICollection.Close();
			}
			NKCUIManager.LoadedUIData uicollectionLoadData = this.m_UICollectionLoadData;
			if (uicollectionLoadData != null)
			{
				uicollectionLoadData.CloseInstance();
			}
			this.m_UICollectionLoadData = null;
			this.m_NKCUICollection = null;
			if (NKCUICutScenPlayer.HasInstance)
			{
				NKCUICutScenPlayer.Instance.StopWithInvalidatingCallBack();
				NKCUICutScenPlayer.Instance.UnLoad();
			}
			this.m_eReserveState = NKCUICollection.CollectionType.CT_NONE;
			this.m_ReserveUnitStrID = "";
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x001556F0 File Offset: 0x001538F0
		public void OnRecvReviewTagVoteCancelAck(NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_ACK sPacket)
		{
			if (this.m_NKCUICollection != null && this.m_NKCUICollection.IsOpen)
			{
				this.m_NKCUICollection.OnRecvReviewTagVoteCancelAck(sPacket);
			}
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x00155719 File Offset: 0x00153919
		public void OnRecvReviewTagVoteAck(NKMPacket_UNIT_REVIEW_TAG_VOTE_ACK sPacket)
		{
			if (this.m_NKCUICollection != null && this.m_NKCUICollection.IsOpen)
			{
				this.m_NKCUICollection.OnRecvReviewTagVoteAck(sPacket);
			}
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x00155742 File Offset: 0x00153942
		public void OnRecvReviewTagListAck(NKMPacket_UNIT_REVIEW_TAG_LIST_ACK sPacket)
		{
			if (this.m_NKCUICollection != null && this.m_NKCUICollection.IsOpen)
			{
				this.m_NKCUICollection.OnRecvReviewTagListAck(sPacket);
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x0015576B File Offset: 0x0015396B
		public void OnRecvTeamCollectionRewardAck(NKMPacket_TEAM_COLLECTION_REWARD_ACK sPacket)
		{
			if (this.m_NKCUICollection != null && this.m_NKCUICollection.IsOpen)
			{
				this.m_NKCUICollection.OnRecvTeamCollectionRewardAck(sPacket);
			}
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x00155794 File Offset: 0x00153994
		public void OnRecvUnitMissionReward(int unitId)
		{
			if (this.m_NKCUICollection != null)
			{
				this.m_NKCUICollection.OnRecvUnitMissionReward(unitId);
			}
		}

		// Token: 0x04003769 RID: 14185
		private NKCUICollection m_NKCUICollection;

		// Token: 0x0400376A RID: 14186
		private NKCUIManager.LoadedUIData m_UICollectionLoadData;

		// Token: 0x0400376B RID: 14187
		private NKCUICollection.CollectionType m_eReserveState = NKCUICollection.CollectionType.CT_NONE;

		// Token: 0x0400376C RID: 14188
		private string m_ReserveUnitStrID = "";
	}
}
