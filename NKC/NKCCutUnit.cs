using System;
using DG.Tweening;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200079E RID: 1950
	public class NKCCutUnit : MonoBehaviour
	{
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x0016E916 File Offset: 0x0016CB16
		public GameObject GoUnit
		{
			get
			{
				return this.m_goUnit;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x0016E91E File Offset: 0x0016CB1E
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x0016E926 File Offset: 0x0016CB26
		public NKCUICharacterView CharacterView
		{
			get
			{
				return this.m_NKCUICharacterView;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x0016E92E File Offset: 0x0016CB2E
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x0016E936 File Offset: 0x0016CB36
		public NKCCutTemplet NKCCutTemplet
		{
			get
			{
				return this.m_NKCCutTemplet;
			}
			set
			{
				this.m_NKCCutTemplet = value;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x0016E93F File Offset: 0x0016CB3F
		// (set) Token: 0x06004C8C RID: 19596 RVA: 0x0016E947 File Offset: 0x0016CB47
		public NKCUICharacterView NKCCharacterView
		{
			get
			{
				return this.m_NKCUICharacterView;
			}
			set
			{
				this.m_NKCUICharacterView = value;
			}
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0016E950 File Offset: 0x0016CB50
		public void BounceUnit(int count, float time)
		{
			this.m_bFinished = false;
			this.m_bounceSequence = DOTween.Sequence();
			for (int i = 0; i < count; i++)
			{
				this.m_bounceSequence.Append(this.m_rectTransform.DOAnchorPosY(this.m_orgPosWithOffset.y + 50f, time, false));
				this.m_bounceSequence.Append(this.m_rectTransform.DOAnchorPosY(this.m_orgPosWithOffset.y, time, false));
			}
			this.m_bounceSequence.OnComplete(new TweenCallback(this.FinishUnit));
			this.m_bounceSequence.Play<Sequence>();
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0016E9ED File Offset: 0x0016CBED
		public void ManualUpdate()
		{
			this.UpdateUnitCrash();
			this.UpdateCanvasGroupAlpha();
			this.UpdateUnitColor();
			this.UpdateUnitAlpha();
			this.UpdateUnitScale();
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x0016EA10 File Offset: 0x0016CC10
		public void UpdateUnitPos()
		{
			if (this.m_NKCCutTemplet == null)
			{
				return;
			}
			if (this.m_bFinished)
			{
				return;
			}
			this.m_NKMTrackingVector2Pos.Update(Time.deltaTime);
			if (this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.IN || this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.OUT || this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.MOVE)
			{
				if (this.m_NKMTrackingVector2Pos.IsTracking())
				{
					Vector2 anchoredPosition = this.m_rectTransform.anchoredPosition;
					anchoredPosition = this.m_NKMTrackingVector2Pos.GetNowValue();
					this.m_rectTransform.anchoredPosition = anchoredPosition;
					return;
				}
				this.FinishUnit();
			}
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0016EAA8 File Offset: 0x0016CCA8
		public void UpdateUnitCrash()
		{
			if (this.m_crash <= 0)
			{
				return;
			}
			if (this.m_goUnit == null)
			{
				return;
			}
			if (!this.m_goUnit.activeSelf)
			{
				return;
			}
			if (!this.m_NKMTrackingVector2Crash.IsTracking())
			{
				this.m_NKMTrackingVector2Crash.SetTracking(new Vector2(this.m_orgPosWithOffset.x + NKMRandom.Range(-50f * (float)this.m_crash / 100f, 50f * (float)this.m_crash / 100f), this.m_orgPosWithOffset.y + NKMRandom.Range(-50f * (float)this.m_crash / 100f, 50f * (float)this.m_crash / 100f)), 0.025f, TRACKING_DATA_TYPE.TDT_NORMAL);
			}
			this.m_NKMTrackingVector2Crash.Update(Time.deltaTime);
			NKMVector2 nowValue = this.m_NKMTrackingVector2Crash.GetNowValue();
			Vector2 anchoredPosition = new Vector2(nowValue.x, nowValue.y);
			this.m_rectTransform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0016EBB0 File Offset: 0x0016CDB0
		public void UpdateCanvasGroupAlpha()
		{
			if (this.m_canvasGroup.alpha >= 1f)
			{
				return;
			}
			if (!this.m_goUnit.activeSelf)
			{
				return;
			}
			this.m_canvasGroup.alpha += Time.deltaTime * 5f;
			if (this.m_canvasGroup.alpha >= 1f)
			{
				this.m_canvasGroup.alpha = 1f;
			}
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0016EC20 File Offset: 0x0016CE20
		public void UpdateUnitColor()
		{
			if (!this.m_NKMTrackingFloatUnitColor.IsTracking())
			{
				return;
			}
			if (this.m_NKCUICharacterView == null)
			{
				return;
			}
			if (!this.m_NKCUICharacterView.HasCharacterIllust())
			{
				return;
			}
			this.m_NKMTrackingFloatUnitColor.Update(Time.deltaTime);
			this.m_NKCUICharacterView.SetColor(this.m_NKMTrackingFloatUnitColor.GetNowValue(), this.m_NKMTrackingFloatUnitColor.GetNowValue(), this.m_NKMTrackingFloatUnitColor.GetNowValue(), -1f, true);
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0016EC9C File Offset: 0x0016CE9C
		public void UpdateUnitAlpha()
		{
			if (!this.m_NKMTrackingFloatUnitAlpha.IsTracking())
			{
				return;
			}
			if (this.m_NKCUICharacterView == null)
			{
				return;
			}
			if (!this.m_NKCUICharacterView.HasCharacterIllust())
			{
				return;
			}
			this.m_NKMTrackingFloatUnitAlpha.Update(Time.deltaTime);
			this.m_NKCUICharacterView.SetColor(1f, 1f, 1f, this.m_NKMTrackingFloatUnitAlpha.GetNowValue(), true);
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0016ED0C File Offset: 0x0016CF0C
		public void UpdateUnitScale()
		{
			if (!this.m_NKMTrackingVector3UnitScale.IsTracking())
			{
				return;
			}
			if (this.m_rectTransform == null)
			{
				return;
			}
			this.m_NKMTrackingVector3UnitScale.Update(Time.deltaTime);
			this.m_rectTransform.localScale = this.m_NKMTrackingVector3UnitScale.GetNowValue();
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0016ED61 File Offset: 0x0016CF61
		public void StopUnitCrash()
		{
			if (this.m_crash <= 0)
			{
				return;
			}
			this.m_crash = 0;
			this.m_rectTransform.anchoredPosition = this.m_orgPosWithOffset;
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0016ED88 File Offset: 0x0016CF88
		public void FinishUnit()
		{
			this.m_bFinished = true;
			this.m_crash = 0;
			this.m_rectTransform.anchoredPosition = this.m_orgPosWithOffset;
			if (this.m_bounceSequence.IsActive())
			{
				this.m_bounceSequence.Kill(false);
			}
			this.OnFinishedAlphaTracking();
			this.OnFinishedScaleTracking();
			this.m_canvasGroup.alpha = 1f;
			if (this.m_NKCCutTemplet != null && this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.OUT && this.m_goUnit.activeSelf)
			{
				this.m_goUnit.SetActive(false);
			}
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0016EE18 File Offset: 0x0016D018
		private void OnFinishedAlphaTracking()
		{
			if (!this.m_NKMTrackingFloatUnitAlpha.IsTracking())
			{
				return;
			}
			this.m_NKCUICharacterView.SetColor(1f, 1f, 1f, this.m_NKMTrackingFloatUnitAlpha.GetTargetValue(), true);
			this.m_NKMTrackingFloatUnitAlpha.Init();
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0016EE64 File Offset: 0x0016D064
		private void OnFinishedScaleTracking()
		{
			if (!this.m_NKMTrackingVector3UnitScale.IsTracking())
			{
				return;
			}
			this.m_rectTransform.localScale = this.m_NKMTrackingVector3UnitScale.GetTargetValue();
			this.m_NKMTrackingVector3UnitScale.StopTracking();
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0016EE9C File Offset: 0x0016D09C
		public void InitCutUnit()
		{
			this.m_rectTransform = base.GetComponent<RectTransform>();
			this.m_canvasGroup = base.GetComponent<CanvasGroup>();
			this.m_NKCUICharacterView = base.GetComponent<NKCUICharacterView>();
			this.m_goUnit = base.gameObject;
			this.m_orgPos = this.m_rectTransform.anchoredPosition;
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x0016EEEA File Offset: 0x0016D0EA
		public bool IsFinished()
		{
			return this.m_bFinished && !this.m_NKMTrackingFloatUnitAlpha.IsTracking() && !this.m_NKMTrackingVector3UnitScale.IsTracking();
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0016EF14 File Offset: 0x0016D114
		public Color GetStartColor(bool bDarkStart)
		{
			if (!bDarkStart)
			{
				return new Color(1f, 1f, 1f, 1f);
			}
			NKCUICharacterView nkcuicharacterView = this.m_NKCUICharacterView;
			if (nkcuicharacterView != null && nkcuicharacterView.GetCurrEffectType() == NKCUICharacterView.EffectType.Hologram)
			{
				return new Color(0.25f, 0.25f, 0.25f, 0.5f);
			}
			return new Color(0.25f, 0.25f, 0.25f, 1f);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0016EF88 File Offset: 0x0016D188
		public Vector2 GetStartPos()
		{
			if (this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.MOVE)
			{
				return NKCUICutScenUnitMgr.GetCutScenUnitMgr().GetUnitRectTransform(this.m_NKCCutTemplet.m_StartPosType).anchoredPosition;
			}
			return this.m_orgPosWithOffset + this.m_NKCCutTemplet.m_StartPos;
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0016EFD4 File Offset: 0x0016D1D4
		public Vector2 GetTargetPos()
		{
			return this.m_orgPosWithOffset + this.m_NKCCutTemplet.m_TargetPos;
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0016EFEC File Offset: 0x0016D1EC
		public void SetUnit(NKCCutScenCharTemplet cNKCCutScenCharTemplet, NKCCutTemplet cNKCCutTemplet)
		{
			this.m_NKCCutTemplet = cNKCCutTemplet;
			this.m_NKMTrackingFloatUnitAlpha.StopTracking();
			this.m_NKMTrackingVector3UnitScale.StopTracking();
			this.m_NKMTrackingFloatUnitColor.StopTracking();
			if (cNKCCutTemplet.CutUnitAction == CutUnitPosAction.MOVE && cNKCCutTemplet.CutUnitPos != cNKCCutTemplet.m_StartPosType)
			{
				NKCUICutScenUnitMgr.GetCutScenUnitMgr().ClearUnitByPos(cNKCCutTemplet, cNKCCutTemplet.m_StartPosType);
			}
			this.m_bFinished = true;
			this.SetUnitIllust(cNKCCutScenCharTemplet, cNKCCutTemplet);
			this.SetUnitScaleByTime(cNKCCutTemplet.m_CharScale, cNKCCutTemplet.m_CharScaleTime);
			if (!this.m_goUnit.activeSelf)
			{
				this.m_goUnit.SetActive(true);
			}
			if (cNKCCutTemplet.m_bFlip)
			{
				this.m_rectTransform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				this.m_rectTransform.localScale = new Vector3(1f, 1f, 1f);
			}
			this.m_crash = cNKCCutTemplet.m_Crash;
			this.m_orgPosWithOffset = new Vector2(this.m_orgPos.x + cNKCCutTemplet.m_CharOffSet.x, this.m_orgPos.y + cNKCCutTemplet.m_CharOffSet.y);
			if (cNKCCutTemplet.CutUnitAction == CutUnitPosAction.IN || cNKCCutTemplet.CutUnitAction == CutUnitPosAction.MOVE)
			{
				this.SetUnitMovePos(this.GetStartPos(), this.GetTargetPos());
			}
			this.m_rectTransform.anchoredPosition = this.m_orgPosWithOffset;
			NKCUICutScenUnitMgr.GetCutScenUnitMgr().DarkenOtherUnitColor(this.m_NKCCutTemplet.CutUnitPos);
			if (cNKCCutTemplet.m_BounceCount > 0)
			{
				this.BounceUnit(cNKCCutTemplet.m_BounceCount, cNKCCutTemplet.m_BounceTime);
			}
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0016F178 File Offset: 0x0016D378
		public void ClearUnit(NKCCutTemplet cNKCCutTemplet, bool bNone, bool bCur)
		{
			this.m_NKMTrackingFloatUnitAlpha.StopTracking();
			this.m_NKMTrackingVector3UnitScale.StopTracking();
			this.m_rectTransform.localScale = Vector3.one;
			this.NKCCharacterView.SetColor(1f, 1f, 1f, 1f, false);
			if (bNone)
			{
				if (this.m_goUnit.activeSelf)
				{
					this.m_goUnit.SetActive(false);
				}
				if (this.m_NKCUICharacterView != null)
				{
					this.m_NKCUICharacterView.CleanupAllEffect();
				}
				this.FinishUnit();
				return;
			}
			if (bCur)
			{
				this.m_NKCCutTemplet = cNKCCutTemplet;
				this.m_bFinished = true;
				this.m_crash = 0;
				this.m_rectTransform.anchoredPosition = this.m_orgPosWithOffset;
				this.m_rectTransform.localScale = Vector3.one;
				this.m_canvasGroup.alpha = 1f;
				this.DoClear(this.GetStartPos(), this.GetTargetPos());
			}
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0016F264 File Offset: 0x0016D464
		public void SetUnitIllust(NKCCutScenCharTemplet cNKCCutScenCharTemplet, NKCCutTemplet cNKCCutTemplet)
		{
			NKCUICharacterView nkcuicharacterView = this.m_NKCUICharacterView;
			string prefab = this.m_prefab;
			this.m_prefab = cNKCCutScenCharTemplet.m_PrefabStr;
			bool flag = false;
			if (this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.DARK)
			{
				flag = true;
			}
			Color startColor = this.GetStartColor(flag);
			if (prefab != cNKCCutScenCharTemplet.m_PrefabStr)
			{
				NKCUICharacterView nkcuicharacterView2 = nkcuicharacterView;
				bool flag2 = false;
				if (nkcuicharacterView2 != null && nkcuicharacterView2.HasCharacterIllust())
				{
					nkcuicharacterView2.SetColor(1f, 1f, 1f, 1f, true);
					if (nkcuicharacterView2.IsDiffrentCharacter(cNKCCutScenCharTemplet.m_PrefabStr))
					{
						if (cNKCCutTemplet.m_bCharFadeOut)
						{
							this.SetUnitFadeOutByTime(1.5f);
						}
						else if (flag)
						{
							this.SetUnitAlpha(1f);
						}
						else if (cNKCCutTemplet.m_bCharFadeIn)
						{
							this.SetUnitFadeInByTime(1.5f);
						}
						else
						{
							this.WhitenUnit();
						}
					}
					flag2 = true;
				}
				if (nkcuicharacterView != null)
				{
					nkcuicharacterView.SetCharacterIllust(cNKCCutScenCharTemplet.m_PrefabStr, cNKCCutScenCharTemplet.m_Background, false, false, 0);
					nkcuicharacterView.SetColor(startColor.r, startColor.g, startColor.b, startColor.a, true);
				}
				if (!flag2)
				{
					if (cNKCCutTemplet.m_bCharFadeOut)
					{
						this.SetUnitFadeOutByTime(1.5f);
					}
					else if (flag)
					{
						this.SetUnitAlpha(1f);
					}
					else if (cNKCCutTemplet.m_bCharFadeIn)
					{
						this.SetUnitFadeInByTime(1.5f);
					}
					else if (cNKCCutTemplet.CutUnitAction != CutUnitPosAction.MOVE)
					{
						this.WhitenUnit();
					}
				}
			}
			else
			{
				if (nkcuicharacterView != null)
				{
					nkcuicharacterView.SetColor(startColor.r, startColor.g, startColor.b, startColor.a, true);
					nkcuicharacterView.SetCharacterIllustBackgroundEnable(cNKCCutScenCharTemplet.m_Background);
					nkcuicharacterView.SetSkinOption(0);
				}
				if (cNKCCutTemplet.m_bCharFadeOut)
				{
					this.SetUnitFadeOutByTime(1.5f);
				}
			}
			if (nkcuicharacterView != null)
			{
				this.m_goUnit.transform.SetAsLastSibling();
				if (cNKCCutTemplet.CutUnitAction == CutUnitPosAction.MOVE)
				{
					NKCASUIUnitIllust unitIllust = NKCUICutScenUnitMgr.GetCutScenUnitMgr().GetUnitCharacterView(cNKCCutTemplet.m_StartPosType).GetUnitIllust();
					if (NKCUICutScenUnitMgr.GetCutScenUnitMgr().GetUnitCutTemplet(cNKCCutTemplet.m_StartPosType).m_Face == cNKCCutTemplet.m_Face)
					{
						float currentAnimationTime = unitIllust.GetCurrentAnimationTime(0);
						nkcuicharacterView.SetAnimationTrackTime(currentAnimationTime);
					}
				}
				if (cNKCCutTemplet.m_Face != NKCASUIUnitIllust.eAnimation.NONE)
				{
					nkcuicharacterView.SetAnimation(cNKCCutTemplet.m_Face, cNKCCutTemplet.m_bFaceLoop);
				}
				if (cNKCCutTemplet.m_bCharHologramEffect)
				{
					nkcuicharacterView.PlayEffect(NKCUICharacterView.EffectType.Hologram);
				}
				nkcuicharacterView.SetPinup(cNKCCutTemplet.m_bCharPinup, cNKCCutTemplet.m_bCharPinupEasingTime);
			}
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0016F4C1 File Offset: 0x0016D6C1
		public void SetUnitFadeInByTime(float time)
		{
			this.SetUnitFadeAlpha(time, true);
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0016F4CB File Offset: 0x0016D6CB
		public void SetUnitFadeOutByTime(float time)
		{
			this.SetUnitFadeAlpha(time, false);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0016F4D8 File Offset: 0x0016D6D8
		private void SetUnitFadeAlpha(float time, bool fadeIn)
		{
			if (this.m_NKMTrackingFloatUnitAlpha != null)
			{
				float nowValue = (float)(fadeIn ? 0 : 1);
				float targetVal = (float)(fadeIn ? 1 : 0);
				this.m_NKMTrackingFloatUnitAlpha.SetNowValue(nowValue);
				this.m_NKMTrackingFloatUnitAlpha.SetTracking(targetVal, time, TRACKING_DATA_TYPE.TDT_NORMAL);
			}
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0016F519 File Offset: 0x0016D719
		public void SetUnitAlpha(float fAlpha)
		{
			this.m_canvasGroup.alpha = fAlpha;
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x0016F527 File Offset: 0x0016D727
		public void WhitenUnit()
		{
			if (this.m_NKCUICharacterView == null)
			{
				return;
			}
			if (!this.m_NKCUICharacterView.HasCharacterIllust())
			{
				return;
			}
			this.SetUnitColorMultiplier(0f, 1f, 0.2f, TRACKING_DATA_TYPE.TDT_NORMAL);
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0016F55C File Offset: 0x0016D75C
		public void DarkenUnit()
		{
			if (this.m_NKCUICharacterView == null)
			{
				return;
			}
			if (!this.m_NKCUICharacterView.HasCharacterIllust())
			{
				return;
			}
			this.SetUnitColorMultiplier(this.m_NKCUICharacterView.GetColor().r, 0.25f, 0.15f, TRACKING_DATA_TYPE.TDT_NORMAL);
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0016F59C File Offset: 0x0016D79C
		public void SetUnitColorMultiplier(float nowValue, float targetValue, float trackingTime, TRACKING_DATA_TYPE trackingDataType)
		{
			this.m_NKMTrackingFloatUnitColor.SetNowValue(nowValue);
			this.m_NKMTrackingFloatUnitColor.SetTracking(targetValue, trackingTime, trackingDataType);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0016F5BC File Offset: 0x0016D7BC
		public void SetUnitScaleByTime(Vector2 m_vScaleXY, float fTime)
		{
			if (m_vScaleXY.x == 0f && m_vScaleXY.y == 0f && fTime == 0f)
			{
				return;
			}
			if (this.m_NKMTrackingVector3UnitScale != null && this.m_rectTransform != null)
			{
				NKMVector3 nv = new NKMVector3(this.m_rectTransform.localScale.x, this.m_rectTransform.localScale.y, 1f);
				this.m_NKMTrackingVector3UnitScale.SetNowValue(nv);
				this.m_NKMTrackingVector3UnitScale.SetTracking(m_vScaleXY, fTime, TRACKING_DATA_TYPE.TDT_NORMAL);
			}
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0016F654 File Offset: 0x0016D854
		public void SetUnitMovePos(Vector2 startPos, Vector2 targetPos)
		{
			if (startPos == targetPos)
			{
				return;
			}
			this.m_bFinished = false;
			this.m_NKMTrackingVector2Pos.SetNowValue(startPos);
			this.m_NKMTrackingVector2Pos.SetTracking(targetPos, this.m_NKCCutTemplet.m_TrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0016F6A0 File Offset: 0x0016D8A0
		public void DoClear(Vector2 startPos, Vector2 targetPos)
		{
			if (this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.PLACE)
			{
				if (this.m_goUnit.activeSelf)
				{
					this.m_goUnit.SetActive(false);
				}
			}
			else if (this.m_NKCCutTemplet.CutUnitAction == CutUnitPosAction.OUT)
			{
				this.SetUnitMovePos(startPos, targetPos);
				this.m_bFinished = false;
			}
			if (this.m_NKCUICharacterView != null)
			{
				this.m_NKCUICharacterView.CleanupAllEffect();
			}
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0016F70C File Offset: 0x0016D90C
		public void Close()
		{
			this.FinishUnit();
			if (this.m_goUnit.activeSelf)
			{
				this.m_goUnit.SetActive(false);
			}
			if (this.m_NKCUICharacterView && this.m_NKCUICharacterView.HasCharacterIllust())
			{
				this.m_NKCUICharacterView.SetColor(1f, 1f, 1f, 1f, true);
				this.m_NKCUICharacterView.CleanUp();
			}
			this.m_rectTransform.localScale = Vector3.one;
			this.m_NKCUICharacterView.SetColor(1f, 1f, 1f, 1f, false);
			this.m_prefab = "";
		}

		// Token: 0x04003C4E RID: 15438
		private const float DARK_COLOR = 0.25f;

		// Token: 0x04003C4F RID: 15439
		private const float UNIT_COLOR_CHANGE_TIME = 0.15f;

		// Token: 0x04003C50 RID: 15440
		private const float CRASH_DIST_DEFAULT_VALUE = 50f;

		// Token: 0x04003C51 RID: 15441
		private const float CRASH_DURATION_TIME = 0.025f;

		// Token: 0x04003C52 RID: 15442
		private const float BOUNCE_DIST_VALUE_Y = 50f;

		// Token: 0x04003C53 RID: 15443
		private NKCCutTemplet m_NKCCutTemplet;

		// Token: 0x04003C54 RID: 15444
		private GameObject m_goUnit;

		// Token: 0x04003C55 RID: 15445
		private RectTransform m_rectTransform;

		// Token: 0x04003C56 RID: 15446
		private Vector2 m_orgPos = new Vector2(0f, 0f);

		// Token: 0x04003C57 RID: 15447
		private Vector2 m_orgPosWithOffset = new Vector2(0f, 0f);

		// Token: 0x04003C58 RID: 15448
		private CanvasGroup m_canvasGroup;

		// Token: 0x04003C59 RID: 15449
		private NKCUICharacterView m_NKCUICharacterView;

		// Token: 0x04003C5A RID: 15450
		private string m_prefab = "";

		// Token: 0x04003C5B RID: 15451
		private int m_crash;

		// Token: 0x04003C5C RID: 15452
		private NKMTrackingFloat m_NKMTrackingFloatUnitColor = new NKMTrackingFloat();

		// Token: 0x04003C5D RID: 15453
		private readonly NKMTrackingFloat m_NKMTrackingFloatUnitAlpha = new NKMTrackingFloat();

		// Token: 0x04003C5E RID: 15454
		private NKMTrackingVector2 m_NKMTrackingVector2Pos = new NKMTrackingVector2();

		// Token: 0x04003C5F RID: 15455
		private NKMTrackingVector2 m_NKMTrackingVector2Crash = new NKMTrackingVector2();

		// Token: 0x04003C60 RID: 15456
		private readonly NKMTrackingVector3 m_NKMTrackingVector3UnitScale = new NKMTrackingVector3();

		// Token: 0x04003C61 RID: 15457
		private Sequence m_bounceSequence;

		// Token: 0x04003C62 RID: 15458
		private bool m_bFinished = true;
	}
}
