using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TweetAnalytics.Models {

    public partial class Tweet {
        public bool ContainsEmoji { get; set; }
        public bool ContainsPhotoUrls { get; set; }
        public bool ContainsUrls { get; set; }
        [JsonProperty ("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("id_str")]
        public string IdStr { get; set; }

        [JsonProperty ("text")]
        public string Text { get; set; }

        [JsonProperty ("source")]
        public string Source { get; set; }

        [JsonProperty ("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty ("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty ("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty ("in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty ("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty ("in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty ("user")]
        public User User { get; set; }

        [JsonProperty ("geo")]
        public object Geo { get; set; }

        [JsonProperty ("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty ("place")]
        public object Place { get; set; }

        [JsonProperty ("contributors")]
        public object Contributors { get; set; }

        [JsonProperty ("retweeted_status")]
        public RetweetedStatus RetweetedStatus { get; set; }

        [JsonProperty ("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty ("quote_count")]
        public long QuoteCount { get; set; }

        [JsonProperty ("reply_count")]
        public long ReplyCount { get; set; }

        [JsonProperty ("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty ("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty ("entities")]
        public Entities Entities { get; set; }

        [JsonProperty ("extended_entities")]
        public ExtendedEntities ExtendedEntities { get; set; }

        [JsonProperty ("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty ("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty ("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty ("filter_level")]
        public string FilterLevel { get; set; }

        [JsonProperty ("lang")]
        public string Lang { get; set; }

        [JsonProperty ("timestamp_ms")]
        public string TimestampMs { get; set; }
    }

    public partial class Entities {
        [JsonProperty ("hashtags")]
        public List<object> Hashtags { get; set; }

        [JsonProperty ("urls")]
        public List<TwitterUrl> Urls { get; set; }

        [JsonProperty ("user_mentions")]
        public List<UserMention> UserMentions { get; set; }

        [JsonProperty ("symbols")]
        public List<object> Symbols { get; set; }

        [JsonProperty ("media")]
        public List<ExtendedMedia> Media { get; set; }
    }

    public partial class ExtendedMedia { // this is media
        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("id_str")]
        public string IdStr { get; set; }

        [JsonProperty ("indices")]
        public List<long> Indices { get; set; }

        [JsonProperty ("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty ("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty ("url")]
        public Uri Url { get; set; }

        [JsonProperty ("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty ("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty ("type")]
        public string Type { get; set; }

        [JsonProperty ("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty ("source_status_id")]
        public string SourceStatusId { get; set; }

        [JsonProperty ("source_status_id_str")]
        public string SourceStatusIdStr { get; set; }

        [JsonProperty ("source_user_id")]
        public string SourceUserId { get; set; }

        [JsonProperty ("source_user_id_str")]
        public string SourceUserIdStr { get; set; }
    }

    public partial class Sizes {
        [JsonProperty ("thumb")]
        public Large Thumb { get; set; }

        [JsonProperty ("medium")]
        public Large Medium { get; set; }

        [JsonProperty ("small")]
        public Large Small { get; set; }

        [JsonProperty ("large")]
        public Large Large { get; set; }
    }

    public partial class Large {
        [JsonProperty ("w")]
        public long W { get; set; }

        [JsonProperty ("h")]
        public long H { get; set; }

        [JsonProperty ("resize")]
        public Resize Resize { get; set; }
    }

    public partial class UserMention {
        [JsonProperty ("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("id_str")]
        public string IdStr { get; set; }

        [JsonProperty ("indices")]
        public List<long> Indices { get; set; }
    }

    public partial class ExtendedEntities {
        [JsonProperty ("media")]
        public List<ExtendedMedia> Media { get; set; }
    }

    public partial class RetweetedStatus {
        [JsonProperty ("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("id_str")]
        public string IdStr { get; set; }

        [JsonProperty ("text")]
        public string Text { get; set; }

        [JsonProperty ("display_text_range")]
        public List<long> DisplayTextRange { get; set; }

        [JsonProperty ("source")]
        public string Source { get; set; }

        [JsonProperty ("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty ("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty ("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty ("in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty ("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty ("in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty ("user")]
        public User User { get; set; }

        [JsonProperty ("geo")]
        public object Geo { get; set; }

        [JsonProperty ("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty ("place")]
        public object Place { get; set; }

        [JsonProperty ("contributors")]
        public object Contributors { get; set; }

        [JsonProperty ("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty ("quote_count")]
        public long QuoteCount { get; set; }

        [JsonProperty ("reply_count")]
        public long ReplyCount { get; set; }

        [JsonProperty ("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty ("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty ("entities")]
        public RetweetedStatusEntities Entities { get; set; }

        [JsonProperty ("extended_entities")]
        public RetweetedStatusExtendedEntities ExtendedEntities { get; set; }

        [JsonProperty ("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty ("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty ("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty ("filter_level")]
        public string FilterLevel { get; set; }

        [JsonProperty ("lang")]
        public string Lang { get; set; }
    }

    public partial class RetweetedStatusEntities {
        [JsonProperty ("hashtags")]
        public List<object> Hashtags { get; set; }

        [JsonProperty ("urls")]
        public List<object> Urls { get; set; }

        [JsonProperty ("user_mentions")]
        public List<object> UserMentions { get; set; }

        [JsonProperty ("symbols")]
        public List<object> Symbols { get; set; }

        [JsonProperty ("media")]
        public List<FluffyMedia> Media { get; set; }
    }

    public partial class FluffyMedia {
        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("id_str")]
        public string IdStr { get; set; }

        [JsonProperty ("indices")]
        public List<long> Indices { get; set; }

        [JsonProperty ("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty ("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty ("url")]
        public Uri Url { get; set; }

        [JsonProperty ("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty ("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty ("type")]
        public string Type { get; set; }

        [JsonProperty ("sizes")]
        public Sizes Sizes { get; set; }
    }

    public partial class RetweetedStatusExtendedEntities {
        [JsonProperty ("media")]
        public List<FluffyMedia> Media { get; set; }
    }

    public partial class User {
        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("id_str")]
        public string IdStr { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty ("location")]
        public string Location { get; set; }

        [JsonProperty ("url")]
        public Uri Url { get; set; }

        [JsonProperty ("description")]
        public string Description { get; set; }

        [JsonProperty ("translator_type")]
        public string TranslatorType { get; set; }

        [JsonProperty ("protected")]
        public bool Protected { get; set; }

        [JsonProperty ("verified")]
        public bool Verified { get; set; }

        [JsonProperty ("followers_count")]
        public long FollowersCount { get; set; }

        [JsonProperty ("friends_count")]
        public long FriendsCount { get; set; }

        [JsonProperty ("listed_count")]
        public long ListedCount { get; set; }

        [JsonProperty ("favourites_count")]
        public long FavouritesCount { get; set; }

        [JsonProperty ("statuses_count")]
        public long StatusesCount { get; set; }

        [JsonProperty ("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty ("utc_offset")]
        public object UtcOffset { get; set; }

        [JsonProperty ("time_zone")]
        public object TimeZone { get; set; }

        [JsonProperty ("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty ("lang")]
        public object Lang { get; set; }

        [JsonProperty ("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty ("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty ("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty ("profile_background_image_url")]
        public string ProfileBackgroundImageUrl { get; set; }

        [JsonProperty ("profile_background_image_url_https")]
        public string ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty ("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty ("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty ("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        [JsonProperty ("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonProperty ("profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonProperty ("profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty ("profile_image_url")]
        public Uri ProfileImageUrl { get; set; }

        [JsonProperty ("profile_image_url_https")]
        public Uri ProfileImageUrlHttps { get; set; }

        [JsonProperty ("profile_banner_url")]
        public Uri ProfileBannerUrl { get; set; }

        [JsonProperty ("default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty ("default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty ("following")]
        public object Following { get; set; }

        [JsonProperty ("follow_request_sent")]
        public object FollowRequestSent { get; set; }

        [JsonProperty ("notifications")]
        public object Notifications { get; set; }

    }
    public class TwitterUrl
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }
        [JsonProperty("display_url")]
        public Uri DisplayUrl { get; set; }
        [JsonProperty("unwound")]
        public Unwound Unwound { get; set; }
        public object Indices { get; set; }
    }
    public partial class Unwound {
        public Uri Url { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public enum Resize { Crop, Fit }
}