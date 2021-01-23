using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using Libre.Models;

namespace Libre.Infrastructure
{
    public class SearchSession 
    {

        [Newtonsoft.Json.JsonIgnore] public ISession Session { get; set; }

        private const string _search = "Search";
        public SearchSettings searchSetting;
        public static SearchSession GetSession(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SearchSession searchStringSession = session?.GetJson<SearchSession>(_search) ?? new SearchSession();

            searchStringSession.Session = session;

            return searchStringSession;
        }

        public void SetSearch(SearchSettings newSearch)
        {
            searchSetting = newSearch;
            Session.SetJson(_search, this);
        }

    }
}
