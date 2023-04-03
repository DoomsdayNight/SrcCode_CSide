using System;
using ClientPacket.Guild;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B59 RID: 2905
	public class NKCUIGuildLobbyWelfareSlot : MonoBehaviour
	{
		// Token: 0x06008481 RID: 33921 RVA: 0x002CAF9A File Offset: 0x002C919A
		public void InitUI()
		{
			this.m_btnBuy.PointerClick.RemoveAllListeners();
			this.m_btnBuy.PointerClick.AddListener(new UnityAction(this.OnClickBuy));
		}

		// Token: 0x06008482 RID: 33922 RVA: 0x002CAFC8 File Offset: 0x002C91C8
		public void SetData(GuildWelfareTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_fDeltaTime = 0f;
			this.m_GuildWelfareTemplet = templet;
			NKMCompanyBuffTemplet nkmcompanyBuffTemplet = NKMCompanyBuffTemplet.Find(templet.CompanyBuffID);
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCUtil.GetCompanyBuffIconSprite(nkmcompanyBuffTemplet.m_CompanyBuffIcon), false);
			if (templet.WelfareLvDisplay > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objLevel, true);
				NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, templet.WelfareLvDisplay));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objLevel, false);
			}
			NKCUtil.SetLabelText(this.m_lbName, NKCStringTable.GetString(templet.WelfareTextTitle, false));
			NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString(templet.WelfareTextDesc, false));
			this.SetBtnState();
			NKCUtil.SetGameobjectActive(this.m_objLock, !NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), templet.m_UnlockInfo, false));
			if (this.m_objLock.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbLock, NKCContentManager.MakeUnlockConditionString(templet.m_UnlockInfo, true));
			}
		}

		// Token: 0x06008483 RID: 33923 RVA: 0x002CB0D4 File Offset: 0x002C92D4
		private void SetBtnState()
		{
			if (NKCScenManager.CurrentUserData().HasBuffGroup(this.m_GuildWelfareTemplet.CompanyBuffGroupID))
			{
				if (NKCScenManager.CurrentUserData().HasBuff(this.m_GuildWelfareTemplet.CompanyBuffID))
				{
					NKCUtil.SetGameobjectActive(this.m_btnBuy, false);
					NKCUtil.SetGameobjectActive(this.m_objApply, true);
					this.SetRemainTime(NKCScenManager.CurrentUserData().GetBuffExpireTimeByGroupId(this.m_GuildWelfareTemplet.CompanyBuffGroupID));
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_btnBuy, false);
				NKCUtil.SetGameobjectActive(this.m_objApply, false);
				return;
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_btnBuy, true);
				NKCUtil.SetGameobjectActive(this.m_objApply, false);
				if (this.m_GuildWelfareTemplet.WelfareCategory == WELFARE_BUFF_TYPE.GUILD)
				{
					if (NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID).grade == GuildMemberGrade.Member)
					{
						this.m_btnBuy.Lock(false);
					}
					else
					{
						this.m_btnBuy.UnLock(false);
					}
				}
				else
				{
					this.m_btnBuy.UnLock(false);
				}
				NKCUtil.SetGameobjectActive(this.m_objApply, false);
				NKCUtil.SetImageSprite(this.m_imgCostIcon, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(this.m_GuildWelfareTemplet.WelfareRequireItemID), false);
				NKCUtil.SetLabelText(this.m_lbCost, this.m_GuildWelfareTemplet.WelfareRequireItemValue.ToString("N0"));
				if (this.m_GuildWelfareTemplet.WelfareCategory != WELFARE_BUFF_TYPE.PERSONAL)
				{
					if (this.m_GuildWelfareTemplet.WelfareCategory == WELFARE_BUFF_TYPE.GUILD)
					{
						if (NKCGuildManager.MyGuildData.unionPoint < (long)this.m_GuildWelfareTemplet.WelfareRequireItemValue)
						{
							NKCUtil.SetLabelTextColor(this.m_lbCost, Color.red);
							return;
						}
						NKCUtil.SetLabelTextColor(this.m_lbCost, NKCUtil.GetColor("#582817"));
					}
					return;
				}
				if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_GuildWelfareTemplet.WelfareRequireItemID) < (long)this.m_GuildWelfareTemplet.WelfareRequireItemValue)
				{
					NKCUtil.SetLabelTextColor(this.m_lbCost, Color.red);
					return;
				}
				NKCUtil.SetLabelTextColor(this.m_lbCost, NKCUtil.GetColor("#582817"));
				return;
			}
		}

		// Token: 0x06008484 RID: 33924 RVA: 0x002CB2D0 File Offset: 0x002C94D0
		private void SetRemainTime(DateTime expireTime)
		{
			NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetTimeSpanString(expireTime - NKCSynchronizedTime.GetServerUTCTime(0.0)));
		}

		// Token: 0x06008485 RID: 33925 RVA: 0x002CB2F8 File Offset: 0x002C94F8
		private void OnClickBuy()
		{
			if (this.m_GuildWelfareTemplet.WelfareCategory != WELFARE_BUFF_TYPE.PERSONAL)
			{
				if (this.m_GuildWelfareTemplet.WelfareCategory == WELFARE_BUFF_TYPE.GUILD)
				{
					if (NKCGuildManager.MyGuildData.unionPoint >= (long)this.m_GuildWelfareTemplet.WelfareRequireItemValue)
					{
						NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_WELFARE_GUILD_CONFIRM_TITLE, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_WELFARE_SUBTAB_GUILD_CONFIRM_BODY, NKCStringTable.GetString(this.m_GuildWelfareTemplet.WelfareTextTitle, false)), this.m_GuildWelfareTemplet.WelfareRequireItemID, this.m_GuildWelfareTemplet.WelfareRequireItemValue, NKCGuildManager.MyGuildData.unionPoint, new NKCPopupResourceConfirmBox.OnButton(this.OnConfirm), null, false);
						return;
					}
					NKCPopupItemLack.Instance.OpenItemMiscLackPopup(this.m_GuildWelfareTemplet.WelfareRequireItemID, this.m_GuildWelfareTemplet.WelfareRequireItemValue, NKCGuildManager.MyGuildData.unionPoint);
				}
				return;
			}
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_GuildWelfareTemplet.WelfareRequireItemID) >= (long)this.m_GuildWelfareTemplet.WelfareRequireItemValue)
			{
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_WELFARE_PERSONAL_CONFIRM_TITLE, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_WELFARE_SUBTAB_PERSONAL_CONFIRM_BODY, NKCStringTable.GetString(this.m_GuildWelfareTemplet.WelfareTextTitle, false)), this.m_GuildWelfareTemplet.WelfareRequireItemID, this.m_GuildWelfareTemplet.WelfareRequireItemValue, new NKCPopupResourceConfirmBox.OnButton(this.OnConfirm), null, false);
				return;
			}
			NKCPopupItemLack.Instance.OpenItemMiscLackPopup(this.m_GuildWelfareTemplet.WelfareRequireItemID, this.m_GuildWelfareTemplet.WelfareRequireItemValue);
		}

		// Token: 0x06008486 RID: 33926 RVA: 0x002CB459 File Offset: 0x002C9659
		private void OnConfirm()
		{
			NKCPacketSender.Send_NKMPacket_GUILD_BUY_BUFF_REQ(this.m_GuildWelfareTemplet.ID);
		}

		// Token: 0x06008487 RID: 33927 RVA: 0x002CB46C File Offset: 0x002C966C
		private void Update()
		{
			if (this.m_objApply.activeSelf)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					if (NKCScenManager.CurrentUserData().HasBuffGroup(this.m_GuildWelfareTemplet.CompanyBuffGroupID))
					{
						this.SetRemainTime(NKCScenManager.CurrentUserData().GetBuffExpireTimeByGroupId(this.m_GuildWelfareTemplet.CompanyBuffGroupID));
						return;
					}
					this.SetBtnState();
				}
			}
		}

		// Token: 0x040070AD RID: 28845
		public Image m_imgIcon;

		// Token: 0x040070AE RID: 28846
		public GameObject m_objLevel;

		// Token: 0x040070AF RID: 28847
		public Text m_lbLevel;

		// Token: 0x040070B0 RID: 28848
		public Text m_lbName;

		// Token: 0x040070B1 RID: 28849
		public Text m_lbDesc;

		// Token: 0x040070B2 RID: 28850
		public NKCUIComStateButton m_btnBuy;

		// Token: 0x040070B3 RID: 28851
		public Image m_imgCostIcon;

		// Token: 0x040070B4 RID: 28852
		public Text m_lbCost;

		// Token: 0x040070B5 RID: 28853
		public GameObject m_objApply;

		// Token: 0x040070B6 RID: 28854
		public Text m_lbRemainTime;

		// Token: 0x040070B7 RID: 28855
		public GameObject m_objLock;

		// Token: 0x040070B8 RID: 28856
		public Text m_lbLock;

		// Token: 0x040070B9 RID: 28857
		private float m_fDeltaTime;

		// Token: 0x040070BA RID: 28858
		private GuildWelfareTemplet m_GuildWelfareTemplet;
	}
}
