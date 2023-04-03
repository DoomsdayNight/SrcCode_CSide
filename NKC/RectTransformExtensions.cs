using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006F3 RID: 1779
	public static class RectTransformExtensions
	{
		// Token: 0x06003F23 RID: 16163 RVA: 0x001480B3 File Offset: 0x001462B3
		public static void SetDefaultScale(this RectTransform trans)
		{
			trans.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x001480CF File Offset: 0x001462CF
		public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
		{
			trans.pivot = aVec;
			trans.anchorMin = aVec;
			trans.anchorMax = aVec;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x001480E8 File Offset: 0x001462E8
		public static Vector2 GetSize(this RectTransform trans)
		{
			return trans.rect.size;
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00148104 File Offset: 0x00146304
		public static float GetWidth(this RectTransform trans)
		{
			return trans.rect.width;
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00148120 File Offset: 0x00146320
		public static float GetHeight(this RectTransform trans)
		{
			return trans.rect.height;
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0014813B File Offset: 0x0014633B
		public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00148160 File Offset: 0x00146360
		public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x + trans.pivot.x * trans.rect.width, newPos.y + trans.pivot.y * trans.rect.height, trans.localPosition.z);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x001481C8 File Offset: 0x001463C8
		public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x + trans.pivot.x * trans.rect.width, newPos.y - (1f - trans.pivot.y) * trans.rect.height, trans.localPosition.z);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00148234 File Offset: 0x00146434
		public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x - (1f - trans.pivot.x) * trans.rect.width, newPos.y + trans.pivot.y * trans.rect.height, trans.localPosition.z);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x001482A0 File Offset: 0x001464A0
		public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x - (1f - trans.pivot.x) * trans.rect.width, newPos.y - (1f - trans.pivot.y) * trans.rect.height, trans.localPosition.z);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00148314 File Offset: 0x00146514
		public static void SetSize(this RectTransform trans, Vector2 newSize)
		{
			Vector2 size = trans.rect.size;
			Vector2 vector = newSize - size;
			trans.offsetMin -= new Vector2(vector.x * trans.pivot.x, vector.y * trans.pivot.y);
			trans.offsetMax += new Vector2(vector.x * (1f - trans.pivot.x), vector.y * (1f - trans.pivot.y));
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x001483B8 File Offset: 0x001465B8
		public static void SetWidth(this RectTransform trans, float newSize)
		{
			trans.SetSize(new Vector2(newSize, trans.rect.size.y));
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x001483E4 File Offset: 0x001465E4
		public static void SetHeight(this RectTransform trans, float newSize)
		{
			trans.SetSize(new Vector2(trans.rect.size.x, newSize));
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00148410 File Offset: 0x00146610
		public static Vector3 GetCenterWorldPos(this RectTransform rt)
		{
			Vector3[] array = new Vector3[4];
			rt.GetWorldCorners(array);
			return (array[0] + array[2]) / 2f;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00148448 File Offset: 0x00146648
		public static Rect GetWorldRect(this RectTransform rect)
		{
			Vector3[] array = new Vector3[4];
			rect.GetWorldCorners(array);
			return Rect.MinMaxRect(Mathf.Min(new float[]
			{
				array[0].x,
				array[1].x,
				array[2].x,
				array[3].x
			}), Mathf.Min(new float[]
			{
				array[0].y,
				array[1].y,
				array[2].y,
				array[3].y
			}), Mathf.Max(new float[]
			{
				array[0].x,
				array[1].x,
				array[2].x,
				array[3].x
			}), Mathf.Max(new float[]
			{
				array[0].y,
				array[1].y,
				array[2].y,
				array[3].y
			}));
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x00148584 File Offset: 0x00146784
		public static Vector3 ClampLocalPos(this RectTransform rectTransfom, Vector3 localPos)
		{
			Vector3 result;
			result.x = Mathf.Clamp(localPos.x, rectTransfom.rect.xMin, rectTransfom.rect.xMax);
			result.y = Mathf.Clamp(localPos.y, rectTransfom.rect.yMin, rectTransfom.rect.yMax);
			result.z = localPos.z;
			return result;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x001485FC File Offset: 0x001467FC
		public static void FitRectTransformToRect(this RectTransform rect, Rect TargetSizeRect, RectTransformExtensions.FitMode fitMode = RectTransformExtensions.FitMode.FitOutside, RectTransformExtensions.ScaleMode scaleMode = RectTransformExtensions.ScaleMode.Scale)
		{
			Vector2 vector = default(Vector2);
			switch (fitMode)
			{
			case RectTransformExtensions.FitMode.FitToRect:
				vector.x = TargetSizeRect.width;
				vector.y = TargetSizeRect.height;
				goto IL_180;
			case RectTransformExtensions.FitMode.FitInside:
			{
				float num = rect.GetWidth() / rect.GetHeight();
				if (rect.GetWidth() / TargetSizeRect.width > rect.GetHeight() / TargetSizeRect.height)
				{
					vector.x = TargetSizeRect.width;
					vector.y = vector.x / num;
					goto IL_180;
				}
				vector.y = TargetSizeRect.height;
				vector.x = vector.y * num;
				goto IL_180;
			}
			case RectTransformExtensions.FitMode.FitToWidth:
			{
				float num2 = rect.GetWidth() / rect.GetHeight();
				vector.x = TargetSizeRect.width;
				vector.y = vector.x / num2;
				goto IL_180;
			}
			case RectTransformExtensions.FitMode.FitToHeight:
			{
				float num3 = rect.GetWidth() / rect.GetHeight();
				vector.y = TargetSizeRect.height;
				vector.x = vector.y * num3;
				goto IL_180;
			}
			}
			float num4 = rect.GetWidth() / rect.GetHeight();
			if (rect.GetWidth() / TargetSizeRect.width > rect.GetHeight() / TargetSizeRect.height)
			{
				vector.y = TargetSizeRect.height;
				vector.x = vector.y * num4;
			}
			else
			{
				vector.x = TargetSizeRect.width;
				vector.y = vector.x / num4;
			}
			IL_180:
			if (scaleMode == RectTransformExtensions.ScaleMode.Scale)
			{
				rect.localScale = new Vector3
				{
					x = vector.x / rect.GetWidth(),
					y = vector.y / rect.GetHeight(),
					z = rect.localScale.z
				};
				return;
			}
			if (scaleMode != RectTransformExtensions.ScaleMode.RectSize)
			{
				return;
			}
			rect.SetSize(vector);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x001487E4 File Offset: 0x001469E4
		public static Vector3 ProjectPointToPlane(this RectTransform plane, Vector3 targetPos)
		{
			Camera subUICamera = NKCCamera.GetSubUICamera();
			Vector2 screenPoint = subUICamera.WorldToScreenPoint(targetPos);
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(plane, screenPoint, subUICamera, out v);
			return v;
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x00148818 File Offset: 0x00146A18
		public static Vector3 ProjectPointToPlaneWorldPos(this RectTransform plane, Vector3 targetPos)
		{
			Vector3 position = plane.ProjectPointToPlane(targetPos);
			return plane.TransformPoint(position);
		}

		// Token: 0x020013CD RID: 5069
		public enum FitMode
		{
			// Token: 0x04009C2F RID: 39983
			FitToRect,
			// Token: 0x04009C30 RID: 39984
			FitOutside,
			// Token: 0x04009C31 RID: 39985
			FitInside,
			// Token: 0x04009C32 RID: 39986
			FitToWidth,
			// Token: 0x04009C33 RID: 39987
			FitToHeight
		}

		// Token: 0x020013CE RID: 5070
		public enum ScaleMode
		{
			// Token: 0x04009C35 RID: 39989
			Scale,
			// Token: 0x04009C36 RID: 39990
			RectSize
		}
	}
}
