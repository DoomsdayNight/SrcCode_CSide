using System;
using Cs.Core.Util;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B4E RID: 2894
	public class NKCUIGuildIntro : NKCUIBase
	{
		// Token: 0x060083C7 RID: 33735 RVA: 0x002C66D3 File Offset: 0x002C48D3
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIGuildIntro>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_INTRO");
		}

		// Token: 0x060083C8 RID: 33736 RVA: 0x002C66E4 File Offset: 0x002C48E4
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIGuildIntro retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIGuildIntro>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x060083C9 RID: 33737 RVA: 0x002C66F4 File Offset: 0x002C48F4
		public void CloseInstance()
		{
			int num = NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_INTRO");
			Debug.Log(string.Format("NKCUIConsortiumIntro close resource retval is {0}", num));
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x060083CA RID: 33738 RVA: 0x002C6731 File Offset: 0x002C4931
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x060083CB RID: 33739 RVA: 0x002C6734 File Offset: 0x002C4934
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x060083CC RID: 33740 RVA: 0x002C6737 File Offset: 0x002C4937
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_CONSORTIUM_INTRO;
			}
		}

		// Token: 0x060083CD RID: 33741 RVA: 0x002C673E File Offset: 0x002C493E
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x002C674C File Offset: 0x002C494C
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x060083CF RID: 33743 RVA: 0x002C675C File Offset: 0x002C495C
		public void InitUI()
		{
			this.m_btnCreate.PointerClick.RemoveAllListeners();
			this.m_btnCreate.PointerClick.AddListener(new UnityAction(this.OnClickCreate));
			this.m_btnJoin.PointerClick.RemoveAllListeners();
			this.m_btnJoin.PointerClick.AddListener(new UnityAction(this.OnClickJoin));
			this.m_btnShop.PointerClick.RemoveAllListeners();
			this.m_btnShop.PointerClick.AddListener(new UnityAction(this.OnClickShop));
			this.m_btnSeasonReward.PointerClick.RemoveAllListeners();
			this.m_btnSeasonReward.PointerClick.AddListener(new UnityAction(this.OnClickSeasonReward));
		}

		// Token: 0x060083D0 RID: 33744 RVA: 0x002C681C File Offset: 0x002C4A1C
		public void Open()
		{
			NKCUtil.SetGameobjectActive(this.m_objJoinLock, ServiceTime.Recent < NKCGuildManager.MyData.guildJoinDisableTime);
			if (this.m_objJoinLock.activeSelf)
			{
				this.m_tNextJoinTime = NKCGuildManager.MyData.guildJoinDisableTime;
				this.SetRemainTime(this.m_tNextJoinTime);
				this.m_btnJoin.Lock(false);
			}
			else
			{
				this.m_tNextJoinTime = DateTime.MinValue;
				this.m_btnJoin.UnLock(false);
			}
			this.RefreshSeasonRewardRedDot();
			bool flag = NKCGuildCoopManager.CheckFirstSeasonStarted();
			NKCUtil.SetGameobjectActive(this.m_btnSeasonReward, NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0) && flag);
			NKCUtil.SetGameobjectActive(this.m_btnShop, NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_SHOP, 0, 0));
			base.UIOpened(true);
		}

		// Token: 0x060083D1 RID: 33745 RVA: 0x002C68D4 File Offset: 0x002C4AD4
		private void SetRemainTime(DateTime endTime)
		{
			if (ServiceTime.Recent < endTime)
			{
				NKCUtil.SetLabelText(this.m_lbJoinLockTime, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_COOLTIME_ONE_PARAM, NKCUtilString.GetRemainTimeString(endTime - ServiceTime.Recent, 2, true)));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objJoinLock, false);
			this.m_btnJoin.UnLock(false);
		}

		// Token: 0x060083D2 RID: 33746 RVA: 0x002C6930 File Offset: 0x002C4B30
		private void Update()
		{
			if (this.m_objJoinLock.activeSelf && this.m_tNextJoinTime > DateTime.MinValue)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRemainTime(this.m_tNextJoinTime);
				}
			}
		}

		// Token: 0x060083D3 RID: 33747 RVA: 0x002C6999 File Offset: 0x002C4B99
		private void OnClickCreate()
		{
			NKCUIGuildCreate.Instance.Open();
		}

		// Token: 0x060083D4 RID: 33748 RVA: 0x002C69A5 File Offset: 0x002C4BA5
		private void OnClickJoin()
		{
			NKCUIGuildJoin.Instance.Open();
		}

		// Token: 0x060083D5 RID: 33749 RVA: 0x002C69B1 File Offset: 0x002C4BB1
		private void OnClickSeasonReward()
		{
			NKCPopupGuildCoopSeasonReward.Instance.Open(new NKCPopupGuildCoopSeasonReward.OnClose(this.RefreshSeasonRewardRedDot));
		}

		// Token: 0x060083D6 RID: 33750 RVA: 0x002C69C9 File Offset: 0x002C4BC9
		private void RefreshSeasonRewardRedDot()
		{
			NKCUtil.SetGameobjectActive(this.m_objSeasonRewardRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable());
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x060083D7 RID: 33751 RVA: 0x002C69DB File Offset: 0x002C4BDB
		private NKCUIShopSingle ConsortiumShop
		{
			get
			{
				if (this.m_ConsortiumShop == null)
				{
					this.m_ConsortiumShop = NKCUIShopSingle.GetInstance("ab_ui_nkm_ui_consortium_shop", "NKM_UI_CONSORTIUM_SHOP");
				}
				return this.m_ConsortiumShop;
			}
		}

		// Token: 0x060083D8 RID: 33752 RVA: 0x002C6A06 File Offset: 0x002C4C06
		private void OnClickShop()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_SHOP, 0, 0))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_NONE_SYSTEM", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCUIShop.ShopShortcut("TAB_EXCHANGE_GUILD_COIN", 0, 0);
		}

		// Token: 0x04006FEE RID: 28654
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006FEF RID: 28655
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_INTRO";

		// Token: 0x04006FF0 RID: 28656
		public NKCUIComStateButton m_btnCreate;

		// Token: 0x04006FF1 RID: 28657
		public NKCUIComStateButton m_btnJoin;

		// Token: 0x04006FF2 RID: 28658
		public GameObject m_objJoinLock;

		// Token: 0x04006FF3 RID: 28659
		public Text m_lbJoinLockTime;

		// Token: 0x04006FF4 RID: 28660
		public NKCUIComStateButton m_btnShop;

		// Token: 0x04006FF5 RID: 28661
		public NKCUIComStateButton m_btnSeasonReward;

		// Token: 0x04006FF6 RID: 28662
		public GameObject m_objSeasonRewardRedDot;

		// Token: 0x04006FF7 RID: 28663
		private DateTime m_tNextJoinTime = DateTime.MinValue;

		// Token: 0x04006FF8 RID: 28664
		private float m_fDeltaTime;

		// Token: 0x04006FF9 RID: 28665
		private NKCUIShopSingle m_ConsortiumShop;
	}
}
