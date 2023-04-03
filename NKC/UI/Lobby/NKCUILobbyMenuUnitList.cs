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
	// Token: 0x02000C1A RID: 3098
	public class NKCUILobbyMenuUnitList : NKCUILobbyMenuButtonBase
	{
		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06008F2D RID: 36653 RVA: 0x0030AF13 File Offset: 0x00309113
		// (set) Token: 0x06008F2E RID: 36654 RVA: 0x0030AF34 File Offset: 0x00309134
		public float UnitFillrate
		{
			get
			{
				if (!(this.m_imgUnitFillrate != null))
				{
					return 0f;
				}
				return this.m_imgUnitFillrate.fillAmount;
			}
			set
			{
				if (this.m_imgUnitFillrate != null)
				{
					this.m_imgUnitFillrate.fillAmount = value;
				}
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06008F2F RID: 36655 RVA: 0x0030AF50 File Offset: 0x00309150
		// (set) Token: 0x06008F30 RID: 36656 RVA: 0x0030AF71 File Offset: 0x00309171
		public float ShipFillrate
		{
			get
			{
				if (!(this.m_imgShipFillrate != null))
				{
					return 0f;
				}
				return this.m_imgShipFillrate.fillAmount;
			}
			set
			{
				if (this.m_imgShipFillrate != null)
				{
					this.m_imgShipFillrate.fillAmount = value;
				}
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06008F31 RID: 36657 RVA: 0x0030AF8D File Offset: 0x0030918D
		// (set) Token: 0x06008F32 RID: 36658 RVA: 0x0030AFAE File Offset: 0x003091AE
		public float OperatorFillrate
		{
			get
			{
				if (!(this.m_imgOperatorFillrate != null))
				{
					return 0f;
				}
				return this.m_imgOperatorFillrate.fillAmount;
			}
			set
			{
				if (this.m_imgOperatorFillrate != null)
				{
					this.m_imgOperatorFillrate.fillAmount = value;
				}
			}
		}

		// Token: 0x06008F33 RID: 36659 RVA: 0x0030AFCA File Offset: 0x003091CA
		public void Init()
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x0030B008 File Offset: 0x00309208
		protected override void ContentsUpdate(NKMUserData userData)
		{
			NKCUtil.SetGameobjectActive(this.m_objNotify, false);
			this.m_UnitCount = userData.m_ArmyData.GetCurrentUnitCount();
			this.m_UnitMaxCount = userData.m_ArmyData.m_MaxUnitCount;
			NKCUtil.SetLabelText(this.m_lbUnitCount, this.m_UnitCount.ToString());
			NKCUtil.SetLabelText(this.m_lbUnitMaxCount, "/" + this.m_UnitMaxCount.ToString());
			this.UnitFillrate = this.GetFillRate(this.m_UnitCount, this.m_UnitMaxCount);
			this.m_ShipCount = userData.m_ArmyData.GetCurrentShipCount();
			this.m_ShipMaxCount = userData.m_ArmyData.m_MaxShipCount;
			NKCUtil.SetLabelText(this.m_lbShipCount, this.m_ShipCount.ToString());
			NKCUtil.SetLabelText(this.m_lbShipMaxCount, "/" + this.m_ShipMaxCount.ToString());
			this.ShipFillrate = this.GetFillRate(this.m_ShipCount, this.m_ShipMaxCount);
			if (!NKCOperatorUtil.IsHide() && NKCOperatorUtil.IsActive())
			{
				this.m_OperatorCount = userData.m_ArmyData.GetCurrentOperatorCount();
				this.m_OperatorMaxCount = userData.m_ArmyData.m_MaxOperatorCount;
				NKCUtil.SetLabelText(this.m_lbOperatorCount, this.m_OperatorCount.ToString());
				NKCUtil.SetLabelText(this.m_lbOperatorMaxCount, "/" + this.m_OperatorMaxCount.ToString());
				this.OperatorFillrate = this.GetFillRate(this.m_OperatorCount, this.m_OperatorMaxCount);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_LOBBY_RIGHT_MENU_2_UNITLIST_OperatorCount, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_LOBBY_RIGHT_MENU_2_UNITLIST_OperatorCount, false);
			}
			this.SetNotify(false);
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x0030B1A4 File Offset: 0x003093A4
		public override void PlayAnimation(bool bActive)
		{
			this.m_lbUnitCount.DOKill(false);
			this.m_imgUnitFillrate.DOKill(false);
			this.m_lbShipCount.DOKill(false);
			this.m_imgShipFillrate.DOKill(false);
			bool flag = NKCOperatorUtil.IsActive() && !NKCOperatorUtil.IsHide();
			if (flag)
			{
				this.m_lbOperatorCount.DOKill(false);
				this.m_imgOperatorFillrate.DOKill(false);
			}
			if (bActive)
			{
				this.m_lbUnitCount.DOText(this.m_UnitCount.ToString(), 0.6f, false, ScrambleMode.Numerals, null).SetEase(Ease.InCubic);
				this.UnitFillrate = 0f;
				this.m_imgUnitFillrate.DOFillAmount(this.GetFillRate(this.m_UnitCount, this.m_UnitMaxCount), 0.6f).SetEase(Ease.InCubic);
				this.m_lbShipCount.DOText(this.m_ShipCount.ToString(), 0.6f, false, ScrambleMode.Numerals, null).SetEase(Ease.InCubic);
				this.ShipFillrate = 0f;
				this.m_imgShipFillrate.DOFillAmount(this.GetFillRate(this.m_ShipCount, this.m_ShipMaxCount), 0.6f).SetEase(Ease.InCubic);
				if (flag)
				{
					this.m_lbOperatorCount.DOText(this.m_OperatorCount.ToString(), 0.6f, false, ScrambleMode.Numerals, null).SetEase(Ease.InCubic);
					this.OperatorFillrate = 0f;
					this.m_imgOperatorFillrate.DOFillAmount(this.GetFillRate(this.m_OperatorCount, this.m_OperatorMaxCount), 0.6f).SetEase(Ease.InCubic);
					return;
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbUnitCount, this.m_UnitCount.ToString());
				this.UnitFillrate = this.GetFillRate(this.m_UnitCount, this.m_UnitMaxCount);
				NKCUtil.SetLabelText(this.m_lbShipCount, this.m_ShipCount.ToString());
				this.ShipFillrate = this.GetFillRate(this.m_ShipCount, this.m_ShipMaxCount);
				if (flag)
				{
					NKCUtil.SetLabelText(this.m_lbOperatorCount, this.m_OperatorCount.ToString());
					this.OperatorFillrate = this.GetFillRate(this.m_OperatorCount, this.m_OperatorMaxCount);
				}
			}
		}

		// Token: 0x06008F36 RID: 36662 RVA: 0x0030B3BA File Offset: 0x003095BA
		private float GetFillRate(int count, int maxCount)
		{
			if (maxCount == 0)
			{
				return 0f;
			}
			return (float)count / (float)maxCount;
		}

		// Token: 0x06008F37 RID: 36663 RVA: 0x0030B3CA File Offset: 0x003095CA
		private void OnButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
		}

		// Token: 0x04007C2D RID: 31789
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C2E RID: 31790
		public GameObject m_objNotify;

		// Token: 0x04007C2F RID: 31791
		public Text m_lbUnitCount;

		// Token: 0x04007C30 RID: 31792
		public Text m_lbUnitMaxCount;

		// Token: 0x04007C31 RID: 31793
		public Image m_imgUnitFillrate;

		// Token: 0x04007C32 RID: 31794
		public Text m_lbShipCount;

		// Token: 0x04007C33 RID: 31795
		public Text m_lbShipMaxCount;

		// Token: 0x04007C34 RID: 31796
		public Image m_imgShipFillrate;

		// Token: 0x04007C35 RID: 31797
		public Text m_lbOperatorCount;

		// Token: 0x04007C36 RID: 31798
		public Text m_lbOperatorMaxCount;

		// Token: 0x04007C37 RID: 31799
		public Image m_imgOperatorFillrate;

		// Token: 0x04007C38 RID: 31800
		private int m_UnitCount;

		// Token: 0x04007C39 RID: 31801
		private int m_UnitMaxCount;

		// Token: 0x04007C3A RID: 31802
		private int m_ShipCount;

		// Token: 0x04007C3B RID: 31803
		private int m_ShipMaxCount;

		// Token: 0x04007C3C RID: 31804
		private int m_OperatorCount;

		// Token: 0x04007C3D RID: 31805
		private int m_OperatorMaxCount;

		// Token: 0x04007C3E RID: 31806
		public GameObject m_NKM_UI_LOBBY_RIGHT_MENU_2_UNITLIST_OperatorCount;

		// Token: 0x04007C3F RID: 31807
		private const float m_fAnimTime = 0.6f;

		// Token: 0x04007C40 RID: 31808
		private const Ease m_eAnimEase = Ease.InCubic;
	}
}
