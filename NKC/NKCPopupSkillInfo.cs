using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007FB RID: 2043
	public class NKCPopupSkillInfo : MonoBehaviour
	{
		// Token: 0x060050E0 RID: 20704 RVA: 0x00188051 File Offset: 0x00186251
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_NKCUISkillSlot.Init(null);
			this.m_Canvas.alpha = 1f;
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0018807B File Offset: 0x0018627B
		private void SetSkillType(NKM_SKILL_TYPE type)
		{
			this.m_lbSkillType.color = NKCUtil.GetSkillTypeColor(type);
			this.m_lbSkillType.text = NKCUtilString.GetSkillTypeName(type);
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0018809F File Offset: 0x0018629F
		private int GetSkillAttackCount(NKMUnitSkillTemplet unitTemplet)
		{
			if (unitTemplet != null)
			{
				return unitTemplet.m_AttackCount;
			}
			return 0;
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x001880AC File Offset: 0x001862AC
		public void OpenForUnit(NKMUnitSkillTemplet skillTemplet, int unitStarGradeMax, int unitLimitBreakLevel, int rearmGrade, bool bIsFuryType)
		{
			if (skillTemplet == null)
			{
				Debug.LogError("Skill Templet Null!!");
				return;
			}
			this.m_NKCUISkillSlot.SetData(skillTemplet, skillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER);
			this.m_NKCUISkillSlot.LockSkill(NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, unitLimitBreakLevel));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_obj_LAYOUT, true);
			NKCUtil.SetGameobjectActive(this.m_obj_NKM_UI_POPUP_SKILL_LV, true);
			NKCUtil.SetGameobjectActive(this.m_obj_NKM_UI_POPUP_SHIP_SKILL_LV, false);
			if (this.m_lbSkillName != null)
			{
				this.m_lbSkillName.text = skillTemplet.GetSkillName();
			}
			this.SetSkillType(skillTemplet.m_NKM_SKILL_TYPE);
			if (!NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, unitLimitBreakLevel))
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, true);
				NKCUtil.SetSkillUnlockStarRank(this.m_lstObjSkillLockStar, skillTemplet, unitStarGradeMax);
			}
			if (skillTemplet.m_fCooltimeSecond > 0f)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillCooldown, true);
				if (bIsFuryType)
				{
					NKCUtil.SetImageSprite(this.m_imgSkillCooldown, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_GAUNTLET_SMALL", false), false);
					NKCUtil.SetLabelText(this.m_lbSkillCooldown, string.Format(NKCUtilString.GET_STRING_COUNT_ONE_PARAM, skillTemplet.m_fCooltimeSecond));
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_imgSkillCooldown, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_TIME", false), false);
					NKCUtil.SetLabelText(this.m_lbSkillCooldown, string.Format(NKCUtilString.GET_STRING_TIME_SECOND_ONE_PARAM, skillTemplet.m_fCooltimeSecond));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillCooldown, false);
			}
			int skillAttackCount = this.GetSkillAttackCount(skillTemplet);
			if (skillAttackCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillAttackCount, true);
				NKCUtil.SetLabelText(this.m_lbSkillAttackCount, string.Format(NKCUtilString.GET_STRING_SKILL_ATTACK_COUNT_ONE_PARAM, skillAttackCount));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillAttackCount, false);
			}
			if (skillTemplet.m_Level == 1)
			{
				this.m_lbSkillDescription.text = skillTemplet.GetSkillDesc();
			}
			else
			{
				NKMUnitSkillTemplet skillTemplet2 = NKMUnitSkillManager.GetSkillTemplet(skillTemplet.m_ID, 1);
				if (skillTemplet2 != null)
				{
					this.m_lbSkillDescription.text = skillTemplet2.GetSkillDesc();
				}
			}
			this.UpdateUnitSkillDetail(skillTemplet, rearmGrade);
			if (this.m_rtSkillDescription != null)
			{
				this.m_rtSkillDescription.anchoredPosition = new Vector2(this.m_rtSkillDescription.anchoredPosition.x, 0f);
			}
			this.UpdateLeaderSkillUI(skillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER);
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x001882F4 File Offset: 0x001864F4
		private void UpdateUnitSkillDetail(NKMUnitSkillTemplet skillTemplet, int rearmGradeLv)
		{
			foreach (KeyValuePair<int, NKMUnitSkillTemplet> keyValuePair in NKMUnitSkillManager.GetSkillTempletContainer(skillTemplet.m_ID).dicTemplets)
			{
				if (keyValuePair.Value.m_Level != 1)
				{
					NKCUIComSkillLevelDetail nkcuicomSkillLevelDetail;
					if (this.m_stkSkillLevelDetail.Count <= 0)
					{
						nkcuicomSkillLevelDetail = UnityEngine.Object.Instantiate<NKCUIComSkillLevelDetail>(this.m_pfbSkillLevelDetail);
					}
					else
					{
						nkcuicomSkillLevelDetail = this.m_stkSkillLevelDetail.Pop();
					}
					if (null == nkcuicomSkillLevelDetail)
					{
						break;
					}
					nkcuicomSkillLevelDetail.gameObject.transform.SetParent(this.m_rtSkillInfoPanel);
					nkcuicomSkillLevelDetail.transform.localScale = Vector3.one;
					NKCUtil.SetGameobjectActive(nkcuicomSkillLevelDetail.gameObject, true);
					nkcuicomSkillLevelDetail.SetData(keyValuePair.Value.m_ID, keyValuePair.Value.m_Level <= skillTemplet.m_Level, keyValuePair.Value.m_Level);
					this.m_lstSkillLevelDetail.Add(nkcuicomSkillLevelDetail);
				}
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x00188408 File Offset: 0x00186608
		public void Clear()
		{
			for (int i = 0; i < this.m_lstSkillLevelDetail.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSkillLevelDetail[i], false);
				this.m_stkSkillLevelDetail.Push(this.m_lstSkillLevelDetail[i]);
			}
			this.m_lstSkillLevelDetail.Clear();
			while (this.m_stkSkillLevelDetail.Count > 0)
			{
				NKCUIComSkillLevelDetail nkcuicomSkillLevelDetail = this.m_stkSkillLevelDetail.Pop();
				if (nkcuicomSkillLevelDetail != null)
				{
					UnityEngine.Object.Destroy(nkcuicomSkillLevelDetail.gameObject);
				}
			}
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00188490 File Offset: 0x00186690
		public void OpenForShip(int slotIdx, int UnitID, int hasShipLv = 0)
		{
			int maxLevelShipID = NKMShipManager.GetMaxLevelShipID(UnitID);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(maxLevelShipID);
			if (unitTempletBase == null)
			{
				Debug.LogError(string.Format("함선 정보를 획득 할 수 없습니다. id({0}) 의 함선정보를 확인해주세요. ", maxLevelShipID));
				return;
			}
			NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase, slotIdx);
			if (shipSkillTempletByIndex == null)
			{
				Debug.Log(string.Format("함선 스킬정보를 확인 할 수 없습니다. shipID : {0} slot idx : {1}", maxLevelShipID, slotIdx));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbSkillName, shipSkillTempletByIndex.GetName());
			NKCUtil.SetLabelText(this.m_lbSkillType, NKCUtilString.GetSkillTypeName(shipSkillTempletByIndex.m_NKM_SKILL_TYPE));
			NKCUtil.SetLabelText(this.m_lbSkillDescription, shipSkillTempletByIndex.GetDesc());
			bool flag = false;
			if (this.m_NKCUISkillSlot != null)
			{
				this.m_NKCUISkillSlot.Cleanup();
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(UnitID);
				if (unitTempletBase2 != null)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex2 = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase2, slotIdx);
					flag = (hasShipLv == 0 || shipSkillTempletByIndex2 == null);
					this.m_NKCUISkillSlot.SetShipData(shipSkillTempletByIndex, flag);
				}
			}
			int num = UnitID % 1000;
			int num2 = 20;
			int num3 = 0;
			int num4 = 0;
			HashSet<string> hashSet = new HashSet<string>();
			for (int i = 0; i < 6; i++)
			{
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase((num2 + i + 1) * 1000 + num);
				if (unitTempletBase3 != null)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex3 = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase3, slotIdx);
					if (shipSkillTempletByIndex3 != null && this.m_lstShipSkillDetail.Count > hashSet.Count && !hashSet.Contains(shipSkillTempletByIndex3.m_ShipSkillStrID))
					{
						NKCUtil.SetLabelText(this.m_lstShipSkillDetail[hashSet.Count].RANK_COUNT, (i + 1).ToString());
						NKCUtil.SetLabelText(this.m_lstShipSkillDetail[hashSet.Count].SKILL_TEXT, shipSkillTempletByIndex3.GetBuildDesc());
						NKCUtil.SetImageSprite(this.m_lstShipSkillDetail[hashSet.Count].RANK_ICON, NKCUtil.GetSpriteCommonIConStar(i + 1), false);
						hashSet.Add(shipSkillTempletByIndex3.m_ShipSkillStrID);
						if (hasShipLv > 0 && i < hasShipLv)
						{
							num3 = hashSet.Count;
							NKCUtil.SetLabelText(this.m_lbSkillName, shipSkillTempletByIndex3.GetName());
							NKCUtil.SetLabelText(this.m_lbSkillDescription, shipSkillTempletByIndex3.GetDesc());
						}
						if (flag && num4 == 0)
						{
							num4 = i + 1;
						}
					}
				}
			}
			if (flag)
			{
				for (int j = 0; j < this.m_lstShipSkillDetail.Count; j++)
				{
					if (j < hashSet.Count)
					{
						NKCUtil.SetLabelTextColor(this.m_lstShipSkillDetail[j].RANK_COUNT, NKCUtil.GetColor("#FFFFFF"));
						CanvasGroup rank_SLOT_CANVAS = this.m_lstShipSkillDetail[j].RANK_SLOT_CANVAS;
						if (rank_SLOT_CANVAS != null)
						{
							rank_SLOT_CANVAS.alpha = 0.4f;
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstShipSkillDetail[j].RANK_SLOT, false);
					}
				}
				NKCUtil.SetStarRank(this.m_lstObjSkillLockStar, num4, 6);
			}
			else
			{
				for (int k = 0; k < this.m_lstShipSkillDetail.Count; k++)
				{
					if (hashSet.Count > k)
					{
						CanvasGroup rank_SLOT_CANVAS2 = this.m_lstShipSkillDetail[k].RANK_SLOT_CANVAS;
						if (rank_SLOT_CANVAS2 != null)
						{
							if (k + 1 == num3)
							{
								rank_SLOT_CANVAS2.alpha = 1f;
							}
							else
							{
								rank_SLOT_CANVAS2.alpha = 0.4f;
							}
						}
						if (k >= num3)
						{
							NKCUtil.SetLabelTextColor(this.m_lstShipSkillDetail[k].RANK_COUNT, NKCUtil.GetColor("#FFFFFF"));
						}
						else
						{
							NKCUtil.SetLabelTextColor(this.m_lstShipSkillDetail[k].RANK_COUNT, NKCUtil.GetColor("#FFCF3B"));
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstShipSkillDetail[k].RANK_SLOT, false);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, flag);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_obj_LAYOUT, false);
			NKCUtil.SetGameobjectActive(this.m_obj_NKM_UI_POPUP_SKILL_LV, false);
			NKCUtil.SetGameobjectActive(this.m_obj_NKM_UI_POPUP_SHIP_SKILL_LV, true);
			if (this.m_rtSkillDescription != null)
			{
				this.m_rtSkillDescription.anchoredPosition = new Vector2(this.m_rtSkillDescription.anchoredPosition.x, 0f);
			}
			this.UpdateLeaderSkillUI(false);
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x001888A8 File Offset: 0x00186AA8
		private void UpdateLeaderSkillUI(bool bLeader = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objLeaderSkillBG, bLeader);
			NKCUtil.SetGameobjectActive(this.m_objLeaderSkillIcon, bLeader);
			NKCUtil.SetGameobjectActive(this.m_objDot, !bLeader);
		}

		// Token: 0x04004127 RID: 16679
		public NKCUISkillSlot m_NKCUISkillSlot;

		// Token: 0x04004128 RID: 16680
		[Header("상세정보")]
		public CanvasGroup m_Canvas;

		// Token: 0x04004129 RID: 16681
		public Text m_lbSkillName;

		// Token: 0x0400412A RID: 16682
		public Text m_lbSkillType;

		// Token: 0x0400412B RID: 16683
		public GameObject m_objSkillLockRoot;

		// Token: 0x0400412C RID: 16684
		public List<GameObject> m_lstObjSkillLockStar;

		// Token: 0x0400412D RID: 16685
		public GameObject m_objSkillCooldown;

		// Token: 0x0400412E RID: 16686
		public Image m_imgSkillCooldown;

		// Token: 0x0400412F RID: 16687
		public Text m_lbSkillCooldown;

		// Token: 0x04004130 RID: 16688
		public GameObject m_objSkillAttackCount;

		// Token: 0x04004131 RID: 16689
		public Text m_lbSkillAttackCount;

		// Token: 0x04004132 RID: 16690
		public Text m_lbSkillDescription;

		// Token: 0x04004133 RID: 16691
		public RectTransform m_rtSkillDescription;

		// Token: 0x04004134 RID: 16692
		[Header("스킬 레벨 정보")]
		public RectTransform m_rtSkillInfoPanel;

		// Token: 0x04004135 RID: 16693
		public NKCUIComSkillLevelDetail m_pfbSkillLevelDetail;

		// Token: 0x04004136 RID: 16694
		private Stack<NKCUIComSkillLevelDetail> m_stkSkillLevelDetail = new Stack<NKCUIComSkillLevelDetail>();

		// Token: 0x04004137 RID: 16695
		private List<NKCUIComSkillLevelDetail> m_lstSkillLevelDetail = new List<NKCUIComSkillLevelDetail>();

		// Token: 0x04004138 RID: 16696
		public List<NKCPopupSkillInfo.ShipSkillSlot> m_lstShipSkillDetail;

		// Token: 0x04004139 RID: 16697
		[Header("스킬 표시")]
		public GameObject m_obj_LAYOUT;

		// Token: 0x0400413A RID: 16698
		public GameObject m_obj_NKM_UI_POPUP_SKILL_LV;

		// Token: 0x0400413B RID: 16699
		public GameObject m_obj_NKM_UI_POPUP_SHIP_SKILL_LV;

		// Token: 0x0400413C RID: 16700
		[Header("리더 스킬")]
		public GameObject m_objLeaderSkillBG;

		// Token: 0x0400413D RID: 16701
		public GameObject m_objDot;

		// Token: 0x0400413E RID: 16702
		public GameObject m_objLeaderSkillIcon;

		// Token: 0x020014B9 RID: 5305
		[Serializable]
		public struct ShipSkillSlot
		{
			// Token: 0x04009EEA RID: 40682
			public GameObject RANK_SLOT;

			// Token: 0x04009EEB RID: 40683
			public CanvasGroup RANK_SLOT_CANVAS;

			// Token: 0x04009EEC RID: 40684
			public Image RANK_ICON;

			// Token: 0x04009EED RID: 40685
			public Text RANK_COUNT;

			// Token: 0x04009EEE RID: 40686
			public Text SKILL_TEXT;
		}
	}
}
