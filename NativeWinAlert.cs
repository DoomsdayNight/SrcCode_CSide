using System;
using System.Runtime.InteropServices;

// Token: 0x02000008 RID: 8
public static class NativeWinAlert
{
	// Token: 0x06000024 RID: 36
	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	// Token: 0x06000025 RID: 37 RVA: 0x000028B4 File Offset: 0x00000AB4
	public static IntPtr GetWindowHandle()
	{
		return NativeWinAlert.GetActiveWindow();
	}

	// Token: 0x06000026 RID: 38
	[DllImport("user32.dll", SetLastError = true)]
	public static extern int MessageBox(IntPtr hwnd, string lpText, string lpCaption, uint uType);
}
