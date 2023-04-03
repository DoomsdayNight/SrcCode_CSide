using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C14 RID: 3092
	public class NKCUILobbyMenuInventory : NKCUILobbyMenuButtonBase
	{
		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06008F07 RID: 36615 RVA: 0x0030A494 File Offset: 0x00308694
		// (set) Token: 0x06008F08 RID: 36616 RVA: 0x0030A4B5 File Offset: 0x003086B5
		public float Fillrate
		{
			get
			{
				if (!(this.m_imgFillrate != null))
				{
					return 0f;
				}
				return this.m_imgFillrate.fillAmount;
			}
			set
			{
				if (this.m_imgFillrate != null)
				{
					this.m_imgFillrate.fillAmount = value;
				}
			}
		}

		// Token: 0x06008F09 RID: 36617 RVA: 0x0030A4D1 File Offset: 0x003086D1
		public void Init()
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
		}

		// Token: 0x06008F0A RID: 36618 RVA: 0x0030A510 File Offset: 0x00308710
		protected override void ContentsUpdate(NKMUserData userData)
		{
			NKCUtil.SetGameobjectActive(this.m_objNotify, false);
			this.m_MiscCount = userData.m_InventoryData.GetCountMiscExceptCurrency();
			this.m_EquipCount = userData.m_InventoryData.GetCountEquipItemTypes();
			this.m_EquipMaxCount = userData.m_InventoryData.m_MaxItemEqipCount;
			NKCUtil.SetLabelText(this.m_lbMiscCount, this.m_MiscCount.ToString());
			NKCUtil.SetLabelText(this.m_lbCount, this.m_EquipCount.ToString());
			NKCUtil.SetLabelText(this.m_lbMaxCount, "/" + this.m_EquipMaxCount.ToString());
			this.Fillrate = this.GetFillRate(this.m_EquipCount, this.m_EquipMaxCount);
			bool flag = false;
			foreach (NKMItemMiscData nkmitemMiscData in NKCScenManager.CurrentUserData().m_InventoryData.MiscItems.Values)
			{
				if (nkmitemMiscData.TotalCount > 0L)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmitemMiscData.ItemID);
					if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable())
					{
						flag = true;
						break;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objNotify, flag);
			this.SetNotify(flag);
		}

		// Token: 0x06008F0B RID: 36619 RVA: 0x0030A644 File Offset: 0x00308844
		public override void PlayAnimation(bool bActive)
		{
			this.m_lbCount.DOKill(false);
			this.m_imgFillrate.DOKill(false);
			this.m_lbMiscCount.DOKill(false);
			if (bActive)
			{
				this.m_lbMiscCount.DOText(this.m_MiscCount.ToString(), 0.6f, false, ScrambleMode.Numerals, null).SetEase(Ease.InCubic);
				this.m_lbCount.DOText(this.m_EquipCount.ToString(), 0.6f, false, ScrambleMode.Numerals, null).SetEase(Ease.InCubic);
				this.Fillrate = 0f;
				this.m_imgFillrate.DOFillAmount(this.GetFillRate(this.m_EquipCount, this.m_EquipMaxCount), 0.6f).SetEase(Ease.InCubic);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbMiscCount, this.m_MiscCount.ToString());
			NKCUtil.SetLabelText(this.m_lbCount, this.m_EquipCount.ToString());
			this.Fillrate = this.GetFillRate(this.m_EquipCount, this.m_EquipMaxCount);
		}

		// Token: 0x06008F0C RID: 36620 RVA: 0x0030A73E File Offset: 0x0030893E
		private float GetFillRate(int count, int maxCount)
		{
			if (maxCount == 0)
			{
				return 0f;
			}
			return (float)count / (float)maxCount;
		}

		// Token: 0x06008F0D RID: 36621 RVA: 0x0030A74E File Offset: 0x0030894E
		private void OnButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_INVENTORY, false);
		}

		// Token: 0x04007C06 RID: 31750
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C07 RID: 31751
		public GameObject m_objNotify;

		// Token: 0x04007C08 RID: 31752
		public Text m_lbMiscCount;

		// Token: 0x04007C09 RID: 31753
		public Text m_lbCount;

		// Token: 0x04007C0A RID: 31754
		public Text m_lbMaxCount;

		// Token: 0x04007C0B RID: 31755
		public Image m_imgFillrate;

		// Token: 0x04007C0C RID: 31756
		private int m_MiscCount;

		// Token: 0x04007C0D RID: 31757
		private int m_EquipCount;

		// Token: 0x04007C0E RID: 31758
		private int m_EquipMaxCount;

		// Token: 0x04007C0F RID: 31759
		private const float m_fAnimTime = 0.6f;

		// Token: 0x04007C10 RID: 31760
		private const Ease m_eAnimEase = Ease.InCubic;
	}
}
