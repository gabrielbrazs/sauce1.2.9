using GooglePlayGames.Native.Cwrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
	internal class PlayerSelectUIResponse : BaseReferenceHolder, IEnumerable<string>, IEnumerable
	{
		internal PlayerSelectUIResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal CommonErrorStatus.UIStatus Status()
		{
			return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetStatus(SelfPtr());
		}

		private string PlayerIdAtIndex(UIntPtr index)
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_GetElement(SelfPtr(), index, out_string, size));
		}

		public unsafe IEnumerator<string> GetEnumerator()
		{
			return PInvokeUtilities.ToEnumerator<string>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_Length(SelfPtr()), new Func<UIntPtr, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		internal uint MinimumAutomatchingPlayers()
		{
			return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMinimumAutomatchingPlayers(SelfPtr());
		}

		internal uint MaximumAutomatchingPlayers()
		{
			return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMaximumAutomatchingPlayers(SelfPtr());
		}

		protected override void CallDispose(HandleRef selfPointer)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_Dispose(selfPointer);
		}

		internal static PlayerSelectUIResponse FromPointer(IntPtr pointer)
		{
			if (PInvokeUtilities.IsNull(pointer))
			{
				return null;
			}
			return new PlayerSelectUIResponse(pointer);
		}
	}
}
