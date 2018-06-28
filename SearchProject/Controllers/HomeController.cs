using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwitterSearcher.Models;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace SearchProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            //On get, Validate user, create new viewmodel and display.

            Auth.SetUserCredentials("7lYht3bDy2P9ji0SXLl66feKi", 
                                    "k9hITdO7MmzeT57xTg75GrtneYjgwglXvoAawQ98XkPE21KMPB", 
                                    "963967640753655809-ZMrjjJP4F7lxCR94DUetJkROY6f1gpx", 
                                    "ILZMtk7b6lk4dBdekuJz5AvN9NPCghBw4IkqOVCTwuBfT");

            AnalyzeViewModel newAnalyzeViewModel = new AnalyzeViewModel();

            return View(newAnalyzeViewModel);
        }

        [HttpPost]
        public IActionResult Index(AnalyzeViewModel newAnalyzeViewModel)
        {
            //On Post, Verify the model and display data with form. If model not valid, refresh the page.
            if (ModelState.IsValid)
            {
                //Counter holds all accounted values.
                Dictionary<string, int> counter = new Dictionary<string, int>();

                //Content holds string arrays of all incoming tweets.
                List<string[]> content = new List<string[]>();

                
                /***Create a tweetinvi searchparameter in english, 
                 * that pulls both popular and recent posts and 
                 * sets the number of results to the user's sample size.
                 ***/
                var searchParameter = new SearchTweetsParameters(newAnalyzeViewModel.Keyword)
                {
                    Lang = LanguageFilter.English,
                    SearchType = SearchResultType.Mixed,
                    MaximumNumberOfResults = newAnalyzeViewModel.SampleSize,
                };

                //All posts are pulled here and added to the content list.
                var tweets = Search.SearchTweets(searchParameter);
                foreach (var tweet in tweets)
                {
                    content.Add(tweet.Text.Split());
                }

                //Each word and it's frequency is calculated and added to the Counter Dictionary.
                foreach (string[] text in content)
                {
                    foreach (string word in text)
                    {
                        if (counter.ContainsKey(word))
                        {
                            counter[word]++;
                        }
                        else
                        {
                            counter.Add(word, 1);
                        }
                    }
                }

                //Finally the ViewModel's Counter is set to the function's and the data is displayed in the view.
                newAnalyzeViewModel.Counter = counter;
                return View(newAnalyzeViewModel);
            }
            else
            {
                //If there is no input, the page is refreshed.
                return View(newAnalyzeViewModel);
            }
        }
    }
}