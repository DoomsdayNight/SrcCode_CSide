using System;
using System.Collections;
using ClientPacket.User;
using Cs.Math;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200074E RID: 1870
	public class NKCUICharacterView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler, IScrollHandler
	{
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x00165C96 File Offset: 0x00163E96
		// (set) Token: 0x06004AA2 RID: 19106 RVA: 0x00165C9E File Offset: 0x00163E9E
		public NKCUICharacterView.eMode CurrentMode { get; private set; }

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06004AA3 RID: 19107 RVA: 0x00165CA7 File Offset: 0x00163EA7
		private RectTransform m_rectRoot
		{
			get
			{
				if (this.m_NKCUICharacterViewEffectPinup == null)
				{
					this.m_NKCUICharacterViewEffectPinup = base.GetComponentInChildren<NKCUICharacterViewEffectPinup>();
				}
				if (!(this.m_NKCUICharacterViewEffectPinup == null))
				{
					return this.m_NKCUICharacterViewEffectPinup.GetRectTransform();
				}
				return this.m_rectIllustRoot;
			}
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00165CE3 File Offset: 0x00163EE3
		public NKMUnitData GetCurrentUnitData()
		{
			return this.m_NKMUnitData;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x00165CEC File Offset: 0x00163EEC
		public void Init(NKCUICharacterView.OnDragEvent onDragEvent = null, NKCUICharacterView.OnTouchEvent onTouchEvent = null)
		{
			if (this.m_srScrollRect != null)
			{
				this.m_srScrollRect.enabled = false;
			}
			this.m_vRootOrigPosition = this.m_rectIllustRoot.anchoredPosition;
			this.m_vRootOrigScale = this.m_rectIllustRoot.localScale;
			this.dOnDragEvent = onDragEvent;
			this.dOnTouchEvent = onTouchEvent;
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x00165D43 File Offset: 0x00163F43
		private void OnDestroy()
		{
			this.CleanUp();
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x00165D4B File Offset: 0x00163F4B
		public void CleanUp()
		{
			this.CleanupAllEffect();
			this.CloseCharacterIllust();
			this.CloseTalk();
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00165D60 File Offset: 0x00163F60
		public void CloseCharacterIllust()
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUIUnitIllust);
			this.m_NKMUnitData = null;
			this.m_NKCASUIUnitIllust = null;
			this.m_CurrentIllustUnitID = 0;
			this.m_CurrentIllustSkinID = 0;
			this.m_CurrentIllustUnitStrID = "";
			this.m_eCurrentUnitType = NKM_UNIT_TYPE.NUT_INVALID;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00165DB0 File Offset: 0x00163FB0
		public NKCASUIUnitIllust GetUnitIllust()
		{
			return this.m_NKCASUIUnitIllust;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00165DB8 File Offset: 0x00163FB8
		public bool HasCharacterIllust()
		{
			return this.m_NKCASUIUnitIllust != null;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00165DC3 File Offset: 0x00163FC3
		public bool IsDiffrentCharacter(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return this.IsDiffrentCharacter(0, 0);
			}
			return this.IsDiffrentCharacter(unitData.m_UnitID, unitData.m_SkinID);
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00165DE3 File Offset: 0x00163FE3
		private bool IsDiffrentCharacter(int characterID, int skinID)
		{
			return this.m_CurrentIllustUnitID != characterID || this.m_CurrentIllustSkinID != skinID;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00165DFC File Offset: 0x00163FFC
		public bool IsDiffrentCharacter(string characterStrID)
		{
			return this.m_CurrentIllustUnitStrID != characterStrID || this.m_CurrentIllustSkinID != 0;
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00165E17 File Offset: 0x00164017
		public Color GetColor()
		{
			if (this.m_NKCASUIUnitIllust != null)
			{
				return this.m_NKCASUIUnitIllust.GetColor();
			}
			return Color.black;
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00165E34 File Offset: 0x00164034
		public void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f, bool bIncludeEffect = false)
		{
			if (this.m_NKCASUIUnitIllust != null)
			{
				if (this.m_mbCurrentEffect != null)
				{
					Color effectColor = this.m_mbCurrentEffect.EffectColor;
					if (fR >= 0f)
					{
						fR *= effectColor.r;
					}
					if (fG >= 0f)
					{
						fG *= effectColor.g;
					}
					if (fB >= 0f)
					{
						fB *= effectColor.b;
					}
					if (fA >= 0f)
					{
						fA *= effectColor.a;
					}
					if (bIncludeEffect)
					{
						this.m_mbCurrentEffect.SetColor(fR, fG, fB, fA);
					}
				}
				this.m_NKCASUIUnitIllust.SetColor(fR, fG, fB, fA);
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00165ED4 File Offset: 0x001640D4
		public void SetColor(Color col, bool bIncludeEffect = false)
		{
			if (this.m_NKCASUIUnitIllust != null)
			{
				if (this.m_mbCurrentEffect != null)
				{
					this.m_NKCASUIUnitIllust.SetColor(this.m_mbCurrentEffect.EffectColor * col);
					if (bIncludeEffect)
					{
						this.m_mbCurrentEffect.SetColor(col);
						return;
					}
				}
				else
				{
					this.m_NKCASUIUnitIllust.SetColor(col);
				}
			}
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00165F30 File Offset: 0x00164130
		public void SetCharacterIllust(NKMBackgroundUnitInfo bgUnitInfo, bool bAsync = false, bool bVfX = true)
		{
			if (bgUnitInfo == null)
			{
				this.CloseCharacterIllust();
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKM_UNIT_TYPE unitType = bgUnitInfo.unitType;
			if (unitType - NKM_UNIT_TYPE.NUT_NORMAL > 1)
			{
				if (unitType != NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					this.CloseCharacterIllust();
					return;
				}
				NKMOperator operatorFromUId = nkmuserData.m_ArmyData.GetOperatorFromUId(bgUnitInfo.unitUid);
				this.SetCharacterIllust(operatorFromUId, bAsync, bgUnitInfo.backImage, bVfX, bgUnitInfo.skinOption);
			}
			else
			{
				NKMUnitData unitOrShipFromUID = nkmuserData.m_ArmyData.GetUnitOrShipFromUID(bgUnitInfo.unitUid);
				this.SetCharacterIllust(unitOrShipFromUID, bAsync, bgUnitInfo.backImage, bVfX, bgUnitInfo.skinOption);
			}
			this.SetOffset(new Vector2(bgUnitInfo.unitPosX, bgUnitInfo.unitPosY));
			if (bgUnitInfo.unitSize == 0f)
			{
				bgUnitInfo.unitSize = 1f;
			}
			this.SetScale(bgUnitInfo.unitSize);
			if (bgUnitInfo.unitFace == 0)
			{
				this.SetDefaultAnimation(bgUnitInfo.unitType);
				return;
			}
			NKMLobbyFaceTemplet nkmlobbyFaceTemplet = NKMTempletContainer<NKMLobbyFaceTemplet>.Find(bgUnitInfo.unitFace);
			if (nkmlobbyFaceTemplet != null)
			{
				this.SetDefaultAnimation(nkmlobbyFaceTemplet);
				return;
			}
			this.SetDefaultAnimation(bgUnitInfo.unitType);
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0016602F File Offset: 0x0016422F
		public void SetCharacterIllust(NKMUnitData unitData, bool bAsync = false, bool bEnableBackground = true, bool bVFX = true, int skinOption = 0)
		{
			if (unitData == null)
			{
				this.CloseCharacterIllust();
				return;
			}
			this.m_NKMUnitData = unitData;
			this._SetIllust(unitData.m_UnitID, unitData.m_SkinID, bEnableBackground, bVFX, bAsync, skinOption);
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0016605A File Offset: 0x0016425A
		public void SetCharacterIllust(NKMOperator operatorData, bool bAsync = false, bool bEnableBackground = true, bool bVFX = true, int skinOption = 0)
		{
			if (operatorData == null)
			{
				this.CloseCharacterIllust();
				return;
			}
			this.m_NKMUnitData = null;
			this._SetIllust(operatorData.id, 0, bEnableBackground, bVFX, bAsync, skinOption);
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00166080 File Offset: 0x00164280
		public void SetCharacterIllust(NKMSkinTemplet skinTemplet, bool bAsync = false, bool bEnableBackground = true, int skinOption = 0)
		{
			if (skinTemplet == null)
			{
				this.CloseCharacterIllust();
				return;
			}
			this._SetIllust(skinTemplet.m_SkinEquipUnitID, skinTemplet.m_SkinID, bEnableBackground, true, bAsync, skinOption);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001660A3 File Offset: 0x001642A3
		public void SetCharacterIllust(NKMUnitData unitData, int skinID, bool bAsync = false, int skinOption = 0)
		{
			if (unitData == null)
			{
				this.CloseCharacterIllust();
				return;
			}
			this.m_NKMUnitData = unitData;
			this._SetIllust(unitData.m_UnitID, skinID, true, true, bAsync, skinOption);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x001660C8 File Offset: 0x001642C8
		public void SetCharacterIllust(NKMUnitTempletBase unitTempletBase, int skinID = 0, bool bAsync = false, bool bEnableBackground = true, int skinOption = 0)
		{
			if (unitTempletBase == null)
			{
				this.CloseCharacterIllust();
				return;
			}
			this._SetIllust(unitTempletBase.m_UnitID, skinID, bEnableBackground, true, bAsync, skinOption);
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x001660E8 File Offset: 0x001642E8
		public void SetCharacterIllust(int unitID, int skinID = 0, bool bAsync = false, bool bEnableBackground = true, int skinOption = 0)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			this.SetCharacterIllust(unitTempletBase, skinID, bAsync, bEnableBackground, skinOption);
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0016610C File Offset: 0x0016430C
		private void _SetIllust(int characterID, int skinID, bool bEnableBackground, bool bVFX, bool bAsync, int skinOption)
		{
			if (this.m_CurrentIllustUnitID == characterID && this.m_CurrentIllustSkinID == skinID)
			{
				this.SetCharacterIllustBackgroundEnable(bEnableBackground);
				this.SetSkinOption(skinOption);
				return;
			}
			if (this.m_eCurrentEffect != NKCUICharacterView.EffectType.None)
			{
				this.CleanupAllEffect();
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUIUnitIllust);
			this.m_CurrentIllustUnitID = characterID;
			this.m_CurrentIllustSkinID = skinID;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(characterID);
			this.m_eCurrentUnitType = unitTempletBase.m_NKM_UNIT_TYPE;
			this.m_CurrentIllustUnitStrID = unitTempletBase.m_UnitStrID;
			this.m_NKCASUIUnitIllust = NKCResourceUtility.OpenSpineIllust(unitTempletBase, skinID, bAsync);
			if (!bAsync)
			{
				if (this.m_NKCASUIUnitIllust != null)
				{
					this.m_NKCASUIUnitIllust.SetParent(this.m_rectRoot, false);
					this.SetDefaultAnimation(unitTempletBase);
					this.m_NKCASUIUnitIllust.SetAnchoredPosition(Vector2.zero);
					this.SetVFX(bVFX);
					this.SetCharacterIllustBackgroundEnable(bEnableBackground);
					this.SetSkinOption(skinOption);
					return;
				}
			}
			else
			{
				base.StartCoroutine(this.ProcessAsyncLoad(bEnableBackground, bVFX, skinOption));
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x001661FC File Offset: 0x001643FC
		public void SetCharacterIllust(string unitStrID, bool bEnableBackground, bool bVFX, bool bAsync, int skinOption = 0)
		{
			if (unitStrID == this.m_CurrentIllustUnitStrID)
			{
				return;
			}
			if (this.m_eCurrentEffect != NKCUICharacterView.EffectType.None)
			{
				this.CleanupAllEffect();
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUIUnitIllust);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitStrID);
			if (unitTempletBase != null)
			{
				this.m_eCurrentUnitType = unitTempletBase.m_NKM_UNIT_TYPE;
				this.m_CurrentIllustUnitID = unitTempletBase.m_UnitID;
				this.m_CurrentIllustSkinID = 0;
				this.m_CurrentIllustUnitStrID = unitStrID;
				this.m_NKCASUIUnitIllust = NKCResourceUtility.OpenSpineIllust(unitTempletBase, false);
			}
			else
			{
				this.m_eCurrentUnitType = NKM_UNIT_TYPE.NUT_NORMAL;
				this.m_CurrentIllustUnitID = 0;
				this.m_CurrentIllustSkinID = 0;
				this.m_CurrentIllustUnitStrID = unitStrID;
				this.m_NKCASUIUnitIllust = NKCResourceUtility.OpenSpineIllustWithManualNaming(unitStrID, bAsync);
			}
			if (!bAsync)
			{
				if (this.m_NKCASUIUnitIllust != null)
				{
					this.m_NKCASUIUnitIllust.SetParent(this.m_rectRoot, false);
					this.m_NKCASUIUnitIllust.SetAnchoredPosition(Vector2.zero);
					this.SetDefaultAnimation(unitTempletBase);
					this.SetVFX(bVFX);
					this.SetCharacterIllustBackgroundEnable(bEnableBackground);
					this.SetSkinOption(skinOption);
					return;
				}
			}
			else
			{
				base.StartCoroutine(this.ProcessAsyncLoad(bEnableBackground, bVFX, skinOption));
			}
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00166301 File Offset: 0x00164501
		private void SetDefaultAnimation(NKMUnitTempletBase templetBase)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			if (templetBase != null)
			{
				this.m_NKCASUIUnitIllust.SetDefaultAnimation(templetBase, true, true);
				return;
			}
			this.SetDefaultAnimation(this.m_eCurrentUnitType);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x0016632C File Offset: 0x0016452C
		private void SetDefaultAnimation(NKM_UNIT_TYPE unitType)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			switch (unitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				this.m_NKCASUIUnitIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, true, false, 0f);
				return;
			case NKM_UNIT_TYPE.NUT_SHIP:
				this.m_NKCASUIUnitIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00166384 File Offset: 0x00164584
		public void SetDefaultAnimation(NKCASUIUnitIllust.eAnimation eAnimation)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			if (this.CheckEmotionToEmotion(eAnimation))
			{
				this.m_NKCASUIUnitIllust.SetDefaultAnimation(eAnimation, true, false, this.m_NKCASUIUnitIllust.GetCurrentAnimationTime(0));
				return;
			}
			this.m_NKCASUIUnitIllust.SetDefaultAnimation(eAnimation, true, false, 0f);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x001663D4 File Offset: 0x001645D4
		public void SetDefaultAnimation(NKMLobbyFaceTemplet faceTemplet)
		{
			if (faceTemplet == null)
			{
				return;
			}
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			NKCASUIUnitIllust.eAnimation eAnimation;
			if (!Enum.TryParse<NKCASUIUnitIllust.eAnimation>(faceTemplet.AnimationName, out eAnimation))
			{
				Debug.LogError("Animation " + faceTemplet.AnimationName + " not exist!");
				return;
			}
			if (this.CheckEmotionToEmotion(eAnimation))
			{
				this.m_NKCASUIUnitIllust.SetDefaultAnimation(eAnimation, true, false, this.m_NKCASUIUnitIllust.GetCurrentAnimationTime(0));
				return;
			}
			this.m_NKCASUIUnitIllust.SetDefaultAnimation(eAnimation, true, false, 0f);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0016644F File Offset: 0x0016464F
		private IEnumerator ProcessAsyncLoad(bool bEnableBackground, bool bVFX, int skinOption)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				yield break;
			}
			while (!this.m_NKCASUIUnitIllust.m_bIsLoaded)
			{
				yield return null;
			}
			this.m_NKCASUIUnitIllust.SetParent(this.m_rectRoot, false);
			this.SetDefaultAnimation(this.m_eCurrentUnitType);
			this.m_NKCASUIUnitIllust.SetAnchoredPosition(Vector2.zero);
			this.SetVFX(bVFX);
			this.SetCharacterIllustBackgroundEnable(bEnableBackground);
			this.SetSkinOption(skinOption);
			yield break;
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00166474 File Offset: 0x00164674
		public void SetAnimation(NKCASUIUnitIllust.eAnimation Animation, bool loop)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			if (loop && this.CheckEmotionToEmotion(Animation))
			{
				this.m_NKCASUIUnitIllust.SetAnimation(Animation, loop, 0, true, this.m_NKCASUIUnitIllust.GetCurrentAnimationTime(0), true);
				return;
			}
			this.m_NKCASUIUnitIllust.SetAnimation(Animation, loop, 0, true, 0f, true);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x001664C8 File Offset: 0x001646C8
		public void SetAnimationTrackTime(float fTrackTime)
		{
			NKCASUISpineIllust nkcasuispineIllust = (NKCASUISpineIllust)this.m_NKCASUIUnitIllust;
			nkcasuispineIllust.m_SpineIllustInstant_SkeletonGraphic.AnimationState.GetCurrent(0).TrackTime = fTrackTime;
			nkcasuispineIllust.ForceUpdateAnimation();
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x001664F4 File Offset: 0x001646F4
		private bool CheckEmotionToEmotion(NKCASUIUnitIllust.eAnimation nextAnimation)
		{
			if (NKCASUIUnitIllust.IsEmotionAnimation(nextAnimation))
			{
				NKCASUIUnitIllust.eAnimation currentAnimation = this.m_NKCASUIUnitIllust.GetCurrentAnimation(0);
				if (NKCASUIUnitIllust.IsEmotionAnimation(currentAnimation) && this.m_NKCASUIUnitIllust.GetAnimationTime(nextAnimation).IsNearlyEqual(this.m_NKCASUIUnitIllust.GetAnimationTime(currentAnimation), 1E-05f))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00166545 File Offset: 0x00164745
		public void SetDefaultTransform(float fDurationTime = 0.25f)
		{
			this.m_fAniTime = fDurationTime;
			this.m_vecDefaultPos = this.m_rectSpineIllustPanel.anchoredPosition;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00166560 File Offset: 0x00164760
		public void SetMode(NKCUICharacterView.eMode mode, bool bAnimate = true)
		{
			this.CurrentMode = mode;
			if (this.m_rectSpineIllustPanel != null)
			{
				this.m_rectSpineIllustPanel.DOComplete(false);
			}
			if (mode != NKCUICharacterView.eMode.Normal)
			{
				if (mode != NKCUICharacterView.eMode.CharacterView)
				{
					return;
				}
				if (this.m_rmIllustViewPanel != null)
				{
					this.m_rmIllustViewPanel.Move("ViewMode", bAnimate);
				}
				if (!NKCDefineManager.DEFINE_UNITY_STANDALONE())
				{
					if (bAnimate)
					{
						if (this.m_rectSpineIllustPanel != null)
						{
							this.m_rectSpineIllustPanel.DOLocalRotate(new Vector3(0f, 0f, 90f), 0.4f, RotateMode.Fast).SetEase(Ease.OutCubic);
							return;
						}
					}
					else if (this.m_rectSpineIllustPanel != null)
					{
						this.m_rectSpineIllustPanel.localRotation = Quaternion.Euler(0f, 0f, 90f);
					}
				}
			}
			else
			{
				if (this.m_rmIllustViewPanel != null)
				{
					this.m_rmIllustViewPanel.Move("Base", bAnimate);
				}
				if (this.m_srScrollRect != null)
				{
					this.m_srScrollRect.enabled = false;
				}
				if (bAnimate)
				{
					if (this.m_rectSpineIllustPanel != null)
					{
						this.m_rectSpineIllustPanel.DOLocalRotate(Vector3.zero, 0.25f, RotateMode.Fast).SetEase(Ease.OutCubic);
					}
				}
				else if (this.m_rectSpineIllustPanel != null)
				{
					this.m_rectSpineIllustPanel.localRotation = Quaternion.identity;
				}
				if (this.m_fAniTime > 0f && this.m_rectSpineIllustPanel != null)
				{
					this.m_rectSpineIllustPanel.DOAnchorPos3D(this.m_vecDefaultPos, this.m_fAniTime, false);
					this.m_rectSpineIllustPanel.DOScale(1f, this.m_fAniTime);
					return;
				}
			}
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0016670B File Offset: 0x0016490B
		public void SetVFX(bool bSet)
		{
			if (this.m_NKCASUIUnitIllust != null)
			{
				this.m_NKCASUIUnitIllust.SetVFX(bSet);
			}
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00166724 File Offset: 0x00164924
		protected static void ZoomRectTransform(ref RectTransform targetRect, Vector3 targetScale, Vector2 ZoomCenterScreenPos)
		{
			if (targetRect == null)
			{
				return;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRect, ZoomCenterScreenPos, null, out vector);
			Vector2 b = new Vector2(vector.x - targetRect.rect.width * (0.5f - targetRect.pivot.x), vector.y - targetRect.rect.height * (0.5f - targetRect.pivot.y));
			Vector2 anchoredPosition = targetRect.anchoredPosition;
			targetRect.localScale = targetScale;
			Vector2 vector2;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRect, ZoomCenterScreenPos, null, out vector2);
			Vector2 a = new Vector2(vector2.x - targetRect.rect.width * (0.5f - targetRect.pivot.x), vector2.y - targetRect.rect.height * (0.5f - targetRect.pivot.y)) - b;
			targetRect.anchoredPosition += a * targetRect.localScale.x;
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00166848 File Offset: 0x00164A48
		public void OnPointerDown(PointerEventData eventData)
		{
			NKCUICharacterView.eMode currentMode = this.CurrentMode;
			if (currentMode != NKCUICharacterView.eMode.Normal)
			{
				if (currentMode == NKCUICharacterView.eMode.CharacterView)
				{
					if (Input.touchCount == 1)
					{
						this.bTouchEventPossible = true;
						this.bMovePossible = true;
						if (this.m_srScrollRect != null)
						{
							this.m_srScrollRect.enabled = (this.CurrentMode == NKCUICharacterView.eMode.CharacterView);
						}
					}
					else
					{
						this.bTouchEventPossible = false;
						this.bMovePossible = false;
						if (this.m_srScrollRect != null)
						{
							this.m_srScrollRect.enabled = false;
						}
					}
					if ((NKCDefineManager.DEFINE_UNITY_EDITOR() || NKCDefineManager.DEFINE_UNITY_STANDALONE()) && Input.touchCount == 0)
					{
						this.bTouchEventPossible = true;
						this.bMovePossible = true;
						if (this.m_srScrollRect != null)
						{
							this.m_srScrollRect.enabled = (this.CurrentMode == NKCUICharacterView.eMode.CharacterView);
							return;
						}
					}
				}
			}
			else
			{
				this.bTouchEventPossible = true;
				this.bMovePossible = false;
				if (this.m_srScrollRect != null)
				{
					this.m_srScrollRect.enabled = false;
				}
			}
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00166939 File Offset: 0x00164B39
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.bTouchEventPossible)
			{
				if (this.dOnTouchEvent == null)
				{
					this.TouchIllust();
					return;
				}
				NKCUICharacterView.OnTouchEvent onTouchEvent = this.dOnTouchEvent;
				if (onTouchEvent == null)
				{
					return;
				}
				onTouchEvent(eventData, this);
			}
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00166964 File Offset: 0x00164B64
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.bTouchEventPossible = false;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00166970 File Offset: 0x00164B70
		public void OnDrag(PointerEventData eventData)
		{
			if (this.CurrentMode == NKCUICharacterView.eMode.CharacterView)
			{
				if (NKCScenManager.GetScenManager().GetHasPinch())
				{
					this.OnPinchZoom(NKCScenManager.GetScenManager().GetPinchCenter(), NKCScenManager.GetScenManager().GetPinchDeltaMagnitude());
				}
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					this.m_srScrollRect.enabled = false;
					this.OnPinchZoom(eventData.position, (eventData.delta.x + eventData.delta.y) / (float)Screen.width);
				}
				else if (this.bMovePossible)
				{
					this.m_srScrollRect.enabled = true;
					if (this.m_srScrollRect != null)
					{
						this.m_srScrollRect.OnDrag(eventData);
					}
				}
			}
			NKCUICharacterView.OnDragEvent onDragEvent = this.dOnDragEvent;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(eventData);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00166A3E File Offset: 0x00164C3E
		public void OnScroll(PointerEventData eventData)
		{
			if (this.CurrentMode == NKCUICharacterView.eMode.CharacterView)
			{
				this.OnPinchZoom(eventData.position, eventData.scrollDelta.y * this.scrollSensibility);
			}
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00166A68 File Offset: 0x00164C68
		public void TouchIllust()
		{
			NKM_UNIT_TYPE eCurrentUnitType = this.m_eCurrentUnitType;
			if (eCurrentUnitType == NKM_UNIT_TYPE.NUT_NORMAL || eCurrentUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_TOUCH, false);
				this.PlayTouchVoice();
			}
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00166A92 File Offset: 0x00164C92
		public void SetScaleLimit(float min, float max)
		{
			this.MIN_ZOOM_SCALE = min;
			this.MAX_ZOOM_SCALE = max;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00166AA4 File Offset: 0x00164CA4
		public void OnPinchZoom(Vector2 PinchCenter, float pinchMagnitude)
		{
			if (this.m_rectSpineIllustPanel == null)
			{
				return;
			}
			float num = this.m_rectSpineIllustPanel.localScale.x * Mathf.Pow(4f, pinchMagnitude);
			num = Mathf.Clamp(num, this.MIN_ZOOM_SCALE, this.MAX_ZOOM_SCALE);
			Vector3 localScale = new Vector3(num, num, 1f);
			this.m_rectSpineIllustPanel.localScale = localScale;
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x00166B0A File Offset: 0x00164D0A
		private void PlayTouchVoice()
		{
			if (this.m_NKMUnitData != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_TOUCH, this.m_NKMUnitData, false, true);
				return;
			}
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_TOUCH, this.m_CurrentIllustUnitID, this.m_CurrentIllustSkinID, false, true);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x00166B39 File Offset: 0x00164D39
		private void OpenTalk(NKMAssetName voiceAssetName, bool bLeft, NKC_UI_TALK_BOX_DIR dir)
		{
			if (voiceAssetName == null)
			{
				return;
			}
			if (NKCStringTable.GetNationalCode() == NKM_NATIONAL_CODE.NNC_KOREA)
			{
				return;
			}
			this.OpenTalk(bLeft, dir, voiceAssetName.m_BundleName + "@" + voiceAssetName.m_AssetName, 1f);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00166B6C File Offset: 0x00164D6C
		public void OpenTalk(bool bLeft, NKC_UI_TALK_BOX_DIR dir, string talk, float fadeTime = 0f)
		{
			if (this.m_talkBox == null)
			{
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_talk_box", "AB_UI_TALK_BOX", false, null);
				this.m_talkBox = nkcassetInstanceData.m_Instant.GetComponent<NKCComUITalkBox>();
				if (this.m_talkBox == null)
				{
					NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
					return;
				}
				this.m_talkInstance = nkcassetInstanceData;
			}
			Transform talkTransform = this.m_NKCASUIUnitIllust.GetTalkTransform(bLeft);
			this.m_talkBox.transform.SetParent(talkTransform);
			this.m_talkBox.transform.localPosition = Vector3.zero;
			NKCUtil.SetGameobjectActive(this.m_talkBox, true);
			this.m_talkBox.SetDir(dir);
			this.m_talkBox.SetText(talk, fadeTime, 0f);
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00166C24 File Offset: 0x00164E24
		private void CloseTalk()
		{
			if (this.m_talkInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_talkInstance);
				this.m_talkInstance = null;
				this.m_talkBox = null;
			}
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00166C47 File Offset: 0x00164E47
		public void SetCharacterIllustBackgroundEnable(bool bValue)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			this.m_NKCASUIUnitIllust.SetIllustBackgroundEnable(bValue);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00166C5E File Offset: 0x00164E5E
		public void SetSkinOption(int index)
		{
			if (this.m_NKCASUIUnitIllust == null)
			{
				return;
			}
			this.m_NKCASUIUnitIllust.SetSkinOption(index);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00166C75 File Offset: 0x00164E75
		public void CloseImmediatelyIllust()
		{
			NKCASUIUnitIllust nkcasuiunitIllust = this.m_NKCASUIUnitIllust;
			if (nkcasuiunitIllust != null)
			{
				nkcasuiunitIllust.Unload();
			}
			this.m_NKMUnitData = null;
			this.m_NKCASUIUnitIllust = null;
			this.m_CurrentIllustUnitID = 0;
			this.m_CurrentIllustSkinID = 0;
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00166CA4 File Offset: 0x00164EA4
		public NKCUICharacterView.EffectType GetCurrEffectType()
		{
			return this.m_eCurrentEffect;
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00166CAC File Offset: 0x00164EAC
		public void CleanupAllEffect()
		{
			if (this.m_mbCurrentEffect != null)
			{
				this.m_mbCurrentEffect.CleanUp(this.m_NKCASUIUnitIllust, this.m_rectRoot);
				UnityEngine.Object.Destroy(this.m_mbCurrentEffect);
			}
			this.m_eCurrentEffect = NKCUICharacterView.EffectType.None;
			this.m_mbCurrentEffect = null;
			this.CleanUpPinupEffect();
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00166D00 File Offset: 0x00164F00
		public void SetPinup(bool bPinup, float bEasingTime)
		{
			if (this.m_NKCUICharacterViewEffectPinup != null)
			{
				if (!bPinup && bEasingTime == 0f)
				{
					this.m_NKCUICharacterViewEffectPinup.SetDeActive();
					return;
				}
				if (bPinup)
				{
					this.m_NKCUICharacterViewEffectPinup.StartPinupEffect(bEasingTime);
					return;
				}
				this.m_NKCUICharacterViewEffectPinup.ClosePinupEffect(bEasingTime);
			}
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00166D4E File Offset: 0x00164F4E
		public void CleanUpPinupEffect()
		{
			if (this.m_NKCUICharacterViewEffectPinup != null)
			{
				this.m_NKCUICharacterViewEffectPinup.CleanUp(this.m_NKCASUIUnitIllust, this.m_rectRoot);
			}
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00166D78 File Offset: 0x00164F78
		public void PlayEffect(NKCUICharacterView.EffectType type)
		{
			if (this.m_eCurrentEffect == type)
			{
				return;
			}
			bool flag = this.MakeEffectInstance(type);
			switch (type)
			{
			case NKCUICharacterView.EffectType.None:
				return;
			case NKCUICharacterView.EffectType.Hologram:
			{
				if (flag)
				{
					return;
				}
				NKCUICharacterViewEffectHologram nkcuicharacterViewEffectHologram = this.m_mbCurrentEffect as NKCUICharacterViewEffectHologram;
				if (nkcuicharacterViewEffectHologram != null)
				{
					nkcuicharacterViewEffectHologram.HologramIn();
					return;
				}
				return;
			}
			case NKCUICharacterView.EffectType.HologramClose:
			{
				NKCUICharacterViewEffectHologram nkcuicharacterViewEffectHologram2 = this.m_mbCurrentEffect as NKCUICharacterViewEffectHologram;
				if (nkcuicharacterViewEffectHologram2 != null)
				{
					nkcuicharacterViewEffectHologram2.HologramOut();
					return;
				}
				return;
			}
			}
			if (this.m_NKCASUIUnitIllust != null)
			{
				this.m_NKCASUIUnitIllust.SetEffectMaterial(type);
			}
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00166E0C File Offset: 0x0016500C
		private bool MakeEffectInstance(NKCUICharacterView.EffectType type)
		{
			bool flag;
			if (type == NKCUICharacterView.EffectType.None || type - NKCUICharacterView.EffectType.Hologram > 1)
			{
				this.CleanupAllEffect();
				flag = false;
			}
			else if (this.m_mbCurrentEffect is NKCUICharacterViewEffectHologram)
			{
				flag = false;
			}
			else
			{
				this.CleanupAllEffect();
				this.m_mbCurrentEffect = base.gameObject.AddComponent<NKCUICharacterViewEffectHologram>();
				flag = true;
			}
			this.m_eCurrentEffect = type;
			if (flag)
			{
				this.m_mbCurrentEffect.SetEffect(this.m_NKCASUIUnitIllust, this.m_rectRoot);
			}
			return flag;
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00166E78 File Offset: 0x00165078
		public void SetOffset(Vector2 anchoredPos)
		{
			this.m_vCharOffset = anchoredPos;
			this.m_rectIllustRoot.anchoredPosition = this.m_vRootOrigPosition + this.m_vCharOffset;
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x00166EA0 File Offset: 0x001650A0
		public Vector3 OffsetToWorldPos(Vector2 offset)
		{
			Vector2 vector = this.m_vRootOrigPosition + offset;
			return this.m_rectIllustRoot.parent.TransformPoint(vector.x, vector.y, this.m_rectIllustRoot.transform.localPosition.z);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00166EEC File Offset: 0x001650EC
		public Vector2 WorldPosToOffset(Vector2 worldPos)
		{
			Vector3 vector = this.m_rectIllustRoot.parent.InverseTransformPoint(worldPos);
			return new Vector2(vector.x, vector.y) - this.m_vRootOrigPosition;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00166F2C File Offset: 0x0016512C
		public void SetScale(float scale)
		{
			this.m_fCharScale = scale;
			this.m_rectIllustRoot.localScale = this.m_vRootOrigScale * this.m_fCharScale;
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00166F51 File Offset: 0x00165151
		public Vector2 GetOffset()
		{
			return this.m_vCharOffset;
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x00166F59 File Offset: 0x00165159
		public float GetScale()
		{
			return this.m_fCharScale;
		}

		// Token: 0x04003974 RID: 14708
		[Header("필수")]
		public RectTransform m_rectIllustRoot;

		// Token: 0x04003975 RID: 14709
		[Header("유닛 뷰/확대/축소 사용시에만 필요")]
		public ScrollRect m_srScrollRect;

		// Token: 0x04003976 RID: 14710
		public RectTransform m_rectSpineIllustPanel;

		// Token: 0x04003977 RID: 14711
		public NKCUIRectMove m_rmIllustViewPanel;

		// Token: 0x04003978 RID: 14712
		private int m_CurrentIllustUnitID;

		// Token: 0x04003979 RID: 14713
		private int m_CurrentIllustSkinID;

		// Token: 0x0400397A RID: 14714
		private string m_CurrentIllustUnitStrID;

		// Token: 0x0400397B RID: 14715
		private NKM_UNIT_TYPE m_eCurrentUnitType;

		// Token: 0x0400397C RID: 14716
		private NKCASUIUnitIllust m_NKCASUIUnitIllust;

		// Token: 0x0400397D RID: 14717
		private NKCComUITalkBox m_talkBox;

		// Token: 0x0400397E RID: 14718
		private NKCAssetInstanceData m_talkInstance;

		// Token: 0x0400397F RID: 14719
		private NKMUnitData m_NKMUnitData;

		// Token: 0x04003980 RID: 14720
		private const string BASE_MOVE_SET = "Base";

		// Token: 0x04003981 RID: 14721
		private const string VIEWMODE_MOVE_SET = "ViewMode";

		// Token: 0x04003982 RID: 14722
		private NKCUICharacterView.OnDragEvent dOnDragEvent;

		// Token: 0x04003983 RID: 14723
		private NKCUICharacterView.OnTouchEvent dOnTouchEvent;

		// Token: 0x04003984 RID: 14724
		private Vector2 m_vecDefaultPos = Vector2.zero;

		// Token: 0x04003985 RID: 14725
		private float m_fAniTime;

		// Token: 0x04003986 RID: 14726
		private bool bTouchEventPossible;

		// Token: 0x04003987 RID: 14727
		private bool bMovePossible;

		// Token: 0x04003988 RID: 14728
		public float scrollSensibility = 0.1f;

		// Token: 0x04003989 RID: 14729
		public const float DEFAULT_MIN_ZOOM_SCALE = 0.1f;

		// Token: 0x0400398A RID: 14730
		public const float DEFAULT_MAX_ZOOM_SCALE = 3f;

		// Token: 0x0400398B RID: 14731
		private float MIN_ZOOM_SCALE = 0.1f;

		// Token: 0x0400398C RID: 14732
		private float MAX_ZOOM_SCALE = 3f;

		// Token: 0x0400398D RID: 14733
		private NKCUICharacterView.EffectType m_eCurrentEffect;

		// Token: 0x0400398E RID: 14734
		private NKCUICharacterViewEffectBase m_mbCurrentEffect;

		// Token: 0x0400398F RID: 14735
		private NKCUICharacterViewEffectPinup m_NKCUICharacterViewEffectPinup;

		// Token: 0x04003990 RID: 14736
		private Vector2 m_vRootOrigPosition;

		// Token: 0x04003991 RID: 14737
		private Vector3 m_vRootOrigScale;

		// Token: 0x04003992 RID: 14738
		private Vector2 m_vCharOffset = Vector2.zero;

		// Token: 0x04003993 RID: 14739
		private float m_fCharScale = 1f;

		// Token: 0x02001423 RID: 5155
		public enum eMode
		{
			// Token: 0x04009D8B RID: 40331
			Normal,
			// Token: 0x04009D8C RID: 40332
			CharacterView
		}

		// Token: 0x02001424 RID: 5156
		// (Invoke) Token: 0x0600A7F3 RID: 42995
		public delegate void OnDragEvent(PointerEventData evt);

		// Token: 0x02001425 RID: 5157
		// (Invoke) Token: 0x0600A7F7 RID: 42999
		public delegate void OnTouchEvent(PointerEventData evt, NKCUICharacterView charView);

		// Token: 0x02001426 RID: 5158
		public enum EffectType
		{
			// Token: 0x04009D8E RID: 40334
			None,
			// Token: 0x04009D8F RID: 40335
			Hologram,
			// Token: 0x04009D90 RID: 40336
			HologramClose,
			// Token: 0x04009D91 RID: 40337
			Gray,
			// Token: 0x04009D92 RID: 40338
			VersusMaskL,
			// Token: 0x04009D93 RID: 40339
			VersusMaskR,
			// Token: 0x04009D94 RID: 40340
			TwopassTransparency
		}
	}
}
