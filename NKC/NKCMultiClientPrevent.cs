using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006AA RID: 1706
	public class NKCMultiClientPrevent : MonoBehaviour
	{
		// Token: 0x0600387E RID: 14462 RVA: 0x001243F4 File Offset: 0x001225F4
		private void Start()
		{
			Debug.Log("NKCMultiClientPrevent Start");
			this.m_hMutex = NKCMultiClientPrevent.Import.CreateMutexW(IntPtr.Zero, false, "CounterSideNxkPC");
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (this.m_hMutex == IntPtr.Zero || (this.m_hMutex != IntPtr.Zero && (lastWin32Error == 183 || lastWin32Error == 6)))
			{
				Application.Quit();
				return;
			}
			Debug.Log("Import.GetLastError() : " + lastWin32Error.ToString());
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x00124473 File Offset: 0x00122673
		private void Update()
		{
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x00124478 File Offset: 0x00122678
		private void OnDestroy()
		{
			Debug.Log("NKCMultiClientPrevent OnDestroy");
			if (this.m_hMutex != IntPtr.Zero)
			{
				NKCMultiClientPrevent.Import.ReleaseMutex(this.m_hMutex);
			}
			if (this.m_hMutex != IntPtr.Zero)
			{
				NKCMultiClientPrevent.Import.CloseHandle(this.m_hMutex);
			}
			this.m_hMutex = IntPtr.Zero;
		}

		// Token: 0x040034C6 RID: 13510
		private IntPtr m_hMutex = IntPtr.Zero;

		// Token: 0x02001375 RID: 4981
		public static class Import
		{
			// Token: 0x0600A5F2 RID: 42482
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern IntPtr CreateMutexW(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

			// Token: 0x0600A5F3 RID: 42483
			[DllImport("kernel32.dll")]
			public static extern bool ReleaseMutex(IntPtr hMutex);

			// Token: 0x0600A5F4 RID: 42484
			[DllImport("kernel32.dll")]
			public static extern bool CloseHandle(IntPtr hObject);

			// Token: 0x040099E8 RID: 39400
			public const int ERROR_ALREADY_EXISTS = 183;

			// Token: 0x040099E9 RID: 39401
			public const int ERROR_INVALID_HANDLE = 6;
		}
	}
}
