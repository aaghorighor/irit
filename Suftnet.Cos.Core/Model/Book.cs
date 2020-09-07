namespace Suftnet.Cos.Core
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    public class Bible
    {
        [JsonProperty("chapter")]
        public long Chapter { get; set; }

        [JsonProperty("verse")]
        public long Verse { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("translation_id")]
        public string TranslationId { get; set; }

        [JsonProperty("book_id")]
        public string BookId { get; set; }

        [JsonProperty("book_name")]
        public string BookName { get; set; }
    }

    public class Bibles
    {
        [JsonProperty("bible")]
        Bible[] Bible { get; set; }
    }
}