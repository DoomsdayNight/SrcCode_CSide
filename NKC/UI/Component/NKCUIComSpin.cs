using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Component
{
	// Token: 0x02000C5F RID: 3167
	public class NKCUIComSpin : MonoBehaviour, IDragHandler, IEventSystemHandler, IEndDragHandler, IBeginDragHandler, IScrollHandler
	{
		// Token: 0x06009371 RID: 37745 RVA: 0x003253C0 File Offset: 0x003235C0
		public void Init(int itemCount, NKCUIBase uiBase)
		{
			this.m_uiBase = uiBase;
			this.m_ItemCount = itemCount;
			this.MENU_ANGLE = 360f / (float)itemCount;
			this.MAX_ROTATION = this.MENU_ANGLE * this.m_fRotateLimitRate;
			this.m_SelectedItemIndex = 0;
			base.transform.localRotation = Quaternion.identity;
		}

		// Token: 0x06009372 RID: 37746 RVA: 0x00325413 File Offset: 0x00323613
		private float GetMenuRotAngle(int menuindex)
		{
			return (float)menuindex * this.MENU_ANGLE;
		}

		// Token: 0x06009373 RID: 37747 RVA: 0x0032541E File Offset: 0x0032361E
		private void SetItem(int menuIndex)
		{
			this.RotateToIndex(menuIndex, true);
			if (this.dOnSelectItem != null)
			{
				this.dOnSelectItem(menuIndex);
			}
		}

		// Token: 0x06009374 RID: 37748 RVA: 0x0032543C File Offset: 0x0032363C
		public void RotateToIndex(int menuIndex, bool bAnimate = true)
		{
			this.m_SelectedItemIndex = menuIndex;
			base.transform.DOKill(false);
			if (!bAnimate)
			{
				base.transform.localRotation = Quaternion.Euler(0f, this.GetMenuRotAngle(menuIndex), 0f);
				return;
			}
			float num = this.NormalizeAngle(base.transform.localRotation.eulerAngles.y, -this.MENU_ANGLE * 0.5f);
			base.transform.localRotation = Quaternion.Euler(0f, num, 0f);
			float menuRotAngle = this.GetMenuRotAngle(menuIndex);
			float num2 = Mathf.Abs(menuRotAngle - num);
			float duration;
			if (num2 >= this.MENU_ANGLE)
			{
				duration = this.m_fMenuSpinTime;
			}
			else
			{
				duration = num2 / this.MENU_ANGLE * this.m_fMenuSpinTime;
			}
			base.transform.DOLocalRotate(new Vector3(0f, menuRotAngle, 0f), duration, RotateMode.Fast).SetEase(this.m_eEase);
		}

		// Token: 0x06009375 RID: 37749 RVA: 0x00325526 File Offset: 0x00323726
		private float NormalizeAngle(float value)
		{
			while (value >= 360f)
			{
				value -= 360f;
			}
			while (value < 0f)
			{
				value += 360f;
			}
			return value;
		}

		// Token: 0x06009376 RID: 37750 RVA: 0x0032554F File Offset: 0x0032374F
		private float NormalizeAngle(float Value, float Offset)
		{
			return this.NormalizeAngle(Value - Offset) + Offset;
		}

		// Token: 0x06009377 RID: 37751 RVA: 0x0032555C File Offset: 0x0032375C
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.m_bDrag)
			{
				return;
			}
			float num = this.MENU_ANGLE * Time.deltaTime * this.MAX_ROT_SPEED;
			float num2 = eventData.delta.x * this.m_fRate;
			if (num2 > num)
			{
				num2 = num;
			}
			else if (num2 < -num)
			{
				num2 = -num;
			}
			this.RotOffset += num2;
			if (this.RotOffset >= this.MAX_ROTATION)
			{
				num2 = this.MAX_ROTATION;
			}
			else if (this.RotOffset <= -this.MAX_ROTATION)
			{
				num2 = -this.MAX_ROTATION;
			}
			else if (this.RotOffset > 0f)
			{
				num2 = NKCUtil.TrackValue(TRACKING_DATA_TYPE.TDT_SLOWER, 0f, this.MAX_ROTATION, this.RotOffset, this.MAX_ROTATION);
			}
			else if (this.RotOffset < 0f)
			{
				num2 = NKCUtil.TrackValue(TRACKING_DATA_TYPE.TDT_SLOWER, 0f, -this.MAX_ROTATION, this.RotOffset, -this.MAX_ROTATION);
			}
			else
			{
				num2 = 0f;
			}
			base.transform.localRotation = Quaternion.Euler(0f, this.GetMenuRotAngle(this.m_SelectedItemIndex) + num2, 0f);
		}

		// Token: 0x06009378 RID: 37752 RVA: 0x00325671 File Offset: 0x00323871
		public void OnEndDrag(PointerEventData eventData)
		{
			this.m_bDrag = false;
			this.ChangeMenuByRotOffset(this.RotOffset);
			this.RotOffset = 0f;
		}

		// Token: 0x06009379 RID: 37753 RVA: 0x00325691 File Offset: 0x00323891
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.m_bDrag = true;
			this.RotOffset = 0f;
			base.transform.DOComplete(false);
		}

		// Token: 0x0600937A RID: 37754 RVA: 0x003256B2 File Offset: 0x003238B2
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y < 0f)
			{
				this.ChangeMenuByRotOffset(this.ROT_THRESHOLD);
				return;
			}
			if (eventData.scrollDelta.y > 0f)
			{
				this.ChangeMenuByRotOffset(-this.ROT_THRESHOLD);
			}
		}

		// Token: 0x0600937B RID: 37755 RVA: 0x003256F4 File Offset: 0x003238F4
		private void ChangeMenuByRotOffset(float RotateOffset)
		{
			this.m_bDrag = false;
			int num;
			if (RotateOffset >= this.ROT_THRESHOLD)
			{
				num = this.m_SelectedItemIndex + 1;
				if (num >= this.m_ItemCount)
				{
					num = 0;
				}
			}
			else if (RotateOffset <= -this.ROT_THRESHOLD)
			{
				num = this.m_SelectedItemIndex - 1;
				if (num < 0)
				{
					num = this.m_ItemCount - 1;
				}
			}
			else
			{
				num = this.m_SelectedItemIndex;
			}
			this.SetItem(num);
		}

		// Token: 0x0600937C RID: 37756 RVA: 0x00325758 File Offset: 0x00323958
		public float GetDeltaIndex(int index)
		{
			return this.NormalizeAngle(base.transform.localRotation.eulerAngles.y - this.GetMenuRotAngle(index), -180f) / this.MENU_ANGLE;
		}

		// Token: 0x0600937D RID: 37757 RVA: 0x00325797 File Offset: 0x00323997
		private void OnDisable()
		{
			this.m_bDrag = false;
			this.RotateToIndex(this.m_SelectedItemIndex, false);
		}

		// Token: 0x0600937E RID: 37758 RVA: 0x003257B0 File Offset: 0x003239B0
		private void Update()
		{
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Left) && this.m_uiBase != null && NKCUIManager.IsTopmostUI(this.m_uiBase))
			{
				this.ChangeMenuByRotOffset(-this.ROT_THRESHOLD);
				NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Left);
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Right) && this.m_uiBase != null && NKCUIManager.IsTopmostUI(this.m_uiBase))
			{
				this.ChangeMenuByRotOffset(this.ROT_THRESHOLD);
				NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Right);
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey) && this.m_uiBase != null && NKCUIManager.IsTopmostUI(this.m_uiBase))
			{
				NKCUIComHotkeyDisplay.OpenInstance(base.transform, new HotkeyEventType[]
				{
					HotkeyEventType.Left,
					HotkeyEventType.Right
				});
			}
		}

		// Token: 0x04008062 RID: 32866
		public NKCUIComSpin.OnSelectItem dOnSelectItem;

		// Token: 0x04008063 RID: 32867
		[Header("메뉴 몇 개?")]
		public int m_ItemCount = 3;

		// Token: 0x04008064 RID: 32868
		[Header("메뉴 하나 돌아가는데 걸리는 시간")]
		public float m_fMenuSpinTime = 1f;

		// Token: 0x04008065 RID: 32869
		[Header("드래그시 최고 속도")]
		public float MAX_ROT_SPEED = 2f;

		// Token: 0x04008066 RID: 32870
		[Header("메뉴 돌아갈때 사용할 Ease Function")]
		public Ease m_eEase = Ease.OutBack;

		// Token: 0x04008067 RID: 32871
		[Header("입력 대비 회전 속도")]
		public float m_fRate = -0.2f;

		// Token: 0x04008068 RID: 32872
		[Header("회전시 좌우로 메뉴 몇개분이 더 돌아가는가")]
		public float m_fRotateLimitRate = 1.25f;

		// Token: 0x04008069 RID: 32873
		[Header("몇 도 이상 회전해야 다음 메뉴로 넘어가는가?")]
		public float ROT_THRESHOLD = 6f;

		// Token: 0x0400806A RID: 32874
		private float MENU_ANGLE = 120f;

		// Token: 0x0400806B RID: 32875
		private float MAX_ROTATION = 150f;

		// Token: 0x0400806C RID: 32876
		private int m_SelectedItemIndex;

		// Token: 0x0400806D RID: 32877
		private float RotOffset;

		// Token: 0x0400806E RID: 32878
		private NKCUIBase m_uiBase;

		// Token: 0x0400806F RID: 32879
		private bool m_bDrag;

		// Token: 0x02001A1B RID: 6683
		// (Invoke) Token: 0x0600BB16 RID: 47894
		public delegate void OnSelectItem(int selectedIndex);
	}
}
