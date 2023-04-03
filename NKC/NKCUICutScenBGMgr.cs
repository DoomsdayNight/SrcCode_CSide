using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI;
using NKM;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B5 RID: 1973
	public class NKCUICutScenBGMgr : MonoBehaviour
	{
		// Token: 0x06004E17 RID: 19991 RVA: 0x00177F83 File Offset: 0x00176183
		public void SetOrgPosAtResetToPlay(Vector2 orgPos)
		{
			this.m_orgPosAtResetToPlay = orgPos;
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00177F8C File Offset: 0x0017618C
		public void SetOrgScaleAtResetToPlay(Vector3 orgScale)
		{
			this.m_orgScaleAtResetToPlay = orgScale;
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x00177F95 File Offset: 0x00176195
		public void SetOrgPos(Vector2 orgPos)
		{
			this.m_orgPos = orgPos;
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00177FA0 File Offset: 0x001761A0
		public static void InitUI(GameObject goNKM_UI_CUTSCEN_PLAYER)
		{
			if (NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr != null)
			{
				return;
			}
			NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_BG_MGR").gameObject.GetComponent<NKCUICutScenBGMgr>();
			NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.SetOrgPosAtResetToPlay(NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.m_rtBG.anchoredPosition);
			NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.SetOrgScaleAtResetToPlay(NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.m_rtBG.localScale);
			NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.SetOrgPos(NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.m_rtBG.anchoredPosition);
			NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr.Close();
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00178030 File Offset: 0x00176230
		public void Reset()
		{
			this.m_NKMTrackingVector2.StopTracking();
			this.m_NKMTrackingVector2_2.StopTracking();
			this.m_NKMTrackingVector3.StopTracking();
			this.m_rtBG.anchoredPosition = this.m_orgPosAtResetToPlay;
			this.m_rtBG.localScale = this.m_orgScaleAtResetToPlay;
			this.m_orgPos = this.m_orgPosAtResetToPlay;
			this.m_bNoWaitBGAni = false;
			this.m_bAniPos = false;
			this.m_bAniScale = false;
			this.m_OffsetPos = new Vector2(0f, 0f);
			this.m_OffsetScale = new Vector3(1f, 1f, 1f);
			this.m_bPause = false;
			this.ClearGOBG();
			this.ClearGOBG_2();
			this.m_imgBG.enabled = false;
			this.m_imgBG_2.enabled = false;
			this.m_bImgBGTweening = false;
			this.m_bImgBG_2_Tweening = false;
			this.m_bSGGoBGTweening = false;
			this.m_bSGGoBG_2_Tweening = false;
			this.m_imgBG_2.enabled = false;
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00178123 File Offset: 0x00176323
		public static NKCUICutScenBGMgr GetCutScenBGMgr()
		{
			return NKCUICutScenBGMgr.m_scNKCUICutScenBGMgr;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0017812A File Offset: 0x0017632A
		private void SetPauseDoTweenObj(bool bPause, object obj)
		{
			if (obj != null && DOTween.IsTweening(obj, false))
			{
				if (bPause)
				{
					DOTween.Pause(obj);
					return;
				}
				DOTween.Play(obj);
			}
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0017814C File Offset: 0x0017634C
		public void SetPause(bool bPause)
		{
			this.m_bPause = bPause;
			this.SetPauseDoTweenObj(bPause, this.m_imgBG);
			this.SetPauseDoTweenObj(bPause, this.m_imgBG_2);
			this.SetPauseDoTweenObj(bPause, this.m_sgGoBG);
			if (this.m_sgGoBG != null)
			{
				if (bPause)
				{
					this.m_sgGoBG.AnimationState.TimeScale = 0f;
				}
				else
				{
					this.m_sgGoBG.AnimationState.TimeScale = 1f;
				}
			}
			this.SetPauseDoTweenObj(bPause, this.m_sgGoBG_2);
			if (this.m_sgGoBG_2 != null)
			{
				if (bPause)
				{
					this.m_sgGoBG_2.AnimationState.TimeScale = 0f;
					return;
				}
				this.m_sgGoBG_2.AnimationState.TimeScale = 1f;
			}
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00178210 File Offset: 0x00176410
		private void CheckAndStartCrash()
		{
			if (!this.m_NKMTrackingVector2.IsTracking())
			{
				this.m_NKMTrackingVector2.SetTracking(new Vector2(this.m_orgPos.x + NKMRandom.Range(-50f * this.m_Crash / 100f, 50f * this.m_Crash / 100f), this.m_orgPos.y + NKMRandom.Range(-50f * this.m_Crash / 100f, 50f * this.m_Crash / 100f)), 0.025f, TRACKING_DATA_TYPE.TDT_NORMAL);
			}
			this.m_NKMTrackingVector2.Update(Time.deltaTime);
			NKMVector2 nowValue = this.m_NKMTrackingVector2.GetNowValue();
			Vector2 anchoredPosition = new Vector2(nowValue.x, nowValue.y);
			if (this.m_rtGoBG != null)
			{
				this.m_rtGoBG.anchoredPosition = anchoredPosition;
				return;
			}
			this.m_rtBG.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00178308 File Offset: 0x00176508
		private void Update()
		{
			if (this.m_bPause)
			{
				return;
			}
			if (base.gameObject.activeSelf)
			{
				if (this.m_fCrashTime > 0f && this.m_Crash > 0f)
				{
					this.m_fElapsedTime += Time.deltaTime;
					if (this.m_fElapsedTime >= this.m_fCrashTime)
					{
						this.StopCrash();
						return;
					}
					this.CheckAndStartCrash();
					return;
				}
				else
				{
					if (this.m_bAniPos)
					{
						this.m_NKMTrackingVector2_2.Update(Time.deltaTime);
						if (this.m_NKMTrackingVector2_2.IsTracking())
						{
							if (this.m_rtGoBG != null)
							{
								this.m_rtGoBG.anchoredPosition = this.m_NKMTrackingVector2_2.GetNowUnityValue();
							}
							else
							{
								this.m_rtBG.anchoredPosition = this.m_NKMTrackingVector2_2.GetNowUnityValue();
							}
						}
						else
						{
							this.m_bAniPos = false;
							if (this.m_rtGoBG != null)
							{
								this.m_rtGoBG.anchoredPosition = this.m_OffsetPos;
								this.m_orgPos = this.m_rtGoBG.anchoredPosition;
							}
							else
							{
								this.m_rtBG.anchoredPosition = this.m_OffsetPos;
								this.m_orgPos = this.m_rtBG.anchoredPosition;
							}
						}
					}
					if (this.m_bAniScale)
					{
						this.m_NKMTrackingVector3.Update(Time.deltaTime);
						if (this.m_NKMTrackingVector3.IsTracking())
						{
							if (this.m_rtGoBG != null)
							{
								this.m_rtGoBG.localScale = new Vector3(this.m_OrgGOScale.x * this.m_NKMTrackingVector3.GetNowUnityValue().x, this.m_OrgGOScale.y * this.m_NKMTrackingVector3.GetNowUnityValue().y, this.m_OrgGOScale.z * this.m_NKMTrackingVector3.GetNowUnityValue().z);
								return;
							}
							this.m_rtBG.localScale = this.m_NKMTrackingVector3.GetNowUnityValue();
							return;
						}
						else
						{
							this.m_bAniScale = false;
							if (this.m_rtGoBG != null)
							{
								this.m_rtGoBG.localScale = new Vector3(this.m_OrgGOScale.x * this.m_OffsetScale.x, this.m_OrgGOScale.y * this.m_OffsetScale.y, this.m_OrgGOScale.z * this.m_OffsetScale.z);
								return;
							}
							this.m_rtBG.localScale = this.m_OffsetScale;
						}
					}
				}
			}
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00178568 File Offset: 0x00176768
		private void ClearGOBG()
		{
			if (this.m_goBG != null)
			{
				if (this.m_sgGoBG != null && this.m_bSGGoBGTweening)
				{
					this.m_sgGoBG.DOKill(true);
				}
				this.m_goBG.transform.SetParent(null);
				UnityEngine.Object.Destroy(this.m_goBG);
				this.m_goBG = null;
				this.m_rtGoBG = null;
				this.m_sgGoBG = null;
			}
			this.m_bSGGoBGTweening = false;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x001785E0 File Offset: 0x001767E0
		private void ClearGOBG_2()
		{
			if (this.m_goBG_2 != null)
			{
				if (this.m_sgGoBG_2 != null && DOTween.IsTweening(this.m_sgGoBG_2, false))
				{
					this.m_sgGoBG_2.DOKill(false);
				}
				this.m_goBG_2.transform.SetParent(null);
				UnityEngine.Object.Destroy(this.m_goBG_2);
				this.m_goBG_2 = null;
				this.m_rtGoBG_2 = null;
				this.m_sgGoBG_2 = null;
			}
			this.m_bSGGoBG_2_Tweening = false;
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0017865C File Offset: 0x0017685C
		private void ProcessGameObjectBG(GameObject tempGO)
		{
			if (this.m_goBG != null && this.m_fBGFadeOutTime > 0f)
			{
				this.m_goBG_2 = this.m_goBG;
				this.m_rtGoBG_2 = this.m_rtGoBG;
				this.m_sgGoBG_2 = this.m_sgGoBG;
				this.m_sgGoBG_2.color = this.m_sgGoBG.color;
				this.m_bSGGoBG_2_Tweening = true;
				this.m_sgGoBG_2.DOColor(this.m_colBGFadeOut, this.m_fBGFadeOutTime).SetEase(this.m_easeBGFadeOut).OnComplete(new TweenCallback(this.ClearGOBG_2));
			}
			else
			{
				this.ClearGOBG();
			}
			this.m_goBG = UnityEngine.Object.Instantiate<GameObject>(tempGO);
			if (this.m_goBG != null)
			{
				this.m_sgGoBG = this.m_goBG.GetComponentInChildren<SkeletonGraphic>();
				this.m_goBG.transform.SetParent(base.gameObject.transform);
				Transform transform = this.m_goBG.transform.Find("STAGE");
				RectTransform component;
				if (transform == null)
				{
					component = this.m_goBG.GetComponent<RectTransform>();
				}
				else
				{
					component = transform.GetComponent<RectTransform>();
					AspectRatioFitter component2 = this.m_goBG.GetComponent<AspectRatioFitter>();
					component2.enabled = false;
					component2.enabled = true;
				}
				if (component != null)
				{
					this.m_rtGoBG = component;
					float a = NKCUIManager.UIFrontCanvasSafeRectTransform.GetWidth() / 1920f;
					float b = NKCUIManager.UIFrontCanvasSafeRectTransform.GetHeight() / 1080f;
					float num = Mathf.Max(a, b);
					this.m_OrgGOScale = new Vector3(num, num, 1f);
					component.localScale = new Vector3(this.m_OrgGOScale.x * this.m_OffsetScale.x, this.m_OrgGOScale.y * this.m_OffsetScale.y, this.m_OrgGOScale.z * this.m_OffsetScale.z);
					component.offsetMin = new Vector2(0f, 0f);
					component.offsetMax = new Vector2(0f, 0f);
					component.anchoredPosition = this.m_OffsetPos;
					this.m_orgPos = component.anchoredPosition;
					return;
				}
				this.m_rtGoBG = null;
				this.m_goBG.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x001788A8 File Offset: 0x00176AA8
		public void Open(bool bGameObjectBGType, string bgFileName, string aniName, bool bGameObjectBGLoop, float fBGFadeInTime, Ease easeBGFadeIn, Color colBGFadeInStart, Color colBGFadeIn, float fBGFadeOutTime, Ease easeBGFadeOut, Color colBGFadeOut)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.m_colBGFadeIn = colBGFadeIn;
			if (bGameObjectBGType)
			{
				this.m_imgBG.enabled = false;
				this.m_imgBG_2.enabled = false;
				this.m_goBG != null;
				if ((!(this.m_goBG != null) || bgFileName.Length > 0 || aniName.Length <= 0) && bgFileName.Length > 0)
				{
					NKCAssetResourceData nkcassetResourceData = NKCResourceUtility.GetAssetResource(bgFileName, bgFileName);
					if (nkcassetResourceData != null)
					{
						GameObject asset = nkcassetResourceData.GetAsset<GameObject>();
						this.ProcessGameObjectBG(asset);
					}
					else
					{
						Debug.Log("Cutscen GameObjectBGType load, name : " + bgFileName);
						nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>(bgFileName, bgFileName, false, null);
						if (nkcassetResourceData != null)
						{
							if (nkcassetResourceData.m_RefCount <= 1)
							{
								Debug.LogWarning("Cutscen GameObjectBGType need to load in the past, name : " + bgFileName);
							}
							GameObject asset2 = nkcassetResourceData.GetAsset<GameObject>();
							this.ProcessGameObjectBG(asset2);
							this.m_lstNKCAssetResourceDataToClose.Add(nkcassetResourceData);
						}
					}
				}
				if (aniName.Length > 0 && this.m_goBG != null)
				{
					SkeletonGraphic componentInChildren = this.m_goBG.GetComponentInChildren<SkeletonGraphic>();
					if (componentInChildren != null)
					{
						componentInChildren.AnimationState.SetAnimation(0, aniName, bGameObjectBGLoop);
					}
				}
				if (this.m_sgGoBG != null)
				{
					if (fBGFadeInTime > 0f)
					{
						this.m_sgGoBG.color = colBGFadeInStart;
						this.m_bSGGoBGTweening = true;
						this.m_sgGoBG.DOColor(colBGFadeIn, fBGFadeInTime).SetEase(easeBGFadeIn).OnComplete(delegate
						{
							this.m_bSGGoBGTweening = false;
						});
					}
					else
					{
						this.m_sgGoBG.color = new Color(1f, 1f, 1f, 1f);
					}
				}
			}
			else
			{
				this.ClearGOBG();
				this.ClearGOBG_2();
				if (fBGFadeInTime > 0f)
				{
					this.m_imgBG.color = colBGFadeInStart;
					this.m_imgBG.DOColor(colBGFadeIn, fBGFadeInTime).SetEase(easeBGFadeIn).OnComplete(delegate
					{
						this.m_bImgBGTweening = false;
					});
					this.m_bImgBGTweening = true;
				}
				else
				{
					this.m_imgBG.color = new Color(1f, 1f, 1f, 1f);
				}
				if (this.m_fBGFadeOutTime > 0f)
				{
					this.m_imgBG_2.enabled = true;
					this.m_imgBG_2.color = this.m_imgBG.color;
					this.m_imgBG_2.sprite = this.m_imgBG.sprite;
					this.m_rtBG_2.anchorMin = this.m_rtBG.anchorMin;
					this.m_rtBG_2.anchorMax = this.m_rtBG.anchorMax;
					this.m_rtBG_2.anchoredPosition = this.m_rtBG.anchoredPosition;
					this.m_rtBG_2.sizeDelta = this.m_rtBG.sizeDelta;
					this.m_bImgBG_2_Tweening = true;
					this.m_imgBG_2.DOColor(this.m_colBGFadeOut, this.m_fBGFadeOutTime).SetEase(this.m_easeBGFadeOut).OnComplete(delegate
					{
						this.m_bImgBG_2_Tweening = false;
					});
				}
				else
				{
					this.m_imgBG_2.enabled = false;
				}
				this.m_fBGFadeOutTime = 0f;
				this.m_imgBG.enabled = true;
				string text = "AB_UI_NKM_UI_CUTSCEN_BG_" + bgFileName;
				NKCAssetResourceData nkcassetResourceData = NKCResourceUtility.GetAssetResource(text, text);
				if (nkcassetResourceData != null)
				{
					this.m_imgBG.sprite = nkcassetResourceData.GetAsset<Sprite>();
				}
				else
				{
					Debug.Log("Cutscen NormalBGType load, name : " + text);
					nkcassetResourceData = NKCAssetResourceManager.OpenResource<Sprite>(text, text, false, null);
					if (nkcassetResourceData != null)
					{
						if (nkcassetResourceData.m_RefCount <= 1)
						{
							Debug.LogWarning("Cutscen NormalBGType need to load in the past, name : " + bgFileName);
						}
						this.m_imgBG.sprite = nkcassetResourceData.GetAsset<Sprite>();
						this.m_lstNKCAssetResourceDataToClose.Add(nkcassetResourceData);
					}
				}
			}
			this.m_fBGFadeOutTime = fBGFadeOutTime;
			this.m_colBGFadeOut = colBGFadeOut;
			this.m_easeBGFadeOut = easeBGFadeOut;
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x00178C69 File Offset: 0x00176E69
		private bool IsCrashing()
		{
			return this.m_NKMTrackingVector2.IsTracking();
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00178C76 File Offset: 0x00176E76
		private bool IsAnimating()
		{
			return !this.m_bNoWaitBGAni && (this.m_NKMTrackingVector2_2.IsTracking() || this.m_NKMTrackingVector3.IsTracking());
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x00178C9C File Offset: 0x00176E9C
		public bool IsFinished()
		{
			return !this.m_bImgBGTweening && !this.m_bImgBG_2_Tweening && !this.m_bSGGoBGTweening && !this.m_bSGGoBG_2_Tweening && !this.IsCrashing() && !this.IsAnimating();
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x00178CDC File Offset: 0x00176EDC
		public void Finish()
		{
			this.StopCrash();
			if (this.m_imgBG != null && this.m_bImgBGTweening)
			{
				this.m_imgBG.color = this.m_colBGFadeIn;
				this.m_imgBG.DOKill(true);
			}
			if (this.m_imgBG_2 != null && this.m_bImgBG_2_Tweening)
			{
				this.m_imgBG_2.DOKill(true);
				this.m_imgBG_2.color = this.m_colBGFadeOut;
			}
			if (this.m_sgGoBG != null && this.m_bSGGoBGTweening)
			{
				this.m_sgGoBG.color = this.m_colBGFadeIn;
				this.m_sgGoBG.DOKill(true);
			}
			if (this.m_sgGoBG_2 != null && this.m_bSGGoBG_2_Tweening)
			{
				this.m_sgGoBG_2.DOKill(true);
			}
			if (!this.m_bNoWaitBGAni)
			{
				this.StopAni();
			}
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x00178DBC File Offset: 0x00176FBC
		private void StopCrash()
		{
			if (this.m_fCrashTime <= 0f)
			{
				return;
			}
			this.m_NKMTrackingVector2.StopTracking();
			this.m_rtBG.anchoredPosition = this.m_orgPos;
			if (this.m_rtGoBG != null)
			{
				this.m_rtGoBG.anchoredPosition = this.m_orgPos;
			}
			this.m_Crash = 0f;
			this.m_fElapsedTime = 0f;
			this.m_fCrashTime = 0f;
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x00178E33 File Offset: 0x00177033
		public void SetCrash(int crash, float fCrashTime)
		{
			if (crash <= 0 || fCrashTime <= 0f)
			{
				return;
			}
			this.StopAni();
			this.StopCrash();
			this.m_fCrashTime = fCrashTime;
			this.m_Crash = (float)crash;
			this.CheckAndStartCrash();
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x00178E64 File Offset: 0x00177064
		public void SetAni(bool bNoWaitBGAni, float fTime, bool bAniPos, Vector2 OffsetPos, TRACKING_DATA_TYPE tdtPos, bool bAniScale, Vector3 OffsetScale, TRACKING_DATA_TYPE tdtScale)
		{
			if (fTime == 0f)
			{
				return;
			}
			if (!bAniPos && !bAniScale)
			{
				return;
			}
			this.m_bNoWaitBGAni = bNoWaitBGAni;
			this.m_bAniPos = bAniPos;
			this.m_bAniScale = bAniScale;
			this.m_OffsetPos = OffsetPos;
			this.m_OffsetScale = OffsetScale;
			if (this.m_bAniPos)
			{
				if (fTime <= 0.01f)
				{
					if (this.m_rtGoBG != null)
					{
						this.m_rtGoBG.anchoredPosition = this.m_OffsetPos;
					}
					else
					{
						this.m_rtBG.anchoredPosition = this.m_OffsetPos;
					}
				}
				Vector3 v = default(Vector3);
				if (this.m_rtGoBG != null)
				{
					v = this.m_rtGoBG.anchoredPosition;
				}
				else
				{
					v = this.m_rtBG.anchoredPosition;
				}
				this.m_NKMTrackingVector2_2.SetNowValue(v);
				this.m_NKMTrackingVector2_2.SetTracking(this.m_OffsetPos, fTime, tdtPos);
			}
			if (this.m_bAniScale)
			{
				Vector3 nowValue = default(Vector3);
				if (this.m_rtGoBG != null)
				{
					nowValue = new Vector3(this.m_rtGoBG.localScale.x / this.m_OrgGOScale.x, this.m_rtGoBG.localScale.y / this.m_OrgGOScale.y, this.m_rtGoBG.localScale.z / this.m_OrgGOScale.z);
				}
				else
				{
					nowValue = this.m_rtBG.localScale;
				}
				this.m_NKMTrackingVector3.SetNowValue(nowValue);
				this.m_NKMTrackingVector3.SetTracking(OffsetScale, fTime, tdtScale);
			}
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x00178FFC File Offset: 0x001771FC
		private void StopAni()
		{
			this.m_NKMTrackingVector2_2.StopTracking();
			this.m_NKMTrackingVector3.StopTracking();
			if (this.m_bAniPos)
			{
				if (this.m_rtGoBG != null)
				{
					this.m_orgPos = (this.m_rtGoBG.anchoredPosition = this.m_OffsetPos);
				}
				else
				{
					this.m_orgPos = (this.m_rtBG.anchoredPosition = this.m_OffsetPos);
				}
			}
			this.m_bAniPos = false;
			if (this.m_bAniScale)
			{
				if (this.m_rtGoBG != null)
				{
					this.m_rtGoBG.localScale = new Vector3(this.m_OrgGOScale.x * this.m_OffsetScale.x, this.m_OrgGOScale.y * this.m_OffsetScale.y, this.m_OrgGOScale.z * this.m_OffsetScale.z);
				}
				else
				{
					this.m_rtBG.localScale = this.m_OffsetScale;
				}
			}
			this.m_bAniScale = false;
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x001790F7 File Offset: 0x001772F7
		public void CloseBG()
		{
			this.ClearGOBG();
			this.ClearGOBG_2();
			this.m_imgBG.enabled = false;
			this.m_imgBG_2.enabled = false;
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x00179120 File Offset: 0x00177320
		public void Close()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.ClearGOBG();
			this.ClearGOBG_2();
			for (int i = 0; i < this.m_lstNKCAssetResourceDataToClose.Count; i++)
			{
				NKCAssetResourceManager.CloseResource(this.m_lstNKCAssetResourceDataToClose[i]);
			}
			this.m_lstNKCAssetResourceDataToClose.Clear();
		}

		// Token: 0x04003DA9 RID: 15785
		public Image m_imgBG;

		// Token: 0x04003DAA RID: 15786
		private bool m_bImgBGTweening;

		// Token: 0x04003DAB RID: 15787
		public RectTransform m_rtBG;

		// Token: 0x04003DAC RID: 15788
		public Image m_imgBG_2;

		// Token: 0x04003DAD RID: 15789
		private bool m_bImgBG_2_Tweening;

		// Token: 0x04003DAE RID: 15790
		public RectTransform m_rtBG_2;

		// Token: 0x04003DAF RID: 15791
		private float m_fBGFadeOutTime;

		// Token: 0x04003DB0 RID: 15792
		private Ease m_easeBGFadeOut = Ease.Linear;

		// Token: 0x04003DB1 RID: 15793
		private Color m_colBGFadeIn = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04003DB2 RID: 15794
		private Color m_colBGFadeOut = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04003DB3 RID: 15795
		private GameObject m_goBG;

		// Token: 0x04003DB4 RID: 15796
		private RectTransform m_rtGoBG;

		// Token: 0x04003DB5 RID: 15797
		private SkeletonGraphic m_sgGoBG;

		// Token: 0x04003DB6 RID: 15798
		private bool m_bSGGoBGTweening;

		// Token: 0x04003DB7 RID: 15799
		private GameObject m_goBG_2;

		// Token: 0x04003DB8 RID: 15800
		private RectTransform m_rtGoBG_2;

		// Token: 0x04003DB9 RID: 15801
		private SkeletonGraphic m_sgGoBG_2;

		// Token: 0x04003DBA RID: 15802
		private bool m_bSGGoBG_2_Tweening;

		// Token: 0x04003DBB RID: 15803
		private Vector3 m_OrgGOScale = new Vector3(1f, 1f, 1f);

		// Token: 0x04003DBC RID: 15804
		private float m_fElapsedTime;

		// Token: 0x04003DBD RID: 15805
		private float m_fCrashTime;

		// Token: 0x04003DBE RID: 15806
		private float m_Crash;

		// Token: 0x04003DBF RID: 15807
		private Vector2 m_orgPosAtResetToPlay = new Vector2(0f, 0f);

		// Token: 0x04003DC0 RID: 15808
		private Vector3 m_orgScaleAtResetToPlay = new Vector3(1f, 1f, 1f);

		// Token: 0x04003DC1 RID: 15809
		private Vector2 m_orgPos = new Vector2(0f, 0f);

		// Token: 0x04003DC2 RID: 15810
		private bool m_bAniPos;

		// Token: 0x04003DC3 RID: 15811
		private bool m_bAniScale;

		// Token: 0x04003DC4 RID: 15812
		private Vector2 m_OffsetPos = new Vector2(0f, 0f);

		// Token: 0x04003DC5 RID: 15813
		private Vector3 m_OffsetScale = new Vector3(1f, 1f, 1f);

		// Token: 0x04003DC6 RID: 15814
		private NKMTrackingVector2 m_NKMTrackingVector2 = new NKMTrackingVector2();

		// Token: 0x04003DC7 RID: 15815
		private NKMTrackingVector2 m_NKMTrackingVector2_2 = new NKMTrackingVector2();

		// Token: 0x04003DC8 RID: 15816
		private NKMTrackingVector3 m_NKMTrackingVector3 = new NKMTrackingVector3();

		// Token: 0x04003DC9 RID: 15817
		private static NKCUICutScenBGMgr m_scNKCUICutScenBGMgr;

		// Token: 0x04003DCA RID: 15818
		private const float CRASH_DIST_DEFAULT_VALUE = 50f;

		// Token: 0x04003DCB RID: 15819
		private bool m_bNoWaitBGAni;

		// Token: 0x04003DCC RID: 15820
		private bool m_bPause;

		// Token: 0x04003DCD RID: 15821
		private List<NKCAssetResourceData> m_lstNKCAssetResourceDataToClose = new List<NKCAssetResourceData>();

		// Token: 0x04003DCE RID: 15822
		public const string CLOSE_BG_RESERVED = "CLOSE";
	}
}
