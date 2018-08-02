using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwitterSearcher.ViewModels;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using System.Linq;

namespace TwitterSearcher.Controllers
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

            SearchViewModel newSearchViewModel = new SearchViewModel();

            return View(newSearchViewModel);
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel newSearchViewModel)
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
                var searchParameter = new SearchTweetsParameters(newSearchViewModel.Keyword)
                {
                    Lang = LanguageFilter.English,
                    SearchType = SearchResultType.Mixed,
                    MaximumNumberOfResults = newSearchViewModel.SampleSize,
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
                        if (counter.ContainsKey(word.ToLower()))
                        {
                            counter[word.ToLower()]++;
                        }
                        else
                        {
                            counter.Add(word.ToLower(), 1);
                        }
                    }
                }

                //checks if word occurs more than 1/10 of the sample size and removes if it does - eliminiting one-off words.
                Dictionary<string, int> ccounter = new Dictionary<string, int>();
                foreach (KeyValuePair<string, int> word in counter)
                {
                    if (word.Value <= (newSearchViewModel.SampleSize / 100))
                    {
                        ccounter.Add(word.Key, word.Value);
                    }
                }
                foreach (KeyValuePair<string, int> word in ccounter)
                {
                    counter.Remove(word.Key);
                }

                //checks if word in list of 'fluff' words and removes them
                string[] fluff = new string[]{ newSearchViewModel.Keyword, "the", "and", "rt", " ", "", "a", "i", "in", "to", "of", "it", "you", "this" };
                foreach (string word in fluff)
                {
                    if (counter.ContainsKey(word))
                    {
                        counter.Remove(word);
                    }
                }
                //saves search if user is logged in
                //if (Session["UserID"] != null)
                {
                }
                //user sort-by
                if (newSearchViewModel.SortBy == "descending")
                {
                    IOrderedEnumerable<KeyValuePair<string, int>> SortedCounter = from entry in counter orderby entry.Value descending select entry;
                    newSearchViewModel.Counter = SortedCounter;
                    return View(newSearchViewModel);
                }
                else
                {
                    IOrderedEnumerable<KeyValuePair<string, int>> SortedCounter = from entry in counter orderby entry.Value ascending select entry;
                    newSearchViewModel.Counter = SortedCounter;
                    return View(newSearchViewModel);
                }
            }
            else
            {
                //If there is no input, the page is refreshed.
                return View(newSearchViewModel);
            }
        }
    }
}