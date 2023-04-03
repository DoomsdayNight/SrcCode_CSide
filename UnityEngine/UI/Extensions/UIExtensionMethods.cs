using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000343 RID: 835
	public static class UIExtensionMethods
	{
		// Token: 0x0600139D RID: 5021 RVA: 0x0004961C File Offset: 0x0004781C
		public static Canvas GetParentCanvas(this RectTransform rt)
		{
			RectTransform rectTransform = rt;
			Canvas canvas = rt.GetComponent<Canvas>();
			int num = 0;
			while (canvas == null || num > 50)
			{
				canvas = rt.GetComponentInParent<Canvas>();
				if (canvas == null)
				{
					rectTransform = rectTransform.parent.GetComponent<RectTransform>();
					num++;
				}
			}
			return canvas;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00049665 File Offset: 0x00047865
		public static Vector2 TransformInputBasedOnCanvasType(this Vector2 input, Canvas canvas)
		{
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				return canvas.GetEventCamera().ScreenToWorldPoint(input);
			}
			return input;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00049688 File Offset: 0x00047888
		public static Vector3 TransformInputBasedOnCanvasType(this Vector2 input, RectTransform rt)
		{
			Canvas parentCanvas = rt.GetParentCanvas();
			if (input == Vector2.zero || parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				return input;
			}
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, input, parentCanvas.GetEventCamera(), out v);
			return parentCanvas.transform.TransformPoint(v);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000496D9 File Offset: 0x000478D9
		public static Camera GetEventCamera(this Canvas input)
		{
			if (!(input.worldCamera == null))
			{
				return input.worldCamera;
			}
			return Camera.main;
		}
	}
}
