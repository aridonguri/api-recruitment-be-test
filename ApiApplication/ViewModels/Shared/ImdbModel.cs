using System.Collections.Generic;

namespace ApiApplication.ViewModels.Shared
{
    public class ImdbModel
    {
    }

    public class ActorList
    {
        public string id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string asCharacter { get; set; }
    }

    public class BoxOffice
    {
        public string budget { get; set; }
        public string openingWeekendUSA { get; set; }
        public string grossUSA { get; set; }
        public string cumulativeWorldwideGross { get; set; }
    }

    public class CompanyList
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class CountryList
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class DirectorList
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GenreList
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class LanguageList
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string title { get; set; }
        public string originalTitle { get; set; }
        public string fullTitle { get; set; }
        public string type { get; set; }
        public string year { get; set; }
        public string image { get; set; }
        public string releaseDate { get; set; }
        public string runtimeMins { get; set; }
        public string runtimeStr { get; set; }
        public string plot { get; set; }
        public string plotLocal { get; set; }
        public bool plotLocalIsRtl { get; set; }
        public string awards { get; set; }
        public string directors { get; set; }
        public List<DirectorList> directorList { get; set; }
        public string writers { get; set; }
        public List<WriterList> writerList { get; set; }
        public string stars { get; set; }
        public List<StarList> starList { get; set; }
        public List<ActorList> actorList { get; set; }
        public object fullCast { get; set; }
        public string genres { get; set; }
        public List<GenreList> genreList { get; set; }
        public string companies { get; set; }
        public List<CompanyList> companyList { get; set; }
        public string countries { get; set; }
        public List<CountryList> countryList { get; set; }
        public string languages { get; set; }
        public List<LanguageList> languageList { get; set; }
        public string contentRating { get; set; }
        public string imDbRating { get; set; }
        public string imDbRatingVotes { get; set; }
        public string metacriticRating { get; set; }
        public object ratings { get; set; }
        public object wikipedia { get; set; }
        public object posters { get; set; }
        public object images { get; set; }
        public object trailer { get; set; }
        public BoxOffice boxOffice { get; set; }
        public object tagline { get; set; }
        public string keywords { get; set; }
        public List<string> keywordList { get; set; }
        public List<Similar> similars { get; set; }
        public object tvSeriesInfo { get; set; }
        public object tvEpisodeInfo { get; set; }
        public string errorMessage { get; set; }
    }

    public class Similar
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imDbRating { get; set; }
    }

    public class StarList
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class WriterList
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
