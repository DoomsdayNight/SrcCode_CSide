using System;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000630 RID: 1584
	public abstract class NKCASUIUnitIllust : NKMObjectPoolData
	{
		// Token: 0x0600311C RID: 12572 RVA: 0x000F3A2A File Offset: 0x000F1C2A
		public override void Close()
		{
			if (this.m_matTemp != null)
			{
				this.m_matTemp.Close();
			}
			base.Close();
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000F3A45 File Offset: 0x000F1C45
		public override void Unload()
		{
			this.UnloadEffectMaterial();
			base.Unload();
		}

		// Token: 0x0600311E RID: 12574
		public abstract Color GetColor();

		// Token: 0x0600311F RID: 12575
		public abstract void SetColor(Color color);

		// Token: 0x06003120 RID: 12576
		public abstract void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f);

		// Token: 0x06003121 RID: 12577
		public abstract void SetParent(Transform parent, bool worldPositionStays);

		// Token: 0x06003122 RID: 12578
		public abstract float GetAnimationTime(NKCASUIUnitIllust.eAnimation eAnim);

		// Token: 0x06003123 RID: 12579
		public abstract float GetAnimationTime(string animName);

		// Token: 0x06003124 RID: 12580
		public abstract NKCASUIUnitIllust.eAnimation GetCurrentAnimation(int trackIndex = 0);

		// Token: 0x06003125 RID: 12581
		public abstract string GetCurrentAnimationName(int trackIndex = 0);

		// Token: 0x06003126 RID: 12582
		public abstract float GetCurrentAnimationTime(int trackIndex = 0);

		// Token: 0x06003127 RID: 12583
		public abstract void SetAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, int trackIndex = 0, bool bForceRestart = true, float fStartTime = 0f, bool bReturnDefault = true);

		// Token: 0x06003128 RID: 12584
		public abstract void SetAnimation(string AnimationName, bool loop, int trackIndex = 0, bool bForceRestart = true, float fStartTime = 0f, bool bReturnDefault = true);

		// Token: 0x06003129 RID: 12585
		public abstract void ForceUpdateAnimation();

		// Token: 0x0600312A RID: 12586
		public abstract void InitializeAnimation();

		// Token: 0x0600312B RID: 12587
		public abstract RectTransform GetRectTransform();

		// Token: 0x0600312C RID: 12588
		public abstract void SetMaterial(Material mat);

		// Token: 0x0600312D RID: 12589
		public abstract void SetDefaultMaterial();

		// Token: 0x0600312E RID: 12590
		public abstract void SetVFX(bool bSet);

		// Token: 0x0600312F RID: 12591
		public abstract Transform GetTalkTransform(bool bLeft);

		// Token: 0x06003130 RID: 12592
		public abstract Vector3 GetBoneWorldPosition(string name);

		// Token: 0x06003131 RID: 12593
		public abstract void SetTimeScale(float value);

		// Token: 0x06003132 RID: 12594 RVA: 0x000F3A53 File Offset: 0x000F1C53
		protected void UnloadEffectMaterial()
		{
			if (this.m_matTemp != null)
			{
				this.m_matTemp.Close();
				this.m_matTemp.Unload();
				this.m_matTemp = null;
			}
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000F3A7C File Offset: 0x000F1C7C
		public void SetEffectMaterial(NKCUICharacterView.EffectType effect)
		{
			this.UnloadEffectMaterial();
			this.m_matTemp = this.MakeEffectMaterial(effect);
			if (this.m_matTemp != null)
			{
				this.SetMaterial(this.m_matTemp.m_Material);
				this.ProcessEffect(effect);
				return;
			}
			Debug.LogWarning("EffectMaterial Load Failed!");
			this.SetDefaultMaterial();
			this.ProcessEffect(NKCUICharacterView.EffectType.None);
		}

		// Token: 0x06003134 RID: 12596
		protected abstract NKCASMaterial MakeEffectMaterial(NKCUICharacterView.EffectType effect);

		// Token: 0x06003135 RID: 12597
		protected abstract void ProcessEffect(NKCUICharacterView.EffectType effect);

		// Token: 0x06003136 RID: 12598 RVA: 0x000F3AD4 File Offset: 0x000F1CD4
		public void SetAnchoredPosition(Vector2 pos)
		{
			RectTransform rectTransform = this.GetRectTransform();
			if (rectTransform != null)
			{
				rectTransform.anchoredPosition = pos;
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000F3AF8 File Offset: 0x000F1CF8
		public void SetDefaultAnimation(NKCASUIUnitIllust.eAnimation value, bool bPlay = true, bool bInitialize = false, float fStartTime = 0f)
		{
			this.m_eDefaultAnimation = value;
			if (bPlay)
			{
				if (bInitialize)
				{
					this.InitializeAnimation();
				}
				this.SetAnimation(value, true, 0, false, fStartTime, true);
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000F3B1A File Offset: 0x000F1D1A
		public void SetDefaultAnimation(NKMUnitTempletBase unitTempletBase, bool bPlay = true, bool bInitialize = false)
		{
			if (unitTempletBase == null)
			{
				return;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP && !unitTempletBase.m_bMonster)
			{
				this.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, bPlay, bInitialize, 0f);
				return;
			}
			this.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, bPlay, bInitialize, 0f);
		}

		// Token: 0x06003139 RID: 12601
		public abstract bool HasAnimation(NKCASUIUnitIllust.eAnimation value);

		// Token: 0x0600313A RID: 12602
		public abstract bool HasAnimation(string name);

		// Token: 0x0600313B RID: 12603
		public abstract void SetIllustBackgroundEnable(bool bValue);

		// Token: 0x0600313C RID: 12604
		public abstract void SetSkin(string skinName);

		// Token: 0x0600313D RID: 12605
		public abstract bool HasSkin(string skinName);

		// Token: 0x0600313E RID: 12606
		public abstract int GetSkinOptionCount();

		// Token: 0x0600313F RID: 12607
		public abstract void SetSkinOption(int index);

		// Token: 0x06003140 RID: 12608 RVA: 0x000F3B54 File Offset: 0x000F1D54
		public static string GetAnimationName(NKCASUIUnitIllust.eAnimation eAnim)
		{
			switch (eAnim)
			{
			case NKCASUIUnitIllust.eAnimation.UNIT_TOUCH:
				return "TOUCH";
			case NKCASUIUnitIllust.eAnimation.UNIT_IDLE:
				return "IDLE";
			case NKCASUIUnitIllust.eAnimation.UNIT_LAUGH:
				return "LAUGH";
			case NKCASUIUnitIllust.eAnimation.UNIT_HATE:
				return "HATE";
			case NKCASUIUnitIllust.eAnimation.UNIT_SERIOUS:
				return "SERIOUS";
			case NKCASUIUnitIllust.eAnimation.UNIT_SERIOUS2:
				return "SERIOUS2";
			case NKCASUIUnitIllust.eAnimation.UNIT_SURPRISE:
				return "SURPRISE";
			case NKCASUIUnitIllust.eAnimation.UNIT_PRIDE:
				return "PRIDE";
			case NKCASUIUnitIllust.eAnimation.UNIT_DESPAIR:
				return "DESPAIR";
			case NKCASUIUnitIllust.eAnimation.UNIT_CONFUSION:
				return "CONFUSION";
			case NKCASUIUnitIllust.eAnimation.UNIT_CONFUSION2:
				return "CONFUSION2";
			case NKCASUIUnitIllust.eAnimation.UNIT_CONFUSION3:
				return "CONFUSION3";
			case NKCASUIUnitIllust.eAnimation.UNIT_HURT:
				return "HURT";
			case NKCASUIUnitIllust.eAnimation.UNIT_TIRED:
				return "TIRED";
			case NKCASUIUnitIllust.eAnimation.UNIT_SKILL:
				return "SKILL";
			default:
				switch (eAnim)
				{
				case NKCASUIUnitIllust.eAnimation.SD_IDLE:
					return "ASTAND";
				case NKCASUIUnitIllust.eAnimation.SD_ATTACK:
					return "ATTACK";
				case NKCASUIUnitIllust.eAnimation.SD_WORKING:
					return "WORKING";
				case NKCASUIUnitIllust.eAnimation.SD_MINING:
					return "MINING";
				case NKCASUIUnitIllust.eAnimation.SD_WALK:
					return "WALK";
				case NKCASUIUnitIllust.eAnimation.SD_RUN:
					return "RUN";
				case NKCASUIUnitIllust.eAnimation.SD_TOUCH:
					return "TOUCH";
				case NKCASUIUnitIllust.eAnimation.SD_DRAG:
					return "DRAG";
				case NKCASUIUnitIllust.eAnimation.SD_WIN:
					return "WIN";
				case NKCASUIUnitIllust.eAnimation.SD_START:
					return "START";
				case NKCASUIUnitIllust.eAnimation.SD_DOWN:
					return "DOWN";
				default:
					switch (eAnim)
					{
					case NKCASUIUnitIllust.eAnimation.SHIP_IDLE:
						return "ASTAND";
					case NKCASUIUnitIllust.eAnimation.BASE_STONE:
						return "BASE_STONE";
					case NKCASUIUnitIllust.eAnimation.BASE_STONE_END:
						return "BASE_STONE_END";
					case NKCASUIUnitIllust.eAnimation.BASE_STONE_FAIL:
						return "BASE_STONE_FAIL";
					case NKCASUIUnitIllust.eAnimation.BASE_HURDLE:
						return "BASE_HURDLE";
					case NKCASUIUnitIllust.eAnimation.BASE_HURDLE_END:
						return "BASE_HURDLE_END";
					case NKCASUIUnitIllust.eAnimation.BASE_HURDLE_FAIL:
						return "BASE_HURDLE_FAIL";
					case NKCASUIUnitIllust.eAnimation.BASE_VH:
						return "BASE_VH";
					case NKCASUIUnitIllust.eAnimation.BASE_VH_END:
						return "BASE_VH_END";
					case NKCASUIUnitIllust.eAnimation.BASE_VH_FAIL:
						return "BASE_VH_FAIL";
					case NKCASUIUnitIllust.eAnimation.BASE_TROPHY:
						return "BASE_TROPHY";
					case NKCASUIUnitIllust.eAnimation.BASE_TROPHY_END:
						return "BASE_TROPHY_END";
					case NKCASUIUnitIllust.eAnimation.BASE_GOAL:
						return "BASE_GOAL";
					case NKCASUIUnitIllust.eAnimation.SKILL1:
						return "SKILL1";
					case NKCASUIUnitIllust.eAnimation.DAMAGE_DOWN:
						return "DAMAGE_DOWN";
					}
					return "";
				}
				break;
			}
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000F3D29 File Offset: 0x000F1F29
		public static bool IsEmotionAnimation(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return eAnim - NKCASUIUnitIllust.eAnimation.UNIT_IDLE <= 12;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000F3D35 File Offset: 0x000F1F35
		public void InvalidateWorldRect()
		{
			this.m_bRectCalculated = false;
		}

		// Token: 0x06003143 RID: 12611
		public abstract Rect GetWorldRect(bool bRecalculateBound = false);

		// Token: 0x04003066 RID: 12390
		protected NKCASUIUnitIllust.eAnimation m_eDefaultAnimation = NKCASUIUnitIllust.eAnimation.UNIT_IDLE;

		// Token: 0x04003067 RID: 12391
		protected NKCASUIUnitIllust.eAnimation m_eCurrentAnimation = NKCASUIUnitIllust.eAnimation.NONE;

		// Token: 0x04003068 RID: 12392
		protected NKCASMaterial m_matTemp;

		// Token: 0x04003069 RID: 12393
		protected bool m_bRectCalculated;

		// Token: 0x020012EA RID: 4842
		public enum eAnimation
		{
			// Token: 0x04009755 RID: 38741
			NONE = -1,
			// Token: 0x04009756 RID: 38742
			UNIT_ENUM_START,
			// Token: 0x04009757 RID: 38743
			UNIT_TOUCH,
			// Token: 0x04009758 RID: 38744
			UNIT_IDLE,
			// Token: 0x04009759 RID: 38745
			UNIT_LAUGH,
			// Token: 0x0400975A RID: 38746
			UNIT_HATE,
			// Token: 0x0400975B RID: 38747
			UNIT_SERIOUS,
			// Token: 0x0400975C RID: 38748
			UNIT_SERIOUS2,
			// Token: 0x0400975D RID: 38749
			UNIT_SURPRISE,
			// Token: 0x0400975E RID: 38750
			UNIT_PRIDE,
			// Token: 0x0400975F RID: 38751
			UNIT_DESPAIR,
			// Token: 0x04009760 RID: 38752
			UNIT_CONFUSION,
			// Token: 0x04009761 RID: 38753
			UNIT_CONFUSION2,
			// Token: 0x04009762 RID: 38754
			UNIT_CONFUSION3,
			// Token: 0x04009763 RID: 38755
			UNIT_HURT,
			// Token: 0x04009764 RID: 38756
			UNIT_TIRED,
			// Token: 0x04009765 RID: 38757
			UNIT_SKILL,
			// Token: 0x04009766 RID: 38758
			UNIT_ENUM_END,
			// Token: 0x04009767 RID: 38759
			SD_ENUM_START = 1000,
			// Token: 0x04009768 RID: 38760
			SD_IDLE,
			// Token: 0x04009769 RID: 38761
			SD_ATTACK,
			// Token: 0x0400976A RID: 38762
			SD_WORKING,
			// Token: 0x0400976B RID: 38763
			SD_MINING,
			// Token: 0x0400976C RID: 38764
			SD_WALK,
			// Token: 0x0400976D RID: 38765
			SD_RUN,
			// Token: 0x0400976E RID: 38766
			SD_TOUCH,
			// Token: 0x0400976F RID: 38767
			SD_DRAG,
			// Token: 0x04009770 RID: 38768
			SD_WIN,
			// Token: 0x04009771 RID: 38769
			SD_START,
			// Token: 0x04009772 RID: 38770
			SD_DOWN,
			// Token: 0x04009773 RID: 38771
			SD_ENUM_END,
			// Token: 0x04009774 RID: 38772
			SHIP_ENUM_START = 2000,
			// Token: 0x04009775 RID: 38773
			SHIP_IDLE,
			// Token: 0x04009776 RID: 38774
			SHIP_ENUM_END,
			// Token: 0x04009777 RID: 38775
			BASE_STONE,
			// Token: 0x04009778 RID: 38776
			BASE_STONE_END,
			// Token: 0x04009779 RID: 38777
			BASE_STONE_FAIL,
			// Token: 0x0400977A RID: 38778
			BASE_HURDLE,
			// Token: 0x0400977B RID: 38779
			BASE_HURDLE_END,
			// Token: 0x0400977C RID: 38780
			BASE_HURDLE_FAIL,
			// Token: 0x0400977D RID: 38781
			BASE_VH,
			// Token: 0x0400977E RID: 38782
			BASE_VH_END,
			// Token: 0x0400977F RID: 38783
			BASE_VH_FAIL,
			// Token: 0x04009780 RID: 38784
			BASE_TROPHY,
			// Token: 0x04009781 RID: 38785
			BASE_TROPHY_END,
			// Token: 0x04009782 RID: 38786
			BASE_GOAL,
			// Token: 0x04009783 RID: 38787
			SKILL1,
			// Token: 0x04009784 RID: 38788
			DAMAGE_DOWN,
			// Token: 0x04009785 RID: 38789
			INVALID
		}

		// Token: 0x020012EB RID: 4843
		public enum UnitIllustType
		{
			// Token: 0x04009787 RID: 38791
			Spine,
			// Token: 0x04009788 RID: 38792
			Cubism
		}
	}
}
