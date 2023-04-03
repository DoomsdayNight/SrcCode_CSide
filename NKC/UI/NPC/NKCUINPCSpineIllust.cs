using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000C05 RID: 3077
	public class NKCUINPCSpineIllust : MonoBehaviour
	{
		// Token: 0x06008E8A RID: 36490 RVA: 0x00307BDC File Offset: 0x00305DDC
		private void Awake()
		{
			if (this.m_bUseTouch && this.m_spUnitIllust != null)
			{
				this.m_spUnitIllust.raycastTarget = false;
				EventTrigger eventTrigger = this.m_spUnitIllust.gameObject.GetComponent<EventTrigger>();
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
		}

		// Token: 0x06008E8B RID: 36491 RVA: 0x00307C6C File Offset: 0x00305E6C
		private void OnEnable()
		{
			if (this.m_spUnitIllust != null)
			{
				if (!this.m_spUnitIllust.IsValid)
				{
					return;
				}
				if (this.HasAnimation("START"))
				{
					this.m_spUnitIllust.AnimationState.SetAnimation(0, "START", false);
					this.m_spUnitIllust.AnimationState.AddAnimation(0, this.defaultAnimation, true, 0f);
					return;
				}
				this.m_spUnitIllust.AnimationState.SetAnimation(0, this.defaultAnimation, true);
			}
		}

		// Token: 0x06008E8C RID: 36492 RVA: 0x00307CF4 File Offset: 0x00305EF4
		public void TouchIllust()
		{
			if (this.m_spUnitIllust != null)
			{
				if (this.HasAnimation("TOUCH"))
				{
					this.m_spUnitIllust.AnimationState.SetAnimation(0, "TOUCH", false);
					this.m_spUnitIllust.AnimationState.AddAnimation(0, this.defaultAnimation, true, 0f);
					if (this.m_dOnTouch != null)
					{
						this.m_dOnTouch();
						return;
					}
				}
				else
				{
					this.m_spUnitIllust.AnimationState.SetAnimation(0, this.defaultAnimation, true);
				}
			}
		}

		// Token: 0x06008E8D RID: 36493 RVA: 0x00307D80 File Offset: 0x00305F80
		public void SetAnimation(string animName)
		{
			if (this.m_spUnitIllust == null)
			{
				return;
			}
			if (this.HasAnimation(animName))
			{
				this.m_spUnitIllust.AnimationState.SetAnimation(0, animName, false);
				this.m_spUnitIllust.AnimationState.AddAnimation(0, this.defaultAnimation, true, 0f);
				return;
			}
			this.m_spUnitIllust.AnimationState.SetAnimation(0, this.defaultAnimation, true);
		}

		// Token: 0x06008E8E RID: 36494 RVA: 0x00307DF4 File Offset: 0x00305FF4
		public void SetDefaultAnimation(string animName)
		{
			if (this.m_spUnitIllust == null)
			{
				return;
			}
			if (this.HasAnimation(animName))
			{
				Skeleton skeleton = this.m_spUnitIllust.Skeleton;
				if (skeleton != null)
				{
					skeleton.SetToSetupPose();
				}
				this.m_spUnitIllust.AnimationState.SetAnimation(0, animName, true);
				this.defaultAnimation = animName;
				return;
			}
			Skeleton skeleton2 = this.m_spUnitIllust.Skeleton;
			if (skeleton2 != null)
			{
				skeleton2.SetToSetupPose();
			}
			this.m_spUnitIllust.AnimationState.SetAnimation(0, "IDLE", true);
			this.defaultAnimation = "IDLE";
		}

		// Token: 0x06008E8F RID: 36495 RVA: 0x00307E84 File Offset: 0x00306084
		public string GetCurrentAnimationName()
		{
			return this.m_spUnitIllust.AnimationState.GetCurrent(0).Animation.Name;
		}

		// Token: 0x06008E90 RID: 36496 RVA: 0x00307EA1 File Offset: 0x003060A1
		private bool HasAnimation(string animName)
		{
			return !(this.m_spUnitIllust == null) && this.m_spUnitIllust.SkeletonData != null && this.m_spUnitIllust.SkeletonData.FindAnimation(animName) != null;
		}

		// Token: 0x04007BA0 RID: 31648
		public SkeletonGraphic m_spUnitIllust;

		// Token: 0x04007BA1 RID: 31649
		public bool m_bUseTouch = true;

		// Token: 0x04007BA2 RID: 31650
		public NKCUINPCSpineIllust.OnTouch m_dOnTouch;

		// Token: 0x04007BA3 RID: 31651
		private string defaultAnimation = "IDLE";

		// Token: 0x020019CF RID: 6607
		// (Invoke) Token: 0x0600BA33 RID: 47667
		public delegate void OnTouch();
	}
}
