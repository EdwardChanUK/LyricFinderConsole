# LyricFinderConsole

Steps
1. Use Visual Studio 2022 to clone this repositary
2. Make sure installed .Net 6.0
3. Make sure the project LyricFinderConsole is the startup project
4. Rebuild solution
5. Start without debuging in Visual Studio

# Design
https://miro.com/app/board/uXjVPZSXGfM=/?share_link_id=106708272737

# Remarks
- Currently using chart lyrics api to retrieve the lyrics (Website: http://www.chartlyrics.com/api.aspx)
- The class LyricsOvhFinder has not used currently since the LyricsOvh production is down. (Details: https://github.com/NTag/lyrics.ovh/issues/15), but tested with mock server. So it can switch to use when their production server remain normal.
- And most of lyrics api has limited request which may not more than 4000 request per day if free.
