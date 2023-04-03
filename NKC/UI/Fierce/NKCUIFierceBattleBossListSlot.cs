using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB2 RID: 2994
	public class NKCUIFierceBattleBossListSlot : MonoBehaviour
	{
		// Token: 0x06008A6F RID: 35439 RVA: 0x002F1284 File Offset: 0x002EF484
		public static NKCUIFierceBattleBossListSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_FIERCE_BATTLE_BOSS_LIST_SLOT", false, null);
			NKCUIFierceBattleBossListSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIFierceBattleBossListSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIFierceBattleBossListSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			return component;
		}

		// Token: 0x06008A70 RID: 35440 RVA: 0x002F1338 File Offset: 0x002EF538
		public void SetData(bool bSeason, int bossGroupdID, int playableBossLv)
		{
			Sprite sp = null;
			bool flag = false;
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(bossGroupdID))
			{
				foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[bossGroupdID])
				{
					if (nkmfierceBossGroupTemplet.Level == playableBossLv)
					{
						flag = nkmfierceBossGroupTemplet.UI_HellModeCheck;
						sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_boss_thumbnail", nkmfierceBossGroupTemplet.UI_BossFaceSlot, false);
						break;
					}
				}
				if (!flag)
				{
					using (List<Image>.Enumerator enumerator2 = this.m_bossImage.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Image image = enumerator2.Current;
							NKCUtil.SetImageSprite(image, sp, false);
						}
						goto IL_DF;
					}
				}
				foreach (Image image2 in this.m_bossNightImage)
				{
					NKCUtil.SetImageSprite(image2, sp, false);
				}
			}
			IL_DF:
			int clearLevel = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetClearLevel(bossGroupdID);
			for (int i = 0; i < this.m_lstClearImage.Count; i++)
			{
				Color color = NKCUIFierceBattleBossListSlot.NKCFierceUI.BossClearColorNotProgress;
				if (i < clearLevel || (i == 2 && clearLevel == 3))
				{
					color = NKCUIFierceBattleBossListSlot.NKCFierceUI.BossClearColorClear;
				}
				else if (i == clearLevel)
				{
					color = NKCUIFierceBattleBossListSlot.NKCFierceUI.BossClearColorCanTry;
				}
				NKCUtil.SetImageColor(this.m_lstClearImage[i], color);
			}
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_BASIC, bSeason && !flag);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE, bSeason && flag);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_END, !bSeason && !flag);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END, !bSeason && flag);
			NKCUtil.SetGameobjectActive(this.m_objNoneRecord, false);
			NKCUtil.SetGameobjectActive(this.m_objBossClearCnt, true);
			this.m_bSeason = bSeason;
			this.m_fierceBossGroupID = bossGroupdID;
		}

		// Token: 0x06008A71 RID: 35441 RVA: 0x002F1524 File Offset: 0x002EF724
		public void SetData(int bossGroupdID)
		{
			Sprite sp = null;
			bool flag = false;
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(bossGroupdID))
			{
				foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[bossGroupdID])
				{
					sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_boss_thumbnail", nkmfierceBossGroupTemplet.UI_BossFaceSlot, false);
				}
				if (!flag)
				{
					using (List<Image>.Enumerator enumerator2 = this.m_bossImage.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Image image = enumerator2.Current;
							NKCUtil.SetImageSprite(image, sp, false);
						}
						goto IL_C9;
					}
				}
				foreach (Image image2 in this.m_bossNightImage)
				{
					NKCUtil.SetImageSprite(image2, sp, false);
				}
			}
			IL_C9:
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_BASIC, true);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE, false);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_END, false);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END, false);
			NKCUtil.SetGameobjectActive(this.m_objBossClearCnt, false);
			this.m_bSeason = true;
			this.m_fierceBossGroupID = bossGroupdID;
		}

		// Token: 0x06008A72 RID: 35442 RVA: 0x002F166C File Offset: 0x002EF86C
		public void OnClicked(bool bSelected)
		{
			if (this.m_bSeason)
			{
				NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_Normal, !bSelected);
				NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_Select, bSelected);
				NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_BASIC_Normal, !bSelected);
				NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_BASIC_Select, bSelected);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_END_Normal, !bSelected);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_END_Select, bSelected);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END_Normal, !bSelected);
			NKCUtil.SetGameobjectActive(this.m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END_Select, bSelected);
		}

		// Token: 0x06008A73 RID: 35443 RVA: 0x002F16EE File Offset: 0x002EF8EE
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008A74 RID: 35444 RVA: 0x002F170D File Offset: 0x002EF90D
		public void SetHasRecord(bool bHas)
		{
			NKCUtil.SetGameobjectActive(this.m_objNoneRecord, !bHas);
		}

		// Token: 0x04007728 RID: 30504
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007729 RID: 30505
		[Header("격전지원 진행 가능 상태")]
		public GameObject m_BOSS_LIST_SLOT_BUTTON_BASIC;

		// Token: 0x0400772A RID: 30506
		public GameObject m_BOSS_LIST_SLOT_BUTTON_BASIC_Normal;

		// Token: 0x0400772B RID: 30507
		public GameObject m_BOSS_LIST_SLOT_BUTTON_BASIC_Select;

		// Token: 0x0400772C RID: 30508
		[Space]
		public GameObject m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE;

		// Token: 0x0400772D RID: 30509
		public GameObject m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_Normal;

		// Token: 0x0400772E RID: 30510
		public GameObject m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_Select;

		// Token: 0x0400772F RID: 30511
		[Header("격전지원 진행 불가 상태")]
		public GameObject m_BOSS_LIST_SLOT_BUTTON_END;

		// Token: 0x04007730 RID: 30512
		public GameObject m_BOSS_LIST_SLOT_BUTTON_END_Normal;

		// Token: 0x04007731 RID: 30513
		public GameObject m_BOSS_LIST_SLOT_BUTTON_END_Select;

		// Token: 0x04007732 RID: 30514
		[Space]
		public GameObject m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END;

		// Token: 0x04007733 RID: 30515
		public GameObject m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END_Normal;

		// Token: 0x04007734 RID: 30516
		public GameObject m_BOSS_LIST_SLOT_BUTTON_NIGHTMARE_END_Select;

		// Token: 0x04007735 RID: 30517
		[Header("보스 이미지")]
		public List<Image> m_bossImage;

		// Token: 0x04007736 RID: 30518
		[Space]
		public List<Image> m_bossNightImage;

		// Token: 0x04007737 RID: 30519
		public GameObject m_objBossClearCnt;

		// Token: 0x04007738 RID: 30520
		[Header("클리어 상태")]
		public List<Image> m_lstClearImage;

		// Token: 0x04007739 RID: 30521
		public int m_fierceBossGroupID;

		// Token: 0x0400773A RID: 30522
		[Header("기타")]
		public NKCUIComStateButton m_csbtnBtn;

		// Token: 0x0400773B RID: 30523
		public GameObject m_objNoneRecord;

		// Token: 0x0400773C RID: 30524
		private bool m_bSeason;

		// Token: 0x02001981 RID: 6529
		public static class NKCFierceUI
		{
			// Token: 0x170019BF RID: 6591
			// (get) Token: 0x0600B91C RID: 47388 RVA: 0x0036C650 File Offset: 0x0036A850
			public static Color BossClearColorNotProgress
			{
				get
				{
					return NKCUtil.GetColor("#818181");
				}
			}

			// Token: 0x170019C0 RID: 6592
			// (get) Token: 0x0600B91D RID: 47389 RVA: 0x0036C65C File Offset: 0x0036A85C
			public static Color BossClearColorClear
			{
				get
				{
					return NKCUtil.GetColor("#1DABE0");
				}
			}

			// Token: 0x170019C1 RID: 6593
			// (get) Token: 0x0600B91E RID: 47390 RVA: 0x0036C668 File Offset: 0x0036A868
			public static Color BossClearColorCanTry
			{
				get
				{
					return NKCUtil.GetColor("#FFCF3B");
				}
			}
		}
	}
}
