using System;
using System.Collections;
using System.IO;
using AssetBundles;
using NKC.InfraTool;

// Token: 0x0200000C RID: 12
public static class PatchToolVMExtension
{
	// Token: 0x0600007B RID: 123 RVA: 0x00002F40 File Offset: 0x00001140
	public static void Init(this PatchToolVM vm)
	{
		vm.PatchController.Init();
		vm.ConfigServerAddress = vm.PatchController.BaseFileServerAddress;
		vm.ProtocolVersion = vm.PatchController.ProtocolVersion;
		vm.Status = "Empty";
		vm.Log = "Empty";
		vm.Solution = "Empty";
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00002F9C File Offset: 0x0000119C
	public static void Update(this PatchToolVM vm)
	{
		PatchCheckController patchController = vm.PatchController;
		vm.Status = patchController.VersionInfoStr;
		vm.Log = patchController.logStr;
		vm.Solution = patchController.ErrorSolutionStr;
		vm.PatchController.Update();
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00002FDF File Offset: 0x000011DF
	public static IEnumerator Run(this PatchToolVM vm)
	{
		PatchCheckController patchController = vm.PatchController;
		yield return (patchController != null) ? patchController.RunAll() : null;
		yield break;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00002FEE File Offset: 0x000011EE
	public static IEnumerator RunExtraAsset(this PatchToolVM vm)
	{
		PatchCheckController patchController = vm.PatchController;
		yield return (patchController != null) ? patchController.ExtraAssetRunAll() : null;
		yield break;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00002FFD File Offset: 0x000011FD
	public static void RefreshServerAddress(this PatchToolVM vm, string address, string protocolVersion)
	{
		vm.ConfigServerAddress = address;
		vm.PatchController.BaseFileServerAddress = address;
		vm.PatchController.ProtocolVersion = protocolVersion;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003020 File Offset: 0x00001220
	public static void CleanUp(this PatchToolVM vm)
	{
		string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
		if (Directory.Exists(localDownloadPath))
		{
			Directory.Delete(localDownloadPath, true);
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003042 File Offset: 0x00001242
	public static void SaveLog(this PatchToolVM vm)
	{
		PatchCheckController patchController = vm.PatchController;
		if (patchController == null)
		{
			return;
		}
		patchController.SaveLogToText();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003054 File Offset: 0x00001254
	public static void SaveOpenTag(this PatchToolVM vm)
	{
		PatchCheckController patchController = vm.PatchController;
		if (patchController == null)
		{
			return;
		}
		patchController.SaveTagListToText();
	}
}
