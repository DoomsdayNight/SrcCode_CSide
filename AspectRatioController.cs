using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200001B RID: 27
public class AspectRatioController : MonoBehaviour
{
	// Token: 0x060000E5 RID: 229
	[DllImport("kernel32.dll")]
	private static extern uint GetCurrentThreadId();

	// Token: 0x060000E6 RID: 230
	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

	// Token: 0x060000E7 RID: 231
	[DllImport("user32.dll")]
	private static extern bool EnumThreadWindows(uint dwThreadId, AspectRatioController.EnumWindowsProc lpEnumFunc, IntPtr lParam);

	// Token: 0x060000E8 RID: 232
	[DllImport("user32.dll")]
	private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	// Token: 0x060000E9 RID: 233
	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool GetWindowRect(IntPtr hwnd, ref AspectRatioController.RECT lpRect);

	// Token: 0x060000EA RID: 234
	[DllImport("user32.dll")]
	private static extern bool GetClientRect(IntPtr hWnd, ref AspectRatioController.RECT lpRect);

	// Token: 0x060000EB RID: 235
	[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
	private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	// Token: 0x060000EC RID: 236
	[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
	private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	// Token: 0x060000ED RID: 237 RVA: 0x0000442E File Offset: 0x0000262E
	private void Awake()
	{
		AspectRatioController.s_Instance = this;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00004438 File Offset: 0x00002638
	private void Start()
	{
		AspectRatioController.s_Instance = this;
		if (Application.isEditor)
		{
			return;
		}
		Application.wantsToQuit += this.ApplicationWantsToQuit;
		AspectRatioController.EnumThreadWindows(AspectRatioController.GetCurrentThreadId(), new AspectRatioController.EnumWindowsProc(AspectRatioController._windowsProc), IntPtr.Zero);
		this.SetAspectRatio(this.aspectRatioWidth, this.aspectRatioHeight, true);
		this.wasFullscreenLastFrame = Screen.fullScreen;
		this.wndProcDelegate = new AspectRatioController.WndProcDelegate(AspectRatioController._wndProc);
		this.newWndProcPtr = Marshal.GetFunctionPointerForDelegate<AspectRatioController.WndProcDelegate>(this.wndProcDelegate);
		this.oldWndProcPtr = AspectRatioController.SetWindowLong(this.unityHWnd, -4, this.newWndProcPtr);
		this.started = true;
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000044E4 File Offset: 0x000026E4
	[MonoPInvokeCallback(typeof(AspectRatioController.EnumWindowsProc))]
	private static bool _windowsProc(IntPtr hWnd, IntPtr lParam)
	{
		StringBuilder stringBuilder = new StringBuilder("UnityWndClass".Length + 1);
		AspectRatioController.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
		if (stringBuilder.ToString() == "UnityWndClass")
		{
			AspectRatioController.s_Instance.unityHWnd = hWnd;
			return false;
		}
		return true;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00004534 File Offset: 0x00002734
	public void SetAspectRatio(float newAspectWidth, float newAspectHeight, bool apply)
	{
		this.aspectRatioWidth = newAspectWidth;
		this.aspectRatioHeight = newAspectHeight;
		this.aspect = this.aspectRatioWidth / this.aspectRatioHeight;
		if (apply)
		{
			Screen.SetResolution(Screen.width, Mathf.RoundToInt((float)Screen.width / this.aspect), Screen.fullScreen);
		}
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00004586 File Offset: 0x00002786
	[MonoPInvokeCallback(typeof(AspectRatioController.WndProcDelegate))]
	private static IntPtr _wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		return AspectRatioController.s_Instance.wndProc(hWnd, msg, wParam, lParam);
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00004598 File Offset: 0x00002798
	private IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		if (msg == 532U)
		{
			AspectRatioController.RECT rect = (AspectRatioController.RECT)Marshal.PtrToStructure(lParam, typeof(AspectRatioController.RECT));
			AspectRatioController.RECT rect2 = default(AspectRatioController.RECT);
			AspectRatioController.GetWindowRect(this.unityHWnd, ref rect2);
			AspectRatioController.RECT rect3 = default(AspectRatioController.RECT);
			AspectRatioController.GetClientRect(this.unityHWnd, ref rect3);
			int num = rect2.Right - rect2.Left - (rect3.Right - rect3.Left);
			int num2 = rect2.Bottom - rect2.Top - (rect3.Bottom - rect3.Top);
			rect.Right -= num;
			rect.Bottom -= num2;
			int num3 = Mathf.Clamp(rect.Right - rect.Left, this.minWidthPixel, this.maxWidthPixel);
			int num4 = Mathf.Clamp(rect.Bottom - rect.Top, this.minHeightPixel, this.maxHeightPixel);
			switch (wParam.ToInt32())
			{
			case 1:
				rect.Left = rect.Right - num3;
				rect.Bottom = rect.Top + Mathf.RoundToInt((float)num3 / this.aspect);
				break;
			case 2:
				rect.Right = rect.Left + num3;
				rect.Bottom = rect.Top + Mathf.RoundToInt((float)num3 / this.aspect);
				break;
			case 3:
				rect.Top = rect.Bottom - num4;
				rect.Right = rect.Left + Mathf.RoundToInt((float)num4 * this.aspect);
				break;
			case 4:
				rect.Left = rect.Right - num3;
				rect.Top = rect.Bottom - Mathf.RoundToInt((float)num3 / this.aspect);
				break;
			case 5:
				rect.Right = rect.Left + num3;
				rect.Top = rect.Bottom - Mathf.RoundToInt((float)num3 / this.aspect);
				break;
			case 6:
				rect.Bottom = rect.Top + num4;
				rect.Right = rect.Left + Mathf.RoundToInt((float)num4 * this.aspect);
				break;
			case 7:
				rect.Left = rect.Right - num3;
				rect.Bottom = rect.Top + Mathf.RoundToInt((float)num3 / this.aspect);
				break;
			case 8:
				rect.Right = rect.Left + num3;
				rect.Bottom = rect.Top + Mathf.RoundToInt((float)num3 / this.aspect);
				break;
			}
			this.setWidth = rect.Right - rect.Left;
			this.setHeight = rect.Bottom - rect.Top;
			rect.Right += num;
			rect.Bottom += num2;
			AspectRatioController.ResolutionChangedEvent resolutionChangedEvent = this.resolutionChangedEvent;
			if (resolutionChangedEvent != null)
			{
				resolutionChangedEvent.Invoke(this.setWidth, this.setHeight, Screen.fullScreen);
			}
			Marshal.StructureToPtr<AspectRatioController.RECT>(rect, lParam, true);
		}
		return AspectRatioController.CallWindowProc(this.oldWndProcPtr, hWnd, msg, wParam, lParam);
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000048BC File Offset: 0x00002ABC
	private void Update()
	{
		if (!this.allowFullscreen && Screen.fullScreen)
		{
			Screen.fullScreen = false;
		}
		if (Screen.fullScreen && !this.wasFullscreenLastFrame)
		{
			int num;
			int num2;
			if (this.aspect < (float)this.pixelWidthOfCurrentScreen / (float)this.pixelHeightOfCurrentScreen)
			{
				num = this.pixelHeightOfCurrentScreen;
				num2 = Mathf.RoundToInt((float)this.pixelHeightOfCurrentScreen * this.aspect);
			}
			else
			{
				num2 = this.pixelWidthOfCurrentScreen;
				num = Mathf.RoundToInt((float)this.pixelWidthOfCurrentScreen / this.aspect);
			}
			Screen.SetResolution(num2, num, true);
			AspectRatioController.ResolutionChangedEvent resolutionChangedEvent = this.resolutionChangedEvent;
			if (resolutionChangedEvent != null)
			{
				resolutionChangedEvent.Invoke(num2, num, true);
			}
		}
		else if (!Screen.fullScreen && this.wasFullscreenLastFrame)
		{
			Screen.SetResolution(this.setWidth, this.setHeight, false);
			AspectRatioController.ResolutionChangedEvent resolutionChangedEvent2 = this.resolutionChangedEvent;
			if (resolutionChangedEvent2 != null)
			{
				resolutionChangedEvent2.Invoke(this.setWidth, this.setHeight, false);
			}
		}
		else if (!Screen.fullScreen && this.setWidth != -1 && this.setHeight != -1 && (Screen.width != this.setWidth || Screen.height != this.setHeight))
		{
			this.setHeight = Screen.height;
			this.setWidth = Mathf.RoundToInt((float)Screen.height * this.aspect);
			Screen.SetResolution(this.setWidth, this.setHeight, Screen.fullScreen);
			AspectRatioController.ResolutionChangedEvent resolutionChangedEvent3 = this.resolutionChangedEvent;
			if (resolutionChangedEvent3 != null)
			{
				resolutionChangedEvent3.Invoke(this.setWidth, this.setHeight, Screen.fullScreen);
			}
		}
		else if (!Screen.fullScreen)
		{
			this.pixelHeightOfCurrentScreen = Screen.currentResolution.height;
			this.pixelWidthOfCurrentScreen = Screen.currentResolution.width;
		}
		this.wasFullscreenLastFrame = Screen.fullScreen;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00004A77 File Offset: 0x00002C77
	private static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
	{
		if (IntPtr.Size == 4)
		{
			return AspectRatioController.SetWindowLong32(hWnd, nIndex, dwNewLong);
		}
		return AspectRatioController.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00004A92 File Offset: 0x00002C92
	private bool ApplicationWantsToQuit()
	{
		if (!this.started)
		{
			return false;
		}
		if (!this.quitStarted)
		{
			base.StartCoroutine(this.DelayedQuit());
			return false;
		}
		return true;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00004AB6 File Offset: 0x00002CB6
	private IEnumerator DelayedQuit()
	{
		AspectRatioController.SetWindowLong(this.unityHWnd, -4, this.oldWndProcPtr);
		yield return new WaitForEndOfFrame();
		this.quitStarted = true;
		Application.Quit();
		yield break;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00004AC5 File Offset: 0x00002CC5
	public static void SetAspectRatio(int x, int y)
	{
		if (AspectRatioController.s_Instance == null)
		{
			return;
		}
		AspectRatioController.s_Instance.SetAspectRatio((float)x, (float)y, true);
	}

	// Token: 0x0400006F RID: 111
	public AspectRatioController.ResolutionChangedEvent resolutionChangedEvent;

	// Token: 0x04000070 RID: 112
	public bool allowFullscreen = true;

	// Token: 0x04000071 RID: 113
	public float aspectRatioWidth = 16f;

	// Token: 0x04000072 RID: 114
	public float aspectRatioHeight = 9f;

	// Token: 0x04000073 RID: 115
	public int minWidthPixel = 512;

	// Token: 0x04000074 RID: 116
	public int minHeightPixel = 512;

	// Token: 0x04000075 RID: 117
	public int maxWidthPixel = 2048;

	// Token: 0x04000076 RID: 118
	public int maxHeightPixel = 2048;

	// Token: 0x04000077 RID: 119
	private static AspectRatioController s_Instance;

	// Token: 0x04000078 RID: 120
	private float aspect;

	// Token: 0x04000079 RID: 121
	private int setWidth = -1;

	// Token: 0x0400007A RID: 122
	private int setHeight = -1;

	// Token: 0x0400007B RID: 123
	private bool wasFullscreenLastFrame;

	// Token: 0x0400007C RID: 124
	private bool started;

	// Token: 0x0400007D RID: 125
	private int pixelHeightOfCurrentScreen;

	// Token: 0x0400007E RID: 126
	private int pixelWidthOfCurrentScreen;

	// Token: 0x0400007F RID: 127
	private bool quitStarted;

	// Token: 0x04000080 RID: 128
	private const int WM_SIZING = 532;

	// Token: 0x04000081 RID: 129
	private const int WMSZ_LEFT = 1;

	// Token: 0x04000082 RID: 130
	private const int WMSZ_RIGHT = 2;

	// Token: 0x04000083 RID: 131
	private const int WMSZ_TOP = 3;

	// Token: 0x04000084 RID: 132
	private const int WMSZ_BOTTOM = 6;

	// Token: 0x04000085 RID: 133
	private const int GWLP_WNDPROC = -4;

	// Token: 0x04000086 RID: 134
	private AspectRatioController.WndProcDelegate wndProcDelegate;

	// Token: 0x04000087 RID: 135
	private const string UNITY_WND_CLASSNAME = "UnityWndClass";

	// Token: 0x04000088 RID: 136
	private IntPtr unityHWnd;

	// Token: 0x04000089 RID: 137
	private IntPtr oldWndProcPtr;

	// Token: 0x0400008A RID: 138
	private IntPtr newWndProcPtr;

	// Token: 0x020010E6 RID: 4326
	[Serializable]
	public class ResolutionChangedEvent : UnityEvent<int, int, bool>
	{
	}

	// Token: 0x020010E7 RID: 4327
	// (Invoke) Token: 0x06009E71 RID: 40561
	private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	// Token: 0x020010E8 RID: 4328
	// (Invoke) Token: 0x06009E75 RID: 40565
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	// Token: 0x020010E9 RID: 4329
	public struct RECT
	{
		// Token: 0x040090D3 RID: 37075
		public int Left;

		// Token: 0x040090D4 RID: 37076
		public int Top;

		// Token: 0x040090D5 RID: 37077
		public int Right;

		// Token: 0x040090D6 RID: 37078
		public int Bottom;
	}
}
