using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.Quests;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Video;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.Native
{
	public class NativeClient : IPlayGamesClient
	{
		private enum AuthState
		{
			Unauthenticated,
			Authenticated,
			SilentPending
		}

		private readonly IClientImpl clientImpl;

		private readonly object GameServicesLock = new object();

		private readonly object AuthStateLock = new object();

		private readonly PlayGamesClientConfiguration mConfiguration;

		private GooglePlayGames.Native.PInvoke.GameServices mServices;

		private volatile NativeTurnBasedMultiplayerClient mTurnBasedClient;

		private volatile NativeRealtimeMultiplayerClient mRealTimeClient;

		private volatile ISavedGameClient mSavedGameClient;

		private volatile IEventsClient mEventsClient;

		private volatile IQuestsClient mQuestsClient;

		private volatile IVideoClient mVideoClient;

		private volatile TokenClient mTokenClient;

		private volatile Action<Invitation, bool> mInvitationDelegate;

		private volatile Dictionary<string, GooglePlayGames.BasicApi.Achievement> mAchievements;

		private volatile GooglePlayGames.BasicApi.Multiplayer.Player mUser;

		private volatile List<GooglePlayGames.BasicApi.Multiplayer.Player> mFriends;

		private volatile Action<bool, string> mPendingAuthCallbacks;

		private volatile Action<bool, string> mSilentAuthCallbacks;

		private volatile AuthState mAuthState;

		private volatile uint mAuthGeneration;

		private volatile bool mSilentAuthFailed;

		private volatile bool friendsLoading;

		internal NativeClient(PlayGamesClientConfiguration configuration, IClientImpl clientImpl)
		{
			PlayGamesHelperObject.CreateObject();
			mConfiguration = Misc.CheckNotNull(configuration);
			this.clientImpl = clientImpl;
		}

		private GooglePlayGames.Native.PInvoke.GameServices GameServices()
		{
			lock (GameServicesLock)
			{
				return mServices;
				IL_0019:
				GooglePlayGames.Native.PInvoke.GameServices result;
				return result;
			}
		}

		public unsafe void Authenticate(Action<bool, string> callback, bool silent)
		{
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Expected O, but got Unknown
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Expected O, but got Unknown
			lock (AuthStateLock)
			{
				if (mAuthState == AuthState.Authenticated)
				{
					InvokeCallbackOnGameThread<bool, string>(callback, true, null);
					return;
				}
				if (mSilentAuthFailed && silent)
				{
					InvokeCallbackOnGameThread(callback, false, "silent auth failed");
					return;
				}
				if (callback != null)
				{
					if (silent)
					{
						mSilentAuthCallbacks = Delegate.Combine((Delegate)mSilentAuthCallbacks, (Delegate)callback);
					}
					else
					{
						mPendingAuthCallbacks = Delegate.Combine((Delegate)mPendingAuthCallbacks, (Delegate)callback);
					}
				}
			}
			InitializeGameServices();
			friendsLoading = false;
			if (mTokenClient.NeedsToRun())
			{
				Debug.Log((object)"Using AuthHelper to sign in");
				mTokenClient.FetchTokens(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			if (!silent)
			{
				if (mTokenClient.NeedsToRun())
				{
					mTokenClient.FetchTokens(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				else
				{
					GameServices().StartAuthorizationUI();
				}
			}
		}

		private static Action<T> AsOnGameThreadCallback<T>(Action<T> callback)
		{
			if (callback == null)
			{
				return delegate
				{
				};
			}
			return delegate(T result)
			{
				NativeClient.InvokeCallbackOnGameThread<T>(callback, result);
			};
		}

		private unsafe static void InvokeCallbackOnGameThread<T, S>(Action<T, S> callback, T data, S msg)
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			if (callback != null)
			{
				_003CInvokeCallbackOnGameThread_003Ec__AnonStorey813<T, S> _003CInvokeCallbackOnGameThread_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CInvokeCallbackOnGameThread_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		private unsafe static void InvokeCallbackOnGameThread<T>(Action<T> callback, T data)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			if (callback != null)
			{
				_003CInvokeCallbackOnGameThread_003Ec__AnonStorey814<T> _003CInvokeCallbackOnGameThread_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CInvokeCallbackOnGameThread_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		private unsafe void InitializeGameServices()
		{
			lock (GameServicesLock)
			{
				if (mServices == null)
				{
					using (GameServicesBuilder gameServicesBuilder = GameServicesBuilder.Create())
					{
						using (PlatformConfiguration configRef = clientImpl.CreatePlatformConfiguration(mConfiguration))
						{
							RegisterInvitationDelegate(mConfiguration.InvitationDelegate);
							gameServicesBuilder.SetOnAuthFinishedCallback(HandleAuthTransition);
							gameServicesBuilder.SetOnTurnBasedMatchEventCallback(new Action<Types.MultiplayerEvent, string, NativeTurnBasedMatch>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
							gameServicesBuilder.SetOnMultiplayerInvitationEventCallback(new Action<Types.MultiplayerEvent, string, GooglePlayGames.Native.PInvoke.MultiplayerInvitation>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
							if (mConfiguration.EnableSavedGames)
							{
								gameServicesBuilder.EnableSnapshots();
							}
							string[] scopes = mConfiguration.Scopes;
							for (int i = 0; i < scopes.Length; i++)
							{
								gameServicesBuilder.AddOauthScope(scopes[i]);
							}
							if (mConfiguration.IsHidingPopups)
							{
								gameServicesBuilder.SetShowConnectingPopup(false);
							}
							Debug.Log((object)"Building GPG services, implicitly attempts silent auth");
							mAuthState = AuthState.SilentPending;
							mServices = gameServicesBuilder.Build(configRef);
							mEventsClient = new NativeEventClient(new GooglePlayGames.Native.PInvoke.EventManager(mServices));
							mQuestsClient = new NativeQuestClient(new GooglePlayGames.Native.PInvoke.QuestManager(mServices));
							mVideoClient = new NativeVideoClient(new GooglePlayGames.Native.PInvoke.VideoManager(mServices));
							mTurnBasedClient = new NativeTurnBasedMultiplayerClient(this, new TurnBasedManager(mServices));
							mTurnBasedClient.RegisterMatchDelegate(mConfiguration.MatchDelegate);
							mRealTimeClient = new NativeRealtimeMultiplayerClient(this, new RealtimeManager(mServices));
							if (mConfiguration.EnableSavedGames)
							{
								mSavedGameClient = new NativeSavedGameClient(new GooglePlayGames.Native.PInvoke.SnapshotManager(mServices));
							}
							else
							{
								mSavedGameClient = new UnsupportedSavedGamesClient("You must enable saved games before it can be used. See PlayGamesClientConfiguration.Builder.EnableSavedGames.");
							}
							mAuthState = AuthState.SilentPending;
							mTokenClient = clientImpl.CreateTokenClient(true);
							if (!GameInfo.WebClientIdInitialized() && (mConfiguration.IsRequestingIdToken || mConfiguration.IsRequestingAuthCode))
							{
								Logger.e("Server Auth Code and ID Token require web clientId to configured.");
							}
							mTokenClient.SetWebClientId("683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com");
							mTokenClient.SetRequestAuthCode(mConfiguration.IsRequestingAuthCode, mConfiguration.IsForcingRefresh);
							mTokenClient.SetRequestEmail(mConfiguration.IsRequestingEmail);
							mTokenClient.SetRequestIdToken(mConfiguration.IsRequestingIdToken);
							mTokenClient.SetHidePopups(mConfiguration.IsHidingPopups);
							mTokenClient.AddOauthScopes(scopes);
							mTokenClient.SetAccountName(mConfiguration.AccountName);
						}
					}
				}
			}
		}

		internal unsafe void HandleInvitation(Types.MultiplayerEvent eventType, string invitationId, GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation)
		{
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			Action<Invitation, bool> currentHandler = mInvitationDelegate;
			if (currentHandler == null)
			{
				Logger.d("Received " + eventType + " for invitation " + invitationId + " but no handler was registered.");
			}
			else if (eventType == Types.MultiplayerEvent.REMOVED)
			{
				Logger.d("Ignoring REMOVED for invitation " + invitationId);
			}
			else
			{
				bool shouldAutolaunch = eventType == Types.MultiplayerEvent.UPDATED_FROM_APP_LAUNCH;
				Invitation invite = invitation.AsInvitation();
				_003CHandleInvitation_003Ec__AnonStorey815 _003CHandleInvitation_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CHandleInvitation_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public string GetUserEmail()
		{
			if (!IsAuthenticated())
			{
				Debug.Log((object)"Cannot get API client - not authenticated");
				return null;
			}
			return mTokenClient.GetEmail();
		}

		public string GetIdToken()
		{
			if (!IsAuthenticated())
			{
				Debug.Log((object)"Cannot get API client - not authenticated");
				return null;
			}
			return mTokenClient.GetIdToken();
		}

		public string GetServerAuthCode()
		{
			if (!IsAuthenticated())
			{
				Debug.Log((object)"Cannot get API client - not authenticated");
				return null;
			}
			return mTokenClient.GetAuthCode();
		}

		public bool IsAuthenticated()
		{
			lock (AuthStateLock)
			{
				return mAuthState == AuthState.Authenticated;
				IL_001e:
				bool result;
				return result;
			}
		}

		public unsafe void LoadFriends(Action<bool> callback)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			_003CLoadFriends_003Ec__AnonStorey816 _003CLoadFriends_003Ec__AnonStorey;
			if (!IsAuthenticated())
			{
				Logger.d("Cannot loadFriends when not authenticated");
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CLoadFriends_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			else if (mFriends != null)
			{
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CLoadFriends_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			else
			{
				mServices.PlayerManager().FetchFriends(new Action<ResponseStatus, List<GooglePlayGames.BasicApi.Multiplayer.Player>>((object)_003CLoadFriends_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public IUserProfile[] GetFriends()
		{
			if (mFriends == null && !friendsLoading)
			{
				Logger.w("Getting friends before they are loaded!!!");
				friendsLoading = true;
				LoadFriends(delegate(bool ok)
				{
					Logger.d("loading: " + ok + " mFriends = " + mFriends);
					if (!ok)
					{
						Logger.e("Friends list did not load successfully.  Disabling loading until re-authenticated");
					}
					friendsLoading = !ok;
				});
			}
			return (IUserProfile[])((mFriends != null) ? ((object)mFriends.ToArray()) : ((object)new IUserProfile[0]));
		}

		private void PopulateAchievements(uint authGeneration, GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse response)
		{
			if (authGeneration != mAuthGeneration)
			{
				Logger.d("Received achievement callback after signout occurred, ignoring");
			}
			else
			{
				Logger.d("Populating Achievements, status = " + response.Status());
				lock (AuthStateLock)
				{
					if (response.Status() != CommonErrorStatus.ResponseStatus.VALID && response.Status() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
					{
						Logger.e("Error retrieving achievements - check the log for more information. Failing signin.");
						Action<bool, string> val = mPendingAuthCallbacks;
						mPendingAuthCallbacks = null;
						if (val != null)
						{
							InvokeCallbackOnGameThread(val, false, "Cannot load achievements, Authenication failing");
						}
						SignOut();
						return;
					}
					Dictionary<string, GooglePlayGames.BasicApi.Achievement> dictionary = new Dictionary<string, GooglePlayGames.BasicApi.Achievement>();
					foreach (NativeAchievement item in response)
					{
						using (item)
						{
							dictionary[item.Id()] = item.AsAchievement();
						}
					}
					Logger.d("Found " + dictionary.Count + " Achievements");
					mAchievements = dictionary;
				}
				Logger.d("Maybe finish for Achievements");
				MaybeFinishAuthentication();
			}
		}

		private void MaybeFinishAuthentication()
		{
			Action<bool, string> val = null;
			lock (AuthStateLock)
			{
				if (mUser == null || mAchievements == null)
				{
					Logger.d("Auth not finished. User=" + mUser + " achievements=" + mAchievements);
					return;
				}
				Logger.d("Auth finished. Proceeding.");
				val = mPendingAuthCallbacks;
				mPendingAuthCallbacks = null;
				mAuthState = AuthState.Authenticated;
			}
			if (val != null)
			{
				Logger.d("Invoking Callbacks: " + val);
				InvokeCallbackOnGameThread<bool, string>(val, true, null);
			}
		}

		private void PopulateUser(uint authGeneration, GooglePlayGames.Native.PInvoke.PlayerManager.FetchSelfResponse response)
		{
			Logger.d("Populating User");
			if (authGeneration != mAuthGeneration)
			{
				Logger.d("Received user callback after signout occurred, ignoring");
			}
			else
			{
				lock (AuthStateLock)
				{
					if (response.Status() != CommonErrorStatus.ResponseStatus.VALID && response.Status() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
					{
						Logger.e("Error retrieving user, signing out");
						Action<bool, string> val = mPendingAuthCallbacks;
						mPendingAuthCallbacks = null;
						if (val != null)
						{
							InvokeCallbackOnGameThread(val, false, "Cannot load user profile");
						}
						SignOut();
						return;
					}
					mUser = response.Self().AsPlayer();
					mFriends = null;
				}
				Logger.d("Found User: " + mUser);
				Logger.d("Maybe finish for User");
				MaybeFinishAuthentication();
			}
		}

		private void HandleAuthTransition(Types.AuthOperation operation, CommonErrorStatus.AuthStatus status)
		{
			Logger.d("Starting Auth Transition. Op: " + operation + " status: " + status);
			lock (AuthStateLock)
			{
				switch (operation)
				{
				case Types.AuthOperation.SIGN_IN:
					if (status == CommonErrorStatus.AuthStatus.VALID)
					{
						if (mSilentAuthCallbacks != null)
						{
							mPendingAuthCallbacks = Delegate.Combine((Delegate)mPendingAuthCallbacks, (Delegate)mSilentAuthCallbacks);
							mSilentAuthCallbacks = null;
						}
						uint currentAuthGeneration = mAuthGeneration;
						mServices.AchievementManager().FetchAll(delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse results)
						{
							PopulateAchievements(currentAuthGeneration, results);
						});
						mServices.PlayerManager().FetchSelf(delegate(GooglePlayGames.Native.PInvoke.PlayerManager.FetchSelfResponse results)
						{
							PopulateUser(currentAuthGeneration, results);
						});
					}
					else if (mAuthState == AuthState.SilentPending)
					{
						mSilentAuthFailed = true;
						mAuthState = AuthState.Unauthenticated;
						Action<bool, string> callback = mSilentAuthCallbacks;
						mSilentAuthCallbacks = null;
						Logger.d("Invoking callbacks, AuthState changed from silentPending to Unauthenticated.");
						InvokeCallbackOnGameThread(callback, false, "silent auth failed");
						if (mPendingAuthCallbacks != null)
						{
							Logger.d("there are pending auth callbacks - starting AuthUI");
							GameServices().StartAuthorizationUI();
						}
					}
					else
					{
						Logger.d("AuthState == " + mAuthState + " calling auth callbacks with failure");
						UnpauseUnityPlayer();
						Action<bool, string> callback2 = mPendingAuthCallbacks;
						mPendingAuthCallbacks = null;
						InvokeCallbackOnGameThread(callback2, false, "Authentication failed");
					}
					break;
				case Types.AuthOperation.SIGN_OUT:
					ToUnauthenticated();
					break;
				default:
					Logger.e("Unknown AuthOperation " + operation);
					break;
				}
			}
		}

		private void UnpauseUnityPlayer()
		{
		}

		private void ToUnauthenticated()
		{
			lock (AuthStateLock)
			{
				mUser = null;
				mFriends = null;
				mAchievements = null;
				mAuthState = AuthState.Unauthenticated;
				mTokenClient = clientImpl.CreateTokenClient(true);
				mAuthGeneration++;
			}
		}

		public void SignOut()
		{
			ToUnauthenticated();
			if (GameServices() != null)
			{
				mTokenClient.Signout();
				GameServices().SignOut();
			}
		}

		public string GetUserId()
		{
			if (mUser == null)
			{
				return null;
			}
			return mUser.id;
		}

		public string GetUserDisplayName()
		{
			if (mUser == null)
			{
				return null;
			}
			return mUser.userName;
		}

		public string GetUserImageUrl()
		{
			if (mUser == null)
			{
				return null;
			}
			return mUser.AvatarURL;
		}

		public unsafe void GetPlayerStats(Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			_003CGetPlayerStats_003Ec__AnonStorey818 _003CGetPlayerStats_003Ec__AnonStorey;
			PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CGetPlayerStats_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public unsafe void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
		{
			mServices.PlayerManager().FetchList(userIds, delegate(NativePlayer[] nativeUsers)
			{
				//IL_004b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0050: Expected O, but got Unknown
				IUserProfile[] users = (IUserProfile[])new IUserProfile[nativeUsers.Length];
				for (int i = 0; i < users.Length; i++)
				{
					users[i] = nativeUsers[i].AsPlayer();
				}
				_003CLoadUsers_003Ec__AnonStorey819._003CLoadUsers_003Ec__AnonStorey81A _003CLoadUsers_003Ec__AnonStorey81A;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CLoadUsers_003Ec__AnonStorey81A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			});
		}

		public GooglePlayGames.BasicApi.Achievement GetAchievement(string achId)
		{
			if (mAchievements == null || !mAchievements.ContainsKey(achId))
			{
				return null;
			}
			return mAchievements[achId];
		}

		public unsafe void LoadAchievements(Action<GooglePlayGames.BasicApi.Achievement[]> callback)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			GooglePlayGames.BasicApi.Achievement[] data = new GooglePlayGames.BasicApi.Achievement[mAchievements.Count];
			mAchievements.Values.CopyTo(data, 0);
			_003CLoadAchievements_003Ec__AnonStorey81B _003CLoadAchievements_003Ec__AnonStorey81B;
			PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CLoadAchievements_003Ec__AnonStorey81B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public void UnlockAchievement(string achId, Action<bool> callback)
		{
			UpdateAchievement("Unlock", achId, callback, (GooglePlayGames.BasicApi.Achievement a) => a.IsUnlocked, delegate(GooglePlayGames.BasicApi.Achievement a)
			{
				a.IsUnlocked = true;
				GameServices().AchievementManager().Unlock(achId);
			});
		}

		public void RevealAchievement(string achId, Action<bool> callback)
		{
			UpdateAchievement("Reveal", achId, callback, (GooglePlayGames.BasicApi.Achievement a) => a.IsRevealed, delegate(GooglePlayGames.BasicApi.Achievement a)
			{
				a.IsRevealed = true;
				GameServices().AchievementManager().Reveal(achId);
			});
		}

		private void UpdateAchievement(string updateType, string achId, Action<bool> callback, Predicate<GooglePlayGames.BasicApi.Achievement> alreadyDone, Action<GooglePlayGames.BasicApi.Achievement> updateAchievment)
		{
			callback = AsOnGameThreadCallback(callback);
			Misc.CheckNotNull(achId);
			InitializeGameServices();
			GooglePlayGames.BasicApi.Achievement achievement = GetAchievement(achId);
			if (achievement == null)
			{
				Logger.d("Could not " + updateType + ", no achievement with ID " + achId);
				callback(false);
			}
			else if (alreadyDone(achievement))
			{
				Logger.d("Did not need to perform " + updateType + ": on achievement " + achId);
				callback(true);
			}
			else
			{
				Logger.d("Performing " + updateType + " on " + achId);
				updateAchievment(achievement);
				GameServices().AchievementManager().Fetch(achId, delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse rsp)
				{
					if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID)
					{
						mAchievements.Remove(achId);
						mAchievements.Add(achId, rsp.Achievement().AsAchievement());
						callback(true);
					}
					else
					{
						Logger.e("Cannot refresh achievement " + achId + ": " + rsp.Status());
						callback(false);
					}
				});
			}
		}

		public void IncrementAchievement(string achId, int steps, Action<bool> callback)
		{
			Misc.CheckNotNull(achId);
			callback = AsOnGameThreadCallback(callback);
			InitializeGameServices();
			GooglePlayGames.BasicApi.Achievement achievement = GetAchievement(achId);
			if (achievement == null)
			{
				Logger.e("Could not increment, no achievement with ID " + achId);
				callback(false);
			}
			else if (!achievement.IsIncremental)
			{
				Logger.e("Could not increment, achievement with ID " + achId + " was not incremental");
				callback(false);
			}
			else if (steps < 0)
			{
				Logger.e("Attempted to increment by negative steps");
				callback(false);
			}
			else
			{
				GameServices().AchievementManager().Increment(achId, Convert.ToUInt32(steps));
				GameServices().AchievementManager().Fetch(achId, delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse rsp)
				{
					if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID)
					{
						mAchievements.Remove(achId);
						mAchievements.Add(achId, rsp.Achievement().AsAchievement());
						callback(true);
					}
					else
					{
						Logger.e("Cannot refresh achievement " + achId + ": " + rsp.Status());
						callback(false);
					}
				});
			}
		}

		public void SetStepsAtLeast(string achId, int steps, Action<bool> callback)
		{
			Misc.CheckNotNull(achId);
			callback = AsOnGameThreadCallback(callback);
			InitializeGameServices();
			GooglePlayGames.BasicApi.Achievement achievement = GetAchievement(achId);
			if (achievement == null)
			{
				Logger.e("Could not increment, no achievement with ID " + achId);
				callback(false);
			}
			else if (!achievement.IsIncremental)
			{
				Logger.e("Could not increment, achievement with ID " + achId + " is not incremental");
				callback(false);
			}
			else if (steps < 0)
			{
				Logger.e("Attempted to increment by negative steps");
				callback(false);
			}
			else
			{
				GameServices().AchievementManager().SetStepsAtLeast(achId, Convert.ToUInt32(steps));
				GameServices().AchievementManager().Fetch(achId, delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse rsp)
				{
					if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID)
					{
						mAchievements.Remove(achId);
						mAchievements.Add(achId, rsp.Achievement().AsAchievement());
						callback(true);
					}
					else
					{
						Logger.e("Cannot refresh achievement " + achId + ": " + rsp.Status());
						callback(false);
					}
				});
			}
		}

		public void ShowAchievementsUI(Action<UIStatus> cb)
		{
			if (IsAuthenticated())
			{
				Action<CommonErrorStatus.UIStatus> callback = Callbacks.NoopUICallback;
				if (cb != null)
				{
					callback = delegate(CommonErrorStatus.UIStatus result)
					{
						cb((UIStatus)result);
					};
				}
				callback = AsOnGameThreadCallback(callback);
				GameServices().AchievementManager().ShowAllUI(callback);
			}
		}

		public int LeaderboardMaxResults()
		{
			return GameServices().LeaderboardManager().LeaderboardMaxResults;
		}

		public void ShowLeaderboardUI(string leaderboardId, LeaderboardTimeSpan span, Action<UIStatus> cb)
		{
			if (IsAuthenticated())
			{
				Action<CommonErrorStatus.UIStatus> callback = Callbacks.NoopUICallback;
				if (cb != null)
				{
					callback = delegate(CommonErrorStatus.UIStatus result)
					{
						cb((UIStatus)result);
					};
				}
				callback = AsOnGameThreadCallback(callback);
				if (leaderboardId == null)
				{
					GameServices().LeaderboardManager().ShowAllUI(callback);
				}
				else
				{
					GameServices().LeaderboardManager().ShowUI(leaderboardId, span, callback);
				}
			}
		}

		public void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback)
		{
			callback = AsOnGameThreadCallback(callback);
			GameServices().LeaderboardManager().LoadLeaderboardData(leaderboardId, start, rowCount, collection, timeSpan, mUser.id, callback);
		}

		public void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback)
		{
			callback = AsOnGameThreadCallback(callback);
			GameServices().LeaderboardManager().LoadScorePage(null, rowCount, token, callback);
		}

		public void SubmitScore(string leaderboardId, long score, Action<bool> callback)
		{
			callback = AsOnGameThreadCallback(callback);
			if (!IsAuthenticated())
			{
				callback(false);
			}
			InitializeGameServices();
			if (leaderboardId == null)
			{
				throw new ArgumentNullException("leaderboardId");
			}
			GameServices().LeaderboardManager().SubmitScore(leaderboardId, score, null);
			callback(true);
		}

		public void SubmitScore(string leaderboardId, long score, string metadata, Action<bool> callback)
		{
			callback = AsOnGameThreadCallback(callback);
			if (!IsAuthenticated())
			{
				callback(false);
			}
			InitializeGameServices();
			if (leaderboardId == null)
			{
				throw new ArgumentNullException("leaderboardId");
			}
			GameServices().LeaderboardManager().SubmitScore(leaderboardId, score, metadata);
			callback(true);
		}

		public IRealTimeMultiplayerClient GetRtmpClient()
		{
			if (!IsAuthenticated())
			{
				return null;
			}
			lock (GameServicesLock)
			{
				return mRealTimeClient;
				IL_0028:
				IRealTimeMultiplayerClient result;
				return result;
			}
		}

		public ITurnBasedMultiplayerClient GetTbmpClient()
		{
			lock (GameServicesLock)
			{
				return mTurnBasedClient;
				IL_001b:
				ITurnBasedMultiplayerClient result;
				return result;
			}
		}

		public ISavedGameClient GetSavedGameClient()
		{
			lock (GameServicesLock)
			{
				return mSavedGameClient;
				IL_001b:
				ISavedGameClient result;
				return result;
			}
		}

		public IEventsClient GetEventsClient()
		{
			lock (GameServicesLock)
			{
				return mEventsClient;
				IL_001b:
				IEventsClient result;
				return result;
			}
		}

		public IQuestsClient GetQuestsClient()
		{
			lock (GameServicesLock)
			{
				return mQuestsClient;
				IL_001b:
				IQuestsClient result;
				return result;
			}
		}

		public IVideoClient GetVideoClient()
		{
			lock (GameServicesLock)
			{
				return mVideoClient;
				IL_001b:
				IVideoClient result;
				return result;
			}
		}

		public unsafe void RegisterInvitationDelegate(InvitationReceivedDelegate invitationDelegate)
		{
			if (invitationDelegate == null)
			{
				mInvitationDelegate = null;
			}
			else
			{
				_003CRegisterInvitationDelegate_003Ec__AnonStorey823 _003CRegisterInvitationDelegate_003Ec__AnonStorey;
				mInvitationDelegate = Callbacks.AsOnGameThreadCallback<Invitation, bool>(new Action<Invitation, bool>((object)_003CRegisterInvitationDelegate_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public IntPtr GetApiClient()
		{
			return InternalHooks.InternalHooks_GetApiClient(mServices.AsHandle());
		}
	}
}
