using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200032D RID: 813
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/Tooltip/Tooltip")]
	public class ToolTip : MonoBehaviour
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000469F1 File Offset: 0x00044BF1
		public Camera GuiCamera
		{
			get
			{
				if (!this._guiCamera)
				{
					this._guiCamera = Camera.main;
				}
				return this._guiCamera;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00046A11 File Offset: 0x00044C11
		public static ToolTip Instance
		{
			get
			{
				if (ToolTip.instance == null)
				{
					ToolTip.instance = Object.FindObjectOfType<ToolTip>();
				}
				return ToolTip.instance;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00046A2F File Offset: 0x00044C2F
		private void Reset()
		{
			this.canvas = base.GetComponentInParent<Canvas>();
			this.canvas = this.canvas.rootCanvas;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00046A50 File Offset: 0x00044C50
		public void Awake()
		{
			ToolTip.instance = this;
			if (!this.canvas)
			{
				this.canvas = base.GetComponentInParent<Canvas>();
				this.canvas = this.canvas.rootCanvas;
			}
			this._guiCamera = this.canvas.worldCamera;
			this.guiMode = this.canvas.renderMode;
			this._rectTransform = base.GetComponent<RectTransform>();
			this.canvasRectTransform = this.canvas.GetComponent<RectTransform>();
			this._layoutGroup = base.GetComponentInChildren<LayoutGroup>();
			this._text = base.GetComponentInChildren<Text>();
			this._inside = false;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00046AF7 File Offset: 0x00044CF7
		public void SetTooltip(string ttext)
		{
			this.SetTooltip(ttext, base.transform.position, false);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00046B0C File Offset: 0x00044D0C
		public void SetTooltip(string ttext, Vector3 basePos, bool refreshCanvasesBeforeGetSize = false)
		{
			this.baseTooltipPos = basePos;
			if (this._text)
			{
				this._text.text = ttext;
			}
			else
			{
				Debug.LogWarning("[ToolTip] Couldn't set tooltip text, tooltip has no child Text component");
			}
			this.ContextualTooltipUpdate(refreshCanvasesBeforeGetSize);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00046B41 File Offset: 0x00044D41
		public void HideTooltip()
		{
			base.gameObject.SetActive(false);
			this._inside = false;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00046B56 File Offset: 0x00044D56
		private void Update()
		{
			if (this._inside)
			{
				this.ContextualTooltipUpdate(false);
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00046B67 File Offset: 0x00044D67
		public void RefreshTooltipSize()
		{
			if (this.tooltipTriggersCanForceCanvasUpdate)
			{
				Canvas.ForceUpdateCanvases();
				if (this._layoutGroup)
				{
					this._layoutGroup.enabled = false;
					this._layoutGroup.enabled = true;
				}
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00046B9C File Offset: 0x00044D9C
		public void ContextualTooltipUpdate(bool refreshCanvasesBeforeGettingSize = false)
		{
			RenderMode renderMode = this.guiMode;
			if (renderMode != RenderMode.ScreenSpaceOverlay)
			{
				if (renderMode == RenderMode.ScreenSpaceCamera)
				{
					this.OnScreenSpaceCamera(refreshCanvasesBeforeGettingSize);
					return;
				}
			}
			else
			{
				this.OnScreenSpaceOverlay(refreshCanvasesBeforeGettingSize);
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00046BC8 File Offset: 0x00044DC8
		public void OnScreenSpaceCamera(bool refreshCanvasesBeforeGettingSize = false)
		{
			this.shiftingVector.x = this.xShift;
			this.shiftingVector.y = this.YShift;
			this.baseTooltipPos.z = this.canvas.planeDistance;
			this.newTTPos = this.GuiCamera.ScreenToViewportPoint(this.baseTooltipPos - this.shiftingVector);
			this.adjustedNewTTPos = this.GuiCamera.ViewportToWorldPoint(this.newTTPos);
			base.gameObject.SetActive(true);
			if (refreshCanvasesBeforeGettingSize)
			{
				this.RefreshTooltipSize();
			}
			this.width = base.transform.lossyScale.x * this._rectTransform.sizeDelta[0];
			this.height = base.transform.lossyScale.y * this._rectTransform.sizeDelta[1];
			RectTransformUtility.ScreenPointToWorldPointInRectangle(this.canvasRectTransform, Vector2.zero, this.GuiCamera, out this.screenLowerLeft);
			RectTransformUtility.ScreenPointToWorldPointInRectangle(this.canvasRectTransform, new Vector2((float)Screen.width, (float)Screen.height), this.GuiCamera, out this.screenUpperRight);
			this.borderTest = this.adjustedNewTTPos.x + this.width / 2f;
			if (this.borderTest > this.screenUpperRight.x)
			{
				this.shifterForBorders.x = this.borderTest - this.screenUpperRight.x;
				this.adjustedNewTTPos.x = this.adjustedNewTTPos.x - this.shifterForBorders.x;
			}
			this.borderTest = this.adjustedNewTTPos.x - this.width / 2f;
			if (this.borderTest < this.screenLowerLeft.x)
			{
				this.shifterForBorders.x = this.screenLowerLeft.x - this.borderTest;
				this.adjustedNewTTPos.x = this.adjustedNewTTPos.x + this.shifterForBorders.x;
			}
			this.borderTest = this.adjustedNewTTPos.y - this.height / 2f;
			if (this.borderTest < this.screenLowerLeft.y)
			{
				this.shifterForBorders.y = this.screenLowerLeft.y - this.borderTest;
				this.adjustedNewTTPos.y = this.adjustedNewTTPos.y + this.shifterForBorders.y;
			}
			this.borderTest = this.adjustedNewTTPos.y + this.height / 2f;
			if (this.borderTest > this.screenUpperRight.y)
			{
				this.shifterForBorders.y = this.borderTest - this.screenUpperRight.y;
				this.adjustedNewTTPos.y = this.adjustedNewTTPos.y - this.shifterForBorders.y;
			}
			this.adjustedNewTTPos = base.transform.rotation * this.adjustedNewTTPos;
			base.transform.position = this.adjustedNewTTPos;
			this.adjustedTTLocalPos = base.transform.localPosition;
			this.adjustedTTLocalPos.z = 0f;
			base.transform.localPosition = this.adjustedTTLocalPos;
			this._inside = true;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00046F00 File Offset: 0x00045100
		public void OnScreenSpaceOverlay(bool refreshCanvasesBeforeGettingSize = false)
		{
			this.shiftingVector.x = this.xShift;
			this.shiftingVector.y = this.YShift;
			this.newTTPos = (this.baseTooltipPos - this.shiftingVector) / this.canvas.scaleFactor;
			this.adjustedNewTTPos = this.newTTPos;
			base.gameObject.SetActive(true);
			if (refreshCanvasesBeforeGettingSize)
			{
				this.RefreshTooltipSize();
			}
			this.width = this._rectTransform.sizeDelta[0];
			this.height = this._rectTransform.sizeDelta[1];
			this.screenLowerLeft = Vector3.zero;
			this.screenUpperRight = this.canvasRectTransform.sizeDelta;
			this.borderTest = this.newTTPos.x + this.width / 2f;
			if (this.borderTest > this.screenUpperRight.x)
			{
				this.shifterForBorders.x = this.borderTest - this.screenUpperRight.x;
				this.adjustedNewTTPos.x = this.adjustedNewTTPos.x - this.shifterForBorders.x;
			}
			this.borderTest = this.adjustedNewTTPos.x - this.width / 2f;
			if (this.borderTest < this.screenLowerLeft.x)
			{
				this.shifterForBorders.x = this.screenLowerLeft.x - this.borderTest;
				this.adjustedNewTTPos.x = this.adjustedNewTTPos.x + this.shifterForBorders.x;
			}
			this.borderTest = this.adjustedNewTTPos.y - this.height / 2f;
			if (this.borderTest < this.screenLowerLeft.y)
			{
				this.shifterForBorders.y = this.screenLowerLeft.y - this.borderTest;
				this.adjustedNewTTPos.y = this.adjustedNewTTPos.y + this.shifterForBorders.y;
			}
			this.borderTest = this.adjustedNewTTPos.y + this.height / 2f;
			if (this.borderTest > this.screenUpperRight.y)
			{
				this.shifterForBorders.y = this.borderTest - this.screenUpperRight.y;
				this.adjustedNewTTPos.y = this.adjustedNewTTPos.y - this.shifterForBorders.y;
			}
			this.adjustedNewTTPos *= this.canvas.scaleFactor;
			base.transform.position = this.adjustedNewTTPos;
			this._inside = true;
		}

		// Token: 0x04000D2E RID: 3374
		private Text _text;

		// Token: 0x04000D2F RID: 3375
		private RectTransform _rectTransform;

		// Token: 0x04000D30 RID: 3376
		private RectTransform canvasRectTransform;

		// Token: 0x04000D31 RID: 3377
		[Tooltip("The canvas used by the tooltip as positioning and scaling reference. Should usually be the root Canvas of the hierarchy this component is in")]
		public Canvas canvas;

		// Token: 0x04000D32 RID: 3378
		[Tooltip("Sets if tooltip triggers will run ForceUpdateCanvases and refresh the tooltip's layout group (if any) when hovered, in order to prevent momentousness misplacement sometimes caused by ContentSizeFitters")]
		public bool tooltipTriggersCanForceCanvasUpdate;

		// Token: 0x04000D33 RID: 3379
		private LayoutGroup _layoutGroup;

		// Token: 0x04000D34 RID: 3380
		private bool _inside;

		// Token: 0x04000D35 RID: 3381
		private float width;

		// Token: 0x04000D36 RID: 3382
		private float height;

		// Token: 0x04000D37 RID: 3383
		public float YShift;

		// Token: 0x04000D38 RID: 3384
		public float xShift;

		// Token: 0x04000D39 RID: 3385
		[HideInInspector]
		public RenderMode guiMode;

		// Token: 0x04000D3A RID: 3386
		private Camera _guiCamera;

		// Token: 0x04000D3B RID: 3387
		private Vector3 screenLowerLeft;

		// Token: 0x04000D3C RID: 3388
		private Vector3 screenUpperRight;

		// Token: 0x04000D3D RID: 3389
		private Vector3 shiftingVector;

		// Token: 0x04000D3E RID: 3390
		private Vector3 baseTooltipPos;

		// Token: 0x04000D3F RID: 3391
		private Vector3 newTTPos;

		// Token: 0x04000D40 RID: 3392
		private Vector3 adjustedNewTTPos;

		// Token: 0x04000D41 RID: 3393
		private Vector3 adjustedTTLocalPos;

		// Token: 0x04000D42 RID: 3394
		private Vector3 shifterForBorders;

		// Token: 0x04000D43 RID: 3395
		private float borderTest;

		// Token: 0x04000D44 RID: 3396
		private static ToolTip instance;
	}
}
