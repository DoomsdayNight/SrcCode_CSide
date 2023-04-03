using System;
using System.Collections.Generic;
using Cs.Math;
using DG.Tweening;
using NKM;
using NKM.Templet;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Module
{
	// Token: 0x02000B1E RID: 2846
	public class NKCUIModuleContractResult : NKCUIBase, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
	{
		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06008197 RID: 33175 RVA: 0x002BAE54 File Offset: 0x002B9054
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06008198 RID: 33176 RVA: 0x002BAE57 File Offset: 0x002B9057
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06008199 RID: 33177 RVA: 0x002BAE5A File Offset: 0x002B905A
		public override string MenuName
		{
			get
			{
				return "NKCUIModuleResult ��� ����";
			}
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x002BAE61 File Offset: 0x002B9061
		public static void CheckInstanceAndClose()
		{
			if (NKCUIModuleContractResult.m_loadedUIData != null)
			{
				NKCUIModuleContractResult.m_loadedUIData.CloseInstance();
				NKCUIModuleContractResult.m_loadedUIData = null;
			}
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x002BAE7C File Offset: 0x002B907C
		public static NKCUIModuleContractResult MakeInstance(string bundleName, string assetName)
		{
			if (NKCUIModuleContractResult.m_loadedUIData == null)
			{
				NKCUIModuleContractResult.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCUIModuleContractResult>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIOverlay, null);
			}
			if (NKCUIModuleContractResult.m_loadedUIData == null)
			{
				return null;
			}
			NKCUIModuleContractResult instance = NKCUIModuleContractResult.m_loadedUIData.GetInstance<NKCUIModuleContractResult>();
			if (null == instance)
			{
				return null;
			}
			return instance;
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x002BAEBE File Offset: 0x002B90BE
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x002BAED9 File Offset: 0x002B90D9
		public override void OnBackButton()
		{
			base.OnBackButton();
			if (!string.IsNullOrEmpty(this.m_strBGMClipName))
			{
				NKCUIModuleHome.PlayBGMMusic();
			}
			UnityAction unityAction = this.dClose;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x0600819E RID: 33182 RVA: 0x002BAF04 File Offset: 0x002B9104
		public void Open(List<NKMUnitData> lstUnits, UnityAction close = null)
		{
			if (lstUnits.Count == 0)
			{
				return;
			}
			if (this.m_ObjectShake.trTarget != null)
			{
				this.m_ShakeObjectOriPos = this.m_ObjectShake.trTarget.transform.position;
			}
			NKCUtil.SetBindFunction(this.m_csbtnSkip, new UnityAction(this.OnSkip));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.dClose = close;
			this.m_targetGrade = NKM_UNIT_GRADE.NUG_N;
			this.m_bAwake = false;
			this.m_curState = NKCUIModuleContractResult.GACHA_SEQUENCE.NONE;
			foreach (NKMUnitData nkmunitData in lstUnits)
			{
				if (this.m_targetGrade < nkmunitData.GetUnitGrade())
				{
					this.m_targetGrade = nkmunitData.GetUnitGrade();
				}
				if (nkmunitData.GetUnitGrade() == NKM_UNIT_GRADE.NUG_SSR)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData);
					if (unitTempletBase != null && !this.m_bAwake)
					{
						this.m_bAwake = unitTempletBase.m_bAwaken;
					}
				}
			}
			if (this.m_SkeletonGraphicBackGround == null || this.m_SkeletonGraphicMain == null)
			{
				this.OnBackButton();
			}
			else
			{
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.BASE);
			}
			if (!string.IsNullOrEmpty(this.m_strBGMClipName))
			{
				NKCSoundManager.PlayMusic(this.m_strBGMClipName, true, 1f, false, 0f, 0f);
			}
			base.UIOpened(true);
		}

		// Token: 0x0600819F RID: 33183 RVA: 0x002BB060 File Offset: 0x002B9260
		private void UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE newState)
		{
			if (this.m_curState == newState)
			{
				return;
			}
			this.m_curState = newState;
			Debug.Log(string.Format("<color=red>UpdateState : {0}</color>", this.m_curState));
			this.m_SkeletonGraphicBackGround.AnimationState.ClearTrack(0);
			this.m_SkeletonGraphicMain.AnimationState.ClearTrack(0);
			switch (this.m_curState)
			{
			case NKCUIModuleContractResult.GACHA_SEQUENCE.BASE:
				this.TrackEntry1 = this.m_SkeletonGraphicBackGround.AnimationState.SetAnimation(0, this.m_curState.ToString(), false);
				this.TrackEntry2 = this.m_SkeletonGraphicMain.AnimationState.SetAnimation(0, this.m_curState.ToString(), false);
				this.m_SkeletonGraphicMain.AnimationState.Complete += this.SetNextAni;
				this.m_SkeletonGraphicMain.AnimationState.Event += this.HandleEvent;
				break;
			case NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG:
				this.TrackEntry1 = this.m_SkeletonGraphicBackGround.AnimationState.SetAnimation(0, NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE.ToString(), true);
				this.TrackEntry2 = this.m_SkeletonGraphicMain.AnimationState.SetAnimation(0, NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE.ToString(), true);
				break;
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT1:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT2:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT3:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT4:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT5:
				this.TrackEntry1 = this.m_SkeletonGraphicBackGround.AnimationState.SetAnimation(0, this.m_curState.ToString(), true);
				this.TrackEntry2 = this.m_SkeletonGraphicMain.AnimationState.SetAnimation(0, this.m_curState.ToString(), true);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnSkip, this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG);
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x002BB240 File Offset: 0x002B9440
		private void SetNextAni(TrackEntry trackEntry)
		{
			switch (this.m_curState)
			{
			case NKCUIModuleContractResult.GACHA_SEQUENCE.BASE:
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE);
				return;
			case NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE:
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG);
				return;
			case NKCUIModuleContractResult.GACHA_SEQUENCE.BASE2:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.AUTO_PLAY:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.AUTO_BACK:
				break;
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT1:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT2:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT3:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT4:
			case NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT5:
				this.OnBackButton();
				break;
			default:
				return;
			}
		}

		// Token: 0x060081A1 RID: 33185 RVA: 0x002BB2A0 File Offset: 0x002B94A0
		private void HandleEvent(TrackEntry trackEntry, Spine.Event e)
		{
			Debug.Log(string.Format("<color=red>Get HandleEvent : {0}/{1}</color>", e.String, e.Data));
			foreach (NKCUIModuleContractResult.EventSoundData eventSoundData in this.m_lstEventSound)
			{
				if (string.Equals(eventSoundData.eventKey, e.String))
				{
					NKCSoundManager.PlaySound(eventSoundData.audioClipName, 1f, 0f, 0f, false, 0f, false, 0f);
				}
			}
		}

		// Token: 0x060081A2 RID: 33186 RVA: 0x002BB344 File Offset: 0x002B9544
		private void Update()
		{
			if (this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.AUTO_BACK)
			{
				this.m_fStartVal = Mathf.Lerp(this.m_fStartVal, this.m_fEndVal, Time.deltaTime * this.m_fAutoMoveSpeed);
				this.MoveTrack(this.TrackEntry2, this.m_fStartVal);
				if (this.m_fStartVal <= this.m_fAutoMoveOffSet)
				{
					this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG);
					return;
				}
			}
			else if (this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.AUTO_PLAY)
			{
				this.m_fStartVal = Mathf.Lerp(this.m_fStartVal, this.m_fEndVal, Time.deltaTime * this.m_fAutoMoveSpeed);
				this.MoveTrack(this.TrackEntry2, this.m_fStartVal);
				if (this.m_fStartVal >= this.m_fAniEndTime - this.m_fAutoMoveOffSet)
				{
					this.SetCachaResult();
					return;
				}
			}
			else if (this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG && this.m_bDragging && this.m_bShakeCharacter && this.m_ObjectShake.trTarget != null)
			{
				float num = Mathf.Min(1f, this.m_fMoveY / this.m_fAniEndTime);
				if (this.m_ObjectShake.startValue < num && this.m_ObjectShake.endValue >= num)
				{
					float num2 = this.m_ObjectShake.endValue - this.m_ObjectShake.startValue;
					float num3 = (float)Math.Round((double)((num - this.m_ObjectShake.startValue) * 100f / num2), 3) * 0.01f;
					int vibrato = (int)((float)this.m_ObjectShake.startVibrato + (float)(this.m_ObjectShake.EndVibrato - this.m_ObjectShake.startVibrato) * num3);
					float randomness = this.m_ObjectShake.startRandomness + (this.m_ObjectShake.endRandomness - this.m_ObjectShake.startRandomness) * num3;
					Vector3 strength = this.m_ObjectShake.startStrength + (this.m_ObjectShake.endStrength - this.m_ObjectShake.startStrength) * num3;
					this.m_ObjectShake.trTarget.DOShakePosition(1f, strength, vibrato, randomness, false, true, ShakeRandomnessMode.Full);
				}
			}
		}

		// Token: 0x060081A3 RID: 33187 RVA: 0x002BB554 File Offset: 0x002B9754
		private void SetCachaResult()
		{
			switch (this.m_targetGrade)
			{
			default:
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT1);
				return;
			case NKM_UNIT_GRADE.NUG_SR:
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT2);
				return;
			case NKM_UNIT_GRADE.NUG_SSR:
				if (!this.m_bAwake)
				{
					this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT3);
					return;
				}
				if (RandomGenerator.Range(1, 10) <= 5)
				{
					this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT4);
					return;
				}
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.RESULT5);
				return;
			}
		}

		// Token: 0x060081A4 RID: 33188 RVA: 0x002BB5BB File Offset: 0x002B97BB
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x060081A5 RID: 33189 RVA: 0x002BB5C4 File Offset: 0x002B97C4
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.BASE || this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE)
			{
				this.UpdateAniState(NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG);
			}
		}

		// Token: 0x060081A6 RID: 33190 RVA: 0x002BB5DF File Offset: 0x002B97DF
		public void OnSkip()
		{
			if (this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.BASE || this.m_curState == NKCUIModuleContractResult.GACHA_SEQUENCE.IDLE)
			{
				return;
			}
			this.OnBackButton();
		}

		// Token: 0x060081A7 RID: 33191 RVA: 0x002BB5FC File Offset: 0x002B97FC
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.m_curState != NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG)
			{
				return;
			}
			this.TrackEntry2 = this.m_SkeletonGraphicMain.AnimationState.SetAnimation(0, NKCUIModuleContractResult.GACHA_SEQUENCE.BASE2.ToString(), false);
			this.m_fAniEndTime = this.TrackEntry2.AnimationEnd;
			this.TrackEntry2.TimeScale = 0f;
			this.MoveTrack(this.TrackEntry2, 0f);
			this.m_fOffset = 0f;
			this.m_fMoveY = 0f;
		}

		// Token: 0x060081A8 RID: 33192 RVA: 0x002BB684 File Offset: 0x002B9884
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_curState != NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG)
			{
				return;
			}
			Vector2 lhs = eventData.delta * this.m_fMoveRate;
			this.m_fOffset += Vector2.Dot(lhs, Vector2.up);
			this.m_fMoveY = this.m_fOffset * -0.003f;
			this.m_bDragging = true;
			this.MoveTrack(this.TrackEntry2, this.m_fMoveY);
		}

		// Token: 0x060081A9 RID: 33193 RVA: 0x002BB6F0 File Offset: 0x002B98F0
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.m_curState != NKCUIModuleContractResult.GACHA_SEQUENCE.DRAG)
			{
				return;
			}
			this.m_bDragging = false;
			DOTween.Kill(this.m_ObjectShake.trTarget, false);
			this.m_ObjectShake.trTarget.DOMove(this.m_ShakeObjectOriPos, 0.1f, false);
			this.TrackEntry2.TimeScale = 1f;
			this.m_fStartVal = this.m_fMoveY;
			if (this.m_fMoveY > this.m_fAniEndTime / 2f)
			{
				this.m_curState = NKCUIModuleContractResult.GACHA_SEQUENCE.AUTO_PLAY;
				this.m_fEndVal = this.m_fAniEndTime;
			}
			else
			{
				this.m_curState = NKCUIModuleContractResult.GACHA_SEQUENCE.AUTO_BACK;
				this.m_fEndVal = 0f;
			}
			this.m_fOffset = 0f;
			this.m_fMoveY = 0f;
		}

		// Token: 0x060081AA RID: 33194 RVA: 0x002BB7AA File Offset: 0x002B99AA
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

		// Token: 0x04006DCA RID: 28106
		public SkeletonGraphic m_SkeletonGraphicBackGround;

		// Token: 0x04006DCB RID: 28107
		public SkeletonGraphic m_SkeletonGraphicMain;

		// Token: 0x04006DCC RID: 28108
		private Vector3 m_ShakeObjectOriPos = Vector3.zero;

		// Token: 0x04006DCD RID: 28109
		public NKCUIComStateButton m_csbtnSkip;

		// Token: 0x04006DCE RID: 28110
		[Header("����")]
		public float m_fAutoMoveSpeed = 3f;

		// Token: 0x04006DCF RID: 28111
		[Header("Shake Option")]
		public bool m_bShakeCharacter = true;

		// Token: 0x04006DD0 RID: 28112
		public NKCUIModuleContractResult.ShakeOption m_ObjectShake;

		// Token: 0x04006DD1 RID: 28113
		[Header("���� ����")]
		public string m_strBGMClipName;

		// Token: 0x04006DD2 RID: 28114
		public List<NKCUIModuleContractResult.EventSoundData> m_lstEventSound;

		// Token: 0x04006DD3 RID: 28115
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04006DD4 RID: 28116
		private UnityAction dClose;

		// Token: 0x04006DD5 RID: 28117
		private NKM_UNIT_GRADE m_targetGrade;

		// Token: 0x04006DD6 RID: 28118
		private bool m_bAwake;

		// Token: 0x04006DD7 RID: 28119
		private NKCUIModuleContractResult.GACHA_SEQUENCE m_curState;

		// Token: 0x04006DD8 RID: 28120
		private TrackEntry TrackEntry1;

		// Token: 0x04006DD9 RID: 28121
		private TrackEntry TrackEntry2;

		// Token: 0x04006DDA RID: 28122
		private float m_fAniEndTime;

		// Token: 0x04006DDB RID: 28123
		private float m_fAutoMoveOffSet = 0.03f;

		// Token: 0x04006DDC RID: 28124
		private float m_fOffset;

		// Token: 0x04006DDD RID: 28125
		public float m_fMoveRate = 1f;

		// Token: 0x04006DDE RID: 28126
		private float m_fMoveY;

		// Token: 0x04006DDF RID: 28127
		private bool m_bDragging;

		// Token: 0x04006DE0 RID: 28128
		private float m_fStartVal;

		// Token: 0x04006DE1 RID: 28129
		private float m_fEndVal;

		// Token: 0x020018B8 RID: 6328
		public enum GACHA_SEQUENCE
		{
			// Token: 0x0400A9AE RID: 43438
			NONE,
			// Token: 0x0400A9AF RID: 43439
			BASE,
			// Token: 0x0400A9B0 RID: 43440
			IDLE,
			// Token: 0x0400A9B1 RID: 43441
			BASE2,
			// Token: 0x0400A9B2 RID: 43442
			DRAG,
			// Token: 0x0400A9B3 RID: 43443
			AUTO_PLAY,
			// Token: 0x0400A9B4 RID: 43444
			AUTO_BACK,
			// Token: 0x0400A9B5 RID: 43445
			RESULT1,
			// Token: 0x0400A9B6 RID: 43446
			RESULT2,
			// Token: 0x0400A9B7 RID: 43447
			RESULT3,
			// Token: 0x0400A9B8 RID: 43448
			RESULT4,
			// Token: 0x0400A9B9 RID: 43449
			RESULT5
		}

		// Token: 0x020018B9 RID: 6329
		[Serializable]
		public struct ShakeOption
		{
			// Token: 0x0400A9BA RID: 43450
			public Transform trTarget;

			// Token: 0x0400A9BB RID: 43451
			public float startValue;

			// Token: 0x0400A9BC RID: 43452
			public float endValue;

			// Token: 0x0400A9BD RID: 43453
			public Vector3 startStrength;

			// Token: 0x0400A9BE RID: 43454
			public Vector3 endStrength;

			// Token: 0x0400A9BF RID: 43455
			public int startVibrato;

			// Token: 0x0400A9C0 RID: 43456
			public int EndVibrato;

			// Token: 0x0400A9C1 RID: 43457
			public float startRandomness;

			// Token: 0x0400A9C2 RID: 43458
			public float endRandomness;
		}

		// Token: 0x020018BA RID: 6330
		[Serializable]
		public struct EventSoundData
		{
			// Token: 0x0400A9C3 RID: 43459
			public string eventKey;

			// Token: 0x0400A9C4 RID: 43460
			public string audioClipName;
		}
	}
}
