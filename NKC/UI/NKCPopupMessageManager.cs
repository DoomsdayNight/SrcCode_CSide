using System;
using System.Collections.Generic;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI.Contract;
using NKC.UI.Result;
using NKM;

namespace NKC.UI
{
	// Token: 0x02000A25 RID: 2597
	internal static class NKCPopupMessageManager
	{
		// Token: 0x060071C5 RID: 29125 RVA: 0x0025D344 File Offset: 0x0025B544
		public static void AddPopupMessage(string message, NKCPopupMessage.eMessagePosition position = NKCPopupMessage.eMessagePosition.Top, bool bShowFX = false, bool bPreemptive = true, float delayTime = 0f, bool bWaitForGameEnd = false)
		{
			PopupMessage value = new PopupMessage(message, position, delayTime, bPreemptive, bShowFX, bWaitForGameEnd);
			NKCPopupMessageManager.s_llMessage.AddLast(value);
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x0025D36C File Offset: 0x0025B56C
		public static void AddPopupMessage(NKM_ERROR_CODE errorCode, NKCPopupMessage.eMessagePosition position = NKCPopupMessage.eMessagePosition.Top, bool bShowFX = false, bool bPreemptive = true, float delayTime = 0f, bool bWaitForGameEnd = false)
		{
			PopupMessage value = new PopupMessage(NKCPacketHandlers.GetErrorMessage(errorCode), position, delayTime, bPreemptive, bShowFX, bWaitForGameEnd);
			NKCPopupMessageManager.s_llMessage.AddLast(value);
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x0025D398 File Offset: 0x0025B598
		public static void AddPopupMessage(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError, NKCPopupMessage.eMessagePosition position = NKCPopupMessage.eMessagePosition.Top, bool bShowFX = false, bool bPreemptive = true, float delayTime = 0f, bool bWaitForGameEnd = false)
		{
			PopupMessage value = new PopupMessage(NKCPublisherModule.GetErrorMessage(resultCode, additionalError), position, delayTime, bPreemptive, bShowFX, bWaitForGameEnd);
			NKCPopupMessageManager.s_llMessage.AddLast(value);
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x0025D3C6 File Offset: 0x0025B5C6
		public static void AddPopupMessage(PopupMessage msg)
		{
			NKCPopupMessageManager.s_llMessage.AddLast(msg);
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x0025D3D4 File Offset: 0x0025B5D4
		public static void Update(float deltaTime)
		{
			if (NKCScenManager.GetScenManager().GetNowScenState() != NKC_SCEN_STATE.NSS_START)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CONTRACT && (NKCUIContractSequence.IsInstanceOpen || NKCUIGameResultGetUnit.IsInstanceOpen || NKCUIResult.IsInstanceOpen))
			{
				return;
			}
			LinkedListNode<PopupMessage> linkedListNode = NKCPopupMessageManager.s_llMessage.First;
			while (linkedListNode != null)
			{
				PopupMessage value = linkedListNode.Value;
				if (value.m_delayTime <= 0f)
				{
					if (value.m_bWaitForGameEnd && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
					{
						linkedListNode = linkedListNode.Next;
					}
					else
					{
						NKCUIManager.NKCPopupMessage.Open(value);
						LinkedListNode<PopupMessage> node = linkedListNode;
						linkedListNode = linkedListNode.Next;
						NKCPopupMessageManager.s_llMessage.Remove(node);
					}
				}
				else
				{
					value.m_delayTime -= deltaTime;
					linkedListNode = linkedListNode.Next;
				}
			}
		}

		// Token: 0x04005DB6 RID: 23990
		private static LinkedList<PopupMessage> s_llMessage = new LinkedList<PopupMessage>();
	}
}
