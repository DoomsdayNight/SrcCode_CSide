using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000344 RID: 836
	public static class UIExtensionsInputManager
	{
		// Token: 0x060013A1 RID: 5025 RVA: 0x000496F5 File Offset: 0x000478F5
		public static bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000496FD File Offset: 0x000478FD
		public static bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00049705 File Offset: 0x00047905
		public static bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004970D File Offset: 0x0004790D
		public static bool GetButton(string input)
		{
			return Input.GetButton(input);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00049715 File Offset: 0x00047915
		public static bool GetButtonDown(string input)
		{
			return Input.GetButtonDown(input);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004971D File Offset: 0x0004791D
		public static bool GetButtonUp(string input)
		{
			return Input.GetButtonUp(input);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00049725 File Offset: 0x00047925
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKey(key);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0004972D File Offset: 0x0004792D
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00049735 File Offset: 0x00047935
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004973D File Offset: 0x0004793D
		public static float GetAxisRaw(string axis)
		{
			return Input.GetAxisRaw(axis);
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00049745 File Offset: 0x00047945
		public static Vector3 MousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0004974C File Offset: 0x0004794C
		public static Vector3 MouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}
	}
}
