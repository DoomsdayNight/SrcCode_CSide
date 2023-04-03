using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

// Token: 0x02000023 RID: 35
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600010E RID: 270 RVA: 0x00004DCF File Offset: 0x00002FCF
	protected static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				Debug.Log("[SteamManager] New SteamManager Object");
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600010F RID: 271 RVA: 0x00004DFD File Offset: 0x00002FFD
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00004E09 File Offset: 0x00003009
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00004E11 File Offset: 0x00003011
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00004E20 File Offset: 0x00003020
	protected virtual void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		Debug.Log("[SteamManager] Steamworks.NET Initialized");
		SteamManager.s_EverInitialized = true;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00004EBC File Offset: 0x000030BC
	protected virtual void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00004F0A File Offset: 0x0000310A
	protected virtual void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00004F2E File Offset: 0x0000312E
	protected virtual void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x0400009E RID: 158
	protected static bool s_EverInitialized;

	// Token: 0x0400009F RID: 159
	protected static SteamManager s_instance;

	// Token: 0x040000A0 RID: 160
	protected bool m_bInitialized;

	// Token: 0x040000A1 RID: 161
	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
