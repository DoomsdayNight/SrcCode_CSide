using System;
using ClientPacket.Common;
using NKC.UI;
using NKC.UI.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200079C RID: 1948
	public class NKCUILobbyUserInfo : MonoBehaviour
	{
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06004C66 RID: 19558 RVA: 0x0016DEFC File Offset: 0x0016C0FC
		// (set) Token: 0x06004C67 RID: 19559 RVA: 0x0016DF09 File Offset: 0x0016C109
		public float ExpBarValue
		{
			get
			{
				return this.m_imgExp.fillAmount;
			}
			set
			{
				this.m_imgExp.fillAmount = value;
			}
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0016DF18 File Offset: 0x0016C118
		public void Init()
		{
			this.m_UserProfileSlot.Init();
			this.SetMoveToShop(this.m_UICountCredit);
			this.SetMoveToShop(this.m_UICountEternium);
			this.SetMoveToShop(this.m_UICountCash);
			if (this.m_btnGuild != null)
			{
				this.m_btnGuild.PointerClick.RemoveAllListeners();
				this.m_btnGuild.PointerClick.AddListener(new UnityAction(this.OnClickGuild));
			}
			this.m_csbtnUserBuff.PointerClick.RemoveAllListeners();
			this.m_csbtnUserBuff.PointerClick.AddListener(new UnityAction(this.OpenUserBuff));
			this.m_csbtnUserBuffNone.PointerClick.RemoveAllListeners();
			this.m_csbtnUserBuffNone.PointerClick.AddListener(new UnityAction(this.OpenUserBuff));
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0016DFE6 File Offset: 0x0016C1E6
		private void SetMoveToShop(NKCUIComItemCount targetButton)
		{
			if (targetButton == null)
			{
				return;
			}
			targetButton.SetOnClickPlusBtn(new NKCUIComItemCount.OnClickPlusBtn(targetButton.OpenMoveToShopPopup));
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0016E004 File Offset: 0x0016C204
		public void SetData(NKMUserData userData)
		{
			this.UpdateUserProfileIcon(userData);
			this.UpdateLevelAndExp(userData);
			this.UpdateUserBuffCount(userData);
			this.RefreshNickname();
			this.SetGuildData();
			NKCUtil.SetGameobjectActive(this.m_objGuildNotify, NKCAlarmManager.CheckGuildNotify(userData));
			this.m_UICountCredit.SetData(userData, 1);
			this.m_UICountEternium.SetData(userData, 2);
			this.m_UICountCash.SetData(userData, 101);
			this.m_RechargeEternium.UpdateData(userData);
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0016E078 File Offset: 0x0016C278
		public void SetGuildData()
		{
			if (this.m_btnGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnGuild, NKCGuildManager.HasGuild());
				if (this.m_btnGuild.gameObject.activeSelf)
				{
					this.m_BadgeUI.SetData(NKCGuildManager.MyGuildData.badgeId, false);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCUtilString.GetUserGuildName(NKCGuildManager.MyGuildData.name, false));
				}
			}
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0016E0E8 File Offset: 0x0016C2E8
		public void UpdateUserProfileIcon(NKMUserData userData)
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				this.m_UserProfileSlot.SetProfiledata(userProfileData, new NKCUISlotProfile.OnClick(this.OpenUserInfo));
				return;
			}
			this.m_UserProfileSlot.SetProfiledata(1001, 0, 0, new NKCUISlotProfile.OnClick(this.OpenUserInfo));
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0016E13A File Offset: 0x0016C33A
		public void UpdateLevelAndExp(NKMUserData userData)
		{
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, userData.UserLevel));
			this.ExpBarValue = NKCExpManager.GetNextLevelExpProgress(userData);
			this.m_RechargeEternium.UpdateData(userData);
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0016E174 File Offset: 0x0016C374
		public void UpdateUserBuffCount(NKMUserData userData)
		{
			if (userData != null)
			{
				NKCCompanyBuff.RemoveExpiredBuffs(userData.m_companyBuffDataList);
				NKCUtil.SetGameobjectActive(this.m_csbtnUserBuff, userData.m_companyBuffDataList.Count > 0);
				NKCUtil.SetGameobjectActive(this.m_csbtnUserBuffNone, userData.m_companyBuffDataList.Count == 0);
				this.m_lbUserBuffCount.text = userData.m_companyBuffDataList.Count.ToString();
			}
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0016E1E0 File Offset: 0x0016C3E0
		public void OnResourceValueChange(NKMItemMiscData itemData)
		{
			if (itemData == null)
			{
				return;
			}
			int itemID = itemData.ItemID;
			if (itemID == 1)
			{
				this.m_UICountCredit.UpdateData(itemData, 0);
				return;
			}
			if (itemID == 2)
			{
				this.m_UICountEternium.UpdateData(itemData, 0);
				this.m_RechargeEternium.UpdateData(NKCScenManager.CurrentUserData());
				return;
			}
			if (itemID != 101)
			{
				return;
			}
			this.m_UICountCash.UpdateData(itemData, 0);
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0016E23F File Offset: 0x0016C43F
		private void OnClickGuild()
		{
			if (NKCGuildManager.HasGuild())
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
			}
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0016E255 File Offset: 0x0016C455
		private void OpenUserInfo(NKCUISlotProfile slot, int frameID)
		{
			NKCUIUserInfo.Instance.Open(NKCScenManager.GetScenManager().GetMyUserData());
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0016E26C File Offset: 0x0016C46C
		private void OpenUserBuff()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKCCompanyBuff.RemoveExpiredBuffs(nkmuserData.m_companyBuffDataList);
				if (nkmuserData.m_companyBuffDataList.Count > 0)
				{
					NKCPopupCompanyBuff.Instance.Open();
					return;
				}
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_LOBBY_USER_BUFF_NONE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0016E2BC File Offset: 0x0016C4BC
		public void RefreshNickname()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKCUtil.SetLabelText(this.m_lbUserName, NKCUtilString.GetUserNickname(myUserData.m_UserNickName, false));
			}
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0016E2EE File Offset: 0x0016C4EE
		public void RefreshRechargeEternium()
		{
			NKCUIRechargeEternium rechargeEternium = this.m_RechargeEternium;
			if (rechargeEternium == null)
			{
				return;
			}
			rechargeEternium.UpdateData(NKCScenManager.GetScenManager().GetMyUserData());
		}

		// Token: 0x04003C20 RID: 15392
		public NKCUISlotProfile m_UserProfileSlot;

		// Token: 0x04003C21 RID: 15393
		public Text m_lbLevel;

		// Token: 0x04003C22 RID: 15394
		public Image m_imgExp;

		// Token: 0x04003C23 RID: 15395
		public Text m_lbUserName;

		// Token: 0x04003C24 RID: 15396
		public NKCUIComStateButton m_btnGuild;

		// Token: 0x04003C25 RID: 15397
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04003C26 RID: 15398
		public Text m_lbGuildName;

		// Token: 0x04003C27 RID: 15399
		public GameObject m_objGuildNotify;

		// Token: 0x04003C28 RID: 15400
		public NKCUIComItemCount m_UICountCredit;

		// Token: 0x04003C29 RID: 15401
		public NKCUIComItemCount m_UICountEternium;

		// Token: 0x04003C2A RID: 15402
		public NKCUIComItemCount m_UICountCash;

		// Token: 0x04003C2B RID: 15403
		public NKCUIComStateButton m_csbtnUserBuff;

		// Token: 0x04003C2C RID: 15404
		public NKCUIComStateButton m_csbtnUserBuffNone;

		// Token: 0x04003C2D RID: 15405
		public Text m_lbUserBuffCount;

		// Token: 0x04003C2E RID: 15406
		public NKCUIRechargeEternium m_RechargeEternium;
	}
}
