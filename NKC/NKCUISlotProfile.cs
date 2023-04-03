using System;
using ClientPacket.Common;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009EE RID: 2542
	[RequireComponent(typeof(NKCUISlot))]
	public class NKCUISlotProfile : MonoBehaviour
	{
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06006DE1 RID: 28129 RVA: 0x00240BCB File Offset: 0x0023EDCB
		private NKCUISlot Slot
		{
			get
			{
				if (this.m_slot == null)
				{
					this.m_slot = base.GetComponent<NKCUISlot>();
					this.m_slot.SetOnClick(new NKCUISlot.OnClick(this.OnClickSlot));
				}
				return this.m_slot;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06006DE2 RID: 28130 RVA: 0x00240C04 File Offset: 0x0023EE04
		// (set) Token: 0x06006DE3 RID: 28131 RVA: 0x00240C0C File Offset: 0x0023EE0C
		public int FrameID { get; private set; }

		// Token: 0x06006DE4 RID: 28132 RVA: 0x00240C15 File Offset: 0x0023EE15
		public void Init()
		{
			this.Slot.Init();
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x00240C24 File Offset: 0x0023EE24
		public void SetProfiledata(NKMCommonProfile profile, NKCUISlotProfile.OnClick onClick)
		{
			this.Slot.SetUnitData(profile.mainUnitId, 1, profile.mainUnitSkinId, false, false, false, new NKCUISlot.OnClick(this.OnClickSlot));
			NKCUtil.SetGameobjectActive(this.Slot.m_imgUpperRightIcon, false);
			this.dOnClick = onClick;
			this.SetProfileFrame(profile.frameId);
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x00240C7C File Offset: 0x0023EE7C
		public void SetProfiledata(int unitID, int skinID, int frameID, NKCUISlotProfile.OnClick onClick)
		{
			this.Slot.SetUnitData(unitID, 1, skinID, false, false, false, new NKCUISlot.OnClick(this.OnClickSlot));
			NKCUtil.SetGameobjectActive(this.Slot.m_imgUpperRightIcon, false);
			this.dOnClick = onClick;
			this.SetProfileFrame(frameID);
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x00240CC8 File Offset: 0x0023EEC8
		public void SetProfiledata(NKMUserProfileData cNKMUserProfileData, NKCUISlotProfile.OnClick onClick)
		{
			this.Slot.SetUnitData(cNKMUserProfileData.commonProfile.mainUnitId, 1, cNKMUserProfileData.commonProfile.mainUnitSkinId, false, false, false, new NKCUISlot.OnClick(this.OnClickSlot));
			NKCUtil.SetGameobjectActive(this.Slot.m_imgUpperRightIcon, false);
			this.dOnClick = onClick;
			this.SetProfileFrame(cNKMUserProfileData.commonProfile.frameId);
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x00240D2F File Offset: 0x0023EF2F
		public void SetSelected(bool value)
		{
			this.Slot.SetSelected(value);
		}

		// Token: 0x06006DE9 RID: 28137 RVA: 0x00240D3D File Offset: 0x0023EF3D
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCUISlotProfile.OnClick onClick = this.dOnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this, this.FrameID);
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x00240D58 File Offset: 0x0023EF58
		private void SetProfileFrame(int frameID)
		{
			if (frameID != 0)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(frameID);
				if (itemMiscTempletByID.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_SELFIE_FRAME)
				{
					Debug.LogError("Not a Frame Item!! ItemID : " + frameID.ToString());
					NKCUtil.SetGameobjectActive(this.m_imgBorder, false);
					return;
				}
				NKCUtil.SetImageSprite(this.m_imgBorder, NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID), false);
				NKCUtil.SetGameobjectActive(this.m_imgBorder, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgBorder, false);
			}
			this.FrameID = frameID;
		}

		// Token: 0x04005969 RID: 22889
		public Image m_imgBorder;

		// Token: 0x0400596A RID: 22890
		private NKCUISlot m_slot;

		// Token: 0x0400596B RID: 22891
		private NKCUISlotProfile.OnClick dOnClick;

		// Token: 0x02001700 RID: 5888
		// (Invoke) Token: 0x0600B1FE RID: 45566
		public delegate void OnClick(NKCUISlotProfile slot, int frameID);
	}
}
