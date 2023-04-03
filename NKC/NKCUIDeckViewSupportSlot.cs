using System;
using ClientPacket.Community;
using ClientPacket.Guild;
using NKC.UI;
using NKC.UI.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007BC RID: 1980
	public class NKCUIDeckViewSupportSlot : MonoBehaviour
	{
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x0017A525 File Offset: 0x00178725
		// (set) Token: 0x06004E76 RID: 20086 RVA: 0x0017A52D File Offset: 0x0017872D
		public long SuppoterCode { get; private set; }

		// Token: 0x06004E77 RID: 20087 RVA: 0x0017A536 File Offset: 0x00178736
		public void Init()
		{
			this.m_slot.Init();
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnSelectButton));
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x0017A570 File Offset: 0x00178770
		public void SetData(WarfareSupporterListData data, bool bGuest, NKCUIDeckViewSupportSlot.OnSelect onSelect)
		{
			this.SuppoterCode = data.commonProfile.friendCode;
			this.dOnSelect = onSelect;
			this.m_slot.SetProfiledata(data.commonProfile, delegate(NKCUISlotProfile slot, int frame)
			{
				this.OnSelectButton();
			});
			NKCUtil.SetLabelText(this.m_txtLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				data.commonProfile.level
			});
			NKCUtil.SetLabelText(this.m_txtName, data.commonProfile.nickname);
			NKCUtil.SetLabelText(this.m_txtUID, NKCUtilString.GetFriendCode(data.commonProfile.friendCode));
			NKCUtil.SetLabelText(this.m_txtLastConnectTime, NKCUtilString.GetLastTimeString(data.lastLoginDate));
			NKCUtil.SetLabelText(this.m_txtPower, data.deckData.CalculateOperationPower().ToString());
			this.SetGuildData(data);
			this.m_lastUsedTime = data.lastUsedDate;
			this.SetCooltime();
			NKCUtil.SetGameobjectActive(this.m_objGuest, bGuest);
			if (bGuest && NKCGuildManager.HasGuild() && NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == data.commonProfile.userUid) != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuest, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objGuildLabel, false);
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0017A6E8 File Offset: 0x001788E8
		private void SetGuildData(WarfareSupporterListData data)
		{
			if (this.m_objGuild != null)
			{
				if (data.guildData != null)
				{
					NKCUtil.SetGameobjectActive(this.m_objGuild, data.guildData.guildUid > 0L);
					if (this.m_objGuild.activeSelf)
					{
						this.m_GuildBadgeUI.SetData(data.guildData.badgeId);
						NKCUtil.SetLabelText(this.m_lbGuildName, data.guildData.guildName);
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objGuild, false);
				}
			}
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0017A76B File Offset: 0x0017896B
		public bool IsCooltime()
		{
			return this.m_objCoolTime.activeSelf;
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0017A778 File Offset: 0x00178978
		public void SelectUI(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelect, bSet);
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x0017A786 File Offset: 0x00178986
		private void OnSelectButton()
		{
			NKCUIDeckViewSupportSlot.OnSelect onSelect = this.dOnSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect(this.SuppoterCode);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x0017A7A0 File Offset: 0x001789A0
		private void Update()
		{
			if (!this.m_objCoolTime.activeSelf)
			{
				return;
			}
			this.m_time += Time.deltaTime;
			if (this.m_time > 1f)
			{
				this.SetCooltime();
				this.m_time -= 1f;
			}
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x0017A7F4 File Offset: 0x001789F4
		private void SetCooltime()
		{
			TimeSpan t = NKCSynchronizedTime.GetServerUTCTime(0.0) - this.m_lastUsedTime;
			TimeSpan timeSpan = TimeSpan.FromHours(12.0) - t;
			NKCUtil.SetLabelText(this.m_txtCoolTime, NKCUtilString.GetTimeSpanString(timeSpan));
			NKCUtil.SetGameobjectActive(this.m_objCoolTime, timeSpan.TotalSeconds > 0.0);
		}

		// Token: 0x04003E0A RID: 15882
		public NKCUIComStateButton m_btn;

		// Token: 0x04003E0B RID: 15883
		public NKCUISlotProfile m_slot;

		// Token: 0x04003E0C RID: 15884
		public Text m_txtLevel;

		// Token: 0x04003E0D RID: 15885
		public Text m_txtName;

		// Token: 0x04003E0E RID: 15886
		public Text m_txtUID;

		// Token: 0x04003E0F RID: 15887
		public Text m_txtLastConnectTime;

		// Token: 0x04003E10 RID: 15888
		public Text m_txtPower;

		// Token: 0x04003E11 RID: 15889
		public Text m_txtCoolTime;

		// Token: 0x04003E12 RID: 15890
		public GameObject m_objCoolTime;

		// Token: 0x04003E13 RID: 15891
		public GameObject m_objSelect;

		// Token: 0x04003E14 RID: 15892
		public GameObject m_objGuest;

		// Token: 0x04003E15 RID: 15893
		public GameObject m_objGuildLabel;

		// Token: 0x04003E16 RID: 15894
		public GameObject m_objGuild;

		// Token: 0x04003E17 RID: 15895
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04003E18 RID: 15896
		public Text m_lbGuildName;

		// Token: 0x04003E19 RID: 15897
		private DateTime m_lastUsedTime;

		// Token: 0x04003E1A RID: 15898
		private float m_time;

		// Token: 0x04003E1C RID: 15900
		private NKCUIDeckViewSupportSlot.OnSelect dOnSelect;

		// Token: 0x0200147A RID: 5242
		// (Invoke) Token: 0x0600A8EC RID: 43244
		public delegate void OnSelect(long supCode);
	}
}
