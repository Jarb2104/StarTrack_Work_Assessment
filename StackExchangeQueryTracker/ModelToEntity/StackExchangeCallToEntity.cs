using SearchStatisticsDB.Entities;
using StackExchangeQueryTracker.Models;

namespace SearchStatisticsDB.ModelToEntity
{
    public class StackExchangeCallToEntity
    {
        public static void StackExchangeResponseModelToEntity(StackExchangeResponseModel responseModel, StackExchangeCall stackExchangeCall)
        {
            foreach (StackOverflowPost item in responseModel.items)
            {
                QueryResult queryResult = new QueryResult();
                queryResult.ResultID = Guid.NewGuid();
                queryResult.AnswerCount = item.answer_count;
                queryResult.Tittle = item.title;
                queryResult.UserName = item.owner.display_name;
                queryResult.PicURL = item.owner.profile_image;

                stackExchangeCall.Results.Add(queryResult);
            }
        }

        public static SearchStatisticsModel StackExchangeCallEntityToModel(List<StackExchangeCall> stackExchangeCalls)
        {

            SearchStatisticsModel searchStatisticsModel = new SearchStatisticsModel();
            searchStatisticsModel.TotalItems = stackExchangeCalls.Sum(sec => sec.Results.Count);
            searchStatisticsModel.LastQuery = stackExchangeCalls.Max(sec => sec.LastTimeRequested);
            searchStatisticsModel.SiteName = stackExchangeCalls[0].Site;
            searchStatisticsModel.SearchesDone = stackExchangeCalls.Sum(sec => sec.TimesRequested);

            return searchStatisticsModel;
        }
    }
}
