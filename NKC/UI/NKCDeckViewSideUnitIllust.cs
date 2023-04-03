using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000964 RID: 2404
	public class NKCDeckViewSideUnitIllust : MonoBehaviour
	{
		// Token: 0x0600600F RID: 24591 RVA: 0x001DEF72 File Offset: 0x001DD172
		public NKMUnitData GetUnitData()
		{
			return this.m_UnitData;
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x001DEF7A File Offset: 0x001DD17A
		public bool hasUnitData()
		{
			return this.m_UnitTempletBase != null;
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x001DEF85 File Offset: 0x001DD185
		public void SetEnableControlLeaderBtn(bool bSet)
		{
			this.m_bEnableControlLeaderBtn = bSet;
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x001DEF8E File Offset: 0x001DD18E
		public void SetShipOnlyMode(bool value)
		{
			this.m_bShipOnlyMode = value;
			this.SetObject();
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x001DEF9D File Offset: 0x001DD19D
		public void SetShowLoadingAnimWhenEmpty(bool bSet)
		{
			this.m_bShowLoadingAnimWhenEmpty = bSet;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x001DEFA8 File Offset: 0x001DD1A8
		public void Init(NKCDeckViewSideUnitIllust.OnUnitInfoClick onUnitInfo, NKCDeckViewSideUnitIllust.OnUnitChangeClick onUnitChange, NKCDeckViewSideUnitIllust.OnLeaderChange onLeaderChange, Animator LoadingAnimator)
		{
			this.ResetObj();
			this.m_animLoading = LoadingAnimator;
			this.dOnUnitInfoClick = onUnitInfo;
			this.dOnUnitChangeClick = onUnitChange;
			this.dOnLeaderChange = onLeaderChange;
			if (this.m_cbtnInfo != null)
			{
				this.m_cbtnInfo.PointerClick.RemoveAllListeners();
				this.m_cbtnInfo.PointerClick.AddListener(new UnityAction(this.OnUnitInfoClicked));
			}
			if (this.m_cbtnChange != null)
			{
				this.m_cbtnChange.PointerClick.RemoveAllListeners();
				this.m_cbtnChange.PointerClick.AddListener(new UnityAction(this.OnUnitChangeClicked));
			}
			if (this.m_cbtnLeader != null)
			{
				this.m_cbtnLeader.PointerClick.RemoveAllListeners();
				this.m_cbtnLeader.PointerClick.AddListener(new UnityAction(this.OnLeaderChangeClicked));
			}
			this.m_vSubMenuAnchoredPosOrg = this.m_rectSubMenu.anchoredPosition;
			this.m_vSpineObjectRootAnchoredPosOrg = this.m_rectSpineObjectRoot.anchoredPosition;
			if (this.m_rectShipSpineObjectRoot.GetComponent<NKCUIComSafeArea>() != null)
			{
				this.m_rectShipSpineObjectRoot.GetComponent<NKCUIComSafeArea>().SetSafeAreaUI();
			}
			this.m_vShipObjectRootAnchoredPosOrg = this.m_rectShipSpineObjectRoot.anchoredPosition;
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x001DF0DC File Offset: 0x001DD2DC
		public void ResetObj()
		{
			this.m_UnitData = null;
			this.m_OperatorData = null;
			this.m_UnitTempletBase = null;
			this.m_UICharacterView.CleanUp();
			this.m_UIShipView.CleanUp();
			this.SetSubMenuPosition(false, false);
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x001DF114 File Offset: 0x001DD314
		public void Open(NKCUIDeckViewer.DeckViewerMode mode, bool bInit)
		{
			if (this.m_bOpen)
			{
				return;
			}
			this.m_bOpen = true;
			this.m_eMode = mode;
			if (mode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid || mode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss)
			{
				this.m_rectShipSpineObjectRoot.anchoredPosition = new Vector2(this.m_vShipObjectRootAnchoredPosOrg.x, this.m_vShipObjectRootAnchoredPosOrg.y + 70f);
				this.m_bShowLoadingAnimWhenEmpty = false;
			}
			else
			{
				this.m_rectShipSpineObjectRoot.anchoredPosition = this.m_vShipObjectRootAnchoredPosOrg;
				this.m_bShowLoadingAnimWhenEmpty = true;
			}
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (bInit)
			{
				this.ResetObj();
				return;
			}
			if (this.m_UnitData != null)
			{
				this.SetObject();
				this.SetSubMenuPosition(true, true);
				return;
			}
			this.SetSubMenuPosition(false, true);
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x001DF1D4 File Offset: 0x001DD3D4
		public void Close()
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.m_bOpen = false;
			this.m_UICharacterView.CleanUp();
			this.m_UIShipView.CleanUp();
			this.SetSubMenuPosition(false, true);
			this.m_rectSubMenu.anchoredPosition = this.m_vSubMenuAnchoredPosOrg;
			this.m_rectSpineObjectRoot.anchoredPosition = this.m_vSpineObjectRootAnchoredPosOrg;
			this.m_rectShipSpineObjectRoot.anchoredPosition = this.m_vShipObjectRootAnchoredPosOrg;
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x001DF244 File Offset: 0x001DD444
		private void SetMenuActive(NKMUnitData unitData)
		{
			if (!this.m_bEnableControlLeaderBtn)
			{
				return;
			}
			if (unitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_cbtnLeader, false);
				return;
			}
			NKM_UNIT_TYPE nkm_UNIT_TYPE = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID).m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (nkm_UNIT_TYPE - NKM_UNIT_TYPE.NUT_SHIP > 1)
				{
				}
				NKCUtil.SetGameobjectActive(this.m_cbtnLeader, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_cbtnLeader, true);
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x001DF2A0 File Offset: 0x001DD4A0
		public void SetUnitData(NKMUnitData unitData, bool bLeader, NKCDeckViewSideUnitIllust.eUnitChangePossible unitchangePossible, bool bForce = false)
		{
			if (unitData != null && this.m_UnitData != null && this.m_UnitData.m_UnitUID == unitData.m_UnitUID && !bForce)
			{
				return;
			}
			this.SetMenuActive(unitData);
			this.SetLeader(bLeader);
			NKCUtil.SetGameobjectActive(this.m_cbtnChange, unitchangePossible == NKCDeckViewSideUnitIllust.eUnitChangePossible.OK);
			this.m_eCurrentUnitChangeBtnState = unitchangePossible;
			NKCUtil.SetGameobjectActive(this.m_objChangeBlocked, unitchangePossible > NKCDeckViewSideUnitIllust.eUnitChangePossible.OK);
			if (unitchangePossible != NKCDeckViewSideUnitIllust.eUnitChangePossible.WORLDMAP_MISSION)
			{
				if (unitchangePossible - NKCDeckViewSideUnitIllust.eUnitChangePossible.WARFARE <= 1)
				{
					NKCUtil.SetLabelText(this.m_lbChangeBlocked, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DOING);
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbChangeBlocked, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DOING_MISSION);
			}
			this.m_OperatorData = null;
			if (unitData == null)
			{
				this.m_UnitData = null;
				this.m_UnitTempletBase = null;
				this.m_textLevel.SetText("0", false, Array.Empty<Text>());
				NKCUtil.SetGameobjectActive(this.m_objExpMax, false);
				this.m_textName.text = "";
				this.m_comStarRank.SetStarRank(0, 1, false);
				this.m_lbPowerSummary.text = "";
			}
			else
			{
				this.m_UnitData = unitData;
				this.m_UnitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
				NKCUtil.SetGameobjectActive(this.m_objExpMax, NKCExpManager.IsUnitMaxLevel(unitData));
				this.m_textLevel.SetLevel(unitData, 0, Array.Empty<Text>());
				NKCUtil.SetLabelText(this.m_textName, this.m_UnitTempletBase.GetUnitName() + NKCUtilString.GetRespawnCountText(this.m_UnitData.m_UnitID));
				if (this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL || this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					this.m_comStarRank.SetStarRank(unitData);
					bool flag = NKCUtil.CheckPossibleShowBan(this.m_eMode);
					int num = unitData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, flag ? NKCBanManager.GetBanDataShip(NKCBanManager.BAN_DATA_TYPE.FINAL) : null, null);
					this.m_lbPowerSummary.text = num.ToString();
				}
				else if (this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					this.m_comStarRank.SetStarRank(0, 0, false);
					NKCUtil.SetLabelText(this.m_lbPowerSummary, this.m_UnitData.CalculateOperationPower(null, 0, null, NKCOperatorUtil.GetOperatorData(this.m_UnitData.m_UnitUID)).ToString());
				}
			}
			this.SetObject();
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x001DF4C4 File Offset: 0x001DD6C4
		public void SetOperatorData(NKMOperator operatorData, NKCDeckViewSideUnitIllust.eUnitChangePossible unitchangePossible, bool bForce = false)
		{
			if (this.m_OperatorData == operatorData && !bForce)
			{
				return;
			}
			this.SetMenuActive(null);
			this.SetLeader(false);
			NKCUtil.SetGameobjectActive(this.m_cbtnChange, unitchangePossible == NKCDeckViewSideUnitIllust.eUnitChangePossible.OK);
			this.m_eCurrentUnitChangeBtnState = unitchangePossible;
			NKCUtil.SetGameobjectActive(this.m_objChangeBlocked, unitchangePossible > NKCDeckViewSideUnitIllust.eUnitChangePossible.OK);
			if (unitchangePossible != NKCDeckViewSideUnitIllust.eUnitChangePossible.WORLDMAP_MISSION)
			{
				if (unitchangePossible - NKCDeckViewSideUnitIllust.eUnitChangePossible.WARFARE <= 1)
				{
					NKCUtil.SetLabelText(this.m_lbChangeBlocked, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DOING);
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbChangeBlocked, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DOING_MISSION);
			}
			this.m_UnitData = null;
			if (operatorData == null)
			{
				this.m_OperatorData = null;
				this.m_UnitTempletBase = null;
				this.m_textLevel.SetText("0", false, Array.Empty<Text>());
				NKCUtil.SetGameobjectActive(this.m_objExpMax, false);
				this.m_textName.text = "";
				this.m_comStarRank.SetStarRank(0, 1, false);
				this.m_lbPowerSummary.text = "";
			}
			else
			{
				this.m_OperatorData = operatorData;
				this.m_UnitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_OperatorData.id);
				this.m_textLevel.SetLevel(this.m_OperatorData, Array.Empty<Text>());
				NKCUtil.SetGameobjectActive(this.m_objExpMax, NKCOperatorUtil.IsMaximumLevel(this.m_OperatorData.level));
				NKCUtil.SetLabelText(this.m_textName, this.m_UnitTempletBase.GetUnitName() ?? "");
				this.m_comStarRank.SetStarRank(0, 0, false);
				NKCUtil.SetLabelText(this.m_lbPowerSummary, this.m_OperatorData.CalculateOperatorOperationPower().ToString());
			}
			this.SetObject();
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x001DF649 File Offset: 0x001DD849
		public bool IsMatchedSideIllustToUnitType(NKM_UNIT_TYPE targetType)
		{
			return this.m_UnitTempletBase != null && this.m_UnitTempletBase.m_NKM_UNIT_TYPE == targetType;
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x001DF664 File Offset: 0x001DD864
		public void UpdateUnit(NKMUnitData unitData)
		{
			if (unitData == null || this.m_UnitData == null)
			{
				return;
			}
			if (this.m_UnitData.m_UnitUID != unitData.m_UnitUID)
			{
				return;
			}
			this.SetUnitData(unitData, this.m_bLeader, this.m_eCurrentUnitChangeBtnState, true);
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x001DF69A File Offset: 0x001DD89A
		public void UpdateOperator(NKMOperator operatorData)
		{
			if (operatorData == null || this.m_OperatorData == null)
			{
				return;
			}
			if (this.m_OperatorData.uid != operatorData.uid)
			{
				return;
			}
			this.SetOperatorData(operatorData, this.m_eCurrentUnitChangeBtnState, true);
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x001DF6CC File Offset: 0x001DD8CC
		public void SetObject()
		{
			NKM_UNIT_TYPE nkm_UNIT_TYPE = NKM_UNIT_TYPE.NUT_INVALID;
			NKM_UNIT_TYPE nkm_UNIT_TYPE2 = NKM_UNIT_TYPE.NUT_INVALID;
			if (!this.m_bOpen)
			{
				this.SetIllust();
				this.SetShipIllust();
				this.SetLeader();
			}
			else
			{
				if (this.m_UICharacterView.HasCharacterIllust())
				{
					nkm_UNIT_TYPE = NKM_UNIT_TYPE.NUT_NORMAL;
					Vector2 vSpineObjectRootAnchoredPosOrg = this.m_vSpineObjectRootAnchoredPosOrg;
					vSpineObjectRootAnchoredPosOrg.x = 900f;
					float num = 0.3f * (900f - this.m_rectSpineObjectRoot.anchoredPosition.x) / 900f;
					this.m_rectSpineObjectRoot.DOKill(false);
					this.m_rectSpineObjectRoot.DOAnchorPos(vSpineObjectRootAnchoredPosOrg, num, false).SetEase(Ease.OutCubic).OnComplete(new TweenCallback(this.SetIllust));
					this.SetCharacterFade(this.m_UICharacterView, 0f, num);
				}
				else
				{
					this.m_rectSpineObjectRoot.DOKill(false);
					Vector2 vSpineObjectRootAnchoredPosOrg2 = this.m_vSpineObjectRootAnchoredPosOrg;
					vSpineObjectRootAnchoredPosOrg2.x = 900f;
					this.m_rectSpineObjectRoot.anchoredPosition = vSpineObjectRootAnchoredPosOrg2;
					this.SetIllust();
				}
				if (this.m_UIShipView.HasCharacterIllust())
				{
					nkm_UNIT_TYPE = NKM_UNIT_TYPE.NUT_SHIP;
					if (this.m_UnitTempletBase == null || this.m_UIShipView.IsDiffrentCharacter(this.m_UnitTempletBase.m_UnitStrID))
					{
						this.m_rectShipSpineObjectRoot.DOKill(false);
						this.m_rectShipSpineObjectRoot.DOScale(new Vector3(-0.7f, 0.7f, 0f), 0.3f).SetEase(Ease.OutCubic).OnComplete(new TweenCallback(this.SetShipIllust));
						this.SetCharacterFade(this.m_UIShipView, 0f, 0.3f);
					}
				}
				else
				{
					this.m_rectShipSpineObjectRoot.DOKill(false);
					this.m_rectShipSpineObjectRoot.localScale = new Vector3(-0.7f, 0.7f, 0f);
					this.SetShipIllust();
				}
				if ((this.m_UnitData == null && this.m_OperatorData == null) || this.m_UnitTempletBase == null)
				{
					nkm_UNIT_TYPE2 = NKM_UNIT_TYPE.NUT_INVALID;
				}
				else
				{
					nkm_UNIT_TYPE2 = this.m_UnitTempletBase.m_NKM_UNIT_TYPE;
				}
			}
			if (this.m_bOpen)
			{
				switch (nkm_UNIT_TYPE2)
				{
				case NKM_UNIT_TYPE.NUT_INVALID:
					this.ShowUnitBG(false);
					if (this.m_bShowLoadingAnimWhenEmpty)
					{
						this.PlayLoadingAnim("BASE");
					}
					else
					{
						this.PlayLoadingAnim("CLOSE");
					}
					break;
				case NKM_UNIT_TYPE.NUT_NORMAL:
				case NKM_UNIT_TYPE.NUT_OPERATOR:
					this.ShowUnitBG(true);
					if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
					{
						this.PlayLoadingAnim("CHANGE");
					}
					else
					{
						this.PlayLoadingAnim("CLOSE");
					}
					break;
				case NKM_UNIT_TYPE.NUT_SHIP:
					this.ShowUnitBG(false);
					if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
					{
						this.PlayLoadingAnim("CHANGE");
					}
					else
					{
						this.PlayLoadingAnim("CLOSE");
					}
					break;
				}
			}
			this.SetSubMenuPosition(this.m_UnitData != null || this.m_OperatorData != null, true);
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x001DF964 File Offset: 0x001DDB64
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x001DF974 File Offset: 0x001DDB74
		private void SetCharacterFade(NKCUICharacterView uiChar, float targetAlpha, float time)
		{
			DOTween.To(() => uiChar.GetColor().a, delegate(float a)
			{
				uiChar.SetColor(-1f, -1f, -1f, a, false);
			}, targetAlpha, time);
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x001DF9B0 File Offset: 0x001DDBB0
		private void SetIllust()
		{
			if (this.m_bShipOnlyMode)
			{
				return;
			}
			if (this.m_UnitTempletBase != null && (this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL || this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR))
			{
				if (this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					this.m_UICharacterView.SetCharacterIllust(this.m_UnitData, false, true, true, 0);
				}
				else if (this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					this.m_UICharacterView.SetCharacterIllust(this.m_OperatorData, false, true, true, 0);
				}
				this.m_rectSpineObjectRoot.DOKill(false);
				this.m_rectSpineObjectRoot.DOAnchorPos(this.m_vSpineObjectRootAnchoredPosOrg, 0.3f, false).SetEase(Ease.OutCubic);
				this.m_UICharacterView.SetColor(-1f, -1f, -1f, 0f, false);
				this.SetCharacterFade(this.m_UICharacterView, 1f, 0.3f);
				return;
			}
			this.m_UICharacterView.CleanUp();
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x001DFAA8 File Offset: 0x001DDCA8
		private void SetShipIllust()
		{
			if (this.m_UnitTempletBase != null && this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				this.m_UIShipView.SetCharacterIllust(this.m_UnitData, false, true, true, 0);
				this.m_rectShipSpineObjectRoot.DOKill(false);
				this.m_rectShipSpineObjectRoot.DOScale(new Vector3(-1f, 1f, 0f), 0.3f).SetEase(Ease.OutCubic);
				this.m_UIShipView.SetColor(-1f, -1f, -1f, 0f, false);
				this.SetCharacterFade(this.m_UIShipView, 1f, 0.3f);
				return;
			}
			this.m_UIShipView.CleanUp();
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x001DFB64 File Offset: 0x001DDD64
		private void SetSubMenuPosition(bool bIn, bool bAnimate = true)
		{
			this.m_rectSubMenu.DOKill(false);
			Vector2 vSubMenuAnchoredPosOrg = this.m_vSubMenuAnchoredPosOrg;
			if (this.m_eMode != NKCUIDeckViewer.DeckViewerMode.DeckSetupOnly)
			{
				vSubMenuAnchoredPosOrg.y += 95f;
			}
			vSubMenuAnchoredPosOrg.x = (float)((bIn && !this.m_bShipOnlyMode) ? 0 : 900);
			if (bAnimate)
			{
				this.m_rectSubMenu.anchoredPosition = new Vector2(this.m_rectSubMenu.anchoredPosition.x, vSubMenuAnchoredPosOrg.y);
				this.m_rectSubMenu.DOAnchorPos(vSubMenuAnchoredPosOrg, 0.6f, false).SetEase(Ease.OutCubic);
				return;
			}
			this.m_rectSubMenu.anchoredPosition = vSubMenuAnchoredPosOrg;
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x001DFC08 File Offset: 0x001DDE08
		public void SetLeader(bool bLeader)
		{
			this.m_bLeader = bLeader;
			this.SetLeader();
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x001DFC17 File Offset: 0x001DDE17
		public void SetLeader()
		{
			if (this.m_bLeader)
			{
				this.m_cbtnLeader.UnLock();
				this.m_cbtnLeader.Select(true, false, false);
				return;
			}
			this.m_cbtnLeader.UnLock();
			this.m_cbtnLeader.Select(false, false, false);
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x001DFC54 File Offset: 0x001DDE54
		private void OnUnitInfoClicked()
		{
			if (this.m_UnitTempletBase != null && this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR && this.m_OperatorData != null)
			{
				NKCUIOperatorInfo.Instance.Open(this.m_OperatorData, new NKCUIOperatorInfo.OpenOption(new List<long>
				{
					this.m_OperatorData.uid
				}, 0));
				return;
			}
			if (this.dOnUnitInfoClick != null)
			{
				this.dOnUnitInfoClick(this.m_UnitData);
			}
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x001DFCC8 File Offset: 0x001DDEC8
		private void OnUnitChangeClicked()
		{
			if (this.dOnUnitChangeClick != null)
			{
				if (this.m_OperatorData != null && this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_OperatorData.id);
					if (unitTempletBase != null)
					{
						this.dOnUnitChangeClick(unitTempletBase.m_NKM_UNIT_TYPE, this.m_OperatorData.uid);
						return;
					}
				}
				else if (this.m_UnitData != null)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
					if (unitTempletBase2 != null)
					{
						this.dOnUnitChangeClick(unitTempletBase2.m_NKM_UNIT_TYPE, this.m_UnitData.m_UnitUID);
					}
				}
			}
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x001DFD5C File Offset: 0x001DDF5C
		public void OnLeaderChangeClicked()
		{
			if (this.dOnLeaderChange != null)
			{
				this.dOnLeaderChange(this.m_UnitData);
			}
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x001DFD77 File Offset: 0x001DDF77
		public void PlayLoadingAnim(string name)
		{
			if (this.m_animLoading == null)
			{
				return;
			}
			if (this.m_bShipOnlyMode)
			{
				return;
			}
			if (this.m_animLoading.gameObject.activeInHierarchy)
			{
				this.m_animLoading.Play(name, -1, 0f);
			}
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x001DFDB5 File Offset: 0x001DDFB5
		private void ShowUnitBG(bool value)
		{
			if (this.m_BGAnimator != null)
			{
				this.m_BGAnimator.SetBool("UnitSelected", value);
			}
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x001DFDD8 File Offset: 0x001DDFD8
		private Sprite GetSpriteMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string stringMoveType = this.GetStringMoveType(type);
			if (string.IsNullOrEmpty(stringMoveType))
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", stringMoveType, false);
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x001DFE04 File Offset: 0x001DE004
		private string GetStringMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string result = string.Empty;
			switch (type)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_1";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_4";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_2";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_3";
				break;
			}
			return result;
		}

		// Token: 0x04004C70 RID: 19568
		private bool m_bOpen;

		// Token: 0x04004C71 RID: 19569
		private NKMUnitData m_UnitData;

		// Token: 0x04004C72 RID: 19570
		private NKMOperator m_OperatorData;

		// Token: 0x04004C73 RID: 19571
		private NKMUnitTempletBase m_UnitTempletBase;

		// Token: 0x04004C74 RID: 19572
		private bool m_bLeader;

		// Token: 0x04004C75 RID: 19573
		public RectTransform m_rectSpineObjectRoot;

		// Token: 0x04004C76 RID: 19574
		public RectTransform m_rectShipSpineObjectRoot;

		// Token: 0x04004C77 RID: 19575
		public RectTransform m_rectSubMenu;

		// Token: 0x04004C78 RID: 19576
		private Vector2 m_vSubMenuAnchoredPosOrg;

		// Token: 0x04004C79 RID: 19577
		private const float SUBMENU_OFFSET_Y_FOR_PVP = 95f;

		// Token: 0x04004C7A RID: 19578
		private Vector2 m_vSpineObjectRootAnchoredPosOrg;

		// Token: 0x04004C7B RID: 19579
		private Vector2 m_vShipObjectRootAnchoredPosOrg;

		// Token: 0x04004C7C RID: 19580
		[Header("버튼")]
		public NKCUIComButton m_cbtnInfo;

		// Token: 0x04004C7D RID: 19581
		public NKCUIComButton m_cbtnChange;

		// Token: 0x04004C7E RID: 19582
		public NKCUIComButton m_cbtnLeader;

		// Token: 0x04004C7F RID: 19583
		public GameObject m_objChangeBlocked;

		// Token: 0x04004C80 RID: 19584
		public Text m_lbChangeBlocked;

		// Token: 0x04004C81 RID: 19585
		[Header("데이터")]
		public Text m_textName;

		// Token: 0x04004C82 RID: 19586
		public NKCUIComTextUnitLevel m_textLevel;

		// Token: 0x04004C83 RID: 19587
		public GameObject m_objExpMax;

		// Token: 0x04004C84 RID: 19588
		public NKCUIComStarRank m_comStarRank;

		// Token: 0x04004C85 RID: 19589
		public Text m_lbPowerSummary;

		// Token: 0x04004C86 RID: 19590
		public NKCUICharacterView m_UICharacterView;

		// Token: 0x04004C87 RID: 19591
		public NKCUICharacterView m_UIShipView;

		// Token: 0x04004C88 RID: 19592
		public Animator m_BGAnimator;

		// Token: 0x04004C89 RID: 19593
		private Animator m_animLoading;

		// Token: 0x04004C8A RID: 19594
		private const float SHIP_SMALL_SCALE = 0.7f;

		// Token: 0x04004C8B RID: 19595
		private NKCDeckViewSideUnitIllust.OnUnitInfoClick dOnUnitInfoClick;

		// Token: 0x04004C8C RID: 19596
		private NKCDeckViewSideUnitIllust.OnUnitChangeClick dOnUnitChangeClick;

		// Token: 0x04004C8D RID: 19597
		private NKCDeckViewSideUnitIllust.OnLeaderChange dOnLeaderChange;

		// Token: 0x04004C8E RID: 19598
		private bool m_bEnableControlLeaderBtn = true;

		// Token: 0x04004C8F RID: 19599
		private bool m_bShipOnlyMode;

		// Token: 0x04004C90 RID: 19600
		private NKCDeckViewSideUnitIllust.eUnitChangePossible m_eCurrentUnitChangeBtnState;

		// Token: 0x04004C91 RID: 19601
		private NKCUIDeckViewer.DeckViewerMode m_eMode;

		// Token: 0x04004C92 RID: 19602
		private bool m_bShowLoadingAnimWhenEmpty = true;

		// Token: 0x020015E9 RID: 5609
		// (Invoke) Token: 0x0600AEA0 RID: 44704
		public delegate void OnUnitInfoClick(NKMUnitData currentUnitData);

		// Token: 0x020015EA RID: 5610
		// (Invoke) Token: 0x0600AEA4 RID: 44708
		public delegate void OnUnitChangeClick(NKM_UNIT_TYPE _NKM_UNIT_TYPE, long selectedUnitUID);

		// Token: 0x020015EB RID: 5611
		// (Invoke) Token: 0x0600AEA8 RID: 44712
		public delegate void OnLeaderChange(NKMUnitData currentUnitData);

		// Token: 0x020015EC RID: 5612
		public enum eUnitChangePossible
		{
			// Token: 0x0400A2BC RID: 41660
			OK,
			// Token: 0x0400A2BD RID: 41661
			WORLDMAP_MISSION,
			// Token: 0x0400A2BE RID: 41662
			WARFARE,
			// Token: 0x0400A2BF RID: 41663
			DIVE
		}
	}
}
