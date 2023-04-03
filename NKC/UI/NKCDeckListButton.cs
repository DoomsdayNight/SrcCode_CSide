using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000960 RID: 2400
	public class NKCDeckListButton : MonoBehaviour
	{
		// Token: 0x06005FC0 RID: 24512 RVA: 0x001DCCC0 File Offset: 0x001DAEC0
		public void Init(int index, NKCDeckListButton.dOnChangedSelected _dOnChangedSelected = null)
		{
			this.m_DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, index);
			this.m_dOnChangedSelected = _dOnChangedSelected;
			this.m_lbIndex.text = this.GetIndexText(index);
			this.m_lbIndexForMulti.text = this.GetIndexText(index);
			this.m_ctToggleForMulti.OnValueChanged.RemoveAllListeners();
			this.m_ctToggleForMulti.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedMultiSelected));
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x001DCD31 File Offset: 0x001DAF31
		private void OnChangedMultiSelected(bool bSet)
		{
			if (this.m_dOnChangedSelected != null)
			{
				this.m_dOnChangedSelected((int)this.m_DeckIndex.m_iIndex, bSet);
			}
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x001DCD54 File Offset: 0x001DAF54
		public void SetMultiSelect(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSingle, !bValue);
			NKCUtil.SetGameobjectActive(this.m_objMulti, bValue);
			if (bValue)
			{
				this.m_cbtnButton.m_ButtonBG_Selected = this.m_BG_Selected_Multi;
				this.m_cbtnButton.m_ButtonBG_Locked = this.m_BG_Locked_Multi;
				this.m_cbtnButton.m_ButtonBG_Normal = this.m_BG_On_Multi;
				return;
			}
			this.m_cbtnButton.m_ButtonBG_Selected = this.m_BG_Selected;
			this.m_cbtnButton.m_ButtonBG_Locked = this.m_BG_Locked;
			this.m_cbtnButton.m_ButtonBG_Normal = this.m_BG_On;
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x001DCDE8 File Offset: 0x001DAFE8
		public void SetTrimDeckSelect(bool isTrimSquad)
		{
			if (isTrimSquad)
			{
				NKCUtil.SetGameobjectActive(this.m_objSingle, false);
				NKCUtil.SetGameobjectActive(this.m_objMulti, false);
				NKCUtil.SetLabelText(this.m_lbTrimDeckIndexNormal, ((int)(this.m_DeckIndex.m_iIndex + 1)).ToString());
				NKCUtil.SetLabelText(this.m_lbTrimDeckIndexSelected, ((int)(this.m_DeckIndex.m_iIndex + 1)).ToString());
				this.m_cbtnButton.m_ButtonBG_Selected = this.m_Trim_Selected;
				this.m_cbtnButton.m_ButtonBG_Normal = this.m_Trim_Normal;
			}
			NKCUtil.SetGameobjectActive(this.m_objTrim, isTrimSquad);
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x001DCE80 File Offset: 0x001DB080
		private string GetIndexText(int index)
		{
			return (index + 1).ToString();
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x001DCE98 File Offset: 0x001DB098
		private string GetStateText(NKMDeckData deckData)
		{
			if (deckData == null)
			{
				return "";
			}
			if (!string.IsNullOrEmpty(this.m_stateText))
			{
				return this.m_stateText;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = deckData.IsValidState();
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DOING)
			{
				return NKCUtilString.GET_STRING_DECK_STATE_DOING_MISSION;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING)
			{
				return NKCUtilString.GET_STRING_DECK_STATE_DOING_WARFARE;
			}
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING)
			{
				return NKCUIDeckViewer.GetDeckName(this.m_DeckIndex);
			}
			return NKCUtilString.GET_STRING_DECK_STATE_DOING_DIVE;
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x001DCF00 File Offset: 0x001DB100
		public void SetData(NKMArmyData armyData, NKMDeckIndex deckIndex, string stateText = "")
		{
			this.m_DeckIndex = deckIndex;
			NKMDeckData deckData = armyData.GetDeckData(this.m_DeckIndex);
			this.m_DeckData = deckData;
			this.m_stateText = stateText;
			this.UpdateUI();
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x001DCF38 File Offset: 0x001DB138
		public void UpdateUI()
		{
			bool bSelect = this.m_cbtnButton.m_bSelect;
			bool flag = this.m_DeckData != null && this.m_DeckData.IsValidState() == NKM_ERROR_CODE.NEC_OK;
			if (!string.IsNullOrEmpty(this.m_stateText))
			{
				flag = true;
			}
			if (flag && bSelect)
			{
				this.m_lbIndex.color = this.m_colTextSelected;
				this.m_lbIndexForMulti.color = this.m_colTextSelected;
				this.m_lbDeckState.color = this.m_colTextSelected;
				this.m_lbDeckStateForMulti.color = this.m_colTextSelected;
				this.m_lbSQUAD.color = this.m_colTextSelected;
				this.m_lbSQUADForMulti.color = this.m_colTextSelected;
			}
			else
			{
				this.m_lbIndex.color = this.m_colTextNormal;
				this.m_lbIndexForMulti.color = this.m_colTextNormal;
				this.m_lbDeckState.color = this.m_colTextNormal;
				this.m_lbDeckStateForMulti.color = this.m_colTextNormal;
				this.m_lbSQUAD.color = this.m_colTextSQUADNormal;
				this.m_lbSQUADForMulti.color = this.m_colTextSQUADNormal;
			}
			if (!flag)
			{
				this.m_imgBGNormal.color = this.m_colImgBGUnusable;
				this.m_imgBGNormalForMulti.color = this.m_colImgBGUnusable;
				this.m_imgBGSelected.color = this.m_colImgBGUnusable;
				this.m_imgBGSelectedForMulti.color = this.m_colImgBGUnusable;
			}
			else
			{
				this.m_imgBGNormal.color = this.m_colImgBGNormal;
				this.m_imgBGNormalForMulti.color = this.m_colImgBGNormal;
				this.m_imgBGSelected.color = this.m_colImgBGSelected;
				this.m_imgBGSelectedForMulti.color = this.m_colImgBGSelected;
			}
			this.m_lbDeckState.text = this.GetStateText(this.m_DeckData);
			this.m_lbDeckStateForMulti.text = this.GetStateText(this.m_DeckData);
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x001DD108 File Offset: 0x001DB308
		public void Lock()
		{
			this.m_cbtnButton.Lock(true);
			NKCUtil.SetGameobjectActive(this.m_objTextRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objTextRootForMulti, false);
			NKCUtil.SetGameobjectActive(this.m_objToggleForMulti, false);
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x001DD13A File Offset: 0x001DB33A
		public void UnLock()
		{
			this.m_cbtnButton.UnLock(true);
			NKCUtil.SetGameobjectActive(this.m_objTextRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objTextRootForMulti, true);
			NKCUtil.SetGameobjectActive(this.m_objToggleForMulti, true);
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x001DD16C File Offset: 0x001DB36C
		public void ButtonSelect()
		{
			this.m_cbtnButton.Select(true, false, false);
			this.UpdateUI();
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x001DD183 File Offset: 0x001DB383
		public void ButtonDeSelect()
		{
			this.m_cbtnButton.Select(false, false, false);
			this.UpdateUI();
		}

		// Token: 0x04004BE6 RID: 19430
		public NKMDeckIndex m_DeckIndex;

		// Token: 0x04004BE7 RID: 19431
		public NKCUIComStateButton m_cbtnButton;

		// Token: 0x04004BE8 RID: 19432
		public GameObject m_BG_On;

		// Token: 0x04004BE9 RID: 19433
		public GameObject m_BG_Selected;

		// Token: 0x04004BEA RID: 19434
		public GameObject m_BG_Locked;

		// Token: 0x04004BEB RID: 19435
		public GameObject m_BG_On_Multi;

		// Token: 0x04004BEC RID: 19436
		public GameObject m_BG_Selected_Multi;

		// Token: 0x04004BED RID: 19437
		public GameObject m_BG_Locked_Multi;

		// Token: 0x04004BEE RID: 19438
		public GameObject m_Trim_Normal;

		// Token: 0x04004BEF RID: 19439
		public GameObject m_Trim_Selected;

		// Token: 0x04004BF0 RID: 19440
		public GameObject m_objSingle;

		// Token: 0x04004BF1 RID: 19441
		public GameObject m_objMulti;

		// Token: 0x04004BF2 RID: 19442
		public GameObject m_objTrim;

		// Token: 0x04004BF3 RID: 19443
		[Header("싱글")]
		public GameObject m_objTextRoot;

		// Token: 0x04004BF4 RID: 19444
		public Text m_lbIndex;

		// Token: 0x04004BF5 RID: 19445
		public Text m_lbDeckState;

		// Token: 0x04004BF6 RID: 19446
		public Text m_lbSQUAD;

		// Token: 0x04004BF7 RID: 19447
		public Image m_imgSupportIcon;

		// Token: 0x04004BF8 RID: 19448
		public Text m_lbSupportText;

		// Token: 0x04004BF9 RID: 19449
		public Image m_imgBGSelected;

		// Token: 0x04004BFA RID: 19450
		public Image m_imgBGNormal;

		// Token: 0x04004BFB RID: 19451
		[Header("멀티")]
		public GameObject m_objTextRootForMulti;

		// Token: 0x04004BFC RID: 19452
		public Text m_lbIndexForMulti;

		// Token: 0x04004BFD RID: 19453
		public Text m_lbDeckStateForMulti;

		// Token: 0x04004BFE RID: 19454
		public Text m_lbSQUADForMulti;

		// Token: 0x04004BFF RID: 19455
		public GameObject m_objToggleForMulti;

		// Token: 0x04004C00 RID: 19456
		public NKCUIComToggle m_ctToggleForMulti;

		// Token: 0x04004C01 RID: 19457
		public Image m_imgBGSelectedForMulti;

		// Token: 0x04004C02 RID: 19458
		public Image m_imgBGNormalForMulti;

		// Token: 0x04004C03 RID: 19459
		public Text m_lbMultiSelectedSeq;

		// Token: 0x04004C04 RID: 19460
		[Header("트림 전용")]
		public Text m_lbTrimDeckIndexNormal;

		// Token: 0x04004C05 RID: 19461
		public Text m_lbTrimDeckIndexSelected;

		// Token: 0x04004C06 RID: 19462
		[Header("컬러")]
		public Color m_colTextSelected;

		// Token: 0x04004C07 RID: 19463
		public Color m_colTextNormal;

		// Token: 0x04004C08 RID: 19464
		public Color m_colTextSQUADNormal;

		// Token: 0x04004C09 RID: 19465
		public Color m_colImgBGNormal;

		// Token: 0x04004C0A RID: 19466
		public Color m_colImgBGUnusable;

		// Token: 0x04004C0B RID: 19467
		public Color m_colImgBGSelected;

		// Token: 0x04004C0C RID: 19468
		private NKMDeckData m_DeckData;

		// Token: 0x04004C0D RID: 19469
		private string m_stateText = string.Empty;

		// Token: 0x04004C0E RID: 19470
		private NKCDeckListButton.dOnChangedSelected m_dOnChangedSelected;

		// Token: 0x020015DF RID: 5599
		// (Invoke) Token: 0x0600AE81 RID: 44673
		public delegate void dOnChangedSelected(int index, bool bSet);
	}
}
