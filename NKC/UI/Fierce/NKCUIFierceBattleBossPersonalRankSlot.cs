using System;
using ClientPacket.LeaderBoard;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB3 RID: 2995
	public class NKCUIFierceBattleBossPersonalRankSlot : MonoBehaviour
	{
		// Token: 0x06008A76 RID: 35446 RVA: 0x002F1728 File Offset: 0x002EF928
		public static NKCUIFierceBattleBossPersonalRankSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_BOSS_PERSONAL_RANK_SLOT", false, null);
			NKCUIFierceBattleBossPersonalRankSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIFierceBattleBossPersonalRankSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIFierceBattleBossPersonalRankSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.Init();
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			return component;
		}

		// Token: 0x06008A77 RID: 35447 RVA: 0x002F17DF File Offset: 0x002EF9DF
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008A78 RID: 35448 RVA: 0x002F17FE File Offset: 0x002EF9FE
		public void Init()
		{
			if (this.m_userProfile != null)
			{
				this.m_userProfile.Init();
			}
		}

		// Token: 0x06008A79 RID: 35449 RVA: 0x002F181C File Offset: 0x002EFA1C
		public void SetData(NKMFierceData fierceData, int Rank)
		{
			NKCUtil.SetLabelText(this.m_Ranking, "#" + Rank.ToString());
			NKCUtil.SetLabelText(this.m_UserName, fierceData.commonProfile.nickname.ToString());
			NKCUtil.SetLabelText(this.m_Score, fierceData.fiercePoint.ToString());
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeUnitData(fierceData.commonProfile.mainUnitId, fierceData.commonProfile.level, fierceData.commonProfile.mainUnitSkinId, 0);
			this.m_userProfile.SetData(data, true, new NKCUISlot.OnClick(this.OnSlotClick));
			this.m_UserUID = fierceData.commonProfile.userUid;
		}

		// Token: 0x06008A7A RID: 35450 RVA: 0x002F18C8 File Offset: 0x002EFAC8
		private void OnSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (NKCPopupFierceUserInfo.IsHasInstance() && NKCPopupFierceUserInfo.Instance.IsSameProfile(this.m_UserUID))
			{
				NKCPopupFierceUserInfo.Instance.Open(null);
				return;
			}
			NKCPacketSender.Send_NKMPacket_FIERCE_PROFILE_REQ(this.m_UserUID, true);
		}

		// Token: 0x0400773D RID: 30525
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x0400773E RID: 30526
		public NKCUISlot m_userProfile;

		// Token: 0x0400773F RID: 30527
		public Text m_Ranking;

		// Token: 0x04007740 RID: 30528
		public Text m_UserName;

		// Token: 0x04007741 RID: 30529
		public Text m_Score;

		// Token: 0x04007742 RID: 30530
		private int m_bossGroupID;

		// Token: 0x04007743 RID: 30531
		private long m_UserUID;
	}
}
