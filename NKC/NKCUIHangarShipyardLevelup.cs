using System;
using DG.Tweening;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A3 RID: 2467
	public class NKCUIHangarShipyardLevelup : MonoBehaviour
	{
		// Token: 0x060066D8 RID: 26328 RVA: 0x0020EE50 File Offset: 0x0020D050
		public void Init(UnityAction MinBtn, UnityAction PreBtn, UnityAction NextBtn, UnityAction MaxBtn)
		{
			if (MinBtn != null)
			{
				this.m_NKM_UI_SHIPYARD_LV_MIN.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_SHIPYARD_LV_MIN.PointerClick.AddListener(MinBtn);
			}
			if (PreBtn != null)
			{
				this.m_NKM_UI_SHIPYARD_LV_PREV.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_SHIPYARD_LV_PREV.PointerClick.AddListener(PreBtn);
			}
			if (NextBtn != null)
			{
				this.m_NKM_UI_SHIPYARD_LV_NEXT.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_SHIPYARD_LV_NEXT.PointerClick.AddListener(NextBtn);
			}
			if (MaxBtn != null)
			{
				this.m_NKM_UI_SHIPYARD_LV_MAX.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_SHIPYARD_LV_MAX.PointerClick.AddListener(MaxBtn);
			}
			if (this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY != null)
			{
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY = this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.gameObject.GetComponent<RectTransform>();
				NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.gameObject, false);
			}
			if (this.m_txt_NKM_UI_SHIPYARD_LV_TEXT != null)
			{
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT = this.m_txt_NKM_UI_SHIPYARD_LV_TEXT.gameObject.GetComponent<RectTransform>();
			}
			this.InitUI();
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x0020EF50 File Offset: 0x0020D150
		private void InitUI()
		{
			this.m_img_NKM_UI_SHIPYARD_LV_NEXT_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_MAX_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_NEXT.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_MAX.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_NEXT.color = NKCUtil.GetColor("#4E4E4E");
			this.m_img_NKM_UI_SHIPYARD_LV_MAX.color = NKCUtil.GetColor("#4E4E4E");
			this.m_img_NKM_UI_SHIPYARD_LV_MIN_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_PREV_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_MIN.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_PREV.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_MIN.color = NKCUtil.GetColor("#4E4E4E");
			this.m_img_NKM_UI_SHIPYARD_LV_PREV.color = NKCUtil.GetColor("#4E4E4E");
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_LevelUp_buttons, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_LevelUp_MAXLEVEL_ROOT, false);
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x0020F064 File Offset: 0x0020D264
		public void OnClickMaximumButton()
		{
			this.m_img_NKM_UI_SHIPYARD_LV_NEXT_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_MAX_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_NEXT.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_MAX.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_NEXT.color = NKCUtil.GetColor("#4E4E4E");
			this.m_img_NKM_UI_SHIPYARD_LV_MAX.color = NKCUtil.GetColor("#4E4E4E");
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x0020F0E8 File Offset: 0x0020D2E8
		public void UpdateShipData(int unitId, int starGrade, int curLv, int startLv, int curMaxLv, int selectLv = 0, bool canTryLvUp = false, int limitBreakLevel = 0)
		{
			this.m_iCurShipId = unitId;
			this.m_iCurShipStarGrade = starGrade;
			this.m_iCurShipLevel = curLv;
			this.m_iStartShipLevel = startLv;
			this.m_iCurrentShipMaxLevel = curMaxLv;
			this.m_iLimitBreakLevel = limitBreakLevel;
			this.UpdateTextUI(selectLv, true);
			this.m_iSelectLevel = selectLv;
			this.m_bCanTryLevelUp = canTryLvUp;
			this.UpdateButtonUI();
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x0020F144 File Offset: 0x0020D344
		private void UpdateTextUI(int newLevel, bool bAnimation = true)
		{
			if (!bAnimation)
			{
				this.m_txt_NKM_UI_SHIPYARD_LV_TEXT.text = string.Format(NKCUtilString.GET_STRING_HANGAR_SHIP_LEVEL_TWO_PARAM, newLevel, this.m_iCurrentShipMaxLevel);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.position = Vector3.zero;
				NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_SHIPYARD_LV_TEXT.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.gameObject, false);
				return;
			}
			if (this.m_iSelectLevel == 0)
			{
				this.m_txt_NKM_UI_SHIPYARD_LV_TEXT.text = string.Format(NKCUtilString.GET_STRING_HANGAR_SHIP_LEVEL_TWO_PARAM, newLevel, this.m_iCurrentShipMaxLevel);
				NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.gameObject, false);
				return;
			}
			this.m_txt_NKM_UI_SHIPYARD_LV_TEXT.text = string.Format(NKCUtilString.GET_STRING_HANGAR_SHIP_LEVEL_TWO_PARAM, this.m_iSelectLevel, this.m_iCurrentShipMaxLevel);
			this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.text = string.Format(NKCUtilString.GET_STRING_HANGAR_SHIP_LEVEL_TWO_PARAM, newLevel, this.m_iCurrentShipMaxLevel);
			if (newLevel > this.m_iSelectLevel)
			{
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.anchoredPosition = new Vector2(300f, this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.anchoredPosition.y);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.DOAnchorPosX(0f, 0.15f, false);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.anchoredPosition = new Vector2(0f, this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.anchoredPosition.y);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.DOAnchorPosX(-300f, 0.15f, false);
			}
			else
			{
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.anchoredPosition = new Vector2(-300f, this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.anchoredPosition.y);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.DOAnchorPosX(0f, 0.15f, false);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.anchoredPosition = new Vector2(0f, this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.anchoredPosition.y);
				this.m_rt_NKM_UI_SHIPYARD_LV_TEXT.DOAnchorPosX(300f, 0.15f, false);
			}
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY.gameObject, true);
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x0020F34C File Offset: 0x0020D54C
		private void UpdateButtonUI()
		{
			if (this.m_iCurrentShipMaxLevel == this.m_iCurShipLevel)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_LevelUp_buttons, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_LevelUp_MAXLEVEL_ROOT, true);
				NKCUIComStarRank starRank = this.m_StarRank;
				if (starRank != null)
				{
					starRank.SetStarRank(this.m_iCurShipStarGrade, this.m_iCurShipStarGrade, NKMShipManager.IsMaxLimitBreak(this.m_iCurShipId, this.m_iLimitBreakLevel));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_LevelUp_buttons, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIPYARD_LevelUp_MAXLEVEL_ROOT, false);
			}
			if (this.m_bCanTryLevelUp && this.m_iSelectLevel < this.m_iCurrentShipMaxLevel)
			{
				this.m_img_NKM_UI_SHIPYARD_LV_NEXT_ICON.color = NKCUtil.GetColor("#582817");
				this.m_img_NKM_UI_SHIPYARD_LV_MAX_ICON.color = NKCUtil.GetColor("#582817");
				this.m_img_NKM_UI_SHIPYARD_LV_NEXT.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
				this.m_img_NKM_UI_SHIPYARD_LV_MAX.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
				this.m_img_NKM_UI_SHIPYARD_LV_NEXT.color = Color.white;
				this.m_img_NKM_UI_SHIPYARD_LV_MAX.color = Color.white;
			}
			if (!this.m_bCanTryLevelUp || this.m_iCurrentShipMaxLevel == this.m_iSelectLevel)
			{
				this.m_img_NKM_UI_SHIPYARD_LV_NEXT_ICON.color = NKCUtil.GetColor("#222222");
				this.m_img_NKM_UI_SHIPYARD_LV_MAX_ICON.color = NKCUtil.GetColor("#222222");
				this.m_img_NKM_UI_SHIPYARD_LV_NEXT.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
				this.m_img_NKM_UI_SHIPYARD_LV_MAX.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
				this.m_img_NKM_UI_SHIPYARD_LV_NEXT.color = NKCUtil.GetColor("#4E4E4E");
				this.m_img_NKM_UI_SHIPYARD_LV_MAX.color = NKCUtil.GetColor("#4E4E4E");
			}
			if (this.m_iSelectLevel > this.m_iStartShipLevel)
			{
				this.m_img_NKM_UI_SHIPYARD_LV_MIN_ICON.color = NKCUtil.GetColor("#582817");
				this.m_img_NKM_UI_SHIPYARD_LV_PREV_ICON.color = NKCUtil.GetColor("#582817");
				this.m_img_NKM_UI_SHIPYARD_LV_MIN.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
				this.m_img_NKM_UI_SHIPYARD_LV_PREV.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
				this.m_img_NKM_UI_SHIPYARD_LV_MIN.color = Color.white;
				this.m_img_NKM_UI_SHIPYARD_LV_PREV.color = Color.white;
				return;
			}
			this.m_img_NKM_UI_SHIPYARD_LV_MIN_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_PREV_ICON.color = NKCUtil.GetColor("#222222");
			this.m_img_NKM_UI_SHIPYARD_LV_MIN.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_PREV.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			this.m_img_NKM_UI_SHIPYARD_LV_MIN.color = NKCUtil.GetColor("#4E4E4E");
			this.m_img_NKM_UI_SHIPYARD_LV_PREV.color = NKCUtil.GetColor("#4E4E4E");
		}

		// Token: 0x040052AF RID: 21167
		[Header("레벨 표시")]
		public GameObject m_NKM_UI_SHIPYARD_LevelUp_buttons;

		// Token: 0x040052B0 RID: 21168
		public GameObject m_NKM_UI_SHIPYARD_LevelUp_MAXLEVEL_ROOT;

		// Token: 0x040052B1 RID: 21169
		public NKCUIComStarRank m_StarRank;

		// Token: 0x040052B2 RID: 21170
		[Header("레벨업 정보")]
		public NKCUIComStateButton m_NKM_UI_SHIPYARD_LV_MIN;

		// Token: 0x040052B3 RID: 21171
		public Image m_img_NKM_UI_SHIPYARD_LV_MIN;

		// Token: 0x040052B4 RID: 21172
		public Image m_img_NKM_UI_SHIPYARD_LV_MIN_ICON;

		// Token: 0x040052B5 RID: 21173
		public NKCUIComStateButton m_NKM_UI_SHIPYARD_LV_PREV;

		// Token: 0x040052B6 RID: 21174
		public Image m_img_NKM_UI_SHIPYARD_LV_PREV;

		// Token: 0x040052B7 RID: 21175
		public Image m_img_NKM_UI_SHIPYARD_LV_PREV_ICON;

		// Token: 0x040052B8 RID: 21176
		public Text m_txt_NKM_UI_SHIPYARD_LV_TEXT;

		// Token: 0x040052B9 RID: 21177
		private RectTransform m_rt_NKM_UI_SHIPYARD_LV_TEXT;

		// Token: 0x040052BA RID: 21178
		public Text m_txt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY;

		// Token: 0x040052BB RID: 21179
		private RectTransform m_rt_NKM_UI_SHIPYARD_LV_TEXT_DUMMY;

		// Token: 0x040052BC RID: 21180
		public NKCUIComStateButton m_NKM_UI_SHIPYARD_LV_NEXT;

		// Token: 0x040052BD RID: 21181
		public Image m_img_NKM_UI_SHIPYARD_LV_NEXT;

		// Token: 0x040052BE RID: 21182
		public Image m_img_NKM_UI_SHIPYARD_LV_NEXT_ICON;

		// Token: 0x040052BF RID: 21183
		public NKCUIComStateButton m_NKM_UI_SHIPYARD_LV_MAX;

		// Token: 0x040052C0 RID: 21184
		public Image m_img_NKM_UI_SHIPYARD_LV_MAX;

		// Token: 0x040052C1 RID: 21185
		public Image m_img_NKM_UI_SHIPYARD_LV_MAX_ICON;

		// Token: 0x040052C2 RID: 21186
		private const int LevelTextMovePositionX = 300;

		// Token: 0x040052C3 RID: 21187
		private int m_iCurShipId;

		// Token: 0x040052C4 RID: 21188
		private int m_iCurShipStarGrade;

		// Token: 0x040052C5 RID: 21189
		private int m_iCurShipLevel;

		// Token: 0x040052C6 RID: 21190
		private int m_iStartShipLevel;

		// Token: 0x040052C7 RID: 21191
		private int m_iCurrentShipMaxLevel;

		// Token: 0x040052C8 RID: 21192
		private int m_iSelectLevel;

		// Token: 0x040052C9 RID: 21193
		private int m_iLimitBreakLevel;

		// Token: 0x040052CA RID: 21194
		private bool m_bCanTryLevelUp;

		// Token: 0x040052CB RID: 21195
		public const float BUTTON_DELAY_GAP = 0.15f;
	}
}
