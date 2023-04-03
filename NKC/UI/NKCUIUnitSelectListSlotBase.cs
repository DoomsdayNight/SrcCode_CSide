using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009FD RID: 2557
	public abstract class NKCUIUnitSelectListSlotBase : MonoBehaviour
	{
		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06006F49 RID: 28489 RVA: 0x0024CACA File Offset: 0x0024ACCA
		public NKMUnitData NKMUnitData
		{
			get
			{
				return this.m_NKMUnitData;
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06006F4A RID: 28490 RVA: 0x0024CAD2 File Offset: 0x0024ACD2
		public NKMUnitTempletBase NKMUnitTempletBase
		{
			get
			{
				return this.m_NKMUnitTempletBase;
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06006F4B RID: 28491 RVA: 0x0024CADA File Offset: 0x0024ACDA
		public NKMOperator NKMOperatorData
		{
			get
			{
				return this.m_OperatorData;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06006F4C RID: 28492 RVA: 0x0024CAE2 File Offset: 0x0024ACE2
		protected virtual NKCResourceUtility.eUnitResourceType UseResourceType
		{
			get
			{
				return NKCResourceUtility.eUnitResourceType.FACE_CARD;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06006F4D RID: 28493 RVA: 0x0024CAE5 File Offset: 0x0024ACE5
		public int PowerCache
		{
			get
			{
				return this.m_iPowerCache;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06006F4E RID: 28494 RVA: 0x0024CAED File Offset: 0x0024ACED
		public NKCUIUnitSelectList.eUnitSlotSelectState UnitSelectState
		{
			get
			{
				return this.m_eUnitSelectState;
			}
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x0024CAF5 File Offset: 0x0024ACF5
		public void SetEnableShowBan(bool bSet)
		{
			this.m_bEnableShowBan = bSet;
			if (!bSet)
			{
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
			}
		}

		// Token: 0x06006F50 RID: 28496 RVA: 0x0024CB0D File Offset: 0x0024AD0D
		public void SetBanDataType(NKCBanManager.BAN_DATA_TYPE BanDataType)
		{
			this.m_eBanDataType = BanDataType;
		}

		// Token: 0x06006F51 RID: 28497 RVA: 0x0024CB16 File Offset: 0x0024AD16
		public void SetEnableShowUpUnit(bool bSet)
		{
			this.m_bEnableShowUpUnit = bSet;
			if (!bSet)
			{
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
			}
		}

		// Token: 0x06006F52 RID: 28498 RVA: 0x0024CB2E File Offset: 0x0024AD2E
		public void SetEnableShowCastingBanSelectedObject(bool bSet)
		{
			this.m_bEnableShowCastingBan = bSet;
		}

		// Token: 0x06006F53 RID: 28499 RVA: 0x0024CB38 File Offset: 0x0024AD38
		public void Init(bool resetLocalScale = false)
		{
			if (resetLocalScale)
			{
				base.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
			}
			this.m_cbtnSlot.PointerClick.RemoveAllListeners();
			this.m_cbtnSlot.PointerClick.AddListener(new UnityAction(this.OnClick));
			if (this.m_btnHave != null)
			{
				this.m_btnHave.PointerClick.RemoveAllListeners();
				this.m_btnHave.PointerClick.AddListener(new UnityAction(this.OnClickHave));
			}
		}

		// Token: 0x06006F54 RID: 28500 RVA: 0x0024CBC8 File Offset: 0x0024ADC8
		protected virtual void SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode mode)
		{
			NKCUtil.SetGameobjectActive(this.m_objCardRoot, mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character);
			NKCUtil.SetGameobjectActive(this.m_objSlotStatus, mode > NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character);
			if (mode != NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character)
			{
				this.SetLock(false, true);
				this.SetFavorite(false);
			}
			if (this.m_imgSlotStatus != null)
			{
				switch (mode)
				{
				case NKCUIUnitSelectListSlotBase.eUnitSlotMode.Empty:
					this.m_imgSlotStatus.sprite = this.m_spEmpty;
					break;
				case NKCUIUnitSelectListSlotBase.eUnitSlotMode.Denied:
					this.m_imgSlotStatus.sprite = this.m_spDenied;
					break;
				case NKCUIUnitSelectListSlotBase.eUnitSlotMode.SelectResource:
					this.m_imgSlotStatus.sprite = this.m_spSelectResource;
					break;
				case NKCUIUnitSelectListSlotBase.eUnitSlotMode.Closed:
					this.m_imgSlotStatus.sprite = this.m_spClosed;
					break;
				case NKCUIUnitSelectListSlotBase.eUnitSlotMode.Add:
					this.m_imgSlotStatus.sprite = this.m_spAdd;
					break;
				}
			}
			if (mode != NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character)
			{
				this.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				this.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			}
		}

		// Token: 0x06006F55 RID: 28501 RVA: 0x0024CCAA File Offset: 0x0024AEAA
		protected Sprite GetBGSprite(NKM_UNIT_GRADE unitGrade)
		{
			switch (unitGrade)
			{
			default:
				return this.m_spBG_N;
			case NKM_UNIT_GRADE.NUG_R:
				return this.m_spBG_R;
			case NKM_UNIT_GRADE.NUG_SR:
				return this.m_spBG_SR;
			case NKM_UNIT_GRADE.NUG_SSR:
				return this.m_spBG_SSR;
			}
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x0024CCDD File Offset: 0x0024AEDD
		protected Sprite GetRaritySprite(NKM_UNIT_GRADE unitGrade)
		{
			switch (unitGrade)
			{
			default:
				return this.m_spN;
			case NKM_UNIT_GRADE.NUG_R:
				return this.m_spR;
			case NKM_UNIT_GRADE.NUG_SR:
				return this.m_spSR;
			case NKM_UNIT_GRADE.NUG_SSR:
				return this.m_spSSR;
			}
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x0024CD10 File Offset: 0x0024AF10
		public virtual void SetData(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			this.RestoreSprite();
			this.dOnSelectThisSlot = onSelectThisSlot;
			bool flag = this.m_NKMUnitData == null || cNKMUnitData == null || this.m_NKMUnitData.m_UnitUID != cNKMUnitData.m_UnitUID || cNKMUnitData.m_UnitID != this.m_UnitID || this.m_SkinID != cNKMUnitData.m_SkinID;
			this.m_NKMUnitData = cNKMUnitData;
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character);
			if (cNKMUnitData != null)
			{
				this.m_UnitID = cNKMUnitData.m_UnitID;
				this.m_SkinID = cNKMUnitData.m_SkinID;
				this.m_NKMUnitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
				if (flag)
				{
					this.SetTempletData(this.m_NKMUnitTempletBase);
					Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(this.UseResourceType, this.m_NKMUnitData);
					if (sprite != null)
					{
						this.m_imgUnitPortrait.sprite = sprite;
						this.m_imgUnitPortrait.enabled = true;
					}
					else
					{
						this.m_imgUnitPortrait.enabled = false;
					}
				}
				if (this.m_lbLevel != null)
				{
					NKCUIComTextUnitLevel nkcuicomTextUnitLevel = this.m_lbLevel as NKCUIComTextUnitLevel;
					if (nkcuicomTextUnitLevel != null)
					{
						nkcuicomTextUnitLevel.SetLevel(cNKMUnitData, 0, Array.Empty<Text>());
					}
					else
					{
						this.m_lbLevel.text = cNKMUnitData.m_UnitLevel.ToString();
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objMaxExp, NKCExpManager.IsUnitMaxLevel(cNKMUnitData));
				Sprite orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(cNKMUnitData, true);
				NKCUtil.SetImageSprite(this.m_imgAttackType, orLoadUnitAttackTypeIcon, true);
				Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(this.m_NKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgClassType, orLoadUnitRoleIcon, true);
				this.SetDeckIndex(deckIndex);
				if (NKCScenManager.CurrentUserData() != null)
				{
					this.m_iPowerCache = this.m_NKMUnitData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null);
				}
				this.SetLock(this.m_NKMUnitData.m_bLock, false);
				this.SetFavorite(this.m_NKMUnitData);
				NKCUITacticUpdateLevelSlot tacticUpdateSlot = this.m_tacticUpdateSlot;
				if (tacticUpdateSlot != null)
				{
					tacticUpdateSlot.SetLevel(this.m_NKMUnitData.tacticLevel, false);
				}
			}
			this.m_cbtnSlot.Select(false, false, true);
			if (this.m_layoutElement != null)
			{
				this.m_layoutElement.enabled = bEnableLayoutElement;
			}
			this.m_eUnitSlotState = NKCUnitSortSystem.eUnitState.NONE;
			this.m_eUnitSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlight, false);
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlightCastingBan, false);
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objNew, false);
			this.SetContractedUnitMark(false);
		}

		// Token: 0x06006F58 RID: 28504 RVA: 0x0024CF58 File Offset: 0x0024B158
		public virtual void SetData(NKMOperator operatorData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			this.RestoreSprite();
			this.dOnSelectThisOperatorSlot = onSelectThisSlot;
			bool flag = this.m_OperatorData == null || operatorData == null || this.m_OperatorData.uid != operatorData.uid || this.m_OperatorData.id != this.m_OperatorID;
			this.m_OperatorData = operatorData;
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character);
			if (operatorData != null)
			{
				this.m_OperatorID = operatorData.id;
				this.m_NKMUnitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
				if (flag)
				{
					this.SetTempletData(this.m_NKMUnitTempletBase);
					Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(this.UseResourceType, operatorData);
					if (sprite != null)
					{
						this.m_imgUnitPortrait.sprite = sprite;
						this.m_imgUnitPortrait.enabled = true;
					}
					else
					{
						this.m_imgUnitPortrait.enabled = false;
					}
				}
				if (this.m_lbLevel != null)
				{
					this.m_lbLevel.text = operatorData.level.ToString();
				}
				NKCUtil.SetGameobjectActive(this.m_objMaxExp, NKCOperatorUtil.IsMaximumLevel(operatorData.level));
				this.SetDeckIndex(deckIndex);
				if (NKCScenManager.CurrentUserData() != null)
				{
					this.m_iPowerCache = operatorData.CalculateOperatorOperationPower();
				}
				this.SetLock(operatorData.bLock, false);
				this.SetFavorite(operatorData);
			}
			this.m_cbtnSlot.Select(false, false, true);
			if (this.m_layoutElement != null)
			{
				this.m_layoutElement.enabled = bEnableLayoutElement;
			}
			this.m_eUnitSlotState = NKCUnitSortSystem.eUnitState.NONE;
			this.m_eUnitSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlight, false);
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlightCastingBan, false);
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objNew, false);
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			this.SetContractedUnitMark(false);
		}

		// Token: 0x06006F59 RID: 28505 RVA: 0x0024D108 File Offset: 0x0024B308
		public void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			this.SetData(templetBase, levelToDisplay, 0, bEnableLayoutElement, onSelectThisSlot);
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x0024D116 File Offset: 0x0024B316
		public void SetOperatorData(NKMUnitTempletBase templetBase, int levelToDisplay, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			this.SetData(templetBase, levelToDisplay, 0, bEnableLayoutElement, onSelectThisSlot);
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x0024D124 File Offset: 0x0024B324
		public virtual void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			this.dOnSelectThisSlot = onSelectThisSlot;
			this.SetData(templetBase, levelToDisplay, skinID, bEnableLayoutElement);
		}

		// Token: 0x06006F5C RID: 28508 RVA: 0x0024D139 File Offset: 0x0024B339
		public virtual void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			this.dOnSelectThisOperatorSlot = onSelectThisSlot;
			this.SetData(templetBase, levelToDisplay, skinID, bEnableLayoutElement);
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x0024D150 File Offset: 0x0024B350
		private void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement)
		{
			this.RestoreSprite();
			this.m_NKMUnitData = null;
			this.m_OperatorData = null;
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character);
			if (templetBase != null)
			{
				this.m_UnitID = templetBase.m_UnitID;
				this.m_SkinID = skinID;
				bool flag = this.m_NKMUnitTempletBase == null || this.m_NKMUnitTempletBase.m_UnitID != templetBase.m_UnitID || this.m_SkinID != skinID;
				this.m_NKMUnitTempletBase = templetBase;
				if (flag)
				{
					this.SetTempletData(this.m_NKMUnitTempletBase);
					if (this.m_imgUnitPortrait != null)
					{
						Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(this.UseResourceType, templetBase.m_UnitID, skinID);
						if (sprite != null)
						{
							this.m_imgUnitPortrait.sprite = sprite;
							this.m_imgUnitPortrait.enabled = true;
						}
						else
						{
							this.m_imgUnitPortrait.enabled = false;
						}
					}
				}
				if (this.m_lbLevel != null)
				{
					NKCUIComTextUnitLevel nkcuicomTextUnitLevel = this.m_lbLevel as NKCUIComTextUnitLevel;
					if (nkcuicomTextUnitLevel != null)
					{
						nkcuicomTextUnitLevel.SetText(levelToDisplay.ToString(), false, Array.Empty<Text>());
					}
					else
					{
						this.m_lbLevel.text = levelToDisplay.ToString();
					}
					if (this.m_lbLevel.transform.parent != null)
					{
						NKCUtil.SetGameobjectActive(this.m_lbLevel.transform.parent, levelToDisplay > 0);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objMaxExp, NKCExpManager.GetUnitMaxLevel(templetBase, 0) <= levelToDisplay);
				Sprite orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(templetBase, true);
				NKCUtil.SetImageSprite(this.m_imgAttackType, orLoadUnitAttackTypeIcon, true);
				Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(this.m_NKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgClassType, orLoadUnitRoleIcon, true);
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(templetBase.m_StarGradeMax - 3, templetBase.m_StarGradeMax, false);
				}
				this.SetDeckIndex(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				this.m_iPowerCache = 0;
			}
			this.SetLock(false, false);
			this.SetFavorite(false);
			this.m_cbtnSlot.Select(false, false, true);
			if (this.m_layoutElement != null)
			{
				this.m_layoutElement.enabled = bEnableLayoutElement;
			}
			this.m_eUnitSlotState = NKCUnitSortSystem.eUnitState.NONE;
			this.m_eUnitSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlight, false);
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlightCastingBan, false);
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objNew, false);
			this.SetContractedUnitMark(false);
			NKCUITacticUpdateLevelSlot tacticUpdateSlot = this.m_tacticUpdateSlot;
			if (tacticUpdateSlot == null)
			{
				return;
			}
			tacticUpdateSlot.SetLevel(0, false);
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x0024D3A4 File Offset: 0x0024B5A4
		public virtual void SetDataForBan(NKMUnitTempletBase templetBase, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bUp = false, bool bSetOriginalCost = false)
		{
		}

		// Token: 0x06006F5F RID: 28511 RVA: 0x0024D3A6 File Offset: 0x0024B5A6
		public virtual void SetDataForBan(NKMOperator operData, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
		}

		// Token: 0x06006F60 RID: 28512 RVA: 0x0024D3A8 File Offset: 0x0024B5A8
		public virtual void SetDataForContractSelection(NKMUnitData cNKMUnitData, bool bHave = true)
		{
		}

		// Token: 0x06006F61 RID: 28513 RVA: 0x0024D3AA File Offset: 0x0024B5AA
		public virtual void SetDataForContractSelection(NKMOperator cNKMOperData)
		{
		}

		// Token: 0x06006F62 RID: 28514 RVA: 0x0024D3AC File Offset: 0x0024B5AC
		public virtual void SetDataForCollection(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bEnable = false)
		{
		}

		// Token: 0x06006F63 RID: 28515 RVA: 0x0024D3AE File Offset: 0x0024B5AE
		public virtual void SetDataForCollection(NKMOperator cNKMUnitData, NKMDeckIndex deckIndex, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot, bool bEnable = false)
		{
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x0024D3B0 File Offset: 0x0024B5B0
		public virtual void SetDataForRearm(NKMUnitData unitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bShowEqup = true, bool bShowLevel = false, bool bUnable = false)
		{
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x0024D3B4 File Offset: 0x0024B5B4
		protected void ProcessBanUIForUnit()
		{
			if (this.m_NKMUnitTempletBase != null)
			{
				if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(this.m_NKMUnitTempletBase.m_UnitID, this.m_eBanDataType))
				{
					NKCUtil.SetGameobjectActive(this.m_objBan, true);
					int unitBanLevel = NKCBanManager.GetUnitBanLevel(this.m_NKMUnitTempletBase.m_UnitID, this.m_eBanDataType);
					NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, unitBanLevel));
					this.m_lbBanLevel.color = Color.red;
					return;
				}
				if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(this.m_NKMUnitTempletBase.m_UnitID))
				{
					NKCUtil.SetGameobjectActive(this.m_objBan, true);
					int unitUpLevel = NKCBanManager.GetUnitUpLevel(this.m_NKMUnitTempletBase.m_UnitID);
					NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_UP_LEVEL_ONE_PARAM, unitUpLevel));
					this.m_lbBanLevel.color = NKCBanManager.UP_COLOR;
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
			}
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x0024D4AC File Offset: 0x0024B6AC
		protected void ProcessBanUIForOperator()
		{
			if (this.m_NKMUnitTempletBase != null)
			{
				if (this.m_bEnableShowBan && NKCBanManager.IsBanOperator(this.m_NKMUnitTempletBase.m_UnitID, this.m_eBanDataType))
				{
					NKCUtil.SetGameobjectActive(this.m_objBan, true);
					int operBanLevel = NKCBanManager.GetOperBanLevel(this.m_NKMUnitTempletBase.m_UnitID, this.m_eBanDataType);
					NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, operBanLevel));
					this.m_lbBanLevel.color = Color.red;
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
			}
		}

		// Token: 0x06006F67 RID: 28519 RVA: 0x0024D53C File Offset: 0x0024B73C
		protected virtual void SetTempletData(NKMUnitTempletBase templetBase)
		{
			if (this.m_imgBG != null)
			{
				this.m_imgBG.sprite = this.GetBGSprite(templetBase.m_NKM_UNIT_GRADE);
			}
			if (this.m_imgRarity != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgRarity, true);
				this.m_imgRarity.sprite = this.GetRaritySprite(templetBase.m_NKM_UNIT_GRADE);
			}
			NKCUtil.SetGameobjectActive(this.m_objRearm, this.m_NKMUnitTempletBase.IsRearmUnit);
			NKCUtil.SetAwakenFX(this.m_animAwakenFX, templetBase);
			NKCUtil.SetLabelText(this.m_lbName, templetBase.GetUnitName());
		}

		// Token: 0x06006F68 RID: 28520 RVA: 0x0024D5D2 File Offset: 0x0024B7D2
		public void SetDeckIndex(NKMDeckIndex deckIndex)
		{
			this.m_DeckIndex = deckIndex;
			NKCUtil.SetGameobjectActive(this.m_objShipNumberRoot, deckIndex.m_eDeckType > NKM_DECK_TYPE.NDT_NONE);
			NKCUtil.SetLabelText(this.m_lbShipNumber, NKCUtilString.GetDeckNumberString(deckIndex));
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x0024D600 File Offset: 0x0024B800
		public virtual void SetLock(bool bLocked, bool bBig = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objLocked, bLocked);
			NKCUtil.SetGameobjectActive(this.m_objLockBig, bLocked && bBig);
		}

		// Token: 0x06006F6A RID: 28522 RVA: 0x0024D61C File Offset: 0x0024B81C
		public virtual void SetFavorite(NKMUnitData unitData)
		{
			this.SetFavorite(unitData != null && unitData.isFavorite);
		}

		// Token: 0x06006F6B RID: 28523 RVA: 0x0024D630 File Offset: 0x0024B830
		public virtual void SetFavorite(NKMOperator operatorData)
		{
			this.SetFavorite(false);
		}

		// Token: 0x06006F6C RID: 28524 RVA: 0x0024D639 File Offset: 0x0024B839
		public virtual void SetFavorite(bool bFavorite)
		{
			NKCUtil.SetGameobjectActive(this.m_objFavorite, bFavorite);
		}

		// Token: 0x06006F6D RID: 28525 RVA: 0x0024D647 File Offset: 0x0024B847
		public virtual void SetDelete(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objDelete, bSet);
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x0024D655 File Offset: 0x0024B855
		public void SetChecked(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objChecked, bSet);
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x0024D664 File Offset: 0x0024B864
		public void SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode mode, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisOperatorSlot = null)
		{
			if (mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character)
			{
				Debug.LogWarning("Dont's use Setmode(Character), use SetData instead");
				return;
			}
			this.m_NKMUnitData = null;
			this.m_OperatorData = null;
			this.m_DeckIndex = NKMDeckIndex.None;
			this.SetMode(mode);
			this.dOnSelectThisSlot = onSelectThisSlot;
			this.dOnSelectThisOperatorSlot = onSelectThisOperatorSlot;
			if (this.m_layoutElement != null)
			{
				this.m_layoutElement.enabled = bEnableLayoutElement;
			}
			this.ClearTouchHoldEvent();
			NKCUtil.SetAwakenFX(this.m_animAwakenFX, null);
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x0024D6DC File Offset: 0x0024B8DC
		public void SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode mode, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			if (mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character)
			{
				Debug.LogWarning("Dont's use Setmode(Character), use SetData instead");
				return;
			}
			this.m_NKMUnitData = null;
			this.m_OperatorData = null;
			this.m_DeckIndex = NKMDeckIndex.None;
			this.SetMode(mode);
			this.dOnSelectThisOperatorSlot = onSelectThisSlot;
			if (this.m_layoutElement != null)
			{
				this.m_layoutElement.enabled = bEnableLayoutElement;
			}
			this.ClearTouchHoldEvent();
			NKCUtil.SetAwakenFX(this.m_animAwakenFX, null);
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x0024D74A File Offset: 0x0024B94A
		public void SetClosed(bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Closed, bEnableLayoutElement, onSelectThisSlot, null);
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x0024D756 File Offset: 0x0024B956
		public virtual void SetEmpty(bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisOperatorSlot = null)
		{
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Empty, bEnableLayoutElement, onSelectThisSlot, onSelectThisOperatorSlot);
		}

		// Token: 0x06006F73 RID: 28531 RVA: 0x0024D762 File Offset: 0x0024B962
		public void SetDenied(bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Denied, bEnableLayoutElement, onSelectThisSlot, null);
		}

		// Token: 0x06006F74 RID: 28532 RVA: 0x0024D76E File Offset: 0x0024B96E
		public void SetSelectResource(bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			this.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.SelectResource, bEnableLayoutElement, onSelectThisSlot, null);
		}

		// Token: 0x06006F75 RID: 28533 RVA: 0x0024D77C File Offset: 0x0024B97C
		public virtual void SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState eUnitSelectState)
		{
			this.m_eUnitSelectState = eUnitSelectState;
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlight, !this.m_bEnableShowCastingBan && this.m_eUnitSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
			NKCUtil.SetGameobjectActive(this.m_objSelectedSlotHighlightCastingBan, this.m_bEnableShowCastingBan && this.m_eUnitSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, this.m_eUnitSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE);
			NKCUtil.SetGameobjectActive(this.m_objDelete, this.m_eUnitSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.DELETE);
		}

		// Token: 0x06006F76 RID: 28534 RVA: 0x0024D7F8 File Offset: 0x0024B9F8
		public virtual void SetSlotState(NKCUnitSortSystem.eUnitState eUnitSlotState)
		{
			this.m_eUnitSlotState = eUnitSlotState;
			switch (this.m_eUnitSlotState)
			{
			case NKCUnitSortSystem.eUnitState.DUPLICATE:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCUtilString.GET_STRING_UNIT_SELECT_IMPOSSIBLE_DUPLICATE_ORGANIZE);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.CITY_SET:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, true);
				NKCUtil.SetGameobjectActive(this.m_objSeized, false);
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_WORLDMAP_CITY_LEADER);
				NKCUtil.SetImageSprite(this.m_imgUsedIcon, this.m_spUsedCity, false);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.CITY_MISSION:
			case NKCUnitSortSystem.eUnitState.WARFARE_BATCH:
			case NKCUnitSortSystem.eUnitState.DIVE_BATCH:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, true);
				NKCUtil.SetGameobjectActive(this.m_objSeized, false);
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_DOING);
				NKCUtil.SetImageSprite(this.m_imgUsedIcon, this.m_spUsedCity, false);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.DECKED:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DECKED);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.MAINUNIT:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCUtilString.GET_STRING_DECK_UNIT_STATE_MAINUNIT);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.LOCKED:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCUtilString.GET_STRING_DECK_UNIT_STATE_LOCKED);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.SEIZURE:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, true);
				NKCUtil.SetGameobjectActive(this.m_objSeized, true);
				NKCUtil.SetLabelText(this.m_lbMissionStatus, "");
				NKCUtil.SetImageSprite(this.m_imgUsedIcon, this.m_spUsedSeized, false);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.LOBBY_UNIT:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, false);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCUtilString.GET_STRING_LOBBY_UNIT_CAPTAIN);
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_CANNOT_USE", false));
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.LEAGUE_BANNED:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, false);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetGameobjectActive(this.m_objLeagueBanned, true);
				NKCUtil.SetGameobjectActive(this.m_objLeaguePicked, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, "");
				goto IL_3AA;
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT:
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_RIGHT:
			{
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, false);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetGameobjectActive(this.m_objLeagueBanned, false);
				NKCUtil.SetGameobjectActive(this.m_objLeaguePicked, true);
				NKCUtil.SetLabelText(this.m_lbBusyText, "");
				Color color = (this.m_eUnitSlotState == NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT) ? this.m_colorLeaguePickedLeft : this.m_colorLeaguePickedRight;
				NKCUtil.SetImageColor(this.m_imgLeaguePicked, color);
				goto IL_3AA;
			}
			case NKCUnitSortSystem.eUnitState.OFFICE_DORM_IN:
				NKCUtil.SetGameobjectActive(this.m_objBusyRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objBusyDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
				NKCUtil.SetLabelText(this.m_lbBusyText, NKCUtilString.GET_STRING_OFFICE_ROOM_IN);
				goto IL_3AA;
			}
			NKCUtil.SetGameobjectActive(this.m_objBusyRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
			IL_3AA:
			NKCUtil.SetGameobjectActive(this.m_objChecked, this.m_eUnitSlotState == NKCUnitSortSystem.eUnitState.CHECKED);
		}

		// Token: 0x06006F77 RID: 28535 RVA: 0x0024DBC4 File Offset: 0x0024BDC4
		public virtual void SetCityLeaderMark(bool value)
		{
		}

		// Token: 0x06006F78 RID: 28536 RVA: 0x0024DBC6 File Offset: 0x0024BDC6
		public void SetNewMark(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objNew, value);
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x0024DBD4 File Offset: 0x0024BDD4
		public void SetNameColor(Color color)
		{
			if (this.m_lbName != null)
			{
				this.m_lbName.color = color;
			}
		}

		// Token: 0x06006F7A RID: 28538 RVA: 0x0024DBF0 File Offset: 0x0024BDF0
		public void SetHaveCount(int count, bool bShowBtn = true)
		{
			if (this.m_lbHaveCount != null)
			{
				if (count > 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objHaveCount, true);
					NKCUtil.SetLabelText(this.m_lbHaveCount, count.ToString());
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objHaveCount, false);
				}
			}
			if (this.m_btnHave != null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnHave, bShowBtn && count > 0);
			}
		}

		// Token: 0x06006F7B RID: 28539 RVA: 0x0024DC60 File Offset: 0x0024BE60
		public void SetSortingTypeValue(bool bSet, NKCUnitSortSystem.eSortOption sortOption, int sortValue = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortingType, bSet);
			if (bSet)
			{
				this.m_lbSortingType.text = this.GetSortName(sortOption);
				if (this.m_NKMUnitTempletBase != null && this.m_NKMUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR && NKCOperatorUtil.IsPercentageStat(sortOption))
				{
					this.m_lbSortingValue.text = NKCOperatorUtil.GetStatPercentageString((float)sortValue);
					return;
				}
				this.m_lbSortingValue.text = sortValue.ToString();
			}
		}

		// Token: 0x06006F7C RID: 28540 RVA: 0x0024DCD4 File Offset: 0x0024BED4
		public void SetSortingTypeValue(bool bSet, NKCOperatorSortSystem.eSortOption sortOption = NKCOperatorSortSystem.eSortOption.Level_High, int sortValue = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortingType, bSet);
			if (bSet)
			{
				this.m_lbSortingType.text = this.GetSortName(sortOption);
				if (this.m_NKMUnitTempletBase != null && this.m_NKMUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR && NKCOperatorUtil.IsPercentageStat(sortOption))
				{
					this.m_lbSortingValue.text = NKCOperatorUtil.GetStatPercentageString((float)sortValue);
					return;
				}
				this.m_lbSortingValue.text = sortValue.ToString();
			}
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x0024DD45 File Offset: 0x0024BF45
		public void SetSortingTypeValue(bool bSet, NKCUnitSortSystem.eSortOption sortOption, string sortValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortingType, bSet);
			if (bSet)
			{
				this.m_lbSortingType.text = this.GetSortName(sortOption);
				this.m_lbSortingValue.text = sortValue;
			}
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x0024DD74 File Offset: 0x0024BF74
		public void SetSortingTypeValue(bool bSet, NKCOperatorSortSystem.eSortOption sortOption, string sortValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortingType, bSet);
			if (bSet)
			{
				this.m_lbSortingType.text = this.GetSortName(sortOption);
				this.m_lbSortingValue.text = sortValue;
			}
		}

		// Token: 0x06006F7F RID: 28543 RVA: 0x0024DDA3 File Offset: 0x0024BFA3
		private string GetSortName(NKCOperatorSortSystem.eSortOption sortOption)
		{
			return this.GetSortName(NKCOperatorSortSystem.ConvertSortOption(sortOption));
		}

		// Token: 0x06006F80 RID: 28544 RVA: 0x0024DDB1 File Offset: 0x0024BFB1
		private string GetSortName(NKCUnitSortSystem.eSortOption sortOption)
		{
			return NKCUnitSortSystem.GetSortName(sortOption);
		}

		// Token: 0x06006F81 RID: 28545 RVA: 0x0024DDB9 File Offset: 0x0024BFB9
		protected virtual void SetFierceBattleOtherBossAlreadyUsed(bool bVal)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE, bVal);
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x0024DDC7 File Offset: 0x0024BFC7
		protected virtual void OnClick()
		{
			if (this.dOnSelectThisSlot != null)
			{
				this.dOnSelectThisSlot(this.m_NKMUnitData, this.m_NKMUnitTempletBase, this.m_DeckIndex, this.m_eUnitSlotState, this.m_eUnitSelectState);
			}
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x0024DDFA File Offset: 0x0024BFFA
		protected virtual void OnClickHave()
		{
			if (this.m_NKMUnitData != null)
			{
				NKCPopupHaveInfo.Instance.Open(this.m_NKMUnitData.m_UnitID);
			}
		}

		// Token: 0x06006F84 RID: 28548 RVA: 0x0024DE19 File Offset: 0x0024C019
		public void InvokeClick()
		{
			this.OnClick();
		}

		// Token: 0x06006F85 RID: 28549 RVA: 0x0024DE21 File Offset: 0x0024C021
		public void ClearTouchHoldEvent()
		{
			this.m_cbtnSlot.dOnPointerHolding = null;
		}

		// Token: 0x06006F86 RID: 28550 RVA: 0x0024DE30 File Offset: 0x0024C030
		public void SetTouchHoldEvent(UnityAction<NKMUnitData> holdAction)
		{
			if (holdAction == null)
			{
				this.m_cbtnSlot.dOnPointerHolding = null;
				return;
			}
			this.m_cbtnSlot.dOnPointerHolding = delegate()
			{
				holdAction(this.m_NKMUnitData);
			};
		}

		// Token: 0x06006F87 RID: 28551 RVA: 0x0024DE80 File Offset: 0x0024C080
		public void SetTouchHoldEvent(UnityAction<NKMOperator> holdAction)
		{
			if (holdAction == null)
			{
				this.m_cbtnSlot.dOnPointerHolding = null;
				return;
			}
			this.m_cbtnSlot.dOnPointerHolding = delegate()
			{
				holdAction(this.m_OperatorData);
			};
		}

		// Token: 0x06006F88 RID: 28552 RVA: 0x0024DECD File Offset: 0x0024C0CD
		protected virtual void RestoreSprite()
		{
		}

		// Token: 0x06006F89 RID: 28553 RVA: 0x0024DECF File Offset: 0x0024C0CF
		public virtual void SetContractedUnitMark(bool value)
		{
		}

		// Token: 0x06006F8A RID: 28554 RVA: 0x0024DED1 File Offset: 0x0024C0D1
		public virtual void SetRecall(bool bValue)
		{
		}

		// Token: 0x04005AD2 RID: 23250
		protected NKMUnitData m_NKMUnitData;

		// Token: 0x04005AD3 RID: 23251
		private int m_UnitID;

		// Token: 0x04005AD4 RID: 23252
		private int m_SkinID;

		// Token: 0x04005AD5 RID: 23253
		protected NKMDeckIndex m_DeckIndex;

		// Token: 0x04005AD6 RID: 23254
		[NonSerialized]
		protected NKMUnitTempletBase m_NKMUnitTempletBase;

		// Token: 0x04005AD7 RID: 23255
		protected NKMOperator m_OperatorData;

		// Token: 0x04005AD8 RID: 23256
		private int m_OperatorID;

		// Token: 0x04005AD9 RID: 23257
		public NKCUIComButton m_cbtnSlot;

		// Token: 0x04005ADA RID: 23258
		[Header("슬롯 상태 오브젝트")]
		public GameObject m_objCardRoot;

		// Token: 0x04005ADB RID: 23259
		public GameObject m_objSlotStatus;

		// Token: 0x04005ADC RID: 23260
		public Image m_imgSlotStatus;

		// Token: 0x04005ADD RID: 23261
		public Sprite m_spEmpty;

		// Token: 0x04005ADE RID: 23262
		public Sprite m_spDenied;

		// Token: 0x04005ADF RID: 23263
		public Sprite m_spClosed;

		// Token: 0x04005AE0 RID: 23264
		public Sprite m_spSelectResource;

		// Token: 0x04005AE1 RID: 23265
		public Sprite m_spAdd;

		// Token: 0x04005AE2 RID: 23266
		[Header("배경")]
		public Image m_imgBG;

		// Token: 0x04005AE3 RID: 23267
		public Sprite m_spBG_N;

		// Token: 0x04005AE4 RID: 23268
		public Sprite m_spBG_R;

		// Token: 0x04005AE5 RID: 23269
		public Sprite m_spBG_SR;

		// Token: 0x04005AE6 RID: 23270
		public Sprite m_spBG_SSR;

		// Token: 0x04005AE7 RID: 23271
		[Header("레어리티")]
		public Image m_imgRarity;

		// Token: 0x04005AE8 RID: 23272
		public Sprite m_spSSR;

		// Token: 0x04005AE9 RID: 23273
		public Sprite m_spSR;

		// Token: 0x04005AEA RID: 23274
		public Sprite m_spR;

		// Token: 0x04005AEB RID: 23275
		public Sprite m_spN;

		// Token: 0x04005AEC RID: 23276
		[Header("유닛 기본 정보")]
		public Image m_imgUnitPortrait;

		// Token: 0x04005AED RID: 23277
		public Text m_lbName;

		// Token: 0x04005AEE RID: 23278
		public Text m_lbLevel;

		// Token: 0x04005AEF RID: 23279
		public GameObject m_objMaxExp;

		// Token: 0x04005AF0 RID: 23280
		public Image m_imgAttackType;

		// Token: 0x04005AF1 RID: 23281
		public Image m_imgClassType;

		// Token: 0x04005AF2 RID: 23282
		public NKCUIComStarRank m_comStarRank;

		// Token: 0x04005AF3 RID: 23283
		public GameObject m_objRearm;

		// Token: 0x04005AF4 RID: 23284
		[Header("부대 번호")]
		public GameObject m_objShipNumberRoot;

		// Token: 0x04005AF5 RID: 23285
		public Text m_lbShipNumber;

		// Token: 0x04005AF6 RID: 23286
		public LayoutElement m_layoutElement;

		// Token: 0x04005AF7 RID: 23287
		[Header("선택 표시 관련")]
		public GameObject m_objBusyRoot;

		// Token: 0x04005AF8 RID: 23288
		public Text m_lbBusyText;

		// Token: 0x04005AF9 RID: 23289
		public GameObject m_objBusyDisable;

		// Token: 0x04005AFA RID: 23290
		public GameObject m_objInCityMission;

		// Token: 0x04005AFB RID: 23291
		public Text m_lbMissionStatus;

		// Token: 0x04005AFC RID: 23292
		public GameObject m_objChecked;

		// Token: 0x04005AFD RID: 23293
		public GameObject m_objLeagueBanned;

		// Token: 0x04005AFE RID: 23294
		public GameObject m_objLeaguePicked;

		// Token: 0x04005AFF RID: 23295
		public Image m_imgLeaguePicked;

		// Token: 0x04005B00 RID: 23296
		public Color m_colorLeaguePickedLeft;

		// Token: 0x04005B01 RID: 23297
		public Color m_colorLeaguePickedRight;

		// Token: 0x04005B02 RID: 23298
		public Image m_imgUsedIcon;

		// Token: 0x04005B03 RID: 23299
		public GameObject m_objSeized;

		// Token: 0x04005B04 RID: 23300
		public Sprite m_spUsedCity;

		// Token: 0x04005B05 RID: 23301
		public Sprite m_spUsedSeized;

		// Token: 0x04005B06 RID: 23302
		[Header("선택된 하이라이트")]
		public GameObject m_objSelectedSlotHighlight;

		// Token: 0x04005B07 RID: 23303
		public GameObject m_objSelectedSlotHighlightCastingBan;

		// Token: 0x04005B08 RID: 23304
		[Header("더 이상 선택안됨 표시")]
		public GameObject m_objDisableSelectSlot;

		// Token: 0x04005B09 RID: 23305
		[Header("잠김/삭제 마크")]
		public GameObject m_objLocked;

		// Token: 0x04005B0A RID: 23306
		public GameObject m_objLockBig;

		// Token: 0x04005B0B RID: 23307
		public GameObject m_objDelete;

		// Token: 0x04005B0C RID: 23308
		[Header("즐겨찾기")]
		public GameObject m_objFavorite;

		// Token: 0x04005B0D RID: 23309
		[Header("New 마크")]
		public GameObject m_objNew;

		// Token: 0x04005B0E RID: 23310
		[Header("정렬 기준/수치")]
		public GameObject m_objSortingType;

		// Token: 0x04005B0F RID: 23311
		public Text m_lbSortingType;

		// Token: 0x04005B10 RID: 23312
		public Text m_lbSortingValue;

		// Token: 0x04005B11 RID: 23313
		[Header("보유 확인")]
		public NKCUIComStateButton m_btnHave;

		// Token: 0x04005B12 RID: 23314
		[Header("보유 갯수")]
		public GameObject m_objHaveCount;

		// Token: 0x04005B13 RID: 23315
		public Text m_lbHaveCount;

		// Token: 0x04005B14 RID: 23316
		[Header("밴 표시")]
		public GameObject m_objBan;

		// Token: 0x04005B15 RID: 23317
		public Text m_lbBanLevel;

		// Token: 0x04005B16 RID: 23318
		public Text m_lbBanApplyDesc;

		// Token: 0x04005B17 RID: 23319
		[Header("레벨 표시")]
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL;

		// Token: 0x04005B18 RID: 23320
		[Header("각성 애니메이션")]
		public Animator m_animAwakenFX;

		// Token: 0x04005B19 RID: 23321
		[Header("격전지원")]
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE;

		// Token: 0x04005B1A RID: 23322
		[Header("전술업데이트")]
		public NKCUITacticUpdateLevelSlot m_tacticUpdateSlot;

		// Token: 0x04005B1B RID: 23323
		protected NKCUIUnitSelectListSlotBase.OnSelectThisSlot dOnSelectThisSlot;

		// Token: 0x04005B1C RID: 23324
		protected NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot dOnSelectThisOperatorSlot;

		// Token: 0x04005B1D RID: 23325
		private int m_iPowerCache;

		// Token: 0x04005B1E RID: 23326
		protected NKCUnitSortSystem.eUnitState m_eUnitSlotState;

		// Token: 0x04005B1F RID: 23327
		protected NKCUIUnitSelectList.eUnitSlotSelectState m_eUnitSelectState;

		// Token: 0x04005B20 RID: 23328
		protected bool m_bEnableShowBan;

		// Token: 0x04005B21 RID: 23329
		protected NKCBanManager.BAN_DATA_TYPE m_eBanDataType = NKCBanManager.BAN_DATA_TYPE.FINAL;

		// Token: 0x04005B22 RID: 23330
		protected bool m_bEnableShowUpUnit;

		// Token: 0x04005B23 RID: 23331
		protected bool m_bEnableShowCastingBan;

		// Token: 0x02001734 RID: 5940
		public enum eUnitSlotMode
		{
			// Token: 0x0400A646 RID: 42566
			Character,
			// Token: 0x0400A647 RID: 42567
			Empty,
			// Token: 0x0400A648 RID: 42568
			Denied,
			// Token: 0x0400A649 RID: 42569
			SelectResource,
			// Token: 0x0400A64A RID: 42570
			Closed,
			// Token: 0x0400A64B RID: 42571
			ClearAll,
			// Token: 0x0400A64C RID: 42572
			AutoComplete,
			// Token: 0x0400A64D RID: 42573
			Add
		}

		// Token: 0x02001735 RID: 5941
		// (Invoke) Token: 0x0600B2A2 RID: 45730
		public delegate void OnSelectThisSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState);

		// Token: 0x02001736 RID: 5942
		// (Invoke) Token: 0x0600B2A6 RID: 45734
		public delegate void OnSelectThisOperatorSlot(NKMOperator operatorData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState);
	}
}
