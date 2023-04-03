using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Mode;
using ClientPacket.Warfare;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009DA RID: 2522
	public class NKCUIShadowBattle : NKCUIBase
	{
		// Token: 0x06006C31 RID: 27697 RVA: 0x00235138 File Offset: 0x00233338
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIShadowBattle.s_LoadedUIData))
			{
				NKCUIShadowBattle.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIShadowBattle>("AB_UI_OPERATION_SHADOW", "AB_UI_OPERATION_SHADOW_READY", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIShadowBattle.CleanupInstance));
			}
			return NKCUIShadowBattle.s_LoadedUIData;
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06006C32 RID: 27698 RVA: 0x0023516C File Offset: 0x0023336C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIShadowBattle.s_LoadedUIData != null && NKCUIShadowBattle.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06006C33 RID: 27699 RVA: 0x00235181 File Offset: 0x00233381
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIShadowBattle.s_LoadedUIData != null && NKCUIShadowBattle.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x00235196 File Offset: 0x00233396
		public static void CleanupInstance()
		{
			NKCUIShadowBattle.s_LoadedUIData = null;
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06006C35 RID: 27701 RVA: 0x0023519E File Offset: 0x0023339E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06006C36 RID: 27702 RVA: 0x002351A1 File Offset: 0x002333A1
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06006C37 RID: 27703 RVA: 0x002351A4 File Offset: 0x002333A4
		public override string MenuName
		{
			get
			{
				return "그림자 전당";
			}
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x002351AC File Offset: 0x002333AC
		public void Init()
		{
			NKCUIComStateButton btnGiveUp = this.m_btnGiveUp;
			if (btnGiveUp != null)
			{
				btnGiveUp.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnGiveUp2 = this.m_btnGiveUp;
			if (btnGiveUp2 != null)
			{
				btnGiveUp2.PointerClick.AddListener(new UnityAction(this.OnTouchGiveUp));
			}
			NKCUIComStateButton btnBack = this.m_btnBack;
			if (btnBack != null)
			{
				btnBack.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnBack2 = this.m_btnBack;
			if (btnBack2 != null)
			{
				btnBack2.PointerClick.AddListener(new UnityAction(this.OnTouchBack));
			}
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.dOnGetObject += this.OnGetObject;
				this.m_scrollRect.dOnProvideData += this.OnProvideData;
				this.m_scrollRect.dOnReturnObject += this.OnReturnObject;
				this.m_scrollRect.ContentConstraintCount = 1;
				this.m_scrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_scrollRect, null);
			}
			if (this.m_item_1 != null)
			{
				this.m_item_1.SetOnClickPlusBtn(new NKCUIComItemCount.OnClickPlusBtn(this.m_item_1.OpenMoveToShopPopup));
			}
			if (this.m_item_2 != null)
			{
				this.m_item_2.SetOnClickPlusBtn(new NKCUIComItemCount.OnClickPlusBtn(this.m_item_2.OpenMoveToShopPopup));
			}
		}

		// Token: 0x06006C39 RID: 27705 RVA: 0x002352F4 File Offset: 0x002334F4
		public void Open(int palaceID)
		{
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(palaceID);
			this.m_lstBattleTemplet.Clear();
			this.m_lstBattleTemplet = NKMShadowPalaceManager.GetBattleTemplets(palaceID);
			if (this.m_lstBattleTemplet == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(palaceTemplet.STAGE_MUSIC_NAME))
			{
				NKCSoundManager.PlayMusic(palaceTemplet.STAGE_MUSIC_NAME, true, 1f, false, 0f, 0f);
			}
			this.m_palaceID = palaceID;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			this.m_palaceData = nkmuserData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData v) => v.palaceId == palaceID);
			this.m_lstBattleTemplet.Sort((NKMShadowBattleTemplet a, NKMShadowBattleTemplet b) => a.BATTLE_ORDER.CompareTo(b.BATTLE_ORDER));
			this.m_scrollRect.TotalCount = this.m_lstBattleTemplet.Count;
			this.m_scrollRect.RefreshCells(true);
			NKCUtil.SetLabelText(this.m_txtName, palaceTemplet.PalaceName);
			string msg = "-:--:--";
			string msg2 = "-:--:--";
			if (this.m_palaceData != null)
			{
				double num = 0.0;
				double num2 = 0.0;
				for (int i = 0; i < this.m_palaceData.dungeonDataList.Count; i++)
				{
					NKMPalaceDungeonData nkmpalaceDungeonData = this.m_palaceData.dungeonDataList[i];
					num += (double)nkmpalaceDungeonData.bestTime;
					num2 += (double)nkmpalaceDungeonData.recentTime;
				}
				if (num > 0.0)
				{
					msg = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds(num));
				}
				if (num2 > 0.0)
				{
					msg2 = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds(num2));
				}
				bool flag = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SHADOW_PALACE_MULTIPLY);
				if (nkmuserData.m_ShadowPalace.rewardMultiply > 1 && flag)
				{
					NKCUtil.SetGameobjectActive(this.m_objPlayingMultiply, true);
					NKCUtil.SetLabelText(this.m_txtPlayingMultiply, NKCUtilString.GET_MULTIPLY_REWARD_ONE_PARAM, new object[]
					{
						nkmuserData.m_ShadowPalace.rewardMultiply
					});
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objPlayingMultiply, false);
				}
			}
			NKCUtil.SetLabelText(this.m_txtBestTime, msg);
			NKCUtil.SetLabelText(this.m_txtCurrentTime, msg2);
			this.SetLife();
			this.SetCost();
			base.UIOpened(true);
			this.m_ani.Play("DF");
			this.m_aniScroll.Play("NKM_UI_SHADOW_READY_SLOT_LIST");
			this.CheckTutorial();
		}

		// Token: 0x06006C3A RID: 27706 RVA: 0x00235558 File Offset: 0x00233758
		private void SetLife()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			for (int i = 0; i < this.m_lstLife.Count; i++)
			{
				if (i < nkmuserData.m_ShadowPalace.life)
				{
					this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE");
				}
				else
				{
					this.m_lstLife[i].Play("NKM_UI_SHADOW_READY_LIFE_OFF");
				}
			}
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x002355C0 File Offset: 0x002337C0
		private void SetCost()
		{
			if (NKMShadowPalaceManager.GetPalaceTemplet(this.m_palaceID) == null)
			{
				return;
			}
			NKMUserData userData = NKCScenManager.CurrentUserData();
			this.m_item_1.SetData(userData, 19);
			this.m_item_2.SetData(userData, 20);
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x002355FD File Offset: 0x002337FD
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
			if (itemData == null)
			{
				return;
			}
			if (itemData.ItemID == 19 || itemData.ItemID == 20)
			{
				this.SetCost();
			}
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x00235624 File Offset: 0x00233824
		public override void CloseInternal()
		{
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x00235628 File Offset: 0x00233828
		private RectTransform OnGetObject(int index)
		{
			if (this.m_stkBattleSlotPool.Count > 0)
			{
				return this.m_stkBattleSlotPool.Pop().GetComponent<RectTransform>();
			}
			NKCUIShadowBattleSlot nkcuishadowBattleSlot = UnityEngine.Object.Instantiate<NKCUIShadowBattleSlot>(this.m_prefabSlot);
			nkcuishadowBattleSlot.transform.SetParent(this.m_trSlot);
			nkcuishadowBattleSlot.Init();
			return nkcuishadowBattleSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x0023567C File Offset: 0x0023387C
		private void OnProvideData(Transform tr, int idx)
		{
			NKCUIShadowBattleSlot component = tr.GetComponent<NKCUIShadowBattleSlot>();
			if (component == null)
			{
				return;
			}
			NKMShadowBattleTemplet battleTemplet = this.m_lstBattleTemplet[idx];
			NKMPalaceDungeonData dungeonData = null;
			int battle_ORDER = this.m_lstBattleTemplet[0].BATTLE_ORDER;
			if (this.m_palaceData != null)
			{
				dungeonData = this.m_palaceData.dungeonDataList.Find((NKMPalaceDungeonData v) => v.dungeonId == battleTemplet.DUNGEON_ID);
				NKMShadowBattleTemplet nkmshadowBattleTemplet = this.m_lstBattleTemplet.Find((NKMShadowBattleTemplet v) => v.DUNGEON_ID == this.m_palaceData.currentDungeonId);
				if (nkmshadowBattleTemplet != null)
				{
					battle_ORDER = nkmshadowBattleTemplet.BATTLE_ORDER;
				}
			}
			component.SetData(battleTemplet, dungeonData, battle_ORDER, new NKCUIShadowBattleSlot.OnTouchBattle(this.OnTouchBattle));
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x00235731 File Offset: 0x00233931
		private void OnReturnObject(Transform go)
		{
			if (base.GetComponent<NKCUIShadowBattleSlot>() != null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			this.m_stkBattleSlotPool.Push(go.GetComponent<NKCUIShadowBattleSlot>());
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x00235768 File Offset: 0x00233968
		private void OnTouchGiveUp()
		{
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(this.m_palaceID);
			if (palaceTemplet == null)
			{
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(palaceTemplet.STAGE_REQ_ITEM_ID);
			string content = NKCUtilString.GET_SHADOW_PALACE_GIVE_UP(new object[]
			{
				palaceTemplet.PALACE_NUM_UI,
				palaceTemplet.PalaceName,
				itemMiscTempletByID.GetItemName()
			});
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, content, delegate()
			{
				NKCPacketSender.Send_NKMPacket_SHADOW_PALACE_GIVEUP_ACK(this.m_palaceID);
			}, null, false);
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x002357D6 File Offset: 0x002339D6
		private void OnTouchBack()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_PALACE, true);
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x002357E8 File Offset: 0x002339E8
		private void OnTouchBattle(NKMShadowBattleTemplet battleTemplet)
		{
			if (battleTemplet == null)
			{
				return;
			}
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(this.m_palaceID);
			if (palaceTemplet == null)
			{
				return;
			}
			int battle_ORDER = this.m_lstBattleTemplet.First<NKMShadowBattleTemplet>().BATTLE_ORDER;
			if (this.m_palaceData != null)
			{
				NKMShadowBattleTemplet nkmshadowBattleTemplet = this.m_lstBattleTemplet.Find((NKMShadowBattleTemplet v) => v.DUNGEON_ID == this.m_palaceData.currentDungeonId);
				if (nkmshadowBattleTemplet != null)
				{
					battle_ORDER = nkmshadowBattleTemplet.BATTLE_ORDER;
				}
			}
			if (battleTemplet.BATTLE_ORDER > battle_ORDER)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EPISODE_GIVE_UP_WARFARE, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpWarfare), null, false);
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(battleTemplet.DUNGEON_ID);
			if (dungeonTempletBase == null)
			{
				return;
			}
			if (dungeonTempletBase.m_UseEventDeck == 0)
			{
				Debug.LogError(string.Format("그림자 전당은 이벤트 덱만 사용 가능함! dungeonTempletBase.m_UseEventDeck : {0}", dungeonTempletBase.m_UseEventDeck));
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(dungeonTempletBase, DeckContents.SHADOW_PALACE);
			NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetReservedBGM(palaceTemplet.STAGE_MUSIC_NAME);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x002358E4 File Offset: 0x00233AE4
		private void OnClickOkGiveUpWarfare()
		{
			NKMPacket_WARFARE_GAME_GIVE_UP_REQ packet = new NKMPacket_WARFARE_GAME_GIVE_UP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x0023590C File Offset: 0x00233B0C
		public void StartCurrentBattle()
		{
			if (NKMShadowPalaceManager.GetPalaceTemplet(this.m_palaceID) == null)
			{
				return;
			}
			NKMShadowBattleTemplet nkmshadowBattleTemplet = null;
			if (this.m_palaceData != null)
			{
				nkmshadowBattleTemplet = this.m_lstBattleTemplet.Find((NKMShadowBattleTemplet v) => v.DUNGEON_ID == this.m_palaceData.currentDungeonId);
			}
			if (nkmshadowBattleTemplet == null)
			{
				nkmshadowBattleTemplet = this.m_lstBattleTemplet.First<NKMShadowBattleTemplet>();
			}
			this.OnTouchBattle(nkmshadowBattleTemplet);
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x0023595F File Offset: 0x00233B5F
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.ShadowBattle, true);
		}

		// Token: 0x040057F0 RID: 22512
		private const string BUNDLE_NAME = "AB_UI_OPERATION_SHADOW";

		// Token: 0x040057F1 RID: 22513
		private const string ASSET_NAME = "AB_UI_OPERATION_SHADOW_READY";

		// Token: 0x040057F2 RID: 22514
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x040057F3 RID: 22515
		public Text m_txtName;

		// Token: 0x040057F4 RID: 22516
		[Header("배틀 슬롯")]
		public Transform m_trSlot;

		// Token: 0x040057F5 RID: 22517
		public NKCUIShadowBattleSlot m_prefabSlot;

		// Token: 0x040057F6 RID: 22518
		public LoopScrollRect m_scrollRect;

		// Token: 0x040057F7 RID: 22519
		[Header("라이프")]
		public List<Animator> m_lstLife;

		// Token: 0x040057F8 RID: 22520
		[Header("시간 기록")]
		public Text m_txtCurrentTime;

		// Token: 0x040057F9 RID: 22521
		public Text m_txtBestTime;

		// Token: 0x040057FA RID: 22522
		[Header("버튼")]
		public NKCUIComStateButton m_btnGiveUp;

		// Token: 0x040057FB RID: 22523
		public NKCUIComStateButton m_btnBack;

		// Token: 0x040057FC RID: 22524
		[Header("애니")]
		public Animator m_ani;

		// Token: 0x040057FD RID: 22525
		public Animator m_aniScroll;

		// Token: 0x040057FE RID: 22526
		[Header("재화")]
		public NKCUIComItemCount m_item_1;

		// Token: 0x040057FF RID: 22527
		public NKCUIComItemCount m_item_2;

		// Token: 0x04005800 RID: 22528
		[Header("중첩작전")]
		public GameObject m_objPlayingMultiply;

		// Token: 0x04005801 RID: 22529
		public Text m_txtPlayingMultiply;

		// Token: 0x04005802 RID: 22530
		private int m_palaceID;

		// Token: 0x04005803 RID: 22531
		private NKMPalaceData m_palaceData;

		// Token: 0x04005804 RID: 22532
		private Stack<NKCUIShadowBattleSlot> m_stkBattleSlotPool = new Stack<NKCUIShadowBattleSlot>();

		// Token: 0x04005805 RID: 22533
		private List<NKMShadowBattleTemplet> m_lstBattleTemplet = new List<NKMShadowBattleTemplet>();
	}
}
