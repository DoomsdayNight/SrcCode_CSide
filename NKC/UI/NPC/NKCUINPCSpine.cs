using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000C04 RID: 3076
	public abstract class NKCUINPCSpine : NKCUINPCBase
	{
		// Token: 0x06008E7C RID: 36476 RVA: 0x003077A1 File Offset: 0x003059A1
		protected void ResetIdleWaitTime()
		{
			this.m_fIdleWaitTime = 0f;
		}

		// Token: 0x06008E7D RID: 36477 RVA: 0x003077B0 File Offset: 0x003059B0
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (this.m_bUseTouch && this.m_spUnitIllust != null)
			{
				this.m_spUnitIllust.raycastTarget = false;
				EventTrigger eventTrigger = base.gameObject.GetComponent<EventTrigger>();
				if (eventTrigger == null)
				{
					eventTrigger = base.gameObject.AddComponent<EventTrigger>();
				}
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener(delegate(BaseEventData data)
				{
					this.TouchIllust();
				});
				eventTrigger.triggers.Clear();
				eventTrigger.triggers.Add(entry);
			}
			if (this.m_bUseDrag && this.m_spUnitIllust != null)
			{
				this.DragEvent();
			}
			this.SetIdleWaitTime();
		}

		// Token: 0x06008E7E RID: 36478 RVA: 0x0030785D File Offset: 0x00305A5D
		public void TouchIllust()
		{
			if (this.m_spUnitIllust != null)
			{
				if (base.HasAction(NPC_ACTION_TYPE.TOUCH))
				{
					this.PlayAni(NPC_ACTION_TYPE.TOUCH, false);
					return;
				}
				this.PlayAni(NPC_ACTION_TYPE.NONE, false);
			}
		}

		// Token: 0x06008E7F RID: 36479 RVA: 0x00307887 File Offset: 0x00305A87
		private bool HasAnimation(string animName)
		{
			return !(this.m_spUnitIllust == null) && this.m_spUnitIllust.SkeletonData != null && this.m_spUnitIllust.SkeletonData.FindAnimation(animName) != null;
		}

		// Token: 0x06008E80 RID: 36480 RVA: 0x003078C0 File Offset: 0x00305AC0
		public void PlayAni(string animationName)
		{
			if (string.IsNullOrEmpty(animationName))
			{
				return;
			}
			if (this.m_spUnitIllust == null)
			{
				return;
			}
			if (this.m_spUnitIllust.AnimationState == null)
			{
				return;
			}
			if (Enum.Parse(typeof(NKCUINPCSpine.AnimationType), animationName) != null)
			{
				NKCUINPCSpine.AnimationType animationType = (NKCUINPCSpine.AnimationType)Enum.Parse(typeof(NKCUINPCSpine.AnimationType), animationName);
				if (animationType == NKCUINPCSpine.AnimationType.NOANI)
				{
					this.m_spUnitIllust.AnimationState.SetAnimation(0, "IDLE", true);
				}
				else
				{
					this.m_spUnitIllust.AnimationState.SetAnimation(0, animationType.ToString(), false);
					this.m_spUnitIllust.AnimationState.AddAnimation(0, "IDLE", true, 0f);
				}
			}
			this.ResetIdleWaitTime();
		}

		// Token: 0x06008E81 RID: 36481 RVA: 0x0030797C File Offset: 0x00305B7C
		public override void PlayAni(NPC_ACTION_TYPE actionType, bool bMute = false)
		{
			NKCNPCTemplet npctemplet = base.GetNPCTemplet(actionType);
			if (npctemplet != null)
			{
				this.PlayAni(npctemplet.m_AnimationName);
				if (!bMute)
				{
					bool bShowCaption = actionType == NPC_ACTION_TYPE.TOUCH || actionType == NPC_ACTION_TYPE.START || actionType == NPC_ACTION_TYPE.ENTER_BASE;
					NKCUINPCBase.PlayVoice(this.NPCType, npctemplet, true, false, bShowCaption);
					return;
				}
				base.StopVoice();
			}
		}

		// Token: 0x06008E82 RID: 36482 RVA: 0x003079CB File Offset: 0x00305BCB
		private bool NeedAnimationBlock(NKCUINPCSpine.AnimationType type)
		{
			return type > NKCUINPCSpine.AnimationType.IDLE && type != NKCUINPCSpine.AnimationType.START;
		}

		// Token: 0x06008E83 RID: 36483 RVA: 0x003079D8 File Offset: 0x00305BD8
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E84 RID: 36484 RVA: 0x003079DC File Offset: 0x00305BDC
		public override void OpenTalk(bool bLeft, NKC_UI_TALK_BOX_DIR dir, string talk, float fadeTime = 0f)
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
			if (this.m_trTalk_L == null && this.m_trTalk_R == null && base.transform.parent != null)
			{
				this.m_trTalk_L = base.transform.parent.Find("Root_Speach_Bubble_L");
				this.m_trTalk_R = base.transform.parent.Find("Root_Speach_Bubble_R");
			}
			Transform transform = bLeft ? this.m_trTalk_L : this.m_trTalk_R;
			if (transform == null)
			{
				this.CloseTalk();
				return;
			}
			this.m_talkBox.transform.SetParent(transform);
			this.m_talkBox.transform.localPosition = Vector3.zero;
			NKCUtil.SetGameobjectActive(this.m_talkBox, true);
			this.m_talkBox.SetDir(dir);
			this.m_talkBox.SetText(talk, fadeTime, 0f);
		}

		// Token: 0x06008E85 RID: 36485 RVA: 0x00307B0E File Offset: 0x00305D0E
		public override void CloseTalk()
		{
			if (this.m_talkInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_talkInstance);
				this.m_talkInstance = null;
				this.m_talkBox = null;
			}
		}

		// Token: 0x06008E86 RID: 36486 RVA: 0x00307B34 File Offset: 0x00305D34
		public void Update()
		{
			if (this.m_bUseIdleAni && !NKCSoundManager.IsPlayingVoice(-1))
			{
				this.m_fIdleWaitTime += Time.deltaTime;
				if (this.m_fIdleWaitTime > this.m_fIdleIntervalTime)
				{
					this.m_fIdleWaitTime = 0f;
					this.PlayAni(NPC_ACTION_TYPE.IDLE, false);
				}
			}
		}

		// Token: 0x06008E87 RID: 36487 RVA: 0x00307B84 File Offset: 0x00305D84
		protected void SetIdleWaitTime()
		{
			if (this.m_bUseIdleAni)
			{
				NKCNPCTemplet npctemplet = base.GetNPCTemplet(NPC_ACTION_TYPE.IDLE);
				if (npctemplet != null)
				{
					this.m_fIdleIntervalTime = (float)npctemplet.m_ConditionValue;
					if (this.m_fIdleIntervalTime <= 0f)
					{
						this.m_bUseIdleAni = false;
					}
				}
			}
		}

		// Token: 0x04007B96 RID: 31638
		public SkeletonGraphic m_spUnitIllust;

		// Token: 0x04007B97 RID: 31639
		public bool m_bUseTouch = true;

		// Token: 0x04007B98 RID: 31640
		public bool m_bUseDrag;

		// Token: 0x04007B99 RID: 31641
		private Transform m_trTalk_L;

		// Token: 0x04007B9A RID: 31642
		private Transform m_trTalk_R;

		// Token: 0x04007B9B RID: 31643
		private NKCComUITalkBox m_talkBox;

		// Token: 0x04007B9C RID: 31644
		private NKCAssetInstanceData m_talkInstance;

		// Token: 0x04007B9D RID: 31645
		protected bool m_bUseIdleAni;

		// Token: 0x04007B9E RID: 31646
		protected float m_fIdleIntervalTime;

		// Token: 0x04007B9F RID: 31647
		protected float m_fIdleWaitTime;

		// Token: 0x020019CE RID: 6606
		private enum AnimationType
		{
			// Token: 0x0400ACF0 RID: 44272
			NOANI,
			// Token: 0x0400ACF1 RID: 44273
			IDLE,
			// Token: 0x0400ACF2 RID: 44274
			DESPAIR,
			// Token: 0x0400ACF3 RID: 44275
			HATE,
			// Token: 0x0400ACF4 RID: 44276
			LAUGH,
			// Token: 0x0400ACF5 RID: 44277
			PRIDE,
			// Token: 0x0400ACF6 RID: 44278
			SERIOUS,
			// Token: 0x0400ACF7 RID: 44279
			START,
			// Token: 0x0400ACF8 RID: 44280
			SURPRISE,
			// Token: 0x0400ACF9 RID: 44281
			TOUCH,
			// Token: 0x0400ACFA RID: 44282
			TOUCH2,
			// Token: 0x0400ACFB RID: 44283
			THANKS
		}
	}
}
