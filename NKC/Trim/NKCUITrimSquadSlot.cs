using System;
using System.Collections.Generic;
using System.Text;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AB3 RID: 2739
	public class NKCUITrimSquadSlot : MonoBehaviour
	{
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x060079D8 RID: 31192 RVA: 0x002891EA File Offset: 0x002873EA
		public bool IsActive
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x002891F8 File Offset: 0x002873F8
		public void Init(int slotIndex, NKCUITrimSquadSlot.OnDeckConfirm onDeckConfirm)
		{
			this.m_dOnDeckConfirm = onDeckConfirm;
			this.m_slotIndex = slotIndex;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnAddSquad, new UnityAction(this.OnClickAddSquad));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnBoss, new UnityAction(this.OnClickBoss));
			if (this.m_csbtnWarning != null)
			{
				this.m_csbtnWarning.PointerDown.RemoveAllListeners();
				this.m_csbtnWarning.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnSquadButtonDown));
			}
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x0028927C File Offset: 0x0028747C
		public void SetData(int trimId, int trimDungeonId, int trimGroup, int trimLevel)
		{
			this.m_trimId = trimId;
			this.m_trimDungeonId = trimDungeonId;
			this.m_trimLevel = trimLevel;
			int num = this.m_slotIndex + 1;
			NKCUtil.SetLabelText(this.m_lbStageNumberOn, num.ToString());
			NKCUtil.SetLabelText(this.m_lbStageNumberOff, num.ToString());
			NKMUserData shipStateData = NKCScenManager.CurrentUserData();
			this.SetShipStateData(shipStateData);
			this.SetWarningState(trimGroup, trimLevel);
			this.SetBossImage(trimId, trimDungeonId, trimLevel);
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x002892EC File Offset: 0x002874EC
		private void SetWarningState(int trimGroup, int trimLevel)
		{
			NKMTrimPointTemplet nkmtrimPointTemplet = NKMTrimPointTemplet.Find(trimGroup, trimLevel);
			int num = int.MaxValue;
			if (nkmtrimPointTemplet != null)
			{
				num = nkmtrimPointTemplet.RecommendCombatPoint;
			}
			int operationPower = NKCLocalDeckDataManager.GetOperationPower(this.m_slotIndex, false, false, false);
			bool flag = num > operationPower && operationPower > 0;
			this.m_operationLowDelta = 0;
			if (flag)
			{
				this.m_operationLowDelta = (num - operationPower) * 10000 / num;
			}
			NKCUtil.SetGameobjectActive(this.m_objWarning, flag);
			NKCUtil.SetGameobjectActive(this.m_objStageNormalState, !flag);
			NKCUtil.SetGameobjectActive(this.m_objStageLowPowerState, flag);
			if (operationPower == 0)
			{
				NKCUtil.SetLabelText(this.m_lbSquadPowerNormal, "-");
				NKCUtil.SetGameobjectActive(this.m_objSquadNormal, true);
				NKCUtil.SetGameobjectActive(this.m_objSquadWarning, false);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbSquadPowerNormal, operationPower.ToString());
			NKCUtil.SetLabelText(this.m_lbSquadPowerWarning, operationPower.ToString());
			NKCUtil.SetGameobjectActive(this.m_objSquadNormal, !flag);
			NKCUtil.SetGameobjectActive(this.m_objSquadWarning, flag);
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x002893D9 File Offset: 0x002875D9
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
		}

		// Token: 0x060079DD RID: 31197 RVA: 0x002893E8 File Offset: 0x002875E8
		private void SetShipStateData(NKMUserData userData)
		{
			long shipUId = NKCLocalDeckDataManager.GetShipUId(this.m_slotIndex);
			NKCLocalDeckDataManager.GetOperationPower(this.m_slotIndex, false, false, false);
			if (shipUId > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objShipRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objAddImage, false);
				NKMUnitData nkmunitData = (userData != null) ? userData.m_ArmyData.GetShipFromUID(shipUId) : null;
				Sprite sp = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, nkmunitData);
				NKCUtil.SetImageSprite(this.m_imgShip, sp, false);
				int num = 0;
				if (nkmunitData != null)
				{
					num = nkmunitData.m_UnitLevel;
				}
				NKCUtil.SetLabelText(this.m_lbShipLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, num.ToString()));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objShipRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objAddImage, true);
		}

		// Token: 0x060079DE RID: 31198 RVA: 0x00289498 File Offset: 0x00287698
		private void SetBossImage(int trimId, int trimDungeonId, int trimLevel)
		{
			List<NKMTrimDungeonTemplet> list = null;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			if (nkmtrimTemplet != null)
			{
				nkmtrimTemplet.TrimDungeonTemplets.TryGetValue(trimDungeonId, out list);
			}
			NKMTrimDungeonTemplet nkmtrimDungeonTemplet = (list != null) ? list.Find((NKMTrimDungeonTemplet e) => e.TrimLevelLow <= trimLevel && e.TrimLevelHigh >= trimLevel) : null;
			Sprite sp = null;
			if (nkmtrimDungeonTemplet != null && !string.IsNullOrEmpty(nkmtrimDungeonTemplet.TimeBossFaceCard))
			{
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_TRIM_BOSS_THUMBNAIL", nkmtrimDungeonTemplet.TimeBossFaceCard, false);
			}
			NKCUtil.SetImageSprite(this.m_imgBoss, sp, false);
		}

		// Token: 0x060079DF RID: 31199 RVA: 0x00289518 File Offset: 0x00287718
		private void OnClickAddSquad()
		{
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			int num = (nkmtrimTemplet != null) ? nkmtrimTemplet.TrimDungeonIds.Length : 0;
			if (num > 0)
			{
				NKCUIDeckViewer.DeckViewerOption deckViewerOption = default(NKCUIDeckViewer.DeckViewerOption);
				deckViewerOption.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck;
				deckViewerOption.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_TRIM, this.m_slotIndex);
				deckViewerOption.ShowDeckIndexList = new List<int>();
				for (int i = 0; i < num; i++)
				{
					deckViewerOption.ShowDeckIndexList.Add(i);
				}
				deckViewerOption.dCheckSideMenuButton = new NKCUIDeckViewer.DeckViewerOption.CheckDeckButtonConfirm(this.CheckDeckButtonConfirm);
				deckViewerOption.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnDeckSideButtonConfirm);
				deckViewerOption.dOnBackButton = new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.OnDeckBackButton);
				NKCUIDeckViewer.Instance.Open(deckViewerOption, true);
			}
		}

		// Token: 0x060079E0 RID: 31200 RVA: 0x002895D4 File Offset: 0x002877D4
		private NKM_ERROR_CODE CheckDeckButtonConfirm(NKMDeckIndex selectedDeckIndex)
		{
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060079E1 RID: 31201 RVA: 0x002895D7 File Offset: 0x002877D7
		private void OnDeckSideButtonConfirm(NKMDeckIndex selectedDeckIndex)
		{
			NKCLocalDeckDataManager.SaveLocalDeck();
			NKCUIDeckViewer.CheckInstanceAndClose();
			if (this.m_dOnDeckConfirm != null)
			{
				this.m_dOnDeckConfirm();
			}
		}

		// Token: 0x060079E2 RID: 31202 RVA: 0x002895F6 File Offset: 0x002877F6
		private void OnDeckBackButton()
		{
			NKCLocalDeckDataManager.SaveLocalDeck();
			NKCUIDeckViewer.CheckInstanceAndClose();
			if (this.m_dOnDeckConfirm != null)
			{
				this.m_dOnDeckConfirm();
			}
		}

		// Token: 0x060079E3 RID: 31203 RVA: 0x00289618 File Offset: 0x00287818
		private void OnSquadButtonDown(PointerEventData pointEventData)
		{
			if (this.m_operationLowDelta <= 0)
			{
				return;
			}
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			string text = null;
			foreach (NKMTrimCombatPenaltyTemplet nkmtrimCombatPenaltyTemplet in ((nkmtrimTemplet != null) ? nkmtrimTemplet.TrimCombatPenaltyList : null))
			{
				text = nkmtrimCombatPenaltyTemplet.BattleConditionId;
				if (nkmtrimCombatPenaltyTemplet.LowCombatRate > this.m_operationLowDelta)
				{
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(text);
			if (templetByStrID == null)
			{
				return;
			}
			string arg;
			if (this.m_operationLowDelta / 10 % 10 == 0 && this.m_operationLowDelta / 100 == 0)
			{
				arg = ((float)this.m_operationLowDelta / 100f).ToString("F2");
			}
			else
			{
				arg = ((float)this.m_operationLowDelta / 100f).ToString("F1");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format(NKCUtilString.GET_STRING_TRIM_SQUAD_COMBAT_PENALTY, arg));
			stringBuilder.Append("\n");
			stringBuilder.Append(templetByStrID.BattleCondDesc_Translated);
			NKCUITrimToolTip.Instance.Open(stringBuilder.ToString(), new Vector2?(pointEventData.position));
		}

		// Token: 0x060079E4 RID: 31204 RVA: 0x0028974C File Offset: 0x0028794C
		private void OnClickBoss()
		{
			List<NKMTrimDungeonTemplet> list = null;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			if (nkmtrimTemplet != null)
			{
				nkmtrimTemplet.TrimDungeonTemplets.TryGetValue(this.m_trimDungeonId, out list);
			}
			NKMTrimDungeonTemplet nkmtrimDungeonTemplet = (list != null) ? list.Find((NKMTrimDungeonTemplet e) => e.TrimLevelLow <= this.m_trimLevel && e.TrimLevelHigh >= this.m_trimLevel) : null;
			NKCPopupEnemyList.Instance.Open((nkmtrimDungeonTemplet != null) ? nkmtrimDungeonTemplet.DungeonTempletBase : null);
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x002897AE File Offset: 0x002879AE
		private void OnDestroy()
		{
			this.m_dOnDeckConfirm = null;
		}

		// Token: 0x0400669E RID: 26270
		public GameObject m_objWarning;

		// Token: 0x0400669F RID: 26271
		public GameObject m_objShipRoot;

		// Token: 0x040066A0 RID: 26272
		public GameObject m_objAddImage;

		// Token: 0x040066A1 RID: 26273
		public GameObject m_objStageNormalState;

		// Token: 0x040066A2 RID: 26274
		public GameObject m_objStageLowPowerState;

		// Token: 0x040066A3 RID: 26275
		public Image m_imgShip;

		// Token: 0x040066A4 RID: 26276
		public Image m_imgBoss;

		// Token: 0x040066A5 RID: 26277
		public Text m_lbShipLevel;

		// Token: 0x040066A6 RID: 26278
		public Text m_lbStageNumberOn;

		// Token: 0x040066A7 RID: 26279
		public Text m_lbStageNumberOff;

		// Token: 0x040066A8 RID: 26280
		public NKCUIComStateButton m_csbtnAddSquad;

		// Token: 0x040066A9 RID: 26281
		public NKCUIComStateButton m_csbtnWarning;

		// Token: 0x040066AA RID: 26282
		public NKCUIComStateButton m_csbtnBoss;

		// Token: 0x040066AB RID: 26283
		public GameObject m_objSquadNormal;

		// Token: 0x040066AC RID: 26284
		public GameObject m_objSquadWarning;

		// Token: 0x040066AD RID: 26285
		public Text m_lbSquadPowerNormal;

		// Token: 0x040066AE RID: 26286
		public Text m_lbSquadPowerWarning;

		// Token: 0x040066AF RID: 26287
		private int m_slotIndex;

		// Token: 0x040066B0 RID: 26288
		private int m_trimId;

		// Token: 0x040066B1 RID: 26289
		private int m_trimDungeonId;

		// Token: 0x040066B2 RID: 26290
		private int m_trimLevel;

		// Token: 0x040066B3 RID: 26291
		private int m_operationLowDelta;

		// Token: 0x040066B4 RID: 26292
		private NKCUITrimSquadSlot.OnDeckConfirm m_dOnDeckConfirm;

		// Token: 0x0200180E RID: 6158
		// (Invoke) Token: 0x0600B4FE RID: 46334
		public delegate void OnDeckConfirm();
	}
}
