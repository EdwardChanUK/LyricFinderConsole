using System.Xml.Serialization;

namespace LyricFinderCore.Finders.ChartLyrics
{
    [XmlRoot(ElementName = "GetLyricResult", Namespace = "http://api.chartlyrics.com/")]
    public class ChartLyricsSearchResult
    {
        [XmlElement("Lyric")]
        public string? Lyric { get; set; }
    }
}
