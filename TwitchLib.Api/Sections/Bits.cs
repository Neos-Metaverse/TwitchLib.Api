﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Extensions.System;

namespace TwitchLib.Api.Sections
{
    public class Bits
    {
        public Bits(TwitchAPI api)
        {
            V5 = new V5Api(api);
            Helix = new HelixApi(api);
        }

        public V5Api V5 { get; }
        public HelixApi Helix { get; }

        public class HelixApi : ApiSection
        {
            public HelixApi(TwitchAPI api) : base(api)
            {
            }

            #region GetBitsLeaderboard
            public Task<Models.Helix.Bits.GetBitsLeaderboardResponse> GetBitsLeaderboardAsync(int count = 10, BitsLeaderboardPeriodEnum period = BitsLeaderboardPeriodEnum.All, DateTime? startedAt = null, string userid = null, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_Bits_Read, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                getParams.Add(new KeyValuePair<string, string>("count", count.ToString()));
                switch(period)
                {
                    case BitsLeaderboardPeriodEnum.Day:
                        getParams.Add(new KeyValuePair<string, string>("period", "day"));
                        break;
                    case BitsLeaderboardPeriodEnum.Week:
                        getParams.Add(new KeyValuePair<string, string>("period", "week"));
                        break;
                    case BitsLeaderboardPeriodEnum.Month:
                        getParams.Add(new KeyValuePair<string, string>("period", "month"));
                        break;
                    case BitsLeaderboardPeriodEnum.Year:
                        getParams.Add(new KeyValuePair<string, string>("period", "year"));
                        break;
                    case BitsLeaderboardPeriodEnum.All:
                        getParams.Add(new KeyValuePair<string, string>("period", "all"));
                        break;
                }
                if (startedAt != null)
                    getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
                if (userid != null)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userid));

                return Api.TwitchGetGenericAsync<Models.Helix.Bits.GetBitsLeaderboardResponse>("/bits/leaderboard", ApiVersion.Helix, getParams);
            }
            #endregion 
        }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetCheermotes
            public Task<Models.v5.Bits.Cheermotes> GetCheermotesAsync(string channelId = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if (channelId != null)
                    getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("channel_id", channelId) };
                return Api.TwitchGetGenericAsync<Models.v5.Bits.Cheermotes>("/bits/actions", ApiVersion.v5, getParams);
            }
            #endregion
        }
    }
}