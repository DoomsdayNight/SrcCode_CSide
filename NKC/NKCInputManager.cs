using System;
using System.Collections.Generic;
using NKC.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020006A0 RID: 1696
	public class NKCInputManager : MonoBehaviour
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x0011F700 File Offset: 0x0011D900
		public static NKCInputManager GetInputManager()
		{
			return NKCInputManager.m_InputManager;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x0011F707 File Offset: 0x0011D907
		public static float ScrollSensibility
		{
			get
			{
				return 150f;
			}
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x0011F70E File Offset: 0x0011D90E
		private void Awake()
		{
			if (NKCInputManager.m_InputManager == null)
			{
				NKCInputManager.m_InputManager = this;
				this.SetDefaultHotkeyEvent();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x0011F738 File Offset: 0x0011D938
		private void Update()
		{
			bool flag = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			this.m_hsCurrentHotKeys.Clear();
			this.m_hsCurrentIngameHotKeys.Clear();
			this.m_hsCurrentUpHotKeys.Clear();
			if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null)
			{
				return;
			}
			if (Input.anyKeyDown)
			{
				foreach (KeyValuePair<KeyCode, HotkeyEventType> keyValuePair in this.m_dicHotkeyPair)
				{
					HotkeyEventType value;
					if (flag)
					{
						if (!this.m_dicShiftHotkeyPair.TryGetValue(keyValuePair.Key, out value))
						{
							value = keyValuePair.Value;
						}
					}
					else
					{
						value = keyValuePair.Value;
					}
					if (Input.GetKeyDown(keyValuePair.Key) && !NKCScenManager.GetScenManager().ProcessGlobalHotkey(value) && !NKCUIManager.OnHotkey(value))
					{
						this.m_hsCurrentHotKeys.Add(value);
					}
				}
				foreach (KeyValuePair<KeyCode, InGamehotkeyEventType> keyValuePair2 in this.m_dicIngameHotkeyPair)
				{
					if (Input.GetKey(keyValuePair2.Key))
					{
						this.m_hsCurrentIngameHotKeys.Add(keyValuePair2.Value);
					}
				}
			}
			if (Input.anyKey)
			{
				foreach (KeyValuePair<KeyCode, HotkeyEventType> keyValuePair3 in this.m_dicHotkeyPair)
				{
					if (Input.GetKey(keyValuePair3.Key))
					{
						NKCScenManager.GetScenManager().ProcessGlobalHotkeyHold(keyValuePair3.Value);
						NKCUIManager.OnHotkeyHold(keyValuePair3.Value);
					}
				}
			}
			foreach (KeyValuePair<KeyCode, HotkeyEventType> keyValuePair4 in this.m_dicHotkeyPair)
			{
				if (Input.GetKeyUp(keyValuePair4.Key))
				{
					NKCUIManager.OnHotKeyRelease(keyValuePair4.Value);
					this.m_hsCurrentUpHotKeys.Add(keyValuePair4.Value);
				}
			}
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x0011F990 File Offset: 0x0011DB90
		private void SetDefaultHotkeyEvent()
		{
			this.AddHotkeyPair(KeyCode.UpArrow, HotkeyEventType.Up, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.DownArrow, HotkeyEventType.Down, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.LeftArrow, HotkeyEventType.Left, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.RightArrow, HotkeyEventType.Right, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.W, HotkeyEventType.Up, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.S, HotkeyEventType.Down, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.A, HotkeyEventType.Left, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.D, HotkeyEventType.Right, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Keypad8, HotkeyEventType.Up, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Keypad2, HotkeyEventType.Down, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Keypad4, HotkeyEventType.Left, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Keypad6, HotkeyEventType.Right, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Tab, HotkeyEventType.NextTab, HotkeyEventType.PrevTab);
			this.AddHotkeyPair(KeyCode.Return, HotkeyEventType.Confirm, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.KeypadEnter, HotkeyEventType.Confirm, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Space, HotkeyEventType.Confirm, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Keypad5, HotkeyEventType.Confirm, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.F1, HotkeyEventType.Help, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.LeftAlt, HotkeyEventType.ShowHotkey, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Plus, HotkeyEventType.Plus, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Minus, HotkeyEventType.Minus, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.KeypadPlus, HotkeyEventType.Plus, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.KeypadMinus, HotkeyEventType.Minus, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.LeftControl, HotkeyEventType.Skip, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Escape, HotkeyEventType.Cancel, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Mouse3, HotkeyEventType.Cancel, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Alpha4, InGamehotkeyEventType.Unit0);
			this.AddHotkeyPair(KeyCode.Alpha3, InGamehotkeyEventType.Unit1);
			this.AddHotkeyPair(KeyCode.Alpha2, InGamehotkeyEventType.Unit2);
			this.AddHotkeyPair(KeyCode.Alpha1, InGamehotkeyEventType.Unit3);
			this.AddHotkeyPair(KeyCode.Alpha5, InGamehotkeyEventType.UnitAssist);
			this.AddHotkeyPair(KeyCode.E, InGamehotkeyEventType.ShipSkill0);
			this.AddHotkeyPair(KeyCode.R, InGamehotkeyEventType.ShipSkill1);
			this.AddHotkeyPair(KeyCode.F12, HotkeyEventType.ScreenCapture, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.M, HotkeyEventType.Mute, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.LeftBracket, HotkeyEventType.MasterVolumeDown, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.RightBracket, HotkeyEventType.MasterVolumeUp, HotkeyEventType.None);
			this.AddHotkeyPair(KeyCode.Tab, InGamehotkeyEventType.Emoticon);
			this.AddHotkeyPair(KeyCode.Equals, HotkeyEventType.HamburgerMenu, HotkeyEventType.None);
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x0011FB5A File Offset: 0x0011DD5A
		private void AddHotkeyPair(KeyCode keyCode, HotkeyEventType eventType, HotkeyEventType shiftEventType = HotkeyEventType.None)
		{
			this.m_dicHotkeyPair.Add(keyCode, eventType);
			if (shiftEventType != HotkeyEventType.None)
			{
				this.m_dicShiftHotkeyPair.Add(keyCode, shiftEventType);
			}
			if (!this.m_dicKeycodeForHotkeyHelp.ContainsKey(eventType))
			{
				this.m_dicKeycodeForHotkeyHelp.Add(eventType, keyCode);
			}
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x0011FB94 File Offset: 0x0011DD94
		private void AddHotkeyPair(KeyCode keyCode, InGamehotkeyEventType eventType)
		{
			this.m_dicIngameHotkeyPair.Add(keyCode, eventType);
			if (!this.m_dicKeycodeForIngameHotkeyHelp.ContainsKey(eventType))
			{
				this.m_dicKeycodeForIngameHotkeyHelp.Add(eventType, keyCode);
			}
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x0011FBC0 File Offset: 0x0011DDC0
		public static bool IsHotkeyPressed(HotkeyEventType eventType)
		{
			return eventType != HotkeyEventType.None && (!(EventSystem.current != null) || !(EventSystem.current.currentSelectedGameObject != null) || !(EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null)) && !(NKCInputManager.m_InputManager == null) && NKCInputManager.m_InputManager._IsHotkeyPressed(eventType);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x0011FC24 File Offset: 0x0011DE24
		private bool _IsHotkeyPressed(HotkeyEventType eventType)
		{
			if (eventType == HotkeyEventType.None)
			{
				return false;
			}
			foreach (KeyValuePair<KeyCode, HotkeyEventType> keyValuePair in this.m_dicHotkeyPair)
			{
				if (keyValuePair.Value == eventType && Input.GetKey(keyValuePair.Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x0011FC94 File Offset: 0x0011DE94
		public static bool CheckHotKeyEvent(HotkeyEventType type)
		{
			return type != HotkeyEventType.None && !(NKCInputManager.m_InputManager == null) && NKCInputManager.m_InputManager._CheckHotKeyEvent(type);
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x0011FCB5 File Offset: 0x0011DEB5
		private bool _CheckHotKeyEvent(HotkeyEventType type)
		{
			return this.m_hsCurrentHotKeys.Contains(type);
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x0011FCC8 File Offset: 0x0011DEC8
		public static bool CheckHotKeyUp(HotkeyEventType type)
		{
			return type != HotkeyEventType.None && !(NKCInputManager.m_InputManager == null) && NKCInputManager.m_InputManager._CheckHotKeyUp(type);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0011FCE9 File Offset: 0x0011DEE9
		private bool _CheckHotKeyUp(HotkeyEventType type)
		{
			return this.m_hsCurrentUpHotKeys.Contains(type);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x0011FCF7 File Offset: 0x0011DEF7
		public static bool CheckHotKeyEvent(InGamehotkeyEventType type)
		{
			return !(NKCInputManager.m_InputManager == null) && NKCInputManager.m_InputManager._CheckHotKeyEvent(type);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0011FD13 File Offset: 0x0011DF13
		private bool _CheckHotKeyEvent(InGamehotkeyEventType type)
		{
			return this.m_hsCurrentIngameHotKeys.Contains(type);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x0011FD21 File Offset: 0x0011DF21
		public static void ConsumeHotKeyEvent(HotkeyEventType type)
		{
			if (NKCInputManager.m_InputManager == null)
			{
				return;
			}
			if (type == HotkeyEventType.ShowHotkey)
			{
				return;
			}
			NKCInputManager.m_InputManager._ConsumeHotKeyEvent(type);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x0011FD42 File Offset: 0x0011DF42
		private void _ConsumeHotKeyEvent(HotkeyEventType type)
		{
			this.m_hsCurrentHotKeys.Remove(type);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0011FD51 File Offset: 0x0011DF51
		public static void ConsumeHotKeyEvent(InGamehotkeyEventType type)
		{
			if (NKCInputManager.m_InputManager == null)
			{
				return;
			}
			NKCInputManager.m_InputManager._ConsumeHotKeyEvent(type);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x0011FD6C File Offset: 0x0011DF6C
		private void _ConsumeHotKeyEvent(InGamehotkeyEventType type)
		{
			this.m_hsCurrentIngameHotKeys.Remove(type);
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x0011FD7C File Offset: 0x0011DF7C
		public string GetHotkeyString(KeyCode keyCode)
		{
			if (keyCode > KeyCode.Space)
			{
				if (keyCode <= KeyCode.RightBracket)
				{
					switch (keyCode)
					{
					case KeyCode.Plus:
						break;
					case KeyCode.Comma:
					case KeyCode.Period:
					case KeyCode.Slash:
					case KeyCode.Colon:
					case KeyCode.Semicolon:
					case KeyCode.Less:
						goto IL_195;
					case KeyCode.Minus:
						goto IL_129;
					case KeyCode.Alpha0:
						return "0";
					case KeyCode.Alpha1:
						return "1";
					case KeyCode.Alpha2:
						return "2";
					case KeyCode.Alpha3:
						return "3";
					case KeyCode.Alpha4:
						return "4";
					case KeyCode.Alpha5:
						return "5";
					case KeyCode.Alpha6:
						return "6";
					case KeyCode.Alpha7:
						return "7";
					case KeyCode.Alpha8:
						return "8";
					case KeyCode.Alpha9:
						return "9";
					case KeyCode.Equals:
						return "=";
					default:
						if (keyCode == KeyCode.LeftBracket)
						{
							return "[";
						}
						if (keyCode != KeyCode.RightBracket)
						{
							goto IL_195;
						}
						return "]";
					}
				}
				else
				{
					switch (keyCode)
					{
					case KeyCode.KeypadMinus:
						goto IL_129;
					case KeyCode.KeypadPlus:
						break;
					case KeyCode.KeypadEnter:
						goto IL_10F;
					case KeyCode.KeypadEquals:
						goto IL_195;
					case KeyCode.UpArrow:
						return "↑";
					case KeyCode.DownArrow:
						return "↓";
					case KeyCode.RightArrow:
						return "→";
					case KeyCode.LeftArrow:
						return "←";
					default:
						if (keyCode == KeyCode.RightControl)
						{
							return "RCtrl";
						}
						if (keyCode != KeyCode.LeftControl)
						{
							goto IL_195;
						}
						return "Ctrl";
					}
				}
				return "+";
				IL_129:
				return "-";
			}
			if (keyCode <= KeyCode.Tab)
			{
				if (keyCode == KeyCode.Backspace)
				{
					return "BackSpace";
				}
				if (keyCode != KeyCode.Tab)
				{
					goto IL_195;
				}
				return "Tab";
			}
			else if (keyCode != KeyCode.Return)
			{
				if (keyCode == KeyCode.Escape)
				{
					return "Esc";
				}
				if (keyCode != KeyCode.Space)
				{
					goto IL_195;
				}
				return "Space";
			}
			IL_10F:
			return '⏎'.ToString();
			IL_195:
			if (KeyCode.A <= keyCode && keyCode <= KeyCode.Z)
			{
				return keyCode.ToString();
			}
			if (KeyCode.F1 <= keyCode && keyCode <= KeyCode.F15)
			{
				return keyCode.ToString();
			}
			Debug.LogError(string.Format("Keycode {0} not defined!", keyCode));
			return string.Empty;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x0011FF70 File Offset: 0x0011E170
		public string GetHotkeyString(HotkeyEventType type)
		{
			KeyCode keyCode;
			if (this.m_dicKeycodeForHotkeyHelp.TryGetValue(type, out keyCode))
			{
				return this.GetHotkeyString(keyCode);
			}
			return string.Empty;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x0011FF9C File Offset: 0x0011E19C
		public string GetHotkeyString(InGamehotkeyEventType type)
		{
			KeyCode keyCode;
			if (this.m_dicKeycodeForIngameHotkeyHelp.TryGetValue(type, out keyCode))
			{
				return this.GetHotkeyString(keyCode);
			}
			return string.Empty;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x0011FFC6 File Offset: 0x0011E1C6
		public static HotkeyEventType GetDirection(Vector2 direction)
		{
			if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			{
				if (direction.x <= 0f)
				{
					return HotkeyEventType.Left;
				}
				return HotkeyEventType.Right;
			}
			else
			{
				if (direction.y <= 0f)
				{
					return HotkeyEventType.Down;
				}
				return HotkeyEventType.Up;
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x00120004 File Offset: 0x0011E204
		public static Vector2 GetMoveVector()
		{
			Vector2 zero = Vector2.zero;
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Left))
			{
				zero.x -= 1f;
			}
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Right))
			{
				zero.x += 1f;
			}
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Up))
			{
				zero.y += 1f;
			}
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Down))
			{
				zero.y -= 1f;
			}
			return zero;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x00120078 File Offset: 0x0011E278
		public static bool IsChatSubmitEnter()
		{
			return Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit");
		}

		// Token: 0x04003468 RID: 13416
		private static NKCInputManager m_InputManager;

		// Token: 0x04003469 RID: 13417
		private HashSet<HotkeyEventType> m_hsCurrentHotKeys = new HashSet<HotkeyEventType>();

		// Token: 0x0400346A RID: 13418
		private HashSet<HotkeyEventType> m_hsCurrentUpHotKeys = new HashSet<HotkeyEventType>();

		// Token: 0x0400346B RID: 13419
		private HashSet<InGamehotkeyEventType> m_hsCurrentIngameHotKeys = new HashSet<InGamehotkeyEventType>();

		// Token: 0x0400346C RID: 13420
		private Dictionary<KeyCode, HotkeyEventType> m_dicHotkeyPair = new Dictionary<KeyCode, HotkeyEventType>();

		// Token: 0x0400346D RID: 13421
		private Dictionary<KeyCode, HotkeyEventType> m_dicShiftHotkeyPair = new Dictionary<KeyCode, HotkeyEventType>();

		// Token: 0x0400346E RID: 13422
		private Dictionary<KeyCode, InGamehotkeyEventType> m_dicIngameHotkeyPair = new Dictionary<KeyCode, InGamehotkeyEventType>();

		// Token: 0x0400346F RID: 13423
		private Dictionary<HotkeyEventType, KeyCode> m_dicKeycodeForHotkeyHelp = new Dictionary<HotkeyEventType, KeyCode>();

		// Token: 0x04003470 RID: 13424
		private Dictionary<InGamehotkeyEventType, KeyCode> m_dicKeycodeForIngameHotkeyHelp = new Dictionary<InGamehotkeyEventType, KeyCode>();
	}
}
