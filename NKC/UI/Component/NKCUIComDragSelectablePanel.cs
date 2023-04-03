using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Component
{
	// Token: 0x02000C53 RID: 3155
	public class NKCUIComDragSelectablePanel : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060092E8 RID: 37608 RVA: 0x00322564 File Offset: 0x00320764
		// (remove) Token: 0x060092E9 RID: 37609 RVA: 0x0032259C File Offset: 0x0032079C
		public event NKCUIComDragSelectablePanel.OnGetObject dOnGetObject;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060092EA RID: 37610 RVA: 0x003225D4 File Offset: 0x003207D4
		// (remove) Token: 0x060092EB RID: 37611 RVA: 0x0032260C File Offset: 0x0032080C
		public event NKCUIComDragSelectablePanel.OnReturnObject dOnReturnObject;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060092EC RID: 37612 RVA: 0x00322644 File Offset: 0x00320844
		// (remove) Token: 0x060092ED RID: 37613 RVA: 0x0032267C File Offset: 0x0032087C
		public event NKCUIComDragSelectablePanel.OnProvideData dOnProvideData;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060092EE RID: 37614 RVA: 0x003226B4 File Offset: 0x003208B4
		// (remove) Token: 0x060092EF RID: 37615 RVA: 0x003226EC File Offset: 0x003208EC
		public event NKCUIComDragSelectablePanel.OnFocus dOnFocus;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060092F0 RID: 37616 RVA: 0x00322724 File Offset: 0x00320924
		// (remove) Token: 0x060092F1 RID: 37617 RVA: 0x0032275C File Offset: 0x0032095C
		public event NKCUIComDragSelectablePanel.OnFocusColor dOnFocusColor;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060092F2 RID: 37618 RVA: 0x00322794 File Offset: 0x00320994
		// (remove) Token: 0x060092F3 RID: 37619 RVA: 0x003227CC File Offset: 0x003209CC
		public event NKCUIComDragSelectablePanel.OnIndexChangeListener dOnIndexChangeListener;

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060092F4 RID: 37620 RVA: 0x00322801 File Offset: 0x00320A01
		public Vector2 ScrollDirection
		{
			get
			{
				return this.m_vScrollDirection.normalized;
			}
		}

		// Token: 0x060092F5 RID: 37621 RVA: 0x0032280E File Offset: 0x00320A0E
		public float GetDragOffset()
		{
			return this.m_fOffset;
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x060092F6 RID: 37622 RVA: 0x00322816 File Offset: 0x00320A16
		// (set) Token: 0x060092F7 RID: 37623 RVA: 0x0032281E File Offset: 0x00320A1E
		public int TotalCount
		{
			get
			{
				return this._totalCount;
			}
			set
			{
				this._totalCount = value;
				if (this.m_dotList != null)
				{
					this.m_dotList.SetMaxCount(value);
				}
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x060092F8 RID: 37624 RVA: 0x00322841 File Offset: 0x00320A41
		// (set) Token: 0x060092F9 RID: 37625 RVA: 0x00322849 File Offset: 0x00320A49
		public int CurrentIndex
		{
			get
			{
				return this.m_currentIndex;
			}
			set
			{
				this.m_currentIndex = value;
			}
		}

		// Token: 0x060092FA RID: 37626 RVA: 0x00322852 File Offset: 0x00320A52
		public RectTransform GetCurrentItem()
		{
			return this.currentItem;
		}

		// Token: 0x060092FB RID: 37627 RVA: 0x0032285C File Offset: 0x00320A5C
		private RectTransform GetObject()
		{
			NKCUIComDragSelectablePanel.OnGetObject onGetObject = this.dOnGetObject;
			RectTransform rectTransform = (onGetObject != null) ? onGetObject() : null;
			if (rectTransform != null)
			{
				rectTransform.SetParent(this.m_rtContentRect, false);
			}
			return rectTransform;
		}

		// Token: 0x060092FC RID: 37628 RVA: 0x00322893 File Offset: 0x00320A93
		private void ReturnObject(RectTransform rt)
		{
			if (rt != null)
			{
				NKCUIComDragSelectablePanel.OnReturnObject onReturnObject = this.dOnReturnObject;
				if (onReturnObject == null)
				{
					return;
				}
				onReturnObject(rt);
			}
		}

		// Token: 0x060092FD RID: 37629 RVA: 0x003228B0 File Offset: 0x00320AB0
		private void SetFocus(RectTransform rt, bool bIsFocus)
		{
			if (rt == null || !this.m_bBannerRotation)
			{
				return;
			}
			if (this.dOnFocus != null)
			{
				this.dOnFocus(rt, bIsFocus);
			}
			if (this.dOnFocusColor != null)
			{
				if (bIsFocus)
				{
					this.dOnFocusColor(rt, this.m_FocusColor);
					return;
				}
				this.dOnFocusColor(rt, this.m_UnFocusColor);
			}
		}

		// Token: 0x060092FE RID: 37630 RVA: 0x00322914 File Offset: 0x00320B14
		private void ProvideData(RectTransform rt, int index, int changeVal = 0)
		{
			if (rt == null)
			{
				return;
			}
			if (this.m_bBannerRotation)
			{
				if (index < 0)
				{
					index = this.TotalCount - 1;
				}
				if (index > this.TotalCount - 1)
				{
					index = 0;
				}
			}
			else
			{
				if (index < 0)
				{
					NKCUtil.SetGameobjectActive(rt, false);
					return;
				}
				if (index >= this.TotalCount)
				{
					NKCUtil.SetGameobjectActive(rt, false);
					return;
				}
			}
			NKCUtil.SetGameobjectActive(rt, true);
			NKCUIComDragSelectablePanel.OnProvideData onProvideData = this.dOnProvideData;
			if (onProvideData == null)
			{
				return;
			}
			onProvideData(rt, index);
		}

		// Token: 0x060092FF RID: 37631 RVA: 0x00322988 File Offset: 0x00320B88
		public void Init(bool rotation = false, bool bUseShortcut = true)
		{
			if (this.m_rtContentRect == null)
			{
				Debug.LogError(base.gameObject.name + " : Content Rect Null!");
				return;
			}
			if (this.m_csbtnPrev != null)
			{
				this.m_csbtnPrev.PointerClick.RemoveAllListeners();
				this.m_csbtnPrev.PointerClick.AddListener(new UnityAction(this.ToPrevItem));
				if (bUseShortcut && this.m_csbtnPrev.m_HotkeyEventType == HotkeyEventType.None)
				{
					NKCUtil.SetHotkey(this.m_csbtnPrev, NKCInputManager.GetDirection(-this.m_vScrollDirection), null, false);
				}
			}
			if (this.m_csbtnNext != null)
			{
				this.m_csbtnNext.PointerClick.RemoveAllListeners();
				this.m_csbtnNext.PointerClick.AddListener(new UnityAction(this.ToNextItem));
				if (bUseShortcut && this.m_csbtnNext.m_HotkeyEventType == HotkeyEventType.None)
				{
					NKCUtil.SetHotkey(this.m_csbtnNext, NKCInputManager.GetDirection(this.m_vScrollDirection), null, false);
				}
			}
			this.m_bBannerRotation = rotation;
		}

		// Token: 0x06009300 RID: 37632 RVA: 0x00322A90 File Offset: 0x00320C90
		public void Prepare()
		{
			this.ReturnObject(this.currentItem);
			this.ReturnObject(this.nextItem);
			this.ReturnObject(this.prevItem);
			this.currentItem = this.GetObject();
			this.nextItem = this.GetObject();
			this.prevItem = this.GetObject();
		}

		// Token: 0x06009301 RID: 37633 RVA: 0x00322AE8 File Offset: 0x00320CE8
		public void SetIndex(int index)
		{
			if (this.currentItem == null)
			{
				this.Prepare();
			}
			this.m_currentIndex = index;
			if (this.m_bBannerRotation)
			{
				if (index + 1 >= this.TotalCount)
				{
					this.ProvideData(this.prevItem, index - 1, 0);
					this.ProvideData(this.currentItem, index, 0);
					this.ProvideData(this.nextItem, 0, 0);
				}
				else if (index == 0)
				{
					this.ProvideData(this.prevItem, this.TotalCount - 1, 0);
					this.ProvideData(this.currentItem, index, 0);
					this.ProvideData(this.nextItem, index + 1, 0);
				}
				else
				{
					this.ProvideData(this.prevItem, index - 1, 0);
					this.ProvideData(this.currentItem, index, 0);
					this.ProvideData(this.nextItem, index + 1, 0);
				}
			}
			else
			{
				this.ProvideData(this.prevItem, index - 1, 0);
				this.SetFocus(this.prevItem, false);
				this.ProvideData(this.currentItem, index, 0);
				this.SetFocus(this.currentItem, true);
				this.ProvideData(this.nextItem, index + 1, 0);
				this.SetFocus(this.nextItem, false);
			}
			this.SetLocationToPrev(this.prevItem, Vector2.zero);
			this.SetLocationToCenter(this.currentItem, Vector2.zero);
			this.SetLocationToNext(this.nextItem, Vector2.zero);
			this.SetSubComponents();
		}

		// Token: 0x06009302 RID: 37634 RVA: 0x00322C50 File Offset: 0x00320E50
		private void ChangeMenuByOffset(float offset)
		{
			if (offset >= this.m_fMoveThreshold)
			{
				this.ToPrevItem();
				return;
			}
			if (offset <= -this.m_fMoveThreshold)
			{
				this.ToNextItem();
				return;
			}
			this.Reposition(true);
		}

		// Token: 0x06009303 RID: 37635 RVA: 0x00322C7C File Offset: 0x00320E7C
		private void ToPrevItem()
		{
			if (this.m_bBannerRotation)
			{
				if (this.m_currentIndex == 0)
				{
					this.m_currentIndex = this.TotalCount - 1;
				}
				else
				{
					this.m_currentIndex--;
				}
			}
			else
			{
				if (this.m_currentIndex == 0)
				{
					this.Reposition(true);
					return;
				}
				this.m_currentIndex--;
			}
			RectTransform rectTransform = this.nextItem;
			this.nextItem = this.currentItem;
			this.currentItem = this.prevItem;
			this.prevItem = rectTransform;
			this.ProvideData(this.prevItem, this.m_currentIndex - 1, -1);
			this.SetLocationToPrev(this.prevItem, this.GetPrevPos() + this.Rubber(this.m_fOffset, this.OneMenuOffsetSize * this.m_fDragLimitRate) * this.ScrollDirection);
			this.Reposition(true);
			if (!this.m_bBannerRotation)
			{
				this.SetFocus(this.currentItem, true);
				this.SetFocus(this.nextItem, false);
			}
			this.m_fAutomoveDeltaTime = 0f;
			this.SetSubComponents();
		}

		// Token: 0x06009304 RID: 37636 RVA: 0x00322D88 File Offset: 0x00320F88
		private void ToNextItem()
		{
			if (this.m_bBannerRotation)
			{
				if (this.m_currentIndex + 1 >= this.TotalCount)
				{
					this.m_currentIndex = 0;
				}
				else
				{
					this.m_currentIndex++;
				}
			}
			else
			{
				if (this.m_currentIndex + 1 >= this.TotalCount)
				{
					this.Reposition(true);
					return;
				}
				this.m_currentIndex++;
			}
			RectTransform rectTransform = this.prevItem;
			this.prevItem = this.currentItem;
			this.currentItem = this.nextItem;
			this.nextItem = rectTransform;
			this.ProvideData(this.nextItem, this.m_currentIndex + 1, 1);
			this.SetLocationToNext(this.nextItem, this.GetNextPos() + this.Rubber(this.m_fOffset, this.OneMenuOffsetSize * this.m_fDragLimitRate) * this.ScrollDirection);
			this.Reposition(true);
			if (!this.m_bBannerRotation)
			{
				this.SetFocus(this.currentItem, true);
				this.SetFocus(this.prevItem, false);
			}
			this.m_fAutomoveDeltaTime = 0f;
			this.SetSubComponents();
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06009305 RID: 37637 RVA: 0x00322E9C File Offset: 0x0032109C
		private float OneMenuOffsetSize
		{
			get
			{
				return Vector2.Dot(this.m_rtContentRect.GetSize(), this.ScrollDirection);
			}
		}

		// Token: 0x06009306 RID: 37638 RVA: 0x00322EB4 File Offset: 0x003210B4
		public void OnEndDrag(PointerEventData eventData)
		{
			if (!this.m_bBannerRotation || this.TotalCount > 1)
			{
				this.ChangeMenuByOffset(this.m_fOffset);
			}
			this.m_fOffset = 0f;
			if (this.m_bBannerRotation && this.TotalCount == 1)
			{
				this.ChangeMenuByOffset(this.m_fOffset);
			}
			this.m_bPauseUpdate = false;
			this.m_fAutomoveDeltaTime = 0f;
		}

		// Token: 0x06009307 RID: 37639 RVA: 0x00322F18 File Offset: 0x00321118
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.m_fOffset = 0f;
			if (!this.m_bBannerRotation || this.TotalCount > 1)
			{
				this.prevItem.DOComplete(false);
				this.currentItem.DOComplete(false);
				this.nextItem.DOComplete(false);
			}
			this.m_bPauseUpdate = true;
			this.m_fAutomoveDeltaTime = 0f;
		}

		// Token: 0x06009308 RID: 37640 RVA: 0x00322F7C File Offset: 0x0032117C
		public void OnDrag(PointerEventData eventData)
		{
			Vector2 lhs = eventData.delta * this.m_fMoveRate;
			this.m_fOffset += Vector2.Dot(lhs, this.ScrollDirection);
			Vector2 offset = this.Rubber(this.m_fOffset, this.OneMenuOffsetSize * this.m_fDragLimitRate) * this.ScrollDirection;
			this.SetLocationToPrev(this.prevItem, offset);
			this.SetLocationToCenter(this.currentItem, offset);
			this.SetLocationToNext(this.nextItem, offset);
		}

		// Token: 0x06009309 RID: 37641 RVA: 0x00323000 File Offset: 0x00321200
		private float Rubber(float currentValue, float Limit)
		{
			if (currentValue == 0f)
			{
				return 0f;
			}
			float num = Mathf.Abs(currentValue);
			return Limit * num / (Limit + num) * Mathf.Sign(currentValue);
		}

		// Token: 0x0600930A RID: 37642 RVA: 0x00323030 File Offset: 0x00321230
		public void OnScroll(PointerEventData eventData)
		{
			this.m_fAutomoveDeltaTime = 0f;
			Vector2 scrollDelta = eventData.scrollDelta;
			if (scrollDelta.y > 0f)
			{
				this.ToPrevItem();
				return;
			}
			if (scrollDelta.y < 0f)
			{
				this.ToNextItem();
			}
		}

		// Token: 0x0600930B RID: 37643 RVA: 0x00323076 File Offset: 0x00321276
		public void CleanUp()
		{
			this.ReturnObject(this.prevItem);
			this.ReturnObject(this.currentItem);
			this.ReturnObject(this.nextItem);
			this.prevItem = null;
			this.currentItem = null;
			this.nextItem = null;
		}

		// Token: 0x0600930C RID: 37644 RVA: 0x003230B1 File Offset: 0x003212B1
		private void SetSubComponents()
		{
			this.SetDot();
			this.NotifyIndexChange();
			this.SetArrow(false);
			this.ChangeFocusColor();
		}

		// Token: 0x0600930D RID: 37645 RVA: 0x003230CC File Offset: 0x003212CC
		public void SetArrow(bool forceHide = false)
		{
			if (forceHide)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnPrev, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnNext, false);
				return;
			}
			if (this.m_bBannerRotation)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnPrev, this.TotalCount > 1);
				NKCUtil.SetGameobjectActive(this.m_csbtnNext, this.TotalCount > 1);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnPrev, this.m_currentIndex != 0);
			NKCUtil.SetGameobjectActive(this.m_csbtnNext, this.m_currentIndex < this.TotalCount - 1);
		}

		// Token: 0x0600930E RID: 37646 RVA: 0x00323155 File Offset: 0x00321355
		private void ChangeFocusColor()
		{
			this.SetFocus(this.prevItem, false);
			this.SetFocus(this.currentItem, true);
			this.SetFocus(this.nextItem, false);
		}

		// Token: 0x0600930F RID: 37647 RVA: 0x0032317E File Offset: 0x0032137E
		private void SetDot()
		{
			if (this.m_dotList == null)
			{
				return;
			}
			this.m_dotList.SetIndex(this.m_currentIndex);
		}

		// Token: 0x06009310 RID: 37648 RVA: 0x003231A0 File Offset: 0x003213A0
		private void NotifyIndexChange()
		{
			if ((!this.m_bBannerRotation || this.TotalCount > 0) && this.dOnIndexChangeListener != null)
			{
				this.dOnIndexChangeListener(this.m_currentIndex);
			}
		}

		// Token: 0x06009311 RID: 37649 RVA: 0x003231CC File Offset: 0x003213CC
		private void Reposition(bool bAnim)
		{
			if (bAnim)
			{
				this.MoveLocationToPrev(this.prevItem);
				this.MoveLocationToCenter(this.currentItem);
				this.MoveLocationToNext(this.nextItem);
				return;
			}
			this.SetLocationToPrev(this.prevItem, Vector2.zero);
			this.SetLocationToCenter(this.currentItem, Vector2.zero);
			this.SetLocationToNext(this.nextItem, Vector2.zero);
		}

		// Token: 0x06009312 RID: 37650 RVA: 0x00323234 File Offset: 0x00321434
		private Vector2 GetPrevPos()
		{
			return -this.m_rtContentRect.GetSize() * this.ScrollDirection - this.Spacing;
		}

		// Token: 0x06009313 RID: 37651 RVA: 0x0032325C File Offset: 0x0032145C
		private Vector2 GetCurrentPos()
		{
			return Vector2.zero;
		}

		// Token: 0x06009314 RID: 37652 RVA: 0x00323263 File Offset: 0x00321463
		private Vector2 GetNextPos()
		{
			return this.m_rtContentRect.GetSize() * this.ScrollDirection + this.Spacing;
		}

		// Token: 0x06009315 RID: 37653 RVA: 0x00323286 File Offset: 0x00321486
		private void SetLocationToPrev(RectTransform target, Vector2 offset)
		{
			if (target != null)
			{
				target.DOKill(false);
				target.anchoredPosition = this.GetPrevPos() + offset;
			}
		}

		// Token: 0x06009316 RID: 37654 RVA: 0x003232AB File Offset: 0x003214AB
		private void MoveLocationToPrev(RectTransform target)
		{
			if (target != null)
			{
				target.DOKill(false);
				target.DOAnchorPos(this.GetPrevPos(), this.m_fMenuMoveTime, false).SetEase(this.m_eEase);
			}
		}

		// Token: 0x06009317 RID: 37655 RVA: 0x003232DD File Offset: 0x003214DD
		private void SetLocationToCenter(RectTransform target, Vector2 offset)
		{
			if (target != null)
			{
				target.DOKill(false);
				target.anchoredPosition = this.GetCurrentPos() + offset;
				target.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06009318 RID: 37656 RVA: 0x0032330D File Offset: 0x0032150D
		private void MoveLocationToCenter(RectTransform target)
		{
			if (target != null)
			{
				target.DOKill(false);
				target.DOAnchorPos(this.GetCurrentPos(), this.m_fMenuMoveTime, false).SetEase(this.m_eEase);
				target.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06009319 RID: 37657 RVA: 0x0032334A File Offset: 0x0032154A
		private void SetLocationToNext(RectTransform target, Vector2 offset)
		{
			if (target != null)
			{
				target.DOKill(false);
				target.anchoredPosition = this.GetNextPos() + offset;
			}
		}

		// Token: 0x0600931A RID: 37658 RVA: 0x0032336F File Offset: 0x0032156F
		private void MoveLocationToNext(RectTransform target)
		{
			if (target != null)
			{
				target.DOKill(false);
				target.DOAnchorPos(this.GetNextPos(), this.m_fMenuMoveTime, false).SetEase(this.m_eEase);
			}
		}

		// Token: 0x0600931B RID: 37659 RVA: 0x003233A4 File Offset: 0x003215A4
		public void Update()
		{
			if (this.m_bPauseUpdate)
			{
				return;
			}
			if (this.m_bAutoMove && this.TotalCount > 1 && this.m_fAutoMoveTime > 0f)
			{
				this.m_fAutomoveDeltaTime += Time.deltaTime;
				if (this.m_fAutomoveDeltaTime > this.m_fAutoMoveTime)
				{
					this.m_fAutomoveDeltaTime = 0f;
					this.ToNextItem();
				}
			}
		}

		// Token: 0x04007FFB RID: 32763
		[Header("Content Rect. 각 아이템은 이 Rect의 사이즈와 같다고 가정")]
		public RectTransform m_rtContentRect;

		// Token: 0x04007FFC RID: 32764
		[Header("이전/다음 아이템 버튼. Nullable")]
		public NKCUIComStateButton m_csbtnPrev;

		// Token: 0x04007FFD RID: 32765
		public NKCUIComStateButton m_csbtnNext;

		// Token: 0x04007FFE RID: 32766
		[Header("Dot Scroll Indicator. Nullable")]
		public NKCUIComDotList m_dotList;

		// Token: 0x04007FFF RID: 32767
		[Header("스크롤 방향")]
		public Vector2 m_vScrollDirection = new Vector2(1f, 0f);

		// Token: 0x04008000 RID: 32768
		[Header("Spacing")]
		public Vector2 Spacing;

		// Token: 0x04008001 RID: 32769
		[Header("다음 칸으로 이동하는 시간")]
		public float m_fMenuMoveTime = 0.4f;

		// Token: 0x04008002 RID: 32770
		[Header("자동으로 다음 칸으로 넘어가는지 여부")]
		public bool m_bAutoMove;

		// Token: 0x04008003 RID: 32771
		[Header("자동으로 다음 칸으로 넘어갈때까지의 시간")]
		public float m_fAutoMoveTime;

		// Token: 0x04008004 RID: 32772
		[Header("메뉴 돌아갈때 사용할 Ease Function")]
		public Ease m_eEase = Ease.OutQuad;

		// Token: 0x04008005 RID: 32773
		[Header("드래그 거리 대비 이동 속도")]
		public float m_fMoveRate = 1f;

		// Token: 0x04008006 RID: 32774
		[Header("끝까지 드래그시 메뉴 몇개분만큼 이동하는지. 스크롤 방향을 변경했는데 드래그시 심하게 튄다면 이 값의 +-를 반대로 할 것")]
		public float m_fDragLimitRate = 0.4f;

		// Token: 0x04008007 RID: 32775
		[Header("얼마 이상 움직였어야 다음 메뉴로 넘어가는가?")]
		public float m_fMoveThreshold = 10f;

		// Token: 0x04008008 RID: 32776
		[Header("배너를 반복하는가?")]
		public bool m_bBannerRotation;

		// Token: 0x04008009 RID: 32777
		[Header("Focus에 따른 색상 변화를 적용하는가?")]
		public Color m_FocusColor = Color.white;

		// Token: 0x0400800A RID: 32778
		public Color m_UnFocusColor = Color.gray;

		// Token: 0x0400800B RID: 32779
		private float m_fOffset;

		// Token: 0x0400800C RID: 32780
		private bool m_bPauseUpdate;

		// Token: 0x0400800D RID: 32781
		private float m_fAutomoveDeltaTime;

		// Token: 0x0400800E RID: 32782
		private int _totalCount;

		// Token: 0x0400800F RID: 32783
		private int m_currentIndex;

		// Token: 0x04008010 RID: 32784
		private RectTransform currentItem;

		// Token: 0x04008011 RID: 32785
		private RectTransform prevItem;

		// Token: 0x04008012 RID: 32786
		private RectTransform nextItem;

		// Token: 0x02001A0E RID: 6670
		// (Invoke) Token: 0x0600BAE7 RID: 47847
		public delegate RectTransform OnGetObject();

		// Token: 0x02001A0F RID: 6671
		// (Invoke) Token: 0x0600BAEB RID: 47851
		public delegate void OnReturnObject(RectTransform rect);

		// Token: 0x02001A10 RID: 6672
		// (Invoke) Token: 0x0600BAEF RID: 47855
		public delegate void OnProvideData(RectTransform rect, int idx);

		// Token: 0x02001A11 RID: 6673
		// (Invoke) Token: 0x0600BAF3 RID: 47859
		public delegate void OnFocus(RectTransform rect, bool bIsFocus);

		// Token: 0x02001A12 RID: 6674
		// (Invoke) Token: 0x0600BAF7 RID: 47863
		public delegate void OnFocusColor(RectTransform rect, Color setColor);

		// Token: 0x02001A13 RID: 6675
		// (Invoke) Token: 0x0600BAFB RID: 47867
		public delegate void OnIndexChangeListener(int index);
	}
}
