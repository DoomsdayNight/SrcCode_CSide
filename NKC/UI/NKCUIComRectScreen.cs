using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000943 RID: 2371
	public class NKCUIComRectScreen : MonoBehaviour
	{
		// Token: 0x06005EAB RID: 24235 RVA: 0x001D623B File Offset: 0x001D443B
		private bool CheckExpandFlag(NKCUIComRectScreen.ScreenExpand flag)
		{
			return (flag & this.m_flagScreenExpand) == flag;
		}

		// Token: 0x06005EAC RID: 24236 RVA: 0x001D6248 File Offset: 0x001D4448
		public void SetScreen(Renderer targetRenderer, bool bAttach, NKCUIComRectScreen.ScreenExpand expandFlag = NKCUIComRectScreen.ScreenExpand.None)
		{
			if (targetRenderer == null)
			{
				this.m_rtCenter.pivot = Vector2.zero;
				this.m_rtCenter.localPosition = Vector3.zero;
				this.m_rtCenter.SetSize(Vector2.zero);
				this.RepositionRectByCenter();
				return;
			}
			this.m_bAttach = bAttach;
			this.m_flagScreenExpand = expandFlag;
			Bounds bounds = targetRenderer.bounds;
			Vector3 position = bounds.center - bounds.extents;
			Vector3 position2 = bounds.center + bounds.extents;
			Camera camera = NKCCamera.GetCamera();
			Camera subUICamera = NKCCamera.GetSubUICamera();
			Vector3 vector = subUICamera.ScreenToWorldPoint(camera.WorldToScreenPoint(position));
			vector.z = 0f;
			Vector3 a = subUICamera.ScreenToWorldPoint(camera.WorldToScreenPoint(position2));
			a.z = 0f;
			vector.x -= (float)this.m_offsetCenter.left;
			vector.y -= (float)this.m_offsetCenter.bottom;
			a.x += (float)this.m_offsetCenter.right;
			a.y += (float)this.m_offsetCenter.top;
			if (this.CheckExpandFlag(NKCUIComRectScreen.ScreenExpand.Left))
			{
				vector.x = (float)(-(float)Screen.width) * 0.5f;
			}
			if (this.CheckExpandFlag(NKCUIComRectScreen.ScreenExpand.Right))
			{
				a.x = (float)Screen.width * 0.5f;
			}
			if (this.CheckExpandFlag(NKCUIComRectScreen.ScreenExpand.Up))
			{
				vector.y = (float)Screen.height * 0.5f;
			}
			if (this.CheckExpandFlag(NKCUIComRectScreen.ScreenExpand.Down))
			{
				a.y = (float)(-(float)Screen.height) * 0.5f;
			}
			this.m_rtCenter.pivot = Vector2.zero;
			this.m_rtCenter.position = vector;
			this.m_rtCenter.SetSize(a - vector);
			this.RepositionRectByCenter();
			this.m_RendererTarget = (bAttach ? targetRenderer : null);
			this.m_rtTarget = null;
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x001D6430 File Offset: 0x001D4630
		public void SetScreen(RectTransform target, bool bIsFromMidCanvas, bool bAttach)
		{
			if (target == null)
			{
				this.m_rtCenter.pivot = Vector2.zero;
				this.m_rtCenter.localPosition = Vector3.zero;
				this.m_rtCenter.SetSize(Vector2.zero);
				this.RepositionRectByCenter();
				return;
			}
			this.m_bAttach = bAttach;
			this.m_rtCenter.pivot = target.pivot;
			Vector2 a;
			if (bIsFromMidCanvas)
			{
				Camera camera = NKCCamera.GetCamera();
				a = NKCCamera.GetSubUICamera().ScreenToWorldPoint(camera.WorldToScreenPoint(target.position));
			}
			else
			{
				a = target.position;
			}
			Vector2 size = target.GetSize();
			Vector2 a2 = a + size * (Vector2.one - target.pivot) * target.lossyScale;
			Vector2 b = a - size * target.pivot * target.lossyScale;
			Vector2 v = (a2 + b) * 0.5f;
			Vector2 vector = a2 - b;
			vector.x = Mathf.Abs(vector.x);
			vector.y = Mathf.Abs(vector.y);
			this.m_rtCenter.pivot = Vector2.one * 0.5f;
			this.m_rtCenter.position = v;
			vector.x += (float)(this.m_offsetCenter.left + this.m_offsetCenter.right);
			vector.y += (float)(this.m_offsetCenter.bottom + this.m_offsetCenter.top);
			this.m_rtCenter.SetSize(vector);
			Vector3 vector2 = new Vector3((target.lossyScale.x != 0f) ? target.lossyScale.x : 1f, (target.lossyScale.y != 0f) ? target.lossyScale.y : 1f, (target.lossyScale.z != 0f) ? target.lossyScale.z : 1f);
			this.m_rtCenter.localScale = new Vector3(1f / vector2.x, 1f / vector2.y, 1f / vector2.z);
			this.RepositionRectByCenter();
			this.m_bIsFromMidCanvas = bIsFromMidCanvas;
			this.m_rtTarget = (bAttach ? target : null);
			this.m_RendererTarget = null;
		}

		// Token: 0x06005EAE RID: 24238 RVA: 0x001D66A9 File Offset: 0x001D48A9
		private void ResetTouchSteal()
		{
			this.m_imgCenter.raycastTarget = false;
			this.m_etCenter.triggers.Clear();
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x001D66C8 File Offset: 0x001D48C8
		public void SetTouchSteal(UnityAction<BaseEventData> onTouch)
		{
			this.m_imgCenter.raycastTarget = true;
			if (onTouch != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener(onTouch);
				this.m_etCenter.triggers.Clear();
				this.m_etCenter.triggers.Add(entry);
			}
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x001D6720 File Offset: 0x001D4920
		public void RepositionRectByCenter()
		{
			Vector2 size = base.GetComponent<RectTransform>().GetSize();
			Vector2 vector = this.m_rtCenter.anchorMin * size + this.m_rtCenter.anchoredPosition;
			float num = vector.x - this.m_rtCenter.pivot.x * this.m_rtCenter.GetWidth();
			float num2 = vector.x + (1f - this.m_rtCenter.pivot.x) * this.m_rtCenter.GetWidth();
			float newSize = vector.y - this.m_rtCenter.pivot.y * this.m_rtCenter.GetHeight();
			float num3 = vector.y + (1f - this.m_rtCenter.pivot.y) * this.m_rtCenter.GetHeight();
			this.m_rtLeft.SetWidth(num);
			this.m_rtRight.SetWidth(size.x - num2);
			this.m_rtTop.SetHeight(size.y - num3);
			this.m_rtTop.offsetMin = new Vector2(num, this.m_rtTop.offsetMin.y);
			this.m_rtTop.offsetMax = new Vector2(num2 - size.x, this.m_rtTop.offsetMax.y);
			this.m_rtBottom.SetHeight(newSize);
			this.m_rtBottom.offsetMin = new Vector2(num, this.m_rtBottom.offsetMin.y);
			this.m_rtBottom.offsetMax = new Vector2(num2 - size.x, this.m_rtBottom.offsetMax.y);
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x001D68C7 File Offset: 0x001D4AC7
		public void CleanUp()
		{
			this.m_rtTarget = null;
			this.m_RendererTarget = null;
			this.ResetTouchSteal();
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x001D68E0 File Offset: 0x001D4AE0
		private void Update()
		{
			if (this.m_bAttach)
			{
				if (this.m_rtTarget != null)
				{
					this.SetScreen(this.m_rtTarget, this.m_bIsFromMidCanvas, true);
					return;
				}
				if (this.m_RendererTarget != null)
				{
					this.SetScreen(this.m_RendererTarget, true, this.m_flagScreenExpand);
				}
			}
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x001D6938 File Offset: 0x001D4B38
		public void SetAlpha(float a)
		{
			Color color = new Color(0f, 0f, 0f, a);
			NKCUtil.SetImageColor(this.m_imgCenter, color);
			NKCUtil.SetImageColor(this.m_rtBottom.GetComponent<Image>(), color);
			NKCUtil.SetImageColor(this.m_rtLeft.GetComponent<Image>(), color);
			NKCUtil.SetImageColor(this.m_rtRight.GetComponent<Image>(), color);
			NKCUtil.SetImageColor(this.m_rtTop.GetComponent<Image>(), color);
		}

		// Token: 0x04004AD0 RID: 19152
		private NKCUIComRectScreen.ScreenExpand m_flagScreenExpand;

		// Token: 0x04004AD1 RID: 19153
		public RectTransform m_rtCenter;

		// Token: 0x04004AD2 RID: 19154
		public Image m_imgCenter;

		// Token: 0x04004AD3 RID: 19155
		public EventTrigger m_etCenter;

		// Token: 0x04004AD4 RID: 19156
		public RectTransform m_rtLeft;

		// Token: 0x04004AD5 RID: 19157
		public RectTransform m_rtRight;

		// Token: 0x04004AD6 RID: 19158
		public RectTransform m_rtTop;

		// Token: 0x04004AD7 RID: 19159
		public RectTransform m_rtBottom;

		// Token: 0x04004AD8 RID: 19160
		public RectOffset m_offsetCenter;

		// Token: 0x04004AD9 RID: 19161
		private RectTransform m_rtTarget;

		// Token: 0x04004ADA RID: 19162
		private Renderer m_RendererTarget;

		// Token: 0x04004ADB RID: 19163
		private bool m_bIsFromMidCanvas;

		// Token: 0x04004ADC RID: 19164
		private bool m_bAttach;

		// Token: 0x020015CD RID: 5581
		[Flags]
		public enum ScreenExpand
		{
			// Token: 0x0400A286 RID: 41606
			None = 0,
			// Token: 0x0400A287 RID: 41607
			Left = 1,
			// Token: 0x0400A288 RID: 41608
			Right = 2,
			// Token: 0x0400A289 RID: 41609
			Up = 4,
			// Token: 0x0400A28A RID: 41610
			Down = 8
		}
	}
}
