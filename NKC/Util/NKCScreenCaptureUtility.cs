using System;
using System.IO;
using UnityEngine;

namespace NKC.Util
{
	// Token: 0x0200080F RID: 2063
	public static class NKCScreenCaptureUtility
	{
		// Token: 0x060051C5 RID: 20933 RVA: 0x0018CF2C File Offset: 0x0018B12C
		public static bool CaptureScreen()
		{
			if (!Directory.Exists("ScreenShot/"))
			{
				Directory.CreateDirectory("ScreenShot/");
			}
			string text = NKCScreenCaptureUtility.MakeCaptureFileName();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			ScreenCapture.CaptureScreenshot(text);
			Debug.Log("Screencapture : " + text);
			return true;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0018CF78 File Offset: 0x0018B178
		public static bool CaptureCamera(Camera camera, string path, int width, int height)
		{
			try
			{
				RenderTexture renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
				RenderTexture targetTexture = camera.targetTexture;
				camera.targetTexture = renderTexture;
				camera.Render();
				camera.targetTexture = targetTexture;
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = renderTexture;
				Texture2D texture2D = new Texture2D(width, height);
				texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
				texture2D.Apply();
				RenderTexture.active = active;
				byte[] bytes = texture2D.EncodeToPNG();
				File.WriteAllBytes(path, bytes);
			}
			catch (Exception ex)
			{
				Debug.LogError("CaptureCamera Failed : Exception " + ex.Message + "\n" + ex.StackTrace);
				return false;
			}
			return true;
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0018D030 File Offset: 0x0018B230
		private static string MakeCaptureFileName()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo("ScreenShot/");
			if (directoryInfo == null || !directoryInfo.Exists)
			{
				return null;
			}
			string str = directoryInfo.FullName + "CounterSide-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			string text = str + ".png";
			if (!File.Exists(text))
			{
				return text;
			}
			int num = 60;
			for (int i = 1; i < num; i++)
			{
				text = str + string.Format(" ({0})", i) + ".png";
				if (!File.Exists(text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0018D0CC File Offset: 0x0018B2CC
		public static bool CaptureScreenWithThumbnail(string capturePath, string thumbnailPath)
		{
			Texture2D texture2D = NKCScreenCaptureUtility.CaptureScreenTexture();
			if (texture2D == null)
			{
				return false;
			}
			byte[] bytes = texture2D.EncodeToPNG();
			File.WriteAllBytes(capturePath, bytes);
			Debug.Log("Screenshot Saved in : " + capturePath);
			if (!string.IsNullOrEmpty(thumbnailPath))
			{
				byte[] bytes2 = NKCScreenCaptureUtility.MakeThumbnailTexture(texture2D, 120).EncodeToPNG();
				File.WriteAllBytes(thumbnailPath, bytes2);
				Debug.Log("Thumbnail Saved in : " + thumbnailPath);
			}
			return true;
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0018D136 File Offset: 0x0018B336
		private static Texture2D CaptureScreenTexture()
		{
			return ScreenCapture.CaptureScreenshotAsTexture();
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0018D140 File Offset: 0x0018B340
		private static Texture2D MakeThumbnailTexture(Texture2D source, int thumbnailSize)
		{
			int num;
			int num2;
			int num3;
			int num4;
			if (source.width < source.height)
			{
				num = 0;
				num2 = source.width;
				num3 = (source.height - source.width) / 2;
				num4 = (source.height + source.width) / 2;
			}
			else if (source.width > source.height)
			{
				num = (source.width - source.height) / 2;
				num2 = (source.width + source.height) / 2;
				num3 = 0;
				num4 = source.height;
			}
			else
			{
				num = 0;
				num2 = source.width;
				num3 = 0;
				num4 = source.height;
			}
			TextureFormat textureFormat = TextureFormat.RGB24;
			Texture2D texture2D = new Texture2D(thumbnailSize, thumbnailSize, textureFormat, false);
			float num5 = (float)(num2 - num);
			float num6 = (float)num / (float)source.width;
			float num7 = num5 / (float)(thumbnailSize * source.width);
			float num8 = (float)(num4 - num3);
			float num9 = (float)num3 / (float)source.height;
			float num10 = num8 / (float)(thumbnailSize * source.height);
			Color[] pixels = texture2D.GetPixels();
			for (int i = 0; i < pixels.Length; i++)
			{
				float u = num6 + num7 * (float)(i % thumbnailSize);
				float v = num9 + num10 * (float)(i / thumbnailSize);
				pixels[i] = source.GetPixelBilinear(u, v);
			}
			texture2D.SetPixels(pixels, 0);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x0400421A RID: 16922
		private const string SCREENSHOT_DIR = "ScreenShot/";

		// Token: 0x0400421B RID: 16923
		private const string FILEPATH_PREFIX = "CounterSide-";

		// Token: 0x0400421C RID: 16924
		private const string FILENAME_DATE_FORMAT = "yyyy-MM-dd HH-mm-ss";

		// Token: 0x0400421D RID: 16925
		private const string FILE_EXTENSION = ".png";
	}
}
