using System;
using ClientPacket.Common;
using ClientPacket.Guild;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Guild
{
	// Token: 0x02000B5A RID: 2906
	public class NKCUIGuildMemberSlot : MonoBehaviour
	{
		// Token: 0x06008489 RID: 33929 RVA: 0x002CB4F8 File Offset: 0x002C96F8
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
		}

		// Token: 0x0600848A RID: 33930 RVA: 0x002CB508 File Offset: 0x002C9708
		public static NKCUIGuildMemberSlot GetNewInstance(Transform parent, NKCUIGuildMemberSlot.OnSelectedSlot selectedSlot)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_MEMBER_SLOT", false, null);
			NKCUIGuildMemberSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGuildMemberSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIGuildMemberSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			component.SetOnSelectedSlot(selectedSlot);
			component.InitUI();
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600848B RID: 33931 RVA: 0x002CB584 File Offset: 0x002C9784
		private void InitUI()
		{
			this.m_slotNormal.InitUI();
			this.m_slotRequest.InitUI();
			this.m_btnSlot.PointerClick.RemoveAllListeners();
			this.m_btnSlot.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
		}

		// Token: 0x0600848C RID: 33932 RVA: 0x002CB5D3 File Offset: 0x002C97D3
		private void SetOnSelectedSlot(NKCUIGuildMemberSlot.OnSelectedSlot onSelectedSlot)
		{
			this.m_dOnSelectedSlot = onSelectedSlot;
		}

		// Token: 0x0600848D RID: 33933 RVA: 0x002CB5DC File Offset: 0x002C97DC
		public void SetData(NKMGuildMemberData guildMemberData, bool bIsMyGuild)
		{
			this.m_Useruid = guildMemberData.commonProfile.userUid;
			NKCUtil.SetGameobjectActive(this.m_slotNormal, true);
			NKCUtil.SetGameobjectActive(this.m_slotRequest, false);
			this.m_slotNormal.SetData(guildMemberData, bIsMyGuild);
		}

		// Token: 0x0600848E RID: 33934 RVA: 0x002CB614 File Offset: 0x002C9814
		public void SetData(FriendListData userData, NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE lobbyUIType)
		{
			this.m_Useruid = userData.commonProfile.userUid;
			NKCUtil.SetGameobjectActive(this.m_slotNormal, false);
			NKCUtil.SetGameobjectActive(this.m_slotRequest, true);
			this.m_slotRequest.SetData(userData, lobbyUIType);
		}

		// Token: 0x0600848F RID: 33935 RVA: 0x002CB64C File Offset: 0x002C984C
		private void OnClickSlot()
		{
			NKCUIGuildMemberSlot.OnSelectedSlot dOnSelectedSlot = this.m_dOnSelectedSlot;
			if (dOnSelectedSlot == null)
			{
				return;
			}
			dOnSelectedSlot(this.m_Useruid);
		}

		// Token: 0x040070BB RID: 28859
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x040070BC RID: 28860
		public NKCUIGuildMemberSlotNormal m_slotNormal;

		// Token: 0x040070BD RID: 28861
		public NKCUIGuildMemberSlotRequest m_slotRequest;

		// Token: 0x040070BE RID: 28862
		private NKCUIGuildMemberSlot.OnSelectedSlot m_dOnSelectedSlot;

		// Token: 0x040070BF RID: 28863
		private NKCAssetInstanceData m_instance;

		// Token: 0x040070C0 RID: 28864
		private long m_Useruid;

		// Token: 0x020018F5 RID: 6389
		// (Invoke) Token: 0x0600B744 RID: 46916
		public delegate void OnSelectedSlot(long userUid);
	}
}
