using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007A5 RID: 1957
	public class NKCDiveGameSector : MonoBehaviour
	{
		// Token: 0x06004D08 RID: 19720 RVA: 0x00171C6F File Offset: 0x0016FE6F
		public NKMDiveSlot GetNKMDiveSlot()
		{
			return this.m_NKMDiveSlot;
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x00171C77 File Offset: 0x0016FE77
		public bool GetGrey()
		{
			return this.m_bGrey;
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x00171C7F File Offset: 0x0016FE7F
		public void SetSlotIndex(int index)
		{
			this.m_SlotIndex = index;
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x00171C88 File Offset: 0x0016FE88
		public int GetSlotIndex()
		{
			return this.m_SlotIndex;
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00171C90 File Offset: 0x0016FE90
		public void SetUISlotIndex(int index)
		{
			this.m_UISlotIndex = index;
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x00171C99 File Offset: 0x0016FE99
		public int GetUISlotIndex()
		{
			return this.m_UISlotIndex;
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x00171CA1 File Offset: 0x0016FEA1
		public void Init(NKCDiveGameSectorSet cNKCDiveGameSectorSet, NKCDiveGameSector.OnClickSector dOnClickSector = null)
		{
			this.m_NKCDiveGameSectorSet = cNKCDiveGameSectorSet;
			this.m_dOnClickSector = dOnClickSector;
			this.m_NKM_UI_DIVE_SECTOR_SLOT.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_DIVE_SECTOR_SLOT.PointerClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x00171CDD File Offset: 0x0016FEDD
		public int GetRealSetSize()
		{
			if (this.m_NKCDiveGameSectorSet != null)
			{
				return this.m_NKCDiveGameSectorSet.GetRealSetSize();
			}
			return 0;
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x00171CFC File Offset: 0x0016FEFC
		public void InvaldGrey()
		{
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP_Img, null);
			NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT_Img, null);
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET_TEXT, new Color(1f, 0.9254902f, 0.31764707f, 1f));
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN_TEXT, new Color(0.7372549f, 0.56078434f, 1f, 1f));
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE_TEXT, new Color(1f, 0.19215687f, 0.20784314f, 1f));
			this.m_bGrey = false;
			this.SetAlphaByReachable();
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x00171DF0 File Offset: 0x0016FFF0
		public void SetGrey()
		{
			this.InvaldGrey();
			Material orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Material>("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", "EP_THUMBNAIL_BLACK_AND_WHITE", false);
			if (orLoadAssetResource != null)
			{
				if (this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET_Img, orLoadAssetResource);
					this.m_bGrey = true;
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET_TEXT, new Color(1f, 1f, 1f, 1f));
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN_Img, orLoadAssetResource);
					this.m_bGrey = true;
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN_TEXT, new Color(1f, 1f, 1f, 1f));
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE_Img, orLoadAssetResource);
					this.m_bGrey = true;
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE_TEXT, new Color(1f, 1f, 1f, 1f));
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID_Img, orLoadAssetResource);
					this.m_bGrey = true;
				}
				if (this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM_Img, orLoadAssetResource);
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL_Img, orLoadAssetResource);
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER_Img, orLoadAssetResource);
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP_Img, orLoadAssetResource);
				}
				else if (this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT != null && this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT.activeSelf)
				{
					NKCUtil.SetImageMaterial(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT_Img, orLoadAssetResource);
				}
				if (this.m_bGrey)
				{
					this.m_CanvasGroup.alpha = 0.25f;
				}
			}
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x00172036 File Offset: 0x00170236
		public bool CheckSelectable()
		{
			return this.m_CanvasGroup.alpha >= 1f && !this.m_bGrey;
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x00172058 File Offset: 0x00170258
		public void PlayOpenAni(bool playSound)
		{
			if (this.m_NKM_UI_DIVE_SECTOR_SLOT_Animator != null)
			{
				this.m_NKM_UI_DIVE_SECTOR_SLOT_Animator.Play("NKM_UI_DIVE_SECTOR_SLOT_OPEN");
				if (playSound)
				{
					NKCSoundManager.PlaySound("FX_UI_DIVE_SLOT_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
				}
			}
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x001720AC File Offset: 0x001702AC
		public void SetSelected(bool bSet)
		{
			if (bSet)
			{
				if (this.m_NKMDiveSlot == null)
				{
					return;
				}
				if (NKCDiveManager.IsGauntletSectorType(this.m_NKMDiveSlot.SectorType))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET_SELECTED, true);
					return;
				}
				if (NKCDiveManager.IsReimannSectorType(this.m_NKMDiveSlot.SectorType))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN_SELECTED, true);
					return;
				}
				if (NKCDiveManager.IsPoincareSectorType(this.m_NKMDiveSlot.SectorType))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE_SELECTED, true);
					return;
				}
				if (NKCDiveManager.IsEuclidSectorType(this.m_NKMDiveSlot.SectorType))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID_SELECTED, true);
					return;
				}
				if (NKCDiveManager.IsBossSectorType(this.m_NKMDiveSlot.SectorType))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_BOSS_SELECTED, true);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET_SELECTED, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN_SELECTED, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE_SELECTED, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID_SELECTED, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_BOSS_SELECTED, bSet);
			}
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x0017219F File Offset: 0x0017039F
		public int GetDistance()
		{
			if (this.m_NKCDiveGameSectorSet)
			{
				return this.m_NKCDiveGameSectorSet.GetDistance();
			}
			return 0;
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x001721BB File Offset: 0x001703BB
		public Vector3 GetFinalPos()
		{
			if (this.m_NKCDiveGameSectorSet != null)
			{
				return this.m_NKCDiveGameSectorSet.transform.localPosition + base.transform.localPosition;
			}
			return base.transform.localPosition;
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x001721F8 File Offset: 0x001703F8
		private void SetAlphaByReachable()
		{
			bool flag = false;
			if (NKCScenManager.GetScenManager().GetMyUserData() != null)
			{
				NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
				if (diveGameData != null)
				{
					flag = (diveGameData.Player.PlayerBase.Distance + 1 == this.GetDistance());
				}
			}
			if (flag)
			{
				this.m_CanvasGroup.alpha = 1f;
				return;
			}
			this.m_CanvasGroup.alpha = 0.5f;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x00172268 File Offset: 0x00170468
		public void SetUI(NKMDiveSlot cNKMDiveSlot)
		{
			if (cNKMDiveSlot == null)
			{
				return;
			}
			this.m_NKMDiveSlot = cNKMDiveSlot;
			this.SetAlphaByReachable();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_START, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_START || cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_NONE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET, NKCDiveManager.IsGauntletSectorType(cNKMDiveSlot.SectorType));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET_BIG, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET_HARD);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN, NKCDiveManager.IsReimannSectorType(cNKMDiveSlot.SectorType));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN_BIG, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN_HARD);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE, NKCDiveManager.IsPoincareSectorType(cNKMDiveSlot.SectorType));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE_BIG, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE_HARD);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID, NKCDiveManager.IsEuclidSectorType(cNKMDiveSlot.SectorType));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_EUCLID, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_EUCLID_BIG, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID_HARD);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_BOSS, NKCDiveManager.IsBossSectorType(cNKMDiveSlot.SectorType));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_BOSS, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_ICON_BOSS_BIG, cNKMDiveSlot.SectorType == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS_HARD);
			if (NKCDiveManager.IsEuclidSectorType(cNKMDiveSlot.SectorType))
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM, NKCDiveManager.IsRandomEventType(cNKMDiveSlot.EventType));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_SAFETY, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT, false);
				NKCUtil.SetGameobjectActive(this.m_EUCLID_TYPE_NORMAL, cNKMDiveSlot.EventType != NKM_DIVE_EVENT_TYPE.NDET_ARTIFACT && cNKMDiveSlot.EventType != NKM_DIVE_EVENT_TYPE.NDET_REPAIR);
				NKCUtil.SetGameobjectActive(this.m_EUCLID_TYPE_ARTIFACT, cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_ARTIFACT);
				NKCUtil.SetGameobjectActive(this.m_EUCLID_TYPE_REPAIR, cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_REPAIR);
			}
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x0017248E File Offset: 0x0017068E
		private void OnClick()
		{
			if (this.m_dOnClickSector != null)
			{
				this.m_dOnClickSector(this, false);
			}
		}

		// Token: 0x04003CD2 RID: 15570
		private NKCDiveGameSector.OnClickSector m_dOnClickSector;

		// Token: 0x04003CD3 RID: 15571
		public Animator m_NKM_UI_DIVE_SECTOR_SLOT_Animator;

		// Token: 0x04003CD4 RID: 15572
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04003CD5 RID: 15573
		public NKCUIComStateButton m_NKM_UI_DIVE_SECTOR_SLOT;

		// Token: 0x04003CD6 RID: 15574
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_START;

		// Token: 0x04003CD7 RID: 15575
		[Header("건틀릿")]
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET;

		// Token: 0x04003CD8 RID: 15576
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET;

		// Token: 0x04003CD9 RID: 15577
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET_BIG;

		// Token: 0x04003CDA RID: 15578
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET_Img;

		// Token: 0x04003CDB RID: 15579
		public Text m_NKM_UI_DIVE_SECTOR_SLOT_ICON_GAUNTLET_TEXT;

		// Token: 0x04003CDC RID: 15580
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_GAUNTLET_SELECTED;

		// Token: 0x04003CDD RID: 15581
		[Header("리만")]
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN;

		// Token: 0x04003CDE RID: 15582
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN;

		// Token: 0x04003CDF RID: 15583
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN_BIG;

		// Token: 0x04003CE0 RID: 15584
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN_Img;

		// Token: 0x04003CE1 RID: 15585
		public Text m_NKM_UI_DIVE_SECTOR_SLOT_ICON_REIMANN_TEXT;

		// Token: 0x04003CE2 RID: 15586
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_REIMANN_SELECTED;

		// Token: 0x04003CE3 RID: 15587
		[Header("푸엥")]
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE;

		// Token: 0x04003CE4 RID: 15588
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE;

		// Token: 0x04003CE5 RID: 15589
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE_BIG;

		// Token: 0x04003CE6 RID: 15590
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE_Img;

		// Token: 0x04003CE7 RID: 15591
		public Text m_NKM_UI_DIVE_SECTOR_SLOT_ICON_POINCARE_TEXT;

		// Token: 0x04003CE8 RID: 15592
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_POINCARE_SELECTED;

		// Token: 0x04003CE9 RID: 15593
		[Header("유클리드")]
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID;

		// Token: 0x04003CEA RID: 15594
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_EUCLID;

		// Token: 0x04003CEB RID: 15595
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_EUCLID_BIG;

		// Token: 0x04003CEC RID: 15596
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID_Img;

		// Token: 0x04003CED RID: 15597
		public Text m_NKM_UI_DIVE_SECTOR_SLOT_ICON_EUCLID_TEXT;

		// Token: 0x04003CEE RID: 15598
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EUCLID_SELECTED;

		// Token: 0x04003CEF RID: 15599
		[Header("유클리드 섹터 타입")]
		public GameObject m_EUCLID_TYPE_NORMAL;

		// Token: 0x04003CF0 RID: 15600
		public GameObject m_EUCLID_TYPE_ARTIFACT;

		// Token: 0x04003CF1 RID: 15601
		public GameObject m_EUCLID_TYPE_REPAIR;

		// Token: 0x04003CF2 RID: 15602
		[Header("보스")]
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_BOSS;

		// Token: 0x04003CF3 RID: 15603
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_BOSS;

		// Token: 0x04003CF4 RID: 15604
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_ICON_BOSS_BIG;

		// Token: 0x04003CF5 RID: 15605
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_BOSS_SELECTED;

		// Token: 0x04003CF6 RID: 15606
		[Header("이벤트")]
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM;

		// Token: 0x04003CF7 RID: 15607
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RANDOM_Img;

		// Token: 0x04003CF8 RID: 15608
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL;

		// Token: 0x04003CF9 RID: 15609
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_RESCUE_SIGNAL_Img;

		// Token: 0x04003CFA RID: 15610
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER;

		// Token: 0x04003CFB RID: 15611
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_CONTAINER_Img;

		// Token: 0x04003CFC RID: 15612
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP;

		// Token: 0x04003CFD RID: 15613
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_LOST_SHIP_Img;

		// Token: 0x04003CFE RID: 15614
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_SAFETY;

		// Token: 0x04003CFF RID: 15615
		public GameObject m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT;

		// Token: 0x04003D00 RID: 15616
		public Image m_NKM_UI_DIVE_SECTOR_SLOT_EVENT_REPAIR_KIT_Img;

		// Token: 0x04003D01 RID: 15617
		private NKMDiveSlot m_NKMDiveSlot;

		// Token: 0x04003D02 RID: 15618
		private NKCDiveGameSectorSet m_NKCDiveGameSectorSet;

		// Token: 0x04003D03 RID: 15619
		private int m_SlotIndex = -1;

		// Token: 0x04003D04 RID: 15620
		private int m_UISlotIndex = -1;

		// Token: 0x04003D05 RID: 15621
		private bool m_bGrey;

		// Token: 0x02001464 RID: 5220
		// (Invoke) Token: 0x0600A898 RID: 43160
		public delegate void OnClickSector(NKCDiveGameSector cNKMDiveSlot, bool bByAuto);
	}
}
