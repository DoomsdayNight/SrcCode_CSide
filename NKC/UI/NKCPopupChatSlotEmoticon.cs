using System;
using ClientPacket.Common;
using ClientPacket.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200091D RID: 2333
	public class NKCPopupChatSlotEmoticon : MonoBehaviour
	{
		// Token: 0x06005D68 RID: 23912 RVA: 0x001CCE80 File Offset: 0x001CB080
		public void SetData(long channelUid, NKMChatMessageData data)
		{
			this.m_slotEmoticon.SetClickEvent(new NKCUISlot.OnClick(this.OnClickEmoticon));
			this.m_slotEmoticon.SetClickEventForChange(null);
			this.m_slotEmoticon.SetSelected(false);
			this.m_slotEmoticon.SetSelectedWithChangeButton(false);
			this.m_slotEmoticon.SetUI(data.emotionId);
			NKMGuildData myGuildData = NKCGuildManager.MyGuildData;
			NKMGuildMemberData nkmguildMemberData = (myGuildData != null) ? myGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == data.commonProfile.userUid) : null;
			if (NKCGuildManager.HasGuild() && NKCGuildManager.MyData.guildUid == channelUid && nkmguildMemberData != null)
			{
				switch (nkmguildMemberData.grade)
				{
				case GuildMemberGrade.Master:
					NKCUtil.SetGameobjectActive(this.m_imgLeader, true);
					NKCUtil.SetImageSprite(this.m_imgLeader, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_consortium_sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_LEADER", false), false);
					break;
				case GuildMemberGrade.Staff:
					NKCUtil.SetGameobjectActive(this.m_imgLeader, true);
					NKCUtil.SetImageSprite(this.m_imgLeader, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_consortium_sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_OFFICER", false), false);
					break;
				case GuildMemberGrade.Member:
					NKCUtil.SetGameobjectActive(this.m_imgLeader, false);
					break;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgLeader, false);
			}
			NKCUtil.SetLabelText(this.m_lbName, data.commonProfile.nickname);
			this.m_slot.SetProfiledata(data.commonProfile, null);
			DateTime systemLocalTime = NKCSynchronizedTime.GetSystemLocalTime(data.createdAt, NKMTime.INTERVAL_FROM_UTC);
			NKCUtil.SetLabelText(this.m_lbTime, systemLocalTime.ToString());
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001CD00E File Offset: 0x001CB20E
		public void PlaySDAni()
		{
			this.m_slotEmoticon.PlaySDAni();
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x001CD01B File Offset: 0x001CB21B
		private void OnClickEmoticon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.m_slotEmoticon.PlaySDAni();
		}

		// Token: 0x04004993 RID: 18835
		public Text m_lbName;

		// Token: 0x04004994 RID: 18836
		public NKCUISlotProfile m_slot;

		// Token: 0x04004995 RID: 18837
		public Image m_imgLeader;

		// Token: 0x04004996 RID: 18838
		public Text m_lbTime;

		// Token: 0x04004997 RID: 18839
		public NKCPopupEmoticonSlotSD m_slotEmoticon;
	}
}
