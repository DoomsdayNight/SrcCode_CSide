using System;
using System.Collections.Generic;
using ClientPacket.Item;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.Office.Forge
{
	// Token: 0x02000840 RID: 2112
	public class NKCOfficeFunitureForgeSlot : NKCOfficeSpineFurniture
	{
		// Token: 0x0600542B RID: 21547 RVA: 0x0019AFB1 File Offset: 0x001991B1
		public override void Init()
		{
			base.Init();
			if (this.m_Slot != null)
			{
				this.m_Slot.Init();
			}
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0019AFD2 File Offset: 0x001991D2
		public void SetIndex(int forgeSlotIndex)
		{
			this.m_ForgeSlotIndex = forgeSlotIndex;
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0019AFDC File Offset: 0x001991DC
		public void UpdateData(bool bAnimate)
		{
			if (this.m_ForgeSlotIndex < 1 || this.m_ForgeSlotIndex > NKMCraftData.MAX_CRAFT_SLOT_DATA)
			{
				this.SetMode(NKCOfficeFunitureForgeSlot.Mode.Disabled, bAnimate);
				return;
			}
			NKMCraftData craftData = NKCScenManager.GetScenManager().GetMyUserData().m_CraftData;
			if (craftData == null)
			{
				this.SetMode(NKCOfficeFunitureForgeSlot.Mode.Disabled, bAnimate);
				return;
			}
			NKMCraftSlotData slotData = craftData.GetSlotData((byte)this.m_ForgeSlotIndex);
			this.m_NKMEquipCreationSlotData = slotData;
			this.SetMode(this.GetMode(), bAnimate);
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0019B048 File Offset: 0x00199248
		private void SetMode(NKCOfficeFunitureForgeSlot.Mode mode, bool bAnimate)
		{
			string animName = "";
			bool flag = NKCOfficeFunitureForgeSlot.s_dicTransitionAnim.TryGetValue(new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(this.m_eCurrentMode, mode), out animName);
			string animName2;
			NKCOfficeFunitureForgeSlot.s_dicModeAnim.TryGetValue(mode, out animName2);
			if (bAnimate && flag && base.HasAnimation(this.m_aSpineFurniture, animName))
			{
				base.SetAnimation(this.m_aSpineFurniture, animName, false, 1f);
				base.AddAnimation(this.m_aSpineFurniture, animName2, true);
			}
			else
			{
				base.SetAnimation(this.m_aSpineFurniture, animName2, true, 1f);
			}
			this.m_eCurrentMode = mode;
			NKCUtil.SetGameobjectActive(this.m_rtTouchArea, mode == NKCOfficeFunitureForgeSlot.Mode.Completed);
			NKCUtil.SetGameobjectActive(this.m_objDisable, mode == NKCOfficeFunitureForgeSlot.Mode.Disabled);
			NKCUtil.SetGameobjectActive(this.m_objOpen, mode == NKCOfficeFunitureForgeSlot.Mode.Open);
			NKCUtil.SetGameobjectActive(this.m_objCompleted, mode == NKCOfficeFunitureForgeSlot.Mode.Completed);
			NKCUtil.SetGameobjectActive(this.m_objBusy, mode == NKCOfficeFunitureForgeSlot.Mode.Busy);
			if (mode - NKCOfficeFunitureForgeSlot.Mode.Completed <= 1)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_NKMEquipCreationSlotData.MoldID);
				if (itemMoldTempletByID != null)
				{
					NKCUtil.SetGameobjectActive(this.m_Slot, true);
					this.m_Slot.SetData(NKCUISlot.SlotData.MakeMoldItemData(itemMoldTempletByID.m_MoldID, (long)this.m_NKMEquipCreationSlotData.Count), false, new NKCUISlot.OnClick(this.OnClickSlot));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_Slot, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_Slot, false);
			}
			if (mode == NKCOfficeFunitureForgeSlot.Mode.Busy)
			{
				NKCUtil.SetGameobjectActive(this.m_lbTime, true);
				this.UpdateTime();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbTime, false);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0019B1B4 File Offset: 0x001993B4
		protected override void Update()
		{
			base.Update();
			NKCOfficeFunitureForgeSlot.Mode mode = this.GetMode();
			if (this.m_eCurrentMode != mode)
			{
				this.SetMode(mode, true);
				return;
			}
			if (this.m_eCurrentMode == NKCOfficeFunitureForgeSlot.Mode.Busy)
			{
				this.UpdateTime();
			}
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0019B1F0 File Offset: 0x001993F0
		private void UpdateTime()
		{
			TimeSpan timeSpan = new TimeSpan(this.m_NKMEquipCreationSlotData.CompleteDate - NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks);
			NKCUtil.SetLabelText(this.m_lbTime, NKCUtilString.GetTimeSpanString(timeSpan));
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0019B238 File Offset: 0x00199438
		private NKCOfficeFunitureForgeSlot.Mode GetMode()
		{
			if (this.m_NKMEquipCreationSlotData == null)
			{
				return NKCOfficeFunitureForgeSlot.Mode.Disabled;
			}
			switch (this.m_NKMEquipCreationSlotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)))
			{
			case NKM_CRAFT_SLOT_STATE.NECSS_EMPTY:
				return NKCOfficeFunitureForgeSlot.Mode.Open;
			case NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW:
				return NKCOfficeFunitureForgeSlot.Mode.Busy;
			case NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED:
				return NKCOfficeFunitureForgeSlot.Mode.Completed;
			default:
				return NKCOfficeFunitureForgeSlot.Mode.Disabled;
			}
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0019B284 File Offset: 0x00199484
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnClick();
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0019B28C File Offset: 0x0019948C
		public override void OnPointerClick(PointerEventData eventData)
		{
			this.OnClick();
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0019B294 File Offset: 0x00199494
		private void OnClick()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_CRAFT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_CRAFT, 0);
				return;
			}
			switch (this.GetMode())
			{
			case NKCOfficeFunitureForgeSlot.Mode.Disabled:
				this.TryUnlock();
				return;
			case NKCOfficeFunitureForgeSlot.Mode.Open:
				NKCUIForgeCraftMold.Instance.Open(this.m_ForgeSlotIndex);
				return;
			case NKCOfficeFunitureForgeSlot.Mode.Completed:
				this.SendCompletedPacket();
				return;
			case NKCOfficeFunitureForgeSlot.Mode.Busy:
				this.TryInstanceCraft();
				return;
			default:
				return;
			}
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0019B2FC File Offset: 0x001994FC
		private void TryInstanceCraft()
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(1012);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_FORGE_CRAFT_USE_MISC_ONE_PARAM, itemMiscTempletByID.GetItemName()), itemMiscTempletByID.m_ItemMiscID, 1, new NKCPopupResourceConfirmBox.OnButton(this.SendInstantCompletePacket), null, false);
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0019B34C File Offset: 0x0019954C
		private void SendInstantCompletePacket()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetCountMiscItem(1012) < 1L)
			{
				NKCShopManager.OpenItemLackPopup(1012, 1);
				return;
			}
			NKMPacket_CRAFT_INSTANT_COMPLETE_REQ nkmpacket_CRAFT_INSTANT_COMPLETE_REQ = new NKMPacket_CRAFT_INSTANT_COMPLETE_REQ();
			nkmpacket_CRAFT_INSTANT_COMPLETE_REQ.index = (byte)this.m_ForgeSlotIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CRAFT_INSTANT_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0019B3A8 File Offset: 0x001995A8
		private void SendCompletedPacket()
		{
			NKMPacket_CRAFT_COMPLETE_REQ nkmpacket_CRAFT_COMPLETE_REQ = new NKMPacket_CRAFT_COMPLETE_REQ();
			nkmpacket_CRAFT_COMPLETE_REQ.index = (byte)this.m_ForgeSlotIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CRAFT_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0019B3DC File Offset: 0x001995DC
		private void TryUnlock()
		{
			if (this.m_ForgeSlotIndex < 1 || this.m_ForgeSlotIndex > NKMCraftData.MAX_CRAFT_SLOT_DATA)
			{
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_CRAFT_SLOT_ADD, 101, 300, new NKCPopupResourceConfirmBox.OnButton(this.SendUnLockPacket), null, false);
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0019B42C File Offset: 0x0019962C
		public void SendUnLockPacket()
		{
			NKMPacket_CRAFT_UNLOCK_SLOT_REQ packet = new NKMPacket_CRAFT_UNLOCK_SLOT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0019B452 File Offset: 0x00199652
		public override RectTransform MakeHighlightRect()
		{
			if (this.m_rtTouchArea != null)
			{
				return this.m_rtTouchArea;
			}
			if (this.m_Slot != null)
			{
				return this.m_Slot.GetComponent<RectTransform>();
			}
			return base.MakeHighlightRect();
		}

		// Token: 0x0400432F RID: 17199
		private static readonly Dictionary<NKCOfficeFunitureForgeSlot.Mode, string> s_dicModeAnim = new Dictionary<NKCOfficeFunitureForgeSlot.Mode, string>
		{
			{
				NKCOfficeFunitureForgeSlot.Mode.Disabled,
				"LOCK_LOOP"
			},
			{
				NKCOfficeFunitureForgeSlot.Mode.Open,
				"OPEN_LOOP"
			},
			{
				NKCOfficeFunitureForgeSlot.Mode.Busy,
				"WORK_LOOP"
			},
			{
				NKCOfficeFunitureForgeSlot.Mode.Completed,
				"COMPLETE_LOOP"
			}
		};

		// Token: 0x04004330 RID: 17200
		private static readonly Dictionary<ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>, string> s_dicTransitionAnim = new Dictionary<ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>, string>
		{
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Disabled, NKCOfficeFunitureForgeSlot.Mode.Open),
				"LOCK_TO_OPEN"
			},
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Open, NKCOfficeFunitureForgeSlot.Mode.Busy),
				"OPEN_TO_WORK"
			},
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Busy, NKCOfficeFunitureForgeSlot.Mode.Completed),
				"WORK_TO_COMPLETE"
			},
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Busy, NKCOfficeFunitureForgeSlot.Mode.Open),
				"WORK_TO_OPEN"
			},
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Completed, NKCOfficeFunitureForgeSlot.Mode.Open),
				"COMPLETE_TO_OPEN"
			},
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Disabled, NKCOfficeFunitureForgeSlot.Mode.Completed),
				"WORK_TO_COMPLETE"
			},
			{
				new ValueTuple<NKCOfficeFunitureForgeSlot.Mode, NKCOfficeFunitureForgeSlot.Mode>(NKCOfficeFunitureForgeSlot.Mode.Open, NKCOfficeFunitureForgeSlot.Mode.Completed),
				"WORK_TO_COMPLETE"
			}
		};

		// Token: 0x04004331 RID: 17201
		[Header("제작 관련")]
		public Text m_lbTime;

		// Token: 0x04004332 RID: 17202
		public NKCUISlot m_Slot;

		// Token: 0x04004333 RID: 17203
		[Header("모드별 오브젝트")]
		public GameObject m_objDisable;

		// Token: 0x04004334 RID: 17204
		public GameObject m_objOpen;

		// Token: 0x04004335 RID: 17205
		public GameObject m_objCompleted;

		// Token: 0x04004336 RID: 17206
		public GameObject m_objBusy;

		// Token: 0x04004337 RID: 17207
		private NKCOfficeFunitureForgeSlot.Mode m_eCurrentMode;

		// Token: 0x04004338 RID: 17208
		private int m_ForgeSlotIndex = -1;

		// Token: 0x04004339 RID: 17209
		private NKMCraftSlotData m_NKMEquipCreationSlotData;

		// Token: 0x020014E7 RID: 5351
		private enum Mode
		{
			// Token: 0x04009F52 RID: 40786
			Disabled,
			// Token: 0x04009F53 RID: 40787
			Open,
			// Token: 0x04009F54 RID: 40788
			Completed,
			// Token: 0x04009F55 RID: 40789
			Busy
		}
	}
}
