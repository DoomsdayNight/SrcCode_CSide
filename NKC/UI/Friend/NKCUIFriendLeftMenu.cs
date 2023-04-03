using System;
using System.Collections.Generic;
using ClientPacket.Community;
using NKM;
using NKM.Contract2;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Friend
{
	// Token: 0x02000B11 RID: 2833
	public class NKCUIFriendLeftMenu : MonoBehaviour
	{
		// Token: 0x06008090 RID: 32912 RVA: 0x002B503C File Offset: 0x002B323C
		public void Init()
		{
			this.m_NKCUIFriendTopMenu.Init();
			this.m_NKCUIFriendMyProfile.Init();
			if (this.m_NKM_UI_FRIEND_MENU_MANAGEMENT != null)
			{
				this.m_NKM_UI_FRIEND_MENU_MANAGEMENT.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENU_MANAGEMENT.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickFriendManagement));
			}
			if (this.m_NKM_UI_FRIEND_MENU_ADD != null)
			{
				this.m_NKM_UI_FRIEND_MENU_ADD.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENU_ADD.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickFriendRegister));
			}
			if (this.m_NKM_UI_FRIEND_MENU_PROFILE != null)
			{
				this.m_NKM_UI_FRIEND_MENU_PROFILE.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENU_PROFILE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickMyProfile));
			}
			if (this.m_NKM_UI_FRIEND_MENU_SHOP != null)
			{
				this.m_NKM_UI_FRIEND_MENU_SHOP.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENU_SHOP.PointerClick.AddListener(new UnityAction(this.OnClickFriendshipShop));
			}
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x002B5147 File Offset: 0x002B3347
		public void Reset()
		{
			this.m_ManageBtn.Select(false, true, false);
			this.m_ManageBtn.Select(true, false, false);
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x002B5167 File Offset: 0x002B3367
		public void Close()
		{
			this.m_NKCUIFriendTopMenu.CloseInstance();
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x002B5174 File Offset: 0x002B3374
		public void SetAddReceiveNew(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENU_ADD_NEW, bSet);
			this.m_NKCUIFriendTopMenu.SetAddReceiveNew(bSet);
		}

		// Token: 0x06008094 RID: 32916 RVA: 0x002B518E File Offset: 0x002B338E
		public void OnClickFriendManagement(bool bSet)
		{
			this.m_NKCUIFriendTopMenu.Open(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_MANAGE);
			this.m_NKCUIFriendMyProfile.Close();
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x002B51A7 File Offset: 0x002B33A7
		public void ForceClickRegisterBtn()
		{
			this.m_RegisterBtn.Select(false, true, false);
			this.m_RegisterBtn.Select(true, false, false);
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x002B51C7 File Offset: 0x002B33C7
		public void OnClickFriendRegister(bool bSet)
		{
			this.m_NKCUIFriendTopMenu.Open(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_REGISTER);
			this.m_NKCUIFriendMyProfile.Close();
		}

		// Token: 0x06008097 RID: 32919 RVA: 0x002B51E0 File Offset: 0x002B33E0
		public void OnClickMyProfile(bool bSet)
		{
			if (bSet)
			{
				this.m_NKM_UI_FRIEND_MENU_PROFILE.Select(true, true, false);
			}
			this.m_NKCUIFriendTopMenu.Close();
			this.m_NKCUIFriendMyProfile.Open();
		}

		// Token: 0x06008098 RID: 32920 RVA: 0x002B520C File Offset: 0x002B340C
		public void OnClickFriendshipShop()
		{
			string shortCutParam = "";
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			foreach (ContractTempletBase contractTempletBase in NKMTempletContainer<ContractTempletBase>.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(contractTempletBase.EventIntervalTemplet) && (nkccontractDataMgr == null || nkccontractDataMgr.CheckOpenCond(contractTempletBase)))
				{
					HashSet<int> priceItemIDSet = contractTempletBase.GetPriceItemIDSet();
					if (priceItemIDSet != null && priceItemIDSet.Contains(8))
					{
						shortCutParam = contractTempletBase.ContractStrID;
						break;
					}
				}
			}
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_CONTRACT, shortCutParam, false);
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x002B52A4 File Offset: 0x002B34A4
		public void CloseSortMenu()
		{
			this.m_NKCUIFriendTopMenu.CloseSortMenu(false);
		}

		// Token: 0x0600809A RID: 32922 RVA: 0x002B52B2 File Offset: 0x002B34B2
		public NKCUIFriendSlot.FRIEND_SLOT_TYPE GetCurrentSlotType()
		{
			return this.m_NKCUIFriendTopMenu.GetCurrentSlotType();
		}

		// Token: 0x0600809B RID: 32923 RVA: 0x002B52BF File Offset: 0x002B34BF
		public void OnRecv(NKMPacket_FRIEND_LIST_ACK cNKMPacket_FRIEND_LIST_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_LIST_ACK);
		}

		// Token: 0x0600809C RID: 32924 RVA: 0x002B52CD File Offset: 0x002B34CD
		public void OnRecv(NKMPacket_SET_EMBLEM_ACK cNKMPacket_SET_EMBLEM_ACK)
		{
			if (this.m_NKCUIFriendMyProfile == null)
			{
				return;
			}
			this.m_NKCUIFriendMyProfile.CheckNKCPopupEmblemListAndClose();
			if (this.m_NKCUIFriendMyProfile.IsOpen())
			{
				this.m_NKCUIFriendMyProfile.OnRecv(cNKMPacket_SET_EMBLEM_ACK);
			}
		}

		// Token: 0x0600809D RID: 32925 RVA: 0x002B5302 File Offset: 0x002B3502
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_NOT cNKMPacket_FRIEND_ACCEPT_NOT)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_ACCEPT_NOT);
		}

		// Token: 0x0600809E RID: 32926 RVA: 0x002B5310 File Offset: 0x002B3510
		public void OnRecv(NKMPacket_FRIEND_DELETE_NOT cNKMPacket_FRIEND_DEL_NOT)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_DEL_NOT);
		}

		// Token: 0x0600809F RID: 32927 RVA: 0x002B531E File Offset: 0x002B351E
		public void OnRecv(NKMPacket_FRIEND_REQUEST_NOT cNKMPacket_FRIEND_ADD_NOT)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_ADD_NOT);
		}

		// Token: 0x060080A0 RID: 32928 RVA: 0x002B532C File Offset: 0x002B352C
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_NOT cNKMPacket_FRIEND_ADD_CANCEL_NOT)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_ADD_CANCEL_NOT);
		}

		// Token: 0x060080A1 RID: 32929 RVA: 0x002B533A File Offset: 0x002B353A
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK)
		{
			if (this.m_NKCUIFriendMyProfile.IsOpen())
			{
				this.m_NKCUIFriendMyProfile.UpdateCommentUI();
			}
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x002B5354 File Offset: 0x002B3554
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK)
		{
			if (this.m_NKCUIFriendMyProfile.IsOpen())
			{
				this.m_NKCUIFriendMyProfile.UpdateDeckUI();
			}
		}

		// Token: 0x060080A3 RID: 32931 RVA: 0x002B536E File Offset: 0x002B356E
		public void UpdateMainCharUI()
		{
			if (this.m_NKCUIFriendMyProfile.IsOpen())
			{
				this.m_NKCUIFriendMyProfile.UpdateMainCharUI();
			}
		}

		// Token: 0x060080A4 RID: 32932 RVA: 0x002B5388 File Offset: 0x002B3588
		public void OnRecv(NKMPacket_FRIEND_DELETE_ACK cNKMPacket_FRIEND_DEL_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_DEL_ACK);
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x002B5396 File Offset: 0x002B3596
		public void OnRecv(NKMPacket_FRIEND_SEARCH_ACK cNKMPacket_FRIEND_SEARCH_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_SEARCH_ACK);
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x002B53A4 File Offset: 0x002B35A4
		public void OnRecv(NKMPacket_FRIEND_RECOMMEND_ACK cNKMPacket_FRIEND_RECOMMEND_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_RECOMMEND_ACK);
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x002B53B2 File Offset: 0x002B35B2
		public void OnRecv(NKMPacket_FRIEND_REQUEST_ACK cNKMPacket_FRIEND_ADD_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_ADD_ACK);
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x002B53C0 File Offset: 0x002B35C0
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_ACK cNKMPacket_FRIEND_ACCEPT_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_ACCEPT_ACK);
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x002B53CE File Offset: 0x002B35CE
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_ACK cNKMPacket_FRIEND_ADD_CANCEL_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_ADD_CANCEL_ACK);
		}

		// Token: 0x060080AA RID: 32938 RVA: 0x002B53DC File Offset: 0x002B35DC
		public void OnRecv(NKMPacket_FRIEND_BLOCK_ACK cNKMPacket_FRIEND_BLOCK_ACK)
		{
			this.m_NKCUIFriendTopMenu.OnRecv(cNKMPacket_FRIEND_BLOCK_ACK);
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x002B53EA File Offset: 0x002B35EA
		public void RefreshNickname()
		{
			this.m_NKCUIFriendMyProfile.RefreshNickname();
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x002B53F7 File Offset: 0x002B35F7
		public void OnGuildDataChanged()
		{
			this.m_NKCUIFriendMyProfile.UpdateGuildData();
		}

		// Token: 0x04006CB5 RID: 27829
		public NKCUIFriendTopMenu m_NKCUIFriendTopMenu;

		// Token: 0x04006CB6 RID: 27830
		public NKCUIFriendMyProfile m_NKCUIFriendMyProfile;

		// Token: 0x04006CB7 RID: 27831
		public NKCUIComToggle m_ManageBtn;

		// Token: 0x04006CB8 RID: 27832
		public NKCUIComToggle m_RegisterBtn;

		// Token: 0x04006CB9 RID: 27833
		public NKCUIComToggle m_NKM_UI_FRIEND_MENU_MANAGEMENT;

		// Token: 0x04006CBA RID: 27834
		public NKCUIComToggle m_NKM_UI_FRIEND_MENU_ADD;

		// Token: 0x04006CBB RID: 27835
		public GameObject m_NKM_UI_FRIEND_MENU_ADD_NEW;

		// Token: 0x04006CBC RID: 27836
		public NKCUIComToggle m_NKM_UI_FRIEND_MENU_PROFILE;

		// Token: 0x04006CBD RID: 27837
		public NKCUIComButton m_NKM_UI_FRIEND_MENU_SHOP;
	}
}
