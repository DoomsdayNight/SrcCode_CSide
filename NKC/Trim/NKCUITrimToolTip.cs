using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AB4 RID: 2740
	public class NKCUITrimToolTip : NKCUIBase
	{
		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x060079E8 RID: 31208 RVA: 0x002897E4 File Offset: 0x002879E4
		public static NKCUITrimToolTip Instance
		{
			get
			{
				if (NKCUITrimToolTip.m_Instance == null)
				{
					NKCUITrimToolTip.m_Instance = NKCUIManager.OpenNewInstance<NKCUITrimToolTip>("AB_UI_TRIM", "AB_UI_TRIM_TOOLTIP", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUITrimToolTip.CleanupInstance)).GetInstance<NKCUITrimToolTip>();
					NKCUITrimToolTip.m_Instance.Init();
				}
				return NKCUITrimToolTip.m_Instance;
			}
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x00289833 File Offset: 0x00287A33
		private static void CleanupInstance()
		{
			NKCUITrimToolTip.m_Instance = null;
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x060079EA RID: 31210 RVA: 0x0028983B File Offset: 0x00287A3B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUITrimToolTip.m_Instance != null && NKCUITrimToolTip.m_Instance.IsOpen;
			}
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x00289856 File Offset: 0x00287A56
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x060079EC RID: 31212 RVA: 0x00289864 File Offset: 0x00287A64
		public override string MenuName
		{
			get
			{
				return "Trim ToolTip";
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x060079ED RID: 31213 RVA: 0x0028986B File Offset: 0x00287A6B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x0028986E File Offset: 0x00287A6E
		private void Init()
		{
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x00289870 File Offset: 0x00287A70
		private void Update()
		{
			if (!Input.anyKey)
			{
				base.Close();
			}
		}

		// Token: 0x060079F0 RID: 31216 RVA: 0x0028987F File Offset: 0x00287A7F
		public void Open(string message, Vector2? touchPos)
		{
			if (base.IsOpen)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbText, message);
			base.UIOpened(true);
			this.SetPosition(touchPos);
		}

		// Token: 0x060079F1 RID: 31217 RVA: 0x002898A4 File Offset: 0x00287AA4
		private void SetPosition(Vector2? touchPos)
		{
			NKCUITrimToolTip.PivotType pivotType = this.VectorToPivotType(touchPos);
			Vector3 zero = Vector3.zero;
			if (touchPos != null)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_rectTransform, touchPos.Value, NKCCamera.GetSubUICamera(), out vector);
				zero.x = vector.x;
				zero.y = vector.y;
				zero.z = 0f;
			}
			Vector3 localPosition;
			switch (pivotType)
			{
			default:
				this.m_rtPanel.pivot = new Vector2(0.5f, 0.5f);
				localPosition = Vector3.zero;
				NKCUtil.SetGameobjectActive(this.m_objLeftTail, false);
				NKCUtil.SetGameobjectActive(this.m_objRightTail, false);
				break;
			case NKCUITrimToolTip.PivotType.RightUp:
				this.m_rtPanel.pivot = new Vector2(1f, 1f);
				localPosition = zero + new Vector3(0f, -1f, 0f) * this.m_touchOffset;
				NKCUtil.SetGameobjectActive(this.m_objLeftTail, false);
				NKCUtil.SetGameobjectActive(this.m_objRightTail, true);
				break;
			case NKCUITrimToolTip.PivotType.RightDown:
				this.m_rtPanel.pivot = new Vector2(1f, 0f);
				localPosition = zero + new Vector3(0f, 1f, 0f) * this.m_touchOffset;
				NKCUtil.SetGameobjectActive(this.m_objLeftTail, false);
				NKCUtil.SetGameobjectActive(this.m_objRightTail, true);
				break;
			case NKCUITrimToolTip.PivotType.LeftUp:
				this.m_rtPanel.pivot = new Vector2(0f, 1f);
				localPosition = zero + new Vector3(0f, -1f, 0f) * this.m_touchOffset;
				NKCUtil.SetGameobjectActive(this.m_objLeftTail, true);
				NKCUtil.SetGameobjectActive(this.m_objRightTail, false);
				break;
			case NKCUITrimToolTip.PivotType.LeftDown:
				this.m_rtPanel.pivot = new Vector2(0f, 0f);
				localPosition = zero + new Vector3(0f, 1f, 0f) * this.m_touchOffset;
				NKCUtil.SetGameobjectActive(this.m_objLeftTail, true);
				NKCUtil.SetGameobjectActive(this.m_objRightTail, false);
				break;
			}
			this.m_rtPanel.localPosition = localPosition;
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x00289AD8 File Offset: 0x00287CD8
		private NKCUITrimToolTip.PivotType VectorToPivotType(Vector2? touchPos)
		{
			if (touchPos == null)
			{
				return NKCUITrimToolTip.PivotType.None;
			}
			Vector2 value = touchPos.Value;
			float num = (float)Screen.width * 0.5f;
			float num2 = (float)Screen.height * 0.5f;
			if (value.x > num)
			{
				if (value.y > num2)
				{
					return NKCUITrimToolTip.PivotType.RightUp;
				}
				return NKCUITrimToolTip.PivotType.RightDown;
			}
			else
			{
				if (value.y > num2)
				{
					return NKCUITrimToolTip.PivotType.LeftUp;
				}
				return NKCUITrimToolTip.PivotType.LeftDown;
			}
		}

		// Token: 0x040066B5 RID: 26293
		private const string ASSET_BUNDLE_NAME = "AB_UI_TRIM";

		// Token: 0x040066B6 RID: 26294
		private const string UI_ASSET_NAME = "AB_UI_TRIM_TOOLTIP";

		// Token: 0x040066B7 RID: 26295
		private static NKCUITrimToolTip m_Instance;

		// Token: 0x040066B8 RID: 26296
		public RectTransform m_rectTransform;

		// Token: 0x040066B9 RID: 26297
		public RectTransform m_rtPanel;

		// Token: 0x040066BA RID: 26298
		public Text m_lbText;

		// Token: 0x040066BB RID: 26299
		public GameObject m_objLeftTail;

		// Token: 0x040066BC RID: 26300
		public GameObject m_objRightTail;

		// Token: 0x040066BD RID: 26301
		public float m_touchOffset;

		// Token: 0x02001810 RID: 6160
		private enum PivotType
		{
			// Token: 0x0400A7FC RID: 43004
			None,
			// Token: 0x0400A7FD RID: 43005
			RightUp,
			// Token: 0x0400A7FE RID: 43006
			RightDown,
			// Token: 0x0400A7FF RID: 43007
			LeftUp,
			// Token: 0x0400A800 RID: 43008
			LeftDown
		}
	}
}
