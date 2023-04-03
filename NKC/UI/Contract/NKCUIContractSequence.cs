using System;
using System.Collections;
using System.Collections.Generic;
using Cs.Math;
using NKM.Templet;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Contract
{
	// Token: 0x02000BEE RID: 3054
	public class NKCUIContractSequence : NKCUIBase, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
	{
		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06008DA4 RID: 36260 RVA: 0x0030296A File Offset: 0x00300B6A
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06008DA5 RID: 36261 RVA: 0x0030296D File Offset: 0x00300B6D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06008DA6 RID: 36262 RVA: 0x00302970 File Offset: 0x00300B70
		public override string MenuName
		{
			get
			{
				return "채용 연출";
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06008DA7 RID: 36263 RVA: 0x00302978 File Offset: 0x00300B78
		public static NKCUIContractSequence Instance
		{
			get
			{
				if (NKCUIContractSequence.m_Instance == null)
				{
					NKCUIContractSequence.m_Instance = NKCUIManager.OpenNewInstance<NKCUIContractSequence>("AB_UI_NKM_UI_CONTRACT_V2", "NKM_UI_CONTRACT_V2_SEQUENCE", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIContractSequence.CleanupInstance)).GetInstance<NKCUIContractSequence>();
					NKCUIContractSequence.m_Instance.Init();
				}
				return NKCUIContractSequence.m_Instance;
			}
		}

		// Token: 0x06008DA8 RID: 36264 RVA: 0x003029C7 File Offset: 0x00300BC7
		private static void CleanupInstance()
		{
			NKCUIContractSequence.m_Instance = null;
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06008DA9 RID: 36265 RVA: 0x003029CF File Offset: 0x00300BCF
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIContractSequence.m_Instance != null && NKCUIContractSequence.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008DAA RID: 36266 RVA: 0x003029EA File Offset: 0x00300BEA
		private void OnDestroy()
		{
			NKCUIContractSequence.m_Instance = null;
		}

		// Token: 0x06008DAB RID: 36267 RVA: 0x003029F2 File Offset: 0x00300BF2
		public override void CloseInternal()
		{
			this.StopCurPlaySound();
			UnityEngine.Object.Destroy(NKCUIContractSequence.m_Instance.gameObject);
			NKCUIContractSequence.m_Instance = null;
		}

		// Token: 0x06008DAC RID: 36268 RVA: 0x00302A10 File Offset: 0x00300C10
		public override void OnBackButton()
		{
			if (!this.m_bSkip)
			{
				this.OnSkip();
				return;
			}
			this.StopCurPlaySound();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_NKM_CONTRACT_HELI_FX1.ClearState();
			this.m_NKM_CONTRACT_HELI_FX2.ClearState();
			this.m_NKM_CONTRACT_HELI_FX3.ClearState();
			this.m_NKM_CONTRACT_HELI_FX4.ClearState();
			this.m_NKM_CONTRACT_HELI_FX5.ClearState();
			this.m_NKM_CONTRACT_HELI_FX6.ClearState();
			this.m_NKM_CONTRACT_HELI_FX7.ClearState();
			this.TrackEntry1 = null;
			this.TrackEntry2 = null;
			this.TrackEntry3 = null;
			this.TrackEntry5 = null;
			this.TrackEntry7 = null;
			this.m_iTargetGrade = 0;
			base.OnBackButton();
			if (this.dClose != null)
			{
				this.dClose();
			}
		}

		// Token: 0x06008DAD RID: 36269 RVA: 0x00302ACE File Offset: 0x00300CCE
		private void Init()
		{
			if (NKCCamera.GetCamera().aspect < 1.777f)
			{
				NKCCamera.RescaleRectToCameraFrustrum(this.m_rtBackground, NKCCamera.GetCamera(), Vector2.zero, 1200f, NKCCamera.FitMode.FitToScreen, NKCCamera.ScaleMode.Scale);
			}
		}

		// Token: 0x06008DAE RID: 36270 RVA: 0x00302B00 File Offset: 0x00300D00
		public void Open(NKM_UNIT_GRADE targetGrade, bool bAwaken, UnityAction close = null)
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.dClose = close;
			this.m_eSelectGrade = targetGrade;
			this.m_bAwaken = bAwaken;
			NKCUtil.SetGameobjectActive(this.m_FX_SOUND_FX_UI_RECRUITMNET_INTRO_01, false);
			NKCUtil.SetGameobjectActive(this.m_FX_SOUND_FX_UI_RECRUITMNET_INTRO_02, false);
			this.UpdateState(NKCUIContractSequence.eSequence.START);
			base.UIOpened(true);
			this.m_iVibrateCnt = 0;
		}

		// Token: 0x06008DAF RID: 36271 RVA: 0x00302B64 File Offset: 0x00300D64
		private void SetSSRHeliAni()
		{
			int num = RandomGenerator.Range(1, 100);
			if (1 == num)
			{
				this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.HANA);
				return;
			}
			if (5 >= num)
			{
				this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.HANA_SUYEON);
				return;
			}
			this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.GAB_HANA_SUYEON);
		}

		// Token: 0x06008DB0 RID: 36272 RVA: 0x00302B9C File Offset: 0x00300D9C
		private void SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER _type)
		{
			switch (_type)
			{
			case NKCUIContractSequence.RECEPTION_MEMBER.HANA_SUYEON:
				this.m_strFirstHeliAniName = "BASE3";
				this.m_strSecondHeliAniName = "BASE4";
				return;
			case NKCUIContractSequence.RECEPTION_MEMBER.GAB_HANA_SUYEON:
				this.m_strFirstHeliAniName = "BASE";
				this.m_strSecondHeliAniName = "BASE2";
				return;
			}
			this.m_strFirstHeliAniName = "BASE5";
			this.m_strSecondHeliAniName = "BASE6";
		}

		// Token: 0x06008DB1 RID: 36273 RVA: 0x00302C04 File Offset: 0x00300E04
		private void UpdateState(NKCUIContractSequence.eSequence type)
		{
			if (this.m_CurStatus == type)
			{
				return;
			}
			this.m_CurStatus = type;
			switch (this.m_CurStatus)
			{
			case NKCUIContractSequence.eSequence.START:
				this.m_lstCurPlaySoundID.Clear();
				NKCUtil.SetGameobjectActive(this.m_VFX, false);
				NKCUtil.SetGameobjectActive(this.EnhanceSR, false);
				NKCUtil.SetGameobjectActive(this.EnhanceSSR, false);
				NKCUtil.SetGameobjectActive(this.EnhanceAWAKEN, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_ARRIVAL, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_START, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_HELI, false);
				if (this.m_bAwaken)
				{
					this.SetSSRHeliAni();
					this.m_iTargetGrade = 3;
					this.m_NKM_CONTRACT_HELI_Animator.SetInteger("TargetGrade", 3);
					this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideAwaken", true);
				}
				else
				{
					switch (this.m_eSelectGrade)
					{
					case NKM_UNIT_GRADE.NUG_N:
						this.m_iTargetGrade = 0;
						this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.HANA);
						break;
					case NKM_UNIT_GRADE.NUG_R:
						this.m_iTargetGrade = 0;
						this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.HANA_SUYEON);
						break;
					case NKM_UNIT_GRADE.NUG_SR:
						this.m_iTargetGrade = 1;
						if (5 >= RandomGenerator.Range(1, 100))
						{
							this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.HANA);
						}
						else
						{
							this.SetHeliAni(NKCUIContractSequence.RECEPTION_MEMBER.HANA_SUYEON);
						}
						break;
					case NKM_UNIT_GRADE.NUG_SSR:
						this.m_iTargetGrade = 2;
						this.SetSSRHeliAni();
						break;
					}
					this.m_NKM_CONTRACT_HELI_Animator.SetInteger("TargetGrade", (int)this.m_eSelectGrade);
				}
				this.InitializeContractRandomPhase(this.m_iTargetGrade);
				this.m_NKM_CONTRACT_HELI_Animator.enabled = false;
				return;
			case NKCUIContractSequence.eSequence.WAIT:
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_ARRIVAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_START, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_HELI, false);
				this.m_lstCurPlaySoundID.Add(NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_START", 1f, 0f, 0f, false, 0f, false, 0f));
				this.m_NKM_CONTRACT_START.ClearState();
				float fDelay = this.m_NKM_CONTRACT_START.AnimationState.SetAnimation(0, "BASE", false).AnimationEnd * this.m_fFadeOutPercent;
				base.StartCoroutine(this.OnFadeOut(fDelay));
				this.m_CurStatus = NKCUIContractSequence.eSequence.SCENE_1;
				return;
			}
			case NKCUIContractSequence.eSequence.SCENE_1:
			case NKCUIContractSequence.eSequence.DRAG:
			case NKCUIContractSequence.eSequence.AUTO_CLOSE:
			case NKCUIContractSequence.eSequence.AUTO_OPEN:
				break;
			case NKCUIContractSequence.eSequence.SCENE_2:
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_HELI, true);
				this.m_bPlaySoundLand2 = false;
				this.m_bPlaySoundLoop2 = false;
				this.m_NKM_CONTRACT_HELI_FX1.ClearState();
				this.TrackEntry1 = this.m_NKM_CONTRACT_HELI_FX1.AnimationState.SetAnimation(0, "BASE", false);
				this.m_NKM_CONTRACT_HELI_FX1.AnimationState.Complete += this.SetNextAni;
				NKCUtil.SetGameobjectActive(this.m_NKM_CONTRACT_HELI_FX2.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_CONTRACT_HELI_FX3.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_CONTRACT_HELI_FX7.gameObject, false);
				this.m_NKM_CONTRACT_HELI_FX4.ClearState();
				this.m_NKM_CONTRACT_HELI_FX4.AnimationState.SetAnimation(0, this.m_strFirstHeliAniName, false);
				this.m_NKM_CONTRACT_HELI_FX4.AnimationState.AddAnimation(0, this.m_strSecondHeliAniName, true, 0f);
				this.m_NKM_CONTRACT_HELI_FX5.ClearState();
				this.m_NKM_CONTRACT_HELI_FX5.AnimationState.SetAnimation(0, "BASE", false);
				this.m_NKM_CONTRACT_HELI_FX6.ClearState();
				this.m_NKM_CONTRACT_HELI_FX6.AnimationState.SetAnimation(0, "BASE", false);
				this.m_NKM_CONTRACT_HELI_FX6.AnimationState.AddAnimation(0, "BASE2", true, 0f);
				this.m_lstCurPlaySoundID.Add(NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_LAND", 1f, 0f, 0f, false, 0f, false, 0f));
				this.m_lstCurPlaySoundID.Add(NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_LOOP", 1f, 0f, 0f, false, 0f, false, 0f));
				NKCUtil.SetGameobjectActive(this.m_VFX, false);
				return;
			case NKCUIContractSequence.eSequence.ENABLE:
				this.m_bSkip = true;
				this.m_NKM_CONTRACT_HELI_FX1.AnimationState.Complete -= this.SetNextAni;
				this.m_NKM_CONTRACT_HELI_FX1.AnimationState.ClearTracks();
				this.m_NKM_CONTRACT_HELI_FX5.AnimationState.ClearTracks();
				NKCUtil.SetGameobjectActive(this.m_NKM_CONTRACT_HELI_FX2.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_CONTRACT_HELI_FX3.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_CONTRACT_HELI_FX7.gameObject, true);
				this.m_NKM_CONTRACT_HELI_Animator.enabled = true;
				this.m_NKM_CONTRACT_HELI_FX1.AnimationState.SetAnimation(0, "BASE2", true);
				this.TrackEntry2 = this.m_NKM_CONTRACT_HELI_FX2.AnimationState.SetAnimation(0, "BASE2", false);
				this.m_NKM_CONTRACT_HELI_FX2.Update(0.001f);
				this.TrackEntry3 = this.m_NKM_CONTRACT_HELI_FX3.AnimationState.SetAnimation(0, "BASE2", false);
				this.m_NKM_CONTRACT_HELI_FX3.Update(0.001f);
				this.TrackEntry5 = this.m_NKM_CONTRACT_HELI_FX5.AnimationState.SetAnimation(0, "BASE2", false);
				this.TrackEntry7 = this.m_NKM_CONTRACT_HELI_FX7.AnimationState.SetAnimation(0, "BASE2", false);
				this.m_NKM_CONTRACT_HELI_FX7.Update(0.001f);
				this.m_NKM_CONTRACT_HELI_FX4.AnimationState.SetAnimation(0, this.m_strSecondHeliAniName, true);
				this.m_NKM_CONTRACT_HELI_FX6.AnimationState.SetAnimation(0, "BASE2", true);
				this.m_fAniEndTime = this.TrackEntry2.AnimationEnd;
				this.m_NKM_CONTRACT_HELI_FX2.AnimationState.TimeScale = 0f;
				this.m_NKM_CONTRACT_HELI_FX3.AnimationState.TimeScale = 0f;
				this.m_NKM_CONTRACT_HELI_FX5.AnimationState.TimeScale = 0f;
				this.m_NKM_CONTRACT_HELI_FX7.AnimationState.TimeScale = 0f;
				NKCUtil.SetGameobjectActive(this.m_VFX, true);
				NKCUtil.SetGameobjectActive(this.m_objDrag_Induce, true);
				break;
			default:
				return;
			}
		}

		// Token: 0x06008DB2 RID: 36274 RVA: 0x003031B4 File Offset: 0x003013B4
		private void Clear()
		{
			for (int i = 0; i < this.m_mrNKM_CONTRACT_START.materials.Length; i++)
			{
				Color color = this.m_mrNKM_CONTRACT_START.materials[i].color;
				color.a = 1f;
				this.m_mrNKM_CONTRACT_START.materials[i].SetColor("_Color", color);
			}
			this.m_CurStatus = NKCUIContractSequence.eSequence.NONE;
			this.m_fStartVal = 0f;
			this.m_fEndVal = 0f;
			this.m_NKM_CONTRACT_HELI_Animator.enabled = true;
			this.m_NKM_CONTRACT_HELI_Animator.Update(0.001f);
			this.m_NKM_CONTRACT_HELI_Animator.enabled = false;
			this.m_iTargetGrade = 0;
			this.m_iVfxGrade = 0;
			this.bNormalSoundPlay = false;
			this.bSRSoundPlay = false;
			this.bSSRSoundPlay = false;
			this.bGradeRePlay = false;
		}

		// Token: 0x06008DB3 RID: 36275 RVA: 0x0030327F File Offset: 0x0030147F
		public void SetNextAni(TrackEntry trackEntry)
		{
			this.UpdateState(NKCUIContractSequence.eSequence.ENABLE);
		}

		// Token: 0x06008DB4 RID: 36276 RVA: 0x00303288 File Offset: 0x00301488
		private void Update()
		{
			if (this.m_CurStatus == NKCUIContractSequence.eSequence.START && !this.AnimatorIsPlaying(this.m_NUM_CONTRACT_NEWMEMBER_ARRIVAL))
			{
				this.UpdateState(NKCUIContractSequence.eSequence.WAIT);
			}
			this.PlayHeliLandingSound();
			if (this.m_CurStatus == NKCUIContractSequence.eSequence.AUTO_CLOSE)
			{
				if (this.m_fStartVal <= 0.033f)
				{
					NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_CLOSE", 1f, 0f, 0f, false, 0f, false, 0f);
					this.m_CurStatus = NKCUIContractSequence.eSequence.ENABLE;
				}
				this.EnableAutomaticDoor();
				return;
			}
			if (this.m_CurStatus == NKCUIContractSequence.eSequence.AUTO_OPEN)
			{
				NKCUtil.SetGameobjectActive(this.m_objDrag_Induce, false);
				if (this.m_fStartVal >= this.m_fAniEndTime - 0.026f)
				{
					base.StartCoroutine(this.CallOnBackButton(this.m_fCloseTime));
					this.m_CurStatus = NKCUIContractSequence.eSequence.NONE;
				}
				this.EnableAutomaticDoor();
			}
		}

		// Token: 0x06008DB5 RID: 36277 RVA: 0x0030334B File Offset: 0x0030154B
		private IEnumerator CallOnBackButton(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			this.OnBackButton();
			yield break;
		}

		// Token: 0x06008DB6 RID: 36278 RVA: 0x00303364 File Offset: 0x00301564
		private bool AnimatorIsPlaying(Animator ani)
		{
			return 1f > ani.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}

		// Token: 0x06008DB7 RID: 36279 RVA: 0x00303388 File Offset: 0x00301588
		private void PlayHeliLandingSound()
		{
			if (this.m_CurStatus == NKCUIContractSequence.eSequence.SCENE_2 && this.TrackEntry1 != null)
			{
				if (!this.m_bPlaySoundLand2 && this.TrackEntry1.AnimationTime * 30f >= 105f)
				{
					this.m_lstCurPlaySoundID.Add(NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_LAND_02", 1f, 0f, 0f, false, 0f, false, 0f));
					this.m_bPlaySoundLand2 = true;
					return;
				}
				if (!this.m_bPlaySoundLoop2 && this.TrackEntry1.AnimationTime * 30f >= 110f)
				{
					this.m_lstCurPlaySoundID.Add(NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_LOOP_02", 1f, 0f, 0f, true, 0f, false, 0f));
					this.m_bPlaySoundLoop2 = true;
				}
			}
		}

		// Token: 0x06008DB8 RID: 36280 RVA: 0x0030345B File Offset: 0x0030165B
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06008DB9 RID: 36281 RVA: 0x00303464 File Offset: 0x00301664
		public void OnPointerDown(PointerEventData eventData)
		{
			if (!this.m_bSkip && this.m_CurStatus >= NKCUIContractSequence.eSequence.WAIT)
			{
				this.OnSkip();
			}
			this.m_fOffset = 0f;
			this.m_fMoveY = 0f;
		}

		// Token: 0x06008DBA RID: 36282 RVA: 0x00303493 File Offset: 0x00301693
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.m_bSkip && this.m_CurStatus >= NKCUIContractSequence.eSequence.WAIT)
			{
				this.OnSkip();
			}
			this.m_fOffset = 0f;
			this.m_fMoveY = 0f;
		}

		// Token: 0x06008DBB RID: 36283 RVA: 0x003034C4 File Offset: 0x003016C4
		private void OnSkip()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_ARRIVAL, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_START, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_HELI, true);
			base.StopAllCoroutines();
			this.StopCurPlaySound();
			this.m_lstCurPlaySoundID.Add(NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_LOOP_02", 1f, 0f, 0f, true, 0f, false, 0f));
			this.SetNextAni(null);
			this.m_bSkip = true;
		}

		// Token: 0x06008DBC RID: 36284 RVA: 0x00303540 File Offset: 0x00301740
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_CurStatus == NKCUIContractSequence.eSequence.ENABLE || this.m_CurStatus == NKCUIContractSequence.eSequence.DRAG)
			{
				Vector2 lhs = eventData.delta * this.m_fMoveRate;
				this.m_fOffset += Vector2.Dot(lhs, Vector2.up);
				this.m_fMoveY = this.m_fOffset * -0.003f;
				this.MoveTrack(this.TrackEntry2, this.m_fMoveY);
				this.MoveTrack(this.TrackEntry3, this.m_fMoveY);
				this.MoveTrack(this.TrackEntry5, this.m_fMoveY);
				this.MoveTrack(this.TrackEntry7, this.m_fMoveY);
				this.m_NKM_CONTRACT_HELI_Animator.SetFloat("Length", Mathf.Clamp(this.m_fMoveY, 0f, this.m_fAniEndTime));
				int integer = this.m_NKM_CONTRACT_HELI_Animator.GetInteger("Phase");
				if (!this.bOpenGate && integer >= 1)
				{
					NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
					this.bOpenGate = true;
				}
				else if (this.bOpenGate && integer < 1)
				{
					NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_HELICOPTER_CLOSE", 1f, 0f, 0f, false, 0f, false, 0f);
					this.bOpenGate = false;
				}
				if (this.m_iTargetGrade > 0 && this.m_iVfxGrade < 2)
				{
					if (integer >= 3 && !this.m_NKM_CONTRACT_HELI_Animator.GetBool("DecideSR"))
					{
						this.m_iVfxGrade = Mathf.Clamp(1, 0, this.m_iTargetGrade);
						this.m_NKM_CONTRACT_HELI_Animator.SetInteger("Grade", this.m_iVfxGrade);
					}
					else if (integer >= 5 && !this.m_NKM_CONTRACT_HELI_Animator.GetBool("DecideSSR"))
					{
						this.m_iVfxGrade = Mathf.Clamp(2, 0, this.m_iTargetGrade);
						this.m_NKM_CONTRACT_HELI_Animator.SetInteger("Grade", this.m_iVfxGrade);
					}
					if (!this.bSRSoundPlay && integer < 3 && this.m_NKM_CONTRACT_HELI_Animator.GetBool("DecideSR"))
					{
						this.bSRSoundPlay = true;
					}
					if (!this.bSSRSoundPlay && integer < 3 && this.m_NKM_CONTRACT_HELI_Animator.GetBool("DecideSR"))
					{
						this.bSSRSoundPlay = true;
					}
					if (integer >= 5 && (this.bSRSoundPlay || this.bSSRSoundPlay) && !this.bGradeRePlay)
					{
						if (this.bSRSoundPlay)
						{
							NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_GET_02", 1f, 0f, 0f, false, 0f, false, 0f);
						}
						else
						{
							NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_GET_03", 1f, 0f, 0f, false, 0f, false, 0f);
						}
						this.bGradeRePlay = true;
					}
				}
				else if (integer >= 5 && !this.bNormalSoundPlay)
				{
					this.bNormalSoundPlay = true;
					NKCSoundManager.PlaySound("FX_UI_RECRUITMNET_GET", 1f, 0f, 0f, false, 0f, false, 0f);
				}
				if (integer >= 6 || this.m_fMoveY >= 0.8f)
				{
					this.m_CurStatus = NKCUIContractSequence.eSequence.AUTO_OPEN;
					this.m_fStartVal = this.m_fMoveY;
					this.m_fEndVal = this.m_fAniEndTime;
					return;
				}
				this.m_CurStatus = NKCUIContractSequence.eSequence.DRAG;
			}
		}

		// Token: 0x06008DBD RID: 36285 RVA: 0x00303868 File Offset: 0x00301A68
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.m_CurStatus != NKCUIContractSequence.eSequence.DRAG)
			{
				return;
			}
			if (this.m_NKM_CONTRACT_HELI_Animator.GetInteger("Phase") < 5)
			{
				this.m_CurStatus = NKCUIContractSequence.eSequence.AUTO_CLOSE;
				this.m_fStartVal = this.m_fMoveY;
				this.m_fEndVal = 0f;
			}
			else
			{
				this.m_CurStatus = NKCUIContractSequence.eSequence.AUTO_OPEN;
				this.m_fStartVal = this.m_fMoveY;
				this.m_fEndVal = this.m_fAniEndTime;
			}
			this.m_fOffset = 0f;
			this.m_fMoveY = 0f;
		}

		// Token: 0x06008DBE RID: 36286 RVA: 0x003038E8 File Offset: 0x00301AE8
		private void EnableAutomaticDoor()
		{
			this.m_fStartVal = Mathf.Lerp(this.m_fStartVal, this.m_fEndVal, Time.deltaTime * this.m_fMoveSpeed);
			this.MoveTrack(this.TrackEntry2, this.m_fStartVal);
			this.MoveTrack(this.TrackEntry3, this.m_fStartVal);
			this.MoveTrack(this.TrackEntry5, this.m_fStartVal);
			this.MoveTrack(this.TrackEntry7, this.m_fStartVal);
			this.m_NKM_CONTRACT_HELI_Animator.SetFloat("Length", Mathf.Clamp(this.m_fStartVal, 0f, this.m_fAniEndTime));
		}

		// Token: 0x06008DBF RID: 36287 RVA: 0x00303986 File Offset: 0x00301B86
		private void MoveTrack(TrackEntry target, float time)
		{
			if (target == null)
			{
				return;
			}
			if (time <= 0f)
			{
				time = 0f;
			}
			if (time >= target.AnimationEnd)
			{
				time = target.AnimationEnd;
			}
			target.TrackTime = time;
		}

		// Token: 0x06008DC0 RID: 36288 RVA: 0x003039B4 File Offset: 0x00301BB4
		private void InitializeContractRandomPhase(int grade)
		{
			float value = UnityEngine.Random.value;
			if (value < 0.333f)
			{
				this.m_iVfxGrade = Mathf.Clamp(2, 0, grade);
			}
			else if (value < 0.666f)
			{
				this.m_iVfxGrade = Mathf.Clamp(1, 0, grade);
			}
			else
			{
				this.m_iVfxGrade = 0;
			}
			this.m_NKM_CONTRACT_HELI_Animator.SetInteger("Grade", this.m_iVfxGrade);
			switch (this.m_iVfxGrade)
			{
			case 0:
				this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideSR", false);
				this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideSSR", false);
				return;
			case 1:
				this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideSR", true);
				this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideSSR", false);
				return;
			case 2:
				this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideSR", true);
				this.m_NKM_CONTRACT_HELI_Animator.SetBool("DecideSSR", true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06008DC1 RID: 36289 RVA: 0x00303A96 File Offset: 0x00301C96
		public void EventEnhanceGrade(int grade)
		{
			if (grade > 2)
			{
				NKCUtil.SetGameobjectActive(this.EnhanceAWAKEN, true);
				return;
			}
			if (grade > 1)
			{
				NKCUtil.SetGameobjectActive(this.EnhanceSSR, true);
				return;
			}
			if (grade > 0)
			{
				NKCUtil.SetGameobjectActive(this.EnhanceSR, true);
			}
		}

		// Token: 0x06008DC2 RID: 36290 RVA: 0x00303ACC File Offset: 0x00301CCC
		private void StopCurPlaySound()
		{
			for (int i = 0; i < this.m_lstCurPlaySoundID.Count; i++)
			{
				NKCSoundManager.StopSound(this.m_lstCurPlaySoundID[i]);
			}
			this.m_lstCurPlaySoundID.Clear();
		}

		// Token: 0x06008DC3 RID: 36291 RVA: 0x00303B0B File Offset: 0x00301D0B
		public void EndAnimation()
		{
			this.m_NKM_CONTRACT_HELI_Animator.SetBool("End", true);
		}

		// Token: 0x06008DC4 RID: 36292 RVA: 0x00303B1E File Offset: 0x00301D1E
		private IEnumerator OnFadeOut(float fDelay)
		{
			yield return new WaitForSeconds(fDelay);
			float num = this.m_fFadeOutTime / this.m_fUpdateTime;
			float fAlphaVal = this.m_fFadeOutStartValue;
			float fAddVal = (this.m_fFadeOutStartValue - this.m_fFadeOutEndValue) / num;
			bool bFadeIn = false;
			while (fAlphaVal > this.m_fFadeOutEndValue)
			{
				fAlphaVal -= fAddVal;
				if (!bFadeIn && fAlphaVal < this.m_fActiveValue)
				{
					this.UpdateState(NKCUIContractSequence.eSequence.SCENE_2);
					bFadeIn = true;
				}
				for (int i = 0; i < this.m_mrNKM_CONTRACT_START.materials.Length; i++)
				{
					Color color = this.m_mrNKM_CONTRACT_START.materials[i].color;
					color.a = Mathf.Clamp(fAlphaVal, 0f, 1f);
					this.m_mrNKM_CONTRACT_START.materials[i].SetColor("_Color", color);
				}
				yield return new WaitForSeconds(this.m_fUpdateTime);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_CONTRACT_START, false);
			yield return null;
			yield break;
		}

		// Token: 0x06008DC5 RID: 36293 RVA: 0x00303B34 File Offset: 0x00301D34
		public void OnContractVibrate()
		{
			if (this.m_iVibrateCnt > 0)
			{
				return;
			}
			this.m_iVibrateCnt++;
		}

		// Token: 0x04007A78 RID: 31352
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONTRACT_V2";

		// Token: 0x04007A79 RID: 31353
		private const string UI_ASSET_NAME = "NKM_UI_CONTRACT_V2_SEQUENCE";

		// Token: 0x04007A7A RID: 31354
		private static NKCUIContractSequence m_Instance;

		// Token: 0x04007A7B RID: 31355
		[Header("Background")]
		public RectTransform m_rtBackground;

		// Token: 0x04007A7C RID: 31356
		[Header("ARRIVAL")]
		public GameObject m_NKM_UI_CONTRACT_ARRIVAL;

		// Token: 0x04007A7D RID: 31357
		public Animator m_NUM_CONTRACT_NEWMEMBER_ARRIVAL;

		// Token: 0x04007A7E RID: 31358
		[Header("START")]
		public GameObject m_NKM_UI_CONTRACT_START;

		// Token: 0x04007A7F RID: 31359
		public SkeletonAnimation m_NKM_CONTRACT_START;

		// Token: 0x04007A80 RID: 31360
		[Header("HELI")]
		public GameObject m_NKM_UI_CONTRACT_HELI;

		// Token: 0x04007A81 RID: 31361
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX1;

		// Token: 0x04007A82 RID: 31362
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX2;

		// Token: 0x04007A83 RID: 31363
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX3;

		// Token: 0x04007A84 RID: 31364
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX4;

		// Token: 0x04007A85 RID: 31365
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX5;

		// Token: 0x04007A86 RID: 31366
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX6;

		// Token: 0x04007A87 RID: 31367
		public SkeletonAnimation m_NKM_CONTRACT_HELI_FX7;

		// Token: 0x04007A88 RID: 31368
		[Header("VFX")]
		public Animator m_NKM_CONTRACT_HELI_Animator;

		// Token: 0x04007A89 RID: 31369
		public GameObject m_VFX;

		// Token: 0x04007A8A RID: 31370
		public GameObject EnhanceSR;

		// Token: 0x04007A8B RID: 31371
		public GameObject EnhanceSSR;

		// Token: 0x04007A8C RID: 31372
		public GameObject EnhanceAWAKEN;

		// Token: 0x04007A8D RID: 31373
		public MeshRenderer m_mrNKM_CONTRACT_START;

		// Token: 0x04007A8E RID: 31374
		private float m_fAniEndTime = 2f;

		// Token: 0x04007A8F RID: 31375
		[Header("Sound-FX")]
		public GameObject m_FX_SOUND_FX_UI_RECRUITMNET_INTRO_01;

		// Token: 0x04007A90 RID: 31376
		public GameObject m_FX_SOUND_FX_UI_RECRUITMNET_INTRO_02;

		// Token: 0x04007A91 RID: 31377
		private NKCUIContractSequence.eSequence m_CurStatus = NKCUIContractSequence.eSequence.NONE;

		// Token: 0x04007A92 RID: 31378
		private int m_iTargetGrade;

		// Token: 0x04007A93 RID: 31379
		private int m_iVfxGrade;

		// Token: 0x04007A94 RID: 31380
		private TrackEntry TrackEntry1;

		// Token: 0x04007A95 RID: 31381
		private TrackEntry TrackEntry2;

		// Token: 0x04007A96 RID: 31382
		private TrackEntry TrackEntry3;

		// Token: 0x04007A97 RID: 31383
		private TrackEntry TrackEntry5;

		// Token: 0x04007A98 RID: 31384
		private TrackEntry TrackEntry7;

		// Token: 0x04007A99 RID: 31385
		private float m_fStartVal;

		// Token: 0x04007A9A RID: 31386
		private float m_fEndVal;

		// Token: 0x04007A9B RID: 31387
		public float m_fMoveSpeed = 5f;

		// Token: 0x04007A9C RID: 31388
		public float m_fCloseTime = 2f;

		// Token: 0x04007A9D RID: 31389
		private const int SR_TRANSITION_PHASE = 3;

		// Token: 0x04007A9E RID: 31390
		private const int SSR_TRANSITION_PHASE = 5;

		// Token: 0x04007A9F RID: 31391
		private const int HALF_TRANSITION_PHASE = 5;

		// Token: 0x04007AA0 RID: 31392
		private const int FORCE_TRANSITION_PHASE = 6;

		// Token: 0x04007AA1 RID: 31393
		[Header("화살표")]
		public GameObject m_objDrag_Induce;

		// Token: 0x04007AA2 RID: 31394
		private UnityAction dClose;

		// Token: 0x04007AA3 RID: 31395
		private List<int> m_lstCurPlaySoundID = new List<int>();

		// Token: 0x04007AA4 RID: 31396
		private NKM_UNIT_GRADE m_eSelectGrade;

		// Token: 0x04007AA5 RID: 31397
		private bool m_bAwaken;

		// Token: 0x04007AA6 RID: 31398
		private string m_strFirstHeliAniName = "";

		// Token: 0x04007AA7 RID: 31399
		private string m_strSecondHeliAniName = "";

		// Token: 0x04007AA8 RID: 31400
		private const float closeOffset = 0.033f;

		// Token: 0x04007AA9 RID: 31401
		private const float openOffset = 0.026f;

		// Token: 0x04007AAA RID: 31402
		private bool m_bPlaySoundLand2;

		// Token: 0x04007AAB RID: 31403
		private bool m_bPlaySoundLoop2;

		// Token: 0x04007AAC RID: 31404
		private bool m_bSkip;

		// Token: 0x04007AAD RID: 31405
		private float m_fOffset;

		// Token: 0x04007AAE RID: 31406
		public float m_fMoveRate = 1f;

		// Token: 0x04007AAF RID: 31407
		private float m_fMoveY;

		// Token: 0x04007AB0 RID: 31408
		private bool bOpenGate;

		// Token: 0x04007AB1 RID: 31409
		private bool bNormalSoundPlay;

		// Token: 0x04007AB2 RID: 31410
		private bool bSRSoundPlay;

		// Token: 0x04007AB3 RID: 31411
		private bool bSSRSoundPlay;

		// Token: 0x04007AB4 RID: 31412
		private bool bGradeRePlay;

		// Token: 0x04007AB5 RID: 31413
		[Header("채용 연출 설정")]
		[Space]
		[Header("헬기유도 fade out 시작 지점(%)")]
		[Range(0f, 1f)]
		public float m_fFadeOutPercent = 0.8f;

		// Token: 0x04007AB6 RID: 31414
		[Header("헬기도착씬 등장 타이밍(alpha기준)")]
		[Range(0f, 1f)]
		public float m_fActiveValue = 0.5f;

		// Token: 0x04007AB7 RID: 31415
		[Header("fade out 종료 값(100~x)")]
		[Range(0f, 1f)]
		public float m_fFadeOutStartValue = 1f;

		// Token: 0x04007AB8 RID: 31416
		[Range(0f, 1f)]
		public float m_fFadeOutEndValue;

		// Token: 0x04007AB9 RID: 31417
		[Range(0f, 1f)]
		public float m_fFadeOutTime = 0.2f;

		// Token: 0x04007ABA RID: 31418
		public float m_fUpdateTime = 0.02f;

		// Token: 0x04007ABB RID: 31419
		private int m_iVibrateCnt;

		// Token: 0x020019C2 RID: 6594
		private enum eSequence
		{
			// Token: 0x0400ACC3 RID: 44227
			NONE = -1,
			// Token: 0x0400ACC4 RID: 44228
			START,
			// Token: 0x0400ACC5 RID: 44229
			WAIT,
			// Token: 0x0400ACC6 RID: 44230
			SCENE_1,
			// Token: 0x0400ACC7 RID: 44231
			SCENE_2,
			// Token: 0x0400ACC8 RID: 44232
			ENABLE,
			// Token: 0x0400ACC9 RID: 44233
			DRAG,
			// Token: 0x0400ACCA RID: 44234
			AUTO_CLOSE,
			// Token: 0x0400ACCB RID: 44235
			AUTO_OPEN
		}

		// Token: 0x020019C3 RID: 6595
		private enum RECEPTION_MEMBER
		{
			// Token: 0x0400ACCD RID: 44237
			HANA,
			// Token: 0x0400ACCE RID: 44238
			HANA_SUYEON,
			// Token: 0x0400ACCF RID: 44239
			GAB_HANA_SUYEON
		}
	}
}
